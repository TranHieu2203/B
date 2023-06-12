Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class PerformanceRepository
#Region "Dashboard"

    Public Function GetStatisticKPIByClassification(ByVal _periodId As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_PERIOD_ID = _periodId,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_DASHBOARD.GET_STATISTIC_KPI_BY_CLASSIFI", obj)
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

    Public Function GetStatisticKPIByClassifiOrg(ByVal _periodId As Integer, ByVal log As UserLog) As DataTable
        Try
            Dim result As DataTable

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_PERIOD_ID = _periodId,
                                    .P_CUR = cls.OUT_CURSOR}
                result = cls.ExecuteStore("PKG_PERFORMANCE_DASHBOARD.GET_KPI_BY_CLASSIFI_ORG", obj)
            End Using

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    Public Function GetStatisticClassification(Optional ByVal Sorts As String = "CODE ASC") As List(Of ClassificationDTO)

        Try
            Dim query = From p In Context.PE_CLASSIFICATION
                        Select New ClassificationDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .VALUE_FROM = p.VALUE_FROM,
                            .VALUE_TO = p.VALUE_TO,
                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try

    End Function

    Public Function GetStatisticKPISeniority(ByVal _periodId As Integer, ByVal log As UserLog) As DataTable
        Try
            Dim result As DataTable

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_PERIOD_ID = _periodId,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_DASHBOARD.GET_STATISTIC_KPI_SENIORITY", obj)

                result = dtData
            End Using
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPerformance")
            Throw ex
        End Try
    End Function

    'Dao nguoc table
    'Private Function ReverseRowsInDataTable(ByVal inputTable As DataTable) As DataTable
    '    Dim outputTable As DataTable = inputTable.Clone()
    '    Dim drFiles As DataRow
    '    Dim dvFiles As DataView

    '    For i As Integer = inputTable.Rows.Count - 1 To 0 Step -1
    '        outputTable.Columns.Add(i & " năm", Type.GetType("System.String"))
    '    Next

    '    For Each filesInfo In outputTable.Rows
    '        drFiles = outputTable.NewRow()
    '        'drFiles("A") = filesInfo.Name
    '        'drFiles("B") = filesInfo.LastWriteTime
    '        'drFiles("C") = filesInfo.Name
    '        'drFiles("D") = filesInfo.LastWriteTime
    '        'drFiles("E") = filesInfo.Name
    '        For Each 
    '            outputTable.Rows.Add(drFiles)
    '        Next filesInfo



    '        Return outputTable
    'End Function



#End Region
End Class