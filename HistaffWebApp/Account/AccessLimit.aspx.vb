Imports Common
Imports Framework.UI
Public Class AccessLimit
    Inherits PageBase

    Public Overrides Property MustAuthenticate As Boolean = False

    Public Property ReturnUrl As String
        Get
            Return ViewState("ReturnUrl")
        End Get
        Set(value As String)
            ViewState("ReturnUrl") = value
        End Set
    End Property

    Public Overrides Sub PageLoad(e As System.EventArgs)
        LogHelper.UpdateAllOnlineUserStatus()
        If Request.Params("ReturnUrl") IsNot Nothing Then
            ReturnUrl = HttpUtility.UrlDecode(Request.Params("ReturnUrl"))
        Else
            ReturnUrl = "/" & Utilities.DefaultPage
        End If
        Me.DataBind()
    End Sub

End Class