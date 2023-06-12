Public Class DocumentDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME_EN As String
    Public Property NAME_VN As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property CHK_MUST_HAVE As Decimal?
    Public Property MUST_HAVE As Boolean
    Public Property TYPE_DOCUMENT_ID As Decimal?
    Public Property TYPE_DOCUMENT_NAME As String
    Public Property ALLOW_UPLOAD_FILE As Boolean
End Class
