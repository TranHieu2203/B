Public Class AccidentDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String

    Public Property COST As Decimal?
    Public Property ACCIDENT_DATE As Date?
    Public Property REASON_ID As Decimal?
    Public Property REASON_NAME As String
    Public Property INFORMATION As String

    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TREATMENT_PLACE As String
    Public Property MONEY As Decimal?
    Public Property MONEY_DATE As Date?
    Public Property REMARK As String

    Public Property MA_THE As String
    Public Property TITLE_NAME As String


    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

End Class
