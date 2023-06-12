Option Strict On
Option Explicit On
Imports System.IO
Imports System.Text

Module Utility

    ''' <summary>
    ''' Write Exception Log
    ''' </summary>
    ''' <param name="exception">Exception (truyên vào biến ex)</param>
    ''' <param name="functionName">Tên Function</param>
    ''' <param name="otherText">Chuỗi text khác nếu có</param>
    ''' <remarks>Framework 1.0</remarks>
    Public Sub WriteExceptionLog(ByVal exception As Exception, ByVal functionName As String, Optional ByVal otherText As String = "")
        Dim err As New StringBuilder()
        Dim logFile As String = String.Empty
        Dim logWriter As System.IO.StreamWriter = Nothing
        Dim logfolder As String
        Dim logformat As String
        Try
            err.AppendLine("Function: " + functionName)
            err.AppendLine("Datetime: " + DateTime.Now.ToString())
            err.AppendLine("Error message: " + exception.Message)
            err.AppendLine("Stack trace: " + exception.StackTrace)

            If exception.InnerException IsNot Nothing Then
                err.AppendLine("Inner error message: " + exception.InnerException.Message)
                err.AppendLine("Inner stack trace: " + exception.InnerException.StackTrace)
            End If

            err.AppendLine("Other Text: " + otherText)
            err.AppendLine("________________________________________________________________________________________________________")

            logfolder = "HistaffFramework\Exception" 'Folder ghi log
            If String.IsNullOrEmpty(logfolder) Then logfolder = "Exception"

            logformat = "yyyyMMdd.log"

            If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder) Then
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder)
            End If

            logFile = AppDomain.CurrentDomain.BaseDirectory & "\" & logfolder & "\" & DateTime.Now.ToString(logformat)

            If System.IO.File.Exists(logFile) Then
                logWriter = System.IO.File.AppendText(logFile)
            Else
                logWriter = System.IO.File.CreateText(logFile)
            End If

            logWriter.WriteLine(err.ToString())
            logWriter.Close()
        Catch e As Exception
            Throw e
        End Try
    End Sub

End Module
