Imports Common.CommonBusiness
Imports Framework.UI

Public Class LogHelper
    Public Shared Property OnlineUsers As Dictionary(Of String, OnlineUser)

    Public Shared Property CurrentUser As UserDTO
        Get
            Return HttpContext.Current.Session("CurrentUser")
        End Get
        Set(ByVal value As UserDTO)
            HttpContext.Current.Session("CurrentUser") = value
        End Set
    End Property

    Public Shared Property ImageUser As Byte()
        Get
            Return HttpContext.Current.Session("ImageUser")
        End Get
        Set(ByVal value As Byte())
            HttpContext.Current.Session("ImageUser") = value
        End Set
    End Property

    Public Shared Sub UpdateOnlineUser(ByVal user As UserDTO,
                                       ByVal currentApp As String)
        If OnlineUsers Is Nothing Then
            OnlineUsers = New Dictionary(Of String, OnlineUser)
        End If

        If user IsNot Nothing Then
            If Not OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) Then
                OnlineUsers.Add(HttpContext.Current.Session.SessionID,
                                        New OnlineUser With {.User = user,
                                                            .LoginDate = DateTime.Now,
                                                            .LastAccessDate = DateTime.Now,
                                                            .ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent.Name,
                                                             .CurrentApp = currentApp,
                                                            .IP = HttpContext.Current.Request.UserHostAddress})
            End If
        End If
    End Sub

    Public Shared Property ViewName As String
        Get
            Return HttpContext.Current.Session("ViewName")
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("ViewName") = value
        End Set
    End Property

    Public Shared Property ViewDescription As String
        Get
            Return HttpContext.Current.Session("ViewDescription")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("ViewDescription") = value
        End Set
    End Property

    Public Shared Property ViewGroup As String
        Get
            Return HttpContext.Current.Session("ViewGroup")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("ViewGroup") = value
        End Set
    End Property


    Public Shared Property ActionName As String
        Get
            Return HttpContext.Current.Session("ActionName")
        End Get

        Set(ByVal value As String)
            HttpContext.Current.Session("ActionName") = value
        End Set
    End Property

    Public Shared Sub UpdateAccessLog(ByVal view As ViewBase)
        Try
            If OnlineUsers IsNot Nothing _
                AndAlso OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) _
                AndAlso view.Allow Then

                ViewName = view.ViewName
                Dim user = OnlineUsers(HttpContext.Current.Session.SessionID)
                user.LastAccessDate = DateTime.Now
                If user.AccessFunctions Is Nothing Then
                    user.AccessFunctions = New Dictionary(Of String, String)
                End If
                If Not user.AccessFunctions.ContainsKey(view.ViewName) Then
                    user.AccessFunctions.Add(view.ViewName, view.ViewDescription)
                End If
                If view.ViewName.ToUpper <> "CTRLONLINEUSER" Then
                    user.CurrentViewName = view.ViewName
                    user.CurrentViewDesc = view.ViewDescription
                End If
                OnlineUsers(HttpContext.Current.Session.SessionID) = user
                UpdateAllOnlineUserStatus()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub UpdateAllOnlineUserStatus()
        For Each item In OnlineUsers
            If item.Value IsNot Nothing Then
                If item.Value.Status <> "KILLED" Then
                    If (DateTime.Now - item.Value.LastAccessDate).TotalMinutes > CommonConfig.ActiveTimeout Then
                        item.Value.Status = "INACTIVE"
                    Else
                        item.Value.Status = "ACTIVE"
                    End If
                End If
            End If
        Next
    End Sub


    Public Shared Sub Kill(ByVal SessionID As String)
        Try
            If OnlineUsers IsNot Nothing _
                AndAlso OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) Then
                OnlineUsers(SessionID).Status = "KILLED"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function GetSessionStatus(ByVal SessionID As String) As String
        Try
            If OnlineUsers IsNot Nothing _
                AndAlso OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) Then
                Return OnlineUsers(SessionID).Status
            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Public Shared Function GetSessionCurrentApp(ByVal SessionID As String) As String
        Try
            If OnlineUsers IsNot Nothing _
                AndAlso OnlineUsers.ContainsKey(HttpContext.Current.Session.SessionID) Then
                Return OnlineUsers(SessionID).CurrentApp
            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Public Shared Sub SaveAccessLog(ByVal SessionID As String, ByVal LoginStatus As String)
        Try
            If LogHelper.OnlineUsers IsNot Nothing AndAlso LogHelper.OnlineUsers.ContainsKey(SessionID) Then

                Dim user As OnlineUser = LogHelper.OnlineUsers(SessionID)
                Dim rep As New CommonRepository
                Dim accessLog As New AccessLog With {.Username = user.User.USERNAME,
                                                  .Fullname = user.User.FULLNAME,
                                                  .Email = user.User.EMAIL,
                                                  .Mobile = user.User.TELEPHONE,
                                                  .AccessFunctions = user.AccessFunctionsStr,
                                                  .LoginDate = user.LoginDate,
                                                  .LogoutDate = DateTime.Now,
                                                  .LogoutStatus = LoginStatus,
                                                  .IP = user.IP,
                                                  .ComputerName = user.ComputerName}
                'Update access log
                rep.InsertAccessLog(accessLog)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function GetUserLog() As CommonBusiness.UserLog
        Return New UserLog With {.Username = Common.GetUsername,
                                .Fullname = LogHelper.CurrentUser.FULLNAME,
                                .Email = LogHelper.CurrentUser.EMAIL,
                                .Mobile = LogHelper.CurrentUser.TELEPHONE,
                                .ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent.Name,
                                .Ip = HttpContext.Current.Request.UserHostAddress,
                                .ViewName = LogHelper.ViewName,
                                .ViewDescription = LogHelper.ViewDescription,
                                .ViewGroup = LogHelper.ViewGroup,
                                .ActionName = LogHelper.ActionName}
    End Function


End Class


Public Class OnlineUser
    Public Property User As UserDTO
    Public Property LoginDate As DateTime
    Public ReadOnly Property LoginTime As Integer
        Get
            Return (DateTime.Now - LoginDate).TotalMinutes
        End Get
    End Property

    Public Property LastAccessDate As DateTime

    Public ReadOnly Property NoAccessTime As Integer
        Get
            Return (DateTime.Now - LastAccessDate).TotalMinutes
        End Get
    End Property

    'ACTIVE, INACTIVE, KILLED
    Public Property Status As String = "ACTIVE"

    Public Property AccessFunctions As Dictionary(Of String, String)
    Public Property CurrentViewDesc As String
    Public Property CurrentViewName As String
    Public Property CurrentApp As String
    Public ReadOnly Property AccessFunctionsStr As String
        Get
            If AccessFunctions IsNot Nothing Then
                Dim lst As List(Of String) = (From p In AccessFunctions Select p.Value).ToList
                Return String.Join(";", lst)
            End If
            Return ""
        End Get
    End Property


    Public Property IP As String
    Public Property ComputerName As String
End Class



