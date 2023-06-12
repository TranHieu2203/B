Public Class ClassificationDTO

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property ID As Decimal
    Public Property CODE As String
    Public Property TR_TYPE_ID As Decimal?
    Public Property TR_TYPE_NAME As String
    Public Property VALUE_FROM As Decimal?
    Public Property VALUE_TO As Decimal?
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property EFFECT_DATE As Date?
End Class
