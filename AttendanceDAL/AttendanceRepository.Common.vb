Imports System.Globalization
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class AttendanceRepository

#Region "kỲ CÔNG"
    Public Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.LOAD_PERIOD",
                                               New With {.P_ORG_ID = obj.ORG_ID,
                                                         .P_YEAR = obj.YEAR,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check trạng thái kỳ công
    ''' </summary>
    ''' <param name="_param"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IS_PERIODSTATUS(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim iCount = (From p In Context.AT_PERIOD
                          From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                          From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                          Where p.ID = _param.PERIOD_ID And po.STATUSCOLEX = 0).Count
            If iCount = 0 Then
                Return True
            Else
                Return False
            End If

            Dim query = (From p In Context.AT_PERIOD
                         From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                         Where po.STATUSCOLEX = 1 And
                         p.ID = _param.PERIOD_ID And
                         po.ORG_ID <> 1).Any

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check trạng thái tính kỳ công
    ''' </summary>
    ''' <param name="_param"></param>
    ''' <param name="isAfter"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IS_PERIOD_PAYSTATUS(ByVal _param As ParamDTO, ByVal isAfter As Boolean, ByVal log As UserLog) As Boolean
        Try
            Dim period_id As Decimal = 0
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            period_id = _param.PERIOD_ID

            If isAfter Then
                Dim endDate_after = (From p In Context.AT_PERIOD
                                     Where p.ID = _param.PERIOD_ID
                                     Select p.END_DATE).FirstOrDefault
                If endDate_after IsNot Nothing Then
                    endDate_after = endDate_after.Value.AddDays(-1)
                    Dim period_after = (From p In Context.AT_PERIOD
                                        Where p.END_DATE = _param.ENDDATE).FirstOrDefault

                    If period_after IsNot Nothing Then
                        period_id = period_after.ID
                    End If
                End If
            End If

            Dim query = (From p In Context.AT_PERIOD
                         From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                         From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And
                                                                  f.USERNAME.ToUpper = log.Username.ToUpper)
                         Where po.STATUSPAROX = _param.STATUS And
                         p.ID = period_id And
                         po.ORG_ID <> 1).Any

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IS_PERIODSTATUS_BY_DATE(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Dim emp As HU_EMPLOYEE
            If Not String.IsNullOrEmpty(_param.EMPLOYEE_CODE) Then
                emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Equals(_param.EMPLOYEE_CODE.ToLower())).FirstOrDefault
            Else
                emp = (From p In Context.HU_EMPLOYEE Where p.ID = _param.EMPLOYEE_ID).FirstOrDefault
            End If

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = emp.ORG_ID,
                                           .P_ISDISSOLVE = False})
            End Using

            Dim exists = (From p In Context.AT_PERIOD
                          From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                          From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                          Where po.STATUSCOLEX = 1 And p.START_DATE <= _param.FROMDATE And
                          p.END_DATE >= _param.FROMDATE And po.ORG_ID <> 1).Any
            ' NEU KY CONG MO
            If exists <= 0 And _param.ENDDATE.HasValue Then
                exists = (From p In Context.AT_PERIOD
                          From po In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                          From k In Context.SE_CHOSEN_ORG.Where(Function(f) po.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                          Where po.STATUSCOLEX = 1 And p.START_DATE <= _param.ENDDATE And
                          p.END_DATE >= _param.ENDDATE And po.ORG_ID <> 1).Any
            End If
            Return exists
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function Load_Emp_obj() As List(Of AT_PERIODDTO)
        Try
            Dim dt = (From p In Context.OT_OTHER_LIST.Where(Function(f) f.TYPE_CODE = "OBJECT_EMPLOYEE" AndAlso f.ACTFLG = "A") Select New AT_PERIODDTO With {
                     .ID = p.ID,
                     .PERIOD_NAME = p.NAME_VN}).ToList
            Return dt
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As List(Of AT_PERIODDTO)
        Try
            Dim query = From p In Context.AT_PERIOD
                        Order By p.MONTH Ascending

            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .PERIOD_T = p.PERIOD_T,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE})

            If IsNumeric(obj.YEAR) Then
                lst = lst.Where(Function(f) f.YEAR = obj.YEAR)
            End If

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function Load_date(ByVal period_id As Decimal, ByVal emp_obj_id As Decimal) As AT_PERIODDTO
        Try
            Dim re As New AT_PERIODDTO

            Dim id = (From p In Context.AT_TIME_PERIOD
                      From a In Context.AT_PERIOD.Where(Function(f) p.EFFECTMONTH.Value.Month <= f.MONTH And p.EFFECTMONTH.Value.Year <= f.YEAR).DefaultIfEmpty
                      Where p.OBJ_EMPLOYEE_ID = emp_obj_id And a.ID = period_id
                      Order By p.EFFECTMONTH Descending
                      ).FirstOrDefault '.p.ID
            If id IsNot Nothing Then
                Dim dt = (From p In Context.AT_TIME_PERIOD.Where(Function(f) f.ID = id.p.ID)).FirstOrDefault
                Dim start_date = ""
                Dim end_date = ""

                start_date = start_date + If(dt.FROMDATE_PERIOD < 10, "0" + dt.FROMDATE_PERIOD.ToString, dt.FROMDATE_PERIOD.ToString)
                If dt.FROMDATE_BEFOREMONTH = -1 Then
                    'If dt.EFFECTMONTH.Value.Month = 1 Then
                    If id.a.MONTH = 1 Then
                        start_date = start_date + "/" + "12/" + (id.a.YEAR - 1).ToString
                    Else
                        start_date = start_date + "/" + If((id.a.MONTH - 1) < 10, "0" + (id.a.MONTH - 1).ToString, (id.a.MONTH - 1).ToString) + "/" + id.a.YEAR.ToString
                    End If
                Else
                    start_date = start_date + "/" + If(id.a.MONTH < 10, "0" + id.a.MONTH.ToString, id.a.MONTH.ToString) + "/" + id.a.YEAR.ToString
                End If
                If dt.TODATE_ENDMONTH = -1 Then
                    end_date = DateTime.DaysInMonth(id.a.YEAR, id.a.MONTH).ToString + "/" + If(id.a.MONTH < 10, "0" + id.a.MONTH.ToString, id.a.MONTH.ToString) + "/" + id.a.YEAR.ToString
                Else
                    end_date = If(dt.TODATE_PERIOD < 10, "0" + dt.TODATE_PERIOD.ToString, dt.TODATE_PERIOD.ToString) + "/" + If(id.a.MONTH < 10, "0" + id.a.MONTH.ToString, id.a.MONTH.ToString) + "/" + id.a.YEAR.ToString
                End If

                Dim dateTemp As Date
                DateTime.TryParseExact(start_date, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, dateTemp)
                re.START_DATE = dateTemp
                DateTime.TryParseExact(end_date, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, dateTemp)
                re.END_DATE = dateTemp

                re.PERIOD_T = id.a.PERIOD_T

                're.START_DATE = Date.Parse(start_date)

                're.END_DATE = Date.Parse(end_date)

                Return re
            Else
                Return re
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As AT_PERIODDTO
        Try
            Dim query = From p In Context.AT_PERIOD
                        Where p.ID = obj.PERIOD_ID
            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CLOSEDOPEN_PERIOD(ByVal param As ParamDTO, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CLOSEDOPEN_PERIOD",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = param.ORG_ID,
                                                         .P_ISDISSOLVE = param.IS_DISSOLVE,
                                                         .P_STATUS = param.STATUS,
                                                         .P_PERIOD_ID = param.PERIOD_ID})
            End Using

            If param.STATUS = 0 Then
                ' TINH phep nam
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT",
                                                   New With {.P_USERNAME = log.Username.ToUpper,
                                                             .P_ORG_ID = param.ORG_ID,
                                                             .P_PERIOD_ID = param.PERIOD_ID,
                                                             .P_ISDISSOLVE = param.IS_DISSOLVE})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function



#End Region

#Region "Ham Dung chung"
    Public Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal),
                                       ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean
        Dim lstPeriod As List(Of AT_PERIOD)
        Try
            lstPeriod = (From p In Context.AT_PERIOD
                         Where (startdate >= p.START_DATE And startdate <= p.END_DATE) _
                         Or (enddate >= p.START_DATE And enddate <= p.END_DATE) _
                         Or (startdate <= p.START_DATE And enddate >= p.END_DATE)).ToList

            If lstPeriod.Count = 0 Then
                sAction = "Chưa thiết lập kỳ công. Thao tác thực hiện không thành công"
                Return True
            End If

            Dim lstOrg = (From p In Context.HU_EMPLOYEE Where lstEmp.Contains(p.ID) Select p.ORG_ID).ToList
            For Each item In lstPeriod
                Dim lstStatus = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = item.ID And lstOrg.Contains(p.ORG_ID)).ToList
                For Each status In lstStatus
                    If status.STATUSCOLEX = 0 Then
                        sAction = "Bảng công hiện tại đã khóa. Thao tác thực hiện không thành công."
                        Return False
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, ".CheckPeriodClose")
        End Try
    End Function
#End Region

#Region "Máy chấm công"
    Public Function GetTerminalFromOtOtherList() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TERMINAL_FROM_OTOTHERLIST",
                                               New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTerminal(ByVal obj As AT_TERMINALSDTO, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TERMINAL",
                                               New With {.P_ID = obj.ID,
                                                         .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetTerminalAuto() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TERMINAL_AUTO",
                                               New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function UpdateTerminalLastTime(ByVal obj As AT_TERMINALSDTO) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_UPDATE_TERMINAL_LASTIME",
                                 New With {.P_ID = obj.ID,
                                           .P_DATE = obj.MODIFIED_DATE})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function UpdateTerminalStatus(ByVal obj As AT_TERMINALSDTO) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_STATUS_TERMINAL_LASTIME",
                                 New With {.P_ID = obj.ID,
                                           .P_DATE = obj.MODIFIED_DATE,
                                           .P_STATUS = obj.TERMINAL_STATUS,
                                           .P_ROW = obj.TERMINAL_ROW})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Export Data By Org"
    Public Function GetDataFromOrg(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETDATAFROMORG",
                                               New With {.P_USERNAME = log.Username.ToUpper,
                                                         .P_ORG_ID = obj.ORG_ID,
                                                         .P_ISDISSOLVE = obj.IS_DISSOLVE,
                                                         .P_EXPORT_TYPE = obj.P_EXPORT_TYPE,
                                                         .P_PERIOD_ID = obj.PERIOD_ID,
                                                         .P_EMP_OBJ = obj.EMP_OBJ,
                                                         .P_START_DATE = obj.START_DATE,
                                                         .P_END_DATE = obj.END_DATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetPortalWSDataImport(ByVal obj As ParamDTO) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_PORTAL_WS_DATA_IMPORT",
                                               New With {.P_EMPLOYEE_ID = obj.EMPLOYEE_ID,
                                                         .P_PERIOD_ID = obj.PERIOD_ID,
                                                         .P_EMP_OBJ = obj.EMP_OBJ,
                                                         .P_START_DATE = obj.START_DATE,
                                                         .P_END_DATE = obj.END_DATE,
                                                         .P_CUR = cls.OUT_CURSOR,
                                                         .P_CUR2 = cls.OUT_CURSOR,
                                                         .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

    Public Function LOAD_PERIODYEAR() As List(Of AT_PERIODDTO)
        Try
            Dim query = From p In Context.AT_PERIOD

            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .YEAR = p.YEAR}).Distinct

            lst = lst.OrderBy("YEAR ASC")

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

End Class
