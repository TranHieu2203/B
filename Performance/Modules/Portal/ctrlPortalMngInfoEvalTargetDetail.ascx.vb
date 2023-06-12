Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPortalMngInfoEvalTargetDetail
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance\Modules\Portal" + Me.GetType().Name.ToString()
    Dim rep As New PerformanceRepository
    Private psp As New CommonRepository
#Region "Property"
    Public Property EmployeeID As Decimal

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("KPI_ASSESSMENT_TEXT", GetType(String))
                dt.Columns.Add("TARGET_TYPE_ID", GetType(String))
                dt.Columns.Add("TARGET_TYPE_NAME", GetType(String))
                dt.Columns.Add("UNIT_NAME", GetType(String))
                dt.Columns.Add("FREQUENCY_NAME", GetType(String))
                dt.Columns.Add("DESCRIPTION", GetType(String))
                dt.Columns.Add("SOURCE_NAME", GetType(String))
                dt.Columns.Add("GOAL_TYPE_NAME", GetType(String))
                dt.Columns.Add("TARGET", GetType(String))
                dt.Columns.Add("TARGET_MIN", GetType(String))
                dt.Columns.Add("RATIO", GetType(String))
                dt.Columns.Add("BENCHMARK", GetType(String))
                dt.Columns.Add("EMPLOYEE_ACTUAL", GetType(String))
                dt.Columns.Add("EMPLOYEE_POINT", GetType(String))
                dt.Columns.Add("DIRECT_ACTUAL", GetType(String))
                dt.Columns.Add("DIRECT_POINT", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                dt.Columns.Add("NOTE_QLTT", GetType(String))
                dt.Columns.Add("TARGET_TYPE_CODE", GetType(String))
                dt.Columns.Add("GOAL_TYPE_CODE", GetType(String))
                dt.Columns.Add("START_DATE", GetType(String))
                dt.Columns.Add("END_DATE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PerformanceRepository
        Try

        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        rgMngInfoEvalTarget.AllowCustomPaging = True
        rgMngInfoEvalTarget.PageSize = Common.Common.DefaultPageSize
        SetFilter(rgMngInfoEvalTarget)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataToCombo()
    End Sub

#End Region

#Region "Event"
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMngInfoEvalTarget.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMngInfoEvalTarget.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    If rgMngInfoEvalTarget.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsData As New DataSet
                    Dim dtTemp As New DataTable
                    dtTemp = dtData.Clone
                    For Each item As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                        Dim newRow As DataRow = dtTemp.NewRow
                        newRow("ID") = item.GetDataKeyValue("ID")
                        newRow("STATUS_NAME") = item.GetDataKeyValue("STATUS_NAME")
                        newRow("EMPLOYEE_CODE") = item.GetDataKeyValue("EMPLOYEE_CODE")
                        newRow("EMPLOYEE_NAME") = item.GetDataKeyValue("EMPLOYEE_NAME")
                        newRow("TITLE_NAME") = item.GetDataKeyValue("TITLE_NAME")
                        newRow("ORG_NAME") = item.GetDataKeyValue("ORG_NAME")
                        newRow("KPI_ASSESSMENT_TEXT") = item.GetDataKeyValue("KPI_ASSESSMENT_TEXT")
                        newRow("UNIT_NAME") = item.GetDataKeyValue("UNIT_NAME")
                        newRow("FREQUENCY_NAME") = item.GetDataKeyValue("FREQUENCY_NAME")
                        newRow("DESCRIPTION") = item.GetDataKeyValue("DESCRIPTION")
                        newRow("SOURCE_NAME") = item.GetDataKeyValue("SOURCE_NAME")
                        newRow("GOAL_TYPE_NAME") = item.GetDataKeyValue("GOAL_TYPE_NAME")
                        newRow("TARGET") = item.GetDataKeyValue("TARGET")
                        newRow("TARGET_MIN") = item.GetDataKeyValue("TARGET_MIN")
                        newRow("RATIO") = item.GetDataKeyValue("RATIO")
                        newRow("BENCHMARK") = item.GetDataKeyValue("BENCHMARK")
                        newRow("EMPLOYEE_ACTUAL") = item.GetDataKeyValue("EMPLOYEE_ACTUAL")
                        newRow("EMPLOYEE_POINT") = item.GetDataKeyValue("EMPLOYEE_POINT")
                        newRow("DIRECT_ACTUAL") = item.GetDataKeyValue("DIRECT_ACTUAL")
                        newRow("NOTE") = item.GetDataKeyValue("NOTE")
                        newRow("NOTE_QLTT") = item.GetDataKeyValue("NOTE_QLTT")
                        newRow("TARGET_TYPE_CODE") = item.GetDataKeyValue("TARGET_TYPE_CODE")
                        newRow("DIRECT_POINT") = item.GetDataKeyValue("DIRECT_POINT")
                        newRow("GOAL_TYPE_CODE") = item.GetDataKeyValue("GOAL_TYPE_CODE")
                        newRow("START_DATE") = If(IsDate(item.GetDataKeyValue("START_DATE")), CDate(item.GetDataKeyValue("START_DATE")).ToString("dd/MM/yyyy"), "")
                        newRow("END_DATE") = If(IsDate(item.GetDataKeyValue("END_DATE")), CDate(item.GetDataKeyValue("END_DATE")).ToString("dd/MM/yyyy"), "")
                        dtTemp.Rows.Add(newRow)
                    Next
                    dsData.Tables.Add(dtTemp)
                    ' DM loai danh gia
                    Dim dt = rep.GetOtherList("TYPE_CRITERIA", Common.Common.SystemLanguage.Name, False)
                    dsData.Tables.Add(dt)

                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)

                    ExportTemplate("Performance\Import\Template_Import_KPI_Assessment.xlsx",
                                              dsData, Nothing,
                                              "Template_Import_KPI_Assessment" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtDatas, "APPROVE_EVALUATE_TARGET")
                        End If
                    End Using
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub

    Protected Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
                rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
                rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
            Else
                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If
        Else
            cboPeriod.Text = ""
            cboPeriod.ClearSelection()
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
    End Sub

    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        If cboPeriod.SelectedValue <> "" Then
            Dim table As DataTable
            table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
            rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
            rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
        Else
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("ID").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("ID") = rows("ID")
                newRow("STATUS_NAME") = rows("STATUS_NAME")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("KPI_ASSESSMENT_TEXT") = rows("KPI_ASSESSMENT_TEXT")
                newRow("TARGET_TYPE_ID") = rows("TARGET_TYPE_ID")
                newRow("TARGET_TYPE_NAME") = rows("TARGET_TYPE_NAME")
                newRow("UNIT_NAME") = rows("UNIT_NAME")
                newRow("FREQUENCY_NAME") = rows("FREQUENCY_NAME")
                newRow("DESCRIPTION") = rows("DESCRIPTION")
                newRow("SOURCE_NAME") = rows("SOURCE_NAME")
                newRow("GOAL_TYPE_NAME") = rows("GOAL_TYPE_NAME")
                newRow("TARGET") = rows("TARGET")
                newRow("TARGET_MIN") = rows("TARGET_MIN")
                newRow("RATIO") = rows("RATIO")
                newRow("BENCHMARK") = rows("BENCHMARK")
                newRow("EMPLOYEE_ACTUAL") = rows("EMPLOYEE_ACTUAL")
                newRow("EMPLOYEE_POINT") = rows("EMPLOYEE_POINT")
                newRow("DIRECT_ACTUAL") = rows("DIRECT_ACTUAL")
                newRow("DIRECT_POINT") = rows("DIRECT_POINT")
                newRow("NOTE") = rows("NOTE")
                newRow("NOTE_QLTT") = rows("NOTE_QLTT")
                newRow("TARGET_TYPE_CODE") = rows("TARGET_TYPE_CODE")
                newRow("GOAL_TYPE_CODE") = rows("GOAL_TYPE_CODE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New PerformanceRepository
                If rep.IMPORT_KPI_ASSESSMENT(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgMngInfoEvalTarget.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PE_KPI_ASSESMENT_DETAIL_DTO
        Dim rep As New PerformanceRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            SetValueObjectByRadGrid(rgMngInfoEvalTarget, _filter)
            If cboYear.Text <> "" Then
                _filter.YEAR = cboYear.SelectedValue
            End If
            If cboPeriod.Text <> "" Then
                _filter.PE_PERIOD_ID = cboPeriod.SelectedValue
            End If
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            _filter.EMPLOYEE = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.Get_Portal_Target_Detail(_filter, Sorts).ToTable()
                Else
                    Return rep.Get_Portal_Target_Detail(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.Get_Portal_Target_Detail(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.Get_Portal_Target_Detail(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows)
                End If

                rgMngInfoEvalTarget.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetDataToCombo()
        Dim rep As New PerformanceRepository
        Dim reps As New PerformanceStoreProcedure
        Dim dtTable As New DataTable
        dtTable = reps.GetYear(False)
        FillRadCombobox(cboYear, dtTable, "YEAR", "ID")
        cboYear.SelectedValue = Date.Now.Year
        If cboYear.SelectedValue <> "" Then
            FillRadCombobox(cboPeriod, rep.GetPeriodByYear(cboYear.SelectedValue), "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim table As DataTable
                table = rep.GetDateByPeriod(cboPeriod.SelectedValue)
                rdFromDate.SelectedDate = If(table.Rows(0)(0).ToString() = "", Nothing, table.Rows(0)(0))
                rdToDate.SelectedDate = If(table.Rows(0)(1).ToString() = "", Nothing, table.Rows(0)(1))
            Else
                rdFromDate.SelectedDate = Nothing
                rdToDate.SelectedDate = Nothing
            End If
        Else
            cboPeriod.Text = ""
            cboPeriod.ClearSelection()
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
        End If
        Dim dtData = rep.GetOtherList("PROCESS_STATUS", Common.Common.SystemLanguage.Name, True)
        If dtData IsNot Nothing Then
            Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
            FillRadCombobox(cboStatus, data, "NAME", "ID")
            cboStatus.SelectedValue = PortalStatus.WaitingForApproval
        End If
    End Sub


    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim lstEmp As New List(Of String)
            dtError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập QL Đánh giá"
                ImportValidate.EmptyValue("DIRECT_ACTUAL", row, rowError, isError, sError)

                If Not IsDBNull(row("TARGET_TYPE_ID")) AndAlso Not String.IsNullOrEmpty(row("TARGET_TYPE_ID")) Then
                    If row("TARGET_TYPE_ID").ToString = "788641" Then
                        sError = "Chỉ được nhập số"
                        ImportValidate.IsValidNumber("DIRECT_ACTUAL", row, rowError, isError, sError)
                    Else
                        sError = "Chỉ được ngày"
                        ImportValidate.IsValidDate("DIRECT_ACTUAL", row, rowError, isError, sError)
                    End If
                End If

                If isError Then
                    ''rowError("ID") = iRow
                    rowError("STATUS_NAME") = row("STATUS_NAME").ToString
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("KPI_ASSESSMENT_TEXT") = row("KPI_ASSESSMENT_TEXT").ToString
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("KPI_ASSESSMENT_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_KPI_Assessment_Err')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            '_mylog.WriteLog(_mylog._info, _classPath, method,
            '                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex.ToString)
        End Try
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack();", True)
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
            'designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Xlsx))

            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region
End Class