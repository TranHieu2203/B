Imports Common
Imports Telerik.Web.UI
Public Class ctrlFindProgramPopupDialog
    Inherits CommonView

    Delegate Sub OrgTitleSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event OrgTitleSelected As OrgTitleSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String

    Private Property TitleList As List(Of Decimal)

    Public ReadOnly Property SelectedTitle() As List(Of Decimal)
        Get
            Return TitleList
        End Get
    End Property
    Public Property CurrentValue As String
        Get
            If ViewState(Me.ID & "_CurrentValue") Is Nothing Then
                ViewState(Me.ID & "_CurrentValue") = ""
            End If
            Return ViewState(Me.ID & "_CurrentValue")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_CurrentValue") = value
        End Set
    End Property
    Public Property IDSelect As String
        Get
            If ViewState(Me.ID & "_IDSelect") Is Nothing Then
                ViewState(Me.ID & "_IDSelect") = ""
            End If
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Public Property LoadAllOrganization As Boolean
        Get
            If ViewState(Me.ID & "_LoadAllOrganization") Is Nothing Then
                ViewState(Me.ID & "_LoadAllOrganization") = False
            End If
            Return ViewState(Me.ID & "_LoadAllOrganization")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_LoadAllOrganization") = value
        End Set
    End Property

    Public Property ID_CANDIDATE As Object
        Get
            If ViewState(Me.ID & "_ID_CANDIDATE") Is Nothing Then
                ViewState(Me.ID & "_ID_CANDIDATE") = ""
            End If
            Return ViewState(Me.ID & "_ID_CANDIDATE")
        End Get
        Set(ByVal value As Object)
            ViewState(Me.ID & "_ID_CANDIDATE") = value
        End Set
    End Property

    Public Property ID_PROGRAM_CANDIDATE As Object
        Get
            If ViewState(Me.ID & "_ID_PROGRAM_CANDIDATE") Is Nothing Then
                ViewState(Me.ID & "_ID_PROGRAM_CANDIDATE") = ""
            End If
            Return ViewState(Me.ID & "_ID_PROGRAM_CANDIDATE")
        End Get
        Set(ByVal value As Object)
            ViewState(Me.ID & "_ID_PROGRAM_CANDIDATE") = value
        End Set
    End Property
    Public Property P_PROGRAM_ID_OLD As Object
        Get
            If ViewState(Me.ID & "_P_PROGRAM_ID_OLD") Is Nothing Then
                ViewState(Me.ID & "_P_PROGRAM_ID_OLD") = ""
            End If
            Return ViewState(Me.ID & "_P_PROGRAM_ID_OLD")
        End Get
        Set(ByVal value As Object)
            ViewState(Me.ID & "_P_PROGRAM_ID_OLD") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId = AjaxManager.ClientID
            AjaxLoadingId = AjaxLoading.ClientID
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnResponseEnd = "onRequestEnd"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim eventArg As String = e.Argument  'Request("__EVENTARGUMENT")
            If Left(eventArg, 13) <> "PopupPostback" Then
                Exit Sub
            End If
            If eventArg <> "" Then
                eventArg = Right(eventArg, eventArg.Length - 14)
                If eventArg = "Cancel" Then
                    Close()
                    RaiseEvent CancelClicked(Nothing, e)
                Else
                    Dim Ids = eventArg.Split(";")
                    Dim empIds As New List(Of Decimal)
                    For Each str As String In Ids
                        If str <> "" Then
                            Dim id As Decimal
                            Try
                                id = Decimal.Parse(str)
                            Catch ex As Exception
                            End Try
                            If id <> 0 Then
                                empIds.Add(id)
                            End If
                        End If
                    Next
                    If empIds.Count > 0 Then
                        Dim rep As New CommonRepository
                        TitleList = empIds
                        RaiseEvent OrgTitleSelected(Nothing, e)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"
    Public Sub Show()
        Dim a = ID_CANDIDATE
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlFindProgramPopupView&LoadAllOrganization=" & If(LoadAllOrganization, "1", "0") & "&group=Shared&noscroll=1&ID_CANDIDATE=" & ID_CANDIDATE & "&ID_PROGRAM_CANDIDATE=" & ID_PROGRAM_CANDIDATE & "&P_PROGRAM_ID_OLD=" & P_PROGRAM_ID_OLD & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub
    Public Sub Close()
        Dim script As String
        script = "$find('" & popupId & "').close();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub
#End Region

End Class