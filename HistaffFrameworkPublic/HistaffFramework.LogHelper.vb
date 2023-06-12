Option Strict On
Option Explicit On

Public Class UserLogHelper

#Region "1. Properties"
    Public Shared Property CurrentLogUser As CurrentUserLogDTO
        Get
            Return CType(HttpContext.Current.Session("CurrentLogUser"), CurrentUserLogDTO)
        End Get
        Set(ByVal value As CurrentUserLogDTO)
            HttpContext.Current.Session("CurrentLogUser") = value
        End Set
    End Property
#End Region

#Region "2. Function"
    ''' <summary>
    ''' Get UserName của User login
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUsername() As String
        Try
            Return CurrentLogUser.USERNAME.ToUpper()
        Catch ex As Exception

        End Try
        Return String.Empty
    End Function

    ''' <summary>
    ''' Get thông tin ghi log của User đang đăng nhập
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCurrentLogUser() As CurrentUserLog
        Try
            Return New CurrentUserLog With {.Username = CurrentLogUser.USERNAME.ToUpper(),
                                    .Fullname = CurrentLogUser.FULLNAME,
                                    .Email = CurrentLogUser.EMAIL,
                                    .ComputerName = System.Security.Principal.WindowsIdentity.GetCurrent.Name,
                                    .Ip = HttpContext.Current.Request.UserHostAddress}
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
#End Region
End Class

''' <summary>
''' 1 số Thông tin user lấy từ Database
''' </summary>
''' <remarks></remarks>
Public Class CurrentUserLogDTO
    Public Property ID As Decimal
    Public Property USERNAME As String
    Public Property FULLNAME As String
    Public Property EMAIL As String
End Class

''' <summary>
''' Thông user hiện tại đang đăng nhập
''' </summary>
''' <remarks></remarks>
Public Class CurrentUserLog
    Public Username As String
    Public Fullname As String
    Public Email As String
    Public Ip As String
    Public ComputerName As String
End Class



