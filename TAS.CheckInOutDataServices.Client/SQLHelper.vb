Public Class SQLHelper
    Private Shared Function Connect(strConn As String) As Boolean
        Try
            If g_dbOra Is Nothing Then g_dbOra = New OleDb.OleDbConnection
            g_dbOra.ConnectionString = strConn
            g_dbOra.Open()

            Return True
        Catch ex As Exception
            WriteEventsLog("Connect DB: " & ex.Message)
            Return False
        End Try

    End Function

    Private Shared Function Disconnect() As Boolean
        If Not g_dbOra Is Nothing Then
            g_dbOra.Close()
            g_dbOra.Dispose()
        End If
    End Function

    Public Shared Function ExecuteQuery(ByVal strSql As String, strConn As String) As DataSet
        Dim ds As New DataSet
        Try
            Connect(strConn)
            Dim da As New OleDb.OleDbDataAdapter(strSql, g_dbOra)
            da.Fill(ds)
            Disconnect()
            Return ds
        Catch ex As Exception
            WriteEventsLog("ExecuteNonQuery: " & strSql & ex.Message)
            Return ds
        End Try
    End Function

    Public Shared Function ExecuteNonQuery(ByVal strsql As String, strConn As String, Optional ByVal isStore As Boolean = False) As Integer
        Dim i As Integer = 0
        Try
            Dim cmd As New Data.OleDb.OleDbCommand
            Connect(strConn)
            cmd.Connection = g_dbOra
            If isStore Then cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = strsql
            cmd.CommandTimeout = 3000000
            i = cmd.ExecuteNonQuery()

            Disconnect()
        Catch ex As Exception
            WriteEventsLog("ExecuteNonQuery: " & strsql & ex.Message)
            i = 0
        End Try
        Return i
    End Function
End Class
