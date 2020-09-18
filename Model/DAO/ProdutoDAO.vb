Imports System.Data.SQLite

Public Class ProdutoDAO : Inherits DatabaseController
    Public Function CreateProduto(ByVal Produto As Produto) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "INSERT INTO Produto (Nome, Valor) VALUES (@nome, @valor);"
        Command.Parameters.AddWithValue("@nome", Produto.GetNome())
        Command.Parameters.AddWithValue("@valor", Produto.GetValor())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function EditProduto(ByVal Produto As Produto) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "UPDATE Produto SET Nome = @nome, Valor = @valor WHERE Codigo = @codigo;"
        Command.Parameters.AddWithValue("@nome", Produto.GetNome())
        Command.Parameters.AddWithValue("@valor", Produto.GetValor())
        Command.Parameters.AddWithValue("@codigo", Produto.GetCódigo())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function RemoveProduto(ByVal Produto As Produto) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "DELETE FROM Produto WHERE Codigo = @codigo;"
        Command.Parameters.AddWithValue("@codigo", Produto.GetCódigo())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function GetProduto(ByVal Produto As Produto) As Produto
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader

        Command.CommandText = "SELECT Nome, Valor FROM Produto WHERE Codigo = @codigo;"
        Command.Parameters.AddWithValue("@codigo", Produto.GetCódigo())

        DataReader = Command.ExecuteReader()

        If Not DataReader.HasRows Then
            Produto = Nothing
        Else
            While DataReader.Read()
                Produto.SetNome(DataReader("Nome"))
                Produto.SetValor(Decimal.Parse(DataReader("Valor")))
            End While
        End If

        DataReader.Close()
        Disconnect(Connection)

        Return Produto
    End Function

    Public Function GetAllProduto() As List(Of Produto)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader
        Dim Produtos As New List(Of Produto)

        Command.CommandText = "SELECT Codigo, Nome, Valor FROM Produto;"

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Dim Produto As New Produto(Integer.Parse(DataReader("Codigo")), DataReader("Nome"), Decimal.Parse(DataReader("Valor")))
            Produtos.Add(Produto)
        End While

        DataReader.Close()
        Disconnect(Connection)

        Return Produtos
    End Function

    Public Function CheckForName(ByVal Produto As Produto) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader

        Command.CommandText = "SELECT Nome, Valor FROM Produto WHERE Nome = @nome;"
        Command.Parameters.AddWithValue("@nome", Produto.GetNome())

        DataReader = Command.ExecuteReader()

        Dim HasRows = DataReader.HasRows

        DataReader.Close()
        Disconnect(Connection)

        Return HasRows
    End Function
End Class
