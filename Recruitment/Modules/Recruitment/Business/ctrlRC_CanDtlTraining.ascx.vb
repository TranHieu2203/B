Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Ionic.Crc
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_CanDtlTraining
    Inherits CommonView
    'Dim _mylog As New MyLog()
    'Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()
    Dim checkCRUD As Integer = 0 '0-chua thao tac 1-Insert 2-Edit 3-Save 4-Delete

#Region "Property"
    Property checkClickUpload As Integer
        Get
            Return ViewState(Me.ID & "_checkClickUpload")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkClickUpload") = value
        End Set
    End Property

    'Property EmployeeInfo As EmployeeDTO
    '    Get
    '        Return PageViewState(Me.ID & "_EmployeeInfo")
    '    End Get
    '    Set(ByVal value As EmployeeDTO)
    '        PageViewState(Me.ID & "_EmployeeInfo") = value
    '    End Set
    'End Property

    'Public Property EmployeeTrain As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
    '    Get
    '        Return ViewState(Me.ID & "_EmployeeTrain")
    '    End Get
    '    Set(ByVal value As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO))
    '        ViewState(Me.ID & "_EmployeeTrain") = value
    '    End Set
    'End Property

    Property DeleteEmployeeTrain As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteEmployeeTrain")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteEmployeeTrain") = value
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

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
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

    Property EmployeeTrain As DataTable
        Get
            Return ViewState(Me.ID & "_EmployeeTrain")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_EmployeeTrain") = value
        End Set
    End Property
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property
#End Region

