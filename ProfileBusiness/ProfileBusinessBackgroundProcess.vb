Imports System.Threading
Imports ProfileDAL

Namespace ProfileBusiness.BackgroundProcess
    Public Class ProfileBusinessBackgroundProcess

        Private WithEvents timer As Timers.Timer
        Private _interval As Integer = 60000 '1min
        Private _date As Date = Date.Now

        Public Property Interval As Integer
            Get
                Return _interval
            End Get
            Set(ByVal value As Integer)
                _interval = value
            End Set
        End Property

        Public Sub Start()

            timer = New Timers.Timer(_interval) '1min
            timer.Start()

        End Sub

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles timer.Elapsed

            ThreadPool.QueueUserWorkItem(AddressOf New ProfileRepository().CheckAndUpdateEmployeeInformation, Now)
            ThreadPool.QueueUserWorkItem(AddressOf New ProfileRepository().UpdateTrangThaiNghiViec, Now)
            ThreadPool.QueueUserWorkItem(AddressOf New ProfileRepository().CheckSendMailStockRemind, Now)
            ' gửi mail nhắc nhở hợp đồng hết hạn từ ngày 15 - 20 hàng tháng
            'If Format(_date, "dd") >= 1 And Format(_date, "dd") <= 29 Then
            'ThreadPool.QueueUserWorkItem(AddressOf New ProfileRepository().CheckAndSendMailReminder, Now)
            'End If

        End Sub

    End Class
End Namespace
