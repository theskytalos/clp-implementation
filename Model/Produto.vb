Public Class Produto
    Private Código As Integer
    Private Nome As String
    Private Valor As Decimal

    Public Function GetCódigo() As Integer
        Return Código
    End Function

    Public Sub SetCódigo(ByVal Código As Integer)
        Me.Código = Código
    End Sub

    Public Function GetNome() As String
        Return Nome
    End Function

    Public Sub SetNome(ByVal Nome As String)
        Me.Nome = Nome
    End Sub

    Public Function GetValor() As Decimal
        Return Valor
    End Function

    Public Sub SetValor(ByVal Valor As Decimal)
        Me.Valor = Valor
    End Sub
End Class
