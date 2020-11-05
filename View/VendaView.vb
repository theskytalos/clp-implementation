Imports System.Text

Public Class VendaView
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
        Console.WriteLine("5. Listas Todos")
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
                MainMenu("Input Inválido.")
        End Select
    End Sub

    Private Shared Function Create(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o RG do cliente: ")
        Dim RG = Console.ReadLine()
        Dim Itens As New Dictionary(Of String, String)

        While True
            Console.Write("Insira o código do item (0 para sair): ")
            Dim Código = Console.ReadLine()

            If Código = 0 Then
                Exit While
            End If

            Console.Write("Insira a quantidade do item: ")
            Dim Quantidade = Console.ReadLine()

            Itens.Add(Código, Quantidade)
        End While

        Dim VendaController As New VendaController()

        Try
            If VendaController.CreateVenda(DateTime.Now.ToShortDateString(), RG, Itens) Then
                Return "Venda realizada com sucesso."
            Else
                Return "Não foi possível realizar a venda."
            End If
        Catch ex As Exception
            Create(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function

    Private Shared Function Edit(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o número da venda: ")
        Dim Número = Console.ReadLine()

        Dim VendaController As New VendaController()
        Dim ClienteController As New ClienteController()
        Dim Venda As Venda
        Dim ClienteRG As String = ""
        Dim Data As Date

        Try
            Venda = VendaController.GetVenda(Número)

            If Venda Is Nothing Then
                Return "Não existe uma venda cadastrada com número '" + Número.ToString() + "'"
            Else
                ClienteRG = Venda.GetCliente().GetRG()
                Data = Venda.GetData()
            End If
        Catch ex As Exception
            Edit(ex.Message)
        End Try

        Console.WriteLine("O que deseja editar?")
        Console.WriteLine()
        Console.WriteLine("1. Data")
        Console.WriteLine("2. Cliente")
        Console.WriteLine("3. Itens")
        Console.WriteLine()

        Console.Write(">> ")
        Dim ConsoleInput = Console.ReadLine()

        Select Case ConsoleInput
            Case "1"
                Console.Write("Digite o nova data: ")
                Data = Console.ReadLine()
            Case "2"
                Console.Write("Digite o RG do novo cliente: ")
                ClienteRG = Console.ReadLine()

                Try
                    If ClienteController.GetCliente(ClienteRG) Is Nothing Then
                        Edit("Não existe um cliente cadastrado com este RG.")
                    End If
                Catch ex As Exception
                    Edit(ex.Message)
                End Try
            Case "3"
                Console.Write("E agora? kkkk")
            Case Else
                Edit("Input inválido.")
        End Select

        Try
            If VendaController.EditVenda(Número, Data, ClienteRG) Then
                Return "Venda editada com sucesso."
            Else
                Return "Não foi possível editar a venda."
            End If
        Catch ex As Exception
            Edit(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function

    Private Shared Function Remove(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o número da venda: ")
        Dim Número = Console.ReadLine()

        Dim VendaController As New VendaController()

        Try
            If VendaController.RemoveVenda(Número) Then
                Return "Venda removida com sucesso."
            Else
                Return "Não foi possível remover a venda."
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

        Console.Write("Insira o número da venda: ")
        Dim Número = Console.ReadLine()

        Dim VendaController As New VendaController()
        Dim Venda As Venda

        Try
            Venda = VendaController.GetVenda(Número)
            If Venda Is Nothing Then
                Return "Não existe uma venda cadastrada com o número '" + Número.ToString() + "'."
            Else
                Return Venda.ToString()
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

        Dim VendaController As New VendaController()
        Dim Vendas As List(Of Venda)

        Try
            Vendas = VendaController.GetAllVenda()

            If Vendas.Count = 0 Then
                Return "Não há vendas para serem mostradas."
            Else
                Dim SB As New StringBuilder()
                For Each Venda In Vendas
                    SB.Append(Venda.ToString()).Append(vbCrLf)
                Next
                Return SB.ToString()
            End If
        Catch ex As Exception
            Enumerate(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function
End Class
