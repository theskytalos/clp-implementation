Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class ClienteController
    Public Function CreateCliente(ByVal Nome As String, ByVal Endereço As String, ByVal RG As String, ByVal DataDeNascimento As String) As Boolean
        If Nome.Trim.Length < 3 Then
            Throw New Exception("O Nome do cliente não pode ser menor que 3 caracteres.")
        End If

        If Nome.Trim.Length > 128 Then
            Throw New Exception("O Nome do cliente não pode ser maior que 128 caracteres.")
        End If

        If Regex.IsMatch(Nome.Trim, "^[0-9]+$") Then
            Throw New Exception("O Nome do cliente não pode conter números.")
        End If

        If Endereço.Trim.Length < 5 Then
            Throw New Exception("O Endereço do cliente não pode ser menor que 5 caracteres.")
        End If

        If Endereço.Trim.Length > 256 Then
            Throw New Exception("O Endereço do cliente não pode ser maior que 256 caracteres.")
        End If

        If RG.Trim.Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim.Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Cliente As New Cliente()
        Dim ClienteDAO As New ClienteDAO()

        Cliente.SetNome(Nome)
        Cliente.SetEndereço(Endereço)
        Cliente.SetRG(RG)
        Cliente.SetDataDeNascimento(Date.ParseExact(DataDeNascimento, "d/m/Y", CultureInfo.InvariantCulture))

        Return ClienteDAO.CreateCliente(Cliente)
    End Function

    Public Function EditCliente(ByVal Nome As String, ByVal Endereço As String, ByVal RG As String, ByVal DataDeNascimento As String) As Boolean

    End Function

    Public Function RemoveCliente(ByVal RG As String) As Boolean
        If RG.Trim.Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim.Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Cliente As New Cliente(RG)
        Dim ClienteDAO As New ClienteDAO()

        Return ClienteDAO.RemoveCliente(Cliente)
    End Function

    Public Function GetCliente(ByVal RG As String) As Cliente
        If RG.Trim.Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim.Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Cliente As New Cliente(RG)
        Dim ClienteDAO As New ClienteDAO()

        ClienteDAO.GetCliente(Cliente) 'Retorna cliente por referência

        Return Cliente
    End Function

    Public Function GetAllCliente() As List(Of Cliente)
        Dim ClienteDAO As New ClienteDAO()

        Return ClienteDAO.GetAllCliente()
    End Function
End Class
