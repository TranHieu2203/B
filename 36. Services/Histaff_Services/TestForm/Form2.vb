Imports Smart.OracleDBServices
Imports Infragistics
Public Class Form2


    Private Sub SetMaterial_Type()
        'Dim idb As New Smart.OracleDBServices.OracleHelper
        ' Dim ds As DataSet

        'Dim rstData As New DataSet
        Dim dTable As New DataTable("com")
        dTable = OracleHelper.ExecuteDataTable("Data Source=stockz_live;User Id=sgo;PWD=sgo", "PKG_COMMON.getInvestorByUser", New Object() {"admin", 1})
        'Dim r As New Random
        'Dim dRow As DataRow
        'dTable.Columns.Add("INVESTORID", GetType(System.Double))
        'dTable.Columns.Add("custodycode", GetType(System.String))
        'dTable.Columns.Add("FullName", GetType(System.String))
        'dTable.PrimaryKey = New DataColumn() {dTable.Columns("INVESTORID")}

        'For i = 0 To 999999
        '    dRow = dTable.NewRow
        '    dRow("INVESTORID") = i
        '    dRow("custodycode") = "053C" & i
        '    dRow("FullName") = i
        '    dTable.Rows.Add(dRow)
        'Next

        Try
            UltraGrid1.DisplayLayout.Bands(0).Columns.Add("Cancel", "Hủy")
            UltraGrid1.DisplayLayout.Bands(0).Columns.Add("Edit", "Sửa")

            With UltraGrid1
              
                .DataSource = dTable
                For Each urow As Win.UltraWinGrid.UltraGridRow In UltraGrid1.Rows
                    urow.Cells("Cancel").Value = "Hủy"
                    urow.Cells("Edit").Value = "Sửa"
                Next
                UltraGrid1.DisplayLayout.Bands(0).Columns("Cancel").Style = Win.UltraWinGrid.ColumnStyle.Button
                UltraGrid1.DisplayLayout.Bands(0).Columns("Edit").Style = Win.UltraWinGrid.ColumnStyle.Button
                UltraGrid1.DisplayLayout.Bands(0).Columns("Edit").ButtonDisplayStyle = Win.UltraWinGrid.ButtonDisplayStyle.Always
                UltraGrid1.DisplayLayout.Bands(0).Columns("Cancel").ButtonDisplayStyle = Win.UltraWinGrid.ButtonDisplayStyle.Always
                ' .Firm = "053C"
            End With
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Form2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetMaterial_Type()
    End Sub

   
    Private Sub griInvestor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles griInvestor.Load

    End Sub
End Class