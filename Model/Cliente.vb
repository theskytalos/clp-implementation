Public Class Cliente : Inherits Pessoa
    Private RG As String
    Private DataDeNascimento As Date

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
End Class
