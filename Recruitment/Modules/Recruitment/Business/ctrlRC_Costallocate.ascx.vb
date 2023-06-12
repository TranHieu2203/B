Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRC_Costallocate
    Inherits Common.CommonView
    Protected WithEvents StageView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    Private userlog As UserLog
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Public Property tabSource As DataTable
        Get
            Return PageViewState(Me.ID & "_tabSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_tabSource") = value
        End Set
    End Property

    Property lstItemDecimals As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_lstDeleteDecimals")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_lstDeleteDecimals") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get

            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set

    End Property
#End Region

#Region "Page"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Delete,
                                       ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgData, True)
                    txtOrgName.Enabled = False
                    btnFindORG.Enabled = False
                    ntbCostAmount.Enabled = False
                    txtRemark.Enabled = False
                    ResetText()
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgData, False)
                    txtOrgName.Enabled = True
                    btnFindORG.Enabled = True
                    ntbCostAmount.Enabled = True
                    txtRemark.Enabled = True
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            txtOrgName.Text = String.Empty
            ntbCostAmount.Value = Nothing
            txtRemark.Text = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                'Load thông tin đợt tuyển dụng
                hdStageID.Value = Request.Params("STAGE_ID")
                Dim tab = store.STAGE_GetByID(hdStageID.Value)
                lblTitle.Text = tab.Rows(0)("Title").ToString()
                lblOrganizationName.Text = tab.Rows(0)("ORGANIZATIONNAME").ToString()
                lblYear.Text = tab.Rows(0)("YEAR").ToString()

                Dim fromDate As DateTime = CType(tab.Rows(0)("STARTDATE"), DateTime)
                Dim toDate As DateTime = CType(tab.Rows(0)("ENDDATE"), DateTime)
                lblStartDate.Text = fromDate.ToString("dd/MM/yyyy")
                lblEndDate.Text = toDate.ToString("dd/MM/yyyy")

                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("ORG_NAME", txtOrgName)
        dic.Add("COSTAMOUNT", ntbCostAmount)
        dic.Add("REMARK", txtRemark)
        dic.Add("ORG_ID", hdOrgID)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCostallocate As COSTALLOCATE_DTO
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(Int32.Parse(item("ID").Text))
                    Next

                    lstItemDecimals = lstDeletes
                    If lstItemDecimals.Count > 0 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objCostallocate = New COSTALLOCATE_DTO
                        objCostallocate.ORG_ID = hdOrgID.Value
                        objCostallocate.RC_STAGE_ID = hdStageID.Value
                        objCostallocate.CostAmount = ntbCostAmount.Value
                        objCostallocate.REMARK = txtRemark.Text.Trim()

                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                IsSaveCompleted = store.COSTALLOCATE_AddNew(
                                                            objCostallocate.RC_STAGE_ID,
                                                            objCostallocate.ORG_ID,
                                                            objCostallocate.CostAmount,
                                                            objCostallocate.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                            Case CommonMessage.STATE_EDIT
                                objCostallocate.ID = rgData.SelectedValue
                                IsSaveCompleted = store.COSTALLOCATE_Update(
                                                            objCostallocate.ID,
                                                            objCostallocate.RC_STAGE_ID,
                                                            objCostallocate.ORG_ID,
                                                            objCostallocate.CostAmount,
                                                            objCostallocate.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))


                        End Select
                        If IsSaveCompleted Then

                            'Cập nhật tổng chi phí đã phân bổ vào chi phí dự kiến
                            store.STAGE_UpdateCostestimate(hdStageID.Value)

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            rgData.Rebind()
                            ResetText()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            Dim IsCompleted As Boolean
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                For Each item As Int32 In lstItemDecimals
                    IsCompleted = store.COSTALLOCATE_Delete(item)
                Next

                If IsCompleted Then
                    'Cập nhật tổng chi phí đã phân bổ vào chi phí dự kiến
                    store.STAGE_UpdateCostestimate(hdStageID.Value)

                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If

                CurrentState = CommonMessage.STATE_NORMAL
                rgData.Rebind()
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindORG.Click
        Try

            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnFindOrg_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hdOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindOrg_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub


#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            If hdStageID.Value IsNot Nothing Then
                tabSource = store.COSTALLOCATE_GetByStage(hdStageID.Value)
                If tabSource IsNot Nothing Then
                    rgData.VirtualItemCount = tabSource.Rows.Count
                    rgData.DataSource = tabSource
                Else
                    rgData.DataSource = New List(Of COSTALLOCATE_DTO)
                End If
            Else
                rgData.DataSource = New List(Of COSTALLOCATE_DTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

#End Region

End Class