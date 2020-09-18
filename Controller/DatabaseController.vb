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
            DatabaseCreate &= "CREATE TABLE Venda (Numero INTEGER PRIMARY KEY AUTOINCREMENT, Data TEXT, RG_FK INTEGER, FOREIGN KEY (RG_FK) REFERENCES Cliente (RG));"
            DatabaseCreate &= "CREATE TABLE ItemVenda (ID INTEGER PRIMARY KEY AUTOINCREMENT, Codigo_FK INTEGER, Numero_FK INTEGER, Valor DECIMAL(10,2), Quantidade INTEGER, FOREIGN KEY (Codigo_FK) REFERENCES Produto (Codigo), FOREIGN KEY (Numero_FK) REFERENCES Venda (Numero_FK));"

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

    Protected Shared Sub Disconnect(ByRef Connection As SQLiteConnection)
        If Connection.State <> ConnectionState.Closed Then
            Connection.Close()
        End If
    End Sub
End Class
