Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Setup_LDTTT_NV_QLCH
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


    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("BRAND_NAME", GetType(String))
                dt.Columns.Add("BRAND_ID", GetType(String))
                dt.Columns.Add("STAFF_OBJECT_NAME", GetType(String))
                dt.Columns.Add("STAFF_OBJECT_ID", GetType(String))
                dt.Columns.Add("GRADE_NAME", GetType(String))
                dt.Columns.Add("GRADE_ID", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("FROM_RATE", GetType(String))
                dt.Columns.Add("TO_RATE", GetType(String))
                dt.Columns.Add("LDTTT", GetType(String))
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
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Nhập file mẫu")
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
                        ClearControlValue(txtNote, cboBrand, cboGrade, cboStaffObject, rdEffectDate, numFromRate, numToRate, numLDTTT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtNote, cboBrand, cboGrade, cboStaffObject, rdEffectDate, numFromRate, numToRate, numLDTTT)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(txtNote, cboBrand, cboGrade, cboStaffObject, rdEffectDate, numFromRate, numToRate, numLDTTT)
                    Case ""
                        cboBrand.AutoPostBack = False
                        cboStaffObject.AutoPostBack = False
                        cboGrade.AutoPostBack = False
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
        Dim _filter As New PA_SETUP_LDTTT_NV_QLCH_DTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, Sorts).ToTable()
                Else
                    Return rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter).ToTable()
                End If
            Else
                Dim PA_SETUP_LDTTT_NV_QLCH As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)
                If Sorts IsNot Nothing Then
                    PA_SETUP_LDTTT_NV_QLCH = rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    PA_SETUP_LDTTT_NV_QLCH = rep.GET_PA_SETUP_LDTT_NV_QLCH(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = PA_SETUP_LDTTT_NV_QLCH
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
                    EnableControlAll(True, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)
                    Refresh("Cancel")
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeletePA_SETUP_LDTT_NV_QLCH(lstDeletes) Then
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
                dataTable = rep.GetOtherList("STAFF_GROUP", True)
                FillRadCombobox(cboStaffObject, dataTable, "NAME", "ID")
                dtData = rep.GetOtherList("GRADE_XLCH", True)
                FillRadCombobox(cboGrade, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("BRAND", cboBrand)
            dic.Add("GRADE", cboGrade)
            dic.Add("STAFF_OBJECT", cboStaffObject)
            dic.Add("NOTE", txtNote)
            dic.Add("FROM_RATE", numFromRate)
            dic.Add("TO_RATE", numToRate)
            dic.Add("LDTTT", numLDTTT)
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
                ClearControlValue(txtNote, cboBrand, cboGrade, cboStaffObject, rdEffectDate, numFromRate, numToRate, numLDTTT)
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
        Dim obj As New PA_SETUP_LDTTT_NV_QLCH_DTO
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(True, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)
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

                    ClearControlValue(True, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "PA_SETUP_TDTTT_GROUPNV")
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
                        obj.LDTTT = numLDTTT.Value

                        If cboBrand.SelectedValue <> "" Then
                            obj.BRAND = cboBrand.SelectedValue
                        End If
                        If cboGrade.SelectedValue <> "" Then
                            obj.GRADE = cboGrade.SelectedValue
                        End If
                        If cboStaffObject.SelectedValue <> "" Then
                            obj.STAFF_OBJECT = cboStaffObject.SelectedValue
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.ValidatePA_SETUP_LDTT_NV_QLCH(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.InsertPA_SETUP_LDTTT_NV_QLCH(obj, gID) Then
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
                                If rep.ValidatePA_SETUP_LDTT_NV_QLCH(obj) Then
                                    ShowMessage("Dữ liệu đã tồn tại", Utilities.NotifyType.Error)
                                ElseIf rep.ModifyPA_SETUP_LDTTT_NV_QLCH(obj, gID) Then
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
                    ClearControlValue(True, rdEffectDate, cboBrand, cboGrade, cboStaffObject, txtNote, numFromRate, numToRate, numLDTTT)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dsData As DataSet = rep.Get_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT()
                    dsData.Tables(0).TableName = "Table"
                    dsData.Tables(1).TableName = "Table1"
                    dsData.Tables(2).TableName = "Table2"
                    ''ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TimeSheet_Monthly')", True)
                    ExportTemplate("Payroll\Import\Template_ThietLap_TDTTT_NhomNV.xls",
                                              dsData, Nothing,
                                              "Template_ThietLap_TDTTT_NhomNV" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
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
                newRow("BRAND_NAME") = rows("BRAND_NAME")
                newRow("BRAND_ID") = rows("BRAND_ID")
                newRow("STT") = rows("STT")
                newRow("STAFF_OBJECT_NAME") = rows("STAFF_OBJECT_NAME")
                newRow("STAFF_OBJECT_ID") = rows("STAFF_OBJECT_ID")
                newRow("GRADE_NAME") = rows("GRADE_NAME")
                newRow("GRADE_ID") = rows("GRADE_ID")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("FROM_RATE") = rows("FROM_RATE")
                newRow("TO_RATE") = rows("TO_RATE")
                newRow("LDTTT") = rows("LDTTT")
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
                If rep.IMPORT_PA_SETUP_LDTT_NV_QLCH(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import lỗi"), NotifyType.Error)
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

                sError = "Chưa chọn nhãn hàng"
                ImportValidate.EmptyValue("BRAND_NAME", row, rowError, isError, sError)

                sError = "Chưa chọn đối tượng nhân viên"
                ImportValidate.EmptyValue("STAFF_OBJECT_NAME", row, rowError, isError, sError)

                sError = "Chưa chọn xếp loại"
                ImportValidate.EmptyValue("GRADE_NAME", row, rowError, isError, sError)

                sError = "Chưa nhập ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)

                sError = "Chưa nhập TLDT từ"
                ImportValidate.EmptyValue("FROM_RATE", row, rowError, isError, sError)

                sError = "Chưa nhập TLDT đến"
                ImportValidate.EmptyValue("TO_RATE", row, rowError, isError, sError)

                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) Then
                    sError = "Phải nhập ngày tháng"
                    ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("FROM_RATE")) AndAlso Not String.IsNullOrEmpty(row("FROM_RATE")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("FROM_RATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("TO_RATE")) AndAlso Not String.IsNullOrEmpty(row("TO_RATE")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("TO_RATE", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("LDTTT")) AndAlso Not String.IsNullOrEmpty(row("LDTTT")) Then
                    sError = "Phải nhập số"
                    ImportValidate.IsValidNumber("LDTTT", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("EFFECT_DATE")) AndAlso Not String.IsNullOrEmpty(row("EFFECT_DATE")) AndAlso IsDate(row("EFFECT_DATE")) AndAlso
                    Not IsDBNull(row("LDTTT")) AndAlso Not String.IsNullOrEmpty(row("LDTTT")) AndAlso
                    Not IsDBNull(row("BRAND_ID")) AndAlso Not String.IsNullOrEmpty(row("BRAND_ID")) AndAlso
                    Not IsDBNull(row("STAFF_OBJECT_ID")) AndAlso Not String.IsNullOrEmpty(row("STAFF_OBJECT_ID")) AndAlso
                    Not IsDBNull(row("GRADE_ID")) AndAlso Not String.IsNullOrEmpty(row("GRADE_ID")) AndAlso
                    IsNumeric(row("FROM_RATE")) AndAlso IsNumeric(row("TO_RATE")) Then
                    If rep.ValidatePA_SETUP_LDTT_NV_QLCH(New PA_SETUP_LDTTT_NV_QLCH_DTO With {.BRAND = CDec(row("BRAND_ID")),
                                                                                              .STAFF_OBJECT = CDec(row("STAFF_OBJECT_ID")),
                                                                                              .ID = 0,
                                                                                              .GRADE = CDec(row("GRADE_ID")),
                                                                                              .LDTTT = CDec(row("LDTTT").ToString.Trim.Replace(".", ",")),
                                                                                              .EFFECT_DATE = CDate(row("EFFECT_DATE")),
                                                                                              .FROM_RATE = CDec(row("FROM_RATE").ToString.Trim.Replace(".", ",")),
                                                                                              .TO_RATE = CDec(row("TO_RATE").ToString.Trim.Replace(".", ","))}) Then
                        sError = "Dữ liệu đã tồn tại"
                        ImportValidate.IsValidTime("BRAND_NAME", row, rowError, isError, sError)
                    End If

                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("PA_SETUP_LDTT_NV_QLCH") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ThietLap_TDTTT_NhomNV_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


#End Region
End Class