Public Class AT_PORTAL_REG_DTO
    Public Property ID As Decimal
    Public Property ID_EMPLOYEE As Decimal
    Public Property ID_EMPLOYEE1 As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property PROCESS As String
    Public Property DISPLAY As String
    Public Property JOBTITLE As String
    Public Property DEPARTMENT As String

    Public Property AT_PORTAL_REG_LIST_ID As Decimal?
    Public Property ID_SIGN As Decimal?
    Public Property FROM_DATE As Date?
    Public Property CREATED_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property FROM_HOUR As Date?
    Public Property TO_HOUR As Date?
    Public Property DAYCOUNT As Decimal?
    Public Property SDAYCOUNT As String
    Public Property HOURCOUNT As Decimal?
    Public Property REGDATE As Date?
    Public Property NOTE As String ' ghi chu
    Public Property NOTE_AT As String
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property REASON As String

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

    Public Property ID_REGGROUP As Decimal?
    Public Property LINK_POPUP As String

    Public Property ID_NB As Decimal?
    Public Property NGHI_BU As String
    Public Property YEAR As Decimal?
    Public Property TOTAL_LEAVE As Decimal?
    Public Property DAYIN_KH As Decimal?
    Public Property DAYOUT_KH As Decimal?
    Public Property WORK_HARD As Decimal? ' CHECKBOX NGAY LAM VIEC
    Public Property MODIFIED_BY As String

    Public Property MODIFIED_DATE As Date?

    Public Property IS_WORK_DAY As Boolean?


    Public Property REGIST_INFO As Decimal?
    Public Property REGIST_INFO_NAME As String
    Public Property ORG_CHECK_IN As Decimal?
    Public Property ORG_CHECK_IN_NAME As String
    Public Property MINUTE As Decimal?
    Public Property WORKINGDAY As Date?
    Public Property REASON_NOT_AGREE As String
    Public Property FROM_DATE_STR As String
    Public Property TO_DATE_STR As String
End Class
