Imports System.Globalization
Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_Reimbursement
    Inherits Common.CommonView

    Protected WithEvents ctrlFindReimbursementPopup As ctrlFindEmployeePopup
#Region "Property"

    Public Property Reimbursements As List(Of ReimbursementDTO)
        Get
            Return ViewState(Me.ID & "_Reimbursements")
        End Get
        Set(ByVal value As List(Of ReimbursementDTO))
            ViewState(Me.ID & "_Reimbursements") = value
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
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Import)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = "Xuất file mẫu"
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = "Nhập file mẫu"
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
                    If rgMain.SelectedValue IsNot Nothing Then
                        Dim item = (From p In Reimbursements Where p.ID = rgMain.SelectedValue).SingleOrDefault
                        rntxtYear.Value = item.YEAR
                        cboCourse.SelectedValue = item.TR_PROGRAM_ID
                        rntxtCostOfStudent.Value = item.COST_OF_STUDENT
                        rdStartDate.SelectedDate = item.START_DATE
                        hidEmployeeID.Value = item.EMPLOYEE_ID
                        txtCode.Text = item.EMPLOYEE_CODE
                        txtName.Text = item.EMPLOYEE_NAME
                        txtNote.Text = item.REMARK
                    End If
                    UpdateCtrlState(False)
                    rntxtSeachYear.Value = Date.Now.Year

                Case CommonMessage.STATE_EDIT
                    UpdateCtrlState(True)
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub UpdateCtrlState(ByVal state As Boolean)
        Try
            'rntxtSeachYear.ReadOnly = state
            'cboSearchCourse.Enabled = Not state
            'txtSeachEmployee.ReadOnly = state
            'btnSearch.Enabled = Not state

            ''rntxtYear.ReadOnly = Not state
            'txtNote.ReadOnly = Not state
            'rnReimburseCost.ReadOnly = Not state
            'rntxtYear.AutoPostBack = state
            'Utilities.EnableRadCombo(cboCourse, state)
            'cboCourse.AutoPostBack = state
            'cbReserves.ReadOnly = Not state
            'btnFindReimbursement.Enabled = True
            'EnableRadDatePicker(rdStartDate, state)

            'EnabledGridNotPostback(rgMain, Not state)
            ''EnabledGrid(rgMain, Not state, False)
            'rgMain.AllowMultiRowSelection = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearCtrlValue()
        Try
            rntxtYear.Value = Date.Now.Year
            cboCourse.ClearSelection()
            cboCourse.Text = ""
            rntxtCostOfStudent.Text = ""
            cbReserves.Checked = False
            rdStartDate.SelectedDate = Nothing
            txtCode.Text = ""
            txtName.Text = ""
            txtNote.Text = ""
            rnReimburseCost.ClearValue()
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
            rdTerDate.DatePopupButton.Enabled = False
            rdEndDate.DatePopupButton.Enabled = False

            rntxtSeachYear.Value = Date.Now.Year
            GetPlanInYearOrg2()

            rntxtYear.Value = Date.Now.Year
            GetPlanInYearOrg()

            Dim hidBind As HiddenField = New HiddenField
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidBind)

            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("YEAR", rntxtYear)
            'dic.Add("TR_PROGRAM_ID;TR_PROGRAM_NAME", cboCourse)
            'dic.Add("COST_OF_STUDENT", rntxtCostOfStudent)
            'dic.Add("IS_RESERVES", cbReserves)
            'dic.Add("START_DATE", rdStartDate)
            'dic.Add("EMPLOYEE_ID", hidEmployeeID)
            'dic.Add("EMPLOYEE_CODE", txtCode)
            'dic.Add("EMPLOYEE_NAME", txtName)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objReimbursement As New ReimbursementDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearCtrlValue()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If item.GetDataKeyValue("TO_DATE") Is Nothing Then
                            ShowMessage(Translate("Chương trình của nhân viên này không có ngày kết thúc.<br />Bạn vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("COMMIT_WORK") Is Nothing Then
                            ShowMessage(Translate("Nhân viên này chưa được cam kết đào tạo.<br />Bạn vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    CurrentState = CommonMessage.STATE_EDIT
                    'rntxtYear_TextChanged(Nothing, Nothing)
                    'cboCourse.ClearSelection()
                    'cboCourse.Items.Clear()

                    'Dim item As GridDataItem = rgMain.SelectedItems(0)
                    'If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
                    '    cboCourse.SelectedValue = item.GetDataKeyValue("TR_PROGRAM_ID")
                    'End If
                    'hidEmployeeID.Value = item.GetDataKeyValue("EMPLOYEE_ID")
                    'txtCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE")
                    'txtName.Text = item.GetDataKeyValue("EMPLOYEE_NAME")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "Reimbursement")

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim CW As Decimal = -1
                        Dim TD As Date = Today
                        For Each item As GridDataItem In rgMain.SelectedItems
                            If item.GetDataKeyValue("TO_DATE") IsNot Nothing Then
                                TD = CDate(item.GetDataKeyValue("TO_DATE"))
                            End If
                            If item.GetDataKeyValue("COMMIT_WORK") IsNot Nothing Then
                                CW = CDec(item.GetDataKeyValue("COMMIT_WORK"))
                            End If
                        Next

                        With objReimbursement
                            .COST_REIMBURSE = rnReimburseCost.Value
                            .REMARK = txtNote.Text
                            .YEAR = rntxtYear.Value
                            .TR_PROGRAM_ID = cboCourse.SelectedValue
                            .COST_OF_STUDENT = rntxtCostOfStudent.Value
                            .IS_RESERVES = cbReserves.Checked
                            .START_DATE = rdStartDate.SelectedDate
                            .EMPLOYEE_ID = hidEmployeeID.Value
                            .COMMIT_WORK = CW
                            .TO_DATE = TD
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertReimbursement(objReimbursement, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objReimbursement.ID = rgMain.SelectedValue
                                If rep.ModifyReimbursement(objReimbursement, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objReimbursement.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

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
                ElseIf DateTime.TryParseExact(rows("CLOSING_DATE").ToString.Trim, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result) = False Then
                    newRow("CLOSING_DATE") = "Ngày chốt bồi hoàn không đúng định dạng"
                    _error = False
                ElseIf store.CHECK_VALID_DATE_REIMBURSEMENT(Decimal.Parse(rows("TR_PROGRAM_ID").ToString.Trim), Decimal.Parse(rows("EMPLOYEE_ID").ToString.Trim), result) = False Then
                    newRow("CLOSING_DATE") = "Ngày bồi hoàn phải lớn hơn Từ ngày cam kết bồi hoàn"
                    _error = False
                End If

                If rows("MONTH_PERIOD").ToString.Trim <> "" AndAlso DateTime.TryParseExact(rows("MONTH_PERIOD").ToString.Trim, "MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result) = False Then
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
        Dim obj As New ReimbursementDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)

            obj.YEAR = rntxtSeachYear.Value
            If cboSearchCourse.SelectedValue <> "" Then
                obj.TR_PROGRAM_ID = cboSearchCourse.SelectedValue
            End If
            obj.EMPLOYEE_CODE = txtSeachEmployee.Text

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Reimbursements = rep.GetReimbursement(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            Else
                Me.Reimbursements = rep.GetReimbursement(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Me.Reimbursements
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Try
            If rgMain.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = CType(rgMain.SelectedItems(0), GridDataItem)
            If item.GetDataKeyValue("YEAR") IsNot Nothing Then
                rntxtYear.Value = CDec(item.GetDataKeyValue("YEAR"))
            Else
                rntxtYear.Value = Nothing
            End If
            GetPlanInYearOrg()

            If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
                cboCourse.SelectedValue = CDec(item.GetDataKeyValue("TR_PROGRAM_ID"))
            Else
                cboCourse.SelectedValue = Nothing
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If

            If item.GetDataKeyValue("COST_OF_STUDENT") IsNot Nothing Then
                rntxtCostOfStudent.Value = CDec(item.GetDataKeyValue("COST_OF_STUDENT"))
            Else
                rntxtCostOfStudent.ClearValue()
            End If

            If item.GetDataKeyValue("IS_RESERVES") IsNot Nothing Then
                cbReserves.Checked = CBool(item.GetDataKeyValue("IS_RESERVES"))
            Else
                cbReserves.Checked = False
            End If

            If item.GetDataKeyValue("START_DATE") IsNot Nothing Then
                rdStartDate.SelectedDate = CDate(item.GetDataKeyValue("START_DATE"))
            Else
                rdStartDate.SelectedDate = Nothing
            End If

            If item.GetDataKeyValue("EMPLOYEE_ID") IsNot Nothing Then
                hidEmployeeID.Value = CDec(item.GetDataKeyValue("EMPLOYEE_ID"))
            Else
                hidEmployeeID.Value = Nothing
            End If

            If item.GetDataKeyValue("EMPLOYEE_CODE") IsNot Nothing Then
                txtCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE").ToString
            Else
                txtCode.Text = ""
            End If

            If item.GetDataKeyValue("EMPLOYEE_NAME") IsNot Nothing Then
                txtName.Text = item.GetDataKeyValue("EMPLOYEE_NAME").ToString
            Else
                txtName.Text = ""
            End If

            If item.GetDataKeyValue("TER_DATE") IsNot Nothing Then
                rdTerDate.SelectedDate = CDate(item.GetDataKeyValue("TER_DATE"))
            Else
                rdTerDate.SelectedDate = Nothing
            End If

            If item.GetDataKeyValue("COMMIT_END") IsNot Nothing Then
                rdEndDate.SelectedDate = CDate(item.GetDataKeyValue("COMMIT_END"))
            Else
                rdEndDate.SelectedDate = Nothing
            End If

            If item.GetDataKeyValue("CONVERED_TIME") IsNot Nothing Then
                rnTotalCommitDays.Value = CDec(item.GetDataKeyValue("CONVERED_TIME"))
            Else
                rnTotalCommitDays.ClearValue()
            End If

            If item.GetDataKeyValue("COMMIT_DAYS_REMAIN") IsNot Nothing Then
                rnCommitDaysRemain.Value = CDec(item.GetDataKeyValue("COMMIT_DAYS_REMAIN"))
            Else
                rnCommitDaysRemain.ClearValue()
            End If

            If item.GetDataKeyValue("COST_OF_STUDENT") IsNot Nothing AndAlso item.GetDataKeyValue("COMMIT_DAYS_REMAIN") IsNot Nothing AndAlso CDec(item.GetDataKeyValue("CONVERED_TIME")) > 0 Then
                rnReimburseCost.Value = CDec(item.GetDataKeyValue("COST_OF_STUDENT")) * (CDec(item.GetDataKeyValue("COMMIT_DAYS_REMAIN")) / CDec(item.GetDataKeyValue("CONVERED_TIME")))
            Else
                rnReimburseCost.ClearValue()
            End If

            If item.GetDataKeyValue("REMARK") IsNot Nothing Then
                txtNote.Text = item.GetDataKeyValue("REMARK").ToString
            Else
                txtNote.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnFindReimbursement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindReimbursement.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindReimbursementPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindReimbursementPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindReimbursementPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim reps As New TrainingStoreProcedure
        Try
            lstCommonEmployee = CType(ctrlFindReimbursementPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            Dim itm = lstCommonEmployee(0)
            hidEmployeeID.Value = itm.EMPLOYEE_ID
            txtCode.Text = itm.EMPLOYEE_CODE
            txtName.Text = itm.FULLNAME_VN
            Dim terDate = reps.GET_TER_DATE(hidEmployeeID.Value)
            If terDate.ToString <> "01/01/0001 12:00:00 SA" Then
                rdTerDate.SelectedDate = reps.GET_TER_DATE(hidEmployeeID.Value)
            End If
            If cboSearchCourse.SelectedValue <> "" Then
                rdEndDate.SelectedDate = reps.GET_COMMIT_END_DATE(hidEmployeeID.Value, cboSearchCourse.SelectedValue)
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New TrainingRepository
        Dim _validate As New ReimbursementDTO
        Try
            If cboCourse.SelectedValue <> "" Then
                _validate.TR_PROGRAM_ID = cboCourse.SelectedValue
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.ID = rgMain.SelectedValue
                    _validate.EMPLOYEE_ID = hidEmployeeID.Value
                    args.IsValid = rep.ValidateReimbursement(_validate)
                Else
                    _validate.EMPLOYEE_ID = hidEmployeeID.Value
                    args.IsValid = rep.ValidateReimbursement(_validate)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
    Private Sub cboCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        Try
            If cboCourse.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    Dim obj = rep.GetProgramById(cboCourse.SelectedValue)
                    rntxtCostOfStudent.Value = obj.COST_STUDENT
                End Using
            Else
                rntxtCostOfStudent.Value = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboSearchCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboSearchCourse.SelectedIndexChanged
        Dim reps As New TrainingStoreProcedure
        Try
            If hidEmployeeID.Value <> "" Then
                rdEndDate.SelectedDate = reps.GET_COMMIT_END_DATE(hidEmployeeID.Value, cboSearchCourse.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rntxtSeachYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtSeachYear.TextChanged
        Try
            GetPlanInYearOrg2()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub GetPlanInYearOrg2()
        Try
            If rntxtSeachYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramByYear(True, rntxtSeachYear.Value)
                    FillRadCombobox(cboSearchCourse, dtData, "NAME", "ID")
                End Using
            Else
                cboSearchCourse.ClearSelection()
                cboSearchCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"
#End Region
End Class