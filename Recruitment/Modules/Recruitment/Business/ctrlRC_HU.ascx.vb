Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_HU
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Private store As New RecruitmentStoreProcedure()
    Public Overrides Property MustAuthorize As Boolean = True
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

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property
    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property lstContractType As List(Of Profile.ProfileBusiness.ContractTypeDTO)
        Get
            Return ViewState(Me.ID & "_lstContractType")
        End Get
        Set(ByVal value As List(Of Profile.ProfileBusiness.ContractTypeDTO))
            ViewState(Me.ID & "_lstContractType") = value
        End Set
    End Property
    Property hidCandidateID As Decimal
        Get
            Return ViewState(Me.ID & "_hidCandidateID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hidCandidateID") = value
        End Set
    End Property

    Property DT_RCNegotiate As RCNegotiateDTO
        Get
            Return ViewState(Me.ID & "_DT_RCNegotiate")
        End Get
        Set(ByVal value As RCNegotiateDTO)
            ViewState(Me.ID & "_DT_RCNegotiate") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            If CommonConfig.APP_SETTING_15() Then
                lbObjectLaborNew.Visible = False
                spObjectLaborNew.Visible = False
                cboObjectLabor.Visible = False
                reqObjectLabor.Visible = False
            End If

            If CommonConfig.APP_SETTING_16() Then
                spObjectLaborNew.Visible = False
                reqObjectLabor.Visible = False
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Dim dtData As New DataTable
            Using rep As New ProfileRepository
                'load kieu du lieu
                'load loại hợp đồng
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                lstContractType = ListComboData.LIST_CONTRACTTYPE
                FillDropDownList(cboContractType, lstContractType, "NAME", "ID", Common.Common.SystemLanguage, True)

                dtData = rep.GetSalaryTypeList(Date.Now, True)
                FillRadCombobox(cboSalTYPE, dtData, "NAME", "ID", False)
                dtData = rep.GetOtherList(OtherTypes.TaxTable, True)
                FillRadCombobox(cboTaxTable, dtData, "NAME", "ID", False)
                dtData = rep.GetOtherList(OtherTypes.EmployeeType, True)
                FillRadCombobox(cboEmployeeType, dtData, "NAME", "ID", True)
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            ' CType(MainToolBar.Items(2), RadToolBarButton).Text = "Chuyển HSNS"
            ' CType(MainToolBar.Items(2), RadToolBarButton).Enabled = True
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("Save",
            '                                                         ToolbarIcons.Edit,
            '                                                         ToolbarAuthorize.Special1,
            '                                                         Translate("Lưu")))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("Cancel",
            '                                                         ToolbarIcons.Edit,
            '                                                         ToolbarAuthorize.Special1,
            '                                                         Translate("Hủy")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SEND_HSNS",
                                                                     ToolbarIcons.Edit,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Chuyển HSNS")))
            CType(MainToolBar.Items(1), RadToolBarButton).OuterCssClass = "RadToolbarDelete"
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
                    CurrentState = CommonMessage.STATE_NORMAL
                    Dim _filter = New RC_TransferCAN_ToEmployeeDTO
                    _filter.CANDIDATE_ID = hidCandidateID
                    _filter.RC_PROGRAM_ID = IDSelect
                    Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = IDSelect})
                    Dim objtran = rep.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(_filter)
                    If obj IsNot Nothing Then
                        txtcodeYCTD.Text = obj.CODE_YCTD
                        txtnameYCTD.Text = obj.NAME_YCTD
                        txtPerRequest.Text = obj.PER_REQUEST
                        txtOrgName.Text = obj.ORG_NAME
                        txtTitle.Text = obj.TITLE_NAME
                        'If obj.FOLLOWERS_EMP_ID IsNot Nothing Then
                        '    hidEmpID.Value = obj.FOLLOWERS_EMP_ID
                        'End If
                        If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                            txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                            'txtOrgName_EMP.ToolTip = DrawTreeByString(obj.ORG_DESC)
                        End If
                        rdSendDate.SelectedDate = obj.SEND_DATE
                        If obj.LOCATION_ID IsNot Nothing Then
                            cboLocation.SelectedValue = obj.LOCATION_ID
                        End If
                    End If
                    Dim objCan = rep.GET_LIST_EMPLOYEE_ELECT(IDSelect, "TRUNGTUYEN")
                    Dim infoCan = (From p In objCan Where p("ID_CANDIDATE") = hidCandidateID).FirstOrDefault
                    If infoCan IsNot Nothing Then
                        'lblRC_EmpCode.Text = infoCan("CANDIDATE_CODE")
                        txtTenNV.Text = infoCan("FULLNAME_VN")
                    End If
                    If objtran IsNot Nothing Then
                        If objtran.DIRECT_MANAGER_ID IsNot Nothing Then
                            hidEmpID.Value = objtran.DIRECT_MANAGER_ID
                        End If
                        hidOrgID.Value = objtran.ORG_ID
                        txtOrgName_EMP.Text = objtran.ORG_NAME

                        LoadComboTitle()
                        hidTitleID.Value = objtran.TITLE_ID
                        cboChucDanh.SelectedValue = objtran.TITLE_ID
                        txtPersonRequest.Text = objtran.DIRECT_MANAGER_NAME
                        If IsNumeric(objtran.OBJECT_LABOR_ID) Then
                            cboObjectLabor.SelectedValue = objtran.OBJECT_LABOR_ID 'loại hình lao động
                        End If
                        If IsNumeric(objtran.OBJECT_ATTENDANCE_ID) Then
                            cboObject.SelectedValue = objtran.OBJECT_ATTENDANCE_ID
                        End If
                        If IsNumeric(objtran.OBJECT_EMPLOYEE_ID) Then
                            rcOBJECT_EMPLOYEE.SelectedValue = objtran.OBJECT_EMPLOYEE_ID
                        End If

                        If IsNumeric(objtran.OBJECT_ATTENDANT_ID) Then
                            rcOBJECT_ATTENDANT.SelectedValue = objtran.OBJECT_ATTENDANT_ID
                        End If

                        If IsNumeric(objtran.WORK_PLACE_ID) Then
                            rcRegion.SelectedValue = objtran.WORK_PLACE_ID
                        End If
                        'Thông tin đàm phán từ "Loại hợp đồng"
                        If IsNumeric(objtran.CONTRACT_TYPE_ID) AndAlso objtran.CONTRACT_TYPE_ID > 0 Then
                            cboContractType.SelectedValue = objtran.CONTRACT_TYPE_ID
                            cboContractType.Text = objtran.CONTRACT_TYPE_NAME

                            If IsNumeric(objtran.SAL_TYPE_ID) AndAlso objtran.SAL_TYPE_ID > 0 Then
                                cboSalTYPE.SelectedValue = objtran.SAL_TYPE_ID
                            End If
                            If IsNumeric(objtran.TAX_TABLE_ID) AndAlso objtran.TAX_TABLE_ID > 0 Then
                                cboTaxTable.SelectedValue = objtran.TAX_TABLE_ID
                            End If
                            If IsNumeric(objtran.EMPLOYEE_TYPE_ID) AndAlso objtran.EMPLOYEE_TYPE_ID > 0 Then
                                cboEmployeeType.SelectedValue = objtran.EMPLOYEE_TYPE_ID
                            End If
                            If IsDate(objtran.CONTRACT_FROM) Then
                                rdStartDate.SelectedDate = objtran.CONTRACT_FROM
                            End If
                            If IsDate(objtran.CONTRACT_TO) Then
                                rdExpireDate.SelectedDate = objtran.CONTRACT_TO
                            End If
                            If IsNumeric(objtran.SAL_BASIC) AndAlso objtran.SAL_BASIC > 0 Then
                                rnProbationSal.Value = objtran.SAL_BASIC
                                'rnProbationSal.Text = objtran.SALARY_PROBATION
                            End If
                            If IsNumeric(objtran.OTHERSALARY1) AndAlso objtran.OTHERSALARY1 > 0 Then
                                rnOtherSalary1.Value = objtran.OTHERSALARY1
                                'rnOtherSalary1.Text = objtran.OTHERSALARY1
                            End If
                            If IsNumeric(objtran.PERCENT_SAL) AndAlso objtran.PERCENT_SAL > 0 Then
                                rnPercentSalary.Text = objtran.PERCENT_SAL
                            End If
                            If IsNumeric(objtran.SAL_TOTAL) AndAlso objtran.SAL_TOTAL > 0 Then
                                rnOfficialSal.Value = objtran.SAL_TOTAL
                                'rnOfficialSal.Text = objtran.SALARY_OFFICIAL
                            End If


                        Else
                            'Thông tin đàm phán từ "Loại hợp đồng"
                            Dim _params = New RecruitmentBusiness.ParamDTO
                            Dim objInfo As List(Of RCNegotiateDTO) = rep.GetRCNegotiate(New RCNegotiateDTO, _params)
                            DT_RCNegotiate = (From p In objInfo Where p.RC_PROGRAM_ID = IDSelect And p.RC_CANDIDATE_ID = hidCandidateID).FirstOrDefault
                            If DT_RCNegotiate IsNot Nothing Then
                                If IsNumeric(DT_RCNegotiate.CONTRACT_TYPE_ID) AndAlso DT_RCNegotiate.CONTRACT_TYPE_ID > 0 Then
                                    cboContractType.SelectedValue = DT_RCNegotiate.CONTRACT_TYPE_ID
                                    cboContractType.Text = DT_RCNegotiate.CONTRACT_TYPE_NAME
                                End If
                                If IsNumeric(DT_RCNegotiate.SAL_TYPE_ID) AndAlso DT_RCNegotiate.SAL_TYPE_ID > 0 Then
                                    cboSalTYPE.SelectedValue = DT_RCNegotiate.SAL_TYPE_ID
                                End If
                                If IsNumeric(DT_RCNegotiate.TAX_TABLE_ID) AndAlso DT_RCNegotiate.TAX_TABLE_ID > 0 Then
                                    cboTaxTable.SelectedValue = DT_RCNegotiate.TAX_TABLE_ID
                                End If
                                If IsNumeric(DT_RCNegotiate.EMPLOYEE_TYPE_ID) AndAlso DT_RCNegotiate.EMPLOYEE_TYPE_ID > 0 Then
                                    cboEmployeeType.SelectedValue = DT_RCNegotiate.EMPLOYEE_TYPE_ID
                                End If
                                If IsDate(DT_RCNegotiate.CONTRACT_FROMDATE) Then
                                    rdStartDate.SelectedDate = DT_RCNegotiate.CONTRACT_FROMDATE
                                End If
                                If IsDate(DT_RCNegotiate.CONTRACT_TODATE) Then
                                    rdExpireDate.SelectedDate = DT_RCNegotiate.CONTRACT_TODATE
                                End If
                                If IsNumeric(DT_RCNegotiate.SALARY_PROBATION) AndAlso DT_RCNegotiate.SALARY_PROBATION > 0 Then
                                    rnProbationSal.Value = DT_RCNegotiate.SALARY_PROBATION
                                    rnProbationSal.Text = DT_RCNegotiate.SALARY_PROBATION
                                End If
                                If IsNumeric(DT_RCNegotiate.OTHERSALARY1) AndAlso DT_RCNegotiate.OTHERSALARY1 > 0 Then
                                    rnOtherSalary1.Value = DT_RCNegotiate.OTHERSALARY1
                                    rnOtherSalary1.Text = DT_RCNegotiate.OTHERSALARY1
                                End If
                                If IsNumeric(DT_RCNegotiate.PERCENT_SAL) AndAlso DT_RCNegotiate.PERCENT_SAL > 0 Then
                                    rnPercentSalary.Text = DT_RCNegotiate.PERCENT_SAL
                                End If
                                If IsNumeric(DT_RCNegotiate.SALARY_OFFICIAL) AndAlso DT_RCNegotiate.SALARY_OFFICIAL > 0 Then
                                    rnOfficialSal.Value = DT_RCNegotiate.SALARY_OFFICIAL
                                    rnOfficialSal.Text = DT_RCNegotiate.SALARY_OFFICIAL
                                End If
                            End If
                        End If
                    End If


                Case "InsertView"
                    CurrentState = CommonMessage.STATE_EDIT
                    'CurrentState = CommonMessage.STATE_NORMAL
                    Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = IDSelect})
                    txtcodeYCTD.Text = obj.CODE_YCTD
                    txtnameYCTD.Text = obj.NAME_YCTD
                    txtPerRequest.Text = obj.PER_REQUEST

                    txtOrgName.Text = obj.ORG_NAME
                    txtOrgName_EMP.Text = obj.ORG_NAME
                    If obj.FOLLOWERS_EMP_ID IsNot Nothing Then
                        hidEmpID.Value = obj.FOLLOWERS_EMP_ID
                    End If
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                        txtOrgName_EMP.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If

                    rdSendDate.SelectedDate = obj.SEND_DATE

                    If obj.LOCATION_ID IsNot Nothing Then
                        cboLocation.SelectedValue = obj.LOCATION_ID
                    End If
                    'hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    txtTitle.Text = obj.TITLE_NAME

                    hidTitleID.Value = obj.TITLE_ID
                    LoadComboTitle()
                    cboChucDanh.SelectedValue = obj.TITLE_ID
                    cboChucDanh.Text = obj.TITLE_NAME
                    If IsNumeric(obj.WORKING_PLACE_ID) Then
                        rcRegion.SelectedValue = obj.WORKING_PLACE_ID
                    End If
                    'LAY THONG TIN UNG VIEN
                    Dim objCan = rep.GET_LIST_EMPLOYEE_ELECT(IDSelect, "TRUNGTUYEN")
                    Dim infoCan = (From p In objCan Where p("ID_CANDIDATE") = hidCandidateID).FirstOrDefault
                    If infoCan IsNot Nothing Then
                        'lblRC_EmpCode.Text = infoCan("CANDIDATE_CODE")
                        txtTenNV.Text = infoCan("FULLNAME_VN")
                    End If

                    'Thông tin đàm phán từ "Loại hợp đồng"
                    Dim _filter = New RCNegotiateDTO
                    Dim _params = New RecruitmentBusiness.ParamDTO
                    Dim objInfo As List(Of RCNegotiateDTO) = rep.GetRCNegotiate(_filter, _params)
                    DT_RCNegotiate = (From p In objInfo Where p.RC_PROGRAM_ID = IDSelect And p.RC_CANDIDATE_ID = hidCandidateID).FirstOrDefault
                    If DT_RCNegotiate IsNot Nothing Then
                        If IsNumeric(DT_RCNegotiate.CONTRACT_TYPE_ID) AndAlso DT_RCNegotiate.CONTRACT_TYPE_ID > 0 Then
                            cboContractType.SelectedValue = DT_RCNegotiate.CONTRACT_TYPE_ID
                            cboContractType.Text = DT_RCNegotiate.CONTRACT_TYPE_NAME
                        End If
                        If IsNumeric(DT_RCNegotiate.SAL_TYPE_ID) AndAlso DT_RCNegotiate.SAL_TYPE_ID > 0 Then
                            cboSalTYPE.SelectedValue = DT_RCNegotiate.SAL_TYPE_ID
                        End If
                        If IsNumeric(DT_RCNegotiate.TAX_TABLE_ID) AndAlso DT_RCNegotiate.TAX_TABLE_ID > 0 Then
                            cboTaxTable.SelectedValue = DT_RCNegotiate.TAX_TABLE_ID
                        End If
                        If IsNumeric(DT_RCNegotiate.EMPLOYEE_TYPE_ID) AndAlso DT_RCNegotiate.EMPLOYEE_TYPE_ID > 0 Then
                            cboEmployeeType.SelectedValue = DT_RCNegotiate.EMPLOYEE_TYPE_ID
                        End If
                        If IsDate(DT_RCNegotiate.CONTRACT_FROMDATE) Then
                            rdStartDate.SelectedDate = DT_RCNegotiate.CONTRACT_FROMDATE
                        End If
                        If IsDate(DT_RCNegotiate.CONTRACT_TODATE) Then
                            rdExpireDate.SelectedDate = DT_RCNegotiate.CONTRACT_TODATE
                        End If
                        If IsNumeric(DT_RCNegotiate.SALARY_PROBATION) AndAlso DT_RCNegotiate.SALARY_PROBATION > 0 Then
                            rnProbationSal.Value = DT_RCNegotiate.SALARY_PROBATION
                            rnProbationSal.Text = DT_RCNegotiate.SALARY_PROBATION
                        End If
                        If IsNumeric(DT_RCNegotiate.OTHERSALARY1) AndAlso DT_RCNegotiate.OTHERSALARY1 > 0 Then
                            rnOtherSalary1.Value = DT_RCNegotiate.OTHERSALARY1
                            rnOtherSalary1.Text = DT_RCNegotiate.OTHERSALARY1
                        End If
                        If IsNumeric(DT_RCNegotiate.PERCENT_SAL) AndAlso DT_RCNegotiate.PERCENT_SAL > 0 Then
                            rnPercentSalary.Text = DT_RCNegotiate.PERCENT_SAL
                        End If
                        If IsNumeric(DT_RCNegotiate.SALARY_OFFICIAL) AndAlso DT_RCNegotiate.SALARY_OFFICIAL > 0 Then
                            rnOfficialSal.Value = DT_RCNegotiate.SALARY_OFFICIAL
                            rnOfficialSal.Text = DT_RCNegotiate.SALARY_OFFICIAL
                        End If
                    End If
            End Select
            If cboSalTYPE.Text = "" Then
                chkSend.Enabled = False
                chkSend.Checked = False
            Else
                chkSend.Enabled = True

            End If
            If hidIS_EMP.Value = 1 Then
                Dim item As RadToolBarButton
                item = CType(_toolbar.Items(0), RadToolBarButton)
                item.Enabled = False
                Dim item1 As RadToolBarButton
                item1 = CType(_toolbar.Items(1), RadToolBarButton)
                item1.Enabled = False
                EnableControlAll(False, cboChucDanh, cboObjectLabor, cboObject, rcOBJECT_EMPLOYEE, rcOBJECT_ATTENDANT, rcRegion)
                DisableRadCombobox(cboChucDanh)
                DisableRadCombobox(cboObjectLabor)
                DisableRadCombobox(cboObject)
                DisableRadCombobox(rcOBJECT_EMPLOYEE)
                DisableRadCombobox(rcOBJECT_ATTENDANT)
                DisableRadCombobox(rcRegion)
                btnFindEmployee.Enabled = False
                btnFindOrg.Enabled = False
                txtPersonRequest.Enabled = False
                Dim c As RadTextBox = txtOrgName_EMP
                c.BackColor = Drawing.Color.Khaki
                Dim d As RadTextBox = txtPersonRequest
                d.BackColor = Drawing.Color.Khaki
                chkSend.Enabled = False
            End If

            If rep.CheckTransferToEmployee(hidCandidateID) = False Then
                Dim item As RadToolBarButton
                item = CType(_toolbar.Items(1), RadToolBarButton)
                item.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim log As New UserLog
        log = LogHelper.GetUserLog
        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Save()
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    'Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Program&group=Business")
                Case "SEND_HSNS"
                    Save()
                    If IsNumeric(hidID.Value) Then

                        Dim psp As New RecruitmentRepository
                        'If (rdStartDate.SelectedDate IsNot Nothing) Then
                        '    'Lay thong tin sign ho so luong
                        '    Dim signer = psp.GET_SIGNER_BY_FUNC("ctrlHU_WageNewEdit", rdStartDate.SelectedDate)
                        '    If signer.Rows.Count > 0 Then
                        '        If IsNumeric(signer.Rows(0)("ID")) Then
                        '            SignName_ID_SAL = CDec(signer.Rows(0)("ID"))
                        '        End If
                        '        SignName_SAL = signer.Rows(0)("EMPLOYEE_NAME")
                        '        SignTitle_SAL = signer.Rows(0)("TITLE_NAME")
                        '    End If

                        '    'Lay thong tin sign hop dong
                        '    Dim signer1 = psp.GET_SIGNER_BY_FUNC("ctrlHU_ContractNewEdit", rdStartDate.SelectedDate)
                        '    If signer1.Rows.Count > 0 Then
                        '        If IsNumeric(signer.Rows(0)("ID")) Then
                        '            hidSign.Value = CDec(signer.Rows(0)("ID"))
                        '        End If
                        '        txtSignName.Text = signer.Rows(0)("EMPLOYEE_NAME")
                        '        txtSignTitle.Text = signer.Rows(0)("TITLE_NAME")
                        '    End If
                        'End If

                        Dim CopyAllowance As Decimal
                        If chkIsCopyAllowance.Checked Then
                            CopyAllowance = 1
                        Else
                            CopyAllowance = 0
                        End If

                        Dim CopySalaryAndContract As Decimal
                        If chkSend.Checked Then
                            CopySalaryAndContract = 1
                        Else
                            CopySalaryAndContract = 0
                        End If

                        Dim REPs As Int32
                        REPs = psp.INSERT_CADIDATE_EMPLOYEE1(hidCandidateID, IDSelect, Int32.Parse(hidID.Value), CopyAllowance, CopySalaryAndContract, log.Username, log.Ip + log.ComputerName)
                        If REPs = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Dim item As RadToolBarButton
                            item = CType(_toolbar.Items(0), RadToolBarButton)
                            item.Enabled = False
                            Dim item1 As RadToolBarButton
                            item1 = CType(_toolbar.Items(1), RadToolBarButton)
                            item1.Enabled = False
                        Else
                            Dim MESS As String = String.Empty
                            Select Case REPs
                                Case -2
                                    MESS = "Cập nhật dữ liệu hồ sơ nhân viên lỗi"
                                Case -3
                                    MESS = "Cập nhật dữ liệu hồ sơ nhân viên lỗi (CV)"
                                Case -4
                                    MESS = "Cập nhật dữ liệu trình độ học vấn lỗi"
                                Case -5
                                    MESS = "Cập nhật dữ liệu quá trình công tác trước đây lỗi (CV)"
                                Case -6
                                    MESS = "Cập nhật dữ liệu thông tin công ty trước đây lỗi"
                                Case -7
                                    MESS = "Cập nhật trạng thái lỗi"
                                Case -8
                                    MESS = "Cập nhật thông tin sức khỏe lỗi"
                                Case -9
                                    MESS = "Cập nhật thông tin người thân lỗi"
                            End Select
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) + Chr(10) + MESS _
                            , Utilities.NotifyType.Error)
                        End If
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Private Sub txtPersonRequest_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPersonRequest.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtPersonRequest.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of EmployeePopupFindListDTO)
                Dim _filter As New EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtPersonRequest.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtPersonRequest.Text = ""
                ElseIf Count = 1 Then
                    Dim item = EmployeeList(0)
                    txtPersonRequest.Text = item.EMPLOYEE_CODE & " - " & item.FULLNAME_VN
                    hidEmpID.Value = item.EMPLOYEE_ID
                    isLoadPopup = 0
                ElseIf Count > 1 Then
                    If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                    End If
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtPersonRequest.Text
                        ctrlFindEmployeePopup.MultiSelect = True
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.Show()
                        isLoadPopup = 1
                    End If
                End If
            Else
                txtPersonRequest.Text = ""
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Try
            isLoadPopup = 1
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
                txtPersonRequest.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                hidEmpID.Value = itm.EMPLOYEE_ID
            Next
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName_EMP.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName_EMP.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
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
            End Select
            'EnableControlAll()
            DisableRadCombobox(cboSalTYPE)
            DisableRadCombobox(cboTaxTable)
            DisableRadCombobox(cboEmployeeType)
            DisableRadCombobox(cboContractType)
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub
    Private Sub DisableRadCombobox(ByVal cntrl As RadComboBox)

        With cntrl
            .ShowToggleImage = False
            .AllowCustomText = False
            .MarkFirstMatch = False
            .EnableTextSelection = False
            .EnableLoadOnDemand = False
            .BackColor = Drawing.Color.Khaki
            .ShowDropDownOnTextboxClick = False
            .Enabled = False
            .Skin = String.Empty    'important 
            .AutoPostBack = False
            .CssClass = "new"
            Dim tmpItem As RadComboBoxItem = .SelectedItem
            .Items.Clear()
            If tmpItem IsNot Nothing Then
                .Items.Clear()
                .Items.Add(tmpItem)
                tmpItem.Selected = True
            End If
        End With
    End Sub
    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Dim rep1 As New ProfileRepository
            Dim obj As Integer = rep1.GET_DEFAULT_OBJECT_ATTENDANCE()

            rep1.Dispose()

            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("OBJECT_LABOR", True)
                FillRadCombobox(cboObjectLabor, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("OBJECT_ATTENDANCE", True)
                FillRadCombobox(cboObject, dtData, "NAME", "ID")

                If obj > 0 Then
                    cboObject.SelectedValue = obj
                End If

                dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
                FillRadCombobox(rcOBJECT_EMPLOYEE, dtData, "NAME", "ID", True)

                dtData = rep.GetOtherList("OBJECT_ATTENDANT", True)
                FillRadCombobox(rcOBJECT_ATTENDANT, dtData, "NAME", "ID", True)

                dtData = rep.Get_HU_WORK_PLACE("", True)
                FillRadCombobox(rcRegion, dtData, "NAME_VN", "ID")



            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If Request.Params("ID") IsNot Nothing Then
                hidID.Value = Decimal.Parse(Request.Params("ID"))
            End If
            If Request.Params("ProgramID") IsNot Nothing Then
                IDSelect = Decimal.Parse(Request.Params("ProgramID"))
            End If
            If Request.Params("IS_EMP") IsNot Nothing Then
                hidIS_EMP.Value = Decimal.Parse(Request.Params("IS_EMP"))
            End If
            If Request.Params("CandidateID") IsNot Nothing Then
                hidCandidateID = Decimal.Parse(Request.Params("CandidateID"))
            End If
            If IsNumeric(hidID.Value) Then
                Refresh("UpdateView")
            Else
                Refresh("InsertView")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadComboTitle()

        If hidOrgID.Value <> "" Then
            Dim dtData As DataTable
            dtData = store.GET_TITLE_IN_PLAN(hidOrgID.Value, 0)
            If dtData.Rows.Count > 0 Then
                FillRadCombobox(cboChucDanh, dtData, "NAME", "ID")
            End If
        Else
            cboChucDanh.Items.Clear()
            cboChucDanh.ClearSelection()
            cboChucDanh.Text = ""
        End If


    End Sub
    Private Sub Save()
        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Dim obj As New RC_TransferCAN_ToEmployeeDTO
        If IsNumeric(hidID.Value) Then
            obj.ID = hidID.Value
        End If
        If IsNumeric(IDSelect) Then
            obj.RC_PROGRAM_ID = IDSelect
        End If
        If IsNumeric(hidCandidateID) Then
            obj.CANDIDATE_ID = hidCandidateID
        End If
        If IsNumeric(hidOrgID.Value) Then
            obj.ORG_ID = hidOrgID.Value
        End If
        If IsNumeric(cboChucDanh.SelectedValue) Then
            obj.TITLE_ID = cboChucDanh.SelectedValue
        End If
        If IsNumeric(hidEmpID.Value) Then
            obj.DIRECT_MANAGER_ID = hidEmpID.Value 'quan ly truc tiep
        End If
        If IsNumeric(cboObjectLabor.SelectedValue) Then
            obj.OBJECT_LABOR_ID = cboObjectLabor.SelectedValue 'loại hình lao động
        End If
        If IsNumeric(cboObject.SelectedValue) Then
            obj.OBJECT_ATTENDANCE_ID = cboObject.SelectedValue
        End If
        If IsNumeric(rcOBJECT_EMPLOYEE.SelectedValue) Then
            obj.OBJECT_EMPLOYEE_ID = rcOBJECT_EMPLOYEE.SelectedValue
        End If
        If IsNumeric(rcOBJECT_ATTENDANT.SelectedValue) Then
            obj.OBJECT_ATTENDANT_ID = rcOBJECT_ATTENDANT.SelectedValue
        End If

        If IsNumeric(rcRegion.SelectedValue) Then
            obj.WORK_PLACE_ID = rcRegion.SelectedValue
        End If

        If IsNumeric(cboContractType.SelectedValue) Then
            obj.CONTRACT_TYPE_ID = cboContractType.SelectedValue
        End If
        If IsDate(rdStartDate.SelectedDate) Then
            obj.CONTRACT_FROM = rdStartDate.SelectedDate
        End If
        If IsDate(rdExpireDate.SelectedDate) Then
            obj.CONTRACT_TO = rdExpireDate.SelectedDate
        End If
        If IsNumeric(cboSalTYPE.SelectedValue) Then
            obj.SAL_TYPE_ID = cboSalTYPE.SelectedValue
        End If
        If IsNumeric(cboTaxTable.SelectedValue) Then
            obj.TAX_TABLE_ID = cboTaxTable.SelectedValue
        End If
        If IsNumeric(cboEmployeeType.SelectedValue) Then
            obj.EMPLOYEE_TYPE_ID = cboEmployeeType.SelectedValue
        End If
        If IsNumeric(rnProbationSal.Value) Then
            obj.SAL_BASIC = rnProbationSal.Value
        End If
        If IsNumeric(rnOtherSalary1.Value) Then
            obj.OTHERSALARY1 = rnOtherSalary1.Value
        End If
        If IsNumeric(rnPercentSalary.Value) Then
            obj.PERCENT_SAL = rnPercentSalary.Value
        End If

        If IsNumeric(rnOfficialSal.Value) Then
            obj.SAL_TOTAL = rnOfficialSal.Value
        End If


        obj.TRANSFER_HSL_HD = chkSend.Checked
        'If hidEmpID.Value <> "" Then
        '    obj.EMPLOYEE_CODE = hidEmpID.Value
        'End If
        'If hidEmpID.Value <> "" Then
        '    obj.STATUS = hidEmpID.Value
        'End If
        If IsNumeric(hidID.Value) Then
            If rep.Modify_RC_TRANSFERCAN_TOEMPLOYEE(obj, gID) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                'Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Program&group=Business")
                hidID.Value = gID
                Dim item1 As RadToolBarButton
                item1 = CType(_toolbar.Items(1), RadToolBarButton)
                item1.Enabled = True
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Else
            If rep.Insert_RC_TRANSFERCAN_TOEMPLOYEE(obj, gID) Then
                'Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Program&group=Business")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                hidID.Value = gID
                Dim item1 As RadToolBarButton
                item1 = CType(_toolbar.Items(1), RadToolBarButton)
                item1.Enabled = True
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        End If


    End Sub
    Private Sub Save_HUWOKING()

        Using rep As New ProfileBusinessRepository
            Dim gID As Decimal
            Dim objWorking = New Profile.ProfileBusiness.WorkingDTO
            rep.InsertWorking(objWorking, gID)
        End Using


    End Sub
#End Region

End Class