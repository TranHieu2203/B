Imports System.Web
Imports Framework.Data

Public Class ProfileDashboardRepository
    Inherits ProfileRepositoryBase

#Region "Profile Dashboard"
    Public Function GetListEmployeeStatistic() As List(Of OtherListDTO)
        Try
            '
            Dim query As List(Of OtherListDTO)
            query = (From p In Context.OT_OTHER_LIST
                     Where p.OT_OTHER_LIST_TYPE.CODE = ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.Name And
                     p.ACTFLG = "A"
                     Order By p.ATTRIBUTE1
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetListChangeStatistic() As List(Of OtherListDTO)
        Try
            Dim query As List(Of OtherListDTO)
            query = (From p In Context.OT_OTHER_LIST
                     Where p.OT_OTHER_LIST_TYPE.CODE = ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.Name And
                     p.ACTFLG = "A"
                     Order By p.ATTRIBUTE1
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Select Case _type
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.GENDER
                    Return GetStatisticGender(_orgID, log)
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.CONTRACT_TYPE
                    Return GetStatisticContractType(_orgID, log)
                Case ProfileCommon.OT_DASHBOARD_EMPLOYEE_STATISTIC.LEARNING
                    Return GetStatisticLearing(_orgID, log)
            End Select
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetChangeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Select Case _type
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_COUNT_YEAR
                    Return GetStatisticEmployeeCountYear(_orgID, log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_COUNT_MON
                    Return GetStatisticEmployeeCountMonth(_orgID, log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.EMP_CHANGE_MON
                    Return GetStatisticEmployeeChangeMonth(_orgID, log)
                Case ProfileCommon.OT_DASHBOARD_CHANGE_STATISTIC.TER_CHANGE_MON
                    Return GetStatisticTerChangeMOnth(_orgID, log)
            End Select
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticGender(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_ORGID = _orgID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_GENDER", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticContractType(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_ORGID = _orgID,
                                .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_CTRACT_TYPE", obj)

                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                       .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                       .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticLearing(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_ORGID = _orgID,
                                .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_LEARNING", obj)

                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                       .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                       .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticEmployeeCountYear(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMPLOYEE_YEAR",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_ORGID = _orgID,
                                                              .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of StatisticDTO)()
            End Using

            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticEmployeeCountMonth(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMPLOYEE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                               .P_ORGID = _orgID,
                                                              .P_CUR = cls.OUT_CURSOR})
                lst = dtData.ToList(Of StatisticDTO)()
            End Using

            For i = 1 To 12
                Dim str As String = IIf(i < 10, "0" & i.ToString, i.ToString())
                Dim q = (From p In lst Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    lst.Add(New StatisticDTO With {.NAME = str, .VALUE = 0})
                End If
            Next
            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticTerChangeMOnth(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst1 As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_TER_CHANGE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_ORGID = _orgID,
                                                              .P_CUR = cls.OUT_CURSOR}, False)
                lst1 = dsData.Tables(0).ToList(Of StatisticDTO)()
            End Using

            For i = 0 To lst1.Count - 1
                Dim r As New StatisticDTO
                r.NAME = lst1(i).NAME
                r.VALUE = lst1(i).VALUE
                result.Add(r)
            Next

            For i = 1 To 12
                Dim str As String
                str = i.ToString
                If i > 0 And i < 10 Then
                    str = "0" & i.ToString()
                End If
                Dim q = (From p In result Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    result.Add(New StatisticDTO With {.NAME = str, .VALUE = 0, .VALUE_SECOND = 0})
                End If
            Next
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetStatisticEmployeeChangeMonth(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Dim lst1 As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_EMP_CHANGE_MONTH",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_ORGID = _orgID,
                                                              .P_CUR = cls.OUT_CURSOR}, False)
                lst1 = dsData.Tables(0).ToList(Of StatisticDTO)()
            End Using

            For i = 0 To lst1.Count - 1
                Dim r As New StatisticDTO
                r.NAME = lst1(i).NAME
                r.VALUE = lst1(i).VALUE
                result.Add(r)
            Next

            For i = 1 To 12
                Dim str As String
                str = i.ToString
                If i > 0 And i < 10 Then
                    str = "0" & i.ToString()
                End If
                Dim q = (From p In result Where p.NAME = str).FirstOrDefault
                If q Is Nothing Then
                    result.Add(New StatisticDTO With {.NAME = str, .VALUE = 0, .VALUE_SECOND = 0})
                End If
            Next
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticSeniority(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_ORGID = _orgID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_STATISTIC_SENIORITY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "1 năm" & " (" & dr("MOT") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("MOT") Is Nothing, 0, dr("MOT")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "2 năm" & " (" & dr("HAI") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("HAI") Is Nothing, 0, dr("HAI")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "3 năm" & " (" & dr("BA") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("BA") Is Nothing, 0, dr("BA")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "4 năm" & " (" & dr("BON") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("BON") Is Nothing, 0, dr("BON")))})
                        result.Add(New StatisticDTO With {
                                   .NAME = ">5 năm" & " (" & dr("NAM") & ")",
                                   .VALUE = Decimal.Parse(IIf(dr("NAM") Is Nothing, 0, dr("BON")))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Reminder"

    Public Function GetRemind(ByVal sRemind As String, ByVal log As UserLog, Optional ByVal _orgID As Decimal = 1) As List(Of ReminderLogDTO)
        Try
            Dim query As New List(Of ReminderLogDTO)()
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _orgID,
                                           .P_ISDISSOLVE = -1})
            End Using
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_REMIND",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_REMIND = sRemind,
                                                              .P_CUR = cls.OUT_CURSOR})
                Dim listOrg = String.Join(",", (From k In Context.SE_CHOSEN_ORG.Where(Function(f) f.USERNAME.ToUpper = log.Username.ToUpper)
                                                Select k.ORG_ID).ToArray)
                Dim queryDt = dtData.Select("ORG_ID in (" & listOrg & ")")
                If queryDt.Length > 0 Then
                    query = queryDt.CopyToDataTable.ToList(Of ReminderLogDTO)()
                End If
            End Using
            Return query

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCompanyNewInfo(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim query As List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_DASHBOARD.GET_COMPANY_NEW_INFO",
                                                    New With {.P_USERNAME = log.Username,
                                                              .P_ORG_ID = _orgID,
                                                              .P_CUR = cls.OUT_CURSOR})
                query = dtData.ToList(Of StatisticDTO)()
            End Using

            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#End Region

