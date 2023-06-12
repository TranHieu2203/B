Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness

        Public Function GetStatisticTotalWorking(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.IAttendanceBusiness.GetStatisticTotalWorking
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetStatisticTotalWorking(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticTimeOff(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                           Implements ServiceContracts.IAttendanceBusiness.GetStatisticTimeOff
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetStatisticTimeOff(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticTimeOtByOrg(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.IAttendanceBusiness.GetStatisticTimeOtByOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetStatisticTimeOtByOrg(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticTimeProject(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO) _
                   Implements ServiceContracts.IAttendanceBusiness.GetStatisticTimeProject
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetStatisticTimeProject(_year, _month, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


    End Class

End Namespace
