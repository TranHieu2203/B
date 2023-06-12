Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_SalaryType_Group
    Inherits Common.CommonView

#Region "Property"

    Public Property SalaryType_Groups As List(Of SalaryType_GroupDTO)
        Get
            Return ViewState(Me.ID & "_SalaryType_Groups")
        End Get
        Set(ByVal value As List(Of SalaryType_GroupDTO))
            ViewState(Me.ID & "_SalaryType_Groups") = value
        End Set
    End Property

    Property DeleteSalaryType_Groups As List(Of SalaryType_GroupDTO)
        Get
            Return ViewState(Me.ID & "_DeleteSalaryType_Groups")
        End Get
        Set(ByVal value As List(Of SalaryType_GroupDTO))
            ViewState(Me.ID & "_DeleteSalaryType_Groups") = value
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
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryLevels
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                GetDataToCombo()
                rgData.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable

        Dim _filter As New SalaryType_GroupDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, _filter)
            If cboSalaryType.SelectedValue <> "" Then
                _filter.SAL_TYPE_ID = cboSalaryType.SelectedValue
            End If
            If cboSalaryGroup.SelectedValue <> "" Then
                _filter.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            End If
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSalaryType_Group(_filter, 0, Integer.MaxValue, 0, Sorts).ToTable
                    Else
                        Return rep.GetSalaryType_Group(_filter, 0, Integer.MaxValue, 0).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.SalaryType_Groups = rep.GetSalaryType_Group(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        Me.SalaryType_Groups = rep.GetSalaryType_Group(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = Me.SalaryType_Groups
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim status As String = "0"
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL

                    EnabledGrid(rgData, True)

                    EnableRadCombo(cboSalaryType, True)
                    EnableRadCombo(cboSalaryGroup, False)
                    EnableControlAll(False, chkIs_Direct_Salary, txtRemark, rntxtOrders)
                    cboSalaryType.Focus()
                Case CommonMessage.STATE_NEW
                    EnabledGrid(rgData, False)

                    EnableRadCombo(cboSalaryType, True)
                    EnableRadCombo(cboSalaryGroup, True)
                    EnableControlAll(True, chkIs_Direct_Salary, txtRemark, rntxtOrders)
                    ClearControlValue(cboSalaryType, cboSalaryGroup, chkIs_Direct_Salary, txtRemark, rntxtOrders)
                    cboSalaryType.Focus()
                    If chkIs_Direct_Salary.Checked Then
                        status = "1"
                        'EnableRadCombo(cboSalaryGroup, False)
                    Else
                        status = "0"
                        'EnableRadCombo(cboSalaryGroup, True)
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", "ShowHideGroup(" + status + ");", True)
                Case CommonMessage.STATE_EDIT
                    EnabledGrid(rgData, False)
                    EnableRadCombo(cboSalaryType, False)
                    EnableControlAll(True, chkIs_Direct_Salary, txtRemark, rntxtOrders)
                    cboSalaryType.Focus()
                    If chkIs_Direct_Salary.Checked Then
                        status = "1"
                        'EnableRadCombo(cboSalaryGroup, False)
                    Else
                        status = "0"
                        'EnableRadCombo(cboSalaryGroup, True)
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", "ShowHideGroup(" + status + ");", True)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryType_GroupStatus(lstDeletes, 1) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryType_Group(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryType_Group(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryType_Group As New SalaryType_GroupDTO
        Dim gID As Decimal

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

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of SalaryType_GroupDTO)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(New SalaryType_GroupDTO With {.ID = Decimal.Parse(item("ID").Text)})
                    Next

                    DeleteSalaryType_Groups = lstDeletes
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryType_Group")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboSalaryType.SelectedValue = "" Or cboSalaryType.SelectedValue Is Nothing Then
                            ShowMessage(Translate("Bạn chưa chọn nhóm lương"), Utilities.NotifyType.Warning)
                            cboSalaryType.Focus()
                            Return
                        End If
                        If chkIs_Direct_Salary.Checked = False And String.IsNullOrEmpty(cboSalaryGroup.SelectedValue) Then
                            ShowMessage(Translate("Bạn chưa chọn bảng lương"), Utilities.NotifyType.Warning)
                            cboSalaryGroup.Focus()
                            Return
                        End If
                        If chkIs_Direct_Salary.Checked Then
                            cboSalaryGroup.ClearValue()
                        End If
                        objSalaryType_Group.SAL_TYPE_ID = cboSalaryType.SelectedValue
                        objSalaryType_Group.SAL_GROUP_ID = If(String.IsNullOrEmpty(cboSalaryGroup.SelectedValue), Nothing, Decimal.Parse(cboSalaryGroup.SelectedValue))
                        objSalaryType_Group.IS_DIRECT_SALARY = chkIs_Direct_Salary.Checked
                        objSalaryType_Group.REMARK = txtRemark.Text
                        objSalaryType_Group.ORDERS = If(rntxtOrders.Text = "", 1, Decimal.Parse(rntxtOrders.Text))

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objSalaryType_Group.ACTFLG = "A"
                                    objSalaryType_Group.IS_DELETED = 0
                                    If rep.InsertSalaryType_Group(objSalaryType_Group, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objSalaryType_Group.ID = rgData.SelectedValue
                                    If rep.ModifySalaryType_Group(objSalaryType_Group, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objSalaryType_Group.ID
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
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
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

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate

        Dim _validate As New SalaryType_GroupDTO
        Try
            Using rep As New PayrollRepository
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.SAL_TYPE_ID = cboSalaryType.SelectedValue
                    _validate.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                    _validate.IS_DIRECT_SALARY = chkIs_Direct_Salary.Checked
                    _validate.ID = rgData.SelectedValue
                    args.IsValid = rep.ValidateSalaryType_Group(_validate)

                Else
                    _validate.SAL_TYPE_ID = cboSalaryType.SelectedValue
                    _validate.SAL_GROUP_ID = If(String.IsNullOrEmpty(cboSalaryGroup.SelectedValue), 0, Decimal.Parse(cboSalaryGroup.SelectedValue))
                    _validate.IS_DIRECT_SALARY = chkIs_Direct_Salary.Checked
                    args.IsValid = rep.ValidateSalaryType_Group(_validate)
                End If

            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub chkIs_Direct_Salary_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIs_Direct_Salary.CheckedChanged
        Try
            If chkIs_Direct_Salary.Checked Then
                EnableRadCombo(cboSalaryGroup, False)
            Else
                EnableRadCombo(cboSalaryGroup, True)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim status As String = 0
        If rgData.SelectedItems.Count > 0 Then
            Dim slItem As GridDataItem

            slItem = rgData.SelectedItems(0)
            hidID.Value = slItem("ID").Text

            cboSalaryType.SelectedValue = slItem("SAL_TYPE_ID").Text
            Dim chk As RadButton = DirectCast(slItem.FindControl("chkIS_DIRECT_SALARY"), RadButton)
            chkIs_Direct_Salary.Checked = chk.Checked
            If chkIs_Direct_Salary.Checked Then
                cboSalaryGroup.ClearValue()
                status = "1"
            Else
                cboSalaryGroup.SelectedValue = slItem("SAL_GROUP_ID").Text
                status = "0"
            End If
            txtRemark.Text = slItem("REMARK").Text
            rntxtOrders.Text = If(String.IsNullOrEmpty(slItem("ORDERS").Text), 1, Decimal.Parse(slItem("ORDERS").Text))

        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", "ShowHideGroup(" + status + ");", True)
    End Sub

#End Region

#Region "Custom"
    Private Sub GetDataToCombo()
        Dim comboBoxDataDto As New ComboBoxDataDTO
        Using rep As New PayrollRepository
            comboBoxDataDto.GET_SALARY_GROUP = True
            comboBoxDataDto.GET_SALARY_TYPE = True
            rep.GetComboboxData(comboBoxDataDto)

            FillDropDownList(cboSalaryType, comboBoxDataDto.LIST_SALARY_TYPE, "NAME", "ID", Common.Common.SystemLanguage, False)
            FillDropDownList(cboSalaryGroup, comboBoxDataDto.LIST_SALARY_GROUP, "NAME", "ID", Common.Common.SystemLanguage, True)
        End Using
    End Sub
#End Region
End Class