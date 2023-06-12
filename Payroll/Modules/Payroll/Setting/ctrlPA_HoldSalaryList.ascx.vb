Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_HoldSalaryList
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public IDSelect As Integer
    Public Property vPAHoldSalary As List(Of PAHoldSalaryDTO)
        Get
            Return ViewState(Me.ID & "_HoldSalary")
        End Get
        Set(ByVal value As List(Of PAHoldSalaryDTO))
            ViewState(Me.ID & "_HoldSalary") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgData)
        rgData.AllowCustomPaging = True
        rgData.PageSize = Common.Common.DefaultPageSize
        'rgCostCenter.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Delete, ToolbarItem.Seperator)

            Refresh()
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
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
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()
        Dim rep As New PayrollRepository
        Dim obj As New PAHoldSalaryDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPAHoldSalary = rep.GetHoldSalaryList(Utilities.ObjToDecima(rcboPeriod.SelectedValue, 0), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                Me.vPAHoldSalary = rep.GetHoldSalaryList(Utilities.ObjToDecima(rcboPeriod.SelectedValue, 0), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.vPAHoldSalary
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState

                Case CommonMessage.STATE_NEW
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstDeletes.Add(item.GetDataKeyValue("ID"))
                    'Next
                    For Each i As GridDataItem In rgData.SelectedItems
                        lstDeletes.Add(i.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteHoldSalary(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        rnmYear.Text = Date.Now.Year
        rnmYear_TextChanged(Nothing, Nothing)
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlFindEmployeePopup_CancelClicked(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        CurrentState = CommonMessage.STATE_NORMAL
        UpdateToolbarState()
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New PayrollRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim lstPA_HoldSalary = New List(Of PAHoldSalaryDTO)
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New PAHoldSalaryDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.PERIOD_ID = Utilities.ObjToInt(rcboPeriod.SelectedValue)
                    lstPA_HoldSalary.Add(item)
                Next
                If rep.InsertHoldSalary(lstPA_HoldSalary) Then
                    rgData.Rebind()
                End If
            End If
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(sender As Object, e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        rgData.Rebind()
    End Sub

    Private Sub rcboPeriod_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcboPeriod.SelectedIndexChanged
        rgData.Rebind()
    End Sub

    Private Sub rnmYear_TextChanged(sender As Object, e As System.EventArgs) Handles rnmYear.TextChanged
        Dim rep As New PayrollRepository
        Try
            If rnmYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                rcboPeriod.DataSource = rep.GetPeriodbyYear(rnmYear.Value)
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                rcboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objHoldSalary As New PAHoldSalaryDTO
        Dim rep As New PayrollRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If Utilities.ObjToInt(rcboPeriod.SelectedValue) > 0 Then
                        CurrentState = CommonMessage.STATE_NEW
                        UpdateControlState()
                        ctrlFindEmployeePopup.Show()
                    Else
                        ShowMessage(Translate("Bạn chưa chọn kỳ lương. Kiểm tra lại."), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            CreateDataFilter()
        Catch ex As Exception
            CurrentState = CommonMessage.STATE_NORMAL
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
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
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Private Sub GetDataCombo()
    '    Dim rep As New PayrollRepository
    '    Try
    '        If ListComboData Is Nothing Then
    '            ListComboData = New ComboBoxDataDTO
    '            ListComboData.GET_LIST_RESIDENT = True
    '            rep.GetComboboxData(ListComboData)
    '        End If
    '       FillDropDownList(rcboResident, ListComboData.LIST_LIST_RESIDENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboResident.SelectedValue)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub    
    '    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    '        Dim rep As New ProfileRepository
    '        Dim _validate As New CostCenterDTO
    '        Try
    '            If CurrentState = CommonMessage.STATE_EDIT Then
    '                _validate.ID = rgCostCenter.SelectedValue
    '                _validate.CODE = txtCode.Text.Trim
    '                args.IsValid = rep.ValidateCostCenter(_validate)
    '            Else
    '                _validate.CODE = txtCode.Text.Trim
    '                args.IsValid = rep.ValidateCostCenter(_validate)
    '            End If

    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try
    '    End Sub

    '#End Region

#End Region

End Class