Imports Priority.OData
Imports System.Reflection

Public Class oListViewItem : Inherits ListViewItem

    Private _oDataObject As oDataObject
    Public Property oDataObject() As oDataObject
        Get
            Return _oDataObject
        End Get
        Set(ByVal value As oDataObject)
            _oDataObject = value
        End Set
    End Property

    Sub New(ByRef Item As oDataObject)

        Dim F As Boolean = False
        Me.Tag = Item.KeyString
        _oDataObject = Item

        For Each mInfo As PropertyInfo In Item.GetType.GetProperties
            ' Iterate through all the attributes of the method.
            Dim attrib As New CustomProperties(mInfo)
            If attrib.Browsable Then
                Dim val As Object = Item.GetType.GetProperty(mInfo.Name).GetValue(Item, Nothing)
                If Not val Is Nothing Then
                    If F Then
                        Me.SubItems.Add(val.ToString)
                    Else
                        Me.Text = val.ToString
                        F = True
                    End If

                Else
                    If F Then
                        Me.SubItems.Add("")
                    Else
                        Me.Text = ""
                    End If

                End If
            End If
        Next
    End Sub

    Sub UpdateRow()

        Dim i As Integer = 0
        For Each mInfo As PropertyInfo In _oDataObject.GetType.GetProperties

            ' Iterate through all the attributes of the method.
            Dim attrib As New CustomProperties(mInfo)

            If attrib.Browsable Then

                Dim val As Object = _oDataObject.GetType.GetProperty(mInfo.Name).GetValue(_oDataObject, Nothing)
                If Not val Is Nothing Then
                    Me.SubItems(i).Text = val.ToString

                Else
                    Me.SubItems(i).Text = ""

                End If

                i += 1

            End If

        Next

    End Sub

End Class
