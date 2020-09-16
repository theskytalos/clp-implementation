Module Program
    Sub Main(args As String())
        Console.Title = "Implementação Trabalho de CLP"

        Dim DatabaseController As New DatabaseController

        Try
            DatabaseController.CreateDatabase()

            While True
                Dim Result As Integer = ConsoleView.ShowMainMenu()

                Select Case Result
                    Case 1
                        ClienteView.ShowMenu()
                    Case 2
                        ProdutoView.ShowMenu()
                    Case 3
                        VendaView.ShowMenu()
                    Case 4
                        Environment.Exit(0)
                End Select
            End While
        Catch ex As Exception
            ConsoleView.ShowError(ex.Message)
        End Try
    End Sub
End Module
