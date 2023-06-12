Public Class CourseDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME As String
    Public Property TR_CER_GROUP_ID As Decimal?
    Public Property TR_CER_GROUP_NAME As String
    Public Property TR_CERTIFICATE_ID As Decimal?
    Public Property TR_CERTIFICATE_NAME As String
    Public Property TR_TRAIN_FORM_ID As Decimal?
    Public Property TR_TRAIN_FORM_NAME As String
    Public Property TR_TRAIN_ENTRIES_ID As Decimal?
    Public Property TR_TRAIN_ENTRIES_NAME As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property TR_TRAIN_FIELD_ID As Decimal?
    Public Property TR_TRAIN_FIELD_NAME As String
    Public Property TR_PROGRAM_GROUP_ID As Decimal?
    Public Property TR_PROGRAM_GROUP_NAME As String
    Public Property DRIVER As Boolean
    Public Property REMARK As String
    Public Property LST_TITLE_GROUP As List(Of OtherListDTO)
    Public Property LST_TITLE As List(Of TitleDTO)
End Class
