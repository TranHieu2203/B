Public Class JobPositinTreeDTO
    Public Property ID As Decimal
    Public Property PARENT_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property CODE As String
    Public Property LY_FTE As Decimal?
    Public Property TOTAL_EMP As Decimal?
    Public Property CHENH_LECH1 As Decimal?
    Public Property TOTAL_TITLE As Decimal?
    Public Property CHENH_LECH2 As Decimal?
    Public Property IS_JOB As Boolean?
    Public Property NHOMDUAN As Boolean?
End Class

Public Class JobChildTreeDTO
    Public Property ID As Decimal
    Public Property PARENT_ID As Decimal?
    Public Property NAME As String
    Public Property FUNCTION_NAME As String
End Class
