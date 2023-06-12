Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlTR_TitleCourse
    Inherits Common.CommonView
    Protected WithEvents ExamsDtlView As ViewBase
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

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            'dic.Add("TITLE_GROUP_ID", cboTitleGroup)
            'dic.Add("HU_TITLE_ID", cboTitle)

            dic.Add("TITLE_GROUP_NAME", txtGroupTitle)
            dic.Add("HU_TITLE_NAME", txtTitle)

            dic.Add("TR_COURSE_ID", cboCourse)
            dic.Add("TR_COURSE_REMARK", txtNote)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("IS_CHECK", chkIsCheck)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            EnableRadCombo(cboCourse, False)
            'rgData.DataSource = Nothing
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Delete)
            MainToolBar.Items(5).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(6).Text = Translate("Nhập file mẫu")
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub showcontrol(ByVal ishow As Boolean)
        Label3.Visible = ishow
        Label2.Visible = ishow
        cboTitleGroup.Visible = ishow
        cboTitle.Visible = ishow

        lbCode.Visible = Not ishow
        txtGroupTitle.Visible = Not ishow
        Label1.Visible = Not ishow
        txtTitle.Visible = Not ishow

    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    showcontrol(False)

                    EnableControlAll(False, txtNote, rdEffectDate, chkIsCheck, cboCourse)
                    EnableControlAll(False, cboTitleGroup, cboTitle)
                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    showcontrol(True)


                    EnableRadCombo(cboTitleGroup, True)
                    EnableRadCombo(cboTitle, True)
                    EnableRadCombo(cboCourse, True)
                    EnableControlAll(True, txtNote, rdEffectDate, chkIsCheck)
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim objDelete As TitleCourseDTO
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    objDelete = New TitleCourseDTO With {.ID = item.GetDataKeyValue("ID")}
                    If rep.DeleteTitleCourse(objDelete) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
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

    Private Sub GetDataCombo()
        Dim rep As New TrainingRepository
        Dim dtData As DataTable
        Dim dtCourse As DataTable
        Try
            dtData = rep.GetOtherList("HU_TITLE_GROUP")
            FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

            'Dim reps As New TrainingStoreProcedure
            'Dim dt = reps.GetTitleByGroup(CDec(Val(cboTitleGroup.SelectedValue)))
            'FillRadCombobox(cboTitle, dt, "NAME_VN", "ID")

            dtCourse = rep.GetCourseByList()
            FillRadCombobox(cboCourse, dtCourse, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New TrainingRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    'If cboTitleGroup.SelectedValue = "" Then
                    '    ShowMessage(Translate("Bạn phải chọn Nhóm chức danh"), Utilities.NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    CurrentState = CommonMessage.STATE_NEW
                    cboCourse.SelectedValue = Nothing
                    hidID.Value = ""
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New TitleCourseDTO
                        If hidID.Value <> "" Then
                            obj.ID = Decimal.Parse(hidID.Value)
                        End If
                        obj.TR_COURSE_ID = cboCourse.SelectedValue
                        If cboTitle.SelectedValue <> "" Then
                            obj.HU_TITLE_ID = cboTitle.SelectedValue
                        Else
                            obj.HU_TITLE_ID = 0
                        End If
                        obj.TR_COURSE_REMARK = txtNote.Text
                        If cboTitleGroup.SelectedValue <> "" Then
                            obj.TITLE_GROUP_ID = cboTitleGroup.SelectedValue
                        End If
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.IS_CHECK = chkIsCheck.Checked
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                            Case CommonMessage.STATE_EDIT
                                obj.ACTFLG = "A"
                        End Select
                        If rep.UpdateTitleCourse(obj) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    cboCourse.SelectedValue = Nothing
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dsData = rep.GET_TITLE_COURSE_IMPORT()
                    ExportTemplate("Training\Import\Import_TitleCourse.xlsx",
                                              dsData, Nothing,
                                              "Template_Import_TitleCourse" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            rep.Dispose()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
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


#End Region

#Region "Page"

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

    'Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

    '    Dim dtData As DataTable
    '    Try
    '        'Using rep As New TrainingRepository
    '        '    hidOrgID.Value = ""
    '        '    lblOrgName.Text = ""
    '        '    cboTitle.Items.Clear()
    '        '    cboTitle.ClearSelection()
    '        '    cboTitle.Text = ""
    '        '    If ctrlOrg.CurrentValue <> "" Then
    '        '        hidOrgID.Value = ctrlOrg.CurrentValue
    '        '        lblOrgName.Text = ctrlOrg.CurrentText
    '        '        dtData = rep.GetTitleByOrgList(Decimal.Parse(ctrlOrg.CurrentValue), False)
    '        '        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
    '        '    End If
    '        '    rgData.Rebind()
    '        'End Using
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
    Private Sub cboTitleGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitleGroup.SelectedIndexChanged
        Try
            Dim reps As New TrainingStoreProcedure
            ClearControlValue(cboTitle)
            Dim dt = reps.GetTitleByGroup(cboTitleGroup.SelectedValue)
            FillRadCombobox(cboTitle, dt, "NAME_VN", "ID")
            'rgData.CurrentPageIndex = 0
            'rgData.MasterTableView.SortExpressions.Clear()
            'rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New TitleCourseDTO
        Dim rep As New TrainingRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of TitleCourseDTO)
            If cboTitle.SelectedValue <> "" Then
                _filter.HU_TITLE_ID = cboTitle.SelectedValue
            Else
                _filter.HU_TITLE_ID = 0
            End If
            If cboTitleGroup.SelectedValue <> "" Then
                _filter.TITLE_GROUP_ID = cboTitleGroup.SelectedValue
            Else
                _filter.TITLE_GROUP_ID = 0
            End If
            If Sorts IsNot Nothing Then
                lstData = rep.GetTitleCourse(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetTitleCourse(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Try
            Dim rep As New TrainingRepository
            Dim dtData As DataTable

            dtData = rep.GetOtherList("HU_TITLE_GROUP")
            FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

            If rgData.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = CType(rgData.SelectedItems(0), GridDataItem)
            If item.GetDataKeyValue("TITLE_GROUP_ID") IsNot Nothing Then
                cboTitleGroup.SelectedValue = CDec(item.GetDataKeyValue("TITLE_GROUP_ID"))
                cboTitleGroup.Text = item.GetDataKeyValue("TITLE_GROUP_NAME")

                Dim reps As New TrainingStoreProcedure
                Dim dt = reps.GetTitleByGroup(CDec(item.GetDataKeyValue("TITLE_GROUP_ID")))
                FillRadCombobox(cboTitle, dt, "NAME_VN", "ID")
                cboTitle.SelectedValue = CDec(item.GetDataKeyValue("HU_TITLE_ID"))
                cboTitle.Text = item.GetDataKeyValue("HU_TITLE_NAME")

            End If
        Catch ex As Exception
            Throw ex
        End Try
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
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("GROUP_TITLE") = rows("GROUP_TITLE")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("COURSE_NAME") = rows("COURSE_NAME")
                newRow("CHECK") = rows("CHECK")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("NOTE") = rows("NOTE")
                newRow("GROUP_TITLE_ID") = rows("GROUP_TITLE_ID")
                newRow("TITLE_ID") = rows("TITLE_ID")
                newRow("COURSE_ID") = rows("COURSE_ID")
                newRow("IS_CHECK") = rows("IS_CHECK")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New TrainingRepository
                If rep.IMPORT_TITLECOURSE(DocXml) Then
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
                sError = "Chưa chọn Nhóm chức danh"
                ImportValidate.EmptyValue("GROUP_TITLE", row, rowError, isError, sError)

                sError = "Chưa chọn Khóa đào tạo"
                ImportValidate.EmptyValue("COURSE_NAME", row, rowError, isError, sError)

                sError = "Chưa chọn loại Bắt buộc"
                ImportValidate.EmptyValue("CHECK", row, rowError, isError, sError)

                sError = "Chưa chọn loại Ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)

                '' check date
                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Sai định dạng"
                    ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    dtError.Rows.Add(rowError)
                End If
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("TR_COURSE_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TitleCourse_Error')", True)
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
        dtdata.AcceptChanges()
    End Sub
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("GROUP_TITLE", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("COURSE_NAME", GetType(String))
                dt.Columns.Add("CHECK", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                dt.Columns.Add("GROUP_TITLE_ID", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("COURSE_ID", GetType(String))
                dt.Columns.Add("IS_CHECK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
End Class