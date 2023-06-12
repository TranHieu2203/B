Imports Telerik.Web.UI

Public Class ctrlFindTRRequestPopup
    Inherits CommonView

    Delegate Sub TRRequestSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event TRRequestSelected As TRRequestSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
    Private TRRequestListID As List(Of Decimal)
    Private _request_Select As String

    Public ReadOnly Property SelectedTRRequestID As List(Of Decimal)
        Get
            Return TRRequestListID
        End Get
    End Property

    Public Property Year As Decimal
        Get
            Return ViewState(Me.ID & "_Year")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Year") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(e As System.EventArgs)
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
    Private Sub AjaxManager_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
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
                        TRRequestListID = empIds
                        RaiseEvent TRRequestSelected(Nothing, e)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"
    'Public Sub Show()
    '    Dim script As String
    '    script = "var oWnd = $find('" & popupId & "');"
    '    script &= "oWnd.add_close(OnClientClose);"
    '    script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindTRRequestPopupDialog&noscroll=1');"
    '    script &= "oWnd.show();"
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    'End Sub
    Public Sub Show()
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindTRRequestPopupDialog&noscroll=1&YEAR=" & Year & "');"
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