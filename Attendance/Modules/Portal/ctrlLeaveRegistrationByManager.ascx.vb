Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI

Public Class ctrlLeaveRegistrationByManager
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

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
                    Dim lstApprove As New List(Of AT_PORTAL_REG_DTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim dto As New AT_PORTAL_REG_DTO
                        dto.ID = item.GetDataKeyValue("ID")
                        dto.YEAR = item.GetDataKeyValue("YEAR")
                        dto.STATUS = PortalStatus.ApprovedByLM
                        lstApprove.Add(dto)
                    Next
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
        rgMain.SetFilter()
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'SetFilter(rgMain)
        InitControl()
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
                        If dtDatas IsNot Nothing AndAlso dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Approve Leave Request (LM)")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                            Exit Sub
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
            Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient
            Dim ID_EMPLOYEE As Integer
            Dim NOTE As String
            Dim ID_REGGROUP As Integer
            Dim fromdate As Date
            Dim todate As Date
            Dim EmpName As String
            Dim ID_SIGN As Integer
            Dim DEPARTMENT_ID As Integer
            Dim DAYNUM As Integer
            Dim APP_LEVEL As String
            For Each dr As GridDataItem In rgMain.SelectedItems
                ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
                NOTE = dr.GetDataKeyValue("NOTE")
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                fromdate = dr.GetDataKeyValue("FROM_DATE")
                todate = dr.GetDataKeyValue("TO_DATE")
                EmpName = dr.GetDataKeyValue("EMPLOYEE_NAME")
                ID_SIGN = dr.GetDataKeyValue("ID_SIGN")
                DEPARTMENT_ID = dr.GetDataKeyValue("DEPARTMENT_ID")
                DAYNUM = dr.GetDataKeyValue("TOTAL_LEAVE")
                APP_LEVEL = dr.GetDataKeyValue("APP_LEVEL")

                Using rep As New AttendanceRepository
                    Dim periodid = rep.GetperiodID(ID_EMPLOYEE, fromdate, todate)
                    'Dim check = rep.CHECK_PERIOD_CLOSE(periodid)
                    'If check = 0 Then
                    '    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim orgId = rep.GetOrgIdLevel2(LogHelper.CurrentUser.EMPLOYEE_ID)
                    Dim checkOverMonth = rep.CheckOverMonth(LogHelper.CurrentUser.EMPLOYEE_ID, orgId, fromdate, todate)

                    If checkOverMonth <> -1 Then
                        ShowMessage(Translate(" Nhân viên " + EmpName + " đã đăng ký nghỉ quá " + CType(checkOverMonth, String) + " ngày trong tháng "), NotifyType.Warning)
                    End If
                    Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, -1, 1, "LEAVE", "", ID_REGGROUP)


                    If result = 0 Then
                        rep.INSERT_REGIMES(ID_REGGROUP, ID_SIGN)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End Using
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
        Dim fromdate As Date
        Dim todate As Date
        For Each dr As GridDataItem In rgMain.SelectedItems
            ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
            ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
            fromdate = dr.GetDataKeyValue("FROM_DATE")
            todate = dr.GetDataKeyValue("TO_DATE")
            Using rep As New AttendanceRepository
                Dim periodid = rep.GetperiodID(ID_EMPLOYEE, fromdate, todate)
                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, -1, 2, "LEAVE", strComment, ID_REGGROUP)
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
                LeaveMasters = rep.PRS_GETLEAVE_BY_APPROVE1(_filter)
            Else
                LeaveMasters = rep.PRS_GETLEAVE_BY_APPROVE1(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
            End If
            Dim strWher As String = "1=1 "
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                strWher += " AND  EMPLOYEE_CODE LIKE '" + "%" + _filter.EMPLOYEE_CODE + "%" + "'"
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                strWher += " AND  STATUS_NAME LIKE '" + "%" + _filter.STATUS_NAME + "%" + "'"
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                strWher += " AND  EMPLOYEE_NAME LIKE '" + "%" + _filter.EMPLOYEE_NAME + "%" + "'"
            End If
            If _filter.DEPARTMENT IsNot Nothing Then
                strWher += " AND  DEPARTMENT LIKE '" + "%" + _filter.DEPARTMENT + "%" + "'"
            End If
            If _filter.SIGN_NAME IsNot Nothing Then
                strWher += " AND  SIGN_NAME LIKE '" + "%" + _filter.SIGN_NAME + "%" + "'"
            End If
            If _filter.JOBTITLE IsNot Nothing Then
                strWher += " AND  JOBTITLE LIKE '" + "%" + _filter.JOBTITLE + "%" + "'"
            End If
            If _filter.FROM_DATE.HasValue Then
                strWher += " AND  FROM_DATE = '" + Date.Parse(_filter.FROM_DATE).ToString("dd/MM/yyyy") + "'"
            End If
            If _filter.TO_DATE.HasValue Then
                strWher += " AND  TO_DATE = '" + Date.Parse(_filter.TO_DATE).ToString("dd/MM/yyyy") + "'"
            End If

            If _filter.TOTAL_LEAVE IsNot Nothing Then
                strWher += " AND  TOTAL_LEAVE = '" + _filter.TOTAL_LEAVE.ToString() + "'"
            End If
            If _filter.NOTE IsNot Nothing Then
                strWher += " AND  NOTE LIKE '" + "%" + _filter.NOTE + "%" + "'"
            End If
            If _filter.MODIFIED_BY IsNot Nothing Then
                strWher += " AND  MODIFIED_BY LIKE '" + "%" + _filter.MODIFIED_BY + "%" + "'"
            End If
            If _filter.MODIFIED_DATE.HasValue Then
                strWher += " AND  MODIFIED_DATE = '" + Date.Parse(_filter.MODIFIED_DATE).ToString("dd/MM/yyyy") + "'"
            End If
            If _filter.REASON IsNot Nothing Then
                strWher += " AND  REASON LIKE '" + "%" + _filter.REASON + "%" + "'"
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
            Return Me.LeaveMasters

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class