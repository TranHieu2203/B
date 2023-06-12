Imports System.IO
Imports System.Security.AccessControl

Public NotInheritable Class WriteLog
    Private Sub New()
    End Sub
    Shared Fs As FileStream = Nothing
    Public Shared Sub WriteAllText(ByVal FolderLog As String,
                                   ByVal FileLog As String,
                                   ByVal Datas As String, Optional ByVal intBufferSize As Int64 = 409600)
        Dim LogFs As String = ManageLogDir(FolderLog, FileLog)
        Try
            If eventLog1 Is Nothing Then
                InitEventLog()
            End If
            If Fs Is Nothing Then
                Fs = New FileStream(LogFs, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)
                Fs.Position = Fs.Length
            End If

            If Not LogFs.Equals(Fs.Name) Then
                Fs = New FileStream(LogFs, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)
                Fs.Position = Fs.Length
            End If

            Dim Data As Byte() = System.Text.Encoding.UTF8.GetBytes(Datas)
            Fs.Write(Data, 0, Data.Length)
            Fs.Flush()
            eventLog1.WriteEntry(Datas)

        Catch generatedExceptionName As Exception
        End Try
    End Sub

    Public Shared Sub DeleteDirectory(Dir As String)
        Try
            Directory.Delete(Dir)

        Catch generatedExceptionName As Exception
        End Try
    End Sub

    ' Adds an ACL entry on the specified file for the specified account.
    Public Shared Sub AddFileSecurity(fileName As String, account As String, rights As FileSystemRights, controlType As AccessControlType)
        ' Get a FileSecurity object that represents the 
        ' current security settings.
        Dim fSecurity As FileSecurity = File.GetAccessControl(fileName)

        ' Add the FileSystemAccessRule to the security settings. 
        fSecurity.AddAccessRule(New FileSystemAccessRule(account, rights, controlType))

        ' Set the new access settings.
        File.SetAccessControl(fileName, fSecurity)

    End Sub

    ' Removes an ACL entry on the specified file for the specified account.
    Public Shared Sub RemoveFileSecurity(fileName As String, account As String, rights As FileSystemRights, controlType As AccessControlType)

        ' Get a FileSecurity object that represents the 
        ' current security settings.
        Dim fSecurity As FileSecurity = File.GetAccessControl(fileName)

        ' Add the FileSystemAccessRule to the security settings. 
        fSecurity.RemoveAccessRule(New FileSystemAccessRule(account, rights, controlType))

        ' Set the new access settings.
        File.SetAccessControl(fileName, fSecurity)

    End Sub

    Public Shared eventLog1 As EventLog
    Public Shared Sub InitEventLog()
        eventLog1 = New System.Diagnostics.EventLog()
        DirectCast(eventLog1, System.ComponentModel.ISupportInitialize).BeginInit()
        eventLog1.EnableRaisingEvents = True
        eventLog1.Log = "Application"
        eventLog1.Source = "TAS Services"
        DirectCast(eventLog1, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Shared Function ManageLogDir(ByVal FolderLog As String, ByVal FileLog As String) As String
        Dim PathSep As String = Path.DirectorySeparatorChar.ToString()
        Dim PathTmp As String = FolderLog + PathSep + String.Format(FileLog, DateTime.Now)
        CheckDirExist(FolderLog)
        Return PathTmp
    End Function

    Private Shared Sub CheckDirExist(Dir As String)
        If Directory.Exists(Dir) = False Then
            Directory.CreateDirectory(Dir)
        End If
    End Sub
End Class
