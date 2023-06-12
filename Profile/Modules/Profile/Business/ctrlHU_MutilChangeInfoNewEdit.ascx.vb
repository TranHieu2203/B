Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_MutilChangeInfoNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindManager As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj ListComboData
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
    Private Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property
    Private Property dtbImport As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbImport")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbImport") = value
        End Set
    End Property
    Public Property dem As Integer
        Get
            Return ViewState(Me.ID & "_dem")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_dem") = value
        End Set
    End Property
    Private Property dtAllowList As DataTable
        Get
            Return PageViewState(Me.ID & "_dtAllowList")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtAllowList") = value
        End Set
    End Property
    Public Property ColumnImportWelfare As Integer
        Get
            Return ViewState(Me.ID & "_ColumnImportWelfare")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_ColumnImportWelfare") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj WelfareMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WelfareMng As WelfareMngDTO
        Get
            Return ViewState(Me.ID & "_WelfareMngDTO")
        End Get
        Set(ByVal value As WelfareMngDTO)
            ViewState(Me.ID & "_WelfareMngDTO") = value
        End Set
    End Property
    Property Employee_PL As List(Of WorkingDTO)
        Get
            Return ViewState(Me.ID & "_Employee_PL")
        End Get
        Set(value As List(Of WorkingDTO))
            ViewState(Me.ID & "_Employee_PL") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj _Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Id As Integer
        Get
            Return ViewState(Me.ID & "_Id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Id") = value
        End Set
    End Property
    Property checkDelete As Integer
        Get
            Return ViewState(Me.ID & "_checkDelete")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkDelete") = value
        End Set
    End Property
    Property Total_money As Decimal
        Get
            Return ViewState(Me.ID & "_Total_money")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Total_money") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj popupID
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
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

    Property dtDecisionType As DataTable
        Get
            Return ViewState(Me.ID & "_dtDecisionType")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDecisionType") = value
        End Set
    End Property
    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not Page.IsPostBack Then
                Refresh()
                txtManagerNew.Visible = False
                btnFindDirect.Visible = False
                rqManagerNew.Enabled = False
                txtOrgName.Visible = False
                btnFindOrg.Visible = False
                rqOrgName.Enabled = False
                cboTitle.Visible = False
                rqTitle.Enabled = False
                cboObjectAttendant.Visible = False
                rqObjectAttendant.Enabled = False
                cboObjectEmp.Visible = False
                rqObjectEmp.Enabled = False
                cboWorkPlace.Visible = False
                rqWorkPlace.Enabled = False
                cbOBJECT_ATTENDANCE.Visible = False
                rqOBJECT_ATTENDANCE.Enabled = False
                cboObjectLaborNew.Visible = False
                reqObjectLabor.Enabled = False
                If CommonConfig.ISHIDE_OBJECTLABORNEW() Then
                    chkObjectLaborNew.Visible = False
                    cboObjectLaborNew.Visible = False
                    reqObjectLabor.Visible = False
                End If
            End If
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("gUID"))
                LoadData()
                rgEmployee.Rebind()
                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If


        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        ColumnImportWelfare = 13
        SetGridFilter(rgEmployee)
        AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
        AjaxManagerId = AjaxManager.ClientID
        rgEmployee.AllowCustomPaging = True
        rgEmployee.ClientSettings.EnablePostBackOnRowClick = False
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                        ToolbarItem.Seperator, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'LoadControlPopup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
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
                        Dim s = (From q In Employee_PL Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_PL.Remove(s)
                    Next
                    '_result = False
                    checkDelete = 1
                    dtbImport = Employee_PL.ToTable()
                    rgEmployee.DataSource = dtbImport
                    rgEmployee_NeedDataSource(Nothing, Nothing)
                    rgEmployee.Rebind()
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                    phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                    'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                End If
                Select Case isLoadPopup
                    Case 1
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                    Case 3
                        ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                        If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                            ctrlFindOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                        End If
                        ctrlFindOrgPopup.IS_HadLoad = False
                        phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    Case 4
                        If Not phFindSignManager.Controls.Contains(ctrlFindManager) Then
                            ctrlFindManager = Me.Register("ctrlFindManager", "Common", "ctrlFindEmployeePopup")
                            phFindSignManager.Controls.Add(ctrlFindManager)
                            ctrlFindManager.MultiSelect = False
                            Dim ComStore As New Common.CommonProcedureNew
                            If ComStore.CHECK_EXIST_SE_CONFIG("HU_QUTT_PERMISION") = -1 Then
                                ctrlFindManager.LoadAllOrganization = True
                            Else
                                ctrlFindManager.LoadAllOrganization = False
                                ctrlFindManager.is_All = False
                            End If
                            ctrlFindManager.MustHaveContract = False
                        End If

                End Select
            Catch ex As Exception
                Throw ex
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If Employee_PL Is Nothing Then
                    Employee_PL = New List(Of WorkingDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New WorkingDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.EMPLOYEE_ID = emp.EMPLOYEE_ID
                    employee.ID = emp.ID
                    employee.EMPLOYEE_NAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    employee.OBJECT_ATTENDANCE_NAME = emp.OBJECT_NAME
                    employee.OBJECT_LABORNAME = emp.LABOUR_NAME
                    employee.OBJECT_ATTENDANCE = emp.OBJECTTIMEKEEPING
                    employee.OBJECT_LABOR = emp.OBJECT_LABOR
                    employee.STAFF_RANK_ID = emp.STAFF_RANK_ID
                    employee.STAFF_RANK_NAME = emp.STAFF_RANK_NAME
                    employee.OBJECT_ATTENDANT_ID = emp.OBJECT_ATTENDANT_ID
                    employee.OBJECT_ATTENDANT_NAME = emp.OBJECT_ATTENDANT_NAME
                    employee.OBJECT_EMPLOYEE_ID = emp.OBJECT_EMPLOYEE_ID
                    employee.OBJECT_EMPLOYEE_NAME = emp.OBJECT_EMPLOYEE_NAME
                    employee.WORK_PLACE_ID = emp.WORK_PLACE_ID
                    employee.WORK_PLACE_NAME = emp.WORK_PLACE_NAME
                    employee.DIRECT_MANAGER = emp.DIRECT_MANAGER
                    Dim checkEmployeeCode As WorkingDTO = Employee_PL.Find(Function(p) p.EMPLOYEE_CODE = emp.EMPLOYEE_CODE)
                    If (Not checkEmployeeCode Is Nothing) Then
                        Continue For
                    End If
                    Employee_PL.Add(employee)
                Next
                '_result = False
                dtbImport = Employee_PL.ToTable()
                rgEmployee.Rebind()
                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlFindEmployeePopup_CancelClicked(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objdata As New WorkingDTO
            Dim check As New Decimal
            Dim lstemp As New List(Of WorkingDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    'If cboStatus.SelectedValue = "" Then
                    '    ShowMessage(Translate("Bạn phải chọn trạng thái"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If cboStatus.SelectedValue = 447 Then
                    '    If txtUpload.Text = "" Then
                    '        ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End If

                    'If cboDecisionType.SelectedValue = "" Then
                    '    ShowMessage(Translate("Bạn phải chọn loại quyết định"), NotifyType.Warning)
                    '    cboDecisionType.Focus()
                    '    Exit Sub
                    'End If
                    'If cboDecisionType.Text = "Quyết định thay đổi QLTT" Then

                    'Else
                    '    If cboTitle.SelectedValue = "" Then
                    '        ShowMessage(Translate("Bạn phải chọn chức danh"), NotifyType.Warning)
                    '        cboTitle.Focus()
                    '        Exit Sub
                    '    End If
                    'End If

                    If rgEmployee.Items.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên"), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each line As GridDataItem In rgEmployee.Items
                        objdata.EMPLOYEE_ID = line.GetDataKeyValue("EMPLOYEE_ID").ToString
                        'If cboDecisionType.Text = "Quyết định thay đổi QLTT" Then
                        '    objdata.TITLE_ID = If(line.GetDataKeyValue("TITLE_ID").ToString <> "", line.GetDataKeyValue("TITLE_ID").ToString, Nothing)
                        '    objdata.ORG_ID = If(line.GetDataKeyValue("ORG_ID").ToString <> "", line.GetDataKeyValue("ORG_ID").ToString, Nothing)
                        'Else
                        If chkTitle.Checked AndAlso cboTitle.SelectedValue <> "" Then
                            objdata.TITLE_ID = cboTitle.SelectedValue
                        Else
                            objdata.TITLE_ID = If(line.GetDataKeyValue("TITLE_ID").ToString <> "", line.GetDataKeyValue("TITLE_ID").ToString, Nothing)
                        End If
                        If chkOrgName.Checked AndAlso hidOrg.Value <> "" Then
                            objdata.ORG_ID = hidOrg.Value
                        Else
                            objdata.ORG_ID = If(line.GetDataKeyValue("ORG_ID").ToString <> "", line.GetDataKeyValue("ORG_ID").ToString, Nothing)
                            hidOrg.Value = If(line.GetDataKeyValue("ORG_ID").ToString <> "", line.GetDataKeyValue("ORG_ID").ToString, Nothing)
                        End If
                        'End If

                        If cboStatus.SelectedValue <> "" Then
                            objdata.STATUS_ID = cboStatus.SelectedValue
                        End If
                        objdata.EFFECT_DATE = rdEffectDate.SelectedDate
                        'objdata.EXPIRE_DATE = rdExpireDate.SelectedDate
                        If cboDecisionType.SelectedValue <> "" Then
                            objdata.DECISION_TYPE_ID = cboDecisionType.SelectedValue
                        End If
                        objdata.SIGN_DATE = rdSignDate.SelectedDate
                        If hidSign.Value <> "" Then
                            objdata.SIGN_ID = hidSign.Value
                        End If
                        'If Not String.IsNullOrEmpty(txtDecision.Text) Then
                        '    objdata.DECISION_NO = txtDecision.Text
                        'Else
                        objdata.DECISION_NO = GetDecisioNo(line.GetDataKeyValue("EMPLOYEE_CODE"))
                        'End If
                        'objdata.SIGN_NAME = txtSignName.Text

                        'objdata.SIGN_TITLE = txtSignTitle.Text
                        objdata.REMARK = txtSDESC.Text

                        'objdata.IS_PROCESS = chkIsProcess.Checked
                        objdata.IS_MISSION = True
                        If chkManagerNew.Checked AndAlso hidManager.Value <> "" Then
                            objdata.DIRECT_MANAGER = hidManager.Value
                        Else
                            'objdata.DIRECT_MANAGER = If(line.GetDataKeyValue("DIRECT_MANAGER").ToString <> "", line.GetDataKeyValue("DIRECT_MANAGER").ToString, Nothing)
                            objdata.DIRECT_MANAGER = Nothing
                        End If
                        If chkObjectEmp.Checked AndAlso cboObjectEmp.SelectedValue <> "" Then
                            objdata.OBJECT_EMPLOYEE_ID = cboObjectEmp.SelectedValue
                        Else
                            objdata.OBJECT_EMPLOYEE_ID = If(line.GetDataKeyValue("OBJECT_EMPLOYEE_ID").ToString <> "", line.GetDataKeyValue("OBJECT_EMPLOYEE_ID").ToString, Nothing)
                        End If
                        If chkObjectAttendant.Checked AndAlso cboObjectAttendant.SelectedValue <> "" Then
                            objdata.OBJECT_ATTENDANT_ID = cboObjectAttendant.SelectedValue
                        Else
                            objdata.OBJECT_ATTENDANT_ID = If(line.GetDataKeyValue("OBJECT_ATTENDANT_ID").ToString <> "", line.GetDataKeyValue("OBJECT_ATTENDANT_ID").ToString, Nothing)
                        End If
                        If chkWorkPlace.Checked AndAlso cboWorkPlace.SelectedValue <> "" Then
                            objdata.WORK_PLACE_ID = cboWorkPlace.SelectedValue
                        Else
                            objdata.WORK_PLACE_ID = If(line.GetDataKeyValue("WORK_PLACE_ID").ToString <> "", line.GetDataKeyValue("WORK_PLACE_ID").ToString, Nothing)
                        End If
                        If chkOBJECT_ATTENDANCE.Checked AndAlso cbOBJECT_ATTENDANCE.SelectedValue <> "" Then
                            objdata.OBJECT_ATTENDANCE = cbOBJECT_ATTENDANCE.SelectedValue
                        Else
                            objdata.OBJECT_ATTENDANCE = If(line.GetDataKeyValue("OBJECT_ATTENDANCE").ToString <> "", line.GetDataKeyValue("OBJECT_ATTENDANCE").ToString, Nothing)
                        End If
                        objdata.IS_3B = False
                        objdata.IS_WAGE = IsWageDecisionType(cboDecisionType.SelectedValue)
                        objdata.SAL_INS = objdata.SAL_BASIC
                        objdata.ALLOWANCE_TOTAL = 0
                        If chkObjectLaborNew.Checked AndAlso cboObjectLaborNew.SelectedValue <> "" Then
                            objdata.OBJECT_LABOR = cboObjectLaborNew.SelectedValue
                        Else
                            objdata.OBJECT_LABOR = If(line.GetDataKeyValue("OBJECT_LABOR").ToString <> "", line.GetDataKeyValue("OBJECT_LABOR").ToString, Nothing)
                        End If
                        'objdata.STAFF_RANK_ID = If(line.GetDataKeyValue("STAFF_RANK_ID").ToString <> "", line.GetDataKeyValue("STAFF_RANK_ID").ToString, Nothing)
                        'objdata.FILENAME = txtUpload.Text.Trim
                        objdata.ATTACH_FILE = If(Down_File Is Nothing, "", Down_File)
                        If objdata.ATTACH_FILE = "" Then
                            objdata.ATTACH_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objdata.ATTACH_FILE = If(objdata.ATTACH_FILE Is Nothing, "", objdata.ATTACH_FILE)
                        End If
                        Dim rep As New ProfileBusinessRepository
                        check = rep.InsertListWorking1(objdata)
                        rep.Dispose()
                    Next
                    If CurrentState = CommonMessage.STATE_NEW Then
                        If check = True Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_MutilChangeInfoNewEdit&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                        objdata.ID = WelfareMng.ID
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(objdata.ID)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_MutilChangeInfoNewEdit&group=Business")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function IsWageDecisionType(ByVal id As Decimal) As Boolean
        Using rep As New ProfileRepository
            Dim otherlist = rep.GetOtherList(ProfileCommon.DECISION_TYPE.Name)
            If otherlist IsNot Nothing Then
                Return otherlist.AsEnumerable().Any(Function(f) (f.Item("CODE") = ProfileCommon.DECISION_TYPE.Promotion Or
                                                        f.Item("CODE") = ProfileCommon.DECISION_TYPE.AffectSalary) And
                                                  f.Item("ID") = id)
            End If
        End Using
        Return False
    End Function
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidOrg.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'Using rep As New ProfileRepository
                '    If IsNumeric(e.CurrentValue) Then

                '    End If
                'End Using
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnFindDirect_Click(sender As Object, e As System.EventArgs) Handles btnFindDirect.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 4
            UpdateControlState()
            ctrlFindManager.MustHaveContract = False
            ctrlFindManager.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindManager_EmployeeSelected(sender As Object, e As System.EventArgs) Handles ctrlFindManager.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindManager.SelectedEmployee(0)
            txtManagerNew.Text = objEmployee.FULLNAME_VN
            hidManager.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function Count_All_Column() As Integer
        Dim count As Integer
        Dim SumAllColImport As Integer
        SumAllColImport = ColumnImportWelfare
        Return SumAllColImport
    End Function
    Private Function GetDataFromGrid(ByVal gr As RadGrid) As DataTable
        Dim bsSource As DataTable
        Try
            bsSource = New DataTable()
            For Each Col As GridColumn In gr.Columns
                Dim DataColumn As DataColumn = New DataColumn(Col.UniqueName)
                bsSource.Columns.Add(DataColumn)
            Next
            'coppy data to grid
            For Each Item As GridDataItem In gr.EditItems
                If Item.Display = False Then Continue For
                Dim Dr As DataRow = bsSource.NewRow()
                For Each col As GridColumn In gr.Columns
                    Try
                        If col.UniqueName = "cbStatus" Then
                            If Item.Selected = True Then
                                Dr(col.UniqueName) = 1
                            Else
                                Dr(col.UniqueName) = 0
                            End If
                            Continue For
                        End If
                        If InStr(",MONEY_TOTAL,REMARK,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "cb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rt"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
                                Case "rd"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadDatePicker).SelectedDate
                            End Select
                        Else
                            Dr(col.UniqueName) = Item.GetDataKeyValue(col.UniqueName)
                        End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                bsSource.Rows.Add(Dr)
            Next
            bsSource.AcceptChanges()
            Return bsSource
        Catch ex As Exception
        End Try
    End Function

    Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                Dim rtxtmoney As New RadNumericTextBox
                rtxtmoney = CType(edit.FindControl("rnMONEY"), RadNumericTextBox)
                SetDataToGrid_Org(edit)
                edit.Dispose()
                item.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim chk = DirectCast(sender, CheckBox)
            Select Case chk.ID
                Case "chkManagerNew"
                    If chkManagerNew.Checked Then
                        txtManagerNew.Visible = True
                        btnFindDirect.Visible = True
                        rqManagerNew.Enabled = True
                    Else
                        txtManagerNew.Visible = False
                        btnFindDirect.Visible = False
                        rqManagerNew.Enabled = False
                    End If
                Case "chkOrgName"
                    If chkOrgName.Checked Then
                        txtOrgName.Visible = True
                        btnFindOrg.Visible = True
                        rqOrgName.Enabled = True
                    Else
                        txtOrgName.Visible = False
                        btnFindOrg.Visible = False
                        rqOrgName.Enabled = False
                    End If
                Case "chkTitle"
                    If chkTitle.Checked Then
                        cboTitle.Visible = True
                        rqTitle.Enabled = True
                    Else
                        cboTitle.Visible = False
                        rqTitle.Enabled = False
                    End If
                Case "chkObjectAttendant"
                    If chkObjectAttendant.Checked Then
                        cboObjectAttendant.Visible = True
                        rqObjectAttendant.Enabled = True
                    Else
                        cboObjectAttendant.Visible = False
                        rqObjectAttendant.Enabled = False

                    End If
                Case "chkObjectEmp"
                    If chkObjectEmp.Checked Then
                        cboObjectEmp.Visible = True
                        rqObjectEmp.Enabled = True
                    Else
                        cboObjectEmp.Visible = False
                        rqObjectEmp.Enabled = False
                    End If
                Case "chkWorkPlace"
                    If chkWorkPlace.Checked Then
                        cboWorkPlace.Visible = True
                        rqWorkPlace.Enabled = True
                    Else
                        cboWorkPlace.Visible = False
                        rqWorkPlace.Enabled = False
                    End If
                Case "chkOBJECT_ATTENDANCE"
                    If chkOBJECT_ATTENDANCE.Checked Then
                        cbOBJECT_ATTENDANCE.Visible = True
                        rqOBJECT_ATTENDANCE.Enabled = True
                    Else
                        cbOBJECT_ATTENDANCE.Visible = False
                        rqOBJECT_ATTENDANCE.Enabled = False
                    End If
                Case "chkObjectLaborNew"
                    If chkObjectLaborNew.Checked Then
                        cboObjectLaborNew.Visible = True
                        reqObjectLabor.Enabled = True
                    Else
                        cboObjectLaborNew.Visible = False
                        reqObjectLabor.Enabled = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



#End Region

#Region "Custom"


    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim dtData As DataTable
        Dim store As New ProfileStoreProcedure
        Try

            dtDecisionType = store.GET_DECISION_TYPE_EXCEPT_NV()
            FillRadCombobox(cboDecisionType, dtDecisionType, "NAME", "ID")
            dtData = rep.GetOtherList("DECISION_STATUS", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(cboObjectEmp, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("OBJECT_ATTENDANT", True)
            FillRadCombobox(cboObjectAttendant, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("WORK_PLACE", True)
            FillRadCombobox(cboWorkPlace, dtData, "NAME", "ID")

            dtData = rep.GetOtherList(ProfileCommon.OBJECT_ATTENDANCE.Code)
            FillRadCombobox(cbOBJECT_ATTENDANCE, dtData, "NAME", "ID", True)

            dtData = rep.GetOtherList("OBJECT_LABOR")
            FillRadCombobox(cboObjectLaborNew, dtData, "NAME", "ID")

            dtData = rep.GetTitleList(True)
            cboTitle.ClearValue()
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim rep As New ProfileBusinessRepository
        Try
            WelfareMng = rep.GetWelfareMngById(_Id)
            If Not WelfareMng Is Nothing Then

                hidIDEmp.Value = WelfareMng.EMPLOYEE_ID
                'txtEmployeeCode.Text = WelfareMng.EMPLOYEE_CODE
                'txtTITLE.Text = WelfareMng.TITLE_NAME
                'txtEMPLOYEE.Text = WelfareMng.EMPLOYEE_NAME
                'txtORG.Text = WelfareMng.ORG_NAME

                If WelfareMng.EFFECT_DATE < Date.Now Then
                    MainToolBar.Items(0).Enabled = False
                    'LeftPane.Enabled = False
                End If
                txtSDESC.Text = WelfareMng.SDESC

                If WelfareMng.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    MainToolBar.Items(0).Enabled = False
                    LeftPane.Enabled = False
                End If
                If checkDelete <> 1 Then
                    Dim repst = New ProfileStoreProcedure
                    dtbImport = repst.Get_list_Welfare_EMP(_Id)
                    'Employee_PL = rep.GetlistWelfareEMP(_Id)
                    'dtbImport = Employee_PL.ToTable()
                End If

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("gUID"))
                CurrentState = CommonMessage.STATE_EDIT

            Else
                CurrentState = CommonMessage.STATE_NEW
                If Not IsPostBack Then
                    Employee_PL = New List(Of WorkingDTO)
                    dtbImport = Employee_PL.ToTable()
                End If
            End If
            rgEmployee.VirtualItemCount = dtbImport.Rows.Count 'Employee_PL.Count
            rgEmployee.DataSource = dtbImport

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetDataToGrid_Org(ByVal EditItem As GridEditableItem)
        Try
            For Each col As GridColumn In rgEmployee.Columns
                Try
                    'Dim groupid
                    'For Each LINE In Employee_PL
                    '    groupid = LINE.GROUP_ID
                    'Next
                    ' Employee_PL.Find(Function(f) f.EMPLOYEE_CODE = empployee_code)
                    Dim empployee_code = EditItem.GetDataKeyValue("EMPLOYEE_CODE")
                    'Dim ds = dtbImport.AsEnumerable().ToList()
                    Dim rowData = (From p In dtbImport Where p("EMPLOYEE_CODE") = empployee_code).ToList
                    If rowData Is Nothing Then
                        Exit Sub
                    End If
                    If InStr(",MONEY_TOTAL,REMARK,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "cb"

                            Case "rt"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = rowData(0)("REMARK")
                            Case "rn"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("MONEY_TOTAL").ToString()
                        End Select
                    Else
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub


    Private Sub cboDecisionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDecisionType.SelectedIndexChanged
        Try
            'GetSigner()
            'If cboDecisionType.Text = "Quyết định thay đổi QLTT" Then
            '    btnFindOrg.Enabled = False
            '    cboTitle.Enabled = False
            '    RequiredFieldValidator1.Enabled = False
            'Else
            '    btnFindOrg.Enabled = True
            '    cboTitle.Enabled = True
            '    RequiredFieldValidator1.Enabled = True
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        txtUploadFile.Text = ""
    '        Dim listExtension = New List(Of String)
    '        listExtension.Add(".xls")
    '        listExtension.Add(".xlsx")
    '        listExtension.Add(".txt")
    '        listExtension.Add(".ctr")
    '        listExtension.Add(".doc")
    '        listExtension.Add(".docx")
    '        listExtension.Add(".xml")
    '        listExtension.Add(".png")
    '        listExtension.Add(".jpg")
    '        listExtension.Add(".bitmap")
    '        listExtension.Add(".jpeg")
    '        listExtension.Add(".gif")
    '        listExtension.Add(".pdf")
    '        listExtension.Add(".rar")
    '        listExtension.Add(".zip")
    '        listExtension.Add(".ppt")
    '        listExtension.Add(".pptx")
    '        Dim fileName As String

    '        Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/")
    '        If ctrlUpload1.UploadedFiles.Count >= 1 Then
    '            For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
    '                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
    '                Dim str_Filename = Guid.NewGuid.ToString() + "\"
    '                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
    '                    System.IO.Directory.CreateDirectory(strPath + str_Filename)
    '                    strPath = strPath + str_Filename
    '                    fileName = System.IO.Path.Combine(strPath, file.FileName)
    '                    file.SaveAs(fileName, True)
    '                    'txtUploadFile.Text = file.FileName
    '                    Down_File = str_Filename
    '                Else
    '                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
    '                    Exit Sub
    '                End If
    '            Next
    '            'loadDatasource(txtUploadFile.Text)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub loadDatasource(ByVal strUpload As String)
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try

    '        If strUpload <> "" Then
    '            txtUploadFile.Text = strUpload
    '            FileOldName = txtUpload.Text
    '            txtUpload.Text = strUpload
    '        Else

    '            strUpload = String.Empty
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub



    'Public Sub GetSigner()
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim store As New ProfileStoreProcedure
    '    Try
    '        ClearControlValue(txtSignName, txtSignTitle, hidSign)
    '        If IsDate(rdEffectDate.SelectedDate) AndAlso cboDecisionType.SelectedValue <> "" Then
    '            Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, cboDecisionType.SelectedValue)
    '            If signer.Rows.Count > 0 Then
    '                If IsNumeric(signer.Rows(0)("ID")) Then
    '                    hidSign.Value = CDec(signer.Rows(0)("ID"))
    '                End If
    '                txtSignName.Text = signer.Rows(0)("EMPLOYEE_NAME")
    '                txtSignTitle.Text = signer.Rows(0)("TITLE_NAME")
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        Throw ex
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Function GetDecisioNo(ByVal employee_code As String) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim decisionNo As String = ""
        Dim store As New ProfileStoreProcedure
        Try
            If Not String.IsNullOrEmpty(employee_code) AndAlso cboDecisionType.SelectedValue <> "" AndAlso IsDate(rdEffectDate.SelectedDate) AndAlso IsNumeric(hidOrg.Value) Then
                Dim org = store.GET_ORGNAME_2(hidOrg.Value)
                Dim typeCode = (From p In dtDecisionType Where p("ID") = cboDecisionType.SelectedValue Select p("CODE")).FirstOrDefault.ToString
                decisionNo = employee_code & "/" & rdEffectDate.SelectedDate.Value.ToString("MM/yyyy") & "/" & typeCode & "-" & org.Rows(0)("SHORT_NAME").ToString
            End If
            Return decisionNo
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub rdEffectDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'GetSigner()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub txtManagerNew_TextChanged(sender As Object, e As EventArgs) Handles txtManagerNew.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtManagerNew.Text <> "" Then
                    hidManager.Value = Nothing
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtManagerNew.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtManagerNew.Text = ""
                    ElseIf Count = 1 Then
                        Dim emp = EmployeeList(0)
                        txtManagerNew.Text = emp.FULLNAME_VN
                        hidManager.Value = emp.EMPLOYEE_ID
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindSignManager.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindSignManager.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindSignManager.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindManager", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtManagerNew.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            phFindSignManager.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 4
                        End If
                    End If
                Else
                    Hid_IsEnter.Value = Nothing
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtOrgName_TextChanged(sender As Object, e As EventArgs) Handles txtOrgName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtOrgName.Text.Trim <> "" Then
                Dim List_org = rep.GetOrganizationLocationTreeView()
                Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtOrgName.Text.Trim.ToUpper)).ToList
                If orgList.Count <= 0 Then
                    ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    ClearControlValue(hidOrg, txtOrgName)
                ElseIf orgList.Count = 1 Then
                    hidOrg.Value = orgList(0).ID
                    txtOrgName.Text = orgList(0).NAME_VN
                Else
                    List_Oganization_ID = (From p In orgList Select p.ID).ToList
                    btnFindOrg_Click(Nothing, Nothing)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class