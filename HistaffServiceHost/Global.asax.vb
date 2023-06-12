'Imports AttendanceBusiness.BackgroundProcess
Imports AttendanceBusiness.BackgroundProcess
Imports CommonBusiness.BackgroundProcess
Imports PayrollBusiness.BackgroundProcess
Imports ProfileBusiness.BackgroundProcess

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        SheduleScanDataInOut.Start()
        'Chạy Timer Schedule background cho Common
        Dim objCommonBgProcess As New CommonBusinessBackgroundProcess
        If IsNumeric(ConfigurationManager.AppSettings("SEBGPROCESSINTERVAL")) Then
            objCommonBgProcess.Interval = Convert.ToInt32(ConfigurationManager.AppSettings("SEBGPROCESSINTERVAL")) * 1000
        End If
        objCommonBgProcess.Start()

        'Chạy Timer Schedule background cho Profile
        Dim objProfileBgProcess As New ProfileBusinessBackgroundProcess
        If IsNumeric(ConfigurationManager.AppSettings("HUBGPROCESSINTERVAL")) Then
            objProfileBgProcess.Interval = Convert.ToInt32(ConfigurationManager.AppSettings("HUBGPROCESSINTERVAL")) * 1000
        End If
        objProfileBgProcess.Start()

        'Chạy Timer Schedule background cho Attendace
        Dim objAttendaceBgProcess As New AttendanceBusinessBackgroundProcess
        If IsNumeric(ConfigurationManager.AppSettings("ATBGPROCESSINTERVAL")) Then
            objAttendaceBgProcess.Interval = Convert.ToInt32(ConfigurationManager.AppSettings("ATBGPROCESSINTERVAL")) * 1000
        End If
        objAttendaceBgProcess.Start()

        'Chạy Timer Schedule background cho Attendace
        Dim objPayrollBgProcess As New PayrollBusinessBackgroundProcess
        If IsNumeric(ConfigurationManager.AppSettings("PABGPROCESSINTERVAL")) Then
            objPayrollBgProcess.Interval = Convert.ToInt32(ConfigurationManager.AppSettings("PABGPROCESSINTERVAL")) * 1000
        End If
        objPayrollBgProcess.Start()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class