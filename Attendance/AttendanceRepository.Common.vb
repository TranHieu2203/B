Imports Attendance.AttendanceBusiness

Partial Class AttendanceRepository
    Inherits AttendanceRepositoryBase

    Public Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO) As DataTable
        Dim dt As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.LOAD_PERIOD(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_Emp_obj() As List(Of AT_PERIODDTO)
        Dim dt As List(Of AT_PERIODDTO)
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Load_Emp_obj()
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO) As List(Of AT_PERIODDTO)
        Dim dt As List(Of AT_PERIODDTO)
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.LOAD_PERIODBylinq(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function LOAD_PERIODYEAR() As List(Of AT_PERIODDTO)
        Dim dt As List(Of AT_PERIODDTO)
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.LOAD_PERIODYEAR()
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_date(ByVal period_id As Decimal, ByVal emp_obj_id As Decimal) As AT_PERIODDTO
        Dim dt As AT_PERIODDTO
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Load_date(period_id, emp_obj_id)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO) As AT_PERIODDTO
        Dim dt As AT_PERIODDTO
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.LOAD_PERIODByID(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetTerminal(ByVal obj As AT_TERMINALSDTO) As DataTable
        Dim dt As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetTerminal(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetDataFromOrg(ByVal obj As PARAMDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetDataFromOrg(obj, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPortalWSDataImport(ByVal obj As ParamDTO) As DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetPortalWSDataImport(obj)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CLOSEDOPEN_PERIOD(ByVal param As PARAMDTO) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.CLOSEDOPEN_PERIOD(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IS_PERIODSTATUS(ByVal param As PARAMDTO) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.IS_PERIODSTATUS(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IS_PERIOD_PAYSTATUS(ByVal param As ParamDTO, Optional ByVal isAfter As Boolean = False) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.IS_PERIOD_PAYSTATUS(param, isAfter, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IS_PERIODSTATUS_BY_DATE(ByVal param As PARAMDTO) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.IS_PERIODSTATUS_BY_DATE(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal),
                                    ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean
        Try
            _isAvailable = False
            Using rep As New AttendanceBusinessClient
                Try
                    _isAvailable = rep.CheckPeriodClose(lstEmp, startdate, enddate, sAction)
                    Return _isAvailable
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            _isAvailable = True
        End Try
    End Function
End Class
