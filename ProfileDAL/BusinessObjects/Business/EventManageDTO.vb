Public Class EventManageDTO
    Public Property ID As Decimal
    Public Property TITLE As String
    Public Property DETAIL As String
    Public Property ADD_TIME As Date?
    Public Property IS_SHOW As Boolean?
    Public Property STATUS_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property EMPLOYEE_CREATED As String
    Public Property START_DATE As Date?
    Public Property END_DATE As Date?
    Public Property ATTACH_FILE As String
    Public Property FILE_NAME As String
    Public Property EMPIDs As String
    Public Property USERNAME As String
    Public Property TEMPLATE_CODE As String
End Class
