' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Imports System.ServiceModel
Imports Framework.Data
Imports InsuranceDAL

Namespace InsuranceBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IInsuranceBusiness
#Region "Get Data Combobox"
        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetInsListChangeType() As DataTable
        <OperationContract()>
        Function GetInsListWhereHealth() As DataTable
        <OperationContract()>
        Function GetInsListEntitledType() As DataTable
#End Region

#Region "Danh mục loại điều chỉnh Hồ sơ Bảo hiểm"
        'INS_MASTERLIST	      Danh mục loại điều chỉnh Hồ sơ Bảo hiểm
        <OperationContract()>
        Function GetInsListMasterlist(ByVal username As String, ByVal type As String _
                                    , ByVal id As String _
                                    , ByVal sval1 As String _
                                    , ByVal sval2 As String _
                                    , ByVal nval1 As Double? _
                                    , ByVal nval2 As Double? _
                                    , ByVal dval1 As Date? _
                                    , ByVal dval2 As Date? _
                                    , ByVal status As Double?) As DataTable
        <OperationContract()>
        Function UpdateInsListMasterlist(ByVal username As String, ByVal type As String _
                                        , ByVal id As String _
                                        , ByVal name As String _
                                        , ByVal sval1 As String _
                                        , ByVal sval2 As String _
                                        , ByVal sval3 As String _
                                        , ByVal nval1 As Double? _
                                        , ByVal nval2 As Double? _
                                        , ByVal nval3 As Double? _
                                        , ByVal dval1 As Date? _
                                        , ByVal dval2 As Date? _
                                        , ByVal dval3 As Date? _
                                        , ByVal idx As Double? _
                                        , ByVal status As Double?) As Double
        <OperationContract()>
        Function DeleteInsListMasterlist(ByVal id As String) As Boolean
        <OperationContract()>
        Function GetCheckExist(ByVal table As String, ByVal id As String) As DataTable
        <OperationContract()>
        Function UpdateStatusForList(ByVal username As String, ByVal code As String, ByVal lstId As String _
                                            , ByVal status As Decimal) As Boolean
#End Region

#Region "danh muc loai thay doi"
        <OperationContract()>
        Function GetInsTypeChange(Optional ByVal Is_Full As Boolean = False) As DataTable
        <OperationContract()>
        Function DeleteInsTypeChange(ByVal id As Double?) As Boolean
        <OperationContract()>
        Function UpdateInsTypeChange(ByVal username As String, ByVal id As Double? _
                                            , ByVal code As String _
                                            , ByVal name As String _
                                            , ByVal note As String) As Boolean
#End Region

#Region "Danh mục đơn vị bảo hiểm"
        <OperationContract()>
        Function GetInsListInsurance(Optional ByVal Is_Full As Boolean = False) As DataTable
        <OperationContract()>
        Function GetInsListInsuranceByUsername(ByVal User_Name As String, Optional ByVal Is_Full As Boolean = False) As DataTable
        <OperationContract()>
        Function UpdateInsListInsurance(ByVal username As String, ByVal id As Double? _
                                            , ByVal code As String _
                                            , ByVal name As String _
                                            , ByVal address As String _
                                            , ByVal phone_number As String) As Boolean
        <OperationContract()>
        Function DeleteInsListInsurance(ByVal id As Double?) As Boolean
#End Region

