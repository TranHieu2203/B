Imports PerformanceBusiness.ServiceContracts
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Public Class PerformanceBusiness
        Implements IPerformanceBusiness

        Public Function TestService(ByVal str As String) As String Implements ServiceContracts.IPerformanceBusiness.TestService
            Return "Hello world " & str
        End Function

        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IPerformanceBusiness.GetOtherList
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetOtherList(sType, sLang, isBlank)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetPeriodList(ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IPerformanceBusiness.GetPeriodList
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriodList(isBlank)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetObjectGroupList(ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IPerformanceBusiness.GetObjectGroupList
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroupList(isBlank)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetObjectGroupByPeriodList(ByVal periodID As Decimal, ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IPerformanceBusiness.GetObjectGroupByPeriodList
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroupByPeriodList(periodID, isBlank)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetCriteriaByObjectList(ByVal objectID As Decimal, ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IPerformanceBusiness.GetCriteriaByObjectList
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaByObjectList(objectID, isBlank)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As PerformanceCommon.TABLE_NAME) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CheckExistInDatabase
            Try
                Using rep As New PerformanceRepository
                    Return rep.CheckExistInDatabase(lstID, table)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

    End Class
End Namespace
