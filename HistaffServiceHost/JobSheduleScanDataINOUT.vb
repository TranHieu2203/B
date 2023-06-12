Imports AttendanceBusiness.BackgroundProcess
Imports Quartz
Imports Quartz.Impl

Public Class JobSheduleScanDataINOUT
    Implements IJob
    Public Sub Execute(ByVal context As Quartz.IJobExecutionContext) Implements Quartz.IJob.Execute
        Dim objAttendaceBgProcess As New AttendanceBusinessBackgroundProcess
        Try
            objAttendaceBgProcess.ProcessInOutData()
        Catch ex As Exception
            Throw ex
        Finally
            objAttendaceBgProcess = Nothing
        End Try
    End Sub
End Class
Public Class SheduleScanDataInOut
    Public Shared Sub Start()
        Dim cronStr As String = System.Configuration.ConfigurationManager.AppSettings("Cron").ToString()
        Dim scheduler As IScheduler = StdSchedulerFactory.GetDefaultScheduler
        scheduler.Start()
        Dim job As IJobDetail = JobBuilder.Create(Of JobSheduleScanDataINOUT).Build
        Dim trigger As ITrigger = TriggerBuilder.Create.WithIdentity("JobSheduleScanDataINOUT", "ScanDataInOut").WithCronSchedule(cronStr).WithPriority(1).Build()
        scheduler.ScheduleJob(job, trigger)
    End Sub
End Class
