Imports System.Windows.Forms

Public Class SiblingButtons

    Public Event Navigate(ByVal Key As Integer)

#Region "Public Methods"

    Private ld As Boolean = False

    Sub Add(ByVal Key As Integer, ByVal Title As String, ByVal Selected As Boolean)

        Dim btn As New Button
        With btn
            .Text = Title
            .Tag = Key
            .Font = New Font("Verdana", 10, FontStyle.Regular)

            Select Case Selected
                Case True
                    .ForeColor = Color.Red
                Case Else
                    .ForeColor = Color.Black
            End Select

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

#End Region

#Region "Private Event Handlers"

    Private Sub hBtnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        ld = False
        With TryCast(sender, Button)
            RaiseEvent Navigate(CInt(.Tag))
        End With
    End Sub

    Private Sub btnSelect_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.ValueChanged
        With pnlButtons    
            .Left = 0 - .Controls(btnSelect.Value).Left            
        End With
    End Sub

    Private Sub pnlButtons_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlButtons.Paint
        Dim l As Integer = 0
        Dim c As Integer = 0
        For Each btn As Button In pnlButtons.Controls
            With btn
                .Left = l
                .Width = e.Graphics.MeasureString(.Text, .Font).Width + 10
                .Top = 1
                .Height = Me.Height - 2
                l += .Width
                If Not ld And btn.ForeColor = Color.Red Then
                    pnlButtons.Left = 0 - pnlButtons.Controls(c).Left
                    btnSelect.Value = c
                End If
            End With
            c += 1
        Next

        Me.pnlButtons.Width = l
        btnSelect.Visible = Me.pnlButtons.Width > Screen.PrimaryScreen.Bounds.Width
        ld = True

    End Sub

#End Region

End Class
