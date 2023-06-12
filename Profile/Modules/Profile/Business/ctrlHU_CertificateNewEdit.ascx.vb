Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_CertificateNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
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

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Dim IdSelect As Decimal

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

    Property Is_dis As String
        Get
            Return ViewState(Me.ID & "_Is_dis")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Is_dis") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarContract
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim objTrain = rep.GetCertificateById(IdSelect)
                    If objTrain IsNot Nothing Then
                        hidEmp_ID.Value = objTrain.EMPLOYEE_ID

                        txtEmployeeCode.Text = objTrain.EMPLOYEE_CODE

                        txtEmployeeName.Text = objTrain.EMPLOYEE_NAME

                        txtTITLE.Text = objTrain.TITLE_NAME

                        txtOrg_Name.Text = objTrain.ORG_NAME

                        txtCertificateName.Text = objTrain.CERTIFICATE_NAME

                        rdFROM_DATE.SelectedDate = objTrain.FROM_DATE

                        rdTO_DATE.SelectedDate = objTrain.TO_DATE

                        If IsNumeric(objTrain.LEVEL_ID) Then
                            cboLevel.SelectedValue = objTrain.LEVEL_ID
                        End If

                        If IsNumeric(objTrain.POINT_LEVEL) Then
                            rnScore.Value = objTrain.POINT_LEVEL
                        End If

                        txtContent.Text = objTrain.CONTENT_LEVEL

                        txtSpecializedTrain.Text = objTrain.SPECIALIZED_TRAIN

                        If IsNumeric(objTrain.YEAR_GRA) Then
                            rntGraduateYear.Value = objTrain.YEAR_GRA
                        End If

                        txtResult.Text = objTrain.RESULT_TRAIN

                        If IsDate(objTrain.EFFECTIVE_DATE_FROM) Then
                            rdEffectDate.SelectedDate = objTrain.EFFECTIVE_DATE_FROM
                        End If

                        If IsDate(objTrain.EFFECTIVE_DATE_TO) Then
                            rdExpireDate.SelectedDate = objTrain.EFFECTIVE_DATE_TO
                        End If

                        If IsNumeric(objTrain.FORM_TRAIN_ID) Then
                            cboTrainingForm.SelectedValue = objTrain.FORM_TRAIN_ID
                        End If

                        'If IsNumeric(objTrain.TRAIN_PLACE) Then
                        '    cboTrainPlace.SelectedValue = objTrain.TRAIN_PLACE
                        'End If

                        If IsNumeric(objTrain.CERTIFICATE_ID) Then
                            cboCertificate.SelectedValue = objTrain.CERTIFICATE_ID
                        End If

                        If IsNumeric(objTrain.CERTIFICATE_GROUP_ID) Then
                            cboCertificateGroup.SelectedValue = objTrain.CERTIFICATE_GROUP_ID
                        End If

                        GetCertificateType()
                        If IsNumeric(objTrain.CERTIFICATE_TYPE_ID) Then
                            cboCertificateType.SelectedValue = objTrain.CERTIFICATE_TYPE_ID
                        End If

                        If IsNumeric(objTrain.MAJOR) Then
                            cboMajor1.SelectedValue = objTrain.MAJOR
                            'chkIsMajor1.Enabled = True
                        End If

                        If IsNumeric(objTrain.GRADUATE_SCHOOL) Then
                            cboSchool1.SelectedValue = objTrain.GRADUATE_SCHOOL
                        End If

                        'chkIsMajor1.Checked = objTrain.IS_MAJOR

                        txtUploadFile.Text = objTrain.UPLOAD_FILE

                        txtUpload.Text = objTrain.FILE_NAME

                        txtRemark.Text = objTrain.NOTE

                        If objTrain.IS_MAIN IsNot Nothing Then
                            chkIS_MAIN.Checked = objTrain.IS_MAIN
                        End If

                        If txtUpload.Text <> "" Then
                            btnDownload.Visible = True
                            If txtUpload.Text.ToUpper.Contains(".JPG") Or txtUpload.Text.ToUpper.Contains(".GIF") Or txtUpload.Text.ToUpper.Contains(".PNG") Then
                                btnView.Visible = True
                                Dim file = rep.GetFileForView(txtUploadFile.Text)
                                Dim link As String = file.LINK & "\" & file.FILE_NAME
                                link = link.Replace("\", "(slash)")
                                hidLink.Value = link
                            Else
                                btnView.Visible = False
                            End If
                        Else
                            btnDownload.Visible = False
                        End If

                        If IsNumeric(cboCertificate.SelectedValue) Then
                            If Decimal.Parse(cboCertificate.SelectedValue) = 7085 Then
                                SetDisplayControl("BANG_CAP")
                            End If
                            If Decimal.Parse(cboCertificate.SelectedValue) = 7086 Then
                                SetDisplayControl("CHUNG_CHI")
                            End If
                        End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    cboCertificate.SelectedValue = 7085
                    SetDisplayControl("BANG_CAP")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()

                    If Page.IsValid Then
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        Dim objTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO

                        objTrain.EMPLOYEE_ID = hidEmp_ID.Value

                        objTrain.FROM_DATE = rdFROM_DATE.SelectedDate

                        objTrain.TO_DATE = rdTO_DATE.SelectedDate

                        objTrain.IS_MAIN = chkIS_MAIN.Checked

                        If cboTrainingForm.SelectedValue <> "" Then
                            objTrain.FORM_TRAIN_ID = cboTrainingForm.SelectedValue
                        End If

                        If cboMajor1.SelectedValue <> "" Then
                            objTrain.MAJOR = cboMajor1.SelectedValue
                        End If

                        If cboSchool1.SelectedValue <> "" Then
                            objTrain.GRADUATE_SCHOOL = cboSchool1.SelectedValue
                        End If

                        'objTrain.IS_MAJOR = chkIsMajor1.Checked

                        If cboCertificate.SelectedValue <> "" Then
                            objTrain.CERTIFICATE_ID = cboCertificate.SelectedValue
                        End If

                        If cboCertificateGroup.SelectedValue <> "" Then
                            objTrain.CERTIFICATE_GROUP_ID = cboCertificateGroup.SelectedValue
                        End If

                        If cboCertificateType.SelectedValue <> "" Then
                            objTrain.CERTIFICATE_TYPE_ID = cboCertificateType.SelectedValue
                        End If

                        If IsNumeric(rntGraduateYear.Value) Then
                            objTrain.YEAR_GRA = rntGraduateYear.Value
                        End If

                        objTrain.SPECIALIZED_TRAIN = txtSpecializedTrain.Text.Trim

                        objTrain.RESULT_TRAIN = txtResult.Text.Trim

                        If IsDate(rdEffectDate.SelectedDate) Then
                            objTrain.EFFECTIVE_DATE_FROM = rdEffectDate.SelectedDate
                        End If

                        If IsDate(rdExpireDate.SelectedDate) Then
                            objTrain.EFFECTIVE_DATE_TO = rdExpireDate.SelectedDate
                        End If

                        objTrain.FILE_NAME = txtUploadFile.Text

                        If cboLevel.SelectedValue = "" Then
                            objTrain.LEVEL_ID = Nothing
                        Else
                            objTrain.LEVEL_ID = cboLevel.SelectedValue
                        End If

                        If IsNumeric(rnScore.Value) Then
                            objTrain.POINT_LEVEL = rnScore.Value
                        End If

                        objTrain.CONTENT_LEVEL = txtContent.Text

                        objTrain.NOTE = txtRemark.Text

                        objTrain.CERTIFICATE_NAME = txtCertificateName.Text.Trim

                        'If cboTrainPlace.SelectedValue <> "" Then
                        '    objTrain.TRAIN_PLACE = cboTrainPlace.SelectedValue
                        'End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objTrain.ID = 0

                                If chkIS_MAIN.Checked Then
                                    If rep.CheckExistEmployeeCertificate_IsMain(objTrain) Then
                                        ShowMessage("Nhân viên đã có dữ liệu bằng chính", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                'If chkIsMajor1.Checked Then
                                '    If rep.CheckExistEmployeeCertificate_IsMajor(objTrain) Then
                                '        ShowMessage("Nhân viên đã có dữ liệu chuyên môn cao nhất", NotifyType.Warning)
                                '        Exit Sub
                                '    End If
                                'End If

                                If rep.InsertProcessTraining(objTrain, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objTrain.ID = Request.Params("ID")

                                If chkIS_MAIN.Checked Then
                                    If rep.CheckExistEmployeeCertificate_IsMain(objTrain) Then
                                        ShowMessage("Nhân viên đã có dữ liệu bằng chính", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                'If chkIsMajor1.Checked Then
                                '    If rep.CheckExistEmployeeCertificate_IsMajor(objTrain) Then
                                '        ShowMessage("Nhân viên đã có dữ liệu chuyên môn cao nhất", NotifyType.Warning)
                                '        Exit Sub
                                '    End If
                                'End If

                                If rep.ModifyProcessTraining(objTrain, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Dim str As String = "document.getElementsByClassName('rtbBtn')[0].style.pointerEvents = 'auto';"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                hidEmp_ID.Value = lstEmpID(0)
                FillData(lstEmpID(0))
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Dim configPath As String = ConfigurationManager.AppSettings("PathCetificateFolder")
        Try
            If txtUpload.Text <> "" Then
                Dim fileObj As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    fileObj = rep.GetFileForView(txtUploadFile.Text)
                End Using
                Dim link = fileObj.LINK
                Dim name = fileObj.FILE_NAME
                Dim path = link & name
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
                Response.WriteFile(file.FullName)
                Response.End()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    Dim fileContent As Byte() = New Byte(file.ContentLength) {}
                    Dim buffer As Byte() = New Byte(file.ContentLength - 1) {}
                    Using str As Stream = file.InputStream
                        str.Read(buffer, 0, buffer.Length)
                    End Using
                    Dim guidID = Guid.NewGuid.ToString()
                    Dim obj As New FileUploadDTO
                    obj.FILE_NAME = file.FileName
                    obj.CODE_PATH = "EMPDTLTRAINNING"
                    obj.NAME = guidID
                    If Not rep.AddFileUpload(obj, buffer) Then
                        txtUpload.Text = file.FileName
                        txtUploadFile.Text = obj.NAME
                        btnDownload.Visible = True
                        If obj.FILE_NAME.ToUpper.Contains(".JPG") Or obj.FILE_NAME.ToUpper.Contains(".GIF") Or obj.FILE_NAME.ToUpper.Contains(".PNG") Then
                            btnView.Visible = True
                            Dim fileObj = rep.GetFileForView(txtUploadFile.Text)
                            Dim link As String = fileObj.LINK & "\" & fileObj.FILE_NAME
                            link = link.Replace("\", "(slash)")
                            hidLink.Value = link
                        Else
                            btnView.Visible = False
                        End If
                    End If
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Dim rep_PROFILE As New ProfileBusinessRepository
        Try
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0).EMPLOYEE_ID
                        hidEmp_ID.Value = empID
                        FillData(empID)
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)

                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = True
                            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Reset_Find_Emp()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            hidEmp_ID.Value = Nothing
            txtEmployeeName.Text = ""
            txtTITLE.Text = ""
            txtOrg_Name.Text = ""
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadPopup(isLoadPopup)
            If txtUpload.Text <> "" Then
                btnDownload.Visible = True
                If txtUpload.Text.ToUpper.Contains(".JPG") Or txtUpload.Text.ToUpper.Contains(".GIF") Or txtUpload.Text.ToUpper.Contains(".PNG") Then
                    btnView.Visible = True
                Else
                    btnView.Visible = False
                End If
            Else
                btnDownload.Visible = False
                btnView.Visible = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        ListComboData = New ComboBoxDataDTO
        ListComboData.GET_CERTIFICATE_TYPE = True
        ListComboData.GET_TRAINING_FORM = True
        ListComboData.GET_LEVEL_TRAIN = True
        ListComboData.GET_TDCM = True
        ListComboData.GET_TDT = True
        rep.GetComboList(ListComboData)
        FillDropDownList(cboCertificate, ListComboData.LIST_CERTIFICATE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        FillDropDownList(cboLevel, ListComboData.LIST_LEVEL_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        FillDropDownList(cboTrainingForm, ListComboData.LIST_TRAINING_FORM, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        FillDropDownList(cboMajor1, ListComboData.LIST_TDCM, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        FillDropDownList(cboSchool1, ListComboData.LIST_TDT, "NAME_VN", "ID", Common.Common.SystemLanguage, False)

        Dim dtData As DataTable
        'dtData = rep.GetOtherList("NOIDAOTAO", True)
        'FillRadCombobox(cboTrainPlace, dtData, "NAME", "ID")
        dtData = rep.GetOtherList("TR_CER_GROUP", True)
        FillRadCombobox(cboCertificateGroup, dtData, "NAME", "ID")

        rep.Dispose()
    End Sub

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IdSelect = Request.Params("ID")
                    Refresh("UpdateView")
                Else
                    Refresh("NormalView")
                End If
                If Request.Params("EmpID") IsNot Nothing Then
                    FillData(Request.Params("EmpID"))
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileBusinessRepository
                Dim item = rep.GetContractEmployeeByID(empID)
                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    MainToolBar.Items(0).Enabled = False
                Else
                    MainToolBar.Items(0).Enabled = True
                End If
                hidEmp_ID.Value = empID
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTITLE.Text = item.TITLE_NAME_VN
                txtOrg_Name.Text = item.ORG_NAME
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Private Sub LoadPopup(ByVal popupType As Int32)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case popupType
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    'Private Sub cboMajor1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboMajor1.SelectedIndexChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Try
    '        If cboMajor1.SelectedValue <> "" Then
    '            chkIsMajor1.Enabled = True
    '        Else
    '            chkIsMajor1.Checked = False
    '            chkIsMajor1.Enabled = False
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(ViewName, ID, ex)
    '    End Try
    'End Sub

    Private Sub GetCertificateType()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        ListComboData = New ComboBoxDataDTO
        Dim dtData As New DataTable
        Try
            ClearControlValue(cboCertificateType)
            If IsNumeric(cboCertificateGroup.SelectedValue) Then
                If Decimal.Parse(cboCertificateGroup.SelectedValue) = 789068 Then
                    dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                    FillRadCombobox(cboCertificateType, dtData, "NAME", "ID")
                ElseIf Decimal.Parse(cboCertificateGroup.SelectedValue) = 789067 Then
                    dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
                    FillRadCombobox(cboCertificateType, dtData, "NAME", "ID")
                Else
                    dtData = rep.GetOtherList("CERTIFICATE_TYPE_2", True)
                    FillRadCombobox(cboCertificateType, dtData, "NAME", "ID")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Protected Sub cboCertificateGroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboCertificateGroup.SelectedIndexChanged
        GetCertificateType()
    End Sub

    Protected Sub cboCertificate_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboCertificate.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Try
            ClearControlValue(chkIS_MAIN, cboCertificateGroup, cboCertificateType, txtCertificateName, rdFROM_DATE,
                              rdTO_DATE, cboLevel, txtSpecializedTrain, cboMajor1, txtContent, cboSchool1,
                              rntGraduateYear, rnScore, txtResult, cboTrainingForm, rdEffectDate, rdExpireDate, txtUpload,
                              txtUploadFile, txtRemark, txtRemindLink, hidLink)
            btnDownload.Visible = False
            btnView.Visible = False

            If IsNumeric(cboCertificate.SelectedValue) Then
                If Decimal.Parse(cboCertificate.SelectedValue) = 7085 Then
                    SetDisplayControl("BANG_CAP")
                End If
                If Decimal.Parse(cboCertificate.SelectedValue) = 7086 Then
                    SetDisplayControl("CHUNG_CHI")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Private Sub SetDisplayControl(ByVal Type As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetDefaultDisplayControl()
            Select Case Type
                Case "BANG_CAP"
                    SetDisplayControl_BANG_CAP()
                Case "CHUNG_CHI"
                    SetDisplayControl_CHUNG_CHI()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Private Sub SetDefaultDisplayControl()
        tr_CertificateGroup_CertificateType.Style.Remove("display")
        tr_EffectDate_ExpireDate.Style.Remove("display")
        td_Label_IS_MAIN.Style.Remove("display")
        td_CheckBox_IS_MAIN.Style.Remove("display")
        tr_Frommonth_Tomonth.Style.Remove("display")
        tr_Level_SpecializedTrain.Style.Remove("display")
        tr_Major1_IsMajor1.Style.Remove("display")
        tr_School1.Style.Remove("display")
        td_Label_GraduateYear.Style.Remove("display")
        td_RadNumericTextBox_GraduateYear.Style.Remove("display")
        tr_Result_TrainingForm.Style.Remove("display")

        Required_cboCertificateGroup.Enabled = True
        Required_cboCertificateType.Enabled = True
        Required_rdEffectDate.Enabled = True
        Required_cboLevel.Enabled = True
        Required_txtSpecializedTrain.Enabled = True
        Required_cboMajor1.Enabled = True
        Required_cboSchool1.Enabled = True
        Required_rntGraduateYear.Enabled = True
    End Sub

    Private Sub SetDisplayControl_BANG_CAP()
        tr_CertificateGroup_CertificateType.Style.Add("display", "none")
        tr_EffectDate_ExpireDate.Style.Add("display", "none")

        Required_cboCertificateGroup.Enabled = False
        Required_cboCertificateType.Enabled = False
        Required_rdEffectDate.Enabled = False
    End Sub

    Private Sub SetDisplayControl_CHUNG_CHI()
        td_Label_IS_MAIN.Style.Add("display", "none")
        td_CheckBox_IS_MAIN.Style.Add("display", "none")
        tr_Frommonth_Tomonth.Style.Add("display", "none")
        tr_Level_SpecializedTrain.Style.Add("display", "none")
        tr_Major1_IsMajor1.Style.Add("display", "none")
        tr_School1.Style.Add("display", "none")
        td_Label_GraduateYear.Style.Add("display", "none")
        td_RadNumericTextBox_GraduateYear.Style.Add("display", "none")
        tr_Result_TrainingForm.Style.Add("display", "none")

        Required_cboLevel.Enabled = False
        Required_txtSpecializedTrain.Enabled = False
        Required_cboMajor1.Enabled = False
        Required_cboSchool1.Enabled = False
        Required_rntGraduateYear.Enabled = False
    End Sub

    Private Sub CusVal_txtCertificateName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CusVal_txtCertificateName.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsNumeric(cboCertificate.SelectedValue) Then
                If Decimal.Parse(cboCertificate.SelectedValue) = 7085 Then
                    If txtCertificateName.Text <> "" Then
                        args.IsValid = True
                        Exit Sub
                    End If
                End If
                If Decimal.Parse(cboCertificate.SelectedValue) = 7086 Then
                    Dim listID As New List(Of Decimal)({789069, 789070, 789071})
                    If IsNumeric(cboCertificateType.SelectedValue) Then
                        If listID.Contains(Decimal.Parse(cboCertificateType.SelectedValue)) Then
                            If txtCertificateName.Text <> "" Then
                                args.IsValid = True
                                Exit Sub
                            End If
                        Else
                            args.IsValid = True
                            Exit Sub
                        End If
                    End If
                End If
            End If
            args.IsValid = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub
#End Region

End Class