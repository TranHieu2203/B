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

Public Class ctrlPA_Setup_Index
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Setting" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' Down_File
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

    ''' <summary>
    ''' FileOldName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
    Property dataTable As DataTable
        Get
            Return ViewState(Me.ID & "_dataTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dataTable") = value
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

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgMain)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            MainToolBar.Items(6).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(7).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(txtNote, cboBrand, numFactor, cboIndexType, rdEffectDate, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtNote, cboBrand, numFactor, cboIndexType, rdEffectDate, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(txtNote, cboBrand, numFactor, cboIndexType, rdEffectDate, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                    Case ""
                        cboBrand.AutoPostBack = False
                        cboIndexType.AutoPostBack = False
                        numFactor.AutoPostBack = False
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New PayrollRepository
        Dim _filter As New PA_SETUP_INDEX_DTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_PA_SETUP_INDEX(_filter, Sorts).ToTable()
                Else
                    Return rep.GET_PA_SETUP_INDEX(_filter).ToTable()
                End If
            Else
                Dim PA_SETUP_INDEX As List(Of PA_SETUP_INDEX_DTO)
                If Sorts IsNot Nothing Then
                    PA_SETUP_INDEX = rep.GET_PA_SETUP_INDEX(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    PA_SETUP_INDEX = rep.GET_PA_SETUP_INDEX(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = PA_SETUP_INDEX
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                    Refresh("Cancel")
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DELETE_PA_SETUP_INDEX(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

            End Select
            rep.Dispose()

            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New PayrollStoreProcedure
        Try
            Dim dtData As DataTable
            Using rep As New PayrollRepository
                dtData = rep.GetOtherList("BRAND", True)
                FillRadCombobox(cboBrand, dtData, "NAME", "ID")
                dataTable = rep.GetOtherList("INDEX_TYPE", True)
                FillRadCombobox(cboIndexType, dataTable, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("BRAND", cboBrand)
            dic.Add("FACTOR", numFactor)
            dic.Add("INDEX_TYPE", cboIndexType)
            dic.Add("NOTE", txtNote)
            dic.Add("FROM_COMPLETION_RATE", numFromCompletionRate)
            dic.Add("TO_COMPLETION_RATE", numToCompletionRate)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("IS_GET_TLHT", chkGetTLHT)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New PA_SETUP_INDEX_DTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(True, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
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

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    ClearControlValue(True, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "PA_SETUP_INDEX")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj.NOTE = txtNote.Text.Trim
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.FROM_COMPLETION_RATE = numFromCompletionRate.Value
                        obj.TO_COMPLETION_RATE = numToCompletionRate.Value
                        obj.FACTOR = numFactor.Value
                        obj.IS_GET_TLHT = chkGetTLHT.Checked
                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = cboBrand.SelectedValue
                        End If
                        If cboIndexType.SelectedValue <> "" Then
                            obj.INDEX_TYPE = cboIndexType.SelectedValue
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.VALIDATE_PA_SETUP_INDEX(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.INSERT_PA_SETUP_INDEX(obj, gID) Then
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
                                If rep.VALIDATE_PA_SETUP_INDEX(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.MODIFY_PA_SETUP_INDEX(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(True, rdEffectDate, cboBrand, numFactor, cboIndexType, txtNote, numFromCompletionRate, numToCompletionRate, chkGetTLHT)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dsData As New DataSet
                    dsData = rep.EXPORT_PA_SETUP_INDEX()
                    dsData.Tables(0).TableName = "Table"
                    dsData.Tables(1).TableName = "Table1"

                    ExportTemplate("Payroll\Import\Template_ThietLap_TiLeDatCacChiSo.xls", _
                                              dsData, Nothing, _
                                              "Template_ThietLap_TiLeDatCacChiSo" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái cho Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Try
            If rgMain.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgMain.SelectedItems(0)
            End If
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
                    If rep.IMPORT_PA_SETUP_INDEX(DocXml, LogHelper.CurrentUser.USERNAME) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("IMPORT_ERROR_PA_SETUP_INDEX") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_ERROR_PA_SETUP_INDEX')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            dtTemp.Columns(3).ColumnName = "INDEX_TYPE_NAME"
            dtTemp.Columns(4).ColumnName = "INDEX_TYPE_ID"
            dtTemp.Columns(5).ColumnName = "EFFECT_DATE"
            dtTemp.Columns(6).ColumnName = "FROM_COMPLETIONRATE"
            dtTemp.Columns(7).ColumnName = "TO_COMPLETIONRATE"
            dtTemp.Columns(8).ColumnName = "NUMERIC_FACTOR"
            dtTemp.Columns(9).ColumnName = "REMARK"
            dtTemp.Columns(10).ColumnName = "IS_GET_TLHT_NAME"
            dtTemp.Columns(11).ColumnName = "IS_GET_TLHT_ID"

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
                dtLogs.Columns.Add("DISCIPTION", GetType(String))
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
                If IsDBNull(rows("BRAND_NAME")) AndAlso IsDBNull(rows("BRAND_ID")) AndAlso IsDBNull(rows("INDEX_TYPE_NAME")) AndAlso IsDBNull(rows("INDEX_TYPE_ID")) AndAlso IsDBNull(rows("EFFECT_DATE")) AndAlso IsDBNull(rows("FROM_COMPLETIONRATE")) AndAlso IsDBNull(rows("TO_COMPLETIONRATE")) AndAlso IsDBNull(rows("NUMERIC_FACTOR")) AndAlso IsDBNull(rows("REMARK")) AndAlso IsDBNull(rows("IS_GET_TLHT_NAME")) AndAlso IsDBNull(rows("IS_GET_TLHT_ID")) Then
                    rows.Delete()
                    Continue For
                End If

                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                newRow = dtLogs.NewRow
                newRow("STT") = count + 1

                If IsDBNull(rows("BRAND_ID")) Then
                    newRow("DISCIPTION") = "Bạn phải chọn Nhãn hàng, "
                    _error = False
                End If

                If IsDBNull(rows("FROM_COMPLETIONRATE")) Or IsDBNull(rows("TO_COMPLETIONRATE")) Then
                    If IsDBNull(rows("FROM_COMPLETIONRATE")) Then
                        newRow("DISCIPTION") &= "Bạn phải chọn TLHT(%) Từ, "
                        _error = False
                    End If
                    If IsDBNull(rows("TO_COMPLETIONRATE")) Then
                        newRow("DISCIPTION") &= "Bạn phải chọn TLHT(%) Đến, "
                        _error = False
                    End If
                Else
                    If rows("TO_COMPLETIONRATE") < rows("FROM_COMPLETIONRATE") Then
                        newRow("DISCIPTION") &= "TLHT(%) Đến phải lớn hơn TLHT(%) Từ, "
                        _error = False
                    End If
                End If

                If IsDBNull(rows("INDEX_TYPE_ID")) Then
                    newRow("DISCIPTION") &= "Bạn phải chọn Loại chỉ số, "
                    _error = False
                End If

                If IsDBNull(rows("EFFECT_DATE")) OrElse rows("EFFECT_DATE") = "" Then
                    newRow("DISCIPTION") &= "Bạn phải chọn Ngày hiệu lực, "
                    _error = False
                Else
                    If CheckDate(rows("EFFECT_DATE"), effectDate) = False Then
                        newRow("DISCIPTION") &= "Ngày hiệu lực không đúng định dạng, "
                        _error = False
                    End If
                End If

                If IsDBNull(rows("NUMERIC_FACTOR")) Then
                    newRow("DISCIPTION") &= "Bạn phải nhập Tỉ lệ(%) đạt, "
                    _error = False
                End If

                If Not IsDBNull(rows("REMARK")) Then
                    Dim isValid As Boolean = regex.IsMatch(rows("REMARK").ToString.Trim)
                    If Not isValid Then
                        newRow("DISCIPTION") &= "Thông tin Ghi chú có chứa mã HTML, "
                        _error = False
                    End If
                End If

                If _error Then
                    Dim countExists As Decimal = 0
                    Dim obj As New PA_SETUP_INDEX_DTO

                    obj.EFFECT_DATE = effectDate
                    obj.FACTOR = Double.Parse(rows("NUMERIC_FACTOR").ToString.Replace(".", ","))
                    obj.BRAND = Double.Parse(rows("BRAND_ID"))
                    obj.INDEX_TYPE = Double.Parse(rows("INDEX_TYPE_ID"))

                    obj.ID = 0
                    If rep.VALIDATE_PA_SETUP_INDEX(obj) Then
                        countExists &= 1
                    End If

                    Dim sltQ = "INDEX_TYPE_ID = '" & rows("INDEX_TYPE_ID") & "' AND EFFECT_DATE = '" & obj.EFFECT_DATE.Value.Date.ToString("dd/MM/yyyy") & "' AND BRAND_ID = '" & rows("BRAND_ID") & "' AND NUMERIC_FACTOR = '" & rows("NUMERIC_FACTOR") & "'"
                    Dim query = dtTemp.Select(sltQ.ToString.Replace(",", "."))

                    If query.Any AndAlso query.Count > 1 Then
                        countExists &= 1
                    End If
                    If countExists > 0 Then
                        newRow("DISCIPTION") = "Dữ liệu đã tồn tại, "
                        _error = False
                    End If
                End If

                If _error = False Then
                    newRow("BRAND_NAME") = rows("BRAND_NAME")
                    newRow("BRAND_ID") = rows("BRAND_ID")
                    newRow("INDEX_TYPE_NAME") = rows("INDEX_TYPE_NAME")
                    newRow("INDEX_TYPE_ID") = rows("INDEX_TYPE_ID")
                    newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                    newRow("FROM_COMPLETIONRATE") = rows("FROM_COMPLETIONRATE")
                    newRow("TO_COMPLETIONRATE") = rows("TO_COMPLETIONRATE")
                    newRow("NUMERIC_FACTOR") = rows("NUMERIC_FACTOR")
                    newRow("REMARK") = rows("REMARK")
                    newRow("IS_GET_TLHT_NAME") = rows("IS_GET_TLHT_NAME")
                    newRow("IS_GET_TLHT_ID") = rows("IS_GET_TLHT_ID")
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