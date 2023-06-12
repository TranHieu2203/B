Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlTimeTimesheet_machine
    Inherits Common.CommonView
    Private store As New CommonProcedureNew
    Public WithEvents AjaxManager As RadAjaxManager
    Public repProGram As New CommonProgramsRepository
    Public clrConverter As New System.Drawing.ColorConverter
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <summary>
    ''' Obj programID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programID As Decimal
        Get
            Return ViewState(Me.ID & "_programID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_programID") = value
        End Set
    End Property
    Public Property strEMPLOYEES As String
        Get
            Return ViewState(Me.ID & "_strEMPLOYEES")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strEMPLOYEES") = value
        End Set
    End Property
    Public Property TIME_TIMESHEET_MACHINETDTO As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Get
            Return ViewState(Me.ID & "_TIME_TIMESHEET_MACHINETDTO")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_MACHINETDTO))
            ViewState(Me.ID & "_TIME_TIMESHEET_MACHINETDTO") = value
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
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("WORKINGDAY", GetType(String))
                dt.Columns.Add("TIMEIN_REALITY", GetType(String))
                dt.Columns.Add("TIMEOUT_REALITY", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj CheckLoad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckLoad As Decimal
        Get
            Return ViewState(Me.ID & "_CheckLoad")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_CheckLoad") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj isLoadedPara
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isLoadedPara As Decimal
        Get
            Return ViewState(Me.ID & "_isLoadedPara")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isLoadedPara") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj listParameters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property listParameters As DataTable
        Get
            Return ViewState(Me.ID & "_listParameters")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_listParameters") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj lstSequence
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstSequence As DataTable
        Get
            Return ViewState(Me.ID & "_lstSequence")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_lstSequence") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isRunAfterComplete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isRunAfterComplete As DataTable
        Get
            Return ViewState(Me.ID & "_isRunAfterComplete")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_isRunAfterComplete") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj requestID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property requestID As Decimal
        Get
            Return ViewState(Me.ID & "_requestID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_requestID") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj isLoadedData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isLoadedData As Decimal
        Get
            Return ViewState(Me.ID & "_isLoadedData")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isLoadedData") = value
        End Set
    End Property
    Property btnEnable As Boolean
        Get
            Return ViewState(Me.ID & "_btnEnable")
        End Get
        Set(value As Boolean)
            ViewState(Me.ID & "_btnEnable") = value
        End Set
    End Property


    Public Property checkRunRequest As Decimal
        Get
            Return ViewState(Me.ID & "_checkRunRequest")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_checkRunRequest") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj lstParameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstParameter As List(Of PARAMETER_DTO)
        Get
            Return ViewState(Me.ID & "_lstParameter")
        End Get
        Set(ByVal value As List(Of PARAMETER_DTO))
            ViewState(Me.ID & "_lstParameter") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj hasError
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property hasError As Decimal
        Get
            Return ViewState(Me.ID & "_hasError")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hasError") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj LoadFirstAfterCal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoadFirstAfterCal As Boolean
        Get
            Return ViewState(Me.ID & "_LoadFirstAfterCal")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_LoadFirstAfterCal") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj lstLabelPara
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstLabelPara As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstLabelPara")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstLabelPara") = value
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
            If programID <> -1 Then
                LoadAllParameterRequest()
            End If
            CheckLoad = New Decimal
            CheckLoad = 1
            isLoadedPara = 0
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
    ''' Khởi tạo control, grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgTimeTimesheet_machine)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgTimeTimesheet_machine.AllowCustomPaging = True
            rgTimeTimesheet_machine.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            If Not IsPostBack Then
                GirdConfig(rgTimeTimesheet_machine)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
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
                'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    'rdtungay.SelectedDate = periodid.START_DATE
                    'rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                    'rdtungay.SelectedDate = lsData.Item(0).START_DATE
                    'rdDenngay.SelectedDate = lsData.Item(0).END_DATE
                End If
            End If

            lsData = rep.Load_Emp_obj()
            FillRadCombobox(cboEmpObj, lsData, "PERIOD_NAME", "ID", True)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load, khởi tạo menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                       ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("LOCK", ToolbarIcons.DeActive,
                                                                     ToolbarAuthorize.Special1, Translate("Khóa")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("UNLOCK", ToolbarIcons.Active,
                                                                     ToolbarAuthorize.Special1, Translate("Mở khóa")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DELETE,
                                                                     ToolbarIcons.Delete,
                                                                     ToolbarAuthorize.Delete,
                                                                     Translate("Xóa")))
            CType(MainToolBar.Items(2), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {"CAL_TIME_TIMESHEET", FrameworkUtilities.OUT_STRING}))
            programID = Int32.Parse(obj(0).ToString())
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
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
                        rgTimeTimesheet_machine.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_machine.CurrentPageIndex = 0
                        rgTimeTimesheet_machine.MasterTableView.SortExpressions.Clear()
                        rgTimeTimesheet_machine.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgTimeTimesheet_machine.MasterTableView.ClearSelectedItems()
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
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien TextChanged cua control RadNumTB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadNumTB_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadNumTB.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            TimerRequest.Interval = Int32.Parse(RadNumTB.Text) * 1000
            If requestID <> 0 Then
                GetStatus()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý load lại các control sau 1 khoảng thời gian nhất định
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub TimerRequest_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRequest.Tick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetStatus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedNodeChanged sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim repS As New AttendanceStoreProcedure
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .S_ORG_ID = strOrgs,
                                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
            End If
            rgTimeTimesheet_machine.CurrentPageIndex = 0
            rgTimeTimesheet_machine.Rebind()
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
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARTIEM_CALCULATE
                    If Not IsNumeric(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Chưa chọn kỳ công"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If Not IsNumeric(cboEmpObj.SelectedValue) Then
                        ShowMessage(Translate("Chưa chọn đối tượng nhân viên"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    'kiem tra xem program nay` co duoc phep chay trong he thong hay khong
                    If Not CheckRunProgram() Then
                        Exit Sub
                    End If
                    'If btnEnable = False Then
                    '    ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    'If Not CheckRunProgram() Then
                    '    Exit Sub
                    'End If
                    strEMPLOYEES = String.Join(",", (From p In rgTimeTimesheet_machine.SelectedItems Select p.GetDataKeyValue("EMPLOYEE_ID")).Distinct.ToArray)
                    isLoadedData = 0
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), System.Drawing.Color)
                    lblRequest.Text = ""
                    CurrentState = STATE_ACTIVE
                    UpdateControlState()
                    checkRunRequest = 1   'Clicked button calculate in toolbar  
                    If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                        Exit Sub
                    End If
                    If IsNumeric(cboPeriod.SelectedValue) Then
                        lblStatus.Text = repProGram.StatusString("Running")
                        GetAllInformationInRequestMain()
                        TimerRequest.Enabled = True
                        LoadFirstAfterCal = True
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOTSELECT_PERIOD), NotifyType.Warning)
                        Exit Sub
                    End If
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgTimeTimesheet_machine.ExportExcel(Server, Response, dtDatas, "DataTimeSheetMachine")
                        End If
                    End Using
                Case "IMPORT_TEMP"
                    ctrlUpload1.Show()
                Case "EXPORT_TEMP"
                    Timesheet_machineExport()
                    ' ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Timesheet_machine&PERIOD_ID=" & "1" & "&orgid=" & "1" & "&IS_DISSOLVE=" & IIf("1", "1", "0") & "');", True)
                    Refresh("UpdateView")
                Case "LOCK"
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboEmpObj.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim List_ID = New List(Of Decimal?)

                    If chkLock_UnLock.Checked = False Then
                        If rgTimeTimesheet_machine.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        Else
                            For Each _item As GridDataItem In rgTimeTimesheet_machine.SelectedItems
                                List_ID.Add(_item.GetDataKeyValue("ID"))
                            Next
                        End If
                    Else
                        CreateDataFilter(True)
                        If TIME_TIMESHEET_MACHINETDTO IsNot Nothing AndAlso TIME_TIMESHEET_MACHINETDTO.Count > 0 Then
                            List_ID = (From d In TIME_TIMESHEET_MACHINETDTO Select d.ID).ToList
                        End If
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveMachines(List_ID, True) Then
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
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(cboEmpObj.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim List_ID = New List(Of Decimal?)

                    If chkLock_UnLock.Checked = False Then
                        If rgTimeTimesheet_machine.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        Else
                            For Each _item As GridDataItem In rgTimeTimesheet_machine.SelectedItems
                                List_ID.Add(_item.GetDataKeyValue("ID"))
                            Next
                        End If
                    Else
                        CreateDataFilter(True)
                        If TIME_TIMESHEET_MACHINETDTO IsNot Nothing AndAlso TIME_TIMESHEET_MACHINETDTO.Count > 0 Then
                            List_ID = (From d In TIME_TIMESHEET_MACHINETDTO Select d.ID).ToList
                        End If
                    End If
                    If List_ID IsNot Nothing AndAlso List_ID.Count > 0 Then
                        If rep.ActiveMachines(List_ID, False) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            ' gọi lại hàm này để load lại trạng thái của menu
                            cboPeriod_SelectedIndexChanged(Nothing, Nothing)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgTimeTimesheet_machine.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Warning)
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
    Private Sub Timesheet_machineExport()
        Try
            ExportTemplate("Attendance\Import\Template_GiaiTrinhNgayCong.xlsx",
                                      New DataSet(), Nothing,
                                      "Template_GiaiTrinhNgayCong" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
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
        Dim obj As New AT_TIME_TIMESHEET_MACHINETDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgTimeTimesheet_machine, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue,
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                            .IS_FULL = True}
            Dim Sorts As String = rgTimeTimesheet_machine.MasterTableView.SortExpressions.GetSortString()
            obj.PERIOD_ID = CDec(Val(cboPeriod.SelectedValue))
            If cboEmpObj.SelectedValue <> "" Then
                obj.EMP_OBJ_ID = CDec(Val(cboEmpObj.SelectedValue))
            End If
            If rdtungay.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            If rtEmployee_Code.Text <> "" Then
                obj.EMPLOYEE_CODE = rtEmployee_Code.Text
            End If
            For Each item In cbFilter.CheckedItems
                If item.Value = 1 Then
                    obj.IS_LATE = True
                End If
                If item.Value = 2 Then
                    obj.IS_EARLY = True
                End If
                If item.Value = 3 Then
                    obj.IS_REALITY = True
                End If
                If item.Value = 4 Then
                    obj.IS_NON_WORKING_VALUE = True
                End If
                If item.Value = 5 Then
                    obj.IS_WRONG_SHIFT = True
                End If
            Next
            Dim LIST_STATUS_SEARCH As New List(Of String)
            If obj.IS_LATE = True Or obj.IS_EARLY = True Then
                LIST_STATUS_SEARCH.Add("DITRE")
                LIST_STATUS_SEARCH.Add("DITRE_VESOM")
            End If
            If obj.IS_REALITY = True Then
                LIST_STATUS_SEARCH.Add("THIEUQT")
            End If
            If obj.IS_NON_WORKING_VALUE = True Then
                LIST_STATUS_SEARCH.Add("KHONGQT")
            End If
            If obj.IS_WRONG_SHIFT = True Then
                LIST_STATUS_SEARCH.Add("SAICA")
            End If
            obj.LIST_STATUS_SEARCH = LIST_STATUS_SEARCH
            If obj.IS_LATE = True And obj.IS_EARLY = True And obj.IS_REALITY = True And obj.IS_NON_WORKING_VALUE = True And obj.IS_WRONG_SHIFT = True Then
                obj.LIST_STATUS_SEARCH = New List(Of String)
            End If
            If obj.FROM_DATE IsNot Nothing And obj.END_DATE IsNot Nothing Then
                If Not isFull Then
                    If Sorts IsNot Nothing Then
                        Me.TIME_TIMESHEET_MACHINETDTO = rep.GetMachines(obj, _param, MaximumRows, rgTimeTimesheet_machine.CurrentPageIndex, rgTimeTimesheet_machine.PageSize, Sorts) '"CREATED_DATE desc")
                    Else
                        Me.TIME_TIMESHEET_MACHINETDTO = rep.GetMachines(obj, _param, MaximumRows, rgTimeTimesheet_machine.CurrentPageIndex, rgTimeTimesheet_machine.PageSize)
                    End If
                Else
                    TIME_TIMESHEET_MACHINETDTO = rep.GetMachines(obj, _param)
                    Return TIME_TIMESHEET_MACHINETDTO.ToTable()
                End If
            Else
                Me.TIME_TIMESHEET_MACHINETDTO = New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
            End If



            rgTimeTimesheet_machine.VirtualItemCount = MaximumRows
            rgTimeTimesheet_machine.DataSource = Me.TIME_TIMESHEET_MACHINETDTO
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
            rgTimeTimesheet_machine.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChange combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim repS As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_T", "PERIOD_ID", True)

            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
            'If dtData.Count > 0 Then
            '    Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            '    If periodid IsNot Nothing Then
            '        cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
            '        rdtungay.SelectedDate = periodid.START_DATE
            '        rdDenngay.SelectedDate = periodid.END_DATE
            '        rgTimeTimesheet_machine.Rebind()
            '    Else
            '        cboPeriod.SelectedIndex = 0
            '        Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
            '        rdtungay.SelectedDate = per.START_DATE
            '        rdDenngay.SelectedDate = per.END_DATE
            '    End If
            'End If
            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .S_ORG_ID = strOrgs,
                                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
            repS = Nothing
            period = Nothing
            startTime = Nothing
            dtData = Nothing
        End Try

    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim repS As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
            'If cboPeriod.SelectedValue <> "" Then
            '    Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

            '    Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
            '    rdtungay.SelectedDate = p.START_DATE
            '    rdDenngay.SelectedDate = p.END_DATE
            'End If
            'rgTimeTimesheet_machine.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .S_ORG_ID = strOrgs,
                                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
            repS = Nothing
            period = Nothing
        End Try

    End Sub
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTimeTimesheet_machine.NeedDataSource
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
    Protected Sub rgTimeTimesheet_machine_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgTimeTimesheet_machine.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Request Ajax
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
                rgTimeTimesheet_machine.CurrentPageIndex = 0
                rgTimeTimesheet_machine.Rebind()
                If rgTimeTimesheet_machine.Items IsNot Nothing AndAlso rgTimeTimesheet_machine.Items.Count > 0 Then
                    rgTimeTimesheet_machine.Items(0).Selected = True
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
    ''' Xử lý sự kiện OkClicked của ctrlUpload1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable = New DataTable()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xlsx")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("DATA") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 2, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            'dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(0)(col)
                If colName IsNot DBNull.Value Then
                    dtDataHeader.Columns(col).ColumnName = colName
                End If
            Next
            dtDataHeader.Rows.RemoveAt(0)
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If isRow Then
                        dtData.ImportRow(row)
                    End If
                Next
            Next
            If loadToGrid(dtDataHeader) = False Then
            Else
                Dim lstobjUdp As New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
                Dim objUdp As AT_TIME_TIMESHEET_MACHINETDTO
                For Each row As DataRow In dtData.Rows
                    objUdp = New AT_TIME_TIMESHEET_MACHINETDTO
                    objUdp.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                    Dim working_day As Date
                    Dim strdate = row("WORKINGDAY").ToString.Split("/")
                    Dim strDateTimeIn = row("TIMEIN_REALITY").ToString().Split(":")
                    Dim strDateTimeOut = row("TIMEOUT_REALITY").ToString().Split(":")
                    If strdate.Length <> 3 Then Continue For
                    working_day = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)))
                    objUdp.WORKINGDAY = working_day
                    If strDateTimeIn.Length = 2 Then
                        objUdp.TIMEIN_REALITY = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)), CDec(strDateTimeIn(0)), CDec(strDateTimeIn(1)), 0)
                    End If
                    If strDateTimeOut.Length = 2 Then
                        objUdp.TIMEOUT_REALITY = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)), CDec(strDateTimeOut(0)), CDec(strDateTimeOut(1)), 0)
                    End If
                    objUdp.NOTE = row("NOTE")
                    lstobjUdp.Add(objUdp)
                Next
                Using rep As New AttendanceRepository
                    If rep.IMPORT_TIMESHEET_MACHINE(lstobjUdp) Then
                        Refresh("InsertView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                End Using
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        Finally
            dsDataPrepare.Dispose()
            dtDataHeader.Dispose()
        End Try
    End Sub
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

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
#End Region
#Region "FUNCTION/PROCEDURE"
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load các tham số được truyền lên
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetListParameters() As List(Of PARAMETER_DTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        lstParameter = New List(Of PARAMETER_DTO)
        'Dim newRequest As New REQUEST_DTO
        'Dim index As Decimal = 0
        Try
            Dim newParameter As New PARAMETER_DTO
            Dim value As String = ""
            newParameter.PARAMETER_NAME = "User name"
            newParameter.SEQUENCE = lstSequence.Rows(0)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "USERNAME"
            value = LogHelper.GetUserLog.Username
            newParameter.VALUE = value
            lstParameter.Add(newParameter)

            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "ORG_ID"
            newParameter.SEQUENCE = lstSequence.Rows(1)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "ORG_ID"
            'value = Decimal.Parse(ctrlOrganization.CurrentValue)
            newParameter.VALUE = Decimal.Parse(ctrlOrganization.CurrentValue)
            lstParameter.Add(newParameter)
            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "PERIOD_ID"
            newParameter.SEQUENCE = lstSequence.Rows(2)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "PERIOD_ID"
            'value = Decimal.Parse(ctrlOrganization.CurrentValue)
            newParameter.VALUE = Decimal.Parse(cboPeriod.SelectedValue)
            lstParameter.Add(newParameter)

            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "ISDISSOLVE"
            newParameter.SEQUENCE = lstSequence.Rows(3)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "ISDISSOLVE"
            If ctrlOrganization.IsDissolve = True Then
                newParameter.VALUE = 1
            Else
                newParameter.VALUE = 0
            End If
            lstParameter.Add(newParameter)

            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "DELETE_ALL"
            newParameter.SEQUENCE = lstSequence.Rows(4)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 0
            newParameter.CODE = "DELETE_ALL"
            newParameter.VALUE = 1
            lstParameter.Add(newParameter)

            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "OBJ_EMP_ID"
            newParameter.SEQUENCE = lstSequence.Rows(5)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "OBJ_EMP_ID"
            newParameter.VALUE = CDec(Val(cboEmpObj.SelectedValue))
            lstParameter.Add(newParameter)
            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "FROM_DATE"
            newParameter.SEQUENCE = lstSequence.Rows(6)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "FROM_DATE"
            Dim vDate As Date = CDate(rdtungay.SelectedDate)
            newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
            lstParameter.Add(newParameter)
            newParameter = Nothing
            newParameter = New PARAMETER_DTO
            'Dim value As String = ""
            newParameter.PARAMETER_NAME = "TO_DATE"
            newParameter.SEQUENCE = lstSequence.Rows(7)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "TO_DATE"
            vDate = CDate(rdDenngay.SelectedDate)
            newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
            lstParameter.Add(newParameter)

            newParameter = Nothing
            newParameter = New PARAMETER_DTO()
            newParameter.PARAMETER_NAME = "EMPLIST"
            newParameter.SEQUENCE = lstSequence.Rows(8)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 0
            newParameter.CODE = "EMPLIST"
            newParameter.VALUE = strEMPLOYEES
            lstParameter.Add(newParameter)

            newParameter = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return lstParameter
    End Function
    ''' <summary>
    ''' hàm tạo List Parameter để chuyền vào StoreProcedure
    ''' sử dụng: New With{.[Tên tham số trong Store1] = [Value1],
    '''                   .[Tên tham số trong Store2] = [Value2]}
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function CreateParameterList(Of T)(ByVal parameters As T) As List(Of List(Of Object))
        Dim lstParameter As New List(Of List(Of Object))
        For Each info As PropertyInfo In parameters.GetType().GetProperties()
            Dim param As New List(Of Object)
            param.Add(info.Name)
            param.Add(info.GetValue(parameters, Nothing))
            lstParameter.Add(param)
        Next
        Return lstParameter
    End Function
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load các tham số được truyền lên
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetListParametersValue() As List(Of Object)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rs As New List(Of Object)
        hasError = 0
        Dim index As Decimal = 0
        Dim str As String = ""
        Dim typefield As String = ""
        Try
            rs.Add(requestID)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return rs
    End Function

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc insert vào database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAllInformationInRequestMain()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log = UserLogHelper.GetCurrentLogUser()
            lblStatus.Text = repProGram.StatusString("Initial")
            Dim repH As New HistaffFrameworkRepository
            Dim newRequest As New REQUEST_DTO
            lstParameter = New List(Of PARAMETER_DTO)
            Dim index As Decimal = 0
            newRequest.PROGRAM_ID = programID
            newRequest.PHASE_CODE = "I"
            newRequest.STATUS_CODE = "Initial"
            newRequest.START_DATE = DateTime.Now
            newRequest.END_DATE = DateTime.Now.AddDays(100)
            newRequest.ACTUAL_START_DATE = DateTime.Now
            newRequest.ACTUAL_COMPLETE_DATE = DateTime.Now.AddDays(100)
            newRequest.CREATED_BY = log.Username
            newRequest.CREATED_DATE = DateTime.Now
            newRequest.MODIFIED_BY = log.Username
            newRequest.MODIFIED_DATE = DateTime.Now
            newRequest.CREATED_LOG = log.Ip + "-" + log.ComputerName
            newRequest.MODIFIED_LOG = log.Ip + "-" + log.ComputerName

            'Get program with programId
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = FrameworkUtilities.OUT_CURSOR})
            'get all Function(Report) in system
            Dim dt = repH.ExecuteStore("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_ID", lstlst)


            'get list parameter
            lstParameter = GetListParameters()

            'call function insert into database with request and parameters
            requestID = New Decimal
            requestID = (New CommonProgramsRepository).Insert_Requests(newRequest, dt, lstParameter, 1)
            lblRequest.Text = requestID.ToString
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    Public Function checkPara() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstParameter = New List(Of PARAMETER_DTO)
            lstParameter = GetListParameters()
            For index = 0 To lstSequence.Rows.Count - 1
                If lstParameter(index).IS_REQUIRE = 1 Then
                    If lstParameter(index).VALUE Is Nothing OrElse lstParameter(index).VALUE = "" Then
                        If lstLabelPara IsNot Nothing AndAlso lstLabelPara(index) IsNot Nothing Then
                            ShowMessage("Điều kiện" & lstLabelPara(index).ToUpper() & "bắt buộc nhập", NotifyType.Warning)
                        End If
                        CurrentState = STATE_NORMAL
                        UpdateControlState()
                        Return False
                    End If
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return True
    End Function
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham kiem tra 1 chuong + user co duoc phep chay trong he thong hay khong
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CheckRunProgram() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Kiem tra: neu program nay chi chay 1 luc duoc 1 cai (ISOLATION_PROGRAM : 1) thi exit
            '          neu program chay duoc dong` thoi --> kiem tra: (ISOLATION_USER : 1) bao' da~ co' user chay program nay roi
            Dim log = UserLogHelper.GetCurrentLogUser()
            Dim RunProgramCheck = repProGram.CheckRunProgram(programID, log.Username)
            Select Case RunProgramCheck
                Case 0 'duoc phep chay --> do anything
                    Return True
                Case 1 'khong duoc phep chay --> (program nay chi chay cung luc 1 cai')
                    ShowMessage("Chương trình này đã được chạy trong hệ thống. " & vbNewLine & "Vui lòng kiểm tra trong danh sách chương trình đang chạy", NotifyType.Warning)
                    Return False
                Case 2 'khong duoc phep chay (program nay chi chay khi khac user chay )     
                    ShowMessage("Tài khoản đang được sử dụng, vui lòng đăng nhập bằng tài khoản khác để tính công", NotifyType.Warning)
                    Return False
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Nhận và show dữ liệu sau khi xử lý
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RunResult()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgTimeTimesheet_machine.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc show ra kết quả
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Run()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If isRunAfterComplete(0).Item("IS_RUN_AFTER_COMPLETE") = 0 Then 'khong cho chay lien`
                Exit Sub
            Else 'chay lien`
                isLoadedData = 0
                RunResult()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc ẩn hiện các control theo status
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRR_GET_STATUS_IN_REQUEST", New List(Of Object)(New Object() {requestID, FrameworkUtilities.OUT_STRING}))
            Select Case obj(0).ToString
                Case "P"
                    lblStatus.Text = repProGram.StatusString("Pending")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_PENDING), System.Drawing.Color)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                Case "R"
                    lblStatus.Text = repProGram.StatusString("Running")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_RUNNING), System.Drawing.Color)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                Case "C"
                    lblStatus.Text = repProGram.StatusString("Complete")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE), System.Drawing.Color)
                    TimerRequest.Enabled = False
                    CurrentState = STATE_DETAIL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    Run()
                Case "CW"
                    lblStatus.Text = repProGram.StatusString("CompleteWarning")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), System.Drawing.Color)
                    TimerRequest.Enabled = False
                    CurrentState = STATE_DEACTIVE
                Case "CPE" 'lỗi phần tham số truyền vào
                    lblStatus.Text = repProGram.StatusString("ParameterError")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), System.Drawing.Color)
                    TimerRequest.Enabled = False
                    Dim objLog As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE", New List(Of Object)(New Object() {requestID, FrameworkUtilities.OUT_STRING}))
                    Dim logDoc As String = objLog(0).ToString()
                    ShowMessage(logDoc, Framework.UI.Utilities.NotifyType.Warning)
                    CurrentState = STATE_DEACTIVE
                Case "E"
                    lblStatus.Text = repProGram.StatusString("Error")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_ERROR), System.Drawing.Color)
                    TimerRequest.Enabled = False
                    CurrentState = STATE_DEACTIVE
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            rep.Dispose()
        End Try
    End Sub
    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load các tham số được truyền 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAllParameterRequest()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New HistaffFrameworkRepositoryBase
            Dim index As Decimal = 0
            listParameters = New DataTable
            lstSequence = New DataTable
            isRunAfterComplete = New DataTable
            listParameters = (New CommonProgramsRepository).GetAllParameters(programID)
            lstSequence = listParameters.DefaultView.ToTable(True, "SEQUENCE")
            isRunAfterComplete = listParameters.DefaultView.ToTable(True, "IS_RUN_AFTER_COMPLETE")
            'load all parameter but no value

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Function loadToGrid(dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim objImport As New AT_TIME_TIMESHEET_MACHINETDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow

                If row("EMPLOYEE_CODE") Is DBNull.Value OrElse row("EMPLOYEE_CODE") = "" Then
                    sError = "Chưa nhập mã nhân viên"
                    ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                End If
                If row("NOTE") Is DBNull.Value OrElse row("NOTE") = "" Then
                    sError = "Chưa nhập lý do giải trình"
                    ImportValidate.EmptyValue("NOTE", row, rowError, isError, sError)
                End If
                If row("WORKINGDAY") Is DBNull.Value OrElse row("WORKINGDAY") = "" Then
                    sError = "Chưa nhập ngày giải trình"
                    ImportValidate.IsValidTime("WORKINGDAY", row, rowError, isError, sError)
                Else
                    If Not IsDate(row("WORKINGDAY")) Then
                        sError = "Ngày giải '" + row("WORKINGDAY") + "'  trình chưa đúng định dạng"
                        ImportValidate.IsValidTime("WORKINGDAY", row, rowError, isError, sError)
                    End If
                End If
                If (row("TIMEIN_REALITY") Is DBNull.Value OrElse row("TIMEIN_REALITY") = "") AndAlso (row("TIMEOUT_REALITY") Is DBNull.Value OrElse row("TIMEOUT_REALITY") = "") Then
                    sError = "Chưa nhập giờ vào giải trình"
                    ImportValidate.EmptyValue("TIMEIN_REALITY", row, rowError, isError, sError)
                    sError = "Chưa nhập giờ ra giải trình"
                    ImportValidate.EmptyValue("TIMEOUT_REALITY", row, rowError, isError, sError)
                End If
                If isError Then
                    rowError("STT") = row("STT")
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    objImport.EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString
                    objImport.WORKINGDAY = CDate(row("WORKINGDAY"))
                    If rep.CheckExistAT_LATE_COMBACKOUT(objImport) Then
                        rowError("STT") = row("STT")
                        If rowError("EMPLOYEE_CODE").ToString = "" Then
                            rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                            sError = "Ngày giải '" + row("WORKINGDAY") + "' trình bị trùng"
                            rowError("WORKINGDAY") = sError
                        End If
                        dtError.Rows.Add(rowError)
                    End If
                End If
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                dtDataHeader.TableName = "DATA_HEADER"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                dsData.Tables.Add(dtDataHeader)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_GiaiTrinhNgayCong_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
#End Region

    Private Sub cboEmpObj_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmpObj.SelectedIndexChanged
        Try
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim rep = New AttendanceRepository
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgTimeTimesheet_machine_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTimeTimesheet_machine.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

            For Each item As GridDataItem In rgTimeTimesheet_machine.Items
                Dim status = item("STATUS_NAME").Text
                If status <> "" Then
                    item("STATUS_NAME").ForeColor = Color.Red
                End If
                Dim shiftType = item("SHIFT_TYPE_CODE").Text
                If shiftType = "&nbsp;" Then
                    item("SHIFT_TYPE_CODE").BackColor = Color.FromName("#fd5454")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDel As New List(Of Decimal)
                    For Each item As GridDataItem In rgTimeTimesheet_machine.SelectedItems
                        lstDel.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteTimesheetMachinet(lstDel) Then
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
End Class