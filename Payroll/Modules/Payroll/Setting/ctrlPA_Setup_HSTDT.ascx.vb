Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Setup_HSTDT
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

    Property dtDataImportHSTDT As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportHSTDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportHSTDT") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("BRAND_NAME", GetType(String))
                dt.Columns.Add("BRAND", GetType(String))
                dt.Columns.Add("STAFF_GROUP_NAME", GetType(String))
                dt.Columns.Add("STAFF_GROUP", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("FROM_RATE", GetType(String))
                dt.Columns.Add("TO_RATE", GetType(String))
                dt.Columns.Add("COEFFICIENT", GetType(String))
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
                                       ToolbarItem.Delete, ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE,
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.None,
                                                                  "Xuất file mẫu"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                                  ToolbarIcons.Import,
                                                                  ToolbarAuthorize.None,
                                                                  "Nhập file mẫu"))
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
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
                        ClearControlValue(txtNote, cboBrand, cboModelShop, cboStaffGroup, rdEffectDate, numFromRate, numToRate, numHSTDT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtNote, cboBrand, cboModelShop, cboStaffGroup, rdEffectDate, numFromRate, numToRate, numHSTDT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(txtNote, cboBrand, cboModelShop, cboStaffGroup, rdEffectDate, numFromRate, numToRate, numHSTDT)
                    Case ""
                        cboBrand.AutoPostBack = False
                        cboModelShop.AutoPostBack = False
                        cboStaffGroup.AutoPostBack = False
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
        Dim _filter As New PA_SETUP_HSTDT_DTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_PA_SETUP_HSTDT(_filter, Sorts).ToTable()
                Else
                    Return rep.GET_PA_SETUP_HSTDT(_filter).ToTable()
                End If
            Else
                Dim PA_SETUP_HSTDT As List(Of PA_SETUP_HSTDT_DTO)
                If Sorts IsNot Nothing Then
                    PA_SETUP_HSTDT = rep.GET_PA_SETUP_HSTDT(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    PA_SETUP_HSTDT = rep.GET_PA_SETUP_HSTDT(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = PA_SETUP_HSTDT
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
                    EnableControlAll(True, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)
                    Refresh("Cancel")
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DELETE_PA_SETUP_HSTDT(lstDeletes) Then
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
                dataTable = rep.GetOtherList("MODELSHOP", True)
                FillRadCombobox(cboModelShop, dataTable, "NAME", "ID")
                dtData = rep.GetOtherList("STAFF_GROUP", True)
                FillRadCombobox(cboStaffGroup, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("BRAND", cboBrand)
            dic.Add("MODELSHOP", cboModelShop)
            dic.Add("STAFF_GROUP", cboStaffGroup)
            dic.Add("NOTE", txtNote)
            dic.Add("FROM_RATE", numFromRate)
            dic.Add("TO_RATE", numToRate)
            dic.Add("COEFFICIENT", numHSTDT)
            dic.Add("EFFECT_DATE", rdEffectDate)
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

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearControlValue(txtNote, cboBrand, cboModelShop, cboStaffGroup, rdEffectDate, numFromRate, numToRate, numHSTDT)
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
        Dim obj As New PA_SETUP_HSTDT_DTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_ImportHSTDT()

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()

                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(True, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)
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

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    ClearControlValue(True, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "PA_SETUP_HSTDT")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj.NOTE = txtNote.Text.Trim
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        obj.FROM_RATE = numFromRate.Value
                        obj.TO_RATE = numToRate.Value
                        obj.COEFFICIENT = numHSTDT.Value

                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = cboBrand.SelectedValue
                        End If
                        If cboModelShop.SelectedValue <> "" Then
                            obj.MODELSHOP = cboModelShop.SelectedValue
                        End If
                        If cboStaffGroup.SelectedValue <> "" Then
                            obj.STAFF_GROUP = cboStaffGroup.SelectedValue
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.VALIDATE_PA_SETUP_HSTDT(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.INSERT_PA_SETUP_HSTDT(obj, gID) Then
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
                                If rep.VALIDATE_PA_SETUP_HSTDT(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.MODIFY_PA_SETUP_HSTDT(obj, gID) Then
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
                    ClearControlValue(True, rdEffectDate, cboBrand, cboModelShop, cboStaffGroup, txtNote, numFromRate, numToRate, numHSTDT)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Template_ImportHSTDT()
        Dim rep As New Payroll.PayrollRepository
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As New DataSet
            dsData = rep.GetHSTDTImport()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            rep.Dispose()

            ExportTemplate("Payroll\Import\Template_ThietLap_HeSo_TDT.xls",
                                  dsData, Nothing, "Template_ThietLap_HeSo_TDT" & Format(Date.Now, "yyyymmdd"))


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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New PayrollRepository
            Dim sp As New PayrollStoreProcedure()
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("BRAND<>'""'").CopyToDataTable.Rows
                If (IsDBNull(rows("BRAND_NAME")) OrElse rows("BRAND_NAME") = "") And (IsDBNull(rows("STAFF_GROUP_NAME")) OrElse rows("STAFF_GROUP_NAME") = "") Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("BRAND_NAME") = rows("BRAND_NAME")
                newRow("BRAND") = rows("BRAND")
                newRow("STAFF_GROUP_NAME") = rows("STAFF_GROUP_NAME")
                newRow("STAFF_GROUP") = rows("STAFF_GROUP")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("FROM_RATE") = rows("FROM_RATE")
                newRow("TO_RATE") = rows("TO_RATE")
                newRow("COEFFICIENT") = rows("COEFFICIENT")
                newRow("NOTE") = rows("NOTE")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                If sp.IMPORT_HSTDT(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgMain.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
        'dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
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
            dtDataImportHSTDT = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim rep As New PayrollRepository
            Dim sp As New PayrollStoreProcedure()
            Dim dsData As DataSet = rep.GetHSTDTImport()
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                If row("BRAND") Is DBNull.Value OrElse row("BRAND") = "" Then
                    sError = "Chưa chọn nhãn hàng"
                    ImportValidate.EmptyValue("BRAND", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("BRAND")) = False Then
                        sError = "Nhãn hàng không đúng định dạng"
                        ImportValidate.IsValidNumber("BRAND", row, rowError, isError, sError)
                    End If
                End If
                If row("STAFF_GROUP") Is DBNull.Value OrElse row("STAFF_GROUP") = "" Then
                    sError = "Chưa chọn nhóm nhân viên"
                    ImportValidate.EmptyValue("STAFF_GROUP", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("STAFF_GROUP")) = False Then
                        sError = "Nhóm nhân viên không đúng định dạng"
                        ImportValidate.IsValidNumber("STAFF_GROUP", row, rowError, isError, sError)
                    End If
                End If
                If row("EFFECT_DATE") Is DBNull.Value OrElse row("EFFECT_DATE") = "" Then
                    sError = "Chưa nhập ngày hiệu lực"
                    ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("EFFECT_DATE")) = False Then
                        sError = "Ngày hiệu lực không đúng định dạng"
                        ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                    End If
                End If
                If row("FROM_RATE") Is DBNull.Value OrElse row("FROM_RATE") = "" Then
                    sError = "Chưa nhập TLDT từ"
                    ImportValidate.EmptyValue("FROM_RATE", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("FROM_RATE")) = False Then
                        sError = "TLDT từ không đúng định dạng"
                        ImportValidate.IsValidNumber("FROM_RATE", row, rowError, isError, sError)
                    End If
                End If
                If row("TO_RATE") Is DBNull.Value OrElse row("TO_RATE") = "" Then
                    sError = "Chưa nhập TLDT đến"
                    ImportValidate.EmptyValue("TO_RATE", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("TO_RATE")) = False Then
                        sError = "TLDT đến không đúng định dạng"
                        ImportValidate.IsValidNumber("TO_RATE", row, rowError, isError, sError)
                    End If
                End If
                If row("COEFFICIENT") Is DBNull.Value OrElse row("COEFFICIENT") = "" Then
                    sError = "Chưa nhập hệ số TDT"
                    ImportValidate.EmptyValue("COEFFICIENT", row, rowError, isError, sError)
                Else
                    If IsNumeric(row("COEFFICIENT")) = False Then
                        sError = "Hệ số TDT không đúng định dạng"
                        ImportValidate.IsValidNumber("COEFFICIENT", row, rowError, isError, sError)
                    End If
                End If

                If isError Then
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportHSTDT.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_HSTDT');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


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

#End Region
End Class