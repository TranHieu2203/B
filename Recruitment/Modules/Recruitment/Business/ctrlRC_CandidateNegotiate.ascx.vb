Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_CandidateNegotiate
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()
    Dim _tyLeThuViec As Decimal
    Dim _tyLeChinhThuc As Decimal
    Dim psp As New RecruitmentStoreProcedure
    Private store As New RecruitmentStoreProcedure()

#Region "Properties"
    Property hidID As Decimal
        Get
            Return ViewState(Me.ID & "_hidID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hidID") = value
        End Set
    End Property
    Property hidOrg As Decimal
        Get
            Return ViewState(Me.ID & "_hidOrg")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hidOrg") = value
        End Set
    End Property
    Property hidTitle As Decimal
        Get
            Return ViewState(Me.ID & "_hidTitle")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hidTitle") = value
        End Set
    End Property
    Property hidProgramID As Decimal
        Get
            Return ViewState(Me.ID & "_hidProgramID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hidProgramID") = value
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
    Property total As Decimal
        Get
            Return ViewState(Me.ID & "_total")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_total") = value
        End Set
    End Property
    Property basicSal As Decimal
        Get
            Return ViewState(Me.ID & "_basicSal")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_basicSal") = value
        End Set
    End Property

    Property lstAllow As List(Of RC_Allowance_CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstAllow")
        End Get
        Set(ByVal value As List(Of RC_Allowance_CandidateDTO))
            ViewState(Me.ID & "_lstAllow") = value
        End Set

    End Property

    Property contractCode As String
        Get
            Return ViewState(Me.ID & "_contractCode")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_contractCode") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi cac control tren page theo trang thai cac control da duoc thiet lap
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            My.Application.ChangeCulture("en-US")
            GetParams()
            'Load % hưởng lương
            LoadPercentDefault()
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao gia tri ban dau cho cac control tren page
    ''' khoi tao trang thai cho grid rgAssets voi cac thiet lap filter
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgMain)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao gia tri cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Rebind du lieu cho rgAssets theo case message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        ' Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                tbcheck.Visible = False
                hidProgramID = Request.Params("ProgramID")
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = hidProgramID})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg = objPro.ORG_ID
                lblTitleName.Text = objPro.TITLE_NAME
                hidTitle = objPro.TITLE_ID
                lblCode.Text = objPro.CODE_YCTD
                lblName.Text = objPro.RC_REQUEST_NAME

                hidCandidateID = Request.Params("CandidateID")
                Dim objCan = rep.GET_LIST_EMPLOYEE_ELECT(hidProgramID, "TRUNGTUYEN")
                Dim infoCan = (From p In objCan Where p("ID_CANDIDATE") = hidCandidateID).FirstOrDefault
                If infoCan IsNot Nothing Then
                    lblRC_EmpCode.Text = infoCan("CANDIDATE_CODE")
                    lblRC_EmpName.Text = infoCan("FULLNAME_VN")
                End If

                'Thông tin đàm phán từ "Loại hợp đồng"
                Dim _filter = New RCNegotiateDTO
                Dim _params = New ParamDTO
                Dim objInfo As List(Of RCNegotiateDTO) = rep.GetRCNegotiate(_filter, _params)
                Dim dtData = (From p In objInfo Where p.RC_PROGRAM_ID = hidProgramID And p.RC_CANDIDATE_ID = hidCandidateID).FirstOrDefault
                If dtData IsNot Nothing Then
                    If IsNumeric(dtData.CONTRACT_TYPE_ID) AndAlso dtData.CONTRACT_TYPE_ID > 0 Then
                        cboContractType.SelectedValue = dtData.CONTRACT_TYPE_ID
                        cboContractType.Text = dtData.CONTRACT_TYPE_NAME
                    End If
                    If IsNumeric(dtData.SAL_TYPE_ID) AndAlso dtData.SAL_TYPE_ID > 0 Then
                        cboSalTYPE.SelectedValue = dtData.SAL_TYPE_ID
                        cboSalTYPE.Text = dtData.SAL_TYPE_NAME
                    End If
                    If IsNumeric(dtData.TAX_TABLE_ID) AndAlso dtData.TAX_TABLE_ID > 0 Then
                        cboTaxTable.SelectedValue = dtData.TAX_TABLE_ID
                        cboTaxTable.Text = dtData.TAX_TABLE_NAME
                    End If
                    If IsNumeric(dtData.EMPLOYEE_TYPE_ID) AndAlso dtData.EMPLOYEE_TYPE_ID > 0 Then
                        cboEmployeeType.SelectedValue = dtData.EMPLOYEE_TYPE_ID
                        cboEmployeeType.Text = dtData.EMPLOYEE_TYPE_NAME
                    End If
                    If IsDate(dtData.CONTRACT_FROMDATE) Then
                        rdStartDate.SelectedDate = dtData.CONTRACT_FROMDATE
                    End If
                    If IsDate(dtData.CONTRACT_TODATE) Then
                        rdExpireDate.SelectedDate = dtData.CONTRACT_TODATE
                    End If
                    If IsNumeric(dtData.SALARY_PROBATION) AndAlso dtData.SALARY_PROBATION > 0 Then
                        rnProbationSal.Value = dtData.SALARY_PROBATION
                        rnProbationSal.Text = dtData.SALARY_PROBATION
                        basicSal = dtData.SALARY_PROBATION
                    End If
                    If IsNumeric(dtData.OTHERSALARY1) AndAlso dtData.OTHERSALARY1 > 0 Then
                        rnOtherSalary1.Value = dtData.OTHERSALARY1
                        rnOtherSalary1.Text = dtData.OTHERSALARY1
                    End If
                    If IsNumeric(dtData.PERCENT_SAL) AndAlso dtData.PERCENT_SAL > 0 Then
                        rnPercentSalary.Value = dtData.PERCENT_SAL
                        rnPercentSalary.Text = dtData.PERCENT_SAL
                    End If
                    If IsNumeric(dtData.SALARY_OFFICIAL) AndAlso dtData.SALARY_OFFICIAL > 0 Then
                        rnOfficialSal.Value = dtData.SALARY_OFFICIAL
                        rnOfficialSal.Text = dtData.SALARY_OFFICIAL
                    End If
                    txtRemark.Text = dtData.NOTE
                End If
                If hidIS_EMP.Value = 1 Then
                    Dim item As RadToolBarButton
                    item = CType(_toolbar.Items(0), RadToolBarButton)
                    item.Enabled = False

                    EnableControlAll(False, cboContractType, rdStartDate, rdExpireDate, cboSalTYPE, cboTaxTable, cboEmployeeType, rnProbationSal, rnOtherSalary1, rnPercentSalary, rnOfficialSal, txtRemark, cboSalTYPESAL, cboTaxTableSAL, cboEmployeeTypeSAL, rnProbationSalSAL, rnOtherSalary1SAL, rnPercentSalarySAL, rnOfficialSalSAL)
                End If

                If cboContractType.SelectedValue <> "" Then
                    Using rep1 As New ProfileRepository
                        ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                        ListComboData.GET_CONTRACTTYPE = True
                        rep1.GetComboList(ListComboData)
                        Dim lst = (From p In ListComboData.LIST_CONTRACTTYPE Where p.ID = cboContractType.SelectedValue).SingleOrDefault
                        contractCode = lst.CODE

                        If contractCode = "HDHN" Then
                            tbcheck.Visible = True
                            Dim objsal As List(Of RC_Salary_CandidateDTO) = rep.GetRCSalaryCandidate(hidProgramID, hidCandidateID)
                            Dim dt = (From p In objsal Where p.RC_PROGRAM_ID = hidProgramID And p.RC_CANDIDATE_ID = hidCandidateID).FirstOrDefault
                            If dt IsNot Nothing Then

                                If IsNumeric(dt.SAL_TYPE_ID) Then
                                    cboSalTYPESAL.SelectedValue = dt.SAL_TYPE_ID
                                End If
                                If IsNumeric(dt.TAX_TABLE_ID) Then
                                    cboTaxTableSAL.SelectedValue = dt.TAX_TABLE_ID
                                End If
                                If IsNumeric(dt.EMPLOYEE_TYPE_ID) Then
                                    cboEmployeeTypeSAL.SelectedValue = dt.EMPLOYEE_TYPE_ID
                                End If
                                If IsDate(dt.EFFECT_DATE) Then
                                    rdEffectDate.SelectedDate = dt.EFFECT_DATE
                                End If

                                If IsNumeric(dt.SALARY_PROBATION) Then
                                    rnProbationSalSAL.Value = dt.SALARY_PROBATION
                                End If
                                If IsNumeric(dt.OTHERSALARY1) Then
                                    rnOtherSalary1SAL.Value = dt.OTHERSALARY1
                                End If
                                If IsNumeric(dt.PERCENT_SAL) Then
                                    rnPercentSalarySAL.Value = dt.PERCENT_SAL
                                End If
                                If IsNumeric(dt.SALARY_OFFICIAL) Then
                                    rnOfficialSalSAL.Value = dt.SALARY_OFFICIAL
                                End If
                            End If
                        Else
                            tbcheck.Visible = False
                        End If

                    End Using
                End If

                lstAllow = rep.GetRCAllowanceCandidate(hidProgramID, hidCandidateID)
                rgAllow.Rebind()
            End If



            _myLog.WriteLog(_myLog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho control combobox, thiet lap ngon ngu hien thi cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtData As New DataTable
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                'load kieu du lieu
                'load loại hợp đồng
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE_POPUP = True
                rep.GetComboList(ListComboData)
                lstContractType = ListComboData.LIST_CONTRACTTYPE_POPUP
                FillDropDownList(cboContractType, lstContractType, "NAME", "ID", Common.Common.SystemLanguage, False)

                dtData = rep.GetSalaryTypeList(Date.Now, True)
                FillRadCombobox(cboSalTYPE, dtData, "NAME", "ID", False)
                FillRadCombobox(cboSalTYPESAL, dtData, "NAME", "ID", False)
                dtData = rep.GetOtherList(OtherTypes.TaxTable, True)
                FillRadCombobox(cboTaxTable, dtData, "NAME", "ID", False)
                FillRadCombobox(cboTaxTableSAL, dtData, "NAME", "ID", False)
                dtData = rep.GetOtherList(OtherTypes.EmployeeType, True)
                FillRadCombobox(cboEmployeeType, dtData, "NAME", "ID", True)
                FillRadCombobox(cboEmployeeTypeSAL, dtData, "NAME", "ID", True)

                dtData = rep.GET_Hu_Allowance_List(True)
                FillRadCombobox(cboAlow, dtData, "NAME", "ID", False)
                dtData = rep.GetOtherList("DVT_PHUCAP", True)
                FillRadCombobox(cboAllowanceUnit, dtData, "NAME", "ID", False)
            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw
        End Try

    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 30/06/2017 15:33
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control OnMainToolbar
    ''' Cac command la them moi, sua, kich hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objRCNegotiateList As New RCNegotiateDTO
        Dim objRcSalaryCandidateList As New RC_Salary_CandidateDTO
        Dim rep As New RecruitmentRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If cboSalTYPE.Text = "Thử việc" Then
                        If rnPercentSalary.Value < 85 Then
                            ShowMessage(Translate("nhập % hưởng lương 'thử việc' > 85%"), NotifyType.Warning)
                            'rnPercentSalary.Value = 85
                            rnPercentSalary.Focus()
                            Exit Sub
                        End If
                    End If

                    If contractCode = "HDHN" Then
                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập ngày hiệu lực."), NotifyType.Warning)
                            Exit Sub
                        Else
                            If rdEffectDate.SelectedDate <= rdStartDate.SelectedDate Or rdEffectDate.SelectedDate >= rdExpireDate.SelectedDate Then
                                ShowMessage(Translate("Ngày hiệu lực phải lớn hơn ngày bắt đầu và nhỏ hơn ngày kết thúc."), NotifyType.Warning)
                                Exit Sub
                            End If

                            objRcSalaryCandidateList.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If

                        If cboSalTYPESAL.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn Nhóm lương."), NotifyType.Warning)
                            Exit Sub
                        Else
                            objRcSalaryCandidateList.SAL_TYPE_ID = cboSalTYPESAL.SelectedValue
                        End If

                        If cboTaxTableSAL.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn Biểu thuế."), NotifyType.Warning)
                            Exit Sub
                        Else
                            objRcSalaryCandidateList.TAX_TABLE_ID = cboTaxTableSAL.SelectedValue
                        End If

                        If cboEmployeeTypeSAL.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn loại nhân viên."), NotifyType.Warning)
                            Exit Sub
                        Else
                            objRcSalaryCandidateList.EMPLOYEE_TYPE_ID = cboEmployeeTypeSAL.SelectedValue
                        End If

                        If rnProbationSalSAL.Value Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập Lương cơ bản."), NotifyType.Warning)
                            Exit Sub
                        Else
                            objRcSalaryCandidateList.SALARY_PROBATION = rnProbationSalSAL.Value
                        End If

                        If rnPercentSalarySAL.Value Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập % hưởng lương"), NotifyType.Warning)
                            Exit Sub
                        Else
                            objRcSalaryCandidateList.PERCENT_SAL = rnPercentSalarySAL.Value
                        End If

                        If rnOfficialSalSAL.Value IsNot Nothing Then
                            objRcSalaryCandidateList.SALARY_OFFICIAL = rnOfficialSalSAL.Value
                        End If

                        If rnOtherSalary1SAL.Value IsNot Nothing Then
                            objRcSalaryCandidateList.OTHERSALARY1 = rnOtherSalary1SAL.Value
                        End If

                        objRcSalaryCandidateList.RC_CANDIDATE_ID = hidCandidateID
                        objRcSalaryCandidateList.RC_PROGRAM_ID = hidProgramID

                    End If

                    If Page.IsValid Then
                        objRCNegotiateList.RC_PROGRAM_ID = hidProgramID
                        objRCNegotiateList.RC_CANDIDATE_ID = hidCandidateID
                        objRCNegotiateList.CONTRACT_TYPE_ID = cboContractType.SelectedValue
                        objRCNegotiateList.CONTRACT_FROMDATE = rdStartDate.SelectedDate
                        objRCNegotiateList.CONTRACT_TODATE = rdExpireDate.SelectedDate
                        objRCNegotiateList.SAL_TYPE_ID = cboSalTYPE.SelectedValue
                        objRCNegotiateList.TAX_TABLE_ID = cboTaxTable.SelectedValue
                        objRCNegotiateList.EMPLOYEE_TYPE_ID = cboEmployeeType.SelectedValue
                        objRCNegotiateList.SALARY_PROBATION = rnProbationSal.Value
                        If IsNumeric(rnOtherSalary1.Value) Then
                            objRCNegotiateList.OTHERSALARY1 = rnOtherSalary1.Value
                        End If
                        If IsNumeric(rnOfficialSal.Value) Then
                            objRCNegotiateList.SALARY_OFFICIAL = rnOfficialSal.Value
                        End If
                        objRCNegotiateList.PERCENT_SAL = rnPercentSalary.Value
                        objRCNegotiateList.NOTE = txtRemark.Text
                        If rep.InsertRCNegotiate(objRCNegotiateList, gID) Then
                            ' Trở về màn hình chính: tắt popup
                            'CẬP NHẬT OFFER_ID
                            Dim Offer_ID As String = "OFFERLETTER"
                            rep.UPDATE_PROGRAM_CANDIDATE_OFFER_ID(hidCandidateID, hidProgramID, Offer_ID)

                            ' INSERT PHU CAP
                            UPDATE_ALLOW()
                            ' HĐ học nghề
                            If contractCode = "HDHN" Then
                                rep.InsertRcSalaryCandidate(objRcSalaryCandidateList)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else

                    End If
            End Select
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UPDATE_ALLOW()
        Dim rep As New RecruitmentRepository
        Dim dtAllowUpdate As New List(Of RC_Allowance_CandidateDTO)

        For Each dtGrid As GridDataItem In rgAllow.Items

            Dim obj As New RC_Allowance_CandidateDTO

            obj.RC_CANDIDATE_ID = hidCandidateID
            obj.RC_PROGRAM_ID = hidProgramID
            'obj.ALLOWANCE_ID = Decimal.Parse(dtGrid("ALLOWANCE_ID").Text)
            obj.ALLOWANCE_ID = dtGrid.GetDataKeyValue("ALLOWANCE_ID")
            obj.EFFECT_FROM = dtGrid.GetDataKeyValue("EFFECT_FROM")
            obj.EFFECT_TO = dtGrid.GetDataKeyValue("EFFECT_TO")
            obj.MONEY = dtGrid.GetDataKeyValue("MONEY")
            obj.ALLOWANCE_UNIT_ID = dtGrid.GetDataKeyValue("ALLOWANCE_UNIT_ID")

            dtAllowUpdate.Add(obj)
        Next

        If dtAllowUpdate.Count > 0 Then
            rep.InsertRcAlowCandidate(dtAllowUpdate)

            lstAllow = rep.GetRCAllowanceCandidate(hidProgramID, hidCandidateID)
            rgAllow.Rebind()
        End If

    End Sub

    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Try
            If lstAllow IsNot Nothing AndAlso lstAllow.Count > 0 Then
                rgAllow.DataSource = lstAllow
            Else
                lstAllow = New List(Of RC_Allowance_CandidateDTO)
                rgAllow.DataSource = New List(Of RC_Allowance_CandidateDTO)
            End If

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgAllow_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAllow.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertAllow"
                    If cboAlow.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn phụ cấp"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rdAlowStartDate.SelectedDate Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập ngày bắt đầu phụ cấp"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If ntxtMoneny.Value Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập số tiền phụ cấp"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rdAlowExpireDate.SelectedDate IsNot Nothing Then
                        If rdAlowStartDate.SelectedDate >= rdAlowExpireDate.SelectedDate Then
                            ShowMessage(Translate("Ngày kết thúc phụ cấp phải lớn hơn ngày bắt đầu phụ cấp."), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    Dim allow As New RC_Allowance_CandidateDTO
                    If cboAllowanceUnit.SelectedValue <> "" Then
                        allow.ALLOWANCE_UNIT_ID = cboAllowanceUnit.SelectedValue
                        allow.ALLOWANCE_UNIT_NAME = cboAllowanceUnit.Text
                    End If
                    allow.ALLOWANCE_ID = cboAlow.SelectedValue
                    allow.ALLOWANCE_NAME = cboAlow.Text
                    allow.EFFECT_FROM = rdAlowStartDate.SelectedDate

                    If rdAlowExpireDate.SelectedDate IsNot Nothing Then
                        allow.EFFECT_TO = rdAlowExpireDate.SelectedDate
                    End If

                    allow.MONEY = ntxtMoneny.Value

                    allow.RC_CANDIDATE_ID = hidCandidateID
                    allow.RC_PROGRAM_ID = hidProgramID

                    lstAllow.Add(allow)
                    rgAllow.Rebind()

                Case "DeleteAllow"
                    'Dim str As String
                    If rgAllow.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = "REMOVEALLOW"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "REMOVEALLOW" And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                For Each selected As GridDataItem In rgAllow.SelectedItems
                    lstAllow.RemoveAll(Function(x) x.ALLOWANCE_ID = selected.GetDataKeyValue("ALLOWANCE_ID"))
                Next

                rgAllow.Rebind()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selectedIndexChanged cua control cboContractType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboContractType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContractType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ControlSelected_Change()
            Using rep As New ProfileRepository
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                Dim lst = (From p In ListComboData.LIST_CONTRACTTYPE Where p.ID = cboContractType.SelectedValue).SingleOrDefault
                contractCode = lst.CODE

                If contractCode = "HDHN" Then
                    tbcheck.Visible = True
                Else
                    tbcheck.Visible = False
                End If

            End Using

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rdStartDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdStartDate.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As New DataTable
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            dtData = rep.GetSalaryTypeList(rdStartDate.SelectedDate, True)
            FillRadCombobox(cboSalTYPE, dtData, "NAME", "ID", False)
            ControlSelected_Change()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function ControlSelected_Change()
        Dim item As New Profile.ProfileBusiness.ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim startTime As DateTime = DateTime.UtcNow

        'trường”theo tháng/ngày” 
        Dim code As String = ""
        'Quy tắc lấy ngày hết hiệu lực
        Dim code_enddate As String = ""
        Dim obj As New Profile.ProfileBusiness.ContractTypeDTO
        Try
            If rdStartDate.SelectedDate Is Nothing Then
                ShowMessage(Translate("Mời chọn ngày bắt đầu"), Utilities.NotifyType.Warning)
                Exit Function
            End If
            If cboContractType.SelectedValue = "" Then
                ShowMessage(Translate("Mời chọn loại hợp đồng"), Utilities.NotifyType.Warning)
                Exit Function
            End If
            Using rep As New ProfileRepository
                obj = rep.GetContractTypeID(Decimal.Parse(cboContractType.SelectedValue))
                If obj IsNot Nothing Then
                    code = obj.FLOWING_MD
                    code_enddate = obj.CODE_GET_ENDDATE
                Else
                    Exit Function
                End If
            End Using

            Select Case code
                Case "KXD"
                    rdExpireDate.SelectedDate = Nothing
                Case "NGAY"
                    rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddDays(obj.PERIOD)
                Case "THANG"
                    If code_enddate = "THANG" Then
                        rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddMonths(obj.PERIOD).LastDateOfMonth()
                    End If

                    If code_enddate = "QUY" Then
                        Dim month = rdStartDate.SelectedDate.Value.AddMonths(obj.PERIOD).Month
                        Dim year = rdStartDate.SelectedDate.Value.AddMonths(obj.PERIOD).Year

                        'Quý 1
                        If month >= 1 And month <= 3 Then
                            Dim d As New Date(year, 3, 1)
                            rdExpireDate.SelectedDate = d.LastDateOfMonth()
                        End If
                        'Quý 2
                        If month >= 4 And month <= 6 Then
                            Dim d As New Date(year, 6, 1)
                            rdExpireDate.SelectedDate = d.LastDateOfMonth()
                        End If
                        'Quý 3
                        If month >= 7 And month <= 9 Then
                            Dim d As New Date(year, 9, 1)
                            rdExpireDate.SelectedDate = d.LastDateOfMonth()
                        End If
                        'Quý 4
                        If month >= 10 And month <= 12 Then
                            Dim d As New Date(year, 12, 1)
                            rdExpireDate.SelectedDate = d.LastDateOfMonth()
                        End If
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cua control cusvalContractType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusvalContractType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusvalContractType.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New Profile.ProfileBusiness.ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            validate.ID = cboContractType.SelectedValue
            validate.ACTFLG = "A"
            args.IsValid = rep.ValidateContractType(validate)

            If Not args.IsValid Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, False)
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub LoadPercentDefault()
        Dim commonStore As New CommonProcedureNew
        Try
            Dim tyLeThuViec = commonStore.GET_VALUE_PA_PAYMENT("TyLeThuViec")
            Dim tyLeChinhThuc = commonStore.GET_VALUE_PA_PAYMENT("TyLeChinhThuc")
            If IsNumeric(tyLeThuViec) Then
                _tyLeThuViec = tyLeThuViec
            End If
            If IsNumeric(tyLeChinhThuc) Then
                _tyLeChinhThuc = tyLeChinhThuc
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cboEmployeeType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployeeType.SelectedIndexChanged
        'Dim taxTables As New List(Of OtherListDTO)
        If String.IsNullOrWhiteSpace(cboEmployeeType.SelectedValue) Then
            Exit Sub
        End If
        Select Case cboEmployeeType.Text
            Case "Thử việc"
                'EnableControlAll(True, rnPercentSalary, rnOfficialSal, rnOtherSalary1, rnProbationSal)
                rnPercentSalary.Value = _tyLeThuViec
                'ClearControlValue(rnProbationSal)
            Case "Chính thức"
                'EnableControlAll(True, rnPercentSalary, rnOfficialSal, rnOtherSalary1, rnProbationSal)
                rnPercentSalary.Value = _tyLeChinhThuc
                'ClearControlValue(rnProbationSal)
        End Select
        If rnPercentSalary.Value.HasValue Then
            'If cboSalTYPE.Text <> "Kiêm nhiệm" Then
            rnOfficialSal.Value = (rnProbationSal.Value + _
                If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * rnPercentSalary.Value / 100
            ' End If
        End If
    End Sub
    Private Sub rnProbationSal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnProbationSal.TextChanged
        Try
            'CalculatorSalary()
            If rnProbationSal.Value < basicSal Then
                ShowMessage(Translate("Không được điều chỉnh lương cơ bản nhỏ hơn hệ số nhân với lương tối thiểu vùng"), NotifyType.Warning)
                Exit Sub
            End If
            If rnProbationSal.Value Is Nothing Then
                ShowMessage(Translate("Bạn hãy nhập lương cơ bản"), NotifyType.Warning)
                Exit Sub
            End If

            If rnPercentSalary.Value.HasValue Then
                'If cboSalTYPE.Text <> "Kiêm nhiệm" Then
                rnOfficialSal.Value = (rnProbationSal.Value + _
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * rnPercentSalary.Value / 100
                ' basicSalary.Enabled = False
                'End If
                CHECKWARNING_SAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOtherSalary1_TextChanged(sender As Object, e As System.EventArgs) Handles rnOtherSalary1.TextChanged
        Try
            If rnPercentSalary.Value.HasValue Then
                'If cboSalTYPE.Text <> "Kiêm nhiệm" Then
                rnOfficialSal.Value = (rnProbationSal.Value +
                    If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * rnPercentSalary.Value / 100
                'End If
                CHECKWARNING_SAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOfficialSal_TextChanged(sender As Object, e As System.EventArgs) Handles rnOfficialSal.TextChanged
        Try
            If rnOfficialSal.Value.HasValue Then
                CHECKWARNING_SAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnPercentSalary_TextChanged(sender As Object, e As System.EventArgs) Handles rnPercentSalary.TextChanged
        Try
            If rnPercentSalary.Value.HasValue Then
                'If cboSalTYPE.Text <> "Kiêm nhiệm" Then                    
                rnOfficialSal.Value = (rnProbationSal.Value + _
                    If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * rnPercentSalary.Value / 100
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub cboSalTYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalTYPE.SelectedIndexChanged
        If String.IsNullOrWhiteSpace(cboSalTYPE.SelectedValue) Then
            Exit Sub
        End If
        'Select Case cboSalTYPE.Text
        '    Case "Thử việc"
        '        EnableControlAll(True, rnPercentSalary, rnOfficialSal, rnOtherSalary1, rnProbationSal)
        '        'rnPercentSalary.Value = _tyLeThuViec
        '        ClearControlValue(rnProbationSal)
        '    Case "Chính thức"
        '        EnableControlAll(True, rnPercentSalary, rnOfficialSal, rnOtherSalary1, rnProbationSal)
        '        'rnPercentSalary.Value = _tyLeChinhThuc
        '        ClearControlValue(rnProbationSal)
        'End Select
        If rnPercentSalary.Value.HasValue Then
            'If cboSalTYPE.Text <> "Kiêm nhiệm" Then
            rnOfficialSal.Value = (rnProbationSal.Value +
                If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * rnPercentSalary.Value / 100
            'End If
        End If
    End Sub
    Private Sub GetParams()
        Try

            If Request.Params("IS_EMP") IsNot Nothing Then
                hidIS_EMP.Value = Decimal.Parse(Request.Params("IS_EMP"))
            Else
                hidIS_EMP.Value = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CHECKWARNING_SAL()
        Try
            Dim RANK_SAL As Decimal = 0
            Dim LuongAB As Decimal = 0
            Dim TongLuongThucTe As Decimal = 0
            If IsNumeric(hidOrg) AndAlso IsNumeric(hidTitle) AndAlso rdStartDate.SelectedDate IsNot Nothing Then
                RANK_SAL = store.GET_RC_HR_PLANING_DETAIL_RANK_SAL(Int32.Parse(hidOrg), Int32.Parse(hidTitle), rdStartDate.SelectedDate)


                Dim DT = store.GET_PLAN_DETAIL_BY_ORG("ADMIN", rdStartDate.SelectedDate, Int32.Parse(hidOrg), False)
                If DT.Rows.Count > 0 Then
                    LuongAB = DT.Rows(0)("LUONGAB")
                End If
                TongLuongThucTe = store.GET_WORKING_TOTAL_SAL("ADMIN", rdStartDate.SelectedDate, Int32.Parse(hidOrg), False)

            End If
            If rnOfficialSal.Value.HasValue AndAlso RANK_SAL > -1 Then
                If rnOfficialSal.Value > RANK_SAL Then
                    ShowMessage(Translate("Mức lương đã vượt quá Rank lương của vị trí tuyển dụng trong bộ phận"), NotifyType.Warning)
                End If
            End If
            If store.CHECK_EXIST_SE_CONFIG("RC_SAL_BUDGET_EXCEEDED") = -1 Then
                If rnOfficialSal.Value.HasValue AndAlso LuongAB > 0 AndAlso TongLuongThucTe > 0 Then
                    Dim to_sal As Decimal = Decimal.Parse(rnOfficialSal.Value) + TongLuongThucTe
                    If to_sal > LuongAB Then
                        ShowMessage(Translate("Mức lương đã vượt quá ngân sách tuyển dụng của bộ phận"), NotifyType.Warning)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboEmployeeTypeSAL_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployeeTypeSAL.SelectedIndexChanged
        'Dim taxTables As New List(Of OtherListDTO)
        If String.IsNullOrWhiteSpace(cboEmployeeType.SelectedValue) Then
            Exit Sub
        End If
        Select Case cboEmployeeType.Text
            Case "Thử việc"
                rnPercentSalary.Value = _tyLeThuViec
            Case "Chính thức"
                rnPercentSalary.Value = _tyLeChinhThuc
        End Select
        If rnPercentSalary.Value.HasValue Then
            rnOfficialSalSAL.Value = (rnProbationSalSAL.Value + _
                If(rnOtherSalary1SAL.Value.HasValue, rnOtherSalary1SAL.Value, 0)) * rnPercentSalarySAL.Value / 100
        End If
    End Sub
    Private Sub rnProbationSalSAL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnProbationSalSAL.TextChanged
        Try
            If rnProbationSalSAL.Value < basicSal Then
                ShowMessage(Translate("Không được điều chỉnh lương cơ bản nhỏ hơn hệ số nhân với lương tối thiểu vùng"), NotifyType.Warning)
                Exit Sub
            End If
            If rnProbationSalSAL.Value Is Nothing Then
                ShowMessage(Translate("Bạn hãy nhập lương cơ bản"), NotifyType.Warning)
                Exit Sub
            End If

            If rnPercentSalarySAL.Value.HasValue Then
                rnOfficialSalSAL.Value = (rnProbationSalSAL.Value + _
                        If(rnOtherSalary1SAL.Value.HasValue, rnOtherSalary1SAL.Value, 0)) * rnPercentSalarySAL.Value / 100
                CHECKWARNING_SALSAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOtherSalary1SAL_TextChanged(sender As Object, e As System.EventArgs) Handles rnOtherSalary1SAL.TextChanged
        Try
            If rnPercentSalarySAL.Value.HasValue Then
                rnOfficialSalSAL.Value = (rnProbationSalSAL.Value +
                    If(rnOtherSalary1SAL.Value.HasValue, rnOtherSalary1SAL.Value, 0)) * rnPercentSalarySAL.Value / 100
                CHECKWARNING_SALSAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOfficialSalSAL_TextChanged(sender As Object, e As System.EventArgs) Handles rnOfficialSalSAL.TextChanged
        Try
            If rnOfficialSalSAL.Value.HasValue Then
                CHECKWARNING_SALSAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnPercentSalarySAL_TextChanged(sender As Object, e As System.EventArgs) Handles rnPercentSalarySAL.TextChanged
        Try
            If rnPercentSalarySAL.Value.HasValue Then
                rnOfficialSalSAL.Value = (rnProbationSalSAL.Value + _
                    If(rnOtherSalary1SAL.Value.HasValue, rnOtherSalary1SAL.Value, 0)) * rnPercentSalarySAL.Value / 100
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub cboSalTYPESAL_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalTYPESAL.SelectedIndexChanged
        If String.IsNullOrWhiteSpace(cboSalTYPESAL.SelectedValue) Then
            Exit Sub
        End If
        If rnPercentSalarySAL.Value.HasValue Then
            rnOfficialSalSAL.Value = (rnProbationSalSAL.Value +
                If(rnOtherSalary1SAL.Value.HasValue, rnOtherSalary1SAL.Value, 0)) * rnPercentSalarySAL.Value / 100
        End If
    End Sub

    Private Sub CHECKWARNING_SALSAL()
        Try
            Dim RANK_SAL As Decimal = 0
            Dim LuongAB As Decimal = 0
            Dim TongLuongThucTe As Decimal = 0
            If IsNumeric(hidOrg) AndAlso IsNumeric(hidTitle) AndAlso rdStartDate.SelectedDate IsNot Nothing Then
                RANK_SAL = store.GET_RC_HR_PLANING_DETAIL_RANK_SAL(Int32.Parse(hidOrg), Int32.Parse(hidTitle), rdStartDate.SelectedDate)


                Dim DT = store.GET_PLAN_DETAIL_BY_ORG("ADMIN", rdStartDate.SelectedDate, Int32.Parse(hidOrg), False)
                If DT.Rows.Count > 0 Then
                    LuongAB = DT.Rows(0)("LUONGAB")
                End If
                TongLuongThucTe = store.GET_WORKING_TOTAL_SAL("ADMIN", rdStartDate.SelectedDate, Int32.Parse(hidOrg), False)

            End If
            If rnOfficialSalSAL.Value.HasValue AndAlso RANK_SAL > -1 Then
                If rnOfficialSalSAL.Value > RANK_SAL Then
                    ShowMessage(Translate("Mức lương đã vượt quá Rank lương của vị trí tuyển dụng trong bộ phận"), NotifyType.Warning)
                End If
            End If
            If store.CHECK_EXIST_SE_CONFIG("RC_SAL_BUDGET_EXCEEDED") = -1 Then
                If rnOfficialSalSAL.Value.HasValue AndAlso LuongAB > 0 AndAlso TongLuongThucTe > 0 Then
                    Dim to_sal As Decimal = Decimal.Parse(rnOfficialSalSAL.Value) + TongLuongThucTe
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

End Class