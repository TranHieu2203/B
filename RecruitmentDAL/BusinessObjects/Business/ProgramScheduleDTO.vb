Public Class ProgramScheduleDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property RC_PROGRAM_ID As Decimal?
    Public Property RC_PROGRAM_EXAMS_ID As Decimal?
    Public Property EXAMS_NAME As String
    Public Property EXAMS_POINT_LADDER As Decimal?
    Public Property EXAMS_POINT_PASS As Decimal?
    Public Property EXAMS_ORDER As Decimal?
    Public Property EXAMS_PLACE As String
    Public Property NOTE As String
    Public Property SCHEDULE_DATE As Date?
    Public Property lstScheduleCan As List(Of ProgramScheduleCanDTO)
    Public Property lstScheduleUsher As List(Of ProgramScheduleUsherDTO)
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property TIME As String
    Public Property ID_EXAM As String
    Public Property NAME_EXAM As String
End Class
