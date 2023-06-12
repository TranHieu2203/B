Imports Common.CommonBusiness
Imports Telerik.Web.UI

Public Class ctrlUploadWithTeamplate
    Inherits CommonView

#Region "Property"
    Delegate Sub OkClickedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Delegate Sub cboSelectedIndexChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Delegate Sub CancelClickedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event OkClicked As OkClickedDelegate
    Event cboSelectedIndexChanged As cboSelectedIndexChangedDelegate
    Event CancelClicked As CancelClickedDelegate
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property UploadedFiles As UploadedFileCollection
        Get
            Return RadAsyncUpload1.UploadedFiles
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Machine_Type As String
        Get
            Return cbMachine_type.SelectedValue
            Return Nothing
        End Get
    End Property

    Public Property MachineType As Decimal
        Get
            Return PageViewState(Me.ID & "_MachineType")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_MachineType") = value
        End Set
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
    Public Overrides Sub ViewInit(ByVal e As EventArgs)
        RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
        Try
            'Dim arr As New ArrayList()
            'arr.Add(New DictionaryEntry("--Chọn hệ thống Import--", "NON"))
            'arr.Add(New DictionaryEntry("Vân tay", "TOUCH_ID"))
            'arr.Add(New DictionaryEntry("Acess Control", "ACESS_CONTROL"))
            'arr.Add(New DictionaryEntry("Car Parking", "CAR_PARKING"))
            Dim ICom As ICommonBusiness = New CommonBusinessClient()
            Dim dtdata = ICom.GetOtherListByTypeToCombo("TIME_RECORDER")
            With cbMachine_type
                .DataSource = dtdata
                .DataTextField = "NAME_VN"
                .DataValueField = "CODE"
                .DataBind()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Hide()
    End Sub
#End Region

#Region "Event"
    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        Hide()
        RaiseEvent OkClicked(sender, e)
    End Sub

    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        Hide()
        RaiseEvent CancelClicked(sender, e)
    End Sub
    Private Sub cbMachine_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbMachine_type.SelectedIndexChanged
        RaiseEvent cboSelectedIndexChanged(sender, e)
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