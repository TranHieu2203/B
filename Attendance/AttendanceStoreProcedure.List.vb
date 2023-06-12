Imports Attendance.AttendanceBusiness
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class AttendanceStoreProcedure
    Private rep As New HistaffFrameworkRepository

    ' Xóa đăng ký nghỉ
    Public Function AT_DELETE_PORTAL_REG(ByVal LSTID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_ATTENDANCE_PORTAL.AT_DELETE_PORTAL_REG", _
                                                   New List(Of Object)(New Object() {LSTID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function SE_GETTEMPLATE_APP(ByVal empID As Decimal, ByVal frDateReg As Date, ByVal toDateReg As Date, ByVal kindReg As String, ByVal orgEmp As Decimal, Optional ByVal dayNumReg As Decimal = 0, Optional ByVal signID As Decimal = 0) As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.SE_GETTEMPLATE_APP", New List(Of Object)(New Object() {empID, frDateReg, toDateReg, kindReg, orgEmp, dayNumReg, signID}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Lay danh sach dang ky tren portal
    Public Function GET_REG_PORTAL(ByVal EMPLOYEEID As Decimal?, ByVal START_DATE As Date?, ByVal END_DATE As Date?, ByVal LSTSTATUS As String, ByVal TYPE As String) As List(Of APPOINTMENT_DTO)
        Dim dt As New DataTable
        Dim lst As New List(Of APPOINTMENT_DTO)
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_REG_PORTAL", New List(Of Object)(New Object() {EMPLOYEEID, START_DATE, END_DATE, LSTSTATUS, TYPE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim itm As New APPOINTMENT_DTO
                itm.ID = dr("ID")
                itm.EMPLOYEEID = dr("EMPLOYEE_ID")
                itm.SIGNID = dr("SIGN_ID").ToString
                itm.SIGNCODE = dr("SIGN_CODE").ToString
                itm.SIGNNAME = dr("SIGN_NAME").ToString
                itm.GSIGNCODE = dr("GSIGN_CODE").ToString
                itm.SUBJECT = dr("SUBJECT").ToString
                If dr("FROM_HOUR").ToString <> "" Then
                    itm.FROMHOUR = dr("FROM_HOUR").ToString
                End If
                If dr("TO_HOUR").ToString <> "" Then
                    itm.TOHOUR = dr("TO_HOUR").ToString
                End If
                If dr("WORKING_DAY").ToString <> "" Then
                    itm.WORKINGDAY = dr("WORKING_DAY").ToString
                End If
                itm.NVALUE = dr("NVALUE").ToString
                itm.NVALUE_NAME = dr("NVALUE_NAME").ToString
                itm.STATUS = dr("STATUS").ToString
                itm.NOTE = dr("NOTE").ToString
                If dr("JOIN_DATE").ToString <> "" Then
                    itm.JOINDATE = Convert.ToDateTime(dr("JOIN_DATE"))
                End If

                lst.Add(itm)
            Next
        End If
        Return lst
    End Function
    Public Function CHECK_VALIDATE(ByVal type As Decimal, ByVal code As String, ByVal todate As DateTime, ByVal fromdate As DateTime) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_LIST.CHECK_VALIDATE", New List(Of Object)(New Object() {type, code, todate, fromdate, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    ''' <summary>
    ''' Ktra Dong/Mo bang cong
    ''' </summary>
    ''' <param name="P_ORG_ID"></param>
    ''' <param name="P_PERIOD_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IS_PERIODSTATUS(ByVal P_ORG_ID As String, ByVal P_PERIOD_ID As Decimal?) As Boolean
        Dim rs As Boolean = False
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_LIST.IS_PERIODSTATUS", New List(Of Object)(New Object() {P_ORG_ID, P_PERIOD_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                rs = True
            Else
                rs = False
            End If
        Else
            rs = False
        End If
        Return rs
    End Function
    Public Function Lock_OT(ByVal P_ID As String, ByVal P_PERIOD_ID As Decimal, ByVal P_STATUS As Decimal) As Boolean

        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_LIST.Lock_OT", New List(Of Object)(New Object() {P_ID, P_PERIOD_ID, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            If ds.Tables(0).Rows(0)("RE") = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function Delete_OT(ByVal P_ID As String) As Boolean

        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_LIST.DELETE_TIMESHEET_OT", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            If ds.Tables(0).Rows(0)("RE") = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function PRINT_DONPHEP(ByVal P_ID As Decimal?, ByVal P_EMPLOYEE_ID As Decimal?, ByVal P_DATE_TIME As Date?) As DataTable
        Dim dt As DataTable
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_BUSINESS.PRINT_DONPHEP", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_ID, P_DATE_TIME}))
            If ds IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function GET_LEAVE_SHEET_FOR_PORTAL(ByVal _filter As AT_LEAVESHEETDTO, ByRef Total As Integer,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer) As DataTable
        Dim dt As New DataTable
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_LEAVESHEET.GET_LEAVE_SHEET_FOR_PORTAL", New List(Of Object)(New Object() {_filter.EMPLOYEE_ID, _filter.FROM_DATE, _filter.END_DATE, _filter.STATUS}))
            If ds IsNot Nothing Then
                dt = ds.Tables(0)

                If _filter.STATUS_NAME <> "" Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("STATUS_NAME").ToString.ToLower.Contains(_filter.STATUS_NAME.ToLower)).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("STATUS_NAME").ToString.ToLower.Contains(_filter.STATUS_NAME.ToLower)).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.EMPLOYEE_CODE <> "" Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("EMPLOYEE_CODE").ToString.ToLower.Contains(_filter.EMPLOYEE_CODE.ToLower)).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("EMPLOYEE_CODE").ToString.ToLower.Contains(_filter.EMPLOYEE_CODE.ToLower)).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.VN_FULLNAME <> "" Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("VN_FULLNAME").ToString.ToLower.Contains(_filter.VN_FULLNAME.ToLower)).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("VN_FULLNAME").ToString.ToLower.Contains(_filter.VN_FULLNAME.ToLower)).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.ORG_NAME <> "" Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("ORG_NAME").ToString.ToLower.Contains(_filter.ORG_NAME.ToLower)).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("ORG_NAME").ToString.ToLower.Contains(_filter.ORG_NAME.ToLower)).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.MANUAL_NAME <> "" Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("MANUAL_NAME").ToString.ToLower.Contains(_filter.MANUAL_NAME.ToLower)).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("MANUAL_NAME").ToString.ToLower.Contains(_filter.MANUAL_NAME.ToLower)).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.LEAVE_FROM.HasValue Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("LEAVE_FROM") = _filter.LEAVE_FROM).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("LEAVE_FROM") = _filter.LEAVE_FROM).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If _filter.LEAVE_TO.HasValue Then
                    Dim ck = (From p As DataRow In dt.AsEnumerable Where p("LEAVE_TO") = _filter.LEAVE_TO).ToList
                    If ck.Count > 0 AndAlso ck IsNot Nothing Then
                        dt = (From p As DataRow In dt.AsEnumerable Where p("LEAVE_TO") = _filter.LEAVE_TO).ToList.CopyToDataTable
                    Else
                        dt = New DataTable
                    End If
                End If

                If dt.Rows.Count > 0 Then
                    Total = dt.Rows.Count
                    dt = dt.AsEnumerable.Skip(PageIndex * PageSize).Take(PageSize).CopyToDataTable
                Else
                    dt = New DataTable
                End If
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function


    Public Function GET_GENDER(ByVal P_EMPLOYEE_ID As Decimal) As DataTable
        Dim dt As DataTable
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_LIST.GET_GENDER", New List(Of Object)(New Object() {P_EMPLOYEE_ID}))
            If ds IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function CHECK_CHILDREN(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_EFFECT_DATE As Date, ByVal P_EXPIRE_DATE As Date) As Decimal
        Try
            Dim res As Decimal
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_LIST.CHECK_CHILDREN", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_EFFECT_DATE, P_EXPIRE_DATE}))
            If ds IsNot Nothing Then
                res = ds.Tables(0)(0)("RESULT")
            End If
            Return res
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#Region "Declare OT"

    Public Function COUNT_OT_NIGHT(ByVal P_date As Date, ByVal before_h1 As Decimal?, ByVal before_m1 As Decimal?, ByVal before_h2 As Decimal?, ByVal before_m2 As Decimal?,
                                   ByVal after_h1 As Decimal?, ByVal after_m1 As Decimal?, ByVal after_h2 As Decimal?, ByVal after_m2 As Decimal?) As Decimal
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.COUNT_OT_NIGHT", New List(Of Object)(New Object() {P_date,
                                                                                                                            before_h1,
                                                                                                                            before_m1,
                                                                                                                            before_h2,
                                                                                                                            before_m2,
                                                                                                                            after_h1,
                                                                                                                            after_m1,
                                                                                                                            after_h2,
                                                                                                                            after_m2
                                                                                                                            }))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt(0)("KQ")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_VALUE_OT_MONTH(ByVal P_EMP_ID As Decimal, ByVal periodid As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_VALUE_OT_MONTH", New List(Of Object)(New Object() {P_EMP_ID, periodid, P_DATE, P_HOURS, P_TYPE_OT}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt(0)("KQ")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_VALUE_OT_YEAR(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_VALUE_OT_YEAR", New List(Of Object)(New Object() {P_EMP_ID, P_DATE, P_HOURS, P_TYPE_OT}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If

            Return dt(0)("KQ")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CHECK_EXITS_YEARLEAVEEDIT(ByVal P_EMP_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_ID As Decimal) As Decimal
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.CHECK_EXITS_YEARLEAVEEDIT", New List(Of Object)(New Object() {P_EMP_ID, P_YEAR, P_ID}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If

            Return dt(0)("KQ")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UPDATE_OT_REG(ByVal objOT As AT_OT_REGISTRATIONDTO, ByVal P_HS_OT As String, ByVal P_USERNAME As String, ByVal P_ORG_OT_ID As Integer) As Decimal
        Dim count As Decimal = 0
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.UPDATE_OT_REG", New List(Of Object)(New Object() {objOT.ID,
                                                                                                                   objOT.EMPLOYEE_ID,
                                                                                                                   0,
                                                                                                                   objOT.OT_TYPE_ID,
                                                                                                                   objOT.REASON,
                                                                                                                   objOT.NOTE,
                                                                                                                   objOT.FROM_AM,
                                                                                                                   objOT.FROM_AM_MN,
                                                                                                                   objOT.TO_AM,
                                                                                                                   objOT.TO_AM_MN,
                                                                                                                   objOT.FROM_PM,
                                                                                                                   objOT.FROM_PM_MN,
                                                                                                                   objOT.TO_PM,
                                                                                                                   objOT.TO_PM_MN,
                                                                                                                   objOT.REGIST_DATE,
                                                                                                                   objOT.SIGN_CODE,
                                                                                                                   objOT.SIGN_ID,
                                                                                                                   objOT.TOTAL_OT,
                                                                                                                   objOT.STATUS,
                                                                                                                   objOT.PM_FROMHOURS_AFTERCHECK,
                                                                                                                   objOT.PM_TOHOURS_AFTERCHECK,
                                                                                                                   objOT.IS_PASS_DAY,
                                                                                                                   objOT.HOURS_TOTAL_AM,
                                                                                                                   objOT.HOURS_TOTAL_PM,
                                                                                                                   objOT.HOURS_TOTAL_DAY,
                                                                                                                   objOT.HOURS_TOTAL_NIGHT,
                                                                                                                   objOT.DK_PORTAL,
                                                                                                                   P_HS_OT,
                                                                                                                   P_USERNAME,
                                                                                                                   P_ORG_OT_ID,
                                                                                                                   objOT.CREATED_BY_EMP,
                                                                                                                   objOT.BY_ANOTHER,
                                                                                                                   objOT.TOTAL_DAY_TT}))
            If Not dsData Is Nothing Then
                If Not dsData.Tables(0) Is Nothing Then
                    count = CDec(dsData.Tables(0).Rows(0)(0))
                End If
            End If
            Return count
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_REG_DATA_BY_ID(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_REG_DATA_BY_ID", New List(Of Object)(New Object() {P_ID}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_HIDE_COL_TIME_OT() As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_LIST.GET_HIDE_COL_TIME_OT", New List(Of Object)(New Object() {}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_ORG_OT_BY_ID(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_ORG_OT_BY_ID", New List(Of Object)(New Object() {P_ID}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_AT_TERMINALS() As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_LIST.GET_AT_TERMINALS", New List(Of Object))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_TIME_OT_COEFF_OVER(ByVal P_DATE_SIGN As Date) As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_LIST.GET_TIME_OT_COEFF_OVER", New List(Of Object)(New Object() {P_DATE_SIGN}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CHECK_OT_SHIFT(ByVal SIGN_ID As Decimal, ByVal _from_Hour As Date, ByVal _to_Hour As Date) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_LIST.CHECK_OT_SHIFT", _
                                                   New List(Of Object)(New Object() {SIGN_ID, _from_Hour, _to_Hour, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region
#Region "Di tre ve sơm"
    Public Function GET_SWIPE_DATA(ByVal lstID As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_SWIPE_DATA", New List(Of Object)(New Object() {lstID}))
            If dsData IsNot Nothing Then
                dt = dsData.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DELETE_SWIPE_DATA(ByVal lstID As String) As Decimal
        Dim count As Decimal = 0
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.DELETE_SWIPE_DATA", New List(Of Object)(New Object() {lstID}))
            If Not dsData Is Nothing Then
                If Not dsData.Tables(0) Is Nothing Then
                    count = CDec(dsData.Tables(0).Rows(0)(0))
                End If
            End If
            Return count
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UPDATE_INSERT_AT_SWIPE_DATA(ByVal objID As Decimal, ByVal objID_AT_SWIPE As String) As Decimal
        Dim count As Decimal = 0
        Try
            Dim dsData As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.UPDATE_INSERT_AT_SWIPE_DATA", New List(Of Object)(New Object() {objID,
                                                                                                                   objID_AT_SWIPE}))
            If Not dsData Is Nothing Then
                If Not dsData.Tables(0) Is Nothing Then
                    count = CDec(dsData.Tables(0).Rows(0)(0))
                End If
            End If
            Return count
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CAL_TIME_TIMESHEET_EMP(ByVal P_USERNAME As String, _
                                           ByVal P_FROM_DATE As Date, _
                                           ByVal P_EMPLIST As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_EMP",
                                                   New List(Of Object)(New Object() {P_USERNAME, P_FROM_DATE, P_EMPLIST, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function CAL_TIME_TIMESHEET_BY_EMP(ByVal USERNAME As String, ByVal P_DELETE_ALL As Decimal, ByVal P_DATE As Date, ByVal lstEmp As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_BY_EMP",
                                                   New List(Of Object)(New Object() {USERNAME, P_DELETE_ALL, P_DATE, lstEmp, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function CAL_TIME_TIMESHEET_BY_EMPS(ByVal P_USERNAME As String, _
                                           ByVal P_FROM_DATE As Date, _
                                           ByVal P_TO_DATE As Date, _
                                           ByVal P_EMPLIST As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_BY_EMPS",
                                                   New List(Of Object)(New Object() {P_USERNAME, P_FROM_DATE, P_TO_DATE, P_EMPLIST, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

#End Region

    Public Function GET_TIMESHEET_MONTHLY_DATA() As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_DASHBOARD.GET_TIMESHEET_MONTHLY_DATA")
        Return ds
    End Function

    Public Function IMPORT_TIMESHEET_MONTHLY(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_DASHBOARD.IMPORT_TIMESHEET_MONTHLY", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function GetDataImportCO1() As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL_IMPORT")
        Return ds
    End Function
    Public Function InsertATTime_WorkStandard_proc(ByVal P_STRID As String, ByVal P_USER As String, ByVal P_LOG As String, ByVal P_YEAR As Decimal) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_PROCESS.INSERT_AT_TIME_WORKSTANDARD_COPY", New List(Of Object)(New Object() {P_STRID, P_USER, P_LOG, P_YEAR, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function Get_CALAMVIEC_IMPORT() As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_LIST.GET_CALAMVIEC_IMPORT")
        Return ds
    End Function
    Public Function IMPORT_CALAMVIEC_ACCESS(ByVal P_DATA As String, ByVal P_USERNAME As String, ByVal P_LOG As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_LIST.IMPORT_CALAMVIEC_ACCESS", New List(Of Object)(New Object() {P_DATA, P_USERNAME, P_LOG}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function GetPeriodNameAndYear(ByVal isBlank As Decimal) As DataSet
        Dim dtData As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_PERIOD_NAME_AND_YEAR", New List(Of Object)(New Object() {isBlank}))
        Return dtData
    End Function
End Class
