Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSendMailLuong
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtData As New DataTable
    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    ''' <summary>
    ''' vData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo các thiết lập cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Bind dữ liệu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo cho toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.SendMail)

            MainToolBar.Items(0).Text = Translate("Gửi phiếu lương")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click của btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeach.Click
        Try
            rgData.Rebind()
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OKClick khi click ctrlUpload trên control ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
            If rep.SaveImport(Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
                ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
            End If
            rep.Dispose()
            rgData.DataSource = vData
            rgData.DataBind()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NoodeCheck của ctrlListSalary
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlListSalary_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ctrlListSalary.NodeCheck
        Try
            rgData.Rebind()
        Catch ex As Exception
        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click command trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                        row("COLDATA") = "&=DATA." & node.Value
                        dtColName.Rows.Add(row)
                    Next
                    Session("IMPORTSALARY_COLNAME") = dtColName
                    Using rep As New PayrollRepository
                        Dim dt = rep.GetImportSalary(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
                        Session("IMPORTSALARY_DATACOL") = dt
                    End Using
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSalary')", True)
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
                Case CommonMessage.TOOLBARITEM_SENDMAIL
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Using rep As New PayrollRepository

                        Dim lstEmployee As New List(Of Decimal)
                        For Each Item As GridDataItem In rgData.SelectedItems
                            lstEmployee.Add(Item.GetDataKeyValue("ID"))
                            'Dim receiver As String = ""
                            'Dim cc As String = ""
                            'Dim subject As String
                            'Dim body As String = ""
                            'Dim fileAttachments As String = String.Empty
                            'Dim view As String = ""
                            'subject = String.Format("Thông báo phiếu lương tháng {0}", Item.GetDataKeyValue("MONTH_DATE").ToString)

                            'receiver = Item.GetDataKeyValue("WORK_EMAIL")

                            'Dim path As Object = String.Format("{0}&fid=ctrlPA_PortalSalary", ConfigurationManager.AppSettings("PathPortalSalary"))
                            'body = String.Format("Kính gửi Anh/Chị {0} <br /><br />Phiếu lương tháng {1} của Anh/Chị đã được đăng tải lên cổng thông tin, Anh/Chị có thể truy cập <a href='{2}'>vào đây</a> xem chi tiết.<br /><br /> Cảm ơn!<br />Phòng NS kính báo.<br />Lưu ý: Email này được gởi tự động từ phần mềm nhân sự vui lòng không replly.", Item.GetDataKeyValue("FULLNAME_VN").ToString, Item.GetDataKeyValue("MONTH_DATE").ToString, path)


                            'If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, view) Then
                            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            'Else
                            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            '    Exit Sub
                            'End If
                        Next

                        Dim orgId As Decimal?
                        If lstEmployee.Count = 0 Then
                            orgId = ctrlOrg.CurrentValue
                        End If

                        'Gửi file đính kèm: do hệ thống chưa dùng nên không xài
                        If rep.ActionSendPayslip(lstEmployee,
                                              orgId,
                                              ctrlOrg.IsDissolve,
                                              cboPeriod.SelectedValue) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                        End If

                    End Using
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện buttonCommand của ctrlMessageBox 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của ctrlOrg
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ColumnCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgData.ColumnCreated
        Try
            CreateCol()
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của ctrlYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo dữ liệu filter cho rad grid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim rep As New PayrollRepository
        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim TotalRow As Decimal = 0
            If Sorts Is Nothing Then
                vData = rep.GET_DATA_SEND_MAIL(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
            Else
                vData = rep.GET_DATA_SEND_MAIL(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text), Sorts)
            End If
            rgData.VirtualItemCount = Utilities.ObjToInt(vData.Rows.Count)
            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If Not vData.Columns.Contains(node.Value) Then
                    vData.Columns.Add(node.Value)
                End If
            Next
            rep.Dispose()
            rgData.DataSource = vData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo cây lương
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateTreeSalaryNote()
        Try
            Dim rep As New PayrollRepository
            Dim objSalari As List(Of PAListSalariesDTO)
            objSalari = rep.GetSalaryList_TYPE(1)
            rep.Dispose()
            Dim node As New RadTreeNode
            node.Value = 0
            node.Text = "Check All"
            ctrlListSalary.Nodes.Add(node)
            For Each item In objSalari
                Dim nodeChild As New RadTreeNode
                nodeChild.Value = item.COL_NAME
                nodeChild.Text = item.NAME_VN
                node.Nodes.Add(nodeChild)
            Next
            ctrlListSalary.Nodes.Add(node)
            ctrlListSalary.ExpandAllNodes()

        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức tạo danh sách GridBoundColumn
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateCol()
        Try
            Dim listcol() As String = {"cbStatus", "EMPLOYEE_CODE", "FULLNAME_VN", "ORG_NAME"}
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
                rgData.MasterTableView.Columns.Add(col)
                stringKey.Add(col.DataField)
            Next
            rgData.MasterTableView.ClientDataKeyNames = stringKey.ToArray
        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 31/08/2017 16:47
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy dữ liệu cho danh sách combobox
    ''' </summary>
    ''' <remarks></remarks>
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
            objSalatyType = rep.GetSalaryTypebyIncentive(0)
            cboSalaryType.DataSource = objSalatyType
            cboSalaryType.DataValueField = "ID"
            cboSalaryType.DataTextField = "NAME"
            cboSalaryType.DataBind()
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class