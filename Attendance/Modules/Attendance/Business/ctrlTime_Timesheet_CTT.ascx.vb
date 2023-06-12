Imports System.Drawing
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
Public Class ctrlTime_Timesheet_CTT
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public repProGram As New CommonProgramsRepository
    Public clrConverter As New System.Drawing.ColorConverter
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("STAFF_RANK_NAME", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))
                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))
                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D25", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))

                dt.Columns.Add("D1_COLOR", GetType(String))
                dt.Columns.Add("D2_COLOR", GetType(String))
                dt.Columns.Add("D3_COLOR", GetType(String))
                dt.Columns.Add("D4_COLOR", GetType(String))
                dt.Columns.Add("D5_COLOR", GetType(String))
                dt.Columns.Add("D6_COLOR", GetType(String))
                dt.Columns.Add("D7_COLOR", GetType(String))
                dt.Columns.Add("D8_COLOR", GetType(String))
                dt.Columns.Add("D9_COLOR", GetType(String))
                dt.Columns.Add("D10_COLOR", GetType(String))
                dt.Columns.Add("D11_COLOR", GetType(String))
                dt.Columns.Add("D12_COLOR", GetType(String))
                dt.Columns.Add("D13_COLOR", GetType(String))
                dt.Columns.Add("D14_COLOR", GetType(String))
                dt.Columns.Add("D15_COLOR", GetType(String))
                dt.Columns.Add("D16_COLOR", GetType(String))
                dt.Columns.Add("D17_COLOR", GetType(String))
                dt.Columns.Add("D18_COLOR", GetType(String))
                dt.Columns.Add("D19_COLOR", GetType(String))
                dt.Columns.Add("D20_COLOR", GetType(String))
                dt.Columns.Add("D21_COLOR", GetType(String))
                dt.Columns.Add("D22_COLOR", GetType(String))
                dt.Columns.Add("D23_COLOR", GetType(String))
                dt.Columns.Add("D24_COLOR", GetType(String))
                dt.Columns.Add("D25_COLOR", GetType(String))
                dt.Columns.Add("D26_COLOR", GetType(String))
                dt.Columns.Add("D27_COLOR", GetType(String))
                dt.Columns.Add("D28_COLOR", GetType(String))
                dt.Columns.Add("D29_COLOR", GetType(String))
                dt.Columns.Add("D30_COLOR", GetType(String))
                dt.Columns.Add("D31_COLOR", GetType(String))

                dt.Columns.Add("DESCRIPTION", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property dtError As DataTable
        Get
            Return ViewState(Me.ID & "_dtError")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_dtError") = value
        End Set
    End Property

    Public Property TIME_TIMESHEET_DAILYDTO As List(Of AT_TIME_TIMESHEET_DAILYDTO)
        Get
            Return ViewState(Me.ID & "_TIME_TIMESHEET_DAILYDTO")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_DAILYDTO))
            ViewState(Me.ID & "_TIME_TIMESHEET_DAILYDTO") = value
        End Set
    End Property
    Public Property dtTIME_TIMESHEET As DataSet
        Get
            Return ViewState(Me.ID & "_dtTIME_TIMESHEET")
        End Get
        Set(ByVal value As DataSet)
            ViewState(Me.ID & "_dtTIME_TIMESHEET") = value
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
            rgTimeTimesheet_cct.SetFilter()
            rgTimeTimesheet_cct.AllowCustomPaging = True
            rgTimeTimesheet_cct.ClientSettings.EnablePostBackOnRowClick = False
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
    ''' Khởi tạo control, popup
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar

            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Export)
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            'MainToolBar.Items(0).Text = Translate("Tổng hợp")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Export,
                                                                  Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_ORIGIN",
            '                                                      ToolbarIcons.Export,
            '                                                      ToolbarAuthorize.Export,
            '                                                      Translate("Xuất bảng công gốc")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("CALCULATE",
                                                                  ToolbarIcons.Calculator,
                                                                  ToolbarAuthorize.Special1,
                                                                  Translate("Tổng hợp")))
            CType(MainToolBar.Items(1), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {"CAL_TIMESHEET_DAILY", FrameworkUtilities.OUT_STRING}))
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
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
            table = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(cboObjectEmployee, table, "NAME", "ID", True)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' REload page
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
                        rgTimeTimesheet_cct.Rebind()
                        'SelectedItemDataGridByKey(rgDeclareiTimeRice, IDSelect, , rgDeclareiTimeRice.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_cct.CurrentPageIndex = 0
                        rgTimeTimesheet_cct.MasterTableView.SortExpressions.Clear()
                        rgTimeTimesheet_cct.Rebind()
                        'SelectedItemDataGridByKey(rgDeclareiTimeRice, IDSelect, )
                    Case "Cancel"
                        rgTimeTimesheet_cct.MasterTableView.ClearSelectedItems()
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
    ''' <lastupdate>16/08/2017</lastupdate>
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
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event selectedNodeChange Sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            Else
                If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                    Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                    .S_ORG_ID = strOrgs,
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    'hien tai bo rang buoc trong du an tng 
                    'issue TNG(-456)
                    '[Chấm công > Nghiệp vụ > Xử lý dữ liệu chấm công] Chọn kỳ công tháng 9/2019, nút Tổng hợp và xuất excel bị disable. Xem clip đính kèm
                    'Dim btnEnable As Boolean = False
                    'btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                    'CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
                    'CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
                    btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                End If

                rgTimeTimesheet_cct.CurrentPageIndex = 0
                rgTimeTimesheet_cct.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click ok popup import file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim xls As New ExcelCommon
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable
        Dim dtError As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORTSHIFTCTT") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 4, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataHeader.Rows.RemoveAt(0)
            dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(0)(col)
                dtDataHeader.Columns(col).ColumnName = colName
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
                dtError = saveGrid()
                If dtError.Rows.Count > 0 Then
                    dtError.TableName = "DATA"
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importTimesheet_CTT_Error1')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu
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
                                            .PERIOD_ID = Decimal.Parse(IIf(cboPeriod.SelectedValue = "", -1, cboPeriod.SelectedValue)),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            ' kiem tra ky cong da dong chua?

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARTIEM_CALCULATE
                    If ctrlOrganization.IsDissolve = True Then
                        _param.IS_DISSOLVE = True
                    Else
                        _param.IS_DISSOLVE = False
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If Not rdtungay.SelectedDate.HasValue Or Not rdDenngay.SelectedDate.HasValue Then
                        ShowMessage(Translate("Từ ngày đến ngày chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgTimeTimesheet_cct.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    Dim is_delete As Decimal = 0

                    If chkSummary.Checked Then
                        is_delete = 1
                    Else
                        is_delete = 0
                    End If
                    If is_delete = 1 Then
                        ctrlMessageBox.MessageText = Translate("Tất cả dữ liệu sẽ được tạo mới lại bao gồm cả dữ liệu được nhập từ excel, Bạn có tiếp tục?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        If Not CheckRunProgram() Then
                            Exit Sub
                        End If
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
                        'If getSE_CASE_CONFIG("ctrlTime_Timesheet_CTT") > 0 Then
                        '    rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                        '                               Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                        '    Refresh("UpdateView")
                        'Else
                        '    rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                        '                               Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                        '    Refresh("UpdateView")
                        'End If
                    End If
                Case TOOLBARITEM_DELETE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgTimeTimesheet_cct.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "IMPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()
                Case "EXPORT_TEMP"
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
                        Exit Sub
                    End If
                    If Not IsNumeric(cboObjectEmployee.SelectedValue) Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được chọn"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim dtDatas As DataTable
                    dtDatas = CreateDataFilter(True)
                    Session("EXPORTTIMESHEETDAILY") = dtDatas
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Time_TimeSheetCCT&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & ctrlOrganization.CurrentValue & "&objEmp=" & CDec(Val(cboObjectEmployee.SelectedValue)) & "&IS_DISSOLVE=" & IIf(ctrlOrganization.IsDissolve, "1", "0") & "')", True)
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgTimeTimesheet_cct.ExportExcel(Server, Response, dtDatas, "Time_Timesheet_CTT")
                        End If
                    End Using
                Case "EXPORT_ORIGIN"
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = GetDataOrigin()
                        If dtData.Rows.Count > 0 Then
                            rgTimeTimesheet_cct.ExportExcel(Server, Response, dtData, "Timesheet_Origin")
                        End If
                    End Using
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
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_TIME_TIMESHEET_DAILYDTO
        Dim startdate As Date '= CType("01/01/2016", Date)
        Dim enddate As Date '= CType("31/01/2016", Date)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                'Dim year As String = cboYear.SelectedValue.ToString
                'If (rdtungay.SelectedDate IsNot Nothing) Then
                '    startdate = rdtungay.SelectedDate
                'Else
                '    If (year IsNot Nothing) Then
                '        startdate = CType("01/01/" & year, Date)
                '        rdtungay.SelectedDate = startdate
                '    End If
                'End If
                'If (rdDenngay.SelectedDate IsNot Nothing) Then
                '    enddate = rdDenngay.SelectedDate
                'Else
                '    If (year IsNot Nothing) Then
                '        If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                '            enddate = CType("31/01/" & year, Date)
                '        Else
                '            enddate = CType("01/31/" & year, Date)
                '        End If
                '        rdDenngay.SelectedDate = enddate
                '    End If
                'End If
                If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) And Not String.IsNullOrEmpty(cboObjectEmployee.SelectedValue) Then
                    Dim obj As New AT_PERIODDTO
                    obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                    _filter.PERIOD_ID = obj.PERIOD_ID
                    Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboObjectEmployee.SelectedValue))) 'rep.LOAD_PERIODByID(obj)

                    If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                        startdate = ddate.START_DATE
                        enddate = ddate.END_DATE
                        _filter.END_DATE = enddate
                        _filter.FROM_DATE = startdate
                    End If
                End If
                If Not String.IsNullOrEmpty(cboObjectEmployee.SelectedValue) And _filter.FROM_DATE IsNot Nothing And _filter.END_DATE IsNot Nothing Then
                    For i = 1 To 31
                        If startdate <= enddate Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM")
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).Visible = True
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "Removecss3"
                            If startdate.DayOfWeek = DayOfWeek.Sunday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> CN"
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "css3"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T2"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T3"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T4"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T5"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T6"
                            ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                                rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T7"
                            End If
                            startdate = startdate.AddDays(1)
                        Else
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).Visible = False
                        End If
                    Next
                Else
                    For i = 1 To 31
                        rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).Visible = False
                    Next
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Try
                SetValueObjectByRadGrid(rgTimeTimesheet_cct, _filter)
                Dim Sorts As String = rgTimeTimesheet_cct.MasterTableView.SortExpressions.GetSortString()
                If cboPeriod.SelectedValue <> "" Then
                    _filter.PERIOD_ID = cboPeriod.SelectedValue
                End If
                If cboObjectEmployee.SelectedValue <> "" Then
                    _filter.EMP_OBJ = cboObjectEmployee.SelectedValue
                Else
                    _filter.EMP_OBJ = 0
                End If
                _filter.PAGE_INDEX = rgTimeTimesheet_cct.CurrentPageIndex + 1
                _filter.PAGE_SIZE = rgTimeTimesheet_cct.PageSize
                _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                _filter.IS_DISSOLVE = ctrlOrganization.IsDissolve
                'muon field de gan clear cache
                _filter.IS_TERMINATE = ckClearCache.Checked
                ' _filter.IS_IMPORT = ckimport.Checked
            Catch ex As Exception
                Throw ex
            End Try
            Dim ds = rep.GetCCT(_filter)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0).Columns.Contains("D29") Then
                Dim newColumn As New Data.DataColumn("D29", GetType(System.String))
                newColumn.DefaultValue = ""
                ds.Tables(0).Columns.Add(newColumn)
            End If
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0).Columns.Contains("D30") Then
                Dim newColumn As New Data.DataColumn("D30", GetType(System.String))
                newColumn.DefaultValue = ""
                ds.Tables(0).Columns.Add(newColumn)
            End If
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0).Columns.Contains("D31") Then
                Dim newColumn As New Data.DataColumn("D31", GetType(System.String))
                newColumn.DefaultValue = ""
                ds.Tables(0).Columns.Add(newColumn)
            End If
            If Not isFull Then
                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Count > 0 Then
                    Dim tableCct = ds.Tables(0)
                    rgTimeTimesheet_cct.VirtualItemCount = Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgTimeTimesheet_cct.DataSource = tableCct
                Else
                    rgTimeTimesheet_cct.VirtualItemCount = 0
                    rgTimeTimesheet_cct.DataSource = New DataTable
                End If
            Else
                _filter.PAGE_INDEX = 1
                _filter.PAGE_SIZE = Integer.MaxValue
                Return rep.GetCCT(_filter).Tables(0)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load data cho table để set datasource cho grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataOrigin() As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_DAILYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetValueObjectByRadGrid(rgTimeTimesheet_cct, obj)

            Dim Sorts As String = rgTimeTimesheet_cct.MasterTableView.SortExpressions.GetSortString()

            If cboPeriod.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriod.SelectedValue
            End If
            If rdtungay.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            obj.IS_DISSOLVE = ctrlOrganization.IsDissolve

            Dim ds = rep.GetCCT_Origin(obj)
            Return ds
        Catch ex As Exception
            Throw ex
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
            rgTimeTimesheet_cct.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox Năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            LoadStartEndDate()
            'If dtData.Count > 0 Then
            '    Dim periodid = (From d In dtData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
            '    If periodid IsNot Nothing Then
            '        cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
            '        rdtungay.SelectedDate = periodid.START_DATE
            '        rdDenngay.SelectedDate = periodid.END_DATE
            '    Else
            '        cboPeriod.SelectedIndex = 0
            '        rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.ToString(), Date)
            '        rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.ToString(), Date)
            '    End If

            '    If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
            '        Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
            '        Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
            '                                        .S_ORG_ID = strOrgs,
            '                                        .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
            '                                        .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            '        Dim btnEnable As Boolean = False
            '        btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
            '        CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
            '        CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
            '    End If
            '    'rgTimeTimesheet_cct.Rebind()
            'Else
            '    ClearControlValue(rdtungay, rdDenngay)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
        Try
            If cboPeriod.SelectedValue <> "" Then
                LoadStartEndDate()
                'Dim p = (From o In Me.PERIOD Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                'If p IsNot Nothing Then
                '    rdtungay.SelectedDate = p.START_DATE
                '    rdDenngay.SelectedDate = p.END_DATE
                'Else
                '    Dim dtData As List(Of AT_PERIODDTO)
                '    Dim rep As New AttendanceRepository
                '    Dim period As New AT_PERIODDTO
                '    period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                '    period.YEAR = Decimal.Parse(cboYear.SelectedValue)
                '    dtData = rep.LOAD_PERIODBylinq(period)

                '    Dim pnot = (From o In dtData Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                '    If pnot IsNot Nothing Then
                '        rdtungay.SelectedDate = pnot.START_DATE
                '        rdDenngay.SelectedDate = pnot.END_DATE
                '    End If
                'End If
            End If

            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .S_ORG_ID = strOrgs,
                                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                Dim btnEnable As Boolean = False
                btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
                CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
            End If

            'rgTimeTimesheet_cct.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload datasource for grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTimeTimesheet_cct.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsPostBack Then
                CreateDataFilter(False)
            Else
                Dim dt As DataTable = New DataTable("data")
                dt.Columns.Add("EMPLOYEE_ID", GetType(Integer))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("OBJECT_ATTENDANCE_NAME", GetType(String))
                'dt.Columns.Add("TOTAL_DAY_SAL", GetType(String))
                'dt.Columns.Add("TOTAL_DAY_NON_SAL", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))
                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))
                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))

                rgTimeTimesheet_cct.DataSource = dt
                rgTimeTimesheet_cct.VirtualItemCount = 0
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Set color for column grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTimeTimesheet_cct_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTimeTimesheet_cct.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim rowView = CType(e.Item.DataItem, DataRowView)
            '    Dim dataItem As GridDataItem = e.Item
            '    Dim i As Integer = 1
            '    While True
            '        Dim str = "D" & i
            '        If rowView.Row.Table.Columns.Contains(str) Then
            '            If rowView.Row(str & "_COLOR") Is DBNull.Value Then
            '                i = i + 1
            '                Continue While
            '            End If
            '            If String.IsNullOrEmpty(rowView.Row(str & "_COLOR")) Then
            '                i = i + 1
            '                Continue While
            '            End If
            '            Select Case rowView.Row(str & "_COLOR")
            '                Case 1
            '                    dataItem(str).BackColor = Drawing.Color.LightBlue
            '                Case 2
            '                    dataItem(str).BackColor = Drawing.Color.DarkGreen
            '                Case 3
            '                    dataItem(str).BackColor = Drawing.Color.Yellow
            '                Case 4
            '                    dataItem(str).BackColor = Drawing.Color.Red
            '            End Select
            '        Else
            '            Exit While
            '        End If
            '        i = i + 1
            '    End While
            'End If
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            'End If
            Dim dtManual = rep.GetAT_TIME_MANUAL(New AT_TIME_MANUALDTO)
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim rowView = CType(e.Item.DataItem, DataRowView)
                Dim dataItem As GridDataItem = e.Item
                Dim i As Integer = 1
                While True
                    Dim str = "D" & i
                    If rowView.Row.Table.Columns.Contains(str) Then
                        Try
                            Dim _CodeKH = rowView.Row(str)

                            Dim ManualName = (From p In dtManual Where p.CODE_KH.ToUpper = _CodeKH.ToUpper Select p.NAME_VN).FirstOrDefault
                            dataItem(str).ToolTip = ManualName
                            If String.IsNullOrEmpty(_CodeKH) OrElse _CodeKH = " " OrElse _CodeKH = "K" Then
                                dataItem(str).BackColor = Color.Red
                            End If
                        Catch ex As Exception
                        End Try

                    Else
                        Exit While
                    End If
                    i = i + 1
                End While
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Request ajaxmanager
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
                rgTimeTimesheet_cct.CurrentPageIndex = 0
                rgTimeTimesheet_cct.Rebind()
                If rgTimeTimesheet_cct.Items IsNot Nothing AndAlso rgTimeTimesheet_cct.Items.Count > 0 Then
                    rgTimeTimesheet_cct.Items(0).Selected = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event click cancle popup import
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "CUSTOM"
    ''' <summary>
    ''' Save data from grid to DB when import
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function saveGrid() As DataTable
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim startdate As Date '= CType("01/01/2016", Date)
        Dim enddate As Date '= CType("31/01/2016", Date)
        Try
            Dim year As String = cboYear.SelectedValue.ToString
            If (rdtungay.SelectedDate IsNot Nothing) Then
                startdate = rdtungay.SelectedDate
            Else
                If (year IsNot Nothing) Then
                    startdate = CType("01/01/" & year, Date)
                    rdtungay.SelectedDate = startdate
                End If
            End If
            If (rdDenngay.SelectedDate IsNot Nothing) Then
                enddate = rdDenngay.SelectedDate
            Else
                If (year IsNot Nothing) Then
                    If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                        enddate = CType("31/01/" & year, Date)
                    Else
                        enddate = CType("01/31/" & year, Date)
                    End If
                    rdDenngay.SelectedDate = enddate
                End If
            End If
            dtData.TableName = "Data"
            Dim dtImport = CreateDataImport()
            For i As Integer = 0 To dtData.Rows.Count - 1
                Dim row As DataRow = dtData.Rows(i)
                dtImport.ImportRow(row)
                AssignDateToTable(dtImport, i, startdate, enddate)
            Next
            dtImport.TableName = "DATA"
            Dim dt = rep.InsertLeaveSheetDaily(dtImport, cboPeriod.SelectedValue, cboObjectEmployee.SelectedValue)
            Return dt
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load data from file import to grid
    ''' </summary>
    ''' <param name="dtDataHeader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid(ByVal dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim dtDatas As AT_PERIODDTO
        Dim rep As New AttendanceRepository
        Dim reps As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim dtDataImportEmployee As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim irow = 6
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            dtError.Columns.Add("STT")
            period.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            dtDatas = rep.Load_date(period.PERIOD_ID, Decimal.Parse(cboObjectEmployee.SelectedValue)) ' rep.LOAD_PERIODByID(period)
            Dim dtManual = rep.GetAT_TIME_MANUAL(New AT_TIME_MANUALDTO)
            For Each row As DataRow In dtData.Rows
                Dim leavesDaysP As Decimal = 0
                Dim leavesDaysNB As Decimal = 0
                Dim dtSourceEntitlement As New DataTable()
                Dim rowD As String = ""
                Dim leaveFrom As New DateTime
                Dim leaveFromNB As New DateTime
                Dim balance As Decimal = 0
                Dim cur_have As Decimal = 0
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    irow += 1
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Mã nhân viên không phải hệ thống chiết xuất"
                ImportValidate.IsValidNumber("EMPLOYEE_ID", row, rowError, isError, sError)
                If rowError("EMPLOYEE_ID").ToString <> "" Then
                    rowError("EMPLOYEE_CODE") = sError
                End If

                For index = 1 To (dtDatas.END_DATE - dtDatas.START_DATE).Value.TotalDays + 1
                    If row("D" & index).ToString <> "" Then
                        Dim r = row("D" & index).ToString
                        Dim exists As New AT_TIME_MANUALDTO
                        Try
                            exists = (From p In dtManual Where p.CODE_KH.ToUpper = r.ToUpper).FirstOrDefault
                        Catch ex As Exception
                            exists = Nothing
                        End Try
                        If exists.CODE1 = "P" Or exists.CODE2 = "P" Then  ' r = "P"
                            rowD = "D" & index.ToString
                            leaveFrom = Convert.ToDateTime(index.ToString + "/" + dtDatas.START_DATE.Value.Month.ToString + "/" + dtDatas.START_DATE.Value.Year.ToString)
                            dtSourceEntitlement = rep.GET_INFO_PHEPNAM_IMPORT_CTT(row("EMPLOYEE_ID"), leaveFrom)
                            If dtSourceEntitlement.Rows.Count > 0 Then
                                balance = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
                            End If
                        End If

                        If exists.CODE1 = "B" Or exists.CODE2 = "B" Then ' r = "NB"
                            rowD = "D" & index.ToString
                            leaveFromNB = Convert.ToDateTime(index.ToString + "/" + dtDatas.START_DATE.Value.Month.ToString + "/" + dtDatas.START_DATE.Value.Year.ToString)
                            Dim dtSourceNB = rep.GET_INFO_PHEPNAM_IMPORT_CTT(row("EMPLOYEE_ID"), leaveFromNB)
                            If dtSourceNB.Rows.Count > 0 Then
                                cur_have = If(dtSourceNB.Rows(0)("PHEP_CONLAI_B") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("PHEP_CONLAI_B").ToString()))
                            End If
                        End If
                        If exists Is Nothing Then
                            rowError("D" & index) = row("D" & index).ToString & " không tồn tại"
                            isError = True
                        Else
                            row("D" & index) = exists.ID
                            If exists.CODE1 = "P" Then
                                leavesDaysP = leavesDaysP + 0.5
                            End If
                            If exists.CODE2 = "P" Then
                                leavesDaysP = leavesDaysP + 0.5
                            End If

                            If exists.CODE1 = "B" Then
                                leavesDaysNB = leavesDaysNB + 0.5
                            End If
                            If exists.CODE2 = "B" Then
                                leavesDaysNB = leavesDaysNB + 0.5
                            End If
                            'ElseIf exists.CODE = "B" Or exists.CODE_KH = "NB" Then
                            '    leavesDaysNB = leavesDaysNB + 1
                            'End If
                        End If
                    End If

                Next
                row("DESCRIPTION") = ""
                If leavesDaysP > balance Then
                    sError = "Số ngày đăng ký nghỉ lớn hơn Quỹ phép năm còn lại, vui lòng điều chỉnh lại dữ liệu"
                    row("DESCRIPTION") = row("DESCRIPTION") & vbCrLf & sError
                    isError = True
                End If

                If leavesDaysNB > cur_have Then
                    sError = "Số ngày đăng ký nghỉ lớn hơn Phép bù còn lại, vui lòng điều chỉnh lại dữ liệu"
                    row("DESCRIPTION") = row("DESCRIPTION") & vbCrLf & sError
                    isError = True
                End If
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("DESCRIPTION") = row("DESCRIPTION").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow += 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importTimesheet_CTT_Error&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & ctrlOrganization.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrganization.IsDissolve, "1", "0") & "')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load, khởi tạo popup sơ đồ tổ chức
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim xls As New ExcelCommon
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                    If Not CheckRunProgram() Then
                        Exit Sub
                    End If
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
                    'Dim lsEmployee As New List(Of Decimal?)
                    'Dim employee_id As Decimal?
                    'For Each items As GridDataItem In rgTimeTimesheet_cct.MasterTableView.GetSelectedItems()
                    '    Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                    '    employee_id = Decimal.Parse(item)
                    '    lsEmployee.Add(employee_id)
                    'Next
                    'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                    '                                .PERIOD_ID = Decimal.Parse(IIf(cboPeriod.SelectedValue = "", -1, cboPeriod.SelectedValue)),
                    '                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    'Dim is_delete As Decimal = 0

                    'If chkSummary.Checked Then
                    '    is_delete = 1
                    'Else
                    '    is_delete = 0
                    'End If
                    'If is_delete = 1 Then
                    '    ctrlMessageBox.MessageText = Translate(CommonMessage.CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM)
                    '    ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                    '    ctrlMessageBox.DataBind()
                    '    ctrlMessageBox.Show()
                    'End If
                    'If getSE_CASE_CONFIG("ctrlTimeTimesheet_machine_case1") > 0 Then
                    '    rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                    '                               Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                    '    Refresh("UpdateView")
                    'Else
                    '    rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                    '                               Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                    '    Refresh("UpdateView")
                    'End If
                    'Return
            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
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
    ''' Update trạng thái toolbar
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

    Private Function CreateDataImport() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("EMPLOYEE_ID", GetType(String))
        dt.Columns.Add("D1", GetType(String))
        dt.Columns.Add("D2", GetType(String))
        dt.Columns.Add("D3", GetType(String))
        dt.Columns.Add("D4", GetType(String))
        dt.Columns.Add("D5", GetType(String))
        dt.Columns.Add("D6", GetType(String))
        dt.Columns.Add("D7", GetType(String))
        dt.Columns.Add("D8", GetType(String))
        dt.Columns.Add("D9", GetType(String))
        dt.Columns.Add("D10", GetType(String))
        dt.Columns.Add("D11", GetType(String))
        dt.Columns.Add("D12", GetType(String))
        dt.Columns.Add("D13", GetType(String))
        dt.Columns.Add("D14", GetType(String))
        dt.Columns.Add("D15", GetType(String))
        dt.Columns.Add("D16", GetType(String))
        dt.Columns.Add("D17", GetType(String))
        dt.Columns.Add("D18", GetType(String))
        dt.Columns.Add("D19", GetType(String))
        dt.Columns.Add("D20", GetType(String))
        dt.Columns.Add("D21", GetType(String))
        dt.Columns.Add("D22", GetType(String))
        dt.Columns.Add("D23", GetType(String))
        dt.Columns.Add("D24", GetType(String))
        dt.Columns.Add("D25", GetType(String))
        dt.Columns.Add("D26", GetType(String))
        dt.Columns.Add("D27", GetType(String))
        dt.Columns.Add("D28", GetType(String))
        dt.Columns.Add("D29", GetType(String))
        dt.Columns.Add("D30", GetType(String))
        dt.Columns.Add("D31", GetType(String))
        dt.Columns.Add("DA1", GetType(String))
        dt.Columns.Add("DA2", GetType(String))
        dt.Columns.Add("DA3", GetType(String))
        dt.Columns.Add("DA4", GetType(String))
        dt.Columns.Add("DA5", GetType(String))
        dt.Columns.Add("DA6", GetType(String))
        dt.Columns.Add("DA7", GetType(String))
        dt.Columns.Add("DA8", GetType(String))
        dt.Columns.Add("DA9", GetType(String))
        dt.Columns.Add("DA10", GetType(String))
        dt.Columns.Add("DA11", GetType(String))
        dt.Columns.Add("DA12", GetType(String))
        dt.Columns.Add("DA13", GetType(String))
        dt.Columns.Add("DA14", GetType(String))
        dt.Columns.Add("DA15", GetType(String))
        dt.Columns.Add("DA16", GetType(String))
        dt.Columns.Add("DA17", GetType(String))
        dt.Columns.Add("DA18", GetType(String))
        dt.Columns.Add("DA19", GetType(String))
        dt.Columns.Add("DA20", GetType(String))
        dt.Columns.Add("DA21", GetType(String))
        dt.Columns.Add("DA22", GetType(String))
        dt.Columns.Add("DA23", GetType(String))
        dt.Columns.Add("DA24", GetType(String))
        dt.Columns.Add("DA25", GetType(String))
        dt.Columns.Add("DA26", GetType(String))
        dt.Columns.Add("DA27", GetType(String))
        dt.Columns.Add("DA28", GetType(String))
        dt.Columns.Add("DA29", GetType(String))
        dt.Columns.Add("DA30", GetType(String))
        dt.Columns.Add("DA31", GetType(String))
        Return dt
    End Function

    Private Sub AssignDateToTable(ByVal dt As DataTable, ByVal row_num As Integer, ByVal startdate As Date, ByVal enddate As Date)
        Try
            dt.Rows(row_num)("DA1") = String.Format("{0:dd/MM/yyyy}", startdate)
            For i = 2 To 31
                startdate = startdate.AddDays(1)
                If startdate <= enddate Then
                    dt.Rows(row_num)("DA" + i.ToString() + "") = String.Format("{0:dd/MM/yyyy}", startdate)
                Else
                    dt.Rows(row_num)("DA" + i.ToString() + "") = String.Empty
                End If
            Next
        Catch ex As Exception
            Throw ex
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
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "DELETE_ALL"
            newParameter.VALUE = 1
            lstParameter.Add(newParameter)
            newParameter = Nothing

            newParameter = New PARAMETER_DTO
            newParameter.PARAMETER_NAME = "EMPLOYEE_OBJECT"
            newParameter.SEQUENCE = lstSequence.Rows(5)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "EMPLOYEE_OBJECT"
            value = cboObjectEmployee.SelectedValue
            newParameter.VALUE = value
            lstParameter.Add(newParameter)
            newParameter = Nothing

            newParameter = New PARAMETER_DTO
            newParameter.PARAMETER_NAME = "IS_CAL_TIMETIMESHEET"
            newParameter.SEQUENCE = lstSequence.Rows(6)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "IS_CAL_TIMETIMESHEET"
            value = CDec(chkSummary.Checked)
            newParameter.VALUE = value
            lstParameter.Add(newParameter)
            newParameter = Nothing

            newParameter = New PARAMETER_DTO
            newParameter.PARAMETER_NAME = "IS_EXCEL"
            newParameter.SEQUENCE = lstSequence.Rows(7)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "IS_EXCEL"
            value = CDec(ckimport.Checked)
            newParameter.VALUE = value
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
                        ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
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
            rgTimeTimesheet_cct.Rebind()
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

#End Region
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
End Class