Imports System.Collections.Generic
Imports TAS.CheckInOutDataServices.Client.AttendaceBusiness

Public Class frmMain_MCC
    Inherits System.Windows.Forms.Form
    Dim mv_dtListTerminal As DataTable
    Dim bln_flag As Boolean = False
    Dim isLoad As Boolean = False
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdConfig As System.Windows.Forms.Button
    Friend WithEvents timReading As System.Windows.Forms.Timer
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents dtpDateGetData As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbAuto As System.Windows.Forms.CheckBox
    Friend WithEvents txtActivity As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents bw As System.ComponentModel.BackgroundWorker
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain_MCC))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cbAuto = New System.Windows.Forms.CheckBox()
        Me.dtpDateGetData = New System.Windows.Forms.DateTimePicker()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdConfig = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.timReading = New System.Windows.Forms.Timer(Me.components)
        Me.txtActivity = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.bw = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Controls.Add(Me.cbAuto)
        Me.Panel1.Controls.Add(Me.dtpDateGetData)
        Me.Panel1.Controls.Add(Me.cmdClear)
        Me.Panel1.Controls.Add(Me.cmdStart)
        Me.Panel1.Controls.Add(Me.cmdConfig)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Controls.Add(Me.cmdStop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1024, 67)
        Me.Panel1.TabIndex = 35
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdExit.Location = New System.Drawing.Point(891, 19)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(128, 35)
        Me.cmdExit.TabIndex = 46
        Me.cmdExit.Text = "Exit"
        '
        'cbAuto
        '
        Me.cbAuto.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cbAuto.AutoSize = True
        Me.cbAuto.Checked = True
        Me.cbAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAuto.Enabled = False
        Me.cbAuto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuto.Location = New System.Drawing.Point(61, 24)
        Me.cbAuto.Name = "cbAuto"
        Me.cbAuto.Size = New System.Drawing.Size(115, 27)
        Me.cbAuto.TabIndex = 45
        Me.cbAuto.Text = "Automatic"
        Me.cbAuto.UseVisualStyleBackColor = True
        '
        'dtpDateGetData
        '
        Me.dtpDateGetData.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.dtpDateGetData.CalendarFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateGetData.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateGetData.Enabled = False
        Me.dtpDateGetData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateGetData.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateGetData.Location = New System.Drawing.Point(185, 20)
        Me.dtpDateGetData.Name = "dtpDateGetData"
        Me.dtpDateGetData.Size = New System.Drawing.Size(151, 29)
        Me.dtpDateGetData.TabIndex = 44
        Me.dtpDateGetData.Value = New Date(2015, 8, 21, 16, 41, 33, 0)
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdClear.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdClear.Location = New System.Drawing.Point(619, 19)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(128, 35)
        Me.cmdClear.TabIndex = 43
        Me.cmdClear.Text = "Clear"
        '
        'cmdStart
        '
        Me.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdStart.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdStart.Location = New System.Drawing.Point(347, 19)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(128, 35)
        Me.cmdStart.TabIndex = 41
        Me.cmdStart.Text = "Start"
        '
        'cmdConfig
        '
        Me.cmdConfig.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdConfig.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdConfig.Location = New System.Drawing.Point(483, 19)
        Me.cmdConfig.Name = "cmdConfig"
        Me.cmdConfig.Size = New System.Drawing.Size(128, 35)
        Me.cmdConfig.TabIndex = 36
        Me.cmdConfig.Text = "Config"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdClose.Location = New System.Drawing.Point(755, 19)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(128, 35)
        Me.cmdClose.TabIndex = 35
        Me.cmdClose.Text = "Hide"
        '
        'cmdStop
        '
        Me.cmdStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdStop.Location = New System.Drawing.Point(347, 19)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(128, 35)
        Me.cmdStop.TabIndex = 42
        Me.cmdStop.Text = "Stop"
        Me.cmdStop.Visible = False
        '
        'timReading
        '
        '
        'txtActivity
        '
        Me.txtActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtActivity.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActivity.Location = New System.Drawing.Point(3, 76)
        Me.txtActivity.Name = "txtActivity"
        Me.txtActivity.Size = New System.Drawing.Size(1024, 282)
        Me.txtActivity.TabIndex = 36
        Me.txtActivity.Text = ""
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtActivity, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1030, 361)
        Me.TableLayoutPanel1.TabIndex = 37
        '
        'bw
        '
        '
        'frmMain_MCC
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(8, 22)
        Me.ClientSize = New System.Drawing.Size(1030, 361)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain_MCC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TAS SDK Services: Đọc dữ liệu IO"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Event"

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtpDateGetData.Value = Date.Now 'là Auto thì lấy thời gian hiện tại
            LoadConfig()
            InitNotifyIcon()
            TimerStart(False)
            CloseAllowed = False
            If Not isLoad Then
                isLoad = True
                bw.RunWorkerAsync()
            End If
        Catch ex As Exception
            TS_AddLogText(String.Format("[frmMain_Load] ", ex.Message))
        End Try
    End Sub

    Private Sub frmMain_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'If Not CloseAllowed AndAlso e.CloseReason = CloseReason.UserClosing Then
        '    e.Cancel = True
        '    Me.Hide()
        '    notifyIcon.ShowBalloonTip(2000)
        'End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        CloseAllowed = False
        Me.Close()
    End Sub

    Private Sub cmdConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfig.Click
        TimerStop()
        Dim frm As New frmConfig_MCC
        frm.ShowDialog()
        LoadConfig()
        TimerStart()
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtActivity.Clear()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        TimerStart()
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        TimerStop()
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        AppExit()
    End Sub

    Private Sub cbAuto_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbAuto.CheckedChanged
        If cbAuto.Checked Then
            dtpDateGetData.Enabled = False
            TS_AddLogText("Lấy dữ liệu từ Máy chấm công => Tự động")
        Else
            dtpDateGetData.Enabled = True
            TS_AddLogText("Lấy dữ liệu từ Máy chấm công => Tùy chỉnh")
        End If
    End Sub

    Private Sub timReading_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timReading.Tick
        Try
            If cbAuto.Checked Then
                dtpDateGetData.Value = Date.Now 'là Auto thì lấy thời gian hiện tại
            End If

            dtpDateGetData.Enabled = False
            If bw.IsBusy = False Then
                bw.RunWorkerAsync()
            End If
            dtpDateGetData.Enabled = True
        Catch ex As Exception
            TS_AddLogText(String.Format("[frmMain_Load] ", ex.Message))
        End Try
    End Sub

    Private Sub bw_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw.DoWork
        Try
            If bln_flag = False Then
                RunRequest()
            End If
        Catch ex As Exception
            TS_AddLogText(String.Format("[frmMain_Load] ", ex.Message))
        End Try
    End Sub
