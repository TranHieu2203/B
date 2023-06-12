Imports System.Data.Common
Imports System.Data.Entity

Public Class Database
    Public Shared Function GetDbCtxConnection(ByVal ConnString As String) As DbConnection
        Dim db = New DbContext(ConnString)
        Return db.Database.Connection
    End Function

End Class