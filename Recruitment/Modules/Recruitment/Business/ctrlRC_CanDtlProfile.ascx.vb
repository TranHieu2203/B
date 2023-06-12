Imports System.Reflection
Imports Common
Imports Common.Common
Imports Common.CommonBusiness
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_CanDtlProfile
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    'Mã nhân viên
    Property CandidateCode As String
        Get
            Return PageViewState(Me.ID & "_CandidateCode")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CandidateCode") = value
        End Set
    End Property

    'Popup

#Region "Properties"
    Property lstComboData As Recruitment.RecruitmentBusiness.ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_lstComboData")
        End Get
        Set(ByVal value As Recruitment.RecruitmentBusiness.ComboBoxDataDTO)
            PageViewState(Me.ID & "_lstComboData") = value
        End Set
    End Property

    Property ListComboData As Recruitment.RecruitmentBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Recruitment.RecruitmentBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    'Thông tin nhân viên
    Property CandidateInfo As CandidateDTO
        Get
            Return PageViewState(Me.ID & "_CandidateInfo")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_CandidateInfo") = value
        End Set
    End Property

    Property PreviousIndex As Integer
        Get
            Return PageViewState(Me.ID & "_PreviousIndex")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_PreviousIndex") = value
        End Set
    End Property

    Property Reload As String
        Get
            Return PageViewState(Me.ID & "_Reload")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_Reload") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_Reload")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_Reload") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property IDSelect As String
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    'Public Property ImageFile As Telerik.Web.UI.UploadedFile 'File ảnh upload từ ViewUploadImage.
    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return PageViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            PageViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property


    Property EmpCode As CandidateDTO
        Get
            Return ViewState(Me.ID & "_EmpCode")
        End Get
        Set(ByVal value As CandidateDTO)
            ViewState(Me.ID & "_EmpCode") = value
        End Set
    End Property

    Dim IDemp As Decimal

    Dim FormType As Integer

    Property flagTab As Integer
        Get
            Return PageViewState(Me.ID & "_flagTab")
        End Get
        Set(ByVal value As Integer)
            PageViewState(Me.ID & "_flagTab") = value
        End Set
    End Property

#End Region

