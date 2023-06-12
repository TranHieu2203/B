Public Class AssessmentResultDTO
    Public Property ID As Decimal
    Public Property TR_CHOOSE_FORM_ID As Decimal?
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property lstResult As List(Of AssessmentResultDtlDTO)
    Public Property CRI_COURSE_ID As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property NOTE1 As String
    Public Property NOTE2 As String
    Public Property NOTE3 As String
    Public Property NOTE4 As String
    Public Property EMPLOYEE_COURSE_ID As Decimal?
    Public Property IS_LOCK As Decimal?
    Public Property IS_LOCK_TEXT As String
    Public Property ASSESMENT_ID As Decimal?
    Public Property STATUS As String

End Class
