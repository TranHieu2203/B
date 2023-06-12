Public Class ContractTypeDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property PERIOD As Decimal?
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property BHXH As Decimal?
    Public Property BHYT As Decimal?
    Public Property BHTN As Decimal?
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property TYPE_ID As Decimal?
    Public Property TYPE_NAME As String
    Public Property TYPE_CODE As String

    Public Property FLOWING_MD_ID As Decimal?
    Public Property FLOWING_MD As String

    Public Property CODE_GET_ENDDATE_ID As Decimal?
    Public Property CODE_GET_ENDDATE As String

    Public Property IS_HOCVIEC As Boolean?
    Public Property IS_REQUIREMENT As Boolean?
    Public Property IS_HSL As Boolean?
    Public Property NAME_VISIBLE_ONFORM As String
End Class
