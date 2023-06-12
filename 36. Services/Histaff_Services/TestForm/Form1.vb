Public Class Form1
    Dim dTable As New DataTable
    Private Sub SetMaterial_Type()

        'Dim rstData As New DataSet
        'Dim dTable As New DataTable("com")
        Dim r As New Random
        Dim dRow As DataRow

        dTable.Columns.Add("SHL", GetType(System.Double))
        dTable.Columns.Add("Qtty", GetType(System.Double))
        dTable.Columns.Add("Price", GetType(System.Double))
        dTable.Columns.Add("Name", GetType(System.String))
        dTable.PrimaryKey = New DataColumn() {dTable.Columns("SHL")}

        For i = 0 To 10000
            dRow = dTable.NewRow
            dRow("SHL") = i
            dRow("Qtty") = 10 + i
            dRow("Price") = 100 + i
            If i Mod 2 = 0 Then
                dRow("Name") = "nguyen" & i
            Else
                dRow("Name") = "le" & i
            End If

            dTable.Rows.Add(dRow)
        Next


        
        'Try
        '    With MyGridView1
        '        .DataSource = dTable
        '    End With
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetMaterial_Type()
    End Sub

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            With MyGridView1
                .MyGrid.DisplayLayout.LoadStyle = Infragistics.Win.UltraWinGrid.LoadStyle.LoadOnDemand
                .DataSource = dTable
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
