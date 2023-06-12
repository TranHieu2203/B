Imports System.Threading
Imports AttendanceDAL

Namespace AttendanceBusiness.BackgroundProcess
    Public Class AttendanceBusinessBackgroundProcess

        Private WithEvents timer As Timers.Timer
        Private _interval As Integer = 60000 '1min
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
        ''' <summary>
        ''' Get data IN/OUT in machine 
        ''' </summary>
        ''' <returns></returns>
        Public Function ProcessInOutData() As Boolean
            Return AttendanceRepositoryStatic.Instance.ProcessInOutData()
        End Function

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles timer.Elapsed
            'Import file quẹt thẻ / vân tay
            'ThreadPool.QueueUserWorkItem(AddressOf AttendanceRepositoryStatic.Instance.ProcessInOutData, Now)
        End Sub
    End Class
End Namespace