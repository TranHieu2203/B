Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlThietLapKieuCong
    Inherits Common.CommonView

#Region "Property"

    '    Public Property CostCenters As List(Of CostCenterDTO)
    '        Get
    '            Return ViewState(Me.ID & "_CostCenters")
    '        End Get
    '        Set(ByVal value As List(Of CostCenterDTO))
    '            ViewState(Me.ID & "_CostCenters") = value
    '        End Set
    '    End Property

    '    Property ActiveCostCenters As List(Of CostCenterDTO)
    '        Get
    '            Return ViewState(Me.ID & "_ActiveCostCenters")
    '        End Get
    '        Set(ByVal value As List(Of CostCenterDTO))
    '            ViewState(Me.ID & "_ActiveCostCenters") = value
    '        End Set
    '    End Property

    '    Property DeleteCostCenters As List(Of CostCenterDTO)
    '        Get
    '            Return ViewState(Me.ID & "_DeleteCostCenters")
    '        End Get
    '        Set(ByVal value As List(Of CostCenterDTO))
    '            ViewState(Me.ID & "_DeleteCostCenters") = value
    '        End Set
    '    End Property

    '    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
    '        Try
    '            Refresh()
    '            UpdateControlState()
    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try
    '    End Sub

    '    Property IDSelect As Decimal
    '        Get
    '            Return ViewState(Me.ID & "_IDSelect")
    '        End Get
    '        Set(ByVal value As Decimal)
    '            ViewState(Me.ID & "_IDSelect") = value
    '        End Set
    '    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgDanhMuc)
        rgDanhMuc.AllowCustomPaging = True
        'rgCostCenter.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                        ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
    '        Dim rep As New ProfileRepository
    '        Try
    '            If Not IsPostBack Then
    '                rgCostCenter.Rebind()
    '                CurrentState = CommonMessage.STATE_NORMAL
    '            Else

    '                Select Case Message
    '                    Case "UpdateView"
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
    '                        rgCostCenter.Rebind()
    '                        SelectedItemDataGridByKey(rgCostCenter, IDSelect, , rgCostCenter.CurrentPageIndex)

    '                        CurrentState = CommonMessage.STATE_NORMAL
    '                    Case "InsertView"
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
    '                        rgCostCenter.CurrentPageIndex = 0
    '                        rgCostCenter.MasterTableView.SortExpressions.Clear()
    '                        rgCostCenter.Rebind()
    '                        SelectedItemDataGridByKey(rgCostCenter, IDSelect, )
    '                    Case "Cancel"
    '                        rgCostCenter.MasterTableView.ClearSelectedItems()
    '                End Select
    '            End If

    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try
    '    End Sub

    'Protected Sub CreateDataFilter()
    '    Dim rep As New ProfileRepository
    '    Dim obj As New CostCenterDTO
    '    Try
    '        Dim MaximumRows As Integer
    '        SetValueObjectByRadGrid(rgDanhMuc, obj)
    '        Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
    '        If Sorts IsNot Nothing Then
    '            Me.rgDanhMuc = rep.GetCostCenter(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, Sorts)
    '        Else
    '            Me.rgDanhMuc = rep.GetCostCenter(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
    '        End If

    '        rgDanhMuc.VirtualItemCount = MaximumRows
    '        rgDanhMuc.DataSource = Me.rgDanhMuc
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    '    Public Overrides Sub UpdateControlState()
    '        Dim rep As New ProfileRepository
    '        Try
    '            Select Case CurrentState
    '                Case CommonMessage.STATE_NEW
    '                    If rgCostCenter.SelectedValue IsNot Nothing Then
    '                        Dim item = (From p In CostCenters Where p.ID = rgCostCenter.SelectedValue).SingleOrDefault
    '                        txtCode.Text = item.CODE
    '                        txtName.Text = item.NAME
    '                    End If
    '                    EnabledGridNotPostback(rgCostCenter, False)
    '                    txtCode.Text = ""
    '                    txtName.Text = ""
    '                    txtCode.ReadOnly = False
    '                    txtName.ReadOnly = False

    '                Case CommonMessage.STATE_NORMAL
    '                    EnabledGridNotPostback(rgCostCenter, True)
    '                    txtCode.ReadOnly = True
    '                    txtName.ReadOnly = True
    '                    txtCode.Text = ""
    '                    txtName.Text = ""
    '                Case CommonMessage.STATE_EDIT

    '                    EnabledGridNotPostback(rgCostCenter, False)
    '                    txtCode.ReadOnly = False
    '                    txtName.ReadOnly = False
    '                Case CommonMessage.STATE_DEACTIVE
    '                    If rep.ActiveCostCenter(ActiveCostCenters, "I") Then
    '                        ActiveCostCenters = Nothing
    '                        Refresh("UpdateView")
    '                        UpdateControlState()
    '                    Else
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
    '                        CurrentState = CommonMessage.STATE_NORMAL
    '                        UpdateControlState()
    '                    End If
    '                Case CommonMessage.STATE_ACTIVE
    '                    If rep.ActiveCostCenter(ActiveCostCenters, "A") Then
    '                        ActiveCostCenters = Nothing
    '                        Refresh("UpdateView")
    '                        UpdateControlState()
    '                    Else
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
    '                        CurrentState = CommonMessage.STATE_NORMAL
    '                        UpdateControlState()
    '                    End If

    '                Case CommonMessage.STATE_DELETE
    '                    If rep.DeleteCostCenter(DeleteCostCenters) Then
    '                        DeleteCostCenters = Nothing
    '                        Refresh("UpdateView")
    '                        UpdateControlState()
    '                    Else
    '                        CurrentState = CommonMessage.STATE_NORMAL
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
    '                        UpdateControlState()
    '                    End If
    '            End Select
    '            txtCode.Focus()
    '            UpdateToolbarState()
    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try

    '    End Sub

    '    Public Overrides Sub BindData()
    '        Dim dic As New Dictionary(Of String, Control)
    '        dic.Add("CODE", txtCode)
    '        dic.Add("NAME", txtName)
    '        Utilities.OnClientRowSelectedChanged(rgCostCenter, dic)
    '    End Sub

