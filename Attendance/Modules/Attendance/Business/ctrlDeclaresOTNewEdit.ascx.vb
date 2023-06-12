Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Telerik.Web.UI

Public Class ctrlDeclaresOTNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Dim checkSave As Boolean = False

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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
    Private Property EmployeeShift As EMPLOYEE_SHIFT_DTO
        Get
            Return PageViewState(Me.ID & "_EmployeeShift")
        End Get
        Set(ByVal value As EMPLOYEE_SHIFT_DTO)
            PageViewState(Me.ID & "_EmployeeShift") = value
        End Set
    End Property
    Private Property lstdtHoliday As DataTable
        Get
            Return PageViewState(Me.ID & "_lstdtHoliday")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_lstdtHoliday") = value
        End Set
    End Property
    Protected Property ListManual As List(Of AT_FMLDTO)
        Get
            Return PageViewState(Me.ID & "_ListFML")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
            PageViewState(Me.ID & "_ListFML") = value
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
    Property OtRegistration As AT_OT_REGISTRATIONDTO
        Get
            Return ViewState(Me.ID & "_OTRegistration")
        End Get
        Set(ByVal value As AT_OT_REGISTRATIONDTO)
            ViewState(Me.ID & "_OTRegistration") = value
        End Set
    End Property

    Property userType As String
        Get
            Return ViewState(Me.ID & "_userType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userType") = value
        End Set
    End Property

    Property lstHsOT As List(Of OT_OTHERLIST_DTO)
        Get
            Return ViewState(Me.ID & "_lstHsOT")
        End Get
        Set(value As List(Of OT_OTHERLIST_DTO))
            ViewState(Me.ID & "_lstHsOT") = value
        End Set
    End Property
    Property otFrTime As DateTime
        Get
            Return ViewState(Me.ID & "_otFrTime")
        End Get
        Set(value As DateTime)
            ViewState(Me.ID & "_otFrTime") = value
        End Set
    End Property
    Property otToTime As DateTime
        Get
            Return ViewState(Me.ID & "_otToTime")
        End Get
        Set(value As DateTime)
            ViewState(Me.ID & "_otToTime") = value
        End Set
    End Property
    Property hidTotalDayTT As Decimal?
        Get
            Return ViewState(Me.ID & "_hidTotalDayTT")
        End Get
        Set(value As Decimal?)
            ViewState(Me.ID & "_hidTotalDayTT") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            If CommonConfig.AT_HS_OT() Then
                Hide_ot.Visible = False
                cbohs_ot.Visible = False
                RequiredFieldValidator4.Enabled = False
            Else
                Hide_ot.Visible = True
                cbohs_ot.Visible = True
                RequiredFieldValidator4.Enabled = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay params 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If Not IsPostBack Then
                If ListComboData Is Nothing Then
                    ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                    ListComboData.GET_LIST_OT_TYPE = True
                    ListComboData.GET_LIST_HS_OT = True
                    rep.GetComboboxData(ListComboData)
                End If

                FillRadCombobox(cboTypeOT, ListComboData.LIST_LIST_OT_TYPE, "NAME_VN", "ID", True)
                'If ListComboData.LIST_LIST_OT_TYPE.Count > 0 Then
                '    cboTypeOT.SelectedValue = 6922
                'End If
                Me.lstHsOT = ListComboData.LIST_LIST_HS_OT
                FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "NAME_VN", "ID", True)

                Dim table As DataTable = LoadComboMinute()
                FillRadCombobox(cboFromAM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboToAM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboFromPM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboToPM, table, "NAME_VN", "ID", True)

                If hidEmpId.Value <> "" Then
                    EmployeeDto = rep.GetEmployeeInfor(hidEmpId.Value, Nothing)
                    If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                        txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                        txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                        txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                        txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                    End If
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim rep As New AttendanceRepository
            Dim store As New AttendanceStoreProcedure
            If Not IsPostBack Then
                Dim id As Decimal = 0
                Dim empId As Decimal = 0
                Decimal.TryParse(Request.QueryString("id"), id)
                Decimal.TryParse(Request.QueryString("employeeID"), empId)
                'userType = Request.QueryString("typeUser")
                hidID.Value = id
                'hidValid.Value = 0
                hidEmpId.Value = empId
                'Dim dto As New AT_OT_REGISTRATIONDTO
                'dto.ID = hidID.Value
                'If hidID.Value = 0 Then
                '    dto.EMPLOYEE_ID = hidEmpId.Value
                'Else
                '    dto.EMPLOYEE_ID = empId
                'End If
                'dto.P_MANAGER = "APP"

                'If dto.ID > 0 Then
                '    Dim data = rep.GetOtRegistration(dto)
                '    If data IsNot Nothing Then
                '        OtRegistration = data.FirstOrDefault
                '        EmployeeShift = rep.GetEmployeeShifts(empId, OtRegistration.REGIST_DATE, OtRegistration.REGIST_DATE).FirstOrDefault
                '        If EmployeeShift IsNot Nothing Then
                '            hidSignId.Value = EmployeeShift.ID_SIGN
                '            txtSignCode.Text = EmployeeShift.SIGN_CODE
                '        End If
                '    End If
                'End If


                Dim dtData = store.GET_REG_DATA_BY_ID(hidID.Value)
                Dim dtData_ORG_OT As New DataTable

                Dim vCheck As Boolean
                If dtData.Rows.Count > 0 AndAlso If(IsDBNull(dtData.Rows(0)("OTHER_ORG")), 0, dtData.Rows(0)("OTHER_ORG")) = 0 Then
                    vCheck = True
                End If

                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    If dtData.Rows.Count > 0 Then
                        If IsNumeric(dtData.Rows(0)("ORG_OT_ID")) Then
                            dtData_ORG_OT = store.GET_ORG_OT_BY_ID(dtData.Rows(0)("ORG_OT_ID"))
                        End If
                        EmployeeShift = rep.GetEmployeeShifts(empId, dtData.Rows(0)("REGIST_DATE"), dtData.Rows(0)("REGIST_DATE")).FirstOrDefault
                        If EmployeeShift IsNot Nothing Then
                            hidSignId.Value = EmployeeShift.ID_SIGN
                            txtSignCode.Text = EmployeeShift.SIGN_CODE

                            If EmployeeShift.HOURS_START IsNot Nothing Then
                                rdHours_Start.SelectedDate = EmployeeShift.HOURS_START
                            End If
                            If EmployeeShift.HOURS_END IsNot Nothing Then
                                rdHours_Stop.SelectedDate = EmployeeShift.HOURS_END
                            End If
                        End If

                        hidStatus.Value = If(IsNumeric(dtData.Rows(0)("STATUS")), dtData.Rows(0)("STATUS"), 0)

                        If IsDate(dtData.Rows(0)("REGIST_DATE")) Then
                            rdRegDate.SelectedDate = dtData.Rows(0)("REGIST_DATE")
                        End If
                        txtNote.Text = dtData.Rows(0)("NOTE").ToString

                        If IsNumeric(dtData.Rows(0)("OT_TYPE_ID")) Then
                            cboTypeOT.SelectedValue = dtData.Rows(0)("OT_TYPE_ID")
                            'change_cboTypeOT()
                        End If
                        If IsNumeric(dtData.Rows(0)("FROM_AM")) Then
                            rntbFromAM.Value = CDec(dtData.Rows(0)("FROM_AM"))
                        End If
                        If IsNumeric(dtData.Rows(0)("FROM_AM_MN")) Then
                            cboFromAM.SelectedValue = CDec(dtData.Rows(0)("FROM_AM_MN"))
                        End If
                        If IsNumeric(dtData.Rows(0)("TO_AM")) Then
                            rntbToAM.Value = CDec(dtData.Rows(0)("TO_AM"))
                        End If
                        If IsNumeric(dtData.Rows(0)("TO_AM_MN")) Then
                            cboToAM.SelectedValue = CDec(dtData.Rows(0)("TO_AM_MN"))
                        End If
                        If IsNumeric(dtData.Rows(0)("FROM_PM")) Then
                            rntbFromPM.Value = CDec(dtData.Rows(0)("FROM_PM"))
                        End If
                        If IsNumeric(dtData.Rows(0)("FROM_PM_MN")) Then
                            cboFromPM.SelectedValue = CDec(dtData.Rows(0)("FROM_PM_MN"))
                        End If
                        If IsNumeric(dtData.Rows(0)("TO_PM")) Then
                            rntbToPM.Value = CDec(dtData.Rows(0)("TO_PM"))
                        End If
                        If IsNumeric(dtData.Rows(0)("TO_PM_MN")) Then
                            cboToPM.SelectedValue = CDec(dtData.Rows(0)("TO_PM_MN"))
                        End If
                        If IsNumeric(dtData.Rows(0)("PM_FROMHOURS_AFTERCHECK")) Then
                            chkIsFrHourAfter.Checked = CDec(dtData.Rows(0)("PM_FROMHOURS_AFTERCHECK"))
                        End If
                        If IsNumeric(dtData.Rows(0)("PM_TOHOURS_AFTERCHECK")) Then
                            chkIsToHourAfter.Checked = CDec(dtData.Rows(0)("PM_TOHOURS_AFTERCHECK"))
                        End If

                        If IsNumeric(dtData.Rows(0)("IS_PASS_DAY")) Then
                            chkPassDay.Checked = CDec(dtData.Rows(0)("IS_PASS_DAY"))
                        End If
                        If IsNumeric(dtData.Rows(0)("TOTAL_OT_TT")) Then
                            txtSumOT.Text = Replace(CDec(dtData.Rows(0)("TOTAL_OT_TT")).ToString, ",", ".")
                        End If
                        Dim ot_hs = (From p In Me.lstHsOT Where p.CODE.ToUpper.Equals(dtData.Rows(0)("OT_CODE").ToString.ToUpper)).FirstOrDefault
                        If ot_hs IsNot Nothing Then
                            cbohs_ot.SelectedValue = ot_hs.ID
                            cbohs_ot.Text = ot_hs.NAME_VN
                        End If

                        Using rep2 As New HistaffFrameworkRepository
                            Dim response = rep2.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_ACCUMULATIVE_OT", New List(Of Object)(New Object() {empId, rdRegDate.SelectedDate.Value, OUT_NUMBER}))
                            If response IsNot Nothing AndAlso response(0).ToString() <> "" Then
                                rntTotalAccumulativeOTHours.Text = Decimal.Parse(response(0).ToString()).ToString("N1")
                            End If
                        End Using
                        dtData = store.GET_TIME_OT_COEFF_OVER(rdRegDate.SelectedDate)
                        If dtData IsNot Nothing Then
                            otFrTime = dtData.Rows(0)("FROMDATE_NIGHTHOUR_F")
                            otToTime = dtData.Rows(0)("TODATE_NIGHTHOUR_F")
                        Else
                            ShowMessage("Chưa thiết lập hệ số OT", NotifyType.Warning)
                            Exit Sub
                        End If
                        If dtData_ORG_OT IsNot Nothing Then
                            If dtData_ORG_OT.Rows.Count > 0 Then
                                hidOrgID.Value = If(IsNumeric(dtData_ORG_OT.Rows(0)("ID")), dtData_ORG_OT.Rows(0)("ID").ToString, "")
                                txtOrgID.Text = dtData_ORG_OT.Rows(0)("NAME_VN").ToString
                                If dtData_ORG_OT.Rows(0)("DESCRIPTION_PATH") IsNot Nothing AndAlso dtData_ORG_OT.Rows(0)("DESCRIPTION_PATH") <> "" Then
                                    txtOrgID.ToolTip = DrawTreeByString(dtData_ORG_OT.Rows(0)("DESCRIPTION_PATH").ToString)
                                End If
                            End If
                        End If
                    End If
                End If
                'CalculateOT()
                If vCheck Then
                    hidOrgID.Value = ""
                    txtOrgID.Text = ""
                End If
                EmployeeDto = rep.GetEmployeeInfor(empId, Nothing)
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                End If
                'change_cboTypeOT()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            ShowPopupEmployee()
            If IsPostBack Then
                Exit Sub
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim store As New AttendanceStoreProcedure
            Dim periodid As Decimal
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdRegDate, rntbFromAM, cboFromAM, rntbToAM, cboToAM, rntbToPM, rntbFromPM, cboFromPM, rntbToPM, cboToPM, txtNote)

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If hidSignId.Value.ToString = "" Then 'chua dang ky ca
                            lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(rdRegDate.SelectedDate, rdRegDate.SelectedDate)
                            If lstdtHoliday.Rows.Count <= 0 Then 'Ko phai ngay le
                                ShowMessage("Nhân viên chưa được gán ca. Vui lòng kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        Dim shift_Start As Date = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, EmployeeShift.HOURS_START.Value.Hour, EmployeeShift.HOURS_START.Value.Minute, 0)
                        Dim shift_Stop As Date = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, EmployeeShift.HOURS_END.Value.Hour, EmployeeShift.HOURS_END.Value.Minute, 0)
                        If EmployeeShift.IS_TOMORROW Then
                            shift_Stop = shift_Stop.AddDays(1)
                        End If
                        If rntbFromAM.Value.HasValue Or rntbToAM.Value.HasValue Then
                            If Not rntbFromAM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập từ giờ làm thêm AM."), NotifyType.Warning)
                                rntbFromAM.Focus()
                                Exit Sub
                            Else
                                If cboFromAM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút AM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If Not rntbToAM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập đến giờ làm thêm AM."), NotifyType.Warning)
                                rntbToAM.Focus()
                                Exit Sub
                            Else
                                If cboToAM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút AM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                            If IsNumeric(hidSignId.Value) Then
                                Dim FromHour As Date = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, rntbFromAM.Value, cboFromAM.SelectedValue, 0)
                                Dim ToHour As Date = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, rntbToAM.Value, cboToAM.SelectedValue, 0)
                                Dim TimeCheck As Date = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, 22, 0, 0)
                                If hidSignId.Value <> 1 And hidSignId.Value <> 2 Then

                                    'If store.CHECK_OT_SHIFT(hidSignId.Value, FromHour, ToHour) Then
                                    '    ShowMessage(Translate("Giờ đăng ký OT giao với giờ làm việc hành chính"), NotifyType.Warning)
                                    '    Exit Sub
                                    'End If
                                    If (FromHour >= shift_Start AndAlso FromHour <= shift_Stop) OrElse
                                        (ToHour > shift_Start AndAlso ToHour <= shift_Stop) OrElse
                                        (shift_Start >= FromHour AndAlso shift_Start < ToHour) OrElse
                                        (shift_Stop >= FromHour AndAlso shift_Stop <= ToHour) Then
                                        ShowMessage(Translate("Giờ đăng ký OT giao với giờ làm việc hành chính"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If ToHour > TimeCheck Then
                                    ShowMessage(Translate("Khung giờ OT trước ca chỉ đăng ký được khung giờ tối đa là 22h00"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If rntbFromPM.Value.HasValue Or rntbToPM.Value.HasValue Then
                            If Not rntbFromPM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập từ giờ làm thêm PM."), NotifyType.Warning)
                                rntbFromPM.Focus()
                                Exit Sub
                            Else
                                If cboFromPM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút PM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If Not rntbToPM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập đến giờ làm thêm PM."), NotifyType.Warning)
                                rntbToPM.Focus()
                                Exit Sub
                            Else
                                If cboToPM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút PM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If rntbToPM.Value IsNot Nothing AndAlso rntbToPM.Value = 24 AndAlso cboToPM.SelectedValue IsNot Nothing AndAlso cboToPM.SelectedValue <> 0 Then
                            ShowMessage(Translate("Đến giờ PM = 24, không được nhập phút."), NotifyType.Warning)
                            Exit Sub
                        End If
                        'If rntbFromAM.Value IsNot Nothing And rntbToAM.Value IsNot Nothing Then
                        '    If CDec(Val(rntbFromAM.Value)) >= CDec(Val(rntbToAM.Value)) And CDec(Val(cboFromAM.SelectedValue)) >= CDec(Val(cboToAM.SelectedValue)) Then
                        '        ShowMessage(Translate("Nhập giờ AM: Từ lớn hơn giờ Đến"), NotifyType.Warning)
                        '        rntbFromAM.Focus()
                        '        Exit Sub
                        '    End If
                        'End If


                        '---check khung gio dki hop le --case 24h
                        If CDec(Val(rntbFromAM.Value)) = 24 Or CDec(Val(rntbFromPM.Value)) = 24 Or (CDec(Val(rntbToAM.Value)) = 24 And CDec(Val(cboToAM.SelectedValue)) > 0) _
                            Or (CDec(Val(rntbToPM.Value)) = 24 And CDec(Val(cboToPM.SelectedValue)) > 0) Then

                            ShowMessage(Translate("Khung giờ đăng ký không hợp lệ (vượt khung 24h)"), NotifyType.Warning)
                            Exit Sub

                        End If

                        '--check tu gio > den gio
                        If rntbToAM.Value IsNot Nothing Then
                            If CDec(Val(rntbFromAM.Value)) > CDec(Val(rntbToAM.Value)) Or (CDec(Val(rntbFromAM.Value)) = CDec(Val(rntbToAM.Value)) And CDec(Val(cboFromAM.SelectedValue)) > CDec(Val(cboToAM.SelectedValue))) Then
                                ShowMessage(Translate("Khung giờ đăng ký không hợp lệ (từ giờ lớn hơn đến giờ)"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If rntbToPM.Value IsNot Nothing Then
                            If CDec(Val(rntbFromPM.Value)) > CDec(Val(rntbToPM.Value)) Or (CDec(Val(rntbFromPM.Value)) = CDec(Val(rntbToPM.Value)) And CDec(Val(cboFromPM.SelectedValue)) > CDec(Val(cboToPM.SelectedValue))) Then
                                ShowMessage(Translate("Khung giờ đăng ký không hợp lệ (từ giờ lớn hơn đến giờ)"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        '--check khung gio dki OT (L,OFF,NBL,..)
                        If rdHours_Start.SelectedDate = rdHours_Stop.SelectedDate Then
                            Dim s_hour = rdHours_Start.SelectedDate.Value.Hour
                            Dim s_mi = rdHours_Start.SelectedDate.Value.Minute

                            If rntbToAM.Value IsNot Nothing Then
                                If CDec(Val(rntbToAM.Value)) > s_hour Or (CDec(Val(rntbToAM.Value)) = s_hour And CDec(Val(cboToAM.SelectedValue)) > s_mi) Then
                                    ShowMessage(Translate("Đăng ký OT trước ca từ 00:00 - " + s_hour.ToString("00") + ":" + s_mi.ToString("00") + ", "), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                            If rntbFromPM.Value IsNot Nothing Then
                                If CDec(Val(rntbFromPM.Value)) < s_hour Or (CDec(Val(rntbFromPM.Value)) = s_hour And CDec(Val(cboToAM.SelectedValue)) < s_mi) Then
                                    ShowMessage(Translate("Đăng ký OT sau ca từ " + s_hour.ToString("00") + ":" + s_mi.ToString("00") + " - 24:00, "), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                        End If





                        Dim hour_frAM As String
                        Dim minute_frAM As String
                        Dim hour_toAM As String
                        Dim minute_toAM As String
                        Dim hour_frPM As String
                        Dim minute_frPM As String
                        Dim hour_toPM As String
                        Dim minute_toPM As String
                        'Buổi sáng
                        '' Từ giờ
                        If rntbFromAM.Value Then
                            hour_frAM = Format(rntbFromAM.Value, "00")
                        End If

                        If Not String.IsNullOrEmpty(cboFromAM.SelectedValue) Then
                            minute_frAM = Format(cboFromAM.SelectedValue, "00")
                        End If

                        '' Đến giờ
                        If rntbToAM.Value Then
                            hour_toAM = Format(rntbToAM.Value, "00")
                        End If

                        If Not String.IsNullOrEmpty(cboToAM.SelectedValue) Then
                            minute_toAM = Format(cboToAM.SelectedValue, "00")
                        End If

                        'Buổi chiều
                        '' Từ giờ
                        If rntbFromPM.Value Then
                            hour_frPM = DateTime.Parse(Format(rntbFromPM.Value, "00 PM")).ToString("HH")
                        End If

                        If Not String.IsNullOrEmpty(cboFromPM.SelectedValue) Then
                            minute_frPM = Format(cboFromPM.SelectedValue, "00")
                        End If
                        '' Đến giờ
                        If rntbToPM.Value Then
                            'hour_toPM = DateTime.Parse(Format(rntbToPM.Value, "00 PM")).ToString("HH")
                            hour_toPM = Format(rntbToPM.Value, "00")
                        End If

                        If Not String.IsNullOrEmpty(cboToPM.SelectedValue) Then
                            minute_toPM = Format(cboToPM.SelectedValue, "00")
                        End If

                        'Nếu có check qua ngày hôm sau: chỉ cho nhập từ 0 - 12
                        '' Từ giờ PM
                        If chkIsFrHourAfter.Checked Then
                            If rntbFromPM.Value.HasValue AndAlso (rntbFromPM.Value < 0 Or rntbFromPM.Value > 12) Then
                                ShowMessage(Translate("chỉ cho nhập Từ giờ PM từ 0 - 12 giờ."), NotifyType.Warning)
                                rntbFromPM.Focus()
                                Exit Sub
                            End If
                        Else
                            'Nếu không check qua ngày hôm sau: chỉ cho nhập từ 12 - 24
                            If rntbFromPM.Value.HasValue AndAlso rntbFromPM.Value < 12 Then
                                ShowMessage(Translate("chỉ cho nhập Từ giờ PM  từ 12 - 24 giờ."), NotifyType.Warning)
                                rntbFromPM.Focus()
                                Exit Sub
                            End If
                        End If


                        'Nếu có check qua ngày hôm sau: chỉ cho nhập từ 0 - 12
                        '' Đến giờ PM
                        If chkIsToHourAfter.Checked Then
                            If rntbToPM.Value.HasValue AndAlso (rntbToPM.Value < 0 Or rntbToPM.Value > 12) Then
                                ShowMessage(Translate("chỉ cho nhập Đến giờ PM từ 0 - 12 giờ."), NotifyType.Warning)
                                rntbToPM.Focus()
                                Exit Sub
                            End If
                        Else
                            'Nếu không check qua ngày hôm sau: chỉ cho nhập từ 12 - 24
                            If rntbToPM.Value.HasValue AndAlso rntbToPM.Value < 12 Then
                                ShowMessage(Translate("chỉ cho nhập Đến giờ PM từ 12 - 24 giờ."), NotifyType.Warning)
                                rntbToPM.Focus()
                                Exit Sub
                            End If
                        End If

                        If chkIsFrHourAfter.Checked And chkIsToHourAfter.Checked Then
                            If rntbFromPM.Value.HasValue AndAlso rntbToPM.Value.HasValue AndAlso hour_frPM > hour_toPM Then
                                ShowMessage(Translate("Từ giờ PM phải nhỏ hơn Đến giờ PM."), NotifyType.Warning)
                                rntbFromPM.Focus()
                                Exit Sub
                            End If
                        Else
                            If chkIsToHourAfter.Checked Then
                                If hour_frPM < hour_toPM Then
                                    ShowMessage(Translate("Từ giờ PM phải lớn hơn Đến giờ PM."), NotifyType.Warning)
                                    rntbFromPM.Focus()
                                    Exit Sub
                                End If
                            Else
                                If hour_frPM > hour_toPM Then
                                    ShowMessage(Translate("Từ giờ PM phải nhỏ hơn Đến giờ PM."), NotifyType.Warning)
                                    rntbFromPM.Focus()
                                    Exit Sub
                                End If
                            End If
                        End If

                        'If rntbFromPM.Value >= rntbToPM.Value And cboFromPM.SelectedValue >= cboToPM.SelectedValue Then
                        '    ShowMessage(Translate("Nhập giờ PM: Từ lớn hơn giờ Đến"), NotifyType.Warning)
                        '    rntbFromPM.Focus()
                        '    Exit Sub
                        'End If

                        'If String.IsNullOrEmpty(hidTotal.Value) Or (Not String.IsNullOrEmpty(hidTotal.Value) AndAlso Decimal.Parse(hidTotal.Value) <= 0) Then
                        '    ShowMessage(Translate("Tổng đăng ký tăng ca không hợp lệ."), NotifyType.Warning)
                        '    UpdateControlState()
                        '    Exit Sub
                        'End If
                        If rntbFromPM.Value.HasValue Or rntbToPM.Value.HasValue Then
                            If IsNumeric(hidSignId.Value) Then
                                If hidSignId.Value <> 1 And hidSignId.Value <> 2 Then
                                    Dim FromHour As Date
                                    Dim ToHour As Date
                                    If chkIsFrHourAfter.Checked Then
                                        FromHour = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, rntbFromPM.Value, cboFromPM.SelectedValue, 0)
                                    Else
                                        FromHour = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, DateTime.Parse(Format(rntbFromPM.Value, "00 PM")).Hour, cboFromPM.SelectedValue, 0)
                                    End If
                                    'If chkIsToHourAfter.Checked Then
                                    '    ToHour = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, rntbToPM.Value, cboToPM.SelectedValue, 0)
                                    'Else
                                    '    ToHour = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, DateTime.Parse(Format(rntbToPM.Value, "00 PM")).Hour, cboToPM.SelectedValue, 0)
                                    'End If

                                    'Nếu nhập giờ sau ca đến = 24 thì chuyển về 00 giờ ngày hôm sau để tính đúng số giờ làm thêm
                                    If rntbToPM.Value = 24 Then
                                        'Lấy ngày đăng ký + 1
                                        Dim day = rdRegDate.SelectedDate.Value.AddDays(1)
                                        ToHour = New DateTime(day.Year, day.Month, day.Day, 0, 0, 0)
                                    Else
                                        ToHour = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, DateTime.Parse(Format(rntbToPM.Value, "00 PM")).Hour, cboToPM.SelectedValue, 0)
                                    End If
                                    'If store.CHECK_OT_SHIFT(hidSignId.Value, FromHour, ToHour) Then
                                    '    ShowMessage(Translate("Giờ đăng ký OT giao với giờ làm việc hành chính"), NotifyType.Warning)
                                    '    Exit Sub
                                    'End If
                                    If (FromHour >= shift_Start AndAlso FromHour < shift_Stop) OrElse
                                        (ToHour >= shift_Start AndAlso ToHour <= shift_Stop) OrElse
                                        (shift_Start >= FromHour AndAlso shift_Stop <= ToHour) OrElse
                                        (shift_Stop > FromHour AndAlso shift_Stop <= ToHour) Then
                                        ShowMessage(Translate("Giờ đăng ký OT giao với giờ làm việc hành chính"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                        Using rep As New AttendanceRepository
                            periodid = rep.GetperiodID_2(hidEmpId.Value, rdRegDate.SelectedDate)
                            If periodid = 0 Then
                                'ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                                'Exit Sub
                            ElseIf periodid = -1 Then
                                ShowMessage(Translate("Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công"), NotifyType.Warning)
                                Exit Sub
                            ElseIf periodid = -2 Then
                                ShowMessage(Translate("Nhân viên chưa được thiết lập đối tượng nhân viên"), NotifyType.Warning)
                                Exit Sub
                            End If
                            Dim checkKicong = rep.CHECK_PERIOD_CLOSE1(periodid, hidEmpId.Value)
                            If checkKicong = 0 Then
                                ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End Using
                        Dim isInsert As Boolean = True

                        Dim obj As New AT_OT_REGISTRATIONDTO
                        obj.EMPLOYEE_ID = hidEmpId.Value
                        obj.IS_DELETED = 0
                        obj.NOTE = txtNote.Text
                        'obj.OT_TYPE_ID = ListComboData.LIST_LIST_OT_TYPE.Where(Function(f) f.CODE = "OT").Select(Function(g) g.ID).FirstOrDefault
                        obj.OT_TYPE_ID = cboTypeOT.SelectedValue
                        obj.REGIST_DATE = rdRegDate.SelectedDate

                        obj.SIGN_ID = If(hidSignId.Value.ToString <> "", hidSignId.Value, Nothing)
                        obj.SIGN_CODE = txtSignCode.Text.Trim

                        CalculateOT()

                        obj.TOTAL_OT = Math.Round(CDec(Val(txtSumOT.Text)), 2, MidpointRounding.AwayFromZero)  ' ObjToDecima(hidTotal.Value, 0)

                        Dim checkMaxOt As Decimal = store.GET_VALUE_OT_MONTH(obj.EMPLOYEE_ID, periodid, obj.REGIST_DATE, obj.TOTAL_OT, CDec(Val(obj.OT_TYPE_ID)))

                        If checkMaxOt > 0 Then
                            ShowMessage(String.Format("Tổng số giờ OT trong tháng đã vượt định mức {0} giờ", checkMaxOt), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim checkMaxOtYear As Decimal = store.GET_VALUE_OT_YEAR(obj.EMPLOYEE_ID, obj.REGIST_DATE, obj.TOTAL_OT, CDec(Val(obj.OT_TYPE_ID)))

                        If checkMaxOtYear > 0 Then
                            ShowMessage(String.Format("Tổng số giờ OT trong tháng đã vượt định mức {0} giờ", checkMaxOtYear), NotifyType.Warning)
                            Exit Sub
                        End If

                        If obj.TOTAL_OT * 60 < EmployeeShift.OT_HOUR_MIN Then
                            ShowMessage(String.Format("Tổng số giờ OT phải lớn hơn hoặc bằng {0} phút", EmployeeShift.OT_HOUR_MIN), NotifyType.Warning)
                            Exit Sub
                        End If

                        If obj.TOTAL_OT * 60 > EmployeeShift.OT_HOUR_MAX Then
                            ShowMessage(String.Format("Tổng số giờ OT vượt quá số giờ OT cho phép. {0} phút", EmployeeShift.OT_HOUR_MAX), NotifyType.Warning)
                            Exit Sub
                        End If

                        If rntbFromAM.Value = 0 AndAlso Not String.IsNullOrEmpty(cboFromAM.SelectedValue) Then
                            obj.FROM_AM = 0
                        Else
                            obj.FROM_AM = rntbFromAM.Value
                        End If
                        If Not String.IsNullOrEmpty(cboFromAM.SelectedValue) Then
                            obj.FROM_AM_MN = cboFromAM.SelectedValue
                        End If

                        If rntbToAM.Value = 0 AndAlso Not String.IsNullOrEmpty(cboToAM.SelectedValue) Then
                            obj.TO_AM = 0
                        Else
                            obj.TO_AM = rntbToAM.Value
                        End If
                        If Not String.IsNullOrEmpty(cboToAM.SelectedValue) Then
                            obj.TO_AM_MN = cboToAM.SelectedValue
                        End If

                        If rntbFromPM.Value = 0 AndAlso Not String.IsNullOrEmpty(cboFromPM.SelectedValue) Then
                            obj.FROM_PM = 0
                        Else
                            obj.FROM_PM = rntbFromPM.Value
                        End If
                        If Not String.IsNullOrEmpty(cboFromPM.SelectedValue) Then
                            obj.FROM_PM_MN = cboFromPM.SelectedValue
                        End If

                        If rntbToPM.Value = 0 AndAlso Not String.IsNullOrEmpty(cboToPM.SelectedValue) Then
                            obj.TO_PM = 0
                        Else
                            obj.TO_PM = rntbToPM.Value
                        End If
                        If Not String.IsNullOrEmpty(cboToPM.SelectedValue) Then
                            obj.TO_PM_MN = cboToPM.SelectedValue
                        End If

                        obj.STATUS = 1
                        hidStatus.Value = 1

                        Dim CODE As String = ""
                        If cbohs_ot.SelectedValue <> "" Then
                            CODE = (From p In Me.lstHsOT Where p.ID = cbohs_ot.SelectedValue).FirstOrDefault.CODE
                        End If

                        If chkIsFrHourAfter.Checked Then
                            obj.PM_FROMHOURS_AFTERCHECK = chkIsFrHourAfter.Checked
                        End If
                        If chkIsToHourAfter.Checked Then
                            obj.PM_TOHOURS_AFTERCHECK = chkIsToHourAfter.Checked
                        End If
                        obj.IS_PASS_DAY = CDec(Val(chkPassDay.Checked))
                        obj.HOURS_TOTAL_AM = If(hidTimeCOEff_S.Value = "", Nothing, hidTimeCOEff_S.Value)
                        obj.HOURS_TOTAL_PM = If(hidTimeCOEff_E.Value = "", Nothing, hidTimeCOEff_E.Value)
                        obj.HOURS_TOTAL_DAY = If(hidHourTotalDay.Value = "", Nothing, hidHourTotalDay.Value)

                        'obj.HOURS_TOTAL_NIGHT = If(hidHourTotalNight.Value = "", Nothing, hidHourTotalNight.Value)
                        'obj.TOTAL_DAY_TT = If(IsNumeric(hidTotalDayTT.Value), hidTotalDayTT.Value, Nothing)

                        obj.HOURS_TOTAL_NIGHT = store.COUNT_OT_NIGHT(obj.REGIST_DATE, obj.FROM_AM, obj.FROM_AM_MN, obj.TO_AM, obj.TO_AM_MN, obj.FROM_PM, obj.FROM_PM_MN, obj.TO_PM, obj.TO_PM_MN)

                        obj.TOTAL_DAY_TT = CDec(Val(txtSumOT.Text)) - obj.HOURS_TOTAL_NIGHT
                        obj.TOTAL_DAY_TT = Math.Round(CDbl(obj.TOTAL_DAY_TT), 2)

                        obj.DK_PORTAL = 0
                        obj.CREATED_BY_EMP = LogHelper.CurrentUser.EMPLOYEE_ID
                        obj.BY_ANOTHER = False

                        Using rep As New AttendanceRepository

                            obj.ID = If(IsNumeric(hidID.Value), hidID.Value, 0)
                            Dim valid = rep.ValidateOtRegistration(obj)
                            If Not valid Then
                                ShowMessage(Translate("Thời gian đăng ký bị trùng, vui lòng kiểm tra lại."), NotifyType.Warning)
                                UpdateControlState()
                                Exit Sub
                            End If

                            If store.UPDATE_OT_REG(obj, CODE, LogHelper.CurrentUser.USERNAME.ToUpper, If(hidOrgID.Value.ToString <> "", hidOrgID.Value, Nothing)) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
                                'CurrentState = CommonMessage.STATE_NORMAL
                                UpdateControlState()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If


                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:34
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmpId.Value = item.EMPLOYEE_ID
                txtFullName.Text = item.FULLNAME_VN
                txtDepartment.Text = item.ORG_NAME
                txtEmpCode.Text = item.EMPLOYEE_CODE
                txtTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ShowPopupEmployee()
        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                'ctrlFindEmployeePopup.MustHaveTerminate = True
                ctrlFindEmployeePopup.MustHaveContract = True
                ctrlFindEmployeePopup.MultiSelect = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
            ElseIf isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rdRegDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdRegDate.SelectedDateChanged
        Try
            If rdRegDate.SelectedDate IsNot Nothing Then
                Using rep As New AttendanceRepository
                    Dim dto As New AT_OT_REGISTRATIONDTO
                    dto.REGIST_DATE = rdRegDate.SelectedDate
                    dto.EMPLOYEE_ID = hidEmpId.Value
                    txtSignCode.ClearValue()
                    rdHours_Start.Clear()
                    rdHours_Stop.Clear()
                    'CalculateOT()
                    If (hidEmpId.Value <> 0) Then
                        Dim validRegistDate = rep.CheckRegDateBetweenJoinAndTerDate(hidEmpId.Value, rdRegDate.SelectedDate)
                        If Not validRegistDate Then
                            ShowMessage(Translate("Ngày làm thêm phải sau ngày vào công ty và trước ngày nghỉ việc."), NotifyType.Warning)
                            rdRegDate.ClearValue()

                            hidSignId.Value = Nothing
                            rdRegDate.Focus()
                            Exit Sub
                        End If
                        'Dim data = rep.GetOtRegistration(dto)
                        'If data IsNot Nothing AndAlso data.Where(Function(f) f.ID <> hidID.Value).FirstOrDefault IsNot Nothing Then
                        '    ShowMessage(Translate("Ngày làm thêm đã được đăng ký"), NotifyType.Warning)
                        '    rdRegDate.ClearValue()
                        '    hidSignId.Value = Nothing
                        '    rdRegDate.Focus()
                        '    Exit Sub
                        'End If

                        EmployeeShift = rep.GetEmployeeShifts(hidEmpId.Value, rdRegDate.SelectedDate, rdRegDate.SelectedDate).FirstOrDefault
                        If EmployeeShift IsNot Nothing Then
                            hidSignId.Value = EmployeeShift.ID_SIGN
                            txtSignCode.Text = EmployeeShift.SIGN_CODE
                            If EmployeeShift.HOURS_START IsNot Nothing Then
                                rdHours_Start.SelectedDate = EmployeeShift.HOURS_START
                            End If
                            If EmployeeShift.HOURS_END IsNot Nothing Then
                                rdHours_Stop.SelectedDate = EmployeeShift.HOURS_END
                            End If
                            chkPassDay.Checked = EmployeeShift.IS_HOURS_STOP
                            'lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(rdRegDate.SelectedDate, rdRegDate.SelectedDate)

                        End If

                    End If
                End Using

                Using rep As New HistaffFrameworkRepository
                    Dim response = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_ACCUMULATIVE_OT", New List(Of Object)(New Object() {hidEmpId.Value, rdRegDate.SelectedDate.Value, OUT_NUMBER}))
                    If response IsNot Nothing Then
                        rntTotalAccumulativeOTHours.Text = Decimal.Parse(response(0).ToString()).ToString("N1")
                    End If
                End Using

                Dim store As New AttendanceStoreProcedure
                Dim dtData = store.GET_TIME_OT_COEFF_OVER(rdRegDate.SelectedDate)
                If dtData.Rows.Count > 0 AndAlso dtData IsNot Nothing Then
                    otFrTime = dtData.Rows(0)("FROMDATE_NIGHTHOUR_F")
                    otToTime = dtData.Rows(0)("TODATE_NIGHTHOUR_F")
                Else
                    ShowMessage("Chưa thiết lập hệ số OT", NotifyType.Warning)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgID.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'duy fix ngay 11/07
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgID.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            'LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub cboTypeOT_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTypeOT.SelectedIndexChanged
        Try
            change_cboTypeOT()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmpCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmpCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmpCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtFullName.Text = ""
                    ElseIf Count = 1 Then
                        Dim item = EmployeeList(0)
                        hidEmpId.Value = item.EMPLOYEE_ID
                        txtFullName.Text = item.FULLNAME_VN
                        txtDepartment.Text = item.ORG_NAME
                        txtEmpCode.Text = item.EMPLOYEE_CODE
                        txtTitle.Text = item.TITLE_NAME
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmpCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
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
        hidEmpId.Value = ""
        txtFullName.Text = ""
        txtDepartment.Text = ""
        'txtEmpCode.Text = ""
        txtTitle.Text = ""
    End Sub
#End Region

#Region "Custom"
    Private Sub change_cboTypeOT()
        Try
            If IsNumeric(cboTypeOT.SelectedValue) Then
                'input_data.Visible = False
                RequiredFieldValidator4.Enabled = False
                Dim OT_NN = ListComboData.LIST_LIST_OT_TYPE.Where(Function(f) f.CODE = "OT_NN").Select(Function(g) g.ID).FirstOrDefault
                Dim OT_KHAC = ListComboData.LIST_LIST_OT_TYPE.Where(Function(f) f.CODE = "OT_KHAC").Select(Function(g) g.ID).FirstOrDefault
                Dim OT_NB = ListComboData.LIST_LIST_OT_TYPE.Where(Function(f) f.CODE = "NB").Select(Function(g) g.ID).FirstOrDefault
                If cboTypeOT.SelectedValue = OT_NN Then 'OT theo hệ số NN
                    ClearControlValue(cbohs_ot)
                    EnableControlAll(False, cbohs_ot)
                ElseIf cboTypeOT.SelectedValue = OT_NB Then 'OT NGHI BU
                    ClearControlValue(cbohs_ot)
                    EnableControlAll(True, cbohs_ot)
                    RequiredFieldValidator4.Enabled = True
                    'input_data.Visible = True

                    'Dim rep As New AttendanceRepository
                    'If ListComboData Is Nothing Then
                    '    ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                    'End If
                    'ListComboData.GET_LIST_HS_OT_NB = True
                    'rep.GetComboboxData(ListComboData)

                    'Me.lstHsOT = ListComboData.LIST_LIST_HS_OT_NB
                    'FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT_NB, "NAME_VN", "ID", True)
                ElseIf cboTypeOT.SelectedValue = OT_KHAC Then 'OT theo hệ số Khác
                    EnableControlAll(True, cbohs_ot)
                    RequiredFieldValidator4.Enabled = True
                    'input_data.Visible = True
                Else
                    EnableControlAll(True, cbohs_ot)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Function LoadComboMinute() As DataTable
        Try
            Dim table As New DataTable
            table.Columns.Add("NAME_VN", GetType(String))
            table.Columns.Add("ID", GetType(Decimal))
            Dim row As DataRow
            For i = 0 To 59
                row = table.NewRow
                row("ID") = i
                row("NAME_VN") = i.ToString
                table.Rows.Add(row)
            Next

            Return table
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Function CalculateOT() As Boolean
        If rdRegDate.SelectedDate.HasValue Then
            Dim totalHour As New TimeSpan
            Dim fromAM As Decimal = 0.0
            Dim fromMNAM As Decimal = 0.0
            Dim toAM As Decimal = 0.0
            Dim toMMAM As Decimal = 0.0
            Dim fromPM As Decimal = 0.0
            Dim fromMNPM As Decimal = 0.0
            Dim toPM As Decimal = 0.0
            Dim toMNPM As Decimal = 0.0

            Dim totalFromAM As New DateTime
            Dim totalToAM As New DateTime
            Dim totalFromPM As New DateTime
            Dim totalToPM As New DateTime
            Dim OTAM As New TimeSpan
            Dim AM As New TimeSpan
            Dim OTPM As New TimeSpan
            Dim PM As New TimeSpan
            Dim SA As New TimeSpan
            Dim CH As New TimeSpan
            Dim totalDayTT As New TimeSpan
            Dim flagOT As Boolean = True

            Try
                'AM
                If rntbFromAM.Value.HasValue And rntbToAM.Value.HasValue Then
                    fromAM = IIf(rntbFromAM.Value.HasValue, rntbFromAM.Value, 0.0)
                    fromMNAM = Decimal.Parse(If(cboFromAM.SelectedValue, cboFromAM.SelectedValue, 0))
                    toAM = IIf(rntbToAM.Value.HasValue, rntbToAM.Value, 0.0)
                    toMMAM = Decimal.Parse(If(cboToAM.SelectedValue, cboToAM.SelectedValue, 0))

                    totalFromAM = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, fromAM, fromMNAM, 0)
                    totalToAM = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, toAM, toMMAM, 0)

                    If totalFromAM > totalToAM Then
                        ShowMessage(Translate("Giờ làm thêm AM không hợp lệ."), NotifyType.Warning)
                        rntbToAM.Focus()
                        Return False
                    End If
                    'If (totalFromAM >= otFrTime.AddDays(-1) And totalFromAM <= otToTime.AddDays(-1)) AndAlso totalToAM >= otToTime.AddDays(-1) Then
                    If totalFromAM <= otToTime.AddDays(-1) AndAlso totalToAM >= otToTime.AddDays(-1) Then
                        'If totalToAM >= otToTime.AddDays(-1) Then
                        '    OTAM += totalToAM - otToTime.AddDays(-1)
                        '    CH += otToTime.AddDays(-1) - totalFromAM
                        'Else
                        '    OTPM += totalToAM - totalFromAM
                        'End If
                        OTAM += totalToAM - totalFromAM ' otToTime.AddDays(-1)
                        CH += otToTime.AddDays(-1) - totalFromAM
                    ElseIf (totalToAM >= otFrTime.AddDays(-1) And totalToAM <= otToTime.AddDays(-1)) Then
                        If totalFromAM <= otFrTime.AddDays(-1) Then
                            CH += totalToAM - otFrTime.AddDays(-1)
                            OTAM += otFrTime.AddDays(-1) - totalFromAM
                        Else
                            OTPM += totalToAM - totalFromAM
                        End If
                    Else
                        flagOT = False
                        OTAM = totalToAM - totalFromAM
                    End If

                End If
                'PM
                If rntbFromPM.Value.HasValue And rntbToPM.Value.HasValue Then
                    fromPM = CDec(IIf(rntbFromPM.Value.HasValue, rntbFromPM.Value, 0.0))
                    fromMNPM = Decimal.Parse(If(cboFromPM.SelectedValue, cboFromPM.SelectedValue, 0))
                    toPM = CDec(IIf(rntbToPM.Value.HasValue, rntbToPM.Value, 0.0))
                    toMNPM = Decimal.Parse(If(cboToPM.SelectedValue, cboToPM.SelectedValue, 0))

                    totalFromPM = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, fromPM, fromMNPM, 0)

                    'Nếu nhập giờ sau ca đến = 24 thì chuyển về 00 giờ ngày hôm sau để tính đúng số giờ làm thêm
                    If toPM = 24 Then
                        'Lấy ngày đăng ký + 1
                        Dim day = rdRegDate.SelectedDate.Value.AddDays(1)
                        totalToPM = New DateTime(day.Year, day.Month, day.Day, 0, 0, 0)
                    Else
                        totalToPM = New DateTime(rdRegDate.SelectedDate.Value.Year, rdRegDate.SelectedDate.Value.Month, rdRegDate.SelectedDate.Value.Day, toPM, toMNPM, 0)
                    End If


                    If chkIsFrHourAfter.Checked Then
                        If chkIsToHourAfter.Checked Then
                            totalFromPM = totalFromPM.AddDays(1)
                            totalToPM = totalToPM.AddDays(1)
                        End If
                    Else
                        If chkIsToHourAfter.Checked Then
                            totalFromPM = totalFromPM
                            totalToPM = totalToPM.AddDays(1)
                        Else
                            totalFromPM = totalFromPM
                            totalToPM = totalToPM
                        End If
                    End If

                    If totalFromPM > totalToPM Then
                        ShowMessage(Translate("Từ giờ PM phải nhỏ hơn đến giờ PM."), NotifyType.Warning)
                        rntbFromPM.Focus()
                        Return False
                    End If
                    If (totalFromPM >= otFrTime And totalFromPM <= otToTime) Then
                        If totalToPM >= otToTime Then
                            SA += totalToPM - otToTime
                            OTPM += otToTime - totalFromPM
                        Else
                            OTPM += totalToPM - totalFromPM
                        End If
                    ElseIf (totalToPM >= otFrTime And totalToPM <= otToTime) Then
                        If totalFromPM <= otFrTime Then
                            OTPM += totalToPM - otFrTime
                            SA += otFrTime - totalFromPM
                        Else
                            OTPM += totalToPM - totalFromPM
                        End If
                    Else
                        flagOT = False
                        OTPM = totalToPM - totalFromPM
                    End If
                End If
                If flagOT Then
                    totalHour = OTAM + OTPM + SA + CH

                    hidTotal.Value = Decimal.Parse(totalHour.Hours.ToString + "," + If(totalHour.Minutes.ToString.Length < 2, "0" + totalHour.Minutes.ToString, totalHour.Minutes.ToString))

                    hidHourTotalNight.Value = Decimal.Parse((OTPM + CH).Hours.ToString + "," + If((OTPM + CH).Minutes.ToString.Length < 2, "0" + (OTPM + CH).Minutes.ToString, (OTPM + CH).Minutes.ToString))
                    hidHourTotalDay.Value = Decimal.Parse((OTAM + SA).Hours.ToString + "," + If((OTAM + SA).Minutes.ToString.Length < 2, "0" + (OTAM + SA).Minutes.ToString, (OTAM + SA).Minutes.ToString))

                    totalDayTT = totalHour - (OTPM + CH)
                    hidTotalDayTT = CDec(Val(totalDayTT.TotalHours)) ' Decimal.Parse(totalDayTT.Hours.ToString + "," + If(totalDayTT.Minutes.ToString.Length < 2, "0" + totalDayTT.Minutes.ToString, totalDayTT.Minutes.ToString))

                    'chittk fix
                    hidHourTotalNight.Value = CDec(Val(txtSumOT.Text)) - hidTotalDayTT

                Else
                    totalHour = OTAM + OTPM
                    hidHourTotalDay.Value = Decimal.Parse((OTAM + OTPM).Hours.ToString + "," + If((OTAM + OTPM).Minutes.ToString.Length < 2, "0" + (OTAM + OTPM).Minutes.ToString, (OTAM + OTPM).Minutes.ToString))
                    hidTotalDayTT = CDec(Val(totalHour.TotalHours)) ' If(IsNumeric(hidHourTotalDay.Value), CDec(hidHourTotalDay.Value), 0)
                    hidTotal.Value = Decimal.Parse(totalHour.Hours.ToString + "," + If(totalHour.Minutes.ToString.Length < 2, "0" + totalHour.Minutes.ToString, totalHour.Minutes.ToString))
                End If

                hidTimeCOEff_S.Value = Decimal.Parse((totalToAM - totalFromAM).Hours.ToString + "," + If((totalToAM - totalFromAM).Minutes.ToString.Length < 2, "0" + (totalToAM - totalFromAM).Minutes.ToString, (totalToAM - totalFromAM).Minutes.ToString))
                hidTimeCOEff_E.Value = Decimal.Parse((totalToPM - totalFromPM).Hours.ToString + "," + If((totalToPM - totalFromPM).Minutes.ToString.Length < 2, "0" + (totalToPM - totalFromPM).Minutes.ToString, (totalToPM - totalFromPM).Minutes.ToString))
                'txtSumOT.Text = hidTotal.Value.ToString
                Return True
            Catch ex As Exception
                ShowMessage(Translate("Thời gian OT không hợp lệ."), NotifyType.Warning)
                Return False
            End Try
        End If
    End Function


#End Region

End Class

