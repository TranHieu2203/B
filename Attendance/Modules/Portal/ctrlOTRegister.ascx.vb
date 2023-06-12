Imports System.Globalization
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI

Public Class ctrlOTRegister
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False
    Dim log As New UserLog
    Dim psp As New AttendanceStoreProcedure
    Dim com As New CommonProcedureNew
    Dim cons As New Contant_OtherList_Attendance
    Dim cons_com As New Contant_Common
    Private rep As New HistaffFrameworkRepository

#Region "Property"
    Private Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property
    Private Property EmployeeId As Decimal
        Get
            Return PageViewState(Me.ID & "_EmployeeId")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_EmployeeId") = value
        End Set
    End Property
    Private Property perioId As Integer
        Get
            Return ViewState(Me.ID & "_perioId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_perioId") = value
        End Set
    End Property

    Private Property orgId As Integer
        Get
            Return ViewState(Me.ID & "_orgId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_orgId") = value
        End Set


    End Property
    Private Property TotalOT As Decimal
        Get
            Return ViewState(Me.ID & "_TotalOT")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_TotalOT") = value
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
            Return ATConstant.GSIGNCODE_OVERTIME
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

    Private Property AppointmentList As List(Of APPOINTMENT_DTO)
        Get
            Return PageViewState(Me.ID & "_AppointmentList")
        End Get
        Set(ByVal value As List(Of APPOINTMENT_DTO))
            PageViewState(Me.ID & "_AppointmentList") = value
        End Set
    End Property

    Private Property lstHolidays As List(Of Date)
        Get
            Return ViewState(Me.ID & "_lstHolidays")
        End Get
        Set(ByVal value As List(Of Date))
            ViewState(Me.ID & "_lstHolidays") = value
        End Set
    End Property
    Protected Property ListManual As List(Of OT_OTHERLIST_DTO)
        Get
            Return PageViewState(Me.ID & "_ListManual")
        End Get
        Set(ByVal value As List(Of OT_OTHERLIST_DTO))
            PageViewState(Me.ID & "_ListManual") = value
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
        If Not IsPostBack Then
            EmployeeId = CurrentUser.EMPLOYEE_ID
            CreateDataTableUpdate()
            perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(sdlRegister.SelectedDate.FirstDateOfMonth)
            orgId = AttendanceRepositoryStatic.Instance.GET_ORGID(EmployeeId)
            'Lấy tổng số giờ OT dã đăng ký
            'TotalOT = GET_TOTAL_OT_APPROVE(EmployeeId, sdlRegister.SelectedDate.LastDateOfMonth)
            'rntxtOT_USED.Text = "Tổng số giờ OT thực tế : " + TotalOT.ToString
            LoadOT()
        End If
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try

            'Me.MainToolBar = tbarMainToolBar
            'BuildToolbar(Me.MainToolBar, ToolbarItem.Print)
            'CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Giải trình dấu vân tay")
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        LoadEmployeeAppointment()
        Dim dtData As DataTable
        dtData = AttendanceRepositoryStatic.Instance.GET_LIST_HOURS()
        If dtData.Rows.Count > 0 Then
            FillRadCombobox(cboHoursFrom, dtData, "NAME", "ID", False)
            FillRadCombobox(cboHoursTo, dtData, "NAME", "ID", False)
        End If
        dtData = AttendanceRepositoryStatic.Instance.GET_LIST_MINUTE()
        If dtData.Rows.Count > 0 Then
            FillRadCombobox(cboMinuteFrom, dtData, "NAME", "ID", False)
            FillRadCombobox(cboMinuteTo, dtData, "NAME", "ID", False)
        End If
    End Sub
#End Region

