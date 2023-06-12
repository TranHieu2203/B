Public Class ContractDTO
    Public Property ID As Guid
    Public Property ORG_ID As Guid
    Public Property ORG_NAME As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACTTYPE_ID As Guid
    Public Property CONTRACTTYPE_NAME As String
    Public Property START_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property SIGNER_TITLE As String
    Public Property SIGNER As Guid
    Public Property SIGNER_NAME As String
    Public Property SIGN_DATE As Date
    Public Property EMPLOYEE_ID As Guid
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_NAME As String
    Public Property IS_PROBATION As Boolean
    Public Property PROBATION_START As Date?
    Public Property PROBATION_END As Date?
    Public Property PROBATION_MONTH As Double
    Public Property FORM_ID As Guid?
    Public Property SALARY_BASIC As Double?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

End Class
