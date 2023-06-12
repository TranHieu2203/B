Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class TrainingStoreProcedure
    Private hfr As New HistaffFrameworkRepository

    Public Function GET_TEACHER_LIST_BY_PROGRAM(ByVal PROGRAM_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TEACHER_LIST_BY_PROGRAM", New List(Of Object)({PROGRAM_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function EXPORT_PROGRAM_RESULT(ByVal PROGRAM_ID As Decimal) As DataSet
        Dim ds As New DataSet
        ds = hfr.ExecuteToDataSet("PKG_TRAINING.EXPORT_PROGRAM_RESULT", New List(Of Object)({PROGRAM_ID}))
        Return ds
    End Function

    Public Function EXPORT_REQUEST() As DataSet
        Dim ds As New DataSet
        ds = hfr.ExecuteToDataSet("PKG_TRAINING.EXPORT_REQUEST")
        Return ds
    End Function

    Public Function EXPORT_REIMBURSEMENT() As DataSet
        Dim ds As New DataSet
        ds = hfr.ExecuteToDataSet("PKG_TRAINING.EXPORT_REIMBURSEMENT")
        Return ds
    End Function

    Public Function CHECK_EMPLOYEE_CODE_EXIST_IN_PROGRAM_RESULT(ByVal PROGRAM_ID As Decimal, ByVal EMPLOYEE_CODE As String) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_EMPLOYEE_CODE_EXIST_IN_PROGRAM_RESULT", New List(Of Object)(New Object() {PROGRAM_ID, EMPLOYEE_CODE, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function CHECK_EXIST_PROGRAM(ByVal PROGRAM_ID As Decimal) As Boolean
        Dim ds As New DataSet
        ds = hfr.ExecuteToDataSet("PKG_TRAINING.CHECK_EXIST_PROGRAM", New List(Of Object)(New Object() {PROGRAM_ID}))
        If CDec(ds.Tables(0).Rows(0)(0)) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function CHECK_VALID_EMPLOYEE_REIMBURSEMENT(ByVal PROGRAM_ID As Decimal, ByVal EMPLOYEE_ID As Decimal) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_VALID_EMPLOYEE_REIMBURSEMENT", New List(Of Object)(New Object() {PROGRAM_ID, EMPLOYEE_ID, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function CHECK_VALID_DATE_REIMBURSEMENT(ByVal PROGRAM_ID As Decimal, ByVal EMPLOYEE_ID As Decimal, ByVal CLOSING_DATE As Date) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_VALID_DATE_REIMBURSEMENT", New List(Of Object)(New Object() {PROGRAM_ID, EMPLOYEE_ID, CLOSING_DATE, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function GET_MAIL_BY_EMPLOYEE(ByVal P_EMPLOYEE_ID As Decimal) As String
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_MAIL_BY_EMPLOYEE", New List(Of Object)({P_EMPLOYEE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("MAIL").ToString()
                End If
            End If
        End If
        Return ""
    End Function
    Public Function CHECK_BB(ByVal P_COURSE_ID As Decimal) As String
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TRAINING_FIELD_BY_COURSE", New List(Of Object)({P_COURSE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("BB").ToString()
                End If
            End If
        End If
        Return ""
    End Function
    Public Function GET_TRAINING_FIELD_BY_COURSE(ByVal P_COURSE_ID As Decimal) As String
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TRAINING_FIELD_BY_COURSE", New List(Of Object)({P_COURSE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("TRAINING_FIELD").ToString()
                End If
            End If
        End If
        Return ""
    End Function

    Public Function GET_MAIL_TEMPLATE(ByVal P_CODE As String, ByVal P_GROUP As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TEMPLATE_MAIL", New List(Of Object)(New Object() {P_CODE, P_GROUP}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function UPDATE_STATUS_SEND_MAIL(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_STATUS As Decimal) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.UPDATE_STATUS_SEND_MAIL", New List(Of Object)({P_EMPLOYEE_ID, P_STATUS, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) = 1 Then
            Return True
        End If
        Return False
    End Function

    Public Function GET_MAIL_INFORMATION_OF_PROGRAM_TRAINING(ByVal P_EMPLOYEE_ID As Decimal, ByVal P_CLASS_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_MAIL_INFORMATION_OF_PROGRAM_TRAINING", New List(Of Object)({P_EMPLOYEE_ID, P_CLASS_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_MAIL_DETAIL_OF_PROGRAM_TRAINING(ByVal P_EMPLOYEE_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_MAIL_DETAIL_OF_PROGRAM_TRAINING", New List(Of Object)({P_EMPLOYEE_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function


    Public Function GET_YEARS_IN_COURSE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_YEARS_IN_COURSE", New List(Of Object)({}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function

    Public Function GET_TRAINING_ID_BY_CODE(ByVal P_CODE As String) As Decimal
        Dim dt As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TRAINING_ID_BY_CODE", New List(Of Object)({P_CODE}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return Convert.ToDecimal(ds.Tables(0).Rows(0)("ID"))
                End If
            End If
        End If
        Return dt
    End Function

    Public Function GET_TITLE_GROUP_BY_PLAN_AND_YEAR(ByVal P_PLAN As Decimal, ByVal P_YEAR As Decimal) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TITLE_GROUP_BY_PLAN_AND_YEAR", New List(Of Object)({P_PLAN, P_YEAR}))
        Return ds
    End Function
    Public Function GET_TITLE_BY_LIST_GROUP(ByVal P_GROUP_IDS As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_TITLE_BY_LIST_GROUP", New List(Of Object)({P_GROUP_IDS}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
    Public Function GET_EMPLOYEES_IN_CLASS(ByVal P_CLASS_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_EMPLOYEES_IN_CLASS", New List(Of Object)({P_CLASS_ID}))
        If ds IsNot Nothing Then
            If ds.Tables(0) IsNot Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt
    End Function
    Public Function GET_PROGRAM_CLASS_ROLL_TEMPLATE_DATA(ByVal P_CLASS_ID As Decimal) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_PROGRAM_CLASS_ROLL_TEMPLATE_DATA", New List(Of Object)({P_CLASS_ID}))
        Return ds
    End Function

    Public Function CHECK_EXIST_EMP_IN_CLASS(ByVal P_CLASS_ID As Decimal, ByVal P_EMP_CODE As String) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_EXIST_EMP_IN_CLASS", New List(Of Object)({P_CLASS_ID, P_EMP_CODE, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function IMPORT_PROGRAM_CLASS_ROLL(ByVal P_DATA As String) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.IMPORT_PROGRAM_CLASS_ROLL", New List(Of Object)(New Object() {P_DATA}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function GET_APP_ALL_TEMPLATES() As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_APP_ALL_TEMPLATES", New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function CHECK_TR_PROGRAM_EMP(ByVal PROGRAM_ID As Decimal, ByVal EMPLOYEE_ID As Decimal) As Boolean
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_TRAINING.CHECK_TR_PROGRAM_EMP", New List(Of Object)({PROGRAM_ID, EMPLOYEE_ID, OUT_NUMBER}))
        If Decimal.Parse(obj(0).ToString()) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function GET_TR_COURSE(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_TR_COURSE", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_TR_CRITERIA_GROUP(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_TR_CRITERIA_GROUP", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_TR_CRITERIA(ByVal P_IS_BLANK As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_TR_CRITERIA", New List(Of Object)(New Object() {P_IS_BLANK}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_POINT_MAX_BY_CRITERIA(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PERFORMANCE_LIST.GET_POINT_MAX_BY_CRITERIA", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_DATA_EXPORT_BY_PROGRAM(ByVal P_PROGRAM_ID As Decimal) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_DATA_EXPORT_BY_PROGRAM", New List(Of Object)({P_PROGRAM_ID}))
        Return ds
    End Function

    Public Function EXPORT_ASSESSMENT_RESULT(ByVal P_YEAR As Decimal, ByVal P_PROGRAM_ID As Decimal) As DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.EXPORT_ASSESSMENT_RESULT", New List(Of Object)({P_YEAR, P_PROGRAM_ID}))
        Return ds
    End Function

    Public Function GET_ALLCLASS_BYPROGRAM(ByVal P_ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_TRAINING.GET_ALLCLASS_BYPROGRAM", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
End Class