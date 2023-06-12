Imports Framework.Data
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "Dashboard"
        Public Function GetStatisticKPIByClassification(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As List(Of StatisticDTO) Implements ServiceContracts.IPerformanceBusiness.GetStatisticKPIByClassification
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetStatisticKPIByClassification(_periodId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStatisticKPIByClassifiOrg(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IPerformanceBusiness.GetStatisticKPIByClassifiOrg
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetStatisticKPIByClassifiOrg(_periodId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStatisticClassification(Optional ByVal Sorts As String = "CODE ASC") As List(Of ClassificationDTO) Implements ServiceContracts.IPerformanceBusiness.GetStatisticClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetStatisticClassification(Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetStatisticKPISeniority(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IPerformanceBusiness.GetStatisticKPISeniority
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetStatisticKPISeniority(_periodId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


    End Class
End Namespace
