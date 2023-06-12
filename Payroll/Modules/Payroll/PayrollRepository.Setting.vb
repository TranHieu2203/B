Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "HoldSalary"

    Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)
        Dim lstHoldSalary As List(Of PAHoldSalaryDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstHoldSalary = rep.GetHoldSalaryList(PeriodId, OrgId, IsDissolve, Me.Log, PageIndex, PageSize, Total, Sorts)
                Return lstHoldSalary
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertHoldSalary(objPeriod, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteHoldSalary(lstDelete)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SALARYTYPE_GROUP"

    Public Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryType_Group(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryType_Group(objSalaryType_Group, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryType_Group(objSalaryType_Group, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryType_Group(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryType_Group(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal bActive As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryType_GroupStatus(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryType_Group(ByVal lstSalaryType_Group As List(Of SalaryType_GroupDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryType_Group(lstSalaryType_Group)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "SalaryPlanning"

    Public Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanning(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanningByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryPlanning(objSalaryPlanning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ImportSalaryPlanning(ByVal dtData As DataTable, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ImportSalaryPlanning(dtData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryPlanning(objSalaryPlanning, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryPlanning(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                dtData = rep.GetTitleByOrgList(orgID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryPlanningImport(ByVal org_id As Decimal) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryPlanningImport(org_id, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_EXPORT_PA_EMP_FORMULER(ByVal org_id As Decimal, ByVal IS_DISSOLVE As Decimal) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_EXPORT_PA_EMP_FORMULER(org_id, IS_DISSOLVE, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "SalaryTracker"

    Public Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryTracker(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO) As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryEmpTracker(_filter, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "PA_SETUP_LDTT_NV_QLCH"
    Public Function GET_PA_SETUP_LDTT_NV_QLCH(ByVal _filter As PA_SETUP_LDTTT_NV_QLCH_DTO,
                              ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)
        Dim lstPA_SETUP_LDTT_NV_QLCH As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_LDTT_NV_QLCH = rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_LDTT_NV_QLCH
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertPA_SETUP_LDTTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPA_SETUP_LDTT_NV_QLCH(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPA_SETUP_LDTTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPA_SETUP_LDTTT_NV_QLCH(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePA_SETUP_LDTT_NV_QLCH(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePA_SETUP_LDTT_NV_QLCH(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidatePA_SETUP_LDTT_NV_QLCH(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_LDTT_NV_QLCH(ByVal _filter As PA_SETUP_LDTTT_NV_QLCH_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)
        Dim lst As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Get_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function IMPORT_PA_SETUP_LDTT_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_SETUP_LDTT_NV_QLCH(P_DOCXML, P_USER)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "PA_Setup_HeSoMR_NV_QLCH"
    Public Function GET_PA_SETUP_HESOMR_NV_QLCH(ByVal _filter As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)
        Dim lstPA_SETUP_HESOMR_NV_QLCH As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_HESOMR_NV_QLCH = rep.GET_PA_SETUP_HESOMR_NV_QLCH(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_HESOMR_NV_QLCH
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SETUP_HESOMR_NV_QLCH(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SETUP_HESOMR_NV_QLCH(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_SETUP_HESOMR_NV_QLCH(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SETUP_HESOMR_NV_QLCH(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_SETUP_HESOMR_NV_QLCH(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_HESOMR_NV_QLCH(ByVal _filter As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)
        Dim lst As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_HESOMR_NV_QLCH(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PA_SETUP_INDEX"
    Public Function GET_PA_SETUP_INDEX(ByVal _filter As PA_SETUP_INDEX_DTO,
                              ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_INDEX_DTO)
        Dim lstPA_SETUP_INDEX As List(Of PA_SETUP_INDEX_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_INDEX = rep.GET_PA_SETUP_INDEX(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_INDEX
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SETUP_INDEX(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SETUP_INDEX(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_SETUP_INDEX(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SETUP_INDEX(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_SETUP_INDEX(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_INDEX(ByVal _filter As PA_SETUP_INDEX_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_INDEX_DTO)
        Dim lst As List(Of PA_SETUP_INDEX_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_INDEX(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function EXPORT_PA_SETUP_INDEX() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.EXPORT_PA_SETUP_INDEX(Common.Common.SystemLanguage.Name)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function IMPORT_PA_SETUP_INDEX(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_PA_SETUP_INDEX(P_DOCXML, P_USER)
            Catch ex As Exception
                rep.Abort()
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
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HSTDT_DTO)
        Dim lstPA_SETUP_INDEX As List(Of PA_SETUP_HSTDT_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_INDEX = rep.GET_PA_SETUP_HSTDT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_INDEX
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SETUP_HSTDT(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SETUP_HSTDT(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_SETUP_HSTDT(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SETUP_HSTDT(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_SETUP_HSTDT(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_HSTDT(ByVal _filter As PA_SETUP_HSTDT_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HSTDT_DTO)
        Dim lst As List(Of PA_SETUP_HSTDT_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_HSTDT(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PA_SETUP_MR_BQN"
    Public Function GET_PA_SETUP_MR_BQN(ByVal _filter As PA_SETUP_MR_BQN_DTO,
                              ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_MR_BQN_DTO)
        Dim lstPA_SETUP_MR_BQN As List(Of PA_SETUP_MR_BQN_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_MR_BQN = rep.GET_PA_SETUP_MR_BQN(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_MR_BQN
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SETUP_MR_BQN(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SETUP_MR_BQN(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_SETUP_MR_BQN(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SETUP_MR_BQN(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_SETUP_MR_BQN(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_MR_BQN(ByVal _filter As PA_SETUP_MR_BQN_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_MR_BQN_DTO)
        Dim lst As List(Of PA_SETUP_MR_BQN_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_MR_BQN(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "PA_SETUP_RATE_DTTT"
    Public Function GET_PA_SETUP_RATE_DTTT(ByVal _filter As PA_SETUP_RATE_DTTT_DTO,
                              ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTTT_DTO)
        Dim lstPA_SETUP_RATE_DTTT As List(Of PA_SETUP_RATE_DTTT_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lstPA_SETUP_RATE_DTTT = rep.GET_PA_SETUP_RATE_DTTT(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPA_SETUP_RATE_DTTT
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.INSERT_PA_SETUP_RATE_DTTT(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function MODIFY_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.MODIFY_PA_SETUP_RATE_DTTT(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DELETE_PA_SETUP_RATE_DTTT(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DELETE_PA_SETUP_RATE_DTTT(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function VALIDATE_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.VALIDATE_PA_SETUP_RATE_DTTT(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_PA_SETUP_RATE_DTTT(ByVal _filter As PA_SETUP_RATE_DTTT_DTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTTT_DTO)
        Dim lst As List(Of PA_SETUP_RATE_DTTT_DTO)

        Using rep As New PayrollBusinessClient
            Try
                lst = rep.GET_PA_SETUP_RATE_DTTT(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Setup NKL"
    Public Function GetListPaSetupNKL(ByVal _filter As PA_SETUP_NKLDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_NKLDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListPaSetupNKL(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaSetupNKL(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaSetupNKL(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePaSetupNKL(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaSetupNKL(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidatePaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidatePaSetupNKL(obj)
            Catch ex As Exception
                rep.Abort()
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
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_CLASSIFICATIONDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPaClassifications(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertPaClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertPaClassification(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyPaClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyPaClassification(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletePaClassification(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeletePaClassification(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateClassification(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
End Class
