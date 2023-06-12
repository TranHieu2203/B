Public Class CBPlanningDTO
    Public Property ID As Decimal
    Public Property YEAR As Decimal?
    Public Property DECISION_NO As String
    Public Property EFFECT_DATE As Date?
    Public Property CONTENT As String
    Public Property SIGNER_ID As Decimal?
    Public Property SIGNER_NAME As String
    Public Property SIGNER_CODE As String
    Public Property SIGN_DATE As Date?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property lstEmp As List(Of CBPlanningEmpDTO)
End Class

Public Class CBPlanningEmpDTO
    Public Property ID As Decimal
    Public Property CB_PLANNING_ID As Decimal?
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
    Public Property TITLE_PLANNING_ID As Decimal?
    Public Property TITLE_PLANNING_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
End Class

Public Class CBPlanningEmpHisDTO
    Public Property ID As Decimal
    Public Property CB_PLANNING_ID As Decimal?
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
    Public Property TITLE_PLANNING_ID As Decimal?
    Public Property TITLE_PLANNING_NAME As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

Public Class CBAssessmentDTO
    Public Property ID As Decimal
    Public Property CONFIRM_YEAR As Decimal?
    Public Property CONTENT As String
    Public Property ASSESSMENT_YEAR As Decimal?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property lstDtl As List(Of CBAssessmentDtlDTO)
End Class

Public Class CBAssessmentDtlDTO
    Public Property ID As Decimal
    Public Property CB_ASS_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property MATHE As Decimal?
    Public Property MATHE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property REMARK As String
    Public Property RESULT As Decimal?
    Public Property RESULT_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

