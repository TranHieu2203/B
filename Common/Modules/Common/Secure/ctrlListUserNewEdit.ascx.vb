Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlListUserNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents UserViewList As ViewBase


    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"
    Property User As CommonBusiness.UserDTO
        Get
            Return PageViewState(Me.ID & "_User")
        End Get
        Set(ByVal value As CommonBusiness.UserDTO)
            PageViewState(Me.ID & "_User") = value
        End Set
    End Property
    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'If Page.IsPostBack Then
        '    Exit Sub
        'End If
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái control theo page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL, ""
                    txtEMAIL.ReadOnly = True
                    txtFULLNAME.ReadOnly = True
                    txtEMPLOYEE_CODE.ReadOnly = True
                    txtTELEPHONE.ReadOnly = True
                    txtUSERNAME.ReadOnly = True
                    txtPASSWORD.ReadOnly = True
                    txtPASSWORD_AGAIN.ReadOnly = True
                    'txtEMPLOYEE_CODE.ReadOnly = True
                    cbIS_AD.Enabled = False
                    cbIS_APP.Enabled = False
                    cbIS_PORTAL.Enabled = False
                    chkChangePass.Enabled = False
                    cbIs_HR.Enabled = False
                    chkRecAcc.Enabled = False
                    Utilities.EnableRadDatePicker(rdEFFECT_DATE, False)
                    Utilities.EnableRadDatePicker(rdEXPIRE_DATE, False)
                    txtEMAIL.Text = ""
                    txtFULLNAME.Text = ""
                    txtTELEPHONE.Text = ""
                    txtUSERNAME.Text = ""
                    txtEMPLOYEE_CODE.Text = ""
                    cbIS_AD.Checked = False
                    cbIS_APP.Checked = True
                    cbIS_PORTAL.Checked = False
                    chkChangePass.Checked = False
                    chkRecAcc.Checked = False
                    rdEFFECT_DATE.SelectedDate = Nothing
                    rdEXPIRE_DATE.SelectedDate = Nothing

                    btnEmployee.Enabled = False
                    'cboUserGrp.Enabled = False

                Case CommonMessage.STATE_NEW
                    txtUSERNAME.ReadOnly = False
                    txtEMAIL.ReadOnly = False
                    txtEMPLOYEE_CODE.ReadOnly = False
                    txtFULLNAME.ReadOnly = False
                    txtTELEPHONE.ReadOnly = False
                    txtPASSWORD.ReadOnly = False
                    txtPASSWORD_AGAIN.ReadOnly = False
                    'txtEMPLOYEE_CODE.ReadOnly = False
                    cbIS_AD.Enabled = True
                    cbIS_APP.Enabled = True
                    cbIS_PORTAL.Enabled = True
                    chkChangePass.Enabled = True
                    chkRecAcc.Enabled = True
                    cbIs_HR.Enabled = True
                    Utilities.EnableRadDatePicker(rdEFFECT_DATE, True)
                    Utilities.EnableRadDatePicker(rdEXPIRE_DATE, True)
                    'isLoadPopup = 1
                    btnEmployee.Enabled = True
                    'cboUserGrp.Enabled = True

                Case CommonMessage.STATE_EDIT
                    txtUSERNAME.ReadOnly = False
                    txtEMAIL.ReadOnly = False
                    txtEMPLOYEE_CODE.ReadOnly = False
                    txtFULLNAME.ReadOnly = False
                    txtTELEPHONE.ReadOnly = False
                    txtPASSWORD.ReadOnly = False
                    txtPASSWORD_AGAIN.ReadOnly = False
                    'txtEMPLOYEE_CODE.ReadOnly = False
                    cbIS_AD.Enabled = True
                    cbIS_APP.Enabled = True
                    cbIS_PORTAL.Enabled = True
                    chkChangePass.Enabled = True
                    cbIs_HR.Enabled = True
                    chkRecAcc.Enabled = True
                    Utilities.EnableRadDatePicker(rdEFFECT_DATE, True)
                    Utilities.EnableRadDatePicker(rdEXPIRE_DATE, True)

                    btnEmployee.Enabled = False
                    'cboUserGrp.Enabled = False

            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load, reload control theo trạng page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository
            Dim store As New CommonProcedureNew
            Dim data = rep.GetGroupListToComboListBox()
            'FillRadCombobox(cboUserGrp, data, "NAME", "ID")
            If store.CHECK_EXIST_SE_CONFIG("IS_HIDE_AD_USER") = -1 Then
                cbIS_AD.Visible = False
            Else
                cbIS_AD.Visible = True
            End If
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NORMAL
                    If User IsNot Nothing Then
                        txtEMAIL.Text = User.EMAIL
                        txtFULLNAME.Text = User.FULLNAME
                        txtTELEPHONE.Text = User.TELEPHONE
                        txtUSERNAME.Text = If(User.USERNAME Is Nothing, "", User.USERNAME.ToUpper)
                        txtEMPLOYEE_CODE.Text = User.EMPLOYEE_CODE
                        cbIS_AD.Checked = User.IS_AD
                        cbIS_APP.Checked = User.IS_APP
                        cbIS_PORTAL.Checked = User.IS_PORTAL
                        If User.IS_LOGIN IsNot Nothing Then
                            chkChangePass.Checked = User.IS_LOGIN
                        Else
                            chkChangePass.Checked = False
                        End If

                        If User.IS_RC IsNot Nothing Then
                            chkRecAcc.Checked = User.IS_RC
                        Else
                            chkRecAcc.Checked = False
                        End If
                        If User.IS_HR IsNot Nothing Then
                            cbIs_HR.Checked = User.IS_HR
                        Else
                            cbIs_HR.Checked = False
                        End If
                        If User.EFFECT_DATE IsNot Nothing Then
                            rdEFFECT_DATE.SelectedDate = User.EFFECT_DATE
                        End If

                        If User.EFFECT_DATE IsNot Nothing Then
                            rdEXPIRE_DATE.SelectedDate = User.EXPIRE_DATE
                        End If

                        hidID.Value = User.ID.ToString

                        If User.EMPLOYEE_ID IsNot Nothing Then
                            hidEmployeeID.Value = User.EMPLOYEE_ID
                        End If

                    Else
                        txtEMAIL.Text = ""
                        txtFULLNAME.Text = ""
                        txtTELEPHONE.Text = ""
                        txtUSERNAME.Text = ""
                        txtEMPLOYEE_CODE.Text = ""
                        cbIS_AD.Checked = False
                        cbIS_APP.Checked = True
                        cbIS_PORTAL.Checked = False
                        chkChangePass.Checked = False
                        cbIs_HR.Checked = False
                        chkRecAcc.Checked = False
                        rdEFFECT_DATE.SelectedDate = Nothing
                        rdEXPIRE_DATE.SelectedDate = Nothing
                    End If
                Case CommonMessage.STATE_NEW
                    txtEMAIL.Text = ""
                    txtFULLNAME.Text = ""
                    txtTELEPHONE.Text = ""
                    txtUSERNAME.Text = ""
                    txtEMPLOYEE_CODE.Text = ""
                    cbIS_AD.Checked = False
                    cbIS_APP.Checked = True
                    cbIS_PORTAL.Checked = False
                    chkChangePass.Checked = False
                    cbIs_HR.Checked = False
                    chkRecAcc.Checked = False
                    rdEFFECT_DATE.SelectedDate = Nothing
                    rdEXPIRE_DATE.SelectedDate = Nothing
            End Select
            'txtUSERNAME.Focus()
            '_myLog.WriteLog(_myLog._info, _classPath, method,
            '                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <summary>
    ''' Event click item menu tooolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs)
        Dim objUser As New CommonBusiness.UserDTO
        Dim rep As New CommonRepository
        Dim str As New CommonProcedureNew
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cbIS_APP.Checked = False And cbIS_PORTAL.Checked = False Then
                            ShowMessage(Translate("Bất buộc check 1 trong 2 check User App / User Portal"), NotifyType.Warning)
                            Exit Sub
                        End If
                        objUser.USERNAME = txtUSERNAME.Text.Trim.ToUpper
                        objUser.TELEPHONE = txtTELEPHONE.Text.Trim
                        objUser.EMAIL = txtEMAIL.Text.Trim
                        objUser.FULLNAME = txtFULLNAME.Text.Trim
                        objUser.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text.Trim
                        objUser.IS_AD = cbIS_AD.Checked
                        objUser.IS_APP = cbIS_APP.Checked
                        objUser.IS_PORTAL = cbIS_PORTAL.Checked
                        objUser.IS_LOGIN = chkChangePass.Checked
                        objUser.IS_RC = chkRecAcc.Checked
                        objUser.IS_HR = cbIs_HR.Checked
                        objUser.IS_CHANGE_PASS = Not chkChangePass.Checked
                        If hidEmployeeID.Value <> "" Then
                            objUser.EMPLOYEE_ID = hidEmployeeID.Value
                        End If

                        If rdEFFECT_DATE.SelectedDate IsNot Nothing Then
                            objUser.EFFECT_DATE = rdEFFECT_DATE.SelectedDate
                        End If
                        If rdEXPIRE_DATE.SelectedDate IsNot Nothing Then
                            objUser.EXPIRE_DATE = rdEXPIRE_DATE.SelectedDate
                        End If

                        'If cboUserGrp.Text <> "" Then
                        '    objUser.GROUP_USER_ID = cboUserGrp.SelectedValue
                        'End If
                        Dim EncryptData As New Framework.UI.EncryptData
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim dtcheck = str.GET_ACCCOUNT_INFO(objUser.EFFECT_DATE).Rows(0)
                                If cbIS_APP.Checked Then
                                    If dtcheck("COUNT_APP_SET") <= dtcheck("COUNT_APP") Then
                                        ShowMessage("Số lượng tài khoản APP đăng ký đã vượt giới hạn cho phép", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If cbIS_PORTAL.Checked Then
                                    If dtcheck("COUNT_PORTAL_SET") <= dtcheck("COUNT_PORTAL") Then
                                        ShowMessage("Số lượng tài khoản Portal đăng ký đã vượt giới hạn cho phép", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If Not cbIS_AD.Checked Then
                                    If txtPASSWORD.Text.Trim = "" Then
                                        ShowMessage("Bạn phải nhập mật khẩu.", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If txtPASSWORD.Text.Trim <> "" Then
                                    objUser.PASSWORD = EncryptData.EncryptString(txtPASSWORD.Text.Trim)
                                End If
                                Dim lst As New List(Of String)
                                lst.Add("'" & txtEMPLOYEE_CODE.Text.Trim & "'")
                                If Not rep.CheckExistValue(lst, "SE_USER", "EMPLOYEE_CODE") Then
                                    ShowMessage(Translate("Mã nhân viên đã được sử dụng"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.CheckWorkStatus("ID", "'" & hidEmployeeID.Value & "'") Then
                                    ShowMessage(Translate("Mã nhân viên đã nghỉ việc"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim grpID As New List(Of Decimal)
                                Dim grp = 0
                                grpID.Add(grp)

                                objUser.GROUP_USER_ID = grpID
                                If rep.InsertUser(objUser) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If

                            Case CommonMessage.STATE_EDIT

                                objUser.ID = Decimal.Parse(hidID.Value)
                                Dim dt = str.GET_ACCCOUNT_BY_ID(Decimal.Parse(hidID.Value)).Rows(0)

                                Dim dtcheck = str.GET_ACCCOUNT_INFO(objUser.EFFECT_DATE).Rows(0)
                                If cbIS_APP.Checked And dt("IS_APP") <> -1 Then
                                    If dtcheck("COUNT_APP_SET") <= dtcheck("COUNT_APP") Then
                                        ShowMessage("Số lượng tài khoản APP đăng ký đã vượt giới hạn cho phép", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If cbIS_PORTAL.Checked And dt("IS_PORTAL") <> -1 Then
                                    If dtcheck("COUNT_PORTAL_SET") <= dtcheck("COUNT_PORTAL") Then
                                        ShowMessage("Số lượng tài khoản Portal đăng ký đã vượt giới hạn cho phép", Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If



                                If txtPASSWORD.Text.Trim.Length > 0 Then
                                    objUser.PASSWORD = EncryptData.EncryptString(txtPASSWORD.Text.Trim)
                                Else
                                    objUser.PASSWORD = User.PASSWORD
                                End If
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(objUser.ID)
                                If rep.CheckExistIDTable(lstID, "SE_USER", "ID") Then
                                    Me.Send(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE)
                                    Exit Sub
                                End If
                                If rep.ModifyUser(objUser) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If
                        End Select
                        UserViewList = Me.Register("ctrlListUser", "Common", "ctrlListUser", "Secure")
                        UserViewList.ReSize()
                    Else
                        UserViewList = Me.Register("ctrlListUser", "Common", "ctrlListUser", "Secure")
                        UserViewList.ReSizeSpliter()
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.Send(CommonMessage.ACTION_CANCEL)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Validate Username
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub validateUSERNAME_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles validateUSERNAME.ServerValidate
        Dim rep As New CommonRepository
        Dim _validate As New UserDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                If Not String.IsNullOrEmpty(User.USERNAME) Then
                    If txtUSERNAME.Text.Trim.ToUpper = User.USERNAME.ToUpper Then
                        args.IsValid = True
                    Else
                        _validate.USERNAME = txtUSERNAME.Text
                        args.IsValid = rep.ValidateUser(_validate)
                    End If
                End If
            Else
                _validate.USERNAME = txtUSERNAME.Text
                args.IsValid = rep.ValidateUser(_validate)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Validate password nhập lại
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalPASSWORD_2_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalPASSWORD_2.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            If CurrentState = CommonMessage.STATE_NEW Then

                'If txtPASSWORD.Text.Trim.Length = 0 Then
                '    args.IsValid = True
                '    Exit Sub
                'End If
                Dim maxLength As Integer
                Dim msg As String = ""

                maxLength = CommonConfig.PasswordLength
                If txtPASSWORD.Text.Length < maxLength Then
                    msg = "<br />" & Translate("Độ dài mật khẩu tối thiểu {0}", maxLength)
                End If
                If CommonConfig.PasswordLowerChar <> 0 Then
                    If Not RegularExpressions.Regex.IsMatch(txtPASSWORD.Text, "^(?=.*[a-z]).*$") Then
                        msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự thường")
                    End If
                End If
                If CommonConfig.PasswordUpperChar <> 0 Then
                    If Not RegularExpressions.Regex.IsMatch(txtPASSWORD.Text, "^(?=.*[A-Z]).*$") Then
                        msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự hoa")
                    End If
                End If
                If CommonConfig.PasswordNumberChar <> 0 Then
                    If Not RegularExpressions.Regex.IsMatch(txtPASSWORD.Text, "^(?=.*[\d]).*$") Then
                        msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự số")
                    End If
                End If
                If CommonConfig.PasswordSpecialChar <> 0 Then
                    If Not RegularExpressions.Regex.IsMatch(txtPASSWORD.Text, "^(?=.*[\W]).*$") Then
                        msg &= "<br />" & Translate("Mật khẩu phải có ít nhất 1 ký tự đặc biệt")
                    End If
                End If
                If msg <> "" Then
                    msg = msg.Substring(6)
                    cvalPASSWORD_2.ErrorMessage = msg
                    cvalPASSWORD_2.ToolTip = msg
                    args.IsValid = False
                    UserViewList = Me.Register("ctrlListUser", "Common", "ctrlListUser", "Secure")
                    UserViewList.ReSizeSpliter()
                Else
                    args.IsValid = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEMPLOYEE_CODE_TextChanged(sender As Object, e As EventArgs) Handles txtEMPLOYEE_CODE.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If txtEMPLOYEE_CODE.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of EmployeePopupFindListDTO)
                Dim _filter As New EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                ElseIf Count = 1 Then
                    Dim item = EmployeeList(0)
                    hidEmployeeID.Value = item.ID.ToString
                    txtEMPLOYEE_CODE.Text = item.EMPLOYEE_CODE
                    txtFULLNAME.Text = item.FULLNAME_VN
                    txtEMAIL.Text = If(item.WORK_EMAIL = "", item.PER_EMAIL, item.WORK_EMAIL)
                    txtTELEPHONE.Text = item.MOBILE_PHONE
                    isLoadPopup = 0
                ElseIf Count > 1 Then
                    Dim a = Page.IsPostBack
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEMPLOYEE_CODE.Text
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.Show()
                        isLoadPopup = 1
                    End If
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEmployee_Click(sender As Object, e As System.EventArgs) Handles btnEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click huy tren form popup list Nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
      Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
        txtEMPLOYEE_CODE.Text = Nothing
        txtFULLNAME.Text = Nothing
        hidEmployeeID.Value = Nothing
    End Sub
    ''' <summary>
    ''' event click Chon ma nhan vien tu popup list nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)

                hidEmployeeID.Value = item.ID.ToString
                txtEMPLOYEE_CODE.Text = item.EMPLOYEE_CODE
                txtFULLNAME.Text = item.FULLNAME_VN
                txtEMAIL.Text = If(item.WORK_EMAIL <> "", item.WORK_EMAIL, item.PER_EMAIL)
                txtTELEPHONE.Text = item.MOBILE_PHONE
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class