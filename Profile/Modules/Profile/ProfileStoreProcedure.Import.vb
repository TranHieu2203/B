Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class ProfileStoreProcedure

    Private hfr As New HistaffFrameworkRepository

    Public Function SaveEmployeeInfoToDB(ByVal dtb As DataTable) As Integer
        Return hfr.ExecuteBatchCommand("PKG_HU_IPROFILE.PRO_CREATE_HU_EMPLOYEE_IMPORT", dtb)
    End Function

    Public Function EmployeeInfoProcess(ByVal userName As String) As String
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.PRO_HU_EMPLOYEE_IMPORT_PROCESS", New List(Of Object)(New Object() {userName, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
    Public Function Imp_Allowance_List(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.Imp_Allowance_List", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
    Public Function CheckInsertAllowance(ByVal pEmpId As String, ByVal pEFFECT_DATE As String, ByVal pId As String)
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.Check_ImportAllowance", New List(Of Object)(New Object() {pEmpId, pEFFECT_DATE, pId}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0)(0)(0) > 0 Then
                Return True
            Else : Return False
            End If
        Else : Return False
        End If
    End Function
    Public Function SaveSalaryInfoToDB(ByVal dtb As DataTable) As Integer
        Return hfr.ExecuteBatchCommand("PKG_HU_IPROFILE.PRO_CREATE_HU_SALARY_IMPORT", dtb)
    End Function

    Public Function SalaryInfoProcess(ByVal userName As String) As String
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.PRO_HU_SALARY_IMPORT_PROCESS", New List(Of Object)(New Object() {userName, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
    Public Function GetTemplateImport() As DataSet
        Return hfr.ExecuteToDataSet("PKG_HU_IPROFILE.PRO_GET_TEMPLATE_EMPLOYEE_LIST", Nothing)
    End Function
    Public Function GetTemplateImportSalary(ByVal username As String, ByVal org_id As Decimal) As DataSet
        Return hfr.ExecuteToDataSet("PKG_HU_IPROFILE.PRO_GET_TEMPLATE_SALARY_LIST", New List(Of Object)(New Object() {username, org_id}))
    End Function
    Public Function SaveImportNonAlwaysWelfare(ByVal dtb As DataTable) As Integer
        Return hfr.ExecuteBatchCommand("PKG_HU_IPROFILE.PRO_CREATE_NONALWAYSWELFARE_IMPORT", dtb)
    End Function

    Public Function CreateSequence() As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.CREATE_SEQUENCE", New List(Of Object)(New Object() {OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function

    Public Function SUM_COST_REIMBURSE(ByVal empID As Decimal) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.TER_GETCOST_REIMBURSE", New List(Of Object)(New Object() {empID, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function

    Public Function GET_ALLOWED_FIVE(ByVal empID As Decimal, ByVal month As Decimal, ByVal year As Decimal) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_ALLOWED_FIVE", New List(Of Object)(New Object() {empID, month, year, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function
    Public Function GET_PERIOD(ByVal P_DATE As Date) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_PERIOD", New List(Of Object)(New Object() {P_DATE, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function
    Public Function GET_INFOR_INS_FROMMONTH(ByVal empID As Decimal) As Date
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_INFOR_INS", New List(Of Object)(New Object() {empID, OUT_DATE}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return Nothing
        End If
    End Function
    Public Function SALARY_LAST_SIX_MONTH(ByVal empID As Decimal, ByVal effdate As Date) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.HU_SUM_SALARY_LAST_SIX_MONTH", New List(Of Object)(New Object() {empID, effdate, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function
    Public Function GET_DECISION_CODE() As Decimal
        Try
            Dim lstObj As New List(Of Object)()
            lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.GET_DECISION_CODE", New List(Of Object)(New Object() {OUT_NUMBER}))
            If lstObj IsNot Nothing Then
                Return lstObj(0)
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function GET_CONTRACTTYPE_CODE() As Decimal
        Try
            Dim lstObj As New List(Of Object)()
            lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.GET_CONTRACTTYPE_CODE", New List(Of Object)(New Object() {OUT_NUMBER}))
            If lstObj IsNot Nothing Then
                Return lstObj(0)
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function COUNT_FORM(ByVal empID As Decimal, ByVal formID As Decimal) As Decimal
        Try
            Dim lstObj As New List(Of Object)()
            lstObj = hfr.ExecuteStoreScalar("PKG_HU_CONTRACT.COUNT_FORM", New List(Of Object)(New Object() {empID, formID, OUT_NUMBER}))
            If lstObj IsNot Nothing Then
                Return lstObj(0)
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function GET_ID_ORG() As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.GET_ID_ORG", New List(Of Object)(New Object() {OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
    Public Function GET_FORM_CODE(ByVal ID As Decimal) As String
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.GET_FORM_CODE", New List(Of Object)(New Object() {ID, OUT_STRING}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
    Public Function GET_FORM_NAME(ByVal ID As Decimal) As String
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.GET_FORM_NAME", New List(Of Object)(New Object() {ID, OUT_STRING}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return String.Empty
        End If
    End Function
    Public Function GetTemplateImportHealthy(ByVal emp_id As Decimal) As DataSet
        Return hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_INFOR_HEALTHY", New List(Of Object)(New Object() {emp_id}))
    End Function

    Public Function SaveInforHealthy(ByVal dtb As DataTable) As Integer
        Return hfr.ExecuteBatchCommand("PKG_HU_IPROFILE.INSERT_INFOR_HEALTHY", dtb)
    End Function

    Public Function GET_INS_THAI_SAN(ByVal emp_id As Decimal, ByVal last_date As Date) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_INS_THAI_SAN", New List(Of Object)(New Object() {emp_id, last_date, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function

    Public Function GET_SO_NAM_TRO_CAP(ByVal emp_id As Decimal, ByVal TYPE As Decimal, ByVal last_Date As Date) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.TINH_SO_NAM_TRO_CAP", New List(Of Object)(New Object() {emp_id, TYPE, last_Date, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function

    Public Function GET_NGHI_KHONG_LUONG(ByVal emp_id As Decimal) As Decimal
        Dim lstObj As New List(Of Object)()
        lstObj = hfr.ExecuteStoreScalar("PKG_PROFILE.GET_NGHI_KHONG_LUONG", New List(Of Object)(New Object() {emp_id, OUT_NUMBER}))
        If lstObj IsNot Nothing Then
            Return lstObj(0)
        Else
            Return 0
        End If
    End Function


#Region "PLHD"
    Public Function COUNT_PLGHHD(ByVal empID As Decimal, ByVal appendTypeId As Decimal) As Decimal
        Try
            Dim lstObj As New List(Of Object)()
            lstObj = hfr.ExecuteStoreScalar("PKG_HU_CONTRACT.COUNT_FORM", New List(Of Object)(New Object() {empID, appendTypeId, OUT_NUMBER}))
            If lstObj IsNot Nothing Then
                Return lstObj(0)
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function
#End Region

    Public Function GET_OTHER_LIST_DSVM(ByVal ORG_ID As Decimal, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_ISDISSOLVE As Boolean, ByVal P_USER As String) As DataSet
        Return hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_OTHER_LIST_DSVM", New List(Of Object)(New Object() {P_USER, ORG_ID, P_START_DATE, P_END_DATE, P_ISDISSOLVE}))
    End Function
    Public Function GET_HU_ORGANIZATION() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_HU_ORGANIZATION", New List(Of Object)(New Object() {}))
    End Function
End Class