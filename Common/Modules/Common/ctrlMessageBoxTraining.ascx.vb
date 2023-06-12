Public Class ctrlMessageBoxTraining
    Inherits CommonView
    Public Property MessageText As String
    Public Property MessageTitle As String
    Public Property Ok_text As String
    Public Property Ok_CSS As String
    Public Property Cancel_text As String
    Public Property Cancel_CSS As String

    Public Property ActionName As String
        Get
            Return CurrentState
        End Get
        Set(ByVal value As String)
            CurrentState = value
        End Set
    End Property
    Delegate Sub ButtonCommandDelegate(ByVal sender As Object, ByVal e As MessageBoxEventArgs)
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetMessageTitle(ByVal text As String)
        MessageTitle = text
    End Sub

    Public Sub Show()
        If MessageTitle <> "" Then
            rwMessage.Title = MessageTitle
        End If
        lblMessage.Text = MessageText
        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True

    End Sub

    Public Sub Hide()
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub


End Class

Public Enum MessageBoxButtonTypeTraining
    ButtonYes = 1
    ButtonNo = 0
End Enum

Public Class MessageBoxEventArgsTraining
    Inherits EventArgs

    Public Sub New(ByVal _ButtonID As MessageBoxButtonType, ByVal _ActionName As String)
        ButtonID = _ButtonID
        ActionName = _ActionName
    End Sub
    Public ButtonID As MessageBoxButtonType
    Public ActionName As String

End Class