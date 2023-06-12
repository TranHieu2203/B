Imports System.Reflection
Imports Framework.Data

Partial Public Class InsuranceRepository

    Public Function GetStatisticInsPayFund(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_DASHBOARD.GET_STATISTIC_INS_PAYFUND", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Quỹ lương bảo hiểm" & " (" & dr("QUYLUONG_BH") & ")",
                                   .VALUE = Decimal.Parse(dr("QUYLUONG_BH"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Tổng quỹ lương" & " (" & dr("TONG_LUONG") & ")",
                                   .VALUE = Decimal.Parse(dr("TONG_LUONG"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticInsPayFundDetail(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_MONTH = _month,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_DASHBOARD.GET_STC_INS_PAYFUND_DETAIL", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHXH nhân viên đóng" & " (" & dr("NV_BHXH") & ")",
                                   .VALUE = Decimal.Parse(dr("NV_BHXH"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHYT nhân viên đóng" & " (" & dr("NV_BHYT") & ")",
                                   .VALUE = Decimal.Parse(dr("NV_BHYT"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHTN nhân viên đóng" & " (" & dr("NV_BHTN") & ")",
                                   .VALUE = Decimal.Parse(dr("NV_BHTN"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHXH công ty đóng" & " (" & dr("CT_BHXH") & ")",
                                   .VALUE = Decimal.Parse(dr("CT_BHXH"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHYT công ty đóng" & " (" & dr("CT_BHYT") & ")",
                                   .VALUE = Decimal.Parse(dr("CT_BHYT"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "BHTN công ty đóng" & " (" & dr("CT_BHTN") & ")",
                                   .VALUE = Decimal.Parse(dr("CT_BHTN"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStatisticInsRate(ByVal _locationId As Integer) As List(Of StatisticInsRateDTO)
        Try
            Dim result As New List(Of StatisticInsRateDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_LOCATION_ID = _locationId,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_DASHBOARD.GET_INS_SPECIFIED_OBJECTS", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticInsRateDTO With {
                                   .TYPE_NAME = "BHXH",
                                   .COMPANY_RATE = Decimal.Parse(dr("SI_COM")),
                                   .EMPLOYEE_RATE = Decimal.Parse(dr("SI_EMP")),
                                   .TOTAL_RATE = Decimal.Parse(dr("SI_TOTAL"))})
                        result.Add(New StatisticInsRateDTO With {
                                   .TYPE_NAME = "BHYT",
                                   .COMPANY_RATE = Decimal.Parse(dr("HI_COM")),
                                   .EMPLOYEE_RATE = Decimal.Parse(dr("HI_EMP")),
                                   .TOTAL_RATE = Decimal.Parse(dr("HI_TOTAL"))})
                        result.Add(New StatisticInsRateDTO With {
                                   .TYPE_NAME = "BHTN",
                                   .COMPANY_RATE = Decimal.Parse(dr("UI_COM")),
                                   .EMPLOYEE_RATE = Decimal.Parse(dr("UI_EMP")),
                                   .TOTAL_RATE = Decimal.Parse(dr("UI_TOTAL"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function GetStatisticInsCeiling(ByVal _locationId As Integer) As List(Of StatisticInsCeilingDTO)
        Try
            Dim result As New List(Of StatisticInsCeilingDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_LOCATION_ID = _locationId,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_DASHBOARD.GET_INS_SPECIFIED_OBJECTS", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticInsCeilingDTO With {
                                   .NAME = "Lương cơ sở",
                                   .VALUE = Decimal.Parse(dr("SALARY_MIN"))})
                        result.Add(New StatisticInsCeilingDTO With {
                                   .NAME = "Mức trần BHXH",
                                   .VALUE = Decimal.Parse(dr("SI"))})
                        result.Add(New StatisticInsCeilingDTO With {
                                   .NAME = "Mức trần BHYT",
                                   .VALUE = Decimal.Parse(dr("HI"))})
                        result.Add(New StatisticInsCeilingDTO With {
                                   .NAME = "Mức trần BHTN",
                                   .VALUE = Decimal.Parse(dr("UI"))})

                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function


End Class
