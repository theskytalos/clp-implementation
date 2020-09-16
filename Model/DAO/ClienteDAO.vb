Imports System.Data.SQLite

Public Class ClienteDAO : Inherits DatabaseController
    Public Function CreateCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "INSERT INTO Cliente (Nome, Endereco, RG, DataDeNascimento) VALUES (@nome, @endereco, @rg, @datadenascimento);"
        Command.Parameters.AddWithValue("@nome", Cliente.GetNome())
        Command.Parameters.AddWithValue("@Endereco", Cliente.GetEndereço())
        Command.Parameters.AddWithValue("@RG", Cliente.GetRG())
        Command.Parameters.AddWithValue("@DataDeNascimento", Cliente.GetDataDeNascimento())

        Command.Prepare()
        Return IIf(Command.ExecuteNonQuery() < 1, False, True)
    End Function

    Public Function EditCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "UPDATE Cliente SET Nome = @nome, Endereco = @endereco, DataDeNascimento = @datadenascimento WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@nome", Cliente.GetNome())
        Command.Parameters.AddWithValue("@Endereco", Cliente.GetEndereço())
        Command.Parameters.AddWithValue("@DataDeNascimento", Cliente.GetDataDeNascimento())
        Command.Parameters.AddWithValue("@RG", Cliente.GetRG())

        Command.Prepare()
        Return IIf(Command.ExecuteNonQuery() < 1, False, True)
    End Function

    Public Function RemoveCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "DELETE FROM Cliente WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())

        Command.Prepare()
        Return IIf(Command.ExecuteNonQuery() < 1, False, True)
    End Function

    Public Sub GetCliente(ByRef Cliente As Cliente)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader

        Command.CommandText = "SELECT Nome, Endereco, DataDeNascimento FROM Cliente WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Cliente.SetNome(DataReader.GetString("Nome"))
            Cliente.SetEndereço(DataReader.GetString("Endereco"))
            Cliente.SetDataDeNascimento(DataReader.GetDateTime("DataDeNascimento"))
        End While

        Disconnect(Connection)
    End Sub

    Public Function GetAllCliente() As List(Of Cliente)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader
        Dim Clientes As New List(Of Cliente)

        Command.CommandText = "SELECT Nome, Endereco, RG, DataDeNascimento FROM Cliente;"

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Dim Cliente As New Cliente(DataReader.GetString("Nome"), DataReader.GetString("Endereco"), DataReader.GetString("RG"), DataReader.GetDateTime("DataDeNascimento"))
            Clientes.Add(Cliente)
        End While

        Return Clientes
    End Function
End Class