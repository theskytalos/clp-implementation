Public Class Venda : Implements ITotalizavel
    Private Número As Integer
    Private Data As Date
    Private Cliente As Cliente
    Private Itens As List(Of ItemVenda)

    Public Function GetNúmero() As Integer
        Return Número
    End Function

    Public Sub SetNúmero(ByVal Número As Integer)
        Me.Número = Número
    End Sub

    Public Function GetData() As Date
        Return Data
    End Function

    Public Sub SetData(ByVal Data As Date)
        Me.Data = Data
    End Sub

    Public Function GetCliente() As Cliente
        Return Cliente
    End Function

    Public Sub SetCliente(ByVal Cliente As Cliente)
        Me.Cliente = Cliente
    End Sub

    Public Function GetItens() As List(Of ItemVenda)
        Return Itens
    End Function

    Public Sub SetItens(ByVal Itens As List(Of ItemVenda))
        Me.Itens = Itens
    End Sub

    Public Function Total() As Decimal Implements ITotalizavel.Total
        Dim TotalCount As Decimal = 0.0

        For Each Item In Itens
            TotalCount += Item.Total()
        Next

        Return TotalCount
    End Function
End Class
