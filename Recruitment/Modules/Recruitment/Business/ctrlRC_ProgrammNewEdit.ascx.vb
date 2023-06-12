Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgrammNewEdit
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

                    txtcodeYCTD.Text = obj.CODE_YCTD
                    txtnameYCTD.Text = obj.NAME_YCTD
                    txtPerRequest.Text = obj.PER_REQUEST

                    txtOrgName.Text = obj.ORG_NAME
                    If obj.FOLLOWERS_EMP_ID IsNot Nothing Then
                        hidEmpID.Value = obj.FOLLOWERS_EMP_ID
                    End If
                    'txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    'duy fix ngay 11/07.2016
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If
                    If obj.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = obj.STATUS_ID
                        cboStatus.Text = obj.STATUS_NAME
                    End If
                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                    rdSendDate.SelectedDate = obj.SEND_DATE
                    rdRespone.SelectedDate = obj.EXPECTED_JOIN_DATE
                    If obj.NUMBERRECRUITMENT Is Nothing Then
                        txtRecruitNumber.Text = 0
                    Else
                        txtRecruitNumber.Text = obj.NUMBERRECRUITMENT
                    End If
                    If obj.IS_SUPPORT IsNot Nothing Then
                        chkTNGSupport.Checked = obj.IS_SUPPORT
                    Else
                        chkTNGSupport.Checked = False
                    End If
                    If obj.IS_OVER_LIMIT IsNot Nothing Then
                        chkVuotDB.Checked = obj.IS_OVER_LIMIT
                    Else
                        chkVuotDB.Checked = False
                    End If
                    If obj.LOCATION_ID IsNot Nothing Then
                        cboLocation.SelectedValue = obj.LOCATION_ID
                    End If

                    txtCode.Text = obj.CODE
                    If obj.CONTRACT_TYPE_ID IsNot Nothing Then
                        cboTypeContract.SelectedValue = obj.CONTRACT_TYPE_ID
                    End If
                    If obj.RC_RECRUIT_PROPERTY IsNot Nothing Then
                        cboTCRecruit.SelectedValue = obj.RC_RECRUIT_PROPERTY
                    End If
                    txtprofileReceive.Text = obj.CANDIDATE_COUNT
                    txtCountRecruited.Text = obj.CANDIDATE_RECEIVED
                    txtRecruitReason.Text = obj.RECRUIT_REASON
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    txtTitle.Text = obj.TITLE_NAME
                    hidTitleID.Value = obj.TITLE_ID
                    If obj.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False
                    End If
                    GetTotalEmployeeByTitleID()
                    If cboStatus.Text = "Phê duyệt" Then
                        cboStatus.Text = ""
                    End If

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    rdSendDate.AutoPostBack = True

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region
    Protected Sub GetTotalEmployeeByTitleID()
        Try
            'Dim OUT_NUMBER As String
            'Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_TOTAL_EMPLOYEE_BY_TITLEID", New List(Of Object)({hidOrgID.Value, cboTitle.SelectedValue, Common.Common.GetUserName(), OUT_NUMBER}))
            'txtCurrentNumber.Text = obj(0).ToString()

            If hidOrgID.Value <> String.Empty Then
                Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), hidTitleID.Value, rdSendDate.SelectedDate)
                If tab.Rows.Count > 0 Then
                    txtCountDB.Text = tab.Rows(0)("NEW_MANNING").ToString()
                    txtCountNow.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                    txtCountTG.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                Else
                    txtCountDB.Text = "0"
                    txtCountNow.Text = "0"
                    txtCountTG.Text = "0"
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New ProgramDTO
                        obj.CODE = txtCode.Text
                        ' obj.RECRUIT_REASON = txtRecruitReason.Text
                        'obj.REQUEST_NUMBER = rntxtRecruitNumber.Value
                        If cboStatus.SelectedValue <> "" Then
                            obj.STATUS_ID = cboStatus.SelectedValue
                        End If
                        If cboRecruitReason.SelectedValue <> "" Then
                            obj.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If


                        'cboTimeWork.SelectedValue = obj.

                        obj.SEND_DATE = rdSendDate.SelectedDate
                        obj.ID = hidID.Value
                        If hidEmpID.Value <> "" Then
                            obj.FOLLOWERS_EMP_ID = hidEmpID.Value
                        End If
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

    'Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
    '    Try
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    'Protected Sub btnFindEmp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmp.Click
    '    Try
    '        isLoadPopup = 2
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            'lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            'If isLoadPopup = 1 Then
            '    lstEmployee.Items.Clear()
            '    For Each itm In lstCommonEmployee
            '        Dim item As New RadListBoxItem
            '        item.Value = itm.ID
            '        item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
            '        lstEmployee.Items.Add(item)
            '    Next
            'End If
            'If isLoadPopup = 2 Then
            '    For Each itm In lstCommonEmployee
            '        hidEmpID.Value = itm.ID
            '        txtFollowers.Text = itm.FULLNAME_VN
            '    Next
            'End If
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
                'RECRUIT PROPERTY
                dtData = rep.GetOtherList("RC_RECRUIT_PROPERTY")
                FillRadCombobox(cboTCRecruit, dtData, "NAME", "ID", True)
                'CONTRACT TYPE
                'dtData = rep.GetOtherList("CONTRACT_TYPE", True)
                'FillRadCombobox(cboTypeContract, dtData, "NAME", "ID", True)
                dtData = rep.GetOtherList("LABOR_TYPE", True)
                FillRadCombobox(cboTypeContract, dtData, "NAME", "ID")
                'tinh thanh
                dtData = rep.GetProvinceList("False")
                FillRadCombobox(cboLocation, dtData, "NAME", "ID", True)
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