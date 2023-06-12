'Create by Truongnn
'Create on 12/12/2007
'Purpose: save Parameters to XML
Public Class frmConfig
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTime1From As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTime2From As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTime1To As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTime2To As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
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
    Friend WithEvents txtDNSName As System.Windows.Forms.TextBox
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig))
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTime1To = New System.Windows.Forms.NumericUpDown()
        Me.txtTime2To = New System.Windows.Forms.NumericUpDown()
        Me.txtInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTime1From = New System.Windows.Forms.NumericUpDown()
        Me.txtTime2From = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDNSName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtTime1To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime2To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime1From, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime2From, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtStatus
        '
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(80, 116)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(222, 22)
        Me.txtStatus.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Status"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtTime1To)
        Me.GroupBox2.Controls.Add(Me.txtTime2To)
        Me.GroupBox2.Controls.Add(Me.txtInterval)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtTime1From)
        Me.GroupBox2.Controls.Add(Me.txtTime2From)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(421, 97)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General Info"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(166, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(23, 16)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "To"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(166, 25)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 16)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "To"
        '
        'txtTime1To
        '
        Me.txtTime1To.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime1To.Location = New System.Drawing.Point(192, 22)
        Me.txtTime1To.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.txtTime1To.Name = "txtTime1To"
        Me.txtTime1To.Size = New System.Drawing.Size(49, 22)
        Me.txtTime1To.TabIndex = 10
        '
        'txtTime2To
        '
        Me.txtTime2To.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime2To.Location = New System.Drawing.Point(192, 45)
        Me.txtTime2To.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.txtTime2To.Name = "txtTime2To"
        Me.txtTime2To.Size = New System.Drawing.Size(49, 22)
        Me.txtTime2To.TabIndex = 12
        '
        'txtInterval
        '
        Me.txtInterval.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInterval.Location = New System.Drawing.Point(108, 68)
        Me.txtInterval.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.txtInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(136, 22)
        Me.txtInterval.TabIndex = 13
        Me.txtInterval.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(247, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 16)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Hour"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(247, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 16)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Hour"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 16)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Run Job Time 2"
        '
        'txtTime1From
        '
        Me.txtTime1From.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime1From.Location = New System.Drawing.Point(108, 22)
        Me.txtTime1From.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.txtTime1From.Name = "txtTime1From"
        Me.txtTime1From.Size = New System.Drawing.Size(49, 22)
        Me.txtTime1From.TabIndex = 9
        '
        'txtTime2From
        '
        Me.txtTime2From.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime2From.Location = New System.Drawing.Point(108, 45)
        Me.txtTime2From.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.txtTime2From.Name = "txtTime2From"
        Me.txtTime2From.Size = New System.Drawing.Size(49, 22)
        Me.txtTime2From.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(250, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Minute(s)"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Run Job Time 1"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Interval"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDatabase)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmdTest)
        Me.GroupBox1.Controls.Add(Me.cmdClose)
        Me.GroupBox1.Controls.Add(Me.cmdSave)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtDNSName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtUsername)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 150)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "DataBase Connection"
        '
        'txtDatabase
        '
        Me.txtDatabase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabase.Location = New System.Drawing.Point(80, 47)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(222, 22)
        Me.txtDatabase.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 16)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Database"
        '
        'cmdTest
        '
        Me.cmdTest.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdTest.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTest.Location = New System.Drawing.Point(320, 53)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(88, 24)
        Me.cmdTest.TabIndex = 7
        Me.cmdTest.Text = "Test Connect"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(320, 80)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(88, 24)
        Me.cmdClose.TabIndex = 8
        Me.cmdClose.Text = "Close"
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(320, 24)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(88, 24)
        Me.cmdSave.TabIndex = 6
        Me.cmdSave.Text = "Save"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Password"
        '
        'txtDNSName
        '
        Me.txtDNSName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDNSName.Location = New System.Drawing.Point(80, 24)
        Me.txtDNSName.Name = "txtDNSName"
        Me.txtDNSName.Size = New System.Drawing.Size(222, 22)
        Me.txtDNSName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Server"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "User Name"
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(80, 70)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(222, 22)
        Me.txtUsername.TabIndex = 3
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(80, 93)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(222, 22)
        Me.txtPassword.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.61776!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.38224!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(427, 259)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'frmConfig
        '
        Me.ClientSize = New System.Drawing.Size(427, 259)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.txtTime1To, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTime2To, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTime1From, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTime2From, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Member"
    Private m_OK As Boolean 'Trạng thái kết nối server
#End Region

