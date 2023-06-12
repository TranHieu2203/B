
Public Class OrgBonusDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property ORDERS As Decimal?
    Public Property ACTFLG As String
End Class

Public Class PaymentSourcesDTO
    Public Property ID As Decimal
    Public Property YEAR As Decimal?
    Public Property NAME As String
    Public Property REMARK As String
    Public Property PAY_TYPE As Decimal?
    Public Property PAY_TYPE_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property ORDERS As Decimal?
    Public Property ACTFLG As String
End Class

Public Class WorkFactorDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property XEPLOAI As String
    Public Property FACTOR As Decimal?
    Public Property BONUSE As Decimal?
    Public Property effect_date As Date?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property ACTFLG As String
End Class
Public Class Work_StandardDTO
    Public Property ID As Decimal
    Public Property YEAR As Decimal?
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String
    Public Property OBJECT_ID As Decimal?
    Public Property OBJECT_NAME As String
    Public Property Period_standard As Decimal?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property ACTFLG As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property param As ParamDTO
End Class


