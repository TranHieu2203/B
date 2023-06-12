Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_AssessmentResult
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Public Property SelectedItemCriGroupFormNotForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = value
        End Set
    End Property

    Public Property SelectedItemCriGroupForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupForm") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
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
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.PageSize = Common.Common.DefaultPageSize
            rgData.AllowCustomPaging = True
            rgData.ClientSettings.EnablePostBackOnRowClick = True
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

            rntxtYear.Value = Date.Now.Year
            GetChooseFormProgramByYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.Lock, ToolbarItem.Unlock)
            MainToolBar.Items(0).Text = Translate("Cập nhật đánh giá")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                EnabledGrid(rgData, False, True)
                rntxtYear.ReadOnly = True
                EnableRadCombo(cboCourse, False)
                EnableControlAll(True, txtNote1, txtNote3, txtNote2, txtNote4)

            Case CommonMessage.STATE_NORMAL
                EnabledGrid(rgData, True, True)
                rntxtYear.ReadOnly = False
                EnableRadCombo(cboCourse, True)
                EnableControlAll(False, txtNote1, txtNote3, txtNote2, txtNote4)
            Case CommonMessage.STATE_EDIT
                EnabledGrid(rgData, False, True)
                rntxtYear.ReadOnly = True
                EnableRadCombo(cboCourse, False)
                EnableControlAll(True, txtNote1, txtNote3, txtNote2, txtNote4)
        End Select
        ChangeToolbarState()
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New TrainingRepository
        Dim store As New TrainingStoreProcedure
        Dim _error As Integer = 0
        Dim sType As Object = Nothing
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    If Not cboCourse.SelectedValue <> "" Then
                        ShowMessage(Translate("Bạn chưa chọn chương trình đào tạo"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rgData.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate("Bạn chưa chọn nhân viên cần đánh giá"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_ASSESSMENT_RESULT&EMPLOYEE_ID=" & rgData.SelectedValue.ToString & _
                    '                                    "&TR_CHOOSE_FORM_ID=" & cboCourse.SelectedValue & "');", True)
                    EXPORT_EXCEL()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nhân viên cần đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Không thể cập nhật nhiều nhân viên một lúc"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_LOCK") = -1 Then
                            ShowMessage(Translate("Nhân viên '" & item.GetDataKeyValue("EMPLOYEE_CODE") & "' đã khóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgResult.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgResult.MasterTableView.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgResult.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgResult.MasterTableView.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        Dim objResult As New AssessmentResultDTO
                        objResult.EMPLOYEE_ID = rgData.SelectedValue
                        objResult.TR_PROGRAM_ID = cboCourse.SelectedValue
                        objResult.NOTE1 = txtNote1.Text
                        objResult.NOTE2 = txtNote2.Text
                        objResult.NOTE3 = txtNote3.Text
                        objResult.NOTE4 = txtNote4.Text
                        Dim lst As New List(Of AssessmentResultDtlDTO)
                        For Each item As GridDataItem In rgResult.Items
                            objResult.CRI_COURSE_ID = item.GetDataKeyValue("CRI_COURSE_ID")
                            Dim pointMax = CDec(item.GetDataKeyValue("TR_CRITERIA_POINT_MAX"))
                            If item.Edit = True Then
                                Dim obj As New AssessmentResultDtlDTO
                                Dim edit = CType(item, GridEditableItem)
                                Dim rntxtPointAss As RadNumericTextBox = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)

                                Dim txtRemark As TextBox = CType(edit("REMARK").Controls(0), TextBox)
                                If rntxtPointAss.Value < 0 Or rntxtPointAss.Value > pointMax Then
                                    ShowMessage("Giá trị đánh giá chỉ từ 0 đến" + pointMax.ToString, NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_EDIT
                                    UpdateControlState()
                                    Exit Sub
                                ElseIf rntxtPointAss.Value Is Nothing Then
                                    ShowMessage("Vui lòng nhập đầy đủ điểm đánh giá", NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_EDIT
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                With obj
                                    .EMPLOYEE_ID = rgData.SelectedValue
                                    .POINT_ASS = rntxtPointAss.Value
                                    .REMARK = txtRemark.Text
                                    .TR_CRITERIA_ID = item.GetDataKeyValue("TR_CRITERIA_ID")
                                    .TR_PROGRAM_ID = cboCourse.SelectedValue
                                End With
                                lst.Add(obj)
                            End If
                        Next
                        objResult.lstResult = lst
                        If rep.UpdateAssessmentResult(objResult) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            For Each item As GridDataItem In rgResult.MasterTableView.Items
                                item.Edit = False
                            Next
                            rgResult.Rebind()
                            rgData.MasterTableView.ClearSelectedItems()
                            rgData.Rebind()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_LOCK
                    If rgData.SelectedItems.Count < 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_LOCK") <> 0 Then
                            ShowMessage(Translate("Tồn tại bản ghi ở trạng thái đã khóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If IsNothing(item.GetDataKeyValue("ASSESMENT_ID")) Then
                            ShowMessage(Translate("Nhân viên '" & item.GetDataKeyValue("EMPLOYEE_CODE") & "' chưa cập nhật đánh giá"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_LOCK)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_LOCK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgData.SelectedItems.Count < 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_LOCK") <> -1 Then
                            ShowMessage(Translate("Tồn tại bản ghi ở trạng thái chưa khóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("ASSESMENT_ID") = 0 Then
                            ShowMessage(Translate("Nhân viên '" & item.GetDataKeyValue("EMPLOYEE_CODE") & "' chưa cập nhật đánh giá"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNLOCK)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_UNLOCK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            UpdateControlState()
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterEmployee()
    End Sub

    Private Sub rgResult_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgResult.NeedDataSource
        CreateDataFilterResult()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_LOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstIds As New List(Of Decimal)
                For idx = 0 To rgData.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    lstIds.Add(item.GetDataKeyValue("ASSESMENT_ID"))
                Next
                Using rep As New TrainingRepository
                    If rep.ChangeStatusAssessmentResult(lstIds, -1) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        UpdateControlState()
                        rgData.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End Using
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_UNLOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstIds As New List(Of Decimal)
                For idx = 0 To rgData.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    lstIds.Add(item.GetDataKeyValue("ASSESMENT_ID"))
                Next
                Using rep As New TrainingRepository
                    If rep.ChangeStatusAssessmentResult(lstIds, 0) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        UpdateControlState()
                        rgData.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        Try
            If cboCourse.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramById(cboCourse.SelectedValue)
                    If dtData.Rows.Count > 0 Then
                        txtAddress.Text = If(Not IsDBNull(dtData.Rows(0)("VENUE")), dtData.Rows(0)("VENUE"), "")
                        txtCenters.Text = If(Not IsDBNull(dtData.Rows(0)("LST_CENTER")), dtData.Rows(0)("LST_CENTER"), "")
                        txtLectures.Text = If(Not IsDBNull(dtData.Rows(0)("LST_LECTURE")), dtData.Rows(0)("LST_LECTURE"), "")
                        rdEndDate.SelectedDate = If(dtData.Rows(0)("END_DATE").ToString <> "", dtData.Rows(0)("END_DATE"), Nothing)
                        rdStartDate.SelectedDate = If(dtData.Rows(0)("START_DATE").ToString <> "", dtData.Rows(0)("START_DATE"), Nothing)
                    End If
                End Using
                'Using rep As New TrainingRepository
                '    Dim obj = rep.GetProgramByChooseFormId(cboCourse.SelectedValue)
                '    If obj IsNot Nothing Then
                '        txtAddress.Text = obj.VENUE
                '        txtCenters.Text = obj.Centers_NAME
                '        txtLectures.Text = obj.Lectures_NAME
                '        rdEndDate.SelectedDate = obj.START_DATE
                '        rdStartDate.SelectedDate = obj.END_DATE
                '        'txtDTB.Text = rep.GET_DTB(cboCourse.SelectedValue)
                '    End If
                'End Using
            Else
                txtAddress.Text = ""
                txtCenters.Text = ""
                txtLectures.Text = ""
                txtDTB.Text = ""
                rdEndDate.SelectedDate = Nothing
                rdStartDate.SelectedDate = Nothing
            End If
            rgResult.Rebind()
            rgData.Rebind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetChooseFormProgramByYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgResult_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgResult.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rntxt As RadNumericTextBox
            Dim txt As TextBox
            rntxt = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt.MinValue = 0
            txt = CType(edit("REMARK").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt.MaxLength = 1023
        End If
    End Sub

    Private Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.SelectedIndexChanged
        rgResult.Rebind()
        CalculateDTB()
    End Sub


    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = "Excel"
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)

                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                If workbook.Worksheets.GetSheetByCodeName("ImportResult") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(4, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            Dim dtData As DataTable
            For Each dt As DataTable In dsDataPrepare.Tables
                If dtData Is Nothing Then
                    dtData = dt.Clone
                End If
                For Each row In dt.Rows
                    dtData.ImportRow(row)
                Next
            Next

            ImportData(dtData)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    Public Sub ImportData(ByVal dtData As DataTable)
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dtError As DataTable = dtData.Clone
            dtError.TableName = "DATA"
            dtError.Columns.Add("STT", GetType(String))
            Dim iRow As Integer = 8
            Dim IsError As Boolean = False
            Dim objResult As New AssessmentResultDTO
            Dim lstDtl As New List(Of AssessmentResultDtlDTO)
            For Each row In dtData.Rows
                Dim sError As String = ""
                Dim rowError = dtError.NewRow
                Dim isRow = ImportValidate.TrimRow(row)
                Dim isScpExist As Boolean = False
                If Not isRow Then
                    iRow += 1
                    Continue For
                End If
                Dim obj As New AssessmentResultDtlDTO


                objResult.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                objResult.TR_CHOOSE_FORM_ID = Decimal.Parse(row("TR_CHOOSE_FORM_ID"))
                obj.TR_CHOOSE_FORM_ID = Decimal.Parse(row("TR_CHOOSE_FORM_ID"))
                obj.TR_CRITERIA_GROUP_ID = Decimal.Parse(row("TR_CRITERIA_GROUP_ID"))
                obj.TR_CRITERIA_ID = Decimal.Parse(row("TR_CRITERIA_ID"))
                sError = "Chưa nhập Điểm đánh giá"
                ImportValidate.IsValidNumber("POINT_ASS", row, rowError, IsError, sError)
                If Not IsError Then
                    obj.POINT_ASS = Decimal.Parse(row("POINT_ASS"))
                End If
                obj.REMARK = row("REMARK")
                lstDtl.Add(obj)
                If IsError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE")
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME")
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_ASSESSMENT_RESULT_ERROR')", True)
                ShowMessage(Translate("Giao dịch không thành công, chi tiết lỗi tệp tin đính kèm"), NotifyType.Warning)
            Else
                Dim rep As New TrainingRepository
                objResult.lstResult = lstDtl
                If rep.UpdateAssessmentResult(objResult) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgResult.Rebind()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


#End Region

#Region "Custom"
    Private Sub CreateDataFilterResult()
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New TR_CriteriaDTO
            Dim lst As New List(Of TR_CriteriaDTO)
            If cboCourse.SelectedValue <> "" Then
                _filter.TR_PROGRAM_ID = Decimal.Parse(cboCourse.SelectedValue)
            End If
            If rgData.SelectedItems.Count <> 0 Then
                _filter.EMPLOYEE_ID = rgData.SelectedValues("EMPLOYEE_ID")
            End If
            Dim Sorts As String = rgResult.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                lst = rep.GetAssessmentResultByID(_filter)
                rgResult.DataSource = lst
            Else
                lst = rep.GetAssessmentResultByID(_filter)
                rgResult.DataSource = lst
            End If
            rgData.VirtualItemCount = MaximumRows
            If rgData.SelectedItems.Count <> 0 Then
                'txtDTB.Text = rep.GET_DTB(cboCourse.SelectedValue, rgData.SelectedValues("EMPLOYEE_ID"))
                CalculateDTB()
            End If
            If lst.Count > 0 Then
                txtNote1.Text = lst(0).NOTE1
                txtNote2.Text = lst(0).NOTE2
                txtNote3.Text = lst(0).NOTE3
                txtNote4.Text = lst(0).NOTE4
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub CalculateDTB()
        Dim rep As New TrainingRepository
        Dim empId = 0
        If rgData.SelectedItems.Count > 0 Then
            Dim item As GridDataItem = rgData.SelectedItems(0)
            empId = item.GetDataKeyValue("EMPLOYEE_ID")
        End If
        If rgResult.Items.Count > 0 Then
            Dim item As GridDataItem = rgResult.Items(0)
            Dim dtb As Decimal = rep.GET_DTB_PORTAL(item.GetDataKeyValue("CRI_COURSE_ID"), empId, cboCourse.SelectedValue)
            txtDTB.Text = If(dtb <> -1, dtb, "")
        Else
            txtDTB.Text = ""
        End If
    End Sub

    Private Sub CreateDataFilterEmployee()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New AssessmentResultDTO
            SetValueObjectByRadGrid(rgData, _filter)
            If cboCourse.SelectedValue <> "" Then
                _filter.TR_CHOOSE_FORM_ID = Decimal.Parse(cboCourse.SelectedValue)
            End If
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgData.DataSource = rep.GetEmployeeAssessmentResult(_filter, rgData.CurrentPageIndex, _
                                                            rgData.PageSize, _
                                                            MaximumRows, _
                                                            Sorts)
            Else
                rgData.DataSource = rep.GetEmployeeAssessmentResult(_filter, rgData.CurrentPageIndex, _
                                                            rgData.PageSize, _
                                                            MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub GetChooseFormProgramByYear()
        Try

            If rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrChooseProgramFormByYear(True, rntxtYear.Value)
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

    Private Sub EXPORT_EXCEL()
        Try
            Dim store As New TrainingStoreProcedure
            Dim ds As New DataSet
            Dim Attachment As String = String.Empty
            ds = store.GET_DATA_EXPORT_BY_PROGRAM(CDec(Val(cboCourse.SelectedValue)))
            DesignTemplateByData(ds, Attachment)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub DesignTemplateByData(ByVal dsData As DataSet, ByRef Attachment As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String = "AssessmentResult_" & DateTime.Now.Day & DateTime.Now.Hour &
                                                  DateTime.Now.Minute & DateTime.Now.Millisecond & ".xls"
        Dim pathOut As String = String.Empty
        Try
            Dim sFileTmp As String = String.Empty
            'If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server   
            '    If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & "Training", "AssessmentResult_template.xls")) Then
            '        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
            '        Exit Sub
            '    Else
            '        pathOut = System.IO.Path.Combine(PathTemplateInFolder & "Training", "AssessmentResult_template.xls")
            '        sFileTmp = System.IO.Path.Combine(PathTemplateInFolder & "Training", "AssessmentResult_template.xls")
            '    End If
            'Else
            '    If Not File.Exists(Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/" & "AssessmentResult_template.xlsx")) Then
            '        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
            '        Exit Sub
            '    Else
            '        pathOut = Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/out/")
            '        sFileTmp = Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/" & "AssessmentResult_template.xlsx")
            '    End If
            'End If
            If Not File.Exists(Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/" & "AssessmentResult_template.xlsx")) Then
                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            Else
                pathOut = Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/out/")
                sFileTmp = Server.MapPath("~/ReportTemplates/" & "Training" & "/" & "Import" & "/" & "AssessmentResult_template.xlsx")
            End If

            Dim wBook As New Aspose.Cells.Workbook(sFileTmp)
            'Style header
            Dim colsHiden As New List(Of Integer)

            Dim style As New Aspose.Cells.Style
            style.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
            style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center
            style.Font.Size = 11
            style.Font.Name = "Times New Roman"
            style.Font.IsBold = True
            style.SetBorder(Aspose.Cells.BorderType.BottomBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            style.SetBorder(Aspose.Cells.BorderType.LeftBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            style.SetBorder(Aspose.Cells.BorderType.RightBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            style.SetBorder(Aspose.Cells.BorderType.TopBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'style.ForegroundColor = System.Drawing.Color.Yellow
            style.Pattern = Aspose.Cells.BackgroundType.Solid

            'Style header
            Dim style_header As New Aspose.Cells.Style
            style_header.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
            style_header.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center
            style_header.Font.Size = 14
            style_header.Font.Name = "Times New Roman"
            style_header.Font.IsBold = True
            'style_header.SetBorder(Aspose.Cells.BorderType.BottomBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'style_header.SetBorder(Aspose.Cells.BorderType.LeftBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'style_header.SetBorder(Aspose.Cells.BorderType.RightBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'style_header.SetBorder(Aspose.Cells.BorderType.TopBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'style_header.ForegroundColor = System.Drawing.Color.Gray
            style_header.Pattern = Aspose.Cells.BackgroundType.Solid
            style_header.IsTextWrapped = True

            Dim styleNormal As New Aspose.Cells.Style
            styleNormal.VerticalAlignment = Aspose.Cells.TextAlignmentType.Center
            styleNormal.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center
            styleNormal.Font.Size = 11
            styleNormal.Font.Name = "Times New Roman"
            styleNormal.Font.IsBold = True
            styleNormal.SetBorder(Aspose.Cells.BorderType.BottomBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            styleNormal.SetBorder(Aspose.Cells.BorderType.LeftBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            styleNormal.SetBorder(Aspose.Cells.BorderType.RightBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            'styleNormal.SetBorder(Aspose.Cells.BorderType.TopBorder, Aspose.Cells.CellBorderType.Thin, System.Drawing.Color.Black)
            style.Pattern = Aspose.Cells.BackgroundType.Solid

            Dim tblIdx = "&=[Table1]"

            Dim wSheet As Aspose.Cells.Worksheet = wBook.Worksheets.Item(0)
            Dim cells As Aspose.Cells.Cells = wSheet.Cells
            Dim mergeCells As Aspose.Cells.Cells = wSheet.Cells
            Dim hidCells As Aspose.Cells.Cells = wSheet.Cells
            Dim headerCells As Aspose.Cells.Cells = wSheet.Cells
            Dim templateCells As Aspose.Cells.Cells = wSheet.Cells
            Dim iStartColumn As Integer = 1
            Dim iStartRow As Integer = 5
            headerCells.Merge(0, 0, 1, 6)
            headerCells(0, 0).PutValue("Kết quả đánh giá của nhân viên sau đào tạo")
            headerCells(0, 0).SetStyle(style_header)


            headerCells(1, 0).PutValue("Năm: ")
            headerCells(2, 0).PutValue("Chương trình đào tạo: ")
            headerCells(1, 1).PutValue(rntxtYear.Value)
            headerCells(2, 1).PutValue(cboCourse.Text)
            headerCells.Merge(2, 1, 1, 2)

            wSheet.AutoFitColumn(0)
            wSheet.AutoFitColumn(1)
            wSheet.AutoFitColumn(2)

            cells.InsertRows(3, 1)

            cells(iStartRow, 0).PutValue("Tên nhân viên")
            cells(iStartRow, 0).SetStyle(style)
            wSheet.AutoFitColumn(0)
            templateCells(iStartRow + 1, 0).PutValue(tblIdx + ".FULLNAME_VN")

            cells(iStartRow, 1).PutValue("Mã nhân viên")
            cells(iStartRow, 1).SetStyle(style)
            wSheet.AutoFitColumn(1)
            templateCells(iStartRow + 1, 1).PutValue(tblIdx + ".EMPLOYEE_CODE")



            Dim dt_label As New DataTable
            Dim dt_data As New DataTable
            dt_label = dsData.Tables(0)
            dt_data = dsData.Tables(1)

            Dim count_lst_lable = dt_label.Rows.Count
            Dim start_col = 2 'cot
            Dim start_col_2 = 2
            Dim head_row = 4 'dong 

            For Each item In dt_label.Rows
                ' firstrow=4 firstcol totalrow=1 totalcol 2-5/6-9/10-13

                cells.Merge(head_row, start_col, 1, 4) ' start_col + 3)

                cells(4, start_col).PutValue(item("NAME"))
                cells(4, start_col).SetStyle(styleNormal)
                wSheet.AutoFitColumn(start_col)

                templateCells(6, start_col_2).PutValue(tblIdx + ".TITRONG_" + item("CRITERIA_ID").ToString)
                cells(5, start_col_2).PutValue("Tỉ trọng")
                cells(5, start_col_2).SetStyle(style)
                wSheet.AutoFitColumn(start_col_2)
                start_col_2 += 1

                templateCells(6, start_col_2).PutValue(tblIdx + ".MUCDO_" + item("CRITERIA_ID").ToString)
                cells(5, start_col_2).PutValue("Mức độ hữu ích")
                cells(5, start_col_2).SetStyle(style)
                wSheet.AutoFitColumn(start_col_2)
                start_col_2 += 1

                templateCells(6, start_col_2).PutValue(tblIdx + ".DIEM_" + item("CRITERIA_ID").ToString)
                cells(5, start_col_2).PutValue("Điểm")
                cells(5, start_col_2).SetStyle(style)
                wSheet.AutoFitColumn(start_col_2)
                start_col_2 += 1

                templateCells(6, start_col_2).PutValue(tblIdx + ".YKIEN_" + item("CRITERIA_ID").ToString)
                cells(5, start_col_2).PutValue("Ý kiến chung")
                cells(5, start_col_2).SetStyle(style)
                wSheet.AutoFitColumn(start_col_2)
                start_col_2 += 1

                start_col = start_col + 4
            Next

            'start_col_2 = 2
            'For Each item In dt_label.Rows
            '    templateCells(6, start_col_2).PutValue(tblIdx + ".TITRONG" + item("CRITERIA_ID").ToString)
            '    start_col_2 += 1

            '    templateCells(6, start_col_2).PutValue(tblIdx + ".MUCDO" + item("CRITERIA_ID").ToString)
            '    start_col_2 += 1

            '    templateCells(6, start_col_2).PutValue(tblIdx + ".DIEM" + item("CRITERIA_ID").ToString)
            '    start_col_2 += 1

            '    templateCells(6, start_col_2).PutValue(tblIdx + ".YKIEN" + item("CRITERIA_ID").ToString)
            '    start_col_2 += 1
            'Next


            Dim start_col_dtb = 2 + dt_label.Rows.Count * 4




            ' firstrow=4 firstcol totalrow=1 totalcol 2-5/6-9/10-13
            templateCells(6, start_col_dtb).PutValue(tblIdx + ".DTB")
            cells.Merge(4, start_col_dtb, 2, 1)
            cells(4, start_col_dtb).PutValue("Điểm trung bình")
            cells(4, start_col_dtb).SetStyle(style)
            cells(5, start_col_dtb).SetStyle(style)
            wSheet.AutoFitColumn(start_col_dtb)
            start_col_dtb += 1

            templateCells(6, start_col_dtb).PutValue(tblIdx + ".NOTE1")
            cells.Merge(4, start_col_dtb, 2, 1)
            cells(4, start_col_dtb).PutValue("Điều bạn tâm đắc và ứng dụng kiến thức trong khóa học vào công việc gì?")
            cells(4, start_col_dtb).SetStyle(style)
            cells(5, start_col_dtb).SetStyle(style)
            wSheet.AutoFitColumn(start_col_dtb)
            start_col_dtb += 1

            templateCells(6, start_col_dtb).PutValue(tblIdx + ".NOTE2")
            cells.Merge(4, start_col_dtb, 2, 1)
            cells(4, start_col_dtb).PutValue("Với người dẫn dắt khóa học này, anh/chị có thể chia sẻ hoặc đóng góp để giúp chúng tôi cải thiện hơn")
            cells(4, start_col_dtb).SetStyle(style)
            cells(5, start_col_dtb).SetStyle(style)
            wSheet.AutoFitColumn(start_col_dtb)
            start_col_dtb += 1

            templateCells(6, start_col_dtb).PutValue(tblIdx + ".NOTE3")
            cells.Merge(4, start_col_dtb, 2, 1)
            cells(4, start_col_dtb).PutValue("Với khóa học này, để có trải nghiệm học tuyệt vời hơn nữa, bạn mong muốn")
            cells(4, start_col_dtb).SetStyle(style)
            cells(5, start_col_dtb).SetStyle(style)
            wSheet.AutoFitColumn(start_col_dtb)
            start_col_dtb += 1

            templateCells(6, start_col_dtb).PutValue(tblIdx + ".NOTE4")
            cells.Merge(4, start_col_dtb, 2, 1)
            cells(4, start_col_dtb).PutValue("Ý kiến khác")
            cells(4, start_col_dtb).SetStyle(style)
            cells(5, start_col_dtb).SetStyle(style)
            wSheet.AutoFitColumn(start_col_dtb)



            wBook.CalculateFormula()
            'Dim path = "C:\workspaces\VNM PROJECT\VNM_v1\HistaffWebApp\ReportTemplates\Recruitment\Report\out\"
            wBook.Save(pathOut + fileName, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            Dim designer As Aspose.Cells.WorkbookDesigner
            designer = New Aspose.Cells.WorkbookDesigner
            designer.Open(pathOut + fileName)
            designer.SetDataSource(dsData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            If System.IO.File.Exists(pathOut + fileName) Then System.IO.File.Delete(pathOut + fileName)
            designer.Workbook.Save(HttpContext.Current.Response, fileName & ".xls", Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))
            ' wBook.Save(HttpContext.Current.Response, fileName, Aspose.Cells.ContentDisposition.Attachment, New Aspose.Cells.OoxmlSaveOptions(Aspose.Cells.SaveFormat.Excel97To2003))

            '_myLog.WriteLog(_myLog._info, _classPath, method,
            '                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class