Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI

Public Class ctrlPE_EmployeeAssessmentDTL
    Inherits Common.CommonView
    Dim dtData As New DataTable
#Region "Property"
    Dim listCriteria As DataSet

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
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
            rgEmployeeAssessmentDtl.AllowCustomPaging = True
            rgEmployeeAssessmentDtl.SetFilter()
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Using rep As New PerformanceRepository
                Dim dtData = rep.GetPeriodList(True)
                FillRadCombobox(cboPeriod, dtData, "NAME", "ID")
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        If Not IsPostBack Then
            CurrentState = CommonMessage.STATE_NORMAL
        End If
    End Sub

    Public Overrides Sub UpdateControlState()

    End Sub

#End Region

#Region "Event"
    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilterCanNotSchedule(True)
                        GetListCriteria()
                        rgEmployeeAssessmentDtl_ColumnCreated(Nothing, Nothing)
                        If dtData.Rows.Count > 0 Then
                            rgEmployeeAssessmentDtl.ExportExcel(Server, Response, dtData, "EmployeeAssessmentDTL")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    Dim peridodID As Decimal
                    Dim objgroupID As Decimal
                    If cboPeriod.SelectedValue <> "" Then
                        peridodID = cboPeriod.SelectedValue
                    End If
                    If cboObjectGroup.SelectedValue <> "" Then
                        objgroupID = cboObjectGroup.SelectedValue
                    End If

                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_PE_EMPLOYEE_ASSESSMENT&PERIOD_ID=" & peridodID & "&OBJGROUP_ID=" & objgroupID & "');", True)
                    GetListCriteria()
                    rgEmployeeAssessmentDtl_ColumnCreated(Nothing, Nothing)
                    rgEmployeeAssessmentDtl.Rebind()
                Case "IMPORT_TEMP"
                    ctrlUpload1.Show()
            End Select
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Function loadToGrid(ByVal dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Try
            If dtDataHeader.Rows.Count = 0 Then
                ShowMessage("File mẫu không tồn tại dữ liệu", NotifyType.Warning)
                Return False
            End If
            If dtDataHeader.Columns.Count <= 9 Then
                ShowMessage("Không có tiêu chí đánh giá nào trong file import", NotifyType.Warning)
                Return False
            End If

            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dsDataCriteriaObj As New DataSet
            dtError = dtDataHeader.Clone
            Dim countErr As New Integer
            Dim irow = 8
            Dim rep As New PerformanceRepository
            If cboPeriod.SelectedValue = "" AndAlso cboObjectGroup.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn kỳ hoặc nhóm đối tượng đánh giá"), Utilities.NotifyType.Error)
                Exit Function
            End If
            dsDataCriteriaObj = rep.GetCriteriaImport(cboObjectGroup.SelectedValue)

            For Each row As DataRow In dtDataHeader.Rows
                isError = False
                rowError = dtError.NewRow
                countErr = 0
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                If dsDataCriteriaObj IsNot Nothing Then
                    If dsDataCriteriaObj.Tables(0).Rows.Count > 0 Then
                        For i = 0 To dsDataCriteriaObj.Tables(0).Rows.Count - 1
                            If row("ID_" & dsDataCriteriaObj.Tables(0).Rows(i)("ID")).ToString() <> "" Then
                                sError = "Không phải kiểu số"
                                ImportValidate.IsValidNumber("ID_" & dsDataCriteriaObj.Tables(0).Rows(i)("ID"), row, rowError, isError, sError)
                            End If
                        Next
                    End If
                End If
                If isError Then
                    rowError("STT") = irow
                    rowError("EMPLOYEE_ID") = row("EMPLOYEE_ID").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("FULLNAME_VN") = row("FULLNAME_VN").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("RANK_NAME") = row("RANK_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                Dim peridodID As Decimal
                Dim objgroupID As Decimal
                If cboPeriod.SelectedValue <> "" Then
                    peridodID = cboPeriod.SelectedValue
                Else
                    Exit Function
                End If
                If cboObjectGroup.SelectedValue <> "" Then
                    objgroupID = cboObjectGroup.SelectedValue
                Else
                    Exit Function
                End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_PE_EMPLOYEE_ASSESSMENT_ERROR&PERIOD_ID=" & peridodID & "&OBJGROUP_ID=" & objgroupID & "')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                GetListCriteria()
                rgEmployeeAssessmentDtl_ColumnCreated(Nothing, Nothing)
                rgEmployeeAssessmentDtl.Rebind()
            Else
                Return True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeard As New DataTable
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORT_EMPLOYEE_ASSESSMENT") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(6, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dsDataPrepare.Tables(0).Clone
            dtDataHeard = dtData.Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dtDataHeard.ImportRow(row)
                Next
            Next
            If loadToGrid(dtDataHeard) Then
                If SaveOT() Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
                GetListCriteria()
                rgEmployeeAssessmentDtl_ColumnCreated(Nothing, Nothing)
                rgEmployeeAssessmentDtl.Rebind()
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Function SaveOT() As Boolean
        Try
            Using rep As New PerformanceRepository
                If rep.ImportEmployeeAssessment(dtData, cboPeriod.SelectedValue, cboObjectGroup.SelectedValue) Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        Return False
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub GetListCriteria()
        Dim rep As New PerformanceRepository
        Try
            If cboObjectGroup.SelectedValue <> "" Then
                listCriteria = New DataSet
                listCriteria = rep.GetCriteriaImport(cboObjectGroup.SelectedValue)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgEmployeeAssessmentDtl_ColumnCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgEmployeeAssessmentDtl.ColumnCreated
        Try
            If cboPeriod.SelectedValue = "" And cboObjectGroup.SelectedValue = "" Then
                Exit Sub
            End If
            Dim listcol() As String = {"cbStatus", "EMPLOYEE_CODE", "FULLNAME_VN", "TITLE_NAME", "ORG_NAME", "ORG_PATH", "RANK_NAME", "CLASSIFICATION"}
            Dim i As Integer = 0
            While (i < rgEmployeeAssessmentDtl.Columns.Count)
                Dim c As GridColumn = rgEmployeeAssessmentDtl.Columns(i)
                If Not listcol.Contains(c.UniqueName) Then
                    rgEmployeeAssessmentDtl.Columns.Remove(c)
                    Continue While
                End If
                i = i + 1
            End While
            Dim stringKey As New List(Of String)
            stringKey.Add("ID")
            stringKey.Add("EMPLOYEE_CODE")
            stringKey.Add("FULLNAME_VN")
            stringKey.Add("TITLE_NAME")
            stringKey.Add("ORG_PATH")
            stringKey.Add("ORG_NAME")
            stringKey.Add("RANK_NAME")
            stringKey.Add("CLASSIFICATION")

            If listCriteria IsNot Nothing Then
                For Each row As DataRow In listCriteria.Tables(0).Rows
                    Dim col As New GridBoundColumn
                    col.HeaderText = row("NAME")
                    col.DataField = "T" & row("ID")
                    col.UniqueName = "T" & row("ID")
                    col.SortExpression = "T" & row("ID")
                    col.AllowFiltering = False
                    col.AllowSorting = True
                    col.DataFormatString = "{0:N0}"
                    col.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    col.HeaderStyle.Width = 100
                    rgEmployeeAssessmentDtl.MasterTableView.Columns.Add(col)
                    stringKey.Add(col.DataField)
                Next
            End If
            rgEmployeeAssessmentDtl.MasterTableView.ClientDataKeyNames = stringKey.ToArray
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgEmployeeAssessmentDtl_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployeeAssessmentDtl.NeedDataSource
        Try
            GetListCriteria()
            rgEmployeeAssessmentDtl_ColumnCreated(Nothing, Nothing)
            CreateDataFilterCanNotSchedule()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged1(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Try
            cboObjectGroup.Items.Clear()
            cboObjectGroup.ClearCheckedItems()
            cboObjectGroup.ClearSelection()
            cboObjectGroup.Text = ""
            If cboPeriod.SelectedValue <> "" Then
                Using rep As New PerformanceRepository
                    Dim dtData = rep.GetObjectGroupByPeriodList(cboPeriod.SelectedValue, False)
                    FillRadCombobox(cboObjectGroup, dtData, "NAME", "ID", True)
                    cboObjectGroup_SelectedIndexChanged(Nothing, Nothing)
                End Using
            Else
                cboObjectGroup.SelectedValue = Nothing
                cboObjectGroup.Text = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboObjectGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboObjectGroup.SelectedIndexChanged
        GetListCriteria()
        rgEmployeeAssessmentDtl.Rebind()
    End Sub
#End Region

#Region "Custom"

    Private Function CreateDataFilterCanNotSchedule(Optional ByVal isFull As Boolean = False)

        Try
            Using rep As New PerformanceRepository
                Dim obj As New PE_EMPLOYEE_ASSESSMENT_DTLDTO
                Dim _param As New ParamDTO
                _param.ORG_ID = 46
                Dim period As Decimal = 0
                Dim group_id As Decimal = 0
                If cboPeriod.SelectedValue <> "" Then
                    period = cboPeriod.SelectedValue
                End If
                If cboObjectGroup.SelectedValue <> "" Then
                    group_id = cboObjectGroup.SelectedValue
                End If
                SetValueObjectByRadGrid(rgEmployeeAssessmentDtl, obj)

                If cboPeriod.SelectedValue <> "" Then
                    obj.PERIOD_ID = cboPeriod.SelectedValue
                Else
                    rgEmployeeAssessmentDtl.VirtualItemCount = 0
                    rgEmployeeAssessmentDtl.DataSource = New DataTable
                    Exit Function
                End If
                If cboObjectGroup.SelectedValue <> "" Then
                    obj.GROUP_OBJ_ID = cboObjectGroup.SelectedValue
                Else
                    rgEmployeeAssessmentDtl.VirtualItemCount = 0
                    rgEmployeeAssessmentDtl.DataSource = New DataTable
                    Exit Function
                End If

                Dim dsData As New DataSet
                dsData = rep.GetEmployeeImportAssessment(_param, obj, rgEmployeeAssessmentDtl.CurrentPageIndex, rgEmployeeAssessmentDtl.PageSize)
                If Not isFull Then
                    If dsData IsNot Nothing Then
                        rgEmployeeAssessmentDtl.VirtualItemCount = Decimal.Parse(dsData.Tables(1).Rows(0)("TOTAL"))
                        rgEmployeeAssessmentDtl.DataSource = dsData
                    Else
                        rgEmployeeAssessmentDtl.VirtualItemCount = 0
                        rgEmployeeAssessmentDtl.DataSource = New DataTable
                    End If
                Else
                    Return rep.GetEmployeeImportAssessment(_param, obj, rgEmployeeAssessmentDtl.CurrentPageIndex, Integer.MaxValue).Tables(0)
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region
End Class