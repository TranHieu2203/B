Imports System.Configuration
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

            logfolder = ConfigurationManager.AppSettings("EXFILEFOLDER")
            If String.IsNullOrEmpty(logfolder) Then logfolder = "Exception"

            logformat = ConfigurationManager.AppSettings("EXFILEFORMAT")
            If String.IsNullOrEmpty(logformat) Then logformat = "yyyyMMdd.log"

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

    Public Function FormatRegisterAppointmentSubjectPortal(ByVal sign As AT_TIME_MANUAL, ByVal val As AT_PORTAL_REG,
                                                          Optional ByVal lstValue As List(Of OT_OTHER_LIST) = Nothing) As String
        Dim rtnVal As New StringBuilder
        'Dim rtnExt As String = ATConstant.EXTENVALUE_FORMAT
        Dim isValid As Boolean = False

        Select Case sign.CODE
            Case ATConstant.SIGNTYPECODE_NUMBER
                rtnVal.Append(IIf(val.NVALUE = 1, "", Format(val.NVALUE, "0.##").ToString()))
            Case ATConstant.SIGNTYPECODE_STRING
                rtnVal.Append(val.SVALUE)
            Case ATConstant.SIGNTYPECODE_DATETIME
                rtnVal.Append(val.DVALUE.ToString("HH:mm"))
        End Select
        rtnVal.Append(sign.CODE)

        If sign.CODE = ATConstant.GSIGNCODE_OVERTIME Then
            Return String.Format("{0}[{1}-{2}]", rtnVal.ToString,
                                 If(val.FROM_HOUR.HasValue, val.FROM_HOUR.Value.ToString("HH:mm"), ""),
                                 If(val.TO_HOUR.HasValue, val.TO_HOUR.Value.ToString("HH:mm"), ""))
        End If

        If sign.CODE = ATConstant.GSIGNCODE_LEAVE And val.NVALUE = 1 Then
            Return rtnVal.ToString
        End If

        If val.NVALUE_ID IsNot Nothing Then
            If lstValue IsNot Nothing Then
                Dim otValue = (From p In lstValue Where p.ID = val.NVALUE_ID).FirstOrDefault
                If otValue IsNot Nothing AndAlso otValue.CODE <> 1 Then
                    rtnVal.Append("(" & otValue.NAME_VN & ")")
                End If
            End If
        End If
        Return rtnVal.ToString()
    End Function


    Public Sub GetContentTemplate(ByVal templatefile As String,
                              ByRef subject As String, ByRef content As String)
        Try
            Dim sqlPath = ConfigurationManager.AppSettings("PATH_MAILFILE")
            Dim sqlFileName = AppDomain.CurrentDomain.BaseDirectory + "\" + sqlPath & "\" & templatefile

            content = System.IO.File.ReadAllText(sqlFileName, Encoding.UTF8)

            Dim contentsplit As String() = content.Split(Environment.NewLine)
            If contentsplit.Length > 1 Then
                'get subject
                subject = contentsplit(0)
            End If
            'remove subject from content
            content = content.Replace(subject & Environment.NewLine, String.Empty)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function NVL(ByVal _value As String, ByVal _default As Decimal?) As Decimal?
        Try
            If Not IsNumeric(_value) Then
                Return _default
            End If
            Return _value
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
