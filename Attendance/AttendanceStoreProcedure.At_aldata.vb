Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Partial Class AttendanceStoreProcedure
    Dim hfr As New HistaffFrameworkRepository
    Public Function GET_INFO_NGHIBU(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_LIST.GET_INFO_NGHIBU",
                                           New List(Of Object)(New Object() {id, fromDate, OUT_CURSOR}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_TER_LAST_DATE(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Try

            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_LIST.GET_INFO_NGHIBU",
                                           New List(Of Object)(New Object() {id, fromDate, OUT_CURSOR}))
            If Not ds Is Nothing Or Not ds.Tables(1) Is Nothing Then
                dt = ds.Tables(1)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_RESIDUAL_ALLOWANCES(ByVal Employee_ID As Decimal, ByVal MANUAL_ID As Decimal, ByVal FromDate As Date) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_LIST.GET_RESIDUAL_ALLOWANCES",
                                           New List(Of Object)(New Object() {Employee_ID, MANUAL_ID, FromDate, OUT_CURSOR}))
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables(0) IsNot Nothing Then
                    dt = ds.Tables(0)
                End If

            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_EMP_BY_ID(ByVal id As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_AT_LIST.GET_EMP_BY_ID",
                                           New List(Of Object)(New Object() {id, OUT_CURSOR}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
