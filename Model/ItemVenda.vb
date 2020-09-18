Public Class ItemVenda : Implements ITotalizavel
    Private ID As Integer
    Private Produto As Produto
    Private Valor As Decimal
    Private Quantidade As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal Produto As Produto)
        Me.Produto = Produto
    End Sub

    Public Sub New(ByVal Produto As Produto, ByVal Valor As Decimal, ByVal Quantidade As Integer)
        Me.Produto = Produto
        Me.Valor = Valor
        Me.Quantidade = Quantidade
    End Sub

    Public Sub New(ByVal ID As Integer, ByVal Produto As Produto, ByVal Valor As Decimal, ByVal Quantidade As Integer)
        Me.ID = ID
        Me.Produto = Produto
        Me.Valor = Valor
        Me.Quantidade = Quantidade
    End Sub

    Public Function GetID() As Integer
        Return ID
    End Function

    Public Sub SetID(ByVal ID As Integer)
        Me.ID = ID
    End Sub

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

    Public Overrides Function ToString() As String
        Return "Produto: " + Produto.GetCódigo().ToString() + "; Valor: " + Valor.ToString() + "; Quantidade: " + Quantidade.ToString()
    End Function
End Class
