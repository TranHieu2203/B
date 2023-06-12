Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_HRBudgetDetail
    Inherits CommonView
    Protected WithEvents ProgramView As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Property HRYear_ID As Decimal
        Get
            Return ViewState(Me.ID & "_HRYear_ID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_HRYear_ID") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("RANK_SAL", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property dtDataImportHr As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportHr")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportHr") = value
        End Set
    End Property

    Property lstHrPlanDetail As List(Of HRPlaningDetailDTO)
        Get
            Return ViewState(Me.ID & "_lstHrPlanDetail")
        End Get
        Set(ByVal value As List(Of HRPlaningDetailDTO))
            ViewState(Me.ID & "_lstHrPlanDetail") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                GetParams()
                Refresh()
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = 50
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim rep As New RecruitmentRepository
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate, ToolbarItem.Import)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Import Ngân sách"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    ctrlOrg.Enabled = False
                    btnSearch.Enabled = False
                Case CommonMessage.STATE_NORMAL
                    ctrlOrg.Enabled = True
                    btnSearch.Enabled = True
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                If Not IsPostBack Then
                    Dim objYearPlan = rep.GetHrYearPlaningByID(HRYear_ID)
                    If objYearPlan IsNot Nothing Then
                        lbYear.Text = objYearPlan.YEAR
                        lbVersion.Text = objYearPlan.VERSION
                        lbEffectDate.Text = objYearPlan.EFFECT_DATE.Value.ToString("dd/MM/yyyy")
                    End If
                End If
                GetDataToCombo()
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
        Dim rep As New RecruitmentRepository
        Dim gID As Decimal
        Try
            Dim dtData As DataTable
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgData.SelectedItems
                        item.Edit = True
                    Next

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "HRPlaningBudgetDetail")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CANCEL
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = False
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lstObj As New List(Of HRPlaningDetailDTO)
                    For Each item As GridDataItem In rgData.EditItems
                        Dim objPL As New HRPlaningDetailDTO
                        Dim edit = CType(item, GridEditableItem)
                        Dim rnRankSal As RadNumericTextBox
                        rnRankSal = CType(edit.FindControl("RANK_SAL"), RadNumericTextBox)

                        objPL.ID = item.GetDataKeyValue("ID")

                        If IsNumeric(rnRankSal.Value) Then
                            objPL.RANK_SAL = rnRankSal.Value
                        End If
                        lstObj.Add(objPL)
                    Next
                    If rep.ModifyHRBudgetDetail(lstObj) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.EditItems
                            item.Edit = False
                        Next
                        rnRankSal.ClearValue()
                        rgData.Rebind()
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_HRDetail&ORGID=" & 1 & "&USERNAME=" & LogHelper.CurrentUser.USERNAME.ToUpper & "&ISDISSOLVE=0')", True)
                    If ctrlOrg.CurrentValue = "" Then
                        ShowMessage(Translate("Chưa chọn phòng ban để xuất file mẫu"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim store As New Recruitment.RecruitmentStoreProcedure
                    Dim dsData As DataSet = store.EXPORT_DATA_HR_DETAIL(CDec(ctrlOrg.CurrentValue), LogHelper.CurrentUser.USERNAME.ToUpper, ctrlOrg.IsDissolve, HRYear_ID)
                    ExportTemplate("Recruitment\Import\Template_Import_HRBudgetDetail.xls", _
                                              dsData, Nothing, _
                                              "DB_NGAN_SACH_" & Format(Date.Now, "yyyyMMdd"))
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim store As New RecruitmentRepository
        Dim dtData As New DataTable
        Try
            ClearControlValue(cboTitles)
            If ctrlOrg.CurrentValue <> "" Then
                dtData = store.GetTitleByOrgList(ctrlOrg.CurrentValue, True)
                FillRadCombobox(cboTitles, dtData, "NAME", "ID")
            End If
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

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If (TypeOf e.Item Is GridEditableItem) AndAlso (e.Item.IsInEditMode) Then
                Dim edititem As GridEditableItem = CType(e.Item, GridEditableItem)
                'Dim numtxtbx As RadNumericTextBox = CType(edititem("YEAR").Controls(0), RadNumericTextBox)
                'numtxtbx.NumberFormat.GroupSeparator = ""
            End If
            If e.Item.Edit Then
                Dim item = CType(e.Item, GridDataItem)
                Dim edit = CType(e.Item, GridEditableItem)
                Dim rnRankSal As RadNumericTextBox


                rnRankSal = CType(edit.FindControl("RANK_SAL"), RadNumericTextBox)


                If IsNumeric(item.GetDataKeyValue("RANK_SAL")) Then
                    rnRankSal.Value = CDec(item.GetDataKeyValue("RANK_SAL"))
                    rnRankSal.SkinID = "Money"
                    rnRankSal.Type = NumericType.Currency
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("ORG_ID<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("ORG_NAME")) OrElse rows("ORG_NAME") = "" Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("TITLE_ID") = rows("TITLE_ID")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("RANK_SAL") = rows("RANK_SAL")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New RecruitmentStoreProcedure
                If sp.IMPORT_HR_BUDGET_DETAIL(DocXml, HRYear_ID, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                rgData.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnAutoFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAutoFill.Click
        Try
            If IsNumeric(rnRankSal.Value) Then
                For Each item In rgData.EditItems
                    Dim edit = CType(item, GridEditableItem)
                    Dim rnGridRankSal As RadNumericTextBox
                    rnGridRankSal = CType(edit.FindControl("RANK_SAL"), RadNumericTextBox)
                    rnGridRankSal.Value = rnRankSal.Value
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New HRPlaningDetailDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of HRPlaningDetailDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If cboTitles.SelectedValue <> "" Then
                _filter.TITLE_ID = cboTitles.SelectedValue
            End If

            If IsNumeric(rnRankSal.Value) Then
                _filter.RANK_SAL = rnRankSal.Value
            End If

            _filter.YEAR_PLAN_ID = HRYear_ID
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.lstHrPlanDetail = rep.GetPlanDetail(_filter, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    Me.lstHrPlanDetail = rep.GetPlanDetail(_filter, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetPlanDetail(_filter, _param, Sorts).ToTable
                Else
                    Return rep.GetPlanDetail(_filter, _param).ToTable
                End If
            End If

            If Me.lstHrPlanDetail Is Nothing Then
                Me.lstHrPlanDetail = New List(Of HRPlaningDetailDTO)
            End If

            rgData.DataSource = Me.lstHrPlanDetail
            rgData.VirtualItemCount = MaximumRows

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetDataToCombo()
        Dim comboBoxDataDto As New ComboBoxDataDTO
        'Using rep As New RecruitmentRepository
        '    Dim dtData As DataTable
        '    dtData = rep.GetOtherList("RC_PROGRAM_STATUS", True)
        '    'FillRadCombobox(cboStatus, dtData, "NAME", "ID")
        'End Using
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                HRYear_ID = If(IsNothing(Request.Params("ID")), 0, Decimal.Parse(Request.Params("ID")))
                hidYearPLID.Value = HRYear_ID
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
        Dim rep As New RecruitmentRepository
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
            Dim iRow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn phòng ban"
                ImportValidate.EmptyValue("ORG_ID", row, rowError, isError, sError)

                sError = "Chưa chọn chức danh"
                ImportValidate.EmptyValue("TITLE_ID", row, rowError, isError, sError)


                If Not String.IsNullOrEmpty(row("RANK_SAL")) AndAlso Not IsNumeric(row("RANK_SAL")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("RANK_SAL", row, rowError, isError, sError)
                End If

                If Not String.IsNullOrEmpty(row("ORG_ID")) AndAlso Not String.IsNullOrEmpty(row("TITLE_ID")) Then
                    Dim checkRS = (From p In lstHrPlanDetail Where p.ORG_ID = CDec(row("ORG_ID")) AndAlso p.TITLE_ID = CDec(row("TITLE_ID"))).FirstOrDefault
                    If checkRS Is Nothing Then
                        sError = "Phòng ban và chức danh chưa được khởi tạo định biên chi tiết"
                        ImportValidate.IsValidTime("TITLE_ID", row, rowError, isError, sError)
                    End If
                End If


                If isError Then
                    ''rowError("ID") = iRow
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("HR_BUDGET_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_HRBudgetDetail_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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

#End Region

End Class