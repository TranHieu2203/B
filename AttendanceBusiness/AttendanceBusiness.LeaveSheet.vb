
Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        'Implements IAttendanceBusiness
        Public Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Boolean Implements IAttendanceBusiness.ValidateLeaveSheetDetail
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.ValidateLeaveSheetDetail(objValidate)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet Implements IAttendanceBusiness.GetLeaveSheet_ById
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_ById(Leave_SheetID, Struct)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_EXPIREDATE_P_BU(ByVal EMP_ID As Decimal, ByVal Fromdate As Date) As DataTable Implements IAttendanceBusiness.GET_EXPIREDATE_P_BU
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GET_EXPIREDATE_P_BU(EMP_ID, Fromdate)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable Implements IAttendanceBusiness.GetLeaveSheet_Detail_ByDate
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_Detail_ByDate(employee_id, fromDate, toDate, manualId)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function Validate_LeaveSheet(ByVal _validate As AT_LEAVESHEETDTO) As VALIDATE_DTO Implements IAttendanceBusiness.Validate_LeaveSheet
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.Validate_LeaveSheet(_validate)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.SaveLeaveSheet
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.SaveLeaveSheet(dsLeaveSheet, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function SaveLeaveSheet_Portal(ByVal LeaveSheet As AT_LEAVESHEETDTO, ByVal dsLeaveSheet As List(Of AT_LEAVESHEETDTO), ByRef gID As Decimal, Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.SaveLeaveSheet_Portal
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.SaveLeaveSheet_Portal(LeaveSheet, dsLeaveSheet, gID, log)
                    Catch ex As Exception
                        Return False
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO) Implements IAttendanceBusiness.GetLeaveSheet_Portal
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveSheet_Portal(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetLeaveCTSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO) Implements IAttendanceBusiness.GetLeaveCTSheet_Portal
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.GetLeaveCTSheet_Portal(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class

End Namespace
