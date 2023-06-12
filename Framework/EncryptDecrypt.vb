Imports System.IO
Imports System.Security.Cryptography


Public Class EncryptDecrypt
    Implements IDisposable

#Region "1. Global Variables "

    '*************************
    '** Global Variables
    '*************************

    Public strFileToEncrypt As String
    Public strFileToDecrypt As String
    Private fsInput As Global.System.IO.FileStream
    Private fsOutput As Global.System.IO.FileStream
    ReadOnly sKey As String = "TVC_PSC"
    Public ReadOnly sFileName As String = "lic.key"
#End Region


#Region "2. Create A Key "

    '*************************
    '** Create A Key
    '*************************

    Public Function CreateKey() As Byte()
        Dim strPassword As String = sKey
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte

        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next

        'Declare what hash to use.
        Dim SHA512 As New Global.System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytKey(31).  It will hold 256 bits.
        Dim bytKey(31) As Byte

        'Use For Next to put a specific size (256 bits) of 
        'bytResult into bytKey. The 0 To 31 will put the first 256 bits
        'of 512 bits into bytKey.
        For i As Integer = 0 To 31
            bytKey(i) = bytResult(i)
        Next

        Return bytKey 'Return the key.
    End Function

#End Region


#Region "3. Create An IV "

    '*************************
    '** Create An IV
    '*************************

    Public Function CreateIV() As Byte()
        Dim strPassword As String = sKey
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte

        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next

        'Declare what hash to use.
        Dim SHA512 As New Global.System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytIV(15).  It will hold 128 bits.
        Dim bytIV(15) As Byte

        'Use For Next to put a specific size (128 bits) of 
        'bytResult into bytIV. The 0 To 30 for bytKey used the first 256 bits.
        'of the hashed password. The 32 To 47 will put the next 128 bits into bytIV.
        For i As Integer = 32 To 47
            bytIV(i - 32) = bytResult(i)
        Next

        Return bytIV 'return the IV
    End Function

#End Region


