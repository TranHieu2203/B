Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities

Public Class ForgetPass
    Inherits PageBase
    Public Overrides Property MustAuthenticate As Boolean = False

#Region "Properties"
    Property countUserName As Decimal
        Get
            Return PageViewState(Me.ID & "_countUserName")
        End Get
        Set(value As Decimal)
            PageViewState(Me.ID & "_countUserName") = value
        End Set
    End Property

    Property countCode As Decimal
        Get
            Return PageViewState(Me.ID & "_countCode")
        End Get
        Set(value As Decimal)
            PageViewState(Me.ID & "_countCode") = value
        End Set
    End Property

    Property UserName As String
        Get
            Return PageViewState(Me.ID & "_UserName")
        End Get
        Set(value As String)
            PageViewState(Me.ID & "_UserName") = value
        End Set
    End Property

    Property ChangeCode As String
        Get
            Return PageViewState(Me.ID & "_ChangeCode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_ChangeCode") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Me.DataBind()
        If Not IsPostBack Then
            countUserName = 0
            countCode = 0
        End If
    End Sub



#End Region

#Region "Event"
    Private Sub btnForgetPassword_Click(sender As Object, e As System.EventArgs) Handles btnForgetPassword.Click
        Dim rep As New CommonRepository
        Dim dataMail As List(Of String)
        Dim body As String = ""
        Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        Try
            If Page.IsValid Then
                Dim uID As Decimal
                Dim result = rep.ValidateUserForgetPass(txtUsername.Text.Trim, uID)
                If result = "1" Then
                    ShowMessage(Translate("Tài khoản không tồn tại, vui lòng nhập lại tên tài khoản"), Utilities.NotifyType.Warning)
                    Exit Sub
                ElseIf result = "2" Then
                    ShowMessage(Translate("Tài khoản chưa được thiết lập email, bạn vui lòng liên hệ bộ phận nhân sự để reset mật khẩu"), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                UserName = txtUsername.Text.Trim
                Dim random As New Random
                dataMail = rep.GET_MAIL_TEMPLATE("QUEN_MK", "Common")
                body = dataMail(0)
                titleMail = "Forget password"

                For i As Decimal = 0 To 6
                    ChangeCode = ChangeCode & chars.Substring(random.Next(1, chars.Length - 1), 1)
                Next

                bodyNew = String.Format(body, ChangeCode)
                If Not Common.Common.sendEmailByServerMail(result,
                                                         If(dataMail(1) IsNot Nothing, dataMail(1), result),
                                                          titleMail, bodyNew, String.Empty) Then
                    ShowMessage(Translate("Đã xảy ra lỗi"), NotifyType.Warning)
                    Exit Sub
                Else
                    ShowMessage(Translate("Hệ thống đã gửi mã xác nhận về email của bạn. Lưu ý mã xác nhận chỉ tồn tại trong 60s!"), NotifyType.Warning)
                    Session("START_CODE") = DateTime.Now
                    Panel1.Visible = False
                    Panel2.Visible = True
                End If
                Panel1.Visible = False
                Panel2.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSubmitCode_Click(sender As Object, e As System.EventArgs) Handles btnSubmitCode.Click
        Dim rep As New CommonRepository
        Try
            If Page.IsValid Then
                If DateDiff(DateInterval.Second, CDate(Session("START_CODE")), DateTime.Now) > 60 Then
                    ShowMessage(Translate("Mã code đã quá thời hạn, vui lòng thao tác lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf Not txtCode.Text.Trim.Equals(ChangeCode) Then
                    ShowMessage(Translate("Mã code không chính xác"), NotifyType.Warning)
                    Exit Sub
                End If
                Panel2.Visible = False
                Panel3.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub validatePASSWORD_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles validatePASSWORD.ServerValidate
        Try
            Dim maxLength As Integer
            Dim msg As String = ""

            maxLength = CommonConfig.PasswordLength
            If txtPassword.Text.Length < maxLength Then
                msg = "<br />" & Translate("Độ dài mật khẩu tối thiểu {0}", maxLength)
            End If
            If CommonConfig.PasswordLowerChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtPassword.Text, "^(?=.*[a-z]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự thường")
                End If
            End If
            If CommonConfig.PasswordUpperChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtPassword.Text, "^(?=.*[A-Z]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự hoa")
                End If
            End If
            If CommonConfig.PasswordNumberChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtPassword.Text, "^(?=.*[\d]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự số")
                End If
            End If
            If CommonConfig.PasswordSpecialChar <> 0 Then
                If Not RegularExpressions.Regex.IsMatch(txtPassword.Text, "^(?=.*[\W]).*$") Then
                    msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự đặc biệt")
                End If
            End If
            If msg <> "" Then
                msg = msg.Substring(6)
                validatePASSWORD.ErrorMessage = msg
                validatePASSWORD.ToolTip = msg
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSubmidPass_Click(sender As Object, e As System.EventArgs) Handles btnSubmidPass.Click
        Try
            If Page.IsValid Then
                Dim rep As New CommonRepository
                If rep.ChangePassByForget(UserName, txtPassword.Text.Trim) Then
                    Dim str As String = "getRadWindow().Close('1');"
                    Session.Remove("START_CODE")
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click, btnCodeCancel.Click, btnPassCancel.Click
        Dim str As String = "getRadWindow().Close('0');"
        Session.Remove("START_CODE")
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
    End Sub

#End Region

End Class