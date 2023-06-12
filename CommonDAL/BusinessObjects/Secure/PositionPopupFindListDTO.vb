Public Class PositionPopupFindListDTO
    Public Property ID As Decimal
    Public Property POSITION_CODE As String
    Public Property FULLNAME_VN As String
    Public Property FULLNAME_EN As String
    Public Property IS_UYBAN As Decimal?
    Public Property IS_NONPHYSICAL As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property LoadAllOrganization As Boolean
    Public Property EMP_LM As String
    Public Property EMP_CSM As String
End Class
