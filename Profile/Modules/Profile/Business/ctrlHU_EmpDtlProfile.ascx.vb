Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpDtlProfile
    Inherits CommonView
    Public Property EmployeeID As Decimal
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private CommonRepo As New CommonRepository
    Private ProfileRepo As New ProfileRepository
    Private ProfileBusRepo As New ProfileBusinessRepository
    Private ProfileStore As New ProfileStoreProcedure
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()


#Region "Properties"
    Property result As Boolean
        Get
            Return PageViewState(Me.ID & "_result")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_result") = value
        End Set
    End Property
    Property gID As Decimal
        Get
            Return PageViewState(Me.ID & "_gID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_gID") = value
        End Set
    End Property
    Property gEmpCode As String
        Get
            Return PageViewState(Me.ID & "_gEmpCode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_gEmpCode") = value
        End Set
    End Property
    Property _binaryImage As Byte()
        Get
            Return PageViewState(Me.ID & "_binaryImage")
        End Get
        Set(ByVal value As Byte())
            PageViewState(Me.ID & "_binaryImage") = value
        End Set
    End Property
    Property EMP_ID_OLD As Decimal
        Get
            Return PageViewState(Me.ID & "_EMP_ID_OLD")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_EMP_ID_OLD") = value
        End Set
    End Property
    Property EmpCV As EmployeeCVDTO
        Get
            Return PageViewState(Me.ID & "_EmpCV")
        End Get
        Set(ByVal value As EmployeeCVDTO)
            PageViewState(Me.ID & "_EmpCV") = value
        End Set
    End Property
    Property EmpEdu As EmployeeEduDTO
        Get
            Return PageViewState(Me.ID & "_EmpEdu")
        End Get
        Set(ByVal value As EmployeeEduDTO)
            PageViewState(Me.ID & "_EmpEdu") = value
        End Set
    End Property
    Property EmpHealth As EmployeeHealthDTO
        Get
            Return PageViewState(Me.ID & "_EmpHealth")
        End Get
        Set(ByVal value As EmployeeHealthDTO)
            PageViewState(Me.ID & "_EmpHealth") = value
        End Set
    End Property
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property isLoadPopup As Decimal
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
        End Set
    End Property

    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return PageViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            PageViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property

    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
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
#End Region

