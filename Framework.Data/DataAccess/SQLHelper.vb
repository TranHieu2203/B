Imports System.Configuration
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports Framework.Data.Utilities

Public Class SQLHelper
    Private Shared g_dbCon As New OleDbConnection
    Private Shared conn As SqlConnection
    Private Shared g_strSql As String = "Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};User ID={2};Password={3};"
    'Private Shared g_strSql As String = "Data Source={0};Network Library=DBMSSOCN;Initial Catalog={1};User ID={2};Password={3};"

    Private Shared s_user As String = ConfigurationManager.AppSettings("User")
    Private Shared s_pass As String = ConfigurationManager.AppSettings("Pass")
    Private Shared s_db As String = ConfigurationManager.AppSettings("DB")
    Private Shared s_dm As String = ConfigurationManager.AppSettings("DM")

    'Dim str As String = If(ConfigurationManager.AppSettings("AuditLogs") IsNot Nothing, ConfigurationManager.AppSettings("AuditLogs"), "")

    Public Shared Function LoadConfig() As String
        Dim g_strUserName As String = s_user
        Dim g_strPassword As String = s_pass
        Dim g_strDatabase As String = s_db
        Dim g_strSever As String = s_dm
        Dim g_ConStringSql As String
        Try
            g_ConStringSql = String.Format(g_strSql, g_strSever, g_strDatabase, g_strUserName, g_strPassword)

            Return g_ConStringSql
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function LoadConfig_CheckOut(ByVal strUser As String, ByVal strPass As String, ByVal strDB As String, ByVal strDm As String) As String
        Dim g_strUserName As String = strUser
        Dim g_strPassword As String = strPass
        Dim g_strDatabase As String = strDB
        Dim g_strSever As String = strDm
        Dim g_ConStringSql As String
        Try
            g_ConStringSql = String.Format(g_strSql, g_strSever, g_strDatabase, g_strUserName, g_strPassword)

            Return g_ConStringSql
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Shared Function Connect(ByVal strConn As String) As Boolean
        Try
            If g_dbCon Is Nothing Then g_dbCon = New OleDb.OleDbConnection
            If g_dbCon.State = ConnectionState.Closed Then
                g_dbCon.ConnectionString = strConn
                g_dbCon.Open()
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, "SQLHelper", "Connect" & strConn)
            Return False
        End Try

    End Function

    Private Shared Function Disconnect() As Boolean
        If Not conn Is Nothing Then
            conn.Close()
            conn.Dispose()
        End If
        If Not g_dbCon Is Nothing Then
            g_dbCon.Close()
            g_dbCon.Dispose()
        End If
    End Function

    Public Shared Function ExecuteQuery(ByVal strSql As String, ByVal strConn As String) As DataSet
        Dim ds As New DataSet
        Try
            If Connect(strConn) Then
                Dim da As New OleDb.OleDbDataAdapter(strSql, g_dbCon)
                da.Fill(ds)
                Disconnect()
                Return ds
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, "SQLHelper", "ExecuteQuery" & strConn & "_" & strSql)
            Disconnect()
            Return ds
        End Try
    End Function

    Public Shared Function ExecuteNonQuery(ByVal strsql As String, ByVal strConn As String, Optional ByVal isStore As Boolean = False) As Integer
        Dim i As Integer = 0
        Try
            Dim cmd As New OleDbCommand
            Connect(strConn)
            cmd.Connection = g_dbCon
            If isStore Then cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = strsql
            cmd.CommandTimeout = 3000000
            i = cmd.ExecuteNonQuery()

            Disconnect()
        Catch ex As Exception
            WriteExceptionLog(ex, "SQLHelper", "ExecuteQuery")
            i = 0
        End Try
        Return i
    End Function

    ''' <summary>
    ''' File SQL để cùng folder của Application
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLText(ByVal strName As String) As String
        Dim objReader As New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Attendance\Data\" & strName & ".txt")
        Dim temp As String
        Dim strSQL As String = ""
        Dim i As Integer = 0
        Try
            Do While objReader.Peek() <> -1
                'doc tung dong
                i = i + 1
                temp = objReader.ReadToEnd()
                Try
                    strSQL = temp
                Catch ex As Exception

                End Try
            Loop
            objReader.Close()
        Catch ex As Exception
            WriteExceptionLog(ex, "SQLHelper", "ExecuteQuery")
        End Try

        Return strSQL
    End Function
End Class