#End Region

#Region "Function"
    Private Sub RunRequest()
        Dim mv_ip As String = ""
        bln_flag = True
        Using rep As New AttendaceBusiness.AttendanceBusinessClient
            mv_dtListTerminal = rep.GetTerminalAuto

            If mv_dtListTerminal IsNot Nothing AndAlso mv_dtListTerminal.Rows.Count > 0 Then
                Try
                    For i As Integer = 0 To mv_dtListTerminal.Rows.Count - 1
                        TS_AddLogText("----------------------------------------------------------------------------")
                        TS_AddLogText(String.Format("[RunRequest] Begin read data from {0} SDK for times: {1}", mv_dtListTerminal.Rows.Count, i + 1))
                        Dim row As DataRow = mv_dtListTerminal.Rows(i)
                        Dim mv_id As String = row("ID").ToString.Trim
                        mv_ip = row("TERMINAL_IP").ToString.Trim
                        Dim mv_port As String = row("PORT").ToString.Trim
                        Dim mv_pass As Decimal?
                        If row("PASS").ToString.Trim <> "" Then
                            mv_pass = Decimal.Parse(row("PASS"))
                        End If
                        Dim mv_LTime As Date?
                        If row("LAST_TIME_UPDATE").ToString <> "" Then
                            mv_LTime = row("LAST_TIME_UPDATE")
                            mv_LTime = mv_LTime.Value.AddMonths(-6)
                            mv_LTime = New Date(mv_LTime.Value.Year, mv_LTime.Value.Month, 1)
                        End If
                        ReadCheckInOutData(mv_id, mv_ip, mv_port, 1, mv_pass, mv_LTime)
                        TS_AddLogText(String.Format("[RunRequest] End read data from {0} SDK for times: {1}", mv_dtListTerminal.Rows.Count, i + 1))
                        'TS_AddLogText("----------------------------------------------------------------------------" & vbNewLine)
                    Next
                Catch ex As Exception
                    TS_AddLogText(String.Format("{0}: Can not connect or read data from machine.", mv_ip))
                End Try
            End If

            bln_flag = False

        End Using
    End Sub

    Private Sub LoadConfig()
        'Load cấu hình từ file Config.xml control
        '-----------------------------------------------------
        Dim dsConfig As New DataSet
        Dim filename As String = ""
        Try
            If IO.File.Exists(g_strAppConfig_MCC) Then
                dsConfig = LoadXMLFile(g_strAppConfig_MCC)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        g_intTime1From = "" & .Rows(0)("Time1From")
                        g_intTime1To = "" & .Rows(0)("Time1To")
                        g_intTime2From = "" & .Rows(0)("Time2From")
                        g_intTime2To = "" & .Rows(0)("Time2To")
                        g_intInterval = CInt("" & .Rows(0)("Interval"))
                    End With

                    timReading.Interval = g_intInterval * 60000
                End If
            End If

        Catch ex As Exception
            TS_AddLogText(String.Format("Error 003:  Execute LoadConfig Fail" & vbNewLine & "{0}", ex.StackTrace))
        End Try
    End Sub

    Private Sub TimerStart(Optional isLoadText As Boolean = True)
        timReading.Enabled = True
        timReading.Start()
        cmdStart.Visible = False
        cmdStop.Visible = True

        notifyIcon.Icon = New Icon(Application.StartupPath & "\img\app_start.ico")
        'Me.Icon = New Icon("app_start.ico")
        If isLoadText Then
            TS_AddLogText(String.Format("[{0}] START Successfull", Me.Text))
        End If
    End Sub

    Private Sub TimerStop()
        timReading.Enabled = False
        timReading.Stop()
        cmdStart.Visible = True
        cmdStop.Visible = False

        notifyIcon.Icon = New Icon(Application.StartupPath & "\img\app_stop.ico")
        GC.Collect()
        GC.SuppressFinalize(Me)

        TS_AddLogText(String.Format("[{0}] STOP Successfull", Me.Text))
    End Sub

    Private Sub AppExit()
        CloseAllowed = True
        notifyIcon.Dispose()
        Application.Exit()
    End Sub

    Private Sub ReadFromSDK(v_id As Decimal, v_ip As String,
                            v_port As String, v_MachineNumber As String,
                            v_pass As Decimal?, lastime As Date?,
                            ByRef ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO),
                            ByRef rowNumber As Integer?)
        Dim sv_ErrorNo As Integer = 0
        Dim sv_ID As Integer = 0
        Dim sv_verifyStatus As Integer
        Dim sv_inOutStatus As Integer
        Dim sv_year As Integer
        Dim sv_month As Integer
        Dim sv_day As Integer
        Dim sv_hour As Integer
        Dim sv_minute As Integer
        Dim sv_second As Integer
        Dim sv_dworkcode As Integer
        Dim TimeStr As Date

        Dim mv_MachineNumber As Integer = 1
        Dim sv_Serial_Number As String = String.Empty
        Dim int_i As Decimal
        Dim dwEnrollNumber As String = ""
        Try
            gs_InitializeSDK()
            If v_ip.Trim <> String.Empty Then
                'TS_AddLogText(String.Format("[ReadFromSDK] Beginning connect to machine with IP: {0} at {1} ...", v_ip, DateTime.Now))
                If v_pass IsNot Nothing Then
                    gv_SDK.SetCommPassword(v_pass)
                End If
                If gv_SDK.Connect_Net(v_ip.Trim, v_port) Then
                    'gv_SDK.EnableDevice(mv_MachineNumber, True)
                    'TS_AddLogText(String.Format("[ReadFromSDK] Connect sucessfully to machine with IP: {0} at {1}", v_ip, DateTime.Now))
                    'gv_SDK.GetSerialNumber(1, sv_Serial_Number)
                    TS_AddLogText(String.Format("[ReadFromSDK] Starting to read SDK data from machine with IP: {0} at {1} ...", v_ip, DateTime.Now))

                    gv_SDK.EnableDevice(mv_MachineNumber, False)
                    gv_SDK.GetDeviceStatus(mv_MachineNumber, 6, rowNumber)
                    If ls_AT_SWIPE_DATADTO Is Nothing Then
                        ls_AT_SWIPE_DATADTO = New List(Of AT_SWIPE_DATADTO)
                    End If
                    'Dim iValue As Integer
                    'gv_SDK.GetDeviceStatus(mv_MachineNumber, 6, iValue)
                    If gv_SDK.ReadGeneralLogData(mv_MachineNumber) Then
                        gv_SDK.ReadMark = True
                        gv_SDK.GetLastError(sv_ErrorNo)
                        While gv_SDK.SSR_GetGeneralLogData(mv_MachineNumber, dwEnrollNumber,
                                                       sv_verifyStatus, sv_inOutStatus,
                                                       sv_year, sv_month, sv_day, sv_hour,
                                                       sv_minute, sv_second, sv_dworkcode)
                            int_i += 1

                            TimeStr = New Date(sv_year, sv_month, sv_day, sv_hour, sv_minute, sv_second)
                            If lastime IsNot Nothing Then
                                If TimeStr.Subtract(lastime).Seconds() > 0 Then
                                    If IsNumeric(dwEnrollNumber) Then
                                        Dim objSwipe As AT_SWIPE_DATADTO = New AT_SWIPE_DATADTO
                                        objSwipe.ITIME_ID = dwEnrollNumber
                                        objSwipe.VALTIME = New Date(sv_year, sv_month, sv_day, sv_hour, sv_minute, 0)
                                        objSwipe.WORKINGDAY = objSwipe.VALTIME.Value.Date
                                        objSwipe.TERMINAL_ID = v_id
                                        ls_AT_SWIPE_DATADTO.Add(objSwipe)

                                    End If
                                End If
                            Else
                                If IsNumeric(dwEnrollNumber) Then
                                    Dim objSwipe As AT_SWIPE_DATADTO = New AT_SWIPE_DATADTO
                                    objSwipe.ITIME_ID = dwEnrollNumber
                                    objSwipe.VALTIME = New Date(sv_year, sv_month, sv_day, sv_hour, sv_minute, 0)
                                    objSwipe.WORKINGDAY = objSwipe.VALTIME.Value.Date
                                    objSwipe.TERMINAL_ID = v_id
                                    ls_AT_SWIPE_DATADTO.Add(objSwipe)
                                End If
                            End If
                        End While

                        'TS_AddLogText(String.Format("[ReadFromSDK] End read SDK data from machine with IP: {0} at {1}", v_ip, DateTime.Now))
                    End If
                    'Else
                    'TS_AddLogText(String.Format("{0}: Can not connect from machine - {1}", v_ip, DateTime.Now))
                End If
            Else
                Return
            End If
        Catch ex As Exception
            TS_AddLogText(String.Format("[{0}] Error when read data: {1}", ex.TargetSite.Name, ex.Message))
            Return
        Finally
            gv_SDK.EnableDevice(mv_MachineNumber, True)
            gv_SDK.Disconnect()
            gv_Initialize = False
        End Try
    End Sub
    Private Function mf_FormatNumber2String(ByVal number As Integer, ByVal length As Integer) As String
        Dim i As Integer = 0
        Dim fv_Str As String
        Try
            fv_Str = number.ToString
            For i = 1 To length - number.ToString.Length
                fv_Str = "0" & fv_Str
            Next
            Return fv_Str
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Get data hàng ngày (Mặc định là ngày hiện tại hoặc có thể Custom, dữ liệu IsTranfer = 0 (chưa lấy)
    ''' Gửi request lên Server để thực hiện import vào hệ thống => Import thành công toàn bộ thì mới update lại trạng thái IsTranfer = 1
    ''' Nếu thất bại 1 row data thì Rollback toàn bộ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadCheckInOutData(v_id As Decimal,
                                   v_ip As String,
                                 v_port As String,
                                 v_machineNumber As String,
                                   v_pass As Decimal?,
                                   lastime As Date?)
        Dim d As Date = Date.Now
        Try

            '1. Kiểm tra thời gian có nằm trong khoảng quy định lấy dữ liệu hay không
            If g_intTime1From <= d.Hour <= g_intTime1To Or g_intTime2From <= d.Hour <= g_intTime2To Then
                Dim isAuto As Boolean = cbAuto.Checked

                Using rep As New AttendanceBusinessClient
                    'TS_AddLogText(String.Format("{0}:{2} Last read time from machine - {1}", v_ip, lastime, v_port))
                    Dim ls_AT_SWIPE_DATADTO As List(Of AT_SWIPE_DATADTO) = Nothing
                    Dim iValue As Integer? = 0
                    ReadFromSDK(v_id, v_ip, v_port, v_machineNumber,
                                v_pass, lastime, ls_AT_SWIPE_DATADTO, iValue)
                    Dim obj As AT_TERMINALSDTO
                    If ls_AT_SWIPE_DATADTO Is Nothing Then
                        TS_AddLogText(String.Format("{0}:{1} Can not connect or read data from machine", v_ip, v_port))
                        obj = New AT_TERMINALSDTO
                        obj.ID = v_id
                        obj.MODIFIED_DATE = d
                        obj.TERMINAL_ROW = iValue
                        obj.TERMINAL_STATUS = "FAIL"
                        rep.UpdateTerminalStatus(obj)
                        Return
                    ElseIf ls_AT_SWIPE_DATADTO.Count = 0 Then
                        TS_AddLogText(String.Format("{0}:{1} No new data found from machine", v_ip, v_port))
                        obj = New AT_TERMINALSDTO
                        obj.ID = v_id
                        obj.MODIFIED_DATE = d
                        obj.TERMINAL_ROW = iValue
                        obj.TERMINAL_STATUS = "OK"
                        rep.UpdateTerminalStatus(obj)
                        Return
                    Else
                        obj = New AT_TERMINALSDTO
                        obj.ID = v_id
                        obj.MODIFIED_DATE = d
                        obj.TERMINAL_ROW = iValue
                        obj.TERMINAL_STATUS = "OK"
                        rep.UpdateTerminalStatus(obj)
                    End If


                    lastime = d
                    Dim count As Integer = ls_AT_SWIPE_DATADTO.Count

                    'TS_AddLogText(String.Format("[ReadCheckInOutData] Data Check In Out: {0} record(s)", count))
                    'TS_AddLogText(String.Format("[ReadCheckInOutData] End Get Data From SDK (Machine: {0}) ", v_ip.ToString()))

                    '4. Nếu có dữ liệu Check In Out chưa chuyển thì mới request lên server để import vào Oracle
                    If count > 0 Then
                        'TS_AddLogText(String.Format("[ReadCheckInOutData] Begin Insert data from SDK to TAS Service (Machine: {0}) ", v_ip.ToString()))
                        '-----------------

                        '5. Nếu import thành công toàn bộ thì mới Update lại trạng thái cho dữ liệu ở SQL
                        'Hàm trên Host đã thực hiện rollback nếu dữ liệu import không thành công toàn bộ

                        ' số bản ghi cập nhật
                        Dim iSoBanGhi As Integer = 1000
                        Dim iSoBanGhiDaCapNhat As Integer = 0
                        ' số dư để làm tròn
                        Dim iSoDu As Integer = ls_AT_SWIPE_DATADTO.Count Mod iSoBanGhi
                        Dim iTongVongLap As Integer
                        ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                        If iSoDu = 0 Then
                            iTongVongLap = (ls_AT_SWIPE_DATADTO.Count - iSoDu) / iSoBanGhi
                        Else
                            iTongVongLap = ((ls_AT_SWIPE_DATADTO.Count - iSoDu) / iSoBanGhi) + 1
                        End If

                        Dim isOk As Boolean = True
                        For item As Integer = 0 To iTongVongLap - 1
                            ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                            Dim lstUpdate As New List(Of AT_SWIPE_DATADTO)

                            If item <> iTongVongLap - 1 Then
                                For idx As Integer = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                    lstUpdate.Add(ls_AT_SWIPE_DATADTO(idx))
                                Next
                            Else
                                For idx As Integer = item * iSoBanGhi To ls_AT_SWIPE_DATADTO.Count - 1
                                    lstUpdate.Add(ls_AT_SWIPE_DATADTO(idx))
                                Next
                            End If
                            ' cập nhật bản ghi
                            If Not rep.ImportSwipeDataAuto(lstUpdate) Then
                                isOk = False
                            Else
                                iSoBanGhiDaCapNhat += lstUpdate.Count
                                'TS_AddLogText(String.Format("[ReadCheckInOutData] Update data from SDK to TAS Service (Machine: {0}): {1}/{2} ", v_ip.ToString(), iSoBanGhiDaCapNhat, ls_AT_SWIPE_DATADTO.Count))

                            End If
                        Next
                        If isOk Then
                            TS_AddLogText(String.Format("{0}:{2} Read data from SDK {1} record(s)", v_ip, count, v_port))
                            'TS_AddLogText(String.Format("[ReadCheckInOutData] Import SDK data to TAS Service successfully from machine with IP: {0}", v_ip))
                            obj = New AT_TERMINALSDTO
                            obj.ID = v_id
                            obj.MODIFIED_DATE = lastime
                            rep.UpdateTerminalLastTime(obj)
                            'TS_AddLogText(String.Format("[ReadCheckInOutData] Update last time data from machine with IP: {0} is: {1}", v_ip, lastime))
                        Else
                            TS_AddLogText(String.Format("{0}:{1} Error when connect to Oracle Server", v_ip, v_port))

                        End If

                        'If rep.ImportSwipeDataAuto(ls_AT_SWIPE_DATADTO) Then
                        '    TS_AddLogText(String.Format("[ReadCheckInOutData] End insert data from SDK to TAS Service (Machine: {0}) ", v_ip.ToString()))
                        '    TS_AddLogText(String.Format("[ReadCheckInOutData] Import SDK data to TAS Service successfully from machine with IP: {0}", v_ip))
                        '    Dim obj As New AT_TERMINALSDTO
                        '    obj.ID = v_id
                        '    obj.MODIFIED_DATE = lastime
                        '    rep.UpdateTerminalLastTime(obj)
                        '    TS_AddLogText(String.Format("[ReadCheckInOutData] Update last time data from machine with IP: {0} is: {1}", v_ip, lastime))
                        'Else
                        '    TS_AddLogText(String.Format("[ReadCheckInOutData] Error when connect to Oracle Server. Please check server connection string or contact server administrator!"))
                        'End If
                    End If
                End Using
            End If
        Catch ex As Exception
            TS_AddLogText(String.Format("Error when connect to Oracle Server. Please check server connection string or contact server administrator!"))
        End Try
    End Sub

#End Region

End Class