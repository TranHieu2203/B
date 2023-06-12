Public Class OrgChartDTO
    Public Property ID As Decimal
    Public Property PARENT_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property FIRST_NAME_VN As String
    Public Property LAST_NAME_VN As String
    Public Property FULLNAME_VN As String
    Public Property ORG_NAME As String
    Public Property EMP_COUNT As String
    Public Property TITLE_NAME_VN As String
    Public Property IMAGE As String
    Public Property IMAGE_BINARY As Byte()
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIL As String
    Public Property ORG_CODE As String
    Public Property ORG_LEVEL As Decimal?
End Class
