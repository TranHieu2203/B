Imports Common
Imports Common.Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalEmpProfile_Edit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CommonConfig.ModuleID = CommonBusiness.SystemConfigModuleID.iPortal
                If HttpContext.Current.Session("ConfigDictionaryCache4") IsNot Nothing Then
                    Session.Remove("ConfigDictionaryCache4")
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                Dim empCV As EmployeeEditDTO
                Using rep As New ProfileBusinessRepository
                    empCV = rep.GetEmployeeEditByID(New EmployeeEditDTO With {.EMPLOYEE_ID = EmployeeID})
                End Using
                If empCV IsNot Nothing Then
                    hidID.Value = empCV.ID
                    If empCV.MARITAL_STATUS IsNot Nothing Then
                        cboFamilyStatus.SelectedValue = empCV.MARITAL_STATUS
                        cboFamilyStatus.Text = empCV.MARITAL_STATUS_NAME
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
                    txtNoHouseHolds.Text = empCV.NO_HOUSEHOLDS
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
                    txtCMNDNoteChange.Text = empCV.CMND_NOTE_CHANGE
                    If IsNumeric(empCV.ACADEMY) Then
                        cboAcademy.SelectedValue = empCV.ACADEMY
                    End If
                    If IsNumeric(empCV.LEARNING_LEVEL) Then
                        cboLearningLevel.SelectedValue = empCV.LEARNING_LEVEL
                    End If
                    If IsNumeric(empCV.MAJOR) Then
                        cboMajor.SelectedValue = empCV.MAJOR
                    End If
                    If IsNumeric(empCV.GRADUATE_SCHOOL_ID) Then
                        cboGraduateSchool.SelectedValue = empCV.GRADUATE_SCHOOL_ID
                    End If
                    If IsNumeric(empCV.GRADUATION_YEAR) Then
                        rnYearGraduate.Value = empCV.GRADUATION_YEAR
                    End If
                    If IsNumeric(empCV.COMPUTER_RANK) Then
                        cboComputerRank.SelectedValue = empCV.COMPUTER_RANK
                    End If
                    If IsNumeric(empCV.COMPUTER_MARK) Then
                        cboComputerMark.SelectedValue = empCV.COMPUTER_MARK
                    End If
                    If IsNumeric(empCV.LANGUAGE) Then
                        cboLanguage.SelectedValue = empCV.LANGUAGE
                    End If
                    If IsNumeric(empCV.LANGUAGE_LEVEL) Then
                        cboLanguageLevel.SelectedValue = empCV.LANGUAGE_LEVEL
                    End If
                    txtITBasic.Text = empCV.COMPUTER_CERTIFICATE
                    txtLanguageMark.Text = empCV.LANGUAGE_MARK
                    ' CMND
                    txtID_NO.Text = empCV.ID_NO
                    rdIDDate.SelectedDate = empCV.ID_DATE
                    Dim dtPlace
                    Using rep As New ProfileRepository
                        dtPlace = rep.GetProvinceList(True)
                        FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
                    End Using
                    cboIDPlace.SelectedValue = empCV.ID_PLACE

                    rdIDDateEnd.SelectedDate = empCV.EXPIRE_DATE_IDNO
                    txtContactPerson.Text = empCV.CONTACT_PER
                    Using rep As New ProfileRepository
                        If ComboBoxDataDTO Is Nothing Then
                            ComboBoxDataDTO = New ComboBoxDataDTO
                            ComboBoxDataDTO.GET_RELATION = True
                            rep.GetComboList(ComboBoxDataDTO)
                        End If
                        If ComboBoxDataDTO IsNot Nothing Then
                            FillDropDownList(cboRelationNLH, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationNLH.SelectedValue)
                        End If
                    End Using
                    If empCV.RELATION_PER_CTR IsNot Nothing Then
                        cboRelationNLH.SelectedValue = empCV.RELATION_PER_CTR
                        cboRelationNLH.Text = empCV.RELATION_PER_CTR_NAME
                    End If

                    txtPerMobilePhone.Text = empCV.CONTACT_PER_MBPHONE
                    txtPerThonAp.Text = empCV.VILLAGE
                    txtHomePhone.Text = empCV.HOME_PHONE
                    txtMobilePhone.Text = empCV.MOBILE_PHONE
                    txtWorkEmail.Text = empCV.WORK_EMAIL
                    txtPerEmail.Text = empCV.PER_EMAIL
                    txtFirstNameVN.Text = empCV.PERSON_INHERITANCE
                    txtBankNo.Text = empCV.BANK_NO

                    If empCV.BANK_ID IsNot Nothing Then
                        cboBank.SelectedValue = empCV.BANK_ID
                        cboBank.Text = empCV.BANK_NAME
                    End If
                    If empCV.BANK_BRANCH_ID IsNot Nothing Then
                        cboBankBranch.SelectedValue = empCV.BANK_BRANCH_ID
                        cboBankBranch.Text = empCV.BANK_BRANCH_NAME
                    End If

                    hidStatus.Value = empCV.STATUS

                    txtReason.Text = empCV.REASON_UNAPROVE
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData
        Try
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("ACADEMY", True)
                FillRadCombobox(cboAcademy, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboLearningLevel, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("MAJOR", True)
                FillRadCombobox(cboMajor, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("HU_GRADUATE_SCHOOL", True)
                FillRadCombobox(cboGraduateSchool, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboComputerRank, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("MARK_EDU", True)
                FillRadCombobox(cboComputerMark, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguageLevel, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try


            If Not CType(CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE"), Boolean) Then
                lbStatus.Text = "Chức năng chỉnh sửa thông tin đã đóng, liên hệ nhân sự để mở lại"
                CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                CType(MainToolBar.Items(3), RadToolBarButton).Enabled = False
                EnableControlAll(False, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                         cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                         cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
            Else
                Select Case hidStatus.Value
                    Case 0 ' Khai báo thông tin
                        lbStatus.Text = "Đã khai báo thông tin chỉnh sửa, Bạn có thể gửi duyệt thông tin"
                        tbarMainToolBar.Items(0).Enabled = True
                        tbarMainToolBar.Items(3).Enabled = True
                        EnableControlAll(True, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                         cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                         cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
                    Case 1 ' Chờ phê duyệt
                        EnableControlAll(False, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                         cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                         cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
                        lbStatus.Text = "Thông tin chỉnh sửa đang ở trạng thái [ Chờ phê duyệt ], Bạn không thể chỉnh sửa"
                        tbarMainToolBar.Items(0).Enabled = False
                        tbarMainToolBar.Items(3).Enabled = False
                        txtReason.Text = ""
                    Case 2 ' Phê duyệt
                        lbStatus.Text = "Thông tin chỉnh sửa đã được [ phê duyệt ], Bạn có thể khai báo thông tin chỉnh sửa"
                        tbarMainToolBar.Items(0).Enabled = True
                        tbarMainToolBar.Items(3).Enabled = False
                        EnableControlAll(True, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtNoHouseHolds)
                    Case 3 ' Không duyệt
                        lbStatus.Text = "Thông tin chỉnh sửa [ không được phê duyệt ], Bạn có thể khai báo lại thông tin chỉnh sửa"
                        tbarMainToolBar.Items(0).Enabled = True
                        tbarMainToolBar.Items(3).Enabled = False
                        EnableControlAll(True, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                         cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                         cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
                    Case Else
                        lbStatus.Text = "Thông tin mới nhất được [ phê duyệt ], Bạn có thể khai báo tiếp thông tin chỉnh sửa"
                        tbarMainToolBar.Items(0).Enabled = True
                        tbarMainToolBar.Items(3).Enabled = False
                        EnableControlAll(True, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                         cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                         cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                         rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                         txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                         cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                         cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
                End Select
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                     cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                     cboIDPlace, txtNavAddress, txtPerAddress, hidFamilyID,
                                     rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                     txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                     cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                     cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark, txtNoHouseHolds)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim obj As New EmployeeEditDTO
                        obj.EMPLOYEE_ID = EmployeeID
                        If cboFamilyStatus.SelectedValue <> "" Then
                            obj.MARITAL_STATUS = Decimal.Parse(cboFamilyStatus.SelectedValue)
                        End If
                        obj.NAV_ADDRESS = txtNavAddress.Text.Trim()
                        If cboNav_Province.SelectedValue <> "" Then
                            obj.NAV_PROVINCE = Decimal.Parse(cboNav_Province.SelectedValue)
                        End If
                        If cboNav_District.SelectedValue <> "" Then
                            obj.NAV_DISTRICT = Decimal.Parse(cboNav_District.SelectedValue)
                        End If
                        If cboNav_Ward.SelectedValue <> "" Then
                            obj.NAV_WARD = Decimal.Parse(cboNav_Ward.SelectedValue)
                        End If
                        obj.PER_ADDRESS = txtPerAddress.Text.Trim()
                        obj.NO_HOUSEHOLDS = txtNoHouseHolds.Text.Trim()
                        If cboPer_Province.SelectedValue <> "" Then
                            obj.PER_PROVINCE = Decimal.Parse(cboPer_Province.SelectedValue)
                        End If
                        If cboPer_District.SelectedValue <> "" Then
                            obj.PER_DISTRICT = Decimal.Parse(cboPer_District.SelectedValue)
                        End If
                        If cboPer_Ward.SelectedValue <> "" Then
                            obj.PER_WARD = Decimal.Parse(cboPer_Ward.SelectedValue)
                        End If

                        If cboAcademy.SelectedValue <> "" Then
                            obj.ACADEMY = Decimal.Parse(cboAcademy.SelectedValue)
                        End If
                        If cboLearningLevel.SelectedValue <> "" Then
                            obj.LEARNING_LEVEL = Decimal.Parse(cboLearningLevel.SelectedValue)
                        End If
                        If cboMajor.SelectedValue <> "" Then
                            obj.MAJOR = Decimal.Parse(cboMajor.SelectedValue)
                        End If
                        If cboGraduateSchool.SelectedValue <> "" Then
                            obj.GRADUATE_SCHOOL_ID = Decimal.Parse(cboGraduateSchool.SelectedValue)
                        End If
                        If IsNumeric(rnYearGraduate.Value) Then
                            obj.GRADUATION_YEAR = rnYearGraduate.Value
                        End If
                        If cboComputerRank.SelectedValue <> "" Then
                            obj.COMPUTER_RANK = Decimal.Parse(cboComputerRank.SelectedValue)
                        End If
                        If cboComputerMark.SelectedValue <> "" Then
                            obj.COMPUTER_MARK = Decimal.Parse(cboComputerMark.SelectedValue)
                        End If
                        If cboLanguage.SelectedValue <> "" Then
                            obj.LANGUAGE = Decimal.Parse(cboLanguage.SelectedValue)
                        End If
                        If cboLanguageLevel.SelectedValue <> "" Then
                            obj.LANGUAGE_LEVEL = Decimal.Parse(cboLanguageLevel.SelectedValue)
                        End If
                        obj.COMPUTER_CERTIFICATE = txtITBasic.Text.Trim
                        obj.LANGUAGE_MARK = txtLanguageMark.Text.Trim

                        ' CMND
                        obj.CMND_NOTE_CHANGE = txtCMNDNoteChange.Text.Trim
                        obj.ID_NO = txtID_NO.Text.Trim()
                        obj.ID_DATE = rdIDDate.SelectedDate
                        obj.ID_PLACE = cboIDPlace.SelectedValue

                        obj.EXPIRE_DATE_IDNO = rdIDDateEnd.SelectedDate
                        obj.CONTACT_PER = txtContactPerson.Text
                        If cboRelationNLH.SelectedValue <> "" Then
                            obj.RELATION_PER_CTR = Decimal.Parse(cboRelationNLH.SelectedValue)
                        End If
                        obj.CONTACT_PER_MBPHONE = txtPerMobilePhone.Text
                        obj.VILLAGE = txtPerThonAp.Text
                        obj.HOME_PHONE = txtHomePhone.Text
                        obj.MOBILE_PHONE = txtMobilePhone.Text
                        obj.WORK_EMAIL = txtWorkEmail.Text
                        obj.PER_EMAIL = txtPerEmail.Text
                        obj.PERSON_INHERITANCE = txtFirstNameVN.Text
                        obj.BANK_NO = txtBankNo.Text
                        If cboBank.SelectedValue <> "" Then
                            obj.BANK_ID = Decimal.Parse(cboBank.SelectedValue)
                        End If
                        If cboBankBranch.SelectedValue <> "" Then
                            obj.BANK_BRANCH_ID = Decimal.Parse(cboBankBranch.SelectedValue)
                        End If

                        Using rep As New ProfileBusinessRepository

                            If hidID.Value.ToString <> "0" Then
                                isInsert = False
                            End If

                            If isInsert Then
                                rep.InsertEmployeeEdit(obj, hidID.Value)
                                hidStatus.Value = 0
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyEmployeeEdit(obj, hidID.Value)
                                hidStatus.Value = 0
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            txtReason.Text = ""
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboFamilyStatus.ItemsRequested, cboNav_Province.ItemsRequested, cboPer_Province.ItemsRequested,
    cboPer_District.ItemsRequested, cboPer_Ward.ItemsRequested, cboNav_District.ItemsRequested, cboNav_Ward.ItemsRequested,
    cboBank.ItemsRequested, cboBankBranch.ItemsRequested
        Using rep As New ProfileRepository
            Dim dtData As DataTable
            Dim sText As String = e.Text
            Dim dValue As Decimal
            Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
            Select Case sender.ID
                Case cboFamilyStatus.ID
                    dtData = rep.GetOtherList("FAMILY_STATUS", True)
                Case cboNav_Province.ID, cboPer_Province.ID
                    dtData = rep.GetProvinceList(True)
                Case cboNav_District.ID, cboPer_District.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetDistrictList(dValue, True)
                Case cboNav_Ward.ID, cboPer_Ward.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetWardList(dValue, True)
                Case cboBank.ID
                    dtData = rep.GetBankList(True)
                Case cboBankBranch.ID
                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                    dtData = rep.GetBankBranchList(dValue, True)
            End Select

            If sText <> "" Then
                Dim dtExist = (From p In dtData
                               Where p("NAME") IsNot DBNull.Value AndAlso _
                               p("NAME").ToString.ToUpper = sText.ToUpper)

                If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso _
                                    p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())

                        sender.Items.Add(radItem)
                    Next
                Else

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = dtData.Rows.Count
                    e.EndOfItems = True

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())

                        sender.Items.Add(radItem)
                    Next
                End If
            Else
                Dim itemOffset As Integer = e.NumberOfItems
                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                e.EndOfItems = endOffset = dtData.Rows.Count

                For i As Integer = itemOffset To endOffset - 1
                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())

                    sender.Items.Add(radItem)
                Next
            End If
        End Using
    End Sub

    Private Sub cusNO_ID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusNO_ID.ServerValidate
        Try

            Using rep As New ProfileBusinessRepository
                args.IsValid = rep.ValidateEmployee("EXIST_ID_NO", EmployeeCode, txtID_NO.Text)
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    lstID.Add(hidID.Value)
                    If lstID.Count > 0 Then
                        rep.SendEmployeeEdit(lstID)
                        hidStatus.Value = 1
                        lbStatus.Text = "Thông tin chỉnh sửa đang ở trạng thái [ Chờ phê duyệt ], Bạn không thể chỉnh sửa"
                        tbarMainToolBar.Items(0).Enabled = False
                        tbarMainToolBar.Items(3).Enabled = False
                        EnableControlAll(False, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                                    cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                                    cboIDPlace, txtNavAddress, txtPerAddress, rdIDDate,
                                    rdIDDateEnd, txtContactPerson, cboRelationNLH, txtPerMobilePhone, txtPerThonAp, txtHomePhone, txtMobilePhone,
                                    txtWorkEmail, txtPerEmail, txtFirstNameVN, txtBankNo, cboBank, cboBankBranch, txtCMNDNoteChange,
                                     cboAcademy, cboLearningLevel, cboMajor, cboGraduateSchool, rnYearGraduate,
                                     cboComputerRank, cboComputerMark, txtITBasic, cboLanguage, cboLanguageLevel, txtLanguageMark)

                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Custom"

#End Region

End Class