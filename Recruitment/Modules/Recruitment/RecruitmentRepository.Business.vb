Imports System.Reflection
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Recruitment.RecruitmentBusiness

Partial Class RecruitmentRepository
    Private rep As New HistaffFrameworkRepository
    Function ImportRC(ByVal Data As DataTable, ByVal ProGramID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ImportRC(Data, ProGramID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#Region "PlanReg"

    Public Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False) As List(Of PlanRegDTO)
        Dim lstPlanReg As List(Of PlanRegDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstPlanReg = rep.GetPlanReg(_filter, PageIndex, PageSize, Total, _param, Sorts, isSearch, Me.Log)
                Return lstPlanReg
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetPlanRegByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertPlanReg(objPlanReg, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyPlanReg(objPlanReg, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeletePlanReg(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateStatusPlanReg(lstID, status)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "PlanSummary"


    Public Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO) As DataTable
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetPlanSummary(_year, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "Request"

    Public Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Dim lstRequest As List(Of RequestDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstRequest = rep.GetRequest(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstRequest
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListOfferCandidate(ByVal _filter As ListOfferCandidateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ListOfferCandidateDTO)
        Dim lstOfferCandidate As List(Of ListOfferCandidateDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstOfferCandidate = rep.GetListOfferCandidate(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstOfferCandidate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetRequestByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertRequest(ByVal objRequest As RequestDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertRequest(objRequest, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyRequest(ByVal objRequest As RequestDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyRequest(objRequest, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteRequest(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOfferCandidate(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteOfferCandidate(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusRequest(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal Insert_Pro As Boolean, Optional ByVal IS_CONFIRM As Boolean = False) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateStatusRequest(lstID, status, Me.Log, Insert_Pro, IS_CONFIRM)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function CancelRequest(ByVal lstID As List(Of Decimal), ByVal reason As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CancelRequest(lstID, reason, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function AutoGenCodeRequestRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.AutoGenCodeRequestRC(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "Request_Portal"

    Public Function GetRequestPortal(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestPortalDTO)
        Dim lstRequest As List(Of RequestPortalDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstRequest = rep.GetRequestPortal(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstRequest
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetRequestPortal_Approve(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                           ByRef Total As Integer, ByVal _param As ParamDTO,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestPortalDTO)
        Dim lstRequest As List(Of RequestPortalDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstRequest = rep.GetRequestPortal_Approve(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstRequest
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetRequestPortalByID(ByVal _filter As RequestPortalDTO) As RequestPortalDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetRequestPortalByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertRequestPortal(ByVal objRequest As RequestPortalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertRequestPortal(objRequest, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyRequestPortal(ByVal objRequest As RequestPortalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyRequestPortal(objRequest, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteRequestPortal(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteRequestPortal(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusRequestPortal(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal Insert_Pro As Boolean) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateStatusRequestPortal(lstID, status, Me.Log, Insert_Pro)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function AutoGenCodeRequestPortalRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.AutoGenCodeRequestPortalRC(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region
#Region "Program"

    Public Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Dim lstProgram As List(Of ProgramDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgram = rep.GetProgram(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstProgram
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Dim lstProgram As List(Of ProgramDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgram = rep.GetProgramSearch(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstProgram
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetProgramByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyProgram(ByVal objProgram As ProgramDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyProgram(objProgram, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function XuatToTrinh(ByVal sID As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.XuatToTrinh(sID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#Region "ProgramExams"

    Public Function GetProgramExams(ByVal _filter As ProgramExamsDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO)
        Dim lstProgramExams As List(Of ProgramExamsDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgramExams = rep.GetProgramExams(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstProgramExams
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Get_EXAMS_ORDER(ByVal RC_PROGRAM_ID) As Decimal
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.Get_EXAMS_ORDER(RC_PROGRAM_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO

        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetProgramExamsByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateProgramExams(objExams, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteProgramExams(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


#End Region
#Region "RC_User_Title_permision"

    Public Function GETUSER_TITLE_PERMISION(ByVal _filter As RC_USER_TITLE_PERMISIONDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of RC_USER_TITLE_PERMISIONDTO)
        Dim lstProgramExams As List(Of RC_USER_TITLE_PERMISIONDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgramExams = rep.GETUSER_TITLE_PERMISION(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstProgramExams
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateUSER_TITLE_PERMISION(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteUSER_TITLE_PERMISION(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GETUSER_TITLE_PERMISION_BY_USER() As List(Of RC_USER_TITLE_PERMISIONDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GETUSER_TITLE_PERMISION_BY_USER(Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Candidate"

    ''' <summary>
    ''' Kiểm tra nhân viên đó có trong hệ thống ko(trừ nhân viên nghỉ việc)
    ''' </summary>
    ''' <param name="sCandidateCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckExistCandidate(ByVal sCandidateCode As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExistCandidate(sCandidateCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CheckExist_Program_Candidate(ByVal lstCandidateID As List(Of Decimal), ByVal ProgramID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExist_Program_Candidate(lstCandidateID, ProgramID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckExist_Program_Schedule_Can(ByVal CanID As Decimal, ByVal ProID As Decimal, ByVal Exams_Order As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExist_Program_Schedule_Can(CanID, ProID, Exams_Order)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateInsertCandidate(ByVal sEmpId As String, ByVal sID_No As String, ByVal sFullName As String, ByVal dBirthDate As Date, ByVal sType As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateInsertCandidate(sEmpId, sID_No, sFullName, dBirthDate, sType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO
        Dim lstCandidate As CandidateDTO

        Using rep As New RecruitmentBusinessClient
            Try
                lstCandidate = rep.GetCandidateInfo(sCandidateCode)
                Return lstCandidate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function IMPORT_CANDIDATE(ByVal P_DOCXML As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Dim result = rep.IMPORT_CANDIDATE(P_DOCXML, Me.Log)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GETCANDIDATEINFO_BY_PSC_ID(ByVal PSC_ID As String) As CandidateDTO
        Dim lstCandidate As CandidateDTO

        Using rep As New RecruitmentBusinessClient
            Try
                lstCandidate = rep.GETCANDIDATEINFO_BY_PSC_ID(PSC_ID)
                Return lstCandidate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()
        Using rep As New RecruitmentBusinessClient
            Try
                Dim _binaryImage As Byte()
                _binaryImage = rep.GetCandidateImage(gEmpID, sError)
                Return _binaryImage
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO
        Dim lstCandidateFamily As CandidateFamilyDTO
        Using rep As New RecruitmentBusinessClient
            Try
                lstCandidateFamily = rep.GetCandidateFamily_ByID(sCandidateID)
                Return lstCandidateFamily
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CreateNewCandidateCode() As CandidateDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CreateNewCandidateCode()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertCandidate(ByVal objEmp As CandidateDTO, ByRef gID As Decimal, _
                                    ByRef _strEmpCode As String, _
                                    ByVal _imageBinary As Byte(), _
                                    ByVal objEmpCV As CandidateCVDTO,
                                    ByVal objEmpEdu As CandidateEduDTO, _
                                    ByVal objEmpOther As CandidateOtherInfoDTO, _
                                    ByVal objEmpHealth As CandidateHealthDTO, _
                                    ByVal objEmpExpect As CandidateExpectDTO, _
                                    ByVal objEmpFamily As CandidateFamilyDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCandidate(objEmp, Me.Log, gID, _strEmpCode, _imageBinary, objEmpCV, objEmpEdu, _
                                                objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function INSERT_RC_PROGRAM_CANDIDATE(ByVal obj As RC_PROGRAM_CANDIDATEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.INSERT_RC_PROGRAM_CANDIDATE(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertProgramCandidate(ByVal lstID As List(Of Decimal), ByVal RC_PROGRAM_ID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertProgramCandidate(lstID, RC_PROGRAM_ID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByRef gID As Decimal, _
                                     ByVal _imageBinary As Byte(), _
                                      ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                      ByVal objEmpOther As CandidateOtherInfoDTO, _
                                      ByVal objEmpHealth As CandidateHealthDTO, _
                                      ByVal objEmpExpect As CandidateExpectDTO, _
                                    ByVal objEmpFamily As CandidateFamilyDTO) As Boolean

        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCandidate(objEmp, Me.Log, gID, _imageBinary, objEmpCV, objEmpEdu, _
                                                 objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



    Public Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByRef sError As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCandidate(lstEmpID, Me.Log, sError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateCV(sCandidateID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateEdu(sCandidateID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateOtherInfo(sCandidateID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateHealthInfo(sCandidateID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateExpectInfo(sCandidateID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateHistory(sCandidateID, sCandidateIDNO)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetListCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetListCandidatePaging(ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetListCandidatePaging(0, Integer.MaxValue, 0, _filter, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetFindCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetListCandidateTransferPaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListCandidate(ByVal _filter As CandidateDTO,
                                 Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetListCandidate(_filter, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), ByVal programID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateProgramCandidate(lstCanID, programID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), ByVal statusID As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateStatusCandidate(lstCanID, statusID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdatePontentialCandidate(lstCanID, bCheck, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateBlackListCandidate(lstCanID, bCheck, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateBlackListRc_Program_Candidate(ByVal lstCanID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateBlackListRc_Program_Candidate(lstCanID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateReHireCandidate(lstCanID, bCheck, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetCandidateImport() As DataSet
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateImport()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ImportCandidate(ByVal lst As List(Of CandidateImportDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ImportCandidate(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function TransferHSNVToCandidate(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.TransferHSNVToCandidate(empID, orgID, titleID, programID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#Region "Family -Quan hệ nhân thân"
    Public Function InsertCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCandidateFamily(objFamily, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCandidateFamily(objFamily, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCandidateFamily(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCandidateFamily(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateFamily(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateFamily(ByVal _validate As CandidateFamilyDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateFamily(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region


#Region "Cá nhân tự đào tạo"
    Public Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCandidateTrainSinger(objTrainSinger, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCandidateTrainSinger(objTrainSinger, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCandidateTrainSinger(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCandidateTrainSinger(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateTrainSinger(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Người tham chiếu"
    Public Function InsertCandidateReference(ByVal objReference As CandidateReferenceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCandidateReference(objReference, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCandidateReference(ByVal objReference As CandidateReferenceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCandidateReference(objReference, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCandidateReference(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCandidateReference(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateReference(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "CandidateBeforeWT - quá trinh công tác trước khi vào ML"
    Public Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCandidateBeforeWT(objCandidateBeforeWT, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCandidateBeforeWT(objCandidateBeforeWT, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCandidateBeforeWT(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCandidateBeforeWT(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetCandidateBeforeWT(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


#End Region

#Region "ProgramSchedule"

    Public Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO)
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetProgramSchedule(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetProgramScheduleByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(ByVal _filter As RC_TransferCAN_ToEmployeeDTO) As RC_TransferCAN_ToEmployeeDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)
        Dim lstProgram As List(Of ProgramScheduleCanDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgram = rep.GetCandidateScheduleByScheduleID(_filter)
                Return lstProgram
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)
        Dim lstProgram As List(Of ProgramScheduleCanDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgram = rep.GetCandidateNotScheduleByScheduleID(_filter)
                Return lstProgram
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramSchedule(ByVal objExams As ProgramScheduleDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateProgramSchedule(objExams, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CandidateResult"

    Public Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)
        Dim lstClassCandidate As List(Of ProgramScheduleCanDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstClassCandidate = rep.GetCandidateResult(_filter)
                Return lstClassCandidate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateCandidateResult(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function
#End Region

#Region "Cost"

    Public Function GetCost(ByVal _filter As CostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO)
        Dim lstCost As List(Of CostDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstCost = rep.GetCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCost(ByVal objExams As CostDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateCost(objExams, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCost(ByVal objCost As CostDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateCost(objCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCost(ByVal obj As CostDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCost(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Manning"
    Public Sub GetTemplateInfo(ByVal programID As Decimal, ByRef fileUrl As String, ByRef fileOut_name As String, ByRef Mid As String)
        Dim pr As New List(Of List(Of Object))

        pr = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_CUR = OUT_CURSOR})
        Dim dsTemplate = rep.ExecuteStore("PKG_HCM_SYSTEM.PRR_GET_TEMPLATE", pr)

        fileUrl = If(dsTemplate.Tables(0).Rows(0).Item("TEMPLATE_URL").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("TEMPLATE_URL"))
        fileOut_name = If(dsTemplate.Tables(0).Rows(0).Item("FILE_OUT_NAME").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("FILE_OUT_NAME"))
        Mid = If(dsTemplate.Tables(0).Rows(0).Item("MID").ToString = "", "", dsTemplate.Tables(0).Rows(0).Item("MID"))
    End Sub

    Public Shared Function CreateParameterList(Of T)(ByVal parameters As T) As List(Of List(Of Object))
        Dim lstParameter As New List(Of List(Of Object))

        For Each info As PropertyInfo In parameters.GetType().GetProperties()
            Dim param As New List(Of Object)

            param.Add(info.Name)
            param.Add(info.GetValue(parameters, Nothing))

            lstParameter.Add(param)
        Next

        Return lstParameter
    End Function
#End Region

#Region "CV Pool"
    Public Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CVPoolEmpDTO)
        Dim lstProgram As List(Of CVPoolEmpDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstProgram = rep.GetCVPoolEmpRecord(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstProgram
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "HR_Planing"
    Public Function GetHRYearPlaning(ByVal _filter As HRYearPlaningDTO, Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HRYearPlaningDTO)
        Dim lstHrYearPl As List(Of HRYearPlaningDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstHrYearPl = rep.GetHRYearPlaning(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstHrYearPl
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertHRYearPlaning(objHRYearPlaning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckExistEffectDate_HRYearPlaning(ByVal Id As Decimal, ByVal EffectDate As Date) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExistEffectDate_HRYearPlaning(Id, EffectDate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyHRYearPlaning(objHRYearPlaning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteHRYearPlaning(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteHRYearPlaning(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateHRYearPlaning(ByVal year As Decimal, ByVal effect_date As Date, ByVal _id As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateHRYearPlaning(year, effect_date, _id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckExistData(ByVal _id As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExistData(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetHrYearPlaningByID(ByVal _id As Decimal) As HRYearPlaningDTO
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetHrYearPlaningByID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "HR_PL_DETAIL"
    Public Function GetPlanDetail(ByVal _filter As HRPlaningDetailDTO, ByVal _param As ParamDTO, Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HRPlaningDetailDTO)
        Dim lstHrPlDetail As List(Of HRPlaningDetailDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstHrPlDetail = rep.GetPlanDetail(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstHrPlDetail
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertHRPlanDetail(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyHRPlanDetail(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteHRPlanDetail(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteHRPlanDetail(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateHRPlanDetail(ByVal ORG_ID As Decimal, ByVal TITLE_ID As Decimal, ByVal YEAR_PLAN_ID As Decimal, ByVal _id As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateHRPlanDetail(ORG_ID, TITLE_ID, YEAR_PLAN_ID, _id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckExistRankSal(ByVal _id As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckExistRankSal(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDetailOrgTitle(ByVal _filter As HRPlaningDetailDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal Sorts As String = "TITLE_NAME") As DataTable
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetDetailOrgTitle(_filter, _param, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyHRBudgetDetail(ByVal lstObj As List(Of HRPlaningDetailDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyHRBudgetDetail(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "RC_CANDIDATE_CONTRACT"
    Public Function GetRCNegotiate(ByVal _filter As RCNegotiateDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RCNegotiateDTO)
        Dim lstRCNegotiate As List(Of RCNegotiateDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstRCNegotiate = rep.GetRCNegotiate(_filter, _param, 0, Integer.MaxValue, 0, Sorts, Me.Log)
                Return lstRCNegotiate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertRCNegotiate(objRCNegotiate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertRcSalaryCandidate(ByVal obj As RC_Salary_CandidateDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertRcSalaryCandidate(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertRcAlowCandidate(ByVal obj As List(Of RC_Allowance_CandidateDTO)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertRcAlowCandidate(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function DeleteRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO) As Decimal
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteRCNegotiate(objRCNegotiate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "RC_TRANSFERCAN_TOEMPLOYEE"
    Public Function Insert_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.Insert_RC_TRANSFERCAN_TOEMPLOYEE(objDTO, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Modify_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.Modify_RC_TRANSFERCAN_TOEMPLOYEE(objDTO, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
    Public Function GetTitleByOrg(ByVal OrgID As Decimal) As DataTable
        Dim lst As DataTable
        Using rep As New RecruitmentBusinessClient
            Try
                lst = rep.GetTitleByOrg(OrgID)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetRCAllowanceCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Allowance_CandidateDTO)
        Dim lst As List(Of RC_Allowance_CandidateDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lst = rep.GetRCAllowanceCandidate(programID, candidateID)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetRCSalaryCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Salary_CandidateDTO)
        Dim lst As List(Of RC_Salary_CandidateDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lst = rep.GetRCSalaryCandidate(programID, candidateID)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckTransferToEmployee(ByVal CandidateId As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.CheckTransferToEmployee(CandidateId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class
