Public Class PAFomulerGroup
    Public Property ID As Decimal?
    Public Property TYPE_PAYMENT As Decimal?
    Public Property TYPE_PAYMENT_NAME As String
    Public Property OBJ_SAL_ID As Decimal?
    Public Property OBJ_SAL_NAME As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property START_DATE As Date?
    Public Property END_DATE As Date?
    Public Property STATUS As Decimal?
    Public Property SDESC As String
    Public Property IDX As Decimal?
    Public Property AWARD_CODE As String
End Class

Public Class NormDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property NORM_ID As Decimal?
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property VALUE As Double?
    Public Property NOTE As String
    Public Property ORG_NAME As String
    Public Property TITLE_NAME As String
    Public Property EMP_CODE As String
    Public Property EMP_NAME As String
    Public Property ORG As String
    Public Property NORM As String
    Public Property YEAR_SENIORITY As Decimal?
    Public Property WORK_STATUS As Decimal?
    Public Property IS_TER As Boolean
    Public Property TER_EFFECT_DATE As Date?

End Class