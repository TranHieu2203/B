Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_PlanRegNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Private rep As New HistaffFrameworkRepository
    'Private proRespository As New ProfileRepository
    Public Overrides Property MustAuthorize As Boolean = True
    Private store As New RecruitmentStoreProcedure()
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
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
            GetTotalEmployeeByTitleID()

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
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetPlanRegByID(New PlanRegDTO With {.ID = IDSelect})
                    txtOrgName.Text = obj.ORG_NAME
                    'txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)

                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then   'duy fix ngay 1107
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If

                    txtRemark.Text = obj.REMARK
                    rntxtRecruitNumber.Value = obj.RECRUIT_NUMBER
                    cboEducationLevel.SelectedValue = obj.EDUCATIONLEVEL
                    If obj.AGESFROM IsNot Nothing Then
                        txtAgesFrom.Text = obj.AGESFROM
                    End If
                    If obj.AGESTO IsNot Nothing Then
                        txtAgesTo.Text = obj.AGESTO
                    End If
                    cboLanguage.SelectedValue = obj.LANGUAGE
                    cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                    If txtScores.Text <> "" Then
                        obj.LANGUAGESCORES = txtScores.Text
                    End If
                    cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                    txtMainTask.Text = obj.MAINTASK
                    cboQualification.SelectedValue = obj.QUALIFICATION
                    cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                    txtQualificationRequest.Text = obj.QUALIFICATIONREQUEST
                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    Dim dtData As DataTable
                    Try
                        dtData = rep.GetTitleByOrgList(obj.ORG_ID, True)
                        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
                    Catch ex As Exception
                        Throw ex
                    End Try

                    cboTitle.SelectedValue = obj.TITLE_ID
                    rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                    rdSendDate.SelectedDate = obj.SEND_DATE
                    For Each itm In obj.lstEmp
                        Dim item As New RadListBoxItem
                        item.Value = itm.EMPLOYEE_ID
                        item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        lstEmployee.Items.Add(item)
                    Next

                    If obj.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False

                        Me.MainToolBar = tbarMain
                        Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Cancel)
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW

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
                        Dim obj As New PlanRegDTO
                        Dim lstEmp As New List(Of PlanRegEmpDTO)
                        obj.SEND_DATE = rdSendDate.SelectedDate
                        obj.ORG_ID = hidOrgID.Value
                        obj.TITLE_ID = cboTitle.SelectedValue
                        obj.RECRUIT_NUMBER = rntxtRecruitNumber.Value
                        obj.EXPECTED_JOIN_DATE = rdExpectedJoinDate.SelectedDate
                        If cboRecruitReason.SelectedValue <> "" Then
                            obj.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If
                        For Each item As RadListBoxItem In lstEmployee.Items
                            Dim emp As New PlanRegEmpDTO
                            emp.EMPLOYEE_ID = item.Value
                            lstEmp.Add(emp)
                        Next
                        obj.REMARK = txtRemark.Text
                        obj.EDUCATIONLEVEL = cboEducationLevel.SelectedValue
                        If txtAgesFrom.Text <> "" Then
                            obj.AGESFROM = txtAgesFrom.Text
                        End If
                        If txtAgesTo.Text <> "" Then
                            obj.AGESTO = txtAgesTo.Text
                        End If
                        obj.LANGUAGE = cboLanguage.SelectedValue
                        obj.LANGUAGELEVEL = cboLanguageLevel.SelectedValue
                        If txtScores.Text <> "" Then
                            obj.LANGUAGESCORES = txtScores.Text
                        End If
                        obj.SPECIALSKILLS = cboSpecialSkills.SelectedValue
                        obj.MAINTASK = txtMainTask.Text
                        obj.QUALIFICATION = cboQualification.SelectedValue
                        obj.COMPUTER_LEVEL = cboComputerLevel.SelectedValue
                        obj.QUALIFICATIONREQUEST = txtQualificationRequest.Text
                        obj.lstEmp = lstEmp
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertPlanReg(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_PlanReg&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyPlanReg(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_PlanReg&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_PlanReg&group=Business")
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

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            lstEmployee.Items.Clear()
            For Each itm In lstCommonEmployee
                Dim item As New RadListBoxItem
                item.Value = itm.EMPLOYEE_ID
                item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                lstEmployee.Items.Add(item)
            Next
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If

            End If
            cboTitle.ClearValue()
            Try
                Dim dtData As DataTable
                Using rep As New RecruitmentRepository
                    If e.CurrentValue <> "" Then
                        dtData = rep.GetTitleByOrgList(Decimal.Parse(e.CurrentValue), True)
                        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            End Try
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
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
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
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
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
                FillRadCombobox(cboRecruitReason, dtData, "NAME", "ID")
                ' Load data to cbo ACADEMY,LANGUAGE, LANGUAGE_LEVEL,MAJOR,SPECIALSKILLS
                'ACADEMY
                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboEducationLevel, dtData, "NAME", "ID", True)
                'LANGUAGE
                dtData = rep.GetOtherList("RC_LANGUAGE", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID", True)
                'LANGUAGE_LEVEL
                dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguageLevel, dtData, "NAME", "ID", True)
                'MAJOR
                dtData = rep.GetOtherList("MAJOR", True)
                FillRadCombobox(cboQualification, dtData, "NAME", "ID", True)
                'SPECIALSKILLS
                dtData = rep.GetOtherList("SPECIALSKILLS", True)
                FillRadCombobox(cboSpecialSkills, dtData, "NAME", "ID", True)
                'COMPUTERLEVEL
                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboComputerLevel, dtData, "NAME", "ID", True)
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

    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        GetTotalEmployeeByTitleID()
    End Sub

    Protected Sub GetTotalEmployeeByTitleID()
        Try
            'Dim OUT_NUMBER As String
            'Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_TOTAL_EMPLOYEE_BY_TITLEID", New List(Of Object)({hidOrgID.Value, cboTitle.SelectedValue, Common.Common.GetUserName(), OUT_NUMBER}))
            'txtCurrentNumber.Text = obj(0).ToString()

            If hidOrgID.Value <> String.Empty Then
                Dim tab As DataTable = store.GetCurrentManningTitle(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue)
                If tab.Rows.Count > 0 Then
                    txtPayrollLimit.Text = tab.Rows(0)("NEW_MANNING").ToString()
                    txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                    txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                Else
                    txtPayrollLimit.Text = "0"
                    txtCurrentNumber.Text = "0"
                    txtDifferenceNumber.Text = "0"
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class