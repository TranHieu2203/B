Public Class FileUploadDTO
    Public Property ID As Decimal
    Public Property LINK As String
    Public Property NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property FOLDER_ID As Decimal?
    Public Property FILE_NAME As String
    Public Property DESCRIPTION As String
    Public Property CODE_PATH As String
    Public Property COLUMNS As String
End Class

Public Class FolderDTO
    Public Property ID As Decimal
    Public Property LINK As String
    Public Property FOLDER_NAME As String
    Public Property PARENT_ID As Decimal?
    Public Property PARENT_NAME As String
End Class
