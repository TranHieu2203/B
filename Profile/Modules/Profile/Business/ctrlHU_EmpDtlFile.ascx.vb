Imports System.IO
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpDtlFile
    Inherits CommonView
    Dim employeeCode As String
    'Public Overrides Property MustAuthorize As Boolean = False

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Business/" + Me.GetType().Name.ToString()
    Dim pathFile As String = System.Configuration.ConfigurationManager.AppSettings("PathFileEmpFolder").ToString()

#Region "Properties"
    ''' <summary>
    ''' OBJ Employee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
    ''' <summary>
    ''' IsLoad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
    ''' <summary>
    ''' ID nhân viên được chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property lstHuFile As List(Of HuFileDTO)
        Get
            Return ViewState(Me.ID & "_lstHuFile")
        End Get
        Set(ByVal value As List(Of HuFileDTO))
            ViewState(Me.ID & "_lstHuFile") = value
        End Set
    End Property
    ''' <summary>
    ''' SelectedItem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    ''' <summary>
    ''' ComboBoxDataDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    ''' <summary>
    ''' 'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
    ''' <summary>
    ''' LicenseFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LicenseFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_LicenseFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_LicenseFile") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Set property for employee info
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao các thiết lập ban đầu
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Try
            rgHuFile.SetFilter()
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPaneLeft)
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Phương thức khởi tạo các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Create,
                         ToolbarItem.Edit,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel,
                         ToolbarItem.Export,
                         ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set thuộc tính cho CurrentPlaceHolder
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            'Using rep As New ProfileRepository
            '    If ComboBoxDataDTO Is Nothing Then
            '        ComboBoxDataDTO = New ComboBoxDataDTO
            '    End If
            '    ComboBoxDataDTO.GET_HUFILE = True
            '    rep.GetComboList(ComboBoxDataDTO)
            '    If ComboBoxDataDTO IsNot Nothing Then
            '        FillDropDownList(cboTypeFile, ComboBoxDataDTO.LIST_HUFILE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTypeFile.SelectedValue)
            '    End If
            'End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện command của ontoolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_NEW
                        ResetControlValue()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_EDIT

                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ResetControlValue()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ResetControlValue()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isLoad = False
                        rgHuFile.Rebind()
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    ResetControlValue()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = lstHuFile.ToTable
                        If dtData.Rows.Count > 0 Then
                            rgHuFile.ExportExcel(Server, Response, dtData, "EmpDtlFile")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
            ctrlEmpBasicInfo.SetProperty("CurrentState", Me.CurrentState)
            ctrlEmpBasicInfo.Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgHuFile_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHuFile.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow.GetDataKeyValue("ID"))) Then
                        e.Item.Selected = True
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgHuFile_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHuFile.NeedDataSource
        If EmployeeInfo IsNot Nothing Then
            Dim rep As New ProfileBusinessRepository
            Dim objHuFile As New HuFileDTO
            objHuFile.EMPLOYEE_ID = EmployeeInfo.ID
            SetValueObjectByRadGrid(rgHuFile, objHuFile)

            lstHuFile = rep.GetEmployeeHuFile(objHuFile)
            rgHuFile.DataSource = lstHuFile
        Else
            rgHuFile.DataSource = New List(Of HuFileDTO)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgHuFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgHuFile.SelectedIndexChanged
        Try
            If rgHuFile.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgHuFile.SelectedItems
                SelectedItem.Add(dr.GetDataKeyValue("ID"))
            Next

            Dim item As GridDataItem = rgHuFile.SelectedItems(0)
            hidHuFileID.Value = item.GetDataKeyValue("ID")
            txtFullName.Text = item.GetDataKeyValue("NAME")
            txtNumberCode.Text = item.GetDataKeyValue("NUMBER_CODE")
            txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            rdFromDate.SelectedDate = item.GetDataKeyValue("FROM_DATE")
            rdToDate.SelectedDate = item.GetDataKeyValue("TO_DATE")
            txtNote.Text = item.GetDataKeyValue("REMARK")
            txtFileNameVN.Text = item.GetDataKeyValue("FILENAME")
            txtFileNameSys.Value = item.GetDataKeyValue("FILENAME_SYS")
            txtSign.Text = item.GetDataKeyValue("SIGN_PERSON")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub cvalIDNO_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalIDNO.ServerValidate
    '    Try
    '        Dim rep As New ProfileBusinessRepository
    '        Dim _validate As New HuFileDTO

    '        'Nếu có Số CMND thì check trùng.
    '        If txtIDNO.Text.Trim() <> "" Then
    '            _validate.ID_NO = txtIDNO.Text.Trim()
    '            If hidHuFileID.Value <> "" Then
    '                _validate.ID = Decimal.Parse(hidHuFileID.Value)
    '            End If
    '            If (rep.ValidateHuFile(_validate)) Then
    '                args.IsValid = True
    '            Else
    '                args.IsValid = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteHuFile() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgHuFile.Rebind()
                    SelectedItem = Nothing
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objHuFile As New HuFileDTO
            Dim rep As New ProfileBusinessRepository
            Dim r As New ProfileRepository
            objHuFile.EMPLOYEE_ID = EmployeeInfo.ID

            objHuFile.NAME = txtFullName.Text.Trim()
            objHuFile.NUMBER_CODE = txtNumberCode.Text.Trim()
            objHuFile.ADDRESS = txtAdress.Text.Trim()
            objHuFile.FROM_DATE = rdFromDate.SelectedDate
            objHuFile.TO_DATE = rdToDate.SelectedDate
            objHuFile.REMARK = txtNote.Text.Trim()
            objHuFile.SIGN_PERSON = txtSign.Text
            Dim gID As Decimal
            If hidHuFileID.Value = "" Then
                If LicenseFile IsNot Nothing Then
                    Dim bytes() As Byte
                    If txtFileNameVN.Text <> "" Then
                        bytes = New Byte(LicenseFile.ContentLength) {}
                        LicenseFile.InputStream.Read(bytes, 0, bytes.Length)
                        'objHuFile.FILENAME_SYS = r.GenFilenameSys("ReportTemplates\Profile\File\HuFile", LicenseFile)
                        objHuFile.FILENAME = LicenseFile.FileName
                        objHuFile.FILENAME_SYS = LicenseFile.GetExtension()
                        txtFileNameSys.Value = LicenseFile.GetExtension()
                    Else
                        bytes = Nothing
                    End If
                    If Not (rep.InsertAttatch_Manager(objHuFile, bytes)) Then
                        Return False
                    End If
                End If

            Else
                objHuFile.ID = Decimal.Parse(hidHuFileID.Value)
                If LicenseFile IsNot Nothing Then
                    Dim bytes() As Byte = New Byte(LicenseFile.ContentLength) {}
                    LicenseFile.InputStream.Read(bytes, 0, bytes.Length)
                    ' objHuFile.FILENAME_SYS = r.GenFilenameSys("ReportTemplates\Profile\File\HuFile", LicenseFile)
                    objHuFile.FILENAME = LicenseFile.FileName
                    objHuFile.FILENAME_SYS = LicenseFile.GetExtension()
                    txtFileNameSys.Value = LicenseFile.GetExtension()
                    If Not rep.ModifyEmployeeHuFile(objHuFile, bytes) Then
                        Return False
                    End If
                End If
            End If
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Overrides Sub UpdateControlState()
        Try
            If CurrentState Is Nothing Then
                CurrentState = STATE_NORMAL
            End If
            Select Case CurrentState
                Case STATE_NEW
                    EnabledGrid(rgHuFile, False)
                    SetStatusControl(True)
                Case STATE_EDIT
                    EnabledGrid(rgHuFile, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgHuFile, True)
                    SetStatusControl(False)
            End Select
            Me.Send(CurrentState)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub SetStatusToolBar()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)
        txtFullName.ReadOnly = Not sTrangThai
        _radAsynceUpload1.Enabled = sTrangThai
        txtNumberCode.ReadOnly = Not sTrangThai
        txtAdress.ReadOnly = Not sTrangThai
        txtFullName.ReadOnly = Not sTrangThai
        txtNote.ReadOnly = Not sTrangThai
        txtSign.ReadOnly = Not sTrangThai
        Utilities.EnableRadDatePicker(rdFromDate, sTrangThai)
        Utilities.EnableRadDatePicker(rdToDate, sTrangThai)
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        ClearControlValue(txtFullName, txtNumberCode, txtAdress, txtFullName,
                          hidHuFileID, txtNote,
                          rdFromDate, rdToDate, txtFileNameVN, txtFileNameSys, txtFileNameDL, txtSign)
    End Sub

    Private Function DeleteHuFile() As Boolean
        Try
            Dim rep As New ProfileBusinessRepository
            Return rep.DeleteEmployeeHuFile(SelectedItem)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnSaveFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveFile.Click
        Try
            If _radAsynceUpload1.UploadedFiles.Count = 0 Then
                Exit Sub
            End If
            Dim file = _radAsynceUpload1.UploadedFiles(0)
            Dim fileContent As Byte() = New Byte(file.ContentLength) {}
            Using str As Stream = file.InputStream
                str.Read(fileContent, 0, file.ContentLength)
            End Using
            'Lưu file vào PageViewStates , khi insert vào Database mới lưu file này lên folder trên server.
            LicenseFile = file
            txtFileNameVN.Text = file.FileName
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnExportFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportFile.Click
        Try
            If hidHuFileID.Value IsNot Nothing Then
                Dim filePath = pathFile & "\" & hidHuFileID.Value & txtFileNameSys.Value
                Dim bts As Byte() = System.IO.File.ReadAllBytes(filePath)
                Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(txtFileNameDL.Value, "_"))
                Response.AddHeader("Content-Length", bts.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.BinaryWrite(bts)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownload.Click
        Try
            If hidHuFileID.Value IsNot Nothing Then
                Dim filePath = pathFile & "\" & hidHuFileID.Value & txtFileNameSys.Value
                Dim bts As Byte() = System.IO.File.ReadAllBytes(filePath)
                Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(txtFileNameDL.Value, "_"))
                Response.AddHeader("Content-Length", bts.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.BinaryWrite(bts)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class