Public Class DecisionDTO
    Public Property ID As Guid
    Public Property NO As String
    Public Property NAME As String
    Public Property TYPE As Guid
    Public Property SIGNER As Guid
    Public Property SIGNER_TITLE As String
    Public Property SIGN_DATE As Date?
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property CREATE_DATE As Date?
    Public Property EMPLOYEE_ID As Guid?
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
