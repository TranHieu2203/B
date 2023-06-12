Public Class ProgramCommitDTO
    Public Property ID As Decimal
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property IS_PLAN As Boolean?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property CONTRACT_TYPE_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As Decimal?
    Public Property ID_DATE As Date?
    Public Property GENDER_NAME As String
    Public Property lstTitle As List(Of Decimal)
    Public Property lstContractType As List(Of Decimal)

    Public Property COMMIT_NO As String
    Public Property CONVERED_TIME As Decimal?
    Public Property COMMIT_WORK As Decimal?
    Public Property COMMIT_DATE As Date?
    Public Property COMMIT_START As Date?
    Public Property COMMIT_END As Date?
    Public Property COMMIT_REMARK As String
    Public Property APPROVER_ID As Decimal?
    Public Property APPROVER_NAME As String
    Public Property APPROVER_TITLE As String

    Public Property COST_STUDENT As Decimal?

    Public Property IS_LOCK As Decimal?
    Public Property CLOSING_DATE As Date?
    Public Property REIMBURSE_TIME As Decimal?
    Public Property MONEY_REIMBURSE As Decimal?
    Public Property MONTH_PERIOD As String
    Public Property REIMBURSE_REMARK As String
    Public Property IS_LOCK_NAME As String
    Public Property TER_DATE As Date?
    Public Property YEAR As Decimal?
    Public Property TR_PROGRAM_NAME As String
    Public Property TR_PROGRAM_START_DATE As Date?
    Public Property TR_PROGRAM_END_DATE As Date?
    Public Property MONEY_COMMIT As Decimal?

    Public Property COMMIT_START_T_SEARCH As Date?
    Public Property COMMIT_START_E_SEARCH As Date?
    Public Property COMMIT_END_T_SEARCH As Date?
    Public Property COMMIT_END_E_SEARCH As Date?
    Public Property EMP_CODE_SEARCH As String
    Public Property YEAR_SEARCH As Decimal?
    Public Property COURSE_SEARCH As Decimal?

    Public Property IS_TER_SEARCH As Boolean?
    Public Property CREATED_DATE As Date?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_EFFECT_DATE As Date?
    Public Property IS_COMMIT As Boolean?
End Class
