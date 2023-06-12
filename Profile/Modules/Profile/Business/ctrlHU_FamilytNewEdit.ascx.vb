Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_FamilytNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSalaryPopup As ctrlFindSalaryPopup
    Public Overrides Property MustAuthorize As Boolean = True

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"
    Public Property popupId As String
    Public Property AjaxManagerId As String
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

    Property Family As FamilyDTO
        Get
            Return ViewState(Me.ID & "_Family")
        End Get
        Set(ByVal value As FamilyDTO)
            ViewState(Me.ID & "_Family") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property code_timekeeping As Integer
        Get
            Return ViewState(Me.ID & "_code_timekeeping")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_code_timekeeping") = value
        End Set
    End Property
    Property start_rankid As Integer
        Get
            Return ViewState(Me.ID & "_start_rankid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_start_rankid") = value
        End Set
    End Property
    Property object_labour As Integer
        Get
            Return ViewState(Me.ID & "_object_labour")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_object_labour") = value
        End Set
    End Property
    Property direct_manager As Integer
        Get
            Return ViewState(Me.ID & "_direct_manager")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_direct_manager") = value
        End Set
    End Property
    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property Is_dis As String
        Get
            Return ViewState(Me.ID & "_Is_dis")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Is_dis") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            If (isPopup) Then
                btnEmployee.Visible = False
            End If
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()

            If (_flag = False) Then
                EnableControlAll_Cus(False, LeftPane)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 06/07/2017 14:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
                'ViewConfig(LeftPane)
                'GirdConfig(rgAllow)
            End If
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarFamily

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            Dim use As New ProfileRepositoryBase

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Family = rep.GetFamilyByID(hidID.Value)
                    If Family IsNot Nothing Then
                        hidID.Value = Family.ID.ToString
                        hidEmployeeID.Value = Family.EMPLOYEE_ID.ToString
                        txtEmployeeCode.Text = Family.EMPLOYEE_CODE
                        txtEmployeeName.Text = Family.EMPLOYEE_NAME
                        txtOrg_Name.Text = Family.ORG_NAME

                        txtIDNO.Text = Family.ID_NO
                        txtFullName.Text = Family.FULLNAME
                        txtRemark.Text = Family.REMARK
                        txtTax.Text = Family.TAXTATION
                        If Family.BIRTH_DATE IsNot Nothing Then
                            rdBirthDate.SelectedDate = Family.BIRTH_DATE
                        End If
                        If Family.IS_DEDUCT IsNot Nothing Then
                            chkIsDeduct.Checked = Family.IS_DEDUCT
                            chkIsDeduct_CheckedChanged(Nothing, Nothing)
                        End If
                        If Family.IS_DEDUCT Then
                            rdDeductFrom.Enabled = True
                            rdDeductTo.Enabled = True
                            rdDeductReg.Enabled = True
                            txtTax.Enabled = True
                        Else
                            rdDeductReg.Enabled = False
                            rdDeductFrom.Enabled = False
                            rdDeductTo.Enabled = False
                            txtTax.Enabled = True
                        End If
                        If Family.IS_SAME_COMPANY IsNot Nothing Then
                            chkis_same_company.Checked = Family.IS_SAME_COMPANY
                        End If
                        If Family.DEDUCT_REG IsNot Nothing Then
                            rdDeductReg.SelectedDate = Family.DEDUCT_REG
                        End If
                        If Family.DEDUCT_FROM IsNot Nothing Then
                            rdDeductFrom.SelectedDate = Family.DEDUCT_FROM
                        End If
                        If Family.DEDUCT_TO IsNot Nothing Then
                            rdDeductTo.SelectedDate = Family.DEDUCT_TO
                        End If
                        If Family.RELATION_ID IsNot Nothing Then
                            cboRelationship.SelectedValue = Family.RELATION_ID
                            cboRelationship.Text = Family.RELATION_NAME
                        End If
                        ''''''''''''''''''''''''
                        If Family.RELATE_OWNER IsNot Nothing Then
                            cboRELATE_OWNER.SelectedValue = Family.RELATE_OWNER
                            cboRELATE_OWNER.Text = Family.RELATE_OWNER_NAME
                        End If
                        If Family.NATIVE IsNot Nothing Then
                            cboNATIVE.SelectedValue = Family.NATIVE
                            cboNATIVE.Text = Family.NATIVE_NAME
                        End If


                        If Family.PROVINCE_ID IsNot Nothing Then
                            cboNguyenQuan.SelectedValue = Family.PROVINCE_ID
                            cboNguyenQuan.Text = Family.PROVINCE_NAME
                        End If
                        txtCareer.Text = Family.CAREER
                        txtTITLE.Text = Family.TITLE_NAME

                        txtAdress1.Text = Family.ADDRESS
                        txtAdress_TT.Text = Family.ADDRESS_TT
                        txtMaHoGiaDinh.Text = Family.CERTIFICATE_CODE
                        txtSoHoKhau.Text = Family.CERTIFICATE_NUM
                        txtHamlet1.Text = Family.AD_VILLAGE
                        chkHousehold.Checked = Family.IS_OWNER
                        If Family.IS_OWNER Then
                            txtMaHoGiaDinh.Enabled = True
                            txtSoHoKhau.Enabled = True
                        Else
                            txtMaHoGiaDinh.Enabled = False
                            txtSoHoKhau.Enabled = False
                        End If
                        'If rep.CheckChuho(hidEmployeeID.Value) > 1 Then
                        '    chkHousehold.Checked = False
                        '    chkHousehold.Enabled = False
                        '    txtMaHoGiaDinh.Enabled = False
                        '    txtSoHoKhau.Enabled = False
                        'End If
                        chkDaMat.Checked = Family.IS_PASS

                        Using rep1 As New ProfileRepository
                            If IsNumeric(Family.AD_PROVINCE_ID) Then
                                Dim dt As DataTable = rep1.GetProvinceList(False)
                                FillRadCombobox(cboProvince_City1, dt, "NAME", "ID")
                                cboProvince_City1.SelectedValue = Family.AD_PROVINCE_ID
                                cboProvince_City1.Text = Family.AD_PROVINCE_NAME
                            End If
                            If IsNumeric(Family.TT_PROVINCE_ID) Then
                                Dim dt As DataTable = rep1.GetProvinceList(False)
                                FillRadCombobox(cboProvince_City2, dt, "NAME", "ID")
                                cboProvince_City2.SelectedValue = Family.TT_PROVINCE_ID
                                cboProvince_City2.Text = Family.TT_PROVINCE_NAME
                            End If
                            If cboProvince_City1.SelectedValue <> "" Then
                                Dim dt As DataTable = rep1.GetDistrictList(cboProvince_City1.SelectedValue, False)
                                FillRadCombobox(cboDistrict1, dt, "NAME", "ID")
                            End If
                            If IsNumeric(Family.AD_DISTRICT_ID) Then
                                cboDistrict1.SelectedValue = Family.AD_DISTRICT_ID
                                cboDistrict1.Text = Family.AD_DISTRICT_NAME
                            End If
                            If cboDistrict1.SelectedValue <> "" Then
                                Dim dt As DataTable = rep1.GetWardList(cboDistrict1.SelectedValue, False)
                                FillRadCombobox(cboCommune1, dt, "NAME", "ID")
                            End If
                            If IsNumeric(Family.AD_WARD_ID) Then
                                cboCommune1.SelectedValue = Family.AD_WARD_ID
                                cboCommune1.Text = Family.AD_WARD_NAME
                            End If
                            If cboProvince_City2.SelectedValue <> "" Then
                                Dim dt As DataTable = rep1.GetDistrictList(cboProvince_City2.SelectedValue, False)
                                FillRadCombobox(cboDistrict2, dt, "NAME", "ID")
                            End If
                            If IsNumeric(Family.TT_DISTRICT_ID) Then
                                cboDistrict2.SelectedValue = Family.TT_DISTRICT_ID
                                cboDistrict2.Text = Family.TT_DISTRICT_NAME
                            End If
                            If cboDistrict2.SelectedValue <> "" Then
                                Dim dt As DataTable = rep1.GetWardList(cboDistrict2.SelectedValue, False)
                                FillRadCombobox(cboCommune2, dt, "NAME", "ID")
                            End If
                            If IsNumeric(Family.TT_WARD_ID) Then
                                cboCommune2.SelectedValue = Family.TT_WARD_ID
                                cboCommune2.Text = Family.TT_WARD_NAME
                            End If

                            If IsNumeric(Family.NATION_ID) Then
                                cboNationlity.SelectedValue = Family.NATION_ID
                            End If
                            If Family.NATION_ID = 0 Or Family.NATION_ID Is Nothing Then
                                ClearControlValue(cboNationlity)
                            End If

                            If IsNumeric(Family.BIRTH_NATION_ID) Then
                                cboNATIONALITYFAMILY.SelectedValue = Family.BIRTH_NATION_ID
                            End If
                            If Family.BIRTH_NATION_ID = 0 Or Family.BIRTH_NATION_ID Is Nothing Then
                                ClearControlValue(cboNATIONALITYFAMILY)
                            End If

                            If IsNumeric(Family.BIRTH_PROVINCE_ID) Then
                                cbTempKtPROVINCE_ID.SelectedValue = Family.BIRTH_PROVINCE_ID
                                Dim dt As DataTable = rep1.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                                FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                            End If


                            If IsNumeric(Family.BIRTH_DISTRICT_ID) Then
                                cbTempKtDISTRICT_ID.SelectedValue = Family.BIRTH_DISTRICT_ID
                            End If

                            If IsNumeric(Family.BIRTH_WARD_ID) Then
                                Dim dt As DataTable = rep1.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                                FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                                cbTempKtWARD_ID.SelectedValue = Family.BIRTH_WARD_ID
                            End If
                            If Family.GENDER IsNot Nothing Then
                                cboGender.SelectedValue = Family.GENDER
                            End If
                            If Family.ID_NO_DATE IsNot Nothing Then
                                rdIDDate.SelectedDate = Family.ID_NO_DATE
                            End If
                            txtIDPlace.Text = Family.ID_NO_PLACE_NAME
                            txtPhone.Text = Family.PHONE
                            If Family.TAXTATION_DATE IsNot Nothing Then
                                rdMSTDate.SelectedDate = Family.TAXTATION_DATE
                            End If
                            txt_MSTPLACE.Text = Family.TAXTATION_PLACE
                            txtBIRTH_CODE.Text = Family.BIRTH_CODE
                            txtQuyen.Text = Family.QUYEN
                        End Using

                        txtUploadFileNPT.Text = Family.UPLOAD_FILE_NPT
                        txtUploadNPT.Text = Family.FILE_NPT
                        If txtUploadNPT.Text <> "" Then
                            btnDownloadNPT.Visible = True
                            If txtUploadNPT.Text.ToUpper.Contains(".JPG") Or Family.FILE_NPT.ToUpper.Contains(".GIF") Or Family.FILE_NPT.ToUpper.Contains(".PNG") Or Family.FILE_NPT.ToUpper.Contains(".JPEG") Then
                                btnViewNPT.Visible = True
                                Dim file = rep.GetFileForView(txtUploadFileNPT.Text)
                                Dim link As String = file.LINK & "\" & file.FILE_NAME
                                link = link.Replace("\", "(slash)")
                                hidLinkNPT.Value = link
                            Else
                                btnViewNPT.Visible = False
                            End If
                        Else
                            btnDownloadNPT.Visible = False
                        End If

                        txtUploadFileFamily.Text = Family.UPLOAD_FILE_FAMILY
                        txtUploadFamily.Text = Family.FILE_FAMILY
                        If txtUploadFamily.Text <> "" Then
                            btnDownloadFamily.Visible = True
                            If txtUploadFamily.Text.ToUpper.Contains(".JPG") Or Family.FILE_FAMILY.ToUpper.Contains(".GIF") Or Family.FILE_FAMILY.ToUpper.Contains(".PNG") Or Family.FILE_FAMILY.ToUpper.Contains(".JPEG") Then
                                btnViewFamily.Visible = True
                                Dim file = rep.GetFileForView(txtUploadFileFamily.Text)
                                Dim link As String = file.LINK & "\" & file.FILE_NAME
                                link = link.Replace("\", "(slash)")
                                hidLinkFamily.Value = link
                            Else
                                btnViewFamily.Visible = False
                            End If
                        Else
                            btnDownloadFamily.Visible = False
                        End If
                        'If Family.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                        '    MainToolBar.Items(0).Enabled = False
                        'End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    Dim dt As New DataTable
                    If Not IsPostBack Then
                        If Request.Params("empID") IsNot Nothing Then
                            Dim item = rep.GetContractEmployeeByID(CDec(Request.Params("empID")))
                            If item IsNot Nothing Then
                                If item.WORK_STATUS IsNot Nothing Then
                                    hidWorkStatus.Value = item.WORK_STATUS
                                End If
                                If IsNumeric(item.ID.ToString) Then
                                    hidEmployeeID.Value = item.ID.ToString
                                End If

                                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                                txtEmployeeName.Text = item.FULLNAME_VN
                                txtTITLE.Text = item.TITLE_NAME_VN
                                txtOrg_Name.Text = item.ORG_NAME
                                Dim employeeId As Double = 0
                                Double.TryParse(hidEmployeeID.Value, employeeId)
                            End If
                        End If
                    End If
                    'rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                    'rgAllow.DataBind()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 06/07/2017 15:41
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objFamily As New FamilyDTO
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        'Dim stt As OtherListDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()

                    If Page.IsValid Then
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        Dim employee = rep.GetEmployeeByID(Decimal.Parse(hidEmployeeID.Value))

                        If chkIsDeduct.Checked = True And rdDeductFrom.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Chưa chọn ngày giảm trừ"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtFullName.Text = "" Then
                            ShowMessage(Translate("Bạn phải nhập Họ tên"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If cboRelationship.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải nhập Mối quan hệ "), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rdBirthDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải nhập Ngày sinh"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rdDeductFrom.SelectedDate > rdDeductTo.SelectedDate Then
                            ShowMessage(Translate("Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.CheckChuho(hidEmployeeID.Value, 0) > 0 And chkHousehold.Checked Then
                                    ShowMessage(Translate("Nhân viên đã có chủ hộ"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If Execute() Then
                                    'If (isPopup) Then
                                    '    Response.Redirect("/Dialog.aspx?mid=Profile&fid=ctrlHU_FamilytNewEdit&group=Business&empID=" & hidEmployeeID.Value)
                                    'Else
                                    '    Session("Result") = 1
                                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Family&group=Business")
                                    'End If
                                    '  ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Family&group=Business")
                                    End If

                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If rep.CheckChuho(hidEmployeeID.Value, hidID.Value) > 0 And chkHousehold.Checked Then
                                    ShowMessage(Translate("Nhân viên đã có chủ hộ"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If Execute() Then
                                    'If (isPopup) Then
                                    '    Dim str As String = "getRadWindow().close('1');"
                                    '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    'Else
                                    '    Session("Result") = 1
                                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Family&group=Business")
                                    'End If
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_FamilytNewEdit&group=Business&noscroll=1&empID=" & hidEmployeeID.Value)
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Family&group=Business")
                                    End If
                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Dim str As String = "for (var i = 0; i < document.getElementsByClassName('rtbBtn').length; i++) { if (document.getElementsByClassName('rtbBtn')[i].outerText == 'Lưu') { document.getElementsByClassName('rtbBtn')[i].style.pointerEvents = 'auto' break; }};"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Exit Sub
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Family&group=Business")
                    End If
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function Execute() As Boolean
        Try
            Dim objFamily As New FamilyDTO
            Dim rep As New ProfileBusinessRepository
            objFamily.EMPLOYEE_ID = hidEmployeeID.Value
            objFamily.BIRTH_DATE = rdBirthDate.SelectedDate
            objFamily.DEDUCT_REG = rdDeductReg.SelectedDate
            objFamily.TAXTATION = txtTax.Text
            objFamily.DEDUCT_FROM = rdDeductFrom.SelectedDate()
            objFamily.DEDUCT_TO = rdDeductTo.SelectedDate()
            objFamily.FULLNAME = txtFullName.Text.Trim()
            objFamily.ID_NO = txtIDNO.Text.Trim()
            objFamily.IS_DEDUCT = chkIsDeduct.Checked
            objFamily.IS_SAME_COMPANY = chkis_same_company.Checked
            objFamily.REMARK = txtRemark.Text.Trim()
            objFamily.CAREER = txtCareer.Text.Trim()
            objFamily.TITLE_NAME = txtTITLE.Text.Trim()
            objFamily.AD_VILLAGE = txtHamlet1.Text.Trim()
            objFamily.CERTIFICATE_CODE = txtMaHoGiaDinh.Text
            objFamily.CERTIFICATE_NUM = txtSoHoKhau.Text
            objFamily.IS_OWNER = chkHousehold.Checked
            objFamily.IS_PASS = chkDaMat.Checked
            objFamily.ADDRESS = txtAdress1.Text
            objFamily.ADDRESS_TT = txtAdress_TT.Text

            objFamily.FILE_FAMILY = txtUploadFileFamily.Text
            objFamily.FILE_NPT = txtUploadFileNPT.Text
            If cboRelationship.SelectedValue <> "" Then
                objFamily.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
            End If
            ''''''''''''
            If cboRELATE_OWNER.SelectedValue <> "" Then
                objFamily.RELATE_OWNER = Decimal.Parse(cboRELATE_OWNER.SelectedValue)
            End If
            If cboNATIVE.SelectedValue <> "" Then
                objFamily.NATIVE = Decimal.Parse(cboNATIVE.SelectedValue)
            End If
            ''''''''''''''''''''''''''''''''''

            If cboNguyenQuan.SelectedValue <> "" Then
                objFamily.PROVINCE_ID = Decimal.Parse(cboNguyenQuan.SelectedValue)
            End If

            If cboProvince_City1.SelectedValue <> "" Then
                objFamily.AD_PROVINCE_ID = Decimal.Parse(cboProvince_City1.SelectedValue)
            End If
            If cboDistrict1.SelectedValue <> "" Then
                objFamily.AD_DISTRICT_ID = Decimal.Parse(cboDistrict1.SelectedValue)
            End If
            If cboCommune1.SelectedValue <> "" Then
                objFamily.AD_WARD_ID = Decimal.Parse(cboCommune1.SelectedValue)
            End If
            If cboProvince_City2.SelectedValue <> "" Then
                objFamily.TT_PROVINCE_ID = Decimal.Parse(cboProvince_City2.SelectedValue)
            End If
            If cboDistrict2.SelectedValue <> "" Then
                objFamily.TT_DISTRICT_ID = Decimal.Parse(cboDistrict2.SelectedValue)
            End If
            If cboCommune2.SelectedValue <> "" Then
                objFamily.TT_WARD_ID = Decimal.Parse(cboCommune2.SelectedValue)
            End If


            If cboGender.SelectedValue <> "" Then
                objFamily.GENDER = cboGender.SelectedValue
            End If

            'If cboNationlity.SelectedValue <> "" Then
            objFamily.NATION_ID = CDec(Val(cboNationlity.SelectedValue))
            'End If

            objFamily.ID_NO_DATE = rdIDDate.SelectedDate
            objFamily.ID_NO_PLACE_NAME = txtIDPlace.Text
            objFamily.PHONE = txtPhone.Text
            objFamily.TAXTATION_DATE = rdMSTDate.SelectedDate
            objFamily.TAXTATION_PLACE = txt_MSTPLACE.Text
            objFamily.BIRTH_CODE = txtBIRTH_CODE.Text
            objFamily.QUYEN = txtQuyen.Text

            'If cboNATIONALITYFAMILY.SelectedValue <> "" Then
            objFamily.BIRTH_NATION_ID = CDec(Val(cboNATIONALITYFAMILY.SelectedValue))
            'End If

            If cbTempKtPROVINCE_ID.SelectedValue <> "" Then
                objFamily.BIRTH_PROVINCE_ID = cbTempKtPROVINCE_ID.SelectedValue
            End If

            If cbTempKtDISTRICT_ID.SelectedValue <> "" Then
                objFamily.BIRTH_DISTRICT_ID = cbTempKtDISTRICT_ID.SelectedValue
            End If

            If cbTempKtWARD_ID.SelectedValue <> "" Then
                objFamily.BIRTH_WARD_ID = cbTempKtWARD_ID.SelectedValue
            End If



            Dim gID As Decimal
            If hidID.Value = "" Then
                rep.InsertEmployeeFamily(objFamily, gID)
            Else
                'objFamily.ID = Decimal.Parse(hidFamilyID.Value)
                'rep.ModifyEmployeeFamily(objFamily, gID)

                objFamily.ID = Decimal.Parse(hidID.Value)

                'Dim objFamilyEdit As New FamilyDTO
                'objFamilyEdit.ID = objFamily.ID
                'Dim list = rep.GetEmployeeFamily(objFamilyEdit)
                'If list.Count > 0 Then
                '    rep.ModifyEmployeeFamily(objFamily, gID)
                'Else
                '    ShowMessage("Dữ liệu không tồn tại!", Utilities.NotifyType.Warning)
                'End If
                rep.ModifyEmployeeFamily(objFamily, gID)

            End If

            rep.Dispose()
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <lastupdate>
    ''' 06/07/2017 17:34
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            'LoadPopup(1)
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindSigner.CancelClicked, ctrlFindSigner2.CancelClicked,
        ctrlFindEmployeePopup.CancelClicked,
        ctrlFindSalaryPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSigner_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSignPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID.ToString
                'txtSignTitle.Text = item.TITLE_NAME
                'txtSigner.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindSignPopup2_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign2.Value = item.ID.ToString
                'txtSignTitle2.Text = item.TITLE_NAME
                'txtSignName2.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If chkHousehold.Checked Then
                cboNationlity.ClearSelection()
                ClearControlValue(txtSoHoKhau, txtAdress1, cboProvince_City1, cboDistrict1, cboCommune1)
            End If
            If lstEmpID.Count <> 0 Then
                FillData(lstEmpID(0))
                If chkHousehold.Checked Then
                    FillAddressByEmp(lstEmpID(0))
                End If
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cua control ctrlMessageBox_Button
    ''' Neu command là item xoa thi cap nhat lai trang thai hien tai la xoa
    ''' Cap nhat lai trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub txtEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Dim rep_PROFILE As New ProfileBusinessRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0).EMPLOYEE_ID
                        If chkHousehold.Checked Then
                            cboNationlity.ClearSelection()
                            ClearControlValue(txtSoHoKhau, txtAdress1, cboProvince_City1, cboDistrict1, cboCommune1)
                        End If
                        FillData(empID)
                        If chkHousehold.Checked Then
                            FillAddressByEmp(empID)
                        End If
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            ctrlFindEmployeePopup.MustHaveContract = True
                            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Reset_Find_Emp()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            hidWorkStatus.Value = Nothing
            hidEmployeeID.Value = Nothing
            'txtEmployeeCode.Text = ""
            txtEmployeeName.Text = ""
            txtTITLE.Text = ""
            'txtSTAFF_RANK.Text = item.STAFF_RANK_NAME
            txtOrg_Name.Text = ""

            Dim employeeId As Double = 0
            cboNationlity.ClearSelection()
            ClearControlValue(txtSoHoKhau, txtAdress1, cboProvince_City1, cboDistrict1, cboCommune1)
            Double.TryParse(hidEmployeeID.Value, employeeId)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnUploadFileFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileFamily.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownloadFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadFamily.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUploadFamily.Text <> "" Then
                Dim fileObj As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    fileObj = rep.GetFileForView(txtUploadFileFamily.Text)
                End Using
                Dim link = fileObj.LINK
                Dim name = fileObj.FILE_NAME
                Dim path = link & name
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
                Response.WriteFile(file.FullName)
                Response.End()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            If ctrlUpload2.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload2.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
                    Dim fileContent As Byte() = New Byte(file.ContentLength) {}

                    Dim buffer As Byte() = New Byte(file.ContentLength - 1) {}
                    Using str As Stream = file.InputStream
                        str.Read(buffer, 0, buffer.Length)
                    End Using
                    Dim guidID = Guid.NewGuid.ToString()
                    Dim obj As New FileUploadDTO
                    obj.FILE_NAME = file.FileName
                    obj.CODE_PATH = "PORTALFAMILYEDIT"
                    obj.NAME = guidID
                    If Not rep.AddFileUpload(obj, buffer) Then
                        txtUploadFamily.Text = file.FileName
                        txtUploadFileFamily.Text = obj.NAME
                        btnDownloadFamily.Visible = True
                        If obj.FILE_NAME.ToUpper.Contains(".JPG") Or obj.FILE_NAME.ToUpper.Contains(".GIF") Or obj.FILE_NAME.ToUpper.Contains(".PNG") Or obj.FILE_NAME.ToUpper.Contains(".JPEG") Then
                            btnViewFamily.Visible = True
                            Dim fileObj = rep.GetFileForView(txtUploadFileFamily.Text)
                            Dim link As String = fileObj.LINK & "\" & fileObj.FILE_NAME
                            link = link.Replace("\", "(slash)")
                            hidLinkFamily.Value = link
                        Else
                            btnViewFamily.Visible = False
                        End If
                    End If
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Upload file bị lỗi"), NotifyType.Error)
        End Try
    End Sub
    Private Sub btnUploadFileNPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileNPT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownloadNPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadNPT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUploadNPT.Text <> "" Then
                Dim fileObj As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    fileObj = rep.GetFileForView(txtUploadFileNPT.Text)
                End Using
                Dim link = fileObj.LINK
                Dim name = fileObj.FILE_NAME
                Dim path = link & name
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
                Response.WriteFile(file.FullName)
                Response.End()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    'Private Sub btnViewFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewFamily.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim fileName As String = txtUploadFileFamily.Text
    '        Dim file As New FileUploadDTO
    '        Using rep As New ProfileBusinessRepository
    '            file = rep.GetFileForView(fileName)
    '        End Using
    '        Dim link As String = file.LINK & "\" & file.FILE_NAME
    '        Dim strName As String = IO.Path.GetExtension(link).ToUpper()

    '        link = link.Replace("\", "(slash)")
    '        If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
    '            Show(link)
    '        Else
    '            ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub btnViewNPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewNPT.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim fileName As String = txtUploadFileNPT.Text
    '        Dim file As New FileUploadDTO
    '        Using rep As New ProfileBusinessRepository
    '            file = rep.GetFileForView(fileName)
    '        End Using
    '        Dim link As String = file.LINK & "\" & file.FILE_NAME
    '        Dim strName As String = IO.Path.GetExtension(link).ToUpper()

    '        link = link.Replace("\", "(slash)")
    '        If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
    '            Show(link)
    '        Else
    '            ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Public Sub Show(strfile As Object)
    '    Dim script As String
    '    script = "var oWnd = $find('" & popupId & "');"
    '    script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
    '    script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
    '    script &= "oWnd.show();"
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    'End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    Dim fileContent As Byte() = New Byte(file.ContentLength) {}

                    Dim buffer As Byte() = New Byte(file.ContentLength - 1) {}
                    Using str As Stream = file.InputStream
                        str.Read(buffer, 0, buffer.Length)
                    End Using
                    Dim guidID = Guid.NewGuid.ToString()
                    Dim obj As New FileUploadDTO
                    obj.FILE_NAME = file.FileName
                    obj.CODE_PATH = "PORTALFAMILYEDIT"
                    obj.NAME = guidID
                    If Not rep.AddFileUpload(obj, buffer) Then
                        txtUploadNPT.Text = file.FileName
                        txtUploadFileNPT.Text = obj.NAME
                        btnDownloadNPT.Visible = True
                        If obj.FILE_NAME.ToUpper.Contains(".JPG") Or obj.FILE_NAME.ToUpper.Contains(".GIF") Or obj.FILE_NAME.ToUpper.Contains(".PNG") Or obj.FILE_NAME.ToUpper.Contains(".JPEG") Then
                            btnViewNPT.Visible = True
                            Dim fileObj = rep.GetFileForView(txtUploadFileNPT.Text)
                            Dim link As String = fileObj.LINK & "\" & fileObj.FILE_NAME
                            link = link.Replace("\", "(slash)")
                            hidLinkNPT.Value = link
                        Else
                            btnViewNPT.Visible = False
                        End If
                    End If
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Upload file bị lỗi"), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"



    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm cập nhật trạng thái của các control trên page
    ''' Xử lý đăng ký popup ứng với giá trị isLoadPopup
    ''' </summary>
    ''' <remarks></remarks>

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            btnEmployee.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                    'EnabledGridNotPostback(rgMain, False)
            End Select
            LoadPopup(isLoadPopup)
            If (hidID.Value = "") Then
                If _toolbar Is Nothing Then Exit Sub
                Dim item As RadToolBarButton
                For i = 0 To _toolbar.Items.Count - 1
                    item = CType(_toolbar.Items(i), RadToolBarButton)
                    'Select Case CurrentState
                    '    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    If item.CommandName = "UNLOCK" Then
                        item.Enabled = False
                    End If
                    'End Select
                Next
            End If
            If Is_dis = "dis_emp" Then
                EnableControlAll(False, txtEmployeeCode, btnEmployee)
            End If
            If txtUploadNPT.Text <> "" Then
                btnDownloadNPT.Visible = True
                If txtUploadNPT.Text.ToUpper.Contains(".JPG") Or txtUploadNPT.Text.ToUpper.Contains(".GIF") Or txtUploadNPT.Text.ToUpper.Contains(".PNG") Or txtUploadNPT.Text.ToUpper.Contains(".JPEG") Then
                    btnViewNPT.Visible = True
                Else
                    btnViewNPT.Visible = False
                End If
            Else
                btnDownloadNPT.Visible = False
                btnViewNPT.Visible = False
            End If
            If txtUploadFamily.Text <> "" Then
                btnDownloadFamily.Visible = True
                If txtUploadFamily.Text.ToUpper.Contains(".JPG") Or txtUploadFamily.Text.ToUpper.Contains(".GIF") Or txtUploadFamily.Text.ToUpper.Contains(".PNG") Or txtUploadFamily.Text.ToUpper.Contains(".JPEG") Then
                    btnViewFamily.Visible = True
                Else
                    btnViewFamily.Visible = False
                End If
            Else
                btnDownloadFamily.Visible = False
                btnViewFamily.Visible = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' cboFamilyType, cboStatus
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Using rep As New ProfileRepository
            If ComboBoxDataDTO Is Nothing Then
                ComboBoxDataDTO = New ComboBoxDataDTO
                ComboBoxDataDTO.GET_RELATION = True
                ComboBoxDataDTO.GET_PROVINCE = True
                ComboBoxDataDTO.GET_DISTRICT = True
                ComboBoxDataDTO.GET_NATION = True
                ComboBoxDataDTO.GET_WARD = True
                ComboBoxDataDTO.GET_RELATE_OWNER = True
                ComboBoxDataDTO.GET_NATIVE = True
                rep.GetComboList(ComboBoxDataDTO)
            End If

            If ComboBoxDataDTO IsNot Nothing Then
                FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                FillDropDownList(cboNguyenQuan, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNguyenQuan.SelectedValue)
                FillDropDownList(cboProvince_City1, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince_City1.SelectedValue)
                FillDropDownList(cboDistrict1, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboDistrict1.SelectedValue)
                FillDropDownList(cboCommune1, ComboBoxDataDTO.LIST_WARD, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboCommune1.SelectedValue)
                FillDropDownList(cboProvince_City2, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince_City2.SelectedValue)
                FillDropDownList(cboDistrict2, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboDistrict2.SelectedValue)
                FillDropDownList(cboCommune2, ComboBoxDataDTO.LIST_WARD, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboCommune2.SelectedValue)

                FillDropDownList(cbTempKtPROVINCE_ID, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempKtPROVINCE_ID.SelectedValue)
                FillDropDownList(cboNationlity, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationlity.SelectedValue)
                FillDropDownList(cboNATIONALITYFAMILY, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNATIONALITYFAMILY.SelectedValue)

                FillDropDownList(cboRELATE_OWNER, ComboBoxDataDTO.LIST_RELATE_OWNER, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRELATE_OWNER.SelectedValue)
                FillDropDownList(cboNATIVE, ComboBoxDataDTO.LIST_NATIVE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNATIVE.SelectedValue)


                cboNationlity.SelectedValue = 244
                cboNATIONALITYFAMILY.SelectedValue = 244
            End If

            If Not IsPostBack Then
                cboGender.DataSource = rep.GetOtherList("GENDER", True)
                cboGender.DataTextField = "NAME"
                cboGender.DataValueField = "ID"
                cboGender.DataBind()
            End If

        End Using
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "IDSelect"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "EmpID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                'cboFamilyType.AutoPostBack = True
                If Request.Params("IDSelect") IsNot Nothing Then
                    hidID.Value = Request.Params("IDSelect")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
                If Request.Params("EmpID") IsNot Nothing Then
                    FillData(Request.Params("EmpID"))
                End If
                If Request.Params("Is_dis") IsNot Nothing Then
                    Is_dis = Request.Params("Is_dis")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức fill dữ liệu cho page
    ''' theo các trạng thái maintoolbar và trạng thái item trên trang
    ''' </summary>
    ''' <param name="empID"></param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Using rep As New ProfileBusinessRepository
                Dim item = rep.GetContractEmployeeByID(empID)
                'rdStartDate.MaxDate = Date.MaxValue
                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    MainToolBar.Items(0).Enabled = False
                Else
                    MainToolBar.Items(0).Enabled = True

                End If

                If item.WORK_STATUS IsNot Nothing Then
                    hidWorkStatus.Value = item.WORK_STATUS
                End If
                If IsNumeric(item.ID.ToString) Then
                    hidEmployeeID.Value = item.ID.ToString
                End If

                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTITLE.Text = item.TITLE_NAME_VN
                'txtSTAFF_RANK.Text = item.STAFF_RANK_NAME
                txtOrg_Name.Text = item.ORG_NAME

                Dim employeeId As Double = 0
                Double.TryParse(hidEmployeeID.Value, employeeId)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 1
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = True
                End If
            Case 2
                If Not FindSigner.Controls.Contains(ctrlFindSigner) Then
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                End If

            Case 3
                If Not FindSalary.Controls.Contains(ctrlFindSalaryPopup) Then
                    ctrlFindSalaryPopup = Me.Register("ctrlFindSalaryPopup", "Profile", "ctrlFindSalaryPopup", "Shared")
                    ctrlFindSalaryPopup.MultiSelect = False
                    If hidEmployeeID.Value <> "" Then
                        ctrlFindSalaryPopup.EmployeeID = Decimal.Parse(hidEmployeeID.Value)
                        Session("EmployeeID") = Decimal.Parse(hidEmployeeID.Value)
                    End If

                    FindSalary.Controls.Add(ctrlFindSalaryPopup)
                    ctrlFindSalaryPopup.Show()
                End If
            Case 4
                If Not FindSigner.Controls.Contains(ctrlFindSigner2) Then
                    ctrlFindSigner2 = Me.Register("ctrlFindSigner2", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner2)
                    ctrlFindSigner2.MultiSelect = False
                    ctrlFindSigner2.MustHaveContract = True
                    ctrlFindSigner2.LoadAllOrganization = True
                End If

        End Select
    End Sub
#End Region

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
 Handles cboProvince_City1.ItemsRequested, cboDistrict1.ItemsRequested, cboCommune1.ItemsRequested,
        cboProvince_City2.ItemsRequested, cboDistrict2.ItemsRequested, cboCommune2.ItemsRequested,
        cbTempKtPROVINCE_ID.ItemsRequested, cbTempKtDISTRICT_ID.ItemsRequested, cbTempKtWARD_ID.ItemsRequested

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
                Select Case sender.ID
                    Case cboProvince_City1.ID, cboProvince_City2.ID, cbTempKtPROVINCE_ID.ID
                        dtData = rep.GetProvinceList(True)
                    Case cboDistrict1.ID, cboDistrict2.ID, cbTempKtDISTRICT_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboCommune1.ID, cboCommune2.ID, cbTempKtWARD_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                End Select
                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    'If dtExist.Count = 0 Then
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
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub chkHousehold_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHousehold.CheckedChanged
        Try
            cboNationlity.ClearSelection()
            ClearControlValue(txtSoHoKhau, txtAdress1, cboProvince_City1, cboDistrict1, cboCommune1)
            If chkHousehold.Checked Then
                txtMaHoGiaDinh.Enabled = True
                txtSoHoKhau.Enabled = True
                If hidEmployeeID.Value <> "" Then
                    FillAddressByEmp(hidEmployeeID.Value)
                End If
            Else
                txtMaHoGiaDinh.Enabled = False
                txtSoHoKhau.Enabled = False
                ClearControlValue(txtMaHoGiaDinh, txtSoHoKhau)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub chkIsDeduct_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsDeduct.CheckedChanged
        If chkIsDeduct.Checked Then
            rdDeductFrom.Enabled = True
            rdDeductTo.Enabled = True
            rdDeductReg.Enabled = True
            reqDeductFrom.Enabled = True
            txtTax.Enabled = True
            hid_Isdeduct.Visible = True
        Else
            rdDeductReg.Enabled = False
            rdDeductFrom.Enabled = False
            rdDeductTo.Enabled = False
            reqDeductFrom.Enabled = False
            hid_Isdeduct.Visible = False
            ClearControlValue(rdDeductReg, rdDeductFrom, rdDeductTo)
            txtTax.Enabled = True
        End If
    End Sub
    Protected Sub rdDeductFrom_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdDeductFrom.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If IsDate(rdBirthDate.SelectedDate) AndAlso IsDate(rdDeductFrom.SelectedDate) Then
                Dim age As Double = 0
                If rdBirthDate.SelectedDate.Value.Month = rdDeductFrom.SelectedDate.Value.Month AndAlso rdBirthDate.SelectedDate.Value.Day = rdDeductFrom.SelectedDate.Value.Day Then
                    age = rdDeductFrom.SelectedDate.Value.Year - rdBirthDate.SelectedDate.Value.Year
                Else
                    age = DateDiff(DateInterval.Day, CDate(rdBirthDate.SelectedDate), CDate(rdDeductFrom.SelectedDate)) / 365.25
                End If
                If age >= 18 And age < 55 Then
                    ShowMessage("Người thân trên 18 tuổi hoặc chưa đến tuổi nghỉ hưu!", Utilities.NotifyType.Error)
                    ClearControlValue(rdDeductFrom)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub FillAddressByEmp(ByVal emp_id As Decimal)
        Try
            Dim store As New ProfileStoreProcedure
            Dim rep As New ProfileRepository
            Dim dtData = store.GET_EMP_PER_INFO(emp_id)
            Dim dtList As New DataTable
            'cboNationlity.ClearSelection()
            'ClearControlValue(txtSoHoKhau, txtAdress1, cboProvince_City1, cboDistrict1, cboCommune1)
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                txtSoHoKhau.Text = If(IsDBNull(dtData.Rows(0)("NO_HOUSEHOLDS")), "", dtData.Rows(0)("NO_HOUSEHOLDS"))
                txtAdress1.Text = If(IsDBNull(dtData.Rows(0)("PER_ADDRESS")), "", dtData.Rows(0)("PER_ADDRESS"))

                If Not IsDBNull(dtData.Rows(0)("NATIONALITY")) Then
                    cboNationlity.SelectedValue = CDec(dtData.Rows(0)("NATIONALITY").ToString)
                    dtList = rep.GetProvinceList1(CDec(dtData.Rows(0)("NATIONALITY").ToString), False)
                    FillRadCombobox(cboProvince_City1, dtList, "NAME", "ID")

                End If
                If Not IsDBNull(dtData.Rows(0)("PER_PROVINCE")) Then
                    cboProvince_City1.SelectedValue = CDec(dtData.Rows(0)("PER_PROVINCE").ToString)
                    dtList = rep.GetDistrictList(CDec(dtData.Rows(0)("PER_PROVINCE").ToString), False)
                    FillRadCombobox(cboDistrict1, dtList, "NAME", "ID")

                End If
                If Not IsDBNull(dtData.Rows(0)("PER_DISTRICT")) Then
                    cboDistrict1.SelectedValue = CDec(dtData.Rows(0)("PER_DISTRICT").ToString)
                    dtList = rep.GetWardList(CDec(dtData.Rows(0)("PER_DISTRICT").ToString), False)
                    FillRadCombobox(cboCommune1, dtList, "NAME", "ID")

                End If
                If Not IsDBNull(dtData.Rows(0)("PER_WARD")) Then
                    cboCommune1.SelectedValue = CDec(dtData.Rows(0)("PER_WARD").ToString)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub chkCopyEmployeeAddress_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCopyEmployeeAddress.CheckedChanged
        If chkCopyEmployeeAddress.Checked Then
            Using rep As New ProfileBusinessClient
                Dim Info As EmployeeCVDTO
                If hidEmployeeID.Value <> "" Then
                    Info = rep.GetEmployeePerInfo(Decimal.Parse(hidEmployeeID.Value))
                    txtAdress1.Text = Info.PER_ADDRESS
                    If Info.PER_PROVINCE IsNot Nothing Then
                        cboProvince_City1.SelectedValue = Info.PER_PROVINCE
                    End If
                    If Info.PER_DISTRICT IsNot Nothing Then
                        cboDistrict1.SelectedValue = Info.PER_DISTRICT
                    End If
                    If Info.PER_WARD IsNot Nothing Then
                        cboCommune1.SelectedValue = Info.PER_WARD
                    End If
                    txtAdress_TT.Text = Info.NAV_ADDRESS
                    If Info.NAV_PROVINCE IsNot Nothing Then
                        cboProvince_City2.SelectedValue = Info.NAV_PROVINCE
                    End If
                    If Info.NAV_DISTRICT IsNot Nothing Then
                        cboDistrict2.SelectedValue = Info.NAV_DISTRICT
                    End If
                    If Info.NAV_WARD IsNot Nothing Then
                        cboCommune2.SelectedValue = Info.NAV_WARD
                    End If
                End If
            End Using
        End If
    End Sub
End Class