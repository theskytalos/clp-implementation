Public Class ConsoleView
    Public Shared Sub ShowError(ByVal ConsoleError As String)
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write("Erro: ")
        Console.ResetColor()
        Console.WriteLine(ConsoleError)
    End Sub

    Public Shared Function ShowMainMenu(Optional ByVal ConsoleError As String = "") As Integer
        Console.Clear()

        ShowHeader("Sistema de Registro Acadêmico")

        If ConsoleError <> String.Empty Then
            ShowError(ConsoleError)
            Console.WriteLine(String.Empty)
        End If
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("1. ")
        Console.ResetColor()
        Console.WriteLine("Cliente")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("2. ")
        Console.ResetColor()
        Console.WriteLine("Produto")
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write("3. ")
        Console.ResetColor()
        Console.WriteLine("Venda")
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write("4. ")
        Console.ResetColor()
        Console.WriteLine("Sair")

        Console.WriteLine(String.Empty)
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.Write(">> ")
        Console.ResetColor()

        Dim Input As String = Console.ReadLine()
        Dim Result As Integer

        If Integer.TryParse(Input, Result) Then
            If Result < 1 Or Result > 4 Then
                ShowMainMenu("Opção Inválida.")
            End If

            Return Result
        Else
            ShowMainMenu("São aceitos somente números.")
        End If
    End Function

    Public Shared Sub ShowHeader(ByVal Header As String)
        Dim LineBuffer = String.Empty
        LineBuffer += "+--"
        LineBuffer += StrDup(Header.Length, "-")
        LineBuffer += "--+"

        Console.WriteLine(LineBuffer)
        LineBuffer = String.Empty

        LineBuffer += "|  "

        Console.Write(LineBuffer)
        Console.ForegroundColor = ConsoleColor.Blue
        Console.Write(Header)
        Console.ResetColor()

        Console.WriteLine("  |")

        LineBuffer = String.Empty
        LineBuffer += "+--"
        LineBuffer += StrDup(Header.Length, "-")
        LineBuffer += "--+"

        Console.WriteLine(LineBuffer & vbCrLf)
    End Sub

    Public Shared Sub ShowTable(ByVal Header() As String, ByVal Data(,) As String)
        If Header.Length <> Data.GetLength(1) Then
            Throw New Exception("O tamanho do cabeçalho não coincide com as colunas dos dados.")
        End If

        Dim LineBuffer = String.Empty

        For I As Integer = 0 To Header.Length - 1 Step 1
            If I = 0 Then
                LineBuffer += "+--"
            End If

            LineBuffer += StrDup(Header(I).Length, "-")

            If I <> Header.Length - 1 Then
                LineBuffer += "--+--"
            End If
        Next

        LineBuffer += "--+"

        Console.WriteLine(LineBuffer)
        LineBuffer = String.Empty

        For I As Integer = 0 To Header.Length - 1 Step 1
            If I = 0 Then
                LineBuffer += "|  "
            End If

            Console.Write(LineBuffer)
            Console.ForegroundColor = ConsoleColor.Blue
            Console.Write(Header(I))
            Console.ResetColor()
            LineBuffer = String.Empty

            If I <> Header.Length - 1 Then
                LineBuffer += "  |  "
            End If
        Next

        LineBuffer += "  |"

        Console.WriteLine(LineBuffer)
        LineBuffer = String.Empty

        For I As Integer = 0 To Header.Length - 1 Step 1
            If I = 0 Then
                LineBuffer += "+--"
            End If

            LineBuffer += StrDup(Header(I).Length, "-")

            If I <> Header.Length - 1 Then
                LineBuffer += "--+--"
            End If
        Next

        LineBuffer += "--+"

        Console.WriteLine(LineBuffer)

        For I As Integer = 0 To Data.GetLength(0) - 1 Step 1

            LineBuffer = String.Empty
            For J As Integer = 0 To Data.GetLength(1) - 1 Step 1
                If I = 0 Then
                    LineBuffer += "|  "
                End If

                LineBuffer += Data(I, J)

                If I <> Header.Length - 1 Then
                    LineBuffer += "  |  "
                End If
            Next

            LineBuffer += "  |"

            Console.WriteLine(LineBuffer)
        Next

    End Sub
End Class
