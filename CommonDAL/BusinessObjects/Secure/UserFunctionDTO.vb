Public Class UserFunctionDTO
    Public Property X As Boolean
    Public Property ID As Decimal
    Public Property USER_ID As Decimal
    Public Property MODULE_NAME As String
    Public Property FUNCTION_ID As Decimal
    Public Property FUNCTION_CODE As String
    Public Property FUNCTION_NAME As String
    Public Property ALLOW_CREATE As Boolean
    Public Property ALLOW_MODIFY As Boolean
    Public Property ALLOW_DELETE As Boolean
    Public Property ALLOW_PRINT As Boolean
    Public Property ALLOW_IMPORT As Boolean
    Public Property ALLOW_EXPORT As Boolean
    Public Property ALLOW_SPECIAL1 As Boolean
    Public Property ALLOW_SPECIAL2 As Boolean
    Public Property ALLOW_SPECIAL3 As Boolean
    Public Property ALLOW_SPECIAL4 As Boolean
    Public Property ALLOW_SPECIAL5 As Boolean
    Public Property GROUP_ID As Decimal?
    Public Property ORDER_BY As Decimal?
    Public Property FUNCTION_GROUP_NAME As String
    Public Property FUNCTION_GROUP_ID As Decimal?

    Public Property ORG_ID As Decimal?
    Public Property WORK_PLACE_ID As Decimal?

    Public Property ORG_NAME As String
    Public Property WORK_PLACE_NAME As String

    Public Property LST_ORG As List(Of Decimal)
    Public Property LST_WL As List(Of Decimal)

End Class