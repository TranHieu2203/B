Imports System.Configuration

Public Class PayrollRepositoryStatic

    Public Shared ReadOnly Property Instance() As PayrollRepository
        Get
            Return New PayrollRepository()
        End Get
    End Property
End Class
