Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SETUP_FRAMEWORK_ECD
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Property orgid As Integer
        Get
            Return ViewState(Me.ID & "_orgid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_orgid") = value
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
    Property objlst As List(Of PA_SETUP_FRAMEWORK_ECDDTO)
        Get
            Return ViewState(Me.ID & "_objlst")
        End Get
        Set(ByVal value As List(Of PA_SETUP_FRAMEWORK_ECDDTO))
            ViewState(Me.ID & "_objlst") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("BRAND_NAME", GetType(String))
                dt.Columns.Add("BRAND_ID", GetType(String))
                dt.Columns.Add("GROUP_EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("GROUP_EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("FROM_AVG_SALE", GetType(String))
                dt.Columns.Add("TO_AVG_SALE", GetType(String))
                dt.Columns.Add("FROM_RATE", GetType(String))
                dt.Columns.Add("TO_RATE", GetType(String))
                dt.Columns.Add("LDTC", GetType(String))
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
            If Not IsPostBack Then
                Refresh()
                UpdateControlState()
                rgData.SetFilter()
                rgData.AllowCustomPaging = True
                rgData.PageSize = Common.Common.DefaultPageSize
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryTypes
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            MainToolBar.Items(6).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(7).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect)
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PA_SETUP_FRAMEWORK_ECDDTO
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSetupFrameWorkECD(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetSetupFrameWorkECD(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        objlst = rep.GetSetupFrameWorkECD(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        objlst = rep.GetSetupFrameWorkECD(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = objlst
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSetupFrameWorkECD(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("FROM_RATE", rnFROM_RATE)
        dic.Add("TO_RATE", rnTO_RATE)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("FROM_AVG_SALE", rnFROM_AVG_SALE)
        dic.Add("TO_AVG_SALE", rnTO_AVG_SALE)
        dic.Add("NOTE", txtNOTE)
        dic.Add("BRAND", cboBrand)
        dic.Add("GROUP_EMPLOYEE_ID", cboGroupEmp)
        dic.Add("LDTC", rnLDTC)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_FRAMEWORK_ECDDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)
                    UpdateControlState()
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
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "DS_LDT_ECD")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        obj.TO_RATE = rnTO_RATE.Value
                        obj.TO_AVG_SALE = rnTO_AVG_SALE.Value
                        obj.FROM_RATE = rnFROM_RATE.Value
                        obj.FROM_AVG_SALE = rnFROM_AVG_SALE.Value
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.NOTE = txtNOTE.Text
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = cboBrand.SelectedValue
                        End If
                        If cboGroupEmp.SelectedValue <> "" Then
                            obj.GROUP_EMPLOYEE_ID = cboGroupEmp.SelectedValue
                        End If
                        If IsNumeric(rnLDTC.Value) Then
                            obj.LDTC = rnLDTC.Value
                        End If
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    'If Not rep.ValidateSetupFrameWorkECD(obj) Then
                                    obj.ID = 0
                                    If rep.InsertSetupFrameWorkECD(obj, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                    'Else
                                    'ShowMessage(Translate("Ngày hiệu lực đã tồn tại"), Utilities.NotifyType.Warning)
                                    'End If

                                Case CommonMessage.STATE_EDIT
                                    Dim sItem As GridDataItem = rgData.SelectedItems(0)
                                    obj.ID = sItem.GetDataKeyValue("ID")
                                    'If Not rep.ValidateSetupFrameWorkECD(obj) Then
                                    If rep.ModifySetupFrameWorkECD(obj, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                    'Else
                                    'ShowMessage(Translate("Ngày hiệu lực đã tồn tại"), Utilities.NotifyType.Warning)
                                    'End If

                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rnFROM_RATE, rnTO_RATE, rnFROM_AVG_SALE, rnTO_AVG_SALE, rdEffectDate, txtNOTE, cboBrand, cboGroupEmp, rnLDTC)
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim rep As New PayrollRepository
                    Dim dsData As DataSet = rep.GET_FRAMEWORK_ECD_IMPORT_DATA()
                    ExportTemplate("Payroll\Business\Template_ThietLap_MucThuong_VP.xls",
                                              dsData, Nothing,
                                              "Template_ThietLap_MucThuong_VP" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes AndAlso e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Try
            If rgData.SelectedItems.Count Then
                Dim sItem As GridDataItem = rgData.SelectedItems(0)
                If sItem.GetDataKeyValue("ID").ToString <> "" Then
                    Dim item = (From p In objlst Where p.ID = Decimal.Parse(sItem.GetDataKeyValue("ID").ToString) Select p).FirstOrDefault
                    If item IsNot Nothing Then

                    End If
                End If
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("STT").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("BRAND_NAME") = rows("BRAND_NAME")
                newRow("BRAND_ID") = rows("BRAND_ID")
                newRow("GROUP_EMPLOYEE_NAME") = rows("GROUP_EMPLOYEE_NAME")
                newRow("GROUP_EMPLOYEE_ID") = rows("GROUP_EMPLOYEE_ID")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("FROM_AVG_SALE") = rows("FROM_AVG_SALE")
                newRow("TO_AVG_SALE") = rows("TO_AVG_SALE")
                newRow("FROM_RATE") = rows("FROM_RATE")
                newRow("TO_RATE") = rows("TO_RATE")
                newRow("LDTC") = rows("LDTC")
                newRow("NOTE") = rows("NOTE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New PayrollRepository
                If rep.IMPORT_FRAMEWORK_ECD(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function

    Private Sub GetDataCombo()
        Dim rep As New PayrollRepository
        Dim dtData As New DataTable
        Try
            dtData = rep.GetOtherList("BRAND", True)
            FillRadCombobox(cboBrand, dtData, "NAME", "ID")
            dtData = rep.GetOtherList("STAFF_GROUP", True)
            FillRadCombobox(cboGroupEmp, dtData, "NAME", "ID")
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
        Dim rep As New PayrollRepository
        Dim rep2 As New Attendance.AttendanceBusiness.AttendanceBusinessClient
        Dim store As New PayrollStoreProcedure
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
                sError = "Chưa chọn nhãn hàng"
                ImportValidate.EmptyValue("BRAND_NAME", row, rowError, isError, sError)

                sError = "Chưa nhập ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)

                sError = "Chưa nhập DT trung bình từ"
                ImportValidate.EmptyValue("FROM_AVG_SALE", row, rowError, isError, sError)

                sError = "Chưa nhập DT trung bình đến"
                ImportValidate.EmptyValue("TO_AVG_SALE", row, rowError, isError, sError)

                sError = "Chưa nhập TLDT(%) từ"
                ImportValidate.EmptyValue("FROM_RATE", row, rowError, isError, sError)

                sError = "Chưa nhập TLDT(%) đến"
                ImportValidate.EmptyValue("TO_RATE", row, rowError, isError, sError)

                sError = "Chưa nhập LDTC chuẩn"
                ImportValidate.EmptyValue("LDTC", row, rowError, isError, sError)

                ''check number
                If Not IsDBNull(row("FROM_AVG_SALE")) AndAlso Not String.IsNullOrEmpty(row("FROM_AVG_SALE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("FROM_AVG_SALE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TO_AVG_SALE")) AndAlso Not String.IsNullOrEmpty(row("TO_AVG_SALE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TO_AVG_SALE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("FROM_RATE")) AndAlso Not String.IsNullOrEmpty(row("FROM_RATE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("FROM_RATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TO_RATE")) AndAlso Not String.IsNullOrEmpty(row("TO_RATE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("TO_RATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("LDTC")) AndAlso Not String.IsNullOrEmpty(row("LDTC")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("LDTC", row, rowError, isError, sError)
                End If

                ''check date
                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Chỉ được nhập dạng ngày"
                    ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    If IsDBNull(rowError("BRAND_NAME")) Then
                        rowError("BRAND_NAME") = row("BRAND_NAME").ToString
                    End If
                    If IsDBNull(rowError("GROUP_EMPLOYEE_NAME")) Then
                        rowError("GROUP_EMPLOYEE_NAME") = row("GROUP_EMPLOYEE_NAME").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("FRAMEWORK_ECD") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ThietLap_MucThuong_VP_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

#End Region

End Class