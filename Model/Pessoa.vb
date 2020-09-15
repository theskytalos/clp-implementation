Public MustInherit Class Pessoa
    Private Nome As String
    Private Endereço As String

    Public Function GetNome() As String
        Return Nome
    End Function

    Public Sub SetNome(ByVal Nome As String)
        Me.Nome = Nome
    End Sub

    Public Function GetEndereço() As String
        Return Endereço
    End Function

    Public Sub SetEndereço(ByVal Endereço As String)
        Me.Endereço = Endereço
    End Sub
End Class
