Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlQuyDinhPhatDiMuonVeSom
    Inherits Common.CommonView

#Region "Property"

    Public IDSelect As Integer
    Public Property AT_DMVS As List(Of AT_DMVSDTO)
        Get
            Return ViewState(Me.ID & "_DMVS")
        End Get
        Set(ByVal value As List(Of AT_DMVSDTO))
            ViewState(Me.ID & "_DMVS") = value
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
            Refresh("")
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()
        Dim rep As New AttendanceRepository
        Dim obj As New AT_DMVSDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.AT_DMVS = rep.GetAT_DMVS(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
            Else
                Me.AT_DMVS = rep.GetAT_DMVS(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
            End If

            rgDanhMuc.VirtualItemCount = MaximumRows
            rgDanhMuc.DataSource = Me.AT_DMVS
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    rdTxtCode.Text = ""
                    rdTxtName.Text = ""
                    rnTxtMuc1.Text = ""
                    rcbLoaiPhat1.Text = ""
                    rnTxtGiatri1.Text = ""
                    rnTxtMuc2.Text = ""
                    rcbLoaiPhat2.Text = ""
                    rnTxtGiatri2.Text = ""
                    rnTxtMuc3.Text = ""
                    rcbLoaiPhat3.Text = ""
                    rnTxtGiatri3.Text = ""
                    rnTxtMuc4.Text = ""
                    rcbLoaiPhat4.Text = ""
                    rnTxtGiatri4.Text = ""
                    rnTxtMuc5.Text = ""
                    rcbLoaiPhat5.Text = ""
                    rnTxtGiatri5.Text = ""

                    rdTxtCode.Enabled = True
                    rdTxtName.Enabled = True
                    rnTxtMuc1.Enabled = True
                    rcbLoaiPhat1.Enabled = True
                    rnTxtGiatri1.Enabled = True
                    rnTxtMuc2.Enabled = True
                    rcbLoaiPhat2.Enabled = True
                    rnTxtGiatri2.Enabled = True
                    rnTxtMuc3.Enabled = True
                    rcbLoaiPhat3.Enabled = True
                    rnTxtGiatri3.Enabled = True
                    rnTxtMuc4.Enabled = True
                    rcbLoaiPhat4.Enabled = True
                    rnTxtGiatri4.Enabled = True
                    rnTxtMuc5.Enabled = True
                    rcbLoaiPhat5.Enabled = True
                    rnTxtGiatri5.Enabled = True
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_NORMAL

                    rdTxtCode.Enabled = False
                    rdTxtName.Enabled = False
                    rnTxtMuc1.Enabled = False
                    rcbLoaiPhat1.Enabled = False
                    rnTxtGiatri1.Enabled = False
                    rnTxtMuc2.Enabled = False
                    rcbLoaiPhat2.Enabled = False
                    rnTxtGiatri2.Enabled = False
                    rnTxtMuc3.Enabled = False
                    rcbLoaiPhat3.Enabled = False
                    rnTxtGiatri3.Enabled = False
                    rnTxtMuc4.Enabled = False
                    rcbLoaiPhat4.Enabled = False
                    rnTxtGiatri4.Enabled = False
                    rnTxtMuc5.Enabled = False
                    rcbLoaiPhat5.Enabled = False
                    rnTxtGiatri5.Enabled = False
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT

                    rdTxtCode.Enabled = True
                    rdTxtName.Enabled = True
                    rnTxtMuc1.Enabled = True
                    rcbLoaiPhat1.Enabled = True
                    rnTxtGiatri1.Enabled = True
                    rnTxtMuc2.Enabled = True
                    rcbLoaiPhat2.Enabled = True
                    rnTxtGiatri2.Enabled = True
                    rnTxtMuc3.Enabled = True
                    rcbLoaiPhat3.Enabled = True
                    rnTxtGiatri3.Enabled = True
                    rnTxtMuc4.Enabled = True
                    rcbLoaiPhat4.Enabled = True
                    rnTxtGiatri4.Enabled = True
                    rnTxtMuc5.Enabled = True
                    rcbLoaiPhat5.Enabled = True
                    rnTxtGiatri5.Enabled = True
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_DMVS(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_DMVS(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_DMVS(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rdTxtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", rdTxtCode)
        dic.Add("NAME_VN", rdTxtName)
        dic.Add("MUC1", rnTxtMuc1)
        dic.Add("LOAIPHAT1", rcbLoaiPhat1)
        dic.Add("GIATRI1", rnTxtGiatri1)
        dic.Add("MUC2", rnTxtMuc2)
        dic.Add("LOAIPHAT2", rcbLoaiPhat2)
        dic.Add("GIATRI2", rnTxtGiatri2)
        dic.Add("MUC3", rnTxtMuc3)
        dic.Add("LOAIPHAT3", rcbLoaiPhat3)
        dic.Add("GIATRI3", rnTxtGiatri3)
        dic.Add("MUC4", rnTxtMuc4)
        dic.Add("LOAIPHAT4", rcbLoaiPhat4)
        dic.Add("GIATRI4", rnTxtGiatri4)
        dic.Add("MUC5", rnTxtMuc5)
        dic.Add("LOAIPHAT5", rcbLoaiPhat5)
        dic.Add("GIATRI5", rnTxtGiatri5)
        Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objDMVS As New AT_DMVSDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If rdTxtCode.Text = "" Then
                        ShowMessage("Bạn chưa nhập mã quy định, kiểm tra lại!", NotifyType.Warning)
                        rdTxtCode.Focus()
                        Exit Sub
                    End If
                    If rdTxtName.Text = "" Then
                        ShowMessage("Bạn chưa nhập tên quy định, kiểm tra lại!", NotifyType.Warning)
                        rdTxtName.Focus()
                        Exit Sub
                    End If
                    If rnTxtMuc1.Value <> 0 Then
                        If rnTxtMuc1.Value > 480 Or rnTxtMuc1.Value < 0 Then
                            ShowMessage("Bạn nhập sai số phút quy định mức 1, kiểm tra lại!", NotifyType.Warning)
                            rnTxtMuc1.Focus()
                            Exit Sub
                        End If
                    End If
                    If rnTxtMuc2.Value <> 0 Then
                        If rnTxtMuc2.Value > 480 Or rnTxtMuc2.Value < 0 Then
                            ShowMessage("Bạn nhập sai số phút quy định mức 2, kiểm tra lại!", NotifyType.Warning)
                            rnTxtMuc2.Focus()
                            Exit Sub
                        End If
                    End If
                    If rnTxtMuc3.Value <> 0 Then
                        If rnTxtMuc3.Value > 480 Or rnTxtMuc3.Value < 0 Then
                            ShowMessage("Bạn nhập sai số phút quy định mức 3, kiểm tra lại!", NotifyType.Warning)
                            rnTxtMuc3.Focus()
                            Exit Sub
                        End If
                    End If
                    If rnTxtMuc4.Value <> 0 Then
                        If rnTxtMuc4.Value > 480 Or rnTxtMuc4.Value < 0 Then
                            ShowMessage("Bạn nhập sai số phút quy định mức 4, kiểm tra lại!", NotifyType.Warning)
                            rnTxtMuc4.Focus()
                            Exit Sub
                        End If
                    End If
                    If rnTxtMuc5.Value <> 0 Then
                        If rnTxtMuc5.Value > 480 Or rnTxtMuc5.Value < 0 Then
                            ShowMessage("Bạn nhập sai số phút quy định mức 5, kiểm tra lại!", NotifyType.Warning)
                            rnTxtMuc5.Focus()
                            Exit Sub
                        End If
                    End If
                    If Page.IsValid Then
                        objDMVS.CODE = rdTxtCode.Text
                        objDMVS.NAME_VN = rdTxtName.Text
                        If rcbLoaiPhat1.SelectedValue <> "" Then
                            objDMVS.LOAIPHAT1 = CDec(rcbLoaiPhat1.SelectedValue)
                        End If
                        objDMVS.MUC1 = rnTxtMuc1.Value
                        objDMVS.GIATRI1 = rnTxtGiatri1.Value
                        If rcbLoaiPhat2.SelectedValue <> "" Then
                            objDMVS.LOAIPHAT2 = CDec(rcbLoaiPhat2.SelectedValue)
                        End If
                        objDMVS.MUC2 = rnTxtMuc2.Value
                        objDMVS.GIATRI2 = rnTxtGiatri2.Value
                        If rcbLoaiPhat3.SelectedValue <> "" Then
                            objDMVS.LOAIPHAT3 = CDec(rcbLoaiPhat3.SelectedValue)
                        End If
                        objDMVS.MUC3 = rnTxtMuc3.Value
                        objDMVS.GIATRI3 = rnTxtGiatri3.Value
                        If rcbLoaiPhat4.SelectedValue <> "" Then
                            objDMVS.LOAIPHAT4 = CDec(rcbLoaiPhat4.SelectedValue)
                        End If
                        objDMVS.MUC4 = rnTxtMuc4.Value
                        objDMVS.GIATRI4 = rnTxtGiatri4.Value
                        If rcbLoaiPhat5.SelectedValue <> "" Then
                            objDMVS.LOAIPHAT5 = CDec(rcbLoaiPhat5.SelectedValue)
                        End If
                        objDMVS.MUC5 = rnTxtMuc5.Value
                        objDMVS.GIATRI5 = rnTxtGiatri5.Value


                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objDMVS.ACTFLG = "A"
                                If rep.InsertAT_DMVS(objDMVS, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objDMVS.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyAT_DMVS(objDMVS, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objDMVS.ID
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

    'Function GetTable() As DataTable
    '    Dim table As New DataTable
    '    ' Create four typed columns in the DataTable.
    '    table.Columns.Add("ID", GetType(Integer))
    '    table.Columns.Add("MAQUYDINH", GetType(String))
    '    table.Columns.Add("TEN_QUYDINH", GetType(String))
    '    table.Columns.Add("MUC", GetType(Decimal))
    '    table.Columns.Add("SO_TIENPHAT", GetType(Decimal))
    '    table.Columns.Add("PHAT_NGAYCONG", GetType(Double))
    '    table.Columns.Add("STATUS", GetType(Boolean))

    '    ' Add five rows with those columns filled in the DataTable.
    '    table.Rows.Add(1, "QD01", "Quy định 01", 5, 500000, 0, True)
    '    table.Rows.Add(2, "QD02", "Quy định 02", 10, 0, 0.25, True)
    '    table.Rows.Add(3, "QD03", "Quy định 03", 15, 500000, 0, True)
    '    Return table
    'End Function

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            CreateDataFilter()
            'rgDanhMuc.DataSource = GetTable()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_DMVSDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = rdTxtCode.Text.Trim
                args.IsValid = rep.ValidateAT_DMVS(_validate)
            Else
                _validate.CODE = rdTxtCode.Text.Trim
                args.IsValid = rep.ValidateAT_DMVS(_validate)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_TYPEPUNISH = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(rcbLoaiPhat1, ListComboData.LIST_LIST_TYPEPUNISH, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcbLoaiPhat1.SelectedValue)
            FillDropDownList(rcbLoaiPhat2, ListComboData.LIST_LIST_TYPEPUNISH, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcbLoaiPhat2.SelectedValue)
            FillDropDownList(rcbLoaiPhat3, ListComboData.LIST_LIST_TYPEPUNISH, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcbLoaiPhat3.SelectedValue)
            FillDropDownList(rcbLoaiPhat4, ListComboData.LIST_LIST_TYPEPUNISH, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcbLoaiPhat4.SelectedValue)
            FillDropDownList(rcbLoaiPhat5, ListComboData.LIST_LIST_TYPEPUNISH, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcbLoaiPhat5.SelectedValue)
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