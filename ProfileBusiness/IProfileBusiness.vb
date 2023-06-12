' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceContracts

    <ServiceContract()>
    Public Interface IProfileBusiness
#Region "Position"
        <OperationContract()>
        Function SwapMasterInterim(ByVal _Id As Decimal, ByVal log As UserLog, ByVal type As String) As Boolean
        <OperationContract()>
        Function GetPositions(ByVal _filter As TitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        <OperationContract()>
        Function InsertPossition(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidatePosition_ACTIVITIES(ByVal objTitle As TitleDTO) As Boolean

        <OperationContract()>
        Function ValidatePosition(ByVal objTitle As TitleDTO) As Boolean

        <OperationContract()>
        Function ModifyPossition(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePositions(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByRef Status As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckOwnerExist(ByVal objTitle As TitleDTO) As Boolean

        <OperationContract()>
        Function DeletePositions(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetPositionByID(ByVal sID As Decimal) As List(Of TitleDTO)
        <OperationContract()>
        Function GetPositionID(ByVal ID As Decimal, ByVal log As UserLog, ByVal sLang As String, Optional ByVal IsReadWrite As Boolean = False) As TitleDTO
        <OperationContract()>
        Function GetPrintJD(ByVal ID As Decimal) As TitleDTO
        <OperationContract()>
        Function GetOrgTreeApp(ByVal sLang As String, ByVal log As UserLog) As List(Of OrganizationDTO)
        <OperationContract()>
        Function GetOrgOMByID(ByVal _Id As Decimal) As OrganizationDTO
        <OperationContract()>
        Function AutoGenCodeHuTile(ByVal tableName As String, ByVal colName As String) As String
        <OperationContract()>
        Function CheckIsOwner(ByVal OrgId As Decimal) As Boolean
        <OperationContract()>
        Function ModifyTitleById(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateManager(ByVal lstID As List(Of Decimal), ByVal objTitle As TitleDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetPositionByOrgID(ByVal _filter As TitleDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "ORG_NAME") As List(Of TitleDTO)
        <OperationContract()>
        Function GetTitleByTitleID(ByVal Id As Decimal) As TitleDTO
        <OperationContract()>
        Function InsertTitleNB(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal, ByVal log As UserLog) As Boolean
#End Region
        <OperationContract()>
        Function ActiveOrgEmp(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function JobPossitionRptHist(ByVal RptMonth As Date, ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)
        <OperationContract()>
        Function ModifyJobPosTreeList(ByVal objJobPositinTree As JobPositinTreeDTO,
                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetJobChileTree(ByVal job_Id As Decimal, ByVal sLang As String) As List(Of JobChildTreeDTO)
        <OperationContract()>
        Function GetjobPosition(ByVal _filter As JobPositionDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal Language As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobPositionDTO)

        <OperationContract()>
        Function InsertjobPosition(ByVal objjob As JobPositionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyjobPosition(ByVal objjob As JobPositionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivejobPosition(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeletejobPosition(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function OrgChartRpt(ByVal Language As String, ByVal log As UserLog) As List(Of OrgChartRptDTO)
        <OperationContract()>
        Function GetJobPosTree(ByVal sLang As String, ByVal log As UserLog) As List(Of JobPositinTreeDTO)
#Region "quan ly file"
        <OperationContract()>
        Function GetFoldersAll() As List(Of FolderDTO)

        <OperationContract()>
        Function GetFoldersStructureInfo(ByVal _folderId As Decimal) As List(Of FolderDTO)

        <OperationContract()>
        Function AddFolder(ByVal _folder As FolderDTO) As Integer

        <OperationContract()>
        Function DeleteFolder(ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function GetFileOfFolder(ByVal _filter As UserFileDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal log As UserLog = Nothing,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc"
                                    ) As List(Of UserFileDTO)

        <OperationContract()>
        Function GetFolderByID(ByVal _id As Decimal) As FolderDTO

        <OperationContract()>
        Function AddUserFile(ByVal _userFile As UserFileDTO, ByVal _imageBinary As Byte()) As Decimal

        <OperationContract()>
        Function DeleteUserFile(ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function GetUserFileByID(ByVal _id As Decimal) As UserFileDTO

        <OperationContract()>
        Function GetUserFileByte(ByVal _id As Decimal) As Byte()
#End Region

#Region "JobBand"
        <OperationContract()>
        Function GetJobBand(ByVal _filter As JobBradDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID desc") As List(Of JobBradDTO)

        <OperationContract()>
        Function InsertJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifJobBand(ByVal objTitle As JobBradDTO, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetJobBandID(ByVal sID As Decimal) As JobBradDTO

        <OperationContract()>
        Function ActiveJobBand(ByVal lstID As List(Of Decimal), ByVal sActive As Decimal) As Boolean

        <OperationContract()>
        Function DeleteJobBand(ByVal lstID As List(Of Decimal)) As Boolean
#End Region
#Region "OM"
        <OperationContract()>
        Function GetOrgTreeList(ByVal username As String, ByVal sACT As String) As List(Of OrganizationDTO)
        <OperationContract()>
        Function GetOrgTreeEmp(ByVal sACT As String) As List(Of OrganizationDTO)

        <OperationContract()>
        Function ActiveOrgTreeList(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCompanyLevel(ByVal orgID As Decimal) As String
        <OperationContract()>
        Function GetDataByProcedures(ByVal isBank As Decimal, Optional ByVal ID As Decimal = 0, Optional ByVal Name As String = "", Optional ByVal sLang As String = "vi-VN") As DataTable
        <OperationContract()>
        Function ModifyOrgTreeList(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyOrgTreeEmp(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetPossition(ByVal _filter As TitleDTO,
                              ByVal _param As ParamDTO,
                               ByVal sLang As String,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal IsReadWrite As Boolean = False,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleDTO)
        <OperationContract()>
        Function GetPossition2(ByVal _filter As TitleDTO,
                              ByVal _param As ParamDTO,
                               ByVal sLang As String,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal IsReadWrite As Boolean = False,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleDTO)
        <OperationContract()>
        Function InsertOrgTreeList(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function Deletejob(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Activejob(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckJobExistInTitle(JobId As Decimal) As Boolean

        <OperationContract()>
        Function Getjob(ByVal _filter As JobDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal Language As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobDTO)
        <OperationContract()>
        Function Insertjob(ByVal objjob As JobDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function Validatejob(ByVal objjob As JobDTO) As Boolean

        <OperationContract()>
        Function Modifyjob(ByVal objjob As JobDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetjobID(ByVal ID As Decimal) As JobDTO
        <OperationContract()>
        Function ValidateJobCode(ByVal objTitle As JobDTO) As Boolean
        <OperationContract()>
        Function GetjobFunctionByJobID(ByVal ID As Decimal) As List(Of JobFunctionDTO)

        <OperationContract()>
        Function InsertjobFunction(ByVal objjob As JobFunctionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyjobFunction(ByVal objjob As JobFunctionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletejobFunction(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region
#Region "event manage"
        <OperationContract()>
        Function GetEventManageByID(ByVal objEventManage As EventManageDTO) As EventManageDTO
        <OperationContract()>
        Function GetEventManageFileByte(ByVal _id As Decimal, ByVal _fileName As String) As Byte()
        <OperationContract()>
        Function GetEventManage(ByVal _filter As EventManageDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EventManageDTO)

        <OperationContract()>
        Function GetEmpByEvent(ByVal _id As Decimal?) As List(Of EventEmpDTO)
        <OperationContract()>
        Function InsertEventManage(ByVal objEventManage As EventManageDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ModifyEventManage(ByVal objEventManage As EventManageDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetEventManageByte(ByVal _id As Decimal) As Byte()
        <OperationContract()>
        Function ApplyEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function UnapplyEventManage(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function PortalSendImage_mb(ByVal employeeCode As String, ByVal userID As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String, ByVal IsEdit As Decimal) As Boolean
        <OperationContract()>
        Function EmployeeImage_mb(ByVal userID As Decimal, ByRef sError As String, ByVal IsEdit As Decimal) As Byte()
        <OperationContract()>
        Function PortalSendImageRelation(ByVal fileFor As String, ByVal userID As Decimal, ByVal id As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String) As Boolean

        <OperationContract()>
        Function PersonRelationImage_mb(ByVal userId As Decimal, ByRef sError As String, ByVal id As Decimal, ByVal tab As Decimal, ByVal fileFor As String) As Byte()

        <OperationContract()>
        Function Profile_mb(ByVal userId As Decimal, ByRef sError As String, ByVal id As Decimal, ByVal tab As Decimal, ByVal fileFor As String) As Byte()
#End Region
#Region " hu_certificateedit"

        <OperationContract()>
        Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        <OperationContract()>
        Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByVal log As UserLog,
                                            ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByVal log As UserLog,
                                            ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        <OperationContract()>
        Function SendCertificateEdit(ByVal lstID As List(Of Decimal),
                                          ByVal log As UserLog) As Boolean
#End Region
#Region "hu_certificate"
        <OperationContract()>
        Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO)
#End Region
#Region "Contract appendix"
        <OperationContract()>
        Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer
#End Region
        <OperationContract()>
        Function GetMaxId() As Decimal
        <OperationContract()>
        Function GetNameOrg(ByVal org_id As Decimal) As String
        <OperationContract()>
        Function GetOrgsTree() As List(Of OrganizationDTO)

        <OperationContract()>
        Function GET_DEFAULT_OBJECT_ATTENDANCE() As Integer

#Region "ngach, bac, thang luong"
        <OperationContract()>
        Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable
#End Region
        <OperationContract()>
        Function Calculator_Salary(ByVal data_in As String) As DataTable
        <OperationContract()>
        Function GetHoSoLuongImport() As DataSet
        <OperationContract()>
        Function GetQTCTImport() As DataSet
        <OperationContract()>
        Function GetBCCCImport() As DataSet
        <OperationContract()>
        Function GetHopdongImport() As DataSet
#Region "Quản lý công nợ"
        <OperationContract()>
        Function GetDebtMng(ByVal _filter As DebtDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DebtDTO)
#End Region

#Region "Location"
        <OperationContract()>
        Function GetLocationID(ByVal ID As Decimal) As LocationDTO

        <OperationContract()>
        Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO)

        <OperationContract()>
        Function GetLocation_V2(ByVal _filter As LocationDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of LocationDTO)

        <OperationContract()>
        Function InsertLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteLocationID(ByVal lstlocation As Decimal, ByVal log As UserLog) As Boolean
        'xóa nv trong black list
        <OperationContract()>
        Function DeleteNVBlackList(ByVal id_no As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Hoadm - Common"

#Region "OtherList"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetINS_Contract_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable

        <OperationContract()>
        Function GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ID As Decimal?) As DataTable

        <OperationContract()>
        Function GetInsListWhereHealth() As DataTable
        <OperationContract()>
        Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal, ByVal sLang As String) As DataTable

        <OperationContract()>
        Function GetBankList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetBankBranchList(ByVal bankID As Decimal, ByVal isBlank As Boolean) As DataTable

        '<OperationContract()>
        'Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean, ByVal Employee_ID As Decimal) As DataTable


        <OperationContract()>
        Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProvinceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProvinceList2(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetNationList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetStaffRankList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GET_INS_HEALTH_BY_EMPID(ByVal empID As Decimal?) As DataTable

        <OperationContract()>
        Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        '<OperationContract()>
        'Function GetSalaryTypes(ByVal query As PA_SALARY_TYPEQuery) As PA_SALARY_TYPEResult

        <OperationContract()>
        Function GetSalaryTypeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GET_Hu_Allowance_List(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_AllowanceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetPA_ObjectSalary(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_WageTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_MissionTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_TripartiteTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function SaveTemplateFileHost(ByVal fByte As Byte(), ByVal folder As String, ByVal filename As String) As Boolean

        <OperationContract()>
        Function GetTemplateFileHost(ByVal path As String) As Byte()

        <OperationContract()>
        Function ExistsTemplateFileHost(ByVal path As String) As Boolean

        <OperationContract()>
        Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                          ByVal isTemplateType As Decimal) As DataTable

        <OperationContract()>
        Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                    ByVal isTemplateType As Decimal) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamic_Muti_1(ByVal dID As String,
                                          ByVal de As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable
        <OperationContract()>
        Function GetHU_DataDynamic_Muti(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable
        <OperationContract()>
        Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamicContract(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetInsRegionList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyList(ByVal groupID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyPeriodList(ByVal year As Decimal, ByVal isBlank As Boolean) As DataTable

#End Region

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommon.TABLE_NAME) As Boolean

        <OperationContract()>
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String

        <OperationContract()>
        Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet

        <OperationContract()>
        Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet

#End Region
#Region "VALIDATE BUSINESS"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Table_Name"></param>
        ''' <param name="Column_Name"></param>
        ''' <param name="Value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateBusiness(ByVal Table_Name As String, ByVal Column_Name As String, ByVal ListID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
#End Region
#Region "List"
        <OperationContract()>
        Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                      ByVal _filter As EmployeeDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
#Region "org_brand"

        <OperationContract()>
        Function GetOrgLevel(ByVal org_id As Decimal) As OrganizationDTO
        <OperationContract()>
        Function GetOrgBrand(ByVal _filter As OrgBrandDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgBrandDTO)
        <OperationContract()>
        Function DeleteOrgBrand(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertOrgBrand(ByVal objOrgBrand As OrgBrandDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function checkOrgBrand(ByVal objOrgBrand As OrgBrandDTO) As Decimal
#End Region

#Region "org_Pause"

        <OperationContract()>
        Function GetOrgLevel_Pause(ByVal org_id As Decimal) As OrganizationDTO
        <OperationContract()>
        Function GetOrgPause(ByVal _filter As ORG_PAUSEDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ORG_PAUSEDTO)
        <OperationContract()>
        Function DeleteOrgPause(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertOrgPause(ByVal objOrgBrand As ORG_PAUSEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function checkOrgPause(ByVal objOrgBrand As ORG_PAUSEDTO) As Decimal
#End Region

#Region "contract procedure"

        <OperationContract()>
        Function GetContractProcedure(ByVal _filter As ContractProcedureDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractProcedureDTO)
        <OperationContract()>
        Function DeleteContractProcedure(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Delete_List_Contract(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertContractProcedure(ByVal objContractProcedure As ContractProcedureDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function checkContractProcedure(ByVal objContractProcedure As ContractProcedureDTO) As Decimal
#End Region
#Region "Title"

        <OperationContract()>
        Function GetTitle(ByVal _filter As TitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        <OperationContract()>
        Function InsertTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function ValidateTitle(ByVal objTitle As TitleDTO) As Boolean

        <OperationContract()>
        Function ModifyTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteTitle(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetTitleByID(ByVal sID As Decimal) As List(Of TitleDTO)
        <OperationContract()>
        Function GetTitleID(ByVal ID As Decimal) As TitleDTO

#End Region

#Region "Chucvu"

        <OperationContract()>
        Function GetChucvu(ByVal _filter As ChucvuDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        <OperationContract()>
        Function InsertChucvu(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function ValidateChucvuCode(ByVal objTitle As ChucvuDTO) As Boolean

        <OperationContract()>
        Function ModifyChucvu(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveChucvu(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteChucvu(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChucvuByID(ByVal sID As Decimal) As List(Of ChucvuDTO)
        <OperationContract()>
        Function GetChucvuID(ByVal ID As Decimal) As ChucvuDTO

#End Region

#Region "Chucvu bld"

        <OperationContract()>
        Function GetChucvuBld(ByVal _filter As ChucvuDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        <OperationContract()>
        Function InsertChucvuBld(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function ValidateChucvuBldCode(ByVal objTitle As ChucvuDTO) As Boolean

        <OperationContract()>
        Function ModifyChucvuBld(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveChucvuBld(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteChucvuBld(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChucvuBldByID(ByVal sID As Decimal) As List(Of ChucvuDTO)
        <OperationContract()>
        Function GetChucvuBldID(ByVal ID As Decimal) As ChucvuDTO
        <OperationContract()>
        Function GetTitleBLDsByOrg(ByVal org_id As Decimal, ByVal emp_id As Decimal?) As List(Of ChucvuDTO)

#End Region

#Region "Chucvu bld"

        <OperationContract()>
        Function GetChucvuTbl(ByVal _filter As ChucvuDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChucvuDTO)

        <OperationContract()>
        Function InsertChucvuTbl(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function ValidateChucvuTblCode(ByVal objTitle As ChucvuDTO) As Boolean

        <OperationContract()>
        Function ModifyChucvuTbl(ByVal objTitle As ChucvuDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveChucvuTbl(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteChucvuTbl(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChucvuTblByID(ByVal sID As Decimal) As List(Of ChucvuDTO)
        <OperationContract()>
        Function GetChucvuTblID(ByVal ID As Decimal) As ChucvuDTO

#End Region

#Region "TitleConcurrent"
        <OperationContract()>
        Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleConcurrentDTO)
        <OperationContract()>
        Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)

        <OperationContract()>
        Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "Document"
        <OperationContract()>
        Function GetAll_Document(ByVal _filter As DocumentDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DocumentDTO)
        <OperationContract()>
        Function ActiveDocument(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteDocument(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertDocument(ByVal objDocument As DocumentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyDocument(ByVal objTitle As DocumentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function Check_Exit_Document(ByVal objDocument As DocumentDTO) As Decimal
#End Region
#Region "ContractType"

        <OperationContract()>
        Function GetContractType(ByVal _filter As ContractTypeDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)

        <OperationContract()>
        Function InsertContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateContractType(ByVal objContractType As ContractTypeDTO) As Boolean

        <OperationContract()>
        Function ModifyContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteContractType(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region
#Region "bhld item"

        <OperationContract()>
        Function GetBHLDItem(ByVal _filter As BHLDItemDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BHLDItemDTO)

        <OperationContract()>
        Function InsertBHLDItem(ByVal objContractType As BHLDItemDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function checkOrderNum(ByVal id As Decimal, ByVal num As Decimal) As Decimal

        <OperationContract()>
        Function ValidateBHLDItem(ByVal objContractType As BHLDItemDTO) As Boolean

        <OperationContract()>
        Function ModifyBHLDItem(ByVal objContractType As BHLDItemDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveBHLDItem(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteBHLDItem(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "WelfareList"
        <OperationContract()>
        Function GET_INFO_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function GetlistWelfareEMP(ByVal Id As Integer) As List(Of Welfatemng_empDTO)
        <OperationContract()>
        Function GET_DETAILS_EMP(ByVal P_ID As Decimal, ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function GET_EXPORT_EMP(ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GetWelfareList(ByVal _filter As WelfareListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)

        <OperationContract()>
        Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateWelfareList(ByVal objWelfareList As WelfareListDTO) As Boolean

        <OperationContract()>
        Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteWelfareList(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "AllowanceList"

        <OperationContract()>
        Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        <OperationContract()>
        Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAllowanceList(ByVal objAllowanceList As AllowanceListDTO) As Boolean

        <OperationContract()>
        Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteAllowanceList(ByVal lstAllowanceList() As AllowanceListDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "RelationShipList"
        <OperationContract()>
        Function GetRelationshipGroupList() As DataTable

        <OperationContract()>
        Function GetRelationshipList(ByVal _filter As RelationshipListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)

        <OperationContract()>
        Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateRelationshipList(ByVal objRelationshipList As RelationshipListDTO) As Boolean

        <OperationContract()>
        Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteRelationshipList(ByVal lstRelationshipList() As RelationshipListDTO, ByVal log As UserLog) As Boolean

#End Region


#Region "Organization"
        <OperationContract()>
        Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO
        <OperationContract()>
        Function GetOrganization(ByVal sACT As String) As List(Of OrganizationDTO)

        <OperationContract()>
        Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO

        <OperationContract()>
        Function InsertOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateOrganization(ByVal objOrganization As OrganizationDTO) As Boolean

        <OperationContract()>
        Function ValidateCostCenterCode(ByVal objOrganization As OrganizationDTO) As Boolean

        <OperationContract()>
        Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ModifyOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean

        <OperationContract()>
        Function ActiveOrganization(ByVal objOrganization() As OrganizationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateUy_Ban_Organization(ByVal lstOrganization() As OrganizationDTO, ByVal sValue As Decimal, ByVal log As UserLog) As Boolean
#End Region

#Region "OrgTitle"

        <OperationContract()>
        Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)

        <OperationContract()>
        Function InsertOrgTitle(ByVal objOrgTitle As List(Of OrgTitleDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

#End Region

#Region "Danh muc tham so he thong"
        ''' <summary>
        ''' Validate Tham so he thong
        ''' </summary>
        ''' <param name="objOtherList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateOtherList(ByVal objOtherList As OtherListDTO) As Boolean
#End Region

#Region "Nation - Danh mục quốc gia"
        ''' <summary>
        ''' Lay danh sach Quoc gia
        ''' </summary>
        ''' <returns>danh sach Quoc gia</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Quoc gia
        ''' </summary>
        ''' <param name="objNation"> Doi tuong quoc gia can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Quoc gia
        ''' </summary>
        ''' <param name="objNation">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateNation(ByVal objNation As NationDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Quoc gia
        ''' </summary>
        ''' <param name="objNation">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveNation(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục quốc gia
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Province - Danh mục Tinh Thanh"
        ''' <summary>
        ''' Lay danh sach tinh thanh theo quoc gia
        ''' </summary>
        ''' <param name="sNationID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO)


        <OperationContract()>
        Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO)


        ''' <summary>
        ''' Lay danh sach Tinh thanh
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Tinh thanh
        ''' </summary>
        ''' <param name="objProvince"> Doi tuong Tinh thanh can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Tinh thanh
        ''' </summary>
        ''' <param name="objProvince">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateProvince(ByVal objProvince As ProvinceDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Tinh thanh
        ''' </summary>
        ''' <param name="objProvince">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục tỉnh thành
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "District - Danh mục Quan Huyen"
        ''' <summary>
        ''' Lay danh sach Quan Huyen
        ''' </summary>
        ''' <returns>danh sach Quan Huyen</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Lay danh sach quan huyen theo ProvinceID
        ''' </summary>
        ''' <param name="sProvinceID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict"> Doi tuong Quan Huyen can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        <OperationContract()>
        Function ValidateDistrict(ByVal _validate As DistrictDTO) As Boolean

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục quận huyện
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean

#End Region

#Region "Ward - Danh mục xã phường"
        ''' <summary>
        ''' Lay danh sach Quan Huyen
        ''' </summary>
        ''' <returns>danh sach Quan Huyen</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Lay danh sach xã phường theo districtID
        ''' </summary>
        ''' <param name="sProvinceID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWardByDistrictID(ByVal sDistrictID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi xã phường
        ''' </summary>
        ''' <param name="objDistrict"> Doi tuong Quan Huyen can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin xã phường
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        <OperationContract()>
        Function ValidateWard(ByVal _validate As Ward_DTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng xã phường
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Xóa danh mục xã phường
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "Bank - Danh mục Ngan hang"
        ''' <summary>
        ''' Lay danh sach Ngan hang
        ''' </summary>
        ''' <returns>danh sach Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)

        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Ngan hang
        ''' </summary>
        ''' <param name="objBank"> Doi tuong Ngan hang can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Ngan hang
        ''' </summary>
        ''' <param name="objBank">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateBank(ByVal objBank As BankDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Ngan hang
        ''' </summary>
        ''' <param name="objBank">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveBank(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục ngân hàng
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "BankBranch - Danh mục Chi nhanh Ngan hang"
        ''' <summary>
        ''' Lay danh sach Chi nhanh Ngan hang
        ''' </summary>
        ''' <returns>danh sach Chi nhanh Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)

        ''' <summary>
        ''' Lay danh sach Chi nhanh Ngan hang theo Bank_ID
        ''' </summary>
        ''' <returns>danh sach Chi nhanh Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBankBranchByBankID(ByVal sBank_Id As Decimal) As List(Of BankBranchDTO)


        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch"> Doi tuong Chi nhanh Ngan hang can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateBankBranch(ByVal objBankBranch As BankBranchDTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveBankBranch(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mcuj chi nhánh ngân hàng
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Asset - Danh muc tai san cap phat"
        ''' <summary>
        ''' Lay danh sach Tai san cap phat
        ''' </summary>
        ''' <returns>danh sach Tai san cap phat</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset"> Doi tuong Tai san cap phat can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAsset(ByVal objAsset As AssetDTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveAsset(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa tài sản cấp phát
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "StaffRank"
        <OperationContract()>
        Function GetStaffRank(ByVal _filter As StaffRankDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)

        <OperationContract()>
        Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateStaffRank(ByVal objStaffRank As StaffRankDTO) As Boolean

        <OperationContract()>
        Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean

        <OperationContract()>
        Function DeleteStaffRank(ByVal lstStaffRank() As StaffRankDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Năng lực"

#Region "CompetencyGroup"

        <OperationContract()>
        Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)

        <OperationContract()>
        Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "Competency"

        <OperationContract()>
        Function GetCompetency(ByVal _filter As CompetencyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)

        <OperationContract()>
        Function InsertCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCompetency(ByVal objCompetency As CompetencyDTO) As Boolean

        <OperationContract()>
        Function ModifyCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCompetency(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyBuild"

        <OperationContract()>
        Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)

        <OperationContract()>
        Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyStandard"

        <OperationContract()>
        Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)

        <OperationContract()>
        Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyAppendix"

        <OperationContract()>
        Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)

        <OperationContract()>
        Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyEmp"

        <OperationContract()>
        Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO)

        <OperationContract()>
        Function UpdateCompetencyEmp(ByVal lstCom As List(Of CompetencyEmpDTO), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyPeriod"

        <OperationContract()>
        Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)

        <OperationContract()>
        Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyAssDtl"

        <OperationContract()>
        Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO)

        <OperationContract()>
        Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO)

        <OperationContract()>
        Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstCom As List(Of CompetencyAssDtlDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#End Region

#End Region

#Region "Bussiness"

#Region "EmployeeBussiness - Nghiệp vụ hồ sơ"


        ''' <summary>
        ''' Lay thong tin nhan vien tu EmployeeCode
        ''' </summary>
        ''' <param name="sEmployeeCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO
        <OperationContract()>
        Function GetEmployeeByEmployeeIDPortal(ByVal empID As Decimal) As EmployeeDTO


        ''' <summary>
        ''' Trả về binary của ảnh hồ sơ nhân viên
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()
        <OperationContract()>
        Function GetEmployeeImageEdit(ByVal gEmpID As Decimal, ByVal Status As String) As Byte()
        <OperationContract()>
        Function EmployeeImage(ByVal userId As Decimal, ByRef sError As String) As Byte()
        ''' <summary>
        ''' Hàm lấy đường dẫn ảnh HSNV để in CV trên portal
        ''' <creater>TUNGLD</creater>
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="isOneEmployee"></param>
        ''' <param name="img_link"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeImage_PrintCV(ByVal gEmpID As Decimal) As String
        ''' <summary>
        ''' Thêm mới nhân viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objBankAccLog"></param>
        ''' <param name="objEmpHealth"></param>
        ''' <param name="objEmpSalary"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                  ByRef _strEmpCode As String,
                                  ByVal _imageBinary As Byte(),
                                  Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                  Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                  Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean
        <OperationContract()>
        Function CreateNewEMPLOYEECode() As EmployeeDTO

        ''' <summary>
        ''' Sửa thông tin nhân viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objBankAccLog"></param>
        ''' <param name="objEmpHealth"></param>
        ''' <param name="objEmpSalary"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                  ByVal _imageBinary As Byte(),
                                  Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                  Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                  Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        ''' <summary>
        ''' Lấy danh sách nhân viên theo điều kiện
        ''' </summary>
        ''' <param name="_orgIds"></param>s
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeOrgChart(ByVal lstOrg As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As List(Of OrgChartDTO)

        ''' <summary>
        ''' Lấy danh sách nhân viên bao gồm ảnh để hiển thị trên org chart
        ''' </summary>
        ''' <param name="_orgIds"></param>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)


        ''' <summary>
        ''' Lấy danh sách nhân viên có phân trang
        ''' </summary>
        ''' <param name="PageIndex"></param>
        ''' <param name="PageSize"></param>
        ''' <param name="Total"></param>
        ''' <param name="_orgIds"></param>
        ''' <param name="_filter"></param>
        ''' <param name="Sorts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        <OperationContract()>
        Function GetListEmployeePagingEx(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)

        <OperationContract()>
        Function GetListWorkingBefore(ByVal _filter As WorkingBeforeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_ID",
                                          Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTO)

        <OperationContract()>
        Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        <OperationContract()>
        Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)

        ''' <summary>
        ''' Lấy thông tin EmployeeCV 
        ''' </summary>
        ''' <param name="sEmployeeID">ID(Decimal) của nhân viên</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO) As Boolean

        ''' <summary>
        ''' Xóa nhân viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean

        <OperationContract()>
        Function ValidateEmployee(ByVal sEmpCode As String, ByVal value As String, ByVal sType As String) As Boolean

        ''' <summary>
        ''' Hàm kiểm tra nhân viên đã có hợp đồng chưa
        ''' </summary>
        ''' <param name="strEmpCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean

        <OperationContract()>
        Function GetEmployeePerInfo(ByVal _emp_id As Decimal) As EmployeeCVDTO

        <OperationContract()>
        Function ValidateEmpWorkEmail(ByVal _email As String) As Boolean

        <OperationContract()>
        Function GetEmployeeByEmail(ByVal _email As String,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)

        <OperationContract()>
        Function PortalSendImage(ByVal employeeCode As String, ByVal userID As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String) As Boolean

        <OperationContract()>
        Function GetEmployeeCuriculumVitae(ByVal empID As Decimal, ByRef empCV As EmployeeCVDTO, ByRef empHealth As EmployeeHealthDTO) As EmployeeDTO
#End Region

#Region "EmployeeEdit"
        <OperationContract()>
        Function GetChangedCVList(ByVal lstEmpEdit As List(Of EmployeeEditDTO)) As Dictionary(Of String, String)

        <OperationContract()>
        Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function UpdateImg_EmployeeEdit_Mobile(ByVal _ID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmployeeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO

        <OperationContract()>
        Function SendEmployeeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeEditDTO)

#End Region

#Region "EmployeeFamily"
        <OperationContract()>
        Function CheckChuho(ByVal emp_id As Decimal, ByVal fa_id As Decimal) As Decimal
        <OperationContract()>
        Function GetFamilyByID(ByVal id As Decimal) As FamilyDTO
        <OperationContract()>
        Function GetWorkingBeforeByID(ByVal id As Decimal) As WorkingBeforeDTO
        <OperationContract()>
        Function GetEmployeeFamily_1(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO)
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


        ''' <summary>
        ''' Check trùng CMND của nhân thân.
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateFamily(ByVal _validate As FamilyDTO) As Boolean
#End Region

#Region "EmployeeFamilyEdit"
        ''' <summary>
        ''' Lay danh sach than nhan thay doi de to mau
        ''' </summary>
        ''' <param name="lstFamilyEdit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChangedFamilyList(ByVal lstFamilyEdit As List(Of FamilyEditDTO)) As Dictionary(Of String, String)
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamilyEdit"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamilyEdit"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistFamilyEdit(ByVal pk_key As Decimal, ByVal tab As Decimal) As FamilyEditDTO

        <OperationContract()>
        Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ReadNotifi(ByVal id As Decimal) As Decimal
        <OperationContract()>
        Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                                  status As String,
                                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO)
        <OperationContract()>
        Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        <OperationContract()>
        Function GetChangedEmployeeCertificateList(ByVal lstEmpEdit As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)) As Dictionary(Of String, String)
#End Region

#Region "EmployeeTrain"
        <OperationContract()>
        Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO)
        <OperationContract()>
        Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO)
        <OperationContract()>
        Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO
        <OperationContract()>
        Function InsertEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteEmployeeTrain(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean
#End Region

#Region "WorkingBefore"

        <OperationContract()>
        Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO)

        <OperationContract()>
        Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingBefore(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "AssetMng"

        <OperationContract()>
        Function GetAssetMng(ByVal _filter As AssetMngDTO,
                             ByVal _param As ParamDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of AssetMngDTO)

        <OperationContract()>
        Function GetAssetMngById(ByVal Id As Integer
                                        ) As AssetMngDTO

        <OperationContract()>
        Function InsertAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAssetMng(ByVal objAssetMng() As AssetMngDTO, ByVal log As UserLog) As Boolean
#End Region
#Region "evaluate"
        <OperationContract()>
        Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO)
        <OperationContract()>
        Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningEvaluateDTO)
        <OperationContract()>
        Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTrainingEvaluateByID(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        <OperationContract()>
        Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteTrainingEvaluate(ByVal objAssetMng As TrainningEvaluateDTO) As Boolean

#End Region
#Region "training manage"
        <OperationContract()>
        Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        <OperationContract()>
        Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        <OperationContract()>
        Function InsertTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTrainingManage(ByVal objAssetMng As TrainningManageDTO) As Boolean
        <OperationContract()>
        Function GetTrainingManageByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        <OperationContract()>
        Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

#Region "trainingforeign"
        <OperationContract()>
        Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningForeignDTO)
        <OperationContract()>
        Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTrainingForeignByID(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        <OperationContract()>
        Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteTrainingForeign(ByVal objAssetMng As TrainningForeignDTO) As Boolean

#End Region
#Region "emp bank"
        <OperationContract()>
        Function saveEmpBank(ByVal dt As DataTable,
                                Optional ByVal log As UserLog = Nothing) As Decimal
        <OperationContract()>
        Function GetEmpBank(ByVal _filter As EmpBankDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of EmpBankDTO)
#End Region
#Region "Contract"
        <OperationContract()>
        Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO)

        <OperationContract()>
        Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO

        <OperationContract()>
        Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean
        <OperationContract()>
        Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean
        <OperationContract()>
        Function CheckStartDate1(ByVal objContract As ContractDTO) As Boolean
        <OperationContract()>
        Function CheckStartDate(ByVal objContract As ContractDTO) As Boolean
        <OperationContract()>
        Function InsertContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function ModifyContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteContract(ByVal objAssetMng As ContractDTO) As Boolean

        <OperationContract()>
        Function CreateContractNo(ByVal objAssetMng As ContractDTO) As String

        <OperationContract()>
        Function CheckContractNo(ByVal objAssetMng As ContractDTO) As Boolean

        <OperationContract()>
        Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        <OperationContract()>
        Function GetMaxConId(empId) As Decimal

        <OperationContract()>
        Function UnApproveContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetCheckContractTypeID(ByVal listID As String) As DataTable

#End Region

#Region "WelfareMng"
        <OperationContract()>
        Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO)
        <OperationContract()>
        Function GetWelfareMngById(ByVal Id As Integer) As WelfareMngDTO

        <OperationContract()>
        Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean

        <OperationContract()>
        Function InsertWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteWelfareMng(ByVal lstWelfareMng() As WelfareMngDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "Working"
        <OperationContract()>
        Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        <OperationContract()>
        Function GetWorking_1(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        <OperationContract()>
        Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO)

        <OperationContract()>
        Function GET_EMPDTL_HU_ALLOWAMCE(ByVal _filter As HUAllowanceDTO,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO)


        <OperationContract()>
        Function InsertWorkingAllowance(ByVal objWorking As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingAllowance(ByVal lstWorkingAllowance() As HUAllowanceDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyWorkingAllowanceNew(ByVal objWorking As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetWorkingByID_1(ByVal _filter As WorkingDTO) As WorkingDTO

        <OperationContract()>
        Function GetWorkingByID(ByVal _filter As WorkingDTO) As WorkingDTO
        <OperationContract()>
        Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO
        <OperationContract()>
        Function InsertListWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetEmpSaved(ByVal Id As Integer) As EmployeeDTO
        <OperationContract()>
        Function InsertMngProfileSaved(ByVal lstMngProSaved As MngProfileSavedDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateNewEdit(ByVal objWorking As HUAllowanceDTO) As Boolean

        <OperationContract()>
        Function DeleteWorking(ByVal objAssetMng As WorkingDTO) As Boolean

        <OperationContract()>
        Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean

        <OperationContract()>
        Function ValEffectdateByEmpCode(ByVal emp_code As String, ByVal effect_date As Date) As Boolean

        <OperationContract()>
        Function GetLastWorkingSalary(ByVal _filter As WorkingDTO) As WorkingDTO

        <OperationContract()>
        Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)


        <OperationContract()>
        Function InsertWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean

        <OperationContract()>
        Function GetChangeInfoImport(ByVal param As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ImportChangeInfo(ByVal lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UnApproveWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetDataExport() As DataSet
        <OperationContract()>
        Function ApproveWorkings(ByVal ids As List(Of Decimal), ByVal acti As Decimal, Optional ByVal log As UserLog = Nothing) As CommandResult
        <OperationContract()>
        Function CheckHasFile(ByVal id As List(Of Decimal)) As Decimal
#End Region

#Region "Discipline"
        <OperationContract()>
        Function CheckHasFileDiscipline(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function ApproveListDiscipline(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineEmpDTO)
        <OperationContract()>
        Function GetOrgDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineOrgDTO)
        <OperationContract()>
        Function GetDiscipline(ByVal _filter As DisciplineDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)

        <OperationContract()>
        Function GetDisciplineByID(ByVal _filter As DisciplineDTO) As DisciplineDTO

        <OperationContract()>
        Function InsertDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateDiscipline(ByVal sType As String, ByVal obj As DisciplineDTO) As Boolean

        <OperationContract()>
        Function DeleteDiscipline(ByVal objAssetMng() As DisciplineDTO) As Boolean

        <OperationContract()>
        Function ApproveDiscipline(ByVal objDiscipline As DisciplineDTO) As Boolean

        <OperationContract()>
        Function Open_ApproveDiscipline(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "DisciplineSalary"

        <OperationContract()>
        Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal log As UserLog = Nothing,
                                     Optional ByVal Sorts As String = "YEAR,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO)

        <OperationContract()>
        Function GetDisciplineSalaryByID(ByVal _filter As DisciplineSalaryDTO) As DisciplineSalaryDTO

        <OperationContract()>
        Function EditDisciplineSalary(ByVal obj As DisciplineSalaryDTO) As Boolean

        <OperationContract()>
        Function ValidateDisciplineSalary(ByVal obj As DisciplineSalaryDTO, ByRef sError As String) As Boolean

        <OperationContract()>
        Function ApproveDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function StopDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean

#End Region
#Region "health exam"

        <OperationContract()>
        Function DeleteHealthExam(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetHealthExam(ByVal _filter As HealthExamDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HealthExamDTO)
#End Region
#Region "Commend"
        <OperationContract()>
        Function CheckHasFileComend(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function GetCommend(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)
        <OperationContract()>
        Function GetEmployeeCommendByID(ByVal ComId As Decimal) As List(Of CommendEmpDTO)

        <OperationContract()>
        Function GetOrgCommendByID(ByVal ComId As Decimal) As List(Of CommendOrgDTO)

        <OperationContract()>
        Function GetCommendByID(ByVal _filter As CommendDTO) As CommendDTO

        <OperationContract()>
        Function InsertCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCommend(ByVal sType As String, ByVal obj As CommendDTO) As Boolean

        <OperationContract()>
        Function DeleteCommend(ByVal objAssetMng As CommendDTO) As Boolean

        <OperationContract()>
        Function ApproveCommend(ByVal objCommend As CommendDTO) As Boolean

        '<OperationContract()>
        'Function GetCommendCode(ByVal id As Decimal) As String

        <OperationContract()>
        Function ApproveListCommend(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        '<OperationContract()>
        'Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean
        '<OperationContract()>
        'Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
        '<OperationContract()>
        'Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
        '                                ByVal PageSize As Integer,
        '                                ByRef Total As Integer,
        '                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)
        '<OperationContract()>
        'Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO
        '<OperationContract()>
        'Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
        '                           ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
        '                         ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        '<OperationContract()>
        'Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
        '                          ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetCommendCode(ByVal id As Decimal) As String
#End Region

#Region "Debt"
        <OperationContract()>
        Function GetDebt(ByVal Id As Decimal) As DebtDTO
        <OperationContract()>
        Function InsertDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteDebt(ByVal obj As List(Of Decimal)) As Boolean
#End Region
#Region "Accident"
        <OperationContract()>
        Function GetAccident(ByVal _filter As AccidentDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of AccidentDTO)

        <OperationContract()>
        Function DeleteAccident(ByVal objID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetAccidentByID(ByVal _filter As AccidentDTO) As AccidentDTO
        <OperationContract()>
        Function InsertAccident(ByVal objTerminate As AccidentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAccident(ByVal objTerminate As AccidentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region
#Region "Terminate"
        <OperationContract()>
        Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable
        <OperationContract()>
        Function Check_has_Ter(ByVal empid As Decimal) As Decimal
        <OperationContract()>
        Function GetMoneyReimburseTerminate(ByVal EmployeeId As Decimal) As Decimal
        <OperationContract()>
        Function ApproveListTerminate(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable

        <OperationContract()>
        Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO)

        <OperationContract()>
        Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO)

        <OperationContract()>
        Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        <OperationContract()>
        Function GetTerminateSeverance(ByVal _filter As TerminateDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        <OperationContract()>
        Function GetPensionBenefits(ByVal _filter As TerminateDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)

        <OperationContract()>
        Function UpdatePensinBenefits(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyTerminate_TV(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetTerminateCopy(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)
        <OperationContract()>
        Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO

        <OperationContract()>
        Function GetEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        <OperationContract()>
        Function GetListEmployeeConcurrentlyByID(ByVal gEmployeeID As Decimal) As List(Of EmployeeDTO)

        <OperationContract()>
        Function InsertTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function Delete_Ins_Arising_While_Unapprove(ByVal empID As Decimal, ByVal effect_Date As Date, ByVal Log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteTerminate(ByVal objID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteBlackList(ByVal objID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ApproveTerminate(ByVal objTerminate As TerminateDTO) As Boolean

        <OperationContract()>
        Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean

        <OperationContract()>
        Function CheckConcurrentlyExpireDate(ByVal objTerminate As TerminateDTO) As Decimal

        <OperationContract()>
        Function GetTyleNV() As DataTable

        <OperationContract()>
        Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable
#End Region

#Region "Terminate3b"
        <OperationContract()>
        Function GetTerminate3b(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of Terminate3BDTO)
        <OperationContract()>
        Function GetTerminate3bByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO

        <OperationContract()>
        Function GetTerminate3bEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO

        <OperationContract()>
        Function InsertTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTerminate3b(ByVal objID As Decimal) As Boolean
        <OperationContract()>
        Function ApproveTerminate3b(ByVal objTerminate3b As Terminate3BDTO) As Boolean

        <OperationContract()>
        Function CheckExistApproveTerminate3b(ByVal gID As Decimal) As Boolean

#End Region

#Region "File Management, Tệp văn bản"
        <OperationContract()>
        Function InsertAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        <OperationContract()>
        Function UpdateAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        <OperationContract()>
        Function DeleteAttatch_Manager(ByVal fileID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetAttachFile_Manager(ByVal fileId As Decimal) As HuFileDTO
        <OperationContract()>
        Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of HuFileDTO)
        <OperationContract()>
        Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByVal ext As String, ByRef fileInfo As HuFileDTO) As Byte()
        <OperationContract()>
        Function GetEmployeeHuFile(ByVal _filter As HuFileDTO) As List(Of HuFileDTO)
        <OperationContract()>
        Function TestEmployeeFileDTO() As EmployeeFileDTO

#End Region


#Region "IPORTAL - Quá trình đào tạo trước khi vào công ty"
        <OperationContract()>
        Function GetCertificateType() As List(Of OtherListDTO)
        <OperationContract()>
        Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

        <OperationContract()>
        Function InsertProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProcessTrainingEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT

        <OperationContract()>
        Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

#End Region

#Region "IPORTAL - Qúa trình công tác trước khi vào công ty"""
        <OperationContract()>
        Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit)

        <OperationContract()>
        Function InsertWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingBeforeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit

        <OperationContract()>
        Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTOEdit)
        <OperationContract()>
        Function GetChangedWorkingBeforeList(ByVal lstWorkingBeforeEdit As List(Of WorkingBeforeDTOEdit)) As Dictionary(Of String, String)

        <OperationContract()>
        Function GetFileForView(ByVal fileUpload As String) As FileUploadDTO

        <OperationContract()>
        Function GetFileByte_Userfile(ByVal fileUpload As String) As Byte()
#End Region
#End Region

#Region "Hoadm"

#Region "Get Combo Data"
        ''' <summary>
        ''' Lấy dữ liệu cho combobox
        ''' </summary>
        ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
        ''' <returns>TRUE: Success</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO, Optional log As UserLog = Nothing) As Boolean
#End Region

#Region "Reminder"
        ''' <summary>
        ''' Lấy danh sách nhắc nhở
        ''' </summary>
        ''' <param name="_dayRemind"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetRemind(ByVal _dayRemind As String, ByVal log As UserLog, Optional ByVal _orgID As Decimal = 1) As List(Of ReminderLogDTO)

#End Region

#Region "Employee Proccess"
        ''' <summary>
        ''' Lấy danh sách nhân thân
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetFamily(ByVal _empId As Decimal) As List(Of FamilyDTO)

        ''' <summary>
        ''' Lấy quá trình công tác trước khi vào công ty
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWorkingBefore(ByVal _empId As Decimal) As List(Of WorkingBeforeDTO)

        ''' <summary>
        ''' Lấy quá trình công tác trong công ty
        ''' </summary>
        ''' <param name="_empCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWorkingProccess(ByVal _empId As Decimal?,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        ''' <summary>
        ''' Lấy quá trình lương
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetSalaryProccess(ByVal _empId As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        ''' <summary>
        ''' Lấy quá trình phúc lợi
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWelfareProccess(ByVal _empId As Decimal) As List(Of WelfareMngDTO)

        ''' <summary>
        ''' Lấy quá trình hợp đồng
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetContractProccess(ByVal _empId As Decimal) As List(Of ContractDTO)

        ''' <summary>
        ''' Lấy quá trình khen thưởng
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        <OperationContract()>
        Function GetCommendProccess(ByVal _empId As Decimal) As List(Of CommendDTO)
        ''' <summary>
        ''' Lấy quá trình kỷ luật
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetConcurrentlyProccess(ByVal _empId As Decimal) As List(Of TitleConcurrentDTO)
        <OperationContract()>
        Function GetDisciplineProccess(ByVal _empId As Decimal) As List(Of DisciplineDTO)
        ''' <summary>
        ''' Lấy quá trình bảo hiểm
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetInsuranceProccess(ByVal _empId As Decimal) As DataTable

        <OperationContract()>
        Function GetEmployeeHistory(ByVal _empId As Decimal) As DataTable

        ''' <summary>
        ''' Qua trinh danh gia KPI
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetAssessKPIEmployee(ByVal _empId As Decimal) As List(Of EmployeeAssessmentDTO)

        'qua trinh nang luc
        <OperationContract()>
        Function GetCompetencyEmployee(ByVal _empId As Decimal) As List(Of EmployeeCompetencyDTO)

#End Region

#Region "Quá trình đào tạo ngoài công ty"
        <OperationContract()>
        Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                      Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        <OperationContract()>
        Function InsertProcessTraining(ByVal objHuPro As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProcessTraining(ByVal objHuPro As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProcessTraining(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CheckExistEmployeeCertificate_IsMain(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean

        <OperationContract()>
        Function CheckExistEmployeeCertificate_IsMajor(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean
#End Region

#Region "Service Auto Update Employee Information"
        <OperationContract()>
        Function CheckAndUpdateEmployeeInformation() As Boolean
#End Region

#Region "Service Send Mail Reminder"
        <OperationContract()>
        Function CheckAndSendMailReminder() As Boolean
#End Region

#End Region
#Region "Manage annual leave plans (ALP)"
        <OperationContract()>
        Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        <OperationContract()>
        Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        <OperationContract()>
        Function InsertALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteALP(ByVal objAssetMng As TrainningManageDTO) As Boolean
        <OperationContract()>
        Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        <OperationContract()>
        Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean
        <OperationContract()>
        Function GetALPByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        <OperationContract()>
        Function ModifyALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region
#Region "Reports"
#Region "Chart"
        <OperationContract()>
        Function Chart_HDLD(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_Age(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_TRINHDO_HOCVAN(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_GENDER(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_TNCT(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_HANHCHINH(ByVal param As ParamDTO,
                                           ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function Chart_TRINHDO_NGOAINGU(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_EmpObj(ByVal param As ParamDTO,
                                       ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function Chart_BAC_LAO_DONG(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_NEW_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_TER_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_BO_NHIEM(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_Employee_Num(ByVal param As ParamDTO, ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function Chart_WorkPlace(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable

        <OperationContract()>
        Function ExportChartReportlist_Data(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, ByVal Kind_report_func As String) As Byte()
#End Region
#Region "Dynamic"
        <OperationContract()>
        Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO)
        <OperationContract()>
        Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO)
        <OperationContract()>
        Function DeleteDynamicReport(ByVal ID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetDynamicReportList() As Dictionary(Of Decimal, String)
        ''' <summary>
        ''' Lấy danh sách các cột của DynamicReport
        ''' </summary>
        ''' <param name="_reportID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO)

        ''' <summary>
        ''' Lấy dữ liệu báo cáo động
        ''' </summary>
        ''' <param name="column">Danh sách các cột cần lấy</param>
        ''' <param name="condition">Expression điều kiện</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDynamicReport(ByVal _reportID As Decimal,
                                     ByVal orgID As Decimal,
                                     ByVal isDissolve As Decimal,
                                     ByVal chkTerminate As Decimal,
                                     ByVal chkHasTerminate As Decimal,
                                     ByVal column As List(Of String),
                                     ByVal condition As String,
                                     ByVal log As UserLog) As DataTable
#End Region


#End Region

#Region "Dashboard"
        ''' <summary>
        ''' Lấy danh sách loại thống kê nhân viên
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployeeStatistic() As List(Of OtherListDTO)

        ''' <summary>
        ''' Lấy danh sách loại thống kê biến động
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListChangeStatistic() As List(Of OtherListDTO)

        ''' <summary>
        ''' Lấy nội dung thống kê nhân viên
        ''' </summary>
        ''' <param name="_type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)

        ''' <summary>
        ''' Lấy nội dung thống kê biến động
        ''' </summary>
        ''' <param name="_type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChangeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetCompanyNewInfo(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSeniority(ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of StatisticDTO)

#End Region

#Region "Hoadm"
        <OperationContract()>
        Function GetOrgFromUsername(ByVal username As String) As Decimal?

        <OperationContract()>
        Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)

#End Region

#Region "Danh mục bảo hộ lao động"

        <OperationContract()>
        Function GetLabourProtection(ByVal _filter As LabourProtectionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionDTO)
        <OperationContract()>
        Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateLabourProtection(ByVal _validate As LabourProtectionDTO) As Boolean
        <OperationContract()>
        Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO)
        <OperationContract()>
        Function GetLabourProtectionMngById(ByVal Id As Integer
                                        ) As LabourProtectionMngDTO
        <OperationContract()>
        Function InsertLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteLabourProtectionMng(ByVal objLabourProtectionMng() As LabourProtectionMngDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Quản lý an toàn lao động"
        <OperationContract()>
        Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO)
        <OperationContract()>
        Function GetOccupationalSafetyById(ByVal Id As Integer
                                       ) As OccupationalSafetyDTO
        <OperationContract()>
        Function InsertOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteOccupationalSafety(ByVal objOccupationalSafety() As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "Danh mục thông tin lương"
        <OperationContract()>
        Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)

#End Region

#Region "BÁO CÁO"
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        <OperationContract()>
        Function ProfileReport(ByVal sPkgName As String, ByVal sStartDate As Date?, ByVal sEndDate As Date?, ByVal sOrg As Integer, ByVal sUserName As String, ByVal sLang As String) As DataTable

        <OperationContract()>
        Function ExportReport(ByVal sPkgName As String,
                              ByVal sStartDate As Date?,
                              ByVal sEndDate As Date?,
                              ByVal sOrg As String,
                              ByVal IsDissolve As Integer,
                              ByVal sUserName As String, ByVal sLang As String) As DataSet

        <OperationContract()>
        Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet

#End Region
#Region "DM Khen thưởng (CommendList)"
        <OperationContract()>
        Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)

        <OperationContract()>
        Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO)

        <OperationContract()>
        Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO)

        <OperationContract()>
        Function InsertCommendList(ByVal objCommendList As CommendListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommendList(ByVal objCommendList As CommendListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean

        <OperationContract()>
        Function ValidateCommendList(ByVal _validate As CommendListDTO)

        <OperationContract()>
        Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
#End Region
#Region "DM Kỷ luật (DisciplineList)"
        <OperationContract()>
        Function GetDisciplineList(ByVal _filter As DisciplineListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineListDTO)

        <OperationContract()>
        Function GetDisciplineListID(ByVal ID As Decimal) As List(Of DisciplineListDTO)

        <OperationContract()>
        Function GetListDisciplineList(ByVal actflg As String) As List(Of DisciplineListDTO)

        <OperationContract()>
        Function InsertDisciplineList(ByVal objDisciplineList As DisciplineListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyDisciplineList(ByVal objDisciplineList As DisciplineListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveDisciplineList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteDisciplineList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean

        <OperationContract()>
        Function ValidateDisciplineList(ByVal _validate As DisciplineListDTO)

        <OperationContract()>
        Function GetDisciplineCode(ByVal id As Decimal) As String
#End Region
#Region "Commend_Level - Cấp khen thưởng"
        <OperationContract()>
        Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)
        <OperationContract()>
        Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO

        <OperationContract()>
        Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO)

        <OperationContract()>
        Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "CommendFormula - Công thức khen thưởng"
        <OperationContract()>
        Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)

        <OperationContract()>
        Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO

        <OperationContract()>
        Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Competency Dashboard"
        <OperationContract()>
        Function GetStatisticTop5Competency(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTop5CopAvg(ByVal _year As Integer, ByVal log As UserLog) As List(Of CompetencyAvgEmplDTO)
#End Region
#Region "Portal Dashboard"
        <OperationContract()>
        Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        <OperationContract()>
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                              Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        <OperationContract()>
        Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
#End Region
#Region "Competency Course"

        <OperationContract()>
        Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)

        <OperationContract()>
        Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region

#Region "EmployeeCriteriaRecord"
        <OperationContract()>
        Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO)
#End Region
#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
        <OperationContract()>
        Function GetPortalCompetencyCourse(ByVal _empId As Decimal) As List(Of EmployeeCriteriaRecordDTO)
#End Region

#Region "Tìm kiếm kế nhiệm (Talent Pool)"

        <OperationContract()>
        Function GetTalentPool(ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TalentPoolDTO)

        <OperationContract()>
        Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ActiveTalentPool(ByVal objTalentPool As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO, ByVal log As UserLog) As DataTable
#End Region

#Region "in phu luc"
        <OperationContract()>
        Function PrintFileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable

        <OperationContract()>
        Function GetContractForm(ByVal formID As Decimal) As OtherListDTO

        <OperationContract()>
        Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO

        <OperationContract()>
        Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO)

        <OperationContract()>
        Function GetEmpDtlContractAppendixPaging(ByVal _filter As FileContractDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO)

        <OperationContract()>
        Function DeleteFileContract(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO)

        <OperationContract()>
        Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean

        <OperationContract()>
        Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal ID As Decimal) As Boolean

        <OperationContract()>
        Function InsertFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef appenNum As String) As Boolean

        <OperationContract()>
        Function UpdateFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO)

        <OperationContract()>
        Function GetTitileBaseOnEmp(ByVal ID As Decimal) As TitleDTO

        <OperationContract()>
        Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String

        <OperationContract()>
        Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO)

        <OperationContract()>
        Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO

        <OperationContract()>
        Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO)

        <OperationContract()>
        Function GetListContract(ByVal ID As Decimal) As DataTable

        <OperationContract()>
        Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
#End Region
#Region "Danh mục người ký"
        <OperationContract()>
        Function GET_HU_SIGNER(ByVal _filter As SignerDTO,
                                  ByVal _param As ParamDTO,
                                   ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        <OperationContract()>
        Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        <OperationContract()>
        Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal, ByVal ORG_ID As Decimal, ByVal title_name As String) As Decimal
        <OperationContract()>
        Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal)
        <OperationContract()>
        Function DeleteSigner(ByVal lstID As String)

#End Region

#Region "QL kiêm nhiệm"
        <OperationContract()>
        Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        <OperationContract()>
        Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        ByVal EMPLOYEE_ID As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        <OperationContract()>
        Function GET_CONCURRENTLY_BY_ID(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function INSERT_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer

        <OperationContract()>
        Function UPDATE_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer

        <OperationContract()>
        Function GET_CONCURRENTLY_BY_EMP(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function DELETE_CONCURRENTLY(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function Validate_Concurrently(ByVal _validate As Temp_ConcurrentlyDTO) As Boolean

        <OperationContract()>
        Function GET_TITLE_ORG(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function INSERT_EMPLOYEE_KN(ByVal P_EMPLOYEE_CODE As String,
                                       ByVal P_ORG_ID As Decimal,
                                       ByVal P_TITLE As Decimal,
                                       ByVal P_DATE As Date,
                                       ByVal P_ID_KN As Decimal,
                                       ByVal P_STAFF_RANK_ID As Decimal) As Boolean

        <OperationContract()>
        Function UPDATE_EMPLOYEE_KN(ByVal P_ID_KN As Decimal,
                                       ByVal P_DATE As Date) As Boolean
#End Region

#Region "Work Place"
        <OperationContract()>
        Function GetWorkPlace(ByVal _filter As WorkPlaceDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkPlaceDTO)

        <OperationContract()>
        Function InsertWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateWorkPlace(ByVal _validate As WorkPlaceDTO)

        <OperationContract()>
        Function ModifyWorkPlace(ByVal objWorkPlace As WorkPlaceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveWorkPlace(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteWorkPlace(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetWorkPlaceID(ByVal ID As Decimal) As WorkPlaceDTO
#End Region

#Region "CTNN"

        <OperationContract()>
        Function GetAbroads(ByVal _filter As HUAbroadDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUAbroadDTO)

        <OperationContract()>
        Function InsertAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean

        <OperationContract()>
        Function DeleteAbroad(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GET_ABROAD_DATA_IMPORT() As DataSet

        <OperationContract()>
        Function IMPORT_ABROAD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region
#Region "bhld"
        <OperationContract()>
        Function getListCol() As List(Of BHLDDTO)

        <OperationContract()>
        Function countItemPortal(ByVal emp_id As Decimal, ByVal type As String, ByVal tag As String) As Decimal

        <OperationContract()>
        Function getOrgName(ByVal id As Decimal) As String

        <OperationContract()>
        Function excel_BHLD_portal(ByVal year As Decimal, ByVal dts As String, ByVal log As UserLog, ByVal status As String) As Byte()


        <OperationContract()>
        Function saveBdldPortal(ByVal year As Decimal, ByVal dt As String, ByVal log As UserLog, ByVal is_send As Decimal) As Decimal

        <OperationContract()>
        Function saveBHLD(ByVal year As Decimal, ByVal lst_col As List(Of String), ByVal dt As DataTable, ByVal is_import As Boolean, ByVal log As UserLog) As Decimal

        <OperationContract()>
        Function GetBHLD(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetBHLD_Register(ByVal year As Integer, ByVal empcode As String, ByVal statusId As Decimal, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetBHLD_Approve(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable
        <OperationContract()>
        Function Excel_DK_PD(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO, ByVal type As String,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As Byte()
        <OperationContract()>
        Function GetBHLD1(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable


        <OperationContract()>
        Function CalculateBHLD(ByVal year As Integer, ByVal _param As ParamDTO, Optional ByVal log As UserLog = Nothing) As Boolean
#End Region

#Region "Travel"

        <OperationContract()>
        Function GetTravels(ByVal _filter As HUTravelDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUTravelDTO)

        <OperationContract()>
        Function InsertTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateTravel(ByVal objTravel As HUTravelDTO) As Boolean

        <OperationContract()>
        Function DeleteTravel(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GET_TRAVEL_DATA_IMPORT() As DataSet

        <OperationContract()>
        Function IMPORT_TRAVEL(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region

#Region "InfoConfirm"

        <OperationContract()>
        Function GetInfoConfirms(ByVal _filter As HUInfoConfirmDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUInfoConfirmDTO)

        <OperationContract()>
        Function InsertInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteInfoConfirm(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetInfoConfirmPrintData(ByVal _id As Decimal) As DataTable
#End Region

#Region "CB Planning"

        <OperationContract()>
        Function GetCBPlannings(ByVal _filter As CBPlanningDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningDTO)

        <OperationContract()>
        Function GetCBPlanning(ByVal _id As Decimal) As CBPlanningDTO

        <OperationContract()>
        Function InsertCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCBPlanning(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CopyCBPlanning(ByVal _id As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCBPlanningsHistory(ByVal _filter As CBPlanningEmpHisDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningEmpHisDTO)
#End Region

#Region "Commitee"

        <OperationContract()>
        Function GetCommitees(ByVal _filter As CommiteeDTO,
                              ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                              ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc",
                              Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO)

        <OperationContract()>
        Function GetCommitee(ByVal _id As Decimal) As CommiteeDTO

        <OperationContract()>
        Function InsertCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommitee(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function CopyCommitee(ByVal _id As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCommiteesHistory(ByVal _filter As CommiteeEmpDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CommiteeEmpDTO)

        <OperationContract()>
        Function Portal_GetCommitee(ByVal _filter As CommiteeDTO,
                                    ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO)

        <OperationContract()>
        Function GetCommiteeProcess(ByVal Username As String, ByVal empID As Decimal) As List(Of CommiteeDTO)
#End Region

#Region "CB Assessment"

        <OperationContract()>
        Function GetCBAssessments(ByVal _filter As CBAssessmentDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBAssessmentDTO)

        <OperationContract()>
        Function GetCBAssessment(ByVal _id As Decimal) As CBAssessmentDTO

        <OperationContract()>
        Function InsertCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCBAssessment(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateCBAssessment(ByVal objCBAssessment As CBAssessmentDTO) As Boolean
#End Region

#Region "Emp NPT"

        <OperationContract()>
        Function GetEmployeeNPTs(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO)

        <OperationContract()>
        Function IMPORT_EMPPLOYEE_NPT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
#End Region

#Region "Signer Setup"
        <OperationContract()>
        Function GetSingerSetups(ByVal _filter As SignerSetupDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable

        <OperationContract()>
        Function GetListSignerSetup(ByVal _filter As SignerSetupDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of SignerSetupDTO)

        <OperationContract()>
        Function InsertSignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal log As UserLog, ByVal _lst_Type As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateSignSet(ByVal _validate As SignerSetupDTO) As Boolean

        <OperationContract()>
        Function ModifySignerSetup(ByVal objSignSet As SignerSetupDTO, ByVal log As UserLog, ByVal _lst_Type As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ActiveSignerSetup(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSignerSetup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region
        <OperationContract()>
        Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function EXPORT_PLHD(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function EXPORT_CONGNO(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function EXPORT_CV(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer

        <OperationContract()>
        Function CHECK_CONTRACT_BY_EMP_CODE(ByVal P_ID As Decimal, ByVal P_EMPID As Decimal) As Integer

        <OperationContract()>
        Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        <OperationContract()>
        Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        <OperationContract()>
        Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        <OperationContract()>
        Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        <OperationContract()>
        Function INPORT_PLHD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IMPORT_CONGNO(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function IMPORT_NV(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function EXPORT_QLKT() As DataSet
        <OperationContract()>
        Function GET_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_LOCATION_EXITS(ByVal P_ID As Decimal?, ByVal ORG_ID As Decimal) As Boolean

        <OperationContract()>
        Function GetCertificates(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal _param As ParamDTO,
                                    ByRef PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        <OperationContract()>
        Function GetCertificateById(ByVal _id As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTO

        <OperationContract()>
        Function ChangeImage(ByVal _EmpID As Decimal, ByVal _SavePath As String, ByVal _ImageName As String, ByVal _imageBinary As Byte(), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Get_Month_Work_Before(ByVal emp_id As Decimal) As Decimal


        <OperationContract()>
        Function GET_REGION_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal

        <OperationContract()>
        Function GET_SALARY_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal

        <OperationContract()>
        Function GET_CODE_EMP_TYPE(ByVal Id As Decimal) As String
        <OperationContract()>
        Function GetEmployeeGroup() As DataTable

        <OperationContract()>
        Function GET_EXPORT_ORG_BARND() As DataSet

        <OperationContract()>
        Function IMPORT_HU_ORG_BARND(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

        <OperationContract()>
        Function GetTitleNamePage(ByVal Code As String) As String

        <OperationContract()>
        Function CheckExistsEmpEdit(ByVal _EMPLOYEE_ID As Decimal) As Boolean

        <OperationContract()>
        Function GetEmpDocumentList(ByVal _emp_id As Decimal) As List(Of MngProfileSavedDTO)

        <OperationContract()>
        Function AddFileUpload(ByVal _File As FileUploadDTO, ByVal _imageBinary As Byte()) As Decimal

        <OperationContract()>
        Function AddFileUpload_mb(ByVal _File As FileUploadDTO, ByVal _imageBinary As Byte()) As Decimal

        <OperationContract()>
        Function copyAddress(ByVal emp_id As Decimal) As FamilyEditDTO

        <OperationContract()>
        Function AddFaceGPS_mb(ByVal _File As FileUploadDTO, ByVal UserID As Decimal, ByVal _imageBinary As Byte()) As Decimal
#Region "HU_SALARY"
        <OperationContract()>
        Function GetHu_Salary(ByVal _filter As HU_SALARYDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of HU_SALARYDTO)
        <OperationContract()>
        Function ModifyHu_Salary(ByVal objWorking As HU_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertHu_Salary(ByVal objWorking As HU_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetHuSalaryByID(ByVal objWorking As HU_SALARYDTO) As HU_SALARYDTO
#End Region

#Region "Salary Items Percent"
        <OperationContract()>
        Function GetSalItemsPercent(ByVal _filter As SalaryItemsPercentDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of SalaryItemsPercentDTO)
        <OperationContract()>
        Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function InsertSalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifySalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateSalItemsPercent(ByVal obj As SalaryItemsPercentDTO) As Boolean

        <OperationContract()>
        Function DeleteSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal status As String, ByVal log As UserLog) As Boolean
#End Region
        <OperationContract()>
        Function GetHU_OrgTreeview(ByVal isBlank As Boolean, ByVal userName As String, Optional ByVal allLevel As Decimal = 0) As DataTable

        <OperationContract()>
        Function PrintEmployeeCV(ByVal EmployeeID As Decimal) As Byte()

        <OperationContract()>
        Function UpdateEmployeeHoldingCode(ByVal lstEmpID As List(Of Decimal), ByVal HoldingCode As String) As Boolean
#Region "Stocks"
        <OperationContract()>
        Function GetStocks(ByVal _filter As StocksDTO, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of StocksDTO)
        <OperationContract()>
        Function GetStocksByID(ByVal _filter As StocksDTO) As StocksDTO

        <OperationContract()>
        Function InsertStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal)

        <OperationContract()>
        Function ModifyStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateStocks(ByVal obj As StocksDTO) As Boolean

        <OperationContract()>
        Function ValidateStocksGenerate(ByVal obj As StocksDTO) As Boolean

        <OperationContract()>
        Function DeleteStocks(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region
#Region "Stocks Transaction"
        <OperationContract()>
        Function GetStocksTransaction(ByVal _filter As StocksTransactionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of StocksTransactionDTO)
        <OperationContract()>
        Function InsertStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateStocksTransactionStatus(ByVal obj As StocksTransactionDTO) As Boolean

        <OperationContract()>
        Function ValidateStocksTransactionBefore(ByVal obj As StocksTransactionDTO) As Boolean

        <OperationContract()>
        Function ValidateStocksTransaction(ByVal obj As StocksTransactionDTO) As Boolean


        <OperationContract()>
        Function DeleteStocksTransaction(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "ReminderList"
        <OperationContract()>
        Function GetReminderList(ByVal _filter As ReminderListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReminderListDTO)
        <OperationContract()>
        Function ModifyReminderList(ByVal objReminderList As ReminderListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveReminderList(ByVal lstID As List(Of Decimal), ByVal sActive As Decimal,
                                   ByVal log As UserLog) As Boolean
#End Region

        <OperationContract()>
        Function CheckExistApproval(ByVal EmpID As Decimal, ByVal Table As String, ByVal Fk_Pkey As Decimal) As Boolean
        <OperationContract()>
        Function CheckHROrgPermission(ByVal username As String, ByVal empID As Decimal) As Boolean
    End Interface

End Namespace
