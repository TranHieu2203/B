Imports Common
Imports Framework.UI

Public Class Login
    Inherits PageBase
    Public Overrides Property MustAuthenticate As Boolean = False
    'Private WithEvents rcb As DropDownList

    Public Property LoginFailCount As Integer
        Get
            If Session("LoginFailCount") Is Nothing Then
                Return 0
            End If
            Return Session("LoginFailCount")
        End Get
        Set(ByVal value As Integer)
            Session("LoginFailCount") = value
        End Set
    End Property

    'Public Sub LoadLanguageCombobox()
    '    Dim rep As New ProfileRepository
    '    Dim lst = rep.GetOtherList("SYSTEM_LANGUAGE")
    '    rcb.DataTextField = "NAME"
    '    rcb.DataValueField = "CODE"
    '    rcb.DataSource = lst
    '    rcb.DataBind()
    '    If rcb.Items.Count > 0 Then
    '        rcb.SelectedValue = Common.Common.SystemLanguage.Name
    '    End If
    'End Sub

    Protected Sub LoggedIn(ByVal sender As Object, ByVal e As EventArgs) Handles LoginUser.LoggedIn
        Try
            Dim chBox As CheckBox = DirectCast(LoginUser.FindControl("RememberMe"), CheckBox)

            If chBox.Checked Then
                Dim myCookie As New HttpCookie("myCookiePortal")
                'Instance the new cookie
                Response.Cookies.Remove("myCookiePortal")
                'Remove previous cookie
                Response.Cookies.Add(myCookie)
                'Create the new cookie
                myCookie.Values.Add("user", Me.LoginUser.UserName)
                'Add the username field to the cookie
                Dim deathDate As DateTime = DateTime.Now.AddDays(15)
                'Days of life
                Response.Cookies("myCookiePortal").Expires = deathDate
                'Assign the life period
                'IF YOU WANT SAVE THE PASSWORD TOO (IT IS NOT RECOMMENDED)
                'myCookie.Values.Add("pass", Me.LoginUser.Password)
            End If
        Catch ex As Exception
        End Try


    End Sub

    Public Sub OnAuthenticate(ByVal sender As Object, ByVal e As AuthenticateEventArgs) Handles LoginUser.Authenticate
        Try
            Dim membershipProvider = Membership.Provider
            Dim txtUsername As TextBox = LoginUser.FindControl("Username")
            Dim txtPassword As TextBox = LoginUser.FindControl("Password")

            If txtUsername.Text = "" Then
                ShowMessage(Translate("Bạn chưa nhập tài khoản"), Utilities.NotifyType.Warning)
                txtUsername.Focus()
                Exit Sub
            End If
            If txtPassword.Text = "" Then
                ShowMessage(Translate("Bạn chưa nhập mật khẩu"), Utilities.NotifyType.Warning)
                txtPassword.Focus()
                Exit Sub
            End If
            e.Authenticated = membershipProvider.ValidateUser(txtUsername.Text.Trim, txtPassword.Text.Trim)

            'Dim SystemLanguage = Common.Common.SystemLanguage
            'Session.Clear()
            'Common.Common.SystemLanguage = SystemLanguage
        Catch ex As Exception
            Select Case ex.Message
                Case LoginError.LDAP_SERVER_NOT_FOUND
                    Utilities.ShowMessage(LoginUser, Me.Translate("LDAP Server chưa được config"), Utilities.NotifyType.Warning)
                Case LoginError.NO_PERMISSION
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản chưa được phân quyền"), Utilities.NotifyType.Warning)
                Case LoginError.USERNAME_EXPIRED
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đã hết hạn"), Utilities.NotifyType.Warning)
                Case LoginError.USERNAME_NOT_EXISTS
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản không tồn tại"), Utilities.NotifyType.Warning)
                Case LoginError.WRONG_PASSWORD
                    Dim Username As TextBox = LoginUser.FindControl("Username")
                    Dim LoginFailCount = HttpContext.Current.Session(Username.Text.Trim & "_LoginFailCount")
                    Dim maxFail As Integer
                    maxFail = CommonConfig.MaxNumberLoginFail
                    Dim FailCount = 0
                    If LoginFailCount IsNot Nothing AndAlso IsNumeric(maxFail) Then
                        FailCount = maxFail - LoginFailCount
                    End If
                    If FailCount > 1 Then
                        Utilities.ShowMessage(LoginUser, Me.Translate("Thông tin đăng nhập không đúng. <br/> Sau " & maxFail & " lần sai tài khoản sẽ bị khóa. <br/> Bạn còn " & FailCount & " lần thử lại"), Utilities.NotifyType.Warning)
                    ElseIf FailCount = 1 Then
                        Utilities.ShowMessage(LoginUser, Me.Translate("Mật khẩu không chính xác. <br/> Bạn còn 1 lần nhập sai, vui lòng liên hệ Nhân sự hoặc thực hiện quên mật khẩu để không bị khóa tài khoản."), Utilities.NotifyType.Warning)
                    Else
                        Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đã bị khóa. <br/> Vui lòng liên hệ Nhân sự để được mở khóa tài khoản và cấp lại mật khẩu"), Utilities.NotifyType.Warning)
                    End If
                Case LoginError.WRONG_USERNAME_OR_PASSWORD
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản hoặc mật khẩu không chính xác"), Utilities.NotifyType.Warning)
                Case LoginError.USER_LOCKED
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đã bị khóa. <br/> Vui lòng liên hệ Nhân sự để được mở khóa tài khoản và cấp lại mật khẩu"), Utilities.NotifyType.Warning)
                Case LoginError.USER_NOT_EMPLOYEE
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản không phải nhân viên"), Utilities.NotifyType.Warning)
                Case LoginError.EMPLOYEE_WORK_STATUS
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản của nhân viên đã nghỉ việc"), Utilities.NotifyType.Warning)
                Case Else
                    DisplayException(Me.Title, "", ex)
            End Select
        End Try
    End Sub

    'Private Sub rcb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcb.SelectedIndexChanged
    '    Common.Common.SystemLanguage = System.Globalization.CultureInfo.CreateSpecificCulture(rcb.SelectedValue)
    '    Response.Redirect(Request.Url.AbsoluteUri)
    'End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Translate("Đăng nhập")
        'rcb = LoginUser.FindControl("rcbLanguage")
        If Not IsPostBack Then
            'LoadLanguageCombobox()
            Try
                If Request.Cookies("myCookiePortal") IsNot Nothing Then
                    Dim cookie As HttpCookie = Request.Cookies.Get("myCookiePortal")
                    Dim user As String = cookie.Values("user").ToString()
                    'Dim pass As String = cookie.Values("pass").ToString()
                    If user.Trim <> "" Then
                        LoginUser.UserName = user
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
        Me.DataBind()
        Dim txtUsername As TextBox = LoginUser.FindControl("UserName")
        txtUsername.Text = txtUsername.Text.Trim
        txtUsername.Focus()
    End Sub
End Class