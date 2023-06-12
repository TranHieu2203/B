Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Framework.UI


Public Class Export
    Inherits System.Web.UI.Page

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Select Case Request.Params("id")
                    Case "Template_Import_HRBudgetDetail_error"
                        Template_Import_HRBudgetDetail_error()
                    Case "WorkShiftImport"
                        WorkShiftImport()
                    Case "Template_ImportShift_error"
                        Template_ImportShift_error()
                    Case "Template_Import_KPI_Assessment_Err"
                        Template_Import_KPI_Assessment_Err()
                    Case "Template_Import_HTCH_Assessment_Err"
                        Template_Import_HTCH_Assessment_Err()
                End Select
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
            End Try
        End If
    End Sub

#End Region

#Region "Process"

    Private Sub Template_Import_HRBudgetDetail_error()
        Try
            Dim dtData = Session("HR_BUDGET_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("HR_BUDGET_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Recruitment\Import\Template_Import_HRBudgetDetail_error.xls",
                                      dtData, dtVar,
                                      "TemplateImportHRBudgetError_" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WorkShiftImport()
        Try
            Dim rep As New Attendance.AttendanceRepository
            Dim org_id = Decimal.Parse(Request.Params("orgid"))
            Dim emp_obj = Decimal.Parse(Request.Params("emp_obj"))
            'Dim is_disolve = Boolean.Parse(Request.Params("IS_DISSOLVE"))
            Dim obj As New Attendance.AttendanceBusiness.ParamDTO


            obj.ORG_ID = org_id
            obj.IS_DISSOLVE = True
            'ANHVN
            'obj.P_EXPORT_TYPE = 6

            obj.P_EXPORT_TYPE = 12
            '----
            If Not String.IsNullOrEmpty(Request.Params("PERIOD_ID")) Then
                obj.PERIOD_ID = Decimal.Parse(Request.Params("PERIOD_ID"))
            End If
            Dim ddate = rep.Load_date(CDec(Val(obj.PERIOD_ID)), CDec(Val(emp_obj)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                obj.START_DATE = ddate.START_DATE
                obj.END_DATE = ddate.END_DATE
            End If
            obj.EMP_OBJ = emp_obj
            Dim dsData As DataSet = rep.GetDataFromOrg(obj)
            dsData.Tables(0).TableName = "Table"
            Dim dtvariable As New DataTable
            dtvariable.Columns.Add("D1", GetType(Date))
            dtvariable.Columns.Add("D2", GetType(Date))
            dtvariable.Columns.Add("D3", GetType(Date))
            dtvariable.Columns.Add("D4", GetType(Date))
            dtvariable.Columns.Add("D5", GetType(Date))
            dtvariable.Columns.Add("D6", GetType(Date))
            dtvariable.Columns.Add("D7", GetType(Date))
            dtvariable.Columns.Add("D8", GetType(Date))
            dtvariable.Columns.Add("D9", GetType(Date))
            dtvariable.Columns.Add("D10", GetType(Date))

            dtvariable.Columns.Add("D11", GetType(Date))
            dtvariable.Columns.Add("D12", GetType(Date))
            dtvariable.Columns.Add("D13", GetType(Date))
            dtvariable.Columns.Add("D14", GetType(Date))
            dtvariable.Columns.Add("D15", GetType(Date))
            dtvariable.Columns.Add("D16", GetType(Date))
            dtvariable.Columns.Add("D17", GetType(Date))
            dtvariable.Columns.Add("D18", GetType(Date))
            dtvariable.Columns.Add("D19", GetType(Date))
            dtvariable.Columns.Add("D20", GetType(Date))

            dtvariable.Columns.Add("D21", GetType(Date))
            dtvariable.Columns.Add("D22", GetType(Date))
            dtvariable.Columns.Add("D23", GetType(Date))
            dtvariable.Columns.Add("D24", GetType(Date))
            dtvariable.Columns.Add("D25", GetType(Date))
            dtvariable.Columns.Add("D26", GetType(Date))
            dtvariable.Columns.Add("D27", GetType(Date))
            dtvariable.Columns.Add("D28", GetType(Date))
            dtvariable.Columns.Add("D29", GetType(Date))
            dtvariable.Columns.Add("D30", GetType(Date))
            dtvariable.Columns.Add("D31", GetType(Date))

            Dim lsData As AT_PERIODDTO
            Dim period As New AT_PERIODDTO
            period.PERIOD_ID = obj.PERIOD_ID
            lsData = rep.LOAD_PERIODByID(period)
            Dim dDay = obj.START_DATE 'lsData.START_DATE
            Dim row = dtvariable.NewRow
            Dim i As Integer = 1
            While dDay <= obj.END_DATE 'lsData.END_DATE
                row("D" & i) = dDay.Value
                dDay = dDay.Value.AddDays(1)
                i = i + 1
            End While
            dtvariable.Rows.Add(row)
            dtvariable.TableName = "Variable"
            dsData.Tables.Add(dtvariable)
            Dim dtHoliday = rep.GetHoliday(New AT_HOLIDAYDTO).ToTable()
            dtHoliday.TableName = "HOLIDAY"
            dsData.Tables.Add(dtHoliday)
            ExportTemplate_XLSX("Attendance\Import\Template_ImportShift.xlsx",
                                      dsData, Nothing,
                                      "Template_ImportShift" & Format(Date.Now, "yyyyMMdd"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_ImportShift_error()
        Try
            Dim dsData As DataSet = Session("EXPORTREPORT")
            ExportTemplate("Attendance\Import\Template_ImportShift_error.xls",
                                      dsData, Nothing,
                                      "Template_ImportShift_error" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_KPI_Assessment_Err()
        Try
            Dim dtData = Session("KPI_ASSESSMENT_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("KPI_ASSESSMENT_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Performance\Import\Template_Import_KPI_Assessment_Err.xlsx",
                                      dtData, dtVar,
                                      "Template_Import_KPI_Assessment_Err" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Template_Import_HTCH_Assessment_Err()
        Try
            Dim dtData = Session("HTCH_ASSESSMENT_ERR")
            Dim dtVar As DataTable = dtData.Clone
            Session.Remove("HTCH_ASSESSMENT_ERR")
            If dtData.Rows.Count > 0 Then
                dtVar.ImportRow(dtData.Rows(0))
            End If
            ExportTemplate("Performance\Import\Template_Import_HTCH_Assessment_Err.xlsx",
                                      dtData, dtVar,
                                      "Template_Import_HTCH_Assessment_Err" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Common"

    Public Function ExportTemplateWithDataCol(ByVal sReportFileName As String,
                                                    ByVal dtDataValue As DataTable,
                                                    ByVal dtColname As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            dtDataValue.TableName = "DATA"

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            Dim cell As Cells = designer.Workbook.Worksheets(0).Cells
            Dim st As New Style
            st.Number = 3
            Dim i As Integer = 6
            For Each dr As DataRow In dtColname.Rows
                cell(1, i).PutValue(dr("COLNAME"))
                cell(2, i).PutValue(dr("COLVAL"))
                cell(3, i).PutValue(dr("COLDATA"))
                cell(3, i).SetStyle(st)
                i += 1
                cell.InsertColumn(i + 1)
            Next
            ' Xoa 2 cot thua cuoi cung
            cell.DeleteColumn(i + 1)
            cell.DeleteColumn(i)

            designer.SetDataSource(dtDataValue)
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Worksheets(0).AutoFitColumns()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dtData As DataTable,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

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
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function ExportTemplate_XLSX(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


#End Region

End Class