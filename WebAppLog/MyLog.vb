Imports System.IO

Public Class MyLog
    Dim _logformat As String = "yyyyMMdd.log"
    Public _folder As String = AppDomain.CurrentDomain.BaseDirectory & "\" & "Logs"
    Public _pathLog As String = _folder & "\" & DateTime.Now.ToString(_logformat)
    Public _info As String = "Info"
    Public _warning As String = "Warning"
    Public _error As String = "Error"

    Public Sub CreateFile()
        Try
            Dim strFile As String = _pathLog
            If Not Directory.Exists(_folder) Then
                Directory.CreateDirectory(_folder)
            End If
            Dim fs As FileStream = Nothing

            fs = File.Create(_pathLog)
            fs.Close()
            Using w As StreamWriter = File.AppendText(_pathLog)
                w.WriteLine("Date,Time,TimeProcess,Class,Method,leve-message,exception,InnerException,StackTrace")
                w.Close()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Log(ByVal logMessage As String, ByVal w As TextWriter)
        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), _
            DateTime.Now.ToLongDateString())
        w.WriteLine("  : {0}", logMessage)
        w.WriteLine("-------------------------------")
    End Sub

    'Public Sub WriteLog(ByVal level As String, ByVal classPath As String, ByVal method As String, ByVal timeProcess As Integer, ByVal message As String)
    '    Try
    '        If (Not File.Exists(_pathLog)) Then
    '            CreateFile()
    '        End If

    '        Using w As StreamWriter = File.AppendText(_pathLog)
    '            w.WriteLine("Datetime: {0} {1}", DateTime.Now.ToLongTimeString(), _
    '                    DateTime.Now.ToLongDateString())
    '            If (timeProcess <> 0 & level.ToUpper() <> _error.ToString().ToUpper()) Then
    '                w.WriteLine("Time Process: {0}", timeProcess)
    '            End If
    '            w.WriteLine("Path: {0} | {1}", classPath, method)
    '            If (Not String.IsNullOrEmpty(message.ToString())) Then
    '                w.WriteLine("{0} : {1} ", level, message)
    '            End If
    '            w.Close()
    '        End Using
    '    Catch ex As Exception
    '    End Try

    'End Sub
    Public Sub WriteLog(ByVal level As String, ByVal classPath As String, ByVal method As String, ByVal timeProcess As Integer, ByVal exception As Exception, ByVal message As String)
        Try
            If (Not File.Exists(_pathLog)) Then
                CreateFile()
            End If
            Dim time As Integer = timeProcess
            Dim strLog = String.Empty
            Using w As StreamWriter = File.AppendText(_pathLog)
                strLog = DateTime.Now.ToString("dd/MM/yyyy") & "," & DateTime.Now.ToString("HH:mm:ss") & "," & If(time <> 0, timeProcess.ToString(), "0") & "," & classPath & "," & method & "," & If(Not String.IsNullOrEmpty(message), level & "-" & message, "") &
                    "," & If(exception IsNot Nothing, level & "-" & exception.Message.ToString(), "") &
                    "," & If(exception IsNot Nothing AndAlso exception.InnerException IsNot Nothing, exception.InnerException.ToString(), "") &
                    "," & If(exception IsNot Nothing AndAlso exception.InnerException IsNot Nothing, exception.InnerException.StackTrace.ToString(), "")
                w.WriteLine(strLog)
                'w.WriteLine("Datetime: {0} {1}", DateTime.Now.ToLongTimeString(),
                '        DateTime.Now.ToLongDateString())
                'Try
                '    If (time <> 0) Then
                '        w.WriteLine("Time Process: {0}", timeProcess)
                '        'If (Not level.ToUpper().ToString() = _error.ToUpper().ToString()) Then
                '        '    w.WriteLine("Time Process: {0}", timeProcess)
                '        'End If
                '    End If
                'Catch ex As Exception
                '    Exit Sub
                'Finally
                'End Try

                'w.WriteLine("Path: {0} | {1}", classPath, method)
                'If (Not String.IsNullOrEmpty(message)) Then
                '    w.WriteLine("{0} : {1}", level, message)
                'End If
                'If exception IsNot Nothing Then
                '    w.WriteLine("{0} : {1} ", level, exception.Message.ToString())
                '    w.WriteLine("StackTrace: {0}", exception.StackTrace.ToString())
                '    If exception.InnerException IsNot Nothing Then
                '        w.WriteLine("InnerMessage : {0} ", exception.InnerException.Message.ToString())
                '        w.WriteLine("InnerStackTrace: {0}", exception.InnerException.StackTrace.ToString())
                '    End If
                'End If

                w.Close()
            End Using
        Catch ex As Exception
            Exit Sub
        Finally

        End Try

    End Sub

End Class