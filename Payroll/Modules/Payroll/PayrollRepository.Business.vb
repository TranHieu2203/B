Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "Calculate Salary"
    Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_Calculate_Load(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_data_calculate(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetLitsCalculate(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalaryVisibleCol()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Import Salary"

    Public Function GetImportSalaryBonus(ByVal Year As Integer, ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetImportBonus(Year, Obj_sal_id, PeriodId, OrgId, IsDissolve, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Import Salary"

    Public Function GetImportSalary(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetImportSalary(Obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_DATA_SEND_MAIL(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_DATA_SEND_MAIL(Obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetMappingSalary(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMappingSalary(Obj_sal_id, PeriodId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetMappingSalaryImport(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMappingSalaryImport(Obj_sal_id, PeriodId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSalaryList() As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryList_TYPE(OBJ_SAL_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImport(SalaryGroup, Period, dtData, lstColVal, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImportBonus(Org_Id, Year, SalaryGroup, Period, dtData, lstColVal, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImportSalary_Fund_Mapping(Year, SalaryGroup, Period, dtData, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "IPORTAL - View phiếu lương"
    Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollSheetSum(PeriodId, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollSheetSumSheet(PeriodId, EmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CHECK_OPEN_CLOSE(PeriodId, EmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Benefit Seniority"
    Public Function GetDelegacyTax(ByVal _filter As PA_Delegacy_TaxDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Delegacy_TaxDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDelegacyTax(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDelegacyTax(ByVal _filter As PA_Delegacy_TaxDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Delegacy_TaxDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDelegacyTax(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifiDelegacy(ByVal lstObj As List(Of PA_Delegacy_TaxDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifiDelegacy(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Accounting Adjusting"
    Public Function GetAccountingAdjusting(ByVal _filter As PA_Accounting_AdjustingDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_AdjustingDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingAdjusting(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAccountingAdjusting(ByVal _filter As PA_Accounting_AdjustingDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_AdjustingDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingAdjusting(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyAccountingAdjust(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertAccountingAdjust(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAccountingAdjust(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAccountingAdjust(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateAccountingAdjust(ByVal _objData As PA_Accounting_AdjustingDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateAccountingAdjust(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Vehicle Norm"
    Public Function GetVehicleNorm(ByVal _filter As PA_Vehicle_NormDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Vehicle_NormDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetVehicleNorm(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetVehicleNorm(ByVal _filter As PA_Vehicle_NormDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Vehicle_NormDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetVehicleNorm(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyVehicleNorm(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertVehicleNorm(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteVehicleNorm(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteVehicleNorm(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateVehicleNorm(ByVal _objData As PA_Vehicle_NormDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateVehicleNorm(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_VEHICLE_NORM_IMPORT() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_VEHICLE_NORM_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    'Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal P_DOCXML As String) As Boolean
    '    Using rep As New PayrollBusinessClient
    '        Try
    '            Return rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(P_DOCXML)
    '        Catch ex As Exception
    '            rep.Abort()
    '            Throw ex
    '        End Try
    '    End Using
    'End Function
    Public Function IMPORT_DATA_VEHICLE_NORM_IMPORT(ByVal dtData As DataTable) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(dtData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Accounting Time"
    Public Function GetAccountingTime(ByVal _filter As PA_Accounting_TimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_TimeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingTime(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAccountingTime(ByVal _filter As PA_Accounting_TimeDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_TimeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingTime(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAccountingTime(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAccountingTime(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PA_ADDTAX"
    Public Function IMPORT_CH(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_CH(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPA_ADDTAX(ByVal _filter As PA_ADDTAXDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_ADDTAXDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_ADDTAX(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPA_ADDTAX(ByVal _filter As PA_ADDTAXDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_ADDTAXDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_ADDTAX(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function GetPA_ADDTAX_ByID(ByVal _id As Decimal?) As PA_ADDTAXDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_ADDTAX_ByID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_ADDTAX(lstObj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyPA_ADDTAX(ByVal lstObj As PA_ADDTAXDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_ADDTAX(lstObj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GET_IMPORT_PA_ADDTAX() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_IMPORT_PA_ADDTAX()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_IMPORT_PA_STORE_DTTD() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_IMPORT_PA_STORE_DTTD()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function SAVE_IMPORT_PA_ADDTAX(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SAVE_IMPORT_PA_ADDTAX(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SAVE_IMPORT_PA_STORE_DTTD(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SAVE_IMPORT_PA_STORE_DTTD(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidatePA_ADDTAX(ByVal _objData As PA_ADDTAXDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidatePA_ADDTAX(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PA Store DTTĐ"
    Public Function GetPA_STORE_DTTD(ByVal _filter As PA_STORE_DTTDDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_STORE_DTTDDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_STORE_DTTD(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPA_STORE_DTTD(ByVal _filter As PA_STORE_DTTDDTO, ByVal _param As ParamDTO,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_STORE_DTTDDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_STORE_DTTD(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CalculateStoreDTTD(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_OBJ_EMP As Decimal, ByVal P_ENDDATE As Date) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CalculateStoreDTTD(P_PERIOD, P_ORG, P_OBJ_EMP, P_ENDDATE, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Delegacy Tax"
    Public Function GetLstBenefitSeniority(ByVal _filter As PaBenefitSeniorityDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PaBenefitSeniorityDTO)
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetLstBenefitSeniority(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetLstBenefitSeniority(ByVal _filter As PaBenefitSeniorityDTO, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PaBenefitSeniorityDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetLstBenefitSeniority(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Calculate_Benefit(ByVal _period_ID As Decimal, ByVal _obj_Emp_ID As Decimal, ByVal _OrgID As Decimal, ByVal _IsDissolve As Decimal) As Boolean
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.Calculate_Benefit(_period_ID, _obj_Emp_ID, _OrgID, _IsDissolve, Me.Log)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Accounting Overtime"
    Public Function GetAccountingOvertime(ByVal _filter As PA_Accounting_OvertimeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_OvertimeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingOvertime(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAccountingOvertime(ByVal _filter As PA_Accounting_OvertimeDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Accounting_OvertimeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingOvertime(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAccountingOvertime(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAccountingOvertime(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangeStatusAccountingOvertime(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusAccountingOvertime(_lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PA_ADDTAX"
    Public Function GetPA_TARGET_DTTD_LABEL(ByVal _filter As PA_TARGET_DTTD_LABELDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_TARGET_DTTD_LABELDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_TARGET_DTTD_LABEL(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetPA_TARGET_DTTD_LABEL(ByVal _filter As PA_TARGET_DTTD_LABELDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_TARGET_DTTD_LABELDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_TARGET_DTTD_LABEL(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_IMPORT_PA_TARGET_DTTD_LABEL() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_IMPORT_PA_TARGET_DTTD_LABEL()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IMPORT_PA_TARGET_DTTD_LABEL(ByVal dtData As DataTable, ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_TARGET_DTTD_LABEL(dtData, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "DTTD_DTPB"
    Public Function GetDTTD_DTPB(ByVal _filter As PA_DTTD_DTPB_EMPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_DTTD_DTPB_EMPDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDTTD_DTPB(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDTTD_DTPB(ByVal _filter As PA_DTTD_DTPB_EMPDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_DTTD_DTPB_EMPDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDTTD_DTPB(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteDTTD_DTPB(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteDTTD_DTPB(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "DTTD_ECD"
    Public Function GetDTTD_ECD(ByVal _filter As PA_DTTD_ECDDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_DTTD_ECDDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDTTD_ECD(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDTTD_ECD(ByVal _filter As PA_DTTD_ECDDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_DTTD_ECDDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDTTD_ECD(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "DTTD_DTPB"
    Public Function GetLDT_VP(ByVal _filter As PA_LDT_VPDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_LDT_VPDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetLDT_VP(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetLDT_VP(ByVal _filter As PA_LDT_VPDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_LDT_VPDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetLDT_VP(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "PA_MA_SCP_QLCH"
    Public Function GetPA_MA_SCP_QLCH(ByVal _filter As PA_MA_SCP_QLCHDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_MA_SCP_QLCHDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_MA_SCP_QLCH(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPA_MA_SCP_QLCH(ByVal _filter As PA_MA_SCP_QLCHDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_MA_SCP_QLCHDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_MA_SCP_QLCH(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Accounting Subsidize"
    Public Function GetAccountingSubsidize(ByVal _filter As PA_AccountingSubsidizeDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_AccountingSubsidizeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingSubsidize(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAccountingSubsidize(ByVal _filter As PA_AccountingSubsidizeDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_AccountingSubsidizeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAccountingSubsidize(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAccountingSubsidize(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAccountingSubsidize(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangeStatusAccountingSubsidize(ByVal _lstID As List(Of Decimal), ByVal _status As Boolean) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusAccountingSubsidize(_lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PA_STORE_SUBSIDIZE"
    Public Function GetPA_STORE_SUBSIDIZE(ByVal _filter As PA_STORE_SUBSIDIZEDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_STORE_SUBSIDIZEDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_STORE_SUBSIDIZE(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPA_STORE_SUBSIDIZE(ByVal _filter As PA_STORE_SUBSIDIZEDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_STORE_SUBSIDIZEDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_STORE_SUBSIDIZE(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_STORE_SUBSIDIZE(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_STORE_SUBSIDIZE(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePA_STORE_SUBSIDIZE(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePA_STORE_SUBSIDIZE(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidatePA_STORE_SUBSIDIZE(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidatePA_STORE_SUBSIDIZE(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_PA_STORE_SUBSIDIZE_IMPORT() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_PA_STORE_SUBSIDIZE_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Get_Brand_Name(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Get_Brand_Name(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Get_Rate(ByVal _objData As PA_STORE_SUBSIDIZEDTO) As PA_STORE_SUBSIDIZEDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Get_Rate(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PA_TARGET_STORE"
    Public Function GET_PA_TARGET_STORE(ByVal _filter As PA_TARGET_STOREDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByVal _param As ParamDTO,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_TARGET_STOREDTO)
        Dim lstPA_TARGET_STORE As List(Of PA_TARGET_STOREDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_TARGET_STORE = rep.GET_PA_TARGET_STORE(_filter, PageIndex, PageSize, _param, Total, Sorts, Me.Log)
                Return lstPA_TARGET_STORE
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_TARGET_STORE(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_TARGET_STORE(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_TARGET_STORE(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_TARGET_STORE(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_TARGET_STORE(ByVal obj As PA_TARGET_STOREDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_TARGET_STORE(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_TARGET_STORE(ByVal _filter As PA_TARGET_STOREDTO,
                                        ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_TARGET_STOREDTO)
        Dim lst As List(Of PA_TARGET_STOREDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_TARGET_STORE(_filter, 0, Integer.MaxValue, _param, 0, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PA_Salary_Detention"
    Public Function GET_PA_SALARY_DETENTION(ByVal _filter As PA_Salary_DetentionDTO, ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Salary_DetentionDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_PA_SALARY_DETENTION(_filter, PageIndex, PageSize, Total, _param, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PA_SALARY_DETENTION(ByVal _filter As PA_Salary_DetentionDTO, ByVal _param As ParamDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_Salary_DetentionDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_PA_SALARY_DETENTION(_filter, 0, Integer.MaxValue, 0, _param, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryDetentionByID(ByVal _filter As PA_Salary_DetentionDTO) As PA_Salary_DetentionDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryDetentionByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPeriod(ByVal isBlank As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPeriod(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INSERT_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SALARY_DETENTION(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function MODIFY_PA_SALARY_DETENTION(ByVal lstObj As List(Of PA_Salary_DetentionDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SALARY_DETENTION(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DELETE_PA_SALARY_DETENTION(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SALARY_DETENTION(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region


#Region "PA_MA_SCP_QLCH"
    Public Function GetManageDTTDDaily(ByVal _filter As PA_ManageDTTDDailyDTO, ByVal PageIndex As Integer,
                                           ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_ManageDTTDDailyDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetManageDTTDDaily(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetManageDTTDDaily(ByVal _filter As PA_ManageDTTDDailyDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_ManageDTTDDailyDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetManageDTTDDaily(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_IMPORT_DTTD_DAILY() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_IMPORT_DTTD_DAILY()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_DTTD_DAILY(ByVal DATA_IN As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_DTTD_DAILY(DATA_IN, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "PayrollSheet lock"
    Public Function GetPayroolSheetLock(ByVal _filter As PA_PayrollSheetLockDTO, ByVal lstOrg As List(Of Decimal),
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_PayrollSheetLockDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayroolSheetLock(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPayroolSheetLock(ByVal _filter As PA_PayrollSheetLockDTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_PayrollSheetLockDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayroolSheetLock(_filter, lstOrg, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ChangeStatusPASheetLock(ByVal lstOrg As List(Of Decimal), ByVal lstEmp As List(Of Decimal), ByVal _status As Decimal, ByVal _period_id As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusPASheetLock(lstOrg, lstEmp, _status, _period_id, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region


#Region "Award 13 month"
    Public Function GetAward13Month(ByVal _filter As PA_Award_13MonthDTO, ByVal lstOrg As List(Of Decimal),
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_Award_13MonthDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAward13Month(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAward13Month(ByVal _filter As PA_Award_13MonthDTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_Award_13MonthDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAward13Month(_filter, lstOrg, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangeStatusAward13Month(ByVal lstID As List(Of Decimal), ByVal _status As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusAward13Month(lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteAward13Month(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAward13Month(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateAward13Month(ByVal lstObj As List(Of PA_Award_13MonthDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.UpdateAward13Month(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateAward13MonthPeriod(ByVal lstObj As List(Of PA_Award_13MonthDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.UpdateAward13MonthPeriod(lstObj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CAL_AWARD_13MONTH(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CAL_AWARD_13MONTH(_org_id, P_YEAR, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function RE_CAL_AWARD_13MONTH(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.RE_CAL_AWARD_13MONTH(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region


#Region "Award 13 month"
    Public Function GetListSalPITCode(ByVal _filter As PA_Salary_PITCodeDTO, ByVal lstOrg As List(Of Decimal),
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_Salary_PITCodeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalPITCode(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSalPITCode(ByVal _filter As PA_Salary_PITCodeDTO, ByVal lstOrg As List(Of Decimal), Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_Salary_PITCodeDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalPITCode(_filter, lstOrg, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangeStatusSalPITCode(ByVal lstID As List(Of Decimal), ByVal _status As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusSalPITCode(lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSalPITCode(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalPITCode(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CAL_SAL_PITCODE(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CAL_SAL_PITCODE(_org_id, P_YEAR, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Tax Income Year"
    Public Function GetListTaxIncomeYear(ByVal _filter As PA_TAXINCOME_YEARDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_TAXINCOME_YEARDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListTaxIncomeYear(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangeStatusTaxIncomeYear(ByVal lstID As List(Of Decimal), ByVal _status As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangeStatusTaxIncomeYear(lstID, _status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteTaxIncomeYear(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteTaxIncomeYear(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateTaxIncomeYearPeriod(ByVal lstID As List(Of Decimal), ByVal _period As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.UpdateTaxIncomeYearPeriod(lstID, _period, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CAL_TAX_INCOME_YEAR(ByVal _org_id As List(Of Decimal), ByVal P_YEAR As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CAL_TAX_INCOME_YEAR(_org_id, P_YEAR, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Document PIT"
    Public Function GetDocumentPITs(ByVal _filter As PA_DOCUMENT_PITDTO, ByVal lstOrg As List(Of Decimal),
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PA_DOCUMENT_PITDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDocumentPITs(_filter, lstOrg, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_EMPLOYEE_PIT_INFO(ByVal P_EMP_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_ID As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_EMPLOYEE_PIT_INFO(P_EMP_ID, P_YEAR, P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertDocumentPIT(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyDocumentPIT(_objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteDocumentPIT(ByVal _lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteDocumentPIT(_lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateDocumentPIT(ByVal _objData As PA_DOCUMENT_PITDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateDocumentPIT(_objData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ChangePITPrintStatus(ByVal lstID As List(Of Decimal), ByVal _type As String, ByVal pit_type As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ChangePITPrintStatus(lstID, _type, pit_type, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region


    Public Function GetHSTDTImport() As DataSet
        Dim dsdata As DataSet

        Using rep As New PayrollBusinessClient
            Try
                dsdata = rep.GetHSTDTImport()
                Return dsdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#Region "Payroll Advance"
    Public Function CAL_PAYROLL_ADVANCE(ByVal _period_ID As Decimal,
                                        ByVal _start_date As Date,
                                        ByVal _end_date As Date,
                                        ByVal _end_date_period As Date,
                                        ByVal _OrgID As Decimal,
                                        ByVal _IsDissolve As Decimal,
                                        ByVal _Sal As Decimal,
                                        ByVal _Nosal As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CAL_PAYROLL_ADVANCE(_period_ID, _start_date, _end_date, _end_date_period, _OrgID, _IsDissolve, _Sal, _Nosal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPayrollAdvance(ByVal _filter As PayrollAdvanceDTO, ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PayrollAdvanceDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollAdvance(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPayrollAdvance(ByVal _filter As PayrollAdvanceDTO, ByVal _param As ParamDTO, Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of PayrollAdvanceDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollAdvance(_filter, _param, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeletePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePayrollAdvance(_lstID, peroid)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ActivePayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal _param As ParamDTO, ByVal peroid As Decimal, ByVal status As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePayrollAdvance(_lstID, _param, peroid, status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyPayrollAdvance(ByVal _lstID As List(Of Decimal), ByVal peroid As Decimal, ByVal salAdvance As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPayrollAdvance(_lstID, peroid, salAdvance, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

End Class
