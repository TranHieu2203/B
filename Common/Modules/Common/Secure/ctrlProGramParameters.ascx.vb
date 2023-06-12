Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlProGramParameters
    Inherits CommonView

    ''' <summary>
    ''' rCbb
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents rCbb As RadComboBox

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' repCPR
    ''' </summary>
    ''' <remarks></remarks>
    Private repCPR As CommonProgramsRepository

#Region "Property"

    ''' <summary>
    ''' rgParaCheck
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property rgParaCheck As Boolean
        Get
            Return PageViewState(Me.ID & "_rgParaCheck")
        End Get

        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_rgParaCheck") = value
        End Set
    End Property

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
    ''' programCode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property programCode As String
        Get
            Return PageViewState(Me.ID & "_programCode")
        End Get

        Set(ByVal value As String)
            PageViewState(Me.ID & "_programCode") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbValueSet
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbValueSet As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbValueSet")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbValueSet") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbDataType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbDataType As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbDataType")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbDataType") = value
        End Set
    End Property

    ''' <summary>
    ''' dtbGridViewParameters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property dtbGridViewParameters As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbGridViewParameters")
        End Get

        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbGridViewParameters") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>27/07/2017</lastupdate>
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
                                 ToolbarItem.Delete)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgProgramParameters.SetFilter()
            If Not Page.IsPostBack Then
                rgProgramParameters.Rebind()
            End If
            'LoadAllParameterInRequest()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Get lấy tham số ,Cập nhật trang thái của ctrl </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetParams()
            CurrentState = CommonMessage.STATE_NORMAL
            rtbProgramCode.Text = programCode
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary> Sự kiện khi select row trên grid </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgProgramParameters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgProgramParameters.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If rgProgramParameters.SelectedItems.Count > 0 Then
                FillDataAllCombobox()

                Dim slItem As GridDataItem
                slItem = rgProgramParameters.SelectedItems(0)
                IDSelected = Decimal.Parse(CDec(slItem.GetDataKeyValue("DESCRIPTIVE_FLEX_COLUMN_ID")))
                rtbProgramCode.Text = programCode
                rtbParameterName.Text = slItem.GetDataKeyValue("LABEL_COLUMN_NAME").ToString
                'rtbReportField.Text = slItem.GetDataKeyValue("APPLICATION_COLUMN_NAME")
                rcbValueSet.SelectedValue = Decimal.Parse(CDec(slItem.GetDataKeyValue("FLEX_VALUE_SET_ID")))
                rtbParaDesc.Text = slItem.GetDataKeyValue("DESCRIPTION").ToString
                RadNTBSequence.Value = Decimal.Parse(CDec(slItem.GetDataKeyValue("SEQUENCE")))
                rcbbDataType.SelectedValue = Decimal.Parse(CDec(slItem.GetDataKeyValue("DATA_TYPE_ID")))
                cbRequire.Checked = CType(slItem("IS_REQUIRE").Controls(0), CheckBox).Checked
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New HistaffFrameworkRepository
        Dim paraID As Decimal = -1
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rec As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW  'begin create program with all information in form
                    'setup any Radcombobox in form                    
                    'rgParaCheck = False
                    FillDataAllCombobox()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If IDSelected = 0 Then
                        IDSelected = 0
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rgProgramParameters.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    CurrentState = CommonMessage.STATE_EDIT
                    'rgParaCheck = False
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SAVE
                    'Neu la New => insert vao AD_DESC_FLEX_COL vs id  = -1
                    'Neu la Edit => Update vao AD_DESC_FLEX_COL vs id = idSelected in gridview
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW

                            If Not Page.IsValid Then
                                Exit Sub
                            End If

                            Dim repCPR As New CommonProgramsRepository
                            'Lấy tất cả thông tin trên form và insert vào AD_PROGRAMS   
                            Dim parameter As New PARAMETERS_DTO
                            parameter.DESCRIPTIVE_FLEX_COLUMN_ID = paraID
                            parameter.FLEX_VALUE_SET_ID = rcbValueSet.SelectedValue
                            parameter.DESCRIPTIVE_FLEXFIELD_NAME = programCode
                            parameter.APPLICATION_COLUMN_NAME = "Nothing"

                            'If Check_ParameterName() Then
                            '    parameter.LABEL_COLUMN_NAME = rtbParameterName.Text.Trim
                            'Else
                            '    Exit Sub
                            'End If

                            'If Check_Sequence() Then
                            '    parameter.SEQUENCE = RadNTBSequence.Value
                            'Else
                            '    Exit Sub
                            'End If

                            parameter.LABEL_COLUMN_NAME = rtbParameterName.Text.Trim
                            parameter.SEQUENCE = RadNTBSequence.Value
                            parameter.APPLICATION_ID = 101
                            parameter.DESCRIPTION = rtbParaDesc.Text.Trim

                            Select Case rcbbDataType.Text
                                Case "Kiểu chuỗi"
                                    parameter.TYPE_FIELD = "NVARCHAR2"
                                Case "Kiểu số"
                                    parameter.TYPE_FIELD = "DECIMAL"
                                Case "Kiểu ngày giờ"
                                    parameter.TYPE_FIELD = "DATE"
                            End Select

                            If cbRequire.Checked Then
                                parameter.IS_REQUIRE = 1
                            Else
                                parameter.IS_REQUIRE = 0
                            End If
                            'Process : Insert
                            Dim check = repCPR.Insert_Update_AD_Program_Parameters(parameter)
                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                CurrentState = CommonMessage.STATE_NORMAL
                                BindData()
                                FillDataToGridView()
                                rgProgramParameters.Rebind()
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                                CurrentState = CommonMessage.STATE_NORMAL
                                rgProgramParameters.Rebind()
                                UpdateControlState()
                            End If

                        Case CommonMessage.STATE_EDIT
                            If Not Page.IsValid Then
                                Exit Sub
                            End If

                            paraID = IDSelected
                            repCPR = New CommonProgramsRepository
                            'Lấy tất cả thông tin trên form và update vào AD_DESC_FLEX_COL theo parameter da~ duoc selected
                            Dim parameter As New PARAMETERS_DTO
                            parameter.DESCRIPTIVE_FLEX_COLUMN_ID = paraID
                            parameter.FLEX_VALUE_SET_ID = rcbValueSet.SelectedValue
                            parameter.DESCRIPTIVE_FLEXFIELD_NAME = programCode
                            parameter.APPLICATION_COLUMN_NAME = "Nothing"
                            parameter.LABEL_COLUMN_NAME = rtbParameterName.Text.Trim
                            parameter.SEQUENCE = RadNTBSequence.Value
                            parameter.APPLICATION_ID = 101
                            parameter.DESCRIPTION = rtbParaDesc.Text.Trim

                            Select Case rcbbDataType.Text
                                Case "Kiểu chuỗi"
                                    parameter.TYPE_FIELD = "NVARCHAR2"
                                Case "Kiểu số"
                                    parameter.TYPE_FIELD = "DECIMAL"
                                Case "Kiểu ngày giờ"
                                    parameter.TYPE_FIELD = "DATE"
                            End Select

                            If cbRequire.Checked Then
                                parameter.IS_REQUIRE = 1
                            Else
                                parameter.IS_REQUIRE = 0
                            End If
                            'Process : Update
                            Dim check = repCPR.Insert_Update_AD_Program_Parameters(parameter)

                            If check = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                'rgParaCheck = True
                                CurrentState = CommonMessage.STATE_NORMAL
                                BindData()
                                FillDataToGridView()
                                rgProgramParameters.Rebind()
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                                'rgParaCheck = True
                                CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                            End If

                            paraID = -1
                    End Select

                Case CommonMessage.TOOLBARITEM_CANCEL
                    'rgParaCheck = True
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearAllInputControl()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If IDSelected = 0 Then
                        IDSelected = 0
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgProgramParameters_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgProgramParameters.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            FillDataToGridView()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary> 
    ''' Event Yes/No messagebox hoi Xoa du lieu
    ''' Update lai trang thai control sau khi process Xoa
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
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgProgramParameters_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If (CurrentState <> CommonMessage.STATE_NEW And (rgProgramParameters.SelectedItems.Count = 0 Or rgProgramParameters.SelectedItems.Count > 1)) Then
                ClearAllInputControl()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Fill dữ liệu lên tất cả control combobox trên form</summary>
    ''' <remarks></remarks>
    Private Sub FillDataAllCombobox()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            dtbValueSet = New DataTable
            'get data in server
            'Program type
            dtbValueSet = (New CommonProgramsRepository).FillComboboxValueSet()
            rcbValueSet.DataSource = dtbValueSet
            rcbValueSet.DataTextField = "NAME"
            rcbValueSet.DataValueField = "ID"
            rcbValueSet.DataBind()
            rcbValueSet.SelectedIndex = -1

            'Template type in
            dtbDataType = New DataTable
            dtbDataType = (New CommonProgramsRepository).FillComboboxDataType()
            rcbbDataType.DataSource = dtbDataType
            rcbbDataType.DataTextField = "NAME"
            rcbbDataType.DataValueField = "ID"
            rcbbDataType.DataBind()
            rcbbDataType.SelectedIndex = -1

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Fill dữ liệu lên GridView Programs</summary>
    ''' <remarks></remarks>
    Private Sub FillDataToGridView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            '1. Khởi tạo DataSource cho GridView => tránh trường hợp DataSource bị null => GridView không create được
            Dim dtb As New DataTable
            dtb = CreateDataTable("AD_DESC_FLEX_COL")
            '2. Get dữ liệu từ đầu đó vào DataSoure              
            dtb = (New CommonProgramsRepository).FillGridViewProgramParameters(programCode)
            '3. Fill DataSoure vào GridView
            If dtb IsNot Nothing Then
                rgProgramParameters.DataSource = dtb
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Create DataSource </summary>
    ''' <remarks>Nếu gọi từ NeedDataSource thì không cần</remarks>
    Private Function CreateDataTable(ByVal tableName As String) As DataTable
        Dim dtb As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            dtb.TableName = tableName
            dtb.Columns.Add("DESCRIPTIVE_FLEX_COLUMN_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_ID", GetType(Decimal))
            dtb.Columns.Add("FLEX_VALUE_SET_NAME", GetType(String))
            dtb.Columns.Add("DESCRIPTIVE_FLEXFIELD_NAME", GetType(String))
            dtb.Columns.Add("APPLICATION_COLUMN_NAME", GetType(String))
            dtb.Columns.Add("LABEL_COLUMN_NAME", GetType(String))
            dtb.Columns.Add("SEQUENCE", GetType(Decimal))
            dtb.Columns.Add("DESCRIPTION", GetType(String))
            dtb.Columns.Add("TYPE_FIELD", GetType(String))
            dtb.Columns.Add("IS_REQUIRE", GetType(Decimal))

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return dtb
    End Function

    ''' <lastupdate>27/07/2017</lastupdate>
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
                    EnabledGrid(rgProgramParameters, True)
                    'rgProgramParameters.Enabled = rgParaCheck
                    rtbProgramCode.ReadOnly = True
                    CustomValidatorParaName.Enabled = True
                    CustomValidatorSequence.Enabled = True

                    EnableControlAll(False, rtbParameterName, rcbValueSet, RadNTBSequence, rcbbDataType, rtbParaDesc, cbRequire, rcbValueSet)

                Case STATE_EDIT
                    EnabledGrid(rgProgramParameters, False, False)
                    'rgProgramParameters.Enabled = rgParaCheck
                    rtbParameterName.ReadOnly = True
                    'rtbReportField.ReadOnly = True
                    rtbProgramCode.ReadOnly = True
                    CustomValidatorParaName.Enabled = False
                    CustomValidatorSequence.Enabled = False

                    EnableControlAll(True, rtbParameterName, rcbValueSet, RadNTBSequence, rcbbDataType, rtbParaDesc, cbRequire, rcbValueSet)

                Case STATE_NEW
                    ClearAllInputControl()
                    EnabledGrid(rgProgramParameters, False, False)
                    'rgProgramParameters.Enabled = rgParaCheck
                    rtbProgramCode.ReadOnly = True
                    CustomValidatorParaName.Enabled = True
                    CustomValidatorSequence.Enabled = True

                    EnableControlAll(True, rtbParameterName, rcbValueSet, RadNTBSequence, rcbbDataType, rtbParaDesc, cbRequire, rcbValueSet)
                Case STATE_DELETE
                    repCPR = New CommonProgramsRepository
                    'CALL FUNCTION DELETE
                    If IDSelected = 0 Then
                        IDSelected = 0
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim check = repCPR.Delete_AD_Desc_Flex_Col(IDSelected)

                    If check = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        'rgParaCheck = True
                        BindData()
                        FillDataToGridView()
                        rgProgramParameters.Rebind()
                        'Response.Redirect(Request.RawUrl)
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        'rgParaCheck = True
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If

                    IDSelected = 0
            End Select

            ChangeToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Clear ctrl</summary>
    ''' <remarks></remarks>
    Protected Sub ClearAllInputControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rtbParameterName.Text = String.Empty
            'rtbReportField.Text = String.Empty
            rcbValueSet.SelectedIndex = -1
            RadNTBSequence.Value = 1
            rcbbDataType.SelectedIndex = -1
            rtbParaDesc.Text = String.Empty
            cbRequire.Checked = True
            rcbValueSet.SelectedIndex = -1

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#Region "Custom report function - sub"

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Get tham số từ URL</summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState Is Nothing Then
                If Request.Params("programCode") IsNot Nothing Then
                    programCode = Request.Params("programCode")
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Check validation cho text box rtbParameterName</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CustomValidatorParaName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidatorParaName.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Dim db As New CommonRepository

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, rtbParameterName.Text.Trim, "", -1, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If

            'If db.CheckExistProgram("AD_DESCR_FLEX_COL", "NAME", "' " & rtbParameterName.Text.Trim & "'") Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            'End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Check validation cho text number box RadNTBSequence </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CustomValidatorSequence_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidatorSequence.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository
        Dim db As New CommonRepository

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, "", "", RadNTBSequence.Value, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If

            'If db.CheckExistProgram("AD_DESCR_FLEX_COL", "SEQUENCE", RadNTBSequence.Value) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            'End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi text box rtbParameterName thay đổi</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtbParameterName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtbParameterName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, rtbParameterName.Text.Trim, "", -1, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
            Else
                'ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME), NotifyType.Warning)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Bắt sự kiện khi text number box RadNTBSequence thay đổi</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadNTBSequence_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadNTBSequence.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, "", "", RadNTBSequence.Value, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
            Else
                'ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE), NotifyType.Warning)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Kiểm tra ParameterName</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Check_ParameterName()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository

        Try
            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, rtbParameterName.Text.Trim, "", -1, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                Return True
            Else
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME), NotifyType.Warning)
                Return False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function

    ''' <lastupdate>27/07/2017</lastupdate>
    ''' <summary>Kiểm tra Sequence</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Check_Sequence()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New HistaffFrameworkRepository

        Try

            Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.CHECK_AD_DESCR_FLEX_COL", New List(Of Object)(New Object() {programCode, "", "", RadNTBSequence.Value, OUT_NUMBER}))
            Dim rs = Decimal.Parse(obj(0).ToString)

            If rs = 1 Then
                Return True
            Else
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE), NotifyType.Warning)
                Return False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function

#End Region

#End Region

End Class