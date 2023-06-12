Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlManInfoInsNewEdit
    Inherits CommonView

    ''' <summary>
    ''' ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrlFindSigner
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' isEdit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    ''' <summary>
    ''' Employee_id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    ''' <summary>
    ''' _Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    ''' <summary>
    ''' RegisterOT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RegisterOT As INS_INFORMATIONDTO
        Get
            Return ViewState(Me.ID & "_objIns_Info")
        End Get
        Set(ByVal value As INS_INFORMATIONDTO)
            ViewState(Me.ID & "_objIns_Info") = value
        End Set
    End Property

    ''' <summary>
    ''' INFOOLDDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property INFOOLDDTO As INS_INFORMATIONDTO
        Get
            Return ViewState(Me.ID & "_INS_INFODTO")
        End Get
        Set(ByVal value As INS_INFORMATIONDTO)
            ViewState(Me.ID & "_INS_INFODTO") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueTTSo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueTTSo As Decimal
        Get
            Return ViewState(Me.ID & "_ValueTTSo")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueTTSo") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueTTThe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueTTThe As Decimal
        Get
            Return ViewState(Me.ID & "_ValueTTThe")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueTTThe") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueNoiKCB
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueNoiKCB As String
        Get
            Return ViewState(Me.ID & "_ValueNoiKCB")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ValueNoiKCB") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            UpdateControlState()
            If IsPostBack Then
                ExcuteScript("OnFocus", "fnRegisterOnfocusOut()")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New INS_INFORMATIONDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetINS_INFOById(obj.ID)
                    INFOOLDDTO = New INS_INFORMATIONDTO

                    If obj IsNot Nothing Then
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        hidEmpID.Value = obj.EMPLOYEE_ID
                        txtName.Text = obj.EMPLOYEE_NAME
                        hidOrgID.Value = obj.ORG_ID
                        hidID.Value = obj.ID
                        txtChucDanh.Text = obj.POSITIONNAME
                        txtDonVi.Text = obj.ORGNAME
                        txtcmnd.Text = obj.CMND
                        txtNoiCap.Text = obj.NOICAP

                        If obj.NGAYSINH.HasValue Then
                            txtNgaysinh.Text = obj.NGAYSINH.Value.ConvertString
                        End If

                        txtNoiSinh.Text = obj.NOISINH
                        txtCapNS.Text = obj.STAFF_RANK_NAME
                        nmTotalBefor.Value = obj.TOTAL_TIME_INS_BEFOR

                        If obj.SALARY IsNot Nothing Then
                            rnSalary.Value = obj.SALARY
                        Else
                            rnSalary.Value = 0
                        End If
                        'If obj.INSCENTERKEY IsNot Nothing Then
                        '    cboOrg_ID_INS.SelectedValue = obj.INSCENTERKEY
                        'Else
                        '    cboOrg_ID_INS.SelectedValue = 0
                        'End If
                        If obj.SO IsNot Nothing Then
                            cbkBHXH.Checked = obj.SO
                        End If

                        If obj.HE IsNot Nothing Then
                            cbkBHYT.Checked = obj.HE
                        End If

                        If obj.UE IsNot Nothing Then
                            cbkBHTN.Checked = obj.UE
                        End If

                        rdFromBHXH.SelectedDate = obj.SOFROM
                        txtSoSo.Text = obj.SOBOOKNO

                        If obj.SOPRVDBOOKSTATUS IsNot Nothing Then
                            cbotinhtrangSo.SelectedValue = obj.SOPRVDBOOKSTATUS
                        End If

                        rdNgaycapXH.SelectedDate = obj.SOPRVDBOOKDAY
                        rdNgayNopSo.SelectedDate = obj.DAYPAYMENTCOMPANY
                        txtNote.Text = obj.NOTE_SO
                        'rdFromYT.SelectedDate = obj.HEFROM
                        'rdToYT.SelectedDate = obj.HETO
                        txtSoThe.Text = obj.HECARDNO

                        If obj.HEPRVDCARDSTATUS IsNot Nothing Then
                            cboTinhTrangThe.SelectedValue = obj.HEPRVDCARDSTATUS
                        End If

                        rdNgayHieuLuc.SelectedDate = obj.HECARDEFFFROM
                        rdNgayhetHL.SelectedDate = obj.HECARDEFFTO

                        If obj.HEWHRREGISKEY IsNot Nothing Then
                            cboNoiKham.SelectedValue = obj.HEWHRREGISKEY
                        End If

                        rdFromTN.SelectedDate = obj.UEFROM
                        rdToTN.SelectedDate = obj.UETO
                        INFOOLDDTO.ORG_ID = obj.ORG_ID
                        _Value = obj.ID
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve nhân viên khi click button [hủy] o popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Show popup chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.IsHideTerminate = True
                    ctrlFindEmployeePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As INS_INFORMATIONDTO
        Dim rep As New InsuranceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If _Value.HasValue Then

                            Dim cmRep As New CommonRepository
                            Dim lstID As New List(Of Decimal)

                            lstID.Add(Convert.ToDecimal(_Value))

                            If cmRep.CheckExistIDTable(lstID, "INS_INFORMATION", "ID") Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                Exit Sub
                            End If

                            obj = New INS_INFORMATIONDTO
                            obj.ID = hidID.Value
                            obj.EMPLOYEE_ID = hidEmpID.Value
                            obj.SALARY = rnSalary.Value
                            obj.INSCENTERKEY = Nothing
                            obj.POSITIONNAME = txtChucDanh.Text
                            obj.ORGNAME = txtDonVi.Text
                            obj.SOFROM = rdFromBHXH.SelectedDate
                            obj.SOBOOKNO = txtSoSo.Text
                            obj.SOPRVDBOOKDAY = rdNgaycapXH.SelectedDate

                            If cbotinhtrangSo.SelectedValue IsNot Nothing AndAlso cbotinhtrangSo.SelectedValue <> "" Then
                                obj.SOPRVDBOOKSTATUS = cbotinhtrangSo.SelectedValue
                            End If

                            obj.DAYPAYMENTCOMPANY = rdNgayNopSo.SelectedDate
                            obj.NOTE_SO = txtNote.Text
                            'obj.HEFROM = rdFromYT.SelectedDate
                            'obj.HETO = rdToYT.SelectedDate
                            obj.HECARDNO = txtSoThe.Text

                            If cboTinhTrangThe.SelectedValue IsNot Nothing AndAlso cboTinhTrangThe.SelectedValue <> "" Then
                                obj.HEPRVDCARDSTATUS = cboTinhTrangThe.SelectedValue
                            End If

                            If cboNoiKham.SelectedValue IsNot Nothing AndAlso cboNoiKham.SelectedValue <> "" Then
                                obj.HEWHRREGISKEY = cboNoiKham.SelectedValue
                            End If

                            obj.HECARDEFFFROM = rdNgayHieuLuc.SelectedDate
                            obj.HECARDEFFTO = rdNgayhetHL.SelectedDate
                            obj.UEFROM = rdFromTN.SelectedDate
                            obj.UETO = rdToTN.SelectedDate
                            obj.SO = cbkBHXH.Checked
                            obj.HE = cbkBHYT.Checked
                            obj.UE = cbkBHTN.Checked
                            obj.TOTAL_TIME_INS_BEFOR = nmTotalBefor.Value
                            rep.ModifyINS_INFO(obj, gstatus)
                        Else
                            obj = New INS_INFORMATIONDTO
                            obj.EMPLOYEE_ID = hidEmpID.Value
                            obj.SALARY = rnSalary.Value
                            obj.INSCENTERKEY = Nothing
                            obj.POSITIONNAME = txtChucDanh.Text
                            obj.ORGNAME = txtDonVi.Text
                            obj.SOFROM = rdFromBHXH.SelectedDate
                            obj.SOBOOKNO = txtSoSo.Text
                            obj.SOPRVDBOOKDAY = rdNgaycapXH.SelectedDate

                            If cbotinhtrangSo.SelectedValue IsNot Nothing AndAlso cbotinhtrangSo.SelectedValue <> "" Then
                                obj.SOPRVDBOOKSTATUS = cbotinhtrangSo.SelectedValue
                            End If

                            obj.DAYPAYMENTCOMPANY = rdNgayNopSo.SelectedDate
                            obj.NOTE_SO = txtNote.Text

                            'obj.HEFROM = rdFromYT.SelectedDate
                            'obj.HETO = rdToYT.SelectedDate
                            obj.HECARDNO = txtSoThe.Text

                            If cboTinhTrangThe.SelectedValue IsNot Nothing AndAlso cboTinhTrangThe.SelectedValue <> "" Then
                                obj.HEPRVDCARDSTATUS = cboTinhTrangThe.SelectedValue
                            End If

                            If cboNoiKham.SelectedValue IsNot Nothing AndAlso cboNoiKham.SelectedValue <> "" Then
                                obj.HEWHRREGISKEY = cboNoiKham.SelectedValue
                            End If

                            obj.HECARDEFFFROM = rdNgayHieuLuc.SelectedDate
                            obj.HECARDEFFTO = rdNgayhetHL.SelectedDate

                            obj.UEFROM = rdFromTN.SelectedDate
                            obj.UETO = rdToTN.SelectedDate

                            obj.SO = cbkBHXH.Checked
                            obj.HE = cbkBHYT.Checked
                            obj.UE = cbkBHTN.Checked
                            obj.TOTAL_TIME_INS_BEFOR = nmTotalBefor.Value

                            rep.InsertINS_INFO(obj, gstatus)
                        End If
                    End If
                    'Dim str As String = "getRadWindow().close('1');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    ''POPUPTOLINK
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlManInfoIns&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlManInfoIns&group=Business")
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event Load thông tin nhân viên đã được chọn ở popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim emp As New EmployeeDTO
        Dim dtLuongBH As New DataTable

        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            emp = rep.GetEmployeeById(empID)

            Dim infoIns As New INS_INFORMATIONDTO
            infoIns = rep.GetInfoInsByEmpID(emp.EMPLOYEE_ID)
            dtLuongBH = rep.GetLuongBH(emp.EMPLOYEE_ID)

            If infoIns Is Nothing Then
                txtEmployeeCode.Text = emp.EMPLOYEE_CODE
                txtName.Text = emp.VN_FULLNAME
                txtChucDanh.Text = emp.TITLE_NAME
                txtcmnd.Text = emp.CMND
                txtNoiCap.Text = emp.NOICAP
                txtDonVi.Text = emp.ORG_NAME

                If emp.NGAYSINH.HasValue Then
                    txtNgaysinh.Text = emp.NGAYSINH.Value.ConvertString
                End If

                txtNoiSinh.Text = emp.NOISINH
                txtCapNS.Text = emp.STAFF_RANK_NAME
                hidEmpID.Value = emp.EMPLOYEE_ID
                hidOrgID.Value = emp.ORG_ID

                If dtLuongBH.Rows.Count > 0 Then
                    rnSalary.Value = CDbl(dtLuongBH.Rows(0)("TOTAL"))
                End If
            Else
                ShowMessage(Translate("Nhân viên đã được khai báo thông tin bảo hiểm!"), NotifyType.Warning)
                Exit Sub
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox TÌNH TRẠNG SỔ có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTinhTrangSo_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTinhTrangSo.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If String.IsNullOrEmpty(cbotinhtrangSo.SelectedValue) Then
                args.IsValid = True
                Exit Sub
            End If

            ListComboData = New ComboBoxDataDTO
            Dim dto As New OT_OTHERLIST_DTO
            Dim list As New List(Of OT_OTHERLIST_DTO)

            dto.ID = Convert.ToDecimal(cbotinhtrangSo.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_STATUSNOBOOK = True
            ListComboData.LIST_LIST_STATUSNOBOOK = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cbotinhtrangSo, ListComboData.LIST_LIST_STATUSNOBOOK, "NAME_VN", "ID", True)
                ClearControlValue(cbotinhtrangSo)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox TÌNH TRẠNG THẺ có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTinhTrangThe_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTinhTrangThe.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If String.IsNullOrEmpty(cboTinhTrangThe.SelectedValue) Then
                args.IsValid = True
                Exit Sub
            End If

            ListComboData = New ComboBoxDataDTO
            Dim dto As New OT_OTHERLIST_DTO
            Dim list As New List(Of OT_OTHERLIST_DTO)

            dto.ID = Convert.ToDecimal(cboTinhTrangThe.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_STATUSCARD = True
            ListComboData.LIST_LIST_STATUSCARD = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cboTinhTrangThe, ListComboData.LIST_LIST_STATUSCARD, "NAME_VN", "ID", True)
                ClearControlValue(cboTinhTrangThe)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox NƠI KHÁM CHỮA BỆNH có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalNoiKham_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalNoiKham.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If String.IsNullOrEmpty(cboTinhTrangThe.SelectedValue) Then
                args.IsValid = True
                Exit Sub
            End If

            ListComboData = New ComboBoxDataDTO
            Dim dto As New INS_WHEREHEALTHDTO
            Dim list As New List(Of INS_WHEREHEALTHDTO)

            dto.ID = Convert.ToDecimal(cboNoiKham.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_INS_WHEREHEALTH = True
            ListComboData.LIST_LIST_INS_WHEREHEALTH = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cboNoiKham, ListComboData.LIST_LIST_INS_WHEREHEALTH, "NAME_VN", "ID", True)
                ClearControlValue(cboNoiKham)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_STATUSNOBOOK = True
                ListComboData.GET_LIST_STATUSCARD = True
                ListComboData.GET_LIST_INS_WHEREHEALTH = True
                ListComboData.GET_LIST_ORG_ID_INS = True
                rep.GetComboboxData(ListComboData)
            End If

            FillRadCombobox(cbotinhtrangSo, ListComboData.LIST_LIST_STATUSNOBOOK, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_STATUSNOBOOK.Count > 0 Then
                cbotinhtrangSo.SelectedIndex = 0
            End If

            FillRadCombobox(cboTinhTrangThe, ListComboData.LIST_LIST_STATUSCARD, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_STATUSCARD.Count > 0 Then
                cboTinhTrangThe.SelectedIndex = 0
            End If

            FillRadCombobox(cboNoiKham, ListComboData.LIST_LIST_INS_WHEREHEALTH, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_INS_WHEREHEALTH.Count > 0 Then
                cboNoiKham.SelectedIndex = 0
            End If
            'FillRadCombobox(cboOrg_ID_INS, ListComboData.LIST_LIST_ORG_ID_INS, "NAME_VN", "ID", True)
            'If ListComboData.LIST_LIST_ORG_ID_INS.Count > 0 Then
            '    cboOrg_ID_INS.SelectedIndex = 0
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class

