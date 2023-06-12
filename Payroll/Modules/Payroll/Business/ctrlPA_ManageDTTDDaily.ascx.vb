Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_ManageDTTDDaily
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Business" + Me.GetType().Name.ToString()


#Region "Property"

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STORE_NAME", GetType(String))
                dt.Columns.Add("STORE_ID", GetType(String))
                dt.Columns.Add("SALE_DATE", GetType(String))
                dt.Columns.Add("DTTD", GetType(String))
                dt.Columns.Add("DTTD_NG", GetType(String))
                dt.Columns.Add("DTTD_KNG1", GetType(String))
                dt.Columns.Add("DTTD_KNG2", GetType(String))
                dt.Columns.Add("TARGET_GROUP_ID", GetType(String))
                dt.Columns.Add("TARGET_GROUP_NAME", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property lstTarget As List(Of OT_OTHERLIST_DTO)
        Get
            Return ViewState(Me.ID & "_lstTarget")
        End Get
        Set(ByVal value As List(Of OT_OTHERLIST_DTO))
            ViewState(Me.ID & "_lstTarget") = value
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
    ''' Xét các trạng thái của grid rgData
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.AllowCustomPaging = True
            rgData.SetFilter()
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
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_TARGET_GROUP = True
            rep.GetComboboxData(ListComboData)
            lstTarget = ListComboData.LIST_TARGET_GROUP
            FillDropDownList(cboTARGET_GROUP, lstTarget, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTARGET_GROUP.SelectedValue)
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

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Calculate)
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")
            MainToolBar.Items(3).Text = Translate("Tải dữ liệu")
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
    ''' Bind lai du lieu cho grid rgData
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
        Dim rep As New PayrollRepository
        Dim sp As New PayrollStoreProcedure
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
                            rgData.ExportExcel(Server, Response, dtData, "DS_DTTD_DAILY")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARTIEM_CALCULATE

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dsData As DataSet = rep.GET_IMPORT_DTTD_DAILY()
                    dsData.Tables(0).TableName = "Table"
                    dsData.Tables(1).TableName = "Table1"
                    ExportTemplate("Payroll\Import\Template_import_DTCH_TheoNgay.xls",
                                              dsData, Nothing,
                                              "Template_import_DTCH_TheoNgay" & Format(Date.Now, "yyyyMMdd"))
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
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
                CurrentState = CommonMessage.STATE_DELETE
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_LOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_UNLOCK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
            End If
            UpdateControlState()
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
    ''' Xu ly su kien click cua button btnSearch
    ''' Thiets lap trang thai cho rgData
    ''' Bind lai du lieu cho rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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
                If String.IsNullOrEmpty(rows("STORE_ID").ToString) AndAlso String.IsNullOrEmpty(rows("TARGET_GROUP_ID").ToString) Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("TARGET_GROUP_NAME") = rows("TARGET_GROUP_NAME")
                newRow("TARGET_GROUP_ID") = rows("TARGET_GROUP_ID")
                newRow("STORE_ID") = rows("STORE_ID")
                newRow("STORE_NAME") = rows("STORE_NAME")
                newRow("SALE_DATE") = rows("SALE_DATE")
                newRow("DTTD") = rows("DTTD").ToString
                newRow("DTTD_NG") = rows("DTTD_NG").ToString
                newRow("DTTD_KNG1") = rows("DTTD_KNG1").ToString
                newRow("DTTD_KNG2") = rows("DTTD_KNG2").ToString
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim rep As New PayrollRepository
                If rep.IMPORT_DTTD_DAILY(DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
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
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai tbarContracts, rgData, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
        Dim _filter As New PA_ManageDTTDDailyDTO
        Try

            If cboTARGET_GROUP.SelectedValue <> "" Then
                _filter.TARGET_GROUP_ID = cboTARGET_GROUP.SelectedValue
            End If
            If IsDate(rdFromDate.SelectedDate) Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If IsDate(rdToDate.SelectedDate) Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_ManageDTTDDailyDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetManageDTTDDaily(_filter, Sorts).ToTable()
                Else
                    Return rep.GetManageDTTDDaily(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstObj = rep.GetManageDTTDDaily(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstObj = rep.GetManageDTTDDaily(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, )
                End If

                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = lstObj
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
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

                sError = "Chưa nhập Nhóm Target"
                ImportValidate.EmptyValue("TARGET_GROUP_ID", row, rowError, isError, sError)

                If Not IsDBNull(row("TARGET_GROUP_ID")) AndAlso Not String.IsNullOrEmpty(row("TARGET_GROUP_ID")) Then
                    Dim t_check = (From p In lstTarget Where p.ID = CDec(row("TARGET_GROUP_ID").ToString)).FirstOrDefault
                    If t_check IsNot Nothing AndAlso t_check.CODE.ToUpper.Equals("TARGET_CH") Then
                        sError = "Chưa nhập Mã cửa hàng"
                        ImportValidate.EmptyValue("STORE_NAME", row, rowError, isError, sError)
                    End If
                End If

                sError = "Chưa nhập ngày bán"
                ImportValidate.EmptyValue("SALE_DATE", row, rowError, isError, sError)

                sError = "Phải nhập kiểu' dd/mm/yyyy'"
                ImportValidate.IsValidDate("SALE_DATE", row, rowError, isError, sError)


                If Not IsDBNull(row("DTTD")) AndAlso Not String.IsNullOrEmpty(row("DTTD")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_NG")) AndAlso Not String.IsNullOrEmpty(row("DTTD_NG")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_NG", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_KNG1")) AndAlso Not String.IsNullOrEmpty(row("DTTD_KNG1")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_KNG1", row, rowError, isError, sError)
                End If

                If Not IsDBNull(row("DTTD_KNG2")) AndAlso Not String.IsNullOrEmpty(row("DTTD_KNG2")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("DTTD_KNG2", row, rowError, isError, sError)
                End If


                If isError Then
                    If IsDBNull(rowError("TARGET_GROUP_NAME")) Then
                        rowError("TARGET_GROUP_NAME") = row("TARGET_GROUP_NAME").ToString
                    End If
                    If IsDBNull(rowError("SALE_DATE")) Then
                        rowError("SALE_DATE") = row("SALE_DATE").ToString
                    End If
                    If IsDBNull(rowError("STORE_NAME")) Then
                        rowError("STORE_NAME") = row("STORE_NAME").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("DTTDDAILY_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_import_DTCH_TheoNgay_Error')", True)
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