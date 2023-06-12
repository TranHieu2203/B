Imports System.Reflection
Imports Framework.Data

Partial Public Class AttendanceRepository

#Region "Dashboard"
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Tổng công nghỉ, tổng công thực tế
    ''' </summary>
    ''' <param name="_year"></param>
    ''' <param name="_month"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatisticTotalWorking(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.GET_STATISTIC_TOTAL_WORKING", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Tổng công nghỉ" & " (" & dr("TOTAL_OFF") & ")",
                                   .VALUE = Decimal.Parse(dr("TOTAL_OFF"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Tổng công thực tế" & " (" & dr("TOTAL_WORKING") & ")",
                                   .VALUE = Decimal.Parse(dr("TOTAL_WORKING"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Attendance")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy dữ liệu cho biểu đồ số giờ làm thêm
    ''' </summary>
    ''' <param name="_year"></param>
    ''' <param name="_month"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatisticTimeOff(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.GET_STATISTIC_TIME_OFF", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Nghỉ phép" & " (" & dr("WORKING_P") & ")",
                                   .VALUE = Decimal.Parse(dr("WORKING_P"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Nghỉ bù" & " (" & dr("WORKING_B") & ")",
                                   .VALUE = Decimal.Parse(dr("WORKING_B"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Nghỉ khác" & " (" & dr("WORKING_OTHER") & ")",
                                   .VALUE = Decimal.Parse(dr("WORKING_OTHER"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Attendance")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy dữ liệu cho biểu đồ nghỉ bù, nghỉ phép, tổng công nghỉ
    ''' </summary>
    ''' <param name="_year"></param>
    ''' <param name="_month"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatisticTimeOtByOrg(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.GET_STATISTIC_TIME_OT_BY_ORG", obj)
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Attendance")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 22/08/2017 14:12
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách công theo dự án
    ''' </summary>
    ''' <param name="_year"></param>
    ''' <param name="_month"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatisticTimeProject(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_ATTENDANCE_DASHBOARD.GET_STATISTIC_TIME_PROJECT", obj)
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Attendance")
            Throw ex
        End Try
    End Function


#End Region

End Class
