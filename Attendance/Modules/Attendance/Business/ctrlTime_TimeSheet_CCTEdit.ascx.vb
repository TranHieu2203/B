Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlTime_TimeSheet_CCTEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    Property Period_id As Integer
        Get
            Return ViewState(Me.ID & "_Period_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Period_id") = value
        End Set
    End Property

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property
    Property RegisterLeave As AT_TIME_TIMESHEET_DAILYDTO
        Get
            Return ViewState(Me.ID & "_AT_TIME_TIMESHEET_DAILYDTO")
        End Get
        Set(ByVal value As AT_TIME_TIMESHEET_DAILYDTO)
            ViewState(Me.ID & "_AT_TIME_TIMESHEET_DAILYDTO") = value
        End Set
    End Property

    Property Shift As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_AT_SHIFTDTO")
        End Get
        Set(ByVal value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_AT_SHIFTDTO") = value
        End Set
    End Property
    Property Sign As List(Of AT_FMLDTO)
        Get
            Return ViewState(Me.ID & "_AT_FMLDTO")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
            ViewState(Me.ID & "_AT_FMLDTO") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Protected Property ListSign As List(Of AT_FMLDTO)
        Get
            Return PageViewState(Me.ID & "_ListSign")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
            PageViewState(Me.ID & "_ListSign") = value
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
            Refresh()
            UpdateControlState()
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
            InitControl()
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
        Dim rep As New AttendanceRepository
        Try
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_TIME_TIMESHEET_DAILYDTO
                    obj.EMPLOYEE_ID = Request.Params("EMPLOYEE_ID")
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim Format = "dd/MM/yyyy"
                    obj.WORKINGDAY = Date.ParseExact(Request.Params("WORKINGDAY"), Format, CultureInfo.InvariantCulture)
                    obj.MANUAL_CODE = Request.Params("ManualCode")
                    obj = rep.GetTimeSheetDailyById(obj)
                    RegisterLeave = New AT_TIME_TIMESHEET_DAILYDTO
                    If obj IsNot Nothing Then
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        Employee_id = obj.EMPLOYEE_ID
                        txtChucDanh.Text = obj.TITLE_NAME
                        txtDonVi.Text = obj.ORG_NAME
                        txtName.Text = obj.VN_FULLNAME
                        If obj.MANUAL_ID IsNot Nothing Then
                            cboTypeManual.SelectedValue = obj.MANUAL_ID
                        End If
                        If obj.SHIFT_ID IsNot Nothing Then
                            txtShiftCode.Text = obj.SHIFT_NAME
                        End If
                        rdFromDay.SelectedDate = obj.WORKINGDAY
                        rdToDay.SelectedDate = obj.WORKINGDAY
                        _Value = obj.ID
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
    ''' Load data for combobox kiểu công
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
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
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_TIME_TIMESHEET_DAILYDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    'If _Value.HasValue Then
                    '    Dim fday As Date?
                    '    Dim tday As Date?
                    '    fday = Date.Parse(rdFromDay.SelectedDate)
                    '    tday = Date.Parse(rdToDay.SelectedDate)

                    '    Dim _param = New ParamDTO With {.EMPLOYEE_CODE = txtEmployeeCode.Text.Trim, _
                    '                                    .FROMDATE = fday, _
                    '                                    .ENDDATE = tday}
                    '    ' kiem tra ky cong da dong chua?
                    '    If rep.IS_PERIODSTATUS_BY_DATE(_param) = False Then
                    '        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm/sửa"), NotifyType.Error)
                    '        Exit Sub
                    '    End If
                    '    While fday <= tday
                    '        obj = New AT_TIME_TIMESHEET_DAILYDTO
                    '        obj.EMPLOYEE_ID = Employee_id
                    '        obj.WORKINGDAY = fday
                    '        If Not String.IsNullOrEmpty(cboTypeManual.SelectedValue) Then
                    '            obj.MANUAL_ID = cboTypeManual.SelectedValue
                    '            obj.MANUAL_CODE = cboTypeManual.Text
                    '        End If
                    '        rep.ModifyLeaveSheetDaily(obj, gstatus)
                    '        fday = fday.Value.AddDays(1)
                    '    End While
                    'End If

                    obj = New AT_TIME_TIMESHEET_DAILYDTO
                    If IsNumeric(Request.Params("PERIOD_ID")) Then
                        obj.PERIOD_ID = Request.Params("PERIOD_ID")
                    End If
                    obj.EMPLOYEE_ID = Employee_id
                    obj.FROM_DATE = rdFromDay.SelectedDate
                    obj.END_DATE = rdToDay.SelectedDate
                    obj.MANUAL_ID = cboTypeManual.SelectedValue
                    If rep.ModifyLeaveSheetDaily(obj, gstatus) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage("Đã có lỗi xảy ra khi cập nhật.", NotifyType.Warning)
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
    ''' Validate combobox kiểu công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTypeManual_ServerValidate(ByVal sender As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTypeManual.ServerValidate
        Dim rep As New AttendanceRepository
        Dim validate As New AT_TIME_MANUALDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If cboTypeManual.SelectedValue Is Nothing Then Return
        If cboTypeManual.SelectedValue = "" Then Return
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboTypeManual.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateAT_TIME_MANUAL(validate)
            End If
            If Not args.IsValid Then
                ListComboData = Nothing
                GetDataCombo()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Load data for combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_MANUAL = True
                ListComboData.GET_LIST_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboTypeManual, ListComboData.LIST_LIST_TYPE_MANUAL, "NAME_VN", "ID", True)
            'FillRadCombobox(cboShiftCode, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_TYPE_MANUAL.Count > 0 Then
                cboTypeManual.SelectedIndex = 0
            End If
            'If ListComboData.LIST_LIST_SHIFT.Count > 0 Then
            '    cboShiftCode.SelectedIndex = 0
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class

