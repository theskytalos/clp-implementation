Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class VendaController
    Public Function CreateVenda(ByVal Data As String, ByVal Cliente As String, ByVal Itens As Dictionary(Of String, String)) As Boolean
        If Not Regex.IsMatch(Data.Trim(), "^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$") Then
            Throw New Exception("A Data da venda é inválida.")
        End If

        If Cliente.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If Cliente.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Venda As New Venda()
        Dim VendaDAO As New VendaDAO()
        Dim ProdutoDAO As New ProdutoDAO()
        Dim ClienteDAO As New ClienteDAO()
        Dim ItemList As New List(Of ItemVenda)

        If IsNothing(ClienteDAO.GetCliente(New Cliente(Integer.Parse(Cliente.Trim())))) Then
            Throw New Exception("Não existe um cliente com este RG.")
        End If

        If Itens.Count = 0 Then
            Throw New Exception("Não é possível realizar uma venda sem itens.")
        End If

        For Each Item In Itens
            If Not IsNumeric(Item.Key.Trim()) Then
                Throw New Exception("O Código '" + Item.Key.Trim() + "' de produto deve ser um número natural.")
            End If

            If Not IsNumeric(Item.Value.Trim()) Then
                Throw New Exception("A Quantidade do produto com o Código '" + Item.Key.Trim() + "' deve ser um número natural.")
            End If

            Dim Produto As Produto = ProdutoDAO.GetProduto(New Produto(Item.Key.Trim()))

            If IsNothing(Produto) Then
                Throw New Exception("Não existe um produto com o Código '" + Item.Key.Trim() + "'.")
            End If

            ItemList.Add(New ItemVenda(New Produto(Integer.Parse(Item.Key.Trim())), Produto.GetValor(), Integer.Parse(Item.Value.Trim())))
        Next

        Venda.SetData(Date.ParseExact(Data.Trim(), "dd/mm/yyyy", CultureInfo.InvariantCulture))
        Venda.SetCliente(New Cliente(Integer.Parse(Cliente.Trim())))
        Venda.SetItens(ItemList)

        Return VendaDAO.CreateVenda(Venda)
    End Function

    Public Function EditVenda(ByVal Número As String, ByVal Data As String, ByVal Cliente As String) As Boolean
        If Not IsNumeric(Número.Trim()) Then
            Throw New Exception("O Número da venda deve ser númerico.")
        End If

        If Not Regex.IsMatch(Data.Trim(), "^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$") Then
            Throw New Exception("A Data da venda é inválida.")
        End If

        If Cliente.Trim().Length < 8 Then
            Throw New Exception("O RG do cliente não pode ser menor que 8 caracteres.")
        End If

        If Cliente.Trim().Length > 16 Then
            Throw New Exception("O RG do cliente não pode ser maior que 16 caracteres.")
        End If

        Dim Venda As New Venda()
        Dim VendaDAO As New VendaDAO()
        Dim ClienteController As New ClienteController()

        If IsNothing(ClienteController.GetCliente(Integer.Parse(Cliente.Trim()))) Then
            Throw New Exception("Não existe um cliente com este RG.")
        End If

        If IsNothing(GetVenda(Número)) Then
            Throw New Exception("Não existe uma venda cadastrada com o Número '" + Número.ToString() + "'")
        End If

        Venda.SetNúmero(Integer.Parse(Número.Trim()))
        Venda.SetData(Date.ParseExact(Data.Trim(), "dd/mm/yyyy", CultureInfo.InvariantCulture))
        Venda.SetCliente(New Cliente(Integer.Parse(Cliente.Trim())))

        Return VendaDAO.EditVenda(Venda)
    End Function

    Public Function RemoveVenda(ByVal Número As String) As Boolean
        If Not IsNumeric(Número.Trim()) Then
            Throw New Exception("O Número da venda deve ser númerico.")
        End If

        Dim Venda As New Venda(Integer.Parse(Número.Trim()))
        Dim VendaDAO As New VendaDAO()

        If IsNothing(VendaDAO.GetVenda(Venda)) Then
            Throw New Exception("Não existe uma venda cadastrada com o Número '" + Número.ToString() + "'")
        End If

        Return VendaDAO.RemoveVenda(Venda)
    End Function

    Public Function GetVenda(ByVal Número As String) As Venda
        If Not IsNumeric(Número.Trim()) Then
            Throw New Exception("O Número da venda deve ser númerico.")
        End If

        Dim Venda As New Venda(Integer.Parse(Número.Trim()))
        Dim VendaDAO As New VendaDAO()
        Dim ItemVendaDAO As New ItemVendaDAO()

        Venda = VendaDAO.GetVenda(Venda)

        If Not IsNothing(Venda) Then
            Venda.SetItens(ItemVendaDAO.GetAllItemVendaByVenda(Venda))
        End If

        Return Venda
    End Function

    Public Function GetAllVenda() As List(Of Venda)
        Dim VendaDAO As New VendaDAO()
        Dim ItemVendaDAO As New ItemVendaDAO()

        Dim AllVenda = VendaDAO.GetAllVenda()

        For Each Venda In AllVenda
            If Not IsNothing(Venda) Then
                Venda.SetItens(ItemVendaDAO.GetAllItemVendaByVenda(Venda))
            End If
        Next

        Return AllVenda
    End Function
End Class
