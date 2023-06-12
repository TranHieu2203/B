<ValidationProperty("SelectedValue")>
Public Class ctrlFindFrameProductivityPopup
    Inherits CommonView
    Delegate Sub FrameProductivitySelectedDelegate(ByVal sender As Object, ByVal e As FrameProductivitySelectedEventArgs)
    Event FrameProductivitySelected As FrameProductivitySelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Delegate Sub CloseClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CloseClicked As CloseClick
    Public Overrides Property MustAuthorize As Boolean = False

    Public Property CheckChildNodes As Boolean
        Get
            Return ctrlFrameProductivity1.CheckChildNodes
        End Get
        Set(ByVal value As Boolean)
            ctrlFrameProductivity1.CheckChildNodes = value
        End Set
    End Property
    Public Property Bind_CheckedValueKeys As List(Of Decimal)
        Get
            Return ctrlFrameProductivity1.Bind_CheckedValueKeys
        End Get
        Set(ByVal value As List(Of Decimal))
            ctrlFrameProductivity1.Bind_CheckedValueKeys = value
        End Set
    End Property
    Public Property Bind_Find_ValueKeys As List(Of Decimal)
        Get
            Return ctrlFrameProductivity1.Bind_Find_ValueKeys
        End Get
        Set(ByVal value As List(Of Decimal))
            ctrlFrameProductivity1.Bind_Find_ValueKeys = value
        End Set
    End Property
    Public Property IS_HadLoad As Boolean?
        Get
            Return ViewState(Me.ID & "_IS_HadLoad")
        End Get
        Set(ByVal value As Boolean?)
            ViewState(Me.ID & "_IS_HadLoad") = value
        End Set
    End Property
    Public Property ShowCheckBoxes As TreeNodeTypes
        Get
            Return IIf(ViewState(Me.ID & "_ShowCheckBoxes") Is Nothing, TreeNodeTypes.None, ViewState(Me.ID & "_ShowCheckBoxes"))
        End Get
        Set(ByVal value As TreeNodeTypes)
            ViewState(Me.ID & "_ShowCheckBoxes") = value
        End Set
    End Property

    Public Property FrameProductivityType As FrameProductivityType
        Get
            Return ViewState(Me.ID & "_FrameProductivityType")
        End Get
        Set(ByVal value As FrameProductivityType)
            ViewState(Me.ID & "_FrameProductivityType") = value
        End Set
    End Property

    Public Property LoadAllFrameProductivityTree As Boolean
        Get
            Return ctrlFrameProductivity1.LoadAllFrameProductivityTree
        End Get
        Set(ByVal value As Boolean)
            ctrlFrameProductivity1.LoadAllFrameProductivityTree = value
        End Set
    End Property
    Public Property Enabled As Boolean
        Get
            Return IIf(ViewState(Me.ID & "_Enabled") Is Nothing, True, ViewState(Me.ID & "_Enabled"))
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Enabled") = value
        End Set
    End Property

    Public Function GetAllChild(ByVal _FrameProductivityId As Decimal) As List(Of Decimal)
        Return ctrlFrameProductivity1.GetAllChild(_FrameProductivityId)
    End Function


    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlFrameProductivity1.AutoPostBack = False
            ctrlFrameProductivity1.LoadDataAfterLoaded = True
            ctrlFrameProductivity1.FrameProductivityType = FrameProductivityType
            ctrlFrameProductivity1.CheckBoxes = ShowCheckBoxes
            ctrlFrameProductivity1.IS_HadLoad = IS_HadLoad
            If Enabled = False Then
                ctrlFrameProductivity1.Enabled = Enabled
                btnYES.Visible = Enabled
                btnNO.Visible = Enabled
                btnClose.Visible = Not Enabled
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Function CurrentStructureInfo() As List(Of OrganizationStructureDTO)
    '    Return ctrlFrameProductivity1.CurrentStructureInfo
    'End Function

    Public Function CurrentItemDataObject() As Object
        Return ctrlFrameProductivity1.CurrentItemDataObject
    End Function
    Public Function CheckedValueKeys() As List(Of Decimal)
        Return ctrlFrameProductivity1.CheckedValueKeys
    End Function
    'Public Function ListOrgChecked() As List(Of OrganizationDTO)
    '    Return ctrlOrg1.ListOrg_Checked

    'End Function

    Public ReadOnly Property IsDissolve As Boolean
        Get
            Return ctrlFrameProductivity1.IsDissolve
        End Get
    End Property

    Public Sub Show()
        rwMessage.VisibleOnPageLoad = True
        rwMessage.Visible = True
    End Sub

    Public Sub Hide()
        rwMessage.VisibleOnPageLoad = False
        rwMessage.Visible = False
    End Sub

    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        Hide()
        RaiseEvent FrameProductivitySelected(sender,
                                        New FrameProductivitySelectedEventArgs(
                                            ctrlFrameProductivity1.CheckedValues,
                                            ctrlFrameProductivity1.CheckedValueKeys,
                                            ctrlFrameProductivity1.GetCheckedTexts,
                                            ctrlFrameProductivity1.CurrentText,
                                            ctrlFrameProductivity1.CurrentValue,
                                            ctrlFrameProductivity1.CurrentItemDataObject,
                                            ctrlFrameProductivity1.CurrentCode))
    End Sub

    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        RaiseEvent CancelClicked(sender, e)
        Hide()
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent CloseClicked(sender, e)
        Hide()
    End Sub
End Class

Public Class FrameProductivitySelectedEventArgs
    Inherits EventArgs
    Public Sub New(ByVal _SelectedValues As List(Of String),
                   ByVal _CheckedValueDecimals As List(Of Decimal),
                   ByVal _SelectedTexts As List(Of String),
                   ByVal _CurrentText As String,
                   ByVal _CurrentValue As String,
                   ByVal _CurrentItemDataObject As Object,
                   ByVal _CurrentCode As String)
        SelectedValues = _SelectedValues
        CheckedValueDecimals = _CheckedValueDecimals
        SelectedTexts = _SelectedTexts
        CurrentValue = _CurrentValue
        CurrentText = _CurrentText
        CurrentCode = _CurrentCode
        CurrentItemDataObject = _CurrentItemDataObject
    End Sub
    Public SelectedValues As List(Of String)
    Public CheckedValueDecimals As List(Of Decimal)
    Public SelectedTexts As List(Of String)
    Public CurrentText As String
    Public CurrentCode As String
    Public CurrentValue As String
    Public CurrentItemDataObject As Object
End Class


