Imports Framework.Data
Imports PayrollDAL


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness

#Region "Dashborad Payroll"
        Public Function GetStatisticSalary(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.IPayrollBusiness.GetStatisticSalary
            Using rep As New PayrollRepository
                Try

                    Return rep.GetStatisticSalary(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetStatisticSalaryRange(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.IPayrollBusiness.GetStatisticSalaryRange
            Using rep As New PayrollRepository
                Try

                    Return rep.GetStatisticSalaryRange(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetStatisticSalaryIncome(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.IPayrollBusiness.GetStatisticSalaryIncome
            Using rep As New PayrollRepository
                Try

                    Return rep.GetStatisticSalaryIncome(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetStatisticSalaryAverage(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.IPayrollBusiness.GetStatisticSalaryAverage
            Using rep As New PayrollRepository
                Try

                    Return rep.GetStatisticSalaryAverage(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Dashborad Planning"
        Public Function GetStatisticSalaryTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet _
                            Implements ServiceContracts.IPayrollBusiness.GetStatisticSalaryTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetStatisticSalaryTracker(_year, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetStatisticEmployeeTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet _
                    Implements ServiceContracts.IPayrollBusiness.GetStatisticEmployeeTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetStatisticEmployeeTracker(_year, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function



#End Region

    End Class

End Namespace
