Public Class Cliente : Inherits Pessoa
    Private RG As String
    Private DataDeNascimento As Date

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal RG As String)
        Me.RG = RG
    End Sub

    Public Sub New(ByVal Nome As String, ByVal Endereço As String, ByVal RG As String, ByVal DataDeNascimento As Date)
        MyBase.New(Nome, Endereço)
        Me.RG = RG
        Me.DataDeNascimento = DataDeNascimento
    End Sub

    Public Function GetRG() As String
        Return RG
    End Function

    Public Sub SetRG(ByVal RG)
        Me.RG = RG
    End Sub

    Public Function GetDataDeNascimento() As Date
        Return DataDeNascimento
    End Function

    Public Sub SetDataDeNascimento(ByVal DataDeNascimento)
        Me.DataDeNascimento = DataDeNascimento
    End Sub

    Public Overrides Function ToString() As String
        Return MyBase.ToString() + "; RG: " + RG + "; Data de Nascimento: " + DataDeNascimento.ToString("dd/mm/yyyy")
    End Function
End Class
