Public Class AttendanceRepositoryStatic

    Public Shared ReadOnly Property Instance() As AttendanceRepository
        Get
            Return New AttendanceRepository()
        End Get
    End Property
End Class
