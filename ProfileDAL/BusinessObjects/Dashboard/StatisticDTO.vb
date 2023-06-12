Public Class StatisticDTO
    Public Property NAME As String
    Public Property VALUE As Decimal
    Public Property VALUE_SECOND As Decimal
End Class

Public Class AT_PORTAL_REG_DTO
    Public Property ID As Decimal
    Public Property ID_EMPLOYEE As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property PROCESS As String
    Public Property DISPLAY As String

    Public Property ID_SIGN As Decimal
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property FROM_HOUR As Date?
    Public Property TO_HOUR As Date?
    Public Property DAYCOUNT As Decimal?
    Public Property SDAYCOUNT As String
    Public Property HOURCOUNT As Decimal?
    Public Property REGDATE As Date?
    Public Property NOTE As String ' ghi chu
    Public Property NOTE_AT As String
    Public Property STATUS As Int16
    Public Property STATUS_NAME As String

    Public Property SIGN_CODE As String
    Public Property SIGN_NAME As String ' loai dang ky
    Public Property GSIGN_CODE As String
    Public Property APP_DATE As Date?
    Public Property APP_LEVEL As Integer?

    Public Property NVALUE As Nullable(Of Decimal)
    Public Property NVALUE_ID As Decimal?
    Public Property NVALUE_NAME As String
    Public Property SVALUE As String
    Public Property DVALUE As Nullable(Of Date)

    Public Property ID_REGGROUP As Guid?
    Public Property LINK_POPUP As String
End Class