#End Region

#Region "Event"
    '    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
    '        Dim objCostCenter As New CostCenterDTO
    '        Dim rep As New ProfileRepository
    '        Dim gID As Decimal
    '        Try
    '            Select Case CType(e.Item, RadToolBarButton).CommandName
    '                Case CommonMessage.TOOLBARITEM_CREATE
    '                    CurrentState = CommonMessage.STATE_NEW

    '                    UpdateControlState()
    '                Case CommonMessage.TOOLBARITEM_EDIT
    '                    If rgCostCenter.SelectedItems.Count = 0 Then
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
    '                        Exit Sub
    '                    End If
    '                    If rgCostCenter.SelectedItems.Count > 1 Then
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
    '                        Exit Sub
    '                    End If
    '                    CurrentState = CommonMessage.STATE_EDIT

    '                    UpdateControlState()
    '                Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
    '                    Dim lstDeleteCostCenters As New List(Of CostCenterDTO)
    '                    Dim sActive As String
    '                    Dim bCheck As Boolean = True
    '                    For idx = 0 To rgCostCenter.SelectedItems.Count - 1
    '                        Dim item As GridDataItem = rgCostCenter.SelectedItems(idx)

    '                        If sActive Is Nothing Then
    '                            sActive = item("ACTFLG").Text
    '                        ElseIf sActive <> item("ACTFLG").Text Then
    '                            bCheck = False
    '                            Exit For
    '                        End If
    '                        lstDeleteCostCenters.Add(New CostCenterDTO With {.ID = Decimal.Parse(item("ID").Text),
    '                                                                         .ACTFLG = sActive})
    '                    Next
    '                    If Not bCheck Then
    '                        ShowMessage("Các bản ghi không cùng 1 trạng thái, kiểm tra lại!", NotifyType.Warning)
    '                        Exit Sub
    '                    End If

    '                    ActiveCostCenters = lstDeleteCostCenters
    '                    If ActiveCostCenters.Count > 0 Then
    '                        If sActive = "A" Then
    '                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_ACTIVE Then
    '                                ShowMessage("Các bản ghi đã ở trạng thái áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
    '                                Exit Sub
    '                            End If
    '                        Else
    '                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_DEACTIVE Then
    '                                ShowMessage("Các bản ghi đã ở trạng thái ngưng áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
    '                                Exit Sub
    '                            End If
    '                        End If
    '                        If sActive = "A" Then
    '                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)

    '                        Else
    '                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)

    '                        End If
    '                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
    '                        ctrlMessageBox.DataBind()
    '                        ctrlMessageBox.Show()
    '                    Else
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
    '                    End If

    '                Case CommonMessage.TOOLBARITEM_DELETE

    '                    Dim lstDeletes As New List(Of CostCenterDTO)
    '                    For idx = 0 To rgCostCenter.SelectedItems.Count - 1
    '                        Dim item As GridDataItem = rgCostCenter.SelectedItems(idx)
    '                        lstDeletes.Add(New CostCenterDTO With {.ID = Decimal.Parse(item("ID").Text)})
    '                    Next

    '                    DeleteCostCenters = lstDeletes
    '                    If DeleteCostCenters.Count > 0 Then
    '                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
    '                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
    '                        ctrlMessageBox.DataBind()
    '                        ctrlMessageBox.Show()
    '                    Else
    '                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
    '                    End If
    '                Case CommonMessage.TOOLBARITEM_EXPORT
    '                    GridExportExcel(rgCostCenter, "CostCenter")
    '                Case CommonMessage.TOOLBARITEM_SAVE
    '                    If Page.IsValid Then
    '                        objCostCenter.CODE = txtCode.Text
    '                        objCostCenter.NAME = txtName.Text
    '                        Select Case CurrentState
    '                            Case CommonMessage.STATE_NEW
    '                                objCostCenter.ACTFLG = "A"
    '                                If rep.InsertCostCenter(objCostCenter, gID) Then
    '                                    CurrentState = CommonMessage.STATE_NEW
    '                                    IDSelect = gID
    '                                    Refresh("InsertView")
    '                                    UpdateControlState()
    '                                Else
    '                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
    '                                End If
    '                            Case CommonMessage.STATE_EDIT
    '                                objCostCenter.ID = rgCostCenter.SelectedValue
    '                                If rep.ModifyCostCenter(objCostCenter, gID) Then
    '                                    CurrentState = CommonMessage.STATE_NORMAL
    '                                    IDSelect = objCostCenter.ID
    '                                    Refresh("UpdateView")
    '                                    UpdateControlState()
    '                                Else
    '                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
    '                                End If
    '                        End Select
    '                    Else
    '                        ExcuteScript("Resize", "ResizeSplitter()")
    '                    End If

    '                Case CommonMessage.TOOLBARITEM_CANCEL
    '                    CurrentState = CommonMessage.STATE_NORMAL
    '                    Refresh("Cancel")
    '                    UpdateControlState()
    '            End Select

    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try
    '    End Sub


    '    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
    '        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
    '            If ActiveCostCenters(0).ACTFLG = "A" Then
    '                CurrentState = CommonMessage.STATE_DEACTIVE
    '            Else
    '                CurrentState = CommonMessage.STATE_ACTIVE
    '            End If
    '            UpdateControlState()
    '        End If
    '        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
    '            CurrentState = CommonMessage.STATE_DELETE
    '            UpdateControlState()
    '        End If
    '    End Sub

    Function GetTable() As DataTable
        ' Create new DataTable instance.
        Dim table As New DataTable

        ' Create four typed columns in the DataTable.
        table.Columns.Add("ID", GetType(Integer))
        table.Columns.Add("MA_KIEUCONG", GetType(String))
        table.Columns.Add("NAME_VN", GetType(String))
        table.Columns.Add("NAME_EN", GetType(String))
        table.Columns.Add("CONG_BUOISANG", GetType(String))
        table.Columns.Add("CONG_BUOICHIEU", GetType(String))
        table.Columns.Add("STATUS", GetType(Boolean))
        table.Columns.Add("GHICHU", GetType(String))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add(1, "MK01", "Kiểu công 1", "Kiểu công 1", "Kiểu công buổi sáng", "Kiểu công buổi chiều", True, "Ghi chú 1")
        table.Rows.Add(2, "MK02", "Kiểu công 2", "Kiểu công 2", "Kiểu công buổi sáng", "Kiểu công buổi chiều", False, "Ghi chú 1")
        table.Rows.Add(3, "MK03", "Kiểu công 3", "Kiểu công 3", "Kiểu công buổi sáng", "Kiểu công buổi chiều", True, "Ghi chú 1")
        Return table
    End Function

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            'CreateDataFilter()
            rgDanhMuc.DataSource = GetTable()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

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

#End Region

#Region "Custom"
    '    Private Sub UpdateToolbarState()
    '        Try
    '            ChangeToolbarState()
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
#End Region

End Class