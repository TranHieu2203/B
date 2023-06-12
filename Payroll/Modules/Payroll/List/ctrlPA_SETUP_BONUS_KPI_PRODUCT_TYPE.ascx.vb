Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlPA_SETUP_BONUS_KPI_PRODUCT_TYPE
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
    Property objlst As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO)
        Get
            Return ViewState(Me.ID & "_objlst")
        End Get
        Set(ByVal value As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO))
            ViewState(Me.ID & "_objlst") = value
        End Set
    End Property
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
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
        Dim _filter As New PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSetupBonusKpiProductType(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetSetupBonusKpiProductType(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        objlst = rep.GetSetupBonusKpiProductType(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        objlst = rep.GetSetupBonusKpiProductType(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
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
                    EnableControlAll(True, cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
                    cboBrand.AutoPostBack = True
                    cboTypeShop.AutoPostBack = True
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
                    cboBrand.AutoPostBack = False
                    cboTypeShop.AutoPostBack = False
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
                    cboBrand.AutoPostBack = True
                    cboTypeShop.AutoPostBack = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSetupBonusKpiProductType(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
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
            FillRadCombobox(cboBrand, rep.GetOtherList("BRAND"), "NAME", "ID", False)
            FillRadCombobox(cboTypeShop, rep.GetOtherList("TYPE_SHOP"), "NAME", "ID", False)
            FillRadCombobox(cboCompleteLv, rep.GetOtherList("COMPLETION_LEVEL"), "NAME", "ID", False)
        End Using

        Dim dic As New Dictionary(Of String, Control)
        dic.Add("BRAND", cboBrand)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("TYPE_SHOP", cboTypeShop)
        dic.Add("COMPLETE_LV", cboCompleteLv)
        dic.Add("FROM_RATE", rnFromRate)
        dic.Add("TO_RATE", rnToRate)
        dic.Add("NG", rnNG)
        dic.Add("NG1", rnKNG1)
        dic.Add("NG2", rnKNG2)
        dic.Add("NOTE", txtNOTE)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
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
                            rgData.ExportExcel(Server, Response, dtData, "BONUS_KPI_PRODUCT_TYPE")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim repo As New PayrollRepository
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = Decimal.Parse(cboBrand.SelectedValue)
                        End If
                        obj.TYPE_SHOP = Decimal.Parse(cboTypeShop.SelectedValue)
                        obj.COMPLETE_LV = Decimal.Parse(cboCompleteLv.SelectedValue)
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.FROM_RATE = rnFromRate.Value
                        obj.TO_RATE = rnToRate.Value
                        obj.NG = rnNG.Value
                        obj.NG1 = rnKNG1.Value
                        obj.NG2 = rnKNG2.Value
                        obj.NOTE = txtNOTE.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    If rep.ValidateSetupBonusKpiProductType(obj) Then
                                        If rep.InsertSetupBonusKpiProductType(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Nhãn hàng, ngày hiệu lực, loại cửa hàng, mức hoàn thành đã được thiết lập"), Utilities.NotifyType.Warning)
                                    End If

                                Case CommonMessage.STATE_EDIT
                                    Dim sItem As GridDataItem = rgData.SelectedItems(0)
                                    obj.ID = sItem.GetDataKeyValue("ID")
                                    If rep.ValidateSetupBonusKpiProductType(obj) Then
                                        If rep.ModifySetupBonusKpiProductType(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("UpdateView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Nhãn hàng, ngày hiệu lực, loại cửa hàng, mức hoàn thành đã được thiết lập"), Utilities.NotifyType.Warning)
                                    End If
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
                    ClearControlValue(cboBrand, cboTypeShop, cboCompleteLv, rdEffectDate, rnFromRate, rnToRate, rnNG, rnKNG1, rnKNG2, txtNOTE)
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim rep As New PayrollRepository
                    Dim dsData As New DataSet
                    dsData = rep.EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE()
                    dsData.Tables(0).TableName = "Table"
                    dsData.Tables(1).TableName = "Table1"
                    dsData.Tables(2).TableName = "Table2"

                    ExportTemplate("Payroll\Import\Template_ThietLap_TLT_TLHTKPI_LoaiSP.xls", _
                                              dsData, Nothing, _
                                              "Template_ThietLap_TLT_TLHTKPI_LoaiSP" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
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
    Private Sub ctrlUpload_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PayrollRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ExcelPackage.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(DocXml, LogHelper.CurrentUser.USERNAME) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_ERROR_PA_SETUP_BONUS_KPI_PRODUCTTYPE')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            Dim rep As New PayrollRepository
            Dim regex As Regex = New Regex("^(?!.*<[^>]+>).*")
            Dim effectDate As Date
            'Lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "BRAND_NAME"
            dtTemp.Columns(2).ColumnName = "BRAND_ID"
            dtTemp.Columns(3).ColumnName = "TYPE_SHOP_NAME"
            dtTemp.Columns(4).ColumnName = "TYPE_SHOP_ID"
            dtTemp.Columns(5).ColumnName = "EFFECT_DATE"
            dtTemp.Columns(6).ColumnName = "COMPLETE_LVL_NAME"
            dtTemp.Columns(7).ColumnName = "COMPLETE_LVL_ID"
            dtTemp.Columns(8).ColumnName = "FROM_RATE"
            dtTemp.Columns(9).ColumnName = "TO_RATE"
            dtTemp.Columns(10).ColumnName = "NG"
            dtTemp.Columns(11).ColumnName = "KNG1"
            dtTemp.Columns(12).ColumnName = "KNG2"
            dtTemp.Columns(13).ColumnName = "NOTE"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()

            dtTemp.AcceptChanges()
            'ADD Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            If dtLogs Is Nothing Then
                dtLogs = dtTemp.Clone
                dtLogs.Columns.Add("DISCRIPTION", GetType(String))
            End If
            dtLogs.Clear()

            'Dim rowDel As DataRow
            'For i As Integer = 1 To dtTemp.Rows.Count - 1
            '    If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            '    rowDel = dtTemp.Rows(i)
            '    If rowDel("STT").ToString.Trim = "" Then
            '        dtTemp.Rows(i).Delete()
            '    End If
            'Next

            For Each rows As DataRow In dtTemp.Rows

                'NẾU TẤT CẢ BẰNG NULL THÌ BỎ QUA KHÔNG XÉT
                If IsDBNull(rows("BRAND_NAME")) AndAlso IsDBNull(rows("BRAND_ID")) AndAlso IsDBNull(rows("TYPE_SHOP_NAME")) AndAlso IsDBNull(rows("TYPE_SHOP_ID")) AndAlso IsDBNull(rows("EFFECT_DATE")) AndAlso IsDBNull(rows("COMPLETE_LVL_NAME")) AndAlso IsDBNull(rows("COMPLETE_LVL_ID")) AndAlso IsDBNull(rows("FROM_RATE")) AndAlso IsDBNull(rows("TO_RATE")) AndAlso IsDBNull(rows("NG")) AndAlso IsDBNull(rows("KNG1")) AndAlso IsDBNull(rows("KNG2")) AndAlso IsDBNull(rows("NOTE")) Then
                    rows.Delete()
                    Continue For
                End If

                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                newRow = dtLogs.NewRow
                newRow("STT") = count + 1

                If IsDBNull(rows("BRAND_ID")) Then
                    newRow("DISCRIPTION") = "Bạn phải chọn Nhãn hàng, "
                    _error = False
                End If

                If IsDBNull(rows("TYPE_SHOP_ID")) Then
                    newRow("DISCRIPTION") &= "Bạn phải chọn Loại cửa hàng, "
                    _error = False
                End If

                If IsDBNull(rows("EFFECT_DATE")) OrElse rows("EFFECT_DATE") = "" Then
                    newRow("DISCRIPTION") &= "Bạn phải chọn Ngày hiệu lực, "
                    _error = False
                Else
                    If CheckDate(rows("EFFECT_DATE"), effectDate) = False Then
                        newRow("DISCRIPTION") &= "Ngày hiệu lực không đúng định dạng, "
                        _error = False
                    End If
                End If

                If IsDBNull(rows("COMPLETE_LVL_ID")) Then
                    newRow("DISCRIPTION") &= "Bạn phải chọn Mức hoàn thành, "
                    _error = False
                End If

                If IsDBNull(rows("FROM_RATE")) Then
                    newRow("DISCRIPTION") &= "Bạn phải nhập TLHT KPI (%) từ, "
                    _error = False
                End If

                If IsDBNull(rows("TO_RATE")) Then
                    newRow("DISCRIPTION") &= "Bạn phải nhập TLHT KPI (%) đến, "
                    _error = False
                End If

                If IsDBNull(rows("NG")) Then
                    newRow("DISCRIPTION") &= "Bạn phải nhập TLT (sản phẩm NG), "
                    _error = False
                End If

                If IsDBNull(rows("KNG1")) Then
                    newRow("DISCRIPTION") &= "Bạn phải nhập TLT (sản phẩm KNG1), "
                    _error = False
                End If

                If IsDBNull(rows("KNG2")) Then
                    newRow("DISCRIPTION") &= "Bạn phải nhập TLT (sản phẩm KNG2), "
                    _error = False
                End If
                If Not IsDBNull(rows("NOTE")) Then
                    Dim isValid As Boolean = regex.IsMatch(rows("NOTE").ToString.Trim)
                    If Not isValid Then
                        newRow("DISCRIPTION") &= "Thông tin Ghi chú có chứa mã HTML, "
                        _error = False
                    End If
                End If

                If _error Then
                    Dim countExists As Decimal = 0
                    Dim obj As New PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO

                    obj.EFFECT_DATE = effectDate
                    obj.COMPLETE_LV = Double.Parse(rows("COMPLETE_LVL_ID"))
                    obj.BRAND = Double.Parse(rows("BRAND_ID"))
                    obj.TYPE_SHOP = Double.Parse(rows("TYPE_SHOP_ID"))

                    obj.ID = 0
                    If rep.ValidateSetupBonusKpiProductType(obj) = False Then
                        countExists &= 1
                    End If

                    Dim sltQ = "COMPLETE_LVL_ID = '" & rows("COMPLETE_LVL_ID") & "' AND EFFECT_DATE = '" & obj.EFFECT_DATE.Value.Date.ToString("dd/MM/yyyy") & "' AND BRAND_ID = '" & rows("BRAND_ID") & "' AND TYPE_SHOP_ID = '" & rows("TYPE_SHOP_ID") & "'"
                    Dim query = dtTemp.Select(sltQ)

                    If query.Any AndAlso query.Count > 1 Then
                        countExists &= 1
                    End If
                    If countExists > 0 Then
                        newRow("DISCRIPTION") = "Dữ liệu đã tồn tại, "
                        _error = False
                    End If
                End If

                If _error = False Then
                    newRow("BRAND_NAME") = rows("BRAND_NAME")
                    newRow("BRAND_ID") = rows("BRAND_ID")
                    newRow("TYPE_SHOP_NAME") = rows("TYPE_SHOP_NAME")
                    newRow("TYPE_SHOP_ID") = rows("TYPE_SHOP_ID")
                    newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                    newRow("COMPLETE_LVL_NAME") = rows("COMPLETE_LVL_NAME")
                    newRow("COMPLETE_LVL_ID") = rows("COMPLETE_LVL_ID")
                    newRow("FROM_RATE") = rows("FROM_RATE")
                    newRow("TO_RATE") = rows("TO_RATE")
                    newRow("NG") = rows("NG")
                    newRow("KNG1") = rows("KNG1")
                    newRow("KNG2") = rows("KNG2")
                    newRow("NOTE") = rows("NOTE")
                    dtLogs.Rows.Add(newRow)
                    count = count + 1
                    _error = True
                End If
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Function CheckDate(ByVal value As String, ByRef effectDate As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, effectDate)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

End Class