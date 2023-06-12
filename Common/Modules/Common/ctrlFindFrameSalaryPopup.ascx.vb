<ValidationProperty("SelectedValue")>
Public Class ctrlFindFrameSalaryPopup
    Inherits CommonView
    Delegate Sub FrameSalarySelectedDelegate(ByVal sender As Object, ByVal e As FrameSalarySelectedEventArgs)
    Event FrameSalarySelected As FrameSalarySelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick
    Delegate Sub CloseClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CloseClicked As CloseClick
    Public Overrides Property MustAuthorize As Boolean = False

    Public Property CheckChildNodes As Boolean
        Get
            Return ctrlFrameSalary1.CheckChildNodes
        End Get
        Set(ByVal value As Boolean)
            ctrlFrameSalary1.CheckChildNodes = value
        End Set
    End Property
    Public Property Bind_CheckedValueKeys As List(Of Decimal)
        Get
            Return ctrlFrameSalary1.Bind_CheckedValueKeys
        End Get
        Set(ByVal value As List(Of Decimal))
            ctrlFrameSalary1.Bind_CheckedValueKeys = value
        End Set
    End Property
    Public Property Bind_Find_ValueKeys As List(Of Decimal)
        Get
            Return ctrlFrameSalary1.Bind_Find_ValueKeys
        End Get
        Set(ByVal value As List(Of Decimal))
            ctrlFrameSalary1.Bind_Find_ValueKeys = value
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

    Public Property FrameSalaryType As FrameSalaryType
        Get
            Return ViewState(Me.ID & "_FrameSalaryType")
        End Get
        Set(ByVal value As FrameSalaryType)
            ViewState(Me.ID & "_FrameSalaryType") = value
        End Set
    End Property

    Public Property LoadAllFrameSalaryTree As Boolean
        Get
            Return ctrlFrameSalary1.LoadAllFrameSalaryTree
        End Get
        Set(ByVal value As Boolean)
            ctrlFrameSalary1.LoadAllFrameSalaryTree = value
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

    Public Function GetAllChild(ByVal _FrameSalaryId As Decimal) As List(Of Decimal)
        Return ctrlFrameSalary1.GetAllChild(_FrameSalaryId)
    End Function


    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlFrameSalary1.AutoPostBack = False
            ctrlFrameSalary1.LoadDataAfterLoaded = True
            ctrlFrameSalary1.FrameSalaryType = FrameSalaryType
            ctrlFrameSalary1.CheckBoxes = ShowCheckBoxes
            ctrlFrameSalary1.IS_HadLoad = IS_HadLoad
            If Enabled = False Then
                ctrlFrameSalary1.Enabled = Enabled
                btnYES.Visible = Enabled
                btnNO.Visible = Enabled
                btnClose.Visible = Not Enabled
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Function CurrentStructureInfo() As List(Of OrganizationStructureDTO)
    '    Return ctrlFrameSalary1.CurrentStructureInfo
    'End Function

    Public Function CurrentItemDataObject() As Object
        Return ctrlFrameSalary1.CurrentItemDataObject
    End Function
    Public Function CheckedValueKeys() As List(Of Decimal)
        Return ctrlFrameSalary1.CheckedValueKeys
    End Function
    'Public Function ListOrgChecked() As List(Of OrganizationDTO)
    '    Return ctrlOrg1.ListOrg_Checked

    'End Function

    Public ReadOnly Property IsDissolve As Boolean
        Get
            Return ctrlFrameSalary1.IsDissolve
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
        RaiseEvent FrameSalarySelected(sender,
                                        New FrameSalarySelectedEventArgs(
                                            ctrlFrameSalary1.CheckedValues,
                                            ctrlFrameSalary1.CheckedValueKeys,
                                            ctrlFrameSalary1.GetCheckedTexts,
                                            ctrlFrameSalary1.CurrentText,
                                            ctrlFrameSalary1.CurrentValue,
                                            ctrlFrameSalary1.CurrentItemDataObject,
                                            ctrlFrameSalary1.CurrentCode))
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

Public Class FrameSalarySelectedEventArgs
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


