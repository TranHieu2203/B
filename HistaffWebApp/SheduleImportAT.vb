Imports System.IO
Imports System.Net.Http
Imports Aspose.Cells
Imports Attendance
Imports HistaffFrameworkPublic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Quartz
Imports Quartz.Impl

Public Class JobSheduleImportAT
    Implements IJob

    Public Sub Execute(ByVal context As IJobExecutionContext) Implements IJob.Execute
        Execute()
    End Sub
    Public Sub Execute()
        Dim fIn As String = System.Configuration.ConfigurationManager.AppSettings("FileIn").ToString()
        Dim fOut As String = System.Configuration.ConfigurationManager.AppSettings("FileOut").ToString()
        Dim pass = System.Configuration.ConfigurationManager.AppSettings("FileInPassword")
        Dim streamWriter As StreamWriter = New StreamWriter(System.IO.Path.Combine(fIn, "log.txt"), True)
        streamWriter.WriteLine("Run at: " + DateTime.Now.ToString)

        Dim rep As New Profile.ProfileStoreProcedure
        Dim allFile = Directory.EnumerateFiles(fIn, "*.*", SearchOption.AllDirectories).Where(Function(w) w.EndsWith(".xls") Or w.EndsWith("xlsx"))
        For Each file As String In allFile
            Try
                Dim dsDataPrepare As New DataSet
                Dim options As New Aspose.Cells.LoadOptions
                If Not String.IsNullOrWhiteSpace(pass) Then
                    options.Password = pass
                End If
                Dim workbook = New Aspose.Cells.Workbook(file)
                Dim worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.GetLastDataRow(0) + 1, 3, True))
                dsDataPrepare.Tables(0).TableName = "DATA"
                dsDataPrepare.Tables(0).Columns(0).ColumnName = "ID"
                dsDataPrepare.Tables(0).Columns(2).ColumnName = "VAL"
                dsDataPrepare.Tables(0).Columns.RemoveAt(1)
                Dim strXML As String = dsDataPrepare.GetXml()

                'If rep.IMPORT_SWIPE_DATA_AUTO(strXML) Then
                '    My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Success", Path.GetFileName(file)), True)
                'Else
                '    My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Error", Path.GetFileName(file)), True)
                'End If

            Catch ex As Exception
                streamWriter.WriteLine("Error: " + ex.Message)
                My.Computer.FileSystem.MoveFile(file, System.IO.Path.Combine(fOut, "Error", Path.GetFileName(file)), True)
                'streamWriter.Close()
                Continue For
            End Try
        Next
        streamWriter.Close()
    End Sub
End Class

