Public Class ReimbursementDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE As String
    Public Property ORG_NAME As String
    Public Property YEAR As Decimal?
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property TR_PROGRAM_NAME As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property COST_OF_STUDENT As Decimal?
    Public Property COMMIT_WORK As Decimal?
    Public Property WORK_AFTER As Double?
    Public Property COST_REIMBURSE As Decimal?
    Public Property START_DATE As Date?
    Public Property IS_RESERVES As Boolean?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property COST_COM_SUPPORT As Decimal?
    Public Property COST_COMPANY As Decimal?
    Public Property STUDENT_NUMBER As Decimal?

    Public Property TER_DATE As Date?
    Public Property COMMIT_END As Date?
    Public Property CONVERED_TIME As Decimal?
    Public Property COMMIT_DAYS_REMAIN As Decimal?
    Public Property REMARK As String
End Class
