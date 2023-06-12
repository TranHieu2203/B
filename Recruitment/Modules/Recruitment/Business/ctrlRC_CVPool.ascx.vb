Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_CVPool
    Inherits CommonView
    'Mã nhân viên
    Dim CandidateCode As String
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Recruitment\Modules\Business" + Me.GetType().Name.ToString()
    'Popup

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
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
                        'txtEmployeeCode.Text = EmpCode.CANDIDATE_CODE
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
        'If Not IsPostBack Then
        '    Dim rep As New RecruitmentRepository
        '    Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
        '    'txtOrgName.Text = objPro.ORG_NAME
        '    'hidOrg.Value = objPro.ORG_ID
        '    'txtTitleName.Text = objPro.TITLE_NAME
        '    'hidTitle.Value = objPro.TITLE_ID
        'End If

        'If Not isLoad Then
        '    Using rep As New RecruitmentRepository
        '        'Trường hợp có thông tin về nhân viên đó thì fill lên các control.
        '        If CandidateInfo IsNot Nothing Then
        '            'Phần hồ sơ
        '            hidID.Value = CandidateInfo.ID.ToString()
        '            hidOrg.Value = CandidateInfo.ORG_ID.ToString()
        '            txtEmpCODE.Text = CandidateInfo.CANDIDATE_CODE
        '            txtFirstNameVN.Text = CandidateInfo.FIRST_NAME_VN
        '            txtLastNameVN.Text = CandidateInfo.LAST_NAME_VN
        '            hidOrg.Value = CandidateInfo.ORG_ID

        '            txtOrgName.Text = CandidateInfo.ORG_NAME


        '            'Phần sơ yếu lý lịch
        '            Dim empCV = rep.GetCandidateCV(CandidateInfo.ID)
        '            If empCV IsNot Nothing Then
        '                If empCV.GENDER IsNot Nothing Then
        '                    cboGender.SelectedValue = empCV.GENDER
        '                End If
        '                cboTinhTrangHN.SelectedValue = empCV.MARITAL_STATUS
        '                If empCV.NATIVE IsNot Nothing Then
        '                    cboNative.SelectedValue = empCV.NATIVE
        '                End If
        '                If empCV.RELIGION IsNot Nothing Then
        '                    cboReligion.SelectedValue = empCV.RELIGION
        '                End If
        '                rntxtCMND.Value = empCV.ID_NO
        '                If empCV.ID_DATE IsNot Nothing Then
        '                    rdCMNDDate.SelectedDate = empCV.ID_DATE
        '                End If
        '                If empCV.ID_PLACE IsNot Nothing Then
        '                    cboCMNDPlace.SelectedValue = empCV.ID_PLACE
        '                End If

        '                If empCV.BIRTH_DATE IsNot Nothing Then
        '                    rdBirthDate.SelectedDate = empCV.BIRTH_DATE
        '                End If
        '                If empCV.BIRTH_NATION_ID IsNot Nothing Then
        '                    cboNation.SelectedValue = empCV.BIRTH_NATION_ID
        '                End If
        '                If empCV.BIRTH_PROVINCE IsNot Nothing Then
        '                    cboProvince.SelectedValue = empCV.BIRTH_PROVINCE
        '                End If
        '                If empCV.NATIONALITY_ID IsNot Nothing Then
        '                    cboNationality.SelectedValue = empCV.NATIONALITY_ID
        '                End If
        '                If empCV.NAV_NATION_ID IsNot Nothing Then
        '                    cboNavNation.SelectedValue = empCV.NAV_NATION_ID
        '                End If
        '                If empCV.NAV_PROVINCE IsNot Nothing Then
        '                    cboNav_Province.SelectedValue = empCV.NAV_PROVINCE
        '                End If
        '                txtPerAddress.Text = empCV.PER_ADDRESS
        '                If empCV.PER_DISTRICT_ID IsNot Nothing Then
        '                    cboPerDictrict.SelectedValue = empCV.PER_DISTRICT_ID
        '                End If
        '                If empCV.PER_NATION_ID IsNot Nothing Then
        '                    cboPerNation.SelectedValue = empCV.PER_NATION_ID
        '                End If
        '                If empCV.PER_PROVINCE IsNot Nothing Then
        '                    cboPerProvince.SelectedValue = empCV.PER_PROVINCE
        '                End If
        '                txtContactAddress.Text = empCV.CONTACT_ADDRESS
        '                If empCV.CONTACT_NATION_ID IsNot Nothing Then
        '                    cboContactNation.SelectedValue = empCV.CONTACT_NATION_ID
        '                End If
        '                If empCV.CONTACT_PROVINCE IsNot Nothing Then
        '                    cboContactProvince.SelectedValue = empCV.CONTACT_PROVINCE
        '                End If
        '                If empCV.CONTACT_DISTRICT_ID IsNot Nothing Then
        '                    cboContractDictrict.SelectedValue = empCV.CONTACT_DISTRICT_ID
        '                End If

        '                If empCV.ID_DATE_EXPIRATION IsNot Nothing Then
        '                    rdCMNDEnd.SelectedDate = empCV.ID_DATE_EXPIRATION
        '                End If
        '                If empCV.IS_RESIDENT IsNot Nothing Then
        '                    cv_cbxKhongCuTru.Checked = empCV.IS_RESIDENT
        '                End If
        '                txtContactAddress.Text = empCV.CONTACT_ADDRESS_TEMP
        '                If empCV.CONTACT_NATION_TEMP IsNot Nothing Then
        '                    cboContactNation.SelectedValue = empCV.CONTACT_NATION_TEMP
        '                End If
        '                If empCV.CONTACT_PROVINCE_TEMP IsNot Nothing Then
        '                    cboContactProvince.SelectedValue = empCV.CONTACT_PROVINCE_TEMP
        '                End If
        '                If empCV.CONTACT_DISTRICT_TEMP IsNot Nothing Then
        '                    cboContractDictrict.SelectedValue = empCV.CONTACT_DISTRICT_TEMP
        '                End If
        '                txtSoDienThoaiCaNhan.Text = empCV.CONTACT_MOBILE
        '                txtSoDienThoaiCoDinh.Text = empCV.CONTACT_PHONE
        '                cv_txtEmailCaNhanCongTy.Text = empCV.WORK_EMAIl
        '                txtEmailCaNhan.Text = empCV.PER_EMAIL
        '                If empCV.PERTAXCODE IsNot Nothing Then
        '                    txtMST.Text = empCV.PERTAXCODE
        '                End If
        '                If empCV.PER_TAX_DATE IsNot Nothing Then
        '                    rdNgayCapMST.SelectedDate = empCV.PER_TAX_DATE
        '                End If
        '                txtNoiCapMST.Text = empCV.PER_TAX_PLACE
        '                Nguoi(than)

        '                If empCV.PASSPORT_DATE_EXPIRATION IsNot Nothing Then
        '                    rdPassportEnd.SelectedDate = empCV.PASSPORT_DATE_EXPIRATION
        '                End If
        '                txtPassport.Text = empCV.PASSPORT_ID
        '                If empCV.PASSPORT_DATE IsNot Nothing Then
        '                    rdPassport.SelectedDate = empCV.PASSPORT_DATE
        '                End If
        '                txtPassportNoiCap.Text = empCV.PASSPORT_PLACE_NAME

        '                If empCV.VISA_NUMBER IsNot Nothing Then
        '                    txtSoViSa.Text = empCV.VISA_NUMBER
        '                End If
        '                If empCV.VISA_DATE IsNot Nothing Then
        '                    rdNgayCapViSa.SelectedDate = empCV.VISA_DATE
        '                End If
        '                If empCV.VISA_DATE_EXPIRATION IsNot Nothing Then
        '                    rdNgayHetHanVisa.SelectedDate = empCV.VISA_DATE_EXPIRATION
        '                End If
        '                txtNoiCapVisa.Text = empCV.VISA_PLACE
        '                If empCV.VNAIRLINES_NUMBER IsNot Nothing Then
        '                    txtVNAirlines.Text = empCV.VNAIRLINES_NUMBER
        '                End If

        '                If empCV.VNAIRLINES_DATE IsNot Nothing Then
        '                    rdVNANgayCap.SelectedDate = empCV.VNAIRLINES_DATE
        '                End If
        '                If empCV.VNAIRLINES_DATE_EXPIRATION IsNot Nothing Then
        '                    rdVNAHetHan.SelectedDate = empCV.VNAIRLINES_DATE_EXPIRATION
        '                End If
        '                If empCV.VNAIRLINES_PLACE IsNot Nothing Then
        '                    txtVNANoiCap.Text = empCV.VNAIRLINES_PLACE
        '                End If
        '                If empCV.LABOUR_NUMBER IsNot Nothing Then
        '                    txtSoLaoDong.Text = empCV.LABOUR_NUMBER
        '                End If
        '                If empCV.LABOUR_DATE IsNot Nothing Then
        '                    rdLaoDongNgayCap.SelectedDate = empCV.LABOUR_DATE
        '                End If
        '                If empCV.LABOUR_DATE_EXPIRATION IsNot Nothing Then
        '                    rdLaoDongHetHan.SelectedDate = empCV.LABOUR_DATE_EXPIRATION
        '                End If
        '                txtLaoDongNoiCap.Text = empCV.LABOUR_PLACE
        '                txtGiayPhepLaoDong.Text = empCV.WORK_PERMIT
        '                If empCV.WORK_PERMIT_START IsNot Nothing Then
        '                    rdGiayPhepLaoDongTyNgay.SelectedDate = empCV.WORK_PERMIT_START
        '                End If
        '                If empCV.WORK_PERMIT_END IsNot Nothing Then
        '                    rdGiayPhepLaoDongDenNgay.SelectedDate = empCV.WORK_PERMIT_END
        '                End If
        '                txtTheTamTru.Text = empCV.TEMP_RESIDENCE_CARD
        '                If empCV.TEMP_RESIDENCE_CARD_START IsNot Nothing Then
        '                    rdTheTamTruTuNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_START
        '                End If
        '                If empCV.TEMP_RESIDENCE_CARD_END IsNot Nothing Then
        '                    rdTheTamTruDenNgay.SelectedDate = empCV.TEMP_RESIDENCE_CARD_END
        '                End If
        '            End If

        '            'Phần trình độ
        '            Dim EmpEducation = rep.GetCandidateEdu(CandidateInfo.ID)
        '            If EmpEducation IsNot Nothing Then
        '                If EmpEducation.ACADEMY IsNot Nothing Then
        '                    cboTrinhDoVanHoa.SelectedValue = EmpEducation.ACADEMY
        '                End If

        '                If EmpEducation.LEARNING_LEVEL IsNot Nothing Then
        '                    cboTrinhDoHocVan.SelectedValue = EmpEducation.LEARNING_LEVEL
        '                End If

        '                If EmpEducation.FIELD IsNot Nothing Then
        '                    cboTrinhDoChuyenMon.SelectedValue = EmpEducation.FIELD
        '                End If

        '                If EmpEducation.SCHOOL IsNot Nothing Then
        '                    cboTruongHoc.SelectedValue = EmpEducation.SCHOOL
        '                End If

        '                If EmpEducation.MAJOR IsNot Nothing Then
        '                    cboChuyenNganh.SelectedValue = EmpEducation.MAJOR
        '                End If

        '                If EmpEducation.DEGREE IsNot Nothing Then
        '                    cboBangCap.SelectedValue = EmpEducation.DEGREE
        '                End If
        '                If EmpEducation.MARK_EDU IsNot Nothing Then
        '                    cboXepLoai.SelectedValue = EmpEducation.MARK_EDU
        '                End If
        '                If EmpEducation.GPA IsNot Nothing Then
        '                    txtDiemTotNghiep.Text = EmpEducation.GPA
        '                End If

        '                txtDegreeChungChi1.Text = EmpEducation.IT_CERTIFICATE
        '                If EmpEducation.IT_LEVEL IsNot Nothing Then
        '                    cboDegreeTrinhDo1.SelectedValue = EmpEducation.IT_LEVEL
        '                End If
        '                If EmpEducation.IT_MARK IsNot Nothing Then
        '                    txtDegreeDiemSoXepLoai1.Text = EmpEducation.IT_MARK
        '                End If

        '                txtDegreeChungChi2.Text = EmpEducation.IT_CERTIFICATE1
        '                If EmpEducation.IT_LEVEL1 IsNot Nothing Then
        '                    cboDegreeTrinhDo2.SelectedValue = EmpEducation.IT_LEVEL1
        '                End If
        '                If EmpEducation.IT_MARK1 IsNot Nothing Then
        '                    txtDegreeDiemSoXepLoai2.Text = EmpEducation.IT_MARK1
        '                End If

        '                txtDegreeChungChi3.Text = EmpEducation.IT_CERTIFICATE2
        '                If EmpEducation.IT_LEVEL2 IsNot Nothing Then
        '                    cboDegreeTrinhDo3.SelectedValue = EmpEducation.IT_LEVEL2
        '                End If
        '                If EmpEducation.IT_MARK2 IsNot Nothing Then
        '                    txtDegreeDiemSoXepLoai3.Text = EmpEducation.IT_MARK2
        '                End If

        '                txtTDNNNgoaiNgu1.Text = EmpEducation.ENGLISH
        '                If EmpEducation.ENGLISH_LEVEL IsNot Nothing Then
        '                    cboTDNNTrinhDo1.SelectedValue = EmpEducation.ENGLISH_LEVEL
        '                End If
        '                If EmpEducation.ENGLISH_MARK IsNot Nothing Then
        '                    txtTDNNDiem1.Text = EmpEducation.ENGLISH_MARK
        '                End If

        '                txtTDNNNgoaiNgu2.Text = EmpEducation.ENGLISH1
        '                If EmpEducation.ENGLISH_LEVEL1 IsNot Nothing Then
        '                    cboTDNNTrinhDo2.SelectedValue = EmpEducation.ENGLISH_LEVEL1
        '                End If
        '                If EmpEducation.ENGLISH_MARK1 IsNot Nothing Then
        '                    txtTDNNDiem2.Text = EmpEducation.ENGLISH_MARK1
        '                End If

        '                txtTDNNNgoaiNgu3.Text = EmpEducation.ENGLISH2
        '                If EmpEducation.ENGLISH_LEVEL2 IsNot Nothing Then
        '                    cboTDNNTrinhDo3.SelectedValue = EmpEducation.ENGLISH_LEVEL2
        '                End If

        '                If EmpEducation.ENGLISH_MARK2 IsNot Nothing Then
        '                    txtTDNNDiem3.Text = EmpEducation.ENGLISH_MARK2
        '                End If
        '                txtEduDateStart.SelectedDate = EmpEducation.DATE_START
        '                txtEduDateEnd.SelectedDate = EmpEducation.DATE_END
        '                txtEduSkill.Text = EmpEducation.ENGLISH_SKILL
        '            End If

        '            'Candidate Other
        '            Dim EmpOtherInfo = rep.GetCandidateOtherInfo(CandidateInfo.ID)
        '            If EmpOtherInfo IsNot Nothing Then
        '                chkCongDoanPhi.Checked = If(EmpOtherInfo.DOAN_PHI Is Nothing, False, EmpOtherInfo.DOAN_PHI)
        '                If EmpOtherInfo.NGAY_VAO_DOAN IsNot Nothing Then
        '                    rdNgayVaoCongDoan.SelectedDate = EmpOtherInfo.NGAY_VAO_DOAN
        '                End If
        '                txtNoiVaoCongDoan.Text = EmpOtherInfo.NOI_VAO_DOAN
        '                txtTKNguoiThuHuong.Text = EmpOtherInfo.ACCOUNT_NAME
        '                txtTKTKChuyenKhoan.Text = If(EmpOtherInfo.ACCOUNT_NUMBER Is Nothing, "", EmpOtherInfo.ACCOUNT_NUMBER)
        '                cboTKNganHang.SelectedValue = EmpOtherInfo.BANK
        '                cboTKChiNhanhNganHang.SelectedValue = EmpOtherInfo.BANK_BRANCH
        '                other_cbxThanhToanQuaNH.Checked = If(EmpOtherInfo.IS_PAYMENT_VIA_BANK Is Nothing, False, EmpOtherInfo.IS_PAYMENT_VIA_BANK)
        '                If EmpOtherInfo.ACCOUNT_EFFECT_DATE IsNot Nothing Then
        '                    rdpTKNgayHieuLuc.SelectedDate = EmpOtherInfo.ACCOUNT_EFFECT_DATE
        '                End If
        '            End If
        '            'Candidate Health
        '            Dim EmpHealthInfo = rep.GetCandidateHealthInfo(CandidateInfo.ID)
        '            If EmpHealthInfo IsNot Nothing Then
        '                txtChieuCao.Text = EmpHealthInfo.CHIEU_CAO
        '                txtCanNang.Text = EmpHealthInfo.CAN_NANG
        '                txtNhomMau.Text = EmpHealthInfo.NHOM_MAU
        '                txtHuyetAp.Text = EmpHealthInfo.HUYET_AP
        '                txtMatTrai.Text = EmpHealthInfo.MAT_TRAI
        '                txtMatPhai.Text = EmpHealthInfo.MAT_PHAI
        '                cboLoaiSucKhoe.SelectedValue = EmpHealthInfo.LOAI_SUC_KHOE
        '                txtTaiMuiHong.Text = EmpHealthInfo.TAI_MUI_HONG
        '                txtRangHamMat.Text = EmpHealthInfo.RANG_HAM_MAT
        '                txtTim.Text = EmpHealthInfo.TIM
        '                txtPhoiNguc.Text = EmpHealthInfo.PHOI_NGUC
        '                txtVienGanB.Text = EmpHealthInfo.VIEM_GAN_B
        '                txtDaHoaLieu.Text = EmpHealthInfo.DA_HOA_LIEU
        '                txtGhiChuSK.Text = EmpHealthInfo.GHI_CHU_SUC_KHOE

        '            End If
        '            'Candidate Nguyện vọng
        '            Dim EmpExpectInfo = rep.GetCandidateExpectInfo(CandidateInfo.ID)
        '            If EmpExpectInfo IsNot Nothing Then
        '                cboExpectThoiGianLamViec.SelectedValue = EmpExpectInfo.TIME_START
        '                txtExpectMucLuongThuViec.Text = EmpExpectInfo.PROBATIONARY_SALARY
        '                txtExpectMucLuongChinhThuc.Text = EmpExpectInfo.OFFICIAL_SALARY
        '                txtExpectNgayBatDau.SelectedDate = EmpExpectInfo.DATE_START
        '                txtExpectDeNghiKhac.Text = EmpExpectInfo.OTHER_REQUEST
        '            End If
        '        End If
        '    End Using
        '    isLoad = True
        'End If
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
                'ListComboData.GET_BANK = True
                ListComboData.GET_BANK_BRACH = True
                ListComboData.GET_DISTRICT = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                rep.GetComboList(ListComboData)
            End If
            Dim dtData
            ' Giới tính
            dtData = rep.GetOtherList("GENDER", True)
            FillRadCombobox(cboGender, dtData, "NAME", "ID")
            'Tình trạng hôn nhân
            dtData = rep.GetOtherList("FAMILY_STATUS", True)
            FillRadCombobox(cboTinhTrangHN, dtData, "NAME", "ID")
            ' Quốc gia
            FillDropDownList(cboNation, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNation.SelectedValue)
            'Nơi sinh
            FillDropDownList(cboProvince, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince.SelectedValue)
            ' Quốc tịch
            FillDropDownList(cboNationality, ListComboData.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationality.SelectedValue)
            ' Tỉnh thành
            FillDropDownList(cboNav_Province, ListComboData.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNav_Province.SelectedValue)
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
            dtData = rep.GetOtherList("RC_TRAINING_SCHOOL", True)
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
            FillRadCombobox(cboTrinhDoTinHoc, dtData, "NAME", "ID")
            'Trình độ ngoại ngữ
            dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
            FillRadCombobox(cboTrinhDoNgoaiNgu, dtData, "NAME", "ID")
            ' Loại sức khỏe
            dtData = rep.GetOtherList("RC_HEALTH_STATUS", True)
            FillRadCombobox(cboLoaiSucKhoe, dtData, "NAME", "ID")
            ' Trạng thái ứng viên
            dtData = rep.GetOtherList("RC_CANDIDATE_STATUS", True)
            FillRadCombobox(cboEmpStatus, dtData, "NAME", "CODE")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Bind Data
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            'Dim dtData
            'Using rep As New RecruitmentRepository

            'End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Định nghĩa Control
    Protected Sub InitControl()
        Try
            'Khoi tao ToolBar
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Export)
            'CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            MainToolBar.Items(2).Text = Translate("Xuất file mẫu")
            MainToolBar.Items(3).Text = Translate("Nhập file mẫu")
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

                    'ResetControlValue()
                    'CandidateInfo = Nothing
                    'CurrentState = CommonMessage.STATE_NEW
                    ''divFileAttach.Visible = True
                    ''divGetFile.Visible = False

                    '' Tao moi ma nhan vien
                    'EmpCode = rep.CreateNewCandidateCode()
                    ''txtEmpCODE.Text = EmpCode.CANDIDATE_CODE
                    'IDemp = EmpCode.ID
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
                    'If Page.IsValid Then
                    '    Select Case CurrentState
                    '        Case STATE_NEW
                    '            If Save(strEmpCode, _err) Then
                    '                'Page.Response.Redirect("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=" & strEmpCode & "&state=Normal&noscroll=1&message=success&reload=1")
                    '                Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID={0}&Can={1}&state=Normal&ORGID={2}&TITLEID={3}&PROGRAM_ID={4}&noscroll=1", hidID.Value, strEmpCode, hidOrg.Value, hidTitle.Value, hidProgramID.Value))
                    '                Exit Sub
                    '            Else
                    '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                    '                Exit Sub
                    '            End If

                    '        Case STATE_EDIT
                    '            If Save(strEmpCode, _err) Then
                    '                CurrentState = CommonMessage.STATE_NORMAL
                    '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    '            Else
                    '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                    '            End If
                    '    End Select
                    'End If
                Case TOOLBARITEM_CANCEL
                    If CurrentState = CommonMessage.STATE_NEW Then 'Nếu là trạng thái new thì xóa ảnh hiện tại
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    'divFileAttach.Visible = False
                Case TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Danh sách ứng viên")
                            Exit Sub
                        End If
                    End Using
                Case TOOLBARITEM_DELETE

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case TOOLBARITEM_EXPORT_TEMPLATE
                    GetInformationLists()
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

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Import_Data()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
                'btnSearchEmp.Enabled = True
                'txtFirstNameVN.ReadOnly = False
                'txtLastNameVN.ReadOnly = False
                'txtEmpCODE.ReadOnly = True
            Case CommonMessage.STATE_EDIT

                'btnSearchEmp.Enabled = False
                'txtFirstNameVN.ReadOnly = False
                'txtLastNameVN.ReadOnly = False
                'txtEmpCODE.ReadOnly = True
            Case CommonMessage.STATE_NORMAL
                'divFileAttach.Visible = False
                'btnSearchEmp.Enabled = True
                'txtFirstNameVN.ReadOnly = True
                'txtLastNameVN.ReadOnly = True
                'txtEmpCODE.ReadOnly = False
        End Select

        ChangeToolbarState()
        Me.Send(CurrentState)
    End Sub

    'Reset mặc định Control
    Public Sub ResetControlValue()
        ' Candidate
        'txtEmpCODE.Text = ""
        'txtFirstNameVN.Text = ""
        'txtLastNameVN.Text = ""
        'hidID.Value = ""
        'cboGender.SelectedValue = 565
        'cboNative.SelectedValue = 1724
        'cboReligion.ClearSelection()

        'rntxtCMND.Value = Nothing
        'rdCMNDDate.SelectedDate = Nothing
        'cboCMNDPlace.ClearSelection()


        'rdBirthDate.SelectedDate = Nothing
        'cboNation.ClearSelection()
        'cboProvince.ClearSelection()

        'cboNationality.SelectedValue = 1558
        'cboNavNation.SelectedValue = 1558
        'cboNav_Province.ClearSelection()

        'txtPerAddress.Text = ""
        'cboPerProvince.ClearSelection()
        'cboPerDictrict.ClearSelection()
        'cboPerNation.ClearSelection()
        'txtContactAddress.Text = ""
        'cboContactProvince.SelectedIndex = 0
        'cboContractDictrict.SelectedIndex = 0
        'cboContactNation.SelectedValue = 1
        ''cboEducationLevel.ClearSelection()
        ''txtPerTaxCode.Text = ""
        ''txtMajor.Text = ""
        ''lblFileName.Text = ""

        'cboPerNation.SelectedValue = 1
        'cboPerProvince.SelectedIndex = 0
        'cboPerDictrict.SelectedIndex = 0

        '' Candidate other
        'txtSoDienThoaiCoDinh.Text = ""
        ''cboNhaMang.ClearSelection()
        'txtSoDienThoaiCaNhan.Text = ""
        ''txtDienThoaiNguoiLH.Text = ""
        'cv_txtEmailCaNhanCongTy.Text = ""
        'txtEmailCaNhan.Text = ""
        'txtSoViSa.Text = ""
        'rdNgayCapViSa.SelectedDate = Nothing
        'rdNgayHetHanVisa.SelectedDate = Nothing
        'txtNoiCapVisa.Text = ""
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
        'cboTinhTrangHN.ClearSelection()
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

#End Region

    Private Sub GetProvinceByNationID(ByVal cboPerPro As Telerik.Web.UI.RadComboBox, ByVal cboDitr As Telerik.Web.UI.RadComboBox, Optional ByVal sNationID As String = "", Optional ByVal sProvinceID As String = "")
        Try
            Dim lstProvinces As New List(Of ProvinceDTO)
            Dim comboBoxDataDTO As New Recruitment.RecruitmentBusiness.ComboBoxDataDTO

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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        rgData.Rebind()
        CreateDataFilter()
    End Sub

    Protected Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New CVPoolEmpDTO
        Dim rep As New RecruitmentRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If txtEmployeeCode.Text <> "" Then
                _filter.CANDIDATE_CODE = txtEmployeeCode.Text
            End If
            If txtEmployeeName.Text <> "" Then
                _filter.FULL_NAME_VN = txtEmployeeName.Text
            End If
            If cboGender.SelectedValue <> "" Then
                _filter.GENDER = cboGender.SelectedValue
            End If
            If cboTinhTrangHN.SelectedValue <> "" Then
                _filter.MARITAL_STATUS = cboTinhTrangHN.SelectedValue
            End If
            If cboNation.SelectedValue <> "" Then
                _filter.PER_COUNTRY = cboNation.SelectedValue
            End If
            If cboProvince.SelectedValue <> "" Then
                _filter.BIRTH_PLACE = cboProvince.SelectedValue
            End If
            _filter.BIRTH_FROM_DATE = rdBirthDateFrom.SelectedDate
            _filter.BIRTH_TO_DATE = rdBirthDateTo.SelectedDate
            If cboNationality.SelectedValue <> "" Then
                _filter.NATIONALITY = cboNationality.SelectedValue
            End If
            If cboNav_Province.SelectedValue <> "" Then
                _filter.PER_PROVINCE = cboNav_Province.SelectedValue
            End If
            If cboTrinhDoVanHoa.SelectedValue <> "" Then
                _filter.ACADEMY = cboTrinhDoVanHoa.SelectedValue
            End If
            If cboTrinhDoHocVan.SelectedValue <> "" Then
                _filter.LEARNING_LEVEL = cboTrinhDoHocVan.SelectedValue
            End If
            If cboTrinhDoChuyenMon.SelectedValue <> "" Then
                _filter.MAJOR = cboTrinhDoChuyenMon.SelectedValue
            End If
            If cboTruongHoc.SelectedValue <> "" Then
                _filter.GRADUATE_SCHOOL = cboTruongHoc.SelectedValue
            End If
            If cboBangCap.SelectedValue <> "" Then
                _filter.DEGREE = cboBangCap.SelectedValue
            End If
            If cboXepLoai.SelectedValue <> "" Then
                _filter.MARK_EDU = cboXepLoai.SelectedValue
            End If
            If cboTrinhDoTinHoc.SelectedValue <> "" Then
                _filter.COMPUTER_RANK = cboTrinhDoTinHoc.SelectedValue
            End If
            If cboTrinhDoNgoaiNgu.SelectedValue <> "" Then
                _filter.LANGUAGE = cboTrinhDoNgoaiNgu.SelectedValue
            End If
            _filter.CHIEU_CAO_TU = txtChieuCaoTu.Value
            _filter.CHIEU_CAO_DEN = txtChieuCaoDen.Value
            _filter.CAN_NANG_TU = txtCanNangTu.Value
            _filter.CAN_NANG_DEN = txtCanNangDen.Value
            If cboLoaiSucKhoe.SelectedValue <> "" Then
                _filter.LOAI_SUC_KHOE = cboLoaiSucKhoe.SelectedValue
            End If
            If cboEmpStatus.SelectedValue <> "" Then
                _filter.CANDIDATE_STATUS = cboEmpStatus.SelectedValue
            End If

            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim lstData As List(Of CVPoolEmpDTO)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCVPoolEmpRecord(_filter, 0, Integer.MaxValue, 0, Sorts).ToTable()
                Else
                    Return rep.GetCVPoolEmpRecord(_filter, 0, Integer.MaxValue, 0).ToTable()
                End If
                rgData.DataSource = Nothing
            Else
                Dim MaximumRows As Integer
                If Sorts IsNot Nothing Then
                    lstData = rep.GetCVPoolEmpRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstData = rep.GetCVPoolEmpRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = lstData

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetInformationLists()
        Dim repStore As New RecruitmentStoreProcedure
        Dim dsDB As New DataSet
        Dim dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc,
            dtNguonUV, dtOrg, dtIt, dtMarkEdu, dtLanguage, dtLanLevel, dtHeath, dtGender, dtVanhoa, dtSchool, dtTitle As New DataTable
        Try
            dsDB = repStore.GET_ALL_LIST(1)
            If dsDB.Tables.Count > 0 Then

                'Ton Giao
                If dsDB.Tables(0) IsNot Nothing AndAlso dsDB.Tables(0).Rows.Count > 0 Then
                    dtTonGiao = dsDB.Tables(0)
                End If

                'Moi Quan He
                If dsDB.Tables(1) IsNot Nothing AndAlso dsDB.Tables(1).Rows.Count > 0 Then
                    dtMoiQH = dsDB.Tables(1)
                End If

                'Quoc Gia
                If dsDB.Tables(2) IsNot Nothing AndAlso dsDB.Tables(2).Rows.Count > 0 Then
                    dtQuocGia = dsDB.Tables(2)
                End If

                'Tinh(Thanh pho)
                If dsDB.Tables(3) IsNot Nothing AndAlso dsDB.Tables(3).Rows.Count > 0 Then
                    dtTinh = dsDB.Tables(3)
                End If

                'Huyen(Quan)
                If dsDB.Tables(4) IsNot Nothing AndAlso dsDB.Tables(4).Rows.Count > 0 Then
                    dtHuyen = dsDB.Tables(4)
                End If

                'Xa(Phuong)
                If dsDB.Tables(5) IsNot Nothing AndAlso dsDB.Tables(5).Rows.Count > 0 Then
                    dtXa = dsDB.Tables(5)
                End If

                'Chuyen Nganh
                If dsDB.Tables(6) IsNot Nothing AndAlso dsDB.Tables(6).Rows.Count > 0 Then
                    dtChuyenNganh = dsDB.Tables(6)
                End If

                'Chuyen Mon
                If dsDB.Tables(7) IsNot Nothing AndAlso dsDB.Tables(7).Rows.Count > 0 Then
                    dtChuyenMon = dsDB.Tables(7)
                End If

                'Tinh Trang Hon Nhan
                If dsDB.Tables(8) IsNot Nothing AndAlso dsDB.Tables(8).Rows.Count > 0 Then
                    dtHonNhan = dsDB.Tables(8)
                End If

                'Dan Toc
                If dsDB.Tables(9) IsNot Nothing AndAlso dsDB.Tables(9).Rows.Count > 0 Then
                    dtDanToc = dsDB.Tables(9)
                End If

                'Nguon UV
                If dsDB.Tables(10) IsNot Nothing AndAlso dsDB.Tables(10).Rows.Count > 0 Then
                    dtNguonUV = dsDB.Tables(10)
                End If

                'Org
                If dsDB.Tables(11) IsNot Nothing AndAlso dsDB.Tables(11).Rows.Count > 0 Then
                    dtOrg = dsDB.Tables(11)
                End If

                'IT Level
                If dsDB.Tables(12) IsNot Nothing AndAlso dsDB.Tables(12).Rows.Count > 0 Then
                    dtIt = dsDB.Tables(12)
                End If

                'Loai chung chi
                If dsDB.Tables(13) IsNot Nothing AndAlso dsDB.Tables(13).Rows.Count > 0 Then
                    dtMarkEdu = dsDB.Tables(13)
                End If

                'Ngoai ngu
                If dsDB.Tables(14) IsNot Nothing AndAlso dsDB.Tables(14).Rows.Count > 0 Then
                    dtLanguage = dsDB.Tables(14)
                End If

                'Trinh do ngaoi ngu
                If dsDB.Tables(15) IsNot Nothing AndAlso dsDB.Tables(15).Rows.Count > 0 Then
                    dtLanLevel = dsDB.Tables(15)
                End If

                'Loai suc khoe
                If dsDB.Tables(16) IsNot Nothing AndAlso dsDB.Tables(16).Rows.Count > 0 Then
                    dtHeath = dsDB.Tables(16)
                End If

                'Gioi tinh
                If dsDB.Tables(17) IsNot Nothing AndAlso dsDB.Tables(17).Rows.Count > 0 Then
                    dtGender = dsDB.Tables(17)
                End If

                'Trinh do van hoa
                If dsDB.Tables(18) IsNot Nothing AndAlso dsDB.Tables(18).Rows.Count > 0 Then
                    dtVanhoa = dsDB.Tables(18)
                End If

                'Truong hoc
                If dsDB.Tables(19) IsNot Nothing AndAlso dsDB.Tables(19).Rows.Count > 0 Then
                    dtSchool = dsDB.Tables(19)
                End If

                'Chuc danh
                If dsDB.Tables(20) IsNot Nothing AndAlso dsDB.Tables(20).Rows.Count > 0 Then
                    dtTitle = dsDB.Tables(20)
                End If

                ExportTemplate("Recruitment\Import\Import_Ungvien_Template.xls", dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc, dtNguonUV,
                                 dtOrg, dtIt, dtMarkEdu, dtLanguage, dtLanLevel, dtHeath, dtGender, dtVanhoa, dtSchool, dtTitle, "Import_UngVien")
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dt1 As DataTable,
                                                    ByVal dt2 As DataTable,
                                                    ByVal dt3 As DataTable,
                                                    ByVal dt4 As DataTable,
                                                    ByVal dt5 As DataTable,
                                                    ByVal dt6 As DataTable,
                                                    ByVal dt7 As DataTable,
                                                    ByVal dt8 As DataTable,
                                                    ByVal dt9 As DataTable,
                                                    ByVal dt10 As DataTable,
                                                    ByVal dt11 As DataTable,
                                                    ByVal dt12 As DataTable,
                                                    ByVal dt13 As DataTable,
                                                    ByVal dt14 As DataTable,
                                                    ByVal dt15 As DataTable,
                                                    ByVal dt16 As DataTable,
                                                    ByVal dt17 As DataTable,
                                                    ByVal dt18 As DataTable,
                                                    ByVal dt19 As DataTable,
                                                    ByVal dt20 As DataTable,
                                                    ByVal dt21 As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)

            If dt1 IsNot Nothing Then
                dt1.TableName = "TableTonGiao"
                designer.SetDataSource(dt1)
            End If

            If dt2 IsNot Nothing Then
                dt2.TableName = "TableMoiQH"
                designer.SetDataSource(dt2)
            End If

            If dt3 IsNot Nothing Then
                dt3.TableName = "TableQuocGia"
                designer.SetDataSource(dt3)
            End If

            If dt4 IsNot Nothing Then
                dt4.TableName = "TableTinh"
                designer.SetDataSource(dt4)
            End If

            If dt5 IsNot Nothing Then
                dt5.TableName = "TableHuyen"
                designer.SetDataSource(dt5)
            End If

            If dt6 IsNot Nothing Then
                dt6.TableName = "TableXa"
                designer.SetDataSource(dt6)
            End If

            If dt7 IsNot Nothing Then
                dt7.TableName = "TableChuyenNganh"
                designer.SetDataSource(dt7)
            End If

            If dt8 IsNot Nothing Then
                dt8.TableName = "TableHocVan"
                designer.SetDataSource(dt8)
            End If

            If dt9 IsNot Nothing Then
                dt9.TableName = "TableHonNhan"
                designer.SetDataSource(dt9)
            End If

            If dt10 IsNot Nothing Then
                dt10.TableName = "TableDanToc"
                designer.SetDataSource(dt10)
            End If

            If dt11 IsNot Nothing Then
                dt11.TableName = "TableNguonUV"
                designer.SetDataSource(dt11)
            End If

            If dt12 IsNot Nothing Then
                dt12.TableName = "TableOrg"
                designer.SetDataSource(dt12)
            End If

            If dt13 IsNot Nothing Then
                dt13.TableName = "TableIT"
                designer.SetDataSource(dt13)
            End If

            If dt14 IsNot Nothing Then
                dt14.TableName = "TableMarkEdu"
                designer.SetDataSource(dt14)
            End If

            If dt15 IsNot Nothing Then
                dt15.TableName = "TableLanguage"
                designer.SetDataSource(dt15)
            End If

            If dt16 IsNot Nothing Then
                dt16.TableName = "TableLanLevel"
                designer.SetDataSource(dt16)
            End If

            If dt17 IsNot Nothing Then
                dt17.TableName = "TableHeath"
                designer.SetDataSource(dt17)
            End If

            If dt18 IsNot Nothing Then
                dt18.TableName = "TableGender"
                designer.SetDataSource(dt18)
            End If

            If dt19 IsNot Nothing Then
                dt19.TableName = "TableVanHoa"
                designer.SetDataSource(dt19)
            End If

            If dt20 IsNot Nothing Then
                dt20.TableName = "TableSchool"
                designer.SetDataSource(dt20)
            End If

            If dt21 IsNot Nothing Then
                dt21.TableName = "TableTitle"
                designer.SetDataSource(dt21)
            End If

            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub Import_Data()
        Try
            Dim rep As New RecruitmentRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_CANDIDATE(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_CV_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New RecruitmentRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "RC_SOURCE_REC_NAME"
            dtTemp.Columns(1).ColumnName = "ORG_NAME"
            dtTemp.Columns(2).ColumnName = "FIRST_NAME_VN"
            dtTemp.Columns(3).ColumnName = "LAST_NAME_VN"
            dtTemp.Columns(4).ColumnName = "FULLNAME_VN"
            dtTemp.Columns(5).ColumnName = "BIRTH_DATE"
            dtTemp.Columns(6).ColumnName = "GENDER_NAME"
            dtTemp.Columns(7).ColumnName = "MARITAL_STATUS_NAME"
            dtTemp.Columns(8).ColumnName = "CONTACT_MOBILE"
            dtTemp.Columns(9).ColumnName = "CONTACT_PHONE"
            dtTemp.Columns(10).ColumnName = "PER_EMAIL"
            dtTemp.Columns(11).ColumnName = "ID_NO"
            dtTemp.Columns(12).ColumnName = "ID_DATE"
            dtTemp.Columns(13).ColumnName = "ID_PLACE_NAME"
            dtTemp.Columns(14).ColumnName = "NATIONALITY_NAME"
            dtTemp.Columns(15).ColumnName = "NATIVE_NAME"
            dtTemp.Columns(16).ColumnName = "RELIGION_NAME"
            dtTemp.Columns(17).ColumnName = "CONTACT_PROVINCE_TEMP_NAME"
            dtTemp.Columns(18).ColumnName = "CONTACT_DISTRICT_TEMP_NAME"
            dtTemp.Columns(19).ColumnName = "CON_WARD_NAME"
            dtTemp.Columns(20).ColumnName = "CONTACT_ADDRESS_TEMP"
            dtTemp.Columns(21).ColumnName = "PER_PROVINCE_NAME"
            dtTemp.Columns(22).ColumnName = "PER_DISTRICT_NAME"
            dtTemp.Columns(23).ColumnName = "PER_WARD_NAME"
            dtTemp.Columns(24).ColumnName = "PER_ADDRESS"
            dtTemp.Columns(25).ColumnName = "IS_WORK_PERMIT"
            dtTemp.Columns(26).ColumnName = "WORK_PERMIT"
            dtTemp.Columns(27).ColumnName = "WORK_PERMIT_START"
            dtTemp.Columns(28).ColumnName = "WORK_PERMIT_END"
            dtTemp.Columns(29).ColumnName = "ACADEMY_NAME"
            dtTemp.Columns(30).ColumnName = "LEARNING_LEVEL_NAME"
            dtTemp.Columns(31).ColumnName = "SCHOOL_NAME"
            dtTemp.Columns(32).ColumnName = "MAJOR_NAME"
            dtTemp.Columns(33).ColumnName = "YEAR_GRADUATE"
            dtTemp.Columns(34).ColumnName = "RATE"
            dtTemp.Columns(35).ColumnName = "IT_LEVEL_NAME"
            dtTemp.Columns(36).ColumnName = "CERTIFICATE_NAME"
            dtTemp.Columns(37).ColumnName = "LANGUAGE_NAME"
            dtTemp.Columns(38).ColumnName = "ENGLISH_LEVEL_NAME"
            dtTemp.Columns(39).ColumnName = "ENGLISH_MARK"
            dtTemp.Columns(40).ColumnName = "DESIRE_POSITION_1_NAME"
            dtTemp.Columns(41).ColumnName = "DESIRE_POSITION_2_NAME"
            dtTemp.Columns(42).ColumnName = "PROBATIONARY_SALARY"
            dtTemp.Columns(43).ColumnName = "OFFICIAL_SALARY"
            dtTemp.Columns(44).ColumnName = "WORK_LOCATION"
            dtTemp.Columns(45).ColumnName = "DATE_START"
            dtTemp.Columns(46).ColumnName = "DESIRE_LEVEL"
            dtTemp.Columns(47).ColumnName = "SENIORITY_EXPERIENCE"
            dtTemp.Columns(48).ColumnName = "WORKED_HSV_HV"
            dtTemp.Columns(49).ColumnName = "OTHER_REQUEST"
            dtTemp.Columns(50).ColumnName = "URGENT_PER_NAME"
            dtTemp.Columns(51).ColumnName = "URGENT_PER_RELATION_NAME"
            dtTemp.Columns(52).ColumnName = "URGENT_PER_SDT"
            dtTemp.Columns(53).ColumnName = "URGENT_ADDRESS"
            dtTemp.Columns(54).ColumnName = "FINDER_NAME"
            dtTemp.Columns(55).ColumnName = "FINDER_SDT"
            dtTemp.Columns(56).ColumnName = "FINDER_ADDRESS"
            dtTemp.Columns(57).ColumnName = "CAN_NANG"
            dtTemp.Columns(58).ColumnName = "CHIEU_CAO"
            dtTemp.Columns(59).ColumnName = "NHOM_MAU"
            dtTemp.Columns(60).ColumnName = "TAI_MUI_HONG"
            dtTemp.Columns(61).ColumnName = "RANG_HAM_MAT"
            dtTemp.Columns(62).ColumnName = "TIM"
            dtTemp.Columns(63).ColumnName = "HUYET_AP"
            dtTemp.Columns(64).ColumnName = "PHOI_NGUC"
            dtTemp.Columns(65).ColumnName = "VIEM_GAN_B"
            dtTemp.Columns(66).ColumnName = "MAT_TRAI"
            dtTemp.Columns(67).ColumnName = "MAT_PHAI"
            dtTemp.Columns(68).ColumnName = "DA_HOA_LIEU"
            dtTemp.Columns(69).ColumnName = "LOAI_SUC_KHOE_NAME"
            dtTemp.Columns(70).ColumnName = "GHI_CHU_SUC_KHOE"
            dtTemp.Columns(71).ColumnName = "KNLV1"
            dtTemp.Columns(72).ColumnName = "FROMDATE_1"
            dtTemp.Columns(73).ColumnName = "TODATE_1"
            dtTemp.Columns(74).ColumnName = "ORG_NAME_1"
            dtTemp.Columns(75).ColumnName = "ORG_ADDRESS_1"
            dtTemp.Columns(76).ColumnName = "TITLE_NAME_1"
            dtTemp.Columns(77).ColumnName = "WORK_1"
            dtTemp.Columns(78).ColumnName = "REASON_LEAVE_1"
            dtTemp.Columns(79).ColumnName = "REMARK_1"
            dtTemp.Columns(80).ColumnName = "KNLV2"
            dtTemp.Columns(81).ColumnName = "FROMDATE_2"
            dtTemp.Columns(82).ColumnName = "TODATE_2"
            dtTemp.Columns(83).ColumnName = "ORG_NAME_2"
            dtTemp.Columns(84).ColumnName = "ORG_ADDRESS_2"
            dtTemp.Columns(85).ColumnName = "TITLE_NAME_2"
            dtTemp.Columns(86).ColumnName = "WORK_2"
            dtTemp.Columns(87).ColumnName = "REASON_LEAVE_2"
            dtTemp.Columns(88).ColumnName = "REMARK_2"
            dtTemp.Columns(89).ColumnName = "KNLV3"
            dtTemp.Columns(90).ColumnName = "FROMDATE_3"
            dtTemp.Columns(91).ColumnName = "TODATE_3"
            dtTemp.Columns(92).ColumnName = "ORG_NAME_3"
            dtTemp.Columns(93).ColumnName = "ORG_ADDRESS_3"
            dtTemp.Columns(94).ColumnName = "TITLE_NAME_3"
            dtTemp.Columns(95).ColumnName = "WORK_3"
            dtTemp.Columns(96).ColumnName = "REASON_LEAVE_3"
            dtTemp.Columns(97).ColumnName = "REMARK_3"
            dtTemp.Columns(98).ColumnName = "RC_SOURCE_REC_ID"
            dtTemp.Columns(99).ColumnName = "ORG_ID"
            dtTemp.Columns(100).ColumnName = "GENDER"
            dtTemp.Columns(101).ColumnName = "MARITAL_STATUS"
            dtTemp.Columns(102).ColumnName = "ID_PLACE"
            dtTemp.Columns(103).ColumnName = "NATIONALITY_ID"
            dtTemp.Columns(104).ColumnName = "NATIVE"
            dtTemp.Columns(105).ColumnName = "RELIGION"
            dtTemp.Columns(106).ColumnName = "CONTACT_PROVINCE_TEMP"
            dtTemp.Columns(107).ColumnName = "CONTACT_DISTRICT_TEMP"
            dtTemp.Columns(108).ColumnName = "CON_WARD"
            dtTemp.Columns(109).ColumnName = "PER_PROVINCE"
            dtTemp.Columns(110).ColumnName = "PER_DISTRICT"
            dtTemp.Columns(111).ColumnName = "PER_WARD"
            dtTemp.Columns(112).ColumnName = "ACADEMY"
            dtTemp.Columns(113).ColumnName = "LEARNING_LEVEL"
            dtTemp.Columns(114).ColumnName = "SCHOOL"
            dtTemp.Columns(115).ColumnName = "MAJOR"
            dtTemp.Columns(116).ColumnName = "IT_LEVEL"
            dtTemp.Columns(117).ColumnName = "CERTIFICATE_ID"
            dtTemp.Columns(118).ColumnName = "LANGUAGE_ID"
            dtTemp.Columns(119).ColumnName = "ENGLISH_LEVEL"
            dtTemp.Columns(120).ColumnName = "DESIRE_POSITION_1"
            dtTemp.Columns(121).ColumnName = "DESIRE_POSITION_2"
            dtTemp.Columns(122).ColumnName = "URGENT_PER_RELATION"
            dtTemp.Columns(123).ColumnName = "LOAI_SUC_KHOE"


            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(2).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim newRow As DataRow

            dtLogs = dtTemp.Clone
            dtLogs.TableName = "DATA"

            'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("FIRST_NAME_VN").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            Dim sError As String
            Dim rep1 As New CommonRepository
            Dim store As New RecruitmentRepository
            Dim lstEmp As New List(Of String)

            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

                _error = False
                newRow = dtLogs.NewRow
                'bắt buộc nhập
                sError = "Chưa nhập dữ liệu"
                ImportValidate.EmptyValue("RC_SOURCE_REC_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("ORG_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("FIRST_NAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("LAST_NAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("FULLNAME_VN", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("BIRTH_DATE", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("GENDER_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CONTACT_MOBILE", rows, newRow, _error, sError)

                ImportValidate.EmptyValue("CONTACT_PROVINCE_TEMP_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CONTACT_DISTRICT_TEMP_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CON_WARD_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("CONTACT_ADDRESS_TEMP", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("ACADEMY_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("LEARNING_LEVEL_NAME", rows, newRow, _error, sError)
                ImportValidate.EmptyValue("DESIRE_POSITION_1_NAME", rows, newRow, _error, sError)

                'CHECK DATE
                sError = "Ngày sai định dạng"
                If rows("BIRTH_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("BIRTH_DATE", rows, newRow, _error, sError)
                End If
                If rows("ID_DATE") Is Nothing Then
                    ImportValidate.IsValidDate("ID_DATE", rows, newRow, _error, sError)
                End If
                If rows("WORK_PERMIT_START") Is Nothing Then
                    ImportValidate.IsValidDate("WORK_PERMIT_START", rows, newRow, _error, sError)
                End If
                If rows("WORK_PERMIT_END") Is Nothing Then
                    ImportValidate.IsValidDate("WORK_PERMIT_END", rows, newRow, _error, sError)
                End If
                If rows("DATE_START") Is Nothing Then
                    ImportValidate.IsValidDate("DATE_START", rows, newRow, _error, sError)
                End If
                If rows("FROMDATE_1") Is Nothing Then
                    ImportValidate.IsValidDate("FROMDATE_1", rows, newRow, _error, sError)
                End If
                If rows("TODATE_1") Is Nothing Then
                    ImportValidate.IsValidDate("TODATE_1", rows, newRow, _error, sError)
                End If
                If rows("FROMDATE_2") Is Nothing Then
                    ImportValidate.IsValidDate("FROMDATE_2", rows, newRow, _error, sError)
                End If
                If rows("TODATE_2") Is Nothing Then
                    ImportValidate.IsValidDate("TODATE_2", rows, newRow, _error, sError)
                End If
                If rows("FROMDATE_3") Is Nothing Then
                    ImportValidate.IsValidDate("FROMDATE_3", rows, newRow, _error, sError)
                End If
                If rows("TODATE_3") Is Nothing Then
                    ImportValidate.IsValidDate("TODATE_3", rows, newRow, _error, sError)
                End If

                If _error Then
                    dtLogs.Rows.Add(newRow)
                    _error = False
                End If
            Next
            dtTemp.AcceptChanges()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class