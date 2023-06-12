Public Class PeriodDTO

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property START_DATE As Date?
    Public Property END_DATE As Date?
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property TYPE_ASS As Decimal?
    Public Property TYPE_ASS_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property YEAR As Decimal?
    Public Property OBJECT_GROUP_ID As Decimal?
    Public Property PE_STATUS As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_TYPE As Decimal?
    Public Property EMPLOYEE_TYPE_NAME As String
    Public Property NUMBER_DAY As Decimal?
    Public Property NUMBER_MONTH As Decimal?
End Class
