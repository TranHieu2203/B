Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSalaryFuncMapping
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Setting" + Me.GetType().Name.ToString()

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
            rgData.PageSize = 50
            GetDataCombo()
            CreateCol()
            CreateDataFilter()

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
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeach.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateCol()
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Event sụ kiên khi click [Cancel] trên popup Upload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            CreateCol()
            CreateDataFilter()

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
                    ShowMessage(Translate("Có lỗi xảy ra khi import file mẫu. Vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End Try

                Try
                    workbook = New Aspose.Cells.Workbook(fileName)
                    worksheet = workbook.Worksheets(0)
                    dsDataPre.Tables.Add(worksheet.Cells.ExportDataTableAsString(2, 0, worksheet.Cells.MaxRow - 1, worksheet.Cells.MaxColumn + 1, True))
                Catch ex As Exception
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End Try

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            If dsDataPre.Tables.Count > 0 Then
                vData = dsDataPre.Tables(0)
            End If

            Dim rep As New PayrollRepository
            Dim RecordSussces As Integer = 0

            If rep.SaveImportSalary_Fund_Mapping(Utilities.ObjToDecima(cboYear.SelectedValue), Utilities.ObjToDecima(cboSalaryType.SelectedValue), Utilities.ObjToDecima(cboPeriod.SelectedValue), vData, RecordSussces) Then
                ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            CreateCol()
            CreateDataFilter()
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

                    For Each col As GridColumn In rgData.Columns
                        If col.UniqueName <> "COL_NAME" And col.UniqueName <> "NAME_VN" And col.UniqueName <> "cbStatus" Then
                            Dim row As DataRow = dtColName.NewRow
                            row("COLVAL") = col.UniqueName
                            row("COLNAME") = col.HeaderText
                            row("COLDATA") = "&=DATA." & col.UniqueName
                            dtColName.Rows.Add(row)
                        End If
                    Next

                    Session("IMPORTSALARY_COLNAME") = dtColName
                    Using rep As New PayrollRepository
                        Dim dt = rep.GetMappingSalaryImport(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue))
                        Session("IMPORTSALARY_DATACOL") = dt
                    End Using
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSalary_FundMapping')", True)

                    rgData.CurrentPageIndex = 0
                    rgData.MasterTableView.SortExpressions.Clear()
                    CreateCol()
                    CreateDataFilter()

                Case CommonMessage.TOOLBARITEM_IMPORT
                    rgData.CurrentPageIndex = 0
                    rgData.MasterTableView.SortExpressions.Clear()
                    CreateCol()
                    CreateDataFilter()

                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim rep As New PayrollRepository
                    Dim stringKey As New List(Of String)
                    'rgData.Rebind()
                    'Dim dt As DataTable = rgData.MasterTableView.DataSource


                    For Each row As GridDataItem In rgData.Items
                        If rgData.Columns.Count >= 3 Then
                            For Each col As GridColumn In rgData.Columns
                                'Dim s As String = row[col.UniqueName].Text
                                If col.UniqueName <> "COL_NAME" And col.UniqueName <> "NAME_VN" And col.UniqueName <> "cbStatus" Then
                                    Dim s As String = row(col.UniqueName).Text
                                End If
                            Next
                        End If
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
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        'Dim rep As New ProfileBusinessRepository
        Try
            UpdateCotrolEnabled(False)
            'EnabledGridNotPostback(rgData, True)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL, CommonMessage.STATE_CANCEL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    UpdateCotrolEnabled(False)

                Case CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
            End Select

            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thái Grid
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgData.Enabled = bCheck
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
    ''' Event ColumnCreated của Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ColumnCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgData.ColumnCreated
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'CreateCol()
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

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            CreateCol()
            CreateDataFilter()

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
            Dim TotalRow As Decimal = 0
            vData = rep.GetMappingSalary(Utilities.ObjToInt(cboSalaryType.SelectedValue), Utilities.ObjToInt(cboPeriod.SelectedValue))

            rgData.VirtualItemCount = TotalRow
            rgData.DataSource = vData
            rgData.DataBind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>23/08/2017</lastupdate>
    ''' <summary>
    ''' Tạo column
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateCol()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Dim objPay As List(Of PaymentSourcesDTO)

        Try
            objPay = rep.GetPaymentSourcesbyYear(Utilities.ObjToInt(cboYear.SelectedValue))
            rgData.DataSource = New DataTable

            Dim listcol() As String = {"cbStatus", "ID", "COL_NAME", "NAME_VN"}
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
            stringKey.Add("COL_NAME")
            stringKey.Add("NAME_VN")

            For Each obj As PaymentSourcesDTO In objPay
                Dim col As New GridCheckBoxColumn
                col.HeaderText = obj.NAME
                col.DataField = obj.ID
                col.UniqueName = obj.ID
                col.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                col.HeaderStyle.Width = 30
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
            Dim objSal As SalaryTypeDTO
            objSal = New SalaryTypeDTO
            objSal.ACTFLG = "A"
            objSalatyType = rep.GetSalaryType(objSal)
            cboSalaryType.DataSource = objSalatyType
            cboSalaryType.DataValueField = "CODE"
            cboSalaryType.DataTextField = "NAME"
            cboSalaryType.DataBind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class