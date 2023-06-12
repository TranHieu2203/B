Imports Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlUpdateManager
    Inherits CommonView

    Delegate Sub ManagerSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event ManagerSelected As ManagerSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Private ManagerListID As List(Of Decimal)
    Public Property popupId2 As String
    Public WithEvents AjaxManager2 As RadAjaxManager
    Public AjaxLoading2 As RadAjaxLoadingPanel
    Public Property AjaxManagerId2 As String
    Public Property AjaxLoadingId2 As String

    Public ReadOnly Property SelectedManager As TitleDTO
        Get
            SelectedManager = New TitleDTO
            If ManagerListID(0) > 0 Then
                SelectedManager.LM = ManagerListID(0)
            End If
            If ManagerListID(1) > 0 Then
                SelectedManager.CSM = ManagerListID(1)
            End If
            Return SelectedManager
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


    Public Property BackupOnAjaxStart As String
        Get
            Return ViewState(Me.ID & "_BackupOnAjaxStart")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_BackupOnAjaxStart") = value
        End Set
    End Property

    Public Property BackupOnAjaxEnd As String
        Get
            Return ViewState(Me.ID & "BackupOnAjaxEnd")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "BackupOnAjaxEnd") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            popupId2 = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            AjaxManager2 = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading2 = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId2 = AjaxManager2.ClientID
            AjaxLoadingId2 = AjaxLoading2.ClientID
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If BackupOnAjaxStart Is Nothing Then
            BackupOnAjaxStart = AjaxManager2.ClientEvents.OnRequestStart
        End If
        If BackupOnAjaxEnd Is Nothing Then
            BackupOnAjaxEnd = AjaxManager2.ClientEvents.OnResponseEnd
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub AjaxManager2_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager2.AjaxRequest
        Try
            Dim eventArg As String = e.Argument  'Request("__EVENTARGUMENT")
            Dim a = Left(eventArg, Me.ClientID.Length + 14)
            Dim b = Me.ClientID & "_PopupPostback"
            If Left(eventArg, Me.ClientID.Length + 14) <> Me.ClientID & "_PopupPostback" Then
                Exit Sub
            End If
            If eventArg <> "" Then
                eventArg = Right(eventArg, eventArg.Length - Me.ClientID.Length - 15)
                If eventArg = "Cancel" Then
                    Close()
                    RaiseEvent CancelClicked(Nothing, e)
                Else
                    Dim Ids = eventArg.Split(";")
                    Dim empIds As New List(Of Decimal)
                    For Each str As String In Ids
                        If str <> "" Then
                            Dim id As Decimal
                            id = Decimal.Parse(str)
                            empIds.Add(id)
                        End If
                    Next
                    If empIds.Count > 0 Then
                        ManagerListID = empIds
                        RaiseEvent ManagerSelected(Nothing, e)
                    End If
                End If
            End If

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = BackupOnAjaxStart
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnResponseEnd = BackupOnAjaxEnd
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"

    Public Sub Show()
        Dim state As String = ""
        Dim script As String
        script = "var oWnd = $find('" & popupId2 & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlUpdateManagerDialog&group=Shared&noscroll=1');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub


    Public Sub Close()
        Dim script As String
        script = "$find('" & popupId2 & "').close();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    End Sub

#End Region

End Class