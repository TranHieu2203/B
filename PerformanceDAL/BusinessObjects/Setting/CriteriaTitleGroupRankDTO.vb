Public Class CriteriaTitleGroupRankDTO
    Public Property ID As Decimal
    Public Property GROUPTITLE_ID As Decimal?
    Public Property GROUPTITLE_NAME As String
    Public Property CRITERIA_ID As Decimal?
    Public Property CRITERIA_CODE As String
    Public Property CRITERIA_NAME As String
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
    Public Property NAME As String

    Public Property CRITERIA_TITLEGROUP_ID As Decimal?
    Public Property RANK_FROM As Decimal?
    Public Property RANK_TO As Decimal?
    Public Property POINT As Decimal?
    Public Property DESCRIPTION As String

    '-------------- danh sach tieu chi 
    Public Property lstObj As List(Of CriteriaTitleGroupRankDTO)
End Class
