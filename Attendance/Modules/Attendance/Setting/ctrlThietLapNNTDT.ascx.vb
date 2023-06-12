Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlThietLapNNTDT
    Inherits Common.CommonView

#Region "Property"
    Public IDSelect As Integer
    Public Property HolidayObject As List(Of AT_HOLIDAY_OBJECTDTO)
        Get
            Return ViewState(Me.ID & "_HolidayObject")
        End Get
        Set(ByVal value As List(Of AT_HOLIDAY_OBJECTDTO))
            ViewState(Me.ID & "_HolidayObject") = value
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
                                       ToolbarItem.Edit, ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                        ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True

            'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_HOLIDAY_OBJECTDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.HolidayObject = rep.GetAT_Holiday_Object(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.HolidayObject = rep.GetAT_Holiday_Object(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.HolidayObject
            Else
                Return rep.GetAT_Holiday_Object(obj).ToTable
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ClearControlValue(txtSalaryContract, txtSalaryBase, rdDate, rdNote)
                    EnableControlAll(True, cboObjcet, cboType, txtSalaryContract, txtSalaryBase, rdDate, rdNote)
                    cboObjcet.SelectedIndex = 0
                    cboType.SelectedIndex = 0
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, cboObjcet, cboType, txtSalaryContract, txtSalaryBase, rdDate, rdNote)
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, cboObjcet, cboType, txtSalaryContract, txtSalaryBase, rdDate, rdNote)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_Holiday_Object(lstDeletes, "I") Then
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
                    If rep.ActiveAT_Holiday_Object(lstDeletes, "A") Then
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
                    If rep.DeleteAT_Holiday_Object(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            cboObjcet.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("EMP_OBJECT", cboObjcet)
        dic.Add("TYPE_SHIT", cboType)
        dic.Add("SALARIED_DATES", txtSalaryContract)
        dic.Add("SALARIED_DATES_CB", txtSalaryBase)
        dic.Add("EFFECT_DATE", rdDate)
        dic.Add("NOTE", rdNote)
        Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
    End Sub

#End Region

#Region "Event"
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_TYPEE_FML = True
                ListComboData.GET_LIST_TYPEEMPLOYEE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboObjcet, ListComboData.LIST_LIST_TYPEEMPLOYEE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboObjcet.SelectedValue)
            FillDropDownList(cboType, ListComboData.LIST_LIST_TYPE_FML, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboType.SelectedValue)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objHolidayObj As New AT_HOLIDAY_OBJECTDTO
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
                    If cboObjcet.SelectedValue = Nothing Then
                        ShowMessage("Bạn chưa chọn đối tượng công, kiểm tra lại!", NotifyType.Warning)
                        cboObjcet.Focus()
                        Exit Sub
                    End If
                    If cboType.SelectedValue = Nothing Then
                        ShowMessage("Bạn chưa chọn kiểu công, kiểm tra lại!", NotifyType.Warning)
                        cboType.Focus()
                        Exit Sub
                    End If
                    If txtSalaryContract.Value Is Nothing Then
                        ShowMessage("Bạn chưa nhập số ngày hưởng lương theo hợp đồng, kiểm tra lại!", NotifyType.Warning)
                        txtSalaryContract.Focus()
                        Exit Sub
                    End If
                    If txtSalaryBase.Value Is Nothing Then
                        ShowMessage("Bạn chưa nhập số ngày hưởng lương cơ bản, kiểm tra lại!", NotifyType.Warning)
                        txtSalaryBase.Focus()
                        Exit Sub
                    End If
                    If rdDate.SelectedDate Is Nothing Then
                        ShowMessage("Bạn chưa nhập ngày hiệu lực, kiểm tra lại!", NotifyType.Warning)
                        rdDate.Focus()
                        Exit Sub
                    End If

                    If Page.IsValid Then
                        objHolidayObj.EMP_OBJECT = cboObjcet.SelectedValue
                        objHolidayObj.TYPE_SHIT = cboType.SelectedValue
                        objHolidayObj.SALARIED_DATES = txtSalaryContract.Value
                        objHolidayObj.SALARIED_DATES_CB = txtSalaryBase.Value
                        objHolidayObj.EFFECT_DATE = rdDate.SelectedDate
                        objHolidayObj.NOTE = rdNote.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objHolidayObj.ACTFLG = "A"
                                If rep.InsertAT_Holiday_Object(objHolidayObj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objHolidayObj.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyAT_Holiday_Object(objHolidayObj, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objHolidayObj.ID
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "INSCHANGE")
                        End If
                    End Using
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

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            CreateDataFilter()
            'rgDanhMuc.DataSource = GetTable()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    'Dim rep As New ProfileRepository
    'Dim _validate As New CostCenterDTO
    'Try
    '    If CurrentState = CommonMessage.STATE_EDIT Then
    '        _validate.ID = rgCostCenter.SelectedValue
    '        _validate.CODE = txtCode.Text.Trim
    '        args.IsValid = rep.ValidateCostCenter(_validate)
    '    Else
    '        _validate.CODE = txtCode.Text.Trim
    '        args.IsValid = rep.ValidateCostCenter(_validate)
    '    End If

    'Catch ex As Exception
    '    DisplayException(Me.ViewName, Me.ID, ex)
    'End Try
    'End Sub

#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class