Public Class PASalaryPlanningDTO
    Public Property ID As Decimal?
    Public Property YEAR As Decimal?
    Public Property MONTH As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property EMP_NUMBER As Decimal?
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_LOG As String
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_LOG As String
    Public Property MANNING_ORG_ID As Decimal?

End Class

Public Class ParamDTO
    Public Property IS_DISSOLVE As Boolean?
    Public Property ORG_ID As Decimal
End Class
