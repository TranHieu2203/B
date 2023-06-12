Partial Public Class frmMain

#Region "Members"
    Private notifyIcon As NotifyIcon
    'Private contextMenu As ContextMenu
    Private menuItemOpen As MenuItem
    Private menuItemExit As MenuItem
    Private menuItemStart As MenuItem
    Private menuItemStop As MenuItem
    Private CloseAllowed As Boolean
#End Region

#Region "NotifyIcon"
    Private Sub InitNotifyIcon()
        Me.components = New System.ComponentModel.Container()
        Me.contextMenu = New System.Windows.Forms.ContextMenu()
        Me.menuItemStart = New System.Windows.Forms.MenuItem()
        Me.menuItemStop = New System.Windows.Forms.MenuItem()
        Me.menuItemOpen = New System.Windows.Forms.MenuItem()
        Me.menuItemExit = New System.Windows.Forms.MenuItem()

        ' Initialize contextMenu 
        Me.contextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItemStart, Me.menuItemStop, Me.menuItemOpen, Me.menuItemExit})

        ' Initialize menuItem
        Me.menuItemStart.Index = 0
        Me.menuItemStart.Text = "Start"
        AddHandler Me.menuItemStart.Click, AddressOf menuItemStart_Click

        Me.menuItemStop.Index = 1
        Me.menuItemStop.Text = "Stop"
        AddHandler Me.menuItemStop.Click, AddressOf menuItemStop_Click

        Me.contextMenu.MenuItems.Add("-")

        Me.menuItemOpen.Index = 3
        Me.menuItemOpen.Text = "Open"
        AddHandler Me.menuItemOpen.Click, AddressOf menuItemOpen_Click

        Me.menuItemExit.Index = 4
        Me.menuItemExit.Text = "Exit"
        AddHandler Me.menuItemExit.Click, AddressOf menuItemExit_Click

        ' Set up how the form should be displayed. 
        'Me.ClientSize = New System.Drawing.Size(292, 266)

        ' Create the NotifyIcon. 
        Me.notifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)

        ' The Icon property sets the icon that will appear 
        ' in the systray for this application.
        notifyIcon.Icon = New Icon("img\app_stop.ico")

        ' The ContextMenu property sets the menu that will 
        ' appear when the systray icon is right clicked.
        notifyIcon.ContextMenu = Me.contextMenu

        ' The Text property sets the text that will be displayed, 
        ' in a tooltip, when the mouse hovers over the systray icon.
        notifyIcon.Text = "TAS.Services"
        notifyIcon.Visible = True

        ' Handle the DoubleClick event to activate the form.
        AddHandler Me.notifyIcon.DoubleClick, AddressOf notifyIcon_DoubleClick

        If cmdStart.Visible = True Then
            notifyIcon.Text = "TAS.Services: Read CC data is working..."
            notifyIcon.BalloonTipText = "TAS.Services: Read CC data is working..."
        Else
            notifyIcon.Text = "TAS.Services: Read CC data has stopped working"
            notifyIcon.BalloonTipText = "TAS.Services: Read CC data has stopped working"
        End If

        notifyIcon.BalloonTipTitle = g_strTitleMsgBox

    End Sub
    Private Sub menuItemStart_Click(sender As Object, e As EventArgs)
        TimerStart()
    End Sub
    Private Sub menuItemStop_Click(sender As Object, e As EventArgs)
        TimerStop()
    End Sub
    Private Sub menuItemOpen_Click(sender As Object, e As EventArgs)
        ' Show the form when the user double clicks on the notify icon. 

        ' Set the WindowState to normal if the form is minimized. 
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Maximized
        End If

        ' Activate the form. 
        Me.Activate()

        Me.Show()
    End Sub
    Private Sub menuItemExit_Click(sender As Object, e As EventArgs)
        AppExit()
    End Sub
    Private Sub notifyIcon_DoubleClick(Sender As Object, e As EventArgs)
        ' Show the form when the user double clicks on the notify icon. 

        ' Set the WindowState to normal if the form is minimized. 
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Maximized
        End If

        ' Activate the form. 
        Me.Activate()

        Me.Show()
    End Sub
#End Region

#Region "Funtion Write Log"
    ''' <summary>
    ''' Because the thread that is triggering the event, and will
    ''' finaly end up doing your code is not created on our GUI-thread
    ''' so we need to create a delegate and invoke the method.
    ''' </summary>
    ''' <param name="text">string; What text to add to txtAcitivty</param>
    Private Delegate Sub AddLogText(text As String)
    Private Sub TS_AddLogText(text As String)

        If Me.InvokeRequired Then
            Dim del As New AddLogText(AddressOf TS_AddLogText)
            Invoke(del, text)
        Else
            text = (Convert.ToString(System.DateTime.Now + " : ") & text) + Environment.NewLine
            txtActivity.Text += text

            txtActivity.SelectionStart = txtActivity.TextLength
            txtActivity.ScrollToCaret()

            WriteLog.WriteAllText(g_strFolderLog, g_strLogFile, text)
            'CLEAR LOG TEXT BOX
            If txtActivity.TextLength >= 30000 Then
                txtActivity.ResetText()

            End If
        End If

    End Sub
#End Region

End Class
