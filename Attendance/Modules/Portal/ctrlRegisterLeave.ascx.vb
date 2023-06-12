Imports System.IO
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRegisterLeave
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListManual")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListManual") = value
        End Set
    End Property

    Private Property ListComboValue As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            PageViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    Private Property SelectedAppointmentList As List(Of AT_TIMESHEET_REGISTERDTO)
        Get
            Return PageViewState(Me.ID & "_SelectedAppointmentList")
        End Get
        Set(ByVal value As List(Of AT_TIMESHEET_REGISTERDTO))
            PageViewState(Me.ID & "_SelectedAppointmentList") = value
        End Set
    End Property

    Private Property AppointmentList As List(Of AT_TIMESHEET_REGISTERDTO)
        Get
            Return PageViewState(Me.ID & "_AppointmentList")
        End Get
        Set(ByVal value As List(Of AT_TIMESHEET_REGISTERDTO))
            PageViewState(Me.ID & "_AppointmentList") = value
        End Set
    End Property

    Private Property SelectedAppointmentListId As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedAppointmentListId")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedAppointmentListId") = value
        End Set
    End Property

    Public ReadOnly Property ApproveProcess As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Private Property lstHolidays As List(Of Date)
        Get
            Return PageViewState(Me.ID & "_lstHolidays")
        End Get
        Set(ByVal value As List(Of Date))
            PageViewState(Me.ID & "_lstHolidays") = value
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
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                ListComboData.GET_LIST_TIME_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboleaveType, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            ListManual = ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE
            If ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE.Count > 0 Then
                cboleaveType.SelectedValue = Nothing
            End If

            'FillRadCombobox(cboDate, ListComboData.LIST_LIST_TIME_SHIFT, "NAME_VN", "CODE", True)
            'If ListComboData.LIST_LIST_TIME_SHIFT.Count > 0 Then
            '    cboDate.SelectedValue = "TIME_SHIFT_1"
            'End If

            LoadEmployeeAppointment()
            noties.Visible = False
            noties1.Visible = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub chkStatus_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRegister.CheckedChanged,
        chkWaitForApprove.CheckedChanged, chkApproved.CheckedChanged, chkDenied.CheckedChanged
        LoadEmployeeAppointment()
    End Sub

    Protected Sub sdlRegister_TimeSlotContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs) Handles sdlRegister.TimeSlotContextMenuItemClicked
        Dim lstAppointment As New List(Of AT_TIMESHEET_REGISTERDTO)
        'Dim lstDup As List(Of AT_TIMESHEET_REGISTERDTO)
        Dim appointment As AT_TIMESHEET_REGISTERDTO
        Dim startDate As Date?
        Dim endDate As Date?
        Dim startDateInc As Date?
        Dim dNValue As Decimal = 0
        Dim startindex As Integer = 0
        Dim lstUIAppointment As List(Of Telerik.Web.UI.Appointment)
        Dim lstEmp As New List(Of EmployeeDTO)
        Dim sAction As String
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalDayRes As New DataTable
        Dim totalDayWanApp As New Decimal
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim rep As New AttendanceRepository
        Try
            startDate = hidStartDate.Value.ToDate()
            endDate = hidEndDate.Value.ToDate()
            If startDate Is Nothing Then
                startDate = e.TimeSlot.Start.Date
            End If
            If endDate Is Nothing Then
                endDate = e.TimeSlot.Start.Date
            End If
            Select Case e.MenuItem.Value
                Case "ADD"
                    Dim diff As TimeSpan = endDate.Value.AddDays(1) - startDate

                    If diff.Days >= 2 Then
                        ShowMessage(Translate("Mỗi lần đăng ký chỉ được đăng ký 1 ngày, bạn vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If lblDay.Text IsNot Nothing And lblDay.Text <> "" Then
                        If diff.Days > lblDay.Text.ToString Then
                            ShowMessage(Translate("Số ngày đăng ký đã vượt quá số ngày giới hạn/lần."), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    If lblYear.Text IsNot Nothing And lblDay.Text <> "" Then
                        If diff.Days + lblNgayDaNghi.Text > lblDay.Text.ToString Then
                            ShowMessage(Translate("Số ngày đăng ký đã vượt quá số ngày giới hạn/năm."), NotifyType.Warning)
                            Exit Sub
                        End If

                    End If

                    Dim lstAppoint = (From p In AppointmentList
                                      Where p.WORKINGDAY >= startDate _
                                      And p.WORKINGDAY <= endDate).ToList

                    If lstAppoint.Count > 0 Then
                        ShowMessage(Translate("Đã đăng ký nghỉ trong khoảng thời gian này. Vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If


                    ' check kỳ công đã đóng không được phép đăng ký nghỉ
                    lstEmp.Add(New EmployeeDTO With {.ID = CurrentUser.EMPLOYEE_ID,
                                                  .EMPLOYEE_CODE = CurrentUser.EMPLOYEE_CODE,
                                                  .FULLNAME_VN = CurrentUser.FULLNAME})

                    If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                                startDate, endDate, sAction) Then
                        ShowMessage(Translate(sAction), NotifyType.Warning)
                        Exit Sub
                    End If

                    If String.IsNullOrEmpty(cboleaveType.SelectedValue) Then
                        ShowMessage(Translate("Chưa chọn ký hiệu để đăng ký"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim sign = (From p In ListManual Where p.ID = Decimal.Parse(cboleaveType.SelectedValue)).FirstOrDefault

                    If lstHolidays.Where(Function(f) f >= startDate And f <= endDate).Count > 0 _
                        And sign.CODE <> "ML" Then
                        ShowMessage(Translate("Ngày lễ không thể đăng ký nghỉ. Thao tác thực hiện không thành công."), NotifyType.Warning)
                        Exit Sub
                    End If

                    ' Check bản ghi phê duyệt
                    'sAction = "IsApprovePortal"
                    'If Not AttendanceRepositoryStatic.Instance.CheckRegisterPortal(
                    '    New EmployeeDTO With {.ID = CurrentUser.EMPLOYEE_ID}, Guid.Empty,
                    '                      ApproveProcess, startDate, endDate,
                    '                      New AT_TIMESHEET_REGISTERDTO With {.SIGNID = Decimal.Parse(cboleaveType.SelectedValue),
                    '                                                .NVALUE = dNValue}, sAction) Then
                    '    ShowMessage(Translate(sAction), NotifyType.Warning)
                    '    LoadEmployeeAppointment()
                    '    Exit Sub
                    'End If

                    '' Check phép năm không được <-3 và phép bù không được < 0
                    dtDataUserPHEPNAM = rep.GetTotalPHEPNAM(CurrentUser.EMPLOYEE_ID, endDate.Value.Year, cboleaveType.SelectedValue)
                    at_entilement = rep.GetPhepNam(CurrentUser.EMPLOYEE_ID, endDate.Value.Year)
                    at_compensatory = rep.GetNghiBu(CurrentUser.EMPLOYEE_ID, endDate.Value.Year)
                    totalDayRes = rep.GetTotalDAY(CurrentUser.EMPLOYEE_ID, 251, startDate, endDate)
                    totalDayWanApp = rep.GetTotalLeaveInYear(CurrentUser.EMPLOYEE_ID, endDate.Value.Year)
                    ' nếu là kiểu đăng ký nghỉ phép
                    If sign.MORNING_ID = 251 And sign.AFTERNOON_ID = 251 Then
                        If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                            'If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                            If at_entilement.TOTAL_HAVE.Value - (totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                                ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    ElseIf sign.MORNING_ID = 251 Or sign.AFTERNOON_ID = 251 Then
                        If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                            If at_entilement.TOTAL_HAVE.Value - ((totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                                ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    End If
                    ' nếu là kiểu đăng ký nghỉ bù
                    dtDataUserPHEPBU = rep.GetTotalPHEPBU(CurrentUser.EMPLOYEE_ID, endDate.Value.Year, cboleaveType.SelectedValue)
                    totalDayRes = rep.GetTotalDAY(CurrentUser.EMPLOYEE_ID, 255, startDate, endDate)
                    If sign.MORNING_ID = 255 And sign.AFTERNOON_ID = 255 Then
                        If lblConLai.Text = "0" Then
                            ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                            Exit Sub
                        End If
                        If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                            If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalDayRes.Rows(0)(0)) < 0 Then
                                ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    ElseIf sign.MORNING_ID = 255 Or sign.AFTERNOON_ID = 255 Then
                        If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                            If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + (totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                                ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                        .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                        .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                        .FROM_DATE = startDate,
                        .TO_DATE = endDate,
                        .NVALUE = If(sign.MORNING_ID = sign.AFTERNOON_ID, 1, 0.5),
                        .SVALUE = ApproveProcess,
                        .NOTE = txtReason.Text + txtCancle.Text,
                        .NOTE_AT = txtReason.Text + txtCancle.Text,
                        .PROCESS = ApproveProcess
                    }

                    AttendanceRepositoryStatic.Instance.InsertPortalRegister(itemInsert)

                    LoadEmployeeAppointment()

                Case "DELINDATE"
                    lstEmp.Add(New EmployeeDTO With {.ID = CurrentUser.EMPLOYEE_ID,
                                   .EMPLOYEE_CODE = CurrentUser.EMPLOYEE_CODE,
                                   .FULLNAME_VN = CurrentUser.FULLNAME})
                    SelectedAppointmentList = New List(Of AT_TIMESHEET_REGISTERDTO)
                    startDateInc = startDate
                    Do
                        appointment = New AT_TIMESHEET_REGISTERDTO
                        appointment.EMPLOYEEID = CurrentUser.EMPLOYEE_ID
                        appointment.WORKINGDAY = startDateInc.Value
                        lstUIAppointment = sdlRegister.GetTimeSlotFromIndex(e.TimeSlot.Index + startindex).Appointments.ToList()
                        If lstUIAppointment.Count > 0 Then
                            If lstUIAppointment(0).ForeColor = chkApproved.ForeColor Then
                                ShowMessage(Translate("Không thể xoá những đăng ký đã phê duyệt."), NotifyType.Warning)
                                Exit Sub
                            End If
                            If lstUIAppointment(0).ForeColor = chkWaitForApprove.ForeColor Then
                                If txtCancle.Text = "" Then
                                    ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If

                        lstAppointment.Add(appointment)
                        startDateInc = startDateInc.Value.AddDays(1)
                        startindex += 1
                    Loop Until startDateInc > endDate

                    SelectedAppointmentList = lstAppointment
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DELINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SENDAPPROVEINDATE"

                    Dim lstAppoint = (From p In AppointmentList
                                      Where p.WORKINGDAY >= startDate _
                                      And p.WORKINGDAY <= endDate _
                                      And p.STATUS = 0).ToList

                    SelectedAppointmentListId = lstAppoint.Select(Function(f) f.ID).ToList

                    If lstAppoint.Count = 0 Then
                        ShowMessage(Translate("Không tồn tại bản ghi ở trạng thái chưa gửi duyệt. Thao tác thực hiện không thành công."), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each itm As AT_TIMESHEET_REGISTERDTO In lstAppoint
                        '' Check phép năm không được <-3 và phép bù không được < 0
                        dtDataUserPHEPNAM = rep.GetTotalPHEPNAM(CurrentUser.EMPLOYEE_ID, endDate.Value.Year, itm.SIGNID)
                        at_entilement = rep.GetPhepNam(CurrentUser.EMPLOYEE_ID, itm.WORKINGDAY.Year)
                        at_compensatory = rep.GetNghiBu(CurrentUser.EMPLOYEE_ID, itm.WORKINGDAY.Year)
                        totalDayRes = rep.GetTotalDAY(CurrentUser.EMPLOYEE_ID, 251, itm.WORKINGDAY, itm.WORKINGDAY)
                        totalDayWanApp = rep.GetTotalLeaveInYear(CurrentUser.EMPLOYEE_ID, itm.WORKINGDAY.Year)
                        ' nếu là kiểu đăng ký nghỉ phép
                        If itm.MORNING_ID = 251 And itm.AFTERNOON_ID = 251 Then
                            If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                                'If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                                If at_entilement.TOTAL_HAVE.Value - (totalDayRes.Rows(0)(0) + totalDayWanApp) < 0 Then
                                    ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        ElseIf itm.MORNING_ID = 251 Or itm.AFTERNOON_ID = 251 Then
                            If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                                If at_entilement.TOTAL_HAVE.Value - ((totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                                    ShowMessage(Translate("Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép."), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        End If
                        ' nếu là kiểu đăng ký nghỉ bù
                        dtDataUserPHEPBU = rep.GetTotalPHEPBU(CurrentUser.EMPLOYEE_ID, endDate.Value.Year, itm.SIGNID)
                        totalDayRes = rep.GetTotalDAY(CurrentUser.EMPLOYEE_ID, 255, itm.WORKINGDAY, itm.WORKINGDAY)
                        If itm.MORNING_ID = 255 And itm.AFTERNOON_ID = 255 Then
                            If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                                If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalDayRes.Rows(0)(0)) < 0 Then
                                    ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        ElseIf itm.MORNING_ID = 255 Or itm.AFTERNOON_ID = 255 Then
                            If dtDataUserPHEPBU IsNot Nothing And at_entilement IsNot Nothing Then
                                If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + (totalDayRes.Rows(0)(0) / 2) + totalDayWanApp) < 0 Then
                                    ShowMessage(Translate("Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép"), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Bạn muốn gửi tất cả những đăng ký trong ngày để phê duyệt?")
                    ctrlMessageBox.MessageTitle = Translate("Gửi duyệt")
                    ctrlMessageBox.ActionName = "SENDAPPROVEINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeAppointment()

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub sdlRegister_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles sdlRegister.AppointmentContextMenuItemClicked
        Try
            Select Case e.MenuItem.Value
                Case "DEL"
                    If e.Appointment.ForeColor = chkApproved.ForeColor Then
                        ShowMessage("Không thể xoá những đăng ký đã phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    SelectedAppointmentList = New List(Of AT_TIMESHEET_REGISTERDTO)
                    SelectedAppointmentList.Add(New AT_TIMESHEET_REGISTERDTO With {.ID = e.Appointment.ID})

                    If e.Appointment.ForeColor = chkWaitForApprove.ForeColor Then
                        If txtCancle.Text = "" Then
                            ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    SelectedAppointmentListId = New List(Of Decimal)
                    SelectedAppointmentListId.Add(e.Appointment.ID)
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DELINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SENDAPPROVE"
                    If e.Appointment.ForeColor = chkApproved.ForeColor Then
                        ShowMessage("Đăng ký đã được phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    If e.Appointment.ForeColor = chkWaitForApprove.ForeColor Then
                        ShowMessage("Đăng ký đang được chờ phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    If e.Appointment.ForeColor = chkDenied.ForeColor Then
                        ShowMessage("Đăng ký đã bị từ chối.", NotifyType.Warning)
                        Exit Sub
                    End If

                    SelectedAppointmentListId = New List(Of Decimal)
                    SelectedAppointmentListId.Add(e.Appointment.ID)
                    ctrlMessageBox.MessageText = Translate("Bạn muốn gửi đăng ký để phê duyệt?")
                    ctrlMessageBox.MessageTitle = Translate("Gửi duyệt")
                    ctrlMessageBox.ActionName = "SENDAPPROVE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeAppointment()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub sdlRegister_NavigationComplete(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerNavigationCompleteEventArgs) Handles sdlRegister.NavigationComplete
        Select Case e.Command
            Case SchedulerNavigationCommand.NavigateToNextPeriod,
               SchedulerNavigationCommand.NavigateToPreviousPeriod,
               SchedulerNavigationCommand.SwitchToSelectedDay,
               SchedulerNavigationCommand.NavigateToSelectedDate
                lstHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalender(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                   sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
                LoadEmployeeAppointment()
        End Select
    End Sub

    Protected Sub sdlRegister_AppointmentDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerEventArgs) Handles sdlRegister.AppointmentDataBound
        Dim objData As AT_TIMESHEET_REGISTERDTO = CType(e.Appointment.DataItem, AT_TIMESHEET_REGISTERDTO)
        e.Appointment.ToolTip = objData.NOTE
        Select Case objData.STATUS
            Case chkRegister.Value
                e.Appointment.ForeColor = chkRegister.ForeColor
                e.Appointment.BorderColor = chkRegister.ForeColor
            Case chkWaitForApprove.Value
                e.Appointment.ForeColor = chkWaitForApprove.ForeColor
                e.Appointment.BorderColor = chkWaitForApprove.ForeColor
            Case chkApproved.Value
                e.Appointment.ForeColor = chkApproved.ForeColor
                e.Appointment.BorderColor = chkApproved.ForeColor
            Case chkDenied.Value
                e.Appointment.ForeColor = chkDenied.ForeColor
                e.Appointment.BorderColor = chkDenied.ForeColor
        End Select
    End Sub

    Protected Sub sdlRegister_TimeSlotCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotCreatedEventArgs) Handles sdlRegister.TimeSlotCreated
        If lstHolidays Is Nothing Then
            lstHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalender(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                   sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
        End If
        For Each dt As DateTime In lstHolidays
            If DateTime.Compare(e.TimeSlot.Start.[Date], dt) = 0 Then
                e.TimeSlot.CssClass = "Holidays"
            End If
        Next
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rtnVal As Boolean
        Dim rtnString As String
        Dim rep As New AttendanceRepository
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case "DELINDATE"
                        For Each item As AT_TIMESHEET_REGISTERDTO In SelectedAppointmentList
                            Dim receiver As String = ""
                            Dim cc As String = ""
                            Dim subject As String
                            Dim body As String = ""
                            Dim fileAttachments As String = String.Empty
                            Dim reader As StreamReader
                            Dim data As DataTable
                            Dim view As String = ""

                            data = rep.GET_AT_PORTAL_REG(item.ID, item.EMPLOYEEID, item.WORKINGDAY)
                            If data(0)("STATUS").ToString = "0" Then
                                Continue For
                            End If
                            If txtCancle.Text = "" Then
                                ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                                Exit Sub
                            End If

                            subject = String.Format("V/v {0} đăng ký nghỉ ngày {1} - {2} đã được rút lại", data(0)("HOTEN"), data(0)("FROM_DATE"), data(0)("TO_DATE"))
                            reader = New StreamReader(Server.MapPath("~/Modules/Attendance/Templates/AT_TIME_CANCEL.html"))

                            body = reader.ReadToEnd
                            body = body.Replace("{HOTEN}", data(0)("HOTEN").ToString)
                            body = body.Replace("{LOAINGHI}", data(0)("LEAVE_NAME").ToString)
                            body = body.Replace("{TUNGAY}", data(0)("FROM_DATE").ToString)
                            body = body.Replace("{DENNGAY}", data(0)("TO_DATE").ToString)
                            body = body.Replace("{LYDO}", txtCancle.Text)

                            cc = data(0)("WORK_EMAIL").ToString
                            receiver = data(0)("MAIL_TO").ToString
                            view = "AT_TIME_CANCEL.html"

                            If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, view) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Next
                        rtnVal = AttendanceRepositoryStatic.Instance.DeletePortalRegisterByDate(SelectedAppointmentList, ListManual)

                        LoadEmployeeAppointment()
                        LoadTotalP()

                    Case "DEL"
                        rtnString = AttendanceRepositoryStatic.Instance.SendRegisterToApprove(SelectedAppointmentListId,
                                                                                             ApproveProcess,
                                                                                             Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost)

                        If String.IsNullOrEmpty(rtnString) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            LoadEmployeeAppointment()
                            LoadTotalP()
                        Else : ShowMessage(Translate(rtnString), NotifyType.Error)
                        End If
                        rtnVal = AttendanceRepositoryStatic.Instance.DeletePortalRegister(SelectedAppointmentList(0).ID)
                    Case "SENDAPPROVEINDATE", "SENDAPPROVE"
                        rtnString = AttendanceRepositoryStatic.Instance.SendRegisterToApprove(SelectedAppointmentListId,
                                                                                              ApproveProcess,
                                                                                              Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost)

                        If String.IsNullOrEmpty(rtnString) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            LoadEmployeeAppointment()
                            LoadTotalP()
                        Else : ShowMessage(Translate(rtnString), NotifyType.Error)
                        End If
                        Exit Sub
                End Select

                If rtnVal Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    LoadEmployeeAppointment()
                    LoadTotalP()
                Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub

    Private Sub cboleaveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboleaveType.SelectedIndexChanged
        Try
            LoadTotalP()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub LoadTotalP()
        Dim rep As New AttendanceRepository
        If cboleaveType.SelectedValue <> "" Then
            Dim sign = (From p In ListManual Where p.ID = Decimal.Parse(cboleaveType.SelectedValue)).FirstOrDefault
            Dim _filter As New TotalDayOffDTO
            Dim obj As New TotalDayOffDTO
            _filter.LEAVE_TYPE = sign.MORNING_ID
            _filter.TIME_MANUAL_ID = Decimal.Parse(cboleaveType.SelectedValue)
            _filter.DATE_REGISTER = hidStartDate.Value.ToDate()
            If _filter.DATE_REGISTER Is Nothing Then
                _filter.DATE_REGISTER = sdlRegister.SelectedDate
            End If
            _filter.EMPLOYEE_ID = CurrentUser.EMPLOYEE_ID
            If sign.AFTERNOON_ID = 251 Or sign.AFTERNOON_ID = 255 Or sign.CODE.Contains("P") Then
                noties.Visible = True
                noties1.Visible = True
                obj = rep.GetTotalDayOff(_filter)
                If obj IsNot Nothing Then
                    If obj.TOTAL_DAY IsNot Nothing Then
                        lblNgayDuocNghi.Text = If(obj.TOTAL_DAY = 0, 0, Decimal.Parse(obj.TOTAL_DAY).ToString())
                    Else
                        lblNgayDuocNghi.Text = Decimal.Parse(0).ToString("#,#.##")
                    End If
                    If obj.USED_DAY IsNot Nothing Then
                        lblNgayDaNghi.Text = If(obj.USED_DAY = 0, 0, Decimal.Parse(obj.USED_DAY).ToString())
                    Else
                        lblNgayDaNghi.Text = Decimal.Parse(0).ToString()
                    End If
                    If obj.REST_DAY IsNot Nothing Then
                        lblConLai.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                    Else
                        lblConLai.Text = Decimal.Parse(0).ToString()
                    End If
                    lblDay.Text = obj.LIMIT_DAY
                    lblYear.Text = obj.LIMIT_YEAR


                    If obj.PREV_HAVE IsNot Nothing Then
                        lblPREV_HAVE.Text = If(obj.PREV_HAVE = 0, 0, Decimal.Parse(obj.PREV_HAVE).ToString())
                    Else
                        lblPREV_HAVE.Text = Decimal.Parse(0).ToString()
                    End If
                    If obj.PREV_USED IsNot Nothing Then
                        lblPREV_USED.Text = If(obj.PREV_USED = 0, 0, Decimal.Parse(obj.PREV_USED).ToString())
                    Else
                        lblPREV_USED.Text = Decimal.Parse(0).ToString()
                    End If
                    If obj.PREVTOTAL_HAVE IsNot Nothing Then
                        lblPREVTOTAL_HAVE.Text = If(obj.PREVTOTAL_HAVE = 0, 0, Decimal.Parse(obj.PREVTOTAL_HAVE).ToString())
                    Else
                        lblPREVTOTAL_HAVE.Text = Decimal.Parse(0).ToString()
                    End If
                    If obj.SENIORITYHAVE IsNot Nothing Then
                        lblSENIORITYHAVE.Text = If(obj.SENIORITYHAVE = 0, 0, Decimal.Parse(obj.SENIORITYHAVE).ToString())
                    Else
                        lblSENIORITYHAVE.Text = Decimal.Parse(0).ToString()
                    End If
                    If obj.TOTAL_HAVE1 IsNot Nothing Then
                        lblTOTAL_HAVE1.Text = If(obj.TOTAL_HAVE1 = 0, 0, Decimal.Parse(obj.TOTAL_HAVE1).ToString())
                    Else
                        lblTOTAL_HAVE1.Text = Decimal.Parse(0).ToString()
                    End If
                End If
            Else
                lblNgayDuocNghi.Text = String.Empty
                lblNgayDaNghi.Text = String.Empty
                lblConLai.Text = String.Empty
                obj = rep.GET_TIME_MANUAL(_filter)
                lblDay.Text = obj.LIMIT_DAY
                lblYear.Text = obj.LIMIT_YEAR
                If obj.USED_DAY IsNot Nothing Then
                    lblNgayDaNghi.Text = If(obj.USED_DAY = 0, 0, Decimal.Parse(obj.USED_DAY).ToString())
                Else
                    lblNgayDaNghi.Text = Decimal.Parse(0).ToString()
                End If
                noties.Visible = True
                noties1.Visible = False
            End If
        Else
            lbAnnual.Visible = False
        End If
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Loads the employee appointment.
    ''' </summary>

    Private Sub LoadEmployeeAppointment()
        Dim startdate As Date
        Dim enddate As Date
        Dim curEmpId As Decimal = LogHelper.CurrentUser.EMPLOYEE_ID

        Dim status As New List(Of Short)

        sdlRegister.SelectedView = SchedulerViewType.MonthView
        startdate = sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek
        enddate = sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek

        If chkRegister.Checked Then status.Add(chkRegister.Value)
        If chkWaitForApprove.Checked Then status.Add(chkWaitForApprove.Value)
        If chkApproved.Checked Then status.Add(chkApproved.Value)
        If chkDenied.Checked Then status.Add(chkDenied.Value)

        AppointmentList = AttendanceRepositoryStatic.Instance.GetRegisterAppointmentInPortalByEmployee(curEmpId, startdate, enddate, ListManual, status)
        sdlRegister.DataSource = AppointmentList
        sdlRegister.DataBind()
        LoadTotalP()
    End Sub

    'Private Function CheckEmployeeAppointment(ByVal lstEmp As List(Of EmployeeDTO),
    '                                          ByVal startdate As Date, ByVal enddate As Date,
    '                                          ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String)
    '    'Try
    '    '    Return AttendanceRepositoryStatic.Instance.CheckRegisterAppointmentByEmployee(lstEmp, startdate, enddate, ListSign, sign_code, sAction)
    '    'Catch ex As Exception
    '    '    Throw ex
    '    'End Try
    'End Function

#End Region

End Class