Imports Common.CommonBusiness
Imports Framework.UI

Public Class CustomMembershipProviderPortal
    Inherits MembershipProvider

    Private ReadOnly Property LoginFailCount(ByVal username As String)
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

    Public Overrides Function ChangePassword(username As String, oldPassword As String, newPassword As String) As Boolean
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

    Public Overrides Function UnlockUser(username As String) As Boolean
        Try
            Dim rep As New CommonRepository
            Return rep.UpdateUserStatus(username, "A")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function LockUser(username As String) As Boolean
        Try
            Dim rep As New CommonRepository
            Return rep.UpdateUserStatus(username, "I")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
        'if User dont have account return false
        Dim bSuccess As Boolean = False
        Try
            Dim rep As New CommonRepository
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
                    'ElseIf user.ACTFLG = "I" Then
                    '    Throw New Exception(LoginError.USER_LOCKED)
                    'ElseIf user.WORK_STATUS = 257 Then
                    '    Throw New Exception(LoginError.EMPLOYEE_WORK_STATUS)
                ElseIf user.PASSWORD = encry.EncryptString(password) Then
                    If user.EFFECT_DATE > Date.Now OrElse (user.EXPIRE_DATE IsNot Nothing AndAlso user.EXPIRE_DATE <= Date.Now) Then
                        Throw New Exception(LoginError.USERNAME_EXPIRED)
                    Else
                        If user.EMPLOYEE_ID Is Nothing OrElse user.EMPLOYEE_ID = 0 Then
                            Throw New Exception(LoginError.USER_NOT_EMPLOYEE)
                        End If
                        bSuccess = True
                    End If
                Else
                    Dim maxFail As Integer
                    maxFail = CommonConfig.MaxNumberLoginFail
                    SetLoginFailCount(username, LoginFailCount(username) + 1)
                    If LoginFailCount(username) > maxFail Then
                        LockUser(username)
                    End If
                    Throw New Exception(LoginError.WRONG_PASSWORD)
                End If
            End Using

            If bSuccess And user.IS_PORTAL Then
                LogHelper.UpdateOnlineUser(user, "Portal")
                LogHelper.CurrentUser = user
            Else
                Throw New Exception(LoginError.NO_PERMISSION)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return bSuccess
    End Function

    Public Overrides Property ApplicationName As String
        Get

            Throw New NotImplementedException
        End Get
        Set(value As String)

        End Set
    End Property

    Public Overrides Function ChangePasswordQuestionAndAnswer(username As String, password As String, newPasswordQuestion As String, newPasswordAnswer As String) As Boolean

        Throw New NotImplementedException
    End Function

    Public Overrides Function CreateUser(username As String, password As String, email As String, passwordQuestion As String, passwordAnswer As String, isApproved As Boolean, providerUserKey As Object, ByRef status As System.Web.Security.MembershipCreateStatus) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overrides Function DeleteUser(username As String, deleteAllRelatedData As Boolean) As Boolean

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

    Public Overrides Function FindUsersByEmail(emailToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function FindUsersByName(usernameToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetAllUsers(pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As System.Web.Security.MembershipUserCollection

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetNumberOfUsersOnline() As Integer

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetPassword(username As String, answer As String) As String
        Dim rep As New CommonRepository
        Return rep.GetPassword(username)
    End Function

    Public Overloads Overrides Function GetUser(providerUserKey As Object, userIsOnline As Boolean) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetUser(username As String, userIsOnline As Boolean) As System.Web.Security.MembershipUser

        Throw New NotImplementedException
    End Function

    Public Overrides Function GetUserNameByEmail(email As String) As String

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

    Public Overrides Function ResetPassword(username As String, answer As String) As String
        Throw New NotImplementedException
    End Function

    Public Overrides Sub UpdateUser(user As System.Web.Security.MembershipUser)
        Throw New NotImplementedException
    End Sub
End Class