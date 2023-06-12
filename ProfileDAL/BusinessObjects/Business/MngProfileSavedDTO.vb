Public Class MngProfileSavedDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property DOCUMENT_ID As Decimal?
    Public Property DOCUMENT_NAME As String
    Public Property DATE_SUBMIT As Date?
    Public Property IS_SUBMITED As Decimal?
    Public Property REMARK As String
    Public Property UPLOAD_FILE As String
    Public Property FILE_NAME As String
    Public Property LIST_MNG As List(Of MngProfileSavedDTO)
    Public Property MUST_HAVE As Boolean
End Class