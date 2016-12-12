Imports System.Reflection
Imports Priority.OData

Public Class DataGridBase : Inherits DataGrid

    Public Sub New(ByRef oDataQuery As oDataQuery)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        With Me            
            .GridLineStyle = DataGridLineStyle.None
            .DataSource = oDataQuery.BindingSource


        End With

        Dim ts1 As New DataGridTableStyle


        For Each mInfo As PropertyInfo In oDataQuery.ObjectType.GetProperties
            ' Iterate through all the attributes of the method.
            Dim attrib As New CustomProperties(mInfo)
            With attrib
                If .Browsable Then
                    Dim col As New DataGridTextBoxColumn
                    col.HeaderText = .DisplayName
                    col.MappingName = mInfo.Name
                    ts1.GridColumnStyles.Add(col)
                End If
            End With

        Next

        Me.TableStyles.Add(ts1)


        For Each k As Integer In oDataQuery.ChildQuery.Keys
            Console.Write(oDataQuery.ChildQuery(k))
        Next

        For Each k As Integer In oDataQuery.SibligQuery.Keys

        Next
    End Sub

End Class
