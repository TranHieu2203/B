Public Class DisciplineSalaryDTO
    Public Property ID As Decimal
    Public Property DISCIPLINE_ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_NAME As String
    Public Property DECISION_NO As String
    Public Property EFFECT_DATE As Date?
    Public Property YEAR As Decimal?
    Public Property MONTH As Decimal?
    Public Property MONEY As Decimal?
    Public Property INDEMNIFY_MONEY As Decimal?
    Public Property APPROVE_STATUS As Boolean
    Public Property param As ParamDTO
    Public Property IS_TERMINATE As Boolean
    Public Property IS_STOP As Boolean
    Public Property MONEY_ORIGIN As Decimal?
    Public Property INDEMNIFY_MONEY_ORIGIN As Decimal?
    Public Property IS_PORTAL As Decimal?
    Public Property IS_PORTAL_NAME As String
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date?

End Class
