Imports System.IO
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPrograms
    Inherits CommonView

    ''' <summary>
    ''' rCbb
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents rCbb As RadComboBox

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

    ''' <summary>
    ''' rep
    ''' </summary>
    ''' <remarks></remarks>
    Public rep As New HistaffFrameworkRepository

    ''' <summary>
    ''' repCPR
    ''' </summary>
    ''' <remarks></remarks>
    Private repCPR As New CommonProgramsRepository

    ''' <summary>
    ''' popupId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property popupId As String

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelected
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IDSelected As Decimal
        Get
            Return PageViewState(Me.ID & "_IDSelected")
        End Get

        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_IDSelected") = value
        End Set
    End Property

    ''' <summary>
    ''' IsLoadDataRow
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IsLoadDataRow As Boolean
        Get
            Return PageViewState(Me.ID & "_IsLoadDataRow")
        End Get

        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_IsLoadDataRow") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueProgramType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValueProgramType As String
        Get
            Return PageViewState(Me.ID & "_ValueProgramType")
        End Get

        Set(ByVal value As String)
            PageViewState(Me.ID & "_ValueProgramType") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbProgramType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbProgramType As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbProgramType")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbProgramType") = value
        End Set
    End Property

    ''' <summary>
    ''' dtTemplates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtTemplates As DataTable
        Get
            Return PageViewState(Me.ID & "_dtTemplates")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtTemplates") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbTemplateTypeIn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbTemplateTypeIn As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbTemplateTypeIn")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbTemplateTypeIn") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbTemplateTypeOut
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbTemplateTypeOut As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbTemplateTypeOut")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbTemplateTypeOut") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbModule
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbModule As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbModule")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbModule") = value
        End Set
    End Property

    ''' <summary>
    ''' mid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property mid As String
        Get
            Return PageViewState(Me.ID & "_mid")
        End Get

        Set(ByVal value As String)
            PageViewState(Me.ID & "_mid") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbGroup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbGroup As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbGroup")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbGroup") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbGridViewProgram
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbGridViewProgram As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbGridViewProgram")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbGridViewProgram") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim startTime As DateTime = DateTime.UtcNow

            Try
                FillDataAllCombobox()
                rgPrograms.SetFilter()
                rgPrograms.Rebind()
                _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End If

    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Create, _
                                 ToolbarItem.Edit, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Save, _
                                 ToolbarItem.Cancel, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Search, _
                                 ToolbarItem.Seperator, _
                                 ToolbarItem.Calculate)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            rtbMain.Items(6).Text = Translate(CommonMessage.TOOLBARITEM_REFRESH)
            rtbMain.Items(8).Text = Translate("Xem Tham số")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin tham số"
            popupId = popup.ClientID

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Cập nhật trang thái của ctrl </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CurrentState = CommonMessage.STATE_NORMAL
            IsLoadDataRow = True
            UpdateControlState()
            rcbbTemplates.Enabled = False

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim proID As Decimal = -1

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW  'begin create program with all information in form
                    'setup any Radcombobox in form
                    IsLoadDataRow = False
                    'FillDataAllCombobox()
                    UpdateControlState()
                    rtbProgramCode.Focus()

                Case CommonMessage.TOOLBARITEM_EDIT
                    'If rgPrograms.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If rgPrograms.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If rgPrograms.SelectedItems.Count = 1 Then
                    '    CurrentState = CommonMessage.STATE_EDIT
                    '    UpdateControlState()
                    '    Dim slItem As GridDataItem
                    '    slItem = rgPrograms.SelectedItems(0)
                    '    UpdateControlCustom(slItem("PROGRAM_TYPE").Text)
                    '    rcbbTemplates.Enabled = True
                    '    Exit Sub
                    'End If

                    If IDSelected = 0 Then
                        IDSelected = 0
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    IsLoadDataRow = False
                    UpdateControlState()
                    UpdateControlCustom(rcbProgramType.Text)

                Case CommonMessage.TOOLBARITEM_SAVE
                    'Neu la New => insert vao AD_PROGRAMS vs programID  = 0
                    'Neu la Edit => Update vao AD_PROGRAMS vs programID = idSelected in gridview

                    If Page.IsValid Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If Not Page.IsValid Then
                                    Exit Sub
                                End If

                                Dim repCPR As New CommonProgramsRepository
                                'Lấy tất cả thông tin trên form và insert vào AD_PROGRAMS   
                                Dim program As New AD_PROGRAM_DTO
                                program.PROGRAM_ID = proID

                                'If Check_Code() Then
                                '    program.CODE = rtbProgramCode.Text.Trim
                                'Else
                                '    Exit Sub
                                'End If

                                'If Check_ProgramName() Then
                                '    program.NAME = rtbProgramName.Text.Trim
                                'Else
                                '    Exit Sub
                                'End If

                                program.CODE = rtbProgramCode.Text.Trim
                                program.NAME = rtbProgramName.Text.Trim
                                program.DESCRIPTION = rtbProgramDesc.Text.Trim

                                If rcbProgramType.Text = "Import" Then
                                    program.PROGRAM_TYPE = rcbProgramType.Text 'rcbProgramType.Text
                                    program.SQLLOADER_FILE = rtbSQLLoaderFile.Text.Trim
                                    program.LINE_FROM = RadNTBLineFrom.Value
                                    'khong lay cac thong tin khac
                                    program.FILE_OUT_NAME = ""
                                    program.STORE_EXECUTE_IN = rtbStoreIn.Text.Trim
                                    program.STORE_EXECUTE_OUT = rtbStoreOut.Text.Trim
                                    program.TEMPLATE_NAME = ""
                                    program.TEMPLATE_TYPE_IN = ""
                                    program.TEMPLATE_TYPE_OUT = ""
                                    program.TEMPLATE_URL = ""

                                ElseIf rcbProgramType.Text = "Process" Then
                                    program.PROGRAM_TYPE = rcbProgramType.Text
                                    program.SQLLOADER_FILE = ""
                                    program.LINE_FROM = 1
                                    'khong lay cac thong tin khac
                                    program.FILE_OUT_NAME = ""
                                    program.STORE_EXECUTE_IN = rtbStoreIn.Text.Trim
                                    program.STORE_EXECUTE_OUT = rtbStoreOut.Text.Trim
                                    program.TEMPLATE_NAME = ""
                                    program.TEMPLATE_TYPE_IN = ""
                                    program.TEMPLATE_TYPE_OUT = ""
                                    program.TEMPLATE_URL = ""

                                Else 'Report
                                    program.PROGRAM_TYPE = rcbProgramType.Text
                                    program.SQLLOADER_FILE = ""
                                    program.LINE_FROM = -1
                                    program.FILE_OUT_NAME = rtbFileOutName.Text.Trim
                                    program.DESCRIPTION = rtbProgramDesc.Text.Trim
                                    program.STORE_EXECUTE_IN = rtbStoreIn.Text.Trim
                                    program.STORE_EXECUTE_OUT = rtbStoreOut.Text.Trim
                                    program.TEMPLATE_NAME = ""
                                    program.TEMPLATE_TYPE_IN = rcbbTemplateTypeIn.SelectedValue
                                    program.TEMPLATE_TYPE_OUT = rcbbTemplateTypeOut.SelectedValue
                                    program.TEMPLATE_URL = rtbFileTemplateURL.Text.Trim
                                End If

                                program.PRIORITY = Decimal.Parse(RadNTBPriority.Text)
                                program.ORDERBY = -1
                                program.ORIENTATION = 0

                                If chkUse.Checked Then
                                    program.STATUS = 1
                                Else
                                    program.STATUS = 0
                                End If

                                program.PERMISSION = 0
                                program.CREATED_BY = Utilities.GetUsername
                                program.MODIFIED_BY = Utilities.GetUsername
                                program.CREATED_LOG = Utilities.GetUsername
                                program.MODIFIED_LOG = Utilities.GetUsername
                                program.OUTPUTFILE_SIZE = 100
                                program.NLS_LANGUAGE = "AL32UTF8"
                                program.NLS_TERRITORY = ">>>"
                                program.PRINTER = "P"
                                program.MODULE_ID = rcbbModule.SelectedValue
                                program.GROUP_ID = rcbbGroup.SelectedValue
                                program.PROGRAM_TYPE_ID = rcbProgramType.SelectedValue

                                Dim check = repCPR.Insert_Update_AD_Programs(program)
                                If check = 1 Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    BindData()
                                    FillDataToGridView()
                                    rgPrograms.Rebind()
                                    ClearAllInputControl()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                                End If

                            Case CommonMessage.STATE_EDIT
                                'If rgPrograms.SelectedItems.Count > 0 Then
                                '    Dim slItem As GridDataItem
                                '    slItem = rgPrograms.SelectedItems(0)
                                '    proID = Decimal.Parse(slItem("PROGRAM_ID").Text)
                                'Else
                                '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                                '    Exit Sub
                                'End If

                                If Not Page.IsValid Then
                                    Exit Sub
                                End If

                                proID = IDSelected

                                Dim repCPR As New CommonProgramsRepository
                                'Lấy tất cả thông tin trên form và update vào AD_PROGRAMS theo program da~ duoc selected
                                Dim program As New AD_PROGRAM_DTO
                                program.PROGRAM_ID = proID
                                program.CODE = rtbProgramCode.Text.Trim
                                program.NAME = rtbProgramName.Text.Trim

                                If rcbProgramType.Text = "Import" Then
                                    program.PROGRAM_TYPE = rcbProgramType.Text
                                    program.SQLLOADER_FILE = rtbSQLLoaderFile.Text.Trim
                                    program.LINE_FROM = RadNTBLineFrom.Value
                                Else
                                    program.PROGRAM_TYPE = rcbProgramType.Text
                                    program.SQLLOADER_FILE = ""
                                    program.LINE_FROM = -1
                                End If

                                program.DESCRIPTION = rtbProgramDesc.Text.Trim
                                program.STORE_EXECUTE_IN = rtbStoreIn.Text.Trim
                                program.STORE_EXECUTE_OUT = rtbStoreOut.Text.Trim
                                program.TEMPLATE_NAME = ""
                                program.TEMPLATE_TYPE_IN = rcbbTemplateTypeIn.SelectedValue
                                program.TEMPLATE_TYPE_OUT = rcbbTemplateTypeOut.SelectedValue
                                program.TEMPLATE_URL = rtbFileTemplateURL.Text.Trim
                                program.PRIORITY = Decimal.Parse(RadNTBPriority.Text)
                                program.ORDERBY = -1
                                program.ORIENTATION = 0

                                If chkUse.Checked Then
                                    program.STATUS = 1
                                Else
                                    program.STATUS = 0
                                End If

                                program.PERMISSION = 0
                                program.CREATED_BY = Utilities.GetUsername
                                program.MODIFIED_BY = Utilities.GetUsername
                                program.CREATED_LOG = Utilities.GetUsername
                                program.MODIFIED_LOG = Utilities.GetUsername
                                program.OUTPUTFILE_SIZE = 100
                                program.NLS_LANGUAGE = "AL32UTF8"
                                program.NLS_TERRITORY = ">>>"
                                program.PRINTER = "P"
                                program.FILE_OUT_NAME = rtbFileOutName.Text.Trim
                                program.MODULE_ID = rcbbModule.SelectedValue
                                program.GROUP_ID = rcbbGroup.SelectedValue
                                program.PROGRAM_TYPE_ID = rcbProgramType.SelectedValue

                                Dim check = repCPR.Insert_Update_AD_Programs(program)
                                If check = 1 Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    ClearAllInputControl()
                                    BindData()
                                    FillDataToGridView()
                                    rgPrograms.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                                End If
                        End Select
                        proID = -1
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgPrograms', 0 , 0, 17)")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            If isPhysical = 0 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server
                                Dim fInfo As New FileInfo(rtbSQLLoaderFile.Text.Trim)
                                If fInfo.Exists Then
                                    fInfo.Delete()
                                Else
                                    ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_FILE_NOT_DELETE), NotifyType.Warning)
                                End If
                            Else
                                If rtbSQLLoaderFile.Text.Trim = "" And rtbFileTemplateURL.Text.Trim = "" Then 'type: process
                                ElseIf rtbSQLLoaderFile.Text.Trim = "" And rtbFileTemplateURL.Text.Trim <> "" Then 'type:report
                                    Dim fInfo As New FileInfo(rtbFileTemplateURL.Text.Trim)
                                    If fInfo.Exists Then
                                        fInfo.Delete()
                                    Else
                                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_FILE_NOT_DELETE), NotifyType.Warning)
                                    End If
                                Else 'Type : Import
                                    Dim fInfo As New FileInfo(rtbSQLLoaderFile.Text.Trim)
                                    If fInfo.Exists Then
                                        fInfo.Delete()
                                    Else
                                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_FILE_NOT_DELETE), NotifyType.Warning)
                                    End If
                                End If
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                            End If

                            If rtbSQLLoaderFile.Text.Trim = "" Then  'Không upload file SQLLOADER nào cả thì normal
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                            Else 'Là loại Import --> Delete file
                                Dim fInfo As New FileInfo(rtbSQLLoaderFile.Text.Trim)
                                If fInfo.Exists Then
                                    fInfo.Delete()
                                End If
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                                ClearAllInputControl()
                            End If

                        Case CommonMessage.STATE_EDIT
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            ClearAllInputControl()
                    End Select

                Case CommonMessage.TOOLBARITEM_SEARCH
                    'repCPR = New CommonProgramsRepository
                    ''Lấy thông tin tìm kiếm trên form 
                    'Dim programSearch As New AD_PROGRAM_DTO
                    'programSearch.CODE = rtbProgramCode.Text
                    'programSearch.NAME = rtbProgramName.Text
                    'programSearch.STORE_EXECUTE_IN = rtbStoreIn.Text
                    'programSearch.STORE_EXECUTE_OUT = rtbStoreOut.Text
                    'programSearch.TEMPLATE_NAME = ""

                    'If rcbbModule.SelectedIndex = -1 Then
                    '    programSearch.MODULE_ID = -1
                    'Else
                    '    programSearch.MODULE_ID = Decimal.Parse(rcbbModule.SelectedValue)
                    'End If

                    'If rcbbGroup.SelectedIndex = -1 Then
                    '    programSearch.GROUP_ID = -1
                    'Else
                    '    programSearch.GROUP_ID = Decimal.Parse(rcbbGroup.SelectedValue)
                    'End If

                    'Dim dtSearch = repCPR.FillGridViewPrograms(programSearch.CODE, programSearch.NAME, programSearch.STORE_EXECUTE_IN, programSearch.STORE_EXECUTE_OUT,
                    '                                        programSearch.TEMPLATE_NAME, programSearch.MODULE_ID, programSearch.GROUP_ID)
                    'rgPrograms.DataSource = dtSearch
                    'rgPrograms.Rebind()

                    For Each column As GridColumn In rgPrograms.MasterTableView.Columns
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter
                        column.CurrentFilterValue = String.Empty
                    Next

                    rgPrograms.MasterTableView.FilterExpression = String.Empty
                    rgPrograms.MasterTableView.ClearEditItems()
                    rgPrograms.Rebind()

                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    'repCPR = New CommonProgramsRepository
                    ''CALL FUNCTION DELETE
                    'If rgPrograms.SelectedItems.Count > 0 Then
                    '    Dim slItem As GridDataItem
                    '    slItem = rgPrograms.SelectedItems(0)
                    '    IDSelected = Decimal.Parse(slItem("PROGRAM_ID").Text)
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'Dim check = repCPR.Delete_AD_Programs(IDSelected)
                    'If check = 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    'End If

                    'CurrentState = CommonMessage.STATE_NORMAL
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgPrograms_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPrograms.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            FillDataToGridView()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Fill dữ liệu lên tất cả control combobox trên form </summary>
    ''' <remarks></remarks>
    Private Sub FillDataAllCombobox()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        dtbProgramType = New DataTable
        dtbTemplateTypeIn = New DataTable
        dtbTemplateTypeOut = New DataTable
        dtbModule = New DataTable
        dtbGroup = New DataTable

        Try
            'Program type
            dtbProgramType = (New CommonProgramsRepository).FillComboboxProgramType()
            rcbProgramType.DataSource = dtbProgramType
            rcbProgramType.DataTextField = "NAME"
            rcbProgramType.DataValueField = "ID"
            rcbProgramType.DataBind()
            rcbProgramType.SelectedIndex = -1

            'Template type in
            dtbTemplateTypeIn = (New CommonProgramsRepository).FillComboboxTemplateTypeIn()
            rcbbTemplateTypeIn.DataSource = dtbTemplateTypeIn
            rcbbTemplateTypeIn.DataTextField = "NAME"
            rcbbTemplateTypeIn.DataValueField = "ID"
            rcbbTemplateTypeIn.DataBind()
            rcbbTemplateTypeIn.SelectedIndex = -1

            'Template type Out
            dtbTemplateTypeOut = (New CommonProgramsRepository).FillComboboxTemplateTypeOut()
            rcbbTemplateTypeOut.DataSource = dtbTemplateTypeOut
            rcbbTemplateTypeOut.DataTextField = "NAME"
            rcbbTemplateTypeOut.DataValueField = "ID"
            rcbbTemplateTypeOut.DataBind()
            rcbbTemplateTypeOut.SelectedIndex = -1

            'Module
            dtbModule = (New CommonProgramsRepository).FillComboboxModules()
            rcbbModule.DataSource = dtbModule
            rcbbModule.DataTextField = "NAME"
            rcbbModule.DataValueField = "ID"
            rcbbModule.DataBind()
            rcbbModule.SelectedIndex = -1

            'Group
            dtbGroup = (New CommonProgramsRepository).FillComboboxFunctionGroup()
            rcbbGroup.DataSource = dtbGroup
            rcbbGroup.DataTextField = "NAME"
            rcbbGroup.DataValueField = "ID"
            rcbbGroup.DataBind()
            rcbbGroup.SelectedIndex = -1

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Fill dữ liệu lên GridView Programs </summary>
    ''' <remarks>Nếu gọi từ NeedDataSource thì không cần</remarks>
    Private Sub FillDataToGridView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            '1. Khởi tạo DataSource cho GridView => tránh trường hợp DataSource bị null => GridView không create được
            Dim dtb As New DataTable
            dtb = CreateDataTable("AD_PROGRAMS")

            dtb = (New CommonProgramsRepository).FillGridViewPrograms("", "", "", "", "", -1, -1)

            '3. Fill DataSoure vào GridView
            If dtb IsNot Nothing Then
                rgPrograms.DataSource = dtb
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Create DataSource </summary>
    ''' <remarks>Nếu gọi từ NeedDataSource thì không cần</remarks>
    Private Function CreateDataTable(ByVal tableName As String) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtb As New DataTable

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtb.TableName = tableName
            dtb.Columns.Add("PROGRAM_ID", GetType(Decimal))
            dtb.Columns.Add("CODE", GetType(String))
            dtb.Columns.Add("NAME", GetType(String))
            dtb.Columns.Add("PROGRAM_TYPE", GetType(String))
            dtb.Columns.Add("DESCRIPTION", GetType(String))
            dtb.Columns.Add("STORE_EXECUTE_IN", GetType(String))
            dtb.Columns.Add("STORE_EXECUTE_OUT", GetType(String))
            dtb.Columns.Add("TEMPLATE_TYPE_IN", GetType(String))
            dtb.Columns.Add("TEMPLATE_TYPE_OUT", GetType(String))
            dtb.Columns.Add("TEMPLATE_NAME", GetType(String))
            dtb.Columns.Add("FILE_OUT_NAME", GetType(String))
            dtb.Columns.Add("TEMPLATE_URL", GetType(String))
            dtb.Columns.Add("PRIORITY", GetType(Decimal))
            dtb.Columns.Add("ORDERBY", GetType(Decimal))
            dtb.Columns.Add("ORIENTATION", GetType(Decimal))
            dtb.Columns.Add("STATUS", GetType(Decimal))
            dtb.Columns.Add("PERMISSION", GetType(Decimal))
            dtb.Columns.Add("MODULE_ID", GetType(Decimal))
            dtb.Columns.Add("MODULE_NAME", GetType(String))
            dtb.Columns.Add("GROUP_ID", GetType(Decimal))
            dtb.Columns.Add("GROUP_NAME", GetType(String))
            dtb.Columns.Add("PROGRAM_TYPE_ID", GetType(Decimal))
            dtb.Columns.Add("SQLLOADER_FILE", GetType(String))
            dtb.Columns.Add("LINE_FROM", GetType(Decimal))

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return dtb
    End Function

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CurrentState
                Case STATE_NORMAL
                    Utilities.EnabledGrid(rgPrograms, True, True)
                    rtbProgramName.ReadOnly = False
                    rtbProgramCode.ReadOnly = False
                    rcbProgramType.Enabled = False
                    rtbProgramDesc.ReadOnly = True
                    RadNTBPriority.ReadOnly = True
                    rtbStoreIn.ReadOnly = True
                    rtbStoreOut.ReadOnly = True
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = True
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rtbSQLLoaderFile.ReadOnly = True
                    btnUpload.Enabled = False
                    rbtSQLLoaderDownload.Enabled = False
                    rbtUpFileTemplate.Enabled = False
                    rbtDeleteTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    rcbbModule.Enabled = False
                    rcbbGroup.Enabled = False
                    RequiredFieldValidatorStoreIn.Enabled = True
                    RequiredFieldValidatorStoreOut.Enabled = True
                    RequiredFieldValidatorFileOutName.Enabled = True
                    RequiredFieldValidatorTypeIn.Enabled = True
                    RequiredFieldValidatorTypeOut.Enabled = True

                    CustomValidatorCode.Enabled = True
                    CustomValidatorName.Enabled = True

                    rtbProgramCode.Enabled = False
                    rtbProgramName.Enabled = False
                    RadNTBPriority.Enabled = False
                    RadNTBLineFrom.Enabled = False
                    chkUse.Enabled = False

                    rgPrograms.Enabled = True

                Case STATE_DEACTIVE
                    rtbProgramCode.ReadOnly = True
                    rcbProgramType.Enabled = False
                    rtbProgramDesc.ReadOnly = True
                    RadNTBPriority.ReadOnly = True
                    rtbStoreIn.ReadOnly = True
                    rtbStoreOut.ReadOnly = True
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = True
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rcbbModule.Enabled = False
                    rcbbGroup.Enabled = False
                    rtbSQLLoaderFile.ReadOnly = True
                    btnUpload.Enabled = False
                    rbtSQLLoaderDownload.Enabled = False
                    rbtUpFileTemplate.Enabled = False
                    rbtDeleteTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    RequiredFieldValidatorStoreIn.Enabled = True
                    RequiredFieldValidatorStoreOut.Enabled = True
                    RequiredFieldValidatorFileOutName.Enabled = True
                    RequiredFieldValidatorTypeIn.Enabled = True
                    RequiredFieldValidatorTypeOut.Enabled = True
                    CustomValidatorCode.Enabled = True
                    CustomValidatorName.Enabled = True

                    RadNTBPriority.Enabled = False
                    RadNTBLineFrom.Enabled = True
                    chkUse.Enabled = True

                Case STATE_EDIT
                    Utilities.EnabledGrid(rgPrograms, False, False)
                    rtbProgramCode.ReadOnly = True
                    rtbProgramName.ReadOnly = False
                    rcbProgramType.Enabled = True
                    rtbProgramDesc.ReadOnly = False
                    RadNTBPriority.ReadOnly = False
                    rtbStoreIn.ReadOnly = False
                    rtbStoreOut.ReadOnly = False
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = False
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rcbbModule.Enabled = True
                    rcbbGroup.Enabled = True
                    rbtUpFileTemplate.Enabled = True
                    rbtDeleteTemplate.Enabled = True
                    rtbDownloadTemplate.Enabled = True

                    RequiredFieldValidatorStoreIn.Enabled = False
                    RequiredFieldValidatorStoreOut.Enabled = False
                    RequiredFieldValidatorFileOutName.Enabled = False
                    RequiredFieldValidatorTypeIn.Enabled = False
                    RequiredFieldValidatorTypeOut.Enabled = False

                    CustomValidatorCode.Enabled = False
                    CustomValidatorName.Enabled = False

                    rtbProgramCode.Enabled = True
                    rtbProgramName.Enabled = True
                    RadNTBPriority.Enabled = True
                    RadNTBLineFrom.Enabled = True
                    chkUse.Enabled = True
                    rcbbTemplates.Enabled = True

                Case STATE_NEW
                    Utilities.EnabledGrid(rgPrograms, False, False)
                    ClearAllInputControl()
                    rtbProgramCode.ReadOnly = False
                    rtbProgramName.ReadOnly = False
                    rcbProgramType.Enabled = True
                    rtbProgramDesc.ReadOnly = False
                    RadNTBPriority.ReadOnly = False
                    rtbFileTemplateURL.ReadOnly = True
                    rtbDownloadTemplate.Enabled = False
                    rtbFileOutName.ReadOnly = False
                    rtbStoreIn.ReadOnly = True
                    rtbStoreOut.ReadOnly = True
                    rtbFileOutName.ReadOnly = True
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rbtUpFileTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    rcbbModule.Enabled = True
                    rcbbGroup.Enabled = True
                    RequiredFieldValidatorStoreIn.Enabled = False
                    RequiredFieldValidatorStoreOut.Enabled = False
                    RequiredFieldValidatorFileOutName.Enabled = False
                    RequiredFieldValidatorTypeIn.Enabled = False
                    RequiredFieldValidatorTypeOut.Enabled = False

                    CustomValidatorCode.Enabled = True
                    CustomValidatorName.Enabled = True

                    rtbProgramCode.Enabled = True
                    rtbProgramName.Enabled = True
                    RadNTBPriority.Enabled = True
                    RadNTBLineFrom.Enabled = True
                    chkUse.Enabled = True
            End Select

            ChangeToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Clear ctrl</summary>
    ''' <remarks></remarks>
    Protected Sub ClearAllInputControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rtbProgramCode.Text = ""
            rtbProgramName.Text = ""
            rtbProgramDesc.Text = ""
            rtbStoreIn.Text = ""
            rtbStoreOut.Text = ""
            chkUse.Checked = False
            rcbProgramType.SelectedIndex = -1
            rtbProgramDesc.Text = String.Empty
            rcbbTemplates.DataSource = Nothing
            rcbbTemplates.Items.Clear()
            RadNTBPriority.Value = 1
            rtbFileTemplateURL.Text = String.Empty
            rtbFileOutName.Text = String.Empty
            rcbbTemplateTypeIn.SelectedIndex = -1
            rcbbTemplateTypeOut.SelectedIndex = -1
            rcbbModule.SelectedIndex = -1
            rcbbGroup.SelectedIndex = -1
            rgPrograms.MasterTableView.ClearSelectedItems()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgPrograms_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If ((CurrentState <> CommonMessage.STATE_NEW And CurrentState <> CommonMessage.STATE_EDIT) And (rgPrograms.SelectedItems.Count = 0 Or rgPrograms.SelectedItems.Count > 1)) Then
                ClearAllInputControl()
            End If '
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load data vào textbox khi chọn checkbox ở grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgPrograms_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgPrograms.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim slItem As GridDataItem

        Try
            If rgPrograms.SelectedItems.Count > 0 Then
                'FillDataAllCombobox()

                'CurrentState = CommonMessage.STATE_DEACTIVE
                'UpdateControlState()

                If Not IsLoadDataRow Then
                    rcbProgramType.SelectedValue = ValueProgramType
                    Return
                End If

                slItem = rgPrograms.SelectedItems(0)
                'UpdateControlCustom(slItem("PROGRAM_TYPE").Text)
                IDSelected = Decimal.Parse(CDec(slItem.GetDataKeyValue("PROGRAM_ID")))
                rtbProgramCode.Text = slItem.GetDataKeyValue("CODE").ToString
                rtbProgramName.Text = slItem.GetDataKeyValue("NAME").ToString
                rtbProgramDesc.Text = slItem.GetDataKeyValue("DESCRIPTION").ToString
                rcbProgramType.SelectedValue = Decimal.Parse(CDec(slItem.GetDataKeyValue("PROGRAM_TYPE_ID")))
                rtbStoreIn.Text = slItem.GetDataKeyValue("STORE_EXECUTE_IN").ToString
                rtbStoreOut.Text = slItem.GetDataKeyValue("STORE_EXECUTE_OUT").ToString
                RadNTBPriority.Value = Decimal.Parse(CDec(slItem.GetDataKeyValue("PRIORITY")))
                rtbFileTemplateURL.Text = slItem.GetDataKeyValue("TEMPLATE_URL").ToString

                If slItem.GetDataKeyValue("PROGRAM_TYPE").ToString = "Report" Then
                    'fill danh sach template cho program report nay
                    Using rep As New HistaffFrameworkRepository
                        Dim ds = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_TEMPLATE_IN_PROGRAM", New List(Of Object)({IDSelected}))
                        dtTemplates = ds.Tables(0)
                    End Using

                    rcbbTemplates.DataSource = Nothing
                    rcbbTemplates.Items.Clear()
                    rcbbTemplates.DataSource = dtTemplates
                    rcbbTemplates.DataValueField = "VALUE"
                    rcbbTemplates.DataTextField = "VALUE"
                    rcbbTemplates.DataBind()
                    rcbbTemplates.SelectedIndex = 0
                    rcbbTemplates.Enabled = True
                End If

                rtbFileOutName.Text = slItem.GetDataKeyValue("FILE_OUT_NAME").ToString
                rcbbTemplateTypeIn.SelectedValue = slItem.GetDataKeyValue("TEMPLATE_TYPE_IN").ToString
                rcbbTemplateTypeOut.SelectedValue = slItem.GetDataKeyValue("TEMPLATE_TYPE_OUT").ToString
                rcbbModule.SelectedValue = Decimal.Parse(CDec(slItem.GetDataKeyValue("MODULE_ID")))
                mid = Decimal.Parse(CDec(slItem.GetDataKeyValue("MODULE_ID")))
                rcbbGroup.SelectedValue = Decimal.Parse(CDec(slItem.GetDataKeyValue("GROUP_ID")))
                chkUse.Checked = CType(slItem("STATUS").Controls(0), CheckBox).Checked
                rtbSQLLoaderFile.Text = slItem.GetDataKeyValue("SQLLOADER_FILE").ToString

                If Not IsNumeric(slItem.GetDataKeyValue("LINE_FROM")) Or (Decimal.Parse(CDec(slItem.GetDataKeyValue("LINE_FROM")))) = -1 Then
                    RadNTBLineFrom.Value = 1
                Else
                    RadNTBLineFrom.Value = (Decimal.Parse(CDec(slItem.GetDataKeyValue("LINE_FROM"))))
                End If

            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Check validation cho textbox rtbProgramCode</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CustomValidatorCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidatorCode.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            'Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_PROGRAMS",
            '                                       New List(Of Object)(New Object() {rtbProgramCode.Text.Trim, "", OUT_NUMBER}))

            'Dim rs = Decimal.Parse(obj(0).ToString)
            'If rs = 1 Then
            '   args.IsValid = True
            'Else
            '   args.IsValid = False
            'End If

            If Not db.CheckExistProgram("AD_PROGRAMS", "CODE", "'" & rtbProgramCode.Text.Trim & "'") Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Check validation cho textbox rtbProgramName</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CustomValidatorName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidatorName.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            'Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_PROGRAMS",
            '                                       New List(Of Object)(New Object() {"", rtbProgramName.Text.Trim, OUT_NUMBER}))

            'Dim rs = Decimal.Parse(obj(0).ToString)
            'If rs = 1 Then
            '   args.IsValid = True
            'Else
            '   args.IsValid = False
            'End If

            If Not db.CheckExistProgram("AD_PROGRAMS", "NAME", "'" & rtbProgramName.Text.Trim & "'") Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên thay đổi giá trị của combobox rcbProgramType</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rcbProgramType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbProgramType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not String.IsNullOrEmpty(rcbProgramType.Text) Then
                UpdateControlCustom(rcbProgramType.Text)
                ValueProgramType = rcbProgramType.SelectedValue
            Else
                ValueProgramType = String.Empty
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên click của button Updload</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlUpload1.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên click của button Download thực hiện download file template về </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtSQLLoaderDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtSQLLoaderDownload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If rcbbModule.Text = "" Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            Else
                mid = GetModule(rcbbModule.SelectedValue)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                                
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(PathControlFolder & mid, rtbSQLLoaderFile.Text.Trim)) '-- if the file exists on the server

                    If file.Exists Then 'set appropriate headers
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                        Response.AddHeader("Content-Length", file.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(file.FullName)
                        Response.End() 'if file does not exist
                    Else
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                    End If 'nothing in the URL as HTTP GET
                Else '0: lấy đường dẫn theo web server
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Control/" & mid), rtbSQLLoaderFile.Text.Trim)) '-- if the file exists on the server

                    If file.Exists Then 'set appropriate headers
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                        Response.AddHeader("Content-Length", file.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(file.FullName)
                        Response.End() 'if file does not exist
                    Else
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                    End If 'nothing in the URL as HTTP GET\
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên click [OK] của popup UpLoad File</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim repCPR = New CommonProgramsRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If rcbbModule.Text = "" Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            Else
                mid = GetModule(rcbbModule.SelectedValue)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server
                    If ctrlUpload1.UploadedFiles.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_VALIDATE_NUM_FILE_UPLOAD), NotifyType.Warning)
                        Exit Sub
                    Else
                        repCPR.CreateNewFolder(PathControlFolder & mid)
                        Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)

                        If file.GetExtension = ".txt" Or file.GetExtension = ".ctr" Then
                            fileName = System.IO.Path.Combine(PathControlFolder & mid, file.FileName)
                            file.SaveAs(fileName, True)
                            rtbSQLLoaderFile.Text = file.FileName
                        Else
                            ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_VALIDATE_EXTENTION_FILE), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                Else '0: lấy đường dẫn theo web server
                    If ctrlUpload1.UploadedFiles.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_VALIDATE_NUM_FILE_UPLOAD), NotifyType.Warning)
                        Exit Sub
                    Else
                        repCPR.CreateNewFolder(Server.MapPath("~/ReportTemplates/Control/" & mid))
                        Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)

                        If file.GetExtension = ".txt" Or file.GetExtension = ".ctr" Then
                            fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Control/" & mid), file.FileName)
                            file.SaveAs(fileName, True)
                            rtbSQLLoaderFile.Text = file.FileName
                        Else
                            ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_VALIDATE_EXTENTION_FILE), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên click của btn Upload File Template</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtUpFileTemplate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbtUpFileTemplate.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlUpload2.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiên click [OK] của popup UpLoad File</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim files As String = ""

        Try
            If rcbbModule.Text = "" Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            Else
                mid = GetModule(rcbbModule.SelectedValue)
                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server
                    If ctrlUpload2.UploadedFiles.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_NOT_CHOOSE_FILE), NotifyType.Warning)
                        Exit Sub
                    Else  'chon >= 1 file      
                        repCPR.CreateNewFolder(Framework.UI.Utilities.PathTemplateInFolder & mid)

                        For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                            Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                            Dim str = rtbFileTemplateURL.Text.Trim

                            If file.GetExtension = ".xls" Or file.GetExtension = ".xlsx" Or file.GetExtension = ".doc" Or file.GetExtension = ".docx" Then
                                If ctrlUpload2.UploadedFiles.Count = 1 Then  '1 file
                                    Dim fInfo As New FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName))

                                    If fInfo.Exists Then
                                        fInfo.Delete()
                                    End If

                                    fileName = System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName)
                                    file.SaveAs(fileName, True)

                                    If str = "" Then
                                        files = files & file.FileName
                                    Else
                                        If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                            files = file.FileName
                                        End If
                                    End If
                                Else
                                    If i = ctrlUpload2.UploadedFiles.Count - 1 Then
                                        Dim fInfo As New FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName))

                                        If fInfo.Exists Then
                                            fInfo.Delete()
                                        End If

                                        fileName = System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName)
                                        file.SaveAs(fileName, True)

                                        If str = "" Then
                                            files = files & file.FileName
                                        Else
                                            If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                                files = files & file.FileName
                                            Else
                                                files = files.Substring(0, files.Length - 1)
                                            End If
                                        End If
                                    Else 'nhung thang` truoc'
                                        Dim fInfo As New FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName))

                                        If fInfo.Exists Then
                                            fInfo.Delete()
                                        End If

                                        fileName = System.IO.Path.Combine(PathTemplateInFolder & mid, file.FileName)
                                        file.SaveAs(fileName, True)

                                        If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                            files = files & file.FileName & ","
                                        End If
                                    End If
                                End If
                            Else
                                ShowMessage(Translate(CM_CTRLPROGRAMS_NOT_EXTENTION_IMPORT_FILE), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                    End If
                Else '0: lấy đường dẫn theo web server
                    If ctrlUpload2.UploadedFiles.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_NOT_CHOOSE_FILE), NotifyType.Warning)
                        Exit Sub
                    Else
                        repCPR.CreateNewFolder(Server.MapPath("~/ReportTemplates/" & mid))
                        For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                            Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                            Dim str = rtbFileTemplateURL.Text.Trim

                            If file.GetExtension = ".xls" Or file.GetExtension = ".xlsx" Or file.GetExtension = ".xls" Or file.GetExtension = ".xlsx" Or file.GetExtension = ".doc" Then
                                If ctrlUpload2.UploadedFiles.Count = 1 Then  '1 file
                                    Dim fInfo As New FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName))

                                    If fInfo.Exists Then
                                        fInfo.Delete()
                                    End If

                                    fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName)
                                    file.SaveAs(fileName, True)

                                    If str = "" Then
                                        files = files & file.FileName
                                    Else
                                        If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                            files = file.FileName
                                        End If
                                    End If
                                Else
                                    If i = ctrlUpload2.UploadedFiles.Count - 1 Then
                                        Dim fInfo As New FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName))

                                        If fInfo.Exists Then
                                            fInfo.Delete()
                                        End If

                                        fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName)
                                        file.SaveAs(fileName, True)

                                        If str = "" Then
                                            files = files & file.FileName
                                        Else
                                            If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                                files = files & file.FileName
                                            Else
                                                files = files.Substring(0, files.Length - 1)
                                            End If
                                        End If
                                    Else 'nhung thang` truoc'
                                        Dim fInfo As New FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName))

                                        If fInfo.Exists Then
                                            fInfo.Delete()
                                        End If

                                        fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), file.FileName)
                                        file.SaveAs(fileName, True)

                                        If (str & ",").Contains(file.FileName & ",") = False Then 'neu file moi upload len k co trong chuoi~ thi add
                                            files = files & file.FileName & ","
                                        End If
                                    End If
                                End If
                            Else
                                ShowMessage(Translate(CM_CTRLPROGRAMS_NOT_EXTENTION_IMPORT_FILE), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                    End If
                End If
            End If

            If rtbFileTemplateURL.Text.Trim = "" Then
                rtbFileTemplateURL.Text = files
            ElseIf files = "" Then
                rtbFileTemplateURL.Text = rtbFileTemplateURL.Text.Trim
            Else
                rtbFileTemplateURL.Text = rtbFileTemplateURL.Text.Trim & "," & files
            End If
            'load lai combobox danh sach template
            FillTemplateCbbAgain(rtbFileTemplateURL.Text.Trim)
            UpdateTemplateInProgram(IDSelected, rtbFileTemplateURL.Text.Trim)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Cập nhật trạng thái các control custom</summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Private Sub UpdateControlCustom(ByVal type As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case type
                Case "Process"
                    rtbStoreIn.ReadOnly = False
                    rtbStoreOut.ReadOnly = False
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = True
                    rtbSQLLoaderFile.ReadOnly = True
                    btnUpload.Enabled = False
                    rbtSQLLoaderDownload.Enabled = False
                    rbtUpFileTemplate.Enabled = False
                    rbtDeleteTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    RadNTBLineFrom.ReadOnly = True
                    RequiredFieldValidatorStoreIn.Enabled = True
                    RequiredFieldValidatorStoreOut.Enabled = True
                    RequiredFieldValidatorFileOutName.Enabled = False
                    RequiredFieldValidatorTypeIn.Enabled = False
                    RequiredFieldValidatorTypeOut.Enabled = False

                    rtbStoreIn.Enabled = True
                    rtbStoreOut.Enabled = True
                    rtbSQLLoaderFile.Enabled = False

                Case "Import"
                    rtbStoreIn.ReadOnly = False
                    rtbStoreOut.ReadOnly = False
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = True
                    rtbSQLLoaderFile.ReadOnly = False
                    btnUpload.Enabled = True
                    rbtSQLLoaderDownload.Enabled = True
                    rbtUpFileTemplate.Enabled = False
                    rbtDeleteTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    RadNTBLineFrom.ReadOnly = False
                    RequiredFieldValidatorStoreIn.Enabled = True
                    RequiredFieldValidatorStoreOut.Enabled = True
                    RequiredFieldValidatorFileOutName.Enabled = False
                    RequiredFieldValidatorTypeIn.Enabled = False
                    RequiredFieldValidatorTypeOut.Enabled = False

                    rtbStoreIn.Enabled = True
                    rtbStoreOut.Enabled = True
                    rtbSQLLoaderFile.Enabled = True

                Case "Report"
                    rtbProgramDesc.ReadOnly = False
                    rtbProgramName.ReadOnly = False
                    rcbbTemplateTypeIn.Enabled = True
                    rcbbTemplateTypeOut.Enabled = True
                    rtbFileTemplateURL.ReadOnly = True
                    rtbFileOutName.ReadOnly = False
                    rtbSQLLoaderFile.ReadOnly = True
                    btnUpload.Enabled = False
                    rbtSQLLoaderDownload.Enabled = False
                    rbtUpFileTemplate.Enabled = True
                    rbtDeleteTemplate.Enabled = True
                    rtbDownloadTemplate.Enabled = True
                    RadNTBLineFrom.ReadOnly = True
                    RequiredFieldValidatorStoreIn.Enabled = True
                    RequiredFieldValidatorStoreOut.Enabled = True
                    RequiredFieldValidatorFileOutName.Enabled = True
                    RequiredFieldValidatorTypeIn.Enabled = True
                    RequiredFieldValidatorTypeOut.Enabled = True

                    rtbStoreIn.ReadOnly = False
                    rtbStoreOut.ReadOnly = False
                    rtbStoreIn.Enabled = True
                    rtbStoreOut.Enabled = True
                    rtbSQLLoaderFile.Enabled = False

                Case "Export"
                    rtbProgramDesc.ReadOnly = False
                    rtbProgramName.ReadOnly = False
                    rbtUpFileTemplate.Enabled = False
                    rbtDeleteTemplate.Enabled = False
                    rtbDownloadTemplate.Enabled = False
                    rcbbTemplateTypeIn.Enabled = False
                    rcbbTemplateTypeOut.Enabled = False
                    rtbFileOutName.Enabled = False
                    rtbStoreIn.Enabled = False
                    rtbStoreOut.Enabled = False
                    rtbSQLLoaderFile.Enabled = False
                    RequiredFieldValidatorStoreIn.Enabled = False
                    RequiredFieldValidatorStoreOut.Enabled = False
                    RequiredFieldValidatorFileOutName.Enabled = False
                    RequiredFieldValidatorTypeIn.Enabled = False
                    RequiredFieldValidatorTypeOut.Enabled = False

                    rtbStoreIn.Enabled = False
                    rtbStoreOut.Enabled = False
                    rtbSQLLoaderFile.Enabled = False
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiện click của button download file Template</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtbDownloadTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtbDownloadTemplate.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rcbbModule.Text = "" Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            Else
                mid = GetModule(rcbbModule.SelectedValue)
                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                                
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, rcbbTemplates.Text)) '-- if the file exists on the server

                    If file.Exists Then 'set appropriate headers
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                        Response.AddHeader("Content-Length", file.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(file.FullName)
                        Response.End() 'if file does not exist
                    Else
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                    End If 'nothing in the URL as HTTP GET\
                Else '0: lấy đường dẫn theo web server
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), rcbbTemplates.Text)) '-- if the file exists on the server

                    If file.Exists Then 'set appropriate headers
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                        Response.AddHeader("Content-Length", file.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(file.FullName)
                        Response.End() 'if file does not exist
                    Else
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                    End If 'nothing in the URL as HTTP GET\
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiện click của button Xóa file Template </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtDeleteTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtDeleteTemplate.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlMessageBox.MessageText = Translate(CommonMessage.CM_CTRLPROGRAM_MESSAGE_BOX_DELETE_THIS_TEMPLATE)
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Sự kiện xử lý khi click với popup Warning</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server
                If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    Dim fInfo As New FileInfo(System.IO.Path.Combine(PathTemplateInFolder & mid, rcbbTemplates.Text))

                    If fInfo.Exists Then
                        fInfo.Delete()
                        FillListTemplate(rcbbTemplates.Text)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        FillListTemplate(rcbbTemplates.Text)
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_EXSIXT_FILE), NotifyType.Warning)
                    End If
                End If
            Else
                If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    Dim fInfo As New FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/" & mid), rcbbTemplates.Text))

                    If fInfo.Exists Then
                        fInfo.Delete()
                        FillListTemplate(rcbbTemplates.Text)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        FillListTemplate(rcbbTemplates.Text)
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_EXSIXT_FILE), NotifyType.Warning)
                    End If
                End If
            End If

            FillTemplateCbbAgain(rtbFileTemplateURL.Text.Trim)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>
    ''' Đổ dữ liệu từ template vào combobox
    ''' </summary>
    ''' <param name="strTemplates"></param>
    ''' <remarks></remarks>
    Private Sub FillTemplateCbbAgain(ByVal strTemplates As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim ds = rep.ExecuteToDataSet("PKG_HCM_SYSTEM.READ_TEMPLATE_WITH_TEMPLATESTR", New List(Of Object)({strTemplates}))
            dtTemplates = ds.Tables(0)
            rcbbTemplates.DataSource = Nothing
            rcbbTemplates.Items.Clear()
            rcbbTemplates.DataSource = dtTemplates
            rcbbTemplates.DataValueField = "VALUE"
            rcbbTemplates.DataTextField = "VALUE"
            rcbbTemplates.DataBind()
            rcbbTemplates.SelectedIndex = 0
            rcbbTemplates.Enabled = True

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Đổ dữ liệu từ template vào combobox</summary>
    ''' <param name="strTemplate"></param>
    ''' <remarks></remarks>
    Private Sub FillListTemplate(ByVal strTemplate As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim newString As String

        Try
            newString = (rtbFileTemplateURL.Text.Trim & ",").Replace(strTemplate & ",", "")

            If newString <> "" Then
                newString = newString.Replace(",,", ",")

                If newString.Substring(0, 1) = "," Then
                    newString = newString.Substring(1, newString.Length - 1)
                ElseIf newString.Substring(newString.Length - 1, 1) = "," Then
                    newString = newString.Substring(0, newString.Length - 2)
                End If

                rtbFileTemplateURL.Text = newString
            Else
                rtbFileTemplateURL.Text = ""
            End If

            UpdateTemplateInProgram(IDSelected, rtbFileTemplateURL.Text.Trim)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Cập nhật templates định nghĩa chương trinh </summary>
    ''' <param name="programID"></param>
    ''' <param name="Templates"></param>
    ''' <remarks></remarks>
    Private Sub UpdateTemplateInProgram(ByVal programID As Decimal, ByVal Templates As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.UPDATE_AD_PROGRAM_TEMPLATES",
                                                  New List(Of Object)(New Object() {programID, Templates, OUT_NUMBER}))
            Dim isUpdated = Decimal.Parse(obj(0).ToString)

            If isUpdated = 1 Then
                ShowMessage(Translate(CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_SUCCESS), NotifyType.Success)
            Else
                ShowMessage(Translate(CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_FAILED), NotifyType.Warning)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Lấy ra danh sách modules</summary>
    ''' <param name="moduleId"></param>
    ''' <remarks></remarks>
    Private Function GetModule(ByVal moduleId As Decimal) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.PRR_GET_MODULE",
                                                   New List(Of Object)(New Object() {moduleId, OUT_STRING}))
            mid = obj(0).ToString

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

        Return mid
    End Function

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Tạo thư mục</summary>
    ''' <param name="url"></param>
    ''' <remarks></remarks>
    Public Sub CreateFolder(ByVal url As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If rcbbModule.Text = "" Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE), NotifyType.Warning)
                Exit Sub
            Else
                mid = GetModule(rcbbModule.SelectedValue)

                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server    
                    '1 - Tiến hành tạo folder
                    Directory.CreateDirectory(url)
                Else '0: lấy đường dẫn theo web server
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Control/" & mid), rtbSQLLoaderFile.Text.Trim)) '-- if the file exists on the server

                    If file.Exists Then 'set appropriate headers
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                        Response.AddHeader("Content-Length", file.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(file.FullName)
                        Response.End() 'if file does not exist
                    Else
                        ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE), NotifyType.Warning)
                    End If 'nothing in the URL as HTTP GET\
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Kiểm tra mã chương trình</summary>
    ''' <remarks></remarks>
    Private Function Check_Code()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_PROGRAMS",
                                                   New List(Of Object)(New Object() {rtbProgramCode.Text.Trim, "", OUT_NUMBER}))

            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                Return True
            Else
                ShowMessage(Translate(CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_CODE), NotifyType.Warning)
                Return False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Kiểm tra tên chương trình</summary>
    ''' <remarks></remarks>
    Private Function Check_ProgramName()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_PROGRAMS",
                                                   New List(Of Object)(New Object() {"", rtbProgramName.Text.Trim, OUT_NUMBER}))

            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                Return True
            Else
                ShowMessage(Translate(CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_NAME), NotifyType.Warning)
                Return False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function

#End Region

End Class