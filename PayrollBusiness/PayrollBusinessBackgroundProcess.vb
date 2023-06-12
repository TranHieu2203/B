Imports System.Threading
Imports PayrollDAL

Namespace PayrollBusiness.BackgroundProcess
    Public Class PayrollBusinessBackgroundProcess

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

            ThreadPool.QueueUserWorkItem(AddressOf New PayrollRepository().CheckAndSendPayslip, Now)
            'ThreadPool.QueueUserWorkItem(AddressOf New PayrollRepository().CheckAndSendBonusSlip, Now)
            'ThreadPool.QueueUserWorkItem (AddressOf New PayrollRepository().CAL_DATA_IN
        End Sub

    End Class
End Namespace