#Region "Page"
    'Định nghĩa trang
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            GetParams()
            'AccessPanelBar()
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
                Select Case FormType
                    Case 0
                        Dim rep As New RecruitmentRepository
                        'Tạo mới mã nhân viên
                        EmpCode = rep.CreateNewCandidateCode()
                        txtEmpCODE.Text = EmpCode.CANDIDATE_CODE
                        IDemp = EmpCode.ID
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Load trang
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim ComStore As New Common.CommonProcedureNew
            If ComStore.CHECK_EXIST_SE_CONFIG("RC_LITERACY_REQUIRE") = -1 Then
                LiteracyRequireChar.Visible = True
                LiteracyRequireField.Visible = True
            Else
                LiteracyRequireChar.Visible = False
                LiteracyRequireField.Visible = False
            End If

            If ComStore.CHECK_EXIST_SE_CONFIG("RC_ID_NO_REQUIRE") = -1 Then
                IdNoRequireChar.Visible = True
                IdNoRequireField.Visible = True
            Else
                IdNoRequireChar.Visible = False
                IdNoRequireField.Visible = False
            End If

            If ComStore.CHECK_EXIST_SE_CONFIG("RC_PERSONAL_EMAIL_REQUIRE") = -1 Then
                PerMailRequireChar.Visible = True
                PerMailRequireField.Visible = True
            Else
                PerMailRequireChar.Visible = False
                PerMailRequireField.Visible = False
            End If

            If ComStore.CHECK_EXIST_SE_CONFIG("RC_MOBILE_PHONE_REQUIRE") = -1 Then
                MobPhoneRequireChar.Visible = True
                MobPhoneRequireField.Visible = True
            Else
                MobPhoneRequireChar.Visible = False
                MobPhoneRequireField.Visible = False
            End If

            If Not IsPostBack Then
                Refresh()
            End If
            UpdateControlState()
            CurrentPlaceHolder = Me.ViewName
            Dim s = Request.Params("isdone")
            If s = "1" Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Đỗ dữ liệu vào form
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim rep As New RecruitmentRepository
                If hidProgramID.Value IsNot Nothing AndAlso hidProgramID.Value <> "" AndAlso hidProgramID.Value <> "0" Then
                    Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                    txtOrgName.Text = objPro.ORG_NAME
                    hidOrg.Value = objPro.ORG_ID
                    txtTitleName.Text = objPro.TITLE_NAME
                    hidTitle.Value = objPro.TITLE_ID
                End If
            End If

            If Not isLoad Then
                Using rep As New RecruitmentRepository
                    'Trường hợp có thông tin về nhân viên đó thì fill lên các control.
                    If CandidateInfo IsNot Nothing Then
                        'Phần hồ sơ
                        hidID.Value = CandidateInfo.ID.ToString()
                        If IsNumeric(CandidateInfo.ORG_ID) Then
                            hidOrg.Value = CandidateInfo.ORG_ID.ToString()
                        End If
                        hidOrgID.Value = CandidateInfo.ORG_ID.ToString()
                        txtOrgID.Text = CandidateInfo.ORG_NAME
                        If CandidateInfo.ORG_DESC IsNot Nothing AndAlso CandidateInfo.ORG_DESC <> "" Then
                            txtOrgID.ToolTip = DrawTreeByString(CandidateInfo.ORG_DESC)
                        End If
                        txtEmpCODE.Text = CandidateInfo.CANDIDATE_CODE
                        txtFirstNameVN.Text = CandidateInfo.FIRST_NAME_VN
                        txtLastNameVN.Text = CandidateInfo.LAST_NAME_VN
                        'hidOrg.Value = CandidateInfo.ORG_ID
                        txtOrgName.Text = CandidateInfo.ORG_NAME
                        txtCare_TitleName.Text = CandidateInfo.CARE_TITLE_NAME
                        txtCare_Website.Text = CandidateInfo.RECRUIMENT_WEBSITE
                        If CandidateInfo.RC_SOURCE_REC_ID IsNot Nothing Then
                            cboRC_SOURCE_REC_ID.SelectedValue = CandidateInfo.RC_SOURCE_REC_ID
                            cboRC_SOURCE_REC_ID.Text = CandidateInfo.RC_SOURCE_REC_NAME
                        End If
                        'Phần sơ yếu lý lịch
                        Dim empCV = rep.GetCandidateCV(CandidateInfo.ID)
                        If empCV IsNot Nothing Then
                            If empCV.CON_WARD IsNot Nothing Then
                                cboContractWard.SelectedValue = empCV.CON_WARD
                                cboContractWard.Text = empCV.CON_WARD_NAME
                            End If
                            If empCV.PER_WARD IsNot Nothing Then
                                cbPerward.SelectedValue = empCV.PER_WARD
                                cbPerward.Text = empCV.PER_WARD_NAME
                            End If

                            If empCV.GENDER IsNot Nothing Then
                                cboGender.SelectedValue = empCV.GENDER
                            End If

                            cboTinhTrangHN.SelectedValue = empCV.MARITAL_STATUS
                            If empCV.NATIVE IsNot Nothing Then
                                cboNative.SelectedValue = empCV.NATIVE
                            End If
                            If empCV.RELIGION IsNot Nothing Then
                                cboReligion.SelectedValue = empCV.RELIGION
                            End If
                            rntxtCMND.Text = empCV.ID_NO
                            If empCV.ID_DATE IsNot Nothing Then
                                rdCMNDDate.SelectedDate = empCV.ID_DATE
                            End If
                            If empCV.ID_PLACE IsNot Nothing Then
                                cboCMNDPlace.SelectedValue = empCV.ID_PLACE
                            End If

                            If empCV.BIRTH_DATE IsNot Nothing Then
                                rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                            End If
                            If empCV.BIRTH_NATION_ID IsNot Nothing Then
                                cboNation.SelectedValue = empCV.BIRTH_NATION_ID
                            End If
                            If empCV.BIRTH_PROVINCE IsNot Nothing Then
                                cboProvince.SelectedValue = empCV.BIRTH_PROVINCE
                            End If
                            If empCV.NATIONALITY_ID IsNot Nothing Then
                                cboNationality.SelectedValue = empCV.NATIONALITY_ID
                            End If
                            If empCV.NAV_NATION_ID IsNot Nothing Then
                                cboNavNation.SelectedValue = empCV.NAV_NATION_ID
                            End If
                            If empCV.NAV_PROVINCE IsNot Nothing Then
                                cboNav_Province.SelectedValue = empCV.NAV_PROVINCE
                            End If
                            txtPerAddress.Text = empCV.PER_ADDRESS
                            If empCV.PER_DISTRICT_ID IsNot Nothing Then
                                cboPerDictrict.SelectedValue = empCV.PER_DISTRICT_ID
                            End If
                            If empCV.PER_NATION_ID IsNot Nothing Then
                                cboPerNation.SelectedValue = empCV.PER_NATION_ID
                            End If
                            If empCV.PER_PROVINCE IsNot Nothing Then
                                cboPerProvince.SelectedValue = empCV.PER_PROVINCE
                            End If
                            txtContactAdd.Text = empCV.CONTACT_ADDRESS
                            If empCV.CONTACT_NATION_ID IsNot Nothing Then
                                cboContactNation.SelectedValue = empCV.CONTACT_NATION_ID
                            End If
                            If empCV.CONTACT_PROVINCE IsNot Nothing Then
                                cboContactProvince.SelectedValue = empCV.CONTACT_PROVINCE
                            End If
                            If empCV.CONTACT_DISTRICT_ID IsNot Nothing Then
                                cboContractDictrict.SelectedValue = empCV.CONTACT_DISTRICT_ID
                            End If

                            If empCV.ID_DATE_EXPIRATION IsNot Nothing Then
                                rdCMNDEnd.SelectedDate = empCV.ID_DATE_EXPIRATION
                            End If
                            If empCV.IS_RESIDENT IsNot Nothing Then
                                cv_cbxKhongCuTru.Checked = empCV.IS_RESIDENT
                            End If
                            txtContactAddress.Text = empCV.CONTACT_ADDRESS_TEMP
                            If empCV.CONTACT_NATION_TEMP IsNot Nothing Then
                                cboContactNation.SelectedValue = empCV.CONTACT_NATION_TEMP
                            End If
                            If empCV.CONTACT_PROVINCE_TEMP IsNot Nothing Then
                                cboContactProvince.SelectedValue = empCV.CONTACT_PROVINCE_TEMP
                            End If
                            If empCV.CONTACT_DISTRICT_TEMP IsNot Nothing Then
                                cboContractDictrict.SelectedValue = empCV.CONTACT_DISTRICT_TEMP
                            End If
                            txtSoDienThoaiCaNhan.Text = empCV.CONTACT_MOBILE
                            txtSoDienThoaiCoDinh.Text = empCV.CONTACT_PHONE
                            cv_txtEmailCaNhanCongTy.Text = empCV.WORK_EMAIl
                            txtEmailCaNhan.Text = empCV.PER_EMAIL
                            If empCV.PERTAXCODE IsNot Nothing Then
                                txtMST.Text = empCV.PERTAXCODE
                            End If
                            If empCV.PER_TAX_DATE IsNot Nothing Then
                                rdNgayCapMST.SelectedDate = empCV.PER_TAX_DATE
                            End If
                            txtNoiCapMST.Text = empCV.PER_TAX_PLACE
                            'Nguoi than

                            If empCV.PASSPORT_DATE_EXPIRATION IsNot Nothing Then
                                rdPassportEnd.SelectedDate = empCV.PASSPORT_DATE_EXPIRATION
                            End If
                            txtPassport.Text = empCV.PASSPORT_ID
                            If empCV.PASSPORT_DATE IsNot Nothing Then
                                rdPassport.SelectedDate = empCV.PASSPORT_DATE
                            End If
                            txtPassportNoiCap.Text = empCV.PASSPORT_PLACE_NAME

                            If empCV.VISA_NUMBER IsNot Nothing Then
                                txtSoViSa.Text = empCV.VISA_NUMBER
                            End If
                            If empCV.VISA_DATE IsNot Nothing Then
                                rdNgayCapViSa.SelectedDate = empCV.VISA_DATE
                            End If
                            If empCV.VISA_DATE_EXPIRATION IsNot Nothing Then
                                rdNgayHetHanVisa.SelectedDate = empCV.VISA_DATE_EXPIRATION
                            End If
                            txtNoiCapVisa.Text = empCV.VISA_PLACE
                            If empCV.VNAIRLINES_NUMBER IsNot Nothing Then
                                txtVNAirlines.Text = empCV.VNAIRLINES_NUMBER
                            End If

                            If empCV.VNAIRLINES_DATE IsNot Nothing Then
                                rdVNANgayCap.SelectedDate = empCV.VNAIRLINES_DATE
                            End If
                            If empCV.VNAIRLINES_DATE_EXPIRATION IsNot Nothing Then
                                rdVNAHetHan.SelectedDate = empCV.VNAIRLINES_DATE_EXPIRATION
                            End If
                            If empCV.VNAIRLINES_PLACE IsNot Nothing Then
                                txtVNANoiCap.Text = empCV.VNAIRLINES_PLACE
                            End If
                            If empCV.LABOUR_NUMBER IsNot Nothing Then
                                txtSoLaoDong.Text = empCV.LABOUR_NUMBER
                            End If
                            If empCV.LABOUR_DATE IsNot Nothing Then
                                rdLaoDongNgayCap.SelectedDate = empCV.LABOUR_DATE
                            End If
                            If empCV.LABOUR_DATE_EXPIRATION IsNot Nothing Then
                                rdLaoDongHetHan.SelectedDate = empCV.LABOUR_DATE_EXPIRATION
                            End If
                            txtLaoDongNoiCap.Text = empCV.LABOUR_PLACE
                            txtGiayPhepLaoDong.Text = empCV.WORK_PERMIT
                            If empCV.WORK_PERMIT_START IsNot Nothing Then
                                rdGiayPhepLaoDongTyNgay.SelectedDate = empCV.WORK_PERMIT_START
                            End If
                            If empCV.WORK_PERMIT_END IsNot Nothing Then
                                rdGiayPhepLaoDongDenNgay.SelectedDate = empCV.WORK_PERMIT_END
                            End If
                            txtTheTamTru.Text = empCV.TEMP_RESIDENCE_CARD
                            If empCV.TEMP_RESIDENCE_CARD_START IsNot Nothing Then
                                rdTheTamTruTuNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_START
                            End If
                            If empCV.TEMP_RESIDENCE_CARD_END IsNot Nothing Then
                                rdTheTamTruDenNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_END
                            End If
                            If empCV.IS_WORK_PERMIT IsNot Nothing Then
                                chkIsPermit.Checked = empCV.IS_WORK_PERMIT
                            End If
                            txtWorkPermitNo.Text = empCV.WORK_PERMIT

                            If empCV.WORK_PERMIT_START IsNot Nothing Then
                                rdFromDate.SelectedDate = empCV.WORK_PERMIT_START
                            End If
                            If empCV.WORK_PERMIT_END IsNot Nothing Then
                                rdToDate.SelectedDate = empCV.WORK_PERMIT_END
                            End If
                            txtNGT_Fullname.Text = empCV.FINDER_NAME
                            txtNGT_SDT.Text = empCV.FINDER_SDT
                            txtNGT_DiaChi.Text = empCV.FINDER_ADDRESS

                            txtNT_FullName.Text = empCV.URGENT_PER_NAME
                            txtNT_SDT.Text = empCV.URGENT_PER_SDT
                            txtNT_DiaChi.Text = empCV.URGENT_ADDRESS
                            If IsNumeric(empCV.URGENT_PER_RELATION) = True Then
                                rcbNT_Relation.SelectedValue = empCV.URGENT_PER_RELATION
                            End If

                            txtStranger.Text = empCV.STRANGER
                            txtWeakness.Text = empCV.WEAKNESS
                        End If

                        'Phần gia đình
                        'Dim EmpFamily = rep.GetCandidateFamily_ByID(CandidateInfo.ID)
                        'If EmpFamily IsNot Nothing Then
                        '    txtNT_FullName.Text = EmpFamily.FULLNAME
                        '    txtNT_SDT.Text = EmpFamily.PHONE_NUMBER
                        '    txtNT_DiaChi.Text = EmpFamily.ADDRESS
                        '    If IsNumeric(EmpFamily.RELATION_ID) = True Then
                        '        rcbNT_Relation.SelectedValue = EmpFamily.RELATION_ID
                        '    End If
                        'End If

                        'Phần trình độ
                        Dim EmpEducation = rep.GetCandidateEdu(CandidateInfo.ID)
                        If EmpEducation IsNot Nothing Then
                            If EmpEducation.ACADEMY IsNot Nothing Then
                                cboTrinhDoVanHoa.SelectedValue = EmpEducation.ACADEMY
                            End If
                            If EmpEducation.LANGUAGE_ID IsNot Nothing Then
                                cboNgoaNgu1.SelectedValue = EmpEducation.LANGUAGE_ID
                                cboNgoaNgu1.Text = EmpEducation.LANGUAGE_NAME
                            End If
                            If EmpEducation.CERTIFICATE_ID IsNot Nothing Then
                                cboChungchi.SelectedValue = EmpEducation.CERTIFICATE_ID
                                cboChungchi.Text = EmpEducation.CERTIFICATE_NAME
                            End If
                            If EmpEducation.YEAR_GRADUATE IsNot Nothing Then
                                txtYearGra.Text = EmpEducation.YEAR_GRADUATE
                            End If

                            If EmpEducation.LEARNING_LEVEL IsNot Nothing Then
                                cboTrinhDoHocVan.SelectedValue = EmpEducation.LEARNING_LEVEL
                            End If

                            If EmpEducation.FIELD IsNot Nothing Then
                                cboTrinhDoChuyenMon.SelectedValue = EmpEducation.FIELD
                            End If

                            If EmpEducation.SCHOOL IsNot Nothing Then
                                cboTruongHoc.SelectedValue = EmpEducation.SCHOOL
                            End If

                            If EmpEducation.MAJOR IsNot Nothing Then
                                cboChuyenNganh.SelectedValue = EmpEducation.MAJOR
                            End If

                            If EmpEducation.DEGREE IsNot Nothing Then
                                cboBangCap.SelectedValue = EmpEducation.DEGREE
                            End If
                            If EmpEducation.MARK_EDU IsNot Nothing Then
                                cboXepLoai.SelectedValue = EmpEducation.MARK_EDU
                            End If
                            If EmpEducation.GPA IsNot Nothing Then
                                txtDiemTotNghiep.Text = EmpEducation.GPA
                            End If

                            txtDegreeChungChi1.Text = EmpEducation.IT_CERTIFICATE
                            If EmpEducation.IT_LEVEL IsNot Nothing Then
                                cboDegreeTrinhDo1.SelectedValue = EmpEducation.IT_LEVEL
                            End If
                            If EmpEducation.IT_MARK IsNot Nothing Then
                                txtDegreeDiemSoXepLoai1.Text = EmpEducation.IT_MARK
                            End If

                            txtDegreeChungChi2.Text = EmpEducation.IT_CERTIFICATE1
                            If EmpEducation.IT_LEVEL1 IsNot Nothing Then
                                cboDegreeTrinhDo2.SelectedValue = EmpEducation.IT_LEVEL1
                            End If
                            If EmpEducation.IT_MARK1 IsNot Nothing Then
                                txtDegreeDiemSoXepLoai2.Text = EmpEducation.IT_MARK1
                            End If

                            txtDegreeChungChi3.Text = EmpEducation.IT_CERTIFICATE2
                            If EmpEducation.IT_LEVEL2 IsNot Nothing Then
                                cboDegreeTrinhDo3.SelectedValue = EmpEducation.IT_LEVEL2
                            End If
                            If EmpEducation.IT_MARK2 IsNot Nothing Then
                                txtDegreeDiemSoXepLoai3.Text = EmpEducation.IT_MARK2
                            End If

                            txtTDNNNgoaiNgu1.Text = EmpEducation.ENGLISH
                            If EmpEducation.ENGLISH_LEVEL IsNot Nothing Then
                                cboTDNNTrinhDo1.SelectedValue = EmpEducation.ENGLISH_LEVEL
                            End If
                            If EmpEducation.ENGLISH_MARK IsNot Nothing Then
                                txtTDNNDiem1.Text = EmpEducation.ENGLISH_MARK
                            End If

                            txtTDNNNgoaiNgu2.Text = EmpEducation.ENGLISH1
                            If EmpEducation.ENGLISH_LEVEL1 IsNot Nothing Then
                                cboTDNNTrinhDo2.SelectedValue = EmpEducation.ENGLISH_LEVEL1
                            End If
                            If EmpEducation.ENGLISH_MARK1 IsNot Nothing Then
                                txtTDNNDiem2.Text = EmpEducation.ENGLISH_MARK1
                            End If

                            txtTDNNNgoaiNgu3.Text = EmpEducation.ENGLISH2
                            If EmpEducation.ENGLISH_LEVEL2 IsNot Nothing Then
                                cboTDNNTrinhDo3.SelectedValue = EmpEducation.ENGLISH_LEVEL2
                            End If

                            If EmpEducation.ENGLISH_MARK2 IsNot Nothing Then
                                txtTDNNDiem3.Text = EmpEducation.ENGLISH_MARK2
                            End If
                            txtEduDateStart.SelectedDate = EmpEducation.DATE_START
                            txtEduDateEnd.SelectedDate = EmpEducation.DATE_END
                            txtEduSkill.Text = EmpEducation.ENGLISH_SKILL
                            txtRate.Text = EmpEducation.RATE
                        End If

                        'Candidate Other
                        Dim EmpOtherInfo = rep.GetCandidateOtherInfo(CandidateInfo.ID)
                        If EmpOtherInfo IsNot Nothing Then
                            chkCongDoanPhi.Checked = If(EmpOtherInfo.DOAN_PHI Is Nothing, False, EmpOtherInfo.DOAN_PHI)
                            If EmpOtherInfo.NGAY_VAO_DOAN IsNot Nothing Then
                                rdNgayVaoCongDoan.SelectedDate = EmpOtherInfo.NGAY_VAO_DOAN
                            End If
                            txtNoiVaoCongDoan.Text = EmpOtherInfo.NOI_VAO_DOAN
                            txtTKNguoiThuHuong.Text = EmpOtherInfo.ACCOUNT_NAME
                            txtTKTKChuyenKhoan.Text = If(EmpOtherInfo.ACCOUNT_NUMBER Is Nothing, "", EmpOtherInfo.ACCOUNT_NUMBER)
                            cboTKNganHang.SelectedValue = EmpOtherInfo.BANK
                            cboTKChiNhanhNganHang.SelectedValue = EmpOtherInfo.BANK_BRANCH
                            cboTKChiNhanhNganHang.Text = EmpOtherInfo.BANK_BRANCH_Name
                            other_cbxThanhToanQuaNH.Checked = If(EmpOtherInfo.IS_PAYMENT_VIA_BANK Is Nothing, False, EmpOtherInfo.IS_PAYMENT_VIA_BANK)
                            If EmpOtherInfo.ACCOUNT_EFFECT_DATE IsNot Nothing Then
                                rdpTKNgayHieuLuc.SelectedDate = EmpOtherInfo.ACCOUNT_EFFECT_DATE
                            End If
                        End If
                        'Candidate Health
                        Dim EmpHealthInfo = rep.GetCandidateHealthInfo(CandidateInfo.ID)
                        If EmpHealthInfo IsNot Nothing Then
                            txtChieuCao.Text = EmpHealthInfo.CHIEU_CAO
                            txtCanNang.Text = EmpHealthInfo.CAN_NANG
                            txtNhomMau.Text = EmpHealthInfo.NHOM_MAU
                            txtHuyetAp.Text = EmpHealthInfo.HUYET_AP
                            txtMatTrai.Text = EmpHealthInfo.MAT_TRAI
                            txtMatPhai.Text = EmpHealthInfo.MAT_PHAI
                            If IsNumeric(EmpHealthInfo.LOAI_SUC_KHOE) Then
                                cboLoaiSucKhoe.SelectedValue = EmpHealthInfo.LOAI_SUC_KHOE
                            End If
                            txtTaiMuiHong.Text = EmpHealthInfo.TAI_MUI_HONG
                            txtRangHamMat.Text = EmpHealthInfo.RANG_HAM_MAT
                            txtTim.Text = EmpHealthInfo.TIM
                            txtPhoiNguc.Text = EmpHealthInfo.PHOI_NGUC
                            txtVienGanB.Text = EmpHealthInfo.VIEM_GAN_B
                            txtDaHoaLieu.Text = EmpHealthInfo.DA_HOA_LIEU
                            txtGhiChuSK.Text = EmpHealthInfo.GHI_CHU_SUC_KHOE

                        End If
                        'Candidate Nguyện vọng
                        Dim EmpExpectInfo = rep.GetCandidateExpectInfo(CandidateInfo.ID)
                        If EmpExpectInfo IsNot Nothing Then
                            If IsNumeric(EmpExpectInfo.TIME_START) Then
                                cboExpectThoiGianLamViec.SelectedValue = EmpExpectInfo.TIME_START
                            End If
                            If IsNumeric(EmpExpectInfo.PROBATIONARY_SALARY) Then
                                txtExpectMucLuongThuViec.Text = EmpExpectInfo.PROBATIONARY_SALARY
                            End If
                            If IsNumeric(EmpExpectInfo.OFFICIAL_SALARY) Then
                                txtExpectMucLuongChinhThuc.Text = EmpExpectInfo.OFFICIAL_SALARY
                            End If
                            If IsDate(EmpExpectInfo.DATE_START) Then
                                txtExpectNgayBatDau.SelectedDate = EmpExpectInfo.DATE_START
                            End If
                            txtExpectDeNghiKhac.Text = EmpExpectInfo.OTHER_REQUEST
                            txtWORK_LOCATION.Text = EmpExpectInfo.WORK_LOCATION

                            If IsNumeric(EmpExpectInfo.DESIRE_POSITION_1) Then
                                cboDesire_Position_1.Text = EmpExpectInfo.DESIRE_POSITION_1_NAME
                                cboDesire_Position_1.SelectedValue = EmpExpectInfo.DESIRE_POSITION_1
                            End If
                            If IsNumeric(EmpExpectInfo.DESIRE_POSITION_2) Then
                                cboDesire_Position_2.Text = EmpExpectInfo.DESIRE_POSITION_2_NAME
                                cboDesire_Position_2.SelectedValue = EmpExpectInfo.DESIRE_POSITION_2
                            End If
                            If IsNumeric(EmpExpectInfo.WORKED_HSV_HV) Then
                                chkWorked_HSV_HV.Checked = EmpExpectInfo.WORKED_HSV_HV
                            End If
                            txtDesire_Level.Text = EmpExpectInfo.DESIRE_LEVEL
                            txtSENIORITY_EXPERIENCE.Text = EmpExpectInfo.SENIORITY_EXPERIENCE

                        End If
                        'Candidate lich su ung tuyen
                        Dim EmpHistory = rep.GetCandidateHistory(CandidateInfo.ID, 0)
                        If EmpHistory IsNot Nothing Then
                            rgDataHistory.DataSource = EmpHistory
                        End If
                    Else
                        'mac dinh load lên 12/12 nên set cứng ở đây,sau này thay đổi id thi set lại chỗ này,trình độ văn hóa
                        cboTrinhDoVanHoa.SelectedValue = 503
                    End If

                End Using
                isLoad = True
            Else
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Lấy tham số Params URL
    Private Sub GetParams()
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            If CurrentState Is Nothing Then

                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Quyền truy cập PanelBar
    Public Sub AccessPanelBar()
        Try
            Dim i As Integer = 0
            ' lặp item của RadPanelBar
            While (i < rtabRecruitmentInfo.Tabs.Count)
                ' lấy item của đang lặp
                Dim itm As RadTab = rtabRecruitmentInfo.Tabs(i)
                Using rep As New CommonRepository
                    ' lấy thông tin user đang đăng nhập
                    Dim user = LogHelper.CurrentUser
                    ' check xem có phải admin không
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Utilities.GetUsername)
                    ' nếu không phải admin
                    If GroupAdmin = False Then
                        ' lấy quyền của user
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Utilities.GetUsername)
                        ' nếu có quyền
                        If permissions IsNot Nothing Then
                            ' kiểm tra các chức năng ngoại trừ chức năng là báo cáo
                            ' thay vì .value e phải xài .ID đúng không nào ;)
                            Dim isPermissions = (From p In permissions Where p.FID = itm.PageViewID).Any
                            ' nếu không tồn tại --> xóa item
                            If Not isPermissions Then
                                rtabRecruitmentInfo.Tabs(i).Visible = False
                                i = i + 1
                                Continue While
                            Else
                                'Set mặc định tabs đầu tiên được chọn
                                If flagTab = 0 Then
                                    rtabRecruitmentInfo.Tabs.Item(i).Selected = True
                                    RadMultiPage1.SelectedIndex = i
                                    flagTab = 1
                                End If

                            End If
                        End If
                    Else
                        ' nếu là admin + có quyền
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                        Else
                            ' xóa nếu admin không có quyền
                            rtabRecruitmentInfo.Tabs.RemoveAt(i)
                            Continue While
                        End If
                    End If
                End Using
                i = i + 1
            End While
            ' làm tưởng tự tabstrip
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Đổ dữ liệu vào Combobox
    Private Sub GetDataCombo()
        Dim rep As New RecruitmentRepository
        Dim sp As New RecruitmentStoreProcedure
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Recruitment.RecruitmentBusiness.ComboBoxDataDTO
                ListComboData.GET_BANK = True
                ListComboData.GET_BANK_BRACH = True
                ListComboData.GET_DISTRICT = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                ListComboData.GET_RELATION = True
                'ListComboData.GET_PROVINC()
                rep.GetComboList(ListComboData)
            End If
            Dim dtData
            '################### Sơ yếu lý lịch ####################
            ' Giới tính
            dtData = rep.GetOtherList("GENDER", True)
            FillRadCombobox(cboGender, dtData, "NAME", "ID")

            'Mối quan hệ
            FillDropDownList(rcbNT_Relation, ListComboData.LIST_RELATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNation.SelectedValue)

            'Tình trạng hôn nhân
            dtData = rep.GetOtherList("FAMILY_STATUS", True)
            FillRadCombobox(cboTinhTrangHN, dtData, "NAME", "ID")
            ' Tôn giáo
            dtData = rep.GetOtherList("RELIGION", True)
            FillRadCombobox(cboReligion, dtData, "NAME", "ID")
            ' Dân tộc
            dtData = rep.GetOtherList("NATIVE", True)
            FillRadCombobox(cboNative, dtData, "NAME", "ID")


            ' Quốc gia
            FillDropDownList(cboNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNation.SelectedValue)
            FillDropDownList(cboPerNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboPerNation.SelectedValue)
            FillDropDownList(cboContactNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContactNation.SelectedValue)
            FillDropDownList(cboContactAddNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContactAddNation.SelectedValue)
            ' Tỉnh thành
            FillDropDownList(cboContactAddProvince, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContactAddProvince.SelectedValue)
            '' Nơi sinh
            FillDropDownList(cboProvince, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince.SelectedValue)
            '' Địa chỉ liên hệ - thành phố
            FillDropDownList(cboContactProvince, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContactProvince.SelectedValue)
            '' Địa chỉ thường chú - thành phố
            FillDropDownList(cboPerProvince, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboPerProvince.SelectedValue)

            '' Quê quán
            FillDropDownList(cboNav_Province, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNav_Province.SelectedValue)

            ' Quận huyện
            FillDropDownList(cboPerDictrict, ListComboData.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboPerDictrict.SelectedValue)
            FillDropDownList(cboContractDictrict, ListComboData.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContractDictrict.SelectedValue)
            FillDropDownList(cboContractAddDictrict, ListComboData.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContractAddDictrict.SelectedValue)
            'phường xã


            ' Nguyen quán
            FillDropDownList(cboNavNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNavNation.SelectedValue)

            ' Quốc tịch
            FillDropDownList(cboNationality, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationality.SelectedValue)

            '################### Thông tin trình độ ################
            'Trình độ văn hóa
            Dim DT As DataTable
            DT = rep.GetOtherList("ACADEMY", True)
            DT.DefaultView.Sort = "ID ASC"
            dtData = DT.DefaultView.ToTable()
            FillRadCombobox(cboTrinhDoVanHoa, dtData, "NAME", "ID")
            cboTrinhDoVanHoa.ClearSelection()
            'cboTrinhDoVanHoa.SelectedValue = 503
            'Trình độ học vấn
            dtData = rep.GetOtherList("LEARNING_LEVEL", True)
            FillRadCombobox(cboTrinhDoHocVan, dtData, "NAME", "ID")
            'Trình độ chuyên môn ('MAJOR)
            dtData = rep.GetOtherList("MAJOR", True)
            FillRadCombobox(cboTrinhDoChuyenMon, dtData, "NAME", "ID", True)
            'Trường học
            dtData = rep.GetOtherList("HU_GRADUATE_SCHOOL", True)
            FillRadCombobox(cboTruongHoc, dtData, "NAME", "ID")
            'Chuyên ngành
            dtData = rep.GetOtherList("MAJOR", True)
            FillRadCombobox(cboChuyenNganh, dtData, "NAME", "ID")
            'Bằng cấp
            dtData = rep.GetOtherList("DEGREE", True)
            FillRadCombobox(cboBangCap, dtData, "NAME", "ID")
            'Xếp loại
            dtData = rep.GetOtherList("MARK_EDU", True)
            FillRadCombobox(cboXepLoai, dtData, "NAME", "ID")

            'Trình độ tin học
            dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
            FillRadCombobox(cboDegreeTrinhDo1, dtData, "NAME", "ID")
            FillRadCombobox(cboDegreeTrinhDo2, dtData, "NAME", "ID")
            FillRadCombobox(cboDegreeTrinhDo3, dtData, "NAME", "ID")

            'Trình độ ngoại ngữ
            dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
            FillRadCombobox(cboTDNNTrinhDo1, dtData, "NAME", "ID")
            FillRadCombobox(cboTDNNTrinhDo2, dtData, "NAME", "ID")
            FillRadCombobox(cboTDNNTrinhDo3, dtData, "NAME", "ID")
            'ngoai ngu
            Dim dtLanguage As DataTable
            dtLanguage = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
            FillRadCombobox(cboNgoaNgu1, dtLanguage, "NAME", "ID")
            'loai chung chi
            dtData = rep.GetOtherList("MARK_EDU")
            FillRadCombobox(cboChungchi, dtData, "NAME", "ID")
            'Ngân hàng
            FillDropDownList(cboTKNganHang, ListComboData.LIST_BANK, "BANK_NAME", "ID", Common.Common.SystemLanguage, True, cboTKNganHang.SelectedValue)

            'Chi nhánh ngân hàng
            'FillDropDownList(cboTKChiNhanhNganHang, ListComboData.LIST_BANK_BRACH, "BRANCH_NAME", "ID", Common.Common.SystemLanguage, True, cboTKChiNhanhNganHang.SelectedValue)
            ' CMND
            FillDropDownList(cboCMNDPlace, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboCMNDPlace.SelectedValue)
            'dtData = rep.GetOtherList("ID_PLACE", True)
            'FillRadCombobox(cboCMNDPlace, dtData, "NAME", "ID")
            ' Nguyện vọng thời gian làm việc
            dtData = rep.GetOtherList("WORK_TIME", True)
            FillRadCombobox(cboExpectThoiGianLamViec, dtData, "NAME", "ID")
            ' Loại sức khỏe
            dtData = rep.GetOtherList("RC_HEALTH_STATUS", True)
            FillRadCombobox(cboLoaiSucKhoe, dtData, "NAME", "ID")
            'anhvn, Nguồn ứng viên
            dtData = rep.GetOtherList("RC_SOURCE_REC", True)
            FillRadCombobox(cboRC_SOURCE_REC_ID, dtData, "NAME", "ID")

            Dim DTTIT = sp.GET_TITLE(True)
            FillRadCombobox(cboDesire_Position_1, DTTIT, "NAME", "ID")
            FillRadCombobox(cboDesire_Position_2, DTTIT, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Bind Data
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Using rep As New RecruitmentRepository

            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Định nghĩa Control
    Protected Sub InitControl()
        Try
            'Khoi tao ToolBar
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Create,
                         ToolbarItem.Edit,
                         ToolbarItem.Seperator,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Dim ajaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ajaxLoading.InitialDelayTime = 100
            'CType(Me.Page, AjaxPage).AjaxManager.AjaxSettings.AddAjaxSetting(btnChoose, phPopup)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    'Sự kiện Toolbar
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Dim _err As String = ""
            Dim strEmpCode As String = ""
            If CandidateInfo IsNot Nothing Then
                strEmpCode = CandidateInfo.CANDIDATE_CODE
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName

                'Case TOOLBARITEM_CREATE

                '    ResetControlValue()
                '    CandidateInfo = Nothing
                '    CurrentState = CommonMessage.STATE_NEW
                '    'divFileAttach.Visible = True
                '    'divGetFile.Visible = False

                '    ' Tao moi ma nhan vien
                '    EmpCode = rep.CreateNewCandidateCode()
                '    txtEmpCODE.Text = EmpCode.CANDIDATE_CODE
                '    IDemp = EmpCode.ID
                Case TOOLBARITEM_EDIT
                    'divFileAttach.Visible = True
                    If CandidateInfo IsNot Nothing Then
                        If (CandidateInfo.STATUS_ID <> RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID) Then
                            CurrentState = CommonMessage.STATE_EDIT
                        Else
                            ShowMessage(Translate("Ứng viên đã là nhân viên. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                            CurrentState = CommonMessage.STATE_NORMAL
                        End If
                    End If
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rdCMNDDate.SelectedDate IsNot Nothing And rdCMNDEnd.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn nhập Ngày hết hạn CMND/CCCD."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case STATE_NEW
                                If Save(strEmpCode, _err) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    If hidProgramID.Value <> "" AndAlso hidOrg.Value <> "" AndAlso hidTitle.Value <> "" Then
                                        Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID={0}&Can={1}&state=Normal&ORGID={2}&TITLEID={3}&PROGRAM_ID={4}&noscroll=1&isdone=1", hidID.Value, strEmpCode, hidOrg.Value, hidTitle.Value, hidProgramID.Value))
                                    Else
                                        Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID={0}&Can={1}&state=Normal&noscroll=1&isdone=1", hidID.Value, strEmpCode))
                                    End If
                                    Exit Sub
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                    Exit Sub
                                End If

                            Case STATE_EDIT
                                If Save(strEmpCode, _err) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case TOOLBARITEM_CANCEL
                    If CurrentState = CommonMessage.STATE_NEW Then 'Nếu là trạng thái new thì xóa ảnh hiện tại
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
                    End If
                    Refresh()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


                Case TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&state=New&noscroll=1&reload=1&FormType=0"))
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Màn hình MessageBox
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            Dim rep As New RecruitmentRepository
            Dim strError As String = ""
            If e.ActionName = CommonMessage.VIEW_TITLE_NEW And e.ButtonID = MessageBoxButtonType.ButtonNo Then

                ClearControlValue(cboDesire_Position_1)
            End If
            If e.ActionName = CommonMessage.VIEW_TITLE_EDIT And e.ButtonID = MessageBoxButtonType.ButtonNo Then

                ClearControlValue(cboDesire_Position_2)
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                Dim lstEmpID = New List(Of Decimal)

                lstEmpID.Add(CandidateInfo.ID)
                rep.DeleteCandidate(lstEmpID, strError)
                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NEW
                    ResetControlValue()
                Else
                    ShowMessage(Translate("Nhân viên này đã có hợp đồng. Hãy xóa hợp đồng trước khi xóa nhân viên."), Utilities.NotifyType.Warning)
                    CurrentState = CommonMessage.STATE_NORMAL
                End If

                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    '##################################### Hồ sơ công tác ######################################

    'Tìm kiếm nhân viên
    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Dim rep As New RecruitmentRepository
        If CurrentState = CommonMessage.STATE_NEW Then
            ' Tao moi ma nhan vien
            EmpCode = rep.CreateNewCandidateCode()
            txtEmpCODE.Text = EmpCode.CANDIDATE_CODE
            IDemp = EmpCode.ID
        End If
        If CurrentState = CommonMessage.STATE_NORMAL Then
            Dim strEmpCode = txtEmpCODE.Text.Trim
            If rep.CheckExistCandidate(strEmpCode) Then
                Page.Response.Redirect("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=" & HttpContext.Current.Server.UrlEncode(strEmpCode) & "&state=Normal&Place=" & HttpContext.Current.Server.UrlEncode(CurrentPlaceHolder) & "&noscroll=1&reload=1")
            Else
                ShowMessage(Translate("Ứng viên không tồn tại."), Utilities.NotifyType.Error)
            End If
        End If
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
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgID.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'duy fix ngay 11/07
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgID.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            'LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub val_Same_Date_FullName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles val_Same_Date_FullName.ServerValidate
        Try
            If CurrentState = STATE_NEW Then
                Using rep As New RecruitmentRepository
                    args.IsValid = rep.ValidateInsertCandidate(txtEmpCODE.Text,
                                                               rntxtCMND.Text,
                                                               txtFirstNameVN.Text & " " & txtLastNameVN.Text,
                                                               rdBirthDate.SelectedDate,
                                                               "DATE_FULLNAME")
                End Using
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cusNO_ID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusNO_ID.ServerValidate
        Try
            If CurrentState = STATE_NEW Then
                Using rep As New RecruitmentRepository
                    args.IsValid = rep.ValidateInsertCandidate(txtEmpCODE.Text,
                                                               rntxtCMND.Text,
                                                               txtFirstNameVN.Text & " " & txtLastNameVN.Text,
                                                               rdBirthDate.SelectedDate, "NO_ID")
                End Using
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusBlackList_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusBlackList.ServerValidate
        Try
            If txtFirstNameVN.Text.Length <> 0 And txtLastNameVN.Text.Length <> 0 And rdBirthDate.SelectedDate IsNot Nothing Then
                Using rep As New RecruitmentRepository
                    args.IsValid = rep.ValidateInsertCandidate(txtEmpCODE.Text, rntxtCMND.Text, txtFirstNameVN.Text & " " & txtLastNameVN.Text, rdBirthDate.SelectedDate, "BLACK_LIST")
                End Using
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub chkIsPermit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsPermit.CheckedChanged
        Try
            If chkIsPermit.Checked Then
                EnableControlAll(True, txtWorkPermitNo, rdFromDate, rdToDate)
                permitCtrl.Visible = True
            Else
                EnableControlAll(False, txtWorkPermitNo, rdFromDate, rdToDate)
                permitCtrl.Visible = False
                ClearControlValue(txtWorkPermitNo, rdFromDate, rdToDate)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    'Cập nhật trạng thái Control và tắt bật Popup
    Public Overrides Sub UpdateControlState()
        If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
            phFindOrg.Controls.Remove(ctrlFindOrgPopup)
            'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
        End If
        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                DisableControls(DetailPane, True)
                DisableControls(RadPane1, True)
                btnSearchEmp.Enabled = True
                txtFirstNameVN.ReadOnly = False
                txtLastNameVN.ReadOnly = False
                txtEmpCODE.ReadOnly = True
                EnableControlAll(False, txtWorkPermitNo, rdFromDate, rdToDate)
                permitCtrl.Visible = False
                If chkIsPermit.Checked Then
                    EnableControlAll(True, txtWorkPermitNo, rdFromDate, rdToDate)
                    permitCtrl.Visible = True
                End If
            Case CommonMessage.STATE_EDIT
                DisableControls(DetailPane, True)
                DisableControls(RadPane1, True)
                btnSearchEmp.Enabled = False
                txtFirstNameVN.ReadOnly = False
                txtLastNameVN.ReadOnly = False
                txtEmpCODE.ReadOnly = True
                EnableControlAll(False, txtWorkPermitNo, rdFromDate, rdToDate)
                permitCtrl.Visible = False
                If chkIsPermit.Checked Then
                    EnableControlAll(True, txtWorkPermitNo, rdFromDate, rdToDate)
                    permitCtrl.Visible = True
                End If
            Case CommonMessage.STATE_NORMAL
                'divFileAttach.Visible = False
                DisableControls(DetailPane, False)
                DisableControls(RadPane1, False)
                btnSearchEmp.Enabled = True
                txtFirstNameVN.ReadOnly = True
                txtLastNameVN.ReadOnly = True
                txtEmpCODE.ReadOnly = False
                EnableControlAll(False, txtWorkPermitNo, rdFromDate, rdToDate)
                permitCtrl.Visible = False
                If chkIsPermit.Checked Then
                    EnableControlAll(True, txtWorkPermitNo, rdFromDate, rdToDate)
                    permitCtrl.Visible = True
                End If

        End Select
        Select Case isLoadPopup

            Case 2
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
        End Select
        ChangeToolbarState()
        Me.Send(CurrentState)
    End Sub


    'Reset mặc định Control
    Public Sub ResetControlValue()
        ' Candidate
        txtEmpCODE.Text = ""
        txtFirstNameVN.Text = ""
        txtLastNameVN.Text = ""
        hidID.Value = ""
        cboGender.SelectedValue = 565
        cboNative.SelectedValue = 1724
        cboReligion.ClearSelection()

        rntxtCMND.Text = Nothing
        rdCMNDDate.SelectedDate = Nothing
        cboCMNDPlace.ClearSelection()


        rdBirthDate.SelectedDate = Nothing
        cboNation.ClearSelection()
        cboProvince.ClearSelection()

        cboNationality.SelectedValue = 1558
        cboNavNation.SelectedValue = 1558
        cboNav_Province.ClearSelection()

        txtPerAddress.Text = ""
        cboPerProvince.ClearSelection()
        cboPerDictrict.ClearSelection()
        cboPerNation.ClearSelection()
        txtContactAddress.Text = ""
        cboContactProvince.SelectedIndex = 0
        cboContractDictrict.SelectedIndex = 0
        cboContactNation.SelectedValue = 1
        'cboEducationLevel.ClearSelection()
        'txtPerTaxCode.Text = ""
        'txtMajor.Text = ""
        'lblFileName.Text = ""

        cboPerNation.SelectedValue = 1
        cboPerProvince.SelectedIndex = 0
        cboPerDictrict.SelectedIndex = 0

        ' Candidate other
        txtSoDienThoaiCoDinh.Text = ""
        'cboNhaMang.ClearSelection()
        txtSoDienThoaiCaNhan.Text = ""
        'txtDienThoaiNguoiLH.Text = ""
        cv_txtEmailCaNhanCongTy.Text = ""
        txtEmailCaNhan.Text = ""
        txtSoViSa.Text = ""
        rdNgayCapViSa.SelectedDate = Nothing
        rdNgayHetHanVisa.SelectedDate = Nothing
        txtNoiCapVisa.Text = ""
        'rdNgayVaoDoan.SelectedDate = Nothing
        'txtNoiVaoDoan.Text = ""
        'rdNgayVaoDang.SelectedDate = Nothing
        'txtNoiVaoDang.Text = ""
        'rdNgayVaoCongDoan.SelectedDate = Nothing
        'txtNoiVaoCongDoan.Text = ""
        'cboThanhPhanGD.ClearSelection()
        'cboThanhPhanBT.ClearSelection()
        'cboThanhPhanCS.ClearSelection()
        'txtSoSoLaoDong.Text = ""
        'rntxSoCon.Value = 0
        'chkDangPhi.Checked = False
        'chkDoanphi.Checked = False
        'chkCongDoanPhi.Checked = False
        cboTinhTrangHN.ClearSelection()
        'txtLoaiXe.Text = ""
        'txtBangLaiXe.Text = ""
        'rdNgayCapBangLai.SelectedDate = Nothing
        'rdNgayHetHanBangLai.SelectedDate = Nothing
        'txtGiayPhepHanhNghe.Text = ""
        'rdTuNgayGPHN.SelectedDate = Nothing
        'rdDenNgayGPHN.SelectedDate = Nothing
        'txtSoTienKyQuy.Value = 0
        'rdNgayKyQuy.SelectedDate = Nothing
        'cboRecruitment.ClearSelection()
        ' Candidate salary
        'rdNgaybatdauhuongluong.SelectedDate = Nothing
        'txtHTTraLuong.Text = ""
        'txtBieuThueTNCN.Text = ""
        'txtORGSL.Text = ""

        'txtLuongCBNN.Value = 0

        'txtLuongchucdanh.Value = 0
        'txtLuongcoban.Value = 0

        'txtLuongtrachnhiem.Value = 0
        'txtTongphucap.Value = 0
        'txtTongkiemnhiem.Value = 0
        'txtTongLuong.Value = 0
        'txtLuongung.Value = 0
        'txtTiencom.Value = 0
        'txtQuycongdong.Value = 0

        'txtLuongcung.Value = 0
        'txtHuonglc.Value = 0
        'txtLuongmem.Value = 0
        'txtHuonglm.Value = 0
        'rdThoingaytraluong.SelectedDate = Nothing
        'rdNgaynangluong.SelectedDate = Nothing
        'txtGhichu.Text = ""


    End Sub

    'Kiểm tra xem trong hệ thống có nhân viên này ko (trừ nhân viên nghỉ việc)?
    Private Sub CheckExistCandidate(ByVal strEmpCode As String)
        Dim rep As New RecruitmentRepository
        CandidateInfo = rep.GetCandidateInfo(strEmpCode) 'Lưu vào viewStates để truyền vào các view con.
    End Sub

    'Lưu
    Private Function Save(ByRef strEmpCode As String, Optional ByRef _err As String = "") As Boolean
        Dim result As Boolean
        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Dim _binaryImage As Byte()
        Dim EmpCV As CandidateCVDTO
        Dim EmpOtherInfo As New CandidateOtherInfoDTO
        Dim EmpEducation As New CandidateEduDTO
        Dim EmpHealthInfo As New CandidateHealthDTO
        Dim EmpExpectInfo As New CandidateExpectDTO
        Dim EmpFamily As New CandidateFamilyDTO
        Dim Rcprogramcandidate As New RC_PROGRAM_CANDIDATEDTO
        Try
            'Candidate
            If CandidateInfo Is Nothing Then
                CandidateInfo = New CandidateDTO
            End If
            If hidID.Value.Trim = "" Then
                CandidateInfo.ID = 0
            Else
                CandidateInfo.ID = Decimal.Parse(hidID.Value)
            End If
            CandidateInfo.ID = IDemp
            'If hidProgramID.Value IsNot Nothing AndAlso hidProgramID.Value.Trim <> "" Then
            '    CandidateInfo.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            'End If
            CandidateInfo.CANDIDATE_CODE = txtEmpCODE.Text.Trim()
            'txtFirstNameVN.Text = StrConv(txtFirstNameVN.Text, VbStrConv.ProperCase)
            CandidateInfo.FIRST_NAME_VN = StrConv(txtFirstNameVN.Text, VbStrConv.ProperCase)
            CandidateInfo.LAST_NAME_VN = StrConv(txtLastNameVN.Text, VbStrConv.ProperCase)
            'If hidOrg.Value IsNot Nothing AndAlso hidOrg.Value.Trim <> "" Then
            '    CandidateInfo.ORG_ID = Decimal.Parse(hidOrg.Value)
            'Else
            '    CandidateInfo.ORG_ID = 0
            'End If
            If hidOrgID.Value IsNot Nothing AndAlso hidOrgID.Value.Trim <> "" Then
                CandidateInfo.ORG_ID = Decimal.Parse(hidOrgID.Value)
            Else
                CandidateInfo.ORG_ID = 0
            End If

            'If hidTitle.Value IsNot Nothing AndAlso hidTitle.Value.Trim <> "" Then
            '    CandidateInfo.TITLE_ID = Decimal.Parse(hidTitle.Value)
            'End If
            CandidateInfo.CARE_TITLE_NAME = txtCare_TitleName.Text.Trim()
            CandidateInfo.RECRUIMENT_WEBSITE = txtCare_Website.Text.Trim()
            If cboRC_SOURCE_REC_ID.SelectedValue <> "" Then
                CandidateInfo.RC_SOURCE_REC_ID = cboRC_SOURCE_REC_ID.SelectedValue
            End If
            'Candidate CV
            EmpCV = New CandidateCVDTO
            If cbPerward.SelectedValue <> "" Then
                EmpCV.PER_WARD = cbPerward.SelectedValue
            End If
            If cboContractWard.SelectedValue <> "" Then
                EmpCV.CON_WARD = cboContractWard.SelectedValue
            End If
            EmpCV.IS_WORK_PERMIT = chkIsPermit.Checked
            EmpCV.WORK_PERMIT = txtWorkPermitNo.Text
            EmpCV.WORK_PERMIT_END = rdFromDate.SelectedDate
            EmpCV.WORK_PERMIT_START = rdToDate.SelectedDate
            EmpCV.GENDER = cboGender.SelectedValue
            EmpCV.NATIVE = cboNative.SelectedValue
            EmpCV.MARITAL_STATUS = cboTinhTrangHN.SelectedValue
            EmpCV.RELIGION = cboReligion.SelectedValue
            If rntxtCMND.Text <> "" Then
                EmpCV.ID_NO = rntxtCMND.Text
            End If

            EmpCV.ID_DATE = rdCMNDDate.SelectedDate

            If cboCMNDPlace.SelectedValue <> "" Then
                EmpCV.ID_PLACE = Decimal.Parse(cboCMNDPlace.SelectedValue)
            End If

            EmpCV.PASSPORT_ID = txtPassport.Text
            EmpCV.PASSPORT_DATE = rdPassport.SelectedDate
            EmpCV.PASSPORT_PLACE_NAME = txtPassportNoiCap.Text
            EmpCV.BIRTH_DATE = rdBirthDate.SelectedDate

            If cboNation.SelectedValue <> "" Then
                EmpCV.BIRTH_NATION_ID = Decimal.Parse(cboNation.SelectedValue)
            End If
            If cboProvince.SelectedValue <> "" Then
                EmpCV.BIRTH_PROVINCE = Decimal.Parse(cboProvince.SelectedValue)
            End If
            If cboNationality.SelectedValue <> "" Then
                EmpCV.NATIONALITY_ID = Decimal.Parse(cboNationality.SelectedValue)
            End If
            If cboNavNation.SelectedValue <> "" Then
                EmpCV.NAV_NATION_ID = Decimal.Parse(cboNavNation.SelectedValue)
            End If
            If cboNav_Province.SelectedValue <> "" Then
                EmpCV.NAV_PROVINCE = Decimal.Parse(cboNav_Province.SelectedValue)
            End If
            EmpCV.PER_ADDRESS = txtPerAddress.Text
            If cboPerDictrict.SelectedValue <> "" Then
                EmpCV.PER_DISTRICT_ID = Decimal.Parse(cboPerDictrict.SelectedValue)
            End If
            If cboPerNation.SelectedValue <> "" Then
                EmpCV.PER_NATION_ID = Decimal.Parse(cboPerNation.SelectedValue)
            End If

            If cboPerProvince.SelectedValue <> "" Then
                EmpCV.PER_PROVINCE = Decimal.Parse(cboPerProvince.SelectedValue)
            End If
            EmpCV.CONTACT_ADDRESS = txtContactAdd.Text
            If cboContactNation.SelectedValue <> "" Then
                EmpCV.CONTACT_NATION_ID = Decimal.Parse(cboContactNation.SelectedValue)
            End If
            If cboContactProvince.SelectedValue <> "" Then
                EmpCV.CONTACT_PROVINCE = Decimal.Parse(cboContactProvince.SelectedValue)
            End If
            If cboContractDictrict.SelectedValue <> "" Then
                EmpCV.CONTACT_DISTRICT_ID = Decimal.Parse(cboContractDictrict.SelectedValue)
            End If

            'Lấy ảnh của nhân viên
            If ImageFile IsNot Nothing Then
                Dim bytes(ImageFile.ContentLength - 1) As Byte
                ImageFile.InputStream.Read(bytes, 0, ImageFile.ContentLength)
                _binaryImage = bytes
                EmpCV.IMAGE = ImageFile.GetExtension 'Lưu lại đuôi ảnh để hiển thị trong view ImageUpload
            Else
                EmpCV.IMAGE = ""
            End If

            EmpCV.ID_DATE_EXPIRATION = rdCMNDEnd.SelectedDate

            EmpCV.IS_RESIDENT = 0
            If cv_cbxKhongCuTru.Checked Then
                EmpCV.IS_RESIDENT = 1
            End If

            EmpCV.CONTACT_ADDRESS_TEMP = txtContactAddress.Text
            If cboContactNation.SelectedValue IsNot Nothing And cboContactNation.SelectedValue <> "" Then
                EmpCV.CONTACT_NATION_TEMP = Decimal.Parse(cboContactNation.SelectedValue)
            End If
            If cboContactProvince.SelectedValue IsNot Nothing And cboContactProvince.SelectedValue <> "" Then
                EmpCV.CONTACT_PROVINCE_TEMP = Decimal.Parse(cboContactProvince.SelectedValue)
            End If
            If cboContractDictrict.SelectedValue IsNot Nothing And cboContractDictrict.SelectedValue <> "" Then
                EmpCV.CONTACT_DISTRICT_TEMP = Decimal.Parse(cboContractDictrict.SelectedValue)
            End If

            EmpCV.CONTACT_MOBILE = txtSoDienThoaiCaNhan.Text
            EmpCV.CONTACT_PHONE = txtSoDienThoaiCoDinh.Text
            EmpCV.PER_EMAIL = txtEmailCaNhan.Text
            EmpCV.WORK_EMAIl = cv_txtEmailCaNhanCongTy.Text
            EmpCV.PERTAXCODE = txtMST.Text
            EmpCV.PER_TAX_DATE = rdNgayCapMST.SelectedDate
            EmpCV.PER_TAX_PLACE = txtNoiCapMST.Text
            EmpCV.PASSPORT_DATE_EXPIRATION = rdPassportEnd.SelectedDate
            EmpCV.VISA_NUMBER = txtSoViSa.Text
            EmpCV.VISA_DATE = rdNgayCapViSa.SelectedDate
            EmpCV.VISA_DATE_EXPIRATION = rdNgayHetHanVisa.SelectedDate
            EmpCV.VISA_PLACE = txtNoiCapVisa.Text
            EmpCV.VNAIRLINES_NUMBER = txtVNAirlines.Text
            EmpCV.VNAIRLINES_DATE = rdVNANgayCap.SelectedDate
            EmpCV.VNAIRLINES_DATE_EXPIRATION = rdVNAHetHan.SelectedDate
            EmpCV.VNAIRLINES_PLACE = txtVNANoiCap.Text
            EmpCV.LABOUR_NUMBER = txtSoLaoDong.Text
            EmpCV.LABOUR_DATE = rdLaoDongNgayCap.SelectedDate
            EmpCV.LABOUR_DATE_EXPIRATION = rdLaoDongHetHan.SelectedDate
            EmpCV.LABOUR_PLACE = txtLaoDongNoiCap.Text
            EmpCV.TEMP_RESIDENCE_CARD = txtTheTamTru.Text
            EmpCV.TEMP_RESIDENCE_CARD_START = rdTheTamTruTuNgay.SelectedDate
            EmpCV.TEMP_RESIDENCE_CARD_END = rdTheTamTruDenNgay.SelectedDate
            EmpCV.FINDER_NAME = txtNGT_Fullname.Text.Trim()
            EmpCV.FINDER_SDT = txtNGT_SDT.Text.Trim()
            EmpCV.FINDER_ADDRESS = txtNGT_DiaChi.Text.Trim()

            EmpCV.URGENT_PER_NAME = txtNT_FullName.Text.Trim
            EmpCV.URGENT_PER_SDT = txtNT_SDT.Text.Trim
            EmpCV.URGENT_ADDRESS = txtNT_DiaChi.Text.Trim
            If IsNumeric(rcbNT_Relation.SelectedValue) = True Then
                EmpCV.URGENT_PER_RELATION = rcbNT_Relation.SelectedValue
            End If

            EmpCV.STRANGER = txtStranger.Text.Trim
            EmpCV.WEAKNESS = txtWeakness.Text.Trim

            'EmpEducation
            If cboNgoaNgu1.SelectedValue <> "" Then
                EmpEducation.LANGUAGE_ID = cboNgoaNgu1.SelectedValue
            End If
            If cboChungchi.SelectedValue <> "" Then
                EmpEducation.CERTIFICATE_ID = cboChungchi.SelectedValue
            End If

            If txtYearGra.Text <> "" Then
                EmpEducation.YEAR_GRADUATE = txtYearGra.Value
            End If

            EmpEducation.MARK_EDU = cboXepLoai.SelectedValue
            EmpEducation.ACADEMY = cboTrinhDoVanHoa.SelectedValue
            EmpEducation.LEARNING_LEVEL = cboTrinhDoHocVan.SelectedValue
            EmpEducation.FIELD = cboTrinhDoChuyenMon.SelectedValue
            EmpEducation.SCHOOL = cboTruongHoc.SelectedValue
            EmpEducation.MAJOR = cboChuyenNganh.SelectedValue
            EmpEducation.DEGREE = cboBangCap.SelectedValue
            EmpEducation.DATE_START = txtEduDateStart.SelectedDate
            EmpEducation.DATE_END = txtEduDateEnd.SelectedDate
            EmpEducation.ENGLISH_SKILL = txtEduSkill.Text

            If txtDiemTotNghiep.Text.Trim() <> "" Then
                EmpEducation.GPA = txtDiemTotNghiep.Text
            End If

            EmpEducation.IT_CERTIFICATE = txtDegreeChungChi1.Text
            EmpEducation.IT_LEVEL = cboDegreeTrinhDo1.SelectedValue
            EmpEducation.IT_MARK = txtDegreeDiemSoXepLoai1.Text

            EmpEducation.IT_CERTIFICATE1 = txtDegreeChungChi2.Text
            EmpEducation.IT_LEVEL1 = cboDegreeTrinhDo2.SelectedValue
            EmpEducation.IT_MARK1 = txtDegreeDiemSoXepLoai2.Text

            EmpEducation.IT_CERTIFICATE2 = txtDegreeChungChi3.Text
            EmpEducation.IT_LEVEL2 = cboDegreeTrinhDo3.SelectedValue
            EmpEducation.IT_MARK2 = txtDegreeDiemSoXepLoai3.Text

            EmpEducation.ENGLISH = txtTDNNNgoaiNgu1.Text
            EmpEducation.ENGLISH_LEVEL = cboTDNNTrinhDo1.SelectedValue
            EmpEducation.ENGLISH_MARK = txtTDNNDiem1.Text

            EmpEducation.ENGLISH1 = txtTDNNNgoaiNgu2.Text
            EmpEducation.ENGLISH_LEVEL1 = cboTDNNTrinhDo2.SelectedValue
            EmpEducation.ENGLISH_MARK1 = txtTDNNDiem2.Text

            EmpEducation.ENGLISH2 = txtTDNNNgoaiNgu3.Text
            EmpEducation.ENGLISH_LEVEL2 = cboTDNNTrinhDo3.SelectedValue
            EmpEducation.ENGLISH_MARK2 = txtTDNNDiem3.Text

            EmpEducation.RATE = txtRate.Text

            'Thông tin tổ chức chính trị ...
            'Đoàn
            EmpOtherInfo.IS_DOANVIEN = chkDoanVien.Checked
            EmpOtherInfo.DOAN_PHI = chkDoanPhi.Checked
            EmpOtherInfo.NGAY_VAO_DOAN = rdNgayVaoDoan.SelectedDate
            If cboChucVuDoan.SelectedValue <> "" Then
                EmpOtherInfo.CHUC_VU_DOAN = cboChucVuDoan.SelectedValue
            End If
            EmpOtherInfo.NOI_VAO_DOAN = txtNoiVaoDoan.Text.Trim()
            'Đảng
            EmpOtherInfo.IS_DANGVIEN = chkDangVien.Checked
            EmpOtherInfo.DANG_PHI = chkDangPhi.Checked
            EmpOtherInfo.NGAY_VAO_DANG = rdNgayVaoDang.SelectedDate
            If cboChucVuDang.SelectedValue <> "" Then
                EmpOtherInfo.CHUC_VU_DANG = cboChucVuDang.SelectedValue
            End If
            EmpOtherInfo.NOI_VAO_DANG = txtNoiVaoDang.Text.Trim()
            If cboDangKiemNhiem.SelectedValue <> "" Then
                EmpOtherInfo.DANG_KIEMNHIEM = cboDangKiemNhiem.SelectedValue
            End If
            EmpOtherInfo.CAPUY_HIENTAI = txtCapUyHienTai.Text.Trim()
            EmpOtherInfo.CAPUY_KIEMNHIEM = txtCapUyKiemNhiem.Text.Trim()
            'Công đoàn
            EmpOtherInfo.IS_CONGDOANPHI = chkCongDoanPhi.Checked
            EmpOtherInfo.CDP_NGAYVAO = rdNgayVaoCongDoan.SelectedDate
            EmpOtherInfo.CDP_NOIVAO = txtNoiVaoCongDoan.Text.Trim()
            'Cựu chiến binh
            EmpOtherInfo.IS_CCB = chkCuuChienBinh.Checked
            EmpOtherInfo.CCB_NOIVAO = txtNoiVaoHoiCuuChienBinh.Text.Trim()
            EmpOtherInfo.CCB_QUANHAM = txtQuanHamChucVuCaoNhat.Text.Trim()
            EmpOtherInfo.CCB_NGAYVAO = rdNgayVaoHoiCuuChienBinh.SelectedDate
            EmpOtherInfo.CCB_NGAYNHAPNGU = rdNgayNhapNgu.SelectedDate
            EmpOtherInfo.CCB_NGAYXUATNGU = rdNgayXuatNgu.SelectedDate
            EmpOtherInfo.CAREER = txtCareer.Text.Trim()
            EmpOtherInfo.TPGD = txtTPGiaDinh.Text.Trim()
            EmpOtherInfo.DANHHIEU = txtDanhHieuDuocPhong.Text.Trim()
            EmpOtherInfo.SOTRUONGCONGTAC = txtSoTruongCongTac.Text.Trim()
            EmpOtherInfo.CONGTAC_LAUNHAT = txtCongTacLauNhat.Text.Trim()
            If cboChucVuCuuChienBinh.SelectedValue <> "" Then
                EmpOtherInfo.CCB_CHUCVU = cboChucVuCuuChienBinh.SelectedValue
            End If
            If cboLyLuanChinhTri.SelectedValue <> "" Then
                EmpOtherInfo.LYLUANCHINHTRI = cboLyLuanChinhTri.SelectedValue
            End If
            If cboQuanLyNhaNuoc.SelectedValue <> "" Then
                EmpOtherInfo.QUANLYNHANUOC = cboQuanLyNhaNuoc.SelectedValue
            End If
            If cboThuongBinh.SelectedValue <> "" Then
                EmpOtherInfo.THUONGBINH = cboThuongBinh.SelectedValue
            End If
            If cboGDChinhSach.SelectedValue <> "" Then
                EmpOtherInfo.GDCS = cboGDChinhSach.SelectedValue
            End If
            EmpOtherInfo = New CandidateOtherInfoDTO
            ' Thông tin tài khoản ngân hàng
            EmpOtherInfo.ACCOUNT_NAME = txtTKNguoiThuHuong.Text
            If txtTKTKChuyenKhoan.Text.Trim() <> "" Then
                EmpOtherInfo.ACCOUNT_NUMBER = txtTKTKChuyenKhoan.Text
            End If

            EmpOtherInfo.BANK = cboTKNganHang.SelectedValue
            EmpOtherInfo.BANK_BRANCH = cboTKChiNhanhNganHang.SelectedValue
            EmpOtherInfo.IS_PAYMENT_VIA_BANK = 0
            If other_cbxThanhToanQuaNH.Checked Then
                EmpOtherInfo.IS_PAYMENT_VIA_BANK = 1
            End If
            EmpOtherInfo.ACCOUNT_EFFECT_DATE = rdpTKNgayHieuLuc.SelectedDate
            ' Thông tin sức khỏe
            EmpHealthInfo = New CandidateHealthDTO
            EmpHealthInfo.CHIEU_CAO = txtChieuCao.Text
            EmpHealthInfo.CAN_NANG = txtCanNang.Text
            EmpHealthInfo.NHOM_MAU = txtNhomMau.Text
            EmpHealthInfo.HUYET_AP = txtHuyetAp.Text
            EmpHealthInfo.MAT_TRAI = txtMatTrai.Text
            EmpHealthInfo.MAT_PHAI = txtMatPhai.Text
            EmpHealthInfo.LOAI_SUC_KHOE = cboLoaiSucKhoe.SelectedValue
            EmpHealthInfo.TAI_MUI_HONG = txtTaiMuiHong.Text
            EmpHealthInfo.RANG_HAM_MAT = txtRangHamMat.Text
            EmpHealthInfo.TIM = txtTim.Text
            EmpHealthInfo.PHOI_NGUC = txtPhoiNguc.Text
            EmpHealthInfo.VIEM_GAN_B = txtVienGanB.Text
            EmpHealthInfo.DA_HOA_LIEU = txtDaHoaLieu.Text
            EmpHealthInfo.GHI_CHU_SUC_KHOE = txtGhiChuSK.Text
            ' Nguyện vọng
            EmpExpectInfo = New CandidateExpectDTO
            EmpExpectInfo.TIME_START = cboExpectThoiGianLamViec.SelectedValue
            EmpExpectInfo.PROBATIONARY_SALARY = If(txtExpectMucLuongThuViec.Text = String.Empty, 0, Decimal.Parse(txtExpectMucLuongThuViec.Text.Trim))
            EmpExpectInfo.OFFICIAL_SALARY = If(txtExpectMucLuongChinhThuc.Text = String.Empty, 0, Decimal.Parse(txtExpectMucLuongChinhThuc.Text.Trim))
            EmpExpectInfo.DATE_START = txtExpectNgayBatDau.SelectedDate
            EmpExpectInfo.OTHER_REQUEST = txtExpectDeNghiKhac.Text
            EmpExpectInfo.WORK_LOCATION = txtWORK_LOCATION.Text
            If cboDesire_Position_1.SelectedValue <> "" Then
                EmpExpectInfo.DESIRE_POSITION_1 = cboDesire_Position_1.SelectedValue
            End If
            If cboDesire_Position_2.SelectedValue <> "" Then
                EmpExpectInfo.DESIRE_POSITION_2 = cboDesire_Position_2.SelectedValue
            End If
            EmpExpectInfo.DESIRE_LEVEL = txtDesire_Level.Text
            EmpExpectInfo.SENIORITY_EXPERIENCE = txtSENIORITY_EXPERIENCE.Text
            EmpExpectInfo.WORKED_HSV_HV = chkWorked_HSV_HV.Checked
            If CandidateInfo IsNot Nothing Then
                If hidID.Value <> "" Then
                    CandidateInfo.ID = Decimal.Parse(hidID.Value)
                    result = rep.ModifyCandidate(CandidateInfo, gID, _binaryImage, EmpCV, EmpEducation, EmpOtherInfo, EmpHealthInfo, EmpExpectInfo, EmpFamily)

                Else
                    result = rep.InsertCandidate(CandidateInfo, gID, strEmpCode, _binaryImage, EmpCV, EmpEducation, EmpOtherInfo, EmpHealthInfo, EmpExpectInfo, EmpFamily)
                    'anhvn, them vao bang Rc_Program_Candidate 
                    If hidProgramID.Value IsNot Nothing AndAlso hidProgramID.Value.Trim <> "" Then
                        Rcprogramcandidate.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
                        Rcprogramcandidate.CANDIDATE_ID = gID
                        Rcprogramcandidate.STATUS_ID = "DUDK"
                        Dim reID As Decimal
                        Dim flag = rep.INSERT_RC_PROGRAM_CANDIDATE(Rcprogramcandidate, reID)
                    End If
                End If
                Refresh()
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Private Sub cboPerNation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPerNation.SelectedIndexChanged
        Try
            If cboPerNation.SelectedValue <> "" Then
                GetProvinceByNationID(cboPerProvince, cboPerDictrict, cboPerNation.SelectedValue, cboPerProvince.SelectedValue)
                If cboContactNation.SelectedValue = "" Then
                    cboContactNation.SelectedValue = cboPerNation.SelectedValue
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboPerProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPerProvince.SelectedIndexChanged
        Try
            If cboPerProvince.SelectedValue <> "" Then
                GetDistrictByProvinceID(cboPerDictrict, cboPerProvince.SelectedValue, cboPerDictrict.SelectedValue)
                If cboContactProvince.SelectedValue = "" Then
                    cboContactProvince.SelectedValue = cboPerProvince.SelectedValue
                    GetDistrictByProvinceID(cboContractDictrict, cboContactProvince.SelectedValue, cboContractDictrict.SelectedValue)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboContactNation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContactNation.SelectedIndexChanged
        Try
            If cboContactNation.SelectedValue <> "" Then
                GetProvinceByNationID(cboContactProvince, cboContractDictrict, cboContactNation.SelectedValue, cboContactProvince.SelectedValue)

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboContactProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContactProvince.SelectedIndexChanged
        Try
            If cboContactProvince.SelectedValue <> "" Then
                GetDistrictByProvinceID(cboContractDictrict, cboContactProvince.SelectedValue, cboContractDictrict.SelectedValue)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboPerDictrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPerDictrict.SelectedIndexChanged
        Try
            Dim dtData As DataTable
            If cboPerDictrict.SelectedValue <> "" Then
                Using rep As New RecruitmentRepository
                    dtData = rep.GetWardList(cboPerDictrict.SelectedValue, True)
                    'FillDropDownList(cbPerward, dtData.AsEnumerable(), "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbPerward.SelectedValue)
                    FillRadCombobox(cbPerward, dtData, "NAME", "ID")
                End Using
            Else
                cbPerward.Text = ""
                'If cboContractDictrict.Text.Trim() <> String.Empty Then
                'Else
                '    cboContractDictrict.SelectedIndex = cboPerDictrict.SelectedIndex
                'End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboDesire_Position_1_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDesire_Position_1.SelectedIndexChanged
        Try
            Dim sp As New RecruitmentStoreProcedure
            If cboDesire_Position_1.SelectedValue <> "" Then
                Using rep As New RecruitmentRepository
                    Dim lst = New List(Of RC_USER_TITLE_PERMISIONDTO)
                    lst = rep.GETUSER_TITLE_PERMISION_BY_USER()
                    If lst.Count > 0 Then
                        Dim posi = cboDesire_Position_1.SelectedValue
                        Dim DTTIT = sp.GET_TITLE_GROUP_ID(posi)
                        Dim check = (From p In lst Where p.GROUP_TITLE_ID = DTTIT).Any
                        If check = False Then
                            ctrlMessageBox.MessageText = Translate("Vị trí chọn không thuộc phân quyền của bạn")
                            ctrlMessageBox.ActionName = CommonMessage.VIEW_TITLE_NEW
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        End If
                    End If

                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboDesire_Position_2_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDesire_Position_2.SelectedIndexChanged
        Try
            Dim sp As New RecruitmentStoreProcedure
            If cboDesire_Position_2.SelectedValue <> "" Then
                Using rep As New RecruitmentRepository
                    Dim lst = New List(Of RC_USER_TITLE_PERMISIONDTO)
                    lst = rep.GETUSER_TITLE_PERMISION_BY_USER()
                    If lst.Count > 0 Then
                        Dim posi = cboDesire_Position_2.SelectedValue
                        Dim DTTIT = sp.GET_TITLE_GROUP_ID(posi)
                        Dim check = (From p In lst Where p.GROUP_TITLE_ID = DTTIT).Any
                        If check = False Then
                            ctrlMessageBox.MessageText = Translate("Vị trí chọn không thuộc phân quyền của bạn")
                            ctrlMessageBox.ActionName = CommonMessage.VIEW_TITLE_EDIT
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        End If
                    End If

                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetProvinceByNationID(ByVal cboPerPro As Telerik.Web.UI.RadComboBox, ByVal cboDitr As Telerik.Web.UI.RadComboBox, Optional ByVal sNationID As String = "", Optional ByVal sProvinceID As String = "")
        Try
            Dim lstProvinces As New List(Of ProvinceDTO)
            Dim comboBoxDataDTO As New Recruitment.RecruitmentBusiness.ComboBoxDataDTO

            'Kiem tra trong ViewState

            Dim rep As New RecruitmentRepository
            lstComboData = New Recruitment.RecruitmentBusiness.ComboBoxDataDTO
            lstComboData.GET_NATION = True      'Lấy danh sách quốc gia
            lstComboData.GET_PROVINCE = True    'Lấy danh sách Tỉnh thành
            lstComboData.GET_DISTRICT = True     'Lấy danh sách Quận huyện
            rep.GetComboList(lstComboData) 'Lấy danh sách các Combo.


            If lstComboData.LIST_PROVINCE.Count > 0 Then
                If sNationID <> "" Then
                    lstProvinces = (From p In lstComboData.LIST_PROVINCE Where p.NATION_ID = sNationID).ToList
                End If
            End If
            FillDropDownList(cboPerPro, lstProvinces, "NAME_VN", "ID", Common.Common.SystemLanguage, True, sProvinceID)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDistrictByProvinceID(ByVal rcbChild As Telerik.Web.UI.RadComboBox, Optional ByVal sProvince As String = "", Optional ByVal sDistrictID As String = "")
        Try
            Dim lstDistricts As New List(Of DistrictDTO)
            Dim comboBoxDataDTO As New Recruitment.RecruitmentBusiness.ComboBoxDataDTO
            'Kiem tra trong ViewState
            If Me.lstComboData Is Nothing Then 'Nếu ko có thì lấy từ Database
                Dim rep As New RecruitmentRepository
                lstComboData = New Recruitment.RecruitmentBusiness.ComboBoxDataDTO
                lstComboData.GET_NATION = True      'Lấy danh sách quốc gia
                lstComboData.GET_PROVINCE = True    'Lấy danh sách Tỉnh thành
                lstComboData.GET_DISTRICT = True     'Lấy danh sách Quận huyện
                rep.GetComboList(lstComboData) 'Lấy danh sách các Combo.
            End If
            If lstComboData.LIST_DISTRICT.Count > 0 Then
                If sProvince <> "" Then
                    lstDistricts = (From p In lstComboData.LIST_DISTRICT Where p.PROVINCE_ID = sProvince).ToList
                End If
                FillDropDownList(rcbChild, lstDistricts, "NAME_VN", "ID", Common.Common.SystemLanguage, True, sDistrictID)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboContractDictrict_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContractDictrict.SelectedIndexChanged
        Try
            Dim dtData As DataTable
            If cboContractDictrict.SelectedValue <> "" Then
                Using rep As New RecruitmentRepository
                    dtData = rep.GetWardList(cboContractDictrict.SelectedValue, True)
                    'FillDropDownList(cboContractWard, dtData, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboContractWard.SelectedValue)
                    FillRadCombobox(cboContractWard, dtData, "NAME", "ID")
                End Using
            Else
                cboContractWard.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    ' Chuyen trang thai control
    Public Sub DisableControls(ByVal control As System.Web.UI.Control, ByVal status As Boolean)
        For Each c As System.Web.UI.Control In control.Controls
            If TypeOf c Is RadTabStrip Then
            Else
                ' Get the Enabled property by reflection.
                Dim type As Type = c.GetType
                Dim prop As PropertyInfo = type.GetProperty("Enabled")

                ' Set it to False to disable the control.
                If Not prop Is Nothing Then
                    prop.SetValue(c, status, Nothing)
                End If
                ' Recurse into child controls.
                If c.Controls.Count > 0 Then
                    Me.DisableControls(c, status)
                End If
            End If

        Next

    End Sub
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
      Handles cboTKNganHang.ItemsRequested, cboTKChiNhanhNganHang.ItemsRequested
        Using rep As New ProfileRepository
            Dim dtData As DataTable
            Dim sText As String = e.Text
            Dim dValue As Decimal
            Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
            Select Case sender.ID

                Case cboTKNganHang.ID
                    dtData = rep.GetBankList(True)
                Case cboTKChiNhanhNganHang.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetBankBranchList(dValue, True)
            End Select

            If sText <> "" Then
                Dim dtExist = (From p In dtData
                               Where p("NAME") IsNot DBNull.Value AndAlso
                               p("NAME").ToString.ToUpper = sText.ToUpper)

                If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso
                                    p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = dtData.Rows.Count 'Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboTitle.ID
                            '    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                Else

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = dtData.Rows.Count
                    e.EndOfItems = True

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboTitle.ID
                            '    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = dtData.Rows.Count 'Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count

                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    Select Case sender.ID
                        'Case cboTitle.ID
                        '    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                    End Select
                    sender.Items.Add(radItem)
                Next
            End If
        End Using
    End Sub
    'Private Sub LoadComboTitle()

    '    If hidOrgID.Value <> "" Then
    '        Dim dtData As DataTable
    '        dtData = store.GET_TITLE_IN_PLAN(hidOrgID.Value, 0)
    '        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
    '    Else
    '        cboTitle.Items.Clear()
    '        cboTitle.ClearSelection()
    '        cboTitle.Text = ""
    '    End If


    'End Sub
End Class