Imports Framework.Data
Imports RecruitmentDAL


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Partial Class RecruitmentBusiness
        Function ImportRC(ByVal Data As DataTable, ByVal ProGramID As Decimal, Optional ByVal log As UserLog = Nothing) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ImportRC
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ImportRC(Data, ProGramID, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#Region "PlanReg"

        Public Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanRegDTO) Implements ServiceContracts.IRecruitmentBusiness.GetPlanReg
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanReg(_filter, PageIndex, PageSize, Total, _param, Sorts, isSearch, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO Implements ServiceContracts.IRecruitmentBusiness.GetPlanRegByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanRegByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertPlanReg(objPlanReg, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyPlanReg(objPlanReg, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeletePlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.DeletePlanReg(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusPlanReg
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateStatusPlanReg(lstID, status)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


#End Region

#Region "PlanSummary"

        Public Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetPlanSummary
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanSummary(_year, _param, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "Request"

        Public Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRequest
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequest(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetListOfferCandidate(ByVal _filter As ListOfferCandidateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ListOfferCandidateDTO) Implements ServiceContracts.IRecruitmentBusiness.GetListOfferCandidate
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetListOfferCandidate(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO Implements ServiceContracts.IRecruitmentBusiness.GetRequestByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequestByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertRequest(objRequest, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyRequest(objRequest, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteRequest(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteOfferCandidate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteOfferCandidate
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteOfferCandidate(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusRequest(ByVal lstID As List(Of Decimal),
                                            ByVal status As Decimal,
                                            ByVal log As UserLog,
                                            ByVal Insert_Pro As Boolean, Optional ByVal IS_CONFIRM As Boolean = False) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateStatusRequest(lstID, status, log, Insert_Pro, IS_CONFIRM)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function CancelRequest(ByVal lstID As List(Of Decimal), ByVal reason As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.CancelRequest
            Try
                Return RecruitmentRepositoryStatic.Instance.CancelRequest(lstID, reason, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function AutoGenCodeRequestRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String Implements ServiceContracts.IRecruitmentBusiness.AutoGenCodeRequestRC
            Try
                Return RecruitmentRepositoryStatic.Instance.AutoGenCodeRequestRC(firstChar, tableName, colName)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region
#Region "Request_Portal"

        Public Function GetRequestPortal(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRequestPortal
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequestPortal(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetRequestPortal_Approve(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRequestPortal_Approve
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequestPortal_Approve(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetRequesPortaltByID(ByVal _filter As RequestPortalDTO) As RequestPortalDTO Implements ServiceContracts.IRecruitmentBusiness.GetRequestPortalByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRequestPortalByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRequestPortal
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertRequestPortal(objRequest, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyRequestPortal
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyRequestPortal(objRequest, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteRequestPortal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteRequestPortal
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteRequestPortal(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusRequestPortal(ByVal lstID As List(Of Decimal),
                                            ByVal status As Decimal,
                                            ByVal log As UserLog,
                                            ByVal Insert_Pro As Boolean) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusRequestPortal
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateStatusRequestPortal(lstID, status, log, Insert_Pro)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function AutoGenCodeRequestPortalRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String Implements ServiceContracts.IRecruitmentBusiness.AutoGenCodeRequestPortalRC
            Try
                Return RecruitmentRepositoryStatic.Instance.AutoGenCodeRequestPortalRC(firstChar, tableName, colName)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region
#Region "Program"

        Public Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgram
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgram(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramSearch
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramSearch(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO Implements ServiceContracts.IRecruitmentBusiness.GetProgramByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyProgram(ByVal objProgram As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyProgram
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyProgram(objProgram, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function XuatToTrinh(ByVal sID As Decimal) As DataTable _
            Implements ServiceContracts.IRecruitmentBusiness.XuatToTrinh
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.XuatToTrinh(sID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#Region "ProgramExams"

        Public Function GetProgramExams(ByVal _filter As ProgramExamsDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramExams
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramExams(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function Get_EXAMS_ORDER(ByVal RC_PROGRAM_ID) As Decimal Implements ServiceContracts.IRecruitmentBusiness.Get_EXAMS_ORDER
            Try
                Dim SORT = RecruitmentRepositoryStatic.Instance.Get_EXAMS_ORDER(RC_PROGRAM_ID)
                Return SORT
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO _
            Implements ServiceContracts.IRecruitmentBusiness.GetProgramExamsByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramExamsByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramExams
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateProgramExams(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteProgramExams
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteProgramExams(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#End Region
#Region "RC_User_Title_permision"
        Public Function GETUSER_TITLE_PERMISION(ByVal _filter As RC_USER_TITLE_PERMISIONDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of RC_USER_TITLE_PERMISIONDTO) Implements ServiceContracts.IRecruitmentBusiness.GETUSER_TITLE_PERMISION
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GETUSER_TITLE_PERMISION(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GETUSER_TITLE_PERMISION_BY_USER(Optional ByVal log As UserLog = Nothing) As List(Of RC_USER_TITLE_PERMISIONDTO) Implements ServiceContracts.IRecruitmentBusiness.GETUSER_TITLE_PERMISION_BY_USER
            Try
                Return RecruitmentRepositoryStatic.Instance.GETUSER_TITLE_PERMISION_BY_USER(log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function UpdateUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO, Optional ByVal log As UserLog = Nothing) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateUSER_TITLE_PERMISION
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateUSER_TITLE_PERMISION(obj, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteUSER_TITLE_PERMISION
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteUSER_TITLE_PERMISION(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Candidate"

        Public Function CheckExistCandidate(ByVal strEmpCode As String) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.CheckExistCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.CheckExistCandidate(strEmpCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_CANDIDATE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.IMPORT_CANDIDATE
            Try
                Dim rep As New RecruitmentRepository
                Return rep.IMPORT_CANDIDATE(P_DOCXML, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckExist_Program_Candidate(ByVal lstCandidateID As List(Of Decimal), ByVal ProgramID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.CheckExist_Program_Candidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.CheckExist_Program_Candidate(lstCandidateID, ProgramID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateInsertCandidate(ByVal sEmpId As String, ByVal sID_No As String, ByVal sFullName As String,
                                               ByVal dBirthDate As Date, ByVal sType As String) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ValidateInsertCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ValidateInsertCandidate(sEmpId, sID_No, sFullName, dBirthDate, sType)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateFamily_ByID
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetCandidateFamily_ByID(sCandidateID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte() _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateImage
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateImage(gEmpID, sError)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CreateNewCandidateCode() As CandidateDTO _
                               Implements ServiceContracts.IRecruitmentBusiness.CreateNewCandidateCode

            Try
                Dim rep As New RecruitmentRepository
                Return rep.CreateNewCandidateCode()
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertCandidateCode")
                Throw ex
            End Try

        End Function
        Public Function InsertProgramCandidate(ByVal lstID As List(Of Decimal), ByVal RC_PROGRAM_ID As Decimal, ByVal log As UserLog) As Boolean _
                                Implements ServiceContracts.IRecruitmentBusiness.InsertProgramCandidate

            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertProgramCandidate(lstID, RC_PROGRAM_ID, log)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertProgramCandidate")
                Throw ex
            End Try

        End Function
        Public Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByRef _strEmpCode As String, _
                                        ByVal _imageBinary As Byte(), _
                                         ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                         ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean _
                                Implements ServiceContracts.IRecruitmentBusiness.InsertCandidate

            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidate(objEmp, log, gID, _strEmpCode, _imageBinary, objEmpCV, objEmpEdu _
                                             , objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertCandidate")
                Throw ex
            End Try

        End Function

        Public Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                         ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                        ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean _
                                Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidate(objEmp, log, gID, _imageBinary, objEmpCV, objEmpEdu, _
                                             objEmpOther, objEmpHealth, objEmpExpect, objEmpFamily)
            Catch ex As Exception
                WriteExceptionLog(ex, "ModifyCandidate")
                Throw ex
            End Try

        End Function
        Public Function INSERT_RC_PROGRAM_CANDIDATE(ByVal obj As RC_PROGRAM_CANDIDATEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.INSERT_RC_PROGRAM_CANDIDATE
            Try
                Return RecruitmentRepositoryStatic.Instance.INSERT_RC_PROGRAM_CANDIDATE(obj, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidatePaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetFindCandidatePaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetFindCandidatePaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidateTransferPaging
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidateTransferPaging(PageIndex, PageSize, Total, _filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListCandidate(ByVal _filter As CandidateDTO,
                                     Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO) _
              Implements ServiceContracts.IRecruitmentBusiness.GetListCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetListCandidate(_filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidate(lstEmpID, log, sError)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Lay thong tin nhan vien tu CandidateCode
        ''' </summary>
        ''' <param name="sCandidateCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO _
                                             Implements ServiceContracts.IRecruitmentBusiness.GetCandidateInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateInfo(sCandidateCode)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try

        End Function
        Public Function GETCANDIDATEINFO_BY_PSC_ID(ByVal PSC_ID As String) As CandidateDTO _
                                             Implements ServiceContracts.IRecruitmentBusiness.GETCANDIDATEINFO_BY_PSC_ID
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GETCANDIDATEINFO_BY_PSC_ID(PSC_ID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try

        End Function
        Public Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateCV
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateCV(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateEdu
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateEdu(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO _
                                                 Implements ServiceContracts.IRecruitmentBusiness.GetCandidateOtherInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateOtherInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateHealthInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateHealthInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO _
                                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateExpectInfo
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateExpectInfo(sCandidateID)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO) _
                                            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateHistory
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateHistory(sCandidateID, sCandidateIDNO)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), ByVal programID As Decimal, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateProgramCandidate(lstCanID, programID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), ByVal statusID As String, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateStatusCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateStatusCandidate(lstCanID, statusID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdatePontentialCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdatePontentialCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateBlackListCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateBlackListCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateBlackListRc_Program_Candidate(ByVal lstCanID As List(Of Decimal), ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateBlackListRc_Program_Candidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateBlackListRc_Program_Candidate(lstCanID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean _
                Implements ServiceContracts.IRecruitmentBusiness.UpdateReHireCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.UpdateReHireCandidate(lstCanID, bCheck, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCandidateImport() As DataSet _
              Implements ServiceContracts.IRecruitmentBusiness.GetCandidateImport
            Try
                Dim rep As New RecruitmentRepository
                Return rep.GetCandidateImport
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ImportCandidate(ByVal lst As List(Of CandidateImportDTO), ByVal log As UserLog) As Boolean _
                      Implements ServiceContracts.IRecruitmentBusiness.ImportCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ImportCandidate(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function TransferHSNVToCandidate(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal,
                                            ByVal log As UserLog) As Boolean _
                      Implements ServiceContracts.IRecruitmentBusiness.TransferHSNVToCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.TransferHSNVToCandidate(empID, orgID, titleID, programID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "CandidateFamily"
        Public Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateFamily
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateFamily(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.InsertCandidateFamily(objFamily, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.ModifyCandidateFamily(objFamily, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteCandidateFamily(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateFamily(ByVal objFamily As CandidateFamilyDTO) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ValidateFamily
            Try
                Return RecruitmentRepositoryStatic.Instance.ValidateFamily(objFamily)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "Cá nhân tự đào tạo"
        Public Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateTrainSinger(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateTrainSinger(objTrainSinger, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateTrainSinger(objTrainSinger, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateTrainSinger(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateTrainSinger
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateTrainSinger(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "Người tham chiếu"
        Public Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateReference(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateReference(ByVal objReference As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateReference(objReference, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateReference(ByVal objReference As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateReference(objReference, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateReference(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateReference
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateReference(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
#Region "Trước khi vào MLG"
        Public Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO) _
            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Dim lst = rep.GetCandidateBeforeWT(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.InsertCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertCandidateBeforeWT(objCandidateBeforeWT, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.ModifyCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.ModifyCandidateBeforeWT(objCandidateBeforeWT, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCandidateBeforeWT(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.DeleteCandidateBeforeWT
            Try
                Dim rep As New RecruitmentRepository
                Return rep.DeleteCandidateBeforeWT(lstDecimals, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#End Region

#Region "ProgramSchedule"

        Public Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO) Implements ServiceContracts.IRecruitmentBusiness.GetProgramSchedule
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function CheckExist_Program_Schedule_Can(ByVal CanID As Decimal, ByVal ProID As Decimal, ByVal Exams_Order As Decimal) As Boolean _
            Implements ServiceContracts.IRecruitmentBusiness.CheckExist_Program_Schedule_Can
            Try
                Dim rep As New RecruitmentRepository
                Return rep.CheckExist_Program_Schedule_Can(CanID, ProID, Exams_Order)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO Implements ServiceContracts.IRecruitmentBusiness.GetProgramScheduleByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramScheduleByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(ByVal _filter As RC_TransferCAN_ToEmployeeDTO) As RC_TransferCAN_ToEmployeeDTO Implements ServiceContracts.IRecruitmentBusiness.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) _
                            Implements ServiceContracts.IRecruitmentBusiness.GetCandidateNotScheduleByScheduleID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateNotScheduleByScheduleID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) _
                                    Implements ServiceContracts.IRecruitmentBusiness.GetCandidateScheduleByScheduleID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateScheduleByScheduleID(_filter)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function UpdateProgramSchedule(ByVal objExams As ProgramScheduleDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateProgramSchedule
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateProgramSchedule(objExams, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CandidateResult"

        Public Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCandidateResult
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCandidateResult(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateCandidateResult
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateCandidateResult(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Cost"

        Public Function GetCost(ByVal _filter As CostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCost
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateCost(ByVal objExams As CostDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.UpdateCost
            Try
                Return RecruitmentRepositoryStatic.Instance.UpdateCost(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCost(ByVal objCost As CostDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ValidateCost
            Try
                Return RecruitmentRepositoryStatic.Instance.ValidateCost(objCost)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCost(ByVal obj As CostDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteCost
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteCost(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CV Pool"

        Public Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of CVPoolEmpDTO) Implements ServiceContracts.IRecruitmentBusiness.GetCVPoolEmpRecord
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetCVPoolEmpRecord(_filter, PageIndex, PageSize, Total, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "HR_Planing"
        Public Function GetHRYearPlaning(ByVal _filter As HRYearPlaningDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRYearPlaningDTO) Implements ServiceContracts.IRecruitmentBusiness.GetHRYearPlaning
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetHRYearPlaning(_filter, PageIndex, PageSize, Total, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertHRYearPlaning
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.InsertHRYearPlaning(objHRYearPlaning, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckExistEffectDate_HRYearPlaning(ByVal Id As Decimal, ByVal EffectDate As Date) As Boolean Implements ServiceContracts.IRecruitmentBusiness.CheckExistEffectDate_HRYearPlaning
            Try
                Dim IsExist As Boolean = RecruitmentRepositoryStatic.Instance.CheckExistEffectDate_HRYearPlaning(Id, EffectDate)
                Return IsExist
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyHRYearPlaning
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ModifyHRYearPlaning(objHRYearPlaning, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteHRYearPlaning(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteHRYearPlaning
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.DeleteHRYearPlaning(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateHRYearPlaning(ByVal year As Decimal, ByVal effect_date As Date, ByVal _id As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ValidateHRYearPlaning
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ValidateHRYearPlaning(year, effect_date, _id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckExistData(ByVal _id As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.CheckExistData
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.CheckExistData(_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHrYearPlaningByID(ByVal _id As Decimal) As HRYearPlaningDTO Implements ServiceContracts.IRecruitmentBusiness.GetHrYearPlaningByID
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetHrYearPlaningByID(_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "HR_PL_DETAIL"
        Public Function GetPlanDetail(ByVal _filter As HRPlaningDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRPlaningDetailDTO) Implements ServiceContracts.IRecruitmentBusiness.GetPlanDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetPlanDetail(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertHRPlanDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.InsertHRPlanDetail(lstObj, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyHRPlanDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ModifyHRPlanDetail(lstObj, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteHRPlanDetail(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.DeleteHRPlanDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.DeleteHRPlanDetail(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateHRPlanDetail(ByVal ORG_ID As Decimal, ByVal TITLE_ID As Decimal, ByVal YEAR_PLAN_ID As Decimal, ByVal _id As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ValidateHRPlanDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ValidateHRPlanDetail(ORG_ID, TITLE_ID, YEAR_PLAN_ID, _id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckExistRankSal(ByVal _id As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.CheckExistRankSal
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.CheckExistRankSal(_id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyHRBudgetDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IRecruitmentBusiness.ModifyHRBudgetDetail
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.ModifyHRBudgetDetail(lstObj, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetDetailOrgTitle(ByVal _filter As HRPlaningDetailDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByRef Total As Integer = 0,
                                          Optional ByVal Sorts As String = "TITLE_NAME", Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetDetailOrgTitle
            Try
                Dim dsData = RecruitmentRepositoryStatic.Instance.GetDetailOrgTitle(_filter, _param, Total, Sorts, log)
                Return dsData
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "RC_CANDIDATE_CONTRACT"
        Public Function GetRCNegotiate(ByVal _filter As RCNegotiateDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", _
                                    Optional ByVal log As UserLog = Nothing) As List(Of RCNegotiateDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRCNegotiate
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRCNegotiate(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRCNegotiate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertRCNegotiate(objRCNegotiate, log, gID)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertRCNegotiate - " + "Time: " + Date.Now)
                Throw ex
            End Try
        End Function

        Public Function InsertRcSalaryCandidate(ByVal obj As RC_Salary_CandidateDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRcSalaryCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertRcSalaryCandidate(obj)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertRcSalaryCandidate - " + "Time: " + Date.Now)
                Throw ex
            End Try
        End Function

        Public Function InsertRcAlowCandidate(ByVal obj As List(Of RC_Allowance_CandidateDTO)) As Boolean Implements ServiceContracts.IRecruitmentBusiness.InsertRcAlowCandidate
            Try
                Dim rep As New RecruitmentRepository
                Return rep.InsertRcAlowCandidate(obj)
            Catch ex As Exception
                WriteExceptionLog(ex, "InsertRcAlowCandidate - " + "Time: " + Date.Now)
                Throw ex
            End Try
        End Function


        Public Function DeleteRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog) As Decimal Implements ServiceContracts.IRecruitmentBusiness.DeleteRCNegotiate
            Try
                Return RecruitmentRepositoryStatic.Instance.DeleteRCNegotiate(objRCNegotiate, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "RC_TRANSFERCAN_TOEMPLOYEE"
        Public Function Insert_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.Insert_RC_TRANSFERCAN_TOEMPLOYEE
            Try
                Return RecruitmentRepositoryStatic.Instance.Insert_RC_TRANSFERCAN_TOEMPLOYEE(objDTO, log, gID)
            Catch ex As Exception
                WriteExceptionLog(ex, "Insert_RC_TRANSFERCAN_TOEMPLOYEE - " + "Time: " + Date.Now)
                Throw ex
            End Try
        End Function

        Public Function Modify_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.Modify_RC_TRANSFERCAN_TOEMPLOYEE
            Try
                Return RecruitmentRepositoryStatic.Instance.Modify_RC_TRANSFERCAN_TOEMPLOYEE(objDTO, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetTitleByOrg(ByVal OrgID As Decimal) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetTitleByOrg
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetTitleByOrg(OrgID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetRCAllowanceCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Allowance_CandidateDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRCAllowanceCandidate
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRCAllowanceCandidate(programID, candidateID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetRCSalaryCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Salary_CandidateDTO) Implements ServiceContracts.IRecruitmentBusiness.GetRCSalaryCandidate
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetRCSalaryCandidate(programID, candidateID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckTransferToEmployee(ByVal CandidateId As Decimal) As Boolean Implements ServiceContracts.IRecruitmentBusiness.CheckTransferToEmployee
            Try
                Return RecruitmentRepositoryStatic.Instance.CheckTransferToEmployee(CandidateId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace