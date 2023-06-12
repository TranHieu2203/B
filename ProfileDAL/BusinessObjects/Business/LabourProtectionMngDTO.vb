Public Class LabourProtectionMngDTO
    Public Property ID As Decimal?
    Public Property LABOURPROTECTION_ID As Decimal?
    Public Property LABOURPROTECTION_NAME As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property LABOUR_SIZE_ID As Decimal?
    Public Property LABOUR_SIZE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property STAFF_RANK_NAME As String
    Public Property TITLE_NAME As String
    Public Property QUANTITY As Decimal?
    Public Property UNIT_PRICE As Decimal?
    Public Property DAYS_ALLOCATED As Date?
    Public Property RETRIEVE_DATE As Date?
    Public Property RETRIEVED As Boolean?
    Public Property IS_RETRIEVED As Boolean?
    Public Property RECOVERY_DATE As Date?
    Public Property INDEMNITY As Decimal?
    Public Property DEPOSIT As Decimal?
    Public Property SDESC As String
    Public Property CREATED_DATE As Date?
    ' thông tin tìm kiếm
    Public Property IS_TERMINATE As Boolean?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_LAST_DATE As Date?
    Public Property FROM_DATE_SEARCH As Date?
    Public Property TO_DATE_SEARCH As Date?

    Public Property DATE_USE As Decimal?
End Class
