Public Class ctrlSessionWarning
    Inherits CommonView




#Region "Property"
    ' Không kiểm tra quyền
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Property EnableLogAccess As Boolean = False
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub
#End Region
#Region "Event"

#End Region
#Region "Custom"
    Private Sub btnCONTINUE_Click(sender As Object, e As System.EventArgs) 'Handles btnCONTINUE.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnCONTINUEClick();", True)
    End Sub
#End Region
End Class


