Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlLeaveDisplay
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Protected Property ListSign As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListSign")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListSign") = value
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
            Return "ctrlLeaveRegister"
        End Get
    End Property


    Private Property Employee_id As Decimal
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    Private Property lstHolidays As List(Of Date)
        Get
            Return PageViewState(Me.ID & "_lstHolidays")
        End Get
        Set(ByVal value As List(Of Date))
            PageViewState(Me.ID & "_lstHolidays") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgGrid.SetFilter()
    End Sub

    Public Overrides Sub BindData()
        Dim temp = AttendanceRepositoryStatic.Instance.GetSignByPage(PageId)
        ListSign = (From p In temp
                    Select New AT_TIME_MANUALDTO With {.ID = p.ID,
                                               .CODE = p.CODE,
                                               .NAME_VN = "[" & p.CODE & "]" & p.NAME_VN}).ToList()
        Dim dtData As DataTable
        dtData = AttendanceRepositoryStatic.Instance.GetEmployeeList
        FillRadCombobox(cboEmployee, dtData, "FULLNAME_VN", "EMPLOYEE_ID")
        rdDate.SelectedDate = Now
        LoadEmployeeAppointment()
    End Sub
#End Region

#Region "Event"

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
            Case chkWaitForApprove.Value
                e.Appointment.ForeColor = chkWaitForApprove.ForeColor
                e.Appointment.BorderColor = chkWaitForApprove.ForeColor
            Case chkApproved.Value
                e.Appointment.ForeColor = chkApproved.ForeColor
                e.Appointment.BorderColor = chkApproved.ForeColor
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

#End Region

#Region "Custom"
    ''' <summary>
    ''' Loads the employee appointment.
    ''' </summary>
    Private Sub LoadEmployeeAppointment()
        Dim startdate As Date
        Dim enddate As Date
        Dim curEmpId As Decimal = Employee_id

        Dim status As New List(Of Short)

        sdlRegister.SelectedView = SchedulerViewType.MonthView
        startdate = sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek
        enddate = sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek
        If chkWaitForApprove.Checked Then status.Add(chkWaitForApprove.Value)
        If chkApproved.Checked Then status.Add(chkApproved.Value)

        AppointmentList = AttendanceRepositoryStatic.Instance.GetRegisterAppointmentInPortalByEmployee(curEmpId, startdate, enddate, ListSign, status)
        sdlRegister.DataSource = AppointmentList
        sdlRegister.DataBind()
    End Sub

#End Region


    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim dtData As New DataTable("Data")
        dtData.Columns.Add("EMPLOYEE_CODE")
        dtData.Columns.Add("FULLNAME_VN")
        dtData.Columns.Add("NAME_VN")
        dtData.Columns.Add("NVALUE_NAME")
        dtData.Columns.Add("NOTE")
        dtData.Columns.Add("STATUS_NAME")
        If rdDate.SelectedDate IsNot Nothing Then
            dtData = AttendanceRepositoryStatic.Instance.GetLeaveDay(rdDate.SelectedDate)
        End If
        rgGrid.DataSource = dtData
    End Sub

    Private Sub rdDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdDate.SelectedDateChanged
        rgGrid.Rebind()
    End Sub

    Private Sub cboEmployee_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Employee_id = cboEmployee.SelectedValue
        BindData()
        LoadEmployeeAppointment()
    End Sub

End Class