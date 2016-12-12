Imports System.Windows.Forms

Public Class ChildButtons

    Public Event Navigate(ByVal Key As Integer)

#Region "Public Methods"

    Sub Add(ByVal Key As Integer, ByVal Title As String)

        Dim btn As New Button
        With btn
            .Text = Title
            .Tag = Key
            .Font = New Font("Verdana", 10, FontStyle.Regular)
            AddHandler .Click, AddressOf hBtnClick

        End With

        pnlButtons.Controls.Add(btn)
        btnSelect.Maximum = pnlButtons.Controls.Count - 1

    End Sub

    Sub Clear()
        With Me
            .pnlButtons.Controls.Clear()
            With .btnSelect
                .Maximum = 0
                .Minimum = 0
                .Value = 0
            End With

        End With
    End Sub
    
    Public WriteOnly Property ButtonsEnabled() As Boolean
        Set(ByVal value As Boolean)
            For Each btn As Button In pnlButtons.Controls
                btn.Enabled = value
            Next
        End Set
    End Property


#End Region

#Region "Private Event Handlers"

    Private Sub hBtnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        With TryCast(sender, Button)
            RaiseEvent Navigate(CInt(.Tag))
        End With
    End Sub

    Private Sub btnSelect_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.ValueChanged
        With pnlButtons
            .Left = 0 - .Controls(btnSelect.Value).Left
        End With
    End Sub

    Public Sub pnlButtons_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlButtons.Paint
        Dim l As Integer = 0
        For Each btn As Button In pnlButtons.Controls
            With btn
                .Left = l
                .Width = e.Graphics.MeasureString(.Text, .Font).Width + 10
                .Top = 1
                .Height = Me.Height - 2
                l += .Width
            End With
        Next

        Me.pnlButtons.Width = l
        btnSelect.Visible = Me.pnlButtons.Width > Screen.PrimaryScreen.Bounds.Width

    End Sub

#End Region

End Class
