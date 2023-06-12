Option Strict On
Option Explicit On

Imports System.IO
Imports Aspose.Cells
Imports Aspose.Cells.Drawing
Imports Telerik.Web.UI

Public Class AsposeExcelCommon
    Implements IDisposable
#Region "THANHNT - REPORT - EXPORT EXCEL"
    ''' <summary>
    ''' 
    ''' </summary>a 
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
    Public Function ExportExcelTemplateReport(ByVal filePath As String, ByVal filePathTemp As String, ByVal fileName As String,
                                            ByVal dsData As DataSet, ByVal Response As System.Web.HttpResponse, Optional ByRef _error As String = "",
                                            Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Dim rep As New HistaffFrameworkRepository
        Try
            Dim pathTemp As String = ""
            If dsData.Tables.Count > 0 Then
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)

                Dim worksheet As Aspose.Cells.Worksheet = designer.Workbook.Worksheets(0)

                If dsData.Tables(0).Rows(0)("FILE_LOGO").ToString IsNot Nothing And dsData.Tables(0).Rows(0)("FILE_LOGO").ToString <> "" And dsData.Tables(0).Rows(0)("FILE_LOGO").ToString <> "NoImage.jpg" Then
                    'Adding a picture at the location of a cell whose row and column indices

                    Dim b As Byte() = File.ReadAllBytes(dsData.Tables(0).Rows(0)("FILE_LOGO").ToString)

                    Dim ms As New System.IO.MemoryStream(b)
                    Dim pictureIndex As Integer = worksheet.Pictures.Add(1, 1, ms)

                    'Accessing the newly added picture
                    Dim picture As Aspose.Cells.Drawing.Picture = worksheet.Pictures(pictureIndex)

                    'Positioning the picture proportional to row height and colum width
                    picture.Width = 400
                    picture.Height = 120
                End If

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()

                'Dim index As Integer = designer.Workbook.Worksheets(0).Pictures.Add(3, 2, 3, 2, "C:\Users\Hong Quan\Pictures\Saved Pictures\961839.jpg")
                'Dim pic As Picture = designer.Workbook.Worksheets(0).Pictures(index)
                'pic.Placement = PlacementType.FreeFloating
                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With
            Else  'Template co nhieu datatable
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                'Dim dsDataDynamic = dsData.Tables(dsData.Tables.Count - 1).Copy()
                'Dim dsDataFill As New DataSet
                'dsDataFill.Tables.Add(dsDataDynamic)
                'Dim table As DataTable = dsData.Tables(dsData.Tables.Count - 1)
                'If (dsData.Tables.CanRemove(table)) Then
                '    dsData.Tables.Remove(table)
                'End If

                'dsData = dsData.
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)
                designer.Process()
                designer.Workbook.CalculateFormula()
                designer.Workbook.Save(filePathTemp & fileName & ".xls", New XlsSaveOptions())

                'roi bị lan 2 luon o day
                designer = New WorkbookDesigner
                designer.Open(filePathTemp & fileName & ".xls")
                designer.SetDataSource(dsData) 'Bind lần 2 -> z hả a
                designer.Process()
                designer.Workbook.CalculateFormula()
                'Dim index As Integer = designer.Workbook.Worksheets(0).Pictures.Add(3, 2, 3, 2, "C:\Users\Hong Quan\Pictures\Saved Pictures\961839.jpg")
                'Dim pic As Picture = designer.Workbook.Worksheets(0).Pictures(index)
                'pic.Placement = PlacementType.FreeFloating
                'sau do mo~ lai file da~ save va fill data vao
                'designer = New WorkbookDesigner
                'designer.Open(filePathTemp & fileName & ".xls")
                'designer.SetDataSource(dsDataFill)

                'add parameter in report and header + footer
                'designer.Process()
                'designer.Workbook.CalculateFormula()
                'Dim range As Range = designer.Workbook.Worksheets(1).Cells.CreateRange("AN:BQ")

                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With

                Dim fInfo As New FileInfo(filePathTemp & fileName & ".xls")
                If fInfo.Exists Then
                    fInfo.Delete()
                End If
            End If

            'System.Diagnostics.Process.Start(fileName) 'view after save

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' </summary>a 
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
    Public Function ExportExcelTemplateReportNoLogo(ByVal filePath As String, ByVal filePathTemp As String, ByVal fileName As String,
                                            ByVal dsData As DataSet, ByVal Response As System.Web.HttpResponse, Optional ByRef _error As String = "",
                                            Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Dim rep As New HistaffFrameworkRepository
        Try
            Dim pathTemp As String = ""
            If dsData.Tables.Count > 0 Then
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)

                Dim worksheet As Aspose.Cells.Worksheet = designer.Workbook.Worksheets(0)

                If dsData.Tables(1).Rows(0)("FILE_LOGO").ToString IsNot Nothing And dsData.Tables(1).Rows(0)("FILE_LOGO").ToString <> "" And dsData.Tables(1).Rows(0)("FILE_LOGO").ToString <> "NoImage.jpg" Then
                    'Adding a picture at the location of a cell whose row and column indices

                    Dim b As Byte() = File.ReadAllBytes(dsData.Tables(1).Rows(0)("FILE_LOGO").ToString)

                    Dim ms As New System.IO.MemoryStream(b)
                    Dim pictureIndex As Integer = worksheet.Pictures.Add(1, 1, ms)

                    'Accessing the newly added picture
                    Dim picture As Aspose.Cells.Drawing.Picture = worksheet.Pictures(pictureIndex)

                    'Positioning the picture proportional to row height and colum width
                    picture.Width = 400
                    picture.Height = 120
                End If

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()

                'Dim index As Integer = designer.Workbook.Worksheets(0).Pictures.Add(3, 2, 3, 2, "C:\Users\Hong Quan\Pictures\Saved Pictures\961839.jpg")
                'Dim pic As Picture = designer.Workbook.Worksheets(0).Pictures(index)
                'pic.Placement = PlacementType.FreeFloating
                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With
            Else  'Template co nhieu datatable
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                'Dim dsDataDynamic = dsData.Tables(dsData.Tables.Count - 1).Copy()
                'Dim dsDataFill As New DataSet
                'dsDataFill.Tables.Add(dsDataDynamic)
                'Dim table As DataTable = dsData.Tables(dsData.Tables.Count - 1)
                'If (dsData.Tables.CanRemove(table)) Then
                '    dsData.Tables.Remove(table)
                'End If

                'dsData = dsData.
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)
                designer.Process()
                designer.Workbook.CalculateFormula()
                designer.Workbook.Save(filePathTemp & fileName & ".xls", New XlsSaveOptions())

                'roi bị lan 2 luon o day
                designer = New WorkbookDesigner
                designer.Open(filePathTemp & fileName & ".xls")
                designer.SetDataSource(dsData) 'Bind lần 2 -> z hả a
                designer.Process()
                designer.Workbook.CalculateFormula()
                'Dim index As Integer = designer.Workbook.Worksheets(0).Pictures.Add(3, 2, 3, 2, "C:\Users\Hong Quan\Pictures\Saved Pictures\961839.jpg")
                'Dim pic As Picture = designer.Workbook.Worksheets(0).Pictures(index)
                'pic.Placement = PlacementType.FreeFloating
                'sau do mo~ lai file da~ save va fill data vao
                'designer = New WorkbookDesigner
                'designer.Open(filePathTemp & fileName & ".xls")
                'designer.SetDataSource(dsDataFill)

                'add parameter in report and header + footer
                'designer.Process()
                'designer.Workbook.CalculateFormula()
                'Dim range As Range = designer.Workbook.Worksheets(1).Cells.CreateRange("AN:BQ")

                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With

                Dim fInfo As New FileInfo(filePathTemp & fileName & ".xls")
                If fInfo.Exists Then
                    fInfo.Delete()
                End If
            End If

            'System.Diagnostics.Process.Start(fileName) 'view after save

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ExportExcelTemplateReportPassword(ByVal filePath As String, ByVal filePathTemp As String, ByVal fileName As String,
                                                        ByVal dsData As DataSet, ByVal passWord As String, Optional ByRef _error As String = "",
                                                        Optional ByVal type As ExportType = ExportType.Excel) As String
        Dim designer As WorkbookDesigner
        Try
            Dim pathTemp As String = ""
            If dsData.Tables.Count = 1 Then
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.Workbook.Decrypt(passWord)  'password
                designer.SetDataSource(dsData)

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()
                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(fileName & ".xls", New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(fileName & ".pdf", New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With
            Else  'Template co nhieu datatable
                Dim dsDataDynamic = dsData.Tables(dsData.Tables.Count - 1).Copy()
                Dim dsDataFill As New DataSet
                dsDataFill.Tables.Add(dsDataDynamic)
                Dim table As DataTable = dsData.Tables(dsData.Tables.Count - 1)
                If (dsData.Tables.CanRemove(table)) Then
                    dsData.Tables.Remove(table)
                End If

                'dsData = dsData.
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)
                designer.Process()
                designer.Workbook.CalculateFormula()
                designer.Workbook.Save(filePathTemp & fileName & ".xls", New XlsSaveOptions())

                'sau do mo~ lai file da~ save va fill data vao
                designer = New WorkbookDesigner
                designer.Open(filePathTemp & fileName & ".xls")
                designer.Workbook.Decrypt(passWord)  'password
                designer.SetDataSource(dsDataFill)

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()

                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(filePathTemp & fileName & ".xls", New XlsSaveOptions())
                            Return filePathTemp & fileName & ".xls"
                        Case ExportType.PDF
                            .Save(filePathTemp & fileName & ".pdf", New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                            Return filePathTemp & fileName & ".pdf"
                    End Select
                End With
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''''' LOCDV 11/07/2015
    ''''' TUY CHINH HAM EXPORT BAO CAO THOI DOI CA CHO MERGER NGAY BAT DAU, KET THUC KY CONG
    ''''' 
    Public Function ExportExcelTemplateReport_Custom(ByVal filePath As String, ByVal filePathTemp As String, ByVal fileName As String,
                                       ByVal dsData As DataSet, ByVal Response As System.Web.HttpResponse, Optional ByRef _error As String = "",
                                       Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Dim rep As New HistaffFrameworkRepository
        Try
            Dim pathTemp As String = ""
            If dsData.Tables.Count = 1 Then
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()
                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With
            Else  'Template co nhieu datatable
                If Not File.Exists(filePath) Then
                    _error = "1"
                    Return False
                End If
                If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                    _error = "2"
                    Return False
                End If
                Dim dsDataDynamic = dsData.Tables(dsData.Tables.Count - 1).Copy()
                Dim dsDataFill As New DataSet
                dsDataFill.Tables.Add(dsDataDynamic)
                Dim table As DataTable = dsData.Tables(dsData.Tables.Count - 1)
                If (dsData.Tables.CanRemove(table)) Then
                    dsData.Tables.Remove(table)
                End If

                'dsData = dsData.
                designer = New WorkbookDesigner
                designer.Open(filePath)
                designer.SetDataSource(dsData)
                designer.Process()
                designer.Workbook.CalculateFormula()
                designer.Workbook.Save(filePathTemp & fileName & ".xls", New XlsSaveOptions())

                'sau do mo~ lai file da~ save va fill data vao
                designer = New WorkbookDesigner
                designer.Open(filePathTemp & fileName & ".xls")
                designer.SetDataSource(dsDataFill)

                'add parameter in report and header + footer
                designer.Process()
                designer.Workbook.CalculateFormula()
                'Dim range As Range = designer.Workbook.Worksheets(1).Cells.CreateRange("AN:BQ")

                'Kiểm tra nếu template là xuất report
                'Dim rng1 As Cells = designer.Workbook.Worksheets(1).Cells

                'UnMerge the cell.
                ''rng1.Merge(3, 9, 3, 30)

                'Dim rng2 As Cells = designer.Workbook.Worksheets(1).Cells
                ''UnMerge the cell.
                'rng2.Merge(3, 40, 3, 69)
                With designer.Workbook
                    .CalculateFormula()
                    Select Case type
                        Case ExportType.Excel
                            .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                        Case ExportType.PDF
                            .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                    End Select
                End With

                Dim fInfo As New FileInfo(filePathTemp & fileName & ".xls")
                If fInfo.Exists Then
                    fInfo.Delete()
                End If
            End If

            'System.Diagnostics.Process.Start(fileName) 'view after save

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'ham noi du lieu cac file excel thanh 1 file duy nhat
    'pathfile chứa list đường dẫn của các file excel cần nối ( ĐƯỜNG DẪN )
    'name là tên sheet và là chuỗi mã nv
    'FILENAME LÀ TÊN FILE CUỐI CÙNG

    Public Function STRING2LIST(ByVal lst As String) As List(Of Object)
        Dim kq As New List(Of Object)

        Dim name As String
        While InStr(lst, ",") > 0
            name = Left(lst, InStr(lst, ",") - 1)
            kq.Add(name)
            lst = Right(lst, Len(lst) - InStr(lst, ","))
        End While
        kq.Add(lst)
        Return kq

    End Function

    Public Function ExportExcelTemplateReport_Merge(ByVal Filename As String, ByVal lstPara As List(Of Object), ByVal storeName As String, ByVal PathTemplateIn As String, ByVal PathTemplateOut As String,
                                                    ByVal fileUrl As String, ByVal Response As System.Web.HttpResponse, Optional ByVal type As ExportType = ExportType.Excel) As String
        Try
            Dim rsString As String = ""
            'tao file excel cuoi cung
            Dim excelFINISH As Workbook = New Workbook()
            Dim data As New DataSet
            Dim rep As New HistaffFrameworkRepository
            Dim name As List(Of Object) = STRING2LIST(lstPara(0).ToString)
            For i As Integer = 0 To name.Count() - 1 Step 1
                lstPara(0) = name(i)
                Try
                    data = rep.ExecuteToDataSet(storeName, lstPara)
                    Dim dsDataDynamic = data.Tables(data.Tables.Count - 1).Copy()
                    Dim dsDataFill As New DataSet
                    dsDataFill.Tables.Add(dsDataDynamic)
                    Dim table As DataTable = data.Tables(data.Tables.Count - 1)
                    If (data.Tables.CanRemove(table)) Then
                        data.Tables.Remove(table)
                    End If

                    'dsData = dsData.
                    Dim designer As WorkbookDesigner
                    designer = New WorkbookDesigner
                    designer.Open(PathTemplateIn)
                    designer.SetDataSource(data)
                    designer.Process()
                    designer.Workbook.CalculateFormula()
                    designer.Workbook.Save(PathTemplateOut & "\" & name(i).ToString() & ".xls", New XlsSaveOptions())
                Catch ex As Exception
                    Return ex.Message.ToString()
                End Try
            Next


            For I As Integer = 0 To name.Count() - 1 Step 1
                'chon sheet dang hien hanh
                Dim WSFINISH As Worksheet = excelFINISH.Worksheets(I)
                'doi ten sheet thanh msnv
                WSFINISH.Name = name(I).ToString()
                'mo excel du lieu
                Dim EXCELTEMP As Workbook = New Workbook(PathTemplateOut & "\" & name(I).ToString() & ".xls")
                Dim WSTEMP As Worksheet = EXCELTEMP.Worksheets(0)
                'copy excel du lieu vao file excel cuoi cung
                WSFINISH.Copy(WSTEMP)
                'tao moi sheet trong excel cuoi
                WSFINISH.Workbook.Worksheets.Add()
            Next

            For i As Integer = 0 To name.Count() - 1 Step 1
                'xoa excel dữ liệu
                File.Delete(PathTemplateOut & "\" & name(i).ToString() & ".xls")
            Next
            'Save the excel file.
            Select Case type
                Case ExportType.Excel
                    excelFINISH.Save(Response, Filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                Case ExportType.PDF
                    excelFINISH.Save(Response, Filename & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
            End Select
            Return rsString
        Catch ex As Exception
            Throw ex
            Return ex.Message.ToString()
        End Try

    End Function


#End Region

    Public Function ReadFileToDataTable(ByVal fileName As String) As DataTable
        Dim workbook As New Aspose.Cells.Workbook(fileName)
        Dim worksheet As Aspose.Cells.Worksheet = workbook.Worksheets(0)
        Dim dtDataPrepare As DataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1)
        Return dtDataPrepare
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
    Public Function ExportExcelTemplate(ByVal filePath As String,
                                            ByVal fileName As String,
                                            ByVal dtData As DataTable,
                                            ByVal Response As System.Web.HttpResponse,
                                            Optional ByVal tableName As String = "DATA",
                                            Optional ByRef _error As String = "",
                                            Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        'check license
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = CStr(1)
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = CStr(2)
                Return False
            End If
            dtData.TableName = tableName
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
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
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
    Public Function ExportExcelTemplate(ByVal filePath As String, ByVal fileName As String,
                                        ByVal dtData As DataTable, ByVal dtVariable As DataTable,
                                        ByVal Response As System.Web.HttpResponse, Optional ByRef _error As String = "",
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = CStr(1)
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = CStr(2)
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
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
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
                                        Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = CStr(1)
                Return False
            End If
            If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                _error = CStr(2)
                Return False
            End If
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
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
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
                _error = CStr(1)
                Return False
            End If
            If dsData Is Nothing OrElse (dsData IsNot Nothing AndAlso dsData.Tables.Count = 0) Then
                _error = CStr(2)
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
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
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
                                          ByVal Response As System.Web.HttpResponse, _
                                          Optional ByVal tableName As String = "DATA",
                                          Optional ByRef _error As String = "",
                                          Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = CStr(1)
                Return False
            End If
            If dtData Is Nothing OrElse (dtData IsNot Nothing AndAlso dtData.Rows.Count = 0) Then
                _error = CStr(2)
                Return False
            End If
            dtData.TableName = tableName
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cells As Cells = designer.Workbook.Worksheets(0).Cells

            For iCol As Integer = 0 To dtData.Columns.Count - 1
                cells(0, iCol).PutValue(dtData.Columns(iCol).Caption)
                For iRow As Integer = 0 To dtData.Rows.Count - 1
                    Dim style As Style = cells(iRow + 1, iCol).GetStyle()
                    style.VerticalAlignment = TextAlignmentType.Center
                    Select Case dtData.Rows(iRow)(iCol).GetType
                        Case GetType(Date), GetType(Date?), GetType(DateTime), GetType(DateTime?)
                            style.Custom = "dd/MM/yyyy"
                            style.HorizontalAlignment = TextAlignmentType.Center
                        Case GetType(Int16), GetType(Int16?), GetType(Int32), GetType(Int32?), GetType(Int64), GetType(Int64?),
                            GetType(Integer), GetType(Integer?), GetType(Double), GetType(Double?), GetType(Decimal), GetType(Decimal?)
                            style.Number = 0
                            style.HorizontalAlignment = TextAlignmentType.Right
                        Case Else
                            style.Number = 49
                            style.HorizontalAlignment = TextAlignmentType.Left
                            style.IsTextWrapped = True
                    End Select

                    cells(iRow + 1, iCol).PutValue(dtData.Rows(iRow)(iCol))
                    cells(iRow + 1, iCol).SetStyle(style)
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
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
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
    Public Function ExportExcelTemplateNoData(ByVal filePath As String,
                                            ByVal fileName As String,
                                            ByVal Response As System.Web.HttpResponse,
                                            Optional ByVal tableName As String = "DATA",
                                            Optional ByRef _error As String = "",
                                            Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        'check license
        Dim designer As WorkbookDesigner
        Try
            If Not File.Exists(filePath) Then
                _error = CStr(1)
                Return False
            End If
            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.Process()
            designer.Workbook.CalculateFormula()
            With designer.Workbook
                .CalculateFormula()
                Select Case type
                    Case ExportType.Excel
                        .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                    Case ExportType.PDF
                        .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
                End Select
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Enum ExportType
        Excel = 0
        PDF = 1
    End Enum

#Region "Export Excel By Grid"
    'Public Function ExportExcelFromRadGridView(ByVal grid As RadGrid, 

    ''' <summary>
    ''' Xuất file Excel từ RadGridView, Nếu không có Item Rows thì không xuất
    ''' </summary>
    ''' <param name="grid">RadGridView</param>
    ''' <param name="dtData">DataSource</param>
    ''' <param name="sv">Server</param>
    ''' <param name="iRow">Dòng bắt đầu xuất dữ liệu</param>
    ''' <param name="sPath">đường dẫn chưa template (~/Folder/..../file.xls .xlsx)</param>
    ''' <returns>File excel đã xuất dữ liệu</returns>
    ''' <remarks></remarks>
    Public Function ExportExcelByRadGrid(ByVal grid As RadGrid,
                                         ByVal dtData As DataTable,
                                         ByVal fileName As String,
                                         ByVal sv As System.Web.HttpServerUtility,
                                         ByVal Response As System.Web.HttpResponse,
                                         Optional ByVal sPath As String = "",
                                         Optional ByVal iRow As Integer = 0,
                                         Optional ByVal type As ExportType = ExportType.Excel) As Boolean
        Dim sLink As String
        If sPath <> "" Then
            sLink = sv.MapPath(sPath)
        Else
            sLink = sv.MapPath("~/ReportTemplates/Common/Common.xls")
        End If
        Dim wb As Workbook = CType(ExportExcelNoTemplate(sLink, dtData, Nothing, False, False, Decimal.Parse(CStr(iRow))), Workbook)
        With wb
            .CalculateFormula()
            Select Case type
                Case ExportType.Excel
                    .Save(Response, fileName & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
                Case ExportType.PDF
                    .Save(Response, fileName & ".pdf", ContentDisposition.Attachment, New OoxmlSaveOptions(CType(FileFormatType.Pdf, SaveFormat)))
            End Select
        End With
    End Function

    ''' <summary>
    ''' Export RadGridView ra file excel không cần có dữ liệu DataSource
    ''' </summary>
    ''' <param name="filePath">Đường dẫn Template</param>
    ''' <param name="dtData">DataSource</param>
    ''' <param name="dtVariable">Header</param>
    ''' <param name="isGroup"></param>
    ''' <param name="isSum"></param>
    ''' <param name="iRowStartImport">Dòng dữ liệu xuất trên file excel</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportExcelNoTemplate(ByVal filePath As String,
                                        ByVal dtData As DataTable, _
                                        Optional ByVal dtVariable As DataTable = Nothing,
                                        Optional ByVal isGroup As Boolean = False,
                                        Optional ByVal isSum As Boolean = False,
                                        Optional ByVal iRowStartImport As Decimal = 0) As Aspose.Cells.Workbook
        Dim designer As WorkbookDesigner
        Try
            dtData.TableName = "DATA"
            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cells As Cells = designer.Workbook.Worksheets(0).Cells
            Dim style As Style
            For iCol As Integer = 0 To dtData.Columns.Count - 1
                Dim cell = cells(CInt(iRowStartImport), iCol)

                ExportExcelNoTemplate_SetHeaderValue(cell.GetStyle, iCol, iRowStartImport,
                                                     dtData.Columns(iCol).Caption, cells)

                For iRow As Integer = 0 To dtData.Rows.Count - 1
                    style = cells(CInt(iRowStartImport + iRow + 1), iCol).GetStyle()
                    cell = cells(CInt(iRowStartImport + iRow + 1), iCol)

                    If isGroup And dtData.Columns(iCol).ColumnName.Contains("GROUP_") Then
                        ExportExcelNoTemplate_SetStyle(style, StyleType.Group)
                        cell.PutValue(dtData.Rows(iRow)(iCol))
                        cell.SetStyle(style)
                    Else
                        If dtData.Columns(iCol).ColumnName = "STT" Then
                            Dim value As Double? = Nothing
                            ExportExcelNoTemplate_SetStyle(style, StyleType.Stt)
                            If dtData.Rows(iRow)(iCol) IsNot DBNull.Value Then
                                value = Double.Parse(CStr(dtData.Rows(iRow)(iCol)))
                            End If
                            cell.PutValue(value)
                            cell.SetStyle(style)
                        Else
                            Dim isSumGroup As Boolean = False
                            If dtData.Columns.Contains("GROUP_1") Then
                                isSumGroup = CBool(IIf(dtData.Rows(iRow)("GROUP_1") IsNot DBNull.Value, True, False))
                            End If
                            If Not isSumGroup AndAlso dtData.Columns.Contains("GROUP_2") Then
                                isSumGroup = CBool(IIf(dtData.Rows(iRow)("GROUP_2") IsNot DBNull.Value, True, False))
                            End If
                            If Not isSumGroup AndAlso dtData.Columns.Contains("GROUP_3") Then
                                isSumGroup = CBool(IIf(dtData.Rows(iRow)("GROUP_3") IsNot DBNull.Value, True, False))
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

    Private Sub ExportExcelNoTemplate_SetHeaderValue(ByVal style As Style, ByVal iCol As Decimal, ByVal iRowStartImport As Decimal,
                                                     ByVal HeaderText As String, ByVal cells As Cells)
        ExportExcelNoTemplate_SetStyle(style, StyleType.Header)
        cells(CInt(iRowStartImport), CInt(iCol)).PutValue(HeaderText)
        cells(CInt(iRowStartImport), CInt(iCol)).SetStyle(style)
    End Sub

    Private Sub ExportExcelNoTemplate_SetStyle(ByRef style As Style, Optional ByVal type As StyleType = StyleType.None)
        style.Borders(BorderType.TopBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.BottomBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.LeftBorder).LineStyle = CellBorderType.Thin
        style.Borders(BorderType.RightBorder).LineStyle = CellBorderType.Thin
        Select Case type
            Case StyleType.Header
                style.Font.IsBold = True
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
    End Sub

    Private Sub ExportExcelNoTemplate_ClearFooterTemp(ByVal dtData As DataTable, ByVal iCol As Integer, ByVal iRow As Integer,
                                                      ByVal iStart As Decimal, ByVal Cells As Cells)

        If iCol = dtData.Columns.Count - 1 And iRow = dtData.Rows.Count - 1 Then
            Dim sStt As String
            For ir As Integer = 0 To dtData.Rows.Count - 1
                sStt = CStr(dtData.Rows(ir)(0))
                If Not IsNumeric(sStt) AndAlso sStt.ToString.Contains("FOOTER") Then
                    Cells(CInt(iStart + ir + 1), 0).PutValue("")
                End If
            Next
        End If
    End Sub

    Private Sub ExportExcelNoTemplate_SetBodyValue(ByVal type As Type, ByVal value As Object, ByVal style As Style, ByVal iCol As Decimal,
                                               ByVal iRow As Decimal, ByVal iRowStartImport As Decimal, ByVal cells As Cells)
        ExportExcelNoTemplate_SetStyle(style, StyleType.Body)
        Select Case type
            Case GetType(Date), GetType(Date?), GetType(DateTime), GetType(DateTime?)
                Dim test As New TimeSpan(0, 0, 0)
                If value IsNot DBNull.Value AndAlso CType(value, DateTime).TimeOfDay <> test Then
                    style.Custom = "HH:mm"
                Else
                    style.Custom = "dd/MM/yyyy"
                End If

                style.HorizontalAlignment = TextAlignmentType.Center
            Case GetType(Int16), GetType(Int16?), GetType(Int32), GetType(Int32?), GetType(Int64), GetType(Int64?),
                GetType(Integer), GetType(Integer?), GetType(Double), GetType(Double?), GetType(Decimal), GetType(Decimal?)
                style.Number = 0
                style.HorizontalAlignment = TextAlignmentType.Right
            Case GetType(Boolean), GetType(Boolean?)
                style.Number = 49
                style.HorizontalAlignment = TextAlignmentType.Center
                If value IsNot DBNull.Value AndAlso value IsNot Nothing Then
                    value = "X"
                Else
                    value = ""
                End If
            Case Else
                style.Number = 49
                style.HorizontalAlignment = TextAlignmentType.Left
                style.IsTextWrapped = True
        End Select

        cells(CInt(iRowStartImport + iRow + 1), CInt(iCol)).PutValue(value)
        cells(CInt(iRowStartImport + iRow + 1), CInt(iCol)).SetStyle(style)
    End Sub
#End Region


    Public Enum StyleType
        None = 0
        Header = 1
        Group = 2
        Sum = 3
        Stt = 4
        Body = 5
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
