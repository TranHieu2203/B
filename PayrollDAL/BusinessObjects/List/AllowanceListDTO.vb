Public Class AllowanceListDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property ALLOWANCE_TYPE As Decimal
    Public Property ALLOWANCE_TYPE_NAME As String
    Public Property ORDERS As Decimal
    Public Property IS_CONTRACT As Decimal
    Public Property IS_INSURANCE As Decimal
    Public Property IS_PAY As Decimal
End Class

Public Class AllowanceDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As String
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME_VN As String
    Public Property ALLOWANCE_TYPE As Decimal
    Public Property ALLOWANCE_TYPE_NAME As String
    Public Property AMOUNT As Decimal
    Public Property EFFECT_DATE As Date
    Public Property EXP_DATE As Date?
    Public Property ACTFLG As String
    Public Property REMARK As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String



End Class