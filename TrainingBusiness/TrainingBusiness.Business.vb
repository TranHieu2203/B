Imports Framework.Data
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Class TrainingBusiness

#Region "Otherlist"

        Public Function GetCourseList() As List(Of CourseDTO) Implements ServiceContracts.ITrainingBusiness.GetCourseList
            Try
                Dim rep As New TrainingRepository
                Return rep.GetCourseList()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO) Implements ServiceContracts.ITrainingBusiness.GetTitlesByOrgs
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTitlesByOrgs(orgIds, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO) Implements ServiceContracts.ITrainingBusiness.GetWIByTitle
            Try
                Dim rep As New TrainingRepository
                Return rep.GetWIByTitle(orgIds, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO Implements ServiceContracts.ITrainingBusiness.GetEntryAndFormByCourseID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEntryAndFormByCourseID(CourseId, langCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Plan"
        Public Function GetPlanEmployee(ByVal filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal lstTitleId As List(Of Decimal), ByVal lstTitleGR As List(Of Decimal), ByVal PageSize As Integer,
                                    ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) Implements ServiceContracts.ITrainingBusiness.GetPlanEmployee
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlanEmployee(filter, _param, PageIndex, lstTitleId, lstTitleGR, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO) Implements ServiceContracts.ITrainingBusiness.GetPlans
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlans(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetPlanById(ByVal Id As Decimal) As PlanDTO Implements ServiceContracts.ITrainingBusiness.GetPlanById
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlanById(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertPlan
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertPlan(plan, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyPlan
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyPlan(plan, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePlans
            Try
                Dim rep As New TrainingRepository
                Return rep.DeletePlans(lstId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GET_PLAN_DATA_IMPORT(ByVal P_ORG_ID As Decimal, ByVal log As UserLog) As DataSet Implements ServiceContracts.ITrainingBusiness.GET_PLAN_DATA_IMPORT
            Try
                Dim rep As New TrainingRepository
                Return rep.GET_PLAN_DATA_IMPORT(P_ORG_ID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function GET_TITLE_COURSE_IMPORT() As DataSet Implements ServiceContracts.ITrainingBusiness.GET_TITLE_COURSE_IMPORT
            Try
                Dim rep As New TrainingRepository
                Return rep.GET_TITLE_COURSE_IMPORT()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function IMPORT_TR_PLAN(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.IMPORT_TR_PLAN
            Try
                Dim rep As New TrainingRepository
                Return rep.IMPORT_TR_PLAN(DATA_IN, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function IMPORT_TITLECOURSE(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.IMPORT_TITLECOURSE
            Try
                Dim rep As New TrainingRepository
                Return rep.IMPORT_TITLECOURSE(DATA_IN, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Function GetTitleByGroupID(ByVal _lstGroupID As List(Of Decimal)) As List(Of TitleDTO) Implements ServiceContracts.ITrainingBusiness.GetTitleByGroupID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTitleByGroupID(_lstGroupID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Request"

        Public Function IMPORT_PROGRAM_RESULT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.IMPORT_PROGRAM_RESULT
            Try
                Dim rep As New TrainingRepository
                Return rep.IMPORT_PROGRAM_RESULT(P_DOCXML, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_REIMBURSEMENT(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.IMPORT_REIMBURSEMENT
            Try
                Dim rep As New TrainingRepository
                Return rep.IMPORT_REIMBURSEMENT(P_DOCXML, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_TR_REQUEST(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.IMPORT_TR_REQUEST
            Try
                Dim rep As New TrainingRepository
                Return rep.IMPORT_TR_REQUEST(P_DOCXML, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) _
                                     Implements ServiceContracts.ITrainingBusiness.GetTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequests(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetTrainingRequestPortalApprove(ByVal filter As RequestDTO,
                                                 ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) _
                                         Implements ServiceContracts.ITrainingBusiness.GetTrainingRequestPortalApprove
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequestPortalApprove(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetTrainingRequestPortal(ByVal filter As RequestDTO,
                                                 ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO) _
                                         Implements ServiceContracts.ITrainingBusiness.GetTrainingRequestPortal
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequestPortal(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO _
                                     Implements ServiceContracts.ITrainingBusiness.GetTrainingRequestsByID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetTrainingRequestsByID(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO) _
                                     Implements ServiceContracts.ITrainingBusiness.GetEmployeeByPlanID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeByPlanID(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String _
                                     Implements ServiceContracts.ITrainingBusiness.GetEmployeeByImportRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeByImportRequest(lstEmpCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32 _
            Implements ServiceContracts.ITrainingBusiness.PRI_PROCESS
            Try
                Dim rep As New TrainingRepository
                Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32 _
            Implements ServiceContracts.ITrainingBusiness.PRI_PROCESS_APP
            Try
                Dim rep As New TrainingRepository
                Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertRequest(Request, lstEmp, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
                              Implements ServiceContracts.ITrainingBusiness.ModifyRequest
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyRequest(Request, lstEmp, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean _
                              Implements ServiceContracts.ITrainingBusiness.UpdateStatusTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.UpdateStatusTrainingRequests(lstID, status)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.DeleteTrainingRequests(lstRequestID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function RejectTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean Implements ServiceContracts.ITrainingBusiness.RejectTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.ApproveTrainingRequests(lstApprove)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ApproveTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean Implements ServiceContracts.ITrainingBusiness.ApproveTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.ApproveTrainingRequests(lstApprove)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SubmitTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.SubmitTrainingRequests
            Try
                Dim rep As New TrainingRepository
                Return rep.SubmitTrainingRequests(lstRequestID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO Implements ServiceContracts.ITrainingBusiness.GetPlanRequestByID
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlanRequestByID(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Function GetEmployeeByCode(ByVal _employee_Code As String) As Decimal Implements ServiceContracts.ITrainingBusiness.GetEmployeeByCode
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeByCode(_employee_Code)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Program"
        Public Function GetEmpByTitleAndOrg(ByVal titleId As Decimal, ByVal orgId As Decimal) As List(Of RecordEmployeeDTO) Implements ServiceContracts.ITrainingBusiness.GetEmpByTitleAndOrg
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmpByTitleAndOrg(titleId, orgId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO Implements ServiceContracts.ITrainingBusiness.GetRequestsForProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.GetRequestsForProgram(ReqID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.ITrainingBusiness.GetPrograms
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPrograms(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProgramEvaluatePortal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramEvaluatePortal
            Try
                Dim rep As New TrainingRepository
                Return rep.GetProgramEvaluatePortal(filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPrograms_Portal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO) Implements ServiceContracts.ITrainingBusiness.GetPrograms_Portal
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPrograms_Portal(filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO) Implements ServiceContracts.ITrainingBusiness.GetPlan_Cost_Detail
            Try
                Dim rep As New TrainingRepository
                Return rep.GetPlan_Cost_Detail(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function GetProgramById(ByVal Id As Decimal) As ProgramDTO Implements ServiceContracts.ITrainingBusiness.GetProgramById
            Try
                Dim rep As New TrainingRepository
                Return rep.GetProgramById(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO _
            Implements ServiceContracts.ITrainingBusiness.GetProgramByChooseFormId
            Try
                Dim rep As New TrainingRepository
                Return rep.GetProgramByChooseFormId(Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.InsertProgram(Program, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgram
            Try
                Dim rep As New TrainingRepository
                Return rep.ModifyProgram(Program, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePrograms
            Try
                Dim rep As New TrainingRepository
                Return rep.DeletePrograms(lstId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Prepare"

        Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO) Implements ServiceContracts.ITrainingBusiness.GetPrepare
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetPrepare(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertPrepare
            Try
                Return TrainingRepositoryStatic.Instance.InsertPrepare(objPrepare, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyPrepare
            Try
                Return TrainingRepositoryStatic.Instance.ModifyPrepare(objPrepare, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeletePrepare
            Try
                Return TrainingRepositoryStatic.Instance.DeletePrepare(lstPrepare)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Class"

        Public Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable Implements ServiceContracts.ITrainingBusiness.GetClass
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClass(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO _
            Implements ServiceContracts.ITrainingBusiness.GetClassByID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertClass
            Try
                Return TrainingRepositoryStatic.Instance.InsertClass(objClass, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyClass
            Try
                Return TrainingRepositoryStatic.Instance.ModifyClass(objClass, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteClass
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClass(lstClass)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Class Student"

        Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetEmployeeNotByClassID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeNotByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetEmployeeByClassID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertClassStudent
            Try
                Return TrainingRepositoryStatic.Instance.InsertClassStudent(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.DeleteClassStudent
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClassStudent(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ClassSchedule"

        Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO) Implements ServiceContracts.ITrainingBusiness.GetClassSchedule
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO _
            Implements ServiceContracts.ITrainingBusiness.GetClassScheduleByID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassScheduleByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.InsertClassSchedule(objClassSchedule, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.ModifyClassSchedule(objClassSchedule, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteClassSchedule
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClassSchedule(lstClassSchedule)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ProgramCommit"

        Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramCommit
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramCommit(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateProgramCommit
            Try
                Return TrainingRepositoryStatic.Instance.UpdateProgramCommit(lst, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "ProgramResult"

        Public Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateProgramResult
            Try
                Return TrainingRepositoryStatic.Instance.UpdateProgramResult(lst, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function CheckProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean Implements ServiceContracts.ITrainingBusiness.CheckProgramResult
            Try
                Return TrainingRepositoryStatic.Instance.CheckProgramResult(lst)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function SendTrainingToEmployeeProfile(ByVal listTrainingId As List(Of Decimal), ByVal issuedDate As Date, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.SendTrainingToEmployeeProfile
            Try
                Return TrainingRepositoryStatic.Instance.SendTrainingToEmployeeProfile(listTrainingId, issuedDate, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetTRResult(ByVal _filter As ProgramResultDTO) As List(Of ProgramResultDTO) Implements ServiceContracts.ITrainingBusiness.GetTRResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetTRResult(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCerificateConfirm(ByVal listTrainingId As List(Of Decimal)) As ProgramResultDTO Implements ServiceContracts.ITrainingBusiness.ValidateCerificateConfirm
            Try
                Dim lst = TrainingRepositoryStatic.Instance.ValidateCerificateConfirm(listTrainingId)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "ProgramCost"

        Public Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramCost
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetProgramCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.InsertProgramCost(objProgramCost, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.ValidateProgramCost(objProgramCost)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.ModifyProgramCost(objProgramCost, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteProgramCost
            Try
                Return TrainingRepositoryStatic.Instance.DeleteProgramCost(lstProgramCost)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region


#Region "Reimbursement"

        Public Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO) Implements ServiceContracts.ITrainingBusiness.GetReimbursement
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetReimbursement(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetReimbursementNew(ByVal _filter As ProgramCommitDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCommitDTO) Implements ServiceContracts.ITrainingBusiness.GetReimbursementNew
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetReimbursementNew(_filter, PageIndex, PageSize, Total, _param, log, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.InsertReimbursement(objReimbursement, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertRegisterTraining_Portal
            Try
                Return TrainingRepositoryStatic.Instance.InsertRegisterTraining_Portal(obj, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.ValidateReimbursement(objReimbursement)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.ModifyReimbursement(objReimbursement, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyRegisterTraining_Portal(ByVal obj As ProgramEmpDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyRegisterTraining_Portal
            Try
                Return TrainingRepositoryStatic.Instance.ModifyRegisterTraining_Portal(obj, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.ActiveReimbursement(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgramCommit
            Try
                Return TrainingRepositoryStatic.Instance.ModifyProgramCommit(objProgramCommit, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function FastUpdateProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.FastUpdateProgramCommit
            Try
                Return TrainingRepositoryStatic.Instance.FastUpdateProgramCommit(objProgramCommit, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteReimbursement(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteReimbursement
            Try
                Return TrainingRepositoryStatic.Instance.DeleteReimbursement(lstID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "ChooseForm"

        Public Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO) Implements ServiceContracts.ITrainingBusiness.GetChooseForm
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetChooseForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.InsertChooseForm(objChooseForm, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.ValidateChooseForm(objChooseForm)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.ModifyChooseForm(objChooseForm, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteChooseForm
            Try
                Return TrainingRepositoryStatic.Instance.DeleteChooseForm(lst)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "AssessmentResult"

        Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO) _
                                    Implements ServiceContracts.ITrainingBusiness.GetEmployeeAssessmentResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetEmployeeAssessmentResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetAssessmentResultByID(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetAssessmentResultByID
            Try
                Return TrainingRepositoryStatic.Instance.GetAssessmentResultByID(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetAssessmentResultByID_Portal(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetAssessmentResultByID_Portal
            Try
                Return TrainingRepositoryStatic.Instance.GetAssessmentResultByID_Portal(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GET_DTB(ByVal sType As Decimal, ByVal sEMP As Decimal) As Decimal _
                Implements ServiceContracts.ITrainingBusiness.GET_DTB
            Try
                Return TrainingRepositoryStatic.Instance.GET_DTB(sType, sEMP)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_DTB_PORTAL(ByVal sType As Decimal, ByVal sEMP As Decimal, ByVal sPROID As Decimal) As Decimal _
                Implements ServiceContracts.ITrainingBusiness.GET_DTB_PORTAL
            Try
                Return TrainingRepositoryStatic.Instance.GET_DTB_PORTAL(sType, sEMP, sPROID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.ITrainingBusiness.UpdateAssessmentResult
            Try
                Return TrainingRepositoryStatic.Instance.UpdateAssessmentResult(obj, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateAssessmentResult_Portal(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.ITrainingBusiness.UpdateAssessmentResult_Portal
            Try
                Return TrainingRepositoryStatic.Instance.UpdateAssessmentResult_Portal(obj, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean _
                               Implements ServiceContracts.ITrainingBusiness.ChangeStatusAssessmentResult
            Try
                Return TrainingRepositoryStatic.Instance.ChangeStatusAssessmentResult(lstID, _status, _log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "TranningRecord"
        Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) _
              Implements ServiceContracts.ITrainingBusiness.GetListEmployeePaging

            Try
                Return TrainingRepositoryStatic.Instance.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                  ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                  Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) _
      Implements ServiceContracts.ITrainingBusiness.GetEmployeeRecord

            Try
                Return TrainingRepositoryStatic.Instance.GetEmployeeRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPortalListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO) _
              Implements ServiceContracts.ITrainingBusiness.GetPortalListEmployeePaging

            Try
                Return TrainingRepositoryStatic.Instance.GetPortalListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Employee Title Course"
        Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO) Implements ServiceContracts.ITrainingBusiness.GetEmployeeTitleCourse
            Try
                Dim rep As New TrainingRepository
                Return rep.GetEmployeeTitleCourse(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "ProgramClassRollcard"

        Public Function GetStudentInClass(ByVal _filter As ProgramClassRollcardDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "ID desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO) Implements ServiceContracts.ITrainingBusiness.GetStudentInClass
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetStudentInClass(_filter, PageIndex, PageSize, _param, Total, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetProgramClassRollcard(ByVal _filter As ProgramClassRollcardDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByVal _param As ParamDTO,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "ID desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of ProgramClassRollcardDTO) Implements ServiceContracts.ITrainingBusiness.GetProgramClassRollcard
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetProgramClassRollcard(_filter, PageIndex, PageSize, _param, Total, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertProgramClassRollcard
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.InsertProgramClassRollcard(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function UpdateProgramClassRollcard(ByVal lstObj As List(Of ProgramClassRollcardDTO),
                                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateProgramClassRollcard
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.UpdateProgramClassRollcard(lstObj, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyProgramClassRollcard
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.ModifyProgramClassRollcard(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteProgramClassRollcard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
                                                            Implements ServiceContracts.ITrainingBusiness.DeleteProgramClassRollcard
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.DeleteProgramClassRollcard(lstID, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "TR CLASS RESULT"

        Public Function GetClassResult(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO) Implements ServiceContracts.ITrainingBusiness.GetClassResult
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetClassResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateClassResultt(ByVal lst As List(Of ProgramClassStudentDTO),
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateClassResultt
            Try
                Return TrainingRepositoryStatic.Instance.UpdateClassResultt(lst, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace
