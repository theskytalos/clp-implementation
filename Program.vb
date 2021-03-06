Module Program

    Sub Main(args As String())
        Console.Title = "Parte 2 Trabalho de CLP - Implementação"

        Dim DatabaseController As New DatabaseController

        Try
            DatabaseController.CreateDatabase()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        MainMenu()

        'TestClient()
        'TestProduto()
        'TestVenda()
    End Sub

    Sub MainMenu(Optional ByVal Message As String = "")
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.WriteLine("1. Cliente")
        Console.WriteLine("2. Produto")
        Console.WriteLine("3. Venda")
        Console.WriteLine("4. Sair")
        Console.WriteLine()

        Console.Write(">> ")
        Dim ConsoleInput = Console.ReadLine().Trim()

        Select Case ConsoleInput
            Case "1"
                ClienteView.Menu()
                MainMenu()
            Case "2"
                ProdutoView.Menu()
                MainMenu()
            Case "3"
                VendaView.Menu()
                MainMenu()
            Case "4"
            Case Else
                MainMenu("Input Inválido")
        End Select
    End Sub

    Sub TestClient()
        Console.WriteLine("#### Teste Cliente #####")

        Dim ClienteController As New ClienteController()

        Try
            Console.WriteLine(ClienteController.CreateCliente("Junior", "R. Capinzal", "666777999", "03/08/1998"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ClienteController.CreateCliente("Canguiço", "R. do Peru", "666777991", "28/02/1995"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ClienteController.GetCliente("666777999").ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Dim AllCliente = ClienteController.GetAllCliente()
            For Each Cliente In AllCliente
                Console.WriteLine(Cliente.ToString())
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ClienteController.RemoveCliente("666777999"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub TestProduto()
        Console.WriteLine("#### Teste Produto #####")

        Dim ProdutoController As New ProdutoController()

        Try
            Console.WriteLine(ProdutoController.CreateProduto("Playstation 5 All Digital", "4499,99"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ProdutoController.CreateProduto("Playstation 5", "4999,99"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ProdutoController.GetProduto("1").ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Dim AllProduto = ProdutoController.GetAllProduto()

            For Each Produto In AllProduto
                Console.WriteLine(Produto.ToString())
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ProdutoController.RemoveProduto("50").ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(ProdutoController.RemoveProduto("2").ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub TestVenda()
        Console.WriteLine("#### Teste Venda #####")

        Dim VendaController As New VendaController()

        Try
            Dim Items As New Dictionary(Of String, String)
            Items.Add(1, 300)
            Items.Add(2, 50)
            Console.WriteLine(VendaController.CreateVenda("18/09/2020", "666777991", Items))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Dim Items As New Dictionary(Of String, String)
            Items.Add(1, 300)
            Console.WriteLine(VendaController.CreateVenda("18/09/2020", "666777991", Items))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try


        Try
            Console.WriteLine(VendaController.GetVenda("1").ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Dim AllVenda = VendaController.GetAllVenda()

            For Each Venda In AllVenda
                Console.WriteLine(Venda.ToString())
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Try
            Console.WriteLine(VendaController.RemoveVenda("1"))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Module
