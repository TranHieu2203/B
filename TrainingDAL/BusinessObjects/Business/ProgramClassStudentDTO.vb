Public Class ProgramClassStudentDTO
    Public Property ID As Decimal
    Public Property TR_CLASS_ID As Decimal?
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
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property GENDER_NAME As String
    Public Property lstTitle As List(Of Decimal)
    Public Property lstContractType As List(Of Decimal)
    Public Property SEND_MAIL_STATUS As String
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property WORK_EMAIL As String
    Public Property PER_EMAIL As String
    Public Property GRADE As Decimal?
    Public Property NOTE As String
    Public Property IS_EXAM As Decimal?
End Class