#Region "Event"
    Protected Sub chkStatus_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRegister.CheckedChanged,
        chkWaitForApprove.CheckedChanged, chkApproved.CheckedChanged, chkDenied.CheckedChanged
        LoadEmployeeAppointment()
    End Sub

    ''' <summary>
    ''' Handles the TimeSlotContextMenuItemClicked event of the sdlRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs" /> instance containing the event data.</param>
    Protected Sub sdlRegister_TimeSlotContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs) Handles sdlRegister.TimeSlotContextMenuItemClicked
        'Dim lstAppointment As New List(Of APPOINTMENT_DTO)
        'Dim lstDup As List(Of APPOINTMENT_DTO)
        'Dim appointment As APPOINTMENT_DTO
        Dim startDate As Date?
        Dim endDate As Date?
        'Dim startDateInc As Date?
        'Dim dNValue As Decimal?
        Dim startindex As Integer = 0
        Dim i As Integer = 0
        ' Dim lstUIAppointment As List(Of Telerik.Web.UI.Appointment)
        'Dim lstEmp As New List(Of EmployeeDTO)
        Try
            startDate = hifStartDate.Value.ToDate()
            endDate = hifEndDate.Value.ToDate()
            If startDate Is Nothing Then
                startDate = e.TimeSlot.Start.Date
            End If
            If endDate Is Nothing Then
                endDate = e.TimeSlot.Start.Date
            End If

            'Kiẻm tra trạng thái bảng công
            If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                Exit Sub
            End If

            Select Case e.MenuItem.Value
                Case "DELINDATE"
                    Dim lstAppoint = (From p In AppointmentList
                                      Where p.WORKINGDAY >= startDate _
                                      And p.WORKINGDAY <= endDate _
                                      And (p.STATUS = 0 Or p.STATUS = 3)).ToList

                    SelectedAppointmentListId = lstAppoint.Select(Function(f) f.ID).ToList

                    If lstAppoint.Count = 0 Then
                        ShowMessage(Translate("Không tồn tại bản ghi ở trạng thái chưa gửi duyệt. Thao tác thực hiện không thành công."), NotifyType.Warning)
                        Exit Sub
                    End If
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

    ''' <summary>
    ''' Handles the AppointmentContextMenuItemClicked event of the sdlRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs" /> instance containing the event data.</param>
    Protected Sub sdlRegister_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles sdlRegister.AppointmentContextMenuItemClicked
        Try
            Select Case e.MenuItem.Value
                Case "DEL"
                    'Kiẻm tra trạng thái bảng công
                    If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                        Exit Sub
                    End If
                    Dim lstAppoint = (From p In AppointmentList
                                      Where p.ID = e.Appointment.ID And (p.STATUS = 0 Or p.STATUS = 3)).ToList
                    If lstAppoint.Count = 0 Then
                        ShowMessage(Translate("Không tồn tại bản ghi ở trạng thái chưa gửi duyệt. Thao tác thực hiện không thành công."), NotifyType.Warning)
                        Exit Sub
                    End If
                    SelectedAppointmentListId = lstAppoint.Select(Function(f) f.ID).ToList
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DEL"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SENDAPPROVE"
                    'Kiẻm tra trạng thái bảng công
                    If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                        Exit Sub
                    End If
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
        Dim rtnVal As Decimal
        Dim startDate As Date?
        Dim startDate_temp As Date?
        Dim endDate As Date?
        startDate = rdFromDate.SelectedDate
        endDate = rdToDate.SelectedDate
        Dim NOTE As String = ""
        Dim strId As String
        Dim lstId As New List(Of Decimal)
        'Dim VALUES As Decimal?

        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                dtTable.Clear()
                Select Case e.ActionName
                    Case "BTNREGISTER"

                        'Dim NEXTDATE = DateTime.Now.AddMonths(1)

                        'If (rdFromDate.SelectedDate > NEXTDATE And rdToDate.SelectedDate > NEXTDATE) Or (NEXTDATE >= rdFromDate.SelectedDate And rdToDate.SelectedDate > NEXTDATE) Then
                        '    ShowMessage(Translate("Thời gian đăng ký làm thêm không hợp lệ, vui lòng kiểm tra lại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'Lay ID group moi lan dang ky portal ( muc dich de su dung bao cao)
                        Dim IdGroup = AttendanceRepositoryStatic.Instance.GET_SEQ_PORTAL_RGT()
                        If AppointmentList IsNot Nothing Then
                            For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= startDate And f.WORKINGDAY <= endDate And f.STATUS = 0)
                                lstId.Add(item.ID)
                                NOTE = item.NOTE
                            Next
                            ' Lấy danh sách ID portal register
                            For Each dr As Decimal In lstId
                                strId &= IIf(strId = vbNullString, dr, "," & dr)
                            Next
                            If strId = "," Then
                                strId = ""
                            End If
                        End If
                        dtTable.TableName = "DATA"
                        startDate_temp = rdFromDate.SelectedDate

                        Dim dateFrom As Date
                        Dim dateTo As Date
                        Dim strDatafrom As String = rdFromDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                        Dim strDataTo As String = rdToDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

                        DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDatafrom, cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateFrom)
                        DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDataTo, cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateTo)

                        Dim difference As TimeSpan = dateTo - dateFrom
                        Do
                            Dim dr As DataRow = dtTable.NewRow()
                            dr("P_EMPLOYEE_ID") = CurrentUser.EMPLOYEE_ID
                            dr("P_WORKINGDAY") = startDate_temp
                            dr("P_SIGN_CODE") = ATConstant.SIGINCODE_OT
                            dr("P_NVALUE") = difference.TotalHours
                            If cboHoursFrom.Text <> "" And cboMinuteFrom.Text <> "" Then
                                dr("P_FROM_HOUR_STRING") = String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue)
                            End If
                            If cboHoursTo.Text <> "" And cboMinuteTo.Text <> "" Then
                                dr("P_TO_HOUR_STRING") = String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue)
                            End If
                            dr("P_NOTE") = txtReason.Text
                            dr("P_ID_GROUP") = IdGroup
                            dr("P_CREATED_BY") = log.Username
                            dr("P_CREATED_LOG") = log.Ip + "/" + log.ComputerName
                            dr("P_NB") = If(chkNB.Checked, 1, 0)
                            dtTable.Rows.Add(dr)
                            startDate_temp = startDate_temp.Value.AddDays(1)
                        Loop Until startDate_temp > endDate
                        rtnVal = CreatePlanningAppointment(dtTable)
                        If rtnVal Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            LoadEmployeeAppointment()
                        Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Case "DELINDATE", "DEL"
                        ' Lấy danh sách ID portal register
                        For Each dr As Decimal In SelectedAppointmentListId
                            strId &= IIf(strId = vbNullString, dr, "," & dr)
                        Next
                        If strId = "," Then
                            strId = ""
                        End If
                        Dim check = psp.AT_DELETE_PORTAL_REG(strId)
                        If check = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            LoadEmployeeAppointment()

                        Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        Exit Sub

                    Case "SENDAPPROVEINDATE", "SENDAPPROVE"
                        Dim repPublic As New EmailCommon
                        ' Lấy danh sách ID portal register
                        Dim sUser As String = LogHelper.CurrentUser.EMPLOYEE_CODE
                        ' Lấy danh sách ID portal register
                        'Dim IdGroup1 = psp.GET_SEQ_PORTAL_RGT()
                        For Each dr As Decimal In SelectedAppointmentListId
                            strId &= IIf(strId = vbNullString, dr, "," & dr)
                        Next

                        Dim dtCheckSendApprove As DataTable = psp.CHECK_SEND_APPROVE(strId)

                        'Dim dt = psp.GET_INFO_EMPLOYEE(strId)
                        'If dt.Rows.Count > 0 Then
                        '    Dim fromdate = (From p In AppointmentList Where SelectedAppointmentListId.Contains(p.ID) Select p.WORKINGDAY).Min
                        '    Dim TOdate = (From p In AppointmentList Where SelectedAppointmentListId.Contains(p.ID) Select p.WORKINGDAY).Max

                        '    For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= fromdate And f.WORKINGDAY <= TOdate And f.STATUS = 0)
                        '        If Regex.IsMatch(NOTE, String.Format("\b{0}\b", Regex.Escape(item.NOTE))) Then
                        '            Continue For
                        '        End If
                        '        NOTE &= IIf(NOTE = vbNullString, item.NOTE, "," & item.NOTE)
                        '    Next
                        '    Dim receiver As String = dt(0)("LOCAL_MAIL_DM").ToString()
                        '    If receiver = "" Then
                        '        ShowMessage("Email không tồn tại", NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        '    Dim cc As String = ""
                        '    'If(dt(0)("PER_EMAIL").Text.ToString() = "&nbsp;", "", dt(0)("LOCAL_MAIL").Text.ToString())
                        '    Dim subject As String = "Đăng ký làm thêm."
                        '    Dim body As String = ""
                        '    Dim fileAttachments As String = String.Empty
                        '    'format body by html template
                        '    Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Attendance/Templates/DangKyPortal.htm"))
                        '    body = reader.ReadToEnd
                        '    body = body.Replace("{GENDER}", dt(0)("GENDER").ToString())
                        '    body = body.Replace("{GENDER_DM}", dt(0)("GENDER_DM").ToString())
                        '    body = body.Replace("{FULLNAME}", dt(0)("FULLNAME").ToString())
                        '    body = body.Replace("{FULLNAME_DM}", dt(0)("FULLNAME_DM").ToString())
                        '    body = body.Replace("{FROM_DATE}", DateTime.ParseExact(fromdate.Value.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        '    body = body.Replace("{TO_DATE}", DateTime.ParseExact(TOdate.Value.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        '    body = body.Replace("{CONTENT}", "OT")
                        '    body = body.Replace("{REMARK}", NOTE)
                        '    body = body.Replace("{TYPE}", "ctrlOTApprove")
                        '    body = body.Replace("{LINK_APPOVE_PORTAL}", dt(0)("LINK_APPOVE_PORTAL").ToString())
                        '    If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments) Then
                        '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
                        '        CurrentState = CommonMessage.STATE_NORMAL
                        '        UpdateControlState()
                        '    Else
                        '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
                        '    End If
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
                        'End If
                        LoadEmployeeAppointment()

                        Dim periodId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("PERIOD_ID").ToString())
                        Dim signId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("SIGN_ID").ToString())
                        Dim totalHours As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("SUMVAL").ToString())
                        Dim IdGroup1 As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("ID_REGGROUP").ToString())
                        Dim outNumber As Decimal = AttendanceRepositoryStatic.Instance.PRI_PROCESS_APP(EmployeeId, periodId, "OVERTIME", totalHours, 0, signId, IdGroup1, "")

                        If outNumber = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                End Select

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegister.Click
        Try
            Dim startDate As Date?
            Dim startDate_temp As Date?
            Dim endDate As Date?
            Dim Sum_Values As Decimal = 0
            Dim Values As Decimal = 0
            Dim SumPRgtUsed As Decimal? = 0 ' Phep nam da dang ky
            Dim SumPRgtCurrent As Decimal? = 0 ' Phep nam dang dang ky
            Dim difference As TimeSpan
            If CheckValiadte() Then
                perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(rdFromDate.SelectedDate)
                'Kiẻm tra trạng thái bảng công
                If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                    Exit Sub
                End If
                perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(rdToDate.SelectedDate)
                'Kiểm tra trạng thái bảng công
                If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                    Exit Sub
                End If

                If Not CheckEmployee(EmployeeId, rdFromDate.SelectedDate) Then
                    Exit Sub
                End If

                Dim dateFrom As Date
                Dim dateTo As Date
                Dim strDatafrom As String = rdFromDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                Dim strDataTo As String = rdToDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

                DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDatafrom, cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateFrom)
                DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDataTo, cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateTo)

                If dateFrom > dateTo Then
                    difference = dateTo.AddDays(1) - dateFrom
                Else
                    difference = dateTo - dateFrom
                End If
                Values = difference.TotalHours

                startDate = rdFromDate.SelectedDate
                endDate = rdToDate.SelectedDate
                startDate_temp = rdFromDate.SelectedDate

                Dim RGT_OT_TO As New Date?
                Do
                    For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= startDate_temp AndAlso f.WORKINGDAY <= startDate_temp)
                        ' Từ giờ đã đăng ký trước đó
                        Dim FROM_HOUR = DateTime.ParseExact(startDate_temp.Value.Date.ToString("dd/MM/yyyy") + " " + Format(item.FROMHOUR, "HH:mm").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        ' Đến giờ đã đăng ký trước đó
                        Dim TO_HOUR = DateTime.ParseExact(startDate_temp.Value.Date.ToString("dd/MM/yyyy") + " " + Format(item.TOHOUR, "HH:mm").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        ' Từ giờ đăng ký
                        Dim RGT_OT_FROM = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.Date.ToString("dd/MM/yyyy"), cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        'Đến giờ đăng ký
                        If dateFrom > dateTo Then
                            RGT_OT_TO = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.AddDays(1).Date.ToString("dd/MM/yyyy"), cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        Else
                            RGT_OT_TO = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.Date.ToString("dd/MM/yyyy"), cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        End If

                        If RGT_OT_FROM >= FROM_HOUR AndAlso RGT_OT_FROM < TO_HOUR AndAlso TO_HOUR <= RGT_OT_TO Then
                            ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If RGT_OT_FROM < FROM_HOUR AndAlso FROM_HOUR < RGT_OT_TO Then
                            ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If RGT_OT_FROM >= FROM_HOUR AndAlso RGT_OT_TO <= TO_HOUR Then
                            ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    Sum_Values = Sum_Values + Values
                    startDate_temp = startDate_temp.Value.AddDays(1)
                Loop Until startDate_temp > endDate
                startDate_temp = startDate

                Dim total_value_rgt = AttendanceRepositoryStatic.Instance.GET_TOTAL_OT_APPROVE3(EmployeeId, endDate)
                Dim check = AttendanceRepositoryStatic.Instance.CHECK_RGT_OT(EmployeeId, startDate, endDate, String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue) _
                                            , String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), Sum_Values + total_value_rgt)
                If check = 1 Then
                    ShowMessage(Translate("Nhân viên chưa được thiết lập ca hoặc ca không tồn tại, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 2 Then
                    ShowMessage(Translate("Chức danh của nhân viên này không được đăng ký làm thêm, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 3 Then
                    ShowMessage(Translate("Thời gian đăng ký OT không nằm trong khoảng ca làm việc, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 4 Then
                    ShowMessage(Translate("Thời gian đăng ký nằm trong khoảng thời gian không tính tăng ca, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 5 Then
                    ShowMessage(Translate("Số giờ đăng ký vượt quá quy định, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 6 Then
                    ShowMessage(Translate("Không cho đăng ký làm thêm đối với trường hợp thai sản , vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 7 Then
                    ShowMessage(Translate("Không cho đăng ký làm thêm đối với trường hợp nuôi con dưới 12 tháng tuổi, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                ElseIf check = 8 Then
                    ShowMessage(Translate("Nhân viên thử việc không được đăng ký làm thêm, vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
                ctrlMessageBox.MessageText = "Bạn có muốn đăng ký không"
                ctrlMessageBox.MessageTitle = "Cảnh báo"
                ctrlMessageBox.ActionName = "BTNREGISTER"
                ctrlMessageBox.DataBind()
                ctrlMessageBox.Show()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Loads the employee appointment.
    ''' </summary>
    Private Sub LoadEmployeeAppointment()
        Dim startdate As Date
        Dim enddate As Date
        Dim sv_sID As String = String.Empty
        Dim status As New List(Of Short)

        sdlRegister.SelectedView = SchedulerViewType.MonthView
        startdate = sdlRegister.SelectedDate.FirstDateOfMonth
        enddate = sdlRegister.SelectedDate.LastDateOfMonth

        If chkRegister.Checked Then status.Add(chkRegister.Value)
        If chkWaitForApprove.Checked Then status.Add(chkWaitForApprove.Value)
        If chkApproved.Checked Then status.Add(chkApproved.Value)
        If chkDenied.Checked Then status.Add(chkDenied.Value)

        For Each ID As Integer In status
            sv_sID &= IIf(sv_sID = vbNullString, ID, "," & ID)
        Next

        AppointmentList = AttendanceRepositoryStatic.Instance.GET_REG_PORTAL(CurrentUser.EMPLOYEE_ID, startdate, enddate, sv_sID, cons.OVERTIME)
        LoadOT()
        sdlRegister.DataSource = AppointmentList
        sdlRegister.DataBind()
    End Sub
    Public Function CreatePlanningAppointment(ByVal dtTable As DataTable) As Integer
        Return rep.ExecuteBatchCommand("PKG_AT_ATTENDANCE_PORTAL.AT_INSERT_PORTAL_REG", dtTable)
    End Function
    Private Function CreateDataTableUpdate() As DataTable
        'Các cột đi theo thứ tự của các cột trong store update
        dtTable = New DataTable
        dtTable.TableName = "DATA"
        dtTable.Columns.Add("P_EMPLOYEE_ID", GetType(Decimal))
        dtTable.Columns.Add("P_WORKINGDAY", GetType(Date))
        dtTable.Columns.Add("P_SIGN_CODE", GetType(String))
        dtTable.Columns.Add("P_NVALUE", GetType(Decimal))
        dtTable.Columns.Add("P_TYPE_LEAVE", GetType(String))
        dtTable.Columns.Add("P_FROM_HOUR_STRING", GetType(String))
        dtTable.Columns.Add("P_TO_HOUR_STRING", GetType(String))
        dtTable.Columns.Add("P_NOTE", GetType(String))
        dtTable.Columns.Add("P_ID_GROUP", GetType(Decimal))
        dtTable.Columns.Add("P_CREATED_BY", GetType(String))
        dtTable.Columns.Add("P_CREATED_LOG", GetType(String))
        dtTable.Columns.Add("P_NB", GetType(Decimal))
        Return dtTable
    End Function
    Private Sub LoadOT()
        ' Hiển thị các thông tin OT thực tế
        TotalOT = GET_TOTAL_OT_APPROVE(CurrentUser.EMPLOYEE_ID, sdlRegister.SelectedDate.LastDateOfMonth)
        rntxtOT_USED.Text = "Tổng số giờ OT thực tế : " + TotalOT.ToString
    End Sub

    Private Function CheckOrgPeriodCloseOT(ByVal ORG_ID As String, ByVal perioId As Decimal) As Boolean
        If perioId <> 0 Then
            Dim check = AttendanceRepositoryStatic.Instance.AT_CHECK_ORG_PERIOD_STATUS_OT(ORG_ID, perioId)
            If check = 1 Then
                ShowMessage(Translate("Bảng công đã được đóng trước đó, vui lòng kiểm tra lại"), NotifyType.Warning)
                Return False
            ElseIf check = 2 Then
                ShowMessage(Translate("Phòng ban chưa được thiết lập trạng thái kỳ công"), NotifyType.Warning)
                Return False
            End If
        Else
            ShowMessage(Translate("Kỳ công không tồn tại, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        Return True
    End Function
    Private Function GET_TOTAL_OT_APPROVE(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Dim result = AttendanceRepositoryStatic.Instance.GET_TOTAL_OT_APPROVE(EMPID, ENDDATE)
        Return result
    End Function
    'Private Function CheckEmployeeAppointment(ByVal lstEmp As List(Of EmployeeDTO), ByVal startdate As Date, ByVal enddate As Date, ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String)
    '    Try
    '        Return AttendanceRepositoryStatic.Instance.CheckRegisterAppointmentByEmployee(lstEmp, startdate, enddate, ListSign, sign_code, sAction)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Private Function CheckValiadte() As Boolean

        If rdFromDate.SelectedDate Is Nothing Or rdToDate.SelectedDate Is Nothing Then
            ShowMessage(Translate("Bạn chưa chọn ngày, vui lòng kiểm tra lại "), NotifyType.Warning)
            Return False
        End If
        'If rtpFROM.SelectedTime Is Nothing Or rtpTO.SelectedTime Is Nothing Then
        '    ShowMessage(Translate("Bạn chưa thời gian, vui lòng kiểm tra lại"), NotifyType.Warning)
        '    Return False
        'End If
        If rdFromDate.SelectedDate IsNot Nothing AndAlso rdToDate.SelectedDate IsNot Nothing AndAlso _
            rdToDate.SelectedDate < rdFromDate.SelectedDate Then
            ShowMessage(Translate("Đến ngày phải lớn hơn từ ngày, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        'If rtpFROM.SelectedTime IsNot Nothing AndAlso rtpTO.SelectedTime IsNot Nothing AndAlso rtpTO.SelectedDate < rtpFROM.SelectedDate Then
        '    ShowMessage(Translate("Thời gian kết thúc phải lớn hơn thời gian bắt đầu, vui lòng kiểm tra lại"), NotifyType.Warning)
        '    Return False
        'End If
        If txtReason.Text.Trim = "" Then
            ShowMessage(Translate("Bạn chưa nhập lý do "), NotifyType.Warning)
            Return False
        End If
        'For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= rdFromdate.SelectedDate And f.WORKINGDAY <= rdToDate.SelectedDate)
        '    If rdFromdate.SelectedDate = item.WORKINGDAY Then
        '        ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
        '        Return False
        '    End If
        'Next
        Return True
    End Function

    Private Function CheckEmployee(ByVal empID As Decimal?, ByVal ENDDATE As Date) As Boolean
        Dim check = AttendanceRepositoryStatic.Instance.AT_CHECK_EMPLOYEE(empID, ENDDATE)
        If check = 1 Then
            ShowMessage(Translate("Nhân viên chưa có quyết định, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        ElseIf check = 2 Then
            ShowMessage(Translate("Cộng tác viên không thể đăng ký, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        Return True
    End Function
#End Region

End Class