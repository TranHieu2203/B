Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_SalaryPlanNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

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
        Dim rep As New PayrollRepository
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetSalaryPlanningByID(New PASalaryPlanningDTO With {.ID = IDSelect})
                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        hidOrgID.Value = obj.ORG_ID
                        rntxtYear.Value = obj.YEAR
                        txtOrgName.Text = obj.ORG_NAME
                        cboTitle.SelectedValue = obj.TITLE_ID
                        rntxtMonth.Value = obj.MONTH
                        rntxtEmpNumber.Value = obj.EMP_NUMBER
                        Dim dtData As DataTable
                        dtData = rep.GetTitleByOrgList(Decimal.Parse(hidOrgID.Value), True)
                        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
                    End If

                Case "InsertView"
                    If Request.Params("year") <> "" Then
                        rntxtYear.Value = Decimal.Parse(Request.Params("year"))
                    End If
                    hidOrgID.Value = Decimal.Parse(Request.Params("org_id"))
                    txtOrgName.Text = Request.Params("Org_Name")
                    Try
                        Dim dtData As DataTable
                        dtData = rep.GetTitleByOrgList(Decimal.Parse(hidOrgID.Value), True)
                        FillRadCombobox(cboTitle, dtData, "NAME", "ID")

                    Catch ex As Exception
                        Throw ex
                    End Try
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
        Dim rep As New PayrollRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New PASalaryPlanningDTO
                        obj.YEAR = rntxtYear.Value
                        obj.ORG_ID = hidOrgID.Value
                        If cboTitle.SelectedValue <> "" Then
                            obj.TITLE_ID = cboTitle.SelectedValue
                        End If
                        obj.MONTH = rntxtMonth.Value
                        obj.EMP_NUMBER = rntxtEmpNumber.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertSalaryPlanning(obj, gID) Then
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Planning&fid=ctrlPA_SalaryPlan&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifySalaryPlanning(obj, gID) Then
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Planning&fid=ctrlPA_SalaryPlan&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Planning&fid=ctrlPA_SalaryPlan&group=Business")
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
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            If ctrlFindOrgPopup.CurrentItemDataObject IsNot Nothing Then
                Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                cboTitle.ClearValue()
                Try
                    Dim dtData As DataTable

                    Using rep As New PayrollRepository
                        If e.CurrentValue <> "" Then
                            dtData = rep.GetTitleByOrgList(Decimal.Parse(e.CurrentValue), True)
                            FillRadCombobox(cboTitle, dtData, "NAME", "ID")

                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                End Try
                isLoadPopup = 0
            End If

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
        Dim rep As New PayrollRepository
        Try
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