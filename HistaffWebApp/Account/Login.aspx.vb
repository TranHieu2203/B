Imports Common
Imports Framework.UI
Imports Profile
Imports WebAppLog

Public Class Login1
    Inherits PageBase
    Public Overrides Property MustAuthenticate As Boolean = False
    Private WithEvents rcb As DropDownList
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = Me.GetType().Name.ToString()



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

    Public Sub LoadLanguageCombobox()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim dtData = rep.GetOtherList("SYSTEM_LANGUAGE")
            rcb = New DropDownList
            rcb.DataTextField = "NAME"
            rcb.DataValueField = "CODE"
            rcb.DataSource = dtData
            rcb.DataBind()
            If rcb.Items.Count > 0 Then
                rcb.SelectedValue = Common.Common.SystemLanguage.Name
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Public Sub LoadMaintain()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Dim notyfi = rep.GET_SYSTEM_MAINTAIN_IS_END()
            Dim lblNoti_Maintain As Label = LoginUser.FindControl("Noti_Maintain")
            lblNoti_Maintain.Text = notyfi
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Public Sub OnLoggingIn(ByVal sender As Object, ByVal e As LoginCancelEventArgs) Handles LoginUser.LoggingIn
        Dim txtUsername As TextBox = LoginUser.FindControl("Username")

        txtUsername.Text = txtUsername.Text.Trim
    End Sub

    Public Sub OnAuthenticate(ByVal sender As Object, ByVal e As AuthenticateEventArgs) Handles LoginUser.Authenticate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim membershipProvider = Membership.Provider
            Dim txtUsername As TextBox = LoginUser.FindControl("Username")
            txtUsername.Text = txtUsername.Text.Trim
            Dim txtPassword As TextBox = LoginUser.FindControl("Password")
            Dim lblNotify As Label = LoginUser.FindControl("Noti_Maintain")
            Dim rep As New CommonRepository
            Dim chkMaintainIsEnd = rep.CHECK_SYSTEM_MAINTAIN_IS_END()
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
            If lblNotify.Text <> "" AndAlso chkMaintainIsEnd AndAlso txtUsername.Text.ToUpper.Trim <> "ADMIN" Then
                ShowMessage(lblNotify.Text, Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If hidAccept.Value <> "" Then
                HttpContext.Current.Session.Add("ACCEPT_LOGIN", CBool(hidAccept.Value))
            End If
            e.Authenticated = membershipProvider.ValidateUser(txtUsername.Text, txtPassword.Text)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Select Case ex.Message
                Case LoginError.LDAP_SERVER_NOT_FOUND
                    Utilities.ShowMessage(LoginUser, Me.Translate("LDAP Server chưa được config"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "LDAP Server chưa được config")
                Case LoginError.NO_PERMISSION
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản chưa được phân quyền"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản chưa được phân quyền")
                Case LoginError.USERNAME_EXPIRED
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đã hết hạn"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản đã hết hạn")
                Case LoginError.USERNAME_NOT_EXISTS
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản không tồn tại"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản không tồn tại")
                Case LoginError.WRONG_PASSWORD
                    Dim Username As TextBox = LoginUser.FindControl("Username")
                    If Username.Text.Trim <> "ADMIN" Then
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
                    Else
                        Utilities.ShowMessage(LoginUser, Me.Translate("Mật khẩu không chính xác."), Utilities.NotifyType.Warning)
                    End If
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Mật khẩu không chính xác")
                Case LoginError.WRONG_USERNAME_OR_PASSWORD
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản hoặc mật khẩu không chính xác"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản hoặc mật khẩu không chính xác")
                Case LoginError.USER_LOCKED
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đã bị khóa. <br/> Vui lòng liên hệ Nhân sự để được mở khóa tài khoản và cấp lại mật khẩu"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản đã bị khóa")
                Case LoginError.EMPLOYEE_WORK_STATUS
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản của nhân viên đã nghỉ việc"), Utilities.NotifyType.Warning)
                    _mylog.WriteLog(_mylog._warning, _classPath, method, 0, Nothing, "Tài khoản của nhân viên đã nghỉ việc")
                Case LoginError.LOGINED
                    Utilities.ShowMessage(LoginUser, Me.Translate("Tài khoản đang login trên một thiết bị khác, bạn có tiếp tục đăng nhập"), Utilities.NotifyType.Multi_Login)
                Case Else
                    DisplayException(Me.Title, "", ex)
                    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Select
        End Try
    End Sub

    Protected Sub LoggedIn(ByVal sender As Object, ByVal e As EventArgs) Handles LoginUser.LoggedIn
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim chBox As CheckBox = DirectCast(LoginUser.FindControl("RememberMe"), CheckBox)

            If chBox.Checked Then
                Dim myCookie As New HttpCookie("myCookieApp")
                'Instance the new cookie
                Response.Cookies.Remove("myCookieApp")
                'Remove previous cookie
                Response.Cookies.Add(myCookie)
                'Create the new cookie
                myCookie.Values.Add("user", Me.LoginUser.UserName)
                'Add the username field to the cookie
                Dim deathDate As DateTime = DateTime.Now.AddDays(15)
                'Days of life
                Response.Cookies("myCookieApp").Expires = deathDate
                'Assign the life period
                'IF YOU WANT SAVE THE PASSWORD TOO (IT IS NOT RECOMMENDED)
                'myCookie.Values.Add("pass", Me.LoginUser.Password)

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try


    End Sub

    Public Overrides Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")
        Common.Common.DisplayException(Me, ex, "")
    End Sub

    Private Sub rcb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcb.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Common.Common.SystemLanguage = System.Globalization.CultureInfo.CreateSpecificCulture(rcb.SelectedValue)
            Response.Redirect(Request.Url.AbsoluteUri)
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.Title, "", ex)

        End Try

    End Sub

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.Title = Translate("Đăng nhập")

            If LogHelper.OnlineUsers IsNot Nothing Then
                Dim ActiveCount As Integer = 0
                For Each item In LogHelper.OnlineUsers
                    If item.Value IsNot Nothing AndAlso item.Value.Status = "ACTIVE" Then
                        ActiveCount += 1
                    End If
                Next

            End If
            rcb = LoginUser.FindControl("rcbLanguage")

            If Not IsPostBack Then
                LoadLanguageCombobox()
                LoadMaintain()
                Try
                    If Request.Cookies("myCookieApp") IsNot Nothing Then
                        Dim cookie As HttpCookie = Request.Cookies.Get("myCookieApp")
                        Dim user As String = cookie.Values("user").ToString()
                        'Dim pass As String = cookie.Values("pass").ToString()
                        If user.Trim <> "" Then
                            LoginUser.UserName = user
                        End If
                    End If
                Catch ex As Exception
                    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                End Try

            End If

            Me.DataBind()
            Dim txtUsername As TextBox = LoginUser.FindControl("UserName")
            txtUsername.Text = txtUsername.Text.Trim
            txtUsername.Focus()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.Title, "", ex)
        End Try
    End Sub


End Class