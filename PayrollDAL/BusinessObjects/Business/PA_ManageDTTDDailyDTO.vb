Public Class PA_ManageDTTDDailyDTO
    Public Property ID As Decimal
    Public Property STORE_ID As Decimal?
    Public Property STORE_NAME As String
    Public Property SALE_DATE As Date?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property DTTD As Decimal?
    Public Property DTTD_NG As Decimal?
    Public Property DTTD_KNG1 As Decimal?
    Public Property DTTD_KNG2 As Decimal?

    Public Property TARGET_GROUP_ID As Decimal?
    Public Property TARGET_GROUP_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
