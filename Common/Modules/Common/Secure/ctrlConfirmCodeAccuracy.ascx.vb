Imports Framework.UI

Public Class ctrlConfirmCodeAccuracy
    Inherits Global.Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public IDSelect As Integer


    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()


    End Sub

    Protected Sub InitControl()
        Try
            'Me.ctrlMessageBox.Listener = Me
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub BindData()
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            If txtCodeAccuracy.Text = "" Then
                ShowMessage(Translate("Vui lòng nhập mật khẩu 2!"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            Session("ConfirmCodeAccuracy") = 0
            If CommonConfig.dicConfig("CODE_ACCURACY") = txtCodeAccuracy.Text Then
                Session("ConfirmCodeAccuracy") = 1
                Dim str As String = "getRadWindow().close(1);"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            Else
                ShowMessage(Translate("Mã xác thực không chính xác"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub




#End Region

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Session("ConfirmCodeAccuracy") = 2
        Dim str As String = "getRadWindow().close(1);"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
    End Sub
End Class