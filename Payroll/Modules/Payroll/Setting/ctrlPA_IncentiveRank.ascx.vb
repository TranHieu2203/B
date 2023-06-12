Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_IncentiveRank
    Inherits Common.CommonView

#Region "Property"

    Dim lstIncentives As New List(Of IncentiveRankDTO)
    Dim lstIncentiveDetail As New List(Of IncentiveRankDetailDTO)

    Property Incentives As List(Of IncentiveRankDTO)
        Get
            Return ViewState(Me.ID & "_Incentive")
        End Get
        Set(ByVal value As List(Of IncentiveRankDTO))
            ViewState(Me.ID & "_Incentive") = value
        End Set
    End Property

    Property IncentiveDetails As List(Of IncentiveRankDetailDTO)
        Get
            Return ViewState(Me.ID & "_IncentiveDetail")
        End Get
        Set(ByVal value As List(Of IncentiveRankDetailDTO))
            ViewState(Me.ID & "_IncentiveDetail") = value
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
    Property DeleteIncentives As List(Of IncentiveRankDTO)
        Get
            Return ViewState(Me.ID & "_DeleteIncentives")
        End Get
        Set(ByVal value As List(Of IncentiveRankDTO))
            ViewState(Me.ID & "_DeleteIncentives") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            If Not IsPostBack Then
                GetSalaryGroupCombo(Date.Now)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgRank.AllowSorting = False
            rgRankDetail.AllowSorting = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarIncentiveRanks
            Common.Common.BuildToolbar(Me.MainToolBar _
                                       , ToolbarItem.Create _
                                       , ToolbarItem.Edit _
                                       , ToolbarItem.Save _
                                       , ToolbarItem.Cancel _
                                       , ToolbarItem.Active _
                                       , ToolbarItem.Deactive _
                                       , ToolbarItem.Delete
                                       )
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgRank.Rebind()
                rgRankDetail.Rebind()
                GetIncentiveCombo()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgRank.Rebind()
                        SelectedItemDataGridByKey(rgRank, IDSelect, , rgRank.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgRank.CurrentPageIndex = 0
                        rgRank.MasterTableView.SortExpressions.Clear()
                        rgRank.Rebind()
                        SelectedItemDataGridByKey(rgRank, IDSelect, )
                    Case "Cancel"
                        rgRank.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objIncentive As New IncentiveRankDTO
        Dim objIncentiveDetail As New IncentiveRankDetailDTO

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgRank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgRank.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgRank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of IncentiveRankDTO)
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgRank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgRank.SelectedItems(idx)
                        lstDeletes.Add(New IncentiveRankDTO With {.ID = Decimal.Parse(item("ID").Text)})
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    DeleteIncentives = lstDeletes
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'Dim dtData As DataTable
                    'Using xls As New ExcelCommon
                    '    dtData = CreateDataFilter(True)
                    '    If dtData.Rows.Count > 0 Then
                    '        rgData.ExportExcel(Server, Response, dtData, "SalaryLevel")
                    '    End If
                    'End Using
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        If cboSalaryGroup.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn bảng lương"), NotifyType.Warning)
                            cboSalaryGroup.Focus()
                            Exit Sub
                        End If
                        If cboSalaryLevel.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn ngạch lương"), NotifyType.Warning)
                            cboSalaryLevel.Focus()
                            Exit Sub
                        End If
                        If cboSalaryRank.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn bậc lương"), NotifyType.Warning)
                            cboSalaryRank.Focus()
                            Exit Sub
                        End If
                        If cboIncentiveType.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn nhóm lương thưởng"), NotifyType.Warning)
                            cboIncentiveType.Focus()
                            Exit Sub
                        End If
                        If rgRankDetail.Items.Count < 1 Then
                            ShowMessage(Translate("Bạn phải thiết lập mức thưởng chi tiết"), NotifyType.Warning)
                            txtFromTarget.Focus()
                            Exit Sub
                        End If
                        Dim gID As Decimal
                        With objIncentive
                            .ID = If(String.IsNullOrEmpty(hidID.Value), 0, Decimal.Parse(hidID.Value))
                            If Not String.IsNullOrEmpty(cboSalaryGroup.SelectedValue) Then
                                .SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                            End If
                            If Not String.IsNullOrEmpty(cboSalaryLevel.SelectedValue) Then
                                .SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                            End If
                            If Not String.IsNullOrEmpty(cboSalaryRank.SelectedValue) Then
                                .SAL_RANK_ID = cboSalaryRank.SelectedValue
                            End If
                            If Not String.IsNullOrEmpty(cboIncentiveType.SelectedValue) Then
                                .SAL_INCENTIVE_ID = cboIncentiveType.SelectedValue
                            End If

                            .EFFECT_DATE = rdEffectDate.SelectedDate
                            .ACTFLG = If(cbACTFLG.Checked, 1, 0)
                            .REMARK = String.Empty
                            .ORDERS = If(String.IsNullOrEmpty(rntxtOrders.Text), 1, Decimal.Parse(rntxtOrders.Text))

                            Dim chk As RadButton
                            For Each item As GridDataItem In rgRankDetail.Items
                                Dim detail = New IncentiveRankDetailDTO
                                detail.INCENTIVE_RANK_ID = item.GetDataKeyValue("INCENTIVE_RANK_ID")
                                detail.FROM_TARGET = item.GetDataKeyValue("FROM_TARGET")
                                detail.TO_TARGET = item.GetDataKeyValue("TO_TARGET")
                                detail.AMOUNT = item.GetDataKeyValue("AMOUNT")
                                chk = DirectCast(item.FindControl("chkINCENTIVE_TYPE_Percent"), RadButton)
                                detail.INCENTIVE_PERCENT = If(chk.Checked, 1, 0)
                                chk = DirectCast(item.FindControl("chkINCENTIVE_TYPE_Amount"), RadButton)
                                detail.INCENTIVE_AMOUNT = If(chk.Checked, 1, 0)
                                detail.REMARK = String.Empty
                                detail.ACTFLG = "A"
                                detail.ORDERS = 1
                                lstIncentiveDetail.Add(detail)
                            Next

                            .INCENTIVERANKDETAIL = lstIncentiveDetail
                        End With
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objIncentive.ACTFLG = "A"
                                    If rep.InsertIncentiveRankIncludeDetail(objIncentive, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        CreateDataIncentiveDetail()
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objIncentive.ID = Decimal.Parse(hidID.Value)
                                    If rep.ModifyIncentiveRankIncludeDetail(objIncentive, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgRank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgRank.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
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
        If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
    End Sub

    Protected Sub cboCombo_ItemChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalaryGroup.SelectedIndexChanged, cboSalaryLevel.SelectedIndexChanged
        Try
            Dim rgLoadRanks As Boolean = False
            Select Case sender.ID
                Case cboSalaryGroup.ID
                    GetSalaryLevelCombo(If(String.IsNullOrEmpty(cboSalaryGroup.SelectedValue), 0, cboSalaryGroup.SelectedValue))
                    rgLoadRanks = True
                Case cboSalaryLevel.ID
                    GetSalaryRankCombo(If(String.IsNullOrEmpty(cboSalaryLevel.SelectedValue), 0, cboSalaryLevel.SelectedValue))
                Case cboSalaryRank.ID
            End Select
            If rgLoadRanks Then
                rgLoadRanks = False
                rgRank.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboSalaryGroup.ItemsRequested, cboSalaryLevel.ItemsRequested, cboSalaryRank.ItemsRequested
        Try
            Dim rgLoadRanks As Boolean = False
            Using rep As New PayrollRepository

                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim dateValue As Date
                Select Case sender.ID
                    Case cboSalaryGroup.ID
                        If e.Context("valueCustom") Is Nothing Then
                            dateValue = Date.Now
                        Else
                            dateValue = Date.Parse(e.Context("valueCustom"))
                        End If
                        GetSalaryGroupCombo(dateValue)
                        rgLoadRanks = True
                    Case cboSalaryLevel.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        GetSalaryLevelCombo(dValue)
                    Case cboSalaryRank.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        GetSalaryRankCombo(dValue)

                End Select
                If rgLoadRanks Then
                    rgLoadRanks = False
                    rgRank.Rebind()
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgRankDetail_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgRankDetail.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertRankDetail"
                    Dim cmdItem As GridCommandItem = DirectCast(rgRankDetail.MasterTableView.GetItems(GridItemType.CommandItem)(0), GridCommandItem)
                    Dim btn As RadButton = DirectCast(cmdItem.FindControl("btnInsertRankDetail"), RadButton)
                    If btn.ToolTip.ToUpper() = "SAVE" Then
                        'Valid Save
                        If String.IsNullOrEmpty(txtFromTarget.Text) Then
                            ShowMessage(Translate("Bạn phải nhập từ %"), NotifyType.Warning)
                            txtFromTarget.Focus()
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(txtToTarget.Text) Then
                            ShowMessage(Translate("Bạn phải nhập đến %"), NotifyType.Warning)
                            txtToTarget.Focus()
                            Exit Sub
                        End If
                        If Decimal.Parse(txtToTarget.Text) < Decimal.Parse(txtFromTarget.Text) Then
                            ShowMessage(Translate("Từ % phải nhỏ hơn đên %"), NotifyType.Warning)
                            txtToTarget.Focus()
                            Exit Sub
                        End If

                        If String.IsNullOrEmpty(txtAmount.Text) Then
                            ShowMessage(Translate("Bạn phải nhập số tiền"), NotifyType.Warning)
                            txtToTarget.Focus()
                            Exit Sub
                        End If
                        If Not cbIncentiveType_Percent.Checked And Not cbIncentiveType_Amount.Checked Then
                            ShowMessage(Translate("Bạn phải chọn loại thưởng"), NotifyType.Warning)
                            cbIncentiveType_Percent.Focus()
                            Exit Sub
                        End If

                        For Each item As GridDataItem In rgRankDetail.Items
                            If item.GetDataKeyValue("FROM_TARGET").Equals(txtFromTarget.Text) And item.GetDataKeyValue("TO_TARGET").Equals(txtToTarget.Text) Then
                                ShowMessage(Translate("Từ % - đến % đã tồn tại"), NotifyType.Warning)
                                txtFromTarget.Focus()
                                Exit Sub
                            End If

                            If (Decimal.Parse(txtFromTarget.Text.Trim()) >= Decimal.Parse(item.GetDataKeyValue("FROM_TARGET")) _
                                And Decimal.Parse(txtToTarget.Text.Trim()) <= Decimal.Parse(item.GetDataKeyValue("TO_TARGET"))) _
                                Or (Decimal.Parse(txtFromTarget.Text.Trim()) <= Decimal.Parse(item.GetDataKeyValue("FROM_TARGET")) _
                                And Decimal.Parse(txtToTarget.Text.Trim()) >= Decimal.Parse(item.GetDataKeyValue("FROM_TARGET"))) _
                            Or (Decimal.Parse(txtFromTarget.Text.Trim()) <= Decimal.Parse(item.GetDataKeyValue("TO_TARGET")) _
                                And Decimal.Parse(txtToTarget.Text.Trim()) >= Decimal.Parse(item.GetDataKeyValue("TO_TARGET"))) _
                            Or (Decimal.Parse(txtFromTarget.Text.Trim()) <= Decimal.Parse(item.GetDataKeyValue("FROM_TARGET")) _
                                And Decimal.Parse(txtToTarget.Text.Trim()) >= Decimal.Parse(item.GetDataKeyValue("TO_TARGET"))) _
                            Then
                                ShowMessage(Translate("Từ % - đến % đã tồn tại"), NotifyType.Warning)
                                txtFromTarget.Focus()
                                Exit Sub
                            End If
                        Next

                        For Each item As GridDataItem In rgRankDetail.Items
                            Dim detail = New IncentiveRankDetailDTO
                            detail.ID = item.GetDataKeyValue("ID")
                            detail.FROM_TARGET = item.GetDataKeyValue("FROM_TARGET")
                            detail.TO_TARGET = item.GetDataKeyValue("TO_TARGET")
                            detail.AMOUNT = item.GetDataKeyValue("AMOUNT")
                            detail.INCENTIVE_PERCENT = item.GetDataKeyValue("INCENTIVE_PERCENT")
                            detail.INCENTIVE_AMOUNT = item.GetDataKeyValue("INCENTIVE_AMOUNT")
                            detail.REMARK = item.GetDataKeyValue("REMARK")
                            detail.ACTFLG = item.GetDataKeyValue("ACTFLG")
                            detail.ORDERS = item.GetDataKeyValue("ORDERS")
                            lstIncentiveDetail.Add(detail)
                        Next
                        Dim detailAdd = New IncentiveRankDetailDTO

                        detailAdd.FROM_TARGET = txtFromTarget.Text
                        detailAdd.TO_TARGET = txtToTarget.Text
                        detailAdd.AMOUNT = txtAmount.Text
                        detailAdd.INCENTIVE_PERCENT = cbIncentiveType_Percent.Checked
                        detailAdd.INCENTIVE_AMOUNT = cbIncentiveType_Amount.Checked
                        lstIncentiveDetail.Add(detailAdd)
                    Else
                        For Each item As GridDataItem In rgRankDetail.Items
                            Dim detail = New IncentiveRankDetailDTO
                            'If (Not String.IsNullOrEmpty(item.GetDataKeyValue("ID")) And item.GetDataKeyValue("ID").Equals(hidDetailID.Value.Trim())) Or _
                            If (item.GetDataKeyValue("FROM_TARGET") = txtFromTarget.Text And item.GetDataKeyValue("TO_TARGET") = txtToTarget.Text) Then
                                detail.ID = hidDetailID.Value
                                detail.FROM_TARGET = Decimal.Parse(txtFromTarget.Text)
                                detail.TO_TARGET = Decimal.Parse(txtToTarget.Text)
                                detail.AMOUNT = Decimal.Parse(txtAmount.Text)
                                detail.INCENTIVE_PERCENT = If(cbIncentiveType_Percent.Checked, 1, 0)
                                detail.INCENTIVE_AMOUNT = If(cbIncentiveType_Amount.Checked, 1, 0)
                            Else
                                detail.ID = item.GetDataKeyValue("ID")
                                detail.FROM_TARGET = item.GetDataKeyValue("FROM_TARGET")
                                detail.TO_TARGET = item.GetDataKeyValue("TO_TARGET")
                                detail.AMOUNT = item.GetDataKeyValue("AMOUNT")
                                detail.INCENTIVE_PERCENT = item.GetDataKeyValue("INCENTIVE_PERCENT")
                                detail.INCENTIVE_AMOUNT = item.GetDataKeyValue("INCENTIVE_AMOUNT")
                                detail.REMARK = item.GetDataKeyValue("REMARK")
                                detail.ACTFLG = item.GetDataKeyValue("ACTFLG")
                                detail.ORDERS = item.GetDataKeyValue("ORDERS")
                            End If
                            lstIncentiveDetail.Add(detail)
                        Next
                        txtFromTarget.ReadOnly = False
                        txtToTarget.ReadOnly = False
                    End If
                    ClearControlValue(txtFromTarget, txtToTarget, txtAmount, cbIncentiveType_Percent, cbIncentiveType_Amount)
                    btn.ToolTip = "SAVE"

                    rgRankDetail.Rebind()

                Case "DeleteRankDetail"
                    For Each item As GridDataItem In rgRankDetail.Items
                        Dim isExist As Boolean = False
                        For Each selected As GridDataItem In rgRankDetail.SelectedItems
                            If item.GetDataKeyValue("ID") = selected.GetDataKeyValue("ID") Then
                                isExist = True

                            ElseIf item.GetDataKeyValue("FROM_TARGET") = selected.GetDataKeyValue("FROM_TARGET") And item.GetDataKeyValue("TO_TARGET") = selected.GetDataKeyValue("TO_TARGET") Then
                                isExist = True
                                Exit For
                            End If
                        Next
                        If Not isExist Then
                            Dim detail As New IncentiveRankDetailDTO
                            detail.ID = item.GetDataKeyValue("ID")
                            detail.INCENTIVE_RANK_ID = item.GetDataKeyValue("INCENTIVE_RANK_ID")
                            detail.FROM_TARGET = item.GetDataKeyValue("FROM_TARGET")
                            detail.TO_TARGET = item.GetDataKeyValue("TO_TARGET")
                            detail.AMOUNT = item.GetDataKeyValue("AMOUNT")
                            detail.INCENTIVE_PERCENT = item.GetDataKeyValue("INCENTIVE_PERCENT")
                            detail.INCENTIVE_AMOUNT = item.GetDataKeyValue("INCENTIVE_AMOUNT")
                            lstIncentiveDetail.Add(detail)
                        End If
                    Next
                    rgRankDetail.Rebind()
                Case "RowClick"
                    Dim cmdItem As GridCommandItem = DirectCast(rgRankDetail.MasterTableView.GetItems(GridItemType.CommandItem)(0), GridCommandItem)
                    Dim btn As RadButton = DirectCast(cmdItem.FindControl("btnInsertRankDetail"), RadButton)
                    btn.ToolTip = "UPDATE"
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgRank_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRank.NeedDataSource
        Try

            CreateDataFilter()
        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgRankDetail_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRankDetail.NeedDataSource
        Try

            CreateDataIncentiveDetail(lstIncentiveDetail)
        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalCode_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Try
            Dim _validate As New IncentiveRankDTO
            Using rep As New PayrollRepository
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                    _validate.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                    _validate.SAL_RANK_ID = cboSalaryRank.SelectedValue
                    _validate.SAL_INCENTIVE_ID = cboIncentiveType.SelectedValue
                    _validate.ID = hidID.Value
                    args.IsValid = rep.ValidateIncentiveRank(_validate)
                Else
                    _validate.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                    _validate.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                    _validate.SAL_RANK_ID = cboSalaryRank.SelectedValue
                    _validate.SAL_INCENTIVE_ID = cboIncentiveType.SelectedValue
                    args.IsValid = rep.ValidateIncentiveRank(_validate)
                End If
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Protected Sub rgRank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgRank.SelectedIndexChanged
        If rgRank.SelectedItems.Count > 0 Then
            Dim slItem As GridDataItem

            slItem = rgRank.SelectedItems(0)
            hidID.Value = slItem("ID").Text

            cboSalaryGroup.SelectedValue = slItem("SAL_GROUP_ID").Text
            GetSalaryLevelCombo(If(String.IsNullOrEmpty(cboSalaryGroup.SelectedValue), 0, cboSalaryGroup.SelectedValue))
            cboSalaryLevel.SelectedValue = slItem("SAL_LEVEL_ID").Text
            GetSalaryRankCombo(If(String.IsNullOrEmpty(cboSalaryLevel.SelectedValue), 0, cboSalaryLevel.SelectedValue))
            cboSalaryRank.SelectedValue = slItem("SAL_RANK_ID").Text
            cboIncentiveType.SelectedValue = slItem("SAL_INCENTIVE_ID").Text

            rdEffectDate.SelectedDate = slItem("EFFECT_DATE").Text
            txtRemark.Text = slItem("REMARK").Text
            Dim txt As RadTextBox = DirectCast(slItem.FindControl("txtrgACTFLG"), RadTextBox)
            cbACTFLG.Checked = If(txt.ToolTip = "A", True, False)
            rntxtOrders.Text = If(String.IsNullOrEmpty(slItem("ORDERS").Text), 1, Decimal.Parse(slItem("ORDERS").Text))

            If Not String.IsNullOrEmpty(hidID.Value) Then
                FillDataDetail(Decimal.Parse(hidID.Value))
            End If

        End If
    End Sub

    Protected Sub rgRankDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgRankDetail.SelectedIndexChanged
        If rgRankDetail.SelectedItems.Count > 0 Then
            Dim slItem As GridDataItem

            slItem = rgRankDetail.SelectedItems(0)
            hidDetailID.Value = slItem("ID").Text
            Dim txt As RadTextBox = DirectCast(slItem.FindControl("txtIdTemp"), RadTextBox)
            If String.IsNullOrEmpty(txt.Text) Then
                hidDetailIDTemp.Value = Guid.NewGuid().ToString()
            Else
                hidDetailIDTemp.Value = txt.Text
            End If

            txtFromTarget.ReadOnly = True
            txtToTarget.ReadOnly = True

            txtFromTarget.Text = slItem("FROM_TARGET").Text
            txtToTarget.Text = slItem("TO_TARGET").Text

            txtAmount.Text = slItem("AMOUNT").Text
            Dim chk As RadButton = DirectCast(slItem.FindControl("chkINCENTIVE_TYPE_Percent"), RadButton)
            If chk IsNot Nothing Then
                cbIncentiveType_Percent.Checked = chk.Checked
            Else
                cbIncentiveType_Percent.Checked = False
            End If
            chk = DirectCast(slItem.FindControl("chkINCENTIVE_TYPE_Amount"), RadButton)
            If chk IsNot Nothing Then
                cbIncentiveType_Amount.Checked = chk.Checked
            Else
                cbIncentiveType_Amount.Checked = False
            End If

        End If
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'Master
                    EnabledGrid(rgRank, False)

                    Utilities.EnableRadCombo(cboSalaryGroup, True)
                    Utilities.EnableRadCombo(cboSalaryLevel, True)
                    Utilities.EnableRadCombo(cboSalaryRank, True)
                    Utilities.EnableRadCombo(cboIncentiveType, True)
                    rdEffectDate.Enabled = True
                    cbACTFLG.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtOrders.ReadOnly = False

                    cboSalaryGroup.ClearValue()
                    cboSalaryLevel.ClearValue()
                    cboSalaryRank.ClearValue()
                    cboIncentiveType.ClearValue()
                    rdEffectDate.ClearValue()
                    cbACTFLG.Checked = True
                    txtRemark.Text = String.Empty
                    rntxtOrders.Text = "1"

                    'Detail
                    EnabledGrid(rgRankDetail, True)

                    txtFromTarget.ReadOnly = False
                    txtToTarget.ReadOnly = False
                    txtAmount.ReadOnly = False
                    cbIncentiveType_Percent.ReadOnly = False
                    cbIncentiveType_Amount.ReadOnly = False

                    txtFromTarget.Text = "1"
                    txtToTarget.Text = "1"
                    txtAmount.Text = "0"
                    cbIncentiveType_Percent.Checked = True
                    cbIncentiveType_Amount.Checked = False
                    lstIncentiveDetail = New List(Of IncentiveRankDetailDTO)
                    rgRankDetail.Rebind()

                Case CommonMessage.STATE_NORMAL
                    'Master
                    EnabledGrid(rgRank, True)

                    Utilities.EnableRadCombo(cboSalaryGroup, True)
                    Utilities.EnableRadCombo(cboSalaryLevel, False)
                    Utilities.EnableRadCombo(cboSalaryRank, False)
                    Utilities.EnableRadCombo(cboIncentiveType, False)
                    rdEffectDate.Enabled = False
                    cbACTFLG.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtOrders.ReadOnly = True

                    rdEffectDate.ClearValue()
                    cbACTFLG.Checked = True
                    txtRemark.Text = String.Empty
                    rntxtOrders.Text = "1"

                    'Detail
                    EnabledGrid(rgRankDetail, False)

                    txtFromTarget.ReadOnly = True
                    txtToTarget.ReadOnly = True
                    txtAmount.ReadOnly = True
                    cbIncentiveType_Percent.ReadOnly = True
                    cbIncentiveType_Amount.ReadOnly = True

                    txtFromTarget.Text = "1"
                    txtToTarget.Text = "1"
                    txtAmount.Text = "0"
                    cbIncentiveType_Percent.Checked = True
                    cbIncentiveType_Amount.Checked = False

                Case CommonMessage.STATE_EDIT
                    'Master
                    EnabledGrid(rgRank, False)

                    Utilities.EnableRadCombo(cboSalaryGroup, False)
                    Utilities.EnableRadCombo(cboSalaryLevel, False)
                    Utilities.EnableRadCombo(cboSalaryRank, False)
                    Utilities.EnableRadCombo(cboIncentiveType, True)
                    rdEffectDate.Enabled = True
                    cbACTFLG.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtOrders.ReadOnly = True

                    'Detail
                    EnabledGrid(rgRankDetail, True)

                    txtFromTarget.ReadOnly = False
                    txtToTarget.ReadOnly = False
                    txtAmount.ReadOnly = False
                    cbIncentiveType_Percent.ReadOnly = False
                    cbIncentiveType_Amount.ReadOnly = False

                Case CommonMessage.STATE_DELETE
                    If rep.DeleteIncentiveRank(DeleteIncentives) Then
                        DeleteIncentives = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgRank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgRank.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveIncentiveRank(lstDeletes, "A", cboIncentiveType.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgRank.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgRank.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgRank.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveIncentiveRank(lstDeletes, "I", cboIncentiveType.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgRank.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetMasterData(ByVal _Filter As IncentiveRankDTO)

        Using rep As New PayrollRepository
            lstIncentives = rep.GetIncentiveRank(_Filter, 1, 1, 1)
        End Using
    End Sub

    Private Sub FillDataDetail(Optional masterId As Decimal = Nothing, Optional id As Decimal = Nothing)
        Using rep As New PayrollRepository
            Dim _filter As New IncentiveRankDetailDTO
            If Not String.IsNullOrEmpty(masterId) Then
                _filter.INCENTIVE_RANK_ID = masterId
            End If
            If Not String.IsNullOrEmpty(id) Then
                _filter.ID = id
            End If

            lstIncentiveDetail = rep.GetIncentiveRankDetail(_filter, 0, Integer.MaxValue, 0)

            If lstIncentiveDetail IsNot Nothing Then
                rgRankDetail.Rebind()
            End If


        End Using
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New IncentiveRankDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgRank, _filter)
            If cboSalaryGroup.SelectedValue <> "" Then
                _filter.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            End If
            Dim Sorts As String = rgRank.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetIncentiveRank(_filter, Sorts).ToTable
                    Else
                        Return rep.GetIncentiveRank(_filter).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.Incentives = rep.GetIncentiveRank(_filter, rgRank.CurrentPageIndex, rgRank.PageSize, MaximumRows, Sorts)
                    Else
                        Me.Incentives = rep.GetIncentiveRank(_filter, rgRank.CurrentPageIndex, rgRank.PageSize, MaximumRows)
                    End If
                    rgRank.VirtualItemCount = MaximumRows
                    rgRank.DataSource = Me.Incentives
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CreateDataIncentiveDetail(Optional ByVal lstIncentiveDetail As List(Of IncentiveRankDetailDTO) = Nothing)
        If lstIncentiveDetail Is Nothing Then
            lstIncentiveDetail = New List(Of IncentiveRankDetailDTO)
        End If
        rgRankDetail.DataSource = lstIncentiveDetail
    End Sub

    Private Sub GetIncentiveCombo()
        Using rep As New PayrollRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            comboBoxDataDTO.GET_INCENTIVE_TYPE = True
            rep.GetComboboxData(comboBoxDataDTO)
            FillDropDownList(cboIncentiveType, comboBoxDataDTO.LIST_INCENTIVE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        End Using
    End Sub

    Private Sub GetSalaryGroupCombo(ByVal dDate As Date)
        Using rep As New PayrollRepository
            Dim dt As DataTable = rep.GetSalaryGroupCombo(dDate, True)
            FillRadCombobox(cboSalaryGroup, dt, "NAME", "ID", True)
        End Using

    End Sub

    Private Sub GetSalaryLevelCombo(ByVal dValue As Decimal)
        Using rep As New PayrollRepository
            Dim dt As DataTable = rep.GetSalaryLevelCombo(dValue, True, "U")
            FillRadCombobox(cboSalaryLevel, dt, "NAME", "ID", False)
        End Using

    End Sub

    Private Sub GetSalaryRankCombo(ByVal dValue As Decimal)
        Using rep As New PayrollRepository
            Dim dt As DataTable = rep.GetSalaryRankCombo(dValue, True)
            FillRadCombobox(cboSalaryRank, dt, "NAME", "ID", False)
        End Using

    End Sub
#End Region

End Class