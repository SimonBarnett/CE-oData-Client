Public Class nullableLabel : Inherits Label

    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = value
            End If
        End Set
    End Property

End Class
