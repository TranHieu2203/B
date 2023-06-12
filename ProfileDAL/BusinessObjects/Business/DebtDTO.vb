Public Class DebtDTO

    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property DEBT_TYPE_ID As Decimal?
    Public Property DEBT_TYPE_NAME As String
    Public Property MONEY As Decimal?
    Public Property DEBT_STATUS As Decimal?
    Public Property DEBT_STATUS_NAME As String
    Public Property REMARK As String
    Public Property DATE_DEBIT As Date?
    Public Property DEDUCT_SALARY As Boolean?
    Public Property SALARY_PERIOD As Decimal?
    Public Property SALARY_PERIOD_NAME As String
    Public Property PAID As Boolean?
    Public Property PAYBACK As Boolean?
    Public Property ATTACH_FILE As String
    Public Property NOTE As String
    Public Property param As ParamDTO

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property PERIOD_TEXT As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property IS_TERMINATE As Boolean

    Public Property WORK_STATUS As Decimal?
    Public Property TER_EFFECT_DATE As Date?
End Class