#Region "Competency Dashboard"
    Public Function GetStatisticTop5Competency(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMPETENCY_DASHBOARD.GET_STATISTIC_TOP5_COMPETENCY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Return New List(Of StatisticDTO)
            'Throw ex
        End Try
    End Function

    Public Function GetStatisticTop5CopAvg(ByVal _year As Integer, ByVal log As UserLog) As List(Of CompetencyAvgEmplDTO)
        Try
            Dim result As New List(Of CompetencyAvgEmplDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMPETENCY_DASHBOARD.GET_STATISTIC_TOP5_COP_AVG", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New CompetencyAvgEmplDTO With {
                                   .COMPETENCY_ID = dr("ID"),
                                   .COMPETENCY_NAME = dr("NV_NAME"),
                                   .LEVEL_NUMBER_STANDARD = Decimal.Parse(dr("NL_CHUAN")),
                                   .LEVEL_NUMBER_ASS_AVG = Decimal.Parse(dr("NLNV_TB"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region
#Region "Portal Dashboard"
    Public Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        Try
            Dim result As New DataTable

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID_EMPLOYEE = _employee_id,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_EMPLOYEE_REG", obj)
                'If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                result = dtData
                'End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                                Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        Try
            Dim result As New TotalDayOffDTO
            Using cls As New DataAccess.QueryData

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

                    result.DASHBOARD_TONG_SO_PHEP = dtData.Rows(0)("DASHBOARD_TONG_SO_PHEP")
                    result.DASHBOARD_SO_PHEP_NAM_CU_CON_LAI = dtData.Rows(0)("DASHBOARD_SO_PHEP_NAM_CU_CON_LAI")
                    result.DASHBOARD_SO_PHEP_NAM_CON_LAI = dtData.Rows(0)("DASHBOARD_SO_PHEP_NAM_CON_LAI")
                    result.DASHBOARD_SO_PHEP_BU_CON_LAI = dtData.Rows(0)("DASHBOARD_SO_PHEP_BU_CON_LAI")
                    result.DASHBOARD_SO_PHEP_DOC_HAI = dtData.Rows(0)("DASHBOARD_SO_PHEP_DOC_HAI")
                    result.DASHBOARD_SO_PHEP_THAM_NIEN = dtData.Rows(0)("DASHBOARD_SO_PHEP_THAM_NIEN")
                End If
            End Using
            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
        Try
            'Dim result As New DataTable

            'Using cls As New DataAccess.QueryData
            '    Dim obj = New With {.P_EMPLOYEE_ID = _employee_id,
            '                        .P_CUR = cls.OUT_CURSOR}
            '    Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_CERTIFICATE_EXPIRES", obj)
            '    If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
            '        result = dtData
            '    End If
            'End Using
            'Return result
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PORTAL_DASHBOARD.GET_CERTIFICATE_EXPIRES",
                                           New With {.P_EMPLOYEE_ID = _employee_id,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
