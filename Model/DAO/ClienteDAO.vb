Imports System.Data.SQLite
Imports System.Globalization

Public Class ClienteDAO : Inherits DatabaseController
    Public Function CreateCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "INSERT INTO Cliente (Nome, Endereco, RG, DataDeNascimento) VALUES (@nome, @endereco, @rg, @datadenascimento);"
        Command.Parameters.AddWithValue("@nome", Cliente.GetNome())
        Command.Parameters.AddWithValue("@endereco", Cliente.GetEndereço())
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())
        Command.Parameters.AddWithValue("@datadenascimento", Cliente.GetDataDeNascimento().ToString("dd/mm/yyyy"))

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function EditCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "UPDATE Cliente SET Nome = @nome, Endereco = @endereco, DataDeNascimento = @datadenascimento WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@nome", Cliente.GetNome())
        Command.Parameters.AddWithValue("@endereco", Cliente.GetEndereço())
        Command.Parameters.AddWithValue("@datadenascimento", Cliente.GetDataDeNascimento().ToString("dd/mm/yyyy"))
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())

        Command.Prepare()
        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function RemoveCliente(ByVal Cliente As Cliente) As Boolean
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()

        Command.CommandText = "DELETE FROM Cliente WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())

        Command.Prepare()

        Dim RowsAffected = Command.ExecuteNonQuery()

        Disconnect(Connection)

        Return (RowsAffected > 0)
    End Function

    Public Function GetCliente(ByVal Cliente As Cliente) As Cliente
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader

        Command.CommandText = "SELECT Nome, Endereco, DataDeNascimento FROM Cliente WHERE RG = @rg;"
        Command.Parameters.AddWithValue("@rg", Cliente.GetRG())

        DataReader = Command.ExecuteReader()

        If Not DataReader.HasRows Then
            Cliente = Nothing
        Else
            While DataReader.Read()
                Cliente.SetNome(DataReader("Nome"))
                Cliente.SetEndereço(DataReader("Endereco"))
                Cliente.SetDataDeNascimento(Date.ParseExact(DataReader("DataDeNascimento"), "dd/mm/yyyy", CultureInfo.InvariantCulture))
            End While
        End If

        DataReader.Close()
        Disconnect(Connection)

        Return Cliente
    End Function

    Public Function GetAllCliente() As List(Of Cliente)
        Dim Connection As SQLiteConnection = Connect()
        Dim Command As SQLiteCommand = Connection.CreateCommand()
        Dim DataReader As SQLiteDataReader
        Dim Clientes As New List(Of Cliente)

        Command.CommandText = "SELECT Nome, Endereco, RG, DataDeNascimento FROM Cliente;"

        DataReader = Command.ExecuteReader()

        While DataReader.Read()
            Dim Cliente As New Cliente(DataReader("Nome"), DataReader("Endereco"), DataReader("RG"), Date.ParseExact(DataReader("DataDeNascimento"), "dd/mm/yyyy", CultureInfo.InvariantCulture))
            Clientes.Add(Cliente)
        End While

        DataReader.Close()
        Disconnect(Connection)

        Return Clientes
    End Function
End Class