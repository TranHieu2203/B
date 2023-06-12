Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class PerformanceStoreProcedure
    Private hfr As New HistaffFrameworkRepository
    Public Function GetYear(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_YEAR", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GetYear2(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_YEAR2", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_PE_PERIOD_BY_YEAR(ByVal P_IS_BLANK As Boolean, ByVal P_YEAR As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_PE_PERIOD_BY_YEAR", New List(Of Object)(New Object() {P_IS_BLANK, P_YEAR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function CHECK_JOIN_DATE_EMP(ByVal P_EMP_ID As Decimal, ByVal P_PERIOD_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.CHECK_JOIN_DATE_EMP", New List(Of Object)(New Object() {P_EMP_ID, P_PERIOD_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CRITERIA(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_CRITERIA", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CRITERIA_HTCH(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_CRITERIA_HTCH", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_CRITERIA_BY_ID(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_CRITERIA_BY_ID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_EVALUATE_CODE(ByVal P_ID As Decimal) As String
        Dim dt = ""
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_EVALUATE_CODE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0).Rows(0).Item(0).ToString
        End If
        Return dt
    End Function

    Public Function GET_CRITERIA_CODE(ByVal P_ID As Decimal) As String
        Dim dt = ""
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_CRITERIA_CODE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0).Rows(0).Item(0).ToString
        End If
        Return dt
    End Function

    Public Function CHECK_EXIST_ASSESMENT(ByVal P_EMP_ID As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_ASSESMENT As String, ByVal P_EFFECT_DATE As Date) As Decimal
        Dim dt = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.CHECK_EXIST_ASSESMENT", New List(Of Object)(New Object() {P_EMP_ID, P_PERIOD_ID, P_ASSESMENT, P_EFFECT_DATE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = Convert.ToDecimal(ds.Tables(0).Rows(0).Item(0))
        End If
        Return dt
    End Function

    Public Function GET_SO_A(ByVal P_ID As Decimal) As Decimal
        Dim dt = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_SO_A", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = Convert.ToDecimal(ds.Tables(0).Rows(0).Item(0))
        End If
        Return dt
    End Function
    Public Function GET_APP_ALL_TEMPLATES() As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_APP_ALL_TEMPLATES", New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function
    Public Function CHECK_EXIST_EMP_IN_PERIOD(ByVal P_EMP_ID As Decimal, ByVal P_PERIOD_ID As Decimal) As Decimal
        Dim dt = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.CHECK_EXIST_EMP_IN_PERIOD", New List(Of Object)(New Object() {P_EMP_ID, P_PERIOD_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = Convert.ToDecimal(ds.Tables(0).Rows(0).Item(0))
        End If
        Return dt
    End Function

    Public Function GET_MAIL_TEMPLATE(ByVal P_CODE As String, ByVal P_GROUP As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_TEMPLATE_MAIL", New List(Of Object)(New Object() {P_CODE, P_GROUP}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function UPDATE_STATUS_SEND_MAIL(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS As Decimal) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PERFORMANCE_LIST.UPDATE_STATUS_SEND_MAIL", New List(Of Object)({P_EMPLOYEE_ID, P_STATUS, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) = 1 Then
            Return True
        End If
        Return False
    End Function

    Public Function UPDATE_STATUS_SEND_MAIL_HTCH(ByVal P_EMPLOYEE_ID As String, ByVal P_PERIOD_ID As Decimal, ByVal P_STATUS As Decimal) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PERFORMANCE_LIST.UPDATE_STATUS_SEND_MAIL_HTCH", New List(Of Object)({P_EMPLOYEE_ID, P_PERIOD_ID, P_STATUS, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) = 1 Then
            Return True
        End If
        Return False
    End Function

    Public Function GET_MAIL_INFORMATION_OF_PE_EMPLOYEE_PERIOD(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_PE_PERIOD_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_MAIL_INFORMATION_OF_PE_EMPLOYEE_PERIOD", New List(Of Object)({P_EMPLOYEE_ID, P_PE_PERIOD_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
    Public Function GET_MAIL_INFORMATION_OF_PE_EMPLOYEEHTCH_PERIOD(ByVal P_EMPLOYEE_ID As String, ByVal P_DIRECT_ID As Decimal, ByVal P_PERIOD_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_MAIL_INFORMATION_OF_PE_EMPLOYEEHTCH_PERIOD", New List(Of Object)({P_EMPLOYEE_ID, P_DIRECT_ID, P_PERIOD_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_MAIL_BY_EMPLOYEE(ByVal P_EMPLOYEE_ID As Decimal) As String
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_MAIL_BY_EMPLOYEE", New List(Of Object)({P_EMPLOYEE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("MAIL").ToString()
                End If
            End If
        End If
        Return ""
    End Function

    Public Function GET_MAIL_INFORMATION_OF_APPROVE_EVALUATE_TARGET(ByVal P_EMPLOYEE_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_MAIL_INFORMATION_OF_APPROVE_EVALUATE_TARGET", New List(Of Object)({P_EMPLOYEE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
    Public Function GetPe_Org_Mr_Rr_ImportData() As DataSet
        Return hfr.ExecuteToDataSet("PKG_PERFORMANCE_BUSINESS.GETPE_ORG_MR_RR_IMPORTDATA")
    End Function
    Public Function IMPORT_PE_ORG_MR_RR(ByVal P_DATA As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_BUSINESS.IMPORT_PE_ORG_MR_RR", New List(Of Object)(New Object() {P_DATA}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
End Class
