Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlLocationNewEdit
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployee As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployee_Contract As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Private psp As New ProfileStoreProcedure()
    Dim log As UserLog = LogHelper.GetUserLog
    Private hfr As New HistaffFrameworkRepository

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Location As LocationDTO
        Get
            Return ViewState(Me.ID & "_Location")
        End Get
        Set(ByVal value As LocationDTO)
            ViewState(Me.ID & "_Location") = value
        End Set
    End Property

    Property Locations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_Locations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_Locations") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property ItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_ItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_ItemList") = value
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

    Property ComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboData") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property OldBankID As String
        Get
            Return ViewState(Me.ID & "_OldBankID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_OldBankID") = value
        End Set
    End Property

    Property IsUpload As Decimal
        Get
            Return ViewState(Me.ID & "_IsUpload")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsUpload") = value
        End Set
    End Property

    Property ActiveLocations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveLocations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_ActiveLocations") = value
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

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Request.Params("ID")
                End If
                If IDSelect <> 0 Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim starttime As DateTime = DateTime.UtcNow
        Try
            GetParams()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(starttime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim dt = rep.GetLocationID(Me.IDSelect)
                    If dt IsNot Nothing Then
                        hfOrg.Value = dt.ORG_ID
                        txtLocationCODE.Text = dt.CODE
                        txtLocationEn.Text = dt.LOCATION_EN_NAME
                        txtLocationVN.Text = dt.LOCATION_VN_NAME
                        txtAddress.Text = dt.ADDRESS
                        txtPhone.Text = dt.PHONE
                        txtFax.Text = dt.FAX
                        txtAddress_Emp.Text = dt.WORK_ADDRESS
                        txtOfficePlace.Text = dt.CONTRACT_PLACE
                        txtWebsite.Text = dt.WEBSITE
                        txtAccountNumber.Text = dt.ACCOUNT_NUMBER
                        OldBankID = dt.ACCOUNT_NUMBER
                        txtLocationShort.Text = dt.LOCATION_SHORT_NAME
                        txtNameVn.Text = dt.NAME_VN
                        If dt.REGION IsNot Nothing Then
                            cboRegion.SelectedValue = dt.REGION
                        End If
                        cbBank.ClearSelection()
                        If dt.BANK_ID IsNot Nothing Then
                            cbBank.SelectedValue = dt.BANK_ID
                            hfBank.Value = dt.BANK_ID
                            FillRadCombobox(cboRank_Banch, rep.GetBankBranchByBankID(hfBank.Value), "NAME", "ID", True)
                        End If

                        cboRank_Banch.ClearSelection()
                        cboRank_Banch.Text = ""
                        If dt.BANK_BRANCH_ID IsNot Nothing And cbBank.SelectedValue <> "" Then
                            cboRank_Banch.SelectedValue = dt.BANK_BRANCH_ID
                            hfBank_Branch.Value = dt.BANK_BRANCH_ID
                        End If
                        txtTaxCode.Text = dt.TAX_CODE
                        txtChange_tax_code.Text = dt.CHANGE_TAX_CODE
                        rdpTaxDate.SelectedDate = dt.TAX_DATE
                        If dt.EMP_LAW_ID IsNot Nothing Then
                            LoadEmpInfor(1, dt.EMP_LAW_ID, dt)
                        Else
                            ClearControlValue(txtLawAgentId, txtLawAgentNationality, txtLawAgentTitle, hfLawAgent)
                        End If

                        cboProvince.ClearSelection()
                        If dt.PROVINCE_ID IsNot Nothing Then
                            cboProvince.SelectedValue = dt.PROVINCE_ID
                            FillRadCombobox(cboDistrict, rep.GetDistrictList(dt.PROVINCE_ID, True), "NAME", "ID")
                        End If

                        cboDistrict.ClearSelection()
                        cboDistrict.Text = ""
                        If dt.DISTRICT_ID IsNot Nothing Then
                            cboDistrict.SelectedValue = dt.DISTRICT_ID
                            FillRadCombobox(cboWard, rep.GetWardList(dt.DISTRICT_ID, True), "NAME", "ID")
                        End If

                        cboWard.ClearSelection()
                        cboWard.Text = ""
                        If dt.WARD_ID IsNot Nothing Then
                            cboWard.SelectedValue = dt.WARD_ID
                        End If

                        If dt.IS_SIGN_CONTRACT Is Nothing Or dt.IS_SIGN_CONTRACT = 0 Then
                            ckIsSignContract.Checked = False
                        Else
                            ckIsSignContract.Checked = True
                        End If

                        txtUpload_LG.Text = dt.FILE_LOGO
                        txtUploadFile_LG.Text = dt.ATTACH_FILE_LOGO

                        txtUpload_HD.Text = dt.FILE_HEADER
                        txtUploadFile_HD.Text = dt.ATTACH_FILE_HEADER

                        txtUpload_FT.Text = dt.FILE_FOOTER
                        txtUploadFile_FT.Text = dt.ATTACH_FILE_FOOTER

                        If dt.EMP_SIGNCONTRACT_ID IsNot Nothing Then
                            LoadEmpInfor(2, dt.EMP_SIGNCONTRACT_ID, dt)
                        Else
                            ClearControlValue(txtSignupAgent, txtSignupAgentTitle, txtSigupAgentNationality, hfSignUpAgent)
                        End If

                        txtTaxPlace.Text = dt.TAX_PLACE
                        txtNameBusiness.Text = dt.BUSINESS_NAME
                        txtNumberBusiness.Text = dt.BUSINESS_NUMBER

                        rdRegisterDate.SelectedDate = dt.BUSINESS_REG_DATE
                        txtNote.Text = dt.NOTE

                        If dt.INS_LIST_ID IsNot Nothing Then
                            rcOrgInsurance.SelectedValue = dt.INS_LIST_ID
                        Else
                            rcOrgInsurance.SelectedValue = Nothing
                        End If
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

#Region "Event"
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objLocationFunction As New LocationDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If hfOrg.Value IsNot Nothing And hfOrg.Value <> "" Then
                            objLocationFunction.ORG_ID = hfOrg.Value
                        End If

                        If rep.CHECK_LOCATION_EXITS(Me.IDSelect, objLocationFunction.ORG_ID) Then
                            ShowMessage(Translate("Thông tin Location đã tồn lại, Vui lòng kiểm tra lại."), NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtLocationCODE.Text IsNot Nothing And txtLocationCODE.Text <> "" Then
                            objLocationFunction.CODE = txtLocationCODE.Text.Trim
                        End If
                        objLocationFunction.LOCATION_EN_NAME = txtLocationEn.Text
                        objLocationFunction.LOCATION_VN_NAME = txtLocationVN.Text
                        objLocationFunction.ADDRESS = txtAddress.Text
                        objLocationFunction.PHONE = txtPhone.Text
                        objLocationFunction.FAX = txtFax.Text
                        objLocationFunction.WORK_ADDRESS = txtAddress_Emp.Text
                        objLocationFunction.CONTRACT_PLACE = txtOfficePlace.Text
                        objLocationFunction.WEBSITE = txtWebsite.Text
                        objLocationFunction.ACCOUNT_NUMBER = txtAccountNumber.Text
                        objLocationFunction.LOCATION_SHORT_NAME = txtLocationShort.Text
                        If cboRegion.SelectedValue <> "" Then
                            objLocationFunction.REGION = cboRegion.SelectedValue
                        End If
                        objLocationFunction.NAME_VN = txtNameVn.Text
                        If hfBank.Value IsNot Nothing And hfBank.Value <> "" Then
                            objLocationFunction.BANK_ID = hfBank.Value
                        End If
                        If hfBank_Branch.Value IsNot Nothing And hfBank_Branch.Value <> "" Then
                            objLocationFunction.BANK_BRANCH_ID = hfBank_Branch.Value
                        End If
                        objLocationFunction.TAX_CODE = txtTaxCode.Text
                        objLocationFunction.CHANGE_TAX_CODE = txtChange_tax_code.Text

                        objLocationFunction.TAX_DATE = rdpTaxDate.SelectedDate

                        If hfLawAgent.Value IsNot Nothing And hfLawAgent.Value <> "" Then
                            objLocationFunction.EMP_LAW_ID = hfLawAgent.Value
                        End If
                        If hfSignUpAgent.Value IsNot Nothing And hfSignUpAgent.Value <> "" Then
                            objLocationFunction.EMP_SIGNCONTRACT_ID = hfSignUpAgent.Value
                        End If
                        objLocationFunction.TAX_PLACE = txtTaxPlace.Text
                        objLocationFunction.BUSINESS_NAME = txtNameBusiness.Text
                        objLocationFunction.BUSINESS_NUMBER = txtNumberBusiness.Text
                        objLocationFunction.BUSINESS_REG_DATE = rdRegisterDate.SelectedDate
                        objLocationFunction.ACTFLG = "A"
                        objLocationFunction.NOTE = txtNote.Text

                        objLocationFunction.IS_SIGN_CONTRACT = If(ckIsSignContract.Checked = True, 1, 0)
                        objLocationFunction.PROVINCE_ID = If(cboProvince.SelectedValue <> "", Decimal.Parse(cboProvince.SelectedValue), 0)
                        objLocationFunction.DISTRICT_ID = If(cboDistrict.SelectedValue <> "", Decimal.Parse(cboDistrict.SelectedValue), 0)
                        objLocationFunction.WARD_ID = If(cboWard.SelectedValue <> "", Decimal.Parse(cboWard.SelectedValue), 0)

                        objLocationFunction.FILE_LOGO = txtUpload_LG.Text.Trim
                        objLocationFunction.ATTACH_FILE_LOGO = txtUploadFile_LG.Text.Trim

                        objLocationFunction.FILE_HEADER = txtUpload_HD.Text.Trim
                        objLocationFunction.ATTACH_FILE_HEADER = txtUploadFile_HD.Text.Trim

                        objLocationFunction.FILE_FOOTER = txtUpload_FT.Text.Trim
                        objLocationFunction.ATTACH_FILE_FOOTER = txtUploadFile_FT.Text.Trim
                        objLocationFunction.INS_LIST_ID = If(rcOrgInsurance.SelectedValue <> "", Decimal.Parse(rcOrgInsurance.SelectedValue), 0)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertLocation(objLocationFunction, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objLocationFunction.ID = Me.IDSelect
                                If rep.ModifyLocation(objLocationFunction, gID) Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            FindEmployee.Controls.Clear()
            FindEmployee_Contract.Controls.Clear()

            Select Case isLoadPopup
                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
                Case 2
                    ctrlFindEmployee = Me.Register("ctrlFindEmployee", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployee.MultiSelect = False
                    ctrlFindEmployee.MustHaveContract = False
                    ctrlFindEmployee.LoadAllOrganization = True
                    FindEmployee.Controls.Add(ctrlFindEmployee)
                Case 3
                    ctrlFindEmployee_Contract = Me.Register("ctrlFindEmployee_Contract", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployee_Contract.MultiSelect = False
                    ctrlFindEmployee_Contract.MustHaveContract = False
                    ctrlFindEmployee_Contract.LoadAllOrganization = True
                    FindEmployee_Contract.Controls.Add(ctrlFindEmployee_Contract)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_BANK = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                ListComboData.GET_DISTRICT = True
                rep.GetComboList(ListComboData)
                ComboData = ListComboData
            End If


            Dim dtPlace = rep.GetProvinceList(True)
            FillRadCombobox(cboProvince, dtPlace, "NAME", "ID")
            Dim dt = rep.GetOtherList("LOCATION", True)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FillRadCombobox(cboRegion, dt, "NAME", "ID")
            End If

            FillDropDownList(cbBank, ListComboData.LIST_BANK, "NAME", "ID", Common.Common.SystemLanguage, True, cbBank.SelectedValue)

            Dim lstSource As DataTable = psp.GetInsListInsurance(False)
            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                FillRadCombobox(rcOrgInsurance, lstSource, "NAME", "ID")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnLawAgentId_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLawAgentId.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindEmployee.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnSignupAgent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSignupAgent.Click
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindEmployee_Contract.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeContract_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee_Contract.EmployeeSelected
        Try
            Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
            Dim nation As String = String.Empty

            lstCommonEmployee = CType(ctrlFindEmployee_Contract.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim dt = psp.EmployeeCV_GetInfo(item.EMPLOYEE_ID)
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    nation = dt.Rows(0)("NATION_NAME_VN").ToString()
                End If

                txtSignupAgent.Text = item.FULLNAME_VN
                txtSignupAgentTitle.Text = item.TITLE_NAME
                txtSigupAgentNationality.Text = nation
                hfSignUpAgent.Value = item.ID
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployee_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee.EmployeeSelected
        Try
            Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
            Dim nation As String = String.Empty

            lstCommonEmployee = CType(ctrlFindEmployee.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim dt = psp.EmployeeCV_GetInfo(item.EMPLOYEE_ID)
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    nation = dt.Rows(0)("NATION_NAME_VN").ToString()
                End If

                txtLawAgentId.Text = item.FULLNAME_VN
                txtLawAgentTitle.Text = item.TITLE_NAME
                txtLawAgentNationality.Text = nation
                hfLawAgent.Value = item.ID

            End If

            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) Handles cboProvince.ItemsRequested, cboDistrict.ItemsRequested, cboWard.ItemsRequested
        Dim startTime As DateTime = DateTime.UtcNow
        Using rep As New ProfileRepository
            Try
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal

                Select Case sender.ID
                    Case cboProvince.ID
                        dtData = rep.GetProvinceList(True)
                    Case cboDistrict.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboWard.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                End Select
                If sText <> "" Then
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
                        sender.Items.Add(radItem)
                    Next
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Private Sub btnUploadFile_LG_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_LG.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 0
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnUploadFile_HD_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_HD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 1
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnUploadFile_FT_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_FT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 2
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileRepository
            If IsUpload = 0 Then
                txtUploadFile_LG.Text = ""
            ElseIf IsUpload = 1 Then
                txtUploadFile_HD.Text = ""
            Else
                txtUploadFile_FT.Text = ""
            End If

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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        Dim fByte = System.IO.File.ReadAllBytes(fileName)
                        rep.SaveTemplateFileHost(fByte, "\ReportTemplates\Profile\LocationInfo\" & str_Filename, file.FileName)
                        If IsUpload = 0 Then
                            txtUpload_LG.Text = file.FileName
                        ElseIf IsUpload = 1 Then
                            txtUpload_HD.Text = file.FileName
                        Else
                            txtUpload_FT.Text = file.FileName
                        End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS,XLSX,TXT,CTR,DOC,DOCX,XML,PNG,JPG,BITMAP,JPEG,GIF,PDF,RAR,ZIP,PPT,PPTX"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                If IsUpload = 0 Then
                    loadDatasource(IsUpload, txtUpload_LG.Text)
                ElseIf IsUpload = 1 Then
                    loadDatasource(IsUpload, txtUpload_HD.Text)
                Else
                    loadDatasource(IsUpload, txtUpload_FT.Text)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_LG_Click(sender As Object, e As System.EventArgs) Handles btnDownload_LG.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_LG.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + txtUploadFile_LG.Text + txtUpload_LG.Text)
                ZipFiles(strPath_Down, 0)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_HD_Click(sender As Object, e As System.EventArgs) Handles btnDownload_HD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_HD.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + txtUploadFile_HD.Text + txtUpload_HD.Text)
                ZipFiles(strPath_Down, 1)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_FT_Click(sender As Object, e As System.EventArgs) Handles btnDownload_FT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_FT.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + txtUploadFile_FT.Text + txtUpload_FT.Text)
                ZipFiles(strPath_Down, 2)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub cboRank_Banch_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboRank_Banch.SelectedIndexChanged
        If cboRank_Banch.SelectedValue <> "" Then
            hfBank_Branch.Value = Double.Parse(cboRank_Banch.SelectedValue)
        Else
            hfBank_Branch.Value = 0
        End If
    End Sub

    Private Sub cbBank_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbBank.SelectedIndexChanged
        Dim rep As New ProfileRepository
        Try
            If cbBank.SelectedValue <> "" Then
                hfBank.Value = Double.Parse(cbBank.SelectedValue)
                Dim bankData = rep.GetBankBranchByBankID(hfBank.Value)
                cboRank_Banch.ClearSelection()
                cboRank_Banch.Text = ""
                FillRadCombobox(cboRank_Banch, bankData, "NAME", "ID", True)
            Else
                hfBank.Value = 0
                cboRank_Banch.ClearSelection()
                cboRank_Banch.Text = ""
                cboRank_Banch.DataSource = New DataTable
                cboRank_Banch.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboProvince_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProvince.SelectedIndexChanged
        Try
            If cboProvince.SelectedValue <> "" Then
                Using rep As New ProfileRepository
                    cboDistrict.Text = ""
                    cboDistrict.ClearSelection()
                    Dim dtData As DataTable = rep.GetDistrictList(cboProvince.SelectedValue, True)
                    FillRadCombobox(cboDistrict, dtData, "NAME", "ID")
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboDistrict_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDistrict.SelectedIndexChanged
        Try
            If cboDistrict.SelectedValue <> "" Then
                Using rep As New ProfileRepository
                    cboWard.Text = ""
                    cboWard.ClearSelection()
                    Dim dtData As DataTable = rep.GetWardList(cboDistrict.SelectedValue, True)
                    FillRadCombobox(cboWard, dtData, "NAME", "ID")
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If txtAccountNumber.Text().Trim() <> "" Then
                        If (psp.BankID_HasExist(txtAccountNumber.Text()) > 0) Then
                            args.IsValid = False
                            Me.ShowMessage(Translate("Số tài khoản đã tồn tại"), Utilities.NotifyType.Error)
                        Else
                            args.IsValid = True
                        End If
                    Else
                        args.IsValid = True
                    End If
                Case CommonMessage.STATE_EDIT
                    If txtAccountNumber.Text().Trim() <> "" Then
                        If OldBankID <> txtAccountNumber.Text().Trim() Then
                            If psp.BankID_HasExist(txtAccountNumber.Text()) > 0 Then
                                args.IsValid = False
                                Me.ShowMessage(Translate("Số tài khoản đã tồn tại"), Utilities.NotifyType.Error)
                            Else
                                args.IsValid = True
                            End If
                        End If
                    Else
                        args.IsValid = True
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hfOrg.Value = e.CurrentValue
                txtLocationCODE.Text = orgItem.CODE
                txtLocationVN.Text = orgItem.NAME_VN
                txtLocationEn.Text = orgItem.NAME_EN
                txtLocationShort.Text = orgItem.SHORT_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub LoadEmpInfor(ByVal index As Integer, ByVal emp_id As Integer, ByVal location As LocationDTO)
        Dim rep_emp As New CommonRepository
        Dim get_nation As DataTable
        Dim nation As String
        Try
            If index = 1 Then
                Dim emp_law = rep_emp.GetEmployeeID(location.EMP_LAW_ID)
                If emp_law IsNot Nothing Then
                    hfLawAgent.Value = emp_law.ID
                    txtLawAgentId.Text = emp_law.FULLNAME_VN
                    txtLawAgentTitle.Text = emp_law.TITLE_NAME
                    get_nation = psp.EmployeeCV_GetInfo(emp_law.EMPLOYEE_ID)
                    If get_nation IsNot Nothing And get_nation.Rows.Count > 0 Then
                        nation = get_nation.Rows(0)("NATION_NAME_VN").ToString()
                        txtLawAgentNationality.Text = nation
                    End If
                End If
            Else
                Dim emp_sign = rep_emp.GetEmployeeID(emp_id)
                If emp_sign IsNot Nothing Then
                    hfSignUpAgent.Value = emp_sign.ID
                    txtSignupAgent.Text = emp_sign.FULLNAME_VN
                    txtSignupAgentTitle.Text = emp_sign.TITLE_NAME
                    get_nation = psp.EmployeeCV_GetInfo(emp_sign.EMPLOYEE_ID)
                    If get_nation IsNot Nothing And get_nation.Rows.Count > 0 Then
                        nation = get_nation.Rows(0)("NATION_NAME_VN").ToString()
                        txtSigupAgentNationality.Text = nation
                    End If
                End If
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
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

    Private Sub loadDatasource(ByVal AttachID As Decimal, ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                If AttachID = 0 Then
                    txtUploadFile_LG.Text = Down_File
                    txtUpload_LG.Text = strUpload
                ElseIf AttachID = 1 Then
                    txtUploadFile_HD.Text = Down_File
                    txtUpload_HD.Text = strUpload
                Else
                    txtUploadFile_FT.Text = Down_File
                    txtUpload_FT.Text = strUpload
                End If
            Else
                strUpload = String.Empty
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class