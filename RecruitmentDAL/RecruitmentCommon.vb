Public Class RecruitmentCommon
    Public Class OT_WORK_STATUS
        Public Shared Name As String = "WORK_STATUS"
        Public Shared TYPE_ID As Decimal = 59
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared MISSION As String = "MISSION"
        Public Shared TERMINATE_ID As Decimal = 257
    End Class

    Public Class RC_RECRUIT_REASON
        Public Shared Name As String = "RC_RECRUIT_REASON"
        Public Shared TYPE_ID As Decimal = 1010
    End Class
    Public Class RC_PLAN_REG_STATUS
        Public Shared Name As String = "RC_PLAN_REG_STATUS"
        Public Shared TYPE_ID As Decimal = 1011
        Public Shared APPROVE_ID As Decimal = 4051
        Public Shared WAIT_ID As Decimal = 4050
        Public Shared NOT_APPROVE_ID As Decimal = 4052
    End Class

    Public Class RC_REQUEST_STATUS
        Public Shared Name As String = "RC_REQUEST_STATUS"
        Public Shared TYPE_ID As Decimal = 1012
        Public Shared APPROVE_ID As Decimal = 4101
        Public Shared WAIT_ID As Decimal = 4100
        Public Shared NOT_APPROVE_ID As Decimal = 4102
    End Class

    Public Class RC_RECRUIT_TYPE
        Public Shared Name As String = "RC_RECRUIT_TYPE"
        Public Shared TYPE_ID As Decimal = 1013
    End Class

    Public Class RC_GROUP_WORK
        Public Shared Name As String = "RC_GROUP_WORK"
        Public Shared TYPE_ID As Decimal = 1014
    End Class

    Public Class RC_PRIORITY_LEVEL
        Public Shared Name As String = "RC_PRIORITY_LEVEL"
        Public Shared TYPE_ID As Decimal = 1015
    End Class

    Public Class RC_TRAINING_LEVEL
        Public Shared Name As String = "RC_TRAINING_LEVEL"
        Public Shared TYPE_ID As Decimal = 1016
    End Class

    Public Class RC_TRAINING_SCHOOL
        Public Shared Name As String = "RC_TRAINING_SCHOOL"
        Public Shared TYPE_ID As Decimal = 1017
    End Class

    Public Class RC_MAJOR
        Public Shared Name As String = "RC_MAJOR"
        Public Shared TYPE_ID As Decimal = 1018
    End Class

    Public Class RC_GRADUATION_TYPE
        Public Shared Name As String = "RC_GRADUATION_TYPE"
        Public Shared TYPE_ID As Decimal = 1019
    End Class

    Public Class RC_LANGUAGE
        Public Shared Name As String = "RC_LANGUAGE"
        Public Shared TYPE_ID As Decimal = 1020
    End Class

    Public Class RC_LANGUAGE_LEVEL
        Public Shared Name As String = "RC_LANGUAGE_LEVEL"
        Public Shared TYPE_ID As Decimal = 1021
    End Class

    Public Class RC_COMPUTER_LEVEL
        Public Shared Name As String = "RC_COMPUTER_LEVEL"
        Public Shared TYPE_ID As Decimal = 1022
    End Class

    Public Class RC_APPEARANCE
        Public Shared Name As String = "RC_APPEARANCE"
        Public Shared TYPE_ID As Decimal = 1023
    End Class

    Public Class RC_HEALTH_STATUS
        Public Shared Name As String = "RC_HEALTH_STATUS"
        Public Shared TYPE_ID As Decimal = 1024
    End Class

    Public Class RC_PROGRAM_STATUS
        Public Shared Name As String = "RC_PROGRAM_STATUS"
        Public Shared TYPE_ID As Decimal = 1025
    End Class

    Public Class RC_RECRUIT_SCOPE
        Public Shared Name As String = "RC_RECRUIT_SCOPE"
        Public Shared TYPE_ID As Decimal = 1026
    End Class

    Public Class RC_SOFT_SKILL
        Public Shared Name As String = "RC_SOFT_SKILL"
        Public Shared TYPE_ID As Decimal = 1027
    End Class

    Public Class RC_CHARACTER
        Public Shared Name As String = "RC_CHARACTER"
        Public Shared TYPE_ID As Decimal = 1028
    End Class

    Public Class RC_CANDIDATE_STATUS
        'Public Shared Name As String = "RC_CANDIDATE_STATUS"
        'Public Shared TYPE_ID As Decimal = 1029
        'Public Shared DUDIEUKIEN_ID As Decimal = 4105
        'Public Shared KHONGDUDIEUKIEN_ID As Decimal = 4106
        'Public Shared THIDAT_ID As Decimal = 4107
        'Public Shared TRUNGTUYEN_ID As Decimal = 4108
        'Public Shared GUITHU_ID As Decimal = 4109
        'Public Shared LANHANVIEN_ID As Decimal = 4110
        'Public Shared KHONGTHIDAT_ID As Decimal = 4111
        'Public Shared TUCHOI_ID As Decimal = 4112
        Public Shared Name As String = "RC_CANDIDATE_STATUS"
        Public Shared TYPE_ID As String = 1029
        Public Shared DUDIEUKIEN_ID As String = "DUDK"
        Public Shared KHONGDUDIEUKIEN_ID As String = "KDUDK"
        Public Shared THIDAT_ID As String = "DAT"
        Public Shared TRUNGTUYEN_ID As String = "TRUNGTUYEN"
        Public Shared GUITHU_ID As String = "THUMOI"
        Public Shared LANHANVIEN_ID As String = "NHANVIEN"
        Public Shared KHONGTHIDAT_ID As String = "KDAT"
        Public Shared TUCHOI_ID As String = "TUCHOI"
        Public Shared NOIBO_ID As String = "NOIBO"
        Public Shared PONTENTIAL As String = "PONTENTIAL"
        Public Shared DATLICH As String = "DATLICH"
    End Class


    Public Enum TABLE_NAME
        HU_COST_CENTER = 1
    End Enum

End Class
