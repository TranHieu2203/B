Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports Common.CommonBusiness
Imports Common.CommonMessage
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlREPORT
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeePopupSingleSelect As ctrlFindEmployeePopup
    Protected WithEvents rCbb As RadComboBox
    Protected WithEvents radDPEmp As RadTextBox
    Public clrConverter As New Drawing.ColorConverter
    Public repProGram As New CommonProgramsRepository
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))
    Public rep As New HistaffFrameworkRepository
    Public Delegate Sub rcbb_SelectedNodeChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    'Public Event rcbbSelectedIndexChanged As rcbb_SelectedNodeChangedDelegate
    Private XMLFileThread As Thread
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Module/Common/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property popupId As String
    Private typeRadCombobox = "Telerik.Web.UI.RadComboBox"
    Private typeRadComboboxList = "Telerik.Web.UI.RadComboBoxList"
    Private typeRadDatePicker = "Telerik.Web.UI.RadDatePicker"
    Private typeRadTextBox = "Telerik.Web.UI.RadTextBox"
    Private typeRadButton = "Telerik.Web.UI.RadButton"
    Private typeRadGrid = "Telerik.Web.UI.RadGrid"
    Private WithEvents timer As Timers.Timer
    Private _interval As Integer = 10000 '5s
    Private isPback As Integer = 0
    'ThanhNT added 13/09/2016 : Check Select single org (True: single, false: check org)
    Public Property isSingleOrgSelect As Boolean
        Get
            Return ViewState(Me.ID & "_isSingleOrgSelect")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isSingleOrgSelect") = value
        End Set
    End Property

    'THanhNT added 18/08/2016: datatable get list employee with previous parameters
    Public Property dtListEmp As DataTable
        Get
            Return ViewState(Me.ID & "_dtListEmp")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtListEmp") = value
        End Set
    End Property

    'ThanhNT added 19/02/2016: mục đích là để xác định chọn 1 nhân viên trong tìm kiếm nhân viên hay k?
    Public Property isSingleSelectEmp As Boolean
        Get
            Return ViewState(Me.ID & "_isSingleSelectEmp")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isSingleSelectEmp") = value
        End Set
    End Property

    Public Property isRunAfterComplete As DataTable
        Get
            Return ViewState(Me.ID & "_isRunAfterComplete")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_isRunAfterComplete") = value
        End Set
    End Property

    Public Property moduleID As Decimal
        Get
            Return ViewState(Me.ID & "_moduleID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_moduleID") = value
        End Set
    End Property

    Public Property CheckLoad As Decimal
        Get
            Return ViewState(Me.ID & "_CheckLoad")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_CheckLoad") = value
        End Set
    End Property

    Public Property CheckVisible As Decimal
        Get
            Return ViewState(Me.ID & "_CheckVisible")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_CheckVisible") = value
        End Set
    End Property

    Public Property IsComplete As Decimal
        Get
            Return ViewState(Me.ID & "_IsComplete")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsComplete") = value
        End Set
    End Property

    Public Property hasError As Decimal
        Get
            Return ViewState(Me.ID & "_hasError")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hasError") = value
        End Set
    End Property

    Public Property isLoadedPara As Decimal
        Get
            Return ViewState(Me.ID & "_isLoadedPara")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isLoadedPara") = value
        End Set
    End Property

    Public Property userLogin As String
        Get
            Return ViewState(Me.ID & "_userLogin")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userLogin") = value
        End Set
    End Property

    Public Property fileUrl As String
        Get
            Return ViewState(Me.ID & "_fileUrl")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileUrl") = value
        End Set
    End Property

    Public Property fileOut_name As String
        Get
            Return ViewState(Me.ID & "_fileOut_name")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileOut_name") = value
        End Set
    End Property

    Public Property mid As String
        Get
            Return ViewState(Me.ID & "_mid")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_mid") = value
        End Set
    End Property

    Public Property EmployeeSelected As String
        Get
            Return ViewState(Me.ID & "_EmployeeSelected")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_EmployeeSelected") = value
        End Set
    End Property

    Public Property template As String
        Get
            Return ViewState(Me.ID & "_template")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_template") = value
        End Set
    End Property

    Public Property lstOrgStr As String
        Get
            Return ViewState(Me.ID & "_lstOrgStr")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_lstOrgStr") = value
        End Set
    End Property

    Private Property dtTemplates As DataTable
        Get
            Return PageViewState(Me.ID & "_dtTemplates")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtTemplates") = value
        End Set
    End Property

    Public Property lstOrg As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstOrg")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstOrg") = value
        End Set
    End Property

    Public Property CheckOrg As Decimal
        Get
            Return ViewState(Me.ID & "_Check")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Check") = value
        End Set
    End Property

    Public Property haveOrgPara As Decimal
        Get
            Return ViewState(Me.ID & "_haveOrgPara")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_haveOrgPara") = value
        End Set
    End Property

    Public Property Interval As Integer
        Get
            Return _interval
        End Get
        Set(ByVal value As Integer)
            _interval = value
        End Set
    End Property

    Private Property DataSourceDS As DataSet
        Get
            Return ViewState(Me.ID & "_DataSourceDS")
        End Get
        Set(ByVal value As DataSet)
            ViewState(Me.ID & "_DataSourceDS") = value
        End Set
    End Property

    Public Property programCode As String
        Get
            Return ViewState(Me.ID & "_programCode")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_programCode") = value
        End Set
    End Property

    ' ThanhNT: thiết lập các property
    Public Property checkRunRequest As Decimal
        Get
            Return ViewState(Me.ID & "_checkRunRequest")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_checkRunRequest") = value
        End Set
    End Property

    Public Property IDSelected As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelected")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelected") = value
        End Set
    End Property

    Public Property funcID As String
        Get
            Return ViewState(Me.ID & "_funcID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_funcID") = value
        End Set
    End Property

    Public Property requestID As Decimal
        Get
            Return ViewState(Me.ID & "_requestID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_requestID") = value
        End Set
    End Property

    Public Property programType As String
        Get
            Return ViewState(Me.ID & "_programType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_programType") = value
        End Set
    End Property

    Public Property isLoadDefault As Boolean
        Get
            Return ViewState(Me.ID & "_isLoadDefault")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoadDefault") = value
        End Set
    End Property

    Public Property isLoadDefaultValue As Boolean
        Get
            Return ViewState(Me.ID & "_isLoadDefaultValue")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoadDefaultValue") = value
        End Set
    End Property

    Public Property lstSequence As DataTable
        Get
            Return ViewState(Me.ID & "_lstSequence")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_lstSequence") = value
        End Set
    End Property

    Public Property lstLabelPara As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstLabelPara")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstLabelPara") = value
        End Set
    End Property

    Public Property lstReportField As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstReportField")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstReportField") = value
        End Set
    End Property

    Public Property lstTypeField As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstTypeField")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstTypeField") = value
        End Set
    End Property

    Public Property lstType As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstType")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstType") = value
        End Set
    End Property

    Public Property lstRequire As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstRequire")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstRequire") = value
        End Set
    End Property

    Public Property lstDefaults As List(Of DEFAULT_DTO)
        Get
            Return ViewState(Me.ID & "_lstDefaults")
        End Get
        Set(ByVal value As List(Of DEFAULT_DTO))
            ViewState(Me.ID & "_lstDefaults") = value
        End Set
    End Property

    Public Property lstCodeVS As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstCodeVS")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstCodeVS") = value
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

    Public Property lstControl As List(Of Control)
        Get
            Return ViewState(Me.ID & "_lstControl")
        End Get
        Set(ByVal value As List(Of Control))
            ViewState(Me.ID & "_lstControl") = value
        End Set
    End Property

    Public Property lstParameter As List(Of PARAMETER_DTO)
        Get
            Return ViewState(Me.ID & "_lstParameter")
        End Get
        Set(ByVal value As List(Of PARAMETER_DTO))
            ViewState(Me.ID & "_lstParameter") = value
        End Set
    End Property

    Property dtFunctions As DataTable
        Get
            Return ViewState(Me.ID & "_dtFunctions")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtFunctions") = value
        End Set
    End Property

    Property listParameters As DataTable
        Get
            Return ViewState(Me.ID & "_listParameters")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_listParameters") = value
        End Set
    End Property

    Property dtTypeReports As DataTable
        Get
            Return ViewState(Me.ID & "_dtTypeReports")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTypeReports") = value
        End Set
    End Property

    ' Khi gán giá trị vòa nhưng lại để tên giống nhau		
    ' viewstate sẽ ko hiểu object gán với list?		
    Property Workings As List(Of WorkingCommonDTO)
        Get
            Return ViewState(Me.ID & "_Workings")
        End Get
        Set(ByVal value As List(Of WorkingCommonDTO))
            ViewState(Me.ID & "_Workings") = value
        End Set
    End Property
    ' Khi gán giá trị vòa nhưng lại để tên giống nhau		
    ' viewstate sẽ ko hiểu object gán với list?		
    Property lstCommonEmployee As List(Of CommonBusiness.EmployeePopupFindDTO)
        Get
            Return ViewState(Me.ID & "_lstCommonEmployee")
        End Get
        Set(ByVal value As List(Of CommonBusiness.EmployeePopupFindDTO))
            ViewState(Me.ID & "_lstCommonEmployee") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo page, control, menu toolbar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Export,
                                ToolbarItem.Import)
            'ToolbarItem.Seperator,
            'ToolbarItem.Refresh,
            Dim ajaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            ajaxLoading.InitialDelayTime = 100
            'rtbMain.Items(0).Text = Translate("Xử lý")
            'rtbMain.Items(1).Text = Translate("Xem kết quả")        
            rtbMain.Items(0).Text = Translate("Xuất file")
            'rtbMain.Items(2).Text = Translate("Xem trạng thái")
            rtbMain.Items(1).Text = Translate("Xuất PDF")
            'rtbMain.Items(4).Text = Translate("Xem log")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            'ctrlOrg.AutoPostBack = False
            'ctrlOrg.LoadDataAfterLoaded = True
            'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.CheckChildNodes = True
            'ctrlOrg.CheckBoxes = TreeNodeTypes.All

            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin nhân viên"
            popupId = popup.ClientID

            'f1.Visible = False
            'f2.Visible = False
            'Type = Request.Params("type")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            AccessPanelBar()
            BuildTreeOtherListType(treeOtherListType, dtTypeReports, dtFunctions)
            FillTemplates()
            UpdateControlState()
            If CheckLoad = 1 And isLoadedPara = 0 AndAlso programID <> -1 Then
                'LoadAllParameterInRequest()
                LoadAllParameterRequest()
            End If
            CheckLoad = New Integer
            CheckLoad = 1
            isLoadedPara = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load parameter theo ID report
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadDefault = True    'Load
            isLoadDefaultValue = False    'UnLoad
            Dim log = UserLogHelper.GetCurrentLogUser()
            userLogin = log.Username
            programID = -1
            moduleID = -1
            GetParams()
            If CheckLoad = 1 Then
                LoadAllParameterRequest()
                isLoadedPara = 1
                Using cls As New HistaffFrameworkRepository
                    Dim obj1 As Object = cls.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_CONTROL_ORG_PROGRAM_ID", New List(Of Object)(New Object() {programID, OUT_NUMBER}))
                    haveOrgPara = Decimal.Parse(obj1(0).ToString())
                    If haveOrgPara = 1 Then
                        ctrlOrg.Enabled = True
                        CheckOrg = New Decimal
                        CheckOrg = 1
                    Else
                        ctrlOrg.Enabled = False
                        CheckOrg = New Decimal
                        CheckOrg = 0
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    If IsComplete = 1 Then
                        GetAllInformationInRequestMain()
                        CurrentState = CommonMessage.STATE_DETAIL
                    End If
                    Exit Sub
                End Using
            End If
            isLoadedPara = 0
            CurrentState = CommonMessage.STATE_NORMAL
            ctrlOrg.Enabled = False
            rcbbTemplates.Enabled = False
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event Click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim objTypeReport As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRR_GET_TYPE_REPORT", New List(Of Object)(New Object() {programID, OUT_STRING}))

            If objTypeReport IsNot Nothing Then
                Dim typeReport = objTypeReport(0).ToString
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARTIEM_CALCULATE
                        lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), Drawing.Color)
                        lblRequest.Text = ""
                        CurrentState = STATE_ACTIVE
                        UpdateControlState()
                        checkRunRequest = 1   'Clicked button calculate in toolbar
                        If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                            Exit Sub
                        End If

                        If CheckOrg = 1 Then
                            If ctrlOrg.CheckedValueKeys.Count = 0 Then
                                ShowMessage("Vui lòng chọn phòng ban/đơn vị!", Framework.UI.Utilities.NotifyType.Warning)
                                CurrentState = STATE_NORMAL
                                UpdateControlState()
                                Exit Sub
                            Else
                                lblStatus.Text = repProGram.StatusString("Running")
                                GetAllInformationInRequestMain()
                                TimerRequest.Enabled = True
                            End If
                        Else
                            lblStatus.Text = repProGram.StatusString("Running")
                            GetAllInformationInRequestMain()
                            TimerRequest.Enabled = True
                        End If
                        'Insert request into AD_REQUESTS and AD_REQUESTS_QUEUES and insert parameter into AD_REQUEST_PARAMETERS

                    Case CommonMessage.TOOLBARITEM_SEARCH  'View result with request
                        If requestID = 0 Then  'never chosen parameter and click button calculate in radtoolbar
                            RunResult()
                        Else 'requestID != 0 --> click button calculate
                            RunResult()
                        End If
                        CurrentState = STATE_DETAIL
                        UpdateControlState()
                    Case CommonMessage.TOOLBARITEM_EXPORT
                        RunResult()
                        CurrentState = STATE_DETAIL
                        UpdateControlState()
                    Case CommonMessage.TOOLBARITEM_REFRESH
                        If requestID = 0 Then
                            lblStatus.Text = "Chọn tham số và nhấn xử lý"
                        Else
                            GetStatus()
                        End If
                    Case CommonMessage.TOOLBARITEM_IMPORT
                        'get store out with program id
                        Dim lstlst As New List(Of List(Of Object))
                        'Dim rs() As Object
                        lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
                        Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)

                        'rs = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)
                        Dim storeOut = ds.Tables(0).Rows(0).ItemArray(0)
                        template = rcbbTemplates.Text
                        ViewReport_Pdf(storeOut)
                        CurrentState = STATE_DETAIL
                        UpdateControlState()
                End Select
            Else
                ShowMessage("Vui lòng chọn báo cáo!", Framework.UI.Utilities.NotifyType.Warning)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event chọn báo cáo cần xem trên treeview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub treeOtherListType_SelectedNodeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOtherListType.NodeClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstParameter = New List(Of PARAMETER_DTO)
            isLoadDefault = True ''''''''''''''''''''''
            If treeOtherListType.SelectedValue <> "" Then
                lblProgram.Text = treeOtherListType.SelectedNode.Text
                lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), Drawing.Color)
                lblStatus.Text = "Chọn tham số và nhấn Xử Lý"
                lblRequest.Text = ""
                EmployeeSelected = ""
                CurrentState = STATE_NORMAL
                UpdateControlState()
                myPlaceHoder.Controls.Clear()
                isLoadDefault = True    'Load
                isLoadDefaultValue = False    'UnLoad
                lstDefaults = New List(Of DEFAULT_DTO)
                Dim nodeSelected = treeOtherListType.SelectedValue 'functionID <=> ctrlXXX or Program_code
                Dim rep As New HistaffFrameworkRepository
                'get program with nodeSelected
                Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {nodeSelected, OUT_STRING}))
                programID = Int32.Parse(obj(0).ToString())
                FillTemplates()
                'get program with nodeSelected
                Dim obj1 As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_CONTROL_ORG", New List(Of Object)(New Object() {nodeSelected, OUT_NUMBER}))
                haveOrgPara = Decimal.Parse(obj1(0).ToString())
                If haveOrgPara = 1 Then
                    ctrlOrg.Enabled = True
                    CheckOrg = New Decimal
                    CheckOrg = 1
                Else
                    ctrlOrg.Enabled = False
                    CheckOrg = New Decimal
                    CheckOrg = 0
                End If
                'Clear data in grid of employee
                lstCommonEmployee = Nothing
                dtListEmp = Nothing
                rgEmployee.Rebind()
                'LoadAllParameterInRequest()  'call sub load all control in program
                LoadAllParameterRequest()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Event TextChanged RadNumTB (số thời gian thực hiện request get trạng thái xử lý (pending, running, complete))
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
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Khởi tạo popup chọn người ký, nhân viên, tổ chức
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If isSingleSelectEmp Then 'Single select employee
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopupSingleSelect) Then
                    ctrlFindEmployeePopupSingleSelect = Me.Register("ctrlFindEmployeePopupSingleSelect", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopupSingleSelect)
                    ctrlFindEmployeePopupSingleSelect.MultiSelect = False
                    ctrlFindEmployeePopup.is_CheckNode = True
                    ctrlFindEmployeePopupSingleSelect.MustHaveContract = False
                    ctrlFindEmployeePopupSingleSelect.LoadAllOrganization = False
                End If
            Else 'Multiselect employee
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                    ctrlFindEmployeePopup.is_CheckNode = True
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                End If
            End If
            If isSingleOrgSelect Then
                ctrlOrg.AutoPostBack = False
                ctrlOrg.LoadDataAfterLoaded = True
                ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrg.CheckBoxes = TreeNodeTypes.None
            Else
                ctrlOrg.AutoPostBack = False
                ctrlOrg.LoadDataAfterLoaded = True
                ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrg.CheckChildNodes = True
                ctrlOrg.CheckBoxes = TreeNodeTypes.All
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CurrentState
                ''-----------Tab Nation----------------'
                Case STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    UpdateControl()
                Case STATE_ACTIVE
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    UpdateControl()
                Case STATE_DETAIL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    UpdateControl()

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load dữ liệu phân quyền, tên báo cáo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AccessPanelBar()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log = UserLogHelper.GetCurrentLogUser()
            'If Not Decimal.TryParse(Request.Params("moduleID"), module_Id) Then
            '    module_Id = 0
            'End If

            Dim repC As New HistaffFrameworkRepository
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_USERNAME = log.Username, .P_TYPE = "Report", .P_PROGRAM_ID = -1, .P_MODULE_ID = -1, .P_STORE_OUT = OUT_CURSOR})
            'get all Function(Report) in system
            Dim dt = repC.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_PERMISSION", lstlst)
            dtFunctions = dt.Tables(0).Copy()

            lblProgram.Text = If(dtFunctions.Select(String.Format("PROGRAM_ID = {0}", programID)).FirstOrDefault() Is Nothing, "Tên chương trình", dtFunctions.Select(String.Format("PROGRAM_ID = {0}", programID)).FirstOrDefault.Item("PROGRAM_NAME"))
            lblStatus.Text = "Chọn tham số và nhấn Xử Lý"
            'GET ALL TYPE REPORT
            Dim lstlst1 As New List(Of List(Of Object))
            lstlst1 = CreateParameterList(New With {.P_MODULE_ID = -1, .P_PROGRAM_TYPE = "Report", .P_STORE_OUT = OUT_CURSOR})
            Dim dt1 = repC.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_TYPE_REPORT", lstlst1)
            dtTypeReports = dt1.Tables(0).Copy()
            'End If
            If CheckVisible = 1 Then
                RadPane5.Visible = False
                RadSplitBar1.Visible = False
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load treeview danh sách báo cáo
    ''' </summary>
    ''' <param name="tree"></param>
    ''' <param name="lstType"></param>
    ''' <param name="lstGroup"></param>
    ''' <remarks></remarks>
    Private Sub BuildTreeOtherListType(ByVal tree As RadTreeView, ByVal lstType As DataTable, ByVal lstGroup As DataTable)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            tree.Nodes.Clear()
            For index = 0 To lstType.Rows.Count - 1
                Dim node As New RadTreeNode
                node.Value = lstType.Rows(index).ItemArray(0).ToString()
                node.Text = lstType.Rows(index).ItemArray(1)
                tree.Nodes.Add(node)
                BuildTreeOtherListGroup(node, lstType, lstGroup)
            Next
            tree.ExpandAllNodes()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load treeview danh sách báo cáo
    ''' </summary>
    ''' <param name="nodeParent"></param>
    ''' <param name="lstType"></param>
    ''' <param name="lstGroup"></param>
    ''' <remarks></remarks>
    Private Sub BuildTreeOtherListGroup(ByVal nodeParent As RadTreeNode, ByVal lstType As DataTable, ByVal lstGroup As DataTable)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            For index = 0 To lstGroup.Rows.Count - 1
                If lstGroup.Rows(index).ItemArray(3) = nodeParent.Value Then
                    Dim node As New RadTreeNode
                    node.Value = lstGroup.Rows(index).ItemArray(1) 'FunctionID <=> CtrlXXX or ProgramCode
                    node.Text = lstGroup.Rows(index).ItemArray(2)  'Function Name <=> ctrlREPORT or REPORTS_XXX                    
                    nodeParent.Nodes.Add(node)
                    If lstGroup.Rows(index).Item("PROGRAM_ID") = programID Then
                        node = treeOtherListType.FindNodeByValue(lstGroup.Rows(index).ItemArray(1))
                        programCode = lstGroup.Rows(index).ItemArray(1).ToString
                        If node IsNot Nothing Then
                            node.Selected = True
                            node.Expanded = True
                            node.ExpandParentNodes()
                            node.Focus()
                        End If

                    End If
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get tham số truyền trên từ request
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState Is Nothing Then
                'lay thong tin tham so duoc truyen tu request sang
                If Request.Params("Type") IsNot Nothing Then
                    'se thuc hien lay ra danh sach tham so va insert vao request => chay luon


                Else 'neu khong co j thi lam binh thuong => chuyen sang show man` hinh report
                    If Request.Params("ProgramID") IsNot Nothing Then
                        Dim objProgramID As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {Request.Params("ProgramID"), OUT_NUMBER}))
                        programID = Decimal.Parse(objProgramID(0).ToString)
                        CheckLoad = 1
                        CheckVisible = 1
                    Else
                        CheckLoad = 0
                        CheckVisible = 0
                    End If

                    'If Request.Params("moduleID") IsNot Nothing Then
                    '    moduleID = Decimal.Parse(Request.Params("moduleID"))
                    'End If

                    If Request.Params("IsComplete") IsNot Nothing Then
                        IsComplete = Decimal.Parse(Request.Params("IsComplete"))
                    End If
                End If

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Load tất cả các tham số truyền từ request
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
            If lstParameter IsNot Nothing AndAlso lstParameter.Count <> 0 Then
                For c = 0 To lstParameter.Count - 1
                    For Each dr As DataRow In listParameters.Rows
                        If dr.Item("CONDITION_WHERE_CLAUSE").ToString.Contains(lstParameter(c).CODE) OrElse dr.Item("CONDITION_WHERE_CLAUSE").ToString.Contains("$USER_CAL") Then
                            Select Case lstTypeField(c)
                                Case "NVARCHAR2"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstParameter(c).CODE, "'" & lstParameter(c).VALUE & "'")
                                Case "DECIMAL"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstParameter(c).CODE, lstParameter(c).VALUE)
                                Case "DATE"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstParameter(c).CODE, "TO_DATE(" & lstParameter(c).VALUE & ", 'DD/MM/RRRR')")
                            End Select
                        End If
                    Next
                Next
            End If

            'Nếu các giá trị mặc định có thì chúng ta thực hiện gán để lấy giá trị của các tham số liên quan tới tham số trước đó nếu có
            If isLoadDefaultValue Then
                For c = 0 To lstDefaults.Count - 1
                    For Each dr As DataRow In listParameters.Rows
                        If dr.Item("CONDITION_WHERE_CLAUSE").ToString.Contains(lstDefaults(c).CODE) Then
                            Select Case lstTypeField(c)
                                Case "NVARCHAR2"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstDefaults(c).CODE, "'" & lstDefaults(c).VALUE & "'")
                                Case "DECIMAL"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstDefaults(c).CODE, lstDefaults(c).VALUE)
                                Case "DATE"
                                    dr.Item("CONDITION_WHERE_CLAUSE") = dr.Item("CONDITION_WHERE_CLAUSE").ToString.Replace(lstDefaults(c).CODE, "TO_DATE(" & lstDefaults(c).VALUE & ", 'DD/MM/RRRR')")
                            End Select
                        End If
                    Next
                Next
            End If

            lstControl = New List(Of Control)
            lstLabelPara = New List(Of String)
            lstReportField = New List(Of String)
            lstRequire = New List(Of Decimal)
            lstTypeField = New List(Of String)
            lstType = New List(Of String)
            lstCodeVS = New List(Of String)
            lstDefaults = New List(Of DEFAULT_DTO)
            Dim defaultDTO As DEFAULT_DTO

            'add any tag in myPlaceHoder
            '<div>
            '    <table>
            '        <tr>
            '            <td>
            '            </td>
            '        </tr>
            '    </table>
            '</div>
            myPlaceHoder.Controls.Add(New LiteralControl("<div>"))
            myPlaceHoder.Controls.Add(New LiteralControl("<table>"))

            For i As Decimal = 0 To lstSequence.Rows.Count - 1
                If listParameters(index).Item("TYPE") = "TreeView" Then  'neu danh sach tham so co control org thi enable ctrlorg len
                    lstLabelPara.Add(listParameters(index).Item("LABEL_COLUMN_NAME"))
                    lstReportField.Add(listParameters(index).Item("APPLICATION_COLUMN_NAME"))
                    lstRequire.Add(listParameters(index).Item("IS_REQUIRE"))
                    lstTypeField.Add(listParameters(index).Item("TYPE_FIELD"))
                    lstType.Add(listParameters(index).Item("TYPE"))
                    lstCodeVS.Add(listParameters(index).Item("CODE"))

                    'Get information for default value
                    defaultDTO = New DEFAULT_DTO
                    defaultDTO.CODE = listParameters(index).Item("CODE")
                    defaultDTO.DEFAULT_ID = Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))
                    defaultDTO.VALUE = IIf(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE")) = -1, Nothing, repProGram.GetDefaultValue(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))))
                    lstDefaults.Add(defaultDTO)

                    isSingleOrgSelect = False
                    UpdateControl()
                    ctrlOrg.Enabled = True
                    index += 1
                    Continue For
                End If
                If listParameters(index).Item("TYPE") = "TreeViewSingle" Then  'neu danh sach tham so co control org thi enable ctrlorg va` chi?cho chon 1 org
                    lstLabelPara.Add(listParameters(index).Item("LABEL_COLUMN_NAME"))
                    lstReportField.Add(listParameters(index).Item("APPLICATION_COLUMN_NAME"))
                    lstRequire.Add(listParameters(index).Item("IS_REQUIRE"))
                    lstTypeField.Add(listParameters(index).Item("TYPE_FIELD"))
                    lstType.Add(listParameters(index).Item("TYPE"))
                    lstCodeVS.Add(listParameters(index).Item("CODE"))

                    'Get information for default value
                    defaultDTO = New DEFAULT_DTO
                    defaultDTO.CODE = listParameters(index).Item("CODE")
                    defaultDTO.DEFAULT_ID = Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))
                    defaultDTO.VALUE = IIf(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE")) = -1, Nothing, repProGram.GetDefaultValue(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))))
                    lstDefaults.Add(defaultDTO)

                    'Chỉ cho chọn 1 org mà thôi
                    isSingleOrgSelect = True
                    UpdateControl()
                    ctrlOrg.Enabled = True
                    index += 1
                    Continue For
                End If
                Dim sequence As Decimal = lstSequence.Rows(i)("SEQUENCE")
                myPlaceHoder.Controls.Add(New LiteralControl("<tr>"))
                If listParameters(index).Item("TYPE") <> "RadGrid" Then
                    'add label
                    myPlaceHoder.Controls.Add(New LiteralControl("<td style='padding-right:15px'>"))
                    Dim newLabel As New Label
                    newLabel.Text = listParameters(index).Item("LABEL_COLUMN_NAME")
                    myPlaceHoder.Controls.Add(newLabel)
                    myPlaceHoder.Controls.Add(New LiteralControl("</td>"))
                    'end add label
                End If

                lstLabelPara.Add(listParameters(index).Item("LABEL_COLUMN_NAME"))
                lstReportField.Add(listParameters(index).Item("APPLICATION_COLUMN_NAME"))
                lstRequire.Add(listParameters(index).Item("IS_REQUIRE"))
                lstTypeField.Add(listParameters(index).Item("TYPE_FIELD"))
                lstType.Add(listParameters(index).Item("TYPE"))
                lstCodeVS.Add(listParameters(index).Item("CODE"))

                'Get information for default value
                defaultDTO = New DEFAULT_DTO
                defaultDTO.CODE = listParameters(index).Item("CODE")
                defaultDTO.DEFAULT_ID = Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))
                defaultDTO.VALUE = IIf(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE")) = -1, Nothing, repProGram.GetDefaultValue(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))))
                lstDefaults.Add(defaultDTO)



                'add control
                myPlaceHoder.Controls.Add(New LiteralControl("<td style='padding-bottom: 3px;'>"))

                Select Case listParameters(index).Item("TYPE")
                    Case "RadCombobox"
                        Dim countDS As Decimal = 0
                        rCbb = New RadComboBox
                        rCbb.ID = "rcbb" & index
                        rCbb.Width = 250
                        rCbb.SkinID = "Number"
                        rCbb.AutoPostBack = True
                        'get datasource for radcombobox
                        AddHandler rCbb.SelectedIndexChanged, AddressOf rCbb_SelectedIndexChanged

                        Select Case listParameters(index).Item("TYPE_VALUESET")
                            Case "I" 'independent
                                Dim dtsource = listParameters.Select("VALUE")

                                If dtsource IsNot Nothing Then
                                    index = index + dtsource.Count
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "VALUE"
                                    rCbb.DataValueField = "VALUE"
                                    rCbb.DataBind()
                                Else
                                    rCbb.DataSource = dtsource
                                    rCbb.DataBind()
                                    index += 1
                                End If

                            Case "T" 'table: query in PL/SQL : exp: select code from AT_SIGN where id = 1
                                'get select + from + where
                                Dim qSelect As String = ""
                                If listParameters(index).Item("ID_COLUMN_NAME").ToString = "" Then
                                    qSelect = listParameters(index).Item("VALUE_COLUMN_NAME").ToString
                                Else
                                    qSelect = listParameters(index).Item("ID_COLUMN_NAME").ToString & " ID, " & listParameters(index).Item("VALUE_COLUMN_NAME").ToString & " CODE"
                                End If
                                Dim qFrom = listParameters(index).Item("TABLE_VIEW_SELECT").ToString


                                Dim qWhere = listParameters(index).Item("CONDITION_WHERE_CLAUSE").ToString
                                'thuc hien replace "code" neu co
                                'If i > 0 Then
                                '    qWhere = ProcessWhereCondition(i, qWhere)
                                'End If
                                'execute query statement

                                Dim dtsource = (New CommonProgramsRepository).GetResultWithSQLStatement(qSelect, qFrom, qWhere)
                                If dtsource IsNot Nothing Then

                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "CODE"
                                    rCbb.DataValueField = "ID"
                                    rCbb.DataBind()
                                    If lstParameter IsNot Nothing AndAlso Not isLoadDefault Then
                                        If lstParameter.Count > 0 Then
                                            rCbb.SelectedValue = lstParameter(index).VALUE
                                        End If
                                    Else
                                        If rCbb.DataSource IsNot Nothing Then
                                            'get default value with default_value_id
                                            If lstDefaults(index).VALUE IsNot Nothing Then
                                                Select Case lstTypeField(index)
                                                    Case "NVARCHAR2"
                                                        rCbb.SelectedValue = lstDefaults(index).VALUE
                                                    Case "DECIMAL"
                                                        rCbb.SelectedValue = Decimal.Parse(lstDefaults(index).VALUE)
                                                    Case "DATE"
                                                        rCbb.SelectedValue = Date.Parse(lstDefaults(index).VALUE)
                                                End Select
                                            End If
                                        End If
                                    End If

                                    index = index + 1
                                Else
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "CODE"
                                    rCbb.DataValueField = "ID"
                                    rCbb.DataBind()
                                    index += 1
                                End If
                        End Select
                        lstControl.Add(rCbb)
                        myPlaceHoder.Controls.Add(rCbb)
                    Case "RadComboboxList"
                        '''''''''''''''''''''''''''''''''''''''
                        '''Nếu trường hợp là RadcomboboxList thì chúng ta generate ra giống như combobox nhưng khác là có check
                        ''' 
                        '''''''''''''''''''''''''''''''''''''''
                        Dim countDS As Decimal = 0
                        rCbb = New RadComboBox
                        rCbb.ID = "rcbb" & index
                        rCbb.Width = 250
                        rCbb.SkinID = "Number"
                        rCbb.CheckBoxes = True
                        rCbb.EnableCheckAllItemsCheckBox = True
                        rCbb.DropDownAutoWidth = RadComboBoxDropDownAutoWidth.Enabled
                        'rCbb.AutoPostBack = True

                        'get datasource for radcombobox
                        'Select Case lstCodeVS(index)
                        '    Case "$YEAR_TEST"
                        'AddHandler rCbb.SelectedIndexChanged, AddressOf rCbb_SelectedIndexChanged
                        'End Select

                        Select Case listParameters(index).Item("TYPE_VALUESET")
                            Case "I" 'independent
                                Dim dtsource = listParameters.Select("VALUE")

                                If dtsource IsNot Nothing Then
                                    index = index + dtsource.Count
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "VALUE"
                                    rCbb.DataValueField = "VALUE"
                                    rCbb.DataBind()
                                Else
                                    rCbb.DataSource = dtsource
                                    rCbb.DataBind()
                                    index += 1
                                End If

                            Case "T" 'table: query in PL/SQL : exp: select code from AT_SIGN where id = 1
                                'get select + from + where
                                Dim qSelect As String = ""
                                If listParameters(index).Item("ID_COLUMN_NAME").ToString = "" Then
                                    qSelect = listParameters(index).Item("VALUE_COLUMN_NAME").ToString
                                Else
                                    qSelect = listParameters(index).Item("ID_COLUMN_NAME").ToString & " ID, " & listParameters(index).Item("VALUE_COLUMN_NAME").ToString & " CODE"
                                End If
                                Dim qFrom = listParameters(index).Item("TABLE_VIEW_SELECT").ToString


                                Dim qWhere = listParameters(index).Item("CONDITION_WHERE_CLAUSE").ToString
                                'thuc hien replace "code" neu co
                                'If i > 0 Then
                                '    qWhere = ProcessWhereCondition(i, qWhere)
                                'End If
                                'execute query statement

                                Dim dtsource = (New CommonProgramsRepository).GetResultWithSQLStatement(qSelect, qFrom, qWhere)
                                If dtsource IsNot Nothing Then
                                    dtsource = dtsource.Rows.Cast(Of DataRow)().Where(Function(row) Not row.ItemArray.All(Function(field) TypeOf field Is System.DBNull)).CopyToDataTable()
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "CODE"
                                    rCbb.DataValueField = "ID"
                                    rCbb.DataBind()
                                    If lstParameter IsNot Nothing AndAlso Not isLoadDefault Then
                                        If lstParameter.Count > 0 Then
                                            Dim itemSelected = lstParameter(index).VALUE.Split(",")
                                            For t = 0 To dtsource.Rows.Count - 1
                                                For j = 0 To itemSelected.Count - 1
                                                    If rCbb.Items.Item(t).Value = itemSelected(j) Then
                                                        rCbb.Items.Item(t).Checked = True
                                                    End If
                                                Next
                                            Next
                                        End If
                                    Else
                                        If rCbb.DataSource IsNot Nothing Then
                                            'get default value with default_value_id
                                            For t = 0 To dtsource.Rows.Count - 1
                                                If rCbb.Items.Item(t).Value = lstDefaults(index).VALUE Then
                                                    rCbb.Items.Item(t).Checked = True
                                                End If
                                            Next
                                        End If
                                    End If

                                    index = index + 1
                                Else
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "CODE"
                                    rCbb.DataValueField = "ID"
                                    rCbb.DataBind()
                                    index += 1
                                End If
                        End Select
                        lstControl.Add(rCbb)
                        myPlaceHoder.Controls.Add(rCbb)
                    Case "DateTime"
                        Dim radDTPicker As New RadDatePicker
                        Dim selecteddate As Date
                        radDTPicker.ID = "rdp" & index
                        radDTPicker.Width = 250
                        radDTPicker.DateInput.DisplayDateFormat = "dd/MM/yyyy"
                        radDTPicker.DateInput.DateFormat = "dd/MM/yyyy"
                        'get default value with default_value_id
                        If lstParameter IsNot Nothing AndAlso Not isLoadDefault Then
                            If lstParameter.Count > 0 Then
                                If lstParameter(index).VALUE IsNot Nothing Then
                                    If DateTime.TryParseExact(lstParameter(index).VALUE, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, selecteddate) Then
                                        radDTPicker.SelectedDate = selecteddate
                                    End If
                                End If
                            End If
                        Else
                            'get default value with default_value_id
                            If lstDefaults(index).VALUE IsNot Nothing Then
                                radDTPicker.SelectedDate = lstDefaults(index).VALUE
                                'End If
                            End If
                        End If
                        lstControl.Add(radDTPicker)
                        myPlaceHoder.Controls.Add(radDTPicker)
                        index += 1
                    Case "RadTextBox"
                        Dim radDP As New RadTextBox
                        radDP.ID = "rnewRequest_" & index
                        radDP.Width = 250
                        index += 1
                        'get default value with default_value_id
                        If lstParameter IsNot Nothing AndAlso Not isLoadDefault Then
                            If lstParameter.Count > 0 Then
                                If lstParameter(index).VALUE IsNot Nothing Then
                                    radDP.Text = lstParameter(index).VALUE
                                End If
                            End If
                        Else
                            'get default value with default_value_id
                            If lstDefaults(index).VALUE IsNot Nothing Then
                                radDP.Text = lstDefaults(index).VALUE
                                'End If
                            End If
                        End If
                        myPlaceHoder.Controls.Add(radDP)
                        lstControl.Add(radDP)
                    Case "FindEmployee"
                        'radDPEmp = New RadTextBox
                        'radDPEmp.ID = "rtbEMployee_" & index
                        'radDPEmp.Width = 250
                        'radDPEmp.Rows = 3
                        'radDPEmp.TextMode = InputMode.MultiLine
                        'radDPEmp.Text = IIf(EmployeeSelected = "", "", EmployeeSelected)
                        'myPlaceHoder.Controls.Add(radDPEmp)

                        Dim rbtnFindEmployee As New RadButton
                        rbtnFindEmployee.ID = "rbtEMployee"
                        rbtnFindEmployee.SkinID = "ButtonView"
                        AddHandler rbtnFindEmployee.Click, AddressOf rbtnFindEmployee_Click
                        rbtnFindEmployee.Width = 100
                        myPlaceHoder.Controls.Add(rbtnFindEmployee)

                        Dim lbl As New Label
                        lbl.ID = "lblFindEmployee"
                        lbl.Text = "  "
                        myPlaceHoder.Controls.Add(lbl)

                        Dim rbtDelEmp As New RadButton
                        rbtDelEmp.ID = "rbtDelEmp"
                        rbtDelEmp.Text = "Xóa nhân viên"
                        rbtDelEmp.SkinID = "skn_RadGrid_Delete"
                        rbtDelEmp.Style.Add("margin-top", "-2px")
                        AddHandler rbtDelEmp.Click, AddressOf rbtDelEmp_Click
                        rbtDelEmp.Width = 150
                        myPlaceHoder.Controls.Add(rbtDelEmp)
                        lstControl.Add(rbtnFindEmployee)
                        index += 1
                    Case "FindEmployeeSgl" 'Chọn 1 nhân viên trong tìm kiếm nhân viên
                        radDPEmp = New RadTextBox
                        radDPEmp.ID = "rtbEMployee_" & index
                        radDPEmp.Width = 250
                        radDPEmp.Rows = 1
                        radDPEmp.ReadOnly = True
                        radDPEmp.Text = IIf(EmployeeSelected = "", "", EmployeeSelected)
                        myPlaceHoder.Controls.Add(radDPEmp)

                        Dim lbl As New Label
                        lbl.ID = "lblFindEmployee"
                        lbl.Text = "  "
                        myPlaceHoder.Controls.Add(lbl)

                        Dim rbtnFindEmployeeSingle As New RadButton
                        rbtnFindEmployeeSingle.ID = "rbtEMployee"
                        rbtnFindEmployeeSingle.SkinID = "ButtonView"
                        AddHandler rbtnFindEmployeeSingle.Click, AddressOf rbtnFindEmployeeSingle_Click
                        rbtnFindEmployeeSingle.Width = 100
                        myPlaceHoder.Controls.Add(rbtnFindEmployeeSingle)
                        lstControl.Add(radDPEmp)
                        index += 1
                    Case "UserTextBox"
                        Dim radDP As New RadTextBox
                        radDP.ID = "rtbUser_" & index
                        radDP.Text = userLogin
                        radDP.ReadOnly = True
                        index += 1
                        myPlaceHoder.Controls.Add(radDP)
                        lstControl.Add(radDP)
                    Case "RadGrid" 'Dùng trong trường hợp load danh sách nhân viên
                        'Chỉ để tượng trưng --> không cần bỏ vào place holder -> chỉ add vào control để lúc lấy data lên thôi                        
                        Select Case listParameters(index).Item("TYPE_VALUESET")
                            Case "I" 'independent
                                Dim dtsource = listParameters.Select("VALUE")

                                If dtsource IsNot Nothing Then
                                    index = index + dtsource.Count
                                    rCbb.DataSource = dtsource
                                    rCbb.DataTextField = "VALUE"
                                    rCbb.DataValueField = "VALUE"
                                    rCbb.DataBind()
                                Else
                                    rCbb.DataSource = dtsource
                                    rCbb.DataBind()
                                    index += 1
                                End If

                            Case "T" 'table: query in PL/SQL : exp: select code from AT_SIGN where id = 1
                                'get select + from + where
                                Dim qSelect As String = ""
                                If listParameters(index).Item("ID_COLUMN_NAME").ToString = "" Then
                                    qSelect = listParameters(index).Item("VALUE_COLUMN_NAME").ToString
                                Else
                                    qSelect = listParameters(index).Item("ID_COLUMN_NAME").ToString & " ID, " & listParameters(index).Item("VALUE_COLUMN_NAME").ToString & " CODE"
                                End If
                                Dim qFrom = listParameters(index).Item("TABLE_VIEW_SELECT").ToString


                                Dim qWhere = listParameters(index).Item("CONDITION_WHERE_CLAUSE").ToString
                                'thuc hien replace "code" neu co
                                'If i > 0 Then
                                '    qWhere = ProcessWhereCondition(i, qWhere)
                                'End If
                                'execute query statement

                                Dim dtsource = (New CommonProgramsRepository).GetResultWithSQLStatementDT(qSelect, qFrom, qWhere)
                                If dtsource.Columns.Count <= 2 Then
                                    dtListEmp = Nothing
                                Else
                                    dtListEmp = dtsource
                                    rgEmployee.DataSource = dtListEmp
                                End If
                                lstCommonEmployee = Nothing 'Clear 
                        End Select
                        rgEmployee.Rebind()
                        index += 1
                        lstControl.Add(New RadGrid)
                End Select
                myPlaceHoder.Controls.Add(New LiteralControl("</td>"))
                myPlaceHoder.Controls.Add(New LiteralControl("</tr>"))
            Next
            myPlaceHoder.Controls.Add(New LiteralControl("</table>"))
            myPlaceHoder.Controls.Add(New LiteralControl("</div>"))

            If isLoadDefault AndAlso Not isLoadDefaultValue Then
                isLoadDefaultValue = True
                myPlaceHoder.Controls.Clear()
                lstParameter = New List(Of PARAMETER_DTO)
                lstParameter = GetListParameters()
                If lstParameter.Count <> 0 Then
                    isLoadDefaultValue = True
                    isLoadDefault = False
                End If
                LoadAllParameterRequest()
            End If

            isLoadDefault = False
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rCbb_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstParameter = New List(Of PARAMETER_DTO)
            lstParameter = GetListParameters()
            myPlaceHoder.Controls.Clear()
            isLoadDefaultValue = False
            LoadAllParameterRequest()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Get list tham số filter của report
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListParameters() As List(Of PARAMETER_DTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim newRequest As New REQUEST_DTO
            lstParameter = New List(Of PARAMETER_DTO)

            Dim index As Decimal = 0
            If lstLabelPara Is Nothing Then
                Return lstParameter
            End If
            Using cls As New HistaffFrameworkRepository
                Dim obj1 As Object = cls.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_CONTROL_ORG_PROGRAM_ID", New List(Of Object)(New Object() {programID, OUT_NUMBER}))
                haveOrgPara = Decimal.Parse(obj1(0).ToString())
            End Using
            If haveOrgPara = 1 Then
                index += 1
                Dim newParameterOrg As New PARAMETER_DTO

                'If ctrlOrg.CheckedValueKeys.Count = 0 Then
                '    ShowMessage("Vui lòng chọn phòng ban/đơn vị!", Framework.UI.Utilities.NotifyType.Warning)
                'Else
                If isSingleOrgSelect Then 'Chon 1 org
                    newParameterOrg.VALUE = ctrlOrg.CurrentValue
                Else
                    lstOrg = ctrlOrg.CheckedValueKeys
                    lstOrgStr = ""
                    For i As Integer = 0 To lstOrg.Count - 1
                        If i = lstOrg.Count - 1 Then
                            lstOrgStr = lstOrgStr & lstOrg(i).ToString
                        Else
                            lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
                        End If
                    Next
                    newParameterOrg.VALUE = lstOrgStr
                End If

                'End If
                newParameterOrg.PARAMETER_NAME = lstLabelPara(0)
                newParameterOrg.REPORT_FIELD = lstReportField(0)
                newParameterOrg.IS_REQUIRE = lstRequire(0)
                newParameterOrg.SEQUENCE = lstSequence.Rows(0)("SEQUENCE")
                newParameterOrg.CODE = lstCodeVS(0)
                lstParameter.Add(newParameterOrg)
            End If

            For Each item As Control In lstControl
                Dim newParameter As New PARAMETER_DTO
                Dim value As String = ""
                newParameter.PARAMETER_NAME = lstLabelPara(index)
                newParameter.SEQUENCE = lstSequence.Rows(index)("SEQUENCE")
                newParameter.REPORT_FIELD = lstReportField(index)
                newParameter.IS_REQUIRE = lstRequire(index)
                newParameter.CODE = lstCodeVS(index)
                Select Case item.GetType.ToString()
                    Case typeRadCombobox
                        Dim radcbb As New RadComboBox
                        radcbb = DirectCast(item, RadComboBox)
                        If lstType(index).ToString() = "RadCombobox" Then
                            value = radcbb.SelectedValue
                        Else
                            If radcbb.CheckedItems.Count = 0 Then
                                value = ""
                            Else
                                value = ","
                                For Each itemCbb As RadComboBoxItem In radcbb.CheckedItems
                                    value &= itemCbb.Value & ","
                                Next
                            End If 'Note: Chuỗi sau khi lấy ra có dạng value = ",1,2,3,"  --> trong store chỉ việc instr(value, X.Field) > 0
                        End If

                        newParameter.VALUE = value
                    Case typeRadDatePicker
                        Dim radDP As New RadDatePicker
                        radDP = DirectCast(item, RadDatePicker)
                        value = String.Format("{0:dd/MM/yyyy}", radDP.SelectedDate)
                        newParameter.VALUE = value
                    Case typeRadTextBox
                        Dim radTB As New RadTextBox
                        radTB = DirectCast(item, RadTextBox)
                        value = radTB.Text
                        newParameter.VALUE = value
                    Case typeRadButton, typeRadGrid 'Dành cho control find employee/ load danh sach nhan vien theo phong ban ...
                        Dim strEmployeeList As String = ""
                        If rgEmployee.SelectedItems.Count = 0 Then
                        Else
                            For i As Integer = 0 To rgEmployee.SelectedItems.Count - 1
                                Dim itemSelected As GridDataItem = rgEmployee.SelectedItems(i)
                                If i = rgEmployee.SelectedItems.Count - 1 Then
                                    strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text()
                                Else
                                    strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text() & ","
                                End If
                            Next
                        End If
                        newParameter.VALUE = strEmployeeList
                End Select
                lstParameter.Add(newParameter)
                index += 1
            Next
            Return lstParameter
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(ex.Message, NotifyType.Alert)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' Get giá trị tham số sau khi nhập của report
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListParametersValue() As List(Of Object)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rs As New List(Of Object)
            hasError = 0
            Dim index As Decimal = 0
            Dim str As String = ""
            Dim typefield As String = ""
            If haveOrgPara = 1 Then
                index += 1
                If isSingleOrgSelect Then
                    rs.Add(ctrlOrg.CurrentValue)
                Else
                    If ctrlOrg.CheckedValueKeys.Count = 0 Then
                        ShowMessage("Vui lòng chọn phòng ban/đơn vị!", NotifyType.Warning)
                        hasError = -1
                    Else
                        lstOrg = ctrlOrg.CheckedValueKeys
                        lstOrgStr = ""
                        For i As Integer = 0 To lstOrg.Count - 1
                            If i = lstOrg.Count - 1 Then
                                lstOrgStr = lstOrgStr & lstOrg(i).ToString
                            Else
                                lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
                            End If
                        Next
                    End If
                    rs.Add(lstOrgStr)
                End If

            End If
            Dim datetime As DateTime
            For Each item As Control In lstControl
                'param.Add((DirectCast(lstLabelPara(index), Object)))
                typefield = lstTypeField(index)
                Select Case item.GetType.ToString()
                    Case typeRadCombobox
                        Dim radcbb As New RadComboBox
                        radcbb = DirectCast(item, RadComboBox)
                        'Kiểm tra nếu Combobox có phải là loại list hay không???
                        If lstType(index).ToString() = "RadCombobox" Then
                            If lstRequire(index) = 1 Then
                                If radcbb.Text Is Nothing OrElse radcbb.Text = "" Then
                                    ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
                                    Exit Function
                                End If
                                str = radcbb.SelectedValue.ToString
                            ElseIf radcbb.SelectedValue Is Nothing OrElse radcbb.Text = "" Then  'nếu không require và tham số không có giá trị
                                str = ""
                            Else
                                str = radcbb.SelectedValue.ToString
                            End If
                        Else  'RadComboboxList
                            If lstRequire(index) = 1 Then
                                If radcbb.CheckedItems.Count = 0 Then
                                    ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
                                    Exit Function
                                Else
                                    str = ","
                                    For Each itemCbb As RadComboBoxItem In radcbb.CheckedItems
                                        str &= itemCbb.Value & ","
                                    Next
                                End If
                            Else 'khong require
                                If radcbb.CheckedItems.Count = 0 Then  'nếu không require và tham số không có giá trị
                                    str = ""
                                Else 'khong require ma` co check chon
                                    str = ","
                                    For Each itemCbb As RadComboBoxItem In radcbb.CheckedItems
                                        str &= itemCbb.Value & ","
                                    Next
                                End If
                                'Note: Chuỗi sau khi lấy ra có dạng value = ",1,2,3,"  --> trong store chỉ việc instr(value, X.Field) > 0
                            End If
                        End If

                        Select Case typefield
                            Case "NVARCHAR2"
                                rs.Add(str)
                            Case "DECIMAL"
                                If IsNumeric(str) Then
                                    rs.Add(Decimal.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu số)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                            Case "DATE"
                                If IsDate(str) Then
                                    rs.Add(Date.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu ngày giờ)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                        End Select
                    Case typeRadTextBox
                        Dim radTB As New RadTextBox
                        radTB = DirectCast(item, RadTextBox)
                        If lstRequire(index) = 1 Then
                            If radTB.Text Is Nothing Then
                                ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
                                Exit Function
                            End If
                            str = radTB.Text.ToString
                        Else 'khong require
                            str = radTB.Text
                        End If
                        Select Case typefield
                            Case "NVARCHAR2"
                                rs.Add(str)
                            Case "DECIMAL"
                                If IsNumeric(str) Then
                                    rs.Add(Decimal.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu số)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                            Case "DATE"
                                If IsDate(str) Then
                                    rs.Add(Date.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu ngày giờ)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                        End Select
                    Case typeRadDatePicker
                        Dim radDP As New RadDatePicker
                        radDP = DirectCast(item, RadDatePicker)
                        If lstRequire(index) = 1 Then  'require
                            If radDP.SelectedDate Is Nothing Then
                                ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
                                Exit Function
                            End If
                            datetime = radDP.SelectedDate
                        Else 'not require
                            If radDP.SelectedDate Is Nothing Then  'nếu không require và tham số không có giá trị
                                datetime = Nothing
                            Else
                                datetime = radDP.SelectedDate
                            End If
                        End If
                        rs.Add(datetime)
                    Case typeRadButton, typeRadGrid 'Dành cho control find employee
                        Dim strEmployeeList As String = ""
                        If lstRequire(index) = 1 Then 'Require
                            If rgEmployee.SelectedItems.Count = 0 Then
                                ShowMessage("Điều kiện " & lstLabelPara(index).ToUpper() & " bắt buộc nhập!", NotifyType.Warning)
                                Exit Function
                            Else
                                For i As Integer = 0 To rgEmployee.SelectedItems.Count - 1
                                    Dim itemSelected As GridDataItem = rgEmployee.SelectedItems(i)
                                    If i = rgEmployee.SelectedItems.Count - 1 Then
                                        strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text()
                                    Else
                                        strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text() & ","
                                    End If
                                Next
                            End If
                        Else 'Not require
                            If rgEmployee.SelectedItems.Count > 0 Then
                                For i As Integer = 0 To rgEmployee.SelectedItems.Count - 1
                                    Dim itemSelected As GridDataItem = rgEmployee.SelectedItems(i)
                                    If i = rgEmployee.SelectedItems.Count - 1 Then
                                        strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text()
                                    Else
                                        strEmployeeList &= itemSelected("EMPLOYEE_CODE").Text() & ","
                                    End If
                                Next
                            End If
                        End If

                        rs.Add(strEmployeeList)

                End Select
                index += 1
            Next
            rs.Add(requestID)

            'add OUT_CURSOR
            'Dim P_OUT As New List(Of Object)
            'P_OUT.Add("P_CURSOR")
            'P_OUT.Add(OUT_CURSOR)
            'rs.Add(P_OUT)
            Return rs
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(datetime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(ex.Message, NotifyType.Alert)
        End Try

    End Function
    ''' <summary>
    ''' Check bắt buộc nhập các tham số của report
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Process Mệnh đề WHere query báo cáo (xử lý format các kiểu dữ liệu)
    ''' </summary>
    ''' <param name="indexCurrent"></param>
    ''' <param name="whereClause"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessWhereCondition(ByVal indexCurrent As Decimal, ByVal whereClause As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rs As String = whereClause

            Dim typefield As String = ""
            Dim code As String = ""
            Dim ctrl As New Control
            'tien hanh replace cai code neu trong menh de where
            For index = 0 To indexCurrent - 1
                'lay gia tri va replace vao
                Dim str As String = ""
                typefield = lstTypeField(index)
                code = lstCodeVS(index)
                If Not whereClause.Contains(code) Then
                    Return whereClause
                Else
                    ctrl = DirectCast(lstControl(index), Control)
                    Select Case ctrl.GetType.ToString()
                        Case typeRadCombobox
                            Dim radcbb As New RadComboBox
                            radcbb = DirectCast(ctrl, RadComboBox)
                            str = radcbb.SelectedValue
                            Select Case typefield
                                Case "NVARCHAR2"
                                    rs = rs.Replace(code, "'" & str & "'")
                                Case "DECIMAL"
                                    rs = rs.Replace(code, str)
                                Case "DATE"
                                    rs = rs.Replace(code, "TO_DATE(" & str & ", 'DD/MM/RRRR')")
                            End Select
                    End Select
                End If
            Next
            Return rs
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Get tất cả các thông tin xử lý các báo cáo (xử lý theo hàng đợi)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAllInformationInRequestMain()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log As New UserLog
            log = LogHelper.GetUserLog
            userLogin = log.Username.ToUpper
            Dim isupdateState As Decimal = 0

            Dim repH As New HistaffFrameworkRepository
            Dim newRequest As New REQUEST_DTO
            lstParameter = New List(Of PARAMETER_DTO)

            Dim index As Decimal = 0
            newRequest.PROGRAM_ID = programID
            newRequest.START_DATE = DateTime.Now
            newRequest.END_DATE = DateTime.Now.AddDays(100)
            newRequest.ACTUAL_START_DATE = DateTime.Now
            newRequest.ACTUAL_COMPLETE_DATE = DateTime.Now.AddDays(100)
            newRequest.CREATED_BY = log.Username.ToUpper
            newRequest.CREATED_DATE = DateTime.Now
            newRequest.MODIFIED_BY = log.Username.ToUpper
            newRequest.MODIFIED_DATE = DateTime.Now
            newRequest.CREATED_LOG = log.Ip + "-" + log.ComputerName
            newRequest.MODIFIED_LOG = log.Ip + "-" + log.ComputerName
            If IsComplete = 1 Then
                newRequest.PHASE_CODE = "C"
                newRequest.STATUS_CODE = "Complete"
                lblStatus.Text = repProGram.StatusString("Complete")
                lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE), Drawing.Color)
            Else
                lblStatus.Text = repProGram.StatusString("Initial")
                newRequest.PHASE_CODE = "I"
                newRequest.STATUS_CODE = "Initial"
                isupdateState = 1
                'get list parameter
                lstParameter = GetListParameters()
            End If
            'Get program with programId
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
            'get all Function(Report) in system
            Dim dt = repH.ExecuteStore("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_ID", lstlst)

            'call function insert into database with request and parameters
            requestID = New Decimal
            requestID = (New CommonProgramsRepository).Insert_Requests(newRequest, dt, lstParameter, isupdateState)
            lblRequest.Text = requestID.ToString
            hidRequestID.Value = requestID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Get các tham số của báo cáo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListParametersInReport() As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New HistaffFrameworkRepository
            Dim rs As New List(Of List(Of Object))
            Dim index As Decimal = 0
            Dim strName As String = ""
            Dim strValue As String = ""
            Dim pr As New List(Of List(Of Object))
            pr = CreateParameterList(New With {.P_REQUEST_ID = requestID, .P_CUR = OUT_CURSOR})
            Dim dtVariable As New DataTable
            Dim drVariable = dtVariable.NewRow()
            DataSourceDS = rep.ExecuteStore("PKG_HCM_SYSTEM.PRR_GET_ALL_PARAMETER_REQUETS", pr)
            'For index = 0 To DataSourceDS.Tables(0).Rows.Count - 1
            '    strName = DataSourceDS.Tables(0).Rows(index).ItemArray(0).ToString
            '    dtVariable.Columns.Add(New DataColumn(strName))
            'Next

            For index = 0 To DataSourceDS.Tables(0).Rows.Count - 1
                strName = DataSourceDS.Tables(0).Rows(index).ItemArray(0).ToString   'get field name in report
                dtVariable.Columns.Add(New DataColumn(strName))
                strValue = DataSourceDS.Tables(0).Rows(index).ItemArray(1).ToString  'get value with field name in report
                drVariable(strName) = strValue
            Next
            dtVariable.Rows.Add(drVariable)
            Return dtVariable
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Get trạng thái xử lý báo cáo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetStatus()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New HistaffFrameworkRepository
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRR_GET_STATUS_IN_REQUEST", New List(Of Object)(New Object() {requestID, OUT_STRING}))

            Select Case obj(0).ToString
                Case "P"
                    lblStatus.Text = repProGram.StatusString("Pending")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_PENDING), Drawing.Color)
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                Case "R"
                    lblStatus.Text = repProGram.StatusString("Running")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_RUNNING), Drawing.Color)
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                Case "C"
                    lblStatus.Text = repProGram.StatusString("Complete")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE), Drawing.Color)
                    TimerRequest.Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    Run()
                Case "CW"
                    lblStatus.Text = repProGram.StatusString("CompleteWarning")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), Drawing.Color)
                    TimerRequest.Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                Case "CPE" 'lỗi phần tham số truyền vào
                    lblStatus.Text = repProGram.StatusString("ParameterError")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), Drawing.Color)
                    TimerRequest.Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    Dim objLog As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE", New List(Of Object)(New Object() {requestID, OUT_STRING}))
                    Dim logDoc As String = objLog(0).ToString()
                    ShowMessage(logDoc, Framework.UI.Utilities.NotifyType.Warning)
                Case "E"
                    lblStatus.Text = repProGram.StatusString("Error")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_ERROR), Drawing.Color)
                    TimerRequest.Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Event Get trạng thái xử lý của báo cáo sau khoảng thời gian đc set
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TimerRequest_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRequest.Tick
        Try
            GetStatus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' View báo cáo dạng file doc hoặc xml
    ''' </summary>
    ''' <param name="store_Out"></param>
    ''' <param name="isExportXML"></param>
    ''' <remarks></remarks>
    Private Sub ViewReport_Doc(ByVal store_Out As String, Optional ByVal isExportXML As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstParaValue As New List(Of Object)
            Dim rep As New HistaffFrameworkRepository
            Dim _error As Integer = 0
            Dim itmp As Integer = 1
            Dim lstlst As New List(Of List(Of Object))
            Dim dataSet As New DataSet

            lstParaValue = GetListParametersValue()
            If hasError = -1 Then
                Exit Sub
            End If

            Dim dsReport As New DataSet
            dsReport = rep.ExecuteToDataSet(store_Out, lstParaValue)

            If (dsReport Is Nothing OrElse dsReport.Tables.Count = 0 OrElse dsReport.Tables(0).Rows.Count = 0) Then
                ShowMessage(Translate("Không có dữ liệu để in báo cáo"), Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            End If

            If programID <> -1 Then
                'get Template URL
                repProGram.GetTemplateInfo(programID, fileUrl, fileOut_name, mid)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 1) 'Theo đường dẫn vật lý
                    Else 'Thực hiện export file sau khi xử lý báo cáo
                        If fileUrl.Contains(template) Then
                            'Không lấy fileUrl nữa mà lấy
                            If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, template)) Then
                                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            Framework.UI.Utilities.ExportWordMailMerge(System.IO.Path.Combine(PathTemplateInFolder & mid, template),
                                           fileOut_name,
                                           dsReport.Tables(0),
                                           Response)
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 

                        End If
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0)
                    Else
                        If fileUrl.Contains(template) Then
                            If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & template)) Then
                                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            Using xls As New AsposeExcelCommon
                                If requestID <> 0 Then
                                    Framework.UI.Utilities.ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid & "/"), template),
                                            fileOut_name & "_" & requestID,
                                            dsReport.Tables(0),
                                            Response)
                                Else
                                    Framework.UI.Utilities.ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid & "/"), template),
                                            fileOut_name,
                                            dsReport.Tables(0),
                                            Response)
                                End If
                            End Using
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 
                            ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                    End If

                End If
            Else
                ShowMessage("Không tìm thấy thông tin mẫu báo cáo trong hệ thống", NotifyType.Warning)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' View báo cáo dạng file excel hoặc xml
    ''' </summary>
    ''' <param name="store_Out"></param>
    ''' <param name="isExportXML"></param>
    ''' <remarks></remarks>
    Private Sub ViewReport_Excel(ByVal store_Out As String, Optional ByVal isExportXML As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstParaValue As New List(Of Object)
            Dim rep As New HistaffFrameworkRepository
            Dim _error As Integer = 0
            Dim itmp As Integer = 1
            Dim lstlst As New List(Of List(Of Object))
            Dim dataSet As New DataSet
            Dim dtVariable As New DataTable

            lstParaValue = GetListParametersValue() 'get all parameters value in request            
            If hasError = -1 Then
                Exit Sub
            End If
            Dim dsReport As New DataSet
            dsReport = rep.ExecuteToDataSet(store_Out, lstParaValue)
            If (dsReport Is Nothing OrElse dsReport.Tables(0).Rows.Count = 0) Then
                ShowMessage(Translate("Không có dữ liệu để in báo cáo"), Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If store_Out <> "PKG_IPAY_REPORT.IPAY_REPORT_07_OUT" Then
                Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                Dim path As String = sourcePath + dsReport.Tables(0).Rows(0)("ATTACH_FILE_LOGO").ToString + dsReport.Tables(0).Rows(0)("FILE_LOGO").ToString
                If Not File.Exists(path) Then
                    dsReport.Tables(0).Rows(0)("FILE_LOGO") = "NoImage.jpg"
                Else
                    dsReport.Tables(0).Rows(0)("FILE_LOGO") = sourcePath + dsReport.Tables(0).Rows(0)("ATTACH_FILE_LOGO") + dsReport.Tables(0).Rows(0)("FILE_LOGO")
                End If
            Else
                Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                Dim path As String = sourcePath + dsReport.Tables(1).Rows(0)("ATTACH_FILE_LOGO").ToString + dsReport.Tables(1).Rows(0)("FILE_LOGO").ToString
                If Not File.Exists(path) Then
                    dsReport.Tables(1).Rows(0)("FILE_LOGO") = "NoImage.jpg"
                Else
                    dsReport.Tables(1).Rows(0)("FILE_LOGO") = sourcePath + dsReport.Tables(1).Rows(0)("ATTACH_FILE_LOGO") + dsReport.Tables(1).Rows(0)("FILE_LOGO")
                End If
            End If

            If programID <> -1 Then
                'get Template URL
                repProGram.GetTemplateInfo(programID, fileUrl, fileOut_name, mid)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 1) 'theo đường dẫn vật lý
                    Else 'Thực hiện export file sau khi xử lý báo cáo
                        If fileUrl.Contains(template) Then
                            If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, template)) Then
                                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            Using xls As New AsposeExcelCommon
                                If requestID <> 0 Then
                                    Dim bCheck = xls.ExportExcelTemplateReport(
                                    System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                Else
                                    Dim bCheck = xls.ExportExcelTemplateReport(
                                    System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                End If

                            End Using
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 
                            ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0) 'theo đường dẫn tương đối Mappath
                    Else
                        If fileUrl.Contains(template) Then
                            If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & template)) Then
                                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If store_Out <> "PKG_IPAY_REPORT.IPAY_REPORT_07_OUT" Then
                                Using xls As New AsposeExcelCommon
                                    If requestID <> 0 Then
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                    Else
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                    End If
                                End Using
                            Else
                                Using xls As New AsposeExcelCommon
                                    If requestID <> 0 Then
                                        Dim bCheck = xls.ExportExcelTemplateReportNoLogo(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                    Else
                                        Dim bCheck = xls.ExportExcelTemplateReportNoLogo(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response)
                                    End If
                                End Using
                            End If
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 
                            ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                End If
            Else
                ShowMessage("Không tìm thấy thông tin mẫu báo cáo trong hệ thống", NotifyType.Warning)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' View báo cáo dạng Excel hoặc pdf
    ''' </summary>
    ''' <param name="store_Out"></param>
    ''' <param name="isPDF"></param>
    ''' <remarks></remarks>
    Private Sub ViewReport_ExcelMerge(ByVal store_Out As String, ByVal isPDF As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstParaValue As New List(Of Object)
            Dim rep As New HistaffFrameworkRepository
            Dim _error As Integer = 0
            Dim itmp As Integer = 1
            Dim lstlst As New List(Of List(Of Object))
            Dim dataSet As New DataSet
            Dim bcheck As String = ""

            lstParaValue = GetListParametersValue() 'get all parameters value in request            
            If hasError = -1 Then
                Exit Sub
            End If

            If programID <> -1 Then

                'get Template URL
                repProGram.GetTemplateInfo(programID, fileUrl, fileOut_name, mid)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server  
                    If fileUrl.Contains(template) Then
                    Else 'Template báo cáo trong định nghĩa # với template được chọn 
                        ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, template)) Then
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Using xls As New AsposeExcelCommon
                        If isPDF Then
                            bcheck = xls.ExportExcelTemplateReport_Merge(fileOut_name & "_" & requestID, lstParaValue, store_Out, System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, template, Response, HistaffFrameworkPublic.AsposeExcelCommon.ExportType.PDF)
                        Else
                            bcheck = xls.ExportExcelTemplateReport_Merge(fileOut_name & "_" & requestID, lstParaValue, store_Out, System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, template, Response)
                        End If
                    End Using
                Else 'Thực hiện export file sau khi xử lý báo cáo
                    If fileUrl.Contains(template) Then
                    Else 'Template báo cáo trong định nghĩa # với template được chọn 
                        ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & template)) Then
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Using xls As New AsposeExcelCommon
                        If isPDF Then
                            bcheck = xls.ExportExcelTemplateReport_Merge(fileOut_name & "_" & requestID, lstParaValue, store_Out, Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, template, Response, HistaffFrameworkPublic.AsposeExcelCommon.ExportType.PDF)
                        Else
                            bcheck = xls.ExportExcelTemplateReport_Merge(fileOut_name & "_" & requestID, lstParaValue, store_Out, Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, template, Response)
                        End If

                    End Using
                End If
                If bcheck <> "" Then
                    ShowMessage(bcheck, NotifyType.Warning)
                End If
            Else
                ShowMessage("Không tìm thấy thông tin mẫu báo cáo trong hệ thống", NotifyType.Warning)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' View báo cáo dạng pdf hoặc xml
    ''' </summary>
    ''' <param name="store_Out"></param>
    ''' <param name="isExportXML"></param>
    ''' <remarks></remarks>
    Private Sub ViewReport_Pdf(ByVal store_Out As String, Optional ByVal isExportXML As Boolean = False)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstParaValue As New List(Of Object)
            Dim rep As New HistaffFrameworkRepository
            Dim _error As Integer = 0
            Dim itmp As Integer = 1
            Dim lstlst As New List(Of List(Of Object))
            Dim dataSet As New DataSet
            Dim dtVariable As New DataTable

            lstParaValue = GetListParametersValue() 'get all parameters value in request            
            If hasError = -1 Then
                Exit Sub
            End If
            Dim dsReport As New DataSet
            dsReport = rep.ExecuteToDataSet(store_Out, lstParaValue)

            If (dsReport Is Nothing OrElse dsReport.Tables.Count = 0) Then
                ShowMessage(Translate("Không có dữ liệu để in báo cáo"), Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If programID <> -1 Then
                'get Template URL
                repProGram.GetTemplateInfo(programID, fileUrl, fileOut_name, mid)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 1) 'theo đường dẫn vật lý
                    Else 'Thực hiện export file sau khi xử lý báo cáo
                        If fileUrl.Contains(template) Then
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 
                            ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, template)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        Else
                            If template.Contains(".xls") OrElse template.Contains(".xlsx") Then 'kiem tra file teamplate
                                'Kiem tra file template
                                '.doc hoac .docx --> true
                                '.xls hoac .xlsx --> false 
                                Using xls As New AsposeExcelCommon
                                    If requestID <> 0 Then
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response, "", AsposeExcelCommon.ExportType.PDF)
                                    Else
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        System.IO.Path.Combine(PathTemplateInFolder & mid, template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response, "", AsposeExcelCommon.ExportType.PDF)
                                    End If
                                End Using
                            Else '.doc, .docx
                                If fileOut_name.Contains(".doc") OrElse fileOut_name.Contains(".docx") Then
                                    Framework.UI.Utilities.ExportWordMailMerge(System.IO.Path.Combine(PathTemplateInFolder & mid, template),
                                       fileOut_name.Substring(0, If(fileOut_name.Contains(".doc"), fileOut_name.Length - 4, fileOut_name.Length - 5)) & ".pdf",
                                       dsReport.Tables(0),
                                       Response, 1)
                                Else
                                    ShowMessage(Translate("Vui lòng liên hệ với quản trị Histaff để định nghĩa là tên file xuất"), Framework.UI.Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0) 'theo đường dẫn tương đối Mappath
                    Else
                        If fileUrl.Contains(template) Then
                        Else 'Template báo cáo trong định nghĩa # với template được chọn 
                            ShowMessage(Translate("Mẫu báo cáo không khớp với định nghĩa"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & template)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        Else
                            Using xls As New AsposeExcelCommon
                                If template.Contains(".xls") OrElse template.Contains(".xlsx") Then 'kiem tra file teamplate
                                    'Kiem tra file template
                                    '.doc hoac .docx --> true
                                    '.xls hoac .xlsx --> false                                 

                                    If requestID <> 0 Then
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response, "", AsposeExcelCommon.ExportType.PDF)
                                    Else
                                        Dim bCheck = xls.ExportExcelTemplateReport(
                                        Server.MapPath("~/ReportTemplates/" & mid & "/" & template), PathTemplateOutFolder & mid, fileOut_name & "_" & requestID, dsReport, Response, "", AsposeExcelCommon.ExportType.PDF)
                                    End If
                                Else '.doc
                                    If fileOut_name.Contains(".doc") OrElse fileOut_name.Contains(".docx") Then
                                        Framework.UI.Utilities.ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid & "/"), template),
                                           fileOut_name.Substring(0, If(fileOut_name.Contains(".doc"), fileOut_name.Length - 4, fileOut_name.Length - 5)) & ".pdf",
                                           dsReport.Tables(0),
                                           Response, 1)
                                    Else
                                        ShowMessage(Translate("Vui lòng liên hệ với quản trị Histaff để định nghĩa là tên file xuất"), Framework.UI.Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If

                                End If
                            End Using
                        End If
                    End If

                End If
            Else
                ShowMessage("Không tìm thấy thông tin mẫu báo cáo trong hệ thống", NotifyType.Warning)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Lấy mẫu file của báo cáo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTemplates()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'load danh sách template lên để người dùng chọn
            Dim rep As New HistaffFrameworkRepository
            Dim ds = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_TEMPLATE_IN_PROGRAM", New List(Of Object)({programID}))
            dtTemplates = ds.Tables(0)
            rcbbTemplates.DataSource = Nothing
            rcbbTemplates.Items.Clear()
            rcbbTemplates.DataSource = dtTemplates
            rcbbTemplates.DataValueField = "VALUE"
            rcbbTemplates.DataTextField = "VALUE"
            rcbbTemplates.DataBind()
            rcbbTemplates.SelectedIndex = 0
            rcbbTemplates.Enabled = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Event xóa dữ liệu nhân viên đã chọn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtDelEmp_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstEmp As New List(Of CommonBusiness.EmployeePopupFindDTO)
            lstEmp = lstCommonEmployee
            For Each abc As GridDataItem In rgEmployee.SelectedItems
                Dim id As Decimal = Decimal.Parse(abc.GetDataKeyValue("ID").ToString())
                lstEmp.RemoveAll(Function(x) x.ID = id)
            Next
            lstCommonEmployee = lstEmp
            rgEmployee.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Tìm nhân viên cho chọn nhiều nhân viên khác phòng ban
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isSingleSelectEmp = False
            UpdateControl()
            ctrlFindEmployeePopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Tìm nhân viên chỉ cho chọn 1 nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtnFindEmployeeSingle_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isSingleSelectEmp = True
            UpdateControl()
            ctrlFindEmployeePopupSingleSelect.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lstEmpTemp As New List(Of CommonBusiness.EmployeePopupFindDTO)
            Dim lstSelectedEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstSelectedEmployee.Count <> 0 Then
                If lstCommonEmployee Is Nothing OrElse lstCommonEmployee.Count = 0 Then
                    lstCommonEmployee = New List(Of CommonBusiness.EmployeePopupFindDTO)
                    lstCommonEmployee = lstSelectedEmployee
                Else
                    lstEmpTemp = lstCommonEmployee
                    For i As Integer = 0 To lstSelectedEmployee.Count - 1
                        For j As Integer = 0 To lstEmpTemp.Count - 1
                            If lstSelectedEmployee(i).EMPLOYEE_ID = lstEmpTemp(j).EMPLOYEE_ID Then
                                Exit For
                            Else
                                If j = lstEmpTemp.Count - 1 Then
                                    lstCommonEmployee.Add(lstSelectedEmployee(i))
                                End If
                            End If
                        Next
                    Next
                End If
            End If
            rgEmployee.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Event chọn nhiều nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopupSingleSelect_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopupSingleSelect.EmployeeSelected
        Dim lstCommonEmployee As New List(Of EmployeePopupFindDTO)
        EmployeeSelected = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopupSingleSelect.SelectedEmployee, List(Of EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                For i As Decimal = 0 To lstCommonEmployee.Count - 1
                    If i = lstCommonEmployee.Count - 1 Then
                        EmployeeSelected = EmployeeSelected & lstCommonEmployee(i).EMPLOYEE_CODE
                    Else
                        EmployeeSelected = EmployeeSelected & lstCommonEmployee(i).EMPLOYEE_CODE & ","
                    End If
                Next
            End If
            lstParameter = New List(Of PARAMETER_DTO)
            lstParameter = GetListParameters()
            myPlaceHoder.Controls.Clear()
            LoadAllParameterRequest()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    'Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of EmployeePopupFindDTO)
    '    EmployeeSelected = ""
    '    Try
    '        lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of EmployeePopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            For i As Decimal = 0 To lstCommonEmployee.Count - 1
    '                If i = lstCommonEmployee.Count - 1 Then
    '                    EmployeeSelected = EmployeeSelected & lstCommonEmployee(i).EMPLOYEE_CODE
    '                Else
    '                    EmployeeSelected = EmployeeSelected & lstCommonEmployee(i).EMPLOYEE_CODE & ","
    '                End If
    '            Next
    '        End If
    '        lstParameter = New List(Of PARAMETER_DTO)
    '        lstParameter = GetListParameters()
    '        myPlaceHoder.Controls.Clear()
    '        LoadAllParameterRequest()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' get result của báo cáo và view theo kiểu view đã set
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RunResult()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            template = rcbbTemplates.Text
            'get store out with program id
            Dim rep As New HistaffFrameworkRepository
            Dim lstlst As New List(Of List(Of Object))
            'Dim rs() As Object
            Dim objTypeReport As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRR_GET_TYPE_REPORT", New List(Of Object)(New Object() {programID, OUT_STRING}))
            Dim typeReport = objTypeReport(0).ToString

            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)

            Dim storeOut = ds.Tables(0).Rows(0).ItemArray(0)
            Select Case typeReport
                Case "doc"
                    ViewReport_Doc(storeOut)
                Case "xls"
                    If programCode.StartsWith("SHEET_") Then
                        ViewReport_ExcelMerge(storeOut, False)
                    Else
                        ViewReport_Excel(storeOut)
                    End If
                Case "pdf"
                    If programCode.StartsWith("SHEET_") Then
                        ViewReport_ExcelMerge(storeOut, False)
                    Else
                        ViewReport_Pdf(storeOut)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Run báo cáo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Run()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If isRunAfterComplete(0).Item("IS_RUN_AFTER_COMPLETE") = 0 Then 'khogn cho chay lien`
                Exit Sub
            Else 'chay lien`
                RunResult()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Export file XML
    ''' </summary>
    ''' <param name="dsReport"></param>
    ''' <param name="requestId"></param>
    ''' <param name="programID"></param>
    ''' <param name="mid"></param>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub ExportFileDataXML(ByVal dsReport As DataSet, ByVal requestId As Decimal, ByVal programID As Decimal, ByVal mid As String, ByVal type As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If type = 1 Then 'đường dẫn vật lý được lấy ở web.config
                dsReport.WriteXml(System.IO.Path.Combine(PathTemplateOutFolder & mid, programID & "_XML_DATA_" & requestId & ".xml"))
                Dim fileToDownload = System.IO.Path.Combine(PathTemplateOutFolder & mid, programID & "_XML_DATA_" & requestId & ".xml") 'Server.MapPath("~/App_Data/someFile.pdf")

                Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileToDownload)

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
            Else
                dsReport.WriteXml(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/ReportOut/" & mid), programID & "_XML_DATA_" & requestId & ".xml"))
                Dim fileToDownload = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/ReportOut/" & mid), programID & "_XML_DATA_" & requestId & ".xml") 'Server.MapPath("~/App_Data/someFile.pdf")

                Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileToDownload)

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' Reload grid Nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If lstCommonEmployee Is Nothing Then
                If dtListEmp Is Nothing Then
                    rgEmployee.DataSource = New List(Of EmployeePopupFindDTO)
                Else
                    rgEmployee.DataSource = dtListEmp 'ThanhNT added 18/08/2016 (Auto load)
                End If
            Else
                rgEmployee.DataSource = lstCommonEmployee
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
End Class