Imports System.IO
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "File Upload Management"


    Private Function SaveFile_Manager(ByVal fileId As Decimal, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)
            Dim fStream As FileStream

            fStream = fInfo.Open(FileMode.Create)

            fStream.Write(fileBytes, 0, fileBytes.Length)

            fStream.Close()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Private Function DeleteFile_Manager(ByVal fileId As Decimal, ByVal ext As String) As Boolean
        Try
            Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()
            Dim filePath = pathFile
            Dim fPath = Path.Combine(filePath, fileId.ToString & ext)
            Dim fInfo As New FileInfo(fPath)

            If fInfo.Exists Then
                fInfo.Delete()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByVal ext As String, ByRef fileInfo As HuFileDTO) As Byte()
        Try
            Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()
            Dim filePath = System.IO.Path.Combine(pathFile)

            Dim fPath = Path.Combine(filePath, fileID.ToString & ext)

            If File.Exists(fPath) Then

                Dim fInfo = New FileInfo(fPath)

                Dim fileBytes As Byte() = New Byte(fInfo.Length) {}

                Using fStream As System.IO.Stream = fInfo.OpenRead()
                    fStream.Read(fileBytes, 0, fInfo.Length)
                End Using

                fileInfo = GetAttachFile_Manager(fileID)

                Return fileBytes
            Else
                Return New Byte() {}
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function InsertAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()
            Dim attFile As HU_FILE
            Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.HU_FILE.EntitySet.Name)
            Dim ext As String = fileInfo.FILENAME_SYS
            If ext <> "" Then
                If SaveFile(fileID, ext, fileBytes) Then

                    attFile = New HU_FILE With {
                        .FILENAME = fileInfo.FILENAME,
                        .REMARK = fileInfo.REMARK,
                        .ID = fileID,
                        .FROM_DATE = fileInfo.FROM_DATE,
                        .TO_DATE = fileInfo.TO_DATE,
                        .NUMBER_CODE = fileInfo.NUMBER_CODE,
                        .ADDRESS = fileInfo.ADDRESS,
                        .SIGN_PERSON = fileInfo.SIGN_PERSON,
                        .EMPLOYEE_ID = fileInfo.EMPLOYEE_ID,
                        .FILENAME_SYS = fileInfo.FILENAME_SYS,
                        .CQBH = fileInfo.CQBH,
                        .NAME = fileInfo.NAME
                    }

                    Context.HU_FILE.AddObject(attFile)
                Else
                    Return False
                End If

            Else
                Return False
            End If

            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function UpdateAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()
            Dim objUpdate As New HU_FILE With {.ID = fileInfo.ID}
            Context.HU_FILE.Attach(objUpdate)
            With objUpdate

                If fileInfo.FILENAME <> "" Then
                    If DeleteFile_Manager(fileInfo.ID, fileInfo.FILENAME_SYS) And SaveFile(fileInfo.ID, fileInfo.FILENAME_SYS, fileBytes) Then
                        .FILENAME = fileInfo.FILENAME
                    Else
                        Return False
                    End If
                End If

                .ID = fileInfo.ID
                .FILENAME = fileInfo.FILENAME
                .FILENAME_SYS = fileInfo.FILENAME_SYS
                .REMARK = fileInfo.REMARK
                .FROM_DATE = fileInfo.FROM_DATE
                .TO_DATE = fileInfo.TO_DATE
                .NUMBER_CODE = fileInfo.NUMBER_CODE
                .ADDRESS = fileInfo.ADDRESS
                .EMPLOYEE_ID = fileInfo.EMPLOYEE_ID
                .ACTFLG = fileInfo.ACTFLG
                .SIGN_PERSON = fileInfo.SIGN_PERSON
                .CQBH = fileInfo.CQBH
                .NAME = fileInfo.NAME

            End With
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function DeleteAttatch_Manager(ByVal fileID As List(Of Decimal)) As Boolean
        Try
            Dim result As Boolean = False
            For Each id In fileID
                Dim fileDelete = Context.HU_FILE.SingleOrDefault(Function(p) p.ID = id)

                If fileDelete IsNot Nothing Then

                    'Clean file trên server
                    If DeleteFile(id) Then
                        Context.HU_FILE.DeleteObject(fileDelete)
                        Context.SaveChanges()
                        result = True
                    End If
                End If
            Next
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of HuFileDTO)
        Try
            Dim listFile As New List(Of HuFileDTO)
            totalPage = 0
            Dim fList = From a In Context.HU_FILE
                        Where a.EMPLOYEE_ID = Employee_id
                        Order By a.CREATED_DATE Descending
                        Select New HuFileDTO With {
                            .ID = a.ID,
                            .FILENAME = a.FILENAME,
                            .FILENAME_SYS = a.FILENAME_SYS,
                            .REMARK = a.REMARK,
                            .FROM_DATE = a.FROM_DATE,
                            .TO_DATE = a.TO_DATE,
                            .NUMBER_CODE = a.NUMBER_CODE,
                            .ADDRESS = a.ADDRESS,
                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                            .ACTFLG = a.ACTFLG,
                            .SIGN_PERSON = a.SIGN_PERSON,
                            .CQBH = a.CQBH
                        }


            totalPage = fList.Count

            listFile = fList.Skip(page * pageSize).Take(pageSize).ToList

            Return listFile

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function

    Public Function GetAttachFile_Manager(ByVal fileId As Decimal) As HuFileDTO
        Try
            Dim a = Context.HU_FILE.SingleOrDefault(Function(p) p.ID = fileId)

            If a IsNot Nothing Then
                Dim fReturn As New HuFileDTO With {
                            .ID = a.ID,
                            .FILENAME = a.FILENAME,
                            .FILENAME_SYS = a.FILENAME_SYS,
                            .REMARK = a.REMARK,
                            .FROM_DATE = a.FROM_DATE,
                            .TO_DATE = a.TO_DATE,
                            .NUMBER_CODE = a.NUMBER_CODE,
                            .ADDRESS = a.ADDRESS,
                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                            .ACTFLG = a.ACTFLG,
                            .SIGN_PERSON = a.SIGN_PERSON,
                            .CQBH = a.CQBH
                        }
                Return fReturn
            Else
                Return Nothing
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function


    Private Function DeleteFile(ByVal fileId As Decimal) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\AttatchFilesManager"
            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)

            If fInfo.Exists Then
                fInfo.Delete()
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteFile")
            Throw ex
        End Try
    End Function

    Private Function SaveFile(ByVal fileId As Decimal, ByVal ext As String, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()
            Dim filePath = pathFile
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fPath = Path.Combine(filePath, fileId.ToString & ext)
            Dim fInfo As New FileInfo(fPath)
            Dim fStream As FileStream

            fStream = fInfo.Open(FileMode.Create)

            fStream.Write(fileBytes, 0, fileBytes.Length)

            fStream.Close()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SaveFile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeHuFile(ByVal _filter As HuFileDTO) As List(Of HuFileDTO)
        Try
            Dim listFile As New List(Of HuFileDTO)
            'Dim sEmpCode As String = String.Empty
            'sEmpCode = (From p In Context.HU_EMPLOYEE Where p.ID = _filter.EMPLOYEE_ID Select p.EMPLOYEE_CODE).FirstOrDefault
            Dim fList = From a In Context.HU_FILE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = a.EMPLOYEE_ID).DefaultIfEmpty
                        Order By a.CREATED_DATE Descending
                        Select New HuFileDTO With {
                            .ID = a.ID,
                            .FILENAME = a.FILENAME,
                            .FILENAME_SYS = a.FILENAME_SYS,
                            .REMARK = a.REMARK,
                            .FROM_DATE = a.FROM_DATE,
                            .TO_DATE = a.TO_DATE,
                            .NUMBER_CODE = a.NUMBER_CODE,
                            .ADDRESS = a.ADDRESS,
                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                            .ACTFLG = a.ACTFLG,
                            .SIGN_PERSON = a.SIGN_PERSON,
                            .CQBH = a.CQBH,
                            .NAME = a.NAME
                        }

            listFile = fList.ToList

            Return listFile

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw
        End Try
    End Function
    Public Function DeleteEmployeeHuFile(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_FILE)
        Try
            lst = (From p In Context.HU_FILE Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_FILE.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertEmployeeHuFile(ByVal objEmployeeHuF As EmployeeFileDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objEmployeeHuFile As New HU_FILE
            objEmployeeHuFile.ID = Utilities.GetNextSequence(Context, Context.HU_FILE.EntitySet.Name)
            objEmployeeHuFile.EMPLOYEE_ID = objEmployeeHuF.EMPLOYEE_ID
            objEmployeeHuFile.CREATED_DATE = DateTime.Now
            objEmployeeHuFile.CREATED_BY = log.Username
            objEmployeeHuFile.CREATED_LOG = log.ComputerName
            objEmployeeHuFile.MODIFIED_DATE = DateTime.Now
            objEmployeeHuFile.MODIFIED_BY = log.Username
            objEmployeeHuFile.MODIFIED_LOG = log.ComputerName
            Context.HU_FILE.AddObject(objEmployeeHuFile)
            Context.SaveChanges(log)
            gID = objEmployeeHuFile.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

End Class
