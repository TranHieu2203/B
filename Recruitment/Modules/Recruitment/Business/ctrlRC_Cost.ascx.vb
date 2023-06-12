Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_Cost
    Inherits Common.CommonView
    Protected WithEvents ExamsDtlView As ViewBase

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
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

    Public Overrides Sub BindData()
        Try
            Using rep As New RecruitmentRepository
                Dim dtData = rep.GetOtherList("RC_FORM", False)
                FillCheckBoxList(lstbForm, dtData, "NAME", "ID")
            End Using
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("COST_ACTUAL", rntxtCostActual)
            dic.Add("COST_EXPECTED", rntxtCostExpected)
            dic.Add("COST_DESCRIPTION", txtCostDescription)
            dic.Add("RC_FORM_NAMES", txtCostDescription)
            dic.Add("REMARK", txtRemark)
            dic.Add("RC_PROGRAM_ID", cboProgramName)
            dic.Add("RC_FORM_IDS", lstbForm)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    rntxtCostActual.Enabled = False
                    rntxtCostExpected.Enabled = False
                    txtCostDescription.Enabled = False
                    txtRemark.Enabled = False
                    EnableRadCombo(cboProgramName, False)
                    lstbForm.Enabled = False
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableRadCombo(cboProgramName, True)
                    rntxtCostActual.Enabled = True
                    rntxtCostExpected.Enabled = True
                    txtCostDescription.Enabled = True
                    txtRemark.Enabled = True
                    ctrlOrg.Enabled = False
                    lstbForm.Enabled = True
                Case CommonMessage.STATE_DELETE
                    Dim objDelete As CostDTO
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    objDelete = New CostDTO With {.ID = item.GetDataKeyValue("ID")}
                    If rep.DeleteCost(objDelete) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
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
        Dim rep As New RecruitmentRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    rntxtCostActual.Value = Nothing
                    rntxtCostExpected.Value = Nothing
                    txtCostDescription.Text = ""
                    txtRemark.Text = ""
                    cboProgramName.ClearSelection()
                    cboProgramName.Text = ""
                    hidID.Value = ""
                    lstbForm.ClearChecked()
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New CostDTO
                        If hidID.Value <> "" Then
                            obj.ID = Decimal.Parse(hidID.Value)
                        End If
                        obj.ORG_ID = hidOrgID.Value
                        obj.COST_ACTUAL = rntxtCostActual.Value
                        obj.COST_DESCRIPTION = txtCostDescription.Text
                        obj.COST_EXPECTED = rntxtCostExpected.Value
                        obj.REMARK = txtRemark.Text
                        obj.RC_PROGRAM_ID = cboProgramName.SelectedValue
                        obj.RC_FORMs = (From item In lstbForm.CheckedItems Select New CostFormDTO With {.RC_FORM_ID = item.Value}).ToList()
                        If lstbForm.CheckedItems.Count > 0 Then
                            obj.RC_FORM_NAMES = lstbForm.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                        End If
                        If rep.UpdateCost(obj) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    rntxtCostActual.Value = Nothing
                    rntxtCostExpected.Value = Nothing
                    txtCostDescription.Text = ""
                    txtRemark.Text = ""
                    cboProgramName.ClearSelection()
                    cboProgramName.Text = ""
                    hidID.Value = ""
                    lstbForm.ClearChecked()
            End Select

            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Dim dtData As DataTable
        Try
            Using rep As New RecruitmentRepository
                hidOrgID.Value = ""
                cboProgramName.Items.Clear()
                cboProgramName.ClearSelection()
                cboProgramName.Text = ""
                If ctrlOrg.CurrentValue <> "" Then
                    hidOrgID.Value = ctrlOrg.CurrentValue
                    dtData = rep.GetProgramList(Decimal.Parse(ctrlOrg.CurrentValue), False)
                    FillRadCombobox(cboProgramName, dtData, "NAME", "ID")
                End If
                rgData.Rebind()
            End Using
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


    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New RecruitmentRepository
        Dim _validate As New CostDTO
        Try
            If cboProgramName.SelectedValue <> "" Then
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.ID = rgData.SelectedValue
                    _validate.RC_PROGRAM_ID = cboProgramName.SelectedValue
                    args.IsValid = rep.ValidateCost(_validate)
                Else
                    _validate.RC_PROGRAM_ID = cboProgramName.SelectedValue
                    args.IsValid = rep.ValidateCost(_validate)
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New CostDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue <> "" Then
                _filter.ORG_ID = ctrlOrg.CurrentValue
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of CostDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetCost(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetCost(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class