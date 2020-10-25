Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class ClienteController
    Public Function CreateCliente(ByVal Nome As String, ByVal Endereço As String, ByVal RG As String, ByVal DataDeNascimento As String) As Boolean
        If Nome.Trim().Length < 3 Then
            Throw New Exception("O Nome do cliente não pode ser menor que 3 caracteres.")
        End If

        If Nome.Trim().Length > 128 Then
            Throw New Exception("O Nome do cliente não pode ser maior que 128 caracteres.")
        End If

        If Regex.IsMatch(Nome.Trim(), "^[0-9]+$") Then
            Throw New Exception("O Nome do cliente não pode conter números.")
        End If

        If Endereço.Trim().Length < 5 Then
            Throw New Exception("O Endereço do cliente não pode ser menor que 5 caracteres.")
        End If

        If Endereço.Trim().Length > 256 Then
            Throw New Exception("O Endereço do cliente não pode ser maior que 256 caracteres.")
        End If

        If RG.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        If Not Regex.IsMatch(DataDeNascimento.Trim(), "^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$") Then
            Throw New Exception("A Data de Nascimento do cliente é inválida.")
        End If

        Dim Cliente As New Cliente()
        Dim ClienteDAO As New ClienteDAO()

        Cliente.SetNome(Nome.Trim())
        Cliente.SetEndereço(Endereço.Trim())
        Cliente.SetRG(RG.Trim())
        Cliente.SetDataDeNascimento(Date.ParseExact(DataDeNascimento.Trim(), "dd/mm/yyyy", CultureInfo.InvariantCulture))

        If Not IsNothing(GetCliente(RG)) Then
            Throw New Exception("Já existe um cliente cadastrado com o RG '" + RG + "'")
        End If

        Return ClienteDAO.CreateCliente(Cliente)
    End Function

    Public Function EditCliente(ByVal Nome As String, ByVal Endereço As String, ByVal RG As String, ByVal DataDeNascimento As String) As Boolean
        If Nome.Trim().Length < 3 Then
            Throw New Exception("O Nome do cliente não pode ser menor que 3 caracteres.")
        End If

        If Nome.Trim().Length > 128 Then
            Throw New Exception("O Nome do cliente não pode ser maior que 128 caracteres.")
        End If

        If Regex.IsMatch(Nome.Trim(), "^[0-9]+$") Then
            Throw New Exception("O Nome do cliente não pode conter números.")
        End If

        If Endereço.Trim().Length < 5 Then
            Throw New Exception("O Endereço do cliente não pode ser menor que 5 caracteres.")
        End If

        If Endereço.Trim().Length > 256 Then
            Throw New Exception("O Endereço do cliente não pode ser maior que 256 caracteres.")
        End If

        If RG.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        If Not Regex.IsMatch(DataDeNascimento.Trim(), "^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$") Then
            Throw New Exception("A Data de Nascimento do cliente é inválida.")
        End If

        Dim Cliente As New Cliente()
        Dim ClienteDAO As New ClienteDAO()

        Cliente.SetNome(Nome.Trim())
        Cliente.SetEndereço(Endereço.Trim())
        Cliente.SetRG(RG.Trim())
        Cliente.SetDataDeNascimento(Date.ParseExact(DataDeNascimento.Trim(), "dd/mm/yyyy", CultureInfo.InvariantCulture))

        If IsNothing(GetCliente(RG)) Then
            Throw New Exception("Não existe um cliente cadastrado com o RG '" + RG + "'")
        End If

        Return ClienteDAO.EditCliente(Cliente)
    End Function

    Public Function RemoveCliente(ByVal RG As String) As Boolean
        If RG.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Cliente As New Cliente(RG)
        Dim ClienteDAO As New ClienteDAO()

        If IsNothing(ClienteDAO.GetCliente(Cliente)) Then
            Throw New Exception("Não existe um cliente cadastrado com o RG '" + Cliente.GetRG() + "'")
        End If

        Return ClienteDAO.RemoveCliente(Cliente)
    End Function

    Public Function GetCliente(ByVal RG As String) As Cliente
        If RG.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If RG.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Cliente As New Cliente(RG)
        Dim ClienteDAO As New ClienteDAO()

        Return ClienteDAO.GetCliente(Cliente)
    End Function

    Public Function GetAllCliente() As List(Of Cliente)
        Dim ClienteDAO As New ClienteDAO()

        Return ClienteDAO.GetAllCliente()
    End Function
End Class