Public Class JobSheduleGetInOut
    Implements IJob

    Public Sub Execute(ByVal context As IJobExecutionContext) Implements IJob.Execute
        Execute()
    End Sub
    Public Sub Execute()

        Dim _uri As String = System.Configuration.ConfigurationManager.AppSettings("Uri").ToString()
        Dim _api As String = System.Configuration.ConfigurationManager.AppSettings("ApiUrl").ToString()

        Dim fromDate = DateTime.Now.AddDays(-1)
        Dim toDate = DateTime.Now
        Dim strFromDate As String = fromDate.Year.ToString() + "-" + IIf(fromDate.Month > 9, fromDate.Month.ToString(), "0" + fromDate.Month.ToString()) + "-" + IIf(fromDate.Day > 9, fromDate.Day.ToString(), "0" + fromDate.Day.ToString())
        Dim strToDate As String = toDate.Year.ToString() + "-" + IIf(toDate.Month > 9, toDate.Month.ToString(), "0" + toDate.Month.ToString()) + "-" + IIf(toDate.Day > 9, toDate.Day.ToString(), "0" + toDate.Day.ToString())

        _api = _api & "?fromDate=" & strFromDate & "&toDate=" & strToDate
        Dim strResult = GetInOutData(_uri, _api)
        Dim dJson = JObject.Parse(strResult)
        Dim jData = dJson.Properties().Where(Function(f) f.Name = "responseData").FirstOrDefault()
        Dim dtData As New DataTable
        dtData = JsonConvert.DeserializeObject(Of DataTable)(jData.Value.ToString)
        'Dim dtFilter = dtData.AsEnumerable().Where(Function(f) f.Field(Of DateTime)("CheckTime") >= fromDate AndAlso f.Field(Of DateTime)("CheckTime") <= toDate).CopyToDataTable()
        'Dim dtFilter = dtData.Select("CheckTime >=#" + fromDate.ToString("MM/dd/yyyy HH:mm:ss") + "# AND CheckTime <=#" + toDate.ToString("MM/dd/yyyy HH:mm:ss") + "#").CopyToDataTable
        dtData.TableName = "Data"
        Dim writer As New System.IO.StringWriter()
        dtData.WriteXml(writer, XmlWriteMode.IgnoreSchema, False)
        Dim docXML = writer.ToString()
        Dim rep As New AttendanceRepository
        Dim result = rep.IMPORT_INOUT_AUTO(docXML, fromDate, toDate)
        'Tambt BCG-908 23/12/2022 Tự động tổng hợp công sau khi get data inout
        If result Then
            CalTimesheetMachinet()
        End If
    End Sub

    Private Function GetInOutData(ByVal uri As String, ByVal api As String) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim client As New HttpClient()
            client.BaseAddress = New Uri(uri)
            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("*/*"))
            client.DefaultRequestHeaders.Add("api-token", "23058010-751a-48a1-9242-ece7f3a85d05")

            Dim res = client.GetAsync(api).Result.Content.ReadAsStringAsync.Result
            Return res
        Catch ex As Exception
        End Try
    End Function

    Private Sub CalTimesheetMachinet()
        Dim rep As New AttendanceRepository
        Dim repH As New HistaffFrameworkRepository
        Dim programID As Decimal
        Dim lstObjEmp = rep.Load_Emp_obj()
        Dim obj As Object = repH.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {"CAL_TIME_TIMESHEET", FrameworkUtilities.OUT_STRING}))
        programID = Int32.Parse(obj(0).ToString())
        Dim dtParam = (New Common.CommonProgramsRepository).GetAllParameters(programID)
        Dim dtSequence = dtParam.DefaultView.ToTable(True, "SEQUENCE")
        For Each item In lstObjEmp
            Dim periodObj = rep.GetperiodByEmpObj(item.ID, DateTime.Now)
            If periodObj IsNot Nothing Then
                Dim lstParameter = New List(Of Common.PARAMETER_DTO)
                Dim newParameter As New Common.PARAMETER_DTO
                Dim value As String = ""
                newParameter.PARAMETER_NAME = "User name"
                newParameter.SEQUENCE = dtSequence.Rows(0)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "USERNAME"
                value = "ADMIN"
                newParameter.VALUE = value
                lstParameter.Add(newParameter)

                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "ORG_ID"
                newParameter.SEQUENCE = dtSequence.Rows(1)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "ORG_ID"
                'value = Decimal.Parse(ctrlOrganization.CurrentValue)
                newParameter.VALUE = 1
                lstParameter.Add(newParameter)
                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "PERIOD_ID"
                newParameter.SEQUENCE = dtSequence.Rows(2)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "PERIOD_ID"
                'value = Decimal.Parse(ctrlOrganization.CurrentValue)
                newParameter.VALUE = periodObj.ID
                lstParameter.Add(newParameter)

                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "ISDISSOLVE"
                newParameter.SEQUENCE = dtSequence.Rows(3)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "ISDISSOLVE"
                newParameter.VALUE = 0
                lstParameter.Add(newParameter)

                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "DELETE_ALL"
                newParameter.SEQUENCE = dtSequence.Rows(4)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 0
                newParameter.CODE = "DELETE_ALL"
                newParameter.VALUE = 1
                lstParameter.Add(newParameter)

                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "OBJ_EMP_ID"
                newParameter.SEQUENCE = dtSequence.Rows(5)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "OBJ_EMP_ID"
                newParameter.VALUE = item.ID
                lstParameter.Add(newParameter)
                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "FROM_DATE"
                newParameter.SEQUENCE = dtSequence.Rows(6)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "FROM_DATE"
                Dim vDate As Date = periodObj.START_DATE
                newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
                lstParameter.Add(newParameter)
                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO
                'Dim value As String = ""
                newParameter.PARAMETER_NAME = "TO_DATE"
                newParameter.SEQUENCE = dtSequence.Rows(7)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 1
                newParameter.CODE = "TO_DATE"
                vDate = periodObj.END_DATE
                newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
                lstParameter.Add(newParameter)

                newParameter = Nothing
                newParameter = New Common.PARAMETER_DTO()
                newParameter.PARAMETER_NAME = "EMPLIST"
                newParameter.SEQUENCE = dtSequence.Rows(8)("SEQUENCE")
                newParameter.REPORT_FIELD = ""
                newParameter.IS_REQUIRE = 0
                newParameter.CODE = "EMPLIST"
                newParameter.VALUE = ""
                lstParameter.Add(newParameter)
                GetAllInformationInRequestMain(lstParameter, programID)
            End If
        Next
    End Sub

    Private Shared Function CreateParameterList(Of T)(ByVal parameters As T) As List(Of List(Of Object))
        Dim lstParameter As New List(Of List(Of Object))
        For Each info As System.Reflection.PropertyInfo In parameters.GetType().GetProperties()
            Dim param As New List(Of Object)
            param.Add(info.Name)
            param.Add(info.GetValue(parameters, Nothing))
            lstParameter.Add(param)
        Next
        Return lstParameter
    End Function

    Private Sub GetAllInformationInRequestMain(ByVal lstParams As List(Of Common.PARAMETER_DTO), ByVal programID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim repH As New HistaffFrameworkRepository
            Dim newRequest As New Common.REQUEST_DTO
            Dim index As Decimal = 0
            newRequest.PROGRAM_ID = programID
            newRequest.PHASE_CODE = "I"
            newRequest.STATUS_CODE = "Initial"
            newRequest.START_DATE = DateTime.Now
            newRequest.END_DATE = DateTime.Now.AddDays(100)
            newRequest.ACTUAL_START_DATE = DateTime.Now
            newRequest.ACTUAL_COMPLETE_DATE = DateTime.Now.AddDays(100)
            newRequest.CREATED_BY = "AUTOGET-INOUT"
            newRequest.CREATED_DATE = DateTime.Now
            newRequest.MODIFIED_BY = "AUTOGET-INOUT"
            newRequest.MODIFIED_DATE = DateTime.Now
            newRequest.CREATED_LOG = ""
            newRequest.MODIFIED_LOG = ""

            'Get program with programId
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = FrameworkUtilities.OUT_CURSOR})
            'get all Function(Report) in system
            Dim dt = repH.ExecuteStore("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_ID", lstlst)

            'get list parameter

            'call function insert into database with request and parameters
            Dim result = (New Common.CommonProgramsRepository).Insert_Requests(newRequest, dt, lstParams, 1)
        Catch ex As Exception
        End Try
    End Sub
