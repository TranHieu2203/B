Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_TARGET_DTTD_LABEL
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"
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
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgDelegacy
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgDelegacy.AllowCustomPaging = True
            rgDelegacy.SetFilter()
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PayrollRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Dim table As New DataTable
            Dim objPeriod As List(Of ATPeriodDTO)
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            Dim row2 As DataRow = table.NewRow
            row2("ID") = DBNull.Value
            row2("YEAR") = DBNull.Value
            table.Rows.Add(row2)
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next

            ListComboData = New ComboBoxDataDTO
            cboBrand.ClearSelection()
            ListComboData.GET_BRAND = True
            rep.GetComboboxData(ListComboData)
            FillDropDownList(cboBrand, ListComboData.LIST_BRANDID, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboBrand.SelectedValue)
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            objPeriod = rep.GetPeriodbyYear(cboYear.SelectedValue)
            cboPeriod.DataSource = objPeriod.OrderBy(Function(x) x.MONTH).ToList()
            cboPeriod.DataValueField = "ID"
            cboPeriod.DataTextField = "PERIOD_NAME"
            cboPeriod.DataBind()

            Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
            If Not lst Is Nothing Then
                cboPeriod.SelectedValue = lst.ID
            Else
                cboPeriod.SelectedIndex = 0
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContracts

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CHECK,
                                                                   ToolbarIcons.Export,
                                                                   ToolbarAuthorize.Special1,
                                                                   Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Nhập file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARTIEM_CALCULATE,
                                                                   ToolbarIcons.Calculator,
                                                                   ToolbarAuthorize.Special1,
                                                                   Translate("Tải dữ liệu")))
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgDelegacy
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
            Else
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command in hop dong, xoa hop dong, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgDelegacy.ExportExcel(Server, Response, dtData, "Target_ActualRevenue")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CHECK
                    Template_Import()
                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        rgDelegacy.CurrentPageIndex = 0
    '        rgDelegacy.MasterTableView.SortExpressions.Clear()
    '        rgDelegacy.Rebind()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua button btnSearch
    ''' Thiets lap trang thai cho rgDelegacy
    ''' Bind lai du lieu cho rgDelegacy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDelegacy.CurrentPageIndex = 0
            rgDelegacy.MasterTableView.SortExpressions.Clear()
            rgDelegacy.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDelegacy.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub rgDelegacy_ItemDataBound1(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDelegacy.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgDelegacy_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDelegacy.ItemCreated
        If TypeOf e.Item Is GridEditableItem Then
            'e.Item.Edit = True
        End If
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(cboPeriod)
            If cboYear.SelectedValue <> "" Then
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
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai tbarContracts, rgDelegacy, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim _filter As New PA_TARGET_DTTD_LABELDTO
        Try

            'If ctrlOrg.CurrentValue Is Nothing Then
            '    rgDelegacy.DataSource = New List(Of PA_ADDTAXDTO)
            '    Exit Function
            'End If


            '_filter.IS_TER = chkTerminate.Checked
            If cboYear.SelectedValue <> "" Then
                _filter.YEAR = cboYear.SelectedValue
            End If
            If cboPeriod.SelectedValue <> "" Then
                _filter.SALEMONTH = cboPeriod.SelectedValue
            End If
            If cboBrand.SelectedValue <> "" Then
                _filter.BRANDID = cboBrand.SelectedValue
            End If
            SetValueObjectByRadGrid(rgDelegacy, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgDelegacy.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_TARGET_DTTD_LABELDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPA_TARGET_DTTD_LABEL(_filter, Sorts).ToTable()
                Else
                    Return rep.GetPA_TARGET_DTTD_LABEL(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstObj = rep.GetPA_TARGET_DTTD_LABEL(_filter, rgDelegacy.CurrentPageIndex, rgDelegacy.PageSize, MaximumRows, Sorts)
                Else
                    lstObj = rep.GetPA_TARGET_DTTD_LABEL(_filter, rgDelegacy.CurrentPageIndex, rgDelegacy.PageSize, MaximumRows)
                End If

                rgDelegacy.VirtualItemCount = MaximumRows
                rgDelegacy.DataSource = lstObj
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub Template_Import()
        Dim rep As New PayrollRepository
        Try
            Dim dsData As DataSet = rep.GET_IMPORT_PA_TARGET_DTTD_LABEL()
            dsData.Tables(0).TableName = "Table"
            ExportTemplate("Payroll\Import\Template_Import_Brand.xls",
                                  dsData, Nothing, "Template_Import_Brand" & Format(Date.Now, "yyyymmdd"))


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
    Private Sub Import_Data()
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

                'Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    'DocXml = sw.ToString
                    Dim RecordSussces As Integer = 0
                    If rep.IMPORT_PA_TARGET_DTTD_LABEL(ds.Tables(0), RecordSussces) Then
                        ShowMessage("Lưu dữ liệu thành công " & RecordSussces & " bản ghi.", NotifyType.Success)
                        rgDelegacy.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('PA_TARGET_DTTD_LABEL')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            ShowMessage(Translate("Có lỗi trong quá trình import"), Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        'Try
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "BRAND_NAME"
        dtTemp.Columns(2).ColumnName = "BRANDID"
        dtTemp.Columns(3).ColumnName = "YEAR"
        dtTemp.Columns(4).ColumnName = "MONTH"
        dtTemp.Columns(5).ColumnName = "TARGETBRAND"
        dtTemp.Columns(6).ColumnName = "TOTALREVENUE"
        dtTemp.Columns(7).ColumnName = "NOTE"
        dtTemp.Columns.Add("SALEMONTH", GetType(Integer))
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("STT", GetType(Integer))
            dtLogs.Columns.Add("BRAND_NAME", GetType(String))
            dtLogs.Columns.Add("YEAR", GetType(String))
            dtLogs.Columns.Add("MONTH", GetType(String))
            dtLogs.Columns.Add("TARGETBRAND", GetType(String))
            dtLogs.Columns.Add("TOTALREVENUE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 3 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("STT").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("STT") = rows("STT")
            newRow("BRAND_NAME") = rows("BRAND_NAME")
            newRow("YEAR") = rows("YEAR")
            newRow("MONTH") = rows("MONTH")
            If rows("BRANDID").ToString.Trim Is Nothing OrElse rows("BRANDID").ToString.Trim.Length = 0 Then
                newRow("DISCIPTION") = "Nhãn hàng- bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("BRANDID")) Then
                    newRow("DISCIPTION") = "Nhãn hàng- Sai định dạng,"
                    _error = False
                End If
            End If

            If rows("YEAR").ToString.Trim Is Nothing OrElse rows("YEAR").ToString.Trim.Length = 0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Năm- bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("YEAR")) Then
                    'rows("YEAR") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Năm - Sai định dạng,"
                    _error = False
                End If
            End If
            If rows("MONTH").ToString.Trim Is Nothing OrElse rows("MONTH").ToString.Trim.Length = 0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng- bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("MONTH")) Then
                    'rows("MONTH") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng - sai định dạng,"
                    _error = False
                End If
            End If
            If rows("TARGETBRAND").ToString.Trim Is Nothing OrElse rows("TARGETBRAND").ToString.Trim.Length = 0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Chỉ tiêu doanh số- bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("TARGETBRAND")) Then
                    'rows("TARGETBRAND") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Chỉ tiêu doanh số - sai định dạng,"
                    _error = False
                End If
            End If
            If rows("TOTALREVENUE").ToString.Trim Is Nothing OrElse rows("TOTALREVENUE").ToString.Trim.Length = 0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tổng doanh thu- bắt buộc nhập,"
                _error = False
            Else
                If Not IsNumeric(rows("TOTALREVENUE")) Then
                    'rows("TOTALREVENUE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tổng doanh thu - sai định dạng,"
                    _error = False
                End If
            End If
            If IsNumeric(rows("YEAR")) AndAlso IsNumeric(rows("MONTH")) Then
                objPeriod = rep.GetPeriodbyYear(CDec(rows("YEAR")))
                Dim lst = (From s In objPeriod Where s.MONTH = CDec(rows("MONTH"))).FirstOrDefault
                If Not lst Is Nothing Then
                    rows("SALEMONTH") = lst.ID
                Else
                    'rows("SALEMONTH") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng bán hàng - Không tồn tại,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
        'Catch ex As Exception

        'End Try
    End Sub
#End Region
End Class