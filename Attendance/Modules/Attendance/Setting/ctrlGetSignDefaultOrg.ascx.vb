Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGetSignDefaultOrg
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
    Property InsertSetUp As List(Of AT_SIGNDEFAULT_ORGDTO)
        Get
            Return ViewState(Me.ID & "_InsertSetUp")
        End Get
        Set(ByVal value As List(Of AT_SIGNDEFAULT_ORGDTO))
            ViewState(Me.ID & "_InsertSetUp") = value
        End Set
    End Property

    ''' <summary>
    ''' AT_SignDF
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_SignDF As List(Of AT_SIGNDEFAULT_ORGDTO)
        Get
            Return ViewState(Me.ID & "_SignDF")
        End Get
        Set(ByVal value As List(Of AT_SIGNDEFAULT_ORGDTO))
            ViewState(Me.ID & "_SignDF") = value
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

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
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
            SetGridFilter(rgSignDefaultOrg)
            rgSignDefaultOrg.AllowCustomPaging = True

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
            If Not IsPostBack Then
                GirdConfig(rgSignDefaultOrg)
            End If
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
                                       ToolbarItem.Export,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive)

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
                        rgSignDefaultOrg.Rebind()
                        SelectedItemDataGridByKey(rgSignDefaultOrg, IDSelect, , rgSignDefaultOrg.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSignDefaultOrg.CurrentPageIndex = 0
                        rgSignDefaultOrg.MasterTableView.SortExpressions.Clear()
                        rgSignDefaultOrg.Rebind()
                        SelectedItemDataGridByKey(rgSignDefaultOrg, IDSelect, )
                    Case "Cancel"
                        rgSignDefaultOrg.MasterTableView.ClearSelectedItems()
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
        Dim obj As New AT_SIGNDEFAULT_ORGDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgSignDefaultOrg, obj)
            Dim Sorts As String = rgSignDefaultOrg.MasterTableView.SortExpressions.GetSortString()


            'If ctrlOrg.CurrentValue IsNot Nothing Then
            '    obj.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            'Else
            '    rgSignDefaultOrg.DataSource = New List(Of AT_SIGNDEFAULT_ORGDTO)
            '    '_filter.param.ORG_ID = psp.GET_ID_ORG()
            '    Exit Function
            'End If

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_SignDF = rep.GetAT_SIGNDEFAULT_ORG(obj, rgSignDefaultOrg.CurrentPageIndex, rgSignDefaultOrg.PageSize, MaximumRows, Sorts)
                Else
                    Me.AT_SignDF = rep.GetAT_SIGNDEFAULT_ORG(obj, rgSignDefaultOrg.CurrentPageIndex, rgSignDefaultOrg.PageSize, MaximumRows)
                End If
                rgSignDefaultOrg.VirtualItemCount = MaximumRows
                rgSignDefaultOrg.DataSource = Me.AT_SignDF
            Else
                Return rep.GetAT_SIGNDEFAULT_ORG(obj).ToTable
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
                    ctrlOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                        ctrlOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                    End If
                    ctrlOrgPopup.IS_HadLoad = False
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
                Case 3
                    phFindOrg.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                        ctrlOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                    End If
                    ctrlOrgPopup.IS_HadLoad = False
                    phFindOrg.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, btnFindOrg)
                    ExcuteScript("Clear", "clRadDatePicker()")
                    EnabledGridNotPostback(rgSignDefaultOrg, False)
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                    EnableControlAll(False, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, btnFindOrg)
                    EnabledGridNotPostback(rgSignDefaultOrg, True)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, btnFindOrg)
                    EnabledGridNotPostback(rgSignDefaultOrg, False)
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgSignDefaultOrg.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgSignDefaultOrg.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT_ORG(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgSignDefaultOrg.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgSignDefaultOrg.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgSignDefaultOrg.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT_ORG(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgSignDefaultOrg.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgSignDefaultOrg.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgSignDefaultOrg.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_SIGNDEFAULT_ORG(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        rgSignDefaultOrg.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

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
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("NOTE", txtNote)
            dic.Add("SIGN_MON", cboSign)
            dic.Add("SIGN_TUE", cboSignTue)
            dic.Add("SIGN_WED", cboSignWed)
            dic.Add("SIGN_THU", cboSignThu)
            dic.Add("SIGN_FRI", cboSignFri)
            dic.Add("SIGN_SAT", cboSignSat)
            dic.Add("SIGN_SUN", cboSignSun)
            dic.Add("ID", hidID)
            dic.Add("EFFECT_DATE_FROM", rdFromDate)
            dic.Add("EFFECT_DATE_TO", rdToDate)
            dic.Add("OBJ_ATTENDANT_ID", cboObject)
            dic.Add("OBJ_EMPLOYEE_ID", rcOBJECT_EMPLOYEE)
            dic.Add("WORKPLACE_ID", cbWorkPlace)
            dic.Add("YEAR", cbYear)
            dic.Add("CALENDAR", txtCalendar)
            Utilities.OnClientRowSelectedChanged(rgSignDefaultOrg, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ' ''' <lastupdate>16/08/2017</lastupdate>
    ' ''' <summary>
    ' ''' Ham xu ly cac event click button chon nhan vien
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Protected Sub btnChooseEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseEmployee.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        Select Case sender.ID
    '            Case btnChooseEmployee.ID
    '                isLoadPopup = 1
    '        End Select

    '        UpdateControlState()

    '        Select Case sender.ID
    '            Case btnChooseEmployee.ID
    '                ctrlFindEmployeePopup.IsHideTerminate = False
    '                ctrlFindEmployeePopup.Show()
    '        End Select

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

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
        Dim objSIGN As New AT_SIGNDEFAULT_ORGDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim store As New AttendanceStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    hidID.Value = 0
                    cboSign.SelectedIndex = 0
                    ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgSignDefaultOrg.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgSignDefaultOrg.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgSignDefaultOrg.SelectedItems(0)
                    hidID.Value = CDec(item.GetDataKeyValue("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    'isValidate = True
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgSignDefaultOrg.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgSignDefaultOrg.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgSignDefaultOrg.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If cval_ServerValidate() = False Then
                        ShowMessage(Translate("Dữ liệu đăng ký bị trùng"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    If Page.IsValid Then
                        If IsDate(rdFromDate.SelectedDate) Then
                            objSIGN.EFFECT_DATE_FROM = rdFromDate.SelectedDate
                        End If
                        If IsDate(rdToDate.SelectedDate) Then
                            objSIGN.EFFECT_DATE_TO = rdToDate.SelectedDate
                        Else
                            objSIGN.EFFECT_DATE_TO = Nothing
                        End If
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
                        If IsNumeric(cboSign.SelectedValue) Then
                            objSIGN.SIGN_MON = cboSign.SelectedValue
                            cvalSign_ServerValidate(cboSign, "Thứ 2")
                        End If
                        If IsNumeric(cboSignTue.SelectedValue) Then
                            objSIGN.SIGN_TUE = cboSignTue.SelectedValue
                            cvalSign_ServerValidate(cboSignTue, "Thứ 3")
                        End If
                        If IsNumeric(cboSignWed.SelectedValue) Then
                            objSIGN.SIGN_WED = cboSignWed.SelectedValue
                            cvalSign_ServerValidate(cboSignWed, "Thứ 4")
                        End If
                        If IsNumeric(cboSignThu.SelectedValue) Then
                            objSIGN.SIGN_THU = cboSignThu.SelectedValue
                            cvalSign_ServerValidate(cboSignThu, "Thứ 5")
                        End If
                        If IsNumeric(cboSignFri.SelectedValue) Then
                            objSIGN.SIGN_FRI = cboSignFri.SelectedValue
                            cvalSign_ServerValidate(cboSignFri, "Thứ 6")
                        End If
                        If IsNumeric(cboSignSat.SelectedValue) Then
                            objSIGN.SIGN_SAT = cboSignSat.SelectedValue
                            cvalSign_ServerValidate(cboSignSat, "Thứ 7")
                        End If
                        If IsNumeric(cboSignSun.SelectedValue) Then
                            objSIGN.SIGN_SUN = cboSignSun.SelectedValue
                            cvalSign_ServerValidate(cboSignSun, "Chủ nhật")
                        End If
                        objSIGN.NOTE = txtNote.Text.Trim
                        If IsNumeric(hidOrgID.Value) AndAlso hidOrgID.Value <> 0 Then
                            objSIGN.ORG_ID = hidOrgID.Value
                        Else
                            objSIGN.ORG_ID = Nothing
                        End If
                        If IsNumeric(cboObject.SelectedValue) Then
                            objSIGN.OBJ_ATTENDANT_ID = cboObject.SelectedValue
                        Else
                            objSIGN.OBJ_ATTENDANT_ID = Nothing
                        End If
                        If IsNumeric(rcOBJECT_EMPLOYEE.SelectedValue) Then
                            objSIGN.OBJ_EMPLOYEE_ID = rcOBJECT_EMPLOYEE.SelectedValue
                        Else
                            objSIGN.OBJ_EMPLOYEE_ID = Nothing
                        End If
                        If IsNumeric(cbWorkPlace.SelectedValue) Then
                            objSIGN.WORKPLACE_ID = cbWorkPlace.SelectedValue
                        Else
                            objSIGN.WORKPLACE_ID = Nothing
                        End If
                        If cbYear.SelectedValue <> "" Then
                            objSIGN.YEAR = cbYear.SelectedValue
                        End If
                        objSIGN.CALENDAR = txtCalendar.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objSIGN.ACTFLG = "A"
                                If rep.InsertAT_SIGNDEFAULT_ORG(objSIGN, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                                    rgSignDefaultOrg.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(hidID.Value))

                                If cmRep.CheckExistIDTable(lstID, "AT_SIGNDEFAULT_ORG", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If
                                objSIGN.ID = hidID.Value
                                'objSIGN.ID = rgSignDefaultOrg.SelectedValue
                                If rep.ModifyAT_SIGNDEFAULT_ORG(objSIGN, rgSignDefaultOrg.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objSIGN.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(txtOrgName, rcOBJECT_EMPLOYEE, cboObject, cbWorkPlace, cbYear, txtCalendar, rdFromDate, rdToDate, txtNote, cboSign, cboSignTue, cboSignThu, cboSignWed, cboSignFri, cboSignSat, cboSignSun, hidOrgID, hidID)
                                    rgSignDefaultOrg.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeRadGrid()")
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
                            rgSignDefaultOrg.ExportExcel(Server, Response, dtDatas, "CaMacDinh")
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
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgSignDefaultOrg.NeedDataSource
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

    ' ''' <lastupdate>11/07/2017</lastupdate>
    ' ''' <summary>Load datasource cho grid</summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        'CreateDataFilter()
    '        'Refresh()
    '        'rgSignDefaultOrg.CurrentPageIndex = 0
    '        'rgSignDefaultOrg.MasterTableView.SortExpressions.Clear()
    '        rgSignDefaultOrg.Rebind()

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ' ''' <lastupdate>16/08/2017</lastupdate>
    ' ''' <summary>
    ' ''' Event xu ly load tat ca thong tin ve to chức khi click button [chọn] o popup ctrlOrgPopup
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
    '    Try
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_SingDefault&orgid= " & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
    '        isLoadPopup = 0
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim fileName As String
    '    Dim dsDataPrepare As New DataSet
    '    Dim rep As New AttendanceRepository
    '    Dim workbook As Aspose.Cells.Workbook
    '    Dim worksheet As Aspose.Cells.Worksheet
    '    Try
    '        If ctrlUpload.UploadedFiles.Count = 0 Then
    '            ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
    '        Dim savepath = Context.Server.MapPath(tempPath)

    '        For Each file As UploadedFile In ctrlUpload.UploadedFiles
    '            fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
    '            file.SaveAs(fileName, True)
    '            workbook = New Aspose.Cells.Workbook(fileName)
    '            If workbook.Worksheets.GetSheetByCodeName("DataImportSignDefault") Is Nothing Then
    '                ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
    '                Exit Sub
    '            End If
    '            worksheet = workbook.Worksheets(0)
    '            dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
    '            If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
    '        Next

    '        dtData = dtData.Clone()
    '        dsDataComper = dsDataPrepare.Tables(0).Clone()

    '        For Each dt As DataTable In dsDataPrepare.Tables
    '            For Each row In dt.Rows
    '                Dim isRow = ImportValidate.TrimRow(row)
    '                If Not isRow Then
    '                    Continue For
    '                End If
    '                dtData.ImportRow(row)
    '                dsDataComper.ImportRow(row)
    '            Next
    '        Next

    '        If loadToGrid() Then
    '            Dim objSIGN As AT_SIGNDEFAULT_ORGDTO
    '            Dim gID As Decimal
    '            Dim dtDataImp As DataTable = dsDataPrepare.Tables(0)

    '            For Each dr In dsDataComper.Rows
    '                objSIGN = New AT_SIGNDEFAULT_ORGDTO
    '                objSIGN.EFFECT_DATE_FROM = ToDate(dr("EFFECT_DATE_FROM"))
    '                objSIGN.EFFECT_DATE_TO = ToDate(dr("EFFECT_DATE_TO"))
    '                objSIGN.SINGDEFAULE = CInt(dr("SHIFT_ID"))
    '                objSIGN.SING_SAT = CInt(dr("SHIFT_SAT_ID"))
    '                objSIGN.SING_SUN = CInt(dr("SHIFT_SUN_ID"))
    '                objSIGN.EMPLOYEE_ID = CInt(dr("EMPLOYEE_ID"))
    '                objSIGN.SIGN_FRI = CInt(dr("SIGN_FRI_ID"))
    '                objSIGN.SIGN_THU = CInt(dr("SIGN_THU_ID"))
    '                objSIGN.SIGN_WED = CInt(dr("SIGN_WED_ID"))
    '                objSIGN.SIGN_TUE = CInt(dr("SIGN_TUE_ID"))
    '                objSIGN.ACTFLG = "A"
    '                objSIGN.NOTE = dr("NOTE")
    '                rep.InsertAT_SIGNDEFAULT(objSIGN, gID)
    '            Next

    '            CurrentState = CommonMessage.STATE_NORMAL
    '            Refresh("InsertView")
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra trươc khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Function loadToGrid() As Boolean
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim dtError As New DataTable("ERROR")
    '    Dim store As New AttendanceStoreProcedure
    '    Try
    '        If dtData.Rows.Count = 0 Then
    '            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
    '            Return False
    '        End If

    '        Dim rowError As DataRow
    '        Dim isError As Boolean = False
    '        Dim sError As String = String.Empty
    '        Dim dtEmpID As DataTable
    '        Dim is_Validate As Boolean
    '        Dim _validate As New AT_SIGNDEFAULT_ORGDTO
    '        Dim rep As New AttendanceRepository
    '        Dim lstEmp As New List(Of String)
    '        Dim shift_1 As String = ""
    '        Dim shift_2 As String = ""
    '        dtDataImportEmployee = dtData.Clone
    '        dtError = dtData.Clone
    '        Dim irow = 5
    '        Dim irowEm = 5

    '        For Each row As DataRow In dtData.Rows
    '            rowError = dtError.NewRow
    '            sError = "Chưa nhập mã nhân viên"
    '            ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
    '            sError = "Ca mặc định"
    '            ImportValidate.IsValidList("SHIFT_NAME", "SHIFT_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SHIFT_SAT_NAME", "SHIFT_SAT_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SHIFT_SUN_NAME", "SHIFT_SUN_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SIGN_FRI_NAME", "SIGN_FRI_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SIGN_THU_NAME", "SIGN_THU_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SIGN_WED_NAME", "SIGN_WED_ID", row, rowError, isError, sError)
    '            ImportValidate.IsValidList("SIGN_TUE_NAME", "SIGN_TUE_ID", row, rowError, isError, sError)
    '            sError = "Ngày hiệu lực không được để trống"
    '            ImportValidate.IsValidDate("EFFECT_DATE_FROM", row, rowError, isError, sError)
    '            sError = "Ngày hết hiệu lực không được để trống"
    '            ImportValidate.IsValidDate("EFFECT_DATE_TO", row, rowError, isError, sError)

    '            If IsNumeric(row("SHIFT_ID")) AndAlso IsNumeric(row("SHIFT_SUN_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SUN_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_SUN_ID"), row("SHIFT_ID")) Then
    '                    rowError("SHIFT_ID") = "Giờ ra ngày ca Chủ nhật và giờ vào ca Thứ 2 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SIGN_TUE_ID")) AndAlso IsNumeric(row("SHIFT_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_TUE_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_ID"), row("SIGN_TUE_ID")) Then
    '                    rowError("SIGN_TUE_ID") = "Giờ ra ngày ca Thứ 2 và giờ vào ca Thứ 3 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SIGN_WED_ID")) AndAlso IsNumeric(row("SIGN_TUE_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_TUE_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_WED_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_TUE_ID"), row("SIGN_WED_ID")) Then
    '                    rowError("SIGN_WED_ID") = "Giờ ra ngày ca Thứ 3 và giờ vào ca Thứ 4 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SIGN_THU_ID")) AndAlso IsNumeric(row("SIGN_WED_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_WED_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_THU_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_WED_ID"), row("SIGN_THU_ID")) Then
    '                    rowError("SIGN_THU_ID") = "Giờ ra ngày ca Thứ 4 và giờ vào ca Thứ 5 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SIGN_FRI_ID")) AndAlso IsNumeric(row("SIGN_THU_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_THU_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SIGN_FRI_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_THU_ID"), row("SIGN_FRI_ID")) Then
    '                    rowError("SIGN_FRI_ID") = "Giờ ra ngày ca Thứ 5 và giờ vào ca Thứ 6 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SHIFT_SAT_ID")) AndAlso IsNumeric(row("SIGN_FRI_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SIGN_FRI_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SAT_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SIGN_FRI_ID"), row("SHIFT_SAT_ID")) Then
    '                    rowError("SHIFT_SAT_ID") = "Giờ ra ngày ca Thứ 6 và giờ vào ca Thứ 7 nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If IsNumeric(row("SHIFT_SUN_ID")) AndAlso IsNumeric(row("SHIFT_SAT_ID")) Then
    '                shift_1 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SAT_ID")) Select p.CODE).FirstOrDefault
    '                shift_2 = (From p In ListShifts Where p.ID = CDec(row("SHIFT_SUN_ID")) Select p.CODE).FirstOrDefault
    '                If shift_1 <> "OFF" AndAlso shift_1 <> "L" AndAlso shift_2 <> "OFF" AndAlso shift_2 <> "L" AndAlso Not store.GET_TIME_IN_OUT(row("SHIFT_SAT_ID"), row("SHIFT_SUN_ID")) Then
    '                    rowError("SHIFT_SUN_ID") = "Giờ ra ngày ca Thứ 7 và giờ vào ca Chủ nhật nhỏ hơn 12 tiếng "
    '                End If
    '            End If

    '            If rowError("EFFECT_DATE_FROM").ToString = "" And _
    '                rowError("EFFECT_DATE_TO").ToString = "" And _
    '                 row("EFFECT_DATE_FROM").ToString <> "" And _
    '                row("EFFECT_DATE_TO").ToString <> "" Then
    '                Dim startdate = Date.Parse(row("EFFECT_DATE_FROM"))
    '                Dim enddate = Date.Parse(row("EFFECT_DATE_TO"))
    '                If startdate > enddate Then
    '                    rowError("EFFECT_DATE_FROM") = "Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực"
    '                    isError = True
    '                End If
    '            End If


    '            If isError Then
    '                rowError("ID") = irow
    '                If rowError("EMPLOYEE_CODE").ToString = "" Then
    '                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
    '                End If
    '                rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
    '                rowError("ORG_NAME") = row("ORG_NAME").ToString
    '                rowError("ORG_PATH") = row("ORG_PATH").ToString
    '                rowError("TITLE_NAME") = row("TITLE_NAME").ToString
    '                dtError.Rows.Add(rowError)
    '            Else
    '                dtDataImportEmployee.ImportRow(row)
    '            End If
    '            irow = irow + 1
    '            isError = False
    '        Next

    '        If dtError.Rows.Count > 0 Then
    '            dtError.TableName = "DATA"
    '            Session("EXPORTREPORT") = dtError
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
    '        End If

    '        If isError Then
    '            Return False
    '        Else
    '            ' check nv them vao file import có nằm trong hệ thống không.
    '            For j As Integer = 0 To dsDataComper.Rows.Count - 1
    '                rowError = dtError.NewRow
    '                If dsDataComper(j)("EMPLOYEE_ID") = "" Then
    '                    dtEmpID = New DataTable
    '                    dtEmpID = rep.GetEmployeeIDInSign(dsDataComper(j)("EMPLOYEE_CODE"))

    '                    If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
    '                        rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dsDataComper(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
    '                        isError = True
    '                    Else
    '                        dsDataComper(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("ID")
    '                    End If
    '                End If
    '                ' check ngày hiệu lực bị trùng.
    '                If dsDataComper(j)("EMPLOYEE_ID") <> "" Then
    '                    _validate.ID = 0
    '                    _validate.EMPLOYEE_ID = CDec(dsDataComper(j)("EMPLOYEE_ID"))
    '                    _validate.EFFECT_DATE_FROM = ToDate(dsDataComper(j)("EFFECT_DATE_FROM"))
    '                    _validate.EFFECT_DATE_TO = ToDate(dsDataComper(j)("EFFECT_DATE_TO"))
    '                    is_Validate = rep.ValidateAT_SIGNDEFAULT(_validate)
    '                    If Not rep.ValidateAT_SIGNDEFAULT(_validate) Then
    '                        rowError("EFFECT_DATE_FROM") = "Khoảng thời gian hiệu lực bị trùng."
    '                        isError = True
    '                    End If
    '                End If
    '                If isError Then
    '                    rowError("ID") = irowEm
    '                    dtError.Rows.Add(rowError)
    '                End If
    '                irowEm = irowEm + 1
    '                isError = False
    '            Next

    '            If dtError.Rows.Count > 0 Then
    '                dtError.TableName = "DATA"
    '                Session("EXPORTREPORT") = dtError
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SingDefault_Error')", True)
    '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
    '            Else
    '                Return True
    '            End If
    '        End If

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Function

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
        Dim listWorkPlace As New List(Of WorkPlaceDTO)
        Dim dtData As New DataTable
        Dim _filter As New WorkPlaceDTO
        Try
            Dim table1 As New DataTable
            table1.Columns.Add("YEAR", GetType(Integer))
            table1.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table1.NewRow
                row("ID") = index
                row("YEAR") = index
                table1.Rows.Add(row)
            Next
            Dim row2 As DataRow = table1.NewRow
            row2("ID") = DBNull.Value
            row2("YEAR") = DBNull.Value
            table1.Rows.Add(row2)
            FillRadCombobox(cbYear, table1, "YEAR", "ID")
            cbYear.SelectedValue = Date.Now.Year
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
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
            Using repProfile As New ProfileRepository
                _filter.ACTFLG_Search = "A"
                listWorkPlace = repProfile.GetWorkPlace(_filter)
                FillDropDownList(cbWorkPlace, listWorkPlace, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbWorkPlace.SelectedValue)

                Dim table As New DataTable
                dtData = repProfile.GetOtherList("OBJECT_EMPLOYEE", True)
                FillRadCombobox(rcOBJECT_EMPLOYEE, dtData, "NAME", "ID")
                dtData = repProfile.GetOtherList("OBJECT_ATTENDANT", True)
                FillRadCombobox(cboObject, dtData, "NAME", "ID")
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
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
    Private Function cval_ServerValidate() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_SIGNDEFAULT_ORGDTO
        Try
            If IsNumeric(hidID.Value) Then
                _validate.ID = hidID.Value
            End If
            If IsDate(rdToDate.SelectedDate) Then
                _validate.EFFECT_DATE_TO = rdToDate.SelectedDate
            End If
            If IsDate(rdFromDate.SelectedDate) Then
                _validate.EFFECT_DATE_FROM = rdFromDate.SelectedDate
            End If
            If IsNumeric(hidOrgID.Value) AndAlso hidOrgID.Value <> 0 Then
                _validate.ORG_ID = hidOrgID.Value
            End If
            If IsNumeric(cbWorkPlace.SelectedValue) Then
                _validate.WORKPLACE_ID = cbWorkPlace.SelectedValue
            End If
            If IsNumeric(cboObject.SelectedValue) Then
                _validate.OBJ_ATTENDANT_ID = cboObject.SelectedValue
            End If
            If IsNumeric(rcOBJECT_EMPLOYEE.SelectedValue) Then
                _validate.OBJ_EMPLOYEE_ID = rcOBJECT_EMPLOYEE.SelectedValue
            End If
            Return rep.ValidateAT_SIGNDEFAULT_ORG(_validate)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox CA MẶC ĐỊNH có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalSign_ServerValidate(ByVal signCombobox As RadComboBox, ByVal signName As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Try
            ValueSign = signCombobox.SelectedValue
            ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
            Dim dto As New AT_SHIFTDTO
            Dim list As New List(Of AT_SHIFTDTO)
            dto.ID = Convert.ToDecimal(signCombobox.SelectedValue)
            list.Add(dto)
            ListComboData.GET_LIST_SHIFT = True
            ListComboData.LIST_LIST_SHIFT = list
            If Not rep.ValidateCombobox(ListComboData) Then
                ShowMessage(Translate("Ký hiệu ca " + signName + " đã khóa hoặc không tồn tại"), NotifyType.Success)
                signCombobox.ClearSelection()
                rep.GetComboboxData(ListComboData)
                FillDropDownList(signCombobox, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, signCombobox.SelectedValue)
                signCombobox.SelectedIndex = 0
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>15/12/2020</lastupdate>
    ''' <summary> Event click button tim kiem don vi </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event Ok popup List don vi </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim orgItem = ctrlOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
                    ClearControlValue(hidOrgID, txtOrgName)
                ElseIf orgList.Count = 1 Then
                    hidOrgID.Value = orgList(0).ID
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

    Private Sub rgSignDefaultOrg_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgSignDefaultOrg.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class