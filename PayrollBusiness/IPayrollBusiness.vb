Imports Framework.Data
Imports PayrollDAL
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace PayrollBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IPayrollBusiness
#Region "Allowance lisst"

        <OperationContract()>
        Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)


#End Region

#Region "Allowance"
        <OperationContract()>
        Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)

        <OperationContract()>
        Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Common"
        <OperationContract()>
        Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal), ByVal orgID As Decimal?, ByVal isDissolve As Decimal?, ByVal periodID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal), ByVal orgID As Decimal?, ByVal isDissolve As Decimal?, ByVal periodID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean
        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region
#Region "Calculate Salary"
        <OperationContract()>
        Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                    ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet
#End Region
#Region "Import Bonus"
        <OperationContract()>
        Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
#End Region
#Region "Import Salary"
        <OperationContract()>
        Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        <OperationContract()>
        Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        <OperationContract()>
        Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        <OperationContract()>
        Function GetSalaryList() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        <OperationContract()>
        Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        <OperationContract()>
        Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
#End Region
#Region "PA_SETUP_MR_BQN"
        <OperationContract()>
        Function GET_PA_SETUP_MR_BQN(ByVal _filter As PA_SETUP_MR_BQN_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_MR_BQN_DTO)

        <OperationContract()>
        Function INSERT_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_SETUP_MR_BQN(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO) As Boolean
#End Region
#Region "PA_TARGET_STORE"
        <OperationContract()>
        Function GET_PA_TARGET_STORE(ByVal _filter As PA_TARGET_STOREDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_STOREDTO)

        <OperationContract()>
        Function INSERT_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_TARGET_STORE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO) As Boolean
#End Region

#Region "PA_Setup_HeSoMR_NV_QLCH"
        <OperationContract()>
        Function GET_PA_SETUP_HESOMR_NV_QLCH(ByVal _filter As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)

        <OperationContract()>
        Function INSERT_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_SETUP_HESOMR_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO) As Boolean
#End Region

#Region "PA_SETUP_RATE_DTTT"
        <OperationContract()>
        Function GET_PA_SETUP_RATE_DTTT(ByVal _filter As PA_SETUP_RATE_DTTT_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTTT_DTO)

        <OperationContract()>
        Function INSERT_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_SETUP_RATE_DTTT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO) As Boolean
#End Region
#Region "PA_SETUP_LDTT_NV_QLCH"
        <OperationContract()>
        Function GET_PA_SETUP_LDTT_NV_QLCH(ByVal _filter As PA_SETUP_LDTTT_NV_QLCH_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)

        <OperationContract()>
        Function InsertPA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPA_SETUP_LDTTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePA_SETUP_LDTT_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidatePA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO) As Boolean

        <OperationContract()>
        Function GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT() As DataSet

        <OperationContract()>
        Function IMPORT_PA_SETUP_LDTT_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region
#Region "PA_SETUP_INDEX"
        <OperationContract()>
        Function GET_PA_SETUP_INDEX(ByVal _filter As PA_SETUP_INDEX_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_INDEX_DTO)

        <OperationContract()>
        Function INSERT_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_SETUP_INDEX(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO) As Boolean

        <OperationContract()>
        Function EXPORT_PA_SETUP_INDEX(ByVal sLang As String) As DataSet

        <OperationContract()>
        Function IMPORT_PA_SETUP_INDEX(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region
#Region "PA_SETUP_HSTDT"
        <OperationContract()>
        Function GET_PA_SETUP_HSTDT(ByVal _filter As PA_SETUP_HSTDT_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HSTDT_DTO)

        <OperationContract()>
        Function INSERT_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function MODIFY_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DELETE_PA_SETUP_HSTDT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function VALIDATE_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO) As Boolean
#End Region
#Region "Hold Salary"

        <OperationContract()>
        Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)

        <OperationContract()>
        Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
#End Region

