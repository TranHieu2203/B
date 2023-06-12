Imports System.Configuration

Public Class InsuranceRepositoryStatic

    ''' <summary>
    ''' Gets the instance.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As InsuranceRepository
        Get
            Return New InsuranceRepository()
        End Get
    End Property
End Class
