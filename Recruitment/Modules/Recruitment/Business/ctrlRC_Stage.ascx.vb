Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRC_Stage
    Inherits Common.CommonView
    'Protected WithEvents StageView As ViewBase
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

    'Property IDSelect As Decimal
    '    Get
    '        Return ViewState(Me.ID & "_IDSelect")
    '    End Get
    '    Set(ByVal value As Decimal)
    '        ViewState(Me.ID & "_IDSelect") = value
    '    End Set
    'End Property

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
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGrid(rgData, True)
                    txtOrganizationName.Enabled = False
                    txtTitle.Enabled = False
                    Utilities.EnableRadDatePicker(dateStart, False)
                    Utilities.EnableRadDatePicker(dateEnd, False)
                    Utilities.EnableRadCombo(cbbSourceOfRec, False)
                    rntCostEstimate.Enabled = False
                    txtRemark.Enabled = False
                    btnSearch.Enabled = True
                    ResetText()
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGrid(rgData, False)
                    txtOrganizationName.Enabled = True
                    txtTitle.Enabled = True
                    Utilities.EnableRadDatePicker(dateStart, True)
                    Utilities.EnableRadDatePicker(dateEnd, True)
                    Utilities.EnableRadCombo(cbbSourceOfRec, True)
                    rntCostEstimate.Enabled = False
                    txtRemark.Enabled = True
                    btnSearch.Enabled = False

            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            txtOrganizationName.Text = String.Empty
            txtTitle.Text = String.Empty
            dateStart.SelectedDate = Nothing
            dateEnd.SelectedDate = Nothing
            cbbSourceOfRec.SelectedValue = Nothing
            cbbSourceOfRec.Text = ""
            rntCostEstimate.Value = Nothing
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

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rntYear.Value = Date.Now.Year
                btnSearch.Enabled = True
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


#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objStage As STAGE_DTO
        'Dim rtnValue As Boolean
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    ResetText()

                    'set default "Đơn vị khai thác" = select Org Name
                    If ctrlOrg.CurrentValue <> "" Then
                        txtOrganizationName.Text = ctrlOrg.CurrentText
                    End If
                    txtTitle.Focus()
                    ctrlOrg.Enabled = False
                    rgData.Enabled = False

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
                        objStage = New STAGE_DTO
                        objStage.ORG_ID = ctrlOrg.CurrentValue
                        objStage.YEAR = rntYear.Value
                        objStage.ORGANIZATIONNAME = ctrlOrg.CurrentText.Trim()
                        objStage.TITLE = txtTitle.Text.Trim()
                        objStage.STARTDATE = dateStart.SelectedDate
                        objStage.ENDDATE = dateEnd.SelectedDate
                        If cbbSourceOfRec.SelectedValue <> String.Empty Then
                            objStage.SOURCEOFREC_ID = cbbSourceOfRec.SelectedValue
                        Else
                            objStage.SOURCEOFREC_ID = Nothing
                        End If
                        objStage.REMARK = txtRemark.Text.Trim()

                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                IsSaveCompleted = store.STAGE_AddNew(
                                                            objStage.ORG_ID,
                                                            objStage.YEAR,
                                                            objStage.ORGANIZATIONNAME,
                                                            objStage.TITLE,
                                                            objStage.STARTDATE,
                                                            objStage.ENDDATE,
                                                            objStage.SOURCEOFREC_ID,
                                                            objStage.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                            Case CommonMessage.STATE_EDIT
                                objStage.ID = rgData.SelectedValue
                                IsSaveCompleted = store.STAGE_Update(
                                                            objStage.ID,
                                                            objStage.ORG_ID,
                                                            objStage.YEAR,
                                                            objStage.ORGANIZATIONNAME,
                                                            objStage.TITLE,
                                                            objStage.STARTDATE,
                                                            objStage.ENDDATE,
                                                            objStage.SOURCEOFREC_ID,
                                                            objStage.REMARK,
                                                            userlog.Username,
                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                        End Select
                        If IsSaveCompleted Then
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
                    ctrlOrg.Enabled = True
                    rgData.Enabled = True
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
                    IsCompleted = store.STAGE_Delete(item)
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
        'Dim dtData As DataTable
        'Try
        '    Using rep As New RecruitmentRepository
        '        hidOrgID.Value = ""
        '        If ctrlOrg.CurrentValue <> "" Then
        '            hidOrgID.Value = ctrlOrg.CurrentValue
        '        End If
        '        rgData.Rebind()
        '    End Using
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try

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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then

                GetDataComboBox()

                rgData.DataSource = Nothing
                tabSource = store.STAGE_GetList(Decimal.Parse(ctrlOrg.CurrentValue), rntYear.Value)
                If tabSource IsNot Nothing Then
                    rgData.VirtualItemCount = tabSource.Rows.Count
                    rgData.DataSource = tabSource
                Else
                    rgData.DataSource = New List(Of STAGE_DTO)
                End If
            Else
                rgData.DataSource = New List(Of STAGE_DTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        'Reset all text form
        ResetText()

        Dim dataItem = TryCast(rgData.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            txtOrganizationName.Text = dataItem("ORGANIZATIONNAME").Text
            txtTitle.Text = dataItem("TITLE").Text
            dateStart.SelectedDate = DateTime.ParseExact(dataItem("STARTDATE").Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
            dateEnd.SelectedDate = DateTime.ParseExact(dataItem("ENDDATE").Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
            cbbSourceOfRec.SelectedValue = dataItem("SOURCE_ID").Text
            rntCostEstimate.Text = Utilities.ObjToInt(HttpUtility.HtmlDecode(dataItem("COSTESTIMATE").Text).Trim)
            txtRemark.Text = dataItem("REMARK").Text
        End If
    End Sub
End Class