Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlImportLuong
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtData As New DataTable

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    ''' <summary>
    ''' vData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property vData As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataView")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataView") = value
        End Set
    End Property

    Property dataExport As DataTable
        Get
            Return ViewState(Me.ID & "_dataExport")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dataExport") = value
        End Set
    End Property

    ''' <summary>
    ''' ListNodeChecked 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListKey As List(Of String)
        Get
            Return ViewState(Me.ID & "_ListKey")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_ListKey") = value
        End Set
    End Property

    Property Temp As String
        Get
            Return ViewState(Me.ID & "_Temp")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Temp") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            ctrlUpload.AllowedExtensions = "xls,xlsx"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not Page.IsPostBack Then
                rgData.PageSize = 50
                GetDataCombo()
                CreateTreeSalaryNote()
                CreateDataFilter()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.Import)

            MainToolBar.Items(0).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(1).Text = Translate("Nhập file mẫu")

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event click button Tìm kiếm 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeach.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateTreeSalaryNote()
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event sụ kiên khi click [OK] trên popup Upload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPre As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("File mẫu không tồn tại"), NotifyType.Warning)
                Exit Sub
            End If

            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                Try
                    fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                    file.SaveAs(fileName, True)
                Catch ex As Exception
                    ShowMessage(Translate("Có lỗi xảy ra khi import file. Vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End Try

                Try
                    workbook = New Aspose.Cells.Workbook(fileName)
                    worksheet = workbook.Worksheets(0)
                    ' Dim tb = worksheet.Cells.ExportDataTable(2, 0, worksheet.Cells.MaxRow - 1, worksheet.Cells.MaxColumn + 1, True)
                    Dim tb = worksheet.Cells.ExportDataTableAsString(2, 0, worksheet.Cells.MaxRow - 1, worksheet.Cells.MaxColumn + 1, True)
                    dsDataPre.Tables.Add(tb)
                Catch ex As Exception
                    ShowMessage(Translate("File import không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End Try

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            If dsDataPre.Tables.Count > 0 Then
                For Each s As String In rgData.MasterTableView.ClientDataKeyNames
                    If Not dsDataPre.Tables(0).Columns.Contains(s) Then
                        dsDataPre.Tables(0).Columns.Add(s)
                    End If
                Next
                vData = dsDataPre.Tables(0)
            End If


            Dim rep As New PayrollRepository
            Dim stringKey As New List(Of String)

            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If node.Value = "NULL" Or node.Value = "0" Then Continue For
                stringKey.Add(node.Value)
            Next

            Dim RecordSussces As Integer = 0
            If stringKey.Count <= 0 Then
                ShowMessage(Translate("Bạn phải chọn ít nhất 1 khoản tiền"), NotifyType.Warning)
                Exit Sub
            End If
            TableMapping(vData, stringKey)

            Dim iD_NHOMLUONG = 0
            'For Each row As DataRow In dataExport.Rows
            '    iD_NHOMLUONG = Decimal.Parse(row("ID_NHOM_LUONG").ToString())
            '    If iD_NHOMLUONG <> 0 Then Exit For
            'Next
            For Each row As DataRow In vData.Rows
                iD_NHOMLUONG = Decimal.Parse(row("ID_NHOM_LUONG").ToString())
                If iD_NHOMLUONG <> 0 Then Exit For
            Next
            If iD_NHOMLUONG <> Decimal.Parse(cboSalaryType.SelectedValue) Then
                ShowMessage("Bạn không thể Import khác nhóm lương", Utilities.NotifyType.Error)
                Exit Sub
            End If

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                If rep.SaveImport(Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
                    ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

                If RecordSussces <> 0 Then
                    rgData.DataSource = vData
                    rgData.DataBind()
                Else
                    rgData.Rebind()
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If




            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)        
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtdata As DataTable, ByVal str_col As List(Of String))
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer = 0
        Dim newRow As DataRow
        Dim empId As Integer
        Dim rep As New PayrollRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("YEAR", GetType(String))
            dtLogs.Columns.Add("LEAVE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtdata.Rows.Count - 1
            If dtdata.Rows(i).RowState = DataRowState.Deleted OrElse dtdata.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtdata.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtdata.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtdata.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            'empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))
            'empId = rep.check_import_sal_ter(rows("EMPLOYEE_CODE"), LogHelper.CurrentUser.USERNAME.ToUpper, cboPeriod.SelectedValue, cboSalaryType.SelectedValue, cboSalGroup.SelectedValue)

            'If empId = 1 Then
            '    newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
            '    _error = False
            'ElseIf empId = 2 Then
            '    newRow("DISCIPTION") = "Mã nhân viên - không được phân quyền,"
            '    _error = False
            'ElseIf empId = 3 Then
            '    newRow("DISCIPTION") = "Mã nhân viên - Ngày nghỉ việc lớn hơn ngày bắt đầu kỳ công,"
            '    _error = False
            'ElseIf empId = 4 Then
            '    newRow("DISCIPTION") = "Import bị lỗi. Kiểm tra validate Import,"
            '    _error = False
            'ElseIf empId = 5 Then
            '    newRow("DISCIPTION") = "Mã nhân viên - Nhân viên chưa được phân quyền cấp bậc L,"
            '    _error = False
            'End If

            For Each item In str_col
                If rows(item).ToString <> "" Then
                    If Not (IsNumeric(rows(item))) Then
                        rows(item) = 0
                        newRow("DISCIPTION") = newRow("DISCIPTION") + item + " - chỉ được nhập số,"
                        _error = False
                    End If
                End If
            Next

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
        Next


        dtdata.AcceptChanges()
    End Sub


    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event sụ kiên khi click node cua treeview Lương
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlListSalary_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles ctrlListSalary.NodeCheck
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim stringKey As New List(Of String)

        Try
            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If node.Value = "NULL" Or node.Value = "0" Then Continue For
                stringKey.Add(node.Value)
            Next

            ListKey = stringKey
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event thay đổi giá trị trên combobbox KỲ CÔNG
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtColName As New DataTable
                    dtColName.Columns.Add("COLVAL")
                    dtColName.Columns.Add("COLNAME")
                    dtColName.Columns.Add("COLDATA")

                    For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                        If node.Value = "NULL" Or node.Value = "0" Then Continue For
                        Dim row As DataRow = dtColName.NewRow
                        row("COLVAL") = node.Value
                        row("COLNAME") = node.Text.Split(":")(1).Trim()
                        row("COLDATA") = "&=DATA." & node.Value
                        dtColName.Rows.Add(row)
                    Next
                    Session("IMPORTSALARY_COLNAME") = dtColName
                    Using rep As New PayrollRepository
                        dataExport = rep.GetImportSalary(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
                        Session("IMPORTSALARY_DATACOL") = dataExport
                    End Using
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSalary')", True)

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim rep As New PayrollRepository
                    Dim stringKey As New List(Of String)

                    For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                        If node.Value = "NULL" Or node.Value = "0" Then Continue For
                        stringKey.Add(node.Value)
                    Next

                    Dim RecordSussces As Integer = 0
                    If stringKey.Count <= 0 Then
                        ShowMessage("Bạn phải chọn ít nhất 1 khoản tiền", NotifyType.Warning)
                        Exit Sub
                    End If

                    If rep.SaveImport(Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, stringKey, RecordSussces) Then
                        ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
                    End If
                    rep.Dispose()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control
    ''' </summary>
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
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event sụ kiên khi click node cua popup Tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboSalaryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSalaryType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            btnSeach_Click(Nothing, Nothing)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event tạo cột cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ColumnCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgData.ColumnCreated
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateCol()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event thay đổi giá trị trên combobbox NĂM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)

        Try
            cboPeriod.ClearSelection()
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()

            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository

        Try
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim TotalRow As Decimal = 0

            If Sorts Is Nothing Then
                vData = rep.GetImportSalary(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text))
            Else
                vData = rep.GetImportSalary(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue), Utilities.ObjToInt(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve, Utilities.ObjToString(rtxtEmployee.Text), Sorts)
            End If

            rgData.VirtualItemCount = Utilities.ObjToInt(vData.Rows.Count)

            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If Not vData.Columns.Contains(node.Value) Then
                    vData.Columns.Add(node.Value)
                End If
            Next
            rep.Dispose()
            rgData.DataSource = vData

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly tạo cột cho treeview lương
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateTreeSalaryNote()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlListSalary.Nodes.Clear()
            Dim rep As New PayrollRepository
            Dim objSalari As List(Of PAListSalariesDTO)
            objSalari = rep.GetSalaryList_TYPE(Utilities.ObjToInt(cboSalaryType.SelectedValue))

            Dim node As New RadTreeNode
            node.Value = 0
            node.Text = Translate("Chọn tất cả")
            ctrlListSalary.Nodes.Add(node)

            For Each item In objSalari
                Dim nodeChild As New RadTreeNode
                nodeChild.Value = item.COL_NAME
                nodeChild.Text = item.NAME_VN
                node.Nodes.Add(nodeChild)
            Next

            ctrlListSalary.Nodes.Add(node)
            ctrlListSalary.ExpandAllNodes()

            If ListKey IsNot Nothing Then
                For Each itemNode As RadTreeNode In ctrlListSalary.GetAllNodes()
                    For Each key As String In ListKey
                        If itemNode.Value = key Then
                            itemNode.Checked = True
                        End If
                    Next
                Next
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly tạo cột cho Grid 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateCol()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim listcol() As String = {"cbStatus", "EMPLOYEE_CODE", "FULLNAME_VN", "ORG_NAME"}
            Dim i As Integer = 0

            While (i < rgData.Columns.Count)
                Dim c As GridColumn = rgData.Columns(i)
                If Not listcol.Contains(c.UniqueName) Then
                    rgData.Columns.Remove(c)
                    Continue While
                End If
                i = i + 1
            End While

            Dim stringKey As New List(Of String)
            stringKey.Add("ID")
            stringKey.Add("EMPLOYEE_CODE")
            stringKey.Add("FULLNAME_VN")
            stringKey.Add("ORG_NAME")

            For Each node As RadTreeNode In ctrlListSalary.CheckedNodes
                If node.Value = "NULL" Or node.Value = "0" Then Continue For
                Dim col As New GridBoundColumn
                col.DataFormatString = "{0:#,##0.##}"
                col.HeaderText = node.Text.Split(":")(1).Trim()
                col.DataField = node.Value
                col.UniqueName = node.Value
                col.HeaderStyle.Width = 120
                rgData.MasterTableView.Columns.Add(col)
                stringKey.Add(col.DataField)
            Next

            rgData.MasterTableView.ClientDataKeyNames = stringKey.ToArray

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim objSalatyType As List(Of SalaryTypeDTO)
        Dim id As Integer = 0

        Try
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow

            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next

            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()

            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If

            'Get Salary Type
            objSalatyType = rep.GetSalaryTypebyIncentive(0)
            cboSalaryType.DataSource = objSalatyType
            cboSalaryType.DataValueField = "ID"
            cboSalaryType.DataTextField = "NAME"
            cboSalaryType.DataBind()
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class