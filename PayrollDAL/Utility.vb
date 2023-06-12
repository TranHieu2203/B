Imports System.IO
Imports System.Text

Public Module Utility
    ''' <summary>
    ''' Writes the exception log.
    ''' </summary>
    ''' <param name="objErr">The obj err.</param>
    ''' <param name="sFunc">The s func.</param>
    Public Sub WriteExceptionLog(ByVal objErr As Exception, ByVal sFunc As String, Optional ByVal OtherText As String = "")
        Dim err As New StringBuilder()
        Dim logFile As String = String.Empty
        Dim logWriter As System.IO.StreamWriter = Nothing
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

            logfolder = "Payroll\Exception"
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

        End Try
    End Sub

    Public Function ConverToACTFLG(ByVal val As Boolean) As String
        Return IIf(val, PAConstant.ACTFLG_ACTIVE, PAConstant.ACTFLG_DEACTIVE)
    End Function

    ''' <summary>
    ''' Convers from ACTFLG.
    ''' </summary>
    ''' <param name="val">The val.</param>
    ''' <returns></returns>
    Public Function ConverFromACTFLG(ByVal val As String) As Boolean
        Return IIf(val = PAConstant.ACTFLG_ACTIVE, True, False)
    End Function

    ''' <summary>
    ''' Checks the effect date.
    ''' </summary>
    ''' <param name="effecdate">The effecdate.</param>
    ''' <param name="expiredate">The expiredate.</param>
    ''' <param name="startdate">The startdate.</param>
    ''' <param name="enddate">The enddate.</param>
    ''' <returns></returns>
    Public Function CheckEffectDate(ByVal effecdate As Date?, ByVal expiredate As Date?, ByVal startdate As Date?, ByVal enddate As Date?) As Boolean
        If Not startdate.HasValue AndAlso Not enddate.HasValue Then
            Return True
        End If

        If Not startdate.HasValue AndAlso enddate.HasValue AndAlso effecdate <= enddate Then
            Return True
        End If

        If Not expiredate.HasValue AndAlso effecdate <= enddate.GetValueOrDefault(Date.MaxValue) Then
            Return True
        ElseIf expiredate.HasValue AndAlso startdate <= expiredate AndAlso _
              effecdate <= enddate.GetValueOrDefault(effecdate.Value) Then
            Return True
        End If

        Return False
    End Function

    Public Function DateDiffDay(ByVal startdate As Date, ByVal enddate As Date) As Double
        Try
            Dim sDate As TimeSpan = enddate - startdate
            Return sDate.TotalDays + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
