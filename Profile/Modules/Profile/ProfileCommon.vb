Public Class ProfileCommon
    Public Class ContractType
        Public Shared Probation As String = "HDTV"
        Public Shared Official As String = "HDCT"
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

    Public Class DECISION_STATUS
        Public Shared Name As String = "DECISION_STATUS"
        Public Shared TYPE_ID As Decimal = 55
        Public Shared APPROVE_ID As Decimal = 447
        Public Shared WAIT_APPROVE_ID As Decimal = 446
        Public Shared NOT_APPROVE_ID As Decimal = 445
    End Class
    Public Class DECISION_TYPE
        Public Shared Name As String = "DECISION_TYPE"
        Public Shared ID As Decimal = 28
        Public Shared Promotion = "QDTT"
        Public Shared AffectSalary = "QDAHL"
    End Class
    Public Class OT_CONTRACT_STATUS
        Public Shared Name As String = "CONTRACT_STATUS"
        Public Shared APPROVE_ID As Decimal = 471
        Public Shared WAIT_APPROVE_ID As Decimal = 472
        Public Shared NOT_APPROVE_ID As Decimal = 4255
        Public Shared TYPE_ID As Decimal = 24
    End Class
    Public Class COMMEND_STATUS
        Public Shared Name As String = "COMMEND_STATUS"
        Public Shared TYPE_ID As Decimal = 180
        Public Shared APPROVE_ID As Decimal = 714
        Public Shared WAIT_APPROVE_ID As Decimal = 715
    End Class
    Public Class DEBT_STATUS
        Public Shared Name As String = "DEBT_STATUS"
        Public Shared TYPE_ID As Decimal = 2271
        Public Shared COMPLETE_ID As Decimal = 7064
        Public Shared NOT_COMPLETE_ID As Decimal = 7065
    End Class
    Public Class DISCIPLINE_STATUS
        Public Shared Name As String = "DISCIPLINE_STATUS"
        Public Shared TYPE_ID As Decimal = 181
        Public Shared APPROVE_ID As Decimal = 447
        Public Shared WAIT_APPROVE_ID As Decimal = 446
    End Class
    Public Class DISCIPLINE_TYPE
        Public Shared Name As String = "DISCIPLINE_TYPE"
        Public Shared TYPE_ID As Decimal = 31
        Public Shared LAYOFF_ID As Decimal = 462
    End Class
    Public Class OT_TER_STATUS
        Public Shared Name As String = "TER_STATUS"
        Public Shared TYPE_ID As Decimal = 52
        Public Shared APPROVE_ID As Decimal = 262
        Public Shared WAIT_APPROVE_ID As Decimal = 263
        Public Shared NOT_APPROVE_ID As Decimal = 264
    End Class
    Public Class HU_TEMPLATE_TYPE
        Public Shared Name As String = "HU_TEMPLATE_TYPE"
        Public Shared CONTRACT_ID As Decimal = 1
        Public Shared DECISION_ID As Decimal = 2
        Public Shared DISCIPLINE_ID As Decimal = 3
        Public Shared TERMINATE_ID As Decimal = 4
        Public Shared TERMINATE3B_ID As Decimal = 5
        Public Shared CONTRACT_SUPPORT_ID As Decimal = 6
        Public Shared DECISION_SUPPORT_ID As Decimal = 7
        Public Shared DISCIPLINE_SUPPORT_ID As Decimal = 8
        Public Shared TERMINATE_SUPPORT_ID As Decimal = 9
        Public Shared PROFILE_SUPPORT_ID As Decimal = 10
        Public Shared APPENDIX_SUPPORT_ID As Decimal = 11
    End Class
    Public Class TERRMINATE_SUPPORT
        Public Shared Name As String = "TERMINATE_SUPPORT"
    End Class
    Public Class COMMEND_OBJECT
        Public Shared Name As String = "COMMEND_OBJECT"
    End Class
    Public Class OT_SIZE
        Public Shared Code As String = "SIZE_LABOUR"
    End Class
    Public Class OT_RESON
        Public Shared Code As String = "REASON"
        Public Shared TYPE_ID As Decimal = 2026
    End Class
    Public Class HU_ASSET_MNG_STATUS
        Public Shared Name As String = "HU_ASSET_MNG_STATUS"
        Public Shared ASSET_TRANSFER As Decimal = 483
        Public Shared ASSET_NEW As Decimal = 484
        Public Shared ASSET_WAIT As Decimal = 485
    End Class
    Public Class TER_STATUS
        Public Shared Code As String = "TER_STATUS"
        Public Shared TYPE_ID As Decimal = 52
    End Class
    Public Class HU_COMMEND_STATUS
        Public Shared Name As String = "HU_COMMEND_STATUS"
        Public Shared APPROVE As Decimal = 447
        Public Shared WAIT_APPROVE As Decimal = 446
    End Class
    Public Class CONTRACT_SUPPORT
        Public Shared Code As String = "CONTRACT_SUPPORT"
        Public Shared TYPE_ID As Decimal = 2036
    End Class

    Public Class OBJECT_ATTENDANCE
        Public Shared Code As String = "OBJECT_ATTENDANCE"
        Public Shared TYPE_ID As Decimal = 2254
    End Class

    Public Class OT_ASSET_GROUP
        Public Shared Code As String = "ASSET_GROUP"
        Public Shared TYPE_ID As Decimal = 5
    End Class
    Public Const ActiveStatus As String = "A"
    Public Class Commend_Pay
        Public Shared TienMat As String = "TM"
    End Class
    Public Class SalaryType
        Public Shared ThuViec As String = "TV"
        Public Shared ChinhThuc As String = "CT"
    End Class
    Public Class TaxTable
        Public Shared Thue10 As String = "T10"
        Public Shared Thue20 As String = "T20"
        Public Shared ThueLuyTien As String = "TLT"
    End Class
End Class
