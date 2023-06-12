Imports System.Configuration
Imports System.Data.Objects
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.DataAccess
Imports Framework.Data.System.Linq.Dynamic
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Oracle.DataAccess.Client
Imports WebAppLog
Partial Public Class AttendanceRepository
#Region "Integate InOut"
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "AttendanceRepository.Business.vb"
    Private Shared g_strSql As String = "Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};User ID={2};Password={3};"
    Public Function ProcessInOutData() As Boolean
        Dim TimeStr As String = ConfigurationManager.AppSettings("Cron")
        If TimeStr = "" Then Return False
        Dim timesync As Date = Now()
        Dim timesyncPre As Date = DateAdd(DateInterval.Day, -1, timesync)
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ReadCheckInOutData(timesyncPre, timesync) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, "",
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), ex, "")

            Throw ex
        End Try
    End Function
    Public Function ReadCheckInOutData(ByVal dateFrom As Date, ByVal dateTo As Date, Optional ByVal TerId As Decimal = -1) As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim strFromDate As String
        Dim strEndDate As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim ds As DataSet
            Dim g_ConStringSql As String
            strFromDate = dateFrom.Year.ToString + IIf(dateFrom.Month >= 10, dateFrom.Month.ToString(), "0" + dateFrom.Month.ToString()) + IIf(dateFrom.Day >= 10, dateFrom.Day.ToString, "0" + dateFrom.Day.ToString())
            strEndDate = dateTo.Year.ToString + IIf(dateTo.Month >= 10, dateTo.Month.ToString(), "0" + dateTo.Month.ToString()) + IIf(dateTo.Day >= 10, dateTo.Day.ToString, "0" + dateTo.Day.ToString())
            Dim query = (From p In Context.AT_TERMINALS.Where(Function(f) f.ID = TerId Or TerId = -1) Select p).ToList()
            Dim strSql As String
            For Each Item In query
                g_ConStringSql = String.Format(g_strSql, Item.TERMINAL_IP, Item.DATABASE_NAME, Item.USERNAME, Item.PASS)
                strSql = String.Format(Item.QUERY_STRING, strFromDate, strEndDate)
                _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
            , Nothing, "Info connect and query" + strSql + Chr(13) + g_ConStringSql)
                ds = SQLHelper.ExecuteQuery(strSql, g_ConStringSql)
                _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
                        , Nothing, "iTime.ReadCheckInOutData " + IIf(ds Is Nothing, -99, 1).ToString)
                If Not ds Is Nothing Then
                    _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
                        , Nothing, "iTime.ReadCheckInOutData " + IIf(ds.Tables Is Nothing, -88, 2).ToString)
                    If ds.Tables.Count = 0 Then Return False
                    Dim sw As New StringWriter()
                    Dim DocXml As String = String.Empty
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    Try
                        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
                        , Nothing, "iTime.ReadCheckInOutData " + Chr(13) + IIf(ds.Tables(0).Rows.Count > 0, 3, -77).ToString)
                        Dim check As Boolean = ImportDataINOUT(DocXml, dateFrom, dateTo, TerId)
                        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
                        , Nothing, "iTime.ReadCheckInOutData " + Chr(13) + IIf(check, 4, -66).ToString)
                        'Return True
                    Catch ex As Exception
                        Continue For
                    End Try
                Else
                    Continue For
                End If
            Next
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
                        , Nothing, "iTime.ReadCheckInOutData || ex:" + Chr(13) + ex.ToString)
            Return False
        End Try
    End Function
    Public Function ReadCheckInOutData_CheckOUT(ByVal _lst_Terminal As AT_TERMINALSDTO, ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim strFromDate As String
        Dim strEndDate As String
        Try
            Dim ds As DataSet
            Dim g_ConStringSql As String
            strFromDate = dateFrom.Year.ToString + IIf(dateFrom.Month >= 10, dateFrom.Month.ToString(), "0" + dateFrom.Month.ToString()) + IIf(dateFrom.Day >= 10, dateFrom.Day.ToString, "0" + dateFrom.Day.ToString())
            strEndDate = dateTo.Year.ToString + IIf(dateTo.Month >= 10, dateTo.Month.ToString(), "0" + dateTo.Month.ToString()) + IIf(dateTo.Day >= 10, dateTo.Day.ToString, "0" + dateTo.Day.ToString())

            g_ConStringSql = SQLHelper.LoadConfig_CheckOut("sc", _lst_Terminal.PASS, "BIOSTAR-HCM", _lst_Terminal.TERMINAL_IP)

            Dim strSql As String = String.Format(SQLHelper.GetSQLText("CHECKINOUT"), strFromDate, strEndDate)
            ds = SQLHelper.ExecuteQuery(strSql, g_ConStringSql)
            If Not ds Is Nothing Then
                If ds.Tables.Count = 0 Then Return False
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                ds.Tables(0).WriteXml(sw, False)
                DocXml = sw.ToString
                Try
                    WriteExceptionLog(Nothing, MethodBase.GetCurrentMethod.Name, "iTime.ReadCheckInOutData strFromDate: " + strFromDate + "|| strEndDate: " + strEndDate)
                    ImportDataINOUT_CHECKOUT(DocXml, _lst_Terminal.ID, dateFrom, dateTo)
                    Return True
                Catch ex As Exception
                    Return True
                End Try
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime.ReadCheckInOutData strFromDate: " + strFromDate + "|| strEndDate: " + strEndDate)
            Throw ex

        End Try
    End Function
    Private Function ImportDataINOUT(ByVal DataXML As String, ByVal FROM_DATE As Date, ByVal END_TODATE As Date, ByVal TerId As Decimal)
        Try
            Using cls As New DataAccess.QueryData
                Dim obj_data As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_INOUT",
                                           New With {.P_DATA = DataXML,
                                                     .P_USER = "AUTO",
                                                     .P_TER_ID = TerId,
                                                     .P_FROM_DATE = FROM_DATE,
                                                     .P_END_DATE = END_TODATE,
                                                     .P_OUT = cls.OUT_CURSOR
                                                     })
                Return CBool(obj_data(0)(0))

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function ImportDataINOUT_CHECKOUT(ByVal DataXML As String, ByVal intTERMINAL_ID As Integer, ByVal FROM_DATE As Date, ByVal END_TODATE As Date)
        Try
            Using cls As New DataAccess.QueryData
                Dim obj_data As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_INOUT_CHECKINOUT",
                                           New With {.P_DATA = DataXML,
                                                     .P_TERMINAL_ID = intTERMINAL_ID,
                                                     .P_USER = "AUTO",
                                                     .P_FROM_DATE = FROM_DATE,
                                                     .P_END_DATE = END_TODATE,
                .P_OUT = cls.OUT_CURSOR
                                                     })
                Return CBool(obj_data(0)(0))

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
    Dim ls_AT_SWIPE_DATADTO As New List(Of AT_SWIPE_DATADTO)
#Region "CONFIG TEMPLATE "
    Public Function GET_CONFIG_TEMPLATE(ByVal MACHINE_TYPE As Decimal?) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_AT_LIST.GET_CONFIG_TEMPLATE",
                                           New With {.P_MACHINE_TYPE = MACHINE_TYPE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

    Public Function ToDate(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return DateTime.ParseExact(item, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
        End If
    End Function
    Public Function ToDecimal(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return CDec(item)
        End If
    End Function

    Public Function IMPORT_AT_SWIPE_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_AT_SWIPE_DATA",
                                               New With {.P_USER = log.Username.ToUpper,
                                                         .P_DATA = DATA_IN,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GET_SWIPE_DATA_IMPORT(ByVal _orgid As Decimal, ByVal _is_dissolove As Boolean, ByVal log As UserLog) As DataSet
        Dim dsData As New DataSet
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SWIPE_DATA_IMPORT",
                                               New With {.P_USER = log.Username.ToUpper,
                                                         .P_ORG_ID = _orgid,
                                                         .P_IS_DISSOLOVE = _is_dissolove,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR}, False)
                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_AT_WORKSIGN_EDIT(ByVal empId As Decimal, ByVal startDate As Date, ByVal EndDate As Date) As DataTable
        Dim dsData As New DataTable
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_AT_WORKSIGN_EDIT",
                                               New With {.P_EMP_ID = empId,
                                                         .P_START_DATE = startDate,
                                                         .P_END_DATE = EndDate,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_AT_SHIFT() As DataTable
        Dim dsData As New DataTable
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_AT_SHIFT",
                                               New With {.P_CUR = cls.OUT_CURSOR}, True)
                Return dsData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_AT_SWIPE_DATA_V1(ByVal log As UserLog, ByVal DATA_IN As String, ByVal Machine_type As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.IMPORT_AT_SWIPE_DATA",
                                               New With {.P_MACHINE_TYPE = Machine_type,
                                                         .P_DATA = DATA_IN,
                                                         .P_USER = log.Username,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CheckExistAT_LATE_COMBACKOUT(ByVal objImport As AT_TIME_TIMESHEET_MACHINETDTO) As Boolean
        Try
            Dim emp_id = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE = objImport.EMPLOYEE_CODE Select e.ID).FirstOrDefault
            Dim rs = (From p In Context.AT_LATE_COMBACKOUT Where p.EMPLOYEE_ID = emp_id And EntityFunctions.TruncateTime(p.WORKINGDAY) = EntityFunctions.TruncateTime(objImport.WORKINGDAY)).Any
            Return rs
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function IMPORT_TIMESHEET_MACHINE(ByVal ListobjImport As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean
        Try
            Dim lstDistinct = (From p In ListobjImport Select p.EMPLOYEE_CODE, p.WORKINGDAY).Distinct.ToList
            Dim type = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 45 And p.CODE = "TQT" Select p.ID).FirstOrDefault
            Dim registInfo = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 44 And p.CODE = "XACNHAN" Select p.ID).FirstOrDefault
            For Each obj In lstDistinct
                Try
                    Dim emp_id = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE = obj.EMPLOYEE_CODE Select e.ID).FirstOrDefault
                    Dim shiftID = (From p In Context.AT_TIME_TIMESHEET_MACHINET Where EntityFunctions.TruncateTime(p.WORKINGDAY) = EntityFunctions.TruncateTime(obj.WORKINGDAY) _
                                                                                  And p.EMPLOYEE_ID = emp_id Select p.SHIFT_ID).FirstOrDefault
                    Dim objData As New AT_LATE_COMBACKOUT
                    Dim objImport = (From p In ListobjImport Where p.EMPLOYEE_CODE = obj.EMPLOYEE_CODE And p.WORKINGDAY = obj.WORKINGDAY).FirstOrDefault
                    Dim _ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    objData.ID = _ID
                    objData.EMPLOYEE_ID = emp_id
                    objData.WORKINGDAY = objImport.WORKINGDAY
                    objData.FROM_HOUR = objImport.TIMEIN_REALITY
                    objData.TO_HOUR = objImport.TIMEOUT_REALITY
                    If shiftID IsNot Nothing Then
                        objData.SHIFT_ID = shiftID
                    End If
                    objData.REMARK = objImport.NOTE
                    objData.TYPE_DSVM = type
                    If objImport.TIMEIN_REALITY IsNot Nothing AndAlso objImport.TIMEOUT_REALITY IsNot Nothing Then
                        objData.MINUTE = DateDiff(DateInterval.Minute, CDate(objImport.TIMEIN_REALITY), CDate(objImport.TIMEOUT_REALITY))
                    End If
                    objData.REGIST_INFO = registInfo
                    objData.STATUS = 1
                    Context.AT_LATE_COMBACKOUT.AddObject(objData)
                    objImport = Nothing
                    emp_id = Nothing
                    objData = Nothing
                    'anhvn, BCG-1037
                    Context.SaveChanges(log)
                    Using cls As New DataAccess.QueryData
                        Dim _obj = New With {.P_ID = _ID, .P_ID_AT_SWIPE = "", .P_OUT = cls.OUT_CURSOR}
                        Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.UPDATE_INSERT_AT_SWIPE_DATA", _obj, False)
                    End Using
                Catch ex As Exception
                    Continue For
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Di som ve muon"

    Public Function GetDSVM_List_Emp(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim Status = New List(Of String)({"KHONGQT", "THIEUQT", "DITRE", "VESOM", "DITRE_VESOM", "SAICA"})

            'From a In Context.SE_USER.Where(Function(f) f.USERNAME = log.Username.ToUpper)
            'From b In Context.HU_EMPLOYEE.Where(Function(f) f.DIRECT_MANAGER = a.EMPLOYEE_ID)
            'From p In Context.AT_TIME_TIMESHEET_MACHINET.Where(Function(f) f.EMPLOYEE_ID = b.ID _
            '    And Status.Contains(f.STATUS))()

            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.CODE = p.SHIFT_CODE).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From z In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = p.STATUS).DefaultIfEmpty
                        From tt In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = z.TYPE_ID).DefaultIfEmpty
                        From v In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.VIOLATION_RANGE_ID).DefaultIfEmpty
            'Where p.STATUS IsNot Nothing
            If _filter.LIST_STATUS_SEARCH IsNot Nothing AndAlso _filter.LIST_STATUS_SEARCH.Count > 0 Then
                query = query.Where(Function(f) _filter.LIST_STATUS_SEARCH.Contains(f.p.STATUS.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .VIOLATE_MINUTE = p.p.VIOLATE_MINUTE,
                                       .VIOLATION_RANGE_ID = p.p.VIOLATION_RANGE_ID,
                                       .VIOLATION_RANGE_NAME = p.v.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .SHIFT_START = p.s.HOURS_START,
                                       .SHIFT_END = p.s.HOURS_STOP,
                                       .TIMEIN_REALITY = p.p.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.p.TIMEOUT_REALITY,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .STATUS_NAME = p.p.STATUS,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .WORK_EMAIL = p.cv.WORK_EMAIL,
                                       .STATUS_NAME1 = p.z.NAME_VN,
                                       .STATUS_TYPE_CODE = p.tt.CODE
                                       })

            lst = lst.Where(Function(f) f.STATUS_TYPE_CODE = "DITRE_VESOM")

            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            'If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
            '    lst = lst.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            'End If

            'If _filter.LIST_STATUS_SEARCH IsNot Nothing AndAlso _filter.LIST_STATUS_SEARCH.Count > 0 Then
            '    query = lst.Where(Function(f) _filter.LIST_STATUS_SEARCH.Contains(f.STATUS_NAME.ToUpper))
            'End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_CHECK_IN_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHECK_IN_NAME.ToLower().Contains(_filter.ORG_CHECK_IN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function GetDSVM(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            If Not IsNumeric(_filter.WORK_STATUS) Then
                _filter.WORK_STATUS = -1
            End If
            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGIST_INFO).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_c In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHECK_IN).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID _
                          And f.PROCESS_TYPE.ToUpper = "WLEO" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0 And h.PROCESS_TYPE.ToUpper = "WLEO").Min(Function(ki) ki.APP_LEVEL))).DefaultIfEmpty()
                        From tt In Context.HU_TITLE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                        From r In Context.AT_REASON_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                        Where e.OBJECT_EMPLOYEE_ID = _filter.WORK_STATUS Or _filter.WORK_STATUS = -1

            Dim str = " - "
            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .ID_AT_SWIPE = p.p.ID_AT_SWIPE,
                                       .REGIST_INFO = p.p.REGIST_INFO,
                                       .REGIST_INFO_NAME = p.o1.NAME_VN,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TYPE_DMVS_NAME = p.o2.NAME_VN,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_CHECK_IN = p.p.ORG_CHECK_IN,
                                       .ORG_CHECK_IN_NAME = p.o_c.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .EMP_APPROVES_NAME = p.ee.EMPLOYEE_CODE & str & p.ee.FULLNAME_VN,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = If(p.p.STATUS = 0, "Chờ phê duyệt", If(p.p.STATUS = 1, "Phê duyệt", If(p.p.STATUS = 2, "Không phê duyệt", "Chưa gửi duyệt"))),
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .REASON_NAME = p.r.NAME})
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_CHECK_IN_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHECK_IN_NAME.ToLower().Contains(_filter.ORG_CHECK_IN_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REASON_NAME) Then
                lst = lst.Where(Function(f) f.REASON_NAME.ToLower().Contains(_filter.REASON_NAME.ToLower()))
            End If
            If _filter.STATUS.HasValue Then
                lst = lst.Where(Function(f) f.STATUS = _filter.STATUS)
            End If
            If _filter.TYPE_DMVS_ID.HasValue Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_ID = _filter.TYPE_DMVS_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetChildren(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "WORKINGDAY desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_CHILDREN_TAKECARE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .WORKINGDAY = p.p.EFFECT_DATE,
                                       .END_DATE = p.p.EXPIRE_DATE,
                                       .REMARK = p.p.NOTE,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TS_EXCEPTION = p.p.TS_EXCEPTION})

            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_CHECK_IN_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHECK_IN_NAME.ToLower().Contains(_filter.ORG_CHECK_IN_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function InsertLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer
        Dim objLate_Combackout As New AT_LATE_COMBACKOUT
        Dim id = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)

        Try

            With objLate_Combackout
                .ID = id
                .EMPLOYEE_ID = obj.EMPLOYEE_ID
                .STATUS = 3
                .REGIST_INFO = obj.REGIST_INFO
                .TYPE_DSVM = obj.TYPE_DMVS_ID
                .SHIFT_ID = obj.SHIFT_ID
                .WORKINGDAY = obj.WORKINGDAY
                .FROM_HOUR = obj.FROM_HOUR
                .TO_HOUR = obj.TO_HOUR
                .MINUTE = obj.MINUTE
                .REMARK = obj.REMARK
                .REASON_ID = obj.REASON_ID
            End With
            Context.AT_LATE_COMBACKOUT.AddObject(objLate_Combackout)
            Context.SaveChanges(log)
            If obj.IS_SEND Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "WLEO", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = id, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim objDelete = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = id).FirstOrDefault
                        Context.AT_LATE_COMBACKOUT.DeleteObject(objDelete)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertLate_CombackoutPortal")
            Return -1
        End Try
    End Function

    Public Function ApproveLate_CombackoutPortal(ByVal lstObj As List(Of AT_LATE_COMBACKOUTDTO)) As Integer
        Try
            For Each item In lstObj
                Dim query = (From m In Context.AT_TIME_TIMESHEET_MACHINET
                             From p In Context.AT_LATE_COMBACKOUT.Where(Function(f) f.EMPLOYEE_ID = m.EMPLOYEE_ID And EntityFunctions.TruncateTime(f.WORKINGDAY) = EntityFunctions.TruncateTime(m.WORKINGDAY)).DefaultIfEmpty
                             Where m.ID = item.ID Select p.ID).FirstOrDefault
                If query > 0 Then
                    Using cls As New DataAccess.QueryData
                        Dim priProcessApp = New With {.P_EMPLOYEE_ID = item.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "WLEO", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = query, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                        Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                        Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                        If outNumber <> 0 Then
                            Return outNumber
                        End If
                    End Using
                End If
            Next
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ApproveLate_CombackoutPortal")
            Return -1
        End Try
    End Function
    Public Function ModifyLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer
        Try
            Dim objLate_Combackout = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = obj.ID).FirstOrDefault
            objLate_Combackout.REGIST_INFO = obj.REGIST_INFO
            objLate_Combackout.TYPE_DSVM = obj.TYPE_DMVS_ID
            objLate_Combackout.SHIFT_ID = obj.SHIFT_ID
            objLate_Combackout.STATUS = obj.STATUS
            objLate_Combackout.FROM_HOUR = obj.FROM_HOUR
            objLate_Combackout.TO_HOUR = obj.TO_HOUR
            objLate_Combackout.MINUTE = obj.MINUTE
            objLate_Combackout.REMARK = obj.REMARK
            objLate_Combackout.REASON_ID = obj.REASON_ID
            Context.SaveChanges(log)
            If obj.IS_SEND Then
                Using cls As New DataAccess.QueryData
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID, .P_PERIOD_ID = 0, .P_PROCESS_TYPE = "WLEO", .P_TOTAL_HOURS = 0, .P_TOTAL_DAY = 0, .P_SIGN_ID = 0, .P_ID_REGGROUP = obj.ID, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    If outNumber <> 0 Then
                        Dim objDelete = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = obj.ID).FirstOrDefault
                        Context.AT_LATE_COMBACKOUT.DeleteObject(objDelete)
                    End If
                    Return outNumber
                End Using
            End If
            Return 0
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyLate_CombackoutPortal")
            Return -1
        End Try
    End Function
    Public Function GetLate_CombackoutByIdPortalNew(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try
            Dim query = From m In Context.AT_TIME_TIMESHEET_MACHINET
                        From p In Context.AT_LATE_COMBACKOUT.Where(Function(f) f.EMPLOYEE_ID = m.EMPLOYEE_ID And EntityFunctions.TruncateTime(f.WORKINGDAY) = EntityFunctions.TruncateTime(m.WORKINGDAY)).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = m.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHECK_IN).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = m.SHIFT_ID).DefaultIfEmpty
                        From r In Context.AT_REASON_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                        Where p.ID = _id
            Dim obj = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .REGIST_INFO = p.p.REGIST_INFO,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_CHECK_IN = p.p.ORG_CHECK_IN,
                                       .ORG_CHECK_IN_NAME = p.o2.NAME_VN,
                                       .ID_AT_SWIPE = p.p.ID_AT_SWIPE,
                                       .WORKINGDAY = p.m.WORKINGDAY,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STATUS = p.p.STATUS,
                                       .SHIFT_ID = p.m.SHIFT_ID,
                                       .SHIFT_NAME = p.s.NAME_VN,
                                       .SHIFT_CODE = p.s.CODE,
                                       .SHIFT_START = p.s.HOURS_START,
                                       .SHIFT_END = p.s.HOURS_STOP,
                                       .TIMEIN_REALITY = p.m.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.m.TIMEOUT_REALITY,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .REASON_ID = p.p.REASON_ID,
                                       .REASON_NAME = p.r.NAME}).FirstOrDefault
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo(1053)
            If obj.SHIFT_START IsNot Nothing AndAlso obj.SHIFT_END IsNot Nothing Then
                obj.SHIFT_NAME = obj.SHIFT_CODE & " (" & obj.SHIFT_START.Value.ToShortTimeString & " - " & obj.SHIFT_END.Value.ToShortTimeString & ")"
            Else
                obj.SHIFT_NAME = obj.SHIFT_CODE
            End If
            If obj.TIMEIN_REALITY IsNot Nothing AndAlso obj.TIMEOUT_REALITY IsNot Nothing Then
                obj.TIME_REALITY = obj.TIMEIN_REALITY.Value.ToShortTimeString & " - " & obj.TIMEOUT_REALITY.Value.ToShortTimeString
            End If
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetLate_CombackoutByIdPortal(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try
            Dim query = From m In Context.AT_TIME_TIMESHEET_MACHINET
                        From p In Context.AT_LATE_COMBACKOUT.Where(Function(f) f.EMPLOYEE_ID = 0 And EntityFunctions.TruncateTime(f.WORKINGDAY) = EntityFunctions.TruncateTime(m.WORKINGDAY)).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = m.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHECK_IN).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = m.SHIFT_ID).DefaultIfEmpty
                        From r In Context.AT_REASON_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                        Where m.ID = _id
            Dim obj = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .REGIST_INFO = p.p.REGIST_INFO,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.m.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_CHECK_IN = p.p.ORG_CHECK_IN,
                                       .ORG_CHECK_IN_NAME = p.o2.NAME_VN,
                                       .ID_AT_SWIPE = p.p.ID_AT_SWIPE,
                                       .WORKINGDAY = p.m.WORKINGDAY,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STATUS = p.p.STATUS,
                                       .SHIFT_ID = p.m.SHIFT_ID,
                                       .SHIFT_NAME = p.s.NAME_VN,
                                       .SHIFT_CODE = p.s.CODE,
                                       .SHIFT_START = p.s.HOURS_START,
                                       .SHIFT_END = p.s.HOURS_STOP,
                                       .TIMEIN_REALITY = p.m.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.m.TIMEOUT_REALITY,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .REASON_ID = p.p.REASON_ID,
                                       .REASON_NAME = p.r.NAME}).FirstOrDefault

            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo(1053)
            If obj.SHIFT_START IsNot Nothing AndAlso obj.SHIFT_END IsNot Nothing Then
                obj.SHIFT_NAME = obj.SHIFT_CODE & " (" & obj.SHIFT_START.Value.ToShortTimeString & " - " & obj.SHIFT_END.Value.ToShortTimeString & ")"
            Else
                obj.SHIFT_NAME = obj.SHIFT_CODE
            End If
            If obj.TIMEIN_REALITY IsNot Nothing AndAlso obj.TIMEOUT_REALITY IsNot Nothing Then
                obj.TIME_REALITY = obj.TIMEIN_REALITY.Value.ToShortTimeString & " - " & obj.TIMEOUT_REALITY.Value.ToShortTimeString
            End If
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try

            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHECK_IN).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From type In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID).DefaultIfEmpty
                        From R In Context.AT_REASON_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim obj = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .REGIST_INFO = p.p.REGIST_INFO,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .TYPE_DMVS_NAME = p.type.NAME,
                                       .TYPE_DMVS_ID = p.p.TYPE_DSVM,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .MINUTE = p.p.MINUTE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .REMARK = p.p.REMARK,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_CHECK_IN = p.p.ORG_CHECK_IN,
                                       .ORG_CHECK_IN_NAME = p.o2.NAME_VN,
                                       .ID_AT_SWIPE = p.p.ID_AT_SWIPE,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STATUS = p.p.STATUS,
                                       .SHIFT_ID = p.p.ID,
                                       .SHIFT_NAME = p.s.NAME_VN,
                                       .SHIFT_CODE = p.s.CODE,
                                       .SHIFT_START = p.s.HOURS_START,
                                       .SHIFT_END = p.s.HOURS_STOP,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .REASON_ID = p.p.REASON_ID,
                                       .REASON_NAME = p.R.NAME}).FirstOrDefault
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GETCHILDREN_TAKECAREBYID(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Try

            Dim query = From p In Context.AT_CHILDREN_TAKECARE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .FROM_DATE = p.p.EFFECT_DATE,
                                       .END_DATE = p.p.EXPIRE_DATE,
                                       .REMARK = p.p.NOTE,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TS_EXCEPTION = p.p.TS_EXCEPTION}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef gID_Swipe As String) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        Dim sql = (From e In Context.HU_EMPLOYEE
                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                   Where e.EMPLOYEE_CODE = objLate_combackout.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And
                   (e.TER_EFFECT_DATE Is Nothing Or
                    (e.TER_EFFECT_DATE IsNot Nothing And
                     e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                   Order By w.EFFECT_DATE Descending
                   Select w).FirstOrDefault
        If sql IsNot Nothing Then
            employee_id = sql.EMPLOYEE_ID
            org_id = sql.ORG_ID
        Else
            Exit Function
        End If

        If Not employee_id Is Nothing Then
            Try
                exits = (From p In Context.AT_LATE_COMBACKOUT
                         Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).Any
                If exits Then
                    Dim objlate = (From p In Context.AT_LATE_COMBACKOUT
                                   Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).FirstOrDefault
                    objlate.EMPLOYEE_ID = employee_id
                    objlate.ORG_ID = org_id
                    objlate.TITLE_ID = sql.TITLE_ID
                    objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objlate.REMARK = objLate_combackout.REMARK
                    objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objlate.MINUTE = objLate_combackout.MINUTE
                    objlate.REGIST_INFO = objLate_combackout.REGIST_INFO
                    objlate.REASON_ID = objLate_combackout.REASON_ID
                    objlate.TO_HOUR = objLate_combackout.TO_HOUR
                    objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                    objlate.STATUS = 1
                    If objLate_combackout.ORG_CHECK_IN Is Nothing Then
                        objlate.ORG_CHECK_IN = org_id
                    Else
                        objlate.ORG_CHECK_IN = objLate_combackout.ORG_CHECK_IN
                    End If
                    gID_Swipe = objlate.ID_AT_SWIPE
                    gID = objlate.ID
                Else
                    objLate_combackoutData = New AT_LATE_COMBACKOUT
                    objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    objLate_combackoutData.EMPLOYEE_ID = employee_id
                    objLate_combackoutData.ORG_ID = org_id
                    objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                    objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                    objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                    objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                    objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                    objLate_combackoutData.REGIST_INFO = objLate_combackout.REGIST_INFO
                    objLate_combackoutData.REASON_ID = objLate_combackout.REASON_ID
                    objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                    objLate_combackoutData.REMARK = objLate_combackout.REMARK
                    objLate_combackoutData.STATUS = 1
                    If objLate_combackout.ORG_CHECK_IN Is Nothing Then
                        objLate_combackoutData.ORG_CHECK_IN = org_id
                    Else
                        objLate_combackoutData.ORG_CHECK_IN = objLate_combackout.ORG_CHECK_IN
                    End If
                    Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)

                    gID = objLate_combackoutData.ID
                    gID_Swipe = ""
                End If
                Context.SaveChanges(log)
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                Throw ex
            End Try
        Else
            Return False
        End If
    End Function

    Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_LATE_COMBACKOUT
        Dim objData As New AT_LATE_COMBACKOUTDTO
        Dim exits As Boolean?
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            For index = 0 To objRegisterDMVSList.Count - 1
                objData = objRegisterDMVSList(index)
                Dim sql = (From e In Context.HU_EMPLOYEE
                           From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                           Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objLate_combackout.WORKINGDAY And
                                  (e.TER_EFFECT_DATE Is Nothing Or
                                   (e.TER_EFFECT_DATE IsNot Nothing And
                                    e.TER_EFFECT_DATE > objLate_combackout.WORKINGDAY)) And w.EFFECT_DATE <= objLate_combackout.WORKINGDAY
                           Order By w.EFFECT_DATE Descending
                           Select w).FirstOrDefault
                If sql IsNot Nothing Then
                    employee_id = sql.EMPLOYEE_ID
                    org_id = sql.ORG_ID
                Else
                    Exit Function
                End If

                If Not employee_id Is Nothing Then
                    exits = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).Any
                    If exits Then
                        Dim objlate = (From p In Context.AT_LATE_COMBACKOUT
                                       Where p.EMPLOYEE_ID = employee_id And p.WORKINGDAY = objLate_combackout.WORKINGDAY And p.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID).FirstOrDefault
                        objlate.EMPLOYEE_ID = employee_id
                        objlate.ORG_ID = org_id
                        objlate.TITLE_ID = sql.TITLE_ID
                        objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objlate.REMARK = objLate_combackout.REMARK
                        objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objData.REGIST_INFO = objLate_combackout.REGIST_INFO
                        objData.SHIFT_ID = objLate_combackout.SHIFT_ID
                        objData.REASON_ID = objLate_combackout.REASON_ID
                        objlate.MINUTE = objLate_combackout.MINUTE
                        objlate.TO_HOUR = objLate_combackout.TO_HOUR
                        objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
                        If objLate_combackout.ORG_CHECK_IN Is Nothing Or objLate_combackout.ORG_CHECK_IN = 0 Then
                            objlate.ORG_CHECK_IN = Nothing
                        Else
                            objlate.ORG_CHECK_IN = objLate_combackout.ORG_CHECK_IN
                        End If

                        objlate.STATUS = objLate_combackout.STATUS

                        objlate.MODIFIED_BY = log.Fullname
                        objlate.MODIFIED_DATE = DateTime.Now
                        objlate.MODIFIED_LOG = log.Ip & "-" & log.ComputerName

                    Else
                        objLate_combackoutData = New AT_LATE_COMBACKOUT
                        objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                        objLate_combackoutData.EMPLOYEE_ID = employee_id
                        objLate_combackoutData.ORG_ID = org_id
                        objLate_combackoutData.TITLE_ID = sql.TITLE_ID
                        objLate_combackoutData.MINUTE = objLate_combackout.MINUTE
                        objLate_combackoutData.TO_HOUR = objLate_combackout.TO_HOUR
                        objLate_combackoutData.FROM_HOUR = objLate_combackout.FROM_HOUR
                        objLate_combackoutData.WORKINGDAY = objLate_combackout.WORKINGDAY
                        objLate_combackoutData.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
                        objLate_combackoutData.REGIST_INFO = objLate_combackout.REGIST_INFO
                        objLate_combackoutData.SHIFT_ID = objLate_combackout.SHIFT_ID
                        objLate_combackoutData.REMARK = objLate_combackout.REMARK
                        objLate_combackoutData.STATUS = objLate_combackout.STATUS
                        objLate_combackoutData.IS_APP = objLate_combackout.IS_APP
                        objLate_combackoutData.REASON_ID = objLate_combackout.REASON_ID

                        If objLate_combackout.ORG_CHECK_IN Is Nothing Or objLate_combackout.ORG_CHECK_IN = 0 Then
                            objLate_combackoutData.ORG_CHECK_IN = Nothing
                        Else
                            objLate_combackoutData.ORG_CHECK_IN = objLate_combackout.ORG_CHECK_IN
                        End If

                        objLate_combackoutData.CREATED_BY = log.Username
                        objLate_combackoutData.CREATED_DATE = DateTime.Now
                        objLate_combackoutData.CREATED_LOG = log.Ip & "-" & log.ComputerName
                        objLate_combackoutData.MODIFIED_BY = log.Fullname
                        objLate_combackoutData.MODIFIED_DATE = DateTime.Now
                        objLate_combackoutData.MODIFIED_LOG = log.Ip & "-" & log.ComputerName

                        Context.AT_LATE_COMBACKOUT.AddObject(objLate_combackoutData)
                        gID = objLate_combackoutData.ID
                    End If
                    Context.SaveChanges()
                End If
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertChildren(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As AT_CHILDREN_TAKECARE
        Dim objData As New AT_LATE_COMBACKOUTDTO
        Dim employee_id As Decimal?
        Try
            For index = 0 To objRegisterDMVSList.Count - 1
                objData = objRegisterDMVSList(index)
                objLate_combackoutData = New AT_CHILDREN_TAKECARE
                objLate_combackoutData.ID = Utilities.GetNextSequence(Context, Context.AT_CHILDREN_TAKECARE.EntitySet.Name)
                objLate_combackoutData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                objLate_combackoutData.EFFECT_DATE = objLate_combackout.WORKINGDAY
                objLate_combackoutData.EXPIRE_DATE = objLate_combackout.END_DATE
                objLate_combackoutData.NOTE = objLate_combackout.REMARK
                objLate_combackoutData.TS_EXCEPTION = objLate_combackout.TS_EXCEPTION
                Context.AT_CHILDREN_TAKECARE.AddObject(objLate_combackoutData)
                gID = objLate_combackoutData.ID
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLate_combackout(ByVal _validate As AT_LATE_COMBACKOUTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.ID <> _validate.ID _
                               And ((_validate.FROM_HOUR >= p.FROM_HOUR AndAlso _validate.FROM_HOUR <= p.TO_HOUR) _
                                    OrElse (_validate.TO_HOUR > p.FROM_HOUR AndAlso _validate.TO_HOUR <= p.TO_HOUR) _
                                    OrElse (p.FROM_HOUR >= _validate.FROM_HOUR AndAlso p.FROM_HOUR < _validate.TO_HOUR) _
                                    OrElse (p.TO_HOUR >= _validate.FROM_HOUR AndAlso p.TO_HOUR <= _validate.TO_HOUR))).Any
                Else
                    query = (From p In Context.AT_LATE_COMBACKOUT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                               And ((_validate.FROM_HOUR >= p.FROM_HOUR AndAlso _validate.FROM_HOUR <= p.TO_HOUR) _
                                    OrElse (_validate.TO_HOUR > p.FROM_HOUR AndAlso _validate.TO_HOUR <= p.TO_HOUR) _
                                    OrElse (p.FROM_HOUR >= _validate.FROM_HOUR AndAlso p.FROM_HOUR < _validate.TO_HOUR) _
                                    OrElse (p.TO_HOUR >= _validate.FROM_HOUR AndAlso p.TO_HOUR <= _validate.TO_HOUR))).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As New AT_LATE_COMBACKOUT With {.ID = objLate_combackout.ID}
        Try
            Dim objlate = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = objLate_combackout.ID).FirstOrDefault
            'objlate.EMPLOYEE_ID = objLate_combackout.EMPLOYEE_ID không cho phép sửa nhân viên
            objlate.WORKINGDAY = objLate_combackout.WORKINGDAY
            objlate.ORG_ID = objLate_combackout.ORG_ID
            objlate.REMARK = objLate_combackout.REMARK
            objlate.TYPE_DSVM = objLate_combackout.TYPE_DMVS_ID
            objlate.REGIST_INFO = objLate_combackout.REGIST_INFO
            objlate.MINUTE = objLate_combackout.MINUTE
            objlate.TO_HOUR = objLate_combackout.TO_HOUR
            objlate.FROM_HOUR = objLate_combackout.FROM_HOUR
            objlate.ORG_CHECK_IN = objLate_combackout.ORG_CHECK_IN
            objlate.SHIFT_ID = objLate_combackout.SHIFT_ID
            objlate.REASON_ID = objLate_combackout.REASON_ID
            objlate.STATUS = 1
            Context.SaveChanges(log)
            If objlate.IS_APP = 0 Then
                Dim lstProcess = From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = objlate.ID And p.PROCESS_TYPE = "WLEO"

                For Each item In lstProcess
                    Context.PROCESS_APPROVED_STATUS.DeleteObject(item)
                Next
                Context.SaveChanges()
            End If
            gID = objLate_combackoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyChildren(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objLate_combackoutData As New AT_CHILDREN_TAKECARE With {.ID = objLate_combackout.ID}
        Try
            Dim objlate = (From p In Context.AT_CHILDREN_TAKECARE Where p.ID = objLate_combackout.ID).FirstOrDefault
            'objlate.EMPLOYEE_ID = objLate_combackout.EMPLOYEE_ID không cho phép sửa nhân viên
            objlate.EFFECT_DATE = objLate_combackout.WORKINGDAY
            objlate.EXPIRE_DATE = objLate_combackout.END_DATE
            objlate.NOTE = objLate_combackout.REMARK
            objlate.TS_EXCEPTION = objLate_combackout.TS_EXCEPTION
            Context.SaveChanges(log)
            gID = objLate_combackoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function DeleteLate_combackoutPortal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_LATE_COMBACKOUT)
        Try
            lstl = (From m In Context.AT_TIME_TIMESHEET_MACHINET
                    From p In Context.AT_LATE_COMBACKOUT.Where(Function(f) f.EMPLOYEE_ID = m.EMPLOYEE_ID And EntityFunctions.TruncateTime(f.WORKINGDAY) = EntityFunctions.TruncateTime(m.WORKINGDAY)).DefaultIfEmpty
                    Where lstID.Contains(m.ID) Select p).ToList

            For index = 0 To lstl.Count - 1
                Context.AT_LATE_COMBACKOUT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_LATE_COMBACKOUT)
        Try
            lstl = (From p In Context.AT_LATE_COMBACKOUT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_LATE_COMBACKOUT.DeleteObject(lstl(index))
            Next

            Dim lstProcessApprovedStatus = (From p In Context.PROCESS_APPROVED_STATUS Where lstID.Contains(p.ID_REGGROUP) AndAlso p.PROCESS_TYPE = "WLEO").ToList
            For index = 0 To lstProcessApprovedStatus.Count - 1
                Context.PROCESS_APPROVED_STATUS.DeleteObject(lstProcessApprovedStatus(index))
            Next

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function GetDMVS_Portal(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            Dim query = From p In Context.AT_LATE_COMBACKOUT
                        From m In Context.AT_TIME_TIMESHEET_MACHINET.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And EntityFunctions.TruncateTime(f.WORKINGDAY) = EntityFunctions.TruncateTime(p.WORKINGDAY)).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGIST_INFO).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o_c In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_CHECK_IN).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From r In Context.AT_REASON_LIST.Where(Function(f) f.ID = p.REASON_ID).DefaultIfEmpty
                        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.ApprovedByLM _
                           And f.PROCESS_TYPE.ToUpper = "WLEO" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.ApprovedByLM And h.PROCESS_TYPE.ToUpper = "WLEO").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From tt In Context.HU_TITLE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED And p.STATUS <> 2).DefaultIfEmpty()
                        From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER And p.STATUS <> 2).DefaultIfEmpty()
                        From w_p In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.WaitingForApproval _
                           And f.PROCESS_TYPE.ToUpper = "WLEO" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.WaitingForApproval And h.PROCESS_TYPE.ToUpper = "WLEO").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From w_t In Context.HU_TITLE.Where(Function(f) f.ID = w_p.EMPLOYEE_APPROVED).DefaultIfEmpty()
                        From w_e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = w_t.MASTER).DefaultIfEmpty()
                        From rj In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.UnApprovedByLM _
                           And f.PROCESS_TYPE.ToUpper = "WLEO" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.UnApprovedByLM And h.PROCESS_TYPE.ToUpper = "WLEO").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From rj_t In Context.HU_TITLE.Where(Function(f) f.ID = rj.EMPLOYEE_APPROVED).DefaultIfEmpty()
                        From rj_e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = rj_t.MASTER).DefaultIfEmpty()

            'Dim approveList = From p In query
            '                  From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.p.ID And f.APP_STATUS = 0 _
            '                And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.p.ID And h.APP_STATUS = 0).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty() _
            'From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty()

            'GET LEAVE_SHEET BY EMPLOYEE ID
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If

            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.rj.APP_NOTES.ToLower().Contains(_filter.REASON.ToLower()))
            End If
            If IsNumeric(_filter.STATUS) Then
                query = query.Where(Function(f) f.p.STATUS = _filter.STATUS)
            End If

            If Not String.IsNullOrEmpty(_filter.REASON_NAME) Then
                query = query.Where(Function(f) f.r.NAME.ToLower().Contains(_filter.REASON_NAME.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_LATE_COMBACKOUTDTO With {
                                                                       .ID = p.m.ID,
                                                                       .LATE_COMBACKOUT_ID = p.p.ID,
                                                                       .REGIST_INFO = p.p.REGIST_INFO,
                                                                       .REGIST_INFO_NAME = p.o1.NAME_VN,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .TYPE_DMVS_NAME = p.o2.NAME_VN,
                                                                       .TO_HOUR = p.p.TO_HOUR,
                                                                       .FROM_HOUR = p.p.FROM_HOUR,
                                                                       .MINUTE = p.p.MINUTE,
                                                                       .WORKINGDAY = p.p.WORKINGDAY,
                                                                       .REMARK = p.p.REMARK,
                                                                       .ORG_ID = p.p.ORG_ID,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_CHECK_IN = p.p.ORG_CHECK_IN,
                                                                       .ORG_CHECK_IN_NAME = p.o_c.NAME_VN,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .ORDER_NUM = p.ot.ORDERBYID,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .REASON = p.rj.APP_NOTES,
                                                                       .REASON_NAME = p.r.NAME,
                                                                       .EMP_APPROVES_NAME = If(p.p.STATUS.Value = 0, p.w_e.EMPLOYEE_CODE & " - " & p.w_e.FULLNAME_VN, If(p.p.STATUS.Value = 1, p.ee.EMPLOYEE_CODE & " - " & p.ee.FULLNAME_VN, If(p.p.STATUS.Value = 2, p.rj_e.EMPLOYEE_CODE & " - " & p.rj_e.FULLNAME_VN, Nothing)))})

            If Not String.IsNullOrEmpty(_filter.TYPE_DMVS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DMVS_NAME.ToLower().Contains(_filter.TYPE_DMVS_NAME.ToLower()))
            End If
            If _filter.STATUS.HasValue Then
                lst = lst.Where(Function(f) f.STATUS = _filter.STATUS)
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If
            If _filter.MINUTE.HasValue Then
                lst = lst.Where(Function(f) f.MINUTE = _filter.MINUTE)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_CHECK_IN_NAME) Then
                lst = lst.Where(Function(f) f.ORG_CHECK_IN_NAME.ToLower().Contains(_filter.ORG_CHECK_IN_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REGIST_INFO_NAME) Then
                lst = lst.Where(Function(f) f.REGIST_INFO_NAME.ToLower().Contains(_filter.REGIST_INFO_NAME.ToLower()))
            End If

            lst = lst.OrderBy("ORDER_NUM")
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeletePortalLate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_LATE_COMBACKOUT)
        Try
            lstl = (From p In Context.AT_LATE_COMBACKOUT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_LATE_COMBACKOUT.DeleteObject(lstl(index))
                'Dim LstobjApproved = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = lstl(index).ID And p.PROCESS_TYPE.ToUpper.Equals("WLEO")).ToList
                'For Each item In LstobjApproved
                '    Context.PROCESS_APPROVED_STATUS.DeleteObject(item)
                'Next
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function GetPortalEmpMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Try

            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SHIFT_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(F) F.CODE = p.STATUS).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
            'And (p.STATUS.ToUpper.Equals("DITRE") Or p.STATUS.ToUpper.Equals("VESOM") Or
            '                                               p.STATUS.ToUpper.Equals("THIEUQT") Or p.STATUS.ToUpper.Equals("KHONGQT") _
            '                                               Or p.STATUS.ToUpper.Equals("DITRE_VESOM"))

            'If _filter.PERIOD_ID IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.PERIOD_ID = _filter.PERIOD_ID)
            'End If
            If _filter.EMP_OBJ_ID IsNot Nothing Then
                query = query.Where(Function(f) f.e.OBJECT_EMPLOYEE_ID = _filter.EMP_OBJ_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.SHIFT_CODE) Then
                query = query.Where(Function(f) f.p.SHIFT_CODE.ToLower().Contains(_filter.SHIFT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.DAYOFWEEK) Then
                query = query.Where(Function(f) f.p.DAYOFWEEK.ToLower().Contains(_filter.DAYOFWEEK.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                query = query.Where(Function(f) f.m.CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If _filter.SHIFTIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN1 = _filter.SHIFTIN)
            End If
            If _filter.SHIFTOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN4 = _filter.SHIFTOUT)
            End If
            If _filter.SHIFTBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN2 = _filter.SHIFTBACKOUT)
            End If
            If _filter.SHIFTBACKIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN3 = _filter.SHIFTBACKIN)
            End If
            If _filter.WORKINGHOUR.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGHOUR = _filter.WORKINGHOUR)
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                query = query.Where(Function(f) f.p.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            'If _filter.IS_LATE Then
            '    query = query.Where(Function(f) f.p.TIMEIN_REALITY > f.p.HOURS_START)
            'End If
            'If _filter.IS_EARLY Then
            '    query = query.Where(Function(f) f.p.TIMEOUT_REALITY < f.p.HOURS_STOP)
            'End If
            'If _filter.IS_REALITY Then
            '    query = query.Where(Function(f) f.p.NOTE IsNot Nothing)
            'End If
            'If _filter.IS_NON_WORKING_VALUE Then
            '    query = query.Where(Function(f) f.p.WORKING_VALUE Is Nothing)
            'End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                query = query.Where(Function(f) f.st.NAME_VN.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.LIST_STATUS_SEARCH.Count > 0 Then
                query = query.Where(Function(f) _filter.LIST_STATUS_SEARCH.Contains(f.p.STATUS.ToUpper()))
            End If
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .MANUAL_CODE = p.m.CODE,
                                       .LATE = p.p.LATE,
                                       .WORKINGHOUR = p.p.WORKINGHOUR,
                                       .SHIFTIN = p.p.VALIN1,
                                       .SHIFTBACKOUT = p.p.VALIN2,
                                       .SHIFTBACKIN = p.p.VALIN3,
                                       .SHIFTOUT = p.p.VALIN4,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TIMEVALIN = p.p.TIMEVALIN,
                                       .TIMEVALOUT = p.p.TIMEVALOUT,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .OBJECT_ATTENDANCE_CODE = p.p.OBJECT_ATTENDANCE_CODE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .HOURS_START = p.p.HOURS_START,
                                       .TIMEIN_REALITY = p.p.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.p.TIMEOUT_REALITY,
                                       .WORKING_VALUE = p.p.WORKING_VALUE,
                                       .SHIFT_TYPE_CODE = p.p.SHIFT_TYPE_CODE,
                                       .NOTE = p.p.NOTE,
                                       .START_MID_HOURS = p.p.START_MID_HOURS,
                                       .END_MID_HOURS = p.p.END_MID_HOURS,
                                       .STATUS_SHIFT = p.p.STATUS_SHIFT,
                                       .STATUS_SHIFT_NAME = p.p.STATUS_SHIFT_NAME,
                                       .DAY_NUM = p.p.DAY_NUM,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .DAYOFWEEK = p.p.DAYOFWEEK,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = p.st.NAME_VN,
                                       .TOMORROW_GOC_NAME = If(p.p.TOMORROW_GOC = -1, "X", ""),
                                       .TOMORROW_GOC = p.p.TOMORROW_GOC,
                                       .TOMORROW_SHIFT = p.p.TOMORROW_SHIFT,
                                       .TOMORROW_SHIFT_NAME = If(p.p.TOMORROW_SHIFT = -1, "X", ""),
                                       .TOMORROW_TT = p.p.TOMORROW_TT,
                                       .TOMORROW_TT_NAME = If(p.p.TOMORROW_TT = -1, "X", "")})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPortalMachinesByID(ByVal _id As Decimal) As AT_TIME_TIMESHEET_MACHINETDTO
        Try

            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        Where p.ID = _id
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {.ID = p.ID,
                                                                                       .WORKINGDAY = p.WORKINGDAY,
                                                                                       .HOURS_START = p.HOURS_START,
                                                                                       .HOURS_STOP = p.HOURS_STOP}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Cham cong may"
    Public Function Upd_TimeTImesheetMachines(ByVal lstObj As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean
        Try
            For Each obj In lstObj
                Dim emp_id = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE = obj.EMPLOYEE_CODE Select e.ID).FirstOrDefault
                Dim LstobjData = (From p In Context.AT_TIME_TIMESHEET_MACHINET Where p.EMPLOYEE_ID = emp_id And p.WORKINGDAY = obj.WORKINGDAY Select p)
                If LstobjData Is Nothing Then Continue For
                For Each objdata In LstobjData
                    If obj.TIMEIN_REALITY IsNot Nothing Then
                        objdata.TIMEIN_REALITY = obj.TIMEIN_REALITY
                    End If
                    If obj.TIMEOUT_REALITY IsNot Nothing Then
                        objdata.TIMEOUT_REALITY = obj.TIMEOUT_REALITY
                    End If
                    objdata.NOTE = obj.NOTE
                Next
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO,
                                               ByVal log As UserLog,
                                               ByVal p_fromdate As Date,
                                               ByVal p_enddate As Date,
                                               ByVal P_ORG_ID As Decimal,
                                               ByVal lstEmployee As List(Of Decimal?),
                                               ByVal p_delAll As Decimal,
                                               ByVal codecase As String) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            Using cls As New DataAccess.NonQueryData
                Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE.Value.Year = p_fromdate.Year And w.START_DATE.Value.Month = p_fromdate.Month).FirstOrDefault
                obj.PERIOD_ID = Period.ID
                LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG GỐC", obj, P_ORG_ID)
                Select Case codecase
                    Case "ctrlTime_Timesheet_CTT"
                        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMESHEET_DAILY",
                                             New With {.P_USERNAME = log.Username.ToUpper,
                                                       .P_ORG_ID = P_ORG_ID,
                                                       .P_PERIOD_ID = Period.ID,
                                                       .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                       .P_DELETE_ALL = p_delAll})
                    Case "ctrlTimeTimesheet_machine_case1"
                        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_HOSE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = Period.ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_DELETE_ALL = p_delAll})
                    Case "ctrlTimeTimesheet_machine_caseALL"
                        cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL",
                                              New With {.P_USERNAME = log.Username.ToUpper,
                                                        .P_ORG_ID = P_ORG_ID,
                                                        .P_PERIOD_ID = Period.ID,
                                                        .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                        .P_DELETE_ALL = p_delAll})

                End Select
                'If codecase = "ctrlTimeTimesheet_machine_case1" Then
                '    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_HOSE",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = P_ORG_ID,
                '                                         .P_PERIOD_ID = Period.ID,
                '                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                '                                         .P_DELETE_ALL = p_delAll})
                'Else
                '    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = P_ORG_ID,
                '                                         .P_PERIOD_ID = Period.ID,
                '                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                '                                         .P_DELETE_ALL = p_delAll})
                'End If
                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function GetMachinesPortal(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SHIFT_ID).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(F) F.CODE = p.STATUS).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And p.WORKINGDAY <= Date.Now

            If _filter.PERIOD_ID IsNot Nothing Then
                Dim period = (From p In Context.AT_PERIOD Where p.ID = _filter.PERIOD_ID).FirstOrDefault
                Dim fromDate As New Date(period.YEAR, period.MONTH, 1, 0, 0, 0)
                Dim toDate As New Date(period.YEAR, period.MONTH, (fromDate.AddMonths(1).AddDays(-1)).Day, 0, 0, 0)
                _filter.FROM_DATE = fromDate
                _filter.END_DATE = toDate
            End If
            If _filter.EMP_OBJ_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.OBJ_EMP_ID = _filter.EMP_OBJ_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.DAYOFWEEK) Then
                query = query.Where(Function(f) f.p.DAYOFWEEK.ToLower().Contains(_filter.DAYOFWEEK.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.SHIFT_CODE) Then
                query = query.Where(Function(f) f.p.SHIFT_CODE.ToLower().Contains(_filter.SHIFT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                query = query.Where(Function(f) f.m.CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If _filter.SHIFTIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN1 = _filter.SHIFTIN)
            End If
            If _filter.SHIFTOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN4 = _filter.SHIFTOUT)
            End If
            If _filter.SHIFTBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN2 = _filter.SHIFTBACKOUT)
            End If
            If _filter.SHIFTBACKIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN3 = _filter.SHIFTBACKIN)
            End If
            If _filter.WORKINGHOUR.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGHOUR = _filter.WORKINGHOUR)
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                query = query.Where(Function(f) f.p.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.MINUTE_DM.HasValue Then
                query = query.Where(Function(f) f.p.MINUTE_DM = _filter.MINUTE_DM)
            End If
            If _filter.MINUTE_VS.HasValue Then
                query = query.Where(Function(f) f.p.MINUTE_VS <= _filter.MINUTE_VS)
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                query = query.Where(Function(f) f.st.NAME_VN.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.LIST_STATUS_SEARCH IsNot Nothing AndAlso _filter.LIST_STATUS_SEARCH.Count > 0 Then
                query = query.Where(Function(f) _filter.LIST_STATUS_SEARCH.Contains(f.p.STATUS.ToUpper))
            End If
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .MANUAL_CODE = If(p.m.CODE Is Nothing, "" + p.m.CODE + p.m.NAME, "[" + p.m.CODE + "] " + p.m.NAME),
                                       .LATE = p.p.LATE,
                                       .WORKINGHOUR = p.p.WORKINGHOUR,
                                       .SHIFTIN = p.p.VALIN1,
                                       .SHIFTBACKOUT = p.p.VALIN2,
                                       .SHIFTBACKIN = p.p.VALIN3,
                                       .SHIFTOUT = p.p.VALIN4,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TIMEVALIN = p.p.TIMEVALIN,
                                       .TIMEVALOUT = p.p.TIMEVALOUT,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .HOURS_START = p.p.HOURS_START,
                                       .TIMEIN_REALITY = p.p.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.p.TIMEOUT_REALITY,
                                       .WORKING_VALUE = p.p.WORKING_VALUE,
                                       .SHIFT_TYPE_CODE = p.p.SHIFT_TYPE_CODE,
                                       .NOTE = p.p.NOTE,
                                       .START_MID_HOURS = p.p.START_MID_HOURS,
                                       .END_MID_HOURS = p.p.END_MID_HOURS,
                                       .STATUS_SHIFT = p.p.STATUS_SHIFT,
                                       .STATUS_SHIFT_NAME = p.p.STATUS_SHIFT_NAME,
                                       .DAY_NUM = p.p.DAY_NUM,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = p.st.NAME_VN,
                                       .MIN_NIGHT = p.p.MIN_NIGHT,
                                       .MINUTE_DM = p.p.MINUTE_DM,
                                       .MINUTE_VS = p.p.MINUTE_VS,
                                       .WORK_HOUR = p.p.WORK_HOUR,
                                       .TOMORROW_GOC_NAME = If(p.p.TOMORROW_GOC = -1, "X", ""),
                                       .TOMORROW_GOC = p.p.TOMORROW_GOC,
                                       .TOMORROW_SHIFT = p.p.TOMORROW_SHIFT,
                                       .TOMORROW_SHIFT_NAME = If(p.p.TOMORROW_SHIFT = -1, "X", ""),
                                       .TOMORROW_TT = p.p.TOMORROW_TT,
                                       .TOMORROW_TT_NAME = If(p.p.TOMORROW_TT = -1, "X", ""),
                                       .DAYOFWEEK = p.p.DAYOFWEEK,
                                       .IS_LOCK_NAME = If(p.p.IS_LOCK = -1, "X", "")})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim rs = lst.ToList
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo(1053)
            For Each item In rs
                If item.HOURS_START IsNot Nothing AndAlso item.HOURS_STOP IsNot Nothing Then
                    item.CODE_HOURS = item.SHIFT_CODE & " (" & item.HOURS_START.Value.ToShortTimeString & " - " & item.HOURS_STOP.Value.ToShortTimeString & ")"
                Else
                    item.CODE_HOURS = item.SHIFT_CODE
                End If
            Next
            Return rs.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_TIMESHEET_MACHINET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From ot In Context.ATV_AT_OT_REGISTRATION.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.REGIST_DATE = p.WORKINGDAY And f.STATUS = 1 And f.IS_DELETED = 0).DefaultIfEmpty
                        From oc In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ACCOUNTING).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SHIFT_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From st In Context.OT_OTHER_LIST.Where(Function(F) F.CODE = p.STATUS).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper.ToUpper)

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE >= _filter.FROM_DATE)
            'End If
            If _filter.EMP_OBJ_ID IsNot Nothing Then
                'Dim emp_obj_id = (From p In Context.HU_WORKING Where p.EFFECT_DATE <= _filter.END_DATE).OrderBy("EFFECT_DATE DESC").FirstOrDefault.OBJECT_EMPLOYEE_ID
                query = query.Where(Function(f) f.p.OBJ_EMP_ID = _filter.EMP_OBJ_ID)

                'query = query.Where(Function(f) (From p In Context.HU_WORKING Where p.EFFECT_DATE <= _filter.END_DATE And f.p.EMPLOYEE_ID = p.EMPLOYEE_ID).OrderBy("EFFECT_DATE DESC").FirstOrDefault.OBJECT_EMPLOYEE_ID = _filter.EMP_OBJ_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                'query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or
                                        f.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_ACCOUNTING) Then
                query = query.Where(Function(f) f.oc.NAME_VN.ToLower().Contains(_filter.ORG_ACCOUNTING.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.SHIFT_CODE) Then
                query = query.Where(Function(f) f.p.SHIFT_CODE.ToLower().Contains(_filter.SHIFT_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                query = query.Where(Function(f) f.m.CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If _filter.SHIFTIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN1 = _filter.SHIFTIN)
            End If
            If _filter.SHIFTOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN4 = _filter.SHIFTOUT)
            End If
            If _filter.SHIFTBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.VALIN2 = _filter.SHIFTBACKOUT)
            End If
            If _filter.SHIFTBACKIN.HasValue Then
                query = query.Where(Function(f) f.p.VALIN3 = _filter.SHIFTBACKIN)
            End If
            If _filter.WORKINGHOUR.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGHOUR = _filter.WORKINGHOUR)
            End If
            If Not String.IsNullOrEmpty(_filter.LEAVE_CODE) Then
                query = query.Where(Function(f) f.p.LEAVE_CODE.ToLower().Contains(_filter.LEAVE_CODE.ToLower()))
            End If

            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.MINUTE_DM.HasValue Then
                query = query.Where(Function(f) f.p.MINUTE_DM = _filter.MINUTE_DM)
            End If
            If _filter.MINUTE_VS.HasValue Then
                query = query.Where(Function(f) f.p.MINUTE_VS <= _filter.MINUTE_VS)
            End If
            'If _filter.IS_LATE Then
            '    query = query.Where(Function(f) f.p.TIMEIN_REALITY > f.p.HOURS_START)
            'End If
            'If _filter.IS_EARLY Then
            '    query = query.Where(Function(f) f.p.TIMEOUT_REALITY < f.p.HOURS_STOP)
            'End If
            'If _filter.IS_REALITY Then
            '    query = query.Where(Function(f) f.p.NOTE IsNot Nothing)
            'End If
            'If _filter.IS_NON_WORKING_VALUE Then
            '    query = query.Where(Function(f) f.p.WORKING_VALUE Is Nothing)
            'End If
            'If _filter.IS_LATE Then
            '    query = query.Where(Function(f) f.p.STATUS.ToUpper = "DITRE" Or f.p.STATUS.ToUpper = "DITRE_VESOM")
            'End If
            'If _filter.IS_EARLY Then
            '    query = query.Where(Function(f) f.p.STATUS.ToUpper = "DITRE" Or f.p.STATUS.ToUpper = "DITRE_VESOM")
            'End If
            'If _filter.IS_REALITY Then
            '    query = query.Where(Function(f) f.p.STATUS.ToUpper = "THIEUQT")
            'End If
            'If _filter.IS_NON_WORKING_VALUE Then
            '    query = query.Where(Function(f) f.p.STATUS.ToUpper = "KHONGQT")
            'End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                query = query.Where(Function(f) f.st.NAME_VN.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.LIST_STATUS_SEARCH IsNot Nothing AndAlso _filter.LIST_STATUS_SEARCH.Count > 0 Then
                query = query.Where(Function(f) _filter.LIST_STATUS_SEARCH.Contains(f.p.STATUS.ToUpper))
            End If
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MACHINETDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_CODE = p.p.SHIFT_CODE,
                                       .MANUAL_CODE = If(p.m.CODE_KH Is Nothing, "" + p.m.CODE_KH + p.m.NAME, "[" + p.m.CODE_KH + "] " + p.m.NAME),
                                       .LATE = p.p.LATE,
                                       .WORKINGHOUR = p.p.WORKINGHOUR,
                                       .SHIFTIN = p.p.VALIN1,
                                       .SHIFTBACKOUT = p.p.VALIN2,
                                       .SHIFTBACKIN = p.p.VALIN3,
                                       .SHIFTOUT = p.p.VALIN4,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TIMEVALIN = p.p.TIMEVALIN,
                                       .TIMEVALOUT = p.p.TIMEVALOUT,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .OBJECT_ATTENDANCE_CODE = p.p.OBJECT_ATTENDANCE_CODE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .HOURS_START = p.p.HOURS_START,
                                       .TIMEIN_REALITY = p.p.TIMEIN_REALITY,
                                       .TIMEOUT_REALITY = p.p.TIMEOUT_REALITY,
                                       .WORKING_VALUE = p.p.WORKING_VALUE,
                                       .SHIFT_TYPE_CODE = p.p.SHIFT_TYPE_CODE,
                                       .NOTE = p.p.NOTE,
                                       .START_MID_HOURS = p.p.START_MID_HOURS,
                                       .END_MID_HOURS = p.p.END_MID_HOURS,
                                       .STATUS_SHIFT = p.p.STATUS_SHIFT,
                                       .STATUS_SHIFT_NAME = p.p.STATUS_SHIFT_NAME,
                                       .DAY_NUM = p.p.DAY_NUM,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = p.st.NAME_VN,
                                       .MIN_NIGHT = p.p.MIN_NIGHT,
                                       .ORG_ACCOUNTING = p.oc.SHORT_NAME,
                                       .MINUTE_DM = p.p.MINUTE_DM,
                                       .MINUTE_VS = p.p.MINUTE_VS,
                                       .WORKDAY_OT = p.ot.WORKDAY_OT,
                                       .WORKDAY_NIGHT = p.ot.WORKDAY_NIGHT,
                                       .WORK_HOUR = p.p.WORK_HOUR,
                                       .TOMORROW_GOC_NAME = If(p.p.TOMORROW_GOC = -1, "X", ""),
                                       .TOMORROW_GOC = p.p.TOMORROW_GOC,
                                       .TOMORROW_SHIFT = p.p.TOMORROW_SHIFT,
                                       .TOMORROW_SHIFT_NAME = If(p.p.TOMORROW_SHIFT = -1, "X", ""),
                                       .TOMORROW_TT = p.p.TOMORROW_TT,
                                       .TOMORROW_TT_NAME = If(p.p.TOMORROW_TT = -1, "X", ""),
                                       .DAYOFWEEK = p.p.DAYOFWEEK,
                                       .IS_LOCK_NAME = If(p.p.IS_LOCK = -1, "X", "")})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ActiveMachines(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of AT_TIME_TIMESHEET_MACHINET)
        Try
            lstData = (From p In Context.AT_TIME_TIMESHEET_MACHINET Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_LOCK = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    'Public Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
    '                                 ByVal _param As ParamDTO,
    '                                 Optional ByRef Total As Integer = 0,
    '                                 Optional ByVal PageIndex As Integer = 0,
    '                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
    '                                 Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
    '    Try
    '        Dim lst As List(Of AT_TIME_TIMESHEET_MACHINETDTO) = New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
    '        Using cls As New DataAccess.QueryData
    '            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETMACHINES",
    '                                       New With {.P_USERNAME = log.Username.ToUpper,
    '                                                 .P_ORGID = _param.ORG_ID,
    '                                                 .P_FROM_DATE = _filter.FROM_DATE,
    '                                                 .P_TO_DATE = _filter.END_DATE,
    '                                                 .P_OUT = cls.OUT_CURSOR})
    '            If dtData IsNot Nothing Then
    '                lst = (From row As DataRow In dtData.Rows
    '                   Select New AT_TIME_TIMESHEET_MACHINETDTO With {.ID = row("ID").ToString(),
    '                                                    .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
    '                                                   .VN_FULLNAME = row("VN_FULLNAME").ToString(),
    '                                                   .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
    '                                                   .TITLE_NAME = row("TITLE_NAME").ToString(),
    '                                                   .ORG_NAME = row("ORG_NAME").ToString(),
    '                                                   .ORG_DESC = row("ORG_DESC").ToString(),
    '                                                   .STAFF_RANK_NAME = row("STAFF_RANK_NAME").ToString(),
    '                                                   .ORG_ID = row("ORG_ID").ToString(),
    '                                                   .WORKINGDAY = If(row("WORKINGDAY") IsNot Nothing, ToDate(row("WORKINGDAY")), Nothing),
    '                                                   .SHIFTIN = If(row("SHIFTIN") IsNot Nothing, ToDate(row("SHIFTIN")), Nothing),
    '                                                   .SHIFTBACKOUT = If(row("SHIFTBACKOUT") IsNot Nothing, ToDate(row("SHIFTBACKOUT")), Nothing),
    '                                                   .SHIFTBACKIN = If(row("SHIFTBACKIN") IsNot Nothing, ToDate(row("SHIFTBACKIN")), Nothing),
    '                                                   .SHIFTOUT = If(row("SHIFTOUT") IsNot Nothing, ToDate(row("SHIFTOUT")), Nothing),
    '                                                   .SHIFT_ID = If(row("SHIFT_ID") IsNot Nothing, ToDecimal(row("SHIFT_ID")), Nothing),
    '                                                   .SHIFT_CODE = If(row("SHIFT_CODE") IsNot Nothing, row("SHIFT_CODE").ToString(), Nothing),
    '                                                   .MANUAL_CODE = If(row("MANUAL_CODE") IsNot Nothing, row("MANUAL_CODE").ToString(), Nothing),
    '                                                   .LATE = If(row("LATE") IsNot Nothing, ToDecimal(row("LATE")), Nothing),
    '                                                   .WORKINGHOUR = If(row("WORKINGHOUR") IsNot Nothing, ToDecimal(row("WORKINGHOUR")), Nothing),
    '                                                   .COMEBACKOUT = If(row("COMEBACKOUT") IsNot Nothing, ToDecimal(row("COMEBACKOUT")), Nothing),
    '                                                   .SALARIED_HOUR = If(row("SALARIED_HOUR") IsNot Nothing, ToDecimal(row("SALARIED_HOUR")), Nothing),
    '                                                  .NOTSALARIED_HOUR = If(row("NOTSALARIED_HOUR") IsNot Nothing, ToDecimal(row("NOTSALARIED_HOUR")), Nothing)
    '                                              }).ToList
    '                Total = lst.Count()
    '                lst = (From l In lst Order By Sorts Select l
    '                Skip PageIndex * PageSize
    '                Take PageSize).ToList
    '            End If
    '        End Using
    '        Return lst
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
    '        Throw ex
    '    End Try
    'End Function

    Public Function DeleteTimesheetMachinet(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of AT_TIME_TIMESHEET_MACHINET)
        Try
            lstData = (From p In Context.AT_TIME_TIMESHEET_MACHINET Where lstID.Contains(p.ID)).ToList
            For Each item In lstData
                Context.AT_TIME_TIMESHEET_MACHINET.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Cham cong tay"

    Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_OBJECT_ATTENDANCE_NAME = param.OBJECT_ATTENDANCE_NAME,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = CDec(param.IS_TERMINATE),
                                                         .P_EMP_OBJ = param.EMP_OBJ,
                                                         .P_START_DATE = param.FROM_DATE,
                                                         .P_END_DATE = param.END_DATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData '.P_COLOR = param.COLOR,
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_CCT_ORIGIN",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_TERMINATE = param.IS_TERMINATE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
        Try
            Dim query =
                      From e In Context.HU_EMPLOYEE
                      From p In Context.AT_TIME_TIMESHEET_DAILY.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.WORKINGDAY = obj.WORKINGDAY).DefaultIfEmpty
                      From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                      From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                      From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID).DefaultIfEmpty
                      Where e.ID = obj.EMPLOYEE_ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_DAILYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .SHIFT_NAME = "[" & p.s.CODE & "] " & p.s.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .MANUAL_CODE = p.m.CODE,
                                       .MANUAL_ID = p.p.MANUAL_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal, ByVal EmpObj As Decimal) As DataTable
        Try
            dtData.Columns(0).ColumnName = "E_ID"
            Dim dsData As New DataSet
            dsData.Tables.Add(dtData)
            Dim strXML = dsData.GetXml()
            For i As Int32 = 1 To dtData.Columns.Count - 1
                strXML = strXML.Replace("<" + dtData.Columns(i).ColumnName + " />", String.Empty)
            Next
            'strXML = strXML.Replace("\n", "").Replace("\r", "").Replace("\t", "")
            strXML = strXML.Replace(Chr(13), String.Empty).Replace(Chr(10), String.Empty)
            strXML = strXML.Replace(" ", String.Empty)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_XML = strXML,
                                           .P_USERNAME = log.Username.ToUpper,
                                           .P_PERIOD_ID = PeriodID,
                                           .P_OBJ_EMPLOYEE = EmpObj,
                                           .P_CUR = cls.OUT_CURSOR}
                Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_TIMESHEET_CTT", obj)
                Return dt
            End Using

            'Dim startDate As Date

            'Dim Period = (From w In Context.AT_PERIOD Where w.ID = PeriodID).FirstOrDefault

            'Using conMng As New ConnectionManager
            '    Using conn As New OracleConnection(conMng.GetConnectionString())
            '        Using cmd As New OracleCommand()
            '            Using resource As New DataAccess.OracleCommon()
            '                Try
            '                    conn.Open()
            '                    cmd.Connection = conn
            '                    cmd.CommandType = CommandType.StoredProcedure
            '                    cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"
            '                    cmd.Transaction = cmd.Connection.BeginTransaction()

            '                    For Each row As DataRow In dtData.Rows
            '                        cmd.Parameters.Clear()
            '                        Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
            '                                                 .P_PERIODId = Period.ID,
            '                                                 .P_USERNAME = log.Username.ToUpper,
            '                                                 .P_D1 = Utilities.Obj2Decima(row("D1"), 0),
            '                                                 .P_D2 = Utilities.Obj2Decima(row("D2"), 0),
            '                                                 .P_D3 = Utilities.Obj2Decima(row("D3"), 0),
            '                                                 .P_D4 = Utilities.Obj2Decima(row("D4"), 0),
            '                                                 .P_D5 = Utilities.Obj2Decima(row("D5"), 0),
            '                                                 .P_D6 = Utilities.Obj2Decima(row("D6"), 0),
            '                                                 .P_D7 = Utilities.Obj2Decima(row("D7"), 0),
            '                                                 .P_D8 = Utilities.Obj2Decima(row("D8"), 0),
            '                                                 .P_D9 = Utilities.Obj2Decima(row("D9"), 0),
            '                                                 .P_D10 = Utilities.Obj2Decima(row("D10"), 0),
            '                                                 .P_D11 = Utilities.Obj2Decima(row("D11"), 0),
            '                                                 .P_D12 = Utilities.Obj2Decima(row("D12"), 0),
            '                                                 .P_D13 = Utilities.Obj2Decima(row("D13"), 0),
            '                                                 .P_D14 = Utilities.Obj2Decima(row("D14"), 0),
            '                                                 .P_D15 = Utilities.Obj2Decima(row("D15"), 0),
            '                                                 .P_D16 = Utilities.Obj2Decima(row("D16"), 0),
            '                                                 .P_D17 = Utilities.Obj2Decima(row("D17"), 0),
            '                                                 .P_D18 = Utilities.Obj2Decima(row("D18"), 0),
            '                                                 .P_D19 = Utilities.Obj2Decima(row("D19"), 0),
            '                                                 .P_D20 = Utilities.Obj2Decima(row("D20"), 0),
            '                                                 .P_D21 = Utilities.Obj2Decima(row("D21"), 0),
            '                                                 .P_D22 = Utilities.Obj2Decima(row("D22"), 0),
            '                                                 .P_D23 = Utilities.Obj2Decima(row("D23"), 0),
            '                                                 .P_D24 = Utilities.Obj2Decima(row("D24"), 0),
            '                                                 .P_D25 = Utilities.Obj2Decima(row("D25"), 0),
            '                                                 .P_D26 = Utilities.Obj2Decima(row("D26"), 0),
            '                                                 .P_D27 = Utilities.Obj2Decima(row("D27"), 0),
            '                                                 .P_D28 = Utilities.Obj2Decima(row("D28"), 0),
            '                                                 .P_D29 = Utilities.Obj2Decima(row("D29"), 0),
            '                                                 .P_D30 = Utilities.Obj2Decima(row("D30"), 0),
            '                                                 .P_D31 = Utilities.Obj2Decima(row("D31"), 0)}

            '                        If objParam IsNot Nothing Then
            '                            For Each info As PropertyInfo In objParam.GetType().GetProperties()
            '                                Dim bOut As Boolean = False
            '                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
            '                                If para IsNot Nothing Then
            '                                    cmd.Parameters.Add(para)
            '                                End If
            '                            Next
            '                        End If
            '                        cmd.ExecuteNonQuery()
            '                    Next

            '                    cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.UPDATE_LEAVESHEET_DAILY"
            '                    cmd.Parameters.Clear()
            '                    Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
            '                                             .P_ENDDATE = Period.END_DATE.Value,
            '                                             .P_USERNAME = log.Username.ToUpper}

            '                    If objParam1 IsNot Nothing Then
            '                        For Each info As PropertyInfo In objParam1.GetType().GetProperties()
            '                            Dim bOut As Boolean = False
            '                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
            '                            If para IsNot Nothing Then
            '                                cmd.Parameters.Add(para)
            '                            End If
            '                        Next
            '                    End If

            '                    cmd.ExecuteNonQuery()

            '                    cmd.Transaction.Commit()
            '                Catch ex As Exception
            '                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            '                    cmd.Transaction.Rollback()
            '                    Throw ex
            '                    Return False
            '                Finally
            '                    'Dispose all resource
            '                    cmd.Dispose()
            '                    conn.Close()
            '                    conn.Dispose()
            '                End Try
            '            End Using
            '        End Using
            '    End Using
            'End Using
            'Context.SaveChanges(log)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            'Dim emp = (From e In Context.HU_EMPLOYEE Where e.ID = objLeave.EMPLOYEE_ID).FirstOrDefault
            'Dim Period = (From w In Context.AT_PERIOD Where w.START_DATE <= objLeave.WORKINGDAY And objLeave.WORKINGDAY <= w.END_DATE).FirstOrDefault

            'Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_DAILY Where r.EMPLOYEE_ID = emp.ID And r.WORKINGDAY = objLeave.WORKINGDAY).FirstOrDefault
            'Dim manual_code As AT_TIME_MANUAL
            'If TimeSheetDaily IsNot Nothing AndAlso TimeSheetDaily.MANUAL_ID IsNot Nothing Then
            '    manual_code = (From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = TimeSheetDaily.MANUAL_ID)).FirstOrDefault
            'End If

            'Dim lstEmployee As New List(Of Decimal?)
            'lstEmployee.Add(emp.ID)
            'Dim obj As New AT_ACTION_LOGDTO
            'obj.EMPLOYEE_ID = emp.ID
            'If manual_code IsNot Nothing Then
            '    obj.OLD_VALUE = manual_code.CODE
            'End If
            'obj.NEW_VALUE = objLeave.MANUAL_CODE
            'obj.PERIOD_ID = Period.ID
            'LOG_AT(New ParamDTO, log, lstEmployee, "CHỈNH SỬA XỬ LÝ DỮ LIỆU CHẤM CÔNG", obj, Nothing)
            'TimeSheetDaily.MANUAL_ID = objLeave.MANUAL_ID

            'Context.SaveChanges(log)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_TIMESHEET_CTT",
                                 New With {.P_FROMDATE = objLeave.FROM_DATE,
                                           .P_TODATE = objLeave.END_DATE,
                                           .P_EMP_ID = objLeave.EMPLOYEE_ID,
                                           .P_MANUAL_ID = objLeave.MANUAL_ID,
                                           .P_PERIOD_ID = objLeave.PERIOD_ID,
                                           .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Bang tong hop lam them"

    Public Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?),
                                         ByVal p_emp_obj As Decimal) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG LÀM THÊM GIỜ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_OT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_EMP_OBJ = p_emp_obj})

                'cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = P_ORG_ID,
                '                                         .P_PERIOD_ID = p_period_id,
                '                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_OT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_EMP_OBJ = param.EMP_OBJ,
                                                         .P_START_DATE = param.START_DATE,
                                                         .P_END_DATE = param.END_DATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO,
                                         ByVal log As UserLog,
                                         ByVal p_period_id As Decimal?,
                                         ByVal P_ORG_ID As Decimal,
                                         ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = p_period_id
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP CÔNG NGHỈ BÙ", obj, P_ORG_ID)
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_OT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .NUMBER_FACTOR_PAY = p.p.NUMBER_FACTOR_PAY,
                                       .NUMBER_FACTOR_CP = p.p.NUMBER_FACTOR_CP,
                                       .BACKUP_MONTH_BEFFORE = p.p.BACKUP_MONTH_BEFORE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteTimeSheetOT(ByVal lstID As List(Of AT_TIME_TIMESHEET_OTDTO)) As Boolean
        Dim id As Decimal = 0
        Try
            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                Dim lstl = (From p In Context.AT_TIME_TIMESHEET_OT Where id = p.ID).FirstOrDefault
                If Not lstl Is Nothing Then
                    Context.AT_TIME_TIMESHEET_OT.DeleteObject(lstl)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetOt(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTimeSheetData As New AT_TIME_TIMESHEET_OT
        Try
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim emp = (From e In Context.HU_EMPLOYEE Where e.EMPLOYEE_CODE.Equals(objLeave.EMPLOYEE_CODE)).FirstOrDefault
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_OT Where r.EMPLOYEE_ID = emp.ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.NUMBER_FACTOR_PAY = objLeave.NUMBER_FACTOR_PAY
            TimeSheetDaily.NUMBER_FACTOR_CP = objLeave.NUMBER_FACTOR_CP
            TimeSheetDaily.BACKUP_MONTH_BEFORE = objLeave.BACKUP_MONTH_BEFFORE
            Dim congNB = Decimal.Parse((TimeSheetDaily.NUMBER_FACTOR_CP + TimeSheetDaily.BACKUP_MONTH_BEFORE) / 8)
            TimeSheetDaily.CONGHIBU = Math.Round(congNB, 1)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Tổng hợp công"

    Public Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'From ot In Context.AT_TIME_TIMESHEET_OT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID And p.SALARY_ID = p.SALARY_ID_NEW).DefaultIfEmpty
            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From kn In Context.HU_CONCURRENTLY.Where(Function(f) f.ID = e.IS_KIEM_NHIEM).DefaultIfEmpty
                        From wo In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.IS_MISSION = -1 And f.EFFECT_DATE <= p.END_DATE).OrderByDescending(Function(x) x.EFFECT_DATE).Take(1).DefaultIfEmpty
                        From wow In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = kn.EMPLOYEE_ID And f.IS_MISSION = -1 And f.EFFECT_DATE <= p.END_DATE).OrderByDescending(Function(x) x.EFFECT_DATE).Take(1).DefaultIfEmpty
                        From oe In Context.AT_TIME_PERIOD.Where(Function(f) f.OBJ_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.DECISION_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.PERIOD_ID = _filter.PERIOD_ID

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.p.WORKING_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .WORKING_MEAL = p.p.WORKING_MEAL,
                                       .LATE = p.p.LATE,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TOTAL_W_NOSALARY = p.p.TOTAL_W_NOSALARY,
                                       .TOTAL_W_SALARY = p.p.TOTAL_W_SALARY,
                                        .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .WORKING_DA = p.p.WORKING_DA,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .WORKING_KLD = p.p.WORKING_KLD,
                                       .WORKING_TN = p.p.WORKING_TN,
                                      .WORKING_DEDUCT = p.p.WORKING_DEDUCT,
                                      .C_AL_T = p.p.C_AL_T,
                                      .OBJECT_EMPLOYEE_ID = p.wo.OBJECT_EMPLOYEE_ID,
                                      .OBJECT_EMPLOYEE_ID_KN = p.wow.OBJECT_EMPLOYEE_ID,
                                      .TOTAL_FACTOR1 = p.p.TOTAL_FACTOR1,
                                      .TOTAL_FACTOR1_5 = p.p.TOTAL_FACTOR1_5,
                                      .TOTAL_FACTOR1_8 = p.p.TOTAL_FACTOR1_8,
                                      .TOTAL_FACTOR2 = p.p.TOTAL_FACTOR2,
                                      .TOTAL_FACTOR2_1 = p.p.TOTAL_FACTOR2_1,
                                      .TOTAL_FACTOR2_7 = p.p.TOTAL_FACTOR2_7,
                                      .TOTAL_FACTOR3 = p.p.TOTAL_FACTOR3,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .END_DATE = p.p.END_DATE,
                                      .TOTAL_FACTOR3_9 = p.p.TOTAL_FACTOR3_9,
                                       .TOTAL_W_H = p.p.TOTAL_W_H,
                                       .HOURS_OLD = p.p.HOURS_OLD,
                                       .HOURS_NEW = p.p.HOURS_NEW,
                                       .WORKING_WFH = p.p.WORKING_WFH,
                                       .WORKING_WFH_COM = p.p.WORKING_WFH_COM,
                                       .VIOLATE_DAY = p.p.VIOLATE_DAY,
                                       .VIOLATE_TOTAL = p.p.VIOLATE_TOTAL,
                                       .IS_LOCK_NAME = If(p.p.IS_LOCK = -1, "X", "")})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If _filter.OBJECT_EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.OBJECT_EMPLOYEE_ID = _filter.OBJECT_EMPLOYEE_ID Or f.OBJECT_EMPLOYEE_ID_KN = _filter.OBJECT_EMPLOYEE_ID)
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_WFH.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_WFH = _filter.WORKING_WFH)
            End If
            If _filter.WORKING_WFH_COM.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_WFH_COM = _filter.WORKING_WFH_COM)
            End If
            If _filter.WORKING_R.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.WORKING_ADD.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_ADD = _filter.WORKING_ADD)
            End If
            If _filter.LATE.HasValue Then
                lst = lst.Where(Function(f) f.LATE = _filter.LATE)
            End If
            If _filter.COMEBACKOUT.HasValue Then
                lst = lst.Where(Function(f) f.COMEBACKOUT = _filter.COMEBACKOUT)
            End If
            If _filter.TOTAL_W_SALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_SALARY = _filter.TOTAL_W_SALARY)
            End If
            If _filter.TOTAL_W_NOSALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_NOSALARY = _filter.TOTAL_W_NOSALARY)
            End If
            If _filter.WORKING_MEAL.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_MEAL = _filter.WORKING_MEAL)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.FROM_DATE >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.END_DATE >= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal _param As ParamDTO, ByVal codecase As String, ByVal lstEmployee As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            'Using cls As New DataAccess.NonQueryData
            '    cls.ExecuteSQL("DELETE FROM SE_EMPLOYEE_CHOSEN S WHERE  UPPER(S.USING_USER) ='" + log.Username.ToUpper + "'")
            'End Using
            'Dim dDay = Date.Now
            'For Each emp As Decimal? In lstEmployee
            '    Dim objNew As New SE_EMPLOYEE_CHOSEN
            '    objNew.EMPLOYEE_ID = emp
            '    objNew.USING_USER = log.Username.ToUpper
            '    objNew.WORKINGDAY = dDay
            '    dDay = dDay.AddDays(1)
            '    Context.SE_EMPLOYEE_CHOSEN.AddObject(objNew)
            'Next
            'Context.SaveChanges()
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = _param.PERIOD_ID
            LOG_AT(_param, log, lstEmployee, "TỔNG HỢP BẢNG CÔNG TỔNG HỢP", obj, _param.ORG_ID)
            Using cls As New DataAccess.NonQueryData
                If codecase = "ctrlTimesheetSummary_case1" Then
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIMESHEET_MONTHLY_HOSE",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = _param.PERIOD_ID,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Else
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_PERIOD_ID = _param.PERIOD_ID,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_FROM_DATE = _param.FROMDATE,
                                                         .P_TO_DATE = _param.ENDDATE,
                                                         .P_OBJ_EMPLOYEE_ID = _param.EMP_OBJ})
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog)
        Try
            Select Case sType
                Case "BEYOND_STANDARD"
                    Using cls As New DataAccess.QueryData
                        cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                         New With {.P_USERNAME = log.Username.ToUpper,
                                                   .P_ORGID = _validate.ORG_ID,
                                                   .P_ISDISSOLVE = _validate.IS_DISSOLVE})
                    End Using
                    Dim query = (From p In Context.AT_TIME_TIMESHEET_MONTHLY
                                 From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                                 From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And
                                                                          f.USERNAME.ToUpper = log.Username.ToUpper)
                                 Where p.PERIOD_ID = _validate.PERIOD_ID And
                                 (p.TOTAL_WORKING IsNot Nothing AndAlso
                                  (p.TOTAL_WORKING - If(p.WORKING_V Is Nothing, 0, p.WORKING_V)) > po.PERIOD_STANDARD))
                    Return (query.Count = 0)

            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ActiveTIME_TIMESHEET_MONTHLY(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean
        Dim lstData As List(Of AT_TIME_TIMESHEET_MONTHLY)
        Try
            lstData = (From p In Context.AT_TIME_TIMESHEET_MONTHLY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_LOCK = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteTimesheetMonthly(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of AT_TIME_TIMESHEET_MONTHLY)
        Try
            lstData = (From p In Context.AT_TIME_TIMESHEET_MONTHLY Where lstID.Contains(p.ID)).ToList
            For Each item In lstData
                Context.AT_TIME_TIMESHEET_MONTHLY.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#Region "Công tháng PORTAL"
    Public Function GetTimeSheetForEmp_Month(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                   ByVal _param As ParamDTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            '
            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From lockM In Context.AT_ORG_PERIOD.Where(Function(f) f.STATUSCOLEX = 0 And f.PERIOD_ID = p.PERIOD_ID And f.ORG_ID = p.ORG_ID)
                        From ot In Context.AT_TIME_TIMESHEET_OT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID And f.SALARY_ID = p.SALARY_ID_NEW And f.IS_LOCK = -1).DefaultIfEmpty
                        From oe In Context.AT_TIME_PERIOD.Where(Function(f) f.OBJ_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) f.ID = p.DECISION_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = w.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.p.WORKING_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .WORKING_MEAL = p.p.WORKING_MEAL,
                                       .LATE = p.p.LATE,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TOTAL_W_NOSALARY = p.p.TOTAL_W_NOSALARY,
                                       .TOTAL_W_SALARY = p.p.TOTAL_W_SALARY,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .WORKING_DA = p.p.WORKING_DA,
                                       .OBJECT_ATTENDANCE = p.p.OBJECT_ATTENDANCE,
                                       .OBJECT_ATTENDANCE_NAME = p.obj_att.NAME_VN,
                                       .MIN_OUT_WORK = p.p.MIN_OUT_WORK,
                                       .MIN_ON_LEAVE = p.p.MIN_ON_LEAVE,
                                       .MIN_DEDUCT = p.p.MIN_DEDUCT,
                                       .MIN_LATE = p.p.MIN_LATE,
                                       .MIN_LATE_EARLY = p.p.MIN_LATE_EARLY,
                                       .MIN_IN_WORK = p.p.MIN_IN_WORK,
                                       .MIN_DEDUCT_WORK = p.p.MIN_DEDUCT_WORK,
                                       .MIN_OUT_WORK_DEDUCT = p.p.MIN_OUT_WORK_DEDUCT,
                                       .MIN_EARLY = p.p.MIN_EARLY,
                                       .WORKING_KLD = p.p.WORKING_KLD,
                                       .WORKING_TN = p.p.WORKING_TN,
                                       .WORKING_DEDUCT = p.p.WORKING_DEDUCT,
                                       .C_AL_T = p.p.C_AL_T,
                                       .OBJECT_EMPLOYEE_ID = p.e.OBJECT_EMPLOYEE_ID,
                                       .TOTAL_FACTOR1 = p.ot.TOTAL_FACTOR1,
                                       .TOTAL_FACTOR1_5 = p.ot.TOTAL_FACTOR1_5,
                                       .TOTAL_FACTOR1_8 = p.ot.TOTAL_FACTOR1_8,
                                       .TOTAL_FACTOR2 = p.ot.TOTAL_FACTOR2,
                                       .TOTAL_FACTOR2_1 = p.ot.TOTAL_FACTOR2_1,
                                       .TOTAL_FACTOR2_7 = p.ot.TOTAL_FACTOR2_7,
                                       .TOTAL_FACTOR3 = p.ot.TOTAL_FACTOR3,
                                       .TOTAL_FACTOR3_9 = p.ot.TOTAL_FACTOR3_9,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .END_DATE = p.p.END_DATE,
                                       .TOTAL_W_H = p.p.TOTAL_W_H,
                                       .OT_DAY = p.ot.OT_DAY,
                                       .OT_NIGHT = p.ot.OT_NIGHT,
                                       .OT_WEEKEND_DAY = p.ot.OT_WEEKEND_DAY,
                                       .OT_WEEKEND_NIGHT = p.ot.OT_WEEKEND_NIGHT,
                                       .OT_HOLIDAY_DAY = p.ot.OT_HOLIDAY_DAY,
                                       .OT_HOLIDAY_NIGHT = p.ot.OT_HOLIDAY_NIGHT,
                                       .TOTAL_NB1 = p.ot.TOTAL_NB1,
                                       .TOTAL_NB1_5 = p.ot.TOTAL_NB1_5,
                                       .NUMBER_FACTOR_CP = p.ot.NUMBER_FACTOR_CP})



            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If IsNumeric(_filter.PERIOD_ID) Then
                lst = lst.Where(Function(f) f.PERIOD_ID = _filter.PERIOD_ID)
            End If
            If IsNumeric(_param.ORG_ID) Then
                lst = lst.Where(Function(f) f.ORG_ID = _param.ORG_ID)
            End If
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.OBJECT_EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.OBJECT_EMPLOYEE_ID = _filter.OBJECT_EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_F.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_R.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.WORKING_ADD.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_ADD = _filter.WORKING_ADD)
            End If
            If _filter.LATE.HasValue Then
                lst = lst.Where(Function(f) f.LATE = _filter.LATE)
            End If
            If _filter.COMEBACKOUT.HasValue Then
                lst = lst.Where(Function(f) f.COMEBACKOUT = _filter.COMEBACKOUT)
            End If
            If _filter.TOTAL_W_SALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_SALARY = _filter.TOTAL_W_SALARY)
            End If
            If _filter.TOTAL_W_NOSALARY.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_W_NOSALARY = _filter.TOTAL_W_NOSALARY)
            End If
            If _filter.WORKING_MEAL.HasValue Then
                lst = lst.Where(Function(f) f.WORKING_MEAL = _filter.WORKING_MEAL)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.FROM_DATE >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.END_DATE >= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#End Region

#Region "Lam thêm"
    Public Function GetRegisterOT(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_OT_REGISTRATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OT_TYPE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From s In Context.SE_USER.Where(Function(f) f.USERNAME = p.MODIFIED_BY).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.STATUS = 1 And s.IS_APP <> 0

            'If _filter.IS_NB IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.IS_NB = _filter.IS_NB)
            'End If

            'If _filter.TYPE_INPUT IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.TYPE_INPUT = _filter.TYPE_INPUT)
            'End If

            Dim lst = query.Select(Function(p) New AT_OT_REGISTRATIONDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_ID = p.t.ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .DEPARTMENT_NAME = p.o.NAME_VN,
                                       .DEPARTMENT_ID = p.o.ID,
                                       .SIGN_ID = p.p.SIGN_ID,
                                       .SIGN_CODE = p.p.SIGN_CODE,
                                       .OT_TYPE_ID = p.p.OT_TYPE_ID,
                                       .OT_TYPE_NAME = p.typeot.NAME_VN,
                                       .REGIST_DATE = p.p.REGIST_DATE,
                                       .ID_REGGROUP = p.p.ID_REGGROUP,
                                       .FROM_AM = p.p.FROM_AM,
                                       .TO_AM = p.p.TO_AM,
                                       .FROM_PM = p.p.FROM_PM,
                                       .TO_PM = p.p.TO_PM,
                                       .FROM_AM_MN = p.p.FROM_AM_MN,
                                       .TO_AM_MN = p.p.TO_AM_MN,
                                       .FROM_PM_MN = p.p.FROM_PM_MN,
                                       .TO_PM_MN = p.p.TO_PM_MN,
                                       .FROM_HOUR_AM = "",
                                       .TO_HOUR_AM = "",
                                       .FROM_HOUR_PM = "",
                                       .TO_HOUR_PM = "",
                                       .TOTAL_OT = p.p.TOTAL_OT,
                                       .OT_100 = p.p.OT_100,
                                       .OT_150 = p.p.OT_150,
                                       .OT_180 = p.p.OT_180,
                                       .OT_200 = p.p.OT_200,
                                       .OT_210 = p.p.OT_210,
                                       .OT_270 = p.p.OT_270,
                                       .OT_300 = p.p.OT_300,
                                       .OT_370 = p.p.OT_370,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = p.status.NAME_VN,
                                       .REASON = p.p.REASON,
                                       .NOTE = p.p.NOTE,
                                       .IS_DELETED = p.p.IS_DELETED,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .MODIFIED_NAME = p.s.FULLNAME,
                                       .CREATED_DATE = p.p.CREATED_DATE})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    Else
            '        lst = lst.Where(Function(f) f.TER_LAST_DATE <= Date.Now)
            '    End If
            'End If
            'Dim dateNow = Date.Now.Date
            'If Not _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            'End If
            If _filter.REGIST_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE >= _filter.REGIST_DATE_FROM)
            End If
            If _filter.REGIST_DATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE <= _filter.REGIST_DATE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                lst = lst.Where(Function(f) f.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.REGIST_DATE.HasValue Then
                lst = lst.Where(Function(f) f.REGIST_DATE = _filter.REGIST_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            'For Each item As AT_OT_REGISTRATIONDTO In lst
            '    item.FROM_HOUR_AM = If(item.FROM_AM IsNot Nothing, item.FROM_AM.ToString + ":" + If(item.FROM_AM_MN = 30, item.FROM_AM_MN.ToString, "00"), "")
            '    item.TO_HOUR_AM = If(item.TO_AM IsNot Nothing, item.TO_AM.ToString + ":" + If(item.TO_AM_MN = 30, item.TO_AM_MN.ToString, "00"), "")
            '    item.FROM_HOUR_PM = If(item.FROM_PM IsNot Nothing, item.FROM_PM.ToString + ":" + If(item.FROM_PM_MN = 30, item.FROM_PM_MN.ToString, "00"), "")
            '    item.TO_HOUR_PM = If(item.TO_PM IsNot Nothing, item.TO_PM.ToString + ":" + If(item.TO_PM_MN = 30, item.TO_PM_MN.ToString, "00"), "")
            'Next
            Dim tempLst = lst.ToList
            For i = 0 To tempLst.Count - 1
                tempLst(i).FROM_HOUR_AM = If(tempLst(i).FROM_AM IsNot Nothing, tempLst(i).FROM_AM.ToString + ":" + If(tempLst(i).FROM_AM_MN = 30, tempLst(i).FROM_AM_MN.ToString, "00"), "")
                tempLst(i).TO_HOUR_AM = If(tempLst(i).TO_AM IsNot Nothing, tempLst(i).TO_AM.ToString + ":" + If(tempLst(i).TO_AM_MN = 30, tempLst(i).TO_AM_MN.ToString, "00"), "")
                tempLst(i).FROM_HOUR_PM = If(tempLst(i).FROM_PM IsNot Nothing, tempLst(i).FROM_PM.ToString + ":" + If(tempLst(i).FROM_PM_MN = 30, tempLst(i).FROM_PM_MN.ToString, "00"), "")
                tempLst(i).TO_HOUR_PM = If(tempLst(i).TO_PM IsNot Nothing, tempLst(i).TO_PM.ToString + ":" + If(tempLst(i).TO_PM_MN = 30, tempLst(i).TO_PM_MN.ToString, "00"), "")
            Next

            'tempLst = tempLst.OrderBy(Sorts)
            'tempLst = tempLst.Skip(PageIndex * PageSize).Take(PageSize)

            Total = tempLst.Count
            Return tempLst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        Try

            Dim query = From p In Context.AT_REGISTER_OT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HS_OT And f.TYPE_CODE = "HS_OT").DefaultIfEmpty
                        From typeot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_OT And f.TYPE_CODE = "TYPE_OT").DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_REGISTER_OTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TYPE_OT = p.typeot.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .FROM_HOUR = p.p.FROM_HOUR,
                                       .TO_HOUR = p.p.TO_HOUR,
                                       .BREAK_HOUR = p.p.BREAK_HOUR,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .IS_NB = p.p.IS_NB,
                                       .NOTE = p.p.NOTE,
                                       .HS_OT = p.p.HS_OT,
                                       .HS_OT_NAME = p.ot.CODE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?

        'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
        Dim emp = (From e In Context.HU_EMPLOYEE
                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                   Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And
                   (e.TER_EFFECT_DATE Is Nothing Or
                    (e.TER_EFFECT_DATE IsNot Nothing And
                     e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                   Order By w.EFFECT_DATE Descending
                   Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT
                           Where r.EMPLOYEE_ID = employee_id And
                           r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                           objRegisterOT.FROM_HOUR < r.TO_HOUR And
                           objRegisterOT.TO_HOUR > r.FROM_HOUR And
                           r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_OT = objRegisterOT.TYPE_OT
            Else
                Dim objRegisterOTData As New AT_REGISTER_OT
                objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                objRegisterOTData.EMPLOYEE_ID = employee_id
                objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                objRegisterOTData.NOTE = objRegisterOT.NOTE
                objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                objRegisterOTData.HOUR = objRegisterOT.HOUR
                objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                objRegisterOTData.APPROVE_ID = 0
                objRegisterOTData.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                           From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                           Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And
                           (e.TER_EFFECT_DATE Is Nothing Or
                            (e.TER_EFFECT_DATE IsNot Nothing And
                             e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                           Order By w.EFFECT_DATE Descending
                           Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR = r.TO_HOUR And
                              objRegisterOT.TO_HOUR = r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    Dim obj = (From r In Context.AT_REGISTER_OT
                               Where r.EMPLOYEE_ID = employee_id And
                               r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                               objRegisterOT.FROM_HOUR = r.TO_HOUR And
                               objRegisterOT.TO_HOUR = r.FROM_HOUR And
                               r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).FirstOrDefault

                    obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                    obj.TO_HOUR = objRegisterOT.TO_HOUR
                    obj.HOUR = objRegisterOT.HOUR
                    obj.NOTE = objRegisterOT.NOTE
                    obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    obj.IS_NB = objRegisterOT.IS_NB
                    obj.HS_OT = objRegisterOT.HS_OT
                    obj.TYPE_OT = objRegisterOT.TYPE_OT
                    obj.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                Else
                    Dim objRegisterOTData As New AT_REGISTER_OT
                    objRegisterOTData.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                    objRegisterOTData.EMPLOYEE_ID = employee_id
                    objRegisterOTData.WORKINGDAY = objRegisterOT.WORKINGDAY
                    objRegisterOTData.FROM_HOUR = objRegisterOT.FROM_HOUR
                    objRegisterOTData.TO_HOUR = objRegisterOT.TO_HOUR
                    objRegisterOTData.NOTE = objRegisterOT.NOTE
                    objRegisterOTData.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                    objRegisterOTData.HOUR = objRegisterOT.HOUR
                    objRegisterOTData.HS_OT = objRegisterOT.HS_OT
                    objRegisterOTData.IS_NB = objRegisterOT.IS_NB
                    objRegisterOTData.TYPE_OT = objRegisterOT.TYPE_OT
                    objRegisterOTData.APPROVE_ID = 0
                    objRegisterOTData.BREAK_HOUR = objRegisterOT.BREAK_HOUR
                    Context.AT_REGISTER_OT.AddObject(objRegisterOTData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRegisterOTData As New AT_REGISTER_OT With {.ID = objRegisterOT.ID}
        Try
            Dim exists = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_REGISTER_OT Where r.ID = objRegisterOT.ID).FirstOrDefault
                obj.WORKINGDAY = objRegisterOT.WORKINGDAY
                obj.FROM_HOUR = objRegisterOT.FROM_HOUR
                obj.TO_HOUR = objRegisterOT.TO_HOUR
                obj.HS_OT = objRegisterOT.HS_OT
                obj.TYPE_INPUT = objRegisterOT.TYPE_INPUT
                obj.IS_NB = objRegisterOT.IS_NB
                obj.HOUR = objRegisterOT.HOUR
                obj.NOTE = objRegisterOT.NOTE
                obj.TYPE_OT = objRegisterOT.TYPE_OT
                obj.BREAK_HOUR = objRegisterOT.BREAK_HOUR
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateRegisterOT(ByVal _validate As AT_REGISTER_OTDTO)
        Dim query
        Try
            If _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_REGISTER_OT
                             Where p.WORKINGDAY = _validate.WORKINGDAY).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_OT_REGISTRATION)
        Try
            lstl = (From p In Context.AT_OT_REGISTRATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_OT_REGISTRATION.DeleteObject(lstl(index))

                Dim idReggrounp As Decimal = lstl(index).ID_REGGROUP
                Dim process = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = idReggrounp And p.PROCESS_TYPE = "OVERTIME").FirstOrDefault
                If process IsNot Nothing Then
                    Context.PROCESS_APPROVED_STATUS.DeleteObject(process)
                End If
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim emp = (From e In Context.HU_EMPLOYEE
                   From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                   Where e.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And
                   (e.TER_EFFECT_DATE Is Nothing Or
                    (e.TER_EFFECT_DATE IsNot Nothing And
                     e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                   Order By w.EFFECT_DATE Descending
                   Select w).FirstOrDefault
        If emp IsNot Nothing Then
            employee_id = emp.EMPLOYEE_ID
            org_id = emp.ORG_ID
        Else
            Exit Function
        End If
        Try
            Dim exists

            If objRegisterOT.ID IsNot Nothing Then
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT And
                          r.ID <> objRegisterOT.ID).Any
            Else
                exists = (From r In Context.AT_REGISTER_OT
                          Where r.EMPLOYEE_ID = employee_id And
                          r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                          objRegisterOT.FROM_HOUR < r.TO_HOUR And
                          objRegisterOT.TO_HOUR > r.FROM_HOUR And
                          r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
            End If
            If exists Then ' có dữ liệu trả lại false không cho phép đăng ký tiếp 
                Return False
            Else 'không  có dữ liệu trả lại true cho phép đăng ký tiếp 
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim objData As New AT_REGISTER_OTDTO
        Try
            For index = 0 To objRegisterOTList.Count - 1
                objData = objRegisterOTList(index)
                'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = objRegisterOT.EMPLOYEE_CODE And p.WORK_STATUS <> 257 And p.TER_LAST_DATE > objRegisterOT.WORKINGDAY).FirstOrDefault
                Dim emp = (From e In Context.HU_EMPLOYEE
                           From w In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                           Where e.EMPLOYEE_CODE = objData.EMPLOYEE_CODE And e.JOIN_DATE <= objRegisterOT.WORKINGDAY And
                           (e.TER_EFFECT_DATE Is Nothing Or
                            (e.TER_EFFECT_DATE IsNot Nothing And
                             e.TER_EFFECT_DATE >= objRegisterOT.WORKINGDAY)) And w.EFFECT_DATE <= objRegisterOT.WORKINGDAY
                           Order By w.EFFECT_DATE Descending
                           Select w).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.EMPLOYEE_ID
                    org_id = emp.ORG_ID
                Else
                    Continue For
                End If
                Dim exists = (From r In Context.AT_REGISTER_OT
                              Where r.EMPLOYEE_ID = employee_id And
                              r.WORKINGDAY = objRegisterOT.WORKINGDAY And
                              objRegisterOT.FROM_HOUR < r.TO_HOUR And
                              objRegisterOT.TO_HOUR > r.FROM_HOUR And
                              r.TYPE_INPUT = objRegisterOT.TYPE_INPUT).Any
                If exists Then
                    strEmployeeCode = strEmployeeCode & objData.EMPLOYEE_CODE & ","
                End If
            Next
            If Not strEmployeeCode.Equals("") Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO), ByVal status_id As Decimal) As Boolean
        Dim lstID As New List(Of Decimal?)
        Try
            lstID = (From p In lstData Select p.ID).ToList
            Dim lstl = (From p In Context.AT_REGISTER_OT Where lstID.Contains(p.ID)).ToList
            For Each obj In lstl
                Dim objData = (From p In lstData
                               Where p.ID = obj.ID).FirstOrDefault
                If objData IsNot Nothing Then
                    obj.APPROVE_DATE = Date.Now
                    obj.APPROVE_HOUR = objData.APPROVE_HOUR
                    obj.APPROVE_ID = status_id
                End If
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                WHERE_CONDITION &= " AND ( UPPER(E.EMPLOYEE_CODE) LIKE  '%" & _filter.EMPLOYEE_CODE.ToUpper() & "%'"
                WHERE_CONDITION &= " OR UPPER(E.FULLNAME_VN) LIKE '%" & _filter.EMPLOYEE_CODE.ToUpper() & "%')"
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                WHERE_CONDITION &= " AND UPPER(E.FULLNAME_VN) LIKE '%" & _filter.FULLNAME.ToUpper() & "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                WHERE_CONDITION &= " AND UPPER(T.NAME_VN) LIKE '%" & _filter.TITLE_NAME.ToUpper() & "%'"
            End If

            If _filter.IS_TER Then
                WHERE_CONDITION &= " AND E.WORK_STATUS = 257"
            End If

            If IsNumeric(_filter.STATUS) Then
                WHERE_CONDITION &= " AND NVL(OT.STATUS,0) = " & _filter.STATUS
            End If
            If IsNumeric(_filter.IS_DELETED) Then
                WHERE_CONDITION &= " AND NVL(OT.IS_DELETED,0) = " & _filter.IS_DELETED
            End If
            If IsNumeric(_filter.DK_PORTAL) Then
                WHERE_CONDITION &= " AND NVL(OT.DK_PORTAL,0) = " & _filter.DK_PORTAL
            End If
            If IsNumeric(_filter.CONFIRM_OT_TT) Then
                If _filter.CONFIRM_OT_TT = 100000000 Then ' GIÁ TRỊ ĐỂ GÁN ĐIỀU KIỆN NÀY <> 1
                    WHERE_CONDITION &= " AND NVL(OT.CONFIRM_OT_TT,0) <> 1"
                Else
                    WHERE_CONDITION &= " AND NVL(OT.CONFIRM_OT_TT,0) = " & _filter.CONFIRM_OT_TT
                End If
            End If

            If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                WHERE_CONDITION &= " AND UPPER(OT_TYPE.NAME_VN) LIKE  '%" & _filter.OT_TYPE_NAME.ToUpper() & "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                WHERE_CONDITION &= " AND UPPER(SH.CODE) LIKE  '%" & _filter.SIGN_CODE.ToUpper() & "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                WHERE_CONDITION &= " AND UPPER(ORG.NAME_VN) LIKE  '%" & _filter.DEPARTMENT_NAME.ToUpper() & "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.HS_NAME) Then
                'WHERE_CONDITION &= " AND to_char(HS.EXCEPTION_COEFF) LIKE  '%" & _filter.HS_NAME.ToUpper() & "%'"
            End If

            If _filter.REGIST_DATE.HasValue Then
                WHERE_CONDITION &= " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') >= '" & _filter.REGIST_DATE.Value.ToString("yyyyMMdd") & "' "
            End If
            WHERE_CONDITION &= " ORDER BY " & Sorts

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.GET_REG_DATA",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = _param.ORG_ID,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_STARTDATE = _filter.REGIST_DATE_FROM,
                                                         .P_ENDDATE = _filter.REGIST_DATE_TO,
                                                         .P_WHERE_CONDITION = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function
    Public Function EXPORT_AT_OT_REGISTRATION(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_LIST.EXPORT_AT_OT_REGISTRATION",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_AT_OT_REGISTRATION(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.INPORT_AT_OT_REGISTRATION",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function GetOTCoeffOver(ByVal _regDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.GET_TIME_OT_COEFF_OVER",
                                           New With {.P_DATE_SIGN = _regDate,
                                                     .P_CUR = cls.OUT_CURSOR}) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function OtGetValueOfMonth(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_VALUE_OT_YEAR", New List(Of Object)(New Object() {P_EMP_ID, P_DATE, P_HOURS, P_TYPE_OT})) ' FALSE : no datatable

                Return dtData(0)("KQ")
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function OtGetValueOfYear(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_VALUE_OT_YEAR", New List(Of Object)(New Object() {P_EMP_ID, P_DATE, P_HOURS, P_TYPE_OT})) ' FALSE : no datatable

                Return dtData(0)("KQ")
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function ModifyPortalOtReg(ByVal objOT As AT_OT_REGISTRATIONDTO, ByVal P_HS_OT As String, ByVal P_USERNAME As String, ByVal P_ORG_OT_ID As Integer) As Decimal
        Try
            Dim count As Decimal = 0
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = objOT.ID,
                                    .P_EMP_ID = objOT.EMPLOYEE_ID,
                                    .P_OT_TYOE_ID = objOT.OT_TYPE_ID,
                                    .P_REASON = objOT.REASON,
                                    .P_NOTE = objOT.NOTE,
                                    .P_FROM_AM = objOT.FROM_AM,
                                    .P_FROM_AM_MN = objOT.FROM_AM_MN,
                                    .P_TO_AM = objOT.TO_AM,
                                    .P_TO_AM_MN = objOT.TO_AM_MN,
                                    .P_FROM_PM = objOT.FROM_PM,
                                    .P_FROM_PM_MN = objOT.FROM_PM_MN,
                                    .P_TO_PM = objOT.TO_PM,
                                    .P_TO_PM_MN = objOT.TO_PM_MN,
                                    .P_REGISTER_DATE = objOT.REGIST_DATE,
                                    .P_SIGN_CODE = objOT.SIGN_CODE,
                                    .P_SIGN_ID = objOT.SIGN_ID,
                                    .P_TOTAL = objOT.TOTAL_OT,
                                    .P_STATUS = objOT.STATUS,
                                    .P_PM_FROMHOURS_AFTERCHECK = objOT.PM_FROMHOURS_AFTERCHECK,
                                    .P_PM_TOHOURS_AFTERCHECK = objOT.PM_TOHOURS_AFTERCHECK,
                                    .P_IS_PASS_DAY = objOT.IS_PASS_DAY,
                                    .P_HOURS_TOTAL_AM = objOT.HOURS_TOTAL_AM,
                                    .P_HOURS_TOTAL_PM = objOT.HOURS_TOTAL_PM,
                                    .P_HOURS_TOTAL_DAY = objOT.HOURS_TOTAL_DAY,
                                    .P_HOURS_TOTAL_NIGHT = objOT.HOURS_TOTAL_NIGHT,
                                    .P_DK_PORTAL = objOT.DK_PORTAL,
                                    .P_HS_OT = P_HS_OT,
                                    .P_USERNAME = P_USERNAME,
                                    .P_ORG_OT_ID = P_ORG_OT_ID,
                                    .P_CREATED_BY_EMP = objOT.CREATED_BY_EMP,
                                    .P_BY_ANOTHER = objOT.BY_ANOTHER,
                                    .P_TOTAL_DAY_TT = objOT.TOTAL_DAY_TT,
                                    .P_OUT = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.UPDATE_OT_REG", obj)

                If Not dtData Is Nothing Then
                    count = CDec(dtData.Rows(0)(0))
                End If
            End Using
            Return count
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function

    Public Function GetOTReigistrationByID(ByVal id As Decimal) As AT_OT_REGISTRATIONDTO
        Try
            Dim query = (From p In Context.AT_OT_REGISTRATION
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                         Where p.ID = id
                         Select New AT_OT_REGISTRATIONDTO With {
                              .ID = p.ID,
                              .ID_REGGROUP = p.ID_REGGROUP,
                              .EMPLOYEE_ID = p.EMPLOYEE_ID,
                              .TITLE_ID = e.TITLE_ID,
                              .REGIST_DATE = p.REGIST_DATE,
                              .TOTAL_OT = p.TOTAL_OT,
                              .SIGN_ID = p.SIGN_ID})
            Return query.FirstOrDefault()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try
    End Function
#End Region
#Region "Xác nhận đăng ký làm thêm"
    Public Function CONFIRM_DECLARES_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_AT_PROCESS.SUBMIT_CONFIRM_DECLARES_OT",
                                 New With {.P_LST_ID = params.P_LST_ID,
                                           .P_REGISTER_FROM = params.REGIST_DATE_FROM,
                                           .P_REGISTER_TO = params.REGIST_DATE_TO,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function
    Public Function CHANGE_CONFIRM_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_AT_PROCESS.CHANGE_CONFIRM_OT",
                                 New With {.P_LST_ID = params.P_LST_ID,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function
    Public Function CALCAULATE_CONFIRM_DECLARES_OT(ByVal paramsOT As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, Optional ByVal params As ParamDTO = Nothing) As Boolean
        Try
            '.P_LST_ID = If(paramsOT.P_LST_ID IsNot Nothing OrElse paramsOT.P_LST_ID <> "", paramsOT.P_LST_ID.TrimEnd(","), ""),
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.CAL_CONFIRM_DECLARES_OT",
                                                             New With {.P_USERNAME = log.Username.Trim.ToUpper,
                                                                       .P_ORG_ID = params.ORG_ID,
                                                                       .P_ISDISSOLVE = params.IS_DISSOLVE,
                                                                       .P_FROM_DATE = paramsOT.REGIST_DATE_FROM,
                                                                       .P_TO_DATE = paramsOT.REGIST_DATE_TO,
                                                                       .P_OUT = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso IsNumeric(dtData.Rows(0)("RS")) AndAlso dtData.Rows(0)("RS") = 1 Then
                    Return True
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function
#End Region
#Region "Khai bao cong com"
    Public Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            Dim dateNow = Date.Now.Date
            If _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.e.WORK_STATUS = 257)
            '    If _filter.WORKINGDAY.HasValue Then
            '        lst = lst.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
            '    End If
            'End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.PRICE.HasValue Then
                lst = lst.Where(Function(f) f.PRICE = _filter.PRICE)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TIME_RICE)
        Try
            lstData = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        Try

            Dim query = From p In Context.AT_TIME_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_TIME_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.p.ORG_ID,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .ACTFLG = p.p.ACTFLG,
                                       .PRICE = p.p.PRICE,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any

            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objDelareRice.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                obj.PRICE = objDelareRice.PRICE
                obj.WORKINGDAY = objDelareRice.WORKINGDAY
            Else
                Dim objDelareRiceData As New AT_TIME_RICE
                objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.ID = objDelareRice.EMPLOYEE_ID).FirstOrDefault
                objDelareRiceData.EMPLOYEE_ID = emp.ID
                employee_id = emp.ID
                org_id = emp.ORG_ID
                objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                objDelareRiceData.ORG_ID = emp.ORG_ID
                objDelareRiceData.PRICE = objDelareRice.PRICE
                objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                objDelareRiceData.NOTE = objDelareRice.NOTE
                Context.AT_TIME_RICE.AddObject(objDelareRiceData)
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_TIME_RICEDTO
        Try
            For index = 0 To objDelareRiceList.Count - 1
                objData = objDelareRiceList(index)
                Dim exists = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).Any
                If exists Then
                    Dim obj = (From r In Context.AT_TIME_RICE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.WORKINGDAY = objDelareRice.WORKINGDAY).FirstOrDefault
                    obj.PRICE = objDelareRice.PRICE
                    obj.WORKINGDAY = objDelareRice.WORKINGDAY
                Else
                    Dim objDelareRiceData As New AT_TIME_RICE
                    objDelareRiceData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_RICE.EntitySet.Name)
                    objDelareRiceData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                    objDelareRiceData.ORG_ID = objData.ORG_ID ' trường hợp này phải lưu org hiện tại của nhân viên
                    objDelareRiceData.WORKINGDAY = objDelareRice.WORKINGDAY
                    objDelareRiceData.PRICE = objDelareRice.PRICE
                    objDelareRiceData.STAFF_RANK_ID = objDelareRice.STAFF_RANK_ID
                    objDelareRiceData.TITLE_ID = objDelareRice.TITLE_ID
                    objDelareRiceData.NOTE = objDelareRice.NOTE
                    Context.AT_TIME_RICE.AddObject(objDelareRiceData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateDelareRice(ByVal _validate As AT_TIME_RICEDTO)
        Dim query
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = _validate.ID And r.EMPLOYEE_ID = _validate.EMPLOYEE_ID And
                                                                                          r.WORKINGDAY = _validate.WORKINGDAY).Any

            If _validate.WORKINGDAY.HasValue Then
                If exists And _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_RICE
                             Where p.ID <> _validate.ID And p.WORKINGDAY = _validate.WORKINGDAY And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).Any
                Else
                    query = (From p In Context.AT_TIME_RICE
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.WORKINGDAY = _validate.WORKINGDAY).Any
                End If
                If query Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareRiceData As New AT_TIME_RICE With {.ID = objDelareRice.ID}
        Try
            Dim exists = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_TIME_RICE Where r.ID = objDelareRice.ID).FirstOrDefault
                obj.ORG_ID = objDelareRice.ORG_ID
                obj.PRICE = objDelareRice.PRICE
                obj.NOTE = objDelareRice.NOTE
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_TIME_RICE)
        Try
            lstl = (From p In Context.AT_TIME_RICE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_TIME_RICE.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region
#Region "quan ly cham cong bu tru"
    Public Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_OFFSETTING_TIMEKEEPING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_BT).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_OFFFSETTINGDTO With {
                                      .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .FROMDATE = p.p.FROMDATE,
                                       .TODATE = p.p.TODATE,
                                       .TYPE_BT = p.p.TYPE_BT,
                                       .REMARK = p.p.REMARK,
                                       .TYPE_NAME = p.ot.NAME_VN,
                                       .MINUTES_BT = p.p.MINUTES_BT,
                                        .WORK_STATUS = p.e.WORK_STATUS,
                                        .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE <= Date.Now)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROMDATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.FROMDATE >= _filter.FROMDATE)
            End If
            If _filter.TODATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.TODATE >= _filter.TODATE)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                lst = lst.Where(Function(f) f.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lst_Tem As List(Of AT_OFFFSETTINGDTO) = lst.ToList
            Dim objData As New AT_OFFFSETTINGDTO
            For index = 0 To lst_Tem.Count - 1
                objData = lst_Tem(index)

                Dim dtCount As DataTable = CountTimeKeeping_Emp(objData.ID.ToString())
                If (dtCount.Rows.Count > 1) Then
                    lst_Tem(index).EMPLOYEE_CODE = "Nhiều nhân viên"
                    lst_Tem(index).FULLNAME_VN = "Nhiều nhân viên"
                    lst_Tem(index).TITLE_NAME = "Nhiều nhân viên"
                    lst_Tem(index).ORG_NAME = "Nhiều nhân viên"
                End If
            Next

            Return lst_Tem

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function CountTimeKeeping_Emp(ByVal group_id As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteSQL("SELECT COUNT(TE.EMPLOYEE_ID) FROM AT_OFFSETTING_TIMEKEEPING_EMP TE WHERE TE.GROUP_ID ='" + group_id + "' GROUP BY TE.GROUP_ID,TE.EMPLOYEE_ID")

                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeTimeKeepingID(ByVal ComId As Decimal) As List(Of AT_OFFFSETTING_EMPDTO)
        Try
            Dim q = (From d In Context.AT_OFFSETTING_TIMEKEEPING
                     From CE In Context.ATV_OFFSETTING.Where(Function(e) e.GROUP_ID = d.ID)
                     From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = CE.EMPLOYEE_ID)
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                     From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                     Where CE.GROUP_ID = ComId
                     Select New AT_OFFFSETTING_EMPDTO With {.EMPLOYEE_ID = CE.EMPLOYEE_ID,
                                                   .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                   .FULLNAME_VN = e.FULLNAME_VN,
                                                   .ORG_NAME = o.NAME_VN,
                                                   .TITLE_ID = e.TITLE_ID,
                                                    .ORG_ID = e.ORG_ID,
                                                   .TITLE_NAME = t.NAME_VN}).ToList
            Return q.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO
        Try

            Dim query = From p In Context.AT_OFFSETTING_TIMEKEEPING
                        From ce In Context.AT_OFFSETTING_TIMEKEEPING_EMP.Where(Function(f) f.GROUP_ID = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ce.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_BT).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_OFFFSETTINGDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .TYPE_BT = p.p.TYPE_BT,
                                       .TYPE_NAME = p.ot.NAME_VN,
                                       .MINUTES_BT = p.p.MINUTES_BT,
                                       .REMARK = p.p.REMARK,
                                       .FROMDATE = p.p.FROMDATE,
                                       .TODATE = p.p.TODATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "Khai báo điều chỉnh thâm niên phép"
    Public Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From mod_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MODIFY_TYPE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .JOIN_DATE = p.p.JOIN_DATE,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .MODIFY_TYPE_ID = p.p.MODIFY_TYPE_ID,
                                       .MODIFY_TYPE_NAME = p.mod_type.NAME_VN,
                                       .END_MONTH_TN = p.p.END_MONTH_TN,
                                       .EXPIRE_YEAR = p.p.EXPIRE_YEAR,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .START_DATE = p.p.START_DATE,
                                       .END_DATE = p.p.END_DATE})

            'If _filter.IS_TERMINATE Then
            '    query = query.Where(Function(f) f.e.WORK_STATUS = 257 And f.e.TER_LAST_DATE <= Date.Now)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.ADJUST_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_TN = _filter.ADJUST_MONTH_TN)
            End If
            If _filter.ADJUST_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_ENTITLEMENT = _filter.ADJUST_ENTITLEMENT)
            End If
            If _filter.ADJUST_MONTH_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.ADJUST_MONTH_ENTITLEMENT = _filter.ADJUST_MONTH_ENTITLEMENT)
            End If

            If _filter.START_MONTH_TN.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_TN = _filter.START_MONTH_TN)
            End If
            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.START_MONTH_EXTEND.HasValue Then
                lst = lst.Where(Function(f) f.START_MONTH_EXTEND = _filter.START_MONTH_EXTEND)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_TN) Then
                lst = lst.Where(Function(f) f.REMARK_TN.ToLower().Contains(_filter.REMARK_TN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK_ENTITLEMENT) Then
                lst = lst.Where(Function(f) f.REMARK_ENTITLEMENT.ToLower().Contains(_filter.REMARK_ENTITLEMENT.ToLower()))
            End If
            If _filter.YEAR_NB.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_NB = _filter.YEAR_NB)
            End If
            If _filter.YEAR_ENTITLEMENT.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_ENTITLEMENT = _filter.YEAR_ENTITLEMENT)
            End If
            If _filter.MONTH_EXTENSION_NB.HasValue Then
                lst = lst.Where(Function(f) f.MONTH_EXTENSION_NB = _filter.MONTH_EXTENSION_NB)
            End If
            If _filter.COM_PAY.HasValue Then
                lst = lst.Where(Function(f) f.COM_PAY = _filter.COM_PAY)
            End If
            If _filter.ENT_PAY.HasValue Then
                lst = lst.Where(Function(f) f.ENT_PAY = _filter.ENT_PAY)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        Try

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_DECLARE_ENTITLEMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_DECLARE_ENTITLEMENTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .YEAR = p.p.YEAR,
                                       .YEAR_NB = p.p.YEAR_NB,
                                       .YEAR_ENTITLEMENT = p.p.YEAR_ENTITLEMENT,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ADJUST_MONTH_TN = p.p.ADJUST_MONTH_TN,
                                       .REMARK_TN = p.p.REMARK_TN,
                                       .JOIN_DATE = p.p.JOIN_DATE,
                                       .ADJUST_ENTITLEMENT = p.p.ADJUST_ENTITLEMENT,
                                       .ADJUST_MONTH_ENTITLEMENT = p.p.ADJUST_MONTH_ENTITLEMENT,
                                       .REMARK_ENTITLEMENT = p.p.REMARK_ENTITLEMENT,
                                       .START_MONTH_TN = p.p.START_MONTH_TN,
                                       .START_MONTH_EXTEND = p.p.START_MONTH_EXTEND,
                                       .ADJUST_NB = p.p.ADJUST_NB,
                                       .START_MONTH_NB = p.p.START_MONTH_NB,
                                       .REMARK_NB = p.p.REMARK_NB,
                                       .MONTH_EXTENSION_NB = p.p.MONTH_EXTENSION_NB,
                                       .COM_PAY = p.p.COM_PAY,
                                       .ENT_PAY = p.p.ENT_PAY,
                                       .MODIFY_TYPE_ID = p.p.MODIFY_TYPE_ID,
                                       .END_MONTH_TN = p.p.END_MONTH_TN,
                                       .EXPIRE_YEAR = p.p.EXPIRE_YEAR,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .START_DATE = p.p.START_DATE,
                                       .END_DATE = p.p.END_DATE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objOffsettingData As New AT_OFFSETTING_TIMEKEEPING

        Try
            ' thêm kỷ luật
            objOffsettingData.ID = Utilities.GetNextSequence(Context, Context.AT_OFFSETTING_TIMEKEEPING.EntitySet.Name)
            objOffSetting.ID = objOffsettingData.ID
            objOffsettingData.REMARK = objOffSetting.REMARK
            objOffsettingData.MINUTES_BT = objOffSetting.MINUTES_BT
            objOffsettingData.FROMDATE = objOffSetting.FROMDATE
            objOffsettingData.EMPLOYEE_ID = objOffSetting.EMPLOYEE_ID
            objOffsettingData.TODATE = objOffSetting.TODATE
            objOffsettingData.TYPE_BT = objOffSetting.TYPE_BT
            objOffsettingData.CREATED_DATE = DateTime.Now
            objOffsettingData.CREATED_BY = log.Username
            objOffsettingData.CREATED_LOG = log.ComputerName
            objOffsettingData.MODIFIED_DATE = DateTime.Now
            objOffsettingData.MODIFIED_BY = log.Username
            objOffsettingData.MODIFIED_LOG = log.ComputerName


            Context.AT_OFFSETTING_TIMEKEEPING.AddObject(objOffsettingData)
            InsertObjectType(objOffSetting)
            Context.SaveChanges(log)
            gID = objOffsettingData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Private Sub InsertObjectType(ByVal objOffSetting As AT_OFFFSETTINGDTO)
        Dim objDataEmp As AT_OFFSETTING_TIMEKEEPING_EMP
        Dim rep As New AttendanceRepository

        If objOffSetting.OFFFSETTING_EMP IsNot Nothing Then
            'xoa danh sach nhan viên cũ truoc khi insert lại
            Dim objD = (From d In Context.AT_OFFSETTING_TIMEKEEPING_EMP Where d.GROUP_ID = objOffSetting.ID).ToList
            For Each obj As AT_OFFSETTING_TIMEKEEPING_EMP In objD
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.DeleteObject(obj)
            Next

            'insert nhan vien mới
            For Each obj As AT_OFFFSETTING_EMPDTO In objOffSetting.OFFFSETTING_EMP
                objDataEmp = New AT_OFFSETTING_TIMEKEEPING_EMP
                objDataEmp.ID = Utilities.GetNextSequence(Context, Context.AT_OFFSETTING_TIMEKEEPING_EMP.EntitySet.Name)
                objDataEmp.GROUP_ID = objOffSetting.ID
                objDataEmp.EMPLOYEE_ID = obj.EMPLOYEE_ID
                objDataEmp.TITILE_ID = obj.TITLE_ID
                objDataEmp.ORG_ID = obj.ORG_ID
                objDataEmp.TYPE_BT = objOffSetting.TYPE_BT
                objDataEmp.WORKING_DAY = obj.WORKING_DAY
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.AddObject(objDataEmp)
            Next
        End If
    End Sub
    Public Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objOffSettingData As New AT_OFFSETTING_TIMEKEEPING With {.ID = objOffSetting.ID}

        Try
            Context.AT_OFFSETTING_TIMEKEEPING.Attach(objOffSettingData)
            ' sửa kỷ luật
            objOffSettingData.ID = objOffSetting.ID
            objOffSettingData.REMARK = objOffSetting.REMARK
            objOffSettingData.TYPE_BT = objOffSetting.TYPE_BT
            objOffSettingData.MINUTES_BT = objOffSetting.MINUTES_BT
            objOffSettingData.FROMDATE = objOffSetting.FROMDATE
            objOffSettingData.EMPLOYEE_ID = objOffSetting.EMPLOYEE_ID
            objOffSettingData.TODATE = objOffSetting.TODATE
            objOffSettingData.MODIFIED_DATE = DateTime.Now
            objOffSettingData.MODIFIED_BY = log.Username
            objOffSettingData.MODIFIED_LOG = log.ComputerName
            ' thêm quyết định            
            InsertObjectType(objOffSetting)
            Context.SaveChanges(log)
            gID = objOffSettingData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
            If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                If objDelareEntitlementNB.ID IsNot Nothing Then ' trường hợp sửa phải kiểm tra khác id hiện tại
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.ID <> objDelareEntitlementNB.ID And t.YEAR = objDelareEntitlementNB.YEAR).Any
                Else
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            End If
            If checkMonthNB = False And checkMonthNP = False Then
                Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                If exists Then
                    Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                    obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    obj.YEAR = objDelareEntitlementNB.YEAR
                    obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                    obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    obj.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
                    obj.MODIFY_TYPE_ID = objDelareEntitlementNB.MODIFY_TYPE_ID
                    obj.END_MONTH_TN = objDelareEntitlementNB.END_MONTH_TN
                    obj.EXPIRE_YEAR = objDelareEntitlementNB.EXPIRE_YEAR
                    obj.START_DATE = objDelareEntitlementNB.START_DATE
                    obj.END_DATE = objDelareEntitlementNB.END_DATE
                Else
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                    objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                    objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                    objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                    objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                    objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                    objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                    objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                    objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                    objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                    objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    objDelareEntitlementNBData.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
                    objDelareEntitlementNBData.MODIFY_TYPE_ID = objDelareEntitlementNB.MODIFY_TYPE_ID
                    objDelareEntitlementNBData.END_MONTH_TN = objDelareEntitlementNB.END_MONTH_TN
                    objDelareEntitlementNBData.EXPIRE_YEAR = objDelareEntitlementNB.EXPIRE_YEAR
                    objDelareEntitlementNBData.START_DATE = objDelareEntitlementNB.START_DATE
                    objDelareEntitlementNBData.END_DATE = objDelareEntitlementNB.END_DATE
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                End If
                Context.SaveChanges(log)
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            Dim objData As New AT_DECLARE_ENTITLEMENTDTO

            For index = 0 To objDelareEntitlementlist.Count - 1
                objData = objDelareEntitlementlist(index)
                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.MONTH_EXTENSION_NB IsNot Nothing Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If objDelareEntitlementNB.START_MONTH_EXTEND IsNot Nothing Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = objData.EMPLOYEE_ID And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = objDelareEntitlementNB.YEAR).Any
                End If
            Next


            If checkMonthNB = False And checkMonthNP = False Then
                For index = 0 To objDelareEntitlementlist.Count - 1
                    objData = objDelareEntitlementlist(index)
                    Dim exists = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.ID = objDelareEntitlementNB.ID).Any
                    If exists Then
                        Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.ID = objDelareEntitlementNB.ID And r.EMPLOYEE_ID = objData.EMPLOYEE_ID).FirstOrDefault
                        obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        obj.YEAR = objDelareEntitlementNB.YEAR
                        obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        obj.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        obj.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        obj.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        obj.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                        obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                    Else
                        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                        objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                        objDelareEntitlementNBData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                        objDelareEntitlementNBData.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                        objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                        objDelareEntitlementNBData.YEAR = objDelareEntitlementNB.YEAR
                        objDelareEntitlementNBData.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                        objDelareEntitlementNBData.YEAR_ENTITLEMENT = objDelareEntitlementNB.YEAR_ENTITLEMENT
                        objDelareEntitlementNBData.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                        objDelareEntitlementNBData.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                        objDelareEntitlementNBData.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                        objDelareEntitlementNBData.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                        objDelareEntitlementNBData.ADJUST_NB = objDelareEntitlementNB.ADJUST_NB
                        objDelareEntitlementNBData.START_MONTH_NB = objDelareEntitlementNB.START_MONTH_NB
                        objDelareEntitlementNBData.REMARK_NB = objDelareEntitlementNB.REMARK_NB
                        objDelareEntitlementNBData.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                        objDelareEntitlementNBData.COM_PAY = objDelareEntitlementNB.COM_PAY
                        objDelareEntitlementNBData.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                        Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                    End If
                    Context.SaveChanges(log)
                Next
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Try
            For Each row As DataRow In dtData.Rows
                Dim employee_id As New Decimal
                Dim year As New Decimal
                employee_id = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                year = Utilities.Obj2Decima(row("YEAR"))

                ' check nghỉ bù chỉ được gia hạn 1 lần trong năm
                If row("MONTH_EXTENSION_NB") IsNot Nothing Then
                    checkMonthNB = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.MONTH_EXTENSION_NB IsNot Nothing And t.YEAR = year).Any
                End If
                ' check nghỉ phép chỉ được gia hạn 1 lần trong năm
                If row("START_MONTH_EXTEND") IsNot Nothing Then
                    checkMonthNP = (From t In Context.AT_DECLARE_ENTITLEMENT Where t.EMPLOYEE_ID = employee_id And t.START_MONTH_EXTEND IsNot Nothing And t.YEAR = year).Any
                End If
            Next

            If checkMonthNB = False And checkMonthNP = False Then
                For Each row As DataRow In dtData.Rows
                    Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT
                    objDelareEntitlementNBData.ID = Utilities.GetNextSequence(Context, Context.AT_DECLARE_ENTITLEMENT.EntitySet.Name)
                    objDelareEntitlementNBData.EMPLOYEE_ID = Utilities.Obj2Decima(row("EMPLOYEE_ID"))
                    objDelareEntitlementNBData.ADJUST_MONTH_TN = Utilities.Obj2Decima(row("ADJUST_MONTH_TN"))
                    objDelareEntitlementNBData.ADJUST_MONTH_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_MONTH_ENTITLEMENT"))
                    objDelareEntitlementNBData.ADJUST_ENTITLEMENT = Utilities.Obj2Decima(row("ADJUST_ENTITLEMENT"))
                    objDelareEntitlementNBData.YEAR = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_NB = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.YEAR_ENTITLEMENT = Utilities.Obj2Decima(row("YEAR"))
                    objDelareEntitlementNBData.START_MONTH_TN = Utilities.Obj2Decima(row("START_MONTH_TN"))
                    objDelareEntitlementNBData.START_MONTH_EXTEND = Utilities.Obj2Decima(row("START_MONTH_EXTEND"))
                    objDelareEntitlementNBData.REMARK_TN = row("REMARK_TN")
                    objDelareEntitlementNBData.REMARK_ENTITLEMENT = Utilities.Obj2Decima(row("REMARK_ENTITLEMENT"))
                    objDelareEntitlementNBData.ADJUST_NB = Utilities.Obj2Decima(row("ADJUST_NB"))
                    objDelareEntitlementNBData.START_MONTH_NB = Utilities.Obj2Decima(row("START_MONTH_NB"))
                    objDelareEntitlementNBData.REMARK_NB = row("REMARK_NB")
                    objDelareEntitlementNBData.MONTH_EXTENSION_NB = Utilities.Obj2Decima(row("MONTH_EXTENSION_NB"))
                    objDelareEntitlementNBData.COM_PAY = Utilities.Obj2Decima(row("COM_PAY"))
                    objDelareEntitlementNBData.ENT_PAY = Utilities.Obj2Decima(row("ENT_PAY"))
                    Context.AT_DECLARE_ENTITLEMENT.AddObject(objDelareEntitlementNBData)
                    Context.SaveChanges(log)
                Next
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDelareEntitlementNBData As New AT_DECLARE_ENTITLEMENT With {.ID = objDelareEntitlementNB.ID}
        Try
            Dim exists = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).Any
            If exists Then
                Dim obj = (From r In Context.AT_DECLARE_ENTITLEMENT Where r.EMPLOYEE_ID = objDelareEntitlementNB.EMPLOYEE_ID).FirstOrDefault
                obj.ADJUST_MONTH_TN = objDelareEntitlementNB.ADJUST_MONTH_TN
                obj.ADJUST_MONTH_ENTITLEMENT = objDelareEntitlementNB.ADJUST_MONTH_ENTITLEMENT
                obj.ADJUST_ENTITLEMENT = objDelareEntitlementNB.ADJUST_ENTITLEMENT
                obj.START_MONTH_TN = objDelareEntitlementNB.START_MONTH_TN
                obj.YEAR = objDelareEntitlementNB.YEAR
                obj.YEAR_NB = objDelareEntitlementNB.YEAR_NB
                obj.YEAR_ENTITLEMENT = objDelareEntitlementNBData.YEAR_ENTITLEMENT
                obj.START_MONTH_EXTEND = objDelareEntitlementNB.START_MONTH_EXTEND
                obj.REMARK_TN = objDelareEntitlementNB.REMARK_TN
                obj.REMARK_ENTITLEMENT = objDelareEntitlementNB.REMARK_ENTITLEMENT
                obj.MONTH_EXTENSION_NB = objDelareEntitlementNB.MONTH_EXTENSION_NB
                obj.COM_PAY = objDelareEntitlementNB.COM_PAY
                obj.ENT_PAY = objDelareEntitlementNB.ENT_PAY
                obj.JOIN_DATE = objDelareEntitlementNB.JOIN_DATE
            Else
                Return False
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_DECLARE_ENTITLEMENT)
        Try
            lstl = (From p In Context.AT_DECLARE_ENTITLEMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_DECLARE_ENTITLEMENT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_OFFSETTING_TIMEKEEPING)
        Dim lsttt As List(Of AT_OFFSETTING_TIMEKEEPING_EMP)
        Try
            lstl = (From p In Context.AT_OFFSETTING_TIMEKEEPING Where lstID.Contains(p.ID)).ToList
            lsttt = (From x In Context.AT_OFFSETTING_TIMEKEEPING_EMP Where lstID.Contains(x.GROUP_ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_OFFSETTING_TIMEKEEPING.DeleteObject(lstl(index))
            Next
            For index = 0 To lsttt.Count - 1
                Context.AT_OFFSETTING_TIMEKEEPING_EMP.DeleteObject(lsttt(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthThamNien(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_TN IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_TN = _validate.START_MONTH_TN _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthPhepNam(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.ADJUST_MONTH_ENTITLEMENT IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ADJUST_MONTH_ENTITLEMENT = _validate.ADJUST_MONTH_ENTITLEMENT _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateMonthNghiBu(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO)
        Dim query
        Try
            If _validate.START_MONTH_NB IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DECLARE_ENTITLEMENT
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.START_MONTH_NB = _validate.START_MONTH_NB _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Bang tong hop công cơm"
    Public Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = P_ORG_ID,
                                                         .P_PERIOD_ID = p_period_id,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE})
                Return True
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_SUMMARY_RICE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_EMPLOYEE_NAME = param.VN_FULLNAME,
                                                         .P_ORG_NAME = param.ORG_NAME,
                                                         .P_TITLE_NAME = param.TITLE_NAME,
                                                         .P_STAFF_RANK_NAME = param.STAFF_RANK_NAME,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
        Try
            Dim query = From p In Context.AT_TIME_TIMESHEET_RICE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ID = obj.ID
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_RICEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .nday_rice = p.p.NDAY_RICE,
                                       .total_rice = p.p.TOTAL_RICE,
                                       .total_rice_declare = p.p.TOTAL_RICE_DECLARE,
                                       .total_rice_price = p.p.TOTAL_RICE_PRICE,
                                       .rice_edit = p.p.RICE_EDIT,
                                       .ORG_ID = p.p.ORG_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetRice(ByVal objTimeSheetDaily As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objTimeSheetDaily.EMPLOYEE_ID And r.PERIOD_ID = objTimeSheetDaily.PERIOD_ID).FirstOrDefault

            If TimeSheetDaily IsNot Nothing Then
                TimeSheetDaily.RICE_EDIT = objTimeSheetDaily.rice_edit
                TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objTimeSheetDaily.rice_edit
                Context.SaveChanges(log)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            TimeSheetDaily.RICE_EDIT = objLeave.rice_edit
            TimeSheetDaily.TOTAL_RICE = TimeSheetDaily.TOTAL_RICE_DECLARE + TimeSheetDaily.TOTAL_RICE_PRICE + objLeave.rice_edit
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim TimeSheetDaily = (From r In Context.AT_TIME_TIMESHEET_RICE Where r.EMPLOYEE_ID = objLeave.EMPLOYEE_ID And r.PERIOD_ID = objLeave.PERIOD_ID).FirstOrDefault
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký công"


    Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        Try

            Dim query = From p In Context.AT_ENTITLEMENT
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
                        Order By p.ID Descending
            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .CUR_HAVE = p.CUR_HAVE,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_REMAIN = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALCULATOR_DAY",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .p_TYPE_MANUAL = P_TYPE_MANUAL,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                                        ByVal P_FROM_DATE As Date,
                                        ByVal P_TO_DATE As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_DAY_LEAVE_OLD",
                                           New With {.P_FROM_DATE = P_FROM_DATE,
                                                     .P_TO_DATE = P_TO_DATE,
                                                     .p_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPNAM",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_YEAR = P_YEAR,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal P_YEAR As Integer,
                                    ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_PHEPBU",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                                     .P_YEAR = P_YEAR,
                                                     .P_TYPE_LEAVE_ID = P_TYPE_LEAVE_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        Try

            Dim query = From p In Context.AT_COMPENSATORY
                        Where p.EMPLOYEE_ID = _id And p.YEAR = _year
            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .TOTAL_HAVE = p.TOTAL_HAVE,
                                       .CUR_HAVE = p.CUR_HAVE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)
        Dim objData As New List(Of AT_LEAVESHEETDTO)
        Try
            For index = 0 To lstEmpID.Count - 1
                Dim employeeID As Decimal = lstEmpID(index).EMPLOYEE_ID
                Dim query = From e In Context.HU_EMPLOYEE
                            From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                            From t In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty
                            From p In Context.AT_ENTITLEMENT.Where(Function(F) F.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            From b In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                            Where p.EMPLOYEE_ID = employeeID And p.YEAR = _year
                If query IsNot Nothing Then
                    Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                          .EMPLOYEE_ID = p.e.ID,
                                          .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                          .VN_FULLNAME = p.e.FULLNAME_VN,
                                          .TITLE_NAME = p.t.NAME_VN,
                                          .ORG_ID = p.e.ORG_ID,
                                          .ORG_NAME = p.o.NAME_VN,
                                          .BALANCE_NOW = p.p.CUR_HAVE,
                                          .NBCL = p.b.CUR_HAVE}).FirstOrDefault
                    If (lst IsNot Nothing) Then
                        objData.Add(lst)
                    End If
                End If
            Next

            Return objData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO
        Try

            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(F) F.ID = p.MANUAL_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                    .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .NOTE = p.p.NOTE,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim emp = (From p In Context.HU_EMPLOYEE
                       Where p.EMPLOYEE_CODE = objLeave.EMPLOYEE_CODE And p.IS_KIEM_NHIEM Is Nothing
                       Select p).FirstOrDefault()
            'Dim dayNum As Decimal = If(objLeave.STATUS_SHIFT = -1 Or objLeave.STATUS_SHIFT = 0, 1, 0.5)
            Dim statusShift As Decimal = If(objLeave.STATUS_SHIFT = -1 Or objLeave.STATUS_SHIFT = 0, 0, objLeave.STATUS_SHIFT)
            If emp IsNot Nothing Then
                Dim query = From p In Context.AT_LEAVESHEET_DETAIL
                            Where p.EMPLOYEE_ID = emp.ID And p.MANUAL_ID = objLeave.MANUAL_ID _
                            And p.LEAVE_DAY = objLeave.LEAVE_FROM And p.STATUS_SHIFT = statusShift

                If query.ToList.Count > 0 Then
                    Return True
                End If
                Dim atLeave As New AT_LEAVESHEET
                atLeave.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                atLeave.EMPLOYEE_ID = emp.ID
                atLeave.LEAVE_FROM = objLeave.LEAVE_FROM
                atLeave.LEAVE_TO = objLeave.LEAVE_TO
                atLeave.MANUAL_ID = objLeave.MANUAL_ID
                atLeave.DAY_NUM = objLeave.DAY_NUM
                atLeave.NOTE = objLeave.NOTE
                atLeave.STATUS = 1 'phe duyet
                atLeave.IS_APP = -1
                atLeave.IMPORT = -1
                Context.AT_LEAVESHEET.AddObject(atLeave)
                Dim atLeaveDT As New AT_LEAVESHEET_DETAIL
                atLeaveDT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                atLeaveDT.EMPLOYEE_ID = emp.ID
                atLeaveDT.LEAVESHEET_ID = atLeave.ID
                atLeaveDT.MANUAL_ID = objLeave.MANUAL_ID
                atLeaveDT.LEAVE_DAY = objLeave.LEAVE_FROM
                atLeaveDT.DAY_NUM = objLeave.DAY_NUM
                atLeaveDT.STATUS_SHIFT = statusShift
                Context.AT_LEAVESHEET_DETAIL.AddObject(atLeaveDT)
            End If
            Context.SaveChanges(log)
            Return (True)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertLeaveSheetList(ByVal objLeaveList As List(Of AT_LEAVESHEETDTO), ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim fromdate As Date?
        Dim fromdateNext As Date?
        Dim objDataList As New AT_LEAVESHEETDTO
        Try

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateLeaveSheet(ByVal _validate As AT_LEAVESHEETDTO)
        Dim query
        Try

            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeaveSheet(ByVal objLeave As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable
        Dim dtDataError As DataTable = dtData.Clone
        Dim dtDataSave As New DataTable
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalPhep As Object
        Dim totalBu As Decimal = 0
        Dim rowError As DataRow
        Dim rowDataSave As DataRow
        Dim isError As Boolean = False
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim employee_id As Decimal?
        Dim org_id As Decimal?
        Dim irow = 8
        Try
            dtDataSave.Columns.Add("EMPLOYEE_CODE")
            dtDataSave.Columns.Add("MANUAL_ID")
            dtDataSave.Columns.Add("STATUS_SHIFT_VALUE")
            'dtDataSave.Columns.Add("MORNING_ID")
            'dtDataSave.Columns.Add("AFTERNOON_ID")
            dtDataSave.Columns.Add("TOTAL_DAY_ENT", GetType(Decimal)) ' tổng ngày nghỉ phép
            dtDataSave.Columns.Add("TOTAL_DAY_COM", GetType(Decimal)) ' tổng ngày nghỉ bù

            For Each row As DataRow In dtData.Rows
                Dim empCode As String = row("EMPLOYEE_CODE")
                Dim fromDate As Date
                DateTime.TryParseExact(row("LEAVE_DAY"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, fromDate)
                Dim toDate As Date
                DateTime.TryParseExact(row("LEAVE_DAY"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, toDate)
                toDate = toDate.AddDays(1)
                Dim manualId As Decimal = Utilities.Obj2Decima(row("MANUAL_ID"))
                Dim statusShift As Decimal = If(row("STATUS_SHIFT_VALUE") = String.Empty, -1, Utilities.Obj2Decima(row("STATUS_SHIFT_VALUE")))
                'Dim monningId As Decimal = Utilities.Obj2Decima(row("MORNING_ID"))
                'Dim afternoonId As Decimal = Utilities.Obj2Decima(row("AFTERNOON_ID"))
                Dim emp = (From e In Context.HU_EMPLOYEE
                           Where e.EMPLOYEE_CODE = empCode And e.JOIN_DATE <= fromDate And
                               (e.TER_EFFECT_DATE Is Nothing Or
                                (e.TER_EFFECT_DATE IsNot Nothing And
                                 e.TER_EFFECT_DATE >= fromDate))
                           Select e).FirstOrDefault
                If emp IsNot Nothing Then
                    employee_id = emp.ID
                    org_id = emp.ORG_ID
                Else
                    Exit For
                End If

                ' tính tổng số ngày nghỉ của 1 nv trừ thứ 7 và chủ nhật.
                rowDataSave = dtDataSave.NewRow
                rowDataSave("EMPLOYEE_CODE") = empCode
                rowDataSave("MANUAL_ID") = manualId
                'rowDataSave("MORNING_ID") = monningId
                'rowDataSave("AFTERNOON_ID") = afternoonId
                If manualId = 5 Then
                    rowDataSave("TOTAL_DAY_ENT") = If(statusShift = -1 Or statusShift = 0, 1, 0.5)
                End If
                If manualId = 6 Then
                    rowDataSave("TOTAL_DAY_COM") = If(statusShift = -1 Or statusShift = 0, 1, 0.5)
                End If
                dtDataSave.Rows.Add(rowDataSave)

                ' khởi tạo dòng trong datatable lỗi.
                rowError = dtDataError.NewRow
                '1. lấy phép năm đã đăng ký.
                dtDataUserPHEPNAM = GetTotalPHEPNAM(employee_id, fromDate.Year, Utilities.Obj2Decima(row("MANUAL_ID")))
                '2. lấy phép năm được phép nghỉ trong năm.
                at_entilement = GetPhepNam(employee_id, fromDate.Year)
                '3 phép bù được phép nghỉ trong năm.
                at_compensatory = GetNghiBu(employee_id, fromDate.Year)
                '4. tổng số ngày đăng ký của nhân viên trên file import.
                'totalDayRes = GetTotalDAY(employee_id, 5, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalPhep = dtDataSave.Compute("SUM(TOTAL_DAY_ENT)", "EMPLOYEE_CODE = " & empCode & " AND MANUAL_ID = 5")

                ' nếu là kiểu đăng ký nghỉ phép
                If Utilities.Obj2Decima(row("MANUAL_ID")) = 5 Then
                    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep) < 0 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 251 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 251 Then
                    '    If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                    '        If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalPhep / 2) < -3 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                ' nếu là kiểu đăng ký nghỉ bù
                dtDataUserPHEPBU = GetTotalPHEPBU(employee_id, fromDate.Year, Utilities.Obj2Decima(row("MANUAL_ID")))
                'totalDayRes = GetTotalDAY(employee_id, 6, Date.Parse(row("LEAVE_FROM")), Date.Parse(row("LEAVE_TO")))
                totalBu = Utilities.Obj2Decima(dtDataSave.Compute("SUM(TOTAL_DAY_COM)", "EMPLOYEE_CODE = " & empCode & " AND MANUAL_ID = 6"))

                If Utilities.Obj2Decima(row("MANUAL_ID")) = 6 Then
                    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu) < 0 Then
                            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                            isError = True
                        End If
                    End If
                    'ElseIf Utilities.Obj2Decima(row("MORNING_ID")) = 255 Or Utilities.Obj2Decima(row("AFTERNOON_ID")) = 255 Then
                    '    If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                    '        If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalBu / 2) < 0 Then
                    '            rowError("MANUAL_NAME") = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép."
                    '            isError = True
                    '        End If
                    '    End If
                End If
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtDataError.Rows.Add(rowError)
                End If
                irow = irow + 1
                isError = False
            Next
            Return dtDataError
        Catch ex As Exception
            Throw ex
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

    Public Function ImportLeaveSheet(ByVal dt As DataTable, ByVal log As UserLog) As DataTable
        Dim sw As New StringWriter()
        Dim ds As New DataTable
        Try
            Dim DocXml As String = String.Empty
            dt.WriteXml(sw, False)
            Using cls As New DataAccess.QueryData
                Dim obj
                obj = New With {.P_XML = sw.ToString(),
                                .P_USERNAME = log.Username.ToUpper,
                                .P_RESULT = cls.OUT_CURSOR}
                ds = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_REGISTER_CO", obj)
            End Using
            Return ds
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function

    Public Function CheckDataImport(ByVal dt As DataTable)
        Dim sw As New StringWriter()
        Dim ds As New DataTable
        Try
            Dim DocXml As String = String.Empty
            dt.WriteXml(sw, False)
            Using cls As New DataAccess.QueryData
                Dim obj
                obj = New With {.P_XML = sw,
                                .P_RESULT = cls.OUT_CURSOR}
                ds = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_REGISTER_CO", obj)

            End Using


        Catch ex As Exception
            Return False
        Finally
            sw.Dispose()
            ds.Dispose()
        End Try
    End Function
    Public Function BulkCopyDataTable(ByVal dt As DataTable) As Boolean
        Try
            'Dim requestID = Utilities.GetNextSequence(Context, Context.at_r.EntitySet.Name)
            Using conMng As New ConnectionManager
                Using bulkCopy As New OracleBulkCopy(conMng.GetConnectionString())
                    bulkCopy.DestinationTableName = "AT_LEAVESHEET_TEMP"
                    With bulkCopy.ColumnMappings
                        .Add("EMPLOYEE_CODE", "EMPLOYEE_CODE")
                        .Add("MANUAL_ID", "MANUAL_ID")
                        .Add("LEAVE_DAY", "LEAVE_DAY")
                        .Add("STATUS_SHIFT_VALUE", "STATUS_SHIFT_VALUE")
                        .Add("NOTE", "NOTE")
                    End With
                    conMng.OpenConnection()
                    bulkCopy.WriteToServer(dt)
                    conMng.CloseConnection()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "NGHI BU"
    ''' <summary>
    ''' Tổng hợp nghỉ bù
    ''' </summary>
    ''' <param name="param"></param>
    ''' <param name="listEmployeeId"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CALL_ENTITLEMENT_NB",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_YEAR = param.YEAR,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using
            'LOG_AT(param, log, Nothing, "TỔNG HỢP NGHỈ BÙ", obj, param.ORG_ID)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    ''' <summary>
    ''' Lấy thông tin nghỉ bù theo params
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="_param"></param>
    ''' <param name="Total"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From ot In Context.AT_COMPENSATORY
                        From p In Context.HUV_COMPENSATORY_FUND.Where(Function(f) f.EMPLOYEE_ID = ot.EMPLOYEE_ID And f.YEAR = ot.YEAR).DefaultIfEmpty
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ot.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty()
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty()
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where ot.YEAR = _filter.YEAR

            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.E.WORK_STATUS IsNot Nothing)
            Else
                query = query.Where(Function(f) f.E.WORK_STATUS = 258)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.AL_T1.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T1 = _filter.AL_T1)
            End If
            If _filter.AL_T2.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T2 = _filter.AL_T2)
            End If
            If _filter.AL_T3.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T3 = _filter.AL_T3)
            End If
            If _filter.AL_T4.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T4 = _filter.AL_T4)
            End If
            If _filter.AL_T5.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T5 = _filter.AL_T5)
            End If
            If _filter.AL_T6.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T6 = _filter.AL_T6)
            End If
            If _filter.AL_T7.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T7 = _filter.AL_T7)
            End If
            If _filter.AL_T8.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T8 = _filter.AL_T8)
            End If
            If _filter.AL_T9.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T9 = _filter.AL_T9)
            End If
            If _filter.AL_T10.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T10 = _filter.AL_T10)
            End If
            If _filter.AL_T11.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T11 = _filter.AL_T11)
            End If
            If _filter.AL_T12.HasValue Then
                query = query.Where(Function(f) f.ot.AL_T12 = _filter.AL_T12)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.ot.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            If _filter.CUR_HAVE1.HasValue Then
                query = query.Where(Function(f) f.ot.CUR_HAVE1 = _filter.CUR_HAVE1)
            End If

            Dim lst = query.Select(Function(p) New AT_COMPENSATORYDTO With {
                                       .ID = p.ot.ID,
                                       .EMPLOYEE_ID = p.ot.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .PREV_HAVE = p.ot.PREV_HAVE,
                                       .AL_T1 = p.ot.AL_T1,
                                       .AL_T2 = If(p.ot.AL_T2 IsNot Nothing, p.ot.AL_T2, 0),
                                       .AL_T3 = If(p.ot.AL_T3 IsNot Nothing, p.ot.AL_T3, 0),
                                       .AL_T4 = If(p.ot.AL_T4 IsNot Nothing, p.ot.AL_T4, 0),
                                       .AL_T5 = If(p.ot.AL_T5 IsNot Nothing, p.ot.AL_T5, 0),
                                       .AL_T6 = If(p.ot.AL_T6 IsNot Nothing, p.ot.AL_T6, 0),
                                       .AL_T7 = If(p.ot.AL_T7 IsNot Nothing, p.ot.AL_T7, 0),
                                       .AL_T8 = If(p.ot.AL_T8 IsNot Nothing, p.ot.AL_T8, 0),
                                       .AL_T9 = If(p.ot.AL_T9 IsNot Nothing, p.ot.AL_T9, 0),
                                       .AL_T10 = If(p.ot.AL_T10 IsNot Nothing, p.ot.AL_T10, 0),
                                       .AL_T11 = If(p.ot.AL_T11 IsNot Nothing, p.ot.AL_T11, 0),
                                       .AL_T12 = If(p.ot.AL_T12 IsNot Nothing, p.ot.AL_T12, 0),
                                       .CUR_USED1 = p.ot.CUR_USED1,
                                       .CUR_USED2 = If(p.ot.CUR_USED2 IsNot Nothing, p.ot.CUR_USED2, 0),
                                       .CUR_USED3 = If(p.ot.CUR_USED3 IsNot Nothing, p.ot.CUR_USED3, 0),
                                       .CUR_USED4 = If(p.ot.CUR_USED4 IsNot Nothing, p.ot.CUR_USED4, 0),
                                       .CUR_USED5 = If(p.ot.CUR_USED5 IsNot Nothing, p.ot.CUR_USED5, 0),
                                       .CUR_USED6 = If(p.ot.CUR_USED6 IsNot Nothing, p.ot.CUR_USED6, 0),
                                       .CUR_USED7 = If(p.ot.CUR_USED7 IsNot Nothing, p.ot.CUR_USED7, 0),
                                       .CUR_USED8 = If(p.ot.CUR_USED8 IsNot Nothing, p.ot.CUR_USED8, 0),
                                       .CUR_USED9 = If(p.ot.CUR_USED9 IsNot Nothing, p.ot.CUR_USED9, 0),
                                       .CUR_USED10 = If(p.ot.CUR_USED10 IsNot Nothing, p.ot.CUR_USED10, 0),
                                       .CUR_USED11 = If(p.ot.CUR_USED11 IsNot Nothing, p.ot.CUR_USED11, 0),
                                       .CUR_USED12 = If(p.ot.CUR_USED12 IsNot Nothing, p.ot.CUR_USED12, 0),
                                       .CUR_HAVE1 = p.ot.CUR_HAVE1,
                                       .CUR_HAVE2 = p.ot.CUR_HAVE2,
                                       .CUR_HAVE3 = p.ot.CUR_HAVE3,
                                       .CUR_HAVE4 = p.ot.CUR_HAVE4,
                                       .CUR_HAVE5 = p.ot.CUR_HAVE5,
                                       .CUR_HAVE6 = p.ot.CUR_HAVE6,
                                       .CUR_HAVE7 = p.ot.CUR_HAVE7,
                                       .CUR_HAVE8 = p.ot.CUR_HAVE8,
                                       .CUR_HAVE9 = p.ot.CUR_HAVE9,
                                       .CUR_HAVE10 = p.ot.CUR_HAVE10,
                                       .CUR_HAVE11 = p.ot.CUR_HAVE11,
                                       .CUR_HAVE12 = p.ot.CUR_HAVE12,
                                       .CUR_HAVE = p.ot.CUR_HAVE,
                                       .CUR_USED = p.ot.CUR_USED,
                                       .TOTAL_CUR_HAVE = p.ot.TOTAL_HAVE,
                                       .CREATED_DATE = p.ot.CREATED_DATE,
                                       .EXPIREDATE = p.ot.EXPIREDATE,
                                       .PERMISSION_FUND = p.p.SUM_DAY_NUM,
                                       .PREV_HAVE1 = p.ot.PREV_HAVE1})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstResult = lst.ToList
            For Each item As AT_COMPENSATORYDTO In lstResult

                item.TOTAL_CUR_HAVE = If(item.TOTAL_CUR_HAVE = 0, Nothing, item.TOTAL_CUR_HAVE)
                item.CUR_USED = If(item.CUR_USED = 0, Nothing, item.CUR_USED)
                item.CUR_HAVE = If(item.CUR_HAVE = 0, Nothing, item.CUR_HAVE)

                item.AL_T1 = If(item.AL_T1 = 0, Nothing, item.AL_T1)
                item.AL_T2 = If(item.AL_T2 = 0, Nothing, item.AL_T2)
                item.AL_T3 = If(item.AL_T3 = 0, Nothing, item.AL_T3)
                item.AL_T4 = If(item.AL_T4 = 0, Nothing, item.AL_T4)
                item.AL_T5 = If(item.AL_T5 = 0, Nothing, item.AL_T5)
                item.AL_T6 = If(item.AL_T6 = 0, Nothing, item.AL_T6)
                item.AL_T7 = If(item.AL_T7 = 0, Nothing, item.AL_T7)
                item.AL_T8 = If(item.AL_T8 = 0, Nothing, item.AL_T8)
                item.AL_T9 = If(item.AL_T9 = 0, Nothing, item.AL_T9)
                item.AL_T10 = If(item.AL_T10 = 0, Nothing, item.AL_T10)
                item.AL_T11 = If(item.AL_T11 = 0, Nothing, item.AL_T11)
                item.AL_T12 = If(item.AL_T12 = 0, Nothing, item.AL_T12)

                item.CUR_USED1 = If(item.CUR_USED1 = 0, Nothing, item.CUR_USED1)
                item.CUR_USED2 = If(item.CUR_USED2 = 0, Nothing, item.CUR_USED2)
                item.CUR_USED3 = If(item.CUR_USED3 = 0, Nothing, item.CUR_USED3)
                item.CUR_USED4 = If(item.CUR_USED4 = 0, Nothing, item.CUR_USED4)
                item.CUR_USED5 = If(item.CUR_USED5 = 0, Nothing, item.CUR_USED5)
                item.CUR_USED6 = If(item.CUR_USED6 = 0, Nothing, item.CUR_USED6)
                item.CUR_USED7 = If(item.CUR_USED7 = 0, Nothing, item.CUR_USED7)
                item.CUR_USED8 = If(item.CUR_USED8 = 0, Nothing, item.CUR_USED8)
                item.CUR_USED9 = If(item.CUR_USED9 = 0, Nothing, item.CUR_USED9)
                item.CUR_USED10 = If(item.CUR_USED10 = 0, Nothing, item.CUR_USED10)
                item.CUR_USED11 = If(item.CUR_USED11 = 0, Nothing, item.CUR_USED11)
                item.CUR_USED12 = If(item.CUR_USED12 = 0, Nothing, item.CUR_USED12)
            Next

            Return lstResult
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Cal_TOTAL_CUR_HAVE(ByVal thang As Int32, ByRef item As AT_COMPENSATORYDTO) As Decimal
        Select Case thang
            Case 1
                Return item.PREV_HAVE + item.AL_T1
            Case 2
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2
            Case 3
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3
            Case 4
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4
            Case 5
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 - item.CUR_USED4 + item.AL_T5
            Case 6
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6
            Case 7
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7
            Case 8
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8
            Case 9
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9
            Case 10
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10
            Case 11
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10 - item.CUR_USED10 + item.AL_T11
            Case 12
                Return item.PREV_HAVE + item.AL_T1 - item.CUR_USED1 + item.AL_T2 - item.CUR_USED2 + item.AL_T3 - item.CUR_USED3 + item.AL_T4 _
                        - item.CUR_USED4 + item.AL_T5 - item.CUR_USED5 + item.AL_T6 - item.CUR_USED6 + item.AL_T7 - item.CUR_USED7 + item.AL_T8 _
                        - item.CUR_USED8 + item.AL_T9 - item.CUR_USED9 + item.AL_T10 - item.CUR_USED10 + item.AL_T11 - item.CUR_USED11 + item.AL_T12
            Case Else
                Return 0
        End Select
    End Function

    Public Function Cal_CUR_HAVE(ByVal thang As Int32, ByRef item As AT_COMPENSATORYDTO) As Decimal
        Select Case thang
            Case 1
                Return item.TOTAL_CUR_HAVE - item.CUR_USED1
            Case 2
                Return item.TOTAL_CUR_HAVE - item.CUR_USED2
            Case 3
                Return item.TOTAL_CUR_HAVE - item.CUR_USED3
            Case 4
                Return item.TOTAL_CUR_HAVE - item.CUR_USED4
            Case 5
                Return item.TOTAL_CUR_HAVE - item.CUR_USED5
            Case 6
                Return item.TOTAL_CUR_HAVE - item.CUR_USED6
            Case 7
                Return item.TOTAL_CUR_HAVE - item.CUR_USED7
            Case 8
                Return item.TOTAL_CUR_HAVE - item.CUR_USED8
            Case 9
                Return item.TOTAL_CUR_HAVE - item.CUR_USED9
            Case 10
                Return item.TOTAL_CUR_HAVE - item.CUR_USED10
            Case 11
                Return item.TOTAL_CUR_HAVE - item.CUR_USED11
            Case 12
                Return item.TOTAL_CUR_HAVE - item.CUR_USED12
            Case Else
                Return 0
        End Select
    End Function
#End Region

#Region "Quan ly vao ra"
    Public Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_DATA_INOUT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From s In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.END_DATE)
                End If
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY <= _filter.END_DATE)
            End If
            If _filter.WORKINGDAY.HasValue Then
                query = query.Where(Function(f) f.p.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                query = query.Where(Function(f) f.e.FULLNAME_VN.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            Dim lst = query.Select(Function(p) New AT_DATAINOUTDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .VALIN1 = p.p.VALIN1,
                                       .VALIN2 = p.p.VALIN2,
                                       .VALIN3 = p.p.VALIN3,
                                       .VALIN4 = p.p.VALIN4,
                                       .VALIN5 = p.p.VALIN5,
                                       .VALIN6 = p.p.VALIN6,
                                       .VALIN7 = p.p.VALIN7,
                                       .VALIN8 = p.p.VALIN8,
                                       .VALIN9 = p.p.VALIN9,
                                       .VALIN10 = p.p.VALIN10,
                                       .VALIN11 = p.p.VALIN11,
                                       .VALIN12 = p.p.VALIN12,
                                       .VALIN13 = p.p.VALIN13,
                                       .VALIN14 = p.p.VALIN14,
                                       .VALIN15 = p.p.VALIN15,
                                       .VALIN16 = p.p.VALIN16,
                                       .VALOUT1 = p.p.VALOUT1,
                                       .VALOUT2 = p.p.VALOUT2,
                                       .VALOUT3 = p.p.VALOUT3,
                                       .VALOUT4 = p.p.VALOUT4,
                                       .VALOUT5 = p.p.VALOUT5,
                                       .VALOUT6 = p.p.VALOUT6,
                                       .VALOUT7 = p.p.VALOUT7,
                                       .VALOUT8 = p.p.VALOUT8,
                                       .VALOUT9 = p.p.VALOUT9,
                                       .VALOUT10 = p.p.VALOUT10,
                                       .VALOUT11 = p.p.VALOUT11,
                                       .VALOUT12 = p.p.VALOUT12,
                                       .VALOUT13 = p.p.VALOUT13,
                                       .VALOUT14 = p.p.VALOUT14,
                                       .VALOUT15 = p.p.VALOUT15,
                                       .VALOUT16 = p.p.VALOUT16,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetiTimeByEmployeeCode(ByVal objid As Decimal?) As Decimal?
        Try
            Return (From e In Context.HU_EMPLOYEE
                    Where e.ID = objid
                    Select e.ITIME_ID).FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        End Try
    End Function

    Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                    ByVal log As UserLog) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT
        Try
            'THEM DU LIEU VAO 
            Dim itime = GetiTimeByEmployeeCode(lstDataInout(0).EMPLOYEE_ID)
            If itime IsNot Nothing OrElse itime <> 0 Then
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                For Each objDataInout In lstDataInout
                                    cmd.Parameters.Clear()
                                    Using resource As New DataAccess.OracleCommon()
                                        Dim objParam = New With {.P_TIMEID = itime,
                                                                 .P_VALTIME = objDataInout.VALIN1,
                                                                 .P_USERNAME = log.Username.ToUpper}

                                        If objParam IsNot Nothing Then
                                            For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                                Dim bOut As Boolean = False
                                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                                If para IsNot Nothing Then
                                                    cmd.Parameters.Add(para)
                                                End If
                                            Next
                                        End If
                                        cmd.ExecuteNonQuery()
                                    End Using
                                Next
                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using

                ' UPDATE DATA TO AT_DATAINOUTDTO
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = itime,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = toDate})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDataInoutData As New AT_DATA_INOUT With {.ID = objDataInout.ID}
        Try
            Context.AT_DATA_INOUT.Attach(objDataInoutData)

            Context.SaveChanges(log)
            gID = objDataInoutData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean
        Dim lstDataInoutData As List(Of AT_DATA_INOUT)
        Dim lstIDDataInout As List(Of Decimal?) = (From p In lstDataInout.ToList Select p.ID).ToList
        Dim empId As Decimal?
        Dim iTime As Decimal?
        Try
            ' xoa du lieu ben bang tai du lieu may cham cong
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).ToList
            empId = (From p In Context.AT_DATA_INOUT Where lstIDDataInout.Contains(p.ID)).FirstOrDefault.EMPLOYEE_ID
            ' lay ma quet the
            iTime = GetiTimeByEmployeeCode(empId)

            Dim swipe = (From p In Context.AT_SWIPE_DATA.Where(Function(f) f.ITIME_ID = iTime)).ToList
            For index = 0 To swipe.Count - 1
                Context.AT_SWIPE_DATA.DeleteObject(swipe(index))
            Next

            For index = 0 To lstDataInoutData.Count - 1
                Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteDataInoutById(ByVal id As Decimal?) As Boolean
        Dim lstDataInoutData As AT_DATA_INOUT
        Try
            lstDataInoutData = (From p In Context.AT_DATA_INOUT Where p.ID = id).FirstOrDefault
            Context.AT_DATA_INOUT.DeleteObject(lstDataInoutData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

#End Region

#Region "PHEP NAM"
    Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function CALL_ENTITLEMENT_HOSE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_TNG",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "TỔNG HỢP NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function AT_ENTITLEMENT_PREV_HAVE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        Try
            Dim obj As New AT_ACTION_LOGDTO
            obj.PERIOD_ID = param.PERIOD_ID
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.AT_ENTITLEMENT_PREV_HAVE",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE})
            End Using

            LOG_AT(param, log, listEmployeeId, "KẾT NGHỈ PHÉP", obj, param.ORG_ID)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function CheckPeriodMonth(ByVal year As Integer, ByVal PeriodId As Integer, ByRef PeriodNext As Integer) As Boolean
        Try

            Dim query = (From p In Context.AT_PERIOD
                         Where p.ID = PeriodId And p.MONTH = 12).FirstOrDefault


            If query IsNot Nothing Then

                Dim query1 = (From p In Context.AT_PERIOD
                              Where p.YEAR = year + 1 And p.MONTH = 1).FirstOrDefault
                PeriodNext = query1.ID
                Return False
            Else
                PeriodNext = 0S
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From en In Context.AT_ENTITLEMENT
                        From p In Context.HUV_PERMISSION_FUND.Where(Function(f) f.EMPLOYEE_ID = en.EMPLOYEE_ID And f.YEAR = en.YEAR).DefaultIfEmpty
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = en.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_STAFF_RANK.Where(Function(f) f.ID = E.STAFF_RANK_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where en.YEAR = _filter.YEAR

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.E.WORK_STATUS IsNot Nothing)
            Else
                query = query.Where(Function(f) f.E.WORK_STATUS = 258)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_VN) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.c.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.JOIN_DATE_STATE.HasValue Then
                query = query.Where(Function(f) f.E.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If
            If _filter.JOIN_DATE.HasValue Then
                query = query.Where(Function(f) f.E.JOIN_DATE = _filter.JOIN_DATE)
            End If
            If _filter.SENIORITY.HasValue Then
                query = query.Where(Function(f) f.en.SENIORITY = _filter.SENIORITY)
            End If
            If _filter.SENIORITYHAVE.HasValue Then
                query = query.Where(Function(f) f.en.SENIORITYHAVE = _filter.SENIORITYHAVE)
            End If
            If _filter.ADVANCELEAVE.HasValue Then
                query = query.Where(Function(f) f.en.ADVANCELEAVE = _filter.ADVANCELEAVE)
            End If
            If _filter.PREV_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.PREV_HAVE = _filter.PREV_HAVE)
            End If
            If _filter.PREV_USED.HasValue Then
                query = query.Where(Function(f) f.en.PREV_USED = _filter.PREV_USED)
            End If
            If _filter.PREVTOTAL_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.PREVTOTAL_HAVE = _filter.PREVTOTAL_HAVE)
            End If
            If _filter.QP_YEAR.HasValue Then
                query = query.Where(Function(f) f.en.QP_YEAR = _filter.QP_YEAR)
            End If
            If _filter.WORKING_TIME_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.WORKING_TIME_HAVE = _filter.WORKING_TIME_HAVE)
            End If
            If _filter.BALANCE_WORKING_TIME.HasValue Then
                query = query.Where(Function(f) f.en.BALANCE_WORKING_TIME = _filter.BALANCE_WORKING_TIME)
            End If
            If _filter.EXPIREDATE.HasValue Then
                query = query.Where(Function(f) f.en.EXPIREDATE = _filter.EXPIREDATE)
            End If
            If _filter.ADJUST_MONTH_TN.HasValue Then
                query = query.Where(Function(f) f.en.ADJUST_MONTH_TN = _filter.ADJUST_MONTH_TN)
            End If
            If _filter.CUR_USED1.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED1 = _filter.CUR_USED1)
            End If
            If _filter.CUR_USED2.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED2 = _filter.CUR_USED2)
            End If
            If _filter.CUR_USED3.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED3 = _filter.CUR_USED3)
            End If
            If _filter.CUR_USED4.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED4 = _filter.CUR_USED4)
            End If
            If _filter.CUR_USED5.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED5 = _filter.CUR_USED5)
            End If
            If _filter.CUR_USED6.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED6 = _filter.CUR_USED6)
            End If
            If _filter.CUR_USED7.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED7 = _filter.CUR_USED7)
            End If
            If _filter.CUR_USED8.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED8 = _filter.CUR_USED8)
            End If
            If _filter.CUR_USED9.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED9 = _filter.CUR_USED9)
            End If
            If _filter.CUR_USED10.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED10 = _filter.CUR_USED10)
            End If
            If _filter.CUR_USED11.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED11 = _filter.CUR_USED11)
            End If
            If _filter.CUR_USED12.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED12 = _filter.CUR_USED12)
            End If
            If _filter.CUR_USED.HasValue Then
                query = query.Where(Function(f) f.en.CUR_USED = _filter.CUR_USED)
            End If
            If _filter.CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.CUR_HAVE = _filter.CUR_HAVE)
            End If
            If _filter.TOTAL_CUR_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE = _filter.TOTAL_CUR_HAVE)
            End If

            If _filter.TOTAL_HAVE1.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE1 = _filter.TOTAL_HAVE1)
            End If
            If _filter.TOTAL_HAVE.HasValue Then
                query = query.Where(Function(f) f.en.TOTAL_HAVE = _filter.TOTAL_HAVE)
            End If

            If _filter.PERMISSION_FUND.HasValue Then
                query = query.Where(Function(f) f.p.SUM_DAY_NUM = _filter.PERMISSION_FUND)
            End If
            If _filter.LEAVEOLD_PAYMENT.HasValue Then
                query = query.Where(Function(f) f.en.LEAVEOLD_PAYMENT = _filter.LEAVEOLD_PAYMENT)
            End If
            If _filter.LEAVENEW_PAYMENT.HasValue Then
                query = query.Where(Function(f) f.en.LEAVENEW_PAYMENT = _filter.LEAVENEW_PAYMENT)
            End If
            If _filter.LEAVE_RO.HasValue Then
                query = query.Where(Function(f) f.en.LEAVE_RO = _filter.LEAVE_RO)
            End If
            If _filter.LEAVERO_NUMBER.HasValue Then
                query = query.Where(Function(f) f.en.LEAVERO_NUMBER = _filter.LEAVERO_NUMBER)
            End If
            Dim lst = query.Select(Function(p) New AT_ENTITLEMENTDTO With {
                                       .ID = p.en.ID,
                                       .EMPLOYEE_ID = p.en.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .TITLE_NAME_VN = p.t.NAME_VN,
                                       .STAFF_RANK_NAME = p.c.NAME,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .JOIN_DATE_STATE = p.en.JOIN_DATE_STATE,
                                       .JOIN_DATE = p.E.JOIN_DATE,
                                       .WORKING_TIME_HAVE = p.en.WORKING_TIME_HAVE,
                                       .PREV_HAVE = p.en.PREV_HAVE,
                                       .EXPIREDATE = If(p.en.PREV_HAVE Is Nothing, Nothing, p.en.EXPIREDATE),
                                       .BALANCE_WORKING_TIME = p.en.BALANCE_WORKING_TIME,
                                       .TOTAL_HAVE = p.en.TOTAL_HAVE,
                                       .CUR_USED1 = If(p.en.CUR_USED1 Is Nothing, 0, p.en.CUR_USED1),
                                       .CUR_USED2 = If(p.en.CUR_USED2 IsNot Nothing, p.en.CUR_USED2, 0),
                                       .CUR_USED3 = If(p.en.CUR_USED3 IsNot Nothing, p.en.CUR_USED3, 0),
                                       .CUR_USED4 = If(p.en.CUR_USED4 IsNot Nothing, p.en.CUR_USED4, 0),
                                       .CUR_USED5 = If(p.en.CUR_USED5 IsNot Nothing, p.en.CUR_USED5, 0),
                                       .CUR_USED6 = If(p.en.CUR_USED6 IsNot Nothing, p.en.CUR_USED6, 0),
                                       .CUR_USED7 = If(p.en.CUR_USED7 IsNot Nothing, p.en.CUR_USED7, 0),
                                       .CUR_USED8 = If(p.en.CUR_USED8 IsNot Nothing, p.en.CUR_USED8, 0),
                                       .CUR_USED9 = If(p.en.CUR_USED9 IsNot Nothing, p.en.CUR_USED9, 0),
                                       .CUR_USED10 = If(p.en.CUR_USED10 IsNot Nothing, p.en.CUR_USED10, 0),
                                       .CUR_USED11 = If(p.en.CUR_USED11 IsNot Nothing, p.en.CUR_USED11, 0),
                                       .CUR_USED12 = If(p.en.CUR_USED12 IsNot Nothing, p.en.CUR_USED12, 0),
                                       .CUR_USED = p.en.CUR_USED,
                                       .PREV_USED = If(p.en.PREV_HAVE Is Nothing, Nothing, p.en.PREV_USED),
                                       .PREV_USED1 = If(p.en.PREV_USED1 Is Nothing, 0, p.en.PREV_USED1),
                                       .PREV_USED2 = If(p.en.PREV_USED2 IsNot Nothing, p.en.PREV_USED2, 0),
                                       .PREV_USED3 = If(p.en.PREV_USED3 IsNot Nothing, p.en.PREV_USED3, 0),
                                       .PREV_USED4 = If(p.en.PREV_USED4 IsNot Nothing, p.en.PREV_USED4, 0),
                                       .PREV_USED5 = If(p.en.PREV_USED5 IsNot Nothing, p.en.PREV_USED5, 0),
                                       .PREV_USED6 = If(p.en.PREV_USED6 IsNot Nothing, p.en.PREV_USED6, 0),
                                       .PREV_USED7 = If(p.en.PREV_USED7 IsNot Nothing, p.en.PREV_USED7, 0),
                                       .PREV_USED8 = If(p.en.PREV_USED8 IsNot Nothing, p.en.PREV_USED8, 0),
                                       .PREV_USED9 = If(p.en.PREV_USED9 IsNot Nothing, p.en.PREV_USED9, 0),
                                       .PREV_USED10 = If(p.en.PREV_USED10 IsNot Nothing, p.en.PREV_USED10, 0),
                                       .PREV_USED11 = If(p.en.PREV_USED11 IsNot Nothing, p.en.PREV_USED11, 0),
                                       .PREV_USED12 = If(p.en.PREV_USED12 IsNot Nothing, p.en.PREV_USED12, 0),
                                       .CREATED_DATE = p.en.CREATED_DATE,
                                       .CREATED_BY = p.en.CREATED_BY,
                                       .SENIORITYHAVE = p.en.SENIORITYHAVE,
                                       .TOTAL_HAVE1 = p.en.TOTAL_HAVE1,
                                       .TIME_OUTSIDE_COMPANY = p.en.TIME_OUTSIDE_COMPANY,
                                       .TIME_SENIORITY = p.en.TIME_SENIORITY,
                                       .MONTH_SENIORITY_CHANGE = p.en.MONTH_SENIORITY_CHANGE,
                                       .TIME_SENIORITY_AFTER_CHANGE = p.en.TIME_SENIORITY_AFTER_CHANGE,
                                       .SENIORITY = p.en.SENIORITY,
                                       .PREVTOTAL_HAVE = p.en.PREVTOTAL_HAVE,
                                       .QP_YEAR = p.en.QP_YEAR,
                                       .CUR_HAVE = p.en.CUR_HAVE,
                                       .ADJUST_MONTH_TN = p.en.ADJUST_MONTH_TN,
                                       .PERMISSION_FUND = p.p.SUM_DAY_NUM,
                                       .LEAVEOLD_PAYMENT = p.en.LEAVEOLD_PAYMENT,
                                       .LEAVENEW_PAYMENT = p.en.LEAVENEW_PAYMENT,
                                       .LEAVE_RO = p.en.LEAVE_RO,
                                       .LEAVERO_NUMBER = p.en.LEAVERO_NUMBER})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportEntitlementLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_PERIOD As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_ENTITLEMENT_LEAVE",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER, .P_PERIOD = P_PERIOD})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function IMPORT_OT(ByVal P_DOCXML As String, ByVal P_USERNAME As String, Optional ByVal P_LOG As UserLog = Nothing) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_OT",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = P_USERNAME,
                                           .P_LOG = P_LOG.ComputerName})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "WorkSign"
    Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                'Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKSIGN",
                '                               New With {.P_USERNAME = log.Username.ToUpper,
                '                                         .P_ORG_ID = param.ORG_ID,
                '                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                '                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                '                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                '                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                '                                         .P_PERIOD_ID = param.PERIOD_ID,
                '                                         .P_VN_FULLNAME = param.VN_FULLNAME.ToUpper,
                '                                         .P_TITLE_NAME = param.TITLE_NAME.ToUpper,
                '                                         .P_ORG_NAME = param.ORG_NAME.ToUpper,
                '                                         .P_CUR = cls.OUT_CURSOR,
                '                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKSIGN",
                                              New With {.P_USERNAME = log.Username.ToUpper,
                                                        .P_ORG_ID = param.ORG_ID,
                                                        .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                        .P_PAGE_INDEX = param.PAGE_INDEX,
                                                        .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                        .P_PAGE_SIZE = param.PAGE_SIZE,
                                                        .P_PERIOD_ID = param.PERIOD_ID,
                                                        .P_EMP_OBJ = param.EMP_OBJ,
                                                        .P_START_DATE = param.START_DATE,
                                                        .P_END_DATE = param.END_DATE,
                                                        .P_CUR = cls.OUT_CURSOR,
                                                        .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function
    ''' <summary>
    ''' Thêm mới xếp ca làm việc sử dụng import
    ''' </summary>
    ''' <param name="dtData"></param>
    ''' <param name="period_id"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertWorkSignByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_id).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKSIGN_DATE"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = period_id,
                                                             .P_USERNAME = log.Username.ToUpper + "-" + log.Ip + log.ComputerName,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), Nothing),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), Nothing),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), Nothing),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), Nothing),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), Nothing),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), Nothing),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), Nothing),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), Nothing),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), Nothing),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), Nothing),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), Nothing),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), Nothing),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), Nothing),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), Nothing),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), Nothing),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), Nothing),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), Nothing),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), Nothing),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), Nothing),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), Nothing),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), Nothing),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), Nothing),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), Nothing),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), Nothing),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), Nothing),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), Nothing),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), Nothing),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), Nothing),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), Nothing),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), Nothing),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), Nothing)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.IMPORT_WORKSIGN_DATE"
                                cmd.Parameters.Clear()

                                Dim objParam1 = New With {.P_STARTDATE = start_date,
                                                         .P_ENDDATE = end_date,
                                                         .P_USERNAME = log.Username.ToUpper}
                                'Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
                                '                         .P_ENDDATE = Period.END_DATE.Value,
                                '                         .P_USERNAME = log.Username.ToUpper}

                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Thêm mới xếp ca làm việc
    ''' </summary>
    ''' <param name="objWorkSigns"></param>
    ''' <param name="objWork"></param>
    ''' <param name="p_fromdate"></param>
    ''' <param name="p_endDate"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSign As New AT_WORKSIGNDTO
        Dim p_fromDateBefor As Date = p_fromdate
        Dim CODE_OFF As String = ""
        CODE_OFF = "OFF"
        Try
            For index = 0 To objWorkSigns.Count - 1
                objWorkSign = objWorkSigns(index)
                p_fromdate = p_fromDateBefor
                While p_fromdate <= p_endDate
                    Dim objWorkSignData As New AT_WORKSIGN
                    ' kiem tra da ton tai chua
                    Dim exist = (From c In Context.AT_WORKSIGN
                                 Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And
                                 c.WORKINGDAY = p_fromdate).Any
                    If exist Then

                        Dim query = (From c In Context.AT_WORKSIGN
                                     Where c.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID And
                                     c.WORKINGDAY = p_fromdate).FirstOrDefault

                        Dim shiftIDU = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault
                        Dim shiftOffu = (From f In Context.AT_SHIFT Where f.CODE = CODE_OFF Select f).FirstOrDefault
                        If Not shiftOffu Is Nothing Then
                            If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOffu.ID) Then
                                If shiftIDU.SUNDAY.Value > 0 Then
                                    query.SHIFT_ID = shiftOffu.ID
                                Else
                                    query.SHIFT_ID = objWork.SHIFT_ID
                                End If
                            ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And Not String.IsNullOrEmpty(shiftOffu.ID) Then
                                If shiftIDU.SATURDAY.Value > 0 Then
                                    query.SHIFT_ID = shiftOffu.ID 'shiftIDU.SATURDAY
                                Else
                                    query.SHIFT_ID = objWork.SHIFT_ID
                                End If
                            Else
                                query.SHIFT_ID = objWork.SHIFT_ID
                            End If
                        End If

                        Dim holidayU = (From f In Context.AT_HOLIDAY Where f.WORKINGDAY = p_fromdate Select f).FirstOrDefault
                        If (Not holidayU Is Nothing) Then
                            query.SHIFT_ID = Nothing
                        End If

                        Context.SaveChanges(log)
                        p_fromdate = p_fromdate.AddDays(1)
                        Continue While
                    End If
                    objWorkSignData.ID = Utilities.GetNextSequence(Context, Context.AT_WORKSIGN.EntitySet.Name)
                    objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
                    objWorkSignData.WORKINGDAY = p_fromdate

                    Dim shiftId = (From f In Context.AT_SHIFT Where f.ID = objWork.SHIFT_ID Select f).FirstOrDefault
                    Dim shiftOff = (From f In Context.AT_SHIFT Where f.CODE = CODE_OFF Select f).FirstOrDefault
                    If p_fromdate.DayOfWeek = DayOfWeek.Sunday And Not String.IsNullOrEmpty(shiftOff.ID) Then
                        If shiftId.SUNDAY.Value > 0 Then
                            objWorkSignData.SHIFT_ID = shiftOff.ID
                        Else
                            objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                        End If
                    ElseIf p_fromdate.DayOfWeek = DayOfWeek.Saturday And Not String.IsNullOrEmpty(shiftOff.ID) Then
                        If shiftId.SATURDAY.Value > 0 Then
                            objWorkSignData.SHIFT_ID = shiftOff.ID 'shiftIDU.SATURDAY
                        Else
                            objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                        End If
                    Else
                        objWorkSignData.SHIFT_ID = objWork.SHIFT_ID
                    End If

                    Dim holiday = (From f In Context.AT_HOLIDAY Where f.WORKINGDAY = p_fromdate Select f).FirstOrDefault
                    If (Not holiday Is Nothing) Then
                        objWorkSignData.SHIFT_ID = Nothing
                    End If

                    objWorkSignData.PERIOD_ID = objWork.PERIOD_ID
                    Context.AT_WORKSIGN.AddObject(objWorkSignData)
                    Context.SaveChanges(log)
                    p_fromdate = p_fromdate.AddDays(1)
                    gID = objWorkSignData.ID
                End While
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Validate tồn tại xếp ca làm việc của  nhân viên
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateWorkSign(ByVal _validate As AT_WORKSIGNDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_WORKSIGN
                             Where p.SHIFT_ID = _validate.SHIFT_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Sửa thông tin xếp ca làm việc
    ''' </summary>
    ''' <param name="objWorkSign"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyWorkSign(ByVal objWorkSign As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkSignData As New AT_WORKSIGN With {.ID = objWorkSign.ID}
        Try
            Context.AT_WORKSIGN.Attach(objWorkSignData)
            objWorkSignData.EMPLOYEE_ID = objWorkSign.EMPLOYEE_ID
            objWorkSignData.WORKINGDAY = objWorkSign.WORKINGDAY
            objWorkSignData.PERIOD_ID = objWorkSign.PERIOD_ID
            objWorkSignData.SHIFT_ID = objWorkSign.SHIFT_ID
            Context.SaveChanges(log)
            gID = objWorkSignData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Xóa thông tin xếp ca làm việc
    ''' </summary>
    ''' <param name="lstWorkSign"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteWorkSign(ByVal lstWorkSign() As AT_WORKSIGNDTO) As Boolean
        Dim lstWorkSignData As List(Of AT_WORKSIGN)
        Dim lstIDWorkSign As List(Of Decimal) = (From p In lstWorkSign.ToList Select p.ID).ToList
        Try

            lstWorkSignData = (From p In Context.AT_WORKSIGN Where lstIDWorkSign.Contains(p.ID)).ToList
            For index = 0 To lstWorkSignData.Count - 1
                Context.AT_WORKSIGN.DeleteObject(lstWorkSignData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Xóa ca làm việc của một nhân viên
    ''' </summary>
    ''' <param name="employee_id"></param>
    ''' <param name="p_From"></param>
    ''' <param name="p_to"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Del_WorkSign_ByEmp(ByVal employee_id As String, ByVal p_From As Date, ByVal p_to As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_WORKSIGN",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM = p_From,
                                                         .P_TO = p_to}, False)
                Return True
            End Using
            Return False

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function


    Public Function Modify_WorkSign_ByEmp(ByVal employee_id As Decimal,
                                          ByVal p_From As Date,
                                          ByVal p_to As Date,
                                          ByVal p_period As Decimal,
                                          ByVal obj As List(Of AT_WORKSIGNEDITDTO),
                                          ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_WORKSIGN_BY_EMP",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM = p_From,
                                                         .P_TO = p_to}, False)

            End Using

            For Each item In obj
                Dim objWorkSignData As New AT_WORKSIGN
                objWorkSignData.ID = Utilities.GetNextSequence(Context, Context.AT_WORKSIGN.EntitySet.Name)
                objWorkSignData.EMPLOYEE_ID = employee_id
                objWorkSignData.WORKINGDAY = item.WORKINGDAY
                objWorkSignData.SHIFT_ID = item.SHIFT_ID
                objWorkSignData.PERIOD_ID = p_period

                Context.AT_WORKSIGN.AddObject(objWorkSignData)
            Next

            Context.SaveChanges(log)

            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get join_date and ter_effect_date of employee
    ''' </summary>
    ''' <param name="employee_id"></param>
    ''' <param name="join_date"></param>
    ''' <param name="ter_effect_date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Date(ByVal employee_id As Decimal,
                             ByRef join_date As Date,
                             ByRef ter_effect_date As Date) As Boolean
        Try
            Dim query = (From p In Context.HU_EMPLOYEE
                         Where p.ID = employee_id
                         Select New With {
                             .JOIN_DATE = p.JOIN_DATE,
                             .TER_EFFECT_DATE = p.TER_EFFECT_DATE}).ToList()
            If query(0).JOIN_DATE IsNot Nothing Then
                join_date = query(0).JOIN_DATE
            End If
            If query(0).TER_EFFECT_DATE IsNot Nothing Then
                ter_effect_date = query(0).TER_EFFECT_DATE
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lấy ca mặc định trong xếp ca
    ''' </summary>
    ''' <param name="param"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETSIGNDEFAULT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_FROMDATE = param.FROMDATE,
                                                         .P_ENDATE = param.ENDDATE,
                                                         .P_EMP_OBJ = param.EMP_OBJ,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function GET_PORTAL_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal p_type As String, ByVal P_IS_EXPORT As Decimal, ByVal log As UserLog) As DataSet
        Try
            Dim _Username As String = log.Username.ToUpper
            'If param.IS_LOAD_DIRECTMNG = True Then
            '    ' Xử lý load danh sách nhân viên có QLTT là User login và bao gồm nhân viên là User login
            '    _Username = "ADMIN"
            'End If
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_PORTAL_WORKSIGN",
                                              New With {.P_USERNAME = _Username,
                                                        .P_ORG_ID = param.ORG_ID,
                                                        .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                        .P_EMPLOYEE_ID = param.EMPLOYEE_ID,
                                                        .P_IS_LOAD_DIRECTMNG = CDec(param.IS_LOAD_DIRECTMNG),
                                                        .P_TYPE = p_type,
                                                        .P_PAGE_INDEX = param.PAGE_INDEX,
                                                        .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                        .P_PAGE_SIZE = param.PAGE_SIZE,
                                                        .P_PERIOD_ID = param.PERIOD_ID,
                                                        .P_EMP_OBJ = param.EMP_OBJ,
                                                        .P_START_DATE = param.START_DATE,
                                                        .P_END_DATE = param.END_DATE,
                                                        .P_STATUS = param.STATUS,
                                                        .P_IS_EXPORT = P_IS_EXPORT,
                                                        .P_CUR = cls.OUT_CURSOR,
                                                        .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function CheckNotSendPortalWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean
        Try
            Dim query = (From p In Context.AT_WORKSIGN_PORTAL Where p.EMPLOYEE_ID = _empID AndAlso p.PERIOD_ID = _period_ID And p.STATUS <> 3 AndAlso p.STATUS <> PortalStatus.UnApprovedByLM)
            Return query.Any()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckWaittingApprovePTWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean
        Try
            Dim query = (From p In Context.AT_WORKSIGN_PORTAL Where p.EMPLOYEE_ID = _empID AndAlso p.PERIOD_ID = _period_ID And p.STATUS = PortalStatus.WaitingForApproval)
            Return query.Any()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeletePortalWS(ByVal _lstIDRegGroup As List(Of Decimal)) As Boolean
        Try
            Dim lstPortalWS = (From p In Context.AT_WORKSIGN_PORTAL Where _lstIDRegGroup.Contains(p.ID_REGGROUP)).ToList()

            For Each item In lstPortalWS
                Context.AT_WORKSIGN_PORTAL.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportPortalWS(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_id).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_AT_ATTENDANCE_PORTAL.INSERT_PORTAL_WORKSIGN_DATE"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = period_id,
                                                             .P_STARTDATE = start_date,
                                                             .P_ENDDATE = end_date,
                                                             .P_USERNAME = log.Username.ToUpper,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), Nothing),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), Nothing),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), Nothing),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), Nothing),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), Nothing),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), Nothing),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), Nothing),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), Nothing),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), Nothing),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), Nothing),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), Nothing),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), Nothing),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), Nothing),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), Nothing),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), Nothing),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), Nothing),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), Nothing),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), Nothing),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), Nothing),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), Nothing),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), Nothing),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), Nothing),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), Nothing),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), Nothing),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), Nothing),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), Nothing),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), Nothing),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), Nothing),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), Nothing),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), Nothing),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), Nothing)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                'cmd.CommandText = "PKG_AT_ATTENDANCE_PORTAL.IMPORT_PORTAL_WORKSIGN"
                                'cmd.Parameters.Clear()

                                'Dim objParam1 = New With {.P_STARTDATE = start_date,
                                '                          .P_ENDDATE = end_date,
                                '                          .P_USERNAME = log.Username.ToUpper}
                                ''Dim objParam1 = New With {.P_STARTDATE = Period.START_DATE.Value,
                                ''                         .P_ENDDATE = Period.END_DATE.Value,
                                ''                         .P_USERNAME = log.Username.ToUpper}

                                'If objParam1 IsNot Nothing Then
                                '    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                '        Dim bOut As Boolean = False
                                '        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                '        If para IsNot Nothing Then
                                '            cmd.Parameters.Add(para)
                                '        End If
                                '    Next
                                'End If

                                'cmd.ExecuteNonQuery()

                                'cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetListWSWaitingApprove(ByVal param As AT_WORKSIGNDTO, ByVal P_IS_EXPORT As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_WS_WAITING_APPROVE",
                                              New With {.P_EMPLOYEE_MNG = param.EMPLOYEE_ID,
                                                        .P_PAGE_INDEX = param.PAGE_INDEX,
                                                        .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                        .P_PAGE_SIZE = param.PAGE_SIZE,
                                                        .P_PERIOD_ID = param.PERIOD_ID,
                                                        .P_EMP_OBJ = param.EMP_OBJ,
                                                        .P_START_DATE = param.START_DATE,
                                                        .P_END_DATE = param.END_DATE,
                                                        .P_STATUS_ID = param.STATUS,
                                                        .P_IS_EXPORT = P_IS_EXPORT,
                                                        .P_CUR = cls.OUT_CURSOR,
                                                        .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function
#End Region

#Region "ProjectAssign"
    Public Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PROJECT_BUSINESS.GET_ProjectAssign",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_PAGE_INDEX = param.PAGE_INDEX,
                                                         .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                         .P_PAGE_SIZE = param.PAGE_SIZE,
                                                         .P_PERIOD_ID = param.PERIOD_ID,
                                                         .P_PROJECT_ID = param.PROJECT_ID,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProjectAssign As New AT_PROJECT_ASSIGNDTO
        Dim p_fromDateBefor As Date = p_fromdate
        Try
            For index = 0 To objProjectAssigns.Count - 1
                objProjectAssign = objProjectAssigns(index)
                p_fromdate = p_fromDateBefor
                While p_fromdate <= p_endDate
                    Dim objProjectAssignData As New AT_PROJECT_ASSIGN
                    ' kiem tra da ton tai chua
                    Dim exist = (From c In Context.AT_PROJECT_ASSIGN
                                 Where c.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID And
                                 c.WORKINGDAY = p_fromdate).Any
                    If exist Then

                        Dim query = (From c In Context.AT_PROJECT_ASSIGN
                                     Where c.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID And
                                     c.WORKINGDAY = p_fromdate).FirstOrDefault

                        query.PROJECT_ID = objWork.PROJECT_ID
                        query.PROJECT_WORK_ID = objWork.PROJECT_WORK_ID
                        query.HOURS = objWork.HOURS

                        Context.SaveChanges(log)
                        p_fromdate = p_fromdate.AddDays(1)
                        Continue While
                    End If
                    objProjectAssignData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT_ASSIGN.EntitySet.Name)
                    objProjectAssignData.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID
                    objProjectAssignData.WORKINGDAY = p_fromdate
                    objProjectAssignData.PERIOD_ID = objWork.PERIOD_ID
                    objProjectAssignData.PROJECT_ID = objWork.PROJECT_ID
                    objProjectAssignData.PROJECT_WORK_ID = objWork.PROJECT_WORK_ID
                    objProjectAssignData.HOURS = objWork.HOURS
                    Context.AT_PROJECT_ASSIGN.AddObject(objProjectAssignData)
                    Context.SaveChanges(log)
                    p_fromdate = p_fromdate.AddDays(1)
                    gID = objProjectAssignData.ID
                End While
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProjectAssignData As New AT_PROJECT_ASSIGN With {.ID = objProjectAssign.ID}
        Try
            Context.AT_PROJECT_ASSIGN.Attach(objProjectAssignData)
            objProjectAssignData.EMPLOYEE_ID = objProjectAssign.EMPLOYEE_ID
            objProjectAssignData.WORKINGDAY = objProjectAssign.WORKINGDAY
            objProjectAssignData.PERIOD_ID = objProjectAssign.PERIOD_ID
            objProjectAssignData.HOURS = objProjectAssign.HOURS
            Context.SaveChanges(log)
            gID = objProjectAssignData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean
        Dim lstProjectAssignData As List(Of AT_PROJECT_ASSIGN)
        Dim lstIDProjectAssign As List(Of Decimal) = (From p In lstProjectAssign.ToList Select p.ID).ToList
        Try

            lstProjectAssignData = (From p In Context.AT_PROJECT_ASSIGN Where lstIDProjectAssign.Contains(p.ID)).ToList
            For index = 0 To lstProjectAssignData.Count - 1
                Context.AT_PROJECT_ASSIGN.DeleteObject(lstProjectAssignData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

#End Region

#Region "TAI DU LIEU MAY CHAM CONG"
    ''' <summary>
    ''' Lấy danh sách dữ liệu máy chấm công
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                    ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Dim orgRoot = (From o In Context.HU_ORGANIZATION Where o.PARENT_ID Is Nothing Select o.ID).FirstOrDefault
            'If Not IsNumeric(orgRoot) Then
            '    orgRoot = 1
            'End If
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = _filter.USERNAME,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = False})
            End Using
            Dim path = AppDomain.CurrentDomain.BaseDirectory
            Dim str1 = "Máy vào"
            Dim str2 = "Máy ra"
            Dim MCC_CHECKIN = "MCC_CHECKIN"
            Dim WIFI_CHECKIN = "WIFI_CHECKIN"
            Dim GPS_CHECKIN = "GPS_CHECKIN"
            Dim strNull = ""
            Dim strNull2 = (From p In Context.OT_OTHER_LIST Where p.CODE = "IMPORT" And p.TYPE_CODE = "FILTER_NULL_VALUE" Select p.NAME_VN).FirstOrDefault
            Dim strNull3 = (From p In Context.OT_OTHER_LIST Where p.CODE = "EXPLAINATION_DATA" And p.TYPE_CODE = "FILTER_NULL_VALUE" Select p.NAME_VN).FirstOrDefault
            Dim IDMCC = (From p In Context.OT_OTHER_LIST Where p.CODE = "MCC_CHECKIN" And p.TYPE_CODE = "ATTENDANCE_CHECKIN_TYPE" Select p.ID).FirstOrDefault
            Dim EXPLAINATION_DATA = "EXPLAINATION_DATA"
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
            , Nothing, "GetSwipeData")
            Dim query = From p In Context.AT_SWIPE_DATA
                        From gps In Context.AT_SETUP_GPS.Where(Function(f) f.ID = p.PLACE_ID).DefaultIfEmpty
                        From wifi In Context.AT_SETUP_WIFI.Where(Function(f) f.ID = p.PLACE_ID).DefaultIfEmpty
                        From fileImage In Context.HU_USERFILES.Where(Function(f) f.NAME = p.IMAGE_GPS).DefaultIfEmpty
                        From machine_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MACHINE_TYPE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                        From bld In Context.HU_TITLE_BLD.Where(Function(f) f.ID = e.MATHE).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = _filter.USERNAME.ToUpper)
            Dim lst = query.Select(Function(p) New AT_SWIPE_DATADTO With {
                                       .ID = p.p.ID,
                                       .ITIME_ID = p.e.ITIME_ID,
                                       .ITIME_ID_S = p.p.ITIME_ID,
                                       .TERMINAL_ID = p.p.TERMINAL_ID,
                                       .TERMINAL_CODE = If(p.p.TERMINAL_ID = 1, str1, If(p.p.TERMINAL_ID = 2, str2, strNull)),
                                       .MACHINE_TYPE = p.p.MACHINE_TYPE,
                                       .MACHINE_TYPE_NAME = p.machine_type.NAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .WORKINGDAY = EntityFunctions.TruncateTime(p.p.WORKINGDAY),
                                       .VALTIME = p.p.VALTIME,
                                       .ORD_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ACCOUNT_SHORTNAME = p.p.ACCOUNT_SHORTNAME,
                                       .MATHE_NAME = p.bld.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .NOTE = p.p.NOTE,
                                       .LONGITUDE = p.p.LONGITUDE,
                                       .LATITUDE = p.p.LATITUDE,
                                       .IMAGE_GPS = If(p.fileImage.FILE_NAME IsNot Nothing, path & p.fileImage.LINK & p.fileImage.FILE_NAME, strNull),
                                       .UPLOAD_IMAGE_GPS = p.fileImage.NAME,
                                       .PLACE_ID = p.p.PLACE_ID,
                                       .PLACE_NAME = If(p.machine_type.CODE = MCC_CHECKIN, p.p.ACCOUNT_SHORTNAME, If(p.machine_type.CODE = WIFI_CHECKIN, p.wifi.NAME_VN, If(p.machine_type.CODE = GPS_CHECKIN, p.gps.NAME, strNull))),
                                       .PLACE_NAME2 = If(p.machine_type.CODE = MCC_CHECKIN, p.p.ACCOUNT_SHORTNAME, If(p.machine_type.CODE = WIFI_CHECKIN, p.wifi.NAME_VN, If(p.machine_type.CODE = EXPLAINATION_DATA, strNull3, If(p.machine_type.CODE = GPS_CHECKIN, p.gps.NAME, strNull2))))})

            If _filter.ITIME_ID <> "" Then
                lst = lst.Where(Function(f) f.ITIME_ID = _filter.ITIME_ID)
            End If
            If _filter.PLACE_ID IsNot Nothing Then
                lst = lst.Where(Function(f) f.PLACE_ID = _filter.PLACE_ID)
            End If
            If (_filter.TERMINAL_CODE <> "") Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToUpper.Contains(_filter.TERMINAL_CODE.ToUpper))
            End If
            If (_filter.EMPLOYEE_CODE <> "") Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If (_filter.PLACE_NAME_FILTER IsNot Nothing AndAlso _filter.PLACE_NAME_FILTER.Count > 0) Then
                'Or _filter.MACHINE_TYPES.Contains(MCC_CHECKIN)
                lst = lst.Where(Function(f) _filter.PLACE_NAME_FILTER.Contains(f.PLACE_NAME2) Or (_filter.MACHINE_TYPES.Contains(IDMCC) And f.ACCOUNT_SHORTNAME IsNot Nothing))
            End If
            If (_filter.MACHINE_TYPES IsNot Nothing AndAlso _filter.MACHINE_TYPES.Count > 0) Then
                lst = lst.Where(Function(f) _filter.MACHINE_TYPES.Contains(f.MACHINE_TYPE))
            End If
            If (_filter.EMPLOYEE_NAME <> "") Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If (_filter.ORG_NAME <> "") Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If (_filter.TITLE_NAME <> "") Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If (_filter.MATHE_NAME <> "") Then
                lst = lst.Where(Function(f) f.MATHE_NAME.ToUpper.Contains(_filter.MATHE_NAME.ToUpper))
            End If
            If (_filter.ITIME_ID_S <> "") Then
                lst = lst.Where(Function(f) f.ITIME_ID_S.ToUpper.Contains(_filter.ITIME_ID_S.ToUpper))
            End If
            If (_filter.MACHINE_TYPE_NAME <> "") Then
                lst = lst.Where(Function(f) f.MACHINE_TYPE_NAME.ToUpper.Contains(_filter.MACHINE_TYPE_NAME.ToUpper))
            End If
            If Not IsNothing(_filter.TERMINAL_ID) Then
                lst = lst.Where(Function(f) f.TERMINAL_ID = _filter.TERMINAL_ID)
            End If
            If Not IsNothing(_filter.MACHINE_TYPE) Then
                lst = lst.Where(Function(f) f.MACHINE_TYPE = _filter.MACHINE_TYPE)
            End If

            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
            , Nothing, "GetSwipeData")
            Return lst.ToList
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString() _
            , Nothing, ex.ToString)
        End Try
    End Function

    Public Function GetTerminalData(ByVal lstCode As List(Of String), ByVal username As String, ByVal orgID As Decimal) As List(Of OT_OTHERLIST_DTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim query As IQueryable(Of AT_TERMINALS)
        Dim query2 As IQueryable(Of AT_SETUP_WIFI)
        Dim query3 As IQueryable(Of AT_SETUP_GPS)
        Dim lstAT_TERMINALS As List(Of OT_OTHERLIST_DTO)
        Dim lstAT_SETUP_WIFI As List(Of OT_OTHERLIST_DTO)
        Dim lstAT_SETUP_GPS As List(Of OT_OTHERLIST_DTO)
        Dim lst = New List(Of OT_OTHERLIST_DTO)

        Dim lstExplaination_Data = New List(Of OT_OTHERLIST_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = username,
                                           .P_ORGID = orgID,
                                           .P_ISDISSOLVE = False})
            End Using
            If lstCode.Contains("MCC_CHECKIN") Then
                query = From p In Context.AT_TERMINALS
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                           f.USERNAME = username.ToUpper)
                        Where p.ACTFLG = "A" Select p
            End If
            If query IsNot Nothing Then
                lstAT_TERMINALS = query.Select(Function(p) New OT_OTHERLIST_DTO With {
                                           .ID = p.ID,
                                           .NAME_VN = p.TERMINAL_NAME}).ToList
                For Each item In lstAT_TERMINALS
                    item.CODE = "AT_TERMINALS_" + item.ID.ToString
                Next
            Else
                lstAT_TERMINALS = New List(Of OT_OTHERLIST_DTO)
            End If
            If lstCode.Contains("WIFI_CHECKIN") Then
                query2 = From p In Context.AT_SETUP_WIFI
                         Where p.ACTFLG = "A" Select p
            End If
            If query2 IsNot Nothing Then
                lstAT_SETUP_WIFI = query2.Select(Function(p) New OT_OTHERLIST_DTO With {
                                           .ID = p.ID,
                                           .NAME_VN = p.NAME_VN}).ToList
                For Each item In lstAT_SETUP_WIFI
                    item.CODE = "AT_SETUP_WIFI_" + item.ID.ToString
                Next
            Else
                lstAT_SETUP_WIFI = New List(Of OT_OTHERLIST_DTO)
            End If
            If lstCode.Contains("GPS_CHECKIN") Then
                query3 = From p In Context.AT_SETUP_GPS
                         Where p.ACTFLG = "A" Select p
            End If
            If query3 IsNot Nothing Then
                lstAT_SETUP_GPS = query3.Select(Function(p) New OT_OTHERLIST_DTO With {
                                           .ID = p.ID,
                                           .NAME_VN = p.NAME}).ToList
                For Each item In lstAT_SETUP_GPS
                    item.CODE = "AT_SETUP_GPS_" + item.ID.ToString
                Next
            Else
                lstAT_SETUP_GPS = New List(Of OT_OTHERLIST_DTO)
            End If
            lst = lstAT_SETUP_WIFI.Union(lstAT_SETUP_GPS).Union(lstAT_TERMINALS).ToList
            If lstCode.Contains("FILE_IMPORT") Then
                lst.Add(New OT_OTHERLIST_DTO With {.CODE = "", .NAME_VN = (From p In Context.OT_OTHER_LIST Where p.CODE = "IMPORT" And p.TYPE_CODE = "FILTER_NULL_VALUE" Select p.NAME_VN).FirstOrDefault})
            End If
            If lstCode.Contains("EXPLAINATION_DATA") Then
                lst.Add(New OT_OTHERLIST_DTO With {.CODE = "", .NAME_VN = (From p In Context.OT_OTHER_LIST Where p.CODE = "EXPLAINATION_DATA" And p.TYPE_CODE = "FILTER_NULL_VALUE" Select p.NAME_VN).FirstOrDefault})
            End If

            Return lst
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Thêm mới dữ liệu chấm công
    ''' </summary>
    ''' <param name="objSwipeData"></param>
    ''' <param name="machine"></param>
    ''' <param name="P_FROMDATE"></param>
    ''' <param name="P_ENDDATE"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO),
                                    ByVal machine As AT_TERMINALSDTO,
                                    ByVal P_FROMDATE As Date?,
                                    ByVal P_ENDDATE As Date?,
                                    ByVal log As UserLog,
                                    ByRef gID As Decimal) As Boolean
        Dim sv_SDK1 As New zkemkeeper.CZKEM
        sv_GetLogData(sv_SDK1, machine, P_FROMDATE, P_ENDDATE)
        Dim objWorkSignData As New AT_SWIPE_DATA
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = ls_AT_SWIPE_DATADTO(index).ITIME_ID,
                                                             .P_VALTIME = ls_AT_SWIPE_DATADTO(index).VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                    gID = objWorkSignData.ID
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using


            ' UPDATE DATA TO AT_DATAINOUTDTO
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                               New With {.P_ITIMEID = 0,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_FROMDATE = P_FROMDATE,
                                                         .P_ENDDATE = P_ENDDATE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' nhập danh sách chấm công theo file mẫu
    ''' </summary>
    ''' <param name="ls_AT_SWIPE_DATADTO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImportSwipeDataAuto(ByVal ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO)) As Boolean
        Dim endDate As Date?
        Dim fromDate As Date?
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD_AUTO"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For index = 0 To ls_AT_SWIPE_DATADTO.Count - 1
                                cmd.Parameters.Clear()
                                Dim objDataInout = ls_AT_SWIPE_DATADTO(index)
                                If endDate Is Nothing Then
                                    endDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date > endDate Then
                                        endDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.VALTIME.Value.Date
                                Else
                                    If objDataInout.VALTIME.Value.Date < fromDate Then
                                        fromDate = objDataInout.VALTIME.Value.Date
                                    End If
                                End If

                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINAL_ID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = "ADMIN"}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                            Throw ex
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
            If endDate IsNot Nothing And fromDate IsNot Nothing Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                                   New With {.P_ITIMEID = 0,
                                                             .P_USERNAME = "ADMIN",
                                                             .P_FROMDATE = fromDate,
                                                             .P_ENDDATE = endDate})
                End Using

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Private Sub sv_GetLogData(ByVal sv_SDK1 As zkemkeeper.CZKEM,
                              ByVal machine As AT_TERMINALSDTO,
                              ByVal p_fromdate As Date?,
                              ByVal p_EndDate As Date?)
        Dim sv_ErrorNo As Integer = 0
        Dim mv_Port As String = machine.PORT
        Dim message As String = ""
        Dim rowSwipe As AT_SWIPE_DATADTO
        Dim sv_vErrorCode As Integer = 0
        Dim sv_vRet As Boolean = False
        Dim sv_i As Integer = 1
        Dim mv_IP As String
        mv_IP = machine.TERMINAL_IP
        Dim mv_MachineNumber = 1
        Try
            If mv_IP <> "" Then
                If Not String.IsNullOrEmpty(machine.PASS) Then
                    sv_SDK1.SetCommPassword(machine.PASS)
                End If
                Try

                    If sv_SDK1.Connect_Net(mv_IP, mv_Port) Then
                        sv_SDK1.EnableDevice(1, False) 'disable the device
                        Dim idwEnrollNumber As Integer
                        Dim idwVerifyMode As Integer
                        Dim idwInOutMode As Integer
                        Dim idwYear As Integer
                        Dim idwMonth As Integer
                        Dim idwDay As Integer
                        Dim idwHour As Integer
                        Dim idwMinute As Integer
                        Dim idwSecond As Integer
                        Dim idwWorkcode As Integer
                        If mv_IP <> "0" Then
                            ls_AT_SWIPE_DATADTO.Clear()
                        End If
                        If sv_vRet Then
                            sv_SDK1.GetLastError(sv_ErrorNo)
                            While sv_ErrorNo <> 0
                                sv_SDK1.EnableDevice(mv_MachineNumber, False)

                                If sv_SDK1.ReadGeneralLogData(mv_MachineNumber) Then
                                    While sv_SDK1.SSR_GetGeneralLogData(mv_MachineNumber, idwEnrollNumber,
                                                                        idwVerifyMode, idwInOutMode,
                                                                        idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond,
                                                                        idwWorkcode)
                                        If idwYear > Date.Now.Year Then
                                            Continue While
                                        End If
                                        If (New Date(idwYear, idwMonth, idwDay) >= CDate(p_fromdate) And New Date(idwYear, idwMonth, idwDay) <= CDate(p_EndDate)) Then
                                            rowSwipe = New AT_SWIPE_DATADTO
                                            rowSwipe.ITIME_ID = idwEnrollNumber
                                            rowSwipe.VALTIME = New Date(idwYear, idwMonth, idwDay, idwHour, idwMinute, 0)
                                            ls_AT_SWIPE_DATADTO.Add(rowSwipe)
                                        End If
                                    End While
                                End If
                                sv_SDK1.GetLastError(sv_ErrorNo)
                            End While
                        Else
                            sv_SDK1.GetLastError(sv_vErrorCode)
                        End If
                    Else
                    End If
                Catch ex As Exception

                Finally
                    sv_SDK1.EnableDevice(1, True)
                    sv_SDK1.Disconnect()
                End Try
            Else
                If String.IsNullOrEmpty(mv_IP) Then
                Else
                    message &= "IP " + mv_IP + " không đúng!" & vbCrLf
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Sub

    Public Function InsertSwipeDataImport(ByVal lstData As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim fromDate As Date?
            Dim endDate As New Date?
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_TIME_CARD"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each objDataInout In lstData
                                If endDate Is Nothing Then
                                    endDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY > endDate Then
                                        endDate = objDataInout.WORKINGDAY
                                    End If
                                End If

                                If fromDate Is Nothing Then
                                    fromDate = objDataInout.WORKINGDAY
                                Else
                                    If objDataInout.WORKINGDAY < fromDate Then
                                        fromDate = objDataInout.WORKINGDAY
                                    End If
                                End If
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_TIMEID = objDataInout.ITIME_ID,
                                                             .P_TERMINALID = objDataInout.TERMINAL_ID,
                                                             .P_VALTIME = objDataInout.VALTIME,
                                                             .P_USERNAME = log.Username.ToUpper}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            ' UPDATE DATA TO AT_DATAINOUTDTO
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.UPDATE_DATAINOUT",
                                               New With {.P_ITIMEID = 0,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_FROMDATE = fromDate,
                                                         .P_ENDDATE = endDate})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean
        Try
            Dim dsData As New DataSet
            dsData.Tables.Add(dtData)
            Dim strXml = dsData.GetXml()

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_SWIPE_DATA",
                                               New With {.P_XML = strXml,
                                                         .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_INOUT(ByVal docXML As String, ByVal fromDate As DateTime, ByVal toDate As DateTime, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.IMPORT_INOUT",
                                               New With {.P_DATA = docXML,
                                                         .P_FROMDATE = fromDate,
                                                         .P_TODATE = toDate,
                                                         .P_USER = If(log IsNot Nothing, log.Username.ToUpper, "AUTO"),
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "LOG"
    Public Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                        ByRef Total As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)

        Try
            Dim query = From p In Context.AT_ACTION_LOG
                        From e In Context.SE_USER.Where(Function(f) f.USERNAME.ToUpper = p.USERNAME.ToUpper)
                        From r In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ACTION_LOGDTO With {
                                       .ID = p.p.ID,
                                       .username = p.p.USERNAME,
                                       .fullname = p.e.FULLNAME,
                                       .email = p.e.EMAIL,
                                       .mobile = p.e.TELEPHONE,
                                       .action_name = p.p.ACTION_NAME,
                                       .action_date = p.p.ACTION_DATE,
                                       .object_name = p.p.OBJECT_NAME,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.r.PERIOD_NAME,
                                       .ip = p.p.IP,
                                       .computer_name = p.p.COMPUTER_NAME,
                                       .action_type = p.p.ACTION_TYPE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .NEW_VALUE = p.p.NEW_VALUE,
                                       .OLD_VALUE = p.p.OLD_VALUE})


            If Not String.IsNullOrEmpty(_filter.username) Then
                lst = lst.Where(Function(f) f.username.ToLower().Contains(_filter.username.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.fullname) Then
                lst = lst.Where(Function(f) f.fullname.ToLower().Contains(_filter.fullname.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.email) Then
                lst = lst.Where(Function(f) f.email.ToLower().Contains(_filter.email.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.mobile) Then
                lst = lst.Where(Function(f) f.mobile.ToLower().Contains(_filter.mobile.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_name) Then
                lst = lst.Where(Function(f) f.action_name.ToLower().Contains(_filter.action_name.ToLower()))
            End If
            If _filter.action_date.HasValue Then
                lst = lst.Where(Function(f) f.action_date >= _filter.action_date)
            End If
            If Not String.IsNullOrEmpty(_filter.object_name) Then
                lst = lst.Where(Function(f) f.object_name.ToLower().Contains(_filter.object_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ip) Then
                lst = lst.Where(Function(f) f.ip.ToLower().Contains(_filter.ip.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.computer_name) Then
                lst = lst.Where(Function(f) f.computer_name.ToLower().Contains(_filter.computer_name.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.action_type) Then
                lst = lst.Where(Function(f) f.action_type.ToLower().Contains(_filter.action_type.ToLower()))
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.NEW_VALUE) Then
                lst = lst.Where(Function(f) f.NEW_VALUE.ToLower().Contains(_filter.NEW_VALUE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.OLD_VALUE) Then
                lst = lst.Where(Function(f) f.OLD_VALUE.ToLower().Contains(_filter.OLD_VALUE.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Dim lstData As List(Of AT_ACTION_LOG)
        Try
            lstData = (From p In Context.AT_ACTION_LOG Where lstDeleteIds.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.AT_ACTION_LOG.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function LOG_AT(ByVal _param As ParamDTO,
                           ByVal log As UserLog,
                           ByVal lstEmployee As List(Of Decimal?),
                           ByVal Object_Name As String,
                           ByVal action As AT_ACTION_LOGDTO,
                           ByVal org_id As Decimal?) As Boolean
        Dim ActionId As Decimal?
        Dim action_log As New AT_ACTION_LOG
        action_log.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_LOG.EntitySet.Name)
        ActionId = action_log.ID
        action_log.USERNAME = log.Username.ToUpper
        action_log.IP = log.Ip
        action_log.ACTION_NAME = log.ActionName
        action_log.ACTION_DATE = DateTime.Now
        action_log.OBJECT_NAME = Object_Name
        action_log.COMPUTER_NAME = log.ComputerName
        action_log.ORG_ID = org_id
        action_log.EMPLOYEE_ID = action.EMPLOYEE_ID
        action_log.OLD_VALUE = action.OLD_VALUE
        action_log.PERIOD_ID = action.PERIOD_ID
        action_log.NEW_VALUE = action.NEW_VALUE
        Context.AT_ACTION_LOG.AddObject(action_log)
        If lstEmployee.Count > 0 Then
            Dim action_logOrg As AT_ACTION_ORG_LOG
            For Each emp As Decimal? In lstEmployee
                action_logOrg = New AT_ACTION_ORG_LOG
                action_logOrg.ID = Utilities.GetNextSequence(Context, Context.AT_ACTION_ORG_LOG.EntitySet.Name)
                action_logOrg.EMPLOYEE_ID = emp
                action_logOrg.ACTION_LOG_ID = ActionId
                Context.AT_ACTION_ORG_LOG.AddObject(action_logOrg)
            Next
        Else
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_CHOSEN_LOGORG",
                             New With {.P_USERNAME = log.Username.ToUpper,
                                       .P_ORGID = _param.ORG_ID,
                                       .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                       .P_ACTION_ID = ActionId})
            End Using
        End If
        Context.SaveChanges()
        Return True
    End Function
#End Region

#Region "IPORTAL - View bảng công"

    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            emp = (From p In Context.HU_EMPLOYEE Where p.ID = EmployeeId).FirstOrDefault

            Dim query = (From p In Context.AT_ORG_PERIOD
                         Where p.PERIOD_ID = PeriodId And p.ORG_ID = emp.ORG_ID).FirstOrDefault


            If query IsNot Nothing Then

                If query.STATUSCOLEX = 0 Then
                    Return query.STATUSCOLEX = 0
                Else
                    Return -1
                End If
            Else
                Return (query Is Nothing)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Lấy thông tin bảng công tổng hợp
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="_param"></param>
    ''' <param name="Total"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Try

            Dim query = From p In Context.AT_TIME_TIMESHEET_MONTHLY
                        From ot In Context.AT_TIME_TIMESHEET_OT.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And f.PERIOD_ID = p.PERIOD_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From po In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        Where (p.PERIOD_ID = _filter.PERIOD_ID)

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.WORKING_A.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_A = _filter.WORKING_A)
            End If
            If _filter.WORKING_B.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_B = _filter.WORKING_B)
            End If
            If _filter.WORKING_C.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_C = _filter.WORKING_C)
            End If
            If _filter.WORKING_D.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_D = _filter.WORKING_D)
            End If
            If _filter.WORKING_E.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_E = _filter.WORKING_E)
            End If
            If _filter.WORKING_F.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_F = _filter.WORKING_F)
            End If
            If _filter.WORKING_H.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_H = _filter.WORKING_H)
            End If
            If _filter.WORKING_J.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_J = _filter.WORKING_J)
            End If
            If _filter.WORKING_K.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_K = _filter.WORKING_K)
            End If
            If _filter.WORKING_L.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_L = _filter.WORKING_L)
            End If
            If _filter.WORKING_N.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_N = _filter.WORKING_N)
            End If
            If _filter.WORKING_O.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_O = _filter.WORKING_O)
            End If
            If _filter.WORKING_P.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_P = _filter.WORKING_P)
            End If
            If _filter.WORKING_Q.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_Q = _filter.WORKING_Q)
            End If
            If _filter.WORKING_R.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_R = _filter.WORKING_R)
            End If
            If _filter.WORKING_S.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_S = _filter.WORKING_S)
            End If
            If _filter.WORKING_T.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_T = _filter.WORKING_T)
            End If
            If _filter.WORKING_TS.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_TS = _filter.WORKING_TS)
            End If
            If _filter.WORKING_V.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_V = _filter.WORKING_V)
            End If
            If _filter.WORKING_X.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_X = _filter.WORKING_X)
            End If
            If _filter.TOTAL_WORKING.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_WORKING = _filter.TOTAL_WORKING)
            End If
            If _filter.LATE.HasValue Then
                query = query.Where(Function(f) f.p.LATE = _filter.LATE)
            End If
            If _filter.COMEBACKOUT.HasValue Then
                query = query.Where(Function(f) f.p.COMEBACKOUT = _filter.COMEBACKOUT)
            End If
            If _filter.TOTAL_W_SALARY.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_W_SALARY = _filter.TOTAL_W_SALARY)
            End If
            If _filter.TOTAL_W_NOSALARY.HasValue Then
                query = query.Where(Function(f) f.p.TOTAL_W_NOSALARY = _filter.TOTAL_W_NOSALARY)
            End If
            If _filter.WORKING_MEAL.HasValue Then
                query = query.Where(Function(f) f.p.WORKING_MEAL = _filter.WORKING_MEAL)
            End If
            Dim lst = query.Select(Function(p) New AT_TIME_TIMESHEET_MONTHLYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.p.ORG_ID,
                                       .PERIOD_ID = p.po.ID,
                                       .PERIOD_STANDARD = p.po.PERIOD_STANDARD,
                                       .STAFF_RANK_NAME = p.s.NAME,
                                       .DECISION_START = p.p.DECISION_START,
                                       .DECISION_END = p.p.DECISION_END,
                                       .WORKING_X = p.p.WORKING_X,
                                       .WORKING_F = p.p.WORKING_F,
                                       .WORKING_E = p.p.WORKING_E,
                                       .WORKING_A = p.p.WORKING_A,
                                       .WORKING_H = p.p.WORKING_H,
                                       .WORKING_D = p.p.WORKING_D,
                                       .WORKING_C = p.p.WORKING_C,
                                       .WORKING_T = p.p.WORKING_T,
                                       .WORKING_Q = p.p.WORKING_Q,
                                       .WORKING_N = p.p.WORKING_N,
                                       .WORKING_P = p.p.WORKING_P,
                                       .WORKING_L = p.p.WORKING_L,
                                       .WORKING_R = p.p.WORKING_R,
                                       .WORKING_S = p.p.WORKING_S,
                                       .WORKING_B = p.p.WORKING_B,
                                       .WORKING_K = p.p.WORKING_K,
                                       .WORKING_J = p.p.WORKING_J,
                                       .WORKING_TS = p.p.WORKING_TS,
                                       .WORKING_O = p.p.WORKING_O,
                                       .WORKING_V = p.p.WORKING_V,
                                       .WORKING_ADD = p.p.WORKING_ADD,
                                       .WORKING_MEAL = p.p.WORKING_MEAL,
                                       .LATE = p.p.LATE,
                                       .COMEBACKOUT = p.p.COMEBACKOUT,
                                       .TOTAL_W_NOSALARY = p.p.TOTAL_W_NOSALARY,
                                       .TOTAL_W_SALARY = p.p.TOTAL_W_SALARY,
                                       .TOTAL_WORKING = p.p.TOTAL_WORKING + If(p.ot.TOTAL_FACTOR_CONVERT Is Nothing, 0, p.ot.TOTAL_FACTOR_CONVERT),
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_LAST_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .TOTAL_FACTOR = p.ot.TOTAL_FACTOR_CONVERT,
                                       .TOTAL_FACTOR1 = p.ot.TOTAL_FACTOR1,
                                       .TOTAL_FACTOR1_5 = p.ot.TOTAL_FACTOR1_5,
                                       .TOTAL_FACTOR2 = p.ot.TOTAL_FACTOR2,
                                       .TOTAL_FACTOR2_7 = p.ot.TOTAL_FACTOR2_7,
                                       .TOTAL_FACTOR3 = p.ot.TOTAL_FACTOR3,
                                       .TOTAL_FACTOR3_9 = p.ot.TOTAL_FACTOR3_9,
                                       .WORKING_DA = p.p.WORKING_DA})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Portal quan ly nghi phep, nghi bu"
    Public Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                                    Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Dim ListManul As New AT_TIME_MANUAL
            ListManul = (From p In Context.AT_TIME_MANUAL
                         Where (p.MORNING_ID = 251 Or p.AFTERNOON_ID = 251) And p.ID = _filter.TIME_MANUAL_ID).FirstOrDefault
            Using cls As New DataAccess.QueryData

                ' nghi bu 
                If (_filter.LEAVE_TYPE = 255) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_COMPENSATORY",
                                     New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                                .P_ID_PORTAL_REG = _filter.ID_PORTAL_REG,
                                               .P_DATE_TIME = _filter.DATE_REGISTER,
                                               .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("TONG_NB")
                        result.USED_DAY = dtData.Rows(0)("DA_NB")
                        result.REST_DAY = dtData.Rows(0)("NB_CON_LAI")
                        result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                        result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))
                    End If
                    'nghi phep
                ElseIf (_filter.LEAVE_TYPE = 251) Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_TOTAL_ENTITLEMENT",
                                    New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                              .P_ID_PORTAL_REG = _filter.ID_PORTAL_REG,
                                              .P_DATE_TIME = _filter.DATE_REGISTER,
                                              .P_OUT = cls.OUT_CURSOR})
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                        result.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        result.DATE_REGISTER = _filter.DATE_REGISTER
                        result.LEAVE_TYPE = _filter.LEAVE_TYPE
                        result.TOTAL_DAY = dtData.Rows(0)("PHEP_TRONG_NAM")
                        result.USED_DAY = dtData.Rows(0)("PHEP_DA_NGHI")
                        result.REST_DAY = dtData.Rows(0)("PHEP_CON_LAI")
                        result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                        result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))

                        result.PREV_HAVE = dtData.Rows(0)("PREV_HAVE")
                        result.PREV_USED = dtData.Rows(0)("PREV_USED")
                        result.PREVTOTAL_HAVE = dtData.Rows(0)("PREVTOTAL_HAVE")
                        result.SENIORITYHAVE = dtData.Rows(0)("SENIORITYHAVE")
                        result.TOTAL_HAVE1 = dtData.Rows(0)("TOTAL_HAVE1")
                        result.TIME_OUTSIDE_COMPANY = dtData.Rows(0)("TIME_OUTSIDE_COMPANY")
                    End If
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO,
                                    Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Using cls As New DataAccess.QueryData

                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL",
                                 New With {.P_ID = _filter.LEAVE_TYPE,
                                           .P_DATE_TIME = _filter.DATE_REGISTER,
                                           .P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                           .P_OUT = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    result.LIMIT_DAY = If(IsDBNull(dtData.Rows(0)("LIMIT_DAY")), Nothing, dtData.Rows(0)("LIMIT_DAY"))
                    result.LIMIT_YEAR = If(IsDBNull(dtData.Rows(0)("LIMIT_YEAR")), Nothing, dtData.Rows(0)("LIMIT_YEAR"))
                    result.USED_DAY = dtData.Rows(0)("PHEP_DA_NGHI")
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO)
        Try
            Dim result As New List(Of HistoryLeaveDTO)
            Using cls As New DataAccess.QueryData
                'lich su
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.MANAGEMENT_HISTORY_LEAVE",
                                 New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                           .P_FROM_DATE = _filter.FROM_DATE,
                                           .P_TO_DATE = _filter.TO_DATE,
                                           .P_OUT = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New HistoryLeaveDTO With {
                                 .EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                 .EMPLOYEE_CODE = dr("EMPLOYEE_CODE"),
                                 .EMPLOYEE_NAME = dr("EMPLOYEE_NAME"),
                                 .REGDATE = If(dr("REG_DATE") IsNot Nothing, ToDate(dr("REG_DATE")), Nothing),
                                 .FROM_DATE = If(dr("FROM_DATE") IsNot Nothing, ToDate(dr("FROM_DATE")), Nothing),
                                  .TO_DATE = If(dr("TO_DATE") IsNot Nothing, ToDate(dr("TO_DATE")), Nothing),
                                  .FROM_HOUR = If(dr("FROM_HOUR") IsNot Nothing, ToDate(dr("FROM_HOUR")), Nothing),
                                  .TO_HOUR = If(dr("TO_HOUR") IsNot Nothing, ToDate(dr("TO_HOUR")), Nothing),
                                 .SIGN_ID = Decimal.Parse(dr("SIGN_ID")),
                                 .SIGN_CODE = dr("SIGN_CODE"),
                                 .SIGN_NAME = dr("SIGN_NAME"),
                                 .APPROVE_DATE = If(dr("APPROVE_DATE") IsNot Nothing, ToDate(dr("APPROVE_DATE")), Nothing),
                                 .APPROVE_STATUS = Decimal.Parse(dr("APPROVE_STATUS"))})
                    Next
                End If
            End Using
            If _filter.SIGN_ID.HasValue Then
                result = result.Where(Function(f) f.SIGN_ID = _filter.SIGN_ID)
            End If
            'result = result.OrderBy(Sorts)
            'Total = result.Count
            'result = result.Skip(PageIndex * PageSize).Take(PageSize)
            Return result.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region
#Region "Check nhân viên có thuộc dự án khi chấm công"
    Public Function CheckExistEm_Pro(ByVal lstID As Decimal, ByVal FromDate As Date, ByVal ToDate As Date, ByVal IDPro As Decimal) As Boolean
        Return True
    End Function
#End Region

#Region "OT"

    Public Function CHECK_LEAVE_ORG_PAUSE_OT(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_EMPLOYEE_ID = P_EMP_ID,
                                    .P_FROM_DATE = P_DATE,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_AT_LEAVESHEET.CHECK_LEAVE_ORG_PAUSE_OT", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CheckRegDateBetweenJoinAndTerDate(ByVal empId As Decimal, ByVal regDate As Date) As Boolean
        Try
            Dim emp = Context.HU_EMPLOYEE.Where(Function(f) f.ID = empId).FirstOrDefault
            If Not emp.TER_LAST_DATE Is Nothing Then
                If regDate < emp.JOIN_DATE Or regDate > emp.TER_LAST_DATE Then
                    Return False
                End If
                Return True
            Else
                If regDate < emp.JOIN_DATE Then
                    Return False
                End If
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Using cls As New DataAccess.QueryData
                Dim userIdOrMngId As Decimal
                Dim dt As DataTable
                Dim obj
                If _filter.P_MANAGER = "APP" Then
                    obj = New With {.P_ID = _filter.ID,
                                    .P_RESULT = cls.OUT_CURSOR}
                    dt = cls.ExecuteStore("PKG_AT_PROCESS.GET_OT_BY_EMPLOYEE", obj)
                Else

                    If _filter.P_MANAGER_ID.ToString <> "" Then

                        userIdOrMngId = _filter.P_MANAGER_ID
                        obj = New With {.P_EMPLOYEE_APP_ID = userIdOrMngId,
                                        .P_STATUS = _filter.STATUS,
                                        .P_FROM_DATE = _filter.REGIST_DATE_FROM,
                                        .P_TO_DATE = _filter.REGIST_DATE_TO,
                                        .P_RESULT = cls.OUT_CURSOR}
                        dt = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETOT_BY_APPROVE", obj)

                    Else
                        userIdOrMngId = Decimal.Parse(_filter.P_USER)
                        obj = New With {.P_EMPLOYEE_APP_ID = userIdOrMngId,
                                        .P_STATUS = _filter.STATUS,
                                        .P_FROM_DATE = _filter.REGIST_DATE_FROM,
                                        .P_TO_DATE = _filter.REGIST_DATE_TO,
                                        .P_REG_DATE = _filter.REGIST_DATE,
                                        .P_RESULT = cls.OUT_CURSOR}
                        dt = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETOT_BY_EMPLOYEE", obj)
                    End If
                End If
                Dim lst As New List(Of AT_OT_REGISTRATIONDTO)
                For Each row As DataRow In dt.Rows
                    Dim dto As New AT_OT_REGISTRATIONDTO
                    dto.ID = If(row("ID").ToString <> "", row("ID"), Nothing)
                    dto.EMPLOYEE_ID = If(row("EMPLOYEE_ID").ToString <> "", row("EMPLOYEE_ID"), Nothing)
                    dto.EMPLOYEE_CODE = If(row("EMPLOYEE_CODE").ToString <> "", row("EMPLOYEE_CODE"), Nothing)
                    dto.FULLNAME = If(row("FULLNAME_VN").ToString <> "", row("FULLNAME_VN"), Nothing)
                    dto.DEPARTMENT_ID = If(row("ORG_ID").ToString <> "", row("ORG_ID"), Nothing)
                    dto.DEPARTMENT_NAME = If(row("ORG_NAME").ToString <> "", row("ORG_NAME"), Nothing)
                    dto.TITLE_ID = If(row("TITLE_ID").ToString <> "", row("TITLE_ID"), Nothing)
                    dto.TITLE_NAME = If(row("TITLE_NAME").ToString <> "", row("TITLE_NAME"), Nothing)
                    dto.REGIST_DATE = If(row("REGIST_DATE").ToString <> "", row("REGIST_DATE"), Nothing)
                    dto.SIGN_ID = If(row("SIGN_ID").ToString <> "", row("SIGN_ID"), Nothing)
                    dto.SIGN_CODE = If(row("SIGN_CODE").ToString <> "", row("SIGN_CODE"), Nothing)
                    dto.OT_TYPE_ID = If(row("OT_TYPE_ID").ToString <> "", row("OT_TYPE_ID"), Nothing)
                    dto.OT_TYPE_NAME = If(row("OT_TYPE_NAME").ToString <> "", row("OT_TYPE_NAME"), Nothing)
                    'dto.ID_REGGROUP = row("ID_REGGROUP")
                    dto.TOTAL_OT = If(row("TOTAL_OT").ToString <> "", row("TOTAL_OT"), Nothing)
                    dto.OT_100 = If(row("OT_100").ToString <> "", row("OT_100"), Nothing)
                    dto.OT_150 = If(row("OT_150").ToString <> "", row("OT_150"), Nothing)
                    dto.OT_180 = If(row("OT_180").ToString <> "", row("OT_180"), Nothing)
                    dto.OT_200 = If(row("OT_200").ToString <> "", row("OT_200"), Nothing)
                    dto.OT_210 = If(row("OT_210").ToString <> "", row("OT_210"), Nothing)
                    dto.OT_270 = If(row("OT_270").ToString <> "", row("OT_270"), Nothing)
                    dto.OT_300 = If(row("OT_300").ToString <> "", row("OT_300"), Nothing)
                    dto.OT_370 = If(row("OT_370").ToString <> "", row("OT_370"), Nothing)
                    dto.FROM_AM = If(row("FROM_AM").ToString <> "", row("FROM_AM"), Nothing)
                    dto.FROM_AM_MN = If(row("FROM_AM_MN").ToString <> "", row("FROM_AM_MN"), Nothing)
                    dto.TO_AM = If(row("TO_AM").ToString <> "", row("TO_AM"), Nothing)
                    dto.TO_AM_MN = If(row("TO_AM_MN").ToString <> "", row("TO_AM_MN"), Nothing)
                    dto.FROM_PM = If(row("FROM_PM").ToString <> "", row("FROM_PM"), Nothing)
                    dto.FROM_PM_MN = If(row("FROM_PM_MN").ToString <> "", row("FROM_PM_MN"), Nothing)
                    dto.TO_PM = If(row("TO_PM").ToString <> "", row("TO_PM"), Nothing)
                    dto.TO_PM_MN = If(row("TO_PM_MN").ToString <> "", row("TO_PM_MN"), Nothing)
                    dto.STATUS = If(row("STATUS").ToString <> "", row("STATUS"), Nothing)
                    dto.STATUS_NAME = If(row("STATUS_NAME").ToString <> "", row("STATUS_NAME"), Nothing)
                    dto.REASON = If(row("REASON").ToString <> "", row("REASON"), Nothing)
                    dto.NOTE = If(row("NOTE").ToString <> "", row("NOTE"), Nothing)
                    dto.IS_DELETED = If(row("IS_DELETED").ToString <> "", row("IS_DELETED"), Nothing)
                    dto.CREATED_BY = If(row("CREATED_BY").ToString <> "", row("CREATED_BY"), Nothing)
                    dto.CREATED_DATE = If(row("CREATED_DATE").ToString <> "", row("CREATED_DATE"), Nothing)
                    dto.CREATED_LOG = If(row("CREATED_LOG").ToString <> "", row("CREATED_LOG"), Nothing)
                    dto.MODIFIED_BY = If(row("MODIFIED_BY").ToString <> "", row("MODIFIED_BY"), Nothing)
                    dto.MODIFIED_DATE = If(row("MODIFIED_DATE").ToString <> "", row("MODIFIED_DATE"), Nothing)
                    dto.MODIFIED_LOG = If(row("MODIFIED_LOG").ToString <> "", row("MODIFIED_LOG"), Nothing)
                    dto.MODIFIED_NAME = If(row("FULLNAME").ToString <> "", row("FULLNAME"), Nothing)
                    lst.Add(dto)
                Next
                If _filter.REGIST_DATE_FROM.HasValue And _filter.REGIST_DATE_TO.HasValue Then
                    lst = (From p In lst.AsEnumerable Where p.REGIST_DATE <= _filter.REGIST_DATE_TO And p.REGIST_DATE >= _filter.REGIST_DATE_FROM
                           Select p).ToList
                Else
                    If _filter.REGIST_DATE_FROM.HasValue Then
                        lst = (From f In lst.AsEnumerable Where f.REGIST_DATE >= _filter.REGIST_DATE_FROM
                               Select f).ToList
                    ElseIf _filter.REGIST_DATE_TO.HasValue Then
                        lst = (From f In lst.AsEnumerable Where f.REGIST_DATE <= _filter.REGIST_DATE_TO
                               Select f).ToList
                    End If
                End If
                If _filter.STATUS > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS = _filter.STATUS
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                    lst = (From f In lst.AsEnumerable Where f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                    lst = (From f In lst.AsEnumerable Where f.FULLNAME.ToLower().Contains(_filter.FULLNAME.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.DEPARTMENT_NAME.ToLower().Contains(_filter.DEPARTMENT_NAME.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower())
                           Select f).ToList
                End If

                'If _filter.REGIST_DATE.HasValue Then
                '    lst = (From f In lst.AsEnumerable Where f.REGIST_DATE >= _filter.REGIST_DATE
                '               Select f).ToList
                'End If

                If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                    lst = (From f In lst.AsEnumerable Where f.SIGN_CODE.ToLower().Contains(_filter.SIGN_CODE.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.OT_TYPE_NAME.ToLower().Contains(_filter.OT_TYPE_NAME.ToLower())
                           Select f).ToList
                End If

                If Not _filter.P_MANAGER_ID.HasValue And _filter.EMPLOYEE_ID > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                           Select f).ToList
                End If
                If _filter.ID > 0 Then
                    lst = (From f In lst.AsEnumerable Where f.ID = _filter.ID
                           Select f).ToList
                End If
                'lst = lst.Where(Function(f) f.IS_DELETED = 0)

                If _filter.OT_100.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_100 = _filter.OT_100
                           Select f).ToList
                End If

                If _filter.OT_100.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_100 = _filter.OT_100
                           Select f).ToList
                End If

                If _filter.OT_150.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_150 = _filter.OT_150
                           Select f).ToList
                End If

                If _filter.OT_200.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_200 = _filter.OT_200
                           Select f).ToList
                End If

                If _filter.OT_210.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_210 = _filter.OT_210
                           Select f).ToList
                End If

                If _filter.OT_270.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_270 = _filter.OT_270
                           Select f).ToList
                End If

                If _filter.OT_300.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_300 = _filter.OT_300
                           Select f).ToList
                End If

                If _filter.OT_370.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.OT_370 = _filter.OT_370
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.NOTE) Then
                    lst = (From f In lst.AsEnumerable Where f.NOTE.ToLower().Contains(_filter.NOTE.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.REASON) Then
                    lst = (From f In lst.AsEnumerable Where f.REASON.ToLower().Contains(_filter.REASON.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                    lst = (From f In lst.AsEnumerable Where f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower())
                           Select f).ToList
                End If

                If Not String.IsNullOrEmpty(_filter.MODIFIED_BY) Then
                    lst = (From f In lst.AsEnumerable Where f.MODIFIED_BY.ToLower().Contains(_filter.MODIFIED_BY.ToLower())
                           Select f).ToList
                End If

                If _filter.MODIFIED_DATE.HasValue Then
                    lst = (From f In lst.AsEnumerable Where f.MODIFIED_DATE = _filter.MODIFIED_DATE
                           Select f).ToList
                End If

                lst = (From f In lst.AsEnumerable
                       Order By f.CREATED_DATE Descending
                       Select f).ToList
                Total = lst.Count
                lst = (From f In lst.AsEnumerable
                       Skip PageIndex * PageSize
                       Take PageSize
                       Select f).ToList
                'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                Return lst.ToList
            End Using
            Total = 0
            Return New List(Of AT_OT_REGISTRATIONDTO)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetOtRegistrationForTimeSheet(ByVal _filter As AT_OT_REGISTRATIONDTO) As List(Of AT_OT_REGISTRATIONDTO)
        Try
            Dim query = From r In Context.AT_OT_REGISTRATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = r.EMPLOYEE_ID)
                        Where r.STATUS = PortalStatus.ApprovedByLM
            Dim lst = query.Select(Function(p) New AT_OT_REGISTRATIONDTO With {
                                       .ID = p.r.ID,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME = p.e.FULLNAME_VN,
                                       .DEPARTMENT_ID = p.e.ORG_ID,
                                       .REGIST_DATE = p.r.REGIST_DATE,
                                       .SIGN_ID = p.r.SIGN_ID,
                                       .SIGN_CODE = p.r.SIGN_CODE,
                                       .OT_TYPE_ID = p.r.OT_TYPE_ID,
                                       .TOTAL_OT = If(p.r.TOTAL_OT Is Nothing, 0, p.r.TOTAL_OT),
                                       .OT_100 = If(p.r.OT_100 Is Nothing, 0, p.r.OT_100),
                                       .OT_150 = If(p.r.OT_150 Is Nothing, 0, p.r.OT_150),
                                       .OT_180 = If(p.r.OT_180 Is Nothing, 0, p.r.OT_180),
                                       .OT_200 = If(p.r.OT_200 Is Nothing, 0, p.r.OT_200),
                                       .OT_210 = If(p.r.OT_210 Is Nothing, 0, p.r.OT_210),
                                       .OT_270 = If(p.r.OT_270 Is Nothing, 0, p.r.OT_270),
                                       .OT_300 = If(p.r.OT_300 Is Nothing, 0, p.r.OT_300),
                                       .OT_370 = If(p.r.OT_370 Is Nothing, 0, p.r.OT_370),
                                       .FROM_AM = p.r.FROM_AM,
                                       .FROM_AM_MN = p.r.FROM_AM_MN,
                                       .TO_AM = p.r.TO_AM,
                                       .TO_AM_MN = p.r.TO_AM_MN,
                                       .FROM_PM = p.r.FROM_PM,
                                       .FROM_PM_MN = p.r.FROM_PM_MN,
                                       .TO_PM = p.r.TO_PM,
                                       .TO_PM_MN = p.r.TO_PM_MN,
                                       .STATUS = p.r.STATUS,
                                       .REASON = p.r.REASON,
                                       .NOTE = p.r.NOTE,
                                       .IS_DELETED = p.r.IS_DELETED,
                                       .CREATED_BY = p.r.CREATED_BY,
                                       .CREATED_DATE = p.r.CREATED_DATE,
                                       .CREATED_LOG = p.r.CREATED_LOG,
                                       .MODIFIED_DATE = p.r.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.r.MODIFIED_LOG})
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_OT_REGISTRATION
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_OT_REGISTRATION.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.OT_TYPE_ID = obj.OT_TYPE_ID
            objData.REASON = obj.REASON
            objData.NOTE = obj.NOTE
            objData.IS_DELETED = obj.IS_DELETED
            objData.OT_100 = obj.OT_100
            objData.OT_150 = obj.OT_150
            objData.OT_180 = obj.OT_180
            objData.OT_200 = obj.OT_200
            objData.OT_210 = obj.OT_210
            objData.OT_270 = obj.OT_270
            objData.OT_300 = obj.OT_300
            objData.OT_370 = obj.OT_370
            objData.FROM_AM = obj.FROM_AM
            objData.FROM_AM_MN = obj.FROM_AM_MN
            objData.TO_AM = obj.TO_AM
            objData.TO_AM_MN = obj.TO_AM_MN
            objData.FROM_PM = obj.FROM_PM
            objData.FROM_PM_MN = obj.FROM_PM_MN
            objData.TO_PM = obj.TO_PM
            objData.TO_PM_MN = obj.TO_PM_MN
            objData.REGIST_DATE = obj.REGIST_DATE
            objData.SIGN_CODE = obj.SIGN_CODE
            objData.SIGN_ID = obj.SIGN_ID
            objData.STATUS = obj.STATUS
            objData.TOTAL_OT = obj.TOTAL_OT
            objData.ID_REGGROUP = Utilities.GetNextSequence(Context, Context.AT_PORTAL_APP.EntitySet.Name)
            Context.AT_OT_REGISTRATION.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_OT_REGISTRATION With {.ID = obj.ID}
        Try
            Context.AT_OT_REGISTRATION.Attach(objData)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.OT_TYPE_ID = obj.OT_TYPE_ID
            objData.REASON = obj.REASON
            objData.NOTE = obj.NOTE
            objData.IS_DELETED = obj.IS_DELETED
            objData.OT_100 = obj.OT_100
            objData.OT_150 = obj.OT_150
            objData.OT_180 = obj.OT_180
            objData.OT_200 = obj.OT_200
            objData.OT_210 = obj.OT_210
            objData.OT_270 = obj.OT_270
            objData.OT_300 = obj.OT_300
            objData.OT_370 = obj.OT_370
            objData.FROM_AM = obj.FROM_AM
            objData.FROM_AM_MN = obj.FROM_AM_MN
            objData.TO_AM = obj.TO_AM
            objData.TO_AM_MN = obj.TO_AM_MN
            objData.FROM_PM = obj.FROM_PM
            objData.FROM_PM_MN = obj.FROM_PM_MN
            objData.TO_PM = obj.TO_PM
            objData.TO_PM_MN = obj.TO_PM_MN
            objData.REGIST_DATE = obj.REGIST_DATE
            objData.SIGN_CODE = obj.SIGN_CODE
            objData.SIGN_ID = obj.SIGN_ID
            objData.STATUS = obj.STATUS
            objData.TOTAL_OT = obj.TOTAL_OT
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function SendApproveOTRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Integer
        Try

            Using cls As New DataAccess.QueryData
                For Each item In obj
                    Dim dtCheckSendApprove As DataTable = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.CHECK_SEND_APPROVE",
                                                                           New With {.P_ID = item.ID.ToString, .P_OUT_CUR = cls.OUT_CURSOR})
                    Dim processType As String = "OVERTIME"
                    Dim periodId As Integer = item.PERIOD_ID
                    Dim signId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("SIGN_ID").ToString())
                    Dim totalHours As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("SUMVAL").ToString())
                    Dim IdGroup1 As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("ID_REGGROUP").ToString())
                    Dim priProcessApp = New With {.P_EMPLOYEE_ID = item.EMPLOYEE_ID, .P_PERIOD_ID = periodId, .P_PROCESS_TYPE = processType, .P_TOTAL_HOURS = totalHours, .P_TOTAL_DAY = 0, .P_SIGN_ID = signId, .P_ID_REGGROUP = IdGroup1, .P_TOKEN = "", .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", priProcessApp)
                    Dim outNumber As Integer = Int32.Parse(priProcessApp.P_RESULT)
                    Return outNumber
                Next
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal empId As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                For Each item In obj
                    Dim processType As String = "OVERTIME"
                    Dim periodId As Integer = Context.AT_PERIOD.Where(Function(f) f.START_DATE <= item.REGIST_DATE AndAlso item.REGIST_DATE <= f.END_DATE).Select(Function(f) f.ID).FirstOrDefault
                    Dim IdGroup1 As Decimal = item.ID_REGGROUP
                    Dim priProcess = New With {.P_EMPLOYEE_APP_ID = empId, .P_EMPLOYEE_ID = item.EMPLOYEE_ID, .P_PE_PERIOD_ID = periodId, .P_STATUS_ID = item.STATUS, .P_PROCESS_TYPE = processType, .P_NOTES = item.REASON, .P_ID_REGGROUP = IdGroup1, .P_RESULT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS", priProcess)
                    Dim outNumber As Integer = Int32.Parse(priProcess.P_RESULT)
                    If outNumber <> 0 Then
                        Return False
                    End If
                Next
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO)
        Try
            Dim reg = (From p In Context.AT_OT_REGISTRATION Where p.ID <> _validate.ID And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.REGIST_DATE = _validate.REGIST_DATE Select p).ToList

            'objOT.FROM_AM,
            'objOT.FROM_AM_MN,
            'objOT.TO_AM,
            'objOT.TO_AM_MN,
            'objOT.FROM_PM,
            'objOT.FROM_PM_MN,
            'objOT.TO_PM,
            'objOT.TO_PM_MN,

            'lấy v2 check so với s1,s2/ check timeline số phút trong ngày
            For Each item In reg
                If item.FROM_AM IsNot Nothing And _validate.FROM_AM IsNot Nothing Then
                    Dim s1 = item.FROM_AM * 60 + item.FROM_AM_MN
                    Dim s2 = item.TO_AM * 60 + item.TO_AM_MN

                    Dim v1 = _validate.FROM_AM * 60 + _validate.FROM_AM_MN
                    Dim v2 = _validate.TO_AM * 60 + _validate.TO_AM_MN

                    If (s1 < v2 And v2 < s2) Or (v1 < s2 And s2 < v2) Or v2 = s2 Then
                        Return False
                    End If
                End If

                If item.FROM_PM IsNot Nothing And _validate.FROM_PM IsNot Nothing Then
                    Dim s1 = item.FROM_PM * 60 + item.FROM_PM_MN
                    Dim s2 = item.TO_PM * 60 + item.TO_PM_MN

                    Dim v1 = _validate.FROM_PM * 60 + _validate.FROM_PM_MN
                    Dim v2 = _validate.TO_PM * 60 + _validate.TO_PM_MN

                    If (s1 < v2 And v2 < s2) Or (v1 < s2 And s2 < v2) Or v2 = s2 Then
                        Return False
                    End If
                End If
            Next
            '========================
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateOtRegistration_bk(ByVal _validate As AT_OT_REGISTRATIONDTO)
        Dim query
        Try
            Dim fromHourAM As Integer = 0
            Dim fromHourPM As Integer = 0
            If _validate.FROM_AM IsNot Nothing Then
                fromHourAM = Int32.Parse(_validate.FROM_AM).ToString("00") & Int32.Parse(_validate.FROM_AM_MN).ToString("00")
            End If
            If _validate.FROM_PM IsNot Nothing Then
                fromHourPM = Int32.Parse(_validate.FROM_PM).ToString("00") & Int32.Parse(_validate.FROM_PM_MN).ToString("00")
            End If
            If _validate.ID <> 0 Then
                query = (From p In Context.AT_OT_REGISTRATION
                         Where p.ID <> _validate.ID _
                           And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                           And p.REGIST_DATE = _validate.REGIST_DATE _
                           And p.IS_DELETED = 0).ToList()
                For Each item As AT_OT_REGISTRATION In query
                    If item.TO_PM Is Nothing AndAlso item.TO_AM.HasValue Then
                        Dim fromHourAMCompare = Int32.Parse(Int32.Parse(item.FROM_AM).ToString("00") & Int32.Parse(item.FROM_AM_MN).ToString("00"))
                        Dim toHourAMCompare = Int32.Parse(Int32.Parse(item.TO_AM).ToString("00") & Int32.Parse(item.TO_AM_MN).ToString("00"))
                        If fromHourAM <> 0 AndAlso fromHourAM >= fromHourAMCompare AndAlso fromHourAM <= toHourAMCompare Then
                            Return True
                        End If
                    ElseIf item.TO_AM Is Nothing AndAlso item.TO_PM.HasValue Then
                        Dim fromHourPMCompare = Int32.Parse(Int32.Parse(item.FROM_PM).ToString("00") & Int32.Parse(item.FROM_PM_MN).ToString("00"))
                        Dim toHourPMCompare = Int32.Parse(Int32.Parse(item.TO_PM).ToString("00") & Int32.Parse(item.TO_PM_MN).ToString("00"))
                        If fromHourPM <> 0 AndAlso fromHourPM >= fromHourPMCompare AndAlso fromHourPM <= toHourPMCompare Then
                            Return True
                        End If
                    Else
                        Dim fromHourAMCompare = Int32.Parse(Int32.Parse(item.FROM_AM).ToString("00") & Int32.Parse(item.FROM_AM_MN).ToString("00"))
                        Dim toHourAMCompare = Int32.Parse(Int32.Parse(item.TO_AM).ToString("00") & Int32.Parse(item.TO_AM_MN).ToString("00"))
                        Dim fromHourPMCompare = Int32.Parse(Int32.Parse(item.FROM_PM).ToString("00") & Int32.Parse(item.FROM_PM_MN).ToString("00"))
                        Dim toHourPMCompare = Int32.Parse(Int32.Parse(item.TO_PM).ToString("00") & Int32.Parse(item.TO_PM_MN).ToString("00"))
                        If fromHourAM >= fromHourAMCompare AndAlso fromHourAM <= toHourAMCompare Then
                            Return True
                        End If
                        If fromHourPM >= fromHourPMCompare AndAlso fromHourAM <= toHourPMCompare Then
                            Return True
                        End If
                    End If
                Next
                Return False
                'And ((_validate.OT_100 <> 0 And p.OT_100 <> 0) Or _validate.OT_100 = 0) _
                '           And ((_validate.OT_150 <> 0 And p.OT_150 <> 0) Or _validate.OT_150 = 0) _
                '           And ((_validate.OT_200 <> 0 And p.OT_200 <> 0) Or _validate.OT_200 = 0) _
                '           And ((_validate.OT_210 <> 0 And p.OT_210 <> 0) Or _validate.OT_210 = 0) _
                '           And ((_validate.OT_270 <> 0 And p.OT_270 <> 0) Or _validate.OT_270 = 0) _
                '           And ((_validate.OT_300 <> 0 And p.OT_300 <> 0) Or _validate.OT_300 = 0) _
                '           And ((_validate.OT_370 <> 0 And p.OT_370 <> 0) Or _validate.OT_370 = 0) _
            Else
                query = (From p In Context.AT_OT_REGISTRATION
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                         And p.REGIST_DATE = _validate.REGIST_DATE _
                         And p.IS_DELETED = 0 _
                         And p.SIGN_ID.HasValue).ToList()
                For Each item As AT_OT_REGISTRATION In query
                    If item.TO_PM Is Nothing AndAlso item.TO_AM.HasValue Then
                        Dim fromHourAMCompare = Int32.Parse(Int32.Parse(item.FROM_AM).ToString("00") & Int32.Parse(item.FROM_AM_MN).ToString("00"))
                        Dim toHourAMCompare = Int32.Parse(Int32.Parse(item.TO_AM).ToString("00") & Int32.Parse(item.TO_AM_MN).ToString("00"))
                        If fromHourAM <> 0 AndAlso fromHourAM >= fromHourAMCompare AndAlso fromHourAM <= toHourAMCompare Then
                            Return True
                        End If
                    ElseIf item.TO_AM Is Nothing AndAlso item.TO_PM.HasValue Then
                        Dim fromHourPMCompare = Int32.Parse(Int32.Parse(item.FROM_PM).ToString("00") & Int32.Parse(item.FROM_PM_MN).ToString("00"))
                        Dim toHourPMCompare = Int32.Parse(Int32.Parse(item.TO_PM).ToString("00") & Int32.Parse(item.TO_PM_MN).ToString("00"))
                        If fromHourPM <> 0 AndAlso fromHourPM >= fromHourPMCompare AndAlso fromHourPM <= toHourPMCompare Then
                            Return True
                        End If
                    Else
                        Dim fromHourAMCompare = Int32.Parse(Int32.Parse(item.FROM_AM).ToString("00") & Int32.Parse(item.FROM_AM_MN).ToString("00"))
                        Dim toHourAMCompare = Int32.Parse(Int32.Parse(item.TO_AM).ToString("00") & Int32.Parse(item.TO_AM_MN).ToString("00"))
                        Dim fromHourPMCompare = Int32.Parse(Int32.Parse(item.FROM_PM).ToString("00") & Int32.Parse(item.FROM_PM_MN).ToString("00"))
                        Dim toHourPMCompare = Int32.Parse(Int32.Parse(item.TO_PM).ToString("00") & Int32.Parse(item.TO_PM_MN).ToString("00"))
                        If fromHourAM >= fromHourAMCompare AndAlso fromHourAM <= toHourAMCompare Then
                            Return True
                        End If
                        If fromHourPM >= fromHourPMCompare AndAlso fromHourAM <= toHourPMCompare Then
                            Return True
                        End If
                    End If
                Next
                Return False
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function Validate_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim strREGIST_DATE = CDate(_validate.REGIST_DATE).ToString("yyyy-MM-dd")
                Dim strFROM_AM As String = Nothing
                Dim strTO_AM As String = Nothing
                Dim strFROM_AM_MN As String = Nothing
                Dim strTO_AM_MN As String = Nothing
                Dim strFROM_PM As String = Nothing
                Dim strTO_PM As String = Nothing
                Dim strFROM_PM_MN As String = Nothing
                Dim strTO_PM_MN As String = Nothing
                If _validate.FROM_AM IsNot Nothing Then
                    If CInt(_validate.FROM_AM) < 10 Then
                        strFROM_AM = "0" & CInt(_validate.FROM_AM).ToString
                    Else
                        strFROM_AM = CInt(_validate.FROM_AM).ToString
                    End If

                End If
                If _validate.TO_AM IsNot Nothing Then
                    If CInt(_validate.TO_AM) < 10 Then
                        strTO_AM = "0" & CInt(_validate.TO_AM).ToString
                    Else
                        strTO_AM = CInt(_validate.TO_AM).ToString
                    End If

                End If

                If _validate.FROM_AM_MN IsNot Nothing Then
                    If CInt(_validate.FROM_AM_MN) < 10 Then
                        strFROM_AM_MN = "0" & CInt(_validate.FROM_AM_MN).ToString
                    Else
                        strFROM_AM_MN = CInt(_validate.FROM_AM_MN).ToString
                    End If

                End If
                If _validate.TO_AM_MN IsNot Nothing Then
                    If CInt(_validate.TO_AM_MN) < 10 Then
                        strTO_AM_MN = "0" & CInt(_validate.TO_AM_MN).ToString
                    Else
                        strTO_AM_MN = CInt(_validate.TO_AM_MN).ToString
                    End If
                End If
                If _validate.FROM_PM IsNot Nothing Then
                    If CInt(_validate.FROM_PM) < 10 Then
                        strFROM_PM = "0" & CInt(_validate.FROM_PM).ToString
                    Else
                        strFROM_PM = CInt(_validate.FROM_PM).ToString
                    End If

                End If
                If _validate.TO_PM IsNot Nothing Then
                    If CInt(_validate.TO_PM) < 10 Then
                        strTO_PM = "0" & CInt(_validate.TO_PM).ToString
                    Else
                        strTO_PM = CInt(_validate.TO_PM).ToString
                    End If

                End If
                If _validate.FROM_PM_MN IsNot Nothing Then
                    If CInt(_validate.FROM_PM_MN) < 10 Then
                        strFROM_PM_MN = "0" & CInt(_validate.FROM_PM_MN).ToString
                    Else
                        strFROM_PM_MN = CInt(_validate.FROM_PM_MN).ToString
                    End If

                End If
                If _validate.TO_PM_MN IsNot Nothing Then
                    If CInt(_validate.TO_PM_MN) < 10 Then
                        strTO_PM_MN = "0" & CInt(_validate.TO_PM_MN).ToString
                    Else
                        strTO_PM_MN = CInt(_validate.TO_PM_MN).ToString
                    End If

                End If

                Dim obj = New With {.P_LANGUAGE = "VI-VN",
                                                         .P_USERID = CInt(_validate.USERID),
                                                         .P_ID = CInt(_validate.ID),
                                                         .P_OTFROM = strREGIST_DATE,
                                                         .P_SIGN_ID = CInt(_validate.SIGN_ID),
                                                         .P_FROM_AM_HH = strFROM_AM,
                                                         .P_TO_AM_HH = strTO_AM,
                                                         .P_FROM_AM_MI = strFROM_AM_MN,
                                                         .P_TO_AM_MI = strTO_AM_MN,
                                                         .P_FROM_PM_HH = strFROM_PM,
                                                         .P_TO_PM_HH = strTO_PM,
                                                         .P_FROM_PM_MI = strFROM_PM_MN,
                                                         .P_TO_PM_MI = strTO_PM_MN,
                                                         .P_ISFRHOURAFTER = _validate.PM_FROMHOURS_AFTERCHECK,
                                                         .P_ISTOHOURAFTER = _validate.PM_TOHOURS_AFTERCHECK,
                                                         .P_CREATED_BY_EMP = 0,
                                                         .P_BY_ANOTHER = 0,
                                                         .P_MESSAGE = cls.OUT_STRING,
                                                         .P_RESPONSESTATUS = cls.OUT_NUMBER}
                Dim dtData = cls.ExecuteStore("PKG_API_MOBILE.API_VALIDATE_ORVERTIME", obj)
                Dim param = New VALIDATE_DTO
                param.MESSAGE = obj.P_MESSAGE
                param.RESPONSESTATUS = obj.P_RESPONSESTATUS
                Return param
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function Api_register_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim strREGIST_DATE = CDate(_validate.REGIST_DATE).ToString("yyyy-MM-dd")
                Dim strFROM_AM = _validate.FROM_AM
                Dim strTO_AM = _validate.TO_AM
                Dim strFROM_AM_MN = _validate.FROM_AM_MN
                Dim strTO_AM_MN = _validate.TO_AM_MN
                Dim strFROM_PM = _validate.FROM_PM
                Dim strTO_PM = _validate.TO_PM
                Dim strFROM_PM_MN = _validate.FROM_PM_MN
                Dim strTO_PM_MN = _validate.TO_PM_MN
                Dim obj = New With {.P_USERID = CInt(_validate.USERID),
                                                         .P_LANGUAGE = "VI-VN",
                                                         .P_ID = CInt(_validate.ID),
                                                         .P_OT_TYPEID = _validate.OT_TYPE_ID,
                                                         .P_REASON = _validate.REASON,
                                                         .P_NOTE = _validate.NOTE,
                                                         .P_FROM_AM = strFROM_AM,
                                                         .P_FROM_AM_MN = strFROM_AM_MN,
                                                         .P_TO_AM = strTO_AM,
                                                         .P_TO_AM_MN = strTO_AM_MN,
                                                         .P_FROM_PM = strFROM_PM,
                                                         .P_FROM_PM_MN = strFROM_PM_MN,
                                                         .P_TO_PM = strTO_PM,
                                                         .P_TO_PM_MN = strTO_PM_MN,
                                                         .P_REGISTER_DATE = strREGIST_DATE,
                                                         .P_SIGN_CODE = _validate.SIGN_CODE,
                                                         .P_SIGN_ID = CInt(_validate.SIGN_ID),
                                                         .P_TOTAL = 0,
                                                         .P_STATUS = _validate.STATUS,
                                                         .P_PM_FROMHOURS_AFTERCHECK = _validate.PM_FROMHOURS_AFTERCHECK,
                                                         .P_PM_TOHOURS_AFTERCHECK = _validate.PM_TOHOURS_AFTERCHECK,
                                                         .P_IS_PASS_DAY = _validate.IS_PASS_DAY,
                                                         .P_HOURS_TOTAL_AM = 0,
                                                         .P_HOURS_TOTAL_PM = 0,
                                                         .P_HOURS_TOTAL_DAY = 0,
                                                         .P_HOURS_TOTAL_NIGHT = 0,
                                                         .P_DK_PORTAL = -1,
                                                         .P_HS_OT = Nothing,
                                                         .P_ORG_OT_ID = _validate.ORG_OT_ID,
                                                         .P_CREATED_BY_EMP = 0,
                                                         .P_BY_ANOTHER = 0,
                                                         .P_TOTAL_DAY_TT = 0,
                                                         .P_ACTION = _validate.ACTION,
                                                         .P_MESSAGE = cls.OUT_STRING,
                                                         .P_RESPONSESTATUS = cls.OUT_NUMBER}
                Dim dtData = cls.ExecuteStore("PKG_API_MOBILE.API__REGISTER_ORVERTIME", obj)
                Dim param = New VALIDATE_DTO
                param.MESSAGE = obj.P_MESSAGE
                param.RESPONSESTATUS = obj.P_RESPONSESTATUS
                Return param
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function API__SEND_APPROVE_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERID = CInt(_validate.USERID),
                                                         .P_LANGUAGE = "VI-VN",
                                                         .P_IDGROUPS = _validate.IDGROUPS,
                                                         .P_MESSAGE = cls.OUT_STRING,
                                                         .P_RESPONSESTATUS = cls.OUT_NUMBER}
                Dim dtData = cls.ExecuteStore("PKG_API_MOBILE.API__SEND_APPROVE_OT", obj)
                Dim param = New VALIDATE_DTO
                param.MESSAGE = obj.P_MESSAGE
                param.RESPONSESTATUS = obj.P_RESPONSESTATUS
                Return param
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function API_APPROVEREGISTER_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_LANGUAGE = "VI-VN",
                                                         .P_USERID = CInt(_validate.USERID),
                                                         .P_IDGROUPS = _validate.IDGROUPS,
                                                         .P_COMMENT = _validate.COMMENT,
                                                         .P_STATUS = _validate.STATUS,
                                                         .P_MESSAGE = cls.OUT_STRING,
                                                         .P_RESPONSESTATUS = cls.OUT_NUMBER}
                Dim dtData = cls.ExecuteStore("PKG_API_MOBILE.API_APPROVEREGISTER_OT", obj)
                Dim param = New VALIDATE_DTO
                param.MESSAGE = obj.P_MESSAGE
                param.RESPONSESTATUS = obj.P_RESPONSESTATUS
                Return param
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            Dim listOTRegistration = (From record In Context.AT_OT_REGISTRATION Where lst.Contains(record.ID))
            For Each item In listOTRegistration
                Context.AT_OT_REGISTRATION.Attach(item)
                item.HR_REVIEW = True
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            Dim deletedLeavePlan = (From record In Context.AT_OT_REGISTRATION Where lstId.Contains(record.ID))
            For Each item In deletedLeavePlan
                Context.AT_OT_REGISTRATION.DeleteObject(item)
            Next

            Dim lst = (From p In Context.PROCESS_APPROVED_STATUS Where lstId.Contains(p.ID_REGGROUP))
            For Each item In lst
                Context.PROCESS_APPROVED_STATUS.DeleteObject(item)
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable
        Try
            Dim dtData As DataTable

            If P_EMPLOYEE = 0 Then
                Dim LstRegister = (From p In Context.AT_PORTAL_REG
                                   Where p.ID = P_ID Select p).FirstOrDefault

                P_EMPLOYEE = LstRegister.ID_EMPLOYEE
                P_DATE_TIME = LstRegister.FROM_DATE
            End If

            Using cls As New DataAccess.QueryData

                dtData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PORTAL_REG",
                                 New With {.P_ID = P_ID,
                                           .P_EMPLOYEE = P_EMPLOYEE,
                                           .P_DATE_TIME = P_DATE_TIME,
                                           .P_OUT = cls.OUT_CURSOR})
            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable
        Try
            Dim dtData As DataTable

            If P_EMPLOYEE = 0 Then
                Dim LstRegister = (From p In Context.AT_PORTAL_REG
                                   Where p.ID = P_ID Select p).FirstOrDefault

                P_EMPLOYEE = LstRegister.ID_EMPLOYEE
                P_DATE_TIME = LstRegister.FROM_DATE
            End If

            Using cls As New DataAccess.QueryData

                dtData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_PORTAL_REG_OT",
                                 New With {.P_ID = P_ID,
                                           .P_EMPLOYEE = P_EMPLOYEE,
                                           .P_DATE_TIME = P_DATE_TIME,
                                           .P_OUT = cls.OUT_CURSOR})
            End Using
            Return dtData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPortalOtRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "
            If IsNumeric(_filter.ID) AndAlso _filter.ID > 0 Then
                WHERE_CONDITION += " AND OT.ID =" + CInt(_filter.ID).ToString + " "
            End If
            If IsNumeric(_filter.STATUS) Then
                WHERE_CONDITION += " AND OT.STATUS =" + _filter.STATUS.ToString + " "
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT.STATUS_NAME) LIKE '%" + _filter.STATUS_NAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                WHERE_CONDITION += " AND ( UPPER(E.EMPLOYEE_CODE) LIKE  '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%'"
                WHERE_CONDITION += " OR UPPER(E.FULLNAME_VN) LIKE '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%')"
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                WHERE_CONDITION += " AND UPPER(E.FULLNAME_VN) LIKE '%" + _filter.FULLNAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                WHERE_CONDITION += " AND UPPER(T.NAME_VN) LIKE '%" + _filter.TITLE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT_TYPE.NAME_VN) LIKE  '%" + _filter.OT_TYPE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                WHERE_CONDITION += " AND UPPER(SH.CODE) LIKE  '%" + _filter.SIGN_CODE.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                WHERE_CONDITION += " AND UPPER(ORG.NAME_VN) LIKE  '%" + _filter.DEPARTMENT_NAME.ToUpper() + "%'"
            End If

            If _filter.REGIST_DATE.HasValue Then
                WHERE_CONDITION += " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') >= '" + _filter.REGIST_DATE.Value.ToString("yyyyMMdd") + "' "
            End If
            If _filter.REGIST_DATE_FROM.HasValue Then
                WHERE_CONDITION += " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') >= '" + _filter.REGIST_DATE_FROM.Value.ToString("yyyyMMdd") + "' "
            End If
            If _filter.REGIST_DATE_TO.HasValue Then
                WHERE_CONDITION += " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') <= '" + _filter.REGIST_DATE_TO.Value.ToString("yyyyMMdd") + "' "
            End If
            WHERE_CONDITION += " ORDER BY " + "STATUS.ORDERBYID" 'Sorts

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.GET_PORTAL_REG_DATA",
                                               New With {.P_EMP_ID = _filter.EMPLOYEE_ID,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_STARTDATE = _filter.REGIST_DATE_FROM,
                                                         .P_ENDDATE = _filter.REGIST_DATE_TO,
                                                         .P_WHERE_CONDITION = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function

    Public Function GetPortalOtApproveData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If IsNumeric(_filter.STATUS) Then
                WHERE_CONDITION += " AND A.APP_STATUS =" + _filter.STATUS.ToString() + " "
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT.STATUS_NAME) LIKE '%" + _filter.STATUS_NAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                WHERE_CONDITION += " AND ( UPPER(E.EMPLOYEE_CODE) LIKE  '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%'"
                WHERE_CONDITION += " OR UPPER(E.FULLNAME_VN) LIKE '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%')"
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                WHERE_CONDITION += " AND UPPER(E.FULLNAME_VN) LIKE '%" + _filter.FULLNAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                WHERE_CONDITION += " AND UPPER(T.NAME_VN) LIKE '%" + _filter.TITLE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT_TYPE.NAME_VN) LIKE  '%" + _filter.OT_TYPE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                WHERE_CONDITION += " AND UPPER(SH.CODE) LIKE  '%" + _filter.SIGN_CODE.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                WHERE_CONDITION += " AND UPPER(ORG.NAME_VN) LIKE  '%" + _filter.DEPARTMENT_NAME.ToUpper() + "%'"
            End If

            If _filter.REGIST_DATE.HasValue Then
                WHERE_CONDITION += " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') >= '" + _filter.REGIST_DATE.Value.ToString("yyyyMMdd") + "' "
            End If

            'WHERE_CONDITION += " ORDER BY " + Sorts

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.GET_PORTAL_APPROVE_DATA",
                                               New With {.P_EMP_ID = _filter.EMPLOYEE_ID,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_STARTDATE = _filter.REGIST_DATE_FROM,
                                                         .P_ENDDATE = _filter.REGIST_DATE_TO,
                                                         .P_WHERE_CONDITION = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function

    Public Function GetPortalOtRegByAnotherData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                                Optional ByRef Total As Integer = 0,
                                                Optional ByVal PageIndex As Integer = 0,
                                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                                Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If IsNumeric(_filter.STATUS) Then
                WHERE_CONDITION += " AND OT.STATUS =" + _filter.STATUS.ToString + " "
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT.STATUS_NAME) LIKE '%" + _filter.STATUS_NAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                WHERE_CONDITION += " AND ( UPPER(E.EMPLOYEE_CODE) LIKE  '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%'"
                WHERE_CONDITION += " OR UPPER(E.FULLNAME_VN) LIKE '%" + _filter.EMPLOYEE_CODE.ToUpper() + "%')"
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME) Then
                WHERE_CONDITION += " AND UPPER(E.FULLNAME_VN) LIKE '%" + _filter.FULLNAME.ToUpper() + "%'"
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                WHERE_CONDITION += " AND UPPER(T.NAME_VN) LIKE '%" + _filter.TITLE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.OT_TYPE_NAME) Then
                WHERE_CONDITION += " AND UPPER(OT_TYPE.NAME_VN) LIKE  '%" + _filter.OT_TYPE_NAME.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.SIGN_CODE) Then
                WHERE_CONDITION += " AND UPPER(SH.CODE) LIKE  '%" + _filter.SIGN_CODE.ToUpper() + "%'"
            End If

            If Not String.IsNullOrEmpty(_filter.DEPARTMENT_NAME) Then
                WHERE_CONDITION += " AND UPPER(ORG.NAME_VN) LIKE  '%" + _filter.DEPARTMENT_NAME.ToUpper() + "%'"
            End If

            If _filter.REGIST_DATE.HasValue Then
                WHERE_CONDITION += " AND TO_CHAR(OT.REGIST_DATE,'yyyyMMdd') >= '" + _filter.REGIST_DATE.Value.ToString("yyyyMMdd") + "' "
            End If

            WHERE_CONDITION += " ORDER BY " + " STATUS.ORDERBYID" 'Sorts

            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.GET_OT_BY_ANOTHER_DATA",
                                               New With {.P_EMP_ID = _filter.CREATED_BY_EMP,
                                                         .P_PAGE_INDEX = PageIndex + 1,
                                                         .P_PAGE_SIZE = PageSize,
                                                         .P_STARTDATE = _filter.REGIST_DATE_FROM,
                                                         .P_ENDDATE = _filter.REGIST_DATE_TO,
                                                         .P_WHERE_CONDITION = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Total = dsData.Tables(1).Rows(0)("TOTAL")
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function

#End Region
#Region "cham cong"
    Public Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean
        Try
            Dim checkData = Nothing
            'For Each it In obj
            '    If type = "OT" Then
            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And p.YEAR_ID = it.FROM_DATE.Value.Year And p.MONTH_ID = it.FROM_DATE.Value.Month _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            Return False
            '        End If
            '    ElseIf type = "LEAVE" Then
            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And ((p.YEAR_ID = it.FROM_DATE.Value.Year And p.MONTH_ID = it.FROM_DATE.Value.Month)) _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            itemError.TO_DATE = Nothing
            '            Return False
            '        End If

            '        checkData = (From p In Context.AT_TIMESHEET
            '                     Where p.EMPLOYEE_ID = it.EMPLOYEE_ID And (p.YEAR_ID = it.TO_DATE.Value.Year And p.MONTH_ID = it.TO_DATE.Value.Month) _
            '                           And (p.STATUS = PortalStatus.ApprovedByLM Or p.STATUS = PortalStatus.VerifiedByHR Or p.STATUS = PortalStatus.WaitingForApproval)).FirstOrDefault()
            '        If checkData IsNot Nothing Then
            '            itemError = it
            '            itemError.FROM_DATE = Nothing
            '            Return False
            '        End If
            '    End If
            'Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "SHIFT CYCLE"
    Public Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO)
        Try
            Dim query = From p In Context.AT_SHIFTCYCLE
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                        From e In Context.AT_FML.Where(Function(f) f.ID = p.FML01_ID).DefaultIfEmpty
                        Where e.ACTFLG = "A"

            Dim lst = query.Select(Function(p) New AT_SHIFTCYCLEDTO With {
                                       .ID = p.p.ID,
                                       .SHIFT_ID = p.p.SHIFT_ID,
                                       .SHIFT_NAME = p.s.NAME_VN,
                                       .STARTDATE = p.p.STARTDATE,
                                       .ENDDATE = p.p.ENDDATE,
                                       .NOTE = p.p.NOTE,
                                       .IS_DELETED = p.p.IS_DELETED,
                                       .FML01_ID = p.p.FML01_ID,
                                       .FML_NAME = "[" & p.e.CODE & "] " & p.e.NAME_VN,
                                       .FML02_ID = p.p.FML02_ID,
                                       .FML03_ID = p.p.FML03_ID,
                                       .FML04_ID = p.p.FML04_ID,
                                       .FML05_ID = p.p.FML05_ID,
                                       .FML06_ID = p.p.FML06_ID,
                                       .FML07_ID = p.p.FML07_ID,
                                       .FML08_ID = p.p.FML08_ID,
                                       .FML09_ID = p.p.FML09_ID,
                                       .FML10_ID = p.p.FML10_ID,
                                       .FML11_ID = p.p.FML11_ID,
                                       .FML12_ID = p.p.FML12_ID,
                                       .FML13_ID = p.p.FML13_ID,
                                       .FML14_ID = p.p.FML14_ID,
                                       .FML15_ID = p.p.FML15_ID,
                                       .FML16_ID = p.p.FML16_ID,
                                       .FML17_ID = p.p.FML17_ID,
                                       .FML18_ID = p.p.FML18_ID,
                                       .FML19_ID = p.p.FML19_ID,
                                       .FML20_ID = p.p.FML20_ID,
                                       .FML21_ID = p.p.FML21_ID,
                                       .FML22_ID = p.p.FML22_ID,
                                       .FML23_ID = p.p.FML23_ID,
                                       .FML24_ID = p.p.FML24_ID,
                                       .FML25_ID = p.p.FML25_ID,
                                       .FML26_ID = p.p.FML26_ID,
                                       .FML27_ID = p.p.FML27_ID,
                                       .FML28_ID = p.p.FML28_ID,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.SHIFT_NAME.ToLower().Contains(_filter.SHIFT_NAME.ToLower()))
            End If
            If _filter.SHIFT_ID.HasValue Then
                lst = lst.Where(Function(f) f.SHIFT_ID = _filter.SHIFT_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.FML_NAME) Then
                lst = lst.Where(Function(f) f.FML_NAME.ToLower().Contains(_filter.FML_NAME.ToLower()))
            End If
            If _filter.FML01_ID.HasValue Then
                lst = lst.Where(Function(f) f.FML01_ID = _filter.FML01_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.STARTDATE.HasValue Then
                lst = lst.Where(Function(f) f.STARTDATE >= _filter.STARTDATE)
            End If
            If _filter.ENDDATE.HasValue Then
                lst = lst.Where(Function(f) f.ENDDATE <= _filter.ENDDATE)
            End If
            If _filter.ID > 0 Then
                lst = lst.Where(Function(f) f.ID = _filter.ID)
            End If
            lst = lst.Where(Function(f) f.IS_DELETED = 0)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO)
        Try
            Dim query = From p In Context.AT_WORKSIGN
                        From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                        Where p.EMPLOYEE_ID = employee_Id And p.WORKINGDAY >= fromDate And p.WORKINGDAY <= toDate
                        Select New EMPLOYEE_SHIFT_DTO With {
                                                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                                .EFFECTIVEDATE = p.WORKINGDAY,
                                                                .HOURS_START = s.HOURS_START,
                                                                .HOURS_END = s.HOURS_STOP,
                                                                .ID_SIGN = s.ID,
                                                                .SIGN_CODE = s.CODE,
                                                                .SHIFT_ID = p.SHIFT_ID,
                                                                .SUBJECT = s.CODE,
                                                                .OT_HOUR_MAX = s.OT_HOUR_MAX,
                                                                .OT_HOUR_MIN = s.OT_HOUR_MIN,
                                                                .IS_TOMORROW = s.IS_TOMORROW,
                                                                .IS_HOURS_STOP = s.IS_HOURS_STOP
                                                            }
            'anhvn, TLSG-135 - xử lý đăng ký cho ngày không gán ca cho

            Dim holiday = (From p In Context.AT_HOLIDAY
                           Where p.WORKINGDAY >= fromDate And p.WORKINGDAY <= toDate And p.ACTFLG = ATConstant.ACTFLG_ACTIVE).Count()
            Dim Worksign = (From p In Context.AT_WORKSIGN
                            From s In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                            Where p.EMPLOYEE_ID = employee_Id And p.WORKINGDAY >= fromDate And p.WORKINGDAY <= toDate).Count
            If holiday <= 0 AndAlso Worksign <= 0 Then
                query = From s In Context.AT_SHIFT
                        Where s.CODE = "OFF"
                        Select New EMPLOYEE_SHIFT_DTO With {
                                                                .EMPLOYEE_ID = employee_Id,
                                                                .EFFECTIVEDATE = fromDate,
                                                                .HOURS_START = s.HOURS_START,
                                                                .HOURS_END = s.HOURS_STOP,
                                                                .ID_SIGN = s.ID,
                                                                .SIGN_CODE = s.CODE,
                                                                .SHIFT_ID = s.ID,
                                                                .SUBJECT = s.CODE,
                                                                .OT_HOUR_MAX = s.OT_HOUR_MAX,
                                                                .OT_HOUR_MIN = s.OT_HOUR_MIN,
                                                                .IS_TOMORROW = s.IS_TOMORROW,
                                                                .IS_HOURS_STOP = s.IS_HOURS_STOP
                                                            }
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region


    Public Function CHECK_OT_REGISTRATION_EXIT(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_HESO As String) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODE_NAME = P_EMP_CODE,
                                    .P_DATE = P_DATE,
                                    .P_HESO = P_HESO,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CHECK_OT_REGISTRATION_EXIT", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CHECK_LEAVE_EXITS(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_MANUAL_ID As Decimal, ByVal P_CA As Decimal) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODE_NAME = P_EMP_CODE,
                                    .P_DATE = P_DATE,
                                    .P_MANUAL_ID = P_MANUAL_ID,
                                    .P_CA = P_CA,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CHECK_LEAVE_EXITS", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CHECK_LEAVE_SHEET(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_CA As Decimal) As Decimal
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODE_NAME = P_EMP_CODE,
                                    .P_DATE = P_DATE,
                                    .P_CA = P_CA,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CHECK_LEAVE_SHEET", obj)

                If obj.P_OUT = ".5" Then
                    Return 0.5
                End If
                Return Decimal.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function INPORT_NB(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_PERIOD_ID As Integer) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INPORT_NB",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username,
                                           .P_PERIOD_ID = P_PERIOD_ID})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function INPORT_NB_PREV(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INPORT_NB_PREV",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username,
                                           .P_YEAR = P_YEAR})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

#Region "WAGE_OFFSET"
    Public Function GetWageOffset(ByVal _filter As AT_WAGEOFFSET_EMPDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_WAGEOFFSET_EMPDTO)
        Try
            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username.ToUpper,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            Dim query = From p In Context.AT_WAGEOFFSET_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_WAGEOFFSET_EMPDTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_ID = p.e.ORG_ID,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_DESC = p.org.DESCRIPTION_PATH,
                                        .TITLE_ID = p.e.TITLE_ID,
                                        .TITLE_NAME = p.title.NAME_VN,
                                        .YEAR = p.p.YEAR,
                                        .NOTE = p.p.NOTE,
                                        .WAGE_OFFSET = p.p.WAGE_OFFSET,
                                        .PERIOD_ID = p.p.PERIOD_ID,
                                        .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.p.YEAR.Value,
                                        .CREATED_DATE = p.p.CREATED_DATE
                                       })

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If IsNumeric(_filter.WAGE_OFFSET) Then
                lst = lst.Where(Function(f) f.WAGE_OFFSET = _filter.WAGE_OFFSET)
            End If

            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetWageOffsetById(ByVal _id As Decimal?) As AT_WAGEOFFSET_EMPDTO
        Try

            Dim query = From p In Context.AT_WAGEOFFSET_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_WAGEOFFSET_EMPDTO With {
                                          .ID = p.p.ID,
                                          .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                          .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                          .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                          .ORG_ID = p.e.ORG_ID,
                                          .ORG_NAME = p.org.NAME_VN,
                                          .TITLE_ID = p.e.TITLE_ID,
                                          .TITLE_NAME = p.title.NAME_VN,
                                          .WAGE_OFFSET = p.p.WAGE_OFFSET,
                                          .PERIOD_ID = p.p.PERIOD_ID,
                                          .PERIOD_NAME = p.period.PERIOD_NAME & "/" & p.p.YEAR.Value,
                                          .CREATED_DATE = p.p.CREATED_DATE
                                         }).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InSertWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWageOffsetData As New AT_WAGEOFFSET_EMP
        Try
            objWageOffsetData.ID = Utilities.GetNextSequence(Context, Context.AT_WAGEOFFSET_EMP.EntitySet.Name)
            objWageOffsetData.EMPLOYEE_ID = objWageOffset.EMPLOYEE_ID
            objWageOffsetData.WAGE_OFFSET = objWageOffset.WAGE_OFFSET
            objWageOffsetData.YEAR = objWageOffset.YEAR
            objWageOffsetData.PERIOD_ID = objWageOffset.PERIOD_ID
            objWageOffsetData.NOTE = objWageOffset.NOTE
            Context.AT_WAGEOFFSET_EMP.AddObject(objWageOffsetData)
            Context.SaveChanges(log)
            gID = objWageOffsetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateWageOffset(ByVal _validate As AT_WAGEOFFSET_EMPDTO)
        Dim query
        Try
            query = (From p In Context.AT_WAGEOFFSET_EMP Where p.ID <> _validate.ID _
                     And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.PERIOD_ID = _validate.PERIOD_ID).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWageOffsetData As New AT_WAGEOFFSET_EMP With {.ID = objWageOffset.ID}
        Try
            objWageOffsetData = (From p In Context.AT_WAGEOFFSET_EMP Where p.ID = objWageOffset.ID).FirstOrDefault
            objWageOffsetData.EMPLOYEE_ID = objWageOffset.EMPLOYEE_ID
            objWageOffsetData.WAGE_OFFSET = objWageOffset.WAGE_OFFSET
            objWageOffsetData.YEAR = objWageOffset.YEAR
            objWageOffsetData.PERIOD_ID = objWageOffset.PERIOD_ID
            objWageOffsetData.NOTE = objWageOffset.NOTE
            Context.SaveChanges(log)
            gID = objWageOffsetData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteWageOffset(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_WAGEOFFSET_EMP)
        Try
            lstl = (From p In Context.AT_WAGEOFFSET_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_WAGEOFFSET_EMP.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "KET PHEP"

    Public Function GetAtConcludeAnnaul(ByVal _filter As AT_CONCLUDE_ANNUAL_YEARDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_CONCLUDE_ANNUAL_YEARDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From en In Context.AT_CONCLUDEANNUAL_YEAR
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = en.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where en.YEAR_CONCLUDE = _filter.YEAR

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.E.WORK_STATUS IsNot Nothing)
            Else
                query = query.Where(Function(f) f.E.WORK_STATUS = 258)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.E.JOIN_DATE = _filter.ORG_NAME)
            End If

            If _filter.YEAR_CONCLUDE IsNot Nothing Then
                query = query.Where(Function(f) f.en.YEAR_CONCLUDE = _filter.YEAR_CONCLUDE)
            End If
            If _filter.YEAR_ANNUAL IsNot Nothing Then
                query = query.Where(Function(f) f.en.YEAR_ANNUAL = _filter.YEAR_ANNUAL)
            End If
            If _filter.ANNUAL_TRANFER IsNot Nothing Then
                query = query.Where(Function(f) f.en.ANNUAL_TRANFER = _filter.ANNUAL_TRANFER)
            End If

            Dim lst = query.Select(Function(p) New AT_CONCLUDE_ANNUAL_YEARDTO With {
                                       .ID = p.en.ID,
                                       .EMPLOYEE_ID = p.en.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .JOIN_DATE = p.E.SENIORITY_DATE,
                                       .YEAR_ANNUAL = p.en.YEAR_ANNUAL,
                                       .YEAR_CONCLUDE = p.en.YEAR_CONCLUDE,
                                       .ANNUAL_TRANFER = p.en.ANNUAL_TRANFER,
                                       .CREATED_BY = p.en.CREATED_BY,
                                       .CREATED_DATE = p.en.CREATED_DATE,
                                       .CREATED_LOG = p.en.CREATED_LOG,
                                       .MODIFIED_BY = p.en.MODIFIED_BY,
                                       .MODIFIED_DATE = p.en.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.en.MODIFIED_LOG})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CAL_CONCLUDE_ANNUAL_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CAL_CONCLUDE_ANNUAL_YEAR",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORG_ID = _param.ORG_ID,
                                           .P_YEAR = P_YEAR,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_AT_CONCLUDE_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Dim cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.IMPORT_AT_CONCLUDE_YEAR",
                                                New With {.P_DOCXML = P_DOCXML,
                                                          .P_USERNAME = log.Username,
                                                          .P_OUT = cls.OUT_CURSOR})
            Return dtData.Rows(0)(0)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function DeleteConcludeYear(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.AT_CONCLUDEANNUAL_YEAR Where _lstID.Contains(p.ID))
            For Each item In lstObj
                Context.AT_CONCLUDEANNUAL_YEAR.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex

        End Try
    End Function

    Public Function UPDATE_CONCLUDE_ANNUAL_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_ANNUAL As Decimal, ByVal P_ANNUAL_TRANFER As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.UPDATE_CONCLUDE_ANNUAL_YEAR",
                                 New With {.P_ID = P_ID,
                                           .P_YEAR_ANNUAL = P_YEAR_ANNUAL,
                                           .P_ANNUAL_TRANFER = P_ANNUAL_TRANFER,
                                           .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function UPDATE_COMPENSATION_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_NB As Decimal, ByVal P_NB_TRANFER As Decimal, ByVal P_NB_EDIT As Decimal, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.UPDATE_COMPENSATION_YEAR",
                                 New With {.P_ID = P_ID,
                                           .P_YEAR_NB = P_YEAR_NB,
                                           .P_NB_TRANFER = P_NB_TRANFER,
                                           .P_NB_EDIT = P_NB_EDIT,
                                           .P_USERNAME = log.Username.ToUpper})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function



    Public Function GetAtCompensation(ByVal _filter As AT_COMPENSATION_YEARDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATION_YEARDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From en In Context.AT_COMPENSATION_YEAR
                        From E In Context.HU_EMPLOYEE.Where(Function(f) f.ID = en.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = E.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = E.TITLE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) E.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where en.YEAR = _filter.YEAR

            If _filter.IS_TERMINATE Then
                query = query.Where(Function(f) f.E.WORK_STATUS IsNot Nothing)
            Else
                query = query.Where(Function(f) f.E.WORK_STATUS = 258)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.E.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FULLNAME_VN) Then
                query = query.Where(Function(f) f.E.FULLNAME_VN.ToLower().Contains(_filter.FULLNAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.t.NAME_VN.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.o.NAME_VN.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.E.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.YEAR_NB IsNot Nothing Then
                query = query.Where(Function(f) f.en.YEAR_NB = _filter.YEAR_NB)
            End If
            If _filter.NB_TRANFER IsNot Nothing Then
                query = query.Where(Function(f) f.en.NB_TRANFER = _filter.NB_TRANFER)
            End If
            If _filter.NB_EDIT IsNot Nothing Then
                query = query.Where(Function(f) f.en.NB_EDIT = _filter.NB_EDIT)
            End If

            Dim lst = query.Select(Function(p) New AT_COMPENSATION_YEARDTO With {
                                       .ID = p.en.ID,
                                       .EMPLOYEE_ID = p.en.EMPLOYEE_ID,
                                       .FULLNAME_VN = p.E.FULLNAME_VN,
                                       .EMPLOYEE_CODE = p.E.EMPLOYEE_CODE,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .JOIN_DATE = p.E.SENIORITY_DATE,
                                       .YEAR = p.en.YEAR,
                                       .YEAR_NB = p.en.YEAR_NB,
                                       .NB_TRANFER = p.en.NB_TRANFER,
                                       .NB_EDIT = p.en.NB_EDIT,
                                       .CREATED_BY = p.en.CREATED_BY,
                                       .CREATED_DATE = p.en.CREATED_DATE,
                                       .CREATED_LOG = p.en.CREATED_LOG,
                                       .MODIFIED_BY = p.en.MODIFIED_BY,
                                       .MODIFIED_DATE = p.en.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.en.MODIFIED_LOG})

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function CAL_COMPENSATION_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CAL_COMPENSATION_YEAR",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORG_ID = _param.ORG_ID,
                                           .P_YEAR = P_YEAR,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_AT_COMPENSATION_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Dim cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_LIST.IMPORT_AT_COMPENSATION_YEAR",
                                                New With {.P_DOCXML = P_DOCXML,
                                                          .P_USERNAME = log.Username,
                                                          .P_OUT = cls.OUT_CURSOR})
            Return dtData.Rows(0)(0)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function DeleteCompensationYear(ByVal _lstID As List(Of Decimal)) As Boolean
        Try
            Dim lstObj = (From p In Context.AT_COMPENSATION_YEAR Where _lstID.Contains(p.ID))
            For Each item In lstObj
                Context.AT_COMPENSATION_YEAR.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex

        End Try
    End Function

#End Region

#Region "Assign Emp to Calendar"

    'GetEmployeeNotByCalendarID
    Public Function GetEmployeeNotByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "EMPLOYEE_CODE",
                                             Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim fromDate As New Date?
            Dim toDate As New Date?
            If IsNumeric(_filter.YEAR) Then
                fromDate = New DateTime(_filter.YEAR, 1, 1)
                toDate = New DateTime(_filter.YEAR, 12, 31)
            End If

            Dim query = From e In Context.HU_EMPLOYEE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where (Not (From P In Context.AT_ASSIGNEMP_CALENDAR
                                    Where P.YEAR = _filter.YEAR
                                    Select P.EMPLOYEE_ID).Contains(e.ID) _
                        And Not (From s In Context.AT_SIGNDEFAULT
                                 Where (s.ACTFLG = "A" And ((s.EFFECT_DATE_FROM >= fromDate And s.EFFECT_DATE_FROM <= toDate) Or (s.EFFECT_DATE_TO >= fromDate And s.EFFECT_DATE_TO <= toDate)))
                                 Select s.EMPLOYEE_ID).Contains(e.ID))
                        Select New AT_ASSIGNEMP_CALENDARDTO With {.EMPLOYEE_ID = e.ID,
                                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                  .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                  .ORG_ID = e.ORG_ID,
                                                                  .ORG_NAME = org.NAME_VN,
                                                                  .ORG_DESC = org.HIERARCHICAL_PATH,
                                                                  .TITLE_NAME = title.NAME_VN,
                                                                  .TITLE_ID = title.ID,
                                                                  .JOIN_DATE = e.JOIN_DATE,
                                                                  .WORK_STATUS = e.WORK_STATUS,
                                                                  .TER_LAST_DATE = e.TER_LAST_DATE}

            Dim lst = query
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If IsNumeric(_filter.TITLE_ID) Then
                lst = lst.Where(Function(f) f.TITLE_ID = _filter.TITLE_ID)
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.YEAR) Then
                lst = lst.Where(Function(f) f.JOIN_DATE <= toDate And (f.TER_LAST_DATE Is Nothing Or (f.TER_LAST_DATE IsNot Nothing AndAlso f.TER_LAST_DATE > fromDate)))
            End If
            Dim LSTEMP As List(Of HU_WORKING)
            If IsNumeric(_filter.CALENDAR_ID) Then
                Dim calendarDt = (From p In Context.AT_SIGNDEFAULT_ORG Where p.ID = _filter.CALENDAR_ID)
                If calendarDt IsNot Nothing And calendarDt.FirstOrDefault.OBJ_ATTENDANT_ID IsNot Nothing Then
                    LSTEMP = (From s In Context.HU_WORKING.Where(Function(f) f.STATUS_ID = 447 And f.EFFECT_DATE < toDate And f.IS_MISSION = -1 And f.OBJECT_ATTENDANT_ID = calendarDt.FirstOrDefault.OBJ_ATTENDANT_ID)
                              Group By s = s.ID Into cc = Group
                              Select cc.OrderByDescending(Function(f) f.EFFECT_DATE).OrderByDescending(Function(f) f.ID).FirstOrDefault).ToList
                End If
                If LSTEMP IsNot Nothing Then
                    Dim Employees = (From p In LSTEMP Select p.EMPLOYEE_ID).ToList
                    lst = lst.Where(Function(f) Employees.Contains(f.EMPLOYEE_ID))
                End If
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    'GetEmployeebByCalendarID
    Public Function GetEmployeebByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using
            Dim fromDate As New Date?
            Dim toDate As New Date?
            Dim query = From e In Context.HU_EMPLOYEE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID _
                                                                  And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From p In Context.AT_ASSIGNEMP_CALENDAR.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From c In Context.AT_SIGNDEFAULT_ORG.Where(Function(f) f.ID = p.CALENDAR_ID)
                        Select New AT_ASSIGNEMP_CALENDARDTO With {.ID = p.ID,
                                                                  .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                  .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                  .ORG_ID = e.ORG_ID,
                                                                  .ORG_NAME = org.NAME_VN,
                                                                  .ORG_DESC = org.HIERARCHICAL_PATH,
                                                                  .TITLE_NAME = title.NAME_VN,
                                                                  .TITLE_ID = title.ID,
                                                                  .YEAR = p.YEAR,
                                                                  .JOIN_DATE = e.JOIN_DATE,
                                                                  .CALENDAR_ID = p.CALENDAR_ID,
                                                                  .CALENDAR_NAME = c.CALENDAR}

            Dim lst = query
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If IsNumeric(_filter.TITLE_ID) Then
                lst = lst.Where(Function(f) f.TITLE_ID = _filter.TITLE_ID)
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.YEAR) Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
                fromDate = New DateTime(_filter.YEAR, 1, 1)
                toDate = New DateTime(_filter.YEAR, 12, 31)
            End If
            If IsNumeric(_filter.CALENDAR_ID) Then
                lst = lst.Where(Function(f) f.CALENDAR_ID = _filter.CALENDAR_ID)

                Dim LSTEMP As List(Of HU_WORKING)
                Dim calendarDt = (From p In Context.AT_SIGNDEFAULT_ORG Where p.ID = _filter.CALENDAR_ID)
                If calendarDt IsNot Nothing And calendarDt.FirstOrDefault.OBJ_ATTENDANT_ID IsNot Nothing Then
                    LSTEMP = (From s In Context.HU_WORKING.Where(Function(f) f.STATUS_ID = 447 And f.EFFECT_DATE < toDate And f.IS_MISSION = -1 And f.OBJECT_ATTENDANT_ID = calendarDt.FirstOrDefault.OBJ_ATTENDANT_ID)
                              Group By s = s.ID Into cc = Group
                              Select cc.OrderByDescending(Function(f) f.EFFECT_DATE).OrderByDescending(Function(f) f.ID).FirstOrDefault).ToList
                End If
                If LSTEMP IsNot Nothing Then
                    Dim Employees = (From p In LSTEMP Select p.EMPLOYEE_ID).ToList
                    lst = lst.Where(Function(f) Employees.Contains(f.EMPLOYEE_ID))
                End If
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    Public Function InsertEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                               ByVal log As UserLog) As Decimal

        Try
            Dim rs
            'THÊM MỚI VÀ TỰ SINH CA LÀM VIỆC SANG AT_WORKSIGN THEO MÀN HÌNH THIẾT LẬP CA CHO CƠ CẤU
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username.Trim.ToUpper,
                                    .P_ORG_ID = _filter.ORG_ID,
                                    .P_ISDISSOLVE = _filter.IS_DISSOLVE,
                                    .P_EMPLOYEE_ID = _filter.lstEmp,
                                    .P_YEAR = _filter.YEAR,
                                    .P_CALENDAR_ID = _filter.CALENDAR_ID,
                                    .P_OUT = cls.OUT_NUMBER}

                Dim query = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.INSERT_GEN_ATWORKSIGN", obj)
                rs = Integer.Parse(obj.P_OUT)
            End Using

            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
        End Try

    End Function

    Public Function DeleteEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                               ByVal log As UserLog) As Decimal
        Try
            Dim rs
            'THÊM MỚI VÀ TỰ SINH CA LÀM VIỆC SANG AT_WORKSIGN THEO MÀN HÌNH THIẾT LẬP CA CHO CƠ CẤU
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = _filter.lstEmp,
                                    .P_YEAR = _filter.YEAR,
                                    .P_OUT = cls.OUT_NUMBER}

                Dim query = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_GEN_ATWORKSIGN", obj)
                rs = Integer.Parse(obj.P_OUT)
            End Using

            Return rs
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex

        End Try
    End Function

#End Region
    Public Function CREATE_WORKNIGHT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CREATE_WORKNIGHT",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_OBJ_EMP_ID = param.EMP_OBJ,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_FROMDATE = param.FROMDATE,
                                                         .P_ENDATE = param.ENDDATE,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function
    Public Function GET_WORKNIGHT(ByVal param As AT_WORKNIGHTDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GET_WORKNIGHT",
                                              New With {.P_USERNAME = log.Username.ToUpper,
                                                        .P_ORG_ID = param.ORG_ID,
                                                        .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                        .P_PAGE_INDEX = param.PAGE_INDEX,
                                                        .P_EMPLOYEE_CODE = param.EMPLOYEE_CODE,
                                                        .P_PAGE_SIZE = param.PAGE_SIZE,
                                                        .P_PERIOD_ID = param.PERIOD_ID,
                                                        .P_EMP_OBJ = param.EMP_OBJ,
                                                        .P_START_DATE = param.START_DATE,
                                                        .P_END_DATE = param.END_DATE,
                                                        .P_CUR = cls.OUT_CURSOR,
                                                        .P_CURCOUNT = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Function InsertWorkNightByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean
        Try
            Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_id).FirstOrDefault

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Using resource As New DataAccess.OracleCommon()
                            Try
                                conn.Open()
                                cmd.Connection = conn
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.INSERT_WORKNIGHT"

                                For Each row As DataRow In dtData.Rows
                                    cmd.Parameters.Clear()
                                    Dim objParam = New With {.P_EMPLOYEEID = row("EMPLOYEE_ID").ToString,
                                                             .P_PERIODId = period_id,
                                                             .P_USERNAME = log.Username.ToUpper + "-" + log.Ip + log.ComputerName,
                                                             .P_D1 = Utilities.Obj2Decima(row("D1"), Nothing),
                                                             .P_D2 = Utilities.Obj2Decima(row("D2"), Nothing),
                                                             .P_D3 = Utilities.Obj2Decima(row("D3"), Nothing),
                                                             .P_D4 = Utilities.Obj2Decima(row("D4"), Nothing),
                                                             .P_D5 = Utilities.Obj2Decima(row("D5"), Nothing),
                                                             .P_D6 = Utilities.Obj2Decima(row("D6"), Nothing),
                                                             .P_D7 = Utilities.Obj2Decima(row("D7"), Nothing),
                                                             .P_D8 = Utilities.Obj2Decima(row("D8"), Nothing),
                                                             .P_D9 = Utilities.Obj2Decima(row("D9"), Nothing),
                                                             .P_D10 = Utilities.Obj2Decima(row("D10"), Nothing),
                                                             .P_D11 = Utilities.Obj2Decima(row("D11"), Nothing),
                                                             .P_D12 = Utilities.Obj2Decima(row("D12"), Nothing),
                                                             .P_D13 = Utilities.Obj2Decima(row("D13"), Nothing),
                                                             .P_D14 = Utilities.Obj2Decima(row("D14"), Nothing),
                                                             .P_D15 = Utilities.Obj2Decima(row("D15"), Nothing),
                                                             .P_D16 = Utilities.Obj2Decima(row("D16"), Nothing),
                                                             .P_D17 = Utilities.Obj2Decima(row("D17"), Nothing),
                                                             .P_D18 = Utilities.Obj2Decima(row("D18"), Nothing),
                                                             .P_D19 = Utilities.Obj2Decima(row("D19"), Nothing),
                                                             .P_D20 = Utilities.Obj2Decima(row("D20"), Nothing),
                                                             .P_D21 = Utilities.Obj2Decima(row("D21"), Nothing),
                                                             .P_D22 = Utilities.Obj2Decima(row("D22"), Nothing),
                                                             .P_D23 = Utilities.Obj2Decima(row("D23"), Nothing),
                                                             .P_D24 = Utilities.Obj2Decima(row("D24"), Nothing),
                                                             .P_D25 = Utilities.Obj2Decima(row("D25"), Nothing),
                                                             .P_D26 = Utilities.Obj2Decima(row("D26"), Nothing),
                                                             .P_D27 = Utilities.Obj2Decima(row("D27"), Nothing),
                                                             .P_D28 = Utilities.Obj2Decima(row("D28"), Nothing),
                                                             .P_D29 = Utilities.Obj2Decima(row("D29"), Nothing),
                                                             .P_D30 = Utilities.Obj2Decima(row("D30"), Nothing),
                                                             .P_D31 = Utilities.Obj2Decima(row("D31"), Nothing)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                Next

                                cmd.CommandText = "PKG_ATTENDANCE_BUSINESS.IMPORT_WORKNIGHT"
                                cmd.Parameters.Clear()

                                Dim objParam1 = New With {.P_STARTDATE = start_date,
                                                         .P_ENDDATE = end_date,
                                                         .P_USERNAME = log.Username.ToUpper}


                                If objParam1 IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()

                                cmd.Transaction.Commit()
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
                                Throw ex
                            Finally
                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#Region "Khóa bảng công nhân viên - AT_TIMESHEET_LOCK"
    Public Function GetAtTimesheetLock(ByVal _filter As AT_TIMESHEET_LOCKDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIMESHEET_LOCKDTO)
        Try

            Dim query = From p In Context.AT_TIMESHEET_LOCK
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From orgtl In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
            'From period In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_TIMESHEET_LOCKDTO With {
                                        .ID = p.p.ID,
                                        .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                        .ORG_ID = p.e.ORG_ID,
                                        .ORG_NAME = p.org.NAME_VN,
                                        .ORG_DESC = p.org.DESCRIPTION_PATH,
                                        .TITLE_ID = p.e.TITLE_ID,
                                        .TITLE_NAME = p.title.NAME_VN,
                                        .REMARK = p.p.REMARK,
                                        .IS_DMVS = p.p.IS_DMVS,
                                        .IS_LEAVE = p.p.IS_LEAVE,
                                        .FROM_DATE = p.p.FROM_DATE,
                                        .TO_DATE = p.p.TO_DATE,
                                        .ORG_NAME_TIMESHEET = p.orgtl.NAME_VN,
                                        .ORG_DESC_TIMESHEET = p.orgtl.DESCRIPTION_PATH,
                                        .IS_OVERTIME = p.p.IS_OVERTIME,
                                        .CREATED_DATE = p.p.CREATED_DATE
                                       })

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If



            If Not String.IsNullOrEmpty(_filter.PERIOD_NAME) Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToLower().Contains(_filter.PERIOD_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetAtTimesheetLockById(ByVal _id As Decimal?) As AT_TIMESHEET_LOCKDTO
        Try

            Dim query = From p In Context.AT_TIMESHEET_LOCK
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New AT_TIMESHEET_LOCKDTO With {
                                          .ID = p.p.ID,
                                          .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                          .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                          .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                          .ORG_ID = p.e.ORG_ID,
                                          .ORG_NAME = p.org.NAME_VN,
                                          .TITLE_ID = p.e.TITLE_ID,
                                          .TITLE_NAME = p.title.NAME_VN,
                                          .CREATED_DATE = p.p.CREATED_DATE
                                         }).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InSertAtTimesheetLock(ByVal obj As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_TIMESHEET_LOCK
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_TIMESHEET_LOCK.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.ORG_ID = obj.ORG_ID
            objData.FROM_DATE = obj.FROM_DATE
            objData.TO_DATE = obj.TO_DATE
            objData.IS_LEAVE = obj.IS_LEAVE
            objData.IS_OVERTIME = obj.IS_OVERTIME
            objData.IS_DMVS = obj.IS_DMVS
            objData.REMARK = obj.REMARK

            Context.AT_TIMESHEET_LOCK.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateAtTimesheetLock(ByVal _validate As AT_TIMESHEET_LOCKDTO)
        Dim query
        Try
            'query = (From p In Context.AT_TIMESHEET_LOCK Where p.ID <> _validate.ID _
            '         And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.PERIOD_ID = _validate.PERIOD_ID).Any
            query = Nothing
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAtTimesheetLock(ByVal obj As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_TIMESHEET_LOCK With {.ID = obj.ID}
        Try
            objData = (From p In Context.AT_TIMESHEET_LOCK Where p.ID = obj.ID).FirstOrDefault
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.ORG_ID = obj.ORG_ID
            objData.FROM_DATE = obj.FROM_DATE
            objData.TO_DATE = obj.TO_DATE
            objData.IS_LEAVE = obj.IS_LEAVE
            objData.IS_OVERTIME = obj.IS_OVERTIME
            objData.IS_DMVS = obj.IS_DMVS
            objData.REMARK = obj.REMARK
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteAtTimesheetLock(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_TIMESHEET_LOCK)
        Try
            lstl = (From p In Context.AT_TIMESHEET_LOCK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_TIMESHEET_LOCK.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Leave Payment"
    Public Function GetLeavePayments(ByVal _filter As AT_LEAVE_PAYMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVE_PAYMENTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_LEAVE_PAYMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From se In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_LEAVE_PAYMENTDTO With {
                                        .ID = p.ID,
                                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = e.FULLNAME_VN,
                                        .ORG_ID = e.ORG_ID,
                                        .ORG_NAME = org.NAME_VN,
                                        .ORG_DESC = org.DESCRIPTION_PATH,
                                        .TITLE_ID = e.TITLE_ID,
                                        .TITLE_NAME = title.NAME_VN,
                                        .REMARK = p.REMARK,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .YEAR = p.YEAR,
                                        .LEAVE_OLD = p.LEAVE_OLD,
                                        .LEAVE_NEW = p.LEAVE_NEW,
                                        .SALARY_AVERAGE = p.SALARY_AVERAGE,
                                        .SALARY_NEW = p.SALARY_NEW,
                                        .SALARY_PAYMENT = p.SALARY_PAYMENT,
                                        .PERIOD_T = p.PERIOD_T,
                                        .CREATED_DATE = p.CREATED_DATE}

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.PERIOD_T) Then
                query = query.Where(Function(f) f.PERIOD_T.ToLower().Contains(_filter.PERIOD_T.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
            End If


            If _filter.EFFECT_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.EFFECT_DATE <= _filter.TO_DATE)
            End If
            If _filter.YEAR.HasValue Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.LEAVE_OLD.HasValue Then
                query = query.Where(Function(f) f.LEAVE_OLD = _filter.LEAVE_OLD)
            End If
            If _filter.LEAVE_NEW.HasValue Then
                query = query.Where(Function(f) f.LEAVE_NEW = _filter.LEAVE_NEW)
            End If
            If _filter.SALARY_AVERAGE.HasValue Then
                query = query.Where(Function(f) f.SALARY_AVERAGE = _filter.SALARY_AVERAGE)
            End If
            If _filter.SALARY_NEW.HasValue Then
                query = query.Where(Function(f) f.SALARY_NEW = _filter.SALARY_NEW)
            End If
            If _filter.SALARY_PAYMENT.HasValue Then
                query = query.Where(Function(f) f.SALARY_PAYMENT = _filter.SALARY_PAYMENT)
            End If

            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeavePaymentById(ByVal _id As Decimal?) As AT_LEAVE_PAYMENTDTO
        Try

            Dim query = From p In Context.AT_LEAVE_PAYMENT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        Select New AT_LEAVE_PAYMENTDTO With {
                                        .ID = p.ID,
                                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = e.FULLNAME_VN,
                                        .ORG_ID = e.ORG_ID,
                                        .ORG_NAME = org.NAME_VN,
                                        .TITLE_ID = e.TITLE_ID,
                                        .TITLE_NAME = title.NAME_VN,
                                        .REMARK = p.REMARK,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .YEAR = p.YEAR,
                                        .LEAVE_OLD = p.LEAVE_OLD,
                                        .LEAVE_NEW = p.LEAVE_NEW,
                                        .SALARY_AVERAGE = p.SALARY_AVERAGE,
                                        .SALARY_NEW = p.SALARY_NEW,
                                        .SALARY_PAYMENT = p.SALARY_PAYMENT,
                                        .PERIOD_T = p.PERIOD_T,
                                        .CREATED_DATE = p.CREATED_DATE}
            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InSertLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean
        Dim objData As New AT_LEAVE_PAYMENT
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVE_PAYMENT.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.YEAR = obj.YEAR
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.LEAVE_OLD = obj.LEAVE_OLD
            objData.LEAVE_NEW = obj.LEAVE_NEW
            objData.SALARY_AVERAGE = obj.SALARY_AVERAGE
            objData.SALARY_NEW = obj.SALARY_NEW
            objData.SALARY_PAYMENT = obj.SALARY_PAYMENT
            objData.PERIOD_T = obj.PERIOD_T
            objData.REMARK = obj.REMARK

            Context.AT_LEAVE_PAYMENT.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean
        Dim objData As New AT_LEAVE_PAYMENT With {.ID = obj.ID}
        Try
            objData = (From p In Context.AT_LEAVE_PAYMENT Where p.ID = obj.ID).FirstOrDefault
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.YEAR = obj.YEAR
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.LEAVE_OLD = obj.LEAVE_OLD
            objData.LEAVE_NEW = obj.LEAVE_NEW
            objData.SALARY_AVERAGE = obj.SALARY_AVERAGE
            objData.SALARY_NEW = obj.SALARY_NEW
            objData.SALARY_PAYMENT = obj.SALARY_PAYMENT
            objData.PERIOD_T = obj.PERIOD_T
            objData.REMARK = obj.REMARK
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateLeavePayment(ByVal _validate As AT_LEAVE_PAYMENTDTO) As Boolean
        Dim query
        Try
            query = (From p In Context.AT_LEAVE_PAYMENT Where p.ID <> _validate.ID AndAlso p.EMPLOYEE_ID = _validate.EMPLOYEE_ID AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE)).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteLeavePayment(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of AT_LEAVE_PAYMENT)
        Try
            lstl = (From p In Context.AT_LEAVE_PAYMENT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.AT_LEAVE_PAYMENT.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeavePaymentSal(ByVal empID As Decimal, ByVal effect_date As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_LEAVESHEET.GET_LEAVE_PAYMENT_SAL",
                                 New With {.P_EMPLOYEE_ID = empID,
                                           .P_EFFECT_DATE = effect_date,
                                           .P_OUT = cls.OUT_CURSOR}, False)

                If dsData IsNot Nothing AndAlso dsData.Tables.Count > 0 Then
                    Return dsData.Tables(0)
                End If
            End Using
            Return New DataTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Timesheet"
    Public Function GetTimesheetDetail_portal(ByVal empID As Decimal, ByVal year As Decimal, ByVal month As Decimal) As DataTable

        Try
            'Dim userid = (From p In Context.SE_USER Where p.EMPLOYEE_ID = empID Select p.ID).FirstOrDefault

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_API_MOBILE.API_GET_TIMESHEET_INFO",
                                           New With {.P_USERID = empID,
                                           .P_LANGUAGE = "vi-VN",
                                           .P_YEAR = year,
                                           .P_MONTH = month,
                                           .P_TOTAL = cls.OUT_NUMBER,
                                           .P_TT = cls.OUT_NUMBER,
                                           .P_LEAVESAL = cls.OUT_NUMBER,
                                           .P_LEAVENOTSAL = cls.OUT_NUMBER,
                                           .P_CTL = cls.OUT_NUMBER,
                                           .P_DMVS = cls.OUT_NUMBER,
                                           .P_CUR = cls.OUT_CURSOR,
                                           .P_MESSAGE = cls.OUT_STRING,
                                           .P_RESPONSESTATUS = cls.OUT_NUMBER})


                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetTimesheetDetail(ByVal PeriodId As Decimal, ByVal EmployeeId As Decimal, ByVal log As UserLog) As DataTable

        Try

            Using cls As New DataAccess.QueryData
                '    Dim dtData As DataTable = cls.ExecuteStore("PKG_API_MOBILE.API_GET_TIMESHEET_INFO",
                '                               New With {.P_USERID = 3026,
                '                               .P_LANGUAGE = "vi-VN",
                '                               .P_YEAR = 2022,
                '                               .P_MONTH = 12,
                '                               .P_TOTAL = cls.OUT_NUMBER,
                '                               .P_TT = cls.OUT_NUMBER,
                '                               .P_LEAVESAL = cls.OUT_NUMBER,
                '                               .P_LEAVENOTSAL = cls.OUT_NUMBER,
                '                               .P_CTL = cls.OUT_NUMBER,
                '                               .P_DMVS = cls.OUT_NUMBER,
                '                               .P_CUR = cls.OUT_CURSOR,
                '                               .P_MESSAGE = cls.OUT_STRING,
                '                               .P_RESPONSESTATUS = cls.OUT_NUMBER})

                Dim dtData As DataTable = cls.ExecuteStore("PKG_API_MOBILE.API_GET_ATMONTHLY_MOBILE",
                                       New With {.P_LANGUAGE = "en-US",
                                       .P_USERID = EmployeeId,
                                       .P_PERIOD_ID = PeriodId,
                                       .P_CUR = cls.OUT_CURSOR,
                                       .P_MESSAGE = cls.OUT_STRING,
                                       .P_RESPONSESTATUS = cls.OUT_NUMBER})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Khác"
    Public Function CheckPeriodIsLock(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_EMPLOYEE_ID = P_EMPLOYEE_ID,
                                    .P_START_DATE = P_START_DATE,
                                    .P_END_DATE = P_END_DATE,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CHECK_PERIOD_IS_LOCK", obj)
                If Decimal.Parse(obj.P_OUT) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return True
        End Try
    End Function

    Public Function CheckValidateApprove(ByVal EMPLOYEE_ID As Decimal, ByVal PROCESS_TYPE As String, ByVal ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_EMPLOYEE_ID = EMPLOYEE_ID,
                                .P_PROCESS_TYPE = PROCESS_TYPE,
                                .P_ID = ID,
                                .P_STARTDATE = P_START_DATE,
                                .P_ENDDATE = P_END_DATE,
                                .P_MESSAGE = cls.OUT_STRING,
                                .P_RESPONSESTATUS = cls.OUT_NUMBER}

                cls.ExecuteStore("PKG_API_MOBILE.API_CHECK_VALIDATE_APPROVE", obj)

                Dim param = New VALIDATE_DTO
                param.MESSAGE = obj.P_MESSAGE
                param.RESPONSESTATUS = obj.P_RESPONSESTATUS
                Return param
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return Nothing
        End Try
    End Function
#End Region

    Public Function IMPORT_AT_TOXICLEAVE_EMP(ByVal P_XML As String, ByVal P_USERNAME As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_XML = P_XML,
                                    .P_USERNAME = P_USERNAME,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.IMPORT_AT_TOXICLEAVE_EMP", obj)
                If Integer.Parse(obj.P_OUT) = 1 Then
                    Return True
                End If
                Return False
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
End Class