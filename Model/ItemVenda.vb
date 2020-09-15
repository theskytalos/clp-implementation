Public Class ItemVenda : Implements ITotalizavel
    Private Produto As Produto
    Private Valor As Decimal
    Private Quantidade As Integer

    Public Function GetProduto() As Produto
        Return Produto
    End Function

    Public Sub SetProduto(ByVal Produto As Produto)
        Me.Produto = Produto
    End Sub

    Public Function GetValor() As Decimal
        Return Valor
    End Function

    Public Sub SetValor(ByVal Valor As Decimal)
        Me.Valor = Valor
    End Sub

    Public Function GetQuantidade() As Integer
        Return Quantidade
    End Function

    Public Sub SetQuantidade(ByVal Quantidade As Integer)
        Me.Quantidade = Quantidade
    End Sub

    Public Function Total() As Decimal Implements ITotalizavel.Total
        Return Valor * Quantidade
    End Function
End Class
