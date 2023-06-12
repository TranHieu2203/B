Imports Common

Public Class Global_asax
    Inherits System.Web.HttpApplication



    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Run SheduleImportAT

        SheduleImportAT.Start()
        'SheduleImportAT.Start()
        ' Fires when the application is started
        LogHelper.OnlineUsers = New Dictionary(Of String, OnlineUser)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub


    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub


    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        Try
            If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                Try
                    LogHelper.SaveAccessLog(Session.SessionID, "Timeout")
                    LogHelper.OnlineUsers.Remove(Session.SessionID)
                Catch ex As Exception

                End Try

            End If

            'Session.Abandon()

        Catch ex As Exception

        End Try
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
End Class