Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffWebAppResources.My.Resources
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_WageMng
    Inherits Common.CommonView
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    'Private ReadOnly RestClient As IServerDataRestClient = New ServerDataRestClient()

#Region "Property"
    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("EXPIRE_DATE", GetType(String))
                dt.Columns.Add("SAL_TYPE_NAME", GetType(String))
                dt.Columns.Add("SAL_TYPE_ID", GetType(String))
                dt.Columns.Add("SAL_PAYMENT_NAME", GetType(String))
                dt.Columns.Add("SAL_PAYMENT_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE", GetType(String))
                dt.Columns.Add("TAX_NAME", GetType(String))
                dt.Columns.Add("TAX_ID", GetType(String))
                dt.Columns.Add("SAL_RANK_NAME", GetType(String))
                dt.Columns.Add("SAL_RANK_ID", GetType(String))
                dt.Columns.Add("SAL_BASIC", GetType(String))
                dt.Columns.Add("TOXIC_RATE", GetType(String))
                dt.Columns.Add("TOXIC_SALARY", GetType(String))
                dt.Columns.Add("SALARY_BHXH", GetType(String))
                dt.Columns.Add("PERCENTSALARY", GetType(String))
                dt.Columns.Add("TOTAL_SALARY", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                dt.Columns.Add("SIGN_DATE", GetType(String))
                dt.Columns.Add("SIGN_ID", GetType(String))

                dt.Columns.Add("DECISION_NO", GetType(String))
                dt.Columns.Add("GAS_SAL", GetType(String))
                dt.Columns.Add("PHONE_SAL", GetType(String))
                dt.Columns.Add("ADDITIONAL_SAL", GetType(String))
                dt.Columns.Add("OTHERSALARY1", GetType(String))
                dt.Columns.Add("PC1", GetType(String))
                dt.Columns.Add("PC2", GetType(String))
                dt.Columns.Add("PC3", GetType(String))
                dt.Columns.Add("PC4", GetType(String))
                dt.Columns.Add("PC5", GetType(String))
                dt.Columns.Add("SHARE_SAL", GetType(String))

                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
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

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Load page, set trang thai cua page va control
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
    ''' <summary>
    ''' Khoi tao page, set thuoc tinh cho grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.SetFilter()
            rgWorking.AllowCustomPaging = True
            InitControl()
            ctrlUpload1.isMultiple = False
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgWorking)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao page, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarWorkings
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Export, ToolbarItem.ApproveBatch,
                                       ToolbarItem.Next, ToolbarItem.Import,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = UI.Approve
            CType(MainToolBar.Items(3), RadToolBarButton).Text = Translate("Phê duyệt")
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Nhập file mẫu")

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_UNLOCK,
                                                                  ToolbarIcons.Unlock,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Mở phê duyệt"))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("ADMIN_UNLOCK",
                                                                  ToolbarIcons.Unlock,
                                                                  ToolbarAuthorize.Special2,
                                                                  "Mở PD ADMIN"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_PRINT,
                                                                  ToolbarIcons.Print,
                                                                  ToolbarAuthorize.Print,
                                                                  "In thông báo lương"))

            CType(MainToolBar.Items(3), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_SPECIAL1
            CType(MainToolBar.Items(4), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_SPECIAL5
            CType(MainToolBar.Items(5), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_IMPORT
            CType(MainToolBar.Items(7), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_SPECIAL3
            CType(MainToolBar.Items(8), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_SPECIAL2

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' reset trang thai page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                'If Not LogHelper.CurrentUser.USERNAME.ToUpper.Equals("ADMIN") Then
                '    CType(MainToolBar.Items(8), RadToolBarButton).Visible = False
                'End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' get data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
#If DEBUG Then
            ' Dim test = RestClient.GetAll
#End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region

#Region "Event"
    ''' <summary>
    ''' event click item menu toolbar
    ''' update lai trang thai control, trang thai page sau khi process event xong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgWorking.ExportExcel(Server, Response, dtData, "Wage")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgWorking.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim dtData As DataTable
                    Dim folderName As String = "Decision"
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgWorking.SelectedItems(0)
                    Dim repValidate As New ProfileBusinessRepository
                    Dim lstIDs As String = ""
                    Dim item_1 = rgWorking.SelectedItems
                    If item_1.Count > 1 Then
                        ShowMessage("Giới hạn in 1 dòng", NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each lst In item_1
                        lstIDs = lstIDs + "," + lst.GetDataKeyValue("ID").ToString
                    Next
                    lstIDs = lstIDs.Remove(0, 1)
                    'If repValidate.ValidateWorking("EXIST_ID", validate) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic_Muti(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DECISION_ID,
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
                    If Not Utilities.GetTemplateLinkFile("TBL",
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
                    Next
                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_HSL_" &
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID And item.GetDataKeyValue("EFFECT_DATE") <= DateTime.Now Then
                            ShowMessage(Translate("Tồn tại hồ sơ lương có ngày hiệu lực <= ngày hiện tại"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                            ShowMessage(Translate("Hồ sơ lương đang ở trạng thái chờ phê duyệt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    Dim workingIds = New List(Of Decimal)
                    For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                        workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                    Next
                    'Dim validateMsg = ValidateApprove(workingIds)
                    'If validateMsg.Count > 0 Then
                    '    Translate(validateMsg.ToString(), NotifyType.Warning)
                    'End If
                    ctrlMessageBox.MessageText = Translate("Bạn có chắc muốn mở phê duyệt")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "ADMIN_UNLOCK"
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                            ShowMessage(Translate("Hồ sơ lương đang ở trạng thái chờ phê duyệt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Bạn có chắc muốn mở phê duyệt")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    Dim workingIds = New List(Of Decimal)
                    For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                        workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                    Next
                    Dim validateMsg = ValidateApprove(workingIds)
                    If validateMsg.Count > 0 Then
                        Translate(validateMsg.ToString(), NotifyType.Warning)
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn phê duyệt")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportHoSoLuong()
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportHoSoLuong');", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select

            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function ValidateApprove(ByRef workingIds As List(Of Decimal)) As List(Of String)
        Dim result = New List(Of String)
        Using repo As New ProfileBusinessRepository
            Dim param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                    .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Dim workings = repo.GetWorking(New WorkingDTO With {.Ids = workingIds}, param)
            For Each workingDto As WorkingDTO In workings
                If Not workingDto.STATUS_ID.HasValue Or workingDto.STATUS_ID.Value <> ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                    result.Add(String.Format("{0} ", workingDto.ID))
                End If
            Next
        End Using
        Return result
    End Function
    ''' <summary>
    ''' Load lai grid khi click node in treeview
    ''' Rebind=> reload lai ham NeedDataSource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click nut tim kiem theo thang bien dong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event Yes/No cua message popup
    ''' update lai trang thai page, trang thai control sau khi process xong event
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
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
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

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            'TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""' OR STT<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then Continue For
                'Dim repFactor As String = rows("FACTORSALARY").ToString.Trim.Replace(",", ".")
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("EXPIRE_DATE") = rows("EXPIRE_DATE")
                newRow("SAL_TYPE_NAME") = rows("SAL_TYPE_NAME")
                newRow("SAL_TYPE_ID") = rows("SAL_TYPE_ID")
                newRow("SAL_PAYMENT_ID") = rows("SAL_PAYMENT_ID")
                newRow("SAL_PAYMENT_NAME") = rows("SAL_PAYMENT_NAME")
                newRow("TAX_NAME") = rows("TAX_NAME")
                newRow("TAX_ID") = rows("TAX_ID")
                newRow("SAL_BASIC") = rows("SAL_BASIC")
                newRow("SALARY_BHXH") = rows("SALARY_BHXH")
                newRow("PERCENTSALARY") = rows("PERCENTSALARY")
                newRow("STATUS_NAME") = rows("STATUS_NAME")
                newRow("STATUS_ID") = rows("STATUS_ID")

                'If IsDate(rows("SIGN_DATE")) Then
                '    newRow("SIGN_DATE") = rows("SIGN_DATE")
                'Else
                '    newRow("SIGN_DATE") = rows("EFFECT_DATE")
                'End If
                If IsDBNull(rows("SIGN_DATE")) OrElse rows("SIGN_DATE").ToString = "" Then
                    newRow("SIGN_DATE") = rows("EFFECT_DATE")
                Else
                    newRow("SIGN_DATE") = rows("SIGN_DATE")
                End If
                newRow("SIGN_ID") = rows("SIGN_ID")

                newRow("DECISION_NO") = rows("DECISION_NO")
                newRow("GAS_SAL") = rows("GAS_SAL")
                newRow("PHONE_SAL") = rows("PHONE_SAL")
                newRow("ADDITIONAL_SAL") = rows("ADDITIONAL_SAL")
                newRow("OTHERSALARY1") = rows("OTHERSALARY1")
                newRow("PC1") = rows("PC1")
                newRow("PC2") = rows("PC2")
                newRow("PC3") = rows("PC3")
                newRow("PC4") = rows("PC4")
                newRow("PC5") = rows("PC5")

                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_HoSoLuong(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgWorking.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub Template_ImportHoSoLuong()
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = rep.GetHoSoLuongImport()
            rep.Dispose()

            ExportTemplate("Payroll\Business\TEMP_IMPORT_HOSOLUONG.xls",
                                  dsData, Nothing, "import_HoSoLuong" & Format(Date.Now, "yyyymmdd"))

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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Check data khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            dtDataImportWorking = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                'If row("EFFECT_DATE") Is DBNull.Value OrElse row("EFFECT_DATE") = "" Then
                '    sError = "Chưa nhập ngày hiệu lực"
                '    ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                'Else
                '    If IsDate(row("EFFECT_DATE")) = False Then
                '        sError = "Ngày hiệu lực không đúng định dạng"
                '        ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                '    End If
                '    Try
                '        If IBusiness.ValEffectdateByEmpCode(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_DATE"))) = False Then
                '            sError = "Tồn tại hồ sơ lương trùng ngày hiệu lực"
                '            ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                '        End If
                '    Catch ex As Exception
                '        GoTo VALIDATE
                '    End Try
                'End If

                If IsDBNull(row("EFFECT_DATE")) OrElse row("EFFECT_DATE").ToString = "" Then
                    isError = True
                    rowError("EFFECT_DATE") = "Chưa nhập ngày hiệu lực"
                ElseIf Not CheckValidDate(row("EFFECT_DATE")) Then
                    isError = True
                    rowError("EFFECT_DATE") = "Ngày hiệu lực không đúng định dạng"
                Else
                    If (Not IsDBNull(row("EXPIRE_DATE")) AndAlso Not row("EXPIRE_DATE").ToString = "") Then
                        If Not CheckValidDate(row("EXPIRE_DATE")) Then
                            isError = True
                            rowError("EXPIRE_DATE") = "Ngày hết hiệu lực không đúng định dạng"
                        Else
                            If CDate(row("EFFECT_DATE")) >= CDate(row("EXPIRE_DATE")) Then
                                isError = True
                                rowError("EXPIRE_DATE") = "Ngày hết hiệu lực phải lớn hơn ngày hiệu lực"
                            End If
                        End If
                    End If
                    Try
                        Dim effect_date As Date
                        DateTime.TryParseExact(row("EFFECT_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, effect_date)
                        If IBusiness.ValEffectdateByEmpCode(row("EMPLOYEE_CODE"), effect_date) = False Then
                            isError = True
                            rowError("EFFECT_DATE") = "Tồn tại hồ sơ lương trùng ngày hiệu lực"
                        End If
                    Catch ex As Exception
                        GoTo VALIDATE
                    End Try
                End If
VALIDATE:
                Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))

                If empId = 0 Then
                    'sError = "Mã nhân viên không tồn tại"
                    'ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                    isError = True
                    rowError("EMPLOYEE_CODE") = "Mã nhân viên không tồn tại"
                Else
                    row("EMPLOYEE_ID") = empId
                End If

                '' Validate Null
                'sError = "Chưa nhập Lương thỏa thuận"
                'ImportValidate.EmptyValue("SAL_BASIC", row, rowError, isError, sError)

                'sError = "Chưa chọn nhóm lương"
                'ImportValidate.EmptyValue("SAL_TYPE_ID", row, rowError, isError, sError)
                If IsDBNull(row("SAL_TYPE_NAME")) OrElse row("SAL_TYPE_NAME").ToString = "" Then
                    isError = True
                    rowError("SAL_TYPE_NAME") = "Chưa chọn Nhóm lương"
                ElseIf Not IsNumeric(row("SAL_TYPE_ID").ToString) Then
                    isError = True
                    rowError("SAL_TYPE_NAME") = "Chưa chọn Nhóm lương"
                End If

                If IsDBNull(row("SAL_PAYMENT_NAME")) OrElse row("SAL_PAYMENT_NAME").ToString = "" Then
                    isError = True
                    rowError("SAL_PAYMENT_NAME") = "Chưa chọn Hình thức trả lương"
                ElseIf Not IsNumeric(row("SAL_TYPE_ID").ToString) Then
                    isError = True
                    rowError("SAL_PAYMENT_NAME") = "Chưa chọn Hình thức trả lương"
                End If
                sError = "Chưa nhập STT"
                ImportValidate.EmptyValue("STT", row, rowError, isError, sError)

                'sError = "Chưa chọn Biểu thuế"
                'ImportValidate.EmptyValue("TAX_ID", row, rowError, isError, sError)
                If IsDBNull(row("TAX_NAME")) OrElse row("TAX_NAME").ToString = "" Then
                    isError = True
                    rowError("TAX_NAME") = "Chưa nhập Biểu thuế"
                ElseIf Not IsNumeric(row("TAX_ID").ToString) Then
                    isError = True
                    rowError("TAX_NAME") = "Chưa nhập Biểu thuế"
                End If

                'sError = "Chưa chọn Trạng thái"
                'ImportValidate.EmptyValue("STATUS_ID", row, rowError, isError, sError)
                If IsDBNull(row("STATUS_NAME")) OrElse row("STATUS_NAME").ToString = "" Then
                    isError = True
                    rowError("STATUS_NAME") = "Chưa nhập Trạng thái"
                End If

                ''Validate Number

                'If row("TOTAL_SALARY") IsNot DBNull.Value AndAlso row("TOTAL_SALARY") <> "" Then
                '    sError = "Chỉ được nhập số"
                '    ImportValidate.IsValidNumber("TOTAL_SALARY", row, rowError, isError, sError)
                'End If

                If IsDBNull(row("SALARY_BHXH")) OrElse row("SALARY_BHXH").ToString = "" Then
                    isError = True
                    rowError("SALARY_BHXH") = "Chưa nhập Lương đóng BHXH"
                ElseIf Not IsNumeric(row("SALARY_BHXH").ToString) Then
                    isError = True
                    rowError("SALARY_BHXH") = "Chỉ được nhập số"
                End If

                If IsDBNull(row("PERCENTSALARY")) OrElse row("PERCENTSALARY").ToString = "" Then
                    isError = True
                    rowError("PERCENTSALARY") = "Chưa nhập % Hưởng lương"
                ElseIf Not IsNumeric(row("PERCENTSALARY").ToString) Then
                    isError = True
                    rowError("PERCENTSALARY") = "Chỉ được nhập số"
                ElseIf row("PERCENTSALARY").ToString <> "85" AndAlso row("PERCENTSALARY").ToString <> "90" AndAlso row("PERCENTSALARY").ToString <> "95" AndAlso row("PERCENTSALARY").ToString <> "100" Then
                    isError = True
                    rowError("PERCENTSALARY") = "Chưa nhập % Hưởng lương"
                End If

                If IsDBNull(row("SAL_BASIC")) OrElse row("SAL_BASIC").ToString = "" Then
                    isError = True
                    rowError("SAL_BASIC") = "Chưa nhập Lương thỏa thuận"
                ElseIf Not IsNumeric(row("SAL_BASIC").ToString) Then
                    isError = True
                    rowError("SAL_BASIC") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("GAS_SAL")) AndAlso row("GAS_SAL").ToString <> "" AndAlso Not IsNumeric(row("GAS_SAL").ToString) Then
                    isError = True
                    rowError("GAS_SAL") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PHONE_SAL")) AndAlso row("PHONE_SAL").ToString <> "" AndAlso Not IsNumeric(row("PHONE_SAL").ToString) Then
                    isError = True
                    rowError("PHONE_SAL") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("ADDITIONAL_SAL")) AndAlso row("ADDITIONAL_SAL").ToString <> "" AndAlso Not IsNumeric(row("ADDITIONAL_SAL").ToString) Then
                    isError = True
                    rowError("ADDITIONAL_SAL") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("OTHERSALARY1")) AndAlso row("OTHERSALARY1").ToString <> "" AndAlso Not IsNumeric(row("OTHERSALARY1").ToString) Then
                    isError = True
                    rowError("OTHERSALARY1") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PC1")) AndAlso row("PC1").ToString <> "" AndAlso Not IsNumeric(row("PC1").ToString) Then
                    isError = True
                    rowError("PC1") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PC2")) AndAlso row("PC2").ToString <> "" AndAlso Not IsNumeric(row("PC2").ToString) Then
                    isError = True
                    rowError("PC2") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PC3")) AndAlso row("PC3").ToString <> "" AndAlso Not IsNumeric(row("PC3").ToString) Then
                    isError = True
                    rowError("PC3") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PC4")) AndAlso row("PC4").ToString <> "" AndAlso Not IsNumeric(row("PC4").ToString) Then
                    isError = True
                    rowError("PC4") = "Chỉ được nhập số"
                End If

                If Not IsDBNull(row("PC5")) AndAlso row("PC5").ToString <> "" AndAlso Not IsNumeric(row("PC5").ToString) Then
                    isError = True
                    rowError("PC5") = "Chỉ được nhập số"
                End If

                If isError = False Then
                    If If(IsNumeric(row("SALARY_BHXH")), Decimal.Parse(row("SALARY_BHXH")), 0) +
                       If(IsNumeric(row("GAS_SAL")), Decimal.Parse(row("GAS_SAL")), 0) +
                       If(IsNumeric(row("PHONE_SAL")), Decimal.Parse(row("PHONE_SAL")), 0) +
                       If(IsNumeric(row("ADDITIONAL_SAL")), Decimal.Parse(row("ADDITIONAL_SAL")), 0) +
                       If(IsNumeric(row("OTHERSALARY1")), Decimal.Parse(row("OTHERSALARY1")), 0) +
                       If(IsNumeric(row("PC1")), Decimal.Parse(row("PC1")), 0) +
                       If(IsNumeric(row("PC2")), Decimal.Parse(row("PC2")), 0) +
                       If(IsNumeric(row("PC3")), Decimal.Parse(row("PC3")), 0) +
                       If(IsNumeric(row("PC4")), Decimal.Parse(row("PC4")), 0) +
                       If(IsNumeric(row("PC5")), Decimal.Parse(row("PC5")), 0) <> If(IsNumeric(row("SAL_BASIC")), Decimal.Parse(row("SAL_BASIC")), 0) Then
                        isError = True
                        rowError("SAL_BASIC") = "Lương thỏa thuận phải bằng tổng tất cả các khoảng"
                    End If
                End If

                If Not IsDBNull(row("SIGN_DATE")) AndAlso row("SIGN_DATE").ToString <> "" AndAlso Not CheckValidDate(row("SIGN_DATE")) Then
                    isError = True
                    rowError("SIGN_DATE") = "Ngày ký không đúng định dạng"
                End If

                'If row("TOXIC_SALARY") IsNot DBNull.Value AndAlso row("TOXIC_SALARY") <> "" Then
                '    sError = "Chỉ được nhập số"
                '    ImportValidate.IsValidNumber("TOXIC_SALARY", row, rowError, isError, sError)
                'End If

                'If row("TOXIC_RATE") IsNot DBNull.Value AndAlso row("TOXIC_RATE") <> "" Then
                '    If Not IsNumeric(row("TOXIC_RATE")) Then
                '        rowError("TOXIC_RATE") = "Chỉ được nhập số"
                '        isError = True
                '    Else
                '        row("TOXIC_RATE") = row("TOXIC_RATE").ToString().Replace(",", ".")
                '    End If
                'End If


                'If Not row("PERCENTSALARY") Is DBNull.Value OrElse Not row("PERCENTSALARY") = "" Then
                '    If row("EMPLOYEE_TYPE_NAME").ToString = "Thử việc" Then
                '        If IsNumeric(row("PERCENTSALARY")) Then
                '            If Integer.Parse(row("PERCENTSALARY")) < 85 Or Integer.Parse(row("PERCENTSALARY")) > 100 Then
                '                sError = "Giá trị nhập không đúng quy định"
                '                ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                '            End If
                '        End If
                '    End If
                '    If row("EMPLOYEE_TYPE_NAME").ToString = "Chính thức" Then
                '        If IsNumeric(row("PERCENTSALARY")) Then
                '            If Integer.Parse(row("PERCENTSALARY")) <> 100 Then
                '                sError = "Giá trị nhập không đúng quy định"
                '                ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                '            End If
                '        End If
                '    End If
                'End If
                ''NGUOI KY k có trong hệ thống
                'If IsNumeric(row("SIGN_ID")) Then
                '    If IBusiness.CHECK_SIGN(row("SIGN_ID")) = 0 Then
                '        sError = "Người ký - Không tồn tại"
                '        ImportValidate.IsValidTime("SIGN_ID", row, rowError, isError, sError)
                '    Else
                '        Dim SIG_Id = rep.CheckEmployee_Exits(row("SIGN_ID"))
                '        If SIG_Id <> 0 Then
                '            row("SIGN_ID") = SIG_Id.ToString
                '        Else
                '            row("SIGN_ID") = 0
                '        End If
                '    End If
                'Else
                '    If IsDate(row("EFFECT_DATE")) Then
                '        Dim signer = sp.GET_SIGNER_BY_FUNC("ctrlHU_WageNewEdit", CDate(row("EFFECT_DATE")))
                '        If signer.Rows.Count > 0 Then
                '            row("SIGN_ID") = signer.Rows(0)("ID").ToString
                '        Else
                '            row("SIGN_ID") = 0
                '        End If
                '    Else
                '        row("SIGN_ID") = 0
                '    End If
                'End If
                If isError Then
                    'rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    'If rowError("EMPLOYEE_CODE").ToString = "" Then
                    '    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    'End If
                    If row("STT").ToString <> "" Then
                        rowError("STT") = row("STT")
                    Else
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                        rowError("EFFECT_DATE") = row("EFFECT_DATE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_HOSOLUONG');", True)
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
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    ''' <summary>
    ''' Thiet lap lai trang thai control
    ''' process event xoa du lieu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep1 As New ProfileStoreProcedure
        Try
            tbarWorkings.Enabled = True
            rgWorking.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_DELETE
                    Dim lstdel = ","

                    For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                        lstdel = lstdel + selectedItem.GetDataKeyValue("ID").ToString + ","
                    Next
                    If rep1.DEL_HSL(lstdel) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorking.CurrentPageIndex = 0
                        rgWorking.MasterTableView.SortExpressions.Clear()

                        rgWorking.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    'Using rep As New ProfileBusinessRepository
                    '    If rep.DeleteWorking(New WorkingDTO With {.ID = rgWorking.SelectedValue}) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '        CurrentState = CommonMessage.STATE_NORMAL
                    '        rgWorking.CurrentPageIndex = 0
                    '        rgWorking.MasterTableView.SortExpressions.Clear()

                    '        rgWorking.Rebind()
                    '    Else
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    End If
                    'End Using

                Case CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    Using rep As New ProfileBusinessRepository
                        Dim workingIds = New List(Of Decimal)
                        Dim acti = ""
                        For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                            workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                            acti = CDec(Val(selectedItem.GetDataKeyValue("STATUS_ID")))
                        Next
                        Dim bCheckHasfile = rep.CheckHasFile(workingIds)
                        'If bCheckHasfile = 1 Then
                        '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rep.ApproveWorkings(workingIds, acti).Status = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgWorking.CurrentPageIndex = 0
                            rgWorking.MasterTableView.SortExpressions.Clear()
                            rgWorking.Rebind()
                        End If
                    End Using
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Get data from database to grid
    ''' </summary>
    ''' <param name="isFull">If = true thi full data, =false load filter or phan trang</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New WorkingDTO
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgWorking.DataSource = New List(Of EmployeeDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgWorking, _filter)

            _filter.FROM_DATE = rdEffectDate.SelectedDate
            _filter.TO_DATE = rdExpireDate.SelectedDate
            _filter.IS_WAGE = True
            ' _filter.IS_MISSION = False
            _filter.IS_TER = chkTerminate.Checked
            If chkhsl.Checked Then
                Dim rep1 As New ProfileStoreProcedure
                Dim tb = rep1.GET_WORKING_WAGE_NEW()
                _filter.Ids = (From item In tb.AsEnumerable() Select item.Field(Of Decimal)(0)).ToList()
            Else
                _filter.Ids = Nothing
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorking.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWorking(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetWorking(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param)
                End If

                rgWorking.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub rgWorking_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorking.ItemDataBound
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
#End Region
#Region "Utility"
    Public Function GetUiResx(ByVal input) As String
        Return UI.ResourceManager.GetString(input)
    End Function
    Public Function GetErrorsResx(ByVal input) As String
        Return Errors.ResourceManager.GetString(input)
    End Function
    Private Function CheckValidDate(ByVal value As String) As Boolean
        Dim isValid As Boolean
        Dim result As Date
        Try
            isValid = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return isValid
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
End Class