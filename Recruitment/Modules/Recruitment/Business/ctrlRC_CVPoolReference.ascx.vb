Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Public Class ctrlRC_CVPoolReference
    Inherits CommonView
    Dim CandidateCode As String
#Region "Properties"
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property
    Property isClear As Boolean
        Get
            Return ViewState(Me.ID & "_isClear")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isClear") = value
        End Set
    End Property
    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Property lstReference As List(Of CandidateReferenceDTO)
        Get
            Return ViewState(Me.ID & "_lstReference")
        End Get
        Set(ByVal value As List(Of CandidateReferenceDTO))
            ViewState(Me.ID & "_lstReference") = value
        End Set
    End Property
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgReference.SetFilter()
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Edit,
                         ToolbarItem.Seperator, ToolbarItem.Save, ToolbarItem.Seperator,
                         ToolbarItem.Cancel, ToolbarItem.Seperator, ToolbarItem.Export, ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(4), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            GetReference()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlRC_CanBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    isClear = False
                Case TOOLBARITEM_EDIT
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isClear = False
                        isLoad = False
                        GetReference()
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    ' If CurrentState = STATE_NEW Then
                    ResetControlValue()
                    'End If
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Dim _error As Integer = 0
                    Using xls As New ExcelCommon
                        Dim lstData As List(Of CandidateReferenceDTO)
                        lstData = lstReference.ToList
                        Dim dtData1 = lstData.ToTable
                        If dtData1.Rows.Count > 0 Then
                            rgReference.ExportExcel(Server, Response, dtData1, "Reference")
                        Else
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                        End If
                    End Using
            End Select
            ctrlRC_CanBasicInfo.SetProperty("CurrentState", Me.CurrentState)
            ctrlRC_CanBasicInfo.Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgReference_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgReference.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                        e.Item.Selected = True
                    End If
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub rgReference_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReference.SelectedIndexChanged
        Try
            If rgReference.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgReference.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next

            Dim item As GridDataItem = rgReference.SelectedItems(0)
            hidReference.Value = HttpUtility.HtmlDecode(item("ID").Text)
            txtHoTen.Text = HttpUtility.HtmlDecode(item("FULLNAME").Text)
            txtChucDanh.Text = HttpUtility.HtmlDecode(item("TITLE_NAME").Text)
            txtDonViCongTac.Text = HttpUtility.HtmlDecode(item("WORK_UNIT").Text)
            txtDiaChiLienHe.Text = HttpUtility.HtmlDecode(item("ADDRESS_CONTACT").Text)
            If HttpUtility.HtmlDecode(item("PHONENUMBER").Text).Trim() <> "" Then
                txtSoDienThoai.Text = HttpUtility.HtmlDecode(item("PHONENUMBER").Text)
            Else
                txtSoDienThoai.Value = Nothing
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteTrainSinger() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    GetReference() 'Load lại data cho lưới.
                    'Hủy selectedItem của grid.
                    SelectedItem = Nothing
                    CurrentState = CommonMessage.STATE_NORMAL
                    isClear = False
                    UpdateControlState()
                    ResetControlValue()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objReference As New CandidateReferenceDTO
            Dim rep As New RecruitmentRepository
            objReference.CANDIDATE_ID = CandidateInfo.ID
            objReference.FULLNAME = txtHoTen.Text
            objReference.TITLE_NAME = txtChucDanh.Text
            objReference.WORK_UNIT = txtDonViCongTac.Text
            objReference.ADDRESS_CONTACT = txtDiaChiLienHe.Text
            objReference.PHONENUMBER = txtSoDienThoai.Text

            Dim gID As Decimal
            If hidReference.Value = "" Then
                rep.InsertCandidateReference(objReference, gID)
            Else
                objReference.ID = Decimal.Parse(hidReference.Value)
                rep.ModifyCandidateReference(objReference, gID)
            End If
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Try
            If CurrentState Is Nothing Then
                CurrentState = STATE_NORMAL
            End If
            Select Case CurrentState
                Case STATE_NEW
                    EnabledGrid(rgReference, False)
                    SetStatusControl(True)
                    If Not isClear Then
                        ResetControlValue()
                        isClear = True
                    End If
                Case STATE_EDIT
                    EnabledGrid(rgReference, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgReference, True)
                    SetStatusControl(False)
            End Select
            Me.Send(CurrentState)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub SetStatusToolBar()
        Try
            Select Case CurrentState
                Case STATE_NORMAL
                    Me.MainToolBar.Items(0).Enabled = True 'Add
                    Me.MainToolBar.Items(2).Enabled = True 'Edit
                    Me.MainToolBar.Items(8).Enabled = True 'Delete

                    Me.MainToolBar.Items(4).Enabled = False 'Save
                    Me.MainToolBar.Items(6).Enabled = False 'Cancel
                Case STATE_NEW, STATE_EDIT
                    Me.MainToolBar.Items(0).Enabled = False 'Add
                    Me.MainToolBar.Items(2).Enabled = False 'Edit
                    Me.MainToolBar.Items(8).Enabled = False 'Delete

                    Me.MainToolBar.Items(4).Enabled = True 'Save
                    Me.MainToolBar.Items(6).Enabled = True 'Cancel

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)
        txtChucDanh.ReadOnly = Not sTrangThai
        txtDiaChiLienHe.ReadOnly = Not sTrangThai
        txtDonViCongTac.ReadOnly = Not sTrangThai
        txtHoTen.ReadOnly = Not sTrangThai
        txtSoDienThoai.ReadOnly = Not sTrangThai
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        hidReference.Value = ""
        txtChucDanh.Text = ""
        txtDiaChiLienHe.Text = ""
        txtDonViCongTac.Text = ""
        txtHoTen.Text = ""
        txtSoDienThoai.Text = ""
    End Sub

    Private Sub GetReference()
        Try
            If CandidateInfo IsNot Nothing Then
                Dim rep As New RecruitmentRepository
                Dim objReference As New CandidateReferenceDTO
                objReference.CANDIDATE_ID = CandidateInfo.ID
                lstReference = rep.GetCandidateReference(objReference)
                rgReference.DataSource = lstReference
                rgReference.DataBind()
                If IDSelect <> 0 Then
                    If rgReference.Items.Count > 0 Then
                        SelectedItemDataGridByKey(rgReference, IDSelect)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Function DeleteTrainSinger() As Boolean
        Try
            Dim rep As New RecruitmentRepository
            Return rep.DeleteCandidateReference(SelectedItem)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class