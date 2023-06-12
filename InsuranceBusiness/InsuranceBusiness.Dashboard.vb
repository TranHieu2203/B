Imports Framework.Data
Imports InsuranceDAL

Namespace InsuranceBusiness.ServiceImplementations
    Partial Public Class InsuranceBusiness

        Public Function GetStatisticInsPayFund(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.IInsuranceBusiness.GetStatisticInsPayFund
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatisticInsPayFund(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticInsPayFundDetail(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                         Implements ServiceContracts.IInsuranceBusiness.GetStatisticInsPayFundDetail
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatisticInsPayFundDetail(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticInsRate(ByVal _locationId As Integer) As List(Of StatisticInsRateDTO) _
                         Implements ServiceContracts.IInsuranceBusiness.GetStatisticInsRate
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatisticInsRate(_locationId)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticInsCeiling(ByVal _locationId As Integer) As List(Of StatisticInsCeilingDTO) _
                         Implements ServiceContracts.IInsuranceBusiness.GetStatisticInsCeiling
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetStatisticInsCeiling(_locationId)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace
