Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsChangeNewEdit
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
    ''' INSCHANGEDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property INSCHANGEDTO As INS_CHANGEDTO
        Get
            Return ViewState(Me.ID & "_INS_CHANGEDTO")
        End Get
        Set(ByVal value As INS_CHANGEDTO)
            ViewState(Me.ID & "_INS_CHANGEDTO") = value
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
    ''' ValueLBD
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueLBD As Decimal
        Get
            Return ViewState(Me.ID & "_ValueLBD")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueLBD") = value
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
                    Dim obj As New INS_CHANGEDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetINS_CHANGEById(obj.ID)
                    INSCHANGEDTO = New INS_CHANGEDTO

                    If obj IsNot Nothing Then
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        txtChucDanh.Text = obj.TITLE_NAME
                        txtDonVi.Text = obj.ORG_NAME
                        txtName.Text = obj.VN_FULLNAME
                        txtcmnd.Text = obj.CMND
                        txtNoiCap.Text = obj.NOICAP

                        If obj.NGAYSINH IsNot Nothing Then
                            txtNgaysinh.Text = obj.NGAYSINH.Value.ConvertString
                        End If

                        txtNoisinh.Text = obj.NOISINH
                        If obj.ISBHXH IsNot Nothing Then
                            chkbhxh.Checked = obj.ISBHXH
                        End If

                        If obj.ISBHYT IsNot Nothing Then
                            chkbhyt.Checked = obj.ISBHYT
                        End If

                        If obj.ISBHTN IsNot Nothing Then
                            chkbhtn.Checked = obj.ISBHTN
                        End If

                        If obj.CHANGE_TYPE IsNot Nothing Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("INS_CHANGE_TYPE", "ID", obj.CHANGE_TYPE) Then
                                cboLoaiBienDong.SelectedValue = obj.CHANGE_TYPE
                            Else
                                cboLoaiBienDong.Text = obj.CHANGE_TYPE_NAME
                                ValueLBD = obj.CHANGE_TYPE
                            End If
                        End If
                        'If obj.EFFECTDATE IsNot Nothing Then
                        '    rdNgayHieuLuc.SelectedDate = obj.EFFECTDATE
                        'End If
                        If obj.CHANGE_MONTH IsNot Nothing Then
                            rdThangBiendong.SelectedDate = obj.CHANGE_MONTH
                        End If

                        If obj.OLDSALARY IsNot Nothing Then
                            nmLuongKyTruoc.Text = obj.OLDSALARY.Value.ToString()
                        End If

                        If obj.NEWSALARY IsNot Nothing Then
                            nmLuongKyNay.Text = obj.NEWSALARY.Value.ToString()
                        End If

                        If obj.RETURN_DATEBHXH IsNot Nothing Then
                            rdNgayTraTheBHXH.SelectedDate = obj.RETURN_DATEBHXH
                        End If

                        If obj.RETURN_DATEBHYT IsNot Nothing Then
                            rdNgayTraTheBHYT.SelectedDate = obj.RETURN_DATEBHYT
                        End If
                        txtnote.Text = obj.NOTE
                        ' thong tin truy thu
                        If obj.CLTFRMMONTH IsNot Nothing Then
                            rdTuNgaytt.SelectedDate = obj.CLTFRMMONTH
                        End If

                        If obj.CLTTOMONTH IsNot Nothing Then
                            rdDenNgayTT.SelectedDate = obj.CLTTOMONTH
                        End If

                        If obj.CLTBHXH IsNot Nothing Then
                            txtBHXHTT.Text = obj.CLTBHXH.ToString
                        End If

                        If obj.CLTBHYT IsNot Nothing Then
                            txtBHYTTT.Text = obj.CLTBHYT.ToString
                        End If

                        If obj.CLTBHTN IsNot Nothing Then
                            txtBHTNTT.Text = obj.CLTBHTN
                        End If

                        If obj.REPFRMMONTH IsNot Nothing Then
                            rdTuNgayThoaiThu.SelectedDate = obj.REPFRMMONTH
                        End If

                        If obj.REPTOMONTH IsNot Nothing Then
                            rddenngayThoaiThu.SelectedDate = obj.REPTOMONTH
                        End If

                        If obj.REPBHXH IsNot Nothing Then
                            txtBHXHTOAITHU.Text = obj.REPBHXH.ToString
                        End If

                        If obj.REPBHYT IsNot Nothing Then
                            txtBHYTTOAITHU.Text = obj.REPBHYT.ToString
                        End If

                        If obj.REPBHTN IsNot Nothing Then
                            txtBHTNTOAITHU.Text = obj.REPBHTN
                        End If
                        ' thong tin thoai thu

                        INSCHANGEDTO.ORG_ID = obj.ORG_ID
                        Employee_id = obj.EMPLOYEE_ID
                        _Value = obj.ID
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

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

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
                    ctrlFindEmployeePopup.IsHideTerminate = False
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
        Dim obj As INS_CHANGEDTO
        Dim rep As New InsuranceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If _Value.HasValue Then

                        Dim cmRep As New CommonRepository
                        Dim lstID As New List(Of Decimal)

                        lstID.Add(Convert.ToDecimal(_Value))

                        If cmRep.CheckExistIDTable(lstID, "INS_CHANGE", "ID") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                            Exit Sub
                        End If

                        obj = New INS_CHANGEDTO
                        obj.EMPLOYEE_ID = Employee_id

                        If Not String.IsNullOrEmpty(cboLoaiBienDong.SelectedValue) Then
                            obj.CHANGE_TYPE = ValueLBD
                        End If

                        obj.ISBHYT = chkbhyt.Checked
                        obj.ISBHTN = chkbhtn.Checked
                        obj.ISBHXH = chkbhxh.Checked

                        'If rdNgayHieuLuc.SelectedDate IsNot Nothing Then
                        '    obj.EFFECTDATE = rdNgayHieuLuc.SelectedDate
                        'End If
                        If rdThangBiendong.SelectedDate IsNot Nothing Then
                            obj.CHANGE_MONTH = rdThangBiendong.SelectedDate
                            obj.EFFECTDATE = New Date(rdThangBiendong.SelectedDate.Value.Year, rdThangBiendong.SelectedDate.Value.Month, 1)
                        End If

                        If Not String.IsNullOrEmpty(nmLuongKyTruoc.Text.Trim) Then
                            obj.OLDSALARY = Decimal.Parse(nmLuongKyTruoc.Text)
                        End If

                        If Not String.IsNullOrEmpty(nmLuongKyNay.Text.Trim) Then
                            obj.NEWSALARY = Decimal.Parse(nmLuongKyNay.Text)
                        End If

                        If rdNgayTraTheBHXH.SelectedDate IsNot Nothing Then
                            obj.RETURN_DATEBHXH = rdNgayTraTheBHXH.SelectedDate
                        End If

                        If rdNgayTraTheBHYT.SelectedDate IsNot Nothing Then
                            obj.RETURN_DATEBHYT = rdNgayTraTheBHYT.SelectedDate
                        End If
                        obj.NOTE = txtnote.Text.Trim
                        ' thong tin truy thu
                        If rdTuNgaytt.SelectedDate IsNot Nothing Then
                            obj.CLTFRMMONTH = rdTuNgaytt.SelectedDate
                        End If

                        If rdDenNgayTT.SelectedDate IsNot Nothing Then
                            obj.CLTTOMONTH = rdDenNgayTT.SelectedDate
                        End If

                        If Not String.IsNullOrEmpty(txtBHXHTT.Text.Trim) Then
                            obj.CLTBHXH = Decimal.Parse(txtBHXHTT.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHYTTT.Text.Trim) Then
                            obj.CLTBHYT = Decimal.Parse(txtBHYTTT.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHTNTT.Text.Trim) Then
                            obj.CLTBHTN = Decimal.Parse(txtBHTNTT.Text)
                        End If
                        ' thong tin thoai thu
                        If rdTuNgayThoaiThu.SelectedDate IsNot Nothing Then
                            obj.REPFRMMONTH = rdTuNgayThoaiThu.SelectedDate
                        End If

                        If rddenngayThoaiThu.SelectedDate IsNot Nothing Then
                            obj.REPTOMONTH = rddenngayThoaiThu.SelectedDate
                        End If

                        If Not String.IsNullOrEmpty(txtBHXHTOAITHU.Text.Trim) Then
                            obj.REPBHXH = Decimal.Parse(txtBHXHTOAITHU.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHYTTOAITHU.Text.Trim) Then
                            obj.REPBHYT = Decimal.Parse(txtBHYTTOAITHU.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHTNTOAITHU.Text.Trim) Then
                            obj.REPBHTN = Decimal.Parse(txtBHTNTOAITHU.Text)
                        End If
                        'obj.ORG_ID = INSCHANGEDTO.ORG_ID
                        obj.ID = _Value
                        rep.ModifyINS_CHANGE(obj, _Value)
                    Else
                        If cboLoaiBienDong.SelectedValue = 1 Then
                            ShowMessage("Bạn không được phép thêm loại biến động này.", NotifyType.Warning)
                            Exit Sub
                        End If

                        obj = New INS_CHANGEDTO
                        obj.EMPLOYEE_ID = Employee_id

                        If Not String.IsNullOrEmpty(cboLoaiBienDong.SelectedValue) Then
                            obj.CHANGE_TYPE = Decimal.Parse(cboLoaiBienDong.SelectedValue)
                        End If

                        obj.ISBHYT = chkbhyt.Checked
                        obj.ISBHTN = chkbhtn.Checked
                        obj.ISBHXH = chkbhxh.Checked

                        'If rdNgayHieuLuc.SelectedDate IsNot Nothing Then
                        '    obj.EFFECTDATE = rdNgayHieuLuc.SelectedDate
                        'End If
                        If rdThangBiendong.SelectedDate IsNot Nothing Then
                            obj.CHANGE_MONTH = rdThangBiendong.SelectedDate
                        End If

                        If Not String.IsNullOrEmpty(nmLuongKyTruoc.Text.Trim) Then
                            obj.OLDSALARY = Decimal.Parse(nmLuongKyTruoc.Text)
                        End If

                        If Not String.IsNullOrEmpty(nmLuongKyNay.Text.Trim) Then
                            obj.NEWSALARY = Decimal.Parse(nmLuongKyNay.Text)
                        End If

                        If rdNgayTraTheBHXH.SelectedDate IsNot Nothing Then
                            obj.RETURN_DATEBHXH = rdNgayTraTheBHXH.SelectedDate
                        End If

                        If rdNgayTraTheBHYT.SelectedDate IsNot Nothing Then
                            obj.RETURN_DATEBHYT = rdNgayTraTheBHYT.SelectedDate
                        End If

                        obj.NOTE = txtnote.Text.Trim
                        ' thong tin truy thu
                        If rdTuNgaytt.SelectedDate IsNot Nothing Then
                            obj.CLTFRMMONTH = rdTuNgaytt.SelectedDate
                        End If

                        If rdDenNgayTT.SelectedDate IsNot Nothing Then
                            obj.CLTTOMONTH = rdDenNgayTT.SelectedDate
                        End If

                        If Not String.IsNullOrEmpty(txtBHXHTT.Text.Trim) Then
                            obj.CLTBHXH = Decimal.Parse(txtBHXHTT.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHYTTT.Text.Trim) Then
                            obj.CLTBHYT = Decimal.Parse(txtBHYTTT.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHTNTT.Text.Trim) Then
                            obj.CLTBHTN = Decimal.Parse(txtBHTNTT.Text)
                        End If
                        ' thong tin thoai thu
                        If rdTuNgayThoaiThu.SelectedDate IsNot Nothing Then
                            obj.REPFRMMONTH = rdTuNgayThoaiThu.SelectedDate
                        End If

                        If rddenngayThoaiThu.SelectedDate IsNot Nothing Then
                            obj.REPTOMONTH = rddenngayThoaiThu.SelectedDate
                        End If

                        If Not String.IsNullOrEmpty(txtBHXHTOAITHU.Text.Trim) Then
                            obj.REPBHXH = Decimal.Parse(txtBHXHTOAITHU.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHYTTOAITHU.Text.Trim) Then
                            obj.REPBHYT = Decimal.Parse(txtBHYTTOAITHU.Text)
                        End If

                        If Not String.IsNullOrEmpty(txtBHTNTOAITHU.Text.Trim) Then
                            obj.REPBHTN = Decimal.Parse(txtBHTNTOAITHU.Text)
                        End If
                        'obj.ORG_ID = INSCHANGEDTO.ORG_ID
                        rep.InsertINS_CHANGE(obj, gstatus)
                    End If

                    'Dim str As String = "getRadWindow().close('1');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    ''POPUPTOLINK
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsChange&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsChange&group=Business")
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
        Dim dtLuong As New DataTable

        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            Dim infoIns As New INS_INFORMATIONDTO
            infoIns = rep.GetInfoInsByEmpID(empID)
            dtLuong = rep.GETLUONGBIENDONG(empID)

            If infoIns IsNot Nothing Then
                emp = rep.GetEmployeeById(empID)
                txtEmployeeCode.Text = emp.EMPLOYEE_CODE
                txtChucDanh.Text = emp.TITLE_NAME
                txtcmnd.Text = emp.CMND
                txtDonVi.Text = emp.ORG_NAME
                txtName.Text = emp.VN_FULLNAME
                txtNoiCap.Text = emp.NOICAP
                chkbhxh.Checked = emp.SO
                chkbhyt.Checked = emp.HE
                chkbhtn.Checked = emp.UE

                If emp.NGAYSINH.HasValue Then
                    txtNgaysinh.Text = emp.NGAYSINH.Value.ConvertString
                End If

                txtNoisinh.Text = emp.NOISINH
                Employee_id = emp.EMPLOYEE_ID

                If dtLuong IsNot Nothing AndAlso dtLuong.Rows.Count > 0 Then
                    nmLuongKyTruoc.Value = CDbl(dtLuong.Rows(0)("OLDSALARY"))
                    nmLuongKyNay.Value = CDbl(dtLuong.Rows(0)("NEWSALARY"))
                End If

                ClearControlValue(cboLoaiBienDong, rdThangBiendong, rdNgayTraTheBHXH, rdNgayTraTheBHYT, txtnote,
                                  rdTuNgaytt, rdDenNgayTT, txtBHXHTT, txtBHYTTT, txtBHTNTT, rdTuNgayThoaiThu,
                                  rddenngayThoaiThu, txtBHXHTOAITHU, txtBHYTTOAITHU, txtBHTNTOAITHU)
            Else
                ShowMessage("Nhân viên chưa được khai báo thông tin bảo hiểm hoặc chưa có biến động tăng mới!", NotifyType.Warning)
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
    ''' Event xử lý khi RadDateTime TỪ NGÀY TRUY THU thay đổi giá trị
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdTuNgaytt_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdTuNgaytt.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fromDate As New Date
        Dim toDate As New Date
        Dim totalMonth As New Decimal
        Dim dtDataTile As New DataTable
        Dim tlBhxh As New Decimal
        Dim tlBhyt As New Decimal
        Dim tlBhtn As New Decimal
        Dim rep As New InsuranceRepository
        Dim oldSalary As Decimal = Utilities.ObjToDecima(nmLuongKyTruoc.Value)
        Dim newSalary As Decimal = Utilities.ObjToDecima(nmLuongKyNay.Value)

        Try
            If rdTuNgaytt.SelectedDate IsNot Nothing Then
                If rdTuNgaytt.SelectedDate.Value.Day < 15 Then
                    fromDate = rdTuNgaytt.SelectedDate
                Else
                    fromDate = rdTuNgaytt.SelectedDate.Value.AddMonths(1)
                End If
            Else
                ClearControlValue(txtBHXHTT, txtBHTNTT, txtBHYTTT)
                Exit Sub
            End If

            If rdDenNgayTT.SelectedDate IsNot Nothing Then
                If rdDenNgayTT.SelectedDate.Value.Day < 15 Then
                    toDate = rdDenNgayTT.SelectedDate
                Else
                    toDate = rdDenNgayTT.SelectedDate.Value.AddMonths(1)
                End If
            Else
                ClearControlValue(txtBHXHTT, txtBHTNTT, txtBHYTTT)
                Exit Sub
            End If

            ' kiểm tra nếu trong cùng 1 tháng biến động thì sẽ không tính truy thu, thoái thu
            Dim denngayMax As Date = rdTuNgaytt.SelectedDate
            denngayMax = New Date(denngayMax.Year, denngayMax.Month, 15)
            Dim TungayNext As Date = rdTuNgaytt.SelectedDate.Value.AddMonths(1)
            TungayNext = New Date(TungayNext.Year, TungayNext.Month, 15)

            If (rdTuNgaytt.SelectedDate.Value.Day < 15 _
                    And rdDenNgayTT.SelectedDate < denngayMax) _
                 Or (rdTuNgaytt.SelectedDate.Value.Day > 15 _
                     And rdDenNgayTT.SelectedDate <= TungayNext) Then
                txtBHXHTT.Text = 0
                txtBHYTTT.Text = 0
                txtBHTNTT.Text = 0
                Exit Sub
            End If

            totalMonth = Utilities.ObjToInt(toDate.Month) - Utilities.ObjToInt(fromDate.Month) + 1
            dtDataTile = rep.GetTiLeDong()

            If dtDataTile IsNot Nothing Then
                tlBhxh = dtDataTile.Rows(0)("SI_EMP")
                tlBhyt = dtDataTile.Rows(0)("HI_EMP")
                tlBhtn = dtDataTile.Rows(0)("UI_EMP")
            End If

            If chkbhxh.Checked Then
                txtBHXHTT.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhxh / 100) * Utilities.ObjToDecima(newSalary - oldSalary)
            End If

            If chkbhyt.Checked Then
                txtBHYTTT.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhyt / 100) * Utilities.ObjToDecima(newSalary - oldSalary)
            End If

            If chkbhtn.Checked Then
                txtBHTNTT.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhtn / 100) * Utilities.ObjToDecima(newSalary - oldSalary)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xử lý khi RadDateTime ĐẾN NGÀY TRUY THU thay đổi giá trị
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdDenNgayTT_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdDenNgayTT.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rdTuNgaytt_SelectedDateChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xử lý khi RadDateTime TỪ NGÀY THOÁI THU thay đổi giá trị
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdTuNgayThoaiThu_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdTuNgayThoaiThu.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fromDate As New Date
        Dim toDate As New Date
        Dim totalMonth As New Decimal
        Dim dtDataTile As New DataTable
        Dim tlBhxh As New Decimal
        Dim tlBhyt As New Decimal
        Dim tlBhtn As New Decimal
        Dim rep As New InsuranceRepository
        Dim oldSalary As Decimal = Utilities.ObjToDecima(nmLuongKyTruoc.Value)
        Dim newSalary As Decimal = Utilities.ObjToDecima(nmLuongKyNay.Value)

        Try
            If rdTuNgayThoaiThu.SelectedDate IsNot Nothing Then
                If rdTuNgayThoaiThu.SelectedDate.Value.Day < 15 Then
                    fromDate = rdTuNgayThoaiThu.SelectedDate
                Else
                    fromDate = rdTuNgayThoaiThu.SelectedDate.Value.AddMonths(1)
                End If
            Else
                ClearControlValue(txtBHTNTOAITHU, txtBHXHTOAITHU, txtBHYTTOAITHU)
                Exit Sub
            End If

            If rddenngayThoaiThu.SelectedDate IsNot Nothing Then
                If rddenngayThoaiThu.SelectedDate.Value.Day < 15 Then
                    toDate = rddenngayThoaiThu.SelectedDate
                Else
                    toDate = rddenngayThoaiThu.SelectedDate.Value.AddMonths(1)
                End If
            Else
                ClearControlValue(txtBHTNTOAITHU, txtBHXHTOAITHU, txtBHYTTOAITHU)
                Exit Sub
            End If

            ' kiểm tra nếu trong cùng 1 tháng biến động thì sẽ không tính truy thu, thoái thu
            Dim denngayMax As Date = rdTuNgayThoaiThu.SelectedDate
            denngayMax = New Date(denngayMax.Year, denngayMax.Month, 15)
            Dim TungayNext As Date = rdTuNgayThoaiThu.SelectedDate.Value.AddMonths(1)
            TungayNext = New Date(TungayNext.Year, TungayNext.Month, 15)

            If (rdTuNgayThoaiThu.SelectedDate.Value.Day < 15 _
                    And rddenngayThoaiThu.SelectedDate < denngayMax) _
                 Or (rdTuNgayThoaiThu.SelectedDate.Value.Day > 15 _
                     And rddenngayThoaiThu.SelectedDate <= TungayNext) Then
                txtBHXHTT.Text = 0
                txtBHYTTT.Text = 0
                txtBHTNTT.Text = 0
                Exit Sub
            End If

            totalMonth = Utilities.ObjToInt(toDate.Month) - Utilities.ObjToInt(fromDate.Month) + 1
            dtDataTile = rep.GetTiLeDong()

            If dtDataTile IsNot Nothing Then
                tlBhxh = dtDataTile.Rows(0)("SI_EMP")
                tlBhyt = dtDataTile.Rows(0)("HI_EMP")
                tlBhtn = dtDataTile.Rows(0)("UI_EMP")
            End If

            If chkbhxh.Checked Then
                txtBHXHTOAITHU.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhxh / 100) * Utilities.ObjToDecima(oldSalary - newSalary)
            End If

            If chkbhyt.Checked Then
                txtBHYTTOAITHU.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhyt / 100) * Utilities.ObjToDecima(oldSalary - newSalary)
            End If

            If chkbhtn.Checked Then
                txtBHTNTOAITHU.Text = Utilities.ObjToInt(totalMonth) * Utilities.ObjToDecima(tlBhtn / 100) * Utilities.ObjToDecima(oldSalary - newSalary)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xử lý khi RadDateTime ĐẾN NGÀY THOÁI THU thay đổi giá trị
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rddenngayThoaiThu_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rddenngayThoaiThu.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rdTuNgayThoaiThu_SelectedDateChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox LOẠI BIẾN ĐỘNG có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalLoaiBienDong_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalLoaiBienDong.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            ValueLBD = cboLoaiBienDong.SelectedValue
            ListComboData = New ComboBoxDataDTO
            Dim dto As New INS_CHANGE_TYPEDTO
            Dim list As New List(Of INS_CHANGE_TYPEDTO)

            dto.ID = Convert.ToDecimal(cboLoaiBienDong.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_CHANGETYPE = True
            ListComboData.LIST_LIST_CHANGETYPE = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cboLoaiBienDong, ListComboData.LIST_LIST_CHANGETYPE, "ARISING_NAME", "ID", True)
                ClearControlValue(cboLoaiBienDong)
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
                ListComboData.GET_LIST_CHANGETYPE = True
                rep.GetComboboxData(ListComboData)
            End If

            FillRadCombobox(cboLoaiBienDong, ListComboData.LIST_LIST_CHANGETYPE, "ARISING_NAME", "ID", True)
            If ListComboData.LIST_LIST_CHANGETYPE.Count > 0 Then
                cboLoaiBienDong.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class

