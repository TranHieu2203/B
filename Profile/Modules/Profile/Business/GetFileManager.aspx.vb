Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Imports System.Xml

Public Class GetFileManager
    Inherits AjaxPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim fileId As String = Request.QueryString("fid") & ""

            If fileId = "" Then
                Response.Write(Translate("Không tìm thấy file bạn chọn."))
                Exit Sub
            End If

            Dim db As New ProfileBusinessRepository

            Dim fileInfo As ProfileBusiness.EmployeeFileDTO = New ProfileBusiness.EmployeeFileDTO
            Dim targetFile As Byte() = db.DownloadAttachFile_Manager(Decimal.Parse(Request.QueryString("fid")), fileInfo)

            Dim fileName As String = fileInfo.FILE_NAME

            Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileName, "_"))
            Response.AddHeader("Content-Length", targetFile.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.BinaryWrite(targetFile)

        Catch ex As Exception
            Response.Write("<img src=""/Static/Images/404.jpg"" /><br />" & Translate("Không tìm thấy file bạn chọn."))
        End Try

    End Sub
End Class