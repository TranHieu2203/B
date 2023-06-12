Option Strict On
Option Explicit On

Imports System.Configuration
Imports Oracle.DataAccess.Client

Namespace DataAccess
    Public Class ConnectionManager
        Implements IDisposable

        ''' <summary>
        ''' Connection dùng chung cho cả App
        ''' </summary>
        Public conn As OracleConnection

        ''' <summary>
        ''' Thông tin về Connection
        ''' </summary>
        Structure ConnectionInfo
            Public ConnUser As String
            Public ConnPassword As String
            Public ConnService As String
            Public ConnServer As String
            Public ConnPort As String
        End Structure

        ''' <summary>
        ''' Tạo chuỗi ConnectionString với Key Connection String Default là DataAccess
        ''' </summary>
        ''' <returns></returns>
        Public Function GetConnectionString() As String
            Try
                Return ConfigurationManager.ConnectionStrings("DataAccess").ConnectionString
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & ".ConnectionManager => GetConnectionString()")
                Throw
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="keyConnectionString">Key trong file config để get connection string</param>
        ''' <returns>Chuỗi kết nối database</returns>
        ''' <remarks></remarks>
        Public Function GetConnectionString(ByVal keyConnectionString As String) As String
            Try
                Return ConfigurationManager.ConnectionStrings(keyConnectionString).ConnectionString
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & String.Format(".ConnectionManager => GetConnectionString({0})", keyConnectionString))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Tạo một OracleConnection
        ''' </summary>
        ''' <returns></returns>
        Public Function GetOracleConnection() As OracleConnection
            Try
                Dim connString As String = GetConnectionString()
                Return New OracleConnection() With {
                 .ConnectionString = connString
                }
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & String.Format(".ConnectionManager => GetOracleConnection()"))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Kiểm tra Connection
        ''' </summary>
        ''' <returns></returns>
        Public Function CheckConnection() As Boolean
            Dim conn As OracleConnection = GetOracleConnection()
            Try
                'Open connection
                conn.Open()
                'Close connection
                conn.Close()
                'Dispose connection
                conn.Dispose()

                Return True
            Catch
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Open connection
        ''' </summary>
        ''' <returns></returns>
        Public Function OpenConnection() As Boolean
            Try
                conn.Open()
                Return True
            Catch
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Close connection
        ''' </summary>
        ''' <returns></returns>
        Public Function CloseConnection() As Boolean
            Try
                If conn IsNot Nothing Then
                    conn.Close()
                    'conn.Dispose();
                    Return True
                End If
            Catch
                Return False
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Kiểm tra trạng thái connection và khởi tạo hoặc open nếu cần
        ''' </summary>
        Public Sub CheckLiveConnection()
            Try
                If conn Is Nothing Then
                    conn = GetOracleConnection()
                    OpenConnection()
                Else
                    If conn.State = ConnectionState.Closed OrElse conn.State = ConnectionState.Broken Then
                        OpenConnection()
                    End If
                End If
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & String.Format(".ConnectionManager => CheckLiveConnection()"))
                Throw
            End Try
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace