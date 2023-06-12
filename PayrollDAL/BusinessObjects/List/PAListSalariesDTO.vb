Public Class PAListSalariesDTO
    Public Property ID As Decimal?
    Public Property TYPE_PAYMENT As Decimal?
    Public Property TYPE_PAYMENT_NAME As String
    Public Property COL_NAME As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property DATA_TYPE As Decimal?
    Public Property COL_INDEX As Decimal?
    Public Property INPUT_FORMULER As String
    Public Property STATUS As String
    Public Property IS_VISIBLE As Boolean
    Public Property IS_INPUT As Boolean
    Public Property IS_CALCULATE As Boolean
    Public Property IS_IMPORT As Boolean
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_LOG As String
    Public Property COL_CODE As String
    Public Property IS_WORKDAY As Boolean?
    Public Property IS_SUMDAY As Boolean?
    Public Property IS_WORKARISING As Boolean?
    Public Property IS_SUMARISING As Boolean?
    Public Property IS_PAYBACK As Boolean?
    Public Property IS_DELETED As Decimal?
    Public Property IMPORT_TYPE_ID As Decimal?
    Public Property IMPORT_TYPE_NAME As String
    Public Property REMARK As String
    Public Property EFFECTIVE_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property OBJ_SAL_ID As Decimal?
    Public Property OBJ_SAL_NAME As String
    Public Property GROUP_TYPE_ID As Decimal?
    Public Property GROUP_TYPE_NAME As String
End Class
Public Class PAListSalDTO
    Public Property ID As Decimal?
    Public Property COL_NAME As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property DATA_TYPE As Decimal?
    Public Property DATA_TYPE_NAME As String
    Public Property GROUP_TYPE As Decimal?
    Public Property GROUP_TYPE_NAME As String
    Public Property COL_INDEX As Decimal?
    Public Property COL_CODE As String
    Public Property STATUS As String
    Public Property REMARK As String
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_LOG As String

End Class
Public Class PA_SALARY_FUND_MAPPINGDTO
    Public Property ID As Decimal?
    Public Property YEAR As Decimal
    Public Property PERIOD_ID As Decimal
    Public Property SALARY_GROUP As Decimal
    Public Property SALARY_NAME As String
    Public Property SALARY_FUND As Decimal
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_LOG As String

End Class

