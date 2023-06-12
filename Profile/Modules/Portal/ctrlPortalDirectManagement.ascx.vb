Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalDirectManagement
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property lstEmployee As List(Of EmployeeDTO)
        Get
            Return Session(Me.ID & "_lstEmployee")
        End Get
        Set(ByVal value As List(Of EmployeeDTO))
            Session(Me.ID & "_lstEmployee") = value
        End Set
    End Property

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
            rgSalary.SetFilter()
            rgSalary.ClientSettings.EnablePostBackOnRowClick = True
            rgAllow.SetFilter()
            rgContract.SetFilter()
            rgCommend.SetFilter()
            rgWorking.SetFilter()
            rgDiscipline.SetFilter()
            rgFamily.SetFilter()
            rgWorkingBefore.SetFilter()
            rgTrainingOutCompany.SetFilter()
            'rgMain.SetFilter()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        EnableControlAll(False, txtWorkStatus, txtEmpCODE,
                                      txtBankNo, txtBirthPlace,
                                      txtDaHoaLieu,
                                      txtFirstNameVN, txtGhiChuSK, txtGraduateSchool,
                                      txtHomePhone, txtHuyetAp, txtID_NO,
                                      txtIDPlace, txtLangMark,
                                      txtLastNameVN, txtMatPhai, txtMatTrai,
                                      txtMobilePhone, txtNavAddress, txtNhomMau,
                                       txtPassNo, txtPassPlace,
                                      txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                      txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                      txtVienGanB, txtVisa, txtVisaPlace,
                                      txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                      txtContactPerson, txtContactPersonPhone, txtChucVuDang, txtChucVuDoan,
                                      rdBirthDate, rdIDDate,
                                      rdNgayVaoDang, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                      rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                      txtCanNang, txtChieuCao,
                                      txtAcademy, txtBank, txtBankBranch, txtFamilyStatus,
                                      txtGender, txtLangLevel,
                                      txtLanguage, txtLearningLevel, txtLoaiSucKhoe,
                                      txtMajorRemark, txtMajor, txtNationlity, txtNative, txtNav_Province, txtPer_Province,
                                      txtReligion, txtStaffRank, txtTitle, txtTrainingForm,
                                      txtPer_District, txtPer_Ward, txtNav_District, txtNav_Ward,
                                      txtContractType, rdContractEffectDate, rdContractExpireDate,
                                      chkDangPhi, chkDoanPhi)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim EmployeeInfo As EmployeeDTO
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub cboEmployee_ItemsRequested(sender As Object, e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles _
        cboEmployee.ItemsRequested

        Using rep As New ProfileBusinessRepository
            Dim sText As String = e.Text
            Dim lstEmployeeData As List(Of EmployeeDTO)
            Dim _filter = New EmployeeDTO
            _filter.DIRECT_MANAGER = EmployeeID
            If lstEmployee Is Nothing Then
                lstEmployee = rep.GetListEmployeePortal(_filter)
            End If

            If sText <> "" Then
                Dim dtExist = (From p In lstEmployee
                               Where (p.EMPLOYEE_CODE & " - " & p.FULLNAME_VN).ToUpper = sText.ToUpper).ToList

                If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In lstEmployee
                                    Where (p.EMPLOYEE_CODE & " - " & p.FULLNAME_VN).ToUpper.Contains(sText.ToUpper)).ToList

                    If dtFilter.Count > 0 Then
                        lstEmployeeData = dtFilter
                    Else
                        lstEmployeeData = New List(Of EmployeeDTO)
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, lstEmployeeData.Count)
                    e.EndOfItems = endOffset = lstEmployeeData.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(lstEmployeeData(i).EMPLOYEE_CODE & " - " & lstEmployeeData(i).FULLNAME_VN, lstEmployeeData(i).ID)

                        sender.Items.Add(radItem)
                    Next
                Else

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = lstEmployee.Count
                    e.EndOfItems = True

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(lstEmployee(i).EMPLOYEE_CODE & " - " & lstEmployee(i).FULLNAME_VN, lstEmployee(i).ID)

                        sender.Items.Add(radItem)
                    Next
                End If
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, lstEmployee.Count)
                e.EndOfItems = endOffset = lstEmployee.Count

                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(lstEmployee(i).EMPLOYEE_CODE & " - " & lstEmployee(i).FULLNAME_VN, lstEmployee(i).ID)

                    sender.Items.Add(radItem)
                Next
            End If
        End Using
    End Sub

    Private Sub cboEmployee_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployee.SelectedIndexChanged
        Try
            If cboEmployee.SelectedValue = "" Then
                ClearControlValue(txtWorkStatus, txtEmpCODE,
                                  txtBankNo, txtBirthPlace,
                                  txtDaHoaLieu, txtOrgName, txtTitleGroup, txtDirectManager,
                                  txtFirstNameVN, txtGhiChuSK, txtGraduateSchool,
                                  txtHomePhone, txtHuyetAp, txtID_NO,
                                  txtIDPlace, txtLangMark,
                                  txtLastNameVN, txtMatPhai, txtMatTrai,
                                  txtMobilePhone, txtNavAddress, txtNhomMau,
                                  txtPassNo, txtPassPlace,
                                  txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                  txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                  txtVienGanB, txtVisa, txtVisaPlace,
                                  txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                  txtContactPerson, txtContactPersonPhone, txtChucVuDang, txtChucVuDoan,
                                  rdBirthDate, rdIDDate, rdJoinDateState, rdJoinDate,
                                  rdNgayVaoDang, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                  rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                  txtCanNang, txtChieuCao,
                                  txtAcademy, txtBank, txtBankBranch, txtFamilyStatus,
                                  txtGender, txtLangLevel,
                                  txtLanguage, txtLearningLevel, txtLoaiSucKhoe,
                                  txtMajorRemark, txtMajor, txtNationlity, txtNative, txtNav_Province, txtPer_Province,
                                  txtReligion, txtStaffRank, txtTitle, txtTrainingForm,
                                  txtPer_District, txtPer_Ward, txtNav_District, txtNav_Ward,
                                  txtContractType, rdContractEffectDate, rdContractExpireDate,
                                  chkDangPhi, chkDoanPhi)
                Exit Sub
            Else
                rpvEmpInfo.Selected = True
                rtIdEmpInfo.Selected = True
            End If
            ' Thông tin nhân viên
            FillEmployeeByID()
            ' Thông tin nhân thân
            rgFamily.Rebind()
            ' Thông tin công tác trước đây
            rgWorkingBefore.Rebind()
            ' Thông tin công tác hiện tại
            rgWorking.Rebind()
            ' Thông tin đào tạo ngoài công ty
            rgTrainingOutCompany.Rebind()
            ' Thông tin hợp đồng lao động
            rgContract.Rebind()
            ' Thông tin thay đổi lương - phụ cấp
            rgSalary.Rebind()
            ' Thông tin khen thưởng
            rgCommend.Rebind()
            ' Thông tin kỷ luật
            rgDiscipline.Rebind()
            ' Chức danh kiêm nhiệm
            'rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgFamily_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgFamily.DataSource = New List(Of FamilyDTO)
                Exit Sub
            End If

            Dim _filter = New FamilyDTO
            _filter.EMPLOYEE_ID = cboEmployee.SelectedValue

            SetValueObjectByRadGrid(rgFamily, _filter)
            Dim rep As New ProfileBusinessRepository

            rgFamily.DataSource = rep.GetEmployeeFamily(_filter)

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorkingBefore_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkingBefore.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgWorkingBefore.DataSource = New List(Of WorkingBeforeDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgWorkingBefore, New WorkingBeforeDTO)
            Using rep As New ProfileBusinessRepository
                rgWorkingBefore.DataSource = rep.GetWorkingBefore(cboEmployee.SelectedValue)
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorking_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgWorking.DataSource = New List(Of WorkingDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgWorking, New WorkingDTO)
            Using rep As New ProfileBusinessRepository
                rgWorking.DataSource = rep.GetWorkingProccess(cboEmployee.SelectedValue)
            End Using

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTrainingOutCompany_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTrainingOutCompany.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgTrainingOutCompany.DataSource = New List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgTrainingOutCompany, New HU_PRO_TRAIN_OUT_COMPANYDTO)
            Using rep As New ProfileBusinessRepository
                rgTrainingOutCompany.DataSource = rep.GetProcessTraining(New HU_PRO_TRAIN_OUT_COMPANYDTO With {
                                                                         .EMPLOYEE_ID = cboEmployee.SelectedValue})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgContract_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgContract.DataSource = New List(Of ContractDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgContract, New ContractDTO)
            Using rep As New ProfileBusinessRepository
                rgContract.DataSource = rep.GetContractProccess(cboEmployee.SelectedValue)
            End Using

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub rgSalary_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSalary.NeedDataSource
        Try
            If cboEmployee.SelectedValue = "" Then
                rgSalary.DataSource = New List(Of WorkingDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgSalary, New WorkingDTO)
            Using rep As New ProfileBusinessRepository
                rgSalary.DataSource = rep.GetSalaryProccess(cboEmployee.SelectedValue)
            End Using

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub rgMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
    '    Try
    '        Dim re As New ProfileRepository
    '        Dim _filter As New TitleConcurrentDTO

    '        If cboEmployee.SelectedValue = "" Then
    '            rgMain.DataSource = New List(Of TitleConcurrentDTO)
    '            Exit Sub
    '        End If

    '        _filter.EMPLOYEE_ID = cboEmployee.SelectedValue
    '        SetValueObjectByRadGrid(rgMain, _filter)

    '        Dim MaximumRows As Integer
    '        Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

    '        If Sorts IsNot Nothing Then
    '            Me.TitleConcurrents = re.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
    '        Else
    '            Me.TitleConcurrents = re.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
    '        End If

    '        rgMain.VirtualItemCount = MaximumRows
    '        rgMain.DataSource = Me.TitleConcurrents
    '    Catch ex As Exception
    '        Me.DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub rgSalary_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgSalary.SelectedIndexChanged
        rgAllow.Rebind()
    End Sub

    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Dim _filter = New WorkingAllowanceDTO
        Try
            If rgSalary.SelectedItems.Count = 0 Then
                rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                Exit Sub
            End If
            SetValueObjectByRadGrid(rgAllow, _filter)
            Dim item As GridDataItem = rgSalary.SelectedItems(0)
            Dim rep As New ProfileBusinessRepository
            _filter.HU_WORKING_ID = item.GetDataKeyValue("ID")
            rgAllow.DataSource = rep.GetAllowanceByWorkingID(_filter)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub rgCommend_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCommend.NeedDataSource
        Try

            If cboEmployee.SelectedValue = "" Then
                rgCommend.DataSource = New List(Of CommendDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgCommend, New CommendDTO)
            Using rep As New ProfileBusinessRepository
                rgCommend.DataSource = rep.GetCommendProccess(cboEmployee.SelectedValue)
            End Using

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub rgDiscipline_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDiscipline.NeedDataSource
        Try

            If cboEmployee.SelectedValue = "" Then
                rgDiscipline.DataSource = New List(Of DisciplineDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgDiscipline, New DisciplineDTO)
            Using rep As New ProfileBusinessRepository
                rgDiscipline.DataSource = rep.GetDisciplineProccess(cboEmployee.SelectedValue)
            End Using

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub FillEmployeeByID()
        Dim EmployeeInfo As EmployeeDTO
        Try
            Using rep As New ProfileBusinessRepository
                EmployeeInfo = rep.GetEmployeeByEmployeeID(cboEmployee.SelectedValue)
                If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.ID <> 0 Then

                    txtEmpCODE.Text = EmployeeInfo.EMPLOYEE_CODE
                    txtFirstNameVN.Text = EmployeeInfo.FIRST_NAME_VN
                    txtLastNameVN.Text = EmployeeInfo.LAST_NAME_VN
                    txtOrgName.Text = EmployeeInfo.ORG_NAME
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(EmployeeInfo.ORG_DESC)
                    txtTitle.Text = EmployeeInfo.TITLE_ID
                    txtTitle.Text = EmployeeInfo.TITLE_NAME_VN
                    txtTitleGroup.Text = EmployeeInfo.TITLE_GROUP_NAME
                    rdJoinDate.SelectedDate = EmployeeInfo.JOIN_DATE
                    rdJoinDateState.SelectedDate = EmployeeInfo.JOIN_DATE_STATE
                    txtStaffRank.Text = EmployeeInfo.STAFF_RANK_NAME
                    txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                    txtLevelManager.Text = EmployeeInfo.LEVEL_MANAGER_NAME
                    txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                    rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                    rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                    txtWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME

                    Dim empCV As EmployeeCVDTO
                    Dim empEdu As EmployeeEduDTO
                    Dim empHealth As EmployeeHealthDTO
                    rep.GetEmployeeAllByID(EmployeeInfo.ID, empCV, empEdu, empHealth)
                    If empCV IsNot Nothing Then
                        txtGender.Text = empCV.GENDER_NAME
                        rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                        txtBirthPlace.Text = empCV.BIRTH_PLACE
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
                        ' SĐT
                        txtHomePhone.Text = empCV.HOME_PHONE
                        txtMobilePhone.Text = empCV.MOBILE_PHONE
                        ' CMND
                        txtID_NO.Text = empCV.ID_NO
                        rdIDDate.SelectedDate = empCV.ID_DATE
                        txtIDPlace.Text = empCV.ID_PLACE
                        'Hộ chiếu
                        txtPassNo.Text = empCV.PASS_NO
                        rdPassDate.SelectedDate = empCV.PASS_DATE
                        rdPassExpireDate.SelectedDate = empCV.PASS_EXPIRE
                        txtPassPlace.Text = empCV.PASS_PLACE
                        'Visa
                        txtVisa.Text = empCV.VISA
                        rdVisaDate.SelectedDate = empCV.VISA_DATE
                        rdVisaExpireDate.SelectedDate = empCV.VISA_EXPIRE
                        txtVisaPlace.Text = empCV.VISA_PLACE
                        'Giấy phép lao động
                        txtWorkPermit.Text = empCV.WORK_PERMIT
                        rdWorkPermitDate.SelectedDate = empCV.WORK_PERMIT_DATE
                        rdWorPermitExpireDate.SelectedDate = empCV.WORK_PERMIT_EXPIRE
                        txtWorkPermitPlace.Text = empCV.WORK_PERMIT_PLACE
                        txtPitCode.Text = empCV.PIT_CODE
                        txtPerEmail.Text = empCV.PER_EMAIL
                        txtCareer.Text = empCV.CAREER
                        txtWorkEmail.Text = empCV.WORK_EMAIL
                        'Người liên hệ khi cần
                        txtContactPerson.Text = empCV.CONTACT_PER
                        'Điện thoại người liên hệ
                        txtContactPersonPhone.Text = empCV.CONTACT_PER_PHONE
                        chkDangPhi.Checked = False
                        If empCV.DANG_PHI IsNot Nothing Then
                            chkDangPhi.Checked = empCV.DANG_PHI
                        End If
                        chkDoanPhi.Checked = False
                        If empCV.DOAN_PHI IsNot Nothing Then
                            chkDoanPhi.Checked = empCV.DOAN_PHI
                        End If
                        txtBank.Text = empCV.BANK_NAME
                        txtBankBranch.Text = empCV.BANK_BRANCH_NAME
                        rdNgayVaoDang.SelectedDate = empCV.NGAY_VAO_DANG
                        rdNgayVaoDoan.SelectedDate = empCV.NGAY_VAO_DOAN
                        txtChucVuDang.Text = empCV.CHUC_VU_DANG
                        txtChucVuDoan.Text = empCV.CHUC_VU_DOAN
                        txtNoiVaoDang.Text = empCV.NOI_VAO_DANG
                        txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                        txtBankNo.Text = empCV.BANK_NO
                    End If
                    If empEdu IsNot Nothing Then
                        txtAcademy.Text = empEdu.ACADEMY_NAME
                        txtTrainingForm.Text = empEdu.TRAINING_FORM_NAME
                        txtLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                        txtLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                        txtLangMark.Text = empEdu.LANGUAGE_MARK
                        txtLanguage.Text = empEdu.LANGUAGE
                        txtMajor.Text = empEdu.MAJOR_NAME
                        txtMajorRemark.Text = empEdu.MAJOR_REMARK
                        txtGraduateSchool.Text = empEdu.GRADUATE_SCHOOL_NAME

                    End If

                    If empHealth IsNot Nothing Then
                        txtCanNang.Text = empHealth.CAN_NANG
                        txtChieuCao.Text = empHealth.CHIEU_CAO
                        txtDaHoaLieu.Text = empHealth.DA_HOA_LIEU
                        txtGhiChuSK.Text = empHealth.GHI_CHU_SUC_KHOE
                        txtHuyetAp.Text = empHealth.HUYET_AP
                        txtMatPhai.Text = empHealth.MAT_PHAI
                        txtMatTrai.Text = empHealth.MAT_TRAI
                        txtNhomMau.Text = empHealth.NHOM_MAU
                        txtPhoiNguc.Text = empHealth.PHOI_NGUC
                        txtRangHamMat.Text = empHealth.RANG_HAM_MAT
                        txtTaiMuiHong.Text = empHealth.TAI_MUI_HONG
                        txtTim.Text = empHealth.TIM
                        txtVienGanB.Text = empHealth.VIEM_GAN_B
                        txtLoaiSucKhoe.Text = empHealth.LOAI_SUC_KHOE
                    End If

                End If
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class