Public Class CommonCommon
    Public Class OT_WORK_STATUS
        Public Shared Name As String = "WORK_STATUS"
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared TERMINATE_ID As Decimal = 257
    End Class

    Public Enum TABLE_NAME
        OT_OTHER_LIST = 1
        OT_OTHER_LIST_GROUP = 2
        OT_OTHER_LIST_TYPE = 3
    End Enum

End Class

Public Enum SystemCodes As Integer
    Profile = 1
    Attendance = 2
    Payroll = 3
    Recruitment = 16
    Training = 17
    Performance = 19
    Organize = 20
    Insurance = 18
End Enum