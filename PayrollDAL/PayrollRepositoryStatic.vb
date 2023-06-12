Public Class PayrollRepositoryStatic

    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As PayrollRepository
        Get
            Return New PayrollRepository()
        End Get
    End Property
End Class
