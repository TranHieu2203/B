Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Public Class ctrlTR_ProgramResult
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Property Upload4Emp As Decimal
        Get
            Return ViewState(Me.ID & "_Upload4Emp")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Upload4Emp") = value
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

    Property Active_Send As Boolean
        Get
            Return ViewState(Me.ID & "_Active_Send")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Active_Send") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()

            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
            '                           ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Print, ToolbarItem.Next)
            'CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(0), RadToolBarButton).Text = "Cập nhật kết quả"
            'CType(MainToolBar.Items(3), RadToolBarButton).Text = "Import kết quả"
            'CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất Template"
            'CType(MainToolBar.Items(5), RadToolBarButton).Text = "In kết quả"
            'CType(MainToolBar.Items(6), RadToolBarButton).Text = "In giấy cam kết nộp chứng chỉ Toiec"
            'CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Export, ToolbarItem.Import, ToolbarItem.Edit)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Cập nhật kết quả"
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Nhập file mẫu"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Cấp chứng nhận"
            'CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL

                Dim objProgram As ProgramDTO
                Using rep As New TrainingRepository
                    objProgram = rep.GetProgramById(hidProgramID.Value)
                End Using

                If objProgram IsNot Nothing Then
                    txtProgramName.Text = objProgram.NAME
                    txtHinhThuc.Text = objProgram.TRAIN_FORM_NAME
                    rdFromDate.SelectedDate = objProgram.START_DATE
                    rdToDate.SelectedDate = objProgram.END_DATE
                    txtCourseName.Text = objProgram.TR_COURSE_NAME
                    txtCourseCode.Text = objProgram.TR_PROGRAM_CODE
                    txtCourseType.Text = objProgram.TR_TYPE_NAME
                    txtCERTIFICATE_NAME.Text = objProgram.CERTIFICATE_NAME

                    txtProgramName.ReadOnly = True
                    txtHinhThuc.ReadOnly = True
                    rdFromDate.Enabled = False
                    rdToDate.Enabled = False
                    txtCourseName.ReadOnly = True
                    txtCourseCode.ReadOnly = True
                    txtCourseType.ReadOnly = True
                    txtCERTIFICATE_NAME.ReadOnly = True

                    Dim item As RadToolBarButton
                    item = CType(_toolbar.Items(5), RadToolBarButton)
                    If objProgram.CERTIFICATE <> -1 Then
                        item.CommandName = "SEND_TRAINING_TO_PROFILE_DISABLE"
                    Else
                        item.CommandName = "SEND_TRAINING_TO_PROFILE_ENABLE"
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT

            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New TrainingRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        'If item.GetDataKeyValue("ID") IsNot Nothing Then
                        '    isSchedule = True
                        '    item.Edit = True
                        'End If
                        If item.Selected Then
                            isSchedule = True
                            item.Edit = True
                        End If
                    Next
                    If Not isSchedule Then
                        'ShowMessage(Translate("Ứng viên chưa được lên lịch thi"), NotifyType.Warning)
                        ShowMessage(Translate("Chưa chọn nhân viên để cập nhật kết quả"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of ProgramResultDTO)
                    For Each item As GridDataItem In rgData.EditItems
                        If item.Edit = True Then

                            Dim edit = CType(item, GridEditableItem)
                            'Dim rntxtDuration As RadNumericTextBox = CType(edit("DURATION").Controls(0), RadNumericTextBox)
                            'Dim cbIsExam As CheckBox = CType(edit("IS_EXAMS").Controls(0), CheckBox)
                            ' Dim cbIsEnd As CheckBox = CType(edit("IS_END").Controls(0), CheckBox)
                            Dim cbIsReach As CheckBox = CType(edit("IS_REACH").Controls(0), CheckBox)
                            'Dim rntxtTOIEC_BENCHMARK As RadNumericTextBox = CType(edit("TOIEC_BENCHMARK").Controls(0), RadNumericTextBox)
                            'Dim rntxtTOIEC_SCORE_IN As RadNumericTextBox = CType(edit("TOIEC_SCORE_IN").Controls(0), RadNumericTextBox)
                            'Dim rntxtTOIEC_SCORE_OUT As RadNumericTextBox = CType(edit("TOIEC_SCORE_OUT").Controls(0), RadNumericTextBox)
                            'Dim rntxtINCREMENT_SCORE As RadNumericTextBox = CType(edit("INCREMENT_SCORE").Controls(0), RadNumericTextBox)
                            Dim cboRank As RadComboBox = CType(edit.FindControl("cboRank"), RadComboBox)
                            'Dim rntxtRETEST_SCORE As RadNumericTextBox = CType(edit("RETEST_SCORE").Controls(0), RadNumericTextBox)
                            Dim cboRank2 As RadComboBox = CType(edit.FindControl("cboRank2"), RadComboBox)
                            'Dim txtRETEST_REMARK As TextBox = CType(edit("RETEST_REMARK").Controls(0), TextBox)
                            Dim rntxtFINAL_SCORE As RadNumericTextBox = CType(edit("FINAL_SCORE").Controls(0), RadNumericTextBox)

                            'Dim cbABSENT_REASON As CheckBox = CType(edit("ABSENT_REASON").Controls(0), CheckBox)
                            'Dim cbABSENT_UNREASON As CheckBox = CType(edit("ABSENT_UNREASON").Controls(0), CheckBox)
                            'Dim cbIsCer As CheckBox = CType(edit("IS_CERTIFICATE").Controls(0), CheckBox)
                            'Dim rdCerDate As RadDatePicker = CType(edit("CERTIFICATE_DATE").Controls(0), RadDatePicker)
                            'Dim txtCerNo As TextBox = CType(edit("CERTIFICATE_NO").Controls(0), TextBox)
                            'Dim rdCER_RECEIVE_DATE As RadDatePicker = CType(edit("CER_RECEIVE_DATE").Controls(0), RadDatePicker)
                            'Dim rdCOMMIT_STARTDATE As RadDatePicker = CType(edit("COMMIT_STARTDATE").Controls(0), RadDatePicker)
                            'Dim rdCOMMIT_ENDDATE As RadDatePicker = CType(edit("COMMIT_ENDDATE").Controls(0), RadDatePicker)
                            'Dim rntxtCOMMIT_WORKMONTH As RadNumericTextBox = CType(edit("COMMIT_WORKMONTH").Controls(0), RadNumericTextBox)
                            'Dim cbIS_REFUND_FEE As CheckBox = CType(edit("IS_REFUND_FEE").Controls(0), CheckBox)
                            'Dim cbIS_RESERVES As CheckBox = CType(edit("IS_RESERVES").Controls(0), CheckBox)
                            Dim txtComment1 As TextBox = CType(edit("COMMENT_1").Controls(0), TextBox)
                            Dim txtComment2 As TextBox = CType(edit("COMMENT_2").Controls(0), TextBox)
                            Dim txtComment3 As TextBox = CType(edit("COMMENT_3").Controls(0), TextBox)
                            Dim txtREMARK As TextBox = CType(edit("REMARK").Controls(0), TextBox)
                            Dim txtATTACH_FILE As RadTextBox = CType(edit.FindControl("_FileName"), RadTextBox)

                            Dim obj As New ProgramResultDTO
                            With obj
                                .ID = item.GetDataKeyValue("ID")
                                .TR_PROGRAM_ID = hidProgramID.Value
                                .EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                                .IS_REACH = cbIsReach.Checked
                                If cboRank.SelectedValue <> "" Then
                                    .TR_RANK_ID = cboRank.SelectedValue
                                End If
                                'If cboRank2.SelectedValue <> "" Then
                                '    .RETEST_RANK_ID = cboRank2.SelectedValue
                                'End If
                                .FINAL_SCORE = rntxtFINAL_SCORE.Value
                                .REMARK = txtREMARK.Text
                                .COMMENT_1 = txtComment1.Text
                                .COMMENT_2 = txtComment2.Text
                                .COMMENT_3 = txtComment3.Text
                                .ATTACH_FILE = txtATTACH_FILE.Text
                            End With
                            lst.Add(obj)
                        End If
                    Next

                    If rep.UpdateProgramResult(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgData.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim sps As New TrainingStoreProcedure
                    Dim dataSet As New DataSet
                    Dim tempPath = "~/ReportTemplates//Training//Import//ImportResult.xlsx"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    dataSet = sps.EXPORT_PROGRAM_RESULT(Decimal.Parse(hidProgramID.Value))
                    dataSet.Tables(0).TableName = "TABLE1"
                    dataSet.Tables(1).TableName = "TABLE2"
                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "ImportResult" & Format(Date.Now, "yyyyMMddHHmmss"), dataSet, Nothing, Response)
                    End Using

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.isMultiple = False
                    ctrlUpload.Show()
                    'Case "SEND_TRAINING_TO_PROFILE_ENABLE"
                    '    Dim lst As New List(Of ProgramResultDTO)
                    '    For Each item As GridDataItem In rgData.EditItems
                    '        Dim obj As New ProgramResultDTO
                    '        With obj
                    '            .TR_PROGRAM_ID = hidProgramID.Value
                    '            .EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    '        End With
                    '        lst.Add(obj)
                    '    Next
                    '    If Not rep.CheckProgramResult(lst) Then
                    '        ShowMessage(Translate("Vui lòng cập nhật kết quả cho học viên trước khi cấp chứng nhận"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'Case "SEND_TRAINING_TO_PROFILE_ENABLE"
                    '    If rgData.MasterTableView.GetSelectedItems.Count = 0 Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    '    Dim listID As New List(Of Decimal)
                    '    For Each item As GridDataItem In rgData.MasterTableView.GetSelectedItems
                    '        If item.GetDataKeyValue("INSERT_HSNV") = 1 Then
                    '            ShowMessage(Translate("Tồn tại nhân viên đã được cập nhật thông tin sang HSNV"), NotifyType.Warning)
                    '            Exit Sub
                    '        End If
                    '        listID.Add(item.GetDataKeyValue("ID"))
                    '    Next
                    '    If rep.SendTrainingToEmployeeProfile(listID) Then
                    '        rgData.Rebind()
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    Else
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    '    End If

            End Select
            UpdateControlState()
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
                    If rep.IMPORT_PROGRAM_RESULT(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_RESULT_ERROR')", True)
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
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(2).ColumnName = "TR_PROGRAM_ID"
            dtTemp.Columns(3).ColumnName = "EMPLOYEE_NAME"
            dtTemp.Columns(4).ColumnName = "ORG_NAME"
            dtTemp.Columns(5).ColumnName = "TITLE_NAME"
            dtTemp.Columns(6).ColumnName = "IS_REACH"
            dtTemp.Columns(7).ColumnName = "TR_RANK_NAME"
            dtTemp.Columns(8).ColumnName = "FINAL_SCORE"
            dtTemp.Columns(9).ColumnName = "COMMENT_1"
            dtTemp.Columns(10).ColumnName = "COMMENT_2"
            dtTemp.Columns(11).ColumnName = "COMMENT_3"
            dtTemp.Columns(12).ColumnName = "REMARK"
            dtTemp.Columns(13).ColumnName = "TR_RANK_ID"
            dtTemp.Columns(14).ColumnName = "ID"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New TrainingStoreProcedure
            Dim lstEmp As New List(Of String)
            Dim stt As Decimal = 0

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                newRow = dtLogs.NewRow
                newRow("STT") = stt + 1

                If psp.CHECK_EMPLOYEE_CODE_EXIST_IN_PROGRAM_RESULT(Decimal.Parse(hidProgramID.Value), rows("EMPLOYEE_CODE").ToString.Trim) = False Then
                    newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE").ToString.Trim + " - Mã học viên không nằm trong chương trình đào tạo"
                    _error = False
                End If

                If rows("IS_REACH").ToString.Trim <> "" AndAlso Not IsNumeric(rows("IS_REACH").ToString.Trim) Then
                    newRow("IS_REACH") = "Kết quả không hợp lệ"
                    _error = False
                End If

                If rows("FINAL_SCORE").ToString.Trim <> "" AndAlso Not IsNumeric(rows("FINAL_SCORE").ToString.Trim) Then
                    newRow("FINAL_SCORE") = "Điểm số không hợp lệ"
                    _error = False
                End If

                If rows("TR_RANK_ID").ToString.Trim <> "" AndAlso Not IsNumeric(rows("TR_RANK_ID").ToString.Trim) Then
                    newRow("TR_RANK_ID") = "Mã xếp loại không hợp lệ"
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
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterStudent()
    End Sub
    Private Sub CreateDataFilterStudent()
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New ProgramResultDTO
            Dim MaximumRows As Integer
            _filter.TR_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramResultDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramResult(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetProgramResult(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
        '    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
        '    ''datarow("COM_NAME").ToolTip = Utilities.DrawTreeByString(datarow("COM_DESC").Text)
        '    'datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        '    If datarow("ATTACH_FILE").Text <> "" Then
        '        Dim linkCell As GridTableCell = datarow("ATTACH_FILE")
        '        Dim link As HyperLink = CType(linkCell.FindControl("AttachFile"), HyperLink)
        '        link.Text = datarow("ATTACH_FILE").Text
        '        link.NavigateUrl = "/ReportTemplates/Training/Upload/Result/" & datarow("ATTACH_FILE").Text
        '    End If
        'End If
        Dim rep As New TrainingRepository

        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim cbIsReach As CheckBox = CType(edit("IS_REACH").Controls(0), CheckBox)
            Dim cboRank As RadComboBox = CType(edit.FindControl("cboRank"), RadComboBox)
            Dim rntxtFINAL_SCORE As RadNumericTextBox = CType(edit("FINAL_SCORE").Controls(0), RadNumericTextBox)
            rntxtFINAL_SCORE.Width = Unit.Percentage(100)

            Dim txtComment1 As TextBox = CType(edit("COMMENT_1").Controls(0), TextBox)
            txtComment1.Width = Unit.Percentage(100)
            Dim txtComment2 As TextBox = CType(edit("COMMENT_2").Controls(0), TextBox)
            txtComment2.Width = Unit.Percentage(100)
            Dim txtComment3 As TextBox = CType(edit("COMMENT_3").Controls(0), TextBox)
            txtComment3.Width = Unit.Percentage(100)
            Dim txtREMARK As TextBox = CType(edit("REMARK").Controls(0), TextBox)
            txtREMARK.Width = Unit.Percentage(100)

            If cboRank IsNot Nothing Then
                Dim dtData As DataTable
                dtData = rep.GetOtherList("TR_RANK", True)
                FillRadCombobox(cboRank, dtData, "NAME", "ID")
                If edit.GetDataKeyValue("TR_RANK_ID") IsNot Nothing Then
                    cboRank.SelectedValue = edit.GetDataKeyValue("TR_RANK_ID")
                End If
            End If
            Dim _filter As New ProgramResultDTO
            _filter.EMPLOYEE_ID = datarow.GetDataKeyValue("EMPLOYEE_ID")
            _filter.TR_PROGRAM_ID = hidProgramID.Value
            Dim lsResual = rep.GetTRResult(_filter)
            Dim objProgram As ProgramDTO
            objProgram = rep.GetProgramById(hidProgramID.Value)


            If objProgram IsNot Nothing Then
                If objProgram.TR_TYPE_ID = 79661 Then 'Tham gia
                    cbIsReach.Enabled = True
                    cboRank.Enabled = False
                    rntxtFINAL_SCORE.Enabled = False
                    txtComment1.Enabled = False
                    txtComment2.Enabled = False
                    txtComment3.Enabled = False
                ElseIf objProgram.TR_TYPE_ID = 79662 Then 'Thi xếp loại
                    cbIsReach.Enabled = True
                    cboRank.Enabled = True
                    rntxtFINAL_SCORE.Enabled = True
                    txtComment1.Enabled = False
                    txtComment2.Enabled = False
                    txtComment3.Enabled = False
                    If lsResual.Count > 0 Then
                        rntxtFINAL_SCORE.Value = lsResual(0).FINAL_SCORE
                        cboRank.SelectedValue = lsResual(0).TR_RANK_ID
                    End If
                ElseIf objProgram.TR_TYPE_ID = 79663 Then 'Đánh giá
                    cbIsReach.Enabled = False
                    cboRank.Enabled = False
                    rntxtFINAL_SCORE.Enabled = False
                    txtComment1.Enabled = True
                    txtComment2.Enabled = True
                    txtComment3.Enabled = True
                End If
            End If

            'If cbo2 IsNot Nothing Then
            '    Dim dtData As DataTable
            '    Using rep As New TrainingRepository
            '        dtData = rep.GetOtherList("TR_RANK", True)
            '        FillRadCombobox(cbo2, dtData, "NAME", "ID")
            '        If edit.GetDataKeyValue("RETEST_RANK_ID") IsNot Nothing Then
            '            cbo2.SelectedValue = edit.GetDataKeyValue("RETEST_RANK_ID")
            '        End If
            '    End Using
            'End If
        End If
    End Sub
    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        If e.CommandName = "UploadFile" Then
            Upload4Emp = e.CommandArgument
            ctrlUpload1.Show()
        ElseIf e.CommandName = "DownloadFile" Then
            Dim url As String = "Download.aspx?" & e.CommandArgument
            Dim str As String = "window.open('" & url + "');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
        End If
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Try
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload/Result")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)

                    For Each abc As GridDataItem In rgData.MasterTableView.Items
                        If abc.GetDataKeyValue("EMPLOYEE_ID").ToString = Upload4Emp Then
                            Dim txtATTACH_FILE As RadTextBox = DirectCast(abc("ATTACH_FILE").FindControl("_FileName"), RadTextBox)
                            txtATTACH_FILE.Text = file.FileName
                            Exit For
                        End If
                    Next

                    'Dim txtATTACH_FILE As RadTextBox = DirectCast(Upload4Emp("ATTACH_FILE").FindControl("_FileName"), RadTextBox)
                    'txtATTACH_FILE.Text = file.FileName

                    'For Each item As GridDataItem In rgData.Items
                    '    If item("EMPLOYEE_ID").Text = UploadedEMP Then
                    '        Dim edit = CType(item, GridEditableItem)
                    '        Dim txtATTACH_FILE As RadNumericTextBox = CType(edit("ATTACH_FILE").Controls(1), RadNumericTextBox)
                    '        txtATTACH_FILE.Text = file.FileName
                    '    End If
                    'Next
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Custom"
#End Region

End Class