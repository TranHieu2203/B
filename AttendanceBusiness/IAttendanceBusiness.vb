Imports AttendanceDAL
Imports Framework.Data
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace AttendanceBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IAttendanceBusiness
#Region "LeaveSheet"
        <OperationContract()>
        Function ImportLeaveSheet(ByVal dt As DataTable, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function ValidateLeaveSheetDetail(ByVal objValidate As AT_LEAVESHEETDTO) As Boolean
        <OperationContract()>
        Function GetLeaveSheet_ById(ByVal Leave_SheetID As Decimal, ByVal Struct As Decimal) As DataSet

        <OperationContract()>
        Function GET_EXPIREDATE_P_BU(ByVal EMP_ID As Decimal, ByVal Fromdate As Date) As DataTable

        <OperationContract()>
        Function GetLeaveSheet_Detail_ByDate(ByVal employee_id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, manualId As Decimal) As DataTable
        <OperationContract()>
        Function Validate_LeaveSheet(ByVal _validate As AT_LEAVESHEETDTO) As VALIDATE_DTO
        <OperationContract()>
        Function SaveLeaveSheet(ByVal dsLeaveSheet As DataSet, Optional ByVal log As UserLog = Nothing) As Boolean
        <OperationContract()>
        Function SaveLeaveSheet_Portal(ByVal LeaveSheet As AT_LEAVESHEETDTO, ByVal dsLeaveSheet As List(Of AT_LEAVESHEETDTO), ByRef gID As Decimal, Optional ByVal log As UserLog = Nothing) As Boolean

        <OperationContract()>
        Function GetLeaveSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        <OperationContract()>
        Function GetLeaveCTSheet_Portal(ByVal _filter As AT_LEAVESHEETDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
#End Region
        <OperationContract()>
        Function ReadCheckInOutData(ByVal dateFrom As Date, ByVal dateTo As Date, Optional ByVal TerId As Decimal = -1) As Boolean
        <OperationContract()>
        Function ReadCheckInOutData_CheckOUT(ByVal _lst_Terminal As AT_TERMINALSDTO, ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean
        <OperationContract()>
        Function GETIDFROMPROCESS(ByVal Id As Decimal) As Decimal
        <OperationContract()>
        Function CAL_SUMMARY_DATA_INOUT(ByVal Period_id As Decimal) As Boolean
        <OperationContract()>
        Function CHECK_TYPE_BREAK(ByVal type_break_id As Decimal) As DataTable
        <OperationContract()>
        Function IMPORT_AT_SWIPE_DATA_V1(ByVal log As UserLog, ByVal DATA_IN As String, ByVal Machine_type As Decimal) As Boolean
        <OperationContract()>
        Function CHECK_CONTRACT(ByVal employee_id As Decimal) As DataTable
        <OperationContract()>
        Function CHECK_PERIOD_CLOSE(ByVal periodid As Integer) As Integer
        <OperationContract()>
        Function CHECK_PERIOD_CLOSE1(ByVal periodid As Integer, ByVal EmpId As Integer) As Integer
        <OperationContract()>
        Function PRS_COUNT_SHIFT(ByVal employee_id As Decimal) As DataTable
        <OperationContract()>
        Function GetperiodID(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As Decimal

        <OperationContract()>
        Function GetperiodByEmpObj(ByVal objEmp As Decimal, ByVal _dateGet As Date) As AT_PERIODDTO

        <OperationContract()>
        Function GetperiodID_2(ByVal employee_Id As Decimal, ByVal RegDate As Date) As Decimal

        <OperationContract()>
        Function PRS_COUNT_INOUTKH(ByVal employee_id As Decimal, ByVal year As Decimal) As DataTable

        <OperationContract()>
        Function GET_AT_SETUPELEAVE_YEAR(ByVal year As Decimal) As DataTable

        <OperationContract()>
        Function SUM_AT_ADVANCELEAVE_EMP_YEAR(ByVal empId As Decimal, ByVal year As Decimal, ByVal ID As Decimal) As DataTable

        <OperationContract()>
        Function GetDayHoliday() As List(Of AT_HOLIDAYDTO)
        <OperationContract()>
        Function GetLeaveInOutKH(ByVal employee_Id As Decimal) As List(Of LEAVEINOUTKHDTO)
        <OperationContract()>
        Function GetLeaveRegistrationListByLM(ByVal _filter As AT_PORTAL_REG_DTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function Upd_TimeTImesheetMachines(ByVal LstObj As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean
        <OperationContract()>
        Function GET_MANUAL_BY_ID(ByVal id As Decimal) As DataTable
        <OperationContract()>
        Function GET_INFO_PHEPNAM(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        <OperationContract()>
        Function GET_INFO_NGHIBU(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        <OperationContract()>
        Function GET_INFO_PHEPNAM_IMPORT_CTT(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
#Region "CONFIG TEMPLATE"
        <OperationContract()>
        Function GET_CONFIG_TEMPLATE(ByVal MACHINE_TYPE As Decimal?) As DataSet
#End Region
        <OperationContract()>
        Function IMPORT_AT_SWIPE_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean
        <OperationContract()>
        Function GET_SWIPE_DATA_IMPORT(ByVal _orgid As Decimal, ByVal _is_dissolove As Boolean, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT_SHIFT() As DataTable
        <OperationContract()>
        Function GET_AT_WORKSIGN_EDIT(ByVal empId As Decimal, ByVal startDate As Date, ByVal EndDate As Date) As DataTable
        <OperationContract()>
        Function GetDataFromOrg(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GetPortalWSDataImport(ByVal obj As ParamDTO) As DataSet
        <OperationContract()>
        Function getSetUpAttEmp(ByVal _filter As SetUpCodeAttDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SetUpCodeAttDTO)
        <OperationContract()>
        Function InsertSetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetUpAttEmp(ByVal objValue As SetUpCodeAttDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSetUpAttEmp(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckValidateMACC(ByVal obj As SetUpCodeAttDTO) As Boolean
        <OperationContract()>
        Function CheckValidateAPPROVE_DATE(ByVal obj As SetUpCodeAttDTO) As Boolean
        <OperationContract()>
        Function IMPORT_TIMESHEET_MACHINE(ByVal ListobjImport As List(Of AT_TIME_TIMESHEET_MACHINETDTO), Optional ByVal log As UserLog = Nothing) As Boolean
        <OperationContract()>
        Function CheckExistAT_LATE_COMBACKOUT(ByVal objImport As AT_TIME_TIMESHEET_MACHINETDTO) As Boolean
#Region "Get Data Combobox"
        <OperationContract()>
        Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32
        <OperationContract()>
        Function UPDATE_INSERT_AT_SWIPE_DATA(ByVal id As Decimal) As Int32
        <OperationContract()>
        Function CAL_TIME_TIMESHEET_EMP(ByVal id As Decimal, ByVal log As UserLog) As Int32
        <OperationContract()>
        Function INSERT_REGIMES(ByVal ID As Decimal, ByVal MANUAL_ID As Decimal) As Int32
        <OperationContract()>
        Function PRS_GETLEAVE_BY_APPROVE1(ByVal param As AT_PORTAL_REG_DTO,
                                         Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As DataTable
        <OperationContract()>
        Function PRS_GETDMVS_BY_APPROVE(ByVal param As AT_PORTAL_REG_DTO,
                                         Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As DataTable
        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectTitleList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProjectWorkList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO, Optional ByVal strUser As String = "ADMIN") As Boolean

        <OperationContract()>
        Function GET_AT_PORTAL_REG(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable

        <OperationContract()>
        Function GET_AT_PORTAL_REG_OT(ByVal P_ID As Decimal, ByVal P_EMPLOYEE As Decimal, ByVal P_DATE_TIME As Date) As DataTable

#End Region

#Region "Ky cong"
        <OperationContract()>
        Function Load_date(ByVal period_id As Decimal, ByVal emp_obj_id As Decimal) As AT_PERIODDTO

        <OperationContract()>
        Function Load_Emp_obj() As List(Of AT_PERIODDTO)

        <OperationContract()>
        Function LOAD_PERIOD(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function LOAD_PERIODBylinq(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As List(Of AT_PERIODDTO)
        <OperationContract()>
        Function LOAD_PERIODYEAR() As List(Of AT_PERIODDTO)
        <OperationContract()>
        Function LOAD_PERIODByID(ByVal obj As AT_PERIODDTO, ByVal log As UserLog) As AT_PERIODDTO
        <OperationContract()>
        Function CLOSEDOPEN_PERIOD(ByVal param As ParamDTO, ByVal log As Framework.Data.UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIOD_PAYSTATUS(ByVal _param As ParamDTO, ByVal isAfter As Boolean, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIODSTATUS(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IS_PERIODSTATUS_BY_DATE(ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "QUAN LY VAO RA"
        <OperationContract()>
        Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_DATAINOUTDTO)
        <OperationContract()>
        Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date,
                                 ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteDataInout(ByVal lstDataInout() As AT_DATAINOUTDTO) As Boolean

#End Region

#Region "Dang ky lam them"
        <OperationContract()>
        Function GetRegisterOT(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        <OperationContract()>
        Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        <OperationContract()>
        Function ValidateRegisterOT(ByVal _validate As AT_REGISTER_OTDTO) As Boolean
        <OperationContract()>
        Function DeleteRegisterOT(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        <OperationContract()>
        Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean

        <OperationContract()>
        Function ApproveRegisterOT(ByVal lstData As List(Of AT_REGISTER_OTDTO), ByVal status As Decimal) As Boolean

        <OperationContract()>
        Function GetRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable

#End Region

#Region "Bang cong may"
        <OperationContract()>
        Function GetMachinesPortal(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        <OperationContract()>
        Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)

        <OperationContract()>
        Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_fromdate As Date, ByVal p_enddate As Date, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_delAll As Decimal, ByVal codecase As String) As Boolean
        <OperationContract()>
        Function ActiveMachines(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteTimesheetMachinet(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Bang cong TAY"

        <OperationContract()>
        Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function ModifyLeaveSheetDaily(ByVal objLeave As AT_TIME_TIMESHEET_DAILYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal log As UserLog, ByVal PeriodID As Decimal, ByVal EmpObj As Decimal) As DataTable

        <OperationContract()>
        Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
#End Region

#Region "Bang cong lam them"
        <OperationContract()>
        Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?), ByVal p_emp_obj As Decimal) As Boolean

        <OperationContract()>
        Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean

        <OperationContract()>
        Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ModifyLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetOt(ByVal objLeave As AT_TIME_TIMESHEET_OTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
#End Region

#Region "Tổng hợp công"
        <OperationContract()>
        Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        <OperationContract()>
        Function CAL_TIME_TIMESHEET_MONTHLY(ByVal param As ParamDTO, ByVal codecase As String, ByVal lstEmployee As List(Of Decimal?), ByVal log As Framework.Data.UserLog) As Boolean

        <OperationContract()>
        Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)

        <OperationContract()>
        Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String, ByVal log As UserLog)

        <OperationContract()>
        Function ActiveTIME_TIMESHEET_MONTHLY(ByVal lstID As List(Of Decimal?), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteTimesheetMonthly(ByVal lstID As List(Of Decimal)) As Boolean
#Region "Công tháng PORTAL"

        <OperationContract()>
        Function GetTimeSheetForEmp_Month(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal PageIndex As Integer = 0,
                                          Optional ByVal PageSize As Integer = Integer.MaxValue,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)


#End Region
#End Region
#Region "Khai bao cong com"
        <OperationContract()>
        Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_RICEDTO)
        <OperationContract()>
        Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveDelareRice(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateDelareRice(ByVal _validate As AT_TIME_RICEDTO) As Boolean
        <OperationContract()>
        Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        <OperationContract()>
        Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean

#End Region
#Region "quan ly bu tru cham cong"
        <OperationContract()>
        Function GetOffSettingTimeKeeping(ByVal _filter As AT_OFFFSETTINGDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_OFFFSETTINGDTO)

        <OperationContract()>
        Function GetOffSettingTimeKeepingById(ByVal _id As Decimal?) As AT_OFFFSETTINGDTO
        <OperationContract()>
        Function GetEmployeeTimeKeepingID(ByVal _id As Decimal) As List(Of AT_OFFFSETTING_EMPDTO)
#End Region
#Region "Khai bao điều chỉnh thâm niên phép"
        <OperationContract()>
        Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_DECLARE_ENTITLEMENTDTO)
        <OperationContract()>
        Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        <OperationContract()>
        Function ActiveDelareEntitlementNB(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        <OperationContract()>
        Function DeleteOffTimeKeeping(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateMonthThamNien(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        <OperationContract()>
        Function ValidateMonthPhepNam(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        <OperationContract()>
        Function ValidateMonthNghiBu(ByVal _validate As AT_DECLARE_ENTITLEMENTDTO) As Boolean

#End Region

#Region "Bang tong hop cong com"
        <OperationContract()>
        Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        <OperationContract()>
        Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function ModifyLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ApprovedTimeSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertLeaveSheetRice(ByVal objLeave As AT_TIME_TIMESHEET_RICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
#End Region

#Region "Dang ky công"
        <OperationContract()>
        Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVESHEETDTO)
        <OperationContract()>
        Function InsertLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertLeaveSheetList(ByVal objRegisterList As List(Of AT_LEAVESHEETDTO), ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyLeaveSheet(ByVal objRegisterOT As AT_LEAVESHEETDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        <OperationContract()>
        Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal P_FROM_DATE As Date,
                                    ByVal P_TO_DATE As Date) As DataTable
        <OperationContract()>
        Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        <OperationContract()>
        Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal P_YEAR As Integer,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        <OperationContract()>
        Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)

        <OperationContract()>
        Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO

        <OperationContract()>
        Function ValidateLeaveSheet(ByVal _validate As AT_LEAVESHEETDTO) As Boolean
        <OperationContract()>
        Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        <OperationContract()>
        Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        <OperationContract()>
        Function ApproveApp(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal userName As String, ByVal type As String) As Decimal
        <OperationContract()>
        Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO)) As Boolean
        <OperationContract()>
        Function DeleteTimeSheetOT(ByVal lstID As List(Of AT_TIME_TIMESHEET_OTDTO)) As Boolean
        <OperationContract()>
        Function checkLeaveImport(ByVal dtData As DataTable) As DataTable

#End Region

#Region "di som ve muon"
        <OperationContract()>
        Function GetDSVM_List_Emp(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)

        <OperationContract()>
        Function GetDSVM(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        <OperationContract()>
        Function GetChildren(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "WORKINGDAY desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)
        <OperationContract()>
        Function GETCHILDREN_TAKECAREBYID(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        <OperationContract()>
        Function ModifyLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer
        <OperationContract()>
        Function ApproveLate_CombackoutPortal(ByVal lstObj As List(Of AT_LATE_COMBACKOUTDTO)) As Integer
        <OperationContract()>
        Function InsertLate_CombackoutPortal(ByVal obj As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog) As Integer
        <OperationContract()>
        Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        <OperationContract()>
        Function GetLate_CombackoutByIdPortal(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        <OperationContract()>
        Function GetLate_CombackoutByIdPortalNew(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        <OperationContract()>
        Function ImportLate_combackout(ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef gID_Swipe As String) As Boolean
        <OperationContract()>
        Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyLate_combackout(ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertChildren(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyChildren(ByVal objDataInout As AT_LATE_COMBACKOUTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateLate_combackout(ByVal _validate As AT_LATE_COMBACKOUTDTO) As Boolean
        <OperationContract()>
        Function DeleteLate_combackout(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteLate_combackoutPortal(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetDMVS_Portal(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LATE_COMBACKOUTDTO)

        <OperationContract()>
        Function DeletePortalLate(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetPortalEmpMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_TIMESHEET_MACHINETDTO)

        <OperationContract()>
        Function GetPortalMachinesByID(ByVal _id As Decimal) As AT_TIME_TIMESHEET_MACHINETDTO

#End Region

#Region "lam bu"
        <OperationContract()>
        Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATORYDTO)
#End Region

#Region "PHEP NAM"
        <OperationContract()>
        Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function AT_ENTITLEMENT_PREV_HAVE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CALCULATE_ENTITLEMENT_HOSE(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CheckPeriodMonth(ByVal year As Integer, ByVal PeriodId As Integer, ByRef PeriodNext As Integer) As Boolean
        <OperationContract()>
        Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_ENTITLEMENTDTO)
        <OperationContract()>
        Function ImportEntitlementLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_PERIOD As Decimal) As Boolean
        <OperationContract()>
        Function IMPORT_OT(ByVal P_DOCXML As String, ByVal P_USERNAME As String, Optional ByVal P_LOG As UserLog = Nothing) As Boolean
#End Region

#Region "WORKSIGN"
        <OperationContract()>
        Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function InsertWORKSIGNByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal)
        <OperationContract()>
        Function ValidateWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO) As Boolean

        <OperationContract()>
        Function ModifyWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWORKSIGN(ByVal lstWORKSIGN() As AT_WORKSIGNDTO) As Boolean
        <OperationContract()>
        Function GETSIGNDEFAULT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function Del_WorkSign_ByEmp(ByVal employee_id As String, ByVal p_From As Date, ByVal p_to As Date) As Boolean
        <OperationContract()>
        Function Modify_WorkSign_ByEmp(ByVal employee_id As Decimal,
                                          ByVal p_From As Date,
                                          ByVal p_to As Date,
                                          ByVal p_period As Decimal,
                                          ByVal obj As List(Of AT_WORKSIGNEDITDTO),
                                          ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Get_Date(ByVal employee_id As Decimal,
                             ByRef join_date As Date,
                             ByRef ter_effect_date As Date) As Boolean
        <OperationContract()>
        Function GET_PORTAL_WORKSIGN(ByVal param As AT_WORKSIGNDTO, ByVal p_type As String, ByVal P_IS_EXPORT As Decimal, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GetListWSWaitingApprove(ByVal param As AT_WORKSIGNDTO, ByVal P_IS_EXPORT As Decimal) As DataSet
        <OperationContract()>
        Function CheckNotSendPortalWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean
        <OperationContract()>
        Function CheckWaittingApprovePTWS(ByVal _empID As Decimal, ByVal _period_ID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePortalWS(ByVal _lstIDRegGroup As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ImportPortalWS(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean
#End Region

#Region "ProjectAssign"
        <OperationContract()>
        Function GET_ProjectAssign(ByVal param As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function InsertProjectAssign(ByVal objProjectAssigns As List(Of AT_PROJECT_ASSIGNDTO), ByVal objWork As AT_PROJECT_ASSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByVal log As UserLog, ByRef gID As Decimal)

        <OperationContract()>
        Function ModifyProjectAssign(ByVal objProjectAssign As AT_PROJECT_ASSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProjectAssign(ByVal lstProjectAssign() As AT_PROJECT_ASSIGNDTO) As Boolean
#End Region

#Region "wifi-gps"
        <OperationContract()>
        Function GetSetupWifi(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO)
        <OperationContract()>
        Function GetSetupGPS(ByVal _filter As AT_SETUP_WIFI_GPS_DTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_WIFI_GPS_DTO)
        <OperationContract()>
        Function GetSetupGPSByID(ByVal _filter As AT_SETUP_WIFI_GPS_DTO) As AT_SETUP_WIFI_GPS_DTO

        <OperationContract()>
        Function InsertSetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertSetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateWIFI_GPS(ByVal org_id As Decimal, ByVal id As Decimal, ByVal flag As String) As Decimal
        <OperationContract()>
        Function ModifySetupWifi(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupGPS(ByVal objTitle As AT_SETUP_WIFI_GPS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveSetupWifi(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ActiveSetupGPS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSetupWifi(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteSetupGPS(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Holiday"
        <OperationContract()>
        Function GetHoliday(ByVal _filter As AT_HOLIDAYDTO,
                             Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        <OperationContract()>
        Function GetHoliday_Hose(ByVal _filter As AT_HOLIDAYDTO,
                             Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAYDTO)
        <OperationContract()>
        Function InsertHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertHOLIDAY_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean
        <OperationContract()>
        Function ValidateHOLIDAY_Hose(ByVal objHOLIDAY As AT_HOLIDAYDTO) As Boolean

        <OperationContract()>
        Function ModifyHOLIDAY(ByVal objHOLIDAY As AT_HOLIDAYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveHoliday(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function ActiveHoliday_Hose(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteHOLIDAY(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteHOLIDAY_Hose(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Holiday gereden"
        <OperationContract()>
        Function GetHolidayGerenal(ByVal _filter As AT_HOLIDAY_GENERALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_GENERALDTO)
        <OperationContract()>
        Function InsertHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO) As Boolean

        <OperationContract()>
        Function ModifyHolidayGerenal(ByVal objHOLIDAYGR As AT_HOLIDAY_GENERALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveHolidayGerenal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteHolidayGerenal(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "AT_FML danh mục kiểu công"
        <OperationContract()>
        Function GetSignByPage(ByVal pagecode As String) As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetAT_FML(ByVal _filter As AT_FMLDTO,
                               Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_FMLDTO)
        <OperationContract()>
        Function InsertAT_FML(ByVal objATFML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_FML(ByVal objATFML As AT_FMLDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_FML(ByVal objATFML As AT_FMLDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_FML(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_FML(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục ca làm việc"
        <OperationContract()>
        Function GetAT_GSIGN(ByVal _filter As AT_GSIGNDTO,
                               Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_GSIGNDTO)
        <OperationContract()>
        Function InsertAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_GSIGN(ByVal objGSIGN As AT_GSIGNDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_GSIGN(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_GSIGN(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Quy định phạt đi sớm về muộn"
        <OperationContract()>
        Function GetAT_DMVS(ByVal _filter As AT_DMVSDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DMVSDTO)
        <OperationContract()>
        Function InsertAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_DMVS(ByVal objData As AT_DMVSDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_DMVS(ByVal objData As AT_DMVSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_DMVS(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_DMVS(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "May cham cong"
        <OperationContract()>
        Function GetTerminalFromOtOtherList() As DataTable

        <OperationContract()>
        Function GetTerminal(ByVal obj As AT_TERMINALSDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function GetTerminalAuto() As DataTable

        <OperationContract()>
        Function UpdateTerminalLastTime(ByVal obj As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function UpdateTerminalStatus(ByVal obj As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        <OperationContract()>
        Function GetTerminalData(ByVal lstCode As List(Of String), ByVal username As String, ByVal orgID As Decimal) As List(Of OT_OTHERLIST_DTO)

        <OperationContract()>
        Function InsertSwipeData(ByVal objSwipeData As List(Of AT_SWIPE_DATADTO), ByVal machine As AT_TERMINALSDTO, ByVal P_FROMDATE As Date?, ByVal P_ENDDATE As Date?, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSwipeDataAuto(ByVal lstSwipeData As List(Of AT_SWIPE_DATADTO)) As Boolean

        <OperationContract()>
        Function InsertSwipeDataImport(ByVal objDelareRice As List(Of AT_SWIPE_DATADTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSwipeData(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function IMPORT_INOUT(ByVal docXML As String, ByVal fromDate As DateTime, ByVal toDate As DateTime, ByVal log As UserLog) As Boolean

#End Region

#Region "Danh mục ca làm việc"
        <OperationContract()>
        Function GetAT_SHIFT(ByVal _filter As AT_SHIFTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTDTO)
        <OperationContract()>
        Function InsertAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SHIFT(ByVal objData As AT_SHIFTDTO) As Boolean

        <OperationContract()>
        Function ValidateAT_ORG_SHIFT(ByVal _ID As Decimal, ByVal _ORGID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAT_SHIFT(ByVal objData As AT_SHIFTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SHIFT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SHIFT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetAT_TIME_MANUALBINCOMBO() As DataTable

        <OperationContract()>
        Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean

        <OperationContract()>
        Function GetAT_SHIFT_ORG_ACCESS_By_ID(ByVal _AT_SHIFT_ID As Decimal) As List(Of Decimal)
#End Region

#Region "Thiết lập số ngày nghỉ theo đối tượng"
        <OperationContract()>
        Function GetAT_Holiday_Object(ByVal _filter As AT_HOLIDAY_OBJECTDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_HOLIDAY_OBJECTDTO)
        <OperationContract()>
        Function InsertAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_Holiday_Object(ByVal objHoliO As AT_HOLIDAY_OBJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_Holiday_Object(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_Holiday_Object(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Thiết lập đối tượng chấm công theo cấp nhân sự"
        <OperationContract()>
        Function GetAT_SETUP_SPECIAL(ByVal _filter As AT_SETUP_SPECIALDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_SPECIALDTO)
        <OperationContract()>
        Function InsertAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_SETUP_SPECIAL(ByVal objData As AT_SETUP_SPECIALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SETUP_SPECIAL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Thiết lập đối tượng chấm công theo nhân viên"
        <OperationContract()>
        Function GetAT_SETUP_TIME_EMP(ByVal _filter As AT_SETUP_TIME_EMPDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_TIME_EMPDTO)
        <OperationContract()>
        Function InsertAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_SETUP_TIME_EMP(ByVal objData As AT_SETUP_TIME_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SETUP_TIME_EMP(ByVal lstID As List(Of Decimal)) As Boolean
#End Region
#Region "Thiết lập thang quy đổi"
        <OperationContract()>
        Function GetAT_SetUp_Exchange(ByVal _filter As AT_SETUP_EXCHANGEDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_SETUP_EXCHANGEDTO)
        <OperationContract()>
        Function InsertAT_SetUp_Exchange(ByVal objData As AT_SETUP_EXCHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAT_SetUp_Exchange(ByVal objData As AT_SETUP_EXCHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_SetUp_Exchange(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SetUp_Exchange(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckTrung_AT__SetUp_exchange(ByVal id As Decimal, ByVal from_minute As Decimal,
                                               ByVal to_minute As Decimal,
                                               ByVal EFFECT_DATE As Date,
                                                  ByVal OBJECT_ATTENDACE As Decimal,
                                                  ByVal TYPE_EXCHANGE As Decimal,
                                                  ByVal ORG_ID As Decimal) As Integer
#End Region
#Region "Đăng ký máy chấm công"
        <OperationContract()>
        Function GetAT_TERMINAL(ByVal _filter As AT_TERMINALSDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)
        <OperationContract()>
        Function GetAT_TERMINAL_STATUS(ByVal _filter As AT_TERMINALSDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALSDTO)

        <OperationContract()>
        Function InsertAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_TERMINAL(ByVal objData As AT_TERMINALSDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_TERMINAL(ByVal objData As AT_TERMINALSDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_TERMINAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_TERMINAL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đăng ký chấm công mặc định"
        <OperationContract()>
        Function GetAT_SIGNDEFAULT(ByVal _filter As AT_SIGNDEFAULTDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULTDTO)


        <OperationContract()>
        Function GetAT_YEARLEAVE_EDIT(ByVal _filter As AT_YEAR_LEAVE_EDITDTO,
                                Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE ASC",
                                  Optional ByVal log As UserLog = Nothing) As List(Of AT_YEAR_LEAVE_EDITDTO)

        <OperationContract()>
        Function InsertAT_YEARLEAVE_EDIT(ByVal objTitle As AT_YEAR_LEAVE_EDITDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function ModifyAT_YEARLEAVE_EDIT(ByVal objTitle As AT_YEAR_LEAVE_EDITDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteExistAT_YEARLEAVE_EDIT(ByVal EmployeeID As Decimal, ByVal Year As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function InsertAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean


        <OperationContract()>
        Function GetAT_ListShift() As DataTable

        <OperationContract()>
        Function GetAT_PERIOD() As DataTable

        <OperationContract()>
        Function GetEmployeeID(ByVal employee_code As String, ByVal end_date As Date) As DataTable

        <OperationContract()>
        Function GetEmployeeIDExits(ByVal employee_code As String) As DataTable

        <OperationContract()>
        Function GetEmployeeIDInSign(ByVal employee_code As String) As DataTable
        <OperationContract()>
        Function GetEmployeeByTimeID(ByVal time_id As Decimal) As DataTable

        <OperationContract()>
        Function ModifyAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO, ByVal log As UserLog, ByRef gID As Decimal, Optional ByVal param As ParamDTO = Nothing) As Boolean

        <OperationContract()>
        Function ValidateAT_SIGNDEFAULT(ByVal objSIGNDEF As AT_SIGNDEFAULTDTO) As Boolean

        <OperationContract()>
        Function ActiveAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function DeleteAT_YEARLEAVE_EDIT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function checkCopySignDefault(ByVal _empID As Decimal, ByVal _year As Decimal) As Boolean

        <OperationContract()>
        Function COPY_SIGN_DEFAULT(ByVal P_ID_COPY As Decimal, ByVal P_YEAR As Decimal, ByVal P_EMP_ID As Decimal, ByVal P_USERNAME As String) As Boolean
#End Region
#Region "Đăng ký ca mặc định cho phòng ban"
        <OperationContract()>
        Function GetAT_SIGNDEFAULT_ORG(ByVal _filter As AT_SIGNDEFAULT_ORGDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of AT_SIGNDEFAULT_ORGDTO)
        <OperationContract()>
        Function InsertAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAT_SIGNDEFAULT_ORG(ByVal obj As AT_SIGNDEFAULT_ORGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_SIGNDEFAULT_ORG(ByVal _validate As AT_SIGNDEFAULT_ORGDTO) As Boolean

        <OperationContract()>
        Function ActiveAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_SIGNDEFAULT_ORG(ByVal lstID As List(Of Decimal)) As Boolean
#End Region
#Region "Đăng ký OT trên portal"
        <OperationContract()>
        Function CheckRegDateBetweenJoinAndTerDate(ByVal empId As Decimal, ByVal regDate As Date) As Boolean

        <OperationContract()>
        Function CHECK_LEAVE_ORG_PAUSE_OT(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date) As Integer

        <OperationContract()>
        Function GetOtRegistration(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_OT_REGISTRATIONDTO)
        <OperationContract()>
        Function InsertOtRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyotRegistration(ByVal obj As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function SendApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal log As UserLog) As Integer
        <OperationContract()>
        Function ApproveOtRegistration(ByVal obj As List(Of AT_OT_REGISTRATIONDTO), ByVal empId As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateOtRegistration(ByVal _validate As AT_OT_REGISTRATIONDTO)
        <OperationContract()>
        Function Validate_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        <OperationContract()>
        Function Api_register_orvertime(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        <OperationContract()>
        Function API_APPROVEREGISTER_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        <OperationContract()>
        Function API__SEND_APPROVE_OT(ByVal _validate As AT_OT_REGISTRATIONDTO) As VALIDATE_DTO
        <OperationContract()>
        Function HRReviewOtRegistration(ByVal lst As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteOtRegistration(ByVal lstId As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GET_REG_PORTAL(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                ByVal strId As String, ByVal type As String) As List(Of APPOINTMENT_DTO)
        <OperationContract()>
        Function GET_TOTAL_OT_APPROVE(ByVal empid As Decimal, ByVal enddate As Date) As Decimal
        <OperationContract()>
        Function AT_CHECK_ORG_PERIOD_STATUS_OT(ByVal LISTORG As String, ByVal PERIOD As Decimal) As Int32
        <OperationContract()>
        Function GET_LIST_HOURS() As DataTable
        <OperationContract()>
        Function GET_LIST_MINUTE() As DataTable
        <OperationContract()>
        Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32

        <OperationContract()>
        Function GET_TIMESHEET_PAYSLIP(ByVal employee_id As Decimal, ByVal tag As String) As String

        <OperationContract()>
        Function PRI_PROCESS_APP_CANCEL(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal id_reggroup As Integer, ByVal token As String, ByVal template_id As Integer) As Int32
        <OperationContract()>
        Function GET_SEQ_PORTAL_RGT() As Decimal
        <OperationContract()>
        Function GET_ORGID(ByVal EMPID As Integer) As Int32
        <OperationContract()>
        Function GET_PERIOD(ByVal DATE_CURRENT As Date) As Int32
        <OperationContract()>
        Function AT_CHECK_EMPLOYEE(ByVal EMPID As Decimal, ByVal ENDDATE As Date) As Int32
        <OperationContract()>
        Function GET_TOTAL_OT_APPROVE3(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        <OperationContract()>
        Function CHECK_RGT_OT(ByVal EMPID As Decimal, ByVal STARTDATE As Date, ByVal ENDDATE As Date,
                                 ByVal FROM_HOUR As String, ByVal TO_HOUR As String, ByVal HOUR_RGT As Decimal) As Int32

        <OperationContract()>
        Function GetPortalOtRegData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetPortalOtRegByAnotherData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal PageIndex As Integer = 0,
                                             Optional ByVal PageSize As Integer = Integer.MaxValue,
                                             Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetPortalOtApproveData(ByVal _filter As AT_OT_REGISTRATIONDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "REGIST_DATE", Optional ByVal log As UserLog = Nothing) As DataTable
#End Region

#Region "Đăng ký nghỉ trên portal"
        <OperationContract()>
        Function GetHolidayByCalenderToTable(ByVal startdate As Date, ByVal enddate As Date) As DataTable
        <OperationContract()>
        Function GetPlanningAppointmentByEmployee(ByVal empid As Decimal, ByVal startdate As DateTime, ByVal enddate As DateTime,
                                                  ByVal listSign As List(Of AT_TIME_MANUALDTO)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function InsertPortalRegister(ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetHolidayByCalender(ByVal startdate As Date, ByVal enddate As Date) As List(Of Date)
        <OperationContract()>
        Function GetRegisterAppointmentInPortalByEmployee(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                          ByVal listSign As List(Of AT_TIME_MANUALDTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function GetRegisterAppointmentInPortalOT(ByVal empid As Decimal, ByVal startdate As Date, ByVal enddate As Date,
                                                          ByVal listSign As List(Of OT_OTHERLIST_DTO), ByVal status As List(Of Short)) As List(Of AT_TIMESHEET_REGISTERDTO)
        <OperationContract()>
        Function GetTotalLeaveInYear(ByVal empid As Decimal, ByVal p_year As Decimal) As Decimal

        <OperationContract()>
        Function DeletePortalRegisterByDate(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of AT_TIME_MANUALDTO)) As Boolean
        <OperationContract()>
        Function DeletePortalRegisterByDateOT(ByVal listappointment As List(Of AT_TIMESHEET_REGISTERDTO), ByVal listSign As List(Of OT_OTHERLIST_DTO)) As Boolean
        <OperationContract()>
        Function DeletePortalRegister(ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function SendRegisterToApprove(ByVal objLstRegisterId As List(Of Decimal), ByVal process As String, ByVal currentUrl As String) As String

#End Region

#Region "Phê duyệt đang ký nghỉ trên portal"
        <OperationContract()>
        Function GetListSignCode(ByVal gSignCode As String) As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetListWaitingForApprove(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function GetListWaitingForApproveOT(ByVal approveId As Decimal, ByVal process As String, ByVal filter As ATRegSearchDTO) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function ApprovePortalRegister(ByVal regID As Decimal?, ByVal approveId As Decimal,
                                       ByVal status As Integer, ByVal note As String,
                                       ByVal currentUrl As String, ByVal process As String,
                                             ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeList() As DataTable
        <OperationContract()>
        Function GetLeaveDay(ByVal dDate As Date) As DataTable
#End Region

#Region "Thiết lập kiểu công"
        <OperationContract()>
        Function GetAT_TIME_MANUAL(ByVal _filter As AT_TIME_MANUALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_MANUALDTO)
        <OperationContract()>
        Function GetAT_TIME_MANUALById(ByVal _id As Decimal?) As AT_TIME_MANUALDTO
        <OperationContract()>
        Function InsertAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_TIME_MANUAL(ByVal objHOLIDAY As AT_TIME_MANUALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAT_TIME_MANUAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAT_TIME_MANUAL(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetDataImportCO() As DataTable
        <OperationContract()>
        Function GetDataImportCO1() As DataSet
#End Region

#Region "Danh mục tham số hệ thống"
        <OperationContract()>
        Function GetListParamItime(ByVal _filter As AT_LISTPARAM_SYSTEAMDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LISTPARAM_SYSTEAMDTO)
        <OperationContract()>
        Function InsertListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO) As Boolean

        <OperationContract()>
        Function ModifyListParamItime(ByVal objHOLIDAY As AT_LISTPARAM_SYSTEAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListParamItime(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteListParamItime(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Báo cáo"
        <OperationContract()>
        Function GET_REPORT() As DataTable
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        <OperationContract()>
        Function GET_AT001(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT002(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT003(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT004(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT005(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT006(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_AT007(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT008(ByVal obj As ParamDTO, ByVal P_DATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT009(ByVal obj As ParamDTO, ByVal P_FROMDATE As Date, ByVal P_TODATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT010(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_AT011(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet
#End Region

#Region "Systeam"
        <OperationContract()>
        Function CheckPeriodClose(ByVal lstEmp As List(Of Decimal), ByVal startdate As Date, ByVal enddate As Date, ByRef sAction As String) As Boolean
        <OperationContract()>
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
        <OperationContract()>
        Function CheckExistInDatabaseAT_SIGNDEFAULT(ByVal lstID As List(Of Decimal), ByVal lstWorking As List(Of Date), ByVal lstShift As List(Of Decimal), ByVal table As AttendanceCommon.TABLE_NAME) As Boolean
#End Region

#Region "IPORTAL - View bảng công"

        <OperationContract()>
        Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
#End Region

#Region "LOG"
        <OperationContract()>
        Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)
        <OperationContract()>
        Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
#End Region

        <OperationContract()>
        Function IMPORT_AT_YEARLEAVE_EDIT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean


#Region "AT_PROJECT_TITLE"

        <OperationContract()>
        Function GetAT_PROJECT_TITLE(ByVal _filter As AT_PROJECT_TITLEDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_TITLEDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_TITLE(ByVal objATFML As AT_PROJECT_TITLEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_TITLE(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "AT_PROJECT"

        <OperationContract()>
        Function GetAT_PROJECT(ByVal _filter As AT_PROJECTDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECTDTO)
        <OperationContract()>
        Function InsertAT_PROJECT(ByVal objATFML As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT(ByVal objATFML As AT_PROJECTDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT(ByVal objATFML As AT_PROJECTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateOtherList(ByVal objOtherList As OT_OTHERLIST_DTO) As Boolean

#End Region

#Region "AT_PROJECT_WORK"

        <OperationContract()>
        Function GetAT_PROJECT_WORK(ByVal _filter As AT_PROJECT_WORKDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_WORKDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_WORK(ByVal objATFML As AT_PROJECT_WORKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_WORK(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "AT_PROJECT_EMP"

        <OperationContract()>
        Function GetAT_PROJECT_EMP(ByVal _filter As AT_PROJECT_EMPDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_PROJECT_EMPDTO)
        <OperationContract()>
        Function InsertAT_PROJECT_EMP(ByVal objATFML As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAT_PROJECT_EMP(ByVal objATFML As AT_PROJECT_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_PROJECT_EMP(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Dashboard"
        <OperationContract()>
        Function GetStatisticTotalWorking(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeOff(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeOtByOrg(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTimeProject(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region
#Region "quan ly cham cong bu tru"
        <OperationContract()>
        Function InsertOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyOffSettingTime(ByVal objOffSetting As AT_OFFFSETTINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
#End Region
#Region "Portal quan ly nghi phep, nghi bu"
        <OperationContract()>
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO

        <OperationContract()>
        Function GET_TIME_MANUAL(ByVal _filter As TotalDayOffDTO, Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO

        <OperationContract()>
        Function GetHistoryLeave(ByVal _filter As HistoryLeaveDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "REGDATE DESC", Optional ByVal log As UserLog = Nothing) As List(Of HistoryLeaveDTO)

#End Region
#Region "AT_PORTAL_REG_LIST"
        <OperationContract()>
        Function GetLeaveRegistrationList(ByVal _filter As AT_PORTAL_REG_DTO,
                                   Optional ByRef Total As Integer = 0,
                                   Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function CheckTimeSheetApproveVerify(ByVal obj As List(Of AT_PROCESS_DTO), ByVal type As String, ByRef itemError As AT_PROCESS_DTO) As Boolean
        <OperationContract()>
        Function DeletePortalReg(ByVal lstId As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ApprovePortalRegList(ByVal obj As List(Of AT_PORTAL_REG_LIST_DTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeInfor(ByVal P_EmpId As Decimal?, ByVal P_Org_ID As Decimal?, Optional ByVal fromDate As Date? = Nothing) As DataTable
        <OperationContract()>
        Function GetLeaveRegistrationById(ByVal _filter As AT_PORTAL_REG_DTO) As AT_PORTAL_REG_DTO
        <OperationContract()>
        Function GetLeaveEmpDetail(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date, Optional ByVal isUpdate As Boolean = False) As List(Of LEAVE_DETAIL_EMP_DTO)
        <OperationContract()>
        Function GetLeaveRegistrationDetailById(ByVal listId As Decimal) As List(Of AT_PORTAL_REG_DTO)
        <OperationContract()>
        Function InsertPortalRegList(ByVal obj As AT_PORTAL_REG_LIST_DTO, ByVal lstObjDetail As List(Of AT_PORTAL_REG_DTO), ByVal log As UserLog, ByRef gID As Decimal, ByRef itemExist As AT_PORTAL_REG_DTO, ByRef isOverAnnualLeave As Boolean) As Boolean
        <OperationContract()>
        Function ModifyPortalRegList(ByVal obj As AT_PORTAL_REG_DTO, ByVal itemRegister As AT_PORTAL_REG_DTO, ByVal log As UserLog) As Boolean
#End Region
#Region "SHIFT CYCLE"
        <OperationContract()>
        Function GetShiftCycle(ByVal _filter As AT_SHIFTCYCLEDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of AT_SHIFTCYCLEDTO)
        <OperationContract()>
        Function GetEmployeeShifts(ByVal employee_Id As Decimal, ByVal fromDate As Date, ByVal toDate As Date) As List(Of EMPLOYEE_SHIFT_DTO)
#End Region

        <OperationContract()>
        Function IMPORT_AT_OT_REGISTRATION(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetOTCoeffOver(ByVal _regDate As Date) As DataTable

        <OperationContract()>
        Function OtGetValueOfMonth(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal

        <OperationContract()>
        Function GetOTReigistrationByID(ByVal id As Decimal) As AT_OT_REGISTRATIONDTO

        <OperationContract()>
        Function OtGetValueOfYear(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date, ByVal P_HOURS As Decimal, Optional ByVal P_TYPE_OT As Decimal = 0) As Decimal

        <OperationContract()>
        Function ModifyPortalOtReg(ByVal objOT As AT_OT_REGISTRATIONDTO, ByVal P_HS_OT As String, ByVal P_USERNAME As String, ByVal P_ORG_OT_ID As Integer) As Decimal

        <OperationContract()>
        Function EXPORT_AT_OT_REGISTRATION(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function CHECK_OT_REGISTRATION_EXIT(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_HESO As String) As Integer

        <OperationContract()>
        Function CHECK_LEAVE_EXITS(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_MANUAL_ID As Decimal, ByVal P_CA As Decimal) As Integer

        <OperationContract()>
        Function CHECK_LEAVE_SHEET(ByVal P_EMP_CODE As String, ByVal P_DATE As String, ByVal P_CA As Decimal) As Decimal

        <OperationContract()>
        Function GET_PE_ASSESS_MESS(ByVal EMP As Decimal?) As DataTable

        <OperationContract()>
        Function PRS_DASHBOARD_BY_APPROVE(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_PROCESS_TYPE As String) As DataTable

        <OperationContract()>
        Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer

        <OperationContract()>
        Function INPORT_NB(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_PERIOD_ID As Integer) As Boolean

        <OperationContract()>
        Function INPORT_NB_PREV(ByVal P_DOCXML As String, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean

        <OperationContract()>
        Function CheckOverMonth(ByVal employeeId As Decimal, ByVal orgId As Decimal _
                                                                , ByVal FromDate As DateTime,
                                                                 ByVal ToDate As DateTime) As Decimal

        <OperationContract()>
        Function GetOrgIdLevel2(ByVal EmpId As Decimal) As Decimal

        <OperationContract()>
        Function GetProcessApprovedStatusByEmpAndId(ByVal EmpId As Decimal, ByVal LeaveSheetId As Decimal) As PROCESS_APPROVED_STATUS

        <OperationContract()>
        Function GetLeaveSheetByEmpAndLeave(ByVal EmpId As Decimal, ByVal LeaveSheetId As Decimal) As AT_LEAVESHEET

        <OperationContract()>
        Function GetAT_Coeff_OT_Exception(ByVal _filter As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIME_EXCEPTIONDTO)
        <OperationContract()>
        Function InsertAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function UpdateAT_Coeff_OT_Exception(ByVal obj As AT_COEFF_OVERTIME_EXCEPTIONDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteAT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ActiveOrDeActive_AT_Coeff_OT_Exception(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function GetAT_Time_Period(ByVal _filter As AT_TIME_PERIODDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_PERIODDTO)
        <OperationContract()>
        Function InsertATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyATTimePeriod(ByVal objTitle As AT_TIME_PERIODDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteATTimePeriod(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CheckATTimePeriod(ByVal pdate As Date, ByVal objEmp As Decimal, ByVal id As Decimal) As Boolean

        <OperationContract()>
        Function GET_AT_TIMEWORK(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_AT_TIMEWORK_EMPLOYEE(ByVal P_ID As Decimal, ByVal P_HE_SO As Decimal?) As DataTable

        <OperationContract()>
        Function GET_AT_TIMEWORKSTANDARD(ByVal P_YEAR As Decimal,
                                            ByVal P_OBJ_EMP As Decimal,
                                            ByVal P_NOT_T7 As Decimal,
                                            ByVal P_NOT_CN As Decimal,
                                            ByVal P_NOT_T7_2 As Decimal,
                                            ByVal P_NOT_2T7 As Decimal,
                                            ByVal P_TC As Decimal?,
                                            ByVal P_CMD As Decimal?,
                                            ByVal P_HE_SO As Decimal?) As DataTable

        <OperationContract()>
        Function GetAT_Time_WorkStandard(ByVal _filter As AT_TIME_WORKSTANDARDDTO,
                                         ByVal _param As ParamDTO,
                                         Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of AT_TIME_WORKSTANDARDDTO)

        <OperationContract()>
        Function InsertATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyATTime_WorkStandard(ByVal objTitle As AT_TIME_WORKSTANDARDDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteATTime_WorkStandard(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CheckATTime_WorkStandard(ByVal year As Decimal, ByVal orgId As Decimal, ByVal objEmp As Decimal, ByVal id As Decimal, ByVal objAttendance As Decimal) As Boolean

        <OperationContract()>
        Function CheckATTime_WorkStandardEmp(ByVal year As Decimal, ByVal enpID As Decimal, ByVal id As Decimal) As Boolean

        <OperationContract()>
        Function GetAT_Time_WorkStandardID(ByVal id As Decimal) As AT_TIME_WORKSTANDARDDTO

        <OperationContract()>
        Function CheckCloseOrg(ByVal year As Decimal, ByVal month As Decimal, ByVal orgId As Decimal) As Boolean

        <OperationContract()>
        Function GetAT_COEFF_OVERTIME(ByVal _filter As AT_COEFF_OVERTIMEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COEFF_OVERTIMEDTO)

        <OperationContract()>
        Function InsertAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyAT_COEFF_OVERTIME(ByVal objTitle As AT_COEFF_OVERTIMEDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAT_COEFF_OVERTIME(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CheckAT_COEFF_OVERTIME(ByVal pDate As Date, ByVal id As Decimal) As Boolean
#Region "CONFIRM DECLARES OT"
        <OperationContract()>
        Function CHANGE_CONFIRM_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CONFIRM_DECLARES_OT(ByVal params As AT_OT_REGISTRATIONDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CALCAULATE_CONFIRM_DECLARES_OT(ByVal paramsOT As AT_OT_REGISTRATIONDTO, ByVal log As UserLog, Optional ByVal params As ParamDTO = Nothing) As Boolean
#End Region
#Region "List ANNUALLEAVE"
        <OperationContract()>
        Function GetAnnualLeave(ByVal _filter As AT_ANNUAL_LEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVEDTO)

        <OperationContract()>
        Function DeleteAnnualLeave(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckAnnualLeave_DATE(ByVal obj As AT_ANNUAL_LEAVEDTO) As Boolean

        <OperationContract()>
        Function ModifyAnnualLeave(ByVal objTitle As AT_ANNUAL_LEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

#Region "List ANNUALLEAVE ORG"
        <OperationContract()>
        Function GetAnnualLeaveOrg(ByVal _filter As AT_ANNUAL_LEAVE_ORGDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ANNUAL_LEAVE_ORGDTO)

        <OperationContract()>
        Function DeleteAnnualLeaveOrg(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAnnualLeaveOrg(ByVal objTitle As AT_ANNUAL_LEAVE_ORGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckAnnualLeaveOrg_DATE(ByVal obj As AT_ANNUAL_LEAVE_ORGDTO) As Boolean

#End Region

#Region "AT SETUP ELEAVE"
        <OperationContract()>
        Function GetAtSetupELeave(ByVal _filter As AT_SETUP_ELEAVEDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SETUP_ELEAVEDTO)

        <OperationContract()>
        Function DeleteAtSetupELeave(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAtSetupELeave(ByVal objTitle As AT_SETUP_ELEAVEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

#Region "AT SETUP ELEAVE"
        <OperationContract()>
        Function GetAtSeniority(ByVal _filter As AT_SENIORITYDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_SENIORITYDTO)

        <OperationContract()>
        Function DeleteAtSeniority(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAtSeniority(ByVal objTitle As AT_SENIORITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckAtSeniority_DATE(ByVal obj As AT_SENIORITYDTO) As Boolean

#End Region

#Region "WAGE_OFFSET"
        <OperationContract()>
        Function GetWageOffset(ByVal _filter As AT_WAGEOFFSET_EMPDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_WAGEOFFSET_EMPDTO)
        <OperationContract()>
        Function GetWageOffsetById(ByVal _id As Decimal?) As AT_WAGEOFFSET_EMPDTO

        <OperationContract()>
        Function InSertWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWageOffset(ByVal objWageOffset As AT_WAGEOFFSET_EMPDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateWageOffset(ByVal _validate As AT_WAGEOFFSET_EMPDTO)

        <OperationContract()>
        Function DeleteWageOffset(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "List TOXIC LEAVE EMP"
        <OperationContract()>
        Function GetAtToxicLeaveEmp(ByVal _filter As AT_TOXIC_LEAVE_EMPDTO,
                                    ByVal log As UserLog,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TOXIC_LEAVE_EMPDTO)

        <OperationContract()>
        Function DeleteAtToxicLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckAtToxicLeaveEmp_DATE(ByVal obj As AT_TOXIC_LEAVE_EMPDTO) As Boolean

        <OperationContract()>
        Function ModifyAtToxicLeaveEmp(ByVal objTitle As AT_TOXIC_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

#Region "List ADVANCE LEAVE EMP"
        <OperationContract()>
        Function GetAtAdvanceLeaveEmp(ByVal _filter As AT_ADVANCE_LEAVE_EMPDTO,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ADVANCE_LEAVE_EMPDTO)

        <OperationContract()>
        Function DeleteAtAdvanceLeaveEmp(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function InsertAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckAtAdvanceLeaveEmp_DATE(ByVal obj As AT_ADVANCE_LEAVE_EMPDTO) As Boolean

        <OperationContract()>
        Function ModifyAtAdvanceLeaveEmp(ByVal objTitle As AT_ADVANCE_LEAVE_EMPDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

        <OperationContract()>
        Function GetAtConcludeAnnaul(ByVal _filter As AT_CONCLUDE_ANNUAL_YEARDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_CONCLUDE_ANNUAL_YEARDTO)


        <OperationContract()>
        Function GetAtCompensation(ByVal _filter As AT_COMPENSATION_YEARDTO,
                                  ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_COMPENSATION_YEARDTO)



        <OperationContract()>
        Function CAL_CONCLUDE_ANNUAL_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean

        <OperationContract()>
        Function CAL_COMPENSATION_YEAR(ByVal _param As ParamDTO, ByVal log As UserLog, ByVal P_YEAR As Integer) As Boolean

        <OperationContract()>
        Function IMPORT_AT_COMPENSATION_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCompensationYear(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function IMPORT_AT_CONCLUDE_YEAR(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteConcludeYear(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UPDATE_CONCLUDE_ANNUAL_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_ANNUAL As Decimal, ByVal P_ANNUAL_TRANFER As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UPDATE_COMPENSATION_YEAR(ByVal P_ID As Integer, ByVal P_YEAR_NB As Decimal, ByVal P_NB_TRANFER As Decimal, ByVal P_NB_EDIT As Decimal, ByVal log As UserLog) As Boolean

#Region "Assign Emp to Calendar"
        <OperationContract()>
        Function GetEmployeeNotByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO)


        <OperationContract()>
        Function GetEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of AT_ASSIGNEMP_CALENDARDTO)
        <OperationContract()>
        Function InsertEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO, ByVal log As UserLog) As Decimal

        <OperationContract()>
        Function DeleteEmployeeByCalendarID(ByVal _filter As AT_ASSIGNEMP_CALENDARDTO, ByVal log As UserLog) As Decimal

#End Region

        <OperationContract()>
        Function PRS_Get_TREvaluateCourse_Nofi(ByVal param As AT_PORTAL_REG_DTO,
                                         Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function CREATE_WORKNIGHT(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function GET_WORKNIGHT(ByVal param As AT_WORKNIGHTDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function InsertWorkNightByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal,
                                           ByVal emp_obj_id As Decimal,
                                           ByVal start_date As Date,
                                           ByVal end_date As Date,
                                           ByVal log As UserLog) As Boolean

#Region "Khóa bảng công nhân viên - AT_TIMESHEET_LOCK"
        <OperationContract()>
        Function GetAtTimesheetLock(ByVal _filter As AT_TIMESHEET_LOCKDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_TIMESHEET_LOCKDTO)
        <OperationContract()>
        Function GetAtTimesheetLockById(ByVal _id As Decimal?) As AT_TIMESHEET_LOCKDTO

        <OperationContract()>
        Function InSertAtTimesheetLock(ByVal objWageOffset As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAtTimesheetLock(ByVal objWageOffset As AT_TIMESHEET_LOCKDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAtTimesheetLock(ByVal _validate As AT_TIMESHEET_LOCKDTO)

        <OperationContract()>
        Function DeleteAtTimesheetLock(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Leave Payment"
        <OperationContract()>
        Function GetLeavePayments(ByVal _filter As AT_LEAVE_PAYMENTDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of AT_LEAVE_PAYMENTDTO)
        <OperationContract()>
        Function GetLeavePaymentById(ByVal _id As Decimal?) As AT_LEAVE_PAYMENTDTO

        <OperationContract()>
        Function InSertLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyLeavePayment(ByVal obj As AT_LEAVE_PAYMENTDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateLeavePayment(ByVal _validate As AT_LEAVE_PAYMENTDTO) As Boolean

        <OperationContract()>
        Function DeleteLeavePayment(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetLeavePaymentSal(ByVal empID As Decimal, ByVal effect_date As Date) As DataTable

#End Region

#Region "Timesheet"
        <OperationContract()>
        Function GetTimesheetDetail(ByVal PeriodId As Decimal, ByVal EmployeeId As Decimal, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function GetTimesheetDetail_portal(ByVal empID As Decimal, ByVal year As Decimal, ByVal month As Decimal) As DataTable

#End Region
#Region "Phê duyệt giải trình công portal"
        <OperationContract()>
        Function Get_At_Late_Combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                          Optional ByVal PageIndex As Integer = 0,
                                         Optional ByVal PageSize As Integer = Integer.MaxValue,
                                         Optional ByRef Total As Integer = 0,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LATE_COMBACKOUTDTO)
#End Region

#Region "Khác"
        <OperationContract()>
        Function CheckPeriodIsLock(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As Boolean
#End Region
        <OperationContract()>
        Function PRS_API_GETNOTIFICATION(ByVal param As SE_NOTIFICATIONDTO,
                                        Optional ByVal UserID As Integer = 0,
                                         Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function CheckValidateApprove(ByVal EMPLOYEE_ID As Decimal, ByVal PROCESS_TYPE As String, ByVal ID As Decimal, ByVal P_START_DATE As Date, ByVal P_END_DATE As Date) As VALIDATE_DTO

#Region "Danh mục Lý do giải trình công"
        <OperationContract()>
        Function Get_AT_REASON_LIST(ByVal _filter As AT_REASON_LIST_DTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_REASON_LIST_DTO)
        <OperationContract()>
        Function Insert_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function Modify_AT_REASON_LIST(ByVal objTitle As AT_REASON_LIST_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function Active_AT_REASON_LIST(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal status As Decimal) As Boolean

        <OperationContract()>
        Function Delete_AT_REASON_LIST(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

        <OperationContract()>
        Function IMPORT_AT_TOXICLEAVE_EMP(ByVal P_XML As String, ByVal P_USERNAME As String) As Boolean
    End Interface

End Namespace
