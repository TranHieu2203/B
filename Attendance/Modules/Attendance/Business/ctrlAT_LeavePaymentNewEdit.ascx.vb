Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAT_LeavePaymentNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

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

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay params 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Select Case Message
                Case "UpdateView"
                    Dim obj As New AT_LEAVE_PAYMENTDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetLeavePaymentById(obj.ID)
                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        hidEmpId.Value = obj.EMPLOYEE_ID
                        txtEmpCode.Text = obj.EMPLOYEE_CODE
                        txtFullName.Text = obj.EMPLOYEE_NAME
                        txtTitle.Text = obj.TITLE_NAME
                        txtDepartment.Text = obj.ORG_NAME
                        If obj.YEAR.HasValue Then
                            rnYear.Value = obj.YEAR
                        End If
                        GetLeaveInfo()
                        If obj.EFFECT_DATE.HasValue Then
                            rdEffectDate.SelectedDate = obj.EFFECT_DATE
                        End If
                        If obj.LEAVE_OLD.HasValue Then
                            rnLeaveOld.Value = obj.LEAVE_OLD
                        End If
                        If obj.LEAVE_NEW.HasValue Then
                            rnLeaveNew.Value = obj.LEAVE_NEW
                        End If
                        If obj.SALARY_AVERAGE.HasValue Then
                            rnSalAverage.Value = obj.SALARY_AVERAGE
                        End If
                        If obj.SALARY_NEW.HasValue Then
                            rnSalNew.Value = obj.SALARY_NEW
                        End If
                        If obj.SALARY_PAYMENT.HasValue Then
                            rnSalPayment.Value = obj.SALARY_PAYMENT
                        End If
                        If Not String.IsNullOrEmpty(obj.PERIOD_T) Then
                            chkSal.Checked = True
                            txtPeriodT.ReadOnly = False
                            txtPeriodT.Text = obj.PERIOD_T
                        End If
                        txtRemark.Text = obj.REMARK
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New AttendanceRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New AT_LEAVE_PAYMENTDTO
                        obj.ID = If(IsNumeric(hidID.Value), hidID.Value, 0)
                        If IsNumeric(hidEmpId.Value) Then
                            obj.EMPLOYEE_ID = hidEmpId.Value
                        End If
                        If IsNumeric(rnYear.Value) Then
                            obj.YEAR = rnYear.Value
                        End If
                        If IsDate(rdEffectDate.SelectedDate) Then
                            obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If
                        If IsNumeric(rnLeaveOld.Value) Then
                            obj.LEAVE_OLD = rnLeaveOld.Value
                        End If
                        If IsNumeric(rnLeaveNew.Value) Then
                            obj.LEAVE_NEW = rnLeaveNew.Value
                        End If
                        If IsNumeric(rnSalAverage.Value) Then
                            obj.SALARY_AVERAGE = rnSalAverage.Value
                        End If
                        If IsNumeric(rnSalNew.Value) Then
                            obj.SALARY_NEW = rnSalNew.Value
                        End If
                        If IsNumeric(rnSalPayment.Value) Then
                            obj.SALARY_PAYMENT = rnSalPayment.Value
                        End If
                        obj.PERIOD_T = txtPeriodT.Text.Trim
                        obj.REMARK = txtRemark.Text.Trim

                        If rep.ValidateLeavePayment(obj) Then
                            ShowMessage(Translate("Dữ liệu đã tồn tại"), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InSertLeavePayment(obj) Then
                                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlAT_LeavePayment&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If rep.ModifyLeavePayment(obj) Then
                                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlAT_LeavePayment&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlAT_LeavePayment&group=Business")
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
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
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
        Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmpId.Value = item.EMPLOYEE_ID
                txtFullName.Text = item.FULLNAME_VN
                txtDepartment.Text = item.ORG_NAME
                txtEmpCode.Text = item.EMPLOYEE_CODE
                txtTitle.Text = item.TITLE_NAME
                GetLeaveInfo()
                GetSalaryInfo()
                GetTotalPayment()
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmpCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmpCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmpCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmpCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim item = EmployeeList(0)
                        hidEmpId.Value = item.EMPLOYEE_ID
                        txtFullName.Text = item.FULLNAME_VN
                        txtDepartment.Text = item.ORG_NAME
                        txtEmpCode.Text = item.EMPLOYEE_CODE
                        txtTitle.Text = item.TITLE_NAME
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmpCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
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

    Private Sub rnYear_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnYear.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetLeaveInfo()
            If IsNumeric(rnLeaveOld.Value) AndAlso IsNumeric(rnPreHave.Value) AndAlso rnLeaveOld.Value > rnPreHave.Value Then
                ClearControlValue(rnSalPayment)
                ShowMessage(Translate("Phép thanh toán vượt phép đang có"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            If IsNumeric(rnLeaveNew.Value) AndAlso IsNumeric(rnCurHave.Value) AndAlso rnLeaveNew.Value > rnCurHave.Value Then
                ClearControlValue(rnSalPayment)
                ShowMessage(Translate("Phép thanh toán vượt phép đang có"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            GetTotalPayment()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetSalaryInfo()
            GetTotalPayment()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rnLeaveOld_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnLeaveOld.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsNumeric(rnLeaveOld.Value) AndAlso IsNumeric(rnPreHave.Value) AndAlso rnLeaveOld.Value > rnPreHave.Value Then
                ClearControlValue(rnSalPayment)
                ShowMessage(Translate("Phép thanh toán vượt phép đang có"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            GetTotalPayment()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rnLeaveNew_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnLeaveNew.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsNumeric(rnLeaveNew.Value) AndAlso IsNumeric(rnCurHave.Value) AndAlso rnLeaveNew.Value > rnCurHave.Value Then
                ClearControlValue(rnSalPayment)
                ShowMessage(Translate("Phép thanh toán vượt phép đang có"), Utilities.NotifyType.Error)
                Exit Sub
            End If
            GetTotalPayment()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub chkSal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSal.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            txtPeriodT.Text = ""
            txtPeriodT.ReadOnly = Not chkSal.Checked
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request.Params("ID") IsNot Nothing Then
                Refresh("UpdateView")
            Else
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetLeaveInfo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(rnPreHave, rnCurHave, rnSalPayment)
            If IsNumeric(hidEmpId.Value) AndAlso IsNumeric(rnYear.Value) Then
                Dim rep As New AttendanceRepository
                Dim item = rep.GetPhepNam(hidEmpId.Value, rnYear.Value)
                If item IsNot Nothing Then
                    rnCurHave.Value = If(item.CUR_HAVE.HasValue, item.CUR_HAVE, 0)
                    rnPreHave.Value = If(item.PREVTOTAL_HAVE.HasValue, item.PREVTOTAL_HAVE, 0)
                End If
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Reset_Find_Emp()
        hidEmpId.Value = ""
        txtFullName.Text = ""
        txtDepartment.Text = ""
        txtTitle.Text = ""
    End Sub

    Private Sub GetSalaryInfo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(rnSalAverage, rnSalNew, rnSalPayment)
            If IsNumeric(hidEmpId.Value) AndAlso IsDate(rdEffectDate.SelectedDate) Then
                Dim rep As New AttendanceRepository
                Dim dtData = rep.GetLeavePaymentSal(hidEmpId.Value, rdEffectDate.SelectedDate)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    rnSalAverage.Value = If(Not IsDBNull(dtData.Rows(0)("SALARY_AVERAGE")), CDec(dtData.Rows(0)("SALARY_AVERAGE")), 0)
                    rnSalNew.Value = If(Not IsDBNull(dtData.Rows(0)("SALARY_NEW")), CDec(dtData.Rows(0)("SALARY_NEW")), 0)
                End If
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetTotalPayment()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(rnSalPayment)
            If IsNumeric(rnLeaveNew.Value) AndAlso IsNumeric(rnLeaveOld.Value) AndAlso IsNumeric(rnSalAverage.Value) AndAlso IsNumeric(rnSalNew.Value) Then
                rnSalPayment.Value = rnSalAverage.Value / 26 * rnLeaveOld.Value + rnSalNew.Value / 26 * rnLeaveNew.Value
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class

