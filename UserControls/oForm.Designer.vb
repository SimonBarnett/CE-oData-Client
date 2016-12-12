<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class oForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(oForm))
        Me.oQueryView = New System.Windows.Forms.Panel
        Me.pnlView = New System.Windows.Forms.Panel
        Me.ListView = New System.Windows.Forms.ListView
        Me.pnlChild = New System.Windows.Forms.Panel
        Me.pnlSibling = New System.Windows.Forms.Panel
        Me.ToolBar = New System.Windows.Forms.ToolBar
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton5 = New System.Windows.Forms.ToolBarButton
        Me.ImageList1 = New System.Windows.Forms.ImageList
        Me.oDataObjectView = New System.Windows.Forms.Panel
        Me.tabChild = New SmartDeviceProject.ChildButtons
        Me.tabSibling = New SmartDeviceProject.SiblingButtons
        Me.oQueryView.SuspendLayout()
        Me.pnlView.SuspendLayout()
        Me.pnlChild.SuspendLayout()
        Me.pnlSibling.SuspendLayout()
        Me.SuspendLayout()
        '
        'oQueryView
        '
        Me.oQueryView.Controls.Add(Me.pnlView)
        Me.oQueryView.Controls.Add(Me.pnlChild)
        Me.oQueryView.Controls.Add(Me.pnlSibling)
        Me.oQueryView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oQueryView.Location = New System.Drawing.Point(0, 0)
        Me.oQueryView.Name = "oQueryView"
        Me.oQueryView.Size = New System.Drawing.Size(237, 353)
        '
        'pnlView
        '
        Me.pnlView.Controls.Add(Me.ListView)
        Me.pnlView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlView.Location = New System.Drawing.Point(0, 25)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(237, 303)
        '
        'ListView
        '
        Me.ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView.FullRowSelect = True
        Me.ListView.Location = New System.Drawing.Point(0, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(237, 303)
        Me.ListView.TabIndex = 2
        Me.ListView.View = System.Windows.Forms.View.List
        '
        'pnlChild
        '
        Me.pnlChild.Controls.Add(Me.tabChild)
        Me.pnlChild.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlChild.Location = New System.Drawing.Point(0, 328)
        Me.pnlChild.Name = "pnlChild"
        Me.pnlChild.Size = New System.Drawing.Size(237, 25)
        '
        'pnlSibling
        '
        Me.pnlSibling.Controls.Add(Me.tabSibling)
        Me.pnlSibling.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSibling.Location = New System.Drawing.Point(0, 0)
        Me.pnlSibling.Name = "pnlSibling"
        Me.pnlSibling.Size = New System.Drawing.Size(237, 25)
        '
        'ToolBar
        '
        Me.ToolBar.Buttons.Add(Me.ToolBarButton1)
        Me.ToolBar.Buttons.Add(Me.ToolBarButton2)
        Me.ToolBar.Buttons.Add(Me.ToolBarButton3)
        Me.ToolBar.Buttons.Add(Me.ToolBarButton4)
        Me.ToolBar.Buttons.Add(Me.ToolBarButton5)
        Me.ToolBar.ImageList = Me.ImageList1
        Me.ToolBar.Name = "ToolBar"
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 0
        Me.ToolBarButton1.Tag = "up"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.ImageIndex = 2
        Me.ToolBarButton2.Tag = "view"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.ImageIndex = 3
        Me.ToolBarButton4.Tag = "add"
        '
        'ToolBarButton5
        '
        Me.ToolBarButton5.ImageIndex = 4
        Me.ToolBarButton5.Tag = "delete"
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(32, 32)
        Me.ImageList1.Images.Clear()
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Icon))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Icon))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Icon))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Icon))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Icon))
        '
        'oDataObjectView
        '
        Me.oDataObjectView.AutoScroll = True
        Me.oDataObjectView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oDataObjectView.Location = New System.Drawing.Point(0, 24)
        Me.oDataObjectView.Name = "oDataObjectView"
        Me.oDataObjectView.Size = New System.Drawing.Size(237, 329)
        Me.oDataObjectView.Visible = False
        '
        'tabChild
        '
        Me.tabChild.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tabChild.Location = New System.Drawing.Point(0, 0)
        Me.tabChild.Name = "tabChild"
        Me.tabChild.Size = New System.Drawing.Size(237, 25)
        Me.tabChild.TabIndex = 4
        '
        'tabSibling
        '
        Me.tabSibling.Dock = System.Windows.Forms.DockStyle.Top
        Me.tabSibling.Location = New System.Drawing.Point(0, 0)
        Me.tabSibling.Name = "tabSibling"
        Me.tabSibling.Size = New System.Drawing.Size(237, 25)
        Me.tabSibling.TabIndex = 3
        '
        'oForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.oDataObjectView)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.oQueryView)
        Me.Name = "oForm"
        Me.Size = New System.Drawing.Size(237, 353)
        Me.oQueryView.ResumeLayout(False)
        Me.pnlView.ResumeLayout(False)
        Me.pnlChild.ResumeLayout(False)
        Me.pnlSibling.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oQueryView As System.Windows.Forms.Panel
    Friend WithEvents pnlView As System.Windows.Forms.Panel
    Friend WithEvents ListView As System.Windows.Forms.ListView
    Friend WithEvents pnlChild As System.Windows.Forms.Panel
    Friend WithEvents tabChild As ChildButtons
    Friend WithEvents pnlSibling As System.Windows.Forms.Panel
    Friend WithEvents tabSibling As SiblingButtons
    Friend WithEvents ToolBar As System.Windows.Forms.ToolBar
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents oDataObjectView As System.Windows.Forms.Panel

End Class
