Imports Performance.PerformanceBusiness

Partial Class PerformanceRepository
#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
    Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal) As DataSet
        Dim lstEmployee As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lstEmployee = rep.GetEmployeeImport(group_obj_id, period_id, Me.Log)
                Return lstEmployee
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet
        Dim lstData As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetCriteriaImport(group_obj_id)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                             ByVal periodID As Decimal,
                                             ByVal group_obj_ID As Decimal) As Boolean
        Dim lst As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                lst = rep.ImportEmployeeAssessment(dtData, periodID, group_obj_ID, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal) As DataSet
        Dim lst As DataSet

        Using rep As New PerformanceBusinessClient
            Try
                lst = rep.GetEmployeeImportAssessment(_param, obj, P_PAGE_INDEX, P_PAGE_SIZE, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
    Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_ORGANIZATIONDTO)
        Dim lstData As List(Of PE_ORGANIZATIONDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetPe_Organization(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO
        Dim lstData As PE_ORGANIZATIONDTO

        Using rep As New PerformanceBusinessClient
            Try
                lstData = rep.GetPe_OrganizationByID(_filter)
                Return lstData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertPe_Organization(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyPe_Organization(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeletePe_Organization(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Assessment"
    Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        Dim lstAssessment As List(Of AssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstAssessment = rep.GetAssessment(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetAssessment(ByVal _filter As AssessmentDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        Dim lstAssessment As List(Of AssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstAssessment = rep.GetAssessment(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByRef gID As Decimal) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.ModifyAssessment(objAssessment, Me.Log, gID)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.UpdateStatusEmployeeAssessment(obj, Me.Log)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.GetListEmployeePortal(_filter)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function
    Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.GetTotalPointRating(obj)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Function Delete_PE_KPI_ASSESSMENT_DETAIL(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Delete_PE_KPI_ASSESSMENT_DETAIL(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

    Public Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessmentDirect(_empId, _year, _status)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_DM_STATUS() As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GET_DM_STATUS()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GET_PE_ASSESSMENT_HISTORY(_Id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP_1(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.CHECK_APP_2(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.PRI_PERFORMACE_APP(_empId, __PE_PERIOD_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.INSERT_PE_ASSESSMENT_HISTORY(P_PE_PE_ASSESSMENT_ID, P_RESULT_DIRECT, P_ASS_DATE, P_REMARK_DIRECT, P_CREATED_BY, P_CREATED_LOG, P_EMPLOYEE_ID, P_SIGN_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.PRI_PERFORMACE_PROCESS(P_EMPLOYEE_APP_ID, P_EMPLOYEE_ID, P_PE_PERIOD_ID, P_STATUS_ID, P_NOTES)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal) As Boolean
        Try
            Using rep As New PerformanceBusinessClient
                Return rep.UpdateStatusAssessmentDirect(obj, Me.Log)
            End Using
        Catch ex As Exception

            Throw ex
        End Try
    End Function


    Public Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKPIAssessEmp(_empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "CBNS xem KPI cua nhan vien"

    Public Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of AssessmentDirectDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetDirectKPIEmployee(filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "CBNS xem KPI cua nhan vien"
    Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessRatingEmployee(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAssessRatingEmployeeOrg(filter, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


#End Region

    Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.PRINT_PE_ASSESS(empID, period, obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


    Public Function DeleteKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteKpiAssessmentDetail(lstObj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.UpdateKpiAssessment(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.UpdateKpiAssessmentDetail(lstObj, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateDateAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateDateAssessment(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetKpiAssessmentByID(ByVal id As Decimal) As KPI_ASSESSMENT_DTO
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessmentByID(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetKpiAssessmentDetailHistory(ByVal lstID As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                         ByVal PageIndex As Integer,
                                                         ByVal PageSize As Integer,
                                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessmentDetailHistory(lstID, _filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetKpiAssessmentDetailByListCode(ByVal lstCode As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                     ByVal PageIndex As Integer,
                                                     ByVal PageSize As Integer,
                                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessmentDetailByListCode(lstCode, _filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetKpiAssessmentDetail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessmentDetail(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetKpiAssessment(ByVal _filter As KPI_ASSESSMENT_DTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessment(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetKpiAssessmentResult(ByVal _filter As KPI_ASSESSMENT_RESULT_DTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of KPI_ASSESSMENT_RESULT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetKpiAssessmentResult(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ChangeStatusAssessmentResult(lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetKpiAssessmentDetail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Dim lstKpiAssessment As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstKpiAssessment = rep.GetKpiAssessmentDetail(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
                Return lstKpiAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetKpiAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Dim lstKpiAssessment As List(Of KPI_ASSESSMENT_DTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstKpiAssessment = rep.GetKpiAssessment(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
                Return lstKpiAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetKpiAssessmentResult(ByVal _filter As KPI_ASSESSMENT_RESULT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of KPI_ASSESSMENT_RESULT_DTO)
        Dim lstKpiAssessment As List(Of KPI_ASSESSMENT_RESULT_DTO)

        Using rep As New PerformanceBusinessClient
            Try
                lstKpiAssessment = rep.GetKpiAssessmentResult(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
                Return lstKpiAssessment
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertKpiAssessment(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteKpiAssessment(ByVal objID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteKpiAssessment(objID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteKpiAssessmentResult(ByVal objID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteKpiAssessmentResult(objID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function SaveChangeRatio(ByVal list As List(Of KPI_ASSESSMENT_DTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.SaveChangeRatio(list, log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetPeriodByYear(ByVal Year As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPeriodByYear(Year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetDateByPeriod(ByVal Period As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetDateByPeriod(Period)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#Region "Portal Assessment"
    Public Function GetPortalAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPortalAssessment(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Get_Portal_Target_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Get_Portal_Target_Detail(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Get_Portal_Approve_Evaluate_Target(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Get_Portal_Approve_Evaluate_Target(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetPortalApprovedKPIAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPortalApprovedKPIAssessment(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_Portal_KPI_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Get_Portal_KPI_Detail(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetPortalAssessment(ByVal _filter As KPI_ASSESSMENT_DTO, Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPortalAssessment(_filter, 0, Integer.MaxValue, 0, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function Get_Portal_Target_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO, Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Get_Portal_Target_Detail(_filter, 0, Integer.MaxValue, 0, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function Get_Portal_Approve_Evaluate_Target(ByVal _filter As KPI_ASSESSMENT_DTO, Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of KPI_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.Get_Portal_Approve_Evaluate_Target(_filter, 0, Integer.MaxValue, 0, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer) As Int32
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_KPI_ASSESSMENT(ByVal DocXML As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.IMPORT_KPI_ASSESSMENT(DocXML, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSubmit(ByVal _kpi_ID As Decimal, ByVal _type As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.ValidateSubmit(_kpi_ID, _type)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "HTCH Assessment"
    Public Function GetHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessment(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessment(_filter, lstOrg, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetHTCHAssessmentDetail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessment_Detail(_filter, lstOrg, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetHTCHAssessment_Detail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessment_Detail(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetHTCHAssessmentByID(ByVal _id As Decimal) As PE_HTCH_ASSESSMENT_DTO
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessmentByID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetHTCHAssessmentListDetail(ByVal _id As Decimal) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetHTCHAssessmentListDetail(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SaveHTCHAssessment(ByVal objData As PE_HTCH_ASSESSMENT_DTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.SaveHTCHAssessment(objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SaveHTCHAssessmentListDetail(ByVal lstObj As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.SaveHTCHAssessmentListDetail(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Function DeleteHTCHAssessment(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.DeleteHTCHAssessment(lstID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function GetPeriodHTCHByYear(ByVal Year As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.GetPeriodHTCHByYear(Year)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


    Function GetYearHTCH(ByVal IS_BLANK As Boolean) As DataTable
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.GetYearHTCH(IS_BLANK)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function CAL_HTCH_ASSESSMENT(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal P_PERIOD As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CAL_HTCH_ASSESSMENT(_org_id, P_YEAR, P_PERIOD, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function CAL_HTCT_ASSESS_DTL(ByVal p_id As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CAL_HTCT_ASSESS_DTL(p_id, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function CHANGE_HTCH_ASSESSMENT_DTL(ByVal p_id As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CHANGE_HTCH_ASSESSMENT_DTL(p_id, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAproveHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetAproveHTCHAssessment(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Function GetApproveHTCHAssessmentDetail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.GetApproveHTCHAssessmentDetail(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_HTCH_ASSESSMENT(ByVal DocXML As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.IMPORT_HTCH_ASSESSMENT(DocXML, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region


#Region "Employee Period"

    Public Function GetPeriodByYear2(ByVal Year As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPeriodByYear2(Year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetDateByPeriod2(ByVal Period As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetDateByPeriod2(Period)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeePeriodHCTH(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EmployeePeriodDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeePeriodHCTH(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeePeriodHCTH(ByVal _filter As EmployeePeriodDTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EmployeePeriodDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeePeriodHCTH(_filter, 0, Integer.MaxValue, 0, lstOrg, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function CAL_EMPLOYEEHTCH_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CAL_EMPLOYEEHTCH_PERIOD(_org_id, _Period_ID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CAL_EMP_RECOMEND_RESULT(ByVal _org_id As List(Of Decimal), ByVal P_PERIOD As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CAL_EMP_RECOMEND_RESULT(_org_id, P_PERIOD, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function DeleteEmployeeHTCHPeriod(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.DeleteEmployeeHTCHPeriod(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmployeePeriods(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EmployeePeriodDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeePeriods(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeePeriods(ByVal _filter As EmployeePeriodDTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of EmployeePeriodDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeePeriods(_filter, 0, Integer.MaxValue, 0, lstOrg, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function CAL_EMPLOYEE_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.CAL_EMPLOYEE_PERIOD(_org_id, _Period_ID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function DeleteEmployeePeriod(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try

                Return rep.DeleteEmployeePeriod(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

    Public Function APPROVE_EVALUATE_TARGET(ByVal P_ID_REGGROUP As Decimal, ByVal P_APPROVE_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS_ID As Decimal, ByVal P_REASON As String) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.APPROVE_EVALUATE_TARGET(P_ID_REGGROUP, P_APPROVE_ID, P_EMPLOYEE_ID, P_STATUS_ID, P_REASON)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#Region "PE_ORG_MR_RR -> Quản lý dữ liệu RR và MR theo tháng"
    Public Function GetPe_Org_Mr_Rr(ByVal _filter As PE_ORG_MR_RRDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_ORG_MR_RRDTO)
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPe_Org_Mr_Rr(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetATPeriodByYear(ByVal Year As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetATPeriodByYear(Year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetYearATPeriod() As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetYearATPeriod()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetPeriod2(ByVal year As Decimal, ByVal isBlank As Decimal) As DataTable
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetPeriod2(year, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidatePe_Org_Mr_Rr(ByVal obj As PE_ORG_MR_RRDTO) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidatePe_Org_Mr_Rr(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

End Class
