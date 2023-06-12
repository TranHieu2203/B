Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class crtlSetUpExchange
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' IDSelec
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer
    ''' <summary>
    ''' list AT_Terminal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_Terminal As List(Of AT_SETUP_EXCHANGEDTO)
        Get
            Return ViewState(Me.ID & "_Termidal")
        End Get
        Set(ByVal value As List(Of AT_SETUP_EXCHANGEDTO))
            ViewState(Me.ID & "_Termidal") = value
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
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo các thiết lập ban đầu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rglSwipeMachine)
            rglSwipeMachine.AllowCustomPaging = True

            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.ShowLevel = 2
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo, thiết lập các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới tình trạng các control theo trạng thái hiện tại của trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"

                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeMachine.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeMachine.CurrentPageIndex = 0
                        rglSwipeMachine.MasterTableView.SortExpressions.Clear()
                        rglSwipeMachine.Rebind()
                    Case "Cancel"
                        rglSwipeMachine.MasterTableView.ClearSelectedItems()
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

    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo dữ liệu filter cho grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SETUP_EXCHANGEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rglSwipeMachine, obj)
            If ctrlOrganization.CurrentValue <> "" Then
                obj.ORG_ID = ctrlOrganization.CurrentValue
                obj.IS_DISSOLVE = ctrlOrganization.IsDissolve

            End If
            Dim Sorts As String = rglSwipeMachine.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_Terminal = rep.GetAT_SetUp_Exchange(obj, rglSwipeMachine.CurrentPageIndex, rglSwipeMachine.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_Terminal = rep.GetAT_SetUp_Exchange(obj, rglSwipeMachine.CurrentPageIndex, rglSwipeMachine.PageSize, MaximumRows)
                End If
                rglSwipeMachine.VirtualItemCount = MaximumRows
                rglSwipeMachine.DataSource = Me.AT_Terminal
            Else
                Return rep.GetAT_SetUp_Exchange(obj).ToTable
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    ' txtCode.Text = rep.AutoGenCode("MCC", "AT_TERMINALS", "TERMINAL_CODE")
                    EnableControlAll(True, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                    EnabledGridNotPostback(rglSwipeMachine, False)

                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                    EnableControlAll(False, txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                    EnabledGridNotPostback(rglSwipeMachine, True)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                    EnabledGridNotPostback(rglSwipeMachine, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rglSwipeMachine.SelectedItems.Count - 1
                        Dim item As GridDataItem = rglSwipeMachine.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SetUp_Exchange(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rglSwipeMachine.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                    ClearControlValue(txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rglSwipeMachine.SelectedItems.Count - 1
                        Dim item As GridDataItem = rglSwipeMachine.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SetUp_Exchange(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rglSwipeMachine.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                    ClearControlValue(txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rglSwipeMachine.SelectedItems.Count - 1
                        Dim item As GridDataItem = rglSwipeMachine.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_SetUp_Exchange(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            'txtCode.Focus()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Bind dữ liệu lên form thêm/sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dic.Add("ORG_NAME", txtCongty)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("OBJECT_ATTENDACE", cboObjectAttendace)
            dic.Add("TYPE_EXCHANGE", cboType)
            dic.Add("FROM_MINUTE", rtxtFromMinute)
            dic.Add("TO_MINUTE", rtxtToMinute)
            ' dic.Add("NUMBER_DATE", rtxtDateDeducted)
            GetDataCombo()
            Utilities.OnClientRowSelectedChanged(rglSwipeMachine, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command khi click các item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTerminal As New AT_SETUP_EXCHANGEDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If ctrlOrganization.CurrentValue = "" Or ctrlOrganization.CurrentValue = 1 Then
                        ShowMessage("Chỉ được thêm cấp công ty, Thao tác lại", NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCongty, rdEffectDate, cboObjectAttendace, cboType, rtxtFromMinute, rtxtToMinute, rtxtDateDeducted, rtxtSTT)
                    txtCongty.Text = ctrlOrganization.CurrentText
                    txtCongty.Enabled = False
                    ctrlOrganization.Enabled = False
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rglSwipeMachine.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If (rglSwipeMachine.SelectedItems.Count > 1) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If ctrlOrganization.CurrentValue = "" Or ctrlOrganization.CurrentValue = 1 Then
                        ShowMessage("Chỉ được thêm cấp công ty, Thao tác lại", NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlOrganization.Enabled = False
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rglSwipeMachine.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rglSwipeMachine.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rglSwipeMachine.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim id_check As Decimal
                        If rtxtFromMinute.Value.HasValue And rtxtToMinute.Value.HasValue And cboObjectAttendace.SelectedValue IsNot Nothing And cboType.SelectedValue IsNot Nothing Then
                            If rglSwipeMachine.SelectedValue IsNot Nothing Then
                                id_check = rglSwipeMachine.SelectedValue
                            Else
                                id_check = 0
                            End If
                            Dim check = rep.CheckTrung_AT__SetUp_exchange(id_check, rtxtFromMinute.Value, rtxtToMinute.Value, rdEffectDate.SelectedDate, cboObjectAttendace.SelectedValue, cboType.SelectedValue, ctrlOrganization.CurrentValue)
                            If check = 1 Then
                                ShowMessage(Translate("Trùng số lượng phút,kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        objTerminal.ORG_ID = ctrlOrganization.CurrentValue
                        objTerminal.EFFECT_DATE = rdEffectDate.SelectedDate
                        If cboObjectAttendace.SelectedValue <> "" Then
                            objTerminal.OBJECT_ATTENDACE = cboObjectAttendace.SelectedValue
                        End If
                        If cboType.SelectedValue <> "" Then
                            objTerminal.TYPE_EXCHANGE = cboType.SelectedValue
                        End If
                        objTerminal.FROM_MINUTE = rtxtFromMinute.Value
                        objTerminal.TO_MINUTE = rtxtToMinute.Value
                        objTerminal.NUMBER_DATE = rtxtDateDeducted.Value
                        If IsNumeric(rtxtSTT.Value) Then
                            objTerminal.STT = rtxtSTT.Value
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objTerminal.ORG_ID = ctrlOrganization.CurrentValue
                                objTerminal.ACTFLG = "A"
                                If rep.InsertAT_SetUp_Exchange(objTerminal, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ctrlOrganization.Enabled = True
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTerminal.ID = rglSwipeMachine.SelectedValue
                                'check exist item at_project_title
                                'Dim _validate As New AT_TERMINALSDTO
                                '_validate.ID = rglSwipeMachine.SelectedValue
                                'không có yêu cầu bắt trùng,nếu có thì mở chỗ này ra r làm tiếp
                                'If rep.ValidateAT_TERMINAL(_validate) Then
                                '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                '    'ClearControlValue(txtCode, txtName, txtAddress, txtGhichu, txtIP, txtpasswords, txtPort, cboTerminalType)
                                '    rglSwipeMachine.Rebind()
                                '    CurrentState = CommonMessage.STATE_NORMAL
                                '    UpdateControlState()
                                '    Exit Sub
                                'End If
                                If rep.ModifyAT_SetUp_Exchange(objTerminal, rglSwipeMachine.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTerminal.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ctrlOrganization.Enabled = True
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ctrlOrganization.Enabled = True
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rglSwipeMachine.ExportExcel(Server, Response, dtDatas, "MCC")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện button command khi click yes/no của confirm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
        End Try
    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rglSwipeMachine.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
    Protected Sub rglSwipeMachine_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rglSwipeMachine.SelectedItems.Count = 0 Or rglSwipeMachine.SelectedItems.Count > 1)) Then
                'ClearControlValue(txtCode, txtName, txtAddress, txtGhichu, txtIP, txtpasswords, txtPort, cboTerminalType)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện server validate của control cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    '    Dim rep As New AttendanceRepository
    '    Dim _validate As New AT_TERMINALSDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.ID = rglSwipeMachine.SelectedValue
    '            ' _validate.TERMINAL_CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateAT_TERMINAL(_validate)
    '        Else
    '            '   _validate.TERMINAL_CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateAT_TERMINAL(_validate)
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của control ctrlOrg
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rglSwipeMachine.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 16/08/2017 14:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New AttendanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtData As DataTable
            dtData = rep.GetOtherList("OBJECT_ATTENDANCE", True)
            FillRadCombobox(cboObjectAttendace, dtData, "NAME", "ID")
            Dim dtData1 As DataTable
            dtData1 = rep.GetOtherList("TYPE_DSVM", True)
            FillRadCombobox(cboType, dtData1, "NAME", "ID")
            rep.Dispose()
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub rglSwipeMachine_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rglSwipeMachine.SelectedIndexChanged
        Try
            If rglSwipeMachine.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rglSwipeMachine.SelectedItems(0)
                txtCongty.Text = slItem.GetDataKeyValue("ORG_NAME")
                rdEffectDate.SelectedDate = slItem.GetDataKeyValue("EFFECT_DATE")
                cboObjectAttendace.Text = slItem.GetDataKeyValue("OBJECT_ATTENDACE_NAME")
                cboType.Text = slItem.GetDataKeyValue("TYPE_EXCHANGE_NAME")
                If slItem.GetDataKeyValue("FROM_MINUTE") IsNot Nothing Then
                    rtxtFromMinute.Value = Decimal.Parse(slItem.GetDataKeyValue("FROM_MINUTE"))
                End If
                If slItem.GetDataKeyValue("TO_MINUTE") IsNot Nothing Then
                    rtxtToMinute.Value = Decimal.Parse(slItem.GetDataKeyValue("TO_MINUTE"))
                End If
                If slItem.GetDataKeyValue("NUMBER_DATE") IsNot Nothing Then
                    rtxtDateDeducted.Value = Decimal.Parse(slItem.GetDataKeyValue("NUMBER_DATE"))
                End If
                If slItem.GetDataKeyValue("STT") IsNot Nothing Then
                    rtxtSTT.Value = Decimal.Parse(slItem.GetDataKeyValue("STT"))
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rglSwipeMachine_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rglSwipeMachine.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(If(datarow.GetDataKeyValue("ORG_DESC") IsNot Nothing, datarow.GetDataKeyValue("ORG_DESC").ToString, ""))
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class