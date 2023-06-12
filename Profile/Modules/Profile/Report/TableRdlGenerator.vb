Imports Framework.UI

Public Class TableRdlGenerator

    Private m_fields As List(Of String)
    Private m_fieldTypes As List(Of String)
    Private m_fieldTexts As New Dictionary(Of String, String)

    Public Property Fields() As List(Of String)
        Get
            Return m_fields
        End Get
        Set(ByVal value As List(Of String))
            m_fields = value
        End Set
    End Property

    Public Property FieldTypes() As List(Of String)
        Get
            Return m_fieldTypes
        End Get
        Set(ByVal value As List(Of String))
            m_fieldTypes = value
        End Set
    End Property

    Public Property FieldTexts As Dictionary(Of String, String)
        Get
            Return m_fieldTexts
        End Get
        Set(value As Dictionary(Of String, String))
            m_fieldTexts = value
        End Set
    End Property



    Public Function CreateTable() As Rdl.TableType
        Dim table As New Rdl.TableType()
        table.Name = "Table1"
        table.Items = New Object() {CreateTableColumns(), CreateHeader(), CreateDetails()}
        table.ItemsElementName = New Rdl.ItemsChoiceType21() {Rdl.ItemsChoiceType21.TableColumns, Rdl.ItemsChoiceType21.Header, Rdl.ItemsChoiceType21.Details}
        Return table
    End Function

    Private Function CreateHeader() As Rdl.HeaderType
        Dim header As New Rdl.HeaderType()
        header.Items = New Object() {CreateHeaderTableRows(), True}
        header.ItemsElementName = New Rdl.ItemsChoiceType20() {Rdl.ItemsChoiceType20.TableRows, Rdl.ItemsChoiceType20.RepeatOnNewPage}
        Return header
    End Function

    Private Function CreateHeaderTableRows() As Rdl.TableRowsType
        Dim headerTableRows As New Rdl.TableRowsType()
        headerTableRows.TableRow = New Rdl.TableRowType() {CreateHeaderTableRow()}
        Return headerTableRows
    End Function

    Private Function CreateHeaderTableRow() As Rdl.TableRowType
        Dim headerTableRow As New Rdl.TableRowType()
        headerTableRow.Items = New Object() {CreateHeaderTableCells(), "0.25in"}
        Return headerTableRow
    End Function

    Private Function CreateHeaderTableCells() As Rdl.TableCellsType
        Dim headerTableCells As New Rdl.TableCellsType()
        headerTableCells.TableCell = New Rdl.TableCellType(m_fields.Count) {}
        Dim i As Integer
        For i = 0 To m_fields.Count - 1
            headerTableCells.TableCell(i) = CreateHeaderTableCell(m_fields(i))
        Next i
        Return headerTableCells
    End Function

    Private Function CreateHeaderTableCell(ByVal fieldName As String) As Rdl.TableCellType
        Dim headerTableCell As New Rdl.TableCellType()
        headerTableCell.Items = New Object() {CreateHeaderTableCellReportItems(fieldName)}
        Return headerTableCell
    End Function

    Private Function CreateHeaderTableCellReportItems(ByVal fieldName As String) As Rdl.ReportItemsType
        Dim headerTableCellReportItems As New Rdl.ReportItemsType()
        headerTableCellReportItems.Items = New Object() {CreateHeaderTableCellTextbox(fieldName)}
        Return headerTableCellReportItems
    End Function

    Private Function CreateHeaderTableCellTextbox(ByVal fieldName As String) As Rdl.TextboxType
        Dim headerTableCellTextbox As New Rdl.TextboxType()
        Dim headerText As String
        If m_fieldTexts.ContainsKey(fieldName) Then
            headerText = m_fieldTexts(fieldName)
        Else
            headerText = fieldName
        End If
        headerTableCellTextbox.Name = fieldName + "_Header"
        headerTableCellTextbox.Items = New Object() {Translate(headerText), CreateHeaderTableCellTextboxStyle(), True}
        headerTableCellTextbox.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
        Return headerTableCellTextbox
    End Function

    Private Function CreateHeaderTableCellTextboxStyle() As Rdl.StyleType
        Dim borderstyle As New Rdl.BorderColorStyleWidthType
        borderstyle.Items = New Object() {"Solid"}
        borderstyle.ItemsElementName = New Rdl.ItemsChoiceType3() {Rdl.ItemsChoiceType3.Default}
        Dim bordercolor As New Rdl.BorderColorStyleWidthType
        bordercolor.Items = New Object() {"Black"}
        bordercolor.ItemsElementName = New Rdl.ItemsChoiceType3() {Rdl.ItemsChoiceType3.Default}
        Dim headerTableCellTextboxStyle As New Rdl.StyleType()
        headerTableCellTextboxStyle.Items = New Object() {"Bold", "11pt", "Times New Roman", "Center", "Middle", "#000000", "White", bordercolor, borderstyle}
        headerTableCellTextboxStyle.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontWeight, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.VerticalAlign, Rdl.ItemsChoiceType5.Color, Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.BorderColor, Rdl.ItemsChoiceType5.BorderStyle}
        Return headerTableCellTextboxStyle
    End Function

    Private Function CreateDetails() As Rdl.DetailsType
        Dim details As New Rdl.DetailsType()
        details.Items = New Object() {CreateTableRows()}
        Return details
    End Function

    Private Function CreateTableRows() As Rdl.TableRowsType
        Dim tableRows As New Rdl.TableRowsType()
        tableRows.TableRow = New Rdl.TableRowType() {CreateTableRow()}
        Return tableRows
    End Function

    Private Function CreateTableRow() As Rdl.TableRowType
        Dim tableRow As New Rdl.TableRowType()
        tableRow.Items = New Object() {CreateTableCells(), "0.25in"}
        Return tableRow
    End Function

    Private Function CreateTableCells() As Rdl.TableCellsType
        Dim tableCells As New Rdl.TableCellsType()
        tableCells.TableCell = New Rdl.TableCellType(m_fields.Count) {}
        Dim i As Integer
        For i = 0 To m_fields.Count - 1
            tableCells.TableCell(i) = CreateTableCell(m_fields(i), m_fieldTypes(i))
        Next i
        Return tableCells
    End Function

    Private Function CreateTableCell(ByVal fieldName As String, ByVal fileType As String) As Rdl.TableCellType
        Dim tableCell As New Rdl.TableCellType()
        tableCell.Items = New Object() {CreateTableCellReportItems(fieldName, fileType)}
        Return tableCell
    End Function

    Private Function CreateTableCellReportItems(ByVal fieldName As String, ByVal fileType As String) As Rdl.ReportItemsType
        Dim reportItems As New Rdl.ReportItemsType()
        reportItems.Items = New Object() {CreateTableCellTextbox(fieldName, fileType)}
        Return reportItems
    End Function

    Private Function CreateTableCellTextbox(ByVal fieldName As String, ByVal fileType As String) As Rdl.TextboxType
        Dim textbox As New Rdl.TextboxType()
        textbox.Name = fieldName
        textbox.Items = New Object() {"=Fields!" + fieldName + ".Value", CreateTableCellTextboxStyle(fieldName, fileType), True}
        textbox.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
        Return textbox
    End Function

    Private Function CreateTableCellTextboxStyle(ByVal fieldName As String, ByVal fileType As String) As Rdl.StyleType
        Dim borderstyle As New Rdl.BorderColorStyleWidthType
        borderstyle.Items = New Object() {"Solid"}
        borderstyle.ItemsElementName = New Rdl.ItemsChoiceType3() {Rdl.ItemsChoiceType3.Default}
        Dim bordercolor As New Rdl.BorderColorStyleWidthType
        bordercolor.Items = New Object() {"Black"}
        bordercolor.ItemsElementName = New Rdl.ItemsChoiceType3() {Rdl.ItemsChoiceType3.Default}
        Dim style As New Rdl.StyleType()

        If fileType.ToUpper = "DATE" Then
            style.Items = New Object() {"Center", bordercolor, borderstyle, "5pt", "Middle", "dd/MM/yyyy", "11pt", "Times New Roman"}
            style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderColor, Rdl.ItemsChoiceType5.BorderStyle, Rdl.ItemsChoiceType5.PaddingLeft, Rdl.ItemsChoiceType5.VerticalAlign, Rdl.ItemsChoiceType5.Format, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily}
        ElseIf fileType.ToUpper = "NUMBER" Then
            style.Items = New Object() {"Left", bordercolor, borderstyle, "5pt", "Middle", "#,##0.##", "11pt", "Times New Roman"}
            style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderColor, Rdl.ItemsChoiceType5.BorderStyle, Rdl.ItemsChoiceType5.PaddingLeft, Rdl.ItemsChoiceType5.VerticalAlign, Rdl.ItemsChoiceType5.Format, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily}
        Else
            style.Items = New Object() {"", "Left", bordercolor, borderstyle, "5pt", "Middle", "11pt", "Times New Roman"}
            style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderColor, Rdl.ItemsChoiceType5.BorderStyle, Rdl.ItemsChoiceType5.PaddingLeft, Rdl.ItemsChoiceType5.VerticalAlign, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily}
        End If
        Return style
    End Function

    Private Function CreateTableColumns() As Rdl.TableColumnsType
        Dim tableColumns As New Rdl.TableColumnsType()
        tableColumns.TableColumn = New Rdl.TableColumnType(m_fields.Count) {}
        Dim i As Integer
        For i = 0 To m_fields.Count - 1
            tableColumns.TableColumn(i) = CreateTableColumn()
        Next i
        Return tableColumns
    End Function

    Private Function CreateTableColumn() As Rdl.TableColumnType
        Dim tableColumn As New Rdl.TableColumnType()
        tableColumn.Items = New Object() {"2in"}
        Return tableColumn
    End Function

    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String) As String
        Using langMgr As New LanguageManager
            Try
                Return langMgr.Translate(str, args)
            Catch ex As Exception
                langMgr.Dispose()
            End Try
            Return str
        End Using

    End Function
End Class