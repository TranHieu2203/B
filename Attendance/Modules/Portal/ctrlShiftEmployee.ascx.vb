Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlShiftEmployee
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Private Property EmployeeShiftList As List(Of EMPLOYEE_SHIFT_DTO)
        Get
            Return PageViewState(Me.ID & "_EmployeeShiftList")
        End Get
        Set(ByVal value As List(Of EMPLOYEE_SHIFT_DTO))
            PageViewState(Me.ID & "_EmployeeShiftList") = value
        End Set
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

    Private Property lstdtHolidays As DataTable
        Get
            Return PageViewState(Me.ID & "_lstdtHolidays")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_lstdtHolidays") = value
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
            LoadEmployeeShift()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub sdlRegister_TimeSlotContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs) Handles sdlRegister.TimeSlotContextMenuItemClicked
        Dim startDate As Date?
        Dim endDate As Date?
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
                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeShift()

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub sdlRegister_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles sdlRegister.AppointmentContextMenuItemClicked
        Try
            Select Case e.MenuItem.Value
                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeShift()
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
                lstdtHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                   sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
                LoadEmployeeShift()
        End Select
    End Sub

    Protected Sub sdlRegister_TimeSlotCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotCreatedEventArgs) Handles sdlRegister.TimeSlotCreated
        Try
            If lstdtHolidays Is Nothing Then
                lstdtHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                       sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
            End If
            'Weekend and off day
            Dim dto = EmployeeShiftList.Where(Function(f) f.EFFECTIVEDATE = e.TimeSlot.Start.[Date]).FirstOrDefault()
            'If (dto IsNot Nothing AndAlso (e.TimeSlot.Start.[Date].DayOfWeek.ToString() = "Sunday" Or e.TimeSlot.Start.[Date].DayOfWeek.ToString() = "Saturday" Or dto.IS_LEAVE)) Then
            '    e.TimeSlot.CssClass = "Weekends"
            'End If

            If (dto IsNot Nothing AndAlso dto.SIGN_CODE = "OFF") OrElse e.TimeSlot.Start.DayOfWeek = DayOfWeek.Saturday _
                OrElse e.TimeSlot.Start.DayOfWeek = DayOfWeek.Sunday Then
                e.TimeSlot.CssClass = "OffDay"
            End If

            If lstdtHolidays IsNot Nothing AndAlso lstdtHolidays.Rows.Count > 0 Then
                For Each dr In lstdtHolidays.Rows
                    If DateTime.Compare(e.TimeSlot.Start.[Date], DateTime.Parse(dr("VALUE"))) = 0 Then
                        'Holidays
                        e.TimeSlot.CssClass = "Holidays"
                        'Compensatory
                        If Not IsDBNull(dr("OFFDAY")) Then
                            If dr("OFFDAY") = "-1" Then
                                e.TimeSlot.CssClass = "Compensatory"
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try

        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
    Protected Sub sdlRegister_AppointmentDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerEventArgs) Handles sdlRegister.AppointmentDataBound

    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Loads the employee appointment.
    ''' </summary>

    Private Sub LoadEmployeeShift()
        Try
            Dim startdate As Date
            Dim enddate As Date
            Dim curEmpId As Decimal = LogHelper.CurrentUser.EMPLOYEE_ID

            Dim status As New List(Of Short)

            sdlRegister.SelectedView = SchedulerViewType.MonthView
            startdate = sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek
            enddate = sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek

            EmployeeShiftList = AttendanceRepositoryStatic.Instance.GetEmployeeShifts(curEmpId, startdate, enddate)
            sdlRegister.DataSource = EmployeeShiftList
            sdlRegister.DataBind()
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub

#End Region

End Class