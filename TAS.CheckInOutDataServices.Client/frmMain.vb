Imports TAS.CheckInOutDataServices
Imports System.Collections.Generic

Public Class frmMain
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbAuto As System.Windows.Forms.CheckBox
    Friend WithEvents txtActivity As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblPath As System.Windows.Forms.Label
    Friend WithEvents bw As System.ComponentModel.BackgroundWorker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cbAuto = New System.Windows.Forms.CheckBox()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdConfig = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.timReading = New System.Windows.Forms.Timer(Me.components)
        Me.txtActivity = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.bw = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.dtpDateFrom)
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Controls.Add(Me.cbAuto)
        Me.Panel1.Controls.Add(Me.dtpDateTo)
        Me.Panel1.Controls.Add(Me.cmdClear)
        Me.Panel1.Controls.Add(Me.cmdStart)
        Me.Panel1.Controls.Add(Me.cmdConfig)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Controls.Add(Me.cmdStop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 44)
        Me.Panel1.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(228, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "=>"
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.dtpDateFrom.CalendarFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateFrom.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(133, 14)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(94, 22)
        Me.dtpDateFrom.TabIndex = 47
        Me.dtpDateFrom.Value = New Date(2015, 8, 21, 16, 41, 33, 0)
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdExit.Location = New System.Drawing.Point(695, 12)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(80, 24)
        Me.cmdExit.TabIndex = 46
        Me.cmdExit.Text = "Exit"
        '
        'cbAuto
        '
        Me.cbAuto.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cbAuto.AutoSize = True
        Me.cbAuto.Checked = True
        Me.cbAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAuto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuto.Location = New System.Drawing.Point(52, 17)
        Me.cbAuto.Name = "cbAuto"
        Me.cbAuto.Size = New System.Drawing.Size(78, 17)
        Me.cbAuto.TabIndex = 45
        Me.cbAuto.Text = "Automatic"
        Me.cbAuto.UseVisualStyleBackColor = True
        '
        'dtpDateTo
        '
        Me.dtpDateTo.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.dtpDateTo.CalendarFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateTo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(252, 13)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(94, 22)
        Me.dtpDateTo.TabIndex = 44
        Me.dtpDateTo.Value = New Date(2015, 8, 21, 16, 41, 33, 0)
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdClear.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdClear.Location = New System.Drawing.Point(525, 12)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(80, 24)
        Me.cmdClear.TabIndex = 43
        Me.cmdClear.Text = "Clear"
        '
        'cmdStart
        '
        Me.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdStart.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdStart.Location = New System.Drawing.Point(355, 12)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(80, 24)
        Me.cmdStart.TabIndex = 41
        Me.cmdStart.Text = "Start"
        '
        'cmdConfig
        '
        Me.cmdConfig.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdConfig.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdConfig.Location = New System.Drawing.Point(440, 12)
        Me.cmdConfig.Name = "cmdConfig"
        Me.cmdConfig.Size = New System.Drawing.Size(80, 24)
        Me.cmdConfig.TabIndex = 36
        Me.cmdConfig.Text = "Config"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdClose.Location = New System.Drawing.Point(610, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 24)
        Me.cmdClose.TabIndex = 35
        Me.cmdClose.Text = "Hide"
        '
        'cmdStop
        '
        Me.cmdStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdStop.Location = New System.Drawing.Point(355, 13)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(80, 24)
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
        Me.txtActivity.Size = New System.Drawing.Size(778, 282)
        Me.txtActivity.TabIndex = 36
        Me.txtActivity.Text = ""
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtActivity, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPath, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(784, 361)
        Me.TableLayoutPanel1.TabIndex = 37
        '
        'lblPath
        '
        Me.lblPath.AutoSize = True
        Me.lblPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPath.Location = New System.Drawing.Point(3, 0)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(778, 23)
        Me.lblPath.TabIndex = 37
        '
        'bw
        '
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 15)
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TAS.Services: Đọc dữ liệu chấm cơm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Event"

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadConfig()
        InitNotifyIcon()
        CloseAllowed = False
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not CloseAllowed AndAlso e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
            notifyIcon.ShowBalloonTip(2000)
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        CloseAllowed = False
        Me.Close()
    End Sub

    Private Sub cmdConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfig.Click
        TimerStop()
        Dim frm As New frmConfig
        frm.ShowDialog()
        LoadConfig()
        TimerStart()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        txtActivity.Clear()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        TimerStart()
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        TimerStop()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        AppExit()
    End Sub

    Private Sub cbAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAuto.CheckedChanged
        If cbAuto.Checked Then
            dtpDateTo.Enabled = False
            dtpDateFrom.Enabled = False
            TS_AddLogText("Change To Get Data From SQL => Automation")
        Else
            dtpDateTo.Enabled = True
            dtpDateFrom.Enabled = True
            TS_AddLogText("Change To Get Data From SQL =>  Custom")
        End If
    End Sub

    Private Sub timReading_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timReading.Tick
        If cbAuto.Checked Then
            dtpDateFrom.Value = Date.Now
            dtpDateTo.Value = Date.Now 'là Auto thì lấy thời gian hiện tại
        End If

        If bw.IsBusy = False Then
            dtpDateTo.Enabled = False
            dtpDateFrom.Enabled = False
            bw.RunWorkerAsync()
        End If
    End Sub

    Private Sub bw_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw.DoWork
        ReadCheckInOutData()
    End Sub

    Private Sub bw_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        dtpDateTo.Enabled = True
        dtpDateFrom.Enabled = True
    End Sub
