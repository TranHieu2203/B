Imports Attendance.AttendanceBusiness

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Public Function GET_REPORT() As DataTable
        Dim dt As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_REPORT()
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Dim lstTitle As List(Of Se_ReportDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTitle = rep.GetReportById(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GET_AT001(ByVal obj As PARAMDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT001(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT002(ByVal obj As PARAMDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT002(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT003(ByVal obj As PARAMDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT003(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_AT004(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT004(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT005(ByVal obj As PARAMDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT005(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_AT006(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT006(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_AT007(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT007(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT008(ByVal obj As ParamDTO, ByVal P_DATE As Date) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT008(obj, P_DATE, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT009(ByVal obj As ParamDTO, ByVal P_FROMDATE As Date, ByVal P_TODATE As Date) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT009(obj, P_FROMDATE, P_TODATE, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT010(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT010(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_AT011(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_AT011(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    '
End Class
