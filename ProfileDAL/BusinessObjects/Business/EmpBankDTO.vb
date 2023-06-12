Public Class EmpBankDTO
    Public Property ID As Decimal
    Public Property ORG_ID As Decimal
    Public Property ORG_CODE As String
    Public Property ORG_NAME As String
    Public Property TITLE_NAME As String

    Public Property IS_TER As Boolean?

    Public Property ORG_DESC As String

    Public Property STK As String
    Public Property BANK_ID As Decimal?
    Public Property BANK_NAME As String

    Public Property PERSON_INHERITANCE As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String



End Class
