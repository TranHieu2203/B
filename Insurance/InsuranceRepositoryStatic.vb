Public Class InsuranceRepositoryStatic

    Public Shared ReadOnly Property Instance() As InsuranceRepository
        Get
            Return New InsuranceRepository()
        End Get
    End Property
End Class
