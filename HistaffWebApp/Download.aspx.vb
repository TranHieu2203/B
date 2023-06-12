Public Class Download
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strRequest As String = Request.QueryString(0)
        'Dim str = strRequest.Split(",")
        Dim str1 = strRequest.Split(",")(0)
        Dim str2 = strRequest.Split(",")(1)
        Dim str3 = ""
        If strRequest.Split(",").Length = 3 Then
            str3 = strRequest.Split(",")(2)
        End If
        If str1 = "ctrlHU_ApproveEmployee_Edit" Then
            If strRequest <> "" Then
                str2 = str2.Replace("(slash)", "/")
                Dim path As String = str2 & "/" & str3
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("Tập tin không tồn tại.")
                End If
            Else
                Response.Write("Please provide a file to download.")
            End If
        End If
        If str1 = "ctrlHU_ApproveFamily_Edit" Then
            If strRequest <> "" Then
                str2 = str2.Replace("(slash)", "/")
                Dim path As String = str2 & "/" & str3
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("Tập tin không tồn tại.")
                End If
            Else
                Response.Write("Please provide a file to download.")
            End If
        End If
        If str1 = "ctrlHU_ApproveWorkingBefore_Edit" Then
            If strRequest <> "" Then
                str2 = str2.Replace("(slash)", "/")
                Dim path As String = str2 & "/" & str3
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("Tập tin không tồn tại.")
                End If
            Else
                Response.Write("Please provide a file to download.")
            End If
        End If
        If str1 = "ctrlTreelistEmp" Then
            If strRequest <> "" Then
                Dim path As String = Server.MapPath("~/ReportTemplates/Profile/OrganizationFileAttach/" & str3 & "/" & str2)
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("Tập tin không tồn tại.")
                End If
            Else
                Response.Write("Please provide a file to download.")
            End If
        End If

        If str1 = "ctrlHU_MngProfileSavedNewEdit" Then
            If strRequest <> "" Then
                Dim path As String = Server.MapPath("~/ReportTemplates/Profile/SavedProfile/Result/" & str2)
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
                    Response.AddHeader("Content-Length", file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(file.FullName)
                    Response.End()
                Else
                    Response.Write("Tập tin " & strRequest & " không tồn tại.")
                End If
            Else
                Response.Write("Please provide a file to download.")
            End If
        End If

    End Sub
End Class