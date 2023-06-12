Public Class EmployeeDTO

    Public Property EMPLOYEE_ID As Decimal?
    Public Property VN_FULLNAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String

    Public Property SO As Boolean?
    Public Property HE As Boolean?
    Public Property UE As Boolean?

    Public Property NGAYSINH As Date?
    Public Property CMND As String
    Public Property NOISINH As String
    Public Property NOICAP As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property MODIFY_TYPE As String '0: not modify, 1: modify image, 2: modify info
End Class
