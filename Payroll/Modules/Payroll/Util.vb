Imports System.IO
Imports Aspose.Cells

Public Class Util
    Public Shared Function OracleToDotNet(ByVal text As String) As Guid
        Dim bytes As Byte() = ParseHex(text)
        Dim guid As New Guid(bytes)
        Return guid
    End Function

    Public Shared Function ParseHex(ByVal text As String) As Byte()
        ' Not the most efficient code in the world, but
        ' it works...
        Dim ret As Byte() = New Byte(text.Length \ 2 - 1) {}
        For i As Integer = 0 To ret.Length - 1
            ret(i) = Convert.ToByte(text.Substring(i * 2, 2), 16)
        Next
        Return ret
    End Function

    Public Shared Function Export2Excel(ByVal sReportFileName As String, ByVal dsSource As DataSet,
                                        ByVal dtVariable As DataTable, Optional ByVal filename As String = "") As Boolean
        'Dim strResult As String = ""
        Dim filePath As String
        Dim templatefolder As String
        'Dim exportfolder As String
        Dim designer As WorkbookDesigner
        Dim fileAttachInfo As String
        Try
            'Dim strRoot As String = System.Windows.Forms.Application.StartupPath & "\lib\"

            'Dim l As New Aspose.Cells.License()
            'Dim strLicense As String = strRoot & "Aspose.Cells.lic"
            'l.SetLicense(strLicense)

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If File.Exists(filePath) Then
                designer = New WorkbookDesigner
                designer.Open(filePath)

                designer.SetDataSource(dsSource)

                If dtVariable IsNot Nothing Then
                    Dim intCols As Integer = dtVariable.Columns.Count
                    For i As Integer = 0 To intCols - 1
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                    Next
                End If

                designer.Process()
                'exportfolder = ConfigurationManager.AppSettings("ExcelFileFolder")
                'Dim fileAttachInfo As String = AppDomain.CurrentDomain.BaseDirectory + "\" + exportfolder + "\"
                'If Not System.IO.Directory.Exists(fileAttachInfo) Then
                'System.IO.Directory.CreateDirectory(fileAttachInfo)
                'End If

                'Save the excel file
                Dim Format As FileFormatType = New Aspose.Cells.FileFormatType()
                Dim fi As New FileInfo(filePath)
                If fi.Extension = ".xlsx" Then
                    Format = FileFormatType.Xlsx
                    If filename <> "" Then
                        fileAttachInfo = filename & ".xlsx"
                    Else
                        fileAttachInfo = Guid.NewGuid().ToString() & ".xlsx"
                    End If

                Else
                    Format = FileFormatType.Excel97To2003
                    If filename <> "" Then
                        fileAttachInfo = filename & ".xls"
                    Else
                        fileAttachInfo = Guid.NewGuid().ToString() & ".xls"
                    End If
                End If
                designer.Workbook.CalculateFormula()
                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response)

                'designer.Workbook.Save(fileAttachInfo, Format)

                'If Not String.IsNullOrEmpty(fileAttachInfo) Then
                'HttpContext.Current.Response.Redirect(fileAttachInfo)
                'File.Delete(fileAttachInfo)
                'End If
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function ExportExcelByDataTable(ByVal sReportFileName As String, ByVal dtTable As DataTable,
                                    ByVal dtVariable As DataTable, Optional ByVal filename As String = "",
                                     Optional ByVal startRow As Integer = 0, Optional ByVal startColumn As Integer = 0,
                                     Optional ByVal isInsert As Boolean = False, Optional ByVal SheetIndex As Integer = 0) As Boolean
        'Dim strResult As String = """
        Dim filePath As String
        Dim templatefolder As String
        'Dim exportfolder As String
        Dim designer As WorkbookDesigner
        Dim fileAttachInfo As String
        Try
            'Dim strRoot As String = System.Windows.Forms.Application.StartupPath & "\lib\"

            'Dim l As New Aspose.Cells.License()
            'Dim strLicense As String = strRoot & "Aspose.Cells.lic"
            'l.SetLicense(strLicense)

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If File.Exists(filePath) Then
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.Workbook.Worksheets(SheetIndex).Cells.ImportDataTable(dtTable, False, startRow, startColumn, isInsert)

                designer.Process()
                'exportfolder = ConfigurationManager.AppSettings("ExcelFileFolder")
                'Dim fileAttachInfo As String = AppDomain.CurrentDomain.BaseDirectory + "\" + exportfolder + "\"
                'If Not System.IO.Directory.Exists(fileAttachInfo) Then
                'System.IO.Directory.CreateDirectory(fileAttachInfo)
                'End If

                'Save the excel file
                Dim Format As FileFormatType = New Aspose.Cells.FileFormatType()
                Dim fi As New FileInfo(filePath)
                If fi.Extension = ".xlsx" Then
                    Format = FileFormatType.Xlsx
                    If filename <> "" Then
                        fileAttachInfo = filename & ".xlsx"
                    Else
                        fileAttachInfo = Guid.NewGuid().ToString() & ".xlsx"
                    End If

                Else
                    Format = FileFormatType.Excel97To2003
                    If filename <> "" Then
                        fileAttachInfo = filename & ".xls"
                    Else
                        fileAttachInfo = Guid.NewGuid().ToString() & ".xls"
                    End If
                End If
                designer.Workbook.CalculateFormula()
                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response)

                'designer.Workbook.Save(fileAttachInfo, Format)

                'If Not String.IsNullOrEmpty(fileAttachInfo) Then
                'HttpContext.Current.Response.Redirect(fileAttachInfo)
                'File.Delete(fileAttachInfo)
                'End If
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function CreateExcelWorkbook(ByVal sReportFile As String, ByVal dsSource As DataSet, ByVal dtVariable As DataTable) As Workbook
        'Dim strResult As String = ""
        Dim filePath As String
        'Dim exportfolder As String
        Dim designer As WorkbookDesigner
        Try

            filePath = sReportFile

            If File.Exists(filePath) Then
                designer = New WorkbookDesigner

                designer.Open(filePath)

                designer.SetDataSource(dsSource)

                If dtVariable IsNot Nothing Then
                    Dim intCols As Integer = dtVariable.Columns.Count
                    For i As Integer = 0 To intCols - 1
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                    Next
                End If

                designer.Process()

                Return designer.Workbook
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Export2Pdf(ByVal sReportFileName As String, ByVal dsSource As DataSet, ByVal dtVariable As DataTable) As Boolean
        'Dim strResult As String = ""
        Dim filePath As String
        Dim templatefolder As String
        'Dim exportfolder As String
        Dim designer As WorkbookDesigner
        Dim fileAttachInfo As String
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If File.Exists(filePath) Then
                designer = New WorkbookDesigner
                designer.Open(filePath)

                designer.SetDataSource(dsSource)

                If dtVariable IsNot Nothing Then
                    Dim intCols As Integer = dtVariable.Columns.Count
                    For i As Integer = 0 To intCols - 1
                        designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                    Next
                End If

                designer.Process()

                'Save the excel file
                Dim Format As FileFormatType = New Aspose.Cells.FileFormatType()
                Dim fi As New FileInfo(filePath)
                Format = FileFormatType.Pdf
                fileAttachInfo = Guid.NewGuid().ToString() & ".pdf"

                designer.Save(fileAttachInfo, SaveType.OpenInBrowser, Format, HttpContext.Current.Response)
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
End Class
