Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Objects
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Partial Public Class AttendanceRepository
    Public Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Boolean
        Try
            Dim q = (From p In Context.AT_LEAVESHEET_DETAIL Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID And p.LEAVE_DAY >= objValidate.LEAVE_FROM And p.LEAVE_DAY <= objValidate.LEAVE_TO And (p.LEAVESHEET_ID <> objValidate.ID Or objValidate.ID = 0) Select p).ToList()
            If q.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet
        Dim dsData As New DataSet()
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_AT_LEAVESHEET.GET_LEAVESHEET_BYID",
                                               New With {.P_ID = Leave_SheetID,
                                                         .P_TRUCT = Struct,
                                                         .P_LEAVE = cls.OUT_CURSOR,
                                                         .P_LEAVE_DETAIL = cls.OUT_CURSOR}, False)
            End Using
            Return dsData
        Catch ex As Exception
        Finally
            dsData.Dispose()
        End Try
    End Function


    Public Function GET_EXPIREDATE_P_BU(ByVal EMP_ID As Decimal, ByVal Fromdate As Date) As DataTable
        Dim dsData As New DataTable()
        Try
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_EXPIREDATE_P_BU",
                                               New With {.P_EMP_ID = EMP_ID,
                                                         .P_DATE = Fromdate,
                                                         .P_OUT = cls.OUT_CURSOR}, True)
            End Using
            Return dsData
        Catch ex As Exception
        Finally
            dsData.Dispose()
        End Try
    End Function

    Public Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable
        Dim dData As New DataTable()
        Try
            Using cls As New DataAccess.QueryData
                dData = cls.ExecuteStore("PKG_AT_LEAVESHEET.GET_LEAVE_SHEET_DETAIL_BYDATE",
                                               New With {.P_EMPLOYEE_ID = employee_id,
                                                         .P_FROM_DATE = fromDate,
                                                         .P_TO_DATE = toDate,
                                                         .P_MANUAL_ID = manualId,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
            End Using
            Return dData
        Catch ex As Exception
        Finally
            dData.Dispose()
        End Try
    End Function
    Public Function Validate_LeaveSheet(ByVal _validate As AT_LEAVESHEETDTO) As VALIDATE_DTO
        Try
            Using cls As New DataAccess.QueryData
                Dim strLEAVEFROM = CDate(_validate.LEAVE_FROM).ToString("yyyy-MM-dd")
                Dim strLEAVETO = CDate(_validate.LEAVE_TO).ToString("yyyy-MM-dd")
                Dim obj = New With {.P_USERID = CInt(_validate.USERID),
                                                         .P_LANGUAGE = "VI-VN",
                                                         .P_LEAVEFROM = strLEAVEFROM,
                                                         .P_LEAVETO = strLEAVETO,
                                                         .P_MANUALID = _validate.MANUAL_ID,
                                                         .P_LEAVEID = _validate.ID,
                                                         .P_LEAVE_TYPE = "LEAVE",
                                                         .P_TIMEFROM = 5,
                                                         .P_TIMETO = 5,
                                                         .P_DAYNUM = _validate.DAY_NUM,
                                                         .P_MESSAGE = cls.OUT_STRING,
                                                         .P_RESPONSESTATUS = cls.OUT_NUMBER}
                Dim dtData = cls.ExecuteStore("PKG_API_MOBILE.API__VALIDATE_REGISTER_LEAVE", obj)
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
    Public Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, ByVal log As UserLog) As Boolean
        Dim rPH As DataRow
        Dim CT As DataTable = New DataTable()
        Dim oProps() As PropertyInfo = Nothing
        Dim objCT As AT_LEAVESHEET_DETAIL
        Dim MANUAL_ID As Decimal
        Dim AT_LEAVESHEET_ID As Decimal
        Dim IS_INSERT As Boolean = False
        Dim STATUS As Integer
        Try
            If dsLeaveSheet.Tables(0) IsNot Nothing AndAlso dsLeaveSheet.Tables(0).Rows.Count = 1 Then
                rPH = dsLeaveSheet.Tables(0).Rows(0)
            Else
                Return False
            End If
            If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dsLeaveSheet.Tables(1).Rows.Count > 0 Then
                CT = dsLeaveSheet.Tables(1)
            Else
                Return False
            End If
            'ket thuc kiem tra va gan du lieu
            If IsNumeric(rPH("ID")) AndAlso rPH("ID") > 0 Then
                'update
                Dim ID_LEAVE As Decimal = CDec(rPH("ID"))
                Dim objPH = (From p In Context.AT_LEAVESHEET Where p.ID = ID_LEAVE Select p).SingleOrDefault
                objPH.MODIFIED_BY = log.Username ' rPH("EMPLOYEE_NAME").ToString
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName
                rPH("STATUS") = 1
                oProps = objPH.GetType().GetProperties()
                For Each pi As PropertyInfo In oProps
                    Try
                        pi.SetValue(objPH, If(IsDBNull(rPH(pi.Name)), Nothing, rPH(pi.Name)), Nothing)
                    Catch ex As Exception
                        Continue For
                    End Try
                Next pi
                Dim lst = (From p In Context.AT_LEAVESHEET_DETAIL Where p.LEAVESHEET_ID = objPH.ID).ToList()
                For index = 0 To lst.Count - 1
                    Context.AT_LEAVESHEET_DETAIL.DeleteObject(lst(index))
                Next
                For Each row As DataRow In CT.Rows
                    objCT = New AT_LEAVESHEET_DETAIL
                    oProps = objCT.GetType().GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Try
                            pi.SetValue(objCT, If(IsDBNull(row(pi.Name)), Nothing, row(pi.Name)), Nothing)
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next pi
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next

                Using cls As New DataAccess.QueryData
                    Dim dData = cls.ExecuteStore("PKG_AT_PROCESS.DELETE_PROCESS_APPROVED_STATUS",
                                                   New With {.P_ID = ID_LEAVE,
                                                             .P_OUT = cls.OUT_NUMBER}, True)
                End Using
            Else
                'insert 
                IS_INSERT = True
                Dim objPH As New AT_LEAVESHEET

                oProps = objPH.GetType().GetProperties()
                For Each pi As PropertyInfo In oProps
                    Try
                        pi.SetValue(objPH, If(IsDBNull(rPH(pi.Name)), Nothing, rPH(pi.Name)), Nothing)
                    Catch ex As Exception
                        Continue For
                    End Try
                Next pi
                objPH.CREATED_BY = log.Username 'rPH("EMPLOYEE_NAME").ToString
                objPH.MODIFIED_BY = log.Username ' rPH("EMPLOYEE_NAME").ToString
                objPH.CREATED_DATE = DateTime.Now
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.CREATED_LOG = log.Ip + "\" + log.ComputerName
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName
                objPH.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)

                Context.AT_LEAVESHEET.AddObject(objPH)
                MANUAL_ID = objPH.MANUAL_ID
                AT_LEAVESHEET_ID = objPH.ID
                For Each row As DataRow In CT.Rows
                    objCT = New AT_LEAVESHEET_DETAIL
                    oProps = objCT.GetType().GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Try
                            pi.SetValue(objCT, If(IsDBNull(row(pi.Name)), Nothing, row(pi.Name)), Nothing)
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next pi
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next
                STATUS = objPH.STATUS
            End If
            Context.SaveChanges()

            Dim MANUAL_CODE = (From p In Context.AT_TIME_MANUAL Where p.ID = CDec(MANUAL_ID) Select p.CODE).FirstOrDefault
            If MANUAL_CODE Is Nothing Then
                MANUAL_CODE = ""
            End If
            If MANUAL_CODE.ToUpper.Trim = "B" Or MANUAL_CODE.ToUpper.Trim = "P" Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_AT_PROCESS.UPDATE_OLD_LEAVE",
                                                   New With {.P_ID = AT_LEAVESHEET_ID})
                End Using
            End If
            If IS_INSERT AndAlso STATUS = 1 AndAlso IsNumeric(MANUAL_ID) Then
                If MANUAL_CODE.ToUpper.Trim = "TS" Or MANUAL_CODE.ToUpper.Trim = "O" Or MANUAL_CODE.ToUpper.Trim = "CO" Or MANUAL_CODE.ToUpper.Trim = "VS" Or MANUAL_CODE.ToUpper.Trim = "KT" Or MANUAL_CODE.ToUpper.Trim = "DS " Then
                    Dim REGIME_CODE As String = ""
                    If MANUAL_CODE.ToUpper.Trim = "DS" Then
                        REGIME_CODE = "D203"
                    ElseIf MANUAL_CODE.ToUpper.Trim = "O" Then
                        REGIME_CODE = "O1"
                    ElseIf MANUAL_CODE.ToUpper.Trim = "CO" Then
                        REGIME_CODE = "O2"
                    ElseIf MANUAL_CODE.ToUpper.Trim = "KT" Then
                        REGIME_CODE = "T1"
                    ElseIf MANUAL_CODE.ToUpper.Trim = "TS" Then
                        REGIME_CODE = "T4"
                    ElseIf MANUAL_CODE.ToUpper.Trim = "VS" Then
                        REGIME_CODE = "T12"
                    End If

                    Using cls As New DataAccess.QueryData
                        Dim dData = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_REGIMES",
                                                       New With {.P_ID = AT_LEAVESHEET_ID,
                                                                 .P_REGIME_CODE = REGIME_CODE}, True)

                    End Using

                    If MANUAL_CODE.ToUpper.Trim = "TS" Then
                        Using cls As New DataAccess.QueryData
                            Dim dData1 = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_MATERNITY_MNG",
                                                           New With {.P_ID = AT_LEAVESHEET_ID,
                                                                     .P_REGIME_CODE = REGIME_CODE}, True)
                        End Using
                    End If

                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            CT.Dispose()
            rPH = Nothing
            objCT = Nothing
        End Try
    End Function
    Public Function SaveLeaveSheet_Portal(ByVal LeaveSheet As AT_LEAVESHEETDTO, ByVal dsLeaveSheet As List(Of AT_LEAVESHEETDTO), ByRef gID As Decimal, ByVal log As UserLog) As Boolean
        Dim objCT As AT_LEAVESHEET_DETAIL
        Dim objPH_ID As Decimal = 0
        Try
            'If dsLeaveSheet.Tables(0) IsNot Nothing AndAlso dsLeaveSheet.Tables(0).Rows.Count = 1 Then
            '    rPH = dsLeaveSheet.Tables(0).Rows(0)
            'Else
            '    Return False
            'End If
            'If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dsLeaveSheet.Tables(1).Rows.Count > 0 Then
            '    CT = dsLeaveSheet.Tables(1)
            'Else
            '    Return False
            'End If
            'ket thuc kiem tra va gan du lieu
            Dim userInfo = (From p In Context.SE_USER Where p.USERNAME.Trim.ToLower = log.Username.Trim.ToLower).FirstOrDefault
            If LeaveSheet.ID > 0 Then
                'update
                Dim ID_LEAVE As Decimal = LeaveSheet.ID
                Dim objPH = (From p In Context.AT_LEAVESHEET Where p.ID = ID_LEAVE Select p).SingleOrDefault
                objPH.MODIFIED_BY = userInfo.FULLNAME
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName

                objPH.EMPLOYEE_ID = LeaveSheet.EMPLOYEE_ID
                objPH.BALANCE_NOW = LeaveSheet.BALANCE_NOW
                objPH.LEAVE_FROM = LeaveSheet.LEAVE_FROM
                objPH.LEAVE_TO = LeaveSheet.LEAVE_TO
                'objPH.NOTE = LeaveSheet.NOTE
                objPH.MANUAL_ID = LeaveSheet.MANUAL_ID
                objPH.DAY_NUM = LeaveSheet.DAY_NUM
                objPH.EMP_APPROVES_NAME = LeaveSheet.EMP_APPROVES_NAME
                objPH.IS_APP = LeaveSheet.IS_APP
                objPH.STATUS = LeaveSheet.STATUS
                'objPH.REASON = LeaveSheet.REASON

                objPH.REASON_LEAVE = LeaveSheet.NOTE
                objPH.NOTE = LeaveSheet.REMARK
                objPH.FROM_LOCATION = LeaveSheet.FROM_LOCATION
                objPH.TO_LOCATION = LeaveSheet.TO_LOCATION
                objPH.PROVINCE_ID = LeaveSheet.PROVINCE_ID
                objPH.TS_SON = LeaveSheet.TS_SON
                objPH.NUMBER_KILOMETER = LeaveSheet.NUMBER_KILOMETER


                'oProps = objPH.GetType().GetProperties()
                'For Each pi As PropertyInfo In oProps
                '    Try
                '        pi.SetValue(objPH, rPH(pi.Name), Nothing)
                '    Catch ex As Exception
                '        Continue For
                '    End Try
                'Next pi

                Dim lst = (From p In Context.AT_LEAVESHEET_DETAIL Where p.LEAVESHEET_ID = objPH.ID).ToList()
                For index = 0 To lst.Count - 1
                    Context.AT_LEAVESHEET_DETAIL.DeleteObject(lst(index))
                Next

                For Each pi As AT_LEAVESHEETDTO In dsLeaveSheet
                    objCT = New AT_LEAVESHEET_DETAIL
                    objCT.EMPLOYEE_ID = objPH.EMPLOYEE_ID
                    objCT.LEAVE_DAY = pi.LEAVE_DAY
                    objCT.MANUAL_ID = pi.MANUAL_ID
                    objCT.DAY_NUM = pi.DAY_NUM
                    objCT.STATUS_SHIFT = pi.STATUS_SHIFT
                    'objCT.SHIFT_ID = pi.SHIFT_ID
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next

                Using cls As New DataAccess.QueryData
                    Dim dData = cls.ExecuteStore("PKG_AT_PROCESS.DELETE_PROCESS_APPROVED_STATUS",
                                                   New With {.P_ID = ID_LEAVE,
                                                             .P_OUT = cls.OUT_NUMBER}, True)
                End Using
                objPH_ID = LeaveSheet.ID
            Else
                'insert 
                Dim obj_ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET.EntitySet.Name)
                Dim objPH As New AT_LEAVESHEET
                objPH.CREATED_BY = userInfo.FULLNAME
                objPH.MODIFIED_BY = userInfo.FULLNAME
                objPH.CREATED_DATE = DateTime.Now
                objPH.MODIFIED_DATE = DateTime.Now
                objPH.CREATED_LOG = log.Ip + "\" + log.ComputerName
                objPH.MODIFIED_LOG = log.Ip + "\" + log.ComputerName
                objPH.ID = obj_ID
                objPH.EMPLOYEE_ID = LeaveSheet.EMPLOYEE_ID
                objPH.BALANCE_NOW = LeaveSheet.BALANCE_NOW
                objPH.LEAVE_FROM = LeaveSheet.LEAVE_FROM
                objPH.LEAVE_TO = LeaveSheet.LEAVE_TO
                'objPH.NOTE = LeaveSheet.NOTE
                objPH.MANUAL_ID = LeaveSheet.MANUAL_ID
                objPH.DAY_NUM = LeaveSheet.DAY_NUM
                objPH.EMP_APPROVES_NAME = LeaveSheet.EMP_APPROVES_NAME
                objPH.IS_APP = LeaveSheet.IS_APP
                objPH.STATUS = LeaveSheet.STATUS
                'objPH.REASON = LeaveSheet.REASON

                objPH.REASON_LEAVE = LeaveSheet.NOTE
                objPH.NOTE = LeaveSheet.REMARK
                objPH.FROM_LOCATION = LeaveSheet.FROM_LOCATION
                objPH.TO_LOCATION = LeaveSheet.TO_LOCATION
                objPH.PROVINCE_ID = LeaveSheet.PROVINCE_ID
                objPH.TS_SON = LeaveSheet.TS_SON
                objPH.NUMBER_KILOMETER = LeaveSheet.NUMBER_KILOMETER


                Context.AT_LEAVESHEET.AddObject(objPH)

                For Each pi As AT_LEAVESHEETDTO In dsLeaveSheet
                    objCT = New AT_LEAVESHEET_DETAIL
                    objCT.EMPLOYEE_ID = objPH.EMPLOYEE_ID
                    objCT.LEAVE_DAY = pi.LEAVE_DAY
                    objCT.MANUAL_ID = pi.MANUAL_ID
                    objCT.DAY_NUM = pi.DAY_NUM
                    objCT.STATUS_SHIFT = pi.STATUS_SHIFT
                    'objCT.SHIFT_ID = pi.SHIFT_ID
                    objCT.ID = Utilities.GetNextSequence(Context, Context.AT_LEAVESHEET_DETAIL.EntitySet.Name)
                    objCT.LEAVESHEET_ID = objPH.ID
                    Context.AT_LEAVESHEET_DETAIL.AddObject(objCT)
                Next
                objPH_ID = obj_ID
            End If
            Context.SaveChanges()
            gID = objPH_ID
            Return True
        Catch ex As Exception
            Return False
        Finally
            'CT.Dispose()
            'rPH = Nothing
            objCT = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lấy thông tin đơn xin nghỉ việc theo mã nhân viên và số đơn nghỉ việc
    ''' </summary>
    ''' <param name="EmpId"></param>
    ''' <param name="LeaveSheetId"></param>
    ''' <returns></returns>
    Public Function GetLeaveSheet(ByVal EmpId As Decimal, ByVal LeaveSheetId As Decimal) As AT_LEAVESHEET
        Try
            Dim qu = From leaveSheet In Context.AT_LEAVESHEET
                     Where leaveSheet.EMPLOYEE_ID = EmpId AndAlso leaveSheet.ID = LeaveSheetId
                     Select leaveSheet

            If qu.Any() Then
                Return qu.First()
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

        Return Nothing
    End Function
    Public Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim lstID = (From p In Context.AT_LEAVESHEET_DETAIL Where (p.LEAVE_DAY >= _filter.FROM_DATE OrElse Not _filter.FROM_DATE.HasValue) And (p.LEAVE_DAY <= _filter.END_DATE OrElse Not _filter.END_DATE.HasValue) Select p.LEAVESHEET_ID).ToList.Distinct.ToList()
            '
            'From l In Context.AT_LEAVESHEET_DETAIL.Where(Function(f) f.LEAVESHEET_ID = p.ID And (f.LEAVE_DAY >= _filter.FROM_DATE OrElse Not _filter.FROM_DATE.HasValue) And (f.LEAVE_DAY <= _filter.END_DATE OrElse Not _filter.END_DATE.HasValue))
            Dim query = From p In Context.AT_LEAVESHEET
                        From l In Context.AT_LEAVESHEET_DETAIL.Where(Function(f) f.LEAVESHEET_ID = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        From pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0 And h.PROCESS_TYPE.ToUpper = "LEAVE").Min(Function(ki) ki.APP_LEVEL))).DefaultIfEmpty()
                        From tt In Context.HU_TITLE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = tt.MASTER).DefaultIfEmpty
                        From pa In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = 1).DefaultIfEmpty
                        From se In Context.SE_APP_TEMPLATE.Where(Function(f) f.ID = pa.TEMPLATE_ID).DefaultIfEmpty
            If _filter.ISTEMINAL Then
                query = query.Where(Function(f) f.e.WORK_STATUS = 257)
                If _filter.WORKINGDAY.HasValue Then
                    query = query.Where(Function(f) f.e.TER_LAST_DATE <= _filter.WORKINGDAY)
                End If
            End If
            'If _filter.FROM_DATE.HasValue Then
            '    query = query.Where(Function(f) f.p.LEAVE_FROM >= _filter.FROM_DATE)
            'End If
            'If _filter.END_DATE.HasValue Then
            '    query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
            'End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.e.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.e.FULLNAME_VN.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
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
            If Not String.IsNullOrEmpty(_filter.FROM_LOCATION) Then
                query = query.Where(Function(f) f.p.FROM_LOCATION.ToLower().Contains(_filter.FROM_LOCATION.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_LOCATION) Then
                query = query.Where(Function(f) f.p.TO_LOCATION.ToLower().Contains(_filter.TO_LOCATION.ToLower()))
            End If
            If IsNumeric(_filter.NUMBER_KILOMETER) Then
                query = query.Where(Function(f) f.p.NUMBER_KILOMETER = _filter.NUMBER_KILOMETER)
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.BALANCE_NOW.HasValue Then
                query = query.Where(Function(f) f.p.BALANCE_NOW = _filter.BALANCE_NOW)
            End If
            'If IsDate(_filter.FROM_DATE) And IsDate(_filter.END_DATE) Then
            '    query = query.Where(Function(f) (f.l.LEAVE_DAY >= _filter.FROM_DATE OrElse f.l.LEAVE_DAY <= _filter.END_DATE) Or
            '                     (f.l.LEAVE_DAY >= _filter.FROM_DATE OrElse f.l.LEAVE_DAY <= _filter.END_DATE))
            'End If

            If IsDate(_filter.FROM_DATE) And IsDate(_filter.END_DATE) Then
                query = query.Where(Function(f) (f.p.LEAVE_FROM >= _filter.FROM_DATE And f.p.LEAVE_FROM <= _filter.END_DATE) Or
                                 (f.p.LEAVE_TO >= _filter.FROM_DATE And f.p.LEAVE_TO <= _filter.END_DATE))
            Else
                If _filter.FROM_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_FROM <= _filter.FROM_DATE And f.p.LEAVE_TO >= _filter.FROM_DATE)
                End If
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
                End If
            End If
            If IsDate(_filter.LEAVE_FROM) Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = CDate(_filter.LEAVE_FROM))
            End If
            If IsDate(_filter.LEAVE_TO) Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If

            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.TEMPLATE_NAME) Then
                query = query.Where(Function(f) f.se.TEMPLATE_NAME.ToLower().Contains(_filter.TEMPLATE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                query = query.Where(Function(f) f.p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If IsNumeric(_filter.MANUAL_ID) Then
                query = query.Where(Function(f) f.p.MANUAL_ID = _filter.MANUAL_ID)
            End If
            'If IsDate(_filter.LEAVE_DAY) Then
            '    query = query.Where(Function(f) f.l.LEAVE_DAY = _filter.LEAVE_DAY)
            'End If
            If IsNumeric(_filter.STATUS) Then
                query = query.Where(Function(f) f.p.STATUS = _filter.STATUS)
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) AndAlso _filter.MANUAL_CODE.Split(",").Any Then
                Dim list = New List(Of String)
                For Each code In _filter.MANUAL_CODE.Split(",")
                    list.Add(code.Trim.ToUpper)
                Next
                query = query.Where(Function(f) list.Contains(f.m.CODE.ToUpper.Trim))
            End If
            Dim str = " - "
            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .TEMPLATE_NAME = p.se.TEMPLATE_NAME,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = If(p.m.CODE Is Nothing, "" + p.m.CODE_KH + p.m.NAME, "[" + p.m.CODE_KH + "] " + p.m.NAME),
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .EMP_APPROVES_NAME = p.ee.EMPLOYEE_CODE & str & p.ee.FULLNAME_VN,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .REMARK = p.p.REMARK,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = If(p.p.STATUS = 0, "Chờ phê duyệt", If(p.p.STATUS = 1, "Phê duyệt", If(p.p.STATUS = 2, "Không phê duyệt", "Chưa gửi duyệt"))),
                                                                       .FROM_LOCATION = p.p.FROM_LOCATION,
                                                                       .TO_LOCATION = p.p.TO_LOCATION,
                                                                       .NUMBER_KILOMETER = p.p.NUMBER_KILOMETER,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", ""),
                                                                       .PROVINCE_ID = p.p.PROVINCE_ID,
                                                                       .PROVINCE_NAME = p.pro.NAME_VN,
                                                                       .REASON_LEAVE = p.p.REASON_LEAVE}).Distinct

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ApproveApp(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal userName As String, ByVal type As String) As Decimal

        Try
            If type = "LEAVE" Then
                For Each i In lstID
                    Dim res = (From p In Context.AT_LEAVESHEET Where p.ID = i Select p).FirstOrDefault

                    If res IsNot Nothing Then
                        Dim maunualCode = (From p In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = res.MANUAL_ID) Select p.CODE).FirstOrDefault
                        res.STATUS = status

                        If maunualCode = "B" Or maunualCode = "P" Then
                            Using cls As New DataAccess.QueryData
                                cls.ExecuteStore("PKG_AT_PROCESS.UPDATE_OLD_LEAVE",
                                                   New With {.P_ID = res.ID})
                            End Using
                        End If

                        If maunualCode = "TS" Or maunualCode = "O" Or maunualCode = "CO" Or maunualCode = "VS" Or maunualCode = "KT" Or maunualCode = "DS " Then
                            Dim REGIME_CODE As String = ""
                            If maunualCode = "DS" Then
                                REGIME_CODE = "D203"
                            ElseIf maunualCode = "O" Then
                                REGIME_CODE = "O1"
                            ElseIf maunualCode = "CO" Then
                                REGIME_CODE = "O2"
                            ElseIf maunualCode = "KT" Then
                                REGIME_CODE = "T1"
                            ElseIf maunualCode = "TS" Then
                                REGIME_CODE = "T4"
                            ElseIf maunualCode = "VS" Then
                                REGIME_CODE = "T12"
                            End If

                            Using cls As New DataAccess.QueryData
                                Dim dData = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_REGIMES",
                                                   New With {.P_ID = res.ID,
                                                             .P_REGIME_CODE = REGIME_CODE}, True)

                            End Using

                            If maunualCode = "TS" Then
                                Using cls As New DataAccess.QueryData
                                    Dim dData1 = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_MATERNITY_MNG",
                                                       New With {.P_ID = res.ID,
                                                                 .P_REGIME_CODE = REGIME_CODE}, True)
                                End Using
                            End If
                        End If
                    End If

                    Dim app = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = i Select p)
                    For Each item In app
                        Dim objData As New PROCESS_APPROVED_STATUS With {.ID = item.ID}
                        objData.APP_STATUS = status
                    Next
                Next
            ElseIf type = "DMVS" Then
                For Each i In lstID
                    Dim reg = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = i Select p).FirstOrDefault
                    If reg IsNot Nothing Then
                        reg.STATUS = status
                    End If

                    Dim app = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = i Select p)
                    For Each item In app
                        item.APP_STATUS = status
                    Next
                Next
            End If

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteLeaveOT")
            Throw ex
        End Try
    End Function

    Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean
        Dim lstl As AT_LEAVESHEET
        Dim id As Decimal = 0
        Try
            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                lstl = (From p In Context.AT_LEAVESHEET Where id = p.ID).FirstOrDefault
                If Not lstl Is Nothing Then
                    Context.AT_LEAVESHEET.DeleteObject(lstl)
                    Dim details = (From r In Context.AT_LEAVESHEET_DETAIL Where r.LEAVESHEET_ID = lstl.ID).ToList
                    If Not details Is Nothing Then
                        For index1 = 0 To details.Count - 1
                            Context.AT_LEAVESHEET_DETAIL.DeleteObject(details(index1))
                        Next
                    End If
                    Dim process = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = lstl.ID And p.PROCESS_TYPE = "LEAVE").FirstOrDefault
                    If process IsNot Nothing Then
                        Context.PROCESS_APPROVED_STATUS.DeleteObject(process)
                    End If
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteLeaveOT")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty
                        From papp In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.WaitingForApproval _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.WaitingForApproval And h.PROCESS_TYPE.ToUpper = "LEAVE").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From eeap_title In Context.HU_TITLE.Where(Function(f) f.ID = papp.EMPLOYEE_APPROVED And p.STATUS <> 2).DefaultIfEmpty
                        From eeap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = eeap_title.MASTER).DefaultIfEmpty
                        From last_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.ApprovedByLM _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.ApprovedByLM And h.PROCESS_TYPE.ToUpper = "LEAVE").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From e_last_ap_title In Context.HU_TITLE.Where(Function(f) f.ID = last_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From e_last_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e_last_ap_title.MASTER).DefaultIfEmpty
                        From last_not_app In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.UnApprovedByLM _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.UnApprovedByLM And h.PROCESS_TYPE.ToUpper = "LEAVE").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From e_last_not_ap_title In Context.HU_TITLE.Where(Function(f) f.ID = last_not_app.EMPLOYEE_APPROVED).DefaultIfEmpty
                        From e_last_not_ap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e_last_not_ap_title.MASTER).DefaultIfEmpty
                        Where m.IS_OTHER <> -1

            'Dim approveList = From p In query
            '                  From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.p.ID And f.APP_STATUS = 0 _
            '                And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.p.ID And h.APP_STATUS = 0).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty() _
            'From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty()

            'GET LEAVE_SHEET BY EMPLOYEE ID
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            'If _filter.FROM_DATE.HasValue Then
            '    query = query.Where(Function(f) f.p.LEAVE_FROM >= _filter.FROM_DATE)
            'End If
            'If _filter.END_DATE.HasValue Then
            '    query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
            'End If
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
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.BALANCE_NOW.HasValue Then
                query = query.Where(Function(f) f.p.BALANCE_NOW = _filter.BALANCE_NOW)
            End If
            If _filter.NGHIBUCONLAI.HasValue Then
                query = query.Where(Function(f) f.nb.CUR_HAVE = _filter.NGHIBUCONLAI)
            End If

            If IsDate(_filter.FROM_DATE) And IsDate(_filter.END_DATE) Then
                query = query.Where(Function(f) (f.p.LEAVE_FROM >= _filter.FROM_DATE And f.p.LEAVE_FROM <= _filter.END_DATE) Or
                                 (f.p.LEAVE_TO >= _filter.FROM_DATE And f.p.LEAVE_TO <= _filter.END_DATE))
            Else
                If _filter.FROM_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_FROM <= _filter.FROM_DATE And f.p.LEAVE_TO >= _filter.FROM_DATE)
                End If
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
                End If
            End If
            If IsDate(_filter.LEAVE_FROM) Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = CDate(_filter.LEAVE_FROM))
            End If
            If IsDate(_filter.LEAVE_TO) Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If

            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.last_not_app.APP_NOTES.ToLower().Contains(_filter.REASON.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FROM_LOCATION) Then
                query = query.Where(Function(f) f.p.FROM_LOCATION.ToLower().Contains(_filter.FROM_LOCATION.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_LOCATION) Then
                query = query.Where(Function(f) f.p.TO_LOCATION.ToLower().Contains(_filter.TO_LOCATION.ToLower()))
            End If
            If IsNumeric(_filter.NUMBER_KILOMETER) Then
                query = query.Where(Function(f) f.p.NUMBER_KILOMETER = _filter.NUMBER_KILOMETER)
            End If

            If IsNumeric(_filter.STATUS) Then
                query = query.Where(Function(f) f.p.STATUS = _filter.STATUS)
            End If
            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = "[" + p.m.CODE_KH + "] " + p.m.NAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .NOTE = p.p.REASON_LEAVE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .ORDER_NUM = p.ot.ORDERBYID,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", ""),
                                                                       .FROM_LOCATION = p.p.FROM_LOCATION,
                                                                       .TO_LOCATION = p.p.TO_LOCATION,
                                                                       .NUMBER_KILOMETER = p.p.NUMBER_KILOMETER,
                                                                       .TS_SON = p.p.TS_SON,
                                                                       .REASON = p.last_not_app.APP_NOTES,
                                                                       .EMP_APPROVES_NAME = If(p.p.STATUS.Value = 0, p.eeap.EMPLOYEE_CODE & " - " & p.eeap.FULLNAME_VN, If(p.p.STATUS.Value = 1, p.e_last_ap.EMPLOYEE_CODE & " - " & p.e_last_ap.FULLNAME_VN, If(p.p.STATUS.Value = 2, p.e_last_not_ap.EMPLOYEE_CODE & " - " & p.e_last_not_ap.FULLNAME_VN, Nothing))),
                                                                       .PROVINCE_ID = p.p.PROVINCE_ID,
                                                                       .PROVINCE_NAME = p.pro.NAME_VN})

            lst = lst.OrderBy("ORDER_NUM")
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function GetLeaveCTSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        Try
            Dim query = From p In Context.AT_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) F.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PROVINCE_ID).DefaultIfEmpty
                        From nb In Context.AT_COMPENSATORY.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID And F.YEAR = _filter.FROM_DATE.Value.Year).DefaultIfEmpty
                        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = _filter.STATUS _
                            And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = _filter.STATUS).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED And p.STATUS <> 2).DefaultIfEmpty
                        From papp In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = PortalStatus.WaitingForApproval _
                            And f.PROCESS_TYPE.ToUpper = "LEAVE" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = PortalStatus.WaitingForApproval And h.PROCESS_TYPE.ToUpper = "LEAVE").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From eeap In Context.HU_EMPLOYEE.Where(Function(f) f.ID = papp.EMPLOYEE_APPROVED And p.STATUS <> 2).DefaultIfEmpty
                        Where m.IS_OTHER = -1

            'Dim approveList = From p In query
            '                  From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.p.ID And f.APP_STATUS = 0 _
            '                And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.p.ID And h.APP_STATUS = 0).Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty() _
            'From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty()

            'GET LEAVE_SHEET BY EMPLOYEE ID
            If _filter.EMPLOYEE_ID.HasValue Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If IsDate(_filter.FROM_DATE) And IsDate(_filter.END_DATE) Then
                query = query.Where(Function(f) (f.p.LEAVE_FROM >= _filter.FROM_DATE And f.p.LEAVE_FROM <= _filter.END_DATE) Or
                                 (f.p.LEAVE_TO >= _filter.FROM_DATE And f.p.LEAVE_TO <= _filter.END_DATE))
            Else
                If _filter.FROM_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_FROM <= _filter.FROM_DATE And f.p.LEAVE_TO >= _filter.FROM_DATE)
                End If
                If _filter.END_DATE.HasValue Then
                    query = query.Where(Function(f) f.p.LEAVE_TO <= _filter.END_DATE)
                End If
            End If
            If IsDate(_filter.LEAVE_FROM) Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = CDate(_filter.LEAVE_FROM))
            End If
            If IsDate(_filter.LEAVE_TO) Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
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
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                query = query.Where(Function(f) f.s.NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.BALANCE_NOW.HasValue Then
                query = query.Where(Function(f) f.p.BALANCE_NOW = _filter.BALANCE_NOW)
            End If
            If _filter.NGHIBUCONLAI.HasValue Then
                query = query.Where(Function(f) f.nb.CUR_HAVE = _filter.NGHIBUCONLAI)
            End If
            If _filter.LEAVE_FROM.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_FROM = _filter.LEAVE_FROM)
            End If
            If _filter.LEAVE_TO.HasValue Then
                query = query.Where(Function(f) f.p.LEAVE_TO = _filter.LEAVE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                query = query.Where(Function(f) f.m.NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.p.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IMPORT) Then
                query = query.Where(Function(f) f.p.IMPORT = -1)
            End If
            If Not String.IsNullOrEmpty(_filter.REASON) Then
                query = query.Where(Function(f) f.pas.APP_NOTES.ToLower().Contains(_filter.REASON.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.FROM_LOCATION) Then
                query = query.Where(Function(f) f.p.FROM_LOCATION.ToLower().Contains(_filter.FROM_LOCATION.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TO_LOCATION) Then
                query = query.Where(Function(f) f.p.TO_LOCATION.ToLower().Contains(_filter.TO_LOCATION.ToLower()))
            End If
            If IsNumeric(_filter.NUMBER_KILOMETER) Then
                query = query.Where(Function(f) f.p.NUMBER_KILOMETER = _filter.NUMBER_KILOMETER)
            End If

            If IsNumeric(_filter.STATUS) Then
                query = query.Where(Function(f) f.p.STATUS = _filter.STATUS)
            End If
            Dim lst = query.Select(Function(p) New AT_LEAVESHEETDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.s.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.e.ORG_ID,
                                                                       .LEAVE_FROM = p.p.LEAVE_FROM,
                                                                       .LEAVE_TO = p.p.LEAVE_TO,
                                                                       .MANUAL_NAME = p.m.NAME,
                                                                       .MANUAL_ID = p.p.MANUAL_ID,
                                                                       .MORNING_ID = p.m.MORNING_ID,
                                                                       .AFTERNOON_ID = p.m.AFTERNOON_ID,
                                                                       .NOTE = p.p.NOTE,
                                                                       .DAY_NUM = p.p.DAY_NUM,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .STATUS = p.p.STATUS,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .ORDER_NUM = p.ot.ORDERBYID,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                                                       .IMPORT = If(p.p.IMPORT = -1, "x", ""),
                                                                       .FROM_LOCATION = p.p.FROM_LOCATION,
                                                                       .TO_LOCATION = p.p.TO_LOCATION,
                                                                       .NUMBER_KILOMETER = p.p.NUMBER_KILOMETER,
                                                                       .REASON = p.pas.APP_NOTES,
                                                                       .EMP_APPROVES_NAME = p.eeap.EMPLOYEE_CODE & " - " & p.eeap.FULLNAME_VN,
                                                                       .PROVINCE_ID = p.p.PROVINCE_ID,
                                                                       .PROVINCE_NAME = p.pro.NAME_VN})

            lst = lst.OrderBy("ORDER_NUM")
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy thông tin PROCESS_APPROVED_STATUS
    ''' </summary>
    ''' <param name="EmpId"></param>
    ''' <param name="LeaveSheetId"></param>
    ''' <returns></returns>
    Public Function GetProcessApprovedStatusByEmpAndId(ByVal EmpId As Decimal, ByVal LeaveSheetId As Decimal) As PROCESS_APPROVED_STATUS
        Try
            Return Context.PROCESS_APPROVED_STATUS.First(Function(p) p.ID_REGGROUP = LeaveSheetId AndAlso p.EMPLOYEE_ID = EmpId)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
End Class
