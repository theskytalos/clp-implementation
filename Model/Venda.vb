Public Class Venda : Implements ITotalizavel
    Private Número As Integer
    Private Data As Date
    Private Cliente As Cliente
    Private Itens As List(Of ItemVenda)

    Public Sub New()

    End Sub

    Public Sub New(ByVal Número As Integer)
        Me.Número = Número
    End Sub

    Public Sub New(ByVal Número As Integer, ByVal Data As Date, ByVal Cliente As Cliente, ByVal Itens As List(Of ItemVenda))
        Me.Número = Número
        Me.Data = Data
        Me.Cliente = Cliente
        Me.Itens = Itens
    End Sub

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

    Public Overrides Function ToString() As String
        Dim ItemVendaString As String = String.Empty

        For Each Item In Itens
            If Item.Equals(Itens.Last()) Then
                ItemVendaString &= vbTab & Item.ToString()
            Else
                ItemVendaString &= vbTab & Item.ToString() & vbCrLf
            End If
        Next

        Return "Número: " + Número.ToString() + "; Data: " + Data.ToString("dd/mm/yyyy") + "; Cliente: " + Cliente.GetRG() + "; Total: R$" + Total().ToString() + "; Itens: " + vbCrLf + ItemVendaString
    End Function
End Class
