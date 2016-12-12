<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class oDataField
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
        Me.top = New System.Windows.Forms.Panel
        Me.Bottom = New System.Windows.Forms.Panel
        Me.Right = New System.Windows.Forms.Panel
        Me.Left = New System.Windows.Forms.Panel
        Me.MainPanel = New System.Windows.Forms.Panel
        Me.MiddleSpace = New System.Windows.Forms.Panel
        Me.pnl_Right = New System.Windows.Forms.Panel
        Me.FieldText = New System.Windows.Forms.Label
        Me.pnl_Left = New System.Windows.Forms.Panel
        Me.FieldSelector = New System.Windows.Forms.LinkLabel
        Me.FieldEdit = New System.Windows.Forms.Label
        Me.MainPanel.SuspendLayout()
        Me.pnl_Right.SuspendLayout()
        Me.pnl_Left.SuspendLayout()
        Me.SuspendLayout()
        '
        'top
        '
        Me.top.Dock = System.Windows.Forms.DockStyle.Top
        Me.top.Location = New System.Drawing.Point(0, 0)
        Me.top.Name = "top"
        Me.top.Size = New System.Drawing.Size(332, 1)
        '
        'Bottom
        '
        Me.Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bottom.Location = New System.Drawing.Point(0, 20)
        Me.Bottom.Name = "Bottom"
        Me.Bottom.Size = New System.Drawing.Size(332, 1)
        '
        'Right
        '
        Me.Right.Dock = System.Windows.Forms.DockStyle.Right
        Me.Right.Location = New System.Drawing.Point(331, 1)
        Me.Right.Name = "Right"
        Me.Right.Size = New System.Drawing.Size(1, 19)
        '
        'Left
        '
        Me.Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.Left.Location = New System.Drawing.Point(0, 1)
        Me.Left.Name = "Left"
        Me.Left.Size = New System.Drawing.Size(1, 19)
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.MiddleSpace)
        Me.MainPanel.Controls.Add(Me.pnl_Right)
        Me.MainPanel.Controls.Add(Me.pnl_Left)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(332, 21)
        '
        'MiddleSpace
        '
        Me.MiddleSpace.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MiddleSpace.Location = New System.Drawing.Point(90, 0)
        Me.MiddleSpace.Name = "MiddleSpace"
        Me.MiddleSpace.Size = New System.Drawing.Size(11, 21)
        '
        'pnl_Right
        '
        Me.pnl_Right.Controls.Add(Me.FieldEdit)
        Me.pnl_Right.Controls.Add(Me.FieldText)
        Me.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_Right.Location = New System.Drawing.Point(101, 0)
        Me.pnl_Right.Name = "pnl_Right"
        Me.pnl_Right.Size = New System.Drawing.Size(231, 21)
        '
        'FieldText
        '
        Me.FieldText.BackColor = System.Drawing.Color.White
        Me.FieldText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FieldText.Location = New System.Drawing.Point(0, 0)
        Me.FieldText.Name = "FieldText"
        Me.FieldText.Size = New System.Drawing.Size(231, 21)
        '
        'pnl_Left
        '
        Me.pnl_Left.Controls.Add(Me.FieldSelector)
        Me.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_Left.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Left.Name = "pnl_Left"
        Me.pnl_Left.Size = New System.Drawing.Size(90, 21)
        '
        'FieldSelector
        '
        Me.FieldSelector.Location = New System.Drawing.Point(4, 0)
        Me.FieldSelector.Name = "FieldSelector"
        Me.FieldSelector.Size = New System.Drawing.Size(86, 21)
        Me.FieldSelector.TabIndex = 1
        Me.FieldSelector.Text = "LinkLabel1"
        Me.FieldSelector.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FieldEdit
        '
        Me.FieldEdit.BackColor = System.Drawing.Color.Red
        Me.FieldEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FieldEdit.Location = New System.Drawing.Point(0, 0)
        Me.FieldEdit.Name = "FieldEdit"
        Me.FieldEdit.Size = New System.Drawing.Size(231, 21)
        Me.FieldEdit.Visible = False
        '
        'oDataField
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.Left)
        Me.Controls.Add(Me.Right)
        Me.Controls.Add(Me.Bottom)
        Me.Controls.Add(Me.top)
        Me.Controls.Add(Me.MainPanel)
        Me.Name = "oDataField"
        Me.Size = New System.Drawing.Size(332, 21)
        Me.MainPanel.ResumeLayout(False)
        Me.pnl_Right.ResumeLayout(False)
        Me.pnl_Left.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents top As System.Windows.Forms.Panel
    Friend WithEvents Bottom As System.Windows.Forms.Panel
    Friend WithEvents Right As System.Windows.Forms.Panel
    Friend WithEvents Left As System.Windows.Forms.Panel
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents MiddleSpace As System.Windows.Forms.Panel
    Friend WithEvents pnl_Right As System.Windows.Forms.Panel
    Friend WithEvents FieldText As System.Windows.Forms.Label
    Friend WithEvents pnl_Left As System.Windows.Forms.Panel
    Friend WithEvents FieldSelector As System.Windows.Forms.LinkLabel
    Friend WithEvents FieldEdit As System.Windows.Forms.Label


End Class
