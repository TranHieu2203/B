Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRC_StageCost
    Inherits Common.CommonView
    Protected WithEvents StageView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    Private userlog As UserLog

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

    Public Property cbxData As Recruitment.RecruitmentBusiness.ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_cbxData")
        End Get
        Set(ByVal value As Recruitment.RecruitmentBusiness.ComboBoxDataDTO)
            PageViewState(Me.ID & "_cbxData") = value
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
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgData, True)
                    Utilities.EnableRadCombo(cbbStage, False)
                    Utilities.EnableRadCombo(cbbSourceOfRec, False)
                    rntCostEstimate.Enabled = False
                    rntCostReality.Enabled = False
                    txtRemark.Enabled = False
                    ResetText()
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgData, False)
                    Utilities.EnableRadCombo(cbbStage, True)
                    Utilities.EnableRadCombo(cbbSourceOfRec, True)
                    rntCostEstimate.Enabled = False
                    rntCostReality.Enabled = True
                    txtRemark.Enabled = True
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            cbbStage.SelectedValue = Nothing
            cbbStage.Text = ""
            cbbSourceOfRec.SelectedValue = Nothing
            cbbSourceOfRec.Text = ""
            rntCostEstimate.Value = Nothing
            rntCostReality.Value = Nothing
            txtRemark.Text = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GetDataComboBox()
        cbxData = New Recruitment.RecruitmentBusiness.ComboBoxDataDTO()
        cbxData.GET_SOURCE_REC = True

        If RecruitmentRepositoryStatic.Instance.GetComboList(cbxData) Then
            FillDropDownList(cbbSourceOfRec, cbxData.LIST_SOURCE_REC, "NAME_VN", "CODE", Common.Common.SystemLanguage, True, cbbSourceOfRec.SelectedValue)
        End If

        If ctrlOrg.CurrentValue <> Nothing Then
            cbbStage.DataSource = store.STAGE_GetData_Combobox(ctrlOrg.CurrentValue)
            cbbStage.DataValueField = "ID"
            cbbStage.DataTextField = "TITLE"
            cbbStage.DataBind()
        End If

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hdIsChanged.Value = 0
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

        dic.Add("RC_STAGE_ID", cbbStage)
        dic.Add("SOURCEOFREC_ID", cbbSourceOfRec)
        dic.Add("COSTESTIMATE", rntCostEstimate)
        dic.Add("COSTREALITY", rntCostReality)
        dic.Add("REMARK", txtRemark)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCost As COST_DTO
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    hdIsChanged.Value = 1

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
                    hdIsChanged.Value = 1
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
                        objCost = New COST_DTO
                        objCost.ORG_ID = ctrlOrg.CurrentValue
                        objCost.RC_STAGE_ID = Decimal.Parse(cbbStage.SelectedValue)
                        'objCost.COSTESTIMATE = Decimal.Parse(rntCostEstimate.Text)
                        'duy fix ngay 11/07
                        objCost.COSTESTIMATE = If(rntCostEstimate.Value IsNot Nothing, rntCostEstimate.Value, 0)
                        '''''''''

                        objCost.REMARK = txtRemark.Text.Trim()
                        If cbbSourceOfRec.SelectedValue <> String.Empty Then
                            objCost.SOURCEOFREC_ID = cbbSourceOfRec.SelectedValue
                        Else
                            objCost.SOURCEOFREC_ID = 0
                        End If
                        If rntCostReality.Text <> String.Empty Then
                            objCost.COSTREALITY = Decimal.Parse(rntCostReality.Text)
                        Else
                            objCost.COSTREALITY = 0
                        End If

                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                IsSaveCompleted = store.COST_AddNew(
                                                            objCost.RC_STAGE_ID,
                                                            objCost.ORG_ID,
                                                            objCost.SOURCEOFREC_ID,
                                                            objCost.COSTESTIMATE,
                                                            objCost.COSTREALITY,
                                                            objCost.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                            Case CommonMessage.STATE_EDIT
                                objCost.ID = rgData.SelectedValue
                                IsSaveCompleted = store.COST_Update(
                                                            objCost.ID,
                                                            objCost.RC_STAGE_ID,
                                                            objCost.ORG_ID,
                                                            objCost.SOURCEOFREC_ID,
                                                            objCost.COSTESTIMATE,
                                                            objCost.COSTREALITY,
                                                            objCost.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                        End Select
                        If IsSaveCompleted Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            hdIsChanged.Value = 0
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
                    hdIsChanged.Value = 0
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
                    IsCompleted = store.COST_Delete(item)
                Next

                If IsCompleted Then
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

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
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

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then

                'Test
                GetDataComboBox()

                tabSource = store.COST_GetList(Decimal.Parse(ctrlOrg.CurrentValue))
                If tabSource IsNot Nothing Then
                    rgData.VirtualItemCount = tabSource.Rows.Count
                    rgData.DataSource = tabSource
                Else
                    rgData.DataSource = New List(Of COST_DTO)
                End If
            Else
                rgData.DataSource = New List(Of COST_DTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)

        If CurrentState <> CommonMessage.STATE_NORMAL Then
            Dim str As String
            Dim arr() As String
            arr = e.Argument.Split("_")
            str = arr(arr.Count - 1)

            Select Case str
                Case "cbbStage"
                    Dim tab As New DataTable()
                    tab = store.STAGE_GetByID(cbbStage.SelectedValue)
                    If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                        rntCostEstimate.Text = If(tab.Rows(0)("COSTESTIMATE") IsNot Nothing, tab.Rows(0)("COSTESTIMATE").ToString(), "")
                    End If

                Case "cbbSourceOfRec"
                    rntCostReality.Text = cbbSourceOfRec.SelectedValue
            End Select
        End If

    End Sub

#End Region



End Class