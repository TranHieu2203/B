Public Class WelfareMngDTO
    Public Property ID As Decimal
    Public Property ORG_ID As Decimal
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_IDS As List(Of Decimal)
    Public Property WELFARE_ID As Decimal
    Public Property WELFARE_NAME As String
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_NAME As String
    Public Property MONEY As Decimal?
    Public Property MONEY_PL As Decimal? 'so tien phuc loi
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property EFFECT_FROM As Date?
    Public Property EFFECT_TO As Date?
    Public Property SDESC As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_TAXION As Decimal?
    Public Property NAME_TAXION As String
    Public Property PERIOD_ID As Decimal?
    Public Property YEAR_PERIOD As Decimal?
    Public Property NAME_PERIOD As String
    Public Property PARAM As ParamDTO
    Public Property IS_TER As Boolean
    Public Property LST_WELFATE_EMP As List(Of Welfatemng_empDTO)
    Public Property WORK_STATUS As Decimal?
    Public Property TER_LAST_DATE As Date?
    Public Property TOTAL_CHILD As Decimal?
    Public Property GENDER_NAME As String
    Public Property CONTRACT_NAME As String
    Public Property SENIORITY As String
    'Public Property REMARK As String
End Class
