Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SalaryDetentionNewEdit
    Inherits CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Payroll\Modules\Payroll\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Org
    ''' 3 - Sign
    ''' </returns>
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
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Dim IDSelect As Decimal?

    Dim FormType As Integer

    Property SalaryDetention As PA_Salary_DetentionDTO
        Get
            Return ViewState(Me.ID & "_SalaryDetention")
        End Get
        Set(ByVal value As PA_Salary_DetentionDTO)
            ViewState(Me.ID & "_SalaryDetention") = value
        End Set
    End Property

    Property lstSalaryDetention As List(Of PA_Salary_DetentionDTO)
        Get
            Return ViewState(Me.ID & "_lstSalaryDetention")
        End Get
        Set(ByVal value As List(Of PA_Salary_DetentionDTO))
            ViewState(Me.ID & "_lstSalaryDetention") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
                Select Case FormType
                    Case 0
                        Me.ViewDescription = Translate("Tạo mới giữ lương nhân viên")
                    Case 1
                        Me.ViewDescription = Translate("Sửa giữ lương nhân viên")
                End Select
            End If

            Me.MainToolBar = tbarCommend

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New PayrollRepository
        Try
            Dim lsData As New DataTable
            Dim lsData1 As New DataTable
            lsData = rep.GetPeriod(1)
            lsData1 = rep.GetPeriod(-1)

            FillRadCombobox(cboYear1, lsData1, "ID", "ID", True)
            FillRadCombobox(cboYear2, lsData1, "ID", "ID", True)
            FillRadCombobox(cboPeriodT, lsData, "PERIOD_T", "ID", True)
            FillRadCombobox(cboPayMonth, lsData, "PERIOD_T", "ID", True)

            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case Message
                Case "UpdateView"
                    SalaryDetention = rep.GetSalaryDetentionByID(New PA_Salary_DetentionDTO With {.ID = IDSelect})
                    CurrentState = CommonMessage.STATE_EDIT
                    If SalaryDetention.PERIOD_ID IsNot Nothing Then
                        cboPeriodT.SelectedValue = SalaryDetention.PERIOD_ID
                    End If
                    If SalaryDetention.PERIOD_ID IsNot Nothing Then
                        cboPayMonth.SelectedValue = SalaryDetention.PAY_MONTH
                    End If
                    txtNote.Text = SalaryDetention.NOTE
                    hidID.Value = SalaryDetention.ID.ToString
                    rgEmployee.Rebind()

                    For Each i As GridItem In rgEmployee.Items
                        i.Edit = True
                    Next

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()

            If FormType = 1 Then
                For Each dataItemEmp As GridDataItem In rgEmployee.MasterTableView.Items
                    dataItemEmp.Selected = True
                Next
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New PayrollRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        Dim lstPA_Salary_Detention As New List(Of PA_Salary_DetentionDTO)
                        For Each row As GridDataItem In rgEmployee.Items
                            Dim Salary_Detention As New PA_Salary_DetentionDTO
                            Salary_Detention.EMPLOYEE_ID = row.GetDataKeyValue("EMPLOYEE_ID")
                            Salary_Detention.NOTE = txtNote.Text.Trim
                            If cboPayMonth.SelectedValue <> "" Then
                                Salary_Detention.PAY_MONTH = cboPayMonth.SelectedValue
                            End If
                            If cboPeriodT.SelectedValue <> "" Then
                                Salary_Detention.PERIOD_ID = cboPeriodT.SelectedValue
                            End If
                            Salary_Detention.IS_DETENTION = -1
                            lstPA_Salary_Detention.Add(Salary_Detention)
                        Next
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.INSERT_PA_SALARY_DETENTION(lstPA_Salary_Detention) Then
                                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_Salary_DetentionNewEdit&group=Business&FormType=0")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                If lstPA_Salary_Detention IsNot Nothing Then
                                    lstPA_Salary_Detention(0).ID = Decimal.Parse(hidID.Value)
                                    If rep.MODIFY_PA_SALARY_DETENTION(lstPA_Salary_Detention) Then
                                        Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_Salary_Detention&group=Business")
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                End If
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Payroll&fid=ctrlPA_Salary_Detention&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện cho nút hủy của popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Dim filter As New PA_Salary_DetentionDTO
        Try
            If lstSalaryDetention Is Nothing Then
                lstSalaryDetention = New List(Of PA_Salary_DetentionDTO)
            End If
            If hidID.Value.ToString <> "" Then
                filter.ID = hidID.Value
                Dim obj = rep.GetSalaryDetentionByID(filter)
                lstSalaryDetention.Add(obj)
            End If

            rgEmployee.DataSource = lstSalaryDetention

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmployee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                If lstSalaryDetention Is Nothing Then
                    lstSalaryDetention = New List(Of PA_Salary_DetentionDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If lstSalaryDetention.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        ShowMessage(Translate("Nhân viên đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim employee As New PA_Salary_DetentionDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.EMPLOYEE_ID = emp.ID
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    lstSalaryDetention.Add(employee)
                Next
                If lstSalaryDetention.Count > 1 Then
                    lstSalaryDetention.Clear()
                    ShowMessage(Translate("Vui lòng chỉ chọn 1 nhân viên."), NotifyType.Warning)
                    Exit Sub
                End If
                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next

                rgEmployee.Rebind()
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = True
                    ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup._isCommend = True
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Get parameters</summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If

                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If

                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub cboYear1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear1.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim lsData As New DataTable
        Dim value As Decimal
        If cboYear1.SelectedValue <> "" Then
            value = CDec(cboYear1.SelectedValue)
        Else
            Exit Sub
        End If
        lsData = rep.GetPeriod(value)
        FillRadCombobox(cboPeriodT, lsData, "PERIOD_T", "ID", False)
    End Sub

    Private Sub cboYear2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear2.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Dim lsData As New DataTable
        Dim value As Decimal
        If cboYear2.SelectedValue <> "" Then
            value = CDec(cboYear2.SelectedValue)
        Else
            Exit Sub
        End If
        lsData = rep.GetPeriod(value)
        FillRadCombobox(cboPayMonth, lsData, "PERIOD_T", "ID", False)
    End Sub
#End Region

End Class