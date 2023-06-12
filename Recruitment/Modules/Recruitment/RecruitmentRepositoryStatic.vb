Public Class RecruitmentRepositoryStatic

    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As RecruitmentRepository
        Get
            Return New RecruitmentRepository()
        End Get
    End Property


End Class
