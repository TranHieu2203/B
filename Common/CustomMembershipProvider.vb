Imports Common.CommonBusiness
Imports Framework.UI
Imports HistaffFrameworkPublic

Public Class CustomMembershipProvider
    Inherits MembershipProvider
    Private _error As LoginError
    Public ReadOnly Property GetError As LoginError
        Get
            Return _error
        End Get
    End Property
    Public ReadOnly Property LoginFailCount(ByVal username As String)
        Get
            If HttpContext.Current.Session(username & "_LoginFailCount") Is Nothing Then
                Return 0
            End If
            Return HttpContext.Current.Session(username & "_LoginFailCount")
        End Get
    End Property
    Private Sub SetLoginFailCount(ByVal username As String, ByVal value As Integer)
        HttpContext.Current.Session(username & "_LoginFailCount") = value
    End Sub

    Public Property domainName As String = ""

    Public Overrides Function ChangePassword(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
        Try
            Dim rep As New CommonRepository
            Dim user = rep.GetUserWithPermision(username)
            Using encry As New EncryptData
                If user Is Nothing Then
                    Return False
                ElseIf user.IS_AD Then
                    Return False
                ElseIf user.PASSWORD = encry.EncryptString(newPassword) Then
                    Return False
                Else
                    Return rep.ChangeUserPassword(username, oldPassword, newPassword)
                End If
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function




    Public Overrides Function UnlockUser(ByVal username As String) As Boolean
        Try
            Dim rep As New CommonRepository
            Return rep.UpdateUserStatus(username, "A")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function LockUser(ByVal username As String) As Boolean
        Try
            Dim rep As New CommonRepository
            Return rep.UpdateUserStatus(username, "I")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub Initialize(ByVal name As String, ByVal config As System.Collections.Specialized.NameValueCollection)
        Try
            If config.AllKeys.Contains("domainName") Then
                domainName = config("domainName")
                config.Remove("domainName")
            End If
            MyBase.Initialize(name, config)
        Catch ex As Exception
            Throw New Exception(LoginError.LDAP_SERVER_NOT_FOUND)
        End Try
    End Sub

    Public Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
        'if User dont have account return false
        Dim bSuccess As Boolean = False
        Try
            Using rep As New CommonRepository
                Dim user = rep.GetUserWithPermision(username)
                Using encry As New EncryptData
                    If user Is Nothing Then
                        Throw New Exception(LoginError.USERNAME_NOT_EXISTS)
                    End If
                    If user.ACTFLG = "I" Then
                        Throw New Exception(LoginError.USER_LOCKED)
                    ElseIf user.IS_AD Then
                        Dim LdapCorrect As LdapDTO

                        'Dim email = username.Substring(username.IndexOf("@") + 1).ToLower
                        For Each item In CommonConfig.ListLDAP
                            'If item.DOMAIN_NAME.ToLower = email Then
                            '    LdapCorrect = item
                            '    Exit For
                            'End If
                            LdapCorrect = item
                            Exit For
                        Next
                        username = If(username.IndexOf("@") <> -1, username.Substring(0, username.IndexOf("@")), username)
                        If LdapCorrect IsNot Nothing Then

                            bSuccess = TVC.LDAP.ADManager.IsUserExists(LdapCorrect.LDAP_NAME,
                                                                       username.ToLower,
                                                                       password,
                                                                       LdapCorrect.BASE_DN)

                        Else
                            Throw New Exception(LoginError.LDAP_SERVER_NOT_FOUND)
                        End If
                        If Not bSuccess Then Throw New Exception(LoginError.WRONG_USERNAME_OR_PASSWORD)
                    ElseIf Not user.IS_USER_PERMISSION Then
                        Throw New Exception(LoginError.NO_PERMISSION)
                        'ElseIf user.ACTFLG = "I" Then
                        '    Throw New Exception(LoginError.USER_LOCKED)
                        'ElseIf user.WORK_STATUS = 257 Then
                        '    Throw New Exception(LoginError.EMPLOYEE_WORK_STATUS)
                    ElseIf user.PASSWORD = encry.EncryptString(password) Then

                        If user.EFFECT_DATE > Date.Now OrElse
                            (user.EXPIRE_DATE IsNot Nothing AndAlso
                             user.EXPIRE_DATE <= Date.Now) Then
                            Throw New Exception(LoginError.USERNAME_EXPIRED)
                        End If

                        bSuccess = True

                    Else
                        Dim bIsAdmin As Boolean = False
                        If user.MODULE_ADMIN <> "" Then
                            bIsAdmin = True
                        End If
                        If Not bIsAdmin Then
                            Dim maxFail As Integer
                            maxFail = CommonConfig.MaxNumberLoginFail
                            SetLoginFailCount(username, LoginFailCount(username) + 1)
                            If LoginFailCount(username) >= maxFail Then
                                LockUser(username)
                            End If
                        End If
                        Throw New Exception(LoginError.WRONG_PASSWORD)
                    End If
                End Using


                If bSuccess And user.IS_APP Then

                    'If LogHelper.OnlineUsers.Count > 0 AndAlso Not user.USERNAME.ToUpper.Equals("ADMIN") Then
                    '    Dim lstOnlnine = LogHelper.OnlineUsers
                    '    Dim rs = (From p In lstOnlnine.AsEnumerable Where p.Value.User.USERNAME.ToUpper = user.USERNAME.ToUpper Select p.Key).ToList

                    '    Dim accept_Login = If(Not IsNothing(HttpContext.Current.Session("ACCEPT_LOGIN")), CBool(HttpContext.Current.Session("ACCEPT_LOGIN")), False)
                    '    If Not accept_Login And rs.Count > 0 Then
                    '        HttpContext.Current.Session.Remove("ACCEPT_LOGIN")
                    '        Throw New Exception(LoginError.LOGINED)
                    '    End If

                    '    For Each item In rs
                    '        LogHelper.SaveAccessLog(item, "Timeout")
                    '        LogHelper.OnlineUsers.Remove(item)
                    '    Next
                    'End If

                    LogHelper.UpdateOnlineUser(user, "Main")
                    LogHelper.CurrentUser = user

                    'Update sử dụng HistaffFramework để lấy Curent User Login
                    Dim userLog As New CurrentUserLogDTO
                    userLog.ID = user.ID
                    userLog.USERNAME = user.USERNAME
                    userLog.FULLNAME = user.FULLNAME
                    userLog.EMAIL = user.EMAIL
                    UserLogHelper.CurrentLogUser = userLog

                ElseIf bSuccess Then
                    Throw New Exception(LoginError.NO_PERMISSION)
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return bSuccess
    End Function

    Public Overrides Property ApplicationName As String
        Get

            Throw New NotImplementedException
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Overrides Function ChangePasswordQuestionAndAnswer(ByVal username As String, ByVal password As String, ByVal newPasswordQuestion As String, ByVal newPasswordAnswer As String) As Boolean

        Throw New NotImplementedException
    End Function

    Public Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, ByVal providerUserKey As Object, ByRef status As System.Web.Security.MembershipCreateStatus) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overrides Function DeleteUser(ByVal username As String, ByVal deleteAllRelatedData As Boolean) As Boolean

        Throw New NotImplementedException
    End Function

    Public Overrides ReadOnly Property EnablePasswordReset As Boolean
        Get

            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property EnablePasswordRetrieval As Boolean
        Get

            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides Function FindUsersByEmail(ByVal emailToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function FindUsersByName(ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetAllUsers(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetNumberOfUsersOnline() As Integer

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetPassword(ByVal username As String, ByVal answer As String) As String
        Dim rep As New CommonRepository
        Return rep.GetPassword(username)
    End Function

    Public Overloads Overrides Function GetUser(ByVal providerUserKey As Object, ByVal userIsOnline As Boolean) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetUser(ByVal username As String, ByVal userIsOnline As Boolean) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetUserNameByEmail(ByVal email As String) As String

        Throw New NotImplementedException
    End Function

    Public Overrides ReadOnly Property MaxInvalidPasswordAttempts As Integer
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters As Integer
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredPasswordLength As Integer
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordAttemptWindow As Integer
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordFormat As System.Web.Security.MembershipPasswordFormat
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordStrengthRegularExpression As String
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresQuestionAndAnswer As Boolean
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresUniqueEmail As Boolean
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides Function ResetPassword(ByVal username As String, ByVal answer As String) As String
        Throw New NotImplementedException
    End Function

    Public Overrides Sub UpdateUser(ByVal user As System.Web.Security.MembershipUser)
        Throw New NotImplementedException
    End Sub
End Class


