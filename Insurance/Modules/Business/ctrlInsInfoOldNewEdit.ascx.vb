Imports Common
Imports Framework.UI
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsInfoOldNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property
    Property INFOOLDDTO As INS_INFOOLDDTO
        Get
            Return ViewState(Me.ID & "_INS_INFOOLDDTO")
        End Get
        Set(ByVal value As INS_INFOOLDDTO)
            ViewState(Me.ID & "_INS_INFOOLDDTO") = value
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

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New INS_INFOOLDDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetINS_INFOOLDById(obj.ID)
                    INFOOLDDTO = New INS_INFOOLDDTO
                    If obj IsNot Nothing Then
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        txtChucDanh.Text = obj.TITLE_NAME
                        txtDonVi.Text = obj.ORG_NAME
                        txtName.Text = obj.VN_FULLNAME
                        txtcmnd.Text = obj.CMND
                        txtNoiCap.Text = obj.NOICAP
                        txtOrg_ID_INS.Text = obj.ORG_ID_INS
                        If obj.SALARY Is Nothing Then
                            txtLuongThamGia.Text = 0
                        Else
                            txtLuongThamGia.Text = obj.SALARY.ToString
                        End If

                        If obj.NGAYSINH.HasValue Then
                            txtNgaysinh.Text = Format(obj.NGAYSINH, "dd/MM/yyyy")
                        End If

                        If obj.ISBHXH.HasValue Then
                            chkBHXH.Checked = obj.ISBHXH
                        End If
                        If obj.SOFROM.HasValue Then
                            rdtuthangbhxh.SelectedDate = obj.SOFROM
                        End If
                        If obj.SOTO.HasValue Then
                            rdDenThangbhxh.SelectedDate = obj.SOTO
                        End If
                        txtnote.Text = obj.NOTE_BHXH

                        'txtNoisinh.Text = obj.NOISINH
                        'txtSosobhxh.Text = obj.SOBOOKNO
                        'If obj.SOSTATUS_BHXH IsNot Nothing Then
                        '    cboTinhTrangsobhxh.SelectedValue = obj.SOSTATUS_BHXH
                        'End If
                        'If obj.SOPRVDBOOKDAY IsNot Nothing Then
                        '    rdNgayCapbhxh.SelectedDate = obj.SOPRVDBOOKDAY
                        'End If
                        'If obj.DAYPAYMENTCOMPANY IsNot Nothing Then
                        '    rdNgayNopbhxh.SelectedDate = obj.DAYPAYMENTCOMPANY
                        'End If
                        'If obj.ISBHYT.HasValue Then
                        '    chkBHYT.Checked = obj.ISBHYT
                        'End If
                        'If obj.HEFROM.HasValue Then
                        '    rdfrommonthBhyt.SelectedDate = obj.HEFROM
                        'End If

                        'If obj.HETO.HasValue Then
                        '    rdtoMonthBhyt.SelectedDate = obj.HETO
                        'End If

                        'txtSoTheYte.Text = obj.HECARDNO
                        'If obj.HESTATUS_BHYT IsNot Nothing Then
                        '    cboTinhTrangTheBhyt.SelectedValue = obj.HESTATUS_BHYT
                        'End If
                        'If obj.HECARDEFFFROM.HasValue Then
                        '    rdNgayHieLuc.SelectedDate = obj.HECARDEFFFROM
                        'End If
                        'If obj.HECARDEFFTO.HasValue Then
                        '    rdNgayHieLuc.SelectedDate = obj.HECARDEFFTO
                        'End If
                        'If obj.HECARDEFFTO.HasValue Then
                        '    rdNgayHetHieuLuc.SelectedDate = obj.HECARDEFFTO
                        'End If
                        'If obj.HEWHRREGISKEY IsNot Nothing Then
                        '    cboNoiKhamChuaBen.SelectedValue = obj.HEWHRREGISKEY
                        'End If
                        'If obj.ISBHTN.HasValue Then
                        '    chkBHTN.Checked = obj.ISBHTN
                        'End If
                        'If obj.UEFROM.HasValue Then
                        '    rdfromMonthBHTN.SelectedDate = obj.UEFROM
                        'End If
                        'If obj.UETO.HasValue Then
                        '    rdtoMonthBHTN.SelectedDate = obj.UETO
                        'End If
                        Employee_id = obj.EMPLOYEE_ID
                        _Value = obj.ID
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'GetDataCombo()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select
            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As INS_INFOOLDDTO
        Dim rep As New InsuranceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If _Value.HasValue Then
                        obj = New INS_INFOOLDDTO
                        obj.ID = _Value
                        obj.EMPLOYEE_ID = Employee_id
                        obj.SOFROM = rdtuthangbhxh.SelectedDate
                        obj.SOTO = rdDenThangbhxh.SelectedDate
                        obj.SALARY = txtLuongThamGia.Value
                        obj.ORG_ID_INS = txtOrg_ID_INS.Text.Trim
                        obj.ISBHXH = chkBHXH.Checked
                        obj.NOTE_BHXH = txtnote.Text.Trim
                        rep.ModifyINS_INFOOLD(obj, gstatus)
                    Else
                        obj = New INS_INFOOLDDTO
                        obj.EMPLOYEE_ID = Employee_id
                        obj.SOFROM = rdtuthangbhxh.SelectedDate
                        obj.SOTO = rdDenThangbhxh.SelectedDate
                        obj.SALARY = txtLuongThamGia.Value
                        obj.ORG_ID_INS = txtOrg_ID_INS.Text.Trim
                        obj.ISBHXH = chkBHXH.Checked
                        obj.NOTE_BHXH = txtnote.Text.Trim
                        rep.InsertINS_INFOOLD(obj, gstatus)
                    End If

                    'Dim str As String = "getRadWindow().close('1');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    ''POPUPTOLINK
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsInfoOld&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsInfoOld&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 16:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien chon nhan vien cho ctrFindEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim emp As New EmployeeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            emp = rep.GetEmployeeById(empID)
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtChucDanh.Text = emp.TITLE_NAME
            txtcmnd.Text = emp.CMND
            txtDonVi.Text = emp.ORG_NAME
            txtName.Text = emp.VN_FULLNAME
            txtNoiCap.Text = emp.NOICAP
            txtCapNS.Text = emp.STAFF_RANK_NAME
            If emp.NGAYSINH.HasValue Then
                txtNgaysinh.Text = emp.NGAYSINH.Value.ConvertString
            End If
            'txtNoisinh.Text = emp.NOISINH
            Employee_id = emp.EMPLOYEE_ID
            ExcuteScript("Resize", "registerOnfocusOut('ctl00_MainContent_ctrlInsInfoOldNewEdit_RadPane2')")
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event cancel chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        ExcuteScript("Resize", "registerOnfocusOut('ctl00_MainContent_ctrlInsInfoOldNewEdit_RadPane2')")
    End Sub
    'Private Sub GetDataCombo()
    '    Dim rep As New InsuranceRepository
    '    Try
    '        If ListComboData Is Nothing Then
    '            ListComboData = New ComboBoxDataDTO
    '            ListComboData.GET_LIST_STATUSNOBOOK = True
    '            ListComboData.GET_LIST_STATUSCARD = True
    '            ListComboData.GET_LIST_INS_WHEREHEALTH = True
    '            ListComboData.GET_LIST_ORG_ID_INS = True
    '            rep.GetComboboxData(ListComboData)
    '        End If
    '        'FillRadCombobox(cboTinhTrangsobhxh, ListComboData.LIST_LIST_STATUSNOBOOK, "NAME_VN", "ID", True)
    '        'If ListComboData.LIST_LIST_STATUSNOBOOK.Count > 0 Then
    '        '    cboTinhTrangsobhxh.SelectedIndex = 0
    '        'End If
    '        'FillRadCombobox(cboTinhTrangTheBhyt, ListComboData.LIST_LIST_STATUSCARD, "NAME_VN", "ID", True)
    '        'If ListComboData.LIST_LIST_STATUSCARD.Count > 0 Then
    '        '    cboTinhTrangsobhxh.SelectedIndex = 0
    '        'End If
    '        'FillRadCombobox(cboNoiKhamChuaBen, ListComboData.LIST_LIST_INS_WHEREHEALTH, "NAME_VN", "ID", True)
    '        'If ListComboData.LIST_LIST_INS_WHEREHEALTH.Count > 0 Then
    '        '    cboNoiKhamChuaBen.SelectedIndex = 0
    '        'End If
    '        'FillRadCombobox(cboOrg_ID_INS, ListComboData.LIST_LIST_ORG_ID_INS, "NAME_VN", "ID", True)
    '        'If ListComboData.LIST_LIST_ORG_ID_INS.Count > 0 Then
    '        '    cboOrg_ID_INS.SelectedIndex = 0
    '        'End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

#End Region

End Class

