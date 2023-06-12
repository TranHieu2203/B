Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDMVSApproveByManager
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance\Modules\Portal" + Me.GetType().Name.ToString()
#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property LeaveMasters As DataTable
        Get
            Return ViewState(Me.ID & "_LeaveMasters")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_LeaveMasters") = value
        End Set
    End Property

    Property LeaveMasterTotal As Int32
        Get
            Return ViewState(Me.ID & "_LeaveMasterTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_LeaveMasterTotal") = value
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

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                    'Dim lstApprove As New List(Of AT_PORTAL_REG_DTO)
                    'For idx = 0 To rgMain.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgMain.SelectedItems(idx)
                    '    Dim dto As New AT_PORTAL_REG_DTO
                    '    dto.ID = item.GetDataKeyValue("ID")
                    '    dto.YEAR = item.GetDataKeyValue("YEAR")
                    '    dto.STATUS = PortalStatus.ApprovedByLM
                    '    lstApprove.Add(dto)
                    'Next
                    'If rep.ApprovePortalRegList(lstApprove) Then
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    Refresh("UpdateView")
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    'End If
                    rgMain.Rebind()
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Request.Headers("NoSearch") IsNot Nothing Then
                RadPane4.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        rgMain.SetFilter()
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'SetFilter(rgMain)
        Try
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane4)
                GirdConfig(rgMain)
            Else
                If Request.Headers("NoSearch") IsNot Nothing Then
                    GirdConfig(rgMain)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt (QLTT)")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt (QLTT)")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New AttendanceRepository
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                        Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                        Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                        Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")
                cboStatus.SelectedValue = PortalStatus.WaitingForApproval
            End If
        End Using
        txtYear.Value = Date.Now.Year
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Approve DMVS ")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") = 1 Or dr.GetDataKeyValue("STATUS") = 2 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng cho đơn đăng ký nghỉ ở trạng thái Chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") = 1 Or dr.GetDataKeyValue("STATUS") = 2 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng cho đơn đăng ký nghỉ ở trạng thái Chờ phê duyệt."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlCommon_Reject.Show()
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim store As New AttendanceStoreProcedure
            Dim rep As New AttendanceRepository
            Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient
            Dim ID_EMPLOYEE As Integer
            Dim ID As Integer
            ''Dim NOTE As String
            Dim ID_REGGROUP As Integer
            Dim workingDay As Date
            Dim DEPARTMENT_ID As Integer

            ''Dim EmpName As String
            For Each dr As GridDataItem In rgMain.SelectedItems
                ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
                ''NOTE = dr.GetDataKeyValue("NOTE").ToString()
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                workingDay = dr.GetDataKeyValue("WORKINGDAY")
                ID = dr.GetDataKeyValue("ID")
                DEPARTMENT_ID = dr.GetDataKeyValue("DEPARTMENT")
                ''EmpName = dr.GetDataKeyValue("VN_FULLNAME")

                Dim periodid = rep.GetperiodID_2(ID_EMPLOYEE, workingDay)

                'Dim check = rep.CHECK_PERIOD_CLOSE(periodid)
                'If check = 0 Then
                '    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If

                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, periodid, 1, "WLEO", "", ID_REGGROUP)
                Dim objData = rep.GetLate_CombackoutById(ID)
                If result = 0 Then
                    If objData.STATUS = PortalStatus.ApprovedByLM Then
                        Dim re = store.UPDATE_INSERT_AT_SWIPE_DATA(ID, Nothing)
                        'Tổng hợp công gốc
                        store.CAL_TIME_TIMESHEET_EMP(LogHelper.GetUserLog.Username, workingDay, ID_EMPLOYEE)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Next
            rgMain.Rebind()
            UpdateControlState()
        End If
    End Sub

    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Dim strComment As String = e.Comment
        Dim ID_EMPLOYEE As Integer

        Dim ID_REGGROUP As Integer
        Dim workingDay As Date
        For Each dr As GridDataItem In rgMain.SelectedItems
            ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
            ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
            workingDay = dr.GetDataKeyValue("WORKINGDAY")
            Using rep As New AttendanceRepository
                Dim periodid = rep.GetperiodID_2(ID_EMPLOYEE, workingDay)
                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, periodid, 2, "WLEO", strComment, ID_REGGROUP)
                If result = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End Using
        Next
        Using rep As New AttendanceRepository
            'If rep.ApprovePortalRegList(lstReject) Then
            '    CurrentState = CommonMessage.STATE_NORMAL
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            '    Refresh("UpdateView")
            'Else
            '    CurrentState = CommonMessage.STATE_NORMAL
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            'End If
        End Using
        rgMain.Rebind()
        UpdateControlState()
    End Sub


