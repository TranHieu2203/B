Public Class RequestDTO
    Property ID As Decimal
    Property TR_PLAN_ID As Decimal?
    Property TR_PLAN_NAME As String
    Property TR_COURSE_ID As Decimal?
    Property TR_COURSE_NAME As String
    Property ORG_ID As Decimal?
    Property ORG_NAME As String
    Property ORG_DESC As String
    Property REQUEST_DATE As Date?
    Property YEAR As Decimal?
    Property EXPECTED_DATE As Date?
    Property EXPECTED_DATE_TO As Date?
    Property CREATED_DATE As Date?
    Property TRAINER_NUMBER As Decimal?
    Property EXPECTED_COST As Decimal?
    Property REQUEST_CODE As String

    Property REQUEST_SENDER_ID As Decimal? 'EMPLOYEE_ID
    Property REQUEST_SENDER_NAME As String
    Property TR_PLACE As String
End Class