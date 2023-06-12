Public Class IncentiveRankDTO
    Public Property ID As Decimal
    Public Property SAL_GROUP_ID As Decimal
    Public Property SAL_GROUP_NAME As String
    Public Property SAL_LEVEL_ID As Decimal
    Public Property SAL_LEVEL_NAME As String
    Public Property SAL_RANK_ID As Decimal
    Public Property RANK As String
    Public Property SAL_INCENTIVE_ID As Decimal
    Public Property SAL_INCENTIVE_NAME As String
    Public Property EFFECT_DATE As Date?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property ACTFLG As String
    Public Property ORDERS As Decimal?
    Public Property INCENTIVERANKDETAIL As List(Of IncentiveRankDetailDTO)
End Class

Public Class IncentiveRankDetailDTO
    Public Property ID As Decimal
    Public Property INCENTIVE_RANK_ID As Decimal
    Public Property FROM_TARGET As Decimal
    Public Property TO_TARGET As Decimal
    Public Property AMOUNT As Decimal
    Public Property INCENTIVE_PERCENT As Decimal
    Public Property INCENTIVE_AMOUNT As Decimal
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property ACTFLG As String
    Public Property ORDERS As Decimal?
End Class

