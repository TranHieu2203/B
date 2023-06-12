Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlDeclareEntitlementNBNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    Property STAFF_RANK_ID As Integer
        Get
            Return ViewState(Me.ID & "_STAFF_RANK_ID")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_STAFF_RANK_ID") = value
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
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane2)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set value cho rntxtYear
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dt As DataTable
        Try
            Using rep As New AttendanceRepository
                dt = rep.GetOtherList("AT_MODIFY_TYPE", False)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    FillRadCombobox(cboModifyType, dt, "NAME", "ID")
                End If
            End Using
            txtYear.Value = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_DECLARE_ENTITLEMENTDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetDelareEntitlementNBById(obj.ID)
                    If obj IsNot Nothing Then
                        isEdit = True
                        txtCode.Text = obj.EMPLOYEE_CODE
                        txtName.Text = obj.VN_FULLNAME
                        txtChucDanh.Text = obj.TITLE_NAME
                        Employee_id = obj.EMPLOYEE_ID
                        txtDonVi.Text = obj.ORG_NAME
                        txtADJUST_MONTH_TN2.Value = obj.ADJUST_MONTH_TN
                        txtREMARK_TN.Text = obj.REMARK_TN
                        If obj.START_MONTH_TN IsNot Nothing Then
                            cboStartMonth.SelectedValue = obj.START_MONTH_TN
                        End If
                        If obj.MODIFY_TYPE_ID IsNot Nothing Then
                            cboModifyType.SelectedValue = obj.MODIFY_TYPE_ID
                        End If
                        txtYear.Value = obj.YEAR
                        If obj.JOIN_DATE IsNot Nothing Then
                            rdStartDate.SelectedDate = obj.JOIN_DATE
                        End If
                        If obj.END_MONTH_TN IsNot Nothing Then
                            cboEndMonth.SelectedValue = obj.END_MONTH_TN
                        End If
                        txtExpireYear.Value = obj.EXPIRE_YEAR
                        _Value = obj.ID

                        rmStartDate.SelectedDate = obj.START_DATE
                        rmEndDate.SelectedDate = obj.END_DATE
                    Else
                        isEdit = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event click cancel popup tìm nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event Click Ok popup chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                  ByVal e As EventArgs) Handles btnFindEmployee.Click
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
                    ctrlFindEmployeePopup.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_DECLARE_ENTITLEMENTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Employee_id <= 0 Then
                        ShowMessage(Translate("Chưa chọn nhân viên cần đăng ký"), NotifyType.Error)
                        Exit Sub
                    End If
                    If Page.IsValid Then
                        Dim startdate As New Date
                        Dim checkMonthNB As Boolean = False
                        Dim checkMonthNP As Boolean = False
                        Dim adjustMonthTn2 As String = ""
                        obj = New AT_DECLARE_ENTITLEMENTDTO
                        obj.EMPLOYEE_ID = Employee_id
                        obj.ADJUST_MONTH_TN = txtADJUST_MONTH_TN2.Value

                        If cboModifyType.SelectedValue <> "" Then
                            obj.MODIFY_TYPE_ID = cboModifyType.SelectedValue
                        End If
                        If cboStartMonth.SelectedValue <> "" Then
                            obj.START_MONTH_TN = cboStartMonth.SelectedValue
                        End If
                        If cboEndMonth.SelectedValue <> "" Then
                            obj.END_MONTH_TN = cboEndMonth.SelectedValue
                        End If
                        obj.YEAR = txtYear.Value
                        obj.YEAR_ENTITLEMENT = txtYear.Value
                        obj.YEAR_NB = txtYear.Value
                        obj.EXPIRE_YEAR = txtExpireYear.Value
                        obj.JOIN_DATE = rdStartDate.SelectedDate
                        obj.REMARK_TN = txtREMARK_TN.Text

                        If _Value.HasValue Then
                            obj.ID = _Value
                        End If

                        obj.START_DATE = rmStartDate.SelectedDate
                        obj.END_DATE = rmEndDate.SelectedDate

                        rep.InsertDelareEntitlementNB(obj, gstatus, checkMonthNB, checkMonthNP)
                        If checkMonthNB Then
                            ShowMessage(Translate("Nhân viên này đã được gia hạn nghỉ bù"), NotifyType.Error)
                            Exit Sub
                        ElseIf checkMonthNP Then
                            ShowMessage(Translate("Nhân viên này đã được gia hạn nghỉ phép"), NotifyType.Error)
                            Exit Sub
                        End If
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK
                        'Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNB&group=Business")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    'Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNB&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Update trạng thái control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                Select Case isLoadPopup
                    Case 1
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.IsHideTerminate = False
                        End If
                End Select
            Catch ex As Exception
                Throw ex
            End Try
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event chọn nhân viên trên popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_DECLARE_ENTITLEMENTDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.STAFF_RANK_ID = lstCommonEmployee(idx).STAFF_RANK_ID
                    isLoadPopup = 0
                    Employee_id = item.EMPLOYEE_ID
                    txtCode.Text = item.EMPLOYEE_CODE
                    txtName.Text = item.VN_FULLNAME
                    txtChucDanh.Text = item.TITLE_NAME
                    txtDonVi.Text = item.ORG_NAME
                    txtHurtType.Text = lstCommonEmployee(idx).HURT_TYPE_NAME
                    rdStartDate.SelectedDate = lstCommonEmployee(idx).JOIN_DATE
                Next
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class

