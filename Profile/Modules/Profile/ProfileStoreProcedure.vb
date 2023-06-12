Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class ProfileStoreProcedure
#Region "ChienNV"
    ''' <summary>
    ''' Create by: ChienNV; Create date:11/10/2017; Lấy danh sách đơn vị đóng bảo hiểm.
    ''' pStruct ="1" lấy dữ liệu , pStruct ="0" chỉ lấy cấu trúc của table
    ''' </summary>
    ''' <param name="pStruct"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListInsurance(Optional ByVal pStruct As String = "1") As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_INSURANCE", New List(Of Object)(New Object() {pStruct}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GetInsListRegion(Optional ByVal pStruct As String = "1") As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_INS_REGION", New List(Of Object)(New Object() {pStruct}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_ORG_LEVEL(Optional ByVal P_STRUCT As String = "1") As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_ORG_LEVEL", New List(Of Object)(New Object() {P_STRUCT}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_ALL_BRANCH_ORGLEVEL(ByVal P_ORGID As Decimal) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_ALL_BRANCH_ORGLEVEL", New List(Of Object)(New Object() {P_ORGID}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CREATE_NEW_EMPCODE() As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.CREATE_NEW_EMPCODE", Nothing)
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


    Public Function DEL_HSL(ByVal lstID As String) As Boolean
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.DEL_HSL", New List(Of Object)(New Object() {lstID}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                Dim dt = ds.Tables(0).Rows(0)("RESULT")
                If Decimal.Parse(dt) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function INSERT_INFO_REMINDER_DETAIL(ByVal username As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE_DASHBOARD.INSERT_INFO_REMINDER_DETAIL",
                                             New List(Of Object)(New Object() {username, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
    Public Function Get_List_Document(ByVal empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.Get_List_Document", New List(Of Object)(New Object() {empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function DELETE_INFO_REMINDER(ByVal P_ID As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE_DASHBOARD.DELETE_INFO_REMINDER",
                                             New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
    Public Function GET_DECISION_TYPE_EXCEPT_NV(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TYPE_EXCEPT_NV", New List(Of Object)(New Object() {isBlank, Common.Common.SystemLanguage.Name}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function get_org_name_c2(ByVal p_emp_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.get_org_name_c2", New List(Of Object)(New Object() {p_emp_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_list_Welfare_EMP(ByVal p_welfare_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.GetlistWelfareEMP", New List(Of Object)(New Object() {p_welfare_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GetTitle(ByVal titleId As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.HU_GET_TITLE_BY_ID", New List(Of Object)(New Object() {titleId}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GetTitleName(ByVal title_id As Int32) As String
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.HU_GET_TITLE_BY_ID", New List(Of Object)(New Object() {title_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0)(1)
            End If
        End If
        Return ""
    End Function

    Public Function CHECKEXIST_ALLOWANCE(ByVal EMP_ID As Decimal, ByVal ALLOWANCE_ID As Decimal, ByVal EFFECT_DATE As Date) As Int16
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.CHECKEXIST_ALLOWANCE", New List(Of Object)(New Object() {EMP_ID, ALLOWANCE_ID, EFFECT_DATE}))
        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0)(0)
            End If
        End If
    End Function

    Public Function get_current_work_history(ByVal p_empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_CURRENT_WORK_HISTORY", New List(Of Object)(New Object() {p_empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_WORKING_BEFORE(ByVal p_empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_WORKING_BEFORE", New List(Of Object)(New Object() {p_empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_WORKING_MISSION_NEW() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_WORKING_MISSION_NEW")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_WORKING_WAGE_NEW() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_WORKING_WAGE_NEW")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CONTRACT_NEW() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_CONTRACT_NEW")
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CURRENT_SALARY_HISTORY(ByVal p_empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_CURRENT_SALARY_HISTORY", New List(Of Object)(New Object() {p_empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_DATA_QDBN_AND_QDDC_DOC(ByVal p_working_id As Decimal, ByVal p_empid As Decimal) As DataSet
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_DATA_QDBN_AND_QDDC_DOC", New List(Of Object)(New Object() {p_working_id, p_empid}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_DECISION_TYPE() As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_DECISION_TYPE", New List(Of Object)(New Object() {}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Phê duyệt hợp đồng hàng loạt
    ''' </summary>
    ''' <param name="dtb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BatchApprovedContract(ByVal dtb As DataTable) As Integer
        Try
            Return hfr.ExecuteBatchCommand("PKG_HU_CONTRACT.APPROVED_CONTRACT", dtb)
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' Phê duyệt phụ lục hợp đồng hàng loạt
    ''' </summary>
    ''' <param name="dtb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BatchApprovedListContract(ByVal dtb As DataTable) As Integer
        Try
            Return hfr.ExecuteBatchCommand("PKG_PROFILE.APPROVED_FILECONTRACT", dtb)
        Catch ex As Exception

        End Try
    End Function
    Public Function Print_Decision(ByVal empID As Decimal, ByVal ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_DECISION", New List(Of Object)(New Object() {empID, ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Print_Contract(ByVal emp_code As String, ByVal contract_NO As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.RP_IPROFILE_HDLD", New List(Of Object)(New Object() {emp_code, contract_NO}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Print_FileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.RP_IPROFILE_PLHDLD", New List(Of Object)(New Object() {emp_code, fileContract_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Print_Terminate(ByVal id As Decimal, ByVal form_id As Decimal) As DataSet
        'Dim dt As New DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_TERMINATE", New List(Of Object)(New Object() {id, form_id}))
        If Not ds Is Nothing Or Not ds.Tables(1) Is Nothing Then
            ds.Tables(1).TableName = "DT1"
            ds.Tables(0).TableName = "DT"
        End If
        Return ds
    End Function

    Public Function Get_Decision_Type(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TYPE", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Decision_Terminate(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TERMINATE", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function PRINT_DISCIPLINE(ByVal empID As Decimal, ByVal ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_DISCIPLINE", New List(Of Object)(New Object() {empID, ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CHECK_WELFARE(ByVal Name As String, ByVal is_Edit As Integer, ByVal org_id As Decimal, ByVal sdate As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_WELFARE", New List(Of Object)(New Object() {Name, is_Edit, org_id, sdate}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_CONTRACT_PROCEDURE(ByVal EMP_ID As Decimal, ByVal START_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_CONTRACT_PROCEDURE", New List(Of Object)(New Object() {EMP_ID, START_DATE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function AUTOCREATE_CONTRACTNO(ByVal EMP_ID As Decimal, ByVal CONTRACT_TYPE As Decimal, ByVal LOCATION As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_CONTRACTNO2",
                                                   New List(Of Object)(New Object() {EMP_ID, CONTRACT_TYPE, LOCATION, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_STOCKNO(ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_STOCKNO",
                                                   New List(Of Object)(New Object() {START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_STOCK_TRANS_NO(ByVal STOCK_ID As Decimal) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_STOCK_TRANS_NO",
                                                   New List(Of Object)(New Object() {STOCK_ID, OUT_STRING}))
        Return obj(0).ToString()
    End Function

    Public Function GET_CODE_OT_OTHER_LIST(ByVal P_ID As Decimal) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_CODE_OT_OTHER_LIST",
                                                   New List(Of Object)(New Object() {P_ID, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_DECISIONNO2(ByVal USER_ID As Decimal, ByVal EMP_ID As Decimal, ByVal P_DECISION_TYPE As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_DECISIONNO2",
                                                   New List(Of Object)(New Object() {USER_ID, EMP_ID, P_DECISION_TYPE, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_DISCIPLINENO(ByVal ORG_ID As Decimal, ByVal USER_ID As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_DISCIPLINENO",
                                                   New List(Of Object)(New Object() {ORG_ID, USER_ID, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_COMMENDNO(ByVal USER_ID As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_COMMENDNO",
                                                   New List(Of Object)(New Object() {USER_ID, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_DECISIONNO(ByVal EMP_ID As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_DECISIONNO",
                                                   New List(Of Object)(New Object() {EMP_ID, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_FILECONTRACTNO(ByVal EMP_ID As Decimal, ByVal CONTRACT_ID As Decimal, ByVal CONTRACT_TYPE As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_FILECONTRACTNO2",
                                                   New List(Of Object)(New Object() {EMP_ID, CONTRACT_ID, CONTRACT_TYPE, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function

    Public Function AUTOCREATE_CONCURRENTLYNO(ByVal EMP_ID As Decimal, ByVal USER_ID As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_CONCURRENTLYNO",
                                                   New List(Of Object)(New Object() {EMP_ID, USER_ID, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_CONCURRENTLYNO2(ByVal EMP_ID As Decimal, ByVal USER_ID As Decimal, ByVal START_DATE As Date) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_CONCURRENTLYNO2",
                                                   New List(Of Object)(New Object() {EMP_ID, USER_ID, START_DATE, OUT_STRING}))
        Return obj(0).ToString()
    End Function
    Public Function AUTOCREATE_WORKINGNO(ByVal EMP_ID As Decimal) As String
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.AUTOCREATE_WORKINGNO",
                                                   New List(Of Object)(New Object() {EMP_ID, OUT_STRING}))
        Return obj(0).ToString()
    End Function

    Public Function AUTOCREATE_NOTIFY_NO(ByVal EMP_ID As Decimal, ByVal START_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.AUTOCREATE_NOTIFY_NO", New List(Of Object)(New Object() {EMP_ID, START_DATE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function CAL_DEBT_EMP(ByVal EMP_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CAL_DEBT_EMP", New List(Of Object)(New Object() {EMP_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function UPDATE_STATUS_UNLOCK_TERMINATE(ByVal EMP_ID As Decimal) As Boolean

        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.UPDATE_STATUS_UNLOCK_TERMINATE", New List(Of Object)(New Object() {EMP_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            Dim dt = ds.Tables(0).Rows(0)("RESULT")
            If Decimal.Parse(dt) = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function CHECK_SETTLENMENT(ByVal EMP_ID As Decimal) As Boolean

        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_SETTLENMENT", New List(Of Object)(New Object() {EMP_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            Dim dt = ds.Tables(0).Rows(0)("RESULT")
            If Decimal.Parse(dt) > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function


    Public Function CAL_TRUYTHU_BHYT(ByVal EMP_ID As Decimal, ByVal START_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.CAL_TRUYTHU_BHYT", New List(Of Object)(New Object() {EMP_ID, START_DATE}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_TRUYTHU_BHXH(ByVal EMP_ID As Decimal, ByVal P_TruyThu_BHYT As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_TRUYTHU_BHXH", New List(Of Object)(New Object() {EMP_ID, P_TruyThu_BHYT}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function CAL_QT(ByVal EMP_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.CAL_QT", New List(Of Object)(New Object() {EMP_ID}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function


    Public Function GET_ORGNAME_2(ByVal P_ORG_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_ORGNAME_2", New List(Of Object)(New Object() {P_ORG_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_DATA_IMPORT_FAMILY() As DataSet
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_DATA_IMPORT_FAMILY", New List(Of Object)(New Object() {}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IMPORT_DATA_FAMILY(ByVal P_DOCXML As String) As Boolean
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.IMPORT_DATA_FAMILY", New List(Of Object)(New Object() {P_DOCXML}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                Dim dt = ds.Tables(0).Rows(0)("RES")
                If Decimal.Parse(dt) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IMPORT_DATA_WORKING_BEFORE(ByVal P_DOCXML As String) As Boolean
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.IMPORT_DATA_WORKING_BEFORE", New List(Of Object)(New Object() {P_DOCXML}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                Dim dt = ds.Tables(0).Rows(0)("RES")
                If Decimal.Parse(dt) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Commend"
    Public Function Get_Commend_Period(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_PERIOD_COMMEND", New List(Of Object)(New Object() {isBlank, year}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_PowerPay(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_POWERPAY_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_List(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_LIST_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Level(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_LEVEL_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Formality(ByVal isBlank As Boolean, ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_FORMALITY_COMMEND", New List(Of Object)(New Object() {isBlank, OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Title(ByVal isBlank As Boolean, ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_TITLE_COMMEND", New List(Of Object)(New Object() {isBlank, OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_PAY_POWER(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_PAY_POWER", New List(Of Object)(New Object() {isBlank, year}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    ''' <summary>
    ''' Load dữ liệu khen thuong đã Imported
    ''' </summary>
    ''' <param name="userName">Lấy nhân viên được phân quyền theo UserName</param>
    ''' <param name="orgID">Org ID</param>
    ''' <returns>Danh sách khen thuong đã import</returns>
    ''' <remarks></remarks>
    Public Function ReadDataForGridViewDataImported(ByVal userName As String, ByVal orgID As Integer, ByVal commendDate As Date, ByVal index As Integer, ByVal obj As Integer) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.READ_DATA_IMPORTED", New List(Of Object)({userName, orgID, commendDate, index, obj}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Dim dtb As New DataTable
            dtb.Columns.Add("EMPLOYEE_ID", GetType(Integer))
            dtb.Columns.Add("FULLNAME_CODE", GetType(String))
            dtb.Columns.Add("FULLNAME_VN", GetType(String))
            dtb.Columns.Add("ORG_ID", GetType(Integer))
            dtb.Columns.Add("ORG_NAME", GetType(String))
            Return dtb
        End If
    End Function
    Public Function CreateDataSalaryImportToDatabase(ByVal dtb As DataTable) As Boolean
        Dim rowInsert As Integer = dtb.Rows.Count
        Dim result As Integer = 0

        result = hfr.ExecuteBatchCommand(" " + "CREATE_HU_IMPORT_COMMEND", dtb)

        If rowInsert = result Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GET_COMMEND_LIST_IMPORT(ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_COMMEND_LIST_IMPORT", New List(Of Object)(New Object() {OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

#End Region

#Region "Xét duyệt khen thưởng - THANHNT 25/08/2016"

    Public Function ReadDataCommendImported(ByVal orgID As Integer, ByVal userName As String, ByVal commendType As Decimal, ByVal DateReview As String, _
                                            ByVal isAll As Decimal) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND_CALCULATE.READ_COMMEND_CALCULATED", New List(Of Object)(New Object() {orgID, userName, commendType, DateReview, isAll}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function ReadListCommendDate(ByVal orgID As Integer, ByVal userName As String, ByVal obj_id As Decimal) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND_CALCULATE.READ_LIST_COMMEND_DATE", New List(Of Object)(New Object() {orgID, userName, obj_id}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    'Public Function CalculateCommend(ByVal orgID As Integer, ByVal userName As String, ByVal year As Decimal, ByVal CommendType As Decimal) As Boolean
    '    Dim obj = hfr.ExecuteStoreScalar("PKG_HU_COMMEND_CALCULATE.CALCULATED_COMMEND", New List(Of Object)(New Object() {orgID, userName, year, CommendType, HistaffFrameworkPublic.FrameworkUtilities.OUT_NUMBER}))
    '    If obj Is Nothing Then
    '        Return If(Decimal.Parse(obj(0).ToString()) = 1, True, False)
    '    Else
    '        Return False
    '    End If
    'End Function

#End Region

#Region "Organization"

    ''' <summary>
    ''' Lấy danh sách Sơ đồ tổ chức có cấp Công ty
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_ORGID_COMPANY_LEVEL() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORGID_COMPANY_LEVEL", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_ORGID_COMPANY_LEVEL_USER_ID(ByVal user_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORGID_COMPANY_LEVEL_USER_ID", New List(Of Object)(New Object() {user_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    ''' <summary>
    ''' Lấy danh sách Loại tổ chức
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_ORG_TYPE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORG_TYPE", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
#End Region

#Region "Terminate-Nghỉ việc"
    Public Function CHECK_TER_EMPEXIST(ByVal TerId As Decimal, ByVal EmpId As Decimal) As Boolean
        Dim _rs As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_TER_EMPEXIST", New List(Of Object)(New Object() {TerId, EmpId}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                _rs = True
            Else
                _rs = False
            End If
        Else
            _rs = False
        End If
        Return _rs
    End Function
#End Region

    Public Function GET_FUNCTION_SIGN(Optional ByVal IS_BLANK As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_FUNCTION_SIGN", New List(Of Object)(New Object() {IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_SIGNER_BY_FUNC(ByVal P_FUNC_NAME As String, ByVal P_DATE As Date, Optional ByVal P_ORG_ID As Decimal = 1, Optional ByVal P_SETUP_TYPE As Decimal? = Nothing) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_SIGNER_BY_FUNC", New List(Of Object)(New Object() {P_FUNC_NAME, P_DATE, P_SETUP_TYPE, P_ORG_ID}))
        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_ORG_BY_EMPCODE(ByVal P_CODE As String, ByVal P_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_ORG_BY_EMPCODE", New List(Of Object)(New Object() {P_CODE, P_DATE}))
        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function UPDATE_STATUS_CONCURRENTHLY(ByVal IDs As String, ByVal strType As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_CONCURRENTLY.UPDATE_STATUS_CONCURRENTHLY", New List(Of Object)(New Object() {IDs, strType}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function APPROVE_CONCURRENTHLY(ByVal IDs As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_CONCURRENTLY.APPROVE_CONCURRENTHLY", New List(Of Object)(New Object() {IDs}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_EMP_PER_INFO(ByVal P_EMP_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_EMP_PER_INFO", New List(Of Object)(New Object() {P_EMP_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function EXPORT_SIGN_SETUP_DATA() As DataSet
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.EXPORT_SIGN_SETUP_DATA")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function IMPORT_SIGN_SETUP(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.IMPORT_SIGN_SETUP", New List(Of Object)(New Object() {P_DOCXML, P_USER}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function

    Public Function CAL_REMAINING_LEAVE(ByVal P_EMPID As Integer, ByVal P_LASTDATE As Date) As Integer
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE.CAL_REMAINING_LEAVE",
                                             New List(Of Object)(New Object() {P_EMPID, P_LASTDATE, OUT_NUMBER}))
        Return CDec(objects(0).ToString())
    End Function

    Public Function CAL_REMAINING_LEAVE_HAVE(ByVal P_EMPID As Integer, ByVal P_YEAR As Integer) As Decimal
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE.CAL_REMAINING_LEAVE_HAVE",
                                             New List(Of Object)(New Object() {P_EMPID, P_YEAR, OUT_NUMBER}))
        Return CDec(objects(0).ToString())
    End Function

    Public Function UPDATE_TERMINATE_COPY_STATUS(ByVal P_ID As Decimal) As Integer
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE.UPDATE_TERMINATE_COPY_STATUS",
                                             New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return Integer.Parse(objects(0).ToString())
    End Function
    Public Function GET_MONEY_BY_CHUCVU(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_CONCURRENTLY.GET_MONEY_BY_CHUCVU", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function DEL_HU_SALARY(ByVal lstID As String) As Boolean
        Try
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.DEL_HU_SALARY", New List(Of Object)(New Object() {lstID}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                Dim dt = ds.Tables(0).Rows(0)("RESULT")
                If Decimal.Parse(dt) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_MIN_AMOUNT(ByVal P_EMPID As Integer, ByVal P_YEAR As Date) As Decimal
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_MIN_AMOUNT",
                                             New List(Of Object)(New Object() {P_EMPID, P_YEAR, OUT_NUMBER}))
        Return Integer.Parse(objects(0).ToString())
    End Function

    Public Function GET_INFO_SALARYITEMSPERCENT(ByVal P_ORG As Decimal, ByVal P_EFFECT_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_INFO_SALARYITEMSPERCENT", New List(Of Object)(New Object() {P_ORG, P_EFFECT_DATE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_INFO_SALARYITEMSPERCENT2(ByVal P_ORG As Decimal, ByVal P_EFFECT_DATE As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_INFO_SALARYITEMSPERCENT2", New List(Of Object)(New Object() {P_ORG, P_EFFECT_DATE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
End Class