#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_PORTAL_REG_DTO
        Try
            _filter.ID_EMPLOYEE = LogHelper.CurrentUser.EMPLOYEE_ID
            If Request.Headers("NoSearch") IsNot Nothing Then
                _filter.STATUS = 0
            Else
                If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                    _filter.STATUS = cboStatus.SelectedValue
                Else
                    _filter.STATUS = 5
                End If
                If txtYear.Value.HasValue Then
                    _filter.YEAR = txtYear.Value
                End If
            End If

            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                LeaveMasters = rep.PRS_GETDMVS_BY_APPROVE(_filter)
                Return LeaveMasters
            Else
                LeaveMasters = rep.PRS_GETDMVS_BY_APPROVE(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
            End If
            Dim strWher As String = "1=1 "
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                strWher += " AND  EMPLOYEE_CODE LIKE '" + "%" + _filter.EMPLOYEE_CODE + "%" + "'"
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                strWher += " AND  UPPER(STATUS_NAME) LIKE UPPER('" + "%" + _filter.STATUS_NAME + "%" + "')"
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                strWher += " AND  UPPER(EMPLOYEE_NAME) LIKE UPPER('" + "%" + _filter.EMPLOYEE_NAME + "%" + "')"
            End If
            If _filter.DEPARTMENT IsNot Nothing Then
                strWher += " AND  UPPER(DEPARTMENT) LIKE UPPER('" + "%" + _filter.DEPARTMENT + "%" + "')"
            End If
            If _filter.SIGN_NAME IsNot Nothing Then
                strWher += " AND  UPPER(SIGN_NAME) LIKE UPPER('" + "%" + _filter.SIGN_NAME + "%" + "')"
            End If
            If _filter.JOBTITLE IsNot Nothing Then
                strWher += " AND  UPPER(JOBTITLE) LIKE UPPER('" + "%" + _filter.JOBTITLE + "%" + "')"
            End If
            If _filter.WORKINGDAY.HasValue Then
                strWher += " AND  WORKINGDAY = '" + Date.Parse(_filter.WORKINGDAY).ToString("dd/MM/yyyy") + "'"
            End If
            If _filter.MINUTE IsNot Nothing Then
                strWher += " AND  MINUTE = '" + _filter.MINUTE.ToString() + "'"
            End If
            If _filter.NOTE IsNot Nothing Then
                strWher += " AND  UPPER(NOTE) LIKE UPPER('" + "%" + _filter.NOTE + "%" + "')"
            End If
            If _filter.MODIFIED_DATE.HasValue Then
                strWher += " AND  MODIFIED_DATE = '" + Date.Parse(_filter.MODIFIED_DATE).ToString("dd/MM/yyyy") + "'"
            End If
            If _filter.REASON IsNot Nothing Then
                strWher += " AND  UPPER(REASON) LIKE UPPER('" + "%" + _filter.REASON + "%" + "')"
            End If
            If _filter.REASON_NOT_AGREE IsNot Nothing Then
                strWher += " AND  UPPER(REASON_NOT_AGREE) LIKE UPPER('" + "%" + _filter.REASON_NOT_AGREE + "%" + "')"
            End If
            If Me.LeaveMasters.Select(strWher).Count > 0 Then
                Me.LeaveMasters = Me.LeaveMasters.Select(strWher).CopyToDataTable()
            Else
                Me.LeaveMasters = Me.LeaveMasters.Clone()
            End If
            rgMain.MasterTableView.FilterExpression = String.Empty
            If LeaveMasters.Rows.Count > 0 Then
                rgMain.VirtualItemCount = LeaveMasters.Rows.Count
            Else
                rgMain.VirtualItemCount = Me.LeaveMasterTotal
            End If

            rgMain.DataSource = Me.LeaveMasters
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class