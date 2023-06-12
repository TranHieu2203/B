Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlATTimeManual
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
#Region "Property"

    Public IDSelect As Integer
    Public Property At_Time_Manual As List(Of AT_TIME_MANUALDTO)
        Get
            Return ViewState(Me.ID & "_ATTimeManual")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            ViewState(Me.ID & "_ATTimeManual") = value
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
    Property ListComboDataAff As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property ListComboDataTypePross As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboDataTypePross")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboDataTypePross") = value
        End Set
    End Property
    Property ListComboDataTimeManuaRate_Morning As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboDataTimeManuaRate_Morning")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboDataTimeManuaRate_Morning") = value
        End Set
    End Property
    Property ListComboDataTimeManuaRate_Afternoon As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboDataTimeManuaRate_Afternoon")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboDataTimeManuaRate_Afternoon") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh("")
            UpdateControlState()
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    ''' <summary>
    ''' Khởi tạo control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
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
    ''' Load, reload page, grid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceBusinessClient
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_MANUALDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.At_Time_Manual = rep.GetAT_TIME_MANUAL(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.At_Time_Manual = rep.GetAT_TIME_MANUAL(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.At_Time_Manual
            Else
                Return rep.GetAT_TIME_MANUAL(obj).ToTable
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Update state control theo State page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    rntxtOrders.Text = ""
                    rdNote.Text = ""
                    'chkIsPrice.Enabled = True
                    'chkIsPrice.Checked = False
                    txtCode.Enabled = True
                    txtCodeKH.Enabled = True
                    txtMappingCode.Enabled = True
                    txtNameVN.Enabled = True
                    rntxtOrders.Enabled = True
                    rdNote.Enabled = True
                    cboMorning.Enabled = True
                    cboAfternoon.Enabled = True
                    cboTypeProcess.Enabled = True
                    cboCongTy.Enabled = True
                    chkIsOther.Enabled = True
                    chkIsRegShift.Enabled = True
                    cboMorningRate.Enabled = True
                    cboAfternoonRate.Enabled = True
                    'rdLimitDay.Enabled = True
                    'rdLimitYear.Enabled = True
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_NORMAL
                    txtCode.Text = ""
                    txtCodeKH.Text = ""
                    txtMappingCode.Text = ""
                    txtNameVN.Text = ""
                    rntxtOrders.Text = ""
                    rdNote.Text = ""
                    cboAfternoon.Text = ""
                    cboMorning.Text = ""
                    cboTypeProcess.Text = ""
                    cboCongTy.Text = ""
                    cboMorningRate.Text = ""
                    chkIsOther.Checked = False
                    chkIsRegShift.Enabled = False
                    cboAfternoonRate.Text = ""
                    'chkIsPrice.Checked = False
                    cboAfternoon.SelectedValue = Nothing
                    cboMorning.SelectedValue = Nothing
                    cboTypeProcess.SelectedValue = Nothing
                    cboCongTy.SelectedValue = Nothing
                    cboMorningRate.SelectedValue = Nothing
                    cboAfternoonRate.SelectedValue = Nothing
                    txtCode.Enabled = False
                    txtCodeKH.Enabled = False
                    txtMappingCode.Enabled = False
                    txtNameVN.Enabled = False
                    rntxtOrders.Enabled = False
                    'chkIsPrice.Enabled = False
                    cboAfternoon.Enabled = False
                    cboMorning.Enabled = False
                    cboTypeProcess.Enabled = False
                    cboCongTy.Enabled = False
                    chkIsOther.Enabled = False
                    chkIsRegShift.Enabled = False
                    cboAfternoonRate.Enabled = False
                    cboMorningRate.Enabled = False
                    rdNote.Enabled = False
                    'rdLimitDay.Enabled = False
                    ' rdLimitDay.Value = Nothing
                    'rdLimitYear.Enabled = False
                    'rdLimitYear.Value = Nothing
                    EnabledGridNotPostback(rgDanhMuc, True)

                Case CommonMessage.STATE_EDIT
                    txtCode.Enabled = True
                    txtMappingCode.Enabled = True
                    txtCodeKH.Enabled = True
                    txtNameVN.Enabled = True
                    rntxtOrders.Enabled = True
                    cboAfternoon.Enabled = True
                    'chkIsPrice.Enabled = True
                    cboMorning.Enabled = True
                    cboTypeProcess.Enabled = True
                    cboCongTy.Enabled = True
                    cboAfternoonRate.Enabled = True
                    chkIsOther.Enabled = True
                    chkIsRegShift.Enabled = True
                    cboMorningRate.Enabled = True
                    rdNote.Enabled = True
                    'rdLimitDay.Enabled = True
                    'rdLimitYear.Enabled = True
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_TIME_MANUAL(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ClearControlValue(txtCode, txtCodeKH, txtMappingCode, txtNameVN, rntxtOrders, cboAfternoon, cboMorning, cboTypeProcess, cboCongTy, chkIsOther, chkIsRegShift, cboMorningRate, cboAfternoonRate, rdNote)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_TIME_MANUAL(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ClearControlValue(txtCode, txtCodeKH, txtMappingCode, txtNameVN, rntxtOrders, cboAfternoon, cboMorning, cboTypeProcess, cboCongTy, chkIsOther, chkIsRegShift, cboMorningRate, cboAfternoonRate, rdNote)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_TIME_MANUAL(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtCode.Focus()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Đổ dữ liệu được chọn từ grid lên control input
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("CODE_KH", txtCodeKH)
            dic.Add("MAPPING_CODE", txtMappingCode)
            dic.Add("NAME_VN", txtNameVN)
            dic.Add("ORDERS", rntxtOrders)
            dic.Add("MORNING_ID", cboMorning)
            dic.Add("AFTERNOON_ID", cboAfternoon)
            dic.Add("TYPE_PROSS_ID", cboTypeProcess)
            dic.Add("ORG_ID", cboCongTy)
            dic.Add("IS_OTHER", chkIsOther)
            dic.Add("IS_REG_SHIFT", chkIsRegShift)
            dic.Add("MORNING_RATE_ID", cboMorningRate)
            dic.Add("AFTERNOON_RATE_ID", cboAfternoonRate)
            'dic.Add("LIMIT_DAY", rdLimitDay)
            'dic.Add("LIMIT_YEAR", rdLimitYear)
            dic.Add("NOTE", rdNote)
            'dic.Add("IS_PAID_RICE", chkIsPrice)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
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
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objHoliday_Gen As New AT_TIME_MANUALDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    cboAfternoon.Text = ""
                    cboMorning.Text = ""
                    cboTypeProcess.Text = ""
                    chkIsOther.Checked = False
                    chkIsRegShift.Checked = False
                    cboCongTy.Text = ""
                    cboAfternoon.SelectedValue = Nothing
                    cboMorning.SelectedValue = Nothing
                    cboTypeProcess.SelectedValue = Nothing
                    cboCongTy.SelectedValue = Nothing

                    cboAfternoonRate.Text = "0.5"
                    cboMorningRate.Text = "0.5"
                    cboMorningRate.SelectedValue = 2
                    cboAfternoonRate.SelectedValue = 2

                    'rdLimitDay.Value = Nothing
                    'rdLimitYear.Value = Nothing
                    rgDanhMuc.SelectedIndexes.Clear()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMuc.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
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
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstID.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstID, AttendanceCommonTABLE_NAME.AT_TIME_MANUAL) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objHoliday_Gen.CODE = txtCode.Text
                        objHoliday_Gen.CODE_KH = txtCodeKH.Text
                        objHoliday_Gen.MAPPING_CODE = txtMappingCode.Text
                        objHoliday_Gen.NAME_VN = txtNameVN.Text
                        objHoliday_Gen.MORNING_ID = cboMorning.SelectedValue
                        objHoliday_Gen.TYPE_PROSS_ID = cboTypeProcess.SelectedValue
                        If cboCongTy.SelectedValue <> "" Then
                            objHoliday_Gen.ORG_ID = cboCongTy.SelectedValue
                        End If
                        objHoliday_Gen.IS_OTHER = chkIsOther.Checked
                        objHoliday_Gen.IS_REG_SHIFT = chkIsRegShift.Checked
                        objHoliday_Gen.MORNING_RATE_ID = cboMorningRate.SelectedValue
                        objHoliday_Gen.AFTERNOON_RATE_ID = cboAfternoonRate.SelectedValue
                        objHoliday_Gen.AFTERNOON_ID = cboAfternoon.SelectedValue
                        'objHoliday_Gen.IS_PAID_RICE = chkIsPrice.Checked
                        objHoliday_Gen.NOTE = rdNote.Text

                        If rntxtOrders.Value IsNot Nothing Then
                            objHoliday_Gen.ORDERS = Decimal.Parse(rntxtOrders.Text)
                        End If

                        'If rdLimitYear.Value IsNot Nothing Then
                        '    objHoliday_Gen.LIMIT_YEAR = rdLimitYear.Value
                        'End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objHoliday_Gen.ACTFLG = "A"
                                If rep.InsertAT_TIME_MANUAL(objHoliday_Gen, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    rgDanhMuc.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim validate As New AT_TIME_MANUALDTO
                                objHoliday_Gen.ID = rgDanhMuc.SelectedValue
                                validate.ID = objHoliday_Gen.ID
                                If rep.ValidateAT_TIME_MANUAL(validate) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    ClearControlValue(txtCode, txtCodeKH, txtMappingCode, txtNameVN, rntxtOrders, cboAfternoon, cboMorning, cboTypeProcess, cboCongTy, chkIsOther, chkIsRegShift, cboMorningRate, cboAfternoonRate, rdNote)
                                    rgDanhMuc.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                If rep.ModifyAT_TIME_MANUAL(objHoliday_Gen, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objHoliday_Gen.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    rgDanhMuc.Rebind()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "KieuCong")
                        End If
                    End Using
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
    ''' Event Yes.No hỏi xóa, áp dụng, ngừng áp dụng
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
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"

    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If String.IsNullOrEmpty(dValue) Then
                Return False
            Else
                Return If(dValue = "-1", True, False)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Get database to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim repS As New ProfileStoreProcedure
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_SIGN = True
                rep.GetComboboxData(ListComboData)
            End If
            If ListComboDataAff Is Nothing Then
                ListComboDataAff = New ComboBoxDataDTO
                ListComboDataAff.GET_LIST_SIGN = True
                rep.GetComboboxData(ListComboDataAff)
            End If

            If ListComboDataTypePross Is Nothing Then
                ListComboDataTypePross = New ComboBoxDataDTO
                ListComboDataTypePross.GET_LIST_TYPE_PROCESS = True
                rep.GetComboboxData(ListComboDataTypePross)
            End If

            If ListComboDataTimeManuaRate_Morning Is Nothing Then
                ListComboDataTimeManuaRate_Morning = New ComboBoxDataDTO
                ListComboDataTimeManuaRate_Morning.GET_LIST_MORNING_RATE = True
                rep.GetComboboxData(ListComboDataTimeManuaRate_Morning)
            End If

            If ListComboDataTimeManuaRate_Afternoon Is Nothing Then
                ListComboDataTimeManuaRate_Afternoon = New ComboBoxDataDTO
                ListComboDataTimeManuaRate_Afternoon.GET_LIST_AFTERNOON_RATE = True
                rep.GetComboboxData(ListComboDataTimeManuaRate_Afternoon)
            End If

            FillRadCombobox(cboMorning, ListComboData.LIST_LIST_SIGN, "NAME_VN", "ID", True)
            'If ListComboData.LIST_LIST_SIGN.Count > 0 Then
            '    cboMorning.SelectedIndex = 0
            'End If
            FillRadCombobox(cboAfternoon, ListComboDataAff.LIST_LIST_SIGN, "NAME_VN", "ID", True)
            'If ListComboData.LIST_LIST_SIGN.Count > 0 Then
            '    cboMorning.SelectedIndex = 0
            'End If

            FillRadCombobox(cboTypeProcess, ListComboDataTypePross.LIST_LIST_TYPE_PROCESS, "NAME_VN", "ID", True)
            FillRadCombobox(cboMorningRate, ListComboDataTimeManuaRate_Morning.LIST_LIST_MORNING_RATE, "VALUE_RATE", "ID", True)
            FillRadCombobox(cboAfternoonRate, ListComboDataTimeManuaRate_Afternoon.LIST_LIST_AFTERNOON_RATE, "VALUE_RATE", "ID", True)

            Dim dtOrgLevel As DataTable
            dtOrgLevel = repS.GET_ORGID_COMPANY_LEVEL()
            Dim dr As DataRow = dtOrgLevel.NewRow
            dr("ORG_ID") = "-1"
            dr("ORG_NAME_VN") = "Dùng  Chung"
            dtOrgLevel.Rows.Add(dr)

            dtOrgLevel.DefaultView.Sort = "ORG_ID ASC"
            FillRadCombobox(cboCongTy, dtOrgLevel, "ORG_NAME_VN", "ORG_ID", True)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Update trạng thái menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Validate code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_TIME_MANUALDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            End If
            If Not args.IsValid Then
                'txtCode.Text = ""
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cvalCodeKH_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCodeKH.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_TIME_MANUALDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE_KH = txtCodeKH.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            Else
                _validate.CODE_KH = txtCodeKH.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            End If
            If Not args.IsValid Then
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cvalCodeKH2_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCodeKH2.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            args.IsValid = Not txtCodeKH.Text.Contains(" ")
            If Not args.IsValid Then
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cvalMappingCode_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMappingCode.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_TIME_MANUALDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.MAPPING_CODE = txtMappingCode.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            Else
                _validate.MAPPING_CODE = txtMappingCode.Text.Trim
                args.IsValid = rep.ValidateAT_TIME_MANUAL(_validate)
            End If
            If Not args.IsValid Then
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate combobox nghỉ nửa ngày
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalMorning_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMorning.ServerValidate
        Dim rep As New AttendanceRepository
        Dim validate As New AT_FMLDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If cboMorning.SelectedValue Is Nothing Then Return
        If cboMorning.SelectedValue = "" Then Return
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboMorning.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateAT_FML(validate)
            End If
            If Not args.IsValid Then
                ListComboData = Nothing
                GetDataCombo()
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate combobox nghỉ nửa ngày
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalAfternoon_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalAfternoon.ServerValidate
        Dim rep As New AttendanceRepository
        Dim validate As New AT_FMLDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If cboAfternoon.SelectedValue Is Nothing Then Return
        If cboAfternoon.SelectedValue = "" Then Return
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboAfternoon.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateAT_FML(validate)
            End If
            If Not args.IsValid Then
                ListComboDataAff = Nothing
                GetDataCombo()
                ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc')")
            End If
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
    Protected Sub rgDanhMuc_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgDanhMuc.SelectedItems.Count = 0 Or rgDanhMuc.SelectedItems.Count > 1)) Then
                ClearControlValue(txtCode, txtMappingCode, txtCodeKH, txtNameVN, rntxtOrders, cboAfternoon, cboMorning, cboTypeProcess, cboCongTy, chkIsOther, chkIsRegShift, cboAfternoonRate, cboMorningRate, rdNote)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Protected Sub rgDanhMuc_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgDanhMuc.SelectedItems.Count = 0 Or rgDanhMuc.SelectedItems.Count > 1)) Then
                ClearControlValue(txtCode, txtCodeKH, txtMappingCode, txtNameVN, rntxtOrders, cboAfternoon, cboMorning, cboTypeProcess, cboCongTy, chkIsOther, chkIsRegShift, cboAfternoonRate, cboMorningRate, rdNote)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
#End Region


End Class