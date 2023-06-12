Imports System.ServiceModel
Imports Framework.Data
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace PerformanceBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IPerformanceBusiness
        <OperationContract()>
        Function TestService(ByVal str As String) As String

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As PerformanceCommon.TABLE_NAME) As Boolean

#Region "Common"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetPeriodList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetObjectGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetObjectGroupByPeriodList(ByVal periodID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetCriteriaByObjectList(ByVal objectID As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)

        <OperationContract()>
        Function GET_CRITERIAL_DATA_IMPORT() As DataSet

        <OperationContract()>
        Function IMPORT_CRITERIAL_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean
#End Region

#Region "List"

#Region "Criteria"
        <OperationContract()>
        Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)

        <OperationContract()>
        Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean

        <OperationContract()>
        Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean
#End Region
#Region "Criteria"
        <OperationContract()>
        Function GetPE_Criteria_HTCH(ByVal _filter As PE_Criteria_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Criteria_HTCHDTO)

        <OperationContract()>
        Function InsertPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidatePE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO) As Boolean

        <OperationContract()>
        Function ModifyPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePE_Criteria_HTCH(ByVal lstCriteria() As Decimal) As Boolean
#End Region
#Region "Classification"
        <OperationContract()>
        Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)

        <OperationContract()>
        Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean

        <OperationContract()>
        Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function CalKPI(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function CalKPI_Result(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
#End Region
#Region "Classification HTCH"
        <OperationContract()>
        Function GetClassificationHTCH(ByVal _filter As ClassificationHTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationHTCHDTO)

        <OperationContract()>
        Function InsertClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO) As Boolean

        <OperationContract()>
        Function ModifyClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveClassificationHTCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean


        <OperationContract()>
        Function DeleteClassificationHTCH(ByVal lstClassification() As Decimal) As Boolean
#End Region
#Region "ObjectGroup"
        <OperationContract()>
        Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)

        <OperationContract()>
        Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean
#End Region

#Region "Period"
        <OperationContract()>
        Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        <OperationContract()>
        Function GetPeriodById(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)

        <OperationContract()>
        Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean

        <OperationContract()>
        Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean
#End Region
#Region "Period HTCH"
        <OperationContract()>
        Function GetPeriodHTCH(ByVal _filter As PE_Period_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)
        <OperationContract()>
        Function GetPeriodHTCHById(ByVal _filter As PE_Period_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO)

        <OperationContract()>
        Function InsertPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidatePeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO) As Boolean

        <OperationContract()>
        Function ModifyPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePeriodHTCH(ByVal lstPeriod() As Decimal) As Boolean
#End Region
#End Region

#Region "Setting"

#Region "ObjectGroupPeriod"
        <OperationContract()>
        Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As List(Of ObjectGroupPeriodDTO)

        <OperationContract()>
        Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As _
                                         List(Of ObjectGroupPeriodDTO)

        <OperationContract()>
        Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "CriteriaObjectGroup"
        <OperationContract()>
        Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CRITERIA_CODE") As List(Of CriteriaObjectGroupDTO)

        <OperationContract()>
        Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As _
                                         List(Of CriteriaObjectGroupDTO)

        <OperationContract()>
        Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "EmployeeAssessment"
        <OperationContract()>
        Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "Employee_CODE",
                                                Optional ByVal log As UserLog = Nothing) As List(Of EmployeeAssessmentDTO)

        <OperationContract()>
        Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As _
                                         List(Of EmployeeAssessmentDTO)

        <OperationContract()>
        Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region
