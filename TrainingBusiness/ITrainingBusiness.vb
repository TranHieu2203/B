Imports System.ServiceModel
Imports Framework.Data
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace TrainingBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface ITrainingBusiness

        <OperationContract()>
        Function TestService(ByVal str As String) As String

        <OperationContract()>
        Function IMPORT_PROGRAM_RESULT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function IMPORT_REIMBURSEMENT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function IMPORT_TR_REQUEST(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function RejectTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        <OperationContract()>
        Function ApproveTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        <OperationContract()>
        Function SendTrainingToEmployeeProfile(ByVal listTrainingId As List(Of Decimal), ByVal issuedDate As Date, ByVal log As UserLog) As Boolean

#Region "Hoadm - Common"

#Region "OtherList"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrCertificateList(ByVal dGroupID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrCenterList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrPlanByYearOrg(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function GetTrPlanByYearOrg2(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog, Optional ByVal isIrregularly As Boolean = False) As DataTable

        <OperationContract()>
        Function GetTrLectureList(ByVal isLocal As Boolean, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuProvinceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuContractTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrProgramByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable

        <OperationContract()>
        Function GetTrCriteriaGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrAssFormList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrChooseProgramFormByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable
        <OperationContract()>
        Function GetTrProgramById(ByVal id As Decimal) As DataTable

#End Region

#Region "CostCenter"
        <OperationContract()>
        Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)

        <OperationContract()>
        Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean

        <OperationContract()>
        Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#End Region

#Region "Hoadm - List"

#Region "Certificate"
        <OperationContract()>
        Function GetCertificate(ByVal _filter As CertificateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO)

        <OperationContract()>
        Function InsertCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCertificate(ByVal objCertificate As CertificateDTO) As Boolean

        <OperationContract()>
        Function ModifyCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCertificate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCertificate(ByVal lstCertificate() As CertificateDTO) As Boolean
#End Region

#Region "Course"
        <OperationContract()>
        Function GetCourse(ByVal _filter As CourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO)

        <OperationContract()>
        Function InsertCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCourse(ByVal objCourse As CourseDTO) As Boolean

        <OperationContract()>
        Function ModifyCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCourse(ByVal lstCourse() As CourseDTO) As Boolean
#End Region

#Region "Center"
        <OperationContract()>
        Function GetCenter(ByVal _filter As CenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO)

        <OperationContract()>
        Function InsertCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCenter(ByVal objCenter As CenterDTO) As Boolean

        <OperationContract()>
        Function ModifyCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCenter(ByVal lstCenter() As CenterDTO) As Boolean
#End Region

#Region "Lecture"
        <OperationContract()>
        Function GetLecture(ByVal _filter As LectureDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO)

        <OperationContract()>
        Function InsertLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateLecture(ByVal objLecture As LectureDTO) As Boolean

        <OperationContract()>
        Function ModifyLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveLecture(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteLecture(ByVal lstLecture() As LectureDTO) As Boolean
#End Region

#Region "CommitAfterTrain"
        <OperationContract()>
        Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO)

        <OperationContract()>
        Function InsertCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommitAfterTrain(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCommitAfterTrain(ByVal lstCommitAfterTrain() As CommitAfterTrainDTO) As Boolean
#End Region

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
        Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCriteria(ByVal lstCriteria() As CriteriaDTO) As Boolean
#End Region

#Region "CriteriaGroup"
        <OperationContract()>
        Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO)

        <OperationContract()>
        Function InsertCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCriteriaGroup(ByVal lstCriteriaGroup() As CriteriaGroupDTO) As Boolean
#End Region

#Region "AssessmentRate"
        <OperationContract()>
        Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO)

        <OperationContract()>
        Function InsertAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAssessmentRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteAssessmentRate(ByVal lstAssessmentRate() As AssessmentRateDTO) As Boolean
#End Region

