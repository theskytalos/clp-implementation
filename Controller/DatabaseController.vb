Imports System.Data
Imports System.Data.SQLite
Public Class DatabaseController

    Const DatabaseName As String = "clp-db.db3"

    Public Shared Sub CreateDatabase()
        If Not IO.File.Exists(DatabaseName) Then
            SQLiteConnection.CreateFile(DatabaseName)

            Dim DatabaseCreate As String = String.Empty

            DatabaseCreate &= "CREATE TABLE Cliente (Nome VARCHAR(128), Endereco VARCHAR(256), RG VARCHAR(16) PRIMARY KEY, DataDeNascimento TEXT);"
            DatabaseCreate &= "CREATE TABLE Produto (Codigo INTEGER PRIMARY KEY AUTOINCREMENT, Nome VARCHAR(128), Valor DECIMAL(10,2));"
            DatabaseCreate &= "CREATE TABLE Venda (Numero INTEGER PRIMARY KEY AUTOINCREMENT, Data TEXT, RG_FK INTEGER);"
            DatabaseCreate &= "CREATE TABLE ItemVenda (Codigo_FK INTEGER, Valor DECIMAL(10,2), Quantidade INTEGER);"

            Dim Command As SQLiteCommand
            Dim Connection As SQLiteConnection = Connect()

            Command = Connection.CreateCommand()
            Command.CommandText = DatabaseCreate

            Command.ExecuteNonQuery()

            Disconnect(Connection)
        End If
    End Sub

    Protected Shared Function Connect() As SQLiteConnection
        Dim Connection As SQLiteConnection

        Connection = New SQLiteConnection(String.Format("Data Source={0};Version=3;", DatabaseName))

        If Connection.State = ConnectionState.Closed Then
            Connection.Open()
        End If

        Return Connection
    End Function

    Protected Shared Function Disconnect(ByRef Connection As SQLiteConnection) As Boolean
        Return IIf(Connection.State = ConnectionState.Closed, False, True)
    End Function
End Class
