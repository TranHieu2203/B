'Create by Truongnn
'Create on 12/12/2007
'Purpose: Some function utility for Read HASTC from XML to BOSC
Imports System.IO

Module mdlGlobal

    Public g_strStatus As String
    Public g_strUserName As String
    Public g_strPassword As String
    Public g_strDatabase As String
    Public g_intTime1From As UInt16
    Public g_intTime1To As UInt16
    Public g_intTime2From As UInt16
    Public g_intTime2To As UInt16
    Public g_intInterval As Int32 = 10
    Public g_ConnString As String
    Public g_backdate As UInt16 = 0
    '--------------------------------------

    Public g_ExceptionFile As String = Application.StartupPath() & "\Exception"
    Public g_strFolderLog As String = Application.StartupPath() & "\Log"
    Public g_strLogFile As String = "Log_{0:ddMMyyyy}.txt"
    Public g_strFolderLogMCC As String = Application.StartupPath() & "\LogMCC"
    Public g_strLogFile_MCC As String = "LogMCC_{0:ddMMyyyy}.txt"
    Public g_strAppConfig As String = Application.StartupPath() & "\config.xml"
    Public g_strAppConfig_List As String = Application.StartupPath() & "\config_LIST.xml"
    Public g_strAppConfig_MCC As String = Application.StartupPath() & "\config_MCC.xml"
    Public g_strAppConfig_MCC_List As String = Application.StartupPath() & "\config_MCC_LIST.xml"
    Public g_strTitleMsgBox As String = "TAS.Services"
    Public g_dbOra As New System.Data.OleDb.OleDbConnection

    Public Sub WriteEventsLog(ByVal Message As String, Optional ByVal intBufferSize As Int64 = 409600)
        'Chuc nang: Ghi nhan vao logfile
        '-------------------------------------------------
        Dim strPathFile As String = g_ExceptionFile
        Dim lastError As String = ""
        Try
            If Not IO.File.Exists(strPathFile) Then
                IO.File.Create(strPathFile, intBufferSize)
            End If
            '-----------
            Dim FilePer As New Security.Permissions.FileIOPermission(Security.Permissions.FileIOPermissionAccess.AllAccess, IO.Path.GetFullPath(strPathFile))
            FilePer.Demand()
            If IO.File.Exists(strPathFile) Then
                Dim f As IO.StreamReader = IO.File.OpenText(strPathFile)
                lastError = f.ReadToEnd
                f.Close()
            End If
            Dim LogF As IO.StreamWriter = IO.File.CreateText(strPathFile)
            Try
                LogF.WriteLine("[Date {0}]", Format(Now, "dd/MM/yyyy hh:mm:ss"))
                LogF.WriteLine(Message)
                LogF.WriteLine("-----------------------------------------------")
                If lastError <> String.Empty Then LogF.Write(Left(lastError, intBufferSize))
            Finally
                LogF.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Đọc File XML vào DataSet
    ''' </summary>
    ''' <param name="strXMLFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataSet(ByVal strXMLFile As String) As DataSet
        'Chuc nang: doc file Template (file xml)-->dataset
        '-------------------------------------------------------
        Try
            If IO.File.Exists(strXMLFile) = False Then
                Return Nothing
            End If
            '---------------------
            Dim ds As New DataSet
            ds.ReadXml(strXMLFile)
            Return ds
        Catch ex As Exception
            WriteEventsLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetPeriodTime(ByVal startTime As Date, ByVal endTime As Date) As String
        Dim lngSecond As Long
        lngSecond = 3600 * (Hour(endTime) - Hour(startTime)) + 60 * (Minute(endTime) - Minute(startTime)) + (Second(endTime) - Second(startTime))
        Return CStr(lngSecond \ 3600 & " h: " & (lngSecond Mod 3600) \ 60 & " m: " & (lngSecond Mod 3600) Mod 60 & " s")
    End Function

End Module

