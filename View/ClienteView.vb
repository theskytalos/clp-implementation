Imports System.Text
Public Class ClienteView
    Public Shared Sub Menu(Optional ByVal Message As String = "")
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.WriteLine("1. Cadastrar")
        Console.WriteLine("2. Editar")
        Console.WriteLine("3. Remover")
        Console.WriteLine("4. Pesquisar")
        Console.WriteLine("5. Listar Todos")
        Console.WriteLine("6. Voltar")
        Console.WriteLine()

        Console.Write(">> ")
        Dim ConsoleInput = Console.ReadLine().Trim()

        Select Case ConsoleInput
            Case "1"
                Menu(Create())
            Case "2"
                Menu(Edit())
            Case "3"
                Menu(Remove())
            Case "4"
                Menu(Search())
            Case "5"
                Menu(Enumerate())
            Case "6"

            Case Else
                MainMenu("Input Inválido")
        End Select
    End Sub

    Private Shared Function Create(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o nome do cliente: ")
        Dim Nome = Console.ReadLine()
        Console.WriteLine()

        Console.Write("Insira o endereço do cliente: ")
        Dim Endereço = Console.ReadLine()
        Console.WriteLine()

        Console.Write("Insira o RG do cliente: ")
        Dim RG = Console.ReadLine()
        Console.WriteLine()

        Console.WriteLine("Insira a data de nascimento do cliente: ")
        Dim DataDeNascimento = Console.ReadLine()

        Dim ClienteController As New ClienteController()

        Try
            If ClienteController.CreateCliente(Nome, Endereço, RG, DataDeNascimento) Then
                Return "Cliente cadastrado com sucesso."
            Else
                Return "Não foi possível cadastrar o cliente."
            End If
        Catch ex As Exception
            Create(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function
    Private Shared Function Edit(Optional ByVal Message As String = "") As String
        Return "Não implementado"
    End Function

    Private Shared Function Remove(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o RG do cliente: ")
        Dim RG = Console.ReadLine()

        Dim ClienteController As New ClienteController()

        Try
            If ClienteController.RemoveCliente(RG) Then
                Return "Cliente removido com sucesso."
            Else
                Return "Não foi possível remover o cliente."
            End If
        Catch ex As Exception
            Remove(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function

    Private Shared Function Search(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o RG do cliente: ")
        Dim RG = Console.ReadLine()

        Dim ClienteController As New ClienteController()
        Dim Cliente As Cliente

        Try
            Cliente = ClienteController.GetCliente(RG)
            If Cliente Is Nothing Then
                Return "Não existe um cliente cadastrado com o RG '" + RG + "'."
            Else
                Return Cliente.ToString()
            End If
        Catch ex As Exception
            Search(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function

    Private Shared Function Enumerate(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Dim ClienteController As New ClienteController()
        Dim Clientes As List(Of Cliente)

        Try
            Clientes = ClienteController.GetAllCliente()

            If Clientes.Count = 0 Then
                Return "Não há nenhum produto a ser mostrado."
            Else
                Dim SB As New StringBuilder()
                For Each Cliente In Clientes
                    SB.Append(Cliente.ToString()).Append(vbCrLf)
                Next
                Return SB.ToString()
            End If
        Catch ex As Exception
            Enumerate(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function
End Class
