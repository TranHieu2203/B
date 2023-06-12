Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Public Class ctrlRC_CVPoolDtlTraining
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
    Property lstTrainSinger As List(Of TrainSingerDTO)
        Get
            Return ViewState(Me.ID & "_lstTrainSinger")
        End Get
        Set(ByVal value As List(Of TrainSingerDTO))
            ViewState(Me.ID & "_lstTrainSinger") = value
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
        rgSingleTranning.SetFilter()
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
            GetTrainSinger()
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
                        GetTrainSinger()
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
                        Dim lstData As List(Of TrainSingerDTO)
                        lstData = lstTrainSinger.ToList
                        Dim dtData1 = lstData.ToTable
                        If dtData1.Rows.Count > 0 Then
                            rgSingleTranning.ExportExcel(Server, Response, dtData1, "Trainning")
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

    Private Sub rgSingleTranning_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgSingleTranning.ItemDataBound
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

    Protected Sub cval_FromDate_ToDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_FromDate_ToDate.ServerValidate
        Try
            If rdFromdate.SelectedDate IsNot Nothing And rdTodate.SelectedDate IsNot Nothing Then
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

    Private Sub rgTrainSinger_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgSingleTranning.SelectedIndexChanged
        Try
            If rgSingleTranning.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgSingleTranning.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next

            Dim item As GridDataItem = rgSingleTranning.SelectedItems(0)
            hidTrainSinger.Value = HttpUtility.HtmlDecode(item("ID").Text)
            txtSchoolName.Text = HttpUtility.HtmlDecode(item("SCHOOL_NAME").Text)
            txtBranchName.Text = HttpUtility.HtmlDecode(item("BRANCH_NAME").Text)
            txtLevel.Text = HttpUtility.HtmlDecode(item("LEVEL").Text)
            txtCERTIFICATE.Text = HttpUtility.HtmlDecode(item("CERTIFICATE").Text)
            txtContent.Text = HttpUtility.HtmlDecode(item("CONTENT").Text)
            txtTraining.Text = HttpUtility.HtmlDecode(item("TRAINNING").Text)
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
            txtRank.Text = HttpUtility.HtmlDecode(item("RANK").Text)
            rntxYeah.Text = HttpUtility.HtmlDecode(item("YEAR_GRADUATE").Text)
            If HttpUtility.HtmlDecode(item("COST").Text).Trim() <> "" Then
                rntxCost.Value = Decimal.Parse(HttpUtility.HtmlDecode(item("COST").Text).Trim())
            End If
            txtRemark.Text = HttpUtility.HtmlDecode(item("REMARK").Text)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteTrainSinger() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    GetTrainSinger() 'Load lại data cho lưới.
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
            Dim objTrainSinger As New TrainSingerDTO
            Dim rep As New RecruitmentRepository
            objTrainSinger.CANDIDATE_ID = CandidateInfo.ID
            objTrainSinger.SCHOOL_NAME = txtSchoolName.Text
            objTrainSinger.BRANCH_NAME = txtBranchName.Text
            objTrainSinger.LEVEL = txtLevel.Text
            objTrainSinger.CERTIFICATE = txtCERTIFICATE.Text
            objTrainSinger.CONTENT = txtContent.Text
            objTrainSinger.TRAINNING = txtTraining.Text
            objTrainSinger.FROMDATE = rdFromdate.SelectedDate
            objTrainSinger.TODATE = rdTodate.SelectedDate
            objTrainSinger.RANK = txtRank.Text
            objTrainSinger.REMARK = txtRemark.Text
            objTrainSinger.COST = If(rntxCost.Value Is Nothing, 0, rntxCost.Value)
            objTrainSinger.YEAR_GRADUATE = If(rntxYeah.Value Is Nothing, 0, rntxYeah.Value)
            Dim gID As Decimal
            If hidTrainSinger.Value = "" Then
                rep.InsertCandidateTrainSinger(objTrainSinger, gID)
            Else
                objTrainSinger.ID = Decimal.Parse(hidTrainSinger.Value)
                rep.ModifyCandidateTrainSinger(objTrainSinger, gID)
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
                    EnabledGrid(rgSingleTranning, False)
                    SetStatusControl(True)
                    If Not isClear Then
                        ResetControlValue()
                        isClear = True
                    End If
                Case STATE_EDIT
                    EnabledGrid(rgSingleTranning, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgSingleTranning, True)
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
        txtSchoolName.ReadOnly = Not sTrangThai
        txtBranchName.ReadOnly = Not sTrangThai
        txtCERTIFICATE.ReadOnly = Not sTrangThai
        txtContent.ReadOnly = Not sTrangThai
        txtLevel.ReadOnly = Not sTrangThai
        txtTraining.ReadOnly = Not sTrangThai
        txtRank.ReadOnly = Not sTrangThai
        rntxYeah.ReadOnly = Not sTrangThai
        rntxCost.ReadOnly = Not sTrangThai
        txtRemark.ReadOnly = Not sTrangThai
        Utilities.EnableRadDatePicker(rdFromdate, sTrangThai)
        Utilities.EnableRadDatePicker(rdTodate, sTrangThai)
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        hidTrainSinger.Value = ""
        txtSchoolName.Text = ""
        txtBranchName.Text = ""
        txtLevel.Text = ""
        txtCERTIFICATE.Text = ""
        txtContent.Text = ""
        txtTraining.Text = ""
        rdFromdate.SelectedDate = Nothing
        rdTodate.SelectedDate = Nothing
        txtRank.Text = ""
        rntxYeah.Value = 0
        rntxCost.Value = 0
        txtRank.Text = ""
    End Sub

    Private Sub GetTrainSinger()
        Try
            If CandidateInfo IsNot Nothing Then
                Dim rep As New RecruitmentRepository
                Dim objTrainSinger As New TrainSingerDTO
                objTrainSinger.CANDIDATE_ID = CandidateInfo.ID
                lstTrainSinger = rep.GetCandidateTrainSinger(objTrainSinger)
                rgSingleTranning.DataSource = lstTrainSinger
                rgSingleTranning.DataBind()
                If IDSelect <> 0 Then
                    If rgSingleTranning.Items.Count > 0 Then
                        SelectedItemDataGridByKey(rgSingleTranning, IDSelect)
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
            Return rep.DeleteCandidateTrainSinger(SelectedItem)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class