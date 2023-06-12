Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class crtlATTimeWorkStandard
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrlOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Protected WithEvents WSCopy As ctrlTimeWorkStandardCopy
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = True

    ''' <summary>
    ''' list AT_Terminal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_Terminal As List(Of AT_TIME_WORKSTANDARDDTO)
        Get
            Return ViewState(Me.ID & "_Termidal")
        End Get
        Set(ByVal value As List(Of AT_TIME_WORKSTANDARDDTO))
            ViewState(Me.ID & "_Termidal") = value
        End Set
    End Property

    Public Property dtCal As DataTable
        Get
            If ViewState(Me.ID & "_dtCal") Is Nothing Then
                ViewState(Me.ID & "_dtCal") = New DataTable
            End If
            Return ViewState(Me.ID & "_dtCal")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtCal") = value
        End Set
    End Property
    Private Property strID As String
        Get
            Return ViewState(Me.ID & "_strID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strID") = value
        End Set
    End Property
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
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True

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
            If Not IsPostBack Then
                GirdConfig(rgData)
            End If
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
                                     ToolbarItem.Save, ToolbarItem.Cancel,
                                     ToolbarItem.Delete, ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_SYNC,
                                                          ToolbarIcons.Sync,
                                                          ToolbarAuthorize.None,
                                                        Translate("Sao chép")))
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
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
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
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_WORKSTANDARDDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_Terminal = rep.GetAT_Time_WorkStandard(obj, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_Terminal = rep.GetAT_Time_WorkStandard(obj, _param, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = Me.AT_Terminal
            Else
                Return rep.GetAT_Time_WorkStandard(obj, _param).ToTable
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
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
            If Me.Controls.Contains(WSCopy) Then
                Me.Controls.Remove(WSCopy)
                'Me.Views.Remove(groupCopy.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 3
                    phFindOrg.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrg.Controls.Add(ctrlOrgPopup)
                    Exit Sub
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                        Exit Sub
                    End If
                Case 4
                    'Dim lst As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lst.Add(item.GetDataKeyValue("ID"))
                    'Next
                    strID = ""
                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        strID = strID & dr.GetDataKeyValue("ID") & ","
                    Next
                    WSCopy = Me.Register("ctrlTimeWorkStandardCopy", "Common", "ctrlTimeWorkStandardCopy", "Secure")
                    'WSCopy.LSTIDOLD = lst
                    WSCopy.BindData()
                    Me.Controls.Add(WSCopy)
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtYear, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, txtCMD, btnCal, btnFindOrg, rdConversionFator)
                    ClearControlValue(txtYear, txtOrgName, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, txtCMD, hidEmpID, rdConversionFator)
                    EnabledGridNotPostback(rgData, False)

                    dtCal = rep.GET_AT_TIMEWORK(0)
                    rgCal.Rebind()
                    EnabledGrid(rgCal, True)
                    rdConversionFator.Value = 1

                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtYear, txtOrgName, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, txtCMD, hidEmpID, rdConversionFator)
                    EnableControlAll(False, txtYear, btnCal, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, txtCMD, btnFindOrg, hidEmpID, rdConversionFator)
                    EnabledGridNotPostback(rgData, True)

                    dtCal = rep.GET_AT_TIMEWORK(0)
                    rgCal.Rebind()
                    EnabledGrid(rgCal, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtYear, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, txtCMD, btnFindOrg, rdConversionFator)
                    EnableControlAll(True, btnCal)
                    EnabledGridNotPostback(rgData, False)
                    EnabledGrid(rgCal, True)

                    'If chkEmployee.Checked Then
                    '    EnableControlAll(True, btnEmployee, txtEmployeeCode)
                    '    EnableControlAll(False, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, btnFindOrg, rdConversionFator)
                    'Else
                    '    EnableControlAll(True, txtYear, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, btnFindOrg, rdConversionFator)
                    '    EnableControlAll(False, btnEmployee, txtEmployeeCode)
                    'End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteATTime_WorkStandard(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
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
    'Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event click button tim kiem don vi </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnCal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCal.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try

            If IsNumeric(rdConversionFator.Value) = False Then
                ShowMessage(Translate("Bạn phải nhập Hệ số quy đổi."), NotifyType.Warning)
                Exit Sub
            End If

            'If chkEmployee.Checked Then
            '    If txtCMD.Text = "" Then
            '        ShowMessage(Translate("Bạn phải nhập Công mặc định."), NotifyType.Warning)
            '        Exit Sub
            '    End If
            '    dtCal = rep.GET_AT_TIMEWORK_EMPLOYEE(txtCMD.Text, If(IsNumeric(rdConversionFator.Value), rdConversionFator.Value, 0))
            '    rgCal.Rebind()
            '    Exit Sub
            'End If

            If txtYear.Text = "" Then
                ShowMessage(Translate("Bạn phải nhập Năm hiệu lực."), NotifyType.Warning)
                Exit Sub
            End If

            If cboObjEmployee.SelectedValue = "" Then
                ShowMessage(Translate("Bạn phải chọn Đối tượng nhân viên."), NotifyType.Warning)
                Exit Sub
            End If

            dtCal = rep.GET_AT_TIMEWORKSTANDARD(txtYear.Text, cboObjEmployee.SelectedValue, chkNotT7.Checked, chkNotCN.Checked, chkNotNT7.Checked, chkNot2T7.Checked, If(txtTC.Text = "", Nothing, txtTC.Text), If(txtCMD.Text = "", Nothing, txtCMD.Text), If(IsNumeric(rdConversionFator.Value), rdConversionFator.Value, 0))
            rgCal.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
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
            dic.Add("ID", hidID)
            dic.Add("YEAR", txtYear)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("OBJ_EMPLOYEE_ID", cboObjEmployee)
            dic.Add("OBJ_ATTENDANT_ID", cboObjAttendant)
            dic.Add("EMPLOYEE_ID", hidEmpID)
            'dic.Add("EMPLOYEE_CODE", txtEmployeeCode)
            'dic.Add("EMPLOYEE_NAME", txtEmployeeName)
            'dic.Add("EMP_ORG_NAME", txtOrg_Name)
            'dic.Add("TITLE_NAME", txtTITLE)
            'dic.Add("IS_EMPLOYEE", chkEmployee)
            dic.Add("CONVERSION_FACTOR", rdConversionFator)
            dic.Add("IS_NOT_CAL_SAT", chkNotT7)
            dic.Add("IS_NOT_CAL_SUN", chkNotCN)
            dic.Add("IS_NOT_CAL_HALF_SAT", chkNotNT7)
            dic.Add("IS_NOT_CAL_2_SAT", chkNot2T7)
            dic.Add("MINUS_MONTHLY_AT", txtTC)
            dic.Add("DEFAULT_AT", txtCMD)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
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
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSIGN As New AT_TIME_WORKSTANDARDDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ClearControlValue(cboObjEmployee, cboObjAttendant)
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        If IsNumeric(rdConversionFator.Value) = False Then
                            ShowMessage(Translate("Hệ số quy đổi bắt buộc nhập, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        Else
                            objSIGN.CONVERSION_FACTOR = rdConversionFator.Value
                        End If

                        'If chkEmployee.Checked AndAlso hidEmpID.Value = "" Then
                        '    ShowMessage(Translate("Mã nhân viên bắt buộc nhập, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'Else
                        '    objSIGN.EMPLOYEE_ID = If(IsNumeric(hidEmpID.Value), Decimal.Parse(hidEmpID.Value), Nothing)
                        'End If

                        objSIGN.YEAR = txtYear.Text
                        objSIGN.ORG_ID = If(IsNumeric(hidOrgID.Value), Decimal.Parse(hidOrgID.Value), 0)

                        If cboObjEmployee.SelectedValue <> "" Then
                            objSIGN.OBJ_EMPLOYEE_ID = cboObjEmployee.SelectedValue
                        End If

                        If cboObjAttendant.SelectedValue <> "" Then
                            objSIGN.OBJ_ATTENDANT_ID = cboObjAttendant.SelectedValue
                        End If

                        'If chkEmployee.Checked = False AndAlso rep.CheckATTime_WorkStandard(txtYear.Text, If(hidOrgID.Value <> "", Decimal.Parse(hidOrgID.Value), 0), cboObjEmployee.SelectedValue, If(rgData.SelectedValue Is Nothing, 0, rgData.SelectedValue), If(cboObjAttendant.SelectedValue <> "", cboObjAttendant.SelectedValue, 0)) = False Then
                        '    ShowMessage(Translate("Năm hiệu lực,Phòng ban và Đối tượng nhân viên đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'If chkEmployee.Checked AndAlso rep.CheckATTime_WorkStandardEmp(txtYear.Text, If(IsNumeric(hidEmpID.Value), Decimal.Parse(hidEmpID.Value), 0), If(rgData.SelectedValue Is Nothing, 0, rgData.SelectedValue)) = False Then
                        '    ShowMessage(Translate("Năm hiệu lực,Mã nhân viên đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        For Each row As GridDataItem In rgCal.Items
                            objSIGN.WORKSTANDARD_M1 = DirectCast(row("T1").FindControl("T1"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M2 = DirectCast(row("T2").FindControl("T2"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M3 = DirectCast(row("T3").FindControl("T3"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M4 = DirectCast(row("T4").FindControl("T4"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M5 = DirectCast(row("T5").FindControl("T5"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M6 = DirectCast(row("T6").FindControl("T6"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M7 = DirectCast(row("T7").FindControl("T7"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M8 = DirectCast(row("T8").FindControl("T8"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M9 = DirectCast(row("T9").FindControl("T9"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M10 = DirectCast(row("T10").FindControl("T10"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M11 = DirectCast(row("T11").FindControl("T11"), RadNumericTextBox).Value
                            objSIGN.WORKSTANDARD_M12 = DirectCast(row("T12").FindControl("T12"), RadNumericTextBox).Value
                        Next
                        If IsNumeric(txtCMD.Text) Then
                            objSIGN.DEFAULT_AT = CDec(txtCMD.Text)
                        End If
                        If IsNumeric(txtTC.Text) Then
                            objSIGN.MINUS_MONTHLY_AT = CDec(txtTC.Text)
                        End If
                        objSIGN.IS_NOT_CAL_HALF_SAT = chkNotNT7.Checked
                        objSIGN.IS_NOT_CAL_2_SAT = chkNot2T7.Checked
                        objSIGN.IS_NOT_CAL_SAT = chkNotT7.Checked
                        objSIGN.IS_NOT_CAL_SUN = chkNotCN.Checked
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objSIGN.IS_EDIT = 0
                                If rep.InsertATTime_WorkStandard(objSIGN, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objSIGN.ID = rgData.SelectedValue
                                objSIGN.IS_EDIT = 1

                                Dim dtdata = rep.GetAT_Time_WorkStandardID(rgData.SelectedValue)

                                If dtdata.WORKSTANDARD_M1 <> objSIGN.WORKSTANDARD_M1 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 1, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 1 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M2 <> objSIGN.WORKSTANDARD_M2 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 2, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 2 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M3 <> objSIGN.WORKSTANDARD_M3 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 3, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 3 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M4 <> objSIGN.WORKSTANDARD_M4 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 4, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 4 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M5 <> objSIGN.WORKSTANDARD_M5 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 5, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 5 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M6 <> objSIGN.WORKSTANDARD_M6 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 6, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 6 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M7 <> objSIGN.WORKSTANDARD_M7 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 7, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 7 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M8 <> objSIGN.WORKSTANDARD_M8 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 8, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 8 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M9 <> objSIGN.WORKSTANDARD_M9 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 9, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 9 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M10 <> objSIGN.WORKSTANDARD_M10 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 10, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 10 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M11 <> objSIGN.WORKSTANDARD_M11 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 11, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 11 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If dtdata.WORKSTANDARD_M12 <> objSIGN.WORKSTANDARD_M12 Then
                                    If rep.CheckCloseOrg(objSIGN.YEAR, 12, objSIGN.ORG_ID) Then
                                        ShowMessage(Translate("Tháng 1 kỳ công đã đóng nên không thể sửa công chuẩn tháng 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If

                                If rep.ModifyATTime_WorkStandard(objSIGN, rgData.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objSIGN.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
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
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "Thiết lập công chuẩn theo cơ cấu")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SYNC
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    isLoadPopup = 4
                    UpdateControlState()
                    WSCopy.Show()
            End Select
            rep.Dispose()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Try
            Dim rep As New AttendanceRepository
            Dim item = CType(rgData.SelectedItems(0), GridDataItem)
            Dim ID = item.GetDataKeyValue("ID").ToString
            dtCal = rep.GET_AT_TIMEWORK(ID)
            rgCal.Rebind()
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgISP_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCal.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                If dtCal IsNot Nothing Then
                    For Each item As DataRow In dtCal.Rows
                        Dim row As GridDataItem = DirectCast(e.Item, GridDataItem)
                        DirectCast(row("T1").FindControl("T1"), RadNumericTextBox).Text = item("T1").ToString.Replace(",", ".")
                        DirectCast(row("T2").FindControl("T2"), RadNumericTextBox).Text = item("T2").ToString.Replace(",", ".")
                        DirectCast(row("T3").FindControl("T3"), RadNumericTextBox).Text = item("T3").ToString.Replace(",", ".")
                        DirectCast(row("T4").FindControl("T4"), RadNumericTextBox).Text = item("T4").ToString.Replace(",", ".")
                        DirectCast(row("T5").FindControl("T5"), RadNumericTextBox).Text = item("T5").ToString.Replace(",", ".")
                        DirectCast(row("T6").FindControl("T6"), RadNumericTextBox).Text = item("T6").ToString.Replace(",", ".")
                        DirectCast(row("T7").FindControl("T7"), RadNumericTextBox).Text = item("T7").ToString.Replace(",", ".")
                        DirectCast(row("T8").FindControl("T8"), RadNumericTextBox).Text = item("T8").ToString.Replace(",", ".")
                        DirectCast(row("T9").FindControl("T9"), RadNumericTextBox).Text = item("T9").ToString.Replace(",", ".")
                        DirectCast(row("T10").FindControl("T10"), RadNumericTextBox).Text = item("T10").ToString.Replace(",", ".")
                        DirectCast(row("T11").FindControl("T11"), RadNumericTextBox).Text = item("T11").ToString.Replace(",", ".")
                        DirectCast(row("T12").FindControl("T12"), RadNumericTextBox).Text = item("T12").ToString.Replace(",", ".")
                    Next
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    'Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim rep As New CommonRepository
    '    Try
    '        'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
    '        If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
    '            Hid_IsEnter.Value = Nothing
    '            If txtEmployeeCode.Text <> "" Then
    '                Dim Count = 0
    '                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
    '                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
    '                _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
    '                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
    '                If Count <= 0 Then
    '                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
    '                    txtEmployeeCode.Text = ""
    '                ElseIf Count = 1 Then
    '                    Dim item = EmployeeList(0)
    '                    txtEmployeeCode.Text = item.EMPLOYEE_CODE
    '                    txtEmployeeName.Text = item.FULLNAME_VN
    '                    txtOrg_Name.Text = item.ORG_NAME
    '                    txtTITLE.Text = item.TITLE_NAME
    '                    hidEmpID.Value = item.EMPLOYEE_ID
    '                    isLoadPopup = 0

    '                ElseIf Count > 1 Then
    '                    If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
    '                        phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
    '                        'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
    '                    End If
    '                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
    '                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
    '                        ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
    '                        ctrlFindEmployeePopup.MultiSelect = False
    '                        ctrlFindEmployeePopup.MustHaveContract = False
    '                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
    '                        ctrlFindEmployeePopup.Show()
    '                        isLoadPopup = 1
    '                    End If
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles WSCopy.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub WSCopy_SelectedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles WSCopy.CopySelected
        Dim store As New AttendanceStoreProcedure
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim log As New CommonBusiness.UserLog
        log = LogHelper.GetUserLog
        Try
            Dim startTime As DateTime = DateTime.UtcNow


            If WSCopy.SelectedID IsNot Nothing AndAlso Not String.IsNullOrEmpty(strID) Then
                For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    Dim OrgID = If(IsNumeric(dr.GetDataKeyValue("ORG_ID")), Decimal.Parse(dr.GetDataKeyValue("ORG_ID")), 0)
                    Dim ObjEmployee = dr.GetDataKeyValue("ORG_ID")
                    Dim IS_emp = CBool(dr.GetDataKeyValue("IS_EMPLOYEE"))
                    Dim objAttendance = CBool(dr.GetDataKeyValue("OBJ_ATTENDANT_ID"))
                    Dim EmpID = If(IsNumeric(dr.GetDataKeyValue("EMPLOYEE_ID")), Decimal.Parse(dr.GetDataKeyValue("EMPLOYEE_ID")), 0)
                    If IS_emp = False AndAlso rep.CheckATTime_WorkStandard(WSCopy.SelectedID, OrgID, ObjEmployee, 0, objAttendance) = False Then
                        ShowMessage(Translate("Năm hiệu lực,Phòng ban và Đối tượng nhân viên đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    If IS_emp AndAlso rep.CheckATTime_WorkStandardEmp(WSCopy.SelectedID, EmpID, 0) = False Then
                        ShowMessage(Translate("Năm hiệu lực,Mã nhân viên đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                Dim status As Int32 = store.InsertATTime_WorkStandard_proc(strID, log.Username, log.Ip + log.ComputerName, CDec(WSCopy.SelectedID))
                If status = 1 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("InsertView")
                    UpdateControlState()
                Else
                    Dim MESS As String = String.Empty
                    Select Case status
                        Case 0
                            MESS = "Copy dữ liệu thất bại"

                    End Select
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) + Chr(10) + MESS _
                        , Utilities.NotifyType.Error)
                End If
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
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
            Dim dtData As DataTable
            dtData = rep.GetOtherList("OBJECT_EMPLOYEE", True)
            FillRadCombobox(cboObjEmployee, dtData, "NAME", "ID")
            dtData = rep.GetOtherList("OBJECT_ATTENDANT", True)
            FillRadCombobox(cboObjAttendant, dtData, "NAME", "ID")
            rep.Dispose()
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

    Private Sub rgISP_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCal.NeedDataSource
        Try
            Dim rep As New AttendanceRepository
            If dtCal.Rows.Count = 0 Then
                dtCal = rep.GET_AT_TIMEWORK(0)
                rgCal.VirtualItemCount = dtCal.Rows.Count
                rgCal.DataSource = dtCal
            Else
                rgCal.VirtualItemCount = dtCal.Rows.Count
                rgCal.DataSource = dtCal
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub chkEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEmployee.CheckedChanged
    '    If chkEmployee.Checked Then
    '        EnableControlAll(True, btnEmployee, txtEmployeeCode)
    '        EnableControlAll(False, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, btnFindOrg)
    '        ClearControlValue(txtOrgName, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, hidOrgID)
    '    Else
    '        EnableControlAll(True, txtYear, cboObjEmployee, cboObjAttendant, chkNotT7, chkNotCN, chkNotNT7, chkNot2T7, txtTC, btnCal, btnFindOrg)
    '        ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrg_Name, txtTITLE, hidEmpID)
    '        EnableControlAll(False, btnEmployee, txtEmployeeCode)
    '    End If

    'End Sub


    'Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
    '    Try
    '        Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
    '        lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            Dim item = lstCommonEmployee(0)
    '            txtEmployeeCode.Text = item.EMPLOYEE_CODE
    '            txtEmployeeName.Text = item.FULLNAME_VN
    '            txtOrg_Name.Text = item.ORG_NAME
    '            txtTITLE.Text = item.TITLE_NAME
    '            hidEmpID.Value = item.EMPLOYEE_ID
    '        End If
    '        isLoadPopup = 0
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = If(datarow.GetDataKeyValue("ORG_DESC") IsNot Nothing, Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString), Utilities.DrawTreeByString(""))
                datarow("EMP_ORG_NAME").ToolTip = If(datarow.GetDataKeyValue("EMP_ORG_DESC") IsNot Nothing, Utilities.DrawTreeByString(datarow.GetDataKeyValue("EMP_ORG_DESC").ToString), Utilities.DrawTreeByString(""))
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class