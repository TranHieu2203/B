Public Class HRPlaningDetailDTO
    Public Property ID As Decimal
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As STRING
    Public Property ORG_DESC As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property CURRENT_COUNT As Decimal?
    Public Property EFFECT_DATE As Date?

    Public Property MONTH_1 As Decimal?
    Public Property MONTH_2 As Decimal?
    Public Property MONTH_3 As Decimal?
    Public Property MONTH_4 As Decimal?
    Public Property MONTH_5 As Decimal?
    Public Property MONTH_6 As Decimal?
    Public Property MONTH_7 As Decimal?
    Public Property MONTH_8 As Decimal?
    Public Property MONTH_9 As Decimal?
    Public Property MONTH_10 As Decimal?
    Public Property MONTH_11 As Decimal?
    Public Property MONTH_12 As Decimal?

    Public Property YEAR_PLAN_ID As Decimal?
    Public Property RANK_SAL As Decimal?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property EMP_COUNT As Decimal?
    Public Property ACTUAL_WAGE_FUND As Decimal? 'QUỸ LƯƠNG THỰC TẾ
    Public Property WAGE_FUNDS_PLAN As Decimal? 'QUỸ LƯƠNG THEO ĐỊNH BIÊN
    Public Property WAGE_FUNDS As Decimal? 'QUỸ LƯƠNG CÒN LẠI
    Public Property MONTH_HRP_DETAIL As Decimal?
End Class

