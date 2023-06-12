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

Public Class ctrlPE_PortalApproveHTCHAssessmentDetail
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance\Modules\Portal" + Me.GetType().Name.ToString()
#Region "Property"
    Property dtPeriod As DataTable
        Get
            Return ViewState(Me.ID & "_dtPeriod")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtPeriod") = value
        End Set
    End Property

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
                dt.Columns.Add("CRITERIA_NAME", GetType(String))
                dt.Columns.Add("RATIO", GetType(String))
                dt.Columns.Add("POINTS_ACTUAL", GetType(String))
                dt.Columns.Add("RESULT_ACTUAL", GetType(String))
                dt.Columns.Add("POINTS_FINAL", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
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
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim rep As New PerformanceRepository
                    Dim objID As New List(Of Decimal)
                    For Each item In rgMngInfoEvalTarget.MasterTableView.GetSelectedItems
                        objID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteHTCHAssessment(objID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngInfoEvalTarget.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMngInfoEvalTarget.ExportExcel(Server, Response, dtDatas, "HTCH Assessment Detail")
                        End If
                    End Using

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
                        newRow("CRITERIA_NAME") = item.GetDataKeyValue("CRITERIA_NAME")
                        newRow("RATIO") = item.GetDataKeyValue("RATIO")
                        newRow("POINTS_ACTUAL") = item.GetDataKeyValue("POINTS_ACTUAL")
                        newRow("RESULT_ACTUAL") = item.GetDataKeyValue("RESULT_ACTUAL")
                        newRow("POINTS_FINAL") = item.GetDataKeyValue("POINTS_FINAL")
                        newRow("NOTE") = item.GetDataKeyValue("NOTE")
                        dtTemp.Rows.Add(newRow)
                    Next
                    dsData.Tables.Add(dtTemp)
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)

                    ExportTemplate("Performance\Import\Template_Import_HTCH_Assessment.xlsx",
                                              dsData, Nothing,
                                              "Template_Import_HTCH_Assessment" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim rep As New PerformanceRepository
            Dim store As New PerformanceStoreProcedure
            Dim strId As String
            Dim countSuccess As Integer
            Dim employeeID = LogHelper.CurrentUser.EMPLOYEE_ID
            If rgMngInfoEvalTarget.SelectedItems.Count = 1 Then

                Dim sItem As GridDataItem = rgMngInfoEvalTarget.SelectedItems(0)
                strId = sItem.GetDataKeyValue("ID").ToString


                Dim outNumber As Decimal

                Try
                    outNumber = rep.PRI_PROCESS_APP(employeeID, sItem.GetDataKeyValue("PE_PERIOD_ID"), "PERFORMANCE", 0, 0, 0, strId, "")
                Catch ex As Exception
                    ShowMessage(ex.ToString, NotifyType.Error)
                End Try

                If outNumber = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                ElseIf outNumber = 1 Then
                    ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Warning)
                ElseIf outNumber = 2 Then
                    ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
                ElseIf outNumber = 3 Then
                    ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
                End If
                Dim Is_Exits As Boolean = True
                If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                    Dim dt = store.GET_APP_ALL_TEMPLATES()
                    If dt IsNot Nothing Then
                        For Each item In dt.Rows
                            If Decimal.Parse(item("ID")) = outNumber Then
                                Is_Exits = False
                                ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                Exit For
                            End If
                        Next
                    End If
                End If
                If Is_Exits Then
                    Refresh("UpdateView")
                    UpdateControlState()
                    rgMngInfoEvalTarget.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            ElseIf rgMngInfoEvalTarget.SelectedItems.Count > 1 Then
                For Each dr As GridDataItem In rgMngInfoEvalTarget.SelectedItems
                    strId = ""
                    strId = dr.GetDataKeyValue("ID").ToString + ","
                    strId = strId.Remove(strId.LastIndexOf(",")).ToString
                    Dim period = CDec(dr.GetDataKeyValue("PE_PERIOD_ID"))
                    Dim outNumber As Decimal

                    Try
                        outNumber = rep.PRI_PROCESS_APP(employeeID, period, "PERFORMANCE", 0, 0, 0, strId, "")
                    Catch ex As Exception
                        ShowMessage(ex.ToString, NotifyType.Error)
                    End Try

                    Dim Is_Exits As Boolean = True
                    If outNumber <> 0 Or outNumber <> 1 Or outNumber <> 2 Then
                        Dim dt = store.GET_APP_ALL_TEMPLATES()
                        If dt IsNot Nothing Then
                            For Each item In dt.Rows
                                If Decimal.Parse(item("ID")) = outNumber Then
                                    Is_Exits = False
                                    'ShowMessage(Translate("Không tồn tại cấp phê duyệt theo template phê duyệt (" + item("TEMPLATE_NAME") + " )"), NotifyType.Warning)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    If Is_Exits Then
                        'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        countSuccess += 1
                    End If
                Next
                If countSuccess > 0 Then
                    Refresh("UpdateView")
                    UpdateControlState()


                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End If
            End If
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        ClearControlValue(rdFromDate, rdToDate)
        cboPeriod.Items.Clear()
        If cboYear.SelectedValue <> "" Then
            Dim rep As New PerformanceRepository
            dtPeriod = rep.GetPeriodHTCHByYear(cboYear.SelectedValue)
            FillRadCombobox(cboPeriod, dtPeriod, "NAME", "ID", True)
            If cboPeriod.SelectedValue <> "" Then
                Dim xRow = (From p In dtPeriod Where Not IsDBNull(p("ID")) AndAlso p("ID") = cboPeriod.SelectedValue).FirstOrDefault
                If Not IsNothing(xRow) Then
                    If Not IsDBNull(xRow("START_DATE")) Then
                        rdFromDate.SelectedDate = CDate(xRow("START_DATE"))
                    End If
                    If Not IsDBNull(xRow("END_DATE")) Then
                        rdToDate.SelectedDate = CDate(xRow("END_DATE"))
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        ClearControlValue(rdToDate, rdFromDate)
        If cboPeriod.SelectedValue <> "" Then
            Dim xRow = (From p In dtPeriod Where Not IsDBNull(p("ID")) AndAlso p("ID") = cboPeriod.SelectedValue).FirstOrDefault
            If Not IsNothing(xRow) Then
                If Not IsDBNull(xRow("START_DATE")) Then
                    rdFromDate.SelectedDate = CDate(xRow("START_DATE"))
                End If
                If Not IsDBNull(xRow("END_DATE")) Then
                    rdToDate.SelectedDate = CDate(xRow("END_DATE"))
                End If
            End If

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
                newRow("CRITERIA_NAME") = rows("CRITERIA_NAME")
                newRow("RATIO") = rows("RATIO")
                newRow("POINTS_ACTUAL") = rows("POINTS_ACTUAL")
                newRow("RESULT_ACTUAL") = rows("RESULT_ACTUAL")
                newRow("POINTS_FINAL") = rows("POINTS_FINAL")
                newRow("NOTE") = rows("NOTE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New PerformanceRepository
                If rep.IMPORT_HTCH_ASSESSMENT(DocXml) Then
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
        Dim _filter As New PE_HTCH_ASSESSMENT_DTL_DTO
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
                _filter.PERIOD_ID = cboPeriod.SelectedValue
            End If
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            _filter.EMPLOYEE_ID = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMngInfoEvalTarget.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetApproveHTCHAssessmentDetail(_filter, 0, Integer.MaxValue, 0, Sorts).ToTable()
                Else
                    Return rep.GetApproveHTCHAssessmentDetail(_filter, 0, Integer.MaxValue, 0).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgMngInfoEvalTarget.DataSource = rep.GetApproveHTCHAssessmentDetail(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows, Sorts)
                Else
                    rgMngInfoEvalTarget.DataSource = rep.GetApproveHTCHAssessmentDetail(_filter, rgMngInfoEvalTarget.CurrentPageIndex, rgMngInfoEvalTarget.PageSize, MaximumRows)
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
        Dim dtTable As New DataTable
        dtTable = rep.GetYearHTCH(True)
        FillRadCombobox(cboYear, dtTable, "YEAR", "YEAR")
        cboYear.SelectedValue = Date.Now.Year
        If cboYear.SelectedValue <> "" Then
            dtPeriod = rep.GetPeriodHTCHByYear(cboYear.SelectedValue)
            FillRadCombobox(cboPeriod, dtPeriod, "NAME", "ID", True)
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
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Cancel).ToString()).CopyToDataTable()
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
                sError = "Chưa nhập Kết quả import"
                ImportValidate.EmptyValue("RESULT_ACTUAL", row, rowError, isError, sError)

                If Not IsDBNull(row("POINTS_ACTUAL")) AndAlso Not String.IsNullOrEmpty(row("POINTS_ACTUAL")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("POINTS_ACTUAL", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("POINTS_FINAL")) AndAlso Not String.IsNullOrEmpty(row("POINTS_FINAL")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("POINTS_FINAL", row, rowError, isError, sError)
                End If

                If isError Then
                    ''rowError("ID") = iRow
                    rowError("STATUS_NAME") = row("STATUS_NAME").ToString
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("CRITERIA_NAME") = row("CRITERIA_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("HTCH_ASSESSMENT_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_HTCH_Assessment_Err')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
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