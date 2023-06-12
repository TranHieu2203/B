Imports System.IO
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPortalEmpProfile
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public WithEvents ViewImage As ViewBase

    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "HistaffPortal\Modules\Profile\" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property TitleConcurrents As List(Of TitleConcurrentDTO)
        Get
            Return ViewState(Me.ID & "_TitleConcurrents")
        End Get
        Set(ByVal value As List(Of TitleConcurrentDTO))
            ViewState(Me.ID & "_TitleConcurrents") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            If CType(CommonConfig.dicConfig("HIDE_MANAGERHEATHINS"), Boolean) Then
                hid_ins0.Visible = False
                hid_ins1.Visible = False
                hid_ins2.Visible = False
                hid_ins3.Visible = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        If Not CType(CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE"), Boolean) Then
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
        Else
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
        End If
        EnableControlAll(False, txtEmpCODE,
                                      txtBankNo, txtEmpStatus, txtWorkStatus,
                                      txtFirstNameVN, txtITimeID, txtObjectLabor,
                                      txtHomePhone, txtObjectIns,
                                      cboIDPlace, txtManager,
                                      txtLastNameVN, txtID_NO,
                                      txtMobilePhone, txtNavAddress,
                                       txtPassNo, txtPassPlace,
                                      txtPerAddress, txtPerEmail,
                                      txtPitCode, txtInsArea,
                                      txtBasic, txtLearningLevel, txtGraduateSchool, txtNamTN, txtCertificate, txtAppDung, txtLanguage,
                                      txtLangLevel, txtLangMark,
                                      txtWorkEmail, txtContactPerson,
                                      rdBirthDate, rdIDDate, rdExpireIDNO,
                                      rdPassDate, rdPassExpireDate,
                                      txtAcademy, txtBank, txtBankBranch, txtFamilyStatus,
                                      txtGender, txtMajor, txtNationlity, txtNative, txtNav_Province, txtPer_Province,
                                      txtReligion, txtTitle, txtEmpStatus,
                                      txtPer_District, txtPer_Ward, txtNav_District, txtNav_Ward,
                                      txtContractType, rdContractEffectDate, rdContractExpireDate,
                                      txtNoBHXH, txtLearningLevel,
                                      txtProvinceBorn, txtDistrictBorn, txtWardBorn,
                                      txtPROVINCEEMP_BRITH, txtDISTRICTEMP_BRITH, txtWARDEMP_BRITH,
                                      rdProbationDate, rdOfficialDate, rdQuitDate, txtDomicile,
                                       ckDANG, ckCA, ckBanTT_ND,
                                      ckCONG_DOAN, rtCV_BANTT, rdNgay_TG_BanTT, ckNU_CONG, rtCV_Ban_Nu_Cong,
                                      rdNgay_TG_Ban_Nu_Cong, rdNgay_Nhap_Ngu_CA, rdNgay_Xuat_Ngu_CA, rtDV_Xuat_Ngu_CA,
                                      ckQD, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD, ckThuong_Binh,
                                      txtHang_Thuong_Binh, txtGD_Chinh_Sach, rdNGAY_VAO_DANG_DB, rtCHUC_VU_DANG, rdNGAY_VAO_DANG,
                                      rdNGAY_VAO_DOAN, ckDOAN_PHI, txtID_REMARK, txtNOI_VAO_DANG,
                                      ckCHUHO, txtNoHouseHolds, txtCodeHouseHolds, txtNoHouseHolds, txtCodeHouseHolds,
                                      txtVillage, txtAddressPerContract, txtRelationNLH, txtPerHomePhone, txtPerMobilePhone,
                                      rdDayPitcode, txtPlacePitcode, txtPerson_Inheritance, txtInsArea, txtBloodGroup, txtHeight, txtWeight,
                                      txtVisaNo, rdVisaEffectDate, rdVisaExpireDate, txtVisaPlace, txtWorkBookNo, rdWBEffectDate, rdWBExpireDate, txtWBPlace,
                                      txtGPLD, rdWorkFromDate, rdWorkToDate)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim EmployeeInfo As EmployeeDTO
        Try
            If Not IsPostBack Then
                CommonConfig.ModuleID = CommonBusiness.SystemConfigModuleID.All
                If HttpContext.Current.Session("ConfigDictionaryCache4") IsNot Nothing Then
                    Session.Remove("ConfigDictionaryCache4")
                End If
                Dim orgTree As OrganizationTreeDTO
                Using rep As New ProfileBusinessRepository
                    EmployeeInfo = rep.GetEmployeeByEmployeeID(EmployeeID)
                    ViewImage.SetProperty("EmployeeInfo", EmployeeInfo)
                    Using repRepo As New ProfileRepository
                        orgTree = repRepo.GetTreeOrgByID(EmployeeInfo.ORG_ID)
                    End Using
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.ID <> 0 Then

                        txtEmpCODE.Text = EmployeeInfo.EMPLOYEE_CODE
                        txtEmployeeCodeOld.Text = EmployeeInfo.EMPLOYEE_CODE_OLD
                        txtFirstNameVN.Text = EmployeeInfo.FIRST_NAME_VN
                        txtLastNameVN.Text = EmployeeInfo.LAST_NAME_VN
                        txtOrgName.Text = EmployeeInfo.ORG_NAME
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(EmployeeInfo.ORG_DESC)
                        txtTitle.Text = EmployeeInfo.TITLE_ID
                        txtTitle.Text = EmployeeInfo.TITLE_NAME_VN
                        rdQuitDate.SelectedDate = EmployeeInfo.TER_EFFECT_DATE

                        txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                        txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                        rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                        rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                        txtNoBHXH.Text = EmployeeInfo.BOOKNO
                        txtEmpStatus.Text = EmployeeInfo.EMP_STATUS_NAME
                        rdProbationDate.SelectedDate = EmployeeInfo.JOIN_DATE
                        rdOfficialDate.SelectedDate = EmployeeInfo.JOIN_DATE_STATE

                        ' Tình trang nhân viên
                        If EmployeeInfo.EMP_STATUS_NAME IsNot Nothing Then
                            txtEmpStatus.Text = EmployeeInfo.EMP_STATUS_NAME
                        End If

                        If EmployeeInfo.WORK_STATUS IsNot Nothing Then
                            txtWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME
                        End If

                        txtITimeID.Text = EmployeeInfo.ITIME_ID
                        txtManager.Text = EmployeeInfo.DIRECT_MANAGER_TITLE_NAME

                        If EmployeeInfo.OBJECT_LABOR IsNot Nothing Then
                            txtObjectLabor.Text = EmployeeInfo.OBJECT_LABOR_NAME
                        End If

                        Dim empCV As EmployeeCVDTO
                        Dim empEdu As EmployeeEduDTO
                        Dim empHealth As EmployeeHealthDTO
                        rep.GetEmployeeAllByID(EmployeeInfo.ID, empCV, empEdu, empHealth)
                        If empHealth IsNot Nothing Then
                            txtBloodGroup.Text = empHealth.NHOM_MAU
                            txtHeight.Text = empHealth.CHIEU_CAO
                            txtWeight.Text = empHealth.CAN_NANG
                        End If
                        If empCV IsNot Nothing Then

                            Dim dtPlace
                            Dim dtLanguageleve As DataTable
                            Using rep1 As New ProfileRepository
                                dtPlace = rep1.GetProvinceList(True)
                                dtLanguageleve = rep1.GetOtherList("RC_LANGUAGE_LEVEL", True)
                                FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
                                'FillRadCombobox(cboLangLevel, dtLanguageleve, "NAME", "ID")
                                'FillRadCombobox(cboBIRTH_PLACE, dtPlace, "NAME", "ID")
                            End Using

                            txtGender.Text = empCV.GENDER_NAME
                            rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                            'cboBIRTH_PLACE.SelectedValue = empCV.BIRTH_PLACE
                            txtFamilyStatus.Text = empCV.MARITAL_STATUS_NAME
                            txtReligion.Text = empCV.RELIGION_NAME
                            txtNative.Text = empCV.NATIVE_NAME
                            txtNationlity.Text = empCV.NATIONALITY_NAME
                            txtNavAddress.Text = empCV.NAV_ADDRESS
                            txtNav_Province.Text = empCV.NAV_PROVINCE_NAME
                            txtNav_District.Text = empCV.NAV_DISTRICT_NAME
                            txtNav_Ward.Text = empCV.NAV_WARD_NAME
                            txtPerAddress.Text = empCV.PER_ADDRESS
                            txtPer_Province.Text = empCV.PER_PROVINCE_NAME
                            txtPer_District.Text = empCV.PER_DISTRICT_NAME
                            txtPer_Ward.Text = empCV.PER_WARD_NAME
                            txtVillage.Text = empCV.VILLAGE
                            txtProvinceBorn.Text = empCV.BIRTH_PLACENAME
                            txtDistrictBorn.Text = empCV.DISTRICTEMP_NAME
                            txtWardBorn.Text = empCV.WARDEMP_NAME
                            txtInsArea.Text = empCV.INS_REGION_NAME
                            txtDomicile.Text = empCV.PROVINCENQ_NAME
                            txtID_REMARK.Text = empCV.ID_REMARK
                            txtNOI_VAO_DANG.Text = empCV.NOI_VAO_DANG
                            txtInsArea.Text = empCV.HEALTH_AREA_INS_NAME
                            ' Đối tượng đóng bảo hiểm
                            If empCV.OBJECT_INS IsNot Nothing Then
                                txtObjectIns.Text = empCV.OBJECT_INS_NAME
                            End If

                            If IsNumeric(empCV.PROVINCEEMP_BRITH) Then
                                txtPROVINCEEMP_BRITH.Text = empCV.PROVINCEEMP_BRITH_NAME
                            End If

                            If IsNumeric(empCV.DISTRICTEMP_BRITH) Then
                                txtDISTRICTEMP_BRITH.Text = empCV.DISTRICTEMP_BRITH_NAME
                            End If

                            If IsNumeric(empCV.WARDEMP_BRITH) Then
                                txtWARDEMP_BRITH.Text = empCV.WARDEMP_BRITH_NAME
                            End If

                            ' SĐT
                            txtHomePhone.Text = empCV.HOME_PHONE
                            txtMobilePhone.Text = empCV.MOBILE_PHONE
                            ' CMND
                            txtID_NO.Text = empCV.ID_NO
                            rdIDDate.SelectedDate = empCV.ID_DATE
                            rdExpireIDNO.SelectedDate = empCV.EXPIRE_DATE_IDNO
                            If empCV.ID_PLACE IsNot Nothing Then
                                cboIDPlace.SelectedValue = empCV.ID_PLACE
                            End If

                            If IsNumeric(empCV.IS_CHUHO) Then
                                ckCHUHO.Checked = CType(empCV.IS_CHUHO, Boolean)
                            End If
                            txtNoHouseHolds.Text = empCV.NO_HOUSEHOLDS
                            txtCodeHouseHolds.Text = empCV.CODE_HOUSEHOLDS

                            'Hộ chiếu
                            txtPassNo.Text = empCV.PASS_NO
                            rdPassDate.SelectedDate = empCV.PASS_DATE
                            rdPassExpireDate.SelectedDate = empCV.PASS_EXPIRE
                            txtPassPlace.Text = empCV.PASS_PLACE
                            'Visa
                            txtVisaNo.Text = empCV.VISA
                            If IsDate(empCV.VISA_DATE) Then
                                rdVisaEffectDate.SelectedDate = empCV.VISA_DATE
                            End If
                            If IsDate(empCV.VISA_EXPIRE) Then
                                rdVisaExpireDate.SelectedDate = empCV.VISA_EXPIRE
                            End If
                            txtVisaPlace.Text = empCV.VISA_PLACE
                            'Số sổ lao động
                            txtWorkBookNo.Text = empCV.BOOK_NO
                            If IsDate(empCV.BOOK_DATE) Then
                                rdWBEffectDate.SelectedDate = empCV.BOOK_DATE
                            End If
                            If IsDate(empCV.BOOK_EXPIRE) Then
                                rdWBExpireDate.SelectedDate = empCV.BOOK_EXPIRE
                            End If
                            txtWBPlace.Text = empCV.SSLD_PLACE_NAME
                            'Giấy phép lao động       
                            txtGPLD.Text = empCV.WORK_PERMIT
                            If IsDate(empCV.WORK_PERMIT_DATE) Then
                                rdWorkFromDate.SelectedDate = empCV.WORK_PERMIT_DATE
                            End If
                            If IsDate(empCV.WORK_PERMIT_EXPIRE) Then
                                rdWorkToDate.SelectedDate = empCV.WORK_PERMIT_EXPIRE
                            End If
                            'Thuế
                            txtPitCode.Text = empCV.PIT_CODE
                            txtPerEmail.Text = empCV.PER_EMAIL
                            rdDayPitcode.SelectedDate = empCV.PIT_CODE_DATE
                            txtPlacePitcode.Text = empCV.PIT_CODE_PLACE
                            txtPerson_Inheritance.Text = empCV.PERSON_INHERITANCE

                            txtWorkEmail.Text = empCV.WORK_EMAIL
                            'Người liên hệ khi cần
                            txtContactPerson.Text = empCV.CONTACT_PER
                            'Điện thoại người liên hệ
                            If empCV.RELATION_PER_CTR IsNot Nothing Then
                                txtRelationNLH.Text = empCV.RELATION_PER_CTR_NAME
                            End If
                            txtAddressPerContract.Text = empCV.ADDRESS_PER_CTR
                            txtPerHomePhone.Text = empCV.CONTACT_PER_PHONE
                            txtPerMobilePhone.Text = empCV.CONTACT_PER_MBPHONE

                            txtBank.Text = empCV.BANK_NAME
                            txtBankBranch.Text = empCV.BANK_BRANCH_NAME
                            txtBankNo.Text = empCV.BANK_NO
                            txtHang_Thuong_Binh.Text = empCV.HANG_THUONG_BINH_NAME
                            txtGD_Chinh_Sach.Text = empCV.GD_CHINH_SACH_NAME
                            '=========================================================
                            If IsNumeric(empCV.DANG) Then
                                ckDANG.Checked = CType(empCV.DANG, Boolean)
                            End If
                            If IsNumeric(empCV.CA) Then
                                ckCA.Checked = CType(empCV.CA, Boolean)
                            End If
                            If IsNumeric(empCV.BANTT) Then
                                ckBanTT_ND.Checked = CType(empCV.BANTT, Boolean)
                            End If
                            If IsNumeric(empCV.CONG_DOAN) Then
                                ckCONG_DOAN.Checked = CType(empCV.CONG_DOAN, Boolean)
                            End If
                            txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                            rtCV_BANTT.Text = empCV.CV_BANTT
                            If IsDate(empCV.NGAY_TG_BANTT) Then
                                rdNgay_TG_BanTT.SelectedDate = empCV.NGAY_TG_BANTT
                            End If
                            If IsNumeric(empCV.NU_CONG) Then
                                ckNU_CONG.Checked = CType(empCV.NU_CONG, Boolean)
                            End If
                            rtCV_Ban_Nu_Cong.Text = empCV.CV_BAN_NU_CONG
                            If IsDate(empCV.NGAY_TG_BAN_NU_CONG) Then
                                rdNgay_TG_Ban_Nu_Cong.SelectedDate = empCV.NGAY_TG_BAN_NU_CONG
                            End If
                            If IsDate(empCV.NGAY_NHAP_NGU_CA) Then
                                rdNgay_Nhap_Ngu_CA.SelectedDate = empCV.NGAY_NHAP_NGU_CA
                            End If
                            If IsDate(empCV.NGAY_XUAT_NGU_CA) Then
                                rdNgay_Xuat_Ngu_CA.SelectedDate = empCV.NGAY_XUAT_NGU_CA
                            End If
                            rtDV_Xuat_Ngu_CA.Text = empCV.DV_XUAT_NGU_CA
                            If IsNumeric(empCV.QD) Then
                                ckQD.Checked = CType(empCV.QD, Boolean)
                            End If
                            If IsDate(empCV.NGAY_NHAP_NGU_QD) Then
                                rdNgay_Nhap_Ngu_QD.SelectedDate = empCV.NGAY_NHAP_NGU_QD
                            End If
                            If IsDate(empCV.NGAY_XUAT_NGU_QD) Then
                                rdNgay_Xuat_Ngu_QD.SelectedDate = empCV.NGAY_XUAT_NGU_QD
                            End If
                            rtDV_Xuat_Ngu_QD.Text = empCV.DV_XUAT_NGU_QD
                            If IsNumeric(empCV.THUONG_BINH) Then
                                ckThuong_Binh.Checked = CType(empCV.THUONG_BINH, Boolean)
                            End If
                            If IsDate(empCV.NGAY_VAO_DANG_DB) Then
                                rdNGAY_VAO_DANG_DB.SelectedDate = empCV.NGAY_VAO_DANG_DB
                            End If
                            rtCHUC_VU_DANG.Text = empCV.CHUC_VU_DANG
                            If IsDate(empCV.NGAY_VAO_DANG) Then
                                rdNGAY_VAO_DANG.SelectedDate = empCV.NGAY_VAO_DANG
                            End If
                            'rtCHUC_VU_DOAN.Text = empCV.CHUC_VU_DOAN
                            If IsNumeric(empCV.DOAN_PHI) Then
                                ckDOAN_PHI.Checked = CType(empCV.DOAN_PHI, Boolean)
                            End If

                            If IsDate(empCV.NGAY_VAO_DOAN) Then
                                rdNGAY_VAO_DOAN.SelectedDate = empCV.NGAY_VAO_DOAN
                            End If
                            '===============================================
                        End If
                        If empEdu IsNot Nothing Then
                            Dim dtLanguage As DataTable
                            Dim dtData As DataTable
                            Using repEdu As New ProfileRepository
                                dtLanguage = repEdu.GetOtherList("RC_LANGUAGE_LEVEL", True)
                                'FillRadCombobox(cboLanguage2, dtLanguage, "NAME", "ID")
                                dtData = repEdu.GetOtherList("QLNN")
                                'FillRadCombobox(cboQLNN, dtData, "NAME", "ID")
                                dtData = repEdu.GetOtherList("LLCT")
                                'FillRadCombobox(cboLLCT, dtData, "NAME", "ID")
                                dtData = repEdu.GetOtherList("RC_COMPUTER_LEVEL")
                            End Using

                            If empEdu.ACADEMY IsNot Nothing Then
                                txtAcademy.Text = empEdu.ACADEMY_NAME
                            End If

                            If empEdu.LEARNING_LEVEL IsNot Nothing Then
                                txtLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                            End If
                            If empEdu.MAJOR IsNot Nothing Then
                                txtMajor.Text = empEdu.MAJOR_NAME
                            End If
                            If empEdu.GRADUATE_SCHOOL_ID IsNot Nothing Then
                                txtGraduateSchool.Text = empEdu.GRADUATE_SCHOOL_NAME
                            End If
                            If empEdu.COMPUTER_RANK IsNot Nothing Then
                                txtBasic.Text = empEdu.COMPUTER_RANK_NAME
                            End If
                            If empEdu.COMPUTER_MARK IsNot Nothing Then
                                txtCertificate.Text = empEdu.COMPUTER_MARK_NAME
                            End If
                            txtAppDung.Text = empEdu.COMPUTER_CERTIFICATE

                            If empEdu.LANGUAGE IsNot Nothing Then
                                txtLanguage.Text = empEdu.LANGUAGE_NAME
                            End If
                            If empEdu.LANGUAGE_LEVEL IsNot Nothing Then
                                txtLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                            End If
                            txtLangMark.Text = empEdu.LANGUAGE_MARK

                            If empEdu.GRADUATION_YEAR IsNot Nothing Then
                                txtNamTN.Text = empEdu.GRADUATION_YEAR
                            End If
                            If IsNumeric(empEdu.TDTH) Then
                                'cboTDTH.SelectedValue = empEdu.TDTH
                            End If
                            'txtITMark.Text = empEdu.DIEM_XLTH
                        End If
                    End If
                    Dim dt_ins As DataTable
                    dt_ins = rep.GET_INS_HEALTH_BY_EMPID(EmployeeInfo.ID)
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
                End Using

            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        InitControl()
        If ViewImage Is Nothing Then ViewImage = Me.Register("ctrlImageUpload", "Profile", "ctrlImageUpload")
        ViewImage.SetProperty("isPortal", True)
        ViewPlaceHoderImage.Controls.Add(ViewImage)
        ''ViewItem.SetProperty("ImageFile", ViewImage.GetProperty("ImageFile"))
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Edit)
            ', ToolbarItem.Print
            'CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("In lý lịch trích ngang")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    'Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
    '    Try
    '        CreateDataFilter()

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_PRINT
                    Print_CV()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New TitleConcurrentDTO
        Try
            _filter.EMPLOYEE_ID = EmployeeID
            'SetValueObjectByRadGrid(rgMain, _filter)

            'Dim MaximumRows As Integer
            'Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()


            'If Sorts IsNot Nothing Then
            '    Me.TitleConcurrents = rep.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            'Else
            '    Me.TitleConcurrents = rep.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            'End If

            'rgMain.VirtualItemCount = MaximumRows
            'rgMain.DataSource = Me.TitleConcurrents
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Xử lý action in cv
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Print_CV()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dsData As DataSet
        Dim rp As New ProfileStoreProcedure
        Dim repP As New ProfileBusinessRepository
        Dim reportName As String = String.Empty
        Dim reportNameOut As String = "String.Empty"
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Try

            dsData = rp.PRINT_CV(EmployeeID)

            If dsData Is Nothing Then
                ShowMessage("Không có dữ liệu in báo cáo", NotifyType.Warning)
                Exit Sub
            End If

            dsData.Tables(0).Rows(0)("IMAGE") = repP.GetEmployeeImage_PrintCV(EmployeeID)
            'If Not File.Exists(Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage\" + dsData.Tables(0).Rows(0)("IMAGE")) Then
            '    dsData.Tables(0).Rows(0)("IMAGE") = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\UploadFile\" + "NoImage.jpg"
            'Else
            '    Dim Image = dsData.Tables(0).Rows(0)("IMAGE")
            '    Dim target As String = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage\"
            '    dsData.Tables(0).Rows(0)("IMAGE") = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage\" + dsData.Tables(0).Rows(0)("IMAGE")
            'End If

            dsData.Tables(0).TableName = "DT"
            dsData.Tables(1).TableName = "DT1"
            dsData.Tables(2).TableName = "DT2"
            dsData.Tables(3).TableName = "DT3"
            dsData.Tables(4).TableName = "DT4"
            dsData.Tables(5).TableName = "DT5"
            dsData.Tables(6).TableName = "DT6"
            dsData.Tables(7).TableName = "DT7"
            reportName = "Employee\CV_Template.doc"
            reportNameOut = "CV.doc"
            If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                                  reportNameOut,
                                  dsData,
                                  Response)
            Else
                ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub DeleteDirectory(ByVal path As String)
        If Directory.Exists(path) Then
            For Each file As String In Directory.GetFiles(path)
                Try
                    System.IO.File.Delete(file)
                Catch ex As Exception
                    Continue For
                End Try
            Next
        End If

    End Sub
#End Region

End Class