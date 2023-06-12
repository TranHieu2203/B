Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPA_TaxationList
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Public IDSelect As Integer
    Public Property vPATaxation As List(Of PATaxationDTO)
        Get
            Return ViewState(Me.ID & "_Taxation")
        End Get
        Set(ByVal value As List(Of PATaxationDTO))
            ViewState(Me.ID & "_Taxation") = value
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
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgCostCenter.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            Refresh()
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim obj As New PATaxationDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPATaxation = rep.GetTaxation(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                Me.vPATaxation = rep.GetTaxation(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.vPATaxation
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    Public Sub StateValidate(ByVal state As Boolean)
        RequiredFieldValidator1.Visible = state
        RequiredFieldValidator2.Visible = state
        RequiredFieldValidator3.Visible = state
        RequiredFieldValidator4.Visible = state
        RequiredFieldValidator5.Visible = state
        RequiredFieldValidator6.Visible = state
    End Sub
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            Select Case CurrentState

                Case CommonMessage.STATE_NORMAL
                    StateValidate(False)
                    rcboResident.Enabled = False
                    rnmValueFrom.Enabled = False
                    rnmValueTo.Enabled = False
                    rnmRate.Enabled = False
                    rnmExceptFast.Enabled = False
                    rdpEffectDate.Enabled = False
                    rdpExpireDate.Enabled = False
                    rtxtDesc.Enabled = False
                    rntxtOrders.Enabled = False
                    EnabledGridNotPostback(rgData, True)
                    rgData.Enabled = True
                Case CommonMessage.STATE_NEW
                    StateValidate(True)
                    'rcboResident.SelectedIndex = 0
                    rnmValueFrom.Value = Nothing
                    rnmValueTo.Value = Nothing
                    rnmRate.Value = Nothing
                    rnmExceptFast.Value = Nothing
                    rdpEffectDate.SelectedDate = Nothing
                    rdpExpireDate.SelectedDate = Nothing
                    rtxtDesc.Text = String.Empty
                    rntxtOrders.Text = "1"

                    rcboResident.Enabled = True
                    rnmValueFrom.Enabled = True
                    rnmValueTo.Enabled = True
                    rnmRate.Enabled = True
                    rnmExceptFast.Enabled = True
                    rdpEffectDate.Enabled = True
                    rdpExpireDate.Enabled = True
                    rtxtDesc.Enabled = True
                    rntxtOrders.Enabled = True
                    EnabledGridNotPostback(rgData, False)
                    rgData.Enabled = False
                Case CommonMessage.STATE_EDIT
                    StateValidate(True)
                    rcboResident.Enabled = True
                    rnmValueFrom.Enabled = True
                    rnmValueTo.Enabled = True
                    rnmRate.Enabled = True
                    rnmExceptFast.Enabled = True
                    rdpEffectDate.Enabled = True
                    rdpExpireDate.Enabled = True
                    rtxtDesc.Enabled = True
                    rntxtOrders.Enabled = True

                    EnabledGridNotPostback(rgData, False)
                    rgData.Enabled = False
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveTaxation(lstDeletes, "I") Then
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
                    If rep.ActiveTaxation(lstDeletes, "A") Then
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
                    If rep.DeleteTaxation(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            'dic.Add("RESIDENT_ID", rcboResident)
            dic.Add("VALUE_FROM", rnmValueFrom)
            dic.Add("VALUE_TO", rnmValueTo)
            dic.Add("RATE", rnmRate)
            dic.Add("EXCEPT_FAST", rnmExceptFast)
            dic.Add("FROM_DATE", rdpEffectDate)
            'dic.Add("TO_DATE", rdpExpireDate)
            dic.Add("SDESC", rtxtDesc)
            dic.Add("ORDERS", rntxtOrders)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTaxation As New PATaxationDTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
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
                    If Page.IsValid Then
                        objTaxation.RESIDENT_ID = If(String.IsNullOrEmpty(rcboResident.SelectedValue), 0, Decimal.Parse(rcboResident.SelectedValue))
                        objTaxation.VALUE_FROM = rnmValueFrom.Value
                        objTaxation.VALUE_TO = rnmValueTo.Value
                        objTaxation.RATE = rnmRate.Value
                        objTaxation.EXCEPT_FAST = rnmExceptFast.Value
                        objTaxation.FROM_DATE = rdpEffectDate.SelectedDate
                        objTaxation.TO_DATE = Nothing 'rdpExpireDate.SelectedDate
                        objTaxation.SDESC = rtxtDesc.Text.Trim
                        objTaxation.ORDERS = If(String.IsNullOrEmpty(rntxtOrders.Text), 1, Decimal.Parse(rntxtOrders.Text))
                        If objTaxation.VALUE_FROM > objTaxation.VALUE_TO Then
                            ShowMessage(Translate("Số tiền TỪ không được lơn hơn số tiền ĐẾN"), NotifyType.Warning)
                            rnmValueTo.Text = String.Empty
                            rnmValueFrom.Text = String.Empty
                            objTaxation.VALUE_FROM = rnmValueFrom.Value
                            objTaxation.VALUE_TO = rnmValueTo.Value
                            Exit Sub
                        End If
                        Dim MaximumRows As Integer

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim dta As List(Of PATaxationDTO) = rep.GetTaxation(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "ORDERS ASC").FindAll(Function(f) (f.FROM_DATE.Value.Date = objTaxation.FROM_DATE And (f.VALUE_FROM <= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO <= f.VALUE_TO) _
                                                                                    Or (f.VALUE_FROM >= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO <= f.VALUE_TO) _
                                                                                    Or (f.VALUE_FROM <= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO >= f.VALUE_TO) _
                                                                                    ))

                                If dta.Count > 0 Then
                                    ShowMessage(Translate("Dữ liệu có khoảng giá trị bị ghi đè nhau!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                objTaxation.ACTFLG = "A"
                                If rep.InsertTaxation(objTaxation, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(rnmExceptFast, rnmRate, rnmValueFrom, rnmValueTo, rntxtOrders, rdpEffectDate, rdpExpireDate, rtxtDesc)
                                    rgData.SelectedIndexes.Clear()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT

                                Dim repvalidate As New CommonRepository
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(rgData.SelectedValue)
                                If repvalidate.CheckExistIDTable(lstID, "PA_Taxation", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    ClearControlValue(rnmExceptFast, rnmRate, rnmValueFrom, rnmValueTo, rntxtOrders)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                objTaxation.ID = rgData.SelectedValue
                                Dim dta As List(Of PATaxationDTO) = rep.GetTaxation(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "ORDERS ASC").FindAll(Function(f) (f.ID <> objTaxation.ID And f.FROM_DATE.Value.Date = objTaxation.FROM_DATE And (f.VALUE_FROM <= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO <= f.VALUE_TO) _
                                                                                   Or (f.VALUE_FROM >= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO <= f.VALUE_TO) _
                                                                                   Or (f.VALUE_FROM <= objTaxation.VALUE_FROM And objTaxation.VALUE_FROM <= f.VALUE_TO And f.VALUE_FROM <= objTaxation.VALUE_TO And objTaxation.VALUE_TO >= f.VALUE_TO) _
                                                                                   ))

                                If dta.Count > 0 Then
                                    ShowMessage(Translate("Dữ liệu có khoảng giá trị bị ghi đè nhau!"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyTaxation(objTaxation, rgData.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTaxation.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(rnmExceptFast, rnmRate, rnmValueFrom, rnmValueTo, rntxtOrders, rdpEffectDate, rdpExpireDate, rtxtDesc)
                                    rgData.SelectedIndexes.Clear()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            'rgData.DataSource = New DataTable()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgData.SelectedItems.Count = 0 Or rgData.SelectedItems.Count > 1)) Then
                ClearControlValue(rnmExceptFast, rnmRate, rnmValueFrom, rnmValueFrom, rnmValueTo, rntxtOrders, rdpEffectDate, rdpExpireDate, rtxtDesc)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Update trạng thái menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_RESIDENT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(rcboResident, ListComboData.LIST_LIST_RESIDENT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, rcboResident.SelectedValue)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

    Private Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If rgData.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgData.SelectedItems(0)
                hidID.Value = slItem("ID").Text
                rnmValueFrom.Value = slItem.GetDataKeyValue("VALUE_FROM").ToString
                rnmValueTo.Value = slItem.GetDataKeyValue("VALUE_TO").ToString
                rnmRate.Value = slItem.GetDataKeyValue("RATE").ToString
                rnmExceptFast.Value = slItem.GetDataKeyValue("EXCEPT_FAST").ToString
                rdpEffectDate.SelectedDate = slItem.GetDataKeyValue("FROM_DATE")
                rtxtDesc.Text = If(slItem.GetDataKeyValue("SDESC") IsNot Nothing, slItem.GetDataKeyValue("SDESC").Text, "")
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class