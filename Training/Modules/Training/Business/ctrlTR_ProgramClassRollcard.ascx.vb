Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramClassRollcard
    Inherits Common.CommonView

#Region "Property"

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("TR_CLASS_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("FULLNAME", GetType(String))
                dt.Columns.Add("CLASS_DATE", GetType(String))
                dt.Columns.Add("ATTEND", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property ProFromDate As Date
        Get
            Return ViewState(Me.ID & "_ProFromDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProFromDate") = value
        End Set
    End Property

    Property ProToDate As Date
        Get
            Return ViewState(Me.ID & "_ProToDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProToDate") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Program As String
        Get
            Return ViewState(Me.ID & "_Program")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Program") = value
        End Set
    End Property

    Property SelectedID As Decimal
        Get
            Return ViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Refresh,
                                                       ToolbarItem.Seperator,
                                                       ToolbarItem.Export,
                                                       ToolbarItem.ExportTemplate,
                                                       ToolbarItem.Import,
                                                       ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = Translate("Cập nhật")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim reps As New TrainingStoreProcedure
        Dim dtData As New DataTable
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidClassID.Value = Request.Params("CLASS_ID")
                dtData = reps.GET_EMPLOYEES_IN_CLASS(Convert.ToDecimal(hidClassID.Value))
                FillRadCombobox(cboEmp, dtData, "NAME", "ID")
                Dim obj As ProgramClassDTO
                Using rep As New TrainingRepository
                    obj = rep.GetClassByID(New ProgramClassDTO With {.ID = hidClassID.Value})
                End Using
                txtClassName.Text = obj.NAME
                rdClassEnd.SelectedDate = obj.END_DATE
                rdClassStart.SelectedDate = obj.START_DATE
                If obj.TR_PROGRAM_ID IsNot Nothing Then
                    Dim prog As ProgramDTO
                    Using rep As New TrainingRepository
                        prog = rep.GetProgramById(obj.TR_PROGRAM_ID)
                        Program = prog.NAME
                        If prog.START_DATE IsNot Nothing Then
                            ProFromDate = prog.START_DATE
                        End If
                        If prog.END_DATE IsNot Nothing Then
                            ProToDate = prog.END_DATE
                        End If
                    End Using
                End If
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(rdClassDate, txtRemark, chkAttend, cboEmp)
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, chkAttend, rdClassDate, cboEmp, txtRemark)
                    Refresh("Cancel")
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, chkAttend, rdClassDate, cboEmp, txtRemark)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, chkAttend, rdClassDate, cboEmp, txtRemark)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using repDel As New TrainingBusinessClient
                        If repDel.DeleteProgramClassRollcard(lstDeletes, LogHelper.GetUserLog) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate("Xóa thất bại, vui lòng kiểm tra lại!"), NotifyType.Error)
                            Refresh("Cancel")
                            UpdateControlState()
                        End If
                    End Using
            End Select
            UpdateToolbarState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EMPLOYEE_ID", cboEmp)
            dic.Add("CLASS_DATE", rdClassDate)
            dic.Add("ATTEND", chkAttend)
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New ProgramClassRollcardDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Dim reps As New TrainingStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "TR_PROGRAM_CLASS_ROLL")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dsData As DataSet = reps.GET_PROGRAM_CLASS_ROLL_TEMPLATE_DATA(Convert.ToDecimal(hidClassID.Value))
                    ExportTemplate("Training\Import\Import_RollCall.xlsx",
                                              dsData, Nothing,
                                              "Import_RollCall" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        obj.REMARK = txtRemark.Text
                        obj.TR_CLASS_ID = hidClassID.Value
                        obj.ATTEND = chkAttend.Checked
                        If cboEmp.SelectedValue <> "" Then
                            obj.EMPLOYEE_ID = cboEmp.SelectedValue
                        End If
                        If rdClassDate.SelectedDate IsNot Nothing Then
                            obj.CLASS_DATE = rdClassDate.SelectedDate
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.InsertProgramClassRollcard(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgMain.SelectedValue

                                Dim lst As New List(Of Decimal)
                                lst.Add(obj.ID)
                                If rep.ModifyProgramClassRollcard(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New TrainingRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New TrainingRepository
        Dim _filter As New ProgramClassRollcardDTO
        Dim _param As New ParamDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            _filter.TR_CLASS_ID = Convert.ToDecimal(Request.Params("CLASS_ID"))
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetProgramClassRollcard(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetProgramClassRollcard(_filter, _param).ToTable()
                End If
            Else
                Dim ProgramClassRollcard As List(Of ProgramClassRollcardDTO)
                If Sorts IsNot Nothing Then
                    ProgramClassRollcard = rep.GetProgramClassRollcard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows, Sorts)
                Else
                    ProgramClassRollcard = rep.GetProgramClassRollcard(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = ProgramClassRollcard
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("EMPLOYEE_ID").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("TR_CLASS_ID") = rows("TR_CLASS_ID")
                newRow("EMPLOYEE_ID") = rows("EMPLOYEE_ID")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("FULLNAME") = rows("FULLNAME")
                newRow("REMARK") = rows("REMARK")
                newRow("ATTEND") = rows("ATTEND")
                newRow("CLASS_DATE") = rows("CLASS_DATE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New TrainingStoreProcedure
                If rep.IMPORT_PROGRAM_CLASS_ROLL(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import lỗi"), NotifyType.Error)
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

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

    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Dim rep As New TrainingStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate("Bạn đã nhập thiếu dữ liệu"), NotifyType.Warning)
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

                sError = "Không được để trống"
                ImportValidate.EmptyValue("EMPLOYEE_ID", row, rowError, isError, sError)

                sError = "Không được để trống"
                ImportValidate.EmptyValue("TR_CLASS_ID", row, rowError, isError, sError)

                sError = "Không được để trống"
                ImportValidate.EmptyValue("CLASS_DATE", row, rowError, isError, sError)

                If Not rep.CHECK_EXIST_EMP_IN_CLASS(Convert.ToDecimal(hidClassID.Value), row("EMPLOYEE_CODE").ToString) Then
                    sError = "Học viên không tồn tại"
                    ImportValidate.IsValidEmail("FULLNAME", row, rowError, isError, sError)
                End If
                If Not IsDBNull(row("CLASS_DATE")) AndAlso Not String.IsNullOrEmpty(row("CLASS_DATE")) Then
                    If Not IsDate(row("CLASS_DATE")) Then
                        sError = "Sai định dạng"
                        ImportValidate.IsValidEmail("CLASS_DATE", row, rowError, isError, sError)
                    End If
                End If
                If Not IsDBNull(row("ATTEND")) AndAlso Not String.IsNullOrEmpty(row("ATTEND")) Then
                    If row("ATTEND").ToString = "" OrElse row("ATTEND").ToString <> "-1" Then
                        If row("ATTEND").ToString <> "0" Then
                            sError = "Chỉ nhập -1 hoặc 0"
                            ImportValidate.IsValidEmail("ATTEND", row, rowError, isError, sError)
                        End If
                    End If
                End If

                If isError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("PROGRAM_CLASS_ROLL") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_RollCall_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region
End Class