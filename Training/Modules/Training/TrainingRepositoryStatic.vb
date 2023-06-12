Public Class TrainingRepositoryStatic

    Public Shared ReadOnly Property Instance() As TrainingRepository
        Get
            Return New TrainingRepository()
        End Get
    End Property
End Class
