Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlPA_PriceLunch
    Inherits Common.CommonView
    'Protected WithEvents TerminateView As ViewBase
#Region "Property"

    Public Property vPeriod As List(Of ATPriceLunchDTO)
        Get
            Return ViewState(Me.ID & "_Period")
        End Get
        Set(ByVal value As List(Of ATPriceLunchDTO))
            ViewState(Me.ID & "_Period") = value
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

    'Property IsLoad As Boolean
    '    Get
    '        Return ViewState(Me.ID & "_IsLoad")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState(Me.ID & "_IsLoad") = value
    '    End Set
    'End Property

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
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            rgData.ClientSettings.EnablePostBackOnRowClick = True
            SetGridFilter(rgData)
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("PERIOD_NAME", txtRemark)
        dic.Add("EFFECT_DATE", dpEffectDate)
        dic.Add("EXPIRE_DATE", dpExpireDate)
        dic.Add("PRICE", nmrPrice)
        dic.Add("REMARK", txtRemark)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Try

            UpdateCotrolEnabled(False)
            EnabledGridNotPostback(rgData, True)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    UpdateCotrolEnabled(False)
                    ClearControlValue(txtRemark, nmrPrice, dpEffectDate, dpExpireDate)
                Case CommonMessage.STATE_NEW
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
                    ClearControlValue(txtRemark, nmrPrice, dpEffectDate, dpExpireDate)
                Case CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Try
            ctrlOrg.Enabled = bCheck
            txtRemark.Enabled = bCheck
            EnableRadDatePicker(dpEffectDate, bCheck)
            EnableRadDatePicker(dpExpireDate, bCheck)
            nmrPrice.Enabled = bCheck
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                rgData.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "U"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "N"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                    Case "C"
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
        Dim objPeriod As New ATPriceLunchDTO
        Dim objOrgPeriod As New List(Of PA_ORG_LUNCH)
        Dim rep As New PayrollRepository
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
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        objPeriod.EFFECT_DATE = dpEffectDate.SelectedDate
                        objPeriod.EXPIRE_DATE = dpExpireDate.SelectedDate
                        objPeriod.PRICE = nmrPrice.Value
                        objPeriod.REMARK = txtRemark.Text

                        For Each item As Decimal In ctrlOrg.CheckedValueKeys
                            Dim objOrg As New PA_ORG_LUNCH
                            objOrg.ORG_ID = item
                            objOrgPeriod.Add(objOrg)
                        Next
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertPriceLunch(objPeriod, objOrgPeriod, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("N")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objPeriod.ID = rgData.SelectedValue
                                If rep.ModifyPriceLunch(objPeriod, objOrgPeriod, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objPeriod.ID
                                    Refresh("U")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New PayrollRepository
        Dim objDelete As ATPriceLunchDTO
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                objDelete = New ATPriceLunchDTO
                objDelete.ID = Utilities.ObjToDecima(rgData.SelectedValue)
                If Not rep.ValidateATPriceLunch(objDelete) Then
                    ShowMessage("Đơn giá tiền ăn đã được sử dụng ở các phòng ban. Bạn không thể xóa!", NotifyType.Warning)
                    Exit Sub
                Else
                    If rep.DeletePriceLunch(objDelete) Then
                        objDelete = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End If

            End If
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Sub CreatDataTemp()
        Dim dt As New DataTable()
        dt.Columns.Add("ID")
        dt.Columns.Add("EFFECT_DATE", GetType(Date))
        dt.Columns.Add("EXPIRE_DATE", GetType(Date))
        dt.Columns.Add("PRICE")

        dt.Rows.Add(1, New Date(2016, 1, 1), New Date(2016, 1, 31), 25)
        dt.Rows.Add(1, New Date(2016, 2, 1), New Date(2016, 2, 29), 22)
        dt.Rows.Add(1, New Date(2016, 3, 1), New Date(2016, 3, 31), 25)

        rgData.VirtualItemCount = 3
        rgData.DataSource = dt
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            'CreatDataTemp()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalEffedate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalEffedate.ServerValidate
        Dim rep As New PayrollRepository
        Dim _validate As New ATPriceLunchDTO
        Try
            _validate.ID = rgData.SelectedValue
            _validate.EFFECT_DATE = dpEffectDate.SelectedDate
            _validate.EXPIRE_DATE = dpExpireDate.SelectedDate
            If CurrentState = CommonMessage.STATE_EDIT Then
                args.IsValid = rep.ValidateATPriceLunchOrg(_validate)
            Else
                args.IsValid = rep.ValidateATPriceLunchOrg(_validate)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub CreateDataFilter()
        Dim _filter As New ATPriceLunchDTO
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPeriod = rep.GetPriceLunchList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                Me.vPeriod = rep.GetPriceLunchList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.vPeriod
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class