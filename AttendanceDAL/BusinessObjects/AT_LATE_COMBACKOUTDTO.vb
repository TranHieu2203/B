Public Class AT_LATE_COMBACKOUTDTO
    Public Property ID As Decimal?
    Public Property LATE_COMBACKOUT_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property VN_FULLNAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property WORKINGDAY As Date?

    Public Property FROM_DATE As Date?
    Public Property END_DATE As Date?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_NAME As String
    Public Property PERIOD_ID As Decimal?

    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String

    Public Property IS_TERMINATE As Boolean?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_LAST_DATE As Date?

    Public Property TYPE_DMVS_ID As Decimal?
    Public Property TYPE_DMVS_NAME As String
    Public Property FROM_HOUR As Date?
    Public Property TO_HOUR As Date?
    Public Property MINUTE As Decimal?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property REGIST_INFO As Decimal?
    Public Property REGIST_INFO_NAME As String

    Public Property ORG_CHECK_IN As Decimal?
    Public Property ORG_CHECK_IN_NAME As String

    Public Property IS_APP As Boolean?
    Public Property IS_SEND As Boolean?
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property REASON As String

    Public Property ID_AT_SWIPE As String
    Public Property EMP_APPROVES_NAME As String

    Public Property SHIFT_ID As Decimal?
    Public Property SHIFT_CODE As String
    Public Property SHIFT_NAME As String
    Public Property SHIFT_START As Date?
    Public Property SHIFT_END As Date?

    Public Property TIME_REALITY As String
    Public Property TIMEIN_REALITY As Date?
    Public Property TIMEOUT_REALITY As Date?
    Public Property MIN_LATE_EARLY As Decimal?
    Public Property WORK_EMAIL As String
    Public Property ORDER_NUM As Decimal?

    Public Property STATUS_ID1 As Decimal?
    Public Property STATUS_NAME1 As String
    Public Property TS_EXCEPTION As Boolean?

    Public Property LIST_STATUS_SEARCH As List(Of String)
    Public Property STATUS_TYPE_CODE As String

    Public Property TIME As String
    Public Property REASON_ID As Decimal?
    Public Property REASON_NAME As String

    Public Property VIOLATION_RANGE_ID As Decimal?
    Public Property VIOLATION_RANGE_NAME As String
    Public Property VIOLATE_MINUTE As Decimal?
End Class
