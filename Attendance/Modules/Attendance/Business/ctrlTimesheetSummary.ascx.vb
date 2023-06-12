Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTimesheetSummary
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

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

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("PERIOD_NAME", GetType(String))
                dt.Columns.Add("PERIOD_ID", GetType(String))
                dt.Columns.Add("WORKING_STANDARD", GetType(String))
                dt.Columns.Add("WORKING_X", GetType(String))
                dt.Columns.Add("WORKING_L", GetType(String))
                dt.Columns.Add("WORKING_P", GetType(String))
                dt.Columns.Add("WORKING_B", GetType(String))
                dt.Columns.Add("WORKING_C", GetType(String))
                dt.Columns.Add("WORKING_R", GetType(String))
                dt.Columns.Add("WORKING_T", GetType(String))
                dt.Columns.Add("WORKING_Q", GetType(String))
                dt.Columns.Add("WORKING_TS", GetType(String))

                dt.Columns.Add("WORKING_O", GetType(String))
                dt.Columns.Add("WORKING_J", GetType(String))
                dt.Columns.Add("WORKING_N", GetType(String))
                dt.Columns.Add("WORKING_K", GetType(String))
                dt.Columns.Add("WORKING_V", GetType(String))
                dt.Columns.Add("WORKING_TN", GetType(String))
                dt.Columns.Add("WORKING_KLD", GetType(String))
                dt.Columns.Add("MIN_LATE", GetType(String))
                dt.Columns.Add("MIN_EARLY", GetType(String))

                dt.Columns.Add("MIN_DEDUCT", GetType(String))
                dt.Columns.Add("WORKING_MEAL", GetType(String))
                dt.Columns.Add("WORKING_A", GetType(String))
                dt.Columns.Add("WORKING_DA", GetType(String))
                dt.Columns.Add("WORKING_ADD", GetType(String))
                dt.Columns.Add("TOTAL_W_SALARY", GetType(String))
                dt.Columns.Add("TOTAL_W_NOSALARY", GetType(String))
                dt.Columns.Add("TOTAL_W_H", GetType(String))
                dt.Columns.Add("WORKING_BHXH", GetType(String))
                dt.Columns.Add("MIN_IN_WORK", GetType(String))
                dt.Columns.Add("MIN_OUT_WORK", GetType(String))
                dt.Columns.Add("SUM_SUPPORT", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
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
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
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
            InitControl()
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
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            MainToolBar.Items(2).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(3).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("LOCK", ToolbarIcons.DeActive,
                                                                     ToolbarAuthorize.Special1, Translate("Khóa")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("UNLOCK", ToolbarIcons.Active,
                                                                     ToolbarAuthorize.Special1, Translate("Mở khóa")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DELETE,
                                                                     ToolbarIcons.Delete,
                                                                     ToolbarAuthorize.Delete,
                                                                     Translate("Xóa")))
            CType(MainToolBar.Items(2), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            CType(MainToolBar.Items(1), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_SPECIAL5
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
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_T", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    'rdtungay.SelectedDate = periodid.START_DATE
                    'rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = 0
                    'Dim periodid1 = (From d In lsData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

                    'If periodid1 IsNot Nothing Then
                    '    rdtungay.SelectedDate = periodid1.START_DATE
                    '    rdDenngay.SelectedDate = periodid1.END_DATE
                    'End If
                    'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'Else
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'End If
                End If
            End If
            Dim dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(cboOjbEmp, dtData, "NAME", "ID")
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
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            If Not IsPostBack Then
                If Not LogHelper.CurrentUser.USERNAME.ToUpper.Equals("ADMIN") Then
                    MainToolBar.Items(6).Visible = False
                End If
            End If
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
    ''' Xử lý sự kiện SelectedNodeChanged cho ctrlOrg
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                ctrlOrg.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgData.CurrentPageIndex = 0
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

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
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARTIEM_CALCULATE
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboOjbEmp.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    'Dim employee_id As Decimal?
                    'For Each items As GridDataItem In rgData.MasterTableView.GetSelectedItems()
                    '    Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                    '    employee_id = Decimal.Parse(item)
                    '    lsEmployee.Add(employee_id)
                    'Next
                    Dim _param = New ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve,
                                                    .FROMDATE = rdtungay.SelectedDate,
                                                    .ENDDATE = rdDenngay.SelectedDate,
                                                    .EMP_OBJ = cboOjbEmp.SelectedValue}
                    If getSE_CASE_CONFIG("ctrlTimesheetSummary_case1") > 0 Then
                        If rep.CAL_TIME_TIMESHEET_MONTHLY(_param, "ctrlTimesheetSummary_case1", lsEmployee) Then
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            Exit Sub
                        End If
                    Else
                        If rep.CAL_TIME_TIMESHEET_MONTHLY(_param, "", lsEmployee) Then
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            Exit Sub
                        End If
                    End If
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "DataTimeSheet")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case TOOLBARITEM_ACTIVE
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New ParamDTO With {.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                    .STATUS = 0,
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}

                    If rep.IS_PERIOD_PAYSTATUS(_param) Then
                        ShowMessage(Translate("Kỳ lương đã đóng, hoặc chưa được tạo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.IS_PERIOD_PAYSTATUS(_param, True) Then
                        ShowMessage(Translate("Kỳ lương tháng trước chưa đóng."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    _param.STATUS = 1
                    'Check tồn tại dữ liệu 
                    'Dim dtDatas As DataTable
                    'dtDatas = CreateDataFilter(True)
                    'If dtDatas.Rows.Count = 0 Then
                    '    ShowMessage(Translate("Chưa tồn tại bảng công tổng hợp trong kỳ công này"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    If rep.CLOSEDOPEN_PERIOD(_param) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.TOOLBARITEM_ACTIVE
                        UpdateControlState()
                        ' gọi lại hàm này để load lại trạng thái của menu
                        cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        Exit Sub
                    End If
                Case TOOLBARITEM_DEACTIVE
                    Dim rep As New AttendanceRepository
                    Dim _param = New ParamDTO With {.PERIOD_ID = cboPeriod.SelectedValue,
                                                    .STATUS = 0,
                                                    .ORG_ID = ctrlOrg.CurrentValue,
                                                    .IS_DISSOLVE = ctrlOrg.IsDissolve}
                    Dim _filter = New AT_TIME_TIMESHEET_MONTHLYDTO
                    _filter.PERIOD_ID = cboPeriod.SelectedValue
                    _filter.ORG_ID = ctrlOrg.CurrentValue
                    _filter.IS_DISSOLVE = ctrlOrg.IsDissolve
                    If Not rep.ValidateTimesheet(_filter, "BEYOND_STANDARD") Then ' Không giống công chuẩn
                        ShowMessage(Translate("Tồn tại nhân viên vượt công chuẩn kỳ công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, hoặc chưa được tạo"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.CLOSEDOPEN_PERIOD(_param) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.TOOLBARITEM_DEACTIVE
                        UpdateControlState()
                        ' gọi lại hàm này để load lại trạng thái của menu
                        cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New AttendanceStoreProcedure
                    Dim dsData As DataSet = store.GET_TIMESHEET_MONTHLY_DATA()
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Attendance\Import\Template_Import_TimeSheet_Monthly.xls",
                                              dsData, Nothing,
                                              "Template_Import_TimeSheet_Monthly" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case "LOCK"
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboOjbEmp.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim List_ID = New List(Of Decimal?)

                    If chkLock_UnLock.Checked = False Then
                        If rgData.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        Else
                            For Each _item As GridDataItem In rgData.SelectedItems
                                List_ID.Add(_item.GetDataKeyValue("ID"))
                            Next
                        End If
                    Else
                        CreateDataFilter(True)
                        If TIME_TIMESHEET_MONTHLYDTO IsNot Nothing AndAlso TIME_TIMESHEET_MONTHLYDTO.Count > 0 Then
                            List_ID = (From d In TIME_TIMESHEET_MONTHLYDTO Select d.ID).ToList
                        End If
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveTIME_TIMESHEET_MONTHLY(List_ID, True) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            ' gọi lại hàm này để load lại trạng thái của menu
                            cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                Case "UNLOCK"
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboOjbEmp.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim List_ID = New List(Of Decimal?)

                    If chkLock_UnLock.Checked = False Then
                        If rgData.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        Else
                            For Each _item As GridDataItem In rgData.SelectedItems
                                List_ID.Add(_item.GetDataKeyValue("ID"))
                            Next
                        End If
                    Else
                        CreateDataFilter(True)
                        If TIME_TIMESHEET_MONTHLYDTO IsNot Nothing AndAlso TIME_TIMESHEET_MONTHLYDTO.Count > 0 Then
                            List_ID = (From d In TIME_TIMESHEET_MONTHLYDTO Select d.ID).ToList
                        End If
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveTIME_TIMESHEET_MONTHLY(List_ID, False) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            ' gọi lại hàm này để load lại trạng thái của menu
                            cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
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
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve,
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
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.TIME_TIMESHEET_MONTHLYDTO = rep.GetTimeSheet(obj, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize, Sorts)
                Else
                    Me.TIME_TIMESHEET_MONTHLYDTO = rep.GetTimeSheet(obj, _param, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize)
                End If
            Else
                TIME_TIMESHEET_MONTHLYDTO = rep.GetTimeSheet(obj, _param)
                Return TIME_TIMESHEET_MONTHLYDTO.ToTable()
                'Return rep.GetTimeSheet(obj, _param).ToTable()
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.TIME_TIMESHEET_MONTHLYDTO
            Dim dtManual = rep.GetAT_TIME_MANUAL(New AT_TIME_MANUALDTO)

            Dim i = 0
            For Each item In rgData.MasterTableView.Columns
                Dim ManualName = (From p In dtManual Where p.CODE.ToUpper = rgData.MasterTableView.Columns(i).HeaderText.ToUpper Select p.NAME_VN).FirstOrDefault
                If ManualName IsNot Nothing Then
                    rgData.MasterTableView.Columns(i).HeaderTooltip = ManualName
                End If
                i += 1
            Next
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
            period.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_T", "PERIOD_ID", True)
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

            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
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
        Dim rep As New AttendanceRepository
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

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""'").CopyToDataTable.Rows
                'If rows("ORG_ID").ToString.ToUpper.Equals("#N/A") OrElse rows("TITLE_ID").ToString.ToUpper.Equals("#N/A") Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                newRow("YEAR") = rows("YEAR")
                newRow("PERIOD_NAME") = rows("PERIOD_NAME")
                newRow("PERIOD_ID") = rows("PERIOD_ID")
                newRow("WORKING_STANDARD") = rows("WORKING_STANDARD")
                newRow("WORKING_X") = rows("WORKING_X")
                newRow("WORKING_L") = rows("WORKING_L")
                newRow("WORKING_P") = rows("WORKING_P")
                newRow("WORKING_B") = rows("WORKING_B")
                newRow("WORKING_C") = rows("WORKING_C")
                newRow("WORKING_R") = rows("WORKING_R")
                newRow("WORKING_T") = rows("WORKING_T")
                newRow("WORKING_Q") = rows("WORKING_Q")
                newRow("WORKING_TS") = rows("WORKING_TS")

                newRow("WORKING_O") = rows("WORKING_O")
                newRow("WORKING_J") = rows("WORKING_J")
                newRow("WORKING_N") = rows("WORKING_N")
                newRow("WORKING_K") = rows("WORKING_K")
                newRow("WORKING_V") = rows("WORKING_V")
                newRow("WORKING_TN") = rows("WORKING_TN")
                newRow("WORKING_KLD") = rows("WORKING_KLD")
                newRow("MIN_LATE") = rows("MIN_LATE")
                newRow("MIN_EARLY") = rows("MIN_EARLY")

                newRow("MIN_DEDUCT") = rows("MIN_DEDUCT")
                newRow("WORKING_MEAL") = rows("WORKING_MEAL")
                newRow("WORKING_A") = rows("WORKING_A")
                newRow("WORKING_DA") = rows("WORKING_DA")
                newRow("WORKING_ADD") = rows("WORKING_ADD")
                newRow("TOTAL_W_SALARY") = rows("TOTAL_W_SALARY")
                newRow("TOTAL_W_NOSALARY") = rows("TOTAL_W_NOSALARY")
                newRow("TOTAL_W_H") = rows("TOTAL_W_H")
                newRow("WORKING_TS") = rows("WORKING_TS")
                newRow("WORKING_BHXH") = rows("WORKING_BHXH")
                newRow("MIN_IN_WORK") = rows("MIN_IN_WORK")
                newRow("MIN_OUT_WORK") = rows("MIN_OUT_WORK")
                newRow("SUM_SUPPORT") = rows("SUM_SUPPORT")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New AttendanceStoreProcedure
                If sp.IMPORT_TIMESHEET_MONTHLY(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
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
            Select Case CurrentState
                Case CommonMessage.STATE_CHECK

                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDel As New List(Of Decimal)
                    For Each item As GridDataItem In rgData.SelectedItems
                        lstDel.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteTimesheetMonthly(lstDel) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select
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
        If cboOjbEmp.SelectedValue = "" Or cboPeriod.SelectedValue = "" Then
            Exit Sub
        End If
        Dim rep As New AttendanceRepository
        Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboOjbEmp.SelectedValue)))
        If ddate IsNot Nothing Then
            rdtungay.SelectedDate = ddate.START_DATE
            rdDenngay.SelectedDate = ddate.END_DATE
        End If
    End Sub


    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Dim rep As New AttendanceRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim lstEmp As New List(Of String)
            dtError = dtData.Clone
            Dim iRow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Chưa nhập công chuẩn"
                ImportValidate.EmptyValue("WORKING_STANDARD", row, rowError, isError, sError)

                sError = "Chưa nhập công thực tế"
                ImportValidate.EmptyValue("WORKING_X", row, rowError, isError, sError)

                sError = "Chưa nhập tổng công mức cũ"
                ImportValidate.EmptyValue("TOTAL_W_SALARY", row, rowError, isError, sError)

                sError = "Chưa nhập tổng công kức mới"
                ImportValidate.EmptyValue("TOTAL_W_NOSALARY", row, rowError, isError, sError)

                sError = "Chưa nhập tổng công tính lương"
                ImportValidate.EmptyValue("TOTAL_W_H", row, rowError, isError, sError)

                If Not IsDBNull(row("PERIOD_ID")) AndAlso Not String.IsNullOrEmpty(row("PERIOD_ID")) Then
                    sError = "Kỳ công đã đóng"
                    Dim checkKicong = rep.CHECK_PERIOD_CLOSE(CDec(row("PERIOD_ID").ToString))
                    If checkKicong = 0 Then
                        ImportValidate.IsValidTime("PERIOD_NAME", row, rowError, isError, sError)
                    End If
                Else
                    sError = "Chưa chọn kỳ công"
                    ImportValidate.IsValidTime("PERIOD_NAME", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("EMPLOYEE_CODE")) Then
                    sError = "Nhân viên không tồn tại"
                    Dim checkEmp = rep.GetEmployeeID(row("EMPLOYEE_CODE"), DateTime.Now)
                    If checkEmp Is Nothing OrElse checkEmp.Rows.Count = 0 Then
                        ImportValidate.IsValidTime("EMPLOYEE_NAME", row, rowError, isError, sError)
                    Else
                        row("EMPLOYEE_ID") = checkEmp.Rows(0)("ID")
                    End If
                End If



                If Not IsDBNull(row("WORKING_STANDARD")) AndAlso Not String.IsNullOrEmpty(row("WORKING_STANDARD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_STANDARD", row, rowError, isError, sError)
                End If

                sError = "Chưa chọn công thực tế"
                ImportValidate.EmptyValue("WORKING_X", row, rowError, isError, sError)

                If Not IsDBNull(row("WORKING_X")) AndAlso Not String.IsNullOrEmpty(row("WORKING_X")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_X", row, rowError, isError, sError)
                End If

                '''''' start

                If Not IsDBNull(row("WORKING_L")) AndAlso Not String.IsNullOrEmpty(row("WORKING_L")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_L", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_P")) AndAlso Not String.IsNullOrEmpty(row("WORKING_P")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_P", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_B")) AndAlso Not String.IsNullOrEmpty(row("WORKING_B")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_B", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_C")) AndAlso Not String.IsNullOrEmpty(row("WORKING_C")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_C", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_R")) AndAlso Not String.IsNullOrEmpty(row("WORKING_R")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_R", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_R")) AndAlso Not String.IsNullOrEmpty(row("WORKING_R")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_R", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_Q")) AndAlso Not String.IsNullOrEmpty(row("WORKING_Q")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_Q", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_TS")) AndAlso Not String.IsNullOrEmpty(row("WORKING_TS")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_TS", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_O")) AndAlso Not String.IsNullOrEmpty(row("WORKING_O")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_O", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_J")) AndAlso Not String.IsNullOrEmpty(row("WORKING_J")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_J", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_N")) AndAlso Not String.IsNullOrEmpty(row("WORKING_N")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_N", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_K")) AndAlso Not String.IsNullOrEmpty(row("WORKING_K")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_K", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_V")) AndAlso Not String.IsNullOrEmpty(row("WORKING_V")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_V", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_TN")) AndAlso Not String.IsNullOrEmpty(row("WORKING_TN")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_TN", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_KLD")) AndAlso Not String.IsNullOrEmpty(row("WORKING_KLD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_KLD", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("MIN_LATE")) AndAlso Not String.IsNullOrEmpty(row("MIN_LATE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("MIN_LATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("MIN_EARLY")) AndAlso Not String.IsNullOrEmpty(row("MIN_EARLY")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("MIN_EARLY", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("MIN_DEDUCT")) AndAlso Not String.IsNullOrEmpty(row("MIN_DEDUCT")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("MIN_DEDUCT", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_MEAL")) AndAlso Not String.IsNullOrEmpty(row("WORKING_MEAL")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_MEAL", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_A")) AndAlso Not String.IsNullOrEmpty(row("WORKING_A")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_A", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_DA")) AndAlso Not String.IsNullOrEmpty(row("WORKING_DA")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_DA", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_ADD")) AndAlso Not String.IsNullOrEmpty(row("WORKING_ADD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_ADD", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TOTAL_W_SALARY")) AndAlso Not String.IsNullOrEmpty(row("TOTAL_W_SALARY")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TOTAL_W_SALARY", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TOTAL_W_NOSALARY")) AndAlso Not String.IsNullOrEmpty(row("TOTAL_W_NOSALARY")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TOTAL_W_NOSALARY", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TOTAL_W_H")) AndAlso Not String.IsNullOrEmpty(row("TOTAL_W_H")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TOTAL_W_H", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("WORKING_BHXH")) AndAlso Not String.IsNullOrEmpty(row("WORKING_BHXH")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("WORKING_BHXH", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("MIN_IN_WORK")) AndAlso Not String.IsNullOrEmpty(row("MIN_IN_WORK")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("MIN_IN_WORK", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("MIN_OUT_WORK")) AndAlso Not String.IsNullOrEmpty(row("MIN_OUT_WORK")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("MIN_OUT_WORK", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("SUM_SUPPORT")) AndAlso Not String.IsNullOrEmpty(row("SUM_SUPPORT")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("SUM_SUPPORT", row, rowError, isError, sError)
                End If

                '''' end




                If isError Then
                    ''rowError("ID") = iRow
                    rowError("STT") = row("STT").ToString
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("TS_MONTHLY_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

#End Region
End Class
