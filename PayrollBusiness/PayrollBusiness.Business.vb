Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Calculate Salary"
        Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_Calculate_Load
            Dim rep As New PayrollRepository
            Return rep.Load_Calculate_Load(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_sum
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_data_sum
            Dim rep As New PayrollRepository
            Return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_temp
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_data_calculate
            Dim rep As New PayrollRepository
            Return rep.Load_data_calculate(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet Implements ServiceContracts.IPayrollBusiness.GetLitsCalculate
            Dim rep As New PayrollRepository
            Return rep.GetLitsCalculate(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaryVisibleCol
            Dim rep As New PayrollRepository
            Return rep.GetListSalaryVisibleCol()
        End Function


#End Region
#Region "Import Bonus"

        Public Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetImportBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.GetImportBonus(Year, obj_sal_id, PeriodId, OrgId, IsDissolve, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Import Salary"

        Public Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetImportSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetImportSalary(obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GET_DATA_SEND_MAIL
            Try
                Dim rep As New PayrollRepository
                Return rep.GET_DATA_SEND_MAIL(obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable Implements ServiceContracts.IPayrollBusiness.GetMappingSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetMappingSalary(obj_sal_id, PeriodId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable Implements ServiceContracts.IPayrollBusiness.GetMappingSalaryImport
            Try
                Dim rep As New PayrollRepository
                Return rep.GetMappingSalaryImport(obj_sal_id, PeriodId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryList() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryList()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryList_TYPE
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryList_TYPE(OBJ_SAL_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImport
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImport(SalaryGroup, Period, dtData, lstColVal, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImportBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImportBonus(Org_Id, Year, SalaryGroup, Period, dtData, lstColVal, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImportSalary_Fund_Mapping
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImportSalary_Fund_Mapping(Year, SalaryGroup, Period, dtData, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "IPORTAL - View phiếu lương"
        Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetPayrollSheetSum
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPayrollSheetSum(PeriodId, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPayrollBusiness.GetPayrollSheetSumSheet
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPayrollSheetSumSheet(PeriodId, EmployeeId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPayrollBusiness.CHECK_OPEN_CLOSE
            Try
                Dim rep As New PayrollRepository
                Return rep.CHECK_OPEN_CLOSE(PeriodId, EmployeeId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                   ByVal orgID As Decimal?,
                                   ByVal isDissolve As Decimal?,
                                   ByVal periodID As Decimal,
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActionSendPayslip
            Using rep As New PayrollRepository
                Try
                    Return rep.ActionSendPayslip(lstEmployee, orgID, isDissolve, periodID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal),
                                   ByVal orgID As Decimal?,
                                   ByVal isDissolve As Decimal?,
                                   ByVal periodID As Decimal,
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActionSendBonusslip
            Using rep As New PayrollRepository
                Try
                    Return rep.ActionSendBonusslip(lstEmployee, orgID, isDissolve, periodID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Delegacy Tax"
        Public Function GetDelegacyTax(ByVal _filter As PA_Delegacy_TaxDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_Delegacy_TaxDTO) Implements ServiceContracts.IPayrollBusiness.GetDelegacyTax
            Try
                Dim rep As New PayrollRepository
                Return rep.GetDelegacyTax(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifiDelegacy(ByVal lstObj As List(Of PA_Delegacy_TaxDTO), ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifiDelegacy
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifiDelegacy(lstObj, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "PA_ADDTAX"
        Public Function GetPA_ADDTAX(ByVal _filter As PA_ADDTAXDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_ADDTAXDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_ADDTAX
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_ADDTAX(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPA_ADDTAX_ByID(ByVal _id As Decimal?) As PA_ADDTAXDTO Implements ServiceContracts.IPayrollBusiness.GetPA_ADDTAX_ByID
            Using rep As New PayrollRepository
                Try

                    Return rep.GetPA_ADDTAX_ByID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_ADDTAX
            Using rep As New PayrollRepository
                Try
                    Return rep.InsertPA_ADDTAX(lstObj, userLog, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByVal userLog As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_ADDTAX
            Using rep As New PayrollRepository
                Try
                    Return rep.ModifyPA_ADDTAX(lstObj, userLog, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_IMPORT_PA_ADDTAX() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_IMPORT_PA_ADDTAX
            Using rep As New PayrollRepository
                Try
                    Return rep.GET_IMPORT_PA_ADDTAX()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_IMPORT_PA_STORE_DTTD() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_IMPORT_PA_STORE_DTTD
            Using rep As New PayrollRepository
                Try
                    Return rep.GET_IMPORT_PA_STORE_DTTD()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IPayrollBusiness.CHECK_EMPLOYEE
            Using rep As New PayrollRepository
                Try

                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SAVE_IMPORT_PA_ADDTAX(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.SAVE_IMPORT_PA_ADDTAX
            Using rep As New PayrollRepository
                Try
                    Return rep.SAVE_IMPORT_PA_ADDTAX(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_CH(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_CH
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_CH(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SAVE_IMPORT_PA_STORE_DTTD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.SAVE_IMPORT_PA_STORE_DTTD
            Using rep As New PayrollRepository
                Try
                    Return rep.SAVE_IMPORT_PA_STORE_DTTD(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidatePA_ADDTAX(ByVal _objData As PA_ADDTAXDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidatePA_ADDTAX
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidatePA_ADDTAX(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "PA Store DTTĐ"
        Public Function GetPA_STORE_DTTD(ByVal _filter As PA_STORE_DTTDDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_DTTDDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_STORE_DTTD
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_STORE_DTTD(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CalculateStoreDTTD(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_OBJ_EMP As Decimal, ByVal P_ENDDATE As Date, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CalculateStoreDTTD
            Try
                Dim rep As New PayrollRepository
                Return rep.CalculateStoreDTTD(P_PERIOD, P_ORG, P_OBJ_EMP, P_ENDDATE, _log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region


#Region "PA_TARGET_STORE"
        Public Function GET_PA_TARGET_STORE(ByVal _filter As PA_TARGET_STOREDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByVal _param As ParamDTO,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                                             Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_STOREDTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_TARGET_STORE
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_TARGET_STORE(_filter, PageIndex, PageSize, _param, Total, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_TARGET_STORE
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_TARGET_STORE(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_TARGET_STORE
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_TARGET_STORE(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_TARGET_STORE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_TARGET_STORE
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_TARGET_STORE(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_TARGET_STORE
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_TARGET_STORE(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Benefit Seniority"
        Public Function GetLstBenefitSeniority(ByVal _filter As PaBenefitSeniorityDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PaBenefitSeniorityDTO) Implements ServiceContracts.IPayrollBusiness.GetLstBenefitSeniority
            Try
                Dim rep As New PayrollRepository
                Return rep.GetLstBenefitSeniority(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Calculate_Benefit(ByVal _period_ID As Decimal, ByVal _obj_Emp_ID As Decimal, ByVal _OrgID As Decimal, ByVal _IsDissolve As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_Benefit
            Try
                Dim rep As New PayrollRepository
                Return rep.Calculate_Benefit(_period_ID, _obj_Emp_ID, _OrgID, _IsDissolve, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Accounting Adjusting"
        Public Function GetAccountingAdjusting(ByVal _filter As PA_Accounting_AdjustingDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_AdjustingDTO) Implements ServiceContracts.IPayrollBusiness.GetAccountingAdjusting
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAccountingAdjusting(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyAccountingAdjust
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyAccountingAdjust(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertAccountingAdjust
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertAccountingAdjust(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAccountingAdjust(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAccountingAdjust
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAccountingAdjust(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateAccountingAdjust
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateAccountingAdjust(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Vehicle Norm"
        Public Function GetVehicleNorm(ByVal _filter As PA_Vehicle_NormDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Vehicle_NormDTO) Implements ServiceContracts.IPayrollBusiness.GetVehicleNorm
            Try
                Dim rep As New PayrollRepository
                Return rep.GetVehicleNorm(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyVehicleNorm
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyVehicleNorm(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertVehicleNorm
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertVehicleNorm(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteVehicleNorm(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteVehicleNorm
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteVehicleNorm(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateVehicleNorm
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateVehicleNorm(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_VEHICLE_NORM_IMPORT() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_VEHICLE_NORM_IMPORT
            Dim rep As New PayrollRepository
            Return rep.GET_VEHICLE_NORM_IMPORT()
        End Function
        'Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal P_DOCXML As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_DATA_VEHICLE_NORM_IMPORT
        '    Dim rep As New PayrollRepository
        '    Return rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(P_DOCXML)
        'End Function
        Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal dtData As DataTable, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_DATA_VEHICLE_NORM_IMPORT
            Try
                Dim rep As New PayrollRepository
                Return rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(dtData, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "PA_TARGET_DTTD_LABEL"
        Public Function GetPA_TARGET_DTTD_LABEL(ByVal _filter As PA_TARGET_DTTD_LABELDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of PA_TARGET_DTTD_LABELDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_TARGET_DTTD_LABEL
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_TARGET_DTTD_LABEL(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_IMPORT_PA_TARGET_DTTD_LABEL() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_IMPORT_PA_TARGET_DTTD_LABEL
            Using rep As New PayrollRepository
                Try
                    Return rep.GET_IMPORT_PA_TARGET_DTTD_LABEL()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function IMPORT_PA_TARGET_DTTD_LABEL(ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_TARGET_DTTD_LABEL
            Try
                Dim rep As New PayrollRepository
                Return rep.IMPORT_PA_TARGET_DTTD_LABEL(dtData, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Accounting Time"
        Public Function GetAccountingTime(ByVal _filter As PA_Accounting_TimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_TimeDTO) Implements ServiceContracts.IPayrollBusiness.GetAccountingTime
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAccountingTime(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAccountingTime(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAccountingTime
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAccountingTime(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Accounting Overtime"
        Public Function GetAccountingOvertime(ByVal _filter As PA_Accounting_OvertimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_Accounting_OvertimeDTO) Implements ServiceContracts.IPayrollBusiness.GetAccountingOvertime
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAccountingOvertime(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAccountingOvertime(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAccountingOvertime
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAccountingOvertime(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusAccountingOvertime(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusAccountingOvertime
            Try
                Dim rep As New PayrollRepository
                Return rep.ChangeStatusAccountingOvertime(_lstID, _status, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "DTTD_DTPB"
        Public Function GetDTTD_DTPB(ByVal _filter As PA_DTTD_DTPB_EMPDTO, ByVal PageIndex As Integer,
                                               ByVal PageSize As Integer,
                                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_DTPB_EMPDTO) Implements ServiceContracts.IPayrollBusiness.GetDTTD_DTPB
            Try
                Dim rep As New PayrollRepository
                Return rep.GetDTTD_DTPB(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteDTTD_DTPB(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteDTTD_DTPB
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteDTTD_DTPB(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "DTTD_ECD"
        Public Function GetDTTD_ECD(ByVal _filter As PA_DTTD_ECDDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_DTTD_ECDDTO) Implements ServiceContracts.IPayrollBusiness.GetDTTD_ECD
            Try
                Dim rep As New PayrollRepository
                Return rep.GetDTTD_ECD(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "PayrollSheet lock"
        Public Function GetPayroolSheetLock(ByVal _filter As PA_PayrollSheetLockDTO, ByVal lstOrg As List(Of Decimal),
                                            ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_PayrollSheetLockDTO) Implements ServiceContracts.IPayrollBusiness.GetPayroolSheetLock
            Try
                Using rep As New PayrollRepository
                    Return rep.GetPayroolSheetLock(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusPASheetLock(ByVal lstOrg As List(Of Decimal), ByVal lstEmp As List(Of Decimal), ByVal _status As Decimal,
                                                ByVal _period_id As Decimal, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusPASheetLock
            Try
                Using rep As New PayrollRepository
                    Return rep.ChangeStatusPASheetLock(lstOrg, lstEmp, _status, _period_id, _log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "LDT_VP"
        Public Function GetLDT_VP(ByVal _filter As PA_LDT_VPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_LDT_VPDTO) Implements ServiceContracts.IPayrollBusiness.GetLDT_VP
            Try
                Dim rep As New PayrollRepository
                Return rep.GetLDT_VP(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "PA_MA_SCP_QLCH"
        Public Function GetPA_MA_SCP_QLCH(ByVal _filter As PA_MA_SCP_QLCHDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_MA_SCP_QLCHDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_MA_SCP_QLCH
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_MA_SCP_QLCH(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Accounting Subsidize"
        Public Function GetAccountingSubsidize(ByVal _filter As PA_AccountingSubsidizeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_AccountingSubsidizeDTO) Implements ServiceContracts.IPayrollBusiness.GetAccountingSubsidize
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAccountingSubsidize(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAccountingSubsidize(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAccountingSubsidize
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAccountingSubsidize(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusAccountingSubsidize(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusAccountingSubsidize
            Try
                Dim rep As New PayrollRepository
                Return rep.ChangeStatusAccountingSubsidize(_lstID, _status, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "PA_STORE_SUBSIDIZE"
        Public Function GetPA_STORE_SUBSIDIZE(ByVal _filter As PA_STORE_SUBSIDIZEDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_STORE_SUBSIDIZEDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_STORE_SUBSIDIZE
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_STORE_SUBSIDIZE(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_STORE_SUBSIDIZE
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyPA_STORE_SUBSIDIZE(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_STORE_SUBSIDIZE
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPA_STORE_SUBSIDIZE(_objData, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePA_STORE_SUBSIDIZE(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePA_STORE_SUBSIDIZE
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePA_STORE_SUBSIDIZE(_lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidatePA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidatePA_STORE_SUBSIDIZE
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidatePA_STORE_SUBSIDIZE(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_PA_STORE_SUBSIDIZE_IMPORT() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_PA_STORE_SUBSIDIZE_IMPORT
            Dim rep As New PayrollRepository
            Return rep.GET_PA_STORE_SUBSIDIZE_IMPORT()
        End Function
        Public Function Get_Brand_Name(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO Implements ServiceContracts.IPayrollBusiness.Get_Brand_Name
            Try
                Dim rep As New PayrollRepository
                Return rep.Get_Brand_Name(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Get_Rate(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO Implements ServiceContracts.IPayrollBusiness.Get_Rate
            Try
                Dim rep As New PayrollRepository
                Return rep.Get_Rate(_objData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region


#Region "PA_Salary_Detention"
        Public Function GetPeriod(ByVal isBlank As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetPeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPeriod(isBlank)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryDetentionByID(ByVal _filter As PA_Salary_DetentionDTO) As PA_Salary_DetentionDTO Implements ServiceContracts.IPayrollBusiness.GetSalaryDetentionByID
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryDetentionByID(_filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_PA_SALARY_DETENTION(ByVal _filter As PA_Salary_DetentionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal log As UserLog = Nothing,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Salary_DetentionDTO) Implements ServiceContracts.IPayrollBusiness.GET_PA_SALARY_DETENTION
            Try
                Dim rep As New PayrollRepository
                Return rep.GET_PA_SALARY_DETENTION(_filter, PageIndex, PageSize, Total, _param, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function INSERT_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SALARY_DETENTION
            Try
                Dim rep As New PayrollRepository
                Return rep.INSERT_PA_SALARY_DETENTION(lstObj, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function MODIFY_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO), ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SALARY_DETENTION
            Try
                Dim rep As New PayrollRepository
                Return rep.MODIFY_PA_SALARY_DETENTION(lstObj, userLog)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DELETE_PA_SALARY_DETENTION(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SALARY_DETENTION
            Try
                Dim rep As New PayrollRepository
                Return rep.DELETE_PA_SALARY_DETENTION(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetHSTDTImport() As DataSet Implements ServiceContracts.IPayrollBusiness.GetHSTDTImport
            Try
                Using rep As New PayrollRepository
                    Return rep.GetHSTDTImport()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "PA_MANAGE_DTTD_DAILY"
        Public Function GetManageDTTDDaily(ByVal _filter As PA_ManageDTTDDailyDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of PA_ManageDTTDDailyDTO) Implements ServiceContracts.IPayrollBusiness.GetManageDTTDDaily
            Try
                Dim rep As New PayrollRepository
                Return rep.GetManageDTTDDaily(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_IMPORT_DTTD_DAILY() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_IMPORT_DTTD_DAILY
            Try
                Dim rep As New PayrollRepository
                Return rep.GET_IMPORT_DTTD_DAILY()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_DTTD_DAILY(ByVal DATA_IN As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_DTTD_DAILY
            Try
                Dim rep As New PayrollRepository
                Return rep.IMPORT_DTTD_DAILY(DATA_IN, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Award 13 month"
        Public Function GetAward13Month(ByVal _filter As PA_Award_13MonthDTO, ByVal lstOrg As List(Of Decimal),
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer,
                                   Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of PA_Award_13MonthDTO) Implements ServiceContracts.IPayrollBusiness.GetAward13Month
            Try
                Using rep As New PayrollRepository
                    Return rep.GetAward13Month(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusAward13Month(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusAward13Month
            Try
                Using rep As New PayrollRepository
                    Return rep.ChangeStatusAward13Month(lstID, _status, _log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAward13Month(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAward13Month
            Try
                Using rep As New PayrollRepository
                    Return rep.DeleteAward13Month(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateAward13Month(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.UpdateAward13Month
            Try
                Using rep As New PayrollRepository
                    Return rep.UpdateAward13Month(lstObj, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CAL_AWARD_13MONTH(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CAL_AWARD_13MONTH
            Try
                Using rep As New PayrollRepository
                    Return rep.CAL_AWARD_13MONTH(_org_id, P_YEAR, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function RE_CAL_AWARD_13MONTH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.RE_CAL_AWARD_13MONTH
            Try
                Using rep As New PayrollRepository
                    Return rep.RE_CAL_AWARD_13MONTH(lstID, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateAward13MonthPeriod(ByVal lstObj As List(Of PA_Award_13MonthDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.UpdateAward13MonthPeriod
            Try
                Using rep As New PayrollRepository
                    Return rep.UpdateAward13MonthPeriod(lstObj, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Salary PIT"
        Public Function GetListSalPITCode(ByVal _filter As PA_Salary_PITCodeDTO, ByVal lstOrg As List(Of Decimal),
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of PA_Salary_PITCodeDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalPITCode
            Try
                Using rep As New PayrollRepository
                    Return rep.GetListSalPITCode(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusSalPITCode(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusSalPITCode
            Try
                Using rep As New PayrollRepository
                    Return rep.ChangeStatusSalPITCode(lstID, _status, _log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalPITCode(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalPITCode
            Try
                Using rep As New PayrollRepository
                    Return rep.DeleteSalPITCode(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CAL_SAL_PITCODE(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CAL_SAL_PITCODE
            Try
                Using rep As New PayrollRepository
                    Return rep.CAL_SAL_PITCODE(_org_id, P_YEAR, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Tax Income Year"
        Public Function GetListTaxIncomeYear(ByVal _filter As PA_TAXINCOME_YEARDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_TAXINCOME_YEARDTO) Implements ServiceContracts.IPayrollBusiness.GetListTaxIncomeYear
            Try
                Using rep As New PayrollRepository
                    Return rep.GetListTaxIncomeYear(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangeStatusTaxIncomeYear(ByVal lstID As List(Of Decimal), ByVal _status As Decimal, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangeStatusTaxIncomeYear
            Try
                Using rep As New PayrollRepository
                    Return rep.ChangeStatusTaxIncomeYear(lstID, _status, _log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTaxIncomeYear(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTaxIncomeYear
            Try
                Using rep As New PayrollRepository
                    Return rep.DeleteTaxIncomeYear(lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateTaxIncomeYearPeriod(ByVal lstID As List(Of Decimal), ByVal _period As Decimal, ByVal _log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.UpdateTaxIncomeYearPeriod
            Try
                Using rep As New PayrollRepository
                    Return rep.UpdateTaxIncomeYearPeriod(lstID, _period, _log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CAL_TAX_INCOME_YEAR(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CAL_TAX_INCOME_YEAR
            Try
                Using rep As New PayrollRepository
                    Return rep.CAL_TAX_INCOME_YEAR(_org_id, P_YEAR, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Document PIT"
        Public Function GetDocumentPITs(ByVal _filter As PA_DOCUMENT_PITDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of PA_DOCUMENT_PITDTO) Implements ServiceContracts.IPayrollBusiness.GetDocumentPITs
            Try
                Using rep As New PayrollRepository
                    Return rep.GetDocumentPITs(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_EMPLOYEE_PIT_INFO(ByVal P_EMP_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_ID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GET_EMPLOYEE_PIT_INFO
            Try
                Using rep As New PayrollRepository
                    Return rep.GET_EMPLOYEE_PIT_INFO(P_EMP_ID, P_YEAR, P_ID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertDocumentPIT
            Try
                Using rep As New PayrollRepository
                    Return rep.InsertDocumentPIT(_objData, userLog)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyDocumentPIT
            Try
                Using rep As New PayrollRepository
                    Return rep.ModifyDocumentPIT(_objData, userLog)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteDocumentPIT(ByVal _lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteDocumentPIT
            Try
                Using rep As New PayrollRepository
                    Return rep.DeleteDocumentPIT(_lstID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateDocumentPIT
            Try
                Using rep As New PayrollRepository
                    Return rep.ValidateDocumentPIT(_objData)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ChangePITPrintStatus(ByVal lstID As List(Of Decimal), ByVal _type As String, ByVal pit_type As String, ByVal userLog As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ChangePITPrintStatus
            Try
                Using rep As New PayrollRepository
                    Return rep.ChangePITPrintStatus(lstID, _type, pit_type, userLog)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Payroll Advance"
        Public Function CAL_PAYROLL_ADVANCE(ByVal _period_ID As Decimal,
                                        ByVal _start_date As Date,
                                        ByVal _end_date As Date,
                                        ByVal _end_date_period As Date,
                                        ByVal _OrgID As Decimal,
                                        ByVal _IsDissolve As Decimal,
                                        ByVal _Sal As Decimal,
                                        ByVal _Nosal As Decimal,
                                        ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CAL_PAYROLL_ADVANCE
            Try
                Using rep As New PayrollRepository
                    Return rep.CAL_PAYROLL_ADVANCE(_period_ID, _start_date, _end_date, _end_date_period, _OrgID, _IsDissolve, _Sal, _Nosal, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPayrollAdvance(ByVal _filter As PayrollAdvanceDTO, ByVal _param As ParamDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "LOCK_NAME desc",
                                     Optional ByVal log As UserLog = Nothing) As List(Of PayrollAdvanceDTO) Implements ServiceContracts.IPayrollBusiness.GetPayrollAdvance
            Try
                Using rep As New PayrollRepository
                    Return rep.GetPayrollAdvance(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePayrollAdvance
            Try
                Using rep As New PayrollRepository
                    Return rep.DeletePayrollAdvance(_lstID, peroid)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal _param As ParamDTO, ByVal peroid As Decimal, ByVal status As Decimal, ByVal Log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePayrollAdvance
            Try
                Using rep As New PayrollRepository
                    Return rep.ActivePayrollAdvance(_lstID, _param, peroid, status, Log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyPayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal, ByVal salAdvance As Decimal, ByVal Log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPayrollAdvance
            Try
                Using rep As New PayrollRepository
                    Return rep.ModifyPayrollAdvance(_lstID, peroid, salAdvance, Log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace

