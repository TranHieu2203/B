﻿Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_CVPoolDtlProfile
    Inherits CommonView
    'Mã nhân viên
    Dim CandidateCode As String

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

            GetParams()
            UpdateControlState()
            Refresh()
            CurrentPlaceHolder = Me.ViewName
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Đỗ dữ liệu vào form
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        If Not IsPostBack Then
            'Dim rep As New RecruitmentRepository
            'Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
            'txtOrgName.Text = objPro.ORG_NAME
            'hidOrg.Value = objPro.ORG_ID
            'txtTitleName.Text = objPro.TITLE_NAME
            'hidTitle.Value = objPro.TITLE_ID
        End If

        If Not isLoad Then
            Using rep As New RecruitmentRepository
                'Trường hợp có thông tin về nhân viên đó thì fill lên các control.
                If CandidateInfo IsNot Nothing Then
                    'Phần hồ sơ
                    hidID.Value = CandidateInfo.ID.ToString()
                    hidOrg.Value = CandidateInfo.ORG_ID.ToString()
                    hidTitle.Value = If(CandidateInfo.TITLE_ID IsNot Nothing, CandidateInfo.TITLE_ID.ToString(), 0)
                    txtEmpCODE.Text = CandidateInfo.CANDIDATE_CODE
                    txtFirstNameVN.Text = CandidateInfo.FIRST_NAME_VN
                    txtLastNameVN.Text = CandidateInfo.LAST_NAME_VN
                    hidOrg.Value = CandidateInfo.ORG_ID

                    'txtOrgName.Text = CandidateInfo.ORG_NAME


                    'Phần sơ yếu lý lịch
                    Dim empCV = rep.GetCandidateCV(CandidateInfo.ID)
                    If empCV IsNot Nothing Then
                        If empCV.GENDER IsNot Nothing Then
                            cboGender.SelectedValue = empCV.GENDER
                        End If
                        If empCV.MARITAL_STATUS IsNot Nothing Then
                            cboTinhTrangHN.SelectedValue = empCV.MARITAL_STATUS
                        End If
                        If empCV.NATIVE IsNot Nothing Then
                            cboNative.SelectedValue = empCV.NATIVE
                        End If
                        If empCV.RELIGION IsNot Nothing Then
                            cboReligion.SelectedValue = empCV.RELIGION
                        End If
                        If empCV.ID_NO IsNot Nothing Then
                            rntxtCMND.Value = empCV.ID_NO
                        End If
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
                        If empCV.PER_ADDRESS IsNot Nothing Then
                            txtPerAddress.Text = empCV.PER_ADDRESS
                        End If
                        If empCV.PER_DISTRICT_ID IsNot Nothing Then
                            cboPerDictrict.SelectedValue = empCV.PER_DISTRICT_ID
                        End If
                        If empCV.PER_NATION_ID IsNot Nothing Then
                            cboPerNation.SelectedValue = empCV.PER_NATION_ID
                        End If
                        If empCV.PER_PROVINCE IsNot Nothing Then
                            cboPerProvince.SelectedValue = empCV.PER_PROVINCE
                        End If
                        If empCV.CONTACT_ADDRESS IsNot Nothing Then
                            txtContactAddress.Text = empCV.CONTACT_ADDRESS
                        End If
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
                        If empCV.CONTACT_ADDRESS_TEMP IsNot Nothing Then
                            txtContactAddress.Text = empCV.CONTACT_ADDRESS_TEMP
                        End If
                        If empCV.CONTACT_NATION_TEMP IsNot Nothing Then
                            cboContactNation.SelectedValue = empCV.CONTACT_NATION_TEMP
                        End If
                        If empCV.CONTACT_PROVINCE_TEMP IsNot Nothing Then
                            cboContactProvince.SelectedValue = empCV.CONTACT_PROVINCE_TEMP
                        End If
                        If empCV.CONTACT_DISTRICT_TEMP IsNot Nothing Then
                            cboContractDictrict.SelectedValue = empCV.CONTACT_DISTRICT_TEMP
                        End If
                        If empCV.CONTACT_MOBILE IsNot Nothing Then
                            txtSoDienThoaiCaNhan.Text = empCV.CONTACT_MOBILE
                        End If
                        If empCV.CONTACT_PHONE IsNot Nothing Then
                            txtSoDienThoaiCoDinh.Text = empCV.CONTACT_PHONE
                        End If
                        If empCV.WORK_EMAIl IsNot Nothing Then
                            cv_txtEmailCaNhanCongTy.Text = empCV.WORK_EMAIl
                        End If
                        If empCV.PER_EMAIL IsNot Nothing Then
                            txtEmailCaNhan.Text = empCV.PER_EMAIL
                        End If
                        If empCV.PERTAXCODE IsNot Nothing Then
                            txtMST.Text = empCV.PERTAXCODE
                        End If
                        If empCV.PER_TAX_DATE IsNot Nothing Then
                            rdNgayCapMST.SelectedDate = empCV.PER_TAX_DATE
                        End If
                        If empCV.PER_TAX_PLACE IsNot Nothing Then
                            txtNoiCapMST.Text = empCV.PER_TAX_PLACE
                        End If

                        'Nguoi than

                        If empCV.PASSPORT_DATE_EXPIRATION IsNot Nothing Then
                            rdPassportEnd.SelectedDate = empCV.PASSPORT_DATE_EXPIRATION
                        End If
                        If empCV.PASSPORT_ID IsNot Nothing Then
                            txtPassport.Text = empCV.PASSPORT_ID
                        End If
                        If empCV.PASSPORT_DATE IsNot Nothing Then
                            rdPassport.SelectedDate = empCV.PASSPORT_DATE
                        End If
                        If empCV.PASSPORT_PLACE_NAME IsNot Nothing Then
                            txtPassportNoiCap.Text = empCV.PASSPORT_PLACE_NAME
                        End If
                        If empCV.VISA_NUMBER IsNot Nothing Then
                            txtSoViSa.Text = empCV.VISA_NUMBER
                        End If
                        If empCV.VISA_DATE IsNot Nothing Then
                            rdNgayCapViSa.SelectedDate = empCV.VISA_DATE
                        End If
                        If empCV.VISA_DATE_EXPIRATION IsNot Nothing Then
                            rdNgayHetHanVisa.SelectedDate = empCV.VISA_DATE_EXPIRATION
                        End If
                        If empCV.VISA_PLACE IsNot Nothing Then
                            txtNoiCapVisa.Text = empCV.VISA_PLACE
                        End If
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
                        If empCV.LABOUR_PLACE IsNot Nothing Then
                            txtLaoDongNoiCap.Text = empCV.LABOUR_PLACE
                        End If
                        If empCV.WORK_PERMIT IsNot Nothing Then
                            txtGiayPhepLaoDong.Text = empCV.WORK_PERMIT
                        End If
                        If empCV.WORK_PERMIT_START IsNot Nothing Then
                            rdGiayPhepLaoDongTyNgay.SelectedDate = empCV.WORK_PERMIT_START
                        End If
                        If empCV.WORK_PERMIT_END IsNot Nothing Then
                            rdGiayPhepLaoDongDenNgay.SelectedDate = empCV.WORK_PERMIT_END
                        End If
                        If empCV.TEMP_RESIDENCE_CARD IsNot Nothing Then
                            txtTheTamTru.Text = empCV.TEMP_RESIDENCE_CARD
                        End If
                        If empCV.TEMP_RESIDENCE_CARD_START IsNot Nothing Then
                            rdTheTamTruTuNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_START
                        End If
                        If empCV.TEMP_RESIDENCE_CARD_END IsNot Nothing Then
                            rdTheTamTruDenNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_END
                        End If
                        'dia chia lien lac
                        If empCV.CON_COUNTRY IsNot Nothing Then
                            cboContactAddNation.SelectedValue = empCV.CON_COUNTRY
                        End If
                        If empCV.CON_PROVINCE IsNot Nothing Then
                            cboContactAddProvince.SelectedValue = empCV.CON_PROVINCE
                        End If
                        If empCV.CON_DISTRICT IsNot Nothing Then
                            cboContractAddDictrict.SelectedValue = empCV.CON_DISTRICT
                        End If


                    End If

                    'Phần trình độ
                    Dim EmpEducation = rep.GetCandidateEdu(CandidateInfo.ID)
                    If EmpEducation IsNot Nothing Then
                        If EmpEducation.ACADEMY IsNot Nothing Then
                            cboTrinhDoVanHoa.SelectedValue = EmpEducation.ACADEMY
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
                        If EmpEducation.IT_CERTIFICATE IsNot Nothing Then
                            txtDegreeChungChi1.Text = EmpEducation.IT_CERTIFICATE
                        End If
                        If EmpEducation.IT_LEVEL IsNot Nothing Then
                            cboDegreeTrinhDo1.SelectedValue = EmpEducation.IT_LEVEL
                        End If
                        If EmpEducation.IT_MARK IsNot Nothing Then
                            txtDegreeDiemSoXepLoai1.Text = EmpEducation.IT_MARK
                        End If
                        If EmpEducation.IT_CERTIFICATE1 IsNot Nothing Then
                            txtDegreeChungChi2.Text = EmpEducation.IT_CERTIFICATE1
                        End If
                        If EmpEducation.IT_LEVEL1 IsNot Nothing Then
                            cboDegreeTrinhDo2.SelectedValue = EmpEducation.IT_LEVEL1
                        End If
                        If EmpEducation.IT_MARK1 IsNot Nothing Then
                            txtDegreeDiemSoXepLoai2.Text = EmpEducation.IT_MARK1
                        End If
                        If EmpEducation.IT_CERTIFICATE2 IsNot Nothing Then
                            txtDegreeChungChi3.Text = EmpEducation.IT_CERTIFICATE2
                        End If
                        If EmpEducation.IT_LEVEL2 IsNot Nothing Then
                            cboDegreeTrinhDo3.SelectedValue = EmpEducation.IT_LEVEL2
                        End If
                        If EmpEducation.IT_MARK2 IsNot Nothing Then
                            txtDegreeDiemSoXepLoai3.Text = EmpEducation.IT_MARK2
                        End If
                        If EmpEducation.ENGLISH IsNot Nothing Then
                            txtTDNNNgoaiNgu1.Text = EmpEducation.ENGLISH
                        End If
                        If EmpEducation.ENGLISH_LEVEL IsNot Nothing Then
                            cboTDNNTrinhDo1.SelectedValue = EmpEducation.ENGLISH_LEVEL
                        End If
                        If EmpEducation.ENGLISH_MARK IsNot Nothing Then
                            txtTDNNDiem1.Text = EmpEducation.ENGLISH_MARK
                        End If
                        If EmpEducation.ENGLISH1 IsNot Nothing Then
                            txtTDNNNgoaiNgu2.Text = EmpEducation.ENGLISH1
                        End If
                        If EmpEducation.ENGLISH_LEVEL1 IsNot Nothing Then
                            cboTDNNTrinhDo2.SelectedValue = EmpEducation.ENGLISH_LEVEL1
                        End If
                        If EmpEducation.ENGLISH_MARK1 IsNot Nothing Then
                            txtTDNNDiem2.Text = EmpEducation.ENGLISH_MARK1
                        End If
                        If EmpEducation.ENGLISH2 IsNot Nothing Then
                            txtTDNNNgoaiNgu3.Text = EmpEducation.ENGLISH2
                        End If
                        If EmpEducation.ENGLISH_LEVEL2 IsNot Nothing Then
                            cboTDNNTrinhDo3.SelectedValue = EmpEducation.ENGLISH_LEVEL2
                        End If
                        If EmpEducation.ENGLISH_MARK2 IsNot Nothing Then
                            txtTDNNDiem3.Text = EmpEducation.ENGLISH_MARK2
                        End If
                        If EmpEducation.DATE_START IsNot Nothing Then
                            txtEduDateStart.SelectedDate = EmpEducation.DATE_START
                        End If
                        If EmpEducation.DATE_END IsNot Nothing Then
                            txtEduDateEnd.SelectedDate = EmpEducation.DATE_END
                        End If
                        If EmpEducation.ENGLISH_SKILL IsNot Nothing Then
                            txtEduSkill.Text = EmpEducation.ENGLISH_SKILL
                        End If

                    End If

                    'Candidate Other
                    Dim EmpOtherInfo = rep.GetCandidateOtherInfo(CandidateInfo.ID)
                    If EmpOtherInfo IsNot Nothing Then
                        chkCongDoanPhi.Checked = If(EmpOtherInfo.DOAN_PHI Is Nothing, False, EmpOtherInfo.DOAN_PHI)
                        If EmpOtherInfo.NGAY_VAO_DOAN IsNot Nothing Then
                            rdNgayVaoCongDoan.SelectedDate = EmpOtherInfo.NGAY_VAO_DOAN
                        End If
                        If EmpOtherInfo.NOI_VAO_DOAN IsNot Nothing Then
                            txtNoiVaoCongDoan.Text = EmpOtherInfo.NOI_VAO_DOAN
                        End If
                        If EmpOtherInfo.ACCOUNT_NAME IsNot Nothing Then
                            txtTKNguoiThuHuong.Text = EmpOtherInfo.ACCOUNT_NAME
                        End If
                        txtTKTKChuyenKhoan.Text = If(EmpOtherInfo.ACCOUNT_NUMBER Is Nothing, "", EmpOtherInfo.ACCOUNT_NUMBER)
                        If EmpOtherInfo.BANK IsNot Nothing Then
                            cboTKNganHang.SelectedValue = EmpOtherInfo.BANK
                        End If
                        If EmpOtherInfo.BANK_BRANCH IsNot Nothing Then
                            cboTKChiNhanhNganHang.SelectedValue = EmpOtherInfo.BANK_BRANCH
                        End If
                        other_cbxThanhToanQuaNH.Checked = If(EmpOtherInfo.IS_PAYMENT_VIA_BANK Is Nothing, False, EmpOtherInfo.IS_PAYMENT_VIA_BANK)
                        If EmpOtherInfo.ACCOUNT_EFFECT_DATE IsNot Nothing Then
                            rdpTKNgayHieuLuc.SelectedDate = EmpOtherInfo.ACCOUNT_EFFECT_DATE
                        End If
                    End If
                    'Candidate Health
                    Dim EmpHealthInfo = rep.GetCandidateHealthInfo(CandidateInfo.ID)
                    If EmpHealthInfo IsNot Nothing Then
                        If EmpHealthInfo.CHIEU_CAO IsNot Nothing Then
                            txtChieuCao.Text = EmpHealthInfo.CHIEU_CAO
                        End If
                        If EmpHealthInfo.CAN_NANG IsNot Nothing Then
                            txtCanNang.Text = EmpHealthInfo.CAN_NANG
                        End If
                        If EmpHealthInfo.NHOM_MAU IsNot Nothing Then
                            txtNhomMau.Text = EmpHealthInfo.NHOM_MAU
                        End If
                        If EmpHealthInfo.HUYET_AP IsNot Nothing Then
                            txtHuyetAp.Text = EmpHealthInfo.HUYET_AP
                        End If
                        If EmpHealthInfo.MAT_TRAI IsNot Nothing Then
                            txtMatTrai.Text = EmpHealthInfo.MAT_TRAI
                        End If
                        If EmpHealthInfo.MAT_PHAI IsNot Nothing Then
                            txtMatPhai.Text = EmpHealthInfo.MAT_PHAI
                        End If
                        'If EmpHealthInfo.LOAI_SUC_KHOE IsNot Nothing Then
                        '    cboLoaiSucKhoe.SelectedValue = EmpHealthInfo.LOAI_SUC_KHOE
                        'End If
                        If EmpHealthInfo.TAI_MUI_HONG IsNot Nothing Then
                            txtTaiMuiHong.Text = EmpHealthInfo.TAI_MUI_HONG
                        End If
                        If EmpHealthInfo.RANG_HAM_MAT IsNot Nothing Then
                            txtRangHamMat.Text = EmpHealthInfo.RANG_HAM_MAT
                        End If
                        If EmpHealthInfo.TIM IsNot Nothing Then
                            txtTim.Text = EmpHealthInfo.TIM
                        End If
                        If EmpHealthInfo.PHOI_NGUC IsNot Nothing Then
                            txtPhoiNguc.Text = EmpHealthInfo.PHOI_NGUC
                        End If
                        If EmpHealthInfo.VIEM_GAN_B IsNot Nothing Then
                            txtVienGanB.Text = EmpHealthInfo.VIEM_GAN_B
                        End If
                        If EmpHealthInfo.DA_HOA_LIEU IsNot Nothing Then
                            txtDaHoaLieu.Text = EmpHealthInfo.DA_HOA_LIEU
                        End If
                        If EmpHealthInfo.GHI_CHU_SUC_KHOE IsNot Nothing Then
                            txtGhiChuSK.Text = EmpHealthInfo.GHI_CHU_SUC_KHOE
                        End If
                    End If
                    'Candidate Nguyện vọng
                    Dim EmpExpectInfo = rep.GetCandidateExpectInfo(CandidateInfo.ID)
                    If EmpExpectInfo IsNot Nothing Then
                        If EmpExpectInfo.TIME_START IsNot Nothing Then
                            cboExpectThoiGianLamViec.SelectedValue = EmpExpectInfo.TIME_START
                        End If
                        txtExpectMucLuongThuViec.Text = If(EmpExpectInfo.PROBATIONARY_SALARY Is Nothing, "", EmpExpectInfo.PROBATIONARY_SALARY)
                        txtExpectMucLuongChinhThuc.Text = If(EmpExpectInfo.OFFICIAL_SALARY Is Nothing, "", EmpExpectInfo.OFFICIAL_SALARY)
                        If EmpExpectInfo.DATE_START IsNot Nothing Then
                            txtExpectNgayBatDau.SelectedDate = EmpExpectInfo.DATE_START
                        End If
                        txtExpectDeNghiKhac.Text = If(EmpExpectInfo.OTHER_REQUEST Is Nothing, "", EmpExpectInfo.OTHER_REQUEST)
                    End If

                    'Candidate lich su ung tuyen
                    Dim EmpHistory = rep.GetCandidateHistory(CandidateInfo.ID, 0)
                    If EmpHistory IsNot Nothing Then
                        rgDataHistory.DataSource = EmpHistory
                    End If
                End If
            End Using
            isLoad = True
        End If
    End Sub

    'Lấy tham số Params URL
    Private Sub GetParams()
        Try
            'hidProgramID.Value = Request.Params("PROGRAM_ID")
            If CurrentState Is Nothing Then

                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
            End If

            Dim rep As New RecruitmentRepository
            CandidateInfo = rep.GetCandidateInfo(Request.Params("Can"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Quyền truy cập PanelBar
    'Public Sub AccessPanelBar()
    '    Try
    '        Dim i As Integer = 0
    '        ' lặp item của RadPanelBar
    '        While (i < rtabRecruitmentInfo.Tabs.Count)
    '            ' lấy item của đang lặp
    '            Dim itm As RadTab = rtabRecruitmentInfo.Tabs(i)
    '            Using rep As New CommonRepository
    '                ' lấy thông tin user đang đăng nhập
    '                Dim user = LogHelper.CurrentUser
    '                ' check xem có phải admin không
    '                Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Utilities.GetUsername)
    '                ' nếu không phải admin
    '                If GroupAdmin = False Then
    '                    ' lấy quyền của user
    '                    Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Utilities.GetUsername)
    '                    ' nếu có quyền
    '                    If permissions IsNot Nothing Then
    '                        ' kiểm tra các chức năng ngoại trừ chức năng là báo cáo
    '                        ' thay vì .value e phải xài .ID đúng không nào ;)
    '                        Dim isPermissions = (From p In permissions Where p.FID = itm.PageViewID).Any
    '                        ' nếu không tồn tại --> xóa item
    '                        If Not isPermissions Then
    '                            rtabRecruitmentInfo.Tabs(i).Visible = False
    '                            i = i + 1
    '                            Continue While
    '                        Else
    '                            'Set mặc định tabs đầu tiên được chọn
    '                            If flagTab = 0 Then
    '                                rtabRecruitmentInfo.Tabs.Item(i).Selected = True
    '                                RadMultiPage1.SelectedIndex = i
    '                                flagTab = 1
    '                            End If

    '                        End If
    '                    End If
    '                Else
    '                    ' nếu là admin + có quyền
    '                    If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
    '                    Else
    '                        ' xóa nếu admin không có quyền
    '                        rtabRecruitmentInfo.Tabs.RemoveAt(i)
    '                        Continue While
    '                    End If
    '                End If
    '            End Using
    '            i = i + 1
    '        End While
    '        ' làm tưởng tự tabstrip
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Đổ dữ liệu vào Combobox
    Private Sub GetDataCombo()
        Dim rep As New RecruitmentRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Recruitment.RecruitmentBusiness.ComboBoxDataDTO
                ListComboData.GET_BANK = True
                ListComboData.GET_BANK_BRACH = True
                ListComboData.GET_DISTRICT = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                rep.GetComboList(ListComboData)
            End If
            Dim dtData

            '################### Sơ yếu lý lịch ####################
            ' Giới tính
            dtData = rep.GetOtherList("GENDER", True)
            FillRadCombobox(cboGender, dtData, "NAME", "ID")

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
            ' Tỉnh thành dia chi lien lac
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

            ' Nguyen quán
            FillDropDownList(cboNavNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNavNation.SelectedValue)

            ' Quốc tịch
            FillDropDownList(cboNationality, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationality.SelectedValue)

            '################### Thông tin trình độ ################
            'Trình độ văn hóa
            dtData = rep.GetOtherList("ACADEMY", True)
            FillRadCombobox(cboTrinhDoVanHoa, dtData, "NAME", "ID")
            'Trình độ học vấn
            dtData = rep.GetOtherList("LEARNING_LEVEL", True)
            FillRadCombobox(cboTrinhDoHocVan, dtData, "NAME", "ID")
            'Trình độ chuyên môn ('MAJOR)
            dtData = rep.GetOtherList("MAJOR", True)
            FillRadCombobox(cboTrinhDoChuyenMon, dtData, "NAME", "ID", True)
            'Trường học
            dtData = rep.GetOtherList("SCHOOL", True)
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
            dtData = rep.GetOtherList("COMPUTER_LEVEL", True)
            FillRadCombobox(cboDegreeTrinhDo1, dtData, "NAME", "ID")
            FillRadCombobox(cboDegreeTrinhDo2, dtData, "NAME", "ID")
            FillRadCombobox(cboDegreeTrinhDo3, dtData, "NAME", "ID")

            'Trình độ ngoại ngữ
            dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
            FillRadCombobox(cboTDNNTrinhDo1, dtData, "NAME", "ID")
            FillRadCombobox(cboTDNNTrinhDo2, dtData, "NAME", "ID")
            FillRadCombobox(cboTDNNTrinhDo3, dtData, "NAME", "ID")

            'Ngân hàng
            FillDropDownList(cboTKNganHang, ListComboData.LIST_BANK, "BANK_NAME", "ID", Common.Common.SystemLanguage, True, cboTKNganHang.SelectedValue)

            'Chi nhánh ngân hàng
            FillDropDownList(cboTKChiNhanhNganHang, ListComboData.LIST_BANK_BRACH, "BRANCH_NAME", "ID", Common.Common.SystemLanguage, True, cboTKChiNhanhNganHang.SelectedValue)
            ' CMND
            dtData = rep.GetOtherList("ID_PLACE", True)
            FillRadCombobox(cboCMNDPlace, dtData, "NAME", "ID")
            ' Nguyện vọng thời gian làm việc
            dtData = rep.GetOtherList("WORK_TIME", True)
            FillRadCombobox(cboExpectThoiGianLamViec, dtData, "NAME", "ID")
            ' Loại sức khỏe
            dtData = rep.GetOtherList("RC_HEALTH_STATUS", True)
            FillRadCombobox(cboLoaiSucKhoe, dtData, "NAME", "ID")
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
                         ToolbarItem.Edit,
                         ToolbarItem.Seperator,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Dim ajaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
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

                Case TOOLBARITEM_CREATE

                    ResetControlValue()
                    CandidateInfo = Nothing
                    CurrentState = CommonMessage.STATE_NEW
                    'divFileAttach.Visible = True
                    'divGetFile.Visible = False

                    ' Tao moi ma nhan vien
                    EmpCode = rep.CreateNewCandidateCode()
                    txtEmpCODE.Text = EmpCode.CANDIDATE_CODE
                    IDemp = EmpCode.ID
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
                        Select Case CurrentState
                            Case STATE_NEW
                                If Save(strEmpCode, _err) Then
                                    'Page.Response.Redirect("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=" & strEmpCode & "&state=Normal&noscroll=1&message=success&reload=1")
                                    Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&gUID={0}&Can={1}&state=Normal&noscroll=1", hidID.Value, strEmpCode))
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
                    CurrentState = CommonMessage.STATE_NORMAL
                    'divFileAttach.Visible = False
                Case TOOLBARITEM_DELETE

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

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
                Page.Response.Redirect("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can=" & HttpContext.Current.Server.UrlEncode(strEmpCode) & "&state=Normal&Place=" & HttpContext.Current.Server.UrlEncode(CurrentPlaceHolder) & "&noscroll=1&reload=1")
            Else
                ShowMessage(Translate("Ứng viên không tồn tại."), Utilities.NotifyType.Error)
            End If
        End If
    End Sub

    Private Sub val_Same_Date_FullName_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles val_Same_Date_FullName.ServerValidate
        Try
            If CurrentState = STATE_NEW Then
                Using rep As New RecruitmentRepository
                    args.IsValid = rep.ValidateInsertCandidate(txtEmpCODE.Text, rntxtCMND.Text, txtFirstNameVN.Text & " " & txtLastNameVN.Text, rdBirthDate.SelectedDate, "DATE_FULLNAME")
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
                    args.IsValid = rep.ValidateInsertCandidate(txtEmpCODE.Text, rntxtCMND.Text, txtFirstNameVN.Text & " " & txtLastNameVN.Text, rdBirthDate.SelectedDate, "NO_ID")
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

#End Region

#Region "Custom"
    'Cập nhật trạng thái Control và tắt bật Popup
    Public Overrides Sub UpdateControlState()
        If CandidateInfo IsNot Nothing Then
            'lblFileName.Text = ""
            'If CandidateInfo.FILE_NAME <> "" Then
            '    divGetFile.Visible = True
            '    lblFileName.Text = CandidateInfo.FILE_NAME
            'End If
        End If

        If CurrentState Is Nothing Then
            CurrentState = STATE_NORMAL
            'divFileAttach.Visible = False
        End If
        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                'divGetFile.Visible = False
                btnSearchEmp.Enabled = True
                txtFirstNameVN.ReadOnly = False
                txtLastNameVN.ReadOnly = False
                txtEmpCODE.ReadOnly = True
            Case CommonMessage.STATE_EDIT

                btnSearchEmp.Enabled = False
                txtFirstNameVN.ReadOnly = False
                txtLastNameVN.ReadOnly = False
                txtEmpCODE.ReadOnly = True
            Case CommonMessage.STATE_NORMAL
                'divFileAttach.Visible = False
                btnSearchEmp.Enabled = True
                txtFirstNameVN.ReadOnly = True
                txtLastNameVN.ReadOnly = True
                txtEmpCODE.ReadOnly = False
        End Select

        ChangeToolbarState()
        'Me.Send(CurrentState)
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

        rntxtCMND.Value = Nothing
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
        'dia chi lien lac
        cboContactAddNation.SelectedIndex = 1
        cboContactAddProvince.SelectedIndex = 0
        cboContractAddDictrict.SelectedIndex = 0


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
            'CandidateInfo.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value) 'xemlai
            CandidateInfo.CANDIDATE_CODE = txtEmpCODE.Text.Trim()
            CandidateInfo.FIRST_NAME_VN = txtFirstNameVN.Text.Trim()
            CandidateInfo.LAST_NAME_VN = txtLastNameVN.Text.Trim()
            CandidateInfo.ORG_ID = Decimal.Parse(hidOrg.Value)
            CandidateInfo.TITLE_ID = Decimal.Parse(hidTitle.Value)


            'Candidate CV
            EmpCV = New CandidateCVDTO
            EmpCV.GENDER = cboGender.SelectedValue
            EmpCV.NATIVE = cboNative.SelectedValue
            EmpCV.MARITAL_STATUS = cboTinhTrangHN.SelectedValue
            EmpCV.RELIGION = cboReligion.SelectedValue
            If rntxtCMND.Text <> "" Then
                EmpCV.ID_NO = Decimal.Parse(rntxtCMND.Value)
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
            EmpCV.CONTACT_ADDRESS = txtContactAddress.Text
            If cboContactNation.SelectedValue <> "" Then
                EmpCV.CONTACT_NATION_ID = Decimal.Parse(cboContactNation.SelectedValue)
            End If
            If cboContactProvince.SelectedValue <> "" Then
                EmpCV.CONTACT_PROVINCE = Decimal.Parse(cboContactProvince.SelectedValue)
            End If
            If cboContractDictrict.SelectedValue <> "" Then
                EmpCV.CONTACT_DISTRICT_ID = Decimal.Parse(cboContractDictrict.SelectedValue)
            End If
            'dia chi lien lac
            If cboContactAddNation.SelectedValue <> "" Then
                EmpCV.CON_COUNTRY = Decimal.Parse(cboContactAddNation.SelectedValue)
            End If
            If cboContactAddProvince.SelectedValue <> "" Then
                EmpCV.CON_PROVINCE = Decimal.Parse(cboContactAddProvince.SelectedValue)
            End If
            If cboContractAddDictrict.SelectedValue <> "" Then
                EmpCV.CON_DISTRICT = Decimal.Parse(cboContractAddDictrict.SelectedValue)
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
            'dia chi lien lac
            If cboContactAddNation.SelectedValue IsNot Nothing And cboContactAddNation.SelectedValue <> "" Then
                EmpCV.CON_COUNTRY = Decimal.Parse(cboContactAddNation.SelectedValue)
            End If
            If cboContactAddProvince.SelectedValue IsNot Nothing And cboContactAddProvince.SelectedValue <> "" Then
                EmpCV.CON_PROVINCE = Decimal.Parse(cboContactAddProvince.SelectedValue)
            End If
            If cboContractAddDictrict.SelectedValue IsNot Nothing And cboContractAddDictrict.SelectedValue <> "" Then
                EmpCV.CON_DISTRICT = Decimal.Parse(cboContractAddDictrict.SelectedValue)
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
            EmpCV.WORK_PERMIT = txtGiayPhepLaoDong.Text
            EmpCV.WORK_PERMIT_START = rdGiayPhepLaoDongTyNgay.SelectedDate
            EmpCV.WORK_PERMIT_END = rdGiayPhepLaoDongDenNgay.SelectedDate
            EmpCV.TEMP_RESIDENCE_CARD = txtTheTamTru.Text
            EmpCV.TEMP_RESIDENCE_CARD_START = rdTheTamTruTuNgay.SelectedDate
            EmpCV.TEMP_RESIDENCE_CARD_END = rdTheTamTruDenNgay.SelectedDate

            'EmpEducation

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
            EmpExpectInfo.PROBATIONARY_SALARY = If(txtExpectMucLuongThuViec.Text = String.Empty, 0, 1)
            EmpExpectInfo.OFFICIAL_SALARY = If(txtExpectMucLuongChinhThuc.Text = String.Empty, 0, 1)
            EmpExpectInfo.DATE_START = txtExpectNgayBatDau.SelectedDate
            EmpExpectInfo.OTHER_REQUEST = txtExpectDeNghiKhac.Text

            If CandidateInfo IsNot Nothing Then
                If hidID.Value <> "" Then
                    CandidateInfo.ID = Decimal.Parse(hidID.Value)
                    result = rep.ModifyCandidate(CandidateInfo, gID, _binaryImage, EmpCV, EmpEducation, EmpOtherInfo, EmpHealthInfo, EmpExpectInfo, EmpFamily)
                Else
                    result = rep.InsertCandidate(CandidateInfo, gID, strEmpCode, _binaryImage, EmpCV, EmpEducation, EmpOtherInfo, EmpHealthInfo, EmpExpectInfo, EmpFamily)
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
            If cboPerDictrict.SelectedValue <> "" Then
                If cboContractDictrict.Text.Trim() <> String.Empty Then

                Else
                    cboContractDictrict.SelectedIndex = cboPerDictrict.SelectedIndex
                End If
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
    ''dia chi lien he
    Private Sub cboContactAddNation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContactAddNation.SelectedIndexChanged
        Try
            If cboContactAddNation.SelectedValue <> "" Then
                GetProvinceByNationID(cboContactAddProvince, cboContractAddDictrict, cboContactAddNation.SelectedValue, cboContactAddProvince.SelectedValue)

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboContactAddProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContactAddProvince.SelectedIndexChanged
        Try
            If cboContactAddProvince.SelectedValue <> "" Then
                GetDistrictByProvinceID(cboContractAddDictrict, cboContactAddProvince.SelectedValue, cboContractAddDictrict.SelectedValue)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

End Class