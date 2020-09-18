Imports System.Data.SQLite
Imports System.Globalization

Public Class VendaDAO : Inherits DatabaseController
    Public Function CreateVenda(ByVal Venda As Venda) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Transaction As SQLiteTransaction = Connection.BeginTransaction()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.Transaction = Transaction

        Try
            Command.CommandText = "INSERT INTO Venda (Data, RG_FK) VALUES (@data, @rg);"
            Command.Parameters.AddWithValue("@data", Venda.GetData().ToString("dd/mm/yyyy"))
            Command.Parameters.AddWithValue("@rg", Venda.GetCliente().GetRG())
            Command.Prepare()
            Command.ExecuteNonQuery()

            Dim LastInsertedId As Integer = Connection.LastInsertRowId

            For Each Item In Venda.GetItens()
                Command.CommandText = "INSERT INTO ItemVenda (Codigo_FK, Numero_FK, Valor, Quantidade) VALUES (@codigo, @numero, @valor, @quantidade);"
                Command.Parameters.AddWithValue("@codigo", Item.GetProduto().GetCódigo())
                Command.Parameters.AddWithValue("@numero", LastInsertedId)
                Command.Parameters.AddWithValue("@valor", Item.GetValor())
                Command.Parameters.AddWithValue("@quantidade", Item.GetQuantidade())
                Command.Prepare()
                Command.ExecuteNonQuery()
            Next

            Transaction.Commit()
            Disconnect(Connection)
            Return True
        Catch ex As Exception
            Transaction.Rollback()
            Disconnect(Connection)
            Return False
        End Try
    End Function

    Public Function EditVenda(ByVal Venda As Venda) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "UPDATE Venda SET Data = @data, RG_FK = @rg WHERE Numero = @numero;"
        Command.Parameters.AddWithValue("@data", Venda.GetData())
        Command.Parameters.AddWithValue("@rg", Venda.GetCliente().GetRG())
        Command.Parameters.AddWithValue("@numero", Venda.GetNúmero())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return RowsAffected > 0
    End Function

    Public Function RemoveVenda(ByVal Venda As Venda) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "DELETE FROM Venda WHERE Numero = @numero;"
        Command.Parameters.AddWithValue("@numero", Venda.GetNúmero())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return RowsAffected > 0
    End Function

    Public Function GetVenda(ByRef Venda As Venda) As Venda
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader

        Command.CommandText = "SELECT Data, RG_FK FROM Venda WHERE Numero = @numero;"
        Command.Parameters.AddWithValue("@numero", Venda.GetNúmero())

        DataReader = Command.ExecuteReader()

        If Not DataReader.HasRows Then
            Venda = Nothing
        Else
            While DataReader.Read()
                Venda.SetData(Date.ParseExact(DataReader("Data"), "dd/mm/yyyy", CultureInfo.InvariantCulture))
                Venda.SetCliente(New Cliente(DataReader("RG_FK")))
            End While
        End If

        DataReader.Close()
        Disconnect(Connection)

        Return Venda
    End Function

    Public Function GetAllVenda() As List(Of Venda)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader
        Dim Vendas As New List(Of Venda)

        Command.CommandText = "SELECT Numero, Data, RG_FK FROM Venda;"

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Dim Venda As New Venda(Integer.Parse(DataReader("Numero")), Date.ParseExact(DataReader("Data"), "dd/mm/yyyy", CultureInfo.InvariantCulture), New Cliente(DataReader("RG_FK")), New List(Of ItemVenda))
            Vendas.Add(Venda)
        End While

        DataReader.Close()
        Disconnect(Connection)

        Return Vendas
    End Function
End Class
