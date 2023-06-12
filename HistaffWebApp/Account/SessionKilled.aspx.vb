Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Public Class SessionKilled
    Inherits PageBase

    Public Overrides Property MustAuthenticate As Boolean = False

    Public Overrides Sub PageLoad(e As System.EventArgs)
        Me.DataBind()
        
    End Sub

End Class