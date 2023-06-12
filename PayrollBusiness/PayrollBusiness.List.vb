Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness
#Region "Allowance List"

        Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO) Implements ServiceContracts.IPayrollBusiness.GetAllowanceList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAllowanceList(_filter, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function



#End Region

#Region "Allowance"
        Public Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO) Implements ServiceContracts.IPayrollBusiness.GetAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAllowance(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertAllowance(objAllowance, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyAllowance(objAllowance, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveAllowance(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAllowance(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Taxation List"

        Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO) Implements ServiceContracts.IPayrollBusiness.GetTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.GetTaxation(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertTaxation(objTaxation, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyTaxation(objTaxation, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveTaxation(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteTaxation(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Payment list"

        Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentList(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentListAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentListAll(Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPaymentList(objPaymentList, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyPaymentList(objPaymentList, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePaymentList(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveSystemCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSystemCriteria
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSystemCriteria(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePaymentList(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ObjectSalary"

        Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalaryAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalaryAll(Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateObjectSalary(objObjectSalary)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveObjectSalary(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteObjectSalary(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Period List"
        Public Function GetPeriodList(ByVal _filter As ATPeriodDTO,
                                      ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "YEAR DESC,MONTH ASC") As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodList(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPeriod
            Try
                Return PayrollRepositoryStatic.Instance.InsertPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriod(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriodDay
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriodDay(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function checkDup(ByVal obj As NormDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.checkDup
            Try
                Return PayrollRepositoryStatic.Instance.checkDup(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePeriod
            Try
                Return PayrollRepositoryStatic.Instance.DeletePeriod(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePeriod(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SET UP COMPLETION KPI SHOP MANAGER"
        Public Function GetSetupCompletion_KPI_ShopManager(ByVal _filter As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO) Implements ServiceContracts.IPayrollBusiness.GetSetupCompletion_KPI_ShopManager
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupCompletion_KPI_ShopManager(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSetupRate(ByVal _filter As PA_SETUP_RATE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTO) Implements ServiceContracts.IPayrollBusiness.GetSetupRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupRate(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckBrandAndShopType(ByVal brandID As Integer, ByVal shopID As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckBrandAndShopType
            Try
                Dim rep = PayrollRepositoryStatic.Instance.CheckBrandAndShopType(brandID, shopID)
                Return rep
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSetupBonusKpiProductType(ByVal _filter As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO) Implements ServiceContracts.IPayrollBusiness.GetSetupBonusKpiProductType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupBonusKpiProductType(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupCompletion_KPI_ShopManager
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupCompletion_KPI_ShopManager(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupCompletion_KPI_ShopManager
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupCompletion_KPI_ShopManager(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupBonusKpiProductType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupBonusKpiProductType(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupBonusKpiProductType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupBonusKpiProductType(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupRate(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupRate(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupCompletion_KPI_ShopManager(ByVal _validate As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupCompletion_KPI_ShopManager
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupCompletion_KPI_ShopManager(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupRate(ByVal _validate As PA_SETUP_RATE_DTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupRate(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupBonusKpiProductType(ByVal _validate As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupBonusKpiProductType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupBonusKpiProductType(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupCompletion_KPI_ShopManager(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupCompletion_KPI_ShopManager
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupCompletion_KPI_ShopManager(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupRate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupRate(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupBonusKpiProductType(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupBonusKpiProductType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupBonusKpiProductType(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_PAYROLL_SHEET_SUM_IMPORT(ByVal P_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_USERNAME As String) As DataSet _
           Implements ServiceContracts.IPayrollBusiness.GET_PAYROLL_SHEET_SUM_IMPORT
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GET_PAYROLL_SHEET_SUM_IMPORT(P_ORG_ID, P_YEAR, P_PERIOD_ID, P_USERNAME)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(P_DOCXML, P_USER)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal sLang As String) As DataSet _
                   Implements ServiceContracts.IPayrollBusiness.EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(sLang)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function IMPORT_PAYROLL_SHEET_SUM(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
           Implements ServiceContracts.IPayrollBusiness.IMPORT_PAYROLL_SHEET_SUM
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_PAYROLL_SHEET_SUM(P_DOCXML, P_USER)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_PAYROLL_ADVANCE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
           Implements ServiceContracts.IPayrollBusiness.IMPORT_PAYROLL_ADVANCE
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_PAYROLL_ADVANCE(P_DOCXML, P_USER)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "SET UP SHOP GRADE"

        Public Function GetSetupShopGrade(ByVal _filter As PA_SETUP_SHOP_GRADEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_SHOP_GRADEDTO) Implements ServiceContracts.IPayrollBusiness.GetSetupShopGrade
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupShopGrade(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupShopGrade
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupShopGrade(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupShopGrade
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupShopGrade(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupShopGrade(ByVal _validate As PA_SETUP_SHOP_GRADEDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupShopGrade
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupShopGrade(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupShopGrade(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupShopGrade
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupShopGrade(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SET UP FRAMEWORK OFFICE"
        Public Function GetGroup_Tilte() As DataTable Implements ServiceContracts.IPayrollBusiness.GetGroup_Tilte
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetGroup_Tilte()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetHU_TITLE() As DataTable Implements ServiceContracts.IPayrollBusiness.GetHU_TITLE
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetHU_TITLE()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSetupFrameWorkOffice(ByVal _filter As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO) Implements ServiceContracts.IPayrollBusiness.GetSetupFrameWorkOffice
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupFrameWorkOffice(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupFrameWorkOffice
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupFrameWorkOffice(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupFrameWorkOffice
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupFrameWorkOffice(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupFrameWorkOffice(ByVal _validate As PA_SETUP_FRAMEWORK_OFFICEDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupFrameWorkOffice
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupFrameWorkOffice(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupFrameWorkOffice(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupFrameWorkOffice
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupFrameWorkOffice(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SET UP FRAMEWORK KPI"
        Public Function GET_KPI_IMPORT() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_KPI_IMPORT
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GET_KPI_IMPORT()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSetupFrameWorkKPI(ByVal _filter As PA_SETUP_FRAMEWORK_KPIDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_KPIDTO) Implements ServiceContracts.IPayrollBusiness.GetSetupFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupFrameWorkKPI(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupFrameWorkKPI(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupFrameWorkKPI(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateFrameWorkKPI(obj)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateSetupFrameWorkKPI(ByVal _validate As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupFrameWorkKPI(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupFrameWorkKPI(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupFrameWorkKPI
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupFrameWorkKPI(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


#Region "SET UP FRAMEWORK ECD"
        Public Function GetSetupFrameWorkECD(ByVal _filter As PA_SETUP_FRAMEWORK_ECDDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_ECDDTO) Implements ServiceContracts.IPayrollBusiness.GetSetupFrameWorkECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetupFrameWorkECD(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertSetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetupFrameWorkECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertSetupFrameWorkECD(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetupFrameWorkECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifySetupFrameWorkECD(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateSetupFrameWorkECD(ByVal _validate As PA_SETUP_FRAMEWORK_ECDDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSetupFrameWorkECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateSetupFrameWorkECD(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSetupFrameWorkECD(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetupFrameWorkECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteSetupFrameWorkECD(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_FRAMEWORK_ECD_IMPORT_DATA() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_FRAMEWORK_ECD_IMPORT_DATA
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GET_FRAMEWORK_ECD_IMPORT_DATA()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_FRAMEWORK_ECD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_FRAMEWORK_ECD
            Try
                Dim lst = PayrollRepositoryStatic.Instance.IMPORT_FRAMEWORK_ECD(P_DOCXML, P_USER)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "work standard"
        Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkStandard
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetWorkStandard(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkStandardbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetWorkStandardbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.InsertWorkStandard(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateWorkStandard(ByVal objPeriod As Work_StandardDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.ValidateWorkStandard(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.IsCompanyLevel
            Try
                Return PayrollRepositoryStatic.Instance.IsCompanyLevel(org_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.ModifyWORKSTANDARD(objPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteWorkStandard
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteWorkStandard(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveWorkStandard
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveWorkStandard(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "List Salary"
        Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "IDX ASC, CREATED_DATE desc") As List(Of PAFomulerGroup) Implements ServiceContracts.IPayrollBusiness.GetAllFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function EXPORT_CH(ByVal log As UserLog) As DataSet _
         Implements ServiceContracts.IPayrollBusiness.EXPORT_CH
            Using rep As New PayrollRepository
                Try
                    Return rep.EXPORT_CH(log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetAllNorm(ByVal _filter As NormDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "ID") As List(Of NormDTO) Implements ServiceContracts.IPayrollBusiness.GetAllNorm
            Try
                Return PayrollRepositoryStatic.Instance.GetAllNorm(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifyFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertNorm
            Try
                Return PayrollRepositoryStatic.Instance.InsertNorm(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyNorm
            Try
                Return PayrollRepositoryStatic.Instance.ModifyNorm(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteFomulerGroup(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteNorm(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteNorm
            Try
                Return PayrollRepositoryStatic.Instance.DeleteNorm(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler) Implements ServiceContracts.IPayrollBusiness.GetListAllSalary
            Try
                Return PayrollRepositoryStatic.Instance.GetListAllSalary(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetObjectSalaryColumn
            Try
                Return PayrollRepositoryStatic.Instance.GetObjectSalaryColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListSalColunm(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetListSalColunm
            Try
                Return PayrollRepositoryStatic.Instance.GetListSalColunm(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetListInputColumn
            Try
                Return PayrollRepositoryStatic.Instance.GetListInputColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO) Implements ServiceContracts.IPayrollBusiness.GetListCalculation
            Try
                Return PayrollRepositoryStatic.Instance.GetListCalculation()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckFomuler
            Try
                Return PayrollRepositoryStatic.Instance.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CopyFomuler(ByRef F_ID As Decimal, ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CopyFomuler
            Try
                Return PayrollRepositoryStatic.Instance.CopyFomuler(F_ID, log, T_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveFomuler
            Try
                Return PayrollRepositoryStatic.Instance.SaveFomuler(objData, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveFolmulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ActiveFolmulerGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Salary list"

        Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.GetListSalaries(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertListSalaries(objListSalaries, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPA_SAL_MAPPING(ByVal objListSal As PA_SALARY_FUND_MAPPINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_SAL_MAPPING
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPA_SAL_MAPPING(objListSal, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyListSalaries(objListSalaries, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveListSalaries(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSalariesStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSalariesStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSalaries(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO) Implements ServiceContracts.IPayrollBusiness.GetListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.GetListSal(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertListSal(objListSal, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyListSal(objListSal, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveListSal(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function



        Public Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSal(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunchList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunchList(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunch
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunch(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.InsertPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunch(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunchOrg
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunchOrg(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.DeletePriceLunch(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


#Region "SalaryGroup"
        Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO) Implements ServiceContracts.IPayrollBusiness.GetCostCenter
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.InsertCostCenter(objCostCenter, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCostCenter(ByVal obj As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.ModifyCostCenter(obj, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ValidateCostCenter(ByVal obj As CostCenterDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.ValidateCostCenter(obj)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveCostCenter
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveCostCenter(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteCostCenterStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteCostCenterStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.DeleteCostCenter(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Org Bonus"
        Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO) Implements ServiceContracts.IPayrollBusiness.GetOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.GetOrgBonus(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertOrgBonus
            Dim rep As New PayrollRepository
            Return rep.InsertOrgBonus(objTitle, log, gID)

        End Function
        Public Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyOrgBonus
            Dim rep As New PayrollRepository
            Return rep.ModifyOrgBonus(objTitle, log, gID)

        End Function
        Public Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveOrgBonus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteOrgBonus(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateOrgBonus(ByVal _validate As OrgBonusDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateOrgBonus(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Payment Sources"
        Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of PaymentSourcesDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentSources(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaymentSources
            Dim rep As New PayrollRepository
            Return rep.InsertPaymentSources(objTitle, log, gID)

        End Function
        Public Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaymentSources
            Dim rep As New PayrollRepository
            Return rep.ModifyPaymentSources(objTitle, log, gID)

        End Function
        Public Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePaymentSources(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePaymentSources(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Work Factor"
        Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of WorkFactorDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.GetWorkFactor(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertWorkFactor
            Dim rep As New PayrollRepository
            Return rep.InsertWorkFactor(objTitle, log, gID)

        End Function
        Public Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyWorkFactor
            Dim rep As New PayrollRepository
            Return rep.ModifyWorkFactor(objTitle, log, gID)

        End Function
        Public Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveWorkFactor(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteWorkFactor(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateWorkFactor(ByVal _validate As WorkFactorDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateWorkFactor(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "SalaryFund List"

        Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryFundByID
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryFundByID(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.UpdateSalaryFund
            Try
                Dim rep As New PayrollRepository
                Return rep.UpdateSalaryFund(objSalaryFund, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "TitleCost List"

        Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                   ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO) Implements ServiceContracts.IPayrollBusiness.GetTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.GetTitleCost(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertTitleCost(objTitleCost, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyTitleCost(objTitleCost, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteTitleCost(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Get List Manning"
        Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.LoadComboboxListMannName
            Try
                Dim rep As New PayrollRepository
                Return rep.LoadComboboxListMannName(org_id, year)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "Validate Combobox"
        Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ValidateCombobox
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "PA_SYSTEM_CRITERIA"

        Public Function GetPA_SYSTEM_CRITERIA(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SYSTEM_CRITERIADTO) Implements ServiceContracts.IPayrollBusiness.GetPA_SYSTEM_CRITERIA
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPA_SYSTEM_CRITERIA(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_SYSTEM_CRITERIA
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPA_SYSTEM_CRITERIA(objPaymentList, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPA_SYSTEM_CRITERIA(ByVal objPaymentList As PA_SYSTEM_CRITERIADTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_SYSTEM_CRITERIA
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyPA_SYSTEM_CRITERIA(objPaymentList, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeletePA_SYSTEM_CRITERIA(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePA_SYSTEM_CRITERIA
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePA_SYSTEM_CRITERIA(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateList_PA_SYSTEM_CRITERIA(ByVal _validate As PA_SYSTEM_CRITERIADTO) Implements ServiceContracts.IPayrollBusiness.ValidateList_PA_SYSTEM_CRITERIA
            Using rep As New PayrollRepository
                Try
                    Return rep.ValidateList_PA_SYSTEM_CRITERIA(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "SALE COMMISION"
        Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO) Implements ServiceContracts.IPayrollBusiness.GetSaleCommision
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSaleCommision(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.InsertSaleCommision(objSaleCommision, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.ModifySaleCommision(objSaleCommision, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSaleCommision(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
           Implements ServiceContracts.IPayrollBusiness.ActiveSaleCommision
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSaleCommision(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetBrandRate(ByVal _filter As PA_BrandRate_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_BrandRate_DTO) Implements ServiceContracts.IPayrollBusiness.GetBrandRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetBrandRate(_filter, PageIndex, PageSize, Total, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertBrandRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertBrandRate(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyBrandRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ModifyBrandRate(obj, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateBrandRate(ByVal _validate As PA_BrandRate_DTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateBrandRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.ValidateBrandRate(_validate)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteBrandRate(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteBrandRate
            Try
                Dim lst = PayrollRepositoryStatic.Instance.DeleteBrandRate(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#Region "Thiết lập cửa hàng do AOMs, TOM quản lý"
        Public Function GetPA_AOMS_TOM_MNG(ByVal _filter As PA_AOMS_TOM_MNG_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_AOMS_TOM_MNG_DTO) Implements ServiceContracts.IPayrollBusiness.GetPA_AOMS_TOM_MNG
            Try
                Return PayrollRepositoryStatic.Instance.GetPA_AOMS_TOM_MNG(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_AOMS_TOM_MNG
            Try
                Return PayrollRepositoryStatic.Instance.InsertPA_AOMS_TOM_MNG(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_AOMS_TOM_MNG
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPA_AOMS_TOM_MNG(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePA_AOMS_TOM_MNG(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePA_AOMS_TOM_MNG
            Try
                Return PayrollRepositoryStatic.Instance.DeletePA_AOMS_TOM_MNG(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckPA_AOMS_TOMExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPA_AOMS_TOMExits
            Try
                Return PayrollRepositoryStatic.Instance.CheckPA_AOMS_TOMExits(empID, orgID, pDate, pID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckPA_AOMS_TOMExits_EF_EX(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPA_AOMS_TOMExits_EF_EX
            Try
                Return PayrollRepositoryStatic.Instance.CheckPA_AOMS_TOMExits_EF_EX(empID, orgID, pDate, pID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetExportAomsTom() As DataTable Implements ServiceContracts.IPayrollBusiness.GetExportAomsTom
            Try
                Return PayrollRepositoryStatic.Instance.GetExportAomsTom()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_PA_AOMS_TOM_MNG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_AOMS_TOM_MNG
            Try
                Return PayrollRepositoryStatic.Instance.IMPORT_PA_AOMS_TOM_MNG(P_DOCXML, P_USER)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_PA_EMP_FORMULER(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_EMP_FORMULER
            Try
                Return PayrollRepositoryStatic.Instance.IMPORT_PA_EMP_FORMULER(P_DOCXML, P_USER)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_PA_SETUP_FRAMEWORK_OFFICE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_SETUP_FRAMEWORK_OFFICE
            Try
                Return PayrollRepositoryStatic.Instance.IMPORT_PA_SETUP_FRAMEWORK_OFFICE(P_DOCXML, P_USER)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_PA_SETUP_HESOMR_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_SETUP_HESOMR_NV_QLCH
            Try
                Return PayrollRepositoryStatic.Instance.IMPORT_PA_SETUP_HESOMR_NV_QLCH(P_DOCXML, P_USER)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Thiết lập nhân viên theo công thức lương"

        Public Function GetPA_EMP_FORMULER(ByVal _filter As PA_EMP_FORMULER_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_FORMULER_DTO) Implements ServiceContracts.IPayrollBusiness.GetPA_EMP_FORMULER
            Try
                Return PayrollRepositoryStatic.Instance.GetPA_EMP_FORMULER(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function



        Public Function GetObj_Sal() As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObj_Sal
            Try
                Return PayrollRepositoryStatic.Instance.GetObj_Sal()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetFORMULER_GROUP(ByVal objSalID As Decimal) As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetFORMULER_GROUP
            Try
                Return PayrollRepositoryStatic.Instance.GetFORMULER_GROUP(objSalID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GETGROUP_EMPLOYEE_ID(ByVal titleID As Decimal) As Decimal? Implements ServiceContracts.IPayrollBusiness.GETGROUP_EMPLOYEE_ID
            Try
                Return PayrollRepositoryStatic.Instance.GETGROUP_EMPLOYEE_ID(titleID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_EMP_FORMULER
            Try
                Return PayrollRepositoryStatic.Instance.InsertPA_EMP_FORMULER(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_EMP_FORMULER
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPA_EMP_FORMULER(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePA_EMP_FORMULER(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePA_EMP_FORMULER
            Try
                Return PayrollRepositoryStatic.Instance.DeletePA_EMP_FORMULER(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CheckPA_EMP_FORMULERExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal TitleID As Decimal, ByVal groupTitleID As Decimal, ByVal formulerID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPA_EMP_FORMULERExits
            Try
                Return PayrollRepositoryStatic.Instance.CheckPA_EMP_FORMULERExits(empID, orgID, TitleID, groupTitleID, formulerID, pDate, pID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetPA_SUM_CH_TOM(ByVal _filter As PA_SumCHTomDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_SumCHTomDTO) Implements ServiceContracts.IPayrollBusiness.GetPA_SUM_CH_TOM
            Try
                Return PayrollRepositoryStatic.Instance.GetPA_SUM_CH_TOM(_filter, PageIndex, PageSize, Total, Sorts, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function CAL_PA_SUM_CH_TOM(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_ISDISSOLVE As Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.CAL_PA_SUM_CH_TOM
            Try
                Return PayrollRepositoryStatic.Instance.CAL_PA_SUM_CH_TOM(P_PERIOD, P_ORG, P_ISDISSOLVE, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#Region "PA_FRAME_SALARY - Khung hệ số lương chức danh"
        Public Function GetFrameSalary(ByVal sACT As String) As List(Of PA_FRAME_SALARYDTO) Implements ServiceContracts.IPayrollBusiness.GetFrameSalary
            Try
                Return PayrollRepositoryStatic.Instance.GetFrameSalary(sACT)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertFrameSalary
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertFrameSalary(objOrganization, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateFrameSalary
            Using rep As New PayrollRepository
                Try

                    Return rep.ValidateFrameSalary(objOrganization)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetMaxId() As Decimal Implements ServiceContracts.IPayrollBusiness.GetMaxId
            Using rep As New PayrollRepository
                Try
                    Return rep.GetMaxId()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetNameFrameSalary(ByVal org_id As Decimal) As String Implements ServiceContracts.IPayrollBusiness.GetNameFrameSalary
            Using rep As New PayrollRepository
                Try
                    Return rep.GetNameFrameSalary(org_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFrameSalary
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyFrameSalary(objOrganization, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyFrameSalaryPath(ByVal lstPath As List(Of PA_FRAME_SALARY_PATHDTO)) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFrameSalaryPath
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyFrameSalaryPath(lstPath)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveFrameSalary(ByVal objOrganization() As PA_FRAME_SALARYDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveFrameSalary
            Using rep As New PayrollRepository
                Try

                    Return rep.ActiveFrameSalary(objOrganization, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "PA_FRAME_PRODUCTIVITY - Khung hệ số năng suất"
        Public Function GetFrame_Productivity(ByVal sACT As String) As List(Of PA_FRAME_PRODUCTIVITYDTO) Implements ServiceContracts.IPayrollBusiness.GetFrame_Productivity
            Try
                Return PayrollRepositoryStatic.Instance.GetFrame_Productivity(sACT)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertFrame_Productivity
            Try
                Dim lst = PayrollRepositoryStatic.Instance.InsertFrame_Productivity(objOrganization, log, gID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateFrame_Productivity
            Using rep As New PayrollRepository
                Try

                    Return rep.ValidateFrame_Productivity(objOrganization)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetMaxIdFrame_Productivity() As Decimal Implements ServiceContracts.IPayrollBusiness.GetMaxIdFrame_Productivity
            Using rep As New PayrollRepository
                Try
                    Return rep.GetMaxIdFrame_Productivity()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetNameFrame_Productivity(ByVal org_id As Decimal) As String Implements ServiceContracts.IPayrollBusiness.GetNameFrame_Productivity
            Using rep As New PayrollRepository
                Try
                    Return rep.GetNameFrame_Productivity(org_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFrame_Productivity
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyFrame_Productivity(objOrganization, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyFrame_ProductivityPath(ByVal lstPath As List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFrame_ProductivityPath
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyFrame_ProductivityPath(lstPath)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveFrame_Productivity(ByVal objOrganization() As PA_FRAME_PRODUCTIVITYDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveFrame_Productivity
            Using rep As New PayrollRepository
                Try

                    Return rep.ActiveFrame_Productivity(objOrganization, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace

