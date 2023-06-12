Public Class ctrlDashboard
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
#End Region

#Region "Page"
    ''' <summary>
    ''' ViewInit
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        IsShowDenyMessage = False

    End Sub
    ''' <summary>
    ''' ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "SizeToFit(0);", True)
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"

#End Region
End Class