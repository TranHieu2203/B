Imports Training.TrainingBusiness

Partial Class TrainingRepository

#Region "Otherlist"

    Public Function GetCourseList() As List(Of CourseDTO)
        Dim lstCourse As List(Of CourseDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCourse = rep.GetCourseList()
                Return lstCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTitlesByOrgs(orgIds, langCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEntryAndFormByCourseID(CourseId, langCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetCenters() As List(Of CenterDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetCenters()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Plan"

    Public Function GetPlanEmployee(ByVal filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal lstTitleId As List(Of Decimal), ByVal lstTitleGR As List(Of Decimal), ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlanEmployee(filter, _param, PageIndex, lstTitleId, lstTitleGR, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PlanDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlans(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPlanById(ByVal Id As Decimal) As PlanDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlanById(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPlan(ByVal plan As PlanDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertPlan(plan, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyPlan(ByVal plan As PlanDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyPlan(plan, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePlans(ByVal lstPlanIDs As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePlans(lstPlanIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_TR_PLAN(ByVal DATA_IN As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.IMPORT_TR_PLAN(DATA_IN, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IMPORT_TITLECOURSE(ByVal DATA_IN As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.IMPORT_TITLECOURSE(DATA_IN, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PLAN_DATA_IMPORT(ByVal P_ORG_ID As Decimal) As DataSet
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GET_PLAN_DATA_IMPORT(P_ORG_ID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_TITLE_COURSE_IMPORT() As DataSet
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GET_TITLE_COURSE_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTitleByGroupID(ByVal _lstGroupID As List(Of Decimal)) As List(Of TitleDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTitleByGroupID(_lstGroupID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Request"

    Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer) As Int32
        Using rep As New TrainingBusinessClient
            Try

                Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        Using rep As New TrainingBusinessClient
            Try

                Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequests(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetTrainingRequestPortalApprove(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequestPortalApprove(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetTrainingRequestPortal(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RequestDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequestPortal(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetTrainingRequestsByID(filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeByImportRequest(lstEmpCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeByPlanID(filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function InsertRequest(ByVal Request As RequestDTO,
                                  lstEmp As List(Of RequestEmpDTO),
                                  ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertRequest(Request, lstEmp, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyRequest(ByVal Request As RequestDTO,
                                  lstEmp As List(Of RequestEmpDTO),
                                  ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyRequest(Request, lstEmp, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateStatusTrainingRequests(lstID, status)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteTrainingRequests(lstRequestID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function SubmitTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.SubmitTrainingRequests(lstRequestID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function RejectTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.RejectTrainingRequests(lstApprove)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ApproveTrainingRequests(ByVal lstApprove As List(Of RequestDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ApproveTrainingRequests(lstApprove)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlanRequestByID(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmployeeByCode(ByVal _employee_Code As String) As Decimal
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeByCode(_employee_Code)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Program"
    Public Function GetEmpByTitleAndOrg(ByVal titleId As Decimal, ByVal orgId As Decimal) As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmpByTitleAndOrg(titleId, orgId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetRequestsForProgram(ReqID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPrograms(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetProgramEvaluatePortal(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetProgramEvaluatePortal(filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPrograms_Portal(ByVal filter As ProgramDTO, Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPrograms_Portal(filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPlan_Cost_Detail(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetProgramById(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetProgramByChooseFormId(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertProgram(ByVal Program As ProgramDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertProgram(Program, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyProgram(ByVal Program As ProgramDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgram(Program, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePrograms(ByVal lstProgramIDs As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePrograms(lstProgramIDs)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Prepare"

    Public Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)
        Dim lstPrepare As List(Of ProgramPrepareDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstPrepare = rep.GetPrepare(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPrepare
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertPrepare(objPrepare, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyPrepare(objPrepare, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePrepare(ByVal lstPrepare As List(Of ProgramPrepareDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeletePrepare(lstPrepare)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Class"

    Public Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Dim lstClass As New DataTable

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetClass(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetClassByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClass(ByVal objClass As ProgramClassDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClass(objClass, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClass(ByVal objClass As ProgramClassDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyClass(objClass, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClass(ByVal lstClass As List(Of ProgramClassDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClass(lstClass)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "ClassStudent"

    Public Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Dim lstClass As List(Of ProgramClassStudentDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetEmployeeByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Dim lstClass As List(Of ProgramClassStudentDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClass = rep.GetEmployeeNotByClassID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClass
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClassStudent(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClassStudent(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ClassSchedule"

    Public Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)
        Dim lstClassSchedule As List(Of ProgramClassScheduleDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetClassSchedule(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO

        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetClassScheduleByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClassSchedule(objClassSchedule, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyClassSchedule(objClassSchedule, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteClassSchedule(ByVal lstClassSchedule As List(Of ProgramClassScheduleDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClassSchedule(lstClassSchedule)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "ProgramCommit"


    Public Function GetProgramCommit(ByVal _filter As ProgramCommitDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)
        Dim lstClassSchedule As List(Of ProgramCommitDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetProgramCommit(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateProgramCommit(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



#End Region

#Region "ProgramResult"


    Public Function GetProgramResult(ByVal _filter As ProgramResultDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)
        Dim lstClassSchedule As List(Of ProgramResultDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetProgramResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateProgramResult(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function CheckProgramResult(ByVal lst As List(Of ProgramResultDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.CheckProgramResult(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendTrainingToEmployeeProfile(ByVal listTrainingId As List(Of Decimal), ByVal issuedDate As Date) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.SendTrainingToEmployeeProfile(listTrainingId, issuedDate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTRResult(ByVal _filter As ProgramResultDTO) As List(Of ProgramResultDTO)
        Dim lstClassSchedule As List(Of ProgramResultDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetTRResult(_filter)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateCerificateConfirm(ByVal listTrainingId As List(Of Decimal)) As ProgramResultDTO

        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCerificateConfirm(listTrainingId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "ProgramCost"

    Public Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)
        Dim lstProgramCost As List(Of ProgramCostDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstProgramCost = rep.GetProgramCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstProgramCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertProgramCost(objProgramCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateProgramCost(objProgramCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgramCost(objProgramCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProgramCost(ByVal lstProgramCost As List(Of ProgramCostDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteProgramCost(lstProgramCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Reimbursement"

    Public Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)
        Dim lstReimbursement As List(Of ReimbursementDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstReimbursement = rep.GetReimbursement(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstReimbursement
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetReimbursementNew(ByVal _filter As ProgramCommitDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCommitDTO)
        Dim lstReimbursement As List(Of ProgramCommitDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstReimbursement = rep.GetReimbursementNew(_filter, PageIndex, PageSize, Total, _param, Me.Log, Sorts)
                Return lstReimbursement
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertReimbursement(objReimbursement, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertRegisterTraining_Portal(ByVal obj As ProgramEmpDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertRegisterTraining_Portal(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateReimbursement(objReimbursement)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyReimbursement(objReimbursement, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyRegisterTraining_Portal(ByVal obj As ProgramEmpDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyRegisterTraining_Portal(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveReimbursement(ByVal lstID As List(Of Decimal), ByVal bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveReimbursement(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgramCommit(objProgramCommit, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function FastUpdateProgramCommit(ByVal objProgramCommit As ProgramCommitDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.FastUpdateProgramCommit(objProgramCommit, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteReimbursement(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteReimbursement(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "ChooseForm"

    Public Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)
        Dim lstChooseForm As List(Of ChooseFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstChooseForm = rep.GetChooseForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstChooseForm
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertChooseForm(objChooseForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateChooseForm(objChooseForm)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyChooseForm(objChooseForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteChooseForm(ByVal lst As List(Of ChooseFormDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteChooseForm(lst)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ChooseForm"

    Public Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)

        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeAssessmentResult(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAssessmentResultByID(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetAssessmentResultByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetAssessmentResultByID_Portal(ByVal _filter As TR_CriteriaDTO) As List(Of TR_CriteriaDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetAssessmentResultByID_Portal(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateAssessmentResult(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateAssessmentResult_Portal(ByVal obj As AssessmentResultDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateAssessmentResult_Portal(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ChangeStatusAssessmentResult(lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee_record"

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Dim lstCertificate As List(Of RecordEmployeeDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetListEmployeePaging(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Dim lstCertificate As List(Of RecordEmployeeDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetEmployeeRecord(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetPortalListEmployeePaging(ByVal _filter As RecordEmployeeDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Dim lstCertificate As List(Of RecordEmployeeDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetPortalListEmployeePaging(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPortalListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of RecordEmployeeDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetPortalListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Employee Title Course"
    Public Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc") As List(Of EmployeeTitleCourseDTO)
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GetEmployeeTitleCourse(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
    Public Function GET_DTB(ByVal sType As Decimal, ByVal sEMP As Decimal) As Decimal
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GET_DTB(sType, sEMP)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_DTB_PORTAL(ByVal sType As Decimal, ByVal sEMP As Decimal, ByVal sPROID As Decimal) As Decimal
        Using rep As New TrainingBusinessClient
            Try
                Return rep.GET_DTB_PORTAL(sType, sEMP, sPROID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function IMPORT_PROGRAM_RESULT(ByVal P_DOCXML As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.IMPORT_PROGRAM_RESULT(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_REIMBURSEMENT(ByVal P_DOCXML As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.IMPORT_REIMBURSEMENT(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_TR_REQUEST(ByVal P_DOCXML As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.IMPORT_TR_REQUEST(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#Region "ProgramClassCardroll"
    Public Function GetStudentInClass(ByVal _filter As ProgramClassRollcardDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByVal _param As ParamDTO,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "ID desc") As List(Of ProgramClassRollcardDTO)
        Dim lstProgramClassRollcardDTO As List(Of ProgramClassRollcardDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstProgramClassRollcardDTO = rep.GetStudentInClass(_filter, PageIndex, PageSize, _param, Total, Sorts, Me.Log)
                Return lstProgramClassRollcardDTO
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStudentInClass(ByVal _filter As ProgramClassRollcardDTO,
                                            ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "ID desc") As List(Of ProgramClassRollcardDTO)
        Dim lst As List(Of ProgramClassRollcardDTO)

        Using rep As New TrainingBusinessClient
            Try
                lst = rep.GetStudentInClass(_filter, 0, Integer.MaxValue, _param, 0, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetProgramClassRollcard(ByVal _filter As ProgramClassRollcardDTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByVal _param As ParamDTO,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "ID desc") As List(Of ProgramClassRollcardDTO)
        Dim lstProgramClassRollcardDTO As List(Of ProgramClassRollcardDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstProgramClassRollcardDTO = rep.GetProgramClassRollcard(_filter, PageIndex, PageSize, _param, Total, Sorts, Me.Log)
                Return lstProgramClassRollcardDTO
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramClassRollcard(ByVal _filter As ProgramClassRollcardDTO,
                                            ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "ID desc") As List(Of ProgramClassRollcardDTO)
        Dim lst As List(Of ProgramClassRollcardDTO)

        Using rep As New TrainingBusinessClient
            Try
                lst = rep.GetProgramClassRollcard(_filter, 0, Integer.MaxValue, _param, 0, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertProgramClassRollcard(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateProgramClassRollcard(ByVal lstObj As List(Of ProgramClassRollcardDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateProgramClassRollcard(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyProgramClassRollcard(ByVal obj As ProgramClassRollcardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyProgramClassRollcard(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "TR CLASS RESULT"


    Public Function GetClassResult(ByVal _filter As ProgramClassStudentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)
        Dim lstClassSchedule As List(Of ProgramClassStudentDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassSchedule = rep.GetClassResult(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassSchedule
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateClassResultt(ByVal lst As List(Of ProgramClassStudentDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.UpdateClassResultt(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

End Class
