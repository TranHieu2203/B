Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SETUP_FRAMEWORK_KPI
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("GROUP_TITLE", GetType(String))
                dt.Columns.Add("GROUP_TITLE_ID", GetType(String))
                dt.Columns.Add("INDEX_TYPE_ID", GetType(String))
                dt.Columns.Add("INDEX_TYPE", GetType(String))
                dt.Columns.Add("BRAND_ID", GetType(String))
                dt.Columns.Add("BRAND", GetType(String))
                dt.Columns.Add("FROM_RATE", GetType(String))
                dt.Columns.Add("TO_RATE", GetType(String))
                dt.Columns.Add("KPI_FACTOR", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))

                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property


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
    Property objlst As List(Of PA_SETUP_FRAMEWORK_KPIDTO)
        Get
            Return ViewState(Me.ID & "_objlst")
        End Get
        Set(ByVal value As List(Of PA_SETUP_FRAMEWORK_KPIDTO))
            ViewState(Me.ID & "_objlst") = value
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
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Import)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True

            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(5), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Nhập file mẫu")

            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
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
        Dim _filter As New PA_SETUP_FRAMEWORK_KPIDTO
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSetupFrameWorkKPI(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetSetupFrameWorkKPI(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        objlst = rep.GetSetupFrameWorkKPI(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        objlst = rep.GetSetupFrameWorkKPI(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
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
                    EnableControlAll(True, rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSetupFrameWorkKPI(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)
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

        Using rep As New PayrollRepository
            FillRadCombobox(cboGroupTitle, rep.GetOtherList("STAFF_GROUP"), "NAME", "ID", False)
            FillRadCombobox(cboBrand, rep.GetOtherList("BRAND"), "NAME", "ID", False)
            FillRadCombobox(cboIndexType, rep.GetOtherList("INDEX_TYPE"), "NAME", "ID", False)
        End Using

        Dim dic As New Dictionary(Of String, Control)

        dic.Add("EMPLOYEE_OBJECT_ID", cboGroupTitle)
        dic.Add("BRAND_ID", cboBrand)
        dic.Add("INDEX_TYPE_ID", cboIndexType)
        dic.Add("FROM_RATE", rnFROM_RATE)
        dic.Add("TO_RATE", rnTO_RATE)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("KPI_FACTOR", rnFROM_AVG_SALE)
        dic.Add("NOTE", txtNOTE)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_FRAMEWORK_KPIDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportKPI()
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)
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
                            rgData.ExportExcel(Server, Response, dtData, "KPI_OFFICE")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        obj.TO_RATE = rnTO_RATE.Value
                        obj.FROM_RATE = rnFROM_RATE.Value
                        If cboGroupTitle.SelectedValue = "" Then
                            ShowMessage(Translate("Nhóm chức danh không hợp lệ "), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND_ID = cboBrand.SelectedValue
                        End If
                        If cboIndexType.SelectedValue <> "" Then
                            obj.INDEX_TYPE_ID = cboIndexType.SelectedValue
                        End If
                        obj.EMPLOYEE_OBJECT_ID = CDec(Val(cboGroupTitle.SelectedValue))
                        obj.KPI_FACTOR = rnFROM_AVG_SALE.Value
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.NOTE = txtNOTE.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    obj.ID = 0
                                    If rep.ValidateFrameWorkKPI(obj) = False Then
                                        If rep.InsertSetupFrameWorkKPI(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Warning)
                                    End If

                                Case CommonMessage.STATE_EDIT
                                    Dim sItem As GridDataItem = rgData.SelectedItems(0)
                                    obj.ID = sItem.GetDataKeyValue("ID")
                                    If rep.ValidateFrameWorkKPI(obj) = False Then
                                        If rep.ModifySetupFrameWorkKPI(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("UpdateView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Warning)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                    ClearControlValue(rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)

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
                    ClearControlValue(rnFROM_RATE, cboGroupTitle, cboBrand, cboIndexType, rnTO_RATE, rnFROM_AVG_SALE, rdEffectDate, txtNOTE)
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
#End Region

#Region "Custom"
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("GROUP_TITLE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("GROUP_TITLE")) OrElse rows("GROUP_TITLE") = "" Then Continue For
                'Dim repFactor As String = rows("FACTORSALARY").ToString.Trim.Replace(",", ".")
                Dim newRow As DataRow = dtData.NewRow
                newRow("GROUP_TITLE") = rows("GROUP_TITLE")
                newRow("GROUP_TITLE_ID") = rows("GROUP_TITLE_ID")
                newRow("INDEX_TYPE") = rows("INDEX_TYPE")
                newRow("INDEX_TYPE_ID") = rows("INDEX_TYPE_ID")
                newRow("BRAND") = rows("BRAND")
                newRow("BRAND_ID") = rows("BRAND_ID")
                newRow("FROM_RATE") = rows("FROM_RATE")
                newRow("TO_RATE") = rows("TO_RATE")
                newRow("KPI_FACTOR") = rows("KPI_FACTOR")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("NOTE") = rows("NOTE")

                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.IMPORT_KPI(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgData.Rebind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtData.Clone
            Dim iRow = 1
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn nhóm chức danh"
                ImportValidate.EmptyValue("GROUP_TITLE", row, rowError, isError, sError)

                If row("BRAND") Is DBNull.Value OrElse row("BRAND") = "" Then
                    sError = "Chưa nhập nhãn hàng"
                    ImportValidate.EmptyValue("BRAND", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("BRAND_ID")) = False Then
                        sError = "ID nhãn hàng không đúng định dạng"
                        ImportValidate.IsValidEmail("BRAND", row, rowError, isError, sError)
                    End If
                End If
                If row("INDEX_TYPE") Is DBNull.Value OrElse row("INDEX_TYPE") = "" Then
                    sError = "Chưa nhập loại chỉ số"
                    ImportValidate.EmptyValue("INDEX_TYPE", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("INDEX_TYPE_ID")) = False Then
                        sError = "ID loại chỉ số không đúng định dạng"
                        ImportValidate.IsValidEmail("INDEX_TYPE", row, rowError, isError, sError)
                    End If
                End If
                If row("FROM_RATE") Is DBNull.Value OrElse row("FROM_RATE") = "" Then
                    sError = "Chưa nhập % TLDT từ"
                    ImportValidate.IsValidTime("FROM_RATE", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("FROM_RATE")) = False Then
                        sError = "% TLDT từ không đúng định dạng"
                        ImportValidate.IsValidTime("FROM_RATE", row, rowError, isError, sError)
                    End If
                End If

                If row("TO_RATE") Is DBNull.Value OrElse row("TO_RATE") = "" Then
                    sError = "Chưa nhập % TLDT đến"
                    ImportValidate.IsValidTime("TO_RATE", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("TO_RATE")) = False Then
                        sError = "% TLDT đến không đúng định dạng"
                        ImportValidate.IsValidTime("TO_RATE", row, rowError, isError, sError)
                    End If
                End If

                If row("KPI_FACTOR") Is DBNull.Value OrElse row("KPI_FACTOR") = "" Then
                    sError = "Chưa nhập hệ số KPI"
                    ImportValidate.IsValidTime("KPI_FACTOR", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("KPI_FACTOR")) = False Then
                        sError = "Hệ số KPI không đúng định dạng"
                        ImportValidate.IsValidTime("KPI_FACTOR", row, rowError, isError, sError)
                    End If
                End If

                If row("EFFECT_DATE") Is DBNull.Value OrElse row("EFFECT_DATE") = "" Then
                    sError = "Chưa nhập ngày hiệu lực"
                    ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("EFFECT_DATE")) = False Then
                        sError = "Ngày hiệu lực không đúng định dạng"
                        ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                    End If
                End If

                If (row("INDEX_TYPE_ID") IsNot DBNull.Value OrElse row("INDEX_TYPE_ID") <> "") And (row("BRAND_ID") IsNot DBNull.Value OrElse row("BRAND_ID") <> "") And (row("EFFECT_DATE") IsNot DBNull.Value OrElse row("EFFECT_DATE") <> "") And (row("GROUP_TITLE") IsNot DBNull.Value OrElse row("GROUP_TITLE") <> "") Then
                    If IsDate(row("EFFECT_DATE")) Then
                        Dim obj As New PA_SETUP_FRAMEWORK_KPIDTO
                        obj.EMPLOYEE_OBJECT_ID = CDec(Val(row("GROUP_TITLE_ID")))
                        obj.BRAND_ID = CDec(Val(row("BRAND_ID")))
                        obj.INDEX_TYPE_ID = CDec(Val(row("INDEX_TYPE_ID")))
                        obj.EFFECT_DATE = Convert.ToDateTime(row("EFFECT_DATE")) 'CDate(row("EFFECT_DATE"))
                        Dim rep As New PayrollRepository
                        If rep.ValidateFrameWorkKPI(obj) Then
                            sError = "Dữ liệu đã tồn tại"
                            ImportValidate.IsValidEmail("EFFECT_DATE", row, rowError, isError, sError)
                        End If
                    End If

                End If


                If isError Then
                    rowError("GROUP_TITLE") = row("GROUP_TITLE").ToString
                    If rowError("GROUP_TITLE").ToString = "" Then
                        rowError("GROUP_TITLE") = row("GROUP_TITLE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                    'Else
                    'dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                'rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_KPI');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            'rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

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
        'dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

    Private Sub Template_ImportKPI()
        Dim rep As New PayrollRepository
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = rep.GET_KPI_IMPORT()
            dsData.Tables(0).TableName = "Table"
            rep.Dispose()

            ExportTemplate("Payroll\Business\KPI_VP.xls",
                                  dsData, Nothing, "import_KPI_VP" & Format(Date.Now, "yyyymmdd"))



        Catch ex As Exception
            Throw ex
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

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

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

    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function
#End Region

End Class