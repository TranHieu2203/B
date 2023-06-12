Imports Attendance

Public Class ProcessApprove
    Inherits System.Web.UI.Page

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim process As String = Request.Params("process")
                Dim aproveid As String = Request.Params("approveid")
                Dim status As String = Request.Params("status")
                Dim reggroup As String = Request.Params("reggroup")

                Dim db As New AttendanceRepository

                If db.ApprovePortalRegister(Decimal.Parse(reggroup), aproveid, status, "",
                                            Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost,
                                            process, False) Then
                    lbMess.Text = "Phê duyệt thành công"
                End If
            Catch ex As Exception
                lbMess.Text = "Bản ghi đã phê duyệt trước đó"
            End Try
        End If
    End Sub

#End Region

End Class