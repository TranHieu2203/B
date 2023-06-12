Imports System.Configuration
Imports System.Threading
Imports CommonDAL

Namespace CommonBusiness.BackgroundProcess

    Public Class CommonBusinessBackgroundProcess

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

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles timer.Elapsed
            Using commonRep As New CommonRepository
                ''ThreadPool.QueueUserWorkItem(AddressOf commonRep.SendMail, Now)

                Dim isRelease As String = ConfigurationManager.AppSettings("ReleaseEvn")
                If isRelease = "Release" Then
                    ThreadPool.QueueUserWorkItem(AddressOf commonRep.CheckAndSendEventMail, Now)
                End If
            End Using
        End Sub

    End Class
End Namespace
