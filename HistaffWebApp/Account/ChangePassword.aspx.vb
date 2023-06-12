Imports Framework.UI
Imports Common

Public Class ChangePassword
    Inherits PageBase
    Public Overrides Property MustAuthenticate As Boolean = False

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Me.DataBind()
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As System.EventArgs) Handles btnChangePassword.Click
        If Page.IsValid Then
            Dim membershipProvider = Membership.Provider
            If txtOLD_PASS.Text = txtNEW_PASS.Text Then
                ShowMessage(Translate("Mật khẩu mới phải khác mật khẩu cũ"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            Dim r As Boolean = membershipProvider.ChangePassword(Common.Common.GetUsername, txtOLD_PASS.Text, txtNEW_PASS.Text)
            If r Then
                If LogHelper.CurrentUser IsNot Nothing Then
                    LogHelper.CurrentUser.IS_CHANGE_PASS = True
                End If
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                Dim script As String
                script = "setTimeout(function () { document.location.href='/'; }, 1000);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "autoclose", script, True)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        End If
    End Sub

    Dim isOld As Boolean = False

    Private Sub valiidateOldPass_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valiidateOldPass.ServerValidate
        Try
            Dim membershipProvider = Membership.Provider
            Dim passOld = membershipProvider.GetPassword(Common.Common.GetUsername, "")
            Dim EncryptData As New EncryptData
            isOld = (EncryptData.EncryptString(txtOLD_PASS.Text) = passOld)
            args.IsValid = isOld

        Catch ex As Exception
            args.IsValid = True
        End Try

    End Sub

    Private Sub validatePASSWORD_2_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles validatePASSWORD_2.ServerValidate
        Try
            If Not isOld Then
                args.IsValid = True
                Exit Sub
            End If
            Dim maxLength As Integer
            Dim msg As String = ""

            maxLength = CommonConfig.PasswordLength
            If txtNEW_PASS.Text.Length < maxLength Then
                msg = "<br />" & Translate("Độ dài mật khẩu tối thiểu {0}", maxLength)
            End If
            If CommonConfig.PasswordLowerChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtNEW_PASS.Text, "^(?=.*[a-z]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự thường")
                End If
            End If
            If CommonConfig.PasswordUpperChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtNEW_PASS.Text, "^(?=.*[A-Z]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự hoa")
                End If
            End If
            If CommonConfig.PasswordNumberChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtNEW_PASS.Text, "^(?=.*[\d]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự số")
                End If
            End If
            If CommonConfig.PasswordSpecialChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtNEW_PASS.Text, "^(?=.*[\W]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự đặc biệt")
                End If
            End If
            If msg <> "" Then
                msg = msg.Substring(6)
                validatePASSWORD_2.ErrorMessage = msg
                validatePASSWORD_2.ToolTip = msg
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Response.Redirect("/Default.aspx")
    End Sub
End Class