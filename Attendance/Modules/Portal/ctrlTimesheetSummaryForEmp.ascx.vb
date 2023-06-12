Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTimesheetSummaryForEmp
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách bảng công tổng hợp 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TIME_TIMESHEET_MONTHLYDTO As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Get
            Return ViewState(Me.ID & "_TIME_TIMESHEET_MONTHLYDTO")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_MONTHLYDTO))
            ViewState(Me.ID & "_TIME_TIMESHEET_MONTHLYDTO") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách PERIOD
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè khởi tạo trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgData)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            rgData.ClientSettings.EnablePostBackOnRowClick = False
            'InitControl()
            If Not IsPostBack Then
                GirdConfig(rgData)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    'Protected Sub InitControl()
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New AttendanceRepository
    '        Me.ctrlMessageBox.Listener = Me
    '        Me.MainToolBar = tbarMainToolBar
    '        Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Deactive, ToolbarItem.Active, ToolbarItem.Calculate, ToolbarItem.Export)
    '        MainToolBar.Items(0).Text = Translate("Đóng")
    '        MainToolBar.Items(1).Text = Translate("Mở")
    '        MainToolBar.Items(2).Text = Translate("Tổng hợp")
    '        Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
    '        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Bind dữ liệu lên form sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 46
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            Dim dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            If dtData IsNot Nothing Then
                FillRadCombobox(cboOjbEmp, dtData, "NAME", "ID")
                cboOjbEmp.SelectedValue = LogHelper.CurrentUser.OBJECT_EMPLOYEE_ID
            End If
            LoadStartEndDate()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới thông tin trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                'Else
                '    Select Case Message
                '        Case "UpdateView"
                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                '            rgData.Rebind()
                '            'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                '            CurrentState = CommonMessage.STATE_NORMAL
                '        Case "InsertView"
                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                '            rgData.CurrentPageIndex = 0
                '            rgData.MasterTableView.SortExpressions.Clear()
                '            rgData.Rebind()
                '            'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                '        Case "Cancel"
                '            rgData.MasterTableView.ClearSelectedItems()
                '    End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                'Case TOOLBARITEM_DELETE
                '    'Kiểm tra các điều kiện để xóa.
                '    If rgData.SelectedItems.Count = 0 Then
                '        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    'Hiển thị Confirm delete.
                '    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                '    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                '    ctrlMessageBox.DataBind()
                '    ctrlMessageBox.Show()
                'Case TOOLBARTIEM_CALCULATE
                '    Dim rep As New AttendanceRepository
                '    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                '        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Error)
                '        Exit Sub
                '    End If
                '    Dim lsEmployee As New List(Of Decimal?)
                '    'Dim employee_id As Decimal?
                '    'For Each items As GridDataItem In rgData.MasterTableView.GetSelectedItems()
                '    '    Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                '    '    employee_id = Decimal.Parse(item)
                '    '    lsEmployee.Add(employee_id)
                '    'Next
                '    Dim _param = New ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                '                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                '                                    .IS_DISSOLVE = ctrlOrg.IsDissolve,
                '                                    .FROMDATE = rdtungay.SelectedDate,
                '                                    .ENDDATE = rdDenngay.SelectedDate,
                '                                    .EMP_OBJ = cboOjbEmp.SelectedValue}
                '    If getSE_CASE_CONFIG("ctrlTimesheetSummaryForEmp_case1") > 0 Then
                '        If rep.CAL_TIME_TIMESHEET_MONTHLY(_param, "ctrlTimesheetSummaryForEmp_case1", lsEmployee) Then
                '            Refresh("UpdateView")
                '        Else
                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                '            Exit Sub
                '        End If
                '    Else
                '        If rep.CAL_TIME_TIMESHEET_MONTHLY(_param, "", lsEmployee) Then
                '            Refresh("UpdateView")
                '        Else
                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                '            Exit Sub
                '        End If
                '    End If
                'Case TOOLBARITEM_EXPORT
                '    Using xls As New ExcelCommon
                '        Dim dtDatas As DataTable
                '        dtDatas = CreateDataFilter(True)
                '        If dtDatas.Rows.Count > 0 Then
                '            rgData.ExportExcel(Server, Response, dtDatas, "DataTimeSheet")
                '        Else
                '            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                '            Exit Sub
                '        End If
                '    End Using
                'Case TOOLBARITEM_ACTIVE
                '    Dim rep As New AttendanceRepository
                '    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                '        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    Dim _param = New ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                '                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                '                                    .STATUS = 0,
                '                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}

                '    If rep.IS_PERIOD_PAYSTATUS(_param) Then
                '        ShowMessage(Translate("Kỳ lương đã đóng, hoặc chưa được tạo"), Utilities.NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    If rep.IS_PERIOD_PAYSTATUS(_param, True) Then
                '        ShowMessage(Translate("Kỳ lương tháng trước chưa đóng."), Utilities.NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    _param.STATUS = 1
                '    'Check tồn tại dữ liệu 
                '    'Dim dtDatas As DataTable
                '    'dtDatas = CreateDataFilter(True)
                '    'If dtDatas.Rows.Count = 0 Then
                '    '    ShowMessage(Translate("Chưa tồn tại bảng công tổng hợp trong kỳ công này"), NotifyType.Warning)
                '    '    Exit Sub
                '    'End If
                '    If rep.CLOSEDOPEN_PERIOD(_param) Then
                '        Refresh("UpdateView")
                '        CurrentState = CommonMessage.TOOLBARITEM_ACTIVE
                '        UpdateControlState()
                '        ' gọi lại hàm này để load lại trạng thái của menu
                '        cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                '    Else
                '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                '        Exit Sub
                '    End If
                'Case TOOLBARITEM_DEACTIVE
                '    Dim rep As New AttendanceRepository
                '    Dim _param = New ParamDTO With {.PERIOD_ID = cboPeriod.SelectedValue, _
                '                                    .STATUS = 0,
                '                                    .ORG_ID = ctrlOrg.CurrentValue,
                '                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}
                '    Dim _filter = New AT_TIME_TIMESHEET_MONTHLYDTO
                '    _filter.PERIOD_ID = cboPeriod.SelectedValue
                '    _filter.ORG_ID = ctrlOrg.CurrentValue
                '    _filter.IS_DISSOLVE = ctrlOrg.IsDissolve
                '    If Not rep.ValidateTimesheet(_filter, "BEYOND_STANDARD") Then ' Không giống công chuẩn
                '        ShowMessage(Translate("Tồn tại nhân viên vượt công chuẩn kỳ công"), NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    If rep.IS_PERIODSTATUS(_param) = False Then
                '        ShowMessage(Translate("Kỳ công đã đóng, hoặc chưa được tạo"), NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    If rep.CLOSEDOPEN_PERIOD(_param) Then
                '        Refresh("UpdateView")
                '        CurrentState = CommonMessage.TOOLBARITEM_DEACTIVE
                '        UpdateControlState()
                '        ' gọi lại hàm này để load lại trạng thái của menu
                '        cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                '    Else
                '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                '        Exit Sub
                '    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo dữ liệu filter grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_MONTHLYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim _param = New ParamDTO With {.ORG_ID = LogHelper.CurrentUser.ORG_ID, _
                                            .IS_DISSOLVE = 0,
                                            .IS_FULL = True}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            'If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
            '    obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            'End If

            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) And Not String.IsNullOrEmpty(cboOjbEmp.SelectedValue) Then

                obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboOjbEmp.SelectedValue))) 'rep.LOAD_PERIODByID(obj)

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    obj.FROM_DATE = ddate.START_DATE
                    obj.END_DATE = ddate.END_DATE
                End If
            End If


            If cboOjbEmp.SelectedValue <> "" Then
                obj.OBJECT_EMPLOYEE_ID = Decimal.Parse(cboOjbEmp.SelectedValue)
            End If
            obj.EMPLOYEE_ID = LogHelper.CurrentUser.EMPLOYEE_ID
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.TIME_TIMESHEET_MONTHLYDTO = rep.GetTimeSheetForEmp_Month(obj, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize, Sorts)
                Else
                    Me.TIME_TIMESHEET_MONTHLYDTO = rep.GetTimeSheetForEmp_Month(obj, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize)
                End If
            Else
                Return rep.GetTimeSheetForEmp_Month(obj, _param).ToTable()
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.TIME_TIMESHEET_MONTHLYDTO
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged khi click cboYear trên ctrlYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged

        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            period.ORG_ID = Decimal.Parse(LogHelper.CurrentUser.ORG_ID)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = 0
                    Dim periodid1 = (From d In dtData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

                    If periodid1 IsNot Nothing Then
                        rdtungay.SelectedDate = periodid1.START_DATE
                        rdDenngay.SelectedDate = periodid1.END_DATE
                    End If
                    'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'Else
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'End If
                End If
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiệnSelectedIndexChanged của control cboPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadStartEndDate()
            'Dim rep As New AttendanceRepository
            'Dim period As New AT_PERIODDTO
            'period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            'period.ORG_ID = 46
            'Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

            'Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
            'rdtungay.SelectedDate = p.START_DATE
            'rdDenngay.SelectedDate = p.END_DATE


            'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
            '                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
            '                                .IS_DISSOLVE = ctrlOrg.IsDissolve}
            'If rep.IS_PERIODSTATUS(_param) Then
            '    Me.MainToolBar = tbarMainToolBar
            '    MainToolBar.Items(2).Enabled = True
            '    MainToolBar.Items(0).Enabled = True
            '    CurrentState = CommonMessage.STATE_NORMAL
            'Else
            '    Me.MainToolBar = tbarMainToolBar
            '    MainToolBar.Items(2).Enabled = False
            '    MainToolBar.Items(0).Enabled = False
            '    CurrentState = CommonMessage.STATE_NORMAL
            'End If
            'ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            'rgData.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
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
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện PageIndexChanged khi change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgData.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgData.CurrentPageIndex = 0
                rgData.Rebind()
                If rgData.Items IsNot Nothing AndAlso rgData.Items.Count > 0 Then
                    rgData.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ButtonCommand khi click yes/no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_CHECK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_CHECK
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            'Select Case CurrentState
            '    Case CommonMessage.STATE_CHECK

            '        If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
            '            ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
            '            Exit Sub
            '        End If
            'End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar trên trang
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub UpdateToolbarState()
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        ChangeToolbarState()
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cboOjbEmp_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboOjbEmp.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadStartEndDate()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    Private Sub LoadStartEndDate()
        If Not IsNumeric(cboPeriod.SelectedIndex) AndAlso cboOjbEmp.SelectedValue = "" Or cboPeriod.SelectedValue = "" Then
            Exit Sub
        End If
        Dim rep As New AttendanceRepository
        Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboOjbEmp.SelectedValue)))
        If ddate IsNot Nothing Then
            rdtungay.SelectedDate = ddate.START_DATE
            rdDenngay.SelectedDate = ddate.END_DATE
        End If
    End Sub
#End Region
End Class
