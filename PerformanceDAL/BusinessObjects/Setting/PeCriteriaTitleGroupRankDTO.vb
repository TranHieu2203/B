Public Class CriteriaTitleGroupDTO
    Public Property ID As Decimal
    Public Property GROUPTITLE_ID As Decimal?
    Public Property GROUPTITLE_NAME As String
    Public Property CRITERIA_ID As Decimal?
    Public Property CRITERIA_CODE As String
    Public Property CRITERIA_NAME As String
    Public Property IS_CHECK As Boolean
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property EFFECT_DATE As Date?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property NOTE As String
    Public Property RATIO As Decimal?

    '-------------- danh sach tieu chi 
    Public Property lstObj As List(Of CriteriaTitleGroupDTO)
End Class
