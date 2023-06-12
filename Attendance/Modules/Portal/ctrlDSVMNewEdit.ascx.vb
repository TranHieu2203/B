Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDSVMNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property EmployeeID As Decimal
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    Property RegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO)
        Get
            Return ViewState(Me.ID & "_objRegisterDMVSList")
        End Get
        Set(ByVal value As List(Of AT_LATE_COMBACKOUTDTO))
            ViewState(Me.ID & "_objRegisterDMVSList") = value
        End Set
    End Property

    Protected Property EmployeeDto As DataTable
        Get
            Return PageViewState(Me.ID & "_EmployeeDto")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_EmployeeDto") = value
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

    Property ID_AT_SWIPE As String
        Get
            Return ViewState(Me.ID & "_ID_AT_SWIPE")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ID_AT_SWIPE") = value
        End Set
    End Property

    Private Property EmployeeShift As EMPLOYEE_SHIFT_DTO
        Get
            Return PageViewState(Me.ID & "_EmployeeShift")
        End Get
        Set(ByVal value As EMPLOYEE_SHIFT_DTO)
            PageViewState(Me.ID & "_EmployeeShift") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
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
            GetParams()
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Dim periodid As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CommonConfig.GetReminderConfigFromDatabase()
                If CommonConfig.IS_AUTO() Then
                    rq1.Enabled = False
                    rq2.Enabled = False
                Else
                    rq1.Enabled = True
                    rq2.Enabled = True
                End If
                EmployeeID = LogHelper.CurrentUser.EMPLOYEE_ID
                If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
                    periodid = Request.Params("periodid")
                    Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
                    rdWorkingday.MinDate = period.START_DATE
                    rdWorkingday.MaxDate = period.END_DATE
                End If
                EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing)
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    hidOrgID.Value = EmployeeDto.Rows(0)("ORG_ID")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                End If
                Select Case Message
                    Case "UpdateView"
                        Dim objData = rep.GetLate_CombackoutById(Request.Params("ID"))
                        If objData IsNot Nothing Then
                            Me._Value = objData.ID
                            If IsDate(objData.WORKINGDAY) Then
                                rdWorkingday.SelectedDate = objData.WORKINGDAY
                            End If
                            If IsNumeric(objData.TYPE_DMVS_ID) Then
                                cboTypeDmvs.SelectedValue = objData.TYPE_DMVS_ID
                            End If

                            If IsNumeric(objData.REGIST_INFO) Then
                                cboRegistInfo.SelectedValue = objData.REGIST_INFO
                            End If
                            If IsNumeric(objData.SHIFT_ID) Then
                                hidShiftID.Value = objData.SHIFT_ID
                            End If

                            If IsDate(objData.SHIFT_START) Then
                                rdShiftStart.SelectedDate = objData.SHIFT_START
                            End If

                            If IsDate(objData.SHIFT_END) Then
                                rdShiftEnd.SelectedDate = objData.SHIFT_END
                            End If
                            txtShiftCode.Text = objData.SHIFT_CODE

                            If IsNumeric(objData.MINUTE) Then
                                txtMinute.Value = objData.MINUTE
                            End If
                            If IsNumeric(objData.ID_AT_SWIPE) Then
                                Me.ID_AT_SWIPE = objData.ID_AT_SWIPE
                            End If
                            If IsNumeric(objData.ORG_CHECK_IN) Then
                                hidOrgID1.Value = objData.ORG_CHECK_IN
                                chkOtherOrg.Checked = True
                                btnFindOrg.Visible = True
                                lblOrgOT.Visible = True
                                txtOrgName.Visible = True
                            Else
                                chkOtherOrg.Checked = False
                                btnFindOrg.Visible = False
                                lblOrgOT.Visible = False
                                txtOrgName.Visible = False
                            End If
                            txtOrgName.Text = objData.ORG_CHECK_IN_NAME
                            txtGhiChu.Text = objData.REMARK

                            If IsDate(objData.FROM_HOUR) Then
                                txtTuGio.SelectedDate = objData.FROM_HOUR
                            End If
                            If IsDate(objData.TO_HOUR) Then
                                txtDenGio.SelectedDate = objData.TO_HOUR
                            End If
                            If IsNumeric(objData.STATUS) Then
                                If objData.STATUS <> PortalStatus.Saved Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    EnableControlAll(False, rdWorkingday, cboTypeDmvs, cboRegistInfo, txtTuGio, txtDenGio, txtMinute, txtGhiChu)
                                Else
                                    CurrentState = CommonMessage.STATE_EDIT
                                End If
                            End If
                        End If
                    Case "InsertWithDataView"
                        Dim DataID = CDec(Request.Params("TIME"))
                        Dim objData = rep.GetPortalMachinesByID(DataID)
                        If objData IsNot Nothing Then
                            If IsDate(objData.WORKINGDAY) Then
                                rdWorkingday.SelectedDate = objData.WORKINGDAY
                                EmployeeShift = rep.GetEmployeeShifts(EmployeeID, objData.WORKINGDAY, objData.WORKINGDAY).FirstOrDefault
                                If EmployeeShift IsNot Nothing Then
                                    hidShiftID.Value = EmployeeShift.ID_SIGN
                                    txtShiftCode.Text = EmployeeShift.SIGN_CODE
                                    If EmployeeShift.HOURS_START IsNot Nothing Then
                                        rdShiftStart.SelectedDate = EmployeeShift.HOURS_START
                                    End If
                                    If EmployeeShift.HOURS_END IsNot Nothing Then
                                        rdShiftEnd.SelectedDate = EmployeeShift.HOURS_END
                                    End If
                                End If
                            End If
                            If CommonConfig.IS_AUTO() Then
                                If IsDate(objData.HOURS_START) Then
                                    txtTuGio.SelectedDate = objData.HOURS_START
                                End If
                                If IsDate(objData.HOURS_STOP) Then
                                    txtDenGio.SelectedDate = objData.HOURS_STOP
                                End If
                            End If
                            If IsDate(objData.HOURS_START) And IsDate(objData.HOURS_STOP) Then
                                txtMinute.Value = DateDiff(DateInterval.Minute, objData.HOURS_START.Value, objData.HOURS_STOP.Value)
                            End If
                        End If
                        CurrentState = CommonMessage.STATE_NEW
                    Case "InsertView"
                        CurrentState = CommonMessage.STATE_NEW
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien lay cac params chuyen sang tu trang view
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetParams()
        Dim ID As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request.Params("ID") IsNot Nothing Then
                Refresh("UpdateView")
            Else
                If Request.Params("TIME") IsNot Nothing Then
                    Refresh("InsertWithDataView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarDMVS
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As AT_LATE_COMBACKOUTDTO
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim lstEmp As New List(Of Common.CommonBusiness.EmployeeDTO)
        Dim sAction As String
        Dim isExist As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If chkOtherOrg.Checked And Not IsNumeric(hidOrgID1.Value) Then
                        ShowMessage(Translate("Chưa nhập Đơn vị/phòng ban làm thêm!"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim dtData As New DataTable
                    dtData = store.SE_GETTEMPLATE_APP(EmployeeID, rdWorkingday.SelectedDate, Date.Now.Date, "WLEO", If(hidOrgID.Value Is Nothing, 0, hidOrgID.Value))
                    If dtData.Rows.Count > 0 Then
                        Dim query = (From p In dtData.AsEnumerable).FirstOrDefault

                        Dim frHour = If(query("FROM_HOUR").ToString <> "", Decimal.Parse(query("FROM_HOUR").ToString), "")
                        If IsNumeric(frHour) Then
                            Dim numberOfDateCompare As Decimal = DateDiff("d", rdWorkingday.SelectedDate.Value.Date, Date.Now.Date)
                            If numberOfDateCompare > frHour Then
                                ShowMessage(Translate("Ngày đăng ký vi phạm quy chế của công ty"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                    End If
                    ' check kỳ công đã đóng
                    Dim periodid = rep.GetperiodID_2(EmployeeID, rdWorkingday.SelectedDate)
                    If periodid = 10 Then
                        ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                        Exit Sub
                    ElseIf periodid = -1 Then
                        ShowMessage(Translate("Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công"), NotifyType.Warning)
                        Exit Sub
                    ElseIf periodid = -2 Then
                        ShowMessage(Translate("Nhân viên chưa được thiết lập đối tượng nhân viên"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Dim checkKicong = rep.CHECK_PERIOD_CLOSE1(periodid, EmployeeID)
                    'If checkKicong = 0 Then
                    '    ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    RegisterDMVSList = New List(Of AT_LATE_COMBACKOUTDTO)

                    RegisterDMVSList.Add(New AT_LATE_COMBACKOUTDTO With {.EMPLOYEE_CODE = txtEmpCode.Text.Trim,
                                                                        .VN_FULLNAME = txtFullName.Text.Trim,
                                                                        .TITLE_NAME = txtTitle.Text.Trim,
                                                                        .EMPLOYEE_ID = EmployeeID})

                    obj = New AT_LATE_COMBACKOUTDTO
                    obj.MINUTE = Decimal.Parse(txtMinute.Text.Trim)
                    obj.TYPE_DMVS_ID = Decimal.Parse(cboTypeDmvs.SelectedValue)
                    obj.REGIST_INFO = CDec(Val(cboRegistInfo.SelectedValue))
                    obj.REMARK = txtGhiChu.Text.Trim
                    obj.WORKINGDAY = rdWorkingday.SelectedDate
                    obj.ORG_CHECK_IN = If(hidOrgID1.Value <> "", Decimal.Parse(hidOrgID1.Value), Nothing)
                    If IsNumeric(hidShiftID.Value) Then
                        obj.SHIFT_ID = hidShiftID.Value
                    End If
                    obj.IS_APP = 0
                    If txtTuGio.SelectedTime IsNot Nothing Then
                        obj.FROM_HOUR = rdWorkingday.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))
                    End If
                    If txtDenGio.SelectedTime IsNot Nothing Then
                        obj.TO_HOUR = rdWorkingday.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                    End If
                    Select Case CurrentState
                        Case CommonMessage.STATE_NEW
                            obj.ID_AT_SWIPE = Nothing
                            obj.ID = 0
                            obj.STATUS = PortalStatus.Saved
                            For idex = 0 To RegisterDMVSList.Count - 1
                                obj.EMPLOYEE_ID = RegisterDMVSList(idex).EMPLOYEE_ID
                                isExist = rep.ValidateLate_combackout(obj)
                                If isExist Then
                                    ShowMessage(Translate("Ngày đăng ký đã tồn tại trong hệ thống"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Next
                            If rep.InsertLate_combackout(RegisterDMVSList, obj, gstatus) Then
                                'store.UPDATE_INSERT_AT_SWIPE_DATA(gstatus, Nothing)
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDMVSMng")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                        Case CommonMessage.STATE_EDIT
                            obj.ID_AT_SWIPE = ID_AT_SWIPE
                            obj.ID = _Value
                            For idex = 0 To RegisterDMVSList.Count - 1
                                obj.EMPLOYEE_ID = RegisterDMVSList(idex).EMPLOYEE_ID
                                isExist = rep.ValidateLate_combackout(obj)
                                If isExist Then
                                    ShowMessage(Translate("Ngày đăng ký đã tồn tại trong hệ thống"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Next
                            If rep.ModifyLate_combackout(obj, gstatus) Then
                                'store.UPDATE_INSERT_AT_SWIPE_DATA(gstatus, ID_AT_SWIPE)
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDMVSMng")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                    End Select

                Case CommonMessage.TOOLBARITEM_CANCEL
                    'Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDMVSTime_Timesheet")
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDMVSMng")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID1.Value = e.CurrentValue
                FillDataInControls(e.CurrentValue)
            End If
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdWorkingday_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdWorkingday.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdWorkingday.SelectedDate IsNot Nothing Then
                Using rep As New AttendanceRepository
                    Dim dto As New AT_OT_REGISTRATIONDTO
                    dto.REGIST_DATE = rdWorkingday.SelectedDate
                    dto.EMPLOYEE_ID = EmployeeID
                    hidShiftID.ClearValue()
                    txtShiftCode.ClearValue()
                    rdShiftStart.Clear()
                    rdShiftEnd.Clear()
                    EmployeeShift = rep.GetEmployeeShifts(EmployeeID, rdWorkingday.SelectedDate, rdWorkingday.SelectedDate).FirstOrDefault
                    If EmployeeShift IsNot Nothing Then
                        hidShiftID.Value = EmployeeShift.ID_SIGN
                        txtShiftCode.Text = EmployeeShift.SIGN_CODE
                        If EmployeeShift.HOURS_START IsNot Nothing Then
                            rdShiftStart.SelectedDate = EmployeeShift.HOURS_START
                        End If
                        If EmployeeShift.HOURS_END IsNot Nothing Then
                            rdShiftEnd.SelectedDate = EmployeeShift.HOURS_END
                        End If
                    End If
                End Using
            End If
            'CommonConfig.GetReminderConfigFromDatabase()
            If Not CommonConfig.IS_AUTO() Then
                txtTuGio.ClearValue()
                txtDenGio.ClearValue()
            Else
                txtTuGio.SelectedDate = rdShiftStart.SelectedDate
                txtDenGio.SelectedDate = rdShiftEnd.SelectedDate
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub chkOtherOrg_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOtherOrg.CheckedChanged
        Try
            ClearControlValue(hidOrgID, txtOrgName)
            btnFindOrg.Visible = chkOtherOrg.Checked
            lblOrgOT.Visible = chkOtherOrg.Checked
            txtOrgName.Visible = chkOtherOrg.Checked
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly cac trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
            phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
        End If
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
            End Select
            ChangeToolbarState()
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_DMVS = True
                ListComboData.GET_LIST_TYPE_INFO = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboTypeDmvs, ListComboData.LIST_LIST_TYPE_DMVS, "NAME_VN", "ID", True)
            FillRadCombobox(cboRegistInfo, ListComboData.LIST_LIST_TYPE_INFO, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_TYPE_DMVS.Count > 0 Then
                cboTypeDmvs.SelectedIndex = 0
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub FillDataInControls(ByVal orgid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim orgTree As OrganizationTreeDTO
        Using rep As New ProfileRepository
            Dim org = rep.GetOrganizationByID(orgid)
            If org IsNot Nothing Then
                txtOrgName.Text = org.NAME_VN
                txtOrgName.ToolTip = org.NAME_VN
            End If
            orgTree = rep.GetTreeOrgByID(orgid)
        End Using
        Try

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
        End Try
    End Sub
#End Region

End Class

