Imports Performance.PerformanceBusiness

Public Class PerformanceRepository
    Inherits PerformanceRepositoryBase


    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, Optional ByVal isBlank As Boolean = False) As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetOtherList(sType, sLang, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetPeriodList(ByVal isBlank As Boolean) As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPeriodList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetObjectGroupList(ByVal isBlank As Boolean) As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetObjectGroupByPeriodList(ByVal periodID As Decimal, ByVal isBlank As Boolean) As DataTable

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupByPeriodList(periodID, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As PerformanceCommonTABLE_NAME) As Boolean
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

End Class
