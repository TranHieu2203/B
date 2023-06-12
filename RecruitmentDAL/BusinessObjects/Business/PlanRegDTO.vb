Public Class PlanRegDTO
    Public Property ID As Decimal
    Public Property SEND_DATE As Date?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property JOBGRADE_ID As Decimal?
    Public Property JOBGRADE_NAME As String
    Public Property RECRUIT_NUMBER As Decimal?
    Public Property EXPECTED_JOIN_DATE As Date?
    Public Property RECRUIT_REASON_ID As Decimal?
    Public Property RECRUIT_REASON_NAME As String
    Public Property REMARK As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
    Public Property lstEmp As List(Of PlanRegEmpDTO)
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property REMARK_REJECT As String
    Public Property EDUCATIONLEVEL As String
    Public Property AGESFROM As Decimal?
    Public Property AGESTO As Decimal?
    Public Property LANGUAGE As String
    Public Property LANGUAGELEVEL As String
    Public Property LANGUAGESCORES As Decimal?
    Public Property SPECIALSKILLS As String
    Public Property MAINTASK As String
    Public Property QUALIFICATION As String
    Public Property QUALIFICATIONREQUEST As String
    Public Property COMPUTER_LEVEL As String
End Class
