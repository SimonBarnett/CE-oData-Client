﻿Imports System.Reflection
Imports Priority.OData

Public Enum eViewMode
    ViewUp = 0
    ViewTable = 2
    ViewForm = 1
    ViewAdd = 3
    ViewDelete = 4
End Enum

Public Class oForm

#Region "Events"

    Public Event NavigateUp()
    Public Event Navigate(ByVal Key As Integer, ByRef oDataQuery As oDataQuery)

#End Region

    Private dfa As New List(Of oDataField)

#Region "Properties"

    Private WithEvents _CurrencyManager As CurrencyManager
    Public Property CurrencyManager() As CurrencyManager
        Get
            Return _CurrencyManager
        End Get
        Set(ByVal value As CurrencyManager)
            _CurrencyManager = value
        End Set
    End Property

    Public ReadOnly Property thisDetail() As oDetail
        Get
            Return TryCast(Me.oDataObjectView.Controls(0), oDetail)
        End Get
    End Property

    Public ReadOnly Property FormTitle() As String
        Get
            Return _ClassProperties.QueryTitle
        End Get
    End Property

    Private _oDataQuery As oDataQuery
    Public ReadOnly Property oDataQuery() As oDataQuery
        Get
            Return _oDataQuery
        End Get
    End Property

#End Region

#Region "Constructor"

    Private _Parent As Form
    Public Sub New(ByRef Parent As Form, ByVal Key As Integer, ByRef oDataQuery As oDataQuery)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Parent = Parent

        DrawForm(Key, oDataQuery)

        ' Add control handlers
        AddHandler ListView.SelectedIndexChanged, AddressOf ListView_SelectedIndexChanged
        AddHandler tabSibling.Navigate, AddressOf hSiblingClick
        AddHandler tabChild.Navigate, AddressOf hChildClick

    End Sub

    Private _ClassProperties As ClassProperties = Nothing
    Private Sub DrawForm(ByVal Key As Integer, ByRef oDataQuery As oDataQuery)

        _oDataQuery = oDataQuery
        _ClassProperties = New ClassProperties(oDataQuery.GetType)
        _CurrencyManager = CType(Me.BindingContext(oDataQuery.BindingSource), CurrencyManager)

        Dim oDetail As New oDetail(Me)
        With ListView
            .Clear()
            .View = View.Details
            .FullRowSelect = True

            For Each mInfo As PropertyInfo In oDataQuery.ObjectType.GetProperties
                ' Iterate through all the attributes of the method.
                Dim attrib As New CustomProperties(mInfo)
                If attrib.Browsable Then
                    ' Add Field
                    oDetail.AddField(New oDataField(Me, attrib))
                    ' Add Column
                    .Columns.Add(attrib.DisplayName, 100, HorizontalAlignment.Left)
                End If
            Next

            For Each oDataObject As oDataObject In _CurrencyManager.List 'oDataQuery.BindingSource
                .Items.Add(New oListViewItem(oDataObject))
            Next

        End With

        oDetail.Sort()

        With Me.oDataObjectView.Controls
            .Add(oDetail)
            .Item(.Count - 1).Dock = DockStyle.Fill
        End With

        With tabSibling
            .Clear()
            If oDataQuery.SibligQuery.Keys.Count > 0 Then
                Me.pnlSibling.Visible = True
                For Each k As Integer In oDataQuery.SibligQuery.Keys
                    .Add(k, oDataQuery.SibligQuery(k).Name, CBool(k = Key))
                Next
            Else
                Me.pnlSibling.Visible = False
            End If

        End With

        With tabChild
            .Clear()
            If oDataQuery.ChildQuery.Keys.Count > 0 Then
                Me.pnlChild.Visible = True
                For Each k As Integer In oDataQuery.ChildQuery.Keys
                    .Add(k, oDataQuery.ChildQuery(k))
                Next
            Else
                Me.pnlChild.Visible = False
            End If

        End With

        With ListView
            If .Items.Count > 0 Then
                .Items(0).Selected = True
            Else
                tabChild.ButtonsEnabled = False
            End If
        End With

        setButtons()
        ViewMode = eViewMode.ViewTable

    End Sub

#End Region

#Region "Selected item"

    Public ReadOnly Property Selected() As oDataObject
        Get
            If ListView.SelectedIndices.Count > 0 Then
                Return _CurrencyManager.Current
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView.SelectedIndices.Count > 0 Then
            _CurrencyManager.Position = ListView.SelectedIndices(0)
        End If
        setButtons()
    End Sub

#End Region

