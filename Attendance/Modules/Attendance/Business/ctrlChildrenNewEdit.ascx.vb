Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlChildrenNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
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
    Property Employee_id As Integer?
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer?)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property
    Property Org_id As Decimal?
        Get
            Return ViewState(Me.ID & "_Org_id")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Org_id") = value
        End Set
    End Property
    Property ID_AT_SWIPE As String
        Get
            Return ViewState(Me.ID & "_ID_AT_SWIPE")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ID_AT_SWIPE") = value
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
    Property RegisterDMVS As AT_LATE_COMBACKOUTDTO
        Get
            Return ViewState(Me.ID & "_AT_LATE_COMBACKOUTDTO")
        End Get
        Set(ByVal value As AT_LATE_COMBACKOUTDTO)
            ViewState(Me.ID & "_AT_LATE_COMBACKOUTDTO") = value
        End Set
    End Property

    Property RegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO)
        Get
            Return ViewState(Me.ID & "_objRegisterDMVSList")
        End Get
        Set(ByVal value As List(Of AT_LATE_COMBACKOUTDTO))
            ViewState(Me.ID & "_objRegisterDMVSList") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
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
            GetParams()
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Dim periodid As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If Not IsPostBack Then
            '    If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
            '        periodid = Request.Params("periodid")
            '        Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
            '        If period.START_DATE IsNot Nothing Then
            '            rdWorkingday.MinDate = period.START_DATE
            '        End If
            '        If period.END_DATE IsNot Nothing Then
            '            rdWorkingday.MaxDate = period.END_DATE
            '        End If
            '    End If
            'End If
            Select Case Message
                Case "UpdateView"
                    Dim obj As New AT_LATE_COMBACKOUTDTO
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GETCHILDREN_TAKECAREBYID(Decimal.Parse(Request.Params("ID")))
                    RegisterDMVS = New AT_LATE_COMBACKOUTDTO
                    If obj IsNot Nothing Then
                        rgData.Enabled = False
                        RegisterDMVSList = New List(Of AT_LATE_COMBACKOUTDTO)
                        Dim item As New AT_LATE_COMBACKOUTDTO
                        item.EMPLOYEE_CODE = obj.EMPLOYEE_CODE
                        item.VN_FULLNAME = obj.VN_FULLNAME
                        item.TITLE_NAME = obj.TITLE_NAME
                        item.ORG_ID = obj.ORG_ID
                        item.ORG_NAME = obj.ORG_NAME
                        item.EMPLOYEE_ID = obj.EMPLOYEE_ID
                        RegisterDMVSList.Add(item)

                        RegisterDMVS.ORG_ID = obj.ORG_ID
                        RegisterDMVS.EMPLOYEE_ID = obj.EMPLOYEE_ID
                        Org_id = obj.ORG_ID
                        Employee_id = obj.EMPLOYEE_ID

                        rdWorkingday.SelectedDate = obj.FROM_DATE
                        rdExpireDay.SelectedDate = obj.END_DATE
                        txtGhiChu.Text = obj.REMARK
                        chkTS_Exception.Checked = obj.TS_EXCEPTION
                        _Value = obj.ID
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien lay cac params chuyen sang tu trang view
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetParams()
        Dim ID As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request.Params("ID") IsNot Nothing Then
                Refresh("UpdateView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien CancelClicked cua control ctrlFindPopup
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
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As AT_LATE_COMBACKOUTDTO
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim lstEmp As New List(Of Common.CommonBusiness.EmployeeDTO)
        Dim sAction As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    ' check kỳ công đã đóng
                    If RegisterDMVSList() Is Nothing Then
                        ShowMessage(Translate("CHƯA CHỌN NHÂN VIÊN ĐỂ ĐĂNG KÝ."), NotifyType.Error)
                        Exit Sub
                    End If
                    If _Value.HasValue Then
                        Dim repCheck As New CommonRepository
                        Dim lstCheck As New List(Of Decimal)
                        lstCheck.Add(_Value)
                        If repCheck.CheckExistIDTable(lstCheck, "AT_CHILDREN_TAKECARE", "ID") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                            Exit Sub
                        End If
                    End If
                    For index = 0 To RegisterDMVSList.Count - 1
                        lstEmp.Add(New Common.CommonBusiness.EmployeeDTO With {.ID = RegisterDMVSList(index).EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = RegisterDMVSList(index).EMPLOYEE_CODE,
                                                    .FULLNAME_VN = RegisterDMVSList(index).VN_FULLNAME})
                    Next
                    If chkTS_Exception.Checked = False Then
                        For Each item In lstEmp
                            Dim res = store.CHECK_CHILDREN(item.ID, rdWorkingday.SelectedDate, rdExpireDay.SelectedDate)
                            If res = 4 Then
                                Continue For
                            End If
                            If res = 0 Then
                                ShowMessage(Translate("Nhân viên " + item.FULLNAME_VN + " chưa từng đăng ký thai sản, không thể đăng ký nuôi con nhỏ."), NotifyType.Warning)
                                Exit Sub
                            End If
                            If res = 1 Then
                                ShowMessage(Translate("Thời gian đăng ký trước nghỉ thai sản, chọn ngày đăng ký khác."), NotifyType.Warning)
                                Exit Sub
                            End If
                            If res = 2 Then
                                ShowMessage(Translate("Ngày đăng ký nuôi con nhỏ vượt 12 tháng."), NotifyType.Warning)
                                Exit Sub
                            End If
                            If res = 3 Then
                                ShowMessage(Translate("Ngày kết thúc nuôi con nhỏ vượt 12 tháng."), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                    End If
                    If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                               rdWorkingday.SelectedDate, rdWorkingday.SelectedDate, sAction) Then
                        ShowMessage(Translate(sAction), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim DELETE_ALL = 1
                    Dim strID_EMP = ""
                    For idex = 0 To RegisterDMVSList.Count - 1
                        strID_EMP = strID_EMP & RegisterDMVSList(idex).EMPLOYEE_ID & ","
                    Next
                    If _Value.HasValue Then
                        obj = New AT_LATE_COMBACKOUTDTO
                        obj.ID = _Value
                        obj.REMARK = txtGhiChu.Text.Trim
                        obj.WORKINGDAY = rdWorkingday.SelectedDate
                        obj.END_DATE = rdExpireDay.SelectedDate
                        obj.TS_EXCEPTION = chkTS_Exception.Checked
                        'If rep.ValidateLate_combackout(obj) Then
                        '    ShowMessage(Translate("Ngày đăng ký đã tồn tại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rep.ModifyChildren(obj, gstatus) Then
                            'Dim str As String = "getRadWindow().close('1');"
                            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            ''POPUPTOLINK

                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlChildren&group=Business")
                        End If
                    Else
                        obj = New AT_LATE_COMBACKOUTDTO
                        obj.REMARK = txtGhiChu.Text.Trim
                        obj.WORKINGDAY = rdWorkingday.SelectedDate
                        obj.END_DATE = rdExpireDay.SelectedDate
                        obj.TS_EXCEPTION = chkTS_Exception.Checked
                        ' không cần đoạn check này vì vào trong hàm insert đã kiểm tra nếu có rồi thì update
                        'If rep.ValidateLate_combackout(obj) Then
                        '    ShowMessage(Translate("Ngày đăng ký đã tồn tại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rep.InsertChildren(RegisterDMVSList, obj, gstatus) Then
                            'Dim str As String = "getRadWindow().close('1');"
                            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            ''POPUPTOLINK

                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlChildren&group=Business")
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlChildren&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If RegisterDMVSList() Is Nothing Then
                rgData.VirtualItemCount = 0
                rgData.DataSource = New List(Of String)
            Else
                'check employee exist in the rad gird
                If (rgData.VirtualItemCount > 0) Then
                    For idx = 0 To rgData.VirtualItemCount - 1
                        Dim item As GridDataItem = rgData.Items(idx)
                        Dim at_item As List(Of AT_LATE_COMBACKOUTDTO) = RegisterDMVSList.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).ToList()
                        If (at_item IsNot Nothing) Then
                            If (at_item.Count = 2) Then
                                RegisterDMVSList.Remove(at_item.Item(0))
                            End If
                        End If
                    Next
                End If

                rgData.VirtualItemCount = RegisterDMVSList.Count
                rgData.DataSource = RegisterDMVSList()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien ItemCommand cua control rgData
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgData.SelectedItems
                        Dim s = (From q In RegisterDMVSList Where
                                 q.EMPLOYEE_ID = i.GetDataKeyValue("EMPLOYEE_ID")).FirstOrDefault
                        RegisterDMVSList.Remove(s)
                    Next
                    rgData.Rebind()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                FillDataInControls(e.CurrentValue)
            End If
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly cac trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                        ctrlFindEmployeePopup.IsHideTerminate = False
                    End If
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Su ly su kien EmployeeSelected cua control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If RegisterDMVSList Is Nothing Then
                    RegisterDMVSList = New List(Of AT_LATE_COMBACKOUTDTO)
                End If
                'RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_LATE_COMBACKOUTDTO
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    RegisterDMVSList.Add(item)
                Next
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If
            rgData.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' create by: ChienNV
    ''' create date:16/10/2017
    ''' FILL DATA IN CONTROLS
    ''' txtOrgName2,txtOrgName,txtBan,txtTo
    ''' </summary>
    ''' <param name="orgid"></param>
    ''' <remarks></remarks>
    Private Sub FillDataInControls(ByVal orgid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim orgTree As OrganizationTreeDTO
        Using rep As New ProfileRepository
            Dim org = rep.GetOrganizationByID(orgid)
            If org IsNot Nothing Then

            End If
            orgTree = rep.GetTreeOrgByID(orgid)
        End Using
        Try

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
        End Try
    End Sub
#End Region
End Class

