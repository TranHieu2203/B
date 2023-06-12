Public Class PE_HTCH_ASSESSMENT_DTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property TITLE_GROUP As Decimal?
    Public Property TITLE_GROUP_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property YEAR As Decimal?
    Public Property PERIOD_NAME As String
    Public Property PERIOD_ID As Decimal?
    Public Property START_DATE As Date?
    Public Property END_DATE As Date?

    Public Property EVALUATION_POINTS As String
    Public Property CLASSIFICATION As String
    Public Property STRENGTH_NOTE As String
    Public Property IMPROVE_NOTE As String
    Public Property PROSPECT_NOTE As String
    Public Property BRANCH_EVALUATE As String
    Public Property REMARK As String
    Public Property REASON As String
    Public Property STATUS_NAME As String
    Public Property STATUS_ID As Decimal?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property EMP_APPROVER_NAME As String
    Public Property lstDetail As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
End Class
