Imports System.Reflection
Imports Framework.Data

Partial Public Class TrainingRepository

#Region "Dashboard"

    Public Function GetStatisticCourse(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_DASHBOARD.GET_STATISTIC_COURSE", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Số lượng khóa đào tạo đã thực hiện trong kế hoạch" & " (" & dr("TH_KEHOACH") & ")",
                                   .VALUE = Decimal.Parse(dr("TH_KEHOACH"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Số lượng khóa đào tạo đã thực hiện đột xuất" & " (" & dr("TH_DOTXUAT") & ")",
                                   .VALUE = Decimal.Parse(dr("TH_DOTXUAT"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Số lượng khóa đào tạo theo kế hoạch" & " (" & dr("P_KE_HOACH") & ")",
                                  .VALUE = Decimal.Parse(dr("P_KE_HOACH"))})
                    Next
                End If
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Training")
            Throw ex
        End Try
    End Function

    Public Function GetStatisticFormCost(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_DASHBOARD.GET_STATISTIC_FORMCOST", obj)
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Training")
            Throw ex
        End Try
    End Function

    Public Function GetStatisticDiligence(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_DASHBOARD.GET_STATISTIC_DILIGENCE", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count = 1 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = "Tham dự cuối khóa" & " (" & dr("IS_END") & ")",
                                   .VALUE = Decimal.Parse(dr("IS_END"))})
                        result.Add(New StatisticDTO With {
                                   .NAME = "Có dự thi" & " (" & dr("IS_EXAMS") & ")",
                                   .VALUE = Decimal.Parse(dr("IS_EXAMS"))})
                        result.Add(New StatisticDTO With {
                                  .NAME = "Kết quả đạt" & " (" & dr("IS_REACH") & ")",
                                  .VALUE = Decimal.Parse(dr("IS_REACH"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Training")
            Throw ex
        End Try
    End Function

    Public Function GetStatisticRank(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_TRAINING_DASHBOARD.GET_STATISTIC_RANK", obj)
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Training")
            Throw ex
        End Try
    End Function


#End Region

End Class