#Region "Navigation Handlers"

    Private Sub setButtons()
        Dim enabled As Boolean = Not Selected Is Nothing
        tabChild.ButtonsEnabled = enabled
        With ToolBar
            .Buttons(eViewMode.ViewUp).Enabled = Not _oDataQuery.Parent Is Nothing
            .Buttons(eViewMode.ViewForm).Enabled = enabled
            .Buttons(eViewMode.ViewDelete).Enabled = enabled And Not ViewMode = eViewMode.ViewAdd
            .Buttons(eViewMode.ViewAdd).Enabled = True
        End With

    End Sub

    Private Sub hChildClick(ByVal i As Integer)
        RaiseEvent Navigate(i, Selected.ChildQuery(i).oDataQuery)

    End Sub

    Private Sub hSiblingClick(ByVal i As Integer)
        _oDataQuery.SibligQuery(i).oDataQuery.Load()
        Me.oDataObjectView.Controls.Clear()
        DrawForm(i, _oDataQuery.SibligQuery(i).oDataQuery)
        _Parent.Text = Me.FormTitle
        ListView.Focus()

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Tag
            Case "up"
                Select Case ViewMode
                    Case eViewMode.ViewTable
                        RaiseEvent NavigateUp()

                    Case eViewMode.ViewForm
                        Try
                            With thisDetail
                                If .ScanBuffer.Length > 0 Then
                                    .ProcessBuffer()
                                End If
                            End With
                            RaiseEvent NavigateUp()

                        Catch ex As Exception
                            MsgBox(ex.Message, , "Navigate up.")
                            Connection.LastError = Nothing

                        End Try

                    Case eViewMode.ViewAdd
                        If CancelAddRow() Then RaiseEvent NavigateUp()

                End Select

            Case "view"
                Select Case ViewMode
                    Case eViewMode.ViewTable
                        e.Button.ImageIndex = eViewMode.ViewForm
                        ViewMode = eViewMode.ViewForm

                    Case eViewMode.ViewForm
                        Try
                            With thisDetail
                                If .ScanBuffer.Length > 0 Then
                                    .ProcessBuffer()
                                End If
                            End With
                            e.Button.ImageIndex = eViewMode.ViewTable
                            ViewMode = eViewMode.ViewTable

                        Catch ex As Exception
                            MsgBox(ex.Message, , "Switch view.")
                            Connection.LastError = Nothing

                        End Try

                    Case eViewMode.ViewAdd
                        If CancelAddRow() Then
                            e.Button.ImageIndex = eViewMode.ViewTable
                            ViewMode = eViewMode.ViewTable
                        End If

                End Select

            Case "delete"
                If MsgBox("Delete item?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    Try
                        _oDataQuery.Delete(Me.Selected)
                        If Not Connection.LastError Is Nothing Then
                            Throw Connection.LastError
                        End If

                        ListView.Items.RemoveAt(ListView.SelectedIndices(0))
                        _CurrencyManager.Refresh()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                        Connection.LastError = Nothing

                    End Try
                End If

            Case "add"
                Select Case ViewMode
                    Case eViewMode.ViewTable, eViewMode.ViewForm
                        _CurrencyManager.AddNew()
                        ViewMode = eViewMode.ViewAdd

                    Case eViewMode.ViewAdd
                        Try
                            With thisDetail
                                If .ScanBuffer.Length > 0 Then
                                    .ProcessBuffer()
                                End If
                            End With

                            _oDataQuery.Add(_CurrencyManager.Current)
                            If Not Connection.LastError Is Nothing Then
                                Throw Connection.LastError
                            End If

                            _CurrencyManager.Refresh()

                            Dim lvi As oListViewItem = New oListViewItem(_CurrencyManager.Current)
                            lvi.Selected = True

                            ListView.Items.Add(lvi)
                            ViewMode = eViewMode.ViewTable

                        Catch ex As Exception
                            MsgBox(ex.Message, , "Add Row.")
                            Connection.LastError = Nothing

                        End Try

                End Select


        End Select

    End Sub

    Public Function CancelAddRow() As Boolean
        If MsgBox("Cancel creation of new record?", MsgBoxStyle.OkCancel, "Cancel?") = MsgBoxResult.Ok Then
            With _CurrencyManager
                .RemoveAt(.Count - 1)
                .Refresh()
            End With

            Return True
        Else
            Return False
        End If
    End Function

    Private _mode As eViewMode = eViewMode.ViewTable
    Public Property ViewMode() As eViewMode
        Get
            Return _mode

        End Get
        Set(ByVal value As eViewMode)
            _mode = value

            With Me
                Select Case value
                    Case eViewMode.ViewTable
                        .oDataObjectView.Visible = False
                        With .oQueryView
                            .Visible = True
                            .Dock = DockStyle.Fill
                            .BringToFront()
                            ListView.Focus()
                            If _CurrencyManager.Position > -1 Then
                                ListView.Items(_CurrencyManager.Position).Selected = True
                            End If
                        End With

                    Case eViewMode.ViewForm, eViewMode.ViewAdd
                        .oQueryView.Visible = False
                        With .oDataObjectView
                            .Dock = DockStyle.Fill
                            .Visible = True
                            .BringToFront()
                            With TryCast(.Controls(0), oDetail)                                
                                .FirstCursor()
                                .TabControl1.Focus()
                            End With
                        End With

                End Select

                setButtons()

            End With

        End Set
    End Property

#End Region

End Class