#Region "Page"
    Public _DIRECT_MANAGER As String
    Public _LEVEL_MANAGER As String
    Public _ORG_ID As String

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CType(CommonConfig.dicConfig("APP_SETTING_17"), Boolean) Then
                Label8.Visible = False
                rdSeniorityDate.Visible = False
            End If
            UpdateControlState()
            Refresh()
            If CommonConfig.APP_SETTING_15() Then
                lbObjectLabor.Visible = False
                cboObjectLabor.Visible = False
                reqObjectLabor.Visible = False
                spObjectLabor.Visible = False
            End If
            If CommonConfig.APP_SETTING_16() Then
                reqObjectLabor.Visible = False
                spObjectLabor.Visible = False
            End If
            If CommonConfig.HIDE_MANAGERHEATHINS() Then
                hid_ins0.Visible = False
                hid_ins1.Visible = False
                hid_ins2.Visible = False
                hid_ins3.Visible = False
                hid_ins4.Visible = False
            End If
            CurrentPlaceHolder = Me.ViewName
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstP, lstD, lstW As DataTable
        Try
            rwPopup.VisibleOnPageLoad = False
            rwPopup.NavigateUrl = ""
            If EmployeeInfo IsNot Nothing Then
                EmployeeID = EmployeeInfo.ID
            End If
            If Not isLoad Then
                If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.ID <> 0 Then
                    If CurrentState = "Copy" Then
                        txtEmpCODE.Text = ""
                        rtEmpCode_OLD.Text = EmployeeInfo.EMPLOYEE_CODE
                        txtTimeID.Text = ""
                        cboWorkStatus.Text = ""
                        cboWorkStatus.SelectedValue = ""
                        cboEmpStatus.Text = ""
                        cboEmpStatus.SelectedValue = ""
                        rdJoinDate.SelectedDate = Nothing
                        rdJoinDateState.SelectedDate = Nothing
                        rdter_effect_date.SelectedDate = Nothing
                        txtContractNo.Text = ""
                        txtContractType.Text = ""
                        rdContractEffectDate.SelectedDate = Nothing
                        rdContractExpireDate.SelectedDate = Nothing
                        If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                            btnFindOrg.Enabled = True
                            EnableRadCombo(cboTitle, True)
                            EnableRadCombo(cboJobLevel, True)
                        End If
                    Else
                        rtEmpCode_OLD.Text = EmployeeInfo.EMPLOYEE_CODE_OLD
                        txtEmpCODE.Text = EmployeeInfo.EMPLOYEE_CODE
                        txtTimeID.Text = EmployeeInfo.ITIME_ID
                        If EmployeeInfo.WORK_STATUS IsNot Nothing Then
                            cboWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME
                            cboWorkStatus.SelectedValue = EmployeeInfo.WORK_STATUS
                        End If
                        If EmployeeInfo.EMP_STATUS_NAME IsNot Nothing Then
                            cboEmpStatus.Text = EmployeeInfo.EMP_STATUS_NAME
                            cboEmpStatus.SelectedValue = EmployeeInfo.EMP_STATUS
                        End If
                        rdJoinDate.SelectedDate = EmployeeInfo.JOIN_DATE
                        rdJoinDateState.SelectedDate = EmployeeInfo.JOIN_DATE_STATE
                        rdter_effect_date.SelectedDate = EmployeeInfo.TER_EFFECT_DATE
                        txtContractNo.Text = EmployeeInfo.CONTRACT_NO
                        txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                        rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                        rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                        If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                            btnFindOrg.Enabled = False
                            EnableRadCombo(cboTitle, False)
                            EnableRadCombo(cboJobLevel, False)
                        End If
                    End If
                    rtBookNo.Text = EmployeeInfo.BOOK_NO_SOCIAL
                    hidID.Value = EmployeeInfo.ID
                    hidIDCopy.Value = EmployeeInfo.ID
                    If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                        hidWorkingID.Value = EmployeeInfo.LAST_WORKING_ID
                    End If
                    If EmployeeInfo.OBJECTTIMEKEEPING IsNot Nothing Then
                        cboObject.SelectedValue = EmployeeInfo.OBJECTTIMEKEEPING
                        cboObject.Text = EmployeeInfo.OBJECTTIMEKEEPING_NAME
                    End If
                    If EmployeeInfo.OBJECT_LABOR IsNot Nothing Then
                        cboObjectLabor.SelectedValue = EmployeeInfo.OBJECT_LABOR
                        cboObjectLabor.Text = EmployeeInfo.OBJECT_LABOR_NAME
                    End If
                    If EmployeeInfo.CONTRACT_ID IsNot Nothing Then
                        hidContractID.Value = EmployeeInfo.CONTRACT_ID
                    End If
                    If EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                        hidIsTer.Value = -1
                    End If
                    If EmployeeInfo.MATHE IsNot Nothing Then
                        cboMaThe.SelectedValue = EmployeeInfo.MATHE
                    End If
                    If EmployeeInfo.COPORATION_DATE IsNot Nothing Then
                        rdCoporationDate.SelectedDate = EmployeeInfo.COPORATION_DATE
                    End If
                    If EmployeeInfo.CONTRACTED_UNIT IsNot Nothing Then
                        cboContractedUnit.SelectedValue = EmployeeInfo.CONTRACTED_UNIT
                    End If
                    Dim dtDataTitle As DataTable
                    Dim obja As New ParamIDDTO
                    If Decimal.Parse(EmployeeInfo.ID) <> 0 Then
                        If Decimal.Parse(EmployeeInfo.ORG_ID) <> 0 Then
                            obja.EMPLOYEE_ID = EmployeeInfo.ID
                            obja.ID = EmployeeInfo.ORG_ID
                            dtDataTitle = ProfileRepo.GetDataByProcedures(9, obja.ID, obja.EMPLOYEE_ID.ToString, Common.Common.SystemLanguage.Name)
                            ClearControlValue(cboTitle)
                            FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                        End If
                    End If
                    txtmanager.Text = EmployeeInfo.DIRECT_MANAGER_TITLE_NAME
                    _DIRECT_MANAGER = Utilities.ObjToString(EmployeeInfo.DIRECT_MANAGER)
                    _LEVEL_MANAGER = Utilities.ObjToString(EmployeeInfo.LEVEL_MANAGER)
                    _ORG_ID = EmployeeInfo.ORG_ID
                    txtFirstNameVN.Text = EmployeeInfo.FIRST_NAME_VN
                    txtLastNameVN.Text = EmployeeInfo.LAST_NAME_VN
                    hidOrgID.Value = EmployeeInfo.ORG_ID
                    If EmployeeInfo.ORG_ID IsNot Nothing Then
                        Dim objOrg = ProfileRepo.GetOrgOMByID(EmployeeInfo.ORG_ID)
                        If objOrg IsNot Nothing Then
                            txtCongTy.Text = objOrg.ORG_NAME2
                        End If
                    End If
                    FillDataInControls(EmployeeInfo.ORG_ID)
                    If EmployeeInfo.TITLE_ID IsNot Nothing Then
                        cboTitle.SelectedValue = EmployeeInfo.TITLE_ID
                        cboTitle.Text = EmployeeInfo.TITLE_NAME_VN
                        cboTitle_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If cboTitle.SelectedValue <> "" Then
                        Dim jobLevel = ProfileRepo.GetDataByProcedures(12, cboTitle.SelectedValue)
                        If jobLevel IsNot Nothing AndAlso jobLevel.Rows.Count > 0 Then
                            FillRadCombobox(cboJobLevel, jobLevel, "NAME_VN", "ID")
                            If EmployeeInfo.STAFF_RANK_ID IsNot Nothing Then
                                cboJobLevel.SelectedValue = EmployeeInfo.STAFF_RANK_ID
                            End If
                        End If
                    End If
                    rtThamNien.Text = CalculateSeniority(rdJoinDate.SelectedDate)
                    txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_TITLE_NAME
                    txtmanager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                    rdSeniorityDate.SelectedDate = EmployeeInfo.SENIORITY_DATE
                    If EmployeeInfo.OBJECT_EMPLOYEE_ID IsNot Nothing Then
                        rcOBJECT_EMPLOYEE.Text = EmployeeInfo.OBJECT_EMPLOYEE_NAME
                        rcOBJECT_EMPLOYEE.SelectedValue = EmployeeInfo.OBJECT_EMPLOYEE_ID
                    End If
                    If EmployeeInfo.WORK_PLACE_ID IsNot Nothing Then
                        rcRegion.Text = EmployeeInfo.WORK_PLACE_NAME
                        rcRegion.SelectedValue = EmployeeInfo.WORK_PLACE_ID
                    End If
                    If EmployeeInfo.OBJECT_ATTENDANT_ID IsNot Nothing Then
                        rcOBJECT_ATTENDANT.Text = EmployeeInfo.OBJECT_ATTENDANT_NAME
                        rcOBJECT_ATTENDANT.SelectedValue = EmployeeInfo.OBJECT_ATTENDANT_ID
                    End If
                    chkForeign.Checked = EmployeeInfo.FOREIGN
                    If EmployeeInfo.CONTRACT_ID IsNot Nothing Then
                        EnableControlAll(False, cboObject, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT)
                    Else
                        EnableControlAll(True, cboObject, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT)
                    End If
                    Dim empCV As EmployeeCVDTO
                    Dim empEdu As EmployeeEduDTO
                    Dim empHealth As EmployeeHealthDTO
                    ProfileBusRepo.GetEmployeeAllByID(EmployeeInfo.ID, empCV, empEdu, empHealth)
                    If empCV IsNot Nothing Then
                        If IsNumeric(empCV.PROVINCENQ_ID) Then
                            cbPROVINCENQ_ID.SelectedValue = empCV.PROVINCENQ_ID
                        End If
                        txtPerson_Inheritance.Text = empCV.PERSON_INHERITANCE
                        rdDayPitcode.SelectedDate = empCV.PIT_CODE_DATE
                        lstP = ProfileRepo.GetProvinceList2(False)
                        FillRadCombobox(cbPROVINCEEMP_ID, lstP, "NAME", "ID")
                        If IsNumeric(empCV.PROVINCEEMP_ID) Then
                            cbPROVINCEEMP_ID.SelectedValue = empCV.PROVINCEEMP_ID
                            lstD = ProfileRepo.GetDistrictList(cbPROVINCEEMP_ID.SelectedValue, False)
                            FillRadCombobox(cbDISTRICTEMP_ID, lstD, "NAME", "ID")
                        End If
                        If IsNumeric(empCV.DISTRICTEMP_ID) Then
                            cbDISTRICTEMP_ID.SelectedValue = empCV.DISTRICTEMP_ID
                            lstW = ProfileRepo.GetWardList(cbDISTRICTEMP_ID.SelectedValue, False)
                            FillRadCombobox(cbWARDEMP_ID, lstW, "NAME", "ID")
                        End If
                        If IsNumeric(empCV.WARDEMP_ID) Then
                            cbWARDEMP_ID.SelectedValue = empCV.WARDEMP_ID
                        End If
                        If IsNumeric(empCV.DANG) Then
                            ckDANG.Checked = CType(empCV.DANG, Boolean)
                        End If
                        If IsNumeric(empCV.IS_CHUHO) Then
                            ckCHUHO.Checked = CType(empCV.IS_CHUHO, Boolean)
                        End If
                        If ckCHUHO.Checked = True Then
                            cboRELATE_OWNER.Enabled = False
                        Else
                            cboRELATE_OWNER.Enabled = True
                        End If
                        If IsNumeric(empCV.CONG_DOAN) Then
                            ckCONG_DOAN.Checked = CType(empCV.CONG_DOAN, Boolean)
                        End If
                        If IsDate(empCV.NGAY_VAO_DANG_DB) Then
                            rdNGAY_VAO_DANG_DB.SelectedDate = empCV.NGAY_VAO_DANG_DB
                        End If
                        rtCHUC_VU_DANG.Text = empCV.CHUC_VU_DANG
                        txtNoHouseHolds.Text = empCV.NO_HOUSEHOLDS
                        txtCodeHouseHolds.Text = empCV.CODE_HOUSEHOLDS
                        If IsDate(empCV.NGAY_VAO_DANG) Then
                            rdNGAY_VAO_DANG.SelectedDate = empCV.NGAY_VAO_DANG
                        End If
                        rtCHUC_VU_DOAN.Text = empCV.CHUC_VU_DOAN
                        If empCV.INS_REGION_ID IsNot Nothing Then
                            cboInsRegion.SelectedValue = empCV.INS_REGION_ID
                        End If
                        If empCV.GENDER IsNot Nothing Then
                            cboGender.SelectedValue = empCV.GENDER
                            cboGender.Text = empCV.GENDER_NAME
                        End If
                        rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                        If empCV.MARITAL_STATUS IsNot Nothing Then
                            cboFamilyStatus.SelectedValue = empCV.MARITAL_STATUS
                            cboFamilyStatus.Text = empCV.MARITAL_STATUS_NAME
                        End If
                        If empCV.RELIGION IsNot Nothing Then
                            cboReligion.SelectedValue = empCV.RELIGION
                            cboReligion.Text = empCV.RELIGION_NAME
                        End If
                        If empCV.NATIVE IsNot Nothing Then
                            cboNative.SelectedValue = empCV.NATIVE
                            cboNative.Text = empCV.NATIVE_NAME
                        End If
                        If empCV.NATIONALITY IsNot Nothing Then
                            cboNationlity.SelectedValue = empCV.NATIONALITY
                            cboNationlity.Text = empCV.NATIONALITY_NAME
                        End If
                        txtNavAddress.Text = empCV.NAV_ADDRESS
                        If empCV.NAV_PROVINCE IsNot Nothing Then
                            cboNav_Province.SelectedValue = empCV.NAV_PROVINCE
                            cboNav_Province.Text = empCV.NAV_PROVINCE_NAME
                        End If
                        If empCV.NAV_DISTRICT IsNot Nothing Then
                            cboNav_District.SelectedValue = empCV.NAV_DISTRICT
                            cboNav_District.Text = empCV.NAV_DISTRICT_NAME
                        End If
                        If empCV.NAV_WARD IsNot Nothing Then
                            cboNav_Ward.SelectedValue = empCV.NAV_WARD
                            cboNav_Ward.Text = empCV.NAV_WARD_NAME
                        End If
                        txtPerAddress.Text = empCV.PER_ADDRESS
                        If empCV.PER_PROVINCE IsNot Nothing Then
                            cboPer_Province.SelectedValue = empCV.PER_PROVINCE
                            cboPer_Province.Text = empCV.PER_PROVINCE_NAME
                        End If
                        If empCV.PER_DISTRICT IsNot Nothing Then
                            cboPer_District.SelectedValue = empCV.PER_DISTRICT
                            cboPer_District.Text = empCV.PER_DISTRICT_NAME
                        End If
                        If empCV.PER_WARD IsNot Nothing Then
                            cboPer_Ward.SelectedValue = empCV.PER_WARD
                            cboPer_Ward.Text = empCV.PER_WARD_NAME
                        End If
                        txtHealthNo.Text = empCV.HEALTH_NO
                        txtNoiVaoDTN.Text = empCV.NOI_VAO_DTN
                        txtChucVuDTN.Text = empCV.CHUC_VU_DTN
                        txtLLCT.Text = empCV.TD_CHINHTRI
                        txtCBOSinhHoat.Text = empCV.CBO_SINHHOAT
                        txtSoLyLich.Text = empCV.SO_LYLICH
                        txtSoTheDang.Text = empCV.SOTHE_DANG
                        If empCV.NGAY_VAO_DTN IsNot Nothing Then
                            rdNgayVaoDTN.SelectedDate = empCV.NGAY_VAO_DTN
                        End If
                        txtHomePhone.Text = empCV.HOME_PHONE
                        txtMobilePhone.Text = empCV.MOBILE_PHONE
                        txtID_NO.Text = empCV.ID_NO
                        rdIDDate.SelectedDate = empCV.ID_DATE
                        SetValueComboBox(cboIDPlace, empCV.ID_PLACE, empCV.PLACE_NAME)
                        SetValueComboBox(rcPlacePitCodeMST, empCV.ID_PLACE, empCV.PLACE_NAME)
                        SetValueComboBox(rcNoiCapSSLD, empCV.SSLD_PLACE_ID, empCV.BOOK_NO)
                        SetValueComboBox(rcNoiCapVisa, empCV.VISA_PLACE_ID, empCV.VISA_PLACE)
                        SetValueComboBox(rcNoiCapHoChieu, empCV.PASS_PLACE_ID, empCV.PASS_PLACE)
                        SetValueComboBox(cboCONTACT_PER_PLACE_IDNO, empCV.ID_PLACE, empCV.PLACE_NAME)
                        txtPassNo.Text = empCV.PASS_NO
                        rdPassDate.SelectedDate = empCV.PASS_DATE
                        rdPassExpireDate.SelectedDate = empCV.PASS_EXPIRE
                        rcNoiCapHoChieu.Text = empCV.PASS_PLACE
                        txtVisa.Text = empCV.VISA
                        rdVisaDate.SelectedDate = empCV.VISA_DATE
                        rdVisaExpireDate.SelectedDate = empCV.VISA_EXPIRE
                        rcNoiCapVisa.Text = empCV.VISA_PLACE
                        txtPitCode.Text = empCV.PIT_CODE
                        txtPerEmail.Text = empCV.PER_EMAIL
                        txtWorkEmail.Text = empCV.WORK_EMAIL
                        txtContactPerson.Text = empCV.CONTACT_PER
                        txtContactPersonPhone.Text = empCV.CONTACT_PER_PHONE
                        txtContactMobilePhone.Text = empCV.CONTACT_PER_MBPHONE
                        If empCV.RELATION_PER_CTR IsNot Nothing Then
                            cboRelationNLH.SelectedValue = empCV.RELATION_PER_CTR
                            cboRelationNLH.Text = empCV.RELATION_PER_CTR_NAME
                        End If
                        If empCV.RELATE_OWNER IsNot Nothing Then
                            cboRELATE_OWNER.SelectedValue = empCV.RELATE_OWNER
                            cboRELATE_OWNER.Text = empCV.RELATE_OWNER
                        End If
                        txtAddressPerContract.Text = empCV.ADDRESS_PER_CTR
                        chkDoanPhi.Checked = False
                        If empCV.DOAN_PHI IsNot Nothing Then
                            chkDoanPhi.Checked = empCV.DOAN_PHI
                        End If
                        If empCV.BANK_ID IsNot Nothing Then
                            cboBank.SelectedValue = empCV.BANK_ID
                            cboBank.Text = empCV.BANK_NAME
                        End If
                        If empCV.BANK_BRANCH_ID IsNot Nothing Then
                            cboBankBranch.SelectedValue = empCV.BANK_BRANCH_ID
                            cboBankBranch.Text = empCV.BANK_BRANCH_NAME
                        End If
                        rdNgayVaoDoan.SelectedDate = empCV.NGAY_VAO_DOAN
                        txtNoiVaoDang.Text = empCV.NOI_VAO_DANG
                        txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                        txtBankNo.Text = empCV.BANK_NO
                        txtSoSoLaoDong.Text = empCV.BOOK_NO
                        If empCV.HEALTH_AREA_INS_NAME IsNot Nothing Then
                            rcNoiKhamChuaBenh.SelectedValue = empCV.HEALTH_AREA_INS_ID
                        End If
                        If IsNumeric(empCV.COPY_ADDRESS) Then
                            chkSaoChep.Checked = CType(empCV.COPY_ADDRESS, Boolean)
                        End If
                        If IsNumeric(empCV.CHECK_NAV) Then
                            chkTamtru.Checked = CType(empCV.CHECK_NAV, Boolean)
                        End If

                        If IsDate(empCV.BOOK_DATE) Then
                            rdNgayCapSSLD.SelectedDate = empCV.BOOK_DATE
                        End If
                        If IsDate(empCV.BOOK_EXPIRE) Then
                            rdNgayHetHanSSLD.SelectedDate = empCV.BOOK_EXPIRE
                        End If
                        If empCV.SSLD_PLACE_ID IsNot Nothing Then
                            rcNoiCapSSLD.Text = empCV.SSLD_PLACE_NAME
                            rcNoiCapSSLD.SelectedValue = empCV.SSLD_PLACE_ID
                        End If
                    End If
                    If empEdu IsNot Nothing Then
                        If empEdu.GRADUATION_YEAR IsNot Nothing Then
                            txtNamTN.Value = empEdu.GRADUATION_YEAR
                        End If
                        If empEdu.GRADUATE_SCHOOL_ID IsNot Nothing Then
                            cboGraduateSchool.SelectedValue = empEdu.GRADUATE_SCHOOL_ID
                            cboGraduateSchool.Text = empEdu.GRADUATE_SCHOOL_NAME
                        End If
                        If empEdu.ACADEMY IsNot Nothing Then
                            cboAcademy.SelectedValue = empEdu.ACADEMY
                            cboAcademy.Text = empEdu.ACADEMY_NAME
                        End If
                        If empEdu.LEARNING_LEVEL IsNot Nothing Then
                            cboLearningLevel.SelectedValue = empEdu.LEARNING_LEVEL
                            cboLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                        End If
                        If empEdu.LANGUAGE_LEVEL IsNot Nothing Then
                            cboLangLevel.SelectedValue = empEdu.LANGUAGE_LEVEL
                            cboLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                        End If
                        txtLangMark.Text = empEdu.LANGUAGE_MARK
                        If empEdu.MAJOR IsNot Nothing Then
                            cboMajor.SelectedValue = empEdu.MAJOR
                            cboMajor.Text = empEdu.MAJOR_NAME
                        End If
                        If empCV.PIT_ID_PLACE IsNot Nothing Then
                            rcPlacePitCodeMST.SelectedValue = empCV.PIT_ID_PLACE
                        End If
                        If empCV.CONTACT_PER_IDNO IsNot Nothing Then
                            txtCONTACT_PER_IDNO.Text = empCV.CONTACT_PER_IDNO.ToString.Trim
                        End If
                        If IsDate(empCV.CONTACT_PER_EFFECT_DATE_IDNO) Then
                            rdCONTACT_PER_EFFECT_DATE_IDNO.SelectedDate = empCV.CONTACT_PER_EFFECT_DATE_IDNO
                        End If
                        If IsDate(empCV.CONTACT_PER_EXPIRE_DATE_IDNO) Then
                            rdCONTACT_PER_EXPIRE_DATE_IDNO.SelectedDate = empCV.CONTACT_PER_EXPIRE_DATE_IDNO
                        End If
                        If IsNumeric(empCV.CONTACT_PER_PLACE_IDNO) Then
                            cboCONTACT_PER_PLACE_IDNO.SelectedValue = empCV.CONTACT_PER_PLACE_IDNO
                        End If
                        Dim dt_ins As DataTable
                        dt_ins = ProfileBusRepo.GET_INS_HEALTH_BY_EMPID(EmployeeInfo.ID)
                        If dt_ins.Rows.Count > 0 Then
                            txtHDBaoHiem.Text = dt_ins.Rows(0).Item("CONTRACT_INS_NO").ToString
                            txtDVBH.Text = dt_ins.Rows(0).Item("DVBH").ToString
                            txtChuongTrinhBH.Text = dt_ins.Rows(0).Item("NAME").ToString
                            If IsNumeric(dt_ins.Rows(0).Item("TIEN_CHBH")) Then
                                rnSoTien.Value = CDec(dt_ins.Rows(0).Item("TIEN_CHBH").ToString)
                            End If
                            If IsDate(dt_ins.Rows(0).Item("EFFECT_DATE")) Then
                                rdNgayHieuLuc.SelectedDate = CDate(dt_ins.Rows(0).Item("EFFECT_DATE"))
                            End If
                            If IsDate(dt_ins.Rows(0).Item("EXPIRE_DATE")) Then
                                rdNgayHetHL.SelectedDate = CDate(dt_ins.Rows(0).Item("EXPIRE_DATE"))
                            End If
                            If IsNumeric(dt_ins.Rows(0).Item("MONEY_INS")) Then
                                rnMoneyBH.Value = CDec(dt_ins.Rows(0).Item("MONEY_INS"))
                            End If
                        End If
                    End If
                    If empHealth IsNot Nothing Then
                        txtCanNang.Text = empHealth.CAN_NANG
                        txtChieuCao.Text = empHealth.CHIEU_CAO
                        txtMatPhai.Text = empHealth.MAT_PHAI
                        txtMatTrai.Text = empHealth.MAT_TRAI
                        txtNhomMau.Text = empHealth.NHOM_MAU
                        txtTim.Text = empHealth.TIM
                        txtHuyeAp.Text = empHealth.HUYET_AP
                        If empHealth.LOAI_SUCKHOE IsNot Nothing Then
                            rcLOAI_SUC_KHOE.SelectedValue = empHealth.LOAI_SUCKHOE
                            rcLOAI_SUC_KHOE.Text = empHealth.LOAI_SUCKHOE_NAME
                        End If
                        If empHealth.NGAY_KHAM IsNot Nothing Then
                            rdNGAY_KHAM.SelectedDate = empHealth.NGAY_KHAM
                        End If
                        rtGHI_CHU_SUC_KHOE.Text = empHealth.GHI_CHU_SUC_KHOE
                    End If
                End If
                isLoad = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtData
            Dim dtPlace As DataTable
            Dim dtPlace2 As DataTable
            Dim dtPlace_hcm As DataTable
            Dim dtDTNV As DataTable
            Dim dtRegion As DataTable
            Dim dtATTENDANT As DataTable
            Dim dINSProgram As DataTable

            dtPlace = ProfileRepo.GetProvinceList(True)
            dtPlace2 = ProfileRepo.GetProvinceList2(True)

            Dim filteredDataTable = dtPlace.AsEnumerable.Where(Function(dr) dr("CODE").ToString = "79")
            If filteredDataTable.Count > 0 Then
                dtPlace_hcm = filteredDataTable.CopyToDataTable
            End If

            FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
            FillRadCombobox(cboCONTACT_PER_PLACE_IDNO, dtPlace, "NAME", "ID")
            FillRadCombobox(rcPlacePitCodeMST, dtPlace, "NAME", "ID")
            FillRadCombobox(rcNoiCapSSLD, dtPlace, "NAME", "ID")
            FillRadCombobox(rcNoiCapVisa, dtPlace, "NAME", "ID")
            FillRadCombobox(rcNoiCapHoChieu, dtPlace, "NAME", "ID")
            FillRadCombobox(cbPROVINCENQ_ID, dtPlace2, "NAME", "ID")

            dtData = ProfileRepo.GetOtherList("HU_GRADUATE_SCHOOL")
            FillRadCombobox(cboGraduateSchool, dtData, "NAME", "ID")

            dtDTNV = ProfileRepo.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(rcOBJECT_EMPLOYEE, dtDTNV, "NAME", "ID")

            dtATTENDANT = ProfileRepo.GetOtherList("OBJECT_ATTENDANT", True)
            FillRadCombobox(rcOBJECT_ATTENDANT, dtATTENDANT, "NAME", "ID")

            dtRegion = ProfileRepo.Get_HU_WORK_PLACE("", True)
            FillRadCombobox(rcRegion, dtRegion, "NAME_VN", "ID")

            dINSProgram = ProfileRepo.GetInsListWhereHealth()
            FillRadCombobox(rcNoiKhamChuaBenh, dINSProgram, "NAME_VN", "ID")

            dtData = ProfileRepo.GetOtherList("OBJECT_ATTENDANCE")
            FillRadCombobox(cboObject, dtData, "NAME", "ID")

            Dim obj As Integer = ProfileRepo.GET_DEFAULT_OBJECT_ATTENDANCE()
            If obj > 0 Then
                cboObject.SelectedValue = obj
            End If

            If ComboBoxDataDTO Is Nothing Then
                ComboBoxDataDTO = New ComboBoxDataDTO
                ComboBoxDataDTO.GET_RELATION = True
                ComboBoxDataDTO.GET_LOCATION = True
                ComboBoxDataDTO.GET_RELATE_OWNER = True
                ProfileRepo.GetComboList(ComboBoxDataDTO)
            End If

            If ComboBoxDataDTO IsNot Nothing Then
                FillDropDownList(cboContractedUnit, ComboBoxDataDTO.LIST_LOCATION, "LOCATION_VN_NAME", "ID", Common.Common.SystemLanguage, True, cboRelationNLH.SelectedValue)
                FillDropDownList(cboRelationNLH, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationNLH.SelectedValue)
                FillDropDownList(cboRELATE_OWNER, ComboBoxDataDTO.LIST_RELATE_OWNER, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRELATE_OWNER.SelectedValue)
            End If
            If dtPlace_hcm IsNot Nothing AndAlso dtPlace_hcm.Rows.Count > 0 Then
                rcNoiCapSSLD.SelectedValue = dtPlace_hcm.Rows(0)("ID")
                rcNoiCapVisa.SelectedValue = dtPlace_hcm.Rows(0)("ID")
                rcNoiCapHoChieu.SelectedValue = dtPlace_hcm.Rows(0)("ID")
            End If

            Dim regions = ProfileRepo.GetOtherList("LOCATION", True)
            cboInsRegion.DataSource = regions
            cboInsRegion.DataTextField = "Name"
            cboInsRegion.DataValueField = "ID"

            Dim jobLevel = ProfileRepo.GetDataByProcedures(12, 0)
            If jobLevel IsNot Nothing AndAlso jobLevel.Rows.Count > 0 Then
                FillRadCombobox(cboJobLevel, jobLevel, "NAME_VN", "ID")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Edit,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phPopupDirect.Controls.Contains(ctrlFindEmployeePopup) Then
                phPopupDirect.Controls.Remove(ctrlFindEmployeePopup)
            End If
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phPopupLevel.Controls.Contains(ctrlFindEmployeePopup) Then
                phPopupLevel.Controls.Remove(ctrlFindEmployeePopup)
            End If
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phEmp_Find.Controls.Contains(ctrlFindEmployeePopup) Then
                phEmp_Find.Controls.Remove(ctrlFindEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
                Case 2
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeeDirectPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                    phPopupDirect.Controls.Add(ctrlFindEmployeePopup)
                Case 3
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeeLevelPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.LoadAllOrganization = True
                    phPopupLevel.Controls.Add(ctrlFindEmployeePopup)
                Case 4
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup_Find", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode_Find.Text
                    phEmp_Find.Controls.Add(ctrlFindEmployeePopup)
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    rpItemCheck.Visible = False
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus, cboInsRegion, txtEmployeeCode_Find, btnCurciculumViate)
                    EnableControlAll(True, txtOrgName2, btnFindOrg, cboTitle, cboJobLevel, txtTimeID, cboObject, cboContractedUnit, cboObjectLabor,
                                    rdDayPitcode, txtPerson_Inheritance, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, rtCHUC_VU_DOAN,
                                    txtBankNo, txtFirstNameVN, rtEmpCode_OLD, txtNamTN, txtHomePhone, txtID_NO, cboIDPlace, txtLastNameVN,
                                    txtMatPhai, txtMatTrai, txtMobilePhone, txtNavAddress, txtNhomMau, txtPassNo, rcNoiCapHoChieu, txtPerAddress,
                                    txtPerEmail, txtPitCode, txtTim, txtVisa, rcNoiCapVisa, txtWorkEmail, txtContactPerson, txtContactPersonPhone,
                                    txtContactMobilePhone, rdBirthDate, rdIDDate, rdSeniorityDate, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                    rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao, cboAcademy, cboGraduateSchool, cboBank, cboBankBranch,
                                    cboFamilyStatus, cboGender, cboLearningLevel, cboLangLevel, cboMajor, cboNationlity, cboNative, cboNav_Province,
                                    cboPer_Province, cboReligion, cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID,
                                    cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID, hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy,
                                    chkDoanPhi, ckCONG_DOAN, ckDANG, ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds, rcOBJECT_EMPLOYEE, rcRegion,
                                    rcOBJECT_ATTENDANT, chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD,
                                    rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO, rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO,
                                    cboCONTACT_PER_PLACE_IDNO, rtBookNo, rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN,
                                    txtLLCT, txtCBOSinhHoat, txtSoLyLich, txtSoTheDang, cboMaThe, cboRelationNLH, rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)
                    If Not Me.AllowCreate Then
                        txtFirstNameVN.ReadOnly = True
                        rtEmpCode_OLD.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If
                Case "Copy"
                    If CType(CommonConfig.dicConfig("APP_SETTING_19"), Boolean) Then
                        LineCopyWToExp.Visible = True
                    Else
                        LineCopyWToExp.Visible = False
                    End If
                    rpItemCheck.Visible = True
                    chkID_NO.Visible = False
                    chkNAME.Visible = False
                    btnWage.Visible = False
                    btnContract.Visible = False
                    btnContractAppendix.Visible = False
                    btnBeforeWorkExp.Visible = False
                    btnFamily.Visible = False
                    btnTraining.Visible = False
                    btnConcurrently.Visible = False
                    btnAllowance.Visible = False
                    btnWorkingProcess.Visible = False
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus, cboInsRegion)
                    EnableControlAll(True, txtOrgName2, btnFindOrg, cboJobLevel, cboTitle, txtmanager, cboObject, cboObjectLabor, rdDayPitcode, txtPerson_Inheritance, txtTimeID,
                                      rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, rtCHUC_VU_DOAN,
                                     txtBankNo,
                                       txtFirstNameVN, rtEmpCode_OLD, txtNamTN, txtHomePhone, txtID_NO, cboIDPlace, txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau, txtPassNo, rcNoiCapHoChieu, txtPerAddress, txtPerEmail, txtPitCode, txtTim,
                                       txtVisa, rcNoiCapVisa, txtWorkEmail, txtContactPerson, txtContactPersonPhone, txtContactMobilePhone, rdBirthDate, rdIDDate, rdSeniorityDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate, rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao,
                                       cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus, cboGender, cboLearningLevel, cboLangLevel,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province, cboReligion,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy, chkDoanPhi, ckCONG_DOAN, ckDANG,
                                        ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT,
                                       chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD, rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO,
                                       rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO, rtBookNo,
                                       rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN, txtLLCT, txtCBOSinhHoat,
                                       txtSoLyLich, txtSoTheDang, cboMaThe, cboRelationNLH,
                                        rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)
                    If Not Me.AllowCreate Then
                        txtFirstNameVN.ReadOnly = True
                        rtEmpCode_OLD.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                    End If
                Case CommonMessage.STATE_EDIT
                    rpItemCheck.Visible = False
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.WORK_STATUS IsNot Nothing Then
                        EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus, cboInsRegion, txtEmployeeCode_Find, btnCurciculumViate,
                                         txtOrgName2, btnFindOrg, cboJobLevel,
                                   cboTitle, txtDirectManager, cboContractedUnit,
                                   txtmanager, cboObjectLabor, txtTimeID, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT)
                        EnableControlAll(True, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, rtCHUC_VU_DOAN,
                                         rdDayPitcode,
                                        txtBankNo,
                                       txtFirstNameVN, rtEmpCode_OLD, txtNamTN, txtHomePhone, txtID_NO, cboIDPlace, txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau, txtPassNo, rcNoiCapHoChieu, txtPerAddress, txtPerEmail, txtPitCode, txtTim,
                                       txtVisa, rcNoiCapVisa, txtWorkEmail, txtContactPerson, txtContactPersonPhone, txtContactMobilePhone, rdBirthDate, rdIDDate, rdSeniorityDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate, rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao,
                                       cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus, cboGender, cboLearningLevel, cboLangLevel,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province, cboReligion,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy, chkDoanPhi, ckCONG_DOAN, ckDANG,
                                        ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds,
                                       chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD, rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO,
                                       rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO, rtBookNo,
                                       rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN, txtLLCT, txtCBOSinhHoat,
                                       txtSoLyLich, txtSoTheDang, cboMaThe, cboRelationNLH,
                                       rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)
                    Else
                        EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus, cboInsRegion, txtEmployeeCode_Find, btnCurciculumViate,
                                         txtOrgName2, btnFindOrg, cboJobLevel, cboContractedUnit,
                                   cboTitle, txtDirectManager,
                                   txtmanager, cboObjectLabor, txtTimeID, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT)
                        EnableControlAll(True, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, rtCHUC_VU_DOAN,
                                         rdDayPitcode,
                                        txtBankNo,
                                       txtFirstNameVN, rtEmpCode_OLD, txtNamTN, txtHomePhone, txtID_NO, cboIDPlace, txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau, txtPassNo, rcNoiCapHoChieu, txtPerAddress, txtPerEmail, txtPitCode, txtTim,
                                       txtVisa, rcNoiCapVisa, txtWorkEmail, txtContactPerson, txtContactPersonPhone, txtContactMobilePhone, rdBirthDate, rdIDDate, rdSeniorityDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate, rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao,
                                       cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus, cboGender, cboLearningLevel, cboLangLevel,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province, cboReligion,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy, chkDoanPhi, ckCONG_DOAN, ckDANG,
                                        ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds,
                                       chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD, rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO,
                                       rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO, rtBookNo,
                                       rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN, txtLLCT, txtCBOSinhHoat,
                                       txtSoLyLich, txtSoTheDang, cboMaThe, cboRelationNLH,
                                       rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)

                    End If
                    If Not Me.AllowModify Then
                        txtFirstNameVN.ReadOnly = True
                        rtEmpCode_OLD.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                        EnableControlAll(False, cboTitle, cboObject, btnFindOrg, cboJobLevel, cboObjectLabor)
                    End If
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                        EnableControlAll(False, cboTitle, btnFindOrg, cboJobLevel, cboContractedUnit, rcOBJECT_EMPLOYEE, cboObjectLabor, rdCoporationDate, rdSeniorityDate, rcRegion, rcOBJECT_ATTENDANT)
                    Else
                        EnableControlAll(True, cboTitle, btnFindOrg, cboJobLevel, cboContractedUnit, rcOBJECT_EMPLOYEE, cboObjectLabor, rdCoporationDate, rdSeniorityDate, rcRegion, rcOBJECT_ATTENDANT)
                    End If
                    EnableControlAll(True, rdCoporationDate, rdSeniorityDate)
                Case Else
                    rpItemCheck.Visible = False
                    If EmployeeInfo IsNot Nothing AndAlso IsNumeric(EmployeeInfo.WORK_STATUS) Then
                        EnableControlAll(True, btnCurciculumViate)
                        EnableControlAll(False, cboWorkStatus, cboEmpStatus, txtEmpCODE,
                                      txtBankNo, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, txtNamTN, txtFirstNameVN, rtEmpCode_OLD, rtCHUC_VU_DOAN,
                                      txtHomePhone, txtID_NO, cboIDPlace, txtTimeID,
                                      txtLastNameVN, txtMatPhai, txtMatTrai, txtMobilePhone, txtNavAddress, txtNhomMau,
                                      txtPassNo, rcNoiCapHoChieu, cboObject, cboObjectLabor, txtTimeID, rdDayPitcode, txtPerson_Inheritance,
                                      txtPerAddress, txtPerEmail, txtPitCode, txtTim, txtVisa, rcNoiCapVisa, txtWorkEmail,
                                      txtContactPerson, txtContactPersonPhone, txtContactMobilePhone, rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate, rdSeniorityDate,
                                      rdNgayVaoDoan, rdPassDate, rdPassExpireDate, rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao,
                                      cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus, cboGender, cboInsRegion,
                                      cboLearningLevel, cboLangLevel, cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province, cboReligion, cboTitle,
                                      cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy,
                                      chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID, btnFindOrg, cboJobLevel, cboContractedUnit, rtBookNo, ckCONG_DOAN, ckDANG,
                                      ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds, rcOBJECT_EMPLOYEE,
                                      rcRegion, rcOBJECT_ATTENDANT, chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD,
                                      rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO, rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO,
                                       rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN, txtLLCT, txtCBOSinhHoat,
                                       txtSoLyLich, txtSoTheDang, cboMaThe,
                                        cboObject, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT, cboRELATE_OWNER, cboRelationNLH, cboRelationNLH,
                                         rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)
                    Else
                        EnableControlAll(True, btnCurciculumViate, btnFindOrg, cboJobLevel, cboTitle, cboObjectLabor)
                        EnableControlAll(False, cboWorkStatus, cboEmpStatus, txtEmpCODE,
                                      txtBankNo, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, txtNamTN, txtFirstNameVN, rtEmpCode_OLD, rtCHUC_VU_DOAN,
                                      txtHomePhone, txtID_NO, cboIDPlace, txtTimeID,
                                      txtLastNameVN, txtMatPhai, txtMatTrai, txtMobilePhone, txtNavAddress, txtNhomMau,
                                      txtPassNo, rcNoiCapHoChieu, cboObject, cboObjectLabor, txtTimeID, rdDayPitcode, txtPerson_Inheritance,
                                      txtPerAddress, txtPerEmail, txtPitCode, txtTim, txtVisa, rcNoiCapVisa, txtWorkEmail,
                                      txtContactPerson, txtContactPersonPhone, txtContactMobilePhone, rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate, rdSeniorityDate,
                                      rdNgayVaoDoan, rdPassDate, rdPassExpireDate, rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao,
                                      cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus, cboGender, cboInsRegion,
                                      cboLearningLevel, cboLangLevel, cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province, cboReligion, cboTitle,
                                      cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, hidID, hidOrgID, hidDirectManager, hidLevelManager, hidIDCopy,
                                      chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID, btnFindOrg, cboJobLevel, cboContractedUnit, rtBookNo, ckCONG_DOAN, ckDANG,
                                      ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds, rcOBJECT_EMPLOYEE,
                                      rcRegion, rcOBJECT_ATTENDANT, chkSaoChep, chkTamtru, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD,
                                      rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO, rdCONTACT_PER_EFFECT_DATE_IDNO, rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO,
                                       rdCoporationDate, txtHealthNo, txtChucVuDTN, rdNgayVaoDTN, txtNoiVaoDTN, txtLLCT, txtCBOSinhHoat,
                                       txtSoLyLich, txtSoTheDang, cboMaThe,
                                        cboObject, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT, cboRELATE_OWNER, cboRelationNLH, cboRelationNLH,
                                        rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)
                    End If
                    EnableControlAll(True, rdCoporationDate, rdSeniorityDate)
            End Select
            ChangeToolbarState()
            Me.Send(CurrentState)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim _err As String = ""
            Dim strEmpID As Decimal
            If EmployeeInfo IsNot Nothing Then
                strEmpID = EmployeeInfo.ID
            End If
            Dim checkBank_No As Boolean = False
            Dim checkBlackList As Boolean = False
            Dim _param = New ParamDTO With {.ORG_ID = 46,
                       .IS_DISSOLVE = False}
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    ResetControlValue()
                    EmployeeInfo = Nothing
                    CurrentState = CommonMessage.STATE_NEW
                Case TOOLBARITEM_EDIT
                    If EmployeeInfo.WORK_STATUS Is Nothing Or
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_SAVE
                    Page.Validate("EmpProfile")
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case "Copy"
                                Dim employee_id As Decimal = Decimal.Parse(hidID.Value)
                                If Save(strEmpID, _err) Then
                                    ProfileStore.UPDATE_TERMINATE_COPY_STATUS(employee_id)
                                    Dim purl = String.Format("~/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&state=Normal&message=SUCCESS", strEmpID)
                                    Response.Redirect(purl, False)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                End If
                            Case STATE_NEW
                                If Not CType(CommonConfig.dicConfig("APP_SETTING_5"), Boolean) And cbDISTRICTEMP_ID.SelectedValue = "" Then
                                    ShowMessage("Quận/huyện nơi sinh bắt buộc chọn", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If Not CType(CommonConfig.dicConfig("APP_SETTING_6"), Boolean) And cbWARDEMP_ID.SelectedValue = "" Then
                                    ShowMessage("Xã/phường nơi sinh bắt buộc chọn", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If txtID_NO.Text <> "" Then
                                    If Not (txtID_NO.Text.Length > 8 And txtID_NO.Text.Length < 13) Then
                                        ShowMessage("CMND/CCID phải lớn hơn 8 và nhỏ hơn 13 số", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                Dim message As String = String.Empty
                                Dim blacklist As String = String.Empty
                                Dim fullname As String = String.Empty
                                Dim cmnd As String = String.Empty
                                checkBank_No = ProfileBusRepo.ValidateEmployee("EXIST_BANK_NO", "", txtBankNo.Text)
                                checkBlackList = ProfileBusRepo.ValidateEmployee("EXIST_ID_NO_TERMINATE", "", txtID_NO.Text)
                                If chkID_NO.Checked Then
                                    cmnd = txtID_NO.Text
                                Else
                                    cmnd = ""
                                End If
                                If chkNAME.Checked Then
                                    fullname = txtFirstNameVN.Text + " " + txtLastNameVN.Text
                                Else
                                    fullname = ""
                                End If
                                Dim ds1 = ProfileStore.GET_EMP_INF(fullname, cmnd)
                                If ds1.Tables(0) IsNot Nothing AndAlso ds1.Tables(0).Rows.Count > 0 And ds1.Tables(1) IsNot Nothing AndAlso ds1.Tables(1).Rows.Count > 0 Then
                                    rwPopup.VisibleOnPageLoad = True
                                    rwPopup.NavigateUrl = "/Dialog.aspx?mid=Profile&fid=ctrlHU_EmpError&group=Business&FULLNAME=" & fullname & "&ID_NO=" & txtID_NO.Text
                                    Exit Sub
                                Else
                                    If ds1.Tables(0) IsNot Nothing AndAlso ds1.Tables(0).Rows.Count > 0 Then
                                        rwPopup.VisibleOnPageLoad = True
                                        rwPopup.NavigateUrl = "/Dialog.aspx?mid=Profile&fid=ctrlHU_EmpError&group=Business&FULLNAME=" & "&ID_NO=" & txtID_NO.Text
                                        Exit Sub
                                    End If
                                    If ds1.Tables(1) IsNot Nothing AndAlso ds1.Tables(1).Rows.Count > 0 Then
                                        rwPopup.VisibleOnPageLoad = True
                                        rwPopup.NavigateUrl = "/Dialog.aspx?mid=Profile&fid=ctrlHU_EmpError&group=Business&FULLNAME=" & fullname & "&ID_NO="
                                        Exit Sub
                                    End If
                                End If
                                If Not checkBank_No Then
                                    message = If(message <> "", message + " Số tài khoản: " + txtBankNo.Text + " đã tồn tại.", "Số tài khoản: " + txtBankNo.Text + " đã tồn tại.")
                                End If
                                If Not checkBlackList Then
                                    blacklist = "Thông tin nhân viên có CMND vừa nhập đã tồn tại trong danh sách đen không tái tuyển dụng."
                                End If
                                If blacklist <> "" Then
                                    blacklist += "Bạn có muốn tuyển dụng lại nhân viên này không?"
                                    ctrlMessageBox.MessageText = Translate(blacklist)
                                    ctrlMessageBox.ActionName = "BLACKLIST"
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else
                                    If message <> "" Then
                                        message += " Bạn có muốn lưu không?"
                                        ctrlMessageBox.MessageText = Translate(message)
                                        ctrlMessageBox.ActionName = "WARNING"
                                        ctrlMessageBox.DataBind()
                                        ctrlMessageBox.Show()
                                    Else
                                        Save(strEmpID, _err)
                                    End If
                                End If
                            Case STATE_EDIT
                                If Not CType(CommonConfig.dicConfig("APP_SETTING_5"), Boolean) And cbDISTRICTEMP_ID.SelectedValue = "" Then
                                    ShowMessage("Quận/huyện nơi sinh bắt buộc chọn", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If Not CType(CommonConfig.dicConfig("APP_SETTING_6"), Boolean) And cbWARDEMP_ID.SelectedValue = "" Then
                                    ShowMessage("Xã/phường nơi sinh bắt buộc chọn", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rdCoporationDate.SelectedDate.HasValue AndAlso rdJoinDate.SelectedDate.HasValue AndAlso rdCoporationDate.SelectedDate > rdJoinDate.SelectedDate Then
                                    ShowMessage("Ngày vào tổng công ty không được lớn hơn ngày vào làm/thử việc", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rdSeniorityDate.SelectedDate.HasValue AndAlso rdJoinDate.SelectedDate.HasValue AndAlso rdSeniorityDate.SelectedDate > rdJoinDate.SelectedDate Then
                                    ShowMessage("Ngày tính thêm niên không được lớn hơn ngày vào làm/thử việc", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rdCoporationDate.SelectedDate.HasValue AndAlso rdSeniorityDate.SelectedDate.HasValue AndAlso rdCoporationDate.SelectedDate > rdSeniorityDate.SelectedDate Then
                                    ShowMessage("Ngày vào tổng công ty không được lớn hơn ngày tính thâm niên", Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim message As String = String.Empty
                                Dim blacklist As String = String.Empty
                                checkBlackList = ProfileBusRepo.ValidateEmployee("EXIST_ID_NO_TERMINATE", EmployeeInfo.EMPLOYEE_CODE, txtID_NO.Text)
                                If Not checkBlackList Then
                                    blacklist = "Thông tin nhân viên có CMND vừa nhập đã tồn tại trong danh sách đen không tái tuyển dụng."
                                End If
                                If blacklist <> "" Then
                                    blacklist += "Bạn có muốn tuyển dụng lại nhân viên này không?"
                                    ctrlMessageBox.MessageText = Translate(blacklist)
                                    ctrlMessageBox.ActionName = "BLACKLIST"
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else
                                    If message <> "" Then
                                        message += " Bạn có muốn lưu không?"
                                        ctrlMessageBox.MessageText = Translate(message)
                                        ctrlMessageBox.ActionName = "WARNING"
                                        ctrlMessageBox.DataBind()
                                        ctrlMessageBox.Show()
                                    Else
                                        Save(strEmpID, _err)
                                    End If
                                End If
                        End Select
                    End If
                Case TOOLBARITEM_CANCEL
                    If CurrentState = "Copy" Then
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TerminateCopy&group=Business")
                    Else
                        Dim purl = String.Format("~/Default.aspx?mid=Profile&fid=ctrlHU_EmployeeMng&group=Business")
                        Response.Redirect(purl, False)
                    End If
                Case TOOLBARITEM_DELETE
                    If EmployeeInfo.ID = 0 Then
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""
                Dim lstEmpID = New List(Of Decimal)
                lstEmpID.Add(EmployeeInfo.ID)
                ProfileBusRepo.DeleteEmployee(lstEmpID, strError)
                If strError = "" Then
                    ResetControlValue()
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NEW
                Else
                    ShowMessage(Translate("Nhân viên này đã có hợp đồng không thể xóa"), Utilities.NotifyType.Warning)
                    CurrentState = CommonMessage.STATE_NORMAL
                End If
                UpdateControlState()
            End If
            If e.ActionName = "BLACKLIST" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If Save(strEmpID, _err) Then
                            ProfileBusRepo.DeleteNVBlackList(txtID_NO.Text)
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                    Case CommonMessage.STATE_EDIT
                        If Save(strEmpID, _err) Then
                            ProfileBusRepo.DeleteNVBlackList(txtID_NO.Text)
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Dim purl = String.Format("~/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&state=Normal&message=SUCCESS", strEmpID)
                            Response.Redirect(purl, False)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                End Select
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.ACTION_ID_NO And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW

                        If Save(strEmpID, _err) Then
                            Page.Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&noscroll=1&message=success&reload=1")
                            Exit Sub
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                    Case CommonMessage.STATE_EDIT
                        If Save(strEmpID, _err) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                End Select
            End If
            If e.ActionName = "CHECK_TITLE" Then
                If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    Dim rep1 As New ProfileRepository
                    Dim obj = rep1.GetPositionID(Decimal.Parse(cboTitle.SelectedValue))
                    If obj IsNot Nothing Then
                        If obj.LM IsNot Nothing Then
                            txtDirectManager.Text = obj.LM_NAME
                            hidDirectManager.Value = obj.LM
                            txtmanager.Text = obj.EMP_LM
                        Else
                            txtDirectManager.Text = "-"
                            hidDirectManager.Value = Nothing
                            txtmanager.Text = "-"
                        End If
                        Dim jobLevel = rep1.GetDataByProcedures(12, 0)
                        If jobLevel IsNot Nothing AndAlso jobLevel.Rows.Count > 0 Then
                            FillRadCombobox(cboJobLevel, jobLevel, "NAME_VN", "ID")
                        End If
                    Else
                        txtDirectManager.Text = "-"
                        hidDirectManager.Value = Nothing
                        txtmanager.Text = "-"
                        ClearControlValue(cboJobLevel)
                    End If
                Else
                    cboTitle.ClearValue()
                End If
            End If
            If e.ActionName = "ACTION_BANK_NO" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If Save(strEmpID, _err) Then
                            Page.Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&noscroll=1&message=success&reload=1")
                            Exit Sub
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                End Select
            End If
            If e.ActionName = "INHERITANCE" And e.ButtonID = MessageBoxButtonType.ButtonNo Then
                ClearControlValue(txtPerson_Inheritance)
            End If
            If e.ActionName = "COMFIRM_MAIL" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Confirm_save()
            End If
            If e.ActionName = "WARNING" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If Save(EmployeeID, _err) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        If Save(EmployeeID, _err) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Dim purl = String.Format("~/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp={0}&state=Normal&message=SUCCESS", strEmpID)
                            Response.Redirect(purl, False)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                End Select
                UpdateControlState()
            Else
                Exit Sub
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboAcademy.ItemsRequested, cboBank.ItemsRequested, cboBankBranch.ItemsRequested,
        cboFamilyStatus.ItemsRequested, cboGender.ItemsRequested, cboLearningLevel.ItemsRequested, cboLangLevel.ItemsRequested, cboMajor.ItemsRequested,
         cboNationlity.ItemsRequested, cboNative.ItemsRequested, cboNav_Province.ItemsRequested,
        cboPer_Province.ItemsRequested, cboReligion.ItemsRequested, cboTitle.ItemsRequested,
        cboWorkStatus.ItemsRequested, cboEmpStatus.ItemsRequested, cboGraduateSchool.ItemsRequested, cbWARDEMP_ID.ItemsRequested, cbDISTRICTEMP_ID.ItemsRequested, cbPROVINCEEMP_ID.ItemsRequested,
        cboPer_District.ItemsRequested, cboPer_Ward.ItemsRequested, cboNav_District.ItemsRequested, cboNav_Ward.ItemsRequested, cbPROVINCENQ_ID.ItemsRequested, cboObjectLabor.ItemsRequested, rcLOAI_SUC_KHOE.ItemsRequested,
        cboRelationNLH.ItemsRequested, cboRELATE_OWNER.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtData As DataTable
            Dim sText As String = e.Text
            Dim dValue As Decimal
            Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
            Select Case sender.ID
                Case cboGraduateSchool.ID
                    dtData = ProfileRepo.GetOtherList("HU_GRADUATE_SCHOOL", True)
                Case cboObjectLabor.ID
                    dtData = ProfileRepo.GetOtherList("OBJECT_LABOR", True)
                Case cboAcademy.ID
                    Dim DT As DataTable
                    DT = ProfileRepo.GetOtherList("ACADEMY", True)
                    DT.DefaultView.Sort = "ID ASC"
                    dtData = DT.DefaultView.ToTable()
                Case cboBank.ID
                    dtData = ProfileRepo.GetBankList(True)
                Case cboBankBranch.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = ProfileRepo.GetBankBranchList(dValue, True)
                Case cboFamilyStatus.ID
                    dtData = ProfileRepo.GetOtherList("FAMILY_STATUS", True)
                Case cboGender.ID
                    dtData = ProfileRepo.GetOtherList("GENDER", True)
                Case cboLearningLevel.ID
                    dtData = ProfileRepo.GetOtherList("LEARNING_LEVEL", True)
                Case cboMajor.ID
                    dtData = ProfileRepo.GetOtherList("MAJOR", True)
                Case cboNationlity.ID
                    dtData = ProfileRepo.GetNationList(True)
                Case cboNative.ID
                    dtData = ProfileRepo.GetOtherList("NATIVE", True)
                Case cboNav_Province.ID, cboPer_Province.ID, cboIDPlace.ID
                    dtData = ProfileRepo.GetProvinceList(True)
                Case cbPROVINCENQ_ID.ID, cbPROVINCEEMP_ID.ID
                    dtData = ProfileRepo.GetProvinceList2(True)
                Case cboNav_District.ID, cboPer_District.ID, cbDISTRICTEMP_ID.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = ProfileRepo.GetDistrictList(dValue, True)
                Case cboNav_Ward.ID, cboPer_Ward.ID, cbWARDEMP_ID.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = ProfileRepo.GetWardList(dValue, True)
                Case cboReligion.ID
                    dtData = ProfileRepo.GetOtherList("RELIGION", True)
                Case cboTitle.ID
                    Dim dtDataTitle As DataTable
                    Dim obj As New ParamIDDTO
                    obj.ID = If(IsNumeric(hidOrgID.Value), Decimal.Parse(hidOrgID.Value), 0)
                    obj.EMPLOYEE_ID = 0
                    dtDataTitle = ProfileRepo.GetDataByProcedures(9, obj.ID, obj.EMPLOYEE_ID.ToString, Common.Common.SystemLanguage.Name)
                    cboTitle.DataSource = dtDataTitle
                    FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
                Case cboWorkStatus.ID
                    dtData = ProfileRepo.GetOtherList("WORK_STATUS", True)
                Case cboWorkStatus.ID
                    dtData = ProfileRepo.GetOtherList("WORK_STATUS", True)
                Case cboLangLevel.ID
                    dtData = ProfileRepo.GetOtherList("LANGUAGE_LEVEL", True)
                Case cboInsRegion.ID
                    dtData = ProfileRepo.GetInsRegionList(True)
                Case rcLOAI_SUC_KHOE.ID
                    dtData = ProfileRepo.GetOtherList("LOAISUCKHOE", True)
            End Select
            If sText <> "" Then
                Dim dtExist = (From p In dtData
                               Where p("NAME") IsNot DBNull.Value AndAlso
                              p("NAME").ToString.ToUpper = sText.ToUpper)
                Dim dtFilter = (From p In dtData
                                Where p("NAME") IsNot DBNull.Value AndAlso
                          p("NAME").ToString.ToUpper.Contains(sText.ToUpper))
                If dtFilter.Count > 0 Then
                    dtData = dtFilter.CopyToDataTable
                Else
                    dtData = dtData.Clone
                End If
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count
                sender.Items.Clear()
                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    Select Case sender.ID
                        Case cboTitle.ID
                            radItem.Attributes("JOB_POSITION_NAME") = dtData.Rows(i)("JOB_POSITION_NAME").ToString()
                    End Select
                    sender.Items.Add(radItem)
                Next
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count
                sender.Items.Clear()
                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    Select Case sender.ID
                        Case cboTitle.ID
                            radItem.Attributes("JOB_POSITION_NAME") = dtData.Rows(i)("JOB_POSITION_NAME").ToString()
                    End Select
                    sender.Items.Add(radItem)
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim chk = DirectCast(sender, CheckBox)
            Select Case chk.ID
                Case "chkSaoChep"
                    Dim dtData_Province As DataTable
                    Dim dtData_District As DataTable
                    Dim dtData_Ward As DataTable
                    If chkSaoChep.Checked = True Then
                        txtNavAddress.Text = txtPerAddress.Text
                        If cboPer_Province.SelectedValue IsNot Nothing Then
                            ClearControlValue(cboNav_Province, cboNav_District, cboNav_Ward)
                            dtData_Province = ProfileRepo.GetProvinceList(True)
                            FillRadCombobox(cboNav_Province, dtData_Province, "NAME", "ID")
                            cboNav_Province.SelectedValue = cboPer_Province.SelectedValue
                            If cboPer_District.SelectedValue IsNot Nothing Then
                                dtData_District = ProfileRepo.GetDistrictList(cboPer_Province.SelectedValue, True)
                                FillRadCombobox(cboNav_District, dtData_District, "NAME", "ID")
                                cboNav_District.SelectedValue = cboPer_District.SelectedValue
                                If cboPer_Ward.SelectedValue IsNot Nothing Then
                                    dtData_Ward = ProfileRepo.GetWardList(cboPer_District.SelectedValue, True)
                                    FillRadCombobox(cboNav_Ward, dtData_Ward, "NAME", "ID")
                                    cboNav_Ward.SelectedValue = cboPer_Ward.SelectedValue
                                End If
                            End If
                        End If
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim _param = New ParamDTO With {.ORG_ID = 46,
                   .IS_DISSOLVE = False}
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Select Case isLoadPopup
                    Case 2
                        txtDirectManager.Text = lstCommonEmployee(0).FULLNAME_VN
                        hidDirectManager.Value = lstCommonEmployee(0).ID.ToString()
                        txtmanager.Text = lstCommonEmployee(0).TITLE_NAME
                    Case 3
                    Case 4
                        Dim states As String = Request.QueryString("state")
                        Dim place As String = If(Request.QueryString("Place") <> "", "&place=" + Request.QueryString("Place"), "")
                        Dim Url As String = "/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" + lstCommonEmployee(0).ID.ToString() + place + "&state=" + states
                        Response.Redirect(Url)
                End Select
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlPopupCommon_CancelClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                FillDataInControls(e.CurrentValue)
            End If
            cboTitle.ClearValue()
            Dim dtDataTitle As DataTable
            Dim obj As New ParamIDDTO
            obj.ID = hidOrgID.Value
            obj.EMPLOYEE_ID = EmployeeID
            dtDataTitle = ProfileRepo.GetDataByProcedures(9, obj.ID, obj.EMPLOYEE_ID.ToString, Common.Common.SystemLanguage.Name)
            cboTitle.DataSource = dtDataTitle
            FillRadCombobox(cboTitle, dtDataTitle, "NAME", "ID", False)
            Dim objOrg = ProfileRepo.GetOrgOMByID(e.CurrentValue)
            txtCongTy.Text = objOrg.ORG_NAME2
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusTitle_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTitle.ServerValidate
        If cboTitle.Text = "" Or cboTitle.SelectedValue = "" Then
            args.IsValid = False
            Exit Sub
        End If
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/SalaryInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim crc As New Crc32()
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then

            Else
                strUpload = String.Empty
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub txtPerson_Inheritance_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim FullName = StrConv(txtFirstNameVN.Text, VbStrConv.ProperCase) & " " & StrConv(txtLastNameVN.Text, VbStrConv.ProperCase)
            Dim Per_In = StrConv(txtPerson_Inheritance.Text, VbStrConv.ProperCase)
            If Not Per_In.ToUpper.Equals(FullName.ToUpper) Then
                Dim Mess = "Người thụ hưởng khác tên nhân viên. Bạn có lưu hay không?"
                ctrlMessageBox.MessageText = Translate(Mess)
                ctrlMessageBox.ActionName = "INHERITANCE"
                ctrlMessageBox.DataBind()
                ctrlMessageBox.Show()
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtEmployeeCode_Find_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode_Find.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If txtEmployeeCode_Find.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of Common.CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New Common.CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEmployeeCode_Find.Text
                EmployeeList = CommonRepo.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                ElseIf Count = 1 Then
                    Dim states As String = Request.QueryString("state")
                    Dim place As String = If(Request.QueryString("Place") <> "", "&place=" + Request.QueryString("Place"), "")
                    Dim Url As String = "/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" + EmployeeList.Item(0).ID.ToString + place + "&state=" + states
                    Response.Redirect(Url)
                ElseIf Count > 1 Then
                    isLoadPopup = 4
                    UpdateControlState()
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.Show()
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTitle.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboTitle.SelectedValue <> "" Then
                Dim dtDataTitle As DataTable
                dtDataTitle = ProfileRepo.GetDataByProcedures(9, hidOrgID.Value, EmployeeID.ToString, Common.Common.SystemLanguage.Name)
                Dim dv As New DataView(dtDataTitle, "ID = " + cboTitle.SelectedValue.ToString + " AND (MASTER_NAME <> '' OR INTERIM_NAME <> '')", "", DataViewRowState.CurrentRows)
                Dim dTable As DataTable
                dTable = dv.ToTable
                If dTable.Rows.Count > 0 Then
                    ctrlMessageBox.MessageText = Translate("Vị trí này đã có người ngồi. Bạn có muốn chọn không?")
                    ctrlMessageBox.ActionName = "CHECK_TITLE"
                    ctrlMessageBox.Show()
                Else
                    Dim obj = ProfileRepo.GetPositionID(Decimal.Parse(cboTitle.SelectedValue))
                    If obj IsNot Nothing Then
                        If obj.LM IsNot Nothing Then
                            txtDirectManager.Text = obj.LM_NAME
                            hidDirectManager.Value = obj.LM
                            txtmanager.Text = obj.EMP_LM
                        Else
                            txtDirectManager.Text = "-"
                            hidDirectManager.Value = Nothing
                            txtmanager.Text = ""
                        End If
                    Else
                        txtDirectManager.Text = "-"
                        hidDirectManager.Value = Nothing
                        txtmanager.Text = ""
                    End If
                End If
                If cboTitle.SelectedValue <> "" Then
                    Dim jobLevel = ProfileRepo.GetDataByProcedures(12, 0)
                    If jobLevel IsNot Nothing AndAlso jobLevel.Rows.Count > 0 Then
                        FillRadCombobox(cboJobLevel, jobLevel, "NAME_VN", "ID")
                    End If
                End If
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub cboContractedUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboContractedUnit.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(cboInsRegion)
            If cboContractedUnit.SelectedValue <> "" Then
                Dim id = CDec(cboContractedUnit.SelectedValue)
                Dim regionID = (From p In ComboBoxDataDTO.LIST_LOCATION Where p.ID = id Select p.REGION).FirstOrDefault
                If regionID IsNot Nothing Then
                    cboInsRegion.SelectedValue = regionID
                End If
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtLastNameVN_TextChanged(sender As Object, e As EventArgs) Handles txtLastNameVN.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If hidID.Value = "" Then
                Dim Str = txtFirstNameVN.Text + " " + txtLastNameVN.Text
                Dim vnChar() As String
                vnChar = {
                    "aAeEoOuUiIdDyY",
                    "áàạảãâấầậẩẫăắằặẳẵ",
                    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ",
                    "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ",
                    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ",
                    "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ",
                    "ÍÌỊỈĨ",
                    "đ",
                    "Đ",
                    "ýỳỵỷỹ",
                    "ÝỲỴỶỸ"
                }
                For i As Decimal = 1 To vnChar.Length - 1
                    For j As Decimal = 0 To vnChar(i).Length - 1
                        Str = Str.Replace(vnChar(i)(j), vnChar(0)(i - 1))
                    Next
                Next
                txtPerson_Inheritance.Text = Str.ToUpper
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Public Sub ResetControlValue()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ClearControlValue(txtBankNo, txtDirectManager, txtmanager, txtFirstNameVN, rtEmpCode_OLD, txtHomePhone, txtID_NO, cboIDPlace, txtTimeID,
                          txtLastNameVN, txtMatPhai, txtMatTrai, txtMobilePhone, txtNavAddress, txtNhomMau, txtOrgName2, txtPassNo, rcNoiCapHoChieu, txtTimeID,
                          txtPerAddress, txtPerEmail, txtPitCode, txtTim, txtVisa, rcNoiCapVisa, txtWorkEmail, txtContactPerson, txtContactPersonPhone, txtContactMobilePhone,
                          rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate, rdSeniorityDate, rdJoinDate, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                          rdVisaDate, rdVisaExpireDate, txtCanNang, txtChieuCao, cboAcademy, cboGraduateSchool, cboBank, cboBankBranch, cboFamilyStatus,
                          cboGender, cboInsRegion,
                          cboLearningLevel, cboLangLevel, cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                          cboReligion, cboTitle, cboWorkStatus, cboEmpStatus, cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                          hidID, hidIDCopy, hidOrgID, hidDirectManager, hidLevelManager, chkDoanPhi, rcOBJECT_EMPLOYEE, rcRegion, rcOBJECT_ATTENDANT, chkSaoChep, chkTamtru,
                          txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, rcNoiCapSSLD, rcNoiKhamChuaBenh, rcPlacePitCodeMST, txtCONTACT_PER_IDNO, rdCONTACT_PER_EFFECT_DATE_IDNO,
                          rdCONTACT_PER_EXPIRE_DATE_IDNO, cboCONTACT_PER_PLACE_IDNO,
                          rdNGAY_KHAM, rcLOAI_SUC_KHOE, rtGHI_CHU_SUC_KHOE)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function Save(ByRef strEmpID As Decimal, Optional ByRef _err As String = "") As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim Is_Copy = False

            Select Case CurrentState
                'Case STATE_NEW
                '    If Not ProfileBusRepo.ValidateEmployee("EXIST_WORK_EMAIL", "", txtWorkEmail.Text) Then
                '        ShowMessage("Địa chỉ email đã tồn tại", NotifyType.Warning)
                '        Exit Function
                '    End If
                Case "Copy"
                    EMP_ID_OLD = hidID.Value
                    hidID.Value = ""
                    Is_Copy = True
                    'Case STATE_EDIT
                    '    If Not ProfileBusRepo.ValidateEmployee("EXIST_WORK_EMAIL", EmployeeInfo.EMPLOYEE_CODE, txtWorkEmail.Text) Then
                    '        ShowMessage("Địa chỉ email đã tồn tại", NotifyType.Warning)
                    '        Exit Function
                    '    End If
            End Select
            If cboNationlity.Text = "Việt Nam" Then
                Dim msg = ""
                If txtID_NO.Text = "" Then
                    msg = "Bạn phải nhập CMND/CCID"
                End If
                If rdIDDate.SelectedDate Is Nothing Then
                    msg = If(msg = "", "Bạn phải chọn Ngày cấp", msg + vbNewLine + "Bạn phải chọn Ngày cấp")
                End If
                If cboIDPlace.SelectedValue = "" Then
                    msg = If(msg = "", "Bạn phải chọn Nơi cấp", msg + vbNewLine + "Bạn phải chọn Nơi cấp")
                End If
                If msg <> "" Then
                    ShowMessage(msg, NotifyType.Warning)
                    Exit Function
                End If
            Else
                If txtPassNo.Text = "" Then
                    ShowMessage("Bạn phải nhập Số hộ chiếu", NotifyType.Warning)
                    Exit Function
                End If
            End If
            If txtID_NO.Text.Length <> 9 And txtID_NO.Text.Length <> 12 Then
                ShowMessage("CMND/CCID phải 9 hoặc 12 số", NotifyType.Warning)
                Exit Function
            End If
            If EmployeeInfo Is Nothing Then
                EmployeeInfo = New EmployeeDTO
            End If
            EmployeeInfo.IMAGE_URL = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage"
            If hidID.Value.Trim = "" Then
                EmployeeInfo.ID = 0
            Else
                EmployeeInfo.ID = Decimal.Parse(hidID.Value)
            End If
            If cboObject.SelectedValue <> "" Then
                EmployeeInfo.OBJECTTIMEKEEPING = cboObject.SelectedValue
                EmployeeInfo.OBJECTTIMEKEEPING_NAME = cboObject.Text
            End If
            EmployeeInfo.EMPLOYEE_CODE = txtEmpCODE.Text
            If cboObjectLabor.SelectedValue <> "" Then
                EmployeeInfo.OBJECT_LABOR = cboObjectLabor.SelectedValue
                EmployeeInfo.OBJECT_LABOR_NAME = cboObjectLabor.Text
            End If
            If cboJobLevel.SelectedValue <> "" Then
                EmployeeInfo.STAFF_RANK_ID = cboJobLevel.SelectedValue
            End If
            If hidDirectManager.Value <> "" Then
                EmployeeInfo.DIRECT_MANAGER = hidDirectManager.Value
            End If
            EmployeeInfo.DIRECT_MANAGER_TITLE_NAME = txtDirectManager.Text
            If hidLevelManager.Value <> "" Then
                EmployeeInfo.LEVEL_MANAGER = hidLevelManager.Value
            End If
            If cboContractedUnit.SelectedValue <> "" Then
                EmployeeInfo.CONTRACTED_UNIT = cboContractedUnit.SelectedValue
            End If
            If Is_Copy = True Then
                If txtTimeID.Text <> "" Then
                    EmployeeInfo.ITIME_ID = txtTimeID.Text
                Else
                    EmployeeInfo.ITIME_ID = ""
                End If
            Else
                If txtEmpCODE.Text <> "" Then
                    EmployeeInfo.ITIME_ID = txtEmpCODE.Text
                End If
            End If
            EmployeeInfo.DIRECT_MANAGER_NAME = txtmanager.Text
            EmployeeInfo.EMPLOYEE_CODE_OLD = rtEmpCode_OLD.Text
            EmployeeInfo.BOOKNO = rtBookNo.Text
            EmployeeInfo.BOOK_NO_SOCIAL = rtBookNo.Text
            EmployeeInfo.EMPLOYEE_CODE = txtEmpCODE.Text.Trim
            EmployeeInfo.FIRST_NAME_VN = StrConv(txtFirstNameVN.Text, VbStrConv.ProperCase)
            EmployeeInfo.FULLNAME_VN = StrConv(txtFirstNameVN.Text, VbStrConv.ProperCase) & " " & StrConv(txtLastNameVN.Text, VbStrConv.ProperCase)
            EmployeeInfo.LAST_NAME_VN = StrConv(txtLastNameVN.Text, VbStrConv.ProperCase)
            EmployeeInfo.ORG_ID = hidOrgID.Value
            EmployeeInfo.SENIORITY_DATE = rdSeniorityDate.SelectedDate
            EmployeeInfo.TITLE_ID = If(cboTitle.Text.Equals(""), Nothing, cboTitle.SelectedValue)
            If cboTitle.SelectedValue <> "" Then
                EmployeeInfo.TITLE_NAME_VN = cboTitle.Text
            End If
            If cboWorkStatus.SelectedValue <> "" Then
                EmployeeInfo.WORK_STATUS = cboWorkStatus.SelectedValue
            End If
            If cboEmpStatus.SelectedValue <> "" Then
                EmployeeInfo.EMP_STATUS = cboEmpStatus.SelectedValue
            End If
            If rcOBJECT_EMPLOYEE.SelectedValue <> "" Then
                EmployeeInfo.OBJECT_EMPLOYEE_ID = rcOBJECT_EMPLOYEE.SelectedValue
            End If
            EmployeeInfo.FOREIGN = chkForeign.Checked
            If rcRegion.SelectedValue <> "" Then
                EmployeeInfo.WORK_PLACE_ID = rcRegion.SelectedValue
            End If
            If rcOBJECT_ATTENDANT.SelectedValue <> "" Then
                EmployeeInfo.OBJECT_ATTENDANT_ID = rcOBJECT_ATTENDANT.SelectedValue
            End If
            If cboMaThe.SelectedValue <> "" Then
                EmployeeInfo.MATHE = cboMaThe.SelectedValue
            End If
            If IsDate(rdCoporationDate.SelectedDate) Then
                EmployeeInfo.COPORATION_DATE = rdCoporationDate.SelectedDate
            End If

            EmpCV = New EmployeeCVDTO
            EmpCV.PERSON_INHERITANCE = txtPerson_Inheritance.Text
            EmpCV.HEALTH_NO = txtHealthNo.Text.Trim()
            EmpCV.PIT_CODE_DATE = rdDayPitcode.SelectedDate
            If cboInsRegion.SelectedValue <> "" Then
                EmpCV.INS_REGION_ID = Decimal.Parse(cboInsRegion.SelectedValue)
            End If
            If cboGender.SelectedValue <> "" Then
                EmpCV.GENDER = Decimal.Parse(cboGender.SelectedValue)
            End If
            If rdBirthDate.SelectedDate IsNot Nothing Then
                EmpCV.BIRTH_DATE = rdBirthDate.SelectedDate
            End If
            If cboFamilyStatus.SelectedValue <> "" Then
                EmpCV.MARITAL_STATUS = Decimal.Parse(cboFamilyStatus.SelectedValue)
            End If
            If cboReligion.SelectedValue <> "" Then
                EmpCV.RELIGION = Decimal.Parse(cboReligion.SelectedValue)
            End If
            If IsNumeric(cbPROVINCEEMP_ID.SelectedValue) Then
                EmpCV.PROVINCEEMP_ID = cbPROVINCEEMP_ID.SelectedValue
            End If
            If IsNumeric(cbDISTRICTEMP_ID.SelectedValue) Then
                EmpCV.DISTRICTEMP_ID = cbDISTRICTEMP_ID.SelectedValue
            End If
            If IsNumeric(cbWARDEMP_ID.SelectedValue) Then
                EmpCV.WARDEMP_ID = cbWARDEMP_ID.SelectedValue
            End If
            If cboNative.SelectedValue <> "" Then
                EmpCV.NATIVE = Decimal.Parse(cboNative.SelectedValue)
            End If
            If cboNationlity.SelectedValue <> "" Then
                EmpCV.NATIONALITY = Decimal.Parse(cboNationlity.SelectedValue)
            End If
            EmpCV.NAV_ADDRESS = txtNavAddress.Text.Trim()
            If cboNav_Province.SelectedValue <> "" Then
                EmpCV.NAV_PROVINCE = Decimal.Parse(cboNav_Province.SelectedValue)
            End If
            If cboNav_District.SelectedValue <> "" Then
                EmpCV.NAV_DISTRICT = Decimal.Parse(cboNav_District.SelectedValue)
            End If
            If cboNav_Ward.SelectedValue <> "" Then
                EmpCV.NAV_WARD = Decimal.Parse(cboNav_Ward.SelectedValue)
            End If
            EmpCV.PER_ADDRESS = txtPerAddress.Text.Trim()
            If cboPer_Province.SelectedValue <> "" Then
                EmpCV.PER_PROVINCE = Decimal.Parse(cboPer_Province.SelectedValue)
            End If
            If cboPer_District.SelectedValue <> "" Then
                EmpCV.PER_DISTRICT = Decimal.Parse(cboPer_District.SelectedValue)
            End If
            If cboPer_Ward.SelectedValue <> "" Then
                EmpCV.PER_WARD = Decimal.Parse(cboPer_Ward.SelectedValue)
            End If
            EmpCV.HOME_PHONE = txtHomePhone.Text.Trim()
            EmpCV.MOBILE_PHONE = txtMobilePhone.Text.Trim()
            EmpCV.ID_NO = txtID_NO.Text.Trim()
            EmpCV.ID_DATE = rdIDDate.SelectedDate
            EmpCV.ID_PLACE = GetValueFromComboBox(cboIDPlace)
            EmpCV.PASS_NO = txtPassNo.Text.Trim()
            EmpCV.PASS_DATE = rdPassDate.SelectedDate
            EmpCV.PASS_EXPIRE = rdPassExpireDate.SelectedDate
            EmpCV.PASS_PLACE = rcNoiCapHoChieu.Text.Trim()
            If IsNumeric(rcNoiCapHoChieu.SelectedValue) Then
                EmpCV.PASS_PLACE_ID = Decimal.Parse(rcNoiCapHoChieu.SelectedValue)
            End If
            EmpCV.VISA = txtVisa.Text.Trim()
            EmpCV.VISA_DATE = rdVisaDate.SelectedDate
            EmpCV.VISA_EXPIRE = rdVisaExpireDate.SelectedDate
            EmpCV.VISA_PLACE = rcNoiCapVisa.Text.ToString
            If IsNumeric(rcNoiCapVisa.SelectedValue) Then
                EmpCV.VISA_PLACE_ID = Decimal.Parse(rcNoiCapVisa.SelectedValue)
            End If
            EmpCV.PIT_CODE = txtPitCode.Text.Trim()
            EmpCV.PER_EMAIL = txtPerEmail.Text.Trim()
            EmpCV.WORK_EMAIL = If(String.IsNullOrEmpty(txtWorkEmail.Text.Trim()), txtPerEmail.Text.Trim(), txtWorkEmail.Text.Trim())
            EmpCV.CONTACT_PER = txtContactPerson.Text.Trim()
            EmpCV.CONTACT_PER_PHONE = txtContactPersonPhone.Text.Trim()
            EmpCV.CONTACT_PER_MBPHONE = txtContactMobilePhone.Text
            If cboRelationNLH.SelectedValue <> "" Then
                EmpCV.RELATION_PER_CTR = Decimal.Parse(cboRelationNLH.SelectedValue)
            End If
            If cboRELATE_OWNER.SelectedValue <> "" Then
                EmpCV.RELATE_OWNER = Decimal.Parse(cboRELATE_OWNER.SelectedValue)
            End If
            EmpCV.ADDRESS_PER_CTR = txtAddressPerContract.Text.Trim()
            EmpCV.DOAN_PHI = CType(chkDoanPhi.Checked, Decimal)
            If cboBank.SelectedValue <> "" Then
                EmpCV.BANK_ID = Decimal.Parse(cboBank.SelectedValue)
            End If
            If cboBankBranch.SelectedValue <> "" Then
                EmpCV.BANK_BRANCH_ID = Decimal.Parse(cboBankBranch.SelectedValue)
            End If
            If IsDate(rdNgayVaoDTN.SelectedDate) Then
                EmpCV.NGAY_VAO_DTN = rdNgayVaoDTN.SelectedDate
            End If
            EmpCV.NOI_VAO_DTN = txtNoiVaoDTN.Text.Trim()
            EmpCV.CHUC_VU_DTN = txtChucVuDTN.Text.Trim()
            EmpCV.TD_CHINHTRI = txtLLCT.Text.Trim()
            EmpCV.CBO_SINHHOAT = txtCBOSinhHoat.Text.Trim()
            EmpCV.SO_LYLICH = txtSoLyLich.Text.Trim()
            EmpCV.SOTHE_DANG = txtSoTheDang.Text.Trim()
            EmpCV.NGAY_VAO_DOAN = rdNgayVaoDoan.SelectedDate
            EmpCV.NOI_VAO_DANG = txtNoiVaoDang.Text.Trim()
            EmpCV.BANK_NO = txtBankNo.Text.Trim()
            If IsNumeric(cbPROVINCENQ_ID.SelectedValue) Then
                EmpCV.PROVINCENQ_ID = cbPROVINCENQ_ID.SelectedValue
            End If
            If IsNumeric(ckDANG.Checked) Then
                EmpCV.DANG = CType(ckDANG.Checked, Decimal)
            End If
            If IsNumeric(ckCHUHO.Checked) Then
                EmpCV.IS_CHUHO = CType(ckCHUHO.Checked, Decimal)
            End If
            If IsNumeric(ckCONG_DOAN.Checked) Then
                EmpCV.CONG_DOAN = CType(ckCONG_DOAN.Checked, Decimal)
            End If
            If IsDate(rdNGAY_VAO_DANG.SelectedDate) Then
                EmpCV.NGAY_VAO_DANG = rdNGAY_VAO_DANG.SelectedDate
            End If
            If IsDate(rdNGAY_VAO_DANG_DB.SelectedDate) Then
                EmpCV.NGAY_VAO_DANG_DB = rdNGAY_VAO_DANG_DB.SelectedDate
            End If
            EmpCV.CHUC_VU_DANG = rtCHUC_VU_DANG.Text
            EmpCV.NO_HOUSEHOLDS = txtNoHouseHolds.Text
            EmpCV.CODE_HOUSEHOLDS = txtCodeHouseHolds.Text
            EmpCV.CHUC_VU_DOAN = rtCHUC_VU_DOAN.Text
            EmpCV.NOI_VAO_DOAN = txtNoiVaoDoan.Text
            If IsNumeric(chkSaoChep.Checked) Then
                EmpCV.COPY_ADDRESS = CType(chkSaoChep.Checked, Decimal)
            End If
            If IsNumeric(chkTamtru.Checked) Then
                EmpCV.CHECK_NAV = CType(chkTamtru.Checked, Decimal)
            End If
            EmpCV.BOOK_NO = txtSoSoLaoDong.Text
            If IsDate(rdNgayCapSSLD.SelectedDate) Then
                EmpCV.BOOK_DATE = rdNgayCapSSLD.SelectedDate
            End If
            If IsDate(rdNgayHetHanSSLD.SelectedDate) Then
                EmpCV.BOOK_EXPIRE = rdNgayHetHanSSLD.SelectedDate
            End If
            If IsNumeric(rcNoiCapSSLD.SelectedValue) Then
                EmpCV.SSLD_PLACE_ID = rcNoiCapSSLD.SelectedValue
            End If
            If IsNumeric(rcNoiKhamChuaBenh.SelectedValue) Then
                EmpCV.HEALTH_AREA_INS_ID = rcNoiKhamChuaBenh.SelectedValue
            End If
            EmpCV.CONTACT_PER_IDNO = txtCONTACT_PER_IDNO.Text.Trim()
            If IsDate(rdCONTACT_PER_EFFECT_DATE_IDNO.SelectedDate) Then
                EmpCV.CONTACT_PER_EFFECT_DATE_IDNO = rdCONTACT_PER_EFFECT_DATE_IDNO.SelectedDate
            End If
            If IsDate(rdCONTACT_PER_EXPIRE_DATE_IDNO.SelectedDate) Then
                EmpCV.CONTACT_PER_EXPIRE_DATE_IDNO = rdCONTACT_PER_EXPIRE_DATE_IDNO.SelectedDate
            End If
            If IsNumeric(cboCONTACT_PER_PLACE_IDNO.SelectedValue) Then
                EmpCV.CONTACT_PER_PLACE_IDNO = cboCONTACT_PER_PLACE_IDNO.SelectedValue
            End If
            If IsNumeric(rcPlacePitCodeMST.SelectedValue) Then
                EmpCV.PIT_ID_PLACE = rcPlacePitCodeMST.SelectedValue
            End If

            EmpHealth = New EmployeeHealthDTO
            EmpHealth.CAN_NANG = txtCanNang.Text
            EmpHealth.CHIEU_CAO = txtChieuCao.Text
            EmpHealth.MAT_PHAI = txtMatPhai.Text.Trim()
            EmpHealth.MAT_TRAI = txtMatTrai.Text.Trim()
            EmpHealth.NHOM_MAU = txtNhomMau.Text.Trim()
            EmpHealth.HUYET_AP = txtHuyeAp.Text.Trim()
            EmpHealth.TIM = txtTim.Text.Trim()
            If IsDate(rdNGAY_KHAM.SelectedDate) Then
                EmpHealth.NGAY_KHAM = Date.Parse(rdNGAY_KHAM.SelectedDate)
            End If
            If IsNumeric(rcLOAI_SUC_KHOE.SelectedValue) Then
                EmpHealth.LOAI_SUCKHOE = Decimal.Parse(rcLOAI_SUC_KHOE.SelectedValue)
            End If
            EmpHealth.GHI_CHU_SUC_KHOE = rtGHI_CHU_SUC_KHOE.Text

            EmpEdu = New EmployeeEduDTO
            If cboGraduateSchool.SelectedValue <> "" Then
                EmpEdu.GRADUATE_SCHOOL_ID = cboGraduateSchool.SelectedValue
                EmpEdu.GRADUATE_SCHOOL_NAME = cboGraduateSchool.Text
            End If
            If txtNamTN.Text <> "" Then
                EmpEdu.GRADUATION_YEAR = txtNamTN.Value
            End If
            If cboAcademy.SelectedValue <> "" Then
                EmpEdu.ACADEMY = cboAcademy.SelectedValue
            End If
            If cboLearningLevel.SelectedValue <> "" Then
                EmpEdu.LEARNING_LEVEL = cboLearningLevel.SelectedValue
            End If
            If cboLangLevel.SelectedValue <> "" Then
                EmpEdu.LANGUAGE_LEVEL = cboLangLevel.SelectedValue
            End If
            EmpEdu.LANGUAGE_MARK = txtLangMark.Text
            If cboMajor.SelectedValue <> "" Then
                EmpEdu.MAJOR = cboMajor.SelectedValue

            End If
            If ImageFile IsNot Nothing Then
                Dim bytes(ImageFile.ContentLength - 1) As Byte
                ImageFile.InputStream.Read(bytes, 0, ImageFile.ContentLength)
                _binaryImage = bytes
                EmpCV.IMAGE = ImageFile.GetExtension
            Else
                EmpCV.IMAGE = ""
            End If

            If Is_Copy Then
                EmployeeInfo.WORK_STATUS = Nothing
            End If
            If Not String.IsNullOrEmpty(txtWorkEmail.Text) Then
                Dim lstObj = ProfileBusRepo.GetEmployeeByEmail(txtWorkEmail.Text, 0, Integer.MaxValue, 0, New ParamDTO With {.ORG_ID = 1, .EMP_CODE = txtEmpCODE.Text, .IS_DISSOLVE = False}).Any
                If lstObj Then
                    ctrlMessageBox.MessageText = "Mail công ty đã tồn tại trên hệ thống, bạn có chắc chắn muốn lưu hay không?"
                    ctrlMessageBox.ActionName = "COMFIRM_MAIL"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    'ShowMessage(Translate("Email đã tồn tại trong hệ thống"), Utilities.NotifyType.Warning)
                Else
                    Confirm_save()
                End If
            Else
                Confirm_save()
            End If
            Return result
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    Public Sub Confirm_save()
        Dim _err As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If hidID.Value <> "" Then
                EmployeeInfo.ID = Decimal.Parse(hidID.Value)
                result = ProfileBusRepo.ModifyEmployee(EmployeeInfo, gID, _binaryImage,
                                            EmpCV,
                                            EmpEdu,
                                            EmpHealth)
            Else
                result = ProfileBusRepo.InsertEmployee(EmployeeInfo, gID, gEmpCode, _binaryImage,
                                            EmpCV,
                                            EmpEdu,
                                            EmpHealth)
                EmployeeInfo.EMPLOYEE_CODE = gEmpCode
            End If
            EmployeeInfo.ID = gID
            Select Case CurrentState
                Case "Copy"
                    Dim Cal_Day
                    Dim SENIORITY
                    If EmployeeInfo.JOIN_DATE Is Nothing AndAlso EmployeeInfo.TER_EFFECT_DATE Is Nothing Then
                        Cal_Day = 0
                        SENIORITY = 0
                    Else
                        Cal_Day = Math.Round((CDate(EmployeeInfo.TER_EFFECT_DATE).Subtract(CDate(EmployeeInfo.JOIN_DATE)).TotalDays) + 1, 2)
                        SENIORITY = Math.Round(Cal_Day / 365 * 12, 2)
                    End If
                    If ProfileStore.COPY_INF_EMP_TERMINATED(ChkCopy_Woking.Checked, ChkCopy_Salary.Checked, ChkCopy_Training.Checked, ChkCopy_Family.Checked,
                                            ChkCopy_ExpWorking.Checked, ChkCopy_WorkingToExp.Checked, ChkCal_SENIORITY.Checked, SENIORITY, EmployeeInfo.ID, LogHelper.GetUserLog().Username.ToUpper, EMP_ID_OLD) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate("Đã có lỗi trong quá trình sao chép"), Utilities.NotifyType.Warning)
                    End If
            End Select
            isLoad = False
            UpdateControlState()
            Refresh()
            If result Then
                Dim purl = String.Format("~/Default.aspx?mid=Profile&fid=ctrlHU_EmployeeMng&group=Business")
                Response.Redirect(purl, False)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub FillDataInControls(ByVal orgid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        cboMaThe.Items.Clear()
        Dim org = ProfileRepo.GetOrganizationByID(orgid)
        If org IsNot Nothing Then
            cboInsRegion.ClearValue()
            SetValueComboBox(cboInsRegion, org.REGION_ID, Nothing)
            txtOrgName2.Text = org.NAME_VN
            txtOrgName2.ToolTip = org.NAME_VN
            Dim lstData = ProfileRepo.GetTitleBLDsByOrg(orgid, If(EmployeeInfo IsNot Nothing, EmployeeInfo.ID, Nothing))
            FillRadCombobox(cboMaThe, lstData, "NAME_VN", "ID")
        End If
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        Finally
        End Try
    End Sub

    Private Function UpperCaseFirst(ByVal str As String) As String
        Try
            If String.IsNullOrEmpty(str) = True Then
                Return ""
            Else
                Return Char.ToUpper(str(0)) + str.Substring(1).ToLower
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ChkCal_SENIORITY_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkCal_SENIORITY.CheckedChanged
        Try
            If rdJoinDate.SelectedDate Is Nothing Then
                ShowMessage(Translate("Bạn phải chọn ngày nhận việc trước"), Utilities.NotifyType.Warning)
                ChkCal_SENIORITY.Checked = False
                Exit Sub
            End If
            If EmployeeInfo.TER_EFFECT_DATE Is Nothing Then
                Exit Sub
            End If
            txtMonths.Text = Math.Round((rdJoinDate.SelectedDate - EmployeeInfo.TER_EFFECT_DATE).Value.Days / 365 * 12, 2)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub ckCHUHO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckCHUHO.CheckedChanged
        If ckCHUHO.Checked = True Then
            cboRELATE_OWNER.Enabled = False
            ClearControlValue(cboRELATE_OWNER)
        Else
            cboRELATE_OWNER.Enabled = True
        End If
    End Sub

    Private Function CalculateSeniority(ByVal dStart As Date?) As String
        Dim dSoNam As Double = 0
        Dim dSoThang As Double = 0
        Dim Cal_Month_Emp As Int32 = 0
        Dim Total_Month As Decimal = 0
        Dim str As String = ""
        Try
            If IsDate(dStart) Then
                Cal_Month_Emp = (DateDiff(DateInterval.Month, CDate(dStart), CDate(Date.Now.Date)))
                Total_Month = Math.Round(Cal_Month_Emp, 2)
                If IsNumeric(Total_Month) Then
                    dSoNam = Total_Month \ 12
                    dSoThang = Math.Round(CDec(Total_Month) Mod 12, 2)
                    str = If(dSoNam > 0, dSoNam.ToString + " Năm ", "") + If(Math.Round(CDec(dSoThang) Mod 12, 2) > 0, Math.Round(CDec(dSoThang) Mod 12, 2).ToString + " Tháng", "")
                End If
            End If
            Return str
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
Public Class ParamIDDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
End Class