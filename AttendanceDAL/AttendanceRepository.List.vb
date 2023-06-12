Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig

Partial Public Class AttendanceRepository
    Dim nvalue_id As Decimal?
    Public Function PRS_COUNT_INOUTKH(ByVal employee_id As Decimal, ByVal year As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.PRS_COUNT_INOUTKH", New With {.P_EMPLOYEE_ID = employee_id,
                                                                                                .P_YEAR = year, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function

    Public Function GET_AT_SETUPELEAVE_YEAR(ByVal year As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_SETUPELEAVE_YEAR", New With {.P_YEAR = year, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function

    Public Function SUM_AT_ADVANCELEAVE_EMP_YEAR(ByVal empId As Decimal, ByVal year As Decimal, ByVal ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.SUM_AT_ADVANCELEAVE_EMP_YEAR", New With {.P_EMP_ID = empId, .P_YEAR = year, .P_ID = ID, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function


    Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.p_employee_app_id = employee_id_app, .P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_STATUS = status, .P_PROCESS_TYPE = process_type, .P_NOTE = notes, .P_ID_REGGROUP = id_reggroup, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function

    Public Function UPDATE_INSERT_AT_SWIPE_DATA(ByVal id As Decimal) As Int32
        Dim Count = 0
        Dim idSwiftData = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = id Select p.ID_AT_SWIPE).FirstOrDefault
        If idSwiftData Is Nothing OrElse idSwiftData = "" Then
            idSwiftData = ""
        End If
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_ID = id, .P_ID_AT_SWIPE = idSwiftData, .P_OUT = cls.OUT_CURSOR}
            Dim dsData As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.UPDATE_INSERT_AT_SWIPE_DATA", obj, False)
            If Not dsData Is Nothing Then
                If Not dsData.Tables(0) Is Nothing Then
                    Count = CDec(dsData.Tables(0).Rows(0)(0))
                End If
            End If
        End Using
        Return Count
    End Function

    Public Function CAL_TIME_TIMESHEET_EMP(ByVal id As Decimal, ByVal log As UserLog) As Int32
        Dim empId = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = id Select p.EMPLOYEE_ID).FirstOrDefault.ToString()
        Dim workingDate = (From p In Context.AT_LATE_COMBACKOUT Where p.ID = id Select p.WORKINGDAY).FirstOrDefault
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_USERNAME = log.Username, .P_FROM_DATE = workingDate, .P_EMPLIST = empId, .P_OUT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_EMP", obj)
            Return Int32.Parse(obj.P_OUT)
        End Using
    End Function

    Public Function INSERT_REGIMES(ByVal ID As Decimal, ByVal MANUAL_ID As Decimal) As Int32
        Dim status = (From p In Context.AT_LEAVESHEET Where p.ID = ID Select CDec(p.STATUS)).FirstOrDefault
        If status = 1 Then


            Dim MANUAL_CODE = (From p In Context.AT_TIME_MANUAL Where p.ID = CDec(MANUAL_ID) Select p.CODE).FirstOrDefault
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
                    Dim obj = New With {.P_ID = ID, .P_REGIME_CODE = REGIME_CODE}
                    Dim store = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_REGIMES", obj)
                End Using
                If MANUAL_CODE.ToUpper.Trim = "TS" Then
                    Using cls As New DataAccess.QueryData
                        Dim dData1 = cls.ExecuteStore("PKG_INS_BUSINESS.INSERT_INS_MATERNITY_MNG",
                                                       New With {.P_ID = ID,
                                                                 .P_REGIME_CODE = REGIME_CODE}, True)
                    End Using
                End If
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function PRS_GETLEAVE_BY_APPROVE1(ByVal param As AT_PORTAL_REG_DTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As New DataTable
            Dim ds As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETLEAVE_BY_APPROVE", New With {.P_EMPLOYEE_ID = param.ID_EMPLOYEE,
                                                                                               .P_STATUS = param.STATUS, .P_YEAR = param.YEAR, .P_CUR = cls.OUT_CURSOR}, False)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        End Using
    End Function
    Public Function PRS_GETDMVS_BY_APPROVE(ByVal param As AT_PORTAL_REG_DTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As New DataTable
            Dim ds As DataSet = cls.ExecuteStore("PKG_AT_PROCESS.PRS_GETDMVS_BY_APPROVE", New With {.P_EMPLOYEE_ID = param.ID_EMPLOYEE,
                                                                                               .P_STATUS = param.STATUS, .P_YEAR = param.YEAR, .P_CUR = cls.OUT_CURSOR}, False)
            If ds IsNot Nothing Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        End Using
    End Function
    Public Function CHECK_CONTRACT(ByVal employee_id As Decimal) As DataTable
        Try

            Dim query = From p In Context.HU_CONTRACT
                        From ce In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ce.TYPE_ID).DefaultIfEmpty
                        Where ot.ID = 6358 And p.EMPLOYEE_ID = employee_id
            Dim lst = query.Select(Function(p) New CONTRACT_DTO With {
                                       .ID = p.p.ID,
                                       .STARTDATE = p.p.START_DATE,
                                       .ENDDATE = p.p.EXPIRE_DATE
                }).ToList
            Return lst.ToTable
        Catch ex As Exception

        End Try
    End Function

    Public Function CHECK_TYPE_BREAK(ByVal type_break_id As Decimal) As DataTable
        Try
            Dim query = From p In Context.AT_TIME_MANUAL
                        From f In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                        From f2 In Context.AT_FML.Where(Function(a) a.ID = p.AFTERNOON_ID).DefaultIfEmpty
                        Where p.ID = type_break_id

            Dim lst = query.Select(Function(p) New AT_TIME_MANUALDTO With {
                                       .ID = p.p.ID,
                                       .IS_LEAVE = p.f.IS_LEAVE,
                                       .IS_LEAVE1 = p.f2.IS_LEAVE,
                                       .CODE1 = p.f.IS_CALHOLIDAY
                }).ToList
            Return lst.ToTable
        Catch ex As Exception

        End Try

    End Function
    Public Function GetLeaveRegistrationListByLM(ByVal _filter As AT_PORTAL_REG_DTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO)
        Try
            Dim query = From p In Context.AT_PORTAL_REG
                        From ce In Context.HUV_AT_PORTAL.Where(Function(f) f.ID_REGGROUP = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From fl In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From s In Context.SE_USER.Where(Function(f) f.USERNAME = p.MODIFIED_BY).DefaultIfEmpty
                        From sh In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = s.EMPLOYEE_CODE).DefaultIfEmpty
                        Where p.STATUS = 0
                        Select New AT_PORTAL_REG_DTO With {
                                                               .ID = p.ID,
                                                               .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                               .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                               .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                               .YEAR = If(p.FROM_DATE.HasValue, p.FROM_DATE.Value.Year, 0),
                                                               .FROM_DATE = ce.FROM_DATE,
                                                               .TO_DATE = ce.TO_DATE,
                                                               .ID_SIGN = p.ID_SIGN,
                                                               .SIGN_CODE = fl.CODE,
                                                               .TOTAL_LEAVE = ce.NVALUE,
                                                               .SIGN_NAME = fl.NAME,
                                                               .STATUS = p.STATUS,
                                                               .STATUS_NAME = ot.NAME_EN,
                                                               .NOTE = p.NOTE,
                                                            .CREATED_DATE = p.CREATED_DATE,
                                                               .DEPARTMENT = o.NAME_VN,
            .JOBTITLE = t.NAME_VN
                                                            }

            If _filter.ID_EMPLOYEE > 0 Then
                query = query.Where(Function(f) f.ID_EMPLOYEE = _filter.ID_EMPLOYEE)
            End If
            If _filter.ID > 0 Then
                query = query.Where(Function(f) f.ID = _filter.ID)
            End If
            If _filter.YEAR > 0 Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.STATUS.HasValue Then
                query = query.Where(Function(f) f.STATUS = _filter.STATUS)
            End If

            If _filter.FROM_DATE.HasValue And _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE >= _filter.FROM_DATE And f.TO_DATE <= _filter.TO_DATE)
            ElseIf _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            ElseIf _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToLower.Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToLower.Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If _filter.NOTE IsNot Nothing Then
                query = query.Where(Function(p) p.NOTE.ToLower.Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToLower.Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.SIGN_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SIGN_NAME.ToLower.Contains(_filter.SIGN_NAME.ToLower()))
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function getSetUpAttEmp(ByVal _filter As SetUpCodeAttDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SetUpCodeAttDTO)
        Try
            Dim query = (From p In Context.AT_SETUP_ATT_EMP
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                         From machine In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MACHINE_ID).DefaultIfEmpty
                         Select New SetUpCodeAttDTO With {
                             .ID = p.ID,
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                             .MACHINE_ID = p.MACHINE_ID,
                             .MACHINE_NAME = machine.NAME_VN,
                             .MACHINE_CODE = machine.CODE,
                             .CODE_ATT = p.CODE_ATT,
                             .APPROVE_DATE = p.APPROVE_DATE,
                             .ORG_ID = e.ORG_ID,
                             .ORG_NAME = org.NAME_VN,
                             .ORG_DESC = org.DESCRIPTION_PATH,
                             .TITLE_ID = e.TITLE_ID,
                             .TITLE_NAME = title.NAME_VN,
                             .NOTE = p.NOTE,
                             .CREATED_BY = p.CREATED_BY,
                             .CREATED_DATE = p.CREATED_DATE,
                             .CREATED_LOG = p.CREATED_LOG,
                             .MODIFIED_BY = p.MODIFIED_BY,
                             .MODIFIED_DATE = p.MODIFIED_DATE,
                             .MODIFIED_LOG = p.MODIFIED_LOG
                             }
                       )
            If Not String.IsNullOrEmpty(_filter.CODE_ATT) Then
                query = query.Where(Function(f) f.CODE_ATT.ToLower().Contains(_filter.CODE_ATT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                query = query.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                query = query.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                query = query.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MACHINE_NAME) Then
                query = query.Where(Function(f) f.MACHINE_NAME.ToLower().Contains(_filter.MACHINE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MACHINE_CODE) Then
                query = query.Where(Function(f) f.MACHINE_CODE.ToLower().Contains(_filter.MACHINE_CODE.ToLower()))
            End If
            If _filter.APPROVE_DATE.HasValue Then
                query = query.Where(Function(f) f.APPROVE_DATE = _filter.APPROVE_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                query = query.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
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

    Public Function InsertSetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_SETUP_ATT_EMP
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_ATT_EMP.EntitySet.Name)
            objData.APPROVE_DATE = objValue.APPROVE_DATE
            objData.EMPLOYEE_ID = objValue.EMPLOYEE_ID
            objData.MACHINE_ID = objValue.MACHINE_ID
            objData.CODE_ATT = objValue.CODE_ATT
            objData.NOTE = objValue.NOTE
            Context.AT_SETUP_ATT_EMP.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifySetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objData = (From p In Context.AT_SETUP_ATT_EMP Where p.ID = objValue.ID).SingleOrDefault
            objData.APPROVE_DATE = objValue.APPROVE_DATE
            objData.EMPLOYEE_ID = objValue.EMPLOYEE_ID
            objData.MACHINE_ID = objValue.MACHINE_ID
            objData.CODE_ATT = objValue.CODE_ATT
            objData.NOTE = objValue.NOTE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
        Try
            _isAvailable = False
            Dim itemInsert As AT_PORTAL_REG
            Dim dayholiday As List(Of AT_HOLIDAYDTO)
            Dim groupid As Decimal? = Nothing
            dayholiday = GetDayHoliday()
            Dim CHECKSHIFT As DataTable = PRS_COUNT_SHIFT(itemRegister.ID_EMPLOYEE)
            Dim I As Integer = 0
            If itemRegister.PROCESS = ATConstant.GSIGNCODE_LEAVE Or itemRegister.PROCESS = ATConstant.GSIGNCODE_WLEO Then
                DeleteRegisterLeavePortal(itemRegister.ID_EMPLOYEE, itemRegister.FROM_DATE,
                                          itemRegister.TO_DATE, New AT_TIME_MANUALDTO With {.ID = itemRegister.ID_SIGN}, itemRegister.PROCESS)
            End If

            While itemRegister.FROM_DATE <= itemRegister.TO_DATE
                If itemRegister.WORK_HARD = 0 Then
                    'kiểm tra vào ngày nghỉ thì k lưu
                    Dim check = (From p In dayholiday Where p.WORKINGDAY <= itemRegister.FROM_DATE And p.WORKINGDAY >= itemRegister.FROM_DATE Select p).ToList.Count
                    If check > 0 Then
                        itemRegister.FROM_DATE = itemRegister.FROM_DATE.Value.AddDays(1)
                        Continue While
                    End If
                    Dim ktra = itemRegister.NVALUE
                    Dim CHECK1 = (From P In CHECKSHIFT.AsEnumerable Where P("WORKINGDAY1") <= itemRegister.FROM_DATE And P("WORKINGDAY1") >= itemRegister.FROM_DATE Select P).ToList.Count
                    If CHECK1 = 0 Then
                        itemRegister.FROM_DATE = itemRegister.FROM_DATE.Value.AddDays(1)
                        Continue While
                    End If
                End If
                itemInsert = New AT_PORTAL_REG
                itemInsert.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_REG.EntitySet.Name)
                I += 1
                If I = 1 Then
                    groupid = itemInsert.ID
                End If

                itemInsert.ID_EMPLOYEE = itemRegister.ID_EMPLOYEE
                itemInsert.ID_SIGN = itemRegister.ID_SIGN
                itemInsert.FROM_DATE = itemRegister.FROM_DATE
                itemInsert.TO_DATE = itemRegister.FROM_DATE
                itemInsert.DAYIN_KH = itemRegister.DAYIN_KH
                itemInsert.DAYOUT_KH = itemRegister.DAYOUT_KH
                itemInsert.WORK_DAY = itemRegister.WORK_HARD
                If itemRegister.FROM_HOUR.HasValue Then
                    itemInsert.FROM_HOUR = itemRegister.FROM_DATE.Value.Date.AddMinutes(itemRegister.FROM_HOUR.Value.TimeOfDay.TotalMinutes)
                End If
                If itemRegister.TO_HOUR.HasValue Then
                    itemInsert.TO_HOUR = itemRegister.FROM_DATE.Value.Date.AddMinutes(itemRegister.TO_HOUR.Value.TimeOfDay.TotalMinutes)
                    If itemInsert.TO_HOUR < itemInsert.FROM_HOUR Then
                        itemInsert.TO_HOUR = itemInsert.TO_HOUR.Value.AddDays(1)
                    End If
                End If
                If itemInsert.FROM_HOUR.HasValue AndAlso itemInsert.TO_HOUR.HasValue Then
                    itemInsert.HOURCOUNT = (itemInsert.TO_HOUR.Value - itemInsert.FROM_HOUR.Value).TotalHours
                End If
                'itemInsert.TO_HOUR = itemRegister.TO_HOUR
                itemInsert.NOTE = itemRegister.NOTE
                itemInsert.NOTE_AT = itemRegister.NOTE_AT
                itemInsert.REGDATE = Date.Now
                itemInsert.STATUS = itemRegister.STATUS
                itemInsert.CREATED_BY = log.Username.ToUpper
                itemInsert.CREATED_DATE = Date.Now
                itemInsert.CREATED_LOG = log.ComputerName
                itemInsert.NVALUE = itemRegister.NVALUE
                itemInsert.MODIFIED_BY = itemRegister.MODIFIED_BY
                itemInsert.MODIFIED_DATE = Date.Now
                itemInsert.SVALUE = itemRegister.PROCESS
                itemInsert.NVALUE_ID = itemRegister.NVALUE_ID
                itemInsert.ID_REGGROUP = groupid
                itemInsert.IS_NB = itemRegister.ID_NB
                Context.AT_PORTAL_REG.AddObject(itemInsert)

                itemRegister.FROM_DATE = itemRegister.FROM_DATE.Value.AddDays(1)
            End While
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "InsertPortalRegister")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function ModifyPortalRegList(ByVal obj As AT_PORTAL_REG_DTO, ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
        _isAvailable = False
        Try
            Dim lst = (From p In Context.AT_PORTAL_REG Where p.ID_REGGROUP = obj.ID).ToList()
            For index = 0 To lst.Count - 1
                Context.AT_PORTAL_REG.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            InsertPortalRegister(itemRegister, log)

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "ModifyPortalRegList")
            Throw ex
        Finally
            _isAvailable = True
        End Try
        Return True

    End Function

    Public Function DeleteSetUpAttEmp(ByVal lstID As List(Of Decimal)) As Boolean
        Try
            Dim lst = (From p In Context.AT_SETUP_ATT_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.AT_SETUP_ATT_EMP.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function CheckValidateMACC(ByVal obj As SetUpCodeAttDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_SETUP_ATT_EMP Where p.MACHINE_ID = obj.MACHINE_ID And p.CODE_ATT = obj.CODE_ATT And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckValidateAPPROVE_DATE(ByVal obj As SetUpCodeAttDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_SETUP_ATT_EMP Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And p.MACHINE_ID = obj.MACHINE_ID And p.APPROVE_DATE = obj.APPROVE_DATE And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#Region "wifi-gps"
    Public Function GetSetupWifi(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = _filter.USERNAME.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_SETUP_WIFI
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = _filter.USERNAME.ToUpper)

            Dim lst = query.Select(Function(p) New AT_SETUP_WIFI_GPS_DTO With {
                                       .ID = p.p.ID,
                                       .IP = p.p.IP,
                                       .NAME_VN = p.p.NAME_VN,
                                       .NAME_WIFI = p.p.NAME_WIFI,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH
                                       })

            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_WIFI) Then
                lst = lst.Where(Function(f) f.NAME_WIFI.ToLower().Contains(_filter.NAME_WIFI.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IP) Then
                lst = lst.Where(Function(f) f.IP.ToLower().Contains(_filter.IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
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
    Public Function GetSetupGPSByID(ByVal _filter As AT_SETUP_WIFI_GPS_DTO) As AT_SETUP_WIFI_GPS_DTO
        Try
            Dim query = From p In Context.AT_SETUP_GPS
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID

            Dim lst = query.Select(Function(p) New AT_SETUP_WIFI_GPS_DTO With {
                                       .ID = p.p.ID,
                                       .NAME = p.p.NAME,
                                       .ADDRESS = p.p.ADDRESS,
                                       .LAT_VD = p.p.LAT_VD,
                                       .LONG_KD = p.p.LONG_KD,
                                       .RADIUS = p.p.RADIUS,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH
                                       })
            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetSetupGPS(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = _filter.USERNAME.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_SETUP_GPS
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = _filter.USERNAME.ToUpper)

            Dim lst = query.Select(Function(p) New AT_SETUP_WIFI_GPS_DTO With {
                                       .ID = p.p.ID,
                                       .NAME = p.p.NAME,
                                       .ADDRESS = p.p.ADDRESS,
                                       .LAT_VD = p.p.LAT_VD,
                                       .LONG_KD = p.p.LONG_KD,
                                       .RADIUS = p.p.RADIUS,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH
                                       })

            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS) Then
                lst = lst.Where(Function(f) f.NAME_WIFI.ToLower().Contains(_filter.ADDRESS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.LAT_VD) Then
                lst = lst.Where(Function(f) f.LAT_VD.ToLower().Contains(_filter.LAT_VD.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.LONG_KD) Then
                lst = lst.Where(Function(f) f.LONG_KD.ToLower().Contains(_filter.LONG_KD.ToLower()))
            End If
            If _filter.RADIUS IsNot Nothing Then
                lst = lst.Where(Function(f) f.RADIUS = _filter.RADIUS)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
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
    Public Function InsertSetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_WIFI
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_WIFI.EntitySet.Name)
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_WIFI = objTitle.NAME_WIFI
            objTitleData.IP = objTitle.IP
            objTitleData.ACTFLG = "A"
            objTitleData.ORG_ID = objTitle.ORG_ID

            Context.AT_SETUP_WIFI.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function InsertSetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_GPS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_GPS.EntitySet.Name)
            objTitleData.NAME = objTitle.NAME
            objTitleData.ADDRESS = objTitle.ADDRESS
            objTitleData.LAT_VD = objTitle.LAT_VD
            objTitleData.LONG_KD = objTitle.LONG_KD
            objTitleData.RADIUS = objTitle.RADIUS
            objTitleData.ACTFLG = "A"
            objTitleData.ORG_ID = objTitle.ORG_ID

            Context.AT_SETUP_GPS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function ValidateWIFI_GPS(ByVal org_id As Decimal, ByVal id As Decimal, ByVal flag As String) As Decimal

        Try
            Dim re = 0
            If flag = "wifi" Then
                If id = 0 Then
                    Return (From p In Context.AT_SETUP_WIFI Where p.ORG_ID = org_id Select p).Count
                Else
                    Return (From p In Context.AT_SETUP_WIFI Where p.ORG_ID = org_id And p.ID <> id Select p).Count
                End If
            Else
                If id = 0 Then
                    Return (From p In Context.AT_SETUP_GPS Where p.ORG_ID = org_id Select p).Count
                Else
                    Return (From p In Context.AT_SETUP_GPS Where p.ORG_ID = org_id And p.ID <> id Select p).Count
                End If
            End If

            Return re
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function ModifySetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_WIFI With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_WIFI Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.IP = objTitle.IP
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.NAME_WIFI = objTitle.NAME_WIFI
            objTitleData.ORG_ID = objTitle.ORG_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function ModifySetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_GPS With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_GPS Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.ADDRESS = objTitle.ADDRESS
            objTitleData.NAME = objTitle.NAME
            objTitleData.LAT_VD = objTitle.LAT_VD
            objTitleData.LONG_KD = objTitle.LONG_KD
            objTitleData.RADIUS = objTitle.RADIUS
            objTitleData.ORG_ID = objTitle.ORG_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveSetupWifi(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_WIFI)
        Try
            lstData = (From p In Context.AT_SETUP_WIFI Where lstID.Contains(p.ID)).ToList
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
    Public Function ActiveSetupGPS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_GPS)
        Try
            lstData = (From p In Context.AT_SETUP_GPS Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteSetupWifi(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SETUP_WIFI)
        Try
            lstHolidayData = (From p In Context.AT_SETUP_WIFI Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SETUP_WIFI.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupGPS(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SETUP_GPS)
        Try
            lstHolidayData = (From p In Context.AT_SETUP_GPS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SETUP_GPS.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region


#Region "List Holiday"
    Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY
            Dim lst = query.Select(Function(p) New AT_HOLIDAYDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .YEAR = p.YEAR,
                                       .NOTE = p.NOTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .OFFDAY = p.OFFDAY})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.YEAR <> 0 Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
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

    Public Function InsertHoliday(ByVal objTitle As AT_HOLIDAYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.OFFDAY = objTitle.OFFDAY
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function


    Public Function ValidateHoliday(ByVal _validate As AT_HOLIDAYDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.WORKINGDAY = _validate.WORKINGDAY _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
                             Where p.WORKINGDAY = _validate.WORKINGDAY).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function ModifyHoliday(ByVal objTitle As AT_HOLIDAYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.OFFDAY = objTitle.OFFDAY
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY)
        Try
            lstData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteHoliday(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function




    Public Function GetDayHoliday() As List(Of AT_HOLIDAYDTO)
        Dim query = From p In Context.AT_HOLIDAY
        Dim lst = query.Select(Function(p) New AT_HOLIDAYDTO With {
                                   .ID = p.ID,
        .WORKINGDAY = p.WORKINGDAY
                })
        Return lst.ToList
    End Function
    Public Function GetHoliday_Hose(ByVal _filter As AT_HOLIDAYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY
                        From s In Context.ATV_HOLIDAY_HOSE.Where(Function(f) f.ID = p.ID)
            Dim lst = query.Select(Function(p) New AT_HOLIDAYDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_EN = p.p.NAME_EN,
                                       .NAME_VN = p.p.NAME_VN,
                                       .WORKINGDAY = p.p.WORKINGDAY,
                                       .YEAR = p.p.YEAR,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .FROMDATE = p.s.FROMDATE,
                                       .TODATE = p.s.TODATE,
                                       .IS_SA = If(p.s.IS_SA = 0, False, True),
                                       .IS_SUN = If(p.s.IS_SU = 0, False, True),
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
            End If
            If _filter.YEAR <> 0 Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
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
    Public Function InsertHoliday_Hose(ByVal objTitle As AT_HOLIDAYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim Id As Integer
        Dim IS_SA As Integer
        Dim IS_SUN As Integer
        If objTitle.IS_SA = True Then
            IS_SA = -1
        Else
            IS_SA = 0
        End If
        If objTitle.IS_SUN = True Then
            IS_SUN = -1
        Else
            IS_SUN = 0
        End If
        Try
            Id = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY.EntitySet.Name)
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_AT_LIST.INSERT_AT_HOLIDAY_HOSE",
                                 New With {.P_ID = Id,
                                           .P_CODE = objTitle.CODE,
                                           .P_NAME = objTitle.NAME_VN,
                                           .P_FROMDATE = objTitle.FROMDATE,
                                           .P_TODATE = objTitle.TODATE,
                                           .P_YEAR = objTitle.YEAR,
                                           .P_NOTE = objTitle.NOTE,
                                           .P_ACTFLG = objTitle.ACTFLG,
                                           .P_ISSA = IS_SA,
                                           .P_ISSU = IS_SUN})
            End Using

            Context.SaveChanges(log)
            gID = Id
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function DeleteHoliday_Hose(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList

            Using cls As New DataAccess.QueryData
                For index = 0 To lstHolidayData.Count - 1
                    cls.ExecuteStore("PKG_AT_LIST.DELETE_AT_HOLIDAY_HOSE",
                                 New With {.P_ID = lstHolidayData(index).ID})
                Next
            End Using
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function ActiveHoliday_Hose(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY)
        Try
            lstData = (From p In Context.AT_HOLIDAY Where lstID.Contains(p.ID)).ToList
            Using cls As New DataAccess.QueryData
                For index = 0 To lstData.Count - 1
                    cls.ExecuteStore("PKG_AT_LIST.ACTIVE_AT_HOLIDAY_HOSE",
                                 New With {.P_ID = lstData(index).ID,
                                           .P_ACTIVE = bActive})
                Next
            End Using
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ValidateHoliday_Hose(ByVal _validate As AT_HOLIDAYDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.WORKINGDAY.HasValue Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where (p.WORKINGDAY >= _validate.TODATE _
                                   And p.WORKINGDAY <= _validate.FROMDATE) _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY
                             Where (p.WORKINGDAY >= _validate.TODATE _
                                   And p.WORKINGDAY <= _validate.FROMDATE)).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_HOLIDAY
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "List Holiday gerenal"
    Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY_GENERAL

            Dim lst = query.Select(Function(p) New AT_HOLIDAY_GENERALDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .WORKINGDAY = p.WORKINGDAY,
                                       .NOTE = p.NOTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.WORKINGDAY.HasValue Then
                lst = lst.Where(Function(f) f.WORKINGDAY = _filter.WORKINGDAY)
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

    Public Function InsertHolidayGerenal(ByVal objTitle As AT_HOLIDAY_GENERALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_GENERAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY_GENERAL.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY_GENERAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateHolidayGerenal(ByVal _validate As AT_HOLIDAY_GENERALDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_HOLIDAY_GENERAL
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyHolidayGerenal(ByVal objTitle As AT_HOLIDAY_GENERALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_GENERAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY_GENERAL Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.WORKINGDAY = objTitle.WORKINGDAY
            objTitleData.NOTE = objTitle.NOTE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY_GENERAL)
        Try
            lstData = (From p In Context.AT_HOLIDAY_GENERAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY_GENERAL)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY_GENERAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY_GENERAL.DeleteObject(lstHolidayData(index))
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

#Region "Danh mục kiểu công"
    Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        Dim query = From p In Context.AT_TIME_MANUAL
                    Where p.ACTFLG = "A" _
                    And p.CODE = "RDT" Or p.CODE = "RVS"
        Dim lst = query.Select(Function(p) New AT_TIME_MANUALDTO With {
                                   .ID = p.ID,
                                   .CODE = p.CODE,
                                   .NAME_VN = p.NAME})
        Return lst.ToList
    End Function

    Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        Try
            Dim query = From p In Context.AT_FML
            Dim lst = query.Select(Function(p) New AT_FMLDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_EN = p.NAME_EN,
                                       .NAME_VN = p.NAME_VN,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .NOTE = p.NOTE,
                                       .IS_LEAVE = p.IS_LEAVE,
                                       .IS_CALHOLIDAY = p.IS_CALHOLIDAY,
                                       .DAY_NUM = p.DAY_NUM,
                                       .IS_SALARY = p.IS_SALARY,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .IS_REG_SHIFT = p.IS_REG_SHIFT,
                                       .IS_BHXH = p.IS_BHXH,
                                       .CODE_KH = p.CODE_KH,
                                       .IS_NOTIFY = p.IS_NOTIFY})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_FML(ByVal objTitle As AT_FMLDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_FML
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_FML.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.IS_CALHOLIDAY = objTitle.IS_CALHOLIDAY
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_LEAVE = objTitle.IS_LEAVE
            objTitleData.IS_REG_SHIFT = objTitle.IS_REG_SHIFT
            objTitleData.DAY_NUM = objTitle.DAY_NUM
            objTitleData.IS_SALARY = objTitle.IS_SALARY
            objTitleData.IS_BHXH = objTitle.IS_BHXH
            objTitleData.IS_NOTIFY = objTitle.IS_NOTIFY
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CODE_KH = objTitle.CODE_KH
            Context.AT_FML.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_FML(ByVal _validate As AT_FMLDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_FML
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_FML
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_FML
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_FML
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_FML(ByVal objTitle As AT_FMLDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_FML With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_FML Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.IS_CALHOLIDAY = objTitle.IS_CALHOLIDAY
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.IS_LEAVE = objTitle.IS_LEAVE
            objTitleData.IS_REG_SHIFT = objTitle.IS_REG_SHIFT
            objTitleData.DAY_NUM = objTitle.DAY_NUM
            objTitleData.IS_SALARY = objTitle.IS_SALARY
            objTitleData.IS_BHXH = objTitle.IS_BHXH
            objTitleData.IS_NOTIFY = objTitle.IS_NOTIFY
            'objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CODE_KH = objTitle.CODE_KH
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_FML)
        Try
            lstData = (From p In Context.AT_FML Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_FML)
        Try
            lstHolidayData = (From p In Context.AT_FML Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_FML.DeleteObject(lstHolidayData(index))
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

#Region "Quy định ca"
    Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        Try

            Dim query = From p In Context.AT_GSIGN
            Dim lst = query.Select(Function(p) New AT_GSIGNDTO With {
                                      .ID = p.ID,
                                      .CODE = p.CODE,
                                      .NAME_VN = p.NAME_VN,
                                      .SOONEST_IN = p.SOONEST_IN,
                                      .LATEST_OUT = p.LATEST_OUT,
                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.CREATED_BY,
                                      .CREATED_DATE = p.CREATED_DATE,
                                      .CREATED_LOG = p.CREATED_LOG,
                                      .MODIFIED_BY = p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If _filter.SOONEST_IN.HasValue Then
                lst = lst.Where(Function(f) f.SOONEST_IN = _filter.SOONEST_IN)
            End If
            If _filter.LATEST_OUT.HasValue Then
                lst = lst.Where(Function(f) f.LATEST_OUT = _filter.LATEST_OUT)
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

    Public Function InsertAT_GSIGN(ByVal objTitle As AT_GSIGNDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_GSIGN
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_GSIGN.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.SOONEST_IN = objTitle.SOONEST_IN
            objTitleData.LATEST_OUT = objTitle.LATEST_OUT
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_GSIGN.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_GSIGN(ByVal _validate As AT_GSIGNDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_GSIGN
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_GSIGN
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_GSIGN
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_GSIGN
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_GSIGN(ByVal objTitle As AT_GSIGNDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_GSIGN With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_GSIGN Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.SOONEST_IN = objTitle.SOONEST_IN
            objTitleData.LATEST_OUT = objTitle.LATEST_OUT
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_GSIGN)
        Try
            lstData = (From p In Context.AT_GSIGN Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_GSIGN)
        Try
            lstHolidayData = (From p In Context.AT_GSIGN Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_GSIGN.DeleteObject(lstHolidayData(index))
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

#Region "Quy định đi muộn về sớm"
    Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        Try

            Dim query = From p In Context.AT_DMVS
                        From t1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT1 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT2 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t3 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT3 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t4 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT4 And f.TYPE_ID = 1035).DefaultIfEmpty
                        From t5 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LOAIPHAT5 And f.TYPE_ID = 1035).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_DMVSDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME_VN,
                                       .MUC1 = p.p.MUC1,
                                       .LOAIPHAT1 = p.p.LOAIPHAT1,
                                       .LOAIPHAT1_NAME = p.t1.NAME_VN,
                                       .GIATRI1 = p.p.GIATRI1,
                                       .MUC2 = p.p.MUC2,
                                       .LOAIPHAT2 = p.p.LOAIPHAT2,
                                       .LOAIPHAT2_NAME = p.t2.NAME_VN,
                                       .GIATRI2 = p.p.GIATRI2,
                                       .MUC3 = p.p.MUC3,
                                       .LOAIPHAT3 = p.p.LOAIPHAT3,
                                       .LOAIPHAT3_NAME = p.t3.NAME_VN,
                                       .GIATRI3 = p.p.GIATRI3,
                                       .MUC4 = p.p.MUC4,
                                       .LOAIPHAT4 = p.p.LOAIPHAT4,
                                       .LOAIPHAT4_NAME = p.t4.NAME_VN,
                                       .GIATRI4 = p.p.GIATRI4,
                                       .MUC5 = p.p.MUC5,
                                       .LOAIPHAT5 = p.p.LOAIPHAT5,
                                       .LOAIPHAT5_NAME = p.t5.NAME_VN,
                                       .GIATRI5 = p.p.GIATRI5,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If _filter.MUC1.HasValue Then
                lst = lst.Where(Function(f) f.MUC1 = _filter.MUC1)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT1_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT1_NAME.ToLower()))
            End If
            If _filter.GIATRI1.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI1 = _filter.GIATRI1)
            End If
            If _filter.MUC2.HasValue Then
                lst = lst.Where(Function(f) f.MUC2 = _filter.MUC2)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT2_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT2_NAME.ToLower()))
            End If
            If _filter.GIATRI2.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI2 = _filter.GIATRI2)
            End If
            If _filter.MUC3.HasValue Then
                lst = lst.Where(Function(f) f.MUC3 = _filter.MUC3)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT3_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT3_NAME.ToLower()))
            End If
            If _filter.GIATRI3.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI3 = _filter.GIATRI3)
            End If
            If _filter.MUC4.HasValue Then
                lst = lst.Where(Function(f) f.MUC4 = _filter.MUC4)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT4_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT4_NAME.ToLower()))
            End If
            If _filter.GIATRI4.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI4 = _filter.GIATRI4)
            End If
            If _filter.MUC5.HasValue Then
                lst = lst.Where(Function(f) f.MUC5 = _filter.MUC5)
            End If
            If Not String.IsNullOrEmpty(_filter.LOAIPHAT5_NAME) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT5_NAME.ToLower()))
            End If
            If _filter.GIATRI5.HasValue Then
                lst = lst.Where(Function(f) f.GIATRI5 = _filter.GIATRI5)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.LOAIPHAT1_NAME.ToLower()))
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

    Public Function InsertAT_DMVS(ByVal objTitle As AT_DMVSDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_DMVS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_DMVS.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.MUC1 = objTitle.MUC1
            objTitleData.LOAIPHAT1 = objTitle.LOAIPHAT1
            objTitleData.GIATRI1 = objTitle.GIATRI1
            objTitleData.MUC2 = objTitle.MUC2
            objTitleData.LOAIPHAT2 = objTitle.LOAIPHAT2
            objTitleData.GIATRI2 = objTitle.GIATRI2
            objTitleData.MUC3 = objTitle.MUC3
            objTitleData.LOAIPHAT3 = objTitle.LOAIPHAT3
            objTitleData.GIATRI3 = objTitle.GIATRI3
            objTitleData.MUC4 = objTitle.MUC4
            objTitleData.LOAIPHAT4 = objTitle.LOAIPHAT4
            objTitleData.GIATRI4 = objTitle.GIATRI4
            objTitleData.MUC5 = objTitle.MUC5
            objTitleData.LOAIPHAT5 = objTitle.LOAIPHAT5
            objTitleData.GIATRI5 = objTitle.GIATRI5
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_DMVS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_DMVS(ByVal _validate As AT_DMVSDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_DMVS
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_DMVS
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_DMVS
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_DMVS
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_DMVS(ByVal objTitle As AT_DMVSDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_DMVS With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_DMVS Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.MUC1 = objTitle.MUC1
            objTitleData.LOAIPHAT1 = objTitle.LOAIPHAT1
            objTitleData.GIATRI1 = objTitle.GIATRI1
            objTitleData.MUC2 = objTitle.MUC2
            objTitleData.LOAIPHAT2 = objTitle.LOAIPHAT2
            objTitleData.GIATRI2 = objTitle.GIATRI2
            objTitleData.MUC3 = objTitle.MUC3
            objTitleData.LOAIPHAT3 = objTitle.LOAIPHAT3
            objTitleData.GIATRI3 = objTitle.GIATRI3
            objTitleData.MUC4 = objTitle.MUC4
            objTitleData.LOAIPHAT4 = objTitle.LOAIPHAT4
            objTitleData.GIATRI4 = objTitle.GIATRI4
            objTitleData.MUC5 = objTitle.MUC5
            objTitleData.LOAIPHAT5 = objTitle.LOAIPHAT5
            objTitleData.GIATRI5 = objTitle.GIATRI5
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_DMVS)
        Try
            lstData = (From p In Context.AT_DMVS Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_DMVS)
        Try
            lstHolidayData = (From p In Context.AT_DMVS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_DMVS.DeleteObject(lstHolidayData(index))
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

#Region "Danh mục ca làm việc"
    Public Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using
            Dim userNameID As Decimal = _filter.SHIFT_DAY
            Dim check As String = "Dùng chung"
            Dim lstID_Access = New List(Of Decimal)
            'lstOrgID = (From p In Context.SE_USER_ORG_ACCESS
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
            '            Where p.USER_ID = userNameID
            '            Select p.ORG_ID).ToList
            lstID_Access = (From p In Context.AT_SHIFT_ORG_ACCESS
                            From soa In Context.SE_USER_ORG_ACCESS.Where(Function(f) p.ORG_ID = f.ORG_ID)
                            From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                            Where soa.USER_ID = userNameID
                            Select p.AT_SHIFT_ID).Distinct.ToList
            Dim query = From p In Context.AT_SHIFT
                        From olg In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From Dsvm In Context.AT_DMVS.Where(Function(f) f.ID = p.PENALIZEA).DefaultIfEmpty
                        From mn In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
            'From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim lst = query.Select(Function(p) New AT_SHIFTDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME_VN,
                                       .NAME_EN = p.p.NAME_EN,
                                       .MANUAL_ID = p.p.MANUAL_ID,
                                       .MANUAL_CODE = p.mn.CODE,
                                       .MANUAL_NAME = p.mn.NAME,
                                       .PENALIZEA = p.p.PENALIZEA,
                                       .PENALIZEA_NAME = p.Dsvm.NAME_VN,
                                       .SATURDAY = p.p.SATURDAY,
                                       .SUNDAY = p.p.SUNDAY,
                                       .HOURS_START = p.p.HOURS_START,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .START_MID_HOURS = p.p.START_MID_HOURS,
                                       .END_MID_HOURS = p.p.END_MID_HOURS,
                                       .HOURS_STAR_CHECKIN = p.p.HOURS_STAR_CHECKIN,
                                       .HOURS_STAR_CHECKOUT = p.p.HOURS_STAR_CHECKOUT,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .ORG_NAME = If(p.p.ORG_ID = -1, check, p.olg.NAME_VN),
                                       .NOTE = p.p.NOTE,
                                       .IS_NOON = p.p.IS_NOON,
                                       .MINHOUSER = p.p.MINHOURS,
                                       .ORG_ID = p.p.ORG_ID,
                                       .SHIFT_DAY = p.p.SHIFT_DAY,
                                       .IS_HOURS_STOP = p.p.IS_HOURS_STOP,
                                       .IS_MID_END = p.p.IS_MID_END,
                                       .IS_HOURS_CHECKOUT = p.p.IS_HOURS_CHECKOUT,
                                       .IS_HOURS_STOP_NAME = If(p.p.IS_HOURS_STOP = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_MID_END_NAME = If(p.p.IS_MID_END = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_HOURS_CHECKOUT_NAME = If(p.p.IS_HOURS_CHECKOUT = 0, "Không áp dụng", "Áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .LATE_HOUR = p.p.LATE_HOUR,
                                       .EARLY_HOUR = p.p.EARLY_HOUR,
                                       .IS_TOMORROW = p.p.IS_TOMORROW,
                                       .IS_TOMORROW_NAME = If(p.p.IS_TOMORROW = 0, "Không áp dụng", "Áp dụng"),
                                       .SHIFT_HOUR = p.p.SHIFT_HOUR,
                                       .OT_HOUR_MIN = p.p.OT_HOUR_MIN,
                                       .OT_HOUR_MAX = p.p.OT_HOUR_MAX,
                                       .IS_HOLYDAY_SHIFT = p.p.IS_HOLYDAY_SHIFT,
                                       .IS_HOLYDAY_SHIFT_NAME = If(p.p.IS_HOLYDAY_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_MORNING_SHIFT = p.p.IS_MORNING_SHIFT,
                                       .IS_MORNING_SHIFT_NAME = If(p.p.IS_MORNING_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_AFTERNOON_SHIFT = p.p.IS_AFTERNOON_SHIFT,
                                       .IS_AFTERNOON_SHIFT_NAME = If(p.p.IS_AFTERNOON_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_MIDDLE_SHIFT = p.p.IS_MIDDLE_SHIFT,
                                       .IS_MIDDLE_SHIFT_NAME = If(p.p.IS_MIDDLE_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_BROKEN_SHIFT = p.p.IS_BROKEN_SHIFT,
                                       .IS_BROKEN_SHIFT_NAME = If(p.p.IS_BROKEN_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_TIME_SHIFT = p.p.IS_TIME_SHIFT,
                                       .IS_TIME_SHIFT_NAME = If(p.p.IS_TIME_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_CH_SUPPORT_SHIFT = p.p.IS_CH_SUPPORT_SHIFT,
                                       .IS_CH_SUPPORT_SHIFT_NAME = If(p.p.IS_CH_SUPPORT_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_SATURDAY_SHIFT = p.p.IS_SATURDAY_SHIFT,
                                       .IS_SATURDAY_SHIFT_NAME = If(p.p.IS_SATURDAY_SHIFT = 0, "Không áp dụng", "Áp dụng"),
                                       .IS_SUNDAY_SHIFT = p.p.IS_SUNDAY_SHIFT,
                                       .IS_SUNDAY_SHIFT_NAME = If(p.p.IS_SUNDAY_SHIFT = 0, "Không áp dụng", "Áp dụng")}).AsQueryable

            '.ORG_NAME = If(p.p.ORG_ID.Value = -1, "Dùng chung", p.o.NAME_EN),
            'hoavv add 3rc

            'If lstID_Access.Count > 0 AndAlso log.Username.ToUpper.ToUpper <> "ADMIN" Then
            '    lst = lst.Where(Function(f) (lstID_Access.Contains(f.ID)) Or f.CODE = "OFF" Or f.CODE = "L")
            'End If

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_EN) Then
                lst = lst.Where(Function(f) f.NAME_EN.ToLower().Contains(_filter.NAME_EN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                lst = lst.Where(Function(f) f.MANUAL_NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_CODE) Then
                lst = lst.Where(Function(f) f.MANUAL_CODE.ToLower().Contains(_filter.MANUAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PENALIZEA_NAME) Then
                lst = lst.Where(Function(f) f.PENALIZEA_NAME.ToLower().Contains(_filter.PENALIZEA_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SATURDAY_NAME) Then
                lst = lst.Where(Function(f) f.PENALIZEA_NAME.ToLower().Contains(_filter.SATURDAY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SATURDAY_CODE) Then
                lst = lst.Where(Function(f) f.SATURDAY_CODE.ToLower().Contains(_filter.SATURDAY_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SUNDAY_NAME) Then
                lst = lst.Where(Function(f) f.SUNDAY_NAME.ToLower().Contains(_filter.SUNDAY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SUNDAY_CODE) Then
                lst = lst.Where(Function(f) f.SUNDAY_CODE.ToLower().Contains(_filter.SUNDAY_CODE.ToLower()))
            End If
            If _filter.HOURS_START.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_START = _filter.HOURS_START)
            End If
            If _filter.HOURS_STOP.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_STOP = _filter.HOURS_STOP)
            End If
            If _filter.HOURS_STAR_CHECKIN.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_STAR_CHECKIN = _filter.HOURS_STAR_CHECKIN)
            End If
            If _filter.HOURS_STAR_CHECKOUT.HasValue Then
                lst = lst.Where(Function(f) f.HOURS_STAR_CHECKOUT = _filter.HOURS_STAR_CHECKOUT)
            End If
            If _filter.MINHOUSER > 0 Then
                lst = lst.Where(Function(f) f.MINHOUSER = _filter.MINHOUSER)
            End If

            If _filter.LATE_HOUR.HasValue Then
                lst = lst.Where(Function(f) f.LATE_HOUR = _filter.LATE_HOUR)
            End If
            If _filter.EARLY_HOUR.HasValue Then
                lst = lst.Where(Function(f) f.EARLY_HOUR = _filter.EARLY_HOUR)
            End If
            If _filter.SHIFT_HOUR > 0 Then
                lst = lst.Where(Function(f) f.SHIFT_HOUR = _filter.SHIFT_HOUR)
            End If
            If _filter.OT_HOUR_MAX > 0 Then
                lst = lst.Where(Function(f) f.OT_HOUR_MAX = _filter.OT_HOUR_MAX)
            End If
            If _filter.OT_HOUR_MIN > 0 Then
                lst = lst.Where(Function(f) f.OT_HOUR_MIN = _filter.OT_HOUR_MIN)
            End If

            If Not String.IsNullOrEmpty(_filter.IS_TOMORROW_NAME) Then
                lst = lst.Where(Function(f) f.IS_TOMORROW_NAME.ToLower().Contains(_filter.IS_TOMORROW_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_SATURDAY_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_SATURDAY_SHIFT_NAME.ToLower().Contains(_filter.IS_SATURDAY_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_SUNDAY_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_SUNDAY_SHIFT_NAME.ToLower().Contains(_filter.IS_SUNDAY_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_HOLYDAY_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_HOLYDAY_SHIFT_NAME.ToLower().Contains(_filter.IS_HOLYDAY_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_MORNING_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_MORNING_SHIFT_NAME.ToLower().Contains(_filter.IS_MORNING_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_AFTERNOON_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_AFTERNOON_SHIFT_NAME.ToLower().Contains(_filter.IS_AFTERNOON_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_MIDDLE_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_MIDDLE_SHIFT_NAME.ToLower().Contains(_filter.IS_MIDDLE_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_BROKEN_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_BROKEN_SHIFT_NAME.ToLower().Contains(_filter.IS_BROKEN_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_TIME_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_TIME_SHIFT_NAME.ToLower().Contains(_filter.IS_TIME_SHIFT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.IS_CH_SUPPORT_SHIFT_NAME) Then
                lst = lst.Where(Function(f) f.IS_CH_SUPPORT_SHIFT_NAME.ToLower().Contains(_filter.IS_CH_SUPPORT_SHIFT_NAME.ToLower()))
            End If


            'lst = lst.OrderBy(Sorts)
            lst = lst.OrderByDescending(Function(f) If(f.CREATED_DATE.HasValue, f.CREATED_DATE, Date.MinValue))
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_SHIFT(ByVal objTitle As AT_SHIFTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SHIFT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SHIFT.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            'objTitleData.APPLY_LAW = objTitle.APPLY_LAW
            'objTitleData.PENALIZEA = objTitle.PENALIZEA
            objTitleData.SATURDAY = objTitle.SATURDAY
            objTitleData.SUNDAY = objTitle.SUNDAY
            objTitleData.MANUAL_ID = objTitle.MANUAL_ID
            objTitleData.HOURS_START = objTitle.HOURS_START
            objTitleData.HOURS_STOP = objTitle.HOURS_STOP
            objTitleData.END_MID_HOURS = objTitle.END_MID_HOURS
            objTitleData.START_MID_HOURS = objTitle.START_MID_HOURS
            objTitleData.HOURS_STAR_CHECKIN = objTitle.HOURS_STAR_CHECKIN
            objTitleData.HOURS_STAR_CHECKOUT = objTitle.HOURS_STAR_CHECKOUT
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_NOON = objTitle.IS_NOON
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            'hoaivv add
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.SHIFT_DAY = objTitle.SHIFT_DAY
            objTitleData.IS_HOURS_STOP = objTitle.IS_HOURS_STOP
            objTitleData.IS_MID_END = objTitle.IS_MID_END
            objTitleData.IS_HOURS_CHECKOUT = objTitle.IS_HOURS_CHECKOUT

            objTitleData.LATE_HOUR = objTitle.LATE_HOUR
            objTitleData.EARLY_HOUR = objTitle.EARLY_HOUR
            objTitleData.IS_TOMORROW = objTitle.IS_TOMORROW
            objTitleData.SHIFT_HOUR = objTitle.SHIFT_HOUR
            objTitleData.OT_HOUR_MIN = objTitle.OT_HOUR_MIN
            objTitleData.OT_HOUR_MAX = objTitle.OT_HOUR_MAX
            objTitleData.IS_HOLYDAY_SHIFT = objTitle.IS_HOLYDAY_SHIFT
            objTitleData.IS_MORNING_SHIFT = objTitle.IS_MORNING_SHIFT
            objTitleData.IS_AFTERNOON_SHIFT = objTitle.IS_AFTERNOON_SHIFT
            objTitleData.IS_MIDDLE_SHIFT = objTitle.IS_MIDDLE_SHIFT
            objTitleData.IS_BROKEN_SHIFT = objTitle.IS_BROKEN_SHIFT
            objTitleData.IS_TIME_SHIFT = objTitle.IS_TIME_SHIFT
            objTitleData.IS_CH_SUPPORT_SHIFT = objTitle.IS_CH_SUPPORT_SHIFT
            objTitleData.IS_SATURDAY_SHIFT = objTitle.IS_SATURDAY_SHIFT
            objTitleData.IS_SUNDAY_SHIFT = objTitle.IS_SUNDAY_SHIFT
            'end
            objTitleData.MINHOURS = objTitle.MINHOUSER
            Context.AT_SHIFT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Update_AT_ORG(objTitle.LIST_ORG, gID, False, log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    Public Function ValidateAT_ORG_SHIFT(ByVal _ID As Decimal, ByVal _ORGID As Decimal) As Boolean
        Try
            Dim query = (From p In Context.AT_SHIFT_ORG_ACCESS Where p.AT_SHIFT_ID = _ID And p.ORG_ID = _ORGID).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function ValidateAT_SHIFT(ByVal _validate As AT_SHIFTDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SHIFT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SHIFT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_SHIFT
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_SHIFT
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SHIFT(ByVal objTitle As AT_SHIFTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SHIFT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SHIFT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            'objTitleData.APPLY_LAW = objTitle.APPLY_LAW
            'objTitleData.PENALIZEA = objTitle.PENALIZEA
            objTitleData.SATURDAY = objTitle.SATURDAY
            objTitleData.SUNDAY = objTitle.SUNDAY
            objTitleData.MANUAL_ID = objTitle.MANUAL_ID
            objTitleData.HOURS_START = objTitle.HOURS_START
            objTitleData.HOURS_STOP = objTitle.HOURS_STOP
            objTitleData.END_MID_HOURS = objTitle.END_MID_HOURS
            objTitleData.START_MID_HOURS = objTitle.START_MID_HOURS
            objTitleData.HOURS_STAR_CHECKIN = objTitle.HOURS_STAR_CHECKIN
            objTitleData.HOURS_STAR_CHECKOUT = objTitle.HOURS_STAR_CHECKOUT
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_NOON = objTitle.IS_NOON
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            'hoaivv add
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.SHIFT_DAY = objTitle.SHIFT_DAY
            objTitleData.IS_HOURS_STOP = objTitle.IS_HOURS_STOP
            objTitleData.IS_MID_END = objTitle.IS_MID_END
            objTitleData.IS_HOURS_CHECKOUT = objTitle.IS_HOURS_CHECKOUT

            objTitleData.LATE_HOUR = objTitle.LATE_HOUR
            objTitleData.EARLY_HOUR = objTitle.EARLY_HOUR
            objTitleData.IS_TOMORROW = objTitle.IS_TOMORROW
            objTitleData.SHIFT_HOUR = objTitle.SHIFT_HOUR
            objTitleData.OT_HOUR_MIN = objTitle.OT_HOUR_MIN
            objTitleData.OT_HOUR_MAX = objTitle.OT_HOUR_MAX
            objTitleData.IS_HOLYDAY_SHIFT = objTitle.IS_HOLYDAY_SHIFT
            objTitleData.IS_MORNING_SHIFT = objTitle.IS_MORNING_SHIFT
            objTitleData.IS_AFTERNOON_SHIFT = objTitle.IS_AFTERNOON_SHIFT
            objTitleData.IS_MIDDLE_SHIFT = objTitle.IS_MIDDLE_SHIFT
            objTitleData.IS_BROKEN_SHIFT = objTitle.IS_BROKEN_SHIFT
            objTitleData.IS_TIME_SHIFT = objTitle.IS_TIME_SHIFT
            objTitleData.IS_CH_SUPPORT_SHIFT = objTitle.IS_CH_SUPPORT_SHIFT
            objTitleData.IS_SATURDAY_SHIFT = objTitle.IS_SATURDAY_SHIFT
            objTitleData.IS_SUNDAY_SHIFT = objTitle.IS_SUNDAY_SHIFT

            'end
            objTitleData.MINHOURS = objTitle.MINHOUSER
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Update_AT_ORG(objTitle.LIST_ORG, gID, True, log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SHIFT)
        Try
            lstData = (From p In Context.AT_SHIFT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SHIFT)
        Try
            lstHolidayData = (From p In Context.AT_SHIFT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SHIFT.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function DeleteAT_SHIFT_ORG_ACCESS(ByVal _AT_SHIFT_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("DELETE AT_SHIFT_ORG_ACCESS WHERE AT_SHIFT_ID =" & _AT_SHIFT_ID)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Update_AT_ORG(ByVal _lstOrg As List(Of Common.CommonBusiness.UserOrgAccessDTO), ByVal _AT_SHIFT_ID As Decimal, ByVal Is_Update As Boolean, ByVal log As UserLog) As Boolean
        If _lstOrg.Count > 0 Then
            If Is_Update Then
                DeleteAT_SHIFT_ORG_ACCESS(_AT_SHIFT_ID)
            End If
            Dim i As Integer
            For i = 0 To _lstOrg.Count - 1
                Dim _item As New AT_SHIFT_ORG_ACCESS
                _item.ID = Utilities.GetNextSequence(Context, Context.AT_SHIFT_ORG_ACCESS.EntitySet.Name)
                _item.ORG_ID = _lstOrg(i).ORG_ID
                _item.AT_SHIFT_ID = _AT_SHIFT_ID
                Context.AT_SHIFT_ORG_ACCESS.AddObject(_item)
            Next
            Context.SaveChanges(log)
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetAT_SHIFT_ORG_ACCESS_By_ID(ByVal _AT_SHIFT_ID As Decimal) As List(Of Decimal)
        Return (From p In Context.AT_SHIFT_ORG_ACCESS
                From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                Where p.AT_SHIFT_ID = _AT_SHIFT_ID
                Select p.ORG_ID).ToList
    End Function
    Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GETAT_TIME_MANUAL",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Try
            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_TYPEPUNISH Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPEPUNISH(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "TYPE_PUNISH" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPESHIFT Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPESHIFT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where (p.ACTFLG = "A" And t.CODE = "TYPE_SHIFT" And p.ID = ID)
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_APPLY_LAW Then
                Dim ID As Decimal = cbxData.LIST_LIST_APPLY_LAW(0).ID
                Dim list As List(Of AT_GSIGNDTO) = (From p In Context.AT_GSIGN Where p.ACTFLG = "A" And p.ID = ID
                                                    Order By p.NAME_VN Descending
                                                    Select New AT_GSIGNDTO With {
                                                        .ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_PENALIZEA Then
                Dim ID As Decimal = cbxData.LIST_LIST_PENALIZEA(0).ID
                Dim list As List(Of AT_DMVSDTO) = (From p In Context.AT_DMVS Where p.ACTFLG = "A" And p.ID = ID
                                                   Order By p.NAME_VN Descending
                                                   Select New AT_DMVSDTO With {
                                                       .ID = p.ID,
                                                       .CODE = p.CODE,
                                                       .NAME_VN = p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_SHIFT Then
                Dim ID As Decimal = cbxData.LIST_LIST_SHIFT(0).ID
                Dim list As List(Of AT_SHIFTDTO) = (From p In Context.AT_SHIFT Where p.ACTFLG = "A" And p.ID = ID
                                                    Order By p.NAME_VN Descending
                                                    Select New AT_SHIFTDTO With {
                                                        .ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_SIGN Then
                Dim ID As Decimal = cbxData.LIST_LIST_SIGN(0).ID
                Dim list As List(Of AT_FMLDTO) = (From p In Context.AT_FML Where p.ACTFLG = "A" And p.ID = ID
                                                  Order By p.NAME_VN Descending
                                                  Select New AT_FMLDTO With {
                                                      .ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPEEMPLOYEE Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPEEMPLOYEE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "TYPE_EMPLOYEE" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPEE_FML Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPE_FML(0).ID
                Dim list As List(Of AT_FMLDTO) = (From p In Context.AT_FML Where p.ACTFLG = "A" And p.ID = ID
                                                  Order By p.NAME_VN Descending
                                                  Select New AT_FMLDTO With {
                                                      .ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .NAME_VN = "[" & p.CODE & "] " & p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_REST_DAY Then
                Dim ID As Decimal = cbxData.LIST_LIST_REST_DAY(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "AT_TIMELEAVE" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPE_DMVS Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPE_DMVS(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.AT_TIME_MANUAL
                                                         Where p.ACTFLG = "A" And p.CODE = "RDT" Or p.CODE = "RVS" And p.ID = ID
                                                         Order By p.NAME Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_VN = p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPE_MANUAL_LEAVE(0).ID
                Dim list As List(Of AT_TIME_MANUALDTO) = (From p In Context.AT_TIME_MANUAL
                                                          From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                          From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                          Where p.ACTFLG = "A" And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And p.ID = ID
                                                          Order By p.NAME Descending
                                                          Select New AT_TIME_MANUALDTO With {
                                                              .ID = p.ID,
                                                              .CODE = p.CODE,
                                                              .MORNING_ID = p.MORNING_ID,
                                                              .AFTERNOON_ID = p.AFTERNOON_ID,
                                                              .ORDER_W = p.ORDER_W,
                                                              .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPE_MANUAL Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPE_MANUAL(0).ID
                Dim list As List(Of AT_TIME_MANUALDTO) = (From p In Context.AT_TIME_MANUAL Where p.ACTFLG = "A" And p.CODE <> "RVS" And p.CODE <> "RDT" And p.ID = ID
                                                          Order By p.NAME Descending
                                                          Select New AT_TIME_MANUALDTO With {
                                                              .ID = p.ID,
                                                              .CODE = p.CODE,
                                                              .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_SHIFT_SUNDAY Then
                Dim ID As Decimal = cbxData.LIST_LIST_SHIFT_SUNDAY(0).ID
                Dim list As List(Of AT_TIME_MANUALDTO) = (From p In Context.AT_TIME_MANUAL Where p.ACTFLG = "A" And p.CODE = "RD" And p.ID = ID
                                                          Order By p.NAME Descending
                                                          Select New AT_TIME_MANUALDTO With {
                                                              .ID = p.ID,
                                                              .CODE = p.CODE,
                                                              .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' danh mục cấp nhân sự
            If cbxData.GET_LIST_STAFF_RANK Then
                Dim ID As Decimal = cbxData.LIST_LIST_STAFF_RANK(0).ID
                Dim list As List(Of HU_STAFF_RANKDTO) = (From p In Context.HU_STAFF_RANK Where p.ACTFLG = "A" And p.ID = ID
                                                         Order By p.NAME Descending
                                                         Select New HU_STAFF_RANKDTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME = p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_HS_OT Then
                Dim ID As Decimal = cbxData.LIST_LIST_HS_OT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION
                                                         Where p.ACTFLG = "A" And p.AUTOGEN <> 0
                                                         Select New OT_OTHERLIST_DTO With {
                                                              .ID = p.ID,
                                                              .CODE = p.EXCEPTION_COEFF_CODE,
                                                              .NAME_VN = p.EXCEPTION_COEFF}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_LIST_TYPE_OT Then
                Dim ID As Decimal = cbxData.LIST_LIST_TYPE_OT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "TYPE_OT" And p.ID = ID
                                                         Order By p.ID Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

#End Region

#Region "Thiết lập số ngày nghỉ chế độ theo đối tượng"
    Public Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        Try

            Dim query = From p In Context.AT_HOLIDAY_OBJECT
                        From t In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.EMP_OBJECT).DefaultIfEmpty
                        From FML In Context.AT_FML.Where(Function(F) F.ID = p.TYPE_SHIT).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_HOLIDAY_OBJECTDTO With {
                                       .ID = p.p.ID,
                                       .EMP_OBJECT = p.p.EMP_OBJECT,
                                       .EMP_OBJECT_NAME = p.t.NAME_VN,
                                       .TYPE_SHIT = p.p.TYPE_SHIT,
                                       .TYPE_SHIT_NAME = p.FML.NAME_VN,
                                       .SALARIED_DATES = p.p.SALARIED_DATES,
                                       .SALARIED_DATES_CB = p.p.SALARIED_DATES_CB,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMP_OBJECT_NAME) Then
                lst = lst.Where(Function(f) f.EMP_OBJECT_NAME.ToLower().Contains(_filter.EMP_OBJECT_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_SHIT_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_SHIT_NAME.ToLower().Contains(_filter.TYPE_SHIT_NAME.ToLower()))
            End If
            If _filter.SALARIED_DATES_CB.HasValue Then
                lst = lst.Where(Function(f) f.SALARIED_DATES_CB = _filter.SALARIED_DATES_CB)
            End If
            If _filter.SALARIED_DATES.HasValue Then
                lst = lst.Where(Function(f) f.SALARIED_DATES = _filter.SALARIED_DATES)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_Holiday_Object(ByVal objTitle As AT_HOLIDAY_OBJECTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_OBJECT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_HOLIDAY_OBJECT.EntitySet.Name)
            objTitleData.EMP_OBJECT = objTitle.EMP_OBJECT
            objTitleData.TYPE_SHIT = objTitle.TYPE_SHIT
            objTitleData.SALARIED_DATES = objTitle.SALARIED_DATES
            objTitleData.SALARIED_DATES_CB = objTitle.SALARIED_DATES_CB
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_HOLIDAY_OBJECT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_Holiday_Object(ByVal _validate As AT_HOLIDAY_OBJECTDTO)

        Try
            'If _validate.EMP_OBJECT <> Nothing Then
            '    If _validate.ID <> 0 Then
            '        query = (From p In Context.AT_HOLIDAY_OBJECT
            '                 Where p.EMP_OBJECT.ToUpper = _validate.EMP_OBJECT.ToUpper _
            '                 And p.ID <> _validate.ID).FirstOrDefault
            '    Else
            '        query = (From p In Context.AT_HOLIDAY_OBJECT
            '                 Where p.EMP_OBJECT.ToUpper = _validate.EMP_OBJECT.ToUpper).FirstOrDefault
            '    End If
            '    Return (query Is Nothing)
            'End If
            'Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_Holiday_Object(ByVal objTitle As AT_HOLIDAY_OBJECTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_HOLIDAY_OBJECT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_HOLIDAY_OBJECT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMP_OBJECT = objTitle.EMP_OBJECT
            objTitleData.TYPE_SHIT = objTitle.TYPE_SHIT
            objTitleData.SALARIED_DATES = objTitle.SALARIED_DATES
            objTitleData.SALARIED_DATES_CB = objTitle.SALARIED_DATES_CB
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_HOLIDAY_OBJECT)
        Try
            lstData = (From p In Context.AT_HOLIDAY_OBJECT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_HOLIDAY_OBJECT)
        Try
            lstHolidayData = (From p In Context.AT_HOLIDAY_OBJECT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_HOLIDAY_OBJECT.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteHolidayObject")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập chấm công theo cấp nhân sự"
    Public Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        Try

            Dim query = From p In Context.AT_SETUP_SPECIAL
                        From S In Context.HU_STAFF_RANK.Where(Function(F) F.ID = p.STAFF_RANK_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_SETUP_SPECIALDTO With {
                                      .ID = p.p.ID,
                                      .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                                      .STAFF_RANK_NAME = p.S.NAME,
                                      .NUMBER_SWIPECARD = p.p.NUMBER_SWIPECARD,
                                      .NOTE = p.p.NOTE,
                                      .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.p.CREATED_BY,
                                      .CREATED_DATE = p.p.CREATED_DATE,
                                      .CREATED_LOG = p.p.CREATED_LOG,
                                      .MODIFIED_BY = p.p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.NUMBER_SWIPECARD.HasValue Then
                lst = lst.Where(Function(f) f.NUMBER_SWIPECARD = _filter.NUMBER_SWIPECARD)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_SETUP_SPECIAL(ByVal objTitle As AT_SETUP_SPECIALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_SPECIAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_SPECIAL.EntitySet.Name)
            'objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            'objTitleData.ORG_ID = objTitle.ORG_ID
            'objTitleData.POS_ID = objTitle.ORG_ID
            'objTitleData.DATE_FROM = objTitle.DATE_FROM
            'objTitleData.DATE_TO = objTitle.DATE_TO
            objTitleData.STAFF_RANK_ID = objTitle.STAFF_RANK_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SETUP_SPECIAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_SETUP_SPECIAL(ByVal _validate As AT_SETUP_SPECIALDTO)
        Dim query
        Try
            If _validate.STAFF_RANK_ID <> 0 Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.STAFF_RANK_ID = _validate.STAFF_RANK_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.STAFF_RANK_ID = _validate.STAFF_RANK_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_SETUP_SPECIAL
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SETUP_SPECIAL(ByVal objTitle As AT_SETUP_SPECIALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_SPECIAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_SPECIAL Where p.ID = objTitleData.ID).SingleOrDefault
            'objTitleData.ID = objTitle.ID
            'objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            'objTitleData.ORG_ID = objTitle.ORG_ID
            'objTitleData.POS_ID = objTitle.ORG_ID
            'objTitleData.DATE_FROM = objTitle.DATE_FROM
            'objTitleData.DATE_TO = objTitle.DATE_TO
            objTitleData.STAFF_RANK_ID = objTitle.STAFF_RANK_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_SPECIAL)
        Try
            lstData = (From p In Context.AT_SETUP_SPECIAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lsAT_SetUp_Special As List(Of AT_SETUP_SPECIAL)
        Try
            lsAT_SetUp_Special = (From p In Context.AT_SETUP_SPECIAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lsAT_SetUp_Special.Count - 1
                Context.AT_SETUP_SPECIAL.DeleteObject(lsAT_SetUp_Special(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAT_SetUp")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập chấm công theo nhân viên"
    Public Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        Try

            Dim query = From p In Context.AT_SETUP_TIME_EMP
                        From S In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(F) S.TITLE_ID = F.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(F) S.ORG_ID = F.ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_SETUP_TIME_EMPDTO With {
                                      .ID = p.p.ID,
                                      .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                      .EMPLOYEE_CODE = p.S.EMPLOYEE_CODE,
                                      .EMPLOYEE_NAME = p.S.FULLNAME_VN,
                                      .ORG_ID = p.S.ORG_ID,
                                      .ORG_NAME = p.o.NAME_VN,
                                      .ORG_DESC = p.o.DESCRIPTION_PATH,
                                      .TITLE_ID = p.S.TITLE_ID,
                                      .TITLE_NAME = p.t.NAME_VN,
                                      .NUMBER_SWIPECARD = p.p.NUMBER_SWIPECARD,
                                      .NOTE = p.p.NOTE,
                                      .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_BY = p.p.CREATED_BY,
                                      .CREATED_DATE = p.p.CREATED_DATE,
                                      .CREATED_LOG = p.p.CREATED_LOG,
                                      .MODIFIED_BY = p.p.MODIFIED_BY,
                                      .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                      .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
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
            If _filter.NUMBER_SWIPECARD.HasValue Then
                lst = lst.Where(Function(f) f.NUMBER_SWIPECARD = _filter.NUMBER_SWIPECARD)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function InsertAT_SETUP_TIME_EMP(ByVal objTitle As AT_SETUP_TIME_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_TIME_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_TIME_EMP.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE.Trim()
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.AT_SETUP_TIME_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_SETUP_TIME_EMP(ByVal _validate As AT_SETUP_TIME_EMPDTO)
        Dim query
        Try
            If _validate.EMPLOYEE_ID <> 0 Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_SETUP_TIME_EMP
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_SETUP_TIME_EMP(ByVal objTitle As AT_SETUP_TIME_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_TIME_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_TIME_EMP Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.NUMBER_SWIPECARD = objTitle.NUMBER_SWIPECARD
            objTitleData.NOTE = objTitle.NOTE.Trim
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_TIME_EMP)
        Try
            lstData = (From p In Context.AT_SETUP_TIME_EMP Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lsAT_SetUp_time_emp As List(Of AT_SETUP_TIME_EMP)
        Try
            lsAT_SetUp_time_emp = (From p In Context.AT_SETUP_TIME_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lsAT_SetUp_time_emp.Count - 1
                Context.AT_SETUP_TIME_EMP.DeleteObject(lsAT_SetUp_time_emp(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteAT_SetUp")
            Throw ex
        End Try
    End Function
#End Region
#Region "Thiết lập thang quy đổi"
    Public Function GetAT_SetUp_Exchange(ByVal _filter As AT_SETUP_EXCHANGEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_SETUP_EXCHANGEDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_SETUP_EXCHANGE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_EXCHANGE And f.ACTFLG = "A").DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDACE And f.ACTFLG = "A").DefaultIfEmpty
                        Select New AT_SETUP_EXCHANGEDTO With {
                                       .ID = p.ID,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .OBJECT_ATTENDACE = p.OBJECT_ATTENDACE,
                                       .OBJECT_ATTENDACE_NAME = ot1.NAME_VN,
                                       .TYPE_EXCHANGE = p.TYPE_EXCHANGE,
                                       .TYPE_EXCHANGE_NAME = ot.NAME_VN,
                                       .NUMBER_DATE = p.NUMBER_DATE,
                                       .STT = p.STT,
                                       .FROM_MINUTE = p.FROM_MINUTE,
                                       .TO_MINUTE = p.TO_MINUTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(F) F.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.FROM_MINUTE IsNot Nothing Then
                lst = lst.Where(Function(F) F.FROM_MINUTE = _filter.FROM_MINUTE)
            End If
            If _filter.TO_MINUTE IsNot Nothing Then
                lst = lst.Where(Function(F) F.TO_MINUTE = _filter.TO_MINUTE)
            End If
            If _filter.NUMBER_DATE IsNot Nothing Then
                lst = lst.Where(Function(F) F.NUMBER_DATE = _filter.NUMBER_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.OBJECT_ATTENDACE_NAME) Then
                lst = lst.Where(Function(f) f.OBJECT_ATTENDACE_NAME.ToLower().Contains(_filter.OBJECT_ATTENDACE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_EXCHANGE_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_EXCHANGE_NAME.ToLower().Contains(_filter.TYPE_EXCHANGE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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
    Public Function InsertAT_SetUp_Exchange(ByVal objTitle As AT_SETUP_EXCHANGEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_EXCHANGE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_EXCHANGE.EntitySet.Name)
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.OBJECT_ATTENDACE = objTitle.OBJECT_ATTENDACE
            objTitleData.TYPE_EXCHANGE = objTitle.TYPE_EXCHANGE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.FROM_MINUTE = objTitle.FROM_MINUTE
            objTitleData.TO_MINUTE = objTitle.TO_MINUTE
            objTitleData.NUMBER_DATE = objTitle.NUMBER_DATE
            objTitleData.STT = objTitle.STT
            Context.AT_SETUP_EXCHANGE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ModifyAT_SetUp_Exchange(ByVal objTitle As AT_SETUP_EXCHANGEDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_EXCHANGE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_EXCHANGE Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.OBJECT_ATTENDACE = objTitle.OBJECT_ATTENDACE
            objTitleData.TYPE_EXCHANGE = objTitle.TYPE_EXCHANGE
            'objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.FROM_MINUTE = objTitle.FROM_MINUTE
            objTitleData.TO_MINUTE = objTitle.TO_MINUTE
            objTitleData.NUMBER_DATE = objTitle.NUMBER_DATE
            objTitleData.STT = objTitle.STT
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteAT_SetUp_Exchange(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_SETUP_EXCHANGE)
        Try
            lstAT_Terminal = (From p In Context.AT_SETUP_EXCHANGE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_SETUP_EXCHANGE.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function
    Public Function ActiveAT_SetUp_Exchange(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SETUP_EXCHANGE)
        Try
            lstData = (From p In Context.AT_SETUP_EXCHANGE Where lstID.Contains(p.ID)).ToList
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
    Public Function CheckTrung_AT__SetUp_exchange(ByVal id As Decimal, ByVal from_minute As Decimal,
                                                  ByVal to_minute As Decimal,
                                                  ByVal EFFECT_DATE As Date,
                                                  ByVal OBJECT_ATTENDACE As Decimal,
                                                  ByVal TYPE_EXCHANGE As Decimal,
                                                  ByVal ORG_ID As Decimal) As Integer
        Dim objlst As New List(Of AT_SETUP_EXCHANGEDTO)
        Try
            objlst = (From p In Context.AT_SETUP_EXCHANGE
                      Where id <> p.ID
                      Select New AT_SETUP_EXCHANGEDTO With {
                       .FROM_MINUTE = p.FROM_MINUTE,
                       .TO_MINUTE = p.TO_MINUTE,
                       .EFFECT_DATE = p.EFFECT_DATE,
                       .OBJECT_ATTENDACE = p.OBJECT_ATTENDACE,
                       .TYPE_EXCHANGE = p.TYPE_EXCHANGE,
                       .ORG_ID = p.ORG_ID
                     }).ToList
            For Each line In objlst
                If (((line.FROM_MINUTE <= from_minute And line.TO_MINUTE >= from_minute) _
                     Or (line.FROM_MINUTE <= to_minute And line.TO_MINUTE >= to_minute)) And
                    EFFECT_DATE = line.EFFECT_DATE And OBJECT_ATTENDACE = line.OBJECT_ATTENDACE _
                    And TYPE_EXCHANGE = line.TYPE_EXCHANGE And ORG_ID = line.ORG_ID) Then
                    Return 1
                End If
                If (((line.FROM_MINUTE <= from_minute And line.TO_MINUTE >= from_minute) _
                     Or (line.FROM_MINUTE <= to_minute And line.TO_MINUTE >= to_minute)) And
                    EFFECT_DATE = line.EFFECT_DATE And OBJECT_ATTENDACE = line.OBJECT_ATTENDACE And
                    TYPE_EXCHANGE = line.TYPE_EXCHANGE And ORG_ID = line.ORG_ID) Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Đăng ký máy chấm công"
    ''' <summary>
    ''' Lấy dữ liệu đăng ký máy chấm công
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_TERMINALS
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TERMINAL_TYPE And f.ACTFLG = "A").DefaultIfEmpty
                        From swipe In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TIME_RECORDER And f.ACTFLG = "A").DefaultIfEmpty
                        Select New AT_TERMINALSDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .TERMINAL_TYPE = p.TERMINAL_TYPE,
                                       .TERMINAL_TYPE_NAME = ot.NAME_VN,
                                       .TIME_RECORDER = p.TIME_RECORDER,
                                       .TIME_RECORDER_NAME = swipe.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_TYPE_NAME.ToLower().Contains(_filter.TERMINAL_TYPE_NAME.ToLower()))
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
    ''' <summary>
    ''' Lấy danh sách đăng ký máy chấm công theo trạng thái
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim query = From p In Context.AT_TERMINALS
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.ACTFLG = "A"
                        Select p

            Dim lst = query.Select(Function(p) New AT_TERMINALSDTO With {
                                       .ID = p.ID,
                                       .TERMINAL_CODE = p.TERMINAL_CODE,
                                       .TERMINAL_NAME = p.TERMINAL_NAME,
                                       .ADDRESS_PLACE = p.ADDRESS_PLACE,
                                       .TERMINAL_IP = p.TERMINAL_IP,
                                       .TERMINAL_STATUS = p.TERMINAL_STATUS,
                                       .LAST_TIME_STATUS = p.LAST_TIME_STATUS,
                                       .LAST_TIME_UPDATE = p.LAST_TIME_UPDATE,
                                       .TERMINAL_ROW = p.TERMINAL_ROW,
                                       .NOTE = p.NOTE,
                                       .PASS = p.PASS,
                                       .PORT = p.PORT,
                                       .CREATED_DATE = p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.TERMINAL_CODE) Then
                lst = lst.Where(Function(f) f.TERMINAL_CODE.ToLower().Contains(_filter.TERMINAL_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_NAME) Then
                lst = lst.Where(Function(f) f.TERMINAL_NAME.ToLower().Contains(_filter.TERMINAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_IP) Then
                lst = lst.Where(Function(f) f.TERMINAL_IP.ToLower().Contains(_filter.TERMINAL_IP.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS_PLACE) Then
                lst = lst.Where(Function(f) f.ADDRESS_PLACE.ToLower().Contains(_filter.ADDRESS_PLACE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PASS) Then
                lst = lst.Where(Function(f) f.PASS.ToLower().Contains(_filter.PASS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PORT) Then
                lst = lst.Where(Function(f) f.PORT.ToLower().Contains(_filter.PORT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TERMINAL_STATUS) Then
                lst = lst.Where(Function(f) f.TERMINAL_STATUS.ToLower().Contains(_filter.TERMINAL_STATUS.ToLower()))
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
    ''' <summary>
    ''' Thêm mới đăng ký máy chấm công
    ''' </summary>
    ''' <param name="objTitle"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertAT_TERMINAL(ByVal objTitle As AT_TERMINALSDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TERMINALS.EntitySet.Name)
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            objTitleData.TERMINAL_TYPE = objTitle.TERMINAL_TYPE
            objTitleData.TIME_RECORDER = objTitle.TIME_RECORDER
            Context.AT_TERMINALS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Validate tồn tại máy chấm công đã đăng ký
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAT_TERMINAL(ByVal _validate As AT_TERMINALSDTO)
        Dim query
        Try
            If _validate.TERMINAL_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TERMINALS
                             Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TERMINALS
                             Where p.TERMINAL_CODE.ToUpper = _validate.TERMINAL_CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_TERMINALS
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_TERMINALS
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Sửa thông tin máy chấm công
    ''' </summary>
    ''' <param name="objTitle"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyAT_TERMINAL(ByVal objTitle As AT_TERMINALSDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TERMINALS With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TERMINALS Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.TERMINAL_CODE = objTitle.TERMINAL_CODE
            objTitleData.TERMINAL_NAME = objTitle.TERMINAL_NAME
            objTitleData.ADDRESS_PLACE = objTitle.ADDRESS_PLACE
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.TERMINAL_IP = objTitle.TERMINAL_IP
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.PASS = objTitle.PASS
            objTitleData.PORT = objTitle.PORT
            objTitleData.TERMINAL_TYPE = objTitle.TERMINAL_TYPE
            objTitleData.TIME_RECORDER = objTitle.TIME_RECORDER
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Kích hoạt thông tin máy chấm công
    ''' </summary>
    ''' <param name="lstID"></param>
    ''' <param name="log"></param>
    ''' <param name="bActive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TERMINALS)
        Try
            lstData = (From p In Context.AT_TERMINALS Where lstID.Contains(p.ID)).ToList
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
    ''' <summary>
    ''' Xóa thông tin máy chấm công
    ''' </summary>
    ''' <param name="lstID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_TERMINALS)
        Try
            lstAT_Terminal = (From p In Context.AT_TERMINALS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_TERMINALS.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function
#End Region

#Region "Đăng ký chấm công mặc định"
    Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = 0})
            End Using

            Dim query = From p In Context.AT_SIGNDEFAULT
                        From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID)
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SINGDEFAULE).DefaultIfEmpty
                        From SH_SAT In Context.AT_SHIFT.Where(Function(F) F.ID = p.SING_SAT).DefaultIfEmpty
                        From SH_SUN In Context.AT_SHIFT.Where(Function(F) F.ID = p.SING_SUN).DefaultIfEmpty
                        From SH_TUE In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGN_TUE).DefaultIfEmpty
                        From SH_WED In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGN_WED).DefaultIfEmpty
                        From SH_THU In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGN_THU).DefaultIfEmpty
                        From SH_FRI In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGN_FRI).DefaultIfEmpty
                        From ORG In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                        From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = ORG.ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From TI In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_SIGNDEFAULTDTO With {
                                       .ID = p.p.ID,
                                       .JOIN_DATE = p.e.JOIN_DATE,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .TITLE_NAME = p.TI.NAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.ORG.NAME_VN,
                                       .ORG_DESC = p.ORG.DESCRIPTION_PATH,
                                       .EFFECT_DATE_FROM = p.p.EFFECT_DATE_FROM,
                                       .EFFECT_DATE_TO = p.p.EFFECT_DATE_TO,
                                       .SINGDEFAULE = p.p.SINGDEFAULE,
                                       .SINGDEFAULF_NAME = p.SH.CODE,
                                       .SIGN_TUE = p.p.SIGN_TUE,
                                       .SIGN_TUE_NAME = p.SH_TUE.CODE,
                                       .SIGN_WED = p.p.SIGN_WED,
                                       .SIGN_WED_NAME = p.SH_WED.CODE,
                                       .SIGN_THU = p.p.SIGN_THU,
                                       .SIGN_THU_NAME = p.SH_THU.CODE,
                                       .SIGN_FRI = p.p.SIGN_FRI,
                                       .SIGN_FRI_NAME = p.SH_FRI.CODE,
                                       .SING_SAT = p.p.SING_SAT,
                                       .SING_SAT_NAME = p.SH_SAT.CODE,
                                       .SING_SUN = p.p.SING_SUN,
                                       .SING_SUN_NAME = p.SH_SUN.CODE,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SINGDEFAULF_NAME) Then
                lst = lst.Where(Function(f) f.SINGDEFAULF_NAME.ToLower().Contains(_filter.SINGDEFAULF_NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_FROM = _filter.EFFECT_DATE_FROM)
            End If
            If _filter.EFFECT_DATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_TO = _filter.EFFECT_DATE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            'lst = lst.Where(Function(f) f.CONTRACT_ID.HasValue)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function


    Public Function GetAT_YEARLEAVE_EDIT(ByVal _filter As AT_YEAR_LEAVE_EDITDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE ASC",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_YEAR_LEAVE_EDITDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = 0})
            End Using

            Dim query = From p In Context.AT_YEARLEAVE_EDIT
                        From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.EMPLOYEE_ID)
                        From ORG In Context.HU_ORGANIZATION.Where(Function(F) F.ID = e.ORG_ID).DefaultIfEmpty
                        From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = ORG.ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From TI In Context.HU_TITLE.Where(Function(F) F.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_YEAR_LEAVE_EDITDTO With {
                                       .ID = p.p.ID,
                                       .YEAR = p.p.YEAR,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.TI.NAME_VN,
                                       .ORG_NAME = p.ORG.NAME_VN,
                                       .LEAVE_NUMBER = p.p.LEAVE_NUMBER,
                                       .LEAVE_OLD = p.p.LEAVE_OLD,
                                       .ORG_DESC = p.ORG.DESCRIPTION_PATH,
                                       .NOTE = p.p.NOTE})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If _filter.LEAVE_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVE_NUMBER = _filter.LEAVE_NUMBER)
            End If

            If _filter.LEAVE_OLD IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVE_OLD = _filter.LEAVE_OLD)
            End If

            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
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


    'Public Function CHECK_NGHI_LE(ByVal ID As Decimal) As Decimal
    '    Try
    '        Dim query= From p In Context.

    '    Catch ex As Exception

    '    End Try
    'End Function


    Public Function PRS_COUNT_SHIFT(ByVal employee_id As Decimal) As DataTable
        Try
            Dim query = From p In Context.AT_WORKSIGN
                        From ce In Context.AT_SHIFT.Where(Function(f) f.ID = p.SHIFT_ID)
                        Where p.EMPLOYEE_ID = employee_id And ce.CODE <> "OFF"
            Dim lst = query.Select(Function(p) New AT_WORKSIGNDTO With {
                                       .ID = p.p.ID,
                                .WORKINGDAY1 = p.p.WORKINGDAY}).ToList
            Return lst.ToTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAT_ListShift() As DataTable
        Try

            Dim query = From p In Context.AT_SHIFT Where p.ACTFLG = "A"
                        From t1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SATURDAY And f.TYPE_ID = 1036).DefaultIfEmpty
                        From t2 In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.SUNDAY).DefaultIfEmpty
                        From Dsvm In Context.AT_DMVS.Where(Function(f) f.ID = p.PENALIZEA).DefaultIfEmpty
                        From mn In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID).DefaultIfEmpty
                        Order By p.CODE
            Dim lst = query.Select(Function(p) New AT_SHIFTDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = "[" & p.p.CODE & "] " & p.p.NAME_VN,
                                       .MANUAL_NAME = "[" & p.mn.CODE & "]" & p.mn.NAME,
                                       .IS_NOON = If(p.p.IS_NOON, "X", ""),
                                       .SUNDAY_NAME = p.t2.NAME,
                                       .HOURS_START = p.p.HOURS_START,
                                       .HOURS_STOP = p.p.HOURS_STOP,
                                       .HOURS_STAR_CHECKIN = p.p.HOURS_STAR_CHECKIN,
                                       .HOURS_STAR_CHECKOUT = p.p.HOURS_STAR_CHECKOUT,
                                       .NOTE = p.p.NOTE,
                                       .ORG_ID = p.p.ORG_ID}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetAT_PERIOD() As DataTable
        Try

            Dim query = From p In Context.AT_PERIOD
                        Order By p.YEAR
            Dim lst = query.Select(Function(p) New AT_PERIODDTO With {
                                       .PERIOD_ID = p.ID,
                                       .PERIOD_NAME = p.PERIOD_NAME}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeID(ByVal employee_code As String, ByVal end_date As Date) As DataTable
        Try
            'Dim Period = (From w In Context.AT_PERIOD Where w.ID = period_ID).FirstOrDefault

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code
                        Order By p.IS_KIEM_NHIEM Descending
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeIDExits(ByVal employee_code As String) As DataTable
        Try
            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code
                        Order By p.IS_KIEM_NHIEM Descending
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        Try

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeByTimeID(ByVal time_ID As Decimal) As DataTable
        Try

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.ITIME_ID = time_ID
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_YEARLEAVE_EDIT(ByVal objTitle As AT_YEAR_LEAVE_EDITDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_YEARLEAVE_EDIT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_YEARLEAVE_EDIT.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.LEAVE_NUMBER = objTitle.LEAVE_NUMBER
            objTitleData.LEAVE_OLD = objTitle.LEAVE_OLD
            Context.AT_YEARLEAVE_EDIT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function


    Public Function ModifyAT_YEARLEAVE_EDIT(ByVal objTitle As AT_YEAR_LEAVE_EDITDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_YEARLEAVE_EDIT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_YEARLEAVE_EDIT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.LEAVE_NUMBER = objTitle.LEAVE_NUMBER
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.LEAVE_OLD = objTitle.LEAVE_OLD
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteExistAT_YEARLEAVE_EDIT(ByVal EmployeeID As Decimal, ByVal Year As Decimal, ByVal log As UserLog) As Boolean
        Dim objExist As New AT_YEARLEAVE_EDIT
        Try
            objExist = (From p In Context.AT_YEARLEAVE_EDIT Where p.EMPLOYEE_ID = EmployeeID And p.YEAR = Year).SingleOrDefault
            Context.AT_YEARLEAVE_EDIT.DeleteObject(objExist)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_AT_YEARLEAVE_EDIT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_AT_PROCESS.IMPORT_AT_YEARLEAVE_EDIT",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function InsertAT_SIGNDEFAULT(ByVal objTitle As AT_SIGNDEFAULTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean
        Dim objTitleData As New AT_SIGNDEFAULT
        Dim objTitleData_Update As New AT_SIGNDEFAULT
        Dim iCount As Integer = 0
        Try
            objTitleData_Update = (From p In Context.AT_SIGNDEFAULT Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID And p.EFFECT_DATE_FROM < objTitle.EFFECT_DATE_FROM Order By p.EFFECT_DATE_FROM Descending).FirstOrDefault
            If objTitleData_Update IsNot Nothing Then
                objTitleData_Update.EFFECT_DATE_TO = objTitle.EFFECT_DATE_FROM.Value.AddDays(-1)
            End If

            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SIGNDEFAULT.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.EFFECT_DATE_FROM = objTitle.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO = objTitle.EFFECT_DATE_TO
            objTitleData.SINGDEFAULE = objTitle.SINGDEFAULE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.SING_SUN = objTitle.SING_SUN
            objTitleData.SING_SAT = objTitle.SING_SAT
            objTitleData.SIGN_TUE = objTitle.SIGN_TUE
            objTitleData.SIGN_WED = objTitle.SIGN_WED
            objTitleData.SIGN_THU = objTitle.SIGN_THU
            objTitleData.SIGN_FRI = objTitle.SIGN_FRI
            Context.AT_SIGNDEFAULT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            'TỰ ĐỘNG PHÁT SINH SANG MÀN HÌNH WORK_SIGN BẢNG AT_WORKSIGN
            Using cls As New DataAccess.QueryData
                Dim todate = New Date?
                If objTitle.EFFECT_DATE_TO Is Nothing Then
                    todate = New DateTime(objTitle.EFFECT_DATE_FROM.Value.Year, 12, 31)
                Else
                    todate = objTitle.EFFECT_DATE_TO
                End If
                Dim obj = New With {.P_EMPLOYEE_ID = objTitle.EMPLOYEE_ID,
                                    .P_USERNAME = log.Username.ToUpper,
                                    .P_ORG_ID = param.ORG_ID,
                                    .P_PERIOD_ID = param.PERIOD_ID,
                                    .P_ISDISSOLVE = param.IS_DISSOLVE,
                                    .P_FROMDATE = objTitle.EFFECT_DATE_FROM,
                                    .P_ENDATE = todate,
                                    .P_OUT = cls.OUT_NUMBER}

                Dim query = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETSIGNDEFAULT_FOREMP", obj)
                Dim rs = Integer.Parse(obj.P_OUT)
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAT_SIGNDEFAULT(ByVal objTitle As AT_SIGNDEFAULTDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean
        Dim objTitleData As New AT_SIGNDEFAULT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SIGNDEFAULT Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.EFFECT_DATE_FROM = objTitle.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO = objTitle.EFFECT_DATE_TO
            objTitleData.SINGDEFAULE = objTitle.SINGDEFAULE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.SING_SUN = objTitle.SING_SUN
            objTitleData.SING_SAT = objTitle.SING_SAT
            objTitleData.SIGN_TUE = objTitle.SIGN_TUE
            objTitleData.SIGN_WED = objTitle.SIGN_WED
            objTitleData.SIGN_THU = objTitle.SIGN_THU
            objTitleData.SIGN_FRI = objTitle.SIGN_FRI
            Context.SaveChanges(log)
            gID = objTitleData.ID
            'TỰ ĐỘNG PHÁT SINH SANG MÀN HÌNH WORK_SIGN BẢNG AT_WORKSIGN
            Using cls As New DataAccess.QueryData
                Dim todate = New Date?
                If objTitle.EFFECT_DATE_TO Is Nothing Then
                    todate = New DateTime(objTitle.EFFECT_DATE_FROM.Value.Year, 12, 31)
                Else
                    todate = objTitle.EFFECT_DATE_TO
                End If
                Dim obj = New With {.P_EMPLOYEE_ID = objTitle.EMPLOYEE_ID,
                                   .P_USERNAME = log.Username.ToUpper,
                                   .P_ORG_ID = param.ORG_ID,
                                   .P_PERIOD_ID = param.PERIOD_ID,
                                   .P_ISDISSOLVE = param.IS_DISSOLVE,
                                   .P_FROMDATE = objTitle.EFFECT_DATE_FROM,
                                   .P_ENDATE = todate,
                                   .P_OUT = cls.OUT_NUMBER}

                Dim query = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.GETSIGNDEFAULT_FOREMP", obj)
                Dim rs = Integer.Parse(obj.P_OUT)
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateAT_SIGNDEFAULT(ByVal _validate As AT_SIGNDEFAULTDTO)
        Try
            'If _validate.EFFECT_DATE_FROM IsNot Nothing And _validate.EFFECT_DATE_TO IsNot Nothing Then
            '    If _validate.ID <> 0 Then
            '        query = (From p In Context.AT_SIGNDEFAULT
            '                 Where (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_TO And _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_FROM) _
            '                 And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
            '                 And p.ID <> _validate.ID).FirstOrDefault
            '    Else
            '        query = (From p In Context.AT_SIGNDEFAULT
            '                 Where (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_TO And _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_FROM) _
            '                 And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).FirstOrDefault
            '    End If
            '    Return (query Is Nothing)
            'Else
            '    If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
            '        query = (From p In Context.AT_SIGNDEFAULT
            '                 Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
            '                 And p.ID = _validate.ID).FirstOrDefault
            '        Return (Not query Is Nothing)
            '    End If
            '    If _validate.ID <> 0 And _validate.ACTFLG = "" Then
            '        query = (From p In Context.AT_SIGNDEFAULT
            '                 Where p.ID = _validate.ID).FirstOrDefault
            '        Return (query Is Nothing)
            '    End If
            'End If
            Dim Query = (From p In Context.AT_SIGNDEFAULT
                         Where p.ID <> _validate.ID AndAlso p.ACTFLG = "A" AndAlso p.EMPLOYEE_ID = _validate.EMPLOYEE_ID AndAlso
                         ((p.EFFECT_DATE_FROM <= _validate.EFFECT_DATE_FROM AndAlso p.EFFECT_DATE_TO >= _validate.EFFECT_DATE_FROM) _
                          OrElse (p.EFFECT_DATE_FROM <= _validate.EFFECT_DATE_TO AndAlso p.EFFECT_DATE_TO >= _validate.EFFECT_DATE_TO) _
                          OrElse (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_FROM AndAlso _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_FROM) _
                          OrElse (_validate.EFFECT_DATE_FROM <= p.EFFECT_DATE_TO AndAlso _validate.EFFECT_DATE_TO >= p.EFFECT_DATE_TO)))
            If Query.Count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SIGNDEFAULT)
        Try
            lstData = (From p In Context.AT_SIGNDEFAULT Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_SIGNDEFAULT)
        Try
            lstAT_Terminal = (From p In Context.AT_SIGNDEFAULT Where lstID.Contains(p.ID)).ToList
            For Each items In lstAT_Terminal
                Context.AT_SIGNDEFAULT.DeleteObject(items)
                'Delete AT_WORKSIGN from AT_SIGNDEFAULT
                Using cls As New DataAccess.QueryData
                    Dim todate = New Date?
                    If items.EFFECT_DATE_TO Is Nothing Then
                        todate = New DateTime(items.EFFECT_DATE_FROM.Value.Year, 12, 31)
                    Else
                        todate = items.EFFECT_DATE_TO
                    End If
                    Dim obj = New With {.P_EMPLOYEE_ID = items.EMPLOYEE_ID,
                                       .P_FROMDATE = items.EFFECT_DATE_FROM,
                                       .P_ENDATE = todate,
                                       .P_OUT = cls.OUT_NUMBER}

                    Dim query = cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.DELETE_AT_WORKSIGN_FROM_AT_SIGNDEFAULT", obj)
                    Dim rs = Integer.Parse(obj.P_OUT)
                End Using

            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_YEARLEAVE_EDIT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_YEARLEAVE_EDIT)
        Try
            lstAT_Terminal = (From p In Context.AT_YEARLEAVE_EDIT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_YEARLEAVE_EDIT.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function

    Public Function checkCopySignDefault(ByVal _empID As Decimal, ByVal _year As Decimal) As Boolean
        Try
            Dim query = (From p In Context.AT_SIGNDEFAULT Where p.EMPLOYEE_ID = _empID AndAlso (p.EFFECT_DATE_FROM.Value.Year = _year OrElse p.EFFECT_DATE_TO.Value.Year = _year)).Any
            If Not query Then
                query = (From p In Context.AT_TIME_TIMESHEET_MACHINET Where p.EMPLOYEE_ID = _empID AndAlso p.WORKINGDAY.Value.Year = _year).Any
                If Not query Then
                    query = (From p In Context.AT_LEAVESHEET_DETAIL Where p.EMPLOYEE_ID = _empID AndAlso p.LEAVE_DAY.Value.Year = _year).Any
                    If Not query Then
                        query = (From p In Context.AT_OT_REGISTRATION Where p.EMPLOYEE_ID = _empID AndAlso p.REGIST_DATE.Value.Year = _year).Any
                    End If
                End If
            End If

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iAttendance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function COPY_SIGN_DEFAULT(ByVal P_ID_COPY As Decimal, ByVal P_YEAR As Decimal, ByVal P_EMP_ID As Decimal, ByVal P_USERNAME As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID_COPY = P_ID_COPY,
                                    .P_YEAR = P_YEAR,
                                    .P_EMP_ID = P_EMP_ID,
                                    .P_USERNAME = P_USERNAME,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.COPY_SIGN_DEFAULT", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Đăng ký ca mặc định cho phòng ban"
    Public Function GetAT_SIGNDEFAULT_ORG(ByVal _filter As AT_SIGNDEFAULT_ORGDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULT_ORGDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim IDObjAT = (From p In Context.OT_OTHER_LIST_TYPE Where p.CODE = "OBJECT_ATTENDANT").FirstOrDefault
            Dim IDObjE = (From p In Context.OT_OTHER_LIST_TYPE Where p.CODE = "OBJECT_EMPLOYEE").FirstOrDefault

            Dim query = From p In Context.AT_SIGNDEFAULT_ORG
                        From SH In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_MON).DefaultIfEmpty
                        From SH_TUE In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_TUE).DefaultIfEmpty
                        From SH_WED In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_WED).DefaultIfEmpty
                        From SH_THU In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_THU).DefaultIfEmpty
                        From SH_FRI In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_FRI).DefaultIfEmpty
                        From SH_SAT In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_SAT).DefaultIfEmpty
                        From SH_SUN In Context.AT_SHIFT.Where(Function(F) F.ID = p.SIGNID_SUN).DefaultIfEmpty
                        From ORG In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                        From WK In Context.HU_WORK_PLACE.Where(Function(F) F.ID = p.WORKPLACE_ID).DefaultIfEmpty
                        From O_EMP In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJ_EMPLOYEE_ID And F.TYPE_ID = IDObjE.ID).DefaultIfEmpty
                        From O_AT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.OBJ_ATTENDANT_ID And F.TYPE_ID = IDObjAT.ID).DefaultIfEmpty
                        From s In Context.SE_CHOSEN_ORG.Where(Function(f) (f.ORG_ID = p.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper) _
                                                                            Or p.ORG_ID Is Nothing)
            'From s In Context.SE_CHOSEN_ORG.Where(Function(f) If(p.ORG_ID = Nothing, f.ORG_ID = 1, f.ORG_ID = ORG.ID) And f.USERNAME.ToUpper = log.Username.ToUpper)


            Dim lst = query.Select(Function(p) New AT_SIGNDEFAULT_ORGDTO With {
                                       .ID = p.p.ID,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.ORG.NAME_VN,
                                       .ORG_DESC = p.ORG.DESCRIPTION_PATH,
                                       .EFFECT_DATE_FROM = p.p.FROMDATE_EFFECT,
                                       .EFFECT_DATE_TO = p.p.TODATE_EFFECT,
                                       .SIGN_MON = p.p.SIGNID_MON,
                                       .SIGN_MON_NAME = p.SH.CODE,
                                       .SIGN_TUE = p.p.SIGNID_TUE,
                                       .SIGN_TUE_NAME = p.SH_TUE.CODE,
                                       .SIGN_WED = p.p.SIGNID_WED,
                                       .SIGN_WED_NAME = p.SH_WED.CODE,
                                       .SIGN_THU = p.p.SIGNID_THU,
                                       .SIGN_THU_NAME = p.SH_THU.CODE,
                                       .SIGN_FRI = p.p.SIGNID_FRI,
                                       .SIGN_FRI_NAME = p.SH_FRI.CODE,
                                       .SIGN_SAT = p.p.SIGNID_SAT,
                                       .SIGN_SAT_NAME = p.SH_SAT.CODE,
                                       .SIGN_SUN = p.p.SIGNID_SUN,
                                       .SIGN_SUN_NAME = p.SH_SUN.CODE,
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .WORKPLACE_ID = p.p.WORKPLACE_ID,
                                       .WORKPLACE_NAME = p.WK.NAME_VN,
                                       .OBJ_EMPLOYEE_ID = p.p.OBJ_EMPLOYEE_ID,
                                       .OBJ_EMPLOYEE_NAME = p.O_EMP.NAME_VN,
                                       .OBJ_ATTENDANT_ID = p.p.OBJ_ATTENDANT_ID,
                                       .OBJ_ATTENDANT_NAME = p.O_AT.NAME_VN,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .YEAR = p.p.YEAR,
                                       .CALENDAR = p.p.CALENDAR,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            lst = lst.Distinct()
            'Phòng ban
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            'Thứ 2
            If Not String.IsNullOrEmpty(_filter.SIGN_MON_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_MON_NAME.ToLower().Contains(_filter.SIGN_MON_NAME.ToLower()))
            End If
            'Thứ 3
            If Not String.IsNullOrEmpty(_filter.SIGN_TUE_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_TUE_NAME.ToLower().Contains(_filter.SIGN_TUE_NAME.ToLower()))
            End If
            'Thứ 4
            If Not String.IsNullOrEmpty(_filter.SIGN_WED_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_WED_NAME.ToLower().Contains(_filter.SIGN_WED_NAME.ToLower()))
            End If
            'Thứ 5
            If Not String.IsNullOrEmpty(_filter.SIGN_THU_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_THU_NAME.ToLower().Contains(_filter.SIGN_THU_NAME.ToLower()))
            End If
            'Thứ 6
            If Not String.IsNullOrEmpty(_filter.SIGN_FRI_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_FRI_NAME.ToLower().Contains(_filter.SIGN_FRI_NAME.ToLower()))
            End If
            'Thứ 7
            If Not String.IsNullOrEmpty(_filter.SIGN_SAT_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_SAT_NAME.ToLower().Contains(_filter.SIGN_SAT_NAME.ToLower()))
            End If
            'CN
            If Not String.IsNullOrEmpty(_filter.SIGN_SUN_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_SUN_NAME.ToLower().Contains(_filter.SIGN_SUN_NAME.ToLower()))
            End If
            'Ngày hiệu lực từ
            If _filter.EFFECT_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_FROM = _filter.EFFECT_DATE_FROM)
            End If
            'Ngày hiệu lực đến
            If _filter.EFFECT_DATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_TO = _filter.EFFECT_DATE_TO)
            End If
            'Trạng thái
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            'Ghi chú
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            'Nơi làm việc
            If Not String.IsNullOrEmpty(_filter.WORKPLACE_NAME) Then
                lst = lst.Where(Function(f) f.WORKPLACE_NAME.ToLower().Contains(_filter.WORKPLACE_NAME.ToLower()))
            End If
            'Đối tượng nhân viên
            If Not String.IsNullOrEmpty(_filter.OBJ_EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.OBJ_EMPLOYEE_NAME.ToLower().Contains(_filter.OBJ_EMPLOYEE_NAME.ToLower()))
            End If
            'Đối tượng chấm công
            If Not String.IsNullOrEmpty(_filter.OBJ_ATTENDANT_NAME) Then
                lst = lst.Where(Function(f) f.OBJ_ATTENDANT_NAME.ToLower().Contains(_filter.OBJ_ATTENDANT_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.CALENDAR) Then
                lst = lst.Where(Function(f) f.CALENDAR.ToLower().Contains(_filter.CALENDAR.ToLower()))
            End If

            If IsNumeric(_filter.YEAR) Then
                lst = lst.Where(Function(f) f.YEAR = CDec(_filter.YEAR))
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

    Public Function InsertAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_SIGNDEFAULT_ORG
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_SIGNDEFAULT_ORG.EntitySet.Name)
            objData.FROMDATE_EFFECT = obj.EFFECT_DATE_FROM
            objData.TODATE_EFFECT = obj.EFFECT_DATE_TO
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.YEAR = obj.YEAR
            objData.CALENDAR = obj.CALENDAR
            If IsNumeric(obj.ORG_ID) Then
                objData.ORG_ID = obj.ORG_ID
            End If
            If IsNumeric(obj.WORKPLACE_ID) Then
                objData.WORKPLACE_ID = obj.WORKPLACE_ID
            End If

            If IsNumeric(obj.OBJ_EMPLOYEE_ID) Then
                objData.OBJ_EMPLOYEE_ID = obj.OBJ_EMPLOYEE_ID
            End If

            If IsNumeric(obj.OBJ_ATTENDANT_ID) Then
                objData.OBJ_ATTENDANT_ID = obj.OBJ_ATTENDANT_ID
            End If
            objData.SIGNID_MON = obj.SIGN_MON
            objData.SIGNID_TUE = obj.SIGN_TUE
            objData.SIGNID_WED = obj.SIGN_WED
            objData.SIGNID_THU = obj.SIGN_THU
            objData.SIGNID_FRI = obj.SIGN_FRI
            objData.SIGNID_SAT = obj.SIGN_SAT
            objData.SIGNID_SUN = obj.SIGN_SUN
            Context.AT_SIGNDEFAULT_ORG.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_SIGNDEFAULT_ORG With {.ID = obj.ID}
        Try
            objData = (From p In Context.AT_SIGNDEFAULT_ORG Where p.ID = objData.ID).SingleOrDefault
            objData.FROMDATE_EFFECT = obj.EFFECT_DATE_FROM
            objData.TODATE_EFFECT = obj.EFFECT_DATE_TO
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.YEAR = obj.YEAR
            objData.CALENDAR = obj.CALENDAR
            objData.ORG_ID = obj.ORG_ID
            objData.WORKPLACE_ID = obj.WORKPLACE_ID
            objData.OBJ_EMPLOYEE_ID = obj.OBJ_EMPLOYEE_ID
            objData.OBJ_ATTENDANT_ID = obj.OBJ_ATTENDANT_ID
            objData.SIGNID_MON = obj.SIGN_MON
            objData.SIGNID_TUE = obj.SIGN_TUE
            objData.SIGNID_WED = obj.SIGN_WED
            objData.SIGNID_THU = obj.SIGN_THU
            objData.SIGNID_FRI = obj.SIGN_FRI
            objData.SIGNID_SAT = obj.SIGN_SAT
            objData.SIGNID_SUN = obj.SIGN_SUN
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ValidateAT_SIGNDEFAULT_ORG(ByVal _validate As AT_SIGNDEFAULT_ORGDTO)
        Try
            Dim Query As Decimal = 0
            If (IsNumeric(_validate.ORG_ID) AndAlso _validate.ORG_ID > 0) Then
                If (IsNumeric(_validate.OBJ_EMPLOYEE_ID) AndAlso _validate.OBJ_EMPLOYEE_ID > 0) Then
                    If (IsNumeric(_validate.OBJ_ATTENDANT_ID) AndAlso _validate.OBJ_ATTENDANT_ID > 0) Then
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đơn vị - Đối tượng nhân viên - Đối tượng chấm công - Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đơn vị - Đối tượng nhân viên - Đối tượng chấm công - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT) And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0)).ToList.Count
                        End If
                    Else
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đơn vị - Đối tượng nhân viên -  Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đơn vị - Đối tượng nhân viên - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    End If
                Else
                    If (IsNumeric(_validate.OBJ_ATTENDANT_ID) AndAlso _validate.OBJ_ATTENDANT_ID > 0) Then
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đơn vị - Đối tượng chấm công - Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đơn vị - Đối tượng chấm công - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    Else
                        'Đơn vị - Nơi làm việc - Ngày hiệu lực từ 
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đơn vị - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    End If
                End If
            Else
                'Không có đơn vị
                If (IsNumeric(_validate.OBJ_EMPLOYEE_ID) AndAlso _validate.OBJ_EMPLOYEE_ID > 0) Then
                    If (IsNumeric(_validate.OBJ_ATTENDANT_ID) AndAlso _validate.OBJ_ATTENDANT_ID > 0) Then
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đối tượng nhân viên - Đối tượng chấm công - Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đối tượng nhân viên - Đối tượng chấm công - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    Else
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đối tượng nhân viên - Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đối tượng nhân viên - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And p.OBJ_EMPLOYEE_ID = _validate.OBJ_EMPLOYEE_ID And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    End If
                Else
                    If (IsNumeric(_validate.OBJ_ATTENDANT_ID) AndAlso _validate.OBJ_ATTENDANT_ID > 0) Then
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            'Đối tượng chấm công - Nơi làm việc - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Đối tượng chấm công - Ngày hiệu lực từ 
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.ORG_ID = 0) And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And p.OBJ_ATTENDANT_ID = _validate.OBJ_ATTENDANT_ID _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    Else
                        'Nơi làm việc - Ngày hiệu lực từ 
                        If (IsNumeric(_validate.WORKPLACE_ID) AndAlso _validate.WORKPLACE_ID > 0) Then
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.WORKPLACE_ID = 0) And p.WORKPLACE_ID = _validate.WORKPLACE_ID _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        Else
                            'Còn lại chỉ xét Ngày hiệu lực từ
                            Query = (From p In Context.AT_SIGNDEFAULT_ORG
                                     Where p.ID <> _validate.ID And (p.ORG_ID = Nothing Or p.WORKPLACE_ID = 0) And (p.WORKPLACE_ID = Nothing Or p.WORKPLACE_ID = 0) _
                                     And (p.OBJ_EMPLOYEE_ID = Nothing Or p.OBJ_EMPLOYEE_ID = 0) And (p.OBJ_ATTENDANT_ID = Nothing Or p.OBJ_ATTENDANT_ID = 0) _
                                     And (_validate.EFFECT_DATE_FROM = p.FROMDATE_EFFECT)).ToList.Count
                        End If
                    End If
                End If
            End If

            If Query > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_SIGNDEFAULT_ORG)
        Try
            lstData = (From p In Context.AT_SIGNDEFAULT_ORG Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of AT_SIGNDEFAULT_ORG)
        Try
            lstData = (From p In Context.AT_SIGNDEFAULT_ORG Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.AT_SIGNDEFAULT_ORG.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTerminal")
            Throw ex
        End Try
    End Function
#End Region
#Region "đăng ký nghỉ trên iportal"
    Public Function GetHolidayByCalenderToTable(ByVal startdate As Date, ByVal enddate As Date) As DataTable
        Try
            Return (From p In Context.AT_HOLIDAY
                    Where p.WORKINGDAY >= startdate And p.WORKINGDAY <= enddate _
                    And p.ACTFLG = ATConstant.ACTFLG_ACTIVE
                    Select p.WORKINGDAY.Value,
                            p.OFFDAY).ToList().ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".")
        End Try
    End Function
    Public Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal, ByVal startdate As DateTime, ByVal enddate As DateTime, _
                                                    ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim lstSignID As List(Of Decimal)
        Try

            Dim lstValue = (From p In Context.OT_OTHER_LIST).ToList
            lstSignID = (From p In listSign Select p.ID).ToList

            Dim qr = From p In Context.AT_TIMESHEET_REGISTER
                     Join e In Context.HU_EMPLOYEE On p.HU_EMPLOYEEID Equals e.ID
                     Join sign In Context.AT_TIME_MANUAL On p.AT_SIGNID Equals sign.ID
                     Group Join rgtext In Context.AT_TIMESHEET_REGISTER On p.ID Equals rgtext.ID Into rgt_ext = Group
                     From rgtext In rgt_ext.DefaultIfEmpty
                     Where lstSignID.Contains(p.AT_SIGNID) _
                     And p.HU_EMPLOYEEID = empid _
                     And p.WORKINGDAY >= startdate _
                     And p.WORKINGDAY <= enddate
                     Order By sign.CODE


            rtnValue = (From p In qr.AsEnumerable
                        Select New AT_TIMESHEET_REGISTERDTO With {.ID = p.p.ID,
                                                         .EMPLOYEEID = p.p.HU_EMPLOYEEID,
                                                         .EMPLOYEECODE = p.e.EMPLOYEE_CODE,
                                                         .EMPLOYEENAME = p.e.FULLNAME_VN,
                                                         .SIGNID = p.sign.ID,
                                                         .SIGNTYPE = p.sign.ID,
                                                         .SIGNCODE = p.sign.CODE,
                                                         .SIGNNAME = p.sign.NAME,
                                                         .WORKINGDAY = p.p.WORKINGDAY,
                                                         .NVALUE = p.p.NVALUE,
                                                         .SVALUE = p.p.SVALUE,
                                                         .DVALUE = p.p.DVALUE,
                                                         .NVALUE_ID = p.p.NVALUE_ID,
                                                         .NOTE = p.p.NOTE}).ToList()

            Return rtnValue
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".")
        Finally

        End Try
    End Function

    Public Function CheckRegisterPortal(ByVal Emp As EmployeeDTO, ByVal ID_REGGROUP As Guid, ByVal process As String,
                                       ByVal startdate As Date, ByVal enddate As Date, _
                                      ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String) As Boolean

        Try
            Dim lstEmp As New List(Of EmployeeDTO)
            lstEmp.Add(Emp)
            Select Case sAction
                Case "ExistPortal"
                    Return ExistPortal(Emp, startdate, enddate, sign_code, sAction)
            End Select

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckRegisterAppointmentByEmployee")
            Throw ex
        End Try
    End Function

    Private Function ExistPortal(ByVal Emp As EmployeeDTO, ByVal startdate As Date, ByVal enddate As Date,
                                      ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String) As Boolean
        Dim signID As Decimal = sign_code.SIGNID
        Dim EmpID As Decimal = Emp.ID
        Try

            Dim query = (From p In Context.AT_PORTAL_REG
                         Where EmpID = p.ID_EMPLOYEE And _
                         p.ID_SIGN = sign_code.SIGNID And _
                         p.FROM_DATE >= startdate And _
                         p.FROM_DATE <= enddate).Count

            Return If(query > 0, False, True)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                             ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Try
            _isAvailable = False
            Dim lstValue = (From p In Context.OT_OTHER_LIST Where p.OT_OTHER_LIST_TYPE.CODE = ATConstant.CODE_TIMELEAVE).ToList
            Dim lstSignId As List(Of Decimal) = (From p In listSign Select p.ID).ToList()
            Dim query = (From p In Context.AT_PORTAL_REG
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                         From AT In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                         Where p.ID_EMPLOYEE = empid _
                         And lstSignId.Contains(p.ID_SIGN) _
                         And p.FROM_DATE >= startdate.Date _
                         And p.FROM_DATE <= enddate.Date _
                         And status.Contains(p.STATUS)
                         Select p, e, AT).ToList

            rtnValue = (From p In query
                        Select New AT_TIMESHEET_REGISTERDTO With {.ID = p.p.ID,
                                                         .EMPLOYEEID = p.e.ID,
                                                         .EMPLOYEECODE = p.e.EMPLOYEE_CODE,
                                                         .EMPLOYEENAME = p.e.FULLNAME_VN,
                                                         .WORKINGDAY = p.p.FROM_DATE,
                                                         .SIGNID = p.p.ID_SIGN,
                                                         .MORNING_ID = p.AT.MORNING_ID,
                                                         .AFTERNOON_ID = p.AT.AFTERNOON_ID,
                                                         .NVALUE = p.p.NVALUE,
                                                         .SVALUE = p.p.SVALUE,
                                                         .DVALUE = p.p.DVALUE,
                                                         .NOTE = p.p.NOTE,
                                                         .STATUS = p.p.STATUS,
                                                         .SUBJECT = If(p.p.SVALUE = "WLEO", p.p.NVALUE, Nothing) & " " & FormatRegisterAppointmentSubjectPortal(p.AT, p.p, lstValue)}).ToList
            Return rtnValue
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRegisterAppointmentInPortalByEmployee")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetRegisterAppointmentInPortalOT(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                         ByVal listSign As List(Of OT_OTHERLIST_DTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim rtnValue As List(Of AT_TIMESHEET_REGISTERDTO)
        Try
            _isAvailable = False
            Dim lstValue = (From p In Context.OT_OTHER_LIST).ToList
            Dim lstSignId As List(Of Decimal) = (From p In listSign Select p.ID).ToList()
            Dim query = (From p In Context.AT_PORTAL_REG
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                         From AT In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                         Where p.ID_EMPLOYEE = empid _
                         And lstSignId.Contains(p.ID_SIGN) _
                         And p.FROM_DATE >= startdate.Date _
                         And p.FROM_DATE <= enddate.Date _
                         And status.Contains(p.STATUS)
                         Select p, e, AT).ToList

            rtnValue = (From p In query
                        Select New AT_TIMESHEET_REGISTERDTO With {.ID = p.p.ID,
                                                         .EMPLOYEEID = p.e.ID,
                                                         .EMPLOYEECODE = p.e.EMPLOYEE_CODE,
                                                         .EMPLOYEENAME = p.e.FULLNAME_VN,
                                                         .WORKINGDAY = p.p.FROM_DATE,
                                                         .SIGNID = p.p.ID_SIGN,
                                                         .NVALUE = p.p.NVALUE,
                                                         .SVALUE = p.p.SVALUE,
                                                         .DVALUE = p.p.DVALUE,
                                                         .NOTE = p.p.NOTE,
                                                         .STATUS = p.p.STATUS,
                                                         .SUBJECT = String.Format("{0} [{1}-{2}]    {3}", p.AT.CODE,
                                                                     If(p.p.FROM_HOUR.HasValue, p.p.FROM_HOUR.Value.ToString("HH:mm"), ""),
                                                                     If(p.p.TO_HOUR.HasValue, p.p.TO_HOUR.Value.ToString("HH:mm"), ""),
                                                                     If(p.p.IS_NB = -1, "Nghỉ bù", Nothing))}).ToList
            Return rtnValue
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRegisterAppointmentInPortalOT")
        Finally
            _isAvailable = True
        End Try
    End Function


    Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal
        Try
            _isAvailable = False
            Dim result As Decimal

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRegisterAppointmentInPortalByEmployee")
        Finally
            _isAvailable = True
        End Try
    End Function





    Public Function DeleteRegisterLeavePortal(ByVal EmpID As Decimal, ByVal startdate As Date, ByVal enddate As Date, _
                                                  ByVal sign_code As AT_TIME_MANUALDTO, ByVal process As String) As Boolean
        Dim query As List(Of AT_PORTAL_REG)
        Dim signID = If(sign_code Is Nothing, 0, sign_code.ID)
        Dim lstSignIDNotSum As New List(Of Decimal)
        Try

            query = (From p In Context.AT_PORTAL_REG
                     Where EmpID = p.ID_EMPLOYEE _
                     And p.FROM_DATE >= startdate _
                     And p.FROM_DATE <= enddate _
                     And p.ID_SIGN <> signID Select p).ToList


            If sign_code IsNot Nothing Then
                ' xóa thông tin ký hiệu đang được đăng ký mà tồn tại trong db
                Dim delShift
                ' nếu là đăng ký nghỉ thì không cần kiểm tra theo id_sign là đăng đi DMVS thì kiểm tra thêm cả id_sing
                If process = "LEAVE" Then
                    delShift = (From p In Context.AT_PORTAL_REG
                                Where p.FROM_DATE >= startdate _
                                And p.FROM_DATE <= enddate _
                                And p.ID_SIGN = signID _
                                And p.ID_EMPLOYEE = EmpID _
                                And p.SVALUE = process _
                                Select p).ToList
                Else
                    delShift = (From p In Context.AT_PORTAL_REG
                                Where p.FROM_DATE >= startdate _
                                And p.FROM_DATE <= enddate _
                                And p.ID_SIGN = signID _
                                And p.ID_EMPLOYEE = EmpID _
                                And p.SVALUE = process _
                                And p.ID_SIGN = sign_code.ID _
                                Select p).ToList
                End If

                For Each shift In delShift
                    Context.AT_PORTAL_REG.DeleteObject(shift)
                Next
            End If

            Dim startDateInc = startdate
            Do

                Dim delShift
                If process = "LEAVE" Then
                    delShift = (From p In Context.AT_PORTAL_REG
                                Where EntityFunctions.TruncateTime(p.FROM_DATE) = EntityFunctions.TruncateTime(startDateInc) _
                                And p.ID_SIGN <> signID And EmpID = p.ID_EMPLOYEE _
                                And p.STATUS <> RegisterStatus.Approved _
                                And p.SVALUE = process
                                Select p).ToList
                Else
                    delShift = (From p In Context.AT_PORTAL_REG
                                Where EntityFunctions.TruncateTime(p.FROM_DATE) = EntityFunctions.TruncateTime(startDateInc) _
                                And p.ID_SIGN <> signID And EmpID = p.ID_EMPLOYEE _
                                And p.STATUS <> RegisterStatus.Approved _
                                And p.SVALUE = process _
                                And p.ID_SIGN = sign_code.ID _
                                Select p).ToList
                End If

                For Each shift In delShift
                    Context.AT_PORTAL_REG.DeleteObject(shift)
                Next

                startDateInc = startDateInc.AddDays(1)
            Loop Until startDateInc > enddate

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteRegisterLeaveByEmployee")
            Throw ex
        End Try
    End Function

    Public Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        Try
            Return (From p In Context.AT_HOLIDAY
                    Where p.WORKINGDAY >= startdate And p.WORKINGDAY <= enddate _
                    And p.ACTFLG = ATConstant.ACTFLG_ACTIVE
                    Select p.WORKINGDAY.Value).ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".")
        End Try
    End Function

    Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        Dim delLstObject As List(Of AT_PORTAL_REG)
        Try
            delLstObject = (From z In listappointment
                            From p In Context.AT_PORTAL_REG
                            From s In listSign
                            Where p.ID_EMPLOYEE = z.EMPLOYEEID And s.ID = p.ID_SIGN _
                            And p.FROM_DATE = z.WORKINGDAY And p.STATUS <> RegisterStatus.Approved
                            Select p).ToList()
            If delLstObject.Count = 0 Then
                delLstObject = (From z In listappointment
                                From p In Context.AT_PORTAL_REG
                                From s In listSign
                                Where s.ID = p.ID_SIGN _
                                And p.ID = z.ID And p.STATUS <> RegisterStatus.Approved
                                Select p).ToList()
            End If

            For Each delObj As AT_PORTAL_REG In delLstObject
                Context.AT_PORTAL_REG.DeleteObject(delObj)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePortalRegisterByDate")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalRegisterByDateOT(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of OT_OTHERLIST_DTO)) As Boolean
        Dim delLstObject As List(Of AT_PORTAL_REG)
        Try
            _isAvailable = False

            delLstObject = (From z In listappointment
                            From p In Context.AT_PORTAL_REG
                            From s In listSign
                            Where p.ID_EMPLOYEE = z.EMPLOYEEID And s.ID = p.ID_SIGN _
                            And p.FROM_DATE = z.WORKINGDAY And p.STATUS <> RegisterStatus.Approved
                            Select p).ToList()

            If delLstObject.Count = 0 Then
                delLstObject = (From z In listappointment
                                From p In Context.AT_PORTAL_REG
                                From s In listSign
                                Where p.ID = z.ID _
                                 And p.STATUS <> RegisterStatus.Approved
                                Select p).ToList()
            End If

            For Each delObj As AT_PORTAL_REG In delLstObject
                Context.AT_PORTAL_REG.DeleteObject(delObj)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePortalRegisterByDate")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        Try
            _isAvailable = False

            Dim delObj As AT_PORTAL_REG = GetObjectById(Of AT_PORTAL_REG)(id)

            If delObj IsNot Nothing Then
                Context.AT_PORTAL_REG.DeleteObject(delObj)
                Context.SaveChanges()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal),
                                          ByVal process As String,
                                          ByVal currentUrl As String) As String
        Dim itemIns As AT_PORTAL_APP
        Dim datecount As Decimal
        Dim objLstRegister As List(Of AT_PORTAL_REG)
        Dim groupid As Guid = Guid.NewGuid

        objLstRegister = (From p In Context.AT_PORTAL_REG
                          Where objLstRegisterId.Contains(p.ID)
                          Select p).ToList()

        If objLstRegister Is Nothing OrElse objLstRegister.Count = 0 Then Return String.Empty

        Dim listApproveUser As List(Of ApproveUserDTO) = GetApproveUsers(objLstRegister(0).ID_EMPLOYEE, process)

        If listApproveUser Is Nothing OrElse listApproveUser.Count = 0 Then
            Return "Chưa thiết lập phê duyệt hoặc người phê duyệt không tồn tại."
        End If

        For Each itm As AT_PORTAL_REG In objLstRegister
            'itm.ID_REGGROUP = groupid
            itm.STATUS = RegisterStatus.WaitForApprove
        Next

        datecount = objLstRegister.Sum(Function(rgt) rgt.NVALUE)

        ' Tiến hành tạo bản ghi phê duyệt cho bản ghi đăng ký này
        Dim idSendMail As Decimal? = Nothing
        Dim emailInform As String = ""
        For Each itemApprove In listApproveUser
            For Each i In objLstRegister.GroupBy(Function(x) New AT_PORTAL_REG With {.ID_REGGROUP = x.ID_REGGROUP})
                itemIns = New AT_PORTAL_APP With {.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_APP.EntitySet.Name), .ID_REGGROUP = objLstRegister(0).ID_REGGROUP,
                                                  .ID_EMPLOYEE = itemApprove.EMPLOYEE_ID,
                                                  .level = itemApprove.LEVEL}

                If itemApprove.LEVEL = 1 Then
                    itemIns.APPROVE_STATUS = RegisterStatus.WaitForApprove ' cho người phê duyệt cấp 1 có trạng thái là chờ phê duyệt.
                    itemIns.ID_REGGROUP = i.Key.ID_REGGROUP
                    'idSendMail = objLstRegister(0).ID_EMPLOYEE
                    idSendMail = itemIns.ID_EMPLOYEE

                    If datecount >= itemApprove.INFORM_DATE Then
                        emailInform = itemApprove.INFORM_EMAIL
                    End If
                Else
                    itemIns.APPROVE_STATUS = RegisterStatus.Regist    ' cho các người phê duyệt khác là đang chờ đến lượt.
                End If

                Context.AT_PORTAL_APP.AddObject(itemIns)
            Next
        Next

        Context.SaveChanges()

        'Tiến hành gửi email cho cấp 1
        If idSendMail.HasValue Then
            ' Kiểm tra nhân viên thay thế
            Dim process_id = (From s In Context.SE_APP_PROCESS
                              Where s.PROCESS_CODE = process Select s).FirstOrDefault.ID

            Dim approveExt = (From p In Context.SE_APP_SETUPEXT
                              Where p.EMPLOYEE_ID = idSendMail.Value _
                              And p.PROCESS_ID = process_id _
                              And p.FROM_DATE <= Date.Today _
                              And p.TO_DATE >= Date.Today).FirstOrDefault

            If process = "OVERTIME" Then
                SendMail(SendMailType.RegToAppTime, objLstRegister(0).ID_EMPLOYEE,
                    idSendMail.Value, If(approveExt Is Nothing, Nothing, approveExt.SUB_EMPLOYEE_ID),
                    process, objLstRegister(0).ID_REGGROUP, currentUrl, emailInform, "")
            Else
                SendMail(SendMailType.RegToApp, objLstRegister(0).ID_EMPLOYEE,
                    idSendMail.Value, If(approveExt Is Nothing, Nothing, approveExt.SUB_EMPLOYEE_ID),
                    process, objLstRegister(0).ID_REGGROUP, currentUrl, emailInform, "")
            End If

        End If

        Return String.Empty
    End Function
#End Region

#Region "Send mail"
    Private Enum SendMailType
        RegToApp
        AppToRegLv1
        AppToReg
        DenyToReg
        RegToAppTime
        AppToRegTime
        AppToRegTimeLv1
        DenyToRegTime
        DelToWaitApp
    End Enum

    Private Function SendMail(ByVal typeSend As SendMailType, ByVal registerId As Decimal,
                              ByVal approveId As Decimal, ByVal approveExtId As Decimal?,
                              ByVal process As String, ByVal registerRecordId As Decimal?,
                              ByVal url As String, Optional ByVal ccEmail As String = "",
                              Optional ByVal sReason As String = "")
        Try
            'Dim config = GetConfig(ModuleID.All)
            Dim _viewname As String = ""
            Dim _from As String = "", _to As String = "", _cc As String = "", _subject As String = "", _content As String = "", _link As String = ""

            Dim config As Dictionary(Of String, String)
            config = GetConfig(ModuleID.All)
            Dim mailfrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")


            'Dim mailfrom = ConfigurationManager.AppSettings("EMAIL_FROM")
            If mailfrom = "" Then
                Return False
            End If

            Dim _listField As Dictionary(Of String, String) = _
                New Dictionary(Of String, String) From {{"[HR_User_Email]", ""}, _
                                                        {"[Request_User_Id]", ""}, _
                                                        {"[Request_User_Email]", ""}, _
                                                        {"[Request_User_Full_Name]", ""}, _
                                                        {"[Request_User_Position]", ""}, _
                                                        {"[Request_User_Location]", ""}, _
                                                        {"[Approve_User_Id]", ""}, _
                                                        {"[Approve_User_Email]", ""}, _
                                                        {"[ApproveExt_User_Email]", ""}, _
                                                        {"[Approve_User_Full_Name]", ""}, _
                                                        {"[Approve_User_Position]", ""}, _
                                                        {"[Approve_User_Location]", ""}, _
                                                        {"[Sign_Name]", ""}, _
                                                        {"[RJ_REASON]", ""}, _
                                                        {"[Start_Date]", ""}, _
                                                        {"[End_Date]", ""}, _
                                                        {"[Tu_Gio]", ""}, _
                                                        {"[Den_Gio]", ""}, _
                                                        {"[Nghi_Bu]", ""}, _
                                                        {"[DirectLink]", ""}
                                                       }

            ' lấy dữ liệu cho các field
            ' - Lấy dữ liệu của quy trình, Lấy [HR_User_Email]
            'Dim processInfo = Context.AT_APP_PROCESS.FirstOrDefault(Function(p) p.PROCESS_CODE = process)
            'If processInfo Is Nothing Then
            '    Return False
            'End If
            _listField("[HR_User_Email]") = ccEmail

            Dim registerRecordInfo = (From p In Context.AT_PORTAL_REG
                                      From s In Context.AT_TIME_MANUAL.Where(Function(F) F.ID = p.ID_SIGN).DefaultIfEmpty
                                      Where p.ID_REGGROUP = registerRecordId
                                      Select p, s
                                      Order By p.FROM_DATE).ToList()

            If registerRecordInfo Is Nothing Then
                Return False
            End If
            Dim sValueName As String = ""
            Select Case process
                Case ATConstant.GSIGNCODE_LEAVE
                    sValueName = "day"
                Case ATConstant.GSIGNCODE_OVERTIME
                    sValueName = "hour"
                Case ATConstant.GSIGNCODE_WLEO
                    sValueName = "hour"
            End Select
            ' - Lấy thông tin về mã đăng ký [Sign_Code]
            For Each item In registerRecordInfo
                If process <> ATConstant.GSIGNCODE_OVERTIME Then
                    If Not _listField("[Sign_Name]").Contains(item.s.NAME + "-") Then
                        Dim sSignName As String = item.s.NAME
                        Dim dValue As String = Format(registerRecordInfo _
                            .Where(Function(f) f.p.ID_SIGN = item.p.ID_SIGN) _
                            .Sum(Function(p) p.p.NVALUE.Value), "0.##")

                        _listField("[Sign_Name]") += item.s.NAME & "-" & dValue & " " & sValueName & ", "
                    End If
                End If
                _listField("[RJ_REASON]") = item.p.NOTE
                If process = ATConstant.GSIGNCODE_OVERTIME Then
                    _listField("[Tu_Gio]") = item.p.FROM_HOUR.Value.ToString("HH:mm")
                    _listField("[Den_Gio]") = item.p.TO_HOUR.Value.ToString("HH:mm")
                    _listField("[Nghi_Bu]") = If(item.p.IS_NB = -1, "Có", "Không")
                End If

            Next
            If process <> ATConstant.GSIGNCODE_OVERTIME Then
                _listField("[Sign_Name]") = _listField("[Sign_Name]").Substring(0, _listField("[Sign_Name]").Length - 2)
            End If
            ' - Lấy thông tin [Start_Date]
            _listField("[Start_Date]") = registerRecordInfo(0).p.FROM_DATE.Value.ToString("dd/MM/yyyy")
            ' - Lấy thông tin [End_Date]
            _listField("[End_Date]") = registerRecordInfo(registerRecordInfo.Count - 1).p.FROM_DATE.Value.ToString("dd/MM/yyyy")


            ' - Lấy thông tin của người đăng ký
            Dim registerInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = registerId)
            Dim infoEmail = Context.HU_EMPLOYEE_CV.SingleOrDefault(Function(p) p.EMPLOYEE_ID = registerId)
            If registerInfo Is Nothing Then
                Return False
            End If
            _listField("[Request_User_Id]") = registerInfo.EMPLOYEE_CODE
            _listField("[Request_User_Email]") = infoEmail.WORK_EMAIL
            _listField("[Request_User_Full_Name]") = registerInfo.FULLNAME_VN
            '_listField("[Request_User_Location]") = registerInfo.HU_ORGANIZATION.NAME_VN
            '_listField("[Request_User_Position]") = registerInfo.HU_TITLE.NAME_VN
            If sReason <> "" Then
                _listField("[RJ_REASON]") = sReason
            End If

            ' - Lấy thông tin của người phê duyệt
            Dim approveInfo
            Dim approveExtInfo


            If approveExtId Is Nothing Or approveExtId = 0 Then
                'approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveId)
                approveInfo = (From p In Context.HU_EMPLOYEE
                               From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                               From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                               From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                               Where p.ID = approveId
                               Select p, c, o, t
                               Order By p.ID).ToList()
                If approveInfo Is Nothing Then
                    Return False
                End If

                For Each info In approveInfo
                    _listField("[Approve_User_Id]") = info.p.EMPLOYEE_CODE
                    _listField("[Approve_User_Email]") = info.c.WORK_EMAIL
                    _listField("[Approve_User_Full_Name]") = info.p.FULLNAME_VN
                    _listField("[Approve_User_Location]") = info.o.NAME_VN
                    _listField("[Approve_User_Position]") = info.t.NAME_VN
                    If ccEmail = "" Or ccEmail Is Nothing Then
                        _listField("[HR_User_Email]") = info.c.WORK_EMAIL
                    End If
                Next

            Else
                'approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveId)
                'approveExtInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveExtId)

                approveInfo = (From p In Context.HU_EMPLOYEE
                               From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                               From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                               From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                               Where p.ID = approveId
                               Select p, c, o, t
                               Order By p.ID).FirstOrDefault()

                approveExtInfo = (From p In Context.HU_EMPLOYEE
                                  From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                                  From o In Context.HU_ORGANIZATION.Where(Function(F) F.ID = p.ORG_ID).DefaultIfEmpty
                                  From t In Context.HU_TITLE.Where(Function(F) F.ID = p.TITLE_ID).DefaultIfEmpty
                                  Where p.ID = approveExtId
                                  Select p, c, o, t
                                  Order By p.ID).FirstOrDefault()


                _listField("[Approve_User_Id]") = approveExtInfo.p.EMPLOYEE_CODE
                _listField("[Approve_User_Email]") = approveExtInfo.c.WORK_EMAIL
                _listField("[ApproveExt_User_Email]") = approveInfo.c.WORK_EMAIL
                _listField("[Approve_User_Full_Name]") = approveExtInfo.p.FULLNAME_VN
                _listField("[Approve_User_Location]") = approveExtInfo.o.NAME_VN
                _listField("[Approve_User_Position]") = approveExtInfo.t.NAME_VN
            End If



            _link = url & ":" & If(config.ContainsKey("PortalPort"), config("PortalPort"), "") & "?fid=ctrl[0][1]&mid=Attendance"

            Select Case process
                Case ATConstant.GSIGNCODE_OVERTIME
                    _link = _link.Replace("[0]", "OT")
                Case ATConstant.GSIGNCODE_WLEO
                    _link = _link.Replace("[0]", "DMVS")
                Case ATConstant.GSIGNCODE_LEAVE
                    _link = _link.Replace("[0]", "Leave")
            End Select

            Select Case typeSend
                Case SendMailType.RegToApp
                    _from = mailfrom
                    _to = "[HR_User_Email]"
                    _cc = "[Request_User_Email]"
                    _viewname = "AT_TIME_REGTOAPP.html"
                    _link = url & ":" & If(config.ContainsKey("PortalPort"),
                                           config("PortalPort"), "") & _
                                       "/ProcessApprove.aspx?process=[0]&action=[1]" & _
                                       "&reggroup=" & registerRecordId.ToString & _
                                       "&approveid=" & approveId & _
                                       "&status=" & 2

                    Select Case process
                        Case ATConstant.GSIGNCODE_OVERTIME
                            _link = _link.Replace("[0]", "OT")
                        Case ATConstant.GSIGNCODE_WLEO
                            _link = _link.Replace("[0]", "DMVS")
                        Case ATConstant.GSIGNCODE_LEAVE
                            _link = _link.Replace("[0]", "Leave")
                    End Select

                    _listField("[Directlink]") = _link.Replace("[1]", "Approve")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.AppToRegLv1
                    _from = mailfrom
                    _to = "[Approve_User_Email]"
                    _cc = "[Request_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_APPTOREG_LV1.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.AppToReg
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_APPTOREG.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.DenyToReg
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_DENYTOREG.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
                Case SendMailType.RegToAppTime
                    _from = mailfrom
                    _to = "[HR_User_Email]"
                    _cc = "[Request_User_Email]"
                    _viewname = "AT_TIME_TOREGOT.html"
                    _link = url & ":" & If(config.ContainsKey("PortalPort"),
                                           config("PortalPort"), "") & _
                                       "/ProcessApprove.aspx?process=[0]&action=[1]" & _
                                       "&reggroup=" & registerRecordId.ToString & _
                                       "&approveid=" & approveId & _
                                       "&status=" & 2

                    Select Case process
                        Case ATConstant.GSIGNCODE_OVERTIME
                            _link = _link.Replace("[0]", "OT")
                        Case ATConstant.GSIGNCODE_WLEO
                            _link = _link.Replace("[0]", "DMVS")
                        Case ATConstant.GSIGNCODE_LEAVE
                            _link = _link.Replace("[0]", "Leave")
                    End Select

                    _listField("[Directlink]") = _link.Replace("[1]", "Approve")
                    GetContentTemplate(_viewname, _subject, _content)

                Case SendMailType.AppToRegTime
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_APPTOREGOT.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)

                Case SendMailType.AppToRegTimeLv1
                    _from = mailfrom
                    _to = "[Approve_User_Email]"
                    _cc = "[Request_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_APPTOREGOT_LV1.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)

                Case SendMailType.DenyToRegTime
                    _from = mailfrom
                    _to = "[Request_User_Email]"
                    _cc = "[Approve_User_Email];[HR_User_Email];[ApproveExt_User_Email]"
                    _viewname = "AT_TIME_DENYTOREGOT.html"
                    _listField("[Directlink]") = _link.Replace("[1]", "Register")
                    GetContentTemplate(_viewname, _subject, _content)
            End Select

            Return InsertMail(_from.ReplaceField(_listField), _to.ReplaceField(_listField),
                              _subject.ReplaceField(_listField), _content.ReplaceField(_listField),
                              _cc.ReplaceField(_listField), , _viewname)


        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Public Function InsertMail(ByVal _from As String, ByVal _to As String,
                               ByVal _subject As String, ByVal _content As String,
                               Optional ByVal _cc As String = "", Optional ByVal _bcc As String = "",
                               Optional ByVal _viewName As String = "", Optional ByVal attachUrl As String = "") As Boolean
        Try

            If _from = String.Empty OrElse _to = String.Empty Then
                Return False
            End If

            Dim _newMail As New SE_MAIL
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
            _newMail.MAIL_FROM = _from
            _newMail.MAIL_TO = _to
            _newMail.MAIL_CC = SplipKey(_cc)
            _newMail.MAIL_BCC = _bcc
            _newMail.SUBJECT = _subject
            _newMail.CONTENT = _content
            _newMail.VIEW_NAME = _viewName
            _newMail.ATTACHMENT = attachUrl
            _newMail.ACTFLG = "I"

            Context.SE_MAIL.AddObject(_newMail)

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Private Function SplipKey(ByVal value As String) As String
        Dim key As String = ""
        If value = "" Or value Is Nothing Then
            Return key
        End If
        Dim array() As String = value.Split(";")

        For Each item As String In array
            If Not key.Contains(item) Then
                key = key + item + ";"
            End If
        Next

        Return key
    End Function
#End Region

#Region "Phê duyệt đăng ký nghỉ"
    Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        Try
            _isAvailable = False
            Return (From p In Context.AT_TIME_MANUAL.ToList
                    Group Join ol In Context.OT_OTHER_LIST On p.ID Equals ol.ID Into g_ol = Group
                    From pol In g_ol.DefaultIfEmpty
                    Where p.CODE = gSignCode _
                    And p.ACTFLG = ATConstant.ACTFLG_ACTIVE _
                    And pol.CODE = "RGT"
                    Select New AT_TIME_MANUALDTO With {.ID = p.ID,
                                              .CODE = p.CODE,
                                              .NAME_VN = p.NAME,
                                              .NOTE = p.NOTE}).ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function ApprovePortalRegister(ByVal regID As Decimal?, ByVal approveId As Decimal,
                                          ByVal status As Integer, ByVal note As String,
                                          ByVal currentUrl As String, ByVal process As String,
                                             ByVal log As UserLog) As Boolean
        Try
            _isAvailable = False
            Dim approveRecord As AT_PORTAL_APP
            Dim registerRecord As List(Of AT_PORTAL_REG)
            Dim bExt As Boolean = False
            Dim approveExt As SE_APP_SETUPEXT
            Dim approveInfo As HU_EMPLOYEE
            Dim approveExtInfo As HU_EMPLOYEE

            approveRecord = (From a In Context.AT_PORTAL_APP
                             Where a.ID_REGGROUP = regID _
                             And a.ID_EMPLOYEE = approveId _
                             And a.APPROVE_STATUS = 1
                             Select a).FirstOrDefault



            ' Lấy bản ghi đang chờ phê duyệt của bản đăng ký đang phê duyệt
            If approveRecord Is Nothing Then

                ' Kiểm tra nhân viên thay thế
                'approveExt = (From p In Context.SE_APP_SETUPEXT
                '              From t In Context.SE_APP_PROCESS.Where(Function(f) p.PROCESS_ID = f.ID).DefaultIfEmpty
                '               Where p.PROCESS_ID = approveId _
                '               And t.PROCESS_CODE = process _
                '               And p.FROM_DATE <= Date.Today _
                '               And p.TO_DATE >= Date.Today).FirstOrDefault


                'approveRecord = (From a In Context.AT_PORTAL_APP
                '                     Where a.ID_REGGROUP = regID _
                '                     And a.ID_EMPLOYEE = approveExt.EMPLOYEE_ID _
                '                     And a.APPROVE_STATUS = 1
                '                     Select a).FirstOrDefault

                'approveExtInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveExt.SUB_EMPLOYEE_ID)
                'bExt = True
            Else
                approveInfo = Context.HU_EMPLOYEE.SingleOrDefault(Function(p) p.ID = approveRecord.ID_EMPLOYEE)
            End If


            ' cập nhật lại trạng thái của bản ghi phê duyệt
            approveRecord.APPROVE_STATUS = status
            approveRecord.APPROVE_DATE = Date.Now

            registerRecord = (From a In Context.AT_PORTAL_REG
                              Where a.ID_REGGROUP = regID
                              Select a).ToList()
            If note = "" Then
                note = "Đồng ý"
            End If
            registerRecord.ForEach(Sub(x) x.NOTE &= approveRecord.level & ". " & _
                                       If(bExt, approveExtInfo.FULLNAME_VN, approveInfo.FULLNAME_VN) & ": " & note & "<br />")

            ' * nếu trạng thái bản ghi là chưa phê duyệt (-1) thì set trạng thái sang đang chờ phê duyệt (1)
            If registerRecord(0).STATUS = -1 Then
                'registerRecord.STATUS = 1
                registerRecord.ForEach(Sub(x) x.STATUS = 1)
            End If

            ' * Nếu không phê duyệt
            If status = RegisterStatus.Denied Then
                ' - Dừng và Email về cho người đăng ký.
                ' + Set trạng thái lại cho bản ghi đăng ký là không phê duyệt

                Dim ApproveRecords1 = From a In Context.AT_PORTAL_APP
                                      From t In Context.HU_EMPLOYEE_CV.Where(Function(f) a.ID_EMPLOYEE = f.EMPLOYEE_ID).DefaultIfEmpty
                                      Where a.ID_REGGROUP = regID _
                                      And a.level <> approveRecord.level
                                      Select a, t
                Dim EMAILCC As String
                If ApproveRecords1.Count > 0 Then
                    For Each item In ApproveRecords1
                        EMAILCC = EMAILCC + If(item.t.WORK_EMAIL <> "", item.t.WORK_EMAIL + ";", Nothing)
                    Next
                End If

                'registerRecord(0).STATUS = status
                registerRecord.ForEach(Sub(x) x.STATUS = status)

                ' + Gửi email
                If process = ATConstant.GSIGNCODE_OVERTIME Then
                    SendMail(SendMailType.DenyToRegTime, registerRecord(0).ID_EMPLOYEE,
                        approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing), process,
                        registerRecord(0).ID_REGGROUP, currentUrl, EMAILCC, note)
                Else
                    SendMail(SendMailType.DenyToReg, registerRecord(0).ID_EMPLOYEE,
                        approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing), process,
                        registerRecord(0).ID_REGGROUP, currentUrl, EMAILCC, note)
                End If
                'Cập nhật thay đổi vào CSDL
                Context.SaveChanges()
                Return True
            End If

            ' Nếu là phê duyệt bản ghi, lấy cấp tiếp theo và cập nhật trạng thái là đang chờ phê duyệt
            Dim nextApproveRecords = From a In Context.AT_PORTAL_APP
                                     Where a.ID_REGGROUP = regID _
                                     And a.level = approveRecord.level + 1
                                     Select a


            ' * Nếu không có bản ghi nào => đây là bản ghi cuối cùng
            '   --> Cập nhật trạng thái bản đăng ký là đã phê duyệt
            If nextApproveRecords.Count() = 0 Then

                Dim ApproveRecords1 = From a In Context.AT_PORTAL_APP
                                      From t In Context.HU_EMPLOYEE_CV.Where(Function(f) a.ID_EMPLOYEE = f.EMPLOYEE_ID).DefaultIfEmpty
                                      Where a.ID_REGGROUP = regID _
                                      And a.level <> approveRecord.level
                                      Select a, t
                Dim EMAILCC As String
                If ApproveRecords1.Count > 0 Then
                    For Each item In ApproveRecords1
                        EMAILCC = EMAILCC + If(item.t.WORK_EMAIL <> "", item.t.WORK_EMAIL + ";", Nothing)
                    Next
                End If

                'registerRecord.STATUS = status
                registerRecord.ForEach(Sub(x) x.STATUS = status)

                '   --> Thực hiện chuyển dữ liệu bản ghi vào AT_RGT
                If Not ApprovePortalRegisterFinallize(registerRecord(0).ID_REGGROUP, log) Then
                    Return False
                End If
                ' đẩy dữ liệu sang phần bảng chấm công
                If process = "LEAVE" Then
                    INSERTAT_LEAVESHEET(registerRecord(0).ID_REGGROUP, log)
                ElseIf process = ATConstant.GSIGNCODE_OVERTIME Then
                    INSERTAT_OVERTIME(registerRecord(0).ID_REGGROUP, log)
                Else
                    INSERTAT_LATE_COMBACKOUT(registerRecord(0).ID_REGGROUP, log)
                End If

                ' + Gửi email cho người đăng ký
                If process = ATConstant.GSIGNCODE_OVERTIME Then
                    SendMail(SendMailType.AppToRegTime, registerRecord(0).ID_EMPLOYEE,
                             approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing),
                             process, registerRecord(0).ID_REGGROUP, currentUrl, EMAILCC, note)
                Else
                    SendMail(SendMailType.AppToReg, registerRecord(0).ID_EMPLOYEE,
                             approveId, If(bExt, approveExt.SUB_EMPLOYEE_ID, Nothing),
                             process, registerRecord(0).ID_REGGROUP, currentUrl, EMAILCC, note)
                End If

            Else
                For Each item In nextApproveRecords
                    item.APPROVE_STATUS = RegisterStatus.WaitForApprove  'Chờ phê duyệt
                    If process = ATConstant.GSIGNCODE_OVERTIME Then
                        SendMail(SendMailType.AppToRegTimeLv1, registerRecord(0).ID_EMPLOYEE,
                       approveId, If(item.ID_EMPLOYEE IsNot Nothing, item.ID_EMPLOYEE, Nothing),
                       process, registerRecord(0).ID_REGGROUP, currentUrl, "", note)
                    Else
                        SendMail(SendMailType.AppToRegLv1, registerRecord(0).ID_EMPLOYEE,
                       approveId, If(item.ID_EMPLOYEE IsNot Nothing, item.ID_EMPLOYEE, Nothing),
                       process, registerRecord(0).ID_REGGROUP, currentUrl, "", note)
                    End If


                Next

            End If

            'Cập nhật thay đổi vào CSDL
            Context.SaveChanges()

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Private Function INSERTAT_LEAVESHEET(ByVal regID As Decimal?, ByVal log As UserLog) As Boolean

        Dim leave As AT_LEAVESHEET
        Dim fromdate As Date?
        Dim todate As Date?
        Dim count As Decimal = 0
        Try
            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                 From s In Context.AT_TIME_MANUAL.Where(Function(C) C.ID = p.ID_SIGN).DefaultIfEmpty
                                 Where p.ID_REGGROUP = regID
                                 Select p, e, s

            If registerRecord Is Nothing Then
                Return False
            End If
            ' lấy ngày đầu và ngày cuối của kỳ nghỉ để đẩy sang công
            For Each i In registerRecord
                If fromdate Is Nothing Then
                    fromdate = i.p.FROM_DATE
                End If
                todate = i.p.TO_DATE
            Next



            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")

        End Try
    End Function

    Private Function INSERTAT_OVERTIME(ByVal regID As Decimal?, ByVal log As UserLog) As Boolean

        Dim ot As AT_REGISTER_OT
        Dim fromdate As Date?
        Dim todate As Date?
        Dim count As Decimal = 0
        Try
            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                 From s In Context.OT_OTHER_LIST.Where(Function(C) C.ID = p.ID_SIGN).DefaultIfEmpty
                                 Where p.ID_REGGROUP = regID
                                 Select p, e, s

            If registerRecord Is Nothing Then
                Return False
            End If
            ' lấy ngày đầu và ngày cuối của kỳ nghỉ để đẩy sang công
            For Each i In registerRecord
                If fromdate Is Nothing Then
                    fromdate = i.p.FROM_DATE
                End If
                todate = i.p.TO_DATE
            Next

            For Each itm In registerRecord
                Dim exists = (From r In Context.AT_REGISTER_OT Where r.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And r.WORKINGDAY = itm.p.FROM_DATE).Any
                If exists = False Then
                    ot = New AT_REGISTER_OT
                    ot.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                    ot.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    ot.WORKINGDAY = itm.p.FROM_DATE
                    ot.FROM_HOUR = itm.p.FROM_HOUR
                    ot.TO_HOUR = itm.p.TO_HOUR
                    ot.NOTE = itm.p.NOTE
                    ot.TYPE_INPUT = 0
                    ot.HOUR = itm.p.HOURCOUNT
                    ot.HS_OT = itm.p.ID_SIGN
                    ot.IS_NB = itm.p.IS_NB
                    ot.TYPE_OT = 0
                    Context.AT_REGISTER_OT.AddObject(ot)
                Else
                    Dim details = (From r In Context.AT_REGISTER_OT Where r.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And r.WORKINGDAY = itm.p.FROM_DATE).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_REGISTER_OT.DeleteObject(details(index))
                    Next
                    ot = New AT_REGISTER_OT
                    ot.ID = Utilities.GetNextSequence(Context, Context.AT_REGISTER_OT.EntitySet.Name)
                    ot.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    ot.WORKINGDAY = itm.p.FROM_DATE
                    ot.FROM_HOUR = itm.p.FROM_HOUR
                    ot.TO_HOUR = itm.p.TO_HOUR
                    ot.NOTE = itm.p.NOTE
                    ot.TYPE_INPUT = 0
                    ot.HOUR = itm.p.HOURCOUNT
                    ot.HS_OT = itm.p.ID_SIGN
                    ot.IS_NB = itm.p.IS_NB
                    ot.TYPE_OT = 0
                    Context.AT_REGISTER_OT.AddObject(ot)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")

        End Try
    End Function

    Private Function INSERTAT_LATE_COMBACKOUT(ByVal regID As Decimal?, ByVal log As UserLog) As Boolean

        Dim leave As AT_LATE_COMBACKOUT
        Dim fromdate As Date?
        Dim todate As Date?
        Dim count As Decimal = 0
        Try
            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 From e In Context.HU_EMPLOYEE.Where(Function(F) F.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                 From s In Context.AT_TIME_MANUAL.Where(Function(C) C.ID = p.ID_SIGN).DefaultIfEmpty
                                 Where p.ID_REGGROUP = regID
                                 Select p, e, s

            If registerRecord Is Nothing Then
                Return False
            End If
            ' lấy ngày đầu và ngày cuối của kỳ nghỉ để đẩy sang công
            For Each i In registerRecord
                If fromdate Is Nothing Then
                    fromdate = i.p.FROM_DATE
                End If
                todate = i.p.TO_DATE
            Next


            For Each itm In registerRecord
                Dim exists = (From p In Context.AT_LATE_COMBACKOUT _
                              Where (p.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And p.WORKINGDAY = itm.p.FROM_DATE And p.TYPE_DSVM = itm.p.ID_SIGN)).Any
                If exists = False Then
                    leave = New AT_LATE_COMBACKOUT
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.ORG_ID = itm.e.ORG_ID
                    leave.MINUTE = itm.p.NVALUE
                    leave.TITLE_ID = itm.e.TITLE_ID
                    leave.TYPE_DSVM = itm.p.ID_SIGN
                    leave.REMARK = itm.p.NOTE
                    Context.AT_LATE_COMBACKOUT.AddObject(leave)
                Else
                    Dim details = (From p In Context.AT_LATE_COMBACKOUT _
                                   Where (p.EMPLOYEE_ID = itm.p.ID_EMPLOYEE And p.WORKINGDAY = itm.p.FROM_DATE And p.TYPE_DSVM = itm.p.ID_SIGN)).ToList
                    For index = 0 To details.Count - 1
                        Context.AT_LATE_COMBACKOUT.DeleteObject(details(index))
                    Next
                    leave = New AT_LATE_COMBACKOUT
                    leave.ID = Utilities.GetNextSequence(Context, Context.AT_LATE_COMBACKOUT.EntitySet.Name)
                    leave.EMPLOYEE_ID = itm.p.ID_EMPLOYEE
                    leave.WORKINGDAY = itm.p.FROM_DATE
                    leave.ORG_ID = itm.e.ORG_ID
                    leave.MINUTE = itm.p.NVALUE
                    leave.TITLE_ID = itm.e.TITLE_ID
                    leave.TYPE_DSVM = itm.p.ID_SIGN
                    leave.REMARK = itm.p.NOTE
                    Context.AT_LATE_COMBACKOUT.AddObject(leave)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")

        End Try
    End Function

    Private Function ApprovePortalRegisterFinallize(ByVal regID As Decimal?,
                                             ByVal log As UserLog) As Boolean
        Dim itm As AT_PORTAL_REG
        Dim itemAppointment As AT_TIMESHEET_REGISTERDTO

        Try
            _isAvailable = False

            Dim registerRecord = From p In Context.AT_PORTAL_REG
                                 Where p.ID_REGGROUP = regID
                                 Select p

            If registerRecord Is Nothing Then
                Return False
            End If
            Dim listAppointment As New List(Of AT_TIMESHEET_REGISTERDTO)
            Dim lstDate As New List(Of Date)
            For Each itm In registerRecord
                nvalue_id = itm.NVALUE_ID
                If nvalue_id IsNot Nothing Then
                    Dim lstEmp As New List(Of Decimal)
                    lstEmp.Add(itm.ID_EMPLOYEE)

                    If Not lstDate.Contains(itm.FROM_DATE) Then
                        lstDate.Add(itm.FROM_DATE)
                        Dim dNValue As Double = registerRecord _
                                                .Where(Function(f) f.FROM_DATE = itm.FROM_DATE) _
                                                .Sum(Function(f) f.NVALUE.Value)
                        'DeleteRegisterLeaveByEmployee(lstEmp, itm.FROM_DATE.Value.Date, itm.FROM_DATE.Value.Date,
                        '                              New List(Of AT_FMLDTO), dNValue,
                        '                                  New AT_FMLDTO With {.ID = itm.ID_SIGN,
                        '                                      .CODE = itm.AT_FMLDTO.CODE})
                    End If
                End If

                itemAppointment = New AT_TIMESHEET_REGISTERDTO
                itemAppointment.EMPLOYEEID = itm.ID_EMPLOYEE
                itemAppointment.NOTE = itm.NOTE
                itemAppointment.NVALUE = itm.NVALUE
                itemAppointment.SIGNID = itm.ID_SIGN
                'itemAppointment.SIGNNAME = itm.AT_SIGN.CODE
                itemAppointment.WORKINGDAY = itm.FROM_DATE
                itemAppointment.NVALUE_ID = itm.NVALUE_ID
                itemAppointment.DEXT1 = itm.FROM_HOUR
                itemAppointment.DEXT2 = itm.TO_HOUR
                itemAppointment.IS_INSERT = True

                listAppointment.Add(itemAppointment)
            Next

            Dim lstDup = CreateRegisterAppointment(listAppointment, log)

            If lstDup IsNot Nothing AndAlso lstDup.Count > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function CreateRegisterAppointment(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO),
                                              ByVal log As UserLog,
                                             Optional ByVal isPlan As Boolean = False,
                                             Optional ByVal lstWorking As List(Of HU_WORKING) = Nothing) As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim insObject As AT_RGT

        Dim working As HU_WORKING
        Dim employee As HU_EMPLOYEE
        'Dim signex As AT_SIGNEXT
        Dim listId As New List(Of Decimal)

        Try
            _isAvailable = False
            Dim lstEmp = (From p In listappointment Where p.IS_INSERT = True Select p.EMPLOYEEID).ToList
            Dim startdate = listappointment.Where(Function(f) f.IS_INSERT = True).Min(Function(p) p.WORKINGDAY)
            Dim enddate = listappointment.Where(Function(f) f.IS_INSERT = True).Max(Function(p) p.WORKINGDAY)

            If listappointment.Where(Function(f) f.IS_INSERT = True).Count > 0 Then
                If Not isPlan Then
                    lstWorking = (From p In Context.HU_WORKING
                                  Where lstEmp.Contains(p.EMPLOYEE_ID) _
                                  And ((startdate >= p.EFFECT_DATE _
                                        And (Not p.EXPIRE_DATE.HasValue OrElse startdate <= p.EXPIRE_DATE)) _
                                   Or (enddate >= p.EFFECT_DATE And (Not p.EXPIRE_DATE.HasValue OrElse enddate <= p.EXPIRE_DATE))) _
                                  Select p).ToList
                End If

            End If

            For Each item As AT_TIMESHEET_REGISTERDTO In listappointment
                If item.IS_INSERT = True Then
                    insObject = New AT_RGT
                    insObject.ID = Utilities.GetNextSequence(Context, Context.AT_RGT.EntitySet.Name)
                    insObject.PLNID = item.PLNID
                    insObject.HU_EMPLOYEEID = item.EMPLOYEEID
                    insObject.AT_SIGNID = item.SIGNID
                    insObject.WORKINGDAY = item.WORKINGDAY
                    insObject.NVALUE = item.NVALUE
                    insObject.SVALUE = item.SVALUE
                    If item.DVALUE IsNot Nothing Then
                        insObject.DVALUE = item.DVALUE
                    Else : insObject.DVALUE = Nothing
                    End If
                    insObject.NVALUE_ID = item.NVALUE_ID
                    working = (From p In lstWorking
                               Where p.EMPLOYEE_ID = insObject.HU_EMPLOYEEID And _
                               p.EFFECT_DATE <= insObject.WORKINGDAY
                               Order By p.EFFECT_DATE Descending
                               Select p).FirstOrDefault()

                    If working IsNot Nothing Then
                        insObject.ORGID = working.ORG_ID
                        insObject.POSID = working.TITLE_ID
                    Else
                        employee = (From p In Context.HU_EMPLOYEE Where p.ID = insObject.HU_EMPLOYEEID Select p).FirstOrDefault()
                        insObject.ORGID = employee.ORG_ID
                        insObject.POSID = employee.TITLE_ID
                    End If

                    insObject.ISAP = IIf(item.ISAP, ATConstant.ACTFLG_ACTIVE, ATConstant.ACTFLG_DEACTIVE)

                    Context.AT_RGT.AddObject(insObject)
                    listId.Add(insObject.ID)
                End If
            Next
            If log Is Nothing Then
                Context.SaveChanges()
            Else
                Context.SaveChanges(log)
            End If
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".CreateRegisterAppointment")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        Dim fromDate As Date = Date.MinValue
        Dim toDate As Date = Date.MaxValue
        Dim signId As Decimal? = Nothing
        Dim empIdName As String = ""
        Dim listChangeReturn As New List(Of AT_PORTAL_REG_DTO)
        Dim curObjReg As AT_PORTAL_REG_DTO
        Dim objExistRegGroup As AT_PORTAL_REG_DTO
        Try
            _isAvailable = False

            If filter IsNot Nothing Then
                If filter.FromDate.HasValue Then
                    fromDate = filter.FromDate.Value
                End If

                If filter.ToDate.HasValue Then
                    toDate = filter.ToDate.Value
                End If

                If filter.SignId.HasValue Then
                    signId = filter.SignId.Value
                End If

                If filter.EmployeeIdName <> "" Then
                    empIdName = filter.EmployeeIdName.ToUpper
                End If
            End If

            'Lấy danh sách các bản ghi đang chờ người này phê duyệt
            'And f.CODE = process _
            'And f.IS_LEAVE = If(process = "LEAVE", -1, 2) _
            Dim list As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                      From p In Context.AT_PORTAL_REG.Where(Function(F) F.ID_REGGROUP = r.ID_REGGROUP).DefaultIfEmpty
                                                      From e In Context.HU_EMPLOYEE.Where(Function(C) C.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                                      From f In Context.AT_TIME_MANUAL.Where(Function(T) T.ID = p.ID_SIGN).DefaultIfEmpty
                                                      Where (r.ID_EMPLOYEE = approveId _
                                                             And (Not signId.HasValue Or p.ID_SIGN = signId.Value) _
                                                             And (empIdName = "" OrElse (e.EMPLOYEE_CODE.ToUpper.Contains(empIdName) OrElse e.FULLNAME_VN.ToUpper.Contains(empIdName))) _
                                                             And (p.TO_DATE.Value <= toDate And p.FROM_DATE.Value >= fromDate)) _
                                                             And p.SVALUE = process _
                                                      Order By r.APPROVE_DATE Descending, p.FROM_DATE, r.ID_REGGROUP, f.CODE _
                                                      Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                         .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                         .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                         .ID_SIGN = p.ID_SIGN,
                                                                                         .GSIGN_CODE = f.CODE,
                                                                                         .REGDATE = p.REGDATE,
                                                                                         .FROM_DATE = p.FROM_DATE,
                                                                                         .TO_DATE = p.TO_DATE,
                                                                                         .FROM_HOUR = p.FROM_HOUR,
                                                                                         .TO_HOUR = p.TO_HOUR,
                                                                                         .DAYCOUNT = p.DAYCOUNT,
                                                                                         .HOURCOUNT = p.HOURCOUNT,
                                                                                         .NOTE = p.NOTE,
                                                                                         .STATUS = r.APPROVE_STATUS,
                                                                                         .APP_DATE = r.APPROVE_DATE,
                                                                                         .APP_LEVEL = r.level,
                                                                                         .SIGN_CODE = f.CODE,
                                                                                         .SIGN_NAME = f.CODE & " - " & f.NAME,
                                                                                         .NVALUE = p.NVALUE,
                                                                                         .ID_REGGROUP = r.ID_REGGROUP}).ToList
            Dim lst = list.Select(Function(p) New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.EMPLOYEE_NAME,
                                                                                .ID_SIGN = p.ID_SIGN,
                                                                                .GSIGN_CODE = p.GSIGN_CODE,
                                                                                .REGDATE = p.REGDATE,
                                                                                .FROM_DATE = p.FROM_DATE,
                                                                                .TO_DATE = p.TO_DATE,
                                                                                .FROM_HOUR = p.FROM_HOUR,
                                                                                .TO_HOUR = p.TO_HOUR,
                                                                                .DAYCOUNT = p.DAYCOUNT,
                                                                                .HOURCOUNT = p.HOURCOUNT,
                                                                                .NOTE = p.NOTE,
                                                                                .STATUS = p.STATUS,
                                                                                .APP_DATE = p.APP_DATE,
                                                                                .APP_LEVEL = p.APP_LEVEL,
                                                                                .SIGN_CODE = p.SIGN_CODE,
                                                                                .SIGN_NAME = p.SIGN_NAME,
                                                                                .NVALUE = p.NVALUE,
                                                                                .ID_REGGROUP = p.ID_REGGROUP})

            Dim sql = (From T In lst
                       Group T By
                         T.ID,
                         T.FROM_DATE,
                         T.TO_DATE
                        Into g = Group
                       Select
                         ID = CType(g.Max(Function(p) p.ID), Decimal?),
                         EMPLOYEE_ID = CType(g.Max(Function(p) p.ID_EMPLOYEE), Decimal?),
                         EMPLOYEE_CODE = g.Max(Function(p) p.EMPLOYEE_CODE),
                         VN_FULLNAME = g.Max(Function(p) p.EMPLOYEE_NAME),
                         ID_SIGN = CType(g.Max(Function(p) p.ID_SIGN), Decimal?),
                         GSIGN_CODE = g.Max(Function(p) p.GSIGN_CODE),
                         REGDATE = CType(g.Max(Function(p) p.REGDATE), DateTime?),
                         FROM_DATE = CType(g.Max(Function(p) p.FROM_DATE), DateTime?),
                         TO_DATE = CType(g.Max(Function(p) p.TO_DATE), DateTime?),
                         FROM_HOUR = CType(g.Max(Function(p) p.FROM_HOUR), DateTime?),
                         TO_HOUR = CType(g.Max(Function(p) p.TO_HOUR), DateTime?),
                         DAYCOUNT = CType(g.Max(Function(p) p.DAYCOUNT), Decimal?),
                         HOURCOUNT = CType(g.Max(Function(p) p.HOURCOUNT), Decimal?),
                         NOTE = g.Max(Function(p) p.NOTE),
                         STATUS = g.Max(Function(p) p.STATUS),
                         APP_DATE = CType(g.Max(Function(p) p.APP_DATE), DateTime?),
                         APP_LEVEL = g.Max(Function(p) p.APP_LEVEL),
                         SIGN_CODE = g.Max(Function(p) p.SIGN_CODE),
                         SIGN_NAME = g.Max(Function(p) p.SIGN_NAME),
                         NVALUE = g.Max(Function(p) p.NVALUE),
                         ID_REGGROUP = g.Max(Function(p) p.ID_REGGROUP)
                              ).ToList

            Dim listReturn As List(Of AT_PORTAL_REG_DTO) = sql.Select(Function(f) New AT_PORTAL_REG_DTO With {.ID = f.ID,
                                                                                .ID_EMPLOYEE = f.EMPLOYEE_ID,
                                                                                .EMPLOYEE_CODE = f.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = f.VN_FULLNAME,
                                                                                .ID_SIGN = f.ID_SIGN,
                                                                                .GSIGN_CODE = f.GSIGN_CODE,
                                                                                .REGDATE = f.REGDATE,
                                                                                .FROM_DATE = f.FROM_DATE,
                                                                                .TO_DATE = f.TO_DATE,
                                                                                .FROM_HOUR = f.FROM_HOUR,
                                                                                .TO_HOUR = f.TO_HOUR,
                                                                                .DAYCOUNT = f.DAYCOUNT,
                                                                                .HOURCOUNT = f.HOURCOUNT,
                                                                                .NOTE = f.NOTE,
                                                                                .STATUS = f.STATUS,
                                                                                .APP_DATE = f.APP_DATE,
                                                                                .APP_LEVEL = f.APP_LEVEL,
                                                                                .SIGN_CODE = f.SIGN_CODE,
                                                                                .SIGN_NAME = f.SIGN_NAME,
                                                                                .NVALUE = f.NVALUE,
                                                                                .ID_REGGROUP = f.ID_REGGROUP
                                 }).ToList

            ' Lấy danh sách những người mà người này được phê duyệt thay thế
            Dim listAppExt = (From p In Context.SE_APP_SETUPEXT
                              From c In Context.SE_APP_PROCESS.Where(Function(f) f.ID = p.PROCESS_ID).DefaultIfEmpty
                              Where p.SUB_EMPLOYEE_ID = approveId _
                              And p.FROM_DATE <= Date.Today _
                              And Date.Today <= p.TO_DATE _
                              And c.PROCESS_CODE = process
                              Select p).ToList

            For Each setupEx In listAppExt

                Dim empId As Decimal = setupEx.EMPLOYEE_ID

                Dim listAppEx As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                               Join p In Context.AT_PORTAL_REG On r.ID_REGGROUP Equals p.ID_REGGROUP
                                                               Join e In Context.HU_EMPLOYEE On p.ID_EMPLOYEE Equals e.ID
                                                               Where (r.ID_EMPLOYEE = empId _
                                                                      And (p.REGDATE.Value <= toDate And p.REGDATE.Value >= fromDate))
                                                               Order By r.APPROVE_DATE Descending, p.FROM_DATE, e.EMPLOYEE_CODE
                                                               Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                                  .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                                  .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                                  .ID_SIGN = p.ID_SIGN,
                                                                                                  .GSIGN_CODE = "2",
                                                                                                  .REGDATE = p.REGDATE,
                                                                                                  .FROM_DATE = p.FROM_DATE,
                                                                                                  .TO_DATE = p.TO_DATE,
                                                                                                  .FROM_HOUR = p.FROM_HOUR,
                                                                                                  .TO_HOUR = p.TO_HOUR,
                                                                                                  .DAYCOUNT = p.DAYCOUNT,
                                                                                                  .HOURCOUNT = p.HOURCOUNT,
                                                                                                  .NOTE = p.NOTE,
                                                                                                  .STATUS = r.APPROVE_STATUS,
                                                                                                  .APP_DATE = r.APPROVE_DATE,
                                                                                                  .APP_LEVEL = r.level,
                                                                                                  .SIGN_CODE = "2",
                                                                                                  .SIGN_NAME = "ABC",
                                                                                                  .NVALUE = p.NVALUE,
                                                                                                  .ID_REGGROUP = p.ID_REGGROUP}).ToList

                If listAppEx.Count > 0 Then
                    listReturn.AddRange(listAppEx)
                End If
            Next

            For Each curObjReg In listReturn
                If curObjReg.ID_REGGROUP IsNot Nothing Then
                    objExistRegGroup = (From p In listChangeReturn
                                        Where p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                        And p.STATUS = curObjReg.STATUS
                                        Select p).SingleOrDefault()

                    If objExistRegGroup IsNot Nothing Then
                        If Not (objExistRegGroup.SIGN_NAME + "<br />").Contains(curObjReg.SIGN_NAME + "<br />") Then
                            If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_LEAVE Then
                                objExistRegGroup.SIGN_NAME += "<br />" & curObjReg.SIGN_NAME
                                objExistRegGroup.DISPLAY += "<br />" & curObjReg.SIGN_CODE + " - " + _
                                    Format((From p In listReturn
                                            Where (p.ID_REGGROUP = objExistRegGroup.ID_REGGROUP _
                                                   And p.STATUS = objExistRegGroup.STATUS _
                                                   And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                            Select p.NVALUE.Value).Sum, "0.##")
                            End If

                            If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_WLEO Then
                                objExistRegGroup.SIGN_NAME += "<br />" & curObjReg.SIGN_NAME
                                objExistRegGroup.DISPLAY += "<br />" & curObjReg.SIGN_CODE + " - " + _
                                    Format((From p In listReturn
                                            Where p.ID_REGGROUP = objExistRegGroup.ID_REGGROUP _
                                            And p.STATUS = objExistRegGroup.STATUS _
                                            And p.SIGN_NAME = curObjReg.SIGN_NAME
                                            Select p.NVALUE.Value).Sum, "0.##")
                            End If
                        End If
                        If curObjReg.GSIGN_CODE = ATConstant.GSIGNCODE_OVERTIME Then
                            objExistRegGroup.DISPLAY += "<br />" + _
                                curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                                curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")
                        End If
                        objExistRegGroup.DAYCOUNT += Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        objExistRegGroup.HOURCOUNT += Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        If objExistRegGroup.FROM_DATE > curObjReg.FROM_DATE Then
                            objExistRegGroup.FROM_DATE = curObjReg.FROM_DATE
                        Else : objExistRegGroup.TO_DATE = curObjReg.FROM_DATE
                        End If
                    Else
                        curObjReg.DAYCOUNT = Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        curObjReg.HOURCOUNT = Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        Select Case curObjReg.GSIGN_CODE
                            Case ATConstant.GSIGNCODE_OVERTIME
                                curObjReg.DISPLAY = curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                                curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")
                            Case ATConstant.GSIGNCODE_LEAVE
                                curObjReg.DISPLAY = curObjReg.SIGN_CODE + " - " + Format((From p In listReturn
                                                                                          Where (p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                                                                                 And p.STATUS = curObjReg.STATUS _
                                                                                                 And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                                                                          Select p.NVALUE.Value).Sum, "0.##")
                            Case ATConstant.GSIGNCODE_WLEO
                                curObjReg.DISPLAY = curObjReg.SIGN_CODE + " - " + Format((From p In listReturn
                                                                                          Where (p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                                                                                 And p.STATUS = curObjReg.STATUS _
                                                                                                 And p.SIGN_NAME = curObjReg.SIGN_NAME)
                                                                                          Select p.NVALUE.Value).Sum, "0.##")
                        End Select

                        listChangeReturn.Add(curObjReg)
                    End If
                End If
            Next


            Return (From p In listChangeReturn Order By p.APP_DATE Descending, p.FROM_DATE, p.EMPLOYEE_CODE).ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetListWaitingForApproveOT(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        Dim fromDate As Date = Date.MinValue
        Dim toDate As Date = Date.MaxValue
        Dim signId As Decimal? = Nothing
        Dim empIdName As String = ""
        Dim listChangeReturn As New List(Of AT_PORTAL_REG_DTO)
        Dim curObjReg As AT_PORTAL_REG_DTO
        Dim objExistRegGroup As AT_PORTAL_REG_DTO
        Try
            _isAvailable = False

            If filter IsNot Nothing Then
                If filter.FromDate.HasValue Then
                    fromDate = filter.FromDate.Value
                End If

                If filter.ToDate.HasValue Then
                    toDate = filter.ToDate.Value
                End If

                If filter.SignId.HasValue Then
                    signId = filter.SignId.Value
                End If

                If filter.EmployeeIdName <> "" Then
                    empIdName = filter.EmployeeIdName.ToUpper
                End If
            End If

            'Lấy danh sách các bản ghi đang chờ người này phê duyệt
            'And f.CODE = process _
            'And f.IS_LEAVE = If(process = "LEAVE", -1, 2) _
            Dim list As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                      From p In Context.AT_PORTAL_REG.Where(Function(F) F.ID_REGGROUP = r.ID_REGGROUP).DefaultIfEmpty
                                                      From e In Context.HU_EMPLOYEE.Where(Function(C) C.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                                                      From f In Context.OT_OTHER_LIST.Where(Function(T) T.ID = p.ID_SIGN).DefaultIfEmpty
                                                      Where (r.ID_EMPLOYEE = approveId _
                                                             And (Not signId.HasValue Or p.ID_SIGN = signId.Value) _
                                                             And (empIdName = "" OrElse (e.EMPLOYEE_CODE.ToUpper.Contains(empIdName) OrElse e.FULLNAME_VN.ToUpper.Contains(empIdName))) _
                                                             And (p.TO_DATE.Value <= toDate And p.FROM_DATE.Value >= fromDate)) _
                                                             And p.SVALUE = process _
                                                      Order By r.APPROVE_DATE Descending, p.FROM_DATE, r.ID_REGGROUP, f.CODE _
                                                      Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                         .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                         .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                         .ID_SIGN = p.ID_SIGN,
                                                                                         .GSIGN_CODE = f.CODE,
                                                                                         .REGDATE = p.REGDATE,
                                                                                         .FROM_DATE = p.FROM_DATE,
                                                                                         .TO_DATE = p.TO_DATE,
                                                                                         .FROM_HOUR = p.FROM_HOUR,
                                                                                         .TO_HOUR = p.TO_HOUR,
                                                                                         .DAYCOUNT = p.DAYCOUNT,
                                                                                         .HOURCOUNT = p.HOURCOUNT,
                                                                                         .NOTE = p.NOTE,
                                                                                         .STATUS = r.APPROVE_STATUS,
                                                                                         .APP_DATE = r.APPROVE_DATE,
                                                                                         .APP_LEVEL = r.level,
                                                                                         .SIGN_CODE = f.CODE,
                                                                                         .SIGN_NAME = f.NAME_VN,
                                                                                         .NVALUE = p.NVALUE,
                                                                                         .NGHI_BU = If(p.IS_NB = -1, "Có", "Không"),
                                                                                         .ID_REGGROUP = r.ID_REGGROUP}).ToList
            Dim lst = list.Select(Function(p) New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = p.EMPLOYEE_NAME,
                                                                                .ID_SIGN = p.ID_SIGN,
                                                                                .GSIGN_CODE = p.GSIGN_CODE,
                                                                                .REGDATE = p.REGDATE,
                                                                                .FROM_DATE = p.FROM_DATE,
                                                                                .TO_DATE = p.TO_DATE,
                                                                                .FROM_HOUR = p.FROM_HOUR,
                                                                                .TO_HOUR = p.TO_HOUR,
                                                                                .DAYCOUNT = p.DAYCOUNT,
                                                                                .HOURCOUNT = p.HOURCOUNT,
                                                                                .NOTE = p.NOTE,
                                                                                .STATUS = p.STATUS,
                                                                                .APP_DATE = p.APP_DATE,
                                                                                .APP_LEVEL = p.APP_LEVEL,
                                                                                .SIGN_CODE = p.SIGN_CODE,
                                                                                .SIGN_NAME = p.SIGN_NAME,
                                                                                .NVALUE = p.NVALUE,
                                                                                .NGHI_BU = p.NGHI_BU,
                                                                                .ID_REGGROUP = p.ID_REGGROUP})

            Dim sql = (From T In lst
                       Group T By
                         T.ID,
                         T.FROM_DATE,
                         T.TO_DATE
                        Into g = Group
                       Select
                         ID = CType(g.Max(Function(p) p.ID), Decimal?),
                         EMPLOYEE_ID = CType(g.Max(Function(p) p.ID_EMPLOYEE), Decimal?),
                         EMPLOYEE_CODE = g.Max(Function(p) p.EMPLOYEE_CODE),
                         VN_FULLNAME = g.Max(Function(p) p.EMPLOYEE_NAME),
                         ID_SIGN = CType(g.Max(Function(p) p.ID_SIGN), Decimal?),
                         GSIGN_CODE = g.Max(Function(p) p.GSIGN_CODE),
                         REGDATE = CType(g.Max(Function(p) p.REGDATE), DateTime?),
                         FROM_DATE = CType(g.Max(Function(p) p.FROM_DATE), DateTime?),
                         TO_DATE = CType(g.Max(Function(p) p.TO_DATE), DateTime?),
                         FROM_HOUR = CType(g.Max(Function(p) p.FROM_HOUR), DateTime?),
                         TO_HOUR = CType(g.Max(Function(p) p.TO_HOUR), DateTime?),
                         DAYCOUNT = CType(g.Max(Function(p) p.DAYCOUNT), Decimal?),
                         HOURCOUNT = CType(g.Max(Function(p) p.HOURCOUNT), Decimal?),
                         NOTE = g.Max(Function(p) p.NOTE),
                         STATUS = g.Max(Function(p) p.STATUS),
                         APP_DATE = CType(g.Max(Function(p) p.APP_DATE), DateTime?),
                         APP_LEVEL = g.Max(Function(p) p.APP_LEVEL),
                         SIGN_CODE = g.Max(Function(p) p.SIGN_CODE),
                         SIGN_NAME = g.Max(Function(p) p.SIGN_NAME),
                         NVALUE = g.Max(Function(p) p.NVALUE),
                         NGHI_BU = g.Max(Function(p) p.NGHI_BU),
                         ID_REGGROUP = g.Max(Function(p) p.ID_REGGROUP)
                              ).ToList

            Dim listReturn As List(Of AT_PORTAL_REG_DTO) = sql.Select(Function(f) New AT_PORTAL_REG_DTO With {.ID = f.ID,
                                                                                .ID_EMPLOYEE = f.EMPLOYEE_ID,
                                                                                .EMPLOYEE_CODE = f.EMPLOYEE_CODE,
                                                                                .EMPLOYEE_NAME = f.VN_FULLNAME,
                                                                                .ID_SIGN = f.ID_SIGN,
                                                                                .GSIGN_CODE = f.GSIGN_CODE,
                                                                                .REGDATE = f.REGDATE,
                                                                                .FROM_DATE = f.FROM_DATE,
                                                                                .TO_DATE = f.TO_DATE,
                                                                                .FROM_HOUR = f.FROM_HOUR,
                                                                                .TO_HOUR = f.TO_HOUR,
                                                                                .DAYCOUNT = f.DAYCOUNT,
                                                                                .HOURCOUNT = f.HOURCOUNT,
                                                                                .NOTE = f.NOTE,
                                                                                .STATUS = f.STATUS,
                                                                                .APP_DATE = f.APP_DATE,
                                                                                .APP_LEVEL = f.APP_LEVEL,
                                                                                .SIGN_CODE = f.SIGN_CODE,
                                                                                .SIGN_NAME = f.SIGN_NAME,
                                                                                .NVALUE = f.NVALUE,
                                                                                 .NGHI_BU = f.NGHI_BU,
                                                                                .ID_REGGROUP = f.ID_REGGROUP
                                 }).ToList

            ' Lấy danh sách những người mà người này được phê duyệt thay thế
            Dim listAppExt = (From p In Context.SE_APP_SETUPEXT
                              From c In Context.SE_APP_PROCESS.Where(Function(f) f.ID = p.PROCESS_ID).DefaultIfEmpty
                              Where p.SUB_EMPLOYEE_ID = approveId _
                              And p.FROM_DATE <= Date.Today _
                              And Date.Today <= p.TO_DATE _
                              And c.PROCESS_CODE = process
                              Select p).ToList

            For Each setupEx In listAppExt

                Dim empId As Decimal = setupEx.EMPLOYEE_ID

                Dim listAppEx As List(Of AT_PORTAL_REG_DTO) = (From r In Context.AT_PORTAL_APP
                                                               Join p In Context.AT_PORTAL_REG On r.ID_REGGROUP Equals p.ID_REGGROUP
                                                               Join e In Context.HU_EMPLOYEE On p.ID_EMPLOYEE Equals e.ID
                                                               Where (r.ID_EMPLOYEE = empId _
                                                                      And (p.REGDATE.Value <= toDate And p.REGDATE.Value >= fromDate))
                                                               Order By r.APPROVE_DATE Descending, p.FROM_DATE, e.EMPLOYEE_CODE
                                                               Select New AT_PORTAL_REG_DTO With {.ID = p.ID,
                                                                                                  .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                                                                  .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                                                  .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                                                  .ID_SIGN = p.ID_SIGN,
                                                                                                  .GSIGN_CODE = "2",
                                                                                                  .REGDATE = p.REGDATE,
                                                                                                  .FROM_DATE = p.FROM_DATE,
                                                                                                  .TO_DATE = p.TO_DATE,
                                                                                                  .FROM_HOUR = p.FROM_HOUR,
                                                                                                  .TO_HOUR = p.TO_HOUR,
                                                                                                  .DAYCOUNT = p.DAYCOUNT,
                                                                                                  .HOURCOUNT = p.HOURCOUNT,
                                                                                                  .NOTE = p.NOTE,
                                                                                                  .STATUS = r.APPROVE_STATUS,
                                                                                                  .APP_DATE = r.APPROVE_DATE,
                                                                                                  .APP_LEVEL = r.level,
                                                                                                  .SIGN_CODE = "2",
                                                                                                  .SIGN_NAME = "ABC",
                                                                                                  .NVALUE = p.NVALUE,
                                                                                                  .NGHI_BU = If(p.IS_NB = -1, "Có", "Không"),
                                                                                                  .ID_REGGROUP = p.ID_REGGROUP}).ToList

                If listAppEx.Count > 0 Then
                    listReturn.AddRange(listAppEx)
                End If
            Next

            For Each curObjReg In listReturn
                If curObjReg.ID_REGGROUP IsNot Nothing Then
                    objExistRegGroup = (From p In listChangeReturn
                                        Where p.ID_REGGROUP = curObjReg.ID_REGGROUP _
                                        And p.STATUS = curObjReg.STATUS
                                        Select p).SingleOrDefault()

                    If objExistRegGroup IsNot Nothing Then
                        objExistRegGroup.DISPLAY += "<br />" + _
                        curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                        curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")

                        objExistRegGroup.DAYCOUNT += Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        objExistRegGroup.HOURCOUNT += Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        If objExistRegGroup.FROM_DATE > curObjReg.FROM_DATE Then
                            objExistRegGroup.FROM_DATE = curObjReg.FROM_DATE
                        Else : objExistRegGroup.TO_DATE = curObjReg.FROM_DATE
                        End If
                    Else
                        curObjReg.DAYCOUNT = Format(If(curObjReg.NVALUE Is Nothing, 0, curObjReg.NVALUE), "0.##")
                        curObjReg.HOURCOUNT = Format(If(curObjReg.HOURCOUNT Is Nothing, 0, curObjReg.HOURCOUNT), "0.##")
                        curObjReg.DISPLAY = curObjReg.FROM_DATE.Value.ToString("dd/MM/yyyy") + ": " + _
                        curObjReg.FROM_HOUR.Value.ToString("HH:mm") + "-" + curObjReg.TO_HOUR.Value.ToString("HH:mm")

                        listChangeReturn.Add(curObjReg)
                    End If
                End If
            Next


            Return (From p In listChangeReturn Order By p.APP_DATE Descending, p.FROM_DATE, p.EMPLOYEE_CODE).ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetEmployeeList() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_LEAVE.GET_EMPLOYEE_LIST",
                                                    New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetLeaveDay(ByVal dDate As Date) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_LEAVE.GET_LEAVE_DAY",
                                                    New With {.P_DATE = dDate,
                                                              .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Thiết lập kiểu công"

    Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        Try
            Dim check As String = "Dùng chung"

            Dim query = From p In Context.AT_TIME_MANUAL
                        From f1 In Context.AT_FML.Where(Function(F) F.ID = p.MORNING_ID).DefaultIfEmpty
                        From f2 In Context.AT_FML.Where(Function(F) F.ID = p.AFTERNOON_ID).DefaultIfEmpty
                        From f3 In Context.AT_TYPE_PROCESS.Where(Function(F) F.ID = p.TYPE_PROSS_ID).DefaultIfEmpty
                        From f4 In Context.AT_TIME_MANUAL_RATE.Where(Function(F) F.ID = p.MORNING_RATE_ID).DefaultIfEmpty
                        From f5 In Context.AT_TIME_MANUAL_RATE.Where(Function(F) F.ID = p.AFTERNOON_RATE_ID).DefaultIfEmpty
                        Group Join g In Context.HU_ORGANIZATION On p.ORG_ID Equals g.ID Into g_olg = Group
                        From olg In g_olg.DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_TIME_MANUALDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME,
                                       .MORNING_ID = p.p.MORNING_ID,
                                       .MORNING_NAME = p.f1.NAME_VN,
                                       .AFTERNOON_ID = p.p.AFTERNOON_ID,
                                       .AFTERNOON_NAME = p.f2.NAME_VN,
                                       .TYPE_PROSS_ID = p.p.TYPE_PROSS_ID,
                                       .TYPE_PROCESS_NAME = p.f3.NAME_VN,
                                       .MORNING_RATE_ID = p.p.MORNING_RATE_ID,
                                       .MORNING_RATE_VALUE = p.f4.VALUE_RATE,
                                       .AFTERNOON_RATE_ID = p.p.AFTERNOON_RATE_ID,
                                       .AFTERNOON_RATE_VALUE = p.f5.VALUE_RATE,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = If(p.p.ORG_ID = -1, check, p.olg.NAME_VN),
                                       .NOTE = p.p.NOTE,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .IS_OTHER = p.p.IS_OTHER,
                                       .MAPPING_CODE = p.p.MAPPING_CODE,
                                       .CODE_KH = p.p.CODE_KH,
                                       .ORDERS = p.p.ORDERS,
                                       .CODE1 = p.f1.CODE,
                                        .CODE2 = p.f2.CODE,
                                       .IS_REG_SHIFT = p.p.IS_REG_SHIFT})


            If Not String.IsNullOrEmpty(_filter.CODE_KH) Then
                lst = lst.Where(Function(f) f.CODE_KH.ToLower().Contains(_filter.CODE_KH.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MAPPING_CODE) Then
                lst = lst.Where(Function(f) f.MAPPING_CODE.ToLower().Contains(_filter.MAPPING_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MORNING_NAME) Then
                lst = lst.Where(Function(f) f.MORNING_NAME.ToLower().Contains(_filter.MORNING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.AFTERNOON_NAME) Then
                lst = lst.Where(Function(f) f.AFTERNOON_NAME.ToLower().Contains(_filter.AFTERNOON_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_PROCESS_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_PROCESS_NAME.ToLower().Contains(_filter.TYPE_PROCESS_NAME.ToLower()))
            End If
            If _filter.MORNING_RATE_VALUE <> 0 Then
                lst = lst.Where(Function(f) f.MORNING_RATE_VALUE = _filter.MORNING_RATE_VALUE)
            End If
            If _filter.AFTERNOON_RATE_VALUE <> 0 Then
                lst = lst.Where(Function(f) f.AFTERNOON_RATE_VALUE = _filter.AFTERNOON_RATE_VALUE)
            End If
            If _filter.ORDERS IsNot Nothing Then
                lst = lst.Where(Function(f) f.ORDERS = _filter.ORDERS)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
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

    Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        Try

            Dim query = From p In Context.AT_TIME_MANUAL
                        Where p.ID = _id

            Dim ls = query.Select(Function(p) New AT_TIME_MANUALDTO With {.ID = p.ID,
                                                                       .MORNING_ID = p.MORNING_ID,
                                                                       .AFTERNOON_ID = p.AFTERNOON_ID}).FirstOrDefault
            Return ls
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function InsertAT_TIME_MANUAL(ByVal objTitle As AT_TIME_MANUALDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_MANUAL
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_MANUAL.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.CODE_KH = objTitle.CODE_KH.Trim
            objTitleData.MAPPING_CODE = objTitle.MAPPING_CODE.Trim
            objTitleData.IS_REG_SHIFT = objTitle.IS_REG_SHIFT
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME = objTitle.NAME_VN.Trim
            objTitleData.MORNING_ID = objTitle.MORNING_ID
            objTitleData.AFTERNOON_ID = objTitle.AFTERNOON_ID
            objTitleData.TYPE_PROSS_ID = objTitle.TYPE_PROSS_ID
            objTitleData.MORNING_RATE_ID = objTitle.MORNING_RATE_ID
            objTitleData.AFTERNOON_RATE_ID = objTitle.AFTERNOON_RATE_ID
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.IS_OTHER = objTitle.IS_OTHER
            Context.AT_TIME_MANUAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_TIME_MANUAL(ByVal _validate As AT_TIME_MANUALDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.CODE_KH <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE_KH.ToUpper = _validate.CODE_KH.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.CODE_KH.ToUpper = _validate.CODE_KH.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            ElseIf _validate.MAPPING_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.MAPPING_CODE.ToUpper = _validate.MAPPING_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.MAPPING_CODE.ToUpper = _validate.MAPPING_CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_TIME_MANUAL
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_TIME_MANUAL(ByVal objTitle As AT_TIME_MANUALDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_MANUAL With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TIME_MANUAL Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.CODE_KH = objTitle.CODE_KH.Trim
            objTitleData.MAPPING_CODE = objTitle.MAPPING_CODE.Trim
            objTitleData.IS_REG_SHIFT = objTitle.IS_REG_SHIFT
            'objTitleData.NAME_EN = objTitle.NAME_EN.Trim
            objTitleData.NAME = objTitle.NAME_VN.Trim
            objTitleData.MORNING_ID = objTitle.MORNING_ID
            objTitleData.AFTERNOON_ID = objTitle.AFTERNOON_ID
            objTitleData.TYPE_PROSS_ID = objTitle.TYPE_PROSS_ID
            objTitleData.MORNING_RATE_ID = objTitle.MORNING_RATE_ID
            objTitleData.AFTERNOON_RATE_ID = objTitle.AFTERNOON_RATE_ID
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_OTHER = objTitle.IS_OTHER
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_TIME_MANUAL)
        Try
            lstData = (From p In Context.AT_TIME_MANUAL Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_TIME_MANUAL)
        Try
            lstHolidayData = (From p In Context.AT_TIME_MANUAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_TIME_MANUAL.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTime_Manual")
            Throw ex
        End Try
    End Function

    Public Function GetDataImportCO() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL_IMPORT",
                                           New With {.CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
    Public Function GetDataImportCO1() As DataSet
        Using cls As New DataAccess.QueryData
            Dim dtData As DataSet = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_TIME_MANUAL_IMPORT",
                                           New With {.CUR = cls.OUT_CURSOR, .CUR1 = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

#Region "Danh mục tham số hệ thống phần itime"
    Public Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        Try

            Dim lst = From p In Context.AT_LIST_PARAM_SYSTEM
                      From i In Context.HU_STAFF_RANK.Where(Function(F) F.ID = p.RANK_PAY_OT).DefaultIfEmpty
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                      Select New AT_LISTPARAM_SYSTEAMDTO With {
                                        .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .EFFECT_DATE_FROM = p.EFFECT_DATE_FROM,
                                       .EFFECT_DATE_TO_NB = p.EFFECT_DATE_TO_NB,
                                       .TO_LEAVE_YEAR = p.TO_LEAVE_YEAR,
                                       .RANK_PAY_OT = p.RANK_PAY_OT,
                                       .RANK_PAY_OT_NAME = i.NAME,
                                       .HOUR_CAL_OT = p.HOUR_CAL_OT,
                                       .HOUR_MAX_OT = p.HOUR_MAX_OT,
                                       .FACTOR_OT = p.FACTOR_OT,
                                       .CREATE_BY_SHOW = p.CREATE_BY_SHOW,
                                       .CREATE_DATE_SHOW = EntityFunctions.TruncateTime(p.CREATE_DATE_SHOW),
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.NOTE,
                                       .MAX_DAY_OT = p.MAX_DAY_OT,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .YEAR_P = p.YEAR_P,
                                       .YEAR_TN = p.YEAR_TN,
                                       .DAY_TN = p.DAY_TN,
                                       .ORG_ID = If(p.ORG_ID Is Nothing, -1, p.ORG_ID),
                                       .ORG_NAME = o.NAME_VN,
                                       .NO_EFFECT_ENT = If(p.NO_EFFECT_ENT Is Nothing, False, CBool(p.NO_EFFECT_ENT)),
                                       .NO_EFFECT_NB = If(p.NO_EFFECT_NB Is Nothing, False, CBool(p.NO_EFFECT_NB)),
                                       .ABSENT_DAY_LIMIT = p.ABSENT_DAY_LIMIT
                          }

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_FROM = _filter.EFFECT_DATE_FROM)
            End If
            If _filter.EFFECT_DATE_TO_NB.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE_TO_NB = _filter.EFFECT_DATE_TO_NB)
            End If
            If _filter.TO_LEAVE_YEAR.HasValue Then
                lst = lst.Where(Function(f) f.TO_LEAVE_YEAR = _filter.TO_LEAVE_YEAR)
            End If
            If _filter.RANK_PAY_OT.HasValue Then
                lst = lst.Where(Function(f) f.RANK_PAY_OT = _filter.RANK_PAY_OT)
            End If
            If _filter.HOUR_CAL_OT.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_CAL_OT = _filter.HOUR_CAL_OT)
            End If
            If _filter.HOUR_MAX_OT.HasValue Then
                lst = lst.Where(Function(f) f.HOUR_MAX_OT = _filter.HOUR_MAX_OT)
            End If
            If _filter.FACTOR_OT.HasValue Then
                lst = lst.Where(Function(f) f.FACTOR_OT = _filter.FACTOR_OT)
            End If
            If _filter.MAX_DAY_OT.HasValue Then
                lst = lst.Where(Function(f) f.MAX_DAY_OT = _filter.MAX_DAY_OT)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CREATE_BY_SHOW) Then
                lst = lst.Where(Function(f) f.CREATE_BY_SHOW.ToLower().Contains(_filter.CREATE_BY_SHOW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.RANK_PAY_OT_NAME) Then
                lst = lst.Where(Function(f) f.RANK_PAY_OT_NAME.ToLower().Contains(_filter.RANK_PAY_OT_NAME.ToLower()))
            End If

            If _filter.CREATE_DATE_SHOW.HasValue Then
                lst = lst.Where(Function(f) f.CREATE_DATE_SHOW = _filter.CREATE_DATE_SHOW)
            End If

            If _filter.YEAR_TN.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_TN = _filter.YEAR_TN)
            End If
            If _filter.YEAR_P.HasValue Then
                lst = lst.Where(Function(f) f.YEAR_P = _filter.YEAR_P)
            End If
            If _filter.DAY_TN.HasValue Then
                lst = lst.Where(Function(f) f.DAY_TN = _filter.DAY_TN)
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

    Public Function InsertListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_LIST_PARAM_SYSTEM
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_LIST_PARAM_SYSTEM.EntitySet.Name)
            objTitleData.CODE = objData.CODE.Trim
            objTitleData.NAME = objData.NAME.Trim
            objTitleData.EFFECT_DATE_FROM = objData.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO_NB = objData.EFFECT_DATE_TO_NB
            objTitleData.TO_LEAVE_YEAR = objData.TO_LEAVE_YEAR
            objTitleData.RANK_PAY_OT = objData.RANK_PAY_OT
            objTitleData.HOUR_CAL_OT = objData.HOUR_CAL_OT
            objTitleData.HOUR_MAX_OT = objData.HOUR_MAX_OT
            objTitleData.FACTOR_OT = objData.FACTOR_OT
            objTitleData.CREATE_BY_SHOW = objData.CREATE_BY_SHOW
            objTitleData.CREATE_DATE_SHOW = objData.CREATE_DATE_SHOW
            objTitleData.NOTE = objData.NOTE
            objTitleData.ACTFLG = objData.ACTFLG
            objTitleData.CREATED_BY = objData.CREATED_BY
            objTitleData.CREATED_DATE = objData.CREATED_DATE
            objTitleData.CREATED_LOG = objData.CREATED_LOG
            objTitleData.MODIFIED_BY = objData.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objData.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objData.MODIFIED_LOG
            objTitleData.YEAR_P = objData.YEAR_P
            objTitleData.YEAR_TN = objData.YEAR_TN
            objTitleData.DAY_TN = objData.DAY_TN
            objTitleData.ORG_ID = objData.ORG_ID
            objTitleData.NO_EFFECT_ENT = objData.NO_EFFECT_ENT
            objTitleData.NO_EFFECT_NB = objData.NO_EFFECT_NB
            objTitleData.ABSENT_DAY_LIMIT = objData.ABSENT_DAY_LIMIT
            Context.AT_LIST_PARAM_SYSTEM.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateListParamItime(ByVal _validate As AT_LISTPARAM_SYSTEAMDTO)
        Dim query
        Try
            If _validate.EFFECT_DATE_FROM IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.EFFECT_DATE_FROM = _validate.EFFECT_DATE_FROM _
                             And p.ID <> _validate.ID And p.ORG_ID = _validate.ORG_ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.EFFECT_DATE_FROM = _validate.EFFECT_DATE_FROM And p.ORG_ID = _validate.ORG_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.AT_LIST_PARAM_SYSTEM
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_LIST_PARAM_SYSTEM With {.ID = objData.ID}
        Try
            objTitleData = (From p In Context.AT_LIST_PARAM_SYSTEM Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.CODE = objData.CODE.Trim
            objTitleData.NAME = objData.NAME.Trim
            objTitleData.EFFECT_DATE_FROM = objData.EFFECT_DATE_FROM
            objTitleData.EFFECT_DATE_TO_NB = objData.EFFECT_DATE_TO_NB
            objTitleData.TO_LEAVE_YEAR = objData.TO_LEAVE_YEAR
            objTitleData.RANK_PAY_OT = objData.RANK_PAY_OT
            objTitleData.HOUR_CAL_OT = objData.HOUR_CAL_OT
            objTitleData.HOUR_MAX_OT = objData.HOUR_MAX_OT
            objTitleData.FACTOR_OT = objData.FACTOR_OT
            objTitleData.CREATE_BY_SHOW = objData.CREATE_BY_SHOW
            objTitleData.CREATE_DATE_SHOW = objData.CREATE_DATE_SHOW
            objTitleData.NOTE = objData.NOTE
            objTitleData.ACTFLG = If(objData.ACTFLG Is Nothing, (From p In Context.AT_LIST_PARAM_SYSTEM Where p.ID = objTitleData.ID Select p.ACTFLG).SingleOrDefault, objData.ACTFLG)
            objTitleData.CREATED_BY = objData.CREATED_BY
            objTitleData.CREATED_DATE = objData.CREATED_DATE
            objTitleData.CREATED_LOG = objData.CREATED_LOG
            objTitleData.MODIFIED_BY = objData.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objData.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objData.MODIFIED_LOG
            objTitleData.YEAR_P = objData.YEAR_P
            objTitleData.YEAR_TN = objData.YEAR_TN
            objTitleData.DAY_TN = objData.DAY_TN
            objTitleData.ORG_ID = objData.ORG_ID
            objTitleData.NO_EFFECT_ENT = objData.NO_EFFECT_ENT
            objTitleData.NO_EFFECT_NB = objData.NO_EFFECT_NB
            objTitleData.ABSENT_DAY_LIMIT = objData.ABSENT_DAY_LIMIT
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_LIST_PARAM_SYSTEM)
        Try
            lstData = (From p In Context.AT_LIST_PARAM_SYSTEM Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_LIST_PARAM_SYSTEM)
        Try
            lstHolidayData = (From p In Context.AT_LIST_PARAM_SYSTEM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_LIST_PARAM_SYSTEM.DeleteObject(lstHolidayData(index))
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

#Region ""
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName & " WHERE " & colName & " LIKE '" & firstChar & "%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "001"
            End If
            Dim number = Decimal.Parse(str.Substring(str.IndexOf(firstChar) + firstChar.Length))
            number = number + 1
            Dim lastChar = Format(number, "000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Select Case table
                Case AttendanceCommon.TABLE_NAME.AT_FML
                    isExist = Execute_ExistInDatabase("AT_TIME_MANUAL", "MORNING_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("AT_TIME_MANUAL", "AFTERNOON_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case AttendanceCommon.TABLE_NAME.AT_SHIFT
                    isExist = Execute_ExistInDatabase("AT_WORKSIGN", "SHIFT_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case AttendanceCommon.TABLE_NAME.AT_TIME_MANUAL
                    isExist = Execute_ExistInDatabase("AT_TIME_TIMESHEET_MACHINET", "MANUAL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If

                Case AttendanceCommon.TABLE_NAME.AT_HOLIDAY
                Case AttendanceCommon.TABLE_NAME.AT_LIST_PARAM_SYSTEM
                Case AttendanceCommon.TABLE_NAME.AT_SETUP_SPECIAL
                Case AttendanceCommon.TABLE_NAME.AT_SETUP_TIME_EMP
                Case AttendanceCommon.TABLE_NAME.AT_GSIGN
                Case AttendanceCommon.TABLE_NAME.AT_DMVS
                Case AttendanceCommon.TABLE_NAME.AT_PROJECT_EMP
                    isExist = Execute_ExistInDatabase("AT_PROJECT_EMP", "PROJECT_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Public Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim strListWorking As Date = lstWorking.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim strListShift As String = lstShift.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Select Case table
                Case AttendanceCommon.TABLE_NAME.AT_SIGNDEFAULT
                    Dim number As New Decimal
                    Dim Sql = "SELECT COUNT(*) FROM AT_WORKSIGN WHERE EMPLOYEE_ID IN (" & strListID & ") AND SHIFT_ID IN (" & strListShift & ")"
                    number = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault

                    If number > 0 Then
                        Return False
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

    Private Function Execute_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

#Region "Project Title"
    ''' <lastupdate>
    ''' 21/08/2017 08:44
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách chức danh trong dự án
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAT_PROJECT_TITLE(ByVal _filter As AT_PROJECT_TITLEDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_TITLEDTO)
        Try

            Dim query = From p In Context.AT_PROJECT_TITLE

            Dim lst = query.Select(Function(p) New AT_PROJECT_TITLEDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
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
    ''' <lastupdate>
    ''' 21/08/2017 08:44
    ''' </lastupdate>
    ''' <summary>
    ''' Thêm chức danh trong dự án
    ''' </summary>
    ''' <param name="objTitle"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertAT_PROJECT_TITLE(ByVal objTitle As AT_PROJECT_TITLEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_TITLE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT_TITLE.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.REMARK = objTitle.REMARK
            Context.AT_PROJECT_TITLE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function
    ''' <lastupdate>
    ''' 21/08/2017 08:44
    ''' </lastupdate>
    ''' <summary>
    ''' Validate tồn tại chức danh trong dự án
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAT_PROJECT_TITLE(ByVal _validate As AT_PROJECT_TITLEDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_PROJECT_TITLE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT_TITLE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 21/08/2017 08:44
    ''' </lastupdate>
    ''' <summary>
    ''' Sửa chức danh trong dự án
    ''' </summary>
    ''' <param name="objTitle"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyAT_PROJECT_TITLE(ByVal objTitle As AT_PROJECT_TITLEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_TITLE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_PROJECT_TITLE Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 21/08/2017 08:44
    ''' </lastupdate>
    ''' <summary>
    ''' Xoa chức danh trong dự án
    ''' </summary>
    ''' <param name="lstID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAT_PROJECT_TITLE(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_PROJECT_TITLE)
        Try
            lstHolidayData = (From p In Context.AT_PROJECT_TITLE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_PROJECT_TITLE.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Project"

    Public Function GetAT_PROJECT(ByVal _filter As AT_PROJECTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECTDTO)
        Try

            Dim query = From p In Context.AT_PROJECT
                        From m In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MANAGER_ID).DefaultIfEmpty
                        From s In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        Select New AT_PROJECTDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .EXPIRE_DATE = p.EXPIRE_DATE,
                            .MANAGER_ID = p.MANAGER_ID,
                            .MANAGER_NAME = m.FULLNAME_VN,
                            .STATUS_ID = p.STATUS_ID,
                            .STATUS_NAME = s.NAME_VN,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANAGER_NAME) Then
                lst = lst.Where(Function(f) f.MANAGER_NAME.ToLower().Contains(_filter.MANAGER_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                lst = lst.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            If _filter.ID <> 0 Then
                lst = lst.Where(Function(f) f.ID = _filter.ID)
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

    Public Function InsertAT_PROJECT(ByVal objTitle As AT_PROJECTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.STATUS_ID = objTitle.STATUS_ID
            objTitleData.MANAGER_ID = objTitle.MANAGER_ID
            Context.AT_PROJECT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_PROJECT(ByVal _validate As AT_PROJECTDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_PROJECT
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_PROJECT(ByVal objTitle As AT_PROJECTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_PROJECT Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.STATUS_ID = objTitle.STATUS_ID
            objTitleData.MANAGER_ID = objTitle.MANAGER_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_PROJECT(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_PROJECT)
        Try
            lstHolidayData = (From p In Context.AT_PROJECT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_PROJECT.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Validate tồn tại trạng thái của dự án
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateOtherList(ByVal _validate As OT_OTHERLIST_DTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    If _validate.ACTFLG <> "" Then
                        query = (From p In Context.OT_OTHER_LIST
                                 Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                                 Where q.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID = _validate.ID _
                                 And p.ACTFLG = _validate.ACTFLG
                             ).FirstOrDefault
                        Return (Not query Is Nothing)
                    Else
                        query = (From p In Context.OT_OTHER_LIST
                                 Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                                 And p.ID <> _validate.ID _
                             ).SingleOrDefault
                        Return (query Is Nothing)
                    End If
                Else
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                            ).FirstOrDefault
                    Return (query Is Nothing)
                End If

            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    'And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.OT_OTHER_LIST
                             Where p.ID = _validate.ID).FirstOrDefault
                    ' And p.TYPE_ID = _validate.TYPE_ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Project Work"

    Public Function GetAT_PROJECT_WORK(ByVal _filter As AT_PROJECT_WORKDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_WORKDTO)
        Try

            Dim query = From p In Context.AT_PROJECT_WORK

            Dim lst = query.Select(Function(p) New AT_PROJECT_WORKDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
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

    Public Function InsertAT_PROJECT_WORK(ByVal objTitle As AT_PROJECT_WORKDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_WORK
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT_WORK.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.REMARK = objTitle.REMARK.Trim
            Context.AT_PROJECT_WORK.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ValidateAT_PROJECT_WORK(ByVal _validate As AT_PROJECT_WORKDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT_WORK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.AT_PROJECT_WORK
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PROJECT_WORK
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ModifyAT_PROJECT_WORK(ByVal objTitle As AT_PROJECT_WORKDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_WORK With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_PROJECT_WORK Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME.Trim
            objTitleData.REMARK = objTitle.REMARK.Trim
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_PROJECT_WORK(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_PROJECT_WORK)
        Try
            lstHolidayData = (From p In Context.AT_PROJECT_WORK Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_PROJECT_WORK.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Project Emp"

    Public Function GetAT_PROJECT_EMP(ByVal _filter As AT_PROJECT_EMPDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_EMPDTO)
        Try

            Dim query = From p In Context.AT_PROJECT_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From projtitle In Context.AT_PROJECT_TITLE.Where(Function(f) f.ID = p.PROJECT_TITLE_ID)
                        From proj In Context.AT_PROJECT.Where(Function(f) f.ID = p.PROJECT_ID)
                        Where p.PROJECT_ID = _filter.PROJECT_ID
                        Select New AT_PROJECT_EMPDTO With {
                                       .ID = p.ID,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .PROJECT_TITLE_ID = p.PROJECT_TITLE_ID,
                                       .PROJECT_TITLE_NAME = projtitle.NAME,
                                       .PROJECT_ID = p.PROJECT_ID,
                                       .PROJECT_NAME = proj.NAME,
                                       .COST = p.COST,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.PROJECT_TITLE_NAME) Then
                lst = lst.Where(Function(f) f.PROJECT_TITLE_NAME.ToLower().Contains(_filter.PROJECT_TITLE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.PROJECT_NAME) Then
                lst = lst.Where(Function(f) f.PROJECT_NAME.ToLower().Contains(_filter.PROJECT_NAME.ToLower()))
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

    Public Function InsertAT_PROJECT_EMP(ByVal objTitle As AT_PROJECT_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_PROJECT_EMP.EntitySet.Name)
            objTitleData.COST = objTitle.COST
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.PROJECT_ID = objTitle.PROJECT_ID
            objTitleData.PROJECT_TITLE_ID = objTitle.PROJECT_TITLE_ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            Context.AT_PROJECT_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAT_PROJECT_EMP(ByVal objTitle As AT_PROJECT_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_PROJECT_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_PROJECT_EMP Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.COST = objTitle.COST
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.PROJECT_ID = objTitle.PROJECT_ID
            objTitleData.PROJECT_TITLE_ID = objTitle.PROJECT_TITLE_ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_PROJECT_EMP(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_PROJECT_EMP)
        Try
            lstHolidayData = (From p In Context.AT_PROJECT_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_PROJECT_EMP.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

    Public Function GET_PE_ASSESS_MESS(ByVal EMP As Decimal?) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_MESS",
                                                    New With {.P_EMPLOYEE_ID = EMP,
                                                              .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#Region "Dang ky OT tren iportal"
    'Lay danh sach dang ky tren portal
    Public Function GET_REG_PORTAL(ByVal EMPLOYEEID As Decimal?, ByVal START_DATE As Date?, ByVal END_DATE As Date?, ByVal LSTSTATUS As String, ByVal TYPE As String) As List(Of APPOINTMENT_DTO)

        Dim lst As New List(Of APPOINTMENT_DTO)
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_REG_PORTAL", _
                                                   New With {.P_EMLOYEEID = EMPLOYEEID, _
                                                             .P_FROMDATE = START_DATE, _
                                                             .P_TODATE = END_DATE, _
                                                             .P_LSTSTATUS = LSTSTATUS, _
                                                             .P_TYPE = TYPE, _
                                                             .P_CUR = cls.OUT_CURSOR})

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
        End Using
    End Function
    ' Lấy tổng số giờ OT phê duyệt và số giờ OT đăng ký chưa phê duyệt
    Public Function GET_TOTAL_OT_APPROVE(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPLOYEEID = EMPID, _
                                .P_ENDDATE = ENDDATE, _
                                .P_RESULT = cls.OUT_NUMBER}
            Dim objects = cls.ExecuteStore("PKG_AT.GET_TOTAL_OT_APPROVE", obj)
            Return Decimal.Parse(obj.P_RESULT)
        End Using
    End Function
    Public Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_LISTORG = LISTORG, .P_PERIOD = PERIOD, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT.AT_CHECK_ORG_PERIOD_STATUS_OT", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function
    Public Function GET_LIST_HOURS() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_AT.GET_LIST_HOURS", New With {.P_CUR = cls.OUT_CURSOR})
            Return dt
        End Using
    End Function

    Public Function GET_LIST_MINUTE() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_AT.GET_LIST_MINUTE", New With {.P_CUR = cls.OUT_CURSOR})
            Return dt
        End Using
    End Function

    Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_PROCESS_TYPE = process_type, .P_TOTAL_HOURS = totalHours, .P_TOTAL_DAY = totalDay, .P_SIGN_ID = sign_id, .P_ID_REGGROUP = id_reggroup, .P_TOKEN = token, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function

    Public Function GET_TIMESHEET_PAYSLIP(ByVal employee_id As Decimal, ByVal tag As String) As String
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPLOYEE_ID = employee_id, .P_TAG = tag, .P_RESULT = cls.OUT_STRING}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.GET_TIMESHEET_PAYSLIP", obj)
            Return obj.P_RESULT
        End Using
    End Function

    Public Function PRI_PROCESS_APP_CANCEL(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal id_reggroup As Integer, ByVal token As String, ByVal template_id As Integer) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPLOYEE_APP_ID = employee_id_app, .P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_PROCESS_TYPE = process_type, .P_ID_REGGROUP = id_reggroup, .P_APP_LEVEL = token, .P_TEMPLATE_ID = template_id, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_PROCESS.PRI_PROCESS_APP_CANCEL", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function
    ' lấy seq đăng ký portal
    Public Function GET_SEQ_PORTAL_RGT() As Decimal
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_SEQ_PORTAL_RGT", obj)
            Return Decimal.Parse(obj.P_RESULT)
        End Using
    End Function
    Public Function GET_ORGID(ByVal EMPID As Integer) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPID = EMPID, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_ORGID", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function

    Public Function GetOrgIdLevel2(ByVal EmpId As Decimal) As Decimal
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMP_ID = EmpId, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT_ATTENDANCE_PORTAL.GET_ORG_ID_2", obj)

            Return CType(obj.P_RESULT, Decimal)
        End Using
    End Function
    Public Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_DATE_CURRENT = DATE_CURRENT, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT.GET_PERIOD", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function
    ' Kiểm tra môt số điều kiện khi nhân viên đăng ký nghỉ, đăng ký đi trễ về sớm, đăng ký làm thêm
    Public Function AT_CHECK_EMPLOYEE(ByVal EMPID As Decimal, ByVal ENDDATE As Date) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPID = EMPID, .P_ENDDATE = ENDDATE, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT.AT_CHECK_EMPLOYEE", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function
    ' Lấy tổng số giờ OT phê duyệt và số giờ OT đăng ký chưa phê duyệt
    Public Function GET_TOTAL_OT_APPROVE3(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Using cls As New DataAccess.QueryData
            Dim obj = New With {.P_EMPID = EMPID, .P_ENDDATE = ENDDATE, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT.GET_TOTAL_OT_APPROVE3", obj)
            Return Decimal.Parse(obj.P_RESULT)
        End Using
    End Function
    ' Kiêm tra một số ràng buộc khi đăng ký OT
    Public Function CHECK_RGT_OT(ByVal EMPID As Decimal, ByVal STARTDATE As Date, ByVal ENDDATE As Date, _
                                 ByVal FROM_HOUR As String, ByVal TO_HOUR As String, ByVal HOUR_RGT As Decimal) As Int32
        Using cls As New DataAccess.QueryData
            Dim obj = New With {EMPID, STARTDATE, ENDDATE, FROM_HOUR, TO_HOUR, HOUR_RGT, .P_RESULT = cls.OUT_NUMBER}
            Dim store = cls.ExecuteStore("PKG_AT.CHECK_RGT_OT", obj)
            Return Int32.Parse(obj.P_RESULT)
        End Using
    End Function
#End Region
#Region "cham cong"
    Public Function GetLeaveRegistrationList(ByVal _filter As AT_PORTAL_REG_DTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO)
        Try
            Dim query = From p In Context.AT_PORTAL_REG
                        From ce In Context.HUV_AT_PORTAL.Where(Function(f) f.ID_REGGROUP = p.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.ID_EMPLOYEE).DefaultIfEmpty
                        From user In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.MODIFIED_BY).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From fl In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        From s In Context.SE_USER.Where(Function(f) f.USERNAME = p.MODIFIED_BY).DefaultIfEmpty
                        From sh In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = s.EMPLOYEE_CODE).DefaultIfEmpty
                        Select New AT_PORTAL_REG_DTO With {
                                                               .ID = p.ID,
                                                               .ID_EMPLOYEE = p.ID_EMPLOYEE,
                                                               .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                               .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                               .YEAR = If(p.FROM_DATE.HasValue, p.FROM_DATE.Value.Year, 0),
                                                               .FROM_DATE = ce.FROM_DATE,
                                                               .TO_DATE = ce.TO_DATE,
                                                               .REASON = If(p.STATUS = 2, p.REASON, Nothing),
                                                               .MODIFIED_DATE = p.MODIFIED_DATE,
                                                               .MODIFIED_BY = user.FULLNAME_VN,
                                                               .ID_SIGN = p.ID_SIGN,
                                                               .SIGN_CODE = fl.CODE,
                                                               .TOTAL_LEAVE = ce.NVALUE,
                                                               .SIGN_NAME = fl.NAME,
                                                               .DAYIN_KH = p.DAYIN_KH,
                                                               .DAYOUT_KH = p.DAYOUT_KH,
                                                               .STATUS = p.STATUS,
                                                               .STATUS_NAME = ot.NAME_EN,
                                                               .NOTE = p.NOTE,
                                                            .CREATED_DATE = p.CREATED_DATE,
                                                               .DEPARTMENT = o.NAME_VN,
                                                               .IS_WORK_DAY = If(p.WORK_DAY = 0, False, True),
                                                                .JOBTITLE = t.NAME_VN
                                                            }

            If _filter.ID_EMPLOYEE > 0 Then
                query = query.Where(Function(f) f.ID_EMPLOYEE = _filter.ID_EMPLOYEE)
            End If
            If _filter.ID > 0 Then
                query = query.Where(Function(f) f.ID = _filter.ID)
            End If
            If _filter.YEAR > 0 Then
                query = query.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.STATUS.HasValue Then
                query = query.Where(Function(f) f.STATUS = _filter.STATUS)
            End If

            If _filter.FROM_DATE.HasValue And _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE >= _filter.FROM_DATE And f.TO_DATE <= _filter.TO_DATE)
            ElseIf _filter.FROM_DATE.HasValue Then
                query = query.Where(Function(f) f.FROM_DATE = _filter.FROM_DATE)
            ElseIf _filter.TO_DATE.HasValue Then
                query = query.Where(Function(f) f.TO_DATE = _filter.TO_DATE)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToLower.Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.EMPLOYEE_NAME.ToLower.Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If _filter.NOTE IsNot Nothing Then
                query = query.Where(Function(p) p.NOTE.ToLower.Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToLower.Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If _filter.SIGN_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.SIGN_NAME.ToLower.Contains(_filter.SIGN_NAME.ToLower()))
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function DeletePortalReg(ByVal lstId As List(Of Decimal)) As Boolean
        Try
            'Dim lst = (From p In Context.AT_PORTAL_REG Where lstId.Contains(p.ID_REGGROUP)).ToList()
            Dim lstDetail = (From p In Context.AT_LEAVESHEET_DETAIL Where lstId.Contains(p.LEAVESHEET_ID)).ToList
            For indexD = 0 To lstDetail.Count - 1
                Context.AT_LEAVESHEET_DETAIL.DeleteObject(lstDetail(indexD))
            Next

            Dim lst = (From p In Context.AT_LEAVESHEET Where lstId.Contains(p.ID)).ToList()
            For index = 0 To lst.Count - 1
                Context.AT_LEAVESHEET.DeleteObject(lst(index))
            Next

            Dim lstProcessApprovedStatus = (From p In Context.PROCESS_APPROVED_STATUS Where lstId.Contains(p.ID_REGGROUP)).ToList()
            For index = 0 To lstProcessApprovedStatus.Count - 1
                Context.PROCESS_APPROVED_STATUS.DeleteObject(lstProcessApprovedStatus(index))
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ApprovePortalRegList(ByVal obj As List(Of AT_PORTAL_REG_LIST_DTO), ByVal log As UserLog) As Boolean
        Try
            'For Each item In obj
            '    Dim objData As New AT_PORTAL_REG_LIST With {.ID = item.ID}
            '    Context.AT_PORTAL_REG_LIST.Attach(objData)
            '    objData.STATUS = item.STATUS
            '    objData.REASON = item.REASON
            '    Context.SaveChanges(log)
            '    Dim content As String = ""
            '    Dim emailTo As String = ""
            '    Dim qlttName As String = ""
            '    'Dim objEmail As New Common.CommonBusiness.EmailCommonConfig

            '    Dim leaveInfor = (From p In Context.AT_PORTAL_REG_LIST
            '                      From s In Context.AT_FML.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
            '                      Where p.ID = item.ID
            '                      Select New AT_PORTAL_REG_LIST_DTO With {
            '                      .ID = p.ID,
            '                      .SIGN_NAME = s.NAME_VN
            '                    }).FirstOrDefault()
            '    If leaveInfor IsNot Nothing Then
            '        'Kiếm tra trạng thái để gửi email
            '        If item.STATUS = PortalStatus.WaitingForApproval Or item.STATUS = PortalStatus.UnApprovedByLM Or item.STATUS = PortalStatus.UnVerifiedByHr Then
            '            If item.STATUS = PortalStatus.WaitingForApproval Then
            '                Dim emailInfor = (From rl In Context.AT_PORTAL_REG_LIST
            '                                  From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = rl.EMPLOYEE_ID And rl.ID = item.ID)
            '                                  From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
            '                                  From em In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER).DefaultIfEmpty
            '                                  From em2 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER_2).DefaultIfEmpty
            '                                  From em3 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER_3).DefaultIfEmpty
            '                                  From cvm In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER).DefaultIfEmpty
            '                                  From cvm2 In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER_2).DefaultIfEmpty
            '                                  From cvm3 In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER_3).DefaultIfEmpty
            '                                  Select New Common.CommonBusiness.EmailCommonConfig With {
            '                              .EmailCC = cv.WORK_EMAIL,
            '                              .EmailTo = cvm.WORK_EMAIL,
            '                              .ExtendField1 = e.FULLNAME_EN,
            '                              .ExtendField2 = em.FULLNAME_EN,
            '                              .ExtendField3 = em2.FULLNAME_EN,
            '                              .ExtendField4 = em3.FULLNAME_EN,
            '                              .ExtendField5 = cvm2.WORK_EMAIL,
            '                              .ExtendField6 = cvm3.WORK_EMAIL
            '                          }).FirstOrDefault()
            '                'Dear {Họ tên QLTT},

            '                'My Request of {Loại nghỉ} has been completed.
            '                'Please review and approve. Thank you

            '                'Regards,
            '                '{Họ tên nhân viên}
            '                If emailInfor IsNot Nothing Then
            '                    If emailInfor.EmailTo IsNot Nothing OrElse emailInfor.EmailTo <> "" Then
            '                        emailTo = emailInfor.EmailTo
            '                    End If
            '                    If emailInfor.ExtendField5 IsNot Nothing Then
            '                        emailTo &= ";" & emailInfor.ExtendField5
            '                    End If
            '                    If emailInfor.ExtendField6 IsNot Nothing Then
            '                        emailTo &= ";" & emailInfor.ExtendField6
            '                    End If


            '                    'Lấy tên các QLTT
            '                    If emailInfor.ExtendField2 IsNot Nothing OrElse emailInfor.ExtendField2 <> "" Then
            '                        qlttName = emailInfor.ExtendField2
            '                    End If
            '                    If emailInfor.ExtendField3 IsNot Nothing Then
            '                        qlttName &= "/" & emailInfor.ExtendField3
            '                    End If
            '                    If emailInfor.ExtendField4 IsNot Nothing Then
            '                        qlttName &= "/" & emailInfor.ExtendField4
            '                    End If
            '                    objEmail.EmailFrom = emailInfor.EmailCC 'From cho người tạo
            '                    If emailTo IsNot Nothing Then
            '                        objEmail.EmailTo = emailTo 'Gửi cho các QLTT
            '                    End If
            '                End If


            '                content = String.Format("Dear {0}, <br/><br/>" &
            '                                    "My Request of {1} has been completed.<br/>" &
            '                                    "Please review and approve. Thank you<br/><br/>" &
            '                                    "Regards,<br/>" &
            '                                    "{2}", qlttName, leaveInfor.SIGN_NAME, emailInfor.ExtendField1)
            '                objEmail.EmailSubject = "Leave Request"
            '                objEmail.EmailContent = content
            '                objEmail.ViewName = "iTime_SendMail"

            '            End If

            '            If item.STATUS = PortalStatus.UnApprovedByLM Then
            '                Dim emailInfor = (From p In Context.AT_PORTAL_REG_LIST
            '                                  From se In Context.SE_USER.Where(Function(f) f.USERNAME = p.CREATED_BY)
            '                                  From e In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = se.EMPLOYEE_CODE)
            '                                  From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
            '                                  From em In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER Or f.ID = e.DIRECT_MANAGER_2 Or f.ID = e.DIRECT_MANAGER_3)
            '                                  From cvm In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = em.ID)
            '                                  Where p.ID = item.ID And em.ID = item.EMPLOYEE_ID
            '                                  Select New Common.CommonBusiness.EmailCommonConfig With {
            '                                     .EmailCC = cv.WORK_EMAIL,
            '                                     .EmailTo = cvm.WORK_EMAIL,
            '                                     .ExtendField1 = e.FULLNAME_EN,
            '                                     .ExtendField2 = em.FULLNAME_EN
            '                                }).FirstOrDefault()
            '                'Dear {Họ tên nhân viên},

            '                'Your Request of {Loại nghỉ} has been unapproved.
            '                'Reason: {Lý do không phê duyệt của QLTT}

            '                'Regards,
            '                '{Họ tên QLTT}
            '                content = String.Format("Dear {0}, <br/><br/>" &
            '                                      "Your Request of {1} has been unapproved.<br/>" &
            '                                      "Reason: {2}<br/><br/>" &
            '                                      "Regards,<br/>" &
            '                                      "{3}", emailInfor.ExtendField1, leaveInfor.SIGN_NAME, item.REASON, emailInfor.ExtendField2)
            '                objEmail.EmailFrom = emailInfor.EmailTo 'from cho QLTT
            '                objEmail.EmailTo = emailInfor.EmailCC 'Gửi cho người tạo 
            '                objEmail.EmailSubject = "Leave Request"
            '                objEmail.EmailContent = content
            '                objEmail.ViewName = "iTime_SendMail"
            '            End If

            '            If item.STATUS = PortalStatus.UnVerifiedByHr Then
            '                'Get email and full name of HR
            '                Dim hrInfor = (From e In Context.HU_EMPLOYEE
            '                               From c In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
            '                               Where e.ID = item.EMPLOYEE_ID
            '                               Select New Common.CommonBusiness.EmailCommonConfig With {
            '                                               .ExtendField1 = e.FULLNAME_EN,
            '                                               .ExtendField2 = c.WORK_EMAIL
            '                                           }).FirstOrDefault()

            '                Dim emailInfor = (From p In Context.AT_PORTAL_REG_LIST
            '                                  From se In Context.SE_USER.Where(Function(f) f.USERNAME = p.CREATED_BY)
            '                                  From e In Context.HU_EMPLOYEE.Where(Function(f) f.EMPLOYEE_CODE = se.EMPLOYEE_CODE)
            '                                  From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
            '                                  From em In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER).DefaultIfEmpty
            '                                  From em2 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER_2).DefaultIfEmpty
            '                                  From em3 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER_3).DefaultIfEmpty
            '                                  From cvm In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER).DefaultIfEmpty
            '                                  From cvm2 In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER_2).DefaultIfEmpty
            '                                  From cvm3 In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.DIRECT_MANAGER_3).DefaultIfEmpty
            '                                  Where p.ID = item.ID
            '                                  Select New Common.CommonBusiness.EmailCommonConfig With {
            '                                     .EmailTo = cv.WORK_EMAIL,
            '                                     .ExtendField1 = e.FULLNAME_EN,
            '                                      .ExtendField2 = cvm.WORK_EMAIL,
            '                                      .ExtendField3 = cvm2.WORK_EMAIL,
            '                                      .ExtendField4 = cvm3.WORK_EMAIL
            '                                 }).FirstOrDefault()
            '                'Dear {Họ tên nhân viên},

            '                'Your Leave Request has been unverified by HR.
            '                'Reason: {Lý do không phê duyệt của CBNS}

            '                'Regards,
            '                '{Họ tên CBNS}
            '                content = String.Format("Dear {0}, <br/><br/>" &
            '                                      "Your Request of {1} has been unverified by HR.<br/>" &
            '                                      "Reason: {2}<br/><br/>" &
            '                                      "Regards,<br/>" &
            '                                      "{3}", emailInfor.ExtendField1, leaveInfor.SIGN_NAME, item.REASON, hrInfor.ExtendField1)
            '                objEmail.EmailFrom = hrInfor.ExtendField2 'From nhân sự
            '                objEmail.EmailTo = emailInfor.EmailTo 'Gửi cho người tạo 
            '                'Check email của các quản lý
            '                Dim emailCc = String.Empty
            '                If (emailInfor.ExtendField2 IsNot Nothing) Then
            '                    emailCc = emailInfor.ExtendField2
            '                End If
            '                If (emailInfor.ExtendField3 IsNot Nothing) Then
            '                    emailCc = emailCc & ";" & emailInfor.ExtendField3
            '                End If
            '                If (emailInfor.ExtendField4 IsNot Nothing) Then
            '                    emailCc = emailCc & ";" & emailInfor.ExtendField4
            '                End If
            '                objEmail.EmailCC = emailCc
            '                objEmail.EmailSubject = "Leave Request"
            '                objEmail.EmailContent = content
            '                objEmail.ViewName = "iTime_SendMail"
            '            End If

            '            If objEmail.EmailFrom IsNot Nothing AndAlso objEmail.EmailTo IsNot Nothing Then
            '                'INSERT EMAIL
            '                InsertMailToSystem(objEmail)
            '                Context.SaveChanges(log)
            '            End If
            '        End If

            '    End If

            '    'Nếu Status = ApproveByLM -> tính lại phép năm cho nhân viên đó
            '    If item.STATUS = PortalStatus.ApprovedByLM OrElse item.STATUS = PortalStatus.UnVerifiedByHr Then
            '        'Get data of employee
            '        Dim employee = (From e In Context.HU_EMPLOYEE
            '                        From p In Context.AT_PORTAL_REG_LIST.Where(Function(f) f.EMPLOYEE_ID = e.ID And f.ID = item.ID)
            '                        Select New EmployeeDTO With {
            '                            .ID = e.ID,
            '                            .ORG_ID = e.ORG_ID,
            '                            .EMPLOYEE_CODE = p.CREATED_BY
            '                        }).FirstOrDefault()

            '        If employee IsNot Nothing Then
            '            Using cls As New DataAccess.NonQueryData
            '                cls.ExecuteStore("PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_BY_YEAR",
            '                                               New With {.P_USERNAME = employee.EMPLOYEE_CODE,
            '                                                         .P_USER_ID_CAL = employee.ID,
            '                                                         .P_ORG_ID = employee.ORG_ID,
            '                                                         .P_YEAR = item.YEAR,
            '                                                         .P_ISDISSOLVE = False})
            '            End Using
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
#Region "cham cong newedit"
    Public Function GetEmployeeInfor(ByVal P_EmpId As Decimal?, ByVal P_Org_ID As Decimal?, Optional ByVal fromDate As Date? = Nothing) As DataTable
        Try
            Dim year = If(fromDate.ToString() <> "", fromDate.Value.Year, Date.Now.Date.Year)
            Dim query = From e In Context.HU_EMPLOYEE
                        From at In Context.AT_ENTITLEMENT.Where(Function(f) f.EMPLOYEE_ID = e.ID And year = f.YEAR).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.DIRECT_MANAGER).DefaultIfEmpty
                        Select New EmployeeDTO With {
                        .ID = e.ID,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .FULLNAME_EN = e.FULLNAME_EN,
                            .FULLNAME_VN = e.FULLNAME_VN,
                            .ORG_ID = e.ORG_ID,
                            .ORG_NAME = o.NAME_VN,
                            .ORG_DESC = o.DESCRIPTION_PATH,
                            .TITLE_ID = e.TITLE_ID,
                            .TITLE_NAME_EN = t.NAME_EN,
                            .TITLE_NAME_VN = t.NAME_VN,
                            .DIRECT_MANAGER = direct.DIRECT_MANAGER
            }
            If IsNumeric(P_EmpId) Then
                query = query.Where(Function(f) f.ID = P_EmpId)
            End If
            If IsNumeric(P_Org_ID) Then
                query = query.Where(Function(f) f.ORG_ID = P_Org_ID)
            End If

            Return query.ToList.ToTable
        Catch ex As Exception

        End Try
    End Function
    Public Function GetLeaveRegistrationById(ByVal _filter As AT_PORTAL_REG_DTO) As AT_PORTAL_REG_DTO
        Try
            Dim query = From e In Context.HU_EMPLOYEE
                        From p In Context.AT_PORTAL_REG.Where(Function(f) f.ID_EMPLOYEE = e.ID).DefaultIfEmpty
                        From ce In Context.HUV_AT_PORTAL.Where(Function(f) f.ID_REGGROUP = p.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From fl In Context.AT_FML.Where(Function(f) f.ID = p.ID_SIGN).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS).DefaultIfEmpty
                        Where p.ID_REGGROUP = _filter.ID
                        Select New AT_PORTAL_REG_DTO With {
                                                                           .ID = p.ID,
                                                                           .ID_EMPLOYEE = e.ID,
                                                                           .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                                           .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                                           .DEPARTMENT = o.NAME_VN,
                                                                           .JOBTITLE = t.NAME_VN,
                                                                           .YEAR = If(p.FROM_DATE.HasValue, p.FROM_DATE.Value.Year, 0),
                                                                           .FROM_DATE = ce.FROM_DATE,
                                                                           .TO_DATE = ce.TO_DATE,
                                                                           .ID_SIGN = p.ID_SIGN,
                                                                           .SIGN_CODE = fl.CODE,
                                                                           .TOTAL_LEAVE = ce.NVALUE,
                                                                           .SIGN_NAME = fl.NAME_VN,
                                                                           .STATUS = p.STATUS,
                                                                           .STATUS_NAME = ot.NAME_VN,
                                                                           .NOTE = p.NOTE,
                                                                           .WORK_HARD = p.WORK_DAY,
            .CREATED_DATE = p.CREATED_DATE
                                                                        }
            'Dim check1
            'Dim check2
            'If _filter.ID_EMPLOYEE > 0 Then
            '    check1 = query.Where(Function(f) f.ID_EMPLOYEE = _filter.ID_EMPLOYEE)
            'End If
            If _filter.ID_EMPLOYEE > 0 Then
                query = query.Where(Function(f) f.ID_EMPLOYEE = _filter.ID_EMPLOYEE)
            End If
            'If check1 Is Nothing Then
            '    query = check2
            'End If
            'If True Then
            '    query = check1
            'End If




            'If _filter.ID.HasValue AndAlso _filter.ID.Value > 0 Then
            '    query = query.Where(Function(f) f.ID = _filter.ID)
            'End If

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetperiodID(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As Decimal
        Try
            Dim query = (From d In Context.AT_PERIOD
                         Where d.START_DATE <= fromDate AndAlso d.END_DATE >= fromDate).Select(Function(f) f.ID).FirstOrDefault()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetperiodID_2(ByVal employee_Id As Decimal, ByVal RegDate As Date) As Decimal
        Try
            Dim startDate As Date
            Dim endDate As Date
            Dim EMP_OBJ As Decimal
            'Using cls As New DataAccess.QueryData
            '    Dim dtData As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.GET_OBJECT_EMPLOYEE_ID",
            '                                        New With {.P_EMPLOYEE_ID = employee_Id,
            '                                                  .P_DATE = RegDate,
            '                                                  .P_CUR = cls.OUT_CURSOR})
            '    EMP_OBJ = dtData(0)("OBJECT_EMPLOYEE")
            'End Using

            EMP_OBJ = (From p In Context.HU_WORKING Where p.EMPLOYEE_ID = employee_Id AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) <= EntityFunctions.TruncateTime(RegDate) _
                                                        AndAlso p.IS_MISSION = -1 And p.STATUS_ID = 447 Order By p.EFFECT_DATE Descending Select p.OBJECT_EMPLOYEE_ID).FirstOrDefault

            If Not IsNumeric(EMP_OBJ) Then
                Return -2
            End If
            Dim time_period = (From p In Context.AT_TIME_PERIOD Where p.OBJ_EMPLOYEE_ID = EMP_OBJ Order By p.EFFECTMONTH Descending).FirstOrDefault
            If time_period Is Nothing Then
                Return -1
            End If
            If time_period.TODATE_ENDMONTH Then
                startDate = New Date(RegDate.Year, RegDate.Month, time_period.FROMDATE_PERIOD.Value)
                endDate = startDate.LastDateOfMonth
            ElseIf time_period.FROMDATE_BEFOREMONTH Then
                If RegDate.Day > time_period.TODATE_PERIOD Then
                    startDate = New Date(RegDate.Year, RegDate.Month, time_period.FROMDATE_PERIOD.Value)
                    endDate = New Date(DateAdd(DateInterval.Month, 1, RegDate).Year, DateAdd(DateInterval.Month, 1, RegDate).Month, time_period.TODATE_PERIOD.Value)
                Else
                    startDate = New Date(DateAdd(DateInterval.Month, -1, RegDate).Year, DateAdd(DateInterval.Month, -1, RegDate).Month, time_period.FROMDATE_PERIOD.Value)
                    endDate = New Date(RegDate.Year, RegDate.Month, time_period.TODATE_PERIOD.Value)
                End If
            Else
                startDate = New Date(RegDate.Year, RegDate.Month, time_period.FROMDATE_PERIOD.Value)
                endDate = New Date(RegDate.Year, RegDate.Month, time_period.TODATE_PERIOD.Value)
            End If
            If startDate <= RegDate AndAlso endDate >= RegDate Then
                Dim query = (From d In Context.AT_PERIOD
                             Where d.MONTH = endDate.Month And d.YEAR = endDate.Year).Select(Function(f) f.ID).FirstOrDefault()
                Return query
            Else
                Return 0
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetperiodByEmpObj(ByVal objEmp As Decimal, ByVal _dateGet As Date) As AT_PERIODDTO
        Try
            Dim startDate As Date
            Dim endDate As Date
            Dim obj As New AT_PERIODDTO
            Dim time_period = (From p In Context.AT_TIME_PERIOD Where p.OBJ_EMPLOYEE_ID = objEmp Order By p.EFFECTMONTH Descending).FirstOrDefault
            If time_period Is Nothing Then
                Return Nothing
            End If
            If time_period.TODATE_ENDMONTH Then
                startDate = New Date(_dateGet.Year, _dateGet.Month, time_period.FROMDATE_PERIOD.Value)
                endDate = startDate.LastDateOfMonth
            ElseIf time_period.FROMDATE_BEFOREMONTH Then
                If _dateGet.Day > time_period.TODATE_PERIOD Then
                    startDate = New Date(_dateGet.Year, _dateGet.Month, time_period.FROMDATE_PERIOD.Value)
                    endDate = New Date(DateAdd(DateInterval.Month, 1, _dateGet).Year, DateAdd(DateInterval.Month, 1, _dateGet).Month, time_period.TODATE_PERIOD.Value)
                Else
                    startDate = New Date(DateAdd(DateInterval.Month, -1, _dateGet).Year, DateAdd(DateInterval.Month, -1, _dateGet).Month, time_period.FROMDATE_PERIOD.Value)
                    endDate = New Date(_dateGet.Year, _dateGet.Month, time_period.TODATE_PERIOD.Value)
                End If
            Else
                startDate = New Date(_dateGet.Year, _dateGet.Month, time_period.FROMDATE_PERIOD.Value)
                endDate = New Date(_dateGet.Year, _dateGet.Month, time_period.TODATE_PERIOD.Value)
            End If
            If startDate <= _dateGet AndAlso endDate >= _dateGet Then
                Dim query = (From d In Context.AT_PERIOD
                             Where d.MONTH = endDate.Month And d.YEAR = endDate.Year).Select(Function(f) f.ID).FirstOrDefault()
                obj.ID = query
                obj.START_DATE = startDate
                obj.END_DATE = endDate
            End If
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function GetLeaveEmpDetail(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, Optional ByVal isUpdate As Boolean = False) As List(Of LEAVE_DETAIL_EMP_DTO)
        Try
            Dim query = From d In Context.AT_PORTAL_REG
                        Where d.FROM_DATE >= fromDate And d.FROM_DATE <= toDate And d.ID_EMPLOYEE = employee_Id
                        Select New LEAVE_DETAIL_EMP_DTO With {
             .FROM_DATE = d.FROM_DATE
                            }
            'Dim query = From d In Context.AT_SHIFTCYCLE_EMP_DETAIL
            '            From e In Context.AT_SHIFTCYCLE_EMP.Where(Function(f) f.ID = d.AT_SHIFTCYCLE_EMP_ID)
            '            From c In Context.AT_SHIFTCYCLE.Where(Function(f) f.SHIFT_ID = e.SHIFT_ID)
            '            From cd In Context.AT_SHIFTCYCLE_DETAIL.Where(Function(f) f.SHIFTDATE = d.EFFECTIVEDATE And c.ID = f.SHIFTCYCLE_ID)
            '            From fm In Context.AT_FML.Where(Function(f) f.ID = d.FML_ID)
            '            Where e.EMPLOYEE_ID = employee_Id And d.EFFECTIVEDATE >= fromDate And d.EFFECTIVEDATE <= toDate
            '            Select New LEAVE_DETAIL_EMP_DTO With {
            '                                                   .EMPLOYEE_ID = e.EMPLOYEE_ID,
            '                                                   .EFFECTIVEDATE = d.EFFECTIVEDATE,
            '                                                   .ID_SIGN = cd.FML_ID,
            '                                                   .SHIFT_ID = e.SHIFT_ID,
            '                                                   .IS_LEAVE = fm.IS_LEAVE,
            '                                                   .LEAVE_VALUE = If(d.FML_ID = 4, 0, If(isUpdate, 0, 1)),
            '                                                   .IS_UPDATE = If(isUpdate, 1, 0),
            '                                                   .IS_OFF = If(d.FML_ID = 4, 1, 0)
            '                                                }

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetLeaveInOutKH(ByVal employee_Id As Decimal) As List(Of LEAVEINOUTKHDTO)
        Try
            Dim query = From d In Context.HU_ANNUALLEAVE_PLANS
                        Where d.EMPLOYEE_ID = employee_Id
                        Select New LEAVEINOUTKHDTO With {
             .FROM_DATE = d.START_DATE,
             .TO_DATE = d.END_DATE
                            }
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GETIDFROMPROCESS(ByVal Id As Decimal) As Decimal
        Try
            Dim query = (From p In Context.AT_PORTAL_REG Where p.ID_REGGROUP = Id).Select(Function(F) F.ID_EMPLOYEE).FirstOrDefault()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function
    Public Function GetLeaveRegistrationDetailById(ByVal listId As Decimal) As List(Of AT_PORTAL_REG_DTO)
        Try
            Dim query = From p In Context.AT_PORTAL_REG
                        From ce In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.ID_SIGN)
                        Select New AT_PORTAL_REG_DTO With {
                                                               .ID = p.ID,
                                                               .REGDATE = p.REGDATE,
                                                               .ID_SIGN = p.ID_SIGN,
                                                               .FROM_DATE = p.FROM_DATE,
                                                               .TO_DATE = p.TO_DATE,
                                                               .NVALUE = p.NVALUE,
                                                               .NVALUE_NAME = ce.NAME,
                                                               .SVALUE = p.SVALUE
                                                            }

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
        Finally
            _isAvailable = True
        End Try
    End Function

    Public Function InsertPortalRegList(ByVal obj As AT_PORTAL_REG_LIST_DTO, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByVal log As UserLog _
                                      , ByRef gID As Decimal, ByRef itemExist As AT_PORTAL_REG_DTO, ByRef isOverAnnualLeave As Boolean) As Boolean
        Dim objData As New AT_PORTAL_REG_LIST
        Try
            'Kiểm tra thêm điều kiện nếu số ngày nghỉ vượt quá phép năm hiện có của nhân viên đó trong năm thì báo
            Dim checkSignIsAnnualLeave = (From p In Context.AT_FML
                                          Where p.ID = obj.ID_SIGN _
                                          And p.CODE = "AL").Any()
            If checkSignIsAnnualLeave Then
                Dim checkDataOverAnnualLeave = (From p In Context.AT_ENTITLEMENT
                                                Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID _
                                                    And p.CUR_HAVE < obj.TOTAL_LEAVE _
                                                    And obj.FROM_DATE.Value.Year = p.YEAR).Any()
                If checkDataOverAnnualLeave Then
                    isOverAnnualLeave = True
                    Return False
                End If
            End If


            'Kiểm tra điều kiện trùng ngày đăng ký nghỉ
            For Each item In lstObjDetail
                Dim checkData = (From p In Context.AT_PORTAL_REG
                                 Where p.REGDATE = item.REGDATE _
                                     And p.ID_EMPLOYEE = item.ID_EMPLOYEE).Any()
                If checkData Then
                    itemExist = item
                    Return False
                Else
                    itemExist = Nothing
                End If
            Next

            objData.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_REG_LIST.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.FROM_DATE = obj.FROM_DATE
            objData.TO_DATE = obj.TO_DATE
            objData.ID_SIGN = obj.ID_SIGN
            objData.TOTAL_LEAVE = obj.TOTAL_LEAVE
            objData.STATUS = obj.STATUS
            objData.NOTE = obj.NOTE
            Context.AT_PORTAL_REG_LIST.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID

            'Cap nhat detail
            InsertPortalReg(gID, lstObjDetail, log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function InsertPortalReg(ByVal listID As Decimal, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByVal log As UserLog) As Boolean
        Dim objData As New AT_PORTAL_REG
        Try
            For Each obj In lstObjDetail
                objData = New AT_PORTAL_REG
                objData.ID = Utilities.GetNextSequence(Context, Context.AT_PORTAL_REG.EntitySet.Name)
                objData.ID_EMPLOYEE = obj.ID_EMPLOYEE
                objData.REGDATE = obj.REGDATE
                objData.ID_SIGN = obj.ID_SIGN
                objData.NVALUE = obj.NVALUE
                objData.SVALUE = obj.SVALUE
                objData.AT_PORTAL_REG_LIST_ID = listID
                objData.NOTE = obj.NOTE
                Context.AT_PORTAL_REG.AddObject(objData)
            Next
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "CHECK KI CONG DA DONG HAY CHUA"
    Public Function CHECK_PERIOD_CLOSE1(ByVal periodid As Integer, ByVal EmpId As Integer) As Integer
        Try

            Dim query = (From p In Context.AT_PERIOD.Where(Function(f) f.ID = periodid)
                         From o In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ORG_ID = o.ORG_ID)
                         Where e.ID = EmpId).Select(Function(f) f.o.STATUSCOLEX).FirstOrDefault
            If query.HasValue Then
                Return query
            Else
                query = 1
                Return query
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function CHECK_PERIOD_CLOSE(ByVal periodid As Integer) As Integer
        Try

            Dim query = (From p In Context.AT_PERIOD.Where(Function(f) f.ID = periodid)
                         From o In Context.AT_ORG_PERIOD.Where(Function(f) f.PERIOD_ID = p.ID)).Select(Function(f) f.o.STATUSCOLEX).FirstOrDefault
            If query.HasValue Then
                Return query
            Else
                query = 1
                Return query
            End If
        Catch ex As Exception

        End Try
    End Function
#End Region

    Public Function PRS_DASHBOARD_BY_APPROVE(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_PROCESS_TYPE As String) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_AT_PROCESS.PRS_DASHBOARD_BY_APPROVE", New With {.P_EMPLOYEE_APP_ID = P_EMPLOYEE_APP_ID,
                                                                                                .P_PROCESS_TYPE = P_PROCESS_TYPE, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function

#Region "Thiết lập hệ số OT ngoại lệ "
    Public Function GetAT_Coeff_OT_Exception(ByVal _filter As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIME_EXCEPTIONDTO)
        Try

            Dim query = From p In Context.AT_COEFF_OVERTIME_EXCEPTION
                        Select New AT_COEFF_OVERTIME_EXCEPTIONDTO With {
                                       .ID = p.ID,
                                       .EXCEPTION_COEFF = p.EXCEPTION_COEFF,
                                       .EXCEPTION_COEFF_PIT = p.EXCEPTION_COEFF_PIT,
                                       .EXCEPTION_COEFF_NONPIT = p.EXCEPTION_COEFF_NONPIT,
                                       .NOTE = p.NOTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .EXCEPTION_COEFF_CODE = p.EXCEPTION_COEFF_CODE,
                                       .AUTOGEN = p.AUTOGEN,
                                       .HIDE = p.HIDE,
                                       .ORDERBY = p.ORDERBY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                        .IS_BU = p.IS_BU}

            Dim lst = query

            If _filter.EXCEPTION_COEFF IsNot Nothing Then
                lst = lst.Where(Function(F) F.EXCEPTION_COEFF = _filter.EXCEPTION_COEFF)
            End If

            If _filter.EXCEPTION_COEFF_PIT IsNot Nothing Then
                lst = lst.Where(Function(F) F.EXCEPTION_COEFF_PIT = _filter.EXCEPTION_COEFF_PIT)
            End If

            If _filter.EXCEPTION_COEFF_NONPIT IsNot Nothing Then
                lst = lst.Where(Function(F) F.EXCEPTION_COEFF_NONPIT = _filter.EXCEPTION_COEFF_NONPIT)
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            If _filter.ACTFLG IsNot Nothing Then
                lst = lst.Where(Function(F) F.ACTFLG = _filter.ACTFLG)
            End If

            If _filter.EXCEPTION_COEFF_CODE IsNot Nothing Then
                lst = lst.Where(Function(F) F.EXCEPTION_COEFF_CODE = _filter.EXCEPTION_COEFF_CODE)
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

    Public Function InsertAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_COEFF_OVERTIME_EXCEPTION
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_COEFF_OVERTIME_EXCEPTION.EntitySet.Name)
            objData.EXCEPTION_COEFF = obj.EXCEPTION_COEFF
            objData.EXCEPTION_COEFF_PIT = obj.EXCEPTION_COEFF_PIT
            objData.EXCEPTION_COEFF_NONPIT = obj.EXCEPTION_COEFF_NONPIT
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.AUTOGEN = obj.AUTOGEN
            objData.HIDE = obj.HIDE
            objData.ORDERBY = obj.ORDERBY
            objData.EXCEPTION_COEFF_CODE = obj.EXCEPTION_COEFF_CODE
            objData.IS_BU = obj.IS_BU
            Context.AT_COEFF_OVERTIME_EXCEPTION.AddObject(objData)
            Context.SaveChanges(log)
            If obj.AUTOGEN Then
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_AT_PROCESS.ALTER_OT_COEFF_COL",
                                     New With {.P_COL_NAME = obj.EXCEPTION_COEFF_CODE.Trim.ToUpper,
                                               .P_TABLE_NAME = "AT_OT_REGISTRATION"})
                End Using
            End If
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function UpdateAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New AT_COEFF_OVERTIME_EXCEPTION
        Dim iCount As Integer = 0
        Try
            objData = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION Where p.ID = obj.ID).SingleOrDefault
            objData.EXCEPTION_COEFF = obj.EXCEPTION_COEFF
            objData.EXCEPTION_COEFF_PIT = obj.EXCEPTION_COEFF_PIT
            objData.EXCEPTION_COEFF_NONPIT = obj.EXCEPTION_COEFF_NONPIT
            objData.NOTE = obj.NOTE
            objData.ACTFLG = obj.ACTFLG
            objData.EXCEPTION_COEFF_CODE = obj.EXCEPTION_COEFF_CODE
            objData.AUTOGEN = obj.AUTOGEN
            objData.HIDE = obj.HIDE
            objData.ORDERBY = obj.ORDERBY
            objData.IS_BU = obj.IS_BU
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_COEFF_OVERTIME_EXCEPTION)
        Try
            lstAT_Terminal = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_COEFF_OVERTIME_EXCEPTION.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function ActiveOrDeActive_AT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_COEFF_OVERTIME_EXCEPTION)
        Try
            lstData = (From p In Context.AT_COEFF_OVERTIME_EXCEPTION Where lstID.Contains(p.ID)).ToList
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

#End Region

#Region "Thiết lập kỳ công"
    Public Function GetAT_Time_Period(ByVal _filter As AT_TIME_PERIODDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_PERIODDTO)
        Try

            Dim query = From p In Context.AT_TIME_PERIOD
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_EMPLOYEE_ID).DefaultIfEmpty
                        Select New AT_TIME_PERIODDTO With {
                                       .ID = p.ID,
                                       .EFFECTMONTH = p.EFFECTMONTH,
                                       .OBJ_EMPLOYEE_ID = p.OBJ_EMPLOYEE_ID,
                                       .OBJECT_ATTENDACE_NAME = ot.NAME_VN,
                                       .FROMDATE_PERIOD = p.FROMDATE_PERIOD,
                                       .TODATE_PERIOD = p.TODATE_PERIOD,
                                       .FROMDATE_BEFOREMONTH = p.FROMDATE_BEFOREMONTH,
                                       .FROMDATE_BEFOREMONTHNAME = If(p.FROMDATE_BEFOREMONTH = 0, "", "X"),
                                       .TODATE_ENDMONTH = p.TODATE_ENDMONTH,
                                       .TODATE_ENDMONTHNAME = If(p.TODATE_ENDMONTH = 0, "", "X"),
                                       .NOTE = p.NOTE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG}

            Dim lst = query

            If _filter.EFFECTMONTH IsNot Nothing Then
                lst = lst.Where(Function(F) F.EFFECTMONTH = _filter.EFFECTMONTH)
            End If

            If Not String.IsNullOrEmpty(_filter.OBJECT_ATTENDACE_NAME) Then
                lst = lst.Where(Function(f) f.OBJECT_ATTENDACE_NAME.ToLower().Contains(_filter.OBJECT_ATTENDACE_NAME.ToLower()))
            End If

            If IsNumeric(_filter.FROMDATE_PERIOD) Then
                lst = lst.Where(Function(F) F.FROMDATE_PERIOD = _filter.FROMDATE_PERIOD)
            End If
            If IsNumeric(_filter.TODATE_PERIOD) Then
                lst = lst.Where(Function(F) F.TODATE_PERIOD = _filter.TODATE_PERIOD)
            End If
            If Not String.IsNullOrEmpty(_filter.FROMDATE_BEFOREMONTHNAME) Then
                lst = lst.Where(Function(f) f.FROMDATE_BEFOREMONTHNAME.ToLower().Contains(_filter.FROMDATE_BEFOREMONTHNAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.TODATE_ENDMONTHNAME) Then
                lst = lst.Where(Function(f) f.TODATE_ENDMONTHNAME.ToLower().Contains(_filter.TODATE_ENDMONTHNAME.ToLower()))
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
    Public Function InsertATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_PERIOD
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_PERIOD.EntitySet.Name)
            objTitleData.EFFECTMONTH = objTitle.EFFECTMONTH
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.FROMDATE_PERIOD = objTitle.FROMDATE_PERIOD
            objTitleData.TODATE_PERIOD = objTitle.TODATE_PERIOD
            objTitleData.FROMDATE_BEFOREMONTH = objTitle.FROMDATE_BEFOREMONTH
            objTitleData.TODATE_ENDMONTH = objTitle.TODATE_ENDMONTH
            objTitleData.NOTE = objTitle.NOTE
            Context.AT_TIME_PERIOD.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ModifyATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_PERIOD With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TIME_PERIOD Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EFFECTMONTH = objTitle.EFFECTMONTH
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.FROMDATE_PERIOD = objTitle.FROMDATE_PERIOD
            objTitleData.TODATE_PERIOD = objTitle.TODATE_PERIOD
            objTitleData.FROMDATE_BEFOREMONTH = objTitle.FROMDATE_BEFOREMONTH
            objTitleData.TODATE_ENDMONTH = objTitle.TODATE_ENDMONTH
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteATTimePeriod(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_TIME_PERIOD)
        Try
            lstAT_Terminal = (From p In Context.AT_TIME_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_TIME_PERIOD.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckATTimePeriod(ByVal pdate As Date, ByVal objEmp As Decimal, ByVal id As Decimal) As Boolean
        Dim query
        Try
            If id <> 0 Then
                query = (From p In Context.AT_TIME_PERIOD
                         Where p.EFFECTMONTH.Value.Year = pdate.Year And p.EFFECTMONTH.Value.Month = pdate.Month And p.OBJ_EMPLOYEE_ID = objEmp And p.ID <> id).FirstOrDefault
            Else
                query = (From p In Context.AT_TIME_PERIOD Where p.EFFECTMONTH.Value.Year = pdate.Year And p.EFFECTMONTH.Value.Month = pdate.Month And p.OBJ_EMPLOYEE_ID = objEmp).FirstOrDefault
            End If

            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_AT_TIMEWORK(ByVal P_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_TIMEWORK", New With {.P_ID = P_ID, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function

    Public Function GET_AT_TIMEWORK_EMPLOYEE(ByVal P_ID As Decimal, ByVal P_HE_SO As Decimal?) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_TIMEWORK_EMPLOYEE", New With {.P_CMD = P_ID, .P_HE_SO = P_HE_SO, .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function

    Public Function GET_AT_TIMEWORKSTANDARD(ByVal P_YEAR As Decimal,
                                            ByVal P_OBJ_EMP As Decimal,
                                            ByVal P_NOT_T7 As Decimal,
                                            ByVal P_NOT_CN As Decimal,
                                            ByVal P_NOT_T7_2 As Decimal,
                                            ByVal P_NOT_2T7 As Decimal,
                                            ByVal P_TC As Decimal?,
                                            ByVal P_CMD As Decimal?,
                                            ByVal P_HE_SO As Decimal?) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_LIST.GET_AT_TIMEWORKSTANDARD", New With {.P_YEAR = P_YEAR,
                                                                                                            .P_OBJ_EMP = P_OBJ_EMP,
                                                                                                            .P_NOT_T7 = P_NOT_T7,
                                                                                                            .P_NOT_CN = P_NOT_CN,
                                                                                                            .P_NOT_T7_2 = P_NOT_T7_2,
                                                                                                            .P_NOT_2T7 = P_NOT_2T7,
                                                                                                            .P_TC = P_TC,
                                                                                                            .P_CMD = P_CMD,
                                                                                                            .P_HE_SO = P_HE_SO,
                                                                                                            .P_CUR = cls.OUT_CURSOR})

            Return dt
        End Using
    End Function


    Public Function GetAT_Time_WorkStandard(ByVal _filter As AT_TIME_WORKSTANDARDDTO,
                                            ByVal _param As ParamDTO,
                                            Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_WORKSTANDARDDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.AT_TIME_WORKSTANDARD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From oe In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From te In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_ATTENDANT_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New AT_TIME_WORKSTANDARDDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .OBJ_EMPLOYEE_ID = p.OBJ_EMPLOYEE_ID,
                                       .OBJ_EMPLOYEE_NAME = ot.NAME_VN,
                                       .OBJ_ATTENDANT_ID = p.OBJ_ATTENDANT_ID,
                                       .OBJ_ATTENDANT_NAME = t.NAME_VN,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .ORG_DESC = o.DESCRIPTION_PATH,
                                       .WORKSTANDARD_M1 = p.WORKSTANDARD_M1,
                                       .WORKSTANDARD_M2 = p.WORKSTANDARD_M2,
                                       .WORKSTANDARD_M3 = p.WORKSTANDARD_M3,
                                       .WORKSTANDARD_M4 = p.WORKSTANDARD_M4,
                                       .WORKSTANDARD_M5 = p.WORKSTANDARD_M5,
                                       .WORKSTANDARD_M6 = p.WORKSTANDARD_M6,
                                       .WORKSTANDARD_M7 = p.WORKSTANDARD_M7,
                                       .WORKSTANDARD_M8 = p.WORKSTANDARD_M8,
                                       .WORKSTANDARD_M9 = p.WORKSTANDARD_M9,
                                       .WORKSTANDARD_M10 = p.WORKSTANDARD_M10,
                                       .WORKSTANDARD_M11 = p.WORKSTANDARD_M11,
                                       .WORKSTANDARD_M12 = p.WORKSTANDARD_M12,
                                       .IS_EDIT = p.IS_EDIT,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EMP_ORG_NAME = oe.NAME_VN,
                                       .EMP_ORG_DESC = oe.DESCRIPTION_PATH,
                                       .TITLE_NAME = te.NAME_VN,
                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                       .CONVERSION_FACTOR = p.CONVERSION_FACTOR,
                                       .IS_NOT_CAL_SAT = p.IS_NOT_CAL_SAT,
                                       .IS_NOT_CAL_SUN = p.IS_NOT_CAL_SUN,
                                       .IS_NOT_CAL_HALF_SAT = p.IS_NOT_CAL_HALF_SAT,
                                       .IS_NOT_CAL_2_SAT = p.IS_NOT_CAL_2_SAT,
                                       .MINUS_MONTHLY_AT = p.MINUS_MONTHLY_AT,
                                       .DEFAULT_AT = p.DEFAULT_AT,
                                       .IS_EMPLOYEE = If(p.EMPLOYEE_ID IsNot Nothing And p.EMPLOYEE_ID <> 0, True, False)}

            Dim lst = query

            If IsNumeric(_filter.YEAR) AndAlso _filter.YEAR <> 0 Then
                lst = lst.Where(Function(F) F.YEAR = _filter.YEAR)
            End If

            If IsNumeric(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(F) F.EMPLOYEE_CODE = _filter.EMPLOYEE_CODE)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMP_ORG_NAME) Then
                lst = lst.Where(Function(f) f.EMP_ORG_NAME.ToLower().Contains(_filter.EMP_ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If


            If Not String.IsNullOrEmpty(_filter.OBJ_ATTENDANT_NAME) Then
                lst = lst.Where(Function(f) f.OBJ_ATTENDANT_NAME.ToLower().Contains(_filter.OBJ_ATTENDANT_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.OBJ_EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.OBJ_EMPLOYEE_NAME.ToLower().Contains(_filter.OBJ_EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If IsNumeric(_filter.CONVERSION_FACTOR) Then
                lst = lst.Where(Function(F) F.CONVERSION_FACTOR = _filter.CONVERSION_FACTOR)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M1) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M1 = _filter.WORKSTANDARD_M1)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M2) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M2 = _filter.WORKSTANDARD_M2)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M3) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M3 = _filter.WORKSTANDARD_M3)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M4) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M4 = _filter.WORKSTANDARD_M4)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M5) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M5 = _filter.WORKSTANDARD_M5)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M6) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M6 = _filter.WORKSTANDARD_M6)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M7) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M7 = _filter.WORKSTANDARD_M7)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M8) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M8 = _filter.WORKSTANDARD_M8)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M9) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M9 = _filter.WORKSTANDARD_M9)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M10) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M10 = _filter.WORKSTANDARD_M10)
            End If

            If IsNumeric(_filter.WORKSTANDARD_M11) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M11 = _filter.WORKSTANDARD_M11)
            End If
            If IsNumeric(_filter.WORKSTANDARD_M12) Then
                lst = lst.Where(Function(F) F.WORKSTANDARD_M12 = _filter.WORKSTANDARD_M12)
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

    Public Function InsertATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_WORKSTANDARD
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TIME_WORKSTANDARD.EntitySet.Name)
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.OBJ_ATTENDANT_ID = objTitle.OBJ_ATTENDANT_ID
            objTitleData.WORKSTANDARD_M1 = objTitle.WORKSTANDARD_M1
            objTitleData.WORKSTANDARD_M2 = objTitle.WORKSTANDARD_M2
            objTitleData.WORKSTANDARD_M3 = objTitle.WORKSTANDARD_M3
            objTitleData.WORKSTANDARD_M4 = objTitle.WORKSTANDARD_M4
            objTitleData.WORKSTANDARD_M5 = objTitle.WORKSTANDARD_M5
            objTitleData.WORKSTANDARD_M6 = objTitle.WORKSTANDARD_M6
            objTitleData.WORKSTANDARD_M7 = objTitle.WORKSTANDARD_M7
            objTitleData.WORKSTANDARD_M8 = objTitle.WORKSTANDARD_M8
            objTitleData.WORKSTANDARD_M9 = objTitle.WORKSTANDARD_M9
            objTitleData.WORKSTANDARD_M10 = objTitle.WORKSTANDARD_M10
            objTitleData.WORKSTANDARD_M11 = objTitle.WORKSTANDARD_M11
            objTitleData.WORKSTANDARD_M12 = objTitle.WORKSTANDARD_M12
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.CONVERSION_FACTOR = objTitle.CONVERSION_FACTOR
            objTitleData.IS_NOT_CAL_SAT = objTitle.IS_NOT_CAL_SAT
            objTitleData.IS_NOT_CAL_SUN = objTitle.IS_NOT_CAL_SUN
            objTitleData.IS_NOT_CAL_HALF_SAT = objTitle.IS_NOT_CAL_HALF_SAT
            objTitleData.IS_NOT_CAL_2_SAT = objTitle.IS_NOT_CAL_2_SAT
            objTitleData.MINUS_MONTHLY_AT = objTitle.MINUS_MONTHLY_AT
            objTitleData.DEFAULT_AT = objTitle.DEFAULT_AT
            Context.AT_TIME_WORKSTANDARD.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ModifyATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TIME_WORKSTANDARD With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TIME_WORKSTANDARD Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.OBJ_ATTENDANT_ID = objTitle.OBJ_ATTENDANT_ID
            objTitleData.WORKSTANDARD_M1 = objTitle.WORKSTANDARD_M1
            objTitleData.WORKSTANDARD_M2 = objTitle.WORKSTANDARD_M2
            objTitleData.WORKSTANDARD_M3 = objTitle.WORKSTANDARD_M3
            objTitleData.WORKSTANDARD_M4 = objTitle.WORKSTANDARD_M4
            objTitleData.WORKSTANDARD_M5 = objTitle.WORKSTANDARD_M5
            objTitleData.WORKSTANDARD_M6 = objTitle.WORKSTANDARD_M6
            objTitleData.WORKSTANDARD_M7 = objTitle.WORKSTANDARD_M7
            objTitleData.WORKSTANDARD_M8 = objTitle.WORKSTANDARD_M8
            objTitleData.WORKSTANDARD_M9 = objTitle.WORKSTANDARD_M9
            objTitleData.WORKSTANDARD_M10 = objTitle.WORKSTANDARD_M10
            objTitleData.WORKSTANDARD_M11 = objTitle.WORKSTANDARD_M11
            objTitleData.WORKSTANDARD_M12 = objTitle.WORKSTANDARD_M12
            objTitleData.IS_NOT_CAL_SAT = objTitle.IS_NOT_CAL_SAT
            objTitleData.IS_NOT_CAL_SUN = objTitle.IS_NOT_CAL_SUN
            objTitleData.IS_NOT_CAL_HALF_SAT = objTitle.IS_NOT_CAL_HALF_SAT
            objTitleData.IS_NOT_CAL_2_SAT = objTitle.IS_NOT_CAL_2_SAT
            objTitleData.MINUS_MONTHLY_AT = objTitle.MINUS_MONTHLY_AT
            objTitleData.DEFAULT_AT = objTitle.DEFAULT_AT
            objTitleData.IS_EDIT = 1
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.CONVERSION_FACTOR = objTitle.CONVERSION_FACTOR
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteATTime_WorkStandard(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_TIME_WORKSTANDARD)
        Try
            lstAT_Terminal = (From p In Context.AT_TIME_WORKSTANDARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_TIME_WORKSTANDARD.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckATTime_WorkStandard(ByVal year As Decimal, ByVal orgId As Decimal, ByVal objEmp As Decimal, ByVal id As Decimal, ByVal objAttendance As Decimal) As Boolean
        Dim query
        Try
            'If id <> 0 Then
            '    query = (From p In Context.AT_TIME_WORKSTANDARD
            '             Where p.YEAR = year And p.OBJ_EMPLOYEE_ID = objEmp And p.ID <> id And p.ORG_ID = orgId And p.OBJ_ATTENDANT_ID = objAttendance).FirstOrDefault
            'Else
            query = (From p In Context.AT_TIME_WORKSTANDARD Where p.YEAR = year And p.ID <> id And p.OBJ_EMPLOYEE_ID = objEmp And p.ORG_ID = orgId And p.OBJ_ATTENDANT_ID = objAttendance).FirstOrDefault
            'End If

            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckATTime_WorkStandardEmp(ByVal year As Decimal, ByVal enpID As Decimal, ByVal id As Decimal) As Boolean
        Dim query
        Try
            If id <> 0 Then
                query = (From p In Context.AT_TIME_WORKSTANDARD
                         Where p.YEAR = year And p.EMPLOYEE_ID = enpID And p.ID <> id).FirstOrDefault
            Else
                query = (From p In Context.AT_TIME_WORKSTANDARD Where p.YEAR = year And p.EMPLOYEE_ID = enpID).FirstOrDefault
            End If

            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetAT_Time_WorkStandardID(ByVal id As Decimal) As AT_TIME_WORKSTANDARDDTO
        Try

            Dim query = From p In Context.AT_TIME_WORKSTANDARD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From oe In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From te In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_ATTENDANT_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        Where p.ID = id
                        Select New AT_TIME_WORKSTANDARDDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .OBJ_EMPLOYEE_ID = p.OBJ_EMPLOYEE_ID,
                                       .OBJ_EMPLOYEE_NAME = ot.NAME_VN,
                                       .OBJ_ATTENDANT_ID = p.OBJ_ATTENDANT_ID,
                                       .OBJ_ATTENDANT_NAME = t.NAME_VN,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .WORKSTANDARD_M1 = p.WORKSTANDARD_M1,
                                       .WORKSTANDARD_M2 = p.WORKSTANDARD_M2,
                                       .WORKSTANDARD_M3 = p.WORKSTANDARD_M3,
                                       .WORKSTANDARD_M4 = p.WORKSTANDARD_M4,
                                       .WORKSTANDARD_M5 = p.WORKSTANDARD_M5,
                                       .WORKSTANDARD_M6 = p.WORKSTANDARD_M6,
                                       .WORKSTANDARD_M7 = p.WORKSTANDARD_M7,
                                       .WORKSTANDARD_M8 = p.WORKSTANDARD_M8,
                                       .WORKSTANDARD_M9 = p.WORKSTANDARD_M9,
                                       .WORKSTANDARD_M10 = p.WORKSTANDARD_M10,
                                       .WORKSTANDARD_M11 = p.WORKSTANDARD_M11,
                                       .WORKSTANDARD_M12 = p.WORKSTANDARD_M12,
                                       .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                       .EMPLOYEE_NAME = e.FULLNAME_VN,
                                       .EMP_ORG_NAME = oe.NAME_VN,
                                       .TITLE_NAME = te.NAME_VN,
                                       .IS_NOT_CAL_SAT = p.IS_NOT_CAL_SAT,
                                       .IS_NOT_CAL_SUN = p.IS_NOT_CAL_SUN,
                                       .IS_NOT_CAL_HALF_SAT = p.IS_NOT_CAL_HALF_SAT,
                                       .IS_NOT_CAL_2_SAT = p.IS_NOT_CAL_2_SAT,
                                       .MINUS_MONTHLY_AT = p.MINUS_MONTHLY_AT,
                                       .DEFAULT_AT = p.DEFAULT_AT}

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckCloseOrg(ByVal year As Decimal, ByVal month As Decimal, ByVal orgId As Decimal) As Boolean
        Dim query
        Try
            query = (From p In Context.AT_ORG_PERIOD
                     From at In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                     Where p.ORG_ID = orgId And at.YEAR = year And at.MONTH = month And p.STATUSCOLEX = 0).FirstOrDefault

            Return (query IsNot Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GetAT_COEFF_OVERTIME(ByVal _filter As AT_COEFF_OVERTIMEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIMEDTO)
        Try

            Dim query = From p In Context.AT_COEFF_OVERTIME
                        Select New AT_COEFF_OVERTIMEDTO With {
                                       .ID = p.ID,
                                       .EFFECTDATE = p.EFFECTDATE,
                                       .FROMDATE_NIGHTHOUR = p.FROMDATE_NIGHTHOUR,
                                       .TODATE_NIGHTHOUR = p.TODATE_NIGHTHOUR,
                                       .IS_TOMOROW = p.IS_TOMOROW,
                                       .IS_TOMOROWNAME = If(p.IS_TOMOROW = 0, "", "X"),
                                       .NIGHT_COEFF = p.NIGHT_COEFF,
                                       .WEEKDAY_COEFF = p.WEEKDAY_COEFF,
                                       .WEEKDAY_COEFF_PIT = p.WEEKDAY_COEFF_PIT,
                                       .WEEKDAY_COEFF_NONPIT = p.WEEKDAY_COEFF_NONPIT,
                                       .NIGHTWEEKDAY_COEFF = p.NIGHTWEEKDAY_COEFF,
                                       .NIGHTWEEKDAY_COEFF_PIT = p.NIGHTWEEKDAY_COEFF_PIT,
                                       .NIGHTWEEKDAY_COEFF_NONPIT = p.NIGHTWEEKDAY_COEFF_NONPIT,
                                       .OFFDAY_COEFF = p.OFFDAY_COEFF,
                                       .OFFDAY_COEFF_PIT = p.OFFDAY_COEFF_PIT,
                                       .OFFDAY_COEFF_NONPIT = p.OFFDAY_COEFF_NONPIT,
                                       .NIGHTOFFDAY_COEFF = p.NIGHTOFFDAY_COEFF,
                                       .NIGHTOFFDAY_COEFF_PIT = p.NIGHTOFFDAY_COEFF_PIT,
                                       .NIGHTOFFDAY_COEFF_NONPIT = p.NIGHTOFFDAY_COEFF_NONPIT,
                                       .HOLIDAY_COEFF = p.HOLIDAY_COEFF,
                                       .HOLIDAY_COEFF_NONPIT = p.HOLIDAY_COEFF_NONPIT,
                                       .HOLIDAY_COEFF_PIT = p.HOLIDAY_COEFF_PIT,
                                       .NIGHTHOLIDAY_COEFF = p.NIGHTHOLIDAY_COEFF,
                                       .NIGHTHOLIDAY_COEFF_PIT = p.NIGHTHOLIDAY_COEFF_PIT,
                                       .NIGHTHOLIDAY_COEFF_NONPIT = p.NIGHTHOLIDAY_COEFF_NONPIT,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .OT_MONTH = p.OT_MONTH,
                                       .OT_YEAR = p.OT_YEAR}

            Dim lst = query

            If _filter.EFFECTDATE IsNot Nothing Then
                lst = lst.Where(Function(F) F.EFFECTDATE = _filter.EFFECTDATE)
            End If

            If _filter.FROMDATE_NIGHTHOUR IsNot Nothing Then
                lst = lst.Where(Function(F) F.FROMDATE_NIGHTHOUR = _filter.FROMDATE_NIGHTHOUR)
            End If

            If _filter.OT_MONTH IsNot Nothing Then
                lst = lst.Where(Function(F) F.OT_MONTH = _filter.OT_MONTH)
            End If

            If _filter.OT_YEAR IsNot Nothing Then
                lst = lst.Where(Function(F) F.OT_YEAR = _filter.OT_YEAR)
            End If

            If _filter.TODATE_NIGHTHOUR IsNot Nothing Then
                lst = lst.Where(Function(F) F.TODATE_NIGHTHOUR = _filter.TODATE_NIGHTHOUR)
            End If

            If Not String.IsNullOrEmpty(_filter.IS_TOMOROWNAME) Then
                lst = lst.Where(Function(f) f.IS_TOMOROWNAME.ToLower().Contains(_filter.IS_TOMOROWNAME.ToLower()))
            End If

            If IsNumeric(_filter.NIGHT_COEFF) Then
                lst = lst.Where(Function(F) F.NIGHT_COEFF = _filter.NIGHT_COEFF)
            End If
            If IsNumeric(_filter.WEEKDAY_COEFF) Then
                lst = lst.Where(Function(F) F.WEEKDAY_COEFF = _filter.WEEKDAY_COEFF)
            End If
            If IsNumeric(_filter.WEEKDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.WEEKDAY_COEFF_PIT = _filter.WEEKDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.WEEKDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.WEEKDAY_COEFF_NONPIT = _filter.WEEKDAY_COEFF_NONPIT)
            End If
            If IsNumeric(_filter.NIGHTWEEKDAY_COEFF) Then
                lst = lst.Where(Function(F) F.NIGHTWEEKDAY_COEFF = _filter.NIGHTWEEKDAY_COEFF)
            End If
            If IsNumeric(_filter.NIGHTWEEKDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.NIGHTWEEKDAY_COEFF_PIT = _filter.NIGHTWEEKDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.NIGHTWEEKDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.NIGHTWEEKDAY_COEFF_NONPIT = _filter.NIGHTWEEKDAY_COEFF_NONPIT)
            End If
            If IsNumeric(_filter.OFFDAY_COEFF) Then
                lst = lst.Where(Function(F) F.OFFDAY_COEFF = _filter.OFFDAY_COEFF)
            End If
            If IsNumeric(_filter.OFFDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.OFFDAY_COEFF_PIT = _filter.OFFDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.OFFDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.OFFDAY_COEFF_NONPIT = _filter.OFFDAY_COEFF_NONPIT)
            End If
            If IsNumeric(_filter.NIGHTOFFDAY_COEFF) Then
                lst = lst.Where(Function(F) F.NIGHTOFFDAY_COEFF = _filter.NIGHTOFFDAY_COEFF)
            End If
            If IsNumeric(_filter.NIGHTOFFDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.NIGHTOFFDAY_COEFF_PIT = _filter.NIGHTOFFDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.NIGHTOFFDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.NIGHTOFFDAY_COEFF_NONPIT = _filter.NIGHTOFFDAY_COEFF_NONPIT)
            End If
            If IsNumeric(_filter.HOLIDAY_COEFF) Then
                lst = lst.Where(Function(F) F.HOLIDAY_COEFF = _filter.HOLIDAY_COEFF)
            End If
            If IsNumeric(_filter.HOLIDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.HOLIDAY_COEFF_PIT = _filter.HOLIDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.HOLIDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.HOLIDAY_COEFF_NONPIT = _filter.HOLIDAY_COEFF_NONPIT)
            End If
            If IsNumeric(_filter.NIGHTHOLIDAY_COEFF) Then
                lst = lst.Where(Function(F) F.NIGHTHOLIDAY_COEFF = _filter.NIGHTHOLIDAY_COEFF)
            End If
            If IsNumeric(_filter.NIGHTHOLIDAY_COEFF_PIT) Then
                lst = lst.Where(Function(F) F.NIGHTHOLIDAY_COEFF_PIT = _filter.NIGHTHOLIDAY_COEFF_PIT)
            End If
            If IsNumeric(_filter.NIGHTHOLIDAY_COEFF_NONPIT) Then
                lst = lst.Where(Function(F) F.NIGHTHOLIDAY_COEFF_NONPIT = _filter.NIGHTHOLIDAY_COEFF_NONPIT)
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

    Public Function InsertAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_COEFF_OVERTIME
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_COEFF_OVERTIME.EntitySet.Name)
            objTitleData.EFFECTDATE = objTitle.EFFECTDATE
            objTitleData.FROMDATE_NIGHTHOUR = objTitle.FROMDATE_NIGHTHOUR
            objTitleData.TODATE_NIGHTHOUR = objTitle.TODATE_NIGHTHOUR
            objTitleData.IS_TOMOROW = objTitle.IS_TOMOROW
            objTitleData.NIGHT_COEFF = objTitle.NIGHT_COEFF
            objTitleData.WEEKDAY_COEFF = objTitle.WEEKDAY_COEFF
            objTitleData.WEEKDAY_COEFF_PIT = objTitle.WEEKDAY_COEFF_PIT
            objTitleData.WEEKDAY_COEFF_NONPIT = objTitle.WEEKDAY_COEFF_NONPIT
            objTitleData.NIGHTWEEKDAY_COEFF = objTitle.NIGHTWEEKDAY_COEFF
            objTitleData.NIGHTWEEKDAY_COEFF_PIT = objTitle.NIGHTWEEKDAY_COEFF_PIT
            objTitleData.NIGHTWEEKDAY_COEFF_NONPIT = objTitle.NIGHTWEEKDAY_COEFF_NONPIT
            objTitleData.OFFDAY_COEFF = objTitle.OFFDAY_COEFF
            objTitleData.OFFDAY_COEFF_PIT = objTitle.OFFDAY_COEFF_PIT
            objTitleData.OFFDAY_COEFF_NONPIT = objTitle.OFFDAY_COEFF_NONPIT
            objTitleData.NIGHTOFFDAY_COEFF = objTitle.NIGHTOFFDAY_COEFF
            objTitleData.NIGHTOFFDAY_COEFF_PIT = objTitle.NIGHTOFFDAY_COEFF_PIT
            objTitleData.NIGHTOFFDAY_COEFF_NONPIT = objTitle.NIGHTOFFDAY_COEFF_NONPIT
            objTitleData.HOLIDAY_COEFF = objTitle.HOLIDAY_COEFF
            objTitleData.HOLIDAY_COEFF_PIT = objTitle.HOLIDAY_COEFF_PIT
            objTitleData.HOLIDAY_COEFF_NONPIT = objTitle.HOLIDAY_COEFF_NONPIT
            objTitleData.NIGHTHOLIDAY_COEFF = objTitle.NIGHTHOLIDAY_COEFF
            objTitleData.NIGHTHOLIDAY_COEFF_PIT = objTitle.NIGHTHOLIDAY_COEFF_PIT
            objTitleData.NIGHTHOLIDAY_COEFF_NONPIT = objTitle.NIGHTHOLIDAY_COEFF_NONPIT
            objTitleData.OT_MONTH = objTitle.OT_MONTH
            objTitleData.OT_YEAR = objTitle.OT_YEAR
            Context.AT_COEFF_OVERTIME.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function ModifyAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_COEFF_OVERTIME With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_COEFF_OVERTIME Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EFFECTDATE = objTitle.EFFECTDATE
            objTitleData.FROMDATE_NIGHTHOUR = objTitle.FROMDATE_NIGHTHOUR
            objTitleData.TODATE_NIGHTHOUR = objTitle.TODATE_NIGHTHOUR
            objTitleData.IS_TOMOROW = objTitle.IS_TOMOROW
            objTitleData.NIGHT_COEFF = objTitle.NIGHT_COEFF
            objTitleData.WEEKDAY_COEFF = objTitle.WEEKDAY_COEFF
            objTitleData.WEEKDAY_COEFF_PIT = objTitle.WEEKDAY_COEFF_PIT
            objTitleData.WEEKDAY_COEFF_NONPIT = objTitle.WEEKDAY_COEFF_NONPIT
            objTitleData.NIGHTWEEKDAY_COEFF = objTitle.NIGHTWEEKDAY_COEFF
            objTitleData.NIGHTWEEKDAY_COEFF_PIT = objTitle.NIGHTWEEKDAY_COEFF_PIT
            objTitleData.NIGHTWEEKDAY_COEFF_NONPIT = objTitle.NIGHTWEEKDAY_COEFF_NONPIT
            objTitleData.OFFDAY_COEFF = objTitle.OFFDAY_COEFF
            objTitleData.OFFDAY_COEFF_PIT = objTitle.OFFDAY_COEFF_PIT
            objTitleData.OFFDAY_COEFF_NONPIT = objTitle.OFFDAY_COEFF_NONPIT
            objTitleData.NIGHTOFFDAY_COEFF = objTitle.NIGHTOFFDAY_COEFF
            objTitleData.NIGHTOFFDAY_COEFF_PIT = objTitle.NIGHTOFFDAY_COEFF_PIT
            objTitleData.NIGHTOFFDAY_COEFF_NONPIT = objTitle.NIGHTOFFDAY_COEFF_NONPIT
            objTitleData.HOLIDAY_COEFF = objTitle.HOLIDAY_COEFF
            objTitleData.HOLIDAY_COEFF_PIT = objTitle.HOLIDAY_COEFF_PIT
            objTitleData.HOLIDAY_COEFF_NONPIT = objTitle.HOLIDAY_COEFF_NONPIT
            objTitleData.NIGHTHOLIDAY_COEFF = objTitle.NIGHTHOLIDAY_COEFF
            objTitleData.NIGHTHOLIDAY_COEFF_PIT = objTitle.NIGHTHOLIDAY_COEFF_PIT
            objTitleData.NIGHTHOLIDAY_COEFF_NONPIT = objTitle.NIGHTHOLIDAY_COEFF_NONPIT
            objTitleData.OT_MONTH = objTitle.OT_MONTH
            objTitleData.OT_YEAR = objTitle.OT_YEAR
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
    Public Function DeleteAT_COEFF_OVERTIME(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAT_Terminal As List(Of AT_COEFF_OVERTIME)
        Try
            lstAT_Terminal = (From p In Context.AT_COEFF_OVERTIME Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAT_Terminal.Count - 1
                Context.AT_COEFF_OVERTIME.DeleteObject(lstAT_Terminal(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckAT_COEFF_OVERTIME(ByVal pDate As Date, ByVal id As Decimal) As Boolean
        Dim query
        Try
            If id <> 0 Then
                query = (From p In Context.AT_COEFF_OVERTIME
                         Where p.ID <> id And p.EFFECTDATE = pDate).FirstOrDefault
            Else
                query = (From p In Context.AT_COEFF_OVERTIME Where p.EFFECTDATE = pDate).FirstOrDefault
            End If

            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region



#Region "List ANNUALLEAVE"
    Public Function GetAnnualLeave(ByVal _filter As AT_ANNUAL_LEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVEDTO)
        Try

            Dim query = From p In Context.AT_ANNUALLEAVE_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ANNUAL_LEAVEDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ANNUALLEAVE = p.p.ANNUALLEAVE,
                                       .FROMDATE_EFFECT = p.p.FROMDATE_EFFECT,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.ANNUALLEAVE IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUALLEAVE = _filter.ANNUALLEAVE)
            End If
            If _filter.FROMDATE_EFFECT IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROMDATE_EFFECT = _filter.FROMDATE_EFFECT)
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

    Public Function InsertAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ANNUALLEAVE_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_ANNUALLEAVE_EMP.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ANNUALLEAVE = objTitle.ANNUALLEAVE
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.NOTE = objTitle.NOTE
            Context.AT_ANNUALLEAVE_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ANNUALLEAVE_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_ANNUALLEAVE_EMP Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ANNUALLEAVE = objTitle.ANNUALLEAVE
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function CheckAnnualLeave_DATE(ByVal obj As AT_ANNUAL_LEAVEDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_ANNUALLEAVE_EMP Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And p.FROMDATE_EFFECT = obj.FROMDATE_EFFECT And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAnnualLeave(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_ANNUALLEAVE_EMP)
        Try
            lstHolidayData = (From p In Context.AT_ANNUALLEAVE_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_ANNUALLEAVE_EMP.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "List ANNUALLEAVE ORG"
    Public Function GetAnnualLeaveOrg(ByVal _filter As AT_ANNUAL_LEAVE_ORGDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVE_ORGDTO)
        Try

            Dim query = From p In Context.AT_ANNUALLEAVE_ORG
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJ_EMPLOYEE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID).DefaultIfEmpty
                        From g In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ANNUAL_LEAVE_ORGDTO With {
                                       .ID = p.p.ID,
                                       .ORG_ID = p.p.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_ID = p.p.TITLE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ANNUALLEAVE = p.p.ANNUALLEAVE,
                                       .FROMDATE_EFFECT = p.p.FROMDATE_EFFECT,
                                       .NOTE = p.p.NOTE,
                                       .OBJ_EMPLOYEE_ID = p.p.OBJ_EMPLOYEE_ID,
                                       .OBJ_EMPLOYEE_NAME = p.obj.NAME_VN,
                                       .OBJECT_EMPLOYEE_ID = p.p.OBJECT_EMPLOYEE_ID,
                                       .OBJECT_EMPLOYEE_NAME = p.ot.NAME_VN,
                                       .GRADE_ID = p.p.GRADE_ID,
                                       .GRADE_NAME = p.g.NAME_VN,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.OBJ_EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.OBJ_EMPLOYEE_NAME.ToLower().Contains(_filter.OBJ_EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.OBJECT_EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.OBJECT_EMPLOYEE_NAME.ToLower().Contains(_filter.OBJECT_EMPLOYEE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.GRADE_NAME) Then
                lst = lst.Where(Function(f) f.GRADE_NAME.ToLower().Contains(_filter.GRADE_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.ANNUALLEAVE IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUALLEAVE = _filter.ANNUALLEAVE)
            End If
            If _filter.FROMDATE_EFFECT IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROMDATE_EFFECT = _filter.FROMDATE_EFFECT)
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

    Public Function InsertAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ANNUALLEAVE_ORG
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_ANNUALLEAVE_ORG.EntitySet.Name)
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.OBJECT_EMPLOYEE_ID = objTitle.OBJECT_EMPLOYEE_ID
            objTitleData.GRADE_ID = objTitle.GRADE_ID
            objTitleData.ANNUALLEAVE = objTitle.ANNUALLEAVE
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.NOTE = objTitle.NOTE
            Context.AT_ANNUALLEAVE_ORG.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ANNUALLEAVE_ORG With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_ANNUALLEAVE_ORG Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ORG_ID = objTitle.ORG_ID
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.OBJ_EMPLOYEE_ID = objTitle.OBJ_EMPLOYEE_ID
            objTitleData.OBJECT_EMPLOYEE_ID = objTitle.OBJECT_EMPLOYEE_ID
            objTitleData.GRADE_ID = objTitle.GRADE_ID
            objTitleData.ANNUALLEAVE = objTitle.ANNUALLEAVE
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function CheckAnnualLeaveOrg_DATE(ByVal obj As AT_ANNUAL_LEAVE_ORGDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_ANNUALLEAVE_ORG Where p.ORG_ID = obj.ORG_ID And p.TITLE_ID = obj.TITLE_ID And p.FROMDATE_EFFECT = obj.FROMDATE_EFFECT _
                         And p.OBJ_EMPLOYEE_ID = obj.OBJ_EMPLOYEE_ID And p.OBJECT_EMPLOYEE_ID = obj.OBJECT_EMPLOYEE_ID And obj.GRADE_ID = p.GRADE_ID And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAnnualLeaveOrg(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_ANNUALLEAVE_ORG)
        Try
            lstHolidayData = (From p In Context.AT_ANNUALLEAVE_ORG Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_ANNUALLEAVE_ORG.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "AT SETUP ELEAVE"
    Public Function GetAtSetupELeave(ByVal _filter As AT_SETUP_ELEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_ELEAVEDTO)
        Try

            Dim query = From p In Context.AT_SETUP_ELEAVE

            Dim lst = query.Select(Function(p) New AT_SETUP_ELEAVEDTO With {
                                       .ID = p.ID,
                                       .EFFECT_YEAR = p.EFFECT_YEAR,
                                       .ANNUAL_YEAR = p.ANNUAL_YEAR,
                                       .ANNUAL_YEAR_NAME = If(p.ANNUAL_YEAR = -1, "X", ""),
                                       .ANNUAL_MONTH = p.ANNUAL_MONTH,
                                       .ANNUAL_MONTH_NAME = If(p.ANNUAL_MONTH = -1, "X", ""),
                                       .OFFICIAL_DATE = p.OFFICIAL_DATE,
                                       .OFFICIAL_DATE_NAME = If(p.OFFICIAL_DATE = -1, "X", ""),
                                       .START_DATE = p.START_DATE,
                                       .START_DATE_NAME = If(p.START_DATE = -1, "X", ""),
                                       .OLDANNUAL_TIME = p.OLDANNUAL_TIME,
                                       .OLDANNUAL_RENEW = p.OLDANNUAL_RENEW,
                                       .ANNUAL_TRANSFER = p.ANNUAL_TRANSFER,
                                       .ANNUAL_PAID = p.ANNUAL_PAID,
                                       .STARTDATE_FROM1 = p.STARTDATE_FROM1,
                                       .STARTDATE_TO1 = p.STARTDATE_TO1,
                                       .ANNUAL_START1 = p.ANNUAL_START1,
                                       .STARTDATE_FROM2 = p.STARTDATE_FROM2,
                                       .STARTDATE_TO2 = p.STARTDATE_TO2,
                                       .ANNUAL_START2 = p.ANNUAL_START2,
                                       .LEAVEDATE_FROM1 = p.LEAVEDATE_FROM1,
                                       .LEAVEDATE_TO1 = p.LEAVEDATE_TO1,
                                       .ANNUAL_LEAVE1 = p.ANNUAL_LEAVE1,
                                       .LEAVEDATE_FROM2 = p.LEAVEDATE_FROM2,
                                       .LEAVEDATE_TO2 = p.LEAVEDATE_TO2,
                                       .ANNUAL_LEAVE2 = p.ANNUAL_LEAVE2,
                                       .WORKDATE_FROM1 = p.WORKDATE_FROM1,
                                       .WORKDATE_TO1 = p.WORKDATE_TO1,
                                       .ANNUAL_WORK1 = p.ANNUAL_WORK1,
                                       .WORKDATE_FROM2 = p.WORKDATE_FROM2,
                                       .WORKDATE_TO2 = p.WORKDATE_TO2,
                                       .ANNUAL_WORK2 = p.ANNUAL_WORK2,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG,
                                       .OLDANNUAL_TIME_BU = p.OLDNB_TIME,
                                       .OLDANNUAL_RENEW_BU = p.OLDNB_RENEW,
                                       .ANNUAL_TRANSFER_BU = p.NB_TRANSFER})

            If _filter.EFFECT_YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECT_YEAR = _filter.EFFECT_YEAR)
            End If

            If _filter.ANNUAL_YEAR_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_YEAR_NAME = _filter.ANNUAL_YEAR_NAME)
            End If

            If _filter.ANNUAL_MONTH_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_MONTH_NAME = _filter.ANNUAL_MONTH_NAME)
            End If

            If _filter.OFFICIAL_DATE_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.OFFICIAL_DATE_NAME = _filter.OFFICIAL_DATE_NAME)
            End If

            If _filter.START_DATE_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.START_DATE_NAME = _filter.START_DATE_NAME)
            End If

            If _filter.ANNUAL_YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_YEAR = _filter.ANNUAL_YEAR)
            End If

            If _filter.ANNUAL_YEAR_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_YEAR_NAME = _filter.ANNUAL_YEAR_NAME)
            End If

            If _filter.ANNUAL_MONTH_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_MONTH_NAME = _filter.ANNUAL_MONTH_NAME)
            End If

            If _filter.OFFICIAL_DATE_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.OFFICIAL_DATE_NAME = _filter.OFFICIAL_DATE_NAME)
            End If

            If _filter.START_DATE_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.START_DATE_NAME = _filter.START_DATE_NAME)
            End If

            If _filter.OLDANNUAL_TIME IsNot Nothing Then
                lst = lst.Where(Function(f) f.OLDANNUAL_TIME = _filter.OLDANNUAL_TIME)
            End If

            If _filter.OLDANNUAL_RENEW IsNot Nothing Then
                lst = lst.Where(Function(f) f.OLDANNUAL_RENEW = _filter.OLDANNUAL_RENEW)
            End If

            If _filter.ANNUAL_TRANSFER IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_TRANSFER = _filter.ANNUAL_TRANSFER)
            End If

            If _filter.OLDANNUAL_TIME_BU IsNot Nothing Then
                lst = lst.Where(Function(f) f.OLDANNUAL_TIME_BU = _filter.OLDANNUAL_TIME_BU)
            End If

            If _filter.OLDANNUAL_RENEW_BU IsNot Nothing Then
                lst = lst.Where(Function(f) f.OLDANNUAL_RENEW_BU = _filter.OLDANNUAL_RENEW_BU)
            End If

            If _filter.ANNUAL_TRANSFER_BU IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_TRANSFER_BU = _filter.ANNUAL_TRANSFER_BU)
            End If

            If _filter.ANNUAL_PAID IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_PAID = _filter.ANNUAL_PAID)
            End If

            If _filter.STARTDATE_FROM1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.STARTDATE_FROM1 = _filter.STARTDATE_FROM1)
            End If

            If _filter.STARTDATE_TO1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.STARTDATE_TO1 = _filter.STARTDATE_TO1)
            End If

            If _filter.ANNUAL_START1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_START1 = _filter.ANNUAL_START1)
            End If

            If _filter.STARTDATE_FROM2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.STARTDATE_FROM2 = _filter.STARTDATE_FROM2)
            End If

            If _filter.STARTDATE_TO2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.STARTDATE_TO2 = _filter.STARTDATE_TO2)
            End If

            If _filter.ANNUAL_START2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_START2 = _filter.ANNUAL_START2)
            End If

            If _filter.LEAVEDATE_FROM1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVEDATE_FROM1 = _filter.LEAVEDATE_FROM1)
            End If

            If _filter.LEAVEDATE_TO1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVEDATE_TO1 = _filter.LEAVEDATE_TO1)
            End If

            If _filter.ANNUAL_LEAVE1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_LEAVE1 = _filter.ANNUAL_LEAVE1)
            End If

            If _filter.LEAVEDATE_FROM2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVEDATE_FROM2 = _filter.LEAVEDATE_FROM2)
            End If

            If _filter.LEAVEDATE_TO2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.LEAVEDATE_TO2 = _filter.LEAVEDATE_TO2)
            End If

            If _filter.ANNUAL_LEAVE2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_LEAVE2 = _filter.ANNUAL_LEAVE2)
            End If

            If _filter.WORKDATE_FROM1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.WORKDATE_FROM1 = _filter.WORKDATE_FROM1)
            End If

            If _filter.WORKDATE_TO1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.WORKDATE_TO1 = _filter.WORKDATE_TO1)
            End If

            If _filter.ANNUAL_WORK1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_WORK1 = _filter.ANNUAL_WORK1)
            End If

            If _filter.WORKDATE_FROM2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.WORKDATE_FROM2 = _filter.WORKDATE_FROM2)
            End If

            If _filter.WORKDATE_TO2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.WORKDATE_TO2 = _filter.WORKDATE_TO2)
            End If

            If _filter.ANNUAL_WORK2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.ANNUAL_WORK2 = _filter.ANNUAL_WORK2)
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

    Public Function InsertAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_ELEAVE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SETUP_ELEAVE.EntitySet.Name)
            objTitleData.EFFECT_YEAR = objTitle.EFFECT_YEAR
            objTitleData.ANNUAL_YEAR = objTitle.ANNUAL_YEAR
            objTitleData.ANNUAL_MONTH = objTitle.ANNUAL_MONTH
            objTitleData.OFFICIAL_DATE = objTitle.OFFICIAL_DATE
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.OLDANNUAL_TIME = objTitle.OLDANNUAL_TIME
            objTitleData.OLDANNUAL_RENEW = objTitle.OLDANNUAL_RENEW
            objTitleData.ANNUAL_TRANSFER = objTitle.ANNUAL_TRANSFER
            objTitleData.OLDNB_TIME = objTitle.OLDANNUAL_TIME_BU
            objTitleData.OLDNB_RENEW = objTitle.OLDANNUAL_RENEW_BU
            objTitleData.NB_TRANSFER = objTitle.ANNUAL_TRANSFER_BU
            objTitleData.ANNUAL_PAID = objTitle.ANNUAL_PAID
            objTitleData.STARTDATE_FROM1 = objTitle.STARTDATE_FROM1
            objTitleData.STARTDATE_TO1 = objTitle.STARTDATE_TO1
            objTitleData.ANNUAL_START1 = objTitle.ANNUAL_START1
            objTitleData.STARTDATE_FROM2 = objTitle.STARTDATE_FROM2
            objTitleData.STARTDATE_TO2 = objTitle.STARTDATE_TO2
            objTitleData.ANNUAL_START2 = objTitle.ANNUAL_START2
            objTitleData.LEAVEDATE_FROM1 = objTitle.LEAVEDATE_FROM1
            objTitleData.LEAVEDATE_TO1 = objTitle.LEAVEDATE_TO1
            objTitleData.ANNUAL_LEAVE1 = objTitle.ANNUAL_LEAVE1
            objTitleData.LEAVEDATE_FROM2 = objTitle.LEAVEDATE_FROM2
            objTitleData.LEAVEDATE_TO2 = objTitle.LEAVEDATE_TO2
            objTitleData.ANNUAL_LEAVE2 = objTitle.ANNUAL_LEAVE2
            objTitleData.WORKDATE_FROM1 = objTitle.WORKDATE_FROM1
            objTitleData.WORKDATE_TO1 = objTitle.WORKDATE_TO1
            objTitleData.ANNUAL_WORK1 = objTitle.ANNUAL_WORK1
            objTitleData.WORKDATE_FROM2 = objTitle.WORKDATE_FROM2
            objTitleData.WORKDATE_TO2 = objTitle.WORKDATE_TO2
            objTitleData.ANNUAL_WORK2 = objTitle.ANNUAL_WORK2

            Context.AT_SETUP_ELEAVE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SETUP_ELEAVE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SETUP_ELEAVE Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EFFECT_YEAR = objTitle.EFFECT_YEAR
            objTitleData.ANNUAL_YEAR = objTitle.ANNUAL_YEAR
            objTitleData.ANNUAL_MONTH = objTitle.ANNUAL_MONTH
            objTitleData.OFFICIAL_DATE = objTitle.OFFICIAL_DATE
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.OLDANNUAL_TIME = objTitle.OLDANNUAL_TIME
            objTitleData.OLDANNUAL_RENEW = objTitle.OLDANNUAL_RENEW
            objTitleData.ANNUAL_TRANSFER = objTitle.ANNUAL_TRANSFER
            objTitleData.OLDNB_TIME = objTitle.OLDANNUAL_TIME_BU
            objTitleData.OLDNB_RENEW = objTitle.OLDANNUAL_RENEW_BU
            objTitleData.NB_TRANSFER = objTitle.ANNUAL_TRANSFER_BU
            objTitleData.ANNUAL_PAID = objTitle.ANNUAL_PAID
            objTitleData.STARTDATE_FROM1 = objTitle.STARTDATE_FROM1
            objTitleData.STARTDATE_TO1 = objTitle.STARTDATE_TO1
            objTitleData.ANNUAL_START1 = objTitle.ANNUAL_START1
            objTitleData.STARTDATE_FROM2 = objTitle.STARTDATE_FROM2
            objTitleData.STARTDATE_TO2 = objTitle.STARTDATE_TO2
            objTitleData.ANNUAL_START2 = objTitle.ANNUAL_START2
            objTitleData.LEAVEDATE_FROM1 = objTitle.LEAVEDATE_FROM1
            objTitleData.LEAVEDATE_TO1 = objTitle.LEAVEDATE_TO1
            objTitleData.ANNUAL_LEAVE1 = objTitle.ANNUAL_LEAVE1
            objTitleData.LEAVEDATE_FROM2 = objTitle.LEAVEDATE_FROM2
            objTitleData.LEAVEDATE_TO2 = objTitle.LEAVEDATE_TO2
            objTitleData.ANNUAL_LEAVE2 = objTitle.ANNUAL_LEAVE2
            objTitleData.WORKDATE_FROM1 = objTitle.WORKDATE_FROM1
            objTitleData.WORKDATE_TO1 = objTitle.WORKDATE_TO1
            objTitleData.ANNUAL_WORK1 = objTitle.ANNUAL_WORK1
            objTitleData.WORKDATE_FROM2 = objTitle.WORKDATE_FROM2
            objTitleData.WORKDATE_TO2 = objTitle.WORKDATE_TO2
            objTitleData.ANNUAL_WORK2 = objTitle.ANNUAL_WORK2
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteAtSetupELeave(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SETUP_ELEAVE)
        Try
            lstHolidayData = (From p In Context.AT_SETUP_ELEAVE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SETUP_ELEAVE.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "AT SETUP"
    Public Function GetAtSeniority(ByVal _filter As AT_SENIORITYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SENIORITYDTO)
        Try

            Dim query = From p In Context.AT_SENIORITY

            Dim lst = query.Select(Function(p) New AT_SENIORITYDTO With {
                                       .ID = p.ID,
                                       .EFFECTDATE = p.EFFECTDATE,
                                       .NOTE = p.NOTE,
                                       .YEAR1 = p.YEAR1,
                                       .YEAR2 = p.YEAR2,
                                       .YEAR3 = p.YEAR3,
                                       .YEAR4 = p.YEAR4,
                                       .YEAR5 = p.YEAR5,
                                       .YEAR6 = p.YEAR6,
                                       .YEAR7 = p.YEAR7,
                                       .YEAR8 = p.YEAR8,
                                       .YEAR9 = p.YEAR9,
                                       .YEAR10 = p.YEAR10,
                                       .YEAR11 = p.YEAR11,
                                       .YEAR12 = p.YEAR12,
                                       .YEAR13 = p.YEAR13,
                                       .YEAR14 = p.YEAR14,
                                       .YEAR15 = p.YEAR15,
                                       .YEAR16 = p.YEAR16,
                                       .YEAR17 = p.YEAR17,
                                       .YEAR18 = p.YEAR18,
                                       .YEAR19 = p.YEAR19,
                                       .YEAR20 = p.YEAR20,
                                       .YEAR21 = p.YEAR21,
                                       .YEAR22 = p.YEAR22,
                                       .YEAR23 = p.YEAR23,
                                       .YEAR24 = p.YEAR24,
                                       .YEAR25 = p.YEAR25,
                                       .YEAR26 = p.YEAR26,
                                       .YEAR27 = p.YEAR27,
                                       .YEAR28 = p.YEAR28,
                                       .YEAR29 = p.YEAR29,
                                       .YEAR30 = p.YEAR30,
                                       .YEAR31 = p.YEAR31,
                                       .YEAR32 = p.YEAR32,
                                       .YEAR33 = p.YEAR33,
                                       .YEAR34 = p.YEAR34,
                                       .YEAR35 = p.YEAR35,
                                       .YEAR36 = p.YEAR36,
                                       .YEAR37 = p.YEAR37,
                                       .YEAR38 = p.YEAR38,
                                       .YEAR39 = p.YEAR39,
                                       .YEAR40 = p.YEAR40,
                                       .YEAR41 = p.YEAR41,
                                       .YEAR42 = p.YEAR42,
                                       .YEAR43 = p.YEAR43,
                                       .YEAR44 = p.YEAR44,
                                       .YEAR45 = p.YEAR45,
                                       .YEAR46 = p.YEAR46,
                                       .YEAR47 = p.YEAR47,
                                       .YEAR48 = p.YEAR48,
                                       .YEAR49 = p.YEAR49,
                                       .YEAR50 = p.YEAR50,
                                       .YEAR51 = p.YEAR51,
                                       .YEAR52 = p.YEAR52,
                                       .YEAR53 = p.YEAR53,
                                       .YEAR54 = p.YEAR54,
                                       .YEAR55 = p.YEAR55,
                                       .YEAR56 = p.YEAR56,
                                       .YEAR57 = p.YEAR57,
                                       .YEAR58 = p.YEAR58,
                                       .YEAR59 = p.YEAR59,
                                       .YEAR60 = p.YEAR60,
                                       .CREATED_BY = p.CREATED_BY,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_LOG = p.CREATED_LOG,
                                       .MODIFIED_BY = p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.MODIFIED_LOG})

            If _filter.EFFECTDATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECTDATE = _filter.EFFECTDATE)
            End If

            If _filter.NOTE IsNot Nothing Then
                lst = lst.Where(Function(f) f.NOTE = _filter.NOTE)
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

    Public Function InsertAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SENIORITY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_SENIORITY.EntitySet.Name)
            objTitleData.EFFECTDATE = objTitle.EFFECTDATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.YEAR1 = objTitle.YEAR1
            objTitleData.YEAR2 = objTitle.YEAR2
            objTitleData.YEAR3 = objTitle.YEAR3
            objTitleData.YEAR4 = objTitle.YEAR4
            objTitleData.YEAR5 = objTitle.YEAR5
            objTitleData.YEAR6 = objTitle.YEAR6
            objTitleData.YEAR7 = objTitle.YEAR7
            objTitleData.YEAR8 = objTitle.YEAR8
            objTitleData.YEAR9 = objTitle.YEAR9
            objTitleData.YEAR10 = objTitle.YEAR10
            objTitleData.YEAR11 = objTitle.YEAR11
            objTitleData.YEAR12 = objTitle.YEAR12
            objTitleData.YEAR13 = objTitle.YEAR13
            objTitleData.YEAR14 = objTitle.YEAR14
            objTitleData.YEAR15 = objTitle.YEAR15
            objTitleData.YEAR16 = objTitle.YEAR16
            objTitleData.YEAR17 = objTitle.YEAR17
            objTitleData.YEAR18 = objTitle.YEAR18
            objTitleData.YEAR19 = objTitle.YEAR19
            objTitleData.YEAR20 = objTitle.YEAR20
            objTitleData.YEAR21 = objTitle.YEAR21
            objTitleData.YEAR22 = objTitle.YEAR22
            objTitleData.YEAR23 = objTitle.YEAR23
            objTitleData.YEAR24 = objTitle.YEAR24
            objTitleData.YEAR25 = objTitle.YEAR25
            objTitleData.YEAR26 = objTitle.YEAR26
            objTitleData.YEAR27 = objTitle.YEAR27
            objTitleData.YEAR28 = objTitle.YEAR28
            objTitleData.YEAR29 = objTitle.YEAR29
            objTitleData.YEAR30 = objTitle.YEAR30
            objTitleData.YEAR31 = objTitle.YEAR31
            objTitleData.YEAR32 = objTitle.YEAR32
            objTitleData.YEAR33 = objTitle.YEAR33
            objTitleData.YEAR34 = objTitle.YEAR34
            objTitleData.YEAR35 = objTitle.YEAR35
            objTitleData.YEAR36 = objTitle.YEAR36
            objTitleData.YEAR37 = objTitle.YEAR37
            objTitleData.YEAR38 = objTitle.YEAR38
            objTitleData.YEAR39 = objTitle.YEAR39
            objTitleData.YEAR40 = objTitle.YEAR40
            objTitleData.YEAR41 = objTitle.YEAR41
            objTitleData.YEAR42 = objTitle.YEAR42
            objTitleData.YEAR43 = objTitle.YEAR43
            objTitleData.YEAR44 = objTitle.YEAR44
            objTitleData.YEAR45 = objTitle.YEAR45
            objTitleData.YEAR46 = objTitle.YEAR46
            objTitleData.YEAR47 = objTitle.YEAR47
            objTitleData.YEAR48 = objTitle.YEAR48
            objTitleData.YEAR49 = objTitle.YEAR49
            objTitleData.YEAR50 = objTitle.YEAR50
            objTitleData.YEAR51 = objTitle.YEAR51
            objTitleData.YEAR52 = objTitle.YEAR52
            objTitleData.YEAR53 = objTitle.YEAR53
            objTitleData.YEAR54 = objTitle.YEAR54
            objTitleData.YEAR55 = objTitle.YEAR55
            objTitleData.YEAR56 = objTitle.YEAR56
            objTitleData.YEAR57 = objTitle.YEAR57
            objTitleData.YEAR58 = objTitle.YEAR58
            objTitleData.YEAR59 = objTitle.YEAR59
            objTitleData.YEAR60 = objTitle.YEAR60

            Context.AT_SENIORITY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_SENIORITY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_SENIORITY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EFFECTDATE = objTitle.EFFECTDATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.YEAR1 = objTitle.YEAR1
            objTitleData.YEAR2 = objTitle.YEAR2
            objTitleData.YEAR3 = objTitle.YEAR3
            objTitleData.YEAR4 = objTitle.YEAR4
            objTitleData.YEAR5 = objTitle.YEAR5
            objTitleData.YEAR6 = objTitle.YEAR6
            objTitleData.YEAR7 = objTitle.YEAR7
            objTitleData.YEAR8 = objTitle.YEAR8
            objTitleData.YEAR9 = objTitle.YEAR9
            objTitleData.YEAR10 = objTitle.YEAR10
            objTitleData.YEAR11 = objTitle.YEAR11
            objTitleData.YEAR12 = objTitle.YEAR12
            objTitleData.YEAR13 = objTitle.YEAR13
            objTitleData.YEAR14 = objTitle.YEAR14
            objTitleData.YEAR15 = objTitle.YEAR15
            objTitleData.YEAR16 = objTitle.YEAR16
            objTitleData.YEAR17 = objTitle.YEAR17
            objTitleData.YEAR18 = objTitle.YEAR18
            objTitleData.YEAR19 = objTitle.YEAR19
            objTitleData.YEAR20 = objTitle.YEAR20
            objTitleData.YEAR21 = objTitle.YEAR21
            objTitleData.YEAR22 = objTitle.YEAR22
            objTitleData.YEAR23 = objTitle.YEAR23
            objTitleData.YEAR24 = objTitle.YEAR24
            objTitleData.YEAR25 = objTitle.YEAR25
            objTitleData.YEAR26 = objTitle.YEAR26
            objTitleData.YEAR27 = objTitle.YEAR27
            objTitleData.YEAR28 = objTitle.YEAR28
            objTitleData.YEAR29 = objTitle.YEAR29
            objTitleData.YEAR30 = objTitle.YEAR30
            objTitleData.YEAR31 = objTitle.YEAR31
            objTitleData.YEAR32 = objTitle.YEAR32
            objTitleData.YEAR33 = objTitle.YEAR33
            objTitleData.YEAR34 = objTitle.YEAR34
            objTitleData.YEAR35 = objTitle.YEAR35
            objTitleData.YEAR36 = objTitle.YEAR36
            objTitleData.YEAR37 = objTitle.YEAR37
            objTitleData.YEAR38 = objTitle.YEAR38
            objTitleData.YEAR39 = objTitle.YEAR39
            objTitleData.YEAR40 = objTitle.YEAR40
            objTitleData.YEAR41 = objTitle.YEAR41
            objTitleData.YEAR42 = objTitle.YEAR42
            objTitleData.YEAR43 = objTitle.YEAR43
            objTitleData.YEAR44 = objTitle.YEAR44
            objTitleData.YEAR45 = objTitle.YEAR45
            objTitleData.YEAR46 = objTitle.YEAR46
            objTitleData.YEAR47 = objTitle.YEAR47
            objTitleData.YEAR48 = objTitle.YEAR48
            objTitleData.YEAR49 = objTitle.YEAR49
            objTitleData.YEAR50 = objTitle.YEAR50
            objTitleData.YEAR51 = objTitle.YEAR51
            objTitleData.YEAR52 = objTitle.YEAR52
            objTitleData.YEAR53 = objTitle.YEAR53
            objTitleData.YEAR54 = objTitle.YEAR54
            objTitleData.YEAR55 = objTitle.YEAR55
            objTitleData.YEAR56 = objTitle.YEAR56
            objTitleData.YEAR57 = objTitle.YEAR57
            objTitleData.YEAR58 = objTitle.YEAR58
            objTitleData.YEAR59 = objTitle.YEAR59
            objTitleData.YEAR60 = objTitle.YEAR60
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function DeleteAtSeniority(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_SENIORITY)
        Try
            lstHolidayData = (From p In Context.AT_SENIORITY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_SENIORITY.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function CheckAtSeniority_DATE(ByVal obj As AT_SENIORITYDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_SENIORITY Where p.EFFECTDATE = obj.EFFECTDATE And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "List TOXIC LEAVE EMP"
    Public Function GetAtToxicLeaveEmp(ByVal _filter As AT_TOXIC_LEAVE_EMPDTO,
                                       ByVal log As UserLog,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TOXIC_LEAVE_EMPDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.AT_TOXICLEAVE_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_TOXIC_LEAVE_EMPDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .FROM_EFFECT1 = p.p.FROM_EFFECT1,
                                       .TOXICLEAVE1 = p.p.TOXICLEAVE1,
                                       .FROM_EFFECT2 = p.p.FROM_EFFECT2,
                                       .TOXICLEAVE2 = p.p.TOXICLEAVE2,
                                       .FROM_EFFECT3 = p.p.FROM_EFFECT3,
                                       .TOXICLEAVE3 = p.p.TOXICLEAVE3,
                                       .FROM_EFFECT4 = p.p.FROM_EFFECT4,
                                       .TOXICLEAVE4 = p.p.TOXICLEAVE4,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If _filter.FROM_EFFECT1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_EFFECT1 = _filter.FROM_EFFECT1)
            End If
            If _filter.TOXICLEAVE1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.TOXICLEAVE1 = _filter.TOXICLEAVE1)
            End If
            If _filter.FROM_EFFECT2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_EFFECT2 = _filter.FROM_EFFECT2)
            End If
            If _filter.TOXICLEAVE2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.TOXICLEAVE2 = _filter.TOXICLEAVE2)
            End If
            If _filter.FROM_EFFECT3 IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_EFFECT3 = _filter.FROM_EFFECT3)
            End If
            If _filter.TOXICLEAVE3 IsNot Nothing Then
                lst = lst.Where(Function(f) f.TOXICLEAVE3 = _filter.TOXICLEAVE3)
            End If
            If _filter.FROM_EFFECT4 IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_EFFECT4 = _filter.FROM_EFFECT4)
            End If
            If _filter.TOXICLEAVE4 IsNot Nothing Then
                lst = lst.Where(Function(f) f.TOXICLEAVE4 = _filter.TOXICLEAVE4)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXPIRE_DATE = _filter.EXPIRE_DATE)
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

    Public Function InsertAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TOXICLEAVE_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_TOXICLEAVE_EMP.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_EFFECT1 = objTitle.FROM_EFFECT1
            objTitleData.TOXICLEAVE1 = objTitle.TOXICLEAVE1
            objTitleData.FROM_EFFECT2 = objTitle.FROM_EFFECT2
            objTitleData.TOXICLEAVE2 = objTitle.TOXICLEAVE2
            objTitleData.FROM_EFFECT3 = objTitle.FROM_EFFECT3
            objTitleData.TOXICLEAVE3 = objTitle.TOXICLEAVE3
            objTitleData.FROM_EFFECT4 = objTitle.FROM_EFFECT4
            objTitleData.TOXICLEAVE4 = objTitle.TOXICLEAVE4
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.NOTE = objTitle.NOTE
            Context.AT_TOXICLEAVE_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_TOXICLEAVE_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_TOXICLEAVE_EMP Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_EFFECT1 = objTitle.FROM_EFFECT1
            objTitleData.TOXICLEAVE1 = objTitle.TOXICLEAVE1
            objTitleData.FROM_EFFECT2 = objTitle.FROM_EFFECT2
            objTitleData.TOXICLEAVE2 = objTitle.TOXICLEAVE2
            objTitleData.FROM_EFFECT3 = objTitle.FROM_EFFECT3
            objTitleData.TOXICLEAVE3 = objTitle.TOXICLEAVE3
            objTitleData.FROM_EFFECT4 = objTitle.FROM_EFFECT4
            objTitleData.TOXICLEAVE4 = objTitle.TOXICLEAVE4
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function CheckAtToxicLeaveEmp_DATE(ByVal obj As AT_TOXIC_LEAVE_EMPDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_TOXICLEAVE_EMP Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And p.EFFECT_DATE = obj.EFFECT_DATE And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAtToxicLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_TOXICLEAVE_EMP)
        Try
            lstHolidayData = (From p In Context.AT_TOXICLEAVE_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_TOXICLEAVE_EMP.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "List ADVANCE LEAVE EMP"
    Public Function GetAtAdvanceLeaveEmp(ByVal _filter As AT_ADVANCE_LEAVE_EMPDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ADVANCE_LEAVE_EMPDTO)
        Try

            Dim query = From p In Context.AT_ADVANCELEAVE_EMP
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(p) New AT_ADVANCE_LEAVE_EMPDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ADVANCELEAVE = p.p.ADVANCELEAVE,
                                       .FROMDATE_EFFECT = p.p.FROMDATE_EFFECT,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If

            If _filter.ADVANCELEAVE IsNot Nothing Then
                lst = lst.Where(Function(f) f.ADVANCELEAVE = _filter.ADVANCELEAVE)
            End If

            If _filter.FROMDATE_EFFECT IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROMDATE_EFFECT = _filter.FROMDATE_EFFECT)
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

    Public Function InsertAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ADVANCELEAVE_EMP
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_ADVANCELEAVE_EMP.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.ADVANCELEAVE = objTitle.ADVANCELEAVE
            objTitleData.NOTE = objTitle.NOTE
            Context.AT_ADVANCELEAVE_EMP.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function ModifyAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_ADVANCELEAVE_EMP With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_ADVANCELEAVE_EMP Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROMDATE_EFFECT = objTitle.FROMDATE_EFFECT
            objTitleData.ADVANCELEAVE = objTitle.ADVANCELEAVE
            objTitleData.NOTE = objTitle.NOTE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try

    End Function

    Public Function CheckAtAdvanceLeaveEmp_DATE(ByVal obj As AT_ADVANCE_LEAVE_EMPDTO) As Boolean
        Try
            Dim query = (From p In Context.AT_ADVANCELEAVE_EMP Where p.EMPLOYEE_ID = obj.EMPLOYEE_ID And p.FROMDATE_EFFECT = obj.FROMDATE_EFFECT And (p.ID <> obj.ID OrElse obj.ID Is Nothing)).ToList()
            If query.Count = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function DeleteAtAdvanceLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_ADVANCELEAVE_EMP)
        Try
            lstHolidayData = (From p In Context.AT_ADVANCELEAVE_EMP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_ADVANCELEAVE_EMP.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

#End Region

#Region "Phê duyệt giải trình công portal"
    Public Function Get_At_Late_Combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LATE_COMBACKOUTDTO)
        Try
            _isAvailable = False
            Dim lstID = (From a In Context.PROCESS_APPROVED_STATUS
                         Select New With {.ID = a.ID_REGGROUP}).ToList
            Dim store As DataTable
            Using cls As New DataAccess.QueryData
                store = cls.ExecuteStore("PKG_AT_PROCESS.GET_AT_LATE_COMBACKOUT", New With {.P_EMPLOYEE_ID = _filter.EMPLOYEE_ID, .P_STATUS = _filter.STATUS, .P_FROM_DATE = _filter.FROM_DATE, .P_TO_DATE = _filter.END_DATE, .P_OUT = cls.OUT_CURSOR})
            End Using
            Dim data = New List(Of AT_LATE_COMBACKOUTDTO)
            For Each item As DataRow In store.Rows
                Dim rsl As New AT_LATE_COMBACKOUTDTO
                If item("ID") IsNot Nothing AndAlso Not IsDBNull(item("ID")) Then
                    rsl.ID = item("ID")
                End If
                If item("EMPLOYEE_ID") IsNot Nothing AndAlso Not IsDBNull(item("EMPLOYEE_ID")) Then
                    rsl.EMPLOYEE_ID = item("EMPLOYEE_ID")
                End If
                If item("EMPLOYEE_CODE") IsNot Nothing AndAlso Not IsDBNull(item("EMPLOYEE_CODE")) Then
                    rsl.EMPLOYEE_CODE = item("EMPLOYEE_CODE")
                End If
                If item("FULLNAME_VN") IsNot Nothing AndAlso Not IsDBNull(item("FULLNAME_VN")) Then
                    rsl.VN_FULLNAME = item("FULLNAME_VN")
                End If
                If item("ORG_NAME") IsNot Nothing AndAlso Not IsDBNull(item("ORG_NAME")) Then
                    rsl.ORG_NAME = item("ORG_NAME")
                End If
                If item("WORKINGDAY") IsNot Nothing AndAlso Not IsDBNull(item("WORKINGDAY")) Then
                    rsl.WORKINGDAY = item("WORKINGDAY")
                End If
                If item("TYPE_DMVS_NAME") IsNot Nothing AndAlso Not IsDBNull(item("TYPE_DMVS_NAME")) Then
                    rsl.TYPE_DMVS_NAME = item("TYPE_DMVS_NAME")
                End If
                If item("REGIST_INFO_NAME") IsNot Nothing AndAlso Not IsDBNull(item("REGIST_INFO_NAME")) Then
                    rsl.REGIST_INFO_NAME = item("REGIST_INFO_NAME")
                End If
                If item("FROM_HOUR") IsNot Nothing AndAlso Not IsDBNull(item("FROM_HOUR")) Then
                    rsl.FROM_HOUR = item("FROM_HOUR")
                End If
                If item("TO_HOUR") IsNot Nothing AndAlso Not IsDBNull(item("TO_HOUR")) Then
                    rsl.TO_HOUR = item("TO_HOUR")
                End If
                If item("MINUTE") IsNot Nothing AndAlso Not IsDBNull(item("MINUTE")) Then
                    rsl.MINUTE = item("MINUTE")
                End If
                If item("REMARK") IsNot Nothing AndAlso Not IsDBNull(item("REMARK")) Then
                    rsl.REMARK = item("REMARK")
                End If
                If item("APP_STATUS") IsNot Nothing AndAlso Not IsDBNull(item("APP_STATUS")) Then
                    rsl.STATUS = item("APP_STATUS")
                End If
                If item("REASON") IsNot Nothing AndAlso Not IsDBNull(item("REASON")) Then
                    rsl.REASON = item("REASON").ToString
                End If
                If item("REASON_NAME") IsNot Nothing AndAlso Not IsDBNull(item("REASON_NAME")) Then
                    rsl.REASON_NAME = item("REASON_NAME").ToString
                End If
                If item("FROM_HOUR") IsNot Nothing AndAlso item("TO_HOUR") IsNot Nothing AndAlso Not IsDBNull(item("FROM_HOUR")) AndAlso Not IsDBNull(item("TO_HOUR")) Then
                    rsl.TIME = item("TIME") 'Mid(item("FROM_HOUR").ToString(), item("FROM_HOUR").ToString().Length - 7, 5) & " - " & Mid(item("TO_HOUR").ToString(), item("TO_HOUR").ToString().Length - 7, 5)
                End If
                data.Add(rsl)
            Next

            'Dim result = From p In Context.AT_LATE_COMBACKOUT
            '             From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
            '             From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
            '             From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_DSVM).DefaultIfEmpty
            '             From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGIST_INFO).DefaultIfEmpty
            '             From ott In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ot.TYPE_ID).DefaultIfEmpty
            '             Where ot.ACTFLG = "A" And UCase(ott.CODE) = UCase("DTVS")
            '             Select New AT_LATE_COMBACKOUTDTO With {.ID = p.ID,
            '                                                    .EMPLOYEE_ID = e.ID,
            '                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
            '                                                    .VN_FULLNAME = e.FULLNAME_VN,
            '                                                    .ORG_NAME = o.NAME_VN,
            '                                                    .WORKINGDAY = p.WORKINGDAY,
            '                                                    .TYPE_DMVS_NAME = ot.NAME_VN,
            '                                                    .REGIST_INFO_NAME = ot2.NAME_VN,
            '                                                    .FROM_HOUR = p.FROM_HOUR,
            '                                                    .TO_HOUR = p.TO_HOUR,
            '                                                    .MINUTE = p.MINUTE,
            '                                                    .REMARK = p.REMARK,
            '                                                    .STATUS = p.STATUS,
            '                                                    .CREATED_DATE = p.CREATED_DATE}
            'If _filter.FROM_DATE.HasValue Then
            '    result = result.Where(Function(f) f.WORKINGDAY >= _filter.FROM_DATE)
            'End If
            'If _filter.END_DATE.HasValue Then
            '    result = result.Where(Function(f) f.WORKINGDAY <= _filter.END_DATE)
            'End If

            'result = result.OrderBy(Sorts)
            'Total = result.Count
            'result = result.Skip(PageIndex * PageSize).Take(PageSize)
            'Dim data = result.ToList()
            'For Each item As AT_LATE_COMBACKOUTDTO In data
            '    Dim notes = (From a In Context.PROCESS_APPROVED_STATUS
            '                 Where a.ID_REGGROUP = item.ID
            '                 Order By a.CREATED_DATE Descending
            '                 Select New With {.NOTE = a.APP_NOTES,
            '                                  .STATUS = a.APP_STATUS}).ToList
            '    If notes.Count > 0 Then
            '        item.STATUS = notes(0).STATUS
            '        item.REASON = notes(0).NOTE
            '    Else
            '        item.STATUS = -1
            '        item.REASON = ""
            '    End If
            '    item.TIME = Mid(item.FROM_HOUR.ToString(), item.FROM_HOUR.ToString().Length - 10, 5) & " - " & Mid(item.TO_HOUR.ToString(), item.TO_HOUR.ToString().Length - 10, 5)
            'Next
            'Dim data2 = New List(Of AT_LATE_COMBACKOUTDTO)
            'If _filter.STATUS.HasValue Then
            '    For Each item As AT_LATE_COMBACKOUTDTO In data
            '        If item.STATUS = _filter.STATUS Then
            '            data2.Add(item)
            '        End If
            '    Next
            'End If
            Return data
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
#End Region

    Public Function PRS_Get_TREvaluateCourse_Nofi(ByVal param As AT_PORTAL_REG_DTO,
                                              Optional ByRef Total As Integer = 0,
                                              Optional ByVal PageIndex As Integer = 0,
                                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                                             Optional ByVal log As UserLog = Nothing) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As New DataTable
            Dim ds As DataSet = cls.ExecuteStore("PKG_TRAINING_BUSINESS.PRS_GET_TREVALUATECOURSE_NOFI", New With {.P_EMPLOYEE_ID = param.ID_EMPLOYEE, .P_CUR = cls.OUT_CURSOR}, False)
            If ds IsNot Nothing Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        End Using
    End Function
    Public Function PRS_API_GETNOTIFICATION(ByVal param As SE_NOTIFICATIONDTO,
                                           Optional ByVal UserID As Integer = 0,
                                              Optional ByRef Total As Integer = 0,
                                              Optional ByVal PageIndex As Integer = 0,
                                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                                             Optional ByVal log As UserLog = Nothing) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dt As New DataTable
            Dim ds As DataSet = cls.ExecuteStore("PKG_API_MOBILE.API_GETNOTIFICATION", New With {.P_LANGUAGE = "vi-VN",
                                                                                              .P_USERID = UserID, .P_PAGE = PageIndex, .P_PAGESIZE = PageSize, .P_CUR = cls.OUT_CURSOR, .P_ROWCOUNT = cls.OUT_NUMBER, .P_MESSAGE = cls.OUT_STRING, .P_RESPONSESTATUS = cls.OUT_NUMBER}, False)
            If ds IsNot Nothing Then
                If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                    dt = ds.Tables(0)
                End If
            End If
            Return dt
        End Using
    End Function

#Region "Danh mục Lý do giải trình công"
    Public Function Get_AT_REASON_LIST(ByVal _filter As AT_REASON_LIST_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_REASON_LIST_DTO)
        Try
            Dim query = From p In Context.AT_REASON_LIST
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New AT_REASON_LIST_DTO With {
                                       .ID = p.p.ID,
                                       .NAME = p.p.NAME,
                                       .TYPE_ID = p.p.TYPE_ID,
                                       .TYPE_NAME = p.ot.NAME_VN,
                                       .NOTE = p.p.NOTE,
                                       .ORDER_NUM = p.p.ORDER_NUM,
                                       .STATUS = p.p.STATUS,
                                       .STATUS_NAME = If(p.p.STATUS = 1, "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_DATE = p.p.CREATED_DATE})

            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_NAME.ToLower().Contains(_filter.TYPE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.ORDER_NUM IsNot Nothing Then
                lst = lst.Where(Function(f) f.ORDER_NUM = _filter.ORDER_NUM)
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                lst = lst.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If

            lst = lst.OrderBy("ORDER_NUM ASC,CREATED_DATE DESC")
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Insert_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_REASON_LIST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.AT_REASON_LIST.EntitySet.Name)
            objTitleData.NAME = objTitle.NAME
            objTitleData.TYPE_ID = objTitle.TYPE_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ORDER_NUM = objTitle.ORDER_NUM
            objTitleData.STATUS = objTitle.STATUS
            Context.AT_REASON_LIST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Modify_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New AT_REASON_LIST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.AT_REASON_LIST Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.NAME = objTitle.NAME
            objTitleData.TYPE_ID = objTitle.TYPE_ID
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ORDER_NUM = objTitle.ORDER_NUM
            objTitleData.STATUS = objTitle.STATUS
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Active_AT_REASON_LIST(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal status As Decimal) As Boolean
        Dim lstData As List(Of AT_REASON_LIST)
        Try
            lstData = (From p In Context.AT_REASON_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = status
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function Delete_AT_REASON_LIST(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of AT_REASON_LIST)
        Try
            lstHolidayData = (From p In Context.AT_REASON_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.AT_REASON_LIST.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region

End Class