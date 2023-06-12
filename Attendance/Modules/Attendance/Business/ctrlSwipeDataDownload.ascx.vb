Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Reflection
Imports System.Web.Script.Serialization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports ComboBoxDataDTO = Attendance.AttendanceBusiness.ComboBoxDataDTO

Public Class ctrlSwipeDataDownload
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
    Const TOTAL_ROW_IMPORT As Integer = 100
    Dim log As New CommonBusiness.UserLog
    Dim psp As New AttendanceStoreProcedure
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Dim lst As New List(Of AT_TERMINALSDTO)
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' AT_Terminal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_Terminal As List(Of AT_TERMINALSDTO)
        Get
            Return ViewState(Me.ID & "_Termidal")
        End Get
        Set(ByVal value As List(Of AT_TERMINALSDTO))
            ViewState(Me.ID & "_Termidal") = value
        End Set
    End Property
    ''' <summary>
    ''' danh sach du lieu cham cong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SWIPE_DATA As List(Of AT_SWIPE_DATADTO)
        Get
            Return ViewState(Me.ID & "_AT_SWIPE_DATADTO")
        End Get
        Set(ByVal value As List(Of AT_SWIPE_DATADTO))
            ViewState(Me.ID & "_AT_SWIPE_DATADTO") = value
        End Set
    End Property
    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set

    End Property
    Private JSONDATA As List(Of DATA_IN)
    Private DATA_IN As DataTable
    Dim dsDataComper As New DataTable
    ''' <summary>
    ''' Danh sach du lieu cham cong
    ''' </summary>
    ''' <remarks></remarks>
    Dim ls_AT_SWIPE_DATADTO As New List(Of AT_SWIPE_DATADTO)
    Dim mv_IP As String = ""
    ''' <summary>
    ''' Tao DataTable luu tru du lieu cham cong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                'dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("ITIME_ID", GetType(String))
                'dt.Columns.Add("TERMINAL_ID", GetType(String))
                'dt.Columns.Add("TERMINAL_NAME", GetType(String))
                dt.Columns.Add("VALTIME", GetType(String))
                dt.Columns.Add("WORKINGDAY", GetType(String))
                'dt.Columns.Add("ORG_CHECK_IN_NAME", GetType(String))

                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Public Property lstCheckType As List(Of OT_OTHERLIST_DTO)
        Get
            Return ViewState(Me.ID & "_lstCheckType")
        End Get
        Set(ByVal value As List(Of OT_OTHERLIST_DTO))
            ViewState(Me.ID & "_lstCheckType") = value
        End Set
    End Property

    Public Property programID As Decimal
        Get
            Return ViewState(Me.ID & "_programID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_programID") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rglSwipeDataDownload.SetFilter()
            rglSwipeDataDownload.AllowCustomPaging = True

            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            UpdateControlState()
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
    ''' Ghi de phuong thuc khoi tao cac control tren trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            log = LogHelper.GetUserLog
            InitControl()
            If Not IsPostBack Then
                'ViewConfig(RadPane1)
                GirdConfig(rglSwipeDataDownload)
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
    ''' Phuong thuc khoi tao, thiet lap cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Dim rep As New HistaffFrameworkRepository
            Common.Common.BuildToolbar(Me.MainToolBar,
                                        ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("DOWNLOADDATA",
                                                                     ToolbarIcons.Calculator,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Tải dữ liệu")))

            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT",
            '                                                        ToolbarIcons.Import,
            '                                                        ToolbarAuthorize.Import,
            '                                                        Translate("Nhập file")))
            CType(MainToolBar.Items(1), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {"CAL_TIME_TIMESHEET", FrameworkUtilities.OUT_STRING}))
            programID = Int32.Parse(obj(0).ToString())
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
    ''' Cap nhat trang thai cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'EnabledGridNotPostback(rglSwipeDataDownload, True)
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
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

    Public Overrides Sub BindData()
        Dim dtdata As DataTable
        Dim dt_Terminal As DataTable
        Try
            Using rep As New AttendanceRepository
                'dtdata = rep.GetOtherList("TIME_RECORDER", True)
                'If dtdata IsNot Nothing AndAlso dtdata.Rows.Count Then
                '    FillRadCombobox(cbMachine_Type, dtdata, "NAME", "ID")
                'End If

                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_ATTENDANCE_CHECKIN_TYPE = True
                rep.GetComboboxData(ListComboData)
                Me.lstCheckType = ListComboData.LIST_LIST_ATTENDANCE_CHECKIN_TYPE
                FillDropDownList(cboTimekeepingType, ListComboData.LIST_LIST_ATTENDANCE_CHECKIN_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            End Using

            'dt_Terminal = psp.GET_AT_TERMINALS()
            'If dt_Terminal.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt_Terminal.Rows
            '        Dim itm As New AT_TERMINALSDTO
            '        itm.ID = dr("ID")
            '        itm.TERMINAL_CODE = dr("TERMINAL_CODE").ToString
            '        itm.TERMINAL_NAME = dr("TERMINAL_NAME").ToString
            '        itm.TERMINAL_IP = dr("TERMINAL_IP").ToString
            '        itm.PASS = dr("PASS").ToString
            '        itm.PORT = dr("PORT").ToString

            '        lst.Add(itm)
            '    Next
            'End If
            'Session("LST_TERMINALS") = lst

            'If dt_Terminal IsNot Nothing AndAlso dt_Terminal.Rows.Count Then
            '    FillRadCombobox(cboTerminal, dt_Terminal, "TERMINAL_NAME", "ID")
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang theo tung trang thai
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeDataDownload.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeDataDownload.CurrentPageIndex = 0
                        rglSwipeDataDownload.MasterTableView.SortExpressions.Clear()
                        rglSwipeDataDownload.Rebind()
                    Case "Cancel"
                        rglSwipeDataDownload.MasterTableView.ClearSelectedItems()
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
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            rglSwipeDataDownload.CurrentPageIndex = 0
            rglSwipeDataDownload.MasterTableView.SortExpressions.Clear()
            rglSwipeDataDownload.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tao du lieu filter cho rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SWIPE_DATADTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrganization.CurrentValue IsNot Nothing Then
                obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            Else
                rglSwipeDataDownload.DataSource = New List(Of AT_SWIPE_DATADTO)
                Exit Function
            End If
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rglSwipeDataDownload, obj)
            obj.FROM_DATE = rdStartDate.SelectedDate
            obj.END_DATE = rdEndDate.SelectedDate
            If IsNumeric(cbMachine_Type.SelectedValue) Then
                obj.MACHINE_TYPE = cbMachine_Type.SelectedValue
            End If
            If cboTerminal.CheckedItems.Count > 0 Then
                obj.PLACE_NAME_FILTER = New List(Of String)
                For Each item In cboTerminal.CheckedItems
                    obj.PLACE_NAME_FILTER.Add(item.Text)
                Next
            End If
            If cboTimekeepingType.CheckedItems.Count > 0 Then
                obj.MACHINE_TYPES = New List(Of Decimal)
                For Each item In cboTimekeepingType.CheckedItems
                    obj.MACHINE_TYPES.Add(item.Value)
                Next
            End If
            obj.ITIME_ID = hidempid1.Value

            obj.USERNAME = LogHelper.CurrentUser.USERNAME

            Dim Sorts As String = rglSwipeDataDownload.MasterTableView.SortExpressions.GetSortString()
            If Not IsDate(rdStartDate.SelectedDate) And Not IsDate(rdEndDate.SelectedDate) Then
                Me.SWIPE_DATA = New List(Of AT_SWIPE_DATADTO)
            Else
                If Not isFull Then
                    If Sorts IsNot Nothing Then
                        Me.SWIPE_DATA = rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows, Sorts)
                    Else
                        Me.SWIPE_DATA = rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows)
                    End If
                Else
                    Return rep.GetSwipeData(obj).ToTable()
                End If
            End If
            rglSwipeDataDownload.VirtualItemCount = MaximumRows
            rglSwipeDataDownload.DataSource = Me.SWIPE_DATA
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
    ''' Xu ly su kien NeedDataSource cho rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rglSwipeDataDownload_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rglSwipeDataDownload.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If IsPostBack Then
                If rdStartDate.SelectedDate Is Nothing AndAlso rdEndDate.SelectedDate Is Nothing Then
                    ShowMessage(Translate("Vui lập nhập Từ ngày và Đến ngày"), NotifyType.Warning)
                    RefreshGrid()
                    Exit Sub
                End If
                If rdStartDate.SelectedDate Is Nothing Then
                    ShowMessage(Translate("Vui lập nhập Từ ngày"), NotifyType.Warning)
                    RefreshGrid()
                    Exit Sub
                End If
                If rdEndDate.SelectedDate Is Nothing Then
                    ShowMessage(Translate("Vui lập nhập Đến ngày"), NotifyType.Warning)
                    RefreshGrid()
                    Exit Sub
                End If
                If cboTimekeepingType.CheckedItems.Count = 0 Then
                    ShowMessage(Translate("Vui lòng chọn Loại chấm công"), NotifyType.Warning)
                    RefreshGrid()
                    Exit Sub
                End If
                ' Dim code = (From p In Me.lstCheckType Where p.ID = cboTimekeepingType.CheckedItems(0).Value).FirstOrDefault
                ' If ((cboTimekeepingType.CheckedItems.Count = 1 AndAlso code.CODE <> "MCC_CHECKIN") OrElse cboTimekeepingType.CheckedItems.Count > 1) AndAlso cboTerminal.CheckedItems.Count = 0 Then
                    ' ShowMessage(Translate("Vui lòng chọn Chấm công tại"), NotifyType.Warning)
                    ' RefreshGrid()
                    ' Exit Sub
                ' End If
            End If
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Command khi click item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rglSwipeDataDownload.ExportExcel(Server, Response, dtData, "SwipeDataDownload")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    Dim rep As New AttendanceRepository
                    Session("SWIPE_DATA_EXPORT") = rep.GET_SWIPE_DATA_IMPORT(1, False)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSwipeData')", True)
                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
                Case "IMPORT"
                    If IsNumeric(cbMachine_Type.SelectedValue) Then
                        ctrlUpload1.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_CHOOSE_MACHINE_TYPE), NotifyType.Warning)
                        Exit Sub
                    End If
                Case "DOWNLOADDATA"
                    'Dim tempStart = rdStartDate.SelectedDate
                    'Dim tempNext = rdStartDate.SelectedDate
                    'Dim _Terminal = cboTerminal.SelectedValue
                    'If cboTerminal.SelectedValue = "" Then
                    '    ShowMessage(Translate("Bạn phải chọn máy chấm công để tải dữ liệu"), NotifyType.Warning)
                    '    Return
                    'End If
                    If IsDate(rdStartDate.SelectedDate) AndAlso IsDate(rdEndDate.SelectedDate) Then
                        Dim subtr = rdEndDate.SelectedDate - rdStartDate.SelectedDate
                        If subtr.Value.Days + 1 > 31 Then
                            ShowMessage(Translate("Khoảng thời gian tải dữ liệu quẹt thẻ không quá 31 ngày"), NotifyType.Warning)
                            Return
                        End If
                        'Dim terID As Decimal = IIf(IsNumeric(cboTerminal.SelectedValue), CDec(cboTerminal.SelectedValue), -1)

                        'Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                        'Do While 1
                        '    tempNext = DateAdd("d", 10, tempStart)
                        '    If tempNext < rdEndDate.SelectedDate Then
                        '        IAttendance.ReadCheckInOutData(tempStart, tempNext, terID)
                        '        tempStart = DateAdd("d", 1, tempNext)
                        '    Else
                        '        tempNext = rdEndDate.SelectedDate
                        '        IAttendance.ReadCheckInOutData(tempStart, tempNext, terID)
                        '        Exit Do
                        '    End If
                        'Loop
                        Dim fromDate = rdStartDate.SelectedDate.Value
                        Dim toDate = rdEndDate.SelectedDate.Value.AddDays(1).AddTicks(-1)

                        Dim _uri = ConfigurationManager.AppSettings("Uri")
                        Dim _api = ConfigurationManager.AppSettings("ApiUrl")
                        _api = _api & "?fromDate=" & fromDate.ToString("yyyy-MM-dd") & "&toDate=" & toDate.ToString("yyyy-MM-dd")
                        Dim strResult = GetInOutData(_uri, _api)
                        Dim dJson = JObject.Parse(strResult)
                        Dim jData = dJson.Properties().Where(Function(f) f.Name = "responseData").FirstOrDefault()
                        Dim dtData As New DataTable
                        dtData = JsonConvert.DeserializeObject(Of DataTable)(jData.Value.ToString)
                        'Dim dtFilter = dtData.AsEnumerable().Where(Function(f) f.Field(Of DateTime)("CheckTime") >= fromDate AndAlso f.Field(Of DateTime)("CheckTime") <= toDate).CopyToDataTable()
                        'Dim dtFilter = dtData.Select("CheckTime >=#" + fromDate.ToString("MM/dd/yyyy HH:mm:ss") + "# AND CheckTime <=#" + toDate.ToString("MM/dd/yyyy HH:mm:ss") + "#").CopyToDataTable
                        dtData.TableName = "Data"
                        Dim writer As New System.IO.StringWriter()
                        dtData.WriteXml(writer, XmlWriteMode.IgnoreSchema, False)
                        Dim docXML = writer.ToString()
                        Dim rep As New AttendanceRepository
                        If rep.IMPORT_INOUT(docXML, fromDate, toDate) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rglSwipeDataDownload.Rebind()
                            'Tambt BCG-908 23/12/2022 Tự động tổng hợp công sau khi get data inout
                            Dim lstObjEmp = rep.Load_Emp_obj()
                            Dim dtParam = (New CommonProgramsRepository).GetAllParameters(programID)
                            Dim dtSequence = dtParam.DefaultView.ToTable(True, "SEQUENCE")
                            For Each item In lstObjEmp
                                Dim periodObj = rep.GetperiodByEmpObj(item.ID, rdEndDate.SelectedDate)
                                If periodObj IsNot Nothing Then
                                    Dim lstParameter = New List(Of PARAMETER_DTO)
                                    Dim newParameter As New PARAMETER_DTO
                                    Dim value As String = ""
                                    newParameter.PARAMETER_NAME = "User name"
                                    newParameter.SEQUENCE = dtSequence.Rows(0)("SEQUENCE")
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
                                    newParameter.SEQUENCE = dtSequence.Rows(1)("SEQUENCE")
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
                                    newParameter.SEQUENCE = dtSequence.Rows(2)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 1
                                    newParameter.CODE = "PERIOD_ID"
                                    'value = Decimal.Parse(ctrlOrganization.CurrentValue)
                                    newParameter.VALUE = periodObj.ID
                                    lstParameter.Add(newParameter)

                                    newParameter = Nothing
                                    newParameter = New PARAMETER_DTO
                                    'Dim value As String = ""
                                    newParameter.PARAMETER_NAME = "ISDISSOLVE"
                                    newParameter.SEQUENCE = dtSequence.Rows(3)("SEQUENCE")
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
                                    newParameter.SEQUENCE = dtSequence.Rows(4)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 0
                                    newParameter.CODE = "DELETE_ALL"
                                    newParameter.VALUE = 1
                                    lstParameter.Add(newParameter)

                                    newParameter = Nothing
                                    newParameter = New PARAMETER_DTO
                                    'Dim value As String = ""
                                    newParameter.PARAMETER_NAME = "OBJ_EMP_ID"
                                    newParameter.SEQUENCE = dtSequence.Rows(5)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 1
                                    newParameter.CODE = "OBJ_EMP_ID"
                                    newParameter.VALUE = item.ID
                                    lstParameter.Add(newParameter)
                                    newParameter = Nothing
                                    newParameter = New PARAMETER_DTO
                                    'Dim value As String = ""
                                    newParameter.PARAMETER_NAME = "FROM_DATE"
                                    newParameter.SEQUENCE = dtSequence.Rows(6)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 1
                                    newParameter.CODE = "FROM_DATE"
                                    Dim vDate As Date = periodObj.START_DATE
                                    newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
                                    lstParameter.Add(newParameter)
                                    newParameter = Nothing
                                    newParameter = New PARAMETER_DTO
                                    'Dim value As String = ""
                                    newParameter.PARAMETER_NAME = "TO_DATE"
                                    newParameter.SEQUENCE = dtSequence.Rows(7)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 1
                                    newParameter.CODE = "TO_DATE"
                                    vDate = periodObj.END_DATE
                                    newParameter.VALUE = vDate.Day.ToString("00") + "/" + vDate.Month.ToString("00") + "/" + vDate.Year.ToString()
                                    lstParameter.Add(newParameter)

                                    newParameter = Nothing
                                    newParameter = New PARAMETER_DTO()
                                    newParameter.PARAMETER_NAME = "EMPLIST"
                                    newParameter.SEQUENCE = dtSequence.Rows(8)("SEQUENCE")
                                    newParameter.REPORT_FIELD = ""
                                    newParameter.IS_REQUIRE = 0
                                    newParameter.CODE = "EMPLIST"
                                    newParameter.VALUE = ""
                                    lstParameter.Add(newParameter)
                                    GetAllInformationInRequestMain(lstParameter)
                                End If
                            Next
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                            Exit Sub
                        End If
                    Else
                        ShowMessage(Translate("Bạn phải chọn khoảng thời gian tải dữ liệu"), NotifyType.Warning)
                        Return
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtEmpID As DataTable
            'Dim is_Validate As Boolean
            Dim _validate As New AT_SWIPE_DATADTO
            Dim rep As New AttendanceRepository
            Dim lstEmp As New List(Of String)
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            dtError.Columns.Add("ID", GetType(String))
            Dim irow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow

                sError = "Mã chấm công nhân viên không được để trống"
                ImportValidate.EmptyValue("ITIME_ID", row, rowError, isError, sError)

                'If row("ITIME_ID") IsNot DBNull.Value Then
                '    dtEmpID = New DataTable
                '    dtEmpID = rep.GetEmployeeByTimeID(row("ITIME_ID"))
                '    If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                '        rowError("ITIME_ID") = "Mã chấm công không tồn tại trên hệ thống."
                '        isError = True
                '    End If
                'End If

                sError = "Giờ không được để trống"
                ImportValidate.EmptyValue("VALTIME", row, rowError, isError, sError)

                'sError = "Chưa chọn máy chấm công"
                'ImportValidate.EmptyValue("TERMINAL_NAME", row, rowError, isError, sError)

                sError = "Ngày không được để trống"
                ImportValidate.IsValidDate("WORKINGDAY", row, rowError, isError, sError)

                sError = "Giờ sai định dạng"
                If Not IsDate(row("WORKINGDAY") + " " + row("VALTIME")) Then
                    rowError("VALTIME") = sError
                    isError = True
                End If

                If isError Then
                    rowError("ID") = irow
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportEmployee.ImportRow(row)
                End If
                irow = irow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SwipeData_Error')", True)
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

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Bind du lieu cho rad grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Function loadToGridByConfig(ByVal dtConfig As DataTable) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtdata As DataTable
            Using rep As New AttendanceRepository
                dtdata = rep.GetOtherList("TIME_RECORDER", False)
            End Using
            Dim MACHINE_TYPE_CODE As String = String.Empty
            If dtdata IsNot Nothing AndAlso dtdata.Rows.Count > 0 Then
                MACHINE_TYPE_CODE = (From P In dtdata.AsEnumerable Where P("ID") = cbMachine_Type.SelectedValue Select P("CODE")).FirstOrDefault
            End If
            DATA_IN = New DataTable("DATA_IN")
            'Create struct DATA IN with table config
            For Each row In dtConfig.Rows
                DATA_IN.Columns.Add(row("COLUMN_CODE").ToString.Trim, GetType(String))
            Next
            'end create struct DATA IN
            'GET DATA 
            For Each rowData In dsDataComper.Rows
                Dim newRow As DataRow = DATA_IN.NewRow()
                For Each rowConfig In dtConfig.Rows
                    newRow(rowConfig("COLUMN_CODE")) = rowData(CType(rowConfig("ORDER_COLUMN"), Integer))
                Next
                DATA_IN.Rows.Add(newRow)
            Next

            'END GET DATA
            If DATA_IN.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            Else
                Return True
            End If

            'TAM THOI NGAT XU LY DEN LINE NAY
            JSONDATA = New List(Of DATA_IN)
            Dim ArrData As New ArrayList()
            For Each row As DataRow In (From P In DATA_IN.AsEnumerable Where P("USER_ID") <> String.Empty OrElse P("USER_ID") <> "" Select P)
                If Not IsNumeric(row("USER_ID")) OrElse row("USER_ID").ToString.Trim = String.Empty Then Continue For
                Dim objJSON As New DATA_IN
                Dim strCultureInfo As String = String.Empty
                objJSON.USER_ID = row("USER_ID").ToString()
                objJSON.MACHINE_TYPE = cbMachine_Type.SelectedValue
                If MACHINE_TYPE_CODE.ToUpper = "CP" Then 'CAR PARKING
                    objJSON.TERMINAL_ID = row("TERMINAL_ID")
                    If row("TERMINAL_ID") = 1 Then 'MAY IN
                        strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "TIME_IN" Select p("FORMAT")).FirstOrDefault
                        objJSON.WORKING_DAY = ConvertDateTo24H(row("TIME_IN"), strCultureInfo)
                    ElseIf row("TERMINAL_ID") = 2 Then 'MAY OUT
                        strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "TIME_OUT" Select p("FORMAT")).FirstOrDefault
                        objJSON.WORKING_DAY = ConvertDateTo24H(row("TIME_OUT"), strCultureInfo)
                    End If
                ElseIf MACHINE_TYPE_CODE = "VT" Then 'TOUCH ID
                    objJSON.TERMINAL_ID = row("TERMINAL_ID")
                    strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "WORKING_DAY" Select p("FORMAT")).FirstOrDefault
                    objJSON.WORKING_DAY = ConvertDateTo24H(row("WORKING_DAY"), strCultureInfo)
                ElseIf MACHINE_TYPE_CODE = "AC" Then ' ACCESS CONTROL
                    objJSON.TERMINAL_ID = ""
                    strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "WORKING_DAY" Select p("FORMAT")).FirstOrDefault
                    objJSON.WORKING_DAY = ConvertDateTo24H(row("WORKING_DAY"), strCultureInfo)
                End If
                JSONDATA.Add(objJSON)
            Next
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OkClicked khi click OK tren ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("Import Data") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                Dim lastRow = worksheet.Cells.GetLastDataRow(2)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 1, lastRow, worksheet.Cells.MaxColumn, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                dtData.TableName = "DATA"
                rep.ImportSwipeData(dtData)
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'check validate 
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_NOT_CHOOSE_FILE), NotifyType.Warning)
                Exit Sub
            End If
            If Not IsNumeric(cbMachine_Type.SelectedValue) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_CHOOSE_MACHINE_TYPE), NotifyType.Warning)
                Exit Sub
            End If
            'end check validate
            'LAY THONG TIN CONFIG TMPLATE => @PAR = MACHINE_TYPE 
            Dim fistRow As Integer = 0
            Dim fistCol As Integer = 0
            Dim file_type As String = String.Empty
            Dim IAttenDance As IAttendanceBusiness = New AttendanceBusinessClient()
            Dim dsConfig As DataSet = IAttenDance.GET_CONFIG_TEMPLATE(cbMachine_Type.SelectedValue)
            If dsConfig IsNot Nothing AndAlso dsConfig.Tables.Count = 2 AndAlso dsConfig.Tables(1) IsNot Nothing AndAlso dsConfig.Tables(1).Rows.Count = 1 Then
                fistRow = dsConfig.Tables(1)(0)("FIST_ROW")
                fistCol = dsConfig.Tables(1)(0)("FIST_COL")
                file_type = dsConfig.Tables(1)(0)("FILE_TYPE")
            End If
            'end get config
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & "." & file_type)
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                Dim lastRow = worksheet.Cells.GetLastDataRow(2) + 1
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(fistRow, fistCol, lastRow, worksheet.Cells.MaxColumn, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dsDataComper.ImportRow(row)
                Next
            Next
            Dim thrIMPORT_DATA As Threading.Thread
            If loadToGridByConfig(dsConfig.Tables(0)) Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                DATA_IN.WriteXml(sw, False)
                DocXml = sw.ToString.Replace(vbCr, "").Replace(vbCrLf, "").Replace(vbLf, "").Trim
                IAttenDance.IMPORT_AT_SWIPE_DATA_V1(log, DocXml, cbMachine_Type.SelectedValue)
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")

                Exit Sub 'NGAT LUONG LAM VIEC O DAY LAM THEO HUONG MOI
                Dim jsonSerialiser = New JavaScriptSerializer()
                Dim Index As Integer = 0
                Dim TotalRow As Integer = JSONDATA.Count
                Dim Sum As Integer = 0
                Dim subArray As DATA_IN()
                While (True)
                    If TotalRow <= TOTAL_ROW_IMPORT Then
                        subArray = New DATA_IN(TotalRow - 1) {}
                        JSONDATA.CopyTo(Index, subArray, 0, TotalRow)
                        Dim strJson = jsonSerialiser.Serialize(subArray)
                        'CAL FUNCTION IMPORT
                        IAttenDance.IMPORT_AT_SWIPE_DATA(log, strJson)
                        Sum += subArray.Length
                        Exit While
                    Else
                        Try
                            subArray = New DATA_IN(TOTAL_ROW_IMPORT - 1) {}
                            JSONDATA.CopyTo(Index, subArray, 0, TOTAL_ROW_IMPORT)
                            Dim strJson = jsonSerialiser.Serialize(subArray)
                            'CAL FUNCTION IMPORT
                            thrIMPORT_DATA = New Threading.Thread(New Threading.ThreadStart(Function()
                                                                                                Return IAttenDance.IMPORT_AT_SWIPE_DATA(log, strJson)
                                                                                            End Function))
                            thrIMPORT_DATA.IsBackground = True
                            thrIMPORT_DATA.Start()
                            'END CALL FUNCTION IMPORT
                            Index = Index + TOTAL_ROW_IMPORT
                            TotalRow = TotalRow - TOTAL_ROW_IMPORT
                            Sum += subArray.Length
                        Catch ex As Exception
                        End Try
                    End If
                End While
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai toolbar
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
    Protected Sub cboTimekeepingType_CheckAllCheck(sender As Object, e As RadComboBoxCheckAllCheckEventArgs) Handles cboTimekeepingType.CheckAllCheck
        For Each rcbItem As RadComboBoxItem In cboTerminal.Items
            rcbItem.Checked = True
        Next


    End Sub
    Private Sub cboTimekeepingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTimekeepingType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If cboTimekeepingType.CheckedItems.Count > 0 Then
                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    Dim lstCode As New List(Of String)
                    For Each cbo In cboTimekeepingType.CheckedItems
                        For Each item In ListComboData.LIST_LIST_ATTENDANCE_CHECKIN_TYPE
                            If item.ID.ToString = cbo.Value Then
                                lstCode.Add(item.CODE)
                            End If
                        Next
                    Next
                    Using rep As New AttendanceRepository
                        Dim lst = rep.GetTerminalData(lstCode, ctrlOrganization.CurrentValue)
                        FillDropDownList(cboTerminal, lst, "NAME_VN", "CODE", Common.Common.SystemLanguage, False)
                    End Using
                End If
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
    ''' Xu ly su kien Click khi click btnSearchEmp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try

            rglSwipeDataDownload.Rebind()
        Catch ex As Exception
        End Try

    End Sub
    Private Function ConvertDateTo24H(ByVal strDate As String, ByVal strCulture As String) As String
        Dim outDate As Date
        Dim StrOut As String = String.Empty
        Try
            If strDate = String.Empty Then Return String.Empty
            Dim culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo(strCulture)
            outDate = Convert.ToDateTime(strDate, culture)
            StrOut = String.Format("{0}/{1}/{2} {3}:{4}", outDate.Day, outDate.Month, outDate.Year, outDate.Hour, outDate.Minute)
            Return StrOut
        Catch ex As Exception
            ShowMessage(strDate + " Loi format date:" + ex.ToString, NotifyType.Warning)
            'Return String.Empty
        End Try
    End Function
#End Region

    Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    'Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0)
                        hidempid1.Value = empID.ITIME_ID
                        txtEmployeeCode.Text = empID.EMPLOYEE_CODE
                        txtEmployeeName.Text = empID.FULLNAME_VN
                        txtTitleName.Text = empID.TITLE_NAME
                        isLoadPopup = 0


                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Reset_Find_Emp()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtEmployeeCode, txtEmployeeName, txtTitleName)

            hidempid1.Value = Nothing

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(sender As Object, e As EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(sender As Object, e As EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            Dim empID1 = ctrlFindEmployeePopup.SelectedEmployee(0)
            hidempid1.Value = empID1.ITIME_ID

            txtEmployeeCode.Text = empID1.EMPLOYEE_CODE
            txtEmployeeName.Text = empID1.FULLNAME_VN
            txtTitleName.Text = empID1.TITLE_NAME
            isLoadPopup = 0
            'FillData(empID)
            'isLoadPopup = 0
            'ClearControlValue(cbSalaryLevel, rnFactorSalary, cbSalaryRank, SalaryInsurance, basicSalary, rdSalaryBHXH, Salary_Total, rnOtherSalary1,
            '                  rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5, rnPercentSalary)
            'FillData()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnFindEmployee_Click(sender As Object, e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub rglSwipeDataDownload_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rglSwipeDataDownload.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
                Dim strName = If(datarow.GetDataKeyValue("IMAGE_GPS") IsNot Nothing, datarow.GetDataKeyValue("IMAGE_GPS").ToString, "")
                Dim lng = If(datarow.GetDataKeyValue("LONGITUDE") IsNot Nothing, datarow.GetDataKeyValue("LONGITUDE").ToString, "")
                Dim lat = If(datarow.GetDataKeyValue("LATITUDE") IsNot Nothing, datarow.GetDataKeyValue("LATITUDE").ToString, "")
                If strName <> "" Then
                    datarow("ViewImgNewTabColumn").Enabled = True
                Else
                    datarow("ViewImgNewTabColumn").Enabled = False
                    datarow("ViewImgNewTabColumn").CssClass = "hide-button"
                End If
                If lng <> "" AndAlso lat <> "" Then
                    datarow("ViewMapColumn").Enabled = True
                Else
                    datarow("ViewMapColumn").Enabled = False
                    datarow("ViewMapColumn").CssClass = "hide-button"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rglSwipeDataDownload_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rglSwipeDataDownload.ItemCommand
        Try
            If e.CommandName = "View" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_IMAGE_GPS").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Or strName.Contains(".JPEG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If
            ElseIf e.CommandName = "ViewMap" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim lng As String = item.GetDataKeyValue("LONGITUDE").ToString()
                Dim lat As String = item.GetDataKeyValue("LATITUDE").ToString()
                Dim script As String
                script = "window.open('Dialog.aspx?mid=Common&fid=ctrlViewMap&Longitude=" + lng + "&Latitude=" + lat + "');"
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "window.open('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub

    Private Function GetInOutData(ByVal uri As String, ByVal api As String) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim client As New HttpClient()
            client.BaseAddress = New Uri(uri)
            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("*/*"))
            client.DefaultRequestHeaders.Add("api-token", "23058010-751a-48a1-9242-ece7f3a85d05")

            Dim res = client.GetAsync(api).Result.Content.ReadAsStringAsync.Result
            Return res
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub RefreshGrid()
        rglSwipeDataDownload.VirtualItemCount = 0
        rglSwipeDataDownload.DataSource = New List(Of AT_SWIPE_DATADTO)
    End Sub

#Region "request"

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
    ''' Xử lý việc insert vào database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAllInformationInRequestMain(ByVal lstParams As List(Of PARAMETER_DTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log = UserLogHelper.GetCurrentLogUser()
            Dim repH As New HistaffFrameworkRepository
            Dim newRequest As New REQUEST_DTO
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

            'call function insert into database with request and parameters
            Dim result = (New CommonProgramsRepository).Insert_Requests(newRequest, dt, lstParams, 1)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

End Class
Public Structure DATA_IN
    Public USER_ID As String
    Public TERMINAL_ID As String
    Public WORKING_DAY As String
    Public MACHINE_TYPE As String
End Structure