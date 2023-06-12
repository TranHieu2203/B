Public Class ATConstant
    Public Const HISTAFF_SHAREDFOLDER_USERNAME As String = "SFUSERNAME"
    Public Const HISTAFF_SHAREDFOLDER_PASSWORD As String = "SFPASSWORD"
    Public Const HISTAFF_SHAREDFOLDER_DOMAIN As String = "SFDOMAIN"

    Public Const HISTAFF_INITDATA_TIME As String = "ATINITTIME"

    Public Const CONFIG_FEXESLEEP As String = "FEXESLEEP"

    Public Const ACTFLG_ACTIVE As String = "A"
    Public Const ACTFLG_DEACTIVE As String = "I"


    Public Const CODE_SIGNMODE As String = "AT_SIGN_MODE"
    Public Const CODE_SIGNTYPE As String = "AT_SIGN_TYPE"
    Public Const CODE_PERIODSTATUS As String = "AT_PERIODSTATUS"
    Public Const CODE_TIMELEAVE As String = "AT_TIMELEAVE"

    Public Const PERIODSTATUS_OPEN As String = "O"
    Public Const PERIODSTATUS_CLOSE As String = "C"
    Public Const PERIODSTATUS_FINALCLOSE As String = "FC"
    Public Const PERIODSTATUS_REOPEN As String = "RO"

    Public Const GSIGNCODE_SHIFT As String = "SHIFT"
    Public Const GSIGNCODE_LEAVE As String = "LEAVE"
    Public Const GSIGNCODE_OVERTIME As String = "OVERTIME"
    Public Const GSIGNCODE_LB As String = "LB"
    Public Const GSIGNCODE_INOUT As String = "INOUT"
    Public Const GSIGNCODE_WLEO As String = "WLEO"
    Public Const GSIGNCODE_WORK As String = "WORK"
    Public Const GSIGNCODE_AL As String = "AL"
    Public Const GSIGNCODE_OTHERS As String = "OTHERS"

    Public Const GSIGNCODE_TIME As String = "TIME" ' BangDV: Thêm để phục vụ cho phần phê duyệt

    Public Const SIGNTYPECODE_STRING As String = "STRING"
    Public Const SIGNTYPECODE_DATETIME As String = "DATETIME"
    Public Const SIGNTYPECODE_NUMBER As String = "NUMBER"

    Public Const SIGNMODECODE_REGISTER As String = "RGT"
    Public Const SIGNMODECODE_SUMMARY As String = "SUM"

    Public Const EXTENVALUE_FORMAT As String = "[NEXT1-NEXT2-NEXT3-SEXT1-SEXT2-SEXT3-DEXT1-DEXT2-DEXT3]"

    Public Const FORMULAR_VIEWNAME_FORMAT As String = "ATV_{0}_{1}"

    Public Const FORMULAR_STATUS_REGIST As String = "R"
    Public Const FORMULAR_STATUS_PROCESS As String = "P"
    Public Const FORMULAR_STATUS_COMPLETE As String = "C"
    Public Const FORMULAR_STATUS_EXCEPTION As String = "E"

    Public Const SYSTEM_USERNAME As String = "SYSTEM"
    Public Const HU_TERMINATE As Decimal = 257
End Class

Public Class OT_AT_TIMELEAVE
    Public Shared AM As Decimal = 342
    Public Shared PM As Decimal = 340
    Public Shared CANGAY As Decimal = 341
End Class

Public Class OT_TRANSFER_STATUS
    Public Shared APPROVE As Decimal = 447
    Public Shared WAIT As Decimal = 446
End Class

Public Enum RegisterStatus
    Regist = 0
    WaitForApprove = 1
    Approved = 2
    Denied = 3
End Enum