Imports System.Data.SQLite
Public Class ItemVendaDAO : Inherits DatabaseController
    Public Function CreateItemVenda(ByVal ItemVenda As ItemVenda, ByVal Número As Integer) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "INSERT INTO ItemVenda (Codigo_FK, Numero_FK, Valor, Quantidade) VALUES (@codigo, @numero, @valor, @quantidade);"
        Command.Parameters.AddWithValue("@codigo", ItemVenda.GetProduto().GetCódigo())
        Command.Parameters.AddWithValue("@numero", Número)
        Command.Parameters.AddWithValue("@valor", ItemVenda.GetValor())
        Command.Parameters.AddWithValue("@quantidade", ItemVenda.GetQuantidade())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return RowsAffected > 0
    End Function

    Public Function EditItemVenda(ByVal ItemVenda As ItemVenda, ByVal Venda As Venda) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "UPDATE ItemVenda SET Codigo_FK = @codigo, Numero_FK = @numero, Valor = @valor, Quantidade = @quantidade WHERE ID = @id;"
        Command.Parameters.AddWithValue("@codigo", ItemVenda.GetProduto().GetCódigo())
        Command.Parameters.AddWithValue("@numero", Venda.GetNúmero())
        Command.Parameters.AddWithValue("@valor", ItemVenda.GetValor())
        Command.Parameters.AddWithValue("@quantidade", ItemVenda.GetQuantidade())
        Command.Parameters.AddWithValue("@id", ItemVenda.GetID())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return RowsAffected > 0
    End Function

    Public Function RemoveItemVenda(ByVal ItemVenda As ItemVenda) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "DELETE FROM ItemVenda WHERE ID = @id;"
        Command.Parameters.AddWithValue("@id", ItemVenda.GetID())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return RowsAffected > 0
    End Function

    Public Function GetAllItemVendaByVenda(ByVal Venda As Venda) As List(Of ItemVenda)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader
        Dim Itens As New List(Of ItemVenda)

        Command.CommandText = "SELECT ID, Codigo_FK, Valor, Quantidade FROM ItemVenda WHERE Numero_FK = @numero;"
        Command.Parameters.AddWithValue("@numero", Venda.GetNúmero())

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Dim ItemVenda As New ItemVenda(Integer.Parse(DataReader("ID")), New Produto(Integer.Parse(DataReader("Codigo_FK"))), Decimal.Parse(DataReader("Valor")), Integer.Parse(DataReader("Quantidade")))
            Itens.Add(ItemVenda)
        End While

        DataReader.Close()
        Disconnect(Connection)

        Return Itens
    End Function
End Class
