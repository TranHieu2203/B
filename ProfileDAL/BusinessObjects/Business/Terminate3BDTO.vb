Public Class Terminate3BDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_SENIORITY As String
    Public Property JOIN_DATE As Date?
    Public Property ID_NO As String
    Public Property IS_IMPACT As Boolean
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_ABBR As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property WRITE_DATE As Date?
    Public Property STATUS_ID As Decimal
    Public Property STATUS_NAME As String
    Public Property STATUS_CODE As String
    Public Property DECISION_ID As Decimal
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IDENTIFI_CARD As Decimal? 'check chon the nv, the gui xe
    Public Property IDENTIFI_RDATE As Date? 'ngay tra the nv, the gui xe
    Public Property IDENTIFI_STATUS As String 'tinh trang the nv, the gui xe
    Public Property SUN_CARD As Decimal? 'check chon the sungroup
    Public Property SUN_RDATE As Date? 'ngay tra the sungroup
    Public Property SUN_STATUS As String 'tinh trang the sungroup
    Public Property INSURANCE_CARD As Decimal? 'check chon the bao hiem y te
    Public Property INSURANCE_RDATE As Date? 'ngay tra the bao hiem y te
    Public Property INSURANCE_STATUS As String 'tinh trang the bao hiem y te
    Public Property REMAINING_LEAVE As Decimal? 'so phep con lai
    Public Property COMPENSATORY_LEAVE As Decimal? 'so ngay nghi bu
    Public Property IS_REMAINING_LEAVE As Boolean 'so phep con lai chuyển sang cty mới
    Public Property IS_COMPENSATORY_LEAVE As Boolean 'so ngay nghi bu chuyển sang cty mới

    Public Property EFFECT_DATE As Date?
    Public Property SIGN_DATE As Date?
    Public Property SIGN_ID As Decimal?
    Public Property SIGN_NAME As String
    Public Property SIGN_TITLE As String

    Public Property IDENTIFI_MONEY As Decimal?
    Public Property SUN_MONEY As Decimal?
    Public Property INSURANCE_MONEY As Decimal?

    Public Property WORK_STATUS As Decimal?
    Public Property EFFECT_FROM As Date?
    Public Property EFFECT_TO As Date?

End Class
