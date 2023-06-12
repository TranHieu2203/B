Public Class ProfileCommon
    Public Class ContractType
        Public Shared Probation As String = "HDTV"
        Public Shared Official As String = "HDTV"
    End Class
    Public Class OT_ASSET_STATUS
        Public Shared Name As String = "ASSET_STATUS"
        Public Shared TYPE_ID As Decimal = 6
    End Class
    Public Class OT_ASSET_GROUP
        Public Shared TYPE_ID As Decimal = 5
        Public Shared STATUS_TYPE_ID As Decimal = 6
    End Class
    Public Class OT_WORK_STATUS
        Public Shared Name As String = "WORK_STATUS"
        Public Shared TYPE_ID As Decimal = 59
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared TERMINATE_ID As Decimal = 257
        Public Shared WORKING_ID As Decimal = 258
        Public Shared WAIT_TERMINATE_ID As Decimal = 256
        Public Shared EMP_STATUS As Decimal = 7
    End Class
    Public Class DISCIPLINE_STATUS
        Public Shared Name As String = "DISCIPLINE_STATUS"
        Public Shared TYPE_ID As Decimal = 181
        Public Shared APPROVE_ID As Decimal = 716
        Public Shared WAIT_APPROVE_ID As Decimal = 717

    End Class
    Public Class DEBT_STATUS
        Public Shared Name As String = "DEBT_STATUS"
        Public Shared TYPE_ID As Decimal = 2271
        Public Shared COMPLETE_ID As Decimal = 7064
        Public Shared NOT_COMPLETE_ID As Decimal = 7065

    End Class
    Public Class DECISION_STATUS
        Public Shared Name As String = "DECISION_STATUS"
        Public Shared TYPE_ID As Decimal = 55
        Public Shared APPROVE_ID As Decimal = 447
        Public Shared WAIT_APPROVE_ID As Decimal = 446
        Public Shared NOT_APPROVE_ID As Decimal = 445
    End Class

    Public Class OT_DECISION_TYPE
        Public Shared Name As String = "DECISION_TYPE"
        Public Shared DISCIPLINE As String = "DISCIPLINE"
        Public Shared COMMEND As String = "COMMEND"
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared MISSION As String = "MISSION"
        Public Shared RecruitDecision As String = "QDTD"
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
        Public Shared APPROVE_ID As Decimal = 447
        Public Shared WAIT_APPROVE_ID As Decimal = 446
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

    Public Class OT_DASHBOARD_EMPLOYEE_STATISTIC
        Public Shared Name As String = "DB_EMP"
        Public Shared GENDER As String = "GENDER"
        Public Shared CONTRACT_TYPE As String = "CONTRACT_TYPE"
        Public Shared LEARNING As String = "LEARNING"
    End Class
    Public Class OT_DASHBOARD_CHANGE_STATISTIC
        Public Shared Name As String = "DB_CHANGE"
        Public Shared EMP_COUNT_YEAR As String = "EMP_COUNT_YEAR"
        Public Shared EMP_COUNT_MON As String = "EMP_COUNT_MON"
        Public Shared TER_CHANGE_MON As String = "TER_CHANGE_MON"
        Public Shared EMP_CHANGE_MON As String = "EMP_CHANGE_MON"
    End Class

    Public Class OT_TER_REASON
        Public Shared Name As String = "TER_REASON"
    End Class

    Public Class OT_CONTRACT_STATUS
        Public Shared Name As String = "CONTRACT_STATUS"
        Public Shared TYPE_ID As Decimal = 24
        Public Shared APPROVE As String = "1"
        Public Shared WAIT_APPROVE As String = "0"
        Public Shared APPROVE_ID As Decimal = 471
        Public Shared WAIT_APPROVE_ID As Decimal = 472
        Public Shared NOT_APPROVE_ID As Decimal = 4255

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

    Public Class OT_PEASSESSMENT
        Public Shared STATUS_ASS As Decimal = 6141
    End Class

    Public Class COMMEND_STATUS
        Public Shared Name As String = "COMMEND_STATUS"
        Public Shared APPROVE_ID As Decimal = 714
        Public Shared WAIT_APPROVE_ID As Decimal = 715
    End Class

    Public Enum TABLE_NAME
        HU_TITLE = 5
        HU_CONTRACT_TYPE = 6
        HU_WELFARE_LIST = 7
        HU_ALLOWANCE_LIST = 8
        HU_RELATIONSHIP_LIST = 9
        HU_NATION = 10
        HU_PROVINCE = 11
        HU_DISTRICT = 12
        HU_BANK = 13
        HU_BANK_BRANCH = 14
        HU_HOSPITAL = 15
        HU_ASSET = 16
        HU_WARD = 17
        HU_STAFF_RANK = 18
        HU_COMMENDLEVER = 19
        HU_PRO_TRAIN_OUT_COMPANY = 20
        HU_WORK_PLACE = 21
    End Enum
    Public Const GSIGNCODE_LEAVE As String = "LEAVE"
    Public Const GSIGNCODE_OVERTIME As String = "OVERTIME"
    Public Const GSIGNCODE_WLEO As String = "WLEO"
    Public Const ActiveStatus As String = "A"


End Class