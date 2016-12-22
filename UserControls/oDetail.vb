Imports Priority
Imports Priority.OData

Public Class oDetail

    Private Fields As New Dictionary(Of String, List(Of oDataField))
    Private SelectedField As oDataField = Nothing
    Private _ScanBuffer As ScanBuffer

#Region "Constructor"

    Private _Parent As oForm

    ''' <summary>
    ''' Detail Form constructor
    ''' </summary>
    ''' <param name="Parent">The oForm this control belongs to.</param>
    ''' <remarks>Constructor initialises the _ScanBuffer and sets events handlers.</remarks>
    Sub New(ByRef Parent As oForm)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Parent = Parent
        _ScanBuffer = New ScanBuffer

        Fields.Add("main", New List(Of oDataField))

        With TabControl1
            AddHandler .KeyDown, AddressOf hKeyDown
            AddHandler .TabPages(0).Paint, AddressOf hTabPaint
            AddHandler .KeyPress, AddressOf _ScanBuffer.hKeypress
            AddHandler .SelectedIndexChanged, AddressOf hTabClick
        End With
        AddHandler _ScanBuffer.BufferChanged, AddressOf hBufferChanged

    End Sub

    ''' <summary>
    ''' Adds a field to the detail form.
    ''' </summary>
    ''' <param name="oDataField">The oDataField interface control that displays the field</param>
    ''' <remarks>oDataFields have custom attributes todetermine screen layout. 
    ''' The screen layout occurs when the Sort method is called.</remarks>
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

    ''' <summary>
    ''' Layout the detail forms controls. 
    ''' </summary>    
    ''' <remarks>The Sort method preforms layout for the controls in the form, sorted by tab and pos attribute.</remarks>
    Public Sub Sort()

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
        If ordered.Count > 0 Then
            If ordered.Count > 1 Then
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
            Else
                With ordered(0)
                    .SetNext(Nothing)
                    .SetPrevious(Nothing)
                End With

            End If
        End If

    End Sub

#End Region

#Region "Pubic Properties"

    Public ReadOnly Property ScanBuffer() As ScanBuffer
        Get
            Return _ScanBuffer
        End Get
    End Property

#End Region

#Region "Event Handlers"

    Private UserClick As Boolean = True
    Private Sub hTabClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If UserClick Then
            Try
                If _ScanBuffer.Value.Length > 0 Then ProcessBuffer()

            Catch ex As Exception
                With _Parent.ListView
                    MsgBox(ex.Message, , SelectedField.DisplayName)

                End With

            End Try

        End If

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
            With SelectedField
                Try
                    ProcessBuffer()                    

                Catch ex As Exception
                    MsgBox(ex.Message, , .DisplayName)
                    Exit Sub

                End Try

            End With
        End If

        SelectedField = sender
        If Not sender Is Nothing Then
            SelectedField.FieldMode = eFieldMode.Edit
            StatusBar.Text = String.Format("{0}: {1}", SelectedField.DisplayName, SelectedField.FieldText.Text)
            TabControl1.SelectedIndex = SelectedField.TabPageIndex
        End If

        TabControl1.Focus()        

    End Sub

#End Region

#Region "Scan Buffer"

    ''' <summary>
    ''' Process the scan buffer
    ''' </summary>
    ''' <remarks>This method processes the contents of the scan buffer, or throws an exception</remarks>
    Public Sub ProcessBuffer()

        Dim exep As Exception = Nothing
        With SelectedField
            Try
                If _ScanBuffer.Value.Length > 0 Then
                    .PropertyInfo.SetValue(_Parent.CurrencyManager.Current, .FieldEdit.Text, Nothing)
                    If Not Connection.LastError Is Nothing Then Throw Connection.LastError

                    .FieldText.Text = .FieldEdit.Text
                    If Not _Parent.ViewMode = eViewMode.ViewAdd Then
                        TryCast(_Parent.ListView.Items(_Parent.ListView.SelectedIndices(0)), oListViewItem).UpdateRow()
                    End If

                End If
                .FieldMode = eFieldMode.View

            Catch ex As Exception
                exep = ex
                UserClick = False
                TabControl1.SelectedIndex = SelectedField.TabPageIndex
                UserClick = True

            Finally
                .FieldEdit.Text = ""
                _ScanBuffer.Clear()
                Connection.LastError = Nothing
                Connection.RaiseEndData()

            End Try

            If Not exep Is Nothing Then
                Throw exep
            End If

        End With

    End Sub

    Private Sub hBufferChanged()
        If Not SelectedField Is Nothing Then
            Select Case _ScanBuffer.is2d
                Case True
                    SelectedField.FieldEdit.Text = String.Empty
                Case Else
                    SelectedField.FieldEdit.Text = _ScanBuffer.Value
            End Select
        End If

    End Sub

    Private Function FirstField(ByVal TabIndex As Integer) As oDataField
        With TabControl1.TabPages(TabIndex)
            For i As Integer = .Controls.Count - 1 To 0 Step -1
                If Not TryCast(.Controls(i), oDataField).ReadOnly Then
                    Return TryCast(.Controls(i), oDataField)
                End If
            Next
        End With
        Return Nothing
    End Function

    Public Sub FirstCursor()
        hClickSelector(FirstField(0), New System.EventArgs)
    End Sub

    Private Sub hKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If Not SelectedField Is Nothing Then
            Select Case e.KeyValue
                Case 27
                    e.Handled = True
                    If _Parent.ViewMode = eViewMode.ViewAdd Then
                        _Parent.CancelAddRow()
                    End If

                Case 8
                    e.Handled = True

                    If (Not _ScanBuffer.is2d) Then
                        _ScanBuffer.BackSpace()
                    End If


                Case 40, 38, 237, 238
                    e.Handled = True
                    Dim ex As New Exception

                    Select Case e.KeyValue
                        Case 40, 39, 238
                            hClickSelector(SelectedField.NextControl, New System.EventArgs)

                        Case 38, 37, 237
                            hClickSelector(SelectedField.PreviousControl, New System.EventArgs)

                    End Select

                Case 39, 37 ' left / right
                    e.Handled = True
                    Dim NextTab As Integer
                    Select Case e.KeyValue
                        Case 39
                            Select Case TabControl1.SelectedIndex
                                Case 0
                                    NextTab = TabControl1.TabPages.Count - 1
                                Case Else
                                    NextTab = TabControl1.SelectedIndex - 1

                            End Select

                        Case 37
                            Select Case TabControl1.SelectedIndex
                                Case TabControl1.TabPages.Count - 1
                                    NextTab = 0
                                Case Else
                                    NextTab = TabControl1.SelectedIndex + 1

                            End Select

                    End Select
                    hClickSelector(FirstField(NextTab), New System.EventArgs)

                Case 32, 113
                    e.Handled = True

                    'If __ScanBuffer.Length = 0 Then
                    '    ParentForm.thisHandler.AltEntry(Me)
                    'End If

                Case 10
                    e.Handled = True

                Case 13
                    e.Handled = True

                    With SelectedField
                        Try
                            ProcessBuffer()
                            hClickSelector(.NextControl, New System.EventArgs)

                        Catch ex As Exception
                            MsgBox(ex.Message, , .DisplayName)

                        End Try

                    End With

                Case 36, 112
                    e.Handled = True
                    With SelectedField.PropertyInfo
                        MsgBox(.help, , .DisplayName)
                    End With

            End Select
        End If

    End Sub

#End Region

End Class
