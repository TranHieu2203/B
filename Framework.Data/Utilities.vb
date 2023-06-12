Imports System.Data.Objects
Imports System.IO
Imports System.Reflection
Imports System.Text

Public Class Utilities
    Implements IDisposable

    Public Shared Function GetNextSequence(ByVal context As ObjectContext, ByVal tbl_name As String) As Decimal
        Try
            Dim seq As Decimal?
            While (True)
                Try
                    seq = context.ExecuteStoreQuery(Of Decimal?)("select SEQ_" & tbl_name & ".nextval from DUAL").FirstOrDefault
                    Dim maxID As Decimal? = context.ExecuteStoreQuery(Of Decimal?)("select Max(ID) from " & tbl_name).FirstOrDefault
                    If maxID IsNot Nothing AndAlso maxID >= seq Then
                        Continue While
                    End If
                    Exit While
                Catch ex As Exception
                    Throw ex
                End Try
            End While

            Return If(seq Is Nothing, 0, seq)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDataTableByList(Of T)(ByVal varlist As IEnumerable(Of T)) As DataTable
        Dim dtReturn As New DataTable("DATA")

        ' column names 
        Dim oProps() As PropertyInfo = Nothing

        If varlist Is Nothing Then
            Return dtReturn
        End If

        For Each rec As T In varlist

            If oProps Is Nothing Then
                oProps = (CType(rec.GetType(), Type)).GetProperties()
                For Each pi As PropertyInfo In oProps
                    Dim colType As Type = pi.PropertyType

                    If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() Is GetType(Nullable(Of ))) Then
                        colType = colType.GetGenericArguments()(0)
                    End If

                    dtReturn.Columns.Add(New DataColumn(pi.Name, colType))
                Next pi
            End If

            Dim dr As DataRow = dtReturn.NewRow()

            For Each pi As PropertyInfo In oProps
                dr(pi.Name) = If(pi.GetValue(rec, Nothing) Is Nothing, DBNull.Value, pi.GetValue(rec, Nothing))
            Next pi

            dtReturn.Rows.Add(dr)
        Next rec
        Return dtReturn
    End Function

    Public Shared Function Obj2Decima(ByVal _obj As Object) As Decimal
        Try
            'If Not IsNumeric(_obj) Then Return 0

            Return CDec(_obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function Obj2Decima(ByVal _obj As Object, defaultValue As Decimal?) As Decimal?
        Try
            'If Not IsNumeric(_obj) Then Return defaultValue
            Return CDec(_obj)
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

    ''' <summary>
    ''' Writes the exception log.
    ''' </summary>
    ''' <param name="objErr">The obj err.</param>
    ''' <param name="sFunc">The s func.</param>
    Public Shared Sub WriteExceptionLog(ByVal objErr As Exception, ByVal sFunc As String, Optional ByVal OtherText As String = "")
        Dim err As New StringBuilder()
        Dim logFile As String = String.Empty
        Dim logWriter As StreamWriter = Nothing
        Dim logfolder As String
        Dim logformat As String
        Try
            err.AppendLine("Function: " + sFunc)
            err.AppendLine("Datetime: " + DateTime.Now)
            err.AppendLine("Error message: " + objErr.Message)
            err.AppendLine("Stack trace: " + objErr.StackTrace)

            If objErr.InnerException IsNot Nothing Then
                err.AppendLine("Inner error message: " + objErr.InnerException.Message)
                err.AppendLine("Inner stack trace: " + objErr.InnerException.StackTrace)
            End If

            err.AppendLine("Other Text: " + OtherText)

            logfolder = "DataAccess\Exception"
            logformat = "yyyyMMdd.log"

            If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder) Then
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder)
            End If
            logFile = AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder & "\" & DateTime.Now.ToString(logformat)

            If File.Exists(logFile) Then
                logWriter = File.AppendText(logFile)
            Else
                logWriter = File.CreateText(logFile)
            End If

            logWriter.WriteLine(err.ToString())
            logWriter.Close()
        Catch e As Exception

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
