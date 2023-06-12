Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Telerik.Web.UI

Public Class ctrlOTRegistrationByLM
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property RegistrationList As List(Of AT_OT_REGISTRATIONDTO)
        Get
            Return ViewState(Me.ID & "_OT_REGISTRATIONDTOS")
        End Get
        Set(ByVal value As List(Of AT_OT_REGISTRATIONDTO))
            ViewState(Me.ID & "_OT_REGISTRATIONDTOS") = value
        End Set
    End Property

    Property OtRegistrationTotal As Int32
        Get
            Return ViewState(Me.ID & "_OtRegistrationTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_OtRegistrationTotal") = value
        End Set
    End Property

    Property Is_Design As Boolean
        Get
            Return ViewState(Me.ID & "_Is_Design")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Is_Design") = value
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
            'Select Case CurrentState

            '    Case CommonMessage.STATE_APPROVE
            '        Dim lstApp As New List(Of AT_OT_REGISTRATIONDTO)
            '        For idx = 0 To rgMain.SelectedItems.Count - 1
            '            Dim item As GridDataItem = rgMain.SelectedItems(idx)
            '            Dim dto As New AT_OT_REGISTRATIONDTO
            '            dto.ID = item.GetDataKeyValue("ID")
            '            dto.REGIST_DATE = item.GetDataKeyValue("REGIST_DATE")
            '            'Kiem tra ky cong da dong hay chua
            '            Dim periodid = rep.GetperiodID_2(EmployeeID, dto.REGIST_DATE)
            '            If periodid = 0 Then
            '                ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
            '                Exit Sub
            '            ElseIf periodid = -1 Then
            '                ShowMessage(Translate("Đối tượng nhân viên chưa được khởi tạo chu kỳ chấm công"), NotifyType.Warning)
            '                Exit Sub
            '            ElseIf periodid = -2 Then
            '                ShowMessage(Translate("Nhân viên chưa được thiết lập đối tượng nhân viên"), NotifyType.Warning)
            '                Exit Sub
            '            End If
            '            Dim checkKicong = rep.CHECK_PERIOD_CLOSE(periodid)
            '            If checkKicong = 0 Then
            '                ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
            '                Exit Sub
            '            End If

            '            dto.STATUS = PortalStatus.ApprovedByLM
            '            dto.ID_REGGROUP = item.GetDataKeyValue("ID_REGGROUP")
            '            dto.REGIST_DATE = item.GetDataKeyValue("REGIST_DATE")
            '            dto.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            '            dto.REASON = ""
            '            lstApp.Add(dto)
            '        Next
            '        If Not rep.ApproveOtRegistration(lstApp, LogHelper.CurrentUser.EMPLOYEE_ID) Then
            '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            '        Else
            '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            '            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            '        End If
            '        rgMain.Rebind()
            'End Select
            rep.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            'BindData()
            If Request.Headers("NoSearch") IsNot Nothing Then
                RadPane4.Visible = False
            End If
            If Not IsPostBack Then
                Is_Design = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        SetFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt bởi QLTT")
            CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt (QLTT)")
            CType(MainToolBar.Items(2), RadToolBarButton).Text = Translate("Xuất Excel")
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

            rdRegDateFrom.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1)
            rdRegDateTo.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1).AddMonths(12).AddDays(-1)
        End Using
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
                            rgMain.ExportExcel(Server, Response, dtDatas, "Approve OT Request (LM)")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Thao tác này chỉ thực hiện với giờ làm thêm đang chờ phê duyệt, vui lòng chọn đơn khác"), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ctrlMessageBox.MessageText = Translate("Bạn có muốn phê duyệt dữ liệu này?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Thao tác này chỉ thực hiện với giờ làm thêm đang chờ phê duyệt, vui lòng chọn đơn khác"), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

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
            Dim ID_REGGROUP As Integer
            Dim workingDay As Date
            Dim DEPARTMENT_ID As Integer

            For Each dr As GridDataItem In rgMain.SelectedItems

                ID_EMPLOYEE = dr.GetDataKeyValue("EMPLOYEE_ID")
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                workingDay = dr.GetDataKeyValue("REGIST_DATE")
                DEPARTMENT_ID = dr.GetDataKeyValue("DEPARTMENT_ID")

                Dim periodid = rep.GetperiodID_2(ID_EMPLOYEE, workingDay)

                'Dim check = rep.CHECK_PERIOD_CLOSE(periodid)
                'If check = 0 Then
                '    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If

                Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, periodid, 1, "OVERTIME", "", ID_REGGROUP)

                If result = 0 Then
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
        Try
            Dim strComment As String = e.Comment
            Dim ID_EMPLOYEE As Integer

            Dim ID_REGGROUP As Integer
            Dim workingDay As Date
            For Each dr As GridDataItem In rgMain.SelectedItems
                ID_EMPLOYEE = dr.GetDataKeyValue("EMPLOYEE_ID")
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                workingDay = dr.GetDataKeyValue("REGIST_DATE")
                Using rep As New AttendanceRepository
                    Dim periodid = rep.GetperiodID_2(ID_EMPLOYEE, workingDay)
                    Dim result = rep.PRI_PROCESS(LogHelper.CurrentUser.EMPLOYEE_ID, ID_EMPLOYEE, periodid, 2, "OVERTIME", strComment, ID_REGGROUP)
                    If result = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End Using
            Next
            rgMain.Rebind()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_OT_REGISTRATIONDTO
        Dim dtData As New DataTable
        Try
            Dim MaximumRows As Integer
            _filter.EMPLOYEE_ID = LogHelper.CurrentUser.EMPLOYEE_ID
            ''_filter.P_MANAGER_ID = LogHelper.CurrentUser.EMPLOYEE_ID

            If Request.Headers("NoSearch") IsNot Nothing Then
                _filter.STATUS = 0
            Else
                If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                    _filter.STATUS = cboStatus.SelectedValue
                End If
                If rdRegDateFrom.SelectedDate.HasValue Then
                    _filter.REGIST_DATE_FROM = rdRegDateFrom.SelectedDate
                End If
                If rdRegDateTo.SelectedDate.HasValue Then
                    _filter.REGIST_DATE_TO = rdRegDateTo.SelectedDate
                End If
            End If

            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    dtData = rep.GetPortalOtApproveData(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize, "EMPLOYEE_CODE, REGIST_DATE desc")
                Else
                    dtData = rep.GetPortalOtApproveData(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            Else
                Return rep.GetPortalOtApproveData(_filter)
            End If
            If Not Is_Design Then
                DesignGrid(dtData)
            End If

            rgMain.VirtualItemCount = Me.OtRegistrationTotal
            rgMain.DataSource = dtData

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rNCol As GridNumericColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        Dim rColChkbox As GridCheckBoxColumn
        rgMain.MasterTableView.Columns.Clear()
        Me.Is_Design = True
        For Each column As DataColumn In dt.Columns
            If ("CONFIRM_OT_TT,TOTAL_DAY_TH,TOTAL_NIGHT_TH,TOTAL_OT_TH,TIMEIN_TT,TIMEOUT_TT").Split(",").Contains(column.ColumnName) Then
                Continue For
            End If
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgMain.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") And Not column.ColumnName.Contains("CHECK") Then
                rCol = New GridBoundColumn()
                rgMain.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.UniqueName = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
                If column.ColumnName = "STT" OrElse column.ColumnName = "MODIFIED_BY" OrElse column.ColumnName = "WORK_STATUS" _
                    OrElse column.ColumnName = "IS_DELETED" OrElse column.ColumnName = "STT" Then
                    rCol.Visible = False
                End If
                If column.ColumnName = "REASON" Then
                    rCol.HeaderText = "Lý do không duyệt"
                End If
                If column.ColumnName = "STATUS_NAME" Then
                    rCol.HeaderText = "Trạng thái"
                End If
                If column.ColumnName = "NOTE" Then
                    rCol.HeaderText = "Lý do làm thêm"
                End If
                If column.ColumnName = "MODIFIED_NAME" Then
                    rCol.HeaderText = "Người tạo đơn"
                End If
            End If
            If column.ColumnName.Contains("ORG_OT_ID_NAME") Then
                rCol = New GridBoundColumn()
                rgMain.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.UniqueName = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130

            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgMain.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.UniqueName = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                If column.ColumnName = "CREATED_DATE" Then
                    rColDate.Visible = False
                End If
                If column.ColumnName = "MODIFIED_DATE" Then
                    rColDate.HeaderText = "Ngày tạo đơn"
                End If
            End If

            If column.ColumnName.Contains("JOIN_SALARY_INS") Then
                rNCol = New GridNumericColumn()
                rgMain.MasterTableView.Columns.Add(rNCol)
                rNCol.UniqueName = column.ColumnName
                rNCol.DataField = column.ColumnName
                rNCol.DataFormatString = ConfigurationManager.AppSettings("FNUMBERGRID")
                rNCol.HeaderText = Translate(column.ColumnName)
                rNCol.HeaderTooltip = Translate(column.ColumnName)
                rNCol.FilterControlToolTip = Translate(column.ColumnName)
                rNCol.HeaderStyle.Width = 150
                rNCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rNCol.AutoPostBackOnFilter = True
                rNCol.CurrentFilterFunction = GridKnownFunction.Contains
                rNCol.ShowFilterIcon = False
                rNCol.FilterControlWidth = 130
            End If

            If column.ColumnName.Contains("CHECK") Then
                rColChkbox = New GridCheckBoxColumn()
                rgMain.MasterTableView.Columns.Add(rColChkbox)
                rColChkbox.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColChkbox.DataField = column.ColumnName
                rColChkbox.HeaderStyle.Width = 150
                rColChkbox.HeaderText = Translate(column.ColumnName)
                rColChkbox.HeaderTooltip = Translate(column.ColumnName)
                rColChkbox.FilterControlToolTip = Translate(column.ColumnName)
                rColChkbox.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                rColChkbox.AllowFiltering = False
                rColChkbox.ShowFilterIcon = False
            End If
        Next
    End Sub

#End Region

End Class