#Region "AssessmentForm"
        <OperationContract()>
        Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO)

        <OperationContract()>
        Function InsertAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAssessmentForm(ByVal lstAssessmentForm() As AssessmentFormDTO) As Boolean
#End Region

#End Region

#Region "Hoadm - Setting"

#Region "SettingAssFormDTO"

        <OperationContract()>
        Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        <OperationContract()>
        Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        <OperationContract()>
        Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSettingAssForm(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#Region "SettingCriteriaGroupDTO"

        <OperationContract()>
        Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        <OperationContract()>
        Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        <OperationContract()>
        Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSettingCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#End Region

#Region "Hoadm - Business"

#Region "Otherlist"

        <OperationContract()>
        Function GetCourseList() As List(Of CourseDTO)

        <OperationContract()>
        Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)

        <OperationContract()>
        Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        <OperationContract()>
        Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO

        <OperationContract()>
        Function GetCenters() As List(Of CenterDTO)

#End Region

#Region "Plan"
        <OperationContract()>
        Function GetPlanEmployee(ByVal filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal lstTitleId As List(Of Decimal), ByVal lstTitleGR As List(Of Decimal), ByVal PageSize As Integer,
                                    ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        <OperationContract()>
        Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO)

        <OperationContract()>
        Function GetPlanById(ByVal Id As Decimal) As PlanDTO

        <OperationContract()>
        Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GET_PLAN_DATA_IMPORT(ByVal P_ORG_ID As Decimal, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GET_TITLE_COURSE_IMPORT() As DataSet

        <OperationContract()>
        Function IMPORT_TR_PLAN(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function IMPORT_TITLECOURSE(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean


        <OperationContract()>
        Function GetTitleByGroupID(ByVal _lstGroupID As List(Of Decimal)) As List(Of TitleDTO)
#End Region

#Region "Request"
        <OperationContract()>
        Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        <OperationContract()>
        Function GetTrainingRequestPortalApprove(ByVal filter As RequestDTO,
                                                 ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)
        <OperationContract()>
        Function GetTrainingRequestPortal(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)

        <OperationContract()>
        Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO
        <OperationContract()>
        Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        <OperationContract()>
        Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32


        <OperationContract()>
        Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String


        <OperationContract()>
        Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)


        <OperationContract()>
        Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function SubmitTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean


        <OperationContract()>
        Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO

        <OperationContract()>
        Function GetEmployeeByCode(ByVal _employee_Code As String) As Decimal

#End Region

#Region "Program"
        <OperationContract()>
        Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        <OperationContract()>
        Function GetEmpByTitleAndOrg(ByVal titleId As Decimal, ByVal orgId As Decimal) As List(Of RecordEmployeeDTO)
        <OperationContract()>
        Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer, ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetProgramEvaluatePortal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer, ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)
        <OperationContract()>
        Function GetPrograms_Portal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer, ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        <OperationContract()>
        Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        <OperationContract()>
        Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO

        <OperationContract()>
        Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean

#End Region

#Region "Prepare"

        <OperationContract()>
        Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)

        <OperationContract()>
        Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean

#End Region

#Region "Class"

        <OperationContract()>
        Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable

        <OperationContract()>
        Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        <OperationContract()>
        Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean

#End Region

#Region "Class Student"

        <OperationContract()>
        Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        <OperationContract()>
        Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        <OperationContract()>
        Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean


#End Region

#Region "ClassSchedule"

        <OperationContract()>
        Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)

        <OperationContract()>
        Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO

        <OperationContract()>
        Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean

#End Region

#Region "ProgramResult"
        <OperationContract()>
        Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)


        <OperationContract()>
        Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CheckProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean

        <OperationContract()>
        Function GetTRResult(ByVal _filter As ProgramResultDTO) As List(Of ProgramResultDTO)

        <OperationContract()>
        Function ValidateCerificateConfirm(ByVal listTrainingId As List(Of Decimal)) As ProgramResultDTO

#End Region

