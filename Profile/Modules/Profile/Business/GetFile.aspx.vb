Imports System.IO
Imports Aspose.Words
Imports Common

Public Class GetFile
    Inherits AjaxPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim id As String = Request.QueryString("id") & ""
            Dim binary As Byte()
            Dim fileName As String = ""
            Select Case id
                Case "EMPDTL_FILE"
                    Dim fileId As String = Request.QueryString("fid") & ""
                    If fileId = "" Then
                        Response.Write(Translate("Không tìm thấy file bạn chọn."))
                        Exit Sub
                    End If

                    Dim db As New ProfileBusinessRepository

                    Dim fileInfo As ProfileBusiness.EmployeeFileDTO = New ProfileBusiness.EmployeeFileDTO
                    'binary = db.DownloadAttachFile_Manager(Decimal.Parse(Request.QueryString("fid")), fileInfo)
                    fileName = fileInfo.FILE_NAME
                Case "TEMPLATE"
                    Dim fileId As String = Request.QueryString("fid") & ""
                    Dim folderName As String = Request.QueryString("folderName") & ""
                    GetTemplateFile(fileId, folderName)
                    Exit Sub
            End Select


            Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileName, "_"))
            'Response.AddHeader("Content-Length", binary.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.BinaryWrite(binary)

        Catch ex As Exception
            Response.Write("<img src=""/Static/Images/404.jpg"" /><br />" & Translate("Không tìm thấy file bạn chọn."))
        End Try

    End Sub


    Public Sub GetTemplateFile(ByVal fileId As String, folderName As String)
        Try
            If folderName.ToUpper.Equals("HRPROCESS") Then
                Dim filePath = "\TemplateDynamic\" & folderName & "\" & fileId & ".doc"
                Using rep As New ProfileRepository

                    Dim fByte = rep.GetTemplateFileHost(filePath)

                    If fByte.Length > 0 Then
                        Dim doc As New Document(New MemoryStream(fByte))
                        doc.Save(Response, fileId & ".doc",
                                 Aspose.Words.ContentDisposition.Attachment,
                                 Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
                    End If
                End Using

            Else
                Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\" & folderName
                Dim dirs As String() = Directory.GetFiles(filePath, fileId.ToString & ".*")
                If dirs.Length > 0 Then
                    Dim doc As New Document(dirs(0))
                    doc.Save(Response, fileId & ".doc",
                             Aspose.Words.ContentDisposition.Attachment,
                             Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))

                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class