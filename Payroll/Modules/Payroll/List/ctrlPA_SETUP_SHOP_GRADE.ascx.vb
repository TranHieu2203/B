Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SETUP_SHOP_GRADE
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()
#Region "Property"
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
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
    Property objlst As List(Of PA_SETUP_SHOP_GRADEDTO)
        Get
            Return ViewState(Me.ID & "_objlst")
        End Get
        Set(ByVal value As List(Of PA_SETUP_SHOP_GRADEDTO))
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
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_NEXT,
                                                 ToolbarIcons.Export,
                                                 ToolbarAuthorize.Export,
                                                 Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                 ToolbarIcons.Import,
                                                 ToolbarAuthorize.Import,
                                                 Translate("Nhập file mẫu")))
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
        Dim _filter As New PA_SETUP_SHOP_GRADEDTO
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSetupShopGrade(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetSetupShopGrade(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        objlst = rep.GetSetupShopGrade(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        objlst = rep.GetSetupShopGrade(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
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
                    EnableControlAll(True, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE, rnBenefit, rnLessDTTT, rnThanDTTT)

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE, rnBenefit, rnLessDTTT, rnThanDTTT)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE, rnBenefit, rnLessDTTT, rnThanDTTT)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSetupShopGrade(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(cboBrand, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE, rnBenefit, rnLessDTTT, rnThanDTTT)
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
            FillRadCombobox(cboType_Shop, rep.GetOtherList("TYPE_SHOP"), "NAME", "ID", False)
            FillRadCombobox(cboGRADE, rep.GetOtherList("GRADE_XLCH"), "NAME", "ID", False)
        End Using

        Dim dic As New Dictionary(Of String, Control)
        dic.Add("BRAND", cboBrand)
        dic.Add("FROM_REVENVUE", rnFROM_REVENUE)
        dic.Add("TO_REVENVUE", rnTO_REVENUE)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("TYPE_SHOP", cboType_Shop)
        dic.Add("GRADE", cboGRADE)
        dic.Add("NOTE", txtNOTE)
        dic.Add("LESS_DTTT", rnLessDTTT)
        dic.Add("THAN_DTTT", rnThanDTTT)
        dic.Add("BENEFIT_VALUE", rnBenefit)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_SHOP_GRADEDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboBrand, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE, rnBenefit, rnLessDTTT, rnThanDTTT)
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
                            rgData.ExportExcel(Server, Response, dtData, "KPI_SHOPMANAGER")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = Decimal.Parse(cboBrand.SelectedValue)
                        End If
                        If cboType_Shop.SelectedValue <> "" Then
                            obj.TYPE_SHOP = Decimal.Parse(cboType_Shop.SelectedValue)
                        End If
                        If cboGRADE.SelectedValue <> "" Then
                            obj.GRADE = Decimal.Parse(cboGRADE.SelectedValue)
                        End If
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.FROM_REVENVUE = rnFROM_REVENUE.Value
                        obj.TO_REVENVUE = rnTO_REVENUE.Value
                        obj.LESS_DTTT = rnLessDTTT.Value
                        obj.THAN_DTTT = rnThanDTTT.Value
                        obj.BENEFIT_VALUE = rnBenefit.Value
                        obj.NOTE = txtNOTE.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    If rep.ValidateSetupShopGrade(obj) Then
                                        If rep.InsertSetupShopGrade(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Đã tồn tại thiết lập xếp loại cửa hàng!"), Utilities.NotifyType.Warning)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    Dim sItem As GridDataItem = rgData.SelectedItems(0)
                                    obj.ID = sItem.GetDataKeyValue("ID")
                                    If rep.ValidateSetupShopGrade(obj) Then
                                        If rep.ModifySetupShopGrade(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("UpdateView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Đã tồn tại thiết lập xếp loại cửa hàng!"), Utilities.NotifyType.Warning)
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
                    ClearControlValue(cboBrand, cboBrand, rnFROM_REVENUE, rdEffectDate, cboType_Shop, rnTO_REVENUE, cboGRADE, txtNOTE)
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
                Case Common.CommonMessage.TOOLBARITEM_NEXT
                    Dim repo As New PayrollRepository
                    Dim dataSet As New DataSet
                    Dim dtVariable As New DataTable
                    Dim tempPath = "~/ReportTemplates//Payroll//Import//Template_XepLoaiCH.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As DataSet
                    dsDanhMuc = repo.EXPORT_CH()

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "Template_XepLoaiCH", dsDanhMuc, Nothing, Response)
                    End Using
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
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function
#End Region
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New PayrollRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                'Dim count As Integer = ds.Tables(0).Columns.Count - 6
                'For i = 0 To count
                '    If ds.Tables(0).Columns(i).ColumnName.Contains("Column") Then
                '        ds.Tables(0).Columns.RemoveAt(i)
                '        i = i - 1
                '    End If
                'Next

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_CH(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_CV_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New PayrollBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "BRAND"
            dtTemp.Columns(2).ColumnName = "BRAND_ID"
            dtTemp.Columns(3).ColumnName = "TYPE_SHOP"
            dtTemp.Columns(4).ColumnName = "TYPE_SHOP_ID"
            dtTemp.Columns(5).ColumnName = "EFFECT_DATE"
            dtTemp.Columns(6).ColumnName = "GRADE"
            dtTemp.Columns(7).ColumnName = "GRADE_ID"
            dtTemp.Columns(8).ColumnName = "DT_FROM"
            dtTemp.Columns(9).ColumnName = "DT_TO"
            dtTemp.Columns(10).ColumnName = "MH1"
            dtTemp.Columns(11).ColumnName = "MH2"
            dtTemp.Columns(12).ColumnName = "MH3"
            dtTemp.Columns(13).ColumnName = "NOTE"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("STT").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New PayrollStoreProcedure
            Dim lstEmp As New List(Of String)

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                _error = False
                newRow = dtLogs.NewRow
                newRow("STT") = rows("STT")

                'bắt buộc nhập
                sError = "Chưa nhập dữ liệu"
                ImportValidate.EmptyValue("STT", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("BRAND", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("TYPE_SHOP", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("GRADE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("EFFECT_DATE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DT_FROM", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DT_TO", rows, newRow, _error, sError)

                'check combo box
                ImportValidate.IsValidList("BRAND", "BRAND_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("TYPE_SHOP", "TYPE_SHOP_ID", rows, newRow, _error, sError)
                ImportValidate.IsValidList("GRADE", "GRADE_ID", rows, newRow, _error, sError)

                'CHECK DATE
                sError = "Ngày sai định dạng"
                If rows("EFFECT_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("BIRTH_DATE", rows, newRow, _error, sError)
                End If

                If _error Then
                    dtLogs.Rows.Add(newRow)
                    _error = False
                End If
            Next
            dtTemp.AcceptChanges()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

End Class