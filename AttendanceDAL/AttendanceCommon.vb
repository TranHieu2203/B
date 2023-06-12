Public Class AttendanceCommon
    Public Class OT_ASSET_STATUS
        Public Shared Name As String = "ASSET_STATUS"
        Public Shared STATUS_WAIT As String = "STATUS_WAIT"
        Public Shared STATUS_NEW As String = "STATUS_NEW"
        Public Shared STATUS_TRANSFER As String = "STATUS_TRANSFER"
    End Class
    Public Class OT_WORK_STATUS
        Public Shared Name As String = "WORK_STATUS"
        Public Shared TYPE_ID As Decimal = 59
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared MISSION As String = "MISSION"
        Public Shared TERMINATE_ID As Decimal = 257
    End Class
    Public Class OT_TRANSFER_STATUS
        Public Shared Name As String = "TRANSFER_STATUS"
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
        Public Shared APPROVE_ID As Decimal = 447
        Public Shared WAIT_APPROVE_ID As Decimal = 446
    End Class
    Public Class OT_DECISION_TYPE
        Public Shared Name As String = "DECISION_TYPE"
        Public Shared DISCIPLINE As String = "DISCIPLINE"
        Public Shared COMMEND As String = "COMMEND"
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared MISSION As String = "MISSION"
    End Class

    Public Class OT_TER_STATUS
        Public Shared Name As String = "TER_STATUS"
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
        Public Shared APPROVE_ID As Decimal = 262
        Public Shared WAIT_APPROVE_ID As Decimal = 263
    End Class

    Public Class OT_COMMEND_STATUS
        Public Shared Name As String = "COMMEND_STATUS"
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
        Public Shared APPROVE_ID As Decimal = 714
        Public Shared WAIT_APPROVE_ID As Decimal = 715
    End Class

    Public Class OT_DISCIPLINE_STATUS
        Public Shared Name As String = "DISCIPLINE_STATUS"
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
        Public Shared APPROVE_ID As Decimal = 716
        Public Shared WAIT_APPROVE_ID As Decimal = 717
    End Class

    Public Class OT_ORG_LEVEL
        Public Shared Name As String = "ORG_LEVEL"
        Public Shared LOCATION_BASE As String = "1"
        Public Shared DEPARTMENT As String = "2"
        Public Shared SUB_DEPARTMENT As String = "3"
        Public Shared [FUNCTION] As String = "4"
        Public Shared SUB_FUNCTION As String = "5"
        Public Shared SECTION As String = "6"
        Public Shared [GROUP] As String = "7"
    End Class
    Public Class OT_GENDER
        Public Shared Name As String = "GENDER"
        Public Shared MALE As String = "0"
        Public Shared FEMALE As String = "1"
        Public Shared MALE_ID As Decimal = 565
        Public Shared FEMALE_ID As Decimal = 566
    End Class
    Public Class OT_RECRUITMENT_REASON
        Public Shared Name As String = "RECRUITMENT_REASON"
        Public Shared IN_AOP As String = "IN_AOP"               'Trong kế hoạch
        Public Shared OUT_AOP As String = "OUT_AOP"             'Ngoài kế hoạch
        Public Shared REPLACEMENT As String = "REPLACEMENT"     'Tuyển thay thế
        Public Shared NOT_PRESENT As String = "NOT_PRESENT"     'Không hiện diện
        Public Shared OTHER As String = "OTHER"                 'Lý do khác
    End Class
    Public Class OT_DASHBOARD_EMPLOYEE_STATISTIC
        Public Shared Name As String = "DB_EMP"
        Public Shared GENDER As String = "GENDER"
        Public Shared CONTRACT_TYPE As String = "CONTRACT_TYPE"
    End Class
    Public Class OT_DASHBOARD_CHANGE_STATISTIC
        Public Shared Name As String = "DB_CHANGE"
        Public Shared EMPLOYEE_COUNT_FOLLOW_YEAR As String = "EMP_COUNT_YEAR"
        Public Shared EMPLOYEE_COUNT_FOLLOW_MONTH As String = "EMP_COUNT_MONTH"
        Public Shared EMPLOYEE_CHANGE_FOLLOW_YEAR As String = "EMP_CHANGE_YEAR"
        Public Shared EMPLOYEE_CHANGE_FOLLOW_MONTH As String = "EMP_CHANGE_MON"
    End Class
    Public Class OT_TRANSFER_TYPE
        Public Shared Name As String = "TRANSFER_TYPE"
    End Class
    Public Class OT_RELATION
        Public Shared Name As String = "RELATION"
    End Class
    Public Class NATION_VN
        Public Shared CODE As String = "VN"

    End Class

    Public Class OT_TER_REASON
        Public Shared Name As String = "TER_REASON"
    End Class

    Public Class OT_CONTRACT_STATUS
        Public Shared Name As String = "CONTRACT_STATUS"
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
    End Class
    Public Class OT_COMMEND
        Public Shared COMMEND_LEVEL As String = "COMMEND_LEVEL"
        Public Shared COMMEND_OBJECT As String = "COMMEND_OBJECT"
        Public Shared COMMEND_TYPE As String = "COMMEND_TYPE"
    End Class
    Public Class OT_DISCIPLINE
        Public Shared DISCIPLINE_LEVEL As String = "DISCIPLINE_LEVEL"
        Public Shared DISCIPLINE_OBJECT As String = "DISCIPLINE_OBJECT"
        Public Shared DISCIPLINE_TYPE As String = "DISCIPLINE_TYPE"
    End Class


    Public Class OT_WORKING_TYPE
        Public Shared TRANSFER_CHANGE_SALARY As Integer = 0
        Public Shared TRANSFER As Integer = 1
        Public Shared CHANGE_SALARY As Integer = 2
        Public Shared NO_CHANGE As Integer = 3
    End Class

    Public Enum TABLE_NAME
        AT_HOLIDAY = 3
        AT_HOLIDAY_GENERAL = 4
        AT_FML = 5
        AT_GSIGN = 6
        AT_DMVS = 7
        AT_SHIFT = 8
        AT_HOLIDAY_OBJECT = 9
        AT_SETUP_SPECIAL = 10
        AT_SETUP_TIME_EMP = 11
        AT_TERMINALS = 12
        AT_SIGNDEFAULT = 13
        AT_TIME_MANUAL = 14
        AT_LIST_PARAM_SYSTEM = 15
        AT_PROJECT_EMP = 16
    End Enum
End Class
