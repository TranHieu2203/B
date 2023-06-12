Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Words

Public Class ctrlSeniorityProcess
    Inherits Common.CommonView

#Region "Property"

    Property objPeriod As List(Of ATPeriodDTO)
        Get
            Return ViewState(Me.ID & "_objPeriod")
        End Get
        Set(ByVal value As List(Of ATPeriodDTO))
            ViewState(Me.ID & "_objPeriod") = value
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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            ctrlUpload.isMultiple = False
            ctrlUpload.MaxFileInput = 1
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarTerminates
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Export,
                                       ToolbarItem.Import)
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New PayrollRepository
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
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kỳ lương"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                    .STATUS = 1,
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}
                    Using rep As New PayrollRepository
                        If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                            ShowMessage(Translate("Tồn tại kỳ công đang mở. Thao tác thực hiện không thành công"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    Using rep As New PayrollRepository
                        Dim _filter = New PASeniorityProcessDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                                     .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                                     .IS_DISSOLVE = ctrlOrg.IsDissolve}
                        If rep.CalSeniorityProcess(_filter) Then
                            ShowMessage("Tổng hợp thâm niên thành công.", NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage("Tổng hợp thâm niên không thành công.", NotifyType.Warning)
                        End If

                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kỳ lương"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(),
                                                        "javascriptfunction",
                                                        "ExportReport('Template_ImportSeniorityProcess" &
                                                        "&PERIOD_ID=" & cboPeriod.SelectedValue &
                                                        "&orgid=" & ctrlOrg.CurrentValue &
                                                        "&IS_DISSOLVE=" & IIf(ctrlOrg.IsDissolve, "1", "0") & "');", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kỳ lương"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlUpload.Show()

            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            If cboPeriod.SelectedValue <> "" Then
                Dim obj = (From p In objPeriod Where p.ID = cboPeriod.SelectedValue).FirstOrDefault
                If obj IsNot Nothing Then
                    Select Case obj.END_DATE.Value.Day
                        Case 31
                            rgData.Columns.FindByDataField("D28").Visible = True
                            rgData.Columns.FindByDataField("D29").Visible = True
                            rgData.Columns.FindByDataField("D30").Visible = True
                            rgData.Columns.FindByDataField("D31").Visible = True
                        Case 30
                            rgData.Columns.FindByDataField("D28").Visible = True
                            rgData.Columns.FindByDataField("D29").Visible = True
                            rgData.Columns.FindByDataField("D30").Visible = True
                            rgData.Columns.FindByDataField("D31").Visible = False
                        Case 29
                            rgData.Columns.FindByDataField("D28").Visible = True
                            rgData.Columns.FindByDataField("D29").Visible = True
                            rgData.Columns.FindByDataField("D30").Visible = False
                            rgData.Columns.FindByDataField("D31").Visible = False
                        Case 28
                            rgData.Columns.FindByDataField("D28").Visible = True
                            rgData.Columns.FindByDataField("D29").Visible = False
                            rgData.Columns.FindByDataField("D30").Visible = False
                            rgData.Columns.FindByDataField("D31").Visible = False
                    End Select
                End If
            End If
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        If Not IsPostBack Then
            If cboPeriod.SelectedValue <> "" Then
                ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.PA)
            End If
        End If
        Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                        .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                        .STATUS = 0,
                                        .IS_DISSOLVE = ctrlOrg.IsDissolve}

        Using rep As New PayrollRepository
            If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                _param.STATUS = 1
                If rep.IS_PERIODSTATUS(_param) Then
                    MainToolBar.Items(0).Enabled = True
                    MainToolBar.Items(1).Enabled = True
                    MainToolBar.Items(2).Enabled = True
                Else
                    MainToolBar.Items(0).Enabled = False
                    MainToolBar.Items(1).Enabled = False
                    MainToolBar.Items(2).Enabled = False
                End If
            Else
                MainToolBar.Items(0).Enabled = False
                MainToolBar.Items(1).Enabled = False
                MainToolBar.Items(2).Enabled = False
            End If
        End Using

        rgData.Rebind()
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        If cboPeriod.SelectedValue <> "" Then
            ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.PA)

            Dim _param = New PA_ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                            .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .STATUS = 0,
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Using rep As New PayrollRepository
                If rep.IS_PERIOD_COLEXSTATUS(_param) Then
                    _param.STATUS = 1
                    If rep.IS_PERIODSTATUS(_param) Then
                        MainToolBar.Items(0).Enabled = True
                        MainToolBar.Items(1).Enabled = True
                        MainToolBar.Items(2).Enabled = True
                    Else
                        MainToolBar.Items(0).Enabled = False
                        MainToolBar.Items(1).Enabled = False
                        MainToolBar.Items(2).Enabled = False
                    End If
                Else
                    MainToolBar.Items(0).Enabled = False
                    MainToolBar.Items(1).Enabled = False
                    MainToolBar.Items(2).Enabled = False
                End If
            End Using
        End If
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
            If dsDataPre.Tables.Count = 0 Then
                Return
            End If
            Dim dtData = dsDataPre.Tables(0).Clone()
            For Each dt As DataTable In dsDataPre.Tables
                For Each row In dt.Rows
                    dtData.ImportRow(row)
                Next
            Next

            Dim dtError As New DataTable("ERROR")
            Dim dtDataImport As New DataTable("ERROR")
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim irow = 4
            dtError = dtData.Clone
            dtDataImport = dtData.Clone
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    irow += 1
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                If row("EMPLOYEE_ID").ToString = "" OrElse Not IsNumeric(row("EMPLOYEE_ID").ToString) Then
                    rowError("EMPLOYEE_CODE") = "Mã nhân viên không phải hệ thống chiết xuất"
                End If
                sError = "Tỷ lệ % Mức thưởng phê duyệt phải nhập số"
                ImportValidate.IsValidNumber("PERCENT_SALARY", row, rowError, isError, sError)
                If rowError("PERCENT_SALARY").ToString = "" Then
                    If row("PERCENT_SALARY") > 100 Then
                        rowError("PERCENT_SALARY") = "Tỷ lệ % Mức thưởng phê duyệt không được > 100"
                        isError = True
                    End If
                    If row("PERCENT_SALARY") < 0 Then
                        rowError("PERCENT_SALARY") = "Tỷ lệ % Mức thưởng phê duyệt không được < 0"
                        isError = True
                    End If
                End If
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE")
                        rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME")
                        rowError("ORG_NAME") = row("ORG_NAME")
                        rowError("ORG_NAME2") = row("ORG_NAME2")
                        rowError("TITLE_NAME") = row("TITLE_NAME")
                        rowError("RANK_NAME") = row("RANK_NAME")
                        rowError("CONTRACT_TYPE_NAME") = row("CONTRACT_TYPE_NAME")
                        rowError("JOIN_DATE_STATE") = row("JOIN_DATE_STATE")
                        rowError("THAM_NIEN") = row("THAM_NIEN")
                        rowError("TIEN_TOI_DA") = row("TIEN_TOI_DA")
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImport.ImportRow(row)
                End If
                irow += 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSeniorityProcess_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            Using rep As New PayrollRepository
                If dtDataImport.Rows.Count > 0 Then
                    If rep.SaveSeniorityProcessImport(dtDataImport, cboPeriod.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgData.Rebind()
                    End If
                End If
            End Using
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Sub GetDataCombo()
        Dim rep As New PayrollRepository
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Dim total As Integer = 0
        Dim _filter As New PASeniorityProcessDTO
        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            SetValueObjectByRadGrid(rgData, _filter)
            _filter.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            _filter.PERIOD_ID = Utilities.ObjToDecima(cboPeriod.SelectedValue)
            _filter.IS_DISSOLVE = ctrlOrg.IsDissolve
            If Not isFull Then

                Dim lst As List(Of PASeniorityProcessDTO)
                If Sorts <> "" Then
                    lst = rep.GetSeniorityProcess(_filter,
                                                         rgData.MasterTableView.CurrentPageIndex,
                                                         rgData.MasterTableView.PageSize, total, Sorts)
                Else

                    lst = rep.GetSeniorityProcess(_filter,
                                                         rgData.MasterTableView.CurrentPageIndex,
                                                         rgData.MasterTableView.PageSize, total)

                End If
                rgData.VirtualItemCount = total
                rgData.DataSource = lst
            Else
                Return rep.GetSeniorityProcess(_filter, Sorts).ToTable
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class