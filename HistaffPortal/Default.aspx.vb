Imports Common
Imports Framework.UI

Public Class _Default
    Inherits AjaxPage

    Protected WithEvents view As ViewBase

    Public Property ApprovePermiss As DataSet
        Get
            Return ViewState(Me.ID & "_ApprovePermiss")
        End Get
        Set(ByVal value As DataSet)
            ViewState(Me.ID & "_ApprovePermiss") = value
        End Set
    End Property

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))
        MyBase.OnInit(e)
        Me.AjaxManager = Me.RadAjaxManager1
        'Me.AjaxManager.EnableAJAX = False
        Me.AjaxLoading = LoadingPanel
        If Request.Params("EnableAjax") IsNot Nothing AndAlso Request.Params("EnableAjax") = False Then
            Me.AjaxManager.EnableAJAX = False
        End If
        Me.AjaxLoading = LoadingPanel
        Me.PopupWindow = rwMainPopup
        If mid IsNot Nothing AndAlso mid.Trim.ToUpper = "SUPPORT" Then
            Response.Redirect("http://support.histaff.vn:8088/")
            Exit Sub
        End If
        If mid IsNot Nothing AndAlso mid.Trim <> "" AndAlso fid IsNot Nothing AndAlso fid.Trim <> "" Then
            Try
                view = Me.Register(fid, mid, fid, group, , True)
                Me.Title = view.ViewDescription
            Catch ex As Exception
                'ShowMessage(ex.ToString, Utilities.NotifyType.Warning)
                DisplayException(Me.Title, "", ex)
            End Try
        End If
    End Sub

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))

        Try
            'Kiểm tra phân quyền Portal
            If LogHelper.CurrentUser Is Nothing And Common.Common.GetUserName <> "" Then
                Dim rep As New Common.CommonRepository
                Dim user = rep.GetUserWithPermision(Common.Common.GetUserName)
                LogHelper.CurrentUser = user
            ElseIf Common.Common.GetUserName = "" Then
                LogHelper.CurrentUser = Nothing
            End If
            If LogHelper.CurrentUser IsNot Nothing Then
                'If LogHelper.CurrentUser.IS_CHANGE_PASS >= 0 And LogHelper.CurrentUser.IS_AD = False Then
                '    HttpContext.Current.Response.Redirect("/Account/ChangePassword.aspx")
                '    Exit Sub
                'End If
                If Not LogHelper.CurrentUser.IS_PORTAL Then
                    ShowMessage(Translate("Bạn không có quyền vô PORTAL"), Utilities.NotifyType.Warning)
                    Session.Abandon()
                    FormsAuthentication.SignOut()
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            'Kiểm tra new user đã thay đổi mật khẩu lần đầu
            If LogHelper.CurrentUser IsNot Nothing Then
                If LogHelper.CurrentUser.IS_LOGIN AndAlso Not LogHelper.CurrentUser.IS_AD Then
                    HttpContext.Current.Response.Redirect("Account/ChangePassword.aspx", False)
                    Exit Sub
                End If
            End If
            If mid IsNot Nothing AndAlso mid.Trim <> "" AndAlso fid IsNot Nothing AndAlso fid.Trim <> "" Then
                If mid.Trim.ToUpper <> "PORTAL" And mid.Trim.ToUpper <> "COMMON" And mid.Trim.ToUpper <> "DASHBOARD" Then
                    If LogHelper.CurrentUser.EMPLOYEE_CODE = "" Then
                        Utilities.Redirect("Dashboard", "ctrlDashboardPortalSixCell")
                        Exit Sub
                    End If
                End If
                If view Is Nothing Then
                    view = Me.Register(fid, mid, fid, group)
                End If
                view.SetProperty("EmployeeID", LogHelper.CurrentUser.EMPLOYEE_ID)
                view.SetProperty("EmployeeCode", LogHelper.CurrentUser.EMPLOYEE_CODE)
                PagePlaceHolder.Controls.Clear()
                PagePlaceHolder.Controls.Add(view)
                liTitle.Text = view.ViewDescription
                Me.Title = Me.Translate(view.ViewDescription)
            Else
                Utilities.Redirect("Dashboard", "ctrlDashboardPortalSixCell")
            End If

        Catch ex As Exception
            DisplayException(Me.Title, "", ex)
        End Try
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))
    End Sub
End Class