Imports Priority
Imports Priority.OData

Public Class oDetail

    Private Fields As New Dictionary(Of String, List(Of oDataField))
    Private SelectedField As oDataField = Nothing
    Private ScanBuffer As ScanBuffer

    Private _Parent As oForm
    Sub New(ByRef Parent As oForm)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Parent = Parent
        ScanBuffer = New ScanBuffer

        Fields.Add("main", New List(Of oDataField))

        With TabControl1
            AddHandler .KeyDown, AddressOf hKeyDown
            AddHandler .TabPages(0).Paint, AddressOf hTabPaint
            AddHandler .KeyPress, AddressOf ScanBuffer.hKeypress
        End With
        AddHandler ScanBuffer.BufferChanged, AddressOf hBufferChanged

    End Sub

    Public Sub AddField(ByVal oDataField As oDataField)
        With oDataField

            AddHandler .FieldSelect, AddressOf hClickSelector

            With .PropertyInfo
                If .Tab Is Nothing Then
                    Fields("main").Add(oDataField)                    
                Else
                    Dim f As Boolean = False
                    For Each TabPage As TabPage In Me.TabControl1.TabPages
                        If String.Compare(.Tab, TabPage.Tag, True) = 0 Then
                            f = True
                            Fields(TabPage.Tag).Add(oDataField)
                        End If
                    Next
                    If Not f Then
                        Dim newtab As New TabPage
                        newtab.Text = .Tab
                        newtab.Tag = .Tab
                        newtab.AutoScroll = True
                        AddHandler newtab.Paint, AddressOf hTabPaint
                        TabControl1.TabPages.Add(newtab)
                        Fields.Add(newtab.Tag, New List(Of oDataField))
                        Fields(newtab.Tag).Add(oDataField)
                    End If
                End If
            End With
        End With
    End Sub

    Public Sub Sort(ByRef oForm As oForm)

        With TabControl1
            If .TabPages(0).Controls.Count = 0 Then
                .TabPages.RemoveAt(0)
                .SelectedIndex = 0
            End If
        End With

        Dim ordered As New Dictionary(Of Integer, oDataField)
        Dim t As Integer = 0
        For Each TabPage As TabPage In Me.TabControl1.TabPages

            Dim tabctrl As New Dictionary(Of Integer, Integer)
            Dim p As New List(Of Integer)

            With Fields(TabPage.Tag)
                For i As Integer = 0 To .Count - 1
                    Dim newp As Integer = .Item(i).PropertyInfo.POS
                    tabctrl.Add(i, newp)
                    If Not p.Contains(newp) Then p.Add(newp)
                Next

                p.Sort()
                For i As Integer = p.Count - 1 To 0 Step -1
                    For Each k As Integer In tabctrl.Keys
                        If tabctrl(k) = p(i) Then
                            With TabPage.Controls
                                Fields(TabPage.Tag).Item(k).TabPageIndex = t
                                .Add(Fields(TabPage.Tag).Item(k))
                                .Item(.Count - 1).Dock = DockStyle.Top
                                If Not Fields(TabPage.Tag).Item(k).ReadOnly Then
                                    ordered.Add(ordered.Count, Fields(TabPage.Tag).Item(k))
                                End If
                            End With
                        End If
                    Next
                Next

            End With

            t += 1

        Next

        For i As Integer = 0 To ordered.Count - 1
            With ordered(i)
                Select Case i
                    Case 0
                        .SetPrevious(ordered(i + 1))
                        .SetNext(ordered(ordered.Count - 1))

                    Case ordered.Count - 1
                        .SetPrevious(ordered(0))
                        .SetNext(ordered(i - 1))

                    Case Else
                        .SetNext(ordered(i - 1))
                        .SetPrevious(ordered(i + 1))

                End Select
            End With

        Next

    End Sub

    Private Sub hTabPaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

        For Each oDataField As oDataField In TryCast(sender, TabPage).Controls
            With oDataField
                .pnl_Left.Width = (.MainPanel.Width * 0.4) - 2
                .pnl_Right.Width = (.MainPanel.Width * 0.6) - 2

                With .FieldSelector
                    .Width = e.Graphics.MeasureString(.Text, .Font).Width + 5
                    .Left = 0
                    If .Width < oDataField.pnl_Left.Width Then
                        .Left = oDataField.pnl_Left.Width - .Width
                    End If
                End With

            End With

        Next

    End Sub

    Private Sub hClickSelector(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Not SelectedField Is Nothing Then
            Try
                ProcessBuffer()                

            Catch EX As Exception
                Exit Sub

            End Try
        End If

        SelectedField = sender
        SelectedField.FieldMode = eFieldMode.Edit
        StatusBar.Text = SelectedField.DisplayName

        With TabControl1
            .SelectedIndex = SelectedField.TabPageIndex
            .Focus()
        End With

    End Sub

    Private Sub hKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If Not SelectedField Is Nothing Then
            Select Case e.KeyValue
                Case 27
                    e.Handled = True
                    'If MsgBox("Click ok to reset and close the form.", MsgBoxStyle.OkCancel, "Reset?") = MsgBoxResult.Ok Then
                    '    _Parent.ParentForm.thisHandler.ResetForm(Me.ParentForm)
                    'End If

                Case 8
                    e.Handled = True

                    If (Not ScanBuffer.is2d) Then
                        ScanBuffer.BackSpace()
                    End If


                Case 40, 39, 38, 37, 237, 238
                    e.Handled = True
                    Dim ex As New Exception

                    Select Case e.KeyValue
                        Case 40, 39, 238
                            hClickSelector(SelectedField.NextControl, New System.EventArgs)

                        Case 38, 37, 237
                            hClickSelector(SelectedField.PreviousControl, New System.EventArgs)

                    End Select

                Case 32, 113
                    e.Handled = True

                    'If _ScanBuffer.Length = 0 Then
                    '    ParentForm.thisHandler.AltEntry(Me)
                    'End If

                Case 10
                    e.Handled = True

                Case 13
                    e.Handled = True

                    With SelectedField
                        Try
                            ProcessBuffer()
                            hClickSelector(SelectedField.NextControl, New System.EventArgs)

                        Catch ex As Exception
                            MsgBox(ex.Message, , .DisplayName)

                        End Try

                    End With


                    'If Me.Selected Then
                    '    _IgnoreClick = True

                    '    Dim ex As New Exception
                    '    ProcessBuffer(ex)

                    '    If IsNothing(ex) Then

                    '        If thisColumn.Value.Length > 0 Then
                    '            Deselect()
                    '            Parent.NextControl(True)
                    '        End If

                    '    Else
                    '        MsgBox(ex.Message, , thisColumn.Title)
                    '    End If

                    'End If

                Case 63, 46, 112
                    'e.Handled = True
                    'With thisColumn
                    '    MsgBox(.Help, MsgBoxStyle.OkOnly, .Title)
                    'End With

            End Select
        End If

    End Sub

    Private Sub ProcessBuffer()

        Dim exep As Exception = Nothing
        With SelectedField
            Try
                If ScanBuffer.Value.Length > 0 Then
                    .PropertyInfo.SetValue(_Parent.CurrencyManager.Current, .FieldEdit.Text, Nothing)
                    .FieldText.Text = .FieldEdit.Text
                    If Not _Parent.ViewMode = eViewMode.ViewAdd Then
                        TryCast(_Parent.ListView.Items(_Parent.ListView.SelectedIndices(0)), oListViewItem).UpdateRow()
                    End If

                End If
                .FieldMode = eFieldMode.View

            Catch ex As Exception
                exep = ex

            Finally
                Connection.RaiseEndData()
                .FieldEdit.Text = ""
                ScanBuffer.Clear()

            End Try

            If Not exep Is Nothing Then
                Throw exep
            End If

        End With
    End Sub

    Private Sub hBufferChanged()
        If Not SelectedField Is Nothing Then
            Select Case ScanBuffer.is2d
                Case True
                    SelectedField.FieldEdit.Text = String.Empty
                Case Else
                    SelectedField.FieldEdit.Text = ScanBuffer.Value
            End Select
        End If

    End Sub

End Class
