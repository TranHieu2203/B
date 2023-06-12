Imports System.Web.Script.Services
Imports System.Web.Services

<WebService>
Public Class ApiServices
    Inherits Common.CommonView
    Private EmpId As String
    Private Id As String
    Private AppStatus As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EmpId = Request.QueryString("EmpId")
        Id = Request.QueryString("Id")
        AppStatus = Request.QueryString("AppStatus")

        hdEmpId.Value = EmpId
        hdId.Value = Id
        hdAppStatus.Value = AppStatus
    End Sub

    ''' <summary>
    ''' Hàm này thực hiện việc Phê duyệt (Không Phê Duyệt)
    ''' </summary>
    Private Sub ApproveOrReject()
        Dim rep As New AttendanceRepository
        Dim entity = rep.GetLeaveSheetByEmpAndLeave(EmpId, Id)


    End Sub
End Class