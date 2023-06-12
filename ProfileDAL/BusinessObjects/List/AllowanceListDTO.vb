Public Class AllowanceListDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property ALLOW_TYPE As Decimal?
    Public Property ALLOW_TYPE_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_CONTRACT As Boolean?
    Public Property IS_INSURANCE As Boolean?
    Public Property IS_DISPLAY As Boolean?
    Public Property ALLOW_LEVEL As Decimal?
    Public Property IS_DEDUCT As Boolean?

    Public Property ORDERS As Decimal?
End Class
