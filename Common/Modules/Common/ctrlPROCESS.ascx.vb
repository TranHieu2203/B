Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlPROCESS
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeePopupSingleSelect As ctrlFindEmployeePopup
    Protected WithEvents rCbb As RadComboBox
    Protected WithEvents radDPEmp As RadTextBox
    Public clrConverter As New Drawing.ColorConverter
    Public repProGram As New CommonProgramsRepository
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Module/Common/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property isAccessPanelBar As Boolean
        Get
            Return ViewState(Me.ID & "_isAccessPanelBar")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isAccessPanelBar") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj popupId
    ''' </summary>
    ''' <remarks></remarks>
    Public Property popupId As String

    ''' <summary>
    ''' Obj typeRadCombobox
    ''' </summary>
    ''' <remarks></remarks>
    Private typeRadCombobox = "Telerik.Web.UI.RadComboBox"

    ''' <summary>
    ''' Obj typeRadDatePicker
    ''' </summary>
    ''' <remarks></remarks>
    Private typeRadDatePicker = "Telerik.Web.UI.RadDatePicker"

    ''' <summary>
    ''' Obj typeRadTextBox
    ''' </summary>
    ''' <remarks></remarks>
    Private typeRadTextBox = "Telerik.Web.UI.RadTextBox"

    ''' <summary>
    ''' Obj timer
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents timer As Timers.Timer

    ''' <summary>
    ''' Obj _interval
    ''' </summary>
    ''' <remarks></remarks>
    Private _interval As Integer = 5000 '5s

    ''' <summary>
    ''' Obj isSingleSelectEmp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' ThanhNT added 19/02/2016: mục đích là để xác định chọn 1 nhân viên trong tìm kiếm nhân viên hay k?
    ''' </remarks>
    Public Property isSingleSelectEmp As Boolean
        Get
            Return ViewState(Me.ID & "_isSingleSelectEmp")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isSingleSelectEmp") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj moduleID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property moduleID As Decimal
        Get
            Return ViewState(Me.ID & "_moduleID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_moduleID") = value
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
    ''' Obj EmployeeSelected
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeSelected As String
        Get
            Return ViewState(Me.ID & "_EmployeeSelected")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_EmployeeSelected") = value
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
    ''' Obj lstOrgStr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgStr As String
        Get
            Return ViewState(Me.ID & "_lstOrgStr")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_lstOrgStr") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj userLogin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userLogin As String
        Get
            Return ViewState(Me.ID & "_userLogin")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userLogin") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj programCode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programCode As String
        Get
            Return ViewState(Me.ID & "_programCode")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_programCode") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj fileUrl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileUrl As String
        Get
            Return ViewState(Me.ID & "_fileUrl")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileUrl") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj fileOut_name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileOut_name As String
        Get
            Return ViewState(Me.ID & "_fileOut_name")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileOut_name") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj mid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property mid As String
        Get
            Return ViewState(Me.ID & "_mid")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_mid") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstOrg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrg As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstOrg")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstOrg") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj CheckOrg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckOrg As Decimal
        Get
            Return ViewState(Me.ID & "_Check")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Check") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj haveOrgPara
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property haveOrgPara As Decimal
        Get
            Return ViewState(Me.ID & "_haveOrgPara")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_haveOrgPara") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj Interval
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Interval As Integer
        Get
            Return _interval
        End Get
        Set(ByVal value As Integer)
            _interval = value
        End Set
    End Property

    ''' <summary>
    ''' Obj DataSourceDS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property DataSourceDS As DataSet
        Get
            Return ViewState(Me.ID & "_DataSourceDS")
        End Get
        Set(ByVal value As DataSet)
            ViewState(Me.ID & "_DataSourceDS") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj checkRunRequest
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' ThanhNT: thiết lập các property
    ''' </remarks>' 
    Public Property checkRunRequest As Decimal
        Get
            Return ViewState(Me.ID & "_checkRunRequest")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_checkRunRequest") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj IDSelected
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDSelected As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelected")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelected") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj funcID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property funcID As String
        Get
            Return ViewState(Me.ID & "_funcID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_funcID") = value
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
    ''' Obj programType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programType As String
        Get
            Return ViewState(Me.ID & "_programType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_programType") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadDefault
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isLoadDefault As Boolean
        Get
            Return ViewState(Me.ID & "_isLoadDefault")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoadDefault") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadDefaultValue
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isLoadDefaultValue As Boolean
        Get
            Return ViewState(Me.ID & "_isLoadDefaultValue")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoadDefaultValue") = value
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

    ''' <summary>
    ''' Obj lstReportField
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstReportField As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstReportField")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstReportField") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstTypeField
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstTypeField As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstTypeField")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstTypeField") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstCodeVS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstCodeVS As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstCodeVS")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstCodeVS") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstRequire
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstRequire As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstRequire")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstRequire") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstDefaults
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstDefaults As List(Of DEFAULT_DTO)
        Get
            Return ViewState(Me.ID & "_lstDefaults")
        End Get
        Set(ByVal value As List(Of DEFAULT_DTO))
            ViewState(Me.ID & "_lstDefaults") = value
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
    ''' Obj programIdOld
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programIdOld As Decimal
        Get
            Return ViewState(Me.ID & "_programIdOld")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_programIdOld") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj lstControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstControl As List(Of Control)
        Get
            Return ViewState(Me.ID & "_lstControl")
        End Get
        Set(ByVal value As List(Of Control))
            ViewState(Me.ID & "_lstControl") = value
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
    ''' Obj dtFunctions
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtFunctions As DataTable
        Get
            Return ViewState(Me.ID & "_dtFunctions")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtFunctions") = value
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
    ''' Obj dtTypeReports
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtTypeReports As DataTable
        Get
            Return ViewState(Me.ID & "_dtTypeReports")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTypeReports") = value
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
    ''' <summary>
    ''' Obj XMLFileThread
    ''' </summary>
    ''' <remarks></remarks>
    Private XMLFileThread As Thread

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Calculate,
                                ToolbarItem.Search,
                                ToolbarItem.Export,
                                ToolbarItem.Refresh,
                                ToolbarItem.Import,
                                ToolbarItem.Print)
            Dim ajaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            ajaxLoading.InitialDelayTime = 100
            rtbMain.Items(0).Text = Translate("Xử lý")
            rtbMain.Items(1).Text = Translate("Xem kết quả")
            rtbMain.Items(2).Text = Translate("Xuất file")
            rtbMain.Items(3).Text = Translate("Xem trạng thái")
            rtbMain.Items(4).Text = Translate("Xuất XML Data")
            rtbMain.Items(5).Text = Translate("Xem log")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            isAccessPanelBar = False
            ctrlOrg.AutoPostBack = False
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckChildNodes = True
            ctrlOrg.CheckBoxes = TreeNodeTypes.All
            TimerRequest.Enabled = False

            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin log"
            popupId = popup.ClientID

            'f1.Visible = False
            'f2.Visible = False
            'Type = Request.Params("type")
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
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If isAccessPanelBar = False Then
                AccessPanelBar()
            End If
            BuildTreeOtherListType(treeOtherListType, dtTypeReports, dtFunctions)
            UpdateControlState()
            BuildTreeOtherListType(treeOtherListType, dtTypeReports, dtFunctions)
            UpdateControlState()
            If CheckLoad = 1 And isLoadedPara = 0 AndAlso programID <> -1 Then  ' AndAlso programIdOld <> programID
                'LoadAllParameterInRequest()
                LoadAllParameterRequest()
            End If
            CheckLoad = New Decimal
            CheckLoad = 1
            isLoadedPara = 0
            If isLoadedData = 1 Then
                DesignGrid(DataSourceDS)
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
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
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
                userLogin = log.Username
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
                    Exit Sub
                End Using
            End If
            isLoadedPara = 0
            CurrentState = CommonMessage.STATE_NORMAL
            ctrlOrg.Enabled = False
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    'kiem tra xem program nay` co duoc phep chay trong he thong hay khong
                    If Not CheckRunProgram() Then
                        Exit Sub
                    End If
                    isLoadedData = 0
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), Drawing.Color)
                    lblRequest.Text = ""
                    CurrentState = STATE_ACTIVE
                    UpdateControlState()
                    Dim a = myPlaceHoder.Controls.IsReadOnly
                    checkRunRequest = 1   'Clicked button calculate in toolbar                    
                    If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                        Exit Sub
                    End If

                    If CheckOrg = 1 Then
                        If ctrlOrg.CheckedValueKeys.Count = 0 Then
                            ShowMessage("Bạn chưa chọn phòng ban!!!", NotifyType.Warning)
                            CurrentState = STATE_NORMAL
                            UpdateControlState()
                            Exit Sub
                        Else
                            lblStatus.Text = repProGram.StatusString("Running")
                            GetAllInformationInRequestMain()
                            TimerRequest.Enabled = True
                            LoadFirstAfterCal = True
                            isAccessPanelBar = True
                        End If
                    Else
                        lblStatus.Text = repProGram.StatusString("Running")
                        GetAllInformationInRequestMain()
                        TimerRequest.Enabled = True
                        LoadFirstAfterCal = True
                        isAccessPanelBar = True
                    End If

                Case CommonMessage.TOOLBARITEM_SEARCH  'View result with request
                    If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                        Exit Sub
                    End If
                    If requestID = 0 Then  'never chosen parameter and click button calculate in radtoolbar 
                        isLoadedData = 0
                        RunResult()
                    Else 'requestID != 0 --> click button calculate
                        isLoadedData = 0
                        RunResult()
                    End If
                    CurrentState = STATE_DETAIL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'get store out with program id
                    Dim lstParaValue As List(Of Object)
                    Dim lstlst As New List(Of List(Of Object))
                    'Dim rs() As Object
                    lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
                    Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)

                    'rs = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)
                    Dim storeOut = ds.Tables(0).Rows(0).ItemArray(0)
                    'execute program with store_out , parameter when user selection
                    If Not checkPara() Then     'kiem tra danh sach tham so da hop le chua?
                        Exit Sub
                    End If
                    lstParaValue = GetListParametersValue()
                    DataSourceDS = New DataSet
                    DataSourceDS = rep.ExecuteToDataSet(storeOut, lstParaValue)
                    Dim dt = ProcessDataSet2DataTable(DataSourceDS)
                    If dt.Rows.Count = 0 Then
                        Exit Sub
                    Else
                        Dim FileName As String = "ExportData" 'Mặc định tên file xuất ra là ExportData
                        If Request.Params("ProgramID") = "PAYROLL_CALCULATE" Then
                            'NGUYENNT đổi tên file xuất lương
                            Dim month = dt.Rows(0)(4).ToString 'Lấy tên của kỳ công, trong tên có bao gồm tháng và năm
                            month = month.Substring(month.Length - 7, 7).Replace("/", ".").Trim ' Lấy 7 ký tự cuối vd 10/2016, trim bỏ khoảng trắng dành cho tháng <10
                            FileName = "Tinhluong." + month 'Ten file là tinhluong nếu là màn hình Tổng hợp lương
                        End If
                        Using cls As New ExcelCommon
                            Dim book = cls.ExportExcelNoTemplate(Server.MapPath("~/ReportTemplates/Common/Common.xls"), FileName, dt, Response)
                        End Using
                    End If
                    CurrentState = STATE_DETAIL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_REFRESH 'lay thong tin trang thai cua request đang chạy                       
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
                    ViewReport_Excel(storeOut, True)
                    CurrentState = STATE_DETAIL
                    UpdateControlState()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control treeOtherListType
    ''' Xet lai cac thiet lap trang thai cho grid data grid
    ''' Thuc hien goi ham LoadAllParameterRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub treeOtherListType_SelectedNodeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOtherListType.NodeClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstParameter = New List(Of PARAMETER_DTO)
            isLoadDefault = True
            If treeOtherListType.SelectedValue <> "" Then
                lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_WAITING), Drawing.Color)
                lblProgram.Text = treeOtherListType.SelectedNode.Text
                lblStatus.Text = "Chọn tham số và nhấn Xử Lý"
                lblRequest.Text = ""
                EmployeeSelected = ""
                CurrentState = STATE_NORMAL
                UpdateControlState()
                isLoadDefault = True    'Load
                isLoadDefaultValue = False    'UnLoad
                lstDefaults = New List(Of DEFAULT_DTO)
                Dim nodeSelected = treeOtherListType.SelectedValue 'functionID <=> ctrlXXX or Program_code
                If IsNumeric(nodeSelected) Then
                    Exit Sub
                Else
                    Dim rep As New HistaffFrameworkRepository
                    'get program with nodeSelected
                    Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {nodeSelected, OUT_STRING}))
                    programID = Int32.Parse(obj(0).ToString())
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
                    'LoadAllParameterInRequest()  'call sub load all control in program
                    LoadAllParameterRequest()
                End If
                isLoadedData = 0
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
    ''' xu ly su kien TextChanged cua control RadNumTB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadNumTB_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadNumTB.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        isAccessPanelBar = True
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

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update các thuộc tính cho các control cho poup
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                'ctrlFindEmployeePopup.MustHaveTerminate = False
                ctrlFindEmployeePopup.MultiSelect = True
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.LoadAllOrganization = False
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
    ''' Phương thức xử lý việc lấy về parameter "ProgramID" và "moduleID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState Is Nothing Then
                Dim rep As New HistaffFrameworkRepository
                If Request.Params("ProgramID") IsNot Nothing Then
                    Dim objProgramID As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_PROGRAM_WITH_CODE", New List(Of Object)(New Object() {Request.Params("ProgramID"), OUT_NUMBER}))
                    programID = Decimal.Parse(objProgramID(0).ToString)
                    CheckLoad = 1
                Else
                    CheckLoad = 0
                End If

                If Request.Params("moduleID") IsNot Nothing Then
                    moduleID = Decimal.Parse(Request.Params("moduleID"))
                End If
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
    ''' Gọi các store để xử lý việc xét thưởng
    ''' Load dữ liệu cho các textbox
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AccessPanelBar()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log = UserLogHelper.GetCurrentLogUser()
            Dim repC As New HistaffFrameworkRepository
            Dim lstlst As New List(Of List(Of Object))
            lstlst = CreateParameterList(New With {.P_USERNAME = log.Username, .P_TYPE = "Process", .P_PROGRAM_ID = -1, .P_MODULE_ID = -1, .P_STORE_OUT = OUT_CURSOR})
            'get all Function(Report) in system
            Dim dt = repC.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_PERMISSION", lstlst)
            dtFunctions = dt.Tables(0).Copy()
            lblProgram.Text = If(dtFunctions.Select(String.Format("PROGRAM_ID = {0}", programID)).FirstOrDefault() Is Nothing, "Tên chương trình", dtFunctions.Select(String.Format("PROGRAM_ID = {0}", programID)).FirstOrDefault.Item("PROGRAM_NAME"))
            If LoadFirstAfterCal Then
                GetStatus()
                LoadFirstAfterCal = False
                Exit Sub
            Else
                lblStatus.ForeColor = Drawing.Color.Black
                lblStatus.Text = "Chọn tham số và nhấn Xử Lý"
            End If
            'GET ALL TYPE REPORT
            Dim lstlst1 As New List(Of List(Of Object))
            lstlst1 = CreateParameterList(New With {.P_MODULE_ID = -1, .P_PROGRAM_TYPE = "Process", .P_STORE_OUT = OUT_CURSOR})
            Dim dt1 = repC.ExecuteStore("PKG_HCM_SYSTEM.READ_ALL_TYPE_REPORT", lstlst1)
            dtTypeReports = dt1.Tables(0).Copy()
            'End If
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
    ''' Dựa theo state để ẩn hiện các control
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
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                    UpdateControl()
                Case STATE_ACTIVE
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                    UpdateControl()
                Case STATE_DEACTIVE
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    UpdateControl()
                Case STATE_DETAIL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    UpdateControl()
            End Select
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
    ''' Khởi tạo cây các loại khen thưởng
    ''' </summary>
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

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo cây các phòng ban trong công ty
    ''' </summary>
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
            If lstParameter IsNot Nothing Then
                For c = 0 To lstParameter.Count - 1
                    For Each dr As DataRow In listParameters.Rows
                        If dr.Item("CONDITION_WHERE_CLAUSE").ToString.Contains(lstParameter(c).CODE) Then
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
                    lstCodeVS.Add(listParameters(index).Item("CODE"))

                    'Get information for default value
                    defaultDTO = New DEFAULT_DTO
                    defaultDTO.CODE = listParameters(index).Item("CODE")
                    defaultDTO.DEFAULT_ID = Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))
                    defaultDTO.VALUE = IIf(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE")) = -1, Nothing, repProGram.GetDefaultValue(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))))
                    lstDefaults.Add(defaultDTO)

                    ctrlOrg.Enabled = True
                    index += 1
                    Continue For
                End If
                Dim sequence As Decimal = lstSequence.Rows(i)("SEQUENCE")
                myPlaceHoder.Controls.Add(New LiteralControl("<tr>"))
                'add label
                myPlaceHoder.Controls.Add(New LiteralControl("<td style='padding-right:15px'>"))
                Dim newLabel As New Label
                newLabel.Text = listParameters(index).Item("LABEL_COLUMN_NAME")
                myPlaceHoder.Controls.Add(newLabel)
                lstLabelPara.Add(listParameters(index).Item("LABEL_COLUMN_NAME"))
                lstReportField.Add(listParameters(index).Item("APPLICATION_COLUMN_NAME"))
                lstRequire.Add(listParameters(index).Item("IS_REQUIRE"))
                lstTypeField.Add(listParameters(index).Item("TYPE_FIELD"))
                lstCodeVS.Add(listParameters(index).Item("CODE"))

                'Get information for default value
                defaultDTO = New DEFAULT_DTO
                defaultDTO.CODE = listParameters(index).Item("CODE")
                defaultDTO.DEFAULT_ID = Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))
                defaultDTO.VALUE = IIf(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE")) = -1, Nothing, repProGram.GetDefaultValue(Decimal.Parse(listParameters(index).Item("DEFAULT_VALUE"))))
                lstDefaults.Add(defaultDTO)

                myPlaceHoder.Controls.Add(New LiteralControl("</td>"))

                'add control
                myPlaceHoder.Controls.Add(New LiteralControl("<td>"))

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
                                    qSelect = listParameters(index).Item("ID_COLUMN_NAME").ToString & " AS ID, " & listParameters(index).Item("VALUE_COLUMN_NAME").ToString & " AS CODE"
                                End If
                                Dim qFrom = listParameters(index).Item("TABLE_VIEW_SELECT").ToString

                                Dim qWhere = listParameters(index).Item("CONDITION_WHERE_CLAUSE").ToString

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
                                'If DateTime.TryParseExact(lstDefaults(index).VALUE, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, selecteddate) Then
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
                        radDPEmp = New RadTextBox
                        radDPEmp.ID = "rtbEMployee_" & index
                        radDPEmp.Width = 250
                        radDPEmp.Rows = 3
                        radDPEmp.TextMode = InputMode.MultiLine
                        radDPEmp.Text = IIf(EmployeeSelected = "", "", EmployeeSelected)
                        myPlaceHoder.Controls.Add(radDPEmp)
                        lstControl.Add(radDPEmp)
                        Dim rbtnFindEmployee As New RadButton
                        rbtnFindEmployee.ID = "rbtEMployee"
                        rbtnFindEmployee.SkinID = "ButtonView"
                        AddHandler rbtnFindEmployee.Click, AddressOf rbtnFindEmployee_Click
                        rbtnFindEmployee.Width = 100
                        index += 1
                        myPlaceHoder.Controls.Add(rbtnFindEmployee)
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
                        radDP.Width = 250
                        radDP.Text = userLogin
                        radDP.ReadOnly = True
                        radDP.WrapperCssClass = "padding-bottom-3px"
                        index += 1
                        myPlaceHoder.Controls.Add(radDP)
                        lstControl.Add(radDP)
                End Select
                myPlaceHoder.Controls.Add(New LiteralControl("</td>"))
                myPlaceHoder.Controls.Add(New LiteralControl("</tr>"))
            Next
            myPlaceHoder.Controls.Add(New LiteralControl("</table>"))
            myPlaceHoder.Controls.Add(New LiteralControl("</div>"))

            If isLoadDefault AndAlso Not isLoadDefaultValue Then
                isLoadDefaultValue = True
                myPlaceHoder.Controls.Clear()
                LoadAllParameterRequest()
            End If


            isLoadDefault = False
            TimerRequest.Enabled = isAccessPanelBar
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
    ''' Xử lý sự kiện SelectedIndexChanged cho rCbb
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub rCbb_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstParameter = New List(Of PARAMETER_DTO)
            lstParameter = GetListParameters()
            myPlaceHoder.Controls.Clear()
            LoadAllParameterRequest()
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
    ''' Load các tham số được truyền lên
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetListParameters() As List(Of PARAMETER_DTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        lstParameter = New List(Of PARAMETER_DTO)
        Dim newRequest As New REQUEST_DTO
        Dim index As Decimal = 0
        Try

            If haveOrgPara = 1 Then
                index += 1
                Dim newParameterOrg As New PARAMETER_DTO

                If ctrlOrg.CheckedValueKeys.Count = 0 Then
                    ShowMessage("Bạn CHƯA chọn phòng ban!!!", Framework.UI.Utilities.NotifyType.Warning)
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
                newParameterOrg.PARAMETER_NAME = lstLabelPara(0)
                newParameterOrg.REPORT_FIELD = lstReportField(0)
                newParameterOrg.IS_REQUIRE = lstRequire(0)
                newParameterOrg.SEQUENCE = lstSequence.Rows(0)("SEQUENCE")
                newParameterOrg.VALUE = lstOrgStr
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
                        value = radcbb.SelectedValue
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
                End Select
                lstParameter.Add(newParameter)
                index += 1
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
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
            If haveOrgPara = 1 Then
                index += 1

                If ctrlOrg.CheckedValueKeys.Count = 0 Then
                    'ShowMessage("Bạn CHƯA chọn phòng ban!!!", NotifyType.Warning)
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
            Dim dtime As DateTime
            For Each item As Control In lstControl
                'param.Add((DirectCast(lstLabelPara(index), Object)))
                typefield = lstTypeField(index)
                Select Case item.GetType.ToString()
                    Case typeRadCombobox
                        Dim radcbb As New RadComboBox
                        radcbb = DirectCast(item, RadComboBox)
                        str = radcbb.SelectedValue.ToString
                        Select Case typefield
                            Case "NVARCHAR2"
                                rs.Add(str)
                            Case "DECIMAL"
                                If IsNumeric(str) Then
                                    rs.Add(Decimal.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!!!" & vbNewLine & _
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
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu ngày giờ)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                        End Select
                    Case typeRadTextBox
                        Dim radTB As New RadTextBox
                        radTB = DirectCast(item, RadTextBox)
                        str = radTB.Text.ToString
                        Select Case typefield
                            Case "NVARCHAR2"
                                rs.Add(str)
                            Case "DECIMAL"
                                If IsNumeric(str) Then
                                    rs.Add(Decimal.Parse(str))
                                Else
                                    'báo lỗi
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!!!" & vbNewLine & _
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
                                    ShowMessage("Kiểu dữ liệu không đúng với định nghĩa tham số thứ " & index + 1 & "!!!!" & vbNewLine & _
                                                "Đề nghị điều chỉnh lại kiểu dữ liệu trong màn hình định nghĩa chương trình" & _
                                                "Kiểu dữ liệu cần điều chỉnh là (Kiểu ngày giờ)", NotifyType.Warning)
                                    hasError = -1
                                    Exit Select
                                End If
                        End Select
                    Case typeRadDatePicker
                        Dim radDP As New RadDatePicker
                        radDP = DirectCast(item, RadDatePicker)
                        dtime = radDP.SelectedDate
                        rs.Add(dtime)
                End Select
                index += 1
            Next
            rs.Add(requestID)

            'add OUT_CURSOR
            'Dim P_OUT As New List(Of Object)
            'P_OUT.Add("P_CURSOR")
            'P_OUT.Add(OUT_CURSOR)
            'rs.Add(P_OUT)
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
    ''' Kiểm tra các tham số truyền lên
    ''' </summary>
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
    ''' Xử lý việc insert vào database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAllInformationInRequestMain()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim log = UserLogHelper.GetCurrentLogUser()
            lblStatus.Text = repProGram.StatusString("Initial")
            'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

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
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
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

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load các tham số vào report
    ''' </summary>
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
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_PENDING), Drawing.Color)
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                Case "R"
                    lblStatus.Text = repProGram.StatusString("Running")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_RUNNING), Drawing.Color)
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False
                Case "C"
                    lblStatus.Text = repProGram.StatusString("Complete")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE), Drawing.Color)
                    TimerRequest.Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    CurrentState = STATE_DETAIL
                    isAccessPanelBar = False
                    Run()
                Case "CW"
                    lblStatus.Text = repProGram.StatusString("CompleteWarning")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), Drawing.Color)
                    TimerRequest.Enabled = False
                    isAccessPanelBar = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    CurrentState = STATE_DEACTIVE
                Case "CPE" 'lỗi phần tham số truyền vào
                    lblStatus.Text = repProGram.StatusString("ParameterError")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_COMPLETE_WARNING), Drawing.Color)
                    TimerRequest.Enabled = False
                    isAccessPanelBar = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    Dim objLog As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_LOG_FILE", New List(Of Object)(New Object() {requestID, OUT_STRING}))
                    Dim logDoc As String = objLog(0).ToString()
                    ShowMessage(logDoc, Framework.UI.Utilities.NotifyType.Warning)
                    CurrentState = STATE_DEACTIVE
                Case "E"
                    lblStatus.Text = repProGram.StatusString("Error")
                    hidRequestID.Value = requestID
                    lblRequest.Text = requestID
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    lblStatus.ForeColor = CType(clrConverter.ConvertFromString(CMConstant.COLOR_ERROR), Drawing.Color)
                    TimerRequest.Enabled = False
                    isAccessPanelBar = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True
                    CurrentState = STATE_DEACTIVE

            End Select
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
        isAccessPanelBar = True
        Try
            GetStatus()
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
    ''' Xử lý việc tạo báo cáo và show theo định dạng word
    ''' </summary>
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

            If (dsReport Is Nothing OrElse dsReport.Tables.Count = 0) Then
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
                        If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, fileUrl)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Framework.UI.Utilities.ExportWordMailMergeDS(System.IO.Path.Combine(PathTemplateInFolder & mid, fileUrl),
                                       fileOut_name,
                                       dsReport,
                                       Response)
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0)
                    Else
                        If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & fileUrl)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Using xls As New AsposeExcelCommon
                            If requestID <> 0 Then
                                Framework.UI.Utilities.ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid & "/"), fileUrl),
                                        fileOut_name & "_" & requestID,
                                        dsReport,
                                        Response)
                            Else
                                Framework.UI.Utilities.ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid & "/"), fileUrl),
                                        fileOut_name,
                                        dsReport,
                                        Response)
                            End If
                        End Using
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

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý việc tạo báo cáo và show theo định dạng excel
    ''' </summary>
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
                        If Not File.Exists(System.IO.Path.Combine(PathTemplateInFolder & mid, fileUrl)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Using xls As New AsposeExcelCommon
                            If requestID <> 0 Then
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                System.IO.Path.Combine(PathTemplateInFolder & mid, fileUrl), PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            Else
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                System.IO.Path.Combine(PathTemplateInFolder & mid, fileUrl), PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            End If

                        End Using
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0) 'theo đường dẫn tương đối Mappath
                    Else
                        If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid & "/" & fileUrl)) Then
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Using xls As New AsposeExcelCommon
                            If requestID <> 0 Then
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                Server.MapPath("~/ReportTemplates/" & mid & "/" & fileUrl), PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            Else
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                Server.MapPath("~/ReportTemplates/" & mid & "/" & fileUrl), PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            End If
                        End Using
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

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo các cột cho grid
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Protected Sub DesignGrid(ByVal ds As DataSet)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'If isLoadedData = 0 Then
            Dim rCol As GridBoundColumn
            Dim columnCode As String
            Dim columnName As String
            rgLoadData.MasterTableView.Columns.Clear()

            For i = 0 To ds.Tables(0).Columns.Count - 1
                'create column name in RadGrid
                columnCode = ds.Tables(0).Columns(i).ColumnName
                columnName = If(ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault() Is Nothing, columnCode, ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault()("NAME"))
                rCol = New GridBoundColumn()
                If columnCode = "EMPLOYEE_CODE" Then
                    rCol.Visible = False
                End If
                rgLoadData.MasterTableView.Columns.Add(rCol)

                rCol.DataField = columnCode
                rCol.HeaderText = columnName
                rCol.HeaderTooltip = columnName
                rCol.DataFormatString = "{0:#,##0.##}"
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.AutoPostBackOnFilter = True
                rCol.AllowFiltering = True
                If columnName.Length > 50 Then
                    rCol.HeaderStyle.Width = 150
                Else
                    rCol.HeaderStyle.Width = 80
                End If

                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                'rCol.ReadOnly = True
            Next
            If DataSourceDS.Tables.Count > 2 Then
                rgLoadData.MasterTableView.DetailTables.Item(0).Columns.Clear()
                rgLoadData.MasterTableView.DetailTables.Item(0).AllowFilteringByColumn = False

                For i = 0 To ds.Tables(2).Columns.Count - 1
                    'create column name in RadGrid
                    columnCode = ds.Tables(2).Columns(i).ColumnName
                    columnName = If(ds.Tables(3).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault() Is Nothing, columnCode, ds.Tables(3).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault()("NAME"))
                    rCol = New GridBoundColumn()
                    If columnCode = "EMPLOYEE_CODE_DT" Then
                    Else
                        rgLoadData.MasterTableView.DetailTables.Item(0).Columns.Add(rCol)
                    End If

                    rCol.DataField = columnCode
                    rCol.HeaderText = columnName
                    rCol.HeaderTooltip = columnName
                    rCol.DataFormatString = "{0:#,##0.##}"
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.AutoPostBackOnFilter = True
                    rCol.AllowFiltering = True
                    If columnName.Length > 50 Then
                        rCol.HeaderStyle.Width = 150
                    Else
                        rCol.HeaderStyle.Width = 80
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    'rCol.ReadOnly = True
                Next
            End If

            'End If
            'load data into rgLoadData
            rgLoadData.DataSource = DataSourceDS
            rgLoadData.DataBind()
            isLoadedData = 1
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
    ''' Load và đưa dữ liệu cho rgLoadData
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub rgLoadData_DetailTableDataBind(ByVal source As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
            If dataItem.Edit Then
                Exit Sub
            End If

            If DataSourceDS.Tables.Count > 2 Then
                Dim dt As DataTable = DataSourceDS.Tables(2)
                Dim dtview As DataView = dt.DefaultView()
                dtview.RowFilter = String.Format("EMPLOYEE_CODE_DT={0}", dataItem.GetDataKeyValue("EMPLOYEE_CODE").ToString)
                e.DetailTableView.DataSource = dtview.ToTable()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Public Function GetDataTable(ByVal query As String) As DataTable
    '    Dim ConnString As [String] = ConfigurationManager.ConnectionStrings("NorthwindConnectionString").ConnectionString
    '    Dim conn As New SqlConnection(ConnString)
    '    Dim adapter As New SqlDataAdapter()
    '    adapter.SelectCommand = New SqlCommand(query, conn)

    '    Dim myDataTable As New DataTable()

    '    conn.Open()
    '    Try
    '        adapter.Fill(myDataTable)
    '    Finally
    '        conn.Close()
    '    End Try

    '    Return myDataTable
    'End Function

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click cho nút rbtnFindEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            UpdateControl()
            ctrlFindEmployeePopup.Show()
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
    ''' Xử lý sự kiện click cho nút rbtnFindEmployeeSingle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Tìm nhân viên chỉ cho chọn 1 nhân viên
    ''' </remarks>
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

    ''' <lastupdate>
    ''' 12/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện slected cho ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Decimal)
        EmployeeSelected = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployeeID, List(Of Decimal))
            If lstCommonEmployee.Count <> 0 Then
                For i As Decimal = 0 To lstCommonEmployee.Count - 1
                    If i = lstCommonEmployee.Count - 1 Then
                        EmployeeSelected = EmployeeSelected & lstCommonEmployee(i)
                    Else
                        EmployeeSelected = EmployeeSelected & lstCommonEmployee(i) & ","
                    End If
                Next
            End If
            LoadAllParameterRequest()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
            'get store out with program id
            Dim rep As New HistaffFrameworkRepository
            Dim lstParaValue As List(Of Object)
            Dim lstlst As New List(Of List(Of Object))
            'Dim rs() As Object
            lstlst = CreateParameterList(New With {.P_PROGRAM_ID = programID, .P_STORE_OUT = OUT_CURSOR})
            Dim ds = rep.ExecuteStore("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)

            'rs = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRO_GET_STORE_OUT_WITH_PROGRAM", lstlst)
            Dim storeOut = ds.Tables(0).Rows(0).ItemArray(0)
            'execute program with store_out , parameter when user selection
            lstParaValue = GetListParametersValue()
            lstParaValueShare = lstParaValue
            DataSourceDS = New DataSet
            DataSourceDS = rep.ExecuteToDataSet(storeOut, lstParaValue)
            If DataSourceDS Is Nothing Then
                ShowMessage(Translate("Không có dữ liệu sau khi tính toán"), NotifyType.Error)
            Else
                DesignGrid(DataSourceDS)
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
    ''' Load và đưa dữ liệu để xuất báo cáo
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ProcessDataSet2DataTable(ByVal ds As DataSet) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rs As New DataTable
            Dim columnCode As String
            rs.TableName = "DATA"
            rs = ds.Tables(0).Copy
            If rs.Rows.Count() = 0 Then
                ShowMessage("Không có dữ liệu để xuất file", NotifyType.Warning)
                Return New DataTable
            Else
                For i = 0 To ds.Tables(0).Columns.Count - 1
                    columnCode = rs.Columns(i).ColumnName
                    rs.Columns(i).ColumnName = If(ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault() Is Nothing, columnCode, ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault()("NAME"))
                Next
                Return rs
            End If

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
    ''' Xủ lý việc xuất kết quả ra file XML
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportFileDataXML(ByVal dsReport As DataSet, ByVal requestId As Decimal, ByVal programID As Decimal, ByVal mid As String, ByVal type As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If type = 1 Then 'đường dẫn vật lý được lấy ở web.config
                If (Not System.IO.Directory.Exists(PathTemplateOutFolder & mid)) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_ERROR_EXIST_FOLDER), NotifyType.Error)
                    Exit Sub
                End If
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
            Dim RunProgramCheck = repProGram.CheckRunProgram(programID, userLogin)
            Select Case RunProgramCheck
                Case 0 'duoc phep chay --> do anything
                    Return True
                Case 1 'khong duoc phep chay --> (program nay chi chay cung luc 1 cai')
                    ShowMessage("Chương trình này đã được chạy trong hệ thống. " & vbNewLine & "Vui lòng kiểm tra trong danh sách chương trình đang chạy", NotifyType.Warning)
                    Return False
                Case 2 'khong duoc phep chay (program nay chi chay khi khac user chay )     
                    ShowMessage("Tài khoản đang được sử dụng, vui lòng đăng nhập bằng tài khoản khác để tính lương", NotifyType.Warning)
                    Return False
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

#End Region

End Class