Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "PA_SETUP_LDTT_NV_QLCH"
        Public Function GET_PA_SETUP_LDTT_NV_QLCH(ByVal _filter As PA_SETUP_LDTTT_NV_QLCH_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_LDTT_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertPA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_SETUP_LDTT_NV_QLCH
            Using rep As New PayrollRepository
                Try
                    Return rep.InsertPA_SETUP_LDTT_NV_QLCH(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPA_SETUP_LDTTT_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyPA_SETUP_LDTT_NV_QLCH(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeletePA_SETUP_LDTT_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DeletePA_SETUP_LDTT_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.DeletePA_SETUP_LDTT_NV_QLCH(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidatePA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ValidatePA_SETUP_LDTT_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.ValidatePA_SETUP_LDTT_NV_QLCH(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT() As DataSet _
            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT
            Using rep As New PayrollRepository
                Try

                    Return rep.GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_PA_SETUP_LDTT_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_SETUP_LDTT_NV_QLCH
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_PA_SETUP_LDTT_NV_QLCH(P_DOCXML, P_USER)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "PA_SETUP_INDEX"
        Public Function GET_PA_SETUP_INDEX(ByVal _filter As PA_SETUP_INDEX_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_INDEX_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_INDEX(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_SETUP_INDEX(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_SETUP_INDEX(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_SETUP_INDEX(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_SETUP_INDEX(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_SETUP_INDEX(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function EXPORT_PA_SETUP_INDEX(ByVal sLang As String) As DataSet _
            Implements ServiceContracts.IPayrollBusiness.EXPORT_PA_SETUP_INDEX
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.EXPORT_PA_SETUP_INDEX(sLang)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function IMPORT_PA_SETUP_INDEX(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.IMPORT_PA_SETUP_INDEX
            Using rep As New PayrollRepository
                Try
                    Return rep.IMPORT_PA_SETUP_INDEX(P_DOCXML, P_USER)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "PA_SETUP_HSTDT"
        Public Function GET_PA_SETUP_HSTDT(ByVal _filter As PA_SETUP_HSTDT_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HSTDT_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_HSTDT
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_HSTDT(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SETUP_HSTDT
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_SETUP_HSTDT(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SETUP_HSTDT
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_SETUP_HSTDT(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_SETUP_HSTDT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SETUP_HSTDT
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_SETUP_HSTDT(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_SETUP_HSTDT
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_SETUP_HSTDT(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Hold Salary"

        Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetHoldSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertHoldSalary(objPeriod, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteHoldSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteHoldSalary(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SALARYTYPE_GROUP"
        Public Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryType_Group
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryType_Group(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryType_Group(objSalaryType_Group, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryType_Group(objSalaryType_Group, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO) Implements ServiceContracts.IPayrollBusiness.ValidateSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryType_Group(_validate)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSalaryType_Group
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryType_Group(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryType_GroupStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteSalaryType_GroupStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryType_Group(ByVal lstSalaryType_Group() As SalaryType_GroupDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryType_Group
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryType_Group(lstSalaryType_Group)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "SalaryPlanning"

        Public Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                          ByVal _param As ParamDTO,
                                          Optional ByVal log As UserLog = Nothing) As DataTable _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanning(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanningByID
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanningByID(_filter)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ImportSalaryPlanning(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ImportSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.ImportSalaryPlanning(dtData, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.InsertSalaryPlanning(objSalaryPlanning, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.ModifySalaryPlanning(objSalaryPlanning, log, gID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryPlanning
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.DeleteSalaryPlanning(lstID)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetTitleByOrgList
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetTitleByOrgList(orgID, sLang, isBlank)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSalaryPlanningImport(ByVal org_id As Decimal, ByVal log As UserLog) As DataSet _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryPlanningImport
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryPlanningImport(org_id, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GET_EXPORT_PA_EMP_FORMULER(ByVal org_id As Decimal, ByVal IS_DISSOLVE As Decimal, ByVal log As UserLog) As DataSet _
            Implements ServiceContracts.IPayrollBusiness.GET_EXPORT_PA_EMP_FORMULER
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GET_EXPORT_PA_EMP_FORMULER(org_id, IS_DISSOLVE, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
#Region "PA_Setup_HeSoMR_NV_QLCH"
        Public Function GET_PA_SETUP_HESOMR_NV_QLCH(ByVal _filter As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_HESOMR_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_HESOMR_NV_QLCH(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SETUP_HESOMR_NV_QLCH
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_SETUP_HESOMR_NV_QLCH(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SETUP_HESOMR_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_SETUP_HESOMR_NV_QLCH(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_SETUP_HESOMR_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SETUP_HESOMR_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_SETUP_HESOMR_NV_QLCH(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_SETUP_HESOMR_NV_QLCH
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_SETUP_HESOMR_NV_QLCH(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "SalaryTracker"

        Public Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryTracker(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet _
                                    Implements ServiceContracts.IPayrollBusiness.GetSalaryEmpTracker
            Try
                Using rep As New PayrollRepository
                    Try
                        Return rep.GetSalaryEmpTracker(_filter, _param, log)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "PA_SETUP_MR_BQN"
        Public Function GET_PA_SETUP_MR_BQN(ByVal _filter As PA_SETUP_MR_BQN_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_MR_BQN_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_MR_BQN
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_MR_BQN(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SETUP_MR_BQN
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_SETUP_MR_BQN(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SETUP_MR_BQN
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_SETUP_MR_BQN(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_SETUP_MR_BQN(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SETUP_MR_BQN
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_SETUP_MR_BQN(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_SETUP_MR_BQN
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_SETUP_MR_BQN(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "PA_SETUP_RATE_DTTT"
        Public Function GET_PA_SETUP_RATE_DTTT(ByVal _filter As PA_SETUP_RATE_DTTT_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTTT_DTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GET_PA_SETUP_RATE_DTTT
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GET_PA_SETUP_RATE_DTTT(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.INSERT_PA_SETUP_RATE_DTTT
            Using rep As New PayrollRepository
                Try
                    Return rep.INSERT_PA_SETUP_RATE_DTTT(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function MODIFY_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.MODIFY_PA_SETUP_RATE_DTTT
            Using rep As New PayrollRepository
                Try

                    Return rep.MODIFY_PA_SETUP_RATE_DTTT(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_PA_SETUP_RATE_DTTT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DELETE_PA_SETUP_RATE_DTTT
            Using rep As New PayrollRepository
                Try

                    Return rep.DELETE_PA_SETUP_RATE_DTTT(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function VALIDATE_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.VALIDATE_PA_SETUP_RATE_DTTT
            Using rep As New PayrollRepository
                Try

                    Return rep.VALIDATE_PA_SETUP_RATE_DTTT(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Setup NKL"
        Public Function GetListPaSetupNKL(ByVal _filter As PA_SETUP_NKLDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_NKLDTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GetListPaSetupNKL
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetListPaSetupNKL(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaSetupNKL
            Using rep As New PayrollRepository
                Try
                    Return rep.InsertPaSetupNKL(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaSetupNKL
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyPaSetupNKL(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeletePaSetupNKL(ByVal lstID As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.DeletePaSetupNKL
            Using rep As New PayrollRepository
                Try

                    Return rep.DeletePaSetupNKL(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidatePaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ValidatePaSetupNKL
            Using rep As New PayrollRepository
                Try

                    Return rep.ValidatePaSetupNKL(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Classification"
        Public Function GetPaClassifications(ByVal _filter As PA_CLASSIFICATIONDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_CLASSIFICATIONDTO) _
                                            Implements ServiceContracts.IPayrollBusiness.GetPaClassifications
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetPaClassifications(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaClassification
            Using rep As New PayrollRepository
                Try
                    Return rep.InsertPaClassification(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaClassification
            Using rep As New PayrollRepository
                Try

                    Return rep.ModifyPaClassification(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeletePaClassification(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaClassification
            Using rep As New PayrollRepository
                Try

                    Return rep.DeletePaClassification(lstID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateClassification
            Using rep As New PayrollRepository
                Try

                    Return rep.ValidateClassification(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace

