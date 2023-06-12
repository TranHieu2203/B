Imports Telerik.Web.UI

Public Class ctrlUpload
    Inherits CommonView

#Region "Property"

    Public WriteOnly Property MaxFileSize As Integer
        Set(value As Integer)
            RadAsyncUpload1.MaxFileSize = value
        End Set
    End Property

    Delegate Sub OkClickedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Delegate Sub CancelClickedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event OkClicked As OkClickedDelegate
    Event CancelClicked As CancelClickedDelegate
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property UploadedFiles As UploadedFileCollection
        Get
            Return RadAsyncUpload1.UploadedFiles
            Return Nothing
        End Get
    End Property

    Public WriteOnly Property isMultiple As AsyncUpload.MultipleFileSelection
        Set(ByVal value As AsyncUpload.MultipleFileSelection)
            RadAsyncUpload1.MultipleFileSelection = value
        End Set
    End Property

    Public WriteOnly Property MaxFileInput As Integer
        Set(ByVal value As Integer)
            RadAsyncUpload1.MaxFileInputsCount = value
        End Set
    End Property

    Public WriteOnly Property AllowedExtensions As String
        Set(ByVal value As String)
            For Each Str As String In value.Split(",")
                Str = Str.Trim
            Next
            RadAsyncUpload1.AllowedFileExtensions = value.Split(",")
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewInit(e As EventArgs)
        RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
    End Sub
    Public Overrides Sub ViewLoad(e As System.EventArgs)
        Hide()
    End Sub
#End Region

#Region "Event"
    Private Sub btnYES_Click(sender As Object, e As System.EventArgs) Handles btnYES.Click
        Hide()
        RaiseEvent OkClicked(sender, e)
    End Sub

    Private Sub btnNO_Click(sender As Object, e As System.EventArgs) Handles btnNO.Click
        Hide()
        RaiseEvent CancelClicked(sender, e)
    End Sub
#End Region

#Region "Custom"

    Public Sub Show()
        'rwMessage.Title = MessageTitle
        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True
    End Sub

    Public Sub Hide()
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub

#End Region

End Class