#Region "Function"
    Private Sub LoadConfig()
        'Load cấu hình từ file Config.xml lên combo box
        '-----------------------------------------------------
        Dim dsConfig As New DataSet
        Dim strMsg As String
        Try
            If IO.File.Exists(g_strAppConfig) Then
                dsConfig = LoadXMLFile(g_strAppConfig)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        txtStatus.Text = "" & .Rows(0)("Status")
                        txtDNSName.Text = "" & .Rows(0)("DNSName")
                        txtUsername.Text = "" & .Rows(0)("Username")
                        txtPassword.Text = "" & .Rows(0)("Password")
                        txtDatabase.Text = "" & .Rows(0)("DatabaseName")
                        txtTime1From.Value = CInt("" & .Rows(0)("Time1From"))
                        txtTime1To.Value = CInt("" & .Rows(0)("Time1To"))
                        txtTime2From.Value = CInt("" & .Rows(0)("Time2From"))
                        txtTime2To.Value = CInt("" & .Rows(0)("Time2To"))
                        txtInterval.Value = CInt("" & .Rows(0)("Interval"))
                        g_ConnString = .Rows(0)("ConnString")
                        UpdateConfig()
                    End With
                End If
            Else
                strMsg = "File " & g_strAppConfig & " not found"
                MessageBox.Show(strMsg, g_strTitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            strMsg = "Error description: " & ex.Message
            MessageBox.Show(strMsg, g_strTitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateConfig()
        g_strStatus = txtStatus.Text
        g_strDNSName = txtDNSName.Text
        g_strUserName = txtUsername.Text
        g_strPassword = txtPassword.Text
        g_strDatabase = txtDatabase.Text
        g_intTime1From = txtTime1From.Value
        g_intTime1To = txtTime1To.Value
        g_intTime2From = txtTime2From.Value
        g_intTime2To = txtTime2To.Value
        g_intInterval = txtInterval.Value
        txtStatus.Text = g_strStatus
    End Sub
    Private Function SaveConfig() As Boolean
        Dim dsConfig As New DataSet
        Try
            Dim PathFile As String = g_strAppConfig
            If IO.File.Exists(PathFile) Then
                dsConfig = LoadXMLFile(PathFile)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        If m_OK Then
                            .Rows(0)("Status") = Trim(CStr("OK"))
                        Else
                            .Rows(0)("Status") = Trim(CStr("NOTOK"))
                        End If
                        .Rows(0)("DNSName") = Trim(CStr(txtDNSName.Text))
                        .Rows(0)("Username") = Trim(CStr(txtUsername.Text))
                        .Rows(0)("Password") = Trim(CStr(txtPassword.Text))
                        .Rows(0)("DatabaseName") = Trim(CStr(txtDatabase.Text))
                        .Rows(0)("Time1From") = IIf(CInt(txtTime1From.Text) <= 0, 0, CInt(txtTime1From.Text)) 'Default bang 0
                        .Rows(0)("Time1To") = IIf(CInt(txtTime1To.Text) <= 2, 0, CInt(txtTime1To.Text)) 'Default bang 2
                        .Rows(0)("Time2From") = IIf(CInt(txtTime2From.Text) <= 0, 13, CInt(txtTime2From.Text)) 'Default bang 13
                        .Rows(0)("Time2To") = IIf(CInt(txtTime2To.Text) <= 0, 15, CInt(txtTime2To.Text)) 'Default bang 15
                        .Rows(0)("Interval") = IIf(CInt(txtInterval.Text) <= 0, 15, CInt(txtInterval.Text))

                        txtTime1From.Text = .Rows(0)("Time1From")
                        txtTime1To.Text = .Rows(0)("Time1To")
                        txtTime2From.Text = .Rows(0)("Time2From")
                        txtTime2To.Text = .Rows(0)("Time2To")
                        txtInterval.Text = .Rows(0)("Interval")
                    End With
                    SaveXMLFile(dsConfig, PathFile)
                    UpdateConfig()
                    MessageBox.Show("Save successfully!", g_strTitleMsgBox, MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("File Config.xml not found in " & Application.StartupPath, g_strTitleMsgBox, MessageBoxButtons.OK)
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Save configuration failed" & vbCrLf & " Error description: " & ex.Message, g_strTitleMsgBox, MessageBoxButtons.OK)
            Return False
        End Try

    End Function
    Private Function Connect() As Boolean
        Try
            Dim strconn As String = String.Format(g_ConnString, Trim(txtDNSName.Text), Trim(txtDatabase.Text), Trim(txtUsername.Text), Trim(txtPassword.Text))
            If g_dbOra Is Nothing Then g_dbOra = New OleDb.OleDbConnection
            g_dbOra.ConnectionString = strconn
            g_dbOra.Open()

            Return True
        Catch ex As Exception
            WriteEventsLog("Connect DB: " & ex.Message)
            Return False
        End Try

    End Function
    Private Function DisConnect() As Boolean
        Try
            If g_dbOra Is Nothing Then g_dbOra = New OleDb.OleDbConnection
            If g_dbOra.State = ConnectionState.Open Then g_dbOra.Close()
            Return True
        Catch ex As Exception
            WriteEventsLog("Disconnect DB: " & ex.Message)
            Return False
        End Try

    End Function
    Private Sub TextChange()
        'cmdTest.Enabled = True
        'cmdSave.Enabled = False
    End Sub
    Private Sub TextNoChange()
        'cmdTest.Enabled = False
        'cmdSave.Enabled = False
    End Sub
#End Region

#Region "Form Event"
    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadConfig()
        TextNoChange()
    End Sub

    Private Sub cmdReading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        SaveConfig()
        TextNoChange()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        If Connect() Then
            MsgBox("Connect to Database " & txtDNSName.Text & " is OK")
            m_OK = True
            DisConnect()
            txtStatus.Text = Trim(CStr("OK"))
        Else
            MsgBox("Can not connect to Database " & txtDNSName.Text, MsgBoxStyle.Critical)
            m_OK = False
            txtStatus.Text = Trim(CStr("NOTOK"))
        End If
        cmdSave.Enabled = True
    End Sub

    Private Sub frmConfig_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDNSName.TextChanged, txtDatabase.TextChanged, txtUsername.TextChanged, txtPassword.TextChanged, txtTime1From.ValueChanged, txtTime1From.ValueChanged, txtInterval.ValueChanged, txtTime2From.ValueChanged
        TextChange()
    End Sub
#End Region
End Class