#Region "ProgramCommit"
        <OperationContract()>
        Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)


        <OperationContract()>
        Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean


#End Region

#Region "ProgramCost"
        <OperationContract()>
        Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)

        <OperationContract()>
        Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean

        <OperationContract()>
        Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean
#End Region

#Region "Reimbursement"
        <OperationContract()>
        Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)

        <OperationContract()>
        Function GetReimbursementNew(ByVal _filter As ProgramCommitDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCommitDTO)

        <OperationContract()>
        Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean

        <OperationContract()>
        Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function ModifyProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function FastUpdateProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "ChooseForm"
        <OperationContract()>
        Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)

        <OperationContract()>
        Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean

        <OperationContract()>
        Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean

#End Region


#Region "AssessmentResult"
        <OperationContract()>
        Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)

        <OperationContract()>
        Function GetAssessmentResultByID(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)

        <OperationContract()>
        Function GetAssessmentResultByID_Portal(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)
        <OperationContract()>
        Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateAssessmentResult_Portal(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GET_DTB(ByVal sType As Decimal, ByVal sEMP As Decimal) As Decimal

        <OperationContract()>
        Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean

        <OperationContract()>
        Function GET_DTB_PORTAL(ByVal sType As Decimal, ByVal sEMP As Decimal, ByVal sPROID As Decimal) As Decimal
#End Region

#Region "TranningRecord"
        <OperationContract()>
        Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)

        <OperationContract()>
        Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
        <OperationContract()>
        Function GetPortalListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
#End Region


#End Region

        <OperationContract()>
        Function test(ByVal a As CostDetailDTO) As CostDetailDTO
#Region "Setting Title Course"
        <OperationContract()>
        Function GetTitleCourse(ByVal _filter As TitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO)

        <OperationContract()>
        Function UpdateTitleCourse(ByVal objExams As TitleCourseDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean

#End Region

#Region "Get List"
        <OperationContract()>
        Function GetTitleByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region

#Region "Employee Title Course"
        <OperationContract()>
        Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO)
#End Region

#Region "BAO CAO"
        <OperationContract()>
        Function ExportReport(ByVal sPkgName As String,
                              ByVal sStartDate As Date?,
                              ByVal sEndDate As Date?,
                              ByVal sOrg As String,
                              ByVal IsDissolve As Integer,
                              ByVal sUserName As String, ByVal sLang As String) As DataSet

        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

#End Region

#Region "Dashboard"
        <OperationContract()>
        Function GetStatisticCourse(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticFormCost(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticDiligence(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticRank(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region

#Region "ProgramClassRollcard"
        <OperationContract()>
        Function GetStudentInClass(ByVal _filter As ProgramClassRollcardDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "ID desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO)
        <OperationContract()>
        Function GetProgramClassRollcard(ByVal _filter As ProgramClassRollcardDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "ID desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO)
        <OperationContract()>
        Function InsertProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function UpdateProgramClassRollcard(ByVal lstObj As List(Of ProgramClassRollcardDTO),
                                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteProgramClassRollcard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "SettingCriteriaCourse"
        <OperationContract()>
        Function GET_SETTING_CRITERIA_COURSE(ByVal _filter As SettingCriteriaCourseDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaCourseDTO)
        <OperationContract()>
        Function GET_SETTING_CRITERIA_DETAIL_BY_COURSE(ByVal _filter As SettingCriteriaDetailDTO,
                                 Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaDetailDTO)
        <OperationContract()>
        Function INSERT_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function MODIFY_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DELETE_SETTING_CRITERIA_COURSE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "TR CLASS RESULT"
        <OperationContract()>
        Function GetClassResult(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        <OperationContract()>
        Function UpdateClassResultt(ByVal lst As List(Of ProgramClassStudentDTO),
                                       ByVal log As UserLog) As Boolean
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
        Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
#End Region

    End Interface
End Namespace
