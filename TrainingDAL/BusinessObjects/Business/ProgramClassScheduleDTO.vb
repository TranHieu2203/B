Public Class ProgramClassScheduleDTO
    Public Property ID As Decimal
    Public Property TR_CLASS_ID As Decimal?
    Public Property STUDENTS As List(Of ProgramClassStudentDTO) 'ViewOnly
    Public Property SUBJECT As String
    Public Property START_TIME As Date?
    Public Property END_TIME As Date?
    Public Property CONTENT As String
    Public Property PROGRAM As String 'ViewOnly
    Public Property TOOLTIP As String 'ViewOnly
    Public Property IS_HC As Integer? 'ViewOnly: 0 = Chưa lưu, 1 = Đã lưu
    Public Property IS_HALF As Boolean? 'ViewOnly
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property TOTAL_TIME As Decimal?
End Class