#Region "Page"

    Private Property ListComboData As ComboBoxDataDTO

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            'ctrlEmpBasicInfo
            hidCandidateCode.Value = Request.Params("Can")
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgEmployeeTrain.SetFilter()
            rgEmployeeTrain.AllowCustomPaging = True
            rgEmployeeTrain.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgEmployeeTrain)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <remarks></remarks>
    Private Sub btnUPLOAD_FILE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        checkClickUpload = 1
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf"
            ctrlUpload1.Show()

            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgEmployeeTrain.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, , rgEmployeeTrain.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.CurrentPageIndex = 0
                        rgEmployeeTrain.MasterTableView.SortExpressions.Clear()
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, )
                    Case "Cancel"
                        rdToiThang.SelectedDate = Nothing
                        rdTuThang.SelectedDate = Nothing
                        txtRemindLink.Text = ""
                        FileOldName = ""
                        rgEmployeeTrain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()
        Try
            Dim rep As New RecruitmentStoreProcedure
            Me.EmployeeTrain = rep.Get_candidate_trainning(hidCandidateCode.Value)
            rgEmployeeTrain.DataSource = Me.EmployeeTrain
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgEmployeeTrain, False)
                    EnableControlAll(True, rdToiThang, cboRemark, rdTuThang, rntGraduateYear, txtRemark, cboTrainingForm, txtChuyenNganh, txtKetQua, txtTrainingSchool, txtTrainingType, rdReceiveDegree, chkTerminate, cboLevelId, rtxtPointLevel, rtxtContentLevel, txtCertificateCode, txtNote)
                    EnabledGrid(rgEmployeeTrain, False)
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgEmployeeTrain, True)
                    EnableControlAll(False, rdTuThang, cboRemark, rdToiThang, rntGraduateYear, rdFrom, rdTo, txtRemark, cboTrainingForm, txtChuyenNganh, txtKetQua, txtTrainingSchool, txtTrainingType, rdReceiveDegree, chkTerminate, cboLevelId, rtxtPointLevel, rtxtContentLevel, txtCertificateCode, txtNote)
                    EnabledGrid(rgEmployeeTrain, True)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgEmployeeTrain, False)
                    EnableControlAll(True, rdTuThang, rdToiThang, rntGraduateYear, txtRemark, cboTrainingForm, cboRemark, txtChuyenNganh, txtKetQua, txtTrainingSchool, txtTrainingType, rdReceiveDegree, chkTerminate, cboLevelId, rtxtPointLevel, rtxtContentLevel, txtCertificateCode, txtNote)
                    EnabledGrid(rgEmployeeTrain, False)
                    If cboRemark.SelectedValue <> "" Then
                        If cboRemark.SelectedValue = 7086 Then
                            EnableControlAll(True, rdFrom, rdTo)
                            RequiredFieldValidator3.Visible = True
                            CompareValidator1.Visible = True
                            RequiredFieldValidator4.Visible = True
                        End If
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim rep As New RecruitmentStoreProcedure

                    'Dim rep As New ProfileBusinessRepository
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgEmployeeTrain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgEmployeeTrain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Dim re As Decimal = 0
                    If rep.delete_rc_candidate_trainning(lstDeletes, re) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            Me.Send(CurrentState)
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("FROM_DATE", rdTuThang)
        dic.Add("TO_DATE", rdToiThang)
        dic.Add("YEAR_GRA", rntGraduateYear)
        dic.Add("NAME_SHOOLS", txtTrainingSchool)
        dic.Add("FORM_TRAIN_ID", cboTrainingForm)
        dic.Add("SPECIALIZED_TRAIN", txtChuyenNganh)
        dic.Add("RESULT_TRAIN", txtKetQua)
        dic.Add("CERTIFICATE", cboRemark)
        dic.Add("EFFECTIVE_DATE_FROM", rdFrom)
        dic.Add("EFFECTIVE_DATE_TO", rdTo)
        dic.Add("UPLOAD_FILE", txtUploadFile)
        dic.Add("FILE_NAME", txtRemark)
        dic.Add("TYPE_TRAIN_NAME", txtTrainingType)
        dic.Add("RECEIVE_DEGREE_DATE", rdReceiveDegree)
        dic.Add("IS_RENEWED", chkTerminate)
        dic.Add("POINT_LEVEL", rtxtPointLevel)
        dic.Add("CONTENT_LEVEL", rtxtContentLevel)
        'dic.Add("RECEIVE_DEGREE_DATE", rdReceiveDegree)
        dic.Add("NOTE", txtNote)
        dic.Add("CERTIFICATE_CODE", txtCertificateCode)
        dic.Add("LEVEL_ID", cboLevelId)

        Utilities.OnClientRowSelectedChanged(rgEmployeeTrain, dic)
        Try
            Dim rep As New RecruitmentStoreProcedure
            Dim cb_data As New List(Of OtherListDTO)
            cb_data = rep.GetComboList("GET_LEVEL_TRAIN")
            If cb_data.Count > 0 Then
                FillDropDownList(cboLevelId, cb_data, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevelId.SelectedValue)
            End If
            cb_data = rep.GetComboList("GET_TRAINING_FORM")
            If cb_data.Count > 0 Then
                FillDropDownList(cboTrainingForm, cb_data, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingForm.SelectedValue)
            End If
            cb_data = rep.GetComboList("GET_CERTIFICATE_TYPE")
            If cb_data.Count > 0 Then
                FillDropDownList(cboRemark, cb_data, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingForm.SelectedValue)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ClearControlValue(rdToiThang, rdTuThang, cboTrainingForm, txtTrainingType, rntGraduateYear, txtRemark, txtTrainingSchool,
                                    cboRemark, txtChuyenNganh, txtKetQua, rdFrom, rdTo, rdReceiveDegree, cboLevelId, rtxtPointLevel, rtxtContentLevel, txtCertificateCode, txtNote)
                    chkTerminate.Checked = False
                    checkCRUD = 1
                    'If EmployeeInfo.WORK_STATUS Is Nothing Then 'Or
                    '    EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                    '     '(EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                    '    '(EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                    '    'EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date Then
                    CurrentState = CommonMessage.STATE_NEW
                    rgEmployeeTrain.Rebind()
                    'Else
                    '    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    Exit Sub
                    'End If
                Case CommonMessage.TOOLBARITEM_EDIT
                    checkCRUD = 2
                    If rgEmployeeTrain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgEmployeeTrain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'If EmployeeInfo.WORK_STATUS Is Nothing Then ' Or
                    '    '(EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                    '    '(EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                    '    '(EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                    '    'EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                    CurrentState = CommonMessage.STATE_EDIT
                    'Else
                    '    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    Exit Sub
                    'End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    checkCRUD = 4
                    If rgEmployeeTrain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If EmployeeInfo.WORK_STATUS Is Nothing Then ' Or
                    '    'EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                    '    '(EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                    '    '(EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                    '    'EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date Then
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.Show()
                    'Else
                    '    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    Exit Sub
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Utilities.GridExportExcel(rgEmployeeTrain, "EmployeeTrain")
                Case CommonMessage.TOOLBARITEM_SAVE
                    checkCRUD = 3
                    If Page.IsValid Then
                        Dim objTrain As New RC_CANDIDATE_TRAINNING_DTO

                        'Dim rep As New ProfileBusinessRepository
                        EnableControlAll(False, rdFrom, rdTo)
                        'objTrain.EMPLOYEE_ID = EmployeeInfo.ID
                        objTrain.CANDIDATE_ID = CandidateInfo.ID
                        objTrain.FROM_DATE = rdTuThang.SelectedDate
                        objTrain.TO_DATE = rdToiThang.SelectedDate
                        objTrain.NAME_SHOOLS = txtTrainingSchool.Text.Trim
                        If cboTrainingForm.SelectedValue = "" Then
                            objTrain.FORM_TRAIN_ID = Nothing
                        Else
                            objTrain.FORM_TRAIN_ID = cboTrainingForm.SelectedValue
                        End If

                        If cboRemark.SelectedValue = "" Then
                            objTrain.CERTIFICATE = Nothing
                        Else
                            objTrain.CERTIFICATE = cboRemark.SelectedValue
                        End If
                        objTrain.RECEIVE_DEGREE_DATE = rdReceiveDegree.SelectedDate
                        objTrain.YEAR_GRA = rntGraduateYear.Value
                        objTrain.SPECIALIZED_TRAIN = txtChuyenNganh.Text.Trim
                        objTrain.RESULT_TRAIN = txtKetQua.Text.Trim
                        objTrain.EFFECTIVE_DATE_FROM = rdFrom.SelectedDate
                        objTrain.EFFECTIVE_DATE_TO = rdTo.SelectedDate
                        objTrain.FILE_NAME = txtRemark.Text.Trim
                        objTrain.IS_RENEWED = chkTerminate.Checked

                        If cboLevelId.SelectedValue = "" Then
                            objTrain.LEVEL_ID = Nothing
                        Else
                            objTrain.LEVEL_ID = cboLevelId.SelectedValue
                        End If

                        objTrain.POINT_LEVEL = rtxtPointLevel.Text
                        objTrain.CONTENT_LEVEL = rtxtContentLevel.Text
                        objTrain.NOTE = txtNote.Text
                        objTrain.CERTIFICATE_CODE = txtCertificateCode.Text
                        objTrain.TYPE_TRAIN_NAME = txtTrainingType.Text

                        If Down_File = Nothing Then
                            objTrain.UPLOAD_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objTrain.UPLOAD_FILE = If(Down_File Is Nothing, "", Down_File.ToString)
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim rep As New RecruitmentStoreProcedure
                                If rep.insert_cadidate_trainning(objTrain) <> -1 Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    checkClickUpload = 1
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTrain.ID = hidSelectedtrainID.Value

                                Dim rep As New RecruitmentStoreProcedure
                                If rep.update_cadidate_trainning(objTrain) <> -1 Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTrain.ID
                                    Refresh("UpdateView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")

                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rdToiThang, rdTuThang, cboTrainingForm, txtTrainingType, rntGraduateYear, txtRemark, txtTrainingSchool,
                                    cboRemark, txtChuyenNganh, txtKetQua, rdFrom, rdTo, rdReceiveDegree, cboLevelId, rtxtPointLevel, rtxtContentLevel, txtCertificateCode, txtNote)
                    Refresh("Cancel")
            End Select

            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeTrain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Dim configPath As String = ConfigurationManager.AppSettings("PathCetificateFolder")
        Try
            If txtRemark.Text <> "" Then
                Dim strPath As String
                If txtRemindLink.Text IsNot Nothing Then
                    If txtRemindLink.Text <> "" Then
                        strPath = configPath + txtRemindLink.Text
                        bCheck = True
                    End If
                End If
                If Down_File <> "" Then
                    strPath = configPath + Down_File
                    bCheck = True
                End If
                If bCheck Then
                    ZipFiles(strPath)
                End If
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim crc As New CRC32()
            Dim fileNameZip As String = txtRemark.Text.Trim
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()


            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtRemark.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".pdf")
            listExtension.Add(".jpg")
            listExtension.Add(".png")
            Dim fileName As String
            Dim configPath As String = ConfigurationManager.AppSettings("PathCetificateFolder")
            Dim strPath As String = configPath
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then

                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtRemark.Text = file.FileName
                        'End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtRemark.Text)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            If strUpload <> "" Then
                txtRemark.Text = strUpload
                FileOldName = txtRemark.Text
                txtRemark.Text = strUpload
            Else
                strUpload = String.Empty
            End If

            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"
    Private Sub cvalToiThang_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalToiThang.ServerValidate
        Try
            If rdToiThang.SelectedDate IsNot Nothing Then
                If rdTuThang.SelectedDate > rdToiThang.SelectedDate Then
                    args.IsValid = False
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgEmployeeTrain.SelectedIndexChanged
        Dim dataItem = CType(rgEmployeeTrain.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            Dim r = EmployeeTrain.AsEnumerable.FirstOrDefault(Function(n) n.Field(Of Decimal)("ID") = Decimal.Parse(dataItem.GetDataKeyValue("ID")))
            hidSelectedtrainID.Value = r.Field(Of Decimal?)("ID")
            rdTuThang.SelectedDate = r("FROM_DATE")
            rdToiThang.SelectedDate = r.Field(Of Date?)("TO_DATE") 'dataItem.GetDataKeyValue("TO_DATE")
            rntGraduateYear.Value = r.Field(Of Decimal?)("YEAR_GRA")
            txtTrainingSchool.Text = r.Field(Of String)("NAME_SHOOLS")
            If r.Field(Of Decimal?)("FORM_TRAIN_ID") IsNot Nothing Then
                cboTrainingForm.SelectedValue = r.Field(Of Decimal?)("FORM_TRAIN_ID")
            End If
            txtChuyenNganh.Text = r.Field(Of String)("SPECIALIZED_TRAIN")
            txtTrainingType.Text = r.Field(Of String)("TYPE_TRAIN_NAME")
            txtKetQua.Text = r.Field(Of String)("RESULT_TRAIN")
            If r.Field(Of Decimal?)("CERTIFICATE_ID") IsNot Nothing Then
                cboRemark.SelectedValue = r.Field(Of Decimal?)("CERTIFICATE_ID")
            End If

            cboRemark.Text = r.Field(Of String)("CERTIFICATE")
            If r.Field(Of Date?)("RECEIVE_DEGREE_DATE") IsNot Nothing Then
                rdReceiveDegree.SelectedDate = r.Field(Of Date?)("RECEIVE_DEGREE_DATE")
            Else
                rdReceiveDegree.SelectedDate = Nothing
            End If

            rdFrom.SelectedDate = r.Field(Of Date?)("EFFECTIVE_DATE_FROM")
            rdTo.SelectedDate = r.Field(Of Date?)("EFFECTIVE_DATE_TO")
            txtRemindLink.Text = r.Field(Of String)("UPLOAD_FILE") 'dataItem.GetDataKeyValue("UPLOAD_FILE")
            txtRemark.Text = r.Field(Of String)("FILE_NAME") 'dataItem.GetDataKeyValue("FILE_NAME")
            If r.Field(Of Decimal?)("IS_RENEWED") IsNot Nothing Then
                chkTerminate.Checked = r.Field(Of Decimal?)("IS_RENEWED")
            End If
            If r.Field(Of Decimal?)("LEVEL_ID") IsNot Nothing Then
                cboLevelId.SelectedValue = r.Field(Of Decimal?)("LEVEL_ID")
            End If

            rtxtPointLevel.Text = r.Field(Of String)("POINT_LEVEL") 'dataItem.GetDataKeyValue("POINT_LEVEL")
            rtxtContentLevel.Text = r.Field(Of String)("CONTENT_LEVEL") 'dataItem.GetDataKeyValue("CONTENT_LEVEL")
            txtNote.Text = r.Field(Of String)("NOTE")
            txtCertificateCode.Text = r.Field(Of String)("CERTIFICATE_CODE") 'dataItem.GetDataKeyValue("CERTIFICATE_CODE")
            EnableControlAll(False, rdFrom, rdTo)
        End If
    End Sub

    Protected Sub cboRemark_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboRemark.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        rdFrom.SelectedDate = Nothing
        rdTo.SelectedDate = Nothing
        Try
            If cboRemark.SelectedItem.Text = "Chứng chỉ" Then
                EnableControlAll(True, rdFrom, rdTo)
                RequiredFieldValidator3.Visible = True
                CompareValidator1.Visible = True
                RequiredFieldValidator4.Visible = True
            Else
                EnableControlAll(False, rdFrom, rdTo)
                RequiredFieldValidator3.Visible = False
                CompareValidator1.Visible = False
                RequiredFieldValidator4.Visible = False
                rdFrom.ClearValue()
                rdTo.ClearValue()
            End If

            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rdFrom_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdFrom.SelectedDateChanged
        cboRemark.SelectedValue = 7086
    End Sub
End Class