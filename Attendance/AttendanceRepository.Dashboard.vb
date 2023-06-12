Imports Attendance.AttendanceBusiness

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Public Function GetStatisticTotalWorking(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetStatisticTotalWorking(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticTimeOff(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetStatisticTimeOff(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticTimeOtByOrg(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetStatisticTimeOtByOrg(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetStatisticTimeProject(ByVal _year As Integer, ByVal _month As Integer) As List(Of StatisticDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetStatisticTimeProject(_year, _month, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function



End Class
