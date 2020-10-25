Imports System.Text.RegularExpressions

Public Class ProdutoController
    Public Function CreateProduto(ByVal Nome As String, ByVal Valor As String) As Boolean
        If Nome.Trim().Length = 0 Then
            Throw New Exception("O Nome do produto não pode ser vazio.")
        End If

        If Nome.Trim().Length > 128 Then
            Throw New Exception("O Nome do produto não pode exceder os 128 caracteres.")
        End If

        If IsNumeric(Nome.Trim()) Then
            Throw New Exception("O Nome do produto não pode ser composto apenas por números.")
        End If

        If Not Regex.IsMatch(Valor.Trim(), "^?([0-9]{1,3}.([0-9]{3},)*[0-9]{3}|[0-9]+)(,[0-9][0-9])?$") Then
            Throw New Exception("O Valor do produto precisa ter formato de moeda.")
        End If

        Dim Produto As New Produto()
        Dim ProdutoDAO As New ProdutoDAO()

        Produto.SetNome(Nome.Trim())
        Produto.SetValor(Decimal.Parse(Valor.Trim()))

        If ProdutoDAO.CheckForName(Produto) Then
            Throw New Exception("Já existe um produto cadastrado com o nome '" + Produto.GetNome() + "'")
        End If

        Return ProdutoDAO.CreateProduto(Produto)
    End Function

    Public Function EditProduto(ByVal Código As String, ByVal Nome As String, ByVal Valor As String) As Boolean
        If Not IsNumeric(Código.Trim()) Then
            Throw New Exception("O Código do produto deve ser númerico.")
        End If

        If Nome.Trim().Length = 0 Then
            Throw New Exception("O Nome do produto não pode ser vazio.")
        End If

        If Nome.Trim().Length > 128 Then
            Throw New Exception("O Nome do produto não pode exceder os 128 caracteres.")
        End If

        If IsNumeric(Nome.Trim()) Then
            Throw New Exception("O Nome do produto não pode ser composto apenas por números.")
        End If

        If Not Regex.IsMatch(Valor.Trim(), "^?([0-9]{1,3}.([0-9]{3},)*[0-9]{3}|[0-9]+)(,[0-9][0-9])?$") Then
            Throw New Exception("O Valor do produto precisa ter formato de moeda.")
        End If

        Dim Produto As New Produto()
        Dim ProdutoDAO As New ProdutoDAO()

        Produto.SetCódigo(Integer.Parse(Código.Trim()))
        Produto.SetNome(Nome.Trim())
        Produto.SetValor(Decimal.Parse(Valor.Trim()))

        If IsNothing(GetProduto(Código)) Then
            Throw New Exception("Não existe um produto cadastrado com o Código '" + Código.ToString() + "'")
        End If

        Return ProdutoDAO.EditProduto(Produto)
    End Function

    Public Function RemoveProduto(ByVal Código As String) As Boolean
        If Not IsNumeric(Código.Trim()) Then
            Throw New Exception("O Código do produto deve ser númerico.")
        End If

        Dim Produto As New Produto(Integer.Parse(Código.Trim()))
        Dim ProdutoDAO As New ProdutoDAO()

        If IsNothing(GetProduto(Código)) Then
            Throw New Exception("Não existe um produto cadastrado com o Código '" + Código.ToString() + "'")
        End If

        Return ProdutoDAO.RemoveProduto(Produto)
    End Function

    Public Function GetProduto(ByVal Código As String) As Produto
        If Not IsNumeric(Código.Trim()) Then
            Throw New Exception("O Código do produto deve ser númerico.")
        End If

        Dim Produto As New Produto(Integer.Parse(Código.Trim()))
        Dim ProdutoDAO As New ProdutoDAO()

        Produto = ProdutoDAO.GetProduto(Produto)

        Return Produto
    End Function

    Public Function GetAllProduto() As List(Of Produto)
        Dim ProdutoDAO As New ProdutoDAO()

        Return ProdutoDAO.GetAllProduto()
    End Function
End Class
