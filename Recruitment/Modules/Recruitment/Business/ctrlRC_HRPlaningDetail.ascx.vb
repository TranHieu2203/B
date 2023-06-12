Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_HRPlaningDetail
    Inherits Common.CommonView
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
                dt.Columns.Add("MONTH_1", GetType(String))
                dt.Columns.Add("MONTH_2", GetType(String))
                dt.Columns.Add("MONTH_3", GetType(String))
                dt.Columns.Add("MONTH_4", GetType(String))
                dt.Columns.Add("MONTH_5", GetType(String))
                dt.Columns.Add("MONTH_6", GetType(String))
                dt.Columns.Add("MONTH_7", GetType(String))
                dt.Columns.Add("MONTH_8", GetType(String))
                dt.Columns.Add("MONTH_9", GetType(String))
                dt.Columns.Add("MONTH_10", GetType(String))
                dt.Columns.Add("MONTH_11", GetType(String))
                dt.Columns.Add("MONTH_12", GetType(String))
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
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(6), RadToolBarButton).Text = "Import định biên"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteHRPlanDetail(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
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
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If rep.CheckExistRankSal(item.GetDataKeyValue("ID")) Then
                            ShowMessage(Translate("Tồn tại bản ghi đã khởi tạo dữ liệu định biên Ngân sách, không thể xóa"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "HRPlaningDetail")
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
                        If Not rep.ValidateHRPlanDetail(item.GetDataKeyValue("ORG_ID"), item.GetDataKeyValue("TITLE_ID"), item.GetDataKeyValue("YEAR_PLAN_ID"), item.GetDataKeyValue("ID")) Then
                            ShowMessage(Translate("Đã tồn tại dữ liệu về chức danh " & item.GetDataKeyValue("TITLE_NAME") & " trong phòng ban này"), NotifyType.Error)
                            Exit Sub
                        End If
                        If Not rep.CheckExistRankSal(item.GetDataKeyValue("ID")) Then
                            ShowMessage(Translate("Chức danh " & item.GetDataKeyValue("TITLE_NAME") & " đã được khởi tạo dữ liệu ngân sách, không thể chỉnh sửa"), NotifyType.Error)
                            Exit Sub
                        End If
                    Next
                    For Each item As GridDataItem In rgData.EditItems
                        Dim objPL As New HRPlaningDetailDTO
                        Dim edit = CType(item, GridEditableItem)
                        Dim rnMonth1 As RadNumericTextBox
                        Dim rnMonth2 As RadNumericTextBox
                        Dim rnMonth3 As RadNumericTextBox
                        Dim rnMonth4 As RadNumericTextBox
                        Dim rnMonth5 As RadNumericTextBox
                        Dim rnMonth6 As RadNumericTextBox
                        Dim rnMonth7 As RadNumericTextBox
                        Dim rnMonth8 As RadNumericTextBox
                        Dim rnMonth9 As RadNumericTextBox
                        Dim rnMonth10 As RadNumericTextBox
                        Dim rnMonth11 As RadNumericTextBox
                        Dim rnMonth12 As RadNumericTextBox

                        rnMonth1 = CType(edit.FindControl("MONTH_1"), RadNumericTextBox)
                        rnMonth2 = CType(edit.FindControl("MONTH_2"), RadNumericTextBox)
                        rnMonth3 = CType(edit.FindControl("MONTH_3"), RadNumericTextBox)
                        rnMonth4 = CType(edit.FindControl("MONTH_4"), RadNumericTextBox)
                        rnMonth5 = CType(edit.FindControl("MONTH_5"), RadNumericTextBox)
                        rnMonth6 = CType(edit.FindControl("MONTH_6"), RadNumericTextBox)
                        rnMonth7 = CType(edit.FindControl("MONTH_7"), RadNumericTextBox)
                        rnMonth8 = CType(edit.FindControl("MONTH_8"), RadNumericTextBox)
                        rnMonth9 = CType(edit.FindControl("MONTH_9"), RadNumericTextBox)
                        rnMonth10 = CType(edit.FindControl("MONTH_10"), RadNumericTextBox)
                        rnMonth11 = CType(edit.FindControl("MONTH_11"), RadNumericTextBox)
                        rnMonth12 = CType(edit.FindControl("MONTH_12"), RadNumericTextBox)

                        objPL.ID = item.GetDataKeyValue("ID")
                        objPL.ORG_ID = item.GetDataKeyValue("ORG_ID")
                        objPL.TITLE_ID = item.GetDataKeyValue("TITLE_ID")
                        objPL.YEAR_PLAN_ID = item.GetDataKeyValue("YEAR_PLAN_ID")
                        If IsNumeric(rnMonth1.Value) Then
                            objPL.MONTH_1 = rnMonth1.Value
                        End If
                        If IsNumeric(rnMonth2.Value) Then
                            objPL.MONTH_2 = rnMonth2.Value
                        End If
                        If IsNumeric(rnMonth3.Value) Then
                            objPL.MONTH_3 = rnMonth3.Value
                        End If
                        If IsNumeric(rnMonth4.Value) Then
                            objPL.MONTH_4 = rnMonth4.Value
                        End If
                        If IsNumeric(rnMonth5.Value) Then
                            objPL.MONTH_5 = rnMonth5.Value
                        End If
                        If IsNumeric(rnMonth6.Value) Then
                            objPL.MONTH_6 = rnMonth6.Value
                        End If
                        If IsNumeric(rnMonth7.Value) Then
                            objPL.MONTH_7 = rnMonth7.Value
                        End If
                        If IsNumeric(rnMonth8.Value) Then
                            objPL.MONTH_8 = rnMonth8.Value
                        End If
                        If IsNumeric(rnMonth9.Value) Then
                            objPL.MONTH_9 = rnMonth9.Value
                        End If
                        If IsNumeric(rnMonth10.Value) Then
                            objPL.MONTH_10 = rnMonth10.Value
                        End If
                        If IsNumeric(rnMonth11.Value) Then
                            objPL.MONTH_11 = rnMonth11.Value
                        End If
                        If IsNumeric(rnMonth12.Value) Then
                            objPL.MONTH_12 = rnMonth12.Value
                        End If
                        lstObj.Add(objPL)
                    Next
                    If rep.ModifyHRPlanDetail(lstObj) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.EditItems
                            item.Edit = False
                        Next
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
                    ExportTemplate("Recruitment\Import\Template_Import_HRDetail.xls", _
                                              dsData, Nothing, _
                                              "DB_NHAN_SU_" & Format(Date.Now, "yyyyMMdd"))
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim store As New RecruitmentStoreProcedure
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
                Dim rnMonth1 As RadNumericTextBox
                Dim rnMonth2 As RadNumericTextBox
                Dim rnMonth3 As RadNumericTextBox
                Dim rnMonth4 As RadNumericTextBox
                Dim rnMonth5 As RadNumericTextBox
                Dim rnMonth6 As RadNumericTextBox
                Dim rnMonth7 As RadNumericTextBox
                Dim rnMonth8 As RadNumericTextBox
                Dim rnMonth9 As RadNumericTextBox
                Dim rnMonth10 As RadNumericTextBox
                Dim rnMonth11 As RadNumericTextBox
                Dim rnMonth12 As RadNumericTextBox

                rnMonth1 = CType(edit.FindControl("MONTH_1"), RadNumericTextBox)
                rnMonth2 = CType(edit.FindControl("MONTH_2"), RadNumericTextBox)
                rnMonth3 = CType(edit.FindControl("MONTH_3"), RadNumericTextBox)
                rnMonth4 = CType(edit.FindControl("MONTH_4"), RadNumericTextBox)
                rnMonth5 = CType(edit.FindControl("MONTH_5"), RadNumericTextBox)
                rnMonth6 = CType(edit.FindControl("MONTH_6"), RadNumericTextBox)
                rnMonth7 = CType(edit.FindControl("MONTH_7"), RadNumericTextBox)
                rnMonth8 = CType(edit.FindControl("MONTH_8"), RadNumericTextBox)
                rnMonth9 = CType(edit.FindControl("MONTH_9"), RadNumericTextBox)
                rnMonth10 = CType(edit.FindControl("MONTH_10"), RadNumericTextBox)
                rnMonth11 = CType(edit.FindControl("MONTH_11"), RadNumericTextBox)
                rnMonth12 = CType(edit.FindControl("MONTH_12"), RadNumericTextBox)

                If IsNumeric(item.GetDataKeyValue("MONTH_1")) Then
                    rnMonth1.Value = CDec(item.GetDataKeyValue("MONTH_1"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_2")) Then
                    rnMonth2.Value = CDec(item.GetDataKeyValue("MONTH_2"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_3")) Then
                    rnMonth3.Value = CDec(item.GetDataKeyValue("MONTH_3"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_4")) Then
                    rnMonth4.Value = CDec(item.GetDataKeyValue("MONTH_4"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_5")) Then
                    rnMonth5.Value = CDec(item.GetDataKeyValue("MONTH_5"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_6")) Then
                    rnMonth6.Value = CDec(item.GetDataKeyValue("MONTH_6"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_7")) Then
                    rnMonth7.Value = CDec(item.GetDataKeyValue("MONTH_7"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_8")) Then
                    rnMonth8.Value = CDec(item.GetDataKeyValue("MONTH_8"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_9")) Then
                    rnMonth9.Value = CDec(item.GetDataKeyValue("MONTH_9"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_10")) Then
                    rnMonth10.Value = CDec(item.GetDataKeyValue("MONTH_10"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_11")) Then
                    rnMonth11.Value = CDec(item.GetDataKeyValue("MONTH_11"))
                End If
                If IsNumeric(item.GetDataKeyValue("MONTH_12")) Then
                    rnMonth12.Value = CDec(item.GetDataKeyValue("MONTH_12"))
                End If
            End If
        Catch ex As Exception
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("ORG_ID<>'""'").CopyToDataTable.Rows
                If rows("ORG_ID").ToString.ToUpper.Equals("#N/A") OrElse rows("TITLE_ID").ToString.ToUpper.Equals("#N/A") Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("TITLE_ID") = rows("TITLE_ID")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("MONTH_1") = rows("MONTH_1")
                newRow("MONTH_2") = rows("MONTH_2")
                newRow("MONTH_3") = rows("MONTH_3")
                newRow("MONTH_4") = rows("MONTH_4")
                newRow("MONTH_5") = rows("MONTH_5")
                newRow("MONTH_6") = rows("MONTH_6")
                newRow("MONTH_7") = rows("MONTH_7")
                newRow("MONTH_8") = rows("MONTH_8")
                newRow("MONTH_9") = rows("MONTH_9")
                newRow("MONTH_10") = rows("MONTH_10")
                newRow("MONTH_11") = rows("MONTH_11")
                newRow("MONTH_12") = rows("MONTH_12")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New RecruitmentStoreProcedure
                If sp.IMPORT_HR_DETAIL(DocXml, HRYear_ID, LogHelper.GetUserLog().Username.ToUpper) Then
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


                If Not String.IsNullOrEmpty(row("MONTH_1")) AndAlso Not IsNumeric(row("MONTH_1")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_1", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_2")) AndAlso Not IsNumeric(row("MONTH_2")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_2", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_3")) AndAlso Not IsNumeric(row("MONTH_3")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_3", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_4")) AndAlso Not IsNumeric(row("MONTH_4")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_4", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_5")) AndAlso Not IsNumeric(row("MONTH_5")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_5", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_6")) AndAlso Not IsNumeric(row("MONTH_6")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_6", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_7")) AndAlso Not IsNumeric(row("MONTH_7")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_7", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_8")) AndAlso Not IsNumeric(row("MONTH_8")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_8", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_9")) AndAlso Not IsNumeric(row("MONTH_9")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_9", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_10")) AndAlso Not IsNumeric(row("MONTH_10")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_10", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_11")) AndAlso Not IsNumeric(row("MONTH_11")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_11", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("MONTH_12")) AndAlso Not IsNumeric(row("MONTH_12")) Then
                    sError = "Nhập sai định dạng, bạn phải nhập số"
                    ImportValidate.IsValidTime("MONTH_12", row, rowError, isError, sError)
                End If
                If Not String.IsNullOrEmpty(row("ORG_ID")) AndAlso Not String.IsNullOrEmpty(row("TITLE_ID")) Then
                    Dim checkRS = (From p In lstHrPlanDetail Where p.ORG_ID = CDec(row("ORG_ID")) AndAlso p.TITLE_ID = CDec(row("TITLE_ID")) AndAlso p.RANK_SAL IsNot Nothing).FirstOrDefault
                    If checkRS IsNot Nothing Then
                        sError = "Dữ liệu đã được khởi tạo định biên ngân sách"
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
                Session("HR_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_HRDetail_error')", True)
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