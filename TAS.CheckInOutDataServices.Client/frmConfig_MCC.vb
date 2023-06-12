'Create by Truongnn
'Create on 12/12/2007
'Purpose: save Parameters to XML

Public Class frmConfig_MCC
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTime1From As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTime2From As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTime1To As System.Windows.Forms.NumericUpDown
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents gridMachine As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtTime2To As System.Windows.Forms.NumericUpDown
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig_MCC))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdSave = New System.Windows.Forms.Button()
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
        Me.gridMachine = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtTime1To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime2To, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime1From, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime2From, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gridMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdSave)
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
        Me.GroupBox2.Location = New System.Drawing.Point(3, 213)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(615, 132)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Thông tin chung"
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(108, 96)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(88, 24)
        Me.cmdSave.TabIndex = 23
        Me.cmdSave.Text = "Save"
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
        Me.txtInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(133, 22)
        Me.txtInterval.TabIndex = 13
        Me.txtInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
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
        Me.GroupBox1.Controls.Add(Me.gridMachine)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(615, 204)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Danh sách máy chấm công"
        '
        'gridMachine
        '
        Me.gridMachine.AllowUserToAddRows = False
        Me.gridMachine.AllowUserToDeleteRows = False
        Me.gridMachine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridMachine.Location = New System.Drawing.Point(9, 21)
        Me.gridMachine.MultiSelect = False
        Me.gridMachine.Name = "gridMachine"
        Me.gridMachine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridMachine.Size = New System.Drawing.Size(597, 160)
        Me.gridMachine.TabIndex = 9
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(621, 348)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'frmConfig_MCC
        '
        Me.ClientSize = New System.Drawing.Size(621, 348)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig_MCC"
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
        CType(Me.gridMachine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
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
            If IO.File.Exists(g_strAppConfig_MCC) Then
                dsConfig = LoadXMLFile(g_strAppConfig_MCC)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        txtTime1From.Value = CInt("" & .Rows(0)("Time1From"))
                        txtTime1To.Value = CInt("" & .Rows(0)("Time1To"))
                        txtTime2From.Value = CInt("" & .Rows(0)("Time2From"))
                        txtTime2To.Value = CInt("" & .Rows(0)("Time2To"))
                        txtInterval.Value = CInt("" & .Rows(0)("Interval"))
                        UpdateConfig()
                    End With
                End If
            Else
                strMsg = "File " & g_strAppConfig_MCC & " not found"
                MessageBox.Show(strMsg, g_strTitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            strMsg = "Error description: " & ex.Message
            MessageBox.Show(strMsg, g_strTitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadConfig_List()
        'Load cấu hình từ file Config.xml lên combo box
        '-----------------------------------------------------
        Dim dtConfig As New DataTable
        Dim strMsg As String
        Try
            Using rep As New AttendaceBusiness.AttendanceBusinessClient
                dtConfig = rep.GetTerminalAuto
                dtConfig.Columns.RemoveAt(0)
                dtConfig.Columns(0).ColumnName = "Machine IP"
                dtConfig.Columns(1).ColumnName = "Port"
                dtConfig.Columns(2).ColumnName = "Pass"
                dtConfig.Columns(3).ColumnName = "Address"
                dtConfig.Columns(4).ColumnName = "Last time update"
                dtConfig.Columns(0).ReadOnly = True
                dtConfig.Columns(1).ReadOnly = True
                dtConfig.Columns(2).ReadOnly = True
                dtConfig.Columns(3).ReadOnly = True
                dtConfig.Columns(4).ReadOnly = True
                gridMachine.DataSource = dtConfig
                gridMachine.Refresh()
                UpdateConfig()
            End Using
        Catch ex As Exception
            strMsg = "Error description: " & ex.Message
            MessageBox.Show(strMsg, g_strTitleMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateConfig()
        g_intTime1From = txtTime1From.Value
        g_intTime1To = txtTime1To.Value
        g_intTime2From = txtTime2From.Value
        g_intTime2To = txtTime2To.Value
        g_intInterval = txtInterval.Value
    End Sub

    Private Function SaveConfig() As Boolean
        Dim dsConfig As New DataSet
        Try
            Dim PathFile As String = g_strAppConfig_MCC
            If IO.File.Exists(PathFile) Then
                dsConfig = LoadXMLFile(PathFile)
                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    With dsConfig.Tables(0)
                        If m_OK Then
                            .Rows(0)("Status") = Trim(CStr("OK"))
                        Else
                            .Rows(0)("Status") = Trim(CStr("NOTOK"))
                        End If


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

#End Region

#Region "Form Event"
    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadConfig_List()
        LoadConfig()
    End Sub

    Private Sub frmConfig_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub


#End Region

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        SaveConfig()
    End Sub
End Class
