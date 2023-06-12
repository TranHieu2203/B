Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsRemigeManagerNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Cho phép hiển thị popup nào?
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

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Trang ở trạng thái đang sửa?
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

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Mã nhân viên
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

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Value
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

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Chế độ bảo hiểm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property INSREMIGE As INS_REMIGE_MANAGER_DTO
        Get
            Return ViewState(Me.ID & "_INSREMIGES")
        End Get
        Set(ByVal value As INS_REMIGE_MANAGER_DTO)
            ViewState(Me.ID & "_INSREMIGES") = value
        End Set
    End Property

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách combobox
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
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewInit -  khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Load dữ liệu lên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạoc các giá trị trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Session.Remove("StateAction")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các thành phần trên trang trạng thái trang - view
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New INS_REMIGE_MANAGER_DTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetRegimeManagerByID(obj.ID)
                    INSREMIGE = New INS_REMIGE_MANAGER_DTO
                    If obj IsNot Nothing Then
                        txtEMPLOYEE_CODE.Text = obj.EMPLOYEE_CODE
                        hidEmpID.Value = obj.EMPLOYEE_ID
                        hidID.Value = obj.ID
                        txtEMPLOYEE_NAME.Text = obj.EMPLOYEE_NAME
                        txtTITLE_NAME.Text = obj.TITLE_NAME
                        txtORG_NAME.Text = obj.ORG_NAME
                        If obj.BIRTH_DATE.HasValue Then
                            txtNgaySinh.Text = obj.BIRTH_DATE.Value.ConvertString
                        End If
                        txtCapNS.Text = obj.STAFF_RANK_NAME
                        If obj.ENTITLED_ID IsNot Nothing Then
                            'check exist
                            cboRemigeType.SelectedValue = obj.ENTITLED_ID
                            Dim ins_group As New INS_GROUP_REGIMESDTO
                            ins_group.ACTFLG = "A"
                            If (cboRemigeType.SelectedValue <> "") Then
                                ins_group.ID = Decimal.Parse(cboRemigeType.SelectedValue)
                                Using re As New InsuranceRepository
                                    If re.ValidateGroupRegime(ins_group) Then
                                        If ListComboData Is Nothing Then
                                            ListComboData = New ComboBoxDataDTO
                                            ListComboData.GET_REMIGE_TYPE = True
                                            rep.GetComboboxData(ListComboData)
                                        End If
                                        cboRemigeType.ClearValue()
                                        FillDropDownList(cboRemigeType, ListComboData.LIST_REMIGE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRemigeType.SelectedValue)
                                    End If
                                End Using
                            Else
                                If ListComboData Is Nothing Then
                                    ListComboData = New ComboBoxDataDTO
                                    ListComboData.GET_REMIGE_TYPE = True
                                    rep.GetComboboxData(ListComboData)
                                End If
                                cboRemigeType.ClearValue()
                                FillDropDownList(cboRemigeType, ListComboData.LIST_REMIGE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRemigeType.SelectedValue)

                            End If



                        End If
                        chkis_FOCUSED.Checked = obj.is_FOCUSED
                        chkIS_ATHOME.Checked = obj.is_ATHOME
                        txtSoBHXH.Text = obj.INSURRANCE_NUM
                        txtTheBHXH.Text = obj.BOOK_NUM
                        dpFromDate.SelectedDate = obj.START_DATE
                        dpToDate.SelectedDate = obj.END_DATE
                        rdThangBiendong.SelectedDate = obj.CHANGE_MONTH
                        nmNUM_DATE.Value = obj.NUM_DATE
                        rdDATE_OFF_BIRT.SelectedDate = obj.DATE_OFF_BIRT
                        txtBABY_NAME.Text = obj.BABY_NAME
                        nmNUM_BABY.Value = obj.NUM_BABY
                        nmACCUMULATED_DATE.Value = obj.ACCUMULATED_DATE
                        nmALLOWANCE_SAL.Value = obj.ALLOWANCE_SAL
                        nmALLOWANCE_MONEY.Value = obj.ALLOWANCE_MONEY

                        nmALLOWANCE_MONEY_EDIT.Value = obj.ALLOWANCE_MONEY_EDIT
                        txtCONDITION_ALLOWANCE.Text = obj.CONDITION_ALLOWANCE
                        txtTIME_ALLOWANCE.Text = obj.TIME_ALLOWANCE
                        txtSDESC.Text = obj.SDESC
                        'nmAPPROVAL_INSURRANCE.Value = obj.APPROVAL_INSURRANCE
                        'dpAPPROVAL_DATE.SelectedDate = obj.APPROVAL_DATE
                        nmAPPROVAL_NUM.Value = obj.APPROVAL_NUM
                        _Value = obj.ID
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click buttom hủy trên control ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click button btnEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                  ByVal e As EventArgs) Handles btnFindEmployee.Click
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
                    ctrlFindEmployeePopup.IsHideTerminate = False
                    ctrlFindEmployeePopup.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Click khi click lên item trên main toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim objData As INS_REMIGE_MANAGER_DTO
        Dim rep As New InsuranceRepository
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If _Value.HasValue Then
                            gID = Utilities.ObjToDecima(hidID.Value)
                            Dim ins_manager As New INS_REMIGE_MANAGER_DTO
                            ins_manager.ID = gID
                            Using re As New InsuranceRepository
                                If re.ValidateRegimeManager(ins_manager) Then
                                    Session("StateAction") = 1
                                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManager&group=Business")
                                End If
                            End Using
                            objData = New INS_REMIGE_MANAGER_DTO
                            objData.ID = hidID.Value
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            If (cboRemigeType.SelectedValue <> "") Then
                                objData.ENTITLED_ID = Utilities.ObjToDecima(cboRemigeType.SelectedValue)
                            End If
                            objData.START_DATE = dpFromDate.SelectedDate
                            objData.END_DATE = dpToDate.SelectedDate
                            objData.CHANGE_MONTH = rdThangBiendong.SelectedDate
                            objData.NUM_DATE = nmNUM_DATE.Value
                            objData.DATE_OFF_BIRT = rdDATE_OFF_BIRT.SelectedDate
                            objData.BABY_NAME = txtBABY_NAME.Text.Trim
                            objData.NUM_BABY = nmNUM_BABY.Value
                            objData.ACCUMULATED_DATE = nmACCUMULATED_DATE.Value
                            objData.ALLOWANCE_SAL = nmALLOWANCE_SAL.Value
                            objData.ALLOWANCE_MONEY = nmALLOWANCE_MONEY.Value
                            objData.ALLOWANCE_MONEY_EDIT = nmALLOWANCE_MONEY_EDIT.Value
                            objData.CONDITION_ALLOWANCE = txtCONDITION_ALLOWANCE.Text.Trim
                            objData.TIME_ALLOWANCE = txtTIME_ALLOWANCE.Text.Trim
                            objData.SDESC = txtSDESC.Text.Trim
                            objData.is_ATHOME = chkIS_ATHOME.Checked

                            'objData.APPROVAL_INSURRANCE = nmAPPROVAL_INSURRANCE.Value
                            'objData.APPROVAL_DATE = dpAPPROVAL_DATE.SelectedDate
                            objData.APPROVAL_NUM = nmAPPROVAL_NUM.Value


                            rep.ModifyRegimeManager(objData, gID)
                        Else
                            objData = New INS_REMIGE_MANAGER_DTO
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            If (cboRemigeType.SelectedValue <> "") Then
                                objData.ENTITLED_ID = Utilities.ObjToDecima(cboRemigeType.SelectedValue)
                            Else

                            End If
                            objData.START_DATE = dpFromDate.SelectedDate
                            objData.END_DATE = dpToDate.SelectedDate
                            objData.CHANGE_MONTH = rdThangBiendong.SelectedDate
                            objData.NUM_DATE = nmNUM_DATE.Value
                            objData.DATE_OFF_BIRT = rdDATE_OFF_BIRT.SelectedDate
                            objData.BABY_NAME = txtBABY_NAME.Text.Trim
                            objData.NUM_BABY = nmNUM_BABY.Value
                            objData.ACCUMULATED_DATE = nmACCUMULATED_DATE.Value
                            objData.ALLOWANCE_SAL = nmALLOWANCE_SAL.Value
                            objData.ALLOWANCE_MONEY = nmALLOWANCE_MONEY.Value
                            objData.ALLOWANCE_MONEY_EDIT = nmALLOWANCE_MONEY_EDIT.Value
                            objData.CONDITION_ALLOWANCE = txtCONDITION_ALLOWANCE.Text.Trim
                            objData.TIME_ALLOWANCE = txtTIME_ALLOWANCE.Text.Trim
                            objData.SDESC = txtSDESC.Text.Trim
                            'objData.APPROVAL_INSURRANCE = nmAPPROVAL_INSURRANCE.Value
                            'objData.APPROVAL_DATE = dpAPPROVAL_DATE.SelectedDate
                            objData.APPROVAL_NUM = nmAPPROVAL_NUM.Value
                            objData.is_ATHOME = chkIS_ATHOME.Checked
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertRegimeManager(objData, gstatus)
                        End If
                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK
                        Session("StateAction") = 2
                        Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManager&group=Business")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManager&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện lưu dữ liệu cho trường hợp thêm mới hoặc sửa
    ''' </summary>
    ''' <param name="objData"></param>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal objData As INS_REMIGE_MANAGER_DTO, ByVal gID As Decimal)
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.InsertRegimeManager(objData, gID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    objData.ID = gID
                    'check exist db
                    Dim Validate As New INS_REMIGE_MANAGER_DTO
                    Validate.ID = objData.ID
                    If rep.ValidateRegimeManager(Validate) Then

                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsRemigeManager&group=Business")
                    End If
                    If rep.ModifyRegimeManager(objData, gID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý xóa dữ liệu chế độ bảo hiểm theo danh sách ID
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal gID As List(Of Decimal))
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rep.DeleteRegimeManager(gID) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý lấy dữ liệu cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_REMIGE_TYPE = True
                rep.GetComboboxData(ListComboData)
            End If
            cboRemigeType.ClearValue()
            FillDropDownList(cboRemigeType, ListComboData.LIST_REMIGE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRemigeType.SelectedValue)

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy chế độ hưởng bảo hiểm hiện tạicủa nhân viên
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Cur_ChedoHuong()
        Dim fromDate As New Date
        Dim employeeID As New Integer
        Dim ENTITLED_ID As New Integer
        Dim dtData As New DataTable
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dpFromDate.SelectedDate IsNot Nothing AndAlso dpToDate.SelectedDate IsNot Nothing AndAlso cboRemigeType.SelectedValue IsNot Nothing AndAlso cboRemigeType.SelectedValue <> "" Then
                fromDate = dpFromDate.SelectedDate
                Dim tungay As New Date(fromDate.Year, 1, 1)
                employeeID = hidEmpID.Value
                If (cboRemigeType.SelectedValue <> "") Then
                    ENTITLED_ID = cboRemigeType.SelectedValue
                End If
                dtData = rep.GetTienHuong(Utilities.ObjToInt(nmNUM_DATE.Value), Utilities.ObjToDecima(chkIS_ATHOME.Checked), employeeID, ENTITLED_ID, Utilities.ObjToDecima(nmALLOWANCE_SAL.Value), fromDate, Utilities.ObjToInt(nmNUM_BABY.Value))
                If dtData IsNot Nothing Then
                    nmALLOWANCE_MONEY.Value = Utilities.ObjToDecima(dtData.Rows(0)("SALARY"))
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    cboRemigeType.AutoPostBack = False
                    dpFromDate.AutoPostBack = False
                    dpToDate.AutoPostBack = False
                    'Case Nothing
                    '    cboRemigeType.AutoPostBack = False
                    '    dpFromDate.AutoPostBack = False
                    '    dpToDate.AutoPostBack = False
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
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

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý chọn danh sách nhân viên trên popup nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim dtLuongBH As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim infoIns As New INS_INFORMATIONDTO
                infoIns = rep.GetInfoInsByEmpID(lstCommonEmployee(0).EMPLOYEE_ID)
                dtLuongBH = rep.GetLuongBH(lstCommonEmployee(0).EMPLOYEE_ID)
                If infoIns IsNot Nothing Then
                    hidEmpID.Value = lstCommonEmployee(0).EMPLOYEE_ID
                    txtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                    txtORG_NAME.Text = lstCommonEmployee(0).ORG_NAME
                    txtTITLE_NAME.Text = lstCommonEmployee(0).TITLE_NAME
                    txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN
                    If lstCommonEmployee(0).BIRTH_DATE.HasValue Then
                        txtNgaySinh.Text = lstCommonEmployee(0).BIRTH_DATE.Value.ConvertString
                    End If
                    txtCapNS.Text = lstCommonEmployee(0).STAFF_RANK_NAME
                    'txtBirtAddress.Text = lstCommonEmployee(0).BIRTH_PLACE
                    txtSoBHXH.Text = infoIns.SOBOOKNO
                    txtTheBHXH.Text = infoIns.HECARDNO
                    If dtLuongBH.Rows.Count > 0 Then
                        nmALLOWANCE_SAL.Value = CDbl(dtLuongBH.Rows(0)("TOTAL")) + If(rep.GetAllowanceTotalByDate(lstCommonEmployee(0).EMPLOYEE_ID).HasValue, rep.GetAllowanceTotalByDate(lstCommonEmployee(0).EMPLOYEE_ID), 0)
                    End If
                Else
                    ShowMessage("Nhân viên chưa được khai báo thông tin bảo hiểm hoặc chưa có biến động tăng mới!!", NotifyType.Warning)
                    Exit Sub
                End If
            End If
            ExcuteScript("Script", " registerOnfocusOut('ctl00_MainContent_ctrlInsRemigeManagerNewEdit_RadSplitter3')")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SeletedDateChanged khi thay đổi giá trị cho control rdFromDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dpFromDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpFromDate.SelectedDateChanged
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dpFromDate.SelectedDate IsNot Nothing Then
                If (cboRemigeType.SelectedValue IsNot Nothing And cboRemigeType.SelectedValue <> "") Then
                    If (cboRemigeType.SelectedValue = 127) Then
                        dpToDate.SelectedDate = dpFromDate.SelectedDate.Value.AddMonths(6)
                    End If
                End If
            End If
            If dpToDate.SelectedDate IsNot Nothing Then
                Dim fromDate As New Date
                Dim toDate As New Date
                fromDate = dpFromDate.SelectedDate
                toDate = dpToDate.SelectedDate
                If (cboRemigeType.SelectedValue IsNot Nothing And cboRemigeType.SelectedValue <> "") Then
                    If cboRemigeType.SelectedValue = 127 Then ' nếu là thai san thì sẽ tính cả thứ 7 và chủ nhật, không phải thai sản thì bỏ chủ nhật.
                        nmNUM_DATE.Value = DateDiff(DateInterval.Day, fromDate, toDate) + 1
                    Else
                        nmNUM_DATE.Value = CDbl(rep.CALCULATOR_DAY(fromDate, toDate).Rows(0)("COUNTDATA"))
                    End If
                End If
                Cur_Luyke()
                Cur_ChedoHuong()
            End If
            ExcuteScript("Script", " registerOnfocusOut('ctl00_MainContent_ctrlInsRemigeManagerNewEdit_RadSplitter3')")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SeletedDateChanged khi thay đổi giá trị cho control rdToDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dpToDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpToDate.SelectedDateChanged
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dpFromDate.SelectedDate IsNot Nothing Then
                Dim fromDate As New Date
                Dim toDate As New Date
                fromDate = dpFromDate.SelectedDate
                toDate = dpToDate.SelectedDate
                If (cboRemigeType.SelectedValue IsNot Nothing And cboRemigeType.SelectedValue <> "") Then
                    If cboRemigeType.SelectedValue = 127 Then ' nếu là thai san thì sẽ tính cả thứ 7 và chủ nhật, không phải thai sản thì bỏ chủ nhật.
                        If fromDate = toDate Then
                            nmNUM_DATE.Value = 1
                        Else
                            nmNUM_DATE.Value = DateDiff(DateInterval.Day, fromDate, toDate)
                        End If
                    Else
                        nmNUM_DATE.Value = CDbl(rep.CALCULATOR_DAY(fromDate, toDate).Rows(0)("COUNTDATA"))
                    End If
                End If
                Cur_Luyke()
                Cur_ChedoHuong()
            End If
            ExcuteScript("Script", " registerOnfocusOut('ctl00_MainContent_ctrlInsRemigeManagerNewEdit_RadSplitter3')")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged của control cboRemige
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboRemigeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboRemigeType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dpFromDate_SelectedDateChanged(Nothing, Nothing)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Thông tin lũy kế hiện tại của nhân viên
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Cur_Luyke()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim denngay As New Date
            Dim fromDate As New Date
            Dim employeeID As New Integer
            Dim ENTITLED_ID As New Integer
            Dim dtData As New DataTable
            Dim dtMaxDay As New DataTable
            Dim rep As New InsuranceRepository
            If dpFromDate.SelectedDate IsNot Nothing AndAlso dpToDate.SelectedDate IsNot Nothing AndAlso cboRemigeType.SelectedValue IsNot Nothing AndAlso cboRemigeType.SelectedValue <> "" Then
                fromDate = dpFromDate.SelectedDate
                Dim tungay As New Date(fromDate.Year, 1, 1)
                denngay = dpToDate.SelectedDate
                employeeID = hidEmpID.Value
                If (cboRemigeType.SelectedValue <> "") Then
                    ENTITLED_ID = cboRemigeType.SelectedValue
                End If
                dtData = rep.GetLuyKe(tungay, denngay, employeeID, ENTITLED_ID)
                If dtData Is Nothing Then Exit Sub
                nmACCUMULATED_DATE.Value = Utilities.ObjToInt(dtData.Rows(0)("LUYKE_YEAR")) + Utilities.ObjToInt(nmNUM_DATE.Value) - Utilities.ObjToInt(nmAPPROVAL_NUM.Value)
                If nmACCUMULATED_DATE.Value > 0 Then
                    dtMaxDay = rep.GetMaxDayByID(ENTITLED_ID) ' lấy ra tổng số ngày nghỉ của chế độ thiết lập ban đầu trên form danh mục
                    If dtMaxDay IsNot Nothing Or cboRemigeType.SelectedValue <> 127 Then
                        Dim total = CInt(dtMaxDay.Rows(0)("DAY_OFF_SUMMARY")) - CInt(nmACCUMULATED_DATE.Value)
                        ShowMessage("Tổng số ngày nghỉ còn lại của chế độ này là: " & total, NotifyType.Warning)
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện textchanged cho control nmAPPROVAL_NUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub nmAPPROVAL_NUM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nmAPPROVAL_NUM.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dpToDate_SelectedDateChanged(Nothing, Nothing)
            ExcuteScript("Script", " registerOnfocusOut('ctl00_MainContent_ctrlInsRemigeManagerNewEdit_RadSplitter3')")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ServerValidate của control cvalToDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalToDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalToDate.ServerValidate
        Dim rep As New InsuranceRepository
        Dim _validate As New INS_REMIGE_MANAGER_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _validate.ID = Utilities.ObjToInt(hidID.Value)
            _validate.EMPLOYEE_ID = hidEmpID.Value
            _validate.START_DATE = dpFromDate.SelectedDate
            _validate.END_DATE = dpToDate.SelectedDate
            args.IsValid = rep.ValidateRegimeManager(_validate)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    '''  Xử lý sự kiện ServerValidate của control cvalRemigeType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalRemigeType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRemigeType.ServerValidate
        Dim rep As New InsuranceRepository
        Dim _validate As New INS_REMIGE_MANAGER_DTO
        Dim dtData As New DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboRemigeType.SelectedValue = 126 Then
                _validate.ID = Utilities.ObjToInt(hidID.Value)
                _validate.EMPLOYEE_ID = hidEmpID.Value
                If cboRemigeType.SelectedValue <> "" Then
                    _validate.ENTITLED_ID = cboRemigeType.SelectedValue
                End If
                _validate.START_DATE = dpFromDate.SelectedDate
                _validate.END_DATE = dpToDate.SelectedDate
                dtData = rep.Validate_KhamThai(_validate)
                ' không cho phép đăng ký nghỉ kham thái quá 5 lần trên 1 năm
                If Utilities.ObjToInt(dtData.Rows(0)(0)) < 5 Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CheckedChanged cho control chkIs_ATHOME
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkIS_ATHOME_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIS_ATHOME.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Cur_ChedoHuong()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CheckedChanged cho control chkis_FOCUSED
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkis_FOCUSED_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkis_FOCUSED.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Cur_ChedoHuong()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/09/2017 11:39
    ''' </lastupdate>
    ''' <summary>
    '''  Xử lý sự kiện ServerValidate của control cvalRemigeType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusRemigeType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusRemigeType.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim ins_group As New INS_GROUP_REGIMESDTO
            ins_group.ACTFLG = "A"
            ins_group.ID = cboRemigeType.SelectedValue
            Using re As New InsuranceRepository
                If Not re.ValidateGroupRegime(ins_group) Then
                    args.IsValid = True
                Else
                    args.IsValid = False
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class