End Class

Public Class SheduleImportAT

    Public Shared Sub Start()
        Dim cronStr As String = System.Configuration.ConfigurationManager.AppSettings("Cron").ToString()
        Dim scheduler As IScheduler = StdSchedulerFactory.GetDefaultScheduler
        scheduler.Start()
        Dim job As IJobDetail = JobBuilder.Create(Of JobSheduleImportAT).Build
        Dim trigger As ITrigger = TriggerBuilder.Create.WithIdentity("JobSheduleImportAT", "ImportAT").WithCronSchedule(cronStr).WithPriority(1).Build()
        scheduler.ScheduleJob(job, trigger)

        '' Auto GetInOut
        Dim inOutCronStr As String = System.Configuration.ConfigurationManager.AppSettings("InOutCron").ToString()
        Dim schedulerInOut As IScheduler = StdSchedulerFactory.GetDefaultScheduler
        schedulerInOut.Start()
        Dim jobInOut As IJobDetail = JobBuilder.Create(Of JobSheduleGetInOut).Build
        Dim triggerInOut As ITrigger = TriggerBuilder.Create.WithIdentity("JobSheduleGetInOut", "GetInOut").WithCronSchedule(inOutCronStr).WithPriority(1).Build()
        schedulerInOut.ScheduleJob(jobInOut, triggerInOut)
    End Sub
End Class
