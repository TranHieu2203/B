Imports System.Runtime.CompilerServices
Imports System.Web.UI.WebControls
Imports Aspose.Cells
Imports Telerik.Web.UI

Public Module ExtensionMethods

#Region "RadGrid"

    <Extension()>
    Public Sub SetFilter(ByVal grid As RadGrid)
        Utilities.SetGridFilter(grid)
    End Sub

    <Extension()>
    Public Sub ExportExcel(ByVal grid As RadGrid, ByVal sv As System.Web.HttpServerUtility,
                           ByVal Response As System.Web.HttpResponse, ByVal dtData As DataTable,
                           Optional ByVal fileName As String = "Common",
                           Optional ByVal sPath As String = "",
                           Optional ByVal iRow As Integer = 0)
        Using cls As New ExcelCommon
            Dim lst As New List(Of String)
            Dim index As Integer = 0
            ' Lấy thứ tự sắp xếp trên lưới
            For Each col As GridColumn In grid.Columns
                If col.Visible And dtData.Columns.Contains(col.UniqueName) Then
                    dtData.Columns(col.UniqueName).Caption = col.HeaderText.Replace("<br>", vbLf)
                    lst.Add(col.UniqueName)
                End If
            Next
            ' Sắp xếp lại thứ tự
            While index < lst.Count
                Dim col = dtData.Columns(lst(index))
                col.SetOrdinal(index)
                index += 1
            End While
            ' Xóa các cột thừa
            index = lst.Count
            While index < dtData.Columns.Count
                Dim col = dtData.Columns(index)
                dtData.Columns.Remove(col)
                Continue While
            End While
            Dim book = cls.ExportExcelByRadGrid(grid, dtData, sv, sPath, iRow)
            book.Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
        End Using
    End Sub
#End Region

#Region "RadTextbox"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadTextBox)
        ctrl.Text = ""
        ctrl.ToolTip = ""
    End Sub

#End Region

#Region "RadNumericTextBox"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadNumericTextBox)
        ctrl.Value = Nothing
        ctrl.ToolTip = ""
    End Sub

#End Region

#Region "RadDatePicker"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadDatePicker)
        ctrl.SelectedDate = Nothing
        ctrl.Clear()
    End Sub

#End Region

#Region "RadDateTimePicker"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadDateTimePicker)
        ctrl.SelectedDate = Nothing
        ctrl.Clear()
    End Sub

#End Region

#Region "RadTimePicker"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadTimePicker)
        ctrl.SelectedTime = Nothing
        ctrl.SelectedDate = Nothing
        ctrl.Clear()
    End Sub

#End Region

#Region "HiddenField"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As HiddenField)
        ctrl.Value = ""
    End Sub

#End Region

#Region "TextBox"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As TextBox)
        ctrl.Text = ""
    End Sub

#End Region

#Region "CheckBox"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As CheckBox)
        ctrl.Checked = False
    End Sub

#End Region

#Region "RadComboBox"

    <Extension()>
    Public Sub ClearValue(ByVal ctrl As RadComboBox)
        ctrl.Text = ""
        ctrl.ClearSelection()
        ctrl.ClearCheckedItems()
    End Sub

#End Region

#Region "Collection Convert"
    <Extension()>
    Public Function ConvertString(ByVal dt As Date, Optional formatString As String = "dd/MM/yyyy") As String
        Try
            Return Format(dt, formatString)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <Extension()>
    Public Function ToList(Of T)(ByVal dt As DataTable) As IList(Of T)
        Using collecHelper As New CollectionHelper
            Return collecHelper.ConvertTo(Of T)(dt)
        End Using
    End Function

    <Extension()>
    Public Function ToTable(Of T)(ByVal lst As IList(Of T)) As DataTable
        Return Utilities.GetDataTableByList(lst)
    End Function
#End Region
End Module
