Public Class FunctionDTO
    Public Property ID As Decimal
    Public Property NAME As String
    Public Property MODULE_NAME As String
    Public Property MODULE_ID As Decimal?
    Public Property FID As String
    Public Property FUNCTION_GROUP_NAME As String
    Public Property FUNCTION_GROUP_ID As Decimal?
    Public Property ACTFLG As String
    Public Property SETUP_SIGNER As Boolean?
    Public Property ADMIN_HR As Boolean?
    Public Property ORDER_BY As Decimal?

    Public Property FUNCTION_NAME_FID As String
    Public Property MODULE_NAME_FID As String
    Public Property FUNCTION_GROUP_NAME_FID As String

    Public Property ORG_ID As Decimal?
    Public Property WORK_PLACE_ID As Decimal?

End Class