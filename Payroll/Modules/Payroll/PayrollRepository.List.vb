Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository
#Region "GetAllowance List"

    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)
        Dim lstAllowance As List(Of AllowanceListDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowanceList(_filter, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function



#End Region

#Region "Alowance"
    Public Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)
        Dim lstAllowance As List(Of AllowanceDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowance(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetAllowance(ByVal _filter As AllowanceDTO,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)
        Dim lstAllowance As List(Of AllowanceDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstAllowance = rep.GetAllowance(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAllowance
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertAllowance(objAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyAllowance(objAllowance, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveAllowance(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteAllowance(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Taxation List"

    Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC") As List(Of PATaxationDTO)
        Dim lstTaxation As List(Of PATaxationDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTaxation = rep.GetTaxation(PageIndex, PageSize, Total, Sorts)
                Return lstTaxation
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertTaxation(objTaxation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyTaxation(objTaxation, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveTaxation(ByVal lstTaxation As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveTaxation(lstTaxation, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTaxation(ByVal lstTaxation As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteTaxation(lstTaxation)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Payment List"

    Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Dim lstPaymentList As List(Of PAPaymentListDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPaymentList = rep.GetPaymentList(PageIndex, PageSize, Total, Sorts)
                Return lstPaymentList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaymentList(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaymentList(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActivePaymentList(ByVal lstPaymentList As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePaymentList(lstPaymentList, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSystemCriteria(ByVal lstPaymentList As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSystemCriteria(lstPaymentList, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePaymentList(ByVal lstPaymentList As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaymentList(lstPaymentList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Object Salary"

    Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Dim lstObjectSalary As List(Of PAObjectSalaryDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstObjectSalary = rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts)
                Return lstObjectSalary
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertObjectSalary(objObjectSalary, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateObjectSalary(objObjectSalary)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyObjectSalary(objObjectSalary, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveObjectSalary(ByVal lstObjectSalary As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveObjectSalary(lstObjectSalary, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteObjectSalary(ByVal lstObjectSalary As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteObjectSalary(lstObjectSalary)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "List Salary"

    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup,
                                   Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAllFomulerGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)
        Dim lstPAFomulerGroup As List(Of PAFomulerGroup)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function EXPORT_CH() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.EXPORT_CH(Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetAllNorm(ByVal _filter As NormDTO,
                                   Optional ByVal Sorts As String = "ID") As List(Of NormDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetAllNorm(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetAllNorm(ByVal _filter As NormDTO,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "ID") As List(Of NormDTO)
        Dim lstPAFomulerGroup As List(Of NormDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetAllNorm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertFomulerGroup(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFomulerGroup(objPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertNorm(ByVal obj As NormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertNorm(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyNorm(ByVal obj As NormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyNorm(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function checkDup(ByVal obj As NormDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.checkDup(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteNorm(ByVal lstDelete As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteNorm(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteFomulerGroup(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListAllSalary(gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListInputColumn(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListInputColumn(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListSalColunm(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalColunm(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetObjectSalaryColumn(ByVal typePaymentId As Decimal) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetObjectSalaryColumn(typePaymentId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListCalculation()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CopyFomuler(ByRef F_ID As Decimal, ByRef T_ID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CopyFomuler(F_ID, Me.Log, T_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function SaveFomuler(ByVal objData As PAFomuler, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveFomuler(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveFolmulerGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Period"

    Public Function GetPeriodList(ByVal _filter As ATPeriodDTO,
                                  ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "YEAR DESC,MONTH ASC") As List(Of ATPeriodDTO)
        Dim lstPeriod As List(Of ATPeriodDTO)
        Using rep As New PayrollBusinessClient
            Try
                lstPeriod = rep.GetPeriodList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Dim lstPeriod As List(Of ATPeriodDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPeriodbyYear(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetSalaryTypebyIncentive(ByVal incentive As Decimal) As List(Of SalaryTypeDTO)
        Dim lstPeriod As List(Of SalaryTypeDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetSalaryTypebyIncentive(incentive)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPaymentSourcesbyYear(ByVal year As Decimal) As List(Of PaymentSourcesDTO)
        Dim lstPeriod As List(Of PaymentSourcesDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPaymentSourcesbyYear(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListOrgBonus() As List(Of OrgBonusDTO)
        Dim lstPeriod As List(Of OrgBonusDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetListOrgBonus()
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPeriod(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPeriod(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPeriodDay(objPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPeriod(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePeriod(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActivePeriod(ByVal lstWorkFactor As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePeriod(lstWorkFactor, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Salaries List"

    Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO)
        Dim lstListSalaries As List(Of PAListSalariesDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetListSalaries(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertListSalaries(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertPA_SAL_MAPPING(ByVal objListSalaries As PA_SALARY_FUND_MAPPINGDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_SAL_MAPPING(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyListSalaries(objListSalaries, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListSalaries(ByVal lstListSalaries As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveListSalaries(lstListSalaries, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSalariesStatus(ByVal lstListSalaries As List(Of Decimal), ByVal sActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSalariesStatus(lstListSalaries, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSalaries(ByVal lstListSalaries As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSalaries(lstListSalaries)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        Dim lstListSal As List(Of PAListSalDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSal = rep.GetListSal(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstListSal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertListSal(ByVal objListSal As PAListSalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertListSal(objListSal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyListSal(ByVal objListSal As PAListSalDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyListSal(objListSal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveListSal(ByVal lstListSal As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveListSal(lstListSal, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteListSal(ByVal lstListSal As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteListSal(lstListSal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SET UP COMPLETION KPI SHOP MANAGER"
    Public Function GetSetupCompletion_KPI_ShopManager(ByVal _filter As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO)
        Dim lst As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupCompletion_KPI_ShopManager(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetSetupBonusKpiProductType(ByVal _filter As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO)
        Dim lst As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupBonusKpiProductType(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function CheckBrandAndShopType(ByVal brandID As Integer, ByVal shopID As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Dim result = rep.CheckBrandAndShopType(brandID, shopID)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetSetupRate(ByVal _filter As PA_SETUP_RATE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTO)
        Dim lst As List(Of PA_SETUP_RATE_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupRate(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupCompletion_KPI_ShopManager(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupCompletion_KPI_ShopManager(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertSetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupRate(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupRate(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertSetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupBonusKpiProductType(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupBonusKpiProductType(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupCompletion_KPI_ShopManager(ByVal _validate As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupCompletion_KPI_ShopManager(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupRate(ByVal _validate As PA_SETUP_RATE_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupRate(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupBonusKpiProductType(ByVal _validate As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupBonusKpiProductType(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupCompletion_KPI_ShopManager(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupCompletion_KPI_ShopManager(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupRate(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupRate(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupBonusKpiProductType(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupBonusKpiProductType(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(Common.Common.SystemLanguage.Name)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(P_DOCXML, P_USER)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PAYROLL_SHEET_SUM_IMPORT(ByVal P_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_USERNAME As String) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_PAYROLL_SHEET_SUM_IMPORT(P_ORG_ID, P_YEAR, P_PERIOD_ID, P_USERNAME)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_PAYROLL_SHEET_SUM(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PAYROLL_SHEET_SUM(P_DOCXML, P_USER)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function IMPORT_PAYROLL_ADVANCE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PAYROLL_ADVANCE(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "SET UP SHOP GRADE"

    Public Function GetSetupShopGrade(ByVal _filter As PA_SETUP_SHOP_GRADEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_SHOP_GRADEDTO)
        Dim lst As List(Of PA_SETUP_SHOP_GRADEDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupShopGrade(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupShopGrade(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupShopGrade(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupShopGrade(ByVal _validate As PA_SETUP_SHOP_GRADEDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupShopGrade(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupShopGrade(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupShopGrade(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region


#Region "SET UP FRAMEWORK OFFICE"
    Public Function GetGroup_Tilte() As DataTable
        Try
            Using rep As New PayrollBusinessClient
                Try
                    Return rep.GetGroup_Tilte()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetHU_TITLE() As DataTable
        Try
            Using rep As New PayrollBusinessClient
                Try
                    Return rep.GetHU_TITLE()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSetupFrameWorkOffice(ByVal _filter As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO)
        Dim lst As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupFrameWorkOffice(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupFrameWorkOffice(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupFrameWorkOffice(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupFrameWorkOffice(ByVal _validate As PA_SETUP_FRAMEWORK_OFFICEDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupFrameWorkOffice(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupFrameWorkOffice(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupFrameWorkOffice(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SET UP FRAMEWORK KPI"
    Public Function GET_KPI_IMPORT() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_KPI_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetSetupFrameWorkKPI(ByVal _filter As PA_SETUP_FRAMEWORK_KPIDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_KPIDTO)
        Dim lst As List(Of PA_SETUP_FRAMEWORK_KPIDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupFrameWorkKPI(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupFrameWorkKPI(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupFrameWorkKPI(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateFrameWorkKPI(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupFrameWorkKPI(ByVal _validate As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupFrameWorkKPI(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupFrameWorkKPI(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupFrameWorkKPI(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SET UP FRAMEWORK ECD"
    Public Function GetSetupFrameWorkECD(ByVal _filter As PA_SETUP_FRAMEWORK_ECDDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_ECDDTO)
        Dim lst As List(Of PA_SETUP_FRAMEWORK_ECDDTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetSetupFrameWorkECD(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSetupFrameWorkECD(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifySetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySetupFrameWorkECD(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateSetupFrameWorkECD(ByVal _validate As PA_SETUP_FRAMEWORK_ECDDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSetupFrameWorkECD(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteSetupFrameWorkECD(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSetupFrameWorkECD(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_FRAMEWORK_ECD_IMPORT_DATA() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_FRAMEWORK_ECD_IMPORT_DATA()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_FRAMEWORK_ECD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_FRAMEWORK_ECD(P_DOCXML, P_USER)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Work Standard"

    Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)
        Dim lstListSalaries As List(Of Work_StandardDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetWorkStandard(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetWorkStandardByYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        Dim lstListSalaries As List(Of Work_StandardDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstListSalaries = rep.GetWorkStandardbyYear(year)
                Return lstListSalaries
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWorkStandard(ByVal objWorkStandard As Work_StandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertWorkStandard(objWorkStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorkStandard(ByVal objWorkStandard As Work_StandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyWorkStandard(objWorkStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveWorkStandard(ByVal lstWorkStandard As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveWorkStandard(lstWorkStandard, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateWorkStandard(ByVal lstWorkStandard As Work_StandardDTO) As Boolean
        'Try
        '    Return PayrollRepositoryStatic.Instance.ValidateWorkStandard(objPeriod)
        'Catch ex As Exception
        '    Throw ex
        'End Try
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateWorkStandard(lstWorkStandard)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteWorkStandard(ByVal lstWorkStandard As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteWorkStandard(lstWorkStandard)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IsCompanyLevel(org_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "lunch list : Đơn giá tiền ăn trưa"

    Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)
        Dim lstPeriod As List(Of ATPriceLunchDTO)
        Using rep As New PayrollBusinessClient
            Try
                lstPeriod = rep.GetPriceLunchList(PageIndex, PageSize, Total, Sorts)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)
        Dim lstPeriod As List(Of ATPriceLunchDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPeriod = rep.GetPriceLunch(year)
                Return lstPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPriceLunch(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPriceLunch(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateATPriceLunchOrg(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPriceLunch(objPeriod, objOrgPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePriceLunch(lstPeriod)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of CostCenterDTO)
        Dim lstCostCenter As List(Of CostCenterDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstCostCenter = rep.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCostCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                       Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of CostCenterDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetCostCenter(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateCostCenter(objCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveCostCenter(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteCostCenterStatus(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenter(ByVal lstCostCenter As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteCostCenter(lstCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Org Bonus"
    Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetOrgBonus(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertOrgBonus(ByVal lstOrgBonus As OrgBonusDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertOrgBonus(lstOrgBonus, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyOrgBonus(ByVal lstOrgBonus As OrgBonusDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyOrgBonus(lstOrgBonus, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveOrgBonus(ByVal lstOrgBonus As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveOrgBonus(lstOrgBonus, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteOrgBonus(ByVal lstOrgBonus As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteOrgBonus(lstOrgBonus)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateOrgBonus(ByVal objOrgBonus As OrgBonusDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateOrgBonus(objOrgBonus)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Payment Sources"
    Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS asc") As List(Of PaymentSourcesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPaymentSources(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertPaymentSources(ByVal lstPaymentSources As PaymentSourcesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaymentSources(lstPaymentSources, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyPaymentSources(ByVal lstPaymentSources As PaymentSourcesDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaymentSources(lstPaymentSources, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActivePaymentSources(ByVal lstPaymentSources As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActivePaymentSources(lstPaymentSources, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeletePaymentSources(ByVal lstPaymentSources As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaymentSources(lstPaymentSources)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Work Factor"
    Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " EFFECT_DATE desc") As List(Of WorkFactorDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetWorkFactor(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function InsertWorkFactor(ByVal lstWorkFactor As WorkFactorDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertWorkFactor(lstWorkFactor, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyWorkFactor(ByVal lstWorkFactor As WorkFactorDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyWorkFactor(lstWorkFactor, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveWorkFactor(ByVal lstWorkFactor As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveWorkFactor(lstWorkFactor, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteWorkFactor(ByVal lstWorkFactor As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteWorkFactor(lstWorkFactor)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateWorkFactor(ByVal objWorkFactor As WorkFactorDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateWorkFactor(objWorkFactor)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "SalaryFund List"

    Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryFundByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.UpdateSalaryFund(objSalaryFund, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "TitleCost List"

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Dim lstTitleCost As List(Of PATitleCostDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTitleCost = rep.GetTitleCost(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstTitleCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Dim lstTitleCost As List(Of PATitleCostDTO)

        Using rep As New PayrollBusinessClient
            Try
                lstTitleCost = rep.GetTitleCost(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstTitleCost
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertTitleCost(objTitleCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyTitleCost(objTitleCost, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteTitleCost(ByVal lstTitleCost As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteTitleCost(lstTitleCost)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Get List Manning"

    Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.LoadComboboxListMannName(org_id, year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "Validate Combobox"

    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "PA_SYSTEM_CRITERIA"

    Public Function GetPA_SYSTEM_CRITERIA(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SYSTEM_CRITERIADTO)
        Dim lstPaymentList As List(Of PA_SYSTEM_CRITERIADTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPaymentList = rep.GetPA_SYSTEM_CRITERIA(PageIndex, PageSize, Total, Sorts)
                Return lstPaymentList
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_SYSTEM_CRITERIA(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_SYSTEM_CRITERIA(objPaymentList, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateList_PA_SYSTEM_CRITERIA(ByVal _validate As PA_SYSTEM_CRITERIADTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateList_PA_SYSTEM_CRITERIA(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePA_SYSTEM_CRITERIA(ByVal lstPaymentList As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePA_SYSTEM_CRITERIA(lstPaymentList)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SALE COMMISION"
    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SaleCommisionDTO)
        Dim lstSalaryType As List(Of SaleCommisionDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryType = rep.GetSaleCommision(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO,
                                      Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SaleCommisionDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSaleCommision(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSaleCommision(objSaleCommision, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySaleCommision(objSaleCommision, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSaleCommision(ByVal lstSaleCommision As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSaleCommision(lstSaleCommision)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSaleCommision(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

    Public Function GetBrandRate(ByVal _filter As PA_BrandRate_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer) As List(Of PA_BrandRate_DTO)
        Dim lst As List(Of PA_BrandRate_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GetBrandRate(_filter, PageIndex, PageSize, Total, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function InsertBrandRate(ByVal obj As PA_BrandRate_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertBrandRate(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyBrandRate(ByVal obj As PA_BrandRate_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyBrandRate(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateBrandRate(ByVal _validate As PA_BrandRate_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateBrandRate(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteBrandRate(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteBrandRate(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPA_AOMS_TOM_MNG(ByVal _filter As PA_AOMS_TOM_MNG_DTO,
                                   Optional ByVal Sorts As String = "ID") As List(Of PA_AOMS_TOM_MNG_DTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_AOMS_TOM_MNG(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetPA_AOMS_TOM_MNG(ByVal _filter As PA_AOMS_TOM_MNG_DTO,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "ID") As List(Of PA_AOMS_TOM_MNG_DTO)
        Dim lstPAFomulerGroup As List(Of PA_AOMS_TOM_MNG_DTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetPA_AOMS_TOM_MNG(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_AOMS_TOM_MNG(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_AOMS_TOM_MNG(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePA_AOMS_TOM_MNG(ByVal lstDelete As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePA_AOMS_TOM_MNG(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckPA_AOMS_TOMExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckPA_AOMS_TOMExits(empID, orgID, pDate, pID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckPA_AOMS_TOMExits_EF_EX(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckPA_AOMS_TOMExits_EF_EX(empID, orgID, pDate, pID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetExportAomsTom() As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetExportAomsTom()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_PA_AOMS_TOM_MNG(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_AOMS_TOM_MNG(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_PA_EMP_FORMULER(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_EMP_FORMULER(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_PA_SETUP_FRAMEWORK_OFFICE(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_SETUP_FRAMEWORK_OFFICE(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_PA_SETUP_HESOMR_NV_QLCH(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_SETUP_HESOMR_NV_QLCH(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#Region "Thiết lập nhân viên theo công thức lương"


    Public Function GetPA_EMP_FORMULER(ByVal _filter As PA_EMP_FORMULER_DTO,
                                   Optional ByVal Sorts As String = "ID") As List(Of PA_EMP_FORMULER_DTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_EMP_FORMULER(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetPA_EMP_FORMULER(ByVal _filter As PA_EMP_FORMULER_DTO,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "ID") As List(Of PA_EMP_FORMULER_DTO)
        Dim lstPAFomulerGroup As List(Of PA_EMP_FORMULER_DTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetPA_EMP_FORMULER(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetObj_Sal() As List(Of PAObjectSalaryDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetObj_Sal()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetFORMULER_GROUP(ByVal objSalID As Decimal) As List(Of PAObjectSalaryDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetFORMULER_GROUP(objSalID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GETGROUP_EMPLOYEE_ID(ByVal titleID As Decimal) As Decimal?
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GETGROUP_EMPLOYEE_ID(titleID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_EMP_FORMULER(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_EMP_FORMULER(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeletePA_EMP_FORMULER(ByVal lstDelete As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePA_EMP_FORMULER(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckPA_EMP_FORMULERExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal TitleID As Decimal, ByVal groupTitleID As Decimal, ByVal formulerID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckPA_EMP_FORMULERExits(empID, orgID, TitleID, groupTitleID, formulerID, pDate, pID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function GetPA_SUM_CH_TOM(ByVal _filter As PA_SumCHTomDTO,
                                   Optional ByVal Sorts As String = "ID") As List(Of PA_SumCHTomDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPA_SUM_CH_TOM(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetPA_SUM_CH_TOM(ByVal _filter As PA_SumCHTomDTO,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "ID") As List(Of PA_SumCHTomDTO)
        Dim lstPAFomulerGroup As List(Of PA_SumCHTomDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetPA_SUM_CH_TOM(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CAL_PA_SUM_CH_TOM(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_ISDISSOLVE As Decimal) As Boolean
        Using rep As New PayrollBusinessClient

            Try
                Return rep.CAL_PA_SUM_CH_TOM(P_PERIOD, P_ORG, P_ISDISSOLVE, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#Region "PA_FRAME_SALARY - Khung hệ số lương chức danh"
    Public Function GetFrameSalary(ByVal sACT As String) As List(Of PA_FRAME_SALARYDTO)
        Dim lstPAFomulerGroup As List(Of PA_FRAME_SALARYDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetFrameSalary(sACT)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertFrameSalary(ByVal obj As PA_FRAME_SALARYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertFrameSalary(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetNameFrameSalary(ByVal org_id As Decimal) As String
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetNameFrameSalary(org_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetMaxId() As Decimal
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMaxId()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateFrameSalary(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFrameSalary(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyFrameSalaryPath(ByVal lstPath As List(Of PA_FRAME_SALARY_PATHDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFrameSalaryPath(lstPath)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveOrganization(ByVal lstOrganization As List(Of PA_FRAME_SALARYDTO), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveFrameSalary(lstOrganization, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "PA_FRAME_PRODUCTIVITY - Khung hệ số năng suất"
    Public Function GetFrame_Productivity(ByVal sACT As String) As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Dim lstPAFomulerGroup As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstPAFomulerGroup = rep.GetFrame_Productivity(sACT)
                Return lstPAFomulerGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertFrame_Productivity(ByVal obj As PA_FRAME_PRODUCTIVITYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertFrame_Productivity(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetNameFrame_Productivity(ByVal org_id As Decimal) As String
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetNameFrame_Productivity(org_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetMaxIdFrame_Productivity() As Decimal
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMaxIdFrame_Productivity()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateFrame_Productivity(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFrame_Productivity(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyFrame_ProductivityPath(ByVal lstPath As List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyFrame_ProductivityPath(lstPath)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveFrame_Productivity(ByVal lstOrganization As List(Of PA_FRAME_PRODUCTIVITYDTO), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveFrame_Productivity(lstOrganization, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#End Region
End Class
