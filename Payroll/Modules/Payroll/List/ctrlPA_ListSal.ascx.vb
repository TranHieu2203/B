Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPA_ListSal
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Paroll/Module/Paroll/List/" + Me.GetType().Name.ToString()

#Region "Property"
    Public IDSelect As Integer

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
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True
            rgData.PageSize = 50
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Seperator,
                                       ToolbarItem.Active, ToolbarItem.Seperator,
                                       ToolbarItem.Deactive)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data for combobox kiểu công, set data cho control khi click row trên grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID) '--
            dic.Add("COL_NAME", txtCOL_NAME) '--
            dic.Add("NAME_VN", txtNAME_VN) '--
            dic.Add("GROUP_TYPE", cboGROUP_TYPE)
            dic.Add("DATA_TYPE", cboDATA_TYPE) '--
            dic.Add("COL_INDEX", nmCOL_INDEX) '--
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        'Dim rep As New PayrollBusinessClient
        Try

            UpdateControlState()
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim objData As New List(Of PAListSalDTO)
        Dim _filter As New PAListSalDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Using rep As New PayrollRepository
                If Sorts IsNot Nothing Then
                    objData = rep.GetListSal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "COL_INDEX ASC")
                Else
                    objData = rep.GetListSal(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = objData
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    'EnableRadCombo(cboGROUP_TYPE, True)
                    cboGROUP_TYPE.Enabled = False
                    cboDATA_TYPE.Enabled = False
                    EnableControlAll(False, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
                    rgData.Rebind()
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    'EnableRadCombo(cboGROUP_TYPE, False)
                    cboGROUP_TYPE.Enabled = True
                    cboDATA_TYPE.Enabled = True
                    EnableControlAll(True, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
                    ClearControlValue(cboDATA_TYPE, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    'EnableRadCombo(cboGROUP_TYPE, False)
                    cboGROUP_TYPE.Enabled = True
                    cboDATA_TYPE.Enabled = True
                    EnableControlAll(True, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
                Case CommonMessage.STATE_DEACTIVE, CommonMessage.STATE_ACTIVE, CommonMessage.STATE_DELETE
                    EnabledGridNotPostback(rgData, True)
                    'EnableRadCombo(cboGROUP_TYPE, False)
                    cboGROUP_TYPE.Enabled = False
                    cboDATA_TYPE.Enabled = False
                    EnableControlAll(False, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
                    ClearControlValue(cboDATA_TYPE, txtNAME_VN, txtCOL_NAME, nmCOL_INDEX, txtRemark)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    'Private Sub cboTYPE_PAYMENT_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTYPE_PAYMENT.SelectedIndexChanged
    '    Try
    '        Dim s = cboTYPE_PAYMENT.SelectedValue
    '        CreateDataFilter()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim gID As Decimal
        Dim objdata As PAListSalDTO
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCOL_NAME, txtNAME_VN, txtRemark, cboDATA_TYPE, cboGROUP_TYPE, nmCOL_INDEX)
                    rgData.SelectedIndexes.Clear()
                    txtCOL_NAME.Focus()
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
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    CurrentState = CommonMessage.STATE_ACTIVE
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    CurrentState = CommonMessage.STATE_DEACTIVE
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    CurrentState = CommonMessage.STATE_DELETE
                Case CommonMessage.TOOLBARITEM_SAVE
                    'If cboTYPE_PAYMENT.SelectedIndex < 0 Then
                    '    ShowMessage("Bạn chưa chọn bảng lương, kiểm tra lại!", NotifyType.Warning)
                    '    cboTYPE_PAYMENT.Focus()
                    '    Exit Sub
                    'End If
                    If Page.IsValid Then
                        Using rep As New PayrollRepository

                            objdata = New PAListSalDTO
                            objdata.COL_NAME = txtCOL_NAME.Text
                            objdata.COL_CODE = objdata.COL_NAME
                            objdata.DATA_TYPE = cboDATA_TYPE.SelectedValue
                            objdata.NAME_VN = txtNAME_VN.Text
                            objdata.NAME_EN = txtNAME_VN.Text
                            objdata.COL_INDEX = Utilities.ObjToDecima(nmCOL_INDEX.Value)
                            objdata.GROUP_TYPE = If(String.IsNullOrEmpty(cboGROUP_TYPE.SelectedValue), Nothing, cboGROUP_TYPE.SelectedValue)
                            objdata.REMARK = txtRemark.Text


                            If CurrentState = CommonMessage.STATE_NEW Then
                                objdata.STATUS = "A"
                                If rep.InsertListSal(objdata, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                End If
                            ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                                Dim repcommon As New CommonRepository
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(hidID.Value)
                                If repcommon.CheckExistIDTable(lstID, "PA_LISTSAL", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If
                                objdata.ID = hidID.Value
                                If rep.ModifyListSal(objdata, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                End If
                            End If
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes.no message hỏi xóa, áp dụng, ngừng áp dụng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim lst As New List(Of Decimal)
        Try
            Dim item = rgData.SelectedItems
            For Each i As GridDataItem In item
                lst.Add(i.GetDataKeyValue("ID"))
            Next
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                If rep.ActiveListSal(lst, "A") Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.DataSource = Nothing
                    rgData.Rebind()
                End If
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                If rep.ActiveListSal(lst, "I") Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.DataSource = Nothing
                    rgData.Rebind()
                End If
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                If rep.DeleteListSal(lst) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.DataSource = Nothing
                    rgData.Rebind()
                End If
                'If rep.DeleteListSalariesStatus(lst, 1) Then
                '    CurrentState = CommonMessage.STATE_NORMAL
                'End If
            Else
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            rep.Dispose()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox cboGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboGROUP_TYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGROUP_TYPE.SelectedIndexChanged
        'rgData.Rebind()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim s = cboGROUP_TYPE.SelectedValue
            CreateDataFilter()
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
                ClearControlValue(txtCOL_NAME, txtNAME_VN, txtRemark, cboDATA_TYPE, cboGROUP_TYPE, nmCOL_INDEX)
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
                ListComboData.GET_GROUP_TYPE = True
                rep.GetComboboxData(ListComboData)
            End If
            rep.Dispose()
            FillRadCombobox(cboGROUP_TYPE, ListComboData.LIST_GROUP_TYPE, "NAME_VN", "ID", True)
            'FillDropDownList(cboGROUP_TYPE, ListComboData.LIST_GROUP_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboGROUP_TYPE.SelectedValue)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate nhóm ký hiệu
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalGROUP_TYPE_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalGROUP_TYPE.ServerValidate
        Dim rep As New PayrollRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_GROUP_TYPE = True
                Dim lstID As New List(Of OT_OTHERLIST_DTO)
                Dim obj As New OT_OTHERLIST_DTO
                obj.ID = cboGROUP_TYPE.SelectedValue
                lstID.Add(obj)
                ListComboData.LIST_GROUP_TYPE = lstID
                args.IsValid = rep.ValidateCombobox(ListComboData)
            End If
            If Not args.IsValid Then
                ListComboData = Nothing
                GetDataCombo()
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region



End Class