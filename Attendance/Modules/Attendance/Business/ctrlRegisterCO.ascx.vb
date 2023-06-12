Imports System.IO
Imports System.Reflection
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRegisterCO
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim dsDataComper As New DataTable
    Public repProGram As New CommonProgramsRepository
    Public clrConverter As New System.Drawing.ColorConverter
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Run Process"
    Public Property XMLDATA As String
        Get
            Return ViewState(Me.ID & "_XMLDATA")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_XMLDATA") = value
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
    Public Shared lstParaValueShare As List(Of Object)
    Property _lstParaValueShare As List(Of Object)
        Get
            Return lstParaValueShare
        End Get
        Set(ByVal value As List(Of Object))
            lstParaValueShare = value
        End Set
    End Property
#End Region
#Region "Properties"

    ''' <summary>
    ''' Obj LEAVESHEET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LEAVESHEET As List(Of AT_LEAVESHEETDTO)
        Get
            Return ViewState(Me.ID & "_LEAVESHEET")
        End Get
        Set(ByVal value As List(Of AT_LEAVESHEETDTO))
            ViewState(Me.ID & "_LEAVESHEET") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj PERIOD
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

    ''' <summary>
    ''' Obj dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("LEAVE_DAY", GetType(String))
                dt.Columns.Add("MANUAL_NAME", GetType(String))
                dt.Columns.Add("MANUAL_ID", GetType(String))
                dt.Columns.Add("STATUS_SHIFT", GetType(String))
                dt.Columns.Add("STATUS_SHIFT_VALUE", GetType(String))
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
    ''' Obj dtDataImportEmployee
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

    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgRegisterLeave
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgRegisterLeave)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgRegisterLeave.AllowCustomPaging = True
            rgRegisterLeave.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            InitControl()
            If Not IsPostBack Then '
                GirdConfig(rgRegisterLeave)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New HistaffFrameworkRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Delete
                                       )
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
            '                                                         ToolbarIcons.Export,
            '                                                         ToolbarAuthorize.Export,
            '                                                         Translate("Xuất file mẫu")))
            MainToolBar.Items(3).Text = Translate("Xuất file mẫu")
            CType(MainToolBar.Items(3), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("PRINT",
                                                                     ToolbarIcons.Print,
                                                                     ToolbarAuthorize.Print,
                                                                     Translate("Xem log")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("APPROVE_REG",
                                                                     ToolbarIcons.Approve,
                                                                     ToolbarAuthorize.Create,
                                                                     Translate("Phê duyệt")))
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {"IMPORT_LEAVE_SHEET", FrameworkUtilities.OUT_STRING}))
            programID = Int32.Parse(obj(0).ToString())
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            rep.Dispose()
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            'Dim table As New DataTable
            'table.Columns.Add("YEAR", GetType(Integer))
            'table.Columns.Add("ID", GetType(Integer))
            'Dim row As DataRow
            'For index = 2015 To Date.Now.Year + 1
            '    row = table.NewRow
            '    row("ID") = index
            '    row("YEAR") = index
            '    table.Rows.Add(row)
            'Next
            'FillRadCombobox(cboYear, table, "YEAR", "ID")
            'cboYear.SelectedValue = Date.Now.Year
            'Dim period As New AT_PERIODDTO
            'period.ORG_ID = 1
            'period.YEAR = Date.Now.Year
            'lsData = rep.LOAD_PERIODBylinq(period)
            'Me.PERIOD = lsData
            'FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)

            Dim dtData As New DataTable
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            FillRadCombobox(cbStatus, dtData, "NAME", "ID", True)
            'rdtungay.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1)
            'rdDenngay.SelectedDate = New DateTime(DateTime.Now.Year, 12, 31)
            rdtungay.SelectedDate = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            Dim temp = DateAdd(DateInterval.Month, 1, New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
            rdDenngay.SelectedDate = DateAdd(DateInterval.Day, -1, temp)
            'If lsData.Count > 0 Then
            '    'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            '    If periodid IsNot Nothing Then
            '        'cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
            '        rdtungay.SelectedDate = periodid.START_DATE
            '        rdDenngay.SelectedDate = periodid.END_DATE
            '    Else
            '        'cboPeriod.SelectedIndex = 0
            '        'Dim periodid1 = (From d In lsData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

            '        If periodid1 IsNot Nothing Then
            '            rdtungay.SelectedDate = periodid1.START_DATE
            '            rdDenngay.SelectedDate = periodid1.END_DATE
            '        End If
            '        'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
            '        '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        'Else
            '        '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        'End If

            '        End If
            'End If
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO

                'Điều chỉnh Loại nghỉ (thêm điều kiện Loại xử lý Kiểu công: Đăng ký)
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE_FULL = True
                rep.GetComboboxData(ListComboData)
                FillDropDownList(cboManual, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL, "NAME_VN", "ID")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    phPopup.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select

            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of AT_LEAVESHEETDTO)
                    For idx = 0 To rgRegisterLeave.SelectedItems.Count - 1
                        Dim lst = New AT_LEAVESHEETDTO
                        Dim item As GridDataItem = rgRegisterLeave.SelectedItems(idx)
                        lst.ID = item.GetDataKeyValue("ID")
                        lstDeletes.Add(lst)
                    Next
                    If rep.DeleteLeaveSheet(lstDeletes) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case "APPROVE_REG"
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgRegisterLeave.SelectedItems.Count - 1
                        Dim lst = 0
                        Dim item As GridDataItem = rgRegisterLeave.SelectedItems(idx)
                        lst = item.GetDataKeyValue("ID")
                        lstID.Add(lst)
                    Next
                    If rep.ApproveApp(lstID, 1, LogHelper.CurrentUser.USERNAME, "LEAVE") Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
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
                        rgRegisterLeave.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgRegisterLeave.CurrentPageIndex = 0
                        rgRegisterLeave.MasterTableView.SortExpressions.Clear()
                        rgRegisterLeave.Rebind()
                    Case "Cancel"
                        rgRegisterLeave.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgRegisterLeave
    ''' Bind lai du lieu cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                'ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgRegisterLeave.CurrentPageIndex = 0
            rgRegisterLeave.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat excel, xuat file mau, nhap file mau
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New AttendanceRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case "APPROVE_REG"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If

                    If rgRegisterLeave.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Error)
                        Exit Sub
                    End If

                    For idx = 0 To rgRegisterLeave.SelectedItems.Count - 1
                        Dim lst = 0
                        Dim item As GridDataItem = rgRegisterLeave.SelectedItems(idx)
                        lst = item.GetDataKeyValue("STATUS")

                        If lst <> 0 Then
                            ShowMessage("Chỉ được thao tác trên đơn chờ phê duyệt", Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = "Bạn chắc chắn muốn phê duyệt"
                    ctrlMessageBox.ActionName = "APPROVE_REG"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgRegisterLeave.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtset As New DataSet
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)

                        If dtDatas.Rows.Count > 0 Then
                            dtset.Tables.Add(dtDatas)
                            dtset.Tables(0).TableName = "Table"
                            ExportTemplate("Attendance\Import\LEAVESHEET.xls",
                                      dtset, Nothing,
                                      "LEAVESHEET" & Format(Date.Now, "yyyyMMdd"))

                            'rgRegisterLeave.ExportExcel(Server, Response, dtDatas, "LEAVESHEET")
                        Else
                            ShowMessage(Translate("Không có dữ liệu xuất excel"), NotifyType.Error)
                            Exit Sub
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    isLoadPopup = 0 'Chọn Org
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register')", True)
                    'UpdateControlState()
                    'ctrlOrgPopup.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New AttendanceStoreProcedure
                    Dim dsData As DataSet = store.GetDataImportCO1()
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Attendance\Import\AT_IMPORT_REGISTER_CO.xlsx",
                                      dsData, Nothing,
                                      "AT_IMPOERT_REGISTER_CO" & Format(Date.Now, "yyyyMMdd"))
                Case "IMPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload.AllowedExtensions = "xls,xlsx"
                    ctrlUpload.Show()
                    _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            rep.Dispose()
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            ElseIf e.ActionName = "APPROVE_REG" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = "APPROVE_REG"
                UpdateControlState()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy data cho rgRegisterLeave
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgRegisterLeave, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue,
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgRegisterLeave.MasterTableView.SortExpressions.GetSortString()
            If IsNumeric(cbStatus.SelectedValue) Then
                obj.STATUS = cbStatus.SelectedValue
            End If
            If rdtungay.SelectedDate.HasValue Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate.HasValue Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            If cboManual.SelectedValue <> "" Then
                obj.MANUAL_ID = cboManual.SelectedValue
            End If
            If chkChecknghiViec.Checked Then
                obj.ISTEMINAL = True
            End If
            obj.IS_APP = -1

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.LEAVESHEET = rep.GetLeaveSheet(obj, _param, MaximumRows, rgRegisterLeave.CurrentPageIndex, rgRegisterLeave.PageSize, "CREATED_DATE desc")
                Else
                    Me.LEAVESHEET = rep.GetLeaveSheet(obj, _param, MaximumRows, rgRegisterLeave.CurrentPageIndex, rgRegisterLeave.PageSize)
                End If
            Else
                Return rep.GetLeaveSheet(obj, _param).ToTable
            End If

            rgRegisterLeave.VirtualItemCount = MaximumRows
            rgRegisterLeave.DataSource = Me.LEAVESHEET
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgRegisterLeave
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgRegisterLeave.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdtungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgRegisterLeave.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRegisterLeave.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien OrganizationSelected cua control ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(e.CurrentValue),
                                           .IS_DISSOLVE = ctrlOrgPopup.IsDissolve}
            If rep.IS_PERIODSTATUS(_param) = False Then
                ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                Exit Sub
            End If

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register&&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien click cua buton ctrlUpload
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
            'kiem tra xem program nay` co duoc phep chay trong he thong hay khong
            If Not CheckRunProgram() Then
                Exit Sub
            End If
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                Try
                    workbook = New Aspose.Cells.Workbook(fileName)
                Catch ex As Exception
                    If ex.ToString.Contains("This file's format is not supported or you don't specify a correct format") Then
                        ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Try

                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(6, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
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

            Dim dtError As New DataTable("ERROR")
            'dtError = rep.ImportLeaveSheet(dsDataComper)
            Dim sw As New StringWriter()
            Dim DocXml As String = String.Empty
            dsDataComper.WriteXml(sw, False)
            XMLDATA = sw.ToString()
            isLoadedData = 0
            lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), System.Drawing.Color)
            lblRequest.Text = ""
            CurrentState = STATE_ACTIVE
            UpdateControlState()
            checkRunRequest = 1   'Clicked button calculate in toolbar  
            If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                Exit Sub
            End If
            lblStatus.Text = repProGram.StatusString("Running")
            GetAllInformationInRequestMain()
            TimerRequest.Enabled = True
            LoadFirstAfterCal = True
            'Exit Sub


            'If dtError.Rows.Count > 0 Then
            '    dtError.TableName = "DATA"
            '    Session("EXPORTREPORT") = dtError
            '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            '    Exit Sub
            'Else
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            'End If

        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức validate cho file được upload
    ''' </summary>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtEmpID As DataTable
            'Dim is_Validate As Boolean
            Dim _validate As New AT_LEAVESHEETDTO
            Dim rep As New AttendanceRepository
            Dim store As New AttendanceStoreProcedure
            dtData.TableName = "DATA"
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 8
            Dim irowEm = 8

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                sError = "Ngày nghỉ không được để trống"
                ImportValidate.IsValidDate("LEAVE_DAY", row, rowError, isError, sError)
                sError = "Kiểu công"
                ImportValidate.IsValidList("MANUAL_NAME", "MANUAL_ID", row, rowError, isError, sError)

                If row("EMPLOYEE_CODE").ToString <> "" AndAlso row("LEAVE_DAY").ToString <> "" AndAlso row("MANUAL_ID").ToString <> "" Then
                    If rep.CHECK_LEAVE_EXITS(row("EMPLOYEE_CODE").ToString, row("LEAVE_DAY").ToString, row("MANUAL_ID").ToString, If(row("STATUS_SHIFT_VALUE").ToString <> "", row("STATUS_SHIFT_VALUE").ToString, 0)) > 0 Then
                        sError = "Ngày nghỉ không hợp lệ"
                        isError = True
                    End If
                End If

                If isError = False AndAlso row("EMPLOYEE_CODE").ToString <> "" AndAlso row("LEAVE_DAY").ToString <> "" Then
                    If rep.CHECK_LEAVE_SHEET(row("EMPLOYEE_CODE").ToString, row("LEAVE_DAY").ToString, If(row("STATUS_SHIFT_VALUE").ToString <> "", row("STATUS_SHIFT_VALUE").ToString, 0)) = 0 Then
                        sError = "Ngày có kiểu công là 0.5, 1.5 không được đăng ký đầu ca, cuối ca"
                        isError = True
                    End If
                End If

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        'rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                        rowError("EMPLOYEE_CODE") = String.Format("Nhân viên {0} có dữ liệu đăng ký không hợp lệ. Vui lòng điều chỉnh lại", row("EMPLOYEE_CODE").ToString)
                    End If

                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
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
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError Then
                Return False
            Else
                Dim lstToCheckLeaveLimit As New List(Of AT_LEAVESHEETDTO) 'Emp_ID, Manual_id, total_day
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dsDataComper.Rows.Count - 1
                    dtEmpID = New DataTable
                    dtEmpID = rep.GetEmployeeID(dsDataComper(j)("EMPLOYEE_CODE"), rdDenngay.SelectedDate)
                    rowError = dtError.NewRow
                    If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                        rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dsDataComper(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                        isError = True
                    Else
                        Dim dayNum As Decimal = rep.CHECK_LEAVE_SHEET(dsDataComper(j)("EMPLOYEE_CODE").ToString, dsDataComper(j)("LEAVE_DAY").ToString, If(dsDataComper(j)("STATUS_SHIFT_VALUE").ToString <> "", dsDataComper(j)("STATUS_SHIFT_VALUE").ToString, 0))
                        Dim empId As Decimal = CDec(dtEmpID(0)("ID"))
                        Dim manualId As Decimal = CDec(dsDataComper(j)("MANUAL_ID"))
                        Dim leaveDay As Date = ToDate(dsDataComper(j)("LEAVE_DAY"))
                        If Not lstToCheckLeaveLimit.Exists(Function(f) f.EMPLOYEE_ID = empId And f.MANUAL_ID = manualId) Then
                            Dim dto As New AT_LEAVESHEETDTO
                            dto.EMPLOYEE_CODE = dsDataComper(j)("EMPLOYEE_CODE")
                            dto.EMPLOYEE_ID = empId
                            dto.MANUAL_ID = manualId
                            dto.DAY_NUM = dayNum
                            dto.LEAVE_FROM = leaveDay
                            lstToCheckLeaveLimit.Add(dto)
                        Else
                            Dim dto As AT_LEAVESHEETDTO = lstToCheckLeaveLimit.Find(Function(f) f.EMPLOYEE_ID = empId And f.MANUAL_ID = manualId)
                            dto.DAY_NUM += dayNum
                        End If
                    End If
                    If isError Then
                        rowError("ID") = irowEm
                        dtError.Rows.Add(rowError)
                    End If
                    irowEm = irowEm + 1
                    isError = False
                Next
                If dtError.Rows.Count > 0 Then
                    dtError.TableName = "DATA"
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    'check phép < 0
                    For Each dto As AT_LEAVESHEETDTO In lstToCheckLeaveLimit
                        rowError = dtError.NewRow
                        If dto.MANUAL_ID = 5 Then
                            Dim dtSourceEntitlement = rep.GET_INFO_PHEPNAM(dto.EMPLOYEE_ID, dto.LEAVE_FROM)
                            Dim intBalance = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, 0, CDec(dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString()))
                            If dto.DAY_NUM > intBalance Then
                                rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dto.EMPLOYEE_CODE & " đã vượt quá số phép qui định, vui lòng điều chỉnh lại dữ liệu"
                                isError = True
                            End If
                        End If
                        If dto.MANUAL_ID = 6 Then
                            Dim dtSourceNB = store.GET_INFO_NGHIBU(dto.EMPLOYEE_ID, dto.LEAVE_FROM)
                            Dim intBalance = If(dtSourceNB.Rows(0)("CUR_HAVE") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("CUR_HAVE").ToString()))
                            If dto.DAY_NUM > intBalance Then
                                rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dto.EMPLOYEE_CODE & " đã vượt quá số phép qui định, vui lòng điều chỉnh lại dữ liệu"
                                isError = True
                            End If
                        End If
                        If isError Then
                            rowError("ID") = irowEm
                            dtError.Rows.Add(rowError)
                        End If
                        irowEm = irowEm + 1
                        isError = False
                    Next
                    If dtError IsNot Nothing AndAlso dtError.Rows.Count > 0 Then
                        dtError.TableName = "DATA"
                        Session("EXPORTREPORT") = dtError
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    Else
                        Return True
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien ItemDataBound cua control rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgRegisterLeave_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRegisterLeave.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
                e.Canceled = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
            newParameter.PARAMETER_NAME = "XML DATA"
            newParameter.SEQUENCE = lstSequence.Rows(0)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "XMLDATA"
            value = XMLDATA
            newParameter.VALUE = value '.Replace(vbCrLf, " ").Replace(vbNewLine, " ")
            lstParameter.Add(newParameter)

            newParameter = New PARAMETER_DTO
            newParameter.PARAMETER_NAME = "User name"
            newParameter.SEQUENCE = lstSequence.Rows(1)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "USERNAME"
            value = LogHelper.GetUserLog.Username
            newParameter.VALUE = value
            lstParameter.Add(newParameter)
            newParameter = Nothing


            newParameter = New PARAMETER_DTO
            newParameter.PARAMETER_NAME = "REQUEST ID"
            newParameter.SEQUENCE = lstSequence.Rows(2)("SEQUENCE")
            newParameter.REPORT_FIELD = ""
            newParameter.IS_REQUIRE = 1
            newParameter.CODE = "REQUEST_ID"
            value = 0
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
        Dim rep As New HistaffFrameworkRepository
        Dim dsData As New DataSet
        Try
            'get store out with program id
            Dim lstParaValue As List(Of Object)
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)
            Dim storeOut = ds.Tables(0).Rows(0).ItemArray(0)
            lstParaValue = GetListParametersValue()
            lstParaValueShare = lstParaValue
            dsData = New DataSet
            dsData = rep.ExecuteToDataSet(storeOut, lstParaValue)
            If dsData IsNot Nothing AndAlso dsData.Tables(0).Rows.Count > 0 Then
                dsData.Tables(0).TableName = "DATA"
                Session("EXPORTREPORT") = dsData.Tables(0)
                lblStatus.Text = repProGram.StatusString("CompleteWarning")
                hidRequestID.Value = requestID
                lblRequest.Text = requestID
                lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), System.Drawing.Color)
                TimerRequest.Enabled = False
                CurrentState = STATE_DEACTIVE
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Exit Sub
            Else
                rgRegisterLeave.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        Finally
            rep.Dispose()
            dsData.Dispose()
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
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False
                Case "R"
                    lblStatus.Text = repProGram.StatusString("Running")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_RUNNING), System.Drawing.Color)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False
                Case "C"
                    lblStatus.Text = repProGram.StatusString("Complete")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE), System.Drawing.Color)
                    TimerRequest.Enabled = False
                    CurrentState = STATE_DETAIL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True
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
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True
                    Run()
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
#Region "Custom"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien AjaxRequest cua control AjaxManager
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
                rgRegisterLeave.CurrentPageIndex = 0
                rgRegisterLeave.Rebind()
                If rgRegisterLeave.Items IsNot Nothing AndAlso rgRegisterLeave.Items.Count > 0 Then
                    rgRegisterLeave.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class