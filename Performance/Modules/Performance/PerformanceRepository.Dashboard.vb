Imports Performance.PerformanceBusiness

Partial Class PerformanceRepository
    Public Function GetStatisticKPIByClassification(ByVal _periodId As Integer) As List(Of StatisticDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetStatisticKPIByClassification(_periodId, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticKPIByClassifiOrg(ByVal _periodId As Integer) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetStatisticKPIByClassifiOrg(_periodId, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticClassification() As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstClassification = rep.GetStatisticClassification("CODE ASC")
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetStatisticKPISeniority(ByVal _periodId As Integer) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetStatisticKPISeniority(_periodId, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


End Class