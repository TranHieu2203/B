Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAT_TIMESHEET_LOCK
    Inherits Common.CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Public IDSelect As Integer

    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
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
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
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
            SetGridFilter(rgData)
            rgData.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIMESHEET_LOCKDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    rgData.DataSource = rep.GetAtTimesheetLock(obj, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize, "CREATED_DATE desc")
                Else
                    rgData.DataSource = rep.GetAtTimesheetLock(obj, MaximumRows, rgData.CurrentPageIndex, rgData.PageSize)
                End If
                rgData.VirtualItemCount = MaximumRows
            Else
                Return rep.GetAtTimesheetLock(obj, Sorts).ToTable
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnableControlAll(True, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, cboPeriod, cboYear, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS, rcOBJECT_EMPLOYEE, chkIsEMP, btnFindOrg)
                    'EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(hidEmpId, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, cboPeriod, cboYear, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS, hidOrgID, rcOBJECT_EMPLOYEE, chkIsEMP)
                    'cboYear.ClearSelection()
                    cboPeriod.Items.Clear()
                    'EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, cboPeriod, cboYear, cboPeriod, cboYear, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS, rcOBJECT_EMPLOYEE, chkIsEMP, btnFindOrg)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, cboPeriod, cboYear, cboPeriod, cboYear, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS, rcOBJECT_EMPLOYEE, chkIsEMP, btnFindOrg)
                    'EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAtTimesheetLock(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                        ctrlFindEmployeePopup.IsHideTerminate = True
                    End If
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
            UpdateToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("EMPLOYEE_CODE", txtEmpCode)
            'dic.Add("EMPLOYEE_NAME", txtFullName)
            'dic.Add("ORG_NAME", txtOrgName)
            'dic.Add("TITLE_NAME", txtTitle)
            'dic.Add("NOTE", txtNote)
            'dic.Add("WAGE_OFFSET", rnWageOffset)
            'dic.Add("YEAR", cboYear)
            'dic.Add("PERIOD_ID", cboPeriod)
            'Utilities.OnClientRowSelectedChanged(rgData, dic)
            Dim table As New DataTable
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim store As New AttendanceStoreProcedure
            Dim dtDataPeriod = store.GetPeriodNameAndYear(True).Tables(1)
            FillRadCombobox(cboYear, dtDataPeriod, "YEAR", "YEAR")

            'table.Columns.Add("YEAR", GetType(Integer))
            'table.Columns.Add("ID", GetType(Integer))
            'Dim row As DataRow
            'For index = 2015 To Date.Now.Year + 1
            '    row = table.NewRow
            '    row("ID") = index
            '    row("YEAR") = index
            '    table.Rows.Add(row)
            'Next

            'FillRadCombobox(cboYear, table, "YEAR", "ID")
            'cboYear.SelectedValue = Date.Now.Year
            Using repProfile As New ProfileRepository
                Dim dtData = repProfile.GetOtherList("OBJECT_EMPLOYEE", True)
                FillRadCombobox(rcOBJECT_EMPLOYEE, dtData, "NAME", "ID")
            End Using

            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_T", "PERIOD_ID", True)
            'If lsData.Count > 0 Then
            '    'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            '    Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
            '    If periodid IsNot Nothing Then
            '        cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
            '    Else
            '        cboPeriod.SelectedIndex = 0
            '    End If
            'End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New AT_TIMESHEET_LOCKDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    ClearControlValue(hidEmpId, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS)
                    chkIsEMP.Checked = False
                    chkIsEMP_CheckedChanged(Nothing, Nothing)
                    'cboYear.ClearSelection()
                    cboPeriod.Items.Clear()
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
                    chkIsEMP_CheckedChanged(Nothing, Nothing)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Dim lstID As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstID.Add(Decimal.Parse(item("ID").Text))
                    'Next
                    'If Not rep.CheckExistInDatabase(lstID, AttendanceCommonTABLE_NAME.AT_FML) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If IsNumeric(hidEmpId.Value) Then
                            obj.EMPLOYEE_ID = hidEmpId.Value
                        End If
                        If chkIsEMP.Checked = False Then
                            obj.ORG_ID = hidOrgID.Value
                        End If
                        obj.FROM_DATE = rdFromDate.SelectedDate
                        obj.TO_DATE = rdToDate.SelectedDate
                        obj.IS_LEAVE = rcIsLeave.Checked
                        obj.IS_OVERTIME = rcIsOvertime.Checked
                        obj.IS_DMVS = rcIsDMVS.Checked
                        obj.REMARK = txtNote.Text.Trim
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'If rep.ValidateWageOffset(obj) Then
                                '    ShowMessage(Translate("Dữ liệu bù công của nhân viên đã tồn tại trong kỳ công này!"), Utilities.NotifyType.Error)
                                '    Exit Sub
                                'End If
                                If rep.InSertAtTimesheetLock(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgData.SelectedValue
                                'If rep.ValidateWageOffset(obj) Then
                                '    ShowMessage(Translate("Dữ liệu bù công của nhân viên đã tồn tại trong kỳ công này!"), Utilities.NotifyType.Error)
                                '    Exit Sub
                                'End If
                                If rep.ModifyAtTimesheetLock(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                    chkIsEMP.Checked = False
                    chkIsEMP_CheckedChanged(Nothing, Nothing)
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "BuCong")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Event chọn năm combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsNumeric(cboYear.SelectedValue) Then
                Dim dtData As List(Of AT_PERIODDTO)
                Dim rep As New AttendanceRepository
                Dim period As New AT_PERIODDTO
                period.ORG_ID = 1
                period.YEAR = Decimal.Parse(cboYear.SelectedValue)
                dtData = rep.LOAD_PERIODBylinq(period)
                cboPeriod.ClearSelection()
                FillRadCombobox(cboPeriod, dtData, "PERIOD_T", "PERIOD_ID", True)
                If dtData.Count > 0 Then
                    Dim periodid = (From d In dtData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                    If periodid IsNot Nothing Then
                        cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    Else
                        cboPeriod.SelectedIndex = 0
                    End If
                End If
            Else
                cboPeriod.DataSource = New DataTable()
                cboPeriod.DataBind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lsData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Try
            'ClearControlValue(hidEmpId, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle)
            'cboYear.ClearSelection()
            'cboPeriod.Items.Clear()
            ClearControlValue(hidEmpId, txtEmpCode, txtFullName, txtNote, txtOrgName, txtTitle, cboPeriod, cboYear, txtOrg, rdFromDate, rdToDate, rcIsLeave, rcIsOvertime, rcIsDMVS, hidOrgID, rcOBJECT_EMPLOYEE, chkIsEMP)
            chkIsEMP.Checked = False
            chkIsEMP_CheckedChanged(Nothing, Nothing)
            If rgData.SelectedItems.Count() = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)
                Dim a = item.GetDataKeyValue("ORG_ID")
                If IsNumeric(item.GetDataKeyValue("ORG_ID")) Then
                    hidOrgID.Value = item.GetDataKeyValue("ORG_ID")
                End If
                If IsNumeric(item.GetDataKeyValue("EMPLOYEE_ID")) Then
                    hidEmpId.Value = item.GetDataKeyValue("EMPLOYEE_ID")
                    chkIsEMP.Checked = True
                    chkIsEMP_CheckedChanged(Nothing, Nothing)
                    txtEmpCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE")
                    txtFullName.Text = item.GetDataKeyValue("EMPLOYEE_NAME")
                    txtOrgName.Text = item.GetDataKeyValue("ORG_NAME")
                    txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                End If

                rdFromDate.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rdToDate.SelectedDate = item.GetDataKeyValue("TO_DATE")
                rcIsLeave.Checked = item.GetDataKeyValue("IS_LEAVE")
                rcIsOvertime.Checked = item.GetDataKeyValue("IS_OVERTIME")
                rcIsDMVS.Checked = item.GetDataKeyValue("IS_DMVS")
                txtNote.Text = item.GetDataKeyValue("REMARK")
                txtOrg.Text = item.GetDataKeyValue("ORG_NAME_TIMESHEET")
                ClearControlValue(cboYear)

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
                txtOrgName.Text = item.ORG_NAME
                txtEmpCode.Text = item.EMPLOYEE_CODE
                txtTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
                        hidEmpId.Value = item.EMPLOYEE_ID
                        txtFullName.Text = item.FULLNAME_VN
                        txtOrgName.Text = item.ORG_NAME
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
                            ctrlFindEmployeePopup.MultiSelect = True
                            ctrlFindEmployeePopup.IsHideTerminate = True
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
    Private Sub Reset_Find_Emp()
        hidEmpId.Value = Nothing
        txtFullName.Text = ""
        txtOrgName.Text = ""
        'txtEmpCode.Text = ""
        txtTitle.Text = ""
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
            ctrlOrgPopup.Show()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim orgItem = ctrlOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrg.Text = orgItem.NAME_VN
            End If
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub txtOrg_TextChanged(sender As Object, e As EventArgs) Handles txtOrg.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtOrg.Text.Trim <> "" Then
                    Dim List_org = rep.GetOrganizationLocationTreeView()
                    Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtOrg.Text.Trim.ToUpper)).ToList
                    If orgList.Count <= 0 Then
                        ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        ClearControlValue(hidOrgID, txtOrg)
                    ElseIf orgList.Count = 1 Then
                        hidOrgID.Value = orgList(0).ID
                        txtOrg.Text = orgList(0).NAME_VN
                    Else
                        List_Oganization_ID = (From p In orgList Select p.ID).ToList
                        btnFindOrg_Click(Nothing, Nothing)
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub chkIsEMP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsEMP.CheckedChanged
        Try
            If chkIsEMP.Checked = True Then
                hide1.Visible = True
                reqEmployeeCode.Enabled = True
                ClearControlValue(hidOrgID, txtOrg)
                txtOrg.Enabled = False
                btnFindOrg.Enabled = False
            Else
                hide1.Visible = False
                reqEmployeeCode.Enabled = True
                ClearControlValue(hidEmpId, txtEmpCode, txtFullName, txtOrgName, txtTitle)
                txtOrg.Enabled = True
                btnFindOrg.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rcOBJECT_EMPLOYEE_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcOBJECT_EMPLOYEE.SelectedIndexChanged
        Try
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim rep = New AttendanceRepository
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(rcOBJECT_EMPLOYEE.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdFromDate.SelectedDate = ddate.START_DATE
                rdToDate.SelectedDate = ddate.END_DATE
            End If
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim repS As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(rcOBJECT_EMPLOYEE.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdFromDate.SelectedDate = ddate.START_DATE
                rdToDate.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdFromDate, rdToDate)
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
        End Try

    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 14/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(If(datarow.GetDataKeyValue("ORG_DESC") IsNot Nothing, datarow.GetDataKeyValue("ORG_DESC").ToString, ""))
                datarow("ORG_NAME_TIMESHEET").ToolTip = Utilities.DrawTreeByString(If(datarow.GetDataKeyValue("ORG_DESC_TIMESHEET") IsNot Nothing, datarow.GetDataKeyValue("ORG_DESC_TIMESHEET").ToString, ""))
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class