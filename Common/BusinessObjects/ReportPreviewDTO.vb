Imports Microsoft.Reporting.WebForms

Public Class ReportPreviewDTO
    Public Property DataSourceName As List(Of String)
    Public Property Parameters As List(Of ReportParameter)
    Public Property ReportTemplatePath As String
    Public Property ReportDisplayName As String
    Public Property FromDate As Date?
    Public Property ToDate As Date?
    Public Property SelectedOrgId As List(Of Decimal)
    Public Property OptionalParameter As String
End Class