#Region "THIẾT LẬP NHÓM CHỨC DANH THEO TIÊU CHÍ ĐÁNH GIÁ"
        <OperationContract()>
        Function getCriteriaTitleGroup(ByVal _filter As CriteriaTitleGroupDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupDTO)
        <OperationContract()>
        Function DeleteCriteriaTitleGroup(ByVal lst As List(Of Decimal),
                                          ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetCriteriaTitleGroupbyID(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As CriteriaTitleGroupDTO
        <OperationContract()>
        Function InsertCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateCriteriaTitleGroup_Detail(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As Boolean
#End Region


#End Region

#Region "Business"

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
        <OperationContract()>
        Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet

        <OperationContract()>
        Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                          ByVal periodID As Decimal,
                                          ByVal group_obj_ID As Decimal,
                                          ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
        <OperationContract()>
        Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO)
        <OperationContract()>
        Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO

        <OperationContract()>
        Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đánh giá cho từng nhóm đối tượng đánh giá"
        <OperationContract()>
        Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "Assessment"
        <OperationContract()>
        Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        <OperationContract()>
        Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                  ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetTotalPointRating(ByVal _filter As AssessmentDTO) As DataTable
        <OperationContract()>
        Function Delete_PE_KPI_ASSESSMENT_DETAIL(ByVal lstID As List(Of Decimal)) As Boolean
#End Region
#End Region

#Region "Portal"
        <OperationContract()>
        Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        <OperationContract()>
        Function GET_DM_STATUS() As DataTable
        <OperationContract()>
        Function UpdateStatusAssessmentDirect(ByVal obj As Decimal,
                                                  ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)

        <OperationContract()>
        Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable


        <OperationContract()>
        Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                 ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean

        <OperationContract()>
        Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean

        <OperationContract()>
        Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
#End Region

#Region "CBNS xem KPI cua nhan vien"
        <OperationContract()>
        Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer, ByVal _param As ParamDTO,
                                   Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO)

#End Region

#Region "CBNS xem KPI cua nhan vien"
        <OperationContract()>
        Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        <OperationContract()>
        Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO)
#End Region

