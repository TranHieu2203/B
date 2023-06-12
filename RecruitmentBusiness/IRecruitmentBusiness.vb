Imports System.ServiceModel
Imports Framework.Data
Imports RecruitmentDAL

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IRecruitmentBusiness

        <OperationContract()>
        Function TestService(ByVal str As String) As String
        <OperationContract()>
        Function ImportRC(ByVal Data As DataTable, ByVal ProGramID As Decimal, Optional ByVal log As UserLog = Nothing) As Boolean
#Region "danh muc phuong xa"
        <OperationContract()>
        Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable
#End Region
#Region "Hoadm - List"

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

#Region "ExamsDtl"
        <OperationContract()>
        Function GetExamsDtl(ByVal _filter As ExamsDtlDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO)

        <OperationContract()>
        Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean

#End Region

#End Region

#Region "Hoadm - Business"

#Region "PlanReg"
        <OperationContract()>
        Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanRegDTO)

        <OperationContract()>
        Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO

        <OperationContract()>
        Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean

#End Region

#Region "PlanSummary"

        <OperationContract()>
        Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable


#End Region

#Region "Request"
        <OperationContract()>
        Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)

        <OperationContract()>
        Function GetListOfferCandidate(ByVal _filter As ListOfferCandidateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ListOfferCandidateDTO)

        <OperationContract()>
        Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO

        <OperationContract()>
        Function InsertRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function DeleteOfferCandidate(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateStatusRequest(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog, ByVal Insert_Pro As Boolean, Optional ByVal IS_CONFIRM As Boolean = False) As Boolean

        <OperationContract()>
        Function CancelRequest(ByVal lstID As List(Of Decimal), ByVal reason As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function AutoGenCodeRequestRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
#End Region
#Region "Request_Portal"
        <OperationContract()>
        Function GetRequestPortal(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO)
        <OperationContract()>
        Function GetRequestPortal_Approve(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO)

        <OperationContract()>
        Function GetRequestPortalByID(ByVal _filter As RequestPortalDTO) As RequestPortalDTO

        <OperationContract()>
        Function InsertRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteRequestPortal(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateStatusRequestPortal(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog, ByVal Insert_Pro As Boolean) As Boolean

        <OperationContract()>
        Function AutoGenCodeRequestPortalRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
#End Region
#Region "Program"
        <OperationContract()>
        Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO

        <OperationContract()>
        Function ModifyProgram(ByVal objProgram As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function XuatToTrinh(ByVal sID As Decimal) As DataTable

#Region "ProgramExams"
        <OperationContract()>
        Function GetProgramExams(ByVal _filter As ProgramExamsDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO)

        <OperationContract()>
        Function Get_EXAMS_ORDER(ByVal RC_PROGRAM_ID) As Decimal
        <OperationContract()>
        Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO

        <OperationContract()>
        Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean

#End Region

#End Region
#Region "ProgramExams"
        <OperationContract()>
        Function GETUSER_TITLE_PERMISION(ByVal _filter As RC_USER_TITLE_PERMISIONDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of RC_USER_TITLE_PERMISIONDTO)

        <OperationContract()>
        Function GETUSER_TITLE_PERMISION_BY_USER(Optional ByVal log As UserLog = Nothing) As List(Of RC_USER_TITLE_PERMISIONDTO)

        <OperationContract()>
        Function UpdateUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO, Optional ByVal log As UserLog = Nothing) As Boolean

        <OperationContract()>
        Function DeleteUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO) As Boolean

#End Region
#Region "Candidate"

        <OperationContract()>
        Function IMPORT_CANDIDATE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Kiểm tra ứng viên đó có trong hệ thống ko(trừ ứng viên nghỉ việc)
        ''' </summary>
        ''' <param name="strEmpCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function CheckExistCandidate(ByVal strEmpCode As String) As Boolean
        <OperationContract()>
        Function CheckExist_Program_Candidate(ByVal lstCandidateID As List(Of Decimal), ByVal ProgramID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateInsertCandidate(ByVal sEmpId As String, ByVal sID_No As String, ByVal sFullName As String, ByVal dBirthDate As Date, ByVal sType As String) As Boolean

        <OperationContract()>
        Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO

        ''' <summary>
        ''' Trả về binary của ảnh hồ sơ ứng viên
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()

        ''' <summary>
        ''' Thêm mới ứng viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="_strEmpCode"></param>
        ''' <param name="_imageBinary"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objEmpHis"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                  ByRef _strEmpCode As String,
                                  ByVal _imageBinary As Byte(),
                                  ByVal objEmpCV As CandidateCVDTO,
                                  ByVal objEmpEdu As CandidateEduDTO,
                                   ByVal objEmpOther As CandidateOtherInfoDTO,
                                         ByVal objEmpHealth As CandidateHealthDTO,
                                         ByVal objEmpExpect As CandidateExpectDTO,
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean

        <OperationContract()>
        Function InsertProgramCandidate(ByVal lstID As List(Of Decimal), ByVal RC_PROGRAM_ID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CreateNewCandidateCode() As CandidateDTO
        <OperationContract()>
        Function INSERT_RC_PROGRAM_CANDIDATE(ByVal obj As RC_PROGRAM_CANDIDATEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Sửa thông tin ứng viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="_imageBinary"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objEmpHis"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                  ByVal _imageBinary As Byte(), _
                                   ByVal objEmpCV As CandidateCVDTO,
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                   ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean


        ''' <summary>
        ''' Lấy danh sách ứng viên có phân trang
        ''' </summary>
        ''' <param name="PageIndex"></param>
        ''' <param name="PageSize"></param>
        ''' <param name="Total"></param>
        ''' <param name="_filter"></param>
        ''' <param name="Sorts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        <OperationContract()>
        Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        <OperationContract()>
        Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)



        <OperationContract()>
        Function GetListCandidate(ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        ''' <summary>
        ''' Lay thong tin nhan vien tu CandidateCode
        ''' </summary>
        ''' <param name="sCandidateCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO

        <OperationContract()>
        Function GETCANDIDATEINFO_BY_PSC_ID(ByVal PSC_ID As String) As CandidateDTO

        ''' <summary>
        ''' Lấy thông tin CandidateCV 
        ''' </summary>
        ''' <param name="sCandidateID">ID(Decimal) của ứng viên</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO

        <OperationContract()>
        Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO
        ''' <summary>
        ''' Lấy thông tin khác
        ''' </summary>
        ''' <param name="sCandidateID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO

        <OperationContract()>
        Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO)

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean

        <OperationContract()>
        Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), ByVal programID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), ByVal statusID As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateBlackListRc_Program_Candidate(ByVal lstCanID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCandidateImport() As DataSet


        <OperationContract()>
        Function ImportCandidate(ByVal lst As List(Of CandidateImportDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function TransferHSNVToCandidate(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal,
                                            ByVal log As UserLog) As Boolean

#Region "CandidateFamily"
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


        ''' <summary>
        ''' Check trùng CMND của nhân thân.
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateFamily(ByVal _validate As CandidateFamilyDTO) As Boolean
#End Region

#Region "Cá nhân tự đào tạo"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateTrainSinger(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#Region "Người tham chiếu"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateReference(ByVal objTrainSinger As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateReference(ByVal objTrainSinger As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateReference(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region
#Region "Trước khi vào MLG"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objCandidateBeforeWT"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objCandidateBeforeWT"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateBeforeWT(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#End Region

#Region "ProgramSchedule"
        <OperationContract()>
        Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO)

        <OperationContract()>
        Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO

        <OperationContract()>
        Function GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(ByVal _filter As RC_TransferCAN_ToEmployeeDTO) As RC_TransferCAN_ToEmployeeDTO
        <OperationContract()>
        Function CheckExist_Program_Schedule_Can(ByVal CanID As Decimal, ByVal ProID As Decimal, ByVal Exams_Order As Decimal) As Boolean
        <OperationContract()>
        Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        <OperationContract()>
        Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        <OperationContract()>
        Function UpdateProgramSchedule(ByVal objExams As ProgramScheduleDTO, ByVal log As UserLog) As Boolean


#End Region

#Region "CandidateResult"
        <OperationContract()>
        Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)


        <OperationContract()>
        Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO), ByVal log As UserLog) As Boolean

#End Region

#Region "Cost"
        <OperationContract()>
        Function GetCost(ByVal _filter As CostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO)

        <OperationContract()>
        Function UpdateCost(ByVal objExams As CostDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateCost(ByVal objCostCenter As CostDTO) As Boolean

        <OperationContract()>
        Function DeleteCost(ByVal obj As CostDTO) As Boolean

#End Region

#End Region

#Region "Hoadm - Common"
        <OperationContract()>
        Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetProvinceList(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetContractTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleByOrgListInPlan(ByVal orgID As Decimal, ByVal _year As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProgramExamsList(ByVal programID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProgramList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

#End Region

#Region "CV Pool"
        <OperationContract()>
        Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of CVPoolEmpDTO)
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

#Region "DashBoard"
        <OperationContract()>
        Function GetStatisticGender(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticEduacation(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticCanToEmp(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticEstimateReality(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function REPORT_RANK_SAL_BY_MONTH(ByVal _year As Integer, ByVal _month As Integer, ByVal _org As Integer, ByVal log As UserLog) As List(Of HRPlaningDetailDTO)
        <OperationContract()>
        Function REPORT_RANK_SAL_BY_YEAR_SAL(ByVal _year As Integer, ByVal _org As Integer, ByVal log As UserLog) As List(Of ReportDTO)
#End Region

#Region "HR_PLANING"
        <OperationContract()>
        Function InsertHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckExistEffectDate_HRYearPlaning(ByVal Id As Decimal, ByVal EffectDate As Date) As Boolean

        <OperationContract()>
        Function GetHRYearPlaning(ByVal _filter As HRYearPlaningDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRYearPlaningDTO)

        <OperationContract()>
        Function ModifyHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteHRYearPlaning(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateHRYearPlaning(ByVal year As Decimal, ByVal effect_date As Date, ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function CheckExistData(ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function GetHrYearPlaningByID(ByVal _id As Decimal) As HRYearPlaningDTO
#End Region

#Region "HR_PL_DETAIL"
        <OperationContract()>
        Function InsertHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetPlanDetail(ByVal _filter As HRPlaningDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRPlaningDetailDTO)

        <OperationContract()>
        Function ModifyHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteHRPlanDetail(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateHRPlanDetail(ByVal ORG_ID As Decimal, ByVal TITLE_ID As Decimal, ByVal YEAR_PLAN_ID As Decimal, ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function CheckExistRankSal(ByVal _id As Decimal) As Boolean

        <OperationContract()>
        Function ModifyHRBudgetDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetDetailOrgTitle(ByVal _filter As HRPlaningDetailDTO,
                                  ByVal _param As ParamDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "TITLE_NAME", Optional ByVal log As UserLog = Nothing) As DataTable
#End Region
#Region "RC_CANDIDATE_CONTRACT"
        <OperationContract()>
        Function GetRCNegotiate(ByVal _filter As RCNegotiateDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", _
                                    Optional ByVal log As UserLog = Nothing) As List(Of RCNegotiateDTO)

        <OperationContract()>
        Function InsertRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertRcSalaryCandidate(ByVal obj As RC_Salary_CandidateDTO) As Boolean

        <OperationContract()>
        Function InsertRcAlowCandidate(ByVal obj As List(Of RC_Allowance_CandidateDTO)) As Boolean


        <OperationContract()>
        Function DeleteRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog) As Decimal
#End Region
#Region "RC_TRANSFERCAN_TOEMPLOYEE"
        <OperationContract()>
        Function Insert_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function Modify_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
#End Region
#Region "Yêu cầu tuyển dụng"
        <OperationContract()>
        Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32

        <OperationContract()>
        Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
#End Region
        <OperationContract()>
        Function GetTitleByOrg(ByVal OrgID As Decimal) As DataTable

        <OperationContract()>
        Function GetRCAllowanceCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Allowance_CandidateDTO)

        <OperationContract()>
        Function GetRCSalaryCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Salary_CandidateDTO)

        <OperationContract()>
        Function CheckTransferToEmployee(ByVal CandidateId As Decimal) As Boolean
    End Interface
End Namespace
