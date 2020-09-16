Public Class ClienteView
    Public Shared Sub ShowMenu(Optional ByVal ConsoleError As String = "")
        Console.Clear()
        If ConsoleError <> String.Empty Then
            ConsoleView.ShowError(ConsoleError)
            Console.WriteLine(String.Empty)
        End If
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("1. ")
        Console.ResetColor()
        Console.WriteLine("Cadastrar Novo Cliente")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("2. ")
        Console.ResetColor()
        Console.WriteLine("Editar um Cliente")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("3. ")
        Console.ResetColor()
        Console.WriteLine("Remover um Cliente")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("4. ")
        Console.ResetColor()
        Console.WriteLine("Buscar um Cliente")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("5. ")
        Console.ResetColor()
        Console.WriteLine("Listar Todos Clientes")
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write("6. ")
        Console.ResetColor()
        Console.WriteLine("Voltar")

        Console.WriteLine(String.Empty)
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.Write(">> ")
        Console.ResetColor()

        Dim Input As String = Console.ReadLine()
        Dim Result As Integer

        If Integer.TryParse(Input, Result) Then
            If Result < 1 Or Result > 6 Then
                ShowMenu("Opção Inválida.")
            End If

            Select Case Result
                Case 1
                    CreateClienteMenu()
                Case 2
                    EditClienteMenu()
                Case 3
                    RemoveClienteMenu()
                Case 4
                    GetClienteMenu()
                Case 5
                    ConsoleView.ShowTable({"Nome", "Endereço", "RG", "Data de Nascimento"}, {{"Junior", "R. Capinzal", "666666", "03/08/1998"}})
                Case 6
                    Return
            End Select
            ShowMenu()
        Else
            ShowMenu("São aceitos somente números.")
        End If
    End Sub

    Protected Shared Sub CreateClienteMenu()
        Console.Clear()
    End Sub

    Protected Shared Sub EditClienteMenu()

    End Sub

    Protected Shared Sub RemoveClienteMenu()

    End Sub

    Protected Shared Sub GetClienteMenu()

    End Sub

End Class
