Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlFindTitlePopup
    Inherits CommonView

    Delegate Sub TitleSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event TitleSelected As TitleSelectedDelegate
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
    Public ReadOnly Property SelectedTitle1 As List(Of TitleDTO)
        Get
            Using rep As New CommonRepository
                Return rep.GetTitleFromId(TitleList)
            End Using
        End Get
    End Property
    Private _titleOtherSelect As String
    Public Property TitleOtherSelect As String
        Get
            Return _titleOtherSelect
        End Get
        Set(ByVal value As String)
            _titleOtherSelect = value
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
                        Dim rep As New CommonRepository
                        TitleList = empIds
                        RaiseEvent TitleSelected(Nothing, e)
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
    '    script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindTitlePopupDialog&noscroll=1');"
    '    script &= "oWnd.show();"
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
    'End Sub
    Public Sub Show()
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(OnClientClose);"
        If TitleOtherSelect IsNot Nothing Then
            script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindTitlePopupDialog&noscroll=1&TitleOtherSelect=" & TitleOtherSelect & "');"
        Else
            script &= "oWnd.setUrl('Dialog.aspx?mid=Common&fid=ctrlFindTitlePopupDialog&noscroll=1');"
        End If
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