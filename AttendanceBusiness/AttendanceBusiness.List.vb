Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Public Function CHECK_CONTRACT(ByVal employee_id As Decimal) As DataTable _
              Implements ServiceContracts.IAttendanceBusiness.CHECK_CONTRACT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_CONTRACT(employee_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_TYPE_BREAK(ByVal type_break_id As Decimal) As DataTable _
             Implements ServiceContracts.IAttendanceBusiness.CHECK_TYPE_BREAK
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_TYPE_BREAK(type_break_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_PERIOD_CLOSE1(ByVal periodid As Integer, ByVal EmpId As Integer) As Integer _
              Implements ServiceContracts.IAttendanceBusiness.CHECK_PERIOD_CLOSE1
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_PERIOD_CLOSE1(periodid, EmpId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function
        Public Function CHECK_PERIOD_CLOSE(ByVal periodid As Integer) As Integer _
              Implements ServiceContracts.IAttendanceBusiness.CHECK_PERIOD_CLOSE
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_PERIOD_CLOSE(periodid)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function
        Public Function PRS_COUNT_SHIFT(ByVal employee_id As Decimal) As DataTable _
             Implements ServiceContracts.IAttendanceBusiness.PRS_COUNT_SHIFT
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_COUNT_SHIFT(employee_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetperiodID(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As Decimal _
             Implements ServiceContracts.IAttendanceBusiness.GetperiodID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetperiodID(employee_Id, fromDate, toDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetperiodID_2(ByVal employee_Id As Decimal, ByVal RegDate As Date) As Decimal _
             Implements ServiceContracts.IAttendanceBusiness.GetperiodID_2
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetperiodID_2(employee_Id, RegDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetperiodByEmpObj(ByVal objEmp As Decimal, ByVal _dateGet As Date) As AT_PERIODDTO _
             Implements ServiceContracts.IAttendanceBusiness.GetperiodByEmpObj
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetperiodByEmpObj(objEmp, _dateGet)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRS_COUNT_INOUTKH(ByVal employee_id As Decimal, ByVal year As Decimal) As DataTable _
              Implements ServiceContracts.IAttendanceBusiness.PRS_COUNT_INOUTKH
            Using rep As New AttendanceRepository
                Try

                    Return rep.PRS_COUNT_INOUTKH(employee_id, year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_AT_SETUPELEAVE_YEAR(ByVal year As Decimal) As DataTable _
              Implements ServiceContracts.IAttendanceBusiness.GET_AT_SETUPELEAVE_YEAR
            Using rep As New AttendanceRepository
                Try

                    Return rep.GET_AT_SETUPELEAVE_YEAR(year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SUM_AT_ADVANCELEAVE_EMP_YEAR(ByVal empId As Decimal, ByVal year As Decimal, ByVal ID As Decimal) As DataTable _
              Implements ServiceContracts.IAttendanceBusiness.SUM_AT_ADVANCELEAVE_EMP_YEAR
            Using rep As New AttendanceRepository
                Try

                    Return rep.SUM_AT_ADVANCELEAVE_EMP_YEAR(empId, year, ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32 _
              Implements ServiceContracts.IAttendanceBusiness.PRI_PROCESS
            Using rep As New AttendanceRepository
                Try

                    Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_INSERT_AT_SWIPE_DATA(ByVal id As Decimal) As Int32 _
              Implements ServiceContracts.IAttendanceBusiness.UPDATE_INSERT_AT_SWIPE_DATA
            Using rep As New AttendanceRepository
                Try
                    Return rep.UPDATE_INSERT_AT_SWIPE_DATA(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_TIME_TIMESHEET_EMP(ByVal id As Decimal, ByVal log As UserLog) As Int32 _
              Implements ServiceContracts.IAttendanceBusiness.CAL_TIME_TIMESHEET_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.CAL_TIME_TIMESHEET_EMP(id, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_REGIMES(ByVal ID As Decimal, ByVal MANUAL_ID As Decimal) As Int32 _
              Implements ServiceContracts.IAttendanceBusiness.INSERT_REGIMES
            Using rep As New AttendanceRepository
                Try

                    Return rep.INSERT_REGIMES(ID, MANUAL_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLeaveInOutKH(ByVal employee_Id As Decimal) As List(Of LEAVEINOUTKHDTO) _
             Implements ServiceContracts.IAttendanceBusiness.GetLeaveInOutKH
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLeaveInOutKH(employee_Id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function PRS_GETLEAVE_BY_APPROVE1(ByVal param As AT_PORTAL_REG_DTO,
                                                Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IAttendanceBusiness.PRS_GETLEAVE_BY_APPROVE1
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_GETLEAVE_BY_APPROVE1(param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function PRS_GETDMVS_BY_APPROVE(ByVal param As AT_PORTAL_REG_DTO,
                                                Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IAttendanceBusiness.PRS_GETDMVS_BY_APPROVE
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_GETDMVS_BY_APPROVE(param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLeaveRegistrationListByLM(ByVal _filter As AT_PORTAL_REG_DTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO) _
                                  Implements ServiceContracts.IAttendanceBusiness.GetLeaveRegistrationListByLM
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLeaveRegistrationListByLM(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "CONFIG TEMPLATE"
        Function GET_CONFIG_TEMPLATE(ByVal MACHINE_TYPE As Decimal?) As DataSet Implements ServiceContracts.IAttendanceBusiness.GET_CONFIG_TEMPLATE
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_CONFIG_TEMPLATE(MACHINE_TYPE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
        Function getSetUpAttEmp(ByVal _filter As SetUpCodeAttDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SetUpCodeAttDTO) Implements ServiceContracts.IAttendanceBusiness.getSetUpAttEmp
            Using rep As New AttendanceRepository
                Try
                    Return rep.getSetUpAttEmp(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertSetUpAttEmp(ByVal objValue As SetUpCodeAttDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertSetUpAttEmp
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertSetUpAttEmp(objValue, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifySetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifySetUpAttEmp
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifySetUpAttEmp(objValue, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteSetUpAttEmp(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteSetUpAttEmp
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteSetUpAttEmp(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckValidateMACC(ByVal obj As SetUpCodeAttDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckValidateMACC
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckValidateMACC(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckValidateAPPROVE_DATE(ByVal obj As SetUpCodeAttDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckValidateAPPROVE_DATE
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckValidateAPPROVE_DATE(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "wifi-gps"
        Public Function GetSetupWifi(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO) Implements ServiceContracts.IAttendanceBusiness.GetSetupWifi
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetSetupWifi(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSetupGPS(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO) Implements ServiceContracts.IAttendanceBusiness.GetSetupGPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetSetupGPS(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSetupGPSByID(ByVal _filter As AT_SETUP_WIFI_GPS_DTO) As AT_SETUP_WIFI_GPS_DTO Implements ServiceContracts.IAttendanceBusiness.GetSetupGPSByID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetSetupGPSByID(_filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertSetupWifi
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertSetupWifi(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertSetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertSetupGPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertSetupGPS(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateWIFI_GPS(ByVal org_id As Decimal, ByVal id As Decimal, ByVal flag As String) As Decimal Implements ServiceContracts.IAttendanceBusiness.ValidateWIFI_GPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateWIFI_GPS(org_id, id, flag)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifySetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifySetupWifi
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifySetupWifi(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifySetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifySetupGPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifySetupGPS(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveSetupWifi(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveSetupWifi
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveSetupWifi(lstID, log, bActive)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveSetupGPS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveSetupGPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveSetupGPS(lstID, log, bActive)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteSetupWifi(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteSetupWifi
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteSetupWifi(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteSetupGPS(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteSetupGPS
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteSetupGPS(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "HOLIDAY"

        Public Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO) Implements ServiceContracts.IAttendanceBusiness.GetHoliday
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetHoliday(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDayHoliday() As List(Of AT_HOLIDAYDTO) Implements ServiceContracts.IAttendanceBusiness.GetDayHoliday
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetDayHoliday()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function GetHoliday_Hose(ByVal _filter As AT_HOLIDAYDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO) Implements ServiceContracts.IAttendanceBusiness.GetHoliday_Hose
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetHoliday_Hose(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertHoliday(objHOLIDAY, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertHoliday_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertHOLIDAY_Hose
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertHoliday_Hose(objHOLIDAY, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateHoliday(objHOLIDAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateHOLIDAY_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateHOLIDAY_Hose
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateHoliday_Hose(objHOLIDAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyHoliday(objHOLIDAY, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveHoliday
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveHoliday(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveHoliday_Hose(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveHoliday_Hose
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveHoliday_Hose(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteHOLIDAY(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteHOLIDAY
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteHoliday(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteHOLIDAY_Hose(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteHOLIDAY_Hose
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteHoliday_Hose(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Holiday gerenal"

        Public Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO) Implements ServiceContracts.IAttendanceBusiness.GetHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetHolidayGerenal(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertHolidayGerenal(objHOLIDAYGR, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateHolidayGerenal(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyHoliday(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyHolidayGerenal(objHOLIDAYGR, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveHolidayGerenal(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteHolidayGerenal
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteHolidayGerenal(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region " AT_FML Danh mục kiểu công"
        Public Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO) Implements ServiceContracts.IAttendanceBusiness.GetSignByPage
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetSignByPage(pagecode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_FML(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_FML(ByVal objAT_FML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_FML(objAT_FML, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_FML(ByVal objAT_FML As AT_FMLDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_FML(objAT_FML)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_FML(ByVal objAT_FML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_FML(objAT_FML, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_FML(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_FML
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_FML(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục ca"
        Public Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_GSIGN(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_GSIGN(objGSFND, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_GSIGN(objGSFND)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_GSIGN(ByVal objGSFND As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_GSIGN(objGSFND, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_GSIGN(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_GSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_GSIGN(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quy định đi muộn về sớm"
        Public Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_DMVS(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_DMVS(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_DMVS(ByVal objData As AT_DMVSDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_DMVS(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_DMVS(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_DMVS(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_DMVS
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_DMVS(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục ca làm việc"
        Public Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SHIFT(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SHIFT(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SHIFT(ByVal objData As AT_SHIFTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SHIFT(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_ORG_SHIFT(ByVal _ID As Decimal, ByVal _ORGID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_ORG_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_ORG_SHIFT(_ID, _ORGID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SHIFT(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SHIFT(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SHIFT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SHIFT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TIME_MANUALBINCOMBO() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUALBINCOMBO
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUALBINCOMBO()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateCombobox
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateCombobox(cbxData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetAT_SHIFT_ORG_ACCESS_By_ID(ByVal _AT_SHIFT_ID As System.Decimal) As List(Of Decimal) Implements ServiceContracts.IAttendanceBusiness.GetAT_SHIFT_ORG_ACCESS_By_ID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SHIFT_ORG_ACCESS_By_ID(_AT_SHIFT_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Thiết lập số ngày nghỉ theo đối tượng"
        Public Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Holiday_Object(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_Holiday_Object(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_Holiday_Object(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_Holiday_Object(ByVal objData As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_Holiday_Object(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_Holiday_Object(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_Holiday_Object
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_Holiday_Object(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo cấp nhân sự"
        Public Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SETUP_SPECIAL(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SETUP_SPECIAL(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SETUP_SPECIAL(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SETUP_SPECIAL(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SETUP_SPECIAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SETUP_SPECIAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SETUP_SPECIAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Thiết lập đối tượng chấm công theo nhân viên"
        Public Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SETUP_TIME_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertAT_SETUP_TIME_EMP(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_TIME_EMPDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.ValidateAT_SETUP_TIME_EMP(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyAT_SETUP_TIME_EMP(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SETUP_TIME_EMP(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SETUP_TIME_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteAT_SETUP_TIME_EMP(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Thiết lập thang quy đổi"
        Public Function GetAT_SetUp_Exchange(ByVal _filter As AT_SETUP_EXCHANGEDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_SETUP_EXCHANGEDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SetUp_Exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SetUp_Exchange(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SetUp_Exchange(ByVal objData As AT_SETUP_EXCHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SetUp_Exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SetUp_Exchange(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SetUp_Exchange(ByVal objData As AT_SETUP_EXCHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SetUp_Exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SetUp_Exchange(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SetUp_Exchange(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SetUp_Exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SetUp_Exchange(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SetUp_Exchange(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SetUp_Exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SetUp_Exchange(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckTrung_AT__SetUp_exchange(ByVal id As Decimal, ByVal from_minute As Decimal,
                                                      ByVal to_minute As Decimal,
                                                       ByVal EFFECT_DATE As Date,
                                                  ByVal OBJECT_ATTENDACE As Decimal,
                                                  ByVal TYPE_EXCHANGE As Decimal,
                                                  ByVal ORG_ID As Decimal) As Integer Implements ServiceContracts.IAttendanceBusiness.CheckTrung_AT__SetUp_exchange
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckTrung_AT__SetUp_exchange(id, from_minute, to_minute, EFFECT_DATE, OBJECT_ATTENDACE, TYPE_EXCHANGE, ORG_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Thiết lập máy chấm công"
        Public Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL_STATUS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_TERMINAL(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_TERMINAL(ByVal objData As AT_TERMINALSDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_TERMINAL(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_TERMINAL(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_TERMINAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_TERMINAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_TERMINAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Đăng ký chấm công mặc định"
        Public Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SIGNDEFAULT(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_YEARLEAVE_EDIT(ByVal _filter As AT_YEAR_LEAVE_EDITDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE ASC",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_YEAR_LEAVE_EDITDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_YEARLEAVE_EDIT(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_ListShift() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_ListShift
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_ListShift()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_PERIOD() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetAT_PERIOD
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PERIOD()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeID(ByVal employee_code As String, ByVal end_date As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetEmployeeID(employee_code, end_date)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeIDExits(ByVal employee_code As String) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeIDExits
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetEmployeeIDExits(employee_code)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeIDInSign
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetEmployeeIDInSign(employee_code)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetEmployeeByTimeID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeByTimeID(time_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function InsertAT_YEARLEAVE_EDIT(ByVal objSIGN As AT_YEAR_LEAVE_EDITDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_YEARLEAVE_EDIT(objSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_YEARLEAVE_EDIT(ByVal objSIGN As AT_YEAR_LEAVE_EDITDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_YEARLEAVE_EDIT(objSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteExistAT_YEARLEAVE_EDIT(ByVal EmployeeID As Decimal, ByVal Year As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteExistAT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteExistAT_YEARLEAVE_EDIT(EmployeeID, Year, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_AT_YEARLEAVE_EDIT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.IMPORT_AT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IMPORT_AT_YEARLEAVE_EDIT(P_DOCXML, P_USER)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function InsertAT_SIGNDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_SIGNDEFAULT(objSIGN, log, gID, param)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SIGNDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_SIGNDEFAULT(objSIGN, log, gID, param)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_SINGDEFAULT(ByVal objSIGN As AT_SIGNDEFAULTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_SIGNDEFAULT(objSIGN)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_SIGNDEFAULT(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_SIGNDEFAULT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_YEARLEAVE_EDIT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_YEARLEAVE_EDIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_YEARLEAVE_EDIT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function checkCopySignDefault(ByVal _empID As Decimal, ByVal _year As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.checkCopySignDefault
            Using rep As New AttendanceRepository
                Try

                    Return rep.checkCopySignDefault(_empID, _year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function COPY_SIGN_DEFAULT(ByVal P_ID_COPY As Decimal, ByVal P_YEAR As Decimal, ByVal P_EMP_ID As Decimal, ByVal P_USERNAME As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.COPY_SIGN_DEFAULT
            Using rep As New AttendanceRepository
                Try

                    Return rep.COPY_SIGN_DEFAULT(P_ID_COPY, P_YEAR, P_EMP_ID, P_USERNAME)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Đăng ký ca mặc định cho phòng ban"
        Public Function GetAT_SIGNDEFAULT_ORG(ByVal _filter As AT_SIGNDEFAULT_ORGDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULT_ORGDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_SIGNDEFAULT_ORG(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO,
                                        ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertAT_SIGNDEFAULT_ORG(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO,
                                       ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyAT_SIGNDEFAULT_ORG(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateAT_SIGNDEFAULT_ORG(ByVal _validate As AT_SIGNDEFAULT_ORGDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try
                    Return rep.ValidateAT_SIGNDEFAULT_ORG(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try
                    Return rep.ActiveAT_SIGNDEFAULT_ORG(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_SIGNDEFAULT_ORG
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteAT_SIGNDEFAULT_ORG(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Dang ky OT tren iPortal"
        Public Function GET_REG_PORTAL(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date, _
                                        ByVal strId As String, ByVal type As String) As List(Of APPOINTMENT_DTO) _
                                        Implements IAttendanceBusiness.GET_REG_PORTAL
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_REG_PORTAL(empid, startdate, enddate, strId, type)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_TOTAL_OT_APPROVE(ByVal empid As Decimal, ByVal enddate As Date) As Decimal _
                                        Implements IAttendanceBusiness.GET_TOTAL_OT_APPROVE
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_TOTAL_OT_APPROVE(empid, enddate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32 _
                                        Implements IAttendanceBusiness.AT_CHECK_ORG_PERIOD_STATUS_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.AT_CHECK_ORG_PERIOD_STATUS_OT(LISTORG, PERIOD)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_LIST_HOURS() As DataTable _
                                        Implements IAttendanceBusiness.GET_LIST_HOURS
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_LIST_HOURS()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_LIST_MINUTE() As DataTable _
                                        Implements IAttendanceBusiness.GET_LIST_MINUTE
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_LIST_MINUTE()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32 _
                                        Implements IAttendanceBusiness.PRI_PROCESS_APP
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_TIMESHEET_PAYSLIP(ByVal employee_id As Decimal, ByVal tag As String) As String _
                                        Implements IAttendanceBusiness.GET_TIMESHEET_PAYSLIP
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_TIMESHEET_PAYSLIP(employee_id, tag)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PROCESS_APP_CANCEL(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal id_reggroup As Integer, ByVal token As String, ByVal template_id As Integer) As Int32 _
                                                Implements IAttendanceBusiness.PRI_PROCESS_APP_CANCEL
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRI_PROCESS_APP_CANCEL(employee_id_app, employee_id, period_id, process_type, id_reggroup, token, template_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_SEQ_PORTAL_RGT() As Decimal _
                                        Implements IAttendanceBusiness.GET_SEQ_PORTAL_RGT
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_SEQ_PORTAL_RGT()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_ORGID(ByVal EMPID As Integer) As Int32 _
                                        Implements IAttendanceBusiness.GET_ORGID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_ORGID(EMPID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32 _
                                        Implements IAttendanceBusiness.GET_PERIOD
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_PERIOD(DATE_CURRENT)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function AT_CHECK_EMPLOYEE(ByVal EMPID As Decimal, ByVal ENDDATE As Date) As Int32 _
                                        Implements IAttendanceBusiness.AT_CHECK_EMPLOYEE
            Using rep As New AttendanceRepository
                Try
                    Return rep.AT_CHECK_EMPLOYEE(EMPID, ENDDATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_TOTAL_OT_APPROVE3(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal _
                                        Implements IAttendanceBusiness.GET_TOTAL_OT_APPROVE3
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_TOTAL_OT_APPROVE3(EMPID, ENDDATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_RGT_OT(ByVal EMPID As Decimal, ByVal STARTDATE As Date, ByVal ENDDATE As Date, _
                                 ByVal FROM_HOUR As String, ByVal TO_HOUR As String, ByVal HOUR_RGT As Decimal) As Int32 _
                                        Implements IAttendanceBusiness.CHECK_RGT_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_RGT_OT(EMPID, STARTDATE, ENDDATE, FROM_HOUR, TO_HOUR, HOUR_RGT)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "đăng ký nghỉ trên iportal"
        Public Function GetHolidayByCalenderToTable(ByVal startdate As Date, ByVal enddate As Date) As DataTable _
                                         Implements IAttendanceBusiness.GetHolidayByCalenderToTable
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetHolidayByCalenderToTable(startdate, enddate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal,
                                                  ByVal startdate As DateTime,
                                                  ByVal enddate As DateTime, _
                                                  ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO) _
                                              Implements IAttendanceBusiness.GetPlanningAppointmentByEmployee
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetPlanningAppointmentByEmployee(empid, startdate, enddate, listSign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO,
                                             ByVal log As UserLog) As Boolean _
                                         Implements IAttendanceBusiness.InsertPortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertPortalRegister(itemRegister, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPortalRegList(ByVal obj As AT_PORTAL_REG_DTO, ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean _
                   Implements ServiceContracts.IAttendanceBusiness.ModifyPortalRegList
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyPortalRegList(obj, itemRegister, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHolidayByCalender(ByVal startdate As Date,
                                             ByVal enddate As Date) As List(Of Date) _
                                         Implements IAttendanceBusiness.GetHolidayByCalender
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetHolidayByCalender(startdate, enddate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal,
                                                                 ByVal startdate As Date,
                                                                 ByVal enddate As Date,
                                                                ByVal listSign As List(Of AT_TIME_MANUALDTO),
                                                                ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO) _
                                                            Implements IAttendanceBusiness.GetRegisterAppointmentInPortalByEmployee
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetRegisterAppointmentInPortalByEmployee(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRegisterAppointmentInPortalOT(ByVal empid As Decimal,
                                                                 ByVal startdate As Date,
                                                                 ByVal enddate As Date,
                                                                ByVal listSign As List(Of OT_OTHERLIST_DTO),
                                                                ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO) _
                                                            Implements IAttendanceBusiness.GetRegisterAppointmentInPortalOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetRegisterAppointmentInPortalOT(empid, startdate, enddate, listSign, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal _
                                                          Implements IAttendanceBusiness.GetTotalLeaveInYear
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetTotalLeaveInYear(empid, p_year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO),
                                                   ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean _
                                               Implements IAttendanceBusiness.DeletePortalRegisterByDate
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalRegisterByDate(listappointment, listSign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalRegisterByDateOT(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO),
                                                  ByVal listSign As List(Of OT_OTHERLIST_DTO)) As Boolean _
                                              Implements IAttendanceBusiness.DeletePortalRegisterByDateOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalRegisterByDateOT(listappointment, listSign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalRegister(ByVal id As Decimal) As Boolean _
            Implements IAttendanceBusiness.DeletePortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalRegister(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal),
                                              ByVal process As String, ByVal currentUrl As String) As String _
                                          Implements IAttendanceBusiness.SendRegisterToApprove
            Using rep As New AttendanceRepository
                Try
                    Return rep.SendRegisterToApprove(objLstRegisterId, process, currentUrl)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Phê duyệt đăng ký nghỉ trên iportal"
        Public Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO) _
            Implements IAttendanceBusiness.GetListSignCode
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListSignCode(gSignCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetListWaitingForApprove(ByVal approveId As Decimal,
                                          ByVal process As String,
                                          ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO) _
                                      Implements IAttendanceBusiness.GetListWaitingForApprove
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListWaitingForApprove(approveId, process, filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetListWaitingForApproveOT(ByVal approveId As Decimal,
                                          ByVal process As String,
                                          ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO) _
                                      Implements IAttendanceBusiness.GetListWaitingForApproveOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetListWaitingForApproveOT(approveId, process, filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function ApprovePortalRegister(ByVal regID As Decimal?,
                                       ByVal approveId As Decimal,
                                       ByVal status As Integer,
                                       ByVal note As String,
                                       ByVal currentUrl As String,
                                       ByVal process As String,
                                       ByVal log As UserLog) As Boolean _
                                   Implements IAttendanceBusiness.ApprovePortalRegister
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApprovePortalRegister(regID, approveId, status, note, currentUrl, process, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetEmployeeList() As DataTable _
            Implements IAttendanceBusiness.GetEmployeeList
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetLeaveDay(ByVal dDate As Date) As DataTable _
            Implements IAttendanceBusiness.GetLeaveDay
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveDay(dDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Thiết lập kiểu công"

        Public Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUAL(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO Implements ServiceContracts.IAttendanceBusiness.GetAT_TIME_MANUALById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TIME_MANUALById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_TIME_MANUAL(objHOLIDAY, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_TIME_MANUAL(objHOLIDAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_TIME_MANUAL(objHOLIDAY, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_TIME_MANUAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_TIME_MANUAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_TIME_MANUAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDataImportCO() As DataTable Implements ServiceContracts.IAttendanceBusiness.GetDataImportCO
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetDataImportCO()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDataImportCO1() As DataSet Implements ServiceContracts.IAttendanceBusiness.GetDataImportCO1
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetDataImportCO1()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Danh mục tham số hệ thống"

        Public Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO) Implements ServiceContracts.IAttendanceBusiness.GetListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetListParamItime(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertListParamItime(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateListParamItime(objData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyListParamItime(ByVal objData As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyListParamItime(objData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveListParamItime(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteListParamItime
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteListParamItime(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region ""
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String _
           Implements ServiceContracts.IAttendanceBusiness.AutoGenCode
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.AutoGenCode(firstChar, tableName, colName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.CheckExistInDatabase
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckExistInDatabase(lstID, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean _
           Implements ServiceContracts.IAttendanceBusiness.CheckExistInDatabaseAT_SIGNDEFAULT
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.CheckExistInDatabaseAT_SIGNDEFAULT(lstID, lstWorking, lstShift, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region


#Region "AT_PROJECT_TITLE"
        Public Function GetAT_PROJECT_TITLE(ByVal _filter As AT_PROJECT_TITLEDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_TITLEDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_PROJECT_TITLE
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PROJECT_TITLE(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_PROJECT_TITLE(ByVal objAT_PROJECT_TITLE As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_PROJECT_TITLE
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_PROJECT_TITLE(objAT_PROJECT_TITLE, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_PROJECT_TITLE(ByVal objAT_PROJECT_TITLE As AT_PROJECT_TITLEDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_PROJECT_TITLE
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_PROJECT_TITLE(objAT_PROJECT_TITLE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_PROJECT_TITLE(ByVal objAT_PROJECT_TITLE As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_PROJECT_TITLE
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_PROJECT_TITLE(objAT_PROJECT_TITLE, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_PROJECT_TITLE(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_PROJECT_TITLE
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_PROJECT_TITLE(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_PROJECT"
        Public Function GetAT_PROJECT(ByVal _filter As AT_PROJECTDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECTDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_PROJECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PROJECT(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_PROJECT(ByVal objAT_PROJECT As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_PROJECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_PROJECT(objAT_PROJECT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_PROJECT(ByVal objAT_PROJECT As AT_PROJECTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_PROJECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_PROJECT(objAT_PROJECT)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_PROJECT(ByVal objAT_PROJECT As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_PROJECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_PROJECT(objAT_PROJECT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_PROJECT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_PROJECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_PROJECT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateOtherList(ByVal objOtherList As OT_OTHERLIST_DTO) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.ValidateOtherList
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateOtherList(objOtherList)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT_PROJECT_WORK"
        Public Function GetAT_PROJECT_WORK(ByVal _filter As AT_PROJECT_WORKDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_WORKDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_PROJECT_WORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PROJECT_WORK(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_PROJECT_WORK(ByVal objAT_PROJECT_WORK As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_PROJECT_WORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_PROJECT_WORK(objAT_PROJECT_WORK, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_PROJECT_WORK(ByVal objAT_PROJECT_WORK As AT_PROJECT_WORKDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_PROJECT_WORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_PROJECT_WORK(objAT_PROJECT_WORK)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_PROJECT_WORK(ByVal objAT_PROJECT_WORK As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_PROJECT_WORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_PROJECT_WORK(objAT_PROJECT_WORK, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_PROJECT_WORK(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_PROJECT_WORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_PROJECT_WORK(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_PROJECT_EMP"
        Public Function GetAT_PROJECT_EMP(ByVal _filter As AT_PROJECT_EMPDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_PROJECT_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_PROJECT_EMP(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_PROJECT_EMP(ByVal objAT_PROJECT_EMP As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_PROJECT_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_PROJECT_EMP(objAT_PROJECT_EMP, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_PROJECT_EMP(ByVal objAT_PROJECT_EMP As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_PROJECT_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_PROJECT_EMP(objAT_PROJECT_EMP, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_PROJECT_EMP(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_PROJECT_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_PROJECT_EMP(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Function GET_PE_ASSESS_MESS(ByVal EMP As Decimal?) As DataTable _
            Implements IAttendanceBusiness.GET_PE_ASSESS_MESS
            Using rep As New AttendanceRepository
                Try
                    Return rep.GET_PE_ASSESS_MESS(EMP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function PRS_DASHBOARD_BY_APPROVE(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_PROCESS_TYPE As String) As DataTable _
            Implements IAttendanceBusiness.PRS_DASHBOARD_BY_APPROVE
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_DASHBOARD_BY_APPROVE(P_EMPLOYEE_APP_ID, P_PROCESS_TYPE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "cham cong"
        Public Function GetLeaveRegistrationList(ByVal _filter As AT_PORTAL_REG_DTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO) _
                                     Implements ServiceContracts.IAttendanceBusiness.GetLeaveRegistrationList
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLeaveRegistrationList(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeletePortalReg(ByVal lstId As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.DeletePortalReg
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeletePortalReg(lstId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApprovePortalRegList(ByVal obj As List(Of AT_PORTAL_REG_LIST_DTO), ByVal log As UserLog) As Boolean _
           Implements ServiceContracts.IAttendanceBusiness.ApprovePortalRegList
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApprovePortalRegList(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "cham cong newedit"
        Public Function GetEmployeeInfor(ByVal P_EmpId As Decimal?, ByVal P_Org_ID As Decimal?, Optional ByVal fromDate As Date? = Nothing) As DataTable _
          Implements ServiceContracts.IAttendanceBusiness.GetEmployeeInfor
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeInfor(P_EmpId, P_Org_ID, fromDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLeaveRegistrationById(ByVal _filter As AT_PORTAL_REG_DTO) As AT_PORTAL_REG_DTO Implements ServiceContracts.IAttendanceBusiness.GetLeaveRegistrationById
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveRegistrationById(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLeaveEmpDetail(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, Optional ByVal isUpdate As Boolean = False) As List(Of LEAVE_DETAIL_EMP_DTO) _
            Implements ServiceContracts.IAttendanceBusiness.GetLeaveEmpDetail
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveEmpDetail(employee_Id, fromDate, toDate, isUpdate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLeaveRegistrationDetailById(ByVal listId As Decimal) As List(Of AT_PORTAL_REG_DTO) Implements ServiceContracts.IAttendanceBusiness.GetLeaveRegistrationDetailById
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveRegistrationDetailById(listId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GETIDFROMPROCESS(ByVal Id As Decimal) As Decimal _
              Implements ServiceContracts.IAttendanceBusiness.GETIDFROMPROCESS
            Using rep As New AttendanceRepository
                Try
                    Return rep.GETIDFROMPROCESS(Id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertPortalRegList(ByVal obj As AT_PORTAL_REG_LIST_DTO, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByVal log As UserLog, ByRef gID As Decimal, ByRef itemExist As AT_PORTAL_REG_DTO, ByRef isOverAnnualLeave As Boolean) As Boolean _
           Implements ServiceContracts.IAttendanceBusiness.InsertPortalRegList
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertPortalRegList(obj, lstObjDetail, log, gID, itemExist, isOverAnnualLeave)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Thiết lập hệ số OT ngoại lệ "
        Public Function GetAT_Coeff_OT_Exception(ByVal _filter As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIME_EXCEPTIONDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_Coeff_OT_Exception
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Coeff_OT_Exception(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_Coeff_OT_Exception
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_Coeff_OT_Exception(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.UpdateAT_Coeff_OT_Exception
            Using rep As New AttendanceRepository
                Try

                    Return rep.UpdateAT_Coeff_OT_Exception(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_Coeff_OT_Exception
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_Coeff_OT_Exception(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveOrDeActive_AT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveOrDeActive_AT_Coeff_OT_Exception
            Using rep As New AttendanceRepository
                Try
                    Return rep.ActiveOrDeActive_AT_Coeff_OT_Exception(lstID, log, bActive)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Thiết lập kỳ công"

        Public Function GetAT_Time_Period(ByVal _filter As AT_TIME_PERIODDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_PERIODDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_Time_Period
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Time_Period(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertATTimePeriod
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertATTimePeriod(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyATTimePeriod
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyATTimePeriod(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteATTimePeriod(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteATTimePeriod
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteATTimePeriod(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckATTimePeriod(ByVal pdate As Date, ByVal objEmp As Decimal, ByVal id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckATTimePeriod
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckATTimePeriod(pdate, objEmp, id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_AT_TIMEWORK(ByVal P_ID As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.GET_AT_TIMEWORK
            Using rep As New AttendanceRepository
                Try

                    Return rep.GET_AT_TIMEWORK(P_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_AT_TIMEWORK_EMPLOYEE(ByVal P_ID As Decimal, ByVal P_HE_SO As Decimal?) As DataTable Implements ServiceContracts.IAttendanceBusiness.GET_AT_TIMEWORK_EMPLOYEE
            Using rep As New AttendanceRepository
                Try

                    Return rep.GET_AT_TIMEWORK_EMPLOYEE(P_ID, P_HE_SO)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_AT_TIMEWORKSTANDARD(ByVal P_YEAR As Decimal,
                                            ByVal P_OBJ_EMP As Decimal,
                                            ByVal P_NOT_T7 As Decimal,
                                            ByVal P_NOT_CN As Decimal,
                                            ByVal P_NOT_T7_2 As Decimal,
                                            ByVal P_NOT_2T7 As Decimal,
                                            ByVal P_TC As Decimal?,
                                            ByVal P_CMD As Decimal?,
                                            ByVal P_HE_SO As Decimal?) As DataTable Implements ServiceContracts.IAttendanceBusiness.GET_AT_TIMEWORKSTANDARD
            Using rep As New AttendanceRepository
                Try

                    Return rep.GET_AT_TIMEWORKSTANDARD(P_YEAR, P_OBJ_EMP, P_NOT_T7, P_NOT_CN, P_NOT_T7_2, P_NOT_2T7, P_TC, P_CMD, P_HE_SO)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function GetAT_Time_WorkStandard(ByVal _filter As AT_TIME_WORKSTANDARDDTO,
                                                ByVal _param As ParamDTO,
                                                Optional ByVal PageIndex As Integer = 0,
                                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                                Optional ByRef Total As Integer = 0,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_WORKSTANDARDDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_Time_WorkStandard
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Time_WorkStandard(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertATTime_WorkStandard
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertATTime_WorkStandard(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyATTime_WorkStandard
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyATTime_WorkStandard(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteATTime_WorkStandard(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteATTime_WorkStandard
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteATTime_WorkStandard(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckATTime_WorkStandard(ByVal year As Decimal, ByVal orgId As Decimal, ByVal objEmp As Decimal, ByVal id As Decimal, ByVal objAttendance As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckATTime_WorkStandard
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckATTime_WorkStandard(year, orgId, objEmp, id, objAttendance)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckATTime_WorkStandardEmp(ByVal year As Decimal, ByVal enpID As Decimal, ByVal id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckATTime_WorkStandardEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckATTime_WorkStandardEmp(year, enpID, id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_Time_WorkStandardID(ByVal id As Decimal) As AT_TIME_WORKSTANDARDDTO Implements ServiceContracts.IAttendanceBusiness.GetAT_Time_WorkStandardID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_Time_WorkStandardID(id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckCloseOrg(ByVal year As Decimal, ByVal month As Decimal, ByVal orgId As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckCloseOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckCloseOrg(year, month, orgId)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_COEFF_OVERTIME(ByVal _filter As AT_COEFF_OVERTIMEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIMEDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_COEFF_OVERTIME
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_COEFF_OVERTIME(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_COEFF_OVERTIME
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_COEFF_OVERTIME(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_COEFF_OVERTIME
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_COEFF_OVERTIME(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_COEFF_OVERTIME(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_COEFF_OVERTIME
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_COEFF_OVERTIME(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAT_COEFF_OVERTIME(ByVal pDate As Date, ByVal id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAT_COEFF_OVERTIME
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAT_COEFF_OVERTIME(pDate, id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region


#Region "List ANNUALLEAVE"

        Public Function GetAnnualLeave(ByVal _filter As AT_ANNUAL_LEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVEDTO) Implements ServiceContracts.IAttendanceBusiness.GetAnnualLeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAnnualLeave(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAnnualLeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAnnualLeave(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAnnualLeave_DATE(ByVal obj As AT_ANNUAL_LEAVEDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAnnualLeave_DATE
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAnnualLeave_DATE(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAnnualLeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAnnualLeave(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAnnualLeave(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAnnualLeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAnnualLeave(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region


#Region "List ANNUALLEAVE ORG"

        Public Function GetAnnualLeaveOrg(ByVal _filter As AT_ANNUAL_LEAVE_ORGDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVE_ORGDTO) Implements ServiceContracts.IAttendanceBusiness.GetAnnualLeaveOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAnnualLeaveOrg(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAnnualLeaveOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAnnualLeaveOrg(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAnnualLeaveOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAnnualLeaveOrg(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAnnualLeaveOrg_DATE(ByVal obj As AT_ANNUAL_LEAVE_ORGDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAnnualLeaveOrg_DATE
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAnnualLeaveOrg_DATE(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAnnualLeaveOrg(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAnnualLeaveOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAnnualLeaveOrg(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT SETUP ELEAVE"

        Public Function GetAtSetupELeave(ByVal _filter As AT_SETUP_ELEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_ELEAVEDTO) Implements ServiceContracts.IAttendanceBusiness.GetAtSetupELeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtSetupELeave(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAtSetupELeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAtSetupELeave(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAtSetupELeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAtSetupELeave(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtSetupELeave(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtSetupELeave
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtSetupELeave(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT SETUP ELEAVE"

        Public Function GetAtSeniority(ByVal _filter As AT_SENIORITYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SENIORITYDTO) Implements ServiceContracts.IAttendanceBusiness.GetAtSeniority
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtSeniority(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAtSeniority
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAtSeniority(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAtSeniority
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAtSeniority(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtSeniority(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtSeniority
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtSeniority(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAtSeniority_DATE(ByVal obj As AT_SENIORITYDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAtSeniority_DATE
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAtSeniority_DATE(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "List TOXIC LEAVE EMP"

        Public Function GetAtToxicLeaveEmp(ByVal _filter As AT_TOXIC_LEAVE_EMPDTO,
                                           ByVal log As UserLog,
                                             Optional ByVal PageIndex As Integer = 0,
                                             Optional ByVal PageSize As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TOXIC_LEAVE_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.GetAtToxicLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtToxicLeaveEmp(_filter, log, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAtToxicLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAtToxicLeaveEmp(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAtToxicLeaveEmp_DATE(ByVal obj As AT_TOXIC_LEAVE_EMPDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAtToxicLeaveEmp_DATE
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAtToxicLeaveEmp_DATE(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAtToxicLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAtToxicLeaveEmp(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtToxicLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtToxicLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtToxicLeaveEmp(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "List ADVANCE LEAVE EMP"

        Public Function GetAtAdvanceLeaveEmp(ByVal _filter As AT_ADVANCE_LEAVE_EMPDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ADVANCE_LEAVE_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.GetAtAdvanceLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtAdvanceLeaveEmp(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAtAdvanceLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAtAdvanceLeaveEmp(objTitle, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckAtAdvanceLeaveEmp_DATE(ByVal obj As AT_ADVANCE_LEAVE_EMPDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckAtAdvanceLeaveEmp_DATE
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckAtAdvanceLeaveEmp_DATE(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAtAdvanceLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAtAdvanceLeaveEmp(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtAdvanceLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtAdvanceLeaveEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtAdvanceLeaveEmp(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Phê duyệt giải trình công portal"
        Public Function Get_At_Late_Combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LATE_COMBACKOUTDTO) _
            Implements ServiceContracts.IAttendanceBusiness.Get_At_Late_Combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_At_Late_Combackout(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function PRS_Get_TREvaluateCourse_Nofi(ByVal param As AT_PORTAL_REG_DTO,
                                                        Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IAttendanceBusiness.PRS_Get_TREvaluateCourse_Nofi
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_Get_TREvaluateCourse_Nofi(param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRS_API_GETNOTIFICATION(ByVal param As SE_NOTIFICATIONDTO,
                                               Optional ByVal UserID As Integer = 0,
                                                Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IAttendanceBusiness.PRS_API_GETNOTIFICATION
            Using rep As New AttendanceRepository
                Try
                    Return rep.PRS_API_GETNOTIFICATION(param, UserID, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "Danh mục Lý do giải trình công"
        Public Function Get_AT_REASON_LIST(ByVal _filter As AT_REASON_LIST_DTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_REASON_LIST_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_REASON_LIST
            Using rep As New AttendanceRepository
                Try
                    Return rep.Get_AT_REASON_LIST(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_REASON_LIST
            Using rep As New AttendanceRepository
                Try
                    Return rep.Insert_AT_REASON_LIST(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Modify_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_REASON_LIST
            Using rep As New AttendanceRepository
                Try
                    Return rep.Modify_AT_REASON_LIST(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Active_AT_REASON_LIST(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal status As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Active_AT_REASON_LIST
            Using rep As New AttendanceRepository
                Try
                    Return rep.Active_AT_REASON_LIST(lstID, log, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_REASON_LIST(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_REASON_LIST
            Using rep As New AttendanceRepository
                Try
                    Return rep.Delete_AT_REASON_LIST(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace
