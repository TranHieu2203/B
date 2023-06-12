Imports System.Reflection
Imports Framework.Data

Partial Public Class PayrollRepository

#Region "Dashboard Payroll"
    Public Function GetStatisticSalary(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_IPAY_DASHBOARD.GET_STATISTIC_SALARY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Lương" & " (" & dr("LUONG") & ")",
                                   .VALUE = Decimal.Parse(dr("LUONG"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Phụ cấp" & " (" & dr("PHU_CAP") & ")",
                                   .VALUE = Decimal.Parse(dr("PHU_CAP"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Phúc lợi" & " (" & dr("PHUC_LOI") & ")",
                                  .VALUE = Decimal.Parse(dr("PHUC_LOI"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Thưởng" & " (" & dr("THUONG") & ")",
                                  .VALUE = Decimal.Parse(dr("THUONG"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Bảo hiểm" & " (" & dr("BAO_HIEM") & ")",
                                  .VALUE = Decimal.Parse(dr("BAO_HIEM"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Khác" & " (" & dr("KHAC") & ")",
                                  .VALUE = Decimal.Parse(dr("KHAC"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function
    Public Function GetStatisticSalaryRange(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_IPAY_DASHBOARD.GET_STATISTIC_SALARY_RANGE", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "1-3 triệu" & " (" & dr("MUC_13") & ")",
                                   .VALUE = Decimal.Parse(dr("MUC_13"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "3-5 triệu" & " (" & dr("MUC_35") & ")",
                                   .VALUE = Decimal.Parse(dr("MUC_35"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "5-7 triệu" & " (" & dr("MUC_57") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_57"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "7-9 triệu" & " (" & dr("MUC_79") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_79"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "9-15 triệu" & " (" & dr("MUC_915") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_915"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = ">15 triệu" & " (" & dr("MUC_TREN15") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_TREN15"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function
    Public Function GetStatisticSalaryIncome(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_IPAY_DASHBOARD.GET_STATISTIC_SALARY_INCOME", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "1-3 triệu" & " (" & dr("MUC_13") & ")",
                                   .VALUE = Decimal.Parse(dr("MUC_13"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "3-5 triệu" & " (" & dr("MUC_35") & ")",
                                   .VALUE = Decimal.Parse(dr("MUC_35"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "5-7 triệu" & " (" & dr("MUC_57") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_57"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "7-9 triệu" & " (" & dr("MUC_79") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_79"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "9-15 triệu" & " (" & dr("MUC_915") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_915"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = ">15 triệu" & " (" & dr("MUC_TREN15") & ")",
                                  .VALUE = Decimal.Parse(dr("MUC_TREN15"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function
    Public Function GetStatisticSalaryAverage(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_IPAY_DASHBOARD.GET_STATISTIC_SALARY_AVERAGE", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                  .NAME = "Lương bình quân",
                                  .VALUE = Decimal.Parse(dr("AVG_LUONG"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Thu nhập bình quân",
                                   .VALUE = Decimal.Parse(dr("AVG_THUNHAP"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function


#End Region

#Region "Dashboard Planning"

    Public Function GetStatisticSalaryTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet

        Try
            Dim dsData As DataSet
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_PLANNING_DASHBOARD.GET_PA_SALARY_TRACKER",
                                 New With {.PV_USERNAME = log.Username,
                                           .P_YEAR = _year,
                                           .PV_CUR = cls.OUT_CURSOR}, False)
            End Using

            Return dsData
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanning")
            Throw ex
        End Try

    End Function

    Public Function GetStatisticEmployeeTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet

        Try
            Dim dsData As DataSet
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_PLANNING_DASHBOARD.GET_PA_EMP_TRACKER",
                                 New With {.PV_USERNAME = log.Username,
                                           .P_YEAR = _year,
                                           .PV_CUR = cls.OUT_CURSOR}, False)
            End Using

            Return dsData
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanning")
            Throw ex
        End Try

    End Function



#End Region

End Class