#Region "4. Encrypt / Decrypt File "

    '****************************
    '** Encrypt/Decrypt File
    '****************************

    Public Enum CryptoAction
        'Define the enumeration for CryptoAction.
        ActionEncrypt = 1
        ActionDecrypt = 2
    End Enum

    Public Function EncryptOrDecryptFile(ByVal strInputFile As String, _
                                     ByVal strOutputFile As String, _
                                     ByVal Direction As CryptoAction) As String
        Try 'In case of errors.

            'Send the password to the CreateKey function.
            Dim bytKey = CreateKey()
            'Send the password to the CreateIV function.
            Dim bytIV = CreateIV()

            'Setup file streams to handle input and output.
            fsInput = New Global.System.IO.FileStream(strInputFile, FileMode.Open, _
                                               FileAccess.Read)
            fsOutput = New Global.System.IO.FileStream(strOutputFile, FileMode.OpenOrCreate, _
                                                FileAccess.Write)
            fsOutput.SetLength(0) 'make sure fsOutput is empty

            'Declare variables for encrypt/decrypt process.
            Dim bytBuffer(4096) As Byte 'holds a block of bytes for processing
            Dim lngBytesProcessed As Long = 0 'running count of bytes processed
            Dim lngFileLength As Long = fsInput.Length 'the input file's length
            Dim intBytesInCurrentBlock As Integer 'current bytes being processed
            Dim csCryptoStream As CryptoStream
            'Declare your CryptoServiceProvider.
            Dim cspRijndael As New Global.System.Security.Cryptography.RijndaelManaged


            'Determine if ecryption or decryption and setup CryptoStream.
            Select Case Direction
                Case CryptoAction.ActionEncrypt
                    csCryptoStream = New CryptoStream(fsOutput, _
                    cspRijndael.CreateEncryptor(bytKey, bytIV), _
                    CryptoStreamMode.Write)

                Case CryptoAction.ActionDecrypt
                    csCryptoStream = New CryptoStream(fsOutput, _
                    cspRijndael.CreateDecryptor(bytKey, bytIV), _
                    CryptoStreamMode.Write)
            End Select

            'Use While to loop until all of the file is processed.
            While lngBytesProcessed < lngFileLength
                'Read file with the input filestream.
                intBytesInCurrentBlock = fsInput.Read(bytBuffer, 0, 4096)
                'Write output file with the cryptostream.
                csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock)
                'Update lngBytesProcessed
                lngBytesProcessed = lngBytesProcessed + CLng(intBytesInCurrentBlock)

            End While

            'Close FileStreams and CryptoStream.
            csCryptoStream.Close()
            fsInput.Close()
            fsOutput.Close()

            'If encrypting then delete the original unencrypted file.
            If Direction = CryptoAction.ActionEncrypt Then
                Dim fileOriginal As New FileInfo(strInputFile)
                fileOriginal.Delete()
            End If

            'If decrypting then delete the encrypted file.
            If Direction = CryptoAction.ActionDecrypt Then
                Dim fileEncrypted As New FileInfo(strInputFile)
                fileEncrypted.Delete()
            End If

            'Update the user when the file is done.
            'Dim Wrap As String = Chr(13) + Chr(10)
            'If Direction = CryptoAction.ActionEncrypt Then
            '    MsgBox("Encryption Complete" + Wrap + Wrap + _
            '            "Total bytes processed = " + _
            '            lngBytesProcessed.ToString, _
            '            MsgBoxStyle.Information, "Done")

            'Else
            '    'Update the user when the file is done.
            '    MsgBox("Decryption Complete" + Wrap + Wrap + _
            '           "Total bytes processed = " + _
            '            lngBytesProcessed.ToString, _
            '            MsgBoxStyle.Information, "Done")
            'End If

            Return ""
            'Catch file not found error.
        Catch When Err.Number = 53 'if file not found
            'MsgBox("Please check to make sure the path and filename" + _
            '        "are correct and if the file exists.", _
            '         MsgBoxStyle.Exclamation, "Invalid Path or Filename")
            Return "File không đúng định dạng. Thao tác thực hiện không thành công"
            'Catch all other errors. And delete partial files.
        Catch
            fsInput.Close()
            fsOutput.Close()
            Dim fileDelete As New FileInfo(strInputFile) 'txtDestinationDecrypt.Text
            fileDelete.Delete()
            fileDelete = New FileInfo(strOutputFile) 'txtDestinationDecrypt.Text
            fileDelete.Delete()

            Return "File không đúng định dạng. Thao tác thực hiện không thành công"
        End Try
    End Function

#End Region

    Public Function ReadLicense(ByVal sFileName As String, ByRef sLicenseKey As String) As String
        Dim sLicence As String
        Dim kt As Boolean = False
        Try
            If sLicenseKey = "" OrElse sLicenseKey = String.Empty Then
                kt = True
            End If
            Using encry As New EncryptDecrypt
                If kt Then
                    'Start the decryption.
                    encry.EncryptOrDecryptFile(sFileName, sFileName.Substring(0, sFileName.Length - 4), _
                                         EncryptDecrypt.CryptoAction.ActionDecrypt)

                    sLicenseKey = System.IO.File.ReadAllText(sFileName.Substring(0, sFileName.Length - 4))
                End If
                Using encrydata As New EncryptData
                    sLicence = encrydata.DecryptString(sLicenseKey)
                End Using

                If kt Then
                    'Start the encryption.
                    encry.EncryptOrDecryptFile(sFileName.Substring(0, sFileName.Length - 4), sFileName, _
                                         EncryptDecrypt.CryptoAction.ActionEncrypt)
                End If
            End Using


            Integer.TryParse(sLicence, 0)
            Return sLicence
        Catch ex As Exception
            Return 0
        End Try
    End Function

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