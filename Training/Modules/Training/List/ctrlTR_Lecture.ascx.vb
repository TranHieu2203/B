Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Ionic.Crc
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlTR_Lecture
    Inherits Common.CommonView

    Protected WithEvents ctrlFindLecturePopup As ctrlFindEmployeePopup
#Region "Property"

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Training\Modules\Training\List" + Me.GetType().Name.ToString()
    Public Property Lectures As List(Of LectureDTO)
        Get
            Return ViewState(Me.ID & "_Lectures")
        End Get
        Set(ByVal value As List(Of LectureDTO))
            ViewState(Me.ID & "_Lectures") = value
        End Set
    End Property

    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    '0 - normal
    '1 - Lecture
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set

    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else

                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New TrainingRepository
        Dim _filter As New LectureDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetLecture(_filter, Sorts).ToTable()
                Else
                    Return rep.GetLecture(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Lectures = rep.GetLecture(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.Lectures = rep.GetLecture(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.Lectures
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            If phFindLecture.Controls.Contains(ctrlFindLecturePopup) Then
                phFindLecture.Controls.Remove(ctrlFindLecturePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindLecturePopup = Me.Register("ctrlFindLecturePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindLecturePopup.MustHaveContract = False
                    phFindLecture.Controls.Add(ctrlFindLecturePopup)
                    ctrlFindLecturePopup.MultiSelect = False
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboCenter, True)
                    txtAddress.ReadOnly = False
                    txtService.ReadOnly = False
                    txtVendorCode.ReadOnly = False
                    txtVendorName.ReadOnly = False
                    txtWebsite.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtEmail.ReadOnly = False
                    txtName.ReadOnly = False
                    txtPhone.ReadOnly = False
                    txtRemark.ReadOnly = False
                    chkIsLocal.Enabled = True
                    chkIsLocal.AutoPostBack = True
                    btnDownload.Enabled = True
                    btnUploadFile.Enabled = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboCenter, False)
                    txtAddress.ReadOnly = True
                    txtService.ReadOnly = True
                    txtVendorCode.ReadOnly = True
                    txtVendorName.ReadOnly = True
                    txtWebsite.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtEmail.ReadOnly = True
                    txtName.ReadOnly = True
                    txtPhone.ReadOnly = True
                    txtRemark.ReadOnly = True
                    chkIsLocal.Enabled = False
                    btnFindLecture.Enabled = False
                    btnUploadFile.Enabled = False

                    txtAddress.Text = ""
                    txtService.Text = ""
                    txtVendorCode.Text = ""
                    txtVendorName.Text = ""
                    txtWebsite.Text = ""
                    txtCode.Text = ""
                    txtEmail.Text = ""
                    txtName.Text = ""
                    txtPhone.Text = ""
                    txtRemark.Text = ""
                    cboCenter.ClearSelection()
                    cboCenter.Text = ""
                    chkIsLocal.Checked = False
                    chkIsLocal.AutoPostBack = False
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboCenter, True)
                    txtAddress.ReadOnly = False
                    txtService.ReadOnly = False
                    txtVendorCode.ReadOnly = False
                    txtVendorName.ReadOnly = False
                    txtWebsite.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtEmail.ReadOnly = False
                    txtName.ReadOnly = False
                    txtPhone.ReadOnly = False
                    txtRemark.ReadOnly = False
                    chkIsLocal.Enabled = True
                    chkIsLocal.AutoPostBack = True
                    btnFindLecture.Enabled = True
                    btnUploadFile.Enabled = True

                    If txtUpload.Text <> "" Then
                        btnDownload.Enabled = True
                    Else
                        btnDownload.Enabled = False
                    End If


                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of LectureDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New LectureDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteLecture(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveLecture(lstDeletes, True) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveLecture(lstDeletes, False) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository
                dtData = rep.GetTrCenterList(True)
                FillRadCombobox(cboCenter, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("SERVICE", txtService)
            dic.Add("WEBSITE", txtWebsite)
            dic.Add("VENDOR_NAME", txtVendorName)
            dic.Add("VENDOR_CODE", txtVendorCode)
            dic.Add("ADDRESS", txtAddress)
            dic.Add("LECTURE_CODE", txtCode)
            dic.Add("EMAIL", txtEmail)
            dic.Add("LECTURE_NAME", txtName)
            dic.Add("PHONE", txtPhone)
            dic.Add("REMARK", txtRemark)
            dic.Add("LECTURE_ID", hidLectureID)
            dic.Add("IS_LOCAL", chkIsLocal)
            dic.Add("TR_CENTER_ID", cboCenter)
            dic.Add("FILENAME", txtUpload)
            dic.Add("UPLOAD_FILE", txtRemindLink)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Training/TrainingInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS, XLSX, TXT, CTR, DOC, DOCX, XML, PNG, JPG, BITMAP, JPEG, PDF"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
                btnDownload.Enabled = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String
                If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
                    If txtRemindLink.Text IsNot Nothing Then
                        If txtRemindLink.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Training/TrainingInfo/" + txtRemindLink.Text)
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Training/TrainingInfo/" + Down_File)
                        ZipFiles(strPath_Down)
                    End If
                End If
                'Else
                '    ShowMessage(Translate("Không có gì để  tải xuôgns"), NotifyType.Warning)
                '    Exit Sub
            Else
                Dim dic As New Dictionary(Of String, Control)
                dic.Add("FILENAME", txtUpload)
                dic.Add("UPLOAD_FILE", txtRemindLink)
                dic.Add("SERVICE", txtService)
                dic.Add("WEBSITE", txtWebsite)
                dic.Add("VENDOR_NAME", txtVendorName)
                dic.Add("VENDOR_CODE", txtVendorCode)
                dic.Add("ADDRESS", txtAddress)
                dic.Add("LECTURE_CODE", txtCode)
                dic.Add("EMAIL", txtEmail)
                dic.Add("LECTURE_NAME", txtName)
                dic.Add("PHONE", txtPhone)
                dic.Add("REMARK", txtRemark)
                dic.Add("LECTURE_ID", hidLectureID)
                dic.Add("IS_LOCAL", chkIsLocal)
                dic.Add("TR_CENTER_ID", cboCenter)
                Utilities.OnClientRowSelectedChanged(rgMain, dic)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objLecture As New LectureDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtAddress.Text = ""
                    txtService.Text = ""
                    txtUpload.Text = ""
                    txtUploadFile.Text = ""
                    txtVendorCode.Text = ""
                    txtVendorName.Text = ""
                    txtWebsite.Text = ""
                    txtCode.Text = ""
                    txtEmail.Text = ""
                    txtName.Text = ""
                    txtPhone.Text = ""
                    txtRemark.Text = ""
                    cboCenter.ClearSelection()
                    cboCenter.Text = ""
                    chkIsLocal.Checked = False
                    chkIsLocal.AutoPostBack = True
                    chkIsLocal_CheckedChanged(Nothing, Nothing)
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
                    chkIsLocal_CheckedChanged(Nothing, Nothing)
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    txtCode.Text = item.GetDataKeyValue("LECTURE_CODE")
                    txtName.Text = item.GetDataKeyValue("LECTURE_NAME")
                    hidLectureID.Value = ""
                    If item.GetDataKeyValue("LECTURE_ID") IsNot Nothing Then
                        hidLectureID.Value = item.GetDataKeyValue("LECTURE_ID")
                    End If
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Lecture")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objLecture.EMAIL = txtEmail.Text
                        objLecture.IS_LOCAL = chkIsLocal.Checked
                        If chkIsLocal.Checked Then
                            objLecture.LECTURE_ID = hidLectureID.Value

                        End If
                        objLecture.ADDRESS = txtAddress.Text
                        objLecture.FILENAME = txtUpload.Text
                        objLecture.SERVICE = txtService.Text
                        objLecture.VENDOR_CODE = txtVendorCode.Text
                        objLecture.VENDOR_NAME = txtVendorName.Text
                        objLecture.UPLOAD_FILE = If(Down_File Is Nothing, "", Down_File)
                        If objLecture.UPLOAD_FILE = "" Then
                            objLecture.UPLOAD_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objLecture.UPLOAD_FILE = If(objLecture.UPLOAD_FILE Is Nothing, "", objLecture.UPLOAD_FILE)
                        End If
                        objLecture.WEBSITE = txtWebsite.Text
                        objLecture.LECTURE_CODE = txtCode.Text
                        objLecture.LECTURE_NAME = txtName.Text
                        objLecture.PHONE = txtPhone.Text
                        objLecture.REMARK = txtRemark.Text
                        If cboCenter.SelectedValue <> "" Then
                            objLecture.TR_CENTER_ID = cboCenter.SelectedValue
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objLecture.ACTFLG = True
                                If rep.InsertLecture(objLecture, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objLecture.ID = rgMain.SelectedValue
                                If rep.ModifyLecture(objLecture, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objLecture.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindLecture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindLecture.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindLecturePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try


    End Sub

    Private Sub ctrlFindLecturePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindLecturePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindLecturePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            chkIsLocal_CheckedChanged(Nothing, Nothing)
            Dim itm = lstCommonEmployee(0)
            hidLectureID.Value = itm.ID
            txtCode.Text = itm.EMPLOYEE_CODE
            txtName.Text = itm.FULLNAME_VN
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub chkIsLocal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsLocal.CheckedChanged
        Try
            hidLectureID.Value = ""
            txtCode.Text = ""
            txtName.Text = ""
            If chkIsLocal.Checked Then
                cboCenter.Enabled = False
                cusCenter.Enabled = False
                txtCode.ReadOnly = True
                txtName.ReadOnly = True
                btnFindLecture.Enabled = True
            Else
                cboCenter.Enabled = True
                cusCenter.Enabled = True
                txtCode.ReadOnly = False
                txtName.ReadOnly = False
                btnFindLecture.Enabled = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String = txtUpload.Text.Trim

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class