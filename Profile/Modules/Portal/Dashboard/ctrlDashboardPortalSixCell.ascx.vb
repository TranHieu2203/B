Imports Common

Public Class ctrlDashboardPortalSixCell
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        IsShowDenyMessage = False
        Session("_EmployeeID") = EmployeeID
    End Sub
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "SizeToFit(0);", True)
            Dim Name_tittle = CommonConfig.dicConfig("NAME_MYPAGE_PORTAL")
            lblTittle.Text = Name_tittle
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"

#End Region
End Class