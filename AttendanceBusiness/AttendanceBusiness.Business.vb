Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace AttendanceBusiness.ServiceImplementations
    Partial Public Class AttendanceBusiness
        Function ReadCheckInOutData(ByVal dateFrom As Date, ByVal dateTo As Date, Optional ByVal terId As Decimal = -1) As Boolean Implements IAttendanceBusiness.ReadCheckInOutData
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.ReadCheckInOutData(dateFrom, dateTo, terId)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function ReadCheckInOutData_CheckOUT(ByVal _lst_Terminal As AT_TERMINALSDTO, ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean Implements IAttendanceBusiness.ReadCheckInOutData_CheckOUT
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.ReadCheckInOutData_CheckOUT(_lst_Terminal, dateFrom, dateTo)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function Upd_TimeTImesheetMachines(ByVal LstObj As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.Upd_TimeTImesheetMachines
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.Upd_TimeTImesheetMachines(LstObj, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function CheckExistAT_LATE_COMBACKOUT(ByVal objImport As AT_TIME_TIMESHEET_MACHINETDTO) As Boolean Implements IAttendanceBusiness.CheckExistAT_LATE_COMBACKOUT
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.CheckExistAT_LATE_COMBACKOUT(objImport)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
        Function IMPORT_TIMESHEET_MACHINE(ByVal ListobjImport As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.IMPORT_TIMESHEET_MACHINE
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.IMPORT_TIMESHEET_MACHINE(ListobjImport, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function
#Region "Quan ly vao ra"
        Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO) Implements IAttendanceBusiness.GetDataInout

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDataInout(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                        ByVal log As UserLog) As Boolean _
                                    Implements ServiceContracts.IAttendanceBusiness.InsertDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDataInout(lstDataInout, fromDate, toDate, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDataInout(objDataInout, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDataInout
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDataInout(lstDataInout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "lam bu"
        Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT_NB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALCULATE_ENTITLEMENT_NB(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO) Implements IAttendanceBusiness.GetNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "PHEP NAM"
        Public Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                 ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO) Implements IAttendanceBusiness.GetEntitlement

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEntitlement(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALCULATE_ENTITLEMENT(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CALCULATE_ENTITLEMENT_HOSE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CALCULATE_ENTITLEMENT_HOSE

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CALL_ENTITLEMENT_HOSE(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function AT_ENTITLEMENT_PREV_HAVE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.AT_ENTITLEMENT_PREV_HAVE

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.AT_ENTITLEMENT_PREV_HAVE(param, listEmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckPeriodMonth(ByVal year As Integer, ByVal PeriodId As Integer, ByRef PeriodNext As Integer) As Boolean Implements IAttendanceBusiness.CheckPeriodMonth

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckPeriodMonth(year, PeriodId, PeriodNext)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportEntitlementLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_PERIOD As Decimal) As Boolean Implements IAttendanceBusiness.ImportEntitlementLeave

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportEntitlementLeave(P_DOCXML, P_USER, P_PERIOD)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_OT(ByVal P_DOCXML As String, ByVal P_USERNAME As String, Optional ByVal P_LOG As UserLog = Nothing) As Boolean Implements IAttendanceBusiness.IMPORT_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IMPORT_OT(P_DOCXML, P_USERNAME, P_LOG)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "XEP CA"

        Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_WORKSIGN

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_WORKSIGN(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) Implements ServiceContracts.IAttendanceBusiness.InsertWorkSign
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertWorkSign(objWorkSigns, objWork, p_fromdate, p_endDate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertWORKSIGNByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertWORKSIGNByImport
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertWorkSignByImport(dtData, period_id, emp_obj_id, start_date, end_date, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateWorkSign(objWORKSIGN)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyWorkSign(objWORKSIGN, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteWORKSIGN(ByVal lstWORKSIGN() As AT_WORKSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteWORKSIGN
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteWorkSign(lstWORKSIGN)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataTable Implements IAttendanceBusiness.GETSIGNDEFAULT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GETSIGNDEFAULT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Del_WorkSign_ByEmp(ByVal employee_id As String, ByVal p_From As Date, ByVal p_to As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.Del_WorkSign_ByEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.Del_WorkSign_ByEmp(employee_id, p_From, p_to)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Modify_WorkSign_ByEmp(ByVal employee_id As Decimal,
                                          ByVal p_From As Date,
                                          ByVal p_to As Date,
                                          ByVal p_period As Decimal,
                                          ByVal obj As List(Of AT_WORKSIGNEDITDTO),
                                          ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_WorkSign_ByEmp
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_WorkSign_ByEmp(employee_id, p_From, p_to, p_period, obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Get_Date(ByVal employee_id As Decimal,
                             ByRef join_date As Date,
                             ByRef ter_effect_date As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.Get_Date
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_Date(employee_id, join_date, ter_effect_date)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_PORTAL_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal p_type As String, ByVal P_IS_EXPORT As Decimal, ByVal log As UserLog) As DataSet Implements IAttendanceBusiness.GET_PORTAL_WORKSIGN

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_PORTAL_WORKSIGN(param, p_type, P_IS_EXPORT, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListWSWaitingApprove(ByVal param As AT_WORKSIGNDTO, ByVal P_IS_EXPORT As Decimal) As DataSet Implements IAttendanceBusiness.GetListWSWaitingApprove

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetListWSWaitingApprove(param, P_IS_EXPORT)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckNotSendPortalWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean Implements IAttendanceBusiness.CheckNotSendPortalWS

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckNotSendPortalWS(_empID, _period_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckWaittingApprovePTWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean Implements IAttendanceBusiness.CheckWaittingApprovePTWS

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckWaittingApprovePTWS(_empID, _period_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeletePortalWS(ByVal _lstIDRegGroup As List(Of Decimal)) As Boolean Implements IAttendanceBusiness.DeletePortalWS

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.DeletePortalWS(_lstIDRegGroup)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportPortalWS(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.ImportPortalWS

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportPortalWS(dtData, period_id, emp_obj_id, start_date, end_date, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "XEP CA"

        Public Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_ProjectAssign

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_ProjectAssign(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal) Implements ServiceContracts.IAttendanceBusiness.InsertProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertProjectAssign(objProjectAssigns, objWork, p_fromdate, p_endDate, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyProjectAssign(objProjectAssign, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteProjectAssign
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteProjectAssign(lstProjectAssign)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "May cham cong"
        Function IMPORT_AT_SWIPE_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean Implements IAttendanceBusiness.IMPORT_AT_SWIPE_DATA
            Try
                Using rep As New AttendanceRepository
                    Try
                        Dim isSusses = rep.IMPORT_AT_SWIPE_DATA(log, DATA_IN)
                        Return isSusses
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function GET_SWIPE_DATA_IMPORT(ByVal _orgid As Decimal, ByVal _is_dissolove As Boolean, ByVal log As UserLog) As DataSet Implements IAttendanceBusiness.GET_SWIPE_DATA_IMPORT
            Try
                Using rep As New AttendanceRepository
                    Try
                        Dim isSusses = rep.GET_SWIPE_DATA_IMPORT(_orgid, _is_dissolove, log)
                        Return isSusses
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GET_AT_WORKSIGN_EDIT(ByVal empId As Decimal, ByVal startDate As Date, ByVal EndDate As Date) As DataTable Implements IAttendanceBusiness.GET_AT_WORKSIGN_EDIT
            Try
                Using rep As New AttendanceRepository
                    Try
                        Dim isSusses = rep.GET_AT_WORKSIGN_EDIT(empId, startDate, EndDate)
                        Return isSusses
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GET_AT_SHIFT() As DataTable Implements IAttendanceBusiness.GET_AT_SHIFT
            Try
                Using rep As New AttendanceRepository
                    Try
                        Dim isSusses = rep.GET_AT_SHIFT()
                        Return isSusses
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetTerminalData(ByVal lstCode As List(Of String), ByVal username As String, ByVal orgID As Decimal) As List(Of OT_OTHERLIST_DTO) Implements IAttendanceBusiness.GetTerminalData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTerminalData(lstCode, username, orgID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                              Optional ByVal PageIndex As Integer = 0,
                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                              Optional ByRef Total As Integer = 0,
                              Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO) Implements IAttendanceBusiness.GetSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSwipeData(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO), ByVal machine As AT_TERMINALSDTO, ByVal P_FROMDATE As Date?, ByVal P_ENDDATE As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertSwipeData(objSwipeData, machine, P_FROMDATE, P_ENDDATE, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ImportSwipeDataAuto(ByVal lstSwipeData As List(Of AT_SWIPE_DATADTO)) As Boolean _
            Implements IAttendanceBusiness.ImportSwipeDataAuto

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportSwipeDataAuto(lstSwipeData)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function InsertSwipeDataImport(ByVal objDelareRice As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertSwipeDataImport

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertSwipeDataImport(objDelareRice, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.ImportSwipeData

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ImportSwipeData(dtData, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function IMPORT_INOUT(ByVal docXML As String, ByVal fromDate As DateTime, ByVal toDate As DateTime, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.IMPORT_INOUT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IMPORT_INOUT(docXML, fromDate, toDate, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Di Muon ve som"
        Function GetDSVM_List_Emp(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO) Implements IAttendanceBusiness.GetDSVM_List_Emp

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDSVM_List_Emp(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetLate_combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO) Implements IAttendanceBusiness.GetDSVM

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDSVM(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetChildren(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "WORKINGDAY desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO) Implements IAttendanceBusiness.GetChildren

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetChildren(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ApproveLate_CombackoutPortal(ByVal lstObj As List(Of AT_LATE_COMBACKOUTDTO)) As Integer Implements ServiceContracts.IAttendanceBusiness.ApproveLate_CombackoutPortal
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApproveLate_CombackoutPortal(lstObj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.IAttendanceBusiness.InsertLate_CombackoutPortal
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertLate_CombackoutPortal(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.IAttendanceBusiness.ModifyLate_CombackoutPortal
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyLate_CombackoutPortal(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLate_CombackoutByIdPortalNew(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO Implements ServiceContracts.IAttendanceBusiness.GetLate_CombackoutByIdPortalNew
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLate_CombackoutByIdPortalNew(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLate_CombackoutByIdPortal(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO Implements ServiceContracts.IAttendanceBusiness.GetLate_CombackoutByIdPortal
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLate_CombackoutByIdPortal(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO Implements ServiceContracts.IAttendanceBusiness.GetLate_CombackoutById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLate_CombackoutById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETCHILDREN_TAKECAREBYID(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO Implements ServiceContracts.IAttendanceBusiness.GETCHILDREN_TAKECAREBYID
            Using rep As New AttendanceRepository
                Try

                    Return rep.GETCHILDREN_TAKECAREBYID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef gID_Swipe As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportLate_combackout(objLate_combackout, log, gID, gID_Swipe)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertLate_combackout(objRegisterDMVSList, objLate_combackout, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertChildren(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertChildren
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertChildren(objRegisterDMVSList, objLate_combackout, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyChildren(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyChildren
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyChildren(objLate_combackout, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateLate_combackout(objLate_combackout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyLate_combackout(objLate_combackout, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLate_combackoutPortal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLate_combackoutPortal
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLate_combackoutPortal(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLate_combackout
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLate_combackout(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDMVS_Portal(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO) Implements ServiceContracts.IAttendanceBusiness.GetDMVS_Portal
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetDMVS_Portal(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeletePortalLate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeletePortalLate
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeletePortalLate(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalEmpMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO) Implements ServiceContracts.IAttendanceBusiness.GetPortalEmpMachines
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPortalEmpMachines(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalMachinesByID(ByVal _id As Decimal) As AT_TIME_TIMESHEET_MACHINETDTO Implements ServiceContracts.IAttendanceBusiness.GetPortalMachinesByID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetPortalMachinesByID(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Dang ly lam them"
        Function GetRegisterOT(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO) Implements IAttendanceBusiness.GetRegisterOT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetRegisterOT(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO Implements ServiceContracts.IAttendanceBusiness.GetRegisterById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetRegisterById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateRegisterOT(ByVal objLate_combackout As AT_REGISTER_OTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateRegisterOT(objLate_combackout)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertRegisterOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertRegisterOT(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDataRegisterOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertDataRegisterOT(objRegisterOTList, objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyRegisterOT(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteRegisterOT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckImporAddNewtOT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckImporAddNewtOT(objRegisterOT)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckDataListImportAddNew
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckDataListImportAddNew(objRegisterOTList, objRegisterOT, strEmployeeCode)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO),
                                          ByVal status_id As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ApproveRegisterOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ApproveRegisterOT(lstData, status_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable _
                         Implements ServiceContracts.IAttendanceBusiness.GetRegData
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetRegData(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Bang cham cong may"
        Public Function GetMachinesPortal(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO) Implements IAttendanceBusiness.GetMachinesPortal

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetMachinesPortal(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO) Implements IAttendanceBusiness.GetMachines

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetMachines(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_fromdate As Date, ByVal p_enddate As Date, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_delAll As Decimal, ByVal codecase As String) As Boolean Implements IAttendanceBusiness.Init_TimeTImesheetMachines

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Init_TimeTImesheetMachines(_param, log, p_fromdate, p_enddate, P_ORG_ID, lstEmployee, p_delAll, codecase)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveMachines(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveMachines
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveMachines(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTimesheetMachinet(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteTimesheetMachinet
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteTimesheetMachinet(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Bang cham cong tay"
        Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO,
                               ByVal log As Framework.Data.UserLog) As System.Data.DataSet _
                           Implements IAttendanceBusiness.GetCCT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetCCT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO,
                                      ByVal log As Framework.Data.UserLog) As DataTable _
            Implements IAttendanceBusiness.GetCCT_Origin

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetCCT_Origin(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetDaily

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetDaily(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal, ByVal EmpObj As Decimal) As DataTable Implements IAttendanceBusiness.InsertLeaveSheetDaily

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetDaily(dtData, log, PeriodID, EmpObj)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetDailyById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetDailyById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Bang cong lam them"

        Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_emp_obj As Decimal) As Boolean Implements IAttendanceBusiness.Cal_TimeTImesheet_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_OT(_param, log, p_period_id, P_ORG_ID, lstEmployee, p_emp_obj)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GetSummaryOT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryOT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO,
                                      ByVal log As UserLog,
                                      ByVal p_period_id As Decimal?,
                                      ByVal P_ORG_ID As Decimal,
                                      ByVal lstEmployee As List(Of Decimal?)) As Boolean _
                                  Implements IAttendanceBusiness.Cal_TimeTImesheet_NB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_NB(_param, log, p_period_id, P_ORG_ID, lstEmployee)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO,
                                     ByVal log As Framework.Data.UserLog) As System.Data.DataSet _
            Implements IAttendanceBusiness.GetSummaryNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryNB(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetOt

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetOt(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function InsertLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertLeaveSheetOt

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetOt(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetOtById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetOtById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Tổng hợp công"
        Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO) Implements IAttendanceBusiness.GetTimeSheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimeSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal param As ParamDTO, ByVal codecase As String, ByVal lstEmployee As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean Implements IAttendanceBusiness.CAL_TIME_TIMESHEET_MONTHLY

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CAL_TIME_TIMESHEET_MONTHLY(param, codecase, lstEmployee, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog) _
            Implements IAttendanceBusiness.ValidateTimesheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ValidateTimesheet(_validate, sType, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveTIME_TIMESHEET_MONTHLY(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveTIME_TIMESHEET_MONTHLY
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveTIME_TIMESHEET_MONTHLY(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTimesheetMonthly(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteTimesheetMonthly
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteTimesheetMonthly(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "Công tháng PORTAL"
        Function GetTimeSheetForEmp_Month(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO) Implements IAttendanceBusiness.GetTimeSheetForEmp_Month

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimeSheetForEmp_Month(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#End Region

#Region "Dang Ky công"
        Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO) Implements IAttendanceBusiness.GetLeaveSheet

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetLeaveSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO Implements ServiceContracts.IAttendanceBusiness.GetLeaveById
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetLeaveById(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalPHEPNAM
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTotalPHEPNAM(P_EMPLOYEE_ID, P_YEAR, P_TYPE_LEAVE_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                     ByVal P_YEAR As Integer,
                                     ByVal P_TYPE_LEAVE_ID As Integer) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalPHEPBU
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTotalPHEPBU(P_EMPLOYEE_ID, P_YEAR, P_TYPE_LEAVE_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetTotalDAY
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetTotalDAY(P_EMPLOYEE_ID, P_TYPE_MANUAL, P_FROM_DATE, P_TO_DATE)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                               ByVal P_FROM_DATE As Date,
                               ByVal P_TO_DATE As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetCAL_DAY_LEAVE_OLD
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetCAL_DAY_LEAVE_OLD(P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO Implements ServiceContracts.IAttendanceBusiness.GetPhepNam
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPhepNam(_id, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO) Implements ServiceContracts.IAttendanceBusiness.GetPHEPBUCONLAI
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPHEPBUCONLAI(lstEmpID, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO Implements ServiceContracts.IAttendanceBusiness.GetNghiBu
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetNghiBu(_id, _year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateLeaveSheet(ByVal objtime As AT_LEAVESHEETDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateLeaveSheet(objtime)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLeaveSheet
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertLeaveSheet(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLeaveSheetList(ByVal objRegisterOTList As List(Of AT_LEAVESHEETDTO), ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertLeaveSheetList
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertLeaveSheetList(objRegisterOTList, objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyLeaveSheet(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveApp(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal userName As String, ByVal type As String) As Decimal Implements ServiceContracts.IAttendanceBusiness.ApproveApp
            Using rep As New AttendanceRepository
                Try

                    Return rep.ApproveApp(lstID, status, userName, type)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLeaveSheet
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLeaveSheet(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteTimeSheetOT(ByVal lstID As List(Of AT_TIME_TIMESHEET_OTDTO)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteTimeSheetOT
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteTimeSheetOT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable Implements ServiceContracts.IAttendanceBusiness.checkLeaveImport
            Using rep As New AttendanceRepository
                Try

                    Return rep.checkLeaveImport(dtData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Khai bao cong com"
        Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO) Implements IAttendanceBusiness.GetDelareRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDelareRice(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO Implements ServiceContracts.IAttendanceBusiness.GetDelareRiceById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetDelareRiceById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveDelareRice(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateDelareRice(objDelareRice)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareRice(objDelareRice, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareRiceList
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareRiceList(objDelareRiceList, objDelareRice, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDelareRice(objDelareRice, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDelareRice
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDelareRice(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "quan ly cham cong bu tru"
        Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO) Implements IAttendanceBusiness.GetOffSettingTimeKeeping

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetOffSettingTimeKeeping(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO Implements ServiceContracts.IAttendanceBusiness.GetOffSettingTimeKeepingById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetOffSettingTimeKeepingById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeTimeKeepingID(ByVal ComId As Decimal) As List(Of AT_OFFFSETTING_EMPDTO) _
           Implements ServiceContracts.IAttendanceBusiness.GetEmployeeTimeKeepingID
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetEmployeeTimeKeepingID(ComId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Khai bao điều chỉnh thâm niên phép"
        Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO) Implements IAttendanceBusiness.GetDelareEntitlementNB

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetDelareEntitlementNB(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO Implements ServiceContracts.IAttendanceBusiness.GetDelareEntitlementNBById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetDelareEntitlementNBById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveDelareEntitlementNB(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertDelareEntitlementNB(objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertMultipleDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertMultipleDelareEntitlementNB(objDelareEntitlementlist, objDelareEntitlementNB, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportDelareEntitlementNB(dtData, log, gID, checkMonthNB, checkMonthNP)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyDelareEntitlementNB(objDelareEntitlementNB, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteDelareEntitlementNB
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteDelareEntitlementNB(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteOffTimeKeeping
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteOffTimeKeeping(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthThamNien(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthThamNien
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthThamNien(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthPhepNam(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthPhepNam
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthPhepNam(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateMonthNghiBu(ByVal objHOLIDAYGR As AT_DECLARE_ENTITLEMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateMonthNghiBu
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateMonthNghiBu(objHOLIDAYGR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Bang cong com"
        Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean Implements IAttendanceBusiness.Cal_TimeTImesheet_Rice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Cal_TimeTImesheet_Rice(_param, log, p_period_id, P_ORG_ID, lstEmployee)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GetSummaryRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetSummaryRice(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ModifyLeaveSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyLeaveSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.ApprovedTimeSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ApprovedTimeSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function InsertLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IAttendanceBusiness.InsertLeaveSheetRice

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.InsertLeaveSheetRice(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO Implements ServiceContracts.IAttendanceBusiness.GetTimeSheetRiceById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetTimeSheetRiceById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "IPORTAL - View phiếu lương"

        Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckPeriod
            Try
                Dim rep As New AttendanceRepository
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                   ByVal _param As ParamDTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO) Implements IAttendanceBusiness.GetTimeSheetPortal

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimeSheetPortal(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "LOG"
        Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO) Implements IAttendanceBusiness.GetActionLog

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetActionLog(_filter, Total, PageIndex, PageSize, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function DeleteActionLogsPA(ByVal lstDeleteIds As List(Of Decimal)) As Integer Implements IAttendanceBusiness.DeleteActionLogsAT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.DeleteActionLogsAT(lstDeleteIds)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Portal quan ly nghi phep, nghi bu"
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO Implements IAttendanceBusiness.GetTotalDayOff

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTotalDayOff(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO Implements IAttendanceBusiness.GET_TIME_MANUAL

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_TIME_MANUAL(_filter, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable Implements IAttendanceBusiness.GET_AT_PORTAL_REG

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT_PORTAL_REG(P_ID, P_EMPLOYEE, P_DATE_TIME)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable Implements IAttendanceBusiness.GET_AT_PORTAL_REG_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_AT_PORTAL_REG_OT(P_ID, P_EMPLOYEE, P_DATE_TIME)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                    Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO) Implements IAttendanceBusiness.GetHistoryLeave

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetHistoryLeave(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function




#End Region

#Region "Check nhân viên có thuộc dự án khi chấm công"
        Public Function CheckExistEm_Pro(ByVal lstID As Decimal, ByVal FromDate As Date, ByVal ToDate As Date, ByVal IDPro As Decimal) As Boolean
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CheckExistEm_Pro(lstID, FromDate, ToDate, IDPro)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "cham cong"
        Public Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean _
           Implements IAttendanceBusiness.CheckTimeSheetApproveVerify
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckTimeSheetApproveVerify(obj, type, itemError)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "quan ly cham cong bu tru"
        Public Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
          Implements IAttendanceBusiness.InsertOffSettingTime
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertOffSettingTime(objOffSetting, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements IAttendanceBusiness.ModifyOffSettingTime
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyOffSettingTime(objOffSetting, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "OT"
        Public Function CheckRegDateBetweenJoinAndTerDate(ByVal empId As Decimal, ByVal regDate As Date) As Boolean _
            Implements IAttendanceBusiness.CheckRegDateBetweenJoinAndTerDate
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckRegDateBetweenJoinAndTerDate(empId, regDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_LEAVE_ORG_PAUSE_OT(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date) As Integer _
            Implements IAttendanceBusiness.CHECK_LEAVE_ORG_PAUSE_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_LEAVE_ORG_PAUSE_OT(P_EMP_ID, P_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO) _
                    Implements IAttendanceBusiness.GetOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.GetOtRegistration(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                    Implements IAttendanceBusiness.InsertOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertOtRegistration(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                    Implements IAttendanceBusiness.ModifyotRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ModifyotRegistration(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SendApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Integer _
                    Implements IAttendanceBusiness.SendApproveOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.SendApproveOTRegistration(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal empId As Decimal, ByVal log As UserLog) As Boolean _
                    Implements IAttendanceBusiness.ApproveOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ApproveOtRegistration(obj, empId, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO) _
                    Implements IAttendanceBusiness.ValidateOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.ValidateOtRegistration(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Validate_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO Implements IAttendanceBusiness.Validate_orvertime

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Validate_orvertime(_validate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Api_register_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO Implements IAttendanceBusiness.Api_register_orvertime

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Api_register_orvertime(_validate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function API_APPROVEREGISTER_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO Implements IAttendanceBusiness.API_APPROVEREGISTER_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.API_APPROVEREGISTER_OT(_validate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function API__SEND_APPROVE_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO Implements IAttendanceBusiness.API__SEND_APPROVE_OT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.API__SEND_APPROVE_OT(_validate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean _
                    Implements IAttendanceBusiness.HRReviewOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.HRReviewOtRegistration(lst, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean _
                    Implements IAttendanceBusiness.DeleteOtRegistration
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteOtRegistration(lstId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalOtRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable _
                         Implements ServiceContracts.IAttendanceBusiness.GetPortalOtRegData
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPortalOtRegData(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalOtRegByAnotherData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable _
                         Implements ServiceContracts.IAttendanceBusiness.GetPortalOtRegByAnotherData
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPortalOtRegByAnotherData(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalOtApproveData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable _
                         Implements ServiceContracts.IAttendanceBusiness.GetPortalOtApproveData
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetPortalOtApproveData(_filter, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "SHIFT CYCLE"
        Public Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO) _
                    Implements IAttendanceBusiness.GetShiftCycle
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetShiftCycle(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO) Implements IAttendanceBusiness.GetEmployeeShifts

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEmployeeShifts(employee_Id, fromDate, toDate)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function IMPORT_AT_OT_REGISTRATION(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.IMPORT_AT_OT_REGISTRATION
            Using rep As New AttendanceRepository
                Try

                    Return rep.IMPORT_AT_OT_REGISTRATION(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOTCoeffOver(ByVal _regDate As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetOTCoeffOver
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetOTCoeffOver(_regDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function OtGetValueOfMonth(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal Implements IAttendanceBusiness.OtGetValueOfMonth

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.OtGetValueOfMonth(P_EMP_ID, P_DATE, P_HOURS, P_TYPE_OT)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOTReigistrationByID(ByVal id As Decimal) As AT_OT_REGISTRATIONDTO Implements IAttendanceBusiness.GetOTReigistrationByID

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetOTReigistrationByID(id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function OtGetValueOfYear(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal Implements IAttendanceBusiness.OtGetValueOfYear

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.OtGetValueOfYear(P_EMP_ID, P_DATE, P_HOURS, P_TYPE_OT)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPortalOtReg(ByVal objOT As AT_OT_REGISTRATIONDTO, ByVal P_HS_OT As String, ByVal P_USERNAME As String, ByVal P_ORG_OT_ID As Integer) As Decimal Implements IAttendanceBusiness.ModifyPortalOtReg

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ModifyPortalOtReg(objOT, P_HS_OT, P_USERNAME, P_ORG_OT_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_AT_OT_REGISTRATION(ByVal _param As ParamDTO, ByVal log As UserLog) As System.Data.DataSet Implements IAttendanceBusiness.EXPORT_AT_OT_REGISTRATION

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.EXPORT_AT_OT_REGISTRATION(_param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_OT_REGISTRATION_EXIT(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_HESO As String) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_OT_REGISTRATION_EXIT
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_OT_REGISTRATION_EXIT(P_EMP_CODE, P_DATE, P_HESO)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_LEAVE_EXITS(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_MANUAL_ID As Decimal, ByVal P_CA As Decimal) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_LEAVE_EXITS
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_LEAVE_EXITS(P_EMP_CODE, P_DATE, P_MANUAL_ID, P_CA)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_LEAVE_SHEET(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_CA As Decimal) As Decimal Implements ServiceContracts.IAttendanceBusiness.CHECK_LEAVE_SHEET
            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_LEAVE_SHEET(P_EMP_CODE, P_DATE, P_CA)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IAttendanceBusiness.CHECK_EMPLOYEE
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_NB(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_PERIOD_ID As Integer) As Boolean Implements ServiceContracts.IAttendanceBusiness.INPORT_NB
            Using rep As New AttendanceRepository
                Try

                    Return rep.INPORT_NB(P_DOCXML, log, P_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_NB_PREV(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean Implements ServiceContracts.IAttendanceBusiness.INPORT_NB_PREV
            Using rep As New AttendanceRepository
                Try

                    Return rep.INPORT_NB_PREV(P_DOCXML, log, P_YEAR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function ImportLeaveSheet(ByVal dt As DataTable, ByVal log As UserLog) As DataTable Implements ServiceContracts.IAttendanceBusiness.ImportLeaveSheet
            Using rep As New AttendanceRepository
                Try
                    Return rep.ImportLeaveSheet(dt, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "CONFIRM DECLARES OT"
        Public Function CHANGE_CONFIRM_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.CHANGE_CONFIRM_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CHANGE_CONFIRM_OT(params, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CONFIRM_DECLARES_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.CONFIRM_DECLARES_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CONFIRM_DECLARES_OT(params, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CALCAULATE_CONFIRM_DECLARES_OT(ByVal paramsOT As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, Optional ByVal params As ParamDTO = Nothing) As Boolean Implements _
            ServiceContracts.IAttendanceBusiness.CALCAULATE_CONFIRM_DECLARES_OT
            Using rep As New AttendanceRepository
                Try
                    Return rep.CALCAULATE_CONFIRM_DECLARES_OT(paramsOT, log, params)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "WAGE_OFFSET"
        Function GetWageOffset(ByVal _filter As AT_WAGEOFFSET_EMPDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_WAGEOFFSET_EMPDTO) Implements IAttendanceBusiness.GetWageOffset

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetWageOffset(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWageOffsetById(ByVal _id As Decimal?) As AT_WAGEOFFSET_EMPDTO Implements ServiceContracts.IAttendanceBusiness.GetWageOffsetById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetWageOffsetById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InSertWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InSertWageOffset
            Using rep As New AttendanceRepository
                Try

                    Return rep.InSertWageOffset(objWageOffset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyWageOffset
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyWageOffset(objWageOffset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWageOffset(ByVal _validate As AT_WAGEOFFSET_EMPDTO) Implements ServiceContracts.IAttendanceBusiness.ValidateWageOffset
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateWageOffset(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWageOffset(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteWageOffset
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteWageOffset(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "KET PHEP"
        Public Function GetAtConcludeAnnaul(ByVal _filter As AT_CONCLUDE_ANNUAL_YEARDTO,
                                ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_CONCLUDE_ANNUAL_YEARDTO) Implements IAttendanceBusiness.GetAtConcludeAnnaul

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetAtConcludeAnnaul(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAtCompensation(ByVal _filter As AT_COMPENSATION_YEARDTO,
                                ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATION_YEARDTO) Implements IAttendanceBusiness.GetAtCompensation

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetAtCompensation(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_CONCLUDE_ANNUAL_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean Implements IAttendanceBusiness.CAL_CONCLUDE_ANNUAL_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CAL_CONCLUDE_ANNUAL_YEAR(_param, log, P_YEAR)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_COMPENSATION_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean Implements IAttendanceBusiness.CAL_COMPENSATION_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CAL_COMPENSATION_YEAR(_param, log, P_YEAR)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_AT_COMPENSATION_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.IMPORT_AT_COMPENSATION_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IMPORT_AT_COMPENSATION_YEAR(P_DOCXML, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompensationYear(ByVal _lstID As List(Of Decimal)) As Boolean Implements IAttendanceBusiness.DeleteCompensationYear

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.DeleteCompensationYear(_lstID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_AT_CONCLUDE_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.IMPORT_AT_CONCLUDE_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.IMPORT_AT_CONCLUDE_YEAR(P_DOCXML, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteConcludeYear(ByVal _lstID As List(Of Decimal)) As Boolean Implements IAttendanceBusiness.DeleteConcludeYear

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.DeleteConcludeYear(_lstID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function UPDATE_CONCLUDE_ANNUAL_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_ANNUAL As Decimal, ByVal P_ANNUAL_TRANFER As Decimal, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.UPDATE_CONCLUDE_ANNUAL_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.UPDATE_CONCLUDE_ANNUAL_YEAR(P_ID, P_YEAR_ANNUAL, P_ANNUAL_TRANFER, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_COMPENSATION_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_NB As Decimal, ByVal P_NB_TRANFER As Decimal, ByVal P_NB_EDIT As Decimal, ByVal log As UserLog) As Boolean Implements IAttendanceBusiness.UPDATE_COMPENSATION_YEAR

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.UPDATE_COMPENSATION_YEAR(P_ID, P_YEAR_NB, P_NB_TRANFER, P_NB_EDIT, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Assign Emp to Calendar"
        Public Function GetEmployeeNotByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO) _
                   Implements IAttendanceBusiness.GetEmployeeNotByCalendarID
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEmployeeNotByCalendarID(_filter, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO) _
                   Implements IAttendanceBusiness.GetEmployeeByCalendarID
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetEmployeebByCalendarID(_filter, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO, ByVal log As UserLog) As Decimal Implements ServiceContracts.IAttendanceBusiness.InsertEmployeeByCalendarID
            Using rep As New AttendanceRepository
                Try
                    Return rep.InsertEmployeeByCalendarID(_filter, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO, ByVal log As UserLog) As Decimal Implements ServiceContracts.IAttendanceBusiness.DeleteEmployeeByCalendarID
            Using rep As New AttendanceRepository
                Try
                    Return rep.DeleteEmployeeByCalendarID(_filter, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function CREATE_WORKNIGHT(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataTable Implements IAttendanceBusiness.CREATE_WORKNIGHT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.CREATE_WORKNIGHT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_WORKNIGHT(ByVal param As AT_WORKNIGHTDTO, ByVal log As Framework.Data.UserLog) As System.Data.DataSet Implements IAttendanceBusiness.GET_WORKNIGHT

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GET_WORKNIGHT(param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkNightByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertWorkNightByImport
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertWorkNightByImport(dtData, period_id, emp_obj_id, start_date, end_date, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#Region "Khóa bảng công nhân viên - AT_TIMESHEET_LOCK"
        Function GetAtTimesheetLock(ByVal _filter As AT_TIMESHEET_LOCKDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIMESHEET_LOCKDTO) Implements IAttendanceBusiness.GetAtTimesheetLock

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetAtTimesheetLock(_filter, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAtTimesheetLockById(ByVal _id As Decimal?) As AT_TIMESHEET_LOCKDTO Implements ServiceContracts.IAttendanceBusiness.GetAtTimesheetLockById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAtTimesheetLockById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InSertAtTimesheetLock(ByVal objWageOffset As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InSertAtTimesheetLock
            Using rep As New AttendanceRepository
                Try

                    Return rep.InSertAtTimesheetLock(objWageOffset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAtTimesheetLock(ByVal objWageOffset As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAtTimesheetLock
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAtTimesheetLock(objWageOffset, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAtTimesheetLock(ByVal _validate As AT_TIMESHEET_LOCKDTO) Implements ServiceContracts.IAttendanceBusiness.ValidateAtTimesheetLock
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAtTimesheetLock(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAtTimesheetLock(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAtTimesheetLock
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAtTimesheetLock(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Leave Payment"
        Function GetLeavePayments(ByVal _filter As AT_LEAVE_PAYMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVE_PAYMENTDTO) Implements IAttendanceBusiness.GetLeavePayments

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetLeavePayments(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLeavePaymentById(ByVal _id As Decimal?) As AT_LEAVE_PAYMENTDTO Implements ServiceContracts.IAttendanceBusiness.GetLeavePaymentById
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLeavePaymentById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InSertLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.InSertLeavePayment
            Using rep As New AttendanceRepository
                Try

                    Return rep.InSertLeavePayment(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyLeavePayment
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyLeavePayment(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateLeavePayment(ByVal _validate As AT_LEAVE_PAYMENTDTO) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateLeavePayment
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateLeavePayment(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLeavePayment(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteLeavePayment
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteLeavePayment(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLeavePaymentSal(ByVal empID As Decimal, ByVal effect_date As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.GetLeavePaymentSal
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetLeavePaymentSal(empID, effect_date)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Leave Payment"
        Function GetTimesheetDetail_portal(ByVal empID As Decimal, ByVal year As Decimal, ByVal month As Decimal) As DataTable Implements IAttendanceBusiness.GetTimesheetDetail_portal

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimesheetDetail_portal(empID, year, month)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetTimesheetDetail(ByVal PeriodId As Decimal, ByVal EmployeeId As Decimal, ByVal log As UserLog) As DataTable Implements IAttendanceBusiness.GetTimesheetDetail

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.GetTimesheetDetail(PeriodId, EmployeeId, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Khác"
        Public Function CheckPeriodIsLock(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As Boolean Implements IAttendanceBusiness.CheckPeriodIsLock
            Using rep As New AttendanceRepository
                Try
                    Return rep.CheckPeriodIsLock(P_EMPLOYEE_ID, P_START_DATE, P_END_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function CheckValidateApprove(ByVal EMPLOYEE_ID As Decimal, ByVal PROCESS_TYPE As String, ByVal ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As VALIDATE_DTO Implements IAttendanceBusiness.CheckValidateApprove
            Try
                Using rep As New AttendanceRepository
                    Try
                        Return rep.CheckValidateApprove(EMPLOYEE_ID, PROCESS_TYPE, ID, P_START_DATE, P_END_DATE)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
        Public Function IMPORT_AT_TOXICLEAVE_EMP(ByVal P_XML As String, ByVal P_USERNAME As String) As Boolean Implements IAttendanceBusiness.IMPORT_AT_TOXICLEAVE_EMP
            Using rep As New AttendanceRepository
                Try
                    Return rep.IMPORT_AT_TOXICLEAVE_EMP(P_XML, P_USERNAME)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class

End Namespace
