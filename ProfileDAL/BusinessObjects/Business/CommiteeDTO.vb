Public Class CommiteeDTO
    Public Property ID As Decimal
    Public Property YEAR As Decimal?
    Public Property ORG_ID_EMP As Decimal?
    Public Property DECISION_NO As String
    Public Property NAME As String
    Public Property TARGET As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property SIGNER_ID As Decimal?
    Public Property SIGNER_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property SIGNER_CODE As String
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property lstEmp As List(Of CommiteeEmpDTO)
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_TITLE As String
    Public Property EMPLOYEE_LEVEL As String
    Public Property EMPLOYEE_ORG As String
    Public Property EMPLOYEE_ORG_DESC As String
    Public Property COMMITTE_POSITION As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property STATUS_NAME As String
    Public Property IS_INACTIVE As Boolean?
End Class

Public Class CommiteeEmpDTO
    Public Property ID As Decimal
    Public Property COMMITEE_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property MATHE As Decimal?
    Public Property MATHE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_OUTSIDE_NAME As String
    Public Property IS_OUTSIDE As Boolean?
    Public Property COMMITEE_TITLE_ID As Decimal?
    Public Property COMMITEE_TITLE_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
End Class

