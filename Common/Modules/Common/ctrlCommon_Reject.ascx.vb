Public Class ctrlCommon_Reject
    Inherits CommonView

    Property TitleForm As String
        Get
            Return ViewState(Me.ID & "_TitleForm")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_TitleForm") = value
        End Set
    End Property
    Public Property Comment As String
        Get
            Return txtRemarkReject.Text
        End Get
        Set(value As String)
            txtRemarkReject.Text = value
        End Set
    End Property
    Delegate Sub ButtonCommandDelegate(ByVal sender As Object, ByVal e As CommandSaveEventArgs)
    Event ButtonCommand As ButtonCommandDelegate

    ' Không kiểm tra quyền
    Public Overrides Property MustAuthorize As Boolean = False
    ' Không log
    Public Overrides Property EnableLogAccess As Boolean = False

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Hide()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ChangeTitleForm()
        rwMessage.Title = String.Format(Translate("Lý do {0}"), TitleForm)
        Label1.Text = String.Format(Translate("Vui lòng nhập lý do {0}"), TitleForm)
        RequiredFieldValidator2.ErrorMessage = String.Format(Translate("Bạn phải nhập lý do {0}"), TitleForm)
    End Sub



    Public Sub Show()
        'rwMessage.Title = MessageTitle

        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True
    End Sub

    Public Sub Hide()
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        RaiseEvent ButtonCommand(sender, New CommandSaveEventArgs(txtRemarkReject.Text))
        Hide()
    End Sub


End Class

Public Class CommandSaveEventArgs
    Inherits EventArgs

    Public Sub New(ByVal _strComment As String)

        Comment = _strComment
    End Sub

    Public Comment As String

End Class
