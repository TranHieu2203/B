Imports Framework.UI
Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class CommonReportView
    Inherits ViewBase

    Public Overrides Function IsAuthenticated() As Boolean
        If Utilities.IsAuthenticated AndAlso LogHelper.CurrentUser IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Overrides Sub CheckAuthorization()
        Try
            Dim rep As New CommonRepository

            Dim strStatus As String = LogHelper.GetSessionStatus(Session.SessionID)

            If strStatus = "KILLED" Then
                If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                    LogHelper.SaveAccessLog(Session.SessionID, "Killed")
                    LogHelper.OnlineUsers.Remove(Session.SessionID)
                    Session.Abandon()
                    FormsAuthentication.SignOut()
                    Response.Redirect("/Account/SessionKilled.aspx")
                End If
            End If
            If strStatus = "INACTIVE" Then
                If LogHelper.OnlineUsers IsNot Nothing Then
                    Dim ActiveCount As Integer = 0
                    For Each item In LogHelper.OnlineUsers
                        If item.Value IsNot Nothing AndAlso item.Value.Status = "ACTIVE" Then
                            ActiveCount += 1
                        End If
                    Next
                    'Dim iLimit As Integer = Utilities.AccessLimit(Server.MapPath("~") + EncryptDecrypt.sFileName, Common.AccessLimit)
                    'If ActiveCount > iLimit Then
                    '    Response.Redirect("/Account/AccessLimit.aspx?ReturnUrl=" & HttpUtility.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery))
                    'End If
                End If
            End If

            If Me.MustAuthorize Then
                If Utilities.IsAuthenticated Then
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Utilities.GetUsername)
                    If GroupAdmin = False Then
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Utilities.GetUsername)
                        If permissions IsNot Nothing Then
                            Dim ViewPermissions As List(Of PermissionDTO) = (From p In permissions Where p.FID = Me.ViewName And p.IS_REPORT = True).ToList
                            If ViewPermissions IsNot Nothing Then
                                For Each item In ViewPermissions
                                    Me.Allow = True
                                Next
                            End If
                        End If
                    Else
                        Me.Allow = True
                    End If
                End If
            Else
                Me.Allow = True
            End If
            If Me.MustAuthorize AndAlso Me.Allow Then
                Dim func = rep.GetFunction(Me.ViewName)
                If func IsNot Nothing Then
                    Me.ViewDescription = Translate(func.NAME)
                    Me.ViewGroup = func.FUNCTION_GROUP_NAME
                End If

                If EnableLogAccess Then
                    LogHelper.UpdateAccessLog(Me)
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")
        Common.DisplayException(Me, ex, "ViewName: " & ViewName & " ViewID:" & ID)
    End Sub

End Class
