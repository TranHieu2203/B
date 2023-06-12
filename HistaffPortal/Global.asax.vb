Imports System.IO
Imports System.IO.Compression

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        'LogHelper.OnlineUsers = New Dictionary(Of String, OnlineUser)
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
            'If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then

            '    Try
            '        LogHelper.OnlineUsers.Remove(Session.SessionID)
            '        LogHelper.SaveAccessLog(Session.SessionID, "Timeout")
            '    Catch ex As Exception

            '    End Try

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Protected Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        HttpCompress(CType(sender, HttpApplication))
    End Sub


    Private Sub HttpCompress(ByVal app As HttpApplication)
        Dim acceptEncoding As String = app.Request.Headers("Accept-Encoding")
        Dim prevUncompressedStream As Stream = app.Response.Filter


        If Not (TypeOf app.Context.CurrentHandler Is Page) _
            OrElse app.Context.CurrentHandler.GetType().Name = "SyncSessionlessHandler" _
            OrElse app.Request("HTTP_X_MICROSOFTAJAX") IsNot Nothing Then
            Return
        End If


        If String.IsNullOrEmpty(acceptEncoding) Then
            Return
        End If


        acceptEncoding = acceptEncoding.ToLower()


        If (acceptEncoding.Contains("deflate") OrElse acceptEncoding = "*") AndAlso CompressScript(Request.ServerVariables("SCRIPT_NAME")) Then
            ' deflate 
            app.Response.Filter = New DeflateStream(prevUncompressedStream, CompressionMode.Compress)
            app.Response.AppendHeader("Content-Encoding", "deflate")
        ElseIf acceptEncoding.Contains("gzip") AndAlso CompressScript(Request.ServerVariables("SCRIPT_NAME")) Then
            ' gzip 
            app.Response.Filter = New GZipStream(prevUncompressedStream, CompressionMode.Compress)
            app.Response.AppendHeader("Content-Encoding", "gzip")
        End If
    End Sub


    Private Shared Function CompressScript(ByVal scriptName As String) As Boolean
        If scriptName.ToLower().Contains(".axd") Then
            Return False
        End If
        Return True
    End Function
End Class