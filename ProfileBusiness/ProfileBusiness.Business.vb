Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
        Function Calculator_Salary(ByVal data_in As String) As DataTable Implements ServiceContracts.IProfileBusiness.Calculator_Salary
            Try
                Dim rep As New ProfileRepository
                Return rep.Calculator_Salary(data_in)
            Catch ex As Exception
                Throw
            End Try
        End Function
#Region "EmployeeCriteriaRecord"
        Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO) _
              Implements ServiceContracts.IProfileBusiness.EmployeeCriteriaRecord
            Try
                Dim rep As New ProfileRepository
                Return rep.EmployeeCriteriaRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw
            End Try
        End Function
#End Region
        Public Function GetBCCCImport() As DataSet Implements ServiceContracts.IProfileBusiness.GetBCCCImport
            Try
                Using rep As New ProfileRepository
                    Return rep.GetBCCCImport()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetQTCTImport() As DataSet Implements ServiceContracts.IProfileBusiness.GetQTCTImport
            Try
                Using rep As New ProfileRepository
                    Return rep.GetQTCTImport()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetHopdongImport() As DataSet Implements ServiceContracts.IProfileBusiness.GetHopdongImport
            Try
                Using rep As New ProfileRepository
                    Return rep.GetHopdongImport()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHoSoLuongImport() As DataSet Implements ServiceContracts.IProfileBusiness.GetHoSoLuongImport
            Try
                Using rep As New ProfileRepository
                    Return rep.GetHoSoLuongImport()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#Region "Quản lý công nợ"

        Public Function GetDebtMng(ByVal _filter As DebtDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DebtDTO) Implements ServiceContracts.IProfileBusiness.GetDebtMng
            Try
                Using rep As New ProfileRepository
                    Return rep.GetDebtMng(_filter, PageIndex, PageSize, Total, log, Sorts)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetCertificates(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal _param As ParamDTO,
                                    ByRef PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO) Implements ServiceContracts.IProfileBusiness.GetCertificates
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCertificates(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCertificateById(ByVal _id As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTO Implements ServiceContracts.IProfileBusiness.GetCertificateById
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCertificateById(_id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "CTNN"

        Public Function GetAbroads(ByVal _filter As HUAbroadDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUAbroadDTO) Implements ServiceContracts.IProfileBusiness.GetAbroads
            Try
                Using rep As New ProfileRepository
                    Return rep.GetAbroads(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertAbroad
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertAbroad(objAbroad, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyAbroad(ByVal objAbroad As HUAbroadDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyAbroad
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyAbroad(objAbroad, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateAbroad
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateAbroad(objAbroad)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAbroad(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteAbroad
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteAbroad(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_ABROAD_DATA_IMPORT() As DataSet Implements ServiceContracts.IProfileBusiness.GET_ABROAD_DATA_IMPORT
            Try
                Using rep As New ProfileRepository
                    Return rep.GET_ABROAD_DATA_IMPORT()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_ABROAD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IProfileBusiness.IMPORT_ABROAD
            Try
                Using rep As New ProfileRepository
                    Return rep.IMPORT_ABROAD(P_DOCXML, P_USER)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "bhld"
        Public Function getListCol() As List(Of BHLDDTO) Implements ServiceContracts.IProfileBusiness.getListCol
            Try
                Using rep As New ProfileRepository
                    Return rep.getListCol()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function countItemPortal(ByVal emp_id As Decimal, ByVal type As String, ByVal tag As String) As Decimal Implements ServiceContracts.IProfileBusiness.countItemPortal
            Try
                Using rep As New ProfileRepository
                    Return rep.countItemPortal(emp_id, type, tag)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetBHLD1(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IProfileBusiness.GetBHLD1
            Try
                Using rep As New ProfileRepository
                    Return rep.GetBHLD1(year, empcode, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetBHLD_Register(ByVal year As Integer, ByVal empcode As String, ByVal statusId As Decimal, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IProfileBusiness.GetBHLD_Register
            Try
                Using rep As New ProfileRepository
                    Return rep.GetBHLD_Register(year, empcode, statusId, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Excel_DK_PD(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO, ByVal type As String,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As Byte() Implements ServiceContracts.IProfileBusiness.Excel_DK_PD
            Try
                Using rep As New ProfileRepository
                    Return rep.Excel_DK_PD(year, empcode, statusId, PageIndex, PageSize, Total, _param, type, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetBHLD_Approve(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IProfileBusiness.GetBHLD_Approve
            Try
                Using rep As New ProfileRepository
                    Return rep.GetBHLD_Approve(year, empcode, statusId, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetBHLD(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.IProfileBusiness.GetBHLD
            Try
                Using rep As New ProfileRepository
                    Return rep.GetBHLD(year, empcode, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function saveBdldPortal(ByVal year As Decimal, ByVal dt As String, ByVal log As UserLog, ByVal is_send As Decimal) As Decimal Implements ServiceContracts.IProfileBusiness.saveBdldPortal
            Try
                Using rep As New ProfileRepository
                    Return rep.saveBdldPortal(year, dt, log, is_send)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function excel_BHLD_portal(ByVal year As Decimal, ByVal dts As String, ByVal log As UserLog, ByVal status As String) As Byte() Implements ServiceContracts.IProfileBusiness.excel_BHLD_portal
            Try
                Using rep As New ProfileRepository
                    Return rep.excel_BHLD_portal(year, dts, log, status)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function saveBHLD(ByVal year As Decimal, ByVal lst_col As List(Of String), ByVal dt As DataTable, ByVal is_import As Boolean, ByVal log As UserLog) As Decimal Implements ServiceContracts.IProfileBusiness.saveBHLD
            Try
                Using rep As New ProfileRepository
                    Return rep.saveBHLD(year, lst_col, dt, is_import, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function getOrgName(ByVal id As Decimal) As String Implements ServiceContracts.IProfileBusiness.getOrgName
            Try
                Using rep As New ProfileRepository
                    Return rep.getOrgName(id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CalculateBHLD(ByVal year As Integer, ByVal _param As ParamDTO, Optional ByVal log As UserLog = Nothing) As Boolean Implements ServiceContracts.IProfileBusiness.CalculateBHLD
            Try
                Using rep As New ProfileRepository
                    Return rep.CalculateBHLD(year, _param, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Travel"

        Public Function GetTravels(ByVal _filter As HUTravelDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUTravelDTO) Implements ServiceContracts.IProfileBusiness.GetTravels
            Try
                Using rep As New ProfileRepository
                    Return rep.GetTravels(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertTravel
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertTravel(objTravel, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyTravel(ByVal objTravel As HUTravelDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyTravel
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyTravel(objTravel, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateTravel(ByVal objTravel As HUTravelDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateTravel
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateTravel(objTravel)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTravel(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteTravel
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteTravel(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_TRAVEL_DATA_IMPORT() As DataSet Implements ServiceContracts.IProfileBusiness.GET_TRAVEL_DATA_IMPORT
            Try
                Using rep As New ProfileRepository
                    Return rep.GET_TRAVEL_DATA_IMPORT()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_TRAVEL(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IProfileBusiness.IMPORT_TRAVEL
            Try
                Using rep As New ProfileRepository
                    Return rep.IMPORT_TRAVEL(P_DOCXML, P_USER)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "InfoConfirm"

        Public Function GetInfoConfirms(ByVal _filter As HUInfoConfirmDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of HUInfoConfirmDTO) Implements ServiceContracts.IProfileBusiness.GetInfoConfirms
            Try
                Using rep As New ProfileRepository
                    Return rep.GetInfoConfirms(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertInfoConfirm
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertInfoConfirm(objInfoConfirm, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyInfoConfirm
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyInfoConfirm(objInfoConfirm, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteInfoConfirm(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteInfoConfirm
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteInfoConfirm(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetInfoConfirmPrintData(ByVal _id As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetInfoConfirmPrintData
            Try
                Using rep As New ProfileRepository
                    Return rep.GetInfoConfirmPrintData(_id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CB planning"

        Public Function GetCBPlannings(ByVal _filter As CBPlanningDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningDTO) Implements ServiceContracts.IProfileBusiness.GetCBPlannings
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCBPlannings(_filter, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCBPlanning(ByVal _id As Decimal) As CBPlanningDTO Implements ServiceContracts.IProfileBusiness.GetCBPlanning
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCBPlanning(_id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCBPlanning
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertCBPlanning(objCBPlanning, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyCBPlanning(ByVal objCBPlanning As CBPlanningDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCBPlanning
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyCBPlanning(objCBPlanning, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCBPlanning(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteCBPlanning
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteCBPlanning(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CopyCBPlanning(ByVal _id As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.CopyCBPlanning
            Try
                Using rep As New ProfileRepository
                    Return rep.CopyCBPlanning(_id, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCBPlanningsHistory(ByVal _filter As CBPlanningEmpHisDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBPlanningEmpHisDTO) Implements ServiceContracts.IProfileBusiness.GetCBPlanningsHistory
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCBPlanningsHistory(_filter, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Commitee"

        Public Function GetCommitees(ByVal _filter As CommiteeDTO,
                                     ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                     ByVal _param As ParamDTO,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO) Implements ServiceContracts.IProfileBusiness.GetCommitees
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCommitees(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCommitee(ByVal _id As Decimal) As CommiteeDTO Implements ServiceContracts.IProfileBusiness.GetCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCommitee(_id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertCommitee(objCommitee, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyCommitee(ByVal objCommitee As CommiteeDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyCommitee(objCommitee, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCommitee(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteCommitee(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CopyCommitee(ByVal _id As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.CopyCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.CopyCommitee(_id, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCommiteesHistory(ByVal _filter As CommiteeEmpDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CommiteeEmpDTO) Implements ServiceContracts.IProfileBusiness.GetCommiteesHistory
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCommiteesHistory(_filter, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Portal_GetCommitee(ByVal _filter As CommiteeDTO,
                                           ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of CommiteeDTO) Implements ServiceContracts.IProfileBusiness.Portal_GetCommitee
            Try
                Using rep As New ProfileRepository
                    Return rep.Portal_GetCommitee(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetCommiteeProcess(ByVal Username As String, ByVal empID As Decimal) As List(Of CommiteeDTO) Implements ServiceContracts.IProfileBusiness.GetCommiteeProcess
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCommiteeProcess(Username, empID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "CB Assessment"

        Public Function GetCBAssessments(ByVal _filter As CBAssessmentDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of CBAssessmentDTO) Implements ServiceContracts.IProfileBusiness.GetCBAssessments
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCBAssessments(_filter, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCBAssessment(ByVal _id As Decimal) As CBAssessmentDTO Implements ServiceContracts.IProfileBusiness.GetCBAssessment
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCBAssessment(_id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateCBAssessment(ByVal objCBAssessment As CBAssessmentDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateCBAssessment
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateCBAssessment(objCBAssessment)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCBAssessment
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertCBAssessment(objCBAssessment, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyCBAssessment(ByVal objCBAssessment As CBAssessmentDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCBAssessment
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyCBAssessment(objCBAssessment, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCBAssessment(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteCBAssessment
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteCBAssessment(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Emp NPT"

        Public Function GetEmployeeNPTs(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE asc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO) Implements ServiceContracts.IProfileBusiness.GetEmployeeNPTs
            Try
                Using rep As New ProfileRepository
                    Return rep.GetEmployeeNPTs(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_EMPPLOYEE_NPT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IProfileBusiness.IMPORT_EMPPLOYEE_NPT
            Try
                Using rep As New ProfileRepository
                    Return rep.IMPORT_EMPPLOYEE_NPT(P_DOCXML, P_USER)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Stocks"
        Public Function GetStocks(ByVal _filter As StocksDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc",
                              Optional ByVal log As UserLog = Nothing) As List(Of StocksDTO) Implements ServiceContracts.IProfileBusiness.GetStocks
            Try
                Using rep As New ProfileRepository
                    Return rep.GetStocks(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetStocksByID(ByVal _filter As StocksDTO) As StocksDTO Implements ServiceContracts.IProfileBusiness.GetStocksByID
            Try
                Using rep As New ProfileRepository
                    Return rep.GetStocksByID(_filter)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal) Implements ServiceContracts.IProfileBusiness.InsertStocks
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertStocks(obj, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyStocks(ByVal obj As StocksDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyStocks
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyStocks(obj, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateStocks(ByVal obj As StocksDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStocks
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateStocks(obj)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateStocksGenerate(ByVal obj As StocksDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStocksGenerate
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateStocksGenerate(obj)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteStocks(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteStocks
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteStocks(lstID, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Stocks Transaction"
        Public Function GetStocksTransaction(ByVal _filter As StocksTransactionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of StocksTransactionDTO) Implements ServiceContracts.IProfileBusiness.GetStocksTransaction
            Try
                Using rep As New ProfileRepository
                    Return rep.GetStocksTransaction(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertStocksTransaction
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertStocksTransaction(obj, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyStocksTransaction(ByVal obj As StocksTransactionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyStocksTransaction
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyStocksTransaction(obj, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateStocksTransactionBefore(ByVal obj As StocksTransactionDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStocksTransactionBefore
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateStocksTransactionBefore(obj)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateStocksTransactionStatus(ByVal obj As StocksTransactionDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStocksTransactionStatus
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateStocksTransactionStatus(obj)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateStocksTransaction(ByVal obj As StocksTransactionDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateStocksTransaction
            Try
                Using rep As New ProfileRepository
                    Return rep.ValidateStocksTransaction(obj)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteStocksTransaction(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteStocksTransaction
            Try
                Using rep As New ProfileRepository
                    Return rep.DeleteStocksTransaction(lstID, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckHROrgPermission(ByVal username As String, ByVal empID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.CheckHROrgPermission
            Try
                Using rep As New ProfileRepository
                    Return rep.CheckHROrgPermission(username, empID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace