Public Class OccupationalSafetyDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal
    Public Property ORG_NAME As String
    Public Property TITLE_NAME As String
    Public Property DATE_OF_ACCIDENT As Date?
    Public Property REASON_ID As Decimal?
    Public Property REASON_NAME As String
    Public Property HOLIDAY_ACCIDENTS As Decimal?
    Public Property DESCRIBED_INCIDENT As String
    Public Property EXTENT_OF_INJURY As String
    Public Property THE_COST_OF_ACCIDENTS As Decimal?
    Public Property SDESC As String
    Public Property CREATED_DATE As Date?
    ' thông tin tìm kiếm
    Public Property IS_TERMINATE As Boolean?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_LAST_DATE As Date?
    Public Property FROM_DATE_SEARCH As Date?
    Public Property TO_DATE_SEARCH As Date?
End Class
