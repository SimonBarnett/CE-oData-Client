Imports System.Reflection
Imports Priority.OData

Public Enum eFieldMode
    View
    Edit
End Enum

Public Class oDataField

    Public Event FieldSelect(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private _FieldMode As eFieldMode = eFieldMode.View
    Public Property FieldMode() As eFieldMode
        Get
            Return _FieldMode
        End Get
        Set(ByVal value As eFieldMode)
            _FieldMode = value
            Select Case _FieldMode
                Case eFieldMode.Edit
                    FieldEdit.Visible = True
                    FieldEdit.Text = ""
                    FieldText.Visible = False

                Case eFieldMode.View
                    FieldEdit.Visible = False
                    FieldText.Visible = True

            End Select
        End Set
    End Property

    Private _TabPageIndex As Integer
    Public Property TabPageIndex() As Integer
        Get
            Return _TabPageIndex
        End Get
        Set(ByVal value As Integer)
            _TabPageIndex = value
        End Set
    End Property

    Private _next As oDataField
    Sub SetNext(ByRef oDataField As oDataField)
        _Next = oDataField
    End Sub
    Public ReadOnly Property NextControl() As oDataField
        Get
            Return _next
        End Get
    End Property

    Private _Previous As oDataField
    Sub SetPrevious(ByRef oDataField As oDataField)
        _Previous = oDataField
    End Sub
    Public ReadOnly Property PreviousControl() As oDataField
        Get
            Return _Previous
        End Get
    End Property

#Region "Custom Properties"

    Private _CustomProperties As CustomProperties
    Public ReadOnly Property PropertyInfo() As CustomProperties
        Get
            Return _CustomProperties
        End Get
    End Property

    Public ReadOnly Property FieldName() As String
        Get
            Return _CustomProperties.Name
        End Get
    End Property

    Public ReadOnly Property DisplayName() As String
        Get
            Return _CustomProperties.DisplayName
        End Get
    End Property

    Public ReadOnly Property Mandatory() As Boolean
        Get
            Return _CustomProperties.Mandatory
        End Get
    End Property

    Public ReadOnly Property [ReadOnly]() As Boolean
        Get
            Return _CustomProperties.Readonly
        End Get
    End Property

#End Region

    Private _Parent As oForm
    Public Sub New(ByRef oForm As oForm, ByVal prop As CustomProperties)

        InitializeComponent()

        _CustomProperties = prop
        _Parent = oForm
        Me.Name = _CustomProperties.Name

        With FieldSelector
            .Text = DisplayName            

            Select Case Mandatory
                Case True
                    .ForeColor = Color.Red
                Case Else
                    .ForeColor = Color.Blue
            End Select

            Select Case [ReadOnly]
                Case True
                    FieldSelector.Enabled = False
                    With FieldText
                        .ForeColor = Color.Gray
                        .BackColor = Color.LightGray
                    End With

                Case Else
                    FieldSelector.Enabled = True
                    With FieldText
                        .ForeColor = Color.Black
                        .BackColor = Color.White
                    End With

            End Select

        End With

        'If _Parent.ViewMode = eViewMode.ViewForm Then
        With FieldText
            .DataBindings.Add("Text", _Parent.oDataQuery.BindingSource, FieldName, True)

        End With
        'End If

    End Sub

    Private Sub FieldSelector_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FieldSelector.Click
        RaiseEvent FieldSelect(Me, e)

    End Sub

End Class
