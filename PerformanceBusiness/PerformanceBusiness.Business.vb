Imports Framework.Data
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
        Public Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetEmployeeImport
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeImport(group_obj_id, period_id, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetCriteriaImport
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaImport(group_obj_id)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                                 ByVal periodID As Decimal,
                                                 ByVal group_obj_ID As Decimal,
                                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ImportEmployeeAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.ImportEmployeeAssessment(dtData, periodID, group_obj_ID, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet Implements ServiceContracts.IPerformanceBusiness.GetEmployeeImportAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeImportAssessment(_param, obj, P_PAGE_INDEX, P_PAGE_SIZE, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
        Public Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO) Implements ServiceContracts.IPerformanceBusiness.GetPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPe_Organization(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO Implements ServiceContracts.IPerformanceBusiness.GetPe_OrganizationByID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPe_OrganizationByID(_filter)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPe_Organization(objData, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPe_Organization(objData, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePe_Organization
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePe_Organization(lstID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region


#Region "Assessment"
        Public Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetAssessment(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyAssessment(objAssessment, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.UpdateStatusEmployeeAssessment
            Try
                Using rep As New PerformanceRepository
                    Return rep.UpdateStatusEmployeeAssessment(obj, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO) Implements ServiceContracts.IPerformanceBusiness.GetListEmployeePortal
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetListEmployeePortal(_filter)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetTotalPointRating(ByVal obj As AssessmentDTO) As DataTable Implements ServiceContracts.IPerformanceBusiness.GetTotalPointRating
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetTotalPointRating(obj)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function Delete_PE_KPI_ASSESSMENT_DETAIL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPerformanceBusiness.Delete_PE_KPI_ASSESSMENT_DETAIL
            Try
                Using rep As New PerformanceRepository
                    Return rep.Delete_PE_KPI_ASSESSMENT_DETAIL(lstID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "PORTAL QL PKI cua nhan vien"

        Public Function GetAssessmentDirect(ByVal _empId As System.Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO) _
    Implements ServiceContracts.IPerformanceBusiness.GetAssessmentDirect
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetAssessmentDirect(_empId, _year, _status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_DM_STATUS() As DataTable _
    Implements ServiceContracts.IPerformanceBusiness.GET_DM_STATUS
            Using rep As New PerformanceRepository
                Try
                    Return rep.GET_DM_STATUS()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
   Implements ServiceContracts.IPerformanceBusiness.CHECK_APP
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable _
  Implements ServiceContracts.IPerformanceBusiness.GET_PE_ASSESSMENT_HISTORY
            Using rep As New PerformanceRepository
                Try
                    Return rep.GET_PE_ASSESSMENT_HISTORY(_Id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
   Implements ServiceContracts.IPerformanceBusiness.CHECK_APP_1
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP_1(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable _
  Implements ServiceContracts.IPerformanceBusiness.CHECK_APP_2
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHECK_APP_2(_empId, __PE_PERIOD_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean _
 Implements ServiceContracts.IPerformanceBusiness.PRI_PERFORMACE_APP
            Using rep As New PerformanceRepository
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
                                                 ByVal P_SIGN_ID As Decimal) As Boolean _
Implements ServiceContracts.IPerformanceBusiness.INSERT_PE_ASSESSMENT_HISTORY
            Using rep As New PerformanceRepository
                Try
                    Return rep.INSERT_PE_ASSESSMENT_HISTORY(P_PE_PE_ASSESSMENT_ID, P_RESULT_DIRECT, P_ASS_DATE, P_REMARK_DIRECT, P_CREATED_BY, P_CREATED_LOG, P_EMPLOYEE_ID, P_SIGN_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean _
Implements ServiceContracts.IPerformanceBusiness.PRI_PERFORMACE_PROCESS
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRI_PERFORMACE_PROCESS(P_EMPLOYEE_APP_ID, P_EMPLOYEE_ID, P_PE_PERIOD_ID, P_STATUS_ID, P_NOTES)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusAssessmentDirect(ByVal obj As Decimal,
                                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.UpdateStatusAssessmentDirect
            Using rep As New PerformanceRepository
                Try
                    Return rep.UpdateStatusAssessmentDirect(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetKPIAssessEmp(ByVal _empId As System.Decimal) As List(Of AssessmentDirectDTO) _
Implements ServiceContracts.IPerformanceBusiness.GetKPIAssessEmp
            Using rep As New PerformanceRepository
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
                                Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO) Implements ServiceContracts.IPerformanceBusiness.GetDirectKPIEmployee
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetDirectKPIEmployee(filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Bieu do xep hang nhan vien"
        Public Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessRatingEmployee
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetAssessRatingEmployee(filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO) Implements ServiceContracts.IPerformanceBusiness.GetAssessRatingEmployeeOrg
            Try
                Dim rep As New PerformanceRepository
                Return rep.GetAssessRatingEmployeeOrg(filter, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        Public Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet _
   Implements ServiceContracts.IPerformanceBusiness.PRINT_PE_ASSESS
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRINT_PE_ASSESS(empID, period, obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)) As Boolean _
                Implements ServiceContracts.IPerformanceBusiness.DeleteKpiAssessmentDetail
            Using rep As New PerformanceRepository
                Try

                    Return rep.DeleteKpiAssessmentDetail(lstObj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function UpdateKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean _
                Implements ServiceContracts.IPerformanceBusiness.UpdateKpiAssessment
            Using rep As New PerformanceRepository
                Try

                    Return rep.UpdateKpiAssessment(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function UpdateKpiAssessmentDetail(ByVal lstObj As List(Of PE_KPI_ASSESMENT_DETAIL_DTO),
                                                  Optional ByVal log As UserLog = Nothing) As Boolean _
                Implements ServiceContracts.IPerformanceBusiness.UpdateKpiAssessmentDetail
            Using rep As New PerformanceRepository
                Try

                    Return rep.UpdateKpiAssessmentDetail(lstObj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateDateAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean _
                Implements ServiceContracts.IPerformanceBusiness.ValidateDateAssessment
            Using rep As New PerformanceRepository
                Try

                    Return rep.ValidateDateAssessment(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessmentByID(ByVal id As Decimal) As KPI_ASSESSMENT_DTO _
                                        Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentByID
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentByID(id)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetKpiAssessmentDetailHistory(ByVal lstID As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                         ByVal PageIndex As Integer,
                                                         ByVal PageSize As Integer,
                                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
                                            Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentDetailHistory
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentDetailHistory(lstID, _filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessmentDetailByListCode(ByVal lstCode As List(Of String), ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                                     ByVal PageIndex As Integer,
                                                     ByVal PageSize As Integer,
                                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
                                        Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentDetailByListCode
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentDetailByListCode(lstCode, _filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessmentDetailByGoal(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer, ByVal _param As ParamDTO,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
                                        Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentDetailByGoal
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentDetailByGoal(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessmentDetail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
                                    Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentDetail
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentDetail(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO) _
                                    Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessment
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessment(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetKpiAssessmentResult(ByVal _filter As KPI_ASSESSMENT_RESULT_DTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_RESULT_DTO) _
                                    Implements ServiceContracts.IPerformanceBusiness.GetKpiAssessmentResult
            Using rep As New PerformanceRepository
                Try

                    Dim lst = rep.GetKpiAssessmentResult(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ChangeStatusAssessmentResult(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.ChangeStatusAssessmentResult
            Using rep As New PerformanceRepository
                Try
                    Return rep.ChangeStatusAssessmentResult(lstID, _status, _log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function SaveChangeRatio(ByVal list As List(Of KPI_ASSESSMENT_DTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.SaveChangeRatio
            Using rep As New PerformanceRepository
                Try

                    Return rep.SaveChangeRatio(list, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteKpiAssessment(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.DeleteKpiAssessment
            Using rep As New PerformanceRepository
                Try

                    Return rep.DeleteKpiAssessment(objID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteKpiAssessmentResult(ByVal objID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.DeleteKpiAssessmentResult
            Using rep As New PerformanceRepository
                Try

                    Return rep.DeleteKpiAssessmentResult(objID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeriodByYear(ByVal Year As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetPeriodByYear
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetPeriodByYear(Year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDateByPeriod(ByVal Period As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetDateByPeriod
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetDateByPeriod(Period)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetPeriodByYear2(ByVal Year As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetPeriodByYear2
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetPeriodByYear2(Year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetPeriod2(ByVal year As Decimal, ByVal isBlank As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetPeriod2
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetPeriod2(year, isBlank)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDateByPeriod2(ByVal Period As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetDateByPeriod2
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetDateByPeriod2(Period)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertKpiAssessment(ByVal obj As KPI_ASSESSMENT_DTO) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.InsertKpiAssessment
            Using rep As New PerformanceRepository
                Try

                    Return rep.InsertKpiAssessment(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#Region "Portal Assessment"
        Public Function GetPortalAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetPortalAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetPortalAssessment(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Get_Portal_Target_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.Get_Portal_Target_Detail
            Using rep As New PerformanceRepository
                Try
                    Return rep.Get_Portal_Target_Detail(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_Portal_Approve_Evaluate_Target(ByVal _filter As KPI_ASSESSMENT_DTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.Get_Portal_Approve_Evaluate_Target
            Using rep As New PerformanceRepository
                Try
                    Return rep.Get_Portal_Approve_Evaluate_Target(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPortalApprovedKPIAssessment(ByVal _filter As KPI_ASSESSMENT_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "EFFECT_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of KPI_ASSESSMENT_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetPortalApprovedKPIAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetPortalApprovedKPIAssessment(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_Portal_KPI_Detail(ByVal _filter As PE_KPI_ASSESMENT_DETAIL_DTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer,
                                              Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                              Optional ByVal log As UserLog = Nothing) As List(Of PE_KPI_ASSESMENT_DETAIL_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.Get_Portal_KPI_Detail
            Using rep As New PerformanceRepository
                Try
                    Return rep.Get_Portal_KPI_Detail(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer, Optional ByVal log As UserLog = Nothing) As Int32 _
            Implements ServiceContracts.IPerformanceBusiness.PRI_PROCESS
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32 _
                      Implements ServiceContracts.IPerformanceBusiness.PRI_PROCESS_APP
            Using rep As New PerformanceRepository
                Try
                    Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_KPI_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean _
                      Implements ServiceContracts.IPerformanceBusiness.IMPORT_KPI_ASSESSMENT
            Using rep As New PerformanceRepository
                Try
                    Return rep.IMPORT_KPI_ASSESSMENT(DocXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateSubmit(ByVal _kpi_ID As Decimal, ByVal _type As String) As Boolean _
                      Implements ServiceContracts.IPerformanceBusiness.ValidateSubmit
            Using rep As New PerformanceRepository
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
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetHTCHAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetHTCHAssessment(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHTCHAssessment_Detail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetHTCHAssessment_Detail
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetHTCHAssessment_Detail(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function

        Public Function GetHTCHAssessmentByID(ByVal _id As Decimal) As PE_HTCH_ASSESSMENT_DTO _
            Implements ServiceContracts.IPerformanceBusiness.GetHTCHAssessmentByID
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetHTCHAssessmentByID(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHTCHAssessmentListDetail(ByVal _id As Decimal) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetHTCHAssessmentListDetail
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetHTCHAssessmentListDetail(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SaveHTCHAssessment(ByVal objData As PE_HTCH_ASSESSMENT_DTO, ByVal _log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.SaveHTCHAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.SaveHTCHAssessment(objData, _log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SaveHTCHAssessmentListDetail(ByVal lstObj As List(Of PE_HTCH_ASSESSMENT_DTL_DTO), ByVal _log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.SaveHTCHAssessmentListDetail
            Using rep As New PerformanceRepository
                Try
                    Return rep.SaveHTCHAssessmentListDetail(lstObj, _log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetYearHTCH(ByVal IS_BLANK As Boolean) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetYearHTCH
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetYearHTCH(IS_BLANK)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteHTCHAssessment(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.DeleteHTCHAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.DeleteHTCHAssessment(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeriodHTCHByYear(ByVal Year As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetPeriodHTCHByYear
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetPeriodHTCHByYear(Year)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_HTCH_ASSESSMENT(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CAL_HTCH_ASSESSMENT
            Using rep As New PerformanceRepository
                Try
                    Return rep.CAL_HTCH_ASSESSMENT(_org_id, P_YEAR, P_PERIOD, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_HTCT_ASSESS_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CAL_HTCT_ASSESS_DTL
            Using rep As New PerformanceRepository
                Try
                    Return rep.CAL_HTCT_ASSESS_DTL(p_id, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHANGE_HTCH_ASSESSMENT_DTL(ByVal p_id As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CHANGE_HTCH_ASSESSMENT_DTL
            Using rep As New PerformanceRepository
                Try
                    Return rep.CHANGE_HTCH_ASSESSMENT_DTL(p_id, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAproveHTCHAssessment(ByVal _filter As PE_HTCH_ASSESSMENT_DTO,
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetAproveHTCHAssessment
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetAproveHTCHAssessment(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveHTCHAssessmentDetail(ByVal _filter As PE_HTCH_ASSESSMENT_DTL_DTO,
                                                    ByVal PageIndex As Integer,
                                                    ByVal PageSize As Integer,
                                                    ByRef Total As Integer,
                                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                    Optional ByVal log As UserLog = Nothing) As List(Of PE_HTCH_ASSESSMENT_DTL_DTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetApproveHTCHAssessmentDetail
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetApproveHTCHAssessmentDetail(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_HTCH_ASSESSMENT(ByVal DocXML As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.IMPORT_HTCH_ASSESSMENT
            Using rep As New PerformanceRepository
                Try
                    Return rep.IMPORT_HTCH_ASSESSMENT(DocXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region


#Region "Employee Period"
        Public Function GetEmployeePeriodHCTH(ByVal _filter As EmployeePeriodDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         ByVal lstOrg As List(Of Decimal),
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetEmployeePeriodHCTH
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetEmployeePeriodHCTH(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CAL_EMPLOYEEHTCH_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CAL_EMPLOYEEHTCH_PERIOD
            Using rep As New PerformanceRepository
                Try
                    Return rep.CAL_EMPLOYEEHTCH_PERIOD(_org_id, _Period_ID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_EMP_RECOMEND_RESULT(ByVal _org_id As List(Of Decimal), ByVal P_PERIOD As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CAL_EMP_RECOMEND_RESULT
            Using rep As New PerformanceRepository
                Try
                    Return rep.CAL_EMP_RECOMEND_RESULT(_org_id, P_PERIOD, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployeeHTCHPeriod(ByVal _lstID As List(Of Decimal)) As Boolean _
                      Implements ServiceContracts.IPerformanceBusiness.DeleteEmployeeHTCHPeriod
            Using rep As New PerformanceRepository
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
                                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeePeriodDTO) _
            Implements ServiceContracts.IPerformanceBusiness.GetEmployeePeriods
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetEmployeePeriods(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CAL_EMPLOYEE_PERIOD(ByVal _org_id As List(Of Decimal), ByVal _Period_ID As Decimal, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPerformanceBusiness.CAL_EMPLOYEE_PERIOD
            Using rep As New PerformanceRepository
                Try
                    Return rep.CAL_EMPLOYEE_PERIOD(_org_id, _Period_ID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployeePeriod(ByVal _lstID As List(Of Decimal)) As Boolean _
                      Implements ServiceContracts.IPerformanceBusiness.DeleteEmployeePeriod
            Using rep As New PerformanceRepository
                Try
                    Return rep.DeleteEmployeePeriod(_lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

        Public Function APPROVE_EVALUATE_TARGET(ByVal P_ID_REGGROUP As Decimal, ByVal P_APPROVE_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS_ID As Decimal, ByVal P_REASON As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.APPROVE_EVALUATE_TARGET
            Using rep As New PerformanceRepository
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
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PE_ORG_MR_RRDTO) Implements ServiceContracts.IPerformanceBusiness.GetPe_Org_Mr_Rr
            Using rep As New PerformanceRepository
                Try
                    Return rep.GetPe_Org_Mr_Rr(_filter, PageIndex, PageSize, Total, lstOrg, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetATPeriodByYear(ByVal Year As Decimal) As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetATPeriodByYear
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetATPeriodByYear(Year)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetYearATPeriod() As DataTable _
            Implements ServiceContracts.IPerformanceBusiness.GetYearATPeriod
            Using rep As New PerformanceRepository
                Try

                    Return rep.GetYearATPeriod()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidatePe_Org_Mr_Rr(ByVal obj As PE_ORG_MR_RRDTO) As Boolean _
                Implements ServiceContracts.IPerformanceBusiness.ValidatePe_Org_Mr_Rr
            Using rep As New PerformanceRepository
                Try

                    Return rep.ValidatePe_Org_Mr_Rr(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
