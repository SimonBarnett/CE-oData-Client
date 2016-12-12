<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ChildButtons
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnSelect = New System.Windows.Forms.HScrollBar
        Me.pnlButtons = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelect.LargeChange = 1
        Me.btnSelect.Location = New System.Drawing.Point(117, 0)
        Me.btnSelect.Maximum = 1
        Me.btnSelect.Minimum = -1
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(33, 25)
        Me.btnSelect.TabIndex = 0
        '
        'pnlButtons
        '
        Me.pnlButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(542, 25)
        '
        'NavButtons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.pnlButtons)
        Me.Name = "NavButtons"
        Me.Size = New System.Drawing.Size(150, 25)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSelect As System.Windows.Forms.HScrollBar
    Public WithEvents pnlButtons As System.Windows.Forms.Panel

End Class