Module FileManager
    Public Function LoadFile(ByVal fileName As String) As DataSet
        Dim IOPer As New System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None)
        Dim info As System.IO.FileInfo
        Dim reader As System.IO.FileStream
        Dim dset As New DataSet

        Try
            If Not File.Exists(fileName) Then
                Return Nothing
            End If

            IOPer.AddPathList(System.Security.Permissions.FileIOPermissionAccess.Read, fileName)
            IOPer.Demand()
            IOPer.Assert()
            info = New System.IO.FileInfo(fileName)
            reader = info.Open(FileMode.Open, FileAccess.Read, FileShare.Read)
            dset.ReadXml(reader, XmlReadMode.ReadSchema)
            reader.Close()
            info = Nothing
            Return dset
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SaveFile(ByVal dSet As DataSet, ByVal fileName As String)
        Dim IOPer As New System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None)
        Dim f As FileStream
        Try
            DeleteFile(fileName)
            f = New FileStream(fileName, FileMode.Create)
            IOPer.AddPathList(System.Security.Permissions.FileIOPermissionAccess.AllAccess, fileName)
            IOPer.Demand()
            dSet.WriteXml(f, XmlWriteMode.WriteSchema)
            f.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If Not f Is Nothing Then
                f.Close()
                f = Nothing
            End If
        End Try
    End Sub

    Public Sub CreateFile(ByVal filename As String, ByVal modifiedDate As Date)
        Dim f As FileStream
        Try
            f = New FileStream(filename, FileMode.Create)
            f.Close()
            IO.File.SetLastWriteTime(filename, modifiedDate)
            f = Nothing
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub

    Public Sub DeleteFile(ByVal strPath As String)
        Dim IOPer As New System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None)
        Try
            IOPer.AddPathList(System.Security.Permissions.FileIOPermissionAccess.AllAccess, Path.GetFullPath(strPath))
            IOPer.Demand()
            If File.Exists(strPath) Then
                File.Delete(strPath)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SaveXMLFile(ByVal dSet As DataSet, ByVal fileName As String)
        Dim IOPer As New System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None)
        Dim f As FileStream
        Try
            DeleteFile(fileName)
            f = New FileStream(fileName, FileMode.Create)
            IOPer.AddPathList(System.Security.Permissions.FileIOPermissionAccess.AllAccess, fileName)
            IOPer.Demand()
            dSet.WriteXml(f, XmlWriteMode.WriteSchema)
            f.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If Not f Is Nothing Then
                f.Close()
                f = Nothing
            End If
        End Try
    End Sub

    Public Function LoadXMLFile(ByVal xmlFilename As String) As DataSet
        Dim newDataSet As New DataSet
        Dim fsReadXml As New System.IO.FileStream(xmlFilename, System.IO.FileMode.Open)
        ' Create an XmlTextReader to read the file.
        Dim myXmlReader As New System.Xml.XmlTextReader(fsReadXml)
        ' Read the XML document into the DataSet.
        newDataSet.ReadXml(myXmlReader)
        ' Close the XmlTextReader
        myXmlReader.Close()
        Return newDataSet
    End Function

    Public Function GetFileInfo(ByVal filename As String) As IO.FileInfo
        'get file info
        Dim IOPer As New System.Security.Permissions.FileIOPermission(System.Security.Permissions.PermissionState.None)
        Dim info As FileInfo
        Try
            If Not File.Exists(Path.GetFullPath(filename)) Then Throw New Exception("File " & filename & " Not Found")
            info = New FileInfo(Path.GetFullPath(filename))
        Catch ex As Exception
            Throw ex
        End Try
        Return info
    End Function

    Public Function CStrNull(ByVal datareader As System.Data.SqlClient.SqlDataReader, ByVal colname As String, Optional ByVal Defaultvalue As String = "") As String
        Try
            Return IIf(datareader.Item(colname) Is System.DBNull.Value, Defaultvalue, datareader.Item(colname))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CStrNull(ByVal datarow As System.Data.DataRow, ByVal colname As String, Optional ByVal Defaultvalue As String = "") As String
        Try
            Return IIf(datarow.Item(colname) Is System.DBNull.Value, Defaultvalue, datarow.Item(colname))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsNull(ByVal obj As Object) As Boolean
        Try
            If obj Is Nothing OrElse obj Is System.DBNull.Value Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetDateFormatString() As String

        Return "'"

    End Function

    ' Dung khi luu du lieu kieu date , lua chon EndPoint khi muon dien cac thong tin ve gio phut giay
    Public Function CDBDate(ByVal vntDate As Object, Optional ByVal lngEndPoint As Long = 0) As String
        Try
            Dim strTmp As String = GetDateFormatString()

            If IsNull(vntDate) Then
                CDBDate = "Null"
            ElseIf IsDate(vntDate) Then
                If lngEndPoint = 0 Then
                    CDBDate = strTmp & Format(vntDate, "dd - MMM - yyyy hh:mm:ss") & strTmp
                ElseIf lngEndPoint = 1 Then
                    CDBDate = strTmp & Format(vntDate, "dd - MMM - yyyy 00:00:00") & strTmp
                ElseIf lngEndPoint = 2 Then
                    CDBDate = strTmp & Format(vntDate, "dd - MMM - yyyy 23:59:59") & strTmp
                ElseIf lngEndPoint = 3 Then
                    ' if oracle 
                    CDBDate = "To_Date(" & "'" & Format(vntDate, "dd - MMM - yyyy") & "'" & ",'DD - Mon - YYYY')"
                Else

                    ' if oracle 
                    CDBDate = "To_Date(" & "'" & Format(vntDate, "dd - MMM - yyyy hh/mm/ss") & "'" & ",'DD - Mon - YYYY HH24/MI/SS')"

                End If
            Else
                CDBDate = "Null"
            End If
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function CCDate(ByVal vntDate As Object, ByVal vntMinDate As DateTime) As DateTime
        If IsNull(vntDate) Then Return Nothing
        If IsNull(vntMinDate) Then Return Nothing
        If Not IsDate(vntDate) Then Return Nothing
        If CDate(vntDate) >= vntMinDate Then Return CDate(vntDate)
        Return Nothing
    End Function

    Public Function CDBStr(ByVal strVal As String, Optional ByVal blnBuildSQLinStored As Boolean = False) As String
        Try
            Dim vntPosition As Object

            If IsNull(strVal) Then
                CDBStr = "NULL"
                Exit Function
            ElseIf strVal = "" Then
                CDBStr = "''"
                Exit Function
            ElseIf Not blnBuildSQLinStored Then
                vntPosition = InStr(strVal, "'")
                Do While vntPosition <> 0
                    strVal = Mid(strVal, 1, vntPosition - 1) & "''" & Mid(strVal, vntPosition + 1)
                    vntPosition = InStr(vntPosition + 2, strVal, "'")
                Loop
            Else
                vntPosition = InStr(strVal, "'")
                Do While vntPosition <> 0
                    strVal = Mid(strVal, 1, vntPosition - 1) & "''''" & Mid(strVal, vntPosition + 1)
                    vntPosition = InStr(vntPosition + 4, strVal, "'")
                Loop
            End If
            CDBStr = "'" & CStr(strVal) & "'"
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' Dung luu chuoi, trong truong hop chuoi duoc truyen vao mot stored roi moi xay dung stored trong cau query
    ' thi chuoi se duong ngan bang 2 dau nhay don --> set blnBuildSQLinStored =true
    Public Function CDBStrU(ByVal strVal As String, Optional ByVal blnBuildSQLinStored As Boolean = False) As String
        Try
            If IsNull(strVal) Then
                CDBStrU = "Null"
            Else
                CDBStrU = "N" & CDBStr(strVal, blnBuildSQLinStored)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CCurNull(ByVal vData As Object) As Double
        Try
            If IsNumeric(vData) Then
                Return CDbl(vData)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Module