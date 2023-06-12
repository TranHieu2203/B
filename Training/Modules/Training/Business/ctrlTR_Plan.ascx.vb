Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlTR_Plan
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Training\Modules\Business" + Me.GetType().Name.ToString()
#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("TR_PLAN_CODE", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("ORG_CODE", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("PLAN_TYPE", GetType(String))
                dt.Columns.Add("TR_REQUEST_ID", GetType(String))
                dt.Columns.Add("TR_COURSE_NAME", GetType(String))
                dt.Columns.Add("TR_COURSE_ID", GetType(String))
                dt.Columns.Add("TR_TRAIN_FORM_NAME", GetType(String))
                dt.Columns.Add("TR_TRAIN_FORM_ID", GetType(String))
                dt.Columns.Add("TR_PROPERTIES_NEED_NAME", GetType(String))
                dt.Columns.Add("TR_PROPERTIES_NEED_ID", GetType(String))
                dt.Columns.Add("TR_TRAIN_FIELD_NAME", GetType(String))
                dt.Columns.Add("TR_TRAIN_FIELD_ID", GetType(String))
                dt.Columns.Add("VENUE", GetType(String))
                dt.Columns.Add("CONTENT", GetType(String))
                dt.Columns.Add("TARGET_TRAIN", GetType(String))
                dt.Columns.Add("EXPECT_TR_FROM", GetType(String))
                dt.Columns.Add("EXPECT_TR_TO", GetType(String))
                dt.Columns.Add("TR_COMMIT", GetType(String))
                dt.Columns.Add("STUDENT_NUMBER", GetType(String))
                dt.Columns.Add("COST_TOTAL", GetType(String))
                dt.Columns.Add("TR_CURRENCY_NAME", GetType(String))
                dt.Columns.Add("TR_CURRENCY_ID", GetType(String))
                dt.Columns.Add("EXPECT_CLASS", GetType(String))
                dt.Columns.Add("CENTER", GetType(String))
                dt.Columns.Add("CENTER_ID", GetType(String))
                dt.Columns.Add("CERTIFICATE", GetType(String))
                dt.Columns.Add("CERTIFICATE_NAME", GetType(String))
                dt.Columns.Add("TR_AFTER_TRAIN", GetType(String))
                dt.Columns.Add("TR_TYPE_NAME", GetType(String))
                dt.Columns.Add("TR_TYPE_ID", GetType(String))
                dt.Columns.Add("DAY_REVIEW_1", GetType(String))
                dt.Columns.Add("DAY_REVIEW_2", GetType(String))
                dt.Columns.Add("DAY_REVIEW_3", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            MainToolBar.Items(4).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(5).Text = Translate("Nhập file mẫu")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    If rep.DeletePlans(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rntYear.Value = Date.Now.Year
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    'CurrentState = CommonMessage.STATE_NEW
                    'UpdateControlState()
                    ctrlMessageBox.Ok_text = "Theo nhu cầu"
                    ctrlMessageBox.Ok_CSS = "width:90px"
                    ctrlMessageBox.Cancel_text = "Đột xuất"
                    ctrlMessageBox.MessageTitle = "Thông báo"
                    ctrlMessageBox.MessageText = "Xin vui lòng chọn loại kế hoạch"
                    ctrlMessageBox.ActionName = "CONFIRM"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE

                    Dim repHF = New HistaffFrameworkRepository
                    Dim dtData1 As New DataTable
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        dtData1 = repHF.ExecuteToDataSet("PKG_TRAINING.PLAN_CHECK_REQUEST", New List(Of Object)({item.GetDataKeyValue("ID")})).Tables(0)
                        If dtData1 IsNot Nothing Then
                            If dtData1.Rows.Count >= 1 Then
                                ShowMessage(Translate("Kế hoạch thuộc Yêu cầu đào tạo đã được phê duyệt, xin thử lại."), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim rep As New TrainingRepository
                    Dim dsData = rep.GET_PLAN_DATA_IMPORT(1)
                    ExportTemplate("Training\Import\Template_Import_TR_Plan.xls",
                                              dsData, Nothing,
                                              "Template_Import_TR_Plan" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "Plan")
                        End If
                    End Using
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = "CONFIRM" Then
                If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1&typeConfirm=NC")
                ElseIf e.ButtonID = MessageBoxButtonType.ButtonNo Then
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1&typeConfirm=DX")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("STT <>'""' ").CopyToDataTable.Rows
                If String.IsNullOrEmpty(rows("TR_PLAN_CODE").ToString) AndAlso String.IsNullOrEmpty(rows("YEAR").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("TR_PLAN_CODE") = rows("TR_PLAN_CODE")
                newRow("YEAR") = rows("YEAR")
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("ORG_CODE") = rows("ORG_CODE")
                newRow("PLAN_TYPE") = rows("PLAN_TYPE")
                newRow("TR_REQUEST_ID") = rows("TR_REQUEST_ID")
                newRow("TR_COURSE_NAME") = rows("TR_COURSE_NAME")
                newRow("TR_COURSE_ID") = rows("TR_COURSE_ID")
                newRow("TR_TRAIN_FORM_NAME") = rows("TR_TRAIN_FORM_NAME")
                newRow("TR_TRAIN_FORM_ID") = rows("TR_TRAIN_FORM_ID")
                newRow("TR_PROPERTIES_NEED_NAME") = rows("TR_PROPERTIES_NEED_NAME")
                newRow("TR_PROPERTIES_NEED_ID") = rows("TR_PROPERTIES_NEED_ID")
                newRow("TR_TRAIN_FIELD_NAME") = rows("TR_TRAIN_FIELD_NAME")
                newRow("TR_TRAIN_FIELD_ID") = rows("TR_TRAIN_FIELD_ID")
                newRow("VENUE") = rows("VENUE")
                newRow("CONTENT") = rows("CONTENT")
                newRow("TARGET_TRAIN") = rows("TARGET_TRAIN")
                newRow("EXPECT_TR_FROM") = rows("EXPECT_TR_FROM")
                newRow("EXPECT_TR_TO") = rows("EXPECT_TR_TO")
                newRow("TR_COMMIT") = rows("TR_COMMIT")
                newRow("STUDENT_NUMBER") = rows("STUDENT_NUMBER")
                newRow("COST_TOTAL") = rows("COST_TOTAL")
                newRow("TR_CURRENCY_NAME") = rows("TR_CURRENCY_NAME")
                newRow("TR_CURRENCY_ID") = rows("TR_CURRENCY_ID")
                newRow("EXPECT_CLASS") = rows("EXPECT_CLASS")
                newRow("CENTER") = rows("CENTER")
                newRow("CENTER_ID") = rows("CENTER_ID")
                newRow("CERTIFICATE") = rows("CERTIFICATE")
                newRow("CERTIFICATE_NAME") = rows("CERTIFICATE_NAME")
                newRow("TR_AFTER_TRAIN") = rows("TR_AFTER_TRAIN")
                newRow("TR_TYPE_NAME") = rows("TR_TYPE_NAME")
                newRow("TR_TYPE_ID") = rows("TR_TYPE_ID")
                newRow("DAY_REVIEW_1") = rows("DAY_REVIEW_1")
                newRow("DAY_REVIEW_2") = rows("DAY_REVIEW_2")
                newRow("DAY_REVIEW_3") = rows("DAY_REVIEW_3")
                newRow("REMARK") = rows("REMARK")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New TrainingRepository
                If rep.IMPORT_TR_PLAN(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub rgData_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.item, GridDataItem)
                datarow("TARGET_TRAIN").Text = datarow("TARGET_TRAIN").Text.Replace(vbCrLf, "<br/>")
                datarow("CONTENT").Text = datarow("CONTENT").Text.Replace(vbCrLf, "<br/>")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PlanDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of PlanDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.YEAR = rntYear.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of PlanDTO)
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstData = rep.GetPlans(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetPlans(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
            Else
                Return rep.GetPlans(_filter, 0, rgData.PageSize, Integer.MaxValue, _param).ToTable
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(1)
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
        dtdata.Rows(0).Delete()
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
                sError = "Chưa nhập mã kế hoạch"
                ImportValidate.EmptyValue("TR_PLAN_CODE", row, rowError, isError, sError)

                sError = "Chưa nhập năm"
                ImportValidate.EmptyValue("YEAR", row, rowError, isError, sError)

                sError = "Chưa chọn loại kế hoạch"
                ImportValidate.EmptyValue("PLAN_TYPE", row, rowError, isError, sError)

                If Not IsDBNull(row("PLAN_TYPE")) AndAlso Not String.IsNullOrEmpty(row("PLAN_TYPE")) Then
                    If row("PLAN_TYPE") = 0 Then
                        sError = "Chưa chọn Nhu cầu đào tạo"
                        ImportValidate.EmptyValue("TR_REQUEST_ID", row, rowError, isError, sError)
                    ElseIf row("PLAN_TYPE") = -1 Then
                        sError = "Chưa chọn Khóa đào tạo"
                        ImportValidate.EmptyValue("TR_COURSE_NAME", row, rowError, isError, sError)
                    End If
                End If

                If Not IsDBNull(row("CERTIFICATE")) AndAlso Not String.IsNullOrEmpty(row("CERTIFICATE")) Then
                    If row("CERTIFICATE") = -1 Then
                        sError = "Chưa nhập tên Bằng cấp/Chứng chỉ"
                        ImportValidate.EmptyValue("CERTIFICATE_NAME", row, rowError, isError, sError)
                    End If
                End If

                '' check date
                If Not IsDBNull(row("EXPECT_TR_FROM")) AndAlso Not String.IsNullOrEmpty(row("EXPECT_TR_FROM")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("EXPECT_TR_FROM", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("EXPECT_TR_TO")) AndAlso Not String.IsNullOrEmpty(row("EXPECT_TR_TO")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("EXPECT_TR_TO", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("DAY_REVIEW_1")) AndAlso Not String.IsNullOrEmpty(row("DAY_REVIEW_1")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("DAY_REVIEW_1", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("DAY_REVIEW_2")) AndAlso Not String.IsNullOrEmpty(row("DAY_REVIEW_2")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("DAY_REVIEW_2", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("DAY_REVIEW_3")) AndAlso Not String.IsNullOrEmpty(row("DAY_REVIEW_3")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("DAY_REVIEW_3", row, rowError, isError, sError)
                End If

                '' check number
                If Not IsDBNull(row("STUDENT_NUMBER")) AndAlso Not String.IsNullOrEmpty(row("STUDENT_NUMBER")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("STUDENT_NUMBER", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("COST_TOTAL")) AndAlso Not String.IsNullOrEmpty(row("COST_TOTAL")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("COST_TOTAL", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("EXPECT_CLASS")) AndAlso Not String.IsNullOrEmpty(row("EXPECT_CLASS")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("EXPECT_CLASS", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    If IsDBNull(rowError("TR_PLAN_CODE")) Then
                        rowError("TR_PLAN_CODE") = row("TR_PLAN_CODE").ToString
                    End If
                    If IsDBNull(rowError("YEAR")) Then
                        rowError("YEAR") = row("YEAR").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("TR_PLAN_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TR_Plan_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class