#Region "Taxation List"

        <OperationContract()>
        Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO)

        <OperationContract()>
        Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Payment List"

        <OperationContract()>
        Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function ActiveSystemCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Object Salary"
        <OperationContract()>
        Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean

        <OperationContract()>
        Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Period List"
        <OperationContract()>
        Function GetPeriodList(ByVal _filter As ATPeriodDTO,
                               ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "YEAR DESC,MONTH ASC") As List(Of ATPeriodDTO)
        <OperationContract()>
        Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)

        <OperationContract()>
        Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function checkDup(ByVal obj As NormDTO) As Boolean
        <OperationContract()>
        Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
#End Region

#Region "SET UP COMPLETION KPI SHOP MANAGER"
        <OperationContract()>
        Function GetSetupCompletion_KPI_ShopManager(ByVal _filter As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO)
        <OperationContract()>
        Function GetSetupRate(ByVal _filter As PA_SETUP_RATE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTO)
        <OperationContract()>
        Function CheckBrandAndShopType(ByVal brandID As Integer, ByVal shopID As Integer) As Boolean
        <OperationContract()>
        Function GetSetupBonusKpiProductType(ByVal _filter As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO)
        <OperationContract()>
        Function InsertSetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertSetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertSetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSetupCompletion_KPI_ShopManager(ByVal _validate As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO) As Boolean
        <OperationContract()>
        Function ValidateSetupRate(ByVal _validate As PA_SETUP_RATE_DTO) As Boolean
        <OperationContract()>
        Function ValidateSetupBonusKpiProductType(ByVal _validate As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO) As Boolean
        <OperationContract()>
        Function DeleteSetupCompletion_KPI_ShopManager(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteSetupRate(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteSetupBonusKpiProductType(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal sLang As String) As DataSet

        <OperationContract()>
        Function IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        <OperationContract()>
        Function IMPORT_PAYROLL_SHEET_SUM(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        <OperationContract()>
        Function IMPORT_PAYROLL_ADVANCE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        <OperationContract()>
        Function GET_PAYROLL_SHEET_SUM_IMPORT(ByVal P_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_USERNAME As String) As DataSet


#End Region

#Region "SET UP SHOP GRADE"
        <OperationContract()>
        Function GetSetupShopGrade(ByVal _filter As PA_SETUP_SHOP_GRADEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_SHOP_GRADEDTO)
        <OperationContract()>
        Function InsertSetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSetupShopGrade(ByVal _validate As PA_SETUP_SHOP_GRADEDTO) As Boolean
        <OperationContract()>
        Function DeleteSetupShopGrade(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "SET UP FRAMEWORK OFFICE"
        <OperationContract()>
        Function GetGroup_Tilte() As DataTable
        <OperationContract()>
        Function GetHU_TITLE() As DataTable
        <OperationContract()>
        Function GetSetupFrameWorkOffice(ByVal _filter As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO)
        <OperationContract()>
        Function InsertSetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSetupFrameWorkOffice(ByVal _validate As PA_SETUP_FRAMEWORK_OFFICEDTO) As Boolean
        <OperationContract()>
        Function DeleteSetupFrameWorkOffice(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "SET UP FRAMEWORK KPI"
        <OperationContract()>
        Function GET_KPI_IMPORT() As DataSet
        <OperationContract()>
        Function GetSetupFrameWorkKPI(ByVal _filter As PA_SETUP_FRAMEWORK_KPIDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_KPIDTO)
        <OperationContract()>
        Function InsertSetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSetupFrameWorkKPI(ByVal _validate As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        <OperationContract()>
        Function ValidateFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        <OperationContract()>
        Function DeleteSetupFrameWorkKPI(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "SET UP FRAMEWORK ECD"
        <OperationContract()>
        Function GetSetupFrameWorkECD(ByVal _filter As PA_SETUP_FRAMEWORK_ECDDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_ECDDTO)
        <OperationContract()>
        Function InsertSetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSetupFrameWorkECD(ByVal _validate As PA_SETUP_FRAMEWORK_ECDDTO) As Boolean
        <OperationContract()>
        Function DeleteSetupFrameWorkECD(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GET_FRAMEWORK_ECD_IMPORT_DATA() As DataSet
        <OperationContract()>
        Function IMPORT_FRAMEWORK_ECD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region

#Region "Work Standard"
        <OperationContract()>
        Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)
        <OperationContract()>
        Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        <OperationContract()>
        Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateWorkStandard(ByVal objPeriod As Work_StandardDTO) As Boolean
        <OperationContract()>
        Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
#End Region

#Region "List Salary"
        <OperationContract()>
        Function EXPORT_CH(ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function DeleteNorm(ByVal lstDelete As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        <OperationContract()>
        Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function GetAllFomulerGroup() As List(Of PAFomulerGroup)
        <OperationContract()>
        Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "IDX ASC, CREATED_DATE desc") As List(Of PAFomulerGroup)
        <OperationContract()>
        Function GetAllNorm(ByVal _filter As NormDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ID") As List(Of NormDTO)

        <OperationContract()>
        Function InsertPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePA_AOMS_TOM_MNG(ByVal lstDelete As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetExportAomsTom() As DataTable

        <OperationContract()>
        Function CheckPA_AOMS_TOMExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean

        <OperationContract()>
        Function CheckPA_AOMS_TOMExits_EF_EX(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean

        <OperationContract()>
        Function GetPA_AOMS_TOM_MNG(ByVal _filter As PA_AOMS_TOM_MNG_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_AOMS_TOM_MNG_DTO)
        <OperationContract()>
        Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        <OperationContract()>
        Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListSalColunm(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        <OperationContract()>
        Function CopyFomuler(ByRef F_ID As Decimal, ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean
        <OperationContract()>
        Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function IMPORT_PA_AOMS_TOM_MNG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

        <OperationContract()>
        Function IMPORT_PA_EMP_FORMULER(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

        <OperationContract()>
        Function IMPORT_PA_SETUP_HESOMR_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean


#End Region

#Region "Salary Group"
        <OperationContract()>
        Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)
        <OperationContract()>
        Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetEffectSalaryGroup() As SalaryGroupDTO
        <OperationContract()>
        Function GET_IMPORT_QUYLUONG() As DataSet
        <OperationContract()>
        Function IMPORT_QUYLUONG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region

#Region "Salary Level"

        <OperationContract()>
        Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        <OperationContract()>
        Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevel(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean

#End Region

#Region "Salary Level Unilever"

        <OperationContract()>
        Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        <OperationContract()>
        Function GetSalaryLevelCombo(ByVal salGroupID As Decimal, ByVal isBlank As Boolean, Optional ByVal other_Use As String = "ALL") As DataTable
        <OperationContract()>
        Function InsertSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevel_Unilever(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevel_Unilever(ByVal lstSalaryLevel() As SalaryLevelDTO) As Boolean

#End Region

#Region "Salary Rank"

        <OperationContract()>
        Function GetSalaryRank(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        <OperationContract()>
        Function GetSalaryRank_Unilever(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        <OperationContract()>
        Function GetSalaryRankCombo(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryRank(ByVal objSalaryRank As SalaryRankDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryRank(ByVal lstSalaryRank() As SalaryRankDTO) As Boolean

#End Region

#Region "Incentive Rank"

        <OperationContract()>
        Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        <OperationContract()>
        Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TO_TARGET, CREATED_DATE desc") As List(Of IncentiveRankDetailDTO)

        <OperationContract()>
        Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        <OperationContract()>
        Function GetIncentiveRankIdIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As IncentiveRankDTO
        <OperationContract()>
        Function InsertIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertIncentiveListRankDetail(ByVal objIncentiveRankDetail As List(Of IncentiveRankDetailDTO),
                                   ByVal log As UserLog, ByRef gID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRankIncludeDetail(ByVal objIncentive As IncentiveRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateIncentiveRank(ByVal _validate As IncentiveRankDTO)
        <OperationContract()>
        Function ActiveIncentiveRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteIncentiveRank(ByVal lstIncentiveRank As List(Of IncentiveRankDTO)) As Boolean
        <OperationContract()>
        Function ActiveIncentiveRankDetail(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteIncentiveRankDetail(ByVal lstIncentiveRank() As IncentiveRankDetailDTO) As Boolean

#End Region

#Region "SALARYTYPE_GROUP"

        <OperationContract()>
        Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO)
        <OperationContract()>
        Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO)

        <OperationContract()>
        Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSalaryType_Group(ByVal lstSalaryType_Group() As SalaryType_GroupDTO) As Boolean

#End Region

#Region "Salary Type"
        <OperationContract()>
        Function DeleteSalaryType(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ModifySalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateSalaryType(ByVal objSalaryType As SalaryTypeDTO) As Boolean

        <OperationContract()>
        Function InsertSalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetSalaryType(ByVal _filter As SalaryTypeDTO, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryTypeDTO)

        <OperationContract()>
        Function ActiveSalaryType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function GetSalaryTypebyIncentive(ByVal incentive As Integer) As List(Of SalaryTypeDTO)
        <OperationContract()>
        Function GetPaymentSourcesbyYear(ByVal year As Integer) As List(Of PaymentSourcesDTO)
        <OperationContract()>
        Function GetListOrgBonus() As List(Of OrgBonusDTO)
#End Region

#Region "Salary List"

        <OperationContract()>
        Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function InsertPA_SAL_MAPPING(ByVal objListSal As PA_SALARY_FUND_MAPPINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        <OperationContract()>
        Function InsertListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyListSal(ByVal objListSalaries As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        <OperationContract()>
        Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)
        <OperationContract()>
        Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)

        <OperationContract()>
        Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean

#End Region


#Region "Cos Center"
        <OperationContract()>
        Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO)
        <OperationContract()>
        Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        <OperationContract()>
        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

        <OperationContract()>
        Function TestService(ByVal str As String) As String

        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean

        <OperationContract()>
        Function IMPORT_PA_SETUP_FRAMEWORK_OFFICE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

#Region "IPORTAL - View phiếu lương"
        <OperationContract()>
        Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
#End Region

#Region " Org Bonus"
        <OperationContract()>
        Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        <OperationContract()>
        Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateOrgBonus(ByVal objTitle As OrgBonusDTO) As Boolean
#End Region
#Region " Payment Sources"
        <OperationContract()>
        Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of PaymentSourcesDTO)
        <OperationContract()>
        Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean

#End Region
#Region " Work Factor"
        <OperationContract()>
        Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of WorkFactorDTO)
        <OperationContract()>
        Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateWorkFactor(ByVal objTitle As WorkFactorDTO) As Boolean
#End Region
#Region "SalaryFund List"

        <OperationContract()>
        Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO

        <OperationContract()>
        Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean

#End Region

#Region "TitleCost List"

        <OperationContract()>
        Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)

        <OperationContract()>
        Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean
#End Region


#Region "SalaryPlanning"
        <OperationContract()>
        Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                   ByVal _param As ParamDTO,
                                   Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO

        <OperationContract()>
        Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSalaryPlanning(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetSalaryPlanningImport(ByVal org_id As Decimal, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GET_EXPORT_PA_EMP_FORMULER(ByVal org_id As Decimal, ByVal IS_DISSOLVE As Decimal, ByVal log As UserLog) As DataSet

#End Region

#Region "SalaryTracker"

        <OperationContract()>
        Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

        <OperationContract()>
        Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

#End Region
#Region "Get List Manning"
        <OperationContract()>
        Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
#End Region

#Region "Dashboard Payroll"
        <OperationContract()>
        Function GetStatisticSalary(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryRange(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryIncome(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryAverage(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region

#Region "Dashboard Planning"
        <OperationContract()>
        Function GetStatisticSalaryTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet
        <OperationContract()>
        Function GetStatisticEmployeeTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet
#End Region

#Region "Validate Combobox"
        <OperationContract()>
        Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
#End Region

#Region "PA_SYSTEM_CRITERIA"

        <OperationContract()>
        Function GetPA_SYSTEM_CRITERIA(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SYSTEM_CRITERIADTO)
        <OperationContract()>
        Function InsertPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function DeletePA_SYSTEM_CRITERIA(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateList_PA_SYSTEM_CRITERIA(ByVal _validate As PA_SYSTEM_CRITERIADTO)
#End Region

#Region "SALE COMMISION"
        <OperationContract()>
        Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO)

        <OperationContract()>
        Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifySaleCommision(ByVal objSalaryType As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

#End Region

#Region "Delegacy tax"
        <OperationContract()>
        Function GetDelegacyTax(ByVal _filter As PA_Delegacy_TaxDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_Delegacy_TaxDTO)

        <OperationContract()>
        Function ModifiDelegacy(ByVal lstObj As List(Of PA_Delegacy_TaxDTO), ByVal userLog As UserLog) As Boolean

#End Region
#Region "PA_ADDTAX"
        <OperationContract()>
        Function GetPA_ADDTAX(ByVal _filter As PA_ADDTAXDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_ADDTAXDTO)
        <OperationContract()>
        Function GetPA_ADDTAX_ByID(ByVal _id As Decimal?) As PA_ADDTAXDTO
        <OperationContract()>
        Function InsertPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GET_IMPORT_PA_ADDTAX() As DataSet
        <OperationContract()>
        Function GET_IMPORT_PA_STORE_DTTD() As DataSet

        <OperationContract()>
        Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        <OperationContract()>
        Function SAVE_IMPORT_PA_ADDTAX(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IMPORT_CH(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function SAVE_IMPORT_PA_STORE_DTTD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidatePA_ADDTAX(ByVal _objData As PA_ADDTAXDTO) As Boolean
#End Region

#Region "PA Store DTTĐ"
        <OperationContract()>
        Function GetPA_STORE_DTTD(ByVal _filter As PA_STORE_DTTDDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_DTTDDTO)

        <OperationContract()>
        Function CalculateStoreDTTD(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_OBJ_EMP As Decimal, ByVal P_ENDDATE As Date, ByVal _log As UserLog) As Boolean
#End Region


#Region "Benefit Seniority"
        <OperationContract()>
        Function GetLstBenefitSeniority(ByVal _filter As PaBenefitSeniorityDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PaBenefitSeniorityDTO)

        <OperationContract()>
        Function Calculate_Benefit(ByVal _period_ID As Decimal, ByVal _obj_Emp_ID As Decimal, ByVal _OrgID As Decimal, ByVal _IsDissolve As Decimal, ByVal log As UserLog) As Boolean

#End Region

#Region "Accounting Adjusting"
        <OperationContract()>
        Function GetAccountingAdjusting(ByVal _filter As PA_Accounting_AdjustingDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_AdjustingDTO)

        <OperationContract()>
        Function ModifyAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function InsertAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function DeleteAccountingAdjust(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean

#End Region

#Region "Vehicle Norm"
        <OperationContract()>
        Function GetVehicleNorm(ByVal _filter As PA_Vehicle_NormDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Vehicle_NormDTO)

        <OperationContract()>
        Function ModifyVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function InsertVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function DeleteVehicleNorm(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean
        <OperationContract()>
        Function GET_VEHICLE_NORM_IMPORT() As DataSet
        <OperationContract()>
        Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean
#End Region

#Region "PA_TARGET_DTTD_LABEL"
        <OperationContract()>
        Function GetPA_TARGET_DTTD_LABEL(ByVal _filter As PA_TARGET_DTTD_LABELDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_DTTD_LABELDTO)
        <OperationContract()>
        Function GET_IMPORT_PA_TARGET_DTTD_LABEL() As DataSet
        <OperationContract()>
        Function IMPORT_PA_TARGET_DTTD_LABEL(ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
#End Region
#Region "Accounting Time"
        <OperationContract()>
        Function GetAccountingTime(ByVal _filter As PA_Accounting_TimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_TimeDTO)

        <OperationContract()>
        Function DeleteAccountingTime(ByVal _lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Accounting Overtime"
        <OperationContract()>
        Function GetAccountingOvertime(ByVal _filter As PA_Accounting_OvertimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_OvertimeDTO)

        <OperationContract()>
        Function DeleteAccountingOvertime(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ChangeStatusAccountingOvertime(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean
#End Region

#Region "DTTD_DTPB"
        <OperationContract()>
        Function GetDTTD_DTPB(ByVal _filter As PA_DTTD_DTPB_EMPDTO, ByVal PageIndex As Integer,
                                               ByVal PageSize As Integer,
                                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_DTPB_EMPDTO)

        <OperationContract()>
        Function DeleteDTTD_DTPB(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "DTTD_ECD"
        <OperationContract()>
        Function GetDTTD_ECD(ByVal _filter As PA_DTTD_ECDDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_ECDDTO)

#End Region

#Region "PayrollSheet lock"
        <OperationContract()>
        Function GetPayroolSheetLock(ByVal _filter As PA_PayrollSheetLockDTO, ByVal lstOrg As List(Of Decimal),
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_PayrollSheetLockDTO)

        <OperationContract()>
        Function ChangeStatusPASheetLock(ByVal lstOrg As List(Of Decimal), ByVal lstEmp As List(Of Decimal), ByVal _status As Decimal,
                                           ByVal _period_id As Decimal, ByVal _log As UserLog) As Boolean

#End Region

#Region "LDT_VP"
        <OperationContract()>
        Function GetLDT_VP(ByVal _filter As PA_LDT_VPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_LDT_VPDTO)

#End Region
#Region "PA_MA_SCP_QLCH"
        <OperationContract()>
        Function GetPA_MA_SCP_QLCH(ByVal _filter As PA_MA_SCP_QLCHDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_MA_SCP_QLCHDTO)

#End Region

#Region "Accounting Subsidize"
        <OperationContract()>
        Function GetAccountingSubsidize(ByVal _filter As PA_AccountingSubsidizeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_AccountingSubsidizeDTO)

        <OperationContract()>
        Function DeleteAccountingSubsidize(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ChangeStatusAccountingSubsidize(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean
#End Region

        <OperationContract()>
        Function GetBrandRate(ByVal _filter As PA_BrandRate_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_BrandRate_DTO)
        <OperationContract()>
        Function InsertBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateBrandRate(ByVal _validate As PA_BrandRate_DTO) As Boolean
        <OperationContract()>
        Function DeleteBrandRate(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function UpdateAward13MonthPeriod(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean

#Region "PA_STORE_SUBSIDIZE"
        <OperationContract()>
        Function GetPA_STORE_SUBSIDIZE(ByVal _filter As PA_STORE_SUBSIDIZEDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_SUBSIDIZEDTO)

        <OperationContract()>
        Function ModifyPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function InsertPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function DeletePA_STORE_SUBSIDIZE(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidatePA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean
        <OperationContract()>
        Function GET_PA_STORE_SUBSIDIZE_IMPORT() As DataSet
        <OperationContract()>
        Function Get_Brand_Name(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
        <OperationContract()>
        Function Get_Rate(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
#End Region

#Region "PA_MANAGE_DTTD_DAILY"

        <OperationContract()>
        Function GetManageDTTDDaily(ByVal _filter As PA_ManageDTTDDailyDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_ManageDTTDDailyDTO)

        <OperationContract()>
        Function GET_IMPORT_DTTD_DAILY() As DataSet

        <OperationContract()>
        Function IMPORT_DTTD_DAILY(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean
#End Region

#Region "PA_Salary_Detention"
        <OperationContract()>
        Function GetSalaryDetentionByID(ByVal _filter As PA_Salary_DetentionDTO) As PA_Salary_DetentionDTO
        <OperationContract()>
        Function GET_PA_SALARY_DETENTION(ByVal _filter As PA_Salary_DetentionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal log As UserLog = Nothing,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Salary_DetentionDTO)
        <OperationContract()>
        Function GetPeriod(ByVal isBlank As Decimal) As DataTable
        <OperationContract()>
        Function INSERT_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean
        <OperationContract()>
        Function MODIFY_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean
        <OperationContract()>
        Function DELETE_PA_SALARY_DETENTION(ByVal lstID As List(Of Decimal)) As Boolean
#End Region


#Region "Award 13 month"
        <OperationContract()>
        Function GetAward13Month(ByVal _filter As PA_Award_13MonthDTO, ByVal lstOrg As List(Of Decimal),
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer,
                                   Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of PA_Award_13MonthDTO)

        <OperationContract()>
        Function ChangeStatusAward13Month(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteAward13Month(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateAward13Month(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CAL_AWARD_13MONTH(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function RE_CAL_AWARD_13MONTH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region


#Region "Salary PIT"
        <OperationContract()>
        Function GetListSalPITCode(ByVal _filter As PA_Salary_PITCodeDTO, ByVal lstOrg As List(Of Decimal),
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of PA_Salary_PITCodeDTO)

        <OperationContract()>
        Function ChangeStatusSalPITCode(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSalPITCode(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CAL_SAL_PITCODE(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean

#End Region


#Region "Tax Income Year"
        <OperationContract()>
        Function GetListTaxIncomeYear(ByVal _filter As PA_TAXINCOME_YEARDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_TAXINCOME_YEARDTO)

        <OperationContract()>
        Function ChangeStatusTaxIncomeYear(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteTaxIncomeYear(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateTaxIncomeYearPeriod(ByVal lstID As List(Of Decimal), ByVal _period As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function CAL_TAX_INCOME_YEAR(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean

#End Region

#Region "Setup NKL"
        <OperationContract()>
        Function GetListPaSetupNKL(ByVal _filter As PA_SETUP_NKLDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_NKLDTO)

        <OperationContract()>
        Function InsertPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeletePaSetupNKL(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidatePaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean

#End Region

        <OperationContract()>
        Function GetObj_Sal() As List(Of PAObjectSalaryDTO)

        <OperationContract()>
        Function GetFORMULER_GROUP(ByVal objSalID As Decimal) As List(Of PAObjectSalaryDTO)

        <OperationContract()>
        Function GETGROUP_EMPLOYEE_ID(ByVal titleID As Decimal) As Decimal?

        <OperationContract()>
        Function GetPA_EMP_FORMULER(ByVal _filter As PA_EMP_FORMULER_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_FORMULER_DTO)


        <OperationContract()>
        Function InsertPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePA_EMP_FORMULER(ByVal lstDelete As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckPA_EMP_FORMULERExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal TitleID As Decimal, ByVal groupTitleID As Decimal, ByVal formulerID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean

        <OperationContract()>
        Function GetPA_SUM_CH_TOM(ByVal _filter As PA_SumCHTomDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_SumCHTomDTO)

        <OperationContract()>
        Function CAL_PA_SUM_CH_TOM(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_ISDISSOLVE As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function GetHSTDTImport() As DataSet
#Region "PA_FRAME_SALARY - Khung hệ số lương chức danh"
        <OperationContract()>
        Function GetFrameSalary(ByVal sACT As String) As List(Of PA_FRAME_SALARYDTO)
        <OperationContract()>
        Function InsertFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyFrameSalaryPath(ByVal lstPath As List(Of PA_FRAME_SALARY_PATHDTO)) As Boolean

        <OperationContract()>
        Function ActiveFrameSalary(ByVal objOrganization() As PA_FRAME_SALARYDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO) As Boolean
        <OperationContract()>
        Function GetMaxId() As Decimal
        <OperationContract()>
        Function GetNameFrameSalary(ByVal org_id As Decimal) As String
#End Region
#Region "PA_FRAME_PRODUCTIVITY - Khung hệ số năng suất"
        <OperationContract()>
        Function GetFrame_Productivity(ByVal sACT As String) As List(Of PA_FRAME_PRODUCTIVITYDTO)
        <OperationContract()>
        Function InsertFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyFrame_ProductivityPath(ByVal lstPath As List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)) As Boolean

        <OperationContract()>
        Function ActiveFrame_Productivity(ByVal objOrganization() As PA_FRAME_PRODUCTIVITYDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO) As Boolean
        <OperationContract()>
        Function GetMaxIdFrame_Productivity() As Decimal
        <OperationContract()>
        Function GetNameFrame_Productivity(ByVal org_id As Decimal) As String
#End Region

#Region "Classification"
        <OperationContract()>
        Function GetPaClassifications(ByVal _filter As PA_CLASSIFICATIONDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_CLASSIFICATIONDTO)
        <OperationContract()>
        Function InsertPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeletePaClassification(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean
#End Region

#Region "Document PIT"
        <OperationContract()>
        Function GetDocumentPITs(ByVal _filter As PA_DOCUMENT_PITDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_DOCUMENT_PITDTO)

        <OperationContract()>
        Function GET_EMPLOYEE_PIT_INFO(ByVal P_EMP_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function InsertDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function ModifyDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean

        <OperationContract()>
        Function DeleteDocumentPIT(ByVal _lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean

        <OperationContract()>
        Function ChangePITPrintStatus(ByVal lstID As List(Of Decimal), ByVal _type As String, ByVal pit_type As String, ByVal userLog As UserLog) As Boolean
#End Region

#Region "Don Vi Quy Luong"
        <OperationContract()>
        Function GetDonViQuyLuong(ByVal _filter As DonViQuyLuongDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "NAME ASC") As List(Of DonViQuyLuongDTO)
        <OperationContract()>
        Function InsertDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveDonViQuyLuong(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteDonViQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO) As Boolean

        <OperationContract()>
        Function GetSalaryQuyLuong(ByVal _filter As SalaryQuyLuongDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of SalaryQuyLuongDTO)
        <OperationContract()>
        Function InsertSalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSalaryQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function LOAD_DONVI_QUYLUONG() As List(Of DonViQuyLuongDTO)
        <OperationContract()>
        Function GetEmpNotQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)

        <OperationContract()>
        Function GetEmpQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)

        <OperationContract()>
        Function InsertEmpQuyLuong(ByVal obj As EmpQuyLuongDTO, ByVal lstID As List(Of Decimal),
                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmpQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
#End Region
#Region "Payroll Advance"
        <OperationContract()>
        Function CAL_PAYROLL_ADVANCE(ByVal _period_ID As Decimal,
                                        ByVal _start_date As Date,
                                        ByVal _end_date As Date,
                                        ByVal _end_date_period As Date,
                                        ByVal _OrgID As Decimal,
                                        ByVal _IsDissolve As Decimal,
                                        ByVal _Sal As Decimal,
                                        ByVal _Nosal As Decimal,
                                        ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetPayrollAdvance(ByVal _filter As PayrollAdvanceDTO, ByVal _param As ParamDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "LOCK_NAME desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PayrollAdvanceDTO)

        <OperationContract()>
        Function DeletePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal) As Boolean
        <OperationContract()>
        Function ActivePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal _param As ParamDTO, ByVal peroid As Decimal, ByVal status As Decimal, ByVal Log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyPayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal, ByVal salAdvance As Decimal, ByVal Log As UserLog) As Boolean
#End Region
    End Interface
End Namespace


