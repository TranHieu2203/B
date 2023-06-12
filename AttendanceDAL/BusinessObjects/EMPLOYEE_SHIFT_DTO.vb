Public Class EMPLOYEE_SHIFT_DTO
    Public Property EMPLOYEE_ID As Decimal
    Public Property EFFECTIVEDATE As Date?
    Public Property ID_SIGN As Decimal
    Public Property SIGN_CODE As String
    Public Property SUBJECT As String
    Public Property IS_LEAVE As Boolean
    Public Property SHIFT_ID As Decimal?
    Public Property RecurrenceRule As String
    Public Property RecurrenceParentID As String
    Public Property HOURS As Decimal
    Public Property HOURS_START As Date?
    Public Property HOURS_END As Date?

    Public Property OT_HOUR_MIN As Decimal?
    Public Property OT_HOUR_MAX As Decimal?
    Public Property IS_TOMORROW As Boolean?
    Public Property IS_HOURS_STOP As Boolean?
End Class
