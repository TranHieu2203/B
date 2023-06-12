Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile
Imports Profile.ProfileBusiness
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_RequestNewEdit_TBP
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployee2GridPopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Recruitment\Modules\Recruitment\Business" + Me.GetType().Name.ToString()
    Private rep As New HistaffFrameworkRepository
    Private store As New RecruitmentStoreProcedure()

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isPerson_PT As Integer
        Get
            Return ViewState(Me.ID & "_isPerson_PT")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isPerson_PT") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details

    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Dim IS_CLOCK As Decimal?
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property

    Property DAYNUM As Decimal
        Get
            Return ViewState(Me.ID & "_DAYNUM")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_DAYNUM") = value
        End Set
    End Property

    Property REQUIRER_EMP_ID As Decimal
        Get
            Return ViewState(Me.ID & "_REQUIRER_EMP_ID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_REQUIRER_EMP_ID") = value
        End Set
    End Property

    Property PERSON_PT_RC_EMP_ID As Decimal
        Get
            Return ViewState(Me.ID & "_PERSON_PT_RC_EMP_ID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_PERSON_PT_RC_EMP_ID") = value
        End Set
    End Property

    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
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

    Property IsVuotDinhBien As Boolean
        Get
            Return ViewState(Me.ID & "_IsVuotDinhBien")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsVuotDinhBien") = value
        End Set
    End Property

    Public Property Employee_list As List(Of CommonBusiness.EmployeeDTO)
        Get
            Return PageViewState(Me.ID & "_Employee_list")
        End Get
        Set(value As List(Of CommonBusiness.EmployeeDTO))
            PageViewState(Me.ID & "_Employee_list") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
            SetVisibleFileAttach()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgEmployee.SetFilter()
            rgEmployee.AllowCustomPaging = True
            rgEmployee.PageSize = Common.Common.DefaultPageSize

            TuyenThayThe.Visible = False
            RgTuyenThayThe.Visible = False
            isPerson_PT = 0
            Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator, ToolbarItem.Export)
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(3), RadToolBarButton).Enabled = True
            'CType(MainToolBar.Items(3), RadToolBarButton).Text = "Xuất tờ trình"
            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            chkIn.Checked = True
            IsVuotDinhBien = False
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = IDSelect})
                    hidOrgID.Value = obj.ORG_ID.ToString()
                    txtOrgName.Text = obj.ORG_NAME
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If
                    If obj.IS_IN_PLAN = 0 Then
                        chkOut.Checked = True
                        chkIn.Checked = False
                    End If

                    If obj.IS_IN_PLAN = 1 Then
                        chkIn.Checked = True
                        chkOut.Checked = False
                    End If

                    txtCode_YCTD.Text = obj.CODE_RC
                    txtName_YCTD.Text = obj.NAME_RC

                    LoadComboTitle()

                    cboTitle.SelectedValue = obj.TITLE_ID

                    txtGroupPos.Text = obj.RC_GRPOS

                    rdSendDate.SelectedDate = obj.SEND_DATE

                    rntxtSalary.Text = obj.SAL_OFFER.ToString
                    chkRequestComputer.Checked = obj.IS_REQUEST_COMPUTER
                    txtQualificationOthers.Text = obj.OTHER_QUALIFICATION

                    If obj.CONTRACT_TYPE_ID IsNot Nothing Then
                        cboContractType.SelectedValue = obj.CONTRACT_TYPE_ID
                    End If
                    If obj.LOCATION_ID IsNot Nothing Then
                        cbolocationWork.SelectedValue = obj.LOCATION_ID
                    End If

                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                    If Decimal.Parse(cboRecruitReason.SelectedValue) = 4053 Then
                        TuyenThayThe.Visible = True
                        RgTuyenThayThe.Visible = True
                    Else
                        TuyenThayThe.Visible = False
                        RgTuyenThayThe.Visible = False
                    End If
                    txtRecruitReason.Text = obj.RECRUIT_REASON
                    txtPersonRequest.Text = obj.REQUIRER_NAME
                    REQUIRER_EMP_ID = obj.REQUIRER
                    txtPersonRe_TCTD.Text = obj.PERSON_PT_RC_NAME
                    PERSON_PT_RC_EMP_ID = obj.PERSON_PT_RC
                    If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                        cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                    End If
                    If obj.WORKING_PLACE_ID IsNot Nothing Then
                        cboWorkingPlace.SelectedValue = obj.WORKING_PLACE_ID
                    End If
                    If obj.AGE_FROM IsNot Nothing Then
                        rntxtAgeFrom.Value = obj.AGE_FROM
                    End If
                    If obj.AGE_TO IsNot Nothing Then
                        rntxtAgeTo.Value = obj.AGE_TO
                    End If
                    If obj.QUALIFICATION IsNot Nothing Then
                        cboQualification.SelectedValue = obj.QUALIFICATION
                    End If
                    If obj.SPECIALSKILLS IsNot Nothing Then
                        cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                    End If
                    If obj.LANGUAGE IsNot Nothing Then
                        cboLanguage.SelectedValue = obj.LANGUAGE
                    End If
                    If obj.LANGUAGELEVEL IsNot Nothing Then
                        cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                    End If

                    txtScores.Text = If(obj.LANGUAGESCORES Is Nothing, String.Empty, obj.LANGUAGESCORES)
                    rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                    rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                    If obj.COMPUTER_LEVEL IsNot Nothing Then
                        cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                    End If
                    txtMainTask.Text = obj.MAINTASK
                    txtRequestExperience.Text = obj.REQUEST_EXPERIENCE

                    If obj.STATUS_ID = 4101 Then
                        ChkIs_Approve.Checked = True
                    Else
                        ChkIs_Approve.Checked = False
                    End If

                    rntxtRecruitNumber.Value = obj.RECRUIT_NUMBER
                    If obj.RC_RECRUIT_PROPERTY IsNot Nothing Then
                        cboRecruitProperty.SelectedValue = obj.RC_RECRUIT_PROPERTY
                    End If
                    If obj.GENDER_PRIORITY IsNot Nothing Then
                        cboGenderPriority.SelectedValue = obj.GENDER_PRIORITY
                    End If
                    chkIsOver.Checked = If(obj.IS_OVER_LIMIT Is Nothing, False, obj.IS_OVER_LIMIT)

                    txtForeignAbility.Text = obj.FOREIGN_ABILITY
                    txtComputerAppLevel.Text = obj.COMPUTER_APP_LEVEL

                    txtUpload.Text = obj.FILE_NAME
                    txtUploadFile.Text = obj.UPLOAD_FILE

                    txtDescription.Text = obj.DESCRIPTION
                    txtRemark.Text = obj.REMARK
                    txtRequestOther.Text = obj.REQUEST_OTHER
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    hddFile.Value = obj.DESCRIPTIONATTACHFILE
                    hypFile.Text = obj.DESCRIPTIONATTACHFILE
                    hypFile.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Recruitment/Upload/" + obj.DESCRIPTIONATTACHFILE

                    If Employee_list Is Nothing Then
                        Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                    End If
                    For Each itm In obj.lstEmp
                        Dim item As New CommonBusiness.EmployeeDTO
                        'item.Value = itm.EMPLOYEE_ID
                        'item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        'lstEmployee.Items.Add(item)
                        item.ID = itm.EMPLOYEE_ID
                        item.EMPLOYEE_CODE = itm.EMPLOYEE_CODE
                        item.FULLNAME_VN = itm.EMPLOYEE_NAME
                        item.ORG_NAME = itm.ORG_NAME
                        item.TITLE_NAME_VN = itm.TITLE_NAME
                        item.JOIN_DATE = itm.JOIN_DATE
                        Employee_list.Add(item)
                    Next
                    rgEmployee.Rebind()
                    GetTotalEmployeeByTitleID()

                    If chkIsOver.Checked = False And chkIn.Checked Then
                        CheckNumRecruit()
                    End If

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW

                    rdSendDate.AutoPostBack = True

                    Me.MainToolBar = tbarMain
                    'CType(MainToolBar.Items(3), RadToolBarButton).Enabled = False
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim objRequest As New RequestDTO
                        Dim lstEmp As New List(Of RequestEmpDTO)

                        'objRequest.IS_IN_PLAN = chkIsInPlan.Checked
                        'If objRequest.IS_IN_PLAN Then
                        '    objRequest.RC_PLAN_ID = cboTitle.SelectedValue
                        '    Dim dt As DataTable = store.PLAN_GET_BY_ID(objRequest.RC_PLAN_ID)
                        '    If dt.Rows.Count > 0 Then
                        '        objRequest.TITLE_ID = Decimal.Parse(dt.Rows(0)("TITLE_ID").ToString())
                        '    End If
                        'Else
                        '    objRequest.TITLE_ID = cboTitle.SelectedValue
                        'End If
                        If rntxtRecruitNumber.Value = 0 Then
                            ShowMessage(Translate("SL cần tuyển phải lớn hơn 0!"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If IsVuotDinhBien Then
                            If chkIsOver.Checked = False Then
                                ShowMessage(Translate("SL cần tuyển vượt định biên!"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        objRequest.CODE_RC = txtCode_YCTD.Text
                        objRequest.NAME_RC = txtName_YCTD.Text

                        'Trong định biên or Ngoai định biên
                        If chkIn.Checked Then
                            objRequest.IS_IN_PLAN = 1
                        Else
                            objRequest.IS_IN_PLAN = 0
                        End If

                        objRequest.ORG_ID = hidOrgID.Value

                        'Vị trí tuyển dụng
                        objRequest.TITLE_ID = cboTitle.SelectedValue

                        'Nhóm vị trí TD
                        objRequest.RC_GRPOS = txtGroupPos.Text

                        'Người yêu cầu
                        objRequest.REQUIRER = REQUIRER_EMP_ID

                        'mức lương
                        objRequest.SAL_OFFER = rntxtSalary.Value

                        'yêu cầu máy tính
                        objRequest.IS_REQUEST_COMPUTER = chkRequestComputer.Checked

                        'chuyên môn khác 
                        objRequest.OTHER_QUALIFICATION = txtQualificationOthers.Text

                        'người phụ trách YCTD
                        objRequest.PERSON_PT_RC = PERSON_PT_RC_EMP_ID

                        'phê duyệt 
                        If ChkIs_Approve.Checked Then
                            objRequest.STATUS_ID = 4101
                        Else
                            objRequest.STATUS_ID = 4100
                        End If
                        If cboWorkingPlace.SelectedValue <> "" Then
                            objRequest.WORKING_PLACE_ID = Decimal.Parse(cboWorkingPlace.SelectedValue)
                        End If

                        objRequest.SEND_DATE = rdSendDate.SelectedDate
                        'Hình thức tuyển dụng
                        If cboContractType.SelectedValue <> "" Then
                            objRequest.CONTRACT_TYPE_ID = cboContractType.SelectedValue
                        End If
                        objRequest.RECRUIT_REASON = txtRecruitReason.Text
                        If cboLearningLevel.SelectedValue <> "" Then
                            objRequest.LEARNING_LEVEL_ID = cboLearningLevel.SelectedValue
                        End If
                        objRequest.AGE_FROM = rntxtAgeFrom.Value
                        objRequest.AGE_TO = rntxtAgeTo.Value
                        objRequest.QUALIFICATION = cboQualification.SelectedValue

                        If cboRecruitProperty.SelectedValue <> "" Then
                            objRequest.RC_RECRUIT_PROPERTY = cboRecruitProperty.SelectedValue
                        End If
                        If cbolocationWork.SelectedValue <> "" Then
                            objRequest.LOCATION_ID = cbolocationWork.SelectedValue
                        End If

                        objRequest.IS_OVER_LIMIT = chkIsOver.Checked
                        'objRequest.IS_SUPPORT = chkIsSupport.Checked
                        objRequest.FOREIGN_ABILITY = txtForeignAbility.Text
                        objRequest.COMPUTER_APP_LEVEL = txtComputerAppLevel.Text
                        If cboGenderPriority.SelectedValue <> "" Then
                            objRequest.GENDER_PRIORITY = cboGenderPriority.SelectedValue
                        End If
                        objRequest.RECRUIT_NUMBER = rntxtRecruitNumber.Value

                        objRequest.FILE_NAME = txtUpload.Text.Trim
                        objRequest.UPLOAD_FILE = txtUploadFile.Text.Trim

                        objRequest.DESCRIPTION = txtDescription.Text
                        objRequest.EXPERIENCE_NUMBER = rntxtExperienceNumber.Value
                        'objRequest.FEMALE_NUMBER = rntxtFemaleNumber.Value


                        'objRequest.MALE_NUMBER = rntxtMaleNumber.Value

                        objRequest.REQUEST_EXPERIENCE = txtRequestExperience.Text
                        objRequest.REQUEST_OTHER = txtRequestOther.Text

                        objRequest.EXPECTED_JOIN_DATE = rdExpectedJoinDate.SelectedDate
                        If cboRecruitReason.SelectedValue <> "" Then
                            objRequest.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If
                        'For Each item As RadListBoxItem In lstEmployee.Items
                        '    Dim emp As New RequestEmpDTO
                        '    emp.EMPLOYEE_ID = item.Value
                        '    lstEmp.Add(emp)
                        'Next
                        objRequest.REMARK = txtRemark.Text
                        objRequest.LANGUAGE = cboLanguage.SelectedValue
                        objRequest.LANGUAGELEVEL = cboLanguageLevel.SelectedValue
                        objRequest.COMPUTER_LEVEL = cboComputerLevel.SelectedValue
                        If txtScores.Text <> "" Then
                            objRequest.LANGUAGESCORES = txtScores.Text
                        End If

                        objRequest.SPECIALSKILLS = cboSpecialSkills.SelectedValue
                        objRequest.MAINTASK = txtMainTask.Text

                        objRequest.DESCRIPTIONATTACHFILE = hddFile.Value

                        If Employee_list Is Nothing Then
                            Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                        End If

                        For Each item In Employee_list
                            Dim obj As New RequestEmpDTO
                            obj.EMPLOYEE_ID = item.ID
                            lstEmp.Add(obj)
                        Next
                        objRequest.lstEmp = lstEmp
                        If store.CHECK_EXIST_SE_CONFIG("PERSONRE_TCTD") = -1 Then
                            EnableControlAll(False, txtPersonRe_TCTD, btnFindPersonRe_TCTD)
                            objRequest.IS_INSERT_PRO = False
                        Else
                            objRequest.IS_INSERT_PRO = True
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequest(objRequest, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request_Person_TCTD&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objRequest.ID = hidID.Value
                                If rep.ModifyRequest(objRequest, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request_Person_TCTD&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim hfr As New HistaffFrameworkRepository
                    Dim tempPath As String = "ReportTemplates/Recruitment/Report/"
                    Dim obj = hfr.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_RECRUITMENT_NEEDS", New List(Of Object)({hidID.Value, If(txtPayrollLimit.Text = "", Nothing, txtPayrollLimit.Text), If(txtCurrentNumber.Text = "", Nothing, txtCurrentNumber.Text), rntxtRecruitNumber.Text}))
                    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "BM01_TT_Nhu_cau_TD.doc"),
                                        "TT_Nhu_cau_TD_" + DateTime.Now.ToString("HHmmssddMMyyyy") + ".doc",
                                        obj.Tables(0), Response)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request_Person_TCTD&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Try
            isLoadPopup = 1
            isPerson_PT = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            For Each itm In lstCommonEmployee
                If isPerson_PT = 1 Then
                    REQUIRER_EMP_ID = itm.EMPLOYEE_ID
                    txtPersonRequest.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                End If

                If isPerson_PT = 2 Then
                    PERSON_PT_RC_EMP_ID = itm.EMPLOYEE_ID
                    txtPersonRe_TCTD.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                End If
            Next
            isLoadPopup = 0
            isPerson_PT = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'duy fix ngay 11/07
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked, ctrlFindEmployee2GridPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub btnUploadFileDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileDescription.Click
        ctrlUpload1.Show()
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/" & hddFile.Value
        If System.IO.File.Exists(MapPath(sPath)) Then
            System.IO.File.Delete(MapPath(sPath))
            hddFile.Value = ""
            hypFile.Text = ""
            hypFile.NavigateUrl = ""
            SetVisibleFileAttach()
        Else
            ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
        End If
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        'Dim fileName As String
        Try
            Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
            Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/"

            If file.GetExtension = ".pdf" Or file.GetExtension = ".doc" Or file.GetExtension = ".docx" Or file.GetExtension = ".jpg" Or file.GetExtension = ".png" Then
                Dim fileName As String = hidOrgID.Value & "_" & "_" & cboTitle.SelectedValue & Date.Now.ToString("HHmmssffff") & "_" & file.FileName
                If System.IO.Directory.Exists(MapPath(sPath)) Then
                    file.SaveAs(MapPath(sPath) & fileName, True)
                    hddFile.Value = fileName
                    hypFile.Text = file.FileName
                    hypFile.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Recruitment/Upload/" + fileName
                    SetVisibleFileAttach()
                Else
                    ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
                End If

            Else
                ShowMessage(Translate("Vui lòng upload file có đuôi mở rộng: .pdf,.doc, .docx, .jpg, .png"), NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtUpload.Text = ""
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/")
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUpload.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS,XLSX,TXT,CTR,DOC,DOCX,XML,PNG,JPG,BITMAP,JPEG,GIF,PDF,RAR,ZIP,PPT,PPTX"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUpload.Text)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/" + txtUploadFile.Text + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rntxtSalary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtSalary.TextChanged
        Try
            Dim RANK_SAL As Decimal = 0
            Dim LuongAB As Decimal = 0
            Dim TongLuongThucTe As Decimal = 0
            If txtOrgName.Text <> "" AndAlso cboTitle.Text <> "" AndAlso rdSendDate.SelectedDate IsNot Nothing Then
                RANK_SAL = store.GET_RC_HR_PLANING_DETAIL_RANK_SAL(Int32.Parse(hidOrgID.Value), Int32.Parse(cboTitle.SelectedValue), rdSendDate.SelectedDate)


                Dim DT = store.GET_PLAN_DETAIL_BY_ORG("ADMIN", rdSendDate.SelectedDate, Int32.Parse(hidOrgID.Value), False)
                If DT.Rows.Count > 0 Then
                    LuongAB = DT.Rows(0)("LUONGAB")
                End If
                TongLuongThucTe = store.GET_WORKING_TOTAL_SAL("ADMIN", rdSendDate.SelectedDate, Int32.Parse(hidOrgID.Value), False)

            End If
            If rntxtSalary.Value IsNot Nothing AndAlso RANK_SAL > -1 Then
                If rntxtSalary.Value > RANK_SAL Then
                    ShowMessage(Translate("Mức lương đã vượt quá Rank lương của vị trí tuyển dụng trong bộ phận"), NotifyType.Warning)
                End If
            End If
            If store.CHECK_EXIST_SE_CONFIG("RC_SAL_BUDGET_EXCEEDED") = -1 Then
                If rntxtSalary.Value IsNot Nothing AndAlso LuongAB > 0 AndAlso TongLuongThucTe > 0 Then
                    Dim to_sal As Decimal = Decimal.Parse(rntxtSalary.Value) + TongLuongThucTe
                    If to_sal > LuongAB Then
                        ShowMessage(Translate("Mức lương đã vượt quá ngân sách tuyển dụng của bộ phận"), NotifyType.Warning)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If FindEmployee.Controls.Contains(ctrlFindEmployee2GridPopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployee2GridPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
                Case 3
                    ctrlFindEmployee2GridPopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployee2GridPopup.MustHaveContract = True
                    ctrlFindEmployee2GridPopup.MultiSelect = True
                    FindEmployee.Controls.Add(ctrlFindEmployee2GridPopup)
            End Select
            EnableControlAll(False, txtCode_YCTD, txtName_YCTD, chkIn, chkOut, btnFindOrg, cboTitle, txtGroupPos, cbolocationWork, txtPersonRequest, rdSendDate, rdExpectedJoinDate, cboContractType, cboWorkingPlace, rntxtSalary, cboRecruitProperty, txtCurrentNumber, txtPayrollLimit, txtDifferenceNumber,
            cboRecruitReason, rntxtRecruitNumber, chkIsOver, chkRequestComputer, txtRecruitReason, lstEmployee, cboLearningLevel, cboQualification, txtQualificationOthers, rntxtAgeFrom, rntxtAgeTo, cboLanguage, cboLanguageLevel, txtScores, txtForeignAbility, rntxtExperienceNumber, cboGenderPriority, cboComputerLevel, txtComputerAppLevel, txtDescription,
            txtMainTask, cboSpecialSkills, txtRequestExperience, txtRequestOther, txtRemark, ChkIs_Approve, btnFindEmployee, btnUpload, btnDownload)
            If IS_CLOCK = -1 Then
                EnableControlAll(False, txtPersonRe_TCTD, btnFindPersonRe_TCTD)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
                FillRadCombobox(cboRecruitReason, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboLearningLevel, dtData, "NAME", "ID")
                'load loai hinh hop dong
                dtData = rep.GetOtherList("LABOR_TYPE", True)
                FillRadCombobox(cboContractType, dtData, "NAME", "ID")
                ' Load data to cbo ACADEMY,LANGUAGE, LANGUAGE_LEVEL,MAJOR,SPECIALSKILLS
                'LANGUAGE
                dtData = rep.GetOtherList("RC_LANGUAGE", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID", True)
                'LANGUAGE_LEVEL
                dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguageLevel, dtData, "NAME", "ID", True)
                'MAJOR
                dtData = rep.GetOtherList("MAJOR", True)
                FillRadCombobox(cboQualification, dtData, "NAME", "ID", True)
                'SPECIALSKILLS
                dtData = rep.GetOtherList("SPECIALSKILLS", True)
                FillRadCombobox(cboSpecialSkills, dtData, "NAME", "ID", True)
                'COMPUTERLEVEL
                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboComputerLevel, dtData, "NAME", "ID", True)
                'RECRUIT PROPERTY
                dtData = rep.GetOtherList("RC_RECRUIT_PROPERTY")
                FillRadCombobox(cboRecruitProperty, dtData, "NAME", "ID", True)
                'GENDER
                dtData = rep.GetOtherList("GENDER")
                FillRadCombobox(cboGenderPriority, dtData, "NAME", "ID", True)
                'tinh thanh
                dtData = rep.GetProvinceList("False")
                FillRadCombobox(cbolocationWork, dtData, "NAME", "ID")
            End Using

            Using rep As New ProfileRepository
                Dim dtRegion = rep.Get_HU_WORK_PLACE("", True)
                FillRadCombobox(cboWorkingPlace, dtRegion, "NAME_VN", "ID")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IsNumeric(Request.Params("IS_CLOCK")) Then
                    IS_CLOCK = Decimal.Parse(Request.Params("IS_CLOCK"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetVisibleFileAttach()
        If hddFile.Value <> "" Then
            btnDeleteFile.Visible = True
            hypFile.Visible = True
            btnUploadFileDescription.Visible = False
        Else
            btnDeleteFile.Visible = False
            hypFile.Visible = False
            btnUploadFileDescription.Visible = True
        End If

    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim fileNameZip As String

            'If order = 0 Then
            '    fileNameZip = txtUpload_LG.Text.Trim
            'ElseIf order = 1 Then
            '    fileNameZip = txtUpload_HD.Text.Trim
            'Else
            '    fileNameZip = txtUpload_FT.Text.Trim
            'End If

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
        End Try
    End Sub

    Private Sub rntxtRecruitNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtRecruitNumber.TextChanged
        CheckNumRecruit()
    End Sub

    Private Sub CheckNumRecruit()
        Try
            If chkIn.Checked Then
                If rntxtRecruitNumber.Value = 0 Then
                    ShowMessage(Translate("SL cần tuyển phải lớn hơn hoặc bằng 1"), Utilities.NotifyType.Warning)
                    Exit Sub
                Else
                    If rntxtRecruitNumber.Value > txtDifferenceNumber.Value Then
                        IsVuotDinhBien = True
                        ShowMessage(Translate("SL cần tuyển vượt định biên"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                End If

                'If cboRecruitReason.SelectedValue = "" Then
                '    If txtDifferenceNumber.Value - rntxtRecruitNumber.Value < 0 Then
                '        IsVuotDinhBien = True
                '        ShowMessage(Translate("SL cần tuyển vượt định biên"), Utilities.NotifyType.Warning)
                '        Exit Sub
                '    End If
                'Else
                '    If Decimal.Parse(cboRecruitReason.SelectedValue) = 4053 Then
                '        If chkIsOver.Checked = False Then
                '            If txtDifferenceNumber.Value + Employee_list.Count - rntxtRecruitNumber.Value < 0 Then
                '                IsVuotDinhBien = True
                '                ShowMessage(Translate("SL cần tuyển vượt định biên"), Utilities.NotifyType.Warning)
                '                Exit Sub
                '            End If
                '        End If
                '    Else
                '        If txtDifferenceNumber.Value - rntxtRecruitNumber.Value < 0 Then
                '            IsVuotDinhBien = True
                '            If chkIsOver.Checked = False Then
                '                ShowMessage(Translate("SL cần tuyển vượt định biên"), Utilities.NotifyType.Warning)
                '            End If
                '            Exit Sub
                '        End If
                '    End If
                'End If

            Else
                IsVuotDinhBien = False
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub chkIsInPlan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsInPlan.CheckedChanged
    '    Try
    '        'If chkIsInPlan.Checked Then
    '        '    rdSendDate.AutoPostBack = True
    '        'Else
    '        '    rdSendDate.AutoPostBack = False
    '        'End If

    '        cboTitle.Items.Clear()
    '        cboTitle.ClearSelection()
    '        cboTitle.Text = ""

    '        LoadComboTitle()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

    'Private Sub rdSendDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdSendDate.SelectedDateChanged
    '    Try
    '        LoadComboTitle()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

    Private Sub LoadComboTitle()

        If hidOrgID.Value <> "" Then
            Dim dtData As DataTable
            dtData = store.GET_TITLE_IN_PLAN(hidOrgID.Value, 0)
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        Else
            cboTitle.Items.Clear()
            cboTitle.ClearSelection()
            cboTitle.Text = ""
        End If

        'If chkIsInPlan.Checked Then
        '    If hidOrgID.Value <> "" And rdSendDate.SelectedDate IsNot Nothing Then
        '        Dim dtData As DataTable
        '        Using rep As New RecruitmentRepository
        '            dtData = rep.GetTitleByOrgListInPlan(hidOrgID.Value, rdSendDate.SelectedDate.Value.Year, True)
        '            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        '        End Using
        '    Else
        '        cboTitle.Items.Clear()
        '        cboTitle.ClearSelection()
        '        cboTitle.Text = ""
        '    End If
        'Else
        '    If hidOrgID.Value <> "" Then
        '        Dim dtData As DataTable
        '        Using rep As New RecruitmentRepository
        '            dtData = rep.GetTitleByOrgList(hidOrgID.Value, True)
        '            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        '        End Using
        '    Else
        '        cboTitle.Items.Clear()
        '        cboTitle.ClearSelection()
        '        cboTitle.Text = ""
        '    End If
        'End If

    End Sub

    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Dim rep As New RecruitmentRepository
        GetTotalEmployeeByTitleID()

        'If chkIsInPlan.Checked Then

        'Else
        '    cboLearningLevel.Text = ""
        '    rntxtAgeFrom.Text = ""
        '    rntxtAgeTo.Text = ""
        '    cboQualification.Text = ""
        '    cboSpecialSkills.Text = ""
        '    cboLanguage.Text = ""
        '    cboLanguageLevel.Text = ""
        '    rdExpectedJoinDate.SelectedDate = Nothing
        '    cboComputerLevel.Text = ""
        '    txtMainTask.Text = ""
        '    txtRequestExperience.Text = ""
        'End If
        If hidID.Value <> "" Then
            Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = Decimal.Parse(hidID.Value)})
            If obj.ID > 0 And cboTitle.SelectedValue = obj.RC_PLAN_ID Then
                If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                    cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                End If
                If obj.AGE_FROM IsNot Nothing Then
                    rntxtAgeFrom.Value = obj.AGE_FROM
                End If
                If obj.AGE_TO IsNot Nothing Then
                    rntxtAgeTo.Value = obj.AGE_TO
                End If
                If obj.QUALIFICATION IsNot Nothing Then
                    cboQualification.SelectedValue = obj.QUALIFICATION
                End If
                If obj.SPECIALSKILLS IsNot Nothing Then
                    cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                End If
                If obj.LANGUAGE IsNot Nothing Then
                    cboLanguage.SelectedValue = obj.LANGUAGE
                End If
                If obj.LANGUAGELEVEL IsNot Nothing Then
                    cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                End If

                txtScores.Text = If(obj.LANGUAGESCORES Is Nothing, String.Empty, obj.LANGUAGESCORES)
                rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                If obj.COMPUTER_LEVEL IsNot Nothing Then
                    cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                End If
                txtMainTask.Text = obj.MAINTASK
                txtRequestExperience.Text = obj.REQUEST_EXPERIENCE
            Else
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("EDUCATIONLEVEL") IsNot Nothing And dt.Rows(0)("EDUCATIONLEVEL").ToString() <> String.Empty Then
                        cboLearningLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("EDUCATIONLEVEL").ToString())
                    End If
                    If dt.Rows(0)("AGESFROM") IsNot Nothing And dt.Rows(0)("AGESFROM").ToString() <> String.Empty Then
                        rntxtAgeFrom.Value = Double.Parse(dt.Rows(0)("AGESFROM").ToString())
                    End If
                    If dt.Rows(0)("AGESTO") IsNot Nothing And dt.Rows(0)("AGESTO").ToString() <> String.Empty Then
                        rntxtAgeTo.Value = Double.Parse(dt.Rows(0)("AGESTO").ToString())
                    End If
                    If dt.Rows(0)("QUALIFICATION") IsNot Nothing And dt.Rows(0)("QUALIFICATION").ToString() <> String.Empty Then
                        cboQualification.SelectedValue = Decimal.Parse(dt.Rows(0)("QUALIFICATION").ToString())
                    End If
                    If dt.Rows(0)("SPECIALSKILLS") IsNot Nothing And dt.Rows(0)("SPECIALSKILLS").ToString() <> String.Empty Then
                        cboSpecialSkills.SelectedValue = Decimal.Parse(dt.Rows(0)("SPECIALSKILLS").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGE") IsNot Nothing And dt.Rows(0)("LANGUAGE").ToString() <> String.Empty Then
                        cboLanguage.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGE").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGELEVEL") IsNot Nothing And dt.Rows(0)("LANGUAGELEVEL").ToString() <> String.Empty Then
                        cboLanguageLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGELEVEL").ToString())
                    End If

                    txtScores.Text = dt.Rows(0)("LANGUAGESCORES").ToString()
                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                    If dt.Rows(0)("COMPUTER_LEVEL") IsNot Nothing And dt.Rows(0)("COMPUTER_LEVEL").ToString() <> String.Empty Then
                        cboComputerLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("COMPUTER_LEVEL").ToString())
                    End If
                    txtMainTask.Text = dt.Rows(0)("MAINTASK").ToString()
                    txtRequestExperience.Text = dt.Rows(0)("QUALIFICATIONREQUEST").ToString()
                Else
                    cboLearningLevel.Text = ""
                    rntxtAgeFrom.Text = ""
                    rntxtAgeTo.Text = ""
                    cboQualification.Text = ""
                    cboSpecialSkills.Text = ""
                    cboLanguage.Text = ""
                    cboLanguageLevel.Text = ""
                    rdExpectedJoinDate.SelectedDate = Nothing
                    cboComputerLevel.Text = ""
                    txtMainTask.Text = ""
                    txtRequestExperience.Text = ""
                End If
            End If
        Else
            If cboTitle.SelectedValue <> "" Then
                Dim dtgr = store.GET_GROUP_TITLE_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                txtGroupPos.Text = dtgr.Rows(0)("NAME_VN")
                DAYNUM = Decimal.Parse(dtgr.Rows(0)("DAY_NUM"))
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("EDUCATIONLEVEL") IsNot Nothing And dt.Rows(0)("EDUCATIONLEVEL").ToString() <> String.Empty Then
                        cboLearningLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("EDUCATIONLEVEL").ToString())
                    End If
                    If dt.Rows(0)("AGESFROM") IsNot Nothing And dt.Rows(0)("AGESFROM").ToString() <> String.Empty Then
                        rntxtAgeFrom.Value = Double.Parse(dt.Rows(0)("AGESFROM").ToString())
                    End If
                    If dt.Rows(0)("AGESTO") IsNot Nothing And dt.Rows(0)("AGESTO").ToString() <> String.Empty Then
                        rntxtAgeTo.Value = Double.Parse(dt.Rows(0)("AGESTO").ToString())
                    End If
                    If dt.Rows(0)("QUALIFICATION") IsNot Nothing And dt.Rows(0)("QUALIFICATION").ToString() <> String.Empty Then
                        cboQualification.SelectedValue = Decimal.Parse(dt.Rows(0)("QUALIFICATION").ToString())
                    End If
                    If dt.Rows(0)("SPECIALSKILLS") IsNot Nothing And dt.Rows(0)("SPECIALSKILLS").ToString() <> String.Empty Then
                        cboSpecialSkills.SelectedValue = Decimal.Parse(dt.Rows(0)("SPECIALSKILLS").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGE") IsNot Nothing And dt.Rows(0)("LANGUAGE").ToString() <> String.Empty Then
                        cboLanguage.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGE").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGELEVEL") IsNot Nothing And dt.Rows(0)("LANGUAGELEVEL").ToString() <> String.Empty Then
                        cboLanguageLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGELEVEL").ToString())
                    End If

                    txtScores.Text = dt.Rows(0)("LANGUAGESCORES").ToString()
                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                    If dt.Rows(0)("COMPUTER_LEVEL") IsNot Nothing And dt.Rows(0)("COMPUTER_LEVEL").ToString() <> String.Empty Then
                        cboComputerLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("COMPUTER_LEVEL").ToString())
                    End If
                    txtMainTask.Text = dt.Rows(0)("MAINTASK").ToString()
                    txtRequestExperience.Text = dt.Rows(0)("QUALIFICATIONREQUEST").ToString()
                Else
                    cboLearningLevel.Text = ""
                    rntxtAgeFrom.Text = ""
                    rntxtAgeTo.Text = ""
                    cboQualification.Text = ""
                    cboSpecialSkills.Text = ""
                    cboLanguage.Text = ""
                    cboLanguageLevel.Text = ""
                    rdExpectedJoinDate.SelectedDate = Nothing
                    cboComputerLevel.Text = ""
                    txtMainTask.Text = ""
                    txtRequestExperience.Text = ""


                End If
            Else
                txtGroupPos.Text = ""
            End If
        End If
        'anhvn
        Dim dt_tit = store.GET_TITLE_BY_ID(Int32.Parse(cboTitle.SelectedValue))
        If dt_tit.Rows.Count > 0 Then
            If dt_tit.Rows(0)("REMARK") IsNot Nothing And dt_tit.Rows(0)("REMARK").ToString() <> String.Empty Then
                txtDescription.Text = dt_tit.Rows(0)("REMARK").ToString()
            End If
        End If
    End Sub

    Protected Sub GetTotalEmployeeByTitleID()
        Try

            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty AndAlso cboTitle.Text <> "" Then
                    Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    Dim dt As DataTable = store.GET_NUM_PLANNING_DETAIL_BY_MONTH(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    If dt.Rows.Count > 0 Then
                        txtPayrollLimit.Value = Decimal.Parse(dt.Rows(0)("NEW_MANNING"))
                    Else
                        txtPayrollLimit.Value = 0
                    End If
                    If tab.Rows.Count > 0 Then
                        txtCurrentNumber.Value = Decimal.Parse(tab.Rows(0)("CURRENT_MANNING"))

                    Else
                        txtCurrentNumber.Value = 0
                    End If
                    CalDifferenceNumber()
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CalDifferenceNumber()
        Try
            If txtPayrollLimit.Value Is Nothing Or txtCurrentNumber.Value Is Nothing Then
                Exit Sub
            End If
            If cboRecruitReason.SelectedValue <> "" Then
                'thay the
                If Decimal.Parse(cboRecruitReason.SelectedValue) = 4053 Then
                    txtDifferenceNumber.Value = txtPayrollLimit.Value - txtCurrentNumber.Value + Employee_list.Count
                Else
                    txtDifferenceNumber.Value = txtPayrollLimit.Value - txtCurrentNumber.Value
                End If
            Else
                txtDifferenceNumber.Value = txtPayrollLimit.Value - txtCurrentNumber.Value
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUpload.Text = strUpload
                txtUploadFile.Text = Down_File
            Else
                strUpload = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdSendDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdSendDate.SelectedDateChanged
        Try
            GetTotalEmployeeByTitleID()

            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty Then
                    '' AUTO GENERATE REQUEST CODE
                    ClearControlValue(txtCode_YCTD, txtName_YCTD)

                    Dim dt_code = store.AUTO_GEN_CODE_RC(Int32.Parse(hidOrgID.Value), rdSendDate.SelectedDate)
                    txtCode_YCTD.Text = dt_code.Rows(0)("CODE_REQUEST")

                    Dim value = rdSendDate.SelectedDate.Value
                    txtName_YCTD.Text = txtOrgName.Text & " - " & cboTitle.Text & " - " & value.ToShortDateString

                    rdExpectedJoinDate.SelectedDate = rdSendDate.SelectedDate.Value.AddDays(DAYNUM)

                    'Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    'Dim dt As DataTable = store.GET_NUM_PLANNING_DETAIL_BY_MONTH(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)

                    'If dt.Rows.Count > 0 Then
                    '    txtPayrollLimit.Text = dt.Rows(0)("NEW_MANNING").ToString
                    'Else
                    '    txtPayrollLimit.Text = "0"
                    'End If

                    'If tab.Rows.Count > 0 Then
                    '    'txtPayrollLimit.Text = dt.Rows(0)("NEW_MANNING").ToString()
                    '    txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                    '    txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                    'Else
                    '    'txtPayrollLimit.Text = "0"
                    '    txtCurrentNumber.Text = "0"
                    '    txtDifferenceNumber.Text = "0"
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgEmployee_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource

        Try
            'Dim DT = store.GET_INF_EMP_BY_ID(1)
            If Employee_list Is Nothing Then
                Employee_list = New List(Of CommonBusiness.EmployeeDTO)
            End If
            rgEmployee.DataSource = Employee_list
            CalDifferenceNumber()
            CheckNumRecruit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboRecruitReason_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboRecruitReason.SelectedIndexChanged

        Try
            If cboRecruitReason.SelectedValue = "" Then
                Exit Sub
            End If

            If Decimal.Parse(cboRecruitReason.SelectedValue) = 4053 Then
                TuyenThayThe.Visible = True
                RgTuyenThayThe.Visible = True
            Else
                Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                TuyenThayThe.Visible = False
                RgTuyenThayThe.Visible = False
                rgEmployee.Rebind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện ItemCommand cho grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case e.CommandName

                Case "FindEmployee"
                    isLoadPopup = 3
                    UpdateControlState()
                    ctrlFindEmployee2GridPopup.MultiSelect = True
                    ctrlFindEmployee2GridPopup.Show()

                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_list Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_list.Remove(s)
                    Next
                    rgEmployee.Rebind()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmployee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployee2GridPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee2GridPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindEmployee2GridPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                If Employee_list Is Nothing Then
                    Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_list.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        Continue For
                    End If
                    Dim employee As New CommonBusiness.EmployeeDTO
                    employee.ID = emp.ID
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME_VN = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    employee.JOIN_DATE = emp.JOIN_DATE
                    Employee_list.Add(employee)
                Next
            End If
            isLoadPopup = 0
            rgEmployee.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnFindPersonRe_TCTD_Click(sender As Object, e As EventArgs) Handles btnFindPersonRe_TCTD.Click
        Try
            isLoadPopup = 1
            isPerson_PT = 2
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub chkIn_CheckedChanged(sender As Object, e As EventArgs) Handles chkIn.CheckedChanged, chkOut.CheckedChanged, chkIsOver.CheckedChanged
        Try
            If chkIsOver.Checked = False Then
                CheckNumRecruit()
            End If

            If chkOut.Checked Then
                chkIsOver.Checked = False
                chkIsOver.Enabled = False
            Else
                chkIsOver.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub CusRecruitNumber_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CusRecruitNumber.ServerValidate
    '    Try
    '        args.IsValid = True
    '        If chkIn.Checked Then
    '            If cboRecruitReason.SelectedValue = 4053 Then
    '                If chkIsOver.Checked = False Then
    '                    If rntxtRecruitNumber.Value + Employee_list.Count - txtDifferenceNumber.Value < 0 Then
    '                        args.IsValid = True
    '                        Exit Sub
    '                    End If
    '                End If
    '            Else
    '                If rntxtRecruitNumber.Value - txtDifferenceNumber.Value < 0 Then
    '                    args.IsValid = True
    '                    Exit Sub
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

#End Region


End Class