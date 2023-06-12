Public Class PAConstant

    Public Const ACTFLG_ACTIVE As String = "A"
    Public Const ACTFLG_DEACTIVE As String = "I"
    Public Const SALARYMODECODE_SUMMARY As String = "SUM"
    Public Const SALARYMODECODE_IMPORT As String = "IMPORT"

    Public Const FORMULAR_STATUS_REGIST As String = "R"
    Public Const FORMULAR_STATUS_PROCESS As String = "P"
    Public Const FORMULAR_STATUS_COMPLETE As String = "C"
    Public Const FORMULAR_STATUS_EXCEPTION As String = "E"

    Public Const CODE_SALARYMODE As String = "PA_SALARY_MODE"
    Public Const CODE_SALARYTYPE As String = "PA_SALARY_TYPE"
    Public Const CODE_PERIODSTATUS As String = "AT_PERIODSTATUS"

    Public Const PERIODSTATUS_OPEN As String = "O"
    Public Const PERIODSTATUS_CLOSE As String = "C"
    Public Const PERIODSTATUS_FINALCLOSE As String = "FC"
    Public Const PERIODSTATUS_REOPEN As String = "RO"

    Public Const HU_TERMINATE As Decimal = 257
    Public Const GSALARY_INPUT As String = "INPUT"

    'PhongDV
    Public Const CODE_FAMILY_CONDITION As String = "PA_FAMILY_CONDITION"
    '''''''''''''''
End Class

Public Class OT_TRANSFER_STATUS
    Public Shared APPROVE As Decimal = 447
    Public Shared WAIT As Decimal = 446
End Class

Public Class PA_GSALARY
    Public Shared OUTPUT As Decimal = 1
    Public Shared TAX As Decimal = 2
    Public Shared INPUT As Decimal = 3
    Public Shared IMPORT As Decimal = 4
    Public Shared ADVANCE As Decimal = 6
End Class

Public Class PA_SALARY_TYPE
    Public Shared DATETIME As String = "DATETIME"
    Public Shared NUMBER As String = "NUMBER"
    Public Shared NVARCHAR As String = "STRING"
    Public Shared DATETIME_ID As Decimal = 443
    Public Shared NUMBER_ID As Decimal = 444
    Public Shared NVARCHAR_ID As Decimal = 445
End Class