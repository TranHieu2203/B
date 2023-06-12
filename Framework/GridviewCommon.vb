Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class GridviewCommon
    Public Shared Function DesignGrid(ByVal dt As DataTable) As RadGrid
        Dim rCol As GridBoundColumn
        Dim columnName As String
        Dim gv As New RadGrid
        gv.MasterTableView.Columns.Clear()

        For i = 0 To dt.Columns.Count - 1
            'create column name in RadGrid
            columnName = dt.Columns(i).ColumnName
            rCol = New GridBoundColumn()
            'gv.MasterTableView.Columns.Add(rCol)
            rCol.DataField = columnName
            rCol.HeaderText = columnName
            rCol.HeaderTooltip = columnName
            'rCol.HeaderStyle.Width = 100
            rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            'rCol.ReadOnly = True
            gv.MasterTableView.Columns.Add(rCol)
        Next

        gv.DataSource = dt

        Return gv
    End Function

    Public Shared Function DesignGridWithoutData(ByVal dt As DataTable) As RadGrid
        Dim rCol As GridBoundColumn
        Dim columnName As String
        Dim gv As New RadGrid
        gv.MasterTableView.Columns.Clear()

        For i = 0 To dt.Columns.Count - 1
            'create column name in RadGrid
            columnName = dt.Columns(i).ColumnName
            rCol = New GridBoundColumn()
            rCol.DataField = columnName
            rCol.HeaderText = columnName
            rCol.HeaderTooltip = columnName
            rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            gv.MasterTableView.Columns.Add(rCol)
        Next

        Return gv
    End Function

End Class
