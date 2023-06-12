Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Newtonsoft.Json.Linq

Partial Class ProfileRepository
    ''' <lastupdate>
    ''' 12/07/2017 14:28
    ''' </lastupdate>
    ''' <summary>
    ''' Lay danh sach bao cao
    ''' </summary>
    ''' <param name="_filter"></param>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="_param"></param>
    ''' <param name="Sorts"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReportList(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        Try
            Dim wSTT = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And f.USERNAME = log.Username.ToUpper)
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_3B = p.IS_3B,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .IS_3B_SHORT = p.IS_3B,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .CREATED_DATE = p.CREATED_DATE}


            ' danh sách thông tin quá trình công tác
            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function Chart_HDLD(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Dim filterObj As New JObject
            If Not String.IsNullOrEmpty(_strFilter) Then
                filterObj = JObject.Parse(_strFilter)
            End If
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_HDLD_EXPORT",
                                                               New With {.P_ORG = strOrg,
                                                                         .P_MONTH = param.MONTH,
                                                                         .P_YEAR = param.YEAR,
                                                                         .P_WORKPLACE = If(Not IsNothing(filterObj("work_place")), CType(filterObj("work_place").ToString, Decimal?), Nothing),
                                                                         .P_JOB_BAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                                         .P_USERNAME = log.Username,
                                                                         .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_HDLD",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_WORKPLACE = If(Not IsNothing(filterObj("work_place")), CType(filterObj("work_place").ToString, Decimal?), Nothing),
                                                     .P_JOB_BAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function Chart_Age(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_AGE_EXPORT",
                                                               New With {.P_ORG = strOrg,
                                                                         .P_MONTH = param.MONTH,
                                                                         .P_YEAR = param.YEAR,
                                                                         .P_USERNAME = log.Username,
                                                                         .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_AGE",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_TRINHDO_HOCVAN(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TRINHDO_HOCVAN_EXPORT",
                                                             New With {.P_ORG = strOrg,
                                                                       .P_MONTH = param.MONTH,
                                                                       .P_YEAR = param.YEAR,
                                                                       .P_USERNAME = log.Username,
                                                                       .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TRINHDO_HOCVAN",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_GENDER(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_GENDER_EXPORT",
                                                               New With {.P_ORG = strOrg,
                                                                         .P_MONTH = param.MONTH,
                                                                         .P_YEAR = param.YEAR,
                                                                         .P_USERNAME = log.Username,
                                                                         .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_GENDER",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_TNCT(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)

            Dim filterObj As New JObject
            If Not String.IsNullOrEmpty(_strFilter) Then
                filterObj = JObject.Parse(_strFilter)
            End If
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TNCT_EXPORT",
                                                               New With {.P_ORG = strOrg,
                                                                         .P_MONTH = param.MONTH,
                                                                         .P_YEAR = param.YEAR,
                                                                         .P_JOBBAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                                         .P_USERNAME = log.Username,
                                                                         .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TNCT",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_JOBBAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_HANHCHINH(ByVal param As ParamDTO,
                                       ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData

                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_HANHCHINH",
                                           New With {.P_ORG = param.ORG_ID,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_TRINHDO_NGOAINGU(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TRINHDO_NGOAINGU_EXPORT",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TRINHDO_NGOAINGU",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_EmpObj(ByVal param As ParamDTO,
                                       ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_EMOBJ",
                                           New With {.P_ORG = param.ORG_ID,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_BAC_LAO_DONG(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Dim filterObj As New JObject
            If Not String.IsNullOrEmpty(_strFilter) Then
                filterObj = JObject.Parse(_strFilter)
            End If
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_BAC_LAO_DONG_EXPORT",
                                                              New With {.P_ORG = strOrg,
                                                                        .P_MONTH = param.MONTH,
                                                                        .P_YEAR = param.YEAR,
                                                                        .P_GENDER = If(Not IsNothing(filterObj("gender")), CType(filterObj("gender").ToString, Decimal?), Nothing),
                                                                        .P_USERNAME = log.Username,
                                                                        .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_BAC_LAO_DONG",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_GENDER = If(Not IsNothing(filterObj("gender")), CType(filterObj("gender").ToString, Decimal?), Nothing),
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_NEW_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_NEW_EMPLOYEE_EXPORT",
                                           New With {.P_ORG = strOrg,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_NEW_EMPLOYEE",
                                           New With {.P_ORG = strOrg,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_TER_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TER_EMPLOYEE_EXPORT",
                                           New With {.P_ORG = strOrg,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_TER_EMPLOYEE",
                                           New With {.P_ORG = strOrg,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_BO_NHIEM(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Dim filterObj As New JObject
            If Not String.IsNullOrEmpty(_strFilter) Then
                filterObj = JObject.Parse(_strFilter)
            End If
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_BO_NHIEM_EXPORT",
                                          New With {.P_ORG = strOrg,
                                                    .P_YEAR = param.YEAR,
                                                    .P_WORKPLACE = If(Not IsNothing(filterObj("work_place")), CType(filterObj("work_place").ToString, Decimal?), Nothing),
                                                    .P_JOB_BAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                    .P_USERNAME = log.Username,
                                                    .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_BO_NHIEM",
                                           New With {.P_ORG = strOrg,
                                                     .P_YEAR = param.YEAR,
                                                     .P_WORKPLACE = If(Not IsNothing(filterObj("work_place")), CType(filterObj("work_place").ToString, Decimal?), Nothing),
                                                     .P_JOB_BAND = If(Not IsNothing(filterObj("job_band")), CType(filterObj("job_band").ToString, Decimal?), Nothing),
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_Employee_Num(ByVal param As ParamDTO, ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If _strFilter IsNot Nothing Then
                Dim filterObj = JObject.Parse(_strFilter)

                If Not String.IsNullOrEmpty(filterObj("work_place")) Then
                    WHERE_CONDITION &= " AND X.WORK_PLACE_ID = " & filterObj("work_place").ToString()
                End If
                If Not String.IsNullOrEmpty(filterObj("gender")) Then
                    WHERE_CONDITION &= " AND X.GENDER = " & filterObj("gender").ToString()
                End If
                If Not String.IsNullOrEmpty(filterObj("job_band")) Then
                    WHERE_CONDITION &= " AND X.STAFF_RANK_ID = " & filterObj("job_band").ToString()
                End If
                If Not String.IsNullOrEmpty(filterObj("age")) Then
                    If filterObj("age").ToString = 1 Then
                        WHERE_CONDITION &= " AND X.AGE < 30 "
                    ElseIf filterObj("age").ToString = 2 Then
                        WHERE_CONDITION &= " AND X.AGE BETWEEN 30 AND 35 "
                    ElseIf filterObj("age").ToString = 3 Then
                        WHERE_CONDITION &= " AND X.AGE BETWEEN 36 AND 40 "
                    ElseIf filterObj("age").ToString = 4 Then
                        WHERE_CONDITION &= " AND X.AGE BETWEEN 41 AND 50 "
                    ElseIf filterObj("age").ToString = 5 Then
                        WHERE_CONDITION &= " AND X.AGE BETWEEN 51 AND 58 "
                    ElseIf filterObj("age").ToString = 6 Then
                        WHERE_CONDITION &= " AND X.AGE >= 59 "
                    End If
                End If
                If Not String.IsNullOrEmpty(filterObj("exp")) Then
                    If filterObj("exp").ToString = 1 Then
                        WHERE_CONDITION &= " AND X.EXP < 1 "
                    ElseIf filterObj("exp").ToString = 2 Then
                        WHERE_CONDITION &= " AND X.EXP >=1 AND X.EXP < 2"
                    ElseIf filterObj("exp").ToString = 3 Then
                        WHERE_CONDITION &= " AND X.EXP >=2 AND X.EXP < 3"
                    ElseIf filterObj("exp").ToString = 4 Then
                        WHERE_CONDITION &= " AND X.EXP >=3 AND X.EXP < 5"
                    ElseIf filterObj("exp").ToString = 5 Then
                        WHERE_CONDITION &= " AND X.EXP >= 5 "
                    End If
                End If
                If Not String.IsNullOrEmpty(filterObj("learning_level")) Then
                    WHERE_CONDITION &= " AND X.LEARNING_LEVEL = " & filterObj("learning_level").ToString()
                End If
            End If

            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_EMPLOYEE_NUM_EXPORT",
                                           New With {.P_ORG = param.ORG_ID,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_WHERE = WHERE_CONDITION,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else

                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_EMPLOYEE_NUM",
                                           New With {.P_ORG = param.ORG_ID,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_WHERE = WHERE_CONDITION,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Chart_WorkPlace(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable
        Try
            Dim strOrg = String.Join(",", _lstOrg)
            Dim filterObj As New JObject
            If Not String.IsNullOrEmpty(_strFilter) Then
                filterObj = JObject.Parse(_strFilter)
            End If
            Using cls As New DataAccess.QueryData
                If isExport = 1 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_WORK_PLACE_EXPORT",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                Else
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_CHART.GET_WORK_PLACE",
                                           New With {.P_ORG = strOrg,
                                                     .P_MONTH = param.MONTH,
                                                     .P_YEAR = param.YEAR,
                                                     .P_USERNAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                    Return dtData
                End If

            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

End Class
