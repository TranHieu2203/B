Imports HistaffFrameworkPublic

Partial Class PayrollStoreProcedure
    Dim hfr As New HistaffFrameworkRepository

    Public Function GetTemplateImportDTTD_DTPB(ByVal username As String, ByVal org_id As Decimal, Optional ByVal IS_DISSOLVE As Boolean = False) As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_DTTD_DTPB_DATA_IMPORT", New List(Of Object)(New Object() {username, org_id, IS_DISSOLVE}))
    End Function

    Public Function GET_PERIOD_BY_MONTH(ByVal YEAR As Decimal, ByVal MONTH As Decimal) As DataTable
        Dim dtData As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_PERIOD_BY_MONTH", New List(Of Object)(New Object() {YEAR, MONTH}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                dtData = ds.Tables(0)
            End If
        End If
        Return dtData
    End Function

    Public Function GET_EMP_SALARY(ByVal empID As Decimal, ByVal periodID As String) As DataTable
        Dim dtData As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_EMPLOYEE_SALARY", New List(Of Object)(New Object() {empID, periodID}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
            Return ds.Tables(0)
        End If
        Return dtData
    End Function
    Public Function GET_TARGET_GROUP_CODE(ByVal id As Decimal) As DataTable
        Dim dtData As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_TARGET_GROUP_CODE", New List(Of Object)(New Object() {id}))
        Return ds.Tables(0)
    End Function

    Public Function GET_STORE_CODE_PERIOD_EXITS(ByVal STORE_ID As Decimal, ByVal PERIOD_ID As Decimal) As Decimal
        Dim dtData As New DataTable
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_STORE_CODE_PERIOD_EXITS", New List(Of Object)(New Object() {STORE_ID, PERIOD_ID}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function GetPeriodByNameAndYear(ByVal year As Decimal, ByVal name As String) As DataTable
        Dim dtData As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_PERIOD_BY_MONTH_AND_NAME", New List(Of Object)(New Object() {year, name}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                dtData = ds.Tables(0)
            End If
        End If
        Return dtData
    End Function
    Public Function GetPeriodNameAndYear(ByVal isBlank As Decimal) As DataSet
        Dim dtData As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_PERIOD_NAME_AND_YEAR", New List(Of Object)(New Object() {isBlank}))
        Return dtData
    End Function

    Public Function IMPORT_DTTD_DTPB(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_DTTD_DTPB", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function GET_ADJUSTING_IMPORT_DATA(ByVal username As String, ByVal org_id As Decimal, Optional ByVal IS_DISSOLVE As Boolean = False) As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_ADJUSTING_IMPORT_DATA", New List(Of Object)(New Object() {username, org_id, IS_DISSOLVE}))
    End Function

    Public Function IMPORT_ADJUSTING(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_ADJUSTING", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function GET_DTTD_ECD_IMPORT_DATA() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_DTTD_ECD_IMPORT_DATA")
    End Function
    Public Function IMPORT_DTTD_ECD(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_DTTD_ECD", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function IMPORT_LDT_VP(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_LDT_VP", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function IMPORT_PA_MA_SCP_QLCH(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_PA_MA_SCP_QLCH", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function EXPORT_PA_SETUP_FRAMEWORK_OFFICE() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.EXPORT_PA_SETUP_FRAMEWORK_OFFICE")
    End Function

    Public Function GET_IMPORT_PA_MA_SCP_QLCH() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_IMPORT_PA_MA_SCP_QLCH")
    End Function

    Public Function CACULATE_QLCH(ByVal P_SUBSIDIZE As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_OBJ_EMPLOYEE As Decimal, ByVal P_ORG_ID As Decimal, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.CACULATE_QLCH", New List(Of Object)(New Object() {P_SUBSIDIZE, P_PERIOD_ID, P_OBJ_EMPLOYEE, P_ORG_ID, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function GET_PA_STANDARD_SETUP_IMPORT_DATA() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_PA_STANDARD_SETUP_IMPORT_DATA")
    End Function
    Public Function GET_PA_TARGET_STORE_IMPORT_DATA() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_PA_TARGET_STORE_IMPORT_DATA")
    End Function

    Public Function GET_PA_SETUP_HESOMR_NV_QLCH() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PA_SETTING.GET_PA_SETUP_HESOMR_NV_QLCH")
    End Function

    Public Function IMPORT_PA_TARGET_STORE(ByVal P_DATA As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_PA_TARGET_STORE", New List(Of Object)(New Object() {P_DATA}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function IMPORT_PA_STANDARD_SETUP(ByVal P_DATA As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_PA_STANDARD_SETUP", New List(Of Object)(New Object() {P_DATA}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function IMPORT_PA_STORE_SUBSIDIZE(ByVal P_DATA As String, ByVal P_USERNAME As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_SETTING.IMPORT_PA_STORE_SUBSIDIZE", New List(Of Object)(New Object() {P_DATA, P_USERNAME}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function

    Public Function IMPORT_HSTDT(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.IMPORT_HSTDT", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
    Public Function Get_DocumentPIT_Lien(ByVal P_ID As Decimal) As DataTable
        Dim dtData As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.GET_DOCUMENTPIT_LIEN", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                dtData = ds.Tables(0)
            End If
        End If
        Return dtData
    End Function
End Class