#Region "Danh mục nơi đăng ký khám chữa bệnh"
        <OperationContract()>
        Function GetINS_WHEREHEALTH(ByVal _filter As INS_WHEREHEALTHDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_WHEREHEALTHDTO)
        <OperationContract()>
        Function GetINS_WHEREEXPORT() As List(Of INS_WHEREHEALTHDTO)

        <OperationContract()>
        Function GetStatuSo() As DataTable
        <OperationContract()>
        Function GetStatuHE() As DataTable

        <OperationContract()>
        Function GetHU_PROVINCE(Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        <OperationContract()>
        Function GetHU_DISTRICT(Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        <OperationContract()>
        Function InsertINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO) As Boolean

        <OperationContract()>
        Function ModifyINS_WHEREHEALTH(ByVal objDMVS As INS_WHEREHEALTHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveINS_WHEREHEALTH(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteINS_WHEREHEALTH(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetDistrictByIDPro(ByVal province_ID As Decimal) As DataTable

#End Region

#Region "Chế độ bảo hiểm"

        <OperationContract()>
        Function GetEntitledType(ByVal _filter As INS_ENTITLED_TYPE_DTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                   Optional ByVal PageSize As Integer = Integer.MaxValue,
                                   Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ENTITLED_TYPE_DTO)
        <OperationContract()>
        Function InsertEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateRegimeManager(ByVal _validate As INS_REMIGE_MANAGER_DTO)
        <OperationContract()>
        Function ValidateGroupRegime(ByVal _validate As INS_GROUP_REGIMESDTO)
        <OperationContract()>
        Function ValidateListProgram(ByVal _validate As INS_LIST_PROGRAMDTO)
        <OperationContract()>
        Function ValidateListContract(ByVal _validate As INS_LIST_CONTRACTDTO)

        <OperationContract()>
        Function ValidateEntitledType(ByVal _validate As INS_ENTITLED_TYPE_DTO)

        <OperationContract()>
        Function Validate_KhamThai(ByVal _validate As INS_REMIGE_MANAGER_DTO) As DataTable

        <OperationContract()>
        Function ActiveEntitledType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteEntitledType(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh muc bien dong bao hiem"
        <OperationContract()>
        Function GetInsChangeType(ByVal _filter As INS_CHANGE_TYPEDTO,
                                 Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_CHANGE_TYPEDTO)
        <OperationContract()>
        Function InsertInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO) As Boolean

        <OperationContract()>
        Function ModifyInsChangeType(ByVal objInsChangeType As INS_CHANGE_TYPEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveInsChangeType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteInsChangeType(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục vùng bảo hiểm"
        <OperationContract()>
        Function GetINS_REGION(ByVal _filter As INS_REGION_DTO,
                              Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REGION_DTO)
        <OperationContract()>
        Function InsertINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateINS_REGION(ByVal objDMVS As INS_REGION_DTO) As Boolean

        <OperationContract()>
        Function ModifyINS_REGION(ByVal objDMVS As INS_REGION_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveINS_REGION(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteINS_REGION(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục chi phí theo cấp"
        <OperationContract()>
        Function GetINS_COST_FOLLOW_LEVER(ByVal _filter As INS_COST_FOLLOW_LEVERDTO,
                                         Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_COST_FOLLOW_LEVERDTO)
        <OperationContract()>
        Function InsertINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO) As Boolean

        <OperationContract()>
        Function ModifyINS_COST_FOLLOW_LEVER(ByVal objDMVS As INS_COST_FOLLOW_LEVERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục chế độ hưởng bảo hiểm"
        <OperationContract()>
        Function GetINS_REGIMES(ByVal _filter As INS_GROUP_REGIMESDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "REGIMES_NAME desc") As List(Of INS_GROUP_REGIMESDTO)
        <OperationContract()>
        Function InsertINS_REGIMES(ByVal objDMVS As INS_GROUP_REGIMESDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyINS_REGIMES(ByVal objDMVS As INS_GROUP_REGIMESDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveINS_REGIMES(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteINS_REGIMES(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Danh mục chương trình bảo hiểm"
        <OperationContract()>
        Function GetINS_Program(ByVal _filter As INS_LIST_PROGRAMDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CODE desc") As List(Of INS_LIST_PROGRAMDTO)
        <OperationContract()>
        Function InsertINS_Program(ByVal objDMVS As INS_LIST_PROGRAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyINS_Program(ByVal objDMVS As INS_LIST_PROGRAMDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveINS_Program(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteINS_Program(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetINS_Program_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable
        <OperationContract()>
        Function GetINS_Contract_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable
        <OperationContract()>
        Function GET_CBO_INS_LIST_CONTRACT(ByVal YEAR As Integer) As DataTable
#End Region

#Region "Danh mục hợp đồng bảo hiểm"
        <OperationContract()>
        Function GetINS_LIST_CONTRACT(ByVal _filter As INS_LIST_CONTRACTDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACTDTO)

        <OperationContract()>
        Function GetINS_LIST_CONTRACT_PKG(ByVal CONTRACT_INS_NO As String,
                                             ByVal YEAR As Integer?,
                                             ByVal ORG_INSURANCE As Integer?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_LIST_CONTRACTDTO)

        <OperationContract()>
        Function GetINS_LIST_CONTRACT_BY_ID(ID As Decimal?) As INS_LIST_CONTRACTDTO
        <OperationContract()>
        Function GetINS_LIST_CONTRACT_DETAIL(ByVal _filter As INS_LIST_CONTRACT_DETAILDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACT_DETAILDTO)
        <OperationContract()>
        Function ModifyINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteINS_LIST_CONTRACT_DETAIL(ByVal id As Double?) As Boolean

        <OperationContract()>
        Function Insert_INS_LIST_CONTRACT_DETAIL(ByVal CONTRACT_INS_ID As Double? _
                , ByVal INS_PROGRAM_ID As Double? _
                , ByVal MONEY_INS As Double?) As Boolean

        <OperationContract()>
        Function GetINS_LIST_CONTRACT_DETAIL_BY_ID(ID As Decimal?) As List(Of INS_LIST_CONTRACT_DETAILDTO)

        <OperationContract()>
        Function GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ID As Decimal?) As DataTable

        <OperationContract()>
        Function GET_INS_LIST_INSURANCE(Optional ByVal IS_BLANK As Boolean = True) As DataTable

        <OperationContract()>
        Function DELETE_INS_LIST_CONTRACT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "INS_ARISING"
        <OperationContract()>
        Function GetINS_ARISING(ByVal _filter As INS_ARISINGDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_ARISINGDTO)
        <OperationContract()>
        Function GetINS_ARISINGyById(ByVal _id As Decimal?) As INS_ARISINGDTO
        <OperationContract()>
        Function ModifyINS_ARISING(ByVal objLeave As INS_ARISINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertINS_ARISING(ByVal objLeave As List(Of INS_ARISINGDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
#End Region

#Region "Quy định đối tượng"

        <OperationContract()>
        Function GetSpecifiedObjects(ByVal _filter As INS_SPECIFIED_OBJECTS_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_SPECIFIED_OBJECTS_DTO)
        <OperationContract()>
        Function InsertSpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function VALIDATE_SPECIFIED_OBJECTS(ByVal effect_date As Date, ByVal Foreigner As Boolean, ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function ModifySpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveSpecifiedObjects(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSpecifiedObjects(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Quan ly thông tin bao hiem cu"
        <OperationContract()>
        Function GetINS_INFOOLD(ByVal _filter As INS_INFOOLDDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFOOLDDTO)
        <OperationContract()>
        Function GetINS_INFOOLDById(ByVal _id As Decimal?) As INS_INFOOLDDTO
        <OperationContract()>
        Function InsertINS_INFOOLD(ByVal objLeave As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyINS_INFOOLD(ByVal objLeave As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteINS_INFOOLD(ByVal lstID As List(Of INS_INFOOLDDTO)) As Boolean
        <OperationContract()>
        Function GetEmployeeById(ByVal _id As Decimal?) As EmployeeDTO
        <OperationContract()>
        Function GetEmployeeByIdProcess(ByRef P_EMPLOYEEID As Integer) As DataTable
#End Region

#Region "Quản lý thông tin bảo hiểm"
        <OperationContract()>
        Function GetINS_INFO(ByVal _filter As INS_INFORMATIONDTO,
                                      ByVal _param As PARAMDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFORMATIONDTO)
        <OperationContract()>
        Function GetINS_INFOById(ByVal _id As Decimal?) As INS_INFORMATIONDTO
        <OperationContract()>
        Function InsertINS_INFO(ByVal objIns_info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyINS_INFO(ByVal objIns_info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteINS_INFO(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetInfoPrint(ByVal LISTID As String) As DataTable
        <OperationContract()>
        Function GetLuongBH(ByVal p_EMPLOYEE_ID As Integer) As DataTable
        <OperationContract()>
        Function GetEmployeeID(ByVal p_EMPLOYEE_ID As String) As DataTable
        <OperationContract()>
        Function GetAllowanceTotalByDate(ByVal employeeID As Decimal) As Decimal?
#End Region

#Region "Quan ly bien dong bao hiem"
        <OperationContract()>
        Function GetINS_CHANGE(ByVal _filter As INS_CHANGEDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGEDTO)
        <OperationContract()>
        Function GetINS_CHANGEById(ByVal _id As Decimal?) As INS_CHANGEDTO
        <OperationContract()>
        Function InsertINS_CHANGE(ByVal objLeave As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyINS_CHANGE(ByVal objLeave As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteINS_CHANGE(ByVal lstID As List(Of INS_CHANGEDTO)) As Boolean
        <OperationContract()>
        Function GetTiLeDong() As DataTable
        <OperationContract()>
        Function GETLUONGBIENDONG(ByRef P_EMPLOYEEID As Integer) As DataTable
#End Region

#Region "thay doi thong tin tham gia bao hiem"
        <OperationContract()>
        Function GetChangeInfo(ByVal _filter As INS_CHANGE_INFO_DTO,
                                ByVal OrgId As Integer,
                                ByVal Fillter As String,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGE_INFO_DTO)

        <OperationContract()>
        Function InsertOrModifyChangeInfo(ByVal objTitle As INS_CHANGE_INFO_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetChangeInfoById(ByVal _id As Decimal?) As INS_CHANGE_INFO_DTO
        <OperationContract()>
        Function DeleteChangeInfo(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckDouble(ByVal id As Decimal, ByVal emp_id As Decimal, ByVal type_id As Decimal, ByVal date_change As Date) As Decimal
#End Region
#Region "Quản lý bảo hiểm Sun Care"

        <OperationContract()>
        Function GetSunCare(ByVal _filter As INS_SUN_CARE_DTO,
                                ByVal OrgId As Integer,
                                ByVal Fillter As String,
                                Optional ByVal PageIndex As Integer = 0,
                                Optional ByVal PageSize As Integer = Integer.MaxValue,
                                Optional ByRef Total As Integer = 0,
                                Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_SUN_CARE_DTO)
        <OperationContract()>
        Function GetSunCareById(ByVal _id As Decimal?) As INS_SUN_CARE_DTO
        <OperationContract()>
        Function GetLevelImport() As DataTable
        <OperationContract()>
        Function GetIns_Cost_LeverByID(ByVal _id As Decimal?) As INS_COST_FOLLOW_LEVERDTO
        <OperationContract()>
        Function InsertSunCare(ByVal objTitle As INS_SUN_CARE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySunCare(ByVal objTitle As INS_SUN_CARE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CHECK_MANAGER_SUN_CARE(ByVal P_EMP_CODE As String, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_LEVEL_ID As Decimal) As Integer

        <OperationContract()>
        Function ActiveSunCare(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSunCare(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer

        <OperationContract()>
        Function INPORT_MANAGER_SUN_CARE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
#End Region
#Region "Quản lý bảo hiểm sức khỏe cá nhân, người thân"
        <OperationContract()>
        Function InsertHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertCLAIME_INSURANCE(ByVal objTitle As INS_CLAIME_INSURANCEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateHealth_Insurance(ByVal _validate As INS_HEALTH_INSURANCE_DTO)

        <OperationContract()>
        Function DELETE_INS_CLAIME_INSURANCE(ByVal id As Decimal?) As Boolean
        <OperationContract()>
        Function Delete_INS_HEALTH_INSURANCE(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GET_INS_HEALTH_INSURANCE(
                                          ByVal ORGID As Integer?,
                                             ByVal IS_TERMINATE As Integer?,
                                             ByVal IS_CLAIM As Integer?,
                                             ByVal ISDISSOLVE As Integer?,
                                             ByVal DJOIN_START As Date?,
                                             ByVal DJOIN_END As Date?,
                                             ByVal D_EFFECT_START As Date?,
                                             ByVal D_EFFECT_END As Date?,
                                             ByVal DREDUCE_START As Date?,
                                             ByVal DREDUCE_END As Date?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_HEALTH_INSURANCE_DTO)
#End Region
#Region "Quản lý chế độ bảo hiểm"

        <OperationContract()>
        Function GetRegimeManager(ByVal _filter As INS_REMIGE_MANAGER_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                        ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REMIGE_MANAGER_DTO)
        <OperationContract()>
        Function GetRegimeManagerByID(ByVal _id As Decimal?) As INS_REMIGE_MANAGER_DTO
        <OperationContract()>
        Function GetInfoInsByEmpID(ByVal employee_id As Integer) As INS_INFORMATIONDTO
        <OperationContract()>
        Function GetLuyKe(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date,
                                      ByRef P_EMPLOYEEID As Integer,
                                      ByVal P_ENTITLED_ID As Integer) As DataTable
        <OperationContract()>
        Function CALCULATOR_DAY(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date) As DataTable
        <OperationContract()>
        Function GetMaxDayByID(ByVal P_ENTITLED_ID As Integer) As DataTable
        <OperationContract()>
        Function InsertRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteRegimeManager(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ObjectPayInsurrance(ByVal lstID As List(Of String), ByVal objName As String) As Boolean
        <OperationContract()>
        Function GetTienHuong(ByVal P_NUMOFF As Integer,
                                    ByVal P_ATHOME As Integer,
                                    ByRef P_EMPLOYEEID As Integer,
                                    ByVal P_INSENTILEDKEY As Integer,
                                    ByVal P_SALARY_ADJACENT As Decimal,
                                    ByVal P_FROMDATE As Date,
                                    ByVal P_SOCON As Integer) As DataTable
#End Region

#Region "Khai báo điều chỉnh nhóm bảo hiểm sun care"
        <OperationContract()>
        Function GetGroup_SunCare(ByVal _filter As INS_GROUP_SUN_CAREDTO,
                                      ByVal _param As PARAMDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_GROUP_SUN_CAREDTO)
        <OperationContract()>
        Function GetGroup_SunCareById(ByVal _id As Decimal?) As INS_GROUP_SUN_CAREDTO
        <OperationContract()>
        Function InsertGroup_SunCare(ByVal objIns_info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyGroup_SunCare(ByVal objIns_info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteGroup_SunCare(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetINS_CLAIME_INSURANCE_BY_ID(ByVal ID As Decimal?) As List(Of INS_CLAIME_INSURANCEDTO)
        <OperationContract()>
        Function Get_LIST_FAMILY_BY_ID_EMP(ByVal ID As Decimal?) As DataTable

        <OperationContract()>
        Function GET_INS_HEALTH_INSURANCE_BY_ID(ByVal ID As Decimal?) As DataTable
#End Region

#Region "Báo cáo bảo hiểm"
        <OperationContract()>
        Function GetReportList() As DataTable
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)
        <OperationContract()>
        Function GetD02Tang(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetD02Giam(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetC70_HD(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetQuyLuongBH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetDsBHSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetDsDieuChinhSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetChiPhiSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetOrgInfo(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Org_ID As Decimal) As DataTable
        <OperationContract()>
        Function GetOrgInfoMONTH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Org_ID As Decimal) As DataTable
#End Region

#Region "System"
        <OperationContract()>
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As InsuranceCommon.TABLE_NAME) As Boolean
#End Region

#Region "Dashboard"
        <OperationContract()>
        Function GetStatisticInsPayFund(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticInsPayFundDetail(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticInsRate(ByVal _locationId As Integer) As List(Of StatisticInsRateDTO)
        <OperationContract()>
        Function GetStatisticInsCeiling(ByVal _locationId As Integer) As List(Of StatisticInsCeilingDTO)

#End Region

#Region "Validate Combobox"
        <OperationContract()>
        Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
#End Region

#Region "Function"
        <OperationContract()>
        Function GetEmpInfo(ByVal code As Decimal, Optional ByVal org As String = "") As DataTable
        <OperationContract()>
        Function Check_Exist_Emp_Ins(ByVal strEmpId As String) As DataTable
        <OperationContract()>
        Function Check_Arising_Type(ByVal arising_Type As Decimal) As DataTable
        <OperationContract()>
        Function CheckDayCalculate(ByVal idRegime As Decimal, ByVal dayCal As Decimal) As DataTable
#End Region
#Region "Bussiness"
        'Khai báo biến động bảo hiểm
        <OperationContract()>
        Function GetInsArising(ByVal username As String, ByVal fromdate As Date?, ByVal todate As Date?, ByVal arising_type_id As Double?, ByVal org_id As Decimal, ByVal insurance_id As Double?, ByVal is_Dissolve As Decimal) As DataTable
        <OperationContract()>
        Function UpdateInsArising(ByVal username As String, ByVal effect_date As Date?, ByVal id As Double, ByVal empid As Double, ByVal arising_type_id As Double) As Boolean
        <OperationContract()>
        Function UpdateInsArisingNote(ByVal username As String, ByVal id As Decimal, ByVal note As String) As Boolean
        <OperationContract()>
        Function DeleteInsArising(ByVal username As String, ByVal id As Double) As Boolean

        <OperationContract()>
        Function DELETE_INS_ARSING_LEAVE_SHEET(ByVal id As String) As Boolean

        'Quản lý thông tin bảo hiểm
        <OperationContract()>
        Function GetInsInfomation(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal org_id As Double? _
                                    , ByVal isTer As Double? _
                                    , ByVal isDISSOLVE As Double?
                                    ) As DataTable

        <OperationContract()>
        Function GET_EXPORT_BHXH() As DataTable

        <OperationContract()>
        Function IMPORT_BHXH(ByVal P_DOCXML As String) As Boolean

        <OperationContract()>
        Function UpdateInsInfomation(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As Double? _
                                    , ByVal ins_org_name As String _
                                    , ByVal seniority_insurance As Double? _
                                    , ByVal seniority_insurance_company As String _
                                    , ByVal social_number As String _
                                    , ByVal social_status As Double? _
                                    , ByVal social_submit_date As Date? _
                                    , ByVal social_submit As String _
                                    , ByVal social_grant_date As Date? _
                                    , ByVal social_save_number As String _
                                    , ByVal social_deliver_date As Date? _
                                    , ByVal social_return_date As Date? _
                                    , ByVal social_receiver As String _
                                    , ByVal social_note As String _
                                    , ByVal health_number As String _
                                    , ByVal health_status As Double? _
                                    , ByVal health_effect_from_date As Date? _
                                    , ByVal health_effect_to_date As Date? _
                                    , ByVal health_area_ins_id As Double? _
                                    , ByVal health_receive_date As Date? _
                                    , ByVal health_receiver As String _
                                    , ByVal health_return_date As Date? _
                                    , ByVal unemp_from_month As Date? _
                                    , ByVal unemp_to_month As Date? _
                                    , ByVal unemp_register_month As Date? _
                                    , ByVal si_from_month As Date? _
                                    , ByVal si_to_month As Date? _
                                    , ByVal hi_from_month As Date? _
                                    , ByVal hi_to_month As Date? _
                                    , ByVal si As Double? _
                                    , ByVal hi As Double? _
                                    , ByVal ui As Double? _
                                    , ByVal bhtnld_bnn As Double? _
                                    , ByVal is_hi_five_year As Double? _
                                    , ByVal bhtnld_bnn_from As Date? _
                                    , ByVal bhtnld_bnn_to As Date? _
                                    , ByVal register_place As Decimal?
                                    ) As Boolean
        <OperationContract()>
        Function DeleteInsInfomation(ByVal username As String, ByVal id As Double?) As Boolean

        'INS_ARISING_MANUAL	      Quản lý biến động bảo hiểm
        <OperationContract()>
        Function GetInsArisingManual(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal org_id As Double? _
                                    , ByVal from_date As Date? _
                                    , ByVal to_date As Date? _
                                    , ByVal isTer As Double? _
                                    , ByVal isDISSOLVE As Double?) As DataTable
        <OperationContract()>
        Function UpdateInsArisingManual(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As Double? _
                                    , ByVal ins_org_id As Double? _
                                    , ByVal ins_arising_type_id As Double? _
                                    , ByVal salary_pre_period As Double? _
                                    , ByVal salary_now_period As Double? _
                                    , ByVal from_health_ins_card As Double? _
                                    , ByVal effective_date As Date? _
                                    , ByVal expire_date As Date? _
                                    , ByVal declare_date As Date? _
                                    , ByVal arising_from_month As Date? _
                                    , ByVal arising_to_month As Date? _
                                    , ByVal note As String _
                                    , ByVal social_note As String _
                                    , ByVal health_number As String _
                                    , ByVal health_status As Double? _
                                    , ByVal health_effect_from_date As Date? _
                                    , ByVal health_effect_to_date As Date? _
                                    , ByVal health_area_ins_id As Double? _
                                    , ByVal health_receive_date As Date? _
                                    , ByVal health_receiver As String _
                                    , ByVal health_return_date As Date? _
                                    , ByVal unemp_from_moth As Date? _
                                    , ByVal unemp_to_month As Date? _
                                    , ByVal unemp_register_month As Date? _
                                    , ByVal r_from As Date? _
                                    , ByVal o_from As Date? _
                                    , ByVal r_to As Date? _
                                    , ByVal o_to As Date? _
                                    , ByVal r_si As Double? _
                                    , ByVal o_si As Double? _
                                    , ByVal r_hi As Double? _
                                    , ByVal o_hi As Double? _
                                    , ByVal r_ui As Double? _
                                    , ByVal o_ui As Double? _
                                    , ByVal a_from As Date? _
                                    , ByVal a_to As Date? _
                                    , ByVal a_si As Double? _
                                    , ByVal a_hi As Double? _
                                    , ByVal a_ui As Double? _
                                    , ByVal si As Double? _
                                    , ByVal hi As Double? _
                                    , ByVal ui As Double? _
                                    , ByVal tnld_bnn As Double? _
                                    , ByVal si_sal As Double? _
                                    , ByVal hi_sal As Double? _
                                    , ByVal ui_sal As Double? _
                                    , ByVal tnld_bnn_sal As Double? _
                                    , ByVal si_sal_old As Double? _
                                    , ByVal hi_sal_old As Double? _
                                    , ByVal ui_sal_old As Double? _
                                    , ByVal tnld_bnn_sal_old As Double? _
                                    , ByVal a_tnld_bnn As Double? _
                                    , ByVal r_tnld_bnn As Double? _
                                    , ByVal P_HI_DECLARED_DATE As Date? _
                                    , ByVal P_O_HI_COM As Decimal? _
                                    , ByVal P_O_HI_EMP As Decimal? _
                                    , ByVal P_HI_RATE_ARREARS As Decimal? _
                                    , ByVal P_SALARY_HD As Decimal? _
                                    , ByVal P_SALARY_PC As Decimal? _
                                    , ByVal P_FOREIGN As Decimal?) As Double
        <OperationContract()>
        Function DeleteInsArisingManual(ByVal username As String, ByVal lstid As String) As Double

        <OperationContract()>
        Function InsAraisingAuto(ByVal username As String, ByVal empid As Double?, ByVal ins_arising_type As Double? _
        , ByVal effective_date As Date?, ByVal declare_date As Date?, ByVal hi_date As Date?, ByVal is_foreign As Double?) As DataTable
        <OperationContract()>
        Function InsAraisingAuto2(ByVal username As String, ByVal empid As Double?, ByVal ins_arising_type As Double? _
        , ByVal effective_date As Date?, ByVal declare_date As Date?, ByVal hi_date As Date?, ByVal truythu As String, ByVal is_foreign As Double?) As DataTable

        'Quản lý thông tin hưởng chế độ
        <OperationContract()>
        Function GetInsRegimes(ByVal username As String, ByVal id As Double? _
                            , ByVal regime_id As Double? _
                            , ByVal pay_form As Double? _
                            , ByVal employee_id As String _
                            , ByVal org_id As Double? _
                            , ByVal from_date As Date? _
                            , ByVal to_date As Date? _
                            , ByVal isTer As Double?
                            ) As DataTable
        <OperationContract()>
        Function UpdateInsRegimes(ByVal username As String, ByVal id As Double? _
                                , ByVal employee_id As Double? _
                                , ByVal regime_id As Double? _
                                , ByVal pay_form As Double? _
                                , ByVal from_date As Date? _
                                , ByVal to_date As Date? _
                                , ByVal day_calculator As Double? _
                                , ByVal born_date As Date? _
                                , ByVal name_children As String _
                                , ByVal children_no As Double? _
                                , ByVal accumulate_day As Double? _
                                , ByVal subsidy_salary As Double? _
                                , ByVal subsidy As Double? _
                                , ByVal subsidy_amount As Double? _
                                , ByVal payroll_date As Date? _
                                , ByVal declare_date As Date? _
                                , ByVal condition As String _
                                , ByVal ins_pay_amount As Double? _
                                , ByVal pay_approve_date As Date? _
                                , ByVal approv_day_num As Double? _
                                , ByVal note As String _
                                , ByVal money_advance As Double? _
                                , ByVal off_together As Double? _
                                , ByVal off_in_house As Double? _
                                , ByVal regimes_sal As Double? _
                                , ByVal form_Allow As Decimal?) As Double
        <OperationContract()>
        Function DeleteInsRegimes(ByVal username As String, ByVal id As Double?) As Double
        <OperationContract()>
        Function GetInsRegimes_cal(ByVal employee_id As Double?, ByVal regime_id As Double? _
                                , ByVal from_date As Date? _
                                , ByVal to_date As Date?, Optional ByVal dayCal As Double = -1) As DataTable

        'Điều chỉnh Hồ sơ cấp BHXH, thẻ BHYT
        <OperationContract()>
        Function GetInsModifier(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal org_id As Double? _
                                    , ByVal ins_modifier_type_id As Double? _
                                    , ByVal from_date As Date? _
                                    , ByVal to_date As Date? _
                                    , ByVal isTer As Double?
                                    ) As DataTable
        <OperationContract()>
        Function UpdateInsModifier(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As Double? _
                                        , ByVal ins_modifier_type_id As Double? _
                                        , ByVal reason As String _
                                        , ByVal old_info As String _
                                        , ByVal new_info As String _
                                        , ByVal modifier_date As Date? _
                                        , ByVal note As String _
                                        , ByVal isUpdate As String _
                                        , ByVal birth_date As Date? _
                                        , ByVal areaid As Double? _
                                        , ByVal id_date As Date? _
                                        , ByVal id_place As Double? _
                                        , ByVal birth_place As Double? _
                                        , ByVal per_address As String _
                                        , ByVal per_coun As Double? _
                                        , ByVal per_prov As Double? _
                                        , ByVal per_dist As Double? _
                                        , ByVal per_ward As Double? _
                                        , ByVal con_address As String _
                                        , ByVal con_coun As Double? _
                                        , ByVal con_prov As Double? _
                                        , ByVal con_dist As Double? _
                                        , ByVal con_ward As Double?) As Double
        <OperationContract()>
        Function DeleteInsModifier(ByVal username As String, ByVal id As Double?) As Double

        'Tổng quỹ lương
        <OperationContract()>
        Function CheckInvalidArising(ByVal ins_Org_Id As Decimal) As DataTable
        <OperationContract()>
        Function GetInsTotalSalary(ByVal username As String, ByVal year As Double? _
                                , ByVal month As Double? _
                                , ByVal ins_org_id As Double? _
                                , ByVal period As String
                                ) As DataTable


        <OperationContract()>
        Function SPI_INS_TOTALSALARY_EXPORT(ByVal username As String, ByVal year As Double? _
                                , ByVal month As Double? _
                                , ByVal ins_org_id As Double? _
                                , ByVal period As String
                                ) As DataTable

        <OperationContract()>
        Function GetInsTotalSalaryPeriod(ByVal username As String, ByVal year As Double? _
                                        , ByVal month As Double? _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal period As String
                                        ) As DataTable

        <OperationContract()>
        Function GetInsTotalSalary_Summary(ByVal username As String, ByVal year As Double? _
                                        , ByVal month As Double? _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal period As String _
                                        , ByVal isPre As String
                                        ) As DataTable

        <OperationContract()>
        Function CalInsTotalSalary(ByVal username As String, ByVal year As Double? _
                               , ByVal month As Double? _
                               , ByVal ins_org_id As Double? _
                               , ByVal period As String
                               ) As Double

        <OperationContract()>
        Function CalInsTotalSalaryBatch(ByVal username As String, ByVal fromdate As Date _
                                                                , ByVal todate As Date _
                                                                , ByVal ins_org_id As Double?) As Double

        <OperationContract()>
        Function LockInsTotalSalary(ByVal username As String, ByVal year As Double? _
    , ByVal month As Double? _
    , ByVal ins_org_id As Double? _
    , ByVal period As String
    ) As Double
        <OperationContract()>
        Function LockInsTotalSalaryByDate(ByVal from_date As Date? _
                                               , ByVal to_date As Date? _
                                               , ByVal ins_org_id As Double? _
                                               , ByVal period As Double?) As Double
        <OperationContract()>
        Function CHECK_INS_TOTALSALARY_UNLOCK(ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double?) As Boolean
        <OperationContract()>
        Function CHECK_INS_TOTALSALARY_UNLOCK_BYDATE(ByVal from_date As Date? _
                                               , ByVal to_date As Date? _
                                           , ByVal ins_org_id As Double?) As Boolean
        'Cap nhat thong tin BH
        <OperationContract()>
        Function GetListDataImportHelth(ByVal id As Double?) As DataSet
        <OperationContract()>
        Function GET_INS_HEATH_IMPORT() As DataSet
        'DeleteInsHealthImport
        <OperationContract()>
        Function GetInsHealthImport(ByVal username As String, ByVal id As Double? _
                                , ByVal employee_id As String _
                                , ByVal ins_org_id As Double? _
                                , ByVal insurance_id As Double? _
                                , ByVal effective_from_date As Date? _
                                , ByVal effective_to_date As Date?) As DataTable
        <OperationContract()>
        Function GetInsHealthImportCheckOrg(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As String _
                                        , ByVal ins_org_id As String _
                                        , ByVal insurance_id As Double? _
                                        , ByVal effective_from_date As Date? _
                                        , ByVal effective_to_date As Date?) As DataTable
        <OperationContract()>
        Function UpdateInsHealthImport(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal ins_org_id As Double? _
                                    , ByVal insurance_id As Double? _
                                    , ByVal effective_from_date As Date? _
                                    , ByVal effective_to_date As Date?) As Double
        <OperationContract()>
        Function DeleteInsHealthImport(ByVal username As String, ByVal id As Double?) As Double
        <OperationContract()>
        Function UpdateHealthImport(ByVal username As String, ByVal employee_id As String _
                                                                , ByVal ins_org_name As String _
                                                                , ByVal seniority_insurance As String _
                                                                , ByVal social_number As String _
                                                                , ByVal social_status As String _
                                                                , ByVal social_grant_date As String _
                                                                , ByVal social_save_number As String _
                                                                , ByVal health_number As String _
                                                                , ByVal health_status As String _
                                                                , ByVal health_effect_from_date As String _
                                                                , ByVal health_effect_to_date As String _
                                                                , ByVal health_receive_date As String _
                                                                , ByVal health_receiver As String _
                                                                , ByVal health_area_ins As String
                                                                      ) As Double

        'GetInsHealthExt
        <OperationContract()>
        Function GetInsHealthExt(ByVal username As String, ByVal id As Double? _
                                , ByVal employee_id As String _
                                , ByVal ins_org_id As Double? _
                                , ByVal health_ins_card As String _
                                , ByVal effective_from_date As Date? _
                                , ByVal effective_to_date As Date? _
                                , ByVal health_from_date As Date? _
                                , ByVal health_to_date As Date?) As DataTable
        <OperationContract()>
        Function UpdateInsHealthExt(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal ins_org_id As Double? _
                                    , ByVal health_ins_card As String _
                                    , ByVal effective_from_date As Date? _
                                    , ByVal effective_to_date As Date? _
                                    , ByVal health_from_date As Date? _
                                    , ByVal health_to_date As Date?) As Double
        <OperationContract()>
        Function DeleteInsHealthExt(ByVal username As String, ByVal id As String) As Double

        <OperationContract()>
        Function GET_MLTTC(ByVal p_date As Date) As DataTable
#End Region

        <OperationContract()>
        Function RATE_ARREARS_LIST() As DataTable

        <OperationContract()>
        Function CHECK_INS_ORG_ID(ByVal P_INS_ORG_ID As Decimal) As Boolean

        <OperationContract()>
        Function GET_BHYT_PP(ByVal P_EFFECTIVE_DATE As Date) As DataTable

        <OperationContract()>
        Function GetInsArisingLeaveSheet(ByVal _filter As INS_ARISING_LEAVESHEET_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                        ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE asc") As List(Of INS_ARISING_LEAVESHEET_DTO)

        <OperationContract()>
        Function GET_INS_ARSING_LEAVE_SHEET(ByVal OrgId As Integer,
                                               ByVal IsDissolve As Integer,
                                               ByVal startDate As Date,
                                               ByVal endDate As Date,
                                               ByVal LeaveType As String,
                                               ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UPDATE_INS_ARSING_LEAVE_SHEET(ByVal P_LIST_ID As String,
                                                  ByVal P_FROM_MONTH As Date,
                                                  ByVal P_TO_MONTH As Date,
                                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function INSERT_INS_ARSING_TANG(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function INSERT_INS_ARSING_GIAM(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CHECK_INS_ORG(ByVal P_EMP_ID As Decimal,
                                  ByVal P_INS_ORG_ID As Decimal,
                                  ByVal P_ARISING_FROM_MONTH As Date,
                                  ByRef pDate As String) As Integer
        <OperationContract()>
        Function CHECK_INS_ORG_TANG_GIAM(ByVal P_INS_ORG_ID As Decimal) As String

        <OperationContract()>
        Function CHECK_INS_MANUAL(ByVal P_EMP_ID As Decimal,
                                  ByVal P_INS_ORG_ID As Decimal,
                                  ByVal P_ARISING_FROM_MONTH As Date,
                                  ByRef pDate As String) As Integer

        <OperationContract()>
        Function UPDATE_INS_INFORMATION(ByVal P_EMP_ID As Decimal,
                                           ByVal P_ARISING_FROM_MONTH As Date) As Boolean

#Region "Quá trình bảo hiểm trước khi vào công ty"
        <OperationContract()>
        Function GetInsWorkingBefore(ByVal _filter As INS_WORKING_BEFOREDTO,
                               ByVal OrgId As Integer,
                               ByVal Fillter As String,
                               Optional ByVal PageIndex As Integer = 0,
                               Optional ByVal PageSize As Integer = Integer.MaxValue,
                               Optional ByRef Total As Integer = 0,
                               Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_WORKING_BEFOREDTO)
        <OperationContract()>
        Function InsertInsWorkingBefore(ByVal objTitle As INS_WORKING_BEFOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyInsWorkingBefore(ByVal objTitle As INS_WORKING_BEFOREDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function INPORT_InsWorkingBefore(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Delete_InsWorkingBefore(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateWorkingBefore(ByVal _validate As INS_WORKING_BEFOREDTO)

#End Region

    End Interface
End Namespace
