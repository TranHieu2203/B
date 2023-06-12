Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlPA_PaymentsList
    Inherits Common.CommonView

#Region "Property"
    Public IDSelect As Integer
    Public Property vPAPaymentList As List(Of PAPaymentListDTO)
        Get
            Return ViewState(Me.ID & "_PaymentList")
        End Get
        Set(ByVal value As List(Of PAPaymentListDTO))
            ViewState(Me.ID & "_PaymentList") = value
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

    '#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgData)
        rgData.AllowCustomPaging = True
        'rgCostCenter.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Seperator,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Refresh("")
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
        Dim obj As New PAPaymentListDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPAPaymentList = rep.GetPaymentList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "CREATED_DATE desc")
            Else
                Me.vPAPaymentList = rep.GetPaymentList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.vPAPaymentList
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState

                Case CommonMessage.STATE_NORMAL
                    cboObjectPayment.Enabled = False
                    txtCode.Enabled = False
                    dpEffectiveDate.Enabled = False
                    nmValue.Enabled = False
                    txtDesc.Enabled = False
                    EnabledGridNotPostback(rgData, True)

                    ClearControlValue(cboObjectPayment, txtCode, txtName, dpEffectiveDate, nmValue, txtDesc)

                Case CommonMessage.STATE_NEW

                    cboObjectPayment.SelectedIndex = 0
                    txtCode.Enabled = True
                    dpEffectiveDate.SelectedDate = Nothing
                    nmValue.Value = 0
                    txtDesc.Text = ""

                    cboObjectPayment.Enabled = True
                    txtCode.Enabled = True
                    dpEffectiveDate.Enabled = True
                    nmValue.Enabled = True
                    txtDesc.Enabled = True
                    EnabledGridNotPostback(rgData, False)

                    ClearControlValue(cboObjectPayment, txtCode, txtName, dpEffectiveDate, nmValue, txtDesc)

                Case CommonMessage.STATE_EDIT
                    cboObjectPayment.Enabled = True
                    txtCode.Enabled = True
                    dpEffectiveDate.Enabled = True
                    nmValue.Enabled = True
                    txtDesc.Enabled = True
                    EnabledGridNotPostback(rgData, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActivePaymentList(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActivePaymentList(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeletePaymentList(lstDeletes) Then
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
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("OBJ_PAYMENT_ID", cboObjectPayment)
        dic.Add("CODE", txtCode)
        dic.Add("NAME", txtName)
        dic.Add("EFFECTIVE_DATE", dpEffectiveDate)
        dic.Add("VALUE", nmValue)
        dic.Add("SDESC", txtDesc)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

    '#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objPaymentList As New PAPaymentListDTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

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

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If cboObjectPayment.SelectedIndex < 0 Then
                        ShowMessage("Bạn chưa chọn đối tượng lương, kiểm tra lại!", NotifyType.Warning)
                        cboObjectPayment.Focus()
                        Exit Sub
                    End If                    
                    If dpEffectiveDate.SelectedDate Is Nothing Then
                        ShowMessage("Bạn chưa nhập ngày hiệu lực, kiểm tra lại!", NotifyType.Warning)
                        dpEffectiveDate.Focus()
                        Exit Sub
                    End If
                    If nmValue.Value <= 0 Then
                        ShowMessage("Bạn chưa định mức quy định phải lớn hơn 0, kiểm tra lại!", NotifyType.Warning)
                        dpEffectiveDate.Focus()
                        Exit Sub
                    End If

                    If Page.IsValid Then
                        objPaymentList.OBJ_PAYMENT_ID = cboObjectPayment.SelectedValue
                        objPaymentList.CODE = txtCode.Text
                        objPaymentList.NAME = txtName.Text
                        objPaymentList.EFFECTIVE_DATE = dpEffectiveDate.SelectedDate
                        objPaymentList.VALUE = nmValue.Value
                        objPaymentList.SDESC = txtDesc.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objPaymentList.ACTFLG = "A"
                                If rep.InsertPaymentList(objPaymentList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objPaymentList.ID = rgData.SelectedValue
                                If rep.ModifyPaymentList(objPaymentList, rgData.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objPaymentList.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            'rgData.DataSource = New DataTable()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
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

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim rep As New PayrollRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_OBJECT_PAYMENT = True
                ListComboData.GET_LIST_SALARY = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboObjectPayment, ListComboData.LIST_OBJECT_PAYMENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboObjectPayment.SelectedValue)            

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

End Class