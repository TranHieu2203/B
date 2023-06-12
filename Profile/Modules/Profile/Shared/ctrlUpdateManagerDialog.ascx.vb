Imports Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlUpdateManagerDialog
    Inherits CommonView

    Protected WithEvents ctrlFindLMPopup As ctrlFindPositionPopup
    Protected WithEvents ctrlFindCSMPopup As ctrlFindPositionPopup
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public Property SelectedItem As List(Of String)
        Get
            If PageViewState(Me.ID & "_SelectedItem") Is Nothing Then
                PageViewState(Me.ID & "_SelectedItem") = New List(Of String)
            End If
            Return PageViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of String))
            PageViewState(Me.ID & "_SelectedItem") = value
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
    Public Property popupId2 As String
    Public WithEvents AjaxManager2 As RadAjaxManager
    Public AjaxLoading2 As RadAjaxLoadingPanel
    Public Property AjaxManagerId2 As String
    Public Property AjaxLoadingId2 As String
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            Dim popup As RadWindow
            popup = CType(Me.Parent.Page, AjaxPage).PopupWindow
            popupId2 = popup.ClientID
            AjaxManager2 = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading2 = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId2 = AjaxManager2.ClientID
            AjaxLoadingId2 = AjaxLoading2.ClientID
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlFindLMPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindLMPopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindLMPopup.SelectedEmployeeID.Count > 0 Then
                Dim empID = ctrlFindLMPopup.SelectedEmployeeID(0)
                Dim nameQLTT = ctrlFindLMPopup.SelectedEmployeeID(1)
                Dim codeQLTT = ctrlFindLMPopup.SelectedEmployeeID(2)
                hidLM.Value = empID
                txtLM.Text = codeQLTT & " - " & nameQLTT
                txtLMName.Text = If(ctrlFindLMPopup.SelectedEmployeeID.Count > 4, ctrlFindLMPopup.SelectedEmployeeID(4), "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindCSMPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindCSMPopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindCSMPopup.SelectedEmployeeID.Count > 0 Then
                Dim empID = ctrlFindCSMPopup.SelectedEmployeeID(0)
                Dim nameQLTT = ctrlFindCSMPopup.SelectedEmployeeID(1)
                Dim codeQLTT = ctrlFindCSMPopup.SelectedEmployeeID(2)
                hidCSM.Value = empID
                txtCSM.Text = codeQLTT & " - " & nameQLTT
                txtCSMName.Text = If(ctrlFindCSMPopup.SelectedEmployeeID.Count > 4, ctrlFindCSMPopup.SelectedEmployeeID(4), "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindLM_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindLM.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindLMPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindCSM_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindCSM.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindCSMPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            ChangeToolbarState()
            If phFindCSM.Controls.Contains(ctrlFindCSMPopup) Then
                phFindCSM.Controls.Remove(ctrlFindCSMPopup)
            End If
            If phFindLM.Controls.Contains(ctrlFindLMPopup) Then
                phFindLM.Controls.Remove(ctrlFindLMPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    If Not phFindCSM.Controls.Contains(ctrlFindCSMPopup) Then
                        ctrlFindCSMPopup = Me.Register("ctrlFindCSMPopup", "Common", "ctrlFindPositionPopup")
                        ctrlFindCSMPopup.MultiSelect = False
                        ctrlFindCSMPopup.LoadAllOrganization = False
                        ctrlFindCSMPopup.MustHaveContract = False
                        phFindCSM.Controls.Add(ctrlFindCSMPopup)
                    End If
                Case 2
                    If Not phFindLM.Controls.Contains(ctrlFindLMPopup) Then
                        ctrlFindLMPopup = Me.Register("ctrlFindLMPopup", "Common", "ctrlFindPositionPopup")
                        ctrlFindLMPopup.MultiSelect = False
                        ctrlFindLMPopup.LoadAllOrganization = False
                        ctrlFindLMPopup.MustHaveContract = False
                        phFindLM.Controls.Add(ctrlFindLMPopup)
                    End If

            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindCSMPopup.CancelClicked,
                                 ctrlFindLMPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        hidSelected.Value = ""
        If isChangeCSM.Checked AndAlso hidCSM.Value = "" Then
            Me.ShowMessage("Không được để trống Vị trí QLGT", NotifyType.Warning)
            Exit Sub
        End If
        If isChangeLM.Checked AndAlso hidLM.Value = "" Then
            Me.ShowMessage("Không được để trống Vị trí QLTT", NotifyType.Warning)
            Exit Sub
        End If
        If isChangeLM.Checked AndAlso hidLM.Value <> "" AndAlso isChangeCSM.Checked AndAlso hidCSM.Value <> "" Then
            hidSelected.Value = hidLM.Value & ";" & hidCSM.Value
        ElseIf isChangeLM.Checked AndAlso hidLM.Value <> "" AndAlso Not isChangeCSM.Checked AndAlso hidCSM.Value = "" Then
            hidSelected.Value = hidLM.Value & ";0"
        ElseIf Not isChangeLM.Checked AndAlso hidLM.Value = "" AndAlso isChangeCSM.Checked AndAlso hidCSM.Value <> "" Then
            hidSelected.Value = "0;" & hidCSM.Value
        Else
            Me.ShowMessage("Vui lòng chọn vị trí QLTT hoặc QLGT để thay đổi", NotifyType.Warning)
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnYesClick();", True)
    End Sub

#End Region

#Region "Custom"

#End Region

End Class