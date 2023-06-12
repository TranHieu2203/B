Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness

        Public Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As System.Data.DataTable Implements IAttendanceBusiness.LOAD_PERIOD

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.LOAD_PERIOD(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Load_Emp_obj() As List(Of AT_PERIODDTO) Implements IAttendanceBusiness.Load_Emp_obj

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Load_Emp_obj()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As List(Of AT_PERIODDTO) Implements IAttendanceBusiness.LOAD_PERIODBylinq

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.LOAD_PERIODBylinq(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function LOAD_PERIODYEAR() As List(Of AT_PERIODDTO) Implements IAttendanceBusiness.LOAD_PERIODYEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.LOAD_PERIODYEAR()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Load_date(ByVal period_id As Decimal, ByVal emp_obj_id As Decimal) As AT_PERIODDTO Implements IAttendanceBusiness.Load_date

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Load_date(period_id, emp_obj_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As AT_PERIODDTO Implements IAttendanceBusiness.LOAD_PERIODByID

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.LOAD_PERIODByID(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminalFromOtOtherList() As System.Data.DataTable _
            Implements IAttendanceBusiness.GetTerminalFromOtOtherList

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTerminalFromOtOtherList()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminal(ByVal obj As AT_TERMINALSDTO, ByVal log As UserLog) As System.Data.DataTable _
            Implements IAttendanceBusiness.GetTerminal

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTerminal(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTerminalAuto() As System.Data.DataTable _
            Implements IAttendanceBusiness.GetTerminalAuto

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTerminalAuto()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateTerminalLastTime(ByVal obj As AT_TERMINALSDTO) As Boolean _
            Implements IAttendanceBusiness.UpdateTerminalLastTime

            Using rep As New AttendanceRepository
                Try
                    Return rep.UpdateTerminalLastTime(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateTerminalStatus(ByVal obj As AT_TERMINALSDTO) As Boolean _
            Implements IAttendanceBusiness.UpdateTerminalStatus

            Using rep As New AttendanceRepository
                Try
                    Return rep.UpdateTerminalStatus(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDataFromOrg(ByVal obj As ParamDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GetDataFromOrg

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDataFromOrg(obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalWSDataImport(ByVal obj As ParamDTO) As System.Data.DataSet Implements IAttendanceBusiness.GetPortalWSDataImport

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetPortalWSDataImport(obj)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CLOSEDOPEN_PERIOD(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CLOSEDOPEN_PERIOD

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CLOSEDOPEN_PERIOD(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IS_PERIOD_PAYSTATUS(ByVal param As ParamDTO, ByVal isAfter As Boolean,
                                            ByVal log As Framework.Data.UserLog) As Boolean _
                                        Implements IAttendanceBusiness.IS_PERIOD_PAYSTATUS


            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IS_PERIOD_PAYSTATUS(param, isAfter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IS_PERIODSTATUS(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.IS_PERIODSTATUS

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IS_PERIODSTATUS(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IS_PERIODSTATUS_BY_DATE(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.IS_PERIODSTATUS_BY_DATE

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IS_PERIODSTATUS_BY_DATE(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal),
                                       ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean Implements IAttendanceBusiness.CheckPeriodClose

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckPeriodClose(lstEmp, startdate, enddate, sAction)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace
