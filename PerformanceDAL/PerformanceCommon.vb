Public Class PerformanceCommon

    Public Enum TABLE_NAME
        PE_CRITERIA = 1
        PE_PERIOD = 2
        PE_OBJECT_GROUP = 3
    End Enum
    Public Class OT_WORK_STATUS
        Public Shared Name As String = "WORK_STATUS"
        Public Shared TYPE_ID As Decimal = 59
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared TERMINATE_ID As Decimal = 257
        Public Shared WORKING_ID As Decimal = 258
        Public Shared WAIT_TERMINATE_ID As Decimal = 259
    End Class
End Class
