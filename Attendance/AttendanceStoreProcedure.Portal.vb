Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class AttendanceStoreProcedure
    Public Function CHECK_SEND_APPROVE(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_SEND_APPROVE", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function INSERT_ID_BY_DELETE_SIGN_WORK(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.INSERT_ID_SIGN_WORK_BY_DELETE", New List(Of Object)(New Object() {listID}))
        'Return ds
        'If ds IsNot Nothing Then
        '    dt = ds.Tables(0)
        'End If
        Return dt
    End Function
    Public Function SELECT_ID_BY_DELETE_SIGN_WORK(ByVal Index As Integer) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.SELECT_ID_BY_DELETE_SIGN_WORK", New List(Of Object)(New Object() {Index}))

        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CHECK_DELETE_SIGNWORK(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_DELETE_SIGNWORK", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_APP_ALL_TEMPLATES() As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_APP_ALL_TEMPLATES", New List(Of Object)(New Object() {}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function CHECK_APPROVAL(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_APPROVAL", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
        Else
            dt = Nothing
        End If
        Return dt
    End Function
    Public Function CHECK_DMVS_APPROVAL(ByVal listID As String) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.CHECK_DMVS_APPROVAL", New List(Of Object)(New Object() {listID}))
        'Return ds
        If ds IsNot Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    'APPROVE BY PROCESS
    Public Function GET_APPROVE_STATUS(ByVal ID As Decimal, ByVal P_PROCESS_CODE As String) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_PROCESS.GET_APPROVE_STATUS", New List(Of Object)(New Object() {ID, P_PROCESS_CODE}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        End If
    End Function
    Public Function APPROVE_REG(ByVal employee As Decimal, ByVal employee_app As Decimal, ByVal period_id As Decimal, ByVal status_id As Decimal, ByVal P_PROCESS_CODE As String, ByVal P_NOTES As String, ByVal P_ID_REGGROUP As Decimal) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_AT_PROCESS.PRI_PROCESS", New List(Of Object)(New Object() {employee, employee_app, period_id, status_id, P_PROCESS_CODE, P_NOTES, P_ID_REGGROUP, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function GET_PERIOD_BYDATE(ByVal P_DATE As Date?) As DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_PERIOD_BYDATE", New List(Of Object)(New Object() {P_DATE}))
        If ds IsNot Nothing Then
            Return ds.Tables(0)
        End If
    End Function

    Public Function GET_PERIOD_USER(ByVal user As String) As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_ATTENDANCE_BUSINESS.GET_PERIOD_USER", New List(Of Object)(New Object() {user}))
        If ds IsNot Nothing Then
            Return ds
        End If
    End Function
    ''' <summary>
    ''' KIEM TRA KHOANG THOI GIAN GIUA 2 CA
    ''' </summary>
    ''' <param name="P_SHIFT1"></param>
    ''' <param name="P_SHIFT2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_TIME_IN_OUT(ByVal P_SHIFT1 As String, ByVal P_SHIFT2 As String) As Boolean
        Dim dt As New DataTable
        Dim rs As Boolean = False
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_ATTENDANCE_LIST.GET_TIME_IN_OUT", New List(Of Object)(New Object() {P_SHIFT1, P_SHIFT2}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If dt.Rows(0)(0) >= 12 Then
                rs = True
            Else
                rs = False
            End If
        Else
            rs = False
        End If
        Return rs
    End Function

    Public Function DELETE_AT_WORKSIGN(ByVal P_EMP As String, ByVal P_PERIOD_ID As Decimal, ByVal P_OBJ_EMP As Decimal) As Decimal
        Dim count As Decimal = 0
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_PROCESS.DELETE_AT_WORKSIGN", New List(Of Object)(New Object() {P_EMP, P_PERIOD_ID, P_OBJ_EMP}))
        If Not ds Is Nothing Then
            If Not ds.Tables(0) Is Nothing Then
                count = CDec(ds.Tables(0).Rows(0)(0))
            End If
        End If
        Return count
    End Function
    Public Function GET_PORTAL_WORKSIGN_DIRECT(ByVal P_USER_ID As Decimal, ByVal P_EMPLOYEE_CODE As String, ByVal P_PERIOD_ID As Decimal?, ByVal P_EMP_OBJ As Decimal?, ByVal P_START_DATE As Date?, ByVal P_END_DATE As Date?) As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_PORTAL_WORKSIGN_DIRECT", New List(Of Object)(New Object() {P_USER_ID, P_EMPLOYEE_CODE, P_PERIOD_ID, P_EMP_OBJ, P_START_DATE, P_END_DATE}))
        If ds IsNot Nothing Then
            Return ds
        End If
    End Function
    Public Function GET_PORTAL_WORKSIGN_DIRECT_IMPORT(ByVal P_USER_ID As Decimal, ByVal P_EMPLOYEE_CODE As String, ByVal P_PERIOD_ID As Decimal?, ByVal P_EMP_OBJ As Decimal?, ByVal P_START_DATE As Date?, ByVal P_END_DATE As Date?) As DataSet
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_AT_ATTENDANCE_PORTAL.GET_PORTAL_WORKSIGN_DIRECT_IMPORT", New List(Of Object)(New Object() {P_USER_ID, P_EMPLOYEE_CODE, P_PERIOD_ID, P_EMP_OBJ, P_START_DATE, P_END_DATE}))
        If ds IsNot Nothing Then
            Return ds
        End If
    End Function
End Class
