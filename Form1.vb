Imports Priority.OData
Imports System.Net
Imports Newtonsoft.Json

Public Class Form1

#Region "Connection Details"

    Private Sub SetGlobal()

        With Connection.Settings
            .Server = "walrus.wonderland.local"
            .Environment = "wlnd"
            .Credential = New NetworkCredential("load", "123456")
            .CheckSSL = False
            With .Debug
                .ShowAll()
                .ShowHeaders = False
            End With
        End With

        AddHandler Connection.DebugOutput, AddressOf hDebug
        AddHandler Connection.ErrorOutput, AddressOf hError
        AddHandler Connection.StartData, AddressOf hStartData
        AddHandler Connection.EndData, AddressOf hEndData

    End Sub

    Private Sub hDebug(ByVal data As String)
        Console.WriteLine(data)
    End Sub

    Private Sub hError(ByVal Title As String, ByVal data As String)
        MsgBox(data)
    End Sub

    Private Sub hStartData()
        Cursor.Current = Cursors.WaitCursor
        Application.DoEvents()

    End Sub

    Private Sub hEndData()
        Cursor.Current = Cursors.Default
        Application.DoEvents()

    End Sub

#End Region

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        With Me.Controls
            If .Count > 1 Then
                e.Cancel = True
                hNavigateUp()

            Else
                e.Cancel = Not (MsgBox("Close form?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok)

            End If

        End With
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetGlobal()
        hNavigateChild(0, New QUERY_FAMILY_LOG)

    End Sub

    Private Sub hNavigateUp()

        With Me.Controls

            With .Item(0)
                .Dock = DockStyle.None
                .Visible = False
                .Dispose()
            End With

            With TryCast(.Item(0), oForm)
                Me.Text = .FormTitle
                .Visible = True
                .Dock = DockStyle.Fill
                .BringToFront()
                .Invalidate()
                .ListView.Focus()
            End With

        End With

    End Sub

    Private Sub hNavigateChild(ByVal Key As Integer, ByRef oDataQuery As oDataQuery)
        With Me.Controls
            With oDataQuery
                .Load()
                'If .Parent Is Nothing Then

                'Else
                '    .Load("TECHNICIANLOGIN eq 'tabula'", "FAMILYDESC", SortDirection.asc)
                'End If
            End With

            .Add(New oForm(Me, Key, oDataQuery))
            With TryCast(.Item(.Count - 1), oForm)
                Me.Text = .FormTitle
                .Dock = DockStyle.Fill
                AddHandler .Navigate, AddressOf hNavigateChild
                AddHandler .NavigateUp, AddressOf hNavigateUp
                .BringToFront()
                .ListView.Focus()
            End With

        End With
    End Sub

End Class
