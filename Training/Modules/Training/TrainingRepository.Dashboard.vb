Imports Training.TrainingBusiness

Partial Class TrainingRepository
    Inherits TrainingRepositoryBase
    Public Function GetStatisticCourse(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetStatisticCourse(_year, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticFormCost(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetStatisticFormCost(_year, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticDiligence(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetStatisticDiligence(_year, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticRank(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetStatisticRank(_year, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function



End Class
