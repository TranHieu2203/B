Imports Payroll.PayrollBusiness

Partial Class PayrollRepository
    Inherits PayrollRepositoryBase
#Region "Dashboard Payroll"
    Public Function GetStatisticSalary(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticSalary(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticSalaryRange(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticSalaryRange(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticSalaryIncome(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticSalaryIncome(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticSalaryAverage(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticSalaryAverage(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Dashboard Panning"
    Public Function GetStatisticSalaryTracker(ByVal _year As Integer) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticSalaryTracker(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticEmployeeTracker(ByVal _year As Integer) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetStatisticEmployeeTracker(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function




#End Region

End Class
