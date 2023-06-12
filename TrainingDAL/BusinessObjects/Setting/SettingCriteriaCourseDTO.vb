Public Class SettingCriteriaCourseDTO
    Public Property ID As Decimal
    Public Property TR_COURSE_ID As Decimal?
    Public Property TR_COURSE_NAME As String
    Public Property TR_CRITERIA_GROUP_ID As Decimal?
    Public Property TR_CRITERIA_GROUP_NAME As String
    Public Property EFFECT_FROM As Date?
    Public Property EFFECT_TO As Date?
    Public Property REMARK As String
    Public Property SCALE_POINT As Decimal?
    Public SettingCriteriaDetail As List(Of SettingCriteriaDetailDTO)
End Class
