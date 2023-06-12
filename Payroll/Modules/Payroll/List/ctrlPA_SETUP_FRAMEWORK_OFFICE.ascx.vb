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

Public Class ctrlPA_SETUP_FRAMEWORK_OFFICE
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
    Property objlst As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO)
        Get
            Return ViewState(Me.ID & "_objlst")
        End Get
        Set(ByVal value As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO))
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
        Dim _filter As New PA_SETUP_FRAMEWORK_OFFICEDTO
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSetupFrameWorkOffice(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetSetupFrameWorkOffice(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        objlst = rep.GetSetupFrameWorkOffice(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        objlst = rep.GetSetupFrameWorkOffice(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
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
                    EnableControlAll(True, cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSetupFrameWorkOffice(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                    ClearControlValue(cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)
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

            'FillRadCombobox(cboTitle, rep.GetHU_TITLE(), "NAME_VN", "ID", False)
            FillRadCombobox(cboTitle, rep.GetOtherList("STAFF_GROUP"), "NAME", "ID", False)
        End Using
        ' cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("BRAND", cboBrand)
        dic.Add("TITLE_ID", cboTitle)
        dic.Add("FROM_RATE", rnFROM_RATE)
        dic.Add("TO_RATE", rnTO_RATE)
        dic.Add("EFFECT_DATE", rdEffectDate)
        dic.Add("FROM_TARGET", rnFROM_TARGET)
        dic.Add("TO_TARGET", rnTO_TARGET)
        dic.Add("STANDARD_SALES", rnStandard_Sales)
        dic.Add("NOTE", txtNOTE)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_FRAMEWORK_OFFICEDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE

                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)
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
                            rgData.ExportExcel(Server, Response, dtData, "SETUP_LDT_VP")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = Decimal.Parse(cboBrand.SelectedValue)
                        Else
                            ShowMessage(Translate("Chưa chọn nhãn hàng!"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        If cboTitle.SelectedValue <> "" Then
                            obj.TITLE_ID = Decimal.Parse(cboTitle.SelectedValue)
                        Else
                            ShowMessage(Translate("Chưa chọn chức danh!"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        obj.TO_RATE = rnTO_RATE.Value
                        obj.TO_TARGET = rnTO_TARGET.Value
                        obj.FROM_RATE = rnFROM_RATE.Value
                        obj.FROM_TARGET = rnFROM_TARGET.Value
                        obj.STANDARD_SALES = rnStandard_Sales.Value
                        obj.NOTE = txtNOTE.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    obj.ID = 0
                                    If Not rep.ValidateSetupFrameWorkOffice(obj) Then
                                        If rep.InsertSetupFrameWorkOffice(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Thiết lập đã tồn tại!"), Utilities.NotifyType.Warning)
                                    End If

                                Case CommonMessage.STATE_EDIT
                                    Dim sItem As GridDataItem = rgData.SelectedItems(0)
                                    obj.ID = sItem.GetDataKeyValue("ID")
                                    If Not rep.ValidateSetupFrameWorkOffice(obj) Then
                                        If rep.ModifySetupFrameWorkOffice(obj, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("UpdateView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Thiết lập đã tồn tại!"), Utilities.NotifyType.Warning)
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
                    ClearControlValue(cboBrand, cboTitle, rnFROM_RATE, rnTO_RATE, rnFROM_TARGET, rnTO_TARGET, rdEffectDate, rnStandard_Sales, txtNOTE)

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim store As New PayrollStoreProcedure
                    Dim dsData As DataSet = store.EXPORT_PA_SETUP_FRAMEWORK_OFFICE()

                    ExportTemplate("Payroll\Import\Template_Import_PA_SETUP_FRAMEWORK_OFFICE.xlsx",
                                              dsData, Nothing,
                                              "Template_Import_LDT_VP" & Format(Date.Now, "yyyyMMdd"))
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

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Import_Data()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New PayrollRepository
            '_mylog = LogHelper.GetUserLog
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping1(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_PA_SETUP_FRAMEWORK_OFFICE(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "BRAND_NAME"
        dtTemp.Columns(1).ColumnName = "BRAND"
        dtTemp.Columns(2).ColumnName = "TITLE_NAME"
        dtTemp.Columns(3).ColumnName = "TITLE_ID"
        dtTemp.Columns(4).ColumnName = "EFFECT_DATE"
        dtTemp.Columns(5).ColumnName = "FROM_RATE"
        dtTemp.Columns(6).ColumnName = "TO_RATE"
        dtTemp.Columns(7).ColumnName = "FROM_TARGET"
        dtTemp.Columns(8).ColumnName = "TO_TARGET"
        dtTemp.Columns(9).ColumnName = "STANDARD_SALES"
        dtTemp.Columns(10).ColumnName = "NOTE"

        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow

        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        Dim startDate As Date

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 1 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("BRAND_NAME").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("EMPLOYEE_CODE") = rows("BRAND_NAME") + "-" + rows("TITLE_NAME")

            Dim obj As New PA_SETUP_FRAMEWORK_OFFICEDTO

            obj.ID = 0

            If Not IsNumeric(rows("BRAND")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Nhãn hàng - Không đúng định dạng,"
                _error = False
            Else
                obj.BRAND = Decimal.Parse(rows("BRAND"))
            End If

            If Not IsNumeric(rows("TITLE_ID")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Đối tượng nhân viên - Không đúng định dạng,"
                _error = False
            Else
                obj.TITLE_ID = Decimal.Parse(rows("TITLE_ID"))
            End If

            If IsDBNull(rows("EFFECT_DATE")) OrElse rows("EFFECT_DATE") = "" OrElse CheckDate(rows("EFFECT_DATE"), startDate) = False Then
                rows("EFFECT_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - Không đúng định dạng,"
                _error = False
            Else
                obj.EFFECT_DATE = startDate
            End If

            If Not IsNumeric(rows("FROM_RATE")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "TLHT CTDT(%) từ - Không đúng định dạng,"
                _error = False
            Else
                obj.FROM_RATE = Decimal.Parse(rows("FROM_RATE"))
            End If

            If Not IsNumeric(rows("TO_RATE")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "TLHT CTDT(%) đến - Không đúng định dạng,"
                _error = False
            Else
                obj.TO_RATE = Decimal.Parse(rows("TO_RATE"))
            End If

            If Not IsNumeric(rows("FROM_TARGET")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "CTDT từ - Không đúng định dạng,"
                _error = False
            Else
                obj.FROM_TARGET = Decimal.Parse(rows("FROM_TARGET"))
            End If

            If Not IsNumeric(rows("TO_TARGET")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "CTDT đến - Không đúng định dạng,"
                _error = False
            Else
                obj.TO_TARGET = Decimal.Parse(rows("TO_TARGET"))
            End If

            If Not IsNumeric(rows("STANDARD_SALES")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "LDT chuẩn - Không đúng định dạng,"
                _error = False
            Else
                obj.STANDARD_SALES = Decimal.Parse(rows("STANDARD_SALES"))
            End If

            Using rep As New PayrollRepository
                If _error AndAlso rep.ValidateSetupFrameWorkOffice(obj) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Thiết lập đã tồn tại!,"
                    _error = False
                End If
            End Using

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next

        dtTemp.AcceptChanges()
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "Custom"
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
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