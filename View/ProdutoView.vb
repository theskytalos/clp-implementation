Imports System.Text

Public Class ProdutoView
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
                MainMenu("Input Inválido.")
        End Select
    End Sub

    Private Shared Function Create(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o nome do produto: ")
        Dim Nome = Console.ReadLine()
        Console.WriteLine()

        Console.Write("Insira o valor do produto: ")
        Dim Valor = Console.ReadLine()

        Dim ProdutoController As New ProdutoController()

        Try
            If ProdutoController.CreateProduto(Nome, Valor) Then
                Return "Produto cadastrado com sucesso."
            Else
                Return "Não foi possível cadastrar o produto."
            End If
        Catch ex As Exception
            Create(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function

    Private Shared Function Edit(Optional ByVal Message As String = "") As String
        Return "Não implementado."
    End Function

    Private Shared Function Remove(Optional ByVal Message As String = "") As String
        Console.Clear()

        If Not Message = String.Empty Then
            Console.WriteLine(Message)
            Console.WriteLine()
        End If

        Console.Write("Insira o código do produto: ")
        Dim Código = Console.ReadLine()

        Dim ProdutoController As New ProdutoController()

        Try
            If ProdutoController.RemoveProduto(Código) Then
                Return "Produto removido com sucesso."
            Else
                Return "Não foi possível remover o produto."
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

        Console.Write("Insira o código do produto: ")
        Dim Código = Console.ReadLine()

        Dim ProdutoController As New ProdutoController()
        Dim Produto As Produto

        Try
            Produto = ProdutoController.GetProduto(Código)
            If Produto Is Nothing Then
                Return "Não existe um produto cadastrado com o Código '" + Código.ToString() + "'."
            Else
                Return Produto.ToString()
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

        Dim ProdutoController As New ProdutoController()
        Dim Produtos As List(Of Produto)

        Try
            Produtos = ProdutoController.GetAllProduto()

            If Produtos.Count = 0 Then
                Return "Não há nenhum produto para ser mostrado."
            Else
                Dim SB As New StringBuilder()
                For Each Produto In Produtos
                    SB.Append(Produto.ToString()).Append(vbCrLf)
                Next
                Return SB.ToString()
            End If
        Catch ex As Exception
            Enumerate(ex.Message)
        End Try

        Return "Isso nunca vai ser printado."
    End Function
End Class
