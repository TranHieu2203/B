Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private store As New RecruitmentStoreProcedure()
    Public Overrides Property MustAuthorize As Boolean = True
#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property


#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            GetParams()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
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
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Dim dTable As DataTable
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = IDSelect})
                    txtOrgName.Text = obj.ORG_NAME
                    txtFollowers.Text = obj.FOLLOWERS_EMP_NAME
                    If obj.FOLLOWERS_EMP_ID IsNot Nothing Then
                        hidEmpID.Value = obj.FOLLOWERS_EMP_ID
                    End If
                    'txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    'duy fix ngay 11/07.2016
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If
                    ''''''''
                    'txtProCode.Text = obj.CODE
                    txtRemark.Text = obj.REMARK
                    txtDescription.Text = obj.DESCRIPTION
                    txtRecruitReason.Text = obj.RECRUIT_REASON
                    txtRequestExperience.Text = obj.REQUEST_EXPERIENCE
                    txtChieucao.Text = obj.CHIEUCAO
                    txtCode.Text = obj.CODE

                    If obj.JOB_NAME <> "" Then
                        txtJobName.Text = obj.JOB_NAME
                    Else
                        txtJobName.Text = obj.TITLE_NAME
                    End If
                    txtThiLucPhai.Text = obj.THILUCPHAI
                    txtThiLucTrai.Text = obj.THILUCTRAI
                    rntxtAgeFrom.Value = obj.AGE_FROM
                    rntxtAgeTo.Value = obj.AGE_TO
                    rntxtCanNang.Value = obj.CANNANG
                    rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                    rntxtLanguage1Point.Value = obj.LANGUAGE1_POINT
                    rntxtLanguage2Point.Value = obj.LANGUAGE2_POINT
                    rntxtLanguage3Point.Value = obj.LANGUAGE3_POINT
                    'rntxtRecruitNumber.Value = obj.REQUEST_NUMBER
                    rntxtRequestSalaryFrom.Value = obj.REQUEST_SALARY_FROM
                    rntxtRequestSalaryTo.Value = obj.REQUEST_SALARY_TO
                    If obj.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = obj.STATUS_ID
                    End If
                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                    If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                        cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                    End If
                    If obj.APPEARANCE_ID IsNot Nothing Then
                        cboAppearance.SelectedValue = obj.APPEARANCE_ID
                    End If
                    If obj.COMPUTER_LEVEL_ID IsNot Nothing Then
                        cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL_ID
                    End If
                    If obj.GENDER_ID IsNot Nothing Then
                        cboGender.SelectedValue = obj.GENDER_ID
                    End If
                    If obj.GRADUATION_TYPE_MAIN_ID IsNot Nothing Then
                        cboGraduationTypeMain.SelectedValue = obj.GRADUATION_TYPE_MAIN_ID
                    End If
                    If obj.GRADUATION_TYPE_SUB_ID IsNot Nothing Then
                        cboGraduationTypeSub.SelectedValue = obj.GRADUATION_TYPE_SUB_ID
                    End If
                    If obj.GROUP_WORK_ID IsNot Nothing Then
                        cboGroupWork.SelectedValue = obj.GROUP_WORK_ID
                    End If
                    If obj.HEALTH_STATUS_ID IsNot Nothing Then
                        cboHealthStatus.SelectedValue = obj.HEALTH_STATUS_ID
                    End If
                    If obj.LANGUAGE1_ID IsNot Nothing Then
                        cboLanguage1.SelectedValue = obj.LANGUAGE1_ID
                    End If
                    If obj.LANGUAGE1_LEVEL_ID IsNot Nothing Then
                        cboLanguage1Level.SelectedValue = obj.LANGUAGE1_LEVEL_ID
                    End If
                    If obj.LANGUAGE2_ID IsNot Nothing Then
                        cboLanguage2.SelectedValue = obj.LANGUAGE2_ID
                    End If
                    If obj.LANGUAGE2_LEVEL_ID IsNot Nothing Then
                        cboLanguage2Level.SelectedValue = obj.LANGUAGE2_LEVEL_ID
                    End If
                    If obj.LANGUAGE3_ID IsNot Nothing Then
                        cboLanguage3.SelectedValue = obj.LANGUAGE3_ID
                    End If
                    If obj.LANGUAGE3_LEVEL_ID IsNot Nothing Then
                        cboLanguage3Level.SelectedValue = obj.LANGUAGE3_LEVEL_ID
                    End If
                    If obj.MAJOR_MAIN_ID IsNot Nothing Then
                        cboMajorMain.SelectedValue = obj.MAJOR_MAIN_ID
                    End If
                    If obj.MAJOR_SUB_ID IsNot Nothing Then
                        cboMajorSub.SelectedValue = obj.MAJOR_SUB_ID
                    End If
                    If obj.PRIORITY_LEVEL_ID IsNot Nothing Then
                        cboPriorityLevel.SelectedValue = obj.PRIORITY_LEVEL_ID
                    End If
                    'cboTimeWork.SelectedValue = obj.
                    If obj.TRAINING_LEVEL_MAIN_ID IsNot Nothing Then
                        cboTrainingLevelMain.SelectedValue = obj.TRAINING_LEVEL_MAIN_ID
                    End If
                    If obj.TRAINING_LEVEL_SUB_ID IsNot Nothing Then
                        cboTrainingLevelSub.SelectedValue = obj.TRAINING_LEVEL_SUB_ID
                    End If
                    If obj.TRAINING_SCHOOL_MAIN_ID IsNot Nothing Then
                        cboTrainingSchoolMain.SelectedValue = obj.TRAINING_SCHOOL_MAIN_ID
                    End If
                    If obj.TRAINING_SCHOOL_SUB_ID IsNot Nothing Then
                        cboTrainingSchoolSub.SelectedValue = obj.TRAINING_SCHOOL_SUB_ID
                    End If
                    If obj.WORK_TIME_ID IsNot Nothing Then
                        cboTimeWork.SelectedValue = obj.WORK_TIME_ID
                    End If

                    rdSendDate.SelectedDate = obj.SEND_DATE
                    rdReceiveEnd.SelectedDate = obj.RECEIVE_END
                    rdRecruitStart.SelectedDate = obj.RECRUIT_START
                    lblExpectedToWorkDay.Text = obj.EXPECTED_JOIN_DATE.Value.ToString("dd/MM/yyyy")
                    If obj.NUMBERRECRUITMENT = Nothing Then
                        lblRecruitNumber.Text = 0
                    Else
                        lblRecruitNumber.Text = obj.NUMBERRECRUITMENT
                    End If
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    ' bind data to cboRecruitment
                    dTable = store.STAGE_GetData_Combobox(obj.ORG_ID)
                    FillRadCombobox(cboRecruitment, dTable, "TITLE", "ID")
                    If obj.STAGE_ID IsNot Nothing Then
                        cboRecruitment.SelectedValue = obj.STAGE_ID
                    End If
                    If obj.SPECIALSKILLS IsNot Nothing Then
                        cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                    End If
                    If obj.IS_IN_PLAN Is Nothing Then
                        chkIsInPlan.Checked = False
                    Else
                        chkIsInPlan.Checked = obj.IS_IN_PLAN
                    End If
                    chkIsPortal.Checked = False
                    If obj.IS_PORTAL IsNot Nothing Then
                        chkIsPortal.Checked = obj.IS_PORTAL
                    End If
                    If obj.NEGOTIABLESALARY IsNot Nothing Then
                        chkIsPortal.Checked = obj.NEGOTIABLESALARY
                    End If
                    txtTitle.Text = obj.TITLE_NAME
                    If obj.REQUESTOTHER IsNot Nothing And obj.REQUESTOTHER <> "" Then
                        txtRequestOther.Text = obj.REQUESTOTHER
                    Else
                        txtRequestOther.Text = obj.REQUESTOTHER_DEFAULT
                    End If


                    hidTitleID.Value = obj.TITLE_ID

                    For Each itm In obj.lstEmp
                        Dim item As New RadListBoxItem
                        item.Value = itm.EMPLOYEE_ID
                        item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        lstEmployee.Items.Add(item)
                    Next
                    lstCharacter.ClearChecked()
                    For Each itm In obj.lstCharac
                        Dim item = lstCharacter.FindItemByValue(itm.CHARACTER_ID)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    lstScope.ClearChecked()
                    For Each itm In obj.lstScope
                        Dim item = lstScope.FindItemByValue(itm.RECRUIT_SCOPE_ID)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    lstSoftSkill.ClearChecked()
                    For Each itm In obj.lstSoft
                        Dim item = lstSoftSkill.FindItemByValue(itm.SOFT_SKILL_ID)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next

                    If obj.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False
                    End If
                    chkIsInPlan.Enabled = False
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    chkIsInPlan.Checked = True
                    rdSendDate.AutoPostBack = True

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New ProgramDTO
                        'obj.CODE = txtProCode.Text
                        obj.REMARK = txtRemark.Text
                        obj.DESCRIPTION = txtDescription.Text
                        obj.RECRUIT_REASON = txtRecruitReason.Text
                        obj.REQUEST_EXPERIENCE = txtRequestExperience.Text
                        obj.CHIEUCAO = txtChieucao.Text
                        obj.CODE = txtCode.Text
                        obj.JOB_NAME = txtJobName.Text
                        obj.THILUCPHAI = txtThiLucPhai.Text
                        obj.THILUCTRAI = txtThiLucTrai.Text

                        obj.AGE_FROM = rntxtAgeFrom.Value
                        obj.AGE_TO = rntxtAgeTo.Value
                        obj.CANNANG = rntxtCanNang.Value
                        obj.EXPERIENCE_NUMBER = rntxtExperienceNumber.Value
                        obj.LANGUAGE1_POINT = rntxtLanguage1Point.Value
                        obj.LANGUAGE2_POINT = rntxtLanguage2Point.Value
                        obj.LANGUAGE3_POINT = rntxtLanguage3Point.Value
                        'obj.REQUEST_NUMBER = rntxtRecruitNumber.Value
                        obj.REQUEST_SALARY_FROM = rntxtRequestSalaryFrom.Value
                        If cboStatus.SelectedValue <> "" Then
                            obj.STATUS_ID = cboStatus.SelectedValue
                        End If
                        If cboRecruitReason.SelectedValue <> "" Then
                            obj.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If
                        If cboLearningLevel.SelectedValue <> "" Then
                            obj.LEARNING_LEVEL_ID = cboLearningLevel.SelectedValue
                        End If
                        If cboAppearance.SelectedValue <> "" Then
                            obj.APPEARANCE_ID = cboAppearance.SelectedValue
                        End If
                        If cboComputerLevel.SelectedValue <> "" Then
                            obj.COMPUTER_LEVEL_ID = cboComputerLevel.SelectedValue
                        End If
                        If cboGender.SelectedValue <> "" Then
                            obj.GENDER_ID = cboGender.SelectedValue
                        End If
                        If cboGraduationTypeMain.SelectedValue <> "" Then
                            obj.GRADUATION_TYPE_MAIN_ID = cboGraduationTypeMain.SelectedValue
                        End If
                        If cboGraduationTypeSub.SelectedValue <> "" Then
                            obj.GRADUATION_TYPE_SUB_ID = cboGraduationTypeSub.SelectedValue
                        End If
                        If cboGroupWork.SelectedValue <> "" Then
                            obj.GROUP_WORK_ID = cboGroupWork.SelectedValue
                        End If
                        If cboHealthStatus.SelectedValue <> "" Then
                            obj.HEALTH_STATUS_ID = cboHealthStatus.SelectedValue
                        End If
                        If cboLanguage1.SelectedValue <> "" Then
                            obj.LANGUAGE1_ID = cboLanguage1.SelectedValue
                        End If
                        If cboLanguage1Level.SelectedValue <> "" Then
                            obj.LANGUAGE1_LEVEL_ID = cboLanguage1Level.SelectedValue
                        End If
                        If cboLanguage2.SelectedValue <> "" Then
                            obj.LANGUAGE2_ID = cboLanguage2.SelectedValue
                        End If
                        If cboLanguage2Level.SelectedValue <> "" Then
                            obj.LANGUAGE2_LEVEL_ID = cboLanguage2Level.SelectedValue
                        End If
                        If cboLanguage3.SelectedValue <> "" Then
                            obj.LANGUAGE3_ID = cboLanguage3.SelectedValue
                        End If
                        If cboLanguage3Level.SelectedValue <> "" Then
                            obj.LANGUAGE3_LEVEL_ID = cboLanguage3Level.SelectedValue
                        End If
                        If cboMajorMain.SelectedValue <> "" Then
                            obj.MAJOR_MAIN_ID = cboMajorMain.SelectedValue
                        End If
                        If cboMajorSub.SelectedValue <> "" Then
                            obj.MAJOR_SUB_ID = cboMajorSub.SelectedValue
                        End If
                        If cboPriorityLevel.SelectedValue <> "" Then
                            obj.PRIORITY_LEVEL_ID = cboPriorityLevel.SelectedValue
                        End If
                        'cboTimeWork.SelectedValue = obj.
                        If cboTrainingLevelMain.SelectedValue <> "" Then
                            obj.TRAINING_LEVEL_MAIN_ID = cboTrainingLevelMain.SelectedValue
                        End If
                        If cboTrainingLevelSub.SelectedValue <> "" Then
                            obj.TRAINING_LEVEL_SUB_ID = cboTrainingLevelSub.SelectedValue
                        End If
                        If cboTrainingSchoolMain.SelectedValue <> "" Then
                            obj.TRAINING_SCHOOL_MAIN_ID = cboTrainingSchoolMain.SelectedValue
                        End If
                        If cboTrainingSchoolSub.SelectedValue <> "" Then
                            obj.TRAINING_SCHOOL_SUB_ID = cboTrainingSchoolSub.SelectedValue
                        End If
                        If cboTimeWork.SelectedValue <> "" Then
                            obj.WORK_TIME_ID = cboTimeWork.SelectedValue
                        End If
                        obj.SEND_DATE = rdSendDate.SelectedDate
                        obj.RECEIVE_END = rdReceiveEnd.SelectedDate
                        obj.RECRUIT_START = rdRecruitStart.SelectedDate
                        obj.ID = hidID.Value

                        obj.IS_IN_PLAN = chkIsInPlan.Checked
                        obj.IS_PORTAL = chkIsPortal.Checked
                        If hidEmpID.Value <> "" Then
                            obj.FOLLOWERS_EMP_ID = hidEmpID.Value
                        End If
                        If cboRecruitment.SelectedValue <> "" And Not cboRecruitment.SelectedValue Is Nothing Then
                            obj.STAGE_ID = cboRecruitment.SelectedValue
                        End If
                        If cboSpecialSkills.SelectedValue <> "" And Not cboSpecialSkills.SelectedValue Is Nothing Then
                            obj.SPECIALSKILLS = cboSpecialSkills.SelectedValue
                        End If
                        obj.NEGOTIABLESALARY = chkNegotiableSalary.Checked
                        obj.REQUESTOTHER = txtRequestOther.Text
                        obj.REQUEST_SALARY_TO = rntxtRequestSalaryTo.Value

                        Dim lstEmp As New List(Of ProgramEmpDTO)
                        For Each item As RadListBoxItem In lstEmployee.Items
                            Dim emp As New ProgramEmpDTO
                            emp.EMPLOYEE_ID = item.Value
                            lstEmp.Add(emp)
                        Next
                        Dim lstCharac As New List(Of ProgramCharacterDTO)
                        For Each item In lstCharacter.CheckedItems
                            Dim emp As New ProgramCharacterDTO
                            emp.CHARACTER_ID = item.Value
                            lstCharac.Add(emp)
                        Next
                        Dim lstScp As New List(Of ProgramScopeDTO)
                        For Each item In lstScope.CheckedItems
                            Dim emp As New ProgramScopeDTO
                            emp.RECRUIT_SCOPE_ID = item.Value
                            lstScp.Add(emp)
                        Next
                        Dim lstSoft As New List(Of ProgramSoftSkillDTO)
                        For Each item In lstSoftSkill.CheckedItems
                            Dim emp As New ProgramSoftSkillDTO
                            emp.SOFT_SKILL_ID = item.Value
                            lstSoft.Add(emp)
                        Next

                        obj.lstEmp = lstEmp
                        obj.lstCharac = lstCharac
                        obj.lstScope = lstScp
                        obj.lstSoft = lstSoft
                        If rep.ModifyProgram(obj, gID) Then
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Program&group=Business")

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Program&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindEmp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmp.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If isLoadPopup = 1 Then
                lstEmployee.Items.Clear()
                For Each itm In lstCommonEmployee
                    Dim item As New RadListBoxItem
                    item.Value = itm.ID
                    item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                    lstEmployee.Items.Add(item)
                Next
            End If
            If isLoadPopup = 2 Then
                For Each itm In lstCommonEmployee
                    hidEmpID.Value = itm.ID
                    txtFollowers.Text = itm.FULLNAME_VN
                Next
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_PROGRAM_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
                FillRadCombobox(cboRecruitReason, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboLearningLevel, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_GROUP_WORK", True)
                FillRadCombobox(cboGroupWork, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_PRIORITY_LEVEL", True)
                FillRadCombobox(cboPriorityLevel, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboLearningLevel, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_TRAINING_LEVEL", True)
                FillRadCombobox(cboTrainingLevelMain, dtData, "NAME", "ID")
                FillRadCombobox(cboTrainingLevelSub, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_TRAINING_SCHOOL", True)
                FillRadCombobox(cboTrainingSchoolMain, dtData, "NAME", "ID")
                FillRadCombobox(cboTrainingSchoolSub, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_MAJOR", True)
                FillRadCombobox(cboMajorMain, dtData, "NAME", "ID")
                FillRadCombobox(cboMajorSub, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_GRADUATION_TYPE", True)
                FillRadCombobox(cboGraduationTypeMain, dtData, "NAME", "ID")
                FillRadCombobox(cboGraduationTypeSub, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_LANGUAGE", True)
                FillRadCombobox(cboLanguage1, dtData, "NAME", "ID")
                FillRadCombobox(cboLanguage2, dtData, "NAME", "ID")
                FillRadCombobox(cboLanguage3, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguage1Level, dtData, "NAME", "ID")
                FillRadCombobox(cboLanguage2Level, dtData, "NAME", "ID")
                FillRadCombobox(cboLanguage3Level, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboComputerLevel, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("GENDER", True)
                FillRadCombobox(cboGender, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_APPEARANCE", True)
                FillRadCombobox(cboAppearance, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_HEALTH_STATUS", True)
                FillRadCombobox(cboHealthStatus, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("WORK_TIME", True)
                FillRadCombobox(cboTimeWork, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("RC_RECRUIT_SCOPE", False)
                FillCheckBoxList(lstScope, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_SOFT_SKILL", False)
                FillCheckBoxList(lstSoftSkill, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_CHARACTER", False)
                FillCheckBoxList(lstCharacter, dtData, "NAME", "ID")
                'Kỹ năng đặc biệt
                dtData = rep.GetOtherList("SPECIALSKILLS", True)
                FillRadCombobox(cboSpecialSkills, dtData, "NAME", "ID", True)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class