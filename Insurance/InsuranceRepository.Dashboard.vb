Imports Insurance.InsuranceBusiness

Partial Class InsuranceRepository
    Inherits InsuranceRepositoryBase

    Public Function GetStatisticInsPayFund(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetStatisticInsPayFund(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticInsPayFundDetail(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetStatisticInsPayFundDetail(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticInsRate(Optional ByVal _locationId As Integer = 0) As List(Of StatisticInsRateDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetStatisticInsRate(_locationId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticInsCeiling(Optional ByVal _locationId As Integer = 0) As List(Of StatisticInsCeilingDTO)
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetStatisticInsCeiling(_locationId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

End Class