#End Region

#Region "Function"
    Private Sub LoadConfig()
        'Load cấu hình từ file Config.xml control
        '-----------------------------------------------------
        Dim dsConfig As New DataSet
        Dim filename As String = ""
        Try
            If IO.File.Exists(g_strAppConfig) Then
                dsConfig = LoadXMLFile(g_strAppConfig)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        g_strStatus = "" & .Rows(0)("Status")
                        g_strDatabase = "" & .Rows(0)("DatabaseName")
                        g_strDNSName = "" & .Rows(0)("DNSName")
                        g_strUserName = "" & .Rows(0)("Username")
                        g_strPassword = "" & .Rows(0)("Password")
                        g_intTime1From = "" & .Rows(0)("Time1From")
                        g_intTime1To = "" & .Rows(0)("Time1To")
                        g_intTime2From = "" & .Rows(0)("Time2From")
                        g_intTime2To = "" & .Rows(0)("Time2To")
                        g_intInterval = CInt("" & .Rows(0)("Interval"))
                        g_ConnString = String.Format("" & .Rows(0)("ConnString"), g_strDNSName, g_strDatabase, g_strUserName, g_strPassword)
                    End With

                    timReading.Interval = g_intInterval * 60000

                    lblPath.Text = String.Format("Current Server Name: {0} - DatabaseName: {1}", g_strDNSName, g_strDatabase)
                End If
            End If

        Catch ex As Exception
            TS_AddLogText(String.Format("Error 003:  Execute LoadConfig Fail" & vbNewLine & "{0}", ex.StackTrace))
        End Try
    End Sub

    Private Sub TimerStart()
        timReading.Enabled = True
        timReading.Start()
        cmdStart.Visible = False
        cmdStop.Visible = True

        notifyIcon.Icon = New Icon("img\app_start.ico")

        TS_AddLogText(String.Format("[{0}] START Successfull", Me.Text))
    End Sub

    Private Sub TimerStop()
        timReading.Enabled = False
        timReading.Stop()
        cmdStart.Visible = True
        cmdStop.Visible = False

        notifyIcon.Icon = New Icon("img\app_stop.ico")

        GC.Collect()
        GC.SuppressFinalize(Me)

        TS_AddLogText(String.Format("[{0}] STOP Successfull", Me.Text))
    End Sub

    Private Sub AppExit()
        CloseAllowed = True
        notifyIcon.Dispose()
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Get data hàng ngày (Mặc định là ngày hiện tại hoặc có thể Custom, dữ liệu IsTranfer = 0 (chưa lấy)
    ''' Gửi request lên Server để thực hiện import vào hệ thống => Import thành công toàn bộ thì mới update lại trạng thái IsTranfer = 1
    ''' Nếu thất bại 1 row data thì Rollback toàn bộ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadCheckInOutData()
        Dim d As Date
        d = Date.Now
        '1. Kiểm tra thời gian có nằm trong khoảng quy định lấy dữ liệu hay không
        If g_intTime1From <= d.Hour <= g_intTime1To Or g_intTime2From <= d.Hour <= g_intTime2To Then
            Dim isAuto As Boolean = cbAuto.Checked
            Dim dateFrom As Date = dtpDateFrom.Value
            Dim dateTo As Date = dtpDateTo.Value

            If isAuto Then
                TS_AddLogText(String.Format("[ReadCheckInOutData][Automation Get Data From Sql Server] From {0:dd/MM/yyyy} to {1:dd/MM/yyyy}", dateFrom, dateTo))
            Else
                TS_AddLogText(String.Format("[ReadCheckInOutData][Custom Get Data From Sql Server] From {0:dd/MM/yyyy} to {1:dd/MM/yyyy}", dateFrom, dateTo))
            End If

            '2. Load danh sach View lay du lieu
            Dim ds_ViewData As DataSet
            ds_ViewData = LoadXMLFile(g_strAppConfig_List)

            '3. Lấy câu truy vấn select data CHECKINOUT SQL
            Dim strSql As String = String.Format(GetSQLText("Query\SelectCheckInOutData.txt"), dateFrom, dateTo)
            Dim jj As Integer

            '3.1 Duyêt tung view lay du lieu
            For jj = 0 To ds_ViewData.Tables(0).Rows.Count - 1
                CheckNewData(ds_ViewData.Tables(0).Rows(jj)(0).ToString(), dateFrom, dateTo)

                TS_AddLogText(String.Format("[ReadCheckInOutData] Begin Get Data From Sql Server"))
                '4. Get data từ SQL
                Dim ds As DataSet = SQLHelper.ExecuteQuery(strSql, g_ConnString)
                Dim count As Integer = 0
                Try
                    count = ds.Tables(0).Rows.Count
                    TS_AddLogText(String.Format("[ReadCheckInOutData] Data Check In Out: {0} records", count))
                Catch ex As Exception
                    TS_AddLogText(String.Format("[ReadCheckInOutData] Error 002:  Execute ReadCheckInOutData Fail" & vbNewLine & "{0}", ex.StackTrace))
                End Try
                TS_AddLogText("[ReadCheckInOutData] End Get Data From Sql Server")

                '4. Nếu có dữ liệu Check In Out chưa chuyển thì mới request lên server để import vào Oracle
                If count > 0 Then
                    TS_AddLogText("[ReadCheckInOutData] Begin Request to TAS Service")
                    Dim j As Integer = TransferCheckInOutDataToOracle(ds)
                    TS_AddLogText("[ReadCheckInOutData] End Request to TAS Service")

                    '5. Nếu import thành công toàn bộ thì mới Update lại trạng thái cho dữ liệu ở SQL
                    If j = count Then
                        UpdateIsTransferCheckInOutData(dateFrom, dateTo)
                    Else
                        TS_AddLogText("[ReadCheckInOutData] Có lỗi xảy ra trong quá trình import dữ liệu vào Oracle. Liên hệ Admin hệ thống để kiểm tra!")
                    End If
                End If

                '6 Correct Meal ID cho du lieu moi import
                AutoCorrectMealID()

                TS_AddLogText(vbNewLine & vbNewLine)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Request lên Host WCF để import vào Oracle
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TransferCheckInOutDataToOracle(ByVal ds As DataSet) As Integer
        Dim i As Integer = 0
        Try
            Dim tas As New TASCheckInOutDataServices.TASCheckInOutDataServicesClient
            Dim strSQL As String = GetSQLText("Query\InsertOracleQuery.txt")

            TS_AddLogText("[TransferCheckInOutDataToOracle] Begin import table AT_MEAL_IO_DETAIL")
            i = tas.ImportCheckInOutData(strSQL, ds.Tables(0))

            TS_AddLogText(String.Format("[TransferCheckInOutDataToOracle] End import table AT_MEAL_IO_DETAIL: {0} record(s)", i.ToString()))

        Catch ex As Exception
            TS_AddLogText(String.Format("[TransferCheckInOutDataToOracle] Error 001:  Execute TransferCheckInOutDataToOracle Fail" & vbNewLine & "{0}", ex.StackTrace))
        End Try
        Return i
    End Function

    ''' <summary>
    ''' Hàm update trạng thái dữ liệu CHECKINOUT ở SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateIsTransferCheckInOutData(ByVal dateFrom As Date, ByVal dateTo As Date)
        Try
            '2. Lấy câu truy vấn SQL
            Dim strSql As String = String.Format(GetSQLText("Query\UpdateIsTranferInOutData.txt"), dateFrom, dateTo)
            TS_AddLogText(String.Format("[UpdateIsTransferCheckInOutData] Begin update column IsTransfer of table CHECKINOUT in sql server"))
            Dim i As Integer = SQLHelper.ExecuteNonQuery(strSql, g_ConnString)
            TS_AddLogText(String.Format("[UpdateIsTransferCheckInOutData] End update column IsTransfer of table CHECKINOUT in sql server {0} Record(s)", i))
        Catch ex As Exception
            TS_AddLogText(String.Format("[UpdateIsTransferCheckInOutData] Error 004: Execute UpdateIsTransferCheckInOutData Fail" & vbNewLine & "{0}", ex.StackTrace))
        End Try
    End Sub

    ''' <summary>
    ''' Kiểm tra dữ liệu chấm cơm có dữ liệu mới so với table CHECKINOUT hay không
    ''' </summary>
    ''' <param name="viewName">tên view được cung cấp</param>
    ''' <param name="dateFrom"></param>
    ''' <param name="dateTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckNewData(ByVal viewName As String, ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean
        Try
            '2. Lấy câu truy vấn SQL
            Dim strSql As String = String.Format(GetSQLText("Query\SQLSelectDuLieuChamCom.txt"), viewName, dateFrom, dateTo)

            TS_AddLogText(String.Format("[CheckNewData] Begin check new data from view {0} at from {1:dd/MM/yyyy} to {2:dd/MM/yyyy}", viewName, dateFrom, dateTo))
            Dim i As Integer = SQLHelper.ExecuteNonQuery(strSql, g_ConnString)
            TS_AddLogText(String.Format("[CheckNewData] End check new data from view {0} at from {1:dd/MM/yyyy} to {2:dd/MM/yyyy} => {3} new record(s)", viewName, dateFrom, dateTo, i))

            If i = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            TS_AddLogText(String.Format("[CheckNewData] Error 004: Execute CheckNewData Fail" & vbNewLine & "{0}", ex.StackTrace))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Request lên database Oracle để correct bữa ăn cho dữ liệu chấm cơm được import
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AutoCorrectMealID()
        Try
            Dim tas As New TASCheckInOutDataServices.TASCheckInOutDataServicesClient
            TS_AddLogText(String.Format("[AutoCorrectMealID] Begin auto correct meal id"))
            Dim i() As Object = tas.ExecuteStoreScalar("PKG_MEAL.UPDATE_AT_MEAL_IO_DETAIL", New Object() {0, "OUT_NUMBER"})

            Dim result As Integer = Integer.Parse(i(0).ToString())

            If result <> -1 Then
                TS_AddLogText(String.Format("[AutoCorrectMealID] End auto correct meal id {0} Record(s)", result))
            ElseIf result = -1 Then
                TS_AddLogText(String.Format("[AutoCorrectMealID] End auto correct meal id fail"))
            End If

        Catch ex As Exception
            TS_AddLogText(String.Format("[AutoCorrectMealID] Error 005: Execute AutoCorrectMealID Fail" & vbNewLine & "{0}", ex.StackTrace))
        End Try
    End Sub

    ''' <summary>
    ''' File SQL để cùng folder của Application
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetSQLText(ByVal strName As String) As String
        Dim objReader As New System.IO.StreamReader(Application.StartupPath & "\" & strName)
        Dim temp As String
        Dim strSQL As String = ""
        Dim i As Integer = 0
        Do While objReader.Peek() <> -1
            'doc tung dong
            i = i + 1
            temp = objReader.ReadToEnd()
            Try
                strSQL = temp
            Catch ex As Exception

            End Try
        Loop
        objReader.Close()
        Return strSQL
    End Function
#End Region

End Class
