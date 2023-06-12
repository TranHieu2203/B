Imports System.IO
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTime_Timesheet_ot
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            Refresh()
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgTimeTimesheet_ot)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgTimeTimesheet_ot.AllowCustomPaging = True
            rgTimeTimesheet_ot.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            If Not IsPostBack Then
                GirdConfig(rgTimeTimesheet_ot)
            End If
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
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate, ToolbarItem.Export, ToolbarItem.Delete)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_NEXT,
                                                 ToolbarIcons.Export,
                                                 ToolbarAuthorize.Export,
                                                 Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                 ToolbarIcons.Import,
                                                 ToolbarAuthorize.Import,
                                                 Translate("Nhập file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DEACTIVE,
                                                 ToolbarIcons.DeActive,
                                                 ToolbarAuthorize.Special1,
                                                 Translate("Khóa")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_ACTIVE,
                                                 ToolbarIcons.Active,
                                                 ToolbarAuthorize.Special2,
                                                 Translate("Mở khóa")))
            CType(MainToolBar.Items(3), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
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
    ''' Load data for combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim dtData As New DataTable
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
                'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
            dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(cboObjectEmployee, dtData, "NAME", "ID", True)
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
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_ot.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_ot.CurrentPageIndex = 0
                        rgTimeTimesheet_ot.MasterTableView.SortExpressions.Clear()
                        rgTimeTimesheet_ot.Rebind()
                    Case "Cancel"
                        rgTimeTimesheet_ot.MasterTableView.ClearSelectedItems()
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
#End Region

#Region "Event"
    ''' <summary>
    ''' Event selectedNodeChanged sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgTimeTimesheet_ot.CurrentPageIndex = 0
            rgTimeTimesheet_ot.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            ' kiem tra ky cong da dong chua?

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_ACTIVE
                    If rgTimeTimesheet_ot.SelectedItems.Count = 0 And Not chkAll.Checked Then
                        ShowMessage(Translate("Bạn chưa chọn bản ghi nào"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Lock_UnLock("UnLock")

                Case TOOLBARITEM_DEACTIVE
                    If rgTimeTimesheet_ot.SelectedItems.Count = 0 And Not chkAll.Checked Then
                        ShowMessage(Translate("Bạn chưa chọn bản ghi nào"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Lock_UnLock("Lock")
                Case TOOLBARTIEM_CALCULATE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm"), NotifyType.Error)
                        Exit Sub
                    End If
                    If cboObjectEmployee.SelectedValue = "" Then
                        ShowMessage(Translate("Chưa chọn đối tượng nhân viên"), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    If rep.Cal_TimeTImesheet_OT(_param, Decimal.Parse(cboPeriod.SelectedValue),
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, cboObjectEmployee.SelectedValue) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        Exit Sub
                    End If

                    UpdateToolbarState()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgTimeTimesheet_ot.ExportExcel(Server, Response, dtDatas, "TimeSheet_OT")
                        End If
                    End Using
                Case Common.CommonMessage.TOOLBARITEM_NEXT
                    Dim store As New AttendanceStoreProcedure
                    Dim dtVariable As New DataTable
                    Dim tempPath = "~/ReportTemplates//Attendance//Import//Import_OT.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As DataSet
                    dsDanhMuc = store.GET_PERIOD_USER(LogHelper.CurrentUser.USERNAME)

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "Import_OT" & Format(Date.Now, "yyyyMMddHHmmss"), dsDanhMuc, Nothing, Response)
                    End Using
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
                Case Common.CommonMessage.TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgTimeTimesheet_ot.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            ' 
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_TIME_TIMESHEET_OTDTO
        Dim startdate As Date '= CType("01/01/2016", Date)
        Dim enddate As Date '= CType("31/01/2016", Date)
        Try
            'If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
            '    Dim objperiod As New AT_PERIODDTO
            '    objperiod.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            '    _filter.PERIOD_ID = objperiod.PERIOD_ID
            '    Dim ddate = rep.LOAD_PERIODByID(objperiod)
            '    startdate = ddate.START_DATE
            '    enddate = ddate.END_DATE
            'Else
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
            '    Exit Function
            'End If
            If cboPeriod.SelectedValue = "" Then
                ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                Exit Function
            End If

            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) And Not String.IsNullOrEmpty(cboObjectEmployee.SelectedValue) Then
                Dim obj As New AT_PERIODDTO
                obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                _filter.PERIOD_ID = obj.PERIOD_ID
                Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboObjectEmployee.SelectedValue))) 'rep.LOAD_PERIODByID(obj)

                If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                    startdate = ddate.START_DATE
                    enddate = ddate.END_DATE
                    _filter.END_DATE = enddate
                    _filter.START_DATE = startdate

                End If
            End If
            'If Not String.IsNullOrEmpty(cboObjectEmployee.SelectedValue) And _filter.START_DATE IsNot Nothing And _filter.END_DATE IsNot Nothing Then
            '    For i = 1 To 31
            '        If startdate <= enddate Then
            '            rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM")
            '            rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).Visible = True
            '            rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "Removecss3"
            '            If startdate.DayOfWeek = DayOfWeek.Sunday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> CN"
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "css3"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T2"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T3"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T4"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T5"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T6"
            '            ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
            '                rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T7"
            '            End If
            '            startdate = startdate.AddDays(1)
            '        Else
            '            rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).Visible = False
            '        End If
            '    Next
            'Else
            '    For i = 1 To 31
            '        rgTimeTimesheet_ot.MasterTableView.GetColumn("D" & i).Visible = False
            '    Next
            'End If



            SetValueObjectByRadGrid(rgTimeTimesheet_ot, _filter)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue,
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                            .IS_FULL = True}
            Dim Sorts As String = rgTimeTimesheet_ot.MasterTableView.SortExpressions.GetSortString()

            _filter.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            _filter.PAGE_INDEX = rgTimeTimesheet_ot.CurrentPageIndex

            _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            If cboObjectEmployee.SelectedValue <> "" Then
                _filter.EMP_OBJ = cboObjectEmployee.SelectedValue
            Else
                _filter.EMP_OBJ = 0
            End If
            If _filter.PAGE_INDEX = 0 Then
                _filter.PAGE_INDEX = 1
            End If
            Dim ds As DataSet

            If Not isFull Then
                _filter.PAGE_SIZE = rgTimeTimesheet_ot.PageSize
                ds = rep.GetSummaryOT(_filter)
                If ds IsNot Nothing Then
                    Dim tableCct = ds.Tables(0)
                    'If Not IsPostBack Then
                    '    DesignGrid(tableCct)
                    'End If
                    rgTimeTimesheet_ot.VirtualItemCount = Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgTimeTimesheet_ot.DataSource = tableCct
                Else

                    rgTimeTimesheet_ot.DataSource = New DataTable
                End If
            Else
                _filter.PAGE_SIZE = Int32.MaxValue
                ds = rep.GetSummaryOT(_filter)
                Return ds.Tables(0)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgTimeTimesheet_ot.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event chọn năm combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_T", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                'Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                Dim periodid = (From d In dtData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()

                    'rgTimeTimesheet_ot.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
            LoadStartEndDate()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event chọn kỳ công combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            LoadStartEndDate()
            'rgTimeTimesheet_ot.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTimeTimesheet_ot.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgTimeTimesheet_ot.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Request AjaxManager
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgTimeTimesheet_ot.CurrentPageIndex = 0
                rgTimeTimesheet_ot.Rebind()
                If rgTimeTimesheet_ot.Items IsNot Nothing AndAlso rgTimeTimesheet_ot.Items.Count > 0 Then
                    rgTimeTimesheet_ot.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes.No
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_CHECK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_CHECK
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "CUSTOM"
    ''' <summary>
    ''' Update trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_CHECK
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboObjectEmployee.SelectedValue) Then
                        ShowMessage(Translate("Chưa chọn đối tượng nhân viên"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                  .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    If ctrlOrganization.IsDissolve = True Then
                        _param.IS_DISSOLVE = True
                    Else
                        _param.IS_DISSOLVE = False
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgTimeTimesheet_ot.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    If rep.Cal_TimeTImesheet_OT(_param, Decimal.Parse(cboPeriod.SelectedValue),
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, cboObjectEmployee.SelectedValue) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        Exit Sub
                    End If
                    UpdateToolbarState()

                Case CommonMessage.STATE_DELETE
                    Dim lstID = ","
                    If Not chkAll.Checked Then
                        For Each dr As Telerik.Web.UI.GridDataItem In rgTimeTimesheet_ot.SelectedItems
                            Dim ID As New Decimal
                            ID = dr.GetDataKeyValue("ID")
                            lstID = lstID + ID.ToString + ","
                        Next
                    End If
                    If store.Delete_OT(lstID) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
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

    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rNCol As GridNumericColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        Dim rColChkbox As GridCheckBoxColumn
        Dim store As New AttendanceStoreProcedure
        rgTimeTimesheet_ot.MasterTableView.Columns.Clear()
        Dim dtColHide = store.GET_HIDE_COL_TIME_OT()
        For Each column As DataColumn In dt.Columns
            'If ("CONFIRM_OT_TT,TOTAL_DAY_TH,TOTAL_NIGHT_TH,TOTAL_OT_TH,TIMEIN_TT,TIMEOUT_TT").Split(",").Contains(column.ColumnName) Then
            '    Continue For
            'End If
            If ("NEW_OT_HOLIDAY_DAY,NEW_OT_HOLIDAY_NIGHT").Split(",").Contains(column.ColumnName) Then
                Continue For
            End If
            If dtColHide IsNot Nothing AndAlso dtColHide.Rows.Count > 0 Then
                Dim check = (From p In dtColHide Where p("COL_NAME").ToString.ToUpper.Equals(column.ColumnName.ToUpper)).Any
                If check Then
                    Continue For
                End If
            End If

            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgTimeTimesheet_ot.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Equals("ID") AndAlso Not column.ColumnName.Contains("_ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") And Not column.ColumnName.Contains("CHECK") Then
                rCol = New GridBoundColumn()
                rgTimeTimesheet_ot.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.UniqueName = column.ColumnName
                rCol.HeaderText = If(column.ColumnName = "TITLE_NAME", "Vị trí công việc", Translate(column.ColumnName))
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
                If column.ColumnName = "ORG_DESC" OrElse column.ColumnName = "STT" OrElse column.ColumnName = "MODIFIED_BY" OrElse column.ColumnName = "WORK_STATUS" _
                    OrElse column.ColumnName = "IS_DELETED" OrElse column.ColumnName = "STT" Then
                    rCol.Visible = False
                End If
                If column.ColumnName.Contains("TOTAL_") OrElse column.ColumnName.Contains("NUMBER_") OrElse column.ColumnName.Contains("OT_") Then
                    rCol.DataFormatString = "{0:n2}"
                End If
                If column.ColumnName.Contains("OTIS_LOCK") Then
                    rCol.HeaderStyle.Width = 35
                    rCol.FilterControlWidth = 35
                End If
                'If column.ColumnName = "TOTAL_FACTOR1" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 1.0"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR1_5" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 1.5"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR1_8" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 1.8"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR2" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 2"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR2_1" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 2.1"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR2_7" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 2.7"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR3" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 3"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_FACTOR3_9" Then
                '    rCol.HeaderText = "Tổng số giờ làm thêm hệ số 3.9"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_DAY" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm ngày thường"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_NIGHT" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm đêm thường"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_WEEKEND_DAY" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm ngày nghỉ"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_WEEKEND_NIGHT" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm đêm nghỉ"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_HOLIDAY_DAY" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm ngày lễ"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "OT_HOLIDAY_NIGHT" Then
                '    rCol.HeaderText = "Tổng giờ làm thêm đêm lễ"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "NUMBER_FACTOR_CP" Then
                '    rCol.HeaderText = "Tổng giờ nghỉ bù"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB1" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 1.0"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB1_5" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 1.5"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB1_8" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 1.8"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB2" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 2"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB2_1" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 2.1"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB2_7" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 2.7"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB3" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 3"
                '    rCol.DataFormatString = "{0:n2}"
                'ElseIf column.ColumnName = "TOTAL_NB3_9" Then
                '    rCol.HeaderText = "Tổng số giờ nghỉ bù hệ số 3.9"
                '    rCol.DataFormatString = "{0:n2}"
                'End If
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgTimeTimesheet_ot.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.UniqueName = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                If column.ColumnName = "CREATED_DATE" Then
                    rColDate.Visible = False
                End If

            End If
            If column.ColumnName.Contains("CHECK") Then
                rColChkbox = New GridCheckBoxColumn()
                rgTimeTimesheet_ot.MasterTableView.Columns.Add(rColChkbox)
                rColChkbox.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColChkbox.DataField = column.ColumnName
                rColChkbox.HeaderStyle.Width = 150
                rColChkbox.HeaderText = Translate(column.ColumnName)
                rColChkbox.HeaderTooltip = Translate(column.ColumnName)
                rColChkbox.FilterControlToolTip = Translate(column.ColumnName)
                rColChkbox.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                rColChkbox.AllowFiltering = False
                rColChkbox.ShowFilterIcon = False
            End If
        Next
    End Sub


#End Region
    Private Sub Lock_UnLock(ByVal acti As String)
        Try
            Dim lstID = ","
            Dim store As New AttendanceStoreProcedure
            If Not chkAll.Checked Then
                For Each dr As Telerik.Web.UI.GridDataItem In rgTimeTimesheet_ot.SelectedItems
                    Dim ID As New Decimal
                    ID = dr.GetDataKeyValue("ID")
                    lstID = lstID + ID.ToString + ","
                Next
            End If

            If acti = "Lock" Then
                If store.Lock_OT(lstID, CDec(Val(cboPeriod.SelectedValue)), -1) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgTimeTimesheet_ot.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                If store.Lock_OT(lstID, CDec(Val(cboPeriod.SelectedValue)), 0) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgTimeTimesheet_ot.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub LoadStartEndDate()
        If cboObjectEmployee.SelectedValue = "" Or cboPeriod.SelectedValue = "" Then
            Exit Sub
        End If
        Dim rep As New AttendanceRepository
        Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboObjectEmployee.SelectedValue)))
        If ddate IsNot Nothing Then
            rdtungay.SelectedDate = ddate.START_DATE
            rdDenngay.SelectedDate = ddate.END_DATE
        End If
    End Sub
    Private Sub cboObjectEmployee_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboObjectEmployee.SelectedIndexChanged
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

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New AttendanceRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_OT(DocXml, LogHelper.CurrentUser.USERNAME.ToUpper) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgTimeTimesheet_ot.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_OT')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(2).ColumnName = "EMPLOYEE_NAME"
            dtTemp.Columns(3).ColumnName = "AT_YEAR"
            dtTemp.Columns(4).ColumnName = "AT_MONTH"
            dtTemp.Columns(5).ColumnName = "OT_DAY"
            dtTemp.Columns(6).ColumnName = "OT_NIGHT"
            dtTemp.Columns(7).ColumnName = "OT_WEEKEND_DAY"
            dtTemp.Columns(8).ColumnName = "OT_WEEKEND_NIGHT"
            dtTemp.Columns(9).ColumnName = "OT_HOLIDAY_DAY"
            dtTemp.Columns(10).ColumnName = "OT_HOLIDAY_NIGHT"
            dtTemp.Columns(11).ColumnName = "NEW_OT_DAY"
            dtTemp.Columns(12).ColumnName = "NEW_OT_NIGHT"
            dtTemp.Columns(13).ColumnName = "NEW_OT_WEEKEND_DAY"
            dtTemp.Columns(14).ColumnName = "NEW_OT_WEEKEND_NIGHT"
            dtTemp.Columns(15).ColumnName = "NEW_OT_HOLIDAY_DAY"
            dtTemp.Columns(16).ColumnName = "NEW_OT_HOLIDAY_NIGHT"
            dtTemp.Columns(17).ColumnName = "NUMBER_FACTOR_CP"
            dtTemp.Columns(18).ColumnName = "TOTAL_FACTOR1"
            dtTemp.Columns(19).ColumnName = "TOTAL_FACTOR1_5"
            dtTemp.Columns(20).ColumnName = "TOTAL_FACTOR1_8"
            dtTemp.Columns(21).ColumnName = "TOTAL_FACTOR2"
            dtTemp.Columns(22).ColumnName = "TOTAL_FACTOR2_1"
            dtTemp.Columns(23).ColumnName = "TOTAL_FACTOR2_7"
            dtTemp.Columns(24).ColumnName = "TOTAL_FACTOR3"
            dtTemp.Columns(25).ColumnName = "TOTAL_FACTOR3_9"
            dtTemp.Columns(26).ColumnName = "TOTAL_NB1"
            dtTemp.Columns(27).ColumnName = "TOTAL_NB1_5"
            dtTemp.Columns(28).ColumnName = "TOTAL_NB1_8"
            dtTemp.Columns(29).ColumnName = "TOTAL_NB2"
            dtTemp.Columns(30).ColumnName = "TOTAL_NB2_1"
            dtTemp.Columns(31).ColumnName = "TOTAL_NB2_7"
            dtTemp.Columns(32).ColumnName = "TOTAL_NB3"
            dtTemp.Columns(33).ColumnName = "TOTAL_NB3_9"
            dtTemp.Columns(34).ColumnName = "IS_LOCK"
            dtTemp.Columns(35).ColumnName = "ID_PERIOD"
            dtTemp.Columns(36).ColumnName = "COUNT_OT_SUPPORT"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("STT").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New AttendanceStoreProcedure
            Dim lstEmp As New List(Of String)

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                _error = False
                newRow = dtLogs.NewRow
                newRow("STT") = rows("STT")
                'newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

                sError = "Bạn phải nhập mã nhân viên "
                ImportValidate.EmptyValue("EMPLOYEE_CODE", rows, newRow, _error, sError)
                Dim empId = rep.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE"))

                If empId = 0 Then
                    sError = "Mã nhân viên không tồn tại"
                    ImportValidate.IsValidTime("FULLNAME_VN", rows, newRow, _error, sError)
                End If
                sError = "Bạn phải chọn kỳ công"
                ImportValidate.IsValidNumber("ID_PERIOD", rows, newRow, _error, sError)

                'sError = "Sai định dạng số"
                'If rows("OT_DAY") IsNot Nothing Then
                '    ImportValidate.IsValidDate("BIRTH_DATE", rows, newRow, _error, sError)
                'End If
                'ImportValidate.IsValidNumber("OT_DAY", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("OT_NIGHT", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("OT_WEEKEND_DAY", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("OT_WEEKEND_NIGHT", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("OT_HOLIDAY_DAY", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("NEW_OT_HOLIDAY_NIGHT", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("NUMBER_FACTOR_CP", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR1", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR1_5", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR1_8", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR2", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR2_1", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR2_7", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR3", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_FACTOR3_9", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB1", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB1_5", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB1_8", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB2", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB2_1", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB2_7", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB3", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("TOTAL_NB3_9", rows, newRow, _error, sError)
                'ImportValidate.IsValidNumber("IS_LOCK", rows, newRow, _error, sError)
                If _error Then
                    dtLogs.Rows.Add(newRow)
                    _error = False
                End If
            Next
            dtTemp.AcceptChanges()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgTimeTimesheet_ot_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTimeTimesheet_ot.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class