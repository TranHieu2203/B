Imports Common
Imports Common.CommonBusiness
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI


Public Class ctrlRC_ProgramExamsResult_Dialog
    Inherits Common.CommonView
    Private rep As New HistaffFrameworkRepository
    Private store As New RecruitmentStoreProcedure()
    Private userlog As UserLog
#Region "Property"
    Public Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
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
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim rep As New RecruitmentRepository
            hidProgramID.Value = Request.Params("PROGRAM_ID")

            Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
            Dim tabSource As DataTable = store.EXAMS_GETBYCANDIDATE_BY_PCS_ID(Int32.Parse(Request.Params("PSC_ID")), 0)
            Dim objNV = rep.GETCANDIDATEINFO_BY_PSC_ID(Int32.Parse(Request.Params("PSC_ID")))
            lblSendDate.Text = obj.SEND_DATE.Value.ToString("dd/MM/yyyy")
            lblCode.Text = obj.CODE
            lblJobName.Text = obj.JOB_NAME
            lblOrgName.Text = obj.ORG_NAME
            txtTitleName.Text = obj.TITLE_NAME
            lblRequestNumber.Text = obj.REQUEST_NUMBER
            lblNumberHaveRecruit.Text = obj.CANDIDATE_RECEIVED
            lblRecordreceived.Text = obj.CANDIDATE_COUNT
            lblExamName.Text = tabSource.Rows(0)("EXAM_NAME")
            lblProctor.Text = tabSource.Rows(0)("EMPLOYEE_NAME")
            lblPointLadder.Text = tabSource.Rows(0)("POINT_LADDER").ToString
            lblPointPass.Text = tabSource.Rows(0)("POINT_PASS").ToString
            txtMarks.Value = If(IsNumeric(tabSource.Rows(0)("POINT_RESULT")), Decimal.Parse(tabSource.Rows(0)("POINT_RESULT")), 0)
            txtComment.Text = tabSource.Rows(0)("COMMENT_INFO").ToString
            txtAssessment.Text = tabSource.Rows(0)("ASSESSMENT_INFO").ToString

            lblIDNV.Text = objNV.CANDIDATE_CODE
            lblFullName.Text = objNV.FULLNAME_VN
            lblGENDER.Text = objNV.GENDER_NAME
            lblBIRTH_DATE.Text = objNV.BIRTH_DATE
            hidCandidate_ID.Value = objNV.ID
            If IsNumeric(txtMarks.Value) AndAlso IsNumeric(lblPointPass.Text) Then
                If Int32.Parse(txtMarks.Value) >= Int32.Parse(lblPointPass.Text) Then
                    txtResult.Text = "Đậu vòng"
                Else
                    If txtMarks.Value > 0 Then
                        txtResult.Text = "Rớt vòng"
                    End If

                End If
            End If
            CurrentState = CommonMessage.STATE_NORMAL

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As PROGRAM_SCHEDULE_CAN_DTO
        Dim reps As New RecruitmentRepository
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT

                Case CommonMessage.TOOLBARITEM_SAVE
                    'If Page.IsValid Then
                    If reps.CheckExist_Program_Schedule_Can(If(IsNumeric(hidCandidate_ID.Value), hidCandidate_ID.Value, 0), If(IsNumeric(hidProgramID.Value), hidProgramID.Value, 0), Int32.Parse(Request.Params("EXAMS_ORDER"))) Then
                        ShowMessage("Ứng viên đã tồn tại ở vòng khác, không thể chỉnh sửa", NotifyType.Error)
                        Return
                    End If
                    Dim status As String
                    Dim SCHEDULE As Int32
                    obj = New PROGRAM_SCHEDULE_CAN_DTO
                    obj.POINT_RESULT = txtMarks.Value
                    obj.COMMENT_INFO = txtComment.Text
                    obj.ASSESSMENT_INFO = txtAssessment.Text
                    obj.IS_PASS = If(obj.POINT_RESULT >= Int32.Parse(lblPointPass.Text) = True, 1, 0)
                    If obj.IS_PASS = 1 Then
                        status = "DAUVONG" + Int32.Parse(Request.Params("EXAMS_ORDER")).ToString
                        SCHEDULE = Int32.Parse(Request.Params("EXAMS_ORDER"))
                    Else
                        status = "ROTVONG" + Int32.Parse(Request.Params("EXAMS_ORDER")).ToString
                        SCHEDULE = Int32.Parse(Request.Params("EXAMS_ORDER")) - 1
                    End If
                    userlog = New UserLog
                    userlog = LogHelper.GetUserLog
                    'Select Case CurrentState
                    'Case CommonMessage.STATE_EDIT
                    obj.ID = Int32.Parse(Request.Params("PSC_ID"))
                    IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                            obj.ID,
                                                            obj.POINT_RESULT,
                                                            obj.COMMENT_INFO,
                                                            obj.ASSESSMENT_INFO,
                                                            obj.IS_PASS)
                    'End Select
                    If IsSaveCompleted Then
                        store.UPDATE_PROGRAM_CANDIDATE_STATUS_BY_EXAMS_ORDER(Int32.Parse(Request.Params("PRO_CAN_ID")), status, Int32.Parse(Request.Params("EXAMS_ORDER")), Int32.Parse(Request.Params("PROGRAM_ID")), SCHEDULE)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                    'End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtMarks_TextChanged(sender As Object, e As System.EventArgs) Handles txtMarks.TextChanged
        Try
            If IsNumeric(txtMarks.Value) Then
                If Int32.Parse(txtMarks.Value) >= Int32.Parse(lblPointPass.Text) Then
                    txtResult.Text = "Đậu vòng"
                Else
                    txtResult.Text = "Rớt vòng"

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
    Private Sub cmdExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportExcel.Click
        Dim template_URL As String = String.Format("~/ReportTemplates/Recruitment/Report/Danh sach de nghi ky HĐLĐ thu viec.xls")
        Dim fileName As String = String.Format("Danh sách đề nghị ký HĐLĐ thử việc - {0}", lblJobName.Text)
        Dim _error As String = ""
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_DE_NGHI_THU_VIEC", New List(Of Object)(New Object() {hidProgramID.Value}))

        Using xls As New AsposeExcelCommon
            xls.ExportExcelTemplateReport(Server.MapPath(template_URL), Server.MapPath(template_URL), fileName, ds, Response)

        End Using
    End Sub

End Class