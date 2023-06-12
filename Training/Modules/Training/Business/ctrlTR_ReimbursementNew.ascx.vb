Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports Training.TrainingBusiness


Public Class ctrlTR_ReimbursementNew
    Inherits Common.CommonView

    Protected WithEvents ctrlFindReimbursementPopup As ctrlFindEmployeePopup
#Region "Property"

    Public Property Reimbursements As List(Of ProgramCommitDTO)
        Get
            Return ViewState(Me.ID & "_Reimbursements")
        End Get
        Set(ByVal value As List(Of ProgramCommitDTO))
            ViewState(Me.ID & "_Reimbursements") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    '0 - normal
    '1 - Reimbursement
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set

    End Property

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Lock,
                                       ToolbarItem.Unlock,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Import,
                                       ToolbarItem.Delete)
            'Common.Common.BuildToolbar(Me.MainToolBar,
            '                           ToolbarItem.Create,
            '                           ToolbarItem.Edit,
            '                           ToolbarItem.Seperator,
            '                           ToolbarItem.Save,
            '                           ToolbarItem.Cancel,
            '                           ToolbarItem.Delete)
            'CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            MainToolBar.Items(4).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(5).Text = Translate("Nhập file mẫu")
            MainToolBar.Items(2).Text = Translate("Xử lý")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()

                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            If phFindReimbursement.Controls.Contains(ctrlFindReimbursementPopup) Then
                phFindReimbursement.Controls.Remove(ctrlFindReimbursementPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindReimbursementPopup = Me.Register("ctrlFindReimbursementPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindReimbursementPopup.MustHaveContract = False
                    phFindReimbursement.Controls.Add(ctrlFindReimbursementPopup)
                    ctrlFindReimbursementPopup.MultiSelect = False
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    UpdateCtrlState(True)

                Case CommonMessage.STATE_NORMAL
                    'If rgMain.SelectedValue IsNot Nothing Then
                    '    Dim item = (From p In Reimbursements Where p.ID = rgMain.SelectedValue).SingleOrDefault
                    '    rntxtYear.Value = item.YEAR
                    '    cboCourse.SelectedValue = item.TR_PROGRAM_ID
                    '    'rdStartDate.SelectedDate = item.START_DATE
                    '    'hidEmployeeID.Value = item.EMPLOYEE_ID
                    '    'txtNote.Text = item.REMARK
                    'End If
                    UpdateCtrlState(False)

                Case CommonMessage.STATE_EDIT
                    UpdateCtrlState(True)
                Case CommonMessage.STATE_DELETE
                    Dim List_ID = New List(Of Decimal)

                    For Each _item As GridDataItem In rgMain.SelectedItems
                        List_ID.Add(_item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteReimbursement(List_ID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()

                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub UpdateCtrlState(ByVal state As Boolean)
        Try
            'txtSeachEmployee.ReadOnly = state
            'btnSearch.Enabled = Not state

            'rntxtYear.ReadOnly = Not state
            'txtNote.ReadOnly = Not state
            'rntxtYear.AutoPostBack = state
            'Utilities.EnableRadCombo(cboCourse, state)
            'cboCourse.AutoPostBack = state
            '' EnableRadDatePicker(rdStartDate, state)

            'EnabledGridNotPostback(rgMain, Not state)
            ''EnabledGrid(rgMain, Not state, False)
            ''rgMain.AllowMultiRowSelection = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearCtrlValue()
        Try
            rntxtYear.Value = Date.Now.Year
            cboCourse.ClearSelection()
            cboCourse.Text = ""
            'rdStartDate.SelectedDate = Nothing
            txtNote.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try


            rntxtYear.Value = Date.Now.Year
            GetPlanInYearOrg()

            Dim hidBind As HiddenField = New HiddenField
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidBind)

            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objReimbursement As New ProgramCommitDTO
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    Dim List_ID = New List(Of Decimal)
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        Dim Count = True
                        For Each _item As GridDataItem In rgMain.SelectedItems
                            If _item.GetDataKeyValue("IS_LOCK") = -1 Then
                                ShowMessage("Dữ liệu đang trạng thái khóa, không thể thực hiện", NotifyType.Warning)
                                Exit Sub
                            End If
                            Dim obj As New ProgramCommitDTO
                            Dim ID = _item.GetDataKeyValue("ID")
                            obj.ID = ID
                            If chkWithTer.Checked Then
                                obj.CLOSING_DATE = _item.GetDataKeyValue("TER_DATE")
                            Else
                                obj.CLOSING_DATE = rdCLOSING_DATE.SelectedDate
                            End If
                            'So ngay cam ket
                            Dim ConvertTime = _item.GetDataKeyValue("CONVERED_TIME")
                            'Den ngay camket
                            Dim CommitEnd = _item.GetDataKeyValue("COMMIT_END")
                            'Tong so tien cam ket
                            Dim MoneyCommit = _item.GetDataKeyValue("MONEY_COMMIT")
                            obj.REIMBURSE_TIME = 0
                            If IsDate(CommitEnd) AndAlso IsDate(obj.CLOSING_DATE) Then
                                Dim Totalday = (CDate(CommitEnd) - CDate(obj.CLOSING_DATE)).TotalDays() + 1
                                If Totalday > 0 Then
                                    obj.REIMBURSE_TIME = Totalday
                                End If
                            End If
                            obj.MONEY_REIMBURSE = 0
                            If IsNumeric(MoneyCommit) AndAlso IsNumeric(ConvertTime) AndAlso IsNumeric(obj.REIMBURSE_TIME) AndAlso ConvertTime > 0 AndAlso obj.REIMBURSE_TIME > 0 AndAlso MoneyCommit > 0 Then
                                obj.MONEY_REIMBURSE = CDec(MoneyCommit) / CDec(ConvertTime) * CDec(obj.REIMBURSE_TIME)
                            End If
                            Count = rep.ModifyProgramCommit(obj, ID)
                        Next
                        Refresh("UpdateView")
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "Reimbursement")

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_LOCK
                    Dim List_ID = New List(Of Decimal)
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        For Each _item As GridDataItem In rgMain.SelectedItems
                            List_ID.Add(_item.GetDataKeyValue("ID"))
                        Next
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveReimbursement(List_ID, True) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_UNLOCK
                    Dim List_ID = New List(Of Decimal)
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        For Each _item As GridDataItem In rgMain.SelectedItems
                            List_ID.Add(_item.GetDataKeyValue("ID"))
                        Next
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveReimbursement(List_ID, False) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim sps As New TrainingStoreProcedure
                    Dim dataSet As New DataSet
                    Dim tempPath = "~/ReportTemplates//Training//Import//ImportBoiHoanDT.xlsx"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    dataSet = sps.EXPORT_REIMBURSEMENT()
                    dataSet.Tables(0).TableName = "DATA1"
                    dataSet.Tables(1).TableName = "DATA2"
                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "ImportBoiHoanDT" & Format(Date.Now, "yyyyMMddHHmmss"), dataSet, Nothing, Response)
                    End Using

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.isMultiple = False
                    ctrlUpload.Show()

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New TrainingRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_REIMBURSEMENT(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('ImportBoiHoanDT_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New TrainingRepository
        Dim psp As New TrainingStoreProcedure
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(1).ColumnName = "TR_PROGRAM_CODE"
            dtTemp.Columns(2).ColumnName = "CLOSING_DATE"
            dtTemp.Columns(3).ColumnName = "MONTH_PERIOD"
            dtTemp.Columns(4).ColumnName = "REIMBURSE_REMARK"
            dtTemp.Columns(5).ColumnName = "EMPLOYEE_ID"
            dtTemp.Columns(6).ColumnName = "TR_PROGRAM_ID"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            If dtLogs Is Nothing Then
                dtLogs = dtTemp.Clone
                dtLogs.Columns.Add("STT").SetOrdinal(0)
                dtLogs.TableName = "DATA"
            End If
            dtLogs.Clear()
            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim rep1 As New CommonRepository
            Dim store As New TrainingStoreProcedure
            Dim result As Date
            Dim stt As Decimal = 0

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                newRow = dtLogs.NewRow
                newRow("STT") = stt + 1

                If rows("TR_PROGRAM_CODE").ToString.Trim = "" Then
                    newRow("TR_PROGRAM_CODE") = "Khóa đào tạo không để trống"
                    _error = False
                ElseIf store.CHECK_EXIST_PROGRAM(Decimal.Parse(rows("TR_PROGRAM_ID").ToString.Trim)) = False Then
                    newRow("TR_PROGRAM_ID") = "Khóa đào tạo không tồn tại"
                    _error = False
                ElseIf store.CHECK_VALID_EMPLOYEE_REIMBURSEMENT(Decimal.Parse(rows("TR_PROGRAM_ID").ToString.Trim), Decimal.Parse(rows("EMPLOYEE_ID").ToString.Trim)) = False Then
                    newRow("TR_PROGRAM_ID") = "Nhân viên không tồn tại theo khóa đào tạo hoặc nhân viên không có cam kết bồi hoàn"
                    _error = False
                End If

                If rows("CLOSING_DATE").ToString.Trim = "" Then
                    newRow("CLOSING_DATE") = "Ngày chốt bồi hoàn không để trống"
                    _error = False
                ElseIf DateTime.TryParseExact(rows("CLOSING_DATE").ToString.Trim, "dd/MM/yyyy", New Globalization.CultureInfo("en-US"), Globalization.DateTimeStyles.None, result) = False Then
                    newRow("CLOSING_DATE") = "Ngày chốt bồi hoàn không đúng định dạng"
                    _error = False
                ElseIf store.CHECK_VALID_DATE_REIMBURSEMENT(Decimal.Parse(rows("TR_PROGRAM_ID").ToString.Trim), Decimal.Parse(rows("EMPLOYEE_ID").ToString.Trim), result) = False Then
                    newRow("CLOSING_DATE") = "Ngày bồi hoàn phải lớn hơn Từ ngày cam kết bồi hoàn"
                    _error = False
                End If

                If rows("MONTH_PERIOD").ToString.Trim <> "" AndAlso DateTime.TryParseExact(rows("MONTH_PERIOD").ToString.Trim, "MM/yyyy", New Globalization.CultureInfo("en-US"), Globalization.DateTimeStyles.None, result) = False Then
                    newRow("MONTH_PERIOD") = "Tháng bồi hoàn không đúng định dạng"
                    _error = False
                End If

                If _error = False Then
                    dtLogs.Rows.Add(newRow)
                    _error = True
                End If
                stt += 1
            Next
            dtTemp.AcceptChanges()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub CreateDataFilter()
        Dim rep As New TrainingRepository
        Dim obj As New ProgramCommitDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)
            If cboCourse.SelectedValue <> "" Then
                obj.COURSE_SEARCH = cboCourse.SelectedValue
            End If
            If rdCOMMIT_START_T_SEARCH.SelectedDate IsNot Nothing Then
                obj.COMMIT_START_T_SEARCH = rdCOMMIT_START_T_SEARCH.SelectedDate
            End If
            obj.EMP_CODE_SEARCH = txtSeachEmployee.Text
            If rdCOMMIT_START_E_SEARCH.SelectedDate IsNot Nothing Then
                obj.COMMIT_START_E_SEARCH = rdCOMMIT_START_E_SEARCH.SelectedDate
            End If
            obj.YEAR_SEARCH = rntxtYear.Text
            If rdCOMMIT_END_T_SEARCH.SelectedDate IsNot Nothing Then
                obj.COMMIT_END_T_SEARCH = rdCOMMIT_END_T_SEARCH.SelectedDate
            End If
            If rdCOMMIT_END_E_SEARCH.SelectedDate IsNot Nothing Then
                obj.COMMIT_END_E_SEARCH = rdCOMMIT_END_E_SEARCH.SelectedDate
            End If
            If chkTerminate.Checked Or chkWithTer.Checked Then
                obj.IS_TER_SEARCH = True
            End If

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Reimbursements = rep.GetReimbursementNew(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
            Else
                Me.Reimbursements = rep.GetReimbursementNew(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Me.Reimbursements
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgMain.Rebind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
    Protected Sub btnDienNhanh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDienNhanh.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New TrainingRepository
            If rgMain.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            Else
                Dim item = CType(rgMain.SelectedItems(0), GridDataItem)
                For Each _item As GridDataItem In rgMain.SelectedItems
                    Dim IS_LOCK_NAME = item.GetDataKeyValue("IS_LOCK_NAME")
                    If IS_LOCK_NAME = "X" Then
                        ShowMessage("Dữ liệu đang trạng thái khóa, không thể thực hiện", NotifyType.Warning)
                        Exit Sub
                    End If
                Next

                For Each _item As GridDataItem In rgMain.SelectedItems
                    Dim MONTH_PERIOD = item.GetDataKeyValue("MONTH_PERIOD")
                    Dim REIMBURSE_REMARK = item.GetDataKeyValue("REIMBURSE_REMARK")
                    Dim obj As New ProgramCommitDTO
                    Dim ID = _item.GetDataKeyValue("ID")
                    obj.ID = ID
                    If IsDate(rdMONTH_PERIOD.SelectedDate) Then
                        obj.MONTH_PERIOD = CDate(rdMONTH_PERIOD.SelectedDate).ToString("MM/yyyy")
                    End If
                    obj.REIMBURSE_REMARK = txtNote.Text
                    rep.FastUpdateProgramCommit(obj, ID)
                Next
                Refresh("UpdateView")
                UpdateControlState()
                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
    'Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
    '    Try
    '        If rgMain.SelectedItems.Count = 0 Then Exit Sub

    '        Dim item As GridDataItem = CType(rgMain.SelectedItems(0), GridDataItem)
    '        If item.GetDataKeyValue("YEAR") IsNot Nothing Then
    '            rntxtYear.Value = CDec(item.GetDataKeyValue("YEAR"))
    '        Else
    '            rntxtYear.Value = Nothing
    '        End If
    '        GetPlanInYearOrg()

    '        If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
    '            cboCourse.SelectedValue = CDec(item.GetDataKeyValue("TR_PROGRAM_ID"))
    '        Else
    '            cboCourse.SelectedValue = Nothing
    '            cboCourse.ClearSelection()
    '            cboCourse.Items.Clear()
    '        End If


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub





    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub GetPlanInYearOrg()
        Try
            If rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramByYear(True, rntxtYear.Value)
                    FillRadCombobox(cboCourse, dtData, "NAME", "ID")
                End Using
            Else
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub chkWithTer_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If chkWithTer.Checked Then
                rdCLOSING_DATE.ClearValue()
            End If
            rgMain.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdCLOSING_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Calendar.SelectedDateChangedEventArgs) Handles rdCLOSING_DATE.SelectedDateChanged
        Try
            If IsDate(rdCLOSING_DATE.SelectedDate) Then
                chkWithTer.Checked = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"
#End Region
End Class