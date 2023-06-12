Public Class OrganizationDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME_VN As String
    Public Property NAME_EN As String
    Public Property PARENT_ID As Decimal?
    Public Property PARENT_NAME As String
    Public Property IS_NOT_PER As Boolean
    Public Property FOUNDATION_DATE As Date?
    Public Property DISSOLVE_DATE As Date?
    Public Property HIERARCHICAL_PATH As String
    Public Property DESCRIPTION_PATH As String
    Public Property REMARK As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property ORD_NO As Decimal?
    Public Property COST_CENTER_CODE As String
    Public Property SHORT_NAME As String
    Public Property UY_BAN As Decimal?
    Public Property CHILD_COUNT As Decimal
End Class
