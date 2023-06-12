Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_WorkInfo
    Inherits Common.CommonView

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    Private TYPEREPORT As ArrayList

#Region "Properties"
    Public Property popupId As String

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

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
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try

                Dim startTime As DateTime = DateTime.UtcNow
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                rgEmployeeList.SetFilter()
                Refresh()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                'DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                ViewConfig(RadPane1)
                ViewConfig(RadPane4)
                GirdConfig(rgEmployeeList)
            End If
            'rgEmployeeList.SetFilter()
            'rgEmployeeList.AllowCustomPaging = True
            'rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Seperator, ToolbarItem.Next, ToolbarItem.Import,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = "Nhập file mẫu"
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("PRINT_CV", ToolbarIcons.Print,
            '                                                        ToolbarAuthorize.Export, Translate("In lý lịch trích ngang")))

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdToDate.SelectedDate IsNot Nothing AndAlso rdFromDate.SelectedDate IsNot Nothing Then
                If rdToDate.SelectedDate < rdFromDate.SelectedDate Then
                    ShowMessage("Đến ngày phải lớn hơn Từ ngày", NotifyType.Warning)
                    Exit Sub
                End If
            End If
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objEmployee As New EmployeeDTO
            Dim rep As New ProfileBusinessRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn dòng trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgEmployeeList.ExportExcel(Server, Response, dtData, "EmployeeList")
                        Else
                            ShowMessage(Translate(MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportWorkInf()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
            End Select
            rep.Dispose()
            ' UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Template_ImportWorkInf()
        Dim rep As New ProfileStoreProcedure
        Try
            Dim configPath As String = "Profile\Template_WorkInfBefore.xlsx"

            ' Dim dsData As DataSet = rep.GET_DATA_IMPORT_FAMILY()

            If File.Exists(System.IO.Path.Combine(Server.MapPath("ReportTemplates\" + configPath))) Then

                Using xls As New AsposeExcelCommon
                    Dim bCheck = ExportTemplate(configPath, Nothing, Nothing, "Template_QL_QUATRINHCONGTACTRUOCDAY_" & Format(Date.Now, "yyyyMMdd"))

                End Using
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlUpload_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileStoreProcedure
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
            TableMapping(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_DATA_WORKING_BEFORE(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgEmployeeList.Rebind()
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

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_NAME"
            dtTemp.Columns(2).ColumnName = "COMPANY_NAME"
            dtTemp.Columns(3).ColumnName = "DEPARTMENT"
            dtTemp.Columns(4).ColumnName = "TITLE_NAME"
            dtTemp.Columns(5).ColumnName = "WORK"
            dtTemp.Columns(6).ColumnName = "TER_REASON"
            dtTemp.Columns(7).ColumnName = "COMPANY_ADDRESS"
            dtTemp.Columns(8).ColumnName = "JOIN_DATE"
            dtTemp.Columns(9).ColumnName = "END_DATE"
            dtTemp.Columns(10).ColumnName = "IS_HSV"
            dtTemp.Columns(11).ColumnName = "IS_THAMNIEN"
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim empId As Integer
            Dim startDate As Date
            Dim rep As New ProfileBusinessRepository
            Dim result As Integer

            If dtLogs Is Nothing Then
                dtLogs = New DataTable("data")
                dtLogs.Columns.Add("STT", GetType(Integer))
                dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dtLogs.Columns.Add("DISCIPTION", GetType(String))
            End If
            dtLogs.Clear()

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 3 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
                newRow = dtLogs.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

                empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

                If empId = 0 Then
                    newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                    _error = False
                Else
                    rows("EMPLOYEE_CODE") = empId
                End If

                If IsDBNull(rows("COMPANY_NAME")) OrElse rows("COMPANY_NAME") = "" Then
                    rows("COMPANY_NAME") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tên công ty - Bắt buộc nhập,"
                    _error = False
                End If

                If IsDBNull(rows("DEPARTMENT")) OrElse rows("DEPARTMENT") = "" Then
                    rows("DEPARTMENT") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Phòng ban - Bắt buộc nhập,"
                    _error = False
                End If

                If IsDBNull(rows("TITLE_NAME")) OrElse rows("TITLE_NAME") = "" Then
                    rows("TITLE_NAME") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Vị trí công việc - Bắt buộc nhập,"
                    _error = False
                End If

                If IsDBNull(rows("JOIN_DATE")) OrElse rows("JOIN_DATE") = "" Then
                    rows("JOIN_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ ngày - Bắt buộc nhập,"
                ElseIf CheckDate(rows("JOIN_DATE"), startDate) = False Then
                    rows("JOIN_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ ngày - Không đúng định dạng,"
                    _error = False
                End If

                If IsDBNull(rows("END_DATE")) OrElse rows("END_DATE") = "" Then
                    rows("END_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến ngày - Bắt buộc nhập,"
                ElseIf CheckDate(rows("END_DATE"), startDate) = False Then
                    rows("END_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến ngày - Không đúng định dạng,"
                    _error = False
                End If

                If _error = False Then
                    dtLogs.Rows.Add(newRow)
                    _error = True
                End If
                count += 1
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
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

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                   ByVal dsData As DataSet,
                                                   ByVal dtVariable As DataTable,
                                                   ByVal filename As String) As Boolean
        Dim filePath As String
        Dim templatefolder As String
        Dim filePathOut As String
        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName
            filePathOut = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\"
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", Aspose.Cells.ContentDisposition.Attachment, New XlsSaveOptions(Aspose.Cells.SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    ''' <summary>
    ''' Xử lý sự kiện item databound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    ''' <summary>

    ''' Xử lý sự kiện NeedDataSource của rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            CreateDataFilter()

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' RadGrid_PageIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' AjaxManager_AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgEmployeeList.CurrentPageIndex = 0
                rgEmployeeList.Rebind()
                If rgEmployeeList.Items IsNot Nothing AndAlso rgEmployeeList.Items.Count > 0 Then
                    rgEmployeeList.Items(0).Selected = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Create date:20/10/2017
    ''' Create by: CHIENNV
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub
    Private Sub rgEmployeeList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployeeList.ItemCommand
        Try
            If e.CommandName = "Dowload" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveWorkingBefore_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "View" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            Dim strName = If(datarow.GetDataKeyValue("FILE_NAME") IsNot Nothing, datarow.GetDataKeyValue("FILE_NAME").ToString, "")
            If strName <> "" Then
                datarow("DowloadCommandColumn").Enabled = True
            Else
                datarow("DowloadCommandColumn").Enabled = False
                datarow("DowloadCommandColumn").CssClass = "hide-button"
            End If
            If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Then
                datarow("ViewCommandColumn").Enabled = True
            Else
                datarow("ViewCommandColumn").Enabled = False
                datarow("ViewCommandColumn").CssClass = "hide-button"
            End If
        End If
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Hàm xử lý tạo dữ liệu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim EmployeeList As List(Of WorkingBeforeDTO)
        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New WorkingBeforeDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileBusinessRepository
                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgEmployeeList.DataSource = New List(Of EmployeeDTO)
                    Exit Function
                End If

                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}


                SetValueObjectByRadGrid(rgEmployeeList, _filter)

                If rdFromDate.SelectedDate IsNot Nothing Then
                    _filter.JOIN_DATE = rdFromDate.SelectedDate
                End If

                If rdToDate.SelectedDate IsNot Nothing Then
                    _filter.END_DATE = rdToDate.SelectedDate
                End If

                _filter.IS_TER = chkTerminate.Checked

                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListWorkingBefore(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListWorkingBefore(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListWorkingBefore(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListWorkingBefore(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    End If
                    rgEmployeeList.VirtualItemCount = MaximumRows
                    rgEmployeeList.DataSource = EmployeeList
                End If

            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileBusinessRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstID As New List(Of Decimal)
                For Each item As GridDataItem In rgEmployeeList.SelectedItems
                    lstID.Add(CDec(item.GetDataKeyValue("ID")))
                Next

                If rep.DeleteWorkingBefore(lstID) Then
                    ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                    rgEmployeeList.Rebind()
                Else
                    ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
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


End Class