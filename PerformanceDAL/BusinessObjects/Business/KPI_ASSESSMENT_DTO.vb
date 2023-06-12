Public Class KPI_ASSESSMENT_DTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE As Decimal?
    Public Property YEAR As Decimal?
    Public Property PE_PERIOD_ID As Decimal?
    Public Property START_DATE As Date?
    Public Property END_DATE As Date?
    Public Property EFFECT_DATE As Date?
    Public Property NUMBER_MONTH As Decimal?
    Public Property GOAL As String
    Public Property EVALUATION_POINTS As String
    Public Property CLASSIFICATION As String
    Public Property REMARK As String
    Public Property REASON As String
    Public Property STATUS_ID As Decimal?
    Public Property PORTAL_ID As Decimal?
    Public Property IS_CONFIRM As Decimal?
    Public Property CONFIRM_NAME As String
    Public Property EMP_APPROVES_NAME As String


    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property PE_PERIOD_NAME As String
    Public Property IS_FROM_PORTAL As Boolean?
    Public Property STATUS_NAME As String
    Public Property REASON_CONFIRM As String
    Public Property KPI_EMP_APPROVES_ID As Decimal?
    Public Property KPI_EMP_APPROVES_NAME As String
    Public Property RATIO As Decimal?
    Public Property IS_CONFIRM_STT As String
    Public Property MULTI_CRITERIA As Boolean?

    Public assesmentDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
    Public assesment As List(Of KPI_ASSESSMENT_DTO)

End Class
