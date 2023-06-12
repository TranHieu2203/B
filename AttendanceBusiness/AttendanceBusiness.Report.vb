Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Public Function GET_REPORT() As System.Data.DataTable Implements IAttendanceBusiness.GET_REPORT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_REPORT()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       log As UserLog,
                                       Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IAttendanceBusiness.GetReportById
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT001(ByVal obj As PARAMDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_AT001

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT001(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT002(ByVal obj As PARAMDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_AT002

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT002(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT003(ByVal obj As PARAMDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_AT003

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT003(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT004(ByVal obj As ParamDTO, ByVal log As UserLog) As System.Data.DataSet _
            Implements IAttendanceBusiness.GET_AT004

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT004(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT005(ByVal obj As PARAMDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_AT005

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT005(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_AT006(ByVal obj As ParamDTO, ByVal log As UserLog) As System.Data.DataSet _
            Implements IAttendanceBusiness.GET_AT006

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT006(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_AT007(ByVal obj As ParamDTO, ByVal log As UserLog) As System.Data.DataSet _
            Implements IAttendanceBusiness.GET_AT007

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT007(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT008(ByVal obj As ParamDTO, ByVal P_DATE As Date, ByVal log As UserLog) As System.Data.DataSet _
           Implements IAttendanceBusiness.GET_AT008

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT008(obj, P_DATE, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT009(ByVal obj As ParamDTO, ByVal P_FROMDATE As Date, ByVal P_TODATE As Date, ByVal log As UserLog) As DataSet _
         Implements IAttendanceBusiness.GET_AT009

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT009(obj, P_FROMDATE, P_TODATE, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT010(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet _
            Implements IAttendanceBusiness.GET_AT010

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT010(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT011(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet _
            Implements IAttendanceBusiness.GET_AT011

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT011(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace
