Imports System.Globalization
Imports System.IO
Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDeclaresOT
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Properties"
    Dim store As New AttendanceStoreProcedure
    Dim thr As Threading.Thread
    Public clrConverter As New System.Drawing.ColorConverter
    Public Property AjaxManagerId As String
    ''' <summary>
    ''' Obj isSuccess
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isSuccess As Boolean
        Get
            Return ViewState(Me.ID & "_isSuccess")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isSuccess") = value
        End Set
    End Property
    Private Property REGISTER_OT As List(Of AT_OT_REGISTRATIONDTO)
        Get
            Return ViewState(Me.ID & "_REGISTER_OT")
        End Get
        Set(ByVal value As List(Of AT_OT_REGISTRATIONDTO))
            ViewState(Me.ID & "_REGISTER_OT") = value
        End Set
    End Property
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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
    Property dtItems As DataTable
        Get

            Return PageViewState(Me.ID & "_dtItems")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtItems") = value
        End Set
    End Property
    Property otFrTime As DateTime
        Get
            Return ViewState(Me.ID & "_otFrTime")
        End Get
        Set(value As DateTime)
            ViewState(Me.ID & "_otFrTime") = value
        End Set
    End Property
    Property otToTime As DateTime
        Get
            Return ViewState(Me.ID & "_otToTime")
        End Get
        Set(value As DateTime)
            ViewState(Me.ID & "_otToTime") = value
        End Set
    End Property
    Property hidTotalDayTT As Decimal?
        Get
            Return ViewState(Me.ID & "_hidTotalDayTT")
        End Get
        Set(value As Decimal?)
            ViewState(Me.ID & "_hidTotalDayTT") = value
        End Set
    End Property
    Property hidSignID As Decimal?
        Get
            Return ViewState(Me.ID & "_hidSignID")
        End Get
        Set(value As Decimal?)
            ViewState(Me.ID & "_hidSignID") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Load, khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgDeclaresOT)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgDeclaresOT.AllowCustomPaging = True

            rgDeclaresOT.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
            If Not IsPostBack Then
                GirdConfig(rgDeclaresOT)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            CType(MainToolBar.Items(4), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                rdtungay.SelectedDate = New Date(DateTime.Now.Year, DateTime.Now.Month, 1)
                rdDenngay.SelectedDate = LastDateOfMonth(DateTime.Now)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái của control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDeclaresOT.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDeclaresOT.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteRegisterOT(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.CurrentPageIndex = 0
                        rgDeclaresOT.MasterTableView.SortExpressions.Clear()
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgDeclaresOT.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event ctrlOrganization SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgDeclaresOT.CurrentPageIndex = 0
            rgDeclaresOT.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            'Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
            '                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
            '                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgDeclaresOT.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    Dim dtVariable As New DataTable
                    Dim tempPath = "~/ReportTemplates//Attendance//Import//Template_LamNgoaiGio.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ShowMessage(Translate("Không tồn tại file template"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As DataSet
                    _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                                              .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    dsDanhMuc = rep.EXPORT_AT_OT_REGISTRATION(_param)

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "Import_Lamthemgio" & Format(Date.Now, "yyyyMMddHHmmss"), dsDanhMuc, Nothing, Response)
                    End Using

                Case "IMPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload1.AllowedExtensions = "xls,xlsx"
                    ctrlUpload1.Show()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDeclaresOT.ExportExcel(Server, Response, dtDatas, "DataEntitlement")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event OK popup import file mẫu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try


            Import_Data()

            '_myLog.WriteLog(_myLog._info, _classPath, method,
            '                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' Event OK popup sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_DeclareOT&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Cancel popup sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event Yes.No Messager hỏi xóa
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_OT_REGISTRATIONDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As New DataTable
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDeclaresOT, obj)
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue,
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgDeclaresOT.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate IsNot Nothing Then
                obj.REGIST_DATE_FROM = rdtungay.SelectedDate
            Else
                obj.REGIST_DATE_FROM = New Date(DateTime.Now.Year, DateTime.Now.Month, 1)
            End If

            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.REGIST_DATE_TO = rdDenngay.SelectedDate
            Else
                obj.REGIST_DATE_TO = LastDateOfMonth(DateTime.Now)
            End If

            obj.IS_TER = chkChecknghiViec.Checked

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    dtData = rep.GetRegData(obj, _param, MaximumRows, rgDeclaresOT.CurrentPageIndex, rgDeclaresOT.PageSize, "EMPLOYEE_CODE, REGIST_DATE desc")
                Else
                    dtData = rep.GetRegData(obj, _param, MaximumRows, rgDeclaresOT.CurrentPageIndex, rgDeclaresOT.PageSize)
                End If
            Else
                Return rep.GetRegData(obj, _param)
            End If
            rgDeclaresOT.VirtualItemCount = MaximumRows
            rgDeclaresOT.DataSource = dtData
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Reload, Refresh grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDeclaresOT.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdTungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgDeclaresOT.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgDeclaresOT_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgDeclaresOT.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgDeclaresOT.CurrentPageIndex = 0
                rgDeclaresOT.Rebind()
                If rgDeclaresOT.Items IsNot Nothing AndAlso rgDeclaresOT.Items.Count > 0 Then
                    rgDeclaresOT.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Import_Data()
        Try

            Dim rep As New AttendanceRepository
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

            Dim Countlogs = (
                        From s In dtLogs.AsEnumerable
                        Where s.Field(Of Decimal)("IS_NOT_ERROR") = 0
                        Select s
                      ).Count
            If Countlogs = 0 Then
                dtItems = (
                                From s In dtLogs.AsEnumerable
                                Where s.Field(Of Decimal)("IS_NOT_ERROR") <> 0
                                Select s
                              ).CopyToDataTable()
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If dtItems IsNot Nothing AndAlso dtItems.Rows.Count > 0 Then
                    dtItems.TableName = "ImportOT"
                    dtItems.WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_AT_OT_REGISTRATION(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgDeclaresOT.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                dtLogs.TableName = "DATA"
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_LamNgoaiGio_error')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function validateParam(ByVal stt As Decimal, ByVal emp_code As String, ByVal day As Date, ByVal is_off As Decimal,
                                  ByVal shift_start As Date, ByVal shift_end As Date,
                                  ByVal sh1 As String,
                                  ByVal sm1 As String,
                                  ByVal sh2 As String,
                                  ByVal sm2 As String,
                                  ByVal ch1 As String,
                                  ByVal cm1 As String,
                                  ByVal ch2 As String,
                                  ByVal cm2 As String) As String
        Try
            Dim err = ""
            Dim newRow = dtLogs.NewRow
            Dim shift_hour = shift_start.Hour
            Dim shift_mi = shift_start.Minute

            Dim tc_h = shift_start.Hour
            Dim tc_m = shift_start.Minute
            Dim sc_h = shift_end.Hour
            Dim sc_m = shift_end.Minute

            newRow("ROWNUM") = stt
            newRow("EMPLOYEE_CODE") = emp_code
            newRow("REGIST_DATE") = day.ToString("dd/MM/yyyy")

            newRow("FROM_AM") = sh1
            newRow("FROM_AM_MN") = sm1
            newRow("TO_AM") = sh2
            newRow("TO_AM_MN") = sm2


            newRow("FROM_PM") = ch1
            newRow("FROM_PM_MN") = cm1
            newRow("TO_PM") = ch2
            newRow("TO_PM_MN") = cm2

            newRow("IS_NOT_ERROR") = CDec(Val(True))

            newRow("DISCIPTION") &= ""


            'sh1:sm1 - sh2:sm2  /  tc_h:tc_m   sc_h:sc_m  /  ch1:cm1 - ch2:cm2


            '---check khung gio dki hop le --case 24h
            If CDec(Val(sh1)) = 24 Or CDec(Val(ch1)) = 24 Or (CDec(Val(sh2)) = 24 And CDec(Val(sm2)) > 0) _
                            Or (CDec(Val(ch2)) = 24 And CDec(Val(cm2)) > 0) Then
                err &= "Khung giờ đăng ký không hợp lệ (vượt khung 24h),"
            End If



            '--check tu gio > den gio
            If sh2.ToString <> "" Then
                If CDec(Val(sh1)) > CDec(Val(sh2)) Or (CDec(Val(sh1)) = CDec(Val(sh2)) And CDec(Val(sm1)) > CDec(Val(sm2))) Then
                    err &= "Khung giờ đăng ký không hợp lệ (từ giờ lớn hơn đến giờ),"

                End If
            End If
            If ch2.ToString <> "" Then
                If CDec(Val(ch1)) > CDec(Val(ch2)) Or (CDec(Val(ch1)) = CDec(Val(ch2)) And CDec(Val(cm1)) > CDec(Val(cm2))) Then
                    err &= "Khung giờ đăng ký không hợp lệ (từ giờ lớn hơn đến giờ),"

                End If
            End If

            '--check khung gio dki OT (L,OFF,NBL,..)

            Dim s_hour = shift_hour
            Dim s_mi = shift_mi

            If sh2.ToString <> "" Then
                If is_off = -1 Then
                    If CDec(Val(sh2)) > s_hour Or (CDec(Val(sh2)) = s_hour And CDec(Val(sm2)) > s_mi) Then
                        err &= "Đăng ký OT trước ca từ 00:00 - " + s_hour.ToString("00") + ":" + s_mi.ToString("00") + ", "

                    End If
                Else
                    If CDec(Val(sh2)) > CDec(Val(tc_h)) Or (CDec(Val(sh2)) = CDec(Val(tc_h)) And CDec(Val(sm2)) > CDec(Val(tc_m))) Then
                        err &= "Giờ đăng ký trước ca/sau ca không hợp lệ,"

                    End If
                End If
            End If

            If ch1.ToString <> "" Then
                If is_off = -1 Then
                    If CDec(Val(ch1)) < s_hour Or (CDec(Val(ch1)) = s_hour And CDec(Val(sm2)) < s_mi) Then
                        err &= "Đăng ký OT sau ca từ " + s_hour.ToString("00") + ":" + s_mi.ToString("00") + " - 24:00, "

                    End If
                Else
                    If CDec(Val(ch1)) < CDec(Val(sc_h)) Or (CDec(Val(ch1)) = CDec(Val(sc_h)) And CDec(Val(cm1)) < CDec(Val(sc_m))) Then
                        err &= "Giờ đăng ký trước ca/sau ca không hợp lệ,"

                    End If
                End If
            End If

            Return err
        Catch ex As Exception

        End Try
    End Function

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệudt

            dtTemp.Columns(0).ColumnName = "ROWNUM"
            dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
            dtTemp.Columns(2).ColumnName = "FULLNAME_VN"
            dtTemp.Columns(3).ColumnName = "REGIST_DATE"

            dtTemp.Columns(4).ColumnName = "FROM_AM"
            dtTemp.Columns(5).ColumnName = "FROM_AM_MN"
            dtTemp.Columns(6).ColumnName = "TO_AM"
            dtTemp.Columns(7).ColumnName = "TO_AM_MN"

            dtTemp.Columns(8).ColumnName = "FROM_PM"
            dtTemp.Columns(9).ColumnName = "FROM_PM_MN"
            'dtTemp.Columns(10).ColumnName = "CHECK_FROMPM_NEXTDAY_NAME"

            dtTemp.Columns(10).ColumnName = "TO_PM"
            dtTemp.Columns(11).ColumnName = "TO_PM_MN"
            'dtTemp.Columns(13).ColumnName = "CHECK_TOPM_NEXTDAY_NAME"
            dtTemp.Columns(12).ColumnName = "OT_TYPE_ID_NAME"
            dtTemp.Columns(13).ColumnName = "HS_OT_NAME"
            dtTemp.Columns(14).ColumnName = "REASON_OT"
            'dtTemp.Columns(17).ColumnName = "ORG_CODE"
            'dtTemp.Columns(18).ColumnName = "ORG_NAME"
            dtTemp.Columns(15).ColumnName = "OT_TYPE_ID"
            dtTemp.Columns(16).ColumnName = "HS_OT"
            'dtTemp.Columns(21).ColumnName = "ORG_ID"
            'dtTemp.Columns(19).ColumnName = "CHECK_FROMPM_NEXTDAY"
            'dtTemp.Columns(20).ColumnName = "CHECK_TOPM_NEXTDAY"


            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.AcceptChanges()

            'Bảng lưu Error log
            Dim _error As Boolean = True
            Dim count As Integer = 0
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "Logs"

            dtLogs.Columns.Add("DISCIPTION", GetType(String))

            dtLogs.Columns.Add("IS_DELETED", GetType(Decimal))
            dtLogs.Columns.Add("SIGN_ID", GetType(Decimal))
            dtLogs.Columns.Add("SIGN_CODE", GetType(String))
            dtLogs.Columns.Add("STATUS", GetType(Decimal))
            dtLogs.Columns.Add("IS_PASS_DAY", GetType(Decimal))
            dtLogs.Columns.Add("HOURS_TOTAL_AM", GetType(Decimal))
            dtLogs.Columns.Add("HOURS_TOTAL_PM", GetType(Decimal))
            dtLogs.Columns.Add("HOURS_TOTAL_DAY", GetType(Decimal))
            dtLogs.Columns.Add("HOURS_TOTAL_NIGHT", GetType(Decimal))
            dtLogs.Columns.Add("DK_PORTAL", GetType(Decimal))
            dtLogs.Columns.Add("CREATED_BY_EMP", GetType(String))
            dtLogs.Columns.Add("BY_ANOTHER", GetType(Decimal))
            dtLogs.Columns.Add("TOTAL_DAY_TT", GetType(Decimal))
            dtLogs.Columns.Add("TOTAL_OT", GetType(Decimal))
            dtLogs.Columns.Add("IS_NOT_ERROR", GetType(Decimal))
            dtLogs.Clear()

            For Each rows As DataRow In dtTemp.Rows
                Dim rep As New AttendanceRepository
                Dim EmployeeShift As New EMPLOYEE_SHIFT_DTO
                hidSignID = 0
                'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE       
                If IsDBNull(rows("EMPLOYEE_CODE")) Then
                    rows.Delete()
                End If
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
                'Bắt đầu validate
                Dim rdHours_Start
                Dim rdHours_Stop
                Dim periodid As Decimal
                Dim calOTAM As Boolean = False
                Dim calOTPM As Boolean = False
                count = count + 1
                newRow = dtLogs.NewRow
                newRow("ROWNUM") = count
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("IS_NOT_ERROR") = 0

                newRow("REGIST_DATE") = rows("REGIST_DATE")

                newRow("FROM_AM") = rows("FROM_AM")
                newRow("FROM_AM_MN") = rows("FROM_AM_MN")
                newRow("TO_AM") = rows("TO_AM")
                newRow("TO_AM_MN") = rows("TO_AM_MN")


                newRow("FROM_PM") = rows("FROM_PM")
                newRow("FROM_PM_MN") = rows("FROM_PM_MN")
                newRow("TO_PM") = rows("TO_PM")
                newRow("TO_PM_MN") = rows("TO_PM_MN")



                If IsDBNull(rows("REGIST_DATE")) Then
                    newRow("REGIST_DATE") = rows("REGIST_DATE")
                    newRow("DISCIPTION") &= "Chưa nhập Ngày đăng ký,"
                    _error = False
                Else
                    Dim dateReg As Date
                    If CheckDate(rows("REGIST_DATE"), dateReg) = False Then
                        newRow("REGIST_DATE") = rows("REGIST_DATE")
                        newRow("DISCIPTION") &= "Nhập sai định dạng Ngày đăng ký,"
                        _error = False
                    Else

                        Dim empID = rep.GetEmployeeID(rows("EMPLOYEE_CODE"), dateReg)
                        ' Nhân viên k có trong hệ thống
                        If empID.Rows.Count <= 0 Then
                            rows.Delete()
                            rep.Dispose()
                            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
                            'newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                            'newRow("DISCIPTION") &= "Mã nhân viên - Không tồn tại,"
                            '_error = False
                        Else
                            newRow("REGIST_DATE") = rows("REGIST_DATE")
                            EmployeeShift = rep.GetEmployeeShifts(empID.Rows(0)("ID"), dateReg, dateReg).FirstOrDefault
                            If EmployeeShift IsNot Nothing Then
                                hidSignID = EmployeeShift.ID_SIGN
                                'If EmployeeShift.SIGN_CODE = "OFF" Then 
                                '    newRow("DISCIPTION") &= "Ngày đăng ký đang là ca OFF,"
                                '    _error = True
                                '    dtLogs.Rows.Add(newRow)
                                '    rep.Dispose()
                                '    Continue For
                                'End If
                            End If
                            If hidSignID <= 0 Then 'chua dang ky ca
                                'Dim lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(dateReg, dateReg)
                                'If lstdtHoliday.Rows.Count <= 0 Then 'Ko phai ngay le
                                newRow("DISCIPTION") &= "Nhân viên chưa được gán ca,"
                                _error = True
                                dtLogs.Rows.Add(newRow)
                                rep.Dispose()
                                Continue For
                                'End If
                            End If

                            If Not IsNumeric(rows("OT_TYPE_ID")) Then
                                newRow("OT_TYPE_ID") = rows("OT_TYPE_ID")
                                newRow("DISCIPTION") &= "Chưa nhập Loại làm thêm,"
                                _error = False
                            End If
                            If Not IsNumeric(rows("HS_OT")) Then
                                newRow("HS_OT") = rows("HS_OT")
                                newRow("DISCIPTION") &= "Chưa nhập hệ số làm thêm,"
                                _error = False
                            End If
                            If IsDBNull(rows("REASON_OT")) Then
                                newRow("REASON_OT") = rows("REASON_OT")
                                newRow("DISCIPTION") &= "Chưa nhập Lý do làm thêm,"
                                _error = False
                            End If

                            periodid = rep.GetperiodID_2(empID.Rows(0)("ID"), dateReg)
                            If periodid = 0 Then
                                'ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                                'Exit Sub
                            ElseIf periodid = -1 Then
                                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                                newRow("DISCIPTION") &= "Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công,"
                                _error = False
                            ElseIf periodid = -2 Then
                                newRow("DISCIPTION") &= "Nhân viên chưa được thiết lập đối tượng nhân viên,"
                                _error = False
                            End If
                            Dim checkKicong = rep.CHECK_PERIOD_CLOSE1(periodid, empID.Rows(0)("ID"))
                            If checkKicong = 0 Then
                                newRow("DISCIPTION") &= "Kì công đã đóng. Vui lòng kiểm tra lại,"
                                _error = False
                            End If

                            Dim shift_Start As Date = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, EmployeeShift.HOURS_START.Value.Hour, EmployeeShift.HOURS_START.Value.Minute, 0)
                            Dim shift_Stop As Date = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, EmployeeShift.HOURS_END.Value.Hour, EmployeeShift.HOURS_END.Value.Minute, 0)
                            If EmployeeShift.IS_TOMORROW Then
                                shift_Stop = shift_Stop.AddDays(1)
                            End If

                            Dim er = 0
                            If Not IsDBNull(rows("FROM_AM")) And Not IsDBNull(rows("TO_AM")) Then
                                Dim re = validateParam(count, rows("EMPLOYEE_CODE"), rows("REGIST_DATE"), shift_Start = shift_Stop,
                                      EmployeeShift.HOURS_START, EmployeeShift.HOURS_END,
                                      rows("FROM_AM").ToString,
                                      rows("FROM_AM_MN").ToString,
                                      rows("TO_AM").ToString,
                                      rows("TO_AM_MN").ToString,
                                      "",
                                      "",
                                      "",
                                      "")

                                If re.Length > 0 Then
                                    newRow("IS_NOT_ERROR") = CDec(Val(False))
                                    newRow("DISCIPTION") &= re

                                    er = 1
                                End If

                                'If validateParam(count, rows("EMPLOYEE_CODE"), rows("REGIST_DATE"), shift_Start = shift_Stop,
                                '      EmployeeShift.HOURS_START, EmployeeShift.HOURS_END,
                                '      CDec(Val(rows("FROM_AM").ToString)),
                                '      CDec(Val(rows("FROM_AM_MN").ToString)),
                                '      CDec(Val(rows("TO_AM").ToString)),
                                '      CDec(Val(rows("TO_AM_MN").ToString)),
                                '      CDec(Val(rows("FROM_PM").ToString)),
                                '     CDec(Val(rows("FROM_PM_MN").ToString)),
                                '     CDec(Val(rows("TO_PM").ToString)),
                                '     CDec(Val(rows("TO_PM_MN").ToString))) Then

                                '    _error = True
                                '    rep.Dispose()
                                '    Continue For
                                'End If
                            End If

                            If Not IsDBNull(rows("FROM_PM")) And Not IsDBNull(rows("TO_PM")) Then
                                Dim re = validateParam(count, rows("EMPLOYEE_CODE"), rows("REGIST_DATE"), shift_Start = shift_Stop,
                                      EmployeeShift.HOURS_START, EmployeeShift.HOURS_END,
                                      "",
                                      "",
                                      "",
                                      "",
                                      rows("FROM_PM").ToString,
                                      rows("FROM_PM_MN").ToString,
                                      rows("TO_PM").ToString,
                                      rows("TO_PM_MN").ToString)

                                If re.Length > 0 Then
                                    newRow("IS_NOT_ERROR") = CDec(Val(False))
                                    newRow("DISCIPTION") &= re

                                    er = 1
                                End If
                                'If validateParam(count, rows("EMPLOYEE_CODE"), rows("REGIST_DATE"), shift_Start = shift_Stop,
                                '      EmployeeShift.HOURS_START, EmployeeShift.HOURS_END,
                                '      rows("FROM_AM").ToString,
                                '      rows("FROM_AM_MN").ToString,
                                '      rows("TO_AM").ToString,
                                '      rows("TO_AM_MN").ToString,
                                '      rows("FROM_PM").ToString,
                                '      rows("FROM_PM_MN").ToString,
                                '      rows("TO_PM").ToString,
                                '      rows("TO_PM_MN").ToString) Then

                                '    _error = True
                                '    rep.Dispose()
                                '    Continue For
                                'End If
                            End If

                            If er = 1 Then
                                dtLogs.Rows.Add(newRow)
                                _error = False
                                rep.Dispose()
                                Continue For
                            Else
                                er = 0
                            End If




                            If (IsDBNull(rows("FROM_AM")) And Not IsDBNull(rows("TO_AM"))) And (Not IsDBNull(rows("FROM_AM")) And IsDBNull(rows("TO_AM"))) Then
                                If IsDBNull(rows("FROM_AM")) Then
                                    newRow("DISCIPTION") &= "Phải nhập Từ giờ làm thêm AM,"
                                    _error = False
                                Else
                                    If IsDBNull(rows("FROM_AM_MN")) Then
                                        newRow("DISCIPTION") &= "Vui lòng nhập Từ phút AM,"
                                        _error = False
                                    End If
                                End If

                                If IsDBNull(rows("TO_AM")) Then
                                    newRow("TO_AM") = rows("TO_AM")
                                    newRow("DISCIPTION") &= "Phải nhập Đến giờ làm thêm AM,"
                                    _error = False
                                Else
                                    If IsDBNull(rows("TO_AM_MN")) Then
                                        newRow("TO_AM_MN") = rows("TO_AM_MN")
                                        newRow("DISCIPTION") &= "Vui lòng nhập Đến phút AM,"
                                        _error = False
                                    End If
                                End If
                            ElseIf Not IsDBNull(rows("FROM_AM")) And Not IsDBNull(rows("TO_AM")) Then
                                calOTAM = True
                                If rows("FROM_AM") >= rows("TO_AM") And rows("FROM_AM_MN") >= rows("TO_AM_MN") Then
                                    newRow("FROM_AM") = rows("FROM_AM")
                                    newRow("FROM_AM_MN") = rows("FROM_AM_MN")
                                    newRow("TO_AM") = rows("TO_AM")
                                    newRow("TO_AM_MN") = rows("TO_AM_MN")
                                    newRow("DISCIPTION") &= "Nhập giờ AM: Từ lớn hơn Đến giờ,"
                                    _error = False
                                End If



                                If IsNumeric(hidSignID) Then
                                    If hidSignID <> 1 And hidSignID <> 2 Then
                                        Dim FromHour As Date = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, Decimal.Parse(rows("FROM_AM")), Decimal.Parse(rows("FROM_AM_MN")), 0)
                                        Dim ToHour As Date = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, Decimal.Parse(rows("TO_AM")), Decimal.Parse(rows("TO_AM_MN")), 0)
                                        If (FromHour >= shift_Start AndAlso FromHour <= shift_Stop) OrElse
                                            (ToHour > shift_Start AndAlso ToHour <= shift_Stop) OrElse
                                            (shift_Start >= FromHour AndAlso shift_Start < ToHour) OrElse
                                            (shift_Stop >= FromHour AndAlso shift_Stop <= ToHour) Then
                                            newRow("FROM_AM") = rows("FROM_AM")
                                            newRow("FROM_AM_MN") = rows("FROM_AM_MN")
                                            newRow("TO_AM") = rows("TO_AM")
                                            newRow("TO_AM_MN") = rows("TO_AM_MN")
                                            newRow("DISCIPTION") &= "Giờ đăng ký OT AM giao với giờ làm việc hành chính,"
                                            _error = False
                                        End If
                                    End If
                                End If
                            End If

                            If (IsDBNull(rows("FROM_PM")) And Not IsDBNull(rows("TO_PM"))) And (Not IsDBNull(rows("FROM_PM")) And IsDBNull(rows("TO_PM"))) Then
                                If IsDBNull(rows("FROM_PM")) Then
                                    newRow("FROM_PM") = rows("FROM_PM")
                                    newRow("DISCIPTION") &= "Phải nhập Từ giờ làm thêm PM,"
                                    _error = False
                                Else
                                    If IsDBNull(rows("FROM_PM_MN")) Then
                                        newRow("FROM_PM_MN") = rows("FROM_PM_MN")
                                        newRow("DISCIPTION") &= "Vui lòng nhập Từ phút PM,"
                                        _error = False
                                    End If
                                End If

                                If IsDBNull(rows("TO_PM")) Then
                                    newRow("TO_PM") = rows("TO_PM")
                                    newRow("DISCIPTION") &= "Phải nhập Đến giờ làm thêm PM,"
                                    _error = False
                                Else
                                    If IsDBNull(rows("TO_PM_MN")) Then
                                        newRow("TO_PM_MN") = rows("TO_PM_MN")
                                        newRow("DISCIPTION") &= "Vui lòng nhập Đến phút PM,"
                                        _error = False
                                    End If
                                End If
                            ElseIf Not IsDBNull(rows("FROM_PM")) And Not IsDBNull(rows("TO_PM")) Then

                                calOTPM = True
                                Dim hour_frPM As String = Format(Decimal.Parse(rows("FROM_PM")), "00")
                                Dim minute_frPM As String = Format(Decimal.Parse(rows("FROM_PM_MN")), "00")
                                Dim hour_toPM As String = Format(Decimal.Parse(rows("TO_PM")), "00")
                                Dim minute_toPM As String = Format(Decimal.Parse(rows("TO_PM_MN")), "00")

                                'Nếu có check qua ngày hôm sau: chỉ cho nhập từ 0 - 12
                                '' Từ giờ PM
                                'If Not IsDBNull(rows("CHECK_FROMPM_NEXTDAY")) AndAlso rows("CHECK_FROMPM_NEXTDAY") = 1 Then
                                '    If hour_frPM <> "" AndAlso (hour_frPM < 0 Or hour_frPM > 12) Then
                                '        newRow("FROM_PM") = rows("FROM_PM")
                                '        newRow("DISCIPTION") &= "chỉ cho nhập Từ giờ PM từ 0 - 12 giờ,"
                                '        _error = False
                                '    End If
                                'Else
                                '    'Nếu không check qua ngày hôm sau: chỉ cho nhập từ 12 - 24
                                '    If hour_frPM <> "" AndAlso hour_frPM < 12 Then
                                '        newRow("FROM_PM") = rows("FROM_PM")
                                '        newRow("DISCIPTION") &= "chỉ cho nhập Từ giờ PM từ 12 - 24 giờ,"
                                '        _error = False
                                '    End If
                                'End If


                                'Nếu có check qua ngày hôm sau: chỉ cho nhập từ 0 - 12
                                '' Đến giờ PM
                                'If Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                '    If hour_toPM <> "" AndAlso (hour_toPM < 0 Or hour_toPM > 12) Then
                                '        newRow("TO_PM") = rows("TO_PM")
                                '        newRow("DISCIPTION") &= "chỉ cho nhập Đến giờ PM từ 0 - 12 giờ,"
                                '        _error = False
                                '    End If
                                'Else
                                '    'Nếu không check qua ngày hôm sau: chỉ cho nhập từ 12 - 24
                                '    If hour_toPM <> "" AndAlso hour_toPM < 12 Then
                                '        newRow("TO_PM") = rows("TO_PM")
                                '        newRow("DISCIPTION") &= "chỉ cho nhập Đến giờ PM từ 12 - 24 giờ,"
                                '        _error = False

                                '    End If
                                'End If

                                'If Not IsDBNull(rows("CHECK_FROMPM_NEXTDAY")) AndAlso Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_FROMPM_NEXTDAY") = 1 AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                '    If hour_frPM <> "" AndAlso hour_toPM <> "" AndAlso hour_frPM > hour_toPM Then
                                '        newRow("FROM_PM") = rows("FROM_PM")
                                '        newRow("TO_PM") = rows("TO_PM")
                                '        newRow("DISCIPTION") &= "Từ giờ PM phải nhỏ hơn Đến giờ PM,"
                                '        _error = False
                                '    End If
                                'Else
                                'If Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                '        If hour_frPM < hour_toPM Then
                                '            newRow("FROM_PM") = rows("FROM_PM")
                                '            newRow("TO_PM") = rows("TO_PM")
                                '            newRow("DISCIPTION") &= "Từ giờ PM phải lớn hơn Đến giờ PM,"
                                '            _error = False
                                '        End If
                                '    Else
                                If hour_frPM > hour_toPM Then
                                    newRow("FROM_PM") = rows("FROM_PM")
                                    newRow("TO_PM") = rows("TO_PM")
                                    newRow("DISCIPTION") &= "Từ giờ PM phải nhỏ hơn Đến giờ PM,"
                                    _error = False
                                End If
                                '    End If
                                'End If

                                If IsNumeric(hidSignID) Then
                                    If hidSignID <> 1 And hidSignID <> 2 Then
                                        Dim FromHour As Date
                                        Dim ToHour As Date
                                        FromHour = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, hour_frPM, minute_frPM, 0)
                                        If hour_toPM = "24" Then
                                            'Lấy ngày đăng ký + 1
                                            Dim day = dateReg.AddDays(1)
                                            ToHour = New DateTime(day.Year, day.Month, day.Day, 0, 0, 0)
                                        Else
                                            ToHour = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, hour_toPM, minute_toPM, 0)
                                        End If


                                        If (FromHour >= shift_Start AndAlso FromHour < shift_Stop) OrElse
                                            (ToHour >= shift_Start AndAlso ToHour <= shift_Stop) OrElse
                                            (shift_Start >= FromHour AndAlso shift_Stop <= ToHour) OrElse
                                            (shift_Stop > FromHour AndAlso shift_Stop <= ToHour) Then
                                            newRow("FROM_PM") = rows("FROM_PM")
                                            newRow("FROM_PM_MN") = rows("FROM_PM_MN")
                                            newRow("TO_PM") = rows("TO_PM")
                                            newRow("TO_PM_MN") = rows("TO_PM_MN")
                                            newRow("DISCIPTION") &= "Giờ đăng ký OT PM giao với giờ làm việc hành chính,"
                                            _error = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        If _error Then
                            Dim dtData As New DataTable
                            Dim obj As New AT_OT_REGISTRATIONDTO
                            Dim totalHour As New TimeSpan
                            Dim fromAM As Decimal = 0.0
                            Dim fromMNAM As Decimal = 0.0
                            Dim toAM As Decimal = 0.0
                            Dim toMNAM As Decimal = 0.0
                            Dim fromPM As Decimal = 0.0
                            Dim fromMNPM As Decimal = 0.0
                            Dim toPM As Decimal = 0.0
                            Dim toMNPM As Decimal = 0.0
                            Dim hidTotal As Decimal = 0.0
                            Dim hidHourTotalNight As Decimal = 0.0
                            Dim hidHourTotalDay As Decimal = 0.0
                            Dim hidTimeCOEff_S As Decimal = 0.0
                            Dim hidTimeCOEff_E As Decimal = 0.0

                            Dim totalFromAM As New DateTime
                            Dim totalToAM As New DateTime
                            Dim totalFromPM As New DateTime
                            Dim totalToPM As New DateTime
                            Dim OTAM As New TimeSpan
                            Dim AM As New TimeSpan
                            Dim OTPM As New TimeSpan
                            Dim PM As New TimeSpan
                            Dim SA As New TimeSpan
                            Dim CH As New TimeSpan
                            Dim totalDayTT As New TimeSpan
                            Dim flagOT As Boolean = True

                            Try

                                dtData = store.GET_TIME_OT_COEFF_OVER(dateReg)
                                If dtData IsNot Nothing Then
                                    otFrTime = dtData.Rows(0)("FROMDATE_NIGHTHOUR_F")
                                    otToTime = dtData.Rows(0)("TODATE_NIGHTHOUR_F")
                                Else
                                    newRow("DISCIPTION") &= "Chưa thiết lập hệ số OT,"
                                    _error = True
                                    dtLogs.Rows.Add(newRow)
                                    rep.Dispose()
                                    Continue For
                                End If
                                If calOTAM Then

                                    'AM
                                    fromAM = IIf(IsNumeric(rows("FROM_AM")), Decimal.Parse(rows("FROM_AM")), 0.0)
                                    fromMNAM = Decimal.Parse(If(IsNumeric(rows("FROM_AM_MN")), Decimal.Parse(rows("FROM_AM_MN")), 0))
                                    toAM = IIf(IsNumeric(rows("TO_AM")), Decimal.Parse(rows("TO_AM")), 0.0)
                                    toMNAM = Decimal.Parse(If(IsNumeric(rows("TO_AM_MN")), Decimal.Parse(rows("TO_AM_MN")), 0))

                                    totalFromAM = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, fromAM, fromMNAM, 0)
                                    totalToAM = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, toAM, toMNAM, 0)

                                    ''If (totalFromAM >= otFrTime.AddDays(-1) And totalFromAM <= otToTime.AddDays(-1)) AndAlso totalToAM >= otToTime.AddDays(-1) Then
                                    If totalFromAM <= otToTime.AddDays(-1) AndAlso totalToAM >= otToTime.AddDays(-1) Then
                                        'If totalToAM >= otToTime.AddDays(-1) Then
                                        '    OTAM += totalToAM - otToTime.AddDays(-1)
                                        '    CH += otToTime.AddDays(-1) - totalFromAM
                                        'Else
                                        '    OTPM += totalToAM - totalFromAM
                                        'End If
                                        OTAM += totalToAM - otToTime.AddDays(-1)
                                        CH += otToTime.AddDays(-1) - totalFromAM
                                    ElseIf (totalToAM >= otFrTime.AddDays(-1) And totalToAM <= otToTime.AddDays(-1)) Then
                                        If totalFromAM <= otFrTime.AddDays(-1) Then
                                            CH += totalToAM - otFrTime.AddDays(-1)
                                            OTAM += otFrTime.AddDays(-1) - totalFromAM
                                        Else
                                            'OTPM += totalToAM - totalFromAM ?? kho hieu??
                                            OTAM += totalToAM - totalFromAM
                                        End If
                                    Else
                                        flagOT = False
                                        OTAM = totalToAM - totalFromAM
                                    End If

                                    OTAM = totalToAM - totalFromAM '??
                                End If
                                If calOTPM Then

                                    'PM
                                    fromPM = IIf(IsNumeric(rows("FROM_PM")), Decimal.Parse(rows("FROM_PM")), 0.0)
                                    fromMNPM = Decimal.Parse(If(IsNumeric(rows("FROM_PM_MN")), Decimal.Parse(rows("FROM_PM_MN")), 0))
                                    toPM = IIf(IsNumeric(rows("TO_PM")), Decimal.Parse(rows("TO_PM")), 0.0)
                                    toMNPM = Decimal.Parse(If(IsNumeric(rows("TO_PM_MN")), Decimal.Parse(rows("TO_PM_MN")), 0))

                                    totalFromPM = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, fromPM, fromMNPM, 0)
                                    If toPM = 24 Then
                                        'Lấy ngày đăng ký + 1
                                        Dim day = dateReg.AddDays(1)
                                        totalToPM = New DateTime(day.Year, day.Month, day.Day, 0, 0, 0)
                                    Else
                                        totalToPM = New DateTime(dateReg.Year, dateReg.Month, dateReg.Day, toPM, toMNPM, 0)
                                    End If


                                    'If Not IsDBNull(rows("CHECK_FROMPM_NEXTDAY")) AndAlso rows("CHECK_FROMPM_NEXTDAY") = 1 Then
                                    '    If Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                    '        totalFromPM = totalFromPM.AddDays(1)
                                    '        totalToPM = totalToPM.AddDays(1)
                                    '    End If
                                    'Else
                                    '    If Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                    '        totalFromPM = totalFromPM
                                    '        totalToPM = totalToPM.AddDays(1)
                                    '    Else
                                    totalFromPM = totalFromPM
                                    totalToPM = totalToPM
                                    '    End If
                                    'End If

                                    If (totalFromPM >= otFrTime And totalFromPM <= otToTime) Then
                                        If totalToPM >= otToTime Then
                                            SA += totalToPM - otToTime
                                            OTPM += otToTime - totalFromPM
                                        Else
                                            OTPM += totalToPM - totalFromPM
                                        End If
                                    ElseIf (totalToPM >= otFrTime And totalToPM <= otToTime) Then
                                        If totalFromPM <= otFrTime Then
                                            OTPM += totalToPM - otFrTime
                                            SA += otFrTime - totalFromPM
                                        Else
                                            OTPM += totalToPM - totalFromPM
                                        End If
                                    Else
                                        flagOT = False
                                        OTPM = totalToPM - totalFromPM
                                    End If

                                    OTPM = totalToPM - totalFromPM '???

                                End If
                                If flagOT Then
                                    totalHour = OTAM + OTPM '+ SA + CH

                                    hidTotal = Decimal.Parse(totalHour.Hours.ToString) + CDec(If(totalHour.Minutes.ToString.Length < 2, "0" + totalHour.Minutes.ToString, totalHour.Minutes.ToString)) / 60

                                    hidHourTotalNight = Decimal.Parse((OTPM + CH).Hours.ToString + "," + If((OTPM + CH).Minutes.ToString.Length < 2, "0" + (OTPM + CH).Minutes.ToString, (OTPM + CH).Minutes.ToString))
                                    hidHourTotalDay = Decimal.Parse((OTAM + SA).Hours.ToString + "," + If((OTAM + SA).Minutes.ToString.Length < 2, "0" + (OTAM + SA).Minutes.ToString, (OTAM + SA).Minutes.ToString))

                                    totalDayTT = totalHour - (OTPM + CH)
                                    hidTotalDayTT = Decimal.Parse(totalDayTT.Hours.ToString + "," + If(totalDayTT.Minutes.ToString.Length < 2, "0" + totalDayTT.Minutes.ToString, totalDayTT.Minutes.ToString))
                                Else
                                    totalHour = OTAM + OTPM
                                    hidHourTotalDay = Decimal.Parse((OTAM + OTPM).Hours.ToString + "," + If((OTAM + OTPM).Minutes.ToString.Length < 2, "0" + (OTAM + OTPM).Minutes.ToString, (OTAM + OTPM).Minutes.ToString))
                                    hidTotalDayTT = hidHourTotalDay
                                    hidTotal = Decimal.Parse(totalHour.Hours.ToString) + CDec(If(totalHour.Minutes.ToString.Length < 2, "0" + totalHour.Minutes.ToString, totalHour.Minutes.ToString)) / 60
                                End If

                                hidTimeCOEff_S = Decimal.Parse((totalToAM - totalFromAM).Hours.ToString + "," + If((totalToAM - totalFromAM).Minutes.ToString.Length < 2, "0" + (totalToAM - totalFromAM).Minutes.ToString, (totalToAM - totalFromAM).Minutes.ToString))
                                hidTimeCOEff_E = Decimal.Parse((totalToPM - totalFromPM).Hours.ToString + "," + If((totalToPM - totalFromPM).Minutes.ToString.Length < 2, "0" + (totalToPM - totalFromPM).Minutes.ToString, (totalToPM - totalFromPM).Minutes.ToString))

                                obj.TOTAL_OT = Math.Round(ObjToDecima(hidTotal, 0), 2)

                                Dim checkMaxOt As Decimal = store.GET_VALUE_OT_MONTH(empID.Rows(0)("ID"), periodid, dateReg, obj.TOTAL_OT)

                                If checkMaxOt > 0 Then

                                    newRow("FROM_AM") = fromAM
                                    newRow("FROM_AM_MN") = fromMNAM
                                    newRow("TO_AM") = toAM
                                    newRow("TO_AM_MN") = toMNAM

                                    newRow("FROM_PM") = fromPM
                                    newRow("FROM_PM_MN") = fromMNPM
                                    'newRow("CHECK_FROMPM_NEXTDAY_NAME") = rows("CHECK_FROMPM_NEXTDAY_NAME")
                                    newRow("TO_PM") = toPM
                                    newRow("TO_PM_MN") = toMNPM
                                    'newRow("CHECK_TOPM_NEXTDAY_NAME") = rows("CHECK_TOPM_NEXTDAY_NAME")

                                    newRow("DISCIPTION") &= String.Format("Tổng số giờ OT trong tháng đã vượt định mức {0} giờ", checkMaxOt)
                                    _error = False
                                End If

                                Dim checkMaxOtYear As Decimal = store.GET_VALUE_OT_YEAR(empID.Rows(0)("ID"), dateReg, obj.TOTAL_OT)

                                If checkMaxOtYear > 0 Then

                                    newRow("FROM_AM") = fromAM
                                    newRow("FROM_AM_MN") = fromMNAM
                                    newRow("TO_AM") = toAM
                                    newRow("TO_AM_MN") = toMNAM

                                    newRow("FROM_PM") = fromPM
                                    newRow("FROM_PM_MN") = fromMNPM
                                    'newRow("CHECK_FROMPM_NEXTDAY_NAME") = rows("CHECK_FROMPM_NEXTDAY_NAME")
                                    newRow("TO_PM") = toPM
                                    newRow("TO_PM_MN") = toMNPM
                                    'newRow("CHECK_TOPM_NEXTDAY_NAME") = rows("CHECK_TOPM_NEXTDAY_NAME")

                                    newRow("DISCIPTION") &= String.Format("Tổng số giờ OT trong tháng đã vượt định mức {0} giờ,", checkMaxOtYear)
                                    _error = False
                                End If

                                If obj.TOTAL_OT * 60 < EmployeeShift.OT_HOUR_MIN Then

                                    newRow("FROM_AM") = fromAM
                                    newRow("FROM_AM_MN") = fromMNAM
                                    newRow("TO_AM") = toAM
                                    newRow("TO_AM_MN") = toMNAM

                                    newRow("FROM_PM") = fromPM
                                    newRow("FROM_PM_MN") = fromMNPM
                                    'newRow("CHECK_FROMPM_NEXTDAY_NAME") = rows("CHECK_FROMPM_NEXTDAY_NAME")
                                    newRow("TO_PM") = toPM
                                    newRow("TO_PM_MN") = toMNPM
                                    'newRow("CHECK_TOPM_NEXTDAY_NAME") = rows("CHECK_TOPM_NEXTDAY_NAME")

                                    newRow("DISCIPTION") &= String.Format("Tổng số giờ OT phải lớn hơn hoặc bằng {0} phút,", EmployeeShift.OT_HOUR_MIN)
                                    _error = False
                                End If

                                If obj.TOTAL_OT * 60 > EmployeeShift.OT_HOUR_MAX Then

                                    newRow("FROM_AM") = fromAM
                                    newRow("FROM_AM_MN") = fromMNAM
                                    newRow("TO_AM") = toAM
                                    newRow("TO_AM_MN") = toMNAM

                                    newRow("FROM_PM") = fromPM
                                    newRow("FROM_PM_MN") = fromMNPM
                                    'newRow("CHECK_FROMPM_NEXTDAY_NAME") = rows("CHECK_FROMPM_NEXTDAY_NAME")
                                    newRow("TO_PM") = toPM
                                    newRow("TO_PM_MN") = toMNPM
                                    'newRow("CHECK_TOPM_NEXTDAY_NAME") = rows("CHECK_TOPM_NEXTDAY_NAME")

                                    newRow("DISCIPTION") &= String.Format("Tổng số giờ OT vượt quá số giờ OT cho phép. {0} phút,", EmployeeShift.OT_HOUR_MAX)
                                    _error = False
                                End If

                                obj.EMPLOYEE_ID = empID.Rows(0)("ID")
                                obj.REGIST_DATE = dateReg

                                obj.FROM_AM = fromAM
                                obj.FROM_AM_MN = fromMNAM
                                obj.TO_AM = toAM
                                obj.TO_AM_MN = toMNAM

                                obj.FROM_PM = fromPM
                                obj.FROM_PM_MN = fromMNPM
                                obj.TO_PM = toPM
                                obj.TO_PM_MN = toMNPM

                                Dim valid = rep.ValidateOtRegistration(obj)
                                If Not valid Then
                                    newRow("DISCIPTION") &= "Thời gian đăng ký bị trùng, vui lòng kiểm tra lại,"
                                    _error = False
                                End If
                                'Dim valid = rep.ValidateOtRegistration(obj)
                                'If valid Then
                                '    newRow("DISCIPTION") &= "Ngày OT đã được đăng ký, vui lòng chọn ngày khác,"
                                '    _error = False
                                'End If
                                '' hết phần Validate

                                If _error = False Then
                                    dtLogs.Rows.Add(newRow)
                                    _error = True
                                    rep.Dispose()
                                Else
                                    'lưu dữ liệu vào bảng
                                    newRow("IS_DELETED") = 0
                                    newRow("REASON_OT") = rows("REASON_OT")
                                    newRow("OT_TYPE_ID") = If(IsNumeric(rows("OT_TYPE_ID")), rows("OT_TYPE_ID"), 0)
                                    newRow("HS_OT") = If(IsNumeric(rows("HS_OT")), rows("HS_OT"), 0)
                                    'newRow("ORG_ID") = If(IsNumeric(rows("ORG_ID")), rows("ORG_ID"), 0)

                                    newRow("SIGN_ID") = EmployeeShift.ID_SIGN
                                    newRow("SIGN_CODE") = EmployeeShift.SIGN_CODE

                                    'If fromAM <> 0 Or toMNAM <> 0 Or fromAM <> 0 Or toMNAM <> 0 Then
                                    newRow("FROM_AM") = fromAM
                                    newRow("FROM_AM_MN") = fromMNAM
                                    newRow("TO_AM") = toAM
                                    newRow("TO_AM_MN") = toMNAM
                                    'End If

                                    'If fromPM <> 0 Or toMNPM <> 0 Or fromPM <> 0 Or toMNPM <> 0 Then
                                    newRow("FROM_PM") = fromPM
                                    newRow("FROM_PM_MN") = fromMNPM
                                    newRow("TO_PM") = toPM
                                    newRow("TO_PM_MN") = toMNPM
                                    'End If

                                    newRow("STATUS") = 1

                                    'MANG VÀO DB SELECT
                                    'Dim CODE As String = ""
                                    'If cbohs_ot.SelectedValue <> "" Then
                                    '    CODE = (From p In Me.lstHsOT Where p.ID = cbohs_ot.SelectedValue).FirstOrDefault.CODE
                                    'End If

                                    'If Not IsDBNull(rows("CHECK_FROMPM_NEXTDAY")) AndAlso rows("CHECK_FROMPM_NEXTDAY") = 1 Then
                                    '    newRow("CHECK_FROMPM_NEXTDAY") = rows("CHECK_FROMPM_NEXTDAY")
                                    'End If
                                    'If Not IsDBNull(rows("CHECK_TOPM_NEXTDAY")) AndAlso rows("CHECK_TOPM_NEXTDAY") = 1 Then
                                    '    newRow("CHECK_TOPM_NEXTDAY") = rows("CHECK_TOPM_NEXTDAY")
                                    'End If
                                    newRow("IS_PASS_DAY") = CDec(Val(EmployeeShift.IS_HOURS_STOP))
                                    newRow("HOURS_TOTAL_AM") = If(IsNumeric(hidTimeCOEff_S), hidTimeCOEff_S, 0)
                                    newRow("HOURS_TOTAL_PM") = If(IsNumeric(hidTimeCOEff_E), hidTimeCOEff_E, 0)
                                    newRow("HOURS_TOTAL_DAY") = If(IsNumeric(hidHourTotalDay), hidHourTotalDay, 0)

                                    'newRow("HOURS_TOTAL_NIGHT") = If(IsNumeric(hidHourTotalNight), hidHourTotalNight, 0)
                                    'newRow("TOTAL_DAY_TT") = If(IsNumeric(hidTotalDayTT), hidTotalDayTT, 0)

                                    newRow("HOURS_TOTAL_NIGHT") = store.COUNT_OT_NIGHT(obj.REGIST_DATE, obj.FROM_AM, obj.FROM_AM_MN, obj.TO_AM, obj.TO_AM_MN, obj.FROM_PM, obj.FROM_PM_MN, obj.TO_PM, obj.TO_PM_MN)
                                    newRow("TOTAL_DAY_TT") = Math.Round(ObjToDecima(hidTotal, 0), 2) - CDec(Val(newRow("HOURS_TOTAL_NIGHT")))
                                    newRow("TOTAL_DAY_TT") = Math.Round(newRow("TOTAL_DAY_TT"), 2)

                                    newRow("DK_PORTAL") = 0
                                    newRow("CREATED_BY_EMP") = LogHelper.CurrentUser.EMPLOYEE_ID
                                    newRow("BY_ANOTHER") = CDec(Val(False))
                                    newRow("TOTAL_OT") = Math.Round(ObjToDecima(hidTotal, 0), 2)
                                    newRow("IS_NOT_ERROR") = CDec(Val(True))
                                    dtLogs.Rows.Add(newRow)
                                    rep.Dispose()
                                End If

                            Catch ex As Exception
                                _error = True
                                rep.Dispose()
                                Continue For
                            End Try
                        Else
                            dtLogs.Rows.Add(newRow)
                            _error = True
                            rep.Dispose()
                        End If
                    End If
                End If
            Next
            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef dReg As Date) As Boolean
        Dim dateCheck As Boolean

        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, dReg)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub rgDeclaresOT_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDeclaresOT.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("DEPARTMENT_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("DEPARTMENT_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class