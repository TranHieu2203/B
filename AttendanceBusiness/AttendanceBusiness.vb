Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Implements IAttendanceBusiness

#Region "Get data combobox"

        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetOtherList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetOtherList(sType, sLang, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectTitleList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectTitleList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectTitleList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProjectWorkList(ByVal isBlank As Boolean) As DataTable _
            Implements IAttendanceBusiness.GetProjectWorkList
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetProjectWorkList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function



        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO, Optional ByVal strUser As String = "ADMIN") As Boolean _
            Implements IAttendanceBusiness.GetComboboxData
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetComboboxData(cbxData, strUser)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function CAL_SUMMARY_DATA_INOUT(ByVal Period_id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CAL_SUMMARY_DATA_INOUT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CAL_SUMMARY_DATA_INOUT(Period_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function IMPORT_AT_SWIPE_DATA_V1(ByVal log As UserLog, ByVal DATA_IN As String, ByVal Machine_type As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.IMPORT_AT_SWIPE_DATA_V1
            Using rep As New AttendanceRepository
                Try
                    Return rep.IMPORT_AT_SWIPE_DATA_V1(log, DATA_IN, Machine_type)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "STORE PROCEDURE"
        Public Function GET_MANUAL_BY_ID(ByVal id As Decimal) As DataTable Implements IAttendanceBusiness.GET_MANUAL_BY_ID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_MANUAL_BY_ID(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_INFO_PHEPNAM(ByVal id As Decimal, ByVal fromDate As Date) As DataTable Implements IAttendanceBusiness.GET_INFO_PHEPNAM
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_INFO_PHEPNAM(id, fromDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_INFO_NGHIBU(ByVal id As Decimal, ByVal fromDate As Date) As DataTable Implements IAttendanceBusiness.GET_INFO_NGHIBU
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_INFO_NGHIBU(id, fromDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckOverMonth(ByVal employeeId As Decimal, ByVal orgId As Decimal _
                                                                , ByVal FromDate As DateTime,
                                                                 ByVal ToDate As DateTime) As Decimal Implements IAttendanceBusiness.CheckOverMonth
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckOverMonth(employeeId, orgId, FromDate, ToDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrgIdLevel2(EmpId As Decimal) As Decimal Implements IAttendanceBusiness.GetOrgIdLevel2
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetOrgIdLevel2(EmpId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Lấy thông tin process approve
        ''' </summary>
        ''' <param name="EmpId"></param>
        ''' <param name="LeaveSheetId"></param>
        ''' <returns></returns>
        Public Function GetProcessApprovedStatusByEmpAndId(EmpId As Decimal, LeaveSheetId As Decimal) As PROCESS_APPROVED_STATUS Implements IAttendanceBusiness.GetProcessApprovedStatusByEmpAndId
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetProcessApprovedStatusByEmpAndId(EmpId, LeaveSheetId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Lấy thông tin đơn xin nghỉ việc theo mã nhân viên và số đơn nghỉ việc
        ''' </summary>
        ''' <param name="EmpId"></param>
        ''' <param name="LeaveSheetId"></param>
        ''' <returns></returns>
        Public Function GetLeaveSheetByEmpAndLeave(EmpId As Decimal, LeaveSheetId As Decimal) As AT_LEAVESHEET Implements IAttendanceBusiness.GetLeaveSheetByEmpAndLeave
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveSheet(EmpId, LeaveSheetId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_INFO_PHEPNAM_IMPORT_CTT(ByVal id As Decimal, ByVal fromDate As Date) As DataTable Implements IAttendanceBusiness.GET_INFO_PHEPNAM_IMPORT_CTT
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_INFO_PHEPNAM_IMPORT_CTT(id, fromDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
