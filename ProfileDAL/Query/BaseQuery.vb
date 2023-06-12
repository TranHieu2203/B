Public Class BaseQuery
    Public Property Ids As List(Of String)
    Public Property Status As List(Of String)
    Public Property ID As Decimal?
    Public Sub New()
        Ids = New List(Of String)
        Status = New List(Of String)
    End Sub
End Class
