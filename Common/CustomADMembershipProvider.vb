Imports Framework.UI

Public Class CustomADMembershipProvider
    Inherits ActiveDirectoryMembershipProvider
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
                ElseIf encry.DecryptString(user.PASSWORD) <> oldPassword Then
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

    Public Overrides Sub Initialize(name As String, config As System.Collections.Specialized.NameValueCollection)
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
            Dim rep As New CommonRepository


            Dim user = rep.GetUserWithPermision(username)
            Using encry As New EncryptData
                If user Is Nothing Then
                    Throw New Exception(LoginError.USERNAME_NOT_EXISTS)
                End If
                If user.IS_AD Then
                    bSuccess = MyBase.ValidateUser(username & domainName, password)
                    If Not bSuccess Then Throw New Exception(LoginError.WRONG_USERNAME_OR_PASSWORD)
                ElseIf user.ACTFLG = "I" Then
                    Throw New Exception(LoginError.USER_LOCKED)
                ElseIf encry.DecryptString(user.PASSWORD) = password Then
                    If Not user.IS_USER_PERMISSION Then
                        Throw New Exception(LoginError.NO_PERMISSION)
                    ElseIf user.EFFECT_DATE > Date.Now OrElse (user.EXPIRE_DATE IsNot Nothing AndAlso user.EXPIRE_DATE <= Date.Now) Then
                        Throw New Exception(LoginError.USERNAME_EXPIRED)
                    Else
                        bSuccess = True
                    End If
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
                LogHelper.UpdateOnlineUser(user, "Main")
                LogHelper.CurrentUser = user
            ElseIf bSuccess Then
                Throw New Exception(LoginError.NO_PERMISSION)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return bSuccess
    End Function
End Class