#Region "Dashboard"

        <OperationContract()>
        Function GetStatisticKPIByClassification(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticKPIByClassifiOrg(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable
        <OperationContract()>
        Function GetStatisticClassification(Optional ByVal Sorts As String = "CODE ASC") As List(Of ClassificationDTO)
        <OperationContract()>
        Function GetStatisticKPISeniority(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable



#End Region
        <OperationContract()>
        Function InsertKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        <OperationContract()>
        Function DeleteKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)) As Boolean
        <OperationContract()>
        Function UpdateKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        <OperationContract()>
        Function UpdateKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO),
                                                  Optional ByVal log As UserLog = Nothing) As Boolean

        <OperationContract()>
        Function ValidateDateAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean

        <OperationContract()>
        Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet
        <OperationContract()>
        Function GetKpiAssessmentByID(ByVal id As Decimal) As KPI_ASSESSMENT_DTO
        <OperationContract()>
        Function GetKpiAssessmentDetailHistory(ByVal lstID As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                         ByVal PageIndex As Integer,
                                                         ByVal PageSize As Integer,
                                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        <OperationContract()>
        Function GetKpiAssessmentDetailByListCode(ByVal lstCode As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                     ByVal PageIndex As Integer,
                                                     ByVal PageSize As Integer,
                                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        <OperationContract()>
        Function GetKpiAssessmentDetailByGoal(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        <OperationContract()>
        Function GetKpiAssessmentDetail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        <OperationContract()>
        Function GetKpiAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)
        <OperationContract()>
        Function GetKpiAssessmentResult(ByVal _filter As KPI_ASSESSMENT_RESULT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_RESULT_DTO)
        <OperationContract()>
        Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteKpiAssessment(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteKpiAssessmentResult(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function SaveChangeRatio(ByVal list As List(Of KPI_ASSESSMENT_DTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetPeriodByYear(ByVal Year As Decimal) As DataTable

        <OperationContract()>
        Function GetDateByPeriod(ByVal Period As Decimal) As DataTable
        <OperationContract()>
        Function GetPeriodByYear2(ByVal Year As Decimal) As DataTable
        <OperationContract()>
        Function GetPeriod2(ByVal year As Decimal, ByVal isBlank As Decimal) As DataTable
        <OperationContract()>
        Function GetDateByPeriod2(ByVal Period As Decimal) As DataTable

#Region "Portal Assessment"
        <OperationContract()>
        Function GetPortalAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)
        <OperationContract()>
        Function Get_Portal_Target_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        <OperationContract()>
        Function Get_Portal_Approve_Evaluate_Target(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        <OperationContract()>
        Function GetPortalApprovedKPIAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO)

        <OperationContract()>
        Function Get_Portal_KPI_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        <OperationContract()>
        Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32
        <OperationContract()>
        Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32

        <OperationContract()>
        Function IMPORT_KPI_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateSubmit(ByVal _kpi_ID As Decimal, ByVal _type As String) As Boolean


#End Region

#Region "HTCH Assessment"
        <OperationContract()>
        Function GetHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO)
        <OperationContract()>
        Function GetHTCHAssessment_Detail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        <OperationContract()>
        Function GetHTCHAssessmentByID(ByVal _id As Decimal) As PE_HTCH_ASSESSMENT_DTO

        <OperationContract()>
        Function GetHTCHAssessmentListDetail(ByVal _id As Decimal) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        <OperationContract()>
        Function SaveHTCHAssessment(ByVal objData As PE_HTCH_ASSESSMENT_DTO, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function SaveHTCHAssessmentListDetail(ByVal lstObj As List(Of PE_HTCH_ASSESSMENT_DTL_DTO), ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteHTCHAssessment(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetYearHTCH(ByVal IS_BLANK As Boolean) As DataTable

        <OperationContract()>
        Function GetPeriodHTCHByYear(ByVal Year As Decimal) As DataTable

        <OperationContract()>
        Function CAL_HTCH_ASSESSMENT(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CAL_HTCT_ASSESS_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CHANGE_HTCH_ASSESSMENT_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetAproveHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO)

        <OperationContract()>
        Function GetApproveHTCHAssessmentDetail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)

        <OperationContract()>
        Function IMPORT_HTCH_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Employee Period"
        <OperationContract()>
        Function GetEmployeePeriods(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO)
        <OperationContract()>
        Function GetEmployeePeriodHCTH(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO)
        <OperationContract()>
        Function CAL_EMPLOYEE_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmployeePeriod(ByVal _lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CAL_EMPLOYEEHTCH_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CAL_EMP_RECOMEND_RESULT(ByVal _org_id As List(Of Decimal), ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmployeeHTCHPeriod(ByVal _lstID As List(Of Decimal)) As Boolean
#End Region

        <OperationContract()>
        Function GetCriteriaTitleGroupRankbyID(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                              ByVal log As UserLog) As CriteriaTitleGroupRankDTO


        <OperationContract()>
        Function InsertCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                            ByVal log As UserLog) As Boolean


        <OperationContract()>
        Function ModifyCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                            ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function getCriteriaTitleGroupRank(ByVal _filter As CriteriaTitleGroupRankDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupRankDTO)
        <OperationContract()>
        Function DeleteCriteriaTitleGroupRank(ByVal lst As List(Of Decimal),
                                          ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function APPROVE_EVALUATE_TARGET(ByVal P_ID_REGGROUP As Decimal, ByVal P_APPROVE_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS_ID As Decimal, ByVal P_REASON As String) As Boolean
#Region "PE_ORG_MR_RR -> Quản lý dữ liệu RR và MR theo tháng"
        <OperationContract()>
        Function GetPe_Org_Mr_Rr(ByVal _filter As PE_ORG_MR_RRDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal lstOrg As List(Of Decimal),
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PE_ORG_MR_RRDTO)
        <OperationContract()>
        Function GetATPeriodByYear(ByVal Year As Decimal) As DataTable
        <OperationContract()>
        Function GetYearATPeriod() As DataTable
        <OperationContract()>
        Function ValidatePe_Org_Mr_Rr(ByVal obj As PE_ORG_MR_RRDTO) As Boolean
#End Region
    End Interface
End Namespace
