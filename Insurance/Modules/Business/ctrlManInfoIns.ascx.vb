Imports System.IO
Imports Aspose.Words
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlManInfoIns
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrlOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' AjaxManager
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents AjaxManager As RadAjaxManager

    ''' <summary>
    ''' AjaxManagerId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AjaxManagerId As String

    ''' <summary>
    ''' dtDataImp
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtDataImp As DataTable

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Business" + Me.GetType().Name.ToString()

#Region "Properties"

    ''' <summary>
    ''' INS_INFOMATION
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property INS_INFOMATION As List(Of INS_INFORMATIONDTO)
        Get
            Return ViewState(Me.ID & "_REGISTER_OT")
        End Get
        Set(ByVal value As List(Of INS_INFORMATIONDTO))
            ViewState(Me.ID & "_REGISTER_OT") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set
    End Property

    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME", GetType(String))
                dt.Columns.Add("SOBOOKNO", GetType(String))
                dt.Columns.Add("SOPRVDBOOKDAY", GetType(String))
                dt.Columns.Add("DAYPAYMENTCOMPANY", GetType(String))
                dt.Columns.Add("SOPRVDBOOKSTATUS_NAME", GetType(String))
                dt.Columns.Add("SOPRVDBOOKSTATUS", GetType(String))
                dt.Columns.Add("HEPRVDCARDSTATUS_NAME", GetType(String))
                dt.Columns.Add("HEPRVDCARDSTATUS", GetType(String))
                dt.Columns.Add("HECARDNO", GetType(String))
                dt.Columns.Add("HECARDEFFFROM", GetType(String))
                dt.Columns.Add("HECARDEFFTO", GetType(String))
                dt.Columns.Add("HEWHRREGISKEY_NAME", GetType(String))
                dt.Columns.Add("HEWHRREGISKEY", GetType(String))
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

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
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

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgManagerInfo)

            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID

            rgManagerInfo.AllowCustomPaging = True
            rgManagerInfo.ClientSettings.EnablePostBackOnRowClick = False

            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            ctrlUpload1.AllowedExtensions = "xls,xlsx"

            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
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
                                       ToolbarItem.Print,
                                       ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            CType(MainToolBar.Items(2), RadToolBarButton).Text = "Tờ khai BH"
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Xuất excel"

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New InsuranceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgManagerInfo.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgManagerInfo.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteINS_INFO(lstDeletes) Then
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

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceBusinessClient

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgManagerInfo.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgManagerInfo.CurrentPageIndex = 0
                        rgManagerInfo.MasterTableView.SortExpressions.Clear()
                        rgManagerInfo.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )

                    Case "Cancel"
                        rgManagerInfo.MasterTableView.ClearSelectedItems()
                End Select
            End If

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [chọn] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgManagerInfo.CurrentPageIndex = 0
            rgManagerInfo.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
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
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgManagerInfo.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgManagerInfo.ExportExcel(Server, Response, dtDatas, "Infomation_Ins")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case TOOLBARITEM_PRINT
                    Dim rep As New InsuranceRepository
                    Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
                    Dim dtData As DataTable
                    Dim lstID As String

                    If rgManagerInfo.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For intIndex As Int16 = 0 To rgManagerInfo.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgManagerInfo.SelectedItems(intIndex)
                        lstID = lstID & item("ID").Text & ","
                    Next

                    Dim lstDecision As List(Of INS_INFORMATIONDTO) = (From p In INS_INFOMATION Where lstID.Contains(p.ID)
                                                                      Select New INS_INFORMATIONDTO With {.ID = p.ID}).ToList

                    If lstDecision.Count = 0 Then
                        ShowMessage(Translate("Không tồn tại biểu mẫu"), NotifyType.Warning)
                        Exit Sub
                    End If

                    dtData = rep.GetInfoPrint(lstID)
                    If dtData IsNot Nothing Then

                        'ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Insurance\ToKhai.doc"),
                        '                     "ToKhai.doc",
                        '                     dtData,
                        '                     Response)

                        Dim path As String = ""
                        path = AppDomain.CurrentDomain.BaseDirectory & "Files\Insurance\"

                        If Directory.Exists(path) Then
                            Directory.Delete(path, True)
                        Else
                            Directory.CreateDirectory(path)
                        End If

                        For i = 0 To rgManagerInfo.SelectedItems.Count - 1
                            Dim icheck As GridDataItem = rgManagerInfo.SelectedItems.Item(i)
                            Dim fileName As String = "ToKhai_" & icheck.GetDataKeyValue("EMPLOYEE_CODE") & "_" & _
                                                     Format(Date.Now, "yyyyMMddHHmmss") & ".doc"
                            Dim doc As New Document(System.IO.Path.Combine(Server.MapPath(tempPath)) & "\Insurance\ToKhai.doc")
                            doc.MailMerge.Execute(dtData.Rows(i))
                            doc.Save(path & fileName)
                        Next

                        ZipFiles(path)
                    End If

                Case "EXPORT_TEMP"
                    isLoadPopup = 1 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()

                Case "IMPORT_TEMP"
                    ctrlUpload1.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
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

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository
        Dim obj As New INS_INFORMATIONDTO

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgManagerInfo, obj)

            Dim _param = New PARAMDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}

            Dim Sorts As String = rgManagerInfo.MasterTableView.SortExpressions.GetSortString()

            If rdFrom.SelectedDate.HasValue Then
                obj.FROM_DATE_SEARCH = rdFrom.SelectedDate
            End If

            If rdTo.SelectedDate.HasValue Then
                obj.TO_DATE_SEARCH = rdTo.SelectedDate
            End If

            obj.IS_TERMINATE = chkChecknghiViec.Checked

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.INS_INFOMATION = rep.GetINS_INFO(obj, _param, MaximumRows, rgManagerInfo.CurrentPageIndex, rgManagerInfo.PageSize, "CREATED_DATE desc")
                Else
                    Me.INS_INFOMATION = rep.GetINS_INFO(obj, _param, MaximumRows, rgManagerInfo.CurrentPageIndex, rgManagerInfo.PageSize)
                End If
            Else
                Return rep.GetINS_INFO(obj, _param).ToTable
            End If

            rgManagerInfo.VirtualItemCount = MaximumRows
            rgManagerInfo.DataSource = Me.INS_INFOMATION

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgManagerInfo.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter(False)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgManagerInfo.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện thay đổi index page của RadGrid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgManagerInfo.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xuất báo cáo khi 1 ORG đã được chọn ở popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_InfoIns&orgid=" & e.CurrentValue & "');", True)
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim objShift As New INS_INFORMATIONDTO
        Dim rep As New InsuranceRepository
        dtDataImp = New DataTable
        'Dim gID As Decimal

        Try
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Warning)
                Exit Sub
            End If

            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)

                If workbook.Worksheets.GetSheetByCodeName("ImportInfomationIns") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If

                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(5, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            dtData = dtData.Clone()
            dtDataImp = dsDataPrepare.Tables(0).Clone()

            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row As DataRow In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dtDataImp.ImportRow(row)
                Next
            Next

            If dtDataImp.Columns.Count <> 16 Then
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Warning)
                Exit Sub
            End If

            If loadToGrid() Then
                If SaveData() Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("InsertView")
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub rgManagerInfo_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgManagerInfo.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        datarow("ORGNAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '    End If
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load data từ file import
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        loadToGrid = False
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Dim dtEmpID As DataTable

        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If

            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim rep As New InsuranceRepository
            Dim lstEmp As New List(Of String)
            'dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 7

            For Each row As DataRow In dtData.Rows
                isError = False
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                'If row("EMPLOYEE_CODE").ToString <> "" And rowError("EMPLOYEE_CODE").ToString = "" Then
                '    Dim empCode = row("EMPLOYEE_CODE").ToString
                '    If Not lstEmp.Contains(empCode) Then
                '        lstEmp.Add(empCode)
                '    Else
                '        isError = True
                '        rowError("EMPLOYEE_CODE") = "Nhân viên bị trùng trong file import"
                '    End If
                'End If
                If row("SOPRVDBOOKDAY") <> "" Then
                    ImportValidate.IsValidDate("SOPRVDBOOKDAY", row, rowError, isError, sError)
                End If

                If row("DAYPAYMENTCOMPANY") <> "" Then
                    ImportValidate.IsValidDate("DAYPAYMENTCOMPANY", row, rowError, isError, sError)
                End If

                If rowError("SOPRVDBOOKDAY").ToString = "" And _
                  rowError("DAYPAYMENTCOMPANY").ToString = "" And _
                   row("SOPRVDBOOKDAY").ToString <> "" And _
                  row("DAYPAYMENTCOMPANY").ToString <> "" Then
                    Dim startdate = ToDate(row("SOPRVDBOOKDAY"))
                    Dim enddate = ToDate(row("DAYPAYMENTCOMPANY"))

                    If startdate > enddate Then
                        rowError("SOPRVDBOOKDAY") = "Ngày cấp sổ phải nhỏ hơn ngày nộp sổ cho công ty"
                        isError = True
                    End If
                End If

                If row("HECARDEFFFROM") <> "" Then
                    ImportValidate.IsValidDate("HECARDEFFFROM", row, rowError, isError, sError)
                End If

                If row("HECARDEFFTO") <> "" Then
                    ImportValidate.IsValidDate("HECARDEFFTO", row, rowError, isError, sError)
                End If

                If rowError("HECARDEFFFROM").ToString = "" And _
                  rowError("HECARDEFFTO").ToString = "" And _
                   row("HECARDEFFFROM").ToString <> "" And _
                  row("HECARDEFFTO").ToString <> "" Then
                    Dim startdate = ToDate(row("HECARDEFFFROM"))
                    Dim enddate = ToDate(row("HECARDEFFTO"))

                    If startdate > enddate Then
                        rowError("HECARDEFFFROM") = "Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực thẻ"
                        isError = True
                    End If
                End If

                If row("HEWHRREGISKEY_NAME") <> "" Then
                    sError = "Nơi khám chữa bệnh"
                    ImportValidate.IsValidList("HEWHRREGISKEY_NAME", "HEWHRREGISKEY", row, rowError, isError, sError)
                End If

                If row("SOPRVDBOOKSTATUS_NAME") <> "" Then
                    sError = "Trạng thái sổ BHXH"
                    ImportValidate.IsValidList("SOPRVDBOOKSTATUS_NAME", "SOPRVDBOOKSTATUS", row, rowError, isError, sError)
                End If

                If row("HEPRVDCARDSTATUS_NAME") <> "" Then
                    sError = "Trạng thái thẻ BHXH"
                    ImportValidate.IsValidList("HEPRVDCARDSTATUS_NAME", "HEPRVDCARDSTATUS", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next

            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportInfoIns_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            If isError Then
                Return False
            Else
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dtDataImp.Rows.Count - 1
                    If dtDataImp(j)("EMPLOYEE_ID") = "" Then
                        dtEmpID = New DataTable
                        dtEmpID = rep.GetEmployeeID(dtDataImp(j)("EMPLOYEE_CODE"))
                        rowError = dtError.NewRow
                        If dtEmpID Is Nothing Then
                            rowError("STT") = irow
                            rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dtDataImp(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                            dtError.Rows.Add(rowError)
                        Else
                            dtDataImp(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("EMPLOYEE_ID")
                        End If
                        irow = irow + 1
                    End If
                Next

                If dtError.Rows.Count > 0 Then
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    Return True
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Lưu dữ liệu vào database khi click [Lưu]
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveData() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New InsuranceRepository
            Dim obj As New INS_INFORMATIONDTO
            Dim gId As New Decimal?
            gId = 0
            For Each row As DataRow In dtDataImp.Rows
                obj.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                obj.EMPLOYEE_NAME = row("FULLNAME")
                If row("SOBOOKNO") <> "" Then
                    obj.SOBOOKNO = row("SOBOOKNO")
                End If

                If row("SOPRVDBOOKDAY") <> "" Then
                    obj.SOPRVDBOOKDAY = ToDate(row("SOPRVDBOOKDAY"))
                End If

                If row("DAYPAYMENTCOMPANY") <> "" Then
                    obj.DAYPAYMENTCOMPANY = ToDate(row("DAYPAYMENTCOMPANY"))
                End If

                If row("HECARDNO") <> "" Then
                    obj.HECARDNO = row("HECARDNO")
                End If

                If row("HECARDEFFFROM") <> "" Then
                    obj.HECARDEFFFROM = ToDate(row("HECARDEFFFROM"))
                End If

                If row("HECARDEFFTO") <> "" Then
                    obj.HECARDEFFTO = ToDate(row("HECARDEFFTO"))
                End If

                If row("HEWHRREGISKEY") <> "#N/A" Then
                    obj.HEWHRREGISKEY = row("HEWHRREGISKEY")
                End If

                If row("SOPRVDBOOKSTATUS") <> "#N/A" Then
                    obj.SOPRVDBOOKSTATUS = Utilities.ObjToDecima(row("SOPRVDBOOKSTATUS"))
                End If

                If row("HEPRVDCARDSTATUS") <> "#N/A" Then
                    obj.HEPRVDCARDSTATUS = Utilities.ObjToDecima(row("HEPRVDCARDSTATUS"))
                End If

                rep.InsertINS_INFO(obj, gId)
            Next
            Return True

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        Return False
    End Function

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Phuong thuc xu ly viec zip file vao folder Zip
    ''' </summary>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim crc As New Crc32()

            Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            If Not Directory.Exists(pathZip) Then
                Directory.CreateDirectory(pathZip)
            Else
                Directory.Delete(pathZip, True)
            End If

            Dim s As New ZipOutputStream(File.Create(pathZip & "ToKhai.zip"))
            s.SetLevel(0)
            ' 0 - store only to 9 - means best compression
            For i As Integer = 0 To Directory.GetFiles(path).Length - 1
                ' Must use a relative path here so that files show up in the Windows Zip File Viewer
                ' .. hence the use of Path.GetFileName(...)
                Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

                Dim entry As New ZipEntry(fileName)
                entry.DateTime = DateTime.Now

                ' Read in the 
                Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
                    Dim buffer As Byte() = New Byte(fs.Length - 1) {}
                    fs.Read(buffer, 0, buffer.Length)
                    entry.Size = fs.Length
                    fs.Close()
                    crc.Reset()
                    crc.Update(buffer)
                    entry.Crc = crc.Value
                    s.PutNextEntry(entry)
                    s.Write(buffer, 0, buffer.Length)
                End Using
            Next
            s.Finish()
            s.Close()

            Using FileStream = File.Open(pathZip & "ToKhai.zip", FileMode.Open)
                Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
                FileStream.Read(buffer, 0, buffer.Length)
                Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace("ToKhai.zip", "_"))
                Response.AddHeader("Content-Length", FileStream.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.BinaryWrite(buffer)
                FileStream.Close()
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class