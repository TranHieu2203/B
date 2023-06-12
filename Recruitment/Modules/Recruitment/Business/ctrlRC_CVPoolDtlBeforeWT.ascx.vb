Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Public Class ctrlRC_CVPoolDtlBeforeWT
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
    Property lstCandidateBeforeWT As List(Of CandidateBeforeWTDTO)
        Get
            Return ViewState(Me.ID & "_lstCandidateBeforeWT")
        End Get
        Set(ByVal value As List(Of CandidateBeforeWTDTO))
            ViewState(Me.ID & "_lstCandidateBeforeWT") = value
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
            GetCandidateBeforeWT()
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
                        GetCandidateBeforeWT()
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
                        Dim lstData As List(Of CandidateBeforeWTDTO)
                        lstData = lstCandidateBeforeWT.ToList
                        Dim dtData1 = lstData.ToTable
                        If dtData1.Rows.Count > 0 Then
                            rgCandidateBeforeWT.ExportExcel(Server, Response, dtData1, "CandidateBeforeWorktrip")
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





    Private Sub rgCandidateBeforeWT_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCandidateBeforeWT.ItemDataBound
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

    Private Sub rgCandidateBeforeWT_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCandidateBeforeWT.SelectedIndexChanged
        Try
            If rgCandidateBeforeWT.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateBeforeWT.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next

            Dim item As GridDataItem = rgCandidateBeforeWT.SelectedItems(0)
            hidEmpBeforeWTID.Value = HttpUtility.HtmlDecode(item("ID").Text)
            If HttpUtility.HtmlDecode(item("FROMDATE").Text).Trim() <> "" Then

                rdFromdate.SelectedDate = item("FROMDATE").Text.ToDate
            Else
                rdFromdate.Clear()
            End If
            If HttpUtility.HtmlDecode(item("TODATE").Text).Trim() <> "" Then

                rdTodate.SelectedDate = item("TODATE").Text.ToDate
            Else
                rdTodate.Clear()
            End If
            txtOrgname.Text = HttpUtility.HtmlDecode(item("ORG_NAME").Text)
            txtPhone.Text = HttpUtility.HtmlDecode(item("ORG_PHONE").Text)
            txtOrgAddress.Text = HttpUtility.HtmlDecode(item("ORG_ADDRESS").Text)
            txtTitlename.Text = HttpUtility.HtmlDecode(item("TITLE_NAME").Text)
            txtWork.Text = HttpUtility.HtmlDecode(item("WORK").Text)
            txtReasonLeave.Text = HttpUtility.HtmlDecode(item("REASON_LEAVE").Text)
            If item("FROMDATE").Text.Trim() <> "" Then
                rdFromdate.SelectedDate = item("FROMDATE").Text
            End If
            If item("TODATE").Text.Trim() <> "" Then
                rdFromdate.SelectedDate = item("TODATE").Text
            End If
            txtDirectManager.Text = HttpUtility.HtmlDecode(item("DIRECT_MANAGER").Text)
            txtRemark.Text = HttpUtility.HtmlDecode(item("REMARK").Text)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteCandidateBeforeWT() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    GetCandidateBeforeWT() 'Load lại data cho lưới.
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

    Protected Sub cval_EffectDate_ExpireDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
        Try
            If rdTodate.SelectedDate IsNot Nothing Then
                If rdTodate.SelectedDate < rdFromdate.SelectedDate Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objCandidateBeforeWT As New CandidateBeforeWTDTO
            Dim rep As New RecruitmentRepository
            objCandidateBeforeWT.CANDIDATE_ID = CandidateInfo.ID
            objCandidateBeforeWT.FROMDATE = rdFromdate.SelectedDate
            objCandidateBeforeWT.TODATE = rdTodate.SelectedDate
            objCandidateBeforeWT.ORG_NAME = txtOrgname.Text
            objCandidateBeforeWT.TITLE_NAME = txtTitlename.Text
            objCandidateBeforeWT.WORK = txtWork.Text
            objCandidateBeforeWT.REMARK = txtRemark.Text
            objCandidateBeforeWT.ORG_PHONE = txtOrgname.Text
            objCandidateBeforeWT.ORG_ADDRESS = txtOrgAddress.Text
            objCandidateBeforeWT.DIRECT_MANAGER = txtDirectManager.Text
            objCandidateBeforeWT.REASON_LEAVE = txtReasonLeave.Text

            Dim gID As Decimal
            If hidEmpBeforeWTID.Value = "" Then
                rep.InsertCandidateBeforeWT(objCandidateBeforeWT, gID)
            Else
                objCandidateBeforeWT.ID = Decimal.Parse(hidEmpBeforeWTID.Value)
                rep.ModifyCandidateBeforeWT(objCandidateBeforeWT, gID)
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
                    EnabledGrid(rgCandidateBeforeWT, False)
                    SetStatusControl(True)
                    If Not isClear Then
                        ResetControlValue()
                        isClear = True
                    End If
                Case STATE_EDIT
                    EnabledGrid(rgCandidateBeforeWT, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgCandidateBeforeWT, True)
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
        For Each control As Control In RadPaneLeft.Controls
            If TypeOf control Is RadTextBox Then
                DirectCast(control, RadTextBox).ReadOnly = Not sTrangThai
            End If
        Next
        Utilities.EnableRadDatePicker(rdFromdate, sTrangThai)
        Utilities.EnableRadDatePicker(rdTodate, sTrangThai)
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        hidEmpBeforeWTID.Value = ""
        For Each control As Control In RadPaneLeft.Controls
            If TypeOf control Is RadTextBox Then
                DirectCast(control, RadTextBox).Text = String.Empty
            End If
        Next
        rdFromdate.SelectedDate = Nothing
        rdTodate.SelectedDate = Nothing
    End Sub

    Private Sub GetCandidateBeforeWT()
        Try
            If CandidateInfo IsNot Nothing Then
                Dim rep As New RecruitmentRepository
                Dim objCandidateBeforeWT As New CandidateBeforeWTDTO
                objCandidateBeforeWT.CANDIDATE_ID = CandidateInfo.ID
                lstCandidateBeforeWT = rep.GetCandidateBeforeWT(objCandidateBeforeWT)
                rgCandidateBeforeWT.DataSource = lstCandidateBeforeWT
                rgCandidateBeforeWT.DataBind()
                If IDSelect <> 0 Then
                    If rgCandidateBeforeWT.Items.Count > 0 Then
                        SelectedItemDataGridByKey(rgCandidateBeforeWT, IDSelect)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Function DeleteCandidateBeforeWT() As Boolean
        Try
            Dim rep As New RecruitmentRepository
            Return rep.DeleteCandidateBeforeWT(SelectedItem)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


End Class