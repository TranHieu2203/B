Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlImportQuyThuongHQCV
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
#Region "Property"
    Dim dtData As New DataTable
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property vData As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataView")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataView") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            rgData.PageSize = 50
            GetDataCombo()
            CreateTreeSalaryNote()
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.Import)

            MainToolBar.Items(0).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(1).Text = Translate("Nhập file mẫu")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub btnSeach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeach.Click
        rgData.Rebind()
    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim fileName As String
        Dim dsDataPre As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPre.Tables.Add(worksheet.Cells.ExportDataTableAsString(2, 0, worksheet.Cells.MaxRow - 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            If dsDataPre.Tables.Count > 0 Then
                For Each s As String In rgData.MasterTableView.ClientDataKeyNames
                    If Not dsDataPre.Tables(0).Columns.Contains(s) Then
                        dsDataPre.Tables(0).Columns.Add(s)
                    End If
                Next
                vData = dsDataPre.Tables(0)
            End If

            Dim rep As New PayrollRepository
            Dim stringKey As New List(Of String)
            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If node.Value = "NULL" Or node.Value = "0" Then Continue For
                stringKey.Add(node.Value)
            Next
            Dim RecordSussces As Integer = 0
            If stringKey.Count <= 0 Then
                ShowMessage("Bạn phải chọn ít nhất 1 khoản tiền", NotifyType.Warning)
                Exit Sub
            End If
            If rep.SaveImportBonus(Utilities.ObjToDecima(ctrlOrg.CurrentValue), Utilities.ObjToDecima(cboYear.SelectedValue), Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
                ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
            End If
            rep.Dispose()
            rgData.Rebind()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlListSalary_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ctrlListSalary.NodeCheck
        Try
            rgData.Rebind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Try
            Try
                rgData.CurrentPageIndex = 0
                rgData.MasterTableView.SortExpressions.Clear()
                CreateDataFilter()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtColName As New DataTable
                    dtColName.Columns.Add("COLVAL")
                    dtColName.Columns.Add("COLNAME")
                    dtColName.Columns.Add("COLDATA")

                    For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                        If node.Value = "NULL" Or node.Value = "0" Then Continue For
                        Dim row As DataRow = dtColName.NewRow
                        row("COLVAL") = node.Value
                        row("COLNAME") = node.Text
                        dtColName.Rows.Add(row)
                    Next
                    Session("IMPORTSALARY_DATACOL") = dtColName
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportQuyThuongHQCV')", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim rep As New PayrollRepository
                    Dim stringKey As New List(Of String)
                    For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                        If node.Value = "NULL" Or node.Value = "0" Then Continue For
                        stringKey.Add(node.Value)
                    Next
                    Dim RecordSussces As Integer = 0
                    If stringKey.Count <= 0 Then
                        ShowMessage("Bạn phải chọn ít nhất 1 khoản tiền", NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.SaveImport(Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
                        ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
                    End If
                    rep.Dispose()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ColumnCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgData.ColumnCreated
        'CreateCol()
    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Try
            cboPeriod.ClearSelection()
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()
            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
            rep.Dispose()
            CreateTreeSalaryNote()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Sub CreateDataFilter()
        Dim rep As New PayrollRepository
        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim TotalRow As Decimal = 0
            If Sorts Is Nothing Then
                vData = rep.GetImportSalaryBonus(Utilities.ObjToInt(cboYear.SelectedValue), Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve)
            Else
                vData = rep.GetImportSalaryBonus(Utilities.ObjToInt(cboYear.SelectedValue), Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Sorts)
            End If
            rep.Dispose()
            rgData.VirtualItemCount = Utilities.ObjToInt(vData.Rows.Count)
            rgData.DataSource = vData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub CreateTreeSalaryNote()
        Try
            ctrlListSalary.Nodes.Clear()
            Dim rep As New PayrollRepository
            Dim objSalari As List(Of OrgBonusDTO)
            objSalari = rep.GetListOrgBonus() 'rep.GetSalaryList_TYPE(1)
            Dim node As New RadTreeNode
            node.Value = 0
            node.Text = "Check All"
            ctrlListSalary.Nodes.Add(node)
            For Each item In objSalari
                Dim nodeChild As New RadTreeNode
                nodeChild.Value = item.ID
                nodeChild.Text = item.NAME
                node.Nodes.Add(nodeChild)
            Next
            rep.Dispose()
            ctrlListSalary.Nodes.Add(node)
            ctrlListSalary.ExpandAllNodes()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CreateCol()
        Try
            Dim listcol() As String = {"cbStatus", "PAYMENTSOURCES_ID", "NAME", "MONEY"}
            Dim i As Integer = 0
            While (i < rgData.Columns.Count)
                Dim c As GridColumn = rgData.Columns(i)
                If Not listcol.Contains(c.UniqueName) Then
                    rgData.Columns.Remove(c)
                    Continue While
                End If
                i = i + 1
            End While
            Dim stringKey As New List(Of String)
            stringKey.Add("ID")
            stringKey.Add("EMPLOYEE_CODE")
            stringKey.Add("FULLNAME_VN")
            stringKey.Add("ORG_NAME")
            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If node.Value = "NULL" Or node.Value = "0" Then Continue For
                Dim col As New GridBoundColumn
                col.HeaderText = node.Text
                col.DataField = node.Value
                col.UniqueName = node.Value
                col.HeaderStyle.Width = 120
                col.DataFormatString = "{0:#,##0.##}"
                rgData.MasterTableView.Columns.Add(col)
                stringKey.Add(col.DataField)
            Next
            rgData.MasterTableView.ClientDataKeyNames = stringKey.ToArray
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetDataCombo()
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim objSalatyType As List(Of SalaryTypeDTO)
        Dim id As Integer = 0
        Try
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()
            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
            'Get Salary Type
            objSalatyType = rep.GetSalaryTypebyIncentive(1)
            cboSalaryType.DataSource = objSalatyType
            cboSalaryType.DataValueField = "CODE"
            cboSalaryType.DataTextField = "NAME"
            cboSalaryType.DataBind()
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class