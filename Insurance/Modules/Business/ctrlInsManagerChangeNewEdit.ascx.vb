Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog


Public Class ctrlInsManagerChangeNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

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
    Property INSREMIGE As INS_REMIGE_MANAGER_DTO
        Get
            Return ViewState(Me.ID & "_INSREMIGES")
        End Get
        Set(ByVal value As INS_REMIGE_MANAGER_DTO)
            ViewState(Me.ID & "_INSREMIGES") = value
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
    ''' 06/09/2017 14:00
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
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
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
                    Dim obj As New INS_CHANGE_INFO_DTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetChangeInfoById(obj.ID)

                    INSREMIGE = New INS_REMIGE_MANAGER_DTO
                    If obj IsNot Nothing Then

                        hidEmpID.Value = obj.EMPLOYEE_ID
                        hidID.Value = obj.ID
                        txtEMPLOYEE_CODE.Text = obj.EMPLOYEE_CODE
                        txtEMPLOYEE_NAME.Text = obj.EMPLOYEE_NAME
                        txtORG_NAME.Text = obj.ORG_NAME
                        txtTITLE_NAME.Text = obj.TITLE_NAME

                        txtContentChange.Text = obj.CONTENT_CHANGE
                        txtoldinfo.Text = obj.OLD_INFO
                        txtnewinfo.Text = obj.NEW_INFO
                        txtreasonchange.Text = obj.REASON_CHANGE

                        If obj.TYPE_CHANGE_ID IsNot Nothing Then
                            cboTypeChange.SelectedValue = obj.TYPE_CHANGE_ID
                            cboTypeChange.Text = obj.TYPE_CHANGE_NAME
                        End If
                        If obj.MONTH_DECLARE IsNot Nothing Then
                            rdMonth.SelectedDate = obj.MONTH_DECLARE
                        End If
                        If obj.DATE_CHANGE IsNot Nothing Then
                            rdDateChange.SelectedDate = obj.DATE_CHANGE
                        End If

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

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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
            'hidEmpID.Value = Nothing
            'txtEMPLOYEE_CODE.Text = ""
            'txtORG_NAME.Text = ""
            'txtTITLE_NAME.Text = ""
            'txtEMPLOYEE_NAME.Text = ""
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
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
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As INS_CHANGE_INFO_DTO
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
                            objData = New INS_CHANGE_INFO_DTO
                            objData.ID = hidID.Value
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            objData.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text.Trim
                            objData.EMPLOYEE_NAME = txtEMPLOYEE_NAME.Text.Trim
                            objData.ORG_NAME = txtORG_NAME.Text.Trim
                            objData.TITLE_NAME = txtTITLE_NAME.Text.Trim

                            objData.TYPE_CHANGE_ID = CDec(Val(cboTypeChange.SelectedValue))
                            objData.DATE_CHANGE = rdDateChange.SelectedDate
                            objData.CONTENT_CHANGE = txtContentChange.Text
                            objData.OLD_INFO = txtoldinfo.Text
                            objData.NEW_INFO = txtnewinfo.Text
                            objData.REASON_CHANGE = txtreasonchange.Text
                            If rdMonth.SelectedDate IsNot Nothing Then
                                objData.MONTH_DECLARE = rdMonth.SelectedDate
                            End If
                            If rep.CheckDouble(objData.ID, objData.EMPLOYEE_ID, objData.TYPE_CHANGE_ID, objData.DATE_CHANGE) > 0 Then
                                ShowMessage(Translate("Thông tin bị trùng"), NotifyType.Warning)
                                Exit Sub
                            End If
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertOrModifyChangeInfo(objData, gID)
                        Else
                            objData = New INS_CHANGE_INFO_DTO
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            objData.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text.Trim
                            objData.EMPLOYEE_NAME = txtEMPLOYEE_NAME.Text.Trim
                            objData.ORG_NAME = txtORG_NAME.Text.Trim
                            objData.TITLE_NAME = txtTITLE_NAME.Text.Trim

                            objData.TYPE_CHANGE_ID = CDec(Val(cboTypeChange.SelectedValue))
                            objData.DATE_CHANGE = rdDateChange.SelectedDate
                            objData.CONTENT_CHANGE = txtContentChange.Text
                            objData.OLD_INFO = txtoldinfo.Text
                            objData.NEW_INFO = txtnewinfo.Text
                            objData.REASON_CHANGE = txtreasonchange.Text
                            If rdMonth.SelectedDate IsNot Nothing Then
                                objData.MONTH_DECLARE = rdMonth.SelectedDate
                            End If
                            If rep.CheckDouble(0, objData.EMPLOYEE_ID, objData.TYPE_CHANGE_ID, objData.DATE_CHANGE) > 0 Then
                                ShowMessage(Translate("Thông tin bị trùng"), NotifyType.Warning)
                                Exit Sub
                            End If
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertOrModifyChangeInfo(objData, gID)
                        End If
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho control cboLEVEL
    ''' </summary>
    ''' <param name="args"></param>
    ''' <param name="source"></param>
    ''' <remarks></remarks>
    'Private Sub cvalLEVER_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalLEVEL.ServerValidate
    '    Dim rep As New InsuranceRepository
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If cboLEVEL.SelectedValue <> "" Then
    '            Dim _validate As New INS_COST_FOLLOW_LEVERDTO
    '            _validate.ID = cboLEVEL.SelectedValue
    '            _validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateINS_COST_FOLLOW_LEVER(_validate)
    '            If Not args.IsValid Then
    '                GetDataCombo()
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_INS_TYPE_CHANGE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboTypeChange, ListComboData.LIST_LIST_INS_TYPE_CHANGE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTypeChange.SelectedValue)
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
    ''' 06/09/2017 14:00
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
    ''' 06/09/2017 14:00
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidEmpID.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                txtORG_NAME.Text = lstCommonEmployee(0).ORG_NAME
                txtTITLE_NAME.Text = lstCommonEmployee(0).TITLE_NAME
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtEMPLOYEE_CODE_TextChanged(sender As Object, e As EventArgs) Handles txtEMPLOYEE_CODE.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            hidEmpID.Value = Nothing
            'txtEMPLOYEE_CODE.Text = ""
            txtORG_NAME.Text = ""
            txtTITLE_NAME.Text = ""
            txtEMPLOYEE_NAME.Text = ""
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If txtEMPLOYEE_CODE.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEMPLOYEE_CODE.Text = ""
                ElseIf Count = 1 Then
                    Dim item = EmployeeList(0)
                    hidEmpID.Value = item.EMPLOYEE_ID
                    txtEMPLOYEE_CODE.Text = item.EMPLOYEE_CODE
                    txtORG_NAME.Text = item.ORG_NAME
                    txtTITLE_NAME.Text = item.TITLE_NAME
                    txtEMPLOYEE_NAME.Text = item.FULLNAME_VN
                    isLoadPopup = 0
                ElseIf Count > 1 Then
                    If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                        'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                    End If
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    'ctrlFindEmployeePopup.MustHaveContract = True
                    ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEMPLOYEE_CODE.Text
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.Show()
                    isLoadPopup = 1
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class

