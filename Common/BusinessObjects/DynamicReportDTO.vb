Public Class DynamicReportDTO
    Public Property OrgId As List(Of Decimal)
    Public Property IS_DISSOLVE As Boolean?
    Public Property ColumnText As Dictionary(Of String, String)
    Public Property Column As List(Of String)
    Public Property ColumnType As List(Of String)
    Public Property Expression As String
End Class