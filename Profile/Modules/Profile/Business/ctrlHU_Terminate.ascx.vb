Imports System.Drawing
Imports System.IO
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Terminate
    Inherits Common.CommonView
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Dim log As New UserLog
#Region "Property"

    Property DeleteTerminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_DeleteTerminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_DeleteTerminate") = value
        End Set
    End Property

    Property ApproveTerminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_ApproveTerminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_ApproveTerminate") = value
        End Set
    End Property

    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Refresh()
            UpdateControlState()
            rgTerminate.SetFilter()
            rgTerminate.AllowCustomPaging = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgTerminate)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("TERMINATE_SUPPORT")
                FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            log = LogHelper.GetUserLog
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarTerminates
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Phê duyệt hàng loạt"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_PRINT,
                                                                  ToolbarIcons.Print,
                                                                  ToolbarAuthorize.Special1,
                                                                  "In quyết định nghỉ việc"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("HANDOVER",
                                                                  ToolbarIcons.Print,
                                                                  ToolbarAuthorize.Special1,
                                                                  "In phiếu bàn giao nghỉ việc"))

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái page, trạng thái control, process event xóa dữ liệu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            UpdateCotrolEnabled(False)
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    UpdateCotrolEnabled(True)
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteTerminate(rgTerminate.SelectedValue) Then
                        DeleteTerminate = Nothing
                        IDSelect = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Reset lại page theo trạng thái
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTerminate.Rebind()
                        SelectedItemDataGridByKey(rgTerminate, IDSelect, , rgTerminate.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTerminate.CurrentPageIndex = 0
                        rgTerminate.MasterTableView.SortExpressions.Clear()
                        rgTerminate.Rebind()
                        SelectedItemDataGridByKey(rgTerminate, IDSelect, )
                    Case "Cancel"
                        rgTerminate.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case "KTK"
                    Dim repval As New ProfileBusinessRepository
                    Dim listID As New List(Of Decimal)
                    listID.Add(rgTerminate.SelectedValue)
                    If repval.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rgTerminate.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgTerminate.SelectedItems(0)
                    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                    Dim lstIDs As String = ""

                    Dim item_1 = rgTerminate.SelectedItems
                    If item_1.Count > 100 Then
                        ShowMessage("Giới hạn in 100 dòng", NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each lst As GridDataItem In item_1
                        lstIDs = lstIDs + "," + lst.GetDataKeyValue("ID").ToString
                    Next
                    lstIDs = lstIDs.Remove(0, 1)

                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        'dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                        '                               ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                        '                               folderName)
                        dtData = rep.GetHU_DataDynamic_Muti(lstIDs,
                                                       ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile("QDKTK",
                                                         folderName,
                                                         filePath,
                                                         extension,
                                                         iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If

                    For i As Decimal = 0 To dtData.Rows.Count - 1
                        If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_LOGO") + dtData.Rows(i)("FILE_LOGO"))) Then
                            Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_LOGO"))
                            Dim Image = dtData.Rows(i)("FILE_LOGO")
                            Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                            Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            Else
                                If System.IO.File.Exists(target + "\" + Image) Then
                                    System.IO.File.Delete(target + "\" + Image)
                                End If
                            End If

                            If Not Directory.Exists(tempUpload) Then
                                Directory.CreateDirectory(tempUpload)
                            Else
                                If System.IO.File.Exists(tempUpload + "\" + Image) Then
                                    System.IO.File.Delete(tempUpload + "\" + Image)
                                End If
                            End If

                            Dim file = New FileInfo(tempPathFile + "\" + Image)

                            Try
                                file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                            Catch ex As Exception
                                ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End Try

                            file.IsReadOnly = False

                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                            Dim ratio = originalImage.Width / originalImage.Height
                            Dim height = Convert.ToInt32(150 / ratio)
                            Dim thumbnail As New Bitmap(150, height)
                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                g.DrawImage(originalImage, 0, 0, 150, height)
                            End Using
                            Dim cfileName = Image
                            Dim fileName = System.IO.Path.Combine(target, cfileName)
                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            End If
                            thumbnail.Save(fileName)

                            thumbnail.Dispose()
                            originalImage.Dispose()

                            dtData.Rows(i)("FILE_LOGO") = Server.MapPath("~/EmployeeImageTemp") + "\" + dtData.Rows(i)("FILE_LOGO")
                        End If
                        If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_FOOTER") + dtData.Rows(i)("FILE_FOOTER"))) Then


                            Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_FOOTER"))
                            Dim Image = dtData.Rows(i)("FILE_FOOTER")
                            Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                            Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            Else
                                If System.IO.File.Exists(target + "\" + Image) Then
                                    System.IO.File.Delete(target + "\" + Image)
                                End If
                            End If

                            If Not Directory.Exists(tempUpload) Then
                                Directory.CreateDirectory(tempUpload)
                            Else
                                If System.IO.File.Exists(tempUpload + "\" + Image) Then
                                    System.IO.File.Delete(tempUpload + "\" + Image)
                                End If
                            End If

                            Dim file = New FileInfo(tempPathFile + "\" + Image)

                            Try
                                file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                            Catch ex As Exception
                                ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End Try

                            file.IsReadOnly = False

                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                            Dim thumbnail As New Bitmap(850, 95)
                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                g.DrawImage(originalImage, 0, 0, 850, 95)
                            End Using
                            Dim cfileName = Image
                            Dim fileName = System.IO.Path.Combine(target, cfileName)
                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            End If
                            thumbnail.Save(fileName)

                            thumbnail.Dispose()
                            originalImage.Dispose()

                            dtData.Rows(i)("FILE_FOOTER") = Server.MapPath("~/EmployeeImageTemp") + "\" + dtData.Rows(i)("FILE_FOOTER")
                        End If
                        If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_HEADER") + dtData.Rows(i)("FILE_HEADER"))) Then


                            Dim tempPathFile = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + dtData.Rows(i)("ATTACH_FILE_HEADER"))
                            Dim Image = dtData.Rows(i)("FILE_HEADER")
                            Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImageTemp\"
                            Dim tempUpload As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\RadUploadTemp\"

                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            Else
                                If System.IO.File.Exists(target + "\" + Image) Then
                                    System.IO.File.Delete(target + "\" + Image)
                                End If
                            End If

                            If Not Directory.Exists(tempUpload) Then
                                Directory.CreateDirectory(tempUpload)
                            Else
                                If System.IO.File.Exists(tempUpload + "\" + Image) Then
                                    System.IO.File.Delete(tempUpload + "\" + Image)
                                End If
                            End If

                            Dim file = New FileInfo(tempPathFile + "\" + Image)

                            Try
                                file.CopyTo(Path.Combine(tempUpload + "\" + Image), True)
                            Catch ex As Exception
                                ShowMessage(Translate("Bạn vui lòng xuất lại CV sau 2 phút"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End Try

                            file.IsReadOnly = False

                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(tempUpload, Image))
                            Dim thumbnail As New Bitmap(850, 95)
                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                g.DrawImage(originalImage, 0, 0, 850, 95)
                            End Using
                            Dim cfileName = Image
                            Dim fileName = System.IO.Path.Combine(target, cfileName)
                            If Not Directory.Exists(target) Then
                                Directory.CreateDirectory(target)
                            End If
                            thumbnail.Save(fileName)

                            thumbnail.Dispose()
                            originalImage.Dispose()

                            dtData.Rows(i)("FILE_HEADER") = Server.MapPath("~/EmployeeImageTemp") + "\" + dtData.Rows(i)("FILE_HEADER")
                        End If
                    Next
                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             sourcePath,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CHECK
                    UpdateControlState()
                Case "PRINT_BBBG"
                    Dim repval As New ProfileBusinessRepository
                    Dim listID As New List(Of Decimal)
                    listID.Add(rgTerminate.SelectedValue)
                    If repval.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim item As GridDataItem = rgTerminate.SelectedItems(0)
                    Dim sPath = Server.MapPath("~/ReportTemplates/Profile/Terminate/Print/NV009_BBBG.xlsx")
                    Using rep As New ProfileRepository
                        Dim dsData = rep.GetDataPrintBBBR(item.GetDataKeyValue("ID"))
                        Dim dtVar = dsData.Tables(0)
                        dsData.Tables.RemoveAt(0)
                        Using xls As New ExcelCommon
                            xls.ExportExcelTemplate(sPath,
                                item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & Format(Date.Now, "yyyyMMddHHmmss"),
                                dsData, dtVar, Response)
                        End Using
                    End Using
                Case "PRINT_BBTL"
                    Dim repval As New ProfileBusinessRepository
                    Dim listID As New List(Of Decimal)
                    listID.Add(rgTerminate.SelectedValue)
                    If repval.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgTerminate.SelectedItems(0)
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(5,
                                                         folderName,
                                                         filePath,
                                                         extension,
                                                         iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If
                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                    '"PRINT_QD"
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim repval As New ProfileBusinessRepository
                    Dim listID As New List(Of Decimal)
                    listID.Add(rgTerminate.SelectedValue)
                    If repval.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rgTerminate.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgTerminate.SelectedItems(0)
                    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                    Dim lstIDs As String = ""

                    Dim item_1 = rgTerminate.SelectedItems
                    If item_1.Count > 1 Then
                        ShowMessage("Giới hạn in 1 dòng", NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each lst In item_1
                        lstIDs = lstIDs + "," + lst.GetDataKeyValue("ID").ToString
                    Next
                    lstIDs = lstIDs.Remove(0, 1)

                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic_Muti(lstIDs,
                                                       ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(dtData(0)("BIEUMAU"),
                                                         folderName,
                                                         filePath,
                                                         extension,
                                                         iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If
                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             sourcePath,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgTerminate.ExportExcel(Server, Response, dtData, "Terminate")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveTerminate()
                Case "HANDOVER"
                    If rgTerminate.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgTerminate.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim rep As New ProfileRepository
                    Dim item As GridDataItem = rgTerminate.SelectedItems(0)
                    Dim fByte = rep.GetTemplateFileHost("\TemplateDynamic\Termination\TER_HANDOVER.doc")
                    Dim mStream As New System.IO.MemoryStream(fByte)
                    Dim dtData As New DataTable
                    dtData.Columns.Add("EMPLOYEE_NAME")
                    dtData.Columns.Add("ORG_NAME")
                    dtData.Columns.Add("TER_LASTDATE")
                    Dim newRow As DataRow = dtData.NewRow
                    '',EMPLOYEE_NAME,ORG_NAME,LAST_DATE
                    newRow("EMPLOYEE_NAME") = item.GetDataKeyValue("EMPLOYEE_NAME")
                    newRow("ORG_NAME") = item.GetDataKeyValue("ORG_NAME")
                    newRow("TER_LASTDATE") = CDate(item.GetDataKeyValue("LAST_DATE")).ToString("dd/MM/yyyy")
                    dtData.Rows.Add(newRow)

                    Dim doc As New Aspose.Words.Document(mStream)
                    doc.MailMerge.Execute(dtData)
                    doc.Save(Response, "Ter Handover_" & item.GetDataKeyValue("EMPLOYEE_CODE") & ".doc",
                     Aspose.Words.ContentDisposition.Attachment,
                     Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc))
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes/No trên popup Message Hỏi xóa dữ liệu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgTerminate.CurrentPageIndex = 0
            rgTerminate.MasterTableView.SortExpressions.Clear()
            rgTerminate.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event Click item Node trên TreeView-Sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgTerminate.CurrentPageIndex = 0
            rgTerminate.MasterTableView.SortExpressions.Clear()
            rgTerminate.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reload, load datasource cho grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTerminate.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub rgTerminate_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTerminate.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event click button Hỗ trợ in biểu mẫu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrintSupport_Click(sender As Object, e As System.EventArgs) Handles btnPrintSupport.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboPrintSupport.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
                Exit Sub
            End If
            Dim dtData As DataTable
            Dim folderName As String = ""
            Dim filePath As String = ""
            Dim extension As String = ""
            Dim iError As Integer = 0
            Dim item As GridDataItem = rgTerminate.SelectedItems(0)
            Dim validate As New Profile.ProfileBusiness.OtherListDTO
            ' Kiểm tra + lấy thông tin trong database
            Using rep As New ProfileRepository
                validate.ID = cboPrintSupport.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.TERRMINATE_SUPPORT.Name
                If Not rep.ValidateOtherList(validate) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                    Exit Sub
                End If
                dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                               ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_SUPPORT_ID,
                                               folderName)
                If dtData.Rows.Count = 0 Then
                    ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
                If folderName = "" Then
                    ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
            End Using

            ' Kiểm tra file theo thông tin trong database
            If Not Utilities.GetTemplateLinkFile(cboPrintSupport.SelectedValue,
                                                 folderName,
                                                 filePath,
                                                 extension,
                                                 iError) Then
                Select Case iError
                    Case 1
                        ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                        Exit Sub
                End Select
            End If
            ' Export file mẫu
            Using word As New WordCommon
                word.ExportMailMerge(filePath,
                                     item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & _
                                     Format(Date.Now, "yyyyMMddHHmmss"),
                                     dtData,
                                     Response)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Update trạng thái của control
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ctrlOrg.Enabled = Not bCheck
            Utilities.EnabledGrid(rgTerminate, Not bCheck, False)
            EnableRadDatePicker(rdFromLast, Not bCheck)
            EnableRadDatePicker(rdFromSend, Not bCheck)
            EnableRadDatePicker(rdToLast, Not bCheck)
            EnableRadDatePicker(rdToSend, Not bCheck)

            btnSearch.Enabled = Not bCheck
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data from DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New TerminateDTO
        Dim rep As New ProfileBusinessRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgTerminate.DataSource = New List(Of WorkingDTO)
                Exit Function
            End If
            Dim _param = New Profile.ProfileBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                          .IS_DISSOLVE = ctrlOrg.IsDissolve}
            SetValueObjectByRadGrid(rgTerminate, _filter)

            If rdFromLast.SelectedDate IsNot Nothing Then
                _filter.FROM_LAST_DATE = rdFromLast.SelectedDate
            End If
            If rdFromSend.SelectedDate IsNot Nothing Then
                _filter.FROM_SEND_DATE = rdFromSend.SelectedDate
            End If
            If rdToLast.SelectedDate IsNot Nothing Then
                _filter.TO_LAST_DATE = rdToLast.SelectedDate
            End If
            If rdToSend.SelectedDate IsNot Nothing Then
                _filter.TO_SEND_DATE = rdToSend.SelectedDate
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgTerminate.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetTerminate(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetTerminate(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgTerminate.DataSource = rep.GetTerminate(_filter, rgTerminate.CurrentPageIndex,
                                                     rgTerminate.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgTerminate.DataSource = rep.GetTerminate(_filter, rgTerminate.CurrentPageIndex,
                                                     rgTerminate.PageSize, MaximumRows, _param)
                End If

                rgTerminate.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt nghi viec</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveTerminate()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgTerminate Is Nothing OrElse rgTerminate.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgTerminate.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("262") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                For Each item As GridDataItem In rgTerminate.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                If rep.ApproveListTerminate(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgTerminate.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các đơn nghỉ được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class