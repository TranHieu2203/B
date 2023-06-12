Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGetSignDefault
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrlOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

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
    Public Overrides Property MustAuthorize As Boolean = True

    ''' <summary>
    ''' dsDataComper
    ''' </summary>
    ''' <remarks></remarks>
    Dim dsDataComper As New DataTable

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' isLoadPopupSP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopupSP As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopupSP")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopupSP") = value
        End Set
    End Property


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
    ''' InsertSetUp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertSetUp As List(Of AT_SIGNDEFAULTDTO)
        Get
            Return ViewState(Me.ID & "_InsertSetUp")
        End Get
        Set(ByVal value As List(Of AT_SIGNDEFAULTDTO))
            ViewState(Me.ID & "_InsertSetUp") = value
        End Set
    End Property

    ''' <summary>
    ''' AT_SignDF
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_SignDF As List(Of AT_SIGNDEFAULTDTO)
        Get
            Return ViewState(Me.ID & "_SignDF")
        End Get
        Set(ByVal value As List(Of AT_SIGNDEFAULTDTO))
            ViewState(Me.ID & "_SignDF") = value
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
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("TITLE_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("EFFECT_DATE_FROM", GetType(String))
                dt.Columns.Add("EFFECT_DATE_TO", GetType(String))
                dt.Columns.Add("SHIFT_ID", GetType(String))
                dt.Columns.Add("SHIFT_NAME", GetType(String))
                dt.Columns.Add("SHIFT_SAT_ID", GetType(String))
                dt.Columns.Add("SHIFT_SAT_NAME", GetType(String))
                dt.Columns.Add("SHIFT_SUN_ID", GetType(String))
                dt.Columns.Add("SHIFT_SUN_NAME", GetType(String))

                dt.Columns.Add("SIGN_FRI_ID", GetType(String))
                dt.Columns.Add("SIGN_FRI_NAME", GetType(String))
                dt.Columns.Add("SIGN_THU_ID", GetType(String))
                dt.Columns.Add("SIGN_THU_NAME", GetType(String))
                dt.Columns.Add("SIGN_WED_ID", GetType(String))
                dt.Columns.Add("SIGN_WED_NAME", GetType(String))
                dt.Columns.Add("SIGN_TUE_ID", GetType(String))
                dt.Columns.Add("SIGN_TUE_NAME", GetType(String))


                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueSign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueSign As Decimal
        Get
            Return ViewState(Me.ID & "_ValueSign")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueSign") = value
        End Set
    End Property

    Property ListShifts As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_ListShifts")
        End Get
        Set(value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_ListShifts") = value
        End Set
    End Property

    Property compareYear_Date As String
        Get
            Return ViewState(Me.ID & "_compareYear_Date")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_compareYear_Date") = value
        End Set
    End Property

    Property ValTodate As DateTime?
        Get
            Return ViewState(Me.ID & "_valTodate")
        End Get
        Set(value As DateTime?)
            ViewState(Me.ID & "_valTodate") = value
        End Set
    End Property

    Property valJoindate As DateTime?
        Get
            Return ViewState(Me.ID & "_valJoindate")
        End Get
        Set(value As DateTime?)
            ViewState(Me.ID & "_valJoindate") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            SetGridFilter(rgWorkschedule)
            rgWorkschedule.AllowCustomPaging = True

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            CType(MainToolBar.Items(8), RadToolBarButton).Attributes("Authorize") = CommonMessage.AUTHORIZE_PRINT
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceBusinessClient

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWorkschedule.Rebind()
                        SelectedItemDataGridByKey(rgWorkschedule, IDSelect, , rgWorkschedule.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWorkschedule.CurrentPageIndex = 0
                        rgWorkschedule.MasterTableView.SortExpressions.Clear()
                        rgWorkschedule.Rebind()
                        SelectedItemDataGridByKey(rgWorkschedule, IDSelect, )
                    Case "Cancel"
                        rgWorkschedule.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SIGNDEFAULTDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgWorkschedule, obj)
            Dim Sorts As String = rgWorkschedule.MasterTableView.SortExpressions.GetSortString()


            If ctrlOrg.CurrentValue IsNot Nothing Then
                obj.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                rgWorkschedule.DataSource = New List(Of AT_SIGNDEFAULTDTO)
                '_filter.param.ORG_ID = psp.GET_ID_ORG()
                Exit Function
            End If

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_SignDF = rep.GetAT_SIGNDEFAULT(obj, rgWorkschedule.CurrentPageIndex, rgWorkschedule.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_SignDF = rep.GetAT_SIGNDEFAULT(obj, rgWorkschedule.CurrentPageIndex, rgWorkschedule.PageSize, MaximumRows)
                End If
                rgWorkschedule.VirtualItemCount = MaximumRows
                rgWorkschedule.DataSource = Me.AT_SignDF
            Else
                Return rep.GetAT_SIGNDEFAULT(obj).ToTable
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
                Case 2
                    phPopup.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    rdFromDate.AutoPostBack = True
                    EnableControlAll(True, txtNote, rdFromDate, txtEmpCode, rdToDate, cboSign, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    EnableControlAll(False, txtEmpName, txtOrg, txtTitle)
                    btnChooseEmployee.Enabled = True
                    'txtEmpCode.AutoPostBack = True
                    'ExcuteScript("Clear", "clRadDatePicker()")
                    EnabledGridNotPostback(rgWorkschedule, False)
                Case CommonMessage.STATE_NORMAL
                    btnChooseEmployee.Enabled = False
                    rdFromDate.AutoPostBack = False
                    'txtEmpCode.AutoPostBack = False
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, rdToDate, txtTitle, txtNote, rdFromDate, cboSign, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    EnableControlAll(False, txtEmpCode, txtNote, rdFromDate, rdToDate, cboSign, txtEmpName, txtOrg, txtTitle, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    EnabledGridNotPostback(rgWorkschedule, True)
                Case CommonMessage.STATE_EDIT
                    btnChooseEmployee.Enabled = True
                    rdFromDate.AutoPostBack = True
                    'txtEmpCode.AutoPostBack = True
                    EnableControlAll(True, rdFromDate, txtEmpCode, rdToDate, txtNote, cboSign, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    EnableControlAll(False, txtEmpName, txtOrg, txtTitle)
                    EnabledGridNotPostback(rgWorkschedule, False)
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorkschedule.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, cboSign, rdFromDate, rdToDate, txtNote, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorkschedule.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, cboSign, rdFromDate, rdToDate, txtNote, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE

                    'Dim lstDeletes As New List(Of Decimal)

                    'Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                    '                                .IS_DISSOLVE = ctrlOrg.IsDissolve}

                    'Dim bcheck As Integer = 0
                    'For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                    '    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                    '        ValTodate = New DateTime(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM")).Year, 12, 31)
                    '    End If
                    '    Hid_IsEnter.Value = "ISENTER"
                    '    CurrentState = CommonMessage.TOOLBARITEM_DELETE
                    '    txtEmpCode_TextChanged(Nothing, Nothing)
                    '    'Kiểm tra nếu đã tồn tại 1 trong 3 bảng
                    '    '1. Đăng ký nghỉ chi tiết: AT_LEAVESHEET_DETAIL
                    '    '2. Đăng ký làm thêm: AT_OT_REGISTRATION
                    '    '3. Bảng công gốc: AT_TIME_TIMESHEET_MACHINET
                    '    '=> cảnh báo và không thực hiện tiếp
                    '    Dim objTimeMachine

                    '    objTimeMachine = New AT_TIME_TIMESHEET_MACHINETDTO
                    '    objTimeMachine.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    '    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                    '        objTimeMachine.FROM_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    '    End If
                    '    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                    '        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                    '            objTimeMachine.END_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                    '        End If
                    '    ElseIf ValTodate IsNot Nothing Then
                    '        objTimeMachine.END_DATE = ValTodate
                    '    End If
                    '    Dim bcheck_TimeTimesheet_machine = rep.GetMachines(objTimeMachine, _param)
                    '    If bcheck_TimeTimesheet_machine.Count > 0 Then
                    '        bcheck += 1
                    '    End If

                    '    Dim objLeave = New AT_LEAVESHEETDTO
                    '    objLeave.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    '    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                    '        objLeave.FROM_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    '    End If
                    '    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                    '        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                    '            objLeave.END_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                    '        End If
                    '    ElseIf ValTodate IsNot Nothing Then
                    '        objLeave.END_DATE = ValTodate
                    '    End If
                    '    objLeave.MANUAL_CODE = "TS,NKL"
                    '    Dim bcheck_LeaveSheet = rep.GetLeaveSheet(objLeave, _param)
                    '    If bcheck_LeaveSheet.Count > 0 Then
                    '        bcheck -= 1
                    '    End If

                    '    Dim objRegOT = New AT_OT_REGISTRATIONDTO
                    '    objRegOT.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    '    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                    '        objRegOT.REGIST_DATE_FROM = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    '    End If
                    '    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                    '        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                    '            objRegOT.REGIST_DATE_TO = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                    '        End If
                    '    ElseIf ValTodate IsNot Nothing Then
                    '        objRegOT.REGIST_DATE_TO = ValTodate
                    '    End If
                    '    Dim bcheck_RegDataOT = rep.GetRegData(objRegOT, _param)
                    '    If bcheck_RegDataOT.Rows.Count > 0 Then
                    '        bcheck += 1
                    '    End If

                    '    If bcheck > 0 Then
                    '        'ShowMessage(Translate("Ca làm việc của ngày hiệu lực mới đã phát sinh dữ liệu liên quan."), NotifyType.Warning)
                    '        'Exit Sub
                    '        Continue For
                    '    Else
                    '        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    '    End If
                    'Next
                    'If bcheck > 0 Then
                    '    ShowMessage(Translate("Tồn tại dữ liệu có Ca làm việc của ngày hiệu lực mới đã phát sinh dữ liệu liên quan."), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If rep.DeleteAT_SIGNDEFAULT(lstDeletes) Then
                    '    Refresh("UpdateView")
                    '    UpdateControlState()
                    '    rgWorkschedule.Rebind()
                    '    ExcuteScript("Clear", "clRadDatePicker()")
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    UpdateControlState()
                    'End If
            End Select

            btnChooseEmployee.Focus()
            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("TITLE_ID", hidTitleID)
            dic.Add("ORG_NAME", txtOrg)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("EMPLOYEE_CODE", txtEmpCode)
            dic.Add("EMPLOYEE_ID", hidEmpID)
            dic.Add("EMPLOYEE_NAME", txtEmpName)
            dic.Add("TITLE_NAME", txtTitle)
            dic.Add("SINGDEFAULE", cboSign)
            dic.Add("SING_SUN", cboSignSun)
            dic.Add("SING_SAT", cboSignSat)
            dic.Add("EFFECT_DATE_FROM", rdFromDate)
            dic.Add("EFFECT_DATE_TO", rdToDate)
            dic.Add("NOTE", txtNote)
            dic.Add("SIGN_TUE", cboSignTue)
            dic.Add("SIGN_WED", cboSignWed)
            dic.Add("SIGN_THU", cboSignThu)
            dic.Add("SIGN_FRI", cboSignFri)
            dic.Add("ID", hidID)
            dic.Add("JOIN_DATE", hidJoinDate)
            Utilities.OnClientRowSelectedChanged(rgWorkschedule, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click button chon nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChooseEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnChooseEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnChooseEmployee.ID
                    ctrlFindEmployeePopup.IsHideTerminate = False
                    'Update By : Tran Ngoc Hung
                    'Update Date : 12/12/2022
                    'Description : BCG-874
                    ctrlFindEmployeePopup.IsShowKiemNhiem = True
                    ctrlFindEmployeePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSIGN As New AT_SIGNDEFAULTDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim store As New AttendanceStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    hidID.Value = 0
                    cboSign.SelectedIndex = 0
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, rdToDate, cboSign, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri, rdFromDate)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim repCom As New CommonRepository
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgWorkschedule.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgWorkschedule.SelectedItems(0)
                    hidID.Value = CDec(item.GetDataKeyValue("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    Hid_IsEnter.Value = "ISENTER"
                    'txtEmpCode_TextChanged(Nothing, Nothing)
                    'isValidate = True
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If valJoindate IsNot Nothing And rdFromDate.SelectedDate IsNot Nothing AndAlso valJoindate > rdFromDate.SelectedDate Then
                            ShowMessage(Translate(Format("Ngày hiệu lực từ nhỏ hơn ngày vào làm ({0}). Vui lòng nhập lại."), valJoindate), NotifyType.Warning)
                            rdFromDate.Focus()
                            Exit Sub
                        End If
                        If rdFromDate.SelectedDate IsNot Nothing Then
                            ValTodate = New DateTime(rdFromDate.SelectedDate.Value.Year, 12, 31)
                        End If

                        Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                                                        .IS_DISSOLVE = ctrlOrg.IsDissolve}
                        Dim obj As New AT_SIGNDEFAULTDTO
                        obj.EMPLOYEE_ID = hidEmpID.Value
                        Dim check = rep.GetAT_SIGNDEFAULT(obj)
                        If check IsNot Nothing AndAlso check.Count > 0 Then
                            compareYear_Date = String.Join(",", (From p In check.AsQueryable Select p.EFFECT_DATE_FROM.Value.Year Distinct).ToArray)
                        End If
                        If compareYear_Date IsNot Nothing AndAlso compareYear_Date.Split(",").Contains(rdFromDate.SelectedDate.Value.Year) Then
                            Dim bcheck As Integer = 0
                            Dim objTimeMachine

                            objTimeMachine = New AT_TIME_TIMESHEET_MACHINETDTO
                            objTimeMachine.EMPLOYEE_ID = hidEmpID.Value
                            If rdFromDate.SelectedDate IsNot Nothing Then
                                objTimeMachine.FROM_DATE = rdFromDate.SelectedDate
                            End If
                            If rdToDate.SelectedDate IsNot Nothing Then
                                objTimeMachine.END_DATE = rdToDate.SelectedDate
                            Else
                                objTimeMachine.END_DATE = ValTodate
                            End If
                            Dim bcheck_TimeTimesheet_machine = rep.GetMachines(objTimeMachine, _param)
                            If bcheck_TimeTimesheet_machine.Count > 0 Then
                                bcheck += 1
                            End If

                            Dim objLeave = New AT_LEAVESHEETDTO
                            objLeave.EMPLOYEE_ID = hidEmpID.Value
                            If rdFromDate.SelectedDate IsNot Nothing Then
                                objLeave.FROM_DATE = rdFromDate.SelectedDate
                            End If
                            If rdToDate.SelectedDate IsNot Nothing Then
                                objLeave.END_DATE = rdToDate.SelectedDate
                            Else
                                objLeave.END_DATE = ValTodate
                            End If
                            'objLeave.MANUAL_CODE = "TS,NKL"
                            Dim bcheck_LeaveSheet = rep.GetLeaveSheet(objLeave, _param)
                            If bcheck_LeaveSheet.Count > 0 Then
                                bcheck += 1
                            End If

                            Dim objRegOT = New AT_OT_REGISTRATIONDTO
                            objRegOT.EMPLOYEE_ID = hidEmpID.Value
                            If rdFromDate.SelectedDate IsNot Nothing Then
                                objRegOT.REGIST_DATE_FROM = rdFromDate.SelectedDate
                            End If

                            If rdToDate.SelectedDate IsNot Nothing Then
                                objRegOT.REGIST_DATE_TO = rdToDate.SelectedDate
                            Else
                                objRegOT.REGIST_DATE_TO = ValTodate
                            End If
                            Dim bcheck_RegDataOT = rep.GetRegData(objRegOT, _param)
                            If bcheck_RegDataOT.Rows.Count > 0 Then
                                bcheck += 1
                            End If

                            If bcheck > 0 Then
                                ShowMessage(Translate("Ca làm việc của ngày hiệu lực mới đã phát sinh dữ liệu liên quan."), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        objSIGN.EFFECT_DATE_FROM = rdFromDate.SelectedDate
                        objSIGN.EFFECT_DATE_TO = rdToDate.SelectedDate
                        'If Not store.CHECK_DATE_SIGN_DEFAULT(hidID.Value, hidEmpID.Value, objSIGN.EFFECT_DATE_FROM) Then
                        '    ShowMessage(Translate("Ngày hiệu lực phải lớn hơn ngày hiệu lực trước đó trong tháng"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If cboSign.SelectedValue = "" Then
                        '    cboSign.SelectedValue = Nothing
                        'Else
                        '    objSIGN.SINGDEFAULE = cboSign.SelectedValue
                        'End If
                        'If ValueSign <> 0 Then
                        '    objSIGN.SINGDEFAULE = ValueSign
                        'End If
                        'Dim count = 0
                        'If cboSign.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignSat.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignSun.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignTue.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignWed.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignThu.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If cboSignFri.Text.Contains("OFF") Then
                        '    count += 1
                        'End If
                        'If count >= 2 Then
                        '    ShowMessage(Translate("1 tuần chỉ được đăng kí 1 ca OFF"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If cboSign.SelectedValue <> "" Then
                            objSIGN.SINGDEFAULE = cboSign.SelectedValue
                        End If
                        If IsNumeric(cboSignSat.SelectedValue) Then
                            objSIGN.SING_SAT = cboSignSat.SelectedValue
                        End If
                        If IsNumeric(cboSignSun.SelectedValue) Then
                            objSIGN.SING_SUN = cboSignSun.SelectedValue
                        End If
                        If IsNumeric(cboSignTue.SelectedValue) Then
                            objSIGN.SIGN_TUE = cboSignTue.SelectedValue
                        End If
                        If IsNumeric(cboSignWed.SelectedValue) Then
                            objSIGN.SIGN_WED = cboSignWed.SelectedValue
                        End If
                        If IsNumeric(cboSignThu.SelectedValue) Then
                            objSIGN.SIGN_THU = cboSignThu.SelectedValue
                        End If
                        If IsNumeric(cboSignFri.SelectedValue) Then
                            objSIGN.SIGN_FRI = cboSignFri.SelectedValue
                        End If
                        objSIGN.NOTE = txtNote.Text.Trim
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rdFromDate.SelectedDate IsNot Nothing And hidJoinDate.Value <> "" Then
                                    If rdFromDate.SelectedDate < hidJoinDate.Value Then
                                        ClearControlValue(rdFromDate)
                                        ShowMessage("Ngày hiệu lực phải lớn hơn ngày vào công ty", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                objSIGN.EMPLOYEE_ID = InsertSetUp(0).EMPLOYEE_ID
                                objSIGN.TITLE_ID = InsertSetUp(0).TITLE_ID
                                objSIGN.ORG_ID = InsertSetUp(0).ORG_ID
                                objSIGN.ACTFLG = "A"
                                If rep.InsertAT_SIGNDEFAULT(objSIGN, gID, _param) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, cboSign, rdFromDate, rdToDate, txtNote, cboSignTue, cboSignThu, cboSignWed, cboSignFri, valJoindate, ValTodate)
                                    rgWorkschedule.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If rdFromDate.SelectedDate IsNot Nothing And hidJoinDate.Value <> "" Then
                                    If rdFromDate.SelectedDate < hidJoinDate.Value Then
                                        ClearControlValue(rdFromDate)
                                        ShowMessage("Ngày hiệu lực phải lớn hơn ngày vào công ty", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(hidID.Value))

                                If cmRep.CheckExistIDTable(lstID, "AT_SIGNDEFAULT", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                objSIGN.EMPLOYEE_ID = hidEmpID.Value
                                objSIGN.TITLE_ID = hidTitleID.Value
                                objSIGN.ORG_ID = hidOrgID.Value
                                objSIGN.ID = hidID.Value
                                'objSIGN.ID = rgWorkschedule.SelectedValue
                                If rep.ModifyAT_SIGNDEFAULT(objSIGN, rgWorkschedule.SelectedValue, _param) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objSIGN.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, cboSign, rdFromDate, rdToDate, txtNote, cboSignTue, cboSignThu, cboSignWed, cboSignFri, valJoindate, ValTodate)
                                    rgWorkschedule.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "setDefaultSize()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    ExcuteScript("Clear", "clRadDatePicker()")
                    UpdateControlState()

                Case "EXPORT_TEMP"
                    isLoadPopup = 2 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()

                Case "IMPORT_TEMP"
                    ctrlUpload.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgWorkschedule.ExportExcel(Server, Response, dtDatas, "CaMacDinh")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
            rep.Dispose()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
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
        Dim rep As New AttendanceRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'CurrentState = CommonMessage.STATE_DELETE
                'UpdateControlState()
                Dim lstDeletes As New List(Of Decimal)

                Dim _param = New ParamDTO With {.ORG_ID = ctrlOrg.CurrentValue,
                                                .IS_DISSOLVE = ctrlOrg.IsDissolve}

                Dim bcheck As Integer = 0
                For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                        ValTodate = New DateTime(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM")).Year, 12, 31)
                    End If
                    Hid_IsEnter.Value = "ISENTER"
                    'CurrentState = CommonMessage.TOOLBARITEM_DELETE
                    'txtEmpCode_TextChanged(Nothing, Nothing)
                    'Kiểm tra nếu đã tồn tại 1 trong 3 bảng
                    '1. Đăng ký nghỉ chi tiết: AT_LEAVESHEET_DETAIL
                    '2. Đăng ký làm thêm: AT_OT_REGISTRATION
                    '3. Bảng công gốc: AT_TIME_TIMESHEET_MACHINET
                    '=> cảnh báo và không thực hiện tiếp
                    Dim objTimeMachine

                    objTimeMachine = New AT_TIME_TIMESHEET_MACHINETDTO
                    objTimeMachine.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                        objTimeMachine.FROM_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    End If
                    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                            objTimeMachine.END_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                        End If
                    ElseIf ValTodate IsNot Nothing Then
                        objTimeMachine.END_DATE = ValTodate
                    End If
                    Dim bcheck_TimeTimesheet_machine = rep.GetMachines(objTimeMachine, _param)
                    If bcheck_TimeTimesheet_machine.Count > 0 Then
                        bcheck += 1
                    End If

                    Dim objLeave = New AT_LEAVESHEETDTO
                    objLeave.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                        objLeave.FROM_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    End If
                    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                            objLeave.END_DATE = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                        End If
                    ElseIf ValTodate IsNot Nothing Then
                        objLeave.END_DATE = ValTodate
                    End If
                    objLeave.MANUAL_CODE = "TS,NKL"
                    Dim bcheck_LeaveSheet = rep.GetLeaveSheet(objLeave, _param)
                    If bcheck_LeaveSheet.Count > 0 Then
                        bcheck -= 1
                    End If

                    Dim objRegOT = New AT_OT_REGISTRATIONDTO
                    objRegOT.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                    If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))) Then
                        objRegOT.REGIST_DATE_FROM = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_FROM"))
                    End If
                    If item.GetDataKeyValue("EFFECT_DATE_TO") IsNot Nothing Then
                        If IsDate(Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))) Then
                            objRegOT.REGIST_DATE_TO = Date.Parse(item.GetDataKeyValue("EFFECT_DATE_TO"))
                        End If
                    ElseIf ValTodate IsNot Nothing Then
                        objRegOT.REGIST_DATE_TO = ValTodate
                    End If
                    Dim bcheck_RegDataOT = rep.GetRegData(objRegOT, _param)
                    If bcheck_RegDataOT.Rows.Count > 0 Then
                        bcheck += 1
                    End If

                    If bcheck > 0 Then
                        'ShowMessage(Translate("Ca làm việc của ngày hiệu lực mới đã phát sinh dữ liệu liên quan."), NotifyType.Warning)
                        'Exit Sub
                        Continue For
                    Else
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    End If
                Next
                If bcheck > 0 Then
                    ShowMessage(Translate("Tồn tại dữ liệu có Ca làm việc của ngày hiệu lực mới đã phát sinh dữ liệu liên quan."), NotifyType.Warning)
                    Exit Sub
                End If
                If rep.DeleteAT_SIGNDEFAULT(lstDeletes) Then
                    Refresh("UpdateView")
                    UpdateControlState()
                    rgWorkschedule.Rebind()
                    ExcuteScript("Clear", "clRadDatePicker()")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    UpdateControlState()
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'CreateDataFilter()
            'Refresh()
            'rgWorkschedule.CurrentPageIndex = 0
            'rgWorkschedule.MasterTableView.SortExpressions.Clear()
            rgWorkschedule.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [chọn] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_SingDefault&orgid= " & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If

            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("DataImportSignDefault") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()

            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next

            If loadToGrid() Then
                Dim objSIGN As AT_SIGNDEFAULTDTO
                Dim gID As Decimal
                Dim dtDataImp As DataTable = dsDataPrepare.Tables(0)
                Dim _param = New ParamDTO With {.ORG_ID = 1,
                                                .IS_DISSOLVE = ctrlOrg.IsDissolve}
                For Each dr In dsDataComper.Rows
                    objSIGN = New AT_SIGNDEFAULTDTO
                    objSIGN.EFFECT_DATE_FROM = ToDate(dr("EFFECT_DATE_FROM"))
                    objSIGN.EFFECT_DATE_TO = ToDate(dr("EFFECT_DATE_TO"))
                    objSIGN.SINGDEFAULE = CInt(dr("SHIFT_ID"))
                    objSIGN.SING_SAT = CInt(dr("SHIFT_SAT_ID"))
                    objSIGN.SING_SUN = CInt(dr("SHIFT_SUN_ID"))
                    objSIGN.EMPLOYEE_ID = CInt(dr("EMPLOYEE_ID"))
                    objSIGN.SIGN_FRI = CInt(dr("SIGN_FRI_ID"))
                    objSIGN.SIGN_THU = CInt(dr("SIGN_THU_ID"))
                    objSIGN.SIGN_WED = CInt(dr("SIGN_WED_ID"))
                    objSIGN.SIGN_TUE = CInt(dr("SIGN_TUE_ID"))
                    objSIGN.ACTFLG = "A"
                    objSIGN.NOTE = dr("NOTE")
                    rep.InsertAT_SIGNDEFAULT(objSIGN, gID, _param)
                Next

                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra trươc khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Dim store As New AttendanceStoreProcedure
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If

            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtEmpID As DataTable
            Dim is_Validate As Boolean
            Dim _validate As New AT_SIGNDEFAULTDTO
            Dim rep As New AttendanceRepository
            Dim lstEmp As New List(Of String)
            Dim shift_1 As String = ""
            Dim shift_2 As String = ""
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 5
            Dim irowEm = 5

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                sError = "Ca mặc định"
                ImportValidate.IsValidList("SHIFT_NAME", "SHIFT_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SHIFT_SAT_NAME", "SHIFT_SAT_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SHIFT_SUN_NAME", "SHIFT_SUN_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SIGN_FRI_NAME", "SIGN_FRI_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SIGN_THU_NAME", "SIGN_THU_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SIGN_WED_NAME", "SIGN_WED_ID", row, rowError, isError, sError)
                ImportValidate.IsValidList("SIGN_TUE_NAME", "SIGN_TUE_ID", row, rowError, isError, sError)
                sError = "Ngày hiệu lực không được để trống"
                ImportValidate.IsValidDate("EFFECT_DATE_FROM", row, rowError, isError, sError)
                'sError = "Ngày hết hiệu lực không được để trống"
                'ImportValidate.IsValidDate("EFFECT_DATE_TO", row, rowError, isError, sError)

                If IsNumeric(row("SHIFT_ID")) AndAlso IsNumeric(row("SHIFT_SUN_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SUN_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_SUN_ID"), row("SHIFT_ID")) Then
                        rowError("SHIFT_ID") = "Giờ ra ngày ca Chủ nhật và giờ vào ca Thứ 2 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SIGN_TUE_ID")) AndAlso IsNumeric(row("SHIFT_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_TUE_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_ID"), row("SIGN_TUE_ID")) Then
                        rowError("SIGN_TUE_ID") = "Giờ ra ngày ca Thứ 2 và giờ vào ca Thứ 3 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SIGN_WED_ID")) AndAlso IsNumeric(row("SIGN_TUE_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_TUE_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_WED_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_TUE_ID"), row("SIGN_WED_ID")) Then
                        rowError("SIGN_WED_ID") = "Giờ ra ngày ca Thứ 3 và giờ vào ca Thứ 4 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SIGN_THU_ID")) AndAlso IsNumeric(row("SIGN_WED_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_WED_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_THU_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_WED_ID"), row("SIGN_THU_ID")) Then
                        rowError("SIGN_THU_ID") = "Giờ ra ngày ca Thứ 4 và giờ vào ca Thứ 5 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SIGN_FRI_ID")) AndAlso IsNumeric(row("SIGN_THU_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_THU_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_FRI_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_THU_ID"), row("SIGN_FRI_ID")) Then
                        rowError("SIGN_FRI_ID") = "Giờ ra ngày ca Thứ 5 và giờ vào ca Thứ 6 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SHIFT_SAT_ID")) AndAlso IsNumeric(row("SIGN_FRI_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_FRI_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SAT_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_FRI_ID"), row("SHIFT_SAT_ID")) Then
                        rowError("SHIFT_SAT_ID") = "Giờ ra ngày ca Thứ 6 và giờ vào ca Thứ 7 nhỏ hơn 12 tiếng "
                    End If
                End If

                If IsNumeric(row("SHIFT_SUN_ID")) AndAlso IsNumeric(row("SHIFT_SAT_ID")) Then
                    shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SAT_ID")) Select p.CODE).FirstOrDefault
                    shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SUN_ID")) Select p.CODE).FirstOrDefault
                    If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_SAT_ID"), row("SHIFT_SUN_ID")) Then
                        rowError("SHIFT_SUN_ID") = "Giờ ra ngày ca Thứ 7 và giờ vào ca Chủ nhật nhỏ hơn 12 tiếng "
                    End If
                End If

                If rowError("EFFECT_DATE_FROM").ToString = "" And _
                    rowError("EFFECT_DATE_TO").ToString = "" And _
                     row("EFFECT_DATE_FROM").ToString <> "" And _
                    row("EFFECT_DATE_TO").ToString <> "" Then
                    Dim startdate = Date.Parse(row("EFFECT_DATE_FROM"))
                    Dim enddate = Date.Parse(row("EFFECT_DATE_TO"))
                    If startdate > enddate Then
                        rowError("EFFECT_DATE_FROM") = "Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực"
                        isError = True
                    End If
                End If


                If isError Then
                    rowError("ID") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportEmployee.ImportRow(row)
                End If
                irow = irow + 1
                isError = False
            Next

            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            If isError Then
                Return False
            Else
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dsDataComper.Rows.Count - 1
                    rowError = dtError.NewRow
                    If dsDataComper(j)("EMPLOYEE_ID") = "" Then
                        dtEmpID = New DataTable
                        dtEmpID = rep.GetEmployeeIDInSign(dsDataComper(j)("EMPLOYEE_CODE"))

                        If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                            rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dsDataComper(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                            isError = True
                        Else
                            dsDataComper(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("ID")
                        End If
                    End If
                    ' check ngày hiệu lực bị trùng.
                    If dsDataComper(j)("EMPLOYEE_ID") <> "" Then
                        _validate.ID = 0
                        _validate.EMPLOYEE_ID = CDec(dsDataComper(j)("EMPLOYEE_ID"))
                        _validate.EFFECT_DATE_FROM = ToDate(dsDataComper(j)("EFFECT_DATE_FROM"))
                        _validate.EFFECT_DATE_TO = ToDate(dsDataComper(j)("EFFECT_DATE_TO"))
                        is_Validate = rep.ValidateAT_SIGNDEFAULT(_validate)
                        If Not rep.ValidateAT_SIGNDEFAULT(_validate) Then
                            rowError("EFFECT_DATE_FROM") = "Khoảng thời gian hiệu lực bị trùng."
                            isError = True
                        End If
                    End If
                    If isError Then
                        rowError("ID") = irowEm
                        dtError.Rows.Add(rowError)
                    End If
                    irowEm = irowEm + 1
                    isError = False
                Next

                If dtError.Rows.Count > 0 Then
                    dtError.TableName = "DATA"
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    'Private Sub rgWorkschedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorkschedule.ItemDataBound
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            ListShifts = ListComboData.LIST_LIST_SHIFT
            FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            FillDropDownList(cboSignTue, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignTue.SelectedValue)
            FillDropDownList(cboSignWed, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignWed.SelectedValue)
            FillDropDownList(cboSignThu, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignThu.SelectedValue)
            FillDropDownList(cboSignFri, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignFri.SelectedValue)
            FillDropDownList(cboSignSat, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignSat.SelectedValue)
            FillDropDownList(cboSignSun, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSignSun.SelectedValue)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve nhân viên khi click button [chọn] o popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)

                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_SIGNDEFAULTDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.EMPLOYEE_NAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.TITLE_ID = lstCommonEmployee(idx).TITLE_ID
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    InsertSetUp.Add(item)
                    txtEmpCode.Text = lstCommonEmployee(idx).EMPLOYEE_CODE
                    txtEmpName.Text = lstCommonEmployee(idx).FULLNAME_VN
                    txtOrg.Text = lstCommonEmployee(idx).ORG_NAME
                    txtTitle.Text = lstCommonEmployee(idx).TITLE_NAME
                    ' đẩy dữ liệu vào hider
                    hidEmpID.Value = lstCommonEmployee(idx).ID
                    hidOrgID.Value = lstCommonEmployee(idx).ORG_ID
                    hidTitleID.Value = lstCommonEmployee(idx).TITLE_ID
                    hidJoinDate.Value = lstCommonEmployee(idx).JOIN_DATE

                    'Kiểm tra đã tồn tại trong bảng AT_SIGNDEFAULT chưa
                    'Đã tồn tại thì bỏ qua 
                    'Không tồn tại
                    Dim obj As New AT_SIGNDEFAULTDTO
                    Dim rep_chk As New AttendanceRepository
                    obj.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    Dim check = rep_chk.GetAT_SIGNDEFAULT(obj)
                    valJoindate = lstCommonEmployee(idx).JOIN_DATE
                    If check.Count > 0 Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_EDIT, CommonMessage.STATE_DELETE
                                compareYear_Date = String.Join(",", (From p In check.AsQueryable Where p.ID <> hidID.Value Select p.EFFECT_DATE_FROM.Value.Year Distinct).ToArray)
                            Case CommonMessage.STATE_NEW
                                compareYear_Date = String.Join(",", (From p In check.AsQueryable Select p.EFFECT_DATE_FROM.Value.Year Distinct).ToArray)
                        End Select
                    Else
                        rdFromDate.SelectedDate = lstCommonEmployee(idx).JOIN_DATE
                    End If
                Next
                'SetGridEditRow()
                isLoadPopup = 0
            Else
                InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
                ' SetGridEditRow()
                isLoadPopup = 0
            End If

            rgWorkschedule.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub GetValue_Find_Emp(ByVal lstCommonEmployee As List(Of CommonBusiness.EmployeePopupFindListDTO))
        If lstCommonEmployee.Count <> 0 Then
            Dim rep As New AttendanceRepository
            InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)

            For idx = 0 To lstCommonEmployee.Count - 1
                Dim item As New AT_SIGNDEFAULTDTO
                item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                item.EMPLOYEE_NAME = lstCommonEmployee(idx).FULLNAME_VN
                item.TITLE_ID = lstCommonEmployee(idx).TITLE_ID
                item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                InsertSetUp.Add(item)
                txtEmpCode.Text = lstCommonEmployee(idx).EMPLOYEE_CODE
                txtEmpName.Text = lstCommonEmployee(idx).FULLNAME_VN
                txtOrg.Text = lstCommonEmployee(idx).ORG_NAME
                txtTitle.Text = lstCommonEmployee(idx).TITLE_NAME
                ' đẩy dữ liệu vào hider
                hidEmpID.Value = lstCommonEmployee(idx).ID
                hidOrgID.Value = lstCommonEmployee(idx).ORG_ID
                hidTitleID.Value = lstCommonEmployee(idx).TITLE_ID
                hidJoinDate.Value = lstCommonEmployee(idx).JOIN_DATE

                'Kiểm tra đã tồn tại trong bảng AT_SIGNDEFAULT chưa
                'Đã tồn tại thì bỏ qua 
                'Không tồn tại
                Dim obj As New AT_SIGNDEFAULTDTO
                obj.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                Dim check = rep.GetAT_SIGNDEFAULT(obj)
                valJoindate = lstCommonEmployee(idx).JOIN_DATE
                If check.Count > 0 Then
                    compareYear_Date = String.Join(",", (From p In check.AsQueryable Select p.EFFECT_DATE_FROM.Value.Year Distinct).ToArray)
                Else
                    rdFromDate.SelectedDate = lstCommonEmployee(idx).JOIN_DATE
                End If

            Next
        Else
            InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
        End If
    End Sub
    Private Sub Reset_Find_Emp()
        InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
        'txtEmpCode.Text = ""
        txtEmpName.Text = ""
        txtOrg.Text = ""
        txtTitle.Text = ""
        ' đẩy dữ liệu vào hider
        hidEmpID.Value = Nothing
        hidOrgID.Value = Nothing
        hidTitleID.Value = Nothing
        hidJoinDate.Value = Nothing
    End Sub
    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlFindPopup
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
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra validate cho datepicker ngày hieu luc (EFFECT_DATE)
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalEffedate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalEffedate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_SIGNDEFAULTDTO
        Try
            _validate.ID = hidID.Value
            _validate.EMPLOYEE_ID = hidEmpID.Value
            _validate.EFFECT_DATE_FROM = rdFromDate.SelectedDate
            _validate.EFFECT_DATE_TO = rdToDate.SelectedDate
            'args.IsValid = rep.ValidateAT_SIGNDEFAULT(_validate)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox CA MẶC ĐỊNH có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalSign_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSign.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            Dim shift2 As String = (From p In ListShifts Where p.ID = cboSign.SelectedValue Select p.CODE).FirstOrDefault
            If cboSignSun.SelectedValue <> "" Then
                Dim shift1 As String = (From p In ListShifts Where p.ID = cboSignSun.SelectedValue Select p.CODE).FirstOrDefault
                If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                    If Not store.GET_TIME_IN_OUT(cboSignSun.SelectedValue, cboSign.SelectedValue) Then
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalSingTue_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSingTue.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignTue.SelectedValue Select p.CODE).FirstOrDefault
            If cboSign.SelectedValue <> "" Then
                Dim shift1 As String = (From p In ListShifts Where p.ID = cboSign.SelectedValue Select p.CODE).FirstOrDefault
                If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                    If Not store.GET_TIME_IN_OUT(cboSign.SelectedValue, cboSignTue.SelectedValue) Then
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEmpCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmpCode.TextChanged
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
                        GetValue_Find_Emp(EmployeeList)
                        isLoadPopup = 0
                    ElseIf Count > 1 And rgWorkschedule.SelectedItems.Count = 0 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            'ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmpCode.Text
                            'ctrlFindEmployeePopup.MultiSelect = False
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    CurrentState = CommonMessage.STATE_NEW
                    hidID.Value = 0
                    cboSign.SelectedIndex = 0
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, rdToDate, cboSign, cboSignSat, cboSignSun, cboSignTue, cboSignThu, cboSignWed, cboSignFri)
                    UpdateControlState()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Private Sub cvalSignWed_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSignWed.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignWed.SelectedValue Select p.CODE).FirstOrDefault
            If cboSignTue.SelectedValue <> "" Then
                Dim shift1 As String = (From p In ListShifts Where p.ID = cboSignTue.SelectedValue Select p.CODE).FirstOrDefault
                If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                    'If Not store.GET_TIME_IN_OUT(shift1, shift2) Then
                    If Not store.GET_TIME_IN_OUT(cboSignTue.SelectedValue, cboSignWed.SelectedValue) Then
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalSignThu_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSignThu.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignThu.SelectedValue Select p.CODE).FirstOrDefault
            If cboSignWed.SelectedValue <> "" Then
                Dim shift1 As String = (From p In ListShifts Where p.ID = cboSignWed.SelectedValue Select p.CODE).FirstOrDefault
                If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                    If Not store.GET_TIME_IN_OUT(cboSignWed.SelectedValue, cboSignThu.SelectedValue) Then
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalSignFri_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSignFri.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignFri.SelectedValue Select p.CODE).FirstOrDefault
            If cboSignThu.SelectedValue <> "" Then
                Dim shift1 As String = (From p In ListShifts Where p.ID = cboSignThu.SelectedValue Select p.CODE).FirstOrDefault
                If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                    If Not store.GET_TIME_IN_OUT(cboSignThu.SelectedValue, cboSignFri.SelectedValue) Then
                        args.IsValid = False
                    Else
                        args.IsValid = True
                    End If
                Else
                    args.IsValid = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalSignSat_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSignSat.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            If cboSignSat.SelectedValue <> "" Then
                Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignSat.SelectedValue Select p.CODE).FirstOrDefault
                If cboSignFri.SelectedValue <> "" Then
                    Dim shift1 As String = (From p In ListShifts Where p.ID = cboSignFri.SelectedValue Select p.CODE).FirstOrDefault
                    If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                        If Not store.GET_TIME_IN_OUT(cboSignFri.SelectedValue, cboSignSat.SelectedValue) Then
                            args.IsValid = False
                        Else
                            args.IsValid = True
                        End If
                    Else
                        args.IsValid = True
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalSignSun_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSignSun.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            'ValueSign = cboSign.SelectedValue
            'ListComboData = New ComboBoxDataDTO
            'Dim dto As New AT_SHIFTDTO
            'Dim list As New List(Of AT_SHIFTDTO)
            'dto.ID = Convert.ToDecimal(cboSign.SelectedValue)
            'list.Add(dto)
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.LIST_LIST_SHIFT = list
            'args.IsValid = rep.ValidateCombobox(ListComboData)
            'If rep.ValidateCombobox(ListComboData) Then
            '    args.IsValid = True
            'Else
            '    args.IsValid = False
            '    cboSign.ClearSelection()
            '    rep.GetComboboxData(ListComboData)
            '    FillDropDownList(cboSign, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSign.SelectedValue)
            '    cboSign.SelectedIndex = 0
            'End If
            If cboSignSun.SelectedValue <> "" Then
                Dim shift2 As String = (From p In ListShifts Where p.ID = cboSignSun.SelectedValue Select p.CODE).FirstOrDefault
                If cboSign.SelectedValue <> "" Then
                    Dim shift1 As String = (From p In ListShifts Where p.ID = cboSign.SelectedValue Select p.CODE).FirstOrDefault
                    If shift1 <> "OFF" AndAlso shift1 <> "L" AndAlso shift2 <> "OFF" AndAlso shift2 <> "L" Then
                        If Not store.GET_TIME_IN_OUT(cboSignSat.SelectedValue, cboSignSun.SelectedValue) Then
                            args.IsValid = False
                        Else
                            args.IsValid = True
                        End If
                    Else
                        args.IsValid = True
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdFromDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdFromDate.SelectedDateChanged
        If rdFromDate.SelectedDate IsNot Nothing And hidJoinDate.Value <> "" Then
            If rdFromDate.SelectedDate < hidJoinDate.Value Then

                ClearControlValue(rdFromDate)
                ShowMessage("Ngày hiệu lực phải lớn hơn ngày vào công ty", NotifyType.Warning)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub rgWorkschedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorkschedule.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(If(datarow.GetDataKeyValue("ORG_DESC") IsNot Nothing, datarow.GetDataKeyValue("ORG_DESC").ToString, ""))
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class