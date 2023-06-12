Public Class PA_Accounting_OvertimeDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_CODE As String
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String
    Public Property ORG_SET_ID As Decimal?
    Public Property ORG_SET_NAME As String

    Public Property OT_DAY_OLD As Decimal?
    Public Property OT_NIGHT_OLD As Decimal?
    Public Property OT_OFFDAY_OLD As Decimal?
    Public Property OT_OFFNIGHT_OLD As Decimal?
    Public Property OT_LEDAY_OLD As Decimal?
    Public Property OT_LENIGHT_OLD As Decimal?
    Public Property SALARY_OLD As Decimal?

    Public Property OT_DAY_NEW As Decimal?
    Public Property OT_NIGHT_NEW As Decimal?
    Public Property OT_OFFDAY_NEW As Decimal?
    Public Property OT_OFFNIGHT_NEW As Decimal?
    Public Property OT_LEDAY_NEW As Decimal?
    Public Property OT_LENIGHT_NEW As Decimal?
    Public Property SALARY_NEW As Decimal?

    Public Property IS_LOCK As Boolean?

    Public Property YEAR As Decimal?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_TER As Boolean?
End Class
