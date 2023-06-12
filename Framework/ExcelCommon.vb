Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Telerik.Web.UI

Public Class ExcelCommon
    Implements IDisposable

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dtData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dtData As DataTable, ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = 2
                Return False
            End If
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExportExcelToTable(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dtData As DataTable, ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As DataTable
        Dim designer As WorkbookDesigner
        Try
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            Return designer.Workbook.Worksheets.Item(0).Cells.ExportDataTable(0, 0, 26, 3)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dtData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dtData As DataTable, ByVal dtVariable As DataTable,
                                        ByVal Response As System.Web.HttpResponse, Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = 2
                Return False
            End If
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If

            designer.Process()

            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dsData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dsData As DataSet, ByVal dtVariable As DataTable, _
                                        ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel,
                                        Optional ByVal is2003 As Boolean = True) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            'If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
            '    _error = 2
            '    Return False
            'End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If

            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        If is2003 Then
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Else
                            .Save(Response, fileName & ".xlsx", ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Xlsx))
                        End If
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String, ByVal fileImage As Byte(), _
                                        ByVal dsData As DataSet, ByVal dtVariable As DataTable, _
                                        ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            'If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
            '    _error = 2
            '    Return False
            'End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            'Obtaining the reference of the newly added worksheet by passing its sheet index
            Dim worksheet As Aspose.Cells.Worksheet = designer.Workbook.Worksheets(0)

            If fileImage IsNot Nothing Then
                'Adding a picture at the location of a cell whose row and column indices
                Dim ms As New System.IO.MemoryStream(fileImage)
                Dim pictureIndex As Integer = worksheet.Pictures.Add(1, 10, ms)

                'Accessing the newly added picture
                Dim picture As Aspose.Cells.Drawing.Picture = worksheet.Pictures(pictureIndex)

                'Positioning the picture proportional to row height and colum width
                picture.Width = 110
                picture.Height = 120
            End If

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If

            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dsData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dsData As DataSet, ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                _error = 2
                Return False
            End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dsData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplateNoData(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dsData As DataSet, ByVal Response As System.Web.HttpResponse,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dsData"></param>
    ''' <param name="sheetNumber"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dsData As DataSet,
                                        ByVal Response As System.Web.HttpResponse,
                                        ByVal sheetNumber As Integer,
                                        Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                _error = 2
                Return False
            End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim sheet As Worksheet = designer.Workbook.Worksheets(sheetNumber)
            sheet.Cells.ImportDataTable(dsData.Tables(0), False, 1, 0)
            sheet.Cells.ImportDataTable(dsData.Tables(1), False, 1, 2, False)
            sheet.Cells.ImportDataTable(dsData.Tables(2), False, 1, 4, False)
            sheet.Cells.ImportDataTable(dsData.Tables(3), False, 1, 6, False)
            sheet.Cells.ImportDataTable(dsData.Tables(4), False, 1, 8, False)
            sheet.Cells.ImportDataTable(dsData.Tables(5), False, 1, 10, False)
            designer.SetDataSource(dsData)
            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="dtData"></param>
    ''' <param name="Response"></param>
    ''' <param name="_error">
    ''' 1 - Temp không tồn tại
    ''' 2 - Data không tồn tại
    ''' </param>
    ''' <param name="type">
    ''' 0 - Excel
    ''' 1 - Pdf</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelNoTemplate(ByVal filePath As String,
                                          ByVal fileName As String,
                                          ByVal dtData As DataTable,
                                          ByVal Response As System.Web.HttpResponse,
                                          Optional ByRef _error As String = "",
                                          Optional ByVal type As ExportType = ExportType.Excel,
                                          Optional ByVal iRowStart As Integer = 0,
                                          Optional ByVal titleName As String = "") As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = 1
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = 2
                Return False
            End If
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cells As Cells = designer.Workbook.Worksheets(0).Cells
            Dim style As Style
            If titleName <> "" Then
                cells(1, 0).PutValue(titleName)
                style = cells(1, 0).GetStyle
                style.Font.IsBold = True
                style.Font.Size = 15
                cells(1, 0).SetStyle(style)
                iRowStart += 3
            End If
            For iCol As Integer = 0 To dtData.Columns.Count - 1
                cells(iRowStart, iCol).PutValue(dtData.Columns(iCol).Caption)
                style = cells(iRowStart, iCol).GetStyle
                style.Font.IsBold = True
                style.Borders.SetStyle(CellBorderType.Thin)
                style.Borders(BorderType.DiagonalDown).LineStyle = CellBorderType.None
                style.Borders(BorderType.DiagonalUp).LineStyle = CellBorderType.None
                style.VerticalAlignment = TextAlignmentType.Center
                style.HorizontalAlignment = TextAlignmentType.Center
                cells(iRowStart, iCol).SetStyle(style)
                For iRow As Integer = 0 To dtData.Rows.Count - 1
                    style = cells(iRow + (iRowStart + 1), iCol).GetStyle()
                    style.VerticalAlignment = TextAlignmentType.Center
                    style.Borders.SetStyle(CellBorderType.Thin)
                    style.Borders(BorderType.DiagonalDown).LineStyle = CellBorderType.None
                    style.Borders(BorderType.DiagonalUp).LineStyle = CellBorderType.None
                    Dim value = dtData.Rows(iRow)(iCol)
                    Select Case dtData.Rows(iRow)(iCol).GetType
                        Case GetType(Date), GetType(Date?), GetType(DateTime), GetType(DateTime?)
                            Dim test As New TimeSpan(0, 0, 0)
                            If value IsNot DBNull.Value AndAlso CType(value, DateTime).TimeOfDay <> test Then
                                style.Custom = "HH:mm"
                            Else
                                style.Custom = "dd/MM/yyyy"
                            End If

                            style.HorizontalAlignment = TextAlignmentType.Center
                        Case GetType(Int16), GetType(Int16?), GetType(Int32), GetType(Int32?), GetType(Int64), GetType(Int64?),
                            GetType(Integer), GetType(Integer?)
                            style.Number = 0
                            style.HorizontalAlignment = TextAlignmentType.Right
                        Case GetType(Double), GetType(Double?), GetType(Decimal), GetType(Decimal?)
                            style.Number = 43
                            style.HorizontalAlignment = TextAlignmentType.Right
                        Case GetType(Boolean), GetType(Boolean?)
                            style.Number = 49
                            style.HorizontalAlignment = TextAlignmentType.Center
                            If value IsNot DBNull.Value AndAlso value IsNot Nothing AndAlso value Then
                                value = "X"
                            Else
                                value = ""
                            End If
                        Case Else
                            style.Number = 49
                            style.HorizontalAlignment = TextAlignmentType.Left
                            style.IsTextWrapped = True
                    End Select
                    cells(iRow + (iRowStart + 1), iCol).PutValue(dtData.Rows(iRow)(iCol))
                    cells(iRow + (iRowStart + 1), iCol).SetStyle(style)
                Next
            Next

            designer.Process()

            With designer.Workbook
                .Worksheets(0).AutoFitColumns()
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(FileFormatType.Pdf))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExportExcelByRadGrid(ByVal grid As RadGrid,
                                        ByVal dtData As DataTable,
                                        ByVal sv As System.Web.HttpServerUtility,
                                        ByVal sPath As String,
                                        ByVal iRow As Integer) As Workbook
        Dim sLink As String
        If sPath <> "" Then
            sLink = sPath
        Else
            sLink = sv.MapPath("/ReportTemplates/Common/Common.xls")
        End If
        Return ExportExcelNoTemplate(sLink, dtData, False, False, Nothing, iRow)
    End Function
    Private Sub ExportExcelNoTemplate_SetHeaderValue(ByVal style As Style, ByVal iCol As Decimal, ByVal iRowStartImport As Decimal,
                                                    ByVal HeaderText As String, ByVal cells As Cells)
        ExportExcelNoTemplate_SetStyle(style, StyleType.Header)
        cells(iRowStartImport, iCol).PutValue(HeaderText)
        cells(iRowStartImport, iCol).SetStyle(style)
    End Sub

    Public Enum StyleType
        None = 0
        Header = 1
        Group = 2
        Sum = 3
        Stt = 4
        Body = 5
    End Enum
    Private Function ExportExcelNoTemplate_SetStyle(ByRef style As Style, Optional ByVal type As StyleType = StyleType.None) As Style
        style.Borders(BorderType.TopBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.BottomBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.LeftBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.RightBorder).LineStyle = CellBorderType.Thin
        Select Case type
            Case StyleType.Header
                style.Font.IsBold = True
                style.Font.Size += 1
                style.HorizontalAlignment = TextAlignmentType.Center
            Case StyleType.Group
                style.Font.IsBold = True
                style.HorizontalAlignment = TextAlignmentType.Left
            Case StyleType.Sum
                style.Font.IsBold = True
                style.HorizontalAlignment = TextAlignmentType.Center
            Case StyleType.Stt
                style.HorizontalAlignment = TextAlignmentType.Center
            Case StyleType.Body
            Case StyleType.None
                style.Borders(BorderType.TopBorder).LineStyle = CellBorderType.None
                style.Borders(BorderType.BottomBorder).LineStyle = CellBorderType.None
                style.Borders(BorderType.LeftBorder).LineStyle = CellBorderType.None
                style.Borders(BorderType.RightBorder).LineStyle = CellBorderType.None
        End Select
    End Function
    Public Function ExportExcelNoTemplate(ByVal filePath As String,
                                      ByVal dtData As DataTable, _
                                      ByVal isGroup As Boolean,
                                      ByVal isSum As Boolean,
                                      Optional ByVal dtVariable As DataTable = Nothing,
                                      Optional ByRef iRowStartImport As Decimal = 0)
        Dim designer As WorkbookDesigner
        Try
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cells As Cells = designer.Workbook.Worksheets(0).Cells
            Dim style As Style
            For iCol As Integer = 0 To dtData.Columns.Count - 1
                Dim cell = cells(iRowStartImport, iCol)

                ExportExcelNoTemplate_SetHeaderValue(cell.GetStyle, iCol, iRowStartImport,
                                                     dtData.Columns(iCol).Caption, cells)

                For iRow As Integer = 0 To dtData.Rows.Count - 1
                    style = cells(iRowStartImport + iRow + 1, iCol).GetStyle()
                    cell = cells(iRowStartImport + iRow + 1, iCol)

                    If isGroup And dtData.Columns(iCol).ColumnName.Contains("GROUP_") Then
                        ExportExcelNoTemplate_SetStyle(style, StyleType.Group)
                        cell.PutValue(dtData.Rows(iRow)(iCol))
                        cell.SetStyle(style)
                    Else
                        If dtData.Columns(iCol).ColumnName = "STT" Then
                            Dim value As Double? = Nothing
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                value = Double.Parse(dtData.Rows(iRow)(iCol))
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        ElseIf dtData.Columns(iCol).ColumnName = "YEAR_GRA" Then
                            Dim value As Integer? = Nothing
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                value = Integer.Parse(dtData.Rows(iRow)(iCol))
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        ElseIf dtData.Columns(iCol).ColumnName = "TOTAL_COST" Then
                            Dim value As String = ""
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                value = Long.Parse(dtData.Rows(iRow)(iCol)).ToString("#,#", CultureInfo.InvariantCulture)
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        ElseIf dtData.Columns(iCol).ColumnName = "DAY_NUM" Then
                            Dim value
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                If Integer.TryParse(dtData.Rows(iRow)(iCol), 0) Then
                                    value = Integer.Parse(dtData.Rows(iRow)(iCol))
                                Else
                                    value = Decimal.Parse(dtData.Rows(iRow)(iCol))
                                End If
                            Else
                                value = String.Empty
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        ElseIf dtData.Columns(iCol).ColumnName = "MONEY" Then
                            Dim value As String = ""
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                value = Long.Parse(dtData.Rows(iRow)(iCol)).ToString("#,#", CultureInfo.InvariantCulture)
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        Else
                            Dim isSumGroup As Boolean = False
                            If dtData.Columns.Contains("GROUP_1") Then
                                isSumGroup = IIf(dtData.Rows(iRow)("GROUP_1") IsNot DBNull.Value, True, False)
                            End If
                            If Not isSumGroup AndAlso dtData.Columns.Contains("GROUP_2") Then
                                isSumGroup = IIf(dtData.Rows(iRow)("GROUP_2") IsNot DBNull.Value, True, False)
                            End If
                            If Not isSumGroup AndAlso dtData.Columns.Contains("GROUP_3") Then
                                isSumGroup = IIf(dtData.Rows(iRow)("GROUP_3") IsNot DBNull.Value, True, False)
                            End If
                            If isSumGroup Then
                                ExportExcelNoTemplate_SetStyle(style, StyleType.Sum)
                                cell.PutValue(dtData.Rows(iRow)(iCol))
                                cell.SetStyle(style)
                            Else
                                style.VerticalAlignment = TextAlignmentType.Center
                                ExportExcelNoTemplate_SetBodyValue(dtData.Columns(iCol).DataType, dtData.Rows(iRow)(iCol),
                                                                       style, iCol, iRow, iRowStartImport, cells)
                            End If

                        End If
                    End If
                Next
            Next

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If

            designer.Process()
            With (designer.Workbook)
                .Worksheets(0).AutoFitColumns()
                .Worksheets(0).AutoFitRows()
                .CalculateFormula()
            End With
            Return designer.Workbook


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub ExportExcelNoTemplate_SetBodyValue(ByVal type As Type, ByVal value As Object, ByVal style As Style, ByVal iCol As Decimal,
                                               ByVal iRow As Decimal, ByVal iRowStartImport As Decimal, ByVal cells As Cells)
        ExportExcelNoTemplate_SetStyle(style, StyleType.Body)
        Select Case type
            Case GetType(Date), GetType(Date?), GetType(DateTime), GetType(DateTime?)
                Dim test As New TimeSpan(0, 0, 0)
                If value IsNot DBNull.Value Then
                    If CType(value, DateTime).TimeOfDay <> test Then
                        style.Custom = "HH:mm"
                    ElseIf value = "00:00" Then
                        value = "00:00"
                    Else
                        style.Custom = "dd/MM/yyyy"
                    End If
                End If

                style.HorizontalAlignment = TextAlignmentType.Center
            Case GetType(Int16), GetType(Int16?), GetType(Int32), GetType(Int32?), GetType(Int64), GetType(Int64?),
                GetType(Integer), GetType(Integer?)
                style.Number = 0
                style.HorizontalAlignment = TextAlignmentType.Right
            Case GetType(Double), GetType(Double?), GetType(Decimal), GetType(Decimal?)
                style.Number = 43
                style.HorizontalAlignment = TextAlignmentType.Right
            Case GetType(Boolean), GetType(Boolean?)
                style.Number = 49
                style.HorizontalAlignment = TextAlignmentType.Center
                If value IsNot DBNull.Value AndAlso value IsNot Nothing AndAlso value Then
                    value = "X"
                Else
                    value = ""
                End If
            Case Else
                style.Number = 49
                style.HorizontalAlignment = TextAlignmentType.Left
                style.IsTextWrapped = True
        End Select

        cells(iRowStartImport + iRow + 1, iCol).PutValue(value)
        cells(iRowStartImport + iRow + 1, iCol).SetStyle(style)
    End Sub
    Public Enum ExportType
        Excel = 0
        PDF = 1
    End Enum

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
