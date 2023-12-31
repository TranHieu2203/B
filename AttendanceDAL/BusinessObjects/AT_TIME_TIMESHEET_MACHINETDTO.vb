﻿Public Class AT_TIME_TIMESHEET_MACHINETDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property VN_FULLNAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property WORKINGDAY As Date?
    Public Property FROM_DATE As Date?
    Public Property END_DATE As Date?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property STAFF_RANK_NAME As String
    Public Property CODE_HOURS As String
    Public Property TITLE_NAME As String
    Public Property PERIOD_ID As Decimal?
    Public Property IS_TERMINATE As Boolean?
    Public Property IS_DISSOLVE As Decimal?
    Public Property HOUR_LEAVE As Date?
    Public Property SHIFTIN As Date?
    Public Property SHIFTOUT As Date?
    Public Property SHIFTBACKOUT As Date?
    Public Property SHIFTBACKIN As Date?
    Public Property LEAVE_CODE As String
    Public Property MANUAL_CODE As String
    Public Property SHIFT_CODE As String
    Public Property LATE As Decimal?
    Public Property MORNING_ID As Decimal?
    Public Property MORNING_NAME As String
    Public Property SHIFT_ID As Decimal?
    Public Property ARTERNOON_ID As Decimal?
    Public Property ARTERNOON_NAME As String
    Public Property LATE_DEDUCTION As Boolean?
    Public Property WORKINGHOUR As Decimal?
    Public Property LATE_OFFICE As Boolean?
    Public Property COMEBACKOUT As Decimal?
    Public Property COMEBACKOUT_DEDUCTION As Boolean?
    Public Property COMEBACKOUT_OFFICE As Boolean?
    Public Property WORKDAY_OT As String
    Public Property WORKDAY_NIGHT As String
    Public Property LATE_PERMISSION As Boolean?
    Public Property COMEBACKOUT_PERMISSION As Boolean?
    Public Property VALIN1 As Date?
    Public Property VALIN2 As Date?
    Public Property VALIN3 As Date?
    Public Property VALIN4 As Date?
    Public Property TYPE_DAY As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property SALARIED_HOUR As Decimal?
    Public Property NOTSALARIED_HOUR As Decimal?
    Public Property OBJECT_ATTENDANCE As Decimal?
    Public Property OBJECT_ATTENDANCE_NAME As String
    Public Property MIN_IN_WORK As Decimal?
    Public Property MIN_OUT_WORK As Decimal?
    Public Property MIN_DEDUCT_WORK As Decimal?
    Public Property MIN_ON_LEAVE As Decimal?
    Public Property MIN_DEDUCT As Decimal?
    Public Property MIN_OUT_WORK_DEDUCT As Decimal?
    Public Property MIN_LATE As Decimal?
    Public Property MIN_EARLY As Decimal?
    Public Property MIN_LATE_EARLY As Decimal?
    Public Property WORK_HOUR As Decimal?
    Public Property TIMEVALIN_TEMP As Date?
    Public Property OBJECT_ATTENDANCE_CODE As String
    Public Property HOURS_STOP As Date?
    Public Property HOURS_START As Date?
    Public Property TIMEIN_REALITY As Date?
    Public Property TIMEOUT_REALITY As Date?
    Public Property START_MID_HOURS As Date?
    Public Property END_MID_HOURS As Date?
    Public Property WORKING_VALUE As Decimal?
    Public Property SHIFT_DAY As Decimal?
    Public Property SHIFT_TYPE_CODE As String
    Public Property SHIFT_TYPE_CODE0 As String
    Public Property SHIFT_TYPE_ID As Decimal?
    Public Property NOTE As String
    Public Property TIMEVALIN As Date?
    Public Property TIMEVALOUT As Date?
    Public Property IS_LATE As Boolean?
    Public Property IS_EARLY As Boolean?
    Public Property IS_REALITY As Boolean?
    Public Property IS_NON_WORKING_VALUE As Boolean?
    Public Property STATUS_SHIFT As Decimal?
    Public Property STATUS_SHIFT_NAME As String
    Public Property DAY_NUM As Decimal?
    Public Property EMP_OBJ_ID As Decimal?
    Public Property IS_WRONG_SHIFT As Boolean?

    Public Property STATUS As String
    Public Property STATUS_NAME As String
    Public Property MIN_NIGHT As Decimal?
    Public Property ORG_ACCOUNTING As String
    Public Property LIST_STATUS_SEARCH As List(Of String)

    Public Property TOMORROW_SHIFT_NAME As String
    Public Property TOMORROW_SHIFT As Decimal?
    Public Property TOMORROW_GOC_NAME As String
    Public Property TOMORROW_GOC As Decimal?
    Public Property TOMORROW_TT_NAME As String
    Public Property TOMORROW_TT As Decimal?
    Public Property DAYOFWEEK As String
    Public Property IS_LOCK_NAME As String
    Public Property MINUTE_DM As Decimal?
    Public Property MINUTE_VS As Decimal?
End Class
