
Public Class ApproveSetupDTO
    Public Property ID As Decimal
    Public Property PROCESS_ID As Decimal
    Public Property TEMPLATE_ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property SIGN_ID As Decimal?
    Public Property LEAVEPLAN_ID As Decimal?

    Public Property PROCESS_NAME As String
    Public Property TEMPLATE_NAME As String
    Public Property TITLE_NAME As String
    Public Property SIGN_NAME As String
    Public Property LEAVEPLAN_NAME As String

    Public Property NUM_REQUEST As Decimal?
    Public Property REQUEST_EMAIL As String
    Public Property MAIL_ACCEPTED As String
    Public Property MAIL_ACCEPTING As String

    Public Property FROM_HOUR As Decimal?
    Public Property TO_HOUR As Decimal?
    Public Property FROM_DAY As Decimal?
    Public Property TO_DAY As Decimal?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property IS_IGNORE_APPROVE_LEVEL As Boolean

    Public Property GROUP_TITLE_ID As Decimal?
    Public Property GROUP_TITLE_NAME As String

    Public Property IS_OVER_LIMIT As Boolean

End Class
