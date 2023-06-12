Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramResult
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            UpdateControlState()
            Refresh()
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

            Dim dtData
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Using rep As New RecruitmentRepository
                dtData = rep.GetProgramExamsList(hidProgramID.Value, False)
                FillRadCombobox(cboExams, dtData, "NAME", "ID", True)

            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                txtOrgName.Text = objPro.ORG_NAME
                txtTitleName.Text = objPro.TITLE_NAME
                rdSendDate.SelectedDate = objPro.SEND_DATE
                txtCode.Text = objPro.CODE
                txtJobName.Text = objPro.JOB_NAME
                rntxtRequestNumber.Value = objPro.REQUEST_NUMBER
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT
                EnableRadCombo(cboExams, False)
            Case CommonMessage.STATE_NORMAL
                EnableRadCombo(cboExams, True)
        End Select
        ChangeToolbarState()
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If cboExams.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn môn thi"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        If item.GetDataKeyValue("ID") IsNot Nothing Then
                            isSchedule = True
                            item.Edit = True
                        End If
                    Next
                    If Not isSchedule Then
                        ShowMessage(Translate("Ứng viên chưa được lên lịch thi"), NotifyType.Warning)
                        Exit Sub
                    End If
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of ProgramScheduleCanDTO)
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.Edit = True Then
                            Dim obj As New ProgramScheduleCanDTO
                            obj.ID = item.GetDataKeyValue("ID")
                            Dim edit = CType(item, GridEditableItem)
                            Dim rntxtPointResult As RadNumericTextBox = CType(edit("POINT_RESULT").Controls(0), RadNumericTextBox)
                            Dim txtComment As TextBox = CType(edit("COMMENT_INFO").Controls(0), TextBox)
                            Dim txtAssessment As TextBox = CType(edit("ASSESSMENT_INFO").Controls(0), TextBox)
                            Dim cbIsPass As CheckBox = CType(edit("IS_PASS").Controls(0), CheckBox)
                            obj.POINT_RESULT = rntxtPointResult.Value
                            obj.EXAMS_POINT_LADDER = rntxtPointLadder.Value
                            obj.EXAMS_POINT_PASS = rntxtPointPass.Value
                            obj.IS_PASS = cbIsPass.Checked
                            obj.COMMENT_INFO = txtComment.Text
                            obj.ASSESSMENT_INFO = txtAssessment.Text
                            obj.IS_PV = chkIsPV.Checked
                            lst.Add(obj)
                        End If
                    Next

                    If rep.UpdateCandidateResult(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgData.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim txt As TextBox
            Dim rntxt As RadNumericTextBox
            Dim cb As CheckBox
            If Not chkIsPV.Checked Then
                rntxt = CType(edit("POINT_RESULT").Controls(0), RadNumericTextBox)
                rntxt.Width = Unit.Percentage(100)
                rntxt.MinValue = 0
                If rntxtPointLadder.Value IsNot Nothing Then
                    rntxt.MaxValue = rntxtPointLadder.Value
                End If
                rntxt.ReadOnly = (edit.GetDataKeyValue("ID") Is Nothing)
            Else
                cb = CType(edit("IS_PASS").Controls(0), CheckBox)
                cb.Enabled = (edit.GetDataKeyValue("ID") IsNot Nothing)
            End If
            txt = CType(edit("COMMENT_INFO").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt.MaxLength = 1023
            txt.ReadOnly = (edit.GetDataKeyValue("ID") Is Nothing)
            txt = CType(edit("ASSESSMENT_INFO").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt.MaxLength = 1023
            txt.ReadOnly = (edit.GetDataKeyValue("ID") Is Nothing)

        End If
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterStudent()
    End Sub

    Private Sub btnThiDat_Click(sender As Object, e As System.EventArgs) Handles btnThiDat.Click
        For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
            Dim status_id As Decimal = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next

        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Thi đạt cho các ứng viên?")
        ctrlMessageBox.ActionName = "THIDAT"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnKhongThiDat_Click(sender As Object, e As System.EventArgs) Handles btnKhongThiDat.Click

        For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
            Dim status_id As Decimal = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next

        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Thi không đạt cho các ứng viên?")
        ctrlMessageBox.ActionName = "THIKHONGDAT"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then

                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
                Next
                Using rep As New RecruitmentRepository
                    'Xóa nhân viên.
                    Select Case e.ActionName
                        Case "THIDAT"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "THIKHONGDAT"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGTHIDAT_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                    End Select
                    rgData.Rebind()
                End Using


            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboExams_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboExams.SelectedIndexChanged
        Try
            chkIsPV.Checked = False
            If cboExams.SelectedValue <> "" Then
                Dim rep As New RecruitmentRepository
                Dim obj = rep.GetProgramExamsByID(New ProgramExamsDTO With {.ID = cboExams.SelectedValue})
                rntxtPointLadder.Value = obj.POINT_LADDER
                rntxtPointPass.Value = obj.POINT_PASS
                If obj.IS_PV IsNot Nothing Then
                    chkIsPV.Checked = obj.IS_PV
                End If
            End If
            CType(rgData.Columns.FindByDataField("POINT_RESULT"), GridNumericColumn).ReadOnly = chkIsPV.Checked
            CType(rgData.Columns.FindByDataField("IS_PASS"), GridCheckBoxColumn).ReadOnly = Not chkIsPV.Checked
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Dim rep As New RecruitmentRepository
            Dim _filter As New ProgramScheduleCanDTO
            If cboExams.SelectedValue <> "" Then
                _filter.RC_PROGRAM_EXAMS_ID = Decimal.Parse(cboExams.SelectedValue)
            End If
            _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            rgData.DataSource = rep.GetCandidateResult(_filter)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region


End Class