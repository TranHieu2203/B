<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdMeal = New System.Windows.Forms.Button()
        Me.cmdSDK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdMeal
        '
        Me.cmdMeal.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMeal.Location = New System.Drawing.Point(295, 83)
        Me.cmdMeal.Name = "cmdMeal"
        Me.cmdMeal.Size = New System.Drawing.Size(277, 120)
        Me.cmdMeal.TabIndex = 0
        Me.cmdMeal.Text = "Histaff Meal Services"
        Me.cmdMeal.UseVisualStyleBackColor = True
        '
        'cmdSDK
        '
        Me.cmdSDK.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSDK.Location = New System.Drawing.Point(572, 84)
        Me.cmdSDK.Name = "cmdSDK"
        Me.cmdSDK.Size = New System.Drawing.Size(277, 120)
        Me.cmdSDK.TabIndex = 1
        Me.cmdSDK.Text = "Histaff SDK Services"
        Me.cmdSDK.UseVisualStyleBackColor = True
        '
        'fMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 325)
        Me.Controls.Add(Me.cmdSDK)
        Me.Controls.Add(Me.cmdMeal)
        Me.MaximizeBox = False
        Me.Name = "fMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Histaff Services"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdMeal As System.Windows.Forms.Button
    Friend WithEvents cmdSDK As System.Windows.Forms.Button
End Class
