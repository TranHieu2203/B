Public Class TrainingRepositoryStatic

    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As TrainingRepository
        Get
            Return New TrainingRepository()
        End Get
    End Property
End Class
