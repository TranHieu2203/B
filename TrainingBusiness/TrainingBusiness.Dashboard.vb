Imports Framework.Data
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Public Class TrainingBusiness

        Public Function GetStatisticCourse(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.ITrainingBusiness.GetStatisticCourse
            Try
                Return TrainingRepositoryStatic.Instance.GetStatisticCourse(_year, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetStatisticFormCost(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.ITrainingBusiness.GetStatisticFormCost
            Try
                Return TrainingRepositoryStatic.Instance.GetStatisticFormCost(_year, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetStatisticDiligence(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.ITrainingBusiness.GetStatisticDiligence
            Try
                Return TrainingRepositoryStatic.Instance.GetStatisticDiligence(_year, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetStatisticRank(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.ITrainingBusiness.GetStatisticRank
            Try
                Return TrainingRepositoryStatic.Instance.GetStatisticRank(_year, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


    End Class

End Namespace
