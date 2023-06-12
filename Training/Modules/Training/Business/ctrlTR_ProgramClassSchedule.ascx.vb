﻿Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramClassSchedule
    Inherits Common.CommonView

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Private Property lstSchedule As List(Of ProgramClassScheduleDTO)
        Get
            Return PageViewState(Me.ID & "_lstSchedule")
        End Get
        Set(ByVal value As List(Of ProgramClassScheduleDTO))
            PageViewState(Me.ID & "_lstSchedule") = value
        End Set
    End Property

    Property Program As String
        Get
            Return ViewState(Me.ID & "_Program")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Program") = value
        End Set
    End Property

    Property ProFromDate As Date
        Get
            Return ViewState(Me.ID & "_ProFromDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProFromDate") = value
        End Set
    End Property

    Property ProToDate As Date
        Get
            Return ViewState(Me.ID & "_ProToDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProToDate") = value
        End Set
    End Property

    Property SelectedID As Decimal
        Get
            Return ViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property
    Private Property SelectedAppointmentList As List(Of ProgramClassScheduleDTO)
        Get
            Return PageViewState(Me.ID & "_SelectedAppointmentList")
        End Get
        Set(ByVal value As List(Of ProgramClassScheduleDTO))
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
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'SetGridFilter(rgMain)
        'rgMain.AllowCustomPaging = True
        'rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Seperator, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidClassID.Value = Request.Params("CLASS_ID")
                Dim obj As ProgramClassDTO
                Using rep As New TrainingRepository
                    obj = rep.GetClassByID(New ProgramClassDTO With {.ID = hidClassID.Value})
                End Using
                txtClassName.Text = obj.NAME
                rdClassEnd.SelectedDate = obj.END_DATE
                rdClassStart.SelectedDate = obj.START_DATE
                'If obj.TR_PROGRAM_ID IsNot Nothing Then
                '    Dim prog As ProgramDTO
                '    Using rep As New TrainingRepository
                '        prog = rep.GetProgramById(obj.TR_PROGRAM_ID)
                '        Program = prog.NAME
                '        If prog.START_DATE IsNot Nothing Then
                '            ProFromDate = prog.START_DATE
                '        End If
                '        If prog.END_DATE IsNot Nothing Then
                '            ProToDate = prog.END_DATE
                '        End If
                '    End Using
                'End If
                ProFromDate = obj.START_DATE
                ProToDate = obj.END_DATE
                GetSchedule()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        'rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        'rgMain.CurrentPageIndex = 0
                        'rgMain.MasterTableView.SortExpressions.Clear()
                        'rgMain.Rebind()

                    Case "Cancel"
                        'rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    UpdateControlStatus(True)

                Case CommonMessage.STATE_NORMAL
                    UpdateControlStatus(False)
                    ClearControlValue()

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of ProgramClassScheduleDTO)
                    'For idx = 0 To rgMain.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgMain.SelectedItems(idx)
                    '    lstDeletes.Add(New ProgramClassScheduleDTO With {.ID = item.GetDataKeyValue("ID")})
                    'Next
                    If rep.DeleteClassSchedule(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub UpdateControlStatus(ByVal state As Boolean)
        'EnabledGridNotPostback(rgMain, Not state)
        EnableRadDatePicker(dpFromDate, state)
        EnableRadDatePicker(dpToDate, state)
        EnableRadDatePicker(dtpFromTime, state)
        EnableRadDatePicker(dtpToTime, state)
        txtContent.Enabled = state
        btnRegist.Enabled = state
        txtTOTAL_TIME.Enabled = state
        'chkIsHalf.Enabled = state
        'chkIsHC.Enabled = state
    End Sub
    Public Sub ClearControlValue()
        'EnabledGridNotPostback(rgMain, Not state)
        dpFromDate.SelectedDate = Nothing
        dpToDate.SelectedDate = Nothing
        dtpFromTime.SelectedDate = Nothing
        dtpToTime.SelectedDate = Nothing
        txtContent.Text = ""
        txtTOTAL_TIME.Value = 0
        'chkIsHalf.Enabled = state
        'chkIsHC.Enabled = state
    End Sub
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)
            'dic.Add("IS_HC", chkIsHalf)
            'dic.Add("IS_HALF", chkIsHC)
            dic.Add("START_DATE", dtpFromTime)
            dic.Add("END_DATE", dtpToTime)
            dic.Add("CONTENT", txtContent)
            dic.Add("TOTAL_TIME", txtTOTAL_TIME)
            'Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub sdlRegister_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles sdlRegister.AppointmentContextMenuItemClicked
        Try
            Select Case e.MenuItem.Value
                Case "DEL"
                    SelectedAppointmentList = New List(Of ProgramClassScheduleDTO)
                    SelectedAppointmentList.Add(New ProgramClassScheduleDTO With {.ID = e.Appointment.ID})

                    SelectedAppointmentListId = New List(Of Decimal)
                    SelectedAppointmentListId.Add(e.Appointment.ID)
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DELINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objClassSchedule As New ProgramClassScheduleDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    'If rgMain.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                For Each sche As ProgramClassScheduleDTO In lstSchedule
                                    If sche.IS_HC = 0 Then '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
                                        With objClassSchedule
                                            .TR_CLASS_ID = sche.TR_CLASS_ID
                                            .START_TIME = sche.START_TIME
                                            .END_TIME = sche.END_TIME
                                            .CONTENT = sche.CONTENT
                                            .TOTAL_TIME = sche.TOTAL_TIME
                                        End With

                                        If Not rep.InsertClassSchedule(objClassSchedule, gID) Then
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Next
                                CurrentState = CommonMessage.STATE_NORMAL
                                Refresh("InsertView")
                                ClearControlValue()
                                UpdateControlState()
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New TrainingRepository
        Dim rtnVal As Boolean
        Dim rtnString As String
        Try
            'If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            '    Dim lstClassSchedule As New List(Of ProgramClassScheduleDTO)
            '    lstClassSchedule.Add(New ProgramClassScheduleDTO With {.ID = SelectedID})
            '    If rep.DeleteClassSchedule(lstClassSchedule) Then
            '        CurrentState = CommonMessage.STATE_NORMAL
            '        ClearControlValue()
            '        UpdateControlState()
            '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            '    Else
            '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            '        Exit Sub
            '    End If
            'End If
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case "DELINDATE"
                        If rep.DeleteClassSchedule(SelectedAppointmentList) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ClearControlValue()
                            UpdateControlState()
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            lstSchedule.Clear()
                            lstSchedule = New List(Of ProgramClassScheduleDTO)
                            GetSchedule()
                            LoadSchedule()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetSchedule()
        Dim dtData As DataSet
        Dim tsp As New TrainingStoreProcedure
        Try
            dtData = tsp.GetSchedule(hidClassID.Value, ProFromDate, ProToDate)

            Dim unsaveList As New List(Of ProgramClassScheduleDTO)
            If lstSchedule IsNot Nothing AndAlso lstSchedule.Count > 0 Then
                unsaveList = (From s In lstSchedule Where s.IS_HC = 2) '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
            End If

            lstSchedule = New List(Of ProgramClassScheduleDTO)
            For Each dr As DataRow In dtData.Tables(0).Rows
                Dim schedule As New ProgramClassScheduleDTO
                schedule.ID = dr("ID").ToString
                schedule.TR_CLASS_ID = dr("TR_CLASS_ID").ToString
                schedule.IS_HC = dr("IS_HC").ToString
                schedule.PROGRAM = dr("PROGRAM").ToString
                schedule.START_TIME = dr("START_TIME").ToString
                schedule.END_TIME = dr("END_TIME").ToString
                schedule.CONTENT = dr("CONTENT").ToString()
                schedule.SUBJECT = ConvertData(schedule.START_TIME.Value.TimeOfDay, _
                                               schedule.END_TIME.Value.TimeOfDay, Program)

                If schedule.IS_HC = 2 Then '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
                    Dim lstStudent As New List(Of ProgramClassStudentDTO)
                    For Each drw As DataRow In dtData.Tables(1).Rows
                        Dim stu As New ProgramClassStudentDTO
                        stu.TR_CLASS_ID = drw("TR_CLASS_ID").ToString()
                        stu.EMPLOYEE_ID = drw("EMPLOYEE_ID").ToString()
                        stu.EMPLOYEE_CODE = drw("EMPLOYEE_CODE").ToString()
                        stu.EMPLOYEE_NAME = drw("FULLNAME_VN").ToString()
                        lstStudent.Add(stu)
                    Next
                    schedule.STUDENTS = lstStudent
                End If

                lstSchedule.Add(schedule)
            Next

            If unsaveList IsNot Nothing Then
                lstSchedule.AddRange(unsaveList)
            End If

            LoadSchedule()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnRegist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegist.Click
        Try
            Dim ExitSub As Boolean = False
            If dpFromDate.SelectedDate Is Nothing Then
                ExitSub = ConvertData("Vui lòng chọn ngày học")
                dpFromDate.Focus()
            ElseIf dpFromDate.SelectedDate < rdClassStart.SelectedDate Or rdClassEnd.SelectedDate < dpFromDate.SelectedDate Then
                ExitSub = ConvertData("Ngày bắt đầu lịch học nằm ngoài Thời gian học<br />Vui lòng kiểm tra lại")
                dtpFromTime.Focus()
            ElseIf dpToDate.SelectedDate IsNot Nothing And (dpToDate.SelectedDate < rdClassStart.SelectedDate Or rdClassEnd.SelectedDate < dpToDate.SelectedDate) Then
                ExitSub = ConvertData("Ngày kết thúc lịch học nằm ngoài Thời gian học<br />Vui lòng kiểm tra lại")
                dtpFromTime.Focus()
            ElseIf dpToDate.SelectedDate IsNot Nothing And (dpFromDate.SelectedDate > dpToDate.SelectedDate) Then
                ExitSub = ConvertData("Ngày kết thúc lịch học > Ngày bắt đầu lịch học<br />Vui lòng kiểm tra lại")
                dtpFromTime.Focus()
            ElseIf dtpFromTime.SelectedTime Is Nothing Then
                ExitSub = ConvertData("Vui lòng chọn thời gian học")
                dtpFromTime.Focus()
            ElseIf dtpToTime.SelectedTime Is Nothing Then
                ExitSub = ConvertData("Vui lòng chọn thời gian học")
                dtpToTime.Focus()
            End If

            If ExitSub Then Exit Sub

            Dim startdate As Date = dpFromDate.SelectedDate
            Dim enddate As Date
            If dpToDate.SelectedDate Is Nothing Then
                enddate = startdate
            Else
                enddate = dpToDate.SelectedDate
            End If
            enddate = enddate.AddDays(1)

            If lstSchedule Is Nothing Then lstSchedule = New List(Of ProgramClassScheduleDTO)

            Do Until startdate = enddate
                Dim schedule As New ProgramClassScheduleDTO
                schedule.ID = CDec(Today.Ticks)
                schedule.TR_CLASS_ID = hidClassID.Value
                schedule.IS_HC = 0 '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
                schedule.PROGRAM = Program
                schedule.START_TIME = ConvertData(startdate, dtpFromTime.SelectedTime)
                schedule.END_TIME = ConvertData(startdate, dtpToTime.SelectedTime)
                schedule.CONTENT = txtContent.Text
                schedule.SUBJECT = ConvertData(dtpFromTime.SelectedTime, dtpToTime.SelectedTime, Program)
                schedule.TOTAL_TIME = txtTOTAL_TIME.Value
                lstSchedule.Add(schedule)

                startdate = startdate.AddDays(1)
            Loop

            LoadSchedule()

            ClearControlValue()

            'sdlRegister.SelectedView = SchedulerViewType.MonthView
            'startdate = sdlRegister.SelectedDate
            'enddate = sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek
            'Dim myAppointment As New Appointment(Date.Now, startdate, enddate, "Description")
            'myAppointment.ToolTip = "Custom Tooltip"
            'sdlRegister.Appointments.Add(myAppointment)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Function ConvertData(ByVal dDate As Date, ByVal dTime As TimeSpan)
        Try
            Return DateTime.ParseExact(dDate.ToString("yyyyMMdd") & dTime.ToString("hhmm"), "yyyyMMddHHmm", Nothing)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function ConvertData(ByVal dFromTime As TimeSpan, ByVal dToTime As TimeSpan, ByVal proName As String)
        Try
            Return dFromTime.ToString("hh\:mm") & " - " & dToTime.ToString("hh\:mm") & ": " & proName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function ConvertData(ByVal Message As String)
        Try
            ShowMessage(Translate(Message), Utilities.NotifyType.Error, 10)
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub LoadSchedule()
        Try
            If lstSchedule Is Nothing Then
                lstSchedule = New List(Of ProgramClassScheduleDTO)
            End If
            If lstSchedule.Count > 0 Then
                LoadToolTip(lstSchedule)
            End If
            sdlRegister.DataSource = lstSchedule
            sdlRegister.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadToolTip(ByRef lstScheDu As List(Of ProgramClassScheduleDTO))
        Try
            Dim br = "<br />"
            Dim bbr = "<br /><br />"
            For Each sche As ProgramClassScheduleDTO In lstScheDu
                sche.TOOLTIP = "Khóa: " & sche.PROGRAM & bbr & _
                               "Thời gian: " & sche.START_TIME.Value.ToString("hh\:mm") & "-" & sche.END_TIME.Value.ToString("hh\:mm") & bbr & _
                               "Nội dung: " & sche.CONTENT
                If sche.STUDENTS IsNot Nothing Then
                    If sche.STUDENTS.Count > 0 Then
                        sche.TOOLTIP &= bbr & "Các học viên trùng khóa:"
                        For Each em As ProgramClassStudentDTO In sche.STUDENTS
                            sche.TOOLTIP &= br & "- " & em.EMPLOYEE_CODE & ", " & em.EMPLOYEE_NAME
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub sdlRegister_TimeSlotCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotCreatedEventArgs) Handles sdlRegister.TimeSlotCreated
        Try
            If rdClassStart.SelectedDate IsNot Nothing Then
                If e.TimeSlot.Start.Date < rdClassStart.SelectedDate Then
                    e.TimeSlot.CssClass = "Disabled"
                End If
                If e.TimeSlot.Start.Date = rdClassStart.SelectedDate Then
                    e.TimeSlot.CssClass = "FirstLaunch"
                    'sdlRegister.SelectedDate = rdClassStart.SelectedDate
                    'e.TimeSlot.Start = rdClassStart.SelectedDate
                End If
            End If

            If rdClassEnd.SelectedDate IsNot Nothing Then
                If e.TimeSlot.Start.Date > rdClassEnd.SelectedDate Then
                    e.TimeSlot.CssClass = "Disabled"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub RadScheduler1_AppointmentCreated(ByVal sender As Object, ByVal e As AppointmentCreatedEventArgs) Handles sdlRegister.AppointmentCreated
        Try
            If e.Appointment.Visible Then
                Dim App As ProgramClassScheduleDTO = (From s In lstSchedule Where s.ID = e.Appointment.ID).FirstOrDefault
                If App.IS_HC = 2 Then '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
                    e.Appointment.ForeColor = Drawing.Color.Red
                End If
                e.Appointment.ToolTip = App.TOOLTIP
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub sdlRegister_AppointmentClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerEventArgs) Handles sdlRegister.AppointmentClick
        Try
            ClearControlValue()
            Dim App As ProgramClassScheduleDTO = (From s In lstSchedule Where s.ID = e.Appointment.ID).FirstOrDefault
            If App.IS_HC <> 2 Then '0 = Chưa lưu, 1 = Đã lưu, 2 = lịch trùng
                SelectedID = App.ID
                dpFromDate.SelectedDate = App.START_TIME
                dtpFromTime.SelectedTime = App.START_TIME.Value.TimeOfDay
                dpToDate.SelectedDate = App.END_TIME
                dtpToTime.SelectedTime = App.END_TIME.Value.TimeOfDay
                txtTOTAL_TIME.Value = App.TOTAL_TIME.Value
                txtContent.Text = App.CONTENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"
#End Region

    Private Sub dtpToTime_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpToTime.SelectedDateChanged
        If dtpToTime.SelectedTime IsNot Nothing And dtpFromTime.SelectedTime IsNot Nothing Then
            txtTOTAL_TIME.Value = dtpToTime.SelectedDate.Value.Hour - dtpFromTime.SelectedDate.Value.Hour
        End If
    End Sub

    Private Sub dtpFromTime_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpFromTime.SelectedDateChanged
        If dtpToTime.SelectedTime IsNot Nothing And dtpFromTime.SelectedTime IsNot Nothing Then
            txtTOTAL_TIME.Value = dtpToTime.SelectedDate.Value.Hour - dtpFromTime.SelectedDate.Value.Hour
        End If

    End Sub
End Class