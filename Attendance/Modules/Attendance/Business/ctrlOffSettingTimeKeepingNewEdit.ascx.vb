Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlOffSettingTimeKeepingNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
    Dim _result As Boolean = False
    Dim log As New Common.CommonBusiness.UserLog
#Region "Property"

    Property Employee_BT As List(Of AT_OFFFSETTING_EMPDTO)
        Get
            Return ViewState(Me.ID & "_Employee_BT")
        End Get
        Set(value As List(Of AT_OFFFSETTING_EMPDTO))
            ViewState(Me.ID & "_Employee_BT") = value
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
        log = LogHelper.GetUserLog
        Try
            SetGridFilter(rgEmployee)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgEmployee.AllowCustomPaging = True
            rgEmployee.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            If Not IsPostBack Then
                _result = True
                ViewConfig(RadPane2)
                GirdConfig(rgEmployee)
            End If
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
        Try

            GetDataCombo()
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
    End Sub
#End Region
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceBusinessClient
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LIST_OFFTIME_TYPE = True
            rep.GetComboboxData(ListComboData, log.Username)
            FillDropDownList(cboTypeBT, ListComboData.LIST_LIST_OFFTIME, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
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
    'Protected Sub btnEmployee_Click(ByVal sender As Object,
    '                              ByVal e As EventArgs) Handles btnEmployee.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Try
    '        Select Case sender.ID
    '            Case btnEmployee.ID
    '                isLoadPopup = 1
    '        End Select

    '        UpdateControlState()
    '        Select Case sender.ID
    '            Case btnFindEmployee.ID
    '                ctrlFindEmployeePopup.Show()
    '        End Select
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_OFFFSETTINGDTO
        Dim lstOffSettingEmp As New List(Of AT_OFFFSETTING_EMPDTO)
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        Dim startdate As New Date
                        obj = New AT_OFFFSETTINGDTO
                        obj.EMPLOYEE_ID = Employee_id
                        obj.REMARK = txtREMARK.Text
                        obj.MINUTES_BT = txtNumber.Text
                        If cboTypeBT.SelectedValue <> "" Then
                            obj.TYPE_BT = cboTypeBT.SelectedValue
                        End If
                        obj.FROMDATE = rdFromDate.SelectedDate
                        obj.TODATE = rdToDate.SelectedDate
                        If _Value.HasValue Then
                            obj.ID = _Value
                        End If
                        For Each item In Employee_BT
                            Dim fromDate = rdFromDate.SelectedDate
                            While (fromDate <= rdToDate.SelectedDate)
                                Dim o As New AT_OFFFSETTING_EMPDTO
                                o.EMPLOYEE_ID = item.EMPLOYEE_ID
                                o.ORG_ID = item.ORG_ID
                                o.TITLE_ID = item.TITLE_ID
                                o.WORKING_DAY = fromDate
                                lstOffSettingEmp.Add(o)
                                fromDate = fromDate.Value.AddDays(1)
                            End While
                        Next
                        If lstOffSettingEmp.Count = 0 Then
                            ShowMessage(Translate("Vui lòng chọn nhân viên trước khi lưu"), NotifyType.Warning)
                            Exit Sub
                        End If
                        obj.OFFFSETTING_EMP = lstOffSettingEmp
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertOffSettingTime(obj, gID) Then
                                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOffSettingTimeKeeping&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If rep.ModifyOffSettingTime(obj, gID) Then
                                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOffSettingTimeKeeping&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOffSettingTimeKeeping&group=Business")
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
    ''' Event Request for ajax
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Try
    '        Dim url = e.Argument
    '        If (url.Contains("reload=1")) Then
    '            rgEmployee.CurrentPageIndex = 0
    '            rgEmployee.Rebind()
    '            If rgEmployee.Items IsNot Nothing AndAlso rgEmployee.Items.Count > 0 Then
    '                rgEmployee.Items(0).Selected = True
    '            End If
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Update trạng thái control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                    'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                End If
                Select Case isLoadPopup
                    Case 1
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
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
                If Employee_BT Is Nothing Then
                    Employee_BT = New List(Of AT_OFFFSETTING_EMPDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New AT_OFFFSETTING_EMPDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.EMPLOYEE_ID = emp.EMPLOYEE_ID
                    employee.ID = emp.ID
                    Employee_id = emp.ID
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    'If cboCommendObj.SelectedIndex = 0 Then
                    '    txtDecisionNo.Text = employee.EMPLOYEE_CODE + " / KT / " + Date.Now.ToString("MMyy")
                    'End If

                    Dim checkEmployeeCode As AT_OFFFSETTING_EMPDTO = Employee_BT.Find(Function(p) p.EMPLOYEE_CODE = emp.EMPLOYEE_CODE)
                    If (Not checkEmployeeCode Is Nothing) Then
                        Continue For
                    End If

                    Employee_BT.Add(employee)
                Next
                _result = False
                rgEmployee.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Private Sub rgEmployee_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_BT Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_BT.Remove(s)
                    Next
                    _result = False
                    rgEmployee.Rebind()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Dim rep As New AttendanceRepository
        Try
            If _result = True Then
                Dim Message = Request.Params("VIEW")
                Select Case Message
                    Case "TRUE"
                        Dim obj As New AT_OFFFSETTINGDTO
                        obj.ID = Decimal.Parse(Request.Params("ID"))
                        CurrentState = CommonMessage.STATE_EDIT
                        obj = rep.GetOffSettingTimeKeepingById(obj.ID)
                        If obj IsNot Nothing Then
                            isEdit = True
                            'txtCode.Text = obj.EMPLOYEE_CODE
                            'txtName.Text = obj.VN_FULLNAME
                            'txtChucDanh.Text = obj.TITLE_NAME
                            Employee_id = obj.EMPLOYEE_ID
                            txtNumber.Text = obj.MINUTES_BT
                            txtREMARK.Text = obj.REMARK
                            rdFromDate.SelectedDate = obj.FROMDATE
                            rdToDate.SelectedDate = obj.TODATE
                            cboTypeBT.Text = obj.TYPE_NAME
                            _Value = obj.ID
                        Else
                            isEdit = False
                        End If
                        Employee_BT = rep.GetEmployeeTimeKeepingID(obj.ID)

                    Case Nothing
                        CurrentState = CommonMessage.STATE_NEW
                        If Not IsPostBack Then
                            Employee_BT = New List(Of AT_OFFFSETTING_EMPDTO)
                        End If
                End Select
            End If
            rgEmployee.VirtualItemCount = Employee_BT.Count

            Dim lst = Employee_BT.Skip(rgEmployee.CurrentPageIndex * rgEmployee.PageSize).Take(rgEmployee.PageSize)

            rgEmployee.DataSource = lst
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

End Class

