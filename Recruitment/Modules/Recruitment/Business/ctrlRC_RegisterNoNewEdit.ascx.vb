Imports Framework.UI
Imports Common
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities

Public Class ctrlRC_RegisterNoNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopupRe As ctrlFindOrgPopup
    Protected WithEvents ctrlFindOrgPopupPe As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

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
                    Dim obj = rep.GetRegisterNoByID(New RC_REGISTER_CODEDTO With {.ID = IDSelect})
                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        hidRequietOrgID.Value = obj.ORG_ID_REQUEST
                        hidPerfromOrgID.Value = obj.ORG_ID_PERFORM
                        hidRequiredID.Value = obj.ID_REQUIRED_VOTES

                        txtCode.Text = obj.CODE
                        cboYear.SelectedValue = obj.YEAR
                        txtPERFORM_ORG.Text = obj.ORG_PERFROM_NAME
                        txtREQUEST_ORG.Text = obj.ORG_REQUEST_NAME
                        cboTitle.SelectedValue = obj.TITLE_ID
                        nmNUMBER_PLAN.Value = obj.NUMBER_PLAN
                        nmNUMBER_MUST.Value = obj.NUMBER_MUST
                        nmNUMBER_NOW.Value = obj.NUMBER_NOW
                        rdRC_CREATE_DATE.SelectedDate = obj.RC_CREATE_DATE
                        cboRC_CHECK.SelectedValue = obj.RC_CHECK
                        nmNUMBER_REQUIRED_DTL.Value = obj.NUMBER_REQUIRED_DETAIL
                        nmREQUIRED_ID_DTL.Value = obj.ID_REQUIRED_DETAIL
                        rdREQUIRED_DATE.SelectedDate = obj.TIME_REQUIRED
                        cboTYPE_REQUIRED.SelectedValue = obj.TYPE_REQUIRED
                        nmFromAge.Value = obj.FROM_AGE
                        nmToAge.Value = obj.TO_AGE
                        cboGENDER.SelectedValue = obj.GENDER
                        cboLEARNING_LEVEL.SelectedValue = obj.LEARNING_LEVEL
                        txtMAJOR.Text = obj.MAJOR
                        nmEXPERIENCE.Value = obj.EXPERIENCE
                        txtSKILLS.Text = obj.SKILLS
                        txtEXAM1.Text = obj.EXAM_1
                        txtEXAM2.Text = obj.EXAM_2
                        txtEXAM3.Text = obj.EXAM_3

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
                        Dim obj As New RC_REGISTER_CODEDTO
                        obj.CODE = txtCode.Text
                        obj.YEAR = cboYear.SelectedValue
                        obj.ID_REQUIRED_VOTES = hidRequiredID.Value
                        obj.ORG_ID_REQUEST = hidRequietOrgID.Value
                        obj.TITLE_ID = cboTitle.SelectedValue
                        obj.NUMBER_PLAN = nmNUMBER_PLAN.Value
                        obj.NUMBER_MUST = nmNUMBER_MUST.Value
                        obj.NUMBER_NOW = nmNUMBER_NOW.Value
                        obj.RC_CREATE_DATE = rdRC_CREATE_DATE.SelectedDate
                        obj.RC_CHECK = cboRC_CHECK.SelectedValue
                        obj.NUMBER_REQUIRED_DETAIL = nmNUMBER_REQUIRED_DTL.Value
                        obj.ID_REQUIRED_DETAIL = nmREQUIRED_ID_DTL.Value
                        obj.TIME_REQUIRED = rdREQUIRED_DATE.SelectedDate
                        obj.TYPE_REQUIRED = cboTYPE_REQUIRED.SelectedValue
                        obj.ORG_ID_PERFORM = hidPerfromOrgID.Value
                        obj.FROM_AGE = nmFromAge.Value
                        obj.TO_AGE = nmToAge.Value
                        obj.GENDER = cboGENDER.SelectedValue
                        obj.LEARNING_LEVEL = cboLEARNING_LEVEL.SelectedValue
                        obj.MAJOR = txtMAJOR.Text
                        obj.EXPERIENCE = nmEXPERIENCE.Value
                        obj.SKILLS = txtSKILLS.Text
                        obj.EXAM_1 = txtEXAM1.Text
                        obj.EXAM_2 = txtEXAM2.Text
                        obj.EXAM_3 = txtEXAM3.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRegisterNo(obj, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyRegisterNo(obj, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopupRe.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnFindOrg2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg2.Click
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindOrgPopupPe.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopupRe.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopupRe.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidRequietOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtREQUEST_ORG.Text = orgItem.NAME_VN
                txtREQUEST_ORG.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
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

    Private Sub ctrlFindOrgPopupPe_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopupPe.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopupPe.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidPerfromOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtPERFORM_ORG.Text = orgItem.NAME_VN
                txtPERFORM_ORG.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopupRe.CancelClicked
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
            If phFindOrgRe.Controls.Contains(ctrlFindOrgPopupRe) Then
                phFindOrgRe.Controls.Remove(ctrlFindOrgPopupRe)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If phFindOrgPe.Controls.Contains(ctrlFindOrgPopupPe) Then
                phFindOrgPe.Controls.Remove(ctrlFindOrgPopupPe)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopupRe = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopupRe.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrgRe.Controls.Add(ctrlFindOrgPopupRe)
                    ctrlFindOrgPopupRe.Show()
                Case 3
                    ctrlFindOrgPopupPe = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopupPe.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrgPe.Controls.Add(ctrlFindOrgPopupPe)
                    ctrlFindOrgPopupPe.Show()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Dim rep As New RecruitmentRepository
        Try
            Dim tableYear As New DataTable
            Dim dtData
            Dim dtCheck
            Dim dtContractType
            Dim dtACADEMY
            tableYear.Columns.Add("YEAR", GetType(Integer))
            tableYear.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 5
                row = tableYear.NewRow
                row("ID") = index
                row("YEAR") = index
                tableYear.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, tableYear, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            ' giới tính
            dtData = rep.GetOtherList("GENDER")
            FillRadCombobox(cboGENDER, dtData, "NAME", "ID")
            ' kiem tra dinh bien
            dtCheck = rep.GetOtherList("RC_CHECK_REQUIRED")
            FillRadCombobox(cboRC_CHECK, dtCheck, "NAME", "ID")
            ' loai hinh tuyen dung 
            dtContractType = rep.GetContractTypeList(True)
            FillRadCombobox(cboTYPE_REQUIRED, dtContractType, "NAME", "ID")
            ' trình độ văn hóa 
            dtACADEMY = rep.GetOtherList("ACADEMY")
            FillRadCombobox(cboLEARNING_LEVEL, dtACADEMY, "NAME", "ID")

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