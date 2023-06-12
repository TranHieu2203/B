Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class crtlATCoeffOverTime
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
    Public Property AT_Terminal As List(Of AT_COEFF_OVERTIMEDTO)
        Get
            Return ViewState(Me.ID & "_Termidal")
        End Get
        Set(ByVal value As List(Of AT_COEFF_OVERTIMEDTO))
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
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True

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
                                       ToolbarItem.Delete, ToolbarItem.Export)
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
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
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
        Dim obj As New AT_COEFF_OVERTIMEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)

            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_Terminal = rep.GetAT_COEFF_OVERTIME(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_Terminal = rep.GetAT_COEFF_OVERTIME(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = Me.AT_Terminal
            Else
                Return rep.GetAT_COEFF_OVERTIME(obj).ToTable
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
                    EnableControlAll(True, rdEffectDate, rtFromdate_NightHour, rtTodate_NightHour, chkIs_Tomorow, rtxtNight_Coeff, ntxtWeekday_Coeff, ntxtWeekday_Coeff_PIT, ntxtWeekday_Coeff_NonPIT, ntxtNightWeekday_Coeff, ntxtNightWeekday_Coeff_PIT, ntxtNightWeekday_Coeff_NonPIT, ntxtOffday_Coeff, ntxtOffday_Coeff_PIT, ntxtOffday_Coeff_NonPIT, ntxtNightOffday_Coeff, ntxtNightOffday_Coeff_PIT, ntxtNightOffday_Coeff_NonPIT, ntxtHoliday_Coeff, ntxtHoliday_Coeff_PIT, ntxtHoliday_Coeff_NonPIT, ntxtNightHoliday_Coeff, ntxtNightHoliday_Coeff_PIT, ntxtNightHoliday_Coeff_NonPIT, rnOtMonth, rnOtYear)
                    EnabledGridNotPostback(rgData, False)

                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(rdEffectDate, rtFromdate_NightHour, rtTodate_NightHour, chkIs_Tomorow, rtxtNight_Coeff, ntxtWeekday_Coeff, ntxtWeekday_Coeff_PIT, ntxtWeekday_Coeff_NonPIT, ntxtNightWeekday_Coeff, ntxtNightWeekday_Coeff_PIT, ntxtNightWeekday_Coeff_NonPIT, ntxtOffday_Coeff, ntxtOffday_Coeff_PIT, ntxtOffday_Coeff_NonPIT, ntxtNightOffday_Coeff, ntxtNightOffday_Coeff_PIT, ntxtNightOffday_Coeff_NonPIT, ntxtHoliday_Coeff, ntxtHoliday_Coeff_PIT, ntxtHoliday_Coeff_NonPIT, ntxtNightHoliday_Coeff, ntxtNightHoliday_Coeff_PIT, ntxtNightHoliday_Coeff_NonPIT, rnOtMonth, rnOtYear)
                    EnableControlAll(False, rdEffectDate, rtFromdate_NightHour, rtTodate_NightHour, chkIs_Tomorow, rtxtNight_Coeff, ntxtWeekday_Coeff, ntxtWeekday_Coeff_PIT, ntxtWeekday_Coeff_NonPIT, ntxtNightWeekday_Coeff, ntxtNightWeekday_Coeff_PIT, ntxtNightWeekday_Coeff_NonPIT, ntxtOffday_Coeff, ntxtOffday_Coeff_PIT, ntxtOffday_Coeff_NonPIT, ntxtNightOffday_Coeff, ntxtNightOffday_Coeff_PIT, ntxtNightOffday_Coeff_NonPIT, ntxtHoliday_Coeff, ntxtHoliday_Coeff_PIT, ntxtHoliday_Coeff_NonPIT, ntxtNightHoliday_Coeff, ntxtNightHoliday_Coeff_PIT, ntxtNightHoliday_Coeff_NonPIT, rnOtMonth, rnOtYear)
                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rdEffectDate, rtFromdate_NightHour, rtTodate_NightHour, chkIs_Tomorow, rtxtNight_Coeff, ntxtWeekday_Coeff, ntxtWeekday_Coeff_PIT, ntxtWeekday_Coeff_NonPIT, ntxtNightWeekday_Coeff, ntxtNightWeekday_Coeff_PIT, ntxtNightWeekday_Coeff_NonPIT, ntxtOffday_Coeff, ntxtOffday_Coeff_PIT, ntxtOffday_Coeff_NonPIT, ntxtNightOffday_Coeff, ntxtNightOffday_Coeff_PIT, ntxtNightOffday_Coeff_NonPIT, ntxtHoliday_Coeff, ntxtHoliday_Coeff_PIT, ntxtHoliday_Coeff_NonPIT, ntxtNightHoliday_Coeff, ntxtNightHoliday_Coeff_PIT, ntxtNightHoliday_Coeff_NonPIT, rnOtMonth, rnOtYear)
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_COEFF_OVERTIME(lstDeletes) Then
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
            dic.Add("ID", hidID)
            dic.Add("EFFECTDATE", rdEffectDate)
            dic.Add("FROMDATE_NIGHTHOUR", rtFromdate_NightHour)
            dic.Add("TODATE_NIGHTHOUR", rtTodate_NightHour)
            dic.Add("IS_TOMOROW", chkIs_Tomorow)
            dic.Add("NIGHT_COEFF", rtxtNight_Coeff)
            dic.Add("WEEKDAY_COEFF", ntxtWeekday_Coeff)
            dic.Add("WEEKDAY_COEFF_PIT", ntxtWeekday_Coeff_PIT)
            dic.Add("WEEKDAY_COEFF_NONPIT", ntxtWeekday_Coeff_NonPIT)
            dic.Add("NIGHTWEEKDAY_COEFF", ntxtNightWeekday_Coeff)
            dic.Add("NIGHTWEEKDAY_COEFF_PIT", ntxtNightWeekday_Coeff_PIT)
            dic.Add("NIGHTWEEKDAY_COEFF_NONPIT", ntxtNightWeekday_Coeff_NonPIT)
            dic.Add("OFFDAY_COEFF", ntxtOffday_Coeff)
            dic.Add("OFFDAY_COEFF_PIT", ntxtOffday_Coeff_PIT)
            dic.Add("OFFDAY_COEFF_NONPIT", ntxtOffday_Coeff_NonPIT)
            dic.Add("NIGHTOFFDAY_COEFF", ntxtNightOffday_Coeff)
            dic.Add("NIGHTOFFDAY_COEFF_PIT", ntxtNightOffday_Coeff_PIT)
            dic.Add("NIGHTOFFDAY_COEFF_NONPIT", ntxtNightOffday_Coeff_NonPIT)
            dic.Add("HOLIDAY_COEFF", ntxtHoliday_Coeff)
            dic.Add("HOLIDAY_COEFF_PIT", ntxtHoliday_Coeff_PIT)
            dic.Add("HOLIDAY_COEFF_NONPIT", ntxtHoliday_Coeff_NonPIT)
            dic.Add("NIGHTHOLIDAY_COEFF", ntxtNightHoliday_Coeff)
            dic.Add("NIGHTHOLIDAY_COEFF_PIT", ntxtNightHoliday_Coeff_PIT)
            dic.Add("NIGHTHOLIDAY_COEFF_NONPIT", ntxtNightHoliday_Coeff_NonPIT)
            dic.Add("OT_MONTH", rnOtMonth)
            dic.Add("OT_YEAR", rnOtYear)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Try
            Dim rep As New AttendanceRepository
            Dim item = CType(rgData.SelectedItems(0), GridDataItem)
            Dim ID = item.GetDataKeyValue("ID").ToString
            Dim At_Data = (From p In AT_Terminal Where p.ID = Decimal.Parse(ID)).SingleOrDefault
            rtFromdate_NightHour.SelectedDate = At_Data.FROMDATE_NIGHTHOUR
            rtTodate_NightHour.SelectedDate = At_Data.TODATE_NIGHTHOUR
            rep.Dispose()
        Catch ex As Exception
            Throw ex
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
        Dim objTerminal As New AT_COEFF_OVERTIMEDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdEffectDate, rtFromdate_NightHour, rtTodate_NightHour, chkIs_Tomorow, rtxtNight_Coeff, ntxtWeekday_Coeff, ntxtWeekday_Coeff_PIT, ntxtWeekday_Coeff_NonPIT, ntxtNightWeekday_Coeff, ntxtNightWeekday_Coeff_PIT, ntxtNightWeekday_Coeff_NonPIT, ntxtOffday_Coeff, ntxtOffday_Coeff_PIT, ntxtOffday_Coeff_NonPIT, ntxtNightOffday_Coeff, ntxtNightOffday_Coeff_PIT, ntxtNightOffday_Coeff_NonPIT, ntxtHoliday_Coeff, ntxtHoliday_Coeff_PIT, ntxtHoliday_Coeff_NonPIT, ntxtNightHoliday_Coeff, ntxtNightHoliday_Coeff_PIT, ntxtNightHoliday_Coeff_NonPIT)
                    rtxtNight_Coeff.Value = 0
                    ntxtWeekday_Coeff.Value = 0
                    ntxtWeekday_Coeff_PIT.Value = 0
                    ntxtWeekday_Coeff_NonPIT.Value = 0
                    ntxtNightWeekday_Coeff.Value = 0
                    ntxtNightWeekday_Coeff_PIT.Value = 0
                    ntxtNightWeekday_Coeff_NonPIT.Value = 0
                    ntxtOffday_Coeff.Value = 0
                    ntxtOffday_Coeff_PIT.Value = 0
                    ntxtOffday_Coeff_NonPIT.Value = 0
                    ntxtNightOffday_Coeff.Value = 0
                    ntxtNightOffday_Coeff_PIT.Value = 0
                    ntxtNightOffday_Coeff_NonPIT.Value = 0
                    ntxtHoliday_Coeff.Value = 0
                    ntxtHoliday_Coeff_PIT.Value = 0
                    ntxtHoliday_Coeff_NonPIT.Value = 0
                    ntxtNightHoliday_Coeff.Value = 0
                    ntxtNightHoliday_Coeff_PIT.Value = 0
                    ntxtNightHoliday_Coeff_NonPIT.Value = 0
                    rnOtMonth.Value = 0
                    rnOtYear.Value = 0
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If (rgData.SelectedItems.Count > 1) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

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

                        If rep.CheckAT_COEFF_OVERTIME(rdEffectDate.SelectedDate, If(rgData.SelectedValue Is Nothing, 0, rgData.SelectedValue)) = False Then
                            ShowMessage(Translate("Ngày hiệu lực đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        objTerminal.EFFECTDATE = rdEffectDate.SelectedDate
                        objTerminal.FROMDATE_NIGHTHOUR = rtFromdate_NightHour.SelectedDate
                        objTerminal.TODATE_NIGHTHOUR = rtTodate_NightHour.SelectedDate
                        objTerminal.IS_TOMOROW = chkIs_Tomorow.Checked
                        objTerminal.NIGHT_COEFF = If(rtxtNight_Coeff.Value Is Nothing, 0, rtxtNight_Coeff.Value)
                        objTerminal.WEEKDAY_COEFF = If(ntxtWeekday_Coeff.Value Is Nothing, 0, ntxtWeekday_Coeff.Value)
                        objTerminal.WEEKDAY_COEFF_PIT = If(ntxtWeekday_Coeff_PIT.Value Is Nothing, 0, ntxtWeekday_Coeff_PIT.Value)
                        objTerminal.WEEKDAY_COEFF_NONPIT = If(ntxtWeekday_Coeff_NonPIT.Value Is Nothing, 0, ntxtWeekday_Coeff_NonPIT.Value)
                        objTerminal.NIGHTWEEKDAY_COEFF = If(ntxtNightWeekday_Coeff.Value Is Nothing, 0, ntxtNightWeekday_Coeff.Value)
                        objTerminal.NIGHTWEEKDAY_COEFF_PIT = If(ntxtNightWeekday_Coeff_PIT.Value Is Nothing, 0, ntxtNightWeekday_Coeff_PIT.Value)
                        objTerminal.NIGHTWEEKDAY_COEFF_NONPIT = If(ntxtNightWeekday_Coeff_NonPIT.Value Is Nothing, 0, ntxtNightWeekday_Coeff_NonPIT.Value)
                        objTerminal.OFFDAY_COEFF = If(ntxtOffday_Coeff.Value Is Nothing, 0, ntxtOffday_Coeff.Value)
                        objTerminal.OFFDAY_COEFF_PIT = If(ntxtOffday_Coeff_PIT.Value Is Nothing, 0, ntxtOffday_Coeff_PIT.Value)
                        objTerminal.OFFDAY_COEFF_NONPIT = If(ntxtOffday_Coeff_NonPIT.Value Is Nothing, 0, ntxtOffday_Coeff_NonPIT.Value)
                        objTerminal.NIGHTOFFDAY_COEFF = If(ntxtNightOffday_Coeff.Value Is Nothing, 0, ntxtNightOffday_Coeff.Value)
                        objTerminal.NIGHTOFFDAY_COEFF_PIT = If(ntxtNightOffday_Coeff_PIT.Value Is Nothing, 0, ntxtNightOffday_Coeff_PIT.Value)
                        objTerminal.NIGHTOFFDAY_COEFF_NONPIT = If(ntxtNightOffday_Coeff_NonPIT.Value Is Nothing, 0, ntxtNightOffday_Coeff_NonPIT.Value)
                        objTerminal.HOLIDAY_COEFF = If(ntxtHoliday_Coeff.Value Is Nothing, 0, ntxtHoliday_Coeff.Value)
                        objTerminal.HOLIDAY_COEFF_PIT = If(ntxtHoliday_Coeff_PIT.Value Is Nothing, 0, ntxtHoliday_Coeff_PIT.Value)
                        objTerminal.HOLIDAY_COEFF_NONPIT = If(ntxtHoliday_Coeff_NonPIT.Value Is Nothing, 0, ntxtHoliday_Coeff_NonPIT.Value)
                        objTerminal.NIGHTHOLIDAY_COEFF = If(ntxtNightHoliday_Coeff.Value Is Nothing, 0, ntxtNightHoliday_Coeff.Value)
                        objTerminal.NIGHTHOLIDAY_COEFF_PIT = If(ntxtNightHoliday_Coeff_PIT.Value Is Nothing, 0, ntxtNightHoliday_Coeff_PIT.Value)
                        objTerminal.NIGHTHOLIDAY_COEFF_NONPIT = If(ntxtNightHoliday_Coeff_NonPIT.Value Is Nothing, 0, ntxtNightHoliday_Coeff_NonPIT.Value)
                        objTerminal.OT_MONTH = If(rnOtMonth.Value Is Nothing, 0, rnOtMonth.Value)
                        objTerminal.OT_YEAR = If(rnOtYear.Value Is Nothing, 0, rnOtYear.Value)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertAT_COEFF_OVERTIME(objTerminal, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTerminal.ID = rgData.SelectedValue
                                If rep.ModifyAT_COEFF_OVERTIME(objTerminal, rgData.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTerminal.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
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
                            rgData.ExportExcel(Server, Response, dtDatas, "Thiết lập Chu kỳ chấm công")
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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
    Protected Sub rgData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgData.SelectedItems.Count = 0 Or rgData.SelectedItems.Count > 1)) Then
                'ClearControlValue(txtCode, txtName, txtAddress, txtGhichu, txtIP, txtpasswords, txtPort, cboTerminalType)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

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
            'Dim dtData As DataTable
            'dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            'FillRadCombobox(cboObjEmployee, dtData, "NAME", "ID")
            rep.Dispose